using BasBE100;
using HelpersPrimavera10;
using IntBE100;
using InvBE100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Internal.Editors;
using Primavera.Extensibility.Internal.Editors.Details;
using Primavera.Extensibility.Platform.Services;
using StdBE100;
using StdPlatBS100;
using System;
using System.Collections.Generic;
using System.Data;


namespace PP_Qualidade.Internal
{
    public class UiEditorInternos : EditorInternos
    {
        HelperFunctions _Helpers = new HelperFunctions();

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (DocumentoInterno.Linhas.NumItens <= 0) { return; }

            #region Get Primeira Linha para o próximo processo
            int primeiraLinhaInt = 0;
            for (int i = 1; i <= DocumentoInterno.Linhas.NumItens; i++)
            {
                if (BSO.Base.Artigos.Existe(DocumentoInterno.Linhas.GetEdita(i).Artigo))
                {
                    primeiraLinhaInt = i;
                    break;
                }
            }

            if (primeiraLinhaInt == 0) { primeiraLinhaInt = 1; }
            #endregion

            if (!DateTime.TryParse(DocumentoInterno.Linhas.GetEdita(primeiraLinhaInt).CamposUtil["CDU_Validade"].Valor.ToString(), out DateTime result))
            {
                PSO.MensagensDialogos.MostraErro($"Validade do produto na linha {primeiraLinhaInt} não está preenchida.");
                Cancel = true;
            }

            double cxe = 0, cxs = 0, kge = 0, kgs = 0;
            string erro = "";
            IntBELinhaDocumentoInterno primeiraLinha = DocumentoInterno.Linhas.GetEdita(primeiraLinhaInt);

            for (int i = primeiraLinhaInt; i < DocumentoInterno.Linhas.NumItens; i++)
            {
                if (!(bool)DocumentoInterno.Linhas.GetEdita(i).CamposUtil["CDU_Pescado"].Valor)  continue; 

                IntBELinhaDocumentoInterno iLinha = DocumentoInterno.Linhas.GetEdita(i);
                double quantidade = iLinha.Quantidade;
                string unidade = iLinha.Unidade;

                if (quantidade > 0)
                {
                    cxe += (double)iLinha.CamposUtil["CDU_Caixas"].Valor;

                    if (unidade == "KG")
                        kge += Math.Round(quantidade, 2);
                    else if (unidade == "CX")
                        kge += Math.Round(quantidade * (double)iLinha.CamposUtil["CDU_KilosPorCaixa"].Valor, 2);
                } else
                {
                    cxs += (double)iLinha.CamposUtil["CDU_Caixas"].Valor;

                    if (unidade == "KG")
                        kgs -= Math.Round(quantidade, 2);
                    else if (unidade == "CX")
                        kgs -= Math.Round(iLinha.Quantidade * (double)iLinha.CamposUtil["CDU_KilosPorCaixa"].Valor, 2);
                }

                if (!(bool)primeiraLinha.CamposUtil["CDU_ProdTransformado"].Valor)
                {
                    if (primeiraLinha.CamposUtil["CDU_Origem"].Valor != iLinha.CamposUtil["CDU_Origem"].Valor)
                        erro += $"Inconsistencia de origem na linha {i}.";

                    if (primeiraLinha.CamposUtil["CDU_FormaObtencao"].Valor != iLinha.CamposUtil["CDU_FormaObtencao"].Valor)
                        erro += $"Inconsistencia da forma de obtenção na linha {i}.";

                    if (primeiraLinha.CamposUtil["CDU_ZonaFAO"].Valor != iLinha.CamposUtil["CDU_ZonaFAO"].Valor)
                        erro += $"Inconsistencia da zona de captura na linha {i}.";
                }
            }

