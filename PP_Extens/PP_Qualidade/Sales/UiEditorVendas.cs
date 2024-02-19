using BasBE100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using StdPlatBS100;
using System;
using System.Data;
using VndBE100;


namespace PP_Qualidade.Sales
{
    public class UiEditorVendas : EditorVendas
    {
        HelpersPrimavera10.HelperFunctions _Helpers = new HelpersPrimavera10.HelperFunctions();


        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            string matricula;

            if (!DocumentoVenda.EmModoEdicao)
            {
                bool pedeMatricula = (bool)_Helpers.GetDataTableDeSQL(
                    "SELECT CDU_PedeMatricula " +
                    "FROM SeriesVendas " +
                   $"WHERE TipoDoc = '{DocumentoVenda.Tipodoc}' AND Serie = '{DocumentoVenda.Serie}';")
                    .Rows[0][0];

                if (pedeMatricula)
                {
                    matricula = _Helpers.MostraInputForm("Matricula", "Matrícula da Viatura:", DocumentoVenda.Matricula);

                    if (matricula != DocumentoVenda.Matricula)
                    {
                        DocumentoVenda.Matricula = matricula;
                    }
                }

                #region Check Quantidade e PrecUnit para valores inválidos (0)
                string linhasInvalidas = "";
                if (DocumentoVenda.Linhas.NumItens > 0)
                {
                    for (int i = 1; i <= DocumentoVenda.Linhas.NumItens; i++)
                    {
                        VndBELinhaDocumentoVenda linha = DocumentoVenda.Linhas.GetEdita(i);

                        if (string.IsNullOrEmpty(linha.Artigo) && (linha.Quantidade == 0 || linha.PrecUnit == 0))
                        {
                            linhasInvalidas += i.ToString() + ", ";
                        }
                    }
                }

                StdBSTipos.ResultMsg resultado;
                if (!string.IsNullOrWhiteSpace(linhasInvalidas.Trim()))
                {
                    resultado = PSO.MensagensDialogos.MostraMensagem(
                        StdBSTipos.TipoMsg.PRI_SimNao,
                        "Atenção, há linhas com quantidades ou preços a zero nas linhas:" + Environment.NewLine +
                        linhasInvalidas + Environment.NewLine +
                        "Continuar com a gravação do documento?",
                        StdBSTipos.IconId.PRI_Critico);
                } else
                {
                    resultado = PSO.MensagensDialogos.MostraMensagem(
                        StdBSTipos.TipoMsg.PRI_SimNao,
                        "Confirma a gravação do documento?");
                }

                if (resultado == StdBSTipos.ResultMsg.PRI_Nao) { Cancel = true; return; }
                #endregion

                #region CDU_CodFornecedor
                string codFornecedor;
                bool linhaDescricaoFornecedor = false;
                var cduCodFornecedor = BSO.Base.Clientes.DaValorAtributo(DocumentoVenda.Entidade, "CDU_CodFornecedor");

                if (cduCodFornecedor != null
                    && DocumentoVenda.TipoEntidade == "C"
                    && DocumentoVenda.Linhas.NumItens > 0)
                {
                    codFornecedor = cduCodFornecedor;

                    foreach (VndBELinhaDocumentoVenda linha in DocumentoVenda.Linhas)
                    {
                        if (linha.TipoLinha == "60" && linha.Descricao.Substring(0, 11) == "Fornecedor:")
                        {
                            linhaDescricaoFornecedor = true;
                            break;
                        }
                    }

                    if (linhaDescricaoFornecedor)
                    {
                        codFornecedor = _Helpers.MostraInputForm("Fornecedor", "Código de fornecedor:", codFornecedor);

                        if (!string.IsNullOrWhiteSpace(codFornecedor))
                        {
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: $"Fornecedor: {codFornecedor}");
                        }
                    }
                }
                #endregion

                #region CDU_PedirNEncomenda
                string requisicao;
                bool linhaDescricaoEncomenda = false;

