﻿using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasBE100;
using System.Threading.Tasks;
using DCT_Extens.Helpers;
using ErpBS100;
using StdPlatBS100;
using PRISDK100;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using StdBE100;

namespace DCT_Extens.Forms
{
    public partial class FormReimpressao : CustomForm
    {
        private ErpBS _BSO { get; set; }
        private StdPlatBS _PSO { get; set; }
        private clsSDKContexto _SDKContexto { get; set; }
        private HelperFunctions _Helpers { get; set; }

        private bool _mapasVazio;

        public FormReimpressao()
        {
            _BSO = PriMotores.Motor;
            _PSO = PriMotores.Plataforma;
            _SDKContexto = PriMotores.PriSDKContexto;
            _Helpers = new HelperFunctions();

            InitializeComponent();
        }

        private void FormReimpressao_Load(object sender, EventArgs e)
        {
            // Inicializar controlos Primavera
            priGrelha_Docs.Inicializa(_SDKContexto);
            f4_Cliente.Inicializa(_SDKContexto);

            // Inicialização dos controlos
            InicializaPriGrelha();
            InicializaListBox_TipoDoc();
            InicializaCmbBox_Mapas();
            
            dtPicker_DataDocInicial.Value = DateTime.Now;
            dtPicker_DataDocFinal.Value = DateTime.Now;
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            InicializaPriGrelha();
        }

        private void cmbBox_Mapas_TextChanged(object sender, EventArgs e)
        {
            ActualizaCBoxSerie();
        }


        #region Funções de Inicialização
        private void InicializaPriGrelha()
        {
            priGrelha_Docs.TituloGrelha = "DocsReimpressao";
            priGrelha_Docs.PermiteActualizar = true;
            priGrelha_Docs.PermiteAgrupamentosUser = true;
            priGrelha_Docs.PermiteScrollBars = true;
            priGrelha_Docs.PermiteVistas = false;
            priGrelha_Docs.PermiteEdicao = false;
            priGrelha_Docs.PermiteDataFill = false;
            priGrelha_Docs.PermiteFiltros = true;
            priGrelha_Docs.PermiteActiveBar = false;
            priGrelha_Docs.PermiteContextoVazia = false;
            priGrelha_Docs.PermiteOrdenacao = true;
            
            // Colunas da tabela de reimpressão
            // Cf - CheckBox - defines whether it will print or not
            // Data - Date - document issuance date
            // Doc (DrillDown) - Str - DocType
            // Serie - Str
            // Numero (DrillDown) - Long/Int
            // Tipo Entidade - Str
            // Entidade (DrillDown) - Str
            // Moeda - Str/Currency
            // Total - Double/Float - total value of the Doc
            // Imp - Symbol - whether it has been printed or not
            
            priGrelha_Docs.AddColKey(strColKey: "Cf", intTipo: 10, strTitulo: "Cf.", dblLargura: 3, blnMostraSempre: true, blnVisivel: true);
            priGrelha_Docs.AddColKey(strColKey: "Data", intTipo: 5, strTitulo: "Data", dblLargura: 13, strCamposBaseDados: "Data", blnMostraSempre: true);
            priGrelha_Docs.AddColKey(strColKey: "TipoDoc", intTipo: 5, strTitulo: "Doc", dblLargura: 7, strCamposBaseDados: "TipoDoc", blnDrillDown: true, blnMostraSempre: true);
            priGrelha_Docs.AddColKey(strColKey: "Serie", intTipo: 5, strTitulo: "Serie", dblLargura: 7, strCamposBaseDados: "Serie", blnMostraSempre: true);
            priGrelha_Docs.AddColKey(strColKey: "NumDoc", intTipo: 5, strTitulo: "Numero", dblLargura: 10, strCamposBaseDados: "NumDoc", blnDrillDown: true, blnMostraSempre: true);
            priGrelha_Docs.AddColKey(strColKey: "TipoEntidade", intTipo: 5, strTitulo: "Tipo Entidade", dblLargura: 5, strCamposBaseDados: "TipoEntidade", blnMostraSempre: true);
            priGrelha_Docs.AddColKey(strColKey: "Entidade", intTipo: 5, strTitulo: "Entidade", dblLargura: 10, strCamposBaseDados: "Entidade", blnDrillDown: true, blnMostraSempre: true);
            priGrelha_Docs.AddColKey(strColKey: "Moeda", intTipo: 5, strTitulo: "Moeda", dblLargura: 7, strCamposBaseDados: "Moeda", blnDrillDown: true, blnMostraSempre: true);
            priGrelha_Docs.AddColKey(strColKey: "TotalDocumento", intTipo: 2, strTitulo: "Total", dblLargura: 10, strCamposBaseDados: "TotalDocumento", blnMostraSempre: true);

        }
        private void InicializaListBox_TipoDoc()
        {
            listBox_TipoDoc.Items.Clear();

            DataTable tipoDocsTabela = _Helpers.GetDataTableDeSQL($"" +
                $" SELECT Documento + ' - ' + Descricao AS Doc" +
                $" FROM DocumentosVenda" +
                $" WHERE Inactivo = 0" +
                $"  AND CDU_ModReimpDocs = 1" +
                $" ORDER BY Documento DESC;");

            // Get coluna Doc como List e depois converte pra Array ao introduzir na ListBox (não aceita List)
            // Mais eficiente que loop pela Lista toda.
            // As razões são interessantes (se bem que em datasets pequenos podem não fazer grande diferença.
            // https://pastebin.com/T6CXVXtE
            var tipoDocsValores = tipoDocsTabela.AsEnumerable()
                .Select(linha => linha.Field<string>("Docs")).ToList();

            listBox_TipoDoc.Items.AddRange(tipoDocsValores.ToArray());
        }
        private void InicializaCmbBox_Mapas()
        {
            DataTable mapasTabela = _Helpers.GetDataTableDeSQL(
                " SELECT Descricao " +
                " FROM [PRIEMPRE].[dbo].[Mapas] " +
                " WHERE " +
                "   Categoria = 'DocVenda' " +
                "   AND Apl = 'GCP' " +
                "   AND Custom = 1 " +
                "   ORDER BY Descricao;");

            var tipoDocsLista = mapasTabela.AsEnumerable()
                .Select(linha => linha.Field<string>("Descricao")).ToList();

            if (!tipoDocsLista.Any())
            {
                PSO.MensagensDialogos.MostraErro("Não foi possivel encontrar mapas personalizados.");
                Close();
            }
            else
            {
                cmbBox_Mapas.Items.Clear();
                cmbBox_Mapas.Items.AddRange(tipoDocsLista.ToArray());
            }


        }
        #endregion

