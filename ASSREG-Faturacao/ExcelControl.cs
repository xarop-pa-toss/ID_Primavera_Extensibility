using System; using System.Data; using System.Linq; using System.IO;using System.Runtime.InteropServices;
using _Excel = Microsoft.Office.Interop.Excel; using DataSet = System.Data.DataSet; using DataTable = System.Data.DataTable; using OleDb = System.Data.OleDb;
using Primavera.Extensibility.Sales.Editors; using Primavera.Extensibility.BusinessEntities;
using System.Collections.Generic; using ExcelInterop = Microsoft.Office.Interop.Excel; using System.Data.SqlClient;

namespace ASRLB_ImportacaoFatura
{
    public class ExcelControl : EditorVendas
    {
        internal string FicheiroCopiado { get; }
        public DataSet DtSet { get; }
        public string conString { get; set; }
        public List<string> folhasList { get; set; }


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
                    Ligacao.Open();

                    // Get 
                    DataTable dtExcelSchema = Ligacao.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, null);
                    folhasList = new List<string>(dtExcelSchema.Rows.Count);
                    foreach (DataRow row in dtExcelSchema.Rows)
                    {
                        string nomeFolha = row["TABLE_NAME"].ToString();
                        if (nomeFolha.EndsWith("$") || nomeFolha.EndsWith("$'")) // Exclui hidden sheets
                            folhasList.Add(row["TABLE_NAME"].ToString());
                    }
                    
                    Ligacao.Close();

                    DataSet DtSet = new DataSet();
                    OleDb.OleDbCommand cmdTotalLinhas;
                    OleDb.OleDbCommand cmdConteudoLinha;
                    OleDb.OleDbDataAdapter Adapter;
                    int ind = 0;

                    foreach (string nomeFolha in folhasList)
                    {
                        Ligacao.Open();

                        // Get total linhas usadas
                        cmdTotalLinhas = new OleDb.OleDbCommand ("SELECT Count(*) FROM [" + nomeFolha + "];", Ligacao);
                        int linhasTotal = (int)cmdTotalLinhas.ExecuteScalar() - 5;
                        cmdTotalLinhas.Dispose();
                        

                        // Get linhas da folha. Preenche adapter
                        //string query = "SELECT F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18 FROM [" + nomeFolha + "A6:R" + linhasTotal + "];";
                        string query = "SELECT F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18 FROM [" + nomeFolha + "A6:R" + linhasTotal + "];";
                        cmdConteudoLinha = new OleDb.OleDbCommand(query, Ligacao);

                        Adapter = new OleDb.OleDbDataAdapter();
                        Adapter.SelectCommand = cmdConteudoLinha;
                        Adapter.Fill(DtSet, "Tabela" + ind);
                        
                        Adapter.Dispose();
                        cmdConteudoLinha.Dispose();
                        Ligacao.Close();

                        DataTable DtTable = DtSet.Tables[ind];
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

                        DtTable.DefaultView.Sort = "Benef";


                        // *** Validação das linhas de acordo com critérios ***
                        // DataTable.Delete() não apaga linha no momento mas marca para ser apagada. Só quando se chama DataTable.AcceptChanges() é que todas as linhas marcadas são removidas. ***
                        DtTable.AcceptChanges(); // Deixa a DataTable num estado estável para poder ser manipulada sem erros.
                        
                        string processar, predio, benef;

                        for (int lin = 0; lin < DtTable.Rows.Count; lin++)
                        {
                            // Se contador for nulo, apaga linha e ignora.
                            if (DtTable.Rows[lin].Field<string>("Nº Contador") == null)
                            {
                                DtTable.Rows[lin].Delete();
                                continue;
                            }

                            // Se não tiver leituras, apaga linha e ignora
                            if (DtTable.Rows[lin].Field<DateTime?>("Data 1").ToString() == null && DtTable.Rows[lin].Field<DateTime?>("Data 2").ToString() == null)
                            {
                                DtTable.Rows[lin].Delete();
                                continue;
                            }

                            processar = DtTable.Rows[lin].Field<string>("Processar").Trim();
                            if (processar == "N" || processar == null || processar == "") { DtTable.Rows[lin].Delete(); continue; }
                            else if (processar != "S") { throw new Exception("Valor da coluna 'Processar' ( " + processar + " ) na linha " + (lin - 5).ToString() + " da folha " + nomeFolha + " não é valido.\n\n DtTable.Rows.Count: " + DtTable.Rows.Count+"\nLinhasTotal : " + (linhasTotal) + "\nlin: " + lin); return DtSet; }

                            benef = DtTable.Rows[lin].Field<double>("Benef").ToString().PadLeft(5, '0');
                            predio = DtTable.Rows[lin].Field<string>("Prédio");
                        }

                        DtTable.AcceptChanges();

                        // Nova primeira coluna com numeração das linhas
                        DtTable.Columns.Add("#", typeof(int)).SetOrdinal(0);
                        for (int i = 0; i < DtTable.Rows.Count; i++) { DtTable.Rows[i][0] = i + 1; }

                        ind++;
                    }
                    Ligacao.Close();
                    return DtSet;
                }
                catch (IOException e) { PSO.MensagensDialogos.MostraAviso("Não foi possível estabelecer ligação ao ficheiro!\n\n",StdPlatBS100.StdBSTipos.IconId.PRI_Critico, e.ToString()); Ligacao.Close(); return DtSet; }
                catch (Exception e) { PSO.MensagensDialogos.MostraAviso("Erro não especificado.\n\n",StdPlatBS100.StdBSTipos.IconId.PRI_Critico, e.ToString()); Ligacao.Close(); return DtSet; }

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