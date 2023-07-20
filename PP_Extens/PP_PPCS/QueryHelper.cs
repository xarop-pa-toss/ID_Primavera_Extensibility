using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;

namespace PP_PPCS
{
    public class QueryHelper : EditorVendas
    {
        public override void AntesDeAnular(ref string Motivo, ref string Observacoes, ref bool Cancel, ExtensibilityEventArgs e)
        {
        }
    }   
}
