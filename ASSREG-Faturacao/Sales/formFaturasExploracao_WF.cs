﻿using BasBE100;
using ErpBS100;
using StdBE100;
using StdPlatBS100;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using VndBE100;

namespace ASRLB_ImportacaoFatura.Sales
{
    public partial class formFaturasExploracao_WF : Form
    {
        public static Dictionary<string, string> linhaDict { get; set; }
        public Dictionary<int, string> _escaloes = new Dictionary<int, string>();
        public Dictionary<int, string> _escaloesArroz = new Dictionary<int, string>();
        private int _counterLinha = 1;
        private int _countContador = 0;
        private bool _comErro = false;
        private ErpBS BSO = new ErpBS();
        private StdPlatBS PSO = new StdPlatBS();

        //CalcRegantes globals
        private string _dataFull, _cultura;
        private int _ano, _ultimaLeitura, _leitura1, _leitura2, _consumoTotal;
        private double _escalao1, _escalao2;
        private Dictionary<string, StdBELista> DictTaxa = new Dictionary<string, StdBELista>();
        private double _taxa1, _taxa2, _taxa3, _taxa2022, _consumo1, _consumo2, _consumo3, _consumo2022;

        // ***
        // CTRL + M + O para colapsar todas as funções e CTRL + M + L para expandir (Visual Studio)
        // ***

        public formFaturasExploracao_WF()
        {
            InitializeComponent();
        }

        // BOTÕES
        private void btnEscolherFicheiro_WF_Click(object sender, EventArgs e)
        {
            OpenFileDialog form = new OpenFileDialog();

            form.Filter = "Ficheiros Excel (*.xlsx)|*.xlsx|Todos os ficheiros (*.*)|*.*"; ;
            form.FilterIndex = 2;
            form.Multiselect = true;
            form.RestoreDirectory = true;

            if (form.ShowDialog() == DialogResult.OK)
            {
                string[] ficheiros = form.FileNames;
                listBoxFicheiros_WF.Items.AddRange(ficheiros);
            }
        }

        private void btnRemover_WF_Click(object sender, EventArgs e)
        {
            var selection = listBoxFicheiros_WF.SelectedIndices;
            if (selection.Count > 0)
            {
                for (int i = selection.Count; i > 0; i--)
                {
                    listBoxFicheiros_WF.Items.RemoveAt(selection[i - 1]);
                }
            }
        }

        private void btnLimparLista_WF_Click(object sender, EventArgs e)
        {
            listBoxFicheiros_WF.Items.Clear();
        }

