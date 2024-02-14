using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using BasBE100;
using System.Data;
using VndBE100;
using System.Runtime.InteropServices;


namespace PP_Qualidade.Sales
{
    public class UiEditorVendas : EditorVendas
    {
        HelpersPrimavera10.HelperFunctions _Helpers = new HelpersPrimavera10.HelperFunctions();

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);
        
            BasBEArtigo art = BSO.Base.Artigos.Edita(Artigo);
            VndBELinhaDocumentoVenda linha = DocumentoVenda.Linhas.GetEdita(NumLinha);

            if ((bool)art.CamposUtil["CDU_VendaEmCaixa"].Valor)
            {
                string kgsPorCaixa = _Helpers.MostraInputForm("Quilos", "Quilos por caixa:" , art.CamposUtil["CDU_KilosPorCaixa"].ToString());

                if (decimal.TryParse(kgsPorCaixa, out decimal kilosPorCaixa))
                {
                    linha.CamposUtil["CDU_KilosPorCaixa"].Valor = kilosPorCaixa;
                } else
                {
                    PSO.MensagensDialogos.MostraAviso($"'{kgsPorCaixa}' não é um valor válido.", StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);
                    art.Dispose();
                    Cancel = true;
                    return;
                }

                string caixasNum = _Helpers.MostraInputForm("Caixas", "Nro. de caixas:", "");

                if (int.TryParse(caixasNum, out int caixas))
                {
                    linha.CamposUtil["CDU_Caixas"].Valor = caixas;
                    linha.Quantidade = caixas;
                } else
                {
                    PSO.MensagensDialogos.MostraAviso($"'{caixasNum}' não é um valor válido.", StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);
                    art.Dispose();
                    Cancel = true;
                    return;
                }
            }

            using (DataTable fornecedorTbl = _Helpers.GetDataTableDeSQL(
                $"SELECT CDU_PedeFornecedor" +
                $"FROM SeriesVendas" +
                $"WHERE TipoDoc = '{DocumentoVenda.Tipodoc}' And Serie = '{DocumentoVenda.Serie}'"))
            {
                if (fornecedorTbl.Rows.Count > 0 && (bool)fornecedorTbl.Rows[0]["CDU_PedeFornecedor"] == true)
                {
                    string fornecedor = _Helpers.MostraInputForm("Proveniencia", "Fornecedor:", linha.CamposUtil["CDU_Fornecedor"].Valor.ToString());
                    linha.CamposUtil["CDU_Fornecedor"].Valor = fornecedor.Substring(0, 20);
                }
            }

            if ((bool)art.CamposUtil["CDU_MemorizaLote"].Valor)
            {
                string lote = _Helpers.MostraInputForm("Lote", "Introduza o lote do artigo:", art.CamposUtil["CDU_UltimoLote"].Valor.ToString());
                lote = lote.Trim().ToUpper().PadRight(25);
                linha.CamposUtil["CDU_LoteAux"].Valor = lote;

                if (lote != art.CamposUtil["CDU_UltimoLote"].Valor.ToString())
                {
                    art.CamposUtil["CDU_UltimoLote"].Valor = lote;
                    BSO.Base.Artigos.Actualiza(art);
                }
            }

            art.Dispose();
            linha.CamposUtil["CDU_DescricaoBase"].Valor = DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao;
        }

        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);

            string nrDoc;
            string s;
            string matricula;
            string mor;
            ADODB.Recordset morada;
            ADODB.Recordset vend;
            ADODB.Recordset serie;
            BasBECliente cliente = BSO.Base.Clientes.Edita(Cliente);

            if ((bool)cliente.CamposUtil["CDU_DiversasMoradas"].Valor)
            {
                DataTable moradaTbl = _Helpers.GetDataTableDeSQL(
                    "SELECT MoradaAlternativa, Morada, Localidade, CP, CPLocalidade, Distrito" +
                    "FROM MoradasAlternativasClientes" +
                   $"WHERE Cliente = N'{DocumentoVenda.Entidade}';");

                if (moradaTbl.Rows.Count > 0)
                {
                    string morInput = _Helpers.MostraInputForm("Código da Morada", "Morada do documento:", moradaTbl.Rows[0]["MoradaAlternativa"].ToString());
                    moradaTbl = _Helpers.GetDataTableDeSQL(
                        "SELECT MoradaAlternativa, Morada, Morada2, Localidade, CP, CPLocalidade, Distrito From MoradasAlternativasClientes Where (Cliente = N'" + DocumentoVenda.Entidade + "') And (MoradaAlternativa = N'" + mor + "');");

                    if (!morada.EOF)
                    {
                        DocumentoVenda.morada = CStr(nz(morada!morada));
                        DocumentoVenda.Localidade = CStr(nz(morada!Localidade));
                        DocumentoVenda.Morada2 = CStr(nz(morada!Morada2));
                        DocumentoVenda.CodigoPostal = CStr(nz(morada!cp));
                        DocumentoVenda.LocalidadeCodigoPostal = CStr(nz(morada!cplocalidade));
                    }
                }

                morada.Close();
            }

            cli = null;

            serie = Aplicacao.BSO.DSO.BDAPL.Execute("Select CDU_PedeVendedor, CDU_PedeDocumento, CDU_PedeMatricula From SeriesVendas Where TipoDoc = '" + DocumentoVenda.TipoDoc + "' And Serie = '" + DocumentoVenda.Serie + "';");

            if (nz(DocumentoVenda.DataDescarga) == "")
            {
                DocumentoVenda.DataDescarga = DocumentoVenda.DataCarga;
            }

            if (!serie.EOF)
            {
                if (serie!CDU_PedeVendedor)
        {
                    s = _Helpers.MostraInputForm("Código de Vendedor:", "Vendedor", "0");
                    vend = Aplicacao.BSO.DSO.BDAPL.Execute("SELECT Vendedor FROM Vendedores WHERE Vendedor = '" + s + "';");

                    if (vend.EOF)
                    {
                        MsgBox("Vendedor '" + s + "' inexistente!", vbExclamation + vbOKOnly);
                    } else
                    {
                        DocumentoVenda.Responsavel = s;
                    }

                    vend.Close();
                }

                if (serie!CDU_PedeDocumento)
        {
                    nrDoc = _Helpers.MostraInputForm("Nº de documento manual:", "Documento");
                    DocumentoVenda.CamposUtil.Item("CDU_NroManual").Valor = Left(Trim(nrDoc), 10);
                    DocumentoVenda.DocsOriginais = Left(Trim(nrDoc), 10);
                }

                if (serie!CDU_PedeMatricula)
        {
                    if (DocumentoVenda.TipoEntidade == "C")
                    {
                        cli = Aplicacao.BSO.Comercial.Clientes.Edita(cliente);

                        if ((Len(nz(cli.CamposUtil("CDU_MatriculaHabitual"))) > 0) && (nz(DocumentoVenda.matricula) == ""))
                        {
                            DocumentoVenda.matricula = cli.CamposUtil("CDU_MatriculaHabitual");
                        }

                        matricula = _Helpers.MostraInputForm("Matrícula da viatura:", , nz(DocumentoVenda.matricula));
                        DocumentoVenda.matricula = nz(matricula);

                        cli = null;
                    }
                }
            }
        }
    }
}
