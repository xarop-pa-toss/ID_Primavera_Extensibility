using Primavera.Extensibility.BusinessEntities; using Primavera.Extensibility.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using StdBE100;
using StdPlatBS100;
using VndBE100;

namespace PP_Extens
{
    internal class PP_CaixasJM : CustomCode
    {
        private readonly VndBEDocumentoVenda _docVenda;
        public PP_CaixasJM(VndBEDocumentoVenda docVenda)
        {
            _docVenda = docVenda;
        }

        public void ProcessarCaixas()
        {

        }

        // Lógica aplicada no AntesDeGravar e no DepoisDeGravar
        internal void EnviarCaixasParaJM()
        {
            List<string> docsJM = new List<string> { };
            // Entidades JM começam com 603
            if (_docVenda.Entidade.StartsWith("603") && docsJM.Contains(_docVenda.Tipodoc))
            {

            }
        }

        internal void ReceberCaixasFornecedor()
        {
            List<string> docsFornecedor = new List<string> { };
            // Entidades fornecedor
            if (_docVenda.Entidade.StartsWith("") && docsFornecedor.Contains(_docVenda.Tipodoc))
            {
            }
        }



        }
    }
}
