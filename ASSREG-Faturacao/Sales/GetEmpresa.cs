using Primavera.Extensibility.Platform.Services;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdBE100;
using ErpBS100;
using StdPlatBS100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;

namespace ASRLB_ImportacaoFatura.Sales
{
    public class GetEmpresa : Plataforma
    {
        public static string codEmpresa { get; private set; }

        public override void DepoisDeAbrirEmpresa(ExtensibilityEventArgs e)
        {
            base.DepoisDeAbrirEmpresa(e);
            codEmpresa = this.Aplicacao.Empresa.CodEmp;
        }
    }
}
