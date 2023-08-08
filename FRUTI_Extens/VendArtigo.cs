using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRISDK100;

namespace FRUTI_Extens
{
    public partial class VendArtigo : Form
    {
        string _relatorio, _rSel, _gSel, _titulo, _dataInicial, _dataFinal, _detalhe;
        public clsSDKContexto _sdkContexto;
        public VendArtigo()
        {
            InitializeComponent();
        }

        private void VendArtigo_Load(object sender, EventArgs e)
        {
            if (_sdkContexto == null) {
                _sdkContexto = new clsSDKContexto();
                //Inicializaçao do contexto SDK a partir do objeto BSO e respetivo módulo.
                _sdkContexto.InicializaPlataforma(_pso);
                _sdkContexto.Inicializa(BSO, "ERP");
                //Inicialização da plataforma no contexto e verificação de assinatura digital.
                PSO.InicializaPlataforma(_sdkContexto);
            }
            SdkPrimavera.InicializaContexto(BSO, PSO);

            f4_TipoTerceiro.Inicializa(SdkPrimavera.ContextoSDK);
            f4_TipoDoc.Inicializa(SdkPrimavera.ContextoSDK);
            prigrelha_Docs.Inicializa(SdkPrimavera.ContextoSDK);
        }
    }
}
