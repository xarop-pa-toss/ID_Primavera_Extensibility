using CmpBE100;
using ErpBS100;
using IntBE100;
using Primavera.Extensibility.CustomCode;
using PRISDK100;
using StdBE100;
using StdPlatBS100;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using VndBE100;

namespace HelpersPrimavera10
{
    /// <summary>
    /// Funções de suporte para ajudar com desenvolvimento de Extensibilidade para o ERP Primavera 10
    /// </summary>
    public class HelperFunctions : CustomCode
    {
        private static ErpBS _BSO { get; set; }
        private static StdBSInterfPub _PSO { get; set; }
        private static clsSDKContexto _SDKContexto { get; set; }
        private static ISecrets _secrets;

        private static object lockObj = new object();
        private static object globalVar;
        private const string EscreveTxtPastaPath = "C:/PastaTecnica/PrimaveraExtensibilityLogs";

        /// <summary>
        /// Inicializa PriMotores estaticamente. Deve ser criada no evento DepoisDeCriarMenus na Plataforma para popular PSO e BSO assim que são abertos. Contém várias funções de suporte.
        /// </summary>
        /// <param name="secrets">Implementa ISecrets.</param>
        public HelperFunctions(ISecrets secrets)
        {
            if (_secrets == null) { _secrets = secrets; }

            PriMotores.InicializarContexto(_secrets);
            _BSO = PriMotores.Motor;
            _PSO = PriMotores.Plataforma;
            _SDKContexto = PriMotores.PriSDKContexto;
        }

        public HelperFunctions()
        {
        }

        /// <summary>
        /// Set uma variável genérica autodestructiva global.
        /// </summary>
        /// <param name="value">Valor de qualquer tipo.</param>
        public void SetGlobalVar<T>(T value)
        {
            lock (lockObj)
            {
                globalVar = value;
            }
        }

        /// <summary>
        /// Get variável genérica criada com SetGlobalVar<T>. Autodestroi-se quando acedida.
        /// </summary>
        public T GetGlobalVarOuDefault<T>()
        {
            lock (lockObj)
            {
                if (globalVar != null && globalVar is T)
                {
                    T resultado = (T)globalVar;
                    globalVar = default(T); // Reset à variável
                    return resultado;
                }
                return default(T);
            }
        }

