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

                string conString = @"Provider=Microsoft.ACE.OLEDB.12.0;"
                            + "Data Source='" + FicheiroCopiado + "'"
                            + ";Extended Properties=\"Excel 12.0;HDR=NO;\"";

                // Trata ficheiro e carrega para DataSet
                RemoverCelulasUnidas(FicheiroCopiado);
            }
            catch (ExcelControlException e) { System.Windows.Forms.MessageBox.Show(e);}

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
            catch (IOException e) { throw new ExcelControlException("Não foi possível aceder ao ficheiro. \nSerá que está aberto no Excel?"); }
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
        internal DataSet CarregarDataSet(string folha, string conString)
        {
            using (OleDb.OleDbConnection Ligacao = new OleDb.OleDbConnection(conString))
            {
                Ligacao.Open();
                // Conta linhas preenchidas
                OleDb.OleDbCommand cmd = new OleDb.OleDbCommand("SELECT Count(*) FROM [" + folha + "$]", Ligacao);
                int linhasTotal = (int)cmd.ExecuteScalar() - 5;

                // Datasets a preencher e query
                DataSet DtSet = new DataSet();
                string query = "SELECT F3,F4,F5,F6,F7,F9,F11,F12 FROM [" + folha + "$A6:Z]";

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

                    // Validação das linhas de acordo com os seguintes critérios:
                    // Ligado == S
                    // Contador 1 - 1 Benef.
                    // Contador != null
                    for (int lin = 0; lin < DtTable.Rows.Count; lin++)
	                {
                        string contador = DtTable.Rows[lin].Field<string>("Nº Contador");
                        if ( contador == null ) { DtTable.Rows[lin].Delete(); continue; }
                        
                        string benef = DtTable.Rows[lin].Field<string>("Benef.");
                        bool x;

                        while ()
                        {

                        }

	                }

                    // Nova primeira coluna com numeração das linhas
                    DtTable.Columns.Add("#", typeof(int)).SetOrdinal(0);
                    for (int i = 0; i < DtTable.Rows.Count; i++) { DtTable.Rows[i][0] = i + 1; }

                    Ligacao.Close();
                    return DtSet;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Não foi possível estabelecer ligação! Erro: " + e); return DtSet;
                    //PSO.MensagensDialogos.MostraErro("Não foi possível estabelecer ligação! Erro: " + e); return DtTable;
                }
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

