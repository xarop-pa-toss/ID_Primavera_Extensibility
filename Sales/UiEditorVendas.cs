using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;


namespace MDL_Obs.Sales
{
    public class UiEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            if (DocumentoVenda.Tipodoc == "FAPO" || DocumentoVenda.Tipodoc == "NCPO")
            {
                for (int i = 1; i <= DocumentoVenda.Linhas.NumItens; i++)
                {
                    string descricao = DocumentoVenda.Linhas.GetEdita(i).Descricao;
                    if (descricao != "")
                    {
                        DocumentoVenda.Observacoes = descricao;
                        break;
                    }
                    else { continue; }

                }
            }
        }
    }
}
