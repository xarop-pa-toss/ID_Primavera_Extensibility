using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRISDK100;


namespace FRU_AlterarTerceiros
{

    public partial class FormAlterarTerceiros : CustomForm
    {
        //Variavél global que contem o contexto e que deverá ser passada para os controlos.
        private clsSDKContexto _sdkContexto;
        private string _tipoDoc, _serie;
        private long _numDoc;

        public FormAlterarTerceiros()
        {
            InitializeComponent();
        }

        private void FormAlterarTerceiros_Load(object sender, EventArgs e)
        {
            InicializaSDKContexto();
            f4Terceiros.Inicializa(_sdkContexto);
            f4TipoDoc.Inicializa(_sdkContexto);

            // Fill TipoDoc combobox
            string query = "SELECT Documento + ' - ' + Descricao AS Doc FROM DocumentosVenda WHERE Inactivo = 0 ORDER BY Documento DESC;";
            cboxTipoDoc.Items.Clear();
            cboxTipoDoc.Items.AddRange(FillComboBox(query).ToArray());


        }
        //Função que inicializa o contexto SDK.
        private void InicializaSDKContexto()
        {
            if (_sdkContexto == null)
            {
                _sdkContexto = new clsSDKContexto();
                //Inicializaçao do contexto SDK a partir do objeto BSO e respetivo módulo.
                _sdkContexto.Inicializa(BSO, "ERP");
                //Inicialização da plataforma no contexto e verificação de assinatura digital.
                PSO.InicializaPlataforma(_sdkContexto);
            }
        }

        // Retorna null se query vazia.
        private List<string> FillComboBox(string query)
        {
            using(StdBE100.StdBELista priLista = BSO.Consulta(query))
            {
                List<string> listaFinal = new List<string>();

                if (!priLista.Vazia())
                {
                    priLista.Inicio();
                    while (!priLista.NoFim())
                    {
                        listaFinal.Add(priLista.Valor(0));
                        priLista.Seguinte();
                    }
                    priLista.Termina();

                    return listaFinal;
                }
                return null;
            }
        }

        private void f4TipoDoc_TextChange(object Sender, F4.TextChangeEventArgs e)
        {
            if (f4TipoDoc.Text != "")
            {
                string query = "SELECT DISTINCT Serie FROM SeriesVendas WHERE TipoDoc = '" + f4TipoDoc.Text + "' ORDER BY Serie DESC;";
                cboxSerie.Items.Clear();
                cboxSerie.Items.AddRange(FillComboBox(query).ToArray());
                cboxSerie.SelectedIndex = 0;
            }
        }

        private void btnAlterarTerceiro_Click(object sender, EventArgs e)
        {
            // Check controlos
            Dictionary<string, string> valoresControlos = GetControlos();

            if (CheckControlos(valoresControlos))
            {
                string strErros;
                using (StdBE100.StdBEExecSql sql = new StdBE100.StdBEExecSql())
                {
                    sql.tpQuery = StdBE100.StdBETipos.EnumTpQuery.tpUPDATE;
                    sql.Tabela = "CabecDoc";                                                                                                                // UPDATE CabecDoc
                    sql.AddCampo("TipoTerceiro", valoresControlos["Terceiro"]);                                                                             // SET TipoTerceiro = ...
                    sql.AddCampo("Tipodoc", valoresControlos["TipoDoc"], true, StdBE100.StdBETipos.EnumTipoCampoSimplificado.tsTexto);                      // WHERE TipoDoc = ...
                    sql.AddCampo("Serie", valoresControlos["Serie"], true, StdBE100.StdBETipos.EnumTipoCampoSimplificado.tsTexto);                          // AND ...
                    sql.AddCampo("NumDoc", Convert.ToInt32(valoresControlos["NumDoc"]), true, StdBE100.StdBETipos.EnumTipoCampoSimplificado.tsInteiro);     // AND ...

                    sql.AddQuery();
                    PSO.ExecSql.Executa(sql);
                }
            }

            //É necessário criar código no Primavera V10 que está Frupor para a empresa ADEGA para fazer o seguinte, poder alterar o tipo de terceiro nos documentos de venda. Para tal é necessário o utilizador introduzir os seguintes campos:
            //-Tipo de documento
            //-Série do documento
            //- Nº de documento
            //Depois poder colocar o tipo de terceiro em tabela.

            // update cabecdoc
            // set tipoterceiro =´005´
            //where tipodoc =´fr´ and serie =´t0123´ and entidade =´mn9998´
        }

        private Dictionary<string, string> GetControlos()
        {
            Dictionary<string, string> valoresControlos = new Dictionary<string, string>();

            valoresControlos.Add("TipoDoc", f4TipoDoc.Text);
            valoresControlos.Add("Terceiro",f4Terceiros.Text);
            valoresControlos.Add("Serie", cboxSerie.Text);
            valoresControlos.Add("NumDoc", numericNumDoc.Text);

            return valoresControlos;
        }

        private bool CheckControlos(Dictionary<string, string> valoresControlos)
        {
            if (valoresControlos.Values.Any(value => value == null)) { 
                System.Windows.Forms.MessageBox.Show("Dados insuficientes. Verifique se existem campos vazios.");
                return false;
            }
            return true;
        }
    }
}
