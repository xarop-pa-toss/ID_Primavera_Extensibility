using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASRLB_ImportacaoFatura.Sales
{
    public class UiEditorVendas : EditorVendas
    {
        // Criação de arrays com tabelasF4 relevantes à verificação dos dados do IMPORT.txt
        // Podia ser feita a verificação ao Motor a cada linha mas é mais lento que aceder a uma estrutura em memória
        public void LER_ParaArray(string strFich)
        {
            int i = 0;
            int k = 0;
            bool jaVerificado;
            bool existemErros = false;

            

            List<string> list_codigoArtigo = new List<string>();

        }
    }
}
