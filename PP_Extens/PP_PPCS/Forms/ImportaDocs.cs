using BasBE100;
using CctBE100;
using CmpBE100;
using ErpBS100;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.Purchases.Editors;
using StdBE100;
using StdPlatBS100;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VndBE100;
using System.Data;
using HelpersPrimavera10;
using PRISDK100;

namespace PP_PPCS
{
    public class ImportaDocs : EditorCompras
    {
        private ErpBS __BSO;
        private StdBSInterfPub __PSO;
        private clsSDKContexto _sdkContexto;

        private HelperFunctions _Helpers = new HelperFunctions();

        public ImportaDocs()
        {
            __PSO = PriMotores.Plataforma;
            __BSO = PriMotores.Motor;
            _sdkContexto = PriMotores.PriSDKContexto;
        }

        public void CriarDocumentoCompra(ref DataRow linha, DateTime datepickerDocNovoValue)
        {
            string localstr = "", SQLErrors = "";
            bool Cancelar, Cancel;
            int vdDadosTodos = (int)BasBETiposGcp.PreencheRelacaoCompras.compDadosTodos;
            CmpBEDocumentoCompra docNovo;
            StdBELista RSet;

            string TipoEntidade = linha["TipoEntidade"].ToString();
            string Entidade = linha["Entidade"].ToString();
            string Filial = linha["Filial"].ToString();
            string TipoDoc = linha["TipoDoc"].ToString();
            string Serie = linha["Serie"].ToString();
            int? NumDoc = (int)linha["NumDoc"];
            DateTime DataDoc = Convert.ToDateTime(linha["Data"] ?? datepickerDocNovoValue);
            string EntLocal = linha["EntLocal"]?.ToString() ?? "";
            string FilialDest = linha["FilialLoc"]?.ToString() ?? "";
            string TipoDocDest = linha["TipoDocLoc"]?.ToString() ?? "";
            string SerieDest = linha["SerieLoc"]?.ToString() ?? "";
            int NumDocDest = linha["NumDocLocal"] as int? ?? 0;
            string Importa = linha["Importa"].ToString();

            Cancelar = false;
            Cancel = false;

            __BSO.IniciaTransaccao();

            // Preenchimento do novo documento de compra
            if (Importa == "A" && __BSO.Compras.Documentos.Existe(FilialDest, TipoDocDest, SerieDest, NumDocDest) == true) {
                docNovo = __BSO.Compras.Documentos.Edita(FilialDest, TipoDocDest, SerieDest, NumDocDest);

                docNovo.DataDoc = DataDoc;
                docNovo.Entidade = EntLocal;
                if (docNovo.Linhas.NumItens > 0) { docNovo.Linhas.RemoveTodos(); }

                // Linhas de comentário adicionadas ao novo documento de compra
                __BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, Descricao: @"
                    Este documento é replicação do documento físico " + TipoDoc + " Nº " + NumDoc.ToString() + "/" + Serie + ".");
                __BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, Descricao: @"
                    Cópia do documento original.");
                __BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, Descricao: @"
                    Documento original anulado!");

                __BSO.Compras.Documentos.PreencheDadosRelacionados(docNovo);
                __BSO.Compras.Documentos.Actualiza(docNovo);

            } else if (Importa == "S") {
                // Verificar se a entidade fornecedor existe
                if (!__BSO.Base.Fornecedores.Existe(EntLocal)) {
                    string docFull = TipoDoc + " N.º " + NumDoc.ToString() + "/" + Serie;
                    localstr = _Helpers.MostraInputForm(
                        "",
                        "A entidade (" + Entidade + ") no documento " + docFull + Environment.NewLine +
                        "Não existe ou não possui correspondencia." + Environment.NewLine +
                        "Qual a entidade a utilizar?", "");

                    if (__BSO.Base.Fornecedores.Existe(localstr)) {
                        EntLocal = localstr ?? "";

                        using (StdBE100.StdBEExecSql sql = new StdBEExecSql())
                        {
                            sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                            sql.Tabela = "TDU_CorrespondenciaEntidades";
                            sql.AddCampo("CDU_EntLocal", EntLocal);
                            sql.AddCampo("CDU_TipoEntidade", TipoEntidade, true);
                            sql.AddCampo("CDU_EntERP", Entidade, true);

                            __PSO.ExecSql.Executa(sql);
                        }
                    } 
                    else {
                        EntLocal = "";
                    }
                }

                // Controlar a existência de entidade e série do documento
                if (EntLocal != "" && __BSO.Base.Series.Existe("C", TipoDoc, Serie)) {
                    docNovo = new CmpBEDocumentoCompra();
                    RSet = new StdBELista();

                    // Verificar existência do documento de destino
                    localstr = FilialDest + SerieDest;

                    if (!string.IsNullOrEmpty(localstr) && NumDocDest  != 0) {

                        // Documento já existente, verificar a existência e editar se existir
                        if (__BSO.Compras.Documentos.Existe(FilialDest, TipoDocDest, SerieDest, NumDocDest)) {
                            docNovo = __BSO.Compras.Documentos.Edita(FilialDest, TipoDocDest, SerieDest, NumDocDest);

                            RSet = __BSO.Consulta(QueriesSQL.GetQuery04(Filial, TipoDoc, Serie, NumDoc.ToString()));
                            docNovo.DataDoc = DataDoc;
                            docNovo.Entidade = EntLocal;
                            docNovo.DescFornecedor = RSet.Valor("DescEntidade") == null ? 0 : RSet.Valor("DescEntidade");
                            docNovo.DescFinanceiro = RSet.Valor("DescPag") == null ? 0 : RSet.Valor("DescPag");

                            RSet.Termina();
                            if (docNovo.Linhas.NumItens > 0) { docNovo.Linhas.RemoveTodos(); }

                        } else { return; }
                    } else {
                        RSet = __BSO.Consulta(QueriesSQL.GetQuery04(Filial, TipoDoc, Serie, NumDoc.ToString()));

                        docNovo = new CmpBEDocumentoCompra();
                        docNovo.Filial = Filial;
                        docNovo.Serie = Serie;
                        docNovo.Tipodoc = TipoDocDest;
                        docNovo.TipoEntidade = TipoEntidade;
                        docNovo.Entidade = EntLocal;
                        docNovo.CamposUtil["CDU_FilialOrig"].Valor = Filial;
                        docNovo.CamposUtil["CDU_TipoDocOrig"].Valor = TipoDoc;
                        docNovo.CamposUtil["CDU_SerieOrig"].Valor = Serie;
                        docNovo.CamposUtil["CDU_NumDocOrig"].Valor = NumDoc;
                        
                        __BSO.Compras.Documentos.PreencheDadosRelacionados(docNovo, ref vdDadosTodos);

                        docNovo.DataDoc = DataDoc;
                        if (docNovo.DataIntroducao < DataDoc) { docNovo.DataIntroducao = DataDoc; }
                        if (docNovo.DataVenc < DataDoc) { docNovo.DataVenc = DataDoc; }

                        docNovo.DescFornecedor = RSet.Valor("DescEntidade") == null ? 0 : RSet.Valor("DescEntidade");
                        docNovo.DescFinanceiro = RSet.Valor("DescPag") == null ? 0 : RSet.Valor("DescPag");
                        docNovo.TrataIvaCaixa = false;

                        RSet.Termina();
                    }

                    RSet = __BSO.Consulta(QueriesSQL.GetQuery05(Filial, TipoDoc, Serie, NumDoc.ToString()));
                    docNovo.NumDocExterno = TipoDoc + " Nº " + NumDoc.ToString() + "/" + Serie + " - " + RSet.Valor("NumDocExterno");

                    RSet = __BSO.Consulta(QueriesSQL.GetQuery06(Filial, TipoDoc, Serie, NumDoc.ToString()));

                    while (!RSet.NoFim()) {

                        CmpBELinhaDocumentoCompra ultimaLinha;
                        double quant = Math.Abs((double)RSet.Valor("Quantidade"));

                        if (!string.IsNullOrEmpty(RSet.Valor("ArtigoDestino")) && __BSO.Base.Artigos.Existe(RSet.Valor("ArtigoDestino"))) {
                            
                            AdicionaLinhaCompra(RSet, docNovo, "ArtigoDestino");

                            ultimaLinha = docNovo.Linhas.GetEdita(docNovo.Linhas.NumItens);

                            if (ultimaLinha.Unidade != RSet.Valor("Unidade")) {
                                dynamic kilosPorCaixa = RSet.Valor("CDU_KilosPorCaixa");

                                if (ultimaLinha.Unidade == "KG" && RSet.Valor("Unidade") == "CX" && kilosPorCaixa != 0) {
                                    ultimaLinha.Quantidade = ultimaLinha.Quantidade * kilosPorCaixa;
                                    ultimaLinha.PrecUnit = ultimaLinha.PrecUnit / kilosPorCaixa;
                                    ultimaLinha.CamposUtil["CDU_vendaEmCaixa"].Valor = 0;
                                } else {
                                    if (!Cancelar) {
                                        __PSO.MensagensDialogos.MostraAviso($"Não é possível converter o artigo {RSet.Valor("Artigo")} em {RSet.Valor("ArtigoDestino")}.\nO documento não será importado.", StdBSTipos.IconId.PRI_Exclama);
                                    }
                                    Cancelar = true;
                                }
                            }
                        } else if (__BSO.Base.Artigos.Existe(RSet.Valor("Artigo"))) {
                            
                            AdicionaLinhaCompra(RSet, docNovo, "Artigo");

                        } else if (RSet.Valor("Artigo") is null) {
                            __BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario);
                        } else {
                            __PSO.MensagensDialogos.MostraAviso(
                                $"ATENÇÃO!\n\nO artigo {RSet.Valor("Artigo")} não esxiste na base de dados.\nCrie o artigo e volte a importar o documento.",
                                StdBSTipos.IconId.PRI_Exclama,
                                $"Artigo: {RSet.Valor("Artigo")} - {RSet.Valor("Descricao")}");
                            Cancel = true;

                            TransaccaoHandler(Cancel);
                        }
                        RSet.Seguinte();
                    } // end while

//---------------------------
                    if (docNovo.Linhas.NumItens > 0 && !Cancelar) {

                        string numdoc = NumDoc.HasValue ? NumDoc.Value.ToString() : null;

                        __BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, 0,
                            $"Este documento é replicação do documento fisico {TipoDoc} N.º {NumDoc.ToString() ?? null}/{Serie}.");
                        __BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, 0,
                            "Cópia do documento original");

                        docNovo.TrataIvaCaixa = false;
                        docNovo.CamposUtil["CDU_NumDocOrig"].Valor = numdoc;
                        __BSO.Compras.Documentos.Actualiza(docNovo);

                        EntLocal = docNovo.Entidade;
                        FilialDest = docNovo.Filial;
                        TipoDocDest = docNovo.Tipodoc;
                        SerieDest = docNovo.Serie;
                        NumDocDest = docNovo.NumDoc;
                        DataDoc = docNovo.DataDoc;
                        Importa = "N";
                    }

