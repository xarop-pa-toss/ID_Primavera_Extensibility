using System; using System.Data; using System.Linq; using System.IO;using System.Runtime.InteropServices;
using _Excel = Microsoft.Office.Interop.Excel; using DataSet = System.Data.DataSet; using DataTable = System.Data.DataTable; using OleDb = System.Data.OleDb;
using Primavera.Extensibility.Sales.Editors; using Primavera.Extensibility.BusinessEntities;
using System.Collections.Generic; using ExcelInterop = Microsoft.Office.Interop.Excel;

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
                            + ";Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1;\"";

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
        public DataSet CarregarDataSet(string origem, string conString)
        {
            using (OleDb.OleDbConnection Ligacao = new OleDb.OleDbConnection(conString))
            {
                try
                {
                    OleDb.OleDbCommand cmdTotalLinhas = new OleDb.OleDbCommand();
                    OleDb.OleDbCommand cmdLinhas = new OleDb.OleDbCommand();
                    cmdTotalLinhas.Connection = Ligacao;
                    cmdLinhas.Connection = Ligacao;
                    Ligacao.Open();

                    DataTable dtExcelSchema = Ligacao.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, null);
                    List<string> folhasList = new List<string>(dtExcelSchema.Rows.Count);
                    foreach (DataRow row in dtExcelSchema.Rows)
                    {
                        string nomeFolha = row["TABLE_NAME"].ToString();
                        if (nomeFolha.EndsWith("$") || nomeFolha.EndsWith("$'")) // Exclui hidden sheets
                            folhasList.Add(row["TABLE_NAME"].ToString());
                    }

                    DataSet DtSet = new DataSet();
                    DtSet.Tables.Add("Tabela0");
                    DataTable DtTable = DtSet.Tables["Tabela0"];

                    foreach (string nomeFolha in folhasList)
                    {
                        //// Necessário remover plicas ' no inicio e fim do nome
                        //string nomeFolha = dtExcelSchema.Rows[row]["TABLE_NAME"].ToString();
                        //nomeFolha = nomeFolha.Remove(nomeFolha.Length - 2, 2);
                        //nomeFolha = nomeFolha.Remove(0, 1);
                        System.Windows.Forms.MessageBox.Show(nomeFolha);
                        // Get total linhas usadas
                        cmdTotalLinhas.CommandText = "SELECT Count(*) FROM [" + nomeFolha + "]";
                        int linhasTotal = (int)cmdTotalLinhas.ExecuteScalar() - 5;
                        System.Windows.Forms.MessageBox.Show("Test");

                        // Get linhas da folha. Preenche adapter
                        string query = "SELECT F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18 FROM [" + nomeFolha + "A6:Z];";
                        OleDb.OleDbDataAdapter Adapter = new OleDb.OleDbDataAdapter(query, Ligacao);
                        Adapter.Fill(DtSet, "Tabela0");
                    }

                    // Cabeçalhos das colunas
                    DtTable.Columns.Add("Prédio");
                    DtTable.Columns.Add("Área");
                    DtTable.Columns.Add("Cultura");
                    DtTable.Columns.Add("Processar");
                    DtTable.Columns.Add("Contador Ligado");
                    DtTable.Columns.Add("TRH");
                    DtTable.Columns.Add("Tx Penalizadora");
                    DtTable.Columns.Add("Nº Contador");
                    DtTable.Columns.Add("Benef");
                    DtTable.Columns.Add("Nome");
                    DtTable.Columns.Add("Última Leitura");
                    DtTable.Columns.Add("Data 1");
                    DtTable.Columns.Add("Leitura 1");
                    DtTable.Columns.Add("Data 2");
                    DtTable.Columns.Add("Leitura 2");

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
                        System.Windows.Forms.MessageBox.Show(benef + " " + predio + " " + processar);
                    }
                    DtTable.AcceptChanges();

                    // Nova primeira coluna com numeração das linhas
                    DtTable.Columns.Add("#", typeof(int)).SetOrdinal(0);
                    for (int i = 0; i < DtTable.Rows.Count; i++) { DtTable.Rows[i][0] = i + 1; }

                    System.Windows.Forms.MessageBox.Show("Excel Control returning DtSet");
                    return DtSet;
                }
                catch (IOException e) { System.Windows.Forms.MessageBox.Show("Não foi possível estabelecer ligação ao ficheiro! \n\n " + e); Ligacao.Close(); return DtSet; }
                catch (Exception e) { System.Windows.Forms.MessageBox.Show("Excepção não tratada! \n\n " + e); Ligacao.Close(); return DtSet; }

                return DtSet;
            }
        }
        public void EliminarCopia(string origem)
        {
            string copiaPath = Path.GetDirectoryName(origem) + "\\\\copia.xlsx";
            if (File.Exists(copiaPath)) { File.Delete(copiaPath); }
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





