using Primavera.Extensibility.Internal.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using System.Data;
using System.Windows.Forms;
using IntBE100;
using Primavera.Extensibility.Extensions;
using HelperFunctionsPrimavera10;
using Primavera.Extensibility.Production.Delegates;

namespace DCT_Extens.Internal
{
    public class UiEditorInternos : EditorInternos
    {
        private HelperFunctions _Helpers = new HelperFunctions(new Secrets());
        private DataTable _tabelaOperadores, _tabelaSerie;

        private bool _deveRepetir = false;
        private Dictionary<string, string> _repetirDict = new Dictionary<string, string>
        {
            { "operador","" },
            { "motivo", "" }
        };

        public override void TipoDocumentoIdentificado(string TipoDocumento, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.TipoDocumentoIdentificado(TipoDocumento, ref Cancel, e);
            LimparRepetirVars();

            // Carregar TDUs OperadorQuebras e séries com CDU_StockQuebras a true.
            // Tem de ser feito pois TipoDocumentoIdentificado não dá série e não existe override para Série identificada.
            _tabelaOperadores = _Helpers.GetDataTableDeSQL("SELECT * FROM TDU_OperadorQuebra;");

            _tabelaSerie = _Helpers.GetDataTableDeSQL($"" +
                $" SELECT Serie, CDU_PedeOperador_Operador, CDU_PedeOperador_Motivo " +
                $" FROM SeriesStocks " +
                $" WHERE " +
                $"  DataInicial >= '2022-01-01' " +
                $"  AND (CDU_PedeOperador_Motivo = 1 OR CDU_PedeOperador_Operador = 1)" +
                $"  AND TipoDoc = '{TipoDocumento}'" +      
                $"  AND Serie = '{DocumentoInterno.Serie}'");
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            IntBELinhaDocumentoInterno linha = DocumentoInterno.Linhas.GetEdita(NumLinha);

            // Se série for válida, só terá uma linha contendo essa série && Se linha for de artigo
            if (_tabelaSerie.Rows.Count.Equals(1) && linha.TipoLinha.Equals("10"))
            {
                var operadoresValidos = from DataRow row in _tabelaOperadores.Rows
                                        where (string)row["CDU_Armazem"] == linha.Armazem
                                        select row["CDU_OperadorQuebra"];

                // Converter operadoresValidos de IEnumerable(object) para List<string>
                List<string> operadoresValidosList = operadoresValidos.OfType<string>().ToList();

                // Se o user tiver picado a checkbox para repetir o motivo para todas as linhas, não vale a pena abrir o form
                // Mas deve sempre abrir na primeira linha claro
                if (NumLinha.Equals(1) || !_deveRepetir)
                {
                    // Form instanciado aqui para que seja um objecto limpo para cada ArtigoIdentificado independentemente se o anterior foi gravado ou não
                    // Recebe os Operadores a listar na combo box e qual o estado dos controlos (campos Motivo e Operador da _tabelaSeries - ver query acima).
                    using (var formInstancia = BSO.Extensibility.CreateCustomFormInstance(typeof(FormStockQuebras)))
                    {
                        if (formInstancia.IsSuccess())
                        {
                            FormStockQuebras formStockQuebras = formInstancia.Result as FormStockQuebras;
                            // Para conseguir usar algumas variáveis dentro do form é necessário enviar após a inicialização pois não é possível cria-lo directamente com argumentos... -_-'
                            formStockQuebras.SetVariaveis(operadoresValidosList, _tabelaSerie.Rows[0]);
                            DialogResult resultado = formStockQuebras.ShowDialog();

                            // Verificações de regras de preenchimento dos dados são feitas dentro do Form e não aqui.
                            if (resultado == DialogResult.OK)
                            {
                                // Impede o form de se abrir outra vez se a checkbox estiver picada. Guarda os valores para usar nas próximas linhas
                                if (formStockQuebras.GetCheckBox_RepetirMotivo) {
                                    _deveRepetir = true;
                                    _repetirDict["operador"] = formStockQuebras.GetCmbBox_Operador;
                                    _repetirDict["motivo"] = formStockQuebras.GetTxtBox_MotivoQuebra;
                                }
                                linha.CamposUtil["CDU_OperadorQuebra"].Valor = formStockQuebras.GetCmbBox_Operador;
                                linha.CamposUtil["CDU_MotivoQuebra"].Valor = formStockQuebras.GetTxtBox_MotivoQuebra;
                            }

                            if (resultado == DialogResult.Cancel)
                            {
                                _Helpers.ApagaLinhasFilhoEPai_docInterno(DocumentoInterno, linha);
                            }
                        }
                    }
                }
                else if (_deveRepetir)
                {
                    linha.CamposUtil["CDU_OperadorQuebra"].Valor = _repetirDict["operador"];
                    linha.CamposUtil["CDU_MotivoQuebra"].Valor = _repetirDict["motivo"];
                }
            }
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (GS1_Geral.LinhasCopiadas)
            {
                // É melhor alterar a variavel de estado assim que possivel para evitar esquecimento mais tarde.
                GS1_Geral.LinhasCopiadas = false;

                IntBELinhasDocumentoInterno linhas = DocumentoInterno.Linhas;
                if (linhas.NumItens == 0) { }

            }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            LimparRepetirVars();
        }

        public override void EntidadeIdentificada(string TipoEntidade, string Entidade, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.EntidadeIdentificada(TipoEntidade, Entidade, ref Cancel, e);

            LimparRepetirVars();
        }

        private void LimparRepetirVars()
        {
            _deveRepetir = false;
            foreach (var key in _repetirDict.Keys.ToList())
            {
                _repetirDict[key] = null;
            }
        }
    }
}
