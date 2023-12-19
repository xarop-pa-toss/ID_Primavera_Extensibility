using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCT_Extens.StockQuebras
{
    public partial class FormStockQuebras : CustomForm
    {
        public bool GetCheckBox_RepetirMotivo
        {
            get { return chkBox_RepetirMotivo.Checked; }
        }
        public string GetTxtBox_MotivoQuebra
        {
            get { return txtBox_MotivoQuebra.Text; }
        }
        public string GetCmbBox_Operador
        {
            get { return cmbBox_Operador.Text; }
        }
        private List<string> _listaOperadores;
        private DataRow _rowSerie;


        public FormStockQuebras(List<string> listaOperadores, DataRow rowSerie)
        {
            _listaOperadores = listaOperadores;
            _rowSerie = rowSerie;

            InitializeComponent();
        }

        private void FormStockQuebras_Load(object sender, EventArgs e)
        {
            cmbBox_Operador.Items.Clear();
            txtBox_MotivoQuebra.Text = string.Empty;

            // Combobox de operador ou Textbox de motivo têm estados diferentes de acordo com as queries às TDU
            // PedeOperador e PedeMotivo activam ou desactivam os controlos mas os conteudos da Combobox muda de acordo com o armazém documento -> armazém do operador
            if (!(bool)_rowSerie["CDU_PedeOperador"]) {
                cmbBox_Operador.Enabled = false;
            } else {
                foreach (string op in _listaOperadores) { cmbBox_Operador.Items.Add(op); }
            }

            if (!(bool)_rowSerie["CDU_PedeMotivo"]) {
                txtBox_MotivoQuebra.Enabled = false;
            }

        }
    }
}
