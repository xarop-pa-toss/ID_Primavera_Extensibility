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
using UpgradeHelpers.Spread;

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
                decimal numDocMax = Convert.ToDecimal(numDocTbl.Rows[0][0]);

                numUpDownNumDoc.Minimum = 1;
                numUpDownNumDoc.Maximum = numDocMax;
                numUpDownNumDoc.Value = numDocMax;
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
               $"WHERE Filial = N'000' AND TipoDoc = N'{tipoDoc}' AND Serie = N'{serie}' AND NumDoc = {numDoc};");

            idCabecDocList.Inicio();
            Guid id = idCabecDocList.Valor(0);

            string idStr = id.ToString();
            idStr = idStr.Substring(1, idStr.Length - 2);


            string priGrelhaQuery =
                "SELECT NumLinha, Artigo, CDU_LoteAux, Descricao, CDU_NomeCientifico, CDU_Caixas, CDU_KilosPorCaixa, Quantidade, Unidade, CDU_Fornecedor, CDU_Origem, CDU_FormaObtencao, CDU_ZonaFAO, Id " +
                "FROM LinhasDoc " +
               $"WHERE IdCabecDoc = '{id}' " +
               $"ORDER BY NumLinha;";

            StdBELista priGrelhaList = _BSO.Consulta(priGrelhaQuery);
            if (!priGrelhaList.Vazia())
            {
                priGrelhaDocs.DataBind(priGrelhaList);
            }
        }

        private void InicializaPrigrelha()
        {
            priGrelhaDocs.TituloGrelha = "DocumentosVenda";

            priGrelhaDocs.AddColKey(strColKey: "N.L.", intTipo: FpCellType.CellTypeStaticText, strTitulo: "N.L.", dblLargura: 4, strCamposBaseDados: "NumLinha", blnMostraSempre: true, blnVisivel: true);
            priGrelhaDocs.AddColKey(strColKey: "Artigo", intTipo: FpCellType.CellTypeEdit, strTitulo: "Artigo", dblLargura: 5, strCamposBaseDados: "Artigo", blnMostraSempre: true, blnVisivel: true);
            priGrelhaDocs.AddColKey(strColKey: "Lote", intTipo: FpCellType.CellTypeEdit, strTitulo: "Lote", dblLargura: 5, strCamposBaseDados: "CDU_LoteAux", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Descrição", intTipo: FpCellType.CellTypeEdit, strTitulo: "Descrição", dblLargura: 25, strCamposBaseDados: "Descricao", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Nome Científico", intTipo: FpCellType.CellTypeEdit, strTitulo: "Nome Científico", dblLargura: 15, strCamposBaseDados: "CDU_NomeCientifico", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Cx.", intTipo: FpCellType.CellTypeEdit, strTitulo: "Cx.", dblLargura: 5, strCamposBaseDados: "CDU_Caixas", blnDrillDown: true, blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Kg/Cx.", intTipo: FpCellType.CellTypeEdit, strTitulo: "Kg/Cx.", dblLargura: 5, strCamposBaseDados: "CDU_KilosPorCaixa", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Quant.", intTipo: FpCellType.CellTypeEdit, strTitulo: "País", dblLargura: 6, strCamposBaseDados: "Quantidade", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Un.", intTipo: FpCellType.CellTypeEdit, strTitulo: "Un.", dblLargura: 4, strCamposBaseDados: "Unidade", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Forn.", intTipo: FpCellType.CellTypeEdit, strTitulo: "Forn.", dblLargura: 5, strCamposBaseDados: "CDU_Fornecedor", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Origem", intTipo: FpCellType.CellTypeEdit, strTitulo: "Origem", dblLargura: 9, strCamposBaseDados: "CDU_Origem", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Obtenção", intTipo: FpCellType.CellTypeEdit, strTitulo: "Obtenção", dblLargura: 15, strCamposBaseDados: "CDU_FormaObtencao", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Zona FAO", intTipo: FpCellType.CellTypeEdit, strTitulo: "Zona FAO", dblLargura: 15, strCamposBaseDados: "CDU_ZonaFAO", blnMostraSempre: true);
            priGrelhaDocs.AddColKey(strColKey: "Id", intTipo: FpCellType.CellTypeStaticText, strTitulo: "ID", dblLargura: 20, strCamposBaseDados: "Id", blnMostraSempre: true);
        }

        private void priGrelhaDocs_LeaveRow(object Sender, PriGrelha.LeaveRowEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Test");
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            string NumLinha, Artigo, CDU_LoteAux, Descricao, CDU_NomeCientifico, CDU_Caixas, CDU_KilosPorCaixa, Quantidade, Unidade, CDU_Fornecedor, CDU_Origem, CDU_FormaObtencao, CDU_ZonaFAO, Id;

            for (int i = 1; i < priGrelhaDocs.Grelha.DataRowCnt; i++)
            {
                NumLinha = priGrelhaDocs.GetGRID_GetValorCelula(i, "N.L");
                Artigo = priGrelhaDocs.GetGRID_GetValorCelula(i, "Artigo");
                CDU_LoteAux = priGrelhaDocs.GetGRID_GetValorCelula(i, "Lote");
                Descricao = priGrelhaDocs.GetGRID_GetValorCelula(i, "Descrição");
                CDU_NomeCientifico = priGrelhaDocs.GetGRID_GetValorCelula(i, "Nome Científico");
                CDU_Caixas = priGrelhaDocs.GetGRID_GetValorCelula(i, "Cx.");
                CDU_KilosPorCaixa = priGrelhaDocs.GetGRID_GetValorCelula(i, "Kg/Cx.");
                Quantidade = priGrelhaDocs.GetGRID_GetValorCelula(i, "Quant.");
                Unidade = priGrelhaDocs.GetGRID_GetValorCelula(i, "Un.");
                CDU_Fornecedor = priGrelhaDocs.GetGRID_GetValorCelula(i, "Forn.");
                CDU_Origem = priGrelhaDocs.GetGRID_GetValorCelula(i, "Origem");
                CDU_FormaObtencao = priGrelhaDocs.GetGRID_GetValorCelula(i, "Obtenção");
                CDU_ZonaFAO = priGrelhaDocs.GetGRID_GetValorCelula(i, "Zona FAO");
                Id = priGrelhaDocs.GetGRID_GetValorCelula(i, "Id");

                using (StdBEExecSql sql = new StdBEExecSql())
                {
                    try
                    {
                        sql.Tabela = "LinhasDoc";
                        sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                        sql.AddCampo("NumLinha", NumLinha);
                        sql.AddCampo("Artigo", Artigo);
                        sql.AddCampo("CDU_LoteAux", CDU_LoteAux);
                        sql.AddCampo("Descricao", Descricao);
                        sql.AddCampo("CDU_NomeCientifico", CDU_NomeCientifico);
                        sql.AddCampo("CDU_Caixas", CDU_Caixas);
                        sql.AddCampo("CDU_KilosPorCaixa", CDU_KilosPorCaixa);
                        sql.AddCampo("Quantidade", Quantidade);
                        sql.AddCampo("Unidade", Unidade);
                        sql.AddCampo("CDU_Fornecedor", CDU_Fornecedor);
                        sql.AddCampo("CDU_Origem", CDU_Origem);
                        sql.AddCampo("CDU_FormaObtencao", CDU_FormaObtencao);
                        sql.AddCampo("CDU_ZonaFAO", CDU_ZonaFAO);
                        sql.AddCampo("Id", Id, true);

                        _PSO.ExecSql.Executa(sql);
                    }
                    catch (Exception ex)
                    {
                        _PSO.MensagensDialogos.MostraErro($"Não foi possivel gravar a linha {NumLinha} - {Artigo} com o ID {Id}");
                    }
                }


            }
        }

        private void priGrelhaDocs_Load(object sender, EventArgs e)
        {

        }
    }
}
