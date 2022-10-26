<<<<<<< Updated upstream
﻿using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using Primavera.Extensibility.Sales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using StdBE100;
using IBasBS100;
using ErpBS100;
using IVndBS100;
=======
﻿using Primavera.Extensibility.CustomForm;
using System; using System.Collections.Generic; using System.IO; using System.Linq; using System.Windows.Forms;
using StdBE100; using BasBE100;
>>>>>>> Stashed changes

/*
 * TODO:
 * - Mudar path hardcoded do ficheiro em servidor
 * - Verificação do IVA em validarFicheiro();
 * - Imprimir pra listBox em vez de msgBox
 * 
 * Aguarda resposta
 * - https://developers.primaverabss.com/en/questions/qual-a-forma-mais-eficiente-de-lancar-faturas/
 * 
 * EDITS:
 * - Restrições de validação são agora com qualquer elemento das listas.
 */

namespace ASRLB_ImportacaoFatura
{
    public partial class janelaImportarFatura : CustomForm
    {
        internal void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                // Lê conteudo da txtBox e valida se ficheiro existe ou é nulo.
                // Copia o ficheiro para o mesmo directório e retorna o path da cópia.
                string ficheiro = validarPath();

<<<<<<< Updated upstream
                // Cria listas fáceis de iterar para evitar chamar métodos Primavera. Populadas dentro do método "por referência".
                List<string> listaArtigos = new List<string>();
                List<string> listaClientes = new List<string>();
                List<string> listaCondPag = new List<string>();
                criarListasPri(ref listaArtigos, ref listaClientes, ref listaCondPag);

                string[] linhasFicheiro = File.ReadAllLines(@"" + ficheiro);
                // Valida dados no ficheiro comparando com os das listas.
                validarFicheiro(linhasFicheiro, ref listaArtigos, ref listaArtigos, ref listaCondPag);

                //processarDados();
            }
            catch
            {
                PSO.Dialogos.MostraErro("ERRO");
            }
        }

=======
        //OK
        internal void btnIniciar_Click(object sender, EventArgs e)
        {
            /* ** INTERRUPÇÃO DE EXECUÇÃO POR FAIL STATES **
                        Os métodos validarFicheiro() e processarDados() são chamados através de if's que determinam o seu resultado (valor do return). Se return true, continua; se return false, então há um outro return void ao método pai (btnIniciar_Click()) que interrompe a sua execução.
                        O return false acontece quando o método interrompeComErro() é chamado.
                        O método validarPath() retorna string e não bool, por isso a sua validação é feita através do resultado do path do ficheiro, igualmente tratado pelo interrompeComErro() */

            //Lê conteudo da txtBox e valida se ficheiro existe.Copia o ficheiro para o mesmo directório e retorna o path da cópia.
            listBox.Items.Clear();
            ficheiro = validarPath();
            if (ficheiro == "") return;

            // Cria listas em memória fáceis de iterar para evitar chamar métodos Primavera. Acedidos por referência (morada na memória em vez do valor da variável).
            List<string> listaArtigos = new List<string>();
            List<string> listaClientes = new List<string>();
            List<string> listaCondPag = new List<string>();
            criarListasPri(ref listaArtigos, ref listaClientes, ref listaCondPag);

            // Valida dados no ficheiro comparando com os das listas criadas em criarListasPri();
            List<string> valoresIva = new List<string> { "06,0", "12,0", "23,0" };
            string[] linhasFicheiro = File.ReadAllLines(@"" + ficheiro);
            int linhasFicheiroTotal = linhasFicheiro.Count();
            if (!validarFicheiro(linhasFicheiro, ref listaArtigos, ref listaClientes, ref listaCondPag, valoresIva, linhasFicheiroTotal)) { return; }

            // Cria um DocumentoVendas para cada Cliente e preenche com as Linhas correspondentes. Quando recebe novo Cliente, valida os dados e grava a fatura.
            if (!processarDados(linhasFicheiro, linhasFicheiroTotal)) { return; }

            File.Delete(ficheiro);
        }

        //OK
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

            // Faz cópia do ficheiro para o mesmo directório. Trabalhamos a cópia e não o original.
            string pasta = Path.GetDirectoryName(path);
            string copiaPath = @"" + pasta + @"\importCopia.txt";
            File.Copy(path, copiaPath);

            return copiaPath;
        }


=======
            catch (FileNotFoundException e)
            { PSO.MensagensDialogos.MostraErro("O ficheiro não existe no caminho específicado"); interromperComErro(e.ToString()); return ""; }
            catch (Exception e)
            { PSO.MensagensDialogos.MostraErro("ERRO: " + e); interromperComErro(e.ToString()); return ""; }
        }

        //OK
>>>>>>> Stashed changes
        internal void criarListasPri(ref List<string> listaArtigos, ref List<string> listaClientes, ref List<string> listaCondPag)
        {
            // Métodos Primavera que devolvem listas internas
            StdBELista proxyArtigos = BSO.Base.Artigos.LstArtigos();
            StdBELista proxyClientes = BSO.Base.Clientes.LstClientes();
            StdBELista proxyCondPag = BSO.Base.CondsPagamento.LstCondsPagamento();

            for (int i = 0; proxyArtigos.NumLinhas() > i; i++)
            {
                listaArtigos.Add(proxyArtigos.DaValor<string>("Artigo"));
            }

            for (int i = 0; proxyClientes.NumLinhas() > i; i++)
            {
                listaClientes.Add(proxyClientes.DaValor<string>("Cliente"));
            }

            for (int i = 0; proxyCondPag.NumLinhas() > i; i++)
            {
                listaCondPag.Add(proxyCondPag.DaValor<string>("CondPag"));
            }
        }

