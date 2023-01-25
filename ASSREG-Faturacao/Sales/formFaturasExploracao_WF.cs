using BasBE100;
using StdBE100;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using VndBE100;
using ErpBS100; using StdPlatBS100;
namespace ASRLB_ImportacaoFatura.Sales
{
    public partial class formFaturasExploracao_WF : Form
    {
        public static Dictionary<string, string> linhaDict { get; set; }
        public Dictionary<int, string> _escaloes = new Dictionary<int, string>();
        public Dictionary<int, string> _escaloesArroz = new Dictionary<int, string>();
        private int _counterLinha = 1;
        private bool _comErro = false;
        private ErpBS BSO = new ErpBS();
        private StdPlatBS PSO = new StdPlatBS();

        //CalcRegantes globals
        private string _dataFull, _cultura, _tipoFatura;
        private int _ano, _consumoTotal, _ultimaLeitura, _leitura1, _leitura2;
        private int _escalao1, _escalao2;
        private Dictionary<string, StdBELista> DictTaxa = new Dictionary<string, StdBELista>();
        private double _taxa1, _taxa2, _taxa3, _consumo1, _consumo2, _consumo3;
        private double area;

        // **** BUGS ****
        // new V

        public formFaturasExploracao_WF()
        {
            InitializeComponent();
        }

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

        private void formFaturasExploracao_WF_Load(object sender, EventArgs e)
        {
            // Carrega TDUs das Taxas Penalizadoras no arranque
            //public StdPlatBS PSO = new StdPlatBS();
            BSO.AbreEmpresaTrabalho(StdBETipos.EnumTipoPlataforma.tpProfissional, "IDCLONE", "id", "pelicano");

            StdBELista listaTaxa_PD = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'PD';");
            StdBELista listaTaxa_PP = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'PP';");
            StdBELista listaTaxa_ANA = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'ANA';");
            StdBELista listaTaxa_CA = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadoraArroz WHERE CDU_Cultura = 'CA';");
            StdBELista listaTaxa_PD_Benaciate = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'PD_Be';");
            StdBELista listaTaxa_PP_Benaciate = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'PP_Be';");

            DictTaxa.Clear();
            DictTaxa.Add("PD", listaTaxa_PD);
            DictTaxa.Add("PP", listaTaxa_PP);
            DictTaxa.Add("ANA", listaTaxa_ANA);
            DictTaxa.Add("CA", listaTaxa_CA);
            DictTaxa.Add("PD_Be", listaTaxa_PD_Benaciate);
            DictTaxa.Add("PP_Be", listaTaxa_PP_Benaciate);
        }

        private void btnConfirmar_WF_Click(object sender, EventArgs e)
        {
            var ficheiros = listBoxFicheiros_WF.Items;
            string tipoFatura = cBoxTipoFatura.SelectedItem.ToString();

            foreach (string path in ficheiros)
            {
                Reset_linhaDict();

                listBoxErros_WF.Items.Add("A ler ficheiro Excel: " + path);
                ExcelControl_WF Excel = new ExcelControl_WF(@"" + path);

                listBoxErros_WF.Items.Add("A carregar ficheiro Excel");
                listBoxErros_WF.Refresh();
                DataSet DtSet = Excel.CarregarDataSet(@"" + path, Excel.conString);
                List<string> folhasList = Excel.folhasList;
                int nomeFolhaInd = 0;

                Excel.EliminarCopia(@"" + path);

                foreach (DataTable DtTable in DtSet.Tables)
                {
                    VndBEDocumentoVenda DocVenda = new VndBEDocumentoVenda();
                    listBoxErros_WF.Items.Add("FOLHA: " + folhasList[nomeFolhaInd]);
                    nomeFolhaInd++;

                    for (int i = 0; i < DtTable.Rows.Count; i++)
                    {
                        if (!BSO.EmTransaccao()) { BSO.IniciaTransaccao(); }

                        _comErro = false;

                        DataRow DtRow = DtTable.Rows[i];

                        // benefIgual é True se Benef na nova linha for igual ao da linha anterior (ainda no linhaDict)
                        // Se Benef for diferente, então é necessário emitir a fatura antes de começar uma nova.
                        bool benefIgual = DtRow.Field<double>("Benef").ToString().PadLeft(5, '0').Equals(linhaDict["Benef"]);
                        // Se contador for o mesmo que o anterior, então é um caso de 1 contador -> multiplos prédios. Neste caso a linha tem de ser completamente ignorada pois os valores são iguais ao da primeira.
                        // Contador é inicializado como "" por isso não vai bater igual na primeira linha.
                        bool contadorIgual = DtRow.Field<string>("Nº Contador").Equals(linhaDict["Contador"]);

                        if (contadorIgual && benefIgual && i > 0) { continue; } // Contador é igual ao da linha anterior em casos de desunião de linhas que têm 'Um contador -> Vários prédios/áreas'. Ignora-se estas linhas pois têm valores duplicados.

                        if (!benefIgual)
                        {
                            //Exclui primeira linha por ainda não existir nada
                            if (i > 0)
                            {
                                string emFat = EmitirFatura(DocVenda);
                                if (emFat != "") { ErroAoEmitir(emFat); break; }
                            }

                            DocVenda = new VndBEDocumentoVenda();
                            _counterLinha = 1;

                            PrepararDict(DtRow); // Preenche dicionário com dados necessários.
                            ProcessarCabecDoc(DocVenda);
                            CalcRegantes(tipoFatura); // Efectua cálculos de valores e taxas associadas.
                            ProcessarLinha(DocVenda, tipoFatura); // Preenche linhasDoc com descrições e leituras com seus valores calculados.
                        }
                        else if (benefIgual && !contadorIgual && i > 0) // Um benef -> Vários contadores. Vão todos os contadores para a mesma fatura
                        {
                            PrepararDict(DtRow);
                            CalcRegantes();
                            ProcessarLinha(DocVenda);
                        }

                        BSO.Vendas.Documentos.CalculaValoresTotais(DocVenda);

                        // Catch para a última linha do DataSet
                        if (i == DtTable.Rows.Count - 1)
                        {
                            string emFat = EmitirFatura(DocVenda);
                            if (emFat != "") { ErroAoEmitir(emFat); break; }
                        }

                        DocVenda.Dispose();
                    }
                    if (_comErro) { break; }
                    else { if (BSO.EmTransaccao()) { BSO.TerminaTransaccao(); } }
                }
                if (_comErro) { break; }
            }
        }


