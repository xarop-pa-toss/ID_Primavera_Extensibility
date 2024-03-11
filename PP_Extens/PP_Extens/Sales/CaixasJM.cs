using ErpBS100;
using HelpersPrimavera10;
using StdPlatBS100;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VndBE100;
using static System.Windows.Forms.LinkLabel;

namespace PP_Extens.Sales
{
    internal class CaixasJM
    {
        private HelperFunctions _Helpers = new HelperFunctions();
        private ErpBS _BSO;
        private StdBSInterfPub _PSO;

        private static DataTable _CaixasJMTabela = new DataTable();
        private static List<VndBELinhaDocumentoVenda> _linhasAbaixoDasCaixasList = new List<VndBELinhaDocumentoVenda>();
        private List<string> _linhasCriadasNoDocList = new List<string>();


        public CaixasJM() 
        {
            _BSO = PriMotores.Motor;
            _PSO = PriMotores.Plataforma;

            Reset();
        }

        private void Reset()
        {
            if (_CaixasJMTabela != null) { _CaixasJMTabela.Clear(); }

            _CaixasJMTabela = new DataTable();
            _CaixasJMTabela.Columns.Add("ArtigoCaixa", typeof(string));
            _CaixasJMTabela.Columns.Add("Quantidade", typeof(double));

            // Definir coluna primária para conseguir usar o Rows.Find()
            DataColumn[] colunaPrimaria = new DataColumn[1];
            colunaPrimaria[0] = _CaixasJMTabela.Columns["ArtigoCaixa"];
            _CaixasJMTabela.PrimaryKey = colunaPrimaria;

            if (_linhasAbaixoDasCaixasList != null) { _linhasAbaixoDasCaixasList.Clear(); }
        }

        internal void ActualizarListaIdLinha(VndBELinhasDocumentoVenda linhas)
        {
            #region Actualizar a Lista de IdLinha (todas as linhas que já existiram no doc).
            // Usado para saber se o ValidaLinha está a actualizar uma linha já existente ou uma nova.
            // Só mantém linhas com artigos e que não sejam caixas.

            foreach (VndBELinhaDocumentoVenda linha in linhas)
            {
                if (!_linhasCriadasNoDocList.Contains(linha.IdLinha) && (linha.TipoLinha == "10" || linha.Descricao == " "))
                {
                    _linhasCriadasNoDocList.Add(linha.IdLinha);
                }
            }
            
            #endregion
        }

        internal void PreencherLinhasDoc(VndBEDocumentoVenda docVenda)
        {
            // Interessam aqui dois casos:
            // Actualização de Valor - ValidaLinha activou numa linha já existente (no caso de actualização de um valor), a linha "em branco" irá existir entre os artigos e as caixas.
            // Linha Nova - ValidaLinha activou na linha entre os artigos e as caixas ou por baixo das caixas.
            // Tem de se discernir entre eles para saber se se cria uma linha em branco ou não.
            // De qualquer forma, todas as linhas de caixa e as abaixo são apagadas e reinseridas com recalculos de quantidade

            // Começamos por recalcular todas as quantidades de CaixasJM (corre todas as linhas do documento até aos artigo caixa).
            // Guardamos em memória também quaisquer linhas que existam abaixo das de artigo caixa.
            GuardarLinhas(docVenda.Linhas);

            #region Apagar linha em branco + linhas caixas + linhas abaixo
            bool paraApagar = false;
            bool adicionarLinhaEmBranco = true;
            List<int> linhasParaRemover = new List<int>();

            for (int i = 1; i <= docVenda.Linhas.NumItens; i++)
            {
                VndBELinhaDocumentoVenda linha = docVenda.Linhas.GetEdita(i);
                if (linha.Artigo.StartsWith("147")) {
                    paraApagar = true; 
                }

                if (linha.TipoLinha == "60" && string.IsNullOrWhiteSpace(linha.Descricao)) { 
                    paraApagar = false;
                    adicionarLinhaEmBranco = false;
                }

                if (paraApagar) { 
                    linhasParaRemover.Add(i); 
                }
            }

            linhasParaRemover.Reverse();
            foreach (int linhaIndice in linhasParaRemover) { docVenda.Linhas.Remove(linhaIndice); }
            #endregion

            #region Escrever linha em branco + linhas de caixas + linhas abaixo
            if (adicionarLinhaEmBranco) {
                _BSO.Vendas.Documentos.AdicionaLinhaEspecial(docVenda, BasBE100.BasBETiposGcp.vdTipoLinhaEspecial.vdLinha_Comentario, 0, " ");
            }

            foreach (DataRow linhaCaixa in _CaixasJMTabela.Rows)
            {
                double quantidadeCaixa = (double)linhaCaixa["Quantidade"];
                _BSO.Vendas.Documentos.AdicionaLinha(docVenda, linhaCaixa["ArtigoCaixa"].ToString(), ref quantidadeCaixa);
            }

            foreach (VndBELinhaDocumentoVenda linhaAbaixoCaixa in _linhasAbaixoDasCaixasList)
            {
                docVenda.Linhas.Add(linhaAbaixoCaixa);
            }
            #endregion
        }

        private void GuardarLinhas(VndBELinhasDocumentoVenda linhasDoc)
        {
            Reset();

            bool linhasAbaixoCaixas = false;

            foreach (VndBELinhaDocumentoVenda linhaDoc in linhasDoc)
            {
                string artigo = linhaDoc.Artigo;
                string artigoCaixa = _BSO.Base.Artigos.DaValorAtributo(artigo, "CDU_CaixaJM");
                double quantidadeCaixas = Convert.ToDouble(linhaDoc.CamposUtil["CDU_Caixas"].Valor);

                // 1 - Skip linha se não for um artigo com CaixaJM ou se não for uma linha que exista abaixo de uma linha de caixa
                if (!string.IsNullOrEmpty(artigoCaixa))
                {
                    DataRow linha = _CaixasJMTabela.Rows.Find(new object[] { artigoCaixa });
                    if (linha != null)
                    {
                        // Update linha existente na tabela
                        linha["Quantidade"] = (double)linha["Quantidade"] + quantidadeCaixas;
                        linhasAbaixoCaixas = true;
                    } else
                    {
                        // Adiciona linha
                        DataRow novaLinha = _CaixasJMTabela.NewRow();
                        novaLinha["ArtigoCaixa"] = artigoCaixa;
                        novaLinha["Quantidade"] = quantidadeCaixas;
                        _CaixasJMTabela.Rows.Add(novaLinha);

                        linhasAbaixoCaixas = true;
                    }
                }
                else if (linhasAbaixoCaixas && !linhaDoc.Artigo.StartsWith("147"))
                {
                    _linhasAbaixoDasCaixasList.Add(linhaDoc);
                    continue;
                }
            }
        }
    }
}
