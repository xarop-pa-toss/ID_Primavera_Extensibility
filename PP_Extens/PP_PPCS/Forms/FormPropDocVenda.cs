using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdBE100;
using PRISDK100;
using HelpersPrimavera10;
using ErpBS100;
using StdPlatBS100;
using System.Data;

namespace PP_PPCS
{
    public partial class FormPropDocVenda : CustomForm
    {
        private ErpBS _BSO;
        private StdBSInterfPub _PSO;
        private clsSDKContexto _sdkContexto;

        private HelperFunctions _Helpers = new HelperFunctions();

        public FormPropDocVenda()
        {
            InitializeComponent();

            _PSO = PriMotores.Plataforma;
            _BSO = PriMotores.Motor;
            _sdkContexto = PriMotores.PriSDKContexto;
        }

        private void FormPropDocVenda_Load(object sender, EventArgs e)
        {
            StdBELista RSt = _BSO.Consulta("SELECT 1 AS Dummy;");
            f4TipoDoc.Inicializa(_sdkContexto);
        }

        private void f4TipoDoc_TextChange(object Sender, PRISDK100.F4.TextChangeEventArgs e)
        {
            // Get Serie do TipoDoc para a ComboBox de Série
            if (f4TipoDoc.Text != "") {
                string query = "SELECT DISTINCT Serie FROM SeriesVendas WHERE TipoDoc = '" + f4TipoDoc.Text + "' ORDER BY Serie DESC;";
                cBoxSerie.Items.Clear();
                cBoxSerie.Items.AddRange(FillComboBox(query).ToArray());
                cBoxSerie.SelectedIndex = 0;
            }
        }

        private List<string> FillComboBox(string query)
        {
            using (StdBE100.StdBELista priLista = _BSO.Consulta(query)) {
                List<string> listaFinal = new List<string>();

                if (!priLista.Vazia()) {
                    priLista.Inicio();
                    while (!priLista.NoFim()) {
                        listaFinal.Add(priLista.Valor(0));
                        priLista.Seguinte();
                    }
                    priLista.Termina();

                    return listaFinal;
                }
                return null;
            }
        }

        private void cBoxSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get NumDoc para o NumericUpDown a partir da Série
            string serie = cBoxSerie.Text;
            string tipoDoc = f4TipoDoc.Text;

            DataTable numDocTbl = _Helpers.GetDataTableDeSQL(
                "SELECT TOP(1) NumDoc " +
                "FROM CabecDoc " +
               $"WHERE TipoDoc = '{tipoDoc}' " +
               $"   AND Serie = '{serie}' " +
               $"ORDER BY NumDoc DESC;");

            if (numDocTbl.Rows.Count > 0)
            {
                foreach (DataRow row in numDocTbl.Rows)
                {
                    numUpDownNumDoc.Value = (decimal)row[0];
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }
    }
}
