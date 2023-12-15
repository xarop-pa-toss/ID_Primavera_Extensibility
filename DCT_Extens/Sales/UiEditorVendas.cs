using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using BasBE100;
using System.Security.Policy;
using VndBE100;
using StdBE100;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace DCT_Extens.Sales
{
    public class UiEditorVendas : EditorVendas
    {
        private string _strMensagem;
        private const double DBL_LIMITE = 999;
        private VndBEDocumentoVenda dv = DocumentoVenda;

        public override void AntesDeImprimir(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeImprimir(ref Cancel, e);

            if (new List<string> { "GRL", "FAL" }.Contains(DocumentoVenda.Tipodoc) && DocumentoVenda.Entidade.Equals("21366"))
            {
                DocumentoVenda.MapaImpressao = "mapatest";
            }
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            string strCliente, resp, vend;
            long i;
            BasBECargaDescarga cargaDescarga;
            bool modNaoGravar = false;

            #region Validar CabecDoc
            // Mensagem a apresentar ao utilizador
            _strMensagem = "O valor desta fatura ultrapassa o limite definido (" + DBL_LIMITE.ToString("F2") + "). Deseja continuar com a gravação?";

            // Validações:
            // - Tipo de documento financeiro (4) de recebimento (R).
            // - Entidade é Cliente e se o cliente tem a validação de faturas activa.
            // - Total de documento é superior ao limite DBL_LIMITE definido no topo do ficheiro
            if (BSO.Vendas.TabVendas.Edita(dv.Tipodoc).TipoDocumento.Equals(4)
                && BSO.Vendas.TabVendas.Edita(dv.Tipodoc).PagarReceber.Equals("R")
                && dv.TipoEntidade.Equals("C")
                && (bool)BSO.Base.Clientes.Edita(dv.Entidade).CamposUtil["CDU_ValidaFatura"].Valor
                && (dv.TotalMerc + dv.TotalOutros + dv.TotalIva) > DBL_LIMITE
                && PSO.MensagensDialogos.MostraPerguntaSimples(_strMensagem))
            {
                Cancel = true;
            }
            #endregion

            #region Forçar Vendedor nas Linhas (vendedor) = Vendedor CabecDoc (responsável)
            if (BSO.Vendas.TabVendas.Edita(dv.Tipodoc).TipoDocumento.Equals(4))
            {
                foreach (VndBELinhaDocumentoVenda linha in dv.Linhas)
                {
                    if (dv.Responsavel != linha.Vendedor) { linha.Vendedor = dv.Responsavel; }
                }
            }
            #endregion

            #region Validar se artigo está bloqueado para descontos (não permite gravar)
            BasBEArtigo artigo = new BasBEArtigo();

            if (new List<string> { "FA", "FA1", "FA2", "FAP" }.Contains(DocumentoVenda.Tipodoc) && dv.Linhas.NumItens > 0)
            {
                foreach (VndBELinhaDocumentoVenda linha in dv.Linhas)
                {
                    artigo = BSO.Base.Artigos.Edita(linha.Artigo);
                    
                    if (linha.TipoLinha.Equals(10)
                        && (bool)artigo.CamposUtil["CDU_ArtBLOQD"].Valor
                        && (linha.DescontoComercial != 0 || dv.DescFinanceiro != 0 || dv.DescEntidade != 0))
                    {
                        string mensagem = "ATENÇÃO: \n" +
                            "Está a aplicar no Artigo \n" +
                            $"{linha.Artigo} - {linha.Descricao} \n" +
                            "um desconto e este encontra-se bloqueado para descontos!! \n\n" +
                            "Valide todos os descontos.";
                        PSO.MensagensDialogos.MostraAviso(mensagem, StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);

                        Cancel = true;
                    }
                }
            }
            #endregion

            #region Alteração de morada de descarga para entidade 13000
            if (dv.Entidade.Equals("13000") && string.IsNullOrEmpty(dv.MoradaEntrega))
            {
                FormCargaDescarga formCD = new FormCargaDescarga();
                // Vamos "subscrever" ao evento do FormClosed do form de modo a conseguirmos aceder às suas propriedades públicas enquanto ele fecha.
                // Temos de criar um método aqui que contenha a lógica para isso (get properties). A sintaxe é igual à do FormCargaDescarga_FormClosed dentro do form.
                formCD.FormClosed += FormCargaDescaga_FormClosed;
                formCD.Show();



            }
            #endregion
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            string motivoOferta = null, nz, d, ultimoMotivo;

            // O código que se segue irá permitir que surja uma inputbox quando é escolhido o cliente 13000, no momento do lançamento dos artigos
            // ao escrever o motivo de oferta nessa inputbox esse valor será gravado no campo de utilizador nas linhasdoc CDU_MotivoOferta.
            // Para que cada vez que se adiciona um artigo e surge a inputbox esta apresenta o ultimo motivo para que se possa fazer ENTER, caso o motivo seja o mesmo do anterior.
            // Para aparecer o anterior motivo, foi criada uma tabela de utilizador TDU_UM com um campo CDU_UltimoMotivo para armazenar esta informação.
            // Requisitos:
            // Criar Tabela de Utilizador TDU_UM com campo de utilizador CDU_UltimoMotivo para armazenar ultimo valor inserido
            // Criar Campo de Utilizador CDU_MotivoOferta nas LinhasDoc para armazenar o motivo de oferta
            // Criar Campo de Utilizador CDU_UltimoMotivo nas linhasdoc

            if (dv.Entidade == "13000" && new List<string> { "FA", "FAL", "FAP" }.Contains(dv.Tipodoc))
            {
                using (StdBELista ultimoMotivoLista = BSO.Consulta("SELECT CDU_UltimoMotivo FROM TDU_UM"))
                {
                    string titulo = dv.Linhas.GetEdita(NumLinha).CamposUtil["CDU_MotivoOferta"].Valor.ToString();

                    PSO.MensagensDialogos.MostraDialogoInput(ref motivoOferta, titulo, "Inserir Motivo de Oferta: ", strValorDefeito: ultimoMotivoLista.DaValor<string>("CDU_UltimoMotivo"));
                    MessageBox.Show("Test");
                    dv.Linhas.GetEdita(NumLinha).CamposUtil["CDU_MotivoOferta"].Valor = motivoOferta;

                    // SQL Updates ou Deletes têm de ser feitos desta forma com o StdBEExecSql
                    using (StdBEExecSql sql = new StdBEExecSql())
                    {
                        sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                        sql.Tabela = "TDU_UM";                          // UPDATE TDU_UM                                                  
                        sql.AddCampo("CDU_UltimoMotivo", motivoOferta); // SET CDU_UltimoMotivo                                                  

                        sql.AddQuery();

                        // Se Update falhar, preenche lista com NumDoc para mostrar ao cliente.
                        try
                        {
                            PSO.ExecSql.Executa(sql);
                        }
                        catch
                        {
                            MessageBox.Show("Erro ao actualizar CDU_UltimoMotivo na tabela TDU_UM");
                        }
                    }
                }
            }
        }

        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);

            if (Cliente.Equals("13000"))
            {
                BasBECargaDescarga cdl = new BasBECargaDescarga
                {
                    MoradaCarga = dv.CargaDescarga.MoradaCarga,
                    Morada2Carga = dv.CargaDescarga.Morada2Carga,
                    LocalidadeCarga = dv.CargaDescarga.LocalidadeCarga,
                    CodPostalCarga = dv.CargaDescarga.CodPostalCarga,
                    CodPostalLocalidadeCarga = dv.CargaDescarga.CodPostalLocalidadeCarga,
                    DistritoCarga = dv.CargaDescarga.DistritoCarga,
                    PaisCarga = dv.CargaDescarga.PaisCarga,
                    EntidadeEntrega = "13000"
                };

                dv.CargaDescarga = cdl;
            }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            //*** SSCC código GS1 ***
            GS1_Geral GS1 = new GS1_Geral(Filial, Serie, Tipo, NumDoc, dv, BSO, PSO);
            GS1.EditorVendas_DepoisDeGravar();
        }

        public override void DepoisDeTransformar(ExtensibilityEventArgs e)
        {
            base.DepoisDeTransformar(e);
            BasBEArtigo artigo;
            StdBELista precUnitDesc;
            double precUnit;

            if (dv.TipoEntidade.Equals("C") && (bool)BSO.Base.Clientes.Edita(dv.Entidade).CamposUtil["CDU_VALIDAPRECO"].Valor)
            {
                foreach (VndBELinhaDocumentoVenda linha in dv.Linhas)
                {
                    precUnitDesc = null;

                    if (linha.TipoLinha.Equals("10"))
                    {
                        artigo = BSO.Base.Artigos.Edita(linha.Artigo);
                        precUnit = linha.PrecUnit;
                        precUnitDesc = BSO.Consulta("" +
                            " SELECT Preco" +
                            " FROM REgrasDescPrec" +
                            " WHERE Campo1 = N" + dv.Entidade +
                            " AND Campo2 = N" + linha.Artigo);

                        if (precUnitDesc.Vazia()) { PSO.MensagensDialogos.MostraAviso($"O artigo {linha.Artigo} não tem valores no Cardex!", StdPlatBS100.StdBSTipos.IconId.PRI_Critico); }
                        else if (precUnit != precUnitDesc.Valor(0)) { PSO.MensagensDialogos.MostraAviso($"O valor da encomenda do artigo {linha.Artigo} não coincide com o valor no Cardex!", StdPlatBS100.StdBSTipos.IconId.PRI_Critico); }
                    }
                    // não parece ser necessário
                    // precunit = 0
                }
            }
        }

        public override void ValidaLinha(int NumLinha, ExtensibilityEventArgs e)
        {
            base.ValidaLinha(NumLinha, e);

            BasBEArtigo artigo;
            VndBELinhaDocumentoVenda linha = dv.Linhas.GetEdita(NumLinha);

            if (new List<string> { "FA", "FA1", "FA2", "FAL", "FAP" }.Contains(dv.Tipodoc))
            {
                artigo = BSO.Base.Artigos.Edita(linha.Artigo);

                if ((bool)artigo.CamposUtil["CDU_ARTBLOQD"].Valor
                    && (linha.DescontoComercial != 0
                        || dv.DescFinanceiro != 0
                        || dv.DescEntidade != 0))
                {
                    PSO.MensagensDialogos.MostraAviso(
                        "ATENÇÃO:\n" +
                        "Está a aplicar no Artigo\n" +
                        $"{linha.Artigo} - {linha.Descricao}", StdPlatBS100.StdBSTipos.IconId.PRI_Critico);

                    dv.Linhas.Remove(NumLinha);
                }
            }

            // *** Se Quantidade tiver casas decimais, avisar
            // Truncate devolve apenas a parte inteira de um dado número. Se o número truncado for igual ao original, o original não tinha casas decimais.
            // Truncate não arredonda. ex: 10.75 truncado = 10
            if (linha.Quantidade != Math.Truncate(linha.Quantidade))
            {
                PSO.MensagensDialogos.MostraAviso(
                    "ATENÇÃO:\n" +
                    "Está a inserir uma quantidade não inteira (com casas decimais).");
            }
        }


        private void FormCargaDescaga_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
