using Primavera.Extensibility.BusinessEntities; using Primavera.Extensibility.CustomForm;
using System; using System.Data; using System.Windows.Forms; using System.Collections.Concurrent;
using BasBE100; using StdBE100;
using System.Collections.Generic; using System.IO; using System.Linq; using System.Text; using System.Threading.Tasks; 

namespace ASRLB_ImportacaoFatura.Sales
{
    public partial class janelaFaturasExploracao : CustomForm
    {
        static Dictionary<string, string> linhaDict = new Dictionary<string, string>();
        DataSet DtSet = new DataSet();
        private int linha = 0;

        public janelaFaturasExploracao()
        {
            InitializeComponent();
        }

        private void formFaturasExploracao_Load(object sender, EventArgs e)
        {
            ExcelControl Excel = new ExcelControl(@"C:/Users/VM/source/repos/ID_Primavera_Extensibility/ASSREG-Faturacao/Mapa de contadores.xlsx");
            DtSet = Excel.CarregarDataSet("CANTAO 1", Excel.conString);
            DtSet.Tables[0].DefaultView.Sort = "Benef ASC";

            // Inicialização do Dictionary a ser populado com o conteúdo de cada linha do DtSet
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

            // Dados extra, não no Excel
            linhaDict.Add("Artigo", null);
            linhaDict.Add("Descricao", null);
            linhaDict.Add("Consumo", null);
            linhaDict.Add("ValorUnitario", null);
            linhaDict.Add("Data3", null);
            linhaDict.Add("Leitura3", null);

            // Valores de consumo separados por escalão. Depende do Prédio.
            linhaDict.Add("Taxa1", null);
            linhaDict.Add("Consumo1", null);
            linhaDict.Add("Taxa2", null);
            linhaDict.Add("Consumo2", null);
            linhaDict.Add("Taxa3", null);
            linhaDict.Add("Consumo3", null);


            DtGridExploracao.AutoGenerateColumns = true;
            DtGridExploracao.AutoSize = true;
            DtGridExploracao.ScrollBars = ScrollBars.Both;
            DtGridExploracao.DataSource = DtSet.Tables[0];
            DtGridExploracao.Refresh();
            PSO.Dialogos.MostraErro("bruh");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataRow linha in DtSet.Tables[0].Rows)
	        {
                PreparaDictParaCalc(linha);
                CalcRegantes CalcReg = new CalcRegantes(linhaDict);
        	}
        }

        private void PreparaDictParaCalc(DataRow linha)
        {
            // Dados Excel
            // TRH e Taxa Penalizadora vêm como 'S' ou 'N'. Se 'S', substituir pelo valor. Se 'N', cancelar.
            linhaDict["Predio"] = linha.Field<string>("Prédio");
            linhaDict["Area"] = linha.Field<string>("Área");
            linhaDict["Cultura"] = linha.Field<string>("Cultura");
            linhaDict["TRH"] = linha.Field<string>("TRH");
            linhaDict["TaxaPenalizadora"] = linha.Field<string>("Tx Penalizadora");
            linhaDict["Contador"] = linha.Field<string>("Nº Contador");
            linhaDict["Benef"] = linha.Field<string>("Benef");
            linhaDict["Nome"] = linha.Field<string>("Nome");
            linhaDict["UltimaLeitura"] = linha.Field<string>("Última Leitura");
            linhaDict["Data1"] = linha.Field<string>("Data 1");
            linhaDict["Leitura1"] = linha.Field<string>("Leitura 1");
            linhaDict["Data2"] = linha.Field<string>("Data 2");
            linhaDict["Leitura2"] = linha.Field<string>("Leitura 2");

            // Reset aos valores calculados por CalcRegantes.ConsumosRegantes();
            linhaDict["Taxa1"] = null;
            linhaDict["Consumo1"] = null;
            linhaDict["Taxa2"] = null;
            linhaDict["Consumo2"] = null;
            linhaDict["Taxa3"] = null;
            linhaDict["Consumo3"] = null;
        }
    }
}
