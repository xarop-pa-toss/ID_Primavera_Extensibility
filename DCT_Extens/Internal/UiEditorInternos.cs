using Primavera.Extensibility.Internal.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100; using VndBE100; using ErpBS100; using StdBE100; using BasBE100;
using DCT_Extens.Helpers;
using System.Data;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using IntBE100;
using DCT_Extens.StockQuebras;

namespace DCT_Extens.Internal
{
    public class UiEditorInternos : EditorInternos
    {
        private HelperFunctions _Helpers = new HelperFunctions();
        private DataTable _tabelaOperadores, _tabelaSeries;
        internal FormStockQuebras _form = new FormStockQuebras();
        private bool _apagaPai, _apagaLinha, _serieValida;
        internal bool _deveAbrirFormStockQuebras;

        public override void TipoDocumentoIdentificado(string TipoDocumento, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.TipoDocumentoIdentificado(TipoDocumento, ref Cancel, e);

            // Carregar TDUs OperadorQuebras e séries com CDU_StockQuebras a true.
            // Tem de ser feito pois TipoDocumentoIdentificado não dá série e não existe override para Série identificada.
            _tabelaOperadores = _Helpers.GetTabela("SELECT * FROM TDU_OperadorQuebra;");

            _tabelaSeries = _Helpers.GetTabela($"" +
                $" SELECT Serie, CDU_PedeOperador_Operador, CDU_PedeOperador_Motivo " +
                $" FROM SeriesStocks " +
                $" WHERE " +
                $"  DataInicial >= '2022-01-01' " +
                $"   AND (CDU_PedeOperador_Motivo = 1 OR CDU_PedeOperador_Operador = 1)" +
                $"   AND TipoDoc = '{TipoDocumento}'" +
                $"   AND Serie = '{DocumentoInterno.Serie}");

            _serieValida = _tabelaSeries.Rows.Count > 0;
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            IntBELinhaDocumentoInterno linha = DocumentoInterno.Linhas.GetEdita(NumLinha);

            if (_serieValida)
            {
                if (linha.TipoLinha.Equals("10"))
                {
                    var operadoresValidos = from DataRow row in _tabelaOperadores.Rows
                                            where (string)row["CDU_Armazem"] == linha.Armazem
                                            select row["CDU_Operador"];

                    // Se o user tiver picado a checkbox para repetir o motivo para todas as linhas, não vale a pena abrir o form
                    // Mas deve sempre abrir na primeira linha claro
                    if (NumLinha.Equals(1) || _deveAbrirFormStockQuebras == true)
                    {
                        _form.ShowDialog();

                        if (_form.DialogResult == DialogResult.OK)
                        {
                            if (_form.GetCheckBox_RepetirMotivo) { _deveAbrirFormStockQuebras = false; }
                        }
                    }




                }
            }
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            _apagaPai = false;

            if (HelperFunctions.LinhasCopiadas)
            {

            }
        }
    }
}
