Attribute VB_Name = "StockQuebras"

Public arrTipoDoc As New ArrayList '1-D Array com tipos de documentos a validar
Public arrTDU, arrSerie As Variant '2-D Array com conte�do integral da TDU_OperadorQuebra
Public arrTDUFiltrado As New ArrayList '1-D Array com Operadores baseado no arrTDU ap�s identifica��o dos pares Operador-Armaz�m relevantes
Public formActivo As Boolean 'Usados para controlo de estado do FormQuebra1 e controlo de fluxo
Public apagaLinha As Boolean 'Usado para apagar linha caso haja dados insuficientes. Obriga a que Operador e Motivo sejam preenchidos
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

'Carrega TDU_OperadorQuebras para array de modo a s� fazer uma consulta � BD.
Sub CarregarTDU_OperadorQuebras()
    
    Dim recordset As ADODB.recordset
    Const query = "SELECT * FROM TDU_OperadorQuebra ;"
    
    Set recordset = BSO.DSO.BDAPL.Execute(query)
    recordset.MoveFirst
    arrTDU = recordset.GetRows
    
End Sub

'Filtra o arrTDU por �rmazem de acordo com o valor na LinhaSTK e preenche ArrayList com Operador correspondente.
'A lista � usada para popular a cboxOperadorQuebra no FormQuebra1.
Sub Filtrar_arrTDU(ByVal armazemLinha As String)

    arrTDUFiltrado.Clear
    
    For i = LBound(arrTDU, 2) To UBound(arrTDU, 2)
        If arrTDU(1, i) = armazemLinha Then
            arrTDUFiltrado.Add (arrTDU(0, i))
        End If
    Next i
    
End Sub


'------ FUN��ES N�O USADAS NESTE CONTEXTO ------
'Encontra uma string num array 1-D
'Function ExisteNoArray(ByVal stringAEncontrar As String, ByVal arr As Variant) As Boolean
    
'    ExisteNoArray = (UBound(Filter(arr, stringAEncontrar)) > -1)
    
'End Function

'Array 1-D com tipos de documento relevantes (quebras).
'Editar aqui se houver altera��es aos tipo de documentos.
'Function CriarArrTipoDoc()
    
'    arrTipoDoc.Clear
'    arrTipoDoc.Add ("SSQ")
'    arrTipoDoc.Add ("SSQL")
'    arrTipoDoc.Add ("SSQP")
    
'End Function
