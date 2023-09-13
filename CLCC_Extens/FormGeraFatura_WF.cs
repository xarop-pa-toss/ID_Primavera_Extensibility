using System; using System.IO; using System.Collections.Generic;
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
            #endregion

            listBox_Output.Items.Clear();
            listBox_Output.Items.Add("A copiar ficheiro...");

            #region Criação de ficheiro temporário
            _nomeNovo = $"{DateTime.Now:ddMMyyHHmm}.txt";
            _pathCompletoNovo = $"C:\\TEMP\\{_nomeNovo}";
            #endregion

            listBox_Output.Items.Add($"Ficheiro copiado para {_pathCompletoNovo}");
            listBox_Output.Items.Add("A ler informação do ficheiro...");
            try {
                List<string> linhasLista = new List<string>(File.ReadAllLines(_pathCompletoNovo));
            }
            catch {
                listBox_Output.Items.Add("Erro ao ler linhas do ficheiro. Não foi gerado nenhum documento.");
                _PSO.MensagensDialogos.MostraAviso("Erro ao ler linhas do ficheiro. Não foi gerado nenhum documento.", StdBSTipos.IconId.PRI_Critico);
                return;
            }

            listBox_Output.Items.Add("A validar linhas");

        }

        private List<string> ValidarLinhas(string path)
        {
            
            _ficheiroComErros = false;

FNUM = FreeFile
Open strFich For Input Access Read As #FNUM  'Input é para ler
NumFich = FNUM
i = 0
k = 0

List1.AddItem "Lendo ficheiro, aguarde por favor"

Do Until EOF(FNUM)
    i = i + 1

    List1.AddItem "Linha " & i

    ReDim Preserve arrLinhas(1 To i)
    Line Input #FNUM, arrLinhas(i)
    
    If Mid(arrLinhas(i), 1, 3) = "***" Then
        'Ignora, não faz nada
    ElseIf UCase(Trim(Mid(arrLinhas(i), 1, 2))) = "TC" And jaVerificado = False Then


        'É uma linha de Factura
        jaVerificado = True
        If Aplicacao.BSO.Comercial.Artigos.Existe("TC") = False Then
            k = k + 1
            ReDim Preserve ArrErros(1 To k)
            ArrErros(k) = "Erro: Produto inexistente - TC"
            ExistemErros = True
        End If
    ElseIf UCase(Trim(Mid(arrLinhas(i), 1, 2))) <> "TC" And Mid(arrLinhas(i), 1, 2) <> "***" Then
        'É o cliente
        If Aplicacao.BSO.Comercial.Clientes.Existe(Trim(Split(arrLinhas(i), ",")(0))) = False Then
            k = k + 1
            ReDim Preserve ArrErros(1 To k)
            ArrErros(k) = "Erro linha " & i & ". Entidade inexistente - " & Trim(Split(arrLinhas(i), ",")(0)) & ""
            ExistemErros = True


        End If


        If Aplicacao.BSO.Comercial.CondsPagamento.Existe(Trim(Split(arrLinhas(i), ",")(1))) = False Then
            k = k + 1
            ReDim Preserve ArrErros(1 To k)
            ArrErros(k) = "Erro linha " & i & ". Condição de Pagamento inexistente - " & Trim(Split(arrLinhas(i), ",")(1)) & ""
            ExistemErros = True
        End If


    End If
Loop

Close FNUM
        }
    }
}
