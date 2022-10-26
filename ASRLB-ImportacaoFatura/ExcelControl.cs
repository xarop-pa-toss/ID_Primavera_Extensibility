using Primavera.Extensibility.BusinessEntities; using Primavera.Extensibility.CustomCode;
using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks; using OleDb = System.Data.OleDb; using DataSet = System.Data.DataSet;
using Microsoft.Office.Interop.Excel; using _Excel = Microsoft.Office.Interop.Excel; using System.Runtime.InteropServices;


namespace ASRLB_ImportacaoFatura
{
    public class ExcelControl : CustomCode
    {
        string path;
        int ultLinha;
        //_Application ExcelApp;
        //Workbook wb;
        //Worksheet ws;

        public ExcelControl(string path, int sheet)
        {
            // Cria connect string de acordo com a versão do ficheiro. Abre ligação entre Primavera e Excel por OLEDB. Mostra erro no ecrã se ficheiro não for valido.
            string conString = ConnectString();
            if (conString == "Cancel") { return; } //

            // Abre ligação e preenche DataSet com query ao ficheiro Excel. Fecha ligação no final.
            using (OleDb.OleDbConnection Ligacao = new OleDb.OleDbConnection(conString))
            {
                Ligacao.Open();
                OleDb.OleDbDataAdapter DtAdapter = new OleDb.OleDbDataAdapter("SELECT * FROM [Sheet" + sheet + "$]", Ligacao);
                DataSet DtSet = new DataSet();
                DtAdapter.Fill(DtSet);
                
                DtAdapter.Dispose();
                Ligacao.Close();
            }//
            
            /* 
             **** IMPLEMENTAÇÃO COM INTEROP ****
            ExcelApp = new _Excel.Application();
            this.path = path;
            this.wb = ExcelApp.Workbooks.Open(path, 0, false, 5, "", "", true, _Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            this.ws = ExcelApp.Worksheets[sheet];
            ultLinha = ws.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row;
            */
        }

        private string ConnectString()
        {
            string conString = "";

            // Provider = OLEDB Provider para o ficheiro de Excel. Jet.OLEDB.4.0 para ficheiros .xls e ACE.OLEDB.12.0 para ficheiros .xlsx
            // Data Source = caminho do ficheiro no sistema
            // Extended Properties = versão da driver do Excel e HDR=Sim/Nao se a primeira linha conter os cabeçalhos (tornar-se-ão nomes das colunas no DataSet)
            if (path.Substring(-5, 5) == "*.xls")
            {
                return conString = @"Provider=Microsoft.Jet.OLEDB.4.0;"
                                + "Data Source = '" + path + "'"
                                + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
            }
            if (path.Substring(-5, 5) == "*.xlsx")
            {
                return conString = @"Provider=Microsoft.ACE.OLEDB.12.0;"
                                + "Data Source='" + path + "'"
                                + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            }
            PSO.MensagensDialogos.MostraErro("Ficheiro não válido. Deve ser ficheiro Excel (.xls ou .xlsx.)"); return "Cancel";
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
