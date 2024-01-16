using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using System.Data;
using System.Linq;
using HelperFunctionsPrimavera10;
using System;
using StdBE100;
using System.Collections.Generic;

namespace DCT_Extens
{
    public class UiFichaClientes : FichaClientes
    {
        private HelperFunctions _Helpers = new HelperFunctions(new Secrets());
        private string vendedorOriginal;

        public override void AntesDeEditar(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeEditar(Cliente, ref Cancel, e);
            vendedorOriginal = this.Cliente.Vendedor;
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (vendedorOriginal != this.Cliente.Vendedor)
            {
                bool resposta = PSO.MensagensDialogos.MostraPerguntaSimples(
                    "O vendedor atribuído a este cliente foi alterado." + Environment.NewLine +
                    "Deseja transferir o histórico do cliente?");

                if (resposta)
                {
                    // Têm de ser alterados todos os campos RespCobranca(CabecDoc) e Vendedor(LinhasDoc) para o novo vendedor
                    // 1 - Get IDs dos CabecDoc com o vendedor actual.
                    // 2 - Set RespCobranca(CabecDoc.ID = ID) e Vendedor(LinhasDoc.IDCabecDoc = ID) para vendedor novo

                    // 1
                    DataTable cabecDocsTabela = _Helpers.GetDataTableDeSQL(
                        " SELECT Id" +
                        " FROM CabecDoc" +
                        $" WHERE RespCobranca = '{vendedorOriginal}'");

                    List<string> cabecDocsList = cabecDocsTabela.AsEnumerable().Select(x => x[0].ToString()).ToList();

                    // 3
                    StdBEExecSql sql = new StdBEExecSql();
                    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                    sql.Tabela = "CabecDoc";
                    sql.AddCampo("RespCobranca", this.Cliente.Vendedor);
                    sql.AddCampo("Id", cabecDocsList, true);
                    PSO.ExecSql.Executa(sql);

                    sql = new StdBEExecSql();
                    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                    sql.Tabela = "LinhasDoc";
                    sql.AddCampo("Vendedor", this.Cliente.Vendedor);
                    sql.AddCampo("IdCabecDoc", cabecDocsList, true);
                    PSO.ExecSql.Executa(sql);
                }
            }

# region VERIFICAÇÃO DE PERMISSÕES PARA ANULAÇÃO DE CLIENTE
            DataTable TDU = _Helpers.GetDataTableDeSQL("SELECT CDU_Utilizador FROM TDU_PermissaoAnularClientes");

            string userActual = BSO.Contexto.UtilizadorActual;
            var autorizacao = from DataRow linha in TDU.Rows
                              where (string)linha["CDU_Utilizador"] == userActual
                              select (string)linha["CDU_Utilizador"];

            // Se o utilizador actual não tiver permissão, a variavel 'autorizacao' é uma lista vazia.
            if (!autorizacao.Any())
            {
                PSO.MensagensDialogos.MostraAviso("Não tem permissão para alterar o estado Anulado de um cliente. \n Este registo não será gravado.", StdPlatBS100.StdBSTipos.IconId.PRI_Critico);
                Cancel = true;
            }
#endregion
        }
    }
}