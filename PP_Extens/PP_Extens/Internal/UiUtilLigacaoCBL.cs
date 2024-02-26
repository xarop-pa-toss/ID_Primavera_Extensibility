using Primavera.Extensibility.Internal.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpersPrimavera10;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Integration.Modules.HumanResources.Services;
using CblBE100;
using static System.Windows.Forms.LinkLabel;
using System.Security.Cryptography;



namespace PP_Extens.Internal
{
    public class UiUtilLigacaoCBL : UtilLigacaoCBL
    {
        HelperFunctions _Helpers = new HelperFunctions();

        const decimal ERRO = 0.02M;

        public override void AntesDeProcessarCBL(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeProcessarCBL(ref Cancel, e);

            string tipoDoc = DocumentoCBL.Doc;
            decimal totalDeb, totalCred, totalIvaDeb, totalIvaCred, totalDocOrig, totalIvaDocOrig;

            if (DocumentoCBL.Modulo == "V" && !((tipoDoc != "FR" || tipoDoc != "FRH") && DocumentoCBL.LinhasGeral.NumItens > 0)) 
            {
                totalDeb = 0; totalCred = 0; totalIvaDeb = 0; totalIvaCred = 0;
                totalDocOrig = (decimal)BSO.Vendas.Documentos.DaTotalDocumento(DocumentoCBL.IdDocOrigem);

                CalcularTotaisLancamento(ref totalDeb, ref totalCred, ref totalIvaDeb, ref totalIvaCred);

                if (totalDeb != totalCred && Math.Abs(totalDeb - totalCred) <= ERRO) 
                {
                    foreach (CblBELinhaDocGeral linha in DocumentoCBL.LinhasGeral)
                    {
                        if (Math.Abs(linha.Valor - totalDocOrig) <= ERRO && (linha.Conta.StartsWith("21") || linha.Conta.StartsWith("1"))) 
                        {
                            linha.Valor = totalDocOrig;
                            linha.ValorAlt = totalDocOrig;
                        }
                    }
                }
            }
            else if (DocumentoCBL.Modulo == "C" && (tipoDoc == "VFA" || tipoDoc == "VFS") && DocumentoCBL.LinhasGeral.NumItens > 0) 
            {
                totalDeb = 0; totalCred = 0; totalIvaDeb = 0; totalIvaCred = 0;
                totalDocOrig = (decimal)BSO.Compras.Documentos.DaTotalDocumento(DocumentoCBL.IdDocOrigem);
                totalIvaDocOrig = BSO.Compras.Documentos.DaValorAtributoID(DocumentoCBL.IdDocOrigem, "TotalIVA");

                CalcularTotaisLancamento(ref totalDeb, ref totalCred, ref totalIvaDeb, ref totalIvaCred);

                if (totalDeb != totalCred && Math.Abs(totalDeb - totalCred) <= ERRO)
                {
                    foreach (CblBELinhaDocGeral linha in DocumentoCBL.LinhasGeral)
                    {
                        if (Math.Abs(linha.Valor - totalCred) <= ERRO && (linha.Conta.StartsWith("221") || linha.Conta.StartsWith("1")))
                        {
                            linha.Valor = totalDocOrig;
                            linha.ValorAlt = totalDocOrig;
                            linha.ValorIncIVA = totalDocOrig;
                            linha.ValorIncIVAAlt = totalDocOrig;
                        }
                    }
                }

                CalcularTotaisLancamento(ref totalDeb, ref totalCred, ref totalIvaDeb, ref totalIvaCred);

                decimal diff;
                int linhaCounter = 0;

                if (totalDeb != totalCred && Math.Abs(totalDocOrig + totalIvaDocOrig - totalDeb + totalIvaDeb) <= ERRO)
                {
                    CblBELinhaDocGeral linha;
                    diff = 0;
                    linhaCounter = 0;

                    for (int i = 1; i <= DocumentoCBL.LinhasGeral.NumItens; i++)
                    {
                        linha = DocumentoCBL.LinhasGeral.GetEdita(i);
                        if (linha.Valor > diff && (linha.Conta.StartsWith("3") || linha.Conta.StartsWith("6")))
                        {
                            diff = linha.Valor;
                            linhaCounter = i;
                        }
                    }

                    linha = DocumentoCBL.LinhasGeral.GetEdita(linhaCounter);
                    if (linha.Natureza == "D")
                    {
                        linha.Valor = linha.Valor - totalDeb + totalCred;
                    }
                    else if (linha.Natureza == "C")
                    {
                        linha.Valor = linha.Valor + totalDeb - totalCred;
                    }
                }

                CalcularTotaisLancamento (ref totalDeb, ref totalCred, ref totalIvaDeb, ref totalIvaCred);

                if (Math.Abs(totalDeb - totalCred) > 0 && Math.Abs(totalDeb - totalCred) <= ERRO
                    && Math.Abs(BSO.Compras.Documentos.DaValorAtributo(DocumentoCBL.Doc, DocumentoCBL.NumDoc, DocumentoCBL.Serie, "000", "TotalIva")) - totalIvaDeb != 0)
                {
                    CblBELinhaDocGeral linha;
                    diff = 0;

                    for (int i = 1; i <= DocumentoCBL.LinhasGeral.NumItens; i++)
                    {
                        linha = DocumentoCBL.LinhasGeral.GetEdita(i);
                        if (linha.Valor > diff && linha.Conta.StartsWith("243"))
                        {
                            diff = linha.Valor;
                            linhaCounter = i;
                        }
                    }

                    linha = DocumentoCBL.LinhasGeral.GetEdita(linhaCounter);
                    linha.Valor = linha.Valor + (totalCred - totalDeb);
                }
            }
        }
        

        private void CalcularTotaisLancamento(ref decimal totalDeb, ref decimal totalCred, ref decimal totalIvaDeb, ref decimal totalIvaCred)
        {
            totalDeb = 0; totalCred = 0; totalIvaDeb = 0; totalIvaCred = 0;

            foreach (CblBELinhaDocGeral linha in DocumentoCBL.LinhasGeral)
            {
                if (linha.Natureza == "D") 
                {
                    totalDeb += linha.Valor;
                    if (linha.Conta.StartsWith("243")) { totalIvaDeb += linha.Valor; }
                }
                else if (linha.Natureza == "C") 
                {
                    totalCred += linha.Valor;
                    if (linha.Conta.StartsWith("243")) { totalIvaCred += linha.Valor; }
                }
            }
        }
    }
}
