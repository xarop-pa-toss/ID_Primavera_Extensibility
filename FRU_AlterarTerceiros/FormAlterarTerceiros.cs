﻿using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRISDK100; 
using StdBE100; using StdPlatBS100;


namespace FRU_AlterarTerceiros
{
    // MAIN
    public partial class FormAlterarTerceiros : CustomForm
    {
        //Variavél global que contem o contexto e que deverá ser passada para os controlos.
        public clsSDKContexto _sdkContexto;

        public FormAlterarTerceiros()
        {
            InitializeComponent();
        }

        // LOAD e Inicialização
        private void FormAlterarTerceiros_Load(object sender, EventArgs e)
        {
            //if (_sdkContexto == null) {
            //    _sdkContexto = new clsSDKContexto();
            //    //Inicializaçao do contexto SDK a partir do objeto BSO e respetivo módulo.
            //    _sdkContexto.InicializaPlataforma(_plat);
            //    _sdkContexto.Inicializa(BSO, "ERP");
            //    //Inicialização da plataforma no contexto e verificação de assinatura digital.
            //    PSO.InicializaPlataforma(_sdkContexto);
            //}

            //SdkPrimavera.InicializaContexto(BSO, PSO);

            //f4_TipoTerceiro.Inicializa(SdkPrimavera.ContextoSDK);
            //f4_TipoDoc.Inicializa(SdkPrimavera.ContextoSDK);
            //prigrelha_Docs.Inicializa(SdkPrimavera.ContextoSDK);

            datepicker_DataDocInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            datepicker_DataDocFim.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
            f4_TipoDoc.Text = "FR";

            InicializapriGrelhaDocs();
        }
        
        private void InicializapriGrelhaDocs()
        {
            prigrelha_Docs.TituloGrelha = "DocsReimpressao";
            prigrelha_Docs.PermiteActualizar = true;
            prigrelha_Docs.PermiteAgrupamentosUser = true;
            prigrelha_Docs.PermiteScrollBars = true;
            prigrelha_Docs.PermiteVistas = false;
            prigrelha_Docs.PermiteEdicao = false;
            prigrelha_Docs.PermiteDataFill = false;
            prigrelha_Docs.PermiteFiltros = false;
            prigrelha_Docs.PermiteActiveBar = false;
            prigrelha_Docs.PermiteContextoVazia = false;

            //    ' Colunas da tabela de reimpressão
            //' Cf - CheckBox - define se vai imprimir ou não
            //' Data - Date - data de emissão do documento
            //' Doc (DrillDown) - Str - TipoDoc
            //' Série - Str
            //' Numero (DrillDown) - Long/Int

            //'Private Enum ColType
            //'SS_CELL_TYPE_DATE = 0
            //'SS_CELL_TYPE_EDIT = 1
            //'SS_CELL_TYPE_FLOAT = 2
            //'SS_CELL_TYPE_INTEGER = 3
            //'SS_CELL_TYPE_PIC = 4
            //'SS_CELL_TYPE_STATIC_TEXT = 5
            //'SS_CELL_TYPE_TIME = 6
            //'SS_CELL_TYPE_BUTTON = 7
            //'SS_CELL_TYPE_COMBOBOX = 8
            //'SS_CELL_TYPE_PICTURE = 9
            //'SS_CELL_TYPE_CHECKBOX = 10
            //'SS_CELL_TYPE_OWNER_DRAWN = 11
            //'End Enum

            prigrelha_Docs.AddColKey("Cf", 10, "Cf", dblLargura: 5, blnMostraSempre: true, blnVisivel: true);
            prigrelha_Docs.AddColKey("Data", 5, strTitulo: "Data", dblLargura: 15, strCamposBaseDados: "Data", blnMostraSempre: true);
            prigrelha_Docs.AddColKey("TipoDoc", 5, strTitulo: "Doc", dblLargura: 5, strCamposBaseDados: "TipoDoc", blnDrillDown: true, blnMostraSempre: true);
            prigrelha_Docs.AddColKey("Serie", 5, strTitulo: "Serie", dblLargura: 5, strCamposBaseDados: "Serie", blnMostraSempre: true);
            prigrelha_Docs.AddColKey("NumDoc", 5, strTitulo: "Numero", dblLargura: 8, strCamposBaseDados: "NumDoc", blnDrillDown: true, blnMostraSempre: true);
            prigrelha_Docs.AddColKey("TipoTerceiro", 2, strTitulo: "Tipo Terceiro", dblLargura: 10, strCamposBaseDados: "TotalDocumento", blnMostraSempre: true);
            prigrelha_Docs.AddColKey("TotalDocumento", 2, strTitulo: "Total", dblLargura: 8, strCamposBaseDados: "TotalDocumento", blnMostraSempre: true);
        }


