using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCT_Extens.StockQuebras
{
    public partial class FormStockQuebras : CustomForm
    {
        public FormStockQuebras()
        {
            InitializeComponent();
        }

        public bool GetCheckBox_RepetirMotivo
        {
            get { return chkBox_RepetirMotivo.Checked; }
        }

        private void FormStockQuebras_Load(object sender, EventArgs e)
        {

        }
    }
}
