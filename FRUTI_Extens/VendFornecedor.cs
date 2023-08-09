using System;
using System.Windows.Forms;
using PRISDK100;
using ErpBS100;
using StdBE100;
using StdPlatBS100;

namespace FRUTI_Extens
{
    public partial class VendFornecedor : Form
    {
        private ErpBS _BSO;
        private StdPlatBS _PSO;

        public VendFornecedor()
        {
            InitializeComponent();
        }

        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
            string relatorio, rSel, gSel, titulo, dataInicial, dataFinal, detalhe;

            titulo = "Vendas Por Fornecedor";
            relatorio = "VendArt2";
            dataInicial = dtPicker_dataInicial.Value.ToString();
            dataFinal = dtPicker_dataFinal.Value.ToString();

            _PSO.Mapas.Inicializar("ERP");
            _PSO.Mapas.VerificarBdAntesImpressao = true;
            _PSO.Mapas.SelectionFormula = @"
                {CabecDoc.Data} >= " + dataInicial +
                " and {CabecDoc.Data} <= " + dataFinal +
                " and {SubFamilias.SubFamilia} = '" + f4_artigo.Text +
                "' and {DocumentosVenda.TipoDocumento} = 4";
            _PSO.Mapas.JanelaPrincipal = 1;
            _PSO.Mapas.AddFormula("Titulo", "' " + titulo + " (" + dataInicial + " até " + dataFinal + ")'");
            _PSO.Mapas.ImprimeListagem(relatorio, blnModal: true);
        }

        private void VendFornecedor_Load(object sender, EventArgs e)
        {
            Motor.PriEngine.CreateContext("0012004", "faturacao", "*Pelicano*");
            _BSO = Motor.PriEngine.Engine;
            _PSO = Motor.PriEngine.Platform;

            f4_artigo.Inicializa(Motor.PriEngine.PriSDKContexto);

            DateTime dataHoje = DateTime.Now;
            dtPicker_dataInicial.Value = dataHoje.AddDays(-90);
            dtPicker_dataFinal.Value = dataHoje;
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
