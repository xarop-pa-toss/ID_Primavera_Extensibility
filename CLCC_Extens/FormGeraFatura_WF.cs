using System; using System.IO;
using System.Windows.Forms;
using ErpBS100; using StdPlatBS100;

namespace CLCC_Extens
{
    public partial class FormGeraFatura_WF : Form
    {
        private ErpBS _BSO;
        private StdPlatBS _PSO;
        internal string _pathCompletoOriginal, _pathCompletoNovo, _nomeNovo;

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
            #region Criação de ficheiro temporário

            _nomeNovo = $"{DateTime.Now:ddMMyyHHmm}.txt";
            _pathCompletoNovo = $"C:\\TEMP\\{_nomeNovo}";

            #endregion

            listBox_Output.Items.Clear();
            listBox_Output.Items.Add("A copiar ficheiro...");



FileCopy strNomeCompleto, strNovoNomeCompleto
List1.AddItem "Cópia criada. Novo ficheiro: " & strNovoNomeCompleto
List1.AddItem "A ler a informação do ficheiro ... "

LER_ParaArray(strNovoNomeCompleto)

If ExistemErros Then

    For i = 1 To UBound(ArrErros)
        List1.AddItem ArrErros(i)
    Next i
    List1.AddItem "OPERAÇÃO CANCELADA. OCORRERAM ERROS!"
    Exit Sub


End If

'DoEvents
Res = Processar()
If Res<> "" Then
    List1.AddItem Res
Else
    List1.AddItem "OPERAÇÃO REALIZADA COM EXITO!"
End If

Kill strNovoNomeCompleto

Exit Sub

erro:
            If Err.Number = 70 Then
                MsgBox "OPERAÇÃO CANCELADA. O FICHEIRO DEVE ESTAR FECHADO!", vbInformation + vbOKOnly, "Informação"
    Else
        MsgBox Err.Number & Err.Description, , "Informação"
        List1.AddItem "OPERAÇÃO CANCELADA. OCORRERAM ERROS!"
        Close NumFich
        Kill strNovoNomeCompleto
    End If
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
