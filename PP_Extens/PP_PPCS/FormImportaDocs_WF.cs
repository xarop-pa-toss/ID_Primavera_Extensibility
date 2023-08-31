using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; using ErpBS100; using StdPlatBS100; using StdBE100; using VndBE100;

namespace PP_PPCS
{
    public partial class FormImportaDocs_WF : Form
    {
        private ErpBS _BSO;
        private StdPlatBS _PSO;
        private DateTime _dataOrigem, _dataDestino;
        private StdBELista _RSet;
        private string _tabela = "A" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.ToString("HHmmss").Replace(":", "");

        public FormImportaDocs_WF()
        {
            InitializeComponent();
        }

        private void FormImportaDocs_WF_Load(object sender, EventArgs e)
        {
            // ALTERAR AQUI AMBIENTE A USAR
            // 'teste' ou 'prod'
            string ambiente = "teste";
            
            new QueriesSQL(ambiente);

            Motor.PriEngine.CreateContext("PRIPPCS", "id", "*Pelicano*");
            _BSO = Motor.PriEngine.Engine;
            _PSO = Motor.PriEngine.Platform;
            //prigrelha_Docs_WF.Inicializa(Motor.PriEngine.PriSDKContexto);

            datepicker_DataDocImportar_WF.Value = DateTime.Now;
            datepicker_DataDocNovo_WF.Value = DateTime.Now;

            QueriesSQL.AbrirSQL();
            InicializarPriGrelhaDocs();
        }
        
        private void FormImportaDocs_WF_FormClosing(object sender, FormClosingEventArgs e)
        {
            QueriesSQL.FecharSQL();
        }


        //private void InicializarPriGrelhaDocs()
        //{
        //    prigrelha_Docs_WF.LimpaGrelha();
        //    prigrelha_Docs_WF.TituloGrelha = "Documentos do dia: " + _dataOrigem.ToString();
        //    prigrelha_Docs_WF.PermiteActualizar = true;
        //    prigrelha_Docs_WF.PermiteAgrupamentosUser = true;
        //    prigrelha_Docs_WF.PermiteScrollBars = true;
        //    prigrelha_Docs_WF.PermiteVistas = false;
        //    prigrelha_Docs_WF.PermiteFiltros = false;
        //    prigrelha_Docs_WF.PermiteActiveBar = false;
        //    prigrelha_Docs_WF.PermiteContextoVazia = false;

        //    //'Private Enum ColType
        //    //'SS_CELL_TYPE_DATE = 0
        //    //'SS_CELL_TYPE_EDIT = 1
        //    //'SS_CELL_TYPE_FLOAT = 2
        //    //'SS_CELL_TYPE_INTEGER = 3
        //    //'SS_CELL_TYPE_PIC = 4
        //    //'SS_CELL_TYPE_STATIC_TEXT = 5
        //    //'SS_CELL_TYPE_TIME = 6
        //    //'SS_CELL_TYPE_BUTTON = 7
        //    //'SS_CELL_TYPE_COMBOBOX = 8
        //    //'SS_CELL_TYPE_PICTURE = 9
        //    //'SS_CELL_TYPE_CHECKBOX = 10
        //    //'SS_CELL_TYPE_OWNER_DRAWN = 11
        //    //'End Enum


        //    prigrelha_Docs_WF.AddColKey("T.E", 5, "T.E", 4, true);
        //    prigrelha_Docs_WF.AddColKey("Entidade", 5, "Entidade", 10, true, blnDrillDown: true);
        //    prigrelha_Docs_WF.AddColKey("Filial", 5, "Filial", 6, true, blnVisivel: false);
        //    prigrelha_Docs_WF.AddColKey("T.Doc", 5, "T.Doc", 7, true);
        //    prigrelha_Docs_WF.AddColKey("Serie", 5, "Serie", 7, true);
        //    prigrelha_Docs_WF.AddColKey("N.Doc", 5, "N.Doc", 7, true);
        //    prigrelha_Docs_WF.AddColKey("Ent.Loc", 5, "Ent.Loc.", 7, true);
        //    prigrelha_Docs_WF.AddColKey("Fil.Loc", 5, "Fil.Loc.", 7, true, blnVisivel: false);
        //    prigrelha_Docs_WF.AddColKey("T.Doc.Loc", 5, "T.Doc.Loc", 8, true);
        //    prigrelha_Docs_WF.AddColKey("Serie Loc.", 5, "Serie Loc.", 8, true);
        //    prigrelha_Docs_WF.AddColKey("N.Doc.Loc", 5, "N.Doc.Loc", 8, true);
        //    prigrelha_Docs_WF.AddColKey("Data Loc.", 5, "Data Loc.", 9, true);
        //    prigrelha_Docs_WF.AddColKey("Importar", 5, "Importar", 7, false);


