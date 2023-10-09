using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PP_PPCS
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
