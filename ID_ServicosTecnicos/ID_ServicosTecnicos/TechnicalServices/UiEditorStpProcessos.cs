using Primavera.Extensibility.TechnicalServices.Editors; using Primavera.Extensibility.BusinessEntities;
using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks; 
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StpBE100; using StdBE100; using BasBE100;


namespace ID_ServicosTecnicos.TechnicalServices
{
    public class UiEditorStpProcessos : EditorStpProcessos
    {
        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            if (KeyCode == Convert.ToInt32(ConsoleKey.F3))
            {
                Processo.TipoDoc = "PRC";
            }
        }
    }
}
