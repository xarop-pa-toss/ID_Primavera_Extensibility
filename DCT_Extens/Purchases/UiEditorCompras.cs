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
using PRISDK100;


namespace DCT_Extens.Purchases
{
    public class UiEditorCompras : EditorCompras
    {
        private clsSDKContexto sdk;
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            sdk = new clsSDKContexto();
            sdk.Inicializa(BSO, "ERP");
            PSO.InicializaPlataforma(sdk);
            
            base.AntesDeGravar(ref Cancel, e);

            #region Bloqueio de encomendas por artigo
            // Mesma verificação que no ArtigoIdentificado
            // É feita aqui pois podem ser alterados os CDU em runtime
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
            #endregion
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            #region Bloqueio de encomendas por artigo
            BasBEArtigo artigo = BSO.Base.Artigos.Edita(DocumentoCompra.Linhas.GetEdita(NumLinha).Artigo);

            if ((bool)artigo.CamposUtil["CDU_ArtBLOQC"].Valor
                && DocumentoCompra.Entidade != "44"
                && new List<string> { "ECF", "ECL", "ECP" }.Contains(DocumentoCompra.Tipodoc))
            {
                
                PSO.MensagensDialogos.MostraAviso("Este artigo está bloqueado para Encomendas!", StdPlatBS100.StdBSTipos.IconId.PRI_Critico);
                DocumentoCompra.Linhas.GetEdita(NumLinha).Artigo = null;
                Cancel = true;
            }
            #endregion
        }
    }
}