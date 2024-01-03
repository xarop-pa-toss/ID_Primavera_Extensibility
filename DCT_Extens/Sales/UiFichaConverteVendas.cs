using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100; using ErpBS100; using VndBE100; using CctBE100;
using BasBE100;
using System.Windows.Forms;
using DCT_Extens.Helpers;

namespace DCT_Extens.Sales
{
    public class UiFichaConverteVendas : FichaConverteVendas
    {
        private Dictionary<string, double> _clientesTotalDocs = new Dictionary<string, double>();
        private HelperFunctions _Helpers = new HelperFunctions();

        // AntesDeConverter activa DEPOIS do AntesDeGravar

        public override void AntesDeConverter(int NumDoc, string Tipodoc, string Serie, string Filial, string TipodocDestino, string SerieDestino, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeConverter(NumDoc, Tipodoc, Serie, Filial, TipodocDestino, SerieDestino, ref Cancel, e);

            double valorDocOrigem = BSO.Vendas.Documentos.DaValorAtributo(Filial, Tipodoc, Serie, NumDoc, "TotalDocumento");
            string strCliente = BSO.Vendas.Documentos.DaValorAtributo(Filial, Tipodoc, Serie, NumDoc, "Entidade");

            // Dicion�rio mant�m valor total de todos os documentos a converter para cada cliente.
            // Se j� tiver sido convertido um doc para o cliente, apenas actualiza o valor. Ser� usado no antes de gravar para validar se ultrapassa limite de cr�dito.
            if (!_clientesTotalDocs.ContainsKey(strCliente))
            {
                _clientesTotalDocs.Add(strCliente, valorDocOrigem);
            } else
            {
                _clientesTotalDocs[strCliente] += valorDocOrigem;
            }

            //// Adicionar � lista de documentos que v�o ultrapassar o limite de cr�dito do cliente
            //if (cliente.LimiteCredValor)
            //{
            //    // Se o valor do documento + o que o cliente tem em d�vida ultrapassar o limite estabelecido
            //    if (cliente.DebitoContaCorrente + valorDocOrigem > cliente.Limitecredito)
            //    {
            //        _docsQueUltrapassamLimiteCredito.Add($"clienteStr {Tipodoc} {Serie}/{NumDoc} - {valorDocOrigem}�\n");
            //    }
            //}
        }

        public override void DepoisDeConverter(Primavera.Platform.Collections.PrimaveraOrderedDictionary colDocumentosGerados, ExtensibilityEventArgs e)
        {
            base.DepoisDeConverter(colDocumentosGerados, e);
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            List<string> clientesQueUltrapassamLimiteCredito = new List<string>();

            foreach (KeyValuePair<string, double> kvp in _clientesTotalDocs)
            {
                BasBECliente cliente = BSO.Base.Clientes.Edita(kvp.Key);

                // Se cliente tiver activo o tipo de cr�dito com Limite de Cr�dito por Valor + o total dos seus documentos a converter ultrapassar o limite, adiciona este cliente � lista a mostrar ao utilizador
                if (cliente.LimiteCredValor && (cliente.DebitoContaCorrente + kvp.Value > cliente.Limitecredito))
                {
                    double valorAcimaDoLimiteCredito = cliente.Limitecredito - kvp.Value;

                    clientesQueUltrapassamLimiteCredito.Add($"{kvp.Key}: {valorAcimaDoLimiteCredito}� acima do limite {cliente.Limitecredito}�\n");
                }
            }

            if (clientesQueUltrapassamLimiteCredito.Any())
            {
                DialogResult resultado = MessageBox.Show(
                    "Existem clientes que ir�o ficar acima dos seus limites de cr�dito caso decida avan�ar com a convers�o dos documentos. \n\nDeseja proceder com a convers�o?",
                    "Limites de Cr�dito",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                string listaFinalStr = string.Join("", clientesQueUltrapassamLimiteCredito);
                if (resultado == DialogResult.Yes)
                {
                    _Helpers.EscreverParaFicheiroTxt(listaFinalStr, "ConversaoDocumentosVenda_gravado");
                } else
                {
                    _Helpers.EscreverParaFicheiroTxt(listaFinalStr, "ConversaoDocumentosVenda_nao_gravado");
                    PSO.MensagensDialogos.MostraAviso("Nenhum documento foi convertido.\n Ver detalhes para mais informa��es.", StdPlatBS100.StdBSTipos.IconId.PRI_Exclama, listaFinalStr);
                }
            }
        }

        public override void DepoisDeGravar(Primavera.Platform.Collections.PrimaveraOrderedDictionary colTodosDocumentosGerados, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(colTodosDocumentosGerados, e);
        }
    }
}