        /// <summary>
        /// Escreve para ficheiro de texto no caminho definido na propriedade EscreveTxtPastaPath (cria a pasta se não existir).
        /// </summary>
        /// <param name="texto">String a escrever para o ficheiro.</param>
        /// <param name="ficheiroNome">Nome do ficheiro. Será sucedido de data/hora no formato 'ddMMyyyy_HHmmss'.</param>
        /// <param name="pastaPath">Usar formato "C:/Pasta1/Pasta2". Se pasta não existir, será criada. Se for passado como null, escreve para "C:/PastaTecnica/PrimaveraExtensibilityLogs".</param>
        public void EscreverParaFicheiroTxt(string texto, string ficheiroNome, string pastaPath = null)
        {
            if (pastaPath == null) { pastaPath = EscreveTxtPastaPath; }

            string ficheiroNomeFinal = $"{ficheiroNome}_{DateTime.Now.ToString("ddMMyyyy_HHmmss")}";
            string ficheiroPath = Path.Combine(pastaPath, ficheiroNomeFinal);

            // O caminho final (pastaPath) é criado se não existir
            if (!Directory.Exists(pastaPath))
            {
                Directory.CreateDirectory(pastaPath);
            }

            // Criação ficheiro
            try
            {
                File.WriteAllText(ficheiroPath, texto);
            }
            catch (Exception ex)
            {
                _PSO.MensagensDialogos.MostraErro("Aconteceu um erro e não foi possivel criar o ficheiro Log.", sDetalhe: ex.ToString());
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

        /// <summary>
        /// Query SQL directa e sem restrições à base de dados da empresa aberta. Usado para Update/Insert/Delete quando o StdBEExecSql não satisfaz as condições necessárias.
        /// </summary>
        /// <param name="querySQL"></param>
        /// <param name="tabelaIdentityInsert">Nome da tabela a destrancar, caso seja preciso executar CRUD em linhas com campos GUID.
        public void QuerySQL(string querySQL, string tabelaIdentityInsert = null)
        {
            string nomeBDdaEmpresa = _PSO.BaseDados.DaNomeBDdaEmpresa(_BSO.Contexto.CodEmp);
            string connString =
                $"Data Source={_secrets.GetBDServidorInstancia()};" +
                "Network Library=DBMSSOCN;" +
                $"Initial Catalog={nomeBDdaEmpresa};" +
                "Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    // Se parâmetro tabelaIdentityInsert não for nulo, QUERY altera propriedade IDENTITY_INSERT para se conseguir manipular GUIDs
                    List<string> queryList = new List<string>();
                    if (tabelaIdentityInsert != null)
                    {
                        queryList.Add($"SET IDENTITY_INSERT {tabelaIdentityInsert} ON)");
                        queryList.Add(querySQL);
                        queryList.Add($"SET IDENTITY_INSERT {tabelaIdentityInsert} OFF)");
                    } else
                    {
                        queryList.Add(querySQL);
                    }
                    string query = string.Join("; ", queryList);

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    EscreverParaFicheiroTxt(ex.ToString(), "Helpers_AlterarPropriedadeTabelaNaBD_LigacaoBD", EscreveTxtPastaPath);
                }
            }
        }

        /// <summary>
        /// Apaga todas as linhas relacionadas com a linha a ser apagada, sejam irmãs, filhas ou pai.
        /// </summary>
        public void ApagaLinhasFilhoEPai_docVenda(VndBEDocumentoVenda docVenda, VndBELinhaDocumentoVenda linhaPai)
        {
            // Percorre linhas e encontra todas as que sejam filho da linhaPai. Apaga primeiro filhos e depois pai.
            VndBELinhasDocumentoVenda linhasFilho = new VndBELinhasDocumentoVenda();
            string idPai = linhaPai.IdLinha;

            // Usar LINQ para obter os indices das linhas com o seu IDLinhaPai = IDLinha da linha pai passada como argumento
            List<int> indicesLista = docVenda.Linhas
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
                docVenda.Linhas.Remove(ind);
            }
        }

        /// <summary>
        /// Apaga todas as linhas relacionadas com a linha a ser apagada, sejam irmãs, filhas ou pai.
        /// </summary>
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

        /// <summary>
        /// Apaga todas as linhas relacionadas com a linha a ser apagada, sejam irmãs, filhas ou pai.
        /// </summary>
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

        /// <summary>
        /// O mesmo que BSO.Consulta mas devolve DataTable em vez de StdBELista que não é enumerable (não permite usar Foreach loop ou LINQ).
        /// </summary>
        public DataTable GetDataTableDeSQL(string querySQL)
        {
            DataTable TDULista = _BSO.ConsultaDataTable(querySQL);
            return TDULista;
        }

        /// <summary>
        /// Abre InputForm e devolve string. Pode retornar:
        ///  1 - String com valor se user preencheu textbox.
        ///  2 - String null se não user preencheu textbox e permiteNull = true, ou se cancelou o form.
        /// Ou seja, pode retornar null mesmo que permiteNull seja true!!
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="descricao"></param>
        /// <param name="valorDefeito"></param>
        /// <param name="permiteNull"></param>
        public string MostraInputForm(string titulo, string descricao, string valorDefeito, bool permiteNull = true)
        {
            using (InputForm inputForm = new InputForm(titulo, descricao, valorDefeito, permiteNull))
            {
                inputForm.ShowDialog();
                return inputForm.Resposta;
            }
        }
    }
}