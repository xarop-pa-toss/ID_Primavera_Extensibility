using Primavera.Extensibility.Platform.Services;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using HelperFunctionsPrimavera10;


namespace DCT_Extens
{
    public class InicializarPriMotores : Plataforma
    {
        public override void DepoisDeCriarMenus(ExtensibilityEventArgs e)
        {
            base.DepoisDeCriarMenus(e);

            Secrets secrets = new Secrets();
            secrets.SetBSO(this.BSO);
            secrets.SetPSO(this.PSO);

            // HelperFunctions inicializa PriMotores no seu construtor
            HelperFunctions _Helpers = new HelperFunctions(secrets);
        }
    }
}
