using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks; using System.IO; using System.Data;
using OleDb = System.Data.OleDb; using DataSet = System.Data.DataSet; using DataTable = System.Data.DataTable;

using _Excel = Microsoft.Office.Interop.Excel; using System.Runtime.InteropServices;

namespace ASSREG_Faturacao_ExcelStandalone
{
    public class ExcelControl
    {
        internal string FicheiroCopiado { get; }
        public DataSet DtSet { get; }
        public string conString { get; set; }


        public ExcelControl(string origem)
        {
            try
            {
                // Copía ficheiro original com ligação ao Excel por OLEDB (na cópia). Devolve erro e termina se ficheiro não for valido.
                FicheiroCopiado = CopiarExcel(origem);
                if (FicheiroCopiado == null) throw new ExcelControlException("Erro ao copiar ficheiro Excel.");

                conString = @"Provider=Microsoft.ACE.OLEDB.12.0;"
                            + "Data Source='" + FicheiroCopiado + "'"
                            + ";Extended Properties=\"Excel 12.0;HDR=NO;\"";

                // Trata ficheiro e carrega para DataSet
                RemoverCelulasUnidas(FicheiroCopiado);
            }
            catch (ExcelControlException e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }
        
        // Encontra o texto após a última / (nome do ficheiro Excel) e cria a cópia a ser usada pelo resto do programa. Usado no constructor.
        private string CopiarExcel(string origem)
        {
            try
            {
                string destino = Path.GetDirectoryName(origem) + "AssReg_Leituras_Copia.xlsx";
                File.Copy(origem, destino, true);
                return destino;
            }
            catch (IOException) { throw new ExcelControlException("Não foi possivel abrir o ficheiro. Será que está aberto no Excel? Verifique se o 'Microsoft Excel' não está aberto no Gestor de Tarefas (Ctrl+Shift+Esc)."); }
            catch (UnauthorizedAccessException) { throw new ExcelControlException("Este utilizador não tem permissões para aceder ao ficheiro Excel. "); }
        }

        // Corre todas as células de todas as folhas do Workbook. Se encontrar células unidas (merged) remove a união e preenche o espaço com o valor original da célula unída.
        internal void RemoverCelulasUnidas (string path)
        {
            _Excel.Application App = new _Excel.Application();
            _Excel.Workbook wb = App.Workbooks.Open(path, 0, false, 5, "", "", true, _Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            // Corre apenas folhas com "Cantão" no nome para reduzir impacto na performance.
            // https://stackoverflow.com/questions/60493386/unmerging-excel-rows-and-duplicate-data-c-sharp
            foreach (_Excel.Worksheet folha in App.Worksheets)
            {
                if (folha.Name.Substring(0, 6) == "Cantão")
                {
                    _Excel.Worksheet ws = App.Worksheets[folha.Index];
                    int ultLinha = ws.Cells.SpecialCells(_Excel.XlCellType.xlCellTypeLastCell).Row;

                    foreach (_Excel.Range cell in ws.UsedRange)
                    {
                        if (cell.MergeCells)
                        {
                            _Excel.Range cellUnidas = cell.MergeArea;
                            cell.MergeCells = false;
                            cellUnidas.Value = cell.Value;
                        }
                    }
                }
            }

            // É necessário terminar correctamente os processos do Interop para que a folha de Excel não fique pendurada em memória
            wb.Close(true, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
            App.Quit();
            try
            {
                Marshal.ReleaseComObject(App);
                App = null;
            }
            catch
            {
                throw new ExcelControlException("Não foi possivel terminar a instância de Excel. Por favor termine o Microsoft Excel no Gestor de Tarefas.");
                App = null;
            }
            finally { GC.Collect(); }
        }

        // Abre ligação e preenche DataSet com query ao ficheiro Excel. Fecha ligação no final.
        public DataSet CarregarDataSet(string folha, string conString)
        {
            using (OleDb.OleDbConnection Ligacao = new OleDb.OleDbConnection(conString))
            {
                Ligacao.Open();
                // Conta linhas preenchidas
                OleDb.OleDbCommand cmd = new OleDb.OleDbCommand("SELECT Count(*) FROM [" + folha + "$]", Ligacao);
                int linhasTotal = (int)cmd.ExecuteScalar() - 5;

                // Datasets a preencher e query
                DataSet DtSet = new DataSet();
                string query = "SELECT F3,F4,F5,F6,F7,F9,F12,F13 FROM [" + folha + "$A6:Z]";

                // Inicialização do Adapter que faz de imediato a query ao Excel. Preenchimento e configuração do Dataset.
                try
                {
                    OleDb.OleDbDataAdapter Adapter = new OleDb.OleDbDataAdapter(query, Ligacao);
                    Adapter.Fill(DtSet, "Tabela0");
                    Adapter.Dispose();

                    DataTable DtTable = DtSet.Tables[0];
                    // Cabeçalhos das colunas e remoção de linhas não usadas (1 a 4)
                    DtTable.Columns[0].ColumnName = "Prédio";
                    DtTable.Columns[1].ColumnName = "Nº Contador";
                    DtTable.Columns[2].ColumnName = "Benef.";
                    DtTable.Columns[3].ColumnName = "Nome";
                    DtTable.Columns[4].ColumnName = "Última Leitura";
                    DtTable.Columns[5].ColumnName = "Ligado";
                    DtTable.Columns[6].ColumnName = "Data 1";
                    DtTable.Columns[7].ColumnName = "Leitura 1";

                    // *** Validação das linhas de acordo com critérios ***
                    // DataTable.Delete() não apaga no momento mas marca para ser apagada. Só quando se chama DataTable.AcceptChanges() é que todas as linhas marcadas são removidas. ***
                    DtTable.AcceptChanges(); // Deixa a DataTable num estado estável para poder ser manipulada sem erros.

                    for (int lin = 0; lin < DtTable.Rows.Count; lin++)
	                {
                        // Contador != null
                        double? contador = DtTable.Rows[lin].Field<double?>("Nº Contador");
                        if (contador == null) { DtTable.Rows[lin].Delete(); continue; }

                        // Ligado == S
                        string ligado = DtTable.Rows[lin].Field<string>("Ligado");
                        if (ligado == "N") { DtTable.Rows[lin].Delete(); continue; }
                        if (ligado != "S") { throw new ExcelControlException("Valor da coluna 'Ligado' na linha " + (lin - 5).ToString() + " do Excel não é valido."); }

                        
                        double? benef = DtTable.Rows[lin].Field<double?>("Benef.");
                        string predio = DtTable.Rows[lin].Field<string>("Prédio");

                        int x = 0;
                        while (x == x)
                        {
                            // Contador 1 - 1 Benef.
                            if (DtTable.Rows[lin + x + 1].Field<double?>("Nº Contador") == contador && DtTable.Rows[lin + x + 1].Field<double?>("Benef.") != benef)
                            { 
                                DtTable.Rows[lin + x + 1].Delete(); 
                                x += 1; }
                            // Contador 1 - ∞ Prédio
                            else if (DtTable.Rows[lin + x + 1].Field<double?>("Nº Contador") == contador && DtTable.Rows[lin + x + 1].Field<string>("Prédio") != predio)
                            { 
                                DtTable.Rows[lin][0] = DtTable.Rows[lin][0] + "," + DtTable.Rows[lin + x + 1].Field<string>("Prédio"); 
                                DtTable.Rows[lin + x + 1].Delete();
                                x += 1; }
                            else { lin += x;  break; }
                        }
                        //LINQ
                        //TabelaSemContadores.AsEnumerable().Where(x => x["Nº Contador"] == null) ;
                        //TabelaBenefs.AsEnumerable().GroupBy(x => x["Nº Contador"]).Where(x => x.Count() > 1);
                    }
                    DtTable.AcceptChanges();

                    // Nova primeira coluna com numeração das linhas
                    DtTable.Columns.Add("#", typeof(int)).SetOrdinal(0);
                    for (int i = 0; i < DtTable.Rows.Count; i++) { DtTable.Rows[i][0] = i + 1; }

                    Ligacao.Close();
                    return DtSet;
                }
                catch (IOException e) { System.Windows.Forms.MessageBox.Show("Não foi possível estabelecer ligação! Erro: " + e); Ligacao.Close();  return DtSet; }
                catch (Exception e) { System.Windows.Forms.MessageBox.Show("Excepção não tratada: "+ e); Ligacao.Close(); return DtSet; }
            }
        }

        [Serializable]
        public class ExcelControlException : Exception
        {
            public ExcelControlException() { }
            public ExcelControlException(string message) : base(message) { }
            public ExcelControlException(string message, Exception inner) : base(message, inner) { }
            protected ExcelControlException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}

