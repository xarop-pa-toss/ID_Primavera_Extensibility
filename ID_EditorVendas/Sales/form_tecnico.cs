using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using IVndBS100; using IBasBS100; using VndBE100;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_EditorVendas.Sales
{
    public partial class form_tecnico : CustomForm
    {
        public List<string> tecnicos { get; set; }
        public VndBEDocumentoVenda docVenda{ get; set; }
        public string tecnicoEscolhido { get; set; }

        public form_tecnico()
        {
            InitializeComponent();
        }

        private void form_tecnico_Load(object sender, EventArgs e)
        {
            foreach (var i in tecnicos) { cbox_tecnicos.Items.Add(i); }
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            tecnicoEscolhido = cbox_tecnicos.Text;
        }
    }
}
