using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRISDK100; using ErpBS100; using StdBE100; using StdPlatBS100;

namespace FRUTI_Extens
{
    public partial class VendArtigo : Form
    {
        string _relatorio, _rSel, _gSel, _titulo, _dataInicial, _dataFinal, _detalhe;
        private ErpBS _BSO;
        private StdPlatBS _PSO;

        private void btn_Imprimir_Click(object sender, EventArgs e)
        {

        }

        public VendArtigo()
        {
            InitializeComponent();
        }

        private void VendArtigo_Load(object sender, EventArgs e)
        {
            Motor.PriEngine.CreateContext("ADEGA", "admin", "id1234!!");
            _BSO = Motor.PriEngine.Engine;
            _PSO = Motor.PriEngine.Platform;

            f4_subfamilia.Inicializa(Motor.PriEngine.PriSDKContexto);

            DateTime dataHoje = DateTime.Now;
            dtPicker_dataInicial.Value = dataHoje.AddDays(-90);
            dtPicker_dataFinal.Value = dataHoje;
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
