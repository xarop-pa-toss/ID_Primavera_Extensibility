using Primavera.Extensibility.CustomForm;
using StdBE100;
using BasBE100;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

/*
 * ERROS:
 * 
 * 
 * 
 * TODO:
 * - 
 * - Ver especificações do CodIva em processarDados()
 * - Voltar à base após erro!!
 * - Mudar path hardcoded do ficheiro em servidor.
 * 
 * 
 * DONE:
 * - 03/06 - Tratamento de erros 
 * - 01/06 - Verificação do IVA em validarFicheiro().
 * 
 * 
 * EDITS:
 * - Restrições de validação são agora com qualquer elemento das listas.
 */

namespace ASRLB_ImportacaoFatura
{
    public partial class janelaImportarFatura : CustomForm
    {
        public janelaImportarFatura()
        {
            InitializeComponent();
        }
        
        //OK
        internal void btnIniciar_Click(object sender, EventArgs e)
        {
            string ficheiro = null;
            try
            {
                // Lê conteudo da txtBox e valida se ficheiro existe ou é nulo.
                // Copia o ficheiro para o mesmo directório e retorna o path da cópia.
                ficheiro = validarPath();

                // Cria listas fáceis de iterar para evitar chamar métodos Primavera. Alteradas dentro do método "por referência".
                List<string> listaArtigos = new List<string>();
                List<string> listaClientes = new List<string>();
                List<string> listaCondPag = new List<string>();
                criarListasPri(ref listaArtigos, ref listaClientes, ref listaCondPag);


                List<string> valoresIva = new List<string> { "06,0", "12,0", "23,0" };
                List<string> codigosIva = new List<string> { "22", "4", "23" };
                // Valida dados no ficheiro comparando com os das listas.
                string[] linhasFicheiro = File.ReadAllLines(@"" + ficheiro);
                validarFicheiro(linhasFicheiro, ref listaArtigos, ref listaClientes, ref listaCondPag, valoresIva);

                // Manipula o Editor de Vendas e comete alterações
                processarDados(linhasFicheiro, valoresIva, codigosIva);
            }
            catch (Exception error)
            {
                listBox.Items.Add(String.Format("ERRO: {0}", error));
                PSO.Dialogos.MostraErro(String.Format("ERRO: {0}", error));
                File.Delete(ficheiro);
            }
        }


        //OK
        internal string validarPath()
        {
            string path;
            if (txtFicheiroPath.Text == "import.txt no servidor")
            {
                path = @"C:\Users\Ricardo Santos\Documents\import.txt";
            }
            else { path = txtFicheiroPath.Text; }

            if (!File.Exists(path) || path == "")
            {
                PSO.Dialogos.MostraErro("O ficheiro não existe no caminho específicado");
            }

            // Faz cópia do ficheiro para o mesmo directório. Trabalhamos a cópia e não o original.
            string pasta = Path.GetDirectoryName(path);
            string copiaPath = @"" + pasta + @"\importCopia.txt";
            File.Copy(path, copiaPath);

            return copiaPath;
        }


        //OK
        internal void criarListasPri(ref List<string> listaArtigos, ref List<string> listaClientes, ref List<string> listaCondPag)
        {
            // CONTEM TEST PRINTS EM CADA LOOP
            // Abre lista Primavera -> Adiciona caso base a lista interna -> Adiciona restantes -> Fecha lista Primavera
            StdBELista proxyArtigos = BSO.Base.Artigos.LstArtigos();
            listaArtigos.Add(proxyArtigos.Valor(0));
            for (int i = 1; proxyArtigos.NumLinhas() >= i; i++)
            {
                listaArtigos.Add(proxyArtigos.Valor(0));
                proxyArtigos.Seguinte();
                listBox_test.Items.Add(String.Format("Artigos: -{0}-", listaArtigos[i]));
            }
            proxyArtigos.Termina();

            StdBELista proxyClientes = BSO.Base.Clientes.LstClientes();
            listaClientes.Add(proxyClientes.Valor(0));
            for (int i = 1; proxyClientes.NumLinhas() >= i; i++)
            {
                listaClientes.Add(proxyClientes.Valor("Cliente"));
                proxyClientes.Seguinte();
                listBox_test.Items.Add(String.Format("Clientes: -{0}-", listaClientes[i]));
            }
            proxyClientes.Termina();

            StdBELista proxyCondPag = BSO.Base.CondsPagamento.LstCondsPagamento();
            listaCondPag.Add(proxyCondPag.Valor(0));
            for (int i = 1; proxyCondPag.NumLinhas() >= i; i++)
            {
                listaCondPag.Add(proxyCondPag.Valor(0));
                proxyCondPag.Seguinte();
                listBox_test.Items.Add(String.Format("CondPag: -{0}-", listaCondPag[i]));
            }
            proxyCondPag.Termina();
        }


