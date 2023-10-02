using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdBE100;

namespace PP_PPCS
{
    public partial class FormPropDocVenda : CustomForm
    {
        public FormPropDocVenda()
        {
            InitializeComponent();
        }

        private void FormPropDocVenda_Load(object sender, EventArgs e)
        {
            StdBELista RSt = BSO.Consulta("SELECT 1 AS Dummy;");

            InicializaTBoxNumero();
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
            using (StdBE100.StdBELista priLista = BSO.Consulta(query)) {
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

        private void InicializaTBoxNumero()
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }
    }
}
