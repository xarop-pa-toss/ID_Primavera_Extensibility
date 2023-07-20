using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks;
using PRISDK100; using StdBE100; using StdPlatBS100; using VndBE100;

namespace PP_PPCS
{
    public partial class FormImportaDocs : CustomForm
    {
        public FormImportaDocs()
        {
            InitializeComponent();
        }

        string _tabela;
        StdBELista _RSt = new StdBELista();

        private void FormImportaDocs_Load(object sender, EventArgs e)
        {
            SdkPrimavera.InicializaContexto(BSO, PSO);
            prigrelha_Docs.Inicializa(SdkPrimavera.ContextoSDK);

            _tabela = "#A";
            datepicker_DataDocImport.Value = DateTime.Now;
            datepicker_DataDocNew.Value = DateTime.Now;

            InicializaPrigrelhaDocs(DateTime.Now.Date);
        }

        private void InicializaPrigrelhaDocs(DateTime dataImport)
        {
            prigrelha_Docs.TituloGrelha = "Documentos do dia: " + dataImport.ToString();
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
    

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
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

        public static SdkPrimavera InicializaContexto(dynamic BSO, dynamic PSO)
        {
            contextosdk = new PRISDK100.clsSDKContexto();
            contextosdk.Inicializa(BSO, "ERP");
            PSO.InicializaPlataforma(contextosdk);

            return contexto;
        }
    }
}
