using Primavera.Extensibility.Internal.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100; using VndBE100; using ErpBS100; using StdBE100; using BasBE100;

namespace DCT_Extens.Internal
{
    public class UiEditorInternos : EditorInternos
    {
        private bool _apagaPai;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            _apagaPai = false;
            
            if (Helpers.HelperFunctions.LinhasCopiadas)
            {

            }


        }
    }
}
