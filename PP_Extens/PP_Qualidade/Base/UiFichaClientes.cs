using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;


namespace PP_Qualidade.Base
{
    public class UiFichaClientes : FichaClientes
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (string.IsNullOrEmpty(Cliente.Morada)
                || string.IsNullOrEmpty(Cliente.Localidade)
                || string.IsNullOrEmpty(Cliente.Distrito))
            {
                Cancel = true;
                PSO.MensagensDialogos.MostraErro("O cliente necessita de ter definida a Morada, Localidade e Distrito", StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);
            }
        }
    }
}
