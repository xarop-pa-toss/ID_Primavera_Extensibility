using Primavera.Extensibility;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpBS100; using StdPlatBS100; using BasBE100; using StdBE100; using IntBE100; using CmpBE100;
using VndBE100;
using PRISDK100;
using System.Data;
using System.IO;

namespace DCT_Extens.Helpers
{
    public class HelperFunctions : CustomCode
    {
        private static ErpBS _BSO {  get; set; }
        private static StdPlatBS _PSO { get; set; }
        private static clsSDKContexto _SDKContexto { get; set; }

        public HelperFunctions()
        {
            PriMotores.InicializarContexto();
            _BSO = Helpers.PriMotores.Motor;
            _PSO = Helpers.PriMotores.Plataforma;
            _SDKContexto = Helpers.PriMotores.PriSDKContexto;
        }

        // Error Logging para um ficheiro de texto. Se não for dado path, cria uma pasta nos Documentos com o nome do projecto
        public void EscreverParaFicheiroTxt(string texto, string titulo)
        {
            const string PASTAERROSPATH = "C:/PastaTecnica/PrimaveraExtensibilidadeLogs";
            string ficheiroNome = $"{titulo}_{DateTime.Now.ToString("ddMMyyyy_HHmmss")}";
            string ficheiroPath = Path.Combine(PASTAERROSPATH, ficheiroNome);

            // O caminho final (PASTAERROSPATH) é criado se não existir
            if (!Directory.Exists(PASTAERROSPATH))
            {
                Directory.CreateDirectory(PASTAERROSPATH);
            }

            // Criação ficheiro
            try {
                File.WriteAllText(ficheiroPath, texto);
            } catch (Exception ex) {
                _PSO.MensagensDialogos.MostraErro("Aconteceu um erro mas não foi possivel criar o ficheiro Log.", sDetalhe: ex.ToString());
            }
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
                StdBECampo campo = new StdBECampo
                {
                    Nome = kvp.Key,
                    Valor = kvp.Value
                };

                linha.Add(campo);
            }
            registoUtil.EmModoEdicao = true;
            registoUtil.Campos = linha;
            registoUtil.EmModoEdicao = false;

            _BSO.TabelasUtilizador.Actualiza(NomeTDU, registoUtil);
        }

        public void ApagaLinhasFilhoEPai_docVenda(VndBEDocumentoVenda docVenda, VndBELinhaDocumentoVenda linhaPai)
        {
            // Percorre linhas e encontra todas as que sejam filho da linhaPai. Apaga primeiro filhos e depois pai.
            VndBELinhasDocumentoVenda linhasFilho = new VndBELinhasDocumentoVenda();
            string idPai = linhaPai.IdLinha;

            // Usar LINQ para obter os indices das linhas com o seu IDLinhaPai = IDLinha da linha pai passada como argumento
            List<int> indicesLista = docVenda.Linhas
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
                docVenda.Linhas.Remove(ind);
            }
        }

        public void ApagaLinhasFilhoEPai_docInterno(IntBEDocumentoInterno docInterno, IntBELinhaDocumentoInterno linhaPai)
        {
            // Percorre linhas e encontra todas as que sejam filho da linhaPai. Apaga primeiro filhos e depois pai.
            IntBELinhasDocumentoInterno linhasFilho = new IntBELinhasDocumentoInterno();
            string idPai = linhaPai.IdLinha;

            // Usar LINQ para obter os indices das linhas com o seu IDLinhaPai = IDLinha da linha pai passada como argumento
            List<int> indicesLista = docInterno.Linhas
                .Select((linha, indice) => new { Linha = linha, Indice = indice })
                .Where(item => item.Linha.IdLinhaPai == idPai || item.Linha.IdLinha == idPai)
                .Select(item => item.Indice)
                .OrderByDescending(index => index)
                .ToList();

            // Adicionamos linha pai à lista. Se não houver filhos, só a linha pai será apagada.
            // Invertemos a tabela para apagar de baixo para cima (para manter integridade da tabela)
            indicesLista.Reverse();

            foreach (int ind in indicesLista)
            {
                docInterno.Linhas.Remove(ind);
            }
        }

        public void ApagaLinhasFilhoEPai_docCompra(CmpBEDocumentoCompra docCompra, CmpBELinhaDocumentoCompra linhaPai)
        {
            // Percorre linhas e encontra todas as que sejam filho da linhaPai. Apaga primeiro filhos e depois pai.
            CmpBELinhasDocumentoCompra linhasFilho = new CmpBELinhasDocumentoCompra();
            string idPai = linhaPai.IdLinha;

            // Usar LINQ para obter os indices das linhas com o seu IDLinhaPai = IDLinha da linha pai passada como argumento
            List<int> indicesLista = docCompra.Linhas
                .Select((linha, indice) => new { Linha = linha, Indice = indice })
                .Where(item => item.Linha.IdLinhaPai == idPai || item.Linha.IdLinha == idPai)
                .Select(item => item.Indice)
                .OrderByDescending(index => index)
                .ToList();

            // Adicionamos linha pai à lista. Se não houver filhos, só a linha pai será apagada.
            // Invertemos a tabela para apagar de baixo para cima (para manter integridade da tabela)
            indicesLista.Reverse();

            foreach (int ind in indicesLista)
            {
                docCompra.Linhas.Remove(ind);
            }
        }

        // Devolve DataTable pois objecto criado por BSO.Consulta é uma StdBELista que não é enumerable (não permite usar Foreach loop ou LINQ)
        public DataTable GetDataTableDeSQL(string querySQL)
        {
            DataTable TDULista = _BSO.Consulta(querySQL).DataSet.GetTable();
            return TDULista;
        }
    }
}