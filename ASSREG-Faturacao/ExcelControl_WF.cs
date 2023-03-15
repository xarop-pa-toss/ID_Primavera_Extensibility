using System; using System.Data; using System.Linq; using System.IO;using System.Runtime.InteropServices;
using _Excel = Microsoft.Office.Interop.Excel; using DataSet = System.Data.DataSet; using DataTable = System.Data.DataTable; using OleDb = System.Data.OleDb;
using Primavera.Extensibility.Sales.Editors; using Primavera.Extensibility.BusinessEntities;
using System.Collections.Generic; using ExcelInterop = Microsoft.Office.Interop.Excel; using System.Data.SqlClient;

namespace ASRLB_ImportacaoFatura
{
    public class ExcelControl_WF : EditorVendas
    {
        internal string FicheiroCopiado { get; }
        public DataSet DtSet { get; }
        public string conString { get; set; }
        public List<string> folhasList { get; set; }


        public ExcelControl_WF(string origem)
        {
            try
            {
                // Copía ficheiro original com ligação ao Excel por OLEDB (na cópia). Devolve erro e termina se ficheiro não for valido.
                FicheiroCopiado = CopiarExcel(origem);
                if (FicheiroCopiado == null) throw new ExcelControlException("Erro ao copiar ficheiro Excel.");

                conString = @"Provider=Microsoft.ACE.OLEDB.12.0;"
                            + "Data Source='" + FicheiroCopiado + "'"
                            + ";Extended Properties=\"Excel 12.0 Xml;HDR=NO;\"";

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
                throw new ExcelControlException("Não foi possivel terminar a instância de Excel. Por favor termine o Microsoft Excel no Gestor de Tarefas.\n\n");
                App = null;
            }
            finally { GC.Collect(); }
        }