        private void btnConfirmar_WF_Click(object sender, EventArgs e)
        {
            //if (cBoxEmpresa.SelectedItem.ToString() == "" || cBoxPenalizacao.SelectedItem.ToString() == "")
            //{
            //    MessageBox.Show("Não foi escolhida empresa ou tipo de penalização a aplicar.");
            //    return;
            //}

            // *** ABRIR EMPRESA ***
            // *** ASS REG SERVIDOR ***
            string empresa = GetEmpresa.codEmpresa;
            BSO.AbreEmpresaTrabalho(StdBETipos.EnumTipoPlataforma.tpProfissional, empresa, "faturacao", "*Pelicano*");

            // Get valores dos controlos do Form
            var ficheiros = listBoxFicheiros_WF.Items;
            bool comPenalizacao = checkBox_Penalizacao.Checked;
            bool comBenaciate = checkBox_Benaciate.Checked;
            string tipoFatura = cBoxTipoFatura.SelectedItem.ToString();

            // Carrega TDUs das Taxas Penalizadoras no arranque
            StdBELista listaTaxa_PD, listaTaxa_PP, listaTaxa_ANA, listaTaxa_CA;
            if (comBenaciate)
            {
                listaTaxa_PD = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'PD_Be';");
                listaTaxa_PP = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'PP_Be';");
            } else
            {
                listaTaxa_PD = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'PD';");
                listaTaxa_PP = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'PP';");
            }
            listaTaxa_ANA = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'ANA';");
            listaTaxa_CA = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'CA';");
            //StdBELista listaTaxa_PD_Benaciate = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'PD_Be';");
            //StdBELista listaTaxa_PP_Benaciate = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'PP_Be';");

            DictTaxa.Clear();
            DictTaxa.Add("PD", listaTaxa_PD);
            DictTaxa.Add("PP", listaTaxa_PP);
            DictTaxa.Add("ANA", listaTaxa_ANA);
            DictTaxa.Add("CA", listaTaxa_CA);
            //DictTaxa.Add("PD_Be", listaTaxa_PD_Benaciate);
            //DictTaxa.Add("PP_Be", listaTaxa_PP_Benaciate);

            foreach (string path in ficheiros)
            {
                Reset_linhaDict();
                ResetVariaveis();

                listBoxErros_WF.Items.Add("A abrir ficheiro Excel: " + path);
                ExcelControl_WF Excel = new ExcelControl_WF(@"" + path);
                listBoxErros_WF.Items.Add("A carregar folhas...");

                // Array de strings passado por referência para preencher com erros do ficheiro Excel.
                List<string> errosExcelList = new List<string>();
                DataSet DtSet = Excel.CarregarDataSet(@"" + path, Excel.conString, ref errosExcelList);

                // Se houver linhas com dados inválidos no Excel, errosExcel é preenchido com cada um.
                if (errosExcelList.Count > 0) {
                    ErrosExcel(errosExcelList, path);
                    Excel.EliminarCopia(@"" + path);
                    break;
                }

                List<string> folhasList = Excel.folhasList;
                int nomeFolhaInd = 0;

                Excel.EliminarCopia(@"" + path);

                DataTable DtTable = DtSet.Tables["Tabela"];
                VndBEDocumentoVenda DocVenda = new VndBEDocumentoVenda();

                //listBoxErros_WF.Items.Add("FOLHA: " + folhasList[nomeFolhaInd]);
                nomeFolhaInd++;

                if (!BSO.EmTransaccao()) { BSO.IniciaTransaccao(); }

                for (int i = 0; i < DtTable.Rows.Count; i++)
                {
                    _comErro = false;
                    DataRow DtRow = DtTable.Rows[i];

                    // benefIgual é True se Benef na nova linha for igual ao da linha anterior (ainda no linhaDict)
                    // Se Benef for diferente, então é necessário emitir a fatura antes de começar uma nova.
                    bool benefIgual = DtRow.Field<double>("Benef").ToString().PadLeft(5, '0').Equals(linhaDict["Benef"]);
                    // Se contador for o mesmo que o anterior, então é um caso de 1 contador -> multiplos prédios. Neste caso a linha tem de ser completamente ignorada pois os valores são iguais ao da primeira.
                    // Contador é inicializado como "" por isso não vai bater igual na primeira linha.
                    bool contadorIgual = DtRow.Field<string>("Nº Contador").Equals(linhaDict["Contador"]);


                    // Contador é igual ao da linha anterior em casos de desunião de linhas que têm 'Um contador -> Vários prédios/áreas'. Área de cada linha diferente a contabilizar.
                    if (!benefIgual)
                    {
                        //Exclui primeira linha por ainda não existir nada
                        if (i > 0)
                        {
                            BSO.Vendas.Documentos.CalculaValoresTotais(DocVenda);
                            string erroFatura = EmitirFatura(DocVenda);
                            if (erroFatura != "") { ErroAoEmitir(erroFatura); break; }
                            _countContador = 0;
                        }

                        DocVenda = new VndBEDocumentoVenda();
                        _counterLinha = 1;

                        PrepararDict(DtRow); // Preenche dicionário com dados necessários.
                        i += MesmoContadorBenef(DtTable, i);

                        ProcessarCabecDoc(DocVenda, tipoFatura);
                        CalcRegantes(comPenalizacao); // Efectua cálculos de valores e taxas associadas.
                        if (_consumoTotal == 0 && _consumo2022 == 0) { continue; }
                        ProcessarLinha(DocVenda, comPenalizacao); // Preenche linhasDoc com descrições e leituras com seus valores calculados.
                    }
                    else if (benefIgual && !contadorIgual && i > 0) // Um benef -> Vários contadores. Vão todos os contadores para a mesma fatura
                    {
                        _countContador++;
                        PrepararDict(DtRow);
                        i += MesmoContadorBenef(DtTable, i);

                        CalcRegantes(comPenalizacao);
                        ProcessarLinha(DocVenda, comPenalizacao);
                    }

                    // BSO.Vendas.Documentos.CalculaValoresTotais(DocVenda);

                    // Catch para a última linha do DataSet
                    if (i == DtTable.Rows.Count - 1)
                    {
                        BSO.Vendas.Documentos.CalculaValoresTotais(DocVenda);
                        string erroFat = EmitirFatura(DocVenda);
                        if (erroFat != "") { ErroAoEmitir(erroFat); break; }
                    }
                    DocVenda.Dispose();
                }
                if (_comErro) { break; }
                listBoxErros_WF.Items.Add(" ");
                listBoxErros_WF.Items.Add("*** A escrever as faturas na base de dados Primavera. Por favor aguarde... ***");
                if (BSO.EmTransaccao()) { BSO.TerminaTransaccao(); }

                listBoxErros_WF.Items.Add(" ");
                listBoxErros_WF.Items.Add("Folha processada com sucesso. Faturas foram emitidas.");
                listBoxErros_WF.Items.Add(" ");
                listBoxErros_WF.SelectedIndex = listBoxErros_WF.Items.Count - 1;
            }
        }

