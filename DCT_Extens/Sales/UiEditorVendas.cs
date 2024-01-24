using BasBE100;
using HelpersPrimavera10;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Extensions;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VndBE100;

namespace DCT_Extens.Sales
{
    public class UiEditorVendas : EditorVendas
    {
        private HelperFunctions _Helpers = new HelperFunctions();
        private string _strMensagem;
        private const double DBL_LIMITE = 999;

        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);

            #region Processamento de ofertas (cliente 13000 / FormCargaDescarga)
            if (Cliente.Equals("13000"))
            {
                BasBECargaDescarga cdl = new BasBECargaDescarga
                {
                    MoradaCarga = DocumentoVenda.CargaDescarga.MoradaCarga,
                    Morada2Carga = DocumentoVenda.CargaDescarga.Morada2Carga,
                    LocalidadeCarga = DocumentoVenda.CargaDescarga.LocalidadeCarga,
                    CodPostalCarga = DocumentoVenda.CargaDescarga.CodPostalCarga,
                    CodPostalLocalidadeCarga = DocumentoVenda.CargaDescarga.CodPostalLocalidadeCarga,
                    DistritoCarga = DocumentoVenda.CargaDescarga.DistritoCarga,
                    PaisCarga = DocumentoVenda.CargaDescarga.PaisCarga,
                    EntidadeEntrega = "13000"
                };

                DocumentoVenda.CargaDescarga = cdl;
            }
            #endregion
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            #region Processamento de ofertas (cliente 13000 / FormCargaDescarga)
            string motivoOferta = null;

            // O código que se segue irá permitir que surja uma inputbox quando é escolhido o cliente 13000, no momento do lançamento dos artigos
            // ao escrever o motivo de oferta nessa inputbox esse valor será gravado no campo de utilizador nas linhasdoc CDU_MotivoOferta.
            // Para que cada vez que se adiciona um artigo e surge a inputbox esta apresenta o ultimo motivo para que se possa fazer ENTER, caso o motivo seja o mesmo do anterior.
            // Para aparecer o anterior motivo, foi criada uma tabela de utilizador TDU_UM com um campo CDU_UltimoMotivo para armazenar esta informação.
            // Requisitos:
            // Criar Tabela de Utilizador TDU_UM com campo de utilizador CDU_UltimoMotivo para armazenar ultimo valor inserido
            // Criar Campo de Utilizador CDU_MotivoOferta nas LinhasDoc para armazenar o motivo de oferta
            // Criar Campo de Utilizador CDU_UltimoMotivo nas linhasdoc

