﻿using Primavera.Extensibility.Sales.Editors;
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
using System.Security.Policy;
using DCT_Extens.Helpers;

namespace DCT_Extens
{
    public class GS1_Geral : CustomCode
    {
        private string _filial { get; set; }
        private string _serie { get; set; }
        private string _tipo { get; set; }
        private long _numDoc { get; set; }
        private VndBEDocumentoVenda _dv { get; set; }
        private ErpBS _BSO { get; set; }
        private StdPlatBS _PSO { get; set; }
        private HelperFunctions _Helpers { get; set; }

        //Existem problemas quando se utiliza o FormCopiaLinhas para criar um DocStocks a partir de outro
        //Como CopiaLinhas não dá trigger a TipoDocumentoIdentificado() nem ArtigoIdentificado(), é necessário manter uma variavel de estado que fica true pelo EditorCopiaLinhas
        public static bool LinhasCopiadas { get; set; }

        private const string PREFIXO = "3";
        private const string PREFIXO_EMPRESA = "560089876";

        public GS1_Geral(string Filial, string Serie, string Tipo, long NumDoc, VndBEDocumentoVenda dv)
        {
            _filial = Filial;
            _serie = Serie;
            _tipo = Tipo;
            _numDoc = NumDoc;
            _dv = dv;
            _BSO = PriMotores.Motor;
            _PSO = PriMotores.Plataforma;
            _Helpers = new HelperFunctions();

            LinhasCopiadas = false;
        }

        public void EditorVendas_DepoisDeGravar()
        {
            string strCliente = _dv.Entidade;
            BasBECliente objCliente = _BSO.Base.Clientes.Consulta(strCliente);

            if ((bool)objCliente.CamposUtil["CDU_SSCC"].Valor)
            {
                //// Destrancar as colunas GUID na tabela TDU_TTE_PackingCodes para conseguir inserir nessas colunas
                //using (StdBEExecSql sql = new StdBEExecSql())
                //{
                //    sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                //    sql.Tabela = "TDU_TTE_PackingCodes";

                //    sql.AddCampo("IDENTITY_INSERT", "ON");
                //    _PSO.ExecSql.Executa(sql);
                //};

                // Calcular digito controlo para cada linha de artigo e criar a sequencia final.
                // Inserir a sequencia final em cada linha e na tabela TDU_TTE_PackingCodes
                foreach (VndBELinhaDocumentoVenda linha in _dv.Linhas)
                {
                    if (linha.TipoLinha.Equals("10"))
                    {
                        // Incrementar número de sequência da TDU_TTE_PackingCodes (última sequência inserida na tabela)
                        string sequencia = (GetSequencia() + 1).ToString();

                        // Concatena todos os elementos necessários para calcular o Digito de Controlo.
                        // Por fim, concatena o digito ao restante.
                        string strFinal = PREFIXO + PREFIXO_EMPRESA + sequencia;
                        strFinal += GetDigitoControlo(strFinal);

                        // Cada linha com artigo no DocVenda tem de ter o código final no seu CDU_SSCC
                        using (StdBEExecSql sql = new StdBEExecSql())
                        {
                            sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                            sql.Tabela = "LinhasDoc";
                            sql.AddCampo("CDU_SSCC", strFinal);
                            sql.AddCampo("ID", linha.IdLinha, true);
                            _PSO.ExecSql.Executa(sql);
                        }

                        // Para cada linha que recebe um strFinal, tem de ser criada uma entrada na tabela TDU_TTE_PackingCodes
                        string strComPrefixo = "(00)" + strFinal;
                        Dictionary<string, string> dict = new Dictionary<string, string>
                        {
                            { "IdCabec", _dv.ID },
                            { "IdLinha", linha.IdLinha },
                            { "PalletCode", strComPrefixo },
                            { "Sequencia", sequencia }
                        };

                        string query =
                            $"INSERT INTO TDU_TTE_PackingCodes" +
                            $"(IdCabec, IdLinha, PalletCode, Sequencia) VALUES ({_dv.ID}, {linha.IdLinha}, {strComPrefixo}, {sequencia});";
                        _Helpers.QuerySQLComIdentityInsert("TDU_TTE_PackingCodes", query);
                    }
                }
            }
        }

        private int GetSequencia()
        {
            StdBELista recSet = _BSO.Consulta("SELECT TOP(1) sequencia from TDU_TTE_PackingCodes ORDER BY sequencia DESC");
            return recSet.Valor(0);
        }

        private string GetDigitoControlo(string str)
        {
            long digito = 0, flag = 3;
            long valor;

            for (int i = str.Length; i >= 1; i--)
            {
                valor = Convert.ToInt64(str.Substring(i - 1, 1)) * flag;

                digito += valor;
                flag = flag.Equals(3) ? 1 : 3;
            }

            // EXTRAÇÃO DO NÚMERO DE CONTROLO A PARTIR DA SOMA FINAL
            // Nas GS-1, o número de controlo é dado por:
            // Subtracção da soma final pelo múltiplo de 10 mais próximo (igual ou maior).
            // ex. soma = 120. 120 - 120 = 0
            // ex. soma = 123. 130 - 120 = 7
            digito = digito % 10;
            digito = 10 - digito;

            if (digito.Equals(10)) { digito = 0; }

            return digito.ToString();
        }
    }
} 

 
