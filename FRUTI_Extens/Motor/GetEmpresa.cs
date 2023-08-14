using Primavera.Extensibility.Platform.Services;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;


namespace FRUTI_Extens.Motor
{
    public class GetEmpresa : Plataforma
    {
        public static string codEmpresa { get; private set; }
        public static string utilizadorActivo { get; private set; }
        public static string utilizadorActivoPassword { get; private set; }

        public override void DepoisDeAbrirEmpresa(ExtensibilityEventArgs e)
        {
            base.DepoisDeAbrirEmpresa(e);
            codEmpresa = this.Aplicacao.Empresa.CodEmp;
            utilizadorActivo = this.Aplicacao.Utilizador.Nome;
            utilizadorActivoPassword = this.Aplicacao.Utilizador.Password;
        }
    }
}