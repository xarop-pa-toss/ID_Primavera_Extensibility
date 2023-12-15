using DCT_Extens.Helpers;
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

namespace DCT_Extens
{
    public partial class FormCargaDescarga : CustomForm
    {
        private ErpBS _BSO { get; set; }
        private StdPlatBS _PSO { get; set; }
        private clsSDKContexto _SDKContexto { get; set; }
        private HelperFunctions _Helpers { get; set; }

        public FormCargaDescarga()
        {
            _BSO = PriMotores.Motor;
            _PSO = PriMotores.Plataforma;
            _SDKContexto = PriMotores.PriSDKContexto;
            _Helpers = new HelperFunctions();

            InitializeComponent();
        }

        private void FormCargaDescarga_Load(object sender, EventArgs e)
        {
            

        }

        private void FormCargaDescarga_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {

        }
    }
}
