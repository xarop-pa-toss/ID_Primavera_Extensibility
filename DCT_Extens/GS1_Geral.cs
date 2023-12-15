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
using System.Drawing.Text;

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
        HelperFunctions Helpers = new HelperFunctions();

        public GS1_Geral(string Filial, string Serie, string Tipo, long NumDoc, VndBEDocumentoVenda dv, ErpBS BSO, StdBSInterfPub PSO)
        {
            _Filial = Filial;
            _Serie = Serie;
            _Tipo = Tipo;
            _NumDoc = NumDoc;
            _dv = dv;
            _BSO = BSO;
            _PSO = PSO;
        }

        public void EditorVendas_AntesDeGravar()
        {
            string strCliente = _dv.Entidade;
            BasBECliente objCliente = _BSO.Base.Clientes.Consulta(strCliente);

            if ((bool)objCliente.CamposUtil["CDU_SSCC"].Valor)
            {
                // Destrancar as colunas GUID na tabela TDU_TTE_PackingCodes para conseguir inserir nessas colunas
                StdBEExecSql sql = new StdBEExecSql();
                sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                sql.Tabela = "TDU_TTE_PackingCodes";
                sql.AddCampo("IDENTITY_INSERT", "ON");
                _PSO.ExecSql.Executa(sql);
                sql.Dispose();

                // Calcular digito controlo para cada linha de artigo e criar a sequencia final.
                // Inserir a sequencia final em cada linha e na tabela TDU_TTE_PackingCodes
                foreach (VndBELinhaDocumentoVenda linha in _dv.Linhas)
                {
                    if (linha.TipoLinha.Equals(10))
                    {
                        // Incrementar número de sequência da TDU_TTE_PackingCodes (última sequência inserida na tabela)
                        string sequencia = GetSequencia() + 1;

                        // Concatena todos os elementos necessários para calcular o Digito de Controlo.
                        // Por fim, concatena o digito ao restante.
                        string strFinal = PREFIXO + PREFIXO_EMPRESA + sequencia;
                        strFinal += GetDigitoControlo(strFinal);

                        // Cada linha com artigo no DocVenda tem de ter a strFinal no seu CDU_SSCC
                        sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                        sql.Tabela = "LinhasDoc";
                        sql.AddCampo("CDU_SSCC", strFinal);
                        sql.AddCampo("ID", linha.IdLinha, true);
                        _PSO.ExecSql.Executa(sql);
                        sql.Dispose();

                        // Para cada linha que recebe um strFinal, tem de ser criada uma entrada na tabela TDU_TTE_PackingCodes
                        string strComPrefixo = "(00)" + strFinal;
                        Dictionary<string, string> dict = new Dictionary<string, string>
                        {
                            { "IdCabec", _dv.ID },
                            { "IdLinha", linha.IdLinha },
                            { "PalletCode", strComPrefixo },
                            { "Sequencia", sequencia }
                        };
                        Helpers.TDU_Insert("TDU_TTE_PackingCodes", dict, _BSO);
                    }
                }
            }
        }

        private string GetSequencia()
        {

        }

        private int GetDigitoControlo(string strFinal)
        {

        }

    }
} 

 