        // MAIN FUNC
        private void PrepararDict(DataRow DtRow)
        {
            try
            {
                // Dados Excel  
                // TRH e Taxa Penalizadora vêm como 'S' ou 'N'. Se 'S', substituir pelo valor. Se 'N', cancelar.
                linhaDict["Contador"] = DtRow.Field<string>("Nº Contador");
                linhaDict["Predio"] = DtRow.Field<string>("Prédio");
                linhaDict["Area"] = DtRow.Field<double>("Área").ToString();
                linhaDict["Cultura"] = DtRow.Field<string>("Cultura");
                linhaDict["TRH"] = DtRow.Field<string>("TRH").ToString();
                linhaDict["TRHValor"] = null;
                linhaDict["TaxaPenalizadora"] = DtRow.Field<string>("Tx Penalizadora");

                // Transform do Benef para ser igual às Entidades no Primavera.
                linhaDict["Benef"] = DtRow.Field<double?>("Benef").ToString().PadLeft(5, '0');
                linhaDict["Nome"] = DtRow.Field<string>("Nome");
                linhaDict["UltimaLeitura"] = DtRow.Field<double>("Última Leitura").ToString();
                linhaDict["Data1"] = DtRow.Field<DateTime>("Data 1").ToString("dd/MM/yyyy");
                linhaDict["Leitura1"] = DtRow.Field<double>("Leitura 1").ToString();
                linhaDict["Data2"] = DtRow.Field<DateTime>("Data 2").ToString("dd/MM/yyyy");
                linhaDict["Leitura2"] = DtRow.Field<double>("Leitura 2").ToString();

                // Reset aos valores calculados por CalcRegantes;
                linhaDict["Taxa1"] = null;
                linhaDict["Consumo1"] = null;
                linhaDict["Taxa2"] = null;
                linhaDict["Consumo2"] = null;
                linhaDict["Taxa3"] = null;
                linhaDict["Consumo3"] = null;
            }
            catch (Exception e)
            {
                if (BSO.EmTransaccao()) { BSO.DesfazTransaccao(); }
                string erro = "Contador " + linhaDict["Contador"] + ".\nLinha possivelmente contém valores nulos. Por favor corrija o ficheiro Excel. \n\n" + e;
                listBoxErros_WF.Items.Add(erro);
                PSO.MensagensDialogos.MostraErro(erro);
            }
        }