        // BOTÕES
        private void btnActualizarPriGrelha_Click(object sender, EventArgs e)
        {
            prigrelha_Docs.LimpaGrelha();

            // Valores dos controlos
            int
                numDocInicio = (int)num_NumDocInicio.Value,
                numDocFim = (int)num_NumDocFim.Value;
            string
                dataInicio = datepicker_DataDocInicio.Value.ToString(),
                dataFim = datepicker_DataDocFim.Value.ToString(),
                tipoDoc = f4_TipoDoc.Text;

            if (tipoDoc.Equals(null)) {
                return;
            }

            // QUERY SQL
            Dictionary<string, string> sqlDict = new Dictionary<string, string>();

            // Criar cada parte da query
            // A coluna Cf recebe NULL pq a Prigrelha estava a dar problemas se a query não tivesse exactamente a mesma quantidade de colunas que a grelha em si
            sqlDict.Add("select", "SELECT NULL AS Cf, Data, TipoDoc, Serie, NumDoc, TipoTerceiro, TotalDocumento");
            sqlDict.Add("from", "FROM CabecDoc");
            sqlDict.Add("whereData", "WHERE Data BETWEEN CONVERT(datetime, '" + dataInicio + "', 103) AND CONVERT(datetime, '" + dataFim + "', 103)");
            sqlDict.Add("whereTipoDoc", "AND TipoDoc = '" + tipoDoc + "'");
            sqlDict.Add("whereNumDoc", "AND (NumDoc >= " + numDocInicio + " AND NumDoc <= " + numDocFim + ")");
            sqlDict.Add("order", "ORDER BY TipoDoc, NumDoc DESC;");

            string sqlCommand = String.Join(" ", sqlDict.Values);

            // Preenchimento da Prigrelha com a query acima
            StdBELista rcSet = BSO.Consulta(sqlCommand);
            prigrelha_Docs.LimpaGrelha();

            if (!rcSet.Vazia()) {
                prigrelha_Docs.DataBind(rcSet);
            }
        }

        private void btnAlterarTerceiro_Click(object sender, EventArgs e)
        {
            string gTipoDoc, gSerie, gNumDoc;
            Dictionary<string, string> valoresControlos = GetControlos();
            List<string> docsComErroNoUpdateSQL = new List<string>();

            if (!CheckControlos(valoresControlos)) { return; }

            for (int i = 1; i <= prigrelha_Docs.Grelha.DataRowCnt; i++) {

                // Salta linha se checkbox não activada. GetGRID_GetValorCelula devolve sempre dynamic, um tipo de dados verificado em runtime.
                // Por essa razão, não é possivel "assumir" o tipo de dados.
                // Checkboxes nas Prigrelhas devolvem sempre int (1 e 0) e devem ser verificadas como tal, e não como bool como faz sentido.
                if (!object.Equals(prigrelha_Docs.GetGRID_GetValorCelula(i, "Cf"), 1)) {
                    continue;
                }

                gTipoDoc = prigrelha_Docs.GetGRID_GetValorCelula(i, "TipoDoc");
                gSerie = prigrelha_Docs.GetGRID_GetValorCelula(i, "Serie");
                gNumDoc = prigrelha_Docs.GetGRID_GetValorCelula(i, "NumDoc");

                using (StdBEExecSql sql = new StdBEExecSql()) {
                    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                    sql.Tabela = "CabecDoc";                                                                                    // UPDATE CabecDoc
                    sql.AddCampo("TipoTerceiro", valoresControlos["Terceiro"]);                                                 // SET TipoTerceiro = ...
                    sql.AddCampo("Tipodoc", gTipoDoc, true, StdBETipos.EnumTipoCampoSimplificado.tsTexto);                      // WHERE TipoDoc = ...
                    sql.AddCampo("Serie", gSerie, true, StdBETipos.EnumTipoCampoSimplificado.tsTexto);                          // AND ...
                    sql.AddCampo("NumDoc", Convert.ToInt32(gNumDoc), true, StdBETipos.EnumTipoCampoSimplificado.tsInteiro);     // AND ...

                    sql.AddQuery();
                    
                    // Se Update falhar, preenche lista com NumDoc para mostrar ao cliente.
                    try {
                        PSO.ExecSql.Executa(sql);
                    } 
                    catch {
                        docsComErroNoUpdateSQL.Add(gNumDoc);
                    }
                }
            }
            if (docsComErroNoUpdateSQL.Count != 0) {
                PSO.MensagensDialogos.MostraAviso("Não foi possivel alterar o Tipo Terceiro em alguns documentos!", StdBSTipos.IconId.PRI_Exclama, String.Join(", ", docsComErroNoUpdateSQL));
            }
            else {
                PSO.MensagensDialogos.MostraAviso("Todos os documentos alterados com sucesso.", StdBSTipos.IconId.PRI_Informativo);
            }
        }

