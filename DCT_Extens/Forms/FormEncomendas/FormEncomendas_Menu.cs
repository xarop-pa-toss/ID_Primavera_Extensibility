using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT_Extens.Forms.FormEncomendas
{
    public partial class FormEncomendas_Menu : CustomForm
    {
        public FormEncomendas_Menu()
        {
            InitializeComponent();
        }

        private void btn_AbrirFormVendas_Click(object sender, EventArgs e)
        {
            FormEncomendas_Vendas form = new FormEncomendas_Vendas();
            form.Show();
        }

        private void btn_AbrirFormCompras_Click(object sender, EventArgs e)
        {
            FormEncomendas_Compras  form = new FormEncomendas_Compras();
            form.Show();
        }
    }
}
