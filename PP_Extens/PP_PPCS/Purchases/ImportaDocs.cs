using Primavera.Extensibility.Purchases.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdPlatBS100;
using ErpBS100;
using CmpBE100;
using BasBE100;
using StdBE100;



namespace PP_PPCS
{
    public class ImportaDocs : EditorCompras
    {
        private bool CriarDocumentoCompra(
        #region parametros
            string TipoEntidade,
            string Entidade,
            string Filial,
            string TipoDoc,
            string Serie,
            long? NumDoc,
            string EntLocal,
            string FilialDest,
            string TipoDocDest,
            string SerieDest,
            long? NumDocDest,
            DateTime DataDoc,
            string Importa,
            bool Cancel,
            string SQLErrors
        #endregion
            )
        {
            string localstr = "";
            bool Cancelar = false;

            // Preenchimento do novo documento de compra
            if (Importa == "A" && BSO.Compras.Documentos.Existe(FilialDest, TipoDocDest, SerieDest, (int)NumDocDest) == true) {
                CmpBEDocumentoCompra docNovo = new CmpBEDocumentoCompra();
                docNovo = BSO.Compras.Documentos.Edita(FilialDest, TipoDocDest, SerieDest, (int)NumDocDest);
                docNovo.DataDoc = DataDoc;
                docNovo.Entidade = EntLocal;
                if (docNovo.Linhas.NumItens > 0) { docNovo.Linhas.RemoveTodos(); }

                // Linhas de coment�rio adicionadas ao novo documento de compra
                BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, Descricao: @"
                    Este documento � replica��o do documento f�sico " + TipoDoc + " N� " + NumDoc.ToString() + "/" + Serie + ".");
                BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, Descricao: @"
                    C�pia do documento original.");
                BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, Descricao: @"
                    Documento original anulado!");

                BSO.Compras.Documentos.PreencheDadosRelacionados(docNovo);
                BSO.Compras.Documentos.Actualiza(docNovo);

                docNovo.Dispose();
            } else if (Importa == "S") {
                // Verificar se a entidade fornecedor existe
                if (!BSO.Base.Fornecedores.Existe(EntLocal)) {
                    string docFull = TipoDoc + " N.� " + NumDoc.ToString() + "/" + Serie;
                    localstr = Microsoft.VisualBasic.Interaction.InputBox(
                        "A entidade (" + Entidade + ") no documento " + docFull + ".\n" +
                        "N�o existe ou n�o possui correspondencia.\n\n" +
                        "Qual a entidade a utilizar?", Title: docFull);

                    if (BSO.Base.Fornecedores.Existe(localstr)) {
                        EntLocal = localstr;

                        string query = string.Format(@"
                            UPDATE TDU_CorrespondenciaEntidades 
                            SET CDU_EntLocal = {0}
                            WHERE CDU_TipoEntidade = {1}
                                AND CDU_EntERP = {2}", EntLocal, TipoEntidade, Entidade);

                        PSO.ExecSql.ExecutaSP("query", ref SQLErrors);
                    } else {
                        EntLocal = "";
                    }
                }

                // Controlar a exist�ncia de entidade e s�rie do documento
                if (EntLocal != "" && BSO.Base.Series.Existe("C", TipoDoc, Serie)) {
                    CmpBEDocumentoCompra docNovo = new CmpBEDocumentoCompra();
                    StdBELista RSet = new StdBELista();

                    // Verificar exist�ncia do documento de destino
                    localstr = FilialDest + SerieDest;

                    if (!string.IsNullOrEmpty(localstr) && NumDocDest.HasValue) {

                        // Documento j� existente, verificar a exist�ncia e editar se existir
                        if (BSO.Compras.Documentos.Existe(FilialDest, TipoDocDest, SerieDest, (int)NumDocDest)) {

                            docNovo = BSO.Compras.Documentos.Edita(FilialDest, TipoDocDest, SerieDest, (int)NumDocDest);

                            RSet = BSO.Consulta(QueriesSQL.GetQuery04(Filial, TipoDoc, Serie, NumDoc.ToString()));
                            docNovo.DataDoc = DataDoc;
                            docNovo.Entidade = EntLocal;
                            docNovo.DescFornecedor = RSet.Valor("DescEntidade");
                            docNovo.DescFinanceiro = RSet.Valor("DescPag");

                            RSet.Dispose();
                            if (docNovo.Linhas.NumItens > 0) { docNovo.Linhas.RemoveTodos(); }
                        } else { return false; }
                    } else {

                        RSet = BSO.Consulta(QueriesSQL.GetQuery04(Filial, TipoDoc, Serie, NumDoc.ToString()));
                        docNovo.Filial = Filial;
                        docNovo.Serie = Serie;
                        docNovo.Tipodoc = TipoDoc;
                        docNovo.TipoEntidade = TipoEntidade;
                        docNovo.Entidade = EntLocal;
                        docNovo.CamposUtil["CDU_FilialOrig"].Valor = Filial;
                        docNovo.CamposUtil["CDU_TipoDocOrig"].Valor = TipoDoc;
                        docNovo.CamposUtil["CDU_SerieOrig"].Valor = Serie;
                        docNovo.CamposUtil["CDU_NumDocOrig"].Valor = NumDoc;

                        int vdDadosTodos = (int)BasBETiposGcp.PreencheRelacaoCompras.compDadosTodos;
                        BSO.Compras.Documentos.PreencheDadosRelacionados(docNovo, ref vdDadosTodos);

                        docNovo.DataDoc = DataDoc;
                        if (docNovo.DataIntroducao < DataDoc) { docNovo.DataIntroducao = DataDoc; }
                        if (docNovo.DataVenc < DataDoc) { docNovo.DataVenc = DataDoc; }

                        docNovo.DescFornecedor = RSet.Valor("DescEntidade");
                        docNovo.DescFinanceiro = RSet.Valor("DescPag");
                        docNovo.TrataIvaCaixa = false;

                        RSet.Dispose();
                    }

                    RSet = BSO.Consulta(QueriesSQL.GetQuery05(Filial, TipoDoc, Serie, NumDoc.ToString()));
                    docNovo.NumDocExterno = TipoDoc + " N� " + NumDoc.ToString() + "/" + Serie + " - " + RSet.Valor("NumDocExterno");

                    RSet = BSO.Consulta(QueriesSQL.GetQuery06(Filial, TipoDoc, Serie, NumDoc.ToString()));

                }
                return false;
            }
        }
    }
}
