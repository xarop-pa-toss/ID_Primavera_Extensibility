using ErpBS100;
using StdPlatBS100;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PP_Extens
{
    public partial class InputForm : Form
    {
        private string _strDescricao, _strValorDefeito, _resultado;
        public string Resultado { get { return _resultado; } }
        private static StdBSInterfPub _PSO;
        private static ErpBS _BSO;

        public InputForm()
        {
            //_strDescricao = strDescricao;
            //_strValorDefeito = strValorDefeito;
            //_PSO = PSO;
            //_BSO = BSO;

            InitializeComponent();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            
        }

        private void InputForm_Shown(object sender, EventArgs e)
        {
            lbl_Descricao.Text = _strDescricao;
            txtBox_Resposta.Text = _strValorDefeito;

            // Ajustar as altura do form à altura da descrição
            this.Height += lbl_Descricao.Height;
        }

        #region Botões
        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FDU_InputForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            _resultado = txtBox_Resposta.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        private void InputForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