        private void ProcessarCabecDoc(VndBEDocumentoVenda DocVenda, string tipoFatura)
        {
            try
            {
                int vdDadosTodos = (int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosTodos;
                int vdDadosCondPag = (int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosCondPag;

                // Definir TipoDoc
                DocVenda.Tipodoc = tipoFatura;

                DocVenda.Serie = BSO.Base.Series.DaSerieDefeito("V", DocVenda.Tipodoc);
                DocVenda.Entidade = linhaDict["Benef"];
                DocVenda.TipoEntidade = "C";
                BSO.Vendas.Documentos.PreencheDadosRelacionados(DocVenda, ref vdDadosTodos);

                DocVenda.DataDoc = DateTime.Now;
                DocVenda.HoraDefinida = false;
                DocVenda.CondPag = "4";
                BSO.Vendas.Documentos.PreencheDadosRelacionados(DocVenda, ref vdDadosCondPag);
            }
            catch(Exception e)
            {
                if (BSO.EmTransaccao()) { BSO.DesfazTransaccao();}
                ErroAoEmitir(e.ToString());
            }

        }

        private void formFaturasExploracao_WF_Load(object sender, EventArgs e)
        {

        }

        private void formFaturasExploracao_WF_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ProcessarLinha(VndBEDocumentoVenda DocVenda, bool comPenalizacao)
        {
            // Linha 1 - Descrição com NºContador + Consumo Total
            // Linha 2 - Última leitura do ano passado + úlitma leitura feita este ano.
            double consumoTotal = Convert.ToDouble(linhaDict["LeituraFinal"]) - Convert.ToDouble(linhaDict["UltimaLeitura"]);
            string descricao = String.Format("Contador {0}. Consumo total: {1} m³. Área: {2}", linhaDict["Contador"], consumoTotal.ToString(), linhaDict["Area"]); ;
            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: descricao); _counterLinha++;

            descricao = String.Format("Leitura inicial: {0} m³. Leitura final: {1} m³ ({2}).", linhaDict["UltimaLeitura"], linhaDict["LeituraFinal"], linhaDict["DataLeituraFinal"]);
            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: descricao); _counterLinha++;

            // Linhas 3 até 8 - Leituras + TRH
            // _counterLinha começa na linha 3. Variável global para dar track às linhas na fatura independentemente do nº de contadores
            // CUIDADO - implementação de while(true) depende de uso eficaz e deliberado de breaks e continues para evitar loop infinito.
            while (true)
            {
                // Contagens normais
                if (linhaDict["Consumo1"] != "0") { CriarLinhaConsumo(DocVenda, 1, comPenalizacao); linhaDict["Consumo1"] = "0"; _counterLinha++; continue; }
                if (linhaDict["Consumo2"] != "0") { CriarLinhaConsumo(DocVenda, 2, comPenalizacao); linhaDict["Consumo2"] = "0"; _counterLinha++; continue; }
                if (linhaDict["Consumo3"] != "0") { CriarLinhaConsumo(DocVenda, 3, comPenalizacao); linhaDict["Consumo3"] = "0"; _counterLinha++; }

                // Calcula TRH que depende da cultura e popula linhaDict["TRHValor"]. Esse valor é então usado como preço unitário na linha na fatura
                CalcRegantes_TaxaRecursosHidricos(linhaDict["Cultura"]);
                double precUnitTRH = Convert.ToDouble(linhaDict["TRHValor"]);
                double quantidadeTRH = 1;
                string armazem = ""; string localizacao = "";

                if (linhaDict["TRH"] == "S")
                {
                    BSO.Vendas.Documentos.AdicionaLinha(DocVenda, "TRH", ref quantidadeTRH, ref armazem, ref localizacao, precUnitTRH);
                    VndBELinhaDocumentoVenda linhaTRH = DocVenda.Linhas.GetEdita(_counterLinha);
                    linhaTRH.Descricao = "Taxa de Recursos Hídricos";
                    _counterLinha++;
                }

                // Linha em branco para separar do próximo contador
                BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: " "); _counterLinha++;
                break;
            }
        }

        private void CriarLinhaConsumo(VndBEDocumentoVenda DocVenda, int escalao, bool comPenalizacao)
        {
            double quantidade = Convert.ToDouble(linhaDict["Consumo" + escalao]);
            double precUnit = Convert.ToDouble(linhaDict["Taxa" + escalao]);
            string armazem = ""; string localizacao = "";

            BSO.Vendas.Documentos.AdicionaLinha(DocVenda, "TE", ref quantidade, ref armazem, ref localizacao, precUnit);

            VndBELinhaDocumentoVenda linha = DocVenda.Linhas.GetEdita(_counterLinha);
            if (comPenalizacao)
            {
                linha.Quantidade = _consumoTotal;
            }
            
            linha.Descricao = _escaloes[escalao];
            linha.Quantidade = Convert.ToDouble(linhaDict["Consumo" + escalao]);
            linha.PrecUnit = Convert.ToDouble(linhaDict["Taxa" + escalao]);
        }

