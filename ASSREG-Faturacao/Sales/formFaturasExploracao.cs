using Primavera.Extensibility.BusinessEntities; using Primavera.Extensibility.CustomForm;
using Primavera.Extensibility.Sales.Editors;
using System; using System.Data; using System.Windows.Forms; using System.Collections.Concurrent; using System.Threading;
using BasBE100; using StdBE100; using VndBE100;
using System.Collections.Generic; using System.IO; using System.Linq; using System.Text; using System.Threading.Tasks; 

namespace ASRLB_ImportacaoFatura.Sales
{
    public partial class janelaFaturasExploracao : CustomForm
    {
        public static Dictionary<string, string> linhaDict { get; set; }
        public Dictionary<int, string> _escaloes = new Dictionary<int, string>();
        public Dictionary<int, string> _escaloesArroz = new Dictionary<int, string>();
        DataSet DtSet = new DataSet();
        private int _counterLinha = 3;
        private string _faturasComErro = "Benefs: \n";
        private bool _novaFatura;

        //CalcRegantes globals
        private string _dataFull, _cultura;
        private int _ano, _consumoTotal, _ultimaLeitura, _leitura1, _leitura2;
        private int _escalao1, _escalao2;
        private double _taxa1, _taxa2, _taxa3;
        private double _consumo1, _consumo2, _consumo3;

        // **** BUGS ****
        // 

        public janelaFaturasExploracao()
        {
            InitializeComponent();
        }

        private void janelaFaturasExploracao_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Reset das variáveis da Classe para prevenir bugs quando se abre e fecha a janela;
            linhaDict = null; linhaDict = new Dictionary<string, string>();
            DtSet = new DataSet();
            _counterLinha = 3;
            _faturasComErro = "";
            _novaFatura = true;
        }

