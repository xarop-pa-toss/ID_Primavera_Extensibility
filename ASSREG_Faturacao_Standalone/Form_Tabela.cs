using System; using System.Collections.Generic; using System.ComponentModel; using System.Data; using System.Drawing; using System.Linq; using System.Text;using System.Threading.Tasks; using System.Windows.Forms;
using ASSREG_Faturacao_ExcelStandalone;

namespace ASSREG_Faturacao_Standalone
{
    public partial class Form_Tabela : Form
    {
        public Form_Tabela()
        {
            InitializeComponent();
        }

        private void Form_Tabela_Load(object sender, EventArgs e)
        {
            ExcelControl Excel = new ExcelControl(@"C:/Users/VM/source/repos/ID_Primavera_Extensibility/ASSREG-Faturacao/Mapa de contadores.xlsx");
            DataSet DataSource = Excel.CarregarDataSet("CANTAO 1", Excel.conString);

            DtGrid_unfiltered.AutoGenerateColumns = true;
            DtGrid_unfiltered.AutoSize = true;
            DtGrid_unfiltered.ScrollBars = ScrollBars.Both;
            DtGrid_unfiltered.DataSource = DataSource.Tables[0];
            DtGrid_unfiltered.Refresh();

        }
    }
}

