using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using Primavera.Extensibility.Sales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using StdBE100;
using IBasBS100;
using ErpBS100;
using System.Windows.Forms;


namespace ASRLB_ImportacaoFatura
{
    public partial class janelaImportarFatura : CustomForm
    {

        public void btnIniciar_Click(object sender, EventArgs e)
        {
            // Lê conteudo da txtBox e valida se ficheiro existe ou é nulo.
            // Copia o ficheiro para o mesmo directório e retorna o path da cópia.
            string ficheiro = validarPath();

            // Cria Array com cada linha do ficheiro e valida os valores.
            criarArray(ficheiro);

            /* Validação e commit dos valores no Array com os presentes no Primavera.
            Ao contrário do script no P9 que acede às tabelas F4 com cada registo,
            colocamos em memória os valores dessas tabelas, tornando o processo
            mais rápido e leve.*/
            //processamentoDados();
        }


        internal string validarPath()
        {
            string path;
            if (txtFicheiroPath.Text == "import.txt no servidor")
            {
                path = @"C:\Users\Ricardo Santos\Documents\import.txt";
            }
            else { path = txtFicheiroPath.Text; }

            if (!File.Exists(path) || path == "")
            {
                PSO.Dialogos.MostraErro("O ficheiro não existe no caminho específicado");
            }

            // Faz cópia do ficheiro para o mesmo directório. Vamos trabalhar na cópia e não o original.
            string ultimaPasta = Path.GetDirectoryName(path);
            string copiaPath = @"" + ultimaPasta + @"\importCopia.txt";
            File.Copy(path, copiaPath);

            return copiaPath;
        }

        internal void criarArray(string ficheiro)
        {
            // Criar Array com linhas do ficheiro
            string[] linhasFicheiro = File.ReadAllLines(@"" + ficheiro);

            // Métodos Primavera que devolvem listas internas
            StdBELista proxyArtigos = BSO.Base.Artigos.LstArtigos();
            StdBELista proxyClientes = BSO.Base.Clientes.LstClientes();
            StdBELista proxyCondPag = BSO.Base.CondsPagamento.LstCondsPagamento();

            // Criar listas fáceis de iterar para evitar chamar métodos Primavera para cada comparação
            List<string> listaArtigos = new List<string>();
            List<string> listaClientes = new List<string>();
            List<string> listaCondPag = new List<string>();

            for (int n = 0; proxyArtigos.NumLinhas() > n; n++)
            {
                listaArtigos.Add(proxyArtigos.DaValor<string>("Artigo"));
            }

            for (int n = 0; proxyClientes.NumLinhas() > n; n++)
            {
                listaClientes.Add(proxyClientes.DaValor<string>("Cliente"));
            }

            for (int n = 0; proxyCondPag.NumLinhas() > n; n++)
            {
                listaCondPag.Add(proxyCondPag.DaValor<string>("CondPag"));
            }

        }

        private void btnEscolherFicheiro_Click(object sender, EventArgs e)
        {
            OpenFileDialog form = new OpenFileDialog();

            form.Filter = "Ficheiros .txt (*.txt)|(*.txt)";
            form.FilterIndex = 1;
            form.Multiselect = false;

            if(form.ShowDialog() == DialogResult.OK)
            {
                string ficheiro = form.FileName;
                txtFicheiroPath.Text = ficheiro;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
 



        
    



