﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BasBE100;
using StdBE100;
using ErpBS100;
using VndBE100;
using StdPlatBS100;
using System.IO;

namespace ASRLB_ImportacaoFatura.Sales
{
    public partial class formImportarTxt_WF : Form
    {
        private ErpBS BSO = new ErpBS();
        private StdPlatBS PSO = new StdPlatBS(); 

        public formImportarTxt_WF()
        {
            InitializeComponent();
            datePicker.Value = DateTime.Today;
        }
        
        string[] linha;
        string ficheiro = null;
        string copia = null;
        int countFaturas = 0;

        //OK
        private void btnIniciar_Click_1(object sender, EventArgs e)
        {
            /* ** INTERRUPÇÃO DE EXECUÇÃO POR FAIL STATES **
            Os métodos validarFicheiro() e processarDados() são chamados através de if's que determinam o seu resultado (valor do return). Se return true, continua; se return false, então há um outro return void ao método pai (btnIniciar_Click()) que interrompe a sua execução.
            O return false acontece quando o método interrompeComErro() é chamado.
            O método validarPath() retorna string e não bool, por isso a sua validação é feita através do resultado do path do ficheiro, igualmente tratado pelo interrompeComErro() */            

            //Lê conteudo da txtBox e valida se ficheiro existe.Copia o ficheiro para o mesmo directório e retorna o path da cópia.
            listBox.Items.Clear();
            ficheiro = validarPath();
            if (ficheiro == "") return;

            // *** ABRIR EMPRESA ***
            // *** ASS REG SERVIDOR ***
            BSO.AbreEmpresaTrabalho(StdBETipos.EnumTipoPlataforma.tpProfissional, GetEmpresa.codEmpresa, "faturacao", "*Pelicano*");
            
            // Cria listas em memória fáceis de iterar para evitar chamar métodos Primavera. Acedidos por referência (morada na memória em vez do valor da variável).
            List<string> listaArtigos = new List<string>();
            List<string> listaClientes = new List<string>();
            List<string> listaCondPag = new List<string>();

            criarListasPri(ref listaArtigos, ref listaClientes, ref listaCondPag);

            // Valida dados no ficheiro comparando com os das listas criadas em criarListasPri();
            List<string> valoresIva = new List<string> { "06,0", "13,0", "23,0" };
            string[] linhasFicheiro = File.ReadAllLines(@"" + ficheiro);
            File.Delete(ficheiro);

            int linhasFicheiroTotal = linhasFicheiro.Count();
            if (!validarFicheiro(linhasFicheiro, ref listaArtigos, ref listaClientes, ref listaCondPag, valoresIva, linhasFicheiroTotal)) { return; }

            // Cria um DocumentoVendas para cada Cliente e preenche com as Linhas correspondentes. Quando recebe novo Cliente, valida os dados e grava a fatura.
            if (!processarDados(linhasFicheiro, linhasFicheiroTotal)) { return; }
        }

        //OK
        private string validarPath()
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
                copia = @"" + pasta + @"\importCopia.txt";
                File.Copy(path, copia);

