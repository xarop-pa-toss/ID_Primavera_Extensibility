using BasBE100;
using CmpBE100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using PRISDK100;
using System.Collections.Generic;


namespace DCT_Extens.Purchases
{
    public class UiEditorCompras : EditorCompras
    {
        private clsSDKContexto sdk;
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            //sdk = new clsSDKContexto();
            //sdk.Inicializa(BSO, "ERP");
            //PSO.InicializaPlataforma(sdk);

            base.AntesDeGravar(ref Cancel, e);

            #region Bloqueio de encomendas por artigo
            // Mesma verifica��o que no ArtigoIdentificado
            // � feita aqui pois podem ser alterados os CDU em runtime
            if (DocumentoCompra.Entidade != "44"
                && new List<string> { "ECF", "ECL", "ECP" }.Contains(DocumentoCompra.Tipodoc)
                && DocumentoCompra.Linhas.NumItens > 0)
            {
                BasBEArtigo artigo = new BasBEArtigo();
                foreach (CmpBELinhaDocumentoCompra linha in DocumentoCompra.Linhas)
                {
                    artigo = BSO.Base.Artigos.Edita(linha.Artigo);

                    if (linha.TipoLinha.Equals("10") && (bool)artigo.CamposUtil["CDU_ArtBLOQC"].Valor) 
                    {
                        PSO.MensagensDialogos.MostraErro($"O artigo {linha.Artigo} encontra-se bloqueado para Encomendas!");
                        Cancel = true;
                    }
                }
            }
            #endregion
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            //#region Bloqueio de encomendas por artigo
            //BasBEArtigo artigo = BSO.Base.Artigos.Edita(DocumentoCompra.Linhas.GetEdita(NumLinha).Artigo);

            //if ((bool)artigo.CamposUtil["CDU_ArtBLOQC"].Valor
            //    && DocumentoCompra.Entidade != "44"
            //    && new List<string> { "ECF", "ECL", "ECP" }.Contains(DocumentoCompra.Tipodoc))
            //{

            //    PSO.MensagensDialogos.MostraAviso("Este artigo est� bloqueado para Encomendas!", StdPlatBS100.StdBSTipos.IconId.PRI_Critico);
            //    DocumentoCompra.Linhas.GetEdita(NumLinha).Artigo = null;
            //    Cancel = true;
            //}
            //#endregion
        }
    }
}