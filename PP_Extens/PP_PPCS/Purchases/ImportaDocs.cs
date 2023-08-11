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
            bool Cancel
        #endregion
            )
        {
            string localstr = "", SQLErrors = "";
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

                    while (!RSet.NoFim()) {

                        int numLinhas = docNovo.Linhas.NumItens;
                        var ultimaLinha = docNovo.Linhas.GetEdita(numLinhas);

                        if (!string.IsNullOrEmpty(RSet.Valor("ArtigoDestino")) && BSO.Base.Artigos.Existe(RSet.Valor("ArtigoDestino"))) {

                            double quant = Math.Abs((double)RSet.Valor("Quantidade"));

                            BSO.Compras.Documentos.AdicionaLinha(
                                docNovo,
                                RSet.Valor("ArtigoDestino"),
                                ref quant,
                                RSet.Valor("Armazem"),
                                RSet.Valor("Localizacao"),
                                RSet.Valor("PrecUnit") * 0,
                                RSet.Valor("Descontro1"),
                                RSet.Valor("Lote"),
                                1, 1, 1,
                                RSet.Valor("DescEntidade"),
                                RSet.Valor("DescPag"),
                                0, 0,
                                false,
                                false,
                                BSO.Base.Iva.DaValorAtributo("23", "Taxa")
                                );

                            numLinhas = docNovo.Linhas.NumItens;
                            ultimaLinha = docNovo.Linhas.GetEdita(numLinhas);

                            ultimaLinha.CamposUtil["CDU_Pescado"] = RSet.Valor("CDU_Pescado");
                            ultimaLinha.CamposUtil["CDU_NomeCientifico"] = RSet.Valor("CDU_NomeCientfico");
                            ultimaLinha.CamposUtil["CDU_Origem"] = RSet.Valor("CDU_Origem");
                            ultimaLinha.CamposUtil["CDU_FormaObtencao"] = RSet.Valor("CDU_FormaObtencao");
                            ultimaLinha.CamposUtil["CDU_ZonaFAO"] = RSet.Valor("CDU_ZonaFAO");
                            ultimaLinha.CamposUtil["CDU_Caixas"] = RSet.Valor("CDU_Caixas");
                            ultimaLinha.CamposUtil["CDU_VendaEmCaixa"] = RSet.Valor("CDU_VendaEmCaixa");
                            ultimaLinha.CamposUtil["CDU_KilosPorCaixa"] = RSet.Valor("CDU_KilosPorCaixa");
                            ultimaLinha.CamposUtil["CDU_Fornecedor"] = RSet.Valor("CDU_Fornecedor");

                            dynamic kilosPorCaixa = RSet.Valor("CDU_KilosPorCaixa");

                            if (ultimaLinha.Unidade != RSet.Valor("Unidade")) {

                                if (ultimaLinha.Unidade == "KG" && RSet.Valor("Unidade") == "CX" && kilosPorCaixa != 0) {
                                    ultimaLinha.Quantidade = ultimaLinha.Quantidade * kilosPorCaixa;
                                    ultimaLinha.PrecUnit = ultimaLinha.PrecUnit / kilosPorCaixa;
                                    ultimaLinha.CamposUtil["CDU_vendaEmCaixa"].Valor = 0;
                                } else {
                                    if (!Cancelar) {
                                        PSO.MensagensDialogos.MostraAviso($"N�o � poss�vel converter o artigo {RSet.Valor("Artigo")} em {RSet.Valor("ArtigoDestino")}.\nO documento n�o ser� importado.", StdBSTipos.IconId.PRI_Exclama);
                                    }
                                    Cancelar = true;
                                }
                            }
                        } else if (BSO.Base.Artigos.Existe(RSet.Valor("Artigo"))) {

                        }
                    }
                }

                return false;
            }
        }
    }
}