            //É necessário criar código no Primavera V10 que está Frupor para a empresa ADEGA para fazer o seguinte, poder alterar o tipo de terceiro nos documentos de venda. Para tal é necessário o utilizador introduzir os seguintes campos:
            //- Tipo de documento
            //- Série do documento
            //- Nº de documento
            //Depois poder colocar o tipo de terceiro em tabela.


        private void f4TipoDoc_TextChange(object Sender, F4.TextChangeEventArgs e)
        {
            // Get Serie do TipoDoc para a ComboBox de Série
            if (f4_TipoDoc.Text != "") {
                string query = "SELECT DISTINCT Serie FROM SeriesVendas WHERE TipoDoc = '" + f4_TipoDoc.Text + "' ORDER BY Serie DESC;";
                cbox_Serie.Items.Clear();
                cbox_Serie.Items.AddRange(FillComboBox(query).ToArray());
                cbox_Serie.SelectedIndex = 0;
            }
        }

        // Retorna null se query vazia.
        private List<string> FillComboBox(string query)
        {
            using(StdBE100.StdBELista priLista = BSO.Consulta(query))
            {
                List<string> listaFinal = new List<string>();

                if (!priLista.Vazia())
                {
                    priLista.Inicio();
                    while (!priLista.NoFim())
                    {
                        listaFinal.Add(priLista.Valor(0));
                        priLista.Seguinte();
                    }
                    priLista.Termina();

                    return listaFinal;
                }
                return null;
            }
        }

        private Dictionary<string, string> GetControlos()
        {
            Dictionary<string, string> valoresControlos = new Dictionary<string, string>();

            valoresControlos.Add("TipoDoc", f4_TipoDoc.Text);
            valoresControlos.Add("Terceiro",f4_TipoTerceiro.Text);
            valoresControlos.Add("Serie", cbox_Serie.Text);
            valoresControlos.Add("NumDoc", num_NumDocInicio.Text);

            return valoresControlos;
        }

        private bool CheckControlos(Dictionary<string, string> valoresControlos)
        {
            if (valoresControlos.Values.Any(value => value == null)) { 
                System.Windows.Forms.MessageBox.Show("Dados insuficientes. Verifique se existem campos vazios.");
                return false;
            }
            return true;
        }
    }

    // INICIALIZA CONTEXTO PARA CONTROLOS SDK
    internal class SdkPrimavera
    {
        private static readonly SdkPrimavera contexto = new SdkPrimavera();
        private static PRISDK100.clsSDKContexto contextosdk;

        public static PRISDK100.clsSDKContexto ContextoSDK
        {
            get
            {
                return contextosdk;
            }
        }

        private SdkPrimavera()
        {
        }

        internal static SdkPrimavera InicializaContexto(dynamic BSO, dynamic PSO)
        {
            contextosdk = new PRISDK100.clsSDKContexto();
            contextosdk.Inicializa(BSO, "ERP");
            PSO.InicializaPlataforma(contextosdk);

            return contexto;
        }
    }
}
