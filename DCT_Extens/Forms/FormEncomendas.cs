using DCT_Extens.Helpers;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using StdBE100;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCT_Extens
{
    public partial class FormEncomendas : CustomForm
    {
        private ErpBS100.ErpBS _BSO;
        private StdPlatBS100.StdPlatBS _PSO;
        private Helpers.HelperFunctions _Helpers;

        public FormEncomendas()
        {
            _BSO = PriMotores.Motor;
            _PSO = PriMotores.Plataforma;
            _Helpers = new Helpers.HelperFunctions();

            InitializeComponent();

            panel_TipoDocRadios.Controls.Add(radio_Vendas);
            panel_TipoDocRadios.Controls.Add(radio_Compras);
        }


        private void FormEncomendas_Load(object sender, EventArgs e)
        {
            radio_Vendas.Checked = true;
        }

        private void btn_VerDocs_Click(object sender, EventArgs e)
        {
            try
            {
                ActualizaDataGrid();
            }
            catch (Exception ex)
            {
                _Helpers.EscreverParaFicheiroTxt(ex.ToString(), "FormEncomendas_VerDocs_Click");
                PSO.MensagensDialogos.MostraErro("Falha ao ler dados na base de dados Primavera.");
            }
        }

        public void ActualizaDataGrid()
        {
            string query = null;
            if (radio_Vendas.Checked)
            {
                query = $"SELECT " +
                        $"   cds.fechado AS Fechado, " +
                        $"   cd.TipoDoc AS Documento, " +
                        $"   cd.NumDoc AS Numero, " +
                        $"   cd.serie AS Serie, " +
                        $"   cd.data AS Data, " +
                        $"   cd.entidade AS Cliente, " +
                        $"   cd.Nome AS Nome, " +
                        $"   cds.IdCabecDoc " +
                        $"FROM " +
                        $"   cabecdoc cd " +
                        $"   INNER JOIN cabecdocstatus cds ON cd.id = cds.IdCabecDoc " +
                        $"   INNER JOIN documentosvenda dv ON cd.tipodoc = dv.documento " +
                        $"   INNER JOIN clientes cl ON cd.entidade = cl.cliente " +
                        $"WHERE " +
                        $"   cd.data BETWEEN '{dtPicker_DataInicial.Value.ToString("yyyy-MM-dd")}' AND '{dtPicker_DataFinal.Value.ToString("yyyy-MM-dd")}' " +
                        $"   AND tipodoc = '{txtBox_TipoDoc.Text}' " +
                        $"   AND dv.tipodocumento = '2' " +
                        $"   AND cds.estado = 'P' " +
                        $"   AND cds.fechado = '0' " +
                        $"   AND cds.anulado = '0' " +
                        $"ORDER BY Data";

            } else if (radio_Compras.Checked)
            {
                query = $"SELECT " +
                        $"   ccs.fechado AS Fechado, " +
                        $"   cc.TipoDoc AS Documento, " +
                        $"   cc.NumDoc AS Numero, " +
                        $"   cc.serie AS Serie, " +
                        $"   cc.dataDoc AS Data, " +
                        $"   cc.entidade AS Fornecedor, " +
                        $"   cc.Nome AS Nome, " +
                        $"   ccs.IdCabecCompras " +
                        $"FROM " +
                        $"   cabeccompras cc " +
                        $"   INNER JOIN CabecComprasStatus ccs ON cc.Id = ccs.IdCabecCompras " +
                        $"   INNER JOIN DocumentosCompra dc ON cc.TipoDoc = dc.Documento " +
                        $"   INNER JOIN Fornecedores fn ON cc.Entidade = fn.Fornecedor " +
                        $"WHERE " +
                        $"   cc.dataDoc BETWEEN '{dtPicker_DataInicial.Value.ToString("yyyy-MM-dd")}' AND '{dtPicker_DataFinal.Value.ToString("yyyy-MM-dd")}' " +
                        $"   AND tipodoc = '{txtBox_TipoDoc.Text}' " +
                        $"   AND ccs.estado = 'P' " +
                        $"   AND ccs.fechado = '0' " +
                        $"   AND ccs.anulado = '0' " +
                        $"ORDER BY DataDoc";
            }

            DataTable docsTabela = _Helpers.GetDataTableDeSQL(query);
            dataGrid_Docs.DataSource = docsTabela;

            //dataGridView1.ReadOnly = true;
            dataGrid_Docs.Columns[0].ReadOnly = false; //Fechado
            dataGrid_Docs.Columns[1].ReadOnly = true; //Documento
            dataGrid_Docs.Columns[2].ReadOnly = true; //Numero
            dataGrid_Docs.Columns[3].ReadOnly = true; //Serie
            dataGrid_Docs.Columns[4].ReadOnly = true; //Data
            dataGrid_Docs.Columns[5].ReadOnly = true; //Cliente
            dataGrid_Docs.Columns[6].ReadOnly = true; //Nome
            dataGrid_Docs.Columns[6].Width = 319;
            dataGrid_Docs.Columns[7].Visible = false; //coluna com o ID IdCabecDoc
        }


        private void btn_UpdateDB_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow linha in dataGrid_Docs.Rows)
            {
                try
                {
                    if ((bool)linha.Cells[0].Value)
                    {
                        StdBEExecSql sql = new StdBEExecSql();
                        sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                        sql.Tabela = "CabecDocStatus";
                        sql.AddCampo("Fechado", "1");
                        sql.AddCampo("IdCabecDoc", "" + linha.Cells[7].Value + "", true);

                        PSO.ExecSql.Executa(sql);
                    }
                }
                catch (Exception ex)
                {
                    _Helpers.EscreverParaFicheiroTxt(ex.ToString(), "FormEncomendas_UpdateDB_Click");
                    PSO.MensagensDialogos.MostraErro("Não foi possivel fechar todos os documentos seleccionados.");
                    ActualizaDataGrid();
                    break;
                }
            }

            // Se o catch não apanhar nada é porque fechou tudo ok
            PSO.MensagensDialogos.MostraMensagem(StdPlatBS100.StdBSTipos.TipoMsg.PRI_SimplesOk, "Todos os documentos foram fechados com sucesso.");
            ActualizaDataGrid();

        }
    }
}