        #region Funções de Actualização
        private void ActualizaPriGrelha()
        {
            DateTime dataInicial = dtPicker_DataDocInicial.Value;
            DateTime dataFinal = dtPicker_DataDocFinal.Value;

            if (listBox_TipoDoc.SelectedItems.Count.Equals(0))
            {
                PSO.MensagensDialogos.MostraAviso("Não foi seleccionado um tipo de documento.", StdBSTipos.IconId.PRI_Exclama);
                return;
            }

            // Lista de strings com os valores seleccionados na ListBox para a query SQL. Fica apenas com o TipoDoc sem o hífen e descrição
            List<string> tipoDocSelec = listBox_TipoDoc.SelectedItems
                .Cast<string>()
                .Select(x => x.Split(' ')[0].Trim() + ",")
                .ToList();

            // Remoção da virgula do último TipoDoc na lista para evitar erro de SQL e criação da string final
            int ultimoIndice = tipoDocSelec.Count - 1;
            tipoDocSelec[ultimoIndice] = tipoDocSelec[ultimoIndice].Replace(",", string.Empty);
            string tipoDocString = string.Concat(tipoDocSelec);

            // *** SQL QUERY ***
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" SELECT NULL AS Cf, CONVERT(varchar, Data, 103) AS Data, TipoDoc, Serie, NumDoc, TipoEntidade, Entidade, Moeda, TotalDocumento");
            queryBuilder.Append(" FROM CabecDoc");
            queryBuilder.Append($" WHERE Data BETWEEN CONVERT(datetime, '{dataInicial}', 103) AND CONVERT(datetime, '{dataFinal}', 103)");
            queryBuilder.Append($" AND TipoDoc IN ({tipoDocString})");
            // Se f4_Cliente estiver nulo, busca todos os docs.
            if (string.IsNullOrWhiteSpace(f4_Cliente.Text)) { queryBuilder.Append($" AND Entidade = {f4_Cliente.Text}"); }
            queryBuilder.Append(" ORDER BY TipoDoc, NumDoc DESC;");

            // Preencher PriGrelha com resultados da query. DataBind deve ser feito a uma StdBELista (retornado por BSO.Consulta)
            priGrelha_Docs.LimpaGrelha();
            priGrelha_Docs.DataBind(BSO.Consulta(queryBuilder.ToString()));
        }

        #endregion

    }
}