        private string EmitirFatura(VndBEDocumentoVenda DocVenda)
        {
            string strAvisos = "", strErro = "", serie = "";

            BSO.Vendas.Documentos.CalculaValoresTotais(DocVenda);

            // Faturas com valor de 1,99€ ou menor não são emitidas
            try
            {
                if (DocVenda.TotalDocumento >= 2 && BSO.Vendas.Documentos.ValidaActualizacao(DocVenda, BSO.Vendas.TabVendas.Edita(DocVenda.Tipodoc), ref serie, ref strErro))
                {
                    BSO.Vendas.Documentos.Actualiza(DocVenda, ref strAvisos, ref strErro);
                    if (strErro != "") { _comErro = true; return strErro; }
                    listBoxErros_WF.Items.Add(String.Format("Benef {0} - {1} contador(es) no documento {2} {3}/{4}.", DocVenda.Entidade, _countContador + 1, DocVenda.Tipodoc, DocVenda.Serie, DocVenda.NumDoc));
                    listBoxErros_WF.SelectedIndex = listBoxErros_WF.Items.Count - 1;
                    return "";
                }
                else { _comErro = true; return strErro; }
            }
            catch (Exception e) { _comErro = true; return e.ToString(); }
        }

        // CALCULOS 
        private void CalcRegantes(bool comPenalizacao)
        {
            _cultura = linhaDict["Cultura"];
            if (linhaDict["Data1"] != null && linhaDict["Leitura1"] != null)
            {
                _dataFull = linhaDict["Data1"];
                _leitura1 = Convert.ToInt32(linhaDict["Leitura1"]);
            }
            if (linhaDict["Data2"] != null && linhaDict["Leitura2"] != null)
            {
                _dataFull = linhaDict["Data2"];
                _leitura2 = Convert.ToInt32(linhaDict["Leitura2"]);
            }

            _ultimaLeitura = Convert.ToInt32(linhaDict["UltimaLeitura"]);

            //int
            _ano = Convert.ToDateTime(_dataFull).Year;

            // Se o ano for 2022, o consumo entre a 1ª leitura e a última do ano passado (Leitura1 - LeituraFinal) é taxado com o valor mínimo (Cultura PD até 5000) -> _consumo2022
            // O resto do cosnumo (Leitura2 - Leitura1) "começa do zero" e é usado para os restantes cálculos normalmente. -> _consumo1, _consumo2, _consumo3
            // _consumo2022Especial é definido dentro de CalcRegantes_ConsumoTotal pois é lá que tem mais influência
            _consumo2022 = 0;
            _consumoTotal = CalcRegantes_ConsumoTotal();
            linhaDict["Consumo"] = _consumoTotal.ToString();

            //Define _consumo1, _consumo2, _consumo3
            CalcRegantes_Consumos(comPenalizacao);
            //Define _taxa1, _taxa2, _taxa3 a serem aplicadas a cada consumo. _taxa1 é a mais baixa da tabela se _ano = 2022;
            CalcRegantes_TaxasPenalizadoras(comPenalizacao);

            linhaDict["Taxa1"] = _taxa1.ToString();
            linhaDict["Taxa2"] = _taxa2.ToString();
            linhaDict["Taxa3"] = _taxa3.ToString();
            linhaDict["Consumo1"] = _consumo1.ToString();
            linhaDict["Consumo2"] = _consumo2.ToString();
            linhaDict["Consumo3"] = _consumo3.ToString();
        }

        private int CalcRegantes_ConsumoTotal()
        {
            if (linhaDict["Leitura1"] == null && linhaDict["Leitura2"] == null)
            {
                linhaDict["DataLeituraFinal"] = "0";
                linhaDict["LeituraFinal"] = "0";
                linhaDict["TotalLeituras"] = "0";
                return 0;
            }

            if (linhaDict["Leitura2"] == null)
            {
                linhaDict["DataLeituraFinal"] = linhaDict["Data1"];
                linhaDict["LeituraFinal"] = linhaDict["Leitura1"];
                linhaDict["TotalLeituras"] = "1";
                return _leitura1 - _ultimaLeitura;
            }
            else
            {
                linhaDict["DataLeituraFinal"] = linhaDict["Data2"];
                linhaDict["LeituraFinal"] = linhaDict["Leitura2"];
                linhaDict["TotalLeituras"] = "2";
                return _leitura2 - _ultimaLeitura;
            }
        }