        //    //using (StdBEExecSql sql = new StdBEExecSql()) {
        //    //    sql.tpQuery = StdBETipos.EnumTpQuery.tpDELETE;
        //    //    sql.Tabela = "CabecDoc";                                                                                    // UPDATE CabecDoc
        //    //    sql.AddCampo("TipoTerceiro", gTipoTerceiro);                                                                // SET TipoTerceiro = ...
        //    //    sql.AddCampo("Tipodoc", gTipoDoc, true, StdBETipos.EnumTipoCampoSimplificado.tsTexto);                      // WHERE TipoDoc = ...
        //    //    sql.AddCampo("Serie", gSerie, true, StdBETipos.EnumTipoCampoSimplificado.tsTexto);                          // AND ...
        //    //    sql.AddCampo("NumDoc", Convert.ToInt32(gNumDoc), true, StdBETipos.EnumTipoCampoSimplificado.tsInteiro);     // AND ...

        //    //    sql.AddQuery();

        //    //    // Se Update falhar, preenche lista com NumDoc para mostrar ao cliente.
        //    //    try {
        //    //        _PSO.ExecSql.Executa(sql);
        //    //    }
        //    //    catch (Exception ex) {
        //    //        docsComErroNoUpdateSQL.Add(gNumDoc + ex.ToString());
        //    //    }
        //    //}
        //}

        //private void ActualizarPriGrelhaDocs(DateTime dataImport)
        //{
        //    prigrelha_Docs_WF.LimpaGrelha();            

        //    // Ver QueriesSQL.cs
        //    QueriesSQL.CreateTabela(_tabela, dataImport.ToString());
        //    QueriesSQL.InsertTabela(_tabela, dataImport.ToString());

        //    DataTable RSet = _BSO.ConsultaDataTable(QueriesSQL.GetQuery03(_tabela));
        //    //prigrelha_Docs_WF.DataBind(RSet);
        //    DataGrid1.DataSource = RSet;
        //    prigrelha_Docs_WF.PermiteEdicao = true;
        //    prigrelha_Docs_WF.PermiteDataFill = true;

        //    QueriesSQL.DropTabela(_tabela);
        //    QueriesSQL.FecharSQL();
        //}