                return copia;
            }
            catch (FileNotFoundException e)
            { PSO.MensagensDialogos.MostraErro("O ficheiro não existe no caminho específicado"); interromperComErro(e.ToString()); return ""; }
            catch (Exception e)
            { PSO.MensagensDialogos.MostraErro("ERRO: " + e); interromperComErro(e.ToString()); return ""; }
        }

        //OK
        private void criarListasPri(ref List<string> listaArtigos, ref List<string> listaClientes, ref List<string> listaCondPag)
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
        private bool validarFicheiro(string[] linhasFicheiro, ref List<string> listaArtigos, ref List<string> listaClientes, ref List<string> listaCondPag, List<string> valoresIva, int linhasFicheiroTotal)
        {
            // Valida os valores (separados por ',') com os presentes nas listas criadas
            UpdateListbox("*** A VALIDAR LINHAS ***");
            for (int i = 0; i < linhasFicheiroTotal; i++)
            {
                linha = linhasFicheiro[i].Split(',');
                for (int u = 0; u < linha.Count(); u++) { linha[u] = linha[u].Replace(",", ""); linha[u] = linha[u].Replace(".", ","); linha[u] = linha[u].Trim(); }

                // Se for Cabeçalho
                if (linha.Count() == 2)
                {
                    Application.DoEvents();
                    if (!listaClientes.Contains(linha[0]) || !listaCondPag.Contains(linha[1]))
                    {
                        return interromperComErro(String.Format("Cliente {0} ou Condição de Pagamento {1} inválido na linha {2}.", linha[0], linha[1], i.ToString()));
                    }
                }
                // Se for Linha
                else if (linha.Count() == 6)
                {
                    Application.DoEvents();
                    countFaturas += 1;
                    if (!listaArtigos.Contains(linha[0].Replace(".", ""))) {
                        return interromperComErro(String.Format("Código de Artigo {0} inválido na linha {1}.", linha[0], (i + 1).ToString()));
                    }
                    else if (!valoresIva.Contains(linha[4])) {
                        return interromperComErro(String.Format("Valor do IVA {0} inválido na linha {1}.", linha[5], (i + 1).ToString()));
                    }
                }
                else {
                    return interromperComErro(String.Format("Linha {0} contém valores inválidos ou insuficientes.", (i + 1).ToString()));
                }
            }
            UpdateListbox("OK...");
            UpdateListbox(" ");
            return true;
        }

        //OK
        private bool processarDados(string[] linhasFicheiro, int linhasFicheiroTotal)
        {
            bool temLinha = false;
            int faturaActual = 0;
            string serie = "";
            string linhaArmazem = "";
            string linhaLocal = "";
            VndBEDocumentoVenda docVenda = new VndBEDocumentoVenda();
            int vdDadosTodos = (int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosTodos;
            int vdDadosCondPag = (int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosCondPag;

            // Import dos valores do IVA da BD
            StdBELista BELista = BSO.Consulta("SELECT Iva FROM IVA");

            for (int i = 0; i < linhasFicheiroTotal; i++)
            {
                try
                {
                    linha = linhasFicheiro[i].Split(',');
                    for (int u = 0; u < linha.Count(); u++) { linha[u] = linha[u].Replace(",", ""); linha[u] = linha[u].Replace(".", ","); linha[u] = linha[u].Trim(); }

                    // Se for Cabec
                    if (linha.Count() == 2 || i == linhasFicheiroTotal)
                    {
                        UpdateListbox("Cabec");
                        if (temLinha)
                        {
                            string strErro = "";
                            string strAvisos = "";

                            // Introduz fatura na Transacção
                            BSO.Vendas.Documentos.CalculaValoresTotais(docVenda);
                            //listBox.Items.Add(String.Format("PARA VALIDAR: Data Doc: {0}; Data Carga: {1}; Data Descarga: {2}", docVenda.DataDoc, docVenda.DataHoraCarga, docVenda.DataHoraDescarga));
                            if (BSO.Vendas.Documentos.ValidaActualizacao(docVenda, BSO.Vendas.TabVendas.Edita(docVenda.Tipodoc), ref serie, ref strErro))
                            {
                                BSO.Vendas.Documentos.Actualiza(docVenda, ref strAvisos, ref strErro);
                                if (BSO.EmTransaccao()) { UpdateListbox("- INSERIDA + TERMINA -"); BSO.TerminaTransaccao(); }
                                UpdateListbox(String.Format("Fatura {0} para cliente {1} processada com sucesso.", docVenda.NumDoc, docVenda.Entidade));
                                Application.DoEvents();
                            }
                            else
                            {
                                UpdateListbox(String.Format("Ocorreram erros ao gerar a Fatura {0}. ERRO: {1} \n\n A INFORMAÇÃO NÃO FOI PROCESSADA!", docVenda.NumDoc, strErro));
                                Application.DoEvents();
                                if (BSO.EmTransaccao()) { UpdateListbox("- DESFAZ -"); BSO.DesfazTransaccao(); }
                                return interromperComErro(strErro);
                            }
                            temLinha = false;
                        }

                        if (i != linhasFicheiroTotal)
                        {
                            if (BSO.EmTransaccao()) { UpdateListbox("- TERMINA -"); BSO.TerminaTransaccao(); }
                            if (!BSO.EmTransaccao()) { UpdateListbox("- INICIA -"); BSO.IniciaTransaccao(); }

                            docVenda = new VndBEDocumentoVenda();
                            vdDadosTodos = (int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosTodos;
                            vdDadosCondPag = (int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosCondPag;

                            docVenda.Entidade = linha[0];
                            docVenda.TipoEntidade = "C";
                            docVenda.Tipodoc = cBoxDoc.Text;
                            docVenda.Serie = BSO.Base.Series.DaSerieDefeito("V", docVenda.Tipodoc);
                            BSO.Vendas.Documentos.PreencheDadosRelacionados(docVenda, ref vdDadosTodos);

                            //System.Threading.Thread.Sleep(1000);
                            //DateTime horaAgora = DateTime.Now;
                            docVenda.DataDoc = DateTime.Now;
                            //docVenda.DataDoc = datePicker.Value.Add(horaAgora.TimeOfDay);
                            //docVenda.DataHoraCarga = docVenda.DataDoc.AddMinutes(5).AddSeconds(i);
                            docVenda.HoraDefinida = false;
                            docVenda.CondPag = linha[1];
                            BSO.Vendas.Documentos.PreencheDadosRelacionados(docVenda, ref vdDadosCondPag);

                            faturaActual = 0;
                            temLinha = false;
                        }
                    }
                    // Se for Linha
                    else if (linha.Count() == 6)
                    {
                        UpdateListbox("Linha");
                        //  Dados para preencher linha
                        string linhaArtigo = "";
                        if (cBoxArtigo.Text != "Não alterar") { linhaArtigo = cBoxArtigo.Text; } else { linhaArtigo = linha[0]; }

                        string linhaDescricao = "";
                        if (linha[1].Trim() == "TRH") { linhaDescricao = "Taxa de Recursos Hídricos"; } else { linhaDescricao = linha[1]; }
                        
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
            
            if (BSO.EmTransaccao()) { UpdateListbox("- TERMINA -"); BSO.TerminaTransaccao(); }
            
            PSO.MensagensDialogos.MostraAviso("Faturas submetidas com sucesso!", StdPlatBS100.StdBSTipos.IconId.PRI_Informativo);
            UpdateListbox(String.Format("Faturas submetidas com sucesso!"));
            
            return true;
        }


        private void UpdateListbox(string texto)
        {
            if (listBox.InvokeRequired)
            {
                listBox.Invoke(new Action<string>(UpdateListbox), texto);
                return;
            }
            listBox.Items.Add(texto);
            listBox.SelectedIndex = listBox.Items.Count - 1;
        }

        public bool interromperComErro(string error = null)
        {
            if (error is null)
            { error = "Erro inesperado não definido."; }
            UpdateListbox(error); PSO.MensagensDialogos.MostraErro(error);
            Application.DoEvents();
            File.Delete(ficheiro);

            return false;
        }

        private void btnEscolherFicheiro_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog form = new OpenFileDialog();

            form.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            form.FilterIndex = 2;
            form.Multiselect = false;
            form.RestoreDirectory = true;

            if (form.ShowDialog() == DialogResult.OK)
            {
                string ficheiro = form.FileName;
                txtFicheiroPath.Text = ficheiro;
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}