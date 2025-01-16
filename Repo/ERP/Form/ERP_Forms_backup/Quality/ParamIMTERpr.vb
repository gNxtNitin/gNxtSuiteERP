Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamIMTERpr
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColAutoKey As Short = 1
    Private Const ColDocNo As Short = 2
    Private Const ColEName As Short = 3
    Private Const ColSendDate As Short = 4
    Private Const ColRecdDate As Short = 5
    Private Const ColRepairAgency As Short = 6
    Private Const ColRepairDetail As Short = 7
    Private Const ColRepairAmt As Short = 8

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboRepairDate_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRepairDate.SelectedIndexChanged
        If cboRepairDate.Text = "None" Then
            txtDate1.Visible = False
            lblDate1.Visible = False
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboRepairDate.Text = "Between" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = True
            lblDate2.Visible = True
        ElseIf cboRepairDate.Text = "After" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboRepairDate.Text = "Before" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboRepairDate.Text = "On Date" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        End If
    End Sub

    Private Sub chkAllDocNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDocNo.CheckStateChanged
        Call PrintStatus(False)
        If chkAllDocNo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDocNo.Enabled = False
            cmdSearchDocNo.Enabled = False
        Else
            txtDocNo.Enabled = True
            cmdSearchDocNo.Enabled = True
        End If
    End Sub

    Private Sub chkAllRepairAgency_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllRepairAgency.CheckStateChanged
        Call PrintStatus(False)
        If chkAllRepairAgency.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtRepairAgency.Enabled = False
            cmdSearchRepairAgency.Enabled = False
        Else
            txtRepairAgency.Enabled = True
            cmdSearchRepairAgency.Enabled = True
        End If
    End Sub

    Private Sub chkAllEName_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEName.CheckStateChanged
        Call PrintStatus(False)
        If chkAllEName.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtEName.Enabled = False
            cmdSearchEName.Enabled = False
        Else
            txtEName.Enabled = True
            cmdSearchEName.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnIMTERpr(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnIMTERpr(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnIMTERpr(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "IMTE Repair History"
        If chkAllEName.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtEName.Text) <> "" Then
            mSubTitle = mSubTitle & " [E. Name : " & txtEName.Text & " ]"
        End If
        If chkAllDocNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDocNo.Text) <> "" Then
            mSubTitle = mSubTitle & " [Doc No : " & txtDocNo.Text & " ]"
        End If
        If chkAllRepairAgency.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtRepairAgency.Text) <> "" Then
            mSubTitle = mSubTitle & " [Repair Agency : " & txtRepairAgency.Text & " ]"
        End If
        If cboRepairDate.Text = "Between" Then
            mSubTitle = mSubTitle & " [Repaired Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"
        End If
        If cboRepairDate.Text = "After" Then
            mSubTitle = mSubTitle & " [Repaired After  " & txtDate1.Text & " ]"
        End If
        If cboRepairDate.Text = "Before" Then
            mSubTitle = mSubTitle & " [Repaired Before  " & txtDate1.Text & " ]"
        End If
        If cboRepairDate.Text = "On Date" Then
            mSubTitle = mSubTitle & " [Repaired On  " & txtDate1.Text & " ]"
        End If

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\IMTERprHis.rpt"

        SqlStr = MakeSQL
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdSearchDocNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDocNo.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "SELECT DISTINCT A.DOCNO,A.DESCRIPTION AS E_NAME,A.E_NO,A.L_C " & vbCrLf & " FROM QAL_IMTE_MST A, QAL_IMTE_REPAIR B " & vbCrLf & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf & " AND A.DOCNO = B.DOCNO " & vbCrLf & " AND A.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(B.AUTO_KEY_REPAIR,LENGTH(B.AUTO_KEY_REPAIR)-5,4)=" & RsCompany.Fields("FYEAR").Value
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtDocNo.Text = AcName
        End If
        txtDocNo.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchRepairAgency_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchRepairAgency.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REPAIR,LENGTH(AUTO_KEY_REPAIR)-5,4)=" & RsCompany.Fields("FYEAR").Value
        If MainClass.SearchGridMaster(txtRepairAgency.Text, "QAL_IMTE_REPAIR", "REPAIR_AGENCY", , , , SqlStr) = True Then
            txtRepairAgency.Text = AcName
        End If
        txtRepairAgency.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchEName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEName.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "SELECT DISTINCT A.DESCRIPTION AS E_NAME " & vbCrLf & " FROM QAL_IMTE_MST A, QAL_IMTE_REPAIR B " & vbCrLf & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf & " AND A.DOCNO = B.DOCNO " & vbCrLf & " AND A.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(B.AUTO_KEY_REPAIR,LENGTH(B.AUTO_KEY_REPAIR)-5,4)=" & RsCompany.Fields("FYEAR").Value
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtEName.Text = AcName
        End If
        txtEName.Focus()
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

    Private Sub frmParamIMTERpr_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "IMTE Repair History"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamIMTERpr_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        chkAllEName.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllEName_CheckStateChanged(chkAllEName, New System.EventArgs())
        chkAllDocNo.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllDocNo_CheckStateChanged(chkAllDocNo, New System.EventArgs())
        chkAllRepairAgency.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllRepairAgency_CheckStateChanged(chkAllRepairAgency, New System.EventArgs())
        OptOrderBy(0).Checked = True
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillCbo()
        cboRepairDate.Items.Clear()
        cboRepairDate.Items.Add("None")
        cboRepairDate.Items.Add("Between")
        cboRepairDate.Items.Add("After")
        cboRepairDate.Items.Add("Before")
        cboRepairDate.Items.Add("On Date")
        cboRepairDate.SelectedIndex = 0
    End Sub

    Private Sub frmParamIMTERpr_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim mAutoKey As String
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColAutoKey
        mAutoKey = Trim(SprdMain.Text)
        frmIMTERpr.MdiParent = Me.MdiParent
        frmIMTERpr.frmIMTERpr_Activated(Nothing, New System.EventArgs())
        frmIMTERpr.Show()
        frmIMTERpr.txtNumber.Text = mAutoKey
        frmIMTERpr.txtNumber_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Sub txtDocNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocNo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtDocNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocNo.DoubleClick
        Call cmdSearchDocNo_Click(cmdSearchDocNo, New System.EventArgs())
    End Sub

    Private Sub txtDocNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDocNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDocNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDocNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDocNo_Click(cmdSearchDocNo, New System.EventArgs())
    End Sub

    Private Sub txtDocNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDocNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtDocNo.Text) = "" Then GoTo EventExitSub
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REPAIR,LENGTH(AUTO_KEY_REPAIR)-5,4)=" & RsCompany.Fields("FYEAR").Value
        If MainClass.ValidateWithMasterTable(txtDocNo.Text, "DocNo", "DocNo", "QAL_IMTE_REPAIR", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Doc No.", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRepairAgency_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRepairAgency.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtRepairAgency_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRepairAgency.DoubleClick
        Call cmdSearchRepairAgency_Click(cmdSearchRepairAgency, New System.EventArgs())
    End Sub

    Private Sub txtRepairAgency_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRepairAgency.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRepairAgency.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRepairAgency_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRepairAgency.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchRepairAgency_Click(cmdSearchRepairAgency, New System.EventArgs())
    End Sub

    Private Sub txtRepairAgency_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRepairAgency.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtRepairAgency.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REPAIR,LENGTH(AUTO_KEY_REPAIR)-5,4)=" & RsCompany.Fields("FYEAR").Value
        If MainClass.ValidateWithMasterTable(txtRepairAgency.Text, "REPAIR_AGENCY", "REPAIR_AGENCY", "QAL_IMTE_REPAIR", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Repair Agency", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtEName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEName.DoubleClick
        Call cmdSearchEName_Click(cmdSearchEName, New System.EventArgs())
    End Sub

    Private Sub txtEName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchEName_Click(cmdSearchEName, New System.EventArgs())
    End Sub

    Private Sub txtEName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValProbERR
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtEName.Text) = "" Then GoTo EventExitSub
        SqlStr = "SELECT A.DESCRIPTION " & vbCrLf _
                    & " FROM QAL_IMTE_MST A, QAL_IMTE_REPAIR B " & vbCrLf _
                    & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                    & " AND A.DOCNO = B.DOCNO " & vbCrLf _
                    & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND SUBSTR(B.AUTO_KEY_REPAIR,LENGTH(B.AUTO_KEY_REPAIR)-5,4)=" & RsCompany.fields("FYEAR").value & " " & vbCrLf _
                    & " AND A.DESCRIPTION='" & MainClass.AllowSingleQuote(txtEName.Text) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If mRsTemp.EOF Then
            MsgBox("Not a valid E. Name")
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
            .MaxCols = ColRepairAmt
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColAutoKey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColDocNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColEName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .ColsFrozen = ColEName

            .Col = ColSendDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColRecdDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColRepairAgency
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColRepairDetail
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColRepairAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeEditMultiLine = True

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
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1

        MakeSQL = " SELECT DISTINCT B.AUTO_KEY_REPAIR,B.DOCNO,A.DESCRIPTION, " & vbCrLf & " B.SEND_DATE,B.RECD_DATE,B.REPAIR_AGENCY,B.REPAIR_DETAIL,B.REPAIR_AMT " & vbCrLf & " FROM QAL_IMTE_MST A, QAL_IMTE_REPAIR B " & vbCrLf & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf & " AND A.DOCNO = B.DOCNO " & vbCrLf & " AND A.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "

        '            & " AND SUBSTR(B.AUTO_KEY_REPAIR,LENGTH(B.AUTO_KEY_REPAIR)-5,4)=" & RsCompany.fields("FYEAR").value

        If chkAllEName.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtEName.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND A.DESCRIPTION='" & MainClass.AllowSingleQuote(txtEName.Text) & "'"
        End If
        If chkAllDocNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDocNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND B.DOCNO=" & Val(txtDocNo.Text) & ""
        End If
        If chkAllRepairAgency.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtRepairAgency.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND B.REPAIR_AGENCY='" & MainClass.AllowSingleQuote(txtRepairAgency.Text) & "'"
        End If

        If cboRepairDate.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND B.RECD_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboRepairDate.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND B.RECD_DATE >TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboRepairDate.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND B.RECD_DATE <TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboRepairDate.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND B.RECD_DATE =TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If OptOrderBy(0).Checked = True Then 'SEND DATE
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY B.SEND_DATE"
        ElseIf OptOrderBy(1).Checked = True Then  'RECD DATE
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY B.RECD_DATE"
        ElseIf OptOrderBy(2).Checked = True Then  'DOC NO
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY B.DOCNO"
        ElseIf OptOrderBy(3).Checked = True Then  'E NAME
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY A.DESCRIPTION"
        ElseIf OptOrderBy(4).Checked = True Then  'REPAIR AGENCY
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY B.REPAIR_AGENCY"
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If chkAllEName.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtEName.Text) = "" Then
            MsgBox("Please Select E. Name")
            txtEName.Focus()
            Exit Function
        End If
        If chkAllDocNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDocNo.Text) = "" Then
            MsgBox("Please Select Doc No.")
            txtDocNo.Focus()
            Exit Function
        End If
        If chkAllRepairAgency.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtRepairAgency.Text) = "" Then
            MsgBox("Please Select Repair Agency")
            txtRepairAgency.Focus()
            Exit Function
        End If

        If cboRepairDate.Text = "Between" Then
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
        If cboRepairDate.Text = "After" Or cboRepairDate.Text = "Before" Or cboRepairDate.Text = "On Date" Then
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
