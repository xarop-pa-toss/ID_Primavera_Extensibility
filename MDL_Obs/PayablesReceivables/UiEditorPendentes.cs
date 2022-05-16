using Primavera.Extensibility.PayablesReceivables.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;

namespace MDL_Obs.PayablesReceivables
{
    public class UiEditorPendentes : EditorPendentes
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            if (DocumentoPendente.Tipodoc == "NLC" || DocumentoPendente.Tipodoc == "NLD")
            {
                for (int i = 1; i <= DocumentoPendente.Linhas.NumItens; i++)
                {
                    string descricao = DocumentoPendente.Linhas.GetEdita(i).Descricao;
                    if (descricao != "")
                    {
                        DocumentoPendente.Observacoes = descricao;
                        break;
                    }
                    else { continue; }
                }
            }
        }
    }
}
