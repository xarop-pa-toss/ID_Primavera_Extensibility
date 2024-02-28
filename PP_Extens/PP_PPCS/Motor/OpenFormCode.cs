using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_PPCS
{
    public class PriCustomCode1 : CustomCode
    {
        public void Abrir_formImportaDocs()
        {
            FormImportaDocs formImportaDocs = new FormImportaDocs();
            FormQueryStatus_WF formQueryStatus = new FormQueryStatus_WF();

            formImportaDocs.ShowDialog();

            //PSO.UI.AdicionaFormMDI(formImportaDocs);
            PSO.UI.AdicionaFormMDI(formQueryStatus);
        }
    }
}

