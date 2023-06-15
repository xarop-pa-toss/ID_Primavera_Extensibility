VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} FormReimpressao 
   Caption         =   "Reimpressão de Documentos de Venda"
   ClientHeight    =   6780
   ClientLeft      =   45
   ClientTop       =   390
   ClientWidth     =   8910
   OleObjectBlob   =   "FormReimpressao.frx":0000
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "FormReimpressao"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False



Option Base 1
Option Compare Text

Dim DataInicial As Date
Dim DataFinal As Date
Dim NumInicial As Long
Dim NumFinal As Long
Dim tabela As String
Dim TipoDoc As String
Dim Serie As String
Dim RSt As ADODB.recordset
Dim SDKContexto As clsSDKContexto
'


Private Sub UserForm_Initialize()
    
    ' Inicializar SDK
    If SDKContexto Is Nothing Then
        Set SDKContexto = New clsSDKContexto
    
        ' Inicializar SDKContexto com objecto BSO e módulo correspondente
        SDKContexto.Inicializa BSO, "ERP"
        PSO.InicializaPlataforma SDKContexto
        
        ' Associar Controlos Primavera ao SDK
        PriGrelhaDocs.Inicializa SDKContexto
    End If
     
    '--- Inicialização de controlos ---
    InicializaPriGrelha
    
    ' ListBox com tipos de documentos
    InicializaListBoxTipoDoc
    
    ' Datas de documento -> dia 1 e último dia do mês corrente
    DTPickerDataDocInicial.Value = Format(DateSerial(Year(Date), Month(Date), 1), "dd/MM/yyyy")
    DTPickerDataDocFinal.Value = Format(DateAdd("d", -1, DateSerial(Year(Date), Month(Date) + 1, 1)), "dd/MM/yyyy")
    
    ' Numeros de documento
    TxtBoxNumDocInicial.Value = 0
    TxtBoxNumDocFinal.Value = 999999
    
    ' Mapas de impressão
    InicializaCBoxMapa
    
End Sub
'

Private Sub BtnActualizar_Click()
    ActualizaPriGrelha
End Sub

Private Sub CBoxTipoDoc_Change()
    InicializaCBoxSerie
End Sub
'

Private Sub InicializaListBoxTipoDoc()

    Dim sqlCommand As String
    Dim rcSet As ADODB.recordset
    
    ListBoxTipoDoc.Clear
    
    'Preencher ListBox
    sqlCommand = "SELECT Documento + ' - ' + Descricao AS Doc FROM DocumentosVenda WHERE Inactivo = 0 ORDER BY Documento DESC;"
    Set rcSet = Aplicacao.BSO.DSO.BDAPL.Execute(sqlCommand)
    
    rcSet.MoveFirst
    
    While Not rcSet.EOF
        ListBoxTipoDoc.AddItem rcSet("Doc")
        rcSet.MoveNext
    Wend
    
    Set rcSet = Nothing

End Sub
'

Private Sub InicializaCBoxMapa()

    Dim sqlCommand As String
    Dim rcSet As ADODB.recordset
    
    CBoxMapa.Clear
    
    sqlCommand = "SELECT Descricao " + _
                 "FROM [PRIEMPRE].[dbo].[Mapas] " + _
                 "WHERE " + _
                    "Categoria = 'DocVenda' " + _
                    " AND Apl = 'GCP' " + _
                    " AND Custom = 1;"
                    
    Set rcSet = Aplicacao.BSO.DSO.BDAPL.Execute(sqlCommand)
    
    rcSet.MoveFirst
    
    While Not rcSet.EOF
        CBoxMapa.AddItem rcSet("Descricao")
        rcSet.MoveNext
    Wend
    
    Set rcSet = Nothing
    
End Sub
'

