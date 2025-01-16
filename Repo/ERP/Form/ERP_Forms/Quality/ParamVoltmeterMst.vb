Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamVoltmeterMst
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColDocNo As Short = 1
    Private Const ColDescription As Short = 2
    Private Const ColENo As Short = 3
    Private Const ColMakersNo As Short = 4
    Private Const ColMake As Short = 5
    Private Const ColRange As Short = 6
    Private Const ColLC As Short = 7
    Private Const ColLocation As Short = 8
    Private Const ColShuntRatio As Short = 9
    Private Const ColDept As Short = 10
    Private Const ColFrequency As Short = 11
    Private Const ColCalibSource As Short = 12
    Private Const ColLCDate As Short = 13
    Private Const ColCDDate As Short = 14
    Private Const ColCalibration As Short = 15
    Private Const ColHistory As Short = 16

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboCDDate_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCDDate.SelectedIndexChanged
        If cboCDDate.Text = "None" Then
            txtDate1.Visible = False
            lblDate1.Visible = False
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboCDDate.Text = "Between" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = True
            lblDate2.Visible = True
        ElseIf cboCDDate.Text = "After" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboCDDate.Text = "Before" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboCDDate.Text = "On Date" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        End If
    End Sub

    Private Sub cboLCDate_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboLCDate.SelectedIndexChanged
        If cboLCDate.Text = "None" Then
            txtDate3.Visible = False
            lblDate3.Visible = False
            txtDate4.Visible = False
            lblDate4.Visible = False
        ElseIf cboLCDate.Text = "Between" Then
            txtDate3.Visible = True
            lblDate3.Visible = True
            txtDate4.Visible = True
            lblDate4.Visible = True
        ElseIf cboLCDate.Text = "After" Then
            txtDate3.Visible = True
            lblDate3.Visible = True
            txtDate4.Visible = False
            lblDate4.Visible = False
        ElseIf cboLCDate.Text = "Before" Then
            txtDate3.Visible = True
            lblDate3.Visible = True
            txtDate4.Visible = False
            lblDate4.Visible = False
        ElseIf cboLCDate.Text = "On Date" Then
            txtDate3.Visible = True
            lblDate3.Visible = True
            txtDate4.Visible = False
            lblDate4.Visible = False
        End If
    End Sub

    Private Sub chkAllENo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllENo.CheckStateChanged
        Call PrintStatus(False)
        If chkAllENo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtENo.Enabled = False
            cmdSearchENo.Enabled = False
        Else
            txtENo.Enabled = True
            cmdSearchENo.Enabled = True
        End If
    End Sub

    Private Sub chkAllMake_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllMake.CheckStateChanged
        Call PrintStatus(False)
        If chkAllMake.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtMake.Enabled = False
            cmdSearchMake.Enabled = False
        Else
            txtMake.Enabled = True
            cmdSearchMake.Enabled = True
        End If
    End Sub

    Private Sub chkAllDepartment_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDepartment.CheckStateChanged
        Call PrintStatus(False)
        If chkAllDepartment.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDepartment.Enabled = False
            cmdSearchDepartment.Enabled = False
        Else
            txtDepartment.Enabled = True
            cmdSearchDepartment.Enabled = True
        End If
    End Sub

    Private Sub chkAllDescription_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDescription.CheckStateChanged
        Call PrintStatus(False)
        If chkAllDescription.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDescription.Enabled = False
            cmdSearchDescription.Enabled = False
        Else
            txtDescription.Enabled = True
            cmdSearchDescription.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnVoltmetertList(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnVoltmetertList(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnVoltmetertList(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Process Instruments Master List"

        If chkAllDescription.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDescription.Text) <> "" Then
            mSubTitle = mSubTitle & " [Description : " & txtDescription.Text & " ]"
        End If
        If chkAllENo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtENo.Text) <> "" Then
            mSubTitle = mSubTitle & " [Equipment No : " & txtENo.Text & " ]"
        End If
        If chkAllMake.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMake.Text) <> "" Then
            mSubTitle = mSubTitle & " [Make : " & txtMake.Text & " ]"
        End If
        If chkAllDepartment.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDepartment.Text) <> "" Then
            mSubTitle = mSubTitle & " [Department : " & txtDepartment.Text & " ]"
        End If

        If cboCDDate.Text = "Between" Then
            mSubTitle = mSubTitle & " [Calib Due Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"
        End If
        If cboCDDate.Text = "After" Then
            mSubTitle = mSubTitle & " [Calib Due After  " & txtDate1.Text & " ]"
        End If
        If cboCDDate.Text = "Before" Then
            mSubTitle = mSubTitle & " [Calib Due Before  " & txtDate1.Text & " ]"
        End If
        If cboCDDate.Text = "On Date" Then
            mSubTitle = mSubTitle & " [Calib Due On  " & txtDate1.Text & " ]"
        End If

        If cboLCDate.Text = "Between" Then
            mSubTitle = mSubTitle & " [ Last Calib Between  " & txtDate3.Text & " And " & txtDate4.Text & " ]"
        End If
        If cboLCDate.Text = "After" Then
            mSubTitle = mSubTitle & " [Last Calib After  " & txtDate3.Text & " ]"
        End If
        If cboLCDate.Text = "Before" Then
            mSubTitle = mSubTitle & " [Last Calib Before  " & txtDate3.Text & " ]"
        End If
        If cboLCDate.Text = "On Date" Then
            mSubTitle = mSubTitle & " [Last Calib On  " & txtDate3.Text & " ]"
        End If

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\VoltmeterList.rpt"

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

    Private Sub cmdSearchENo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchENo.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtENo.Text, "QAL_VOLTMETER_MST", "E_NO", , , , SqlStr) = True Then
            txtENo.Text = AcName
        End If
        txtENo.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchMake_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMake.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtMake.Text, "QAL_VOLTMETER_MST", "MAKE", , , , SqlStr) = True Then
            txtMake.Text = AcName
        End If
        txtMake.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchDepartment_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDepartment.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DEPT_CODE IN ( " & vbCrLf & " SELECT DISTINCT DEPT_CODE FROM QAL_VOLTMETER_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DEPT_CODE IS NOT NULL )"
        If MainClass.SearchGridMaster(txtDepartment.Text, "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDepartment.Text = AcName
            lblDeptCode.text = AcName1
        End If
        txtDepartment.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchDescription_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDescription.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtDescription.Text, "QAL_VOLTMETER_MST", "Description", , , , SqlStr) = True Then
            txtDescription.Text = AcName
        End If
        txtDescription.Focus()
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

    Private Sub frmParamVoltmeterMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Process Instruments Master List"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamVoltmeterMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        chkAllDescription.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllDescription_CheckStateChanged(chkAllDescription, New System.EventArgs())
        chkAllENo.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllENo_CheckStateChanged(chkAllENo, New System.EventArgs())
        chkAllMake.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllMake_CheckStateChanged(chkAllMake, New System.EventArgs())
        chkAllDepartment.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllDepartment_CheckStateChanged(chkAllDepartment, New System.EventArgs())
        OptOrderBy(0).Checked = True
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillCbo()
        cboCDDate.Items.Clear()
        cboCDDate.Items.Add("None")
        cboCDDate.Items.Add("Between")
        cboCDDate.Items.Add("After")
        cboCDDate.Items.Add("Before")
        cboCDDate.Items.Add("On Date")
        cboCDDate.SelectedIndex = 0

        cboLCDate.Items.Clear()
        cboLCDate.Items.Add("None")
        cboLCDate.Items.Add("Between")
        cboLCDate.Items.Add("After")
        cboLCDate.Items.Add("Before")
        cboLCDate.Items.Add("On Date")
        cboLCDate.SelectedIndex = 0

        cboStatus.Items.Clear()
        cboStatus.Items.Add("Active")
        cboStatus.Items.Add("Inactive")
        cboStatus.Items.Add("Both")
        cboStatus.SelectedIndex = 0

        cboCalibSource.Items.Clear()
        cboCalibSource.Items.Add("INSIDE")
        cboCalibSource.Items.Add("OUTSIDE")
        cboCalibSource.Items.Add("BOTH")
        cboCalibSource.SelectedIndex = 2
    End Sub

    Private Sub frmParamVoltmeterMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        Dim mDocNo As Double
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset

        With SprdMain
            .Row = .ActiveRow
            If .ActiveCol = ColCalibration Then
                .Col = ColDocNo
                mDocNo = Val(.Text)
                frmVoltmeterCal.MdiParent = Me.MdiParent
                frmVoltmeterCal.frmVoltmeterCal_Activated(Nothing, New System.EventArgs())
                frmVoltmeterCal.Show()
                frmVoltmeterCal.txtDocNo.Text = CStr(mDocNo)
                frmVoltmeterCal.txtDocNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
            ElseIf .ActiveCol = ColHistory Then
                .Col = ColDocNo
                mDocNo = Val(.Text)
                frmParamVoltmeterCal.MdiParent = Me.MdiParent
                frmParamVoltmeterCal.frmParamVoltmeterCal_Activated(Nothing, New System.EventArgs())
                frmParamVoltmeterCal.Show()
                frmParamVoltmeterCal.txtDocNo.Text = CStr(mDocNo)
                frmParamVoltmeterCal.txtDocNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                frmParamVoltmeterCal.cmdShow_Click(Nothing, New System.EventArgs())
            End If
        End With
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim mDocNo As String
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColDocNo
        mDocNo = Trim(SprdMain.Text)
        frmVoltmeterMst.MdiParent = Me.MdiParent
        frmVoltmeterMst.frmVoltmeterMst_Activated(Nothing, New System.EventArgs())
        frmVoltmeterMst.Show()
        frmVoltmeterMst.txtDocNo.Text = mDocNo
        frmVoltmeterMst.txtDocNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Sub txtENO_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtENo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtENo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtENo.DoubleClick
        Call cmdSearchENo_Click(cmdSearchENo, New System.EventArgs())
    End Sub

    Private Sub txtENo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtENo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtENo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtENo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtENo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchENo_Click(cmdSearchENo, New System.EventArgs())
    End Sub

    Private Sub txtENo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtENo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtENo.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtENo.Text, "E_NO", "DOCNO", "QAL_VOLTMETER_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid E. No.", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMake_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMake.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtMake_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMake.DoubleClick
        Call cmdSearchMake_Click(cmdSearchMake, New System.EventArgs())
    End Sub

    Private Sub txtMake_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMake.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMake.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMake_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMake.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMake_Click(cmdSearchMake, New System.EventArgs())
    End Sub

    Private Sub txtMake_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMake.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtMake.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtMake.Text, "MAKE", "DOCNO", "QAL_VOLTMETER_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Make", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDepartment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDepartment.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtDepartment_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDepartment.DoubleClick
        Call cmdSearchDepartment_Click(cmdSearchDepartment, New System.EventArgs())
    End Sub

    Private Sub txtDepartment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDepartment.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDepartment.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDepartment_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDepartment.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDepartment_Click(cmdSearchDepartment, New System.EventArgs())
    End Sub

    Private Sub txtDepartment_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDepartment.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtDepartment.Text) = "" Then GoTo EventExitSub
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DEPT_CODE IN ( " & vbCrLf & " SELECT DISTINCT DEPT_CODE FROM QAL_VOLTMETER_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DEPT_CODE IS NOT NULL )"
        If MainClass.ValidateWithMasterTable(txtDepartment.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Department", vbInformation)
            Cancel = True
        Else
            lblDeptCode.text = MasterNo
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDescription_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDescription.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtDescription_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDescription.DoubleClick
        Call cmdSearchDescription_Click(cmdSearchDescription, New System.EventArgs())
    End Sub

    Private Sub txtDescription_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDescription.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDescription.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDescription_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDescription_Click(cmdSearchDescription, New System.EventArgs())
    End Sub

    Private Sub txtDescription_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDescription.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtDescription.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtDescription.Text, "Description", "DOCNO", "QAL_VOLTMETER_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Description", vbInformation)
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
            .MaxCols = ColHistory
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColDocNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColENo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .ColsFrozen = ColENo

            .Col = ColMakersNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColMake
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColRange
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColLC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColLocation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColShuntRatio
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColFrequency
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColCalibSource
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColLCDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColCDDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColCalibration
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "Calibration"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColCalibration, 8)

            .Col = ColHistory
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "History"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColHistory, 8)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColCDDate)
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

        MakeSQL = " SELECT DOCNO, Description, E_NO, MAKERS_NO, " & vbCrLf & " MAKE, RANGE, L_C, LOCATION, SHUNT_RATIO, DEPT_CODE, FREQUENCY, " & vbCrLf & " DECODE(CALI_SOURCE,'I','INSIDE','OUTSIDE'), LAST_CALI_DATE, CALI_DUE_DATE, '','' " & vbCrLf & " FROM QAL_VOLTMETER_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If chkAllDescription.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDescription.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND Description='" & MainClass.AllowSingleQuote(txtDescription.Text) & "'"
        End If
        If chkAllENo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtENo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND E_NO='" & MainClass.AllowSingleQuote(txtENo.Text) & "'"
        End If
        If chkAllMake.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMake.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAKE='" & MainClass.AllowSingleQuote(txtMake.Text) & "'"
        End If
        If chkAllDepartment.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDepartment.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(lblDeptCode.Text) & "'"
        End If

        If cboLCDate.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND LAST_CALI_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate4.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboLCDate.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND LAST_CALI_DATE>TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboLCDate.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND LAST_CALI_DATE<TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboLCDate.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND LAST_CALI_DATE=TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If cboCDDate.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND CALI_DUE_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboCDDate.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND CALI_DUE_DATE>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboCDDate.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND CALI_DUE_DATE<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboCDDate.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND CALI_DUE_DATE=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If cboStatus.SelectedIndex <> 2 Then
            MakeSQL = MakeSQL & vbCrLf & " AND STATUS='" & VB.Left(Trim(cboStatus.Text), 1) & "'"
        End If

        If cboCalibSource.SelectedIndex <> 2 Then
            MakeSQL = MakeSQL & vbCrLf & " AND CALI_SOURCE='" & VB.Left(Trim(cboCalibSource.Text), 1) & "'"
        End If

        If OptOrderBy(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY CALI_DUE_DATE"
        ElseIf OptOrderBy(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY LAST_CALI_DATE"
        ElseIf OptOrderBy(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY DOCNO"
        ElseIf OptOrderBy(3).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY E_NO"
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If chkAllDescription.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDescription.Text) = "" Then
            MsgBox("Please Select Description")
            txtDescription.Focus()
            Exit Function
        End If
        If chkAllENo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtENo.Text) = "" Then
            MsgBox("Please Select E. No.")
            txtENo.Focus()
            Exit Function
        End If
        If chkAllMake.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMake.Text) = "" Then
            MsgBox("Please Select Make")
            txtMake.Focus()
            Exit Function
        End If
        If chkAllDepartment.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDepartment.Text) = "" Then
            MsgBox("Please Select Department.")
            txtDepartment.Focus()
            Exit Function
        End If
        If cboCDDate.Text = "Between" Then
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
        If cboCDDate.Text = "After" Or cboCDDate.Text = "Before" Or cboCDDate.Text = "On Date" Then
            If Not IsDate(txtDate1.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate1.Focus()
                Exit Function
            End If
        End If
        If cboLCDate.Text = "Between" Then
            If Not IsDate(txtDate3.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate3.Focus()
                Exit Function
            End If
            If Not IsDate(txtDate4.Text) Then
                MsgBox("Date2 is Blank.")
                txtDate4.Focus()
                Exit Function
            End If
        End If
        If cboLCDate.Text = "After" Or cboLCDate.Text = "Before" Or cboLCDate.Text = "On Date" Then
            If Not IsDate(txtDate3.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate3.Focus()
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
End Class
