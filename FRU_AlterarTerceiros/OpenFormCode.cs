using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRU_AlterarTerceiros
{
    public class OpenFormCode : CustomCode
    {
        public void Abrir_formFaturasExploracao_WF()
        {
            FormAlterarTerceiros_WF form = new FormAlterarTerceiros_WF();
            form.ShowDialog();
            PSO.UI.AdicionaFormMDI(form);
        }
    }
}
