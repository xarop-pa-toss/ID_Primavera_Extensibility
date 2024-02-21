using Primavera.Extensibility.Purchases.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpersPrimavera10;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using BasBE100;
using CmpBE100;
using System.Net;
using StdPlatBS100;
using StdBE100;


namespace PP_Extens
{ 
    public class UiEditorCompras : EditorCompras
    {
        HelperFunctions _Helpers = new HelperFunctions();
        PP_Geral _PP_Geral = new PP_Geral();

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            CmpBELinhaDocumentoCompra linha = DocumentoCompra.Linhas.GetEdita(NumLinha);

            #region InputForm Quantidades
            using (BasBEArtigo artigo = BSO.Base.Artigos.Edita(Artigo))
            {
                if ((bool)artigo.CamposUtil["CDU_Pescado"].Valor) {
                    linha.CamposUtil["CDU_Pescado"].Valor = artigo.CamposUtil["CDU_Pescado"].Valor;
                    linha.CamposUtil["CDU_NomeCientifico"].Valor = artigo.CamposUtil["CDU_NomeCientifico"].Valor;
                    linha.CamposUtil["CDU_FormaObtencao"].Valor = artigo.CamposUtil["CDU_FormaObtencao"].Valor;
                    linha.CamposUtil["CDU_ZonaFAO"].Valor = artigo.CamposUtil["CDU_ZonaFAO"].Valor;
                    linha.CamposUtil["CDU_Origem"].Valor = artigo.CamposUtil["CDU_Origem"].Valor;
                }
            }

            if ((bool)linha.CamposUtil["CDU_Pescado"].Valor) {
                string quantidadeStr = _Helpers.MostraInputForm("Quantidades em Kg", "Quantidades:", "");
                double quantidadeDbl;
                if (Double.TryParse(quantidadeStr, out quantidadeDbl)) {
                    linha.Quantidade = quantidadeDbl;
                };
            }
            #endregion

            #region InputForm Forma de Obtenção
            Dictionary<string, string> obtencaoDict = new Dictionary<string, string>()
            {
                { "1", "Aquicultura" },
                { "2", "Capturado, Anzóis e aparelhos de anzol" },
                { "3", "Capturado, Dragas" },
                { "4", "Capturado, Nassas e armadilhas" },
                { "5", "Capturado, Redes de arrastar" },
                { "6", "Capturado, Redes de cercar e redes de sacada" },
                { "7", "Capturado, Redes de emalhar e redes semelhantes" },
                { "8", "Capturado, Redes envolventes-arrastantes" }
            };

            do
            {
                string obtencaoStr = _Helpers.MostraInputForm(
                "Forma de Obtenção",
                "Introduza a Forma de Obtenção:" + Environment.NewLine + string.Join(Environment.NewLine, obtencaoDict.Select(x => x.Key + " - " + x.Value)),
                "");

                if (obtencaoDict.TryGetValue(obtencaoStr, out string value))
                {
                    linha.CamposUtil["CDU_FormaObtencao"].Valor = value;
                    break;
                } else
                {
                    PSO.MensagensDialogos.MostraErro("Valor não encontrado. Insira um dos valores listados.", StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);
                    continue;
                }
            } while (true);           
            #endregion
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            #region Verificar NumDoc Externo
            if (!VerificacaoNumDocExterno()) { Cancel = true; return; }
            if (DocumentoCompra.Linhas.NumItens == 0) { return; }

            if (DocumentoCompra.Tipodoc == "RP")
            {
                StdBSTipos.ResultMsg resultado = PSO.MensagensDialogos.MostraMensagem(
                    StdBSTipos.TipoMsg.PRI_SimNao,
                    "Confirma que estão correctas em todas as linhas as informações de método de captura, zona de captura e origem?",
                    StdBSTipos.IconId.PRI_Questiona);

                if (resultado != StdBSTipos.ResultMsg.PRI_Sim) { Cancel = true; return; }

                if (!VerificacaoDataStock()) { Cancel = true; return; }
                if (!VerificacaoTotalKg()) { Cancel = true; return; }
            }



            #endregion
        }

        private bool VerificacaoNumDocExterno()
        {
            string numDocFornStr = _Helpers.MostraInputForm("", "Número do documento do fornecedor:", DocumentoCompra.NumDocExterno.Trim()).Trim();

            if (numDocFornStr.Length == 0) { return true; }

            if (numDocFornStr == "1")
            {
                StdBSTipos.ResultMsg resultado = PSO.MensagensDialogos.MostraMensagem(
                    StdPlatBS100.StdBSTipos.TipoMsg.PRI_SimNao,
                    @"Tem a certeza que quer deixar com o valor '1' o número do documento do fornecedor?",
                    StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);

                if (resultado != StdBSTipos.ResultMsg.PRI_Sim) { return false; }
            }

            DocumentoCompra.NumDocExterno = numDocFornStr;
            return true;
        }

        private bool VerificacaoDataStock()
        {
            if (!DateTime.TryParse(DocumentoCompra.CamposUtil["CDU_DataStock"].Valor.ToString(), out DateTime data)) {
                DocumentoCompra.CamposUtil["CDU_DataStock"].Valor = DateTime.Today;
            }

            string dataStockStr = _Helpers.MostraInputForm(
                "",
                "Data a considerar para o stock:",
                data.ToString("dd-MM-yyyy"));

            if (dataStockStr.Length > 0 && DateTime.TryParse(dataStockStr, out DateTime dataStock)) {
                DocumentoCompra.CamposUtil["CDU_DataStock"].Valor = dataStock;

                return true;
            } else
            {
                PSO.MensagensDialogos.MostraErro("Atenção: a data de stock introduzida é inválida. O documento não será gravado.");
                return false;
            }
        }

        private bool VerificacaoTotalKg()
        {
            if (DocumentoCompra.Linhas.NumItens == 0) return false;

            double kg = 0;
            
            foreach (CmpBELinhaDocumentoCompra linha in DocumentoCompra.Linhas)
            {
                if (!(bool)linha.CamposUtil["CDU_Pescado"].Valor) continue;

                if (linha.Unidade == "KG") {
                    kg = kg + Math.Abs(linha.Quantidade);
                } 
                else if (_PP_Geral.UnidadeCaixa(linha.Unidade))                {
                    kg = kg + Math.Abs(linha.Quantidade) * _PP_Geral.ObterKgDaUnidade(linha.Unidade);
                }
            }

            StdBSTipos.ResultMsg resultado = PSO.MensagensDialogos.MostraMensagem(
                StdBSTipos.TipoMsg.PRI_SimNao,
                "Resumo do documento:" + Environment.NewLine +
                $"{kg} Kilos." + Environment.NewLine + Environment.NewLine +
                "Confirma?",
                StdBSTipos.IconId.PRI_Informativo);

            if (resultado == StdBSTipos.ResultMsg.PRI_Sim) return true;

            return false;
        }
    }
}
