Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb

Friend Class frmMRRReceived
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15
    Private Const ColMRRNo As Short = 1
    Private Const ColMRRDate As Short = 2
    Private Const ColRefType As Short = 3
    Private Const ColPartyName As Short = 4
    Private Const ColReceivedFlag As Short = 5
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()

    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String
        Dim mMRRNo As Double
        Dim mUpdateCount As Integer
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColMRRNo
                mMRRNo = CDbl(Trim(.Text))
                .Col = ColReceivedFlag
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    SqlStr = "UPDATE INV_GATE_HDR SET SEND_AC_FLAG='Y',UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_MRR=" & mMRRNo & ""
                    PubDBCn.Execute(SqlStr)
                    mUpdateCount = mUpdateCount + 1
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " MRR Received.", MsgBoxStyle.Information)
        Exit Sub
ErrPart:
        ''Resume
        ErrorMsg(Err.Description, err.number, MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        OptSelection(1).Checked = True
        Show1()
        FormatSprdMain()
        'cmdShow.Enabled = False
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.Focus() : Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmMRRReceived_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmMRRReceived_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        Dim SqlStr As String
        Dim RS As ADODB.Recordset


        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        cboDivision.Items.Clear()
        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        cboDivision.Items.Add("ALL")
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboDivision.SelectedIndex = 0

        SqlStr = "Select DISTINCT SUPP_CUST_NAME, SUPP_CUST_CODE, SUPP_CUST_ADDR,  SUPP_CUST_CITY, SUPP_CUST_STATE " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY SUPP_CUST_NAME"

        oledbCnn = New OleDbConnection(StrConn)
        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        cboAccount.DataSource = ds
        cboAccount.DataMember = ""
        'cmbCompany.ValueMember = "COMPANY_CODE"
        'cmbCompany.DisplayMember = "Company Name"

        cboAccount.Appearance.FontData.SizeInPoints = 8.5

        cboAccount.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Name"
        cboAccount.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"
        cboAccount.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Address"
        cboAccount.DisplayLayout.Bands(0).Columns(3).Header.Caption = "City"
        cboAccount.DisplayLayout.Bands(0).Columns(4).Header.Caption = "State"
        ''cboAccount.DisplayLayout.Bands(0).Columns(4).Header.Caption = "GST No"

        cboAccount.DisplayLayout.Bands(0).Columns(0).Width = 350
        cboAccount.DisplayLayout.Bands(0).Columns(1).Width = 100
        cboAccount.DisplayLayout.Bands(0).Columns(2).Width = 350
        cboAccount.DisplayLayout.Bands(0).Columns(3).Width = 100
        cboAccount.DisplayLayout.Bands(0).Columns(4).Width = 100

        cboAccount.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5


        cboAccount.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        'cboCompany.Rows(0).Selected = True


        oledbAdapter.Dispose()
        oledbCnn.Close()

        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        FormatSprdMain()
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mDivision As Double

        SqlStr = "SELECT GRMain.AUTO_KEY_MRR,GRMain.MRR_DATE,  " & vbCrLf _
            & " CASE WHEN GRMain.REF_TYPE='C' THEN 'CASH' " & vbCrLf _
            & " WHEN GRMain.REF_TYPE='D' THEN 'DS' " & vbCrLf _
            & " WHEN GRMain.REF_TYPE='F' THEN 'FOC' " & vbCrLf _
            & " WHEN GRMain.REF_TYPE='J' THEN 'JOBWORK' " & vbCrLf _
            & " WHEN GRMain.REF_TYPE='P' THEN 'PO' " & vbCrLf _
            & " WHEN GRMain.REF_TYPE='R' THEN 'RGP' " & vbCrLf _
            & " WHEN GRMain.REF_TYPE='I' THEN 'SR' " & vbCrLf _
            & " WHEN GRMain.REF_TYPE='1' THEN 'JR' END AS REFTYPE, " & vbCrLf _
            & " ACM.SUPP_CUST_NAME,'' " & vbCrLf _
            & " FROM INV_GATE_HDR GRMain,FIN_SUPP_CUST_MST ACM" & vbCrLf _
            & " WHERE GRMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND GRMain.Company_Code=ACM.Company_Code " & vbCrLf _
            & " AND GRMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND GRMain.MRR_FINAL_FLAG='N' AND GRMain.SEND_AC_FLAG='N' "

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND GRMain.DIV_CODE=" & mDivision & ""
            End If
        End If
        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND GRMain.SEND_AC_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND GRMain.SEND_AC_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If Trim(cboAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.SUPP_CUST_NAME ='" & Trim(cboAccount.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY GRMain.AUTO_KEY_MRR"
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()
        With SprdMain
            .MaxCols = ColReceivedFlag
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)
            .set_RowHeight(-1, RowHeight)
            .Row = -1
            .Col = ColMRRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMRRDate, 10)
            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMRRNo, 15)
            .Col = ColRefType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColRefType, 6)
            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 45)
            .Col = ColReceivedFlag
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColReceivedFlag, 8)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColPartyName)
            '    SprdMain.OperationMode = OperationModeSingle
            '    SprdMain.DAutoCellTypes = True
            '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '    SprdMain.GridColor = &HC00000
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0
            .Col = ColMRRNo
            .Text = "MRR No."
            .Col = ColMRRDate
            .Text = "MRR Date"
            .Col = ColRefType
            .Text = "Ref Type"
            .Col = ColPartyName
            .Text = "Party Name"
            .Col = ColReceivedFlag
            .Text = "Received (Yes/No)"
        End With
    End Sub
    Private Sub frmMRRReceived_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColReceivedFlag
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With
        End If
    End Sub
    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(TxtDateFrom.Text)) = False Then
        '        TxtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateTo.Text)) = False Then
        '        txtDateTo.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cboAccount_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs) Handles cboAccount.InitializeLayout
        Try
            e.Layout.Override.AllowRowFiltering = DefaultableBoolean.True
            e.Layout.Override.FilterUIType = FilterUIType.FilterRow
            'e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.ExternalSortSingle
            'e.Layout.Override.HeaderClickAction = HeaderClickAction.SortSingle
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cboAccount_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles cboAccount.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, cboAccount.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub
End Class