        private void CalcRegantes_Consumos(bool comPenalizacao)
        {
            // Separação do consumo total pelos três escalões. Preenche o 1º escalão até ao seu limite antes de ir pro 2º. Será ignorado se for zero.
            // Se houver mais que 7000 de consumo, 5000 ficam no primeiro escalão, 2000 (diferença entre 5000 e 7000) ficam no segundo e o restante no terceiro.
            // *** VALORES BASE PARA UM HECTARE -> 5000 e 7000. VALOR BASE DEVE SER MULTIPLICADO PELA ÁREA ***
            double area = Convert.ToDouble(linhaDict["Area"]);

            if (_cultura != "CA")
            { _escalao1 = 5000 * area; _escalao2 = 2000 * area; }
            else
            { _escalao1 = 12000 * area; _escalao2 = 2000 * area; }

            //if (_cultura != "CA")
            //{ _escalao1 = 5000; _escalao2 = 2000; }
            //else
            //{ _escalao1 = 12000; _escalao2 = 2000; }

            double escalao1e2 = _escalao1 + _escalao2;
            _consumo1 = 0; _consumo2 = 0; _consumo3 = 0;


            // SEM PENALIZAÇÃO
            // Só usamos o consumo total. Definimos _consumo1 = _consumoTotal para utilizar a mesma lógica que o outro tipo de faturação sem reescrever nada.
            if (!comPenalizacao)
            {
                _consumo1 = _consumoTotal;
                return;
            }

            // EXPLICAÇÃO CÁLCULOS
            // Os valores base dos escalões são m3 por 1 hectare. Por essa razão, são calculados os consumos com base em 1 hectare, e então multiplicados pela área total de cada contador.
            // Cada hectare "dá direito" ao valor base de um escalão. Ou seja, se um benef tiver 2 hectares, tem direito a 10000 m3 taxados no primeiro escalão em vez de 5000.
            // e.g. Um consumo total de 11000 m3 em 1 hectare tería os escalões calculados a 5000 -> 2000 -> 4000. Para 3 hectares sería a 15000 -> 6000 -> 12000 (efectivamente o triplo).

            if (comPenalizacao)
            {
                if (_consumoTotal >= escalao1e2)
                {
                    _consumo1 = _escalao1; _consumo2 = _escalao2; _consumo3 = _consumoTotal - escalao1e2;
                }
                else if (_consumoTotal > _escalao1 && _consumoTotal <= escalao1e2)
                {
                    _consumo1 = _escalao1; _consumo2 = _consumoTotal - _escalao1; _consumo3 = 0;
                }
                else { _consumo1 = _consumoTotal; _escalao2 = 0; _consumo3 = 0; }

                linhaDict["Taxa1"] = null;
                linhaDict["Consumo1"] = null;
            }
        }

        private void CalcRegantes_TaxasPenalizadoras(bool comPenalizacao)
        {
            _taxa1 = 0; _taxa2 = 0; _taxa3 = 0;

            if (!comPenalizacao)
            {
                _taxa1 = DictTaxa[_cultura].Valor("CDU_EscalaoUm");
                return;
            }
            else if (comPenalizacao)
            {
                _taxa1 = DictTaxa[_cultura].Valor("CDU_EscalaoUm");
                _taxa2 = DictTaxa[_cultura].Valor("CDU_EscalaoDois");
                _taxa3 = DictTaxa[_cultura].Valor("CDU_EscalaoTres");
                return;
                //_taxa2022 = DictTaxa[_cultura].Valor("CDU_EscalaoUm");
            }
        }