<<<<<<< Updated upstream

        internal void validarFicheiro(string[] linhasFicheiro, ref List<string> listaArtigos, ref List<string> listaClientes, ref List<string> listaCondPag)
=======
        //OK
        internal bool validarFicheiro(string[] linhasFicheiro, ref List<string> listaArtigos, ref List<string> listaClientes, ref List<string> listaCondPag, List<string> valoresIva, int linhasFicheiroTotal)
>>>>>>> Stashed changes
        {
            // Cria array para cada linha do ficheiro
            string[] linha;
            // Controla se há pelo menos uma linha por cliente
            int controlo;
            List<string> valoresIva = new List<string> { "06.0", "13.0", "23.0" };

            // Valida os valores (separados por ',') com os presentes nas listas criadas
            for (int i = 1; i < linhasFicheiro.Count(); i++)
            {
                linha = linhasFicheiro[i].Split(',');

                if (linha.Count() == 2)
                {
                    if (!listaClientes.Contains(linha[0]) && !listaCondPag.Contains(linha[1]))
                    {
                        listBox.Items.Add(String.Format("Cliente {0} ou Condição de Pagamento {1} inválido na linha {2}.", linha[0], linha[1], i.ToString()));
                        break;
                    }
                }
                else if (linha.Count() == 6)
                {
                    if (!listaArtigos.Contains(linha[0]))
                    {
                        listBox.Items.Add(String.Format("Código de Artigo {0} inválido na linha {1}.", linha[0], (i + 1).ToString()));
                        break;
                    }
                    else if (!valoresIva.Contains(linha[4]))
                    {
                        listBox.Items.Add(String.Format("Código do IVA {0} inválido na linha {1}.", linha[5], (i + 1).ToString()));
                        break;
                    }
                }
                else
                {
                    listBox.Items.Add(String.Format("Linha {0} contém valores inválidos ou insuficientes.", (i + 1).ToString()));
                    break;
                }
            }
        }

<<<<<<< Updated upstream

        internal void processarDados(string[] linhasFicheiro, ref List<string> listaArtigos, ref List<string> listaClientes, ref List<string> listaCondPag)
=======
        //OK
        internal bool processarDados(string[] linhasFicheiro, int linhasFicheiroTotal)
>>>>>>> Stashed changes
        {
            var docVenda = new VndBE100.VndBEDocumentoVenda();
            var docVendaLinhas = docVenda.Linhas.GetEdita(0);
            string[] linha;

            // Preenchimento "manual" de primeiro cliente para evitar um 'if' que só corre uma vez
            linha = linhasFicheiro[0].Split(',');
            docVenda.Entidade = linha[0];
            docVenda.TipoEntidade = "C";
            docVenda.Tipodoc = "FTEVB";
            docVenda.Serie = BSO.Base.Series.DaSerieDefeito("V", "FTEVB");

            docVenda.CondPag = linha[1];


            for (int i = 1; i < linhasFicheiro.Count(); i++)
            {
                linha = linhasFicheiro[i].Split(',');

                if (linha.Count() == 2)
                {
                    docVenda.Tipodoc = "FTEVB";
                    docVenda.TipoEntidade = "C";
                    docVenda.Serie = BSO.Base.Series.DaSerieDefeito("V", "FTEVB");
                    docVenda.Entidade = linha[0];
                    docVenda.CondPag = linha[1];

                }
                else if (linha.Count() == 6)
                {
                    docVendaLinhas.Artigo = linha[0];
                    docVendaLinhas.Descricao = linha[1];
                    docVendaLinhas.Quantidade = Convert.ToDouble(linha[2]);
                    docVendaLinhas.PrecUnit = Convert.ToDouble(linha[3]);
                    docVendaLinhas.TaxaIva = Convert.ToSingle(linha[4]);
                }
            }
<<<<<<< Updated upstream
=======
            if (BSO.EmTransaccao()) { BSO.TerminaTransaccao(); MessageBox.Show("Faturas submetidas com sucesso!"); }
            return true;
        }

        // OK
        public bool interromperComErro(string error = "")
        {
            if (error == "")
            { error = "Erro inesperado não definido."; }
            listBox.Items.Add(error); PSO.MensagensDialogos.MostraErro(error);
            File.Delete(ficheiro);

            return false;
>>>>>>> Stashed changes
        }

        //OK
        private void btnEscolherFicheiro_Click(object sender, EventArgs e)
        {
            OpenFileDialog form = new OpenFileDialog();

            form.Filter = "Ficheiros .txt (*.txt)|(*.txt)";
            form.FilterIndex = 1;
            form.Multiselect = false;

            if (form.ShowDialog() == DialogResult.OK)
            {
                string ficheiro = form.FileName;
                txtFicheiroPath.Text = ficheiro;
            }
        }

<<<<<<< Updated upstream

=======
        //OK
>>>>>>> Stashed changes
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}