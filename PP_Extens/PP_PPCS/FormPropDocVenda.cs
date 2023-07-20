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
        string _tDoc;

        public FormPropDocVenda()
        {
            InitializeComponent();
        }

        private void FormPropDocVenda_Load(object sender, EventArgs e)
        {
            StdBELista RSt = BSO.Consulta("SELECT 1 AS Dummy;");

            InicializaCBoxTipoDoc();
            InicializaCBoxSerie();
            InicializaTBoxNumero();
        }

        private void InicializaCBoxTipoDoc()
        {
            string sqlStr; StdBELista rcSet;
            cBoxTipoDoc.Items.Clear();
            
            sqlStr = "SELECT Documento, Descricao FROM DocumentosVenda WHERE Inactivo = 0;";
            rcSet = BSO.Consulta(sqlStr);
            rcSet.Inicio();

            while (!rcSet.NoFim()) {
                if (!cBoxTipoDoc.Items.Contains(rcSet.Valor(0))) {
                    cBoxTipoDoc.Items.Add(rcSet.Valor(0));
                }
            }

            sqlStr = "SELECT TransformaDocVenda AS col0 FROM ParametrosGCP;";
            rcSet = BSO.Consulta(sqlStr);

            cBoxTipoDoc.Text = rcSet.Valor(0);
            _tDoc = cBoxTipoDoc.Text;
            rcSet.Dispose();
        }

        private void InicializaCBoxSerie()
        {

        }

        private void InicializaTBoxNumero()
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void f41_Load(object sender, EventArgs e)
        {

        }
    }
}
