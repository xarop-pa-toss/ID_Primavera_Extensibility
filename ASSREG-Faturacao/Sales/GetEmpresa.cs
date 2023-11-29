using Primavera.Extensibility.Platform.Services;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;

namespace ASRLB_ImportacaoFatura.Sales
{
    public class GetEmpresa : Plataforma
    {
        public static string codEmpresa { get; private set; }

        public GetEmpresa()
        {
        }

        public override void DepoisDeAbrirEmpresa(ExtensibilityEventArgs e)
        {
            base.DepoisDeAbrirEmpresa(e);
            codEmpresa = this.Aplicacao.Empresa.CodEmp;
        }
    }
}
