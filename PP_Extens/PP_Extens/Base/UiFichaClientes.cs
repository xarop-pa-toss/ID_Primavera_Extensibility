using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PP_PPCS.Base
{
    public class UiFichaClientes : FichaClientes
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (string.IsNullOrEmpty(Cliente.Morada)
                || string.IsNullOrEmpty(Cliente.Localidade)
                || string.IsNullOrEmpty(Cliente.Distrito))
            {
                PSO.MensagensDialogos.MostraErro("O cliente necessita de ter definida a Morada, Localidade e Distrito", StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);

                Cancel = true;
                return;
            }
        }
    }
}