        private void formFaturasExploracao_Load(object sender, EventArgs e)
        {            
            ExcelControl Excel = new ExcelControl(@"C:/Users/VM/source/repos/ID_Primavera_Extensibility/ASSREG-Faturacao/Mapa de contadores.xlsx");
            linhaDict = new Dictionary<string, string>();
            DtSet = Excel.CarregarDataSet("CANTAO 1", Excel.conString);
            //DtSet.Tables[0].DefaultView.Sort = "Benef ASC";

            // Inicialização do Dictionary a ser populado com o conteúdo de cada linha do DtSet. 
            // Dados importados do Excel
            linhaDict.Add("Predio", null);
            linhaDict.Add("Area", null);
            linhaDict.Add("Cultura", null);
            linhaDict.Add("TRH", null);
            linhaDict.Add("TaxaPenalizadora", null);
            linhaDict.Add("Contador", null);
            linhaDict.Add("ContadorAnt", "");
            linhaDict.Add("Benef", "");
            linhaDict.Add("Nome", null);
            linhaDict.Add("UltimaLeitura", null);
            linhaDict.Add("Data1", null);
            linhaDict.Add("Leitura1", null);
            linhaDict.Add("Data2", null);
            linhaDict.Add("Leitura2", null);
            //linhaDict.Add("Data3", null);
            //linhaDict.Add("Leitura3", null);
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


            DtGridExploracao.AutoGenerateColumns = true;
            DtGridExploracao.AutoSize = true;
            DtGridExploracao.ScrollBars = ScrollBars.Both;
            DtGridExploracao.DataSource = DtSet.Tables[0];
            DtGridExploracao.Refresh();
            PSO.MensagensDialogos.MostraErro("Load Finished");
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            VndBEDocumentoVenda DocVenda = new VndBEDocumentoVenda();
            //_novaFatura = true;
            //_counterLinha = 3;

            for (int i = 0; i < DtSet.Tables[0].Rows.Count; i++)
            {
                DataRow DtRow = DtSet.Tables[0].Rows[i];
                if (!BSO.EmTransaccao()) { BSO.IniciaTransaccao(); }

                // benefIgual é True se Benef na nova linha for igual ao da linha anterior (ainda no linhaDict)
                // Se Benef for diferente, então é necessário emitir a fatura antes de começar uma nova.
                bool benefIgual = DtRow.Field<string>("Benef").PadLeft(5, '0').Equals(linhaDict["Benef"]);
                // Se contador for o mesmo que o anterior, então é um caso de 1 contador -> multiplos prédios. Neste caso a linha tem de ser completamente ignorada pois os valores são iguais ao da primeira.
                // Contador é inicializado como "" por isso não vai bater igual na primeira linha.
                bool contadorIgual = DtRow.Field<string>("Nº Contador").Equals(linhaDict["Contador"]);

                PSO.MensagensDialogos.MostraErro("" + _counterLinha, StdPlatBS100.StdBSTipos.IconId.PRI_Exclama, "benefIgual: " + benefIgual + "\ncontadorIgual: " + contadorIgual + "\nlinha " + (i + 1));
                if (contadorIgual && i > 0) { continue; } // Contador é igual ao da linha anterior em casos de desunião de linhas que têm 'Um contador -> Vários prédios/áreas'. Ignora-se estas linhas pois têm valores duplicados.

                if (!benefIgual)
                {
                    if (i > 0) EmitirFatura(DocVenda); // Exclui primeira linha por ainda não existir nada
                    DocVenda = new VndBEDocumentoVenda();
                    _novaFatura = true;
                    _counterLinha = 3;

                    PrepararDict(DtRow); // Preenche dicionário com dados necessários.
                    ProcessarCabecDoc(DocVenda);
                    CalcRegantes(); // Efectua cálculos de valores e taxas associadas.
                    //if (_novaFatura) { ; _novaFatura = false; }
                    ProcessarLinha(DocVenda); // Preenche linhasDoc com descrições e leituras com seus valores calculados.
                }
                else if (benefIgual && !contadorIgual && i > 0) // Um benef -> Vários contadores. Vão todos os contadores para a mesma fatura
                {
                    PrepararDict(DtRow);
                    CalcRegantes();
                    ProcessarLinha(DocVenda);
                }

                if (i == DtSet.Tables[0].Rows.Count - 1) { EmitirFatura(DocVenda); } // Catch para a última linha do DataSet
            }

            // Finalização da Transacção. Se não foi processar todas as linhas, não efectiva nenhuma factura.
            // _faturasComErro é uma lista de Benefs que fatura não foi processada.
            if (_faturasComErro == "Benefs: \n")
            {
                if (BSO.EmTransaccao()) { BSO.TerminaTransaccao(); }
            }
            else
            {
                PSO.MensagensDialogos.MostraErro("Ocorreram erros durante a criação das faturas.", StdPlatBS100.StdBSTipos.IconId.PRI_Critico, _faturasComErro);
                if (BSO.EmTransaccao()) { BSO.DesfazTransaccao(); }
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
            linhaDict["Benef"] = DtRow.Field<string>("Benef").PadLeft(5, '0');
            linhaDict["Nome"] = DtRow.Field<string>("Nome");
            linhaDict["UltimaLeitura"] = DtRow.Field<double>("Última Leitura").ToString();
            linhaDict["Data1"] = DtRow.Field<DateTime>("Data 1").ToString("'dd'/'MM'/'yyyy'");
            linhaDict["Leitura1"] = DtRow.Field<double>("Leitura 1").ToString();
            linhaDict["Data2"] = DtRow.Field<DateTime>("Data 2").ToString("'dd'/'MM'/'yyyy'");
            linhaDict["Leitura2"] = DtRow.Field<double>("Leitura 2").ToString();

            // Reset aos valores calculados por CalcRegantes;
            linhaDict["Taxa1"] = null;
            linhaDict["Consumo1"] = null;
            linhaDict["Taxa2"] = null;
            linhaDict["Consumo2"] = null;
            linhaDict["Taxa3"] = null;
            linhaDict["Consumo3"] = null;


            listBox.Items.Add(linhaDict["Predio"] = DtRow.Field<string>("Prédio"));
            listBox.Items.Add(linhaDict["Area"] = DtRow.Field<double>("Área").ToString());
            listBox.Items.Add(linhaDict["Cultura"] = DtRow.Field<string>("Cultura"));
            listBox.Items.Add(linhaDict["TRH"] = DtRow.Field<string>("TRH").ToString());
            listBox.Items.Add(linhaDict["TaxaPenalizadora"] = DtRow.Field<string>("Tx Penalizadora"));
            listBox.Items.Add(linhaDict["Contador"] = DtRow.Field<string>("Nº Contador"));

            listBox.Items.Add(linhaDict["Benef"] = DtRow.Field<string>("Benef").PadLeft(5, '0'));
            listBox.Items.Add(linhaDict["Nome"] = DtRow.Field<string>("Nome"));
            listBox.Items.Add(linhaDict["UltimaLeitura"] = DtRow.Field<double>("Última Leitura").ToString());
            listBox.Items.Add(linhaDict["Data1"] = DtRow.Field<DateTime>("Data 1").ToString());
            listBox.Items.Add(linhaDict["Leitura1"] = DtRow.Field<double>("Leitura 1").ToString());
            listBox.Items.Add(linhaDict["Data2"] = DtRow.Field<DateTime>("Data 2").ToString());
            listBox.Items.Add(linhaDict["Leitura2"] = DtRow.Field<double>("Leitura 2").ToString());

            // Reset aos valores calculados por CalcRegantes;
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

        private void ProcessarLinha(VndBEDocumentoVenda DocVenda)
        {
            // Linha 1 - Descrição com NºContador + Consumo Total
            // Linha 2 - Última leitura do ano passado + úlitma leitura feita este ano.
            string descricao = String.Format("Contador {0} em Cultura {1}. Consumo total de {2} m³.", linhaDict["Contador"], linhaDict["Cultura"], linhaDict["Consumo"]);
            string descricao2 = String.Format("Última Leitura do ano passado: {0} m³. Leitura final deste ano: {1} m³ a {2}.", linhaDict["UltimaLeitura"], linhaDict["LeituraFinal"], linhaDict["DataLeituraFinal"]);
            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: descricao);
            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: descricao2);
            

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

                // TRH
                double quantidadeTRH = 1, precUnitTRH = 1;
                string armazem = ""; string localizacao = "";

                BSO.Vendas.Documentos.AdicionaLinha(DocVenda, "TE", ref quantidadeTRH, ref armazem, ref localizacao, precUnitTRH);
                VndBELinhaDocumentoVenda linhaTRH = DocVenda.Linhas.GetEdita(_counterLinha);
                linhaTRH.Descricao = "Taxa de Recursos Hídricos";
                _counterLinha++;

                // Linha em branco para separar do próximo contador
                BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: "-"); break;
                _counterLinha++;

