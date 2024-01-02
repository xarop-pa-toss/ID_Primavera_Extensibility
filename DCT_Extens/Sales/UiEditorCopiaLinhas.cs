using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;

namespace DCT_Extens
{
    public class UiEditorCopiaLinhas : EditorCopiaLinhas
    {
        public override void DepoisDeCopiar(ExtensibilityEventArgs e)
        {
            base.DepoisDeCopiar(e);

            // Ver definição para mais informação (Alt+F12 na linha abaixo)
            GS1_Geral.LinhasCopiadas = true;
        }
    }
}
