
Imports System.Data.OleDb
Imports System.IO
Imports System
Imports System.Windows.Forms

Imports System.Data.SqlClient   '' System.Data.OleDb
Imports Microsoft.VisualBasic.Compatibility
''Imports ADODB
Module DataGridModule
    Public OleDa As New OleDbDataAdapter()
    Public DtaSet As New DataSet()

    Dim GridColumn1 As New DataGridViewTextBoxColumn()
    Dim GridColumn2 As New DataGridViewTextBoxColumn()
    Dim GridColumn3 As New DataGridViewTextBoxColumn()
    Dim GridColumn4 As New DataGridViewTextBoxColumn()
    Dim GridColumn5 As New DataGridViewTextBoxColumn()

    'Customize Datagridview...
    Public Sub DataConnection(ByVal pSprdView As DataGridView, ByVal pTableName As String, _
                              ByVal pFieldName1 As String, ByVal pHeaderName1 As String, _
                              ByVal pFieldName2 As String, ByVal pHeaderName2 As String, _
                              ByVal pFieldName3 As String, ByVal pHeaderName3 As String, _
                              ByVal pFieldName4 As String, ByVal pHeaderName4 As String, _
                              ByVal pFieldName5 As String, ByVal pHeaderName5 As String, _
                              ByVal pDataGridViewSelectionMode As Long)

        Dim mTotalCol As Long

        mTotalCol = IIf(pFieldName1 <> "", 1, 0) + IIf(pFieldName2 <> "", 1, 0) + IIf(pFieldName3 <> "", 1, 0) + IIf(pFieldName4 <> "", 1, 0) + IIf(pFieldName5 <> "", 1, 0)

        'FullRowSelect=1, CellSelect=0
        pSprdView.AutoGenerateColumns = False
        With GridColumn1
            .DataPropertyName = pFieldName1
            .HeaderText = pHeaderName1
            .Width = 95
        End With
        With GridColumn2
            .DataPropertyName = pFieldName2
            .HeaderText = pHeaderName2
            .Width = 100
        End With
        With GridColumn3
            .DataPropertyName = pFieldName3
            .HeaderText = pHeaderName3
            .Width = 95
        End With
        With GridColumn4
            .DataPropertyName = pFieldName4
            .HeaderText = pHeaderName4
            .Width = 100
        End With

        With GridColumn5
            .DataPropertyName = pFieldName5
            .HeaderText = pHeaderName5
            .Width = 100
        End With

        With pSprdView
            '.Columns.Clear()
            .DataSource = DtaSet
            .DataMember = pTableName
            .ReadOnly = True
            .MultiSelect = False
            .SelectionMode = pDataGridViewSelectionMode '' DataGridViewSelectionMode.FullRowSelect
            .ShowRowErrors = False
            .ShowCellErrors = False
            .AllowUserToAddRows = False ' Disabled or hide (*) Symbol...
            .AllowUserToResizeColumns = False 'Disable HearderText Resize Column...
            .AllowUserToResizeRows = False 'Disabled  row resize...
            .RowHeadersVisible = False 'To hide Left indicator...
            .DefaultCellStyle.SelectionBackColor = Color.SkyBlue 'Selection backcolor....
            .AlternatingRowsDefaultCellStyle.BackColor = Color.LightGoldenrodYellow 'Alternating Backcolor

            If mTotalCol = 1 Then
                .Columns.AddRange(New DataGridViewColumn() {GridColumn1})
            ElseIf mTotalCol = 2 Then
                .Columns.AddRange(New DataGridViewColumn() {GridColumn1, GridColumn2})
            ElseIf mTotalCol = 3 Then
                .Columns.AddRange(New DataGridViewColumn() {GridColumn1, GridColumn2, GridColumn3})
            ElseIf mTotalCol = 4 Then
                .Columns.AddRange(New DataGridViewColumn() {GridColumn1, GridColumn2, GridColumn3, GridColumn4})
            Else
                .Columns.AddRange(New DataGridViewColumn() {GridColumn1, GridColumn2, GridColumn3, GridColumn4, GridColumn5})
            End If
        End With
    End Sub
    
End Module
