using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRISDK100;
using StdBE100; using BasBE100; using StdPlatBS100; using ConstantesPrimavera100;


namespace FRU_AlterarTerceiros
{

    public partial class FormAlterarTerceiros : CustomForm
    {
        //Variavél global que contem o contexto e que deverá ser passada para os controlos.
        public clsSDKContexto _sdkContexto;
        private string _tipoDoc, _serie;
        private long _numDoc;

        public FormAlterarTerceiros()
        {
            InitializeComponent();
        }

        // LOAD
        private void FormAlterarTerceiros_Load(object sender, EventArgs e)
        {
            InicializaSDKContexto();
            f4TipoTerceiro.Inicializa(_sdkContexto);
            f4TipoDoc.Inicializa(_sdkContexto);

            date_DataDocInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            date_DataDocFim.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
            f4TipoDoc.Text = "FR";
        }
        
        private void priGrelhaDocs_Load(object sender, EventArgs e)
        { 
            priGrelhaDocs.Inicializa(_sdkContexto);
            InicializapriGrelhaDocs();
        }


        // INICIALIZAÇÕES
        private void InicializaSDKContexto()
        {
            if (_sdkContexto == null)
            {
                _sdkContexto = new clsSDKContexto();
                //Inicializaçao do contexto SDK a partir do objeto BSO e respetivo módulo.
                _sdkContexto.Inicializa(BSO, "ERP");
                //Inicialização da plataforma no contexto e verificação de assinatura digital.
                PSO.InicializaPlataforma(_sdkContexto);
            }
        }
        
        private void InicializapriGrelhaDocs()
        {
            priGrelhaDocs.Inicializa(_sdkContexto);
            priGrelhaDocs.TituloGrelha = "DocsReimpressao";
            priGrelhaDocs.PermiteActualizar = true;
            priGrelhaDocs.PermiteAgrupamentosUser = true;
            priGrelhaDocs.PermiteScrollBars = true;
            priGrelhaDocs.PermiteVistas = false;
            priGrelhaDocs.PermiteEdicao = false;
            priGrelhaDocs.PermiteDataFill = false;
            priGrelhaDocs.PermiteFiltros = false;
            priGrelhaDocs.PermiteActiveBar = false;
            priGrelhaDocs.PermiteContextoVazia = false;

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

            priGrelhaDocs.AddColKey("Cf", 10, "Cf", 10, blnMostraSempre: true, blnVisivel: true);
            priGrelhaDocs.AddColKey("Data", 5, strTitulo: "Data", dblLargura: 15, strCamposBaseDados: "Data", blnMostraSempre: true);
            priGrelhaDocs.AddColKey("TipoDoc", 5, strTitulo: "Doc", dblLargura: 5, strCamposBaseDados: "TipoDoc", blnDrillDown: true, blnMostraSempre: true);
            priGrelhaDocs.AddColKey("Serie", 5, strTitulo: "Serie", dblLargura: 5, strCamposBaseDados: "Serie", blnMostraSempre: true);
            priGrelhaDocs.AddColKey("NumDoc", 5, strTitulo: "Numero", dblLargura: 8, strCamposBaseDados: "NumDoc", blnDrillDown: true, blnMostraSempre: true);
            priGrelhaDocs.AddColKey("TotalDocumento", 2, strTitulo: "Total", dblLargura: 8, strCamposBaseDados: "TotalDocumento", blnMostraSempre: true);
        }