        // Abre ligação e preenche DataSet com query ao ficheiro Excel. Fecha ligação no final.
        public DataSet CarregarDataSet(string origem, string conString, ref List<string> errosExcel)
        {
            using (OleDb.OleDbConnection Ligacao = new OleDb.OleDbConnection(conString))
            {
                try
                {
                    Ligacao.Open();
                    // Get schema for sheet names
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

                    Ligacao.Open();
                    foreach (string nomeFolha in folhasList)
                    {
                        // Get total linhas usadas
                        cmdTotalLinhas = new OleDb.OleDbCommand("SELECT Count(*) FROM [" + nomeFolha + "];", Ligacao);
                        int linhasTotal = (int)cmdTotalLinhas.ExecuteScalar() - 5;
                        cmdTotalLinhas.Dispose();

                        // Get linhas da folha. Preenche adapter
                        string query = "SELECT F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18 FROM [" + nomeFolha + "A6:R" + (linhasTotal + 5) + "];";
                        cmdConteudoLinha = new OleDb.OleDbCommand(query, Ligacao);
                        Adapter = new OleDb.OleDbDataAdapter();

                        Adapter.SelectCommand = cmdConteudoLinha;
                        Adapter.Fill(DtSet, "Tabela" + ind);
                        Adapter.Dispose();
                        cmdConteudoLinha.Dispose();
                        //System.Windows.Forms.MessageBox.Show("Tabela" + ind + " rows: " + DtSet.Tables["Tabela" + ind].Rows.Count + "\n linhastotal: "+ linhasTotal);
                        ind++;
                    }
                    Ligacao.Close();

                    // Copia estrutura mas não os dados
                    DataTable DtTableBuffer = DtSet.Tables["Tabela0"].Clone();
                    DtTableBuffer.TableName = "Tabela";
                    DtTableBuffer.Columns[0].ColumnName = "Prédio";
                    DtTableBuffer.Columns[1].ColumnName = "Área";
                    DtTableBuffer.Columns[2].ColumnName = "Cultura";
                    DtTableBuffer.Columns[3].ColumnName = "Processar";
                    DtTableBuffer.Columns[4].ColumnName = "Contador Ligado";
                    DtTableBuffer.Columns[5].ColumnName = "TRH";
                    DtTableBuffer.Columns[6].ColumnName = "Tx Penalizadora";
                    DtTableBuffer.Columns[7].ColumnName = "Nº Contador";
                    DtTableBuffer.Columns[8].ColumnName = "Benef";
                    DtTableBuffer.Columns[9].ColumnName = "Nome";
                    DtTableBuffer.Columns[10].ColumnName = "Última Leitura";
                    DtTableBuffer.Columns[11].ColumnName = "Data 1";
                    DtTableBuffer.Columns[12].ColumnName = "Leitura 1";
                    DtTableBuffer.Columns[13].ColumnName = "Data 2";
                    DtTableBuffer.Columns[14].ColumnName = "Leitura 2";

                    // Copia os conteudos de cada tabela no DtSet para a tabela buffer
                    for (int t = 0; t < DtSet.Tables.Count; t++)
                    {
                        foreach (DataRow row in DtSet.Tables[t].Rows)
                        {
                            DtTableBuffer.Rows.Add(row.ItemArray);
                        }
                    }

                    DataSet DtSetFinal = new DataSet();
                    DtSetFinal.Tables.Add(DtTableBuffer);
                    DtSetFinal.AcceptChanges();
                    //System.Windows.Forms.MessageBox.Show("DtSetFinal tables: " + DtSetFinal.Tables.Count + "\nDtSetFinal Rows: " + DtSetFinal.Tables[0].Rows.Count + "\nDtSet tables: " + DtSet.Tables.Count);


                    // *** Validação das linhas de acordo com critérios ***
                    // DataTable.Delete() não apaga linha no momento mas marca para ser apagada. Só quando se chama DataTable.AcceptChanges() é que todas as linhas marcadas são removidas
                    DataTable DtTable = DtSetFinal.Tables[0];
                    string processar, benef, TRH, taxaPen;

                    for (int lin = 0; lin < DtTable.Rows.Count; lin++)
                    {
                        // VALIDAÇÃO DE CADA LINHA
                        processar = DtTable.Rows[lin].Field<string>("Processar");
                        TRH = DtTable.Rows[lin].Field<string>("TRH");
                        taxaPen = DtTable.Rows[lin].Field<string>("Tx Penalizadora");
                        
                        List<string> valoresValidos = new List<string> { "S", "N" };

                        if (processar == "N")
                        { 
                            DtTable.Rows[lin].Delete();
                            continue; 
                        }
                        else if (!valoresValidos.Contains(processar) || !valoresValidos.Contains(TRH) || !valoresValidos.Contains(taxaPen))
                        { 
                            errosExcel.Add("Valor numa das colunas 'Processar', 'TRH', ou 'Taxa Penalizadora' no contador " + DtTable.Rows[lin].Field<string>("Nº Contador") + " não é valido.");
                            continue; 
                        }

                        if (DtTable.Rows[lin].Field<string>("Nº Contador") == null
                            || DtTable.Rows[lin].Field<double?>("Última Leitura") == null
                            || DtTable.Rows[lin].Field<DateTime?>("Data 1") == null
                            || DtTable.Rows[lin].Field<double?>("Leitura 1") == null
                            || !ValidacaoConsumos(DtTable, lin)
                            || DtTable.Rows[lin].Field<double?>("Benef") == null
                            || DtTable.Rows[lin].Field<double?>("Área") <= 0)
                        {
                            errosExcel.Add("Verificar contador -> " + DtTable.Rows[lin].Field<string>("Nº Contador") + "  do Benef " + DtTable.Rows[lin].Field<double?>("Benef").ToString());
                            DtTable.Rows[lin].Delete();
                            continue;
                        }
                        benef = DtTable.Rows[lin].Field<double?>("Benef").ToString().PadLeft(5, '0');
                    }
                    DtTable.AcceptChanges();
                        
                    // Uso de tabela de buffer para dar sort às linhas por Benef. Necessário para faturar vários contadores por fatura mesmo que não estejam logo na linha abaixo.
                    DataTable sortBuffer = new DataTable();
                    DtTable.DefaultView.Sort = "Benef ASC";
                    sortBuffer = DtTable.DefaultView.ToTable();

                    DtTable.Rows.Clear();
                    foreach (DataRow row in sortBuffer.Rows)
                    {
                        DtTable.Rows.Add(row.ItemArray);
                    }
                    DtTable.AcceptChanges();

                    Ligacao.Close();
                    return DtSetFinal;
                }
                catch (IOException e) { System.Windows.Forms.MessageBox.Show("Não foi possível estabelecer ligação ao ficheiro!\n\n"); Ligacao.Close(); return DtSet; }
                catch (Exception e) { System.Windows.Forms.MessageBox.Show("Erro no processamento das linhas. \n\n" + e.ToString()); Ligacao.Close(); return DtSet; }

            return DtSet;
            }
        }

        public bool ValidacaoConsumos(DataTable DtTable, int lin)
        {
            double? leitura1, leitura2, ultimaLeitura;

            leitura1 = DtTable.Rows[lin].Field<double?>("Leitura 1");
            leitura2 = DtTable.Rows[lin].Field<double?>("Leitura 2");
            ultimaLeitura = DtTable.Rows[lin].Field<double?>("Última Leitura");
            
            if (ultimaLeitura == null || leitura1 == null) { return false; }
            if (leitura2 == null) {
                if (leitura1 - ultimaLeitura < 0) { return false; }
            } else {
                if (leitura2 - ultimaLeitura < 0) { return false; }
            }
            return true;
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