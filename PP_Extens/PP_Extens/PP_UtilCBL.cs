using Primavera.Extensibility.Internal.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using CblBE100; using CmpBS100;

namespace PP_Extens
{
    public class PP_UtilCBL : UtilLigacaoCBL
    {
        private double diff, totalDeb, totalCred, totalIvaDeb, totalIvaCred, totalDocOrig, totalIvaDocOrig;

        public override void AntesDeProcessarCBL(ref bool Cancel, ExtensibilityEventArgs e)
        {
            const double ERRO = 0.02;
            CblBEDocumento objCBL = new CblBEDocumento();
            string tipodoc;
            int i, l;

            tipodoc = DocumentoCBL.Doc;

            if (DocumentoCBL.Modulo == "C") {
                string[] docArr = { "VFA", "VFS" };
                if (docArr.Contains(tipodoc) && DocumentoCBL.LinhasGeral.NumItens != 0) {
                    // Reset variaveis
                    totalDeb = 0; totalCred = 0; totalIvaDeb = 0; totalIvaCred = 0;
                    totalDocOrig = BSO.Compras.Documentos.DaTotalDocumento(this.DocumentoCBL.IdDocOrigem);
                    totalIvaDocOrig = BSO.Compras.Documentos.DaValorAtributoID(this.DocumentoCBL.IdDocOrigem, "TotalIVA");
                }
            }

            base.AntesDeProcessarCBL(ref Cancel, e);
        }

        private void CalculaTotaisLancamento(ref double totalDeb, ref double totalCred, ref double totalIvaDeb, ref double totalIvaCred)
        {

            totalDeb = 0;
            totalCred = 0;
            totalIvaDeb = 0;
            totalIvaCred = 0;
            CblBELinhasDocGeral linhasGeral = DocumentoCBL.LinhasGeral;

            for (int i = 1; i < this.DocumentoCBL.LinhasGeral.NumItens; i++) {
                CblBELinhaDocGeral linha = linhasGeral.GetEdita(i);

                if (linha.Natureza == "D") {

                }
            }
        }
    }
}
