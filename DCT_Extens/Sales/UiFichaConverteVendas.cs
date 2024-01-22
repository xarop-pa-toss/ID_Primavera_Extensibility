using BasBE100;
using HelperFunctionsPrimavera10;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCT_Extens.Sales
{
    public class UiFichaConverteVendas : FichaConverteVendas
    {
        private List<string> _clientesQueUltrapassamLimiteList = new List<string>();
        private HelperFunctions _Helpers = new HelperFunctions(new Secrets());

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);
            _clientesQueUltrapassamLimiteList.Clear();
        }

        // AntesDeConverter activa DEPOIS do AntesDeGravar

        public override void AntesDeConverter(int NumDoc, string Tipodoc, string Serie, string Filial, string TipodocDestino, string SerieDestino, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeConverter(NumDoc, Tipodoc, Serie, Filial, TipodocDestino, SerieDestino, ref Cancel, e);

            #region Verificação de limite de crédito do cliente antes de converter um documento de venda
            string strCliente = BSO.Vendas.Documentos.DaValorAtributo(Filial, Tipodoc, Serie, NumDoc, "Entidade");
            BasBECliente cliente = BSO.Base.Clientes.Edita(strCliente);
            double valorDocOrigem = BSO.Vendas.Documentos.DaValorAtributo(Filial, Tipodoc, Serie, NumDoc, "TotalDocumento");


            // Se ultrapassar Limite de Crédito
            if (cliente.LimiteCredValor && (valorDocOrigem + cliente.DebitoContaCorrente > cliente.Limitecredito))
            {
                double valorAcimaDoLimite = cliente.Limitecredito - (valorDocOrigem + cliente.DebitoContaCorrente);

                var resultado = PSO.MensagensDialogos.MostraMensagem(
                    StdPlatBS100.StdBSTipos.TipoMsg.PRI_SimNao,
                    $"O documento {Tipodoc} {Serie}/{NumDoc} ira colocar o cliente acima do seu limite de crédito." + Environment.NewLine +
                    $"Cliente: {strCliente} - {cliente.Nome}" + Environment.NewLine +
                    $"Limite: {cliente.Limitecredito}" + Environment.NewLine +
                    $"Débito Actual: {cliente.DebitoContaCorrente}" + Environment.NewLine +
                    $"Excedente: {valorAcimaDoLimite * -1}" + Environment.NewLine + Environment.NewLine +
                    $"Deseja continuar com a conversão deste documento?",
                    StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);

                if (resultado == StdPlatBS100.StdBSTipos.ResultMsg.PRI_Sim)
                {
                    _clientesQueUltrapassamLimiteList.Add($"{strCliente}: {valorAcimaDoLimite}€ acima do limite de {cliente.Limitecredito}€\n");
                } else
                {
                    Cancel = true;
                }
            }
            #endregion
        }

        public override void DepoisDeGravar(Primavera.Platform.Collections.PrimaveraOrderedDictionary colTodosDocumentosGerados, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(colTodosDocumentosGerados, e);

            #region Verificação de limite de crédito do cliente antes de converter um documento de venda
            if (_clientesQueUltrapassamLimiteList.Any())
            {
                PSO.MensagensDialogos.MostraAviso(
                    "Os seguintes clientes ultrapassaram os seus limites de crédito.",
                    StdPlatBS100.StdBSTipos.IconId.PRI_Exclama,
                    string.Join("", _clientesQueUltrapassamLimiteList));

                _Helpers.EscreverParaFicheiroTxt("Os seguintes clientes ultrapassaram os seus limites de crédito.\n\n" + string.Join("", _clientesQueUltrapassamLimiteList), "ConversaoDocumentosVenda_ClientesUltrapassamLimite");
            }
            #endregion
        }
    }
}
