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

        //private List<string> _artigoCaixaJMList = new List<string> { "147CF2", "147F02", "147F03" };
        private static DataTable _CaixasJMTabela = new DataTable();
        private static List<VndBELinhaDocumentoVenda> _linhasAbaixoDasCaixasList = new List<VndBELinhaDocumentoVenda>();


        public CaixasJM() 
        {
            _BSO = _Helpers.BSO; 
            _PSO = _Helpers.PSO;

            Reset();
        }

        private void Reset()
        {
            if (_CaixasJMTabela != null) { _CaixasJMTabela.Clear(); }

            _CaixasJMTabela = new DataTable();
            _CaixasJMTabela.Columns.Add("ArtigoCaixa", typeof(string));
            _CaixasJMTabela.Columns.Add("Quantidade", typeof(double));

            if (_linhasAbaixoDasCaixasList != null) { _linhasAbaixoDasCaixasList.Clear(); }
        }

        internal void PreencherLinhasDoc(VndBEDocumentoVenda docVenda)
        {
            PreencherEstruturasDados(docVenda.Linhas);

            #region Apagar linhas com caixas e linhas abaixo
            // Apaga todas as linhas abaixo da primeira que seja caixa inclusivé. Serão rescritas com valores recalculados
            for (int i = 1; i <= docVenda.Linhas.NumItens; i++)
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

            #region Escrever novas linhas
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

                if (string.IsNullOrEmpty(artigoCaixa)) { continue; }


                DataRow linha = _CaixasJMTabela.Rows.Find(new object[] { "ArtigoCaixa", artigoCaixa });
                
                if (linha != null)
                {
                    // Update linha existente na tabela
                    linha["Quantidade"] = (double)linha["Quantidade"] + linhaDoc.Quantidade;
                    linhasAposCaixas = true;
                } 
                else
                {
                    // Adiciona linha
                    DataRow novaLinha = _CaixasJMTabela.NewRow();
                    novaLinha["ArtigoCaixa"] = artigo;
                    novaLinha["Quantidade"] = linhaDoc.Quantidade;
                    _CaixasJMTabela.Rows.Add(novaLinha);

                    linhasAposCaixas = true;
                }

                // Check se linha vem depois das linhas de Caixas
                if (linhasAposCaixas)
                {
                    _linhasAbaixoDasCaixasList.Add(linhaDoc);
                }
            }
        }
    }
}
