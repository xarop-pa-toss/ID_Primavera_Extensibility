using System; using System.IO; using System.Collections.Generic; using System.Linq;
using System.Windows.Forms;
using ErpBS100; using StdPlatBS100;

namespace CLCC_Extens
{
    public partial class FormGeraFatura_WF : Form
    {
        private ErpBS _BSO;
        private StdPlatBS _PSO;
        private string _pathCompletoOriginal, _pathCompletoNovo, _nomeNovo;
        private bool _ficheiroComErros;

        public FormGeraFatura_WF()
        {
            InitializeComponent();
        }

        private void FormGeraFatura_WF_Load(object sender, EventArgs e)
        {
            Motor.PriEngine.CreateContext("CLCC", "id", "*Pelicano*");
            _BSO = Motor.PriEngine.Engine;
            _PSO = Motor.PriEngine.Platform;

            listBox_Output.Items.Clear();
            txtbox_Ficheiro.Clear();
        }


        private void btn_EscolherFicheiro_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) 
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "Todos os ficheiros (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    _pathCompletoOriginal = openFileDialog.FileName;
                    txtbox_Ficheiro.Text = _pathCompletoOriginal;
                }
            }
        }

        private void btn_Iniciar_Click(object sender, EventArgs e)
        {
            int i;
            string res;

            #region Validação de existência do ficheiro
            if (string.IsNullOrEmpty(txtbox_Ficheiro.Text)) {
                _PSO.MensagensDialogos.MostraAviso("O caminho do ficheiro está vazio.", StdBSTipos.IconId.PRI_Exclama);
                return;
            }

            if (!File.Exists(_pathCompletoOriginal)) {
                _PSO.MensagensDialogos.MostraAviso("O ficheiro não existe no caminho especificado.", StdBSTipos.IconId.PRI_Exclama);
                return;
            }

            listBox_Output.Items.Clear();
            listBox_Output.Items.Add("A copiar ficheiro...");
            #endregion

            #region Criação de ficheiro temporário
            _nomeNovo = $"{DateTime.Now:ddMMyyHHmm}.txt";
            _pathCompletoNovo = $"C:\\TEMP\\{_nomeNovo}";

            listBox_Output.Items.Add($"Ficheiro copiado para {_pathCompletoNovo}");
            listBox_Output.Items.Add("A ler informação do ficheiro...");
            #endregion

            #region Verificação das linhas e exposição dos erros ao utilizador
            List<string> linhasLista;
            try {
                linhasLista = new List<string>(File.ReadAllLines(_pathCompletoNovo));
            }
            catch {
                listBox_Output.Items.Add("Erro ao ler linhas do ficheiro. Não foi gerado nenhum documento.");
                _PSO.MensagensDialogos.MostraAviso("Erro ao ler linhas do ficheiro. Não foi gerado nenhum documento.", StdBSTipos.IconId.PRI_Critico);
                return;
            }

            listBox_Output.Items.Add("A validar linhas...");
            List<string> linhasErrosLista = ValidarLinhas(linhasLista);

            // Se linhasErros não estiver vazia, é porque tem erros. Cada linha da linhasErros tem o número da linha com erro no ficheiro .txt.
            foreach (string erro in linhasErrosLista) {
                listBox_Output.Items.Add(erro);
            }

            if (linhasErrosLista.Any()) {
                listBox_Output.Items.Add("Existem linhas com erros, por favor verifique")
            }
            #endregion
        }


        private List<string> ValidarLinhas(List<string> linhasLista)
        {
            // Código original VBA traduzido por ChatGPT com algumas alterações
            // Percorre as linhas retiradas do ficheiro .txt e valida algumas condições. Se alguma falhar, guarda o número da linha na List linhasErrosLista
            int linhasLidas = 0;
            bool jaVerificado = false;

            List<string> linhasErrosLista = new List<string>();

            foreach (string line in linhasLista) { 
                
                linhasLidas++;
                Console.WriteLine($"Linha {linhasLidas}");

                if (line.StartsWith("***")) {
                    // Ignorar linha
                } else if (line.Trim().Substring(0, 2).ToUpper() == "TC" && !jaVerificado) {
                    // É linha com documento de venda
                    jaVerificado = true;
                    if (!_BSO.Base.Artigos.Existe("TC")) {
                        linhasErrosLista.Add($"Erro: Produto inexistente - TC");
                    }
                } else if (!line.StartsWith("***") && line.Substring(0, 2).ToUpper() != "TC") {
                    // É um cliente
                    var lineParts = line.Split(',');
                    var cliente = lineParts[0].Trim();
                    var condPagamento = lineParts[1].Trim();

                    if (!_BSO.Base.Clientes.Existe(cliente)) {
                        linhasErrosLista.Add($"Erro: Linha {linhasLidas}. Entidade inexistente - {cliente}");
                    }

                    if (!_BSO.Base.CondsPagamento.Existe(condPagamento)) {
                        linhasErrosLista.Add($"Erro: Linha {linhasLidas}. Condição de pagamento inexistente - {condPagamento}");
                    }
                }
            }
            return linhasErrosLista;
        }
    }
}
