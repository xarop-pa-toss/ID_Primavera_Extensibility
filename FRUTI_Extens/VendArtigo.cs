using System;
using System.Windows.Forms;
using System.Collections.Generic;
using PRISDK100; using ErpBS100; using StdBE100; using StdPlatBS100;

namespace FRUTI_Extens
{
    public partial class VendArtigo : Form
    {
        private ErpBS _BSO;
        private StdPlatBS _PSO;
        private StdBETransaccao _objStdTransac = new StdBETransaccao();

        public VendArtigo()
        {
            InitializeComponent();
        }

        private void VendArtigo_Load(object sender, EventArgs e)
        {
            Motor.PriEngine.CreateContext(Motor.GetEmpresa.codEmpresa, "id", "*pelicano*");
            _BSO = Motor.PriEngine.Engine;
            _PSO = Motor.PriEngine.Platform;

            DateTime dataHoje = DateTime.Now;
            dtPicker_dataInicial.Value = dataHoje;
            dtPicker_dataFinal.Value = dataHoje.AddDays(-90);

            InicializarSubfamilia();
        }

        private void InicializarSubfamilia()
        {
            List<string> subfamiliasList = new List<string>();

            // Transformar a query em List<string> para usar como DataSource da ComboBox
            StdBELista subfamiliasPri = _BSO.Consulta("SELECT subfamilia, descricao FROM Subfamilias");
            subfamiliasPri.Inicio();

            while (!subfamiliasPri.NoFim()) {
                subfamiliasList.Add(subfamiliasPri.Valor("subfamilia") + " - " + subfamiliasPri.Valor("descricao"));
                subfamiliasPri.Seguinte();
            }

            // Popular a combobox com a List
            cBox_subfamilia.DataSource = subfamiliasList;
            cBox_subfamilia.SelectedIndex = 0;
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
            string relatorio, rSel, titulo, dataInicial, dataFinal;

            titulo = "'Vendas Por SubFamilia ";
            relatorio = "VendArt1";
            dataInicial = "date(" + dtPicker_dataInicial.Value.Date.ToString("yyyy,MM,dd") + ")";
            dataFinal = "date(" + dtPicker_dataFinal.Value.Date.ToString("yyyy,MM,dd") + ")";

            // Get primeira palavra até ao espaço no texto da ComboBox
            string original = cBox_subfamilia.Text;
            int indiceDoEspaco = original.IndexOf(' ');
            string subfamilia = original.Substring(0, indiceDoEspaco);

            _PSO.Mapas.Inicializar("ERP");
            _PSO.Mapas.VerificarBdAntesImpressao = true;
            _PSO.Mapas.SelectionFormula = 
                " {CabecDoc.Data} >= " + dataInicial +
                " and {CabecDoc.Data} <= " + dataFinal +
                " and {SubFamilias.SubFamilia} = '" + subfamilia + "'" +
                " and {DocumentosVenda.TipoDocumento} = 4";
            _PSO.Mapas.JanelaPrincipal = 1;
            _PSO.Mapas.AddFormula("Titulo", titulo + " (" + dtPicker_dataInicial.Value.Date.ToString() + " até " + dtPicker_dataInicial.Value.Date.ToString() + ")'");
            _PSO.Mapas.ImprimeListagem(relatorio, blnModal: true);
        }
    }
}
