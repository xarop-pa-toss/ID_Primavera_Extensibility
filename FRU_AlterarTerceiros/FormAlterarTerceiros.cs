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
        private clsSDKContexto sdkContexto;        

        public FormAlterarTerceiros()
        {
            InitializeComponent();
        }

        private void FormAlterarTerceiros_Load(object sender, EventArgs e)
        {
            InicializaSDKContexto();

            // Fill TipoDoc combobox
            string query = "SELECT Documento + ' - ' + Descricao AS Doc FROM DocumentosVenda WHERE Inactivo = 0 ORDER BY Documento DESC;";
            cboxTipoDoc.Items.Clear();
            cboxTipoDoc.Items.AddRange(FillComboBox(query).ToArray());

            // Fill Terceiro combobox
            //var tipoTerceiro = BSO.Base.TiposTerceiro
            //cboxTipoDoc.Items.Clear();
            //cboxTipoDoc.Items.AddRange(FillComboBox(query).ToArray());

            //É necessário criar código no Primavera V10 que está Frupor para a empresa ADEGA para fazer o seguinte, poder alterar o tipo de terceiro nos documentos de venda. Para tal é necessário o utilizador introduzir os seguintes campos:
            //-Tipo de documento
            //-Série do documento
            //- Nº de documento
            //Depois poder colocar o tipo de terceiro em tabela.
        }
        //Função que inicializa o contexto SDK.
        private void InicializaSDKContexto()
        {
            if (sdkContexto == null)
            {
                sdkContexto = new clsSDKContexto();
                //Inicializaçao do contexto SDK a partir do objeto BSO e respetivo módulo.
                sdkContexto.Inicializa(BSO, "ERP");
                //Inicialização da plataforma no contexto e verificação de assinatura digital.
                PSO.InicializaPlataforma(sdkContexto);
            }
        }

        //private void cboxTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // Agarra TipoDoc separando a primeira e segunda parte do texto na combobox de Tipo Doc
        //    string inp = cboxTipoDoc.Text;
        //    string tipoDoc = inp.Substring(0, inp.IndexOf(" "));
            
        //    string query = "SELECT DISTINCT Serie FROM SeriesVenda WHERE TipoDoc = '" + tipoDoc + "' ORDER BY Serie DESC;";
        //    cboxSerie.Items.Clear();
        //    cboxSerie.Items.AddRange(FillComboBox(query).ToArray());
        //}

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
            // Agarra TipoDoc separando a primeira e segunda parte do texto na combobox de Tipo Doc
            string inp = cboxTipoDoc.Text;
            string tipoDoc = inp.Substring(0, inp.IndexOf(" "));

            string query = "SELECT DISTINCT Serie FROM SeriesVenda WHERE TipoDoc = '" + tipoDoc + "' ORDER BY Serie DESC;";
            cboxSerie.Items.Clear();
            cboxSerie.Items.AddRange(FillComboBox(query).ToArray());
        }
    }
}