                    RSet.Termina();
                    docNovo.Dispose();
                }
            }
            // Fim de CriarDocumentoCompra
            if(__BSO.EmTransaccao()) { __BSO.TerminaTransaccao(); }
        }

        public void CriarDocumentoVenda(ref DataRow linha, DateTime datepickerDocNovoValue)
        {
            VndBEDocumentoVenda docNovo;
            CctBEDocumentoLiq docLiq;
            StdBELista RSet = new StdBELista();
            int vdDadosTodos = (int)BasBETiposGcp.PreencheRelacaoCompras.compDadosTodos;

            bool Cancelar, docDestinoJaExiste, liqDocAnulada, fazerLiquidacao, Cancel;
            string fl = "", tdl = "", sl = "", strErro = "";
            int ndl = 0;

            string TipoEntidade = linha["TipoEntidade"].ToString();
            string Entidade = linha["Entidade"].ToString();
            string Filial = linha["Filial"].ToString();
            string TipoDoc = linha["TipoDoc"].ToString();
            string Serie = linha["Serie"].ToString();
            int NumDoc = (int)linha["NumDoc"];
            DateTime DataDoc = (linha["data"] != DBNull.Value )? Convert.ToDateTime(linha["Data"]) : datepickerDocNovoValue;
            string EntLocal = linha["EntLocal"]?.ToString() ?? "";
            string FilialDest = linha["FilialLoc"]?.ToString() ?? "";
            string TipoDocDest = linha["TipoDocLoc"]?.ToString() ?? "";
            string SerieDest = linha["SerieLoc"]?.ToString() ?? "";
            int NumDocDest = linha["NumDocLocal"] as int? ?? 0;
            string Importa = linha["Importa"].ToString();

            docNovo = __BSO.Vendas.Documentos.Edita(FilialDest, TipoDocDest, SerieDest, NumDocDest);
            Cancelar = false; 
            Cancel = false;
            __BSO.IniciaTransaccao();

            switch (Importa) {
                case "A": // Anular o documento de destino
                    if (__BSO.Vendas.Documentos.Existe(FilialDest, TipoDocDest, SerieDest, NumDocDest)) {
                        // A próxima linha preenche as quatro variáveis dadas por referência. Usadas logo a seguir para anular a liquidação.
                        if (__BSO.PagamentosRecebimentos.Liquidacoes.DaDocLiquidacao(FilialDest, "V", TipoDocDest, SerieDest, NumDocDest, ref fl, ref tdl, ref sl, ref ndl)) {

                            // Anular a liquidação do documento
                            __BSO.PagamentosRecebimentos.Liquidacoes.Remove(fl, tdl, sl, ndl);

                            #region PossivelBugFix
                            // No código da V9 existe um bloco aqui que corrige um suposto bug que não actualiza o numerador dos documentos.
                            // Query original traduzida: $"UPDATE SeriesCCT SET Numerador = (SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'";
                            // Descomentar as próximas linhas se necessário.

                            //// Como o Update actua sob um Select e temos de usar o StdBEExecSql, vamos primeiro usar __BSO.Consulta (Select) e usar o resultado no Update.
                            //StdBELista subSelect = __BSO.Consulta($"SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'");

                            //using (StdBEExecSql sql = new StdBEExecSql()) {
                            //    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                            //    sql.Tabela = "SeriesCCT";
                            //    sql.AddCampo("Numerador", subSelect.Valor(0));
                            //    sql.AddQuery();

                            //    __PSO.ExecSql.Executa(sql);
                            //}
                            #endregion
                        }

                        docNovo = __BSO.Vendas.Documentos.Edita(FilialDest, TipoDocDest, SerieDest, NumDocDest);
                        docNovo.HoraDefinida = false;
                        docNovo.Entidade = EntLocal;
                        docNovo.DataDoc = DataDoc;

                        if (docNovo.Linhas.NumItens > 0) { docNovo.Linhas.RemoveTodos(); }

                        // Linhas de comentário adicionadas ao novo documento de compra
                        __BSO.Vendas.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: @"
                            Este documento é replicação do documento físico " + TipoDoc + " Nº " + NumDoc.ToString() + "/" + Serie + ".");
                        __BSO.Vendas.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: "Cópia do documento original.");
                        __BSO.Vendas.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: "Documento original anulado!");

                       __BSO.Vendas.Documentos.PreencheDadosRelacionados(docNovo, ref vdDadosTodos);

                        if (__BSO.Vendas.Documentos.ValidaActualizacao(docNovo, __BSO.Vendas.TabVendas.Edita(docNovo.Tipodoc), ref SerieDest, ref strErro))
                        __BSO.Vendas.Documentos.Actualiza(docNovo);

                        if (__BSO.PagamentosRecebimentos.Liquidacoes.DaDocLiquidacao(FilialDest, "V", TipoDocDest, SerieDest, NumDocDest, ref fl, ref tdl, ref sl, ref ndl)) {
                            //' O doc. venda acabado de gravar fez liquidação automática.
                            //' Garantir que o numerador da série do doc. liquidação criado fica correto (parece ser um bug do Primavera):
                            #region PossivelBugFix
                            // No código da V9 existe um bloco aqui que corrige um suposto bug que não actualiza o numerador dos documentos.
                            // Query original traduzida: $"UPDATE SeriesCCT SET Numerador = (SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'";
                            // Descomentar as próximas linhas se necessário.

                            //// Como o Update actua sob um Select e temos de usar o StdBEExecSql, vamos primeiro usar __BSO.Consulta (Select) e usar o resultado no Update.
                            //StdBELista subSelect = __BSO.Consulta($"SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'");

                            //using (StdBEExecSql sql = new StdBEExecSql()) {
                            //    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                            //    sql.Tabela = "SeriesCCT";
                            //    sql.AddCampo("Numerador", subSelect.Valor(0));
                            //    sql.AddQuery();

                            //    __PSO.ExecSql.Executa(sql);
                            //}
                            #endregion
                        }
                    }
                    break;

                case "S": // Importar o documento
                    // Verificar se a entidade já existe
                    if (!__BSO.Base.Clientes.Existe(EntLocal)) {
                        __PSO.MensagensDialogos.MostraAviso($"A entidade {Entidade} no documento {TipoDoc} N.º{NumDoc}/{Serie} não possui entidade local correspondente." +
                            $"Este documento não vai ser importado!", StdBSTipos.IconId.PRI_Critico);
                        Cancel = true;
                        TransaccaoHandler(Cancel);
                    } else {
                        if (!__BSO.Base.Series.Existe("V", TipoDocDest, Serie)) {
                            __PSO.MensagensDialogos.MostraAviso($"A série do documento {TipoDoc} N.º{NumDoc}/{Serie} não está criada localmente." +
                            $"Este documento não vai ser importado!", StdBSTipos.IconId.PRI_Critico);
                            Cancel = true;
                            TransaccaoHandler(Cancel);
                        } else {
                            // Verificar se documento de destino existe.
                            // Se sim, anula liquidação e abre documento para ser editado. Se não, cria um novo.
                            if (__BSO.Vendas.Documentos.Existe(FilialDest, TipoDocDest, SerieDest, NumDocDest)) {

                                docDestinoJaExiste = true;
                                liqDocAnulada = false;

                                if (__BSO.PagamentosRecebimentos.Liquidacoes.DaDocLiquidacao(FilialDest, "V", TipoDocDest, SerieDest, NumDocDest, ref fl, ref tdl, ref sl, ref ndl)) {
                                    // Anular a liquidação do documento
                                    __BSO.PagamentosRecebimentos.Liquidacoes.Remove(fl, tdl, sl, ndl);
                                    liqDocAnulada = true;

                                    #region PossivelBugFix
                                    // No código da V9 existe um bloco aqui que corrige um suposto bug que não actualiza o numerador dos documentos.
                                    // Query original traduzida: $"UPDATE SeriesCCT SET Numerador = (SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'";
                                    // Descomentar as próximas linhas se necessário.

                                    //// Como o Update actua sob um Select e temos de usar o StdBEExecSql, vamos primeiro usar __BSO.Consulta (Select) e usar o resultado no Update.
                                    //StdBELista subSelect = __BSO.Consulta($"SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'");

                                    //using (StdBEExecSql sql = new StdBEExecSql()) {
                                    //    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                                    //    sql.Tabela = "SeriesCCT";
                                    //    sql.AddCampo("Numerador", subSelect.Valor(0));
                                    //    sql.AddQuery();

                                    //    __PSO.ExecSql.Executa(sql);
                                    //}
                                    #endregion
                                }

                                docNovo = __BSO.Vendas.Documentos.Edita(FilialDest, TipoDocDest, SerieDest, NumDocDest);
                                RSet = __BSO.Consulta(QueriesSQL.GetQuery07(Filial, TipoDoc, Serie, NumDoc.ToString()));

                                docNovo.DataDoc = DataDoc;
                                docNovo.Entidade = EntLocal;
                                docNovo.DescEntidade = RSet.Valor("DescEntidade");
                                docNovo.DescFinanceiro = RSet.Valor("DescPag");
                                docNovo.Responsavel = RSet.Valor("RespCobranca");

                                if (docNovo.Linhas.NumItens > 0) { docNovo.Linhas.RemoveTodos(); }
                                RSet.Termina();
                            } else {

                                docDestinoJaExiste = false;
                                docNovo = new VndBEDocumentoVenda();

                                RSet = __BSO.Consulta(QueriesSQL.GetQuery07(Filial, TipoDoc, Serie, NumDoc.ToString()));

                                docNovo.Tipodoc = TipoDocDest;
                                docNovo.Filial = Filial;
                                docNovo.Serie = Serie;
                                docNovo.TipoEntidade = TipoEntidade;
                                docNovo.Entidade = EntLocal;
                                docNovo.CamposUtil["CDU_FilialOrig"].Valor = Filial;
                                docNovo.CamposUtil["CDU_TipoDocOrig"].Valor = TipoDoc;
                                docNovo.CamposUtil["CDU_SerieOrig"].Valor = Serie;
                                docNovo.CamposUtil["CDU_NumDocOrig"].Valor = NumDoc;

                                __BSO.Vendas.Documentos.PreencheDadosRelacionados(docNovo, ref vdDadosTodos);

                                docNovo.HoraDefinida = true;
                                docNovo.DataDoc = DataDoc;
                                docNovo.DataHoraCarga = DataDoc.AddMinutes(1);
                                docNovo.DescEntidade = RSet.Valor("DescEntidade");
                                docNovo.DescFinanceiro = RSet.Valor("DescPag");
                                docNovo.Responsavel = RSet.Valor("RespCobranca");
                                docNovo.TrataIvaCaixa = false;

                                if (docNovo.DataVenc < docNovo.DataDoc) { docNovo.DataVenc = docNovo.DataDoc; }
                                RSet.Termina();
                            }

                            // LINHAS DOC

                            // Ver se série de documento tem IVA incluido por defeito
                            bool ivaIncluido = __BSO.Consulta(QueriesSQL.GetQuery08(TipoDoc, Serie)).Valor("IvaIncluido") ? true : false;

                            RSet = __BSO.Consulta(QueriesSQL.GetQuery09(Filial, TipoDoc, Serie, NumDoc.ToString()));
                            VndBELinhaDocumentoVenda ultimaLinha = new VndBELinhaDocumentoVenda();

                            while (!RSet.NoFim() && !Cancelar) {
                                if (!string.IsNullOrEmpty(RSet.Valor("ArtigoDestino")) && __BSO.Base.Artigos.Existe(RSet.Valor("ArtigoDestino"))) {

                                    AdicionaLinhaVenda(RSet, docNovo, ivaIncluido, "ArtigoDestino");

                                    ultimaLinha.CamposUtil["CDU_Fornecedor"] = RSet.Valor("CDU_Fornecedor");
                                    if (ultimaLinha.Unidade != RSet.Valor("Unidade").ToString())
                                    {
                                        double kilosPorCaixa = (double)RSet.Valor("CDU_KilosPorCaixa");

                                        if (ultimaLinha.Unidade == "KG" && RSet.Valor("Unidade") == "CX" && kilosPorCaixa != 0)
                                        {
                                            ultimaLinha.Quantidade = ultimaLinha.Quantidade * kilosPorCaixa;
                                            ultimaLinha.PrecUnit = ultimaLinha.PrecUnit / kilosPorCaixa;
                                            ultimaLinha.CamposUtil["CDU_VendaEmCaixa"].Valor = 0;
                                        } else
                                            if (!Cancelar)
                                        {
                                            __PSO.MensagensDialogos.MostraAviso($"Não é possível converter o artigo {RSet.Valor("Artigo")} em {RSet.Valor("ArtigoDestino")}. \n\nO documento não será importado!", StdBSTipos.IconId.PRI_Critico);
                                        }
                                        Cancelar = true;
                                    }
                                } else if (!string.IsNullOrEmpty(RSet.Valor("Artigo")) && __BSO.Base.Artigos.Existe(RSet.Valor("Artigo"))) {

                                    AdicionaLinhaVenda(RSet, docNovo, ivaIncluido, "Artigo");

                                } else if (string.IsNullOrEmpty(RSet.Valor("Artigo"))) {

                                    __BSO.Vendas.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: "");

                                } else {
                                    __PSO.MensagensDialogos.MostraAviso($"Atenção!\n\nO artigo {RSet.Valor("Artigo")} não existe na base de dados.\nCrie o artigo e volte a importar o documento.", StdBSTipos.IconId.PRI_Exclama);
                                    Cancelar = true;
                                }
                                RSet.Seguinte();
                            } // End Loop RSet

                            RSet.Dispose();
                            fazerLiquidacao = false;

                            if (Cancelar) {
                                // O documento não vai ser gravado e a liquidação é reactivada
                                if (docDestinoJaExiste && fazerLiquidacao) { fazerLiquidacao = true; }
                                Cancel = true;
                            } else if (docDestinoJaExiste || docNovo.Linhas.NumItens > 0) {
                                __BSO.Vendas.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario,
                                    Descricao: $"Este documento é replicação do documento fisico {TipoDoc} N.º {NumDoc}/{Serie}.");
                                __BSO.Vendas.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario,
                                    Descricao: "Cópia do documento original");

                                #region GRAVAR DOCUMENTO VENDA
                                strErro = "";
                                __BSO.Vendas.Documentos.ValidaActualizacao(docNovo, __BSO.Vendas.TabVendas.Edita(docNovo.Tipodoc), ref SerieDest, ref strErro);

                                if (strErro == "")
                                {
                                    __BSO.Vendas.Documentos.Actualiza(docNovo);

                                    EntLocal = docNovo.Entidade;
                                    FilialDest = docNovo.Filial;
                                    TipoDocDest = docNovo.Tipodoc;
                                    SerieDest = docNovo.Serie;
                                    NumDocDest = docNovo.NumDoc;
                                    DataDoc = docNovo.DataDoc;
                                    Importa = "N";

                                    if (__BSO.PagamentosRecebimentos.Liquidacoes.DaDocLiquidacao(FilialDest, "V", TipoDocDest, SerieDest, NumDocDest, ref fl, ref tdl, ref sl, ref ndl))
                                    {
                                        // O documento acabado de gravar fez liquidação automática.
                                        #region PossivelBugFix
                                        // No código da V9 existe um bloco aqui que corrige um suposto bug que não actualiza o numerador dos documentos.
                                        // Query original traduzida: $"UPDATE SeriesCCT SET Numerador = (SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'";
                                        // Descomentar as próximas linhas se necessário.

                                        //// Como o Update actua sob um Select e temos de usar o StdBEExecSql, vamos primeiro usar __BSO.Consulta (Select) e usar o resultado no Update.
                                        //StdBELista subSelect = __BSO.Consulta($"SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'");

                                        //using (StdBEExecSql sql = new StdBEExecSql()) {
                                        //    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                                        //    sql.Tabela = "SeriesCCT";
                                        //    sql.AddCampo("Numerador", subSelect.Valor(0));
                                        //    sql.AddQuery();

                                        //    __PSO.ExecSql.Executa(sql);
                                        //}
                                        #endregion
                                    } else
                                    {
                                        fazerLiquidacao = true;
                                    }

                                    TransaccaoHandler(Cancel);
                                }
                                else
                                {
                                    __PSO.MensagensDialogos.MostraAviso("Erro ao gravar documento " + docNovo.Tipodoc + " "+ docNovo.Serie + "/" + docNovo.NumDoc, StdBSTipos.IconId.PRI_Exclama, strErro);
                                    TransaccaoHandler(true);
                                    return;
                                }
                                #endregion
                            }

                            if (!__BSO.EmTransaccao()) { __BSO.IniciaTransaccao(); }
                            if (fazerLiquidacao && __BSO.PagamentosRecebimentos.Pendentes.Existe(FilialDest, "V", TipoDocDest, SerieDest, NumDocDest)) {
                                // Liquidar o documento acabado de criar/alterar
                                docLiq = new CctBEDocumentoLiq();

                                docLiq.Tipodoc = "VDR";
                                docLiq.Serie = __BSO.Base.Series.Existe("M", docLiq.Tipodoc, SerieDest) ? SerieDest : __BSO.Base.Series.DaSerieDefeito("M", docLiq.Tipodoc, DataDoc);
                                docLiq.TipoEntidade = "C";
                                docLiq.Entidade = EntLocal;

                                int cctDadosTodos = (int)BasBETiposGcp.PreencheRelacaoCCT.cctDadosTodos;
                                __BSO.PagamentosRecebimentos.Liquidacoes.PreencheDadosRelacionados(docLiq, ref cctDadosTodos);

                                docLiq.DataDoc = DataDoc;

                                double valorDescMLiq = 0, valorRecMLiq = __BSO.PagamentosRecebimentos.Pendentes.DaValorAtributo("V", TipoDocDest, NumDocDest, 1, SerieDest, FilialDest, "PEN", 0, "ValorPendente");
                                __BSO.PagamentosRecebimentos.Liquidacoes.AdicionaLinha(
                                    docLiq,
                                    FilialDest,
                                    "V",
                                    TipoDocDest,
                                    SerieDest,
                                    NumDocDest,
                                    1, "PEN", 0,
                                    ref valorRecMLiq,
                                    ref valorDescMLiq, 0);

                                if (__BSO.PagamentosRecebimentos.Liquidacoes.ValidaActualizacao(docLiq, ref strErro))
                                {
                                    __BSO.PagamentosRecebimentos.Liquidacoes.Actualiza(docLiq);
                                }

                                TransaccaoHandler(Cancel);
                                docLiq.Dispose();
                                
                                #region PossivelBugFix
                                // No código da V9 existe um bloco aqui que corrige um suposto bug que não actualiza o numerador dos documentos.
                                // Query original traduzida: $"UPDATE SeriesCCT SET Numerador = (SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'";
                                // Descomentar as próximas linhas se necessário.

                                //// Como o Update actua sob um Select e temos de usar o StdBEExecSql, vamos primeiro usar __BSO.Consulta (Select) e usar o resultado no Update.
                                //StdBELista subSelect = __BSO.Consulta($"SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'");

                                //using (StdBEExecSql sql = new StdBEExecSql()) {
                                //    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                                //    sql.Tabela = "SeriesCCT";
                                //    sql.AddCampo("Numerador", subSelect.Valor(0));
                                //    sql.AddQuery();

                                //    __PSO.ExecSql.Executa(sql);
                                //}
                                #endregion
                            }
                        }
                    }
                    break;
            }
            // Fim de DocumentoVenda
            TransaccaoHandler(Cancel);
            if (docNovo != null) { docNovo.Dispose(); }
        }


        private void AdicionaLinhaVenda(StdBELista RSet, VndBEDocumentoVenda docNovo, bool ivaIncluido, string tipoArtigo)
        {
            // Verificar se o documento original é com iva incluido + outras variáveis necessárias para passar por ref
            double quantidade = Math.Abs((double)RSet.Valor("Quantidade"));
            string armazem = RSet.Valor("Armazem");
            string localizacao = RSet.Valor("Localizacao");

            __BSO.Vendas.Documentos.AdicionaLinha(
                docNovo,
                RSet.Valor(tipoArtigo),
                ref quantidade,
                ref armazem,
                ref localizacao,
                InverterSeNegativo(RSet.Valor("PrecUnit")),
                InverterSeNegativo(RSet.Valor("Desconto1")),
                "", 0, 0, 0,
                InverterSeNegativo(RSet.Valor("DescEntidade")),
                InverterSeNegativo(RSet.Valor("DescPag")),
                0, 0, false, ivaIncluido
            );

            VndBELinhaDocumentoVenda ultimaLinha = docNovo.Linhas.GetEdita(docNovo.Linhas.NumItens);
            ultimaLinha.CamposUtil["CDU_Pescado"].Valor = RSet.Valor("CDU_Pescado");
            ultimaLinha.CamposUtil["CDU_NomeCientifico"].Valor = RSet.Valor("CDU_NomeCientifico");
            ultimaLinha.CamposUtil["CDU_Origem"].Valor = RSet.Valor("CDU_Origem");
            ultimaLinha.CamposUtil["CDU_FormaObtencao"].Valor = RSet.Valor("CDU_FormaObtencao");
            ultimaLinha.CamposUtil["CDU_ZonaFAO"].Valor = RSet.Valor("CDU_ZonaFAO");
            ultimaLinha.CamposUtil["CDU_Caixas"].Valor = RSet.Valor("CDU_Caixas");
            ultimaLinha.CamposUtil["CDU_VendaEmCaixa"].Valor = RSet.Valor("CDU_VendaEmCaixa");
            ultimaLinha.CamposUtil["CDU_KilosPorCaixa"].Valor = RSet.Valor("CDU_KilosPorCaixa");
            ultimaLinha.CamposUtil["CDU_Fornecedor"].Valor = RSet.Valor("CDU_Fornecedor");
        }

        private void AdicionaLinhaCompra(StdBELista RSet, CmpBEDocumentoCompra docNovo, string tipoArtigo)
        {
            double quantidade = Math.Abs((double)RSet.Valor("Quantidade"));
            double taxa = __BSO.Base.Iva.DaValorAtributo("6", "Taxa") == null ? 0 : __BSO.Base.Iva.DaValorAtributo("6", "Taxa");
            string armazem = RSet.Valor("Armazem");
            string localizacao = RSet.Valor("Localizacao");

            __BSO.Compras.Documentos.AdicionaLinha(
                docNovo,
                RSet.Valor(tipoArtigo).ToString(),
                ref quantidade,
                ref armazem,
                ref localizacao,
                RSet.Valor("PrecUnit") * 0,
                InverterSeNegativo(RSet.Valor("Desconto1")),
                RSet.Valor("Lote"),
                0, 0, 0,
                InverterSeNegativo(RSet.Valor("DescEntidade") == null ? 0 : RSet.Valor("DescEntidade")),
                InverterSeNegativo(RSet.Valor("DescPag") == null ? 0 : RSet.Valor("DescPag")),
                0, 0, false, false,
                ref taxa
            );

            CmpBELinhaDocumentoCompra ultimaLinha = docNovo.Linhas.GetEdita(docNovo.Linhas.NumItens);
            ultimaLinha.CamposUtil["CDU_Pescado"].Valor = RSet.Valor("CDU_Pescado");
            ultimaLinha.CamposUtil["CDU_NomeCientifico"].Valor = RSet.Valor("CDU_NomeCientifico");
            ultimaLinha.CamposUtil["CDU_Origem"].Valor = RSet.Valor("CDU_Origem");
            ultimaLinha.CamposUtil["CDU_FormaObtencao"].Valor = RSet.Valor("CDU_FormaObtencao");
            ultimaLinha.CamposUtil["CDU_ZonaFAO"].Valor = RSet.Valor("CDU_ZonaFAO");
            ultimaLinha.CamposUtil["CDU_Caixas"].Valor = RSet.Valor("CDU_Caixas");
            ultimaLinha.CamposUtil["CDU_VendaEmCaixa"].Valor = RSet.Valor("CDU_VendaEmCaixa");
            ultimaLinha.CamposUtil["CDU_KilosPorCaixa"].Valor = RSet.Valor("CDU_KilosPorCaixa");
        }


        private void TransaccaoHandler(bool Cancel)
        {
            if (__BSO.EmTransaccao())
            {
                if (Cancel)
                {
                    __BSO.DesfazTransaccao();
                    return;
                } else
                {
                    __BSO.TerminaTransaccao();
                }
            }
        }

        private T InverterSeNegativo<T>(T num) where T: struct, IComparable, IConvertible, IComparable<T>, IEquatable<T>
        {
            // Check se input é numérico
            if (typeof(T) == typeof(int) || typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(decimal))
            {
                dynamic numDinamico = num;

                // Check se negativo e inverter
                if (numDinamico < 0)
                {
                    numDinamico *= -1;
                    return numDinamico;
                }
            }
            // Return valor original se não for numérico
            return num;
        }
    }
}


