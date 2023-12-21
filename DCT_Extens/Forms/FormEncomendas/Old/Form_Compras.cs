﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Encomendas
{
    public partial class Form_Compras : Form
    {
        public Form_Compras()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripStatusLabel1.Text = "";
        }

        private void btnVerDocs_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "A procurar encomendas abertas...";
            statusStrip1.Refresh();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                {
                    cn.Open();


                    var sqlQuery = (@"select ccs.fechado as Fechado,cc.TipoDoc as Documento,
                                        cc.NumDoc as Numero,
                                        cc.serie as Serie,
                                        cc.dataDoc as Data,
                                        cc.entidade as Fornecedor,
                                        cc.Nome as Nome,
                                        ccs.IdCabecCompras
                                    from cabeccompras cc
                                    inner join CabecComprasStatus ccs on cc.Id=IdCabecCompras
	                                inner join DocumentosCompra dc on cc.TipoDoc=dc.Documento
	                                inner join Fornecedores fn on cc.Entidade=fn.Fornecedor 
                                        where cc.dataDoc between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and" +
                                    " '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and" +
                                    " tipodoc = '" + txtTipoDoc.Text + "' and" +
                                    " ccs.estado='P' and" +
                                    " ccs.fechado='0' and" +
                                    " ccs.anulado='0'" +
                                    " order by DataDoc");
                    using (SqlDataAdapter da = new SqlDataAdapter(sqlQuery, cn))
                    {
                        using (DataTable dt = new DataTable())
                        {

                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }

                    toolStripStatusLabel1.Text = "Dados carregados.";
                    statusStrip1.Refresh();

                    //dataGridView1.ReadOnly = true;
                    dataGridView1.Columns[0].ReadOnly = false; //Fechado
                    dataGridView1.Columns[1].ReadOnly = true; //Documento
                    dataGridView1.Columns[2].ReadOnly = true; //Numero
                    dataGridView1.Columns[3].ReadOnly = true; //Serie
                    dataGridView1.Columns[4].ReadOnly = true; //Data
                    dataGridView1.Columns[5].ReadOnly = true; //Cliente
                    dataGridView1.Columns[6].ReadOnly = true; //Nome
                    dataGridView1.Columns[6].Width = 319;
                    dataGridView1.Columns[7].Visible = false; //coluna com o ID IdCabecCompras

                    //MessageBox.Show("A ligar à base de dados");
                }
            }

            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Falha na ligação";
                statusStrip1.Refresh();

                MessageBox.Show("Falha ao tentar ligar\n\n" + ex.Message);
            }


        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateDatabase_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                    {
                        cn.Open();
                        string query = "update CabecComprasStatus set Fechado='1' where IdCabecCompras='" + row.Cells[7].Value + "'";

                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.ExecuteNonQuery();
                        cn.Close();

                        toolStripStatusLabel1.Text = "Os Documentos selecionados, foram alterados para o estado 'Fechado'";
                        statusStrip1.Refresh();

                    }

                }

            }
            MessageBox.Show("Documento(s) Fechado(s)");
            //ZONA DE TESTE PARA UPDATE QUERY
            using (SqlConnection cn = new SqlConnection(Conn.StrCon))
            {
                cn.Open();


                var sqlQuery = (@"select ccs.fechado as Fechado,cc.TipoDoc as Documento,
                                        cc.NumDoc as Numero,
                                        cc.serie as Serie,
                                        cc.dataDoc as Data,
                                        cc.entidade as Fornecedor,
                                        cc.Nome as Nome,
                                        ccs.IdCabecCompras
                                    from cabeccompras cc
                                    inner join CabecComprasStatus ccs on cc.Id=IdCabecCompras
	                                inner join DocumentosCompra dc on cc.TipoDoc=dc.Documento
	                                inner join Fornecedores fn on cc.Entidade=fn.Fornecedor 
                                        where cc.dataDoc between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and" +
                                    " '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and" +
                                    " tipodoc = '" + txtTipoDoc.Text + "' and" +
                                    " ccs.estado='P' and" +
                                    " ccs.fechado='0' and" +
                                    " ccs.anulado='0'" +
                                    " order by DataDoc");
                using (SqlDataAdapter da = new SqlDataAdapter(sqlQuery, cn))
                {
                    using (DataTable dt = new DataTable())
                    {

                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }

                toolStripStatusLabel1.Text = "Dados Atualizados.";
                statusStrip1.Refresh();

                //dataGridView1.ReadOnly = true;
                dataGridView1.Columns[0].ReadOnly = false; //Fechado
                dataGridView1.Columns[1].ReadOnly = false; //Documento
                dataGridView1.Columns[2].ReadOnly = true; //Numero
                dataGridView1.Columns[3].ReadOnly = true; //Serie
                dataGridView1.Columns[4].ReadOnly = true; //Data
                dataGridView1.Columns[5].ReadOnly = true; //Cliente
                dataGridView1.Columns[6].ReadOnly = true; //Nome
                dataGridView1.Columns[6].Width = 319;
                dataGridView1.Columns[7].Visible = false; //coluna com o ID idcabecdoc

                //MessageBox.Show("A ligar à base de dados");
            }
            //FIM DE ZONA DE TESTE PARA UPDATE DE QUERY
        }

        private void Form_Compras_Load(object sender, EventArgs e)
        {

        }
    }
}