                if ((bool)BSO.Base.Clientes.DaValorAtributo(DocumentoVenda.Entidade, "CDU_PedirNEncomenda")
                    && DocumentoVenda.TipoEntidade == "C"
                    && DocumentoVenda.Linhas.NumItens > 0)
                {
                    requisicao = DocumentoVenda.Requisicao;

                    foreach (VndBELinhaDocumentoVenda linha in DocumentoVenda.Linhas)
                    {
                        if (linha.TipoLinha == "60" && linha.Descricao.Substring(0, 10) == "Encomenda:")
                        {
                            linhaDescricaoEncomenda = true;
                            break;
                        }
                    }

                    if (linhaDescricaoEncomenda)
                    {
                        requisicao = _Helpers.MostraInputForm("N. Encomenda", "Número de encomenda:", "");

                        if (!string.IsNullOrWhiteSpace(requisicao))
                        {
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(
                                DocumentoVenda,
                                BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario,
                                Descricao: "Encomenda: " + requisicao);

                            DocumentoVenda.Requisicao = requisicao;
                        }
                    }
                }
                #endregion

                #region CDU_VerificacoesDocumentos
                string verificacoes = BSO.Base.Clientes.DaValorAtributo(DocumentoVenda.Entidade, "CDU_VerificacoesDocumentos");

                if (string.IsNullOrWhiteSpace(verificacoes))
                {
                    verificacoes = verificacoes.Replace("?", "?" + Environment.NewLine + Environment.NewLine);
                    StdBSTipos.ResultMsg resultado2 = PSO.MensagensDialogos.MostraMensagem(
                        StdBSTipos.TipoMsg.PRI_SimNao,
                        verificacoes,
                        StdBSTipos.IconId.PRI_Exclama);

                    if (resultado2 == StdBSTipos.ResultMsg.PRI_Sim)
                    {
                        Cancel = true;
                        return;
                    }
                }
                #endregion
            }
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            BasBEArtigo art = BSO.Base.Artigos.Edita(Artigo);
            VndBELinhaDocumentoVenda linha = DocumentoVenda.Linhas.GetEdita(NumLinha);

            if ((bool)art.CamposUtil["CDU_VendaEmCaixa"].Valor)
            {
                string kgsPorCaixa = _Helpers.MostraInputForm("Quilos", "Quilos por caixa:", art.CamposUtil["CDU_KilosPorCaixa"].ToString());

                if (decimal.TryParse(kgsPorCaixa, out decimal kilosPorCaixa))
                {
                    linha.CamposUtil["CDU_KilosPorCaixa"].Valor = kilosPorCaixa;
                } else
                {
                    PSO.MensagensDialogos.MostraAviso($"'{kgsPorCaixa}' não é um valor válido.", StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);
                    art.Dispose();
                    Cancel = true;
                    return;
                }

                string caixasNum = _Helpers.MostraInputForm("Caixas", "Nro. de caixas:", "");

                if (int.TryParse(caixasNum, out int caixas))
                {
                    linha.CamposUtil["CDU_Caixas"].Valor = caixas;
                    linha.Quantidade = caixas;
                } else
                {
                    PSO.MensagensDialogos.MostraAviso($"'{caixasNum}' não é um valor válido.", StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);
                    art.Dispose();
                    Cancel = true;
                    return;
                }
            }

            using (DataTable fornecedorTbl = _Helpers.GetDataTableDeSQL(
                $"SELECT CDU_PedeFornecedor" +
                $"FROM SeriesVendas" +
                $"WHERE TipoDoc = '{DocumentoVenda.Tipodoc}' And Serie = '{DocumentoVenda.Serie}'"))
            {
                if (fornecedorTbl.Rows.Count > 0 && (bool)fornecedorTbl.Rows[0]["CDU_PedeFornecedor"] == true)
                {
                    string fornecedor = _Helpers.MostraInputForm("Proveniencia", "Fornecedor:", linha.CamposUtil["CDU_Fornecedor"].Valor.ToString());
                    linha.CamposUtil["CDU_Fornecedor"].Valor = fornecedor.Substring(0, 20);
                }
            }

