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
        private string _descricao;

        public InputForm(string descricao)
        {
            _descricao = descricao;
            InitializeComponent();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            // Ajustar as altura do form à altura da descrição
            this.Height += lbl_Descricao.Height;
        }
    }
}
