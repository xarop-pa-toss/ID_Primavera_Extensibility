using Primavera.Extensibility.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpBS100; 
using StdPlatBS100; 
using StdBE100; 
using IntBE100; 
using CmpBE100;
using VndBE100;
using PRISDK100;
using System.Data;
using System.IO;
using Primavera.Extensibility.Extensions;
using System.Data.SqlClient;

namespace HelperFunctionsPrimavera10
{
    public class HelperFunctions : CustomCode
    {
        private static ErpBS _BSO {  get; set; }
        private static StdPlatBS _PSO { get; set; }
        private static clsSDKContexto _SDKContexto { get; set; }
        public readonly ISecrets _secrets;

        private static object lockObj = new object();
        private static object globalVar;

        public HelperFunctions(ISecrets secrets)
        {
            _secrets = secrets;
            
            if (!PriMotores.MotorStatus)
            {
                PriMotores.InicializarContexto(_secrets.Empresa(), _secrets.Utilizador(), _secrets.Password());
            }

            _BSO = PriMotores.Motor;
            _PSO = PriMotores.Plataforma;
            _SDKContexto = PriMotores.PriSDKContexto;
        }

        // SET e GET uma variável de qualquer lado do programa.
        // Variável autodestroi-se quando é acedida (GetVariavel).
        public void SetGlobalVar<T>(T value)
        {
            lock (lockObj)
            {
                globalVar = value;
            }
        }
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

        // Por vezes, TDUs com campos ID têm de ter esses campos desbloqueados antes de se poder escrever
        // Infelizmente o Primavera 10 já não permite fazer queries não-consulta directamente à BD sem passar pelos métodos de controlo dele
        public void QuerySQL(string querySQL, string tabelaIdentityInsert = null)
        {
            string nomeBDdaEmpresa = _PSO.BaseDados.DaNomeBDdaEmpresa(_BSO.Contexto.CodEmp);
            string connString = 
                $"Data Source={_secrets.BDServidorInstancia()};" +
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
                    }
                    else
                    {
                        queryList.Add(querySQL);
                    }
                    string query = string.Join("; ", queryList);

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Connection.Open()
                        command.ExecuteNonQuery();
                    }

                catch (Exception ex)
                {
                    EscreverParaFicheiroTxt(ex.ToString(), "Helpers_AlterarPropriedadeTabelaNaBD_LigacaoBD");
                }
            }
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

        // Abre InputForm e devolve resposta
        public string MostraInputForm(string titulo, string descricao, string valorDefeito, bool permiteNull, ErpBS BSO)
        {
            string resposta = null;

            using (var formInstancia = BSO.Extensibility.CreateCustomFormInstance(typeof(InputForm)))
            {
                InputFormServico.Titulo = titulo;
                InputFormServico.Descricao = descricao;
                InputFormServico.ValorDefeito = valorDefeito;

                if (formInstancia.IsSuccess())
                {
                    (formInstancia.Result as InputForm).ShowDialog();
                    resposta = InputFormServico.Resposta;
                }

                InputFormServico.Limpar();
                if (string.IsNullOrWhiteSpace(resposta) && !permiteNull)
                {
                    System.Windows.Forms.MessageBox.Show("Campo não pode estar vazio.");
                    MostraInputForm(titulo, descricao, valorDefeito, permiteNull, BSO);
                }

                return resposta;
            }
        }
    }
}