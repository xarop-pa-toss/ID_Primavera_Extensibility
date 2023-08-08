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
        private StdBETransaccao _objStdTransac = new StdBETransaccao();

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

        }
    }
}
