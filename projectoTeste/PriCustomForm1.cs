using ErpBS100;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using PRISDK100;
using StdPlatBS100;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectoTeste.Helpers;

namespace projectoTeste
{
    public partial class PriCustomForm1 : CustomForm
    {
        private ErpBS _BSO { get; set; }
        private StdPlatBS _PSO { get; set; }
        private clsSDKContexto _SDKContexto { get; set; }
        private HelperFunctions _Helpers { get; set; }
        private string loadError = null;

        public PriCustomForm1()
        {
            _BSO = PriMotores.Motor;
            _PSO = PriMotores.Plataforma;
            _SDKContexto = PriMotores.PriSDKContexto;
            _Helpers = new HelperFunctions();

            InitializeComponent();
        }

        private void PriCustomForm1_Load(object sender, EventArgs e)
        {
            PSO.MensagensDialogos.MostraMensagem(StdBSTipos.TipoMsg.PRI_SimplesOk, "we hawt");
        }
    }
}
