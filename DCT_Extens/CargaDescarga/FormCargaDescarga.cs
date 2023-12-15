using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT_Extens
{
    public partial class FormCargaDescarga : CustomForm
    {
        public FormCargaDescarga()
        {
            Helpers.PriMotores Motores;
            Helpers.HelperFunctions Helpers = new Helpers.HelperFunctions();

            InitializeComponent();
        }

        private void FormCargaDescarga_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {

        }

        private void FormCargaDescarga_Load(object sender, EventArgs e)
        {

        }
    }
}
