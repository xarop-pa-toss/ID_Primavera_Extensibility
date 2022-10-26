VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "EditorStocks"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = True

Dim num As Integer
Dim FormQuebra As New FormQuebra1


Sub EditorStocks_TipoDocumentoIdentificado(Tipo As String, Cancel As Boolean)

    If formActivo = True Then
        Unload FormQuebra
        Set FormQuebra = Nothing
    End If
    
    'Popula arrTDU
    CarregarTDU_OperadorQuebras
    'Popula arrSerie
    CarregarSerie
    
    'Inicialização / Reset de variáveis de controlo
    apagaLinha , apagaPai = False

End Sub

Sub EditorStocks_ArtigoIdentificado(Artigo As String, NumLinha As Long, Cancel As Boolean)
    
    nLinha = NumLinha
    
    If ValidarSerie(Me.DocumentoStock.tipoDoc, Me.DocumentoStock.serie) Then
        If Me.DocumentoStock.Linhas(NumLinha).TipoLinha = "10" Then
            
            Filtrar_arrTDU (Me.DocumentoStock.Linhas(NumLinha).Armazem)
            FormQuebra.Show vbModal
            
            If apagaLinha Then
                Me.DocumentoStock.Linhas.Remove (NumLinha)
                apagaLinha = False
            End If
                
        End If
    End If
    
End Sub

Private Sub EditorStocks_ValidaLinha(NumLinha As Long)
    
    If apagaPai Then
        EliminarPai_e_Filhos
        apagaPai = False
    End If
End Sub