        private void PrepararDict(DataRow DtRow)
        {
            // Dados Excel
            // TRH e Taxa Penalizadora vêm como 'S' ou 'N'. Se 'S', substituir pelo valor. Se 'N', cancelar.
            linhaDict["Predio"] = DtRow.Field<string>("Prédio");
            linhaDict["Area"] = DtRow.Field<double>("Área").ToString();
            linhaDict["Cultura"] = DtRow.Field<string>("Cultura");
            linhaDict["TRH"] = DtRow.Field<string>("TRH").ToString();
            linhaDict["TaxaPenalizadora"] = DtRow.Field<string>("Tx Penalizadora");
            linhaDict["Contador"] = DtRow.Field<string>("Nº Contador");

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

        private void ProcessarCabecDoc(VndBEDocumentoVenda DocVenda)
        {
            int vdDadosTodos = (int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosTodos;
            int vdDadosCondPag = (int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosCondPag;

            DocVenda.Tipodoc = "FTE";
            DocVenda.Serie = BSO.Base.Series.DaSerieDefeito("V", DocVenda.Tipodoc);
            DocVenda.Entidade = linhaDict["Benef"];
            DocVenda.TipoEntidade = "C";
            BSO.Vendas.Documentos.PreencheDadosRelacionados(DocVenda, ref vdDadosTodos);

            DocVenda.DataDoc = DateTime.Now;
            DocVenda.HoraDefinida = false;
            DocVenda.CondPag = "01";
            BSO.Vendas.Documentos.PreencheDadosRelacionados(DocVenda, ref vdDadosCondPag);
        }

        private void ProcessarLinha(VndBEDocumentoVenda DocVenda, string tipoFatura)
        {
            // Linha 1 - Descrição com NºContador + Consumo Total
            // Linha 2 - Última leitura do ano passado + úlitma leitura feita este ano.
            string descricao = String.Format("Contador {0}. Consumo total de {1} m³.", linhaDict["Contador"], linhaDict["Consumo"]);
            string descricao2 = String.Format("Leitura inicial: {0} m³. Leitura final: {1} m³ ({2}).", linhaDict["UltimaLeitura"], linhaDict["LeituraFinal"], linhaDict["DataLeituraFinal"]);
            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: descricao); _counterLinha++;
            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: descricao2); _counterLinha++;

            // Linhas 3 até 8 - Leituras + TRH
            // _counterLinha começa na linha 3. Variável global para dar track às linhas na fatura independentemente do nº de contadores
            // CUIDADO - implementação de while(true) depende de uso eficaz e deliberado de breaks e continues para evitar loop infinito.
            while (true)
            {
                // Contagens
                // PSO.MensagensDialogos.MostraAviso(linhaDict["Contador"] + " - " + linhaDict["Consumo1"] + " -> " + linhaDict["Consumo2"] + " -> " + linhaDict["Consumo3"]);
                if (linhaDict["Consumo1"] != "0") { CriarLinhaConsumo(DocVenda, 1); linhaDict["Consumo1"] = "0"; _counterLinha++; continue; }
                if (linhaDict["Consumo2"] != "0") { CriarLinhaConsumo(DocVenda, 2); linhaDict["Consumo2"] = "0"; _counterLinha++; continue; }
                if (linhaDict["Consumo3"] != "0") { CriarLinhaConsumo(DocVenda, 3); linhaDict["Consumo3"] = "0"; _counterLinha++; }

                // Calcula TRH que depende da cultura e popula linhaDict["TRH"]. Esse valor é então usado como preço unitário na linha na fatura
                CalcRegantes_TaxaRecursosHidricos(linhaDict["Cultura"]);
                double precUnitTRH = Convert.ToDouble(linhaDict["TRH"]);

                double quantidadeTRH = 1;
                string armazem = ""; string localizacao = "";

                BSO.Vendas.Documentos.AdicionaLinha(DocVenda, "TRH", ref quantidadeTRH, ref armazem, ref localizacao, precUnitTRH);
                VndBELinhaDocumentoVenda linhaTRH = DocVenda.Linhas.GetEdita(_counterLinha);
                linhaTRH.Descricao = "Taxa de Recursos Hídricos";
                _counterLinha++;

                // Linha em branco para separar do próximo contador
                BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: " "); _counterLinha++;
                break;
            }
        }

