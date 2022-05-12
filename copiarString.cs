using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;

// Refer�ncias: BasBE100 e VndBE100

namespace copiarString.Sales
{
    public class copiarString : EditorVendas
    {

        // Classe usada para testes. Adicionar um artigo for�a interac��o com Armaz�m que activa a classe.
        //public override void ArmazemIdentificado(string Armazem, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        //{
        //    System.Windows.Forms.MessageBox.Show(DocumentoVenda.Linhas.NumItens.ToString());

        //    for (int i = 1; i <= DocumentoVenda.Linhas.NumItens; i++)
        //    {
        //        if (DocumentoVenda.Linhas.GetEdita(i).TipoLinha == "10")
        //        {
        //            DocumentoVenda.Observacoes = DocumentoVenda.Linhas.GetEdita(i).Descricao;

        //            //string iString = ToString(i);
        //            break;
        //        }
        //        else
        //        {
        //            continue;
        //            base.ArmazemIdentificado(Armazem, NumLinha, ref Cancel, e);
        //        }
        //    }
        //}


        // Classe para produ��o, igual � de cima mas activada no evento AntesDeGravar

        // Classe de Produ��o
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            for (int i = 1; i <= DocumentoVenda.Linhas.NumItens; i++)
            {
                if (DocumentoVenda.Linhas.GetEdita(i).TipoLinha == "10")
                {
                    DocumentoVenda.Observacoes = DocumentoVenda.Linhas.GetEdita(i).Descricao;
                    break;
                }
                else { continue; }
            }
        }

    }
}

