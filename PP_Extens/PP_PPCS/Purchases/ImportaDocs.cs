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


namespace PP_PPCS
{
    public class ImportaDocs : EditorCompras
    {
        private ErpBS _BSO = Motor.PriEngine.Engine;
        private StdPlatBS _PSO = Motor.PriEngine.Platform;

        public ImportaDocs()
        {
        }

        public void CriarDocumentoCompra(ref DataRow linha, bool Cancel)           
        {
            string localstr = "", SQLErrors = "";
            bool Cancelar = false;
            int vdDadosTodos = (int)BasBETiposGcp.PreencheRelacaoCompras.compDadosTodos;

            string TipoEntidade = linha["TipoEntidade"].ToString();
            string Entidade = linha["Entidade"].ToString();
            string Filial = linha["Filial"].ToString();
            string TipoDoc = linha["TipoDoc"].ToString();
            string Serie = linha["Serie"].ToString();
            int? NumDoc = (int)linha["NumDoc"];
            DateTime DataDoc = Convert.ToDateTime(linha["Data"]);
            string EntLocal = linha["EntLocal"].ToString();
            string FilialDest = linha["FilialLoc"].ToString();
            string TipoDocDest = linha["TipoDocLoc"].ToString();
            string SerieDest = linha["SerieLoc"].ToString();
            int NumDocDest = (int)linha["NumDocLocal"];
            string Importa = linha["Importa"].ToString();

            // Preenchimento do novo documento de compra
            if (Importa == "A" && _BSO.Compras.Documentos.Existe(FilialDest, TipoDocDest, SerieDest, (int)NumDocDest) == true) {
                CmpBEDocumentoCompra docNovo = new CmpBEDocumentoCompra();
                docNovo = _BSO.Compras.Documentos.Edita(FilialDest, TipoDocDest, SerieDest, (int)NumDocDest);
                docNovo.DataDoc = DataDoc;
                docNovo.Entidade = EntLocal;
                if (docNovo.Linhas.NumItens > 0) { docNovo.Linhas.RemoveTodos(); }

                // Linhas de comentário adicionadas ao novo documento de compra
                _BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, Descricao: @"
                    Este documento é replicação do documento físico " + TipoDoc + " Nº " + NumDoc.ToString() + "/" + Serie + ".");
                _BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, Descricao: @"
                    Cópia do documento original.");
                _BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, Descricao: @"
                    Documento original anulado!");

                _BSO.Compras.Documentos.PreencheDadosRelacionados(docNovo);
                _BSO.Compras.Documentos.Actualiza(docNovo);

                docNovo.Dispose();
            } else if (Importa == "S") {
                // Verificar se a entidade fornecedor existe
                if (!_BSO.Base.Fornecedores.Existe(EntLocal)) {
                    string docFull = TipoDoc + " N.º " + NumDoc.ToString() + "/" + Serie;
                    localstr = Microsoft.VisualBasic.Interaction.InputBox(
                        "A entidade (" + Entidade + ") no documento " + docFull + ".\n" +
                        "Não existe ou não possui correspondencia.\n\n" +
                        "Qual a entidade a utilizar?", Title: docFull);

                    if (_BSO.Base.Fornecedores.Existe(localstr)) {
                        EntLocal = localstr;

                        string query = string.Format(@"
                            UPDATE TDU_CorrespondenciaEntidades 
                            SET CDU_EntLocal = {0}
                            WHERE CDU_TipoEntidade = {1}
                                AND CDU_EntERP = {2}", EntLocal, TipoEntidade, Entidade);

                        _PSO.ExecSql.ExecutaSP("query", ref SQLErrors);
                    } else {
                        EntLocal = "";
                    }
                }

                // Controlar a existência de entidade e série do documento
                if (EntLocal != "" && _BSO.Base.Series.Existe("C", TipoDoc, Serie)) {
                    CmpBEDocumentoCompra docNovo = new CmpBEDocumentoCompra();
                    StdBELista RSet = new StdBELista();

                    // Verificar existência do documento de destino
                    localstr = FilialDest + SerieDest;

                    if (!string.IsNullOrEmpty(localstr) && NumDocDest != null) {

                        // Documento já existente, verificar a existência e editar se existir
                        if (_BSO.Compras.Documentos.Existe(FilialDest, TipoDocDest, SerieDest, (int)NumDocDest)) {

                            docNovo = _BSO.Compras.Documentos.Edita(FilialDest, TipoDocDest, SerieDest, (int)NumDocDest);

                            RSet = _BSO.Consulta(QueriesSQL.GetQuery04(Filial, TipoDoc, Serie, NumDoc.ToString()));
                            docNovo.DataDoc = DataDoc;
                            docNovo.Entidade = EntLocal;
                            docNovo.DescFornecedor = RSet.Valor("DescEntidade");
                            docNovo.DescFinanceiro = RSet.Valor("DescPag");

                            RSet.Termina();
                            if (docNovo.Linhas.NumItens > 0) { docNovo.Linhas.RemoveTodos(); }
                        } else { return; }
                    } else {

                        RSet = _BSO.Consulta(QueriesSQL.GetQuery04(Filial, TipoDoc, Serie, NumDoc.ToString()));
                        docNovo.Filial = Filial;
                        docNovo.Serie = Serie;
                        docNovo.Tipodoc = TipoDoc;
                        docNovo.TipoEntidade = TipoEntidade;
                        docNovo.Entidade = EntLocal;
                        docNovo.CamposUtil["CDU_FilialOrig"].Valor = Filial;
                        docNovo.CamposUtil["CDU_TipoDocOrig"].Valor = TipoDoc;
                        docNovo.CamposUtil["CDU_SerieOrig"].Valor = Serie;
                        docNovo.CamposUtil["CDU_NumDocOrig"].Valor = NumDoc;

                        
                        _BSO.Compras.Documentos.PreencheDadosRelacionados(docNovo, ref vdDadosTodos);

                        docNovo.DataDoc = DataDoc;
                        if (docNovo.DataIntroducao < DataDoc) { docNovo.DataIntroducao = DataDoc; }
                        if (docNovo.DataVenc < DataDoc) { docNovo.DataVenc = DataDoc; }

                        docNovo.DescFornecedor = RSet.Valor("DescEntidade");
                        docNovo.DescFinanceiro = RSet.Valor("DescPag");
                        docNovo.TrataIvaCaixa = false;

                        RSet.Termina();
                    }

                    RSet = _BSO.Consulta(QueriesSQL.GetQuery05(Filial, TipoDoc, Serie, NumDoc.ToString()));
                    docNovo.NumDocExterno = TipoDoc + " Nº " + NumDoc.ToString() + "/" + Serie + " - " + RSet.Valor("NumDocExterno");

                    RSet = _BSO.Consulta(QueriesSQL.GetQuery06(Filial, TipoDoc, Serie, NumDoc.ToString()));

                    while (!RSet.NoFim()) {

                        int numLinhas = docNovo.Linhas.NumItens;
                        CmpBELinhaDocumentoCompra ultimaLinha;
                        double quant = Math.Abs((double)RSet.Valor("Quantidade"));

                        if (!string.IsNullOrEmpty(RSet.Valor("ArtigoDestino")) && _BSO.Base.Artigos.Existe(RSet.Valor("ArtigoDestino"))) {

                            // Actualizar sempre numLinhas e ultimaLinha quando se adiciona qualquer linha
                            // Se a certo ponto ficar demasiado dificil de seguir, encapsular os três.
                            _BSO.Compras.Documentos.AdicionaLinha(
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
                                _BSO.Base.Iva.DaValorAtributo("6", "Taxa")
                                );

                            ultimaLinha = docNovo.Linhas.GetEdita(docNovo.Linhas.NumItens);

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
                                        _PSO.MensagensDialogos.MostraAviso($"Não é possível converter o artigo {RSet.Valor("Artigo")} em {RSet.Valor("ArtigoDestino")}.\nO documento não será importado.", StdBSTipos.IconId.PRI_Exclama);
                                    }
                                    Cancelar = true;
                                }
                            }
                        } else if (_BSO.Base.Artigos.Existe(RSet.Valor("Artigo"))) {
                            _BSO.Compras.Documentos.AdicionaLinha(
                                docNovo,
                                RSet.Valor("Artigo"),
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
                                _BSO.Base.Iva.DaValorAtributo("6", "Taxa")
                                );

                            ultimaLinha = docNovo.Linhas.GetEdita(docNovo.Linhas.NumItens);

                            ultimaLinha.CamposUtil["CDU_Caixas"] = RSet.Valor("CDU_Caixas");
                            ultimaLinha.CamposUtil["CDU_Pescado"] = RSet.Valor("CDU_Pescado");
                            ultimaLinha.CamposUtil["CDU_NomeCientifico"] = RSet.Valor("CDU_NomeCientifico");
                            ultimaLinha.CamposUtil["CDU_Origem"] = RSet.Valor("CDU_Origem");
                            ultimaLinha.CamposUtil["CDU_FormaObtencao"] = RSet.Valor("CDU_FormaObtencao");
                            ultimaLinha.CamposUtil["CDU_ZonaFAO"] = RSet.Valor("CDU_ZonaFAO");
                            ultimaLinha.CamposUtil["CDU_VendaEmCaixa"] = RSet.Valor("CDU_VendaEmCaixa");
                            ultimaLinha.CamposUtil["CDU_KilosPorCAixa"] = RSet.Valor("CDU_KilosPorCaixa");

                        } else if (RSet.Valor("Artigo") is null) {
                            _BSO.Compras.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario);
                        } else {
                            _PSO.MensagensDialogos.MostraAviso(
                                $"ATENÇÃO!\n\nO artigo {RSet.Valor("Artigo")} não esxiste na base de dados.\nCrie o artigo e volte a importar o documento.",
                                StdBSTipos.IconId.PRI_Exclama,
                                $"Artigo: {RSet.Valor("Artigo")} - {RSet.Valor("Descricao")}");
                            Cancel = true;
                            return;
                        }

                        RSet.Seguinte();
                    } // end while

