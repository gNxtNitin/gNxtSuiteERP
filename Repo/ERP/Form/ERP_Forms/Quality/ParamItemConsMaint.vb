Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamItemConsMaint
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColDate As Short = 1
    Private Const ColMaintType As Short = 2
    Private Const ColDept As Short = 3
    Private Const ColMachine As Short = 4
    Private Const ColItemCode As Short = 5
    Private Const ColDescription As Short = 6
    Private Const ColUom As Short = 7
    Private Const ColQty As Short = 8
    Private Const ColRate As Short = 9
    Private Const ColAmount As Short = 10

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboRprDate_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRprDate.SelectedIndexChanged
        If cboRprDate.Text = "None" Then
            txtDate1.Visible = False
            lblDate1.Visible = False
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboRprDate.Text = "Between" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = True
            lblDate2.Visible = True
        ElseIf cboRprDate.Text = "After" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboRprDate.Text = "Before" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboRprDate.Text = "On Date" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        End If
    End Sub

    Private Sub chkAllMachine_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllMachine.CheckStateChanged
        Call PrintStatus(False)
        If chkAllMachine.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtMachine.Enabled = False
            cmdSearchMachine.Enabled = False
        Else
            txtMachine.Enabled = True
            cmdSearchMachine.Enabled = True
        End If
    End Sub

    Private Sub chkAllItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItem.CheckStateChanged
        Call PrintStatus(False)
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItem.Enabled = False
            cmdSearchItem.Enabled = False
        Else
            txtItem.Enabled = True
            cmdSearchItem.Enabled = True
        End If
    End Sub

    Private Sub chkAllDept_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDept.CheckStateChanged
        Call PrintStatus(False)
        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDept.Enabled = False
            cmdSearchDept.Enabled = False
        Else
            txtDept.Enabled = True
            cmdSearchDept.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnItemConsMaint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnItemConsMaint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnItemConsMaint(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Item Consumption List during Maintenance"
        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDept.Text) <> "" Then
            mSubTitle = mSubTitle & " [Dept : " & txtDept.Text & " ]"
        End If
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItem.Text) <> "" Then
            mSubTitle = mSubTitle & " [Item : " & txtItem.Text & " ]"
        End If
        If chkAllMachine.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMachine.Text) <> "" Then
            mSubTitle = mSubTitle & " [Machine : " & txtMachine.Text & " ]"
        End If
        If cboRprDate.Text = "Between" Then
            mSubTitle = mSubTitle & " [Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"
        End If
        If cboRprDate.Text = "After" Then
            mSubTitle = mSubTitle & " [After  " & txtDate1.Text & " ]"
        End If
        If cboRprDate.Text = "Before" Then
            mSubTitle = mSubTitle & " [Before  " & txtDate1.Text & " ]"
        End If
        If cboRprDate.Text = "On Date" Then
            mSubTitle = mSubTitle & " [On  " & txtDate1.Text & " ]"
        End If

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemConsMaintLst.rpt"

        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols) = False Then GoTo ReportErr

        SqlStr = FetchRecordForReport()

        '    SqlStr = MakeSQL
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Function FillPrintDummyData(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer) As Boolean

        ''' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 1

            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                GridName.Col = FieldNum
                If FieldNum = 1 Then
                    SetData = "FIELD" & FieldCnt
                    GetData = "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                Else
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                End If
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " " & GetData & ") "
            PubDBCn.Execute(SqlStr)
NextRec:
        Next
        PubDBCn.CommitTrans()
        FillPrintDummyData = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillPrintDummyData = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function FetchRecordForReport() As String

        Dim mSqlStr As String

        mSqlStr = " SELECT * " & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr
    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        'On Error GoTo ErrPart
        'Dim SqlStr As String
        'Dim CntRow As Long
        'Dim mDocNo As Double
        'Dim mLCDate As String
        'Dim mCDate As String
        'Dim mFrequency As Double
        '
        '    PubDBCn.Errors.Clear
        '    PubDBCn.BeginTrans
        '
        '    With SprdMain
        '        For CntRow = 1 To .MaxRows
        '            .Row = CntRow
        '            .Col = ColDocNo
        '            mDocNo = Trim(.Text)
        '
        '            .Col = ColCompletionDate
        '            mLCDate = Format(.Text, "DD/MM/YYYY")
        '
        '            .Col = ColValFrequency
        '            mFrequency = Val(.Text)
        '
        '            If IsDate(mLCDate) Then
        '
        '                mCDate = DateAdd("d", (Val(mFrequency) * 30), mLCDate)
        '
        '                SqlStr = " UPDATE QAL_IMTE_MST SET " & vbCrLf _
        ''                            & " LCDATE=TO_DATE('" & vb6.Format(mLCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
        ''                            & " CDATE=TO_DATE('" & vb6.Format(mCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
        ''                            & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        ''                            & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        ''                            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''                            & " AND DOCNO =" & mDocNo & ""
        '
        '                PubDBCn.Execute SqlStr
        '             End If
        '        Next
        '    End With
        '
        '    PubDBCn.CommitTrans
        ''    CmdSave.Enabled = False
        '    Call cmdShow_Click
        'Exit Sub
        'ErrPart:
        '    ErrorMsg err.Number, err.Description, vbCritical
        '    PubDBCn.RollbackTrans
    End Sub

    Private Sub cmdSearchMachine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachine.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtMachine.Text, "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", , , SqlStr) = True Then
            txtMachine.Text = AcName
        End If
        txtMachine.Focus()
        Exit Sub
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtItem.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItem.Text = AcName
        End If
        txtItem.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtDept.Text, "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName
        End If
        txtDept.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamItemConsMaint_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Item Consumption List during Maintenance"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamItemConsMaint_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11565)

        Call FillCbo()

        chkAllDept.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllDept_CheckStateChanged(chkAllDept, New System.EventArgs())
        chkAllMachine.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllMachine_CheckStateChanged(chkAllMachine, New System.EventArgs())
        chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllItem_CheckStateChanged(chkAllItem, New System.EventArgs())
        OptOrderBy(0).Checked = True
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillCbo()
        cboRprDate.Items.Clear()
        cboRprDate.Items.Add("None")
        cboRprDate.Items.Add("Between")
        cboRprDate.Items.Add("After")
        cboRprDate.Items.Add("Before")
        cboRprDate.Items.Add("On Date")
        cboRprDate.SelectedIndex = 0

        cboMaintType.Items.Clear()
        cboMaintType.Items.Add("BreakDown")
        cboMaintType.Items.Add("Prev. Maint.")
        cboMaintType.Items.Add("Both")
        cboMaintType.SelectedIndex = 2
    End Sub

    Private Sub frmParamItemConsMaint_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub txtmachine_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachine.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtmachine_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachine.DoubleClick
        Call cmdSearchMachine_Click(cmdSearchMachine, New System.EventArgs())
    End Sub

    Private Sub txtmachine_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachine.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachine.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtmachine_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachine.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachine_Click(cmdSearchMachine, New System.EventArgs())
    End Sub

    Private Sub txtmachine_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachine.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtMachine.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtMachine.Text, "MACHINE_DESC", "MACHINE_NO", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Machine", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItem.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtItem_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItem.DoubleClick
        Call cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
    End Sub

    Private Sub txtItem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItem.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItem.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItem_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItem.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
    End Sub

    Private Sub txtItem_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItem.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtItem.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtItem.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Item", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Dept", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColAmount
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeDateSeparator = Asc("/")

            .Col = ColMaintType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColMachine
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeEditMultiLine = False

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeEditMultiLine = False

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeEditMultiLine = False

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''= OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim SqlStr As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, AData1, StrConn, "Y")

        '''********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1

        MakeSQL = " SELECT * FROM (" & vbCrLf & " SELECT A.COMPLETION_DATE AS E_DATE, 'B/D' AS MAINT_TYPE, C.DEPT_DESC, D.MACHINE_DESC, " & vbCrLf & " B.ITEM_CODE, E.ITEM_SHORT_DESC, B.ITEM_UOM, B.ITEM_QTY, B.ITEM_RATE, B.ITEM_AMOUNT " & vbCrLf & " FROM MAN_BREAKDOWN_HDR A, MAN_BREAKDOWN_DET B, " & vbCrLf & " PAY_DEPT_MST C, MAN_MACHINE_MST D, INV_ITEM_MST E " & vbCrLf & " WHERE A.AUTO_KEY_BDSLIP=B.AUTO_KEY_BDSLIP " & vbCrLf & " AND A.COMPANY_CODE=C.COMPANY_CODE AND A.FROM_DEPT_CODE=C.DEPT_CODE " & vbCrLf & " AND A.COMPANY_CODE=D.COMPANY_CODE AND A.MACHINE_NO=D.MACHINE_NO " & vbCrLf & " AND SUBSTR(B.AUTO_KEY_BDSLIP,LENGTH(B.AUTO_KEY_BDSLIP)-1,2)=E.COMPANY_CODE AND B.ITEM_CODE=E.ITEM_CODE" & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " UNION " & vbCrLf & " SELECT A.PM_DATE AS E_DATE, 'P/M' AS MAINT_TYPE, C.DEPT_DESC, D.MACHINE_DESC, " & vbCrLf & " B.ITEM_CODE, E.ITEM_SHORT_DESC, B.ITEM_UOM, B.ITEM_QTY, B.ITEM_RATE, B.ITEM_AMOUNT " & vbCrLf & " FROM MAN_MACHINE_PM_HDR A, MAN_MACHINE_PM_ITEM B, " & vbCrLf & " PAY_DEPT_MST C, MAN_MACHINE_MST D, INV_ITEM_MST E " & vbCrLf & " WHERE A.AUTO_KEY_PM=B.AUTO_KEY_PM " & vbCrLf & " AND A.COMPANY_CODE=D.COMPANY_CODE AND A.MACHINE_NO=D.MACHINE_NO " & vbCrLf & " AND D.COMPANY_CODE=C.COMPANY_CODE AND D.DEPT_CODE=C.DEPT_CODE " & vbCrLf & " AND SUBSTR(B.AUTO_KEY_PM,LENGTH(B.AUTO_KEY_PM)-1,2)=E.COMPANY_CODE AND B.ITEM_CODE=E.ITEM_CODE" & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ) ITEM_CONSUMP " & vbCrLf & " WHERE 1=1 "


        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDept.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND DEPT_DESC='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"
        End If
        If chkAllMachine.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMachine.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachine.Text) & "'"
        End If
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItem.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(txtItem.Text) & "'"
        End If

        If cboMaintType.Text = "BreakDown" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAINT_TYPE = 'B/D' "
        ElseIf cboMaintType.Text = "Prev. Maint." Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAINT_TYPE = 'P/M' "
        End If

        If cboRprDate.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND E_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboRprDate.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND E_DATE>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboRprDate.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND E_DATE<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboRprDate.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND E_DATE=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If OptOrderBy(0).Checked = True Then 'DATE
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY E_DATE"
        ElseIf OptOrderBy(1).Checked = True Then  'DEPT
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY DEPT_DESC"
        ElseIf OptOrderBy(2).Checked = True Then  'MACHINE
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY MACHINE_DESC"
        ElseIf OptOrderBy(3).Checked = True Then  'ITEM
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY ITEM_SHORT_DESC"
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDept.Text) = "" Then
            MsgBox("Please Select Dept")
            txtDept.Focus()
            Exit Function
        End If
        If chkAllMachine.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMachine.Text) = "" Then
            MsgBox("Please Select Machine")
            txtMachine.Focus()
            Exit Function
        End If
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItem.Text) = "" Then
            MsgBox("Please Select Item.")
            txtItem.Focus()
            Exit Function
        End If
        If cboRprDate.Text = "Between" Then
            If Not IsDate(txtDate1.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate1.Focus()
                Exit Function
            End If
            If Not IsDate(txtDate2.Text) Then
                MsgBox("Date2 is Blank.")
                txtDate2.Focus()
                Exit Function
            End If
        End If
        If cboRprDate.Text = "After" Or cboRprDate.Text = "Before" Or cboRprDate.Text = "On Date" Then
            If Not IsDate(txtDate1.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate1.Focus()
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
End Class
