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
using System.Windows.Forms;

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
            List<string> docsJM_Faturacao = new List<string> {"GRP", "FP", "FM"};
            List<string> docsJM_NotaCredito = new List<string> {"NC", "NCD", "NDI"};
            List<string> docsFornecedor_Faturacao = new List<string> {""};
            List<string> docsFornecedor_NotaCredito = new List<string> {"VFS", "VNS"};

            // Entidades JM começam com 603
            if (_docVenda.Entidade.StartsWith("306") && (docsJM_Faturacao.Contains(_docVenda.Tipodoc) || docsJM_NotaCredito.Contains(_docVenda.Tipodoc))) {
                Dictionary<string, double> caixasNoDocumento = GetCaixasNoDocumento();

                foreach (var kvp in caixasNoDocumento)
                {
                    NovaLinhaEmTDU(kvp);
                }
                return; 
            }
            
            // Entidades Fornecedor de caixas
            if (_docVenda.Entidade.StartsWith("086") && (docsFornecedor_Faturacao.Contains(_docVenda.Tipodoc) || docsFornecedor_NotaCredito.Contains(_docVenda.Tipodoc))) {
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
            List<string> artigosCaixaList = new List<string> { " *** PLACEHOLDER *** " };

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
            
            Dictionary<string, string> armazemDict = new Dictionary<string, string>
            {
                {null, "Portipesca"},
                {"1", "Alcobaça"},
                {"2", "Algoz" }
            };

            // Pergunta ORIGEM das caixas
            string resposta = "", armazemOrigem = "";
            string armazemInputBoxDescricao = constructorDescricao(armazemDict);

            while (string.IsNullOrEmpty(resposta))
            {
                try {
                    PSO.MensagensDialogos.MostraDialogoInput(ref resposta, "Armazém de origem das caixas", armazemInputBoxDescricao, 1);
                    armazemOrigem = armazemDict[resposta];
                }
                catch (KeyNotFoundException e) {
                    PSO.MensagensDialogos.MostraAviso("Valor inválido no armazem de origem das caixas.", StdBSTipos.IconId.PRI_Exclama);
                }
            }

            Dictionary<string, string> campos = new Dictionary<string, string>
            {
                {"CDU_ID", PP_Geral.GetGUID()},
                {"CDU_CabecDocID", _docVenda.ID},
                {"CDU_NumEncomenda", _docVenda.Referencia},
                {"CDU_TipoCaixa",  caixas.Key},
                {"CDU_Quantidade", ((int)caixas.Value).ToString()},
                {"CDU_Data", DateTime.Now.ToString()},
                {"CDU_ArmazemOrigem", armazemOrigem}
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

        private string constructorDescricao(Dictionary<string,string> armazemDict)
        {
            StringBuilder descricaoBuilder = new StringBuilder();

            foreach (var kvp in armazemDict)
            {
                descricaoBuilder.AppendLine($"{kvp.Key} - {kvp.Value}");
            }

            return descricaoBuilder.ToString();
            
        }
    }
}