                    if (docNovo.Linhas.NumItens > 0 && !Cancelar) {

                        string numdoc = NumDoc.HasValue ? NumDoc.Value.ToString() : null;

                        _BSO.Compras.Documentos.AdicionaLinhaEspecial( docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, 0,
                            $"Este documento é replicação do documento fisico {TipoDoc} N.º {NumDoc.ToString() ?? null}/{Serie}.");

                        _BSO.Compras.Documentos.AdicionaLinhaEspecial( docNovo, BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario, 0, "Cópia do documento original");

                        docNovo.TrataIvaCaixa = false;
                        docNovo.CamposUtil["CDU_NumDocOrig"].Valor = numdoc;
                        _BSO.Compras.Documentos.Actualiza(docNovo);

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
            return;
        }

        public void CriarDocumentoVenda(ref DataRow linha, bool Cancel)
        {

            VndBEDocumentoVenda docNovo = new VndBEDocumentoVenda();
            CctBEDocumentoLiq docLiq = new CctBEDocumentoLiq();
            StdBELista RSet = new StdBELista();
            int vdDadosTodos = (int)BasBETiposGcp.PreencheRelacaoCompras.compDadosTodos;

            bool Cancelar, ivaIncluido, DocDestinoJaExiste, liqDocAnulada, fazerLiquidacao;
            string fl = "", tdl = "", sl = "";
            int ndl = 0;

            string TipoEntidade = linha["TipoEntidade"].ToString();
            string Entidade = linha["Entidade"].ToString();
            string Filial = linha["Filial"].ToString();
            string TipoDoc = linha["TipoDoc"].ToString();
            string Serie = linha["Serie"].ToString();
            int NumDoc = (int)linha["NumDoc"];
            DateTime DataDoc = Convert.ToDateTime(linha["Data"]);
            string EntLocal = linha["EntLocal"].ToString();
            string FilialDest = linha["FilialLoc"].ToString();
            string TipoDocDest = linha["TipoDocLoc"].ToString();
            string SerieDest = linha["SerieLoc"].ToString();
            int NumDocDest = (int)linha["NumDocLocal"];
            string Importa = linha["Importa"].ToString();

            docNovo = _BSO.Vendas.Documentos.Edita(FilialDest, TipoDocDest, SerieDest, NumDocDest);
            Cancelar = false;

            switch (Importa) {
                case "A": // Anular o documento de destino
                    if (_BSO.Vendas.Documentos.Existe(FilialDest, TipoDocDest, SerieDest, NumDocDest)) {
                        // A próxima linha preenche as quatro variáveis dadas por referência. Usadas logo a seguir para anular a liquidação.
                        if (_BSO.PagamentosRecebimentos.Liquidacoes.DaDocLiquidacao(FilialDest, "V", TipoDocDest, SerieDest, NumDocDest, ref fl, ref tdl, ref sl, ref ndl)) {

                            // Anular a liquidação do documento
                            _BSO.PagamentosRecebimentos.Liquidacoes.Remove(fl, tdl, sl, ndl);

                            #region PossivelBugFix
                            // No código da V9 existe um bloco aqui que corrige um suposto bug que não actualiza o numerador dos documentos.
                            // Query original traduzida: $"UPDATE SeriesCCT SET Numerador = (SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'";
                            // Descomentar as próximas linhas se necessário.

                            //// Como o Update actua sob um Select e temos de usar o StdBEExecSql, vamos primeiro usar _BSO.Consulta (Select) e usar o resultado no Update.
                            //StdBELista subSelect = _BSO.Consulta($"SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'");

                            //using (StdBEExecSql sql = new StdBEExecSql()) {
                            //    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                            //    sql.Tabela = "SeriesCCT";
                            //    sql.AddCampo("Numerador", subSelect.Valor(0));
                            //    sql.AddQuery();

                            //    _PSO.ExecSql.Executa(sql);
                            //}
                            #endregion
                        }

                        docNovo = _BSO.Vendas.Documentos.Edita(FilialDest, TipoDocDest, SerieDest, NumDocDest);

                        docNovo.DataDoc = DataDoc;
                        docNovo.Entidade = EntLocal;

                        if (docNovo.Linhas.NumItens > 0) { docNovo.Linhas.RemoveTodos(); }

                        // Linhas de comentário adicionadas ao novo documento de compra
                        _BSO.Vendas.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: @"
                            Este documento é replicação do documento físico " + TipoDoc + " Nº " + NumDoc.ToString() + "/" + Serie + ".");
                        _BSO.Vendas.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: "Cópia do documento original.");
                        _BSO.Vendas.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: "Documento original anulado!");

                        _BSO.Vendas.Documentos.Actualiza(docNovo);
                        docNovo.Dispose();

                        if (_BSO.PagamentosRecebimentos.Liquidacoes.DaDocLiquidacao(FilialDest, "V", TipoDocDest, SerieDest, NumDocDest, ref fl, ref tdl, ref sl, ref ndl)) {
                            //' O doc. venda acabado de gravar fez liquidação automática.
                            //' Garantir que o numerador da série do doc. liquidação criado fica correto (parece ser um bug do Primavera):
                            #region PossivelBugFix
                            // No código da V9 existe um bloco aqui que corrige um suposto bug que não actualiza o numerador dos documentos.
                            // Query original traduzida: $"UPDATE SeriesCCT SET Numerador = (SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'";
                            // Descomentar as próximas linhas se necessário.

                            //// Como o Update actua sob um Select e temos de usar o StdBEExecSql, vamos primeiro usar _BSO.Consulta (Select) e usar o resultado no Update.
                            //StdBELista subSelect = _BSO.Consulta($"SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'");

                            //using (StdBEExecSql sql = new StdBEExecSql()) {
                            //    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                            //    sql.Tabela = "SeriesCCT";
                            //    sql.AddCampo("Numerador", subSelect.Valor(0));
                            //    sql.AddQuery();

                            //    _PSO.ExecSql.Executa(sql);
                            //}
                            #endregion
                        }
                    }
                    break;

