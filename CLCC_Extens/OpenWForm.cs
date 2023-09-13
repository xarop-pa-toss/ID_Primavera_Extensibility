using Primavera.Extensibility.CustomCode;

namespace CLCC_Extens.Sales
{
    public class OpenWForm : CustomCode
    {
        public void Abrir_FormGeraFatura_WF()
        {
            FormGeraFatura_WF form = new FormGeraFatura_WF();
            form.ShowDialog();
            PSO.UI.AdicionaFormMDI(form);
        }
    }
}
