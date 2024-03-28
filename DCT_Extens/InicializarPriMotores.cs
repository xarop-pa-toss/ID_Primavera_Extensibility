using HelpersPrimavera10;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Platform.Services;
using System.Data;


namespace DCT_Extens
{
    public class InicializarPriMotores : Plataforma
    {

        public override void DepoisDeCriarMenus(ExtensibilityEventArgs e)
        {
            base.DepoisDeCriarMenus(e);

            Secrets secrets = new Secrets();
            secrets.BSO = this.BSO;
            secrets.PSO = this.PSO;

            DataTable instanciaTable = BSO.ConsultaDataTable("SELECT @@SERVERNAME AS ServerName;");
            secrets.BDServidorInstancia = instanciaTable.Rows[0][0].ToString();

            // HelperFunctions inicializa PriMotores no seu construtor
            new HelperFunctions(secrets);
        }
    }
}