            if (DocumentoVenda.Entidade == "13000" && new List<string> { "FA", "FAL", "FAP" }.Contains(DocumentoVenda.Tipodoc))
            {
                using (StdBELista ultimoMotivoLista = BSO.Consulta("SELECT CDU_UltimoMotivo FROM TDU_UM"))
                {
                    string titulo = DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_MotivoOferta"].Valor.ToString();
                    motivoOferta = _Helpers.MostraInputForm(titulo, "Inserir Motivo de Oferta", ultimoMotivoLista.DaValor<string>("CDU_UltimoMotivo"));
                    DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_MotivoOferta"].Valor = motivoOferta;

                    // Se motivo ficar nulo, apaga linha
                    if (!string.IsNullOrEmpty(motivoOferta))
                    {
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
                                string erro = "Erro ao actualizar CDU_UltimoMotivo na tabela TDU_UM";
                                _Helpers.EscreverParaFicheiroTxt(erro, "EditorVendas_ArtigoIdentificado_UltimoMotivo");
                                PSO.MensagensDialogos.MostraErro(erro);
                            }
                        }
                    } else
                    {
                        PSO.MensagensDialogos.MostraErro("Não foi dado um motivo de quebra para este artigo." + Environment.NewLine + "A linha será apagada.");
                        DocumentoVenda.Linhas.Remove(NumLinha);

                    }
                }
            }
            #endregion
        }

        public override void ValidaLinha(int NumLinha, ExtensibilityEventArgs e)
        {
            base.ValidaLinha(NumLinha, e);

            #region Validação de quantidades não inteiras
            VndBELinhaDocumentoVenda linha = DocumentoVenda.Linhas.GetEdita(NumLinha);

            // *** Se Quantidade tiver casas decimais, avisar
            // Truncate devolve apenas a parte inteira de um dado número. Se o número truncado for igual ao original, o original não tinha casas decimais.
            // Truncate não arredonda. ex: 10.75 truncado = 10
            if (linha.Quantidade != Math.Truncate(linha.Quantidade))
            {
                PSO.MensagensDialogos.MostraAviso(
                    "ATENÇÃO:" + Environment.NewLine +
                    "Está a inserir uma quantidade não inteira (com casas decimais).");
            }
            #endregion
        }

        public override void DepoisDeTransformar(ExtensibilityEventArgs e)
        {
            base.DepoisDeTransformar(e);

            #region Validações Cardex
            BasBEArtigo artigo;
            StdBELista precUnitDesc;
            double precUnit;

            if (DocumentoVenda.TipoEntidade.Equals("C") && (bool)BSO.Base.Clientes.Edita(DocumentoVenda.Entidade).CamposUtil["CDU_VALIDAPRECO"].Valor)
            {
                foreach (VndBELinhaDocumentoVenda linha in DocumentoVenda.Linhas)
                {
                    precUnitDesc = null;

                    if (linha.TipoLinha.Equals("10"))
                    {
                        artigo = BSO.Base.Artigos.Edita(linha.Artigo);
                        precUnit = linha.PrecUnit;
                        precUnitDesc = BSO.Consulta("" +
                            " SELECT Preco" +
                            " FROM RegrasDescPrec" +
                            " WHERE Campo1 = N" + DocumentoVenda.Entidade +
                            " AND Campo2 = N" + linha.Artigo);

                        if (precUnitDesc.Vazia()) { PSO.MensagensDialogos.MostraAviso($"O artigo {linha.Artigo} não tem valores no Cardex!", StdPlatBS100.StdBSTipos.IconId.PRI_Critico); } else if (precUnit != precUnitDesc.Valor(0)) { PSO.MensagensDialogos.MostraAviso($"O valor da encomenda do artigo {linha.Artigo} não coincide com o valor no Cardex!", StdPlatBS100.StdBSTipos.IconId.PRI_Critico); }
                    }
                }
            }
            #endregion
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            #region Validação de valor final de documento
            // Mensagem a apresentar ao utilizador
            _strMensagem = "O valor desta fatura ultrapassa o limite definido (" + DBL_LIMITE.ToString("F2") + "). Deseja continuar com a gravação?";

            // Validações:
            // - Tipo de documento financeiro (4) de recebimento (R).
            // - Entidade é Cliente e se o cliente tem a validação de faturas activa.
            // - Total de documento é superior ao limite DBL_LIMITE definido no topo do ficheiro
            if (BSO.Vendas.TabVendas.Edita(DocumentoVenda.Tipodoc).TipoDocumento.Equals(4)
                && BSO.Vendas.TabVendas.Edita(DocumentoVenda.Tipodoc).PagarReceber.Equals("R")
                && DocumentoVenda.TipoEntidade.Equals("C")
                && (bool)BSO.Base.Clientes.Edita(DocumentoVenda.Entidade).CamposUtil["CDU_ValidaFatura"].Valor
                && (DocumentoVenda.TotalMerc + DocumentoVenda.TotalOutros + DocumentoVenda.TotalIva) > DBL_LIMITE
                && !PSO.MensagensDialogos.MostraPerguntaSimples(_strMensagem))
            {
                Cancel = true;
                return;
            }
            #endregion

            #region Alteração de campo Vendedor nas linhas para corresponder ao campo Reponsavel do cabeçalho
            if (BSO.Vendas.TabVendas.Edita(DocumentoVenda.Tipodoc).TipoDocumento.Equals(4))
            {
                foreach (VndBELinhaDocumentoVenda linha in DocumentoVenda.Linhas)
                {
                    if (DocumentoVenda.Responsavel != linha.Vendedor) { linha.Vendedor = DocumentoVenda.Responsavel; }
                }
            }
            #endregion

            #region Validar se artigo está bloqueado para descontos (não permite gravar)
            BasBEArtigo artigo;
            List<string> docsList = new List<string> { "FA", "FA1", "FA2", "FAL", "FAP" };

            if (docsList.Contains(DocumentoVenda.Tipodoc))
            {
                foreach (VndBELinhaDocumentoVenda linha in DocumentoVenda.Linhas)
                {
                    artigo = BSO.Base.Artigos.Edita(linha.Artigo);

                    // CDU_ARTBLOQD está para retornar null por defeito (se nunca foi picado) o que é... mau.
                    // O bool abaixo é true se o valorCDU não for nulo nem falso.
                    object valorCDU = artigo.CamposUtil["CDU_ARTBLOQD"].Valor;
                    bool ArtigoAnulacaoBloqueada = valorCDU as bool? ?? false;

                    if (ArtigoAnulacaoBloqueada
                        && (linha.DescontoComercial != 0 ||
                            DocumentoVenda.DescFinanceiro != 0 ||
                            DocumentoVenda.DescEntidade != 0))
                    {
                        string mensagem =
                            "ATENÇÃO: " + Environment.NewLine +
                            "Está a aplicar no Artigo " + Environment.NewLine +
                            $"{linha.Artigo} - {linha.Descricao}" + Environment.NewLine +
                            "um desconto e este encontra-se bloqueado para descontos!!" + Environment.NewLine + Environment.NewLine +
                            "Valide todos os descontos.";

                        PSO.MensagensDialogos.MostraAviso(mensagem, StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);

                        Cancel = true;
                        return;
                    }
                }
            }
            #endregion

            #region Processamento de ofertas (cliente 13000 / FormCargaDescarga)
            if (DocumentoVenda.Entidade.Equals("13000") && string.IsNullOrEmpty(DocumentoVenda.MoradaEntrega))
            {
                using (var formInstancia = BSO.Extensibility.CreateCustomFormInstance(typeof(FormCargaDescarga)))
                {
                    if (formInstancia.IsSuccess())
                    {
                        FormCargaDescarga formCargaDescarga = formInstancia.Result as FormCargaDescarga;
                        DialogResult resultado = formCargaDescarga.ShowDialog();

                        if (resultado == DialogResult.OK)
                        {
                            if (formCargaDescarga.cdForm != null)
                            {
                                DocumentoVenda.CargaDescarga.MoradaEntrega = formCargaDescarga.cdForm.MoradaEntrega;
                                DocumentoVenda.CargaDescarga.Morada2Entrega = formCargaDescarga.cdForm.Morada2Entrega;
                                DocumentoVenda.CargaDescarga.LocalidadeEntrega = formCargaDescarga.cdForm.LocalidadeEntrega;
                                DocumentoVenda.CargaDescarga.CodPostalEntrega = formCargaDescarga.cdForm.CodPostalEntrega;
                                DocumentoVenda.CargaDescarga.CodPostalLocalidadeEntrega = formCargaDescarga.cdForm.CodPostalLocalidadeEntrega;
                                DocumentoVenda.CargaDescarga.DistritoEntrega = formCargaDescarga.cdForm.DistritoEntrega;
                                DocumentoVenda.CargaDescarga.PaisEntrega = formCargaDescarga.cdForm.PaisEntrega;
                            } else
                            {
                                string erro = "Dados de morada de Carga e Descarga não foram alterados para o cliente 13000." + Environment.NewLine +
                                    "O documento não será gravado." + Environment.NewLine +
                                    "Por favor contacte a Infodinâmica.";

                                _Helpers.EscreverParaFicheiroTxt(erro, "EditorVendas_AntesDeGravar_CargaDescarga");
                                PSO.MensagensDialogos.MostraErro(erro);
                                Cancel = true;
                                return;
                            }
                        }
                    }
                }
            }
            #endregion
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            #region Códigos GS1 / SSCC
            // *** SSCC código GS1 ***
            BasBECliente objCliente = BSO.Base.Clientes.Consulta(DocumentoVenda.Entidade);
            if ((bool)objCliente.CamposUtil["CDU_SSCC"].Valor)
            {
                GS1_Geral GS1 = new GS1_Geral(DocumentoVenda);
                GS1.EditorVendas_DepoisDeGravar();
            }
            #endregion
        }

        // AntesDeImprimir está com "mapatest". Alterar pro real.
        public override void AntesDeImprimir(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeImprimir(ref Cancel, e);

            #region Alteração de mapa para cliente 21366
            if (new List<string> { "GRL", "FAL" }.Contains(DocumentoVenda.Tipodoc) && DocumentoVenda.Entidade.Equals("21366"))
            {
                DocumentoVenda.MapaImpressao = "mapatest";
            }
            #endregion
        }

        private void FormCargaDescarga_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
