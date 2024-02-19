using System;
using System.Windows.Forms;

namespace PP_Qualidade
{
    public partial class FormQueryStatus_WF : Form
    {
        public FormQueryStatus_WF()
        {
            InitializeComponent();
        }

        private void FormQueryStatus_WF_Load(object sender, EventArgs e)
        {

        }

        public void SetStatus(string status) 
        {
            lbl_QueryStatus.Text = status;
            lbl_QueryStatus.Refresh();
        }
    }
}
