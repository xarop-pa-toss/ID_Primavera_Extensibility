using Primavera.Extensibility.CustomForm;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HelperFunctionsPrimavera10
{
    public partial class InputForm : CustomForm
    {
        public InputForm()
        {
            InitializeComponent();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            this.Text = InputFormServico.Titulo;
            lbl_Descricao.Text = InputFormServico.Descricao;
            txtBox_Resposta.Text = InputFormServico.ValorDefeito;

            // Ajustar as altura do form à altura da descrição
            this.SuspendLayout();
            this.Size = new Size(this.Width, this.Height + lbl_Descricao.Height + 30);
            this.ResumeLayout();
        }

        private void InputForm_Shown(object sender, EventArgs e)
        {
        }

        #region Botões
        private void btn_OK_Click(object sender, EventArgs e)
        {
            InputFormServico.Resposta = txtBox_Resposta.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion

        private void InputForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (DialogResult == null)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            
        }

        // Quando textbox está em foco, carregar no Enter activa o botão OK
        private void txtBox_Resposta_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btn_OK.PerformClick();
                e.Handled = true;
            }
        }
    }
}