            StdPlatBS100.StdBSTipos.ResultMsg resposta;
            if (string.IsNullOrEmpty(erro))
                resposta = PSO.MensagensDialogos.MostraMensagem(StdPlatBS100.StdBSTipos.TipoMsg.PRI_SimNao,
                    $"Resumo das quantidades no documento:{Environment.NewLine}" +
                    $"Produto acabado:{Environment.NewLine}" +
                    $"\t{cxe} Caixas;{Environment.NewLine}" +
                    $"\t{kge} Kilos.{Environment.NewLine}" +
                    $"Produto consumido:{Environment.NewLine}" +
                    $"\t{cxs} Caixas;{Environment.NewLine}" +
                    $"\t{kgs} Kilos.{Environment.NewLine}{Environment.NewLine}" +
                    $"Confirma?",
                    StdPlatBS100.StdBSTipos.IconId.PRI_Informativo);
            else
                resposta = PSO.MensagensDialogos.MostraMensagem(StdPlatBS100.StdBSTipos.TipoMsg.PRI_SimNao,
                     $"O documento pode não estar construído correctamente:{Environment.NewLine}{erro}{Environment.NewLine}" +
                     $"Resumo das quantidades no documento:{Environment.NewLine}" +
                     $"Produto acabado:{Environment.NewLine}" +
                     $"\t{cxe} Caixas;{Environment.NewLine}" +
                     $"\t{kge} Kilos.{Environment.NewLine}" +
                     $"Produto consumido:{Environment.NewLine}" +
                     $"\t{cxs} Caixas;{Environment.NewLine}" +
                     $"\t{kgs} Kilos.{Environment.NewLine}{Environment.NewLine}" +
                     $"Confirma?",
                     StdPlatBS100.StdBSTipos.IconId.PRI_Informativo);

            if (resposta != StdPlatBS100.StdBSTipos.ResultMsg.PRI_Sim)
                Cancel = true;
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Tipo != "PA") return;

            #region Tratar Lotes Novos de DocInterno
            if (DocumentoInterno.Linhas.NumItens == 0) return;

            for (int i = 1; i > DocumentoInterno.Linhas.NumItens; i++) 
            {
                IntBELinhaDocumentoInterno linha = DocumentoInterno.Linhas.GetEdita(i);
                if (linha.TipoLinha != "10") continue;
                if (!
                    (linha.Lote == "L01"
                    && BSO.Base.Artigos.DaValorAtributo(linha.Artigo, "MovStock") == "S"
                    && BSO.Base.Artigos.DaValorAtributo(linha.Artigo, "TratamentoLotes")
                    && BSO.Base.Artigos.DaValorAtributo(linha.Artigo, "CDU_Pescado")))
                {
                    continue;
                }

                if (!BSO.Inventario.ArtigosLotes.Existe(linha.Artigo, $"{Tipo}{Serie}/{NumDoc}.{i}"))
                {
                    InvBEArtigoLote lote = new InvBEArtigoLote()
                    {
                        Artigo = linha.Artigo,
                        Lote = $"{Tipo}{Serie}/{NumDoc}.{i}",
                        Descricao = $"Lote {NumDoc}.{i}",
                        DataFabrico = linha.DataEntrega,
                        Activo = true
                    };
                    if (DateTime.TryParse(linha.CamposUtil["CDU_Validade"].Valor.ToString(), out DateTime data)) { lote.Validade = data; }

                    BSO.Inventario.ArtigosLotes.Actualiza(lote);
                }

                if (!BSO.Base.ArtigosCodBarras.Existe(linha.Artigo, linha.Unidade))
                {
                    BasBEArtigoCodBarra cbar = new BasBEArtigoCodBarra()
                    {
                        Artigo = linha.Artigo,
                        CodBarras = $"{Tipo}{Serie}/{NumDoc}.{i}",
                        Unidade = linha.Unidade
                    };

                    BSO.Base.ArtigosCodBarras.Actualiza(cbar);
                }

                _Helpers.QuerySQL(
                    $"UPDATE LinhasInternos" +
                    $"SET Lote = '{Tipo}{Serie}/{NumDoc}.{i}'" +
                    $"WHERE Id = '{linha.ID}';");

                _Helpers.QuerySQL(
                    $"UPDATE LinhasSTK" +
                    $"SET Lote = '{Tipo}{Serie}/{NumDoc}.{i}'" +
                    $"WHERE IdLinhaOrig = '{linha.ID}';");
            }
            #endregion