        // BOTÕES
        private void btnAlterarTerceiro_Click(object sender, EventArgs e)
        {
            // Check linhas da priGrelha
            var grelha = priGrelhaDocs.Grelha;

            for (int i = 1; i <= priGrelhaDocs.) 
            Dictionary<string, string> valoresControlos = GetControlos();

            if (CheckControlos(valoresControlos)) {
                using (StdBE100.StdBEExecSql sql = new StdBE100.StdBEExecSql()) {
                    sql.tpQuery = StdBE100.StdBETipos.EnumTpQuery.tpUPDATE;
                    sql.Tabela = "CabecDoc";                                                                                                                // UPDATE CabecDoc
                    sql.AddCampo("TipoTerceiro", valoresControlos["Terceiro"]);                                                                             // SET TipoTerceiro = ...
                    sql.AddCampo("Tipodoc", valoresControlos["TipoDoc"], true, StdBE100.StdBETipos.EnumTipoCampoSimplificado.tsTexto);                      // WHERE TipoDoc = ...
                    sql.AddCampo("Serie", valoresControlos["Serie"], true, StdBE100.StdBETipos.EnumTipoCampoSimplificado.tsTexto);                          // AND ...
                    sql.AddCampo("NumDoc", Convert.ToInt32(valoresControlos["NumDoc"]), true, StdBE100.StdBETipos.EnumTipoCampoSimplificado.tsInteiro);     // AND ...

                    sql.AddQuery();
                    PSO.ExecSql.Executa(sql);
                    sql.Dispose();
                }
            }

            //É necessário criar código no Primavera V10 que está Frupor para a empresa ADEGA para fazer o seguinte, poder alterar o tipo de terceiro nos documentos de venda. Para tal é necessário o utilizador introduzir os seguintes campos:
            //-Tipo de documento
            //-Série do documento
            //- Nº de documento
            //Depois poder colocar o tipo de terceiro em tabela.

            // update cabecdoc
            // set tipoterceiro =´005´
            //where tipodoc =´fr´ and serie =´t0123´ and entidade =´mn9998´
        }

        private void btnActualizarPriGrelha_Click(object sender, EventArgs e)
        {
            // Valores dos controlos
            int
                numDocInicio = (int)num_NumDocInicio.Value,
                numDocFim = (int)num_NumDocFim.Value;
            string
                dataInicio = date_DataDocInicio.Value.ToString(),
                dataFim = date_DataDocFim.Value.ToString(),
                tipoDoc = f4TipoDoc.Text;

            if (tipoDoc.Equals(null)) {
                PSO.Dialogos.MostraAviso("Tipo de Documento está vazio.", StdBSTipos.IconId.PRI_Exclama);
                return;
            }

            // QUERY SQL
            Dictionary<string, string> sqlDict = new Dictionary<string, string>();

            // Criar cada parte da query
            // A coluna Cf recebe NULL pq a Prigrelha estava a dar problemas se a query não tivesse exactamente a mesma quantidade de colunas que a grelha em si
            sqlDict.Add("select", "SELECT NULL AS Cf, Convert(varchar, Data, 103) AS Data, TipoDoc, Serie, NumDoc, TipoEntidade, Entidade, Moeda, TotalDocumento");
            sqlDict.Add("from", "FROM CabecDoc WHERE");
            sqlDict.Add("whereData", "Data BETWEEN CONVERT(datetime, '" + dataInicio + "', 103) AND CONVERT(datetime, '" + dataFim + "', 103)");
            sqlDict.Add("whereTipoDoc", "AND TipoDoc IN (" + tipoDoc + ")");
            sqlDict.Add("whereNumDoc", "AND (NumDoc >= " + numDocInicio + " AND NumDoc <= " + numDocFim + ")");
            sqlDict.Add("order", "ORDER BY TipoDoc, NumDoc DESC;");

            string sqlCommand = String.Join(" ", sqlDict);


            // PriGrelha Databind e execute da query
            var rcSet = _sdkContexto.BSO.Consulta(sqlCommand);
            priGrelhaDocs.LimpaGrelha();
            priGrelhaDocs.DataBind(rcSet);
        }

        private void f4TipoDoc_TextChange(object Sender, F4.TextChangeEventArgs e)
        {
            if (f4TipoDoc.Text != "") {
                string query = "SELECT DISTINCT Serie FROM SeriesVendas WHERE TipoDoc = '" + f4TipoDoc.Text + "' ORDER BY Serie DESC;";
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

            valoresControlos.Add("TipoDoc", f4TipoDoc.Text);
            valoresControlos.Add("Terceiro",f4TipoTerceiro.Text);
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
}
