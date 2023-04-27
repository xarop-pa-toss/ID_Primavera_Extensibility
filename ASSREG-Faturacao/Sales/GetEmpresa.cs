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
using StdPlatBE100;


namespace ASRLB_ImportacaoFatura.Sales
{
    public class GetEmpresa : Plataforma
    {
        //private ErpBS BSO = new ErpBS();
        //private StdPlatBS PSO = new StdPlatBS();

        public GetEmpresa(formImportarTxt_WF formImportTxt)
        {
            formImportTxt.codEmpresa = this.Aplicacao.Empresa.CodEmp;
        }
    }
}
