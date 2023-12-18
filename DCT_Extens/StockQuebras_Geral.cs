using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCT_Extens.Helpers;
using ErpBS100;
using StdPlatBS100;
using VndBE100;

namespace DCT_Extens
{
    public class StockQuebras_Geral : CustomCode
    {
        private ErpBS _BSO { get; set; }
        private StdPlatBS _PSO { get; set; }
        private HelperFunctions _Helpers { get; set; }

        public StockQuebras_Geral()
        {
            _BSO = PriMotores.Motor;
            _PSO = PriMotores.Plataforma;
            _Helpers = new HelperFunctions();
        }


    }
}
