using Primavera.Extensibility.Purchases.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdBE100;
using StdPlatBS100;
using ErpBS100;
using BasBE100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;

namespace FRUTI_Extens.Purchases
{
    public class UiEditorCompras : EditorCompras
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            int _indArray = 0;
            List<string> tipoDocList = new List<string> { "VFA", "VFD", "VGR", "VFF", "CVS" };
            List<string> artigosComErroNoUpdateSQL = new List<string>();
            string strErro = "";

            if (PSO.MensagensDialogos.MostraPerguntaSimples("Confirma actualiza��o de pre�os?") && tipoDocList.Contains(this.DocumentoCompra.Tipodoc)) {                
                
                if (!BSO.Compras.Documentos.ValidaActualizacao(this.DocumentoCompra, BSO.Compras.TabCompras.Edita(DocumentoCompra.Tipodoc),ref strErro)) {
                    PSO.MensagensDialogos.MostraAviso("N�o foi poss�vel actualizar os valores pretendidos.", StdBSTipos.IconId.PRI_Exclama, strErro);
                    Cancel = true;
                } 
                else {
                    _indArray = 0;
                    string artigoActual, codIvaArtigo, novoPVP1str, novoPVP4str;
                    double margemArtigo, novoPVP1, novoPVP4, taxaIVAArtigo, prUnit, prLiquido;
                    
                    for (int i = 1; i < DocumentoCompra.Linhas.NumItens + 1; i++) {
                        artigoActual = DocumentoCompra.Linhas.GetEdita(i).Artigo;
                        prLiquido = DocumentoCompra.Linhas.GetEdita(i).PrecoLiquido / DocumentoCompra.Linhas.GetEdita(i).Quantidade;
                        // Se o CDU_Margem do artigo for nulo fica a zero, sen�o continua sem altera��o.
                        margemArtigo = (BSO.Base.Artigos.DaValorAtributo(artigoActual, "CDU_Margem") == null) ? 0 : Convert.ToDouble(BSO.Base.Artigos.DaValorAtributo(artigoActual, "CDU_Margem"));
                        codIvaArtigo = BSO.Base.Artigos.DaValorAtributo(artigoActual, "IVA");
                        taxaIVAArtigo = BSO.Base.Iva.DaValorAtributo(codIvaArtigo, "Taxa");

                        if (margemArtigo != 0) {
                            _indArray++;
                            novoPVP4 = prLiquido + (prLiquido * margemArtigo / 100);
                            novoPVP1 = novoPVP4 + (novoPVP4 * (taxaIVAArtigo / 100));

                            novoPVP4str = novoPVP4.ToString().Replace(",", ".");
                            novoPVP1str = novoPVP1.ToString().Replace(",", ".");

                            #region M�todo de Update 1 - com connection string tradicional.
                            Motor.DBUpdate sql = new Motor.DBUpdate();
                            sql.ExecutarUpdate($"UPDATE Artigomoeda " +
                                $"SET PVP4 = {novoPVP4str} AND PVP1 = {novoPVP1str} " +
                                $"WHERE artigo = {artigoActual}");
                            #endregion
                            #region M�todo de Update 2 - Pelo m�todo Primavera (com bug)
                            //StdBEExecSql sql = new StdBEExecSql();
                            //sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                            //sql.Tabela = "Artigomoeda";
                            //sql.AddCampo("PVP4", novoPVP4.ToString().Replace(",", "."));
                            //sql.AddCampo("PVP1", novoPVP1.ToString().Replace(",", "."));
                            //sql.AddCampo("artigo", artigoActual, true);

                            //sql.AddQuery();

                            //// Se Update falhar, preenche lista com NumDoc para mostrar ao cliente.
                            //BSO.IniciaTransaccao();
                            //try {
                            //    PSO.ExecSql.Executa(sql);
                            //}
                            //catch {
                            //    artigosComErroNoUpdateSQL.Add(artigoActual);
                            //    BSO.DesfazTransaccao();
                            //}
                            //sql.Dispose();
                            //BSO.TerminaTransaccao();
                            #endregion
                        }
                    }

                    if (_indArray == 0) { return; }

                    if (BSO.EmTransaccao()) { BSO.DesfazTransaccao(); }
                }
            }
        }
    }
}