        private void CriarLinhaConsumo(VndBEDocumentoVenda DocVenda, int escalao)
        {
            double quantidade = Convert.ToDouble(linhaDict["Consumo" + escalao]);
            double precUnit = Convert.ToDouble(linhaDict["Taxa" + escalao]);
            string armazem = ""; string localizacao = "";

            BSO.Vendas.Documentos.AdicionaLinha(DocVenda, "TE", ref quantidade, ref armazem, ref localizacao, precUnit);

            VndBELinhaDocumentoVenda linha = DocVenda.Linhas.GetEdita(_counterLinha);
            linha.Descricao = String.Format("{0}", _escaloes[escalao]);
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
                    listBoxErros_WF.Items.Add(String.Format("Contador {0} para Benef {1} processado com sucesso na Fatura {2}.", linhaDict["Contador"], DocVenda.Entidade, DocVenda.NumDoc));
                    listBoxErros_WF.SelectedIndex = listBoxErros_WF.Items.Count - 1;
                    return "";
                }
                else { return strErro; }
            }
            catch (Exception e) { _comErro = true; return e.ToString(); }
        }


        private void CalcRegantes(string tipoFatura)
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
            _consumoTotal = CalcRegantes_ConsumoTotal();
            linhaDict["Consumo"] = _consumoTotal.ToString();

            //Define _consumo1, _consumo2, _consumo3
            CalcRegantes_Consumos();
            //Define _taxa1, _taxa2, _taxa3 a serem aplicadas a cada consumo. _taxa1 é a mais baixa da tabela se _ano = 2022;
            CalcRegantes_TaxasPenalizadoras();

            linhaDict["Taxa1"] = _taxa1.ToString();
            linhaDict["Consumo1"] = _consumo1.ToString();
            linhaDict["Taxa2"] = _taxa2.ToString();
            linhaDict["Consumo2"] = _consumo2.ToString();
            linhaDict["Taxa3"] = _taxa3.ToString();
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

        private void CalcRegantes_Consumos()
        {
            // Separação do consumo total pelos três escalões. Preenche o 1º escalão até ao seu limite antes de ir pro 2º. Será ignorado se for zero.
            // Se houver mais que 7000 de consumo, 5000 ficam no primeiro escalão, 2000 (diferença entre 5000 e 7000) ficam no segundo e o restante no terceiro.
            if (_cultura != "CA")
            { _escalao1 = 5000; _escalao2 = 2000; }
            else
            { _escalao1 = 12000; _escalao2 = 2000; }

            int escalao1e2 = _escalao1 + _escalao2;
            _consumo1 = 0; _consumo2 = 0; _consumo3 = 0;

            if (_consumoTotal >= escalao1e2)
            {
                _consumo1 = _escalao1; _consumo2 = _escalao2; _consumo3 = _consumoTotal - escalao1e2;
            }
            else if (_consumoTotal > _escalao1 && _consumoTotal <= escalao1e2)
            {
                _consumo1 = _escalao1; _consumo2 = _consumoTotal - _escalao1; _consumo3 = 0;
            }
            else { _consumo1 = _consumoTotal; _escalao2 = 0; _consumo3 = 0; }
        }

        private void CalcRegantes_TaxasPenalizadoras()
        {
            if (_cultura != "CA")
            {
                _taxa1 = DictTaxa[_cultura].Valor("CDU_escalaoUm");
                _taxa2 = DictTaxa[_cultura].Valor("CDU_escalaoDois");
                _taxa3 = DictTaxa[_cultura].Valor("CDU_escalaoTres");
            }
            else
            {
                _taxa1 = DictTaxa[_cultura].Valor("CDU_escalaoArrozUm");
                _taxa2 = DictTaxa[_cultura].Valor("CDU_escalaoArrozDois");
                _taxa3 = DictTaxa[_cultura].Valor("CDU_escalaoArrozTres");
            }

            if (_ano == 2022)
            {
                _taxa1 = DictTaxa["PD"].Valor("CDU_escalaoUm");
            }
        }

        private void CalcRegantes_TaxaRecursosHidricos(string cultura)
        {
            double baseTRH_U = 0.000706;
            double baseTRH_A = 0.0035;
            double reducao25 = 0.25;
            double agravamento = 1.2;
            double reducao10 = 0.1;
            double reducao90 = 0.9;

            double TRH_U, TRH_A;
            double consumoTotal = Convert.ToDouble(_consumoTotal);

            //Calculos da TRH.
            // São calculados valores do ComponenteU e do Componente A em separado, com _consumoTotal como base. A TRH é a adição de ambos os valores finais.

            // *** Componente U ***
            // _consumoTotal multiplicado pelo valor base TRH do Comp U -> resultado reduzido por 25%
            TRH_U = consumoTotal * baseTRH_U;
            TRH_U = TRH_U * reducao25;
            if (cultura == "CA") { TRH_U = TRH_U * reducao90; }

            // *** Componente A ***
            // _consumoTotal multiplicado pelo valor base TRH do Comp A - > resultado reduzido por 25% -> resultado agravado em 120%
            TRH_A = consumoTotal * baseTRH_A;
            TRH_A = TRH_A * reducao25;
            TRH_A = TRH_A * agravamento;
            if (cultura == "CA") { TRH_A = TRH_A * reducao90; }

            // *** TRH FINAL ***
            // Adição de TRH_A e TRH_U
            linhaDict["TRH"] = (TRH_U + TRH_A).ToString();
        }


        private void ResetVariaveis()
        {
            _counterLinha = 1;
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

            // Dicts com as descrições para cada escalão.
            _escaloes[1] = "Até 5000 m³"; _escaloes[2] = "Entre 5000 e 7000 m³"; _escaloes[3] = "Mais que 7000 m³";
            _escaloesArroz[1] = "Até 12000 m³"; _escaloesArroz[2] = "Entre 12000 e 14000 m³"; _escaloesArroz[3] = "Mais que 14000 m³";

            //DtGridExploracao.AutoGenerateColumns = true;
            //DtGridExploracao.AutoSize = true;
            //DtGridExploracao.ScrollBars = ScrollBars.Both;
            //DtGridExploracao.DataSource = DtSet.Tables[0];
            //DtGridExploracao.Refresh();
            //PSO.MensagensDialogos.MostraErro("Load Finished");
        }

        private void ErroAoEmitir(string erroSistema)
        {
            listBoxErros_WF.Items.Add("");
            listBoxErros_WF.Items.Add("*** ERRO ***");
            listBoxErros_WF.Items.Add(String.Format("Contador {0} para Benef {1} - {2} não foi processado correctamente.", linhaDict["Contador"], linhaDict["Benef"], linhaDict["Nome"]));
            listBoxErros_WF.Items.Add("");
            listBoxErros_WF.Items.Add("Por favor verificar os dados na folha de Excel relativos a este contador.");
            listBoxErros_WF.Items.Add("ATENÇÃO: Nenhuma fatura foi emitida. Só serão emitidas faturas quando todas as linhas do ficheiro Excel forem válidas.");

            string erro = "";
            //foreach (var kv in linhaDict)
            //{
            //    erro = erro + "\n" + kv.Key + ": " + kv.Value;
            //}
            erro = erro + "\n\n *** Erro de sistema ***\n" + erroSistema;

            StdPlatBS PSO = new StdPlatBS();
            PSO.MensagensDialogos.MostraErro("Dados inválidos na fatura ou cliente. Ver detalhes abaixo", StdPlatBS100.StdBSTipos.IconId.PRI_Critico, erro);
            _comErro = true;

            if (BSO.EmTransaccao()) { BSO.DesfazTransaccao(); }
        }
    }
}


