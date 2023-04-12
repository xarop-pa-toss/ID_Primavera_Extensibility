using Primavera.Extensibility.PayablesReceivables.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdBE100;


namespace FRUP_AvisoVencimento.PayablesReceivables
{
    public class UiEditorPendentes : EditorPendentes
    { 
        public void ResetAvisoVencimento()
        {
            try
            {
                StdBEExecSql sql = new StdBEExecSql();
                sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                sql.Tabela = "Pendentes";
                sql.AddCampo("NumAvisos", 0);
                sql.AddQuery();
                PSO.ExecSql.Executa(sql);
                sql = null;

                PSO.MensagensDialogos.MostraMensagem(StdPlatBS100.StdBSTipos.TipoMsg.PRI_SimplesOk, "Comando corrido com sucesso. Número de Avisos alterado para 0.", StdPlatBS100.StdBSTipos.IconId.PRI_Informativo);
            }
            catch (Exception e)
            {
                PSO.MensagensDialogos.MostraAviso("Não foi possivel correr o comando. Número de Avisos não foram alterados.", sDetalhe: e.ToString());
            }
        }
    }
}
