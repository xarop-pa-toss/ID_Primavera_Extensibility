using Primavera.Extensibility.Sales.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSet = System.Data.DataSet; using OleDb = System.Data.OleDb;
using System.Threading.Tasks;
using StdBE100; using StdPlatBS100; using VndBE100; using ErpBS100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using HelpersPrimavera10;

namespace PP_PPCS.Sales
{
    public class UiEditorVendas : EditorVendas
    {
        HelperFunctions _Helpers = new HelperFunctions();

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            var DocVenda = this.DocumentoVenda;
            string matricula = "";
            StdBELista serie = BSO.Consulta($"SELECT CDU_PedeMatricula FROM SeriesVendas WHERE TipoDoc = '{DocVenda.Tipodoc}' AND Serie = '{DocVenda.Serie}';");

            if (!serie.Vazia()) {
                PSO.MensagensDialogos.MostraDialogoInput(ref matricula, "Matricula", "Matricula da viatura:", strValorDefeito: DocVenda.Matricula);
                DocVenda.Matricula = matricula;
            }
            base.AntesDeGravar(ref Cancel, e);
        }

        
        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            var DocVenda = this.DocumentoVenda;
            BasBE100.BasBEArtigo art = BSO.Base.Artigos.Edita(Artigo);

            if (art.CamposUtil["CDU_VendaEmCaixa"].Valor.Equals(true)) {
                // Kilos por caixa (permite decimal)
                string quilosCaixa = _Helpers.MostraInputForm("Quilos/caixa", "Quilos por caixa:", art.CamposUtil["CDU_KilosPorCaixa"].Valor.ToString());
                // Check input
                double x = 0.0;
                if (double.TryParse(quilosCaixa, out x)) {
                    DocVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_KilosPorCaixa"].Valor = x;
                } else {
                    PSO.MensagensDialogos.MostraAviso(String.Format("{0} não é um valor válido", quilosCaixa.ToString()), StdBSTipos.IconId.PRI_Exclama);
                    Cancel = true;
                }

                // Quantidade de caixas (inteiro)
                string caixas = _Helpers.MostraInputForm("Caixas", "Nro. de caixas:", "");
                // Check input
                int y = 0;
                if (int.TryParse(caixas, out y)) {
                    DocVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_Caixas"].Valor = y;
                    DocVenda.Linhas.GetEdita(NumLinha).Quantidade = y;
                } else {
                    PSO.MensagensDialogos.MostraAviso(String.Format("{0} não é um valor válido", caixas.ToString()), StdBSTipos.IconId.PRI_Exclama);
                    Cancel = true;
                }
                art = null;
            }

            // Pede fornecedor / origem
            StdBELista pedeFornecedor = BSO.Consulta($"SELECT CDU_PedeFornecedor FROM SeriesVendas WHERE TipoDoc = '{DocVenda.Tipodoc}' AND Serie = '{DocVenda.Serie}';");

            if (!pedeFornecedor.Vazia())
                pedeFornecedor.Inicio();

            if (pedeFornecedor.Valor("CDU_PedeFornecedor")) {
                string fornecedor = _Helpers.MostraInputForm("Proveniencia", "Fornecedor:", DocVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_Fornecedor"].Valor.ToString());

                DocVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_Fornecedor"].Valor = fornecedor.Trim().ToUpper();
            }

            DocVenda.Dispose();
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);
        }


        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            var DocVenda = DocumentoVenda;

            StdBELista serie = BSO.Consulta($"SELECT CDU_PedeVendedor, CDU_PedeDocumento, CDU_PedeMatricula FROM SeriesVendas WHERE TipoDoc IN ('{DocVenda.Tipodoc}', '{DocVenda.Serie}');");

            // Data de descarga igual à data de carga
            if (DocVenda.DataHoraDescarga == null) {
                DocVenda.DataHoraDescarga = DocVenda.DataHoraCarga;
            }

            if (!serie.Vazia()) {

                // Código do vendedor
                if (serie.Valor("CDU_PedeVendedor")) {

                    string vendedor = _Helpers.MostraInputForm("Vendedor", "Código do Vendedor:", "0");
                    StdBELista vend = BSO.Consulta($"SELECT Vendedor FROM Vendedores WHERE Vendedor = '{vendedor}';");

                    if (vend.Vazia()) {
                        PSO.MensagensDialogos.MostraAviso("Vendedor inexistente!", StdBSTipos.IconId.PRI_Exclama);
                    } else {
                        DocVenda.Responsavel = vendedor;
                    }
                }

                // Número do documento manual a lançar
                if (serie.Valor("CDU_PedeDocumento")) {
                    string nrDoc = _Helpers.MostraInputForm("Documento", "Número do Documento:", "0");

                    if (nrDoc != null)
                    {
                        try
                        {
                            DocVenda.CamposUtil["CDU_NroManual"].Valor = nrDoc.Trim().Substring(0, 10);
                        }
                        catch
                        {
                            PSO.MensagensDialogos.MostraErro("Número de documento inserido precisa de ter 10 dígitos.");
                            Cancel = true;
                        }
                    }
                }

                //Matricula
                if (serie.Valor("CDU_PedeMatricula")) {

                    if (DocVenda.TipoEntidade == "C") {
                        BasBE100.BasBECliente cli = BSO.Base.Clientes.Edita(Cliente);

                        if ((cli.CamposUtil["CDU_MatriculaHabitual"].Valor.ToString().Length > 0) && (DocVenda.Matricula == "")) {
                            DocVenda.Matricula = cli.CamposUtil["CDU_MatriculaHabitual"].Valor.ToString();
                        }

                        string matricula = _Helpers.MostraInputForm("Matricula", "Matricula da Viatura", DocVenda.Matricula);
                        DocVenda.Matricula = matricula;

                        cli.Dispose();
                    }
                }
            }

            serie.Dispose();
            base.ClienteIdentificado(Cliente, ref Cancel, e);
        }
    }
}