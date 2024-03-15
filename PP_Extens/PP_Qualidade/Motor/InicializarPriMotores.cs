using HelpersPrimavera10;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Platform.Services;


namespace PP_Qualidade
{
    public class InicializarPriMotores : Plataforma
    {
        public override void DepoisDeCriarMenus(ExtensibilityEventArgs e)
        {
            base.DepoisDeCriarMenus(e);

            Secrets secrets = new Secrets();
            secrets.SetBSO(this.BSO);
            secrets.SetPSO(this.PSO);

            Secrets.Ambiente = Secrets.AmbienteEnum.TesteRicardo;

            // HelperFunctions inicializa PriMotores no seu construtor
            HelperFunctions _Helpers = new HelperFunctions(secrets);
        }
    }
}