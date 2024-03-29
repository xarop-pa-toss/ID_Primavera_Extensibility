﻿using BasBE100;
using ErpBS100;
using HelpersPrimavera10;
using Primavera.Extensibility.CustomForm;
using PRISDK100;
using StdBE100;
using StdPlatBS100;
using System;
using System.Windows.Forms;

namespace DCT_Extens
{
    public partial class FormCargaDescarga : CustomForm
    {
        private ErpBS _BSO { get; set; }
        private StdBSInterfPub _PSO { get; set; }
        private clsSDKContexto _SDKContexto { get; set; }
        private HelperFunctions _Helpers = new HelperFunctions();

        public bool NaoGravar { get; set; }
        public bool MoradaAlterada, GravarComMoradaPorDefeito;
        public BasBECargaDescarga cdForm { get; private set; }


        public FormCargaDescarga()
        {
            InitializeComponent();

            this.Text = "Alteração de morada de Descarga"; 
        }

        private void FormCargaDescarga_Load(object sender, EventArgs e)
        {
            _BSO = PriMotores.Motor;
            _PSO = PriMotores.Plataforma;
            _SDKContexto = PriMotores.PriSDKContexto;

            //_SDKContexto = new clsSDKContexto();
            //_SDKContexto.Inicializa(BSO, "ERP");
            //_PSO.InterfacePublico.InicializaPlataforma(contexto);

            priGrelha_Moradas.Inicializa(_SDKContexto);
            f4_Entidade.Inicializa(_SDKContexto);

            InicializaPrigrelha();
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            ActualizaPriGrelha();
        }

        private void f4_Entidade_Leave(object sender, EventArgs e)
        {
            ActualizaPriGrelha();
        }

        private void btn_Confirmar_Click(object sender, EventArgs e)
        {
            NaoGravar = false; MoradaAlterada = false; GravarComMoradaPorDefeito = false;

            // Inicializar objecto CargaDescarga público que será populado com linha escolhida na tabela.
            // Este objecto é então usado no EditorVendas_AntesDeGravar para popular a CargaDescarga do DocVenda
            cdForm = new BasBECargaDescarga();
            bool linhaSeleccionada = false;

            // Indice da primeira linha que tenha Cf = true;
            for (int i = 1; i <= priGrelha_Moradas.Grelha.DataRowCnt; i++)
            {
                if (priGrelha_Moradas.GetGRID_GetValorCelula(i, "Cf") == 1)
                {
                    linhaSeleccionada = true;
                    // Dados de Entrega
                    cdForm.MoradaEntrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "Morada");
                    cdForm.Morada2Entrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "Morada2");
                    cdForm.LocalidadeEntrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "Localidade");
                    cdForm.CodPostalEntrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "CodigoPostal");
                    cdForm.CodPostalLocalidadeEntrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "LocalidadePostal");
                    cdForm.DistritoEntrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "Distrito");
                    cdForm.PaisEntrega = priGrelha_Moradas.GetGRID_GetValorCelula(i, "Pais");
                    cdForm.EntidadeEntrega = "13000";

                    break;
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
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => FormCargaDescarga_FormClosed(sender, e)));
            }
        }

        private void ActualizaPriGrelha()
        {
            // A coluna Cf recebe NULL pq a Prigrelha estava a dar problemas se a query não tivesse exactamente a mesma quantidade de colunas que a grelha em si
            // A primeira parte da query vai buscar a morada default, a segunda parte vai buscar todas as moradas alternativas
            string sql =
                "SELECT NULL Cf, 'Fac' AS Codigo, Fac_Mor, Fac_Mor2, Fac_Local, Fac_Cp CodigoPostal, Fac_Cploc LocalidadePostal, " +
                "clt.Pais, Paises.Descricao PaisDescricao, clt.Distrito, Distritos.Descricao DistritoDescricao " +
                "FROM Clientes AS clt " +
                "   LEFT JOIN Paises ON clt.pais = Paises.pais " +
                "   LEFT JOIN Distritos ON clt.Distrito = Distritos.Distrito " +
                "WHERE Cliente = '" + f4_Entidade.Text + "'" +
                "UNION " +
                "SELECT NULL As Cf, MoradaAlternativa AS Codigo, Morada, Morada2, Localidade, Cp AS CodigoPostal, CpLocalidade AS LocalidadePostal, " +
                "mac.Pais, Paises.Descricao AS PaisDescricao, mac.Distrito, Distritos.Descricao AS DistritoDescricao " +
                "FROM MoradasAlternativasClientes AS mac " +
                "   LEFT JOIN Paises ON mac.pais = Paises.pais " +
                "   LEFT JOIN Distritos ON mac.Distrito = Distritos.Distrito " +
                "WHERE Cliente = '" + f4_Entidade.Text + "'" +
                "ORDER BY Codigo";

            StdBELista resultadoList = BSO.Consulta(sql);
            if (!resultadoList.Vazia())
            {
                priGrelha_Moradas.DataBind(resultadoList);
            }
        }
    }
}