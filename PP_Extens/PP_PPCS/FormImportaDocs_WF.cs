using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms; using ErpBS100; using StdPlatBS100; using StdBE100; using VndBE100;
using System.Security.Cryptography;
using BasBE100;

namespace PP_PPCS
{
    public partial class FormImportaDocs_WF : Form
    {
        private ErpBS _BSO;
        private StdPlatBS _PSO;
        private DateTime _dataOrigem, _dataDestino;
        private DataTable _RSet;
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
            datepicker_DataDocNovo.Value = DateTime.Now;

            QueriesSQL.AbrirSQL();
            //InicializarPriGrelhaDocs();
            InicializarDataGrid(datepicker_DataDocImportar_WF.Value);
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
            for (int i = 0; i < 13; i++) {
                DataGridViewTextBoxColumn coluna = new DataGridViewTextBoxColumn();
                DataGrid1.Columns.Add(coluna);
            }

            DataGridViewColumn col;
            //DataGrid1.Columns[0].Name = "TipoEntidade";
            col = DataGrid1.Columns[0];
            col.DataPropertyName = "TipoEntidade";
            col.HeaderText = "T.E.";
            col.Width = 30;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ReadOnly = true;

            //DataGrid1.Columns[1].Name = "Entidade";
            col = DataGrid1.Columns[1];
            col.DataPropertyName = "Entidade";
            col.HeaderText = "Entidade";
            col.Width = 65;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ReadOnly = true;

            //DataGrid1.Columns[2].Name = "Filial";
            col = DataGrid1.Columns[2]; 
            col.DataPropertyName = "Filial";
            col.HeaderText = "Filial";
            col.Width = 25;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ReadOnly = true;
            col.Visible = false;

            //DataGrid1.Columns[3].Name = "TipoDoc";
            col = DataGrid1.Columns[3];
            col.DataPropertyName = "TipoDoc";
            col.HeaderText = "T.Doc";
            col.Width = 45;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.ReadOnly = true;

            //DataGrid1.Columns[4].Name = "Serie";
            col = DataGrid1.Columns[4];
            col.DataPropertyName = "Serie";
            col.HeaderText = "Serie";
            col.Width = 45;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ReadOnly = true;

            //DataGrid1.Columns[5].Name = "NumDoc";
            col = DataGrid1.Columns[5];
            col.DataPropertyName = "NumDoc";
            col.HeaderText = "N.Doc";
            col.Width = 55;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.ReadOnly = true;

            //DataGrid1.Columns[6].Name = "EntLocal";
            col = DataGrid1.Columns[6];
            col.DataPropertyName = "EntLocal";
            col.HeaderText = "Ent.Loc.";
            col.Width = 65;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ReadOnly = true;

            //DataGrid1.Columns[7].Name = "FilialLoc";
            col = DataGrid1.Columns[7];
            col.DataPropertyName = "FilialLoc";
            col.HeaderText = "Fil.Loc.";
            col.Width = 25;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ReadOnly = true;
            col.Visible = false;

            //DataGrid1.Columns[8].Name = "TipoDocLoc";
            col = DataGrid1.Columns[8];
            col.DataPropertyName = "TipoDocLoc";
            col.HeaderText = "T.Doc.Loc";
            col.Width = 35;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ReadOnly = true;

            //DataGrid1.Columns[9].Name = "SerieLoc";
            col = DataGrid1.Columns[9];
            col.DataPropertyName = "SerieLoc";
            col.HeaderText = "Serie Loc.";
            col.Width = 45;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ReadOnly = true;

            //DataGrid1.Columns[10].Name = "NumDocLocal";
            col = DataGrid1.Columns[10];
            col.DataPropertyName = "NumDocLocal";
            col.HeaderText = "N.Doc.Loc.";
            col.Width = 55;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ReadOnly = true;

            //DataGrid1.Columns[11].Name = "Data";
            col = DataGrid1.Columns[11];
            col.DataPropertyName = "Data";
            col.HeaderText = "Data.Loc.";
            col.Width = 85;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.ReadOnly = true;

            //DataGrid1.Columns[12].Name = "Importa";
            col = DataGrid1.Columns[12];
            col.DataPropertyName = "Importa";
            col.HeaderText = "Importar";
            col.Width = 55;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.ReadOnly = false;

            ActualizarDataGrid(dataImport);
        }

        private void ActualizarDataGrid(DateTime dataImport)
        {
            // Se o programa não conseguir criar e popular a tabela temporária, termina programa com uma mensagem de erro.
            if (!QueriesSQL.GerarTabela(_tabela, dataImport.ToString())) { QueriesSQL.FecharSQL(); return; }

            _RSet = _BSO.ConsultaDataTable(QueriesSQL.GetQuery03(_tabela));
            DataGrid1.DataSource = _RSet;

            QueriesSQL.OperacoesTabela(_tabela, "DROP");
            QueriesSQL.FecharSQL();
        }


        private void btn_Processar_WF_Click(object sender, EventArgs e)
        {
            bool Cancel = false;
            string datanova = datepicker_DataDocNovo.Text;
            DateTime dataNovo = datepicker_DataDocNovo.Value;
            DateTime data3 = dateTimePicker1.Value;

            _dataDestino = datepicker_DataDocNovo.Value;

            for (int i = 0; i < _RSet.Rows.Count; i++) {
                if (Cancel == true) { return; }

                DataRow linha = _RSet.Rows[i];

                // Catch minusculas na coluna Importa
                string importa = linha["Importa"].ToString();
                importa = importa.ToUpper();

                if (importa == "S" || importa == "A") {

                    string tipoEntidade = linha["TipoEntidade"].ToString().ToUpper();

                    ImportaDocs importaDocs = new ImportaDocs();
                    if (tipoEntidade.Equals("C")) {
                        importaDocs.CriarDocumentoVenda(ref linha, dataNovo); 
                    } else if (tipoEntidade.Equals("F")) {
                        importaDocs.CriarDocumentoCompra(ref linha, dataNovo);
                    }

                    // CHECK CANCEL
                    bool trans = _BSO.EmTransaccao();
                    if (_BSO.EmTransaccao()) {
                        if (Cancel) { _BSO.DesfazTransaccao(); } else { _BSO.TerminaTransaccao(); }
                    }
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

        private void datepicker_DataDocImportar_WF_ValueChanged(object sender, EventArgs e)
        {
            datepicker_DataDocNovo.Value = datepicker_DataDocImportar_WF.Value;
        }

        private void btn_Actualizar_WF_Click(object sender, EventArgs e)
        {
            QueriesSQL.AbrirSQL();
            ActualizarDataGrid(datepicker_DataDocImportar_WF.Value);
            QueriesSQL.FecharSQL();

            _dataDestino = _dataOrigem;

            btn_Processar_WF.Focus();
        }
    }
}
