using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.Base.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;


namespace DCT_Extens
{
    public class UiFichaClientes : FichaClientes
    {
        private Helpers.HelperFunctions _Helpers = new Helpers.HelperFunctions();

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            DataTable TDU = _Helpers.GetDataTableDeSQL("SELECT CDU_Utilizador FROM TDU_PermissaoAnularClientes");

            string userActual = BSO.Contexto.UtilizadorActual;
            var autorizacao = from DataRow linha in TDU.Rows
                              where (string)linha["CDU_Utilizador"] == userActual
                              select (string)linha["CDU_Utilizador"];

            // Se o utilizador actual não tiver permissão, a variavel 'autorizacao' é uma lista vazia.
            if (!autorizacao.Any())
            {
                PSO.MensagensDialogos.MostraAviso("Não tem permissão para alterar o estado Anulado de um cliente. \n O documento não será gravado.", StdPlatBS100.StdBSTipos.IconId.PRI_Critico);
                Cancel = true;
            }
        }
    }
}
