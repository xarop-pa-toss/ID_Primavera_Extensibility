using Primavera.Extensibility;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpBS100; using StdPlatBS100; using BasBE100; using StdBE100;
using VndBE100;
using PRISDK100;

namespace DCT_Extens.Helpers
{
    public class HelperFunctions : CustomCode
    {
        private ErpBS _BSO {  get; set; }
        private StdPlatBS _PSO { get; set; }
        private clsSDKContexto _SDKContexto { get; set; }

        //Existem problemas quando se utiliza o FormCopiaLinhas para criar um DocStocks a partir de outro
        //Como CopiaLinhas não dá trigger a TipoDocumentoIdentificado() nem ArtigoIdentificado(), é necessário manter uma variavel de estado que fica true pelo EditorCopiaLinhas
        public static bool LinhasCopiadas { get; set; }

        public HelperFunctions()
        {
            _BSO = Helpers.PriMotores.Motor;
            _PSO = Helpers.PriMotores.Plataforma;
            _SDKContexto = Helpers.PriMotores.PriSDKContexto;
            LinhasCopiadas = false;
        }

        // Update TDU se existir linha. Insert se não existir.
        public void TDU_Actualiza(string NomeTDU, Dictionary<string, string> Dict)
        {
            // Criar Campos para RegistoUtil para então inserir na TDU
            // https://v10api.primaverabss.com/html/api/plataforma/StdBE100.StdBETipos.EnumTipoCampo.html

            StdBERegistoUtil registoUtil = new StdBERegistoUtil();
            StdBECampos linha = new StdBECampos();

            foreach (KeyValuePair<string, string> kvp in Dict)
            {
                StdBECampo campo = new StdBECampo();
                campo.Nome = kvp.Key;
                campo.Valor = kvp.Value;

                linha.Add(campo);
            }
            registoUtil.EmModoEdicao = true;
            registoUtil.Campos = linha;
            registoUtil.EmModoEdicao = false;

            _BSO.TabelasUtilizador.Actualiza(NomeTDU, registoUtil);
        }

        public void ApagaLinhasFilhoEPai(VndBEDocumentoVenda dv, VndBELinhaDocumentoVenda linhaPai)
        {
            // Percorre linhas e encontra todas as que sejam filho da linhaPai. Apaga primeiro filhos e depois pai.
            VndBELinhasDocumentoVenda linhasFilho = new VndBELinhasDocumentoVenda();
            string idPai = linhaPai.IdLinha;

            // Usar LINQ para obter os indices das linhas com o seu IDLinhaPai = IDLinha da linha pai passada como argumento
            List<int> indicesLista = dv.Linhas
                .Select((linha, indice) => new { Linha = linha, Indice = indice })
                .Where(item => item.Linha.IdLinhaPai == idPai || item.Linha.IdLinha == idPai )
                .Select(item => item.Indice)
                .OrderByDescending(index => index)
                .ToList();

            // Adicionamos linha pai à lista. Se não houver filhos, só a linha pai será apagada.
            // Invertemos a tabela para apagar de baixo para cima (para manter integridade da tabela)
            indicesLista.Reverse();

            foreach (int ind in indicesLista)
            {
                dv.Linhas.Remove(ind);
            }
        }
    }
}

