using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdBE100;
using PRISDK100;
using HelpersPrimavera10;
using ErpBS100;
using StdPlatBS100;
using System.Data;
using ConstantesPrimavera100;

namespace PP_PPCS
{
    public partial class FormPropDocVenda : CustomForm
    {
        private ErpBS _BSO;
        private StdBSInterfPub _PSO;
        private clsSDKContexto _sdkContexto;

        private HelperFunctions _Helpers = new HelperFunctions();

        public FormPropDocVenda()
        {
            InitializeComponent();

            _PSO = PriMotores.Plataforma;
            _BSO = PriMotores.Motor;
            _sdkContexto = PriMotores.PriSDKContexto;
        }

        private void FormPropDocVenda_Load(object sender, EventArgs e)
        {
            StdBELista RSt = _BSO.Consulta("SELECT 1 AS Dummy;");
            f4TipoDoc.Inicializa(_sdkContexto);
            priGrelhaDocs.Inicializa(_sdkContexto);
            InicializaPrigrelha();
        }

        private void f4TipoDoc_TextChange(object Sender, PRISDK100.F4.TextChangeEventArgs e)
        {
            // Get Serie do TipoDoc para a ComboBox de Série
            if (f4TipoDoc.Text != "") {
                string query = "SELECT DISTINCT Serie FROM SeriesVendas WHERE TipoDoc = '" + f4TipoDoc.Text + "' ORDER BY Serie DESC;";
                cBoxSerie.Items.Clear();
                cBoxSerie.Items.AddRange(FillComboBox(query).ToArray());
                cBoxSerie.SelectedIndex = 0;
            }
        }

        private List<string> FillComboBox(string query)
        {
            using (StdBE100.StdBELista priLista = _BSO.Consulta(query)) {
                List<string> listaFinal = new List<string>();

                if (!priLista.Vazia()) {
                    priLista.Inicio();
                    while (!priLista.NoFim()) {
                        listaFinal.Add(priLista.Valor(0));
                        priLista.Seguinte();
                    }
                    priLista.Termina();

                    return listaFinal;
                }
                return null;
            }
        }

        private void cBoxSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get NumDoc para o NumericUpDown a partir da Série
            string serie = cBoxSerie.Text;
            string tipoDoc = f4TipoDoc.Text;

            DataTable numDocTbl = _Helpers.GetDataTableDeSQL(
                "SELECT TOP(1) NumDoc " +
                "FROM CabecDoc " +
               $"WHERE TipoDoc = '{tipoDoc}' " +
               $"   AND Serie = '{serie}' " +
               $"ORDER BY NumDoc DESC;");

            if (numDocTbl.Rows.Count > 0)
            {
                foreach (DataRow row in numDocTbl.Rows)
                {
                    numUpDownNumDoc.Value = (decimal)row[0];
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string tipoDoc = f4TipoDoc.Text;
            string serie = cBoxSerie.Text;
            int numDoc = (int)numUpDownNumDoc.Value;

            if (_BSO.Vendas.Documentos.Existe("000", tipoDoc, serie, numDoc))
            {
                ActualizaPriGrelha(tipoDoc, serie, numDoc.ToString());
            }
        }

        private void ActualizaPriGrelha(string tipoDoc, string serie, string numDoc)
        {

            StdBELista idCabecDocList = _BSO.Consulta(
                "SELECT Id " +
                "FROM CabecDoc " +
               $"WHERE Filial = N'000' AND TipoDoc = N'{tipoDoc}' AND Serie = N'{serie} AND NumDoc = {numDoc};");

            idCabecDocList.Inicio();
            string id = idCabecDocList.Valor(0);
            id = id.Substring(1, id.Length - 2);


            string priGrelhaQuery =
                "SELECT NumLinha, Artigo, CDU_LoteAux, Descricao, CDU_NomeCientifico, CDU_Caixas, CDU_KilosPorCaixa, Quantidade, Unidade, CDU_Fornecedor, CDU_Origem, CDU_FormaObtencao, CDU_ZonaFAO, Id " +
                "FROM LinhasDoc " +
               $"WHERE IdCabecDoc = '{id} " +
               $"ORDER BY NumLinha;";

            StdBELista priGrelhaList = BSO.Consulta(priGrelhaQuery);
            if (!priGrelhaList.Vazia())
            {
                priGrelhaDocs.DataBind(priGrelhaList);
            }
        }

        private void InicializaPrigrelha()
        {
            priGrelhaDocs.TituloGrelha = "DocumentosVenda";
            priGrelhaDocs.PermiteActualizar = true;
            priGrelhaDocs.PermiteAgrupamentosUser = true;
            priGrelhaDocs.PermiteScrollBars = true;
            priGrelhaDocs.PermiteVistas = false;
            priGrelhaDocs.PermiteEdicao = false;
            priGrelhaDocs.PermiteDataFill = false;
            priGrelhaDocs.PermiteFiltros = false;
            priGrelhaDocs.PermiteActiveBar = false;
            priGrelhaDocs.PermiteContextoVazia = false;

            priGrelhaDocs.AddColKey(strColKey: "N.L.", intTipo: 5, strTitulo: "N.L.", dblLargura: 24, strCamposBaseDados: "NumLinha", blnMostraSempre: true, blnVisivel: true);
            priGrelhaDocs.AddColKey(strColKey: "Artigo", intTipo: 5, strTitulo: "Artigo", dblLargura: 29.5, strCamposBaseDados: "Artigo", blnMostraSempre: true, blnVisivel: true);
            priGrelhaDocs.AddColKey(strColKey: "Lote", intTipo: 5, strTitulo: "Lote", dblLargura: 42, strCamposBaseDados: "CDU_LoteAux", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Descrição", intTipo: 5, strTitulo: "Descrição", dblLargura: 95, strCamposBaseDados: "Descricao", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Nome Científico", intTipo: 5, strTitulo: "Nome Científico", dblLargura: 95, strCamposBaseDados: "CDU_NomeCientifico", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Cx.", intTipo: 5, strTitulo: "Cx.", dblLargura: 30, strCamposBaseDados: "CDU_Caixas", blnDrillDown: true, blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Kg/Cx.", intTipo: 5, strTitulo: "Kg/Cx.", dblLargura: 30, strCamposBaseDados: "CDU_KilosPorCaixa", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Quant.", intTipo: 5, strTitulo: "País", dblLargura: 36, strCamposBaseDados: "Quantidade", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Un.", intTipo: 5, strTitulo: "Un.", dblLargura: 20, strCamposBaseDados: "Unidade", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Forn.", intTipo: 5, strTitulo: "Forn.", dblLargura: 27, strCamposBaseDados: "CDU_Fornecedor", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Origem", intTipo: 5, strTitulo: "Origem", dblLargura: 50, strCamposBaseDados: "CDU_Origem", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Obtenção", intTipo: 5, strTitulo: "Obtenção", dblLargura: 57, strCamposBaseDados: "CDU_FormaObtencao", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Zona FAO", intTipo: 5, strTitulo: "Zona FAO", dblLargura: 81, strCamposBaseDados: "CDU_ZonaFAO", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Id", intTipo: 5, strTitulo: "ID", dblLargura: 24, strCamposBaseDados: "Id", blnMostraSempre: true);

        }
    }
}