Private Sub InicializaPriGrelha()
    
        PriGrelhaDocs.TituloGrelha = "DocsReimpressao"
        PriGrelhaDocs.PermiteActualizar = True
        PriGrelhaDocs.PermiteAgrupamentosUser = True
        PriGrelhaDocs.PermiteScrollBars = True
        PriGrelhaDocs.PermiteVistas = False
        PriGrelhaDocs.PermiteEdicao = False
        PriGrelhaDocs.PermiteDataFill = False
        PriGrelhaDocs.PermiteFiltros = False
        PriGrelhaDocs.PermiteActiveBar = False
        PriGrelhaDocs.PermiteContextoVazia = False
    
        ' Colunas da tabela de reimpressão
    ' Cf - CheckBox - define se vai imprimir ou não
    ' Data - Date - data de emissão do documento
    ' Doc (DrillDown) - Str - TipoDoc
    ' Série - Str
    ' Numero (DrillDown) - Long/Int
    ' Tipo Entidade - Str
    ' Entidade (DrillDown) - Str
    ' Moeda - Str/Moeda
    ' Total - Double/Float - Valor total do Doc
    ' Imp - Simbolo - se já foi impresso ou não
    
    'Private Enum ColType
    'SS_CELL_TYPE_DATE = 0
    'SS_CELL_TYPE_EDIT = 1
    'SS_CELL_TYPE_FLOAT = 2
    'SS_CELL_TYPE_INTEGER = 3
    'SS_CELL_TYPE_PIC = 4
    'SS_CELL_TYPE_STATIC_TEXT = 5
    'SS_CELL_TYPE_TIME = 6
    'SS_CELL_TYPE_BUTTON = 7
    'SS_CELL_TYPE_COMBOBOX = 8
    'SS_CELL_TYPE_PICTURE = 9
    'SS_CELL_TYPE_CHECKBOX = 10
    'SS_CELL_TYPE_OWNER_DRAWN = 11
    'End Enum

        PriGrelhaDocs.AddColKey strColkey:="Cf", intTipo:=10, strTitulo:="Cf.", dblLargura:=3, blnMostraSempre:=True, blnVisivel:=True
        PriGrelhaDocs.AddColKey strColkey:="Data", intTipo:=5, strTitulo:="Data", dblLargura:=15, strCamposBaseDados:="Data", blnMostraSempre:=True
        PriGrelhaDocs.AddColKey strColkey:="TipoDoc", intTipo:=5, strTitulo:="Doc", dblLargura:=5, strCamposBaseDados:="TipoDoc", blnDrillDown:=True, blnMostraSempre:=True
        PriGrelhaDocs.AddColKey strColkey:="Serie", intTipo:=5, strTitulo:="Serie", dblLargura:=5, strCamposBaseDados:="Serie", blnMostraSempre:=True
        PriGrelhaDocs.AddColKey strColkey:="NumDoc", intTipo:=5, strTitulo:="Numero", dblLargura:=8, strCamposBaseDados:="NumDoc", blnDrillDown:=True, blnMostraSempre:=True
        PriGrelhaDocs.AddColKey strColkey:="TipoEntidade", intTipo:=5, strTitulo:="Tipo Entidade", dblLargura:=3, strCamposBaseDados:="TipoEntidade", blnMostraSempre:=True
        PriGrelhaDocs.AddColKey strColkey:="Entidade", intTipo:=5, strTitulo:="Entidade", dblLargura:=8, strCamposBaseDados:="Entidade", blnDrillDown:=True, blnMostraSempre:=True
        PriGrelhaDocs.AddColKey strColkey:="Moeda", intTipo:=5, strTitulo:="Moeda", dblLargura:=5, strCamposBaseDados:="Moeda", blnDrillDown:=True, blnMostraSempre:=True
        PriGrelhaDocs.AddColKey strColkey:="TotalDocumento", intTipo:=2, strTitulo:="Total", dblLargura:=8, strCamposBaseDados:="TotalDocumento", blnMostraSempre:=True
End Sub
'

Private Sub ActualizaPriGrelha()
    
    ' *** Valores dos controlos ***
    DataInicial = DTPickerDataDocInicial.Value
    DataFinal = DTPickerDataDocFinal.Value
    NumInicial = TxtBoxNumDocInicial.Value
    NumFinal = TxtBoxNumDocFinal.Value
    
    ' Se item seleccionado na listbox -> adiciona a TipoDoc (string) com uma vírgula. A usar na cláusula IN da query SQL abaixo
    Dim i As Integer
    For i = 0 To ListBoxTipoDoc.ListCount - 1
        If ListBoxTipoDoc.Selected(i) Then
            ' Para separar o acrónimo da descrição, usamos a função Split para pôr ambos num array
            Dim partes As Variant
            partes = Split(ListBoxTipoDoc.List(i), " - ")
            TipoDoc = TipoDoc & "'" & partes(0) & "',"
        End If
    Next
    
    If TipoDoc = "" Then
        Exit Sub
    Else
        TipoDoc = Left(TipoDoc, Len(TipoDoc) - 1) 'Remove vírgula que fica à direita da última entrada na string
    End If
    
    ' *** SQL QUERY ***
    ' Utilizar um dicionário para criar o comando SQL já que não existe um StringBuilder em VBA
    Dim sqlDict As Object
    Set sqlDict = CreateObject("Scripting.Dictionary")
    
    ' Criar cada parte da query
    ' A coluna Cf recebe NULL pq a Prigrelha estava a dar problemas se a query não tivesse exactamente a mesma quantidade de colunas que a grelha em si
    sqlDict.Add "select", "SELECT NULL AS Cf, Convert(varchar, Data, 103) AS Data, TipoDoc, Serie, NumDoc, TipoEntidade, Entidade, Moeda, TotalDocumento"
    sqlDict.Add "from", "FROM CabecDoc WHERE"
    sqlDict.Add "whereData", "Data BETWEEN CONVERT(datetime, '" & DataInicial & "', 103) AND CONVERT(datetime, '" & DataFinal & "', 103)"
    sqlDict.Add "whereTipoDoc", "AND TipoDoc IN (" & TipoDoc & ")"
    sqlDict.Add "order", "ORDER BY TipoDoc, NumDoc DESC;"
    
    ' Juntar as strings para formar a Query
    Dim sqlCommand As String
    sqlCommand = Join(sqlDict.Items, " ")
    '
    
    ' *** PRIGRELHA DATABIND E QUERY EXECUTE ***
    Dim rcSet As Variant
    Set rcSet = SDKContexto.BSO.Consulta(sqlCommand)
    
    PriGrelhaDocs.LimpaGrelha
    PriGrelhaDocs.DataBind (rcSet)
