using BasBE100;
using CmpBE100;
using InvBE100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using StdPlatBS100;
using System;
using System.Data;
using System.Globalization;


namespace PP_Qualidade.Purchases
{
    public class UiEditorCompras : EditorCompras
    {
        HelpersPrimavera10.HelperFunctions _Helpers = new HelpersPrimavera10.HelperFunctions();


        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            #region Pede numero fornecedor para NumDocExterno
            string numFornecedor = _Helpers.MostraInputForm("Documento", "Numero do documento do fornecedor:", DocumentoCompra.NumDocExterno);
            if (string.IsNullOrEmpty(numFornecedor))
            {
                Cancel = true;
                return;
            }
            DocumentoCompra.NumDocExterno = numFornecedor;
            #endregion

            #region Pede Data Stock
            try
            {
                string dataDefaultStr = DocumentoCompra.CamposUtil["CDU_DataStock"].Valor.ToString() ?? DateTime.Now.ToString();
                string dataStockStr = _Helpers.MostraInputForm("Data Stock", "Data a considerar para o stock:", dataDefaultStr);
                if (string.IsNullOrEmpty(dataStockStr))
                {
                    Cancel = true;
                    return;
                }

                DocumentoCompra.CamposUtil["CDU_DataStock"].Valor = DateTime.ParseExact(dataStockStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                PSO.MensagensDialogos.MostraErro("Não foi possivel converter o valor inserido para uma data válida. Data Stock foi definida como a data do documento.");
                DocumentoCompra.CamposUtil["CDU_DataStock"].Valor = DocumentoCompra.DataDoc;
            }
            #endregion

            #region Caixas e Kilos
            if (DocumentoCompra.Linhas.NumItens != 0)
            {
                int caixas = 0;
                double kgs = 0;

                foreach (CmpBELinhaDocumentoCompra linha in DocumentoCompra.Linhas)
                {
                    if ((bool)linha.CamposUtil["CDU_Pescado"].Valor)
                    {
                        caixas = caixas + (int)(linha.CamposUtil["CDU_Caixas"].Valor ?? 0);

                        if (linha.Unidade == "KG")
                        {
                            kgs = kgs + (linha.Quantidade != null ? linha.Quantidade : 0.0);
                        } else if (linha.Unidade == "CX")
                        {
                            kgs = kgs + (linha.Quantidade * ((double)(linha.CamposUtil["CDU_KilosPorCaixa"].Valor ?? 0.0)));
                        }
                    }
                }

                StdPlatBS100.StdBSTipos.ResultMsg resultado = PSO.MensagensDialogos.MostraMensagem(
                StdPlatBS100.StdBSTipos.TipoMsg.PRI_OkCancelar,
                "Resumo do documento" + Environment.NewLine + Environment.NewLine +
                $"Caixas: {caixas}" + Environment.NewLine +
                $"Kilos: {kgs}" + Environment.NewLine + Environment.NewLine +
                "Confirma?");

                if (resultado == StdPlatBS100.StdBSTipos.ResultMsg.PRI_Cancelar)
                {
                    Cancel = true;
                    return;
                }
            }
            #endregion
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            CmpBELinhaDocumentoCompra linha = DocumentoCompra.Linhas.GetEdita(LinhaActual);

            if (BSO.Base.Artigos.DaValorAtributo(Artigo, "CDU_Pescado"))
            {
                #region Tratar artigo de pescado

                linha.CamposUtil["CDU_Caixas"].Valor = 1;
                linha.PrecUnit = 0;

                string nroCaixasStr = _Helpers.MostraInputForm("Caixas", "Nro. de caixas:", null);
                linha.CamposUtil["CDU_Caixas"].Valor = nroCaixasStr != null ? Convert.ToInt32(nroCaixasStr) : 0;

                if (BSO.Base.Artigos.DaValorAtributo(Artigo, "CDU_VendaEmCaixa"))
                {
                    linha.Quantidade = (double)linha.CamposUtil["CDU_Caixas"].Valor;

                    DataTable dt = _Helpers.GetDataTableDeSQL(
                        " SELECT TOP(1) CDU_KilosPorCaixa As Kgrs " +
                        " FROM LinhasCompras lc " +
                        "   INNER JOIN CabecCompras cc ON lc.IdCabecCompras = cc.Id " +
                       $" WHERE lc.Artigo = '{Artigo}' " +
                       $" ORDER BY cc.DataDoc Desc, cc.NumDoc Desc, lc.NumLinha;");

                    string defaultInputFormText = dt.Rows.Count == 0
                        ? BSO.Base.Artigos.DaValorAtributo(Artigo, "CDU_KilosPorCaixa")
                        : dt.Rows[0][0].ToString();

                    string kilosPorCaixa = _Helpers.MostraInputForm("Quilos por caixa", "Quilos por caixa:", defaultInputFormText);
                    kilosPorCaixa = kilosPorCaixa.Replace(',', '.');

                    if (!double.TryParse(kilosPorCaixa, out double result))
                    {
                        PSO.MensagensDialogos.MostraErro("Valor inválido para 'Quilos por caixa'");
                        Cancel = true;
                        return;
                    }
                    linha.CamposUtil["CDU_KilosPorCaixa"].Valor = result;
                }


                string quantidades = _Helpers.MostraInputForm("", "Quantidades:", "Kgrs");
                quantidades = quantidades.Replace(',', '.');

                if (!double.TryParse(quantidades, out double res))
                {
                    PSO.MensagensDialogos.MostraErro("Valor inválido para 'Quantidade");
                    Cancel = true;
                    return;
                }
                linha.CamposUtil["CDU_KilosPorCaixa"].Valor = res;



                #endregion

                // Preencher CDU com Conformes
                linha.CamposUtil["CDU_ConfCOrganl"].Valor = "Conforme";
                linha.CamposUtil["CDU_AvGeral"].Valor = "Conforme";
                linha.CamposUtil["CDU_Parasitas"].Valor = "Conforme";
            }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);


