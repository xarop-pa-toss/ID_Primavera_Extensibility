using BasBE100;
using DCT_Extens.Helpers;
using ErpBS100;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using PRISDK100;
using StdPlatBS100;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT_Extens
{
    public partial class FormCargaDescarga : CustomForm
    {
        private ErpBS _BSO { get; set; }
        private StdPlatBS _PSO { get; set; }
        private clsSDKContexto _SDKContexto { get; set; }
        private HelperFunctions _Helpers { get; set; }
        
        public BasBECargaDescarga _cargaDescarga { get; set; }
        public bool NaoGravar { get; set; }
        public bool MoradaAlterada, GravarComMoradaPorDefeito;

        public FormCargaDescarga()
        {
            _BSO = PriMotores.Motor;
            _PSO = PriMotores.Plataforma;
            _SDKContexto = PriMotores.PriSDKContexto;
            _Helpers = new HelperFunctions();

            InitializeComponent();
        }

        private void FormCargaDescarga_Load(object sender, EventArgs e)
        {
            priGrelha_Moradas.Inicializa(_SDKContexto);
            f4_Entidade.Inicializa(_SDKContexto);

            InicializaPrigrelha();
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            // Vazio de propósito
        }

        private void btn_Confirmar_Click(object sender, EventArgs e)
        {
            NaoGravar = false; MoradaAlterada = false; GravarComMoradaPorDefeito = false;

            // Inicializar objecto CargaDescarga público que será populado com linha escolhida na tabela.
            // Este objecto é então usado no EditorVendas_AntesDeGravar para popular a CargaDescarga do DocVenda
            _cargaDescarga = new BasBECargaDescarga();
            bool linhaSeleccionada = false;

            // Indice da primeira linha que tenha Cf = true;
            for (int i = 1; i == priGrelha_Moradas.Grelha.DataRowCnt; i++)
            {
                if (priGrelha_Moradas.GetGRID_GetValorCelula(i, "Cf") == 1)
                {
                    linhaSeleccionada = true;
                    // Dados de Entrega
                    _cargaDescarga.MoradaEntrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "Morada");
                    _cargaDescarga.Morada2Entrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "Morada2");
                    _cargaDescarga.LocalidadeEntrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "Localidade");
                    _cargaDescarga.CodPostalEntrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "CodigoPostal");
                    _cargaDescarga.CodPostalLocalidadeEntrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "LocalidadePostal");
                    _cargaDescarga.DistritoEntrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "Distrito");
                    _cargaDescarga.PaisEntrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "Pais");

                    this.Close();
                }
            }
            // Mostra erro se não for escolhida nenhuma morada
            if (!linhaSeleccionada)
            {
                _PSO.MensagensDialogos.MostraErro("Nenhuma morada selecionada.", StdBSTipos.IconId.PRI_Critico);
            }
        }

        private void InicializaPrigrelha()
        {
            priGrelha_Moradas.TituloGrelha = "Moradas";
            priGrelha_Moradas.PermiteActualizar = true;
            priGrelha_Moradas.PermiteAgrupamentosUser = true;
            priGrelha_Moradas.PermiteScrollBars = true;
            priGrelha_Moradas.PermiteVistas = false;
            priGrelha_Moradas.PermiteEdicao = false;
            priGrelha_Moradas.PermiteDataFill = false;
            priGrelha_Moradas.PermiteFiltros = false;
            priGrelha_Moradas.PermiteActiveBar = false;
            priGrelha_Moradas.PermiteContextoVazia = false;

            priGrelha_Moradas.AddColKey(strColKey: "Cf", intTipo: 10, strTitulo: "Cf.", dblLargura: 5, strCamposBaseDados: "Cf", blnMostraSempre: true, blnVisivel: true);
            priGrelha_Moradas.AddColKey(strColKey: "Codigo", intTipo: 5, strTitulo: "Codigo", dblLargura: 10, strCamposBaseDados: "Codigo", blnMostraSempre: true, blnVisivel: true);
            priGrelha_Moradas.AddColKey(strColKey: "Morada", intTipo: 5, strTitulo: "Morada", dblLargura: 25, strCamposBaseDados: "Morada", blnMostraSempre: true);
            priGrelha_Moradas.AddColKey(strColKey: "Morada2", intTipo: 5, strTitulo: "Morada2", dblLargura: 25, strCamposBaseDados: "Morada2", blnDrillDown: true, blnMostraSempre: true);
            priGrelha_Moradas.AddColKey(strColKey: "Localidade", intTipo: 5, strTitulo: "Localidade", dblLargura: 15, strCamposBaseDados: "Localidade", blnMostraSempre: true);
            priGrelha_Moradas.AddColKey(strColKey: "CodigoPostal", intTipo: 5, strTitulo: "Código Postal", dblLargura: 10, strCamposBaseDados: "CodigoPostal", blnDrillDown: true, blnMostraSempre: true);
            priGrelha_Moradas.AddColKey(strColKey: "LocalidadePostal", intTipo: 5, strTitulo: "Localidade Postal", dblLargura: 15, strCamposBaseDados: "LocalidadePostal", blnMostraSempre: true);
            priGrelha_Moradas.AddColKey(strColKey: "Pais", intTipo: 5, strTitulo: "País", dblLargura: 5, strCamposBaseDados: "País", blnMostraSempre: true);
            priGrelha_Moradas.AddColKey(strColKey: "PaisDescricao", intTipo: 5, strTitulo: "Descrição", dblLargura: 10, strCamposBaseDados: "PaisDescricao", blnMostraSempre: true);
            priGrelha_Moradas.AddColKey(strColKey: "Distrito", intTipo: 5, strTitulo: "Distrito", dblLargura: 5, strCamposBaseDados: "Distrito", blnMostraSempre: true);
            priGrelha_Moradas.AddColKey(strColKey: "DistritoDescricao", intTipo: 5, strTitulo: "Descrição", dblLargura: 10, strCamposBaseDados: "DistritoDescricao", blnMostraSempre: true);
        }

        private void FormCargaDescarga_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {


        }
    }
}
