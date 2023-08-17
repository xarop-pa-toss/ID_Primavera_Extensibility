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
        public void Abrir_formImportaDocs_WF()
        {
            FormImportaDocs_WF form = new FormImportaDocs_WF();
            form.ShowDialog();
            PSO.UI.AdicionaFormMDI(form);
        }
    }
}

