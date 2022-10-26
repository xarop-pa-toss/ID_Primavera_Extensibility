using Primavera.Extensibility.Sales.Editors; using Primavera.Extensibility.BusinessEntities; using Primavera.Extensibility.CustomForm; using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks; using System.IO;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100; using BasBE100;


// Erro 0001: tecnicoEscolhido[ ] não inicializado
namespace ID_EditorVendas.Sales
{
    public partial class UiEditorVendas : EditorVendas
    {
        public override void TipoDocumentoIdentificado(string Tipo, ref bool Cancel, ExtensibilityEventArgs e)
        {
            List<string> listaDocs = new List<string>();
            List<string> listaTecnicos = new List<string>();
            
            // Lista de documentos que têm de ter técnico no CDU_Técnico
            StdBELista docsValidos = BSO.Consulta("SELECT Documento FROM DocumentosVenda WHERE TipoDocumento='3' AND BensCirculacao='1'");

            listaDocs.Add(docsValidos.Valor(0));
            for (int i = 1; docsValidos.NumLinhas() >= i; i++)
            {
                listaDocs.Add(docsValidos.Valor(0));
                docsValidos.Seguinte();
            }
            System.Windows.Forms.MessageBox.Show(listaDocs[2]);
            docsValidos.Termina();

            // Lista de técnicos e form se TipoDoc for validado
            if (listaDocs.Contains(Tipo))
            {
                StdBELista tecnicos = BSO.Consulta("SELECT Nome FROM STP_Tecnicos");
                listaTecnicos.Add(tecnicos.Valor(0));
                for (int i = 1; tecnicos.NumLinhas() >= i; i++)
                    {
                        listaTecnicos.Add(tecnicos.Valor(0));
                        tecnicos.Seguinte();
                    }
                tecnicos.Termina();

                // Abre form
                using (var result = BSO.Extensibility.CreateCustomFormInstance(typeof(form_tecnico)))
                {
                    if (result.Result != null)
                    {
                        form_tecnico form = (result.Result as form_tecnico);
                        form.tecnicos = listaTecnicos;
                        form.docVenda = this.DocumentoVenda;
                        form.ShowDialog();
                    } 
                }
            }
            base.TipoDocumentoIdentificado(Tipo, ref Cancel, e);
            
        }            
    }
}
