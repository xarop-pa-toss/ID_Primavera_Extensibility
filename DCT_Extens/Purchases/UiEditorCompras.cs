using Primavera.Extensibility.Purchases.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100;
using CmpBE100;
using ErpBS100;
using BasBE100;


namespace DCT_Extens.Purchases
{
    public class UiEditorCompras : EditorCompras
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (DocumentoCompra.Entidade != "44" 
                && new List<string> { "ECF", "ECL", "ECP" }.Contains(DocumentoCompra.Tipodoc)
                && DocumentoCompra.Linhas.NumItens > 0)
            {
                BasBEArtigo artigo = new BasBEArtigo();
                foreach (CmpBELinhaDocumentoCompra linha in DocumentoCompra.Linhas)
                {
                    artigo = BSO.Base.Artigos.Edita(linha.Artigo);

                    if (linha.TipoLinha.Equals(10) && (bool)artigo.CamposUtil["CDU_ArtBLOQC"].Valor) { Cancel = true; }
                }
            }
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            BasBEArtigo artigo = BSO.Base.Artigos.Edita(DocumentoCompra.Linhas.GetEdita(NumLinha).Artigo);

            if ((bool)artigo.CamposUtil["CDU_ArtBLOQC"].Valor
                && DocumentoCompra.Entidade != "44"
                && new List<string> { "ECF", "ECL", "ECP" }.Contains(DocumentoCompra.Tipodoc))
            {
                PSO.MensagensDialogos.MostraAviso("Este artigo está bloqueado para Encomendas!", StdPlatBS100.StdBSTipos.IconId.PRI_Critico);
                DocumentoCompra.Linhas.GetEdita(NumLinha).Artigo = null;
                Cancel = true;
            }
        }
    }
}