        private void CalcRegantes_TaxaRecursosHidricos(string cultura)
        {
            double reducao25 = 0.25;
            double reducao90 = 0.9;
            double agravamento = 1.2;
            double TRH_U, TRH_A, baseTRH_U, baseTRH_A;

            // ** TDU_TRH NÃO ESTÁ CRIADA **
            // TRH de base está na TDU_TRH para que cliente a possa alterar
            //double baseTRH_U = 0, baseTRH_A = 0;

            //using (StdBELista trh = BSO.Consulta("SELECT MAX(CDU_TRH_U) FROM TDU_TRH")) {
            //    baseTRH_U = Convert.ToDouble(trh.Valor(0));
            //}

            //using (StdBELista trh = BSO.Consulta("SELECT MAX(CDU_TRH_A) FROM TDU_TRH")) {
            //    baseTRH_A = Convert.ToDouble(trh.Valor(0));
            //}

            baseTRH_U = 0.000774;
            baseTRH_A = 0.0038;

            double consumoTotal = Convert.ToDouble(_consumoTotal);
            if (_consumo2022 != 0) { consumoTotal += _consumo2022; }

            // Calculos da TRH.
            // São calculados valores do ComponenteU e do Componente A em separado, com _consumoTotal como base. A TRH é a adição de ambos os valores finais.

            // *** Componente U ***
            // _consumoTotal multiplicado pelo valor base TRH do Comp U -> resultado reduzido por 25%
            TRH_U = consumoTotal * baseTRH_U;
            TRH_U = TRH_U - (TRH_U * reducao25);
            if (cultura == "CA") { TRH_U = TRH_U - (TRH_U * reducao90); }

            // *** Componente A ***
            // _consumoTotal multiplicado pelo valor base TRH do Comp A - > resultado reduzido por 25% -> resultado agravado em 120%
            TRH_A = consumoTotal * baseTRH_A;
            TRH_A = TRH_A - (TRH_A * reducao25);
            TRH_A = TRH_A * agravamento;
            if (cultura == "CA") { TRH_A = TRH_A - (TRH_A * reducao90); }

            // *** TRH FINAL ***
            // Adição de TRH_A e TRH_U
            linhaDict["TRHValor"] = (TRH_U + TRH_A).ToString();
        }

        // SUPORTE
        private int MesmoContadorBenef(DataTable DtTable, int i)
        {
            if (i == DtTable.Rows.Count - 1) { return 0; }

            double area = Convert.ToDouble(linhaDict["Area"]);
            int counter = 0;

            do
            {
                //MessageBox.Show("area inicial: "+ linhaDict["Area"]+"i = "+i);
                bool subContadorIgual = DtTable.Rows[i + 1].Field<string>("Nº Contador").Equals(linhaDict["Contador"]);
                bool subBenefIgual = DtTable.Rows[i + 1].Field<double>("Benef").ToString().PadLeft(5, '0').Equals(linhaDict["Benef"]);

                if (subContadorIgual && subBenefIgual)
                {
                    area += DtTable.Rows[i + 1].Field<double>("Área");
                    i++;
                    counter++;
                }
                else { break; }
            } while (i < DtTable.Rows.Count - 1);

            linhaDict["Area"] = area.ToString();
            //MessageBox.Show("area final: " + area+ "i = " + i +". counter = "+ counter);
            return counter;
        }

        private void ResetVariaveis()
        {
            _counterLinha = 1;
            _taxa1 = 0; _taxa2 = 0; _taxa3 = 0; _taxa2022 = 0;
            _consumo1 = 0; _consumo2 = 0; _consumo3 = 0; _consumo2022 = 0;
        }