                case "S": // Importar o documento

                    // Verificar se a entidade já existe
                    if (!_BSO.Base.Clientes.Existe(EntLocal)) {
                        _PSO.MensagensDialogos.MostraAviso($"A entidade {Entidade} no documento {TipoDoc} N.º{NumDoc}/{Serie} não possui entidade local correspondente." +
                            $"Este documento não vai ser importado!", StdBSTipos.IconId.PRI_Critico);
                        Cancel = true;
                    } else {

                        if (!_BSO.Base.Series.Existe("V", TipoDocDest, Serie)) {
                            _PSO.MensagensDialogos.MostraAviso($"A série do documento {TipoDoc} N.º{NumDoc}/{Serie} não está criada localmente." +
                            $"Este documento não vai ser importado!", StdBSTipos.IconId.PRI_Critico);
                            Cancel = true;
                        } else {
                            // Verificar se documento de destino existe
                            if (_BSO.Vendas.Documentos.Existe(FilialDest, TipoDocDest, SerieDest, NumDocDest)) {

                                DocDestinoJaExiste = true;
                                liqDocAnulada = false;

                                if (_BSO.PagamentosRecebimentos.Liquidacoes.DaDocLiquidacao(FilialDest, "V", TipoDocDest, SerieDest, NumDocDest, ref fl, ref tdl, ref sl, ref ndl)) {
                                    // Anular a liquidação do documento
                                    _BSO.PagamentosRecebimentos.Liquidacoes.Remove(fl, tdl, sl, ndl);
                                    liqDocAnulada = true;

                                    #region PossivelBugFix
                                    // No código da V9 existe um bloco aqui que corrige um suposto bug que não actualiza o numerador dos documentos.
                                    // Query original traduzida: $"UPDATE SeriesCCT SET Numerador = (SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'";
                                    // Descomentar as próximas linhas se necessário.

                                    //// Como o Update actua sob um Select e temos de usar o StdBEExecSql, vamos primeiro usar _BSO.Consulta (Select) e usar o resultado no Update.
                                    //StdBELista subSelect = _BSO.Consulta($"SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'");

                                    //using (StdBEExecSql sql = new StdBEExecSql()) {
                                    //    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                                    //    sql.Tabela = "SeriesCCT";
                                    //    sql.AddCampo("Numerador", subSelect.Valor(0));
                                    //    sql.AddQuery();

                                    //    _PSO.ExecSql.Executa(sql);
                                    //}
                                    #endregion
                                }

                                docNovo = new VndBEDocumentoVenda();
                                RSet = _BSO.Consulta(QueriesSQL.GetQuery07(Filial, TipoDoc, Serie, NumDoc.ToString()));

                                docNovo.DataDoc = DataDoc;
                                docNovo.Entidade = EntLocal;
                                docNovo.DescEntidade = RSet.Valor("DescEntidade");
                                docNovo.DescFinanceiro = RSet.Valor("DescPag");
                                docNovo.Responsavel = RSet.Valor("RespCobranca");

                                RSet.Dispose();

                                if (docNovo.Linhas.NumItens > 0) { docNovo.Linhas.RemoveTodos(); }
                            } else {

                                DocDestinoJaExiste = false;

                                docNovo.Tipodoc = TipoDocDest;
                                docNovo.Filial = Filial;
                                docNovo.Serie = Serie;
                                docNovo.TipoEntidade = TipoEntidade;
                                docNovo.Entidade = EntLocal;
                                docNovo.CamposUtil["CDU_FilialOrig"].Valor = Filial;
                                docNovo.CamposUtil["CDU_TipoDocOrig"].Valor = TipoDoc;
                                docNovo.CamposUtil["CDU_SerieOrig"].Valor = Serie;
                                docNovo.CamposUtil["CDU_NumDocOrig"].Valor = NumDoc;

                                _BSO.Vendas.Documentos.PreencheDadosRelacionados(docNovo, ref vdDadosTodos);

                                docNovo.DataDoc = DataDoc;
                                docNovo.DescEntidade = RSet.Valor("DescEntidade");
                                docNovo.DescFinanceiro = RSet.Valor("DescPag");
                                docNovo.Responsavel = RSet.Valor("RespCobranca");
                                docNovo.TrataIvaCaixa = false;

                                if (docNovo.DataVenc < docNovo.DataDoc) { docNovo.DataVenc = docNovo.DataDoc; }

                                RSet.Termina();
                            }

                            // Verificar se o documento original é com iva incluido
                            ivaIncluido = _BSO.Consulta(QueriesSQL.GetQuery08(TipoDoc, Serie)).Valor("IvaIncluido");
                            
                            RSet = _BSO.Consulta(QueriesSQL.GetQuery09(Filial, TipoDoc, Serie, NumDoc.ToString()));

                            VndBELinhaDocumentoVenda ultimaLinha;
                            double quant = Math.Abs((double)RSet.Valor("Quantidade"));

                            while (!RSet.NoFim() && !Cancelar) {
                                if (!string.IsNullOrEmpty(RSet.Valor("ArtigoDestino")) && _BSO.Base.Artigos.Existe(RSet.Valor("ArtigoDestino"))) {

                                    _BSO.Vendas.Documentos.AdicionaLinha(
                                        docNovo,
                                        RSet.Valor("ArtigoDestino"),
                                        ref quant,
                                        RSet.Valor("Armazem"),
                                        RSet.Valor("Localizacao"),
                                        RSet.Valor("PrecUnit"),
                                        RSet.Valor("Desconto1"),
                                        null, 0, 0, 0,
                                        RSet.Valor("DescEntidade"),
                                        RSet.Valor("DescPag"),
                                        0, 0, false, ivaIncluido);

                                    ultimaLinha = docNovo.Linhas.GetEdita(docNovo.Linhas.NumItens);

                                    ultimaLinha.CamposUtil["CDU_Pescado"].Valor = RSet.Valor("CDU_Pescado");
                                    ultimaLinha.CamposUtil["CDU_NomeCientifico"].Valor = RSet.Valor("CDU_NomeCientifico");
                                    ultimaLinha.CamposUtil["CDU_Origem"].Valor = RSet.Valor("CDU_Origem");
                                    ultimaLinha.CamposUtil["CDU_FormaObtencao"].Valor = RSet.Valor("CDU_FormaObtencao");
                                    ultimaLinha.CamposUtil["CDU_ZonaFAO"].Valor = RSet.Valor("CDU_ZonaFAO");
                                    ultimaLinha.CamposUtil["CDU_Caixas"].Valor = RSet.Valor("CDU_Caixas");
                                    ultimaLinha.CamposUtil["CDU_VendaEmCaixa"].Valor = RSet.Valor("CDU_VendaEmCaixa");
                                    ultimaLinha.CamposUtil["CDU_KilosPorCaixa"].Valor = RSet.Valor("CDU_KilosPorCaixa");
                                    ultimaLinha.CamposUtil["CDU_Fornecedor"].Valor = RSet.Valor("CDU_Fornecedor");

                                    if (ultimaLinha.Unidade != RSet.Valor("Unidade")) {

                                        double kilosPorCaixa = (double)RSet.Valor("CDU_KilosPorCaixa");
                                        if (ultimaLinha.Unidade == "KG" && RSet.Valor("Unidade") == "CX" && kilosPorCaixa != 0) {

                                            ultimaLinha.Quantidade = ultimaLinha.Quantidade * kilosPorCaixa;
                                            ultimaLinha.PrecUnit = ultimaLinha.PrecUnit / kilosPorCaixa;
                                            ultimaLinha.CamposUtil["CDU_VendaEmCaixa"].Valor = 0;
                                        } else {
                                            if (!Cancelar) {
                                                _PSO.MensagensDialogos.MostraAviso($"Não é possível converter o artigo {RSet.Valor("Artigo")} em {RSet.Valor("ArtigoDestino")}. \n\nO documento não será importado!", StdBSTipos.IconId.PRI_Critico);
                                            }
                                            Cancelar = true;
                                        }
                                    }
                                } else if (!string.IsNullOrEmpty(RSet.Valor("Artigo")) && _BSO.Base.Artigos.Existe(RSet.Valor("Artigo"))) {

                                    _BSO.Vendas.Documentos.AdicionaLinha(
                                    docNovo,
                                    RSet.Valor("ArtigoDestino"),
                                    ref quant,
                                    RSet.Valor("Armazem"),
                                    RSet.Valor("Localizacao"),
                                    RSet.Valor("PrecUnit"),
                                    RSet.Valor("Desconto1"),
                                    "", 0, 0, 0,
                                    RSet.Valor("DescEntidade"),
                                    RSet.Valor("DescPag"),
                                    0, 0, false, ivaIncluido);

                                    ultimaLinha = docNovo.Linhas.GetEdita(docNovo.Linhas.NumItens);

                                    ultimaLinha.CamposUtil["CDU_Pescado"].Valor = RSet.Valor("CDU_Pescado");
                                    ultimaLinha.CamposUtil["CDU_NomeCientifico"].Valor = RSet.Valor("CDU_NomeCientifico");
                                    ultimaLinha.CamposUtil["CDU_Origem"].Valor = RSet.Valor("CDU_Origem");
                                    ultimaLinha.CamposUtil["CDU_FormaObtencao"].Valor = RSet.Valor("CDU_FormaObtencao");
                                    ultimaLinha.CamposUtil["CDU_ZonaFAO"].Valor = RSet.Valor("CDU_ZonaFAO");
                                    ultimaLinha.CamposUtil["CDU_Caixas"].Valor = RSet.Valor("CDU_Caixas");
                                    ultimaLinha.CamposUtil["CDU_VendaEmCaixa"].Valor = RSet.Valor("CDU_VendaEmCaixa");
                                    ultimaLinha.CamposUtil["CDU_KilosPorCaixa"].Valor = RSet.Valor("CDU_KilosPorCaixa");
                                    ultimaLinha.CamposUtil["CDU_Fornecedor"].Valor = RSet.Valor("CDU_Fornecedor");
                                } else if (string.IsNullOrEmpty(RSet.Valor("Artigo"))) {

                                    _BSO.Vendas.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: "");
                                } else {

                                    _PSO.MensagensDialogos.MostraAviso($"Atenção!\n\nO artigo {RSet.Valor("Artigo")} não existe na base de dados.\nCrie o artigo e volte a importar o documento.", StdBSTipos.IconId.PRI_Exclama);
                                    Cancelar = true;
                                }
                                RSet.Seguinte();
                            } // End Loop RSet

                            RSet.Dispose();
                            fazerLiquidacao = false;

                            if (Cancelar) {
                                // O documento não vai ser gravado e a liquidação é reactivada
                                if (DocDestinoJaExiste && fazerLiquidacao) { fazerLiquidacao = true; }
                                Cancel = true;
                            } else if (DocDestinoJaExiste || docNovo.Linhas.NumItens > 0) {
                                _BSO.Vendas.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario,
                                    Descricao: $"Este documento é replicação do documento fisico {TipoDoc} N.º {NumDoc}/{Serie}.");
                                _BSO.Vendas.Documentos.AdicionaLinhaEspecial(docNovo, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario,
                                    Descricao: "Cópia do documento original");

                                // GRAVAR O DOCUMENTO DE VENDA
                                _BSO.Vendas.Documentos.Actualiza(docNovo);

                                EntLocal = docNovo.Entidade;
                                FilialDest = docNovo.Filial;
                                TipoDocDest = docNovo.Tipodoc;
                                SerieDest = docNovo.Serie;
                                NumDocDest = docNovo.NumDoc;
                                DataDoc = docNovo.DataDoc;
                                Importa = "N";

                                if (_BSO.PagamentosRecebimentos.Liquidacoes.DaDocLiquidacao(FilialDest, "V", TipoDocDest, SerieDest, NumDocDest, ref fl, ref tdl, ref sl, ref ndl)) {
                                    // O documento acabado de gravar fez liquidação automática.
                                    #region PossivelBugFix
                                    // No código da V9 existe um bloco aqui que corrige um suposto bug que não actualiza o numerador dos documentos.
                                    // Query original traduzida: $"UPDATE SeriesCCT SET Numerador = (SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'";
                                    // Descomentar as próximas linhas se necessário.

                                    //// Como o Update actua sob um Select e temos de usar o StdBEExecSql, vamos primeiro usar _BSO.Consulta (Select) e usar o resultado no Update.
                                    //StdBELista subSelect = _BSO.Consulta($"SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'");

                                    //using (StdBEExecSql sql = new StdBEExecSql()) {
                                    //    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                                    //    sql.Tabela = "SeriesCCT";
                                    //    sql.AddCampo("Numerador", subSelect.Valor(0));
                                    //    sql.AddQuery();

                                    //    _PSO.ExecSql.Executa(sql);
                                    //}
                                    #endregion
                                } else {
                                    fazerLiquidacao = true;
                                }
                            }
                            docNovo.Dispose();

