using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.CustomCode;
using VndBE100;
using ErpBS100;
using StdPlatBS100;
using StdBE100;
using BasBE100;

namespace DCT_Extens
{
    public class GS1_Geral : CustomCode
    {
        private string _Filial { get; set; }
        private string _Serie { get; set; }
        private string _Tipo { get; set; }
        private long _NumDoc { get; set; }
        private VndBEDocumentoVenda _dv { get; set; }
        private ErpBS _BSO { get; set; }
        private StdBSInterfPub _PSO { get; set; }

        private const string PREFIXO = "3";
        private const string PREFIXO_EMPRESA = "560089876";

        public GS1_Geral(string Filial, string Serie, string Tipo, long NumDoc, VndBEDocumentoVenda dv, ErpBS BSO, StdBSInterfPub PSO)
        {
            _Filial = Filial;
            _Serie = Serie;
            _Tipo = Tipo;
            _NumDoc = NumDoc;
            _dv = dv;
            _BSO = BSO;
            _PSO = PSO;

            string strCliente = _dv.Entidade;
            BasBECliente objCliente= BSO.Base.Clientes.Consulta(strCliente);

            if ((bool)objCliente.CamposUtil["CDU_SSCC"].Valor)
            {
                // Destrancar as colunas GUID na tabela TDU_TTE_PackingCodes para conseguir inserir nessas colunas
                StdBEExecSql sql = new StdBEExecSql();
                sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                sql.Tabela = "TDU_TTE_PackingCodes";
                sql.AddCampo("IDENTITY_INSERT", "ON");

                PSO.ExecSql.Executa(sql);


            }
        }

    }
    
}
