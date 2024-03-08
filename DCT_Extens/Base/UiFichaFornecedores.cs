using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT_Extens
{
    public class UiFichaFornecedores : FichaFornecedores
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            #region Validação Segmento-Terceiro
            if (string.IsNullOrEmpty(Fornecedor.SegmentoTerceiro)) { Fornecedor.SegmentoTerceiro = "001"; }
            #endregion
        }
    }
}
