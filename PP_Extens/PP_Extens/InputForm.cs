using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using StdPlatBS100;
using ErpBS100;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PP_Extens
{
    public partial class InputForm : CustomForm
    {
        private string _strDescricao, _strValorDefeito, _resultado;
        public string Resultado { get { return _resultado; } }
        private static StdBSInterfPub _PSO;
        private static ErpBS _BSO;

        public InputForm(string strDescricao, string strValorDefeito, StdBSInterfPub PSO, ErpBS BSO)
        {
            _strDescricao = strDescricao;
            _strValorDefeito = strValorDefeito;
            _PSO = PSO;
            _BSO = BSO;


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
        private void btn_OK_Click(object sender, EventArgs e)
        {
            _resultado = txtBox_Resposta.Text;
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
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
