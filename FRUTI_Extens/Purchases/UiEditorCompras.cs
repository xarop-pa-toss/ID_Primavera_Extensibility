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
        private int _indArray = 0;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            List<string> tipoDocList = new List<string> { "VFA", "VFD", "VGR", "VFF", "CVS" };
            List<string> artigosComErroNoUpdateSQL = new List<string>();
            string strErro = "";

            if (PSO.MensagensDialogos.MostraPerguntaSimples("Confirma actualização de preços?") && tipoDocList.Contains(this.DocumentoCompra.Tipodoc)) {                
                
                if (!BSO.Compras.Documentos.ValidaActualizacao(this.DocumentoCompra, BSO.Compras.TabCompras.Edita(DocumentoCompra.Tipodoc),ref strErro)) {
                    PSO.MensagensDialogos.MostraAviso("Não foi possível actualizar os valores pretendidos.", StdBSTipos.IconId.PRI_Exclama, strErro);
                    Cancel = true;
                } 
                else {
                    _indArray = 0;
                    string artigoActual, codIvaArtigo;
                    double margemArtigo, novoPVP1, novoPVP4, taxaIVAArtigo, prUnit, prLiquido;
                    
                    for (int i = 1; i < DocumentoCompra.Linhas.NumItens; i++) {
                        artigoActual = DocumentoCompra.Linhas.GetEdita(i).Artigo;
                        prLiquido = DocumentoCompra.Linhas.GetEdita(i).PrecoLiquido / DocumentoCompra.Linhas.GetEdita(i).Quantidade;
                        // Se o CDU_Margem do artigo for nulo fica a zero, senão continua sem alteração.
                        margemArtigo = (BSO.Base.Artigos.DaValorAtributo(artigoActual, "CDU_Margem") == null) ? 0 : BSO.Base.Artigos.DaValorAtributo(artigoActual, "CDU_Margem");
                        codIvaArtigo = BSO.Base.Artigos.DaValorAtributo(artigoActual, "IVA");
                        taxaIVAArtigo = BSO.Base.Iva.DaValorAtributo(codIvaArtigo, "Taxa");

                        if (margemArtigo != 0) {
                            _indArray++;
                            novoPVP4 = prLiquido + (prLiquido * margemArtigo / 100);
                            novoPVP1 = novoPVP4 + (novoPVP4 + taxaIVAArtigo / 100);

                            StdBEExecSql sql = new StdBEExecSql();
                            sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                            sql.Tabela = "Artigomoeda";
                            sql.AddCampo("PVP4", novoPVP4.ToString().Replace(",", "."));
                            sql.AddCampo("PVP1", novoPVP1.ToString().Replace(",", "."));
                            sql.AddCampo("artigo", artigoActual, true);

                            sql.AddQuery();

                            // Se Update falhar, preenche lista com NumDoc para mostrar ao cliente.
                            BSO.IniciaTransaccao();
                            try {
                                PSO.ExecSql.Executa(sql);
                            }
                            catch {
                                artigosComErroNoUpdateSQL.Add(artigoActual);
                                BSO.DesfazTransaccao();
                            }
                            BSO.TerminaTransaccao();
                        }
                    }

                    if (_indArray == 0) { return; }

                    BSO.IniciaTransaccao();


                }
            }
            base.AntesDeGravar(ref Cancel, e);

        }

    }
}
