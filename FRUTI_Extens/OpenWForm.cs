using Primavera.Extensibility.CustomCode;

namespace FRUTI_Extens.Sales
{
    public class OpenWForm : CustomCode
    {
        public void Abrir_VendArtigo_WF()
        {
            VendArtigo form = new VendArtigo();
            form.ShowDialog();
            PSO.UI.AdicionaFormMDI(form);
        }
        public void Abrir_VendFornecedor_WF()
        {
            VendFornecedor form = new VendFornecedor();
            form.ShowDialog();
            PSO.UI.AdicionaFormMDI(form);
        }
    }
}
