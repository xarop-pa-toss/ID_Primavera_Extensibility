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

namespace DCT_Extens.Internal
{
    public class UiEditorInternos : EditorInternos
    {
        private bool _apagaPai;
        private HelperFunctions _Helpers = new HelperFunctions();
        private DataTable _tabelaOperadores, _tabelaSeries;

        public override void TipoDocumentoIdentificado(string TipoDocumento, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.TipoDocumentoIdentificado(TipoDocumento, ref Cancel, e);

            // Carregar "arrTDU" e "arrSerie". Tem de ser feito pois TipoDocumentoIdentificado não dá série e não existe override para Série identificada
            _tabelaOperadores = _Helpers.GetTabela("SELECT * FROM TDU_OperadorQuebra;");
            _tabelaSeries = _Helpers.GetTabela($"" +
                $" SELECT TipoDoc, Serie, CDU_PedeOperador_Motivo, CDU_PedeOperador_Operador " +
                $" FROM SeriesStocks " +
                $" WHERE " +
                $"  DataInicial >= '2022-01-01' " +
                $"   AND (CDU_PedeOperador_Motivo = 1 OR CDU_PedeOperador_Operador = 1)" +
                $"   AND TipoDoc = '{TipoDocumento}'" +
                $"   AND Serie = '{};");

            
            bool GetCDU_PedeOperadorPorSerie = false;
            foreach (DataRow linha in _tabelaSeries.Rows)
            {



    //            For i = LBound(arrSerie, 2) To UBound(arrSerie, 2)
    //    If arrSerie(0, i) = TipoDoc And arrSerie(1, i) = Serie And(arrSerie(2, i) = True Or arrSerie(3, i) = True) Then
    //        pedeMotivo = arrSerie(2, i)
    //        pedeOperador = arrSerie(3, i)
    //        ValidarSerie = True
    //        Exit For
    //    End If
    //Next i
            }

            serieValida = ValidarSerie(Me.DocumentoStock.TipoDoc, Me.DocumentoStock.Serie)

        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            _apagaPai = false;
            
            if (Helpers.HelperFunctions.LinhasCopiadas)
            {

            }


        }
    }
}
