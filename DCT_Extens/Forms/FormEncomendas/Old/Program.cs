using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encomendas
{
    internal static class Program
    {
        /// <summary>
        /// Utilitário para fecho de encomendas do Modulo de Vendas ou do Modulo de Compras
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form_Menu());
        }
    }
}
