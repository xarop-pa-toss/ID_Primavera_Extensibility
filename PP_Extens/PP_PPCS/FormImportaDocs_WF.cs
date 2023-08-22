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
        private string _tabela = "#A" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.ToString("HHmmss").Replace(":", "");

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
            QueriesSQL.AbrirSQL();

            Motor.PriEngine.CreateContext("0012004", "id", "*Pelicano*");
            _BSO = Motor.PriEngine.Engine;
            _PSO = Motor.PriEngine.Platform;
            prigrelha_Docs_WF.Inicializa(Motor.PriEngine.PriSDKContexto);

            datepicker_DataDocImportar_WF.Value = DateTime.Now;
            datepicker_DataDocNovo_WF.Value = DateTime.Now;

            InicializarPriGrelhaDocs();
            ActualizarPriGrelhaDocs(DateTime.Now);
        }

        private void InicializarPriGrelhaDocs()
        {
            prigrelha_Docs_WF.LimpaGrelha();
            prigrelha_Docs_WF.TituloGrelha = "Documentos do dia: " + _dataOrigem.ToString();
            prigrelha_Docs_WF.PermiteActualizar = true;
            prigrelha_Docs_WF.PermiteAgrupamentosUser = true;
            prigrelha_Docs_WF.PermiteScrollBars = true;
            prigrelha_Docs_WF.PermiteVistas = false;
            prigrelha_Docs_WF.PermiteEdicao = false;
            prigrelha_Docs_WF.PermiteDataFill = false;
            prigrelha_Docs_WF.PermiteFiltros = false;
            prigrelha_Docs_WF.PermiteActiveBar = false;
            prigrelha_Docs_WF.PermiteContextoVazia = false;

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


            prigrelha_Docs_WF.AddColKey("T.E", 5, "T.E", 24, true);
            prigrelha_Docs_WF.AddColKey("Entidade", 5, "Entidade", 55, true, blnDrillDown: true);
            prigrelha_Docs_WF.AddColKey("Filial", 5, "Filial", 21, true, blnVisivel: false);
            prigrelha_Docs_WF.AddColKey("T.Doc", 5, "T.Doc", 30, true);
            prigrelha_Docs_WF.AddColKey("Serie", 5, "Serie", 40, true);
            prigrelha_Docs_WF.AddColKey("N.Doc", 5, "N.Doc", 50, true);
            prigrelha_Docs_WF.AddColKey("Ent.Loc", 5, "Ent.Loc.", 55, true);
            prigrelha_Docs_WF.AddColKey("Fil.Loc", 5, "Fil.Loc.", 20, true, blnVisivel: false);
            prigrelha_Docs_WF.AddColKey("T.Doc.Loc", 5, "T.Doc.Loc", 35, true);
            prigrelha_Docs_WF.AddColKey("Serie Loc.", 5, "Serie Loc.", 40, true);
            prigrelha_Docs_WF.AddColKey("N.Doc.Loc", 5, "N.Doc.Loc", 50, true);
            prigrelha_Docs_WF.AddColKey("Data Loc.", 5, "Data Loc.", 50, true);
            prigrelha_Docs_WF.AddColKey("Importar", 5, "Importar", 40, true);

            //using (StdBEExecSql sql = new StdBEExecSql()) {
            //    sql.tpQuery = StdBETipos.EnumTpQuery.tpDELETE;
            //    sql.Tabela = "CabecDoc";                                                                                    // UPDATE CabecDoc
            //    sql.AddCampo("TipoTerceiro", gTipoTerceiro);                                                                // SET TipoTerceiro = ...
            //    sql.AddCampo("Tipodoc", gTipoDoc, true, StdBETipos.EnumTipoCampoSimplificado.tsTexto);                      // WHERE TipoDoc = ...
            //    sql.AddCampo("Serie", gSerie, true, StdBETipos.EnumTipoCampoSimplificado.tsTexto);                          // AND ...
            //    sql.AddCampo("NumDoc", Convert.ToInt32(gNumDoc), true, StdBETipos.EnumTipoCampoSimplificado.tsInteiro);     // AND ...

            //    sql.AddQuery();

            //    // Se Update falhar, preenche lista com NumDoc para mostrar ao cliente.
            //    try {
            //        _PSO.ExecSql.Executa(sql);
            //    }
            //    catch (Exception ex) {
            //        docsComErroNoUpdateSQL.Add(gNumDoc + ex.ToString());
            //    }
            //}
        }
        
        private void ActualizarPriGrelhaDocs(DateTime dataImport)
        {
            prigrelha_Docs_WF.LimpaGrelha();

            // Define data de origem. PriGrelha inicializa com data de hoje.
            _dataOrigem = dataImport;

            // Ver QueriesSQL.cs
            QueriesSQL.CreateTabela(_tabela, dataImport.ToString());
            QueriesSQL.InsertTabela(_tabela, dataImport.ToString());

            _RSet = _BSO.Consulta(QueriesSQL.GetQuery03(_tabela));
            prigrelha_Docs_WF.DataBind(_RSet);
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
                        }                        
                    }
                    _RSet.Seguinte();
                }
            }
            btn_Sair_WF.Focus();
        }

        private void btn_Sair_WF_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void FormImportaDocs_WF_FormClosing(object sender, FormClosingEventArgs e)
        {
            QueriesSQL.FecharSQL();
        }

        private void btn_Actualizar_WF_Click(object sender, EventArgs e)
        {

            try {
                QueriesSQL.DropTabela(_tabela);
            }
            catch {
                _PSO.MensagensDialogos.MostraAviso("Não foi possivel actualizar a tabela.", StdBSTipos.IconId.PRI_Exclama, $"Erro ao fazer Drop da tabela {_tabela}.");
            }

            ActualizarPriGrelhaDocs(_dataOrigem);
            
            _dataDestino = _dataOrigem;
            datepicker_DataDocNovo_WF = datepicker_DataDocImportar_WF;

            btn_Processar_WF.Focus();
        }
    }
}
