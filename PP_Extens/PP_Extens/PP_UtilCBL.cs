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
        public override void AntesDeProcessarCBL(ref bool Cancel, ExtensibilityEventArgs e)
        {
            const double ERRO = 0.02;
            CblBEDocumento objCBL = new CblBEDocumento();
            double diff, totalDeb, totalCred, totalIvaDeb, totalIvaCred, totalDocOrig, totalIvaDocOrig;
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
            //If Me.DocumentoCBL.Modulo = "C" Then


            //    If((Tipodoc = "VFA") Or(Tipodoc = "VFS")) And(Me.DocumentoCBL.LinhasGeral.NumItens <> 0) Then
            //        CalculaTotaisLancamento totalDeb, totalCred, totalIvaDeb, totalvaCred


            //        If(totalDeb <> totalCred) And(Abs(totalDeb - totalCred) <= Erro) Then

            //          For i = 1 To Me.DocumentoCBL.LinhasGeral.NumItens

            //              If Abs(Me.DocumentoCBL.LinhasGeral(i).Valor - totalCred) <= Erro Then


            //                    If(Me.DocumentoCBL.LinhasGeral(i).Conta Like "221*") _
            //                        Or(Me.DocumentoCBL.LinhasGeral(i).Conta Like "1*") Then


            //                        Me.DocumentoCBL.LinhasGeral(i).Valor = totalDocOrig
            //                        Me.DocumentoCBL.LinhasGeral(i).ValorAlt = totalDocOrig
            //                        Me.DocumentoCBL.LinhasGeral(i).ValorIncIVA = totalDocOrig
            //                        Me.DocumentoCBL.LinhasGeral(i).ValorIncIVAAlt = totalDocOrig


            //                    End If


            //                End If


            //            Next i


            //        End If


            //        CalculaTotaisLancamento totalDeb, totalCred, totalIvaDeb, totalvaCred


            //        If(totalDeb <> totalCred) And(Abs(totalIvaDeb - totalvaCred) <= Erro) And(Abs(totalvaDocOrig - totalIvaDeb) <= Erro) Then

            //     End If

            //     CalculaTotaisLancamento totalDeb, totalCred, totalIvaDeb, totalvaCred


            //        If(totalDeb <> totalCred) And(Abs(totalDocOrig + totalvaDocOrig - totalDeb + totalIvaDeb) <= Erro) Then

            //          l = 0
            //            diff = 0


            //            For i = 1 To Me.DocumentoCBL.LinhasGeral.NumItens


            //                If(Me.DocumentoCBL.LinhasGeral(i).Valor > diff) And(Me.DocumentoCBL.LinhasGeral(i).Conta Like "[3,6]*") Then

            //                  diff = Me.DocumentoCBL.LinhasGeral(i).Valor
            //                    l = i


            //                End If


            //            Next i


            //            If Me.DocumentoCBL.LinhasGeral(l).Natureza = "D" Then


            //                Me.DocumentoCBL.LinhasGeral(l).Valor = Me.DocumentoCBL.LinhasGeral(l).Valor - totalDeb + totalCred


            //            ElseIf Me.DocumentoCBL.LinhasGeral(l).Natureza = "C" Then


            //                Me.DocumentoCBL.LinhasGeral(l).Valor = Me.DocumentoCBL.LinhasGeral(l).Valor + totalDeb - totalCred


            //            Else

            //                MsgBox "Não pode chegar aqui.", vbOKOnly + vbCritical


            //            End If


            //        End If


            //    End If


            //    CalculaTotaisLancamento totalDeb, totalCred, totalIvaDeb, totalvaCred


            //    If(Abs(totalDeb - totalCred) > 0) And(Abs(totalDeb - totalCred) <= Erro) And _
            //      (Abs(BSO.Comercial.Compras.DaValorAtributo(Me.DocumentoCBL.doc, Me.DocumentoCBL.NumDoc, Me.DocumentoCBL.Serie, "000", "TotalIva")) -totalIvaDeb <> 0) Then

            //     diff = 0


            //        For i = 1 To Me.DocumentoCBL.LinhasGeral.NumItens


            //            If(Me.DocumentoCBL.LinhasGeral(i).Valor > diff) And(Me.DocumentoCBL.LinhasGeral(i).Conta Like "[243]*") Then

            //              diff = Me.DocumentoCBL.LinhasGeral(i).Valor
            //                l = i


            //            End If


            //        Next i


            //        Me.DocumentoCBL.LinhasGeral(i).Valor = Me.DocumentoCBL.LinhasGeral(i).Valor + (totalCred - totalDeb)


            //    End If


            //ElseIf Me.DocumentoCBL.Modulo = "V" Then


            //    If((Tipodoc = "FR") Or(Tipodoc = "FRH")) And(Me.DocumentoCBL.LinhasGeral.NumItens <> 0) Then

            //     totalDeb = 0
            //        totalCred = 0
            //        totalIvaDeb = 0
            //        totalvaCred = 0
            //        totalDocOrig = BSO.Comercial.Vendas.DaTotalDocumento(Me.DocumentoCBL.IdDocOrigem)


            //        CalculaTotaisLancamento totalDeb, totalCred, totalIvaDeb, totalvaCred


            //        If(totalDeb <> totalCred) And(Abs(totalDeb - totalCred) <= Erro) Then

            //          For i = 1 To Me.DocumentoCBL.LinhasGeral.NumItens

            //              If Abs(Me.DocumentoCBL.LinhasGeral(i).Valor - totalDocOrig) <= Erro Then


            //                    If(Me.DocumentoCBL.LinhasGeral(i).Conta Like "21*") _
            //                        Or(Me.DocumentoCBL.LinhasGeral(i).Conta Like "1*") Then


            //                        Me.DocumentoCBL.LinhasGeral(i).Valor = totalDocOrig
            //                        Me.DocumentoCBL.LinhasGeral(i).ValorAlt = totalDocOrig


            //                    End If


            //                End If


            //            Next i


            //        End If


            //    End If


            //End If

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
            //        Private Sub CalculaTotaisLancamento(ByRef totaldeb, totalcred, totalivadeb, totalivacred As Double)

            //    For i = 1 To Me.DocumentoCBL.LinhasGeral.NumItens


            //        If Me.DocumentoCBL.LinhasGeral(i).Natureza = "D" Then

            //            totaldeb = totaldeb + Me.DocumentoCBL.LinhasGeral(i).Valor


            //            If Me.DocumentoCBL.LinhasGeral(i).Conta Like "243*" Then

            //                totalivadeb = totalivadeb + Me.DocumentoCBL.LinhasGeral(i).Valor


            //            End If


            //        ElseIf Me.DocumentoCBL.LinhasGeral(i).Natureza = "C" Then

            //            totalcred = totalcred + Me.DocumentoCBL.LinhasGeral(i).Valor


            //            If Me.DocumentoCBL.LinhasGeral(i).Conta Like "243*" Then

            //                totalivacred = totalivacred + Me.DocumentoCBL.LinhasGeral(i).Valor


            //            End If


            //        Else

            //        End If

            //    Next i

            //End Sub
        }
    }
}
