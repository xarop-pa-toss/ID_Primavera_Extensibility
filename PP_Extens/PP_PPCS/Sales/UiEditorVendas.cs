using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.BusinessEntities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSet = System.Data.DataSet; using OleDb = System.Data.OleDb;
using System.Threading.Tasks;
using StdBE100; using StdPlatBS100; using VndBE100; using ErpBS100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;

namespace PP_PPCS.Sales
{
    public class UiEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            var DocVenda = this.DocumentoVenda;
            string matricula = "";
            double cx, kg;
            long i;
            StdBELista serie = BSO.Consulta(String.Format("SELECT CDU_PedeMatricula FROM SeriesVendas WHERE TipoDoc = '{0}' AND Serie = '{1}';", DocVenda.Tipodoc, DocVenda.Serie));

            if (!serie.Vazia()) {
                PSO.MensagensDialogos.MostraDialogoInput(ref matricula, "Matricula", "Matricula da viatura:", strValorDefeito: DocVenda.Matricula);
                DocVenda.Matricula = matricula;
            }
            base.AntesDeGravar(ref Cancel, e);
        }


        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            var DocVenda = this.DocumentoVenda;
            string s1 = "", s2 = "", s3 = "";
            BasBE100.BasBEArtigo art = BSO.Base.Artigos.Edita(Artigo);

            if (art.CamposUtil["CDU_VendaEmCaixa"].Valor.Equals(true)) {
                // Kilos por caixa (permite decimal)
                PSO.MensagensDialogos.MostraDialogoInput(ref s1, "Quilos/caixa", "Quilos por caixa:", strValorDefeito: art.CamposUtil["CDU_KilosPorCaixa"].Valor.ToString());
                // Check input
                double x = 0.0;
                if (double.TryParse(s1, out x)) {
                    DocVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_KilosPorCaixa"].Valor = x;
                } else {
                    PSO.MensagensDialogos.MostraAviso(String.Format("{0} não é um valor válido", s1.ToString()), StdBSTipos.IconId.PRI_Exclama);
                    Cancel = true;
                }

                // Quantidade de caixas (inteiro)
                PSO.MensagensDialogos.MostraDialogoInput(ref s2, "Caixas", "Nro. de caixas:");
                // Check input
                int y = 0;
                if (int.TryParse(s2, out y)) {
                    DocVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_Caixas"].Valor = y;
                    DocVenda.Linhas.GetEdita(NumLinha).Quantidade = y;
                } else {
                    PSO.MensagensDialogos.MostraAviso(String.Format("{0} não é um valor válido", s2.ToString()), StdBSTipos.IconId.PRI_Exclama);
                    Cancel = true;
                }
                art = null;
            }

            // Pede fornecedor / origem
            StdBELista pedeFornecedor = BSO.Consulta(String.Format("SELECT CDU_PedeFornecedor FROM SeriesVendas WHERE TipoDoc = '{0}' AND Serie = '{1}';", DocVenda.Tipodoc, DocVenda.Serie));

            if (!pedeFornecedor.Vazia())
                pedeFornecedor.Inicio();

            if (pedeFornecedor.Valor("CDU_PedeFornecedor")) {
                PSO.MensagensDialogos.MostraDialogoInput(ref s3, "Proveniencia", "Fornecedor:", strValorDefeito: DocVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_Fornecedor"].Valor.ToString());

                DocVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_Fornecedor"].Valor = s3.Trim().ToUpper();
            }

            DocVenda.Dispose();
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);
        }


        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            var DocVenda = this.DocumentoVenda;
            string nrDoc = "", vendedor = "", matricula = "";

            StdBELista serie = BSO.Consulta(String.Format("SELECT CDU_PedeVendedor, CDU_PedeDocumento, CDU_PedeMatricula FROM SeriesVendas WHERE TipoDoc = '{0}' AND '{1}';", DocVenda.Tipodoc, DocVenda.Serie));

            // Data de descarga igual à data de carga
            if (DocVenda.DataHoraDescarga == null) {
                DocVenda.DataHoraDescarga = DocVenda.DataHoraCarga;
            }

            if (!serie.Vazia()) {

                // Código do vendedor
                if (serie.Valor("CDU_PedeVendedor")) {

                    PSO.MensagensDialogos.MostraDialogoInput(ref vendedor, "Vendedor", "Código do Vendedor:", strValorDefeito: "0");
                    StdBELista vend = BSO.Consulta(String.Format("SELECT Vendedor FROM Vendedores WHERE Vendedor = '{0}';"));

                    if (!vend.Vazia()) {
                        PSO.MensagensDialogos.MostraAviso("Vendedor inexistente!", StdBSTipos.IconId.PRI_Exclama);
                    } else {
                        DocVenda.Responsavel = vendedor;
                    }
                }
                
                // Número do documento manual a lançar
                if (serie.Valor("CDU_PedeDocumento")) {
                    PSO.MensagensDialogos.MostraDialogoInput(ref nrDoc, "Vendedor", "Código do Vendedor:", strValorDefeito: "0");
                    DocVenda.CamposUtil["CDU_NroManual"].Valor = nrDoc.Trim().Substring(0, 10);
                }
            }

            base.ClienteIdentificado(Cliente, ref Cancel, e);
        }
    }
}

