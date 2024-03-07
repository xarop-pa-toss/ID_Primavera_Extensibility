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
            PreencherEstruturasDados(docVenda.Linhas);

            #region Apagar linhas relevantes
            // Interessam aqui dois casos:
            // Actualização de Valor - ValidaLinha activou numa linha já existente (no caso de actualização de um valor), a linha "em branco" irá existir entre os artigos e as caixas.
            // Linha Nova - ValidaLinha activou na linha entre os artigos e as caixas ou por baixo das caixas.
            // Tem de se discernir entre eles para saber se se cria uma linha em branco ou não.

            for (int i = docVenda.Linhas.NumItens; i >= 1; i--)
            {
                VndBELinhaDocumentoVenda linha = docVenda.Linhas.GetEdita(i);
                if (linha.Artigo.StartsWith("147"))
                {
                    for (int k = docVenda.Linhas.NumItens - i; k <= docVenda.Linhas.NumItens; k++) 
                    {
                        docVenda.Linhas.Remove(k);
                    }
                    break;
                }
            }
            #endregion

            #region Escrever linha em branco, linhas de Caixas, e linhas abaixo das de Caixas
            foreach (DataRow linhaCaixa in _CaixasJMTabela.Rows)
            {
                double quantidadeCaixa = (double)linhaCaixa["Quantidade"];

                _BSO.Vendas.Documentos.AdicionaLinha(docVenda, linhaCaixa["ArtigoCaixa"].ToString(), ref quantidadeCaixa);
            }

            foreach (VndBELinhaDocumentoVenda linhaAposCaixa in _linhasAbaixoDasCaixasList)
            {
                docVenda.Linhas.Add(linhaAposCaixa);
            }
            #endregion


        }

        private void PreencherEstruturasDados(VndBELinhasDocumentoVenda linhasDoc)
        {
            Reset();

            bool linhasAposCaixas = false;

            foreach (VndBELinhaDocumentoVenda linhaDoc in linhasDoc)
            {
                string artigo = linhaDoc.Artigo;
                string artigoCaixa = _BSO.Base.Artigos.DaValorAtributo(artigo, "CDU_CaixaJM");

                // 1 - Skip linha se não for um artigo com CaixaJM ou se não for uma linha que exista abaixo de uma linha de caixa
                if (!string.IsNullOrEmpty(artigoCaixa))
                {
                    DataRow linha = _CaixasJMTabela.Rows.Find(new object[] { artigoCaixa });
                    if (linha != null)
                    {
                        // Update linha existente na tabela
                        linha["Quantidade"] = (double)linha["Quantidade"] + linhaDoc.Quantidade;
                        linhasAposCaixas = true;
                    } else
                    {
                        // Adiciona linha
                        DataRow novaLinha = _CaixasJMTabela.NewRow();
                        novaLinha["ArtigoCaixa"] = artigoCaixa;
                        novaLinha["Quantidade"] = linhaDoc.Quantidade;
                        _CaixasJMTabela.Rows.Add(novaLinha);

                        linhasAposCaixas = true;
                    }
                }
                else if (linhasAposCaixas)
                {
                    _linhasAbaixoDasCaixasList.Add(linhaDoc);
                    continue;
                }
            }
        }
    }
}
