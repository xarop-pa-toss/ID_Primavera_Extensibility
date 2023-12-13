using Primavera.Extensibility.Sales.Editors; using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Extensions;
using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks; using System.Windows; using System.Runtime.InteropServices;
using System.Windows.Forms;
using PRISDK100;
using BasBE100; using StdBE100; using VndBE100; using StdPlatBS100.UserForms;
using StdPlatBS100;
using System.Runtime.CompilerServices;

/// <summary>
/// Conversão de PORTIPESCA/Forms/EditorVendas
/// </summary>
namespace PP_Extens.Sales
{
    public class PP_UiEditorVendas : EditorVendas
    {
        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            if (KeyCode == Convert.ToInt32(ConsoleKey.F3))
            {
                PreVisualizarSemGravarNovo();
            }
        }

        public override void TipoDocumentoIdentificado(string Tipo, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.TipoDocumentoIdentificado(Tipo, ref Cancel, e);

            if (Tipo == "ET")
            {
                DocumentoVenda.Serie = "A";
                DocumentoVenda.TipoEntidade = "C";
                DocumentoVenda.Entidade = "74";

                BSO.Vendas.Documentos.PreencheDadosRelacionados(DocumentoVenda);
            }

            FormServicoDados formServicoDados = new FormServicoDados();
        }

        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);
            PP_Geral geral = new PP_Geral();
            BasBECliente cli = BSO.Base.Clientes.Edita(Cliente);

            // Se Cliente tiver mais que uma morada alternativa, pede para distinguir. Se só tiver uma, escolhe essa automaticamente. Se não tiver nenhuma, avança sem efeito.
            if (cli.CamposUtil["CDU_DiversasMoradas"].Valor.Equals(true))
            {
                StdBELista morada = BSO.Consulta("SELECT MoradaAlternativa, Morada, Localidade, CP, CPLocalidade, Distrito FROM MoradasAlternativasClientes WHERE Cliente = N'" + DocumentoVenda.Entidade + "'; ");

                if (!morada.Vazia())
                {
                    string mor = null;

                    //InputForm inputForm_MoradaDoc = new InputForm("Morada do documento:", morada.Valor("moradaalternativa"), PSO, BSO);
                    //DialogResult resultado = inputForm_MoradaDoc.ShowDialog();
                    //if ( resultado == DialogResult.OK) { mor = inputForm_MoradaDoc.Resultado; }
                    //else if (resultado == DialogResult.Cancel) { mor = ""; }

                    //PSO.MensagensDialogos.MostraDialogoInput(ref mor, "Código da Morada", "Morada do documento:", strValorDefeito: morada.Valor("moradaalternativa"));
                    morada = BSO.Consulta("SELECT MoradaAlternativa, Morada, Morada2, Localidade, CP, CPLocalidade, Distrito FROM MoradasAlternativasClientes WHERE (Cliente = N'" + DocumentoVenda.Entidade + "') AND (MoradaAlternativa = N'" + mor + "');");

                    if (!morada.Vazia())
                    {
                        morada.Inicio();
                        DocumentoVenda.Morada = geral.nz(morada.Valor("morada"));
                        DocumentoVenda.Localidade = geral.nz(morada.Valor("localidade"));
                        DocumentoVenda.Morada2 = geral.nz(morada.Valor("morada2"));
                        DocumentoVenda.CodigoPostal = geral.nz(morada.Valor("cp"));
                        DocumentoVenda.LocalidadeCodigoPostal = geral.nz(morada.Valor("cplocalidade"));
                    }
                }
                morada.Termina();
            }
            cli = null; //

            // Se data/hora de descarga estiver vazia, fica igual à data/hora de carga
            string descarga = DocumentoVenda.DataHoraDescarga.ToString();

            // Pede Vendedor se a série o exigir
            StdBELista serie = BSO.Consulta("SELECT CDU_PedeVendedor, CDU_PedeDocumento, CDU_PedeMatricula FROM SeriesVendas WHERE TipoDoc = '" + DocumentoVenda.Tipodoc + "' AND Serie = '" + DocumentoVenda.Serie + "';");
            if (!serie.Vazia())
            {
                serie.Inicio();
                if (serie.Valor("CDU_PedeVendedor").Equals(true))
                {
                    string s = PP_Geral.MostraInputForm("Vendedor", "Código de Vendedor", "", true, BSO);

                    //InputForm inputForm = new InputForm("Código do vendedor:", "0", PSO, BSO);
                    //resultado = inputForm.ShowDialog();
                    //if (resultado == DialogResult.OK) { s = inputForm.Resultado; } else if (resultado == DialogResult.Cancel) { s = ""; }

                    //PSO.MensagensDialogos.MostraDialogoInput(ref s, "Vendedor", "Código de Vendedor:", strValorDefeito: "0");
                    StdBELista vend = BSO.Consulta("SELECT Vendedor FROM Vendedores WHERE Vendedor = '" + s + "';");

                    if (vend.Vazia()) { PSO.MensagensDialogos.MostraAviso("Vendedor " + s + " inexistente!", StdBSTipos.IconId.PRI_Exclama); }
                    else { DocumentoVenda.Responsavel = s; }
                    vend.Termina();
                }//

                // Pede Documento se a série o exigir
                if (serie.Valor("CDU_PedeDocumento").Equals(true))
                {
                    string nrDoc = PP_Geral.MostraInputForm("Documento", "Nº de documento manual:", "", true, BSO);

                    // Queremos os caracteres de 0 a 10. Substring dá erro se fizermos isso e a string de entrada for mais curta que 10 caracteres
                    // Math.Min devolve o menor dos dois valores.
                    int indexFim = Math.Min(10, nrDoc.Length);
                    string nrDocSubstring = nrDoc.Substring(0, indexFim);

                    DocumentoVenda.CamposUtil["CDU_NroManual"].Valor = nrDocSubstring;
                    DocumentoVenda.DocsOriginais = nrDocSubstring;
                }

                // Pede Matricula se o TipoEntidade for 'C'
                if (serie.Valor("CDU_PedeMatricula").Equals(true))
                {
                    if (DocumentoVenda.TipoEntidade == "C")
                    {
                        cli = BSO.Base.Clientes.Edita(Cliente);
                        string matriculaHabitual = cli.CamposUtil["CDU_MatriculaHabitual"].Valor.ToString();
                        string matricula = null;

                        if (geral.nz(ref matriculaHabitual).Length > 0 && geral.nz(ref matricula) == "") { DocumentoVenda.Matricula = geral.nz(ref matriculaHabitual); }

                        matricula = PP_Geral.MostraInputForm("Matricula", "Matricula da viatura:", geral.nz(ref matricula), true, BSO);
                        DocumentoVenda.Matricula = geral.nz(ref matricula);

                        cli = null;
                    }
                }
            }
            serie.Termina();

            // Pede para inserir Nota na Fatura se cliente o exigir
            if (DocumentoVenda.TipoEntidade == "C")
            {
                if (BSO.Vendas.TabVendas.DaValorAtributo(DocumentoVenda.Tipodoc, "TipoDocumento") == 4)
                {
                    string s = BSO.Base.Clientes.DaValorAtributo(DocumentoVenda.Entidade, "CDU_NotaFactura");
                    if (s.Length > 1)
                    {
                        s = PP_Geral.MostraInputForm("Nota", "Nota na Fatura", "", true, BSO);
                        DocumentoVenda.CamposUtil["CDU_NotaFactura"].Valor = geral.nz(ref s);
                    }
                }
            }
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);
            PP_Geral Geral = new PP_Geral();
            string s = null;
            string[] artArr = { "147", "147C", "147T" };

            VndBELinhaDocumentoVenda linha = DocumentoVenda.Linhas.GetEdita(NumLinha);
            if (artArr.Contains(linha.Artigo)) { linha.CamposUtil["CDU_Fornecedor"].Valor = "*0021"; }

            if (Convert.ToBoolean(BSO.Base.Series.DaValorAtributo("V", DocumentoVenda.Tipodoc, DocumentoVenda.Serie, "CDU_PedeFornecedor")))
            {
                string cdu = linha.CamposUtil["CDU_Fornecedor"].Valor.ToString();
                s = PP_Geral.MostraInputForm("Fornecedor", "Fornecedor:", Geral.nz(ref cdu), true, BSO);
                linha.CamposUtil["CDU_Fornecedor"].Valor = Geral.nz(ref s).Trim();
            }

            bool memUltLote = BSO.Base.Artigos.DaValorAtributo(Artigo, "CDU_MemorizaLote");
            if (memUltLote) { s = BSO.Base.Artigos.DaValorAtributo(Artigo, "CDU_UltimoLote"); }

            s = PP_Geral.MostraInputForm("Lote", "Introduza o lote do artigo:\n\n**** ATENÇÃO ****\n\nFornecedorMêsDia\nExemplo: 0010718", s, false, BSO);
            s = Geral.nz(ref s).ToUpper().Trim();
            linha.CamposUtil["CDU_LoteAux"].Valor = s;

            if (memUltLote)
            {
                StdBEExecSql sql = new StdBEExecSql();
                sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;  
                sql.Tabela = "Artigo";                          //UPDATE Artigo
                sql.AddCampo("CDU_UltimoLote", s);              // SET CDU_UltimoLote = s
                sql.AddCampo("Artigo", Artigo, true);           // WHERE Artigo (coluna) = Artigo (variavel)
                sql.AddQuery();
                PSO.ExecSql.Executa(sql);
                sql = null;
            }

            // Pergunta FORMA DE OBTENÇÃO do pescado
            Dictionary<string, string> obtencaoDict = new Dictionary<string, string>
            {
                {"1", "Aquicultura" },
                {"2", "Capturado, Anzóis e aparelhos de anzol" },
                {"3", "Capturado, Dragas" },
                {"4", "Capturado, Nassas e armadilhas" },
                {"5", "Capturado, Redes de arrastar" },
                {"6", "Capturado, Redes de cercar e redes de sacada" },
                {"7", "Capturado, Redes de emalhar e redes semelhantes" },
                {"8", "Capturado, Redes envolventes-arrastantes" },
                {"9", "Apanha" }
            };

            string descricao = Geral.ConstructorDescricaoEmLista(obtencaoDict);
            string resposta = ""; string obtencao = "";

            if (Convert.ToBoolean(linha.CamposUtil["CDU_Pescado"].Valor))
            {
                while (string.IsNullOrEmpty(obtencao))
                {
                    try
                    {
                        resposta = PP_Geral.MostraInputForm("Forma de Obtenção", descricao, "", false, BSO);
                        
                        if (string.IsNullOrEmpty(resposta)) { break; }
                        
                        obtencao = obtencaoDict[resposta];
                        linha.CamposUtil["CDU_FormaObtencao"].Valor = obtencao;
                        break;
                    }
                    catch (KeyNotFoundException)
                    {
                        PSO.MensagensDialogos.MostraAviso("Valor inválido no modo de obtenção.", StdBSTipos.IconId.PRI_Exclama);
                    }
                }
            }
        }

        public override void ValidaLinha(int NumLinha, ExtensibilityEventArgs e)
        {
            base.ValidaLinha(NumLinha, e);
            PP_Geral Geral = new PP_Geral();
            VndBELinhaDocumentoVenda linha = DocumentoVenda.Linhas.GetEdita(NumLinha);

            double precUnit = linha.PrecUnit;

            if (Convert.ToBoolean(linha.CamposUtil["CDU_Pescado"].Valor))
            {
                if (Geral.UnidadeCaixa(linha.Unidade))
                {
                    linha.CamposUtil["CDU_Caixas"].Valor = linha.Quantidade;
                    linha.Descricao = linha.CamposUtil["CDU_DescricaoBase"].Valor + " (" + BSO.Base.Unidades.DaValorAtributo(linha.Unidade, "Descricao") + ")";
                } else
                {
                    double caixas = Convert.ToDouble(linha.CamposUtil["CDU_Caixas"].Valor);
                    if (Geral.nzn(ref caixas) <= 0) { linha.CamposUtil["CDU_Caixas"].Valor = 0; }
                }
            }

            // Adicionado para o MSS - 12/04/2021
            if (DocumentoVenda.Tipodoc == "ET")
            {
                string fornecedor = linha.CamposUtil["CDU_LOTEAUX"].Valor.ToString();
                int length = Convert.ToInt16(fornecedor) - 4;
                linha.CamposUtil["CDU_Fornecedor"].Valor = fornecedor.Substring(0, length);

                string sqlstr = linha.CamposUtil["CDU_NomeCientifico"].Valor + ", " + linha.CamposUtil["CDU_FormaObtencao"].Valor + ", " + linha.CamposUtil["CDU_FormaObtencao"].Valor;
                linha.CamposUtil["CDU_LOTEAUX3"].Valor = sqlstr;

                // UPDATE da ficha de ARTIGO
                // Grava fornecedor na ficha do artigo no campo CDU_Fornecedor
                string forn = linha.CamposUtil["CDU_Fornecedor"].Valor.ToString();                
                StdBEExecSql sql = new StdBEExecSql();
                sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                sql.Tabela = "Artigo";                              //UPDATE Artigo
                sql.AddCampo("CDU_Fornecedor", forn);               // SET CDU_Fornecedor = forn
                sql.AddCampo("Artigo", linha.Artigo, true);         // WHERE Artigo = linha.Artigo
                sql.AddQuery();
                PSO.ExecSql.Executa(sql);

                // Grava DataUltimaActualização do artigo (com DateTime corrente)
                StdBEExecSql sql2 = new StdBEExecSql();
                sql2.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                sql2.Tabela = "Artigo";                              //UPDATE Artigo
                sql2.AddCampo("DataUltimaUtilizacao", DateTime.Now); // SET DataUltimaUtilizacao = data corrente
                sql2.AddCampo("Artigo", linha.Artigo, true);         // WHERE Artigo = linha.Artigo
                sql2.AddQuery();
                PSO.ExecSql.Executa(sql2);

                // Grava forma de obtenção do artigo
                string obt = linha.CamposUtil["CDU_FormaObtencao"].Valor.ToString();
                StdBEExecSql sql3 = new StdBEExecSql();
                sql3.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
                sql3.Tabela = "Artigo";                              //UPDATE Artigo
                sql3.AddCampo("CDU_FormaObtencao", obt); // SET DataUltimaUtilizacao = data corrente
                sql3.AddCampo("Artigo", linha.Artigo, true);         // WHERE Artigo = linha.Artigo
                sql3.AddQuery();
                PSO.ExecSql.Executa(sql3);

                sql = null; sql2 = null; sql3 = null;
                // FIM do UPDATE
            }
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)   
        {
            base.AntesDeGravar(ref Cancel, e);
            PP_Geral geral = new PP_Geral();
            VndBELinhasDocumentoVenda linhas = DocumentoVenda.Linhas;

            for (int i = 1; i < linhas.NumItens; i++)
            {
                string artigo = DocumentoVenda.Linhas.GetEdita(i).Artigo;
                if (!string.IsNullOrEmpty(geral.nz(ref artigo)))
                {
                    if (linhas.GetEdita(i).Unidade == "CX")
                    {
                        PSO.MensagensDialogos.MostraErro("Para que exista uma correta gestão dos stocks não se pode utilizar a unidade 'CX'!!", StdPlatBS100.StdBSTipos.IconId.PRI_Critico);
                        Cancel = true;
                        return;
                    }
                    if (geral.UnidadeCaixa(linhas.GetEdita(i).Unidade))
                    {
                        linhas.GetEdita(i).CamposUtil["CDU_VendaEmCaixa"].Valor = true;
                        linhas.GetEdita(i).CamposUtil["CDU_KilosPorCaixa"].Valor = geral.ObterKgDaUnidade(linhas.GetEdita(i).Unidade);
                        linhas.GetEdita(i).CamposUtil["CDU_Caixas"].Valor = linhas.GetEdita(i).Quantidade;
                    }
                    else
                    {
                        linhas.GetEdita(i).CamposUtil["CDU_VendaEmCaixa"].Valor = false;
                        linhas.GetEdita(i).CamposUtil["CDU_KilosPorCaixa"].Valor = 0;

                        int cx = Convert.ToInt16(linhas.GetEdita(i).CamposUtil["CDU_Caixas"].Valor);
                        if (geral.nzn(ref cx) <= 0) { linhas.GetEdita(i).CamposUtil["CDU_Caixas"].Valor = 0; }
                    }
                }
            } // for loop end

            bool cancelTTE = false;

            if (DocumentoVenda.Tipodoc == "ET") { cancelTTE = true; }
            else if (DocumentoVenda.EmModoEdicao == false && Cancel == false)
            {
                // Verifica se o campo CDU_SeloViatura está vazio
                if (string.IsNullOrEmpty(DocumentoVenda.CamposUtil["CDU_SeloViatura"].Valor.ToString()) && DocumentoVenda.Tipodoc == "GRP")
                {
                    string selo = PP_Geral.MostraInputForm("Selo", "Selo da viatura de transporte:", "", false, BSO);
                    DocumentoVenda.CamposUtil["CDU_SeloViatura"].Valor = selo;
                    }
                }

                //StdBELista query = BSO.Consulta("Select CDU_PedeMatricula From SeriesVendas Where TipoDoc = '" + DocumentoVenda.Tipodoc + "' And Serie = '" + DocumentoVenda.Serie + "';");

                //if (!query.Vazia())
                //{
                //    string m = DocumentoVenda.Matricula;
                //    string matricula = 

                //    if (geral.nz(ref matricula) != DocumentoVenda.Matricula) { DocumentoVenda.Matricula = geral.nz(ref matricula); }
                //}
                //query.Termina();

            // Cria string com número de linhas em sequência
            string s = "";
            if (linhas.NumItens > 0)
            {
                for (int i = 1; i < linhas.NumItens + 1; i++)
                {
                    string artigo = linhas.GetEdita(i).Artigo;
                    if (geral.nz(ref artigo) != "" && (linhas.GetEdita(i).Quantidade == 0 || linhas.GetEdita(i).PrecUnit == 0)) { s = s + " " + i.ToString(); }
                }
            }
            if (s.Trim() != "") {  }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            PP_CaixasJM CaixasJM = new PP_CaixasJM(PSO, BSO, DocumentoVenda);
            CaixasJM.ProcessarCaixas();

            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);
        }


        internal void PreVisualizarSemGravarNovo()
        {
            long nLinhas = DocumentoVenda.Linhas.NumItens, i;
            string tempGUID;

            // Valida num de linhas no Doc
            if (nLinhas == 0) { MessageBox.Show("Não há nenhum documento válido no Editor de Vendas!\n Certifique-se que o Editor de Vendas contém um documento com a Entidade preenchida e com pelo menos uma linha!"); return; }

            // Preenchimento das tabelas de utilizador usadas para a PreVisualização. Mostra a PreVisualização e depois apaga os conteudos das tabelas.
            StdBEExecSql sql = new StdBEExecSql();
            StdBEExecSql sql2 = new StdBEExecSql();
            VndBEDocumentoVenda dv = DocumentoVenda;
            tempGUID = PP_Geral.GetGUID();

            sql.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
            sql.Tabela = "PSI_TempCabecDoc";
            sql.AddCampo("id", tempGUID);
            sql.AddCampo("tipodoc", dv.Tipodoc);
            sql.AddCampo("data", dv.DataDoc);
            sql.AddCampo("entidade", dv.Entidade);
            sql.AddCampo("moeda", dv.Moeda);
            sql.AddCampo("requisicao", dv.Requisicao);
            sql.AddCampo("datavencimento", dv.DataVenc);
            sql.AddCampo("condpag", dv.CondPag);
            sql.AddCampo("respcobranca", dv.Responsavel);
            sql.AddCampo("modoexp", dv.ModoExp);
            sql.AddCampo("regimeiva", dv.RegimeIva);
            sql.AddCampo("numcontribuinte", dv.NumContribuinte);
            sql.AddCampo("nome", dv.Nome);
            sql.AddCampo("morada", dv.Morada);
            sql.AddCampo("morada2", dv.Morada2);
            sql.AddCampo("localidade", dv.Localidade);
            sql.AddCampo("codpostal", dv.CodigoPostal);
            sql.AddCampo("codpostallocalidade", dv.LocalidadeCodigoPostal);
            double totalmerc = dv.RegimeIva == "1" ? (dv.TotalMerc + dv.TotalIva) : dv.TotalMerc;
            sql.AddCampo("TotalMerc", totalmerc);
            sql.AddCampo("TotalIva", dv.TotalIva);
            sql.AddCampo("TotalDesc", dv.TotalDesc);
            sql.AddCampo("TotalOutros", dv.TotalOutros);
            sql.AddCampo("TotalRetencao", dv.TotalRetencao);
            sql.AddCampo("TotalRetencaoGarantia", dv.TotalRetencaoGarantia);
            sql.AddCampo("DescPag", dv.DescFinanceiro);
            sql.AddCampo("DescEntidade", dv.DescEntidade);
            
            sql.AddQuery();
            PSO.ExecSql.Executa(sql);
            sql.Dispose();

            sql2.tpQuery = StdBETipos.EnumTpQuery.tpUPDATE;
            sql2.Tabela = "PSI_TempLinhasDoc";
            for (int x = 1; x <= dv.Linhas.NumItens; x++)
            {
                VndBELinhaDocumentoVenda ldv = dv.Linhas.GetEdita(x);

                sql2.AddCampo("IdCabecDoc", tempGUID);
                sql2.AddCampo("NumLinha", x);
                sql2.AddCampo("TipoLinha", ldv.TipoLinha);
                sql2.AddCampo("artigo", ldv.Artigo);
                sql2.AddCampo("Quantidade", ldv.Quantidade);
                sql2.AddCampo("PrecUnit", ldv.PrecUnit);
                sql2.AddCampo("TaxaIva", ldv.TaxaIva);
                sql2.AddCampo("TaxaIvaEcotaxa", ldv.TaxaIvaEcotaxa);
                sql2.AddCampo("CodIva", ldv.CodIva);
                sql2.AddCampo("Desconto1", ldv.Desconto1);
                sql2.AddCampo("Desconto2", ldv.Desconto2);
                sql2.AddCampo("Desconto3", ldv.Desconto3);
                sql2.AddCampo("PrecoLiquido", ldv.PrecoLiquido);
                sql2.AddCampo("Descricao", ldv.Descricao);
                sql2.AddCampo("Unidade", ldv.Unidade);
                sql2.AddCampo("TotalIliquido", ldv.TotalIliquido);
                sql2.AddCampo("TotalDA", ldv.TotalDA);
                sql2.AddCampo("TotalDC", ldv.TotalDC);
                sql2.AddCampo("TotalDF", ldv.TotalDF);
                sql2.AddCampo("TotalIva", ldv.TotalIva);
                sql2.AddCampo("TotalEcotaxa", ldv.TotalEcotaxa);
                sql2.AddCampo("CDU_Pescado", ldv.CamposUtil["CDU_Pescado"].Valor);
                sql2.AddCampo("CDU_NomeCientifico", ldv.CamposUtil["CDU_NomeCientifico"].Valor);
                sql2.AddCampo("CDU_Origem", ldv.CamposUtil["CDU_Origem"].Valor);
                sql2.AddCampo("CDU_FormaObtencao", ldv.CamposUtil["CDU_FormaObtencao"].Valor);
                sql2.AddCampo("CDU_ZonaFAO", ldv.CamposUtil["CDU_ZonaFAO"].Valor);
                sql2.AddCampo("CDU_Caixas", ldv.CamposUtil["CDU_Caixas"].Valor);
                sql2.AddCampo("CDU_VendaEmCaixa", ldv.CamposUtil["CDU_VendaEmCaixa"].Valor);
                sql2.AddCampo("CDU_KilosPorCaixa", ldv.CamposUtil["CDU_KilosPorCaixa"].Valor);
                sql2.AddCampo("CDU_LoteAux", ldv.CamposUtil["CDU_LoteAux"].Valor);
                
                sql2.AddQuery();
                PSO.ExecSql.Executa(sql2);
                sql2.Dispose();
            }
            PSO.Mapas.Inicializar("ERP");
            PSO.Mapas.SetParametro("ID", tempGUID);
            PSO.Mapas.ImprimeListagem("PP_MR_02", eCultura: StdBETipos.EnumGlobalCultures.CULT_PT, blnImpressaoCheque: false);

            tempGUID = tempGUID.Substring(2, 36);
            BSO.Consulta("DELETE FROM PSI_TempLinhasDoc WHERE IdCabecDoc = '" + tempGUID + "'");
            BSO.Consulta("DELETE FROM PSI_TempCabecDoc WHERE Id = '" + tempGUID + "'");
            sql = null; sql2 = null;
        }
    }
}


