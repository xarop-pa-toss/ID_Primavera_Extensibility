using Primavera.Extensibility.TechnicalServices.Editors; using Primavera.Extensibility.BusinessEntities;
using System; using System.Collections.Generic;  using System.Linq; using System.Text; using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StpBE100; using StdBE100; using BasBE100;


namespace ID_ServicosTecnicos.TechnicalServices
{
    public class UiEditorStpPedidos : EditorStpPedidos
    {
        public override void AntesDeEditarLinha(StpBELinhaPedido linha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            string clienteID = this.Pedido.Cliente;
            var CDU = BSO.Base.Clientes.Consulta(clienteID).CamposUtil["CDU_CTRSUSPENSO"].Valor;

            if (CDU.Equals(true))
            {
                PSO.Dialogos.MostraMensagem(StdPlatBS100.StdBSTipos.TipoMsg.PRI_SimplesOk,"Este cliente tem as assistências suspensas!",StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);
            }

            base.AntesDeEditarLinha(linha, ref Cancel, e);
        }
    }
}