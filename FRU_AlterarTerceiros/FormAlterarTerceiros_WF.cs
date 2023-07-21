﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasBE100; using StdBE100; using ErpBS100; using StdPlatBS100;
using System.IO;


namespace FRU_AlterarTerceiros
{
    public partial class FormAlterarTerceiros_WF : Form
    {
        private ErpBS BSO = new ErpBS();
        private StdPlatBS PSO = new StdPlatBS();
        //Variavél global que contem o contexto e que deverá ser passada para os controlos.

        public FormAlterarTerceiros_WF()
        {
            InitializeComponent();
        }

        // LOAD e Inicialização
        private void FormAlterarTerceiros_WF_Load(object sender, EventArgs e)
        {
            // *** ABRIR EMPRESA ***
            // *** ASS REG SERVIDOR ***
            BSO.AbreEmpresaTrabalho(StdBETipos.EnumTipoPlataforma.tpProfissional, "0012004", "faturacao", "*Pelicano*");

            datepicker_DataDocInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            datepicker_DataDocFim.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);

            FillComboBox(cbox_Docs, "SELECT CONCAT(Documento, ' - ', Descricao) AS Documento FROM DocumentosVenda WHERE Inactivo = 0 ORDER BY Documento DESC;");
            FillComboBox(cbox_TipoTerceiro, "SELECT CONCAT(TipoTerceiro - Descricao) AS TipoTerceiro FROM TipoTerceiros WHERE Clientes = 1;");
        }


        // BOTÕES
        private void btn_ActualizarPriGrelha_Click(object sender, EventArgs e)
        {
            // Valores dos controlos
            int
                numDocInicio = (int)num_NumDocInicio.Value,
                numDocFim = (int)num_NumDocFim.Value;
            string
                dataInicio = datepicker_DataDocInicio.Value.ToString().Substring(0, 10),
                dataFim = datepicker_DataDocFim.Value.ToString().Substring(0, 10),
                tipoDoc = GetValorDaComboBoxSemDescricao(cbox_Docs);

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
            rcSet.Inicio();
            datagrid_Docs.Rows.Clear();

            while (!rcSet.NoFim()) {
                datagrid_Docs.Rows.Add(
                    rcSet.Valor("Cf"),
                    rcSet.Valor("Data"),
                    rcSet.Valor("TipoDoc"),
                    rcSet.Valor("Serie"),
                    rcSet.Valor("NumDoc"),
                    rcSet.Valor("TipoTerceiro"),
                    rcSet.Valor("TotalDocumento"));
                rcSet.Seguinte();
            }
        }

        private void btn_AlterarTerceiro_Click(object sender, EventArgs e)
        {
            string gTipoDoc, gSerie, gNumDoc, gTipoTerceiro;
            Dictionary<string, string> valoresControlos = GetControlos();
            List<string> docsComErroNoUpdateSQL = new List<string>();

            if (!CheckControlos(valoresControlos)) { return; }

            foreach (DataGridViewRow row in datagrid_Docs.Rows) {

                // Se a checkbox não estiver picada, skip pra proxima linha.
                if (!row.Cells["Cf"].Value.Equals(true)) {
                    continue;
                }

                gTipoDoc = row.Cells["TipoDoc"].Value.ToString();
                gSerie = row.Cells["Serie"].Value.ToString();
                gNumDoc = row.Cells["NumDoc"].Value.ToString();
                gTipoTerceiro = GetValorDaComboBoxSemDescricao(cbox_TipoTerceiro);

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
            } else {
                PSO.MensagensDialogos.MostraAviso("Todos os documentos alterados com sucesso.", StdBSTipos.IconId.PRI_Informativo);
            }
        }


        // MISC
        private void cBox_Docs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoDoc = GetValorDaComboBoxSemDescricao(cbox_Docs);
            cbox_Serie.Items.Clear();
            FillComboBox(cbox_Serie, "SELECT DISTINCT Serie FROM CabecDoc WHERE TipoDoc = '" + tipoDoc + "' ORDER BY Serie DESC;");
            if (cbox_Serie.Items.Count > 0) { cbox_Serie.SelectedIndex = 0; } else { cbox_Serie.Text = ""; }
        }


        // HELPERS
        private string GetValorDaComboBoxSemDescricao(ComboBox comboBox)
        {
            // Get substring do conteudo da cBox_TipoDoc para usar apenas o TipoDoc para descobrir a Serie.
            // Encontra o primeiro espaço e usa o índice para apanhar o que está pra trás
            int indicePrimeiroEspaco = comboBox.Text.IndexOf(' ');
            if (indicePrimeiroEspaco == -1) { return ""; } // Se não encontrar espaços, termina.
            string valor = comboBox.Text.Substring(0, indicePrimeiroEspaco);

            return valor;
        }

        private void FillComboBox(ComboBox comboBox, string query)
        {
            // Preenche qualquer winforms combobox com qualquer consulta SQL utilizando StdBELista
            using (StdBELista priLista = BSO.Consulta(query)) {

                if (!priLista.Vazia()) {
                    priLista.Inicio();
                    while (!priLista.NoFim()) {
                        comboBox.Items.Add(priLista.Valor(0));
                        priLista.Seguinte();
                    }
                    priLista.Termina();
                }
            }
        }

        private Dictionary<string, string> GetControlos()
        {
            Dictionary<string, string> valoresControlos = new Dictionary<string, string>();

            valoresControlos.Add("TipoDoc", cbox_Docs.Text);
            valoresControlos.Add("Terceiro", cbox_TipoTerceiro.Text);
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