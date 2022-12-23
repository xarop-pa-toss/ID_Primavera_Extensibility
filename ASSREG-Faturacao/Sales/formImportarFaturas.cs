using BasBE100;
using Primavera.Extensibility.CustomForm;
using StdBE100;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

/*
 * ERROS:
 * 
 * 
 * TODO:
 * - Mudar path hardcoded do ficheiro em servidor.
 *  
 *  
 * EDITS:
 * - Restrições de validação são agora com qualquer elemento das listas.
 * - Ordem da validação de linha/cliente. Não impacta funcionamento.
 * - Redução do número de consultas à base de dados Primavera utilizando listas/arrays em memória aumentando a eficiência e rapidez.
 */

namespace ASRLB_ImportacaoFatura
{
    public partial class janelaImportarFatura : CustomForm
    {
        public janelaImportarFatura()
        {
            InitializeComponent();
            datePicker.Value = DateTime.Today;
        }
        string[] linha;
        string ficheiro = null;
        int countFaturas = 0;

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
        internal string validarPath()
        {
            string path;
            if (txtFicheiroPath.Text == "import.txt no servidor")
            {
                path = @"C:\Users\Ricardo Santos\Documents\import.txt";
            }
            else { path = txtFicheiroPath.Text; }

            try
            {
                // Faz cópia do ficheiro para o mesmo directório. Trabalhamos a cópia e não o original.
                string pasta = Path.GetDirectoryName(path);
                string copiaPath = @"" + pasta + @"\importCopia.txt";
                File.Copy(path, copiaPath);

                return copiaPath;
            }
            catch (FileNotFoundException e)
            { PSO.Dialogos.MostraErro("O ficheiro não existe no caminho específicado"); interromperComErro(e.ToString()); return ""; }
            catch (Exception e)
            { PSO.Dialogos.MostraErro("ERRO: " + e); interromperComErro(e.ToString()); return ""; }
        }


        //OK
        internal void criarListasPri(ref List<string> listaArtigos, ref List<string> listaClientes, ref List<string> listaCondPag)
        {
            StdBELista proxyArtigos = BSO.Base.Artigos.LstArtigos();
            listaArtigos.Add(proxyArtigos.Valor(0));
            for (int i = 1; proxyArtigos.NumLinhas() >= i; i++)
            {
                listaArtigos.Add(proxyArtigos.Valor(0));
                proxyArtigos.Seguinte();
            }
            proxyArtigos.Termina();

            StdBELista proxyClientes = BSO.Base.Clientes.LstClientes();
            listaClientes.Add(proxyClientes.Valor(0));
            for (int i = 1; proxyClientes.NumLinhas() >= i; i++)
            {
                listaClientes.Add(proxyClientes.Valor("Cliente"));
                proxyClientes.Seguinte();
            }
            proxyClientes.Termina();

            StdBELista proxyCondPag = BSO.Base.CondsPagamento.LstCondsPagamento();
            listaCondPag.Add(proxyCondPag.Valor(0));
            for (int i = 1; proxyCondPag.NumLinhas() >= i; i++)
            {
                listaCondPag.Add(proxyCondPag.Valor(0));
                proxyCondPag.Seguinte();
            }
            proxyCondPag.Termina();
        }


        //OK
        internal bool validarFicheiro(string[] linhasFicheiro, ref List<string> listaArtigos, ref List<string> listaClientes, ref List<string> listaCondPag, List<string> valoresIva, int linhasFicheiroTotal)
        {
            // Valida os valores (separados por ',') com os presentes nas listas criadas
            for (int i = 0; i < linhasFicheiroTotal; i++)
            {
                linha = linhasFicheiro[i].Split(',');
                for (int u = 0; u < linha.Count(); u++) { linha[u] = linha[u].Replace(",", ""); linha[u] = linha[u].Replace(".", ","); linha[u] = linha[u].Trim(); }

                // Se for Cabeçalho
                if (linha.Count() == 2)
                {
                    // TEST PRINT
                    listBox.Items.Add(String.Format("validarFicheiro -> Linha 0: {0}; Linha 1: {1}", linha[0], linha[1]));
                    // END TEST PRINT
                    if (!listaClientes.Contains(linha[0]) || !listaCondPag.Contains(linha[1]))
                    {
                        return interromperComErro(String.Format("Cliente {0} ou Condição de Pagamento {1} inválido na linha {2}.", linha[0], linha[1], i.ToString()));
                    }
                }
                // Se for Linha
                else if (linha.Count() == 6)
                {
                    // TEST PRINT
                    listBox.Items.Add(String.Format("validarFicheiro -> Linha 0: {0}; Linha 1: {1}; Linha 2: {2}; Linha 3: {3}; Linha 4: {4}; Linha 5: {5};", linha[0], linha[1], linha[2], linha[3], linha[4], linha[5]));
                    // END TEST PRINT
                    countFaturas += 1;
                    if (!listaArtigos.Contains(linha[0]))
                    {
                        return interromperComErro(String.Format("Código de Artigo {0} inválido na linha {1}.", linha[0], (i + 1).ToString()));
                    }
                    else if (!valoresIva.Contains(linha[4]))
                    {
                        return interromperComErro(String.Format("Valor do IVA {0} inválido na linha {1}.", linha[5], (i + 1).ToString()));
                    }
                }
                else
                {
                    return interromperComErro(String.Format("Linha {0} contém valores inválidos ou insuficientes.", (i + 1).ToString()));
                }
            }
            return true;
        }


