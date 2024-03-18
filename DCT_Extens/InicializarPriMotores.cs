using HelpersPrimavera10;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Platform.Services;


namespace DCT_Extens
{
    public class InicializarPriMotores : Plataforma
    {

        public override void AntesDeCriarMenus(ExtensibilityEventArgs e)
        {
            base.AntesDeCriarMenus(e);

            Secrets secrets = new Secrets();
            secrets.SetBSO(this.BSO);
            secrets.SetPSO(this.PSO);

            string instancia = BSO.Consulta("SELECT @@SERVERNAME AS ServerName;").DaValor<string>(0);
            secrets.SetBDServidorInstancia(instancia);

            // HelperFunctions inicializa PriMotores no seu construtor
            HelperFunctions _Helpers = new HelperFunctions(secrets);
        }
    }
}
