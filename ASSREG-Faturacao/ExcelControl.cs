using System; using System.Data; using System.IO;using System.Runtime.InteropServices;
using _Excel = Microsoft.Office.Interop.Excel; using DataSet = System.Data.DataSet; using DataTable = System.Data.DataTable; using OleDb = System.Data.OleDb;
using Primavera.Extensibility.Sales.Editors; using Primavera.Extensibility.BusinessEntities;

namespace ASRLB_ImportacaoFatura
{
    public class ExcelControl : EditorVendas
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
                string destino = Path.GetDirectoryName(origem) + "\\\\copia.xlsx";
                File.Copy(origem, destino, true);
                return destino;
            }
            catch (IOException) { throw new ExcelControlException("Não foi possivel abrir o ficheiro. Será que está aberto no Excel? Verifique se o 'Microsoft Excel' não está aberto no Gestor de Tarefas (Ctrl+Shift+Esc)."); }
            catch (UnauthorizedAccessException) { throw new ExcelControlException("Este utilizador não tem permissões para aceder ao ficheiro Excel. "); }
        }

        // Corre todas as células de todas as folhas do Workbook. Se encontrar células unidas (merged) remove a união e preenche o espaço com o valor original da célula unída.
        internal void RemoverCelulasUnidas(string path)
        {
            _Excel.Application App = new _Excel.Application();
            _Excel.Workbook wb = App.Workbooks.Open(path, 0, false, 5, "", "", true, _Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            // Corre apenas folhas com "Cantão" no nome para reduzir impacto na performance.
            // https://stackoverflow.com/questions/60493386/unmerging-excel-rows-and-duplicate-data-c-sharp
            foreach (_Excel.Worksheet folha in App.Worksheets)
            {
                if (folha.Name.Substring(0, 6) == "CANTAO")
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
                string query = "SELECT F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18 FROM [" + folha + "$A6:Z]";

                // Inicialização do Adapter que faz de imediato a query ao Excel. Preenchimento e configuração do Dataset.
                try
                {
                    OleDb.OleDbDataAdapter Adapter = new OleDb.OleDbDataAdapter(query, Ligacao);
                    Adapter.Fill(DtSet, "Tabela0");
                    Adapter.Dispose();
                    Ligacao.Close();

                    DataTable DtTable = DtSet.Tables[0];
                    // Cabeçalhos das colunas
                    DtTable.Columns[0].ColumnName = "Prédio";
                    DtTable.Columns[1].ColumnName = "Área";
                    DtTable.Columns[2].ColumnName = "Cultura";
                    DtTable.Columns[3].ColumnName = "Processar";
                    DtTable.Columns[4].ColumnName = "Contador Ligado";
                    DtTable.Columns[5].ColumnName = "TRH";
                    DtTable.Columns[6].ColumnName = "Tx Penalizadora";
                    DtTable.Columns[7].ColumnName = "Nº Contador";
                    DtTable.Columns[8].ColumnName = "Benef";
                    DtTable.Columns[9].ColumnName = "Nome";
                    DtTable.Columns[10].ColumnName = "Última Leitura";
                    DtTable.Columns[11].ColumnName = "Data 1";
                    DtTable.Columns[12].ColumnName = "Leitura 1";
                    DtTable.Columns[13].ColumnName = "Data 2";
                    DtTable.Columns[14].ColumnName = "Leitura 2";

                    //DtTable.DefaultView.Sort = "Benef";


                    // *** Validação das linhas de acordo com critérios ***
                    // DataTable.Delete() não apaga linha no momento mas marca para ser apagada. Só quando se chama DataTable.AcceptChanges() é que todas as linhas marcadas são removidas. ***
                    DtTable.AcceptChanges(); // Deixa a DataTable num estado estável para poder ser manipulada sem erros.

                    string processar, predio, contador, benef;

                    for (int lin = 0; lin < DtTable.Rows.Count; lin++)
                    {
                        // Contador != null
                        contador = DtTable.Rows[lin].Field<string>("Nº Contador");
                        if (contador == null) { DtTable.Rows[lin].Delete(); continue; }

                        // Processar = S
                        processar = DtTable.Rows[lin].Field<string>("Processar");
                        if (processar == "N") { DtTable.Rows[lin].Delete(); continue; }
                        if (processar != "S") { throw new ExcelControlException("Valor da coluna 'Processar' na linha " + (lin - 5).ToString() + " do Excel não é valido."); return DtSet; }

                        benef = DtTable.Rows[lin].Field<string>("Benef").PadLeft(5, '0');
                        predio = DtTable.Rows[lin].Field<string>("Prédio");

                        /*
                        int x = 0;
                        while (x == x)
                        {
                        // Contador 1 - 1 Benef.
                            if (DtTable.Rows[lin + x + 1].Field<double?>("Nº Contador") == contador && DtTable.Rows[lin + x + 1].Field<double?>("Benef.") != benef)
                            { 
                                DtTable.Rows[lin + x + 1].Delete(); 
                                x += 1; }
                        // Contador 1 - ∞ Prédio
                            if (DtTable.Rows[lin + x + 1].Field<double?>("Nº Contador") == contador && DtTable.Rows[lin + x + 1].Field<string>("Prédio") != predio)
                            { 
                                DtTable.Rows[lin][0] = DtTable.Rows[lin][0] + "," + DtTable.Rows[lin + x + 1].Field<string>("Prédio"); 
                                DtTable.Rows[lin + x + 1].Delete();
                                x += 1; }
                            else { lin += x;  break; }
                        } */
                    }
                    DtTable.AcceptChanges();

                    // Nova primeira coluna com numeração das linhas
                    DtTable.Columns.Add("#", typeof(int)).SetOrdinal(0);
                    for (int i = 0; i < DtTable.Rows.Count; i++) { DtTable.Rows[i][0] = i + 1; }

                    Ligacao.Close();
                    return DtSet;
                }
                catch (IOException e) { System.Windows.Forms.MessageBox.Show("Não foi possível estabelecer ligação ao ficheiro! \n\n " + e); return DtSet; }
                catch (Exception e) { System.Windows.Forms.MessageBox.Show("Excepção não tratada! \n\n " + e); return DtSet; }
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