        private void Reset_linhaDict()
        {
            linhaDict = new Dictionary<string, string>();

            // Dados importados do Excel
            linhaDict.Add("Predio", null);
            linhaDict.Add("Area", null);
            linhaDict.Add("Cultura", null);
            linhaDict.Add("TRH", null);
            linhaDict.Add("TaxaPenalizadora", null);
            linhaDict.Add("Contador", null);
            linhaDict.Add("Benef", "");
            linhaDict.Add("Nome", null);
            linhaDict.Add("UltimaLeitura", null);
            linhaDict.Add("Data1", null);
            linhaDict.Add("Leitura1", null);
            linhaDict.Add("Data2", null);
            linhaDict.Add("Leitura2", null);

            // Populados em CalcRegantes.ConsumoTotal() chamado pelo constructor da classe.
            linhaDict.Add("DataLeituraFinal", null);
            linhaDict.Add("LeituraFinal", null);
            linhaDict.Add("TotalLeituras", null);

            // Dados extra, não no Excel
            // Consumo é o total calculado em CalcRegantes
            linhaDict.Add("Descricao", null);
            linhaDict.Add("ConsumoTotal", null);

            // Valores de consumo separados por escalão. Taxa/Escalão depende do Prédio.
            // Populados em CalcRegantes.AplicarTaxas() chamado por btnConfirmar.
            linhaDict.Add("Taxa1", null);
            linhaDict.Add("Consumo1", null);
            linhaDict.Add("Taxa2", null);
            linhaDict.Add("Consumo2", null);
            linhaDict.Add("Taxa3", null);
            linhaDict.Add("Consumo3", null);
            linhaDict.Add("Consumo2022", null);

            // Descrições para cada escalão.
            _escaloes[1] = "Até 5000 m³/ha"; _escaloes[2] = "Entre 5000 e 7000 m³/ha"; _escaloes[3] = "Mais que 7000 m³/ha"; _escaloes[2022] = "";
            _escaloesArroz[1] = "Até 12000 m³/ha"; _escaloesArroz[2] = "Entre 12000 e 14000 m³/ha"; _escaloesArroz[3] = "Mais que 14000 m³/ha";

            //DtGridExploracao.AutoGenerateColumns = true;
            //DtGridExploracao.AutoSize = true;
            //DtGridExploracao.ScrollBars = ScrollBars.Both;
            //DtGridExploracao.DataSource = DtSet.Tables[0];
            //DtGridExploracao.Refresh();
            //PSO.MensagensDialogos.MostraErro("Load Finished");
        }

        private void ErroAoEmitir(string erroFatura)
        {
            if (BSO.EmTransaccao()) { BSO.DesfazTransaccao(); }
            listBoxErros_WF.Items.Add("");
            listBoxErros_WF.Items.Add("*** ERRO ***");
            listBoxErros_WF.Items.Add(String.Format("Contador {0} para Benef {1} - {2} não foi processado correctamente.", linhaDict["Contador"], linhaDict["Benef"], linhaDict["Nome"]));
            listBoxErros_WF.Items.Add("");
            listBoxErros_WF.Items.Add("Por favor verificar os dados na folha de Excel relativos a este contador.");
            listBoxErros_WF.Items.Add("ATENÇÃO: Nenhuma fatura foi emitida. Só serão emitidas faturas quando todas as linhas do ficheiro Excel forem válidas.");
            listBoxErros_WF.SelectedIndex = listBoxErros_WF.Items.Count - 1;

            string erro = "";
            //foreach (var kv in linhaDict)
            //{
            //    erro = erro + "\n" + kv.Key + ": " + kv.Value;
            //}
            erro = erro + "*** Erro de sistema no Contador " + linhaDict["Contador"] + "***\n" + erroFatura;

            PSO.MensagensDialogos.MostraErro("Dados inválidos na fatura ou cliente. Ver detalhes abaixo", StdBSTipos.IconId.PRI_Critico, erro);
            _comErro = true;
        }

        private void ErrosExcel(List<string> errosExcelList, string path)
        {
            string folder = Path.GetDirectoryName(path);
            string errosPath = folder + "\\errosExcel";
            string errosPathBuf = errosPath;

            // Escreve erros para um ficheiro TXT na mesma pasta do Excel
            int fileCounter = 0;
            while (File.Exists(errosPath))
            {
                fileCounter++;
                errosPath = errosPathBuf + fileCounter + ".txt";
            }

            File.WriteAllLines(errosPath, errosExcelList);

            listBoxErros_WF.Items.Add(" ");
            listBoxErros_WF.Items.Add("*** ERRO ***");
            foreach (string erro in errosExcelList)
            {
                listBoxErros_WF.Items.Add(erro);
            }

            PSO.MensagensDialogos.MostraErro("Erros nas linhas do Excel. Ver contadores com erro e corrigir ficheiro. #" + errosExcelList.Count);
        }
    }
}