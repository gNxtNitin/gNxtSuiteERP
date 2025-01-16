Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMTBF
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColBreakDownCode As Short = 1
    Private Const ColBreakDownDesc As Short = 2
    Private Const ColBDStartDate As Short = 3
    Private Const ColBDStartTime As Short = 4
    Private Const ColBDCompleteDate As Short = 5
    Private Const ColBDCompleteTime As Short = 6
    Private Const ColRunningHours As Short = 7

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnMTBF(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnMTBF(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnMTBF(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "MTBF Trend"
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MTBF.rpt"

        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols) = False Then GoTo ReportErr
        SqlStr = FetchRecordForReport()

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
            FieldCnt = 4
            SetData = "FIELD1,FIELD2,FIELD3"
            GetData = "'" & MainClass.AllowSingleQuote(lblCode.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(txtMachine.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblArea.Text) & "'"

            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                GridName.Col = FieldNum
                SetData = SetData & ", " & "FIELD" & FieldCnt
                GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " " & GetData & ") "
            PubDBCn.Execute(SqlStr)
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

    Private Sub cmdSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' "
        If MainClass.SearchGridMaster(Trim(txtMachine.Text), "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", , , SqlStr) = True Then
            txtMachine.Text = AcName
            lblCode.text = AcName1
        End If
        txtMachine.Focus()
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

    Private Sub frmParamMTBF_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "MTBF Trend"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamMTBF_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        txtDateFrom.Text = "01/01/" & VB6.Format(RunDate, "YYYY")
        txtDateTo.Text = "31/12/" & VB6.Format(RunDate, "YYYY")

        txtHours.Text = CStr(22)

        Call PrintStatus(True)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamMTBF_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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

    Private Sub frmParamMTBF_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub txtHours_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtHours.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtHours_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtHours.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        If Trim(txtHours.Text) = "" Then GoTo EventExitSub
        If Val(txtHours.Text) < 1 Or Val(txtHours.Text) > 24 Then
            MsgBox("Running Hours should be between 1 to 24.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtmachine_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachine.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtmachine_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachine.DoubleClick
        Call cmdSearch_Click(cmdSearch, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtmachine_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachine.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtMachine.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.ValidateWithMasterTable(txtMachine.Text, "MACHINE_DESC", "MACHINE_NO", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Machine Does Not Exist In Master.")
            Cancel = True
        Else
            lblCode.text = MasterNo
        End If
        If MainClass.ValidateWithMasterTable(txtMachine.Text, "MACHINE_DESC", "LOCATION", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblArea.text = MasterNo
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
            .MaxCols = ColRunningHours
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColBreakDownCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColBreakDownCode, 8)

            .Col = ColBreakDownDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColBreakDownDesc, 24)

            .Col = ColBDStartDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeDateSeparator = Asc("/")
            .set_ColWidth(ColBDStartDate, 8)

            .Col = ColBDStartTime
            .CellType = SS_CELL_TYPE_TIME
            .TypeTime24Hour = SS_CELL_TIME_24_HOUR_CLOCK
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColBDStartTime, 8)

            .Col = ColBDCompleteDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeDateSeparator = Asc("/")
            .set_ColWidth(ColBDCompleteDate, 10)

            .Col = ColBDCompleteTime
            .CellType = SS_CELL_TYPE_TIME
            .TypeTime24Hour = SS_CELL_TIME_24_HOUR_CLOCK
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColBDCompleteTime, 10)

            .Col = ColRunningHours
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColRunningHours, 18)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If InsertIntoTemp = False Then GoTo LedgError
        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Call CalcHours()

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

        SqlStr = SqlStr & vbCrLf & " AND MAN_BREAKDOWN_HDR.MACHINE_NO='" & MainClass.AllowSingleQuote(lblCode.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY MACHINE_NO, PROBLEM_FACED "

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

        MakeSQL = " SELECT PROBLEM_FACED,PROB_DESC, " & vbCrLf _
                    & " SLIP_DATE, TO_CHAR(SLIP_TIME,'HH24:MI') AS SLIP_TIME, " & vbCrLf _
                    & " COMPLETION_DATE, TO_CHAR(COMP_TIME,'HH24:MI') AS COMP_TIME,'' AS RUNHOURS " & vbCrLf _
                    & " FROM TEMP_BREAKDOWN, MAN_BDPROBLEMS_MST " & vbCrLf _
                    & " WHERE TEMP_BREAKDOWN.COMPANY_CODE=MAN_BDPROBLEMS_MST.COMPANY_CODE (+) " & vbCrLf _
                    & " AND TEMP_BREAKDOWN.PROBLEM_FACED=MAN_BDPROBLEMS_MST.PROB_CODE (+) " & vbCrLf _
                    & " AND TEMP_BREAKDOWN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND TEMP_BREAKDOWN.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
                    & " AND TEMP_BREAKDOWN.MACHINE_NO='" & MainClass.AllowSingleQuote(lblCode.text) & "'" & vbCrLf _
                    & " ORDER BY SLIP_DATE, SLIP_TIME "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub CalcHours()

        On Error GoTo ERR1
        Dim RsHolidays As ADODB.Recordset
        Dim SqlStr As String
        Dim mDate1 As String
        Dim mTime1 As String
        Dim mDate2 As String
        Dim mTime2 As String
        Dim mRunningDays As Double
        Dim mRunningHours As Integer
        Dim mHolidays As Integer
        Dim I As Integer

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColBDStartDate
                mDate2 = Trim(.Text)

                .Col = ColBDStartTime
                mTime2 = Trim(.Text)

                If I = 1 Then
                    mDate1 = Trim(txtDateFrom.Text)
                    mTime1 = "00:00"
                Else
                    .Row = I - 1

                    .Col = ColBDCompleteDate
                    mDate1 = Trim(.Text)

                    .Col = ColBDCompleteTime
                    mTime1 = Trim(.Text)

                    If mDate1 = "" Or mTime1 = "" Then
                        .Col = ColBDStartDate
                        mDate1 = Trim(.Text)

                        .Col = ColBDStartTime
                        mTime1 = Trim(.Text)
                    End If
                End If

                SqlStr = " SELECT COUNT(*) AS CNT FROM PAY_HOLIDAY_MST " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND HOLIDAY_DATE BETWEEN TO_DATE('" & VB6.Format(mDate1, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(mDate2, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHolidays, ADODB.LockTypeEnum.adLockReadOnly)
                If RsHolidays.EOF = False Then
                    If Not IsDbNull(RsHolidays.Fields("CNT").Value) Then
                        mHolidays = RsHolidays.Fields("CNT").Value
                    Else
                        mHolidays = 0
                    End If
                End If

                mRunningDays = (CDate(mDate2 & " " & mTime2).ToOADate - CDate(mDate1 & " " & mTime1).ToOADate) - mHolidays
                mRunningHours = System.Math.Round(mRunningDays * Val(txtHours.Text), 0)

                .Row = I
                .Col = ColRunningHours
                .Text = CStr(mRunningHours)
            Next
        End With

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

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
        If Trim(txtMachine.Text) = "" Then
            MsgInformation("Machine is blank.")
            txtMachine.Focus()
            FieldsVerification = False
            Exit Function
        End If
        If Trim(txtHours.Text) = "" Then
            MsgInformation("Running Hours is blank.")
            txtHours.Focus()
            FieldsVerification = False
            Exit Function
        End If


        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
End Class
