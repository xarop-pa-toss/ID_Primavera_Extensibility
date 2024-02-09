using IntBE100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Internal.Editors;
using Primavera.Extensibility.Internal.Editors.Details;
using System;


namespace PP_Qualidade.Internal
{
    public class UiEditorInternos : EditorInternos
    {
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

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            if (BSO.Base.Artigos.DaValorAtributo(Artigo, "CDU_Pescado")) return;


        }
    }
}
