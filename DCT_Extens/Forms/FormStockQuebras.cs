using HelperFunctionsPrimavera10;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DCT_Extens
{
    public partial class FormStockQuebras : CustomForm
    {
        private HelperFunctions _Helpers = new HelperFunctions(new Secrets());

        public bool GetCheckBox_RepetirMotivo { get { return chkBox_RepetirMotivo.Checked; } }
        public string GetTxtBox_MotivoQuebra { get { return txtBox_MotivoQuebra.Text; } }
        public string GetCmbBox_Operador { get { return cmbBox_Operador.Text; } }

        public static bool RepeteMotivo = false;
        private bool _pedeOperador, _pedeMotivo;
        private List<string> _listaOperadores;
        private DataRow _rowSerie;

        public FormStockQuebras()
        {
            InitializeComponent();
        }

        public void SetVariaveis(List<string> listaOperadores, DataRow rowSerie)
        {
            _listaOperadores = listaOperadores;
            _rowSerie = rowSerie;
        }

        private void FormStockQuebras_Load(object sender, EventArgs e)
        {
            cmbBox_Operador.Items.Clear();
            txtBox_MotivoQuebra.Text = string.Empty;

            _pedeOperador = (bool)_rowSerie["CDU_PedeOperador_Operador"];
            _pedeMotivo = (bool)_rowSerie["CDU_PedeOperador_Motivo"];

            // Combobox de operador ou Textbox de motivo têm estados diferentes de acordo com as queries às TDU
            // PedeOperador e PedeMotivo activam ou desactivam os controlos mas os conteudos da Combobox muda de acordo com o armazém documento -> armazém do operador
            if (!_pedeOperador)
            {
                cmbBox_Operador.Enabled = false;
            } else
            {
                foreach (string op in _listaOperadores) { cmbBox_Operador.Items.Add(op); }
            }

            if (!_pedeMotivo)
            {
                txtBox_MotivoQuebra.Enabled = false;
            }
        }

        private void btn_Confirmar_Click(object sender, EventArgs e)
        {
            // Check motivo nulo
            if (string.IsNullOrEmpty(txtBox_MotivoQuebra.Text) && _pedeMotivo)
            {
                bool resposta = PSO.MensagensDialogos.MostraPerguntaSimples("É necessário introduzir um motivo. A linha será apagada. Deseja continuar?");

                if (resposta)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
            } else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
