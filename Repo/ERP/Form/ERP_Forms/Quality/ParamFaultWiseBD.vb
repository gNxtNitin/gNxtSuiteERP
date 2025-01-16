Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamFaultWiseBD
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColFaultCode As Short = 1
    Private Const ColFaultDesc As Short = 2
    Private Const ColFaultType As Short = 3
    Private Const ColMachNo As Short = 4
    Private Const ColOccuranceNo As Short = 5
    Private Const ColDownTime As Short = 6

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboProbType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboProbType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtFault.Enabled = False
            cmdsearch.Enabled = False
        Else
            txtFault.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnFaultBD(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnFaultBD(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnFaultBD(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Fault Wise Break Down Details"
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        If Trim(txtFault.Text) <> "" Then
            mSubTitle = mSubTitle & " [ Fault : " & txtFault.Text & " ]"
        End If
        If cboProbType.SelectedIndex <> 0 Then
            mSubTitle = mSubTitle & " [ Fault Type : " & cboProbType.Text & " ]"
        End If
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\FaultWiseBD.rpt"
        If InsertIntoTemp = False Then GoTo ReportErr
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

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "MAN_BDPROBLEMS_MST", "PROB_DESC", "PROB_CODE", , , SqlStr) = True Then
            txtFault.Text = AcName
            lblCode.text = AcName1
        End If
        txtFault.Focus()
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
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamFaultWiseBD_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Fault Wise Break Down Details"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamFaultWiseBD_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Width = VB6.TwipsToPixelsX(11355)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtFault.Enabled = False
        cmdsearch.Enabled = False
        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Call PrintStatus(True)

        cboProbType.Items.Clear()
        cboProbType.Items.Add("All")
        cboProbType.Items.Add("Mechanical")
        cboProbType.Items.Add("Electrical")
        cboProbType.Items.Add("Hydraulic")
        cboProbType.SelectedIndex = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamFaultWiseBD_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamFaultWiseBD_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub txtDateFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtFault_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFault.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtFault_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFault.DoubleClick
        Call cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtFault_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFault.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFault.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFault_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtFault.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtFault_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFault.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtFault.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtFault.Text, "PROB_DESC", "PROB_CODE", "MAN_BDPROBLEMS_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Break Down Problem Does Not Exist In Master.")
            Cancel = True
        Else
            lblCode.text = MasterNo
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
            .MaxCols = ColDownTime
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColFaultCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColFaultCode, 8)

            .Col = ColFaultDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColFaultDesc, 30)

            .Col = ColFaultType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColFaultType, 8)

            .Col = ColMachNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColMachNo, 8)

            .Col = ColOccuranceNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColOccuranceNo, 12)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .Col = ColDownTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColDownTime, 8)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If InsertIntoTemp = False Then GoTo LedgError
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

    Private Function InsertIntoTemp() As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_BREAKDOWN NOLOGGING " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        SqlStr = "INSERT INTO TEMP_BREAKDOWN (" & vbCrLf & " USERID, COMPANY_CODE, MACHINE_NO, SLIP_DATE, " & vbCrLf & " SLIP_TIME , FROM_DEPT_CODE, COMPLETION_DATE, COMP_TIME, " & vbCrLf & " SUSPECTED_REASON, PROBLEM_FACED, DEPU_EMP_CODE,DEPU_REMARKS,DOWNTIME) "
        SqlStr = SqlStr & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " COMPANY_CODE,TRIM(MACHINE_NO), SLIP_DATE, " & vbCrLf & " (CASE WHEN SLIP_DATE IS NULL THEN NULL " & vbCrLf & " Else " & vbCrLf & " TO_DATE(TO_CHAR(SLIP_DATE,'DD/MON/RRRR')||TO_CHAR(BRK_DWN_TIME,'HH24:MI'),'DD/MM/RRRR HH24:MI') " & vbCrLf & " END) SLIP_TIME, " & vbCrLf & " TRIM(FROM_DEPT_CODE), "
        SqlStr = SqlStr & vbCrLf & "  COMPLETION_DATE, " & vbCrLf & " (CASE WHEN COMPLETION_DATE IS NULL THEN NULL " & vbCrLf & " Else " & vbCrLf & " TO_DATE(TO_CHAR(COMPLETION_DATE,'DD/MON/RRRR')||TO_CHAR(COMPLETION_TIME,'HH24:MI'),'DD/MM/RRRR HH24:MI') " & vbCrLf & " END) COMP_TIME, " & vbCrLf & " SUSPECTED_REASON , " & vbCrLf & " TRIM(PROBLEM_FACED),DEPU_EMP_CODE, DEPU_REMARKS, " & vbCrLf & " ROUND(ABS(( " & vbCrLf & " (CASE WHEN COMPLETION_DATE IS NULL THEN NULL " & vbCrLf & " Else " & vbCrLf & " TO_DATE(TO_CHAR(COMPLETION_DATE,'DD/MON/RRRR')||TO_CHAR(COMPLETION_TIME,'HH24:MI'),'DD/MM/RRRR HH24:MI') " & vbCrLf & " END) " & vbCrLf & " - " & vbCrLf & " (CASE WHEN SLIP_DATE IS NULL THEN NULL " & vbCrLf & " Else " & vbCrLf & " TO_DATE(TO_CHAR(SLIP_DATE,'DD/MON/RRRR')||TO_CHAR(BRK_DWN_TIME,'HH24:MI'),'DD/MM/RRRR HH24:MI') " & vbCrLf & " END) " & vbCrLf & " ) *24*60)) AS DOWN_TIME " & vbCrLf & " FROM MAN_BREAKDOWN_HDR " & vbCrLf & " WHERE MAN_BREAKDOWN_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MAN_BREAKDOWN_HDR.SLIP_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND MAN_BREAKDOWN_HDR.SLIP_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtFault.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND MAN_BREAKDOWN_HDR.PROBLEM_FACED='" & MainClass.AllowSingleQuote(lblCode.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY PROBLEM_FACED, MACHINE_NO "

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()

        InsertIntoTemp = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        InsertIntoTemp = False
        PubDBCn.RollbackTrans()
    End Function

    Private Function MakeSQL() As String
        On Error GoTo ERR1

        MakeSQL = " SELECT TRIM(PROBLEM_FACED), PROB_DESC, " & vbCrLf _
                    & " DECODE(BDMST.PROB_TYPE,'M','Mechanical','E','Electrical','H','Hydraulic') AS PROB_TYPE, " & vbCrLf _
                    & " TRIM(IH.MACHINE_NO), TO_CHAR(COUNT(SLIP_DATE)), " & vbCrLf _
                    & " TO_CHAR(SUM(DOWNTIME)) AS DWN_TIME " & vbCrLf _
                    & " FROM TEMP_BREAKDOWN IH, MAN_BDPROBLEMS_MST BDMST " & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"



        MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE=BDMST.COMPANY_CODE (+) " & vbCrLf & " AND IH.PROBLEM_FACED=BDMST.PROB_CODE (+) "

        If cboProbType.SelectedIndex <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND BDMST.PROB_TYPE='" & VB.Left(cboProbType.Text, 1) & "' "
        End If

        MakeSQL = MakeSQL & vbCrLf & " GROUP BY PROBLEM_FACED,PROB_DESC, PROB_TYPE, MACHINE_NO "

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY PROBLEM_FACED, MACHINE_NO"
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If Trim(txtDateFrom.Text) = "" Then
            MsgBox("From Date Is Blank")
            txtDateFrom.Focus()
            FieldsVerification = False
            Exit Function
        End If
        If Trim(txtDateTo.Text) = "" Then
            MsgBox("To Date Is Blank")
            txtDateTo.Focus()
            FieldsVerification = False
            Exit Function
        End If
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtFault.Text) = "" Then
                MsgInformation("Fault is blank.")
                txtFault.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
End Class
