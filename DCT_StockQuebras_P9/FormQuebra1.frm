VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} FormQuebra1 
   Caption         =   "Quebras"
   ClientHeight    =   3165
   ClientLeft      =   45
   ClientTop       =   390
   ClientWidth     =   4710
   OleObjectBlob   =   "FormQuebra1.frx":0000
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "FormQuebra1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False


Private Sub btnConfirmar_Click()
    
    Dim operador, motivo As String
    
    If tboxMotivoQuebra.Text = "" Then
        Dim resposta As Integer
        resposta = MsgBox("É necessário introduzir um motivo. A linha será apagada. Deseja continuar?", vbQuestion + vbYesNo + vbDefaultButton2, "Cancelar submissão")
        
        If resposta = vbYes Then
            Fechar
        End If
    Else
        operador = cboxOperadorQuebra.Value
        motivo = tboxMotivoQuebra.Text
        EditorStocks.DocumentoStock.Linhas(nLinha).CamposUtil("CDU_OperadorQuebra").Valor = operador
        EditorStocks.DocumentoStock.Linhas(nLinha).CamposUtil("CDU_MotivoQuebra").Valor = motivo
        Me.Hide
    End If
    
End Sub

Private Sub UserForm_Initialize()

    Me.cboxOperadorQuebra.Clear
    Me.tboxMotivoQuebra.Text = ""
        
    For Each op In arrTDUFiltrado
        Me.cboxOperadorQuebra.AddItem (op)
    Next
    
    cboxOperadorQuebra.ListIndex = 0
    formActivo = True

End Sub

Private Sub btnCancelar_Click()
    
    Fechar
           
End Sub


Sub Fechar()

    Me.Hide
    If Not EditorStocks.DocumentoStock.Linhas(nLinha).IdLinhaPai = "" Then
        apagaPai = True
    Else
        apagaLinha = True
    End If

End Sub