            #region Tratar Lotes Novos
            if (Tipo != "RP" || DocumentoCompra.Linhas.NumItens == 0) { return; }

            int i = 0;

            foreach (CmpBELinhaDocumentoCompra linha in DocumentoCompra.Linhas)
            {
                i++;
                if (linha.TipoLinha == "10")
                {
                    if (linha.Lote == "L01" &&
                        BSO.Base.Artigos.DaValorAtributo(linha.Artigo, "MovStock") == "S" &&
                        BSO.Base.Artigos.DaValorAtributo(linha.Artigo, "TratamentoLotes") &&
                        BSO.Base.Artigos.DaValorAtributo(linha.Artigo, "CDU_Pescado"))
                    {
                        if (BSO.Inventario.ArtigosLotes.Existe(linha.Artigo, $"{Tipo}{Serie}/{NumDoc}.{i}"))
                        {
                            using (InvBEArtigoLote lote = new InvBEArtigoLote())
                            {
                                lote.Artigo = linha.Artigo;
                                lote.Lote = $"{Tipo}{Serie}/{NumDoc}.{i}";
                                lote.Descricao = $"Lote {lote.Lote}";
                                lote.DataFabrico = linha.DataEntrega;
                                lote.Activo = true;

                                BSO.Inventario.ArtigosLotes.Actualiza(lote);
                            }
                        }

                        if (!BSO.Base.ArtigosCodBarras.Existe(linha.Artigo, linha.Unidade))
                        {
                            using (BasBEArtigoCodBarra cbar = new BasBEArtigoCodBarra())
                            {
                                cbar.Artigo = linha.Artigo;
                                cbar.CodBarras = $"{Tipo}{Serie}/{NumDoc}.{i}";
                                cbar.Unidade = linha.Unidade;

                                BSO.Base.ArtigosCodBarras.Actualiza(cbar);
                            }
                        }

                        _Helpers.QuerySQL(
                            $" UPDATE LinhasCompras " +
                            $" SET Lote = '{Tipo}{Serie}/{NumDoc}.{i}' " +
                            $" WHERE Id = '{linha.IdLinha}';");

                        _Helpers.QuerySQL(
                            $" UPDATE LinhasCompras " +
                            $" SET Lote = '{Tipo}{Serie}/{NumDoc}.{i}' " +
                            $" WHERE Id = '{linha.IdLinha}';");
                    }
                }
            }
            #endregion

            #region Imprimir Talões Entrada
            const string REPORT = "TIRP_01";

            PSO.FuncoesUtilizador.Executa("ImprimirTaloesRP");
            _Helpers.QuerySQL(
               " UPDATE LinhasCompras " +
               " SET CDU_ImprimirET = 0 " +
               " FROM LinhasCompras lc " +
               " INNER JOIN CabecCompras cc ON lc.IdCabecCompras = cc.Id " +
               " WHERE ISNULL(lc.CDU_ImprimirET, 0) <> 0 " +
               $" AND cc.Filial = '{Filial}' " +
               $" AND cc.TipoDoc = '{Tipo}' " +
               $" AND cc.Serie = '{Serie}' " +
               $" AND cc.NumDoc = {NumDoc};");
            #endregion
        }
    }
}
