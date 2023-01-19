using Primavera.Extensibility.BusinessEntities; using Primavera.Extensibility.CustomForm;
using Primavera.Extensibility.Sales.Editors;
using System; using System.Data; using System.Windows.Forms; using System.Collections.Concurrent;
using BasBE100; using StdBE100; using VndBE100;
using System.Collections.Generic; using System.IO; using System.Linq; using System.Text; using System.Threading.Tasks; 

namespace ASRLB_ImportacaoFatura.Sales
{
    public partial class janelaFaturasExploracao_test : CustomForm
    {
        public static Dictionary<string, string> linhaDict { get; set; }
        public Dictionary<int, string> _escaloes = new Dictionary<int, string>();
        public Dictionary<int, string> _escaloesArroz = new Dictionary<int, string>();
        DataSet DtSet = new DataSet();
        private int _counterLinha = 3;
        private string _benefAnterior, _faturasComErro = "Benefs: \n";
        private bool _terminarFatura = false, _temErros = false;        

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
            _faturasComErro =  "Benefs: \n";
            _temErros = false;
            _terminarFatura = false;
        }

        private void formFaturasExploracao_Load(object sender, EventArgs e)
        {            
            ExcelControl Excel = new ExcelControl(@"C:/Users/VM/source/repos/ID_Primavera_Extensibility/ASSREG-Faturacao/Mapa de contadores.xlsx");
            linhaDict = new Dictionary<string, string>();
            DtSet = Excel.CarregarDataSet("CANTAO 1", Excel.conString);
            DtSet.Tables[0].DefaultView.Sort = "Benef ASC";

            // Inicialização do Dictionary a ser populado com o conteúdo de cada linha do DtSet. 
            // Dados importados do Excel
            linhaDict.Add("Predio", null);
            linhaDict.Add("Area", null);
            linhaDict.Add("Cultura", null);
            linhaDict.Add("TRH", null);
            linhaDict.Add("TaxaPenalizadora", null);
            linhaDict.Add("Contador", null);
            linhaDict.Add("Benef", null);
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

            for (int i = 0; i < DtSet.Tables[0].Rows.Count; i++)
            {
                DataRow DtRow = DtSet.Tables[0].Rows[i];

                if (!BSO.EmTransaccao()) { BSO.IniciaTransaccao(); }

                // Calcula os valores a colocar na fatura e preenche linhaDict com dados necessários.
                //_counterLinha é usado como controlo de estado de uma fatura. Se _counterLinha == 2, é nova fatura.
                PrepararDict(DtRow);
                CalcRegantes CalcReg = new CalcRegantes();


                // Se o Benef da linha actual não for igual ao da anterior (check em PrepararDict()), então tem todos os contadores do seu Benef e é marcada para ser lançada.
                // Nesse caso, activa-se EmitirFatura, reset ao _counterLinha e começa-se uma fatura nova (sem pular para a próxima linha).
                // Também incluido um catch para a última linha do DataSet.
                if (_counterLinha == 3) { ProcessarCabecDoc(DocVenda); }
                if (!_terminarFatura) { ProcessarLinha(DocVenda); }
                else if (i == DtSet.Tables[0].Rows.Count - 1) { EmitirFatura(DocVenda); }
                else
                {
                    // Valida dados, grava factura e começa nova. Chama TratarErrosFaturacao() se não conseguir submeter fatura.
                    EmitirFatura(DocVenda);
                    
                    DocVenda = new VndBEDocumentoVenda();
                    ProcessarCabecDoc(DocVenda);
                    _counterLinha = 3;
                    ProcessarLinha(DocVenda);
                    _terminarFatura = false;
                }
            }
            // Finalização da Transacção. Se não conseguiu processar todas as linhas, não efectiva nenhuma.
            if (!_temErros) { if (BSO.EmTransaccao()){ BSO.TerminaTransaccao(); } }
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
            string benef = DtRow.Field<string>("Benef");
            linhaDict["Benef"] = benef.PadLeft(5, '0');

            // _benefAnterior utilizado para definir quando terminar a fatura.
            if (_counterLinha == 2) { _benefAnterior = linhaDict["Benef"]; }
            if (linhaDict["Benef"] != _benefAnterior) { _terminarFatura = true; }

            linhaDict["Nome"] = DtRow.Field<string>("Nome");
            linhaDict["UltimaLeitura"] = DtRow.Field<double>("Última Leitura").ToString();
            linhaDict["Data1"] = DtRow.Field<DateTime>("Data 1").ToString();
            linhaDict["Leitura1"] = DtRow.Field<double>("Leitura 1").ToString();
            linhaDict["Data2"] = DtRow.Field<DateTime>("Data 2").ToString();
            linhaDict["Leitura2"] = DtRow.Field<double>("Leitura 2").ToString();
            //linhaDict["Data3"] = DtRow.Field<DateTime>("Data 3").ToString();
            //linhaDict["Leitura3"] = DtRow.Field<double>("Leitura 3").ToString();

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

            // Transform do Benef para ser igual às Entidades no Primavera.
            listBox.Items.Add(linhaDict["Benef"] = benef.PadLeft(5, '0'));

            listBox.Items.Add(linhaDict["Nome"] = DtRow.Field<string>("Nome")) ;
            listBox.Items.Add(linhaDict["UltimaLeitura"] = DtRow.Field<double>("Última Leitura").ToString());
            listBox.Items.Add(linhaDict["Data1"] = DtRow.Field<DateTime>("Data 1").ToString());
            listBox.Items.Add(linhaDict["Leitura1"] = DtRow.Field<double>("Leitura 1").ToString());
            listBox.Items.Add(linhaDict["Data2"] = DtRow.Field<DateTime>("Data 2").ToString());
            listBox.Items.Add(linhaDict["Leitura2"] = DtRow.Field<double>("Leitura 2").ToString());
            //linhaDict["Data3"] = DtRow.Field<DateTime>("Data 3").ToString();
            //linhaDict["Leitura3"] = DtRow.Field<double>("Leitura 3").ToString();

            // Reset aos valores calculados por CalcRegantes;
            linhaDict["Taxa3"] = null;
            linhaDict["Consumo3"] = null;
        }

