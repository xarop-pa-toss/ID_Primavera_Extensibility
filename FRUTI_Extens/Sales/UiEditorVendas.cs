using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdBE100;
using StdPlatBS100;
using ErpBS100;
using BasBE100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;


namespace FRUTI_Extens.Sales
{
    public class UiEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            if (DocumentoVenda.TotalDocumento == 0) {
                PSO.MensagensDialogos.MostraAviso("Documento com valor 0 (Zero)", StdBSTipos.IconId.PRI_Informativo);
                Cancel = true;
            }

            base.AntesDeGravar(ref Cancel, e);
        }
    }
}
