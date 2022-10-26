using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MDL_Obs.Sales
{
    public class UiEditorVendas : EditorVendas
    {
        string descricao { get; set; }
        string numDoc { get; set; }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
             if (DocumentoVenda.Tipodoc == "FAPO" || DocumentoVenda.Tipodoc == "NCPO")
             {
                numDoc = Convert.ToString(DocumentoVenda.NumDoc);
                for (int i = 1; i <= DocumentoVenda.Linhas.NumItens; i++)
                {
                    descricao = DocumentoVenda.Linhas.GetEdita(i).Descricao;
                    if (descricao != "")
                    {
                        DocumentoVenda.Observacoes = descricao;
                        break;
                    }
                    else { continue; }
                }
             }
                base.AntesDeGravar(ref Cancel, e);
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            if (DocumentoVenda.Tipodoc == "FAPO" || DocumentoVenda.Tipodoc == "NCPO")
            {
                // Terceiro argumento define se o campo é uma condição (true = WHERE) ou um campo onde definir dados (false)
                StdBEExecSql ligacaoBD = new StdBEExecSql();
                ligacaoBD.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                ligacaoBD.Tabela = "Historico";
                ligacaoBD.AddCampo("Texto", descricao, false, StdBETipos.EnumTipoCampoSimplificado.tsTexto);
                ligacaoBD.AddCampo("NumDoc", NumDoc, true, StdBETipos.EnumTipoCampoSimplificado.tsTexto);
                ligacaoBD.AddCampo("TipoDoc", Tipo, true, StdBETipos.EnumTipoCampoSimplificado.tsTexto);
                ligacaoBD.AddCampo("Serie", Serie, true, StdBETipos.EnumTipoCampoSimplificado.tsTexto);

                PSO.ExecSql.Executa(ligacaoBD);
            }

            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);
        }
    }
}
