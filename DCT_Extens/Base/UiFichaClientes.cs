﻿using BasBE100;
using HelpersPrimavera10;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using System;
using System.Data;
using System.Drawing.Printing;
using System.Linq;

namespace DCT_Extens
{
    public class UiFichaClientes : FichaClientes
    {
        private HelperFunctions _Helpers = new HelperFunctions();
        private string vendedorOriginal;
        private string _estadoInicialCredito;

        public override void AntesDeCriar(ExtensibilityEventArgs e)
        {
            base.AntesDeCriar(e);
            _estadoInicialCredito = null;
        }

        public override void AntesDeEditar(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeEditar(Cliente, ref Cancel, e);
            vendedorOriginal = this.Cliente.Vendedor;

            // Corresponde aos dois radio buttons do TipoCredito.
            // "1" é Por Limite e "2" é Suspenso
            _estadoInicialCredito = BSO.Base.Clientes.Edita(Cliente).TipoCredito;
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            #region Validação Segmento-Terceiro
            if (string.IsNullOrEmpty(Cliente.SegmentoTerceiro)) { Cliente.SegmentoTerceiro = "001"; }
            #endregion

            #region Verificação de permissões de utilizador para alteração do TipoCredito (Por Limite (1) ou Suspenso (2))
            // Pode ser nulo se a ficha de cliente estiver limpa (cliente novo); deixa de ser nulo quando o cliente é gravado.
            if (!string.IsNullOrEmpty(_estadoInicialCredito) && _estadoInicialCredito != this.Cliente.TipoCredito)
            {
                DataTable TDU = _Helpers.GetDataTableDeSQL("SELECT CDU_Utilizador FROM TDU_PermissaoAnularClientes");

                string userActual = BSO.Contexto.UtilizadorActual;
                var autorizacao = from DataRow linha in TDU.Rows
                                  where (string)linha["CDU_Utilizador"] == userActual
                                  select (string)linha["CDU_Utilizador"];

                // Se o utilizador actual não tiver permissão, a variavel 'autorizacao' é uma lista vazia.
                if (!autorizacao.Any())
                {
                    PSO.MensagensDialogos.MostraAviso("Não tem permissão para alterar o tipo de crédito de um cliente. \n Este registo não será gravado.", StdPlatBS100.StdBSTipos.IconId.PRI_Critico);
                    Cancel = true;
                }
            }
            #endregion
        }

        public override void DepoisDeGravar(string Cliente, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Cliente, e);
            // *** CANCELADO A PEDIDO DO CLIENTE ***
            //#region Transferir histórico de cliente quando é alterado o Responsável de Cobrança (bendedor).
            //if (vendedorOriginal != this.Cliente.Vendedor)
            //{
            //    bool resposta = PSO.MensagensDialogos.MostraPerguntaSimples(
            //        "O vendedor atribuído a este cliente foi alterado." + Environment.NewLine +
            //        "Deseja transferir o histórico do cliente?");

            //    if (resposta)
            //    {
            //        // Têm de ser alterados todos os campos RespCobranca(CabecDoc) e Vendedor(LinhasDoc) para o novo vendedor.
            //        // 1 - Query com os IdCabecDoc que têm o vendedor original
            //        // 2 - Set do novo vendedor nas linhas das tabelas. Podemos encontrar as linhas apenas com o IdCabecDoc
            //        //   CabecDoc pelo campo Id -> RespCobranca
            //        //   LinhasDoc pelo campo IdCabecDoc -> Vendedor
            //        //   Historico pelo campo IdDoc -> RespCobranca e Vendedor

            //        // 1
            //        string idCabecDocQuery =
            //            " SELECT Id" +
            //            " FROM CabecDoc" +
            //            $" WHERE Entidade = '{this.Cliente.Cliente}'";

            //        // 2
            //        _Helpers.QuerySQL(
            //            " UPDATE CabecDoc" +
            //           $" SET RespCobranca = '{this.Cliente.Vendedor}'" +
            //           $" WHERE Id IN ({idCabecDocQuery});");

            //        _Helpers.QuerySQL(
            //            " UPDATE LinhasDoc" +
            //           $" SET Vendedor = '{this.Cliente.Vendedor}'" +
            //           $" WHERE IdCabecDoc IN ({idCabecDocQuery});");

            //        _Helpers.QuerySQL(
            //            " UPDATE Historico" +
            //           $" SET RespCobranca = '{this.Cliente.Vendedor}', Vendedor = '{this.Cliente.Vendedor}'" +
            //           $" WHERE IdDoc IN ({idCabecDocQuery});");
            //    }
            //}
            //#endregion
        }
    }
}