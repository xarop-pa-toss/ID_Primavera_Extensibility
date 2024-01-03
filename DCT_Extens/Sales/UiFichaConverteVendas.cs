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

namespace DCT_Extens.Sales
{
    public class UiFichaConverteVendas : FichaConverteVendas
    {
        public override void AntesDeConverter(int NumDoc, string Tipodoc, string Serie, string Filial, string TipodocDestino, string SerieDestino, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeConverter(NumDoc, Tipodoc, Serie, Filial, TipodocDestino, SerieDestino, ref Cancel, e);

            double valorDocOrigem = BSO.Vendas.Documentos.DaValorAtributo(Filial, Tipodoc, Serie, NumDoc, "TotalDocumento");
            List<string> docsQueUltrapassamLimiteCredito = new List<string>();
            
            string clienteStr = BSO.Vendas.Documentos.DaValorAtributo(Filial, Tipodoc, Serie, NumDoc, "Entidade");
            BasBECliente cliente = BSO.Base.Clientes.Edita(clienteStr);

            
            // Adicionar � lista de documentos que v�o ultrapassar o limite de cr�dito do cliente
            if (cliente.LimiteCredValor)
            {

                // Se o valor do documento + o que o cliente tem em d�vida ultrapassar o limite estabelecido
                if (cliente.DebitoContaCorrente + valorDocOrigem > cliente.Limitecredito)
                {
                    docsQueUltrapassamLimiteCredito.Add($"clienteStr {Tipodoc} {Serie}/{NumDoc} - {valorDocOrigem}�\n");
                }
            }

            
            // Se a lista n�o estiver vazia, mostrar ao cliente uma listagem dos documentos problem�ticos e dar a op��o de cancelar todas as convers�es.
            // Escreve tamb�m para um ficheiro .txt
            if (docsQueUltrapassamLimiteCredito.Any())
            {
                DialogResult resultado = MessageBox.Show("Existem documentos que, se convertidos, ir�o ultrapassar o Limite de Cr�dito de algum(uns) cliente(s)." +
                    "\n Deseja cancelar a convers�o dos documentos?", "Limite de cr�dito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    Cancel = true;
                } 
            }
        }
    }
}
