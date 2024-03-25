using HelpersPrimavera10;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Platform.Services;


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
            HelperFunctions Helpers = new HelperFunctions(secrets);

            System.Data.DataTable instanciaTable = Helpers.GetDataTableDeSQL("SELECT @@SERVERNAME AS ServerName;");
            secrets.SetBDServidorInstancia(instanciaTable.Rows[0][0].ToString());
        }
    }
}
