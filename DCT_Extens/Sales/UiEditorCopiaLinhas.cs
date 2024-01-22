using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;

namespace DCT_Extens
{
    public class UiEditorCopiaLinhas : EditorCopiaLinhas
    {
        public override void DepoisDeCopiar(ExtensibilityEventArgs e)
        {
            base.DepoisDeCopiar(e);

            // Ver defini��o para mais informa��o (Alt+F12 na linha abaixo)
            GS1_Geral.LinhasCopiadas = true;
        }
    }
}
