using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PP_Extens
{
    public partial class InputForm : CustomForm
    {
        private string _descricao, _txtBoxValorDefault;

        public InputForm(string descricao, string txtBoxValorDefault)
        {
            _descricao = descricao;
            _txtBoxValorDefault = txtBoxValorDefault;
            InitializeComponent();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            lbl_Descricao.Text = _descricao;
            txtBox_Resposta.Text = _txtBoxValorDefault;
            // Ajustar as altura do form à altura da descrição
            this.Height += lbl_Descricao.Height;
        }
        #region Botões
        private void btn_OK_Click(object sender, EventArgs e)
        {
            OnOKButtonClick();
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            OnCancelButtonClick();
        }

        private string OnOKButtonClick()
        {
            return txtBox_Resposta.Text;
        }

        private string OnCancelButtonClick()
        {
            return null;
        }
        #endregion
    }
}