        //OK
        internal bool processarDados(string[] linhasFicheiro, int linhasFicheiroTotal)
        {
            bool temLinha = false;
            int faturaActual = 0;
            string serie = "";
            string linhaArmazem = "";
            string linhaLocal = "";
            int vdDadosTodos = (int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosTodos;
            int vdDadosCondPag = (int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosCondPag;
            VndBE100.VndBEDocumentoVenda docVenda = new VndBE100.VndBEDocumentoVenda();
            // Import dos valores do IVA da BD
            StdBELista BELista = new StdBELista();
            BELista = BSO.Consulta("SELECT Iva FROM IVA");
            // Inicialização da barra de progresso
            ProgressBar barraProg = new ProgressBar();
            barraProg.Visible = true;
            barraProg.Minimum = 0;
            barraProg.Maximum = countFaturas;
            barraProg.Value = 0;
            barraProg.Step = 1;


            BSO.IniciaTransaccao();

            for (int i = 0; i < linhasFicheiroTotal; i++)
            {
                try
                {
                    barraProg.PerformStep();
                    linha = linhasFicheiro[i].Split(',');
                    for (int u = 0; u < linha.Count(); u++) { linha[u] = linha[u].Replace(",", ""); linha[u] = linha[u].Replace(".", ","); linha[u] = linha[u].Trim(); }

                    // Se for Cabec
                    if (linha.Count() == 2 || i == linhasFicheiroTotal - 1)
                    {
                        if (temLinha)
                        {
                            string strErro = "";
                            string strAvisos = "";

                            // Introduz fatura na Transacção
                            BSO.Vendas.Documentos.CalculaValoresTotais(docVenda);
                            //listBox.Items.Add(String.Format("PARA VALIDAR: Data Doc: {0}; Data Carga: {1}; Data Descarga: {2}", docVenda.DataDoc, docVenda.DataHoraCarga, docVenda.DataHoraDescarga));
                            if (BSO.Vendas.Documentos.ValidaActualizacao(docVenda, BSO.Vendas.TabVendas.Edita(docVenda.Tipodoc), ref serie, ref strErro))
                            {
                                BSO.Vendas.Documentos.Actualiza(docVenda, ref strAvisos);
                                listBox.Items.Add(String.Format("Fatura {0} para cliente {1} processada com sucesso.", docVenda.NumDoc, docVenda.Entidade));
                                if (i == linhasFicheiroTotal - 1) { return false; }
                            }
                            else
                            {
                                listBox.Items.Add(String.Format("Ocorreram erros ao gerar a Fatura {0}. ERRO: {1} \n A INFORMAÇÃO NÃO FOI PROCESSADA!", docVenda.NumDoc, strErro));
                                if (BSO.EmTransaccao()) { BSO.DesfazTransaccao(); }
                                return interromperComErro(strErro);
                            }
                            temLinha = false;
                        }
                        if (i != linhasFicheiroTotal - 1)
                        {
                            docVenda = new VndBE100.VndBEDocumentoVenda();
                            docVenda.Entidade = linha[0];
                            docVenda.TipoEntidade = "C";
                            docVenda.Tipodoc = cBoxDoc.Text;
                            docVenda.Serie = BSO.Base.Series.DaSerieDefeito("V", docVenda.Tipodoc);
                            BSO.Vendas.Documentos.PreencheDadosRelacionados(docVenda, ref vdDadosTodos);

                            DateTime horaAgora = DateTime.Now;
                            docVenda.DataDoc = datePicker.Value.Add(horaAgora.TimeOfDay);
                            docVenda.DataHoraCarga = docVenda.DataDoc.AddMinutes(5).AddSeconds(i);
                            docVenda.HoraDefinida = true;
                            docVenda.CondPag = linha[1];
                            BSO.Vendas.Documentos.PreencheDadosRelacionados(docVenda, ref vdDadosCondPag);

                            faturaActual = 0;
                            temLinha = false;
                        }
                    }
                    // Se for Linha
                    else if (linha.Count() == 6)
                    {
                        //  Dados para preencher linha
                        string linhaArtigo = "";
                        if (cBoxArtigo.Text != "Não alterar") { linhaArtigo = cBoxArtigo.Text; } else { linhaArtigo = linha[0]; }
                        string linhaDescricao = linha[1];
                        double linhaQuantidade = Convert.ToDouble(linha[2]);
                        double linhaPrecUnit = Convert.ToDouble(linha[3]);
                        var CodIva = BELista.Valor("Iva");
                        float linhaTaxaIva = Convert.ToSingle(linha[4]);
                        double linhaDesconto1 = Convert.ToDouble(linha[5]);

                        // Adiciona linha ao Doc
                        BSO.Vendas.Documentos.AdicionaLinha(docVenda, linhaArtigo, ref linhaQuantidade, ref linhaArmazem, ref linhaLocal, linhaPrecUnit, linhaDesconto1);
                        faturaActual++;
                        docVenda.Linhas.GetEdita(faturaActual).Descricao = linhaDescricao;

                        temLinha = true;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("EXCEPTION: " + e);
                    BSO.DesfazTransaccao();
                    return interromperComErro("ERRO: " + e);
                }
            }
            if (BSO.EmTransaccao()) { BSO.TerminaTransaccao(); MessageBox.Show("Faturas submetidas com sucesso!"); }
            return true;
        }



        public bool interromperComErro(string error = null)
        {
            if (error is null)
            { error = "Erro inesperado não definido."; }
            listBox.Items.Add(error); PSO.Dialogos.MostraErro(error);
            File.Delete(ficheiro);

            return false;
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
            Close();
        }

        private void janelaImportarFatura_Load(object sender, EventArgs e)
        {

        }
    }
}