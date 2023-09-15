using System; using System.IO; using System.Collections.Generic; using System.Linq;
using System.Windows.Forms;
using Primavera.Extensibility.Sales.Editors;
using ErpBS100; using StdPlatBS100; using VndBE100; using BasBE100; using StdBE100;

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
                listBox_Output.Items.Add("Existem linhas com erros, por favor verifique o ficheiro e tente novamente.");
                listBox_Output.Items.Add("Não foi gerado nenhum documento.");
                _PSO.MensagensDialogos.MostraAviso("ERROS NO FICHEIRO.\nNão foi gerado nenhum documento.", StdBSTipos.IconId.PRI_Critico);
                return;
            }
            #endregion

            #region Criação de documentos de venda
            bool criacaoDocs = CriarDocumentosVenda(linhasLista);

            if (!criacaoDocs) {
                _PSO.MensagensDialogos.MostraAviso("ERRO! Nenhum documento foi criado.\nVer detalhes na janela principal.", StdBSTipos.IconId.PRI_Critico);
            } 
            else {
                _PSO.MensagensDialogos.MostraAviso("Todos os documentos foram criados com sucesso.", StdBSTipos.IconId.PRI_Informativo);
            }
            #endregion
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CriarDocumentosVenda(List<string> linhasLista)
        {
            try {
                _BSO.IniciaTransaccao();

                int i = 0;
                while (i < linhasLista.Count) {
                    if (!linhasLista[i].StartsWith("TC") && !linhasLista[i].StartsWith("***")) {

                        // É Cliente -> fazer CabecDoc
                        List<string> customerInfo = linhasLista[i].Split(',').Select(s => s.Trim()).ToList();

                        VndBEDocumentoVenda docVenda = new VndBEDocumentoVenda
                        {
                            Entidade = customerInfo[0],
                            TipoEntidade = "C",
                            Tipodoc = "FR",
                            Serie = _BSO.Base.Series.DaSerieDefeito("V", "FR"),
                            TipoLancamento = "000",
                            DataDoc = DateTime.Now,
                            CondPag = customerInfo[1]
                        };

                        int vdDadosTodos = (int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosTodos;
                        docVenda = _BSO.Vendas.Documentos.PreencheDadosRelacionados(docVenda,ref vdDadosTodos);

                        int k = i + 1;
                        bool UltimaLinha = false;

                        while (k < linhasLista.Count && linhasLista[k].StartsWith("TC") && !UltimaLinha) {
                            List<string> Arr = linhasLista[k].Split(',').Select(s => s.Trim()).ToList();

                            if (Arr.Count == 1) {
                                _BSO.DesfazTransaccao();
                                listBox_Output.Items.Add($"Linha Inconsistente: {k}.\nOPERAÇÃO CANCELADA!");
                                return false;
                            }

                            StdBELista BELista = _BSO.Consulta($"SELECT Iva from IVA where taxa={Arr[5]}");

                            if (BELista.NoFim()) {
                                _BSO.DesfazTransaccao();
                                listBox_Output.Items.Add($"Cód. da Taxa de Iva associada à Taxa na linha {k} inexistente!\nOPERAÇÃO CANCELADA!");
                                return false;
                            }

                            var (Artigo, Descricao, Qtd, PrecU, CodIva, TaxaIva, Desc) = (Arr[1], Arr[2], double.Parse(Arr[3]), double.Parse(Arr[4]), BELista.Valor("Iva").ToString(), double.Parse(Arr[5]), double.Parse(Arr[6]));
                            string Armazem = ""; string Localizacao = "";

                            _BSO.Vendas.Documentos.AdicionaLinha(docVenda, Artigo, ref Qtd, ref Armazem, ref Localizacao, PrecU, Desc);

                            VndBELinhaDocumentoVenda LinhaDoc = docVenda.Linhas.GetEdita(docVenda.Linhas.NumItens);

                            LinhaDoc.Artigo = Artigo;
                            LinhaDoc.Descricao = Descricao;
                            LinhaDoc.CodIva = CodIva;
                            LinhaDoc.TaxaIva = (float)TaxaIva;
                            LinhaDoc.Desconto1 = Desc;

                            k++;

                            if (k >= linhasLista.Count) {
                                UltimaLinha = true;
                                k--;
                            }

                            BELista.Dispose();
                        }

                        _BSO.Vendas.Documentos.CalculaValoresTotais(docVenda);

                        int vdDadosPrestacao = (int)BasBETiposGcp.PreencheRelacaoVendas.vdDadosPrestacao;
                        docVenda = _BSO.Vendas.Documentos.PreencheDadosRelacionados(docVenda, ref vdDadosPrestacao);

                        string strErro = "", strAvisos = "", serieDocLiq = docVenda.Serie;
                        if (_BSO.Vendas.Documentos.ValidaActualizacao(docVenda, _BSO.Vendas.TabVendas.Edita(docVenda.Tipodoc), ref serieDocLiq, ref strErro)) {
                            _BSO.Vendas.Documentos.Actualiza(docVenda, ref strAvisos);
                            listBox_Output.Items.Add($"Inserção bem sucedida da Factura {docVenda.NumDoc}.{strAvisos}");
                        } else {
                            _BSO.DesfazTransaccao();
                            listBox_Output.Items.Add($"Ocorreram erros ao gerar a Factura para o cliente {linhasLista[i]}: {strErro}\nOPERAÇÃO CANCELADA!");
                            return false;
                        }
                        docVenda= null;
                    }
                    i++;
                }
                _BSO.TerminaTransaccao();
                listBox_Output.Items.Add($"OPERAÇÃO REALIZADA COM EXITO!");
                return true;
            }
            catch (Exception ex) {
                _BSO.DesfazTransaccao();
                listBox_Output.Items.Add($"Ocorreram Erros: {ex.Message}\nOPERAÇÃO CANCELADA!");
                return false;
            }
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