        private void InicializarDataGrid(DateTime dataImport)
        {
            // Ver QueriesSQL.cs
            QueriesSQL.CreateTabela(_tabela, dataImport.ToString());
            QueriesSQL.InsertTabela(_tabela, dataImport.ToString());

            DataTable RSet = _BSO.ConsultaDataTable(QueriesSQL.GetQuery03(_tabela));
            
            DataGrid1.DataSource = RSet;

            DataGrid1.Columns[0].Name = "T.E.";
            DataGrid1.Columns[0].HeaderText = "T.E.";
            DataGrid1.Columns[0].Width = 24;
            DataGrid1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGrid1.Columns[0].ReadOnly = true;


            DataGrid1.Columns[1].Name = "Entidade";
            DataGrid1.Columns[1].HeaderText = "Entidade";
            DataGrid1.Columns[1].Width = 54;
            DataGrid1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGrid1.Columns[1].ReadOnly = true;

            DataGrid1.Columns[2].Name = "Filial";
            DataGrid1.Columns[2].HeaderText = "Filial";
            DataGrid1.Columns[2].Width = 20;
            DataGrid1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGrid1.Columns[2].ReadOnly = true;
            DataGrid1.Columns[2].Visible = false;

            DataGrid1.Columns[3].Name = "T.Doc";
            DataGrid1.Columns[3].HeaderText = "T.Doc";
            DataGrid1.Columns[3].Width = 35;
            DataGrid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGrid1.Columns[3].ReadOnly = true;

            DataGrid1.Columns[4].Name = "Serie";
            DataGrid1.Columns[4].HeaderText = "Serie";
            DataGrid1.Columns[4].Width = 39;
            DataGrid1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGrid1.Columns[4].ReadOnly = true;

            DataGrid1.Columns[5].Name = "N.Doc";
            DataGrid1.Columns[5].HeaderText = "N.Doc";
            DataGrid1.Columns[5].Width = 50;
            DataGrid1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGrid1.Columns[5].ReadOnly = true;

            DataGrid1.Columns[6].Name = "Ent.Loc.";
            DataGrid1.Columns[6].HeaderText = "Ent.Loc.";
            DataGrid1.Columns[6].Width = 55;
            DataGrid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGrid1.Columns[6].ReadOnly = false;

            DataGrid1.Columns[7].Name = "Fil.Loc.";
            DataGrid1.Columns[7].HeaderText = "Fil.Loc.";
            DataGrid1.Columns[7].Width = 20;
            DataGrid1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGrid1.Columns[7].ReadOnly = true;

            DataGrid1.Columns[8].Name = "T.Doc.Loc";
            DataGrid1.Columns[8].HeaderText = "T.Doc.Loc";
            DataGrid1.Columns[8].Width = 35;
            DataGrid1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGrid1.Columns[8].ReadOnly = true;

            DataGrid1.Columns[9].Name = "Serie Loc.";
            DataGrid1.Columns[9].HeaderText = "Serie Loc.";
            DataGrid1.Columns[9].Width = 40;
            DataGrid1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGrid1.Columns[9].ReadOnly = false;

            DataGrid1.Columns[9].Name = "Serie Loc.";
            DataGrid1.Columns[9].HeaderText = "Serie Loc.";
            DataGrid1.Columns[9].Width = 40;
            DataGrid1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGrid1.Columns[9].ReadOnly = false;

            DataGrid1.Columns[11].Name = "Data.Loc.";
            DataGrid1.Columns[11].HeaderText = "Data.Loc.";
            DataGrid1.Columns[11].Width = 50;
            DataGrid1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGrid1.Columns[11].ReadOnly = false;

            DataGrid1.Columns[12].Name = "Importar";
            DataGrid1.Columns[12].HeaderText = "Importar";
            DataGrid1.Columns[12].Width = 40;
            DataGrid1.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGrid1.Columns[12].ReadOnly = false;

            QueriesSQL.DropTabela(_tabela);
            QueriesSQL.FecharSQL();
        }

        private void btn_Processar_WF_Click(object sender, EventArgs e)
        {
            string entLoc, filialLoc, tipoDocLoc, serieLoc;
            long numDocLoc;
            bool Cancel = false;

            _dataDestino = datepicker_DataDocNovo_WF.Value;

            if (!_RSet.Vazia()) {
                _RSet.Inicio();

                while (!_RSet.NoFim() && Cancel != true) {
                    
                    // Uniformização do input na coluna Importa
                    string importa = _RSet.Valor("Importa");
                    importa = importa.ToUpper();

                    if (importa == "S" || importa == "N")
                    {
                        entLoc = _RSet.Valor("EntLocal");
                        filialLoc = _RSet.Valor("FilialLoc");
                        tipoDocLoc = _RSet.Valor("TipoDocLoc");
                        serieLoc = _RSet.Valor("SerieLoc");
                        numDocLoc = _RSet.Valor("NumDocLoc");

                        if (_RSet.Valor("TipoEntidade" == "C")  || _RSet.Valor("TipoEntidade" == "F")){
                            //CriarDocumentoVenda()
                            ImportaDocs importaDocs = new ImportaDocs();
                            //importaDocs.CriarDocumentoVenda()
                        }                        
                    }
                    _RSet.Seguinte();
                }
            }
            btn_Sair_WF.Focus();
        }

        private void btn_Sair_WF_Click(object sender, EventArgs e)
        {
            QueriesSQL.FecharSQL();
            this.Close();
            this.Dispose();
        }

        private void btn_Actualizar_WF_Click(object sender, EventArgs e)
        {

            //try {
            //    QueriesSQL.DropTabela(_tabela);
            //}d
            //catch {
            //    _PSO.MensagensDialogos.MostraAviso("Não foi possivel actualizar a tabela.", StdBSTipos.IconId.PRI_Exclama, $"Erro ao fazer Drop da tabela {_tabela}.");
            //}
            QueriesSQL.AbrirSQL();
            ActualizarPriGrelhaDocs(datepicker_DataDocImportar_WF.Value);
            prigrelha_Docs_WF.Grid_BloqueiaColuna("T.E", "Importar", 0, prigrelha_Docs_WF.Grelha.DataRowCnt, false);
            QueriesSQL.FecharSQL();

            _dataDestino = _dataOrigem;
            datepicker_DataDocNovo_WF = datepicker_DataDocImportar_WF;

            btn_Processar_WF.Focus();
        }
    }
}
