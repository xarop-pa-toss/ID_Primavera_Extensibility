using BasBE100;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using System.Data;
using VndBE100;


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
                string kgsPorCaixa = _Helpers.MostraInputForm("Quilos", "Quilos por caixa:", art.CamposUtil["CDU_KilosPorCaixa"].ToString());

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
                        "SELECT MoradaAlternativa, Morada, Morada2, Localidade, CP, CPLocalidade, Distrito From MoradasAlternativasClientes Where (Cliente = N'" + DocumentoVenda.Entidade + "') And (MoradaAlternativa = N'" + morInput + "');");

                    if (moradaTbl.Rows.Count > 0)
                    {
                        DocumentoVenda.Morada = moradaTbl.Rows[0]["Morada"].ToString();
                        DocumentoVenda.Localidade = moradaTbl.Rows[0]["Localidade"].ToString();
                        DocumentoVenda.Morada2 = moradaTbl.Rows[0]["Morada2"].ToString();
                        DocumentoVenda.CodigoPostal = moradaTbl.Rows[0]["CP"].ToString();
                        DocumentoVenda.LocalidadeCodigoPostal = moradaTbl.Rows[0]["CPLocalidade"].ToString();
                    }
                }
                moradaTbl.Dispose();
                cliente.Dispose();
            }


            DataTable serieTbl = _Helpers.GetDataTableDeSQL(
                "SELECT CDU_PedeVendedor, CDU_PedeDocumento, CDU_PedeMatricula" +
                "FROM SeriesVendas " +
               $"WHERE TipoDoc = '{DocumentoVenda.Tipodoc}' AND Serie = '{DocumentoVenda.Serie}';");

            if (DocumentoVenda.DataHoraDescarga == null)
            {
                DocumentoVenda.DataHoraDescarga = DocumentoVenda.DataHoraCarga.AddSeconds(10);
            }

            if (serieTbl.Rows.Count > 0)
            {
                if ((bool)serieTbl.Rows[0]["CDU_PedeVendedor"])
                {
                    string vendedorInput = _Helpers.MostraInputForm("Vendedor","Código de Vendedor:", "0");
                    DataTable vendedorTbl = _Helpers.GetDataTableDeSQL(
                        $"SELECT Vendedor FROM Vendedores WHERE Vendedor = '{vendedorInput}';");

                    if (vendedorTbl.Rows.Count > 0)
                        DocumentoVenda.Responsavel = vendedorInput;
                    else
                        PSO.MensagensDialogos.MostraAviso($"Vendedor '{vendedorInput}' inexistente!", StdPlatBS100.StdBSTipos.IconId.PRI_Exclama);

                    vendedorTbl.Dispose();
                }

                if ((bool)serieTbl.Rows[0]["CDU_PedeDocumento"])
                {
                    string nrDocInput = _Helpers.MostraInputForm("Documento", "Nº de documento manual:", "");
                    DocumentoVenda.CamposUtil["CDU_NroManual"].Valor = nrDocInput.Trim().Substring(0, 10);
                    DocumentoVenda.DocsOriginais = nrDocInput.Trim().Substring(0, 10);
                }

                if ((bool)serieTbl.Rows[0]["CDU_PedeMatricula"])
                {
                    if (DocumentoVenda.TipoEntidade == "C")
                    {
                        if (cliente.CamposUtil["CDU_MatriculaHabitual"].Valor.ToString().Length > 0 && DocumentoVenda.Matricula == "")
                        {
                            DocumentoVenda.Matricula = cliente.CamposUtil["CDU_MatriculaHabitual"].Valor.ToString();
                        }

                        string matricula = _Helpers.MostraInputForm("Matrícula da viatura:", "Matrícula", DocumentoVenda.Matricula);
                        DocumentoVenda.Matricula = matricula;

                        cliente.Dispose();
                    }
                }
            }
        }
    }
}