            if ((bool)art.CamposUtil["CDU_MemorizaLote"].Valor)
            {
                string lote = _Helpers.MostraInputForm("Lote", "Introduza o lote do artigo:", art.CamposUtil["CDU_UltimoLote"].Valor.ToString());
                lote = lote.Trim().ToUpper().PadRight(25);
                linha.CamposUtil["CDU_LoteAux"].Valor = lote;

                if (lote != art.CamposUtil["CDU_UltimoLote"].Valor.ToString())
                {
                    art.CamposUtil["CDU_UltimoLote"].Valor = lote;
                    BSO.Base.Artigos.Actualiza(art);
                }
            }

            art.Dispose();
            linha.CamposUtil["CDU_DescricaoBase"].Valor = DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao;
        }

        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);

            BasBECliente cliente = BSO.Base.Clientes.Edita(Cliente);

            if ((bool)cliente.CamposUtil["CDU_DiversasMoradas"].Valor)
            {
                DataTable moradaTbl = _Helpers.GetDataTableDeSQL(
                    "SELECT MoradaAlternativa, Morada, Localidade, CP, CPLocalidade, Distrito" +
                    "FROM MoradasAlternativasClientes" +
                   $"WHERE Cliente = N'{DocumentoVenda.Entidade}';");

                if (moradaTbl.Rows.Count > 0)
                {
                    string morInput = _Helpers.MostraInputForm("Código da Morada", "Morada do documento:", moradaTbl.Rows[0]["MoradaAlternativa"].ToString());
                    moradaTbl = _Helpers.GetDataTableDeSQL(
                        "SELECT MoradaAlternativa, Morada, Morada2, Localidade, CP, CPLocalidade, Distrito From MoradasAlternativasClientes Where (Cliente = N'" + DocumentoVenda.Entidade + "') And (MoradaAlternativa = N'" + morInput + "');");

                    if (moradaTbl.Rows.Count > 0)
                    {
                        DocumentoVenda.Morada = moradaTbl.Rows[0]["Morada"].ToString();
                        DocumentoVenda.Localidade = moradaTbl.Rows[0]["Localidade"].ToString();
                        DocumentoVenda.Morada2 = moradaTbl.Rows[0]["Morada2"].ToString();
                        DocumentoVenda.CodigoPostal = moradaTbl.Rows[0]["CP"].ToString();
                        DocumentoVenda.LocalidadeCodigoPostal = moradaTbl.Rows[0]["CPLocalidade"].ToString();
                    }
                }
                moradaTbl.Dispose();
                cliente.Dispose();
            }


            DataTable serieTbl = _Helpers.GetDataTableDeSQL(
                "SELECT CDU_PedeVendedor, CDU_PedeDocumento, CDU_PedeMatricula" +
                "FROM SeriesVendas " +
               $"WHERE TipoDoc = '{DocumentoVenda.Tipodoc}' AND Serie = '{DocumentoVenda.Serie}';");

            if (DocumentoVenda.DataHoraDescarga == null)
            {
                DocumentoVenda.DataHoraDescarga = DocumentoVenda.DataHoraCarga.AddSeconds(10);
            }

            if (serieTbl.Rows.Count > 0)
            {
                if ((bool)serieTbl.Rows[0]["CDU_PedeVendedor"])
                {
                    string vendedorInput = _Helpers.MostraInputForm("Vendedor","Código de Vendedor:", "0");
                    DataTable vendedorTbl = _Helpers.GetDataTableDeSQL(
                        $"SELECT Vendedor FROM Vendedores WHERE Vendedor = '{vendedorInput}';");

                    if (vendedorTbl.Rows.Count > 0)
                        DocumentoVenda.Responsavel = vendedorInput;
                    else
                        PSO.MensagensDialogos.MostraAviso($"Vendedor '{vendedorInput}' inexistente!", StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);

                    vendedorTbl.Dispose();
                }

                if ((bool)serieTbl.Rows[0]["CDU_PedeDocumento"])
                {
                    string nrDocInput = _Helpers.MostraInputForm("Documento", "Nº de documento manual:", "");
                    DocumentoVenda.CamposUtil["CDU_NroManual"].Valor = nrDocInput.Trim().Substring(0, 10);
                    DocumentoVenda.DocsOriginais = nrDocInput.Trim().Substring(0, 10);
                }

                if ((bool)serieTbl.Rows[0]["CDU_PedeMatricula"])
                {
                    if (DocumentoVenda.TipoEntidade == "C")
                    {
                        if (cliente.CamposUtil["CDU_MatriculaHabitual"].Valor.ToString().Length > 0 && DocumentoVenda.Matricula == "")
                        {
                            DocumentoVenda.Matricula = cliente.CamposUtil["CDU_MatriculaHabitual"].Valor.ToString();
                        }

                        string matricula = _Helpers.MostraInputForm("Matrícula da viatura:", "Matrícula", DocumentoVenda.Matricula);
                        DocumentoVenda.Matricula = matricula;

                        cliente.Dispose();
                    }
                }
            }
        }

        public override void ArtigoInexistente(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoInexistente(Artigo, NumLinha, ref Cancel, e);

            string artaux;
            string loteaux;

            if (Artigo.StartsWith("#"))
            {
                loteaux = Artigo.Substring(1).ToUpper();
                artaux = BSO.Base.Artigos.DaArtigoComCodBarras(ref loteaux);

                if (BSO.Base.Artigos.Existe(artaux))
                {
                    DataTable origemLoteTbl = _Helpers.GetDataTableDeSQL($"SELECT * FROM usr_Origemdolote('{loteaux}','{artaux}');");

                    if (origemLoteTbl.Rows.Count > 0)
                    {
                        double quantidade = (double)origemLoteTbl.Rows[0]["Quantidade"];
                        string armazem = "", localizacao = "";

                        BSO.Vendas.Documentos.AdicionaLinha(DocumentoVenda, artaux, ref quantidade, ref armazem, ref localizacao, 0, 0, loteaux);
                        DocumentoVenda.Linhas.GetEdita(DocumentoVenda.Linhas.NumItens - 1).CamposUtil["CDU_Pescado"].Valor = true;
                        DocumentoVenda.Linhas.GetEdita(DocumentoVenda.Linhas.NumItens - 1).CamposUtil["CDU_NomeCientifico"].Valor = origemLoteTbl.Rows[0]["NomeCientifico"];
                        DocumentoVenda.Linhas.GetEdita(DocumentoVenda.Linhas.NumItens - 1).CamposUtil["CDU_Origem"].Valor = origemLoteTbl.Rows[0]["Origem"];
                        DocumentoVenda.Linhas.GetEdita(DocumentoVenda.Linhas.NumItens - 1).CamposUtil["CDU_FormaObtencao"].Valor = origemLoteTbl.Rows[0]["FormaObtencao"];
                        DocumentoVenda.Linhas.GetEdita(DocumentoVenda.Linhas.NumItens - 1).CamposUtil["CDU_ZonaFAO"].Valor = origemLoteTbl.Rows[0]["ZonaFAO"];
                        DocumentoVenda.Linhas.GetEdita(DocumentoVenda.Linhas.NumItens - 1).CamposUtil["CDU_Caixas"].Valor = origemLoteTbl.Rows[0]["Caixas"];
                    }
                    origemLoteTbl.Dispose();
                }
                DocumentoVenda.Linhas.Remove(NumLinha - 1);
            }
        }
    }
}
