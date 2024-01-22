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
    public partial class FormCustom : CustomForm
    {
        private clsSDKContexto sdkContexto;


        public FormCustom()
        {
            InitializeComponent();
        }

        private void FormCustom_Load(object sender, EventArgs e)
        {
            InicializaContextoSDK();

            f4.Inicializa(sdkContexto);
        }

        private void InicializaContextoSDK()
        {
            if (sdkContexto == null)
            {
                sdkContexto = new clsSDKContexto();
                sdkContexto.Inicializa(BSO, "ERP");
                PSO.InicializaPlataforma(sdkContexto);

                f4.Inicializa(sdkContexto);
            }
        }
    }
}