        //OK
        internal void validarFicheiro(string[] linhasFicheiro, ref List<string> listaArtigos, ref List<string> listaClientes, ref List<string> listaCondPag, List<string> valoresIva)
        {
            // Cria array que vai conter cada linha do ficheiro
            string[] linha;

            // Valida os valores (separados por ',') com os presentes nas listas criadas
            for (int i = 0; i < linhasFicheiro.Count(); i++)
            {
                linha = linhasFicheiro[i].Split(',');
                if (linha.Count() == 2)
                {
                    // TEST PRINT
                    listBox.Items.Add(String.Format("validarFicheiro -> Linha 0: {0}, Linha 1: {1}", linha[0], linha[1]));
                    if (!listaClientes.Contains(linha[0]) || !listaCondPag.Contains(linha[1]))
                    {
                        restartOnError(String.Format("Cliente {0} ou Condição de Pagamento {1} inválido na linha {2}.", linha[0], linha[1], i.ToString()));
                    }
                }
                else if (linha.Count() == 6)
                {   
                    // TEST PRINT
                    listBox.Items.Add(String.Format("validarFicheiro -> Linha 0: {0}, Linha 1: {1}, Linha 2: {2}, Linha 3: {3}, Linha 4: {4}, Linha 5: {5}", linha[0], linha[1], linha[2], linha[3], linha[4], linha[5]));
                    if (!listaArtigos.Contains(linha[0]))
                    {
                        restartOnError(String.Format("Código de Artigo {0} inválido na linha {1}.", linha[0], (i + 1).ToString()));
                    }
                    else if (!valoresIva.Contains(linha[4].Replace(".", ",")))
                    {
                        restartOnError(String.Format("Valor do IVA {0} inválido na linha {1}.", linha[5], (i + 1).ToString()));
                    }
                }
                else
                {
                    restartOnError(String.Format("Linha {0} contém valores inválidos ou insuficientes.", (i + 1).ToString()));
                }
            }
        }


        //ERRO - System.FormatException: Cadeia de caracteres de entrada com formato incorrecto.
        internal void processarDados(string[] linhasFicheiro, List<string> valoresIva, List<string> codigosIva)
        {
            string[] linha;
            bool temFatura = false;
            var docVenda = new VndBE100.VndBEDocumentoVenda();
            var docVendaLinha = new VndBE100.VndBELinhaDocumentoVenda();
            int vdDadosClientes = ((int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosCliente);
            int vdDadosTodos = ((int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosTodos);
            BSO.IniciaTransaccao();

            for (int i = 0; i < linhasFicheiro.Count(); i++)
            {
                linha = linhasFicheiro[i].Split(',');

                // TEST PRINT
                listBox.Items.Add(String.Format("processarDados -> Linha 0: {0}, Linha 1: {1}", linha[0], linha[1]));

                if (linha.Count() == 2)
                {
                    if (temFatura)
                    {
                        BSO.TerminaTransaccao();
                        BSO.IniciaTransaccao();
                    }
                    // TEST PRINT START
                    for (int u = 0; u < linha.Length; u++)
                    {
                        listBox.Items.Add(linha[u]);
                        //listBox.Items.Add(String.Format("Linha 0: {0}, Linha 1: {1}", linha[0], linha[1]));
                    }
                    // END TEST PRINT

                    // Preenche campos do CabecDoc
                    docVenda.Entidade = linha[0];
                    docVenda.TipoEntidade = "C";
                    docVenda.Tipodoc = "FTEVB";
                    docVenda.Serie = BSO.Base.Series.DaSerieDefeito("V", docVenda.Tipodoc);
                    docVenda = BSO.Vendas.Documentos.PreencheDadosRelacionados(docVenda, ref vdDadosClientes);

                    docVenda.CondPag = linha[1];
                    docVenda.DataDoc = DateTime.Today;
                    BSO.Vendas.Documentos.PreencheDadosRelacionados(docVenda, ref vdDadosTodos);

                    temFatura = false;
                }
                else if (linha.Count() == 6)
                {
                    // TEST PRINT
                    listBox.Items.Add(String.Format("Linha 0: {0}, Linha 1: {1}, Linha 2: {2} Linha 3: {3}, Linha 4: {4}, Linha 5: {5}", linha[0], linha[1], linha[2], linha[3], linha[4], linha[5]));
                    docVendaLinha.Artigo = linha[0].Trim();
                    docVendaLinha.Descricao = linha[1].Trim();
                    docVendaLinha.Quantidade = Convert.ToDouble((linha[2].Replace('.', ',')).Trim());
                    docVendaLinha.PrecUnit = Convert.ToDouble((linha[3].Replace('.', ',')).Trim());
                    docVendaLinha.TaxaIva = Convert.ToSingle((linha[4].Replace('.', ',')).Trim());

                    int indexIva = valoresIva.IndexOf((linha[4].Replace('.', ',')).Trim());
                    docVendaLinha.CodIva = codigosIva[indexIva];
                    

                    // Adiciona linha ao Doc
                    docVenda.Linhas.Add(docVendaLinha);
                    temFatura = true;

                    // Verifica se última fatura linha no ficheiro
                    if (i == linhasFicheiro.Count() - 1)
                    {
                        BSO.TerminaTransaccao();
                        break;
                    }
                }
            }
        }


        public void restartOnError(string error, string exception = "")
        {
            if (error == null)
            {
                error = "Erro inesperado não definido.";
            }
            listBox.Items.Add(error);            
            PSO.Dialogos.MostraErro(error);

            if (exception != "")
            {
                PSO.Dialogos.MostraErro(exception);
            }

        using (var result = BSO.Extensibility.CreateCustomFormInstance(typeof(janelaImportarFatura)))
        {
                this.Close();
                new janelaImportarFatura();
            (result.Result as janelaImportarFatura).Show();
        }
    }

        private void btnEscolherFicheiro_Click(object sender, EventArgs e)
        {
            OpenFileDialog form = new OpenFileDialog();

            form.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; ;
            form.FilterIndex = 2;
            form.Multiselect = false;
            form.RestoreDirectory = true;

            if (form.ShowDialog() == DialogResult.OK)
            {
                string ficheiro = form.FileName;
                txtFicheiroPath.Text = ficheiro;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}