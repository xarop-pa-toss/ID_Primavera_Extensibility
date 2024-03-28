using HelpersPrimavera10;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Platform.Services;
using System.Data;


namespace PP_Qualidade
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

            // Neste projecto, Secrets tem um Enum com o endereço do servidor remoto para quando é preciso manipular a base de dados da PPCS
            Secrets.Ambiente = Secrets.AmbienteEnum.TesteRicardo;

            // HelperFunctions inicializa PriMotores no seu construtor
            new HelperFunctions(secrets);
        }
    }
}