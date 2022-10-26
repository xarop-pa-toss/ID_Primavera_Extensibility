Attribute VB_Name = "StockQuebras"

Public arrTipoDoc As New ArrayList '1-D Array com tipos de documentos a validar
Public arrTDU, arrSerie As Variant '2-D Array com conteúdo integral da TDU_OperadorQuebra
Public arrTDUFiltrado As New ArrayList '1-D Array com Operadores baseado no arrTDU após identificação dos pares Operador-Armazém relevantes
Public formActivo As Boolean 'Usados para controlo de estado do FormQuebra1 e controlo de fluxo
Public apagaLinha As Boolean ' DEVE SER IMPLEMENTADO no ValidaLinha no EditorStocks. Usado para apagar linha caso haja dados insuficientes. Obriga a que Operador e Motivo sejam preenchidos
Public apagaPai As Boolean 'Apaga linha pai e filhos se um dos filhos não for válido.
Public nLinha As Long


Sub CarregarSerie()
    
    Dim recordset As ADODB.recordset
    Const query = "SELECT TipoDoc, Serie, CDU_PedeOperador_Motivo FROM SeriesStocks WHERE DataInicial >= '2022-01-01' AND CDU_PedeOperador_Motivo = 1 ; "
    
    Set recordset = BSO.DSO.BDAPL.Execute(query)
    recordset.MoveFirst
    arrSerie = recordset.GetRows
    
End Sub

Function ValidarSerie(ByVal tipoDoc As String, ByVal serie As String) As Boolean
    
    For i = LBound(arrSerie, 2) To UBound(arrSerie, 2)
        If arrSerie(0, i) = tipoDoc And arrSerie(1, i) = serie And arrSerie(2, i) = True Then
            ValidarSerie = True
            Exit For
        End If
    Next i
    
End Function

'Carrega TDU_OperadorQuebras para array de modo a só fazer uma consulta à BD.
Sub CarregarTDU_OperadorQuebras()
    
    Dim recordset As ADODB.recordset
    Const query = "SELECT * FROM TDU_OperadorQuebra ;"
    
    Set recordset = BSO.DSO.BDAPL.Execute(query)
    recordset.MoveFirst
    arrTDU = recordset.GetRows
    
End Sub

'Filtra o arrTDU por Ármazem de acordo com o valor na LinhaSTK e preenche ArrayList com Operador correspondente.
'A lista é usada para popular a cboxOperadorQuebra no FormQuebra1.
Sub Filtrar_arrTDU(ByVal armazemLinha As String)

    arrTDUFiltrado.Clear
    
    For i = LBound(arrTDU, 2) To UBound(arrTDU, 2)
        If arrTDU(1, i) = armazemLinha Then
            arrTDUFiltrado.Add (arrTDU(0, i))
        End If
    Next i
    
End Sub

'Apaga linhas pai e irmaos se linha original for filho (quando FormQuebra é cancelado)
'Corre as linhas anteriores do documento até encontrar pai (linha pai tem IdLinhaPai = ""), apagando tudo até lá inclusivé
Sub EliminarPai_e_Filhos()
    
    Dim k As Long
    k = EditorStocks.DocumentoStock.Linhas.NumItens
    
    'Apaga sequencialmente da última linha pra cima até ao Pai exclusivé
    While Not EditorStocks.DocumentoStock.Linhas(k).IdLinhaPai = ""
        EditorStocks.DocumentoStock.Linhas.Remove (k)
        k = k - 1
    Wend
    'Apaga pai
    EditorStocks.DocumentoStock.Linhas.Remove (k)
    
End Sub