                BSO.Vendas.Documentos.CalculaValoresTotais(DocVenda);
            }
        }
        
        private void CriarLinhaConsumo (VndBEDocumentoVenda DocVenda, int escalao)
        {
            double quantidade = Convert.ToDouble(linhaDict["Consumo" + escalao]);
            double precUnit = Convert.ToDouble(linhaDict["Taxa" + escalao]);
            string armazem = ""; string localizacao = "";

            BSO.Vendas.Documentos.AdicionaLinha(DocVenda, "TE", ref quantidade, ref armazem, ref localizacao, precUnit);

            VndBELinhaDocumentoVenda linha = DocVenda.Linhas.GetEdita(_counterLinha);
            linha.Descricao = String.Format("Cultura {0} - {1}", linhaDict["Cultura"], _escaloes[escalao]);
            linha.Quantidade = Convert.ToDouble(linhaDict["Consumo" + escalao]);
            linha.PrecUnit = Convert.ToDouble(linhaDict["Taxa" + escalao]);
        }

        private void EmitirFatura(VndBEDocumentoVenda DocVenda)
        {
            string strAvisos = "", strErro = "", serie = "";
            
            BSO.Vendas.Documentos.CalculaValoresTotais(DocVenda);

            PSO.MensagensDialogos.MostraAviso("EmitirFatura _counterLinha: " + _counterLinha, StdPlatBS100.StdBSTipos.IconId.PRI_Exclama, "DocVenda: " + DocVenda.Tipodoc + "\nDocvenda.Tipodoc: " + DocVenda.Tipodoc + "\nSerie: " + serie + "\nstrErro: " + strErro);

            if (BSO.Vendas.Documentos.ValidaActualizacao(DocVenda, BSO.Vendas.TabVendas.Edita(DocVenda.Tipodoc), ref serie, ref strErro))       
            {
                try
                {
                    BSO.Vendas.Documentos.Actualiza(DocVenda, ref strAvisos, ref strErro);
                    listBox.Items.Add(String.Format("Contador {0} para Benef {1} processada com sucesso.", linhaDict["Contador"], DocVenda.Entidade));
                }
                catch (Exception e) { MessageBox.Show(e.ToString()); if (BSO.EmTransaccao()) { BSO.DesfazTransaccao(); } }
                //finally { }
            }
            else
            {
                //MessageBox.Show(strErro);
                listBox.Items.Add(String.Format("Ocorreram erros ao gerar a Fatura {0} para Benef {1}. ERRO: {2} \n A INFORMAÇÃO NÃO FOI PROCESSADA!", DocVenda.NumDoc, DocVenda.Entidade, strErro));

                // Usar no "Detalhe" do Dialogo de erro.
                _faturasComErro = _faturasComErro + linhaDict["Benef"] + "\n";
            }
        }


        private void CalcRegantes()
        {
            _cultura = linhaDict["Cultura"];
            _dataFull = linhaDict["Data1"];
            _leitura1 = Convert.ToInt32(linhaDict["Leitura1"]);
            _leitura2 = Convert.ToInt32(linhaDict["Leitura2"]);
            //_leitura3 = Convert.ToInt32(linhaDict["Leitura3"]);
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
            if (linhaDict["Leitura1"] == null)
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

            if (_consumoTotal > escalao1e2)
            {
                _consumo1 = _escalao1; _consumo2 = _escalao2; _consumo3 = _consumoTotal % escalao1e2;
            }
            else if (_consumoTotal > _escalao1 && _consumoTotal < escalao1e2)
            {
                _consumo1 = _escalao1; _consumo2 = _consumoTotal % _escalao1; _consumo3 = 0;
            }
            else
            {
                _consumo1 = _consumoTotal; _escalao2 = 0; _consumo3 = 0;
            }

                //double consumoCorrente;
                //bool calcFinal= false;

                //consumoCorrente = _escalao1 - _consumoTotal;
                //if (consumoCorrente < 0) {
                //    _consumo1 = _escalao1; 
                //    consumoCorrente =- consumoCorrente; } 
                //else { 
                //    _consumo1 = consumoCorrente;
                //    }

                //if (consumoCorrente > 0) {
                //    consumoCorrente = _escalao2 - consumoCorrente;
                //    _consumo2 = _escalao2;
                //    consumoCorrente =- consumoCorrente; }
                //else {
                //    _consumo2 = consumoCorrente; }
            }

        private void CalcRegantes_TaxasPenalizadoras()
        {
            StdBELista listaTaxa = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = '" + _cultura + "'");
            listaTaxa.Inicio();

            if (_cultura != "CA")
            {
                _taxa1 = listaTaxa.Valor("CDU_escalaoUm");
                _taxa2 = listaTaxa.Valor("CDU_escalaoDois");
                _taxa3 = listaTaxa.Valor("CDU_escalaoTres");
            }
            else
            {
                _taxa1 = listaTaxa.Valor("CDU_escalaoArrozUm");
                _taxa2 = listaTaxa.Valor("CDU_escalaoArrozDois");
                _taxa3 = listaTaxa.Valor("CDU_escalaoArrozTres");
            }

            if (_ano == 2022)
            {
                listaTaxa = BSO.Consulta("SELECT CDU_escalaoUm FROM TDU_TaxaPenalizadora WHERE CDU_Cultura = 'PD'");
                listaTaxa.Inicio();
                _taxa1 = listaTaxa.Valor("CDU_escalaoUm");
            }

            listaTaxa.Dispose();
        }
    }
}
