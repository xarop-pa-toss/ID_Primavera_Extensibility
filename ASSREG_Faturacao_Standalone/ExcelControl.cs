using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks; using System.IO;
using OleDb = System.Data.OleDb; using DataSet = System.Data.DataSet; using DataTable = System.Data.DataTable;

//using Microsoft.Office.Interop.Excel; using _Excel = Microsoft.Office.Interop.Excel; using System.Runtime.InteropServices;

namespace ASSREG_Faturacao_ExcelStandalone
{
    public class ExcelControl
    {
        string conString;
        //_Application ExcelApp;
        //Workbook wb;
        //Worksheet ws;
        public ExcelControl(string path)
        {
            // Cria connect string para uma cópia do ficheiro original. Abre ligação entre Primavera e Excel por OLEDB. Devolve erro e termina se ficheiro não for valido.
            path = ExcelCopia(path);
            if (path == null) return;
            conString = ConnectString(path);
            if (conString == null) return;
        }
        

        // Encontra o texto após a última / (nome do ficheiro Excel) e cria a cópia a ser usada pelo resto do programa. Usado no constructor.
        public string ExcelCopia(string origem)
        {
            try
            {
                string destino = Path.GetDirectoryName(origem) + "AssReg_Leituras_Copia.xlsx";
                File.Copy(origem, destino, true);
                return destino;
            }
            catch (IOException e) { System.Windows.Forms.MessageBox.Show(e.Message); return null; }
            catch (UnauthorizedAccessException e) { System.Windows.Forms.MessageBox.Show(e.Message);return null; }
        }

        // Abre ligação e preenche DataSet com query ao ficheiro Excel. Fecha ligação no final.
        public DataSet CarregarSheet(string folha)
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

                    // Primeira coluna com numeração das linhas
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
        }//

        /* 
         **** IMPLEMENTAÇÃO COM INTEROP ****
        ExcelApp = new _Excel.Application();
        this.path = path;
        this.wb = ExcelApp.Workbooks.Open(path, 0, false, 5, "", "", true, _Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
        this.ws = ExcelApp.Worksheets[sheet];
        ultLinha = ws.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row;
        */

        // Usado no Constructor.
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

