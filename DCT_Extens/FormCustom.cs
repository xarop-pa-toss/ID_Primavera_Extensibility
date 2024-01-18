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
        private StdBSConfApl confApl;
        private StdBE100.StdBETransaccao transaccao;
        private StdPlatBS Plat = new StdPlatBS();
        private ErpBS Motor = new ErpBS();

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
                transaccao = new StdBE100.StdBETransaccao();
                confApl = new StdBSConfApl();

                confApl.Instancia = "Default";
                confApl.AbvtApl = "ERP";
                confApl.Utilizador = "id";
                confApl.PwdUtilizador = "*Pelicano";
                confApl.LicVersaoMinima = "10.00";

                //try
                //{
                //Plat.AbrePlataformaEmpresa(
                //    "DECANTE",
                //    transaccao,
                //    confApl,
                //    StdBE100.StdBETipos.EnumTipoPlataforma.tpProfissional);
                //}
                //catch (Exception ex)
                //{
                //    System.Windows.Forms.MessageBox.Show("Não foi possivel abrir Plataforma. Credenciais inválidas.\nPor favor contacte a Infodinâmica.", "Erro PriMotores");
                //    throw (ex);
                //}

                //if (Platform.Inicializada)
                //{
                Motor.AbreEmpresaTrabalho(
                    StdBE100.StdBETipos.EnumTipoPlataforma.tpProfissional,
                    "DECANTE",
                    "id",
                    "*Pelicano");

                sdkContexto.Inicializa(Motor, "ERP");
            }
        }
    }
}
