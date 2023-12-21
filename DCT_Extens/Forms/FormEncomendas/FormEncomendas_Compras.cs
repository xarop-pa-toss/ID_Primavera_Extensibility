using DCT_Extens.Helpers;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCT_Extens.Forms.FormEncomendas
{
    public partial class FormEncomendas : CustomForm
    {
        private ErpBS100.ErpBS _BSO;
        private StdPlatBS100.StdPlatBS _PSO;
        private Helpers.HelperFunctions _Helpers;

        private string _radioSeleccionado;

        public FormEncomendas()
        {
            _BSO = PriMotores.Motor;
            _PSO = PriMotores.Plataforma;
            _Helpers = new Helpers.HelperFunctions();

            InitializeComponent();
        }

        private void btn_VerDocs_Click(object sender, EventArgs e)
        {
            var query = $"SELECT " +
                            $"   ccs.fechado AS Fechado, " +
                            $"   cc.TipoDoc AS Documento, " +
                            $"   cc.NumDoc AS Numero, " +
                            $"   cc.serie AS Serie, " +
                            $"   cc.dataDoc AS Data, " +
                            $"   cc.entidade AS Fornecedor, " +
                            $"   cc.Nome AS Nome, " +
                            $"   ccs.IdCabecCompras " +
                            $"FROM " +
                            $"   cabeccompras cc " +
                            $"   INNER JOIN CabecComprasStatus ccs ON cc.Id = ccs.IdCabecCompras " +
                            $"   INNER JOIN DocumentosCompra dc ON cc.TipoDoc = dc.Documento " +
                            $"   INNER JOIN Fornecedores fn ON cc.Entidade = fn.Fornecedor " +
                            $"WHERE " +
                            $"   cc.dataDoc BETWEEN '{dtPicker_DataInicial.Value.ToString("yyyy-MM-dd")}' AND '{dtPicker_DataFinal.Value.ToString("yyyy-MM-dd")}' " +
                            $"   AND tipodoc = '{txtBox_TipoDoc.Text}' " +
                            $"   AND ccs.estado = 'P' " +
                            $"   AND ccs.fechado = '0' " +
                            $"   AND ccs.anulado = '0' " +
                            $"ORDER BY " +
                            $"   DataDoc";


        }

        private void FormEncomendas_Load(object sender, EventArgs e)
        {

        }
    }
}
