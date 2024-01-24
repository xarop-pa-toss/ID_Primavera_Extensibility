using Primavera.Extensibility.CustomForm;
using StdPlatBS100;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HelperFunctionsPrimavera10
{
    public partial class InputForm : Form
    {
        private bool _permiteNull = false;

        public string Resposta { get; private set; }


        public InputForm(string titulo, string descricao, string valorDefeito, bool permiteNull = true)
        {
            InitializeComponent();

            this.Text = titulo;
            lbl_Descricao.Text = descricao;
            txtBox_Resposta.Text = valorDefeito;
            _permiteNull = permiteNull;
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            // Ajustar as altura do form à altura da descrição
            this.SuspendLayout();
            this.Size = new Size(this.Width, this.Height + lbl_Descricao.Height + 30);
            this.ResumeLayout();
        }

        #region Botões
        private void btn_OK_Click(object sender, EventArgs e)
        {
            // Se resposta for nula num form que não o permite, abre uma caixa a perguntar se quer tentar novamente ou cancelar.
            // Se resposta não for nula, devolve a resposta.
            if (string.IsNullOrEmpty(txtBox_Resposta.Text) && _permiteNull == false)
            {
                // Se OK, pode tentar responder outra vez. Se Cancel, operação abortada e linha apagada.
                DialogResult resposta = MessageBox.Show(
                    "É necessário um valor para poder avançar." + Environment.NewLine + "Prima OK para tentar outra vez ou Cancelar para abortar a operação.",
                    "Valor vazio",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                if (resposta == DialogResult.Cancel) { btn_Cancelar.PerformClick(); }
                else { InputForm_Load(sender, e); }
            } 
            else 
            {
                Resposta = txtBox_Resposta.Text;
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            Resposta = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        private void InputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        // Quando textbox está em foco, carregar no Enter activa o botão OK
        private void txtBox_Resposta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btn_OK.PerformClick();
                e.Handled = true;
            }
        }
    }
}
