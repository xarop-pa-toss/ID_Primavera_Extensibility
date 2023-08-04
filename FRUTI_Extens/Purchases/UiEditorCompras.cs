using Primavera.Extensibility.Purchases.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdBE100;
using StdPlatBS100;
using ErpBS100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;

namespace FRUTI_Extens.Purchases
{
    public class UiEditorCompras : EditorCompras
    {
        private int _indArray = 0;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            List<string> tipoDocList = new List<string> { "VFA", "VFD", "VGR", "VFF", "CVS" };
            string strErro = "";

            if (PSO.MensagensDialogos.MostraPerguntaSimples("Confirma actualização de preços?") && tipoDocList.Contains(this.DocumentoCompra.Tipodoc)) {                
                
                if (!BSO.Compras.Documentos.ValidaActualizacao(this.DocumentoCompra, BSO.Compras.TabCompras.Edita(DocumentoCompra.Tipodoc),ref strErro)) {
                    PSO.MensagensDialogos.MostraAviso("Não foi possível actualizar os valores pretendidos.", StdBSTipos.IconId.PRI_Exclama, strErro);
                    Cancel = true;
                } 
                else {
                    for (int i = 0; i < DocumentoCompra.Linhas.NumItens; i++) {

                    }

                }
            }
            base.AntesDeGravar(ref Cancel, e);

        }

    }
}
