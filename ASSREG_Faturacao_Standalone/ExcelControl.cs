using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks; 
using OleDb = System.Data.OleDb; using DataSet = System.Data.DataSet; using DataTable = System.Data.DataTable;

//using Microsoft.Office.Interop.Excel; using _Excel = Microsoft.Office.Interop.Excel; using System.Runtime.InteropServices;


namespace ASSREG_Faturacao_ExcelStandalone
{
    public class ExcelControl
    {
        int linhasTotal;
        string path; string conString;
        //_Application ExcelApp;
        //Workbook wb;
        //Worksheet ws;
        public ExcelControl(string path)
        {
            // Cria connect string de acordo com a versão do ficheiro. Abre ligação entre Primavera e Excel por OLEDB. Mostra erro no ecrã se ficheiro não for valido.
            conString = ConnectString(path);
            if (conString == null) { return; } //
        }

        // Abre ligação e preenche DataSet com query ao ficheiro Excel. Fecha ligação no final.
        public DataTable CarregarSheet(int sheet)
        {
            using (OleDb.OleDbConnection Ligacao = new OleDb.OleDbConnection(conString))
            {
                Ligacao.Open();
                // Conta linhas preenchidas
                OleDb.OleDbCommand cmd = new OleDb.OleDbCommand("SELECT Count(*) FROM [Sheet" + sheet + "$]", Ligacao);
                linhasTotal = (int)cmd.ExecuteScalar() - 5;

                // Datasets a preencher com diferentes colunas. Serão merged para uma única DataTable
                DataSet DtSet = new DataSet();
                DataSet DtSetBuffer = new DataSet();
                DataTable DtTable = new DataTable();

                // Queries a executar com o Adapter
                List<string> queries = new List<string>();
                queries.Add("SELECT * FROM [Sheet" + sheet + "$C6:G" + linhasTotal);
                queries.Add("SELECT * FROM [Sheet" + sheet + "$I6:J" + linhasTotal);
                queries.Add("SELECT * FROM [Sheet" + sheet + "$I6:J" + linhasTotal);
                queries.Add("SELECT * FROM [Sheet" + sheet + "$L6:M" + linhasTotal);
                queries.Add("SELECT * FROM [Sheet" + sheet + "$O6:R" + linhasTotal);

                // Execução de queries ao Excel.
                // Inicialização do Adapter (query engine) inclui a primeira query que vai directamente para o DataSet principal. As seguintes usam um buffer para permitir merging.
                try
                {
                    OleDb.OleDbDataAdapter Adapter = new OleDb.OleDbDataAdapter("SELECT * FROM [Sheet" + sheet + "$C6:G" + linhasTotal, Ligacao);
                    Adapter.Fill(DtSet, "Tabela 0");
                    int counter = 1;

                    foreach (string q in queries)
                    {
                        Adapter.SelectCommand.CommandText = q;
                        Adapter.Fill(DtSetBuffer, "Tabela " + counter);
                        DtSet.Tables[0].Merge(DtSetBuffer.Tables[0]);
                        DtSetBuffer.Clear();
                        counter++;
                    }
                    // DataTable a retornar
                    DtTable = DtSet.Tables[0];
                    // Fechar objectos 
                    DtSetBuffer.Dispose(); Adapter.Dispose(); Ligacao.Close();

                    // Cabeçalhos das colunas
                    DtTable.Columns[0].ColumnName = "Prédio";
                    DtTable.Columns[1].ColumnName = "Nº Contador";
                    DtTable.Columns[2].ColumnName = "Benef.";
                    DtTable.Columns[3].ColumnName = "Nome";
                    DtTable.Columns[4].ColumnName = "Última Leitura";
                    DtTable.Columns[5].ColumnName = "Ligado";
                    DtTable.Columns[6].ColumnName = "Data 1";
                    DtTable.Columns[7].ColumnName = "Leitura 1";
                    DtTable.Columns[8].ColumnName = "Data 2";
                    DtTable.Columns[9].ColumnName = "Leitura 2";
                    DtTable.Columns[10].ColumnName = "Data 3";
                    DtTable.Columns[11].ColumnName = "Leitura 3";

                    return DtTable;
                    
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Não foi possível estabelecer ligação! Erro: " + e); return DtTable;
                    //PSO.MensagensDialogos.MostraErro("Não foi possível estabelecer ligação! Erro: " + e); return DtTable;
                }
            }
        }//

        /* 
         **** IMPLEMENTAÇÃO COM INTEROP ****
        ExcelApp = new _Excel.Application();
        this.path = path;
        this.wb = ExcelApp.Workbooks.Open(path, 0, false, 5, "", "", true, _Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
        this.ws = ExcelApp.Worksheets[sheet];
        ultLinha = ws.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row;
        */

        private string ConnectString(string path)
        {
            // Provider = OLEDB Provider para o ficheiro de Excel. Jet.OLEDB.4.0 para ficheiros .xls e ACE.OLEDB.12.0 para ficheiros .xlsx
            // Data Source = caminho do ficheiro no sistema
            // Extended Properties = versão da driver do Excel e HDR=Sim/Nao se a primeira linha conter os cabeçalhos (tornar-se-ão nomes das colunas no DataSet)
            //if (path.Substring(-4, 4) == "*.xls")
            //{
               //return          @"Provider=Microsoft.Jet.OLEDB.4.0;"
               //                + "Data Source = '" + path + "'"
               //                + ";Extended Properties=\"Excel 8.0;HDR=NO;\"";
            //}
            //if (path.Substring(-5, 5) == "*.xlsx")
            //{
                return conString = @"Provider=Microsoft.ACE.OLEDB.12.0;"
                                + "Data Source='" + path + "'"
                                + ";Extended Properties=\"Excel 12.0;HDR=NO;\"";
            //}
            //System.Windows.Forms.MessageBox.Show("Ficheiro não válido. Deve ser ficheiro Excel (.xls ou .xlsx.)"); return null;
            //PSO.MensagensDialogos.MostraErro("Ficheiro não válido. Deve ser ficheiro Excel (.xls ou .xlsx.)"); return null;
        }


        /* **** IMPLEMENTAÇÃO COM INTEROP ****
         * 
         * 
         * public void AbrirExcel()
        {
            DataSet DtSet = new DataSet();
            OleDb.OleDbDataAdapter Comando;
        }
        public string LerCelula(int linha, int col)
        {
            linha++; col++;
            string conteudo = ws.Cells[linha, col].Value2 != null ? ws.Cells[linha, col].Value2 : "";
            return conteudo.ToString();
        }


        public void EscreverCelula(int linha, int col, string valor)
        {
            linha++; col++;
            ws.Cells[linha, col].Value2 = valor.ToString();
        }

        public int UltLinha() { return ultLinha; }

        
        // Termina os processos COM do Excel com segurança e activa o Garbage Collector para prevenir má gestão da RAM.
        // Deve ser chamado sempre que um Workbook deixe de ser necessário. Grava antes de fechar.
        public void Terminar(object obj)
        {
            object semValor = System.Reflection.Missing.Value;

            if (obj.GetType() == wb.GetType()) { wb.Close(true, semValor, semValor); }
            else if (obj.GetType() == ExcelApp.GetType()) { ExcelApp.Quit(); }

            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception e)
            {
                PSO.MensagensDialogos.MostraErro("Não foi possivel terminar o objecto: " + e.ToString());
                obj = null;
            }
            finally { GC.Collect(); }
        } */
    }
}

