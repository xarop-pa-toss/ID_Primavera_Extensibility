using Primavera.Extensibility.BusinessEntities; using Primavera.Extensibility.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using StdBE100;
using StdPlatBS100;
using VndBE100;
using PRISDK100;
using System.Text.RegularExpressions;

namespace PP_Extens
{
    internal class PP_CaixasJM : CustomCode
    {
        private readonly VndBEDocumentoVenda _docVenda;
        public PP_CaixasJM(VndBEDocumentoVenda docVenda)
        {
            _docVenda = docVenda;
        }

        public void ProcessarCaixas()
        {
            // As linhas são introduzidas na TDU da mesma forma, só muda a quantidade das caixas (positivo ou negativo) dependendo de se vão pra JM ou vêm do Fornecedor
            List<string> docsJM = new List<string> { };
            List<string> docsFornecedor = new List<string> { };

            // Entidades JM começam com 603
            if (_docVenda.Entidade.StartsWith("603") && docsJM.Contains(_docVenda.Tipodoc)) {
                Dictionary<string, double> caixasNoDocumento = GetCaixasNoDocumento();

                foreach (var kvp in caixasNoDocumento)
                {
                    NovaLinhaEmTDU(kvp);
                }
                
                return; 
            }
            
            // Entidades Fornecedor de caixas
            if (_docVenda.Entidade.StartsWith("") && docsFornecedor.Contains(_docVenda.Tipodoc)) {
                Dictionary<string,double> caixasNoDocumento = GetCaixasNoDocumento();
                
                foreach (var kvp in caixasNoDocumento)
                {
                    NovaLinhaEmTDU(kvp);
                }

                return;
            }
        }

        private Dictionary<string,double> GetCaixasNoDocumento()
        {
            List<string> artigosCaixaList = new List<string> { "ARTIGOS CAIXA " };

            var caixasNoDocumento = _docVenda.Linhas
                .Where(linha => artigosCaixaList.Contains(linha.Artigo))
                .GroupBy(linha => linha.Artigo)
                .ToDictionary(
                    grupo => grupo.Key,
                    grupo => grupo.Sum(linha => linha.Quantidade));

            return caixasNoDocumento;
        }

        internal void NovaLinhaEmTDU(KeyValuePair<string,double> caixas)
        {
            // Criar Campos para RegistoUtil para então inserir na TDU
            // https://v10api.primaverabss.com/html/api/plataforma/StdBE100.StdBETipos.EnumTipoCampo.html

            StdBERegistoUtil registoUtil = new StdBERegistoUtil();
            StdBECampos linha = new StdBECampos();

            Dictionary<string, string> campos = new Dictionary<string, string>
            {
                {"CDU_ID", PP_Geral.GetGUID()},
                {"CDU_CabecDocID", _docVenda.ID},
                {"CDU_NumEncomenda", _docVenda.Referencia},
                {"CDU_TipoCaixa",  caixas.Key},
                {"CDU_Quantidade", ((int)caixas.Value).ToString()},
                {"CDU_Data", DateTime.Now.ToString()}
            };

            foreach (var kvp in campos)
            {
                StdBECampo campo = new StdBECampo();
                campo.Nome = kvp.Key;
                campo.Valor = kvp.Value;

                linha.Add(campo);
            }

            registoUtil.EmModoEdicao = true;
            registoUtil.Campos = linha;
            registoUtil.EmModoEdicao = false;

            BSO.TabelasUtilizador.Actualiza("TDU_CaixasJM", registoUtil);
        }
    }
}
