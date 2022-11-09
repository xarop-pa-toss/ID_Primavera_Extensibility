Option Explicit On

Private Sub cmdIniciar_Click()
    Dim i As Integer
    Dim Res As String
    On Error GoTo erro

    strNomeCompleto = "C:\Users\Ricardo Santos\Documents\" & Trim(txtFicheiro.Text)

    List1.Clear

    If Dir(strNomeCompleto) = "" Then
        MsgBox "O ficheiro de Importação não existe na raíz do Disco C", vbInformation + vbOKOnly, "Informação"
    Exit Sub
    End If

    If Trim(txtFicheiro) = "" Then
        MsgBox "O ficheiro de Importação não foi definido!", vbInformation + vbOKOnly, "Informação"
    Exit Sub
    End If

    List1.AddItem "A fazer a cópia do ficheiro..."
strNovoNome = "" & Format(Now(), "ddMMyy") & Format(Now(), "HHmm") & ".txt"
    strNovoNomeCompleto = "C:\Users\Ricardo Santos\Documents\" & strNovoNome
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
    If Res <> "" Then
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

End Sub

Private Function Processar() As String
    Dim i As Integer
    Dim k As Integer
    Dim Doc As GcpBEDocumentoVenda
    Dim LinhaDoc As GcpBELinhaDocumentoVenda
    Dim Descricao As String
    Dim Qtd As Double
    Dim PrecU As Double
    Dim CodIva As String
    Dim TaxaIva As Double
    Dim Desc As Double
    Dim Arr
    Dim BELista As StdBELista
    Dim strErro As String
    Dim strAvisos As String
    Dim UltimaLinha As Boolean

    On Error GoTo erro

    i = 1
    UltimaLinha = False

    Aplicacao.BSO.IniciaTransaccao
    While i <= UBound(arrLinhas)
        k = i + 1
        If UCase(Trim(Mid(arrLinhas(i), 1, 2))) <> "TE" And Mid(arrLinhas(i), 1, 3) <> "***" Then
            'é cliente --> cabeçalho Fact
            
            Set Doc = New GcpBEDocumentoVenda
            
            With Doc
                .Entidade = Trim(Split(arrLinhas(i), ",")(0))
                .TipoEntidade = "C"
                .Tipodoc = "FTEVB"
                .Serie = BSO.Comercial.Series.DaSerieDefeito("V", "FTEVB")
            End With
            Set Doc = Aplicacao.BSO.Comercial.Vendas.PreencheDadosRelacionados(Doc, vdDadosTodos)
            With Doc
                .DataDoc = "04/02/2022"
                .CondPag = Trim(Split(arrLinhas(i), ",")(1))
            End With
            Aplicacao.BSO.Comercial.Vendas.PreencheDadosRelacionados Doc, vdDadosCondPag


            While UCase(Trim(Mid(arrLinhas(k), 1, 2))) = "TE" And Not UltimaLinha

                Arr = Split(arrLinhas(k), ",")

                If UBound(Arr) = 0 Then
                    Aplicacao.BSO.DesfazTransaccao
                    List1.AddItem "Linha Inconsistente: " & k & ". " & Chr(13) & "OPERAÇÃO CANCELADA!"
                    Processar = "A INFORMAÇÃO NÃO FOI IMPORTADA!"
                    Exit Function
                End If
                
                Set BELista = BSO.Consulta("SELECT Iva from IVA where taxa=" & Arr(4))
                If BELista.NoFim Then
                    Aplicacao.BSO.DesfazTransaccao
                    List1.AddItem "Cód. da Taxa de Iva associada à Taxa na linha " & k & " inexistente!" & Chr(13) & " OPERAÇÃO CANCELADA!"
                    Processar = "A INFORMAÇÃO NÃO FOI IMPORTADA!"
                    Set BELista = Nothing
                    Exit Function
                End If

                Descricao = Arr(1)
                Qtd = Val(Arr(2))
                PrecU = Val(Arr(3))
                CodIva = BELista("Iva")
                TaxaIva = Val(Arr(4))
                Desc = Val(Arr(5))

                Aplicacao.BSO.Comercial.Vendas.AdicionaLinha Doc, "TE", Qtd, , , PrecU, Desc
                
                Set LinhaDoc = Doc.Linhas.Edita(Doc.Linhas.NumItens)
                With LinhaDoc
                    .Descricao = Descricao
                    .CodIva = CodIva
                    .TaxaIva = TaxaIva
                    .Desconto1 = Desc
                End With

                'Aplicacao.BSO.Vendas.CalculaValoresTotais Doc
                k = k + 1
                If k > UBound(arrLinhas) Then
                    UltimaLinha = True
                    k = k - 1
                End If
                Set BELista = Nothing
            Wend
            
            Aplicacao.BSO.Comercial.Vendas.CalculaValoresTotais Doc
            Set Doc = Aplicacao.BSO.Comercial.Vendas.PreencheDadosRelacionados(Doc, vdDadosPrestacao)
            If Aplicacao.BSO.Comercial.Vendas.ValidaActualizacao(Doc, Aplicacao.BSO.Comercial.TabVendas.Edita(Doc.Tipodoc), Doc.Serie, strErro) Then
                Aplicacao.BSO.Comercial.Vendas.Actualiza Doc, strAvisos
                List1.AddItem "Inserção bem sucedida da Factura " & Doc.NumDoc & "." & strAvisos
            Else
                Aplicacao.BSO.DesfazTransaccao
                List1.AddItem "Ocorreram erros ao gerar a Factura para o cliente " & arrLinhas(i) & ": " & strErro & ""
                    Processar = "A INFORMAÇÃO NÃO FOI IMPORTADA!"
                Exit Function
            End If
            
            Set Doc = Nothing
    End If
        i = k
Wend
Aplicacao.BSO.TerminaTransaccao

    Exit Function

erro:
    Aplicacao.BSO.DesfazTransaccao
    List1.AddItem "Ocorreram Erros: " & Err.Description & Chr(13) & ". OPERAÇÃO CANCELADA!"
    Processar = "A INFORMAÇÃO NÃO FOI IMPORTADA!"

End Function

Private Sub LER_ParaArray(strFich As String)
    Dim FNUM As Integer

    Dim i As Integer
    Dim k As Integer
    Dim jaVerificado As Boolean

    ExistemErros = False

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
        ElseIf UCase(Trim(Mid(arrLinhas(i), 1, 2))) = "TE" And jaVerificado = False Then
            'É uma linha de Factura
            jaVerificado = True
            If Aplicacao.BSO.Comercial.Artigos.Existe("TE") = False Then
                k = k + 1
                ReDim Preserve ArrErros(1 To k)
                ArrErros(k) = "Erro: Produto inexistente - TE"
                ExistemErros = True
            End If
        ElseIf UCase(Trim(Mid(arrLinhas(i), 1, 2))) <> "TE" And Mid(arrLinhas(i), 1, 2) <> "***" Then
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

End Sub


Private Sub cmdSair_Click()
    Unload Me
End Sub

Private Sub Image1_Click()

End Sub

Private Sub List1_Click()

End Sub

Private Sub UserForm_Click()

End Sub