            #region Imprimir Talões de Produção Acabados
            StdBSTipos.ResultMsg resposta = PSO.MensagensDialogos.MostraMensagem(
                StdBSTipos.TipoMsg.PRI_SimNao,
                "Imprimir talões para caixas?");

            if (resposta == StdBSTipos.ResultMsg.PRI_Sim)
            {
                PSO.FuncoesUtilizador.Executa("ImprimirTaloesPA");
            }
            #endregion
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            if (BSO.Base.Artigos.DaValorAtributo(Artigo, "CDU_Pescado")) return;

            #region Tratar Artigo de Pescado
            IIntBELinhaDocumentoInterno linha = DocumentoInterno.Linhas.GetEdita(NumLinha);

            linha.CamposUtil["CDU_Caixas"].Valor = 1;
            if (linha.Lote == "L01") linha.PrecoUnitario = 0;

            linha.CamposUtil["CDU_Caixas"].Valor = _Helpers.MostraInputForm("Caixas", "Número de caixas: ", "", false);

            if (BSO.Base.Artigos.DaValorAtributo(Artigo, "CDU_VendaEmCaixa"))
            {
                linha.Quantidade = (double)linha.CamposUtil["CDU_Caixas"].Valor;

                // Quilos por cada caixa (permite decimal)
                DataTable KilosPorCaixa = _Helpers.GetDataTableDeSQL(
                    $"SELECT TOP(1) CDU_KilosPorCaixa AS Kgrs " +
                    $"FROM LinhasInternos li " +
                    $"  INNER JOIN CabecInternos ci ON li.IdCabecInternos = ci.Id " +
                    $"WHERE li.Artigo = '{Artigo}' " +
                    $"ORDER BY ci.Data DESC, ci.NumDoc DESC, li.NumLinha;");

                string kilosDefault = KilosPorCaixa.Rows.Count > 0 ? KilosPorCaixa.Rows[0].ToString() : BSO.Base.Artigos.DaValorAtributo(Artigo, "CDU_KilosPorCaixa");
                string kilosStr = _Helpers.MostraInputForm("Quilos", "Quilos por Caixa:", kilosDefault);

                if (!double.TryParse(kilosStr, out double kilosDbl))
                {
                    PSO.MensagensDialogos.MostraErro("O valor inserido para os Quilos por caixa não é válido.");
                    Cancel = true;
                    return;
                }
                linha.CamposUtil["CDU_KilosPorCaixa"].Valor = kilosDbl;

            } else
            {
                string kilosStr = _Helpers.MostraInputForm("Quantidades:", "Quantidades", "Kgrs");
                if (!double.TryParse(kilosStr, out double kilosDbl))
                {
                    PSO.MensagensDialogos.MostraErro("O valor inserido para os Quilos por caixa não é válido.");
                    kilosDbl = 0;
                }
                linha.Quantidade = kilosDbl;
            }
            #endregion
        }

        public override void ArtigoInexistente(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoInexistente(Artigo, NumLinha, ref Cancel, e);

            if (DocumentoInterno.Tipodoc != "PA" || !Artigo.StartsWith("#")) return;

            string loteAux = Artigo.Substring(1).ToUpper();
            string artAux = BSO.Base.Artigos.DaArtigoComCodBarras(ref loteAux);
            
            if (BSO.Base.Artigos.Existe(artAux))
            {
                DataTable origemDoLoteTbl = _Helpers.GetDataTableDeSQL($"SELECT * FROM usr_Origemdolote('{loteAux}', '{artAux}')");

                if (NumLinha == 1)
                {
                    IntBELinhaDocumentoInterno linha, linha2;

                    BSO.Internos.Documentos.AdicionaLinha(DocumentoInterno, artAux);
                    linha = DocumentoInterno.Linhas.GetEdita(NumLinha + 1);
                    linha.Lote = "L01";
                    linha.PrecoUnitario = 0;
                    linha.Desconto1 = 0;
                    linha.Quantidade = (double)origemDoLoteTbl.Rows[0]["Quantidade"];
                    linha.CamposUtil["CDU_Pescado"].Valor = true;
                    linha.CamposUtil["CDU_NomeCientifico"].Valor = origemDoLoteTbl.Rows[0]["NomeCientifico"];
                    linha.CamposUtil["CDU_Origem"].Valor = origemDoLoteTbl.Rows[0]["Origem"];
                    linha.CamposUtil["CDU_FormaObtencao"].Valor = origemDoLoteTbl.Rows[0]["FormaObtencao"];
                    linha.CamposUtil["CDU_ZonaFAO"].Valor = origemDoLoteTbl.Rows[0]["ZonaFAO"];
                    linha.CamposUtil["CDU_Caixas"].Valor = origemDoLoteTbl.Rows[0]["Caixas"];

                    BSO.Internos.Documentos.AdicionaLinha(DocumentoInterno, artAux);
                    linha2 = DocumentoInterno.Linhas.GetEdita(NumLinha + 2);
                    linha2.Lote = loteAux;
                    linha2.PrecoUnitario = 1;
                    linha2.Quantidade = 0 - (double)origemDoLoteTbl.Rows[0]["Quantidade"];
                    linha2.CamposUtil["CDU_Pescado"].Valor = true;
                    linha2.CamposUtil["CDU_NomeCientifico"].Valor = origemDoLoteTbl.Rows[0]["NomeCientifico"];
                    linha2.CamposUtil["CDU_Origem"].Valor = origemDoLoteTbl.Rows[0]["Origem"];
                    linha2.CamposUtil["CDU_FormaObtencao"].Valor = origemDoLoteTbl.Rows[0]["FormaObtencao"];
                    linha2.CamposUtil["CDU_ZonaFAO"].Valor = origemDoLoteTbl.Rows[0]["ZonaFAO"];
                    linha2.CamposUtil["CDU_Caixas"].Valor = origemDoLoteTbl.Rows[0]["Caixas"];
                }
                else
                {
                    IntBELinhaDocumentoInterno linha;
                    IntBELinhaDocumentoInterno primeiraLinha = DocumentoInterno.Linhas.GetEdita(1);

                    if (!
                        ((primeiraLinha.Artigo == artAux
                        && primeiraLinha.CamposUtil["CDU_NomeCientifico"].Valor == origemDoLoteTbl.Rows[0]["NomeCientifico"]
                        && primeiraLinha.CamposUtil["CDU_Origem"].Valor == origemDoLoteTbl.Rows[0]["Origem"]
                        && primeiraLinha.CamposUtil["CDU_FormaObtencao"].Valor == origemDoLoteTbl.Rows[0]["FormaObtencao"]
                        && primeiraLinha.CamposUtil["CDU_ZonaFAO"].Valor == origemDoLoteTbl.Rows[0]["ZonaFAO"]
                        )
                        || (bool)BSO.Base.Artigos.DaValorAtributo(Artigo, "CDU_ProdTransf")))
                    {
                        PSO.MensagensDialogos.MostraAviso("O artigo a lançar não possui as características do primeiro artigo.", StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);
                    }

                        BSO.Internos.Documentos.AdicionaLinha(DocumentoInterno, artAux);
                        linha = DocumentoInterno.Linhas.GetEdita(NumLinha + 1);
                        linha.Lote = loteAux;
                        linha.PrecoUnitario = 1;
                        linha.Quantidade = 0 - (double)origemDoLoteTbl.Rows[0]["Quantidade"];
                        linha.CamposUtil["CDU_Pescado"].Valor = true;
                        linha.CamposUtil["CDU_NomeCientifico"].Valor = origemDoLoteTbl.Rows[0]["NomeCientifico"];
                        linha.CamposUtil["CDU_Origem"].Valor = origemDoLoteTbl.Rows[0]["Origem"];
                        linha.CamposUtil["CDU_FormaObtencao"].Valor = origemDoLoteTbl.Rows[0]["FormaObtencao"];
                        linha.CamposUtil["CDU_ZonaFAO"].Valor = origemDoLoteTbl.Rows[0]["ZonaFAO"];
                        linha.CamposUtil["CDU_Caixas"].Valor = origemDoLoteTbl.Rows[0]["Caixas"];
                }
            }
            DocumentoInterno.Linhas.Remove(NumLinha);
        }
    }
}