        private void CalcRegantes_Consumos()
        {

        }

        private void CalcRegantes_TaxasPenalizadoras()
        {

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

            // TEST TEST TEST 
            string format = @"*** CabecDoc ***
            NumDoc: {0}
            TipoDoc: {1}
            Série: {2}            
            Entidade: {3}

            DataDoc: {4}
            _counterLinha: {5}
            ";
            //MessageBox.Show(string.Format(format, DocVenda.NumDoc, DocVenda.Tipodoc, DocVenda.Serie, DocVenda.Entidade, DocVenda.DataDoc, _counterLinha));
            // END TEST
        }

        private void ProcessarLinha(VndBEDocumentoVenda DocVenda)
        {
            // Linha 0 - Descrição com NºContador + Consumo Total
            // Linha 1 - Última leitura do ano passado + úlitma leitura feita este ano.
            string descricao = String.Format("Contador {0} em Cultura {1}. Consumo total de {2} m³.", linhaDict["Contador"], linhaDict["Cultura"], linhaDict["Consumo"]);
            string descricao2 = String.Format("Última Leitura do ano passado: {0} m³. Leitura final deste ano: {1} m³ a {2}.", linhaDict["UltimaLeitura"], linhaDict["LeituraFinal"], linhaDict["DataLeituraFinal"]);
            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: descricao);
            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, Descricao: descricao2);

            // _counterLinha começa na linha 2. int da Classe para dar track às linhas na fatura independentemente do nº de contadores
            int escalao = 1;
            int totalLeituras = Convert.ToInt16(linhaDict["TotalLeituras"]);
            var linha = new VndBELinhaDocumentoVenda();

            // Linhas 2 até 4 - Leituras
            while (totalLeituras > 0)
            {
                double quantidade = Convert.ToDouble(linhaDict["Consumo" + escalao]);
                double precUnit = Convert.ToDouble(linhaDict["Taxa" + escalao]);
                string armazem = "", localizacao = "";

                BSO.Vendas.Documentos.AdicionaLinha(DocVenda, "TE", ref quantidade, ref armazem, ref localizacao, precUnit);
                linha = DocVenda.Linhas.GetEdita(_counterLinha);
                linha.Descricao = String.Format("Cultura {0} - {1}", linhaDict["Cultura"], _escaloes[escalao]);
                linha.Quantidade = Convert.ToDouble(linhaDict["Consumo" + escalao]);
                linha.PrecUnit = Convert.ToDouble(linhaDict["Taxa" + escalao]);

                _counterLinha++; 
                escalao++;
                totalLeituras--;

                // TEST TEST TEST
                string format = @"*** Linha ***
                _counterLinha: {0}
                totalLeituras: {1}
                escalao: {2}

                Descricao: {3}
                Quantidade: {4}
                PrecUnit: {5}

                ";
                //MessageBox.Show(string.Format(format, _counterLinha, totalLeituras, escalao, linha.Descricao, linha.Quantidade, linha.PrecUnit));
                // END TEST
            }
            totalLeituras = 0;

            // TRH na última linha (deste contador)
            BSO.Vendas.Documentos.AdicionaLinha(DocVenda, "TE");
            linha = DocVenda.Linhas.GetEdita(_counterLinha);
            linha.Descricao = "Taxa de Recursos Hídricos";
            _counterLinha++;

            // Deixa uma linha de intervalo para o próximo contador a ser faturado; para ser mais fácil de ler.
            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocVenda, BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario);
            _counterLinha++;
        }

        private void EmitirFatura(VndBEDocumentoVenda DocVenda)
        {
            string strAvisos = "", strErro = "", serie = "";

            BSO.Vendas.Documentos.CalculaValoresTotais(DocVenda);

            if (BSO.Vendas.Documentos.ValidaActualizacao(DocVenda, BSO.Vendas.TabVendas.Edita(DocVenda.Tipodoc), ref serie, ref strErro))
            {
                try
                {
                    BSO.Vendas.Documentos.Actualiza(DocVenda, ref strAvisos);
                    listBox.Items.Add(String.Format("Contador {0} para Benef {1} processada com sucesso.", linhaDict["Contador"], DocVenda.Entidade));
                }
                catch (Exception e) { }
                //finally { if (BSO.EmTransaccao()) { BSO.DesfazTransaccao(); } }
                
            }
            else
            {
                MessageBox.Show(strErro);
                listBox.Items.Add(String.Format("Ocorreram erros ao gerar a Fatura {0} para Benef {1}. ERRO: {2} \n A INFORMAÇÃO NÃO FOI PROCESSADA!", DocVenda.NumDoc, DocVenda.Entidade, strErro));
                
                // Usar no "Detalhe" do Dialogo de erro.
                _faturasComErro = _faturasComErro + linhaDict["Benef"] + "\n";
                _temErros = true;
            }
        }

    }
}