                            if (fazerLiquidacao && _BSO.PagamentosRecebimentos.Pendentes.Existe(FilialDest, "V", TipoDocDest, SerieDest, NumDocDest)) {
                                // Liquidar o documento acabado de criar/alterar
                                docLiq = new CctBEDocumentoLiq();

                                docLiq.Tipodoc = "VDR";
                                docLiq.Serie = (_BSO.Base.Series.Existe("M", docLiq.Tipodoc, SerieDest)) ? SerieDest : _BSO.Base.Series.DaSerieDefeito("M", docLiq.Tipodoc, DataDoc);
                                docLiq.TipoEntidade = "C";
                                docLiq.Entidade = EntLocal;

                                int cctDadosTodos = (int)BasBETiposGcp.PreencheRelacaoCCT.cctDadosTodos;
                                _BSO.PagamentosRecebimentos.Liquidacoes.PreencheDadosRelacionados(docLiq, ref cctDadosTodos);

                                docLiq.DataDoc = DataDoc;

                                double x = 1;
                                _BSO.PagamentosRecebimentos.Liquidacoes.AdicionaLinha(
                                    docLiq, FilialDest, "V", TipoDocDest, SerieDest, NumDocDest, 1, "PEN", 0,
                                    _BSO.PagamentosRecebimentos.Pendentes.DaValorAtributo("V", TipoDocDest, NumDocDest, 1, SerieDest, FilialDest, "PEN", 0, "ValorPendente"), ref x, 0);

                                #region PossivelBugFix
                                // No código da V9 existe um bloco aqui que corrige um suposto bug que não actualiza o numerador dos documentos.
                                // Query original traduzida: $"UPDATE SeriesCCT SET Numerador = (SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'";
                                // Descomentar as próximas linhas se necessário.

                                //// Como o Update actua sob um Select e temos de usar o StdBEExecSql, vamos primeiro usar _BSO.Consulta (Select) e usar o resultado no Update.
                                //StdBELista subSelect = _BSO.Consulta($"SELECT Max(NumDoc) FROM CabLiq WHERE Filial = N'{fl}' AND TipoDoc = N'{tdl}' AND Serie = N'{sl})'");

                                //using (StdBEExecSql sql = new StdBEExecSql()) {
                                //    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                                //    sql.Tabela = "SeriesCCT";
                                //    sql.AddCampo("Numerador", subSelect.Valor(0));
                                //    sql.AddQuery();

                                //    _PSO.ExecSql.Executa(sql);
                                //}
                                #endregion
                            }
                        }
                    }
                    break;
            }
        }
    }
}