End Sub
'
Private Sub BtnImprimir_Click()
    
    Dim i As Integer
    Dim linha As Variant
    Dim chkboxValor As Variant
    Dim falhasImpressao As String
    Dim mapa As String
    
    falhasImpressao = ""
    i = 0
    
    ' Get codigo do mapa para Impressao
        Dim sqlCommand As String
        Dim rcSet As ADODB.recordset
        
        sqlCommand = "SELECT Mapa " + _
                     "FROM [PRIEMPRE].[dbo].[Mapas] " + _
                     "WHERE " + _
                        " Descricao = '" & CBoxMapa.Text & "' " + _
                        " AND Categoria = 'DocVenda' " + _
                        " AND Apl = 'GCP' " + _
                        " AND Custom = 1;"
                        
        Set rcSet = Aplicacao.BSO.DSO.BDAPL.Execute(sqlCommand)
        rcSet.MoveFirst
        mapa = rcSet("Mapa")
        Set rcSet = Nothing
    
    ' Corre loop enquanto linhas da PriGrelha não forem vazias
    Do While True
        i = i + 1
          
        If PriGrelhaDocs.GRID_GetValorCelula(i, "NumDoc") = "" Then
            Exit Do
        End If

        PriGrelhaDocs.Grelha.GetText 1, i, chkboxValor
        
        If CBoxMapa.Text = "" Then
            MsgBox ("Não foi seleccionado um mapa.")
            Exit Sub
        End If
        
        If chkboxValor = 0 Then
            GoTo NextIteration
        End If
        
        ' Valores das colunas para identificar documento
        Dim gTipoDoc As String, gSerie As String, gNumDoc As Long
        
        gTipoDoc = PriGrelhaDocs.GRID_GetValorCelula(i, "TipoDoc")
        gSerie = PriGrelhaDocs.GRID_GetValorCelula(i, "Serie")
        gNumDoc = PriGrelhaDocs.GRID_GetValorCelula(i, "NumDoc")
        
        
        ' *** IMPRESSAO *** '
        Dim printed As Boolean
        Dim docVenda As Variant
        
        If ChkBoxPreVisualizar Then
            Dim path As String
            Dim filePermission As FileDialogPermission
            
            
        End If
        
        ' Imprime doc e verifica se foi bem sucedido
        printed = SDKContexto.BSO.Comercial.Vendas.ImprimeDocumento(gTipoDoc, gSerie, gNumDoc, "000", CInt(TxtBoxNumVias.Text), mapa)
        If Not printed Then
            falhasImpressao = falhasImpressao & ", " & gDoc & " " & gSerie & "/" & gNumero
        End If
        
NextIteration:
    Loop
    
    If Not falhasImpressao = "" Then
        MsgBox ("Documento(s) " & gTipoDoc & " " & gSerie & "/" & gNumDoc & " não foram imprimidos.")
    End If
    
    
End Sub
'


Private Sub UpDownNumVias_SpinDown()
    
    Dim currentVal As String, newVal As Integer
    
    currentVal = TxtBoxNumVias.Text
    
    If currentVal <= 1 Then
        TxtBoxNumVias.Text = "1"
    Else
        newVal = CInt(currentVal) - 1
        TxtBoxNumVias.Text = CStr(newVal)
    End If
    
End Sub

Private Sub UpDownNumVias_SpinUp()
    
        Dim currentVal As String, newVal As Integer
    
    currentVal = TxtBoxNumVias.Text
    newVal = CInt(currentVal) + 1
    TxtBoxNumVias.Text = CStr(newVal)
    
End Sub