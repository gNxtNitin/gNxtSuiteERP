Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource
Imports Infragistics.Win.UltraWinExplorerBar
Imports Infragistics.Win.UltraWinGrid
Imports System.Data.OleDb
Friend Class frmCheckDailyAttnEmp
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColSNO As Short = 0
    Private Const ColAttnDate As Short = 1
    Private Const ColBookNo As Short = 2
    Private Const ColCard As Short = 3
    Private Const ColName As Short = 4
    Private Const ColDept As Short = 5
    Private Const ColINTime As Short = 6
    Private Const ColOutTime As Short = 7
    Private Const ColShiftINTime As Short = 8
    Private Const ColShortTime As Short = 9
    Private Const ColFHalf As Short = 10
    Private Const ColSHalf As Short = 11

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FillHeading()


        Dim cntCol As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        'MainClass.ClearGrid(sprdAttn)

        '    Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        '    NewDate = Format(Tempdate, "dd/mm/yyyy")
        '    lblRunDate.Caption = NewDate

        With sprdAttn
            .MaxCols = ColSHalf

            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(-1, ConRowHeight * 1.5)

            .Row = -1

            .Col = ColBookNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColBookNo, 6)
            .ColHidden = IIf((cboShow.SelectedIndex = 7 Or cboShow.SelectedIndex = 8) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColCard, 8)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColName, 30)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDept, 25)

            For cntCol = ColINTime To ColShiftINTime
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .set_ColWidth(cntCol, 8)
                .ColHidden = IIf((cboShow.SelectedIndex = 7 Or cboShow.SelectedIndex = 8) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
            Next

            For cntCol = ColShortTime To ColShortTime
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .set_ColWidth(cntCol, 8)
            Next

            .Col = ColFHalf
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColFHalf, 10)
            .ColHidden = IIf((cboShow.SelectedIndex = 7 Or cboShow.SelectedIndex = 8) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)

            .Col = ColSHalf
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColSHalf, 10)
            .ColHidden = IIf((cboShow.SelectedIndex = 7 Or cboShow.SelectedIndex = 8) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)

            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColAttnDate
            .Text = "Attn Date"

            .Col = ColBookNo
            .Text = "Book No"

            .Col = ColCard
            .Text = "Emp Card No"


            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColDept
            .Text = "Dept."

            .Col = ColINTime
            .Text = "In Time"

            .Col = ColOutTime
            .Text = "Out Time"

            .Col = ColShiftINTime
            .Text = "Shift IN Time"

            .Col = ColShortTime
            .Text = "Short Time"

            .Col = ColFHalf
            .Text = "Leave Mark First Half"

            .Col = ColSHalf
            .Text = "Leave Mark Second Half"

            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, ColSHalf)
            MainClass.SetSpreadColor(sprdAttn, -1)
            sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
        End With
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtEmpCode.Enabled = False
            cmdsearch.Enabled = False
        Else
            txtEmpCode.Enabled = True
            cmdsearch.Enabled = True
        End If
        Call PrintStatus(False)
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
        End If
    End Sub
    Private Sub chkDivision_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDivision.CheckStateChanged
        If chkDivision.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDivision.Enabled = False
        Else
            cboDivision.Enabled = True
        End If
        Call PrintStatus(False)
    End Sub
    'Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
    '    If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
    '        cboDept.Enabled = False
    '    Else
    '        cboDept.Enabled = True
    '    End If
    '    Call PrintStatus(False)
    'End Sub

    Private Sub chkBookNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBookNo.CheckStateChanged
        If chkBookNo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtBookNo.Enabled = False
        Else
            txtBookNo.Enabled = True
        End If
        Call PrintStatus(False)
    End Sub


    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
    End Sub

    Private Sub chkMinorShift_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMinorShift.CheckStateChanged
        If chkMinorShift.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboMinorShift.Enabled = False
        Else
            cboMinorShift.Enabled = True
        End If
    End Sub

    Private Sub chkPageNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPageNo.CheckStateChanged
        If chkPageNo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPageNo.Enabled = False
        Else
            txtPageNo.Enabled = True
        End If
    End Sub

    Private Sub chkShift_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShift.CheckStateChanged
        If chkShift.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboShift.Enabled = False
        Else
            cboShift.Enabled = True
        End If
    End Sub
    Private Sub chkShow_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShow.CheckStateChanged
        If chkShow.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboShow.Enabled = False
        Else
            cboShow.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        PubDBCn.Errors.Clear()


        ''Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1



        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "From : " & VB6.Format(txtFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtTo.Text, "DD/MM/YYYY")
        'mTitle = "Daily Employee Attendance Report"

        If chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked And cboShow.Text <> "" Then
            mTitle = cboShow.Text & " Report"
        Else
            mTitle = "Employee Attendance Report"
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.Text <> "" Then
            mSubTitle = mSubTitle & " - " & cboCategory.Text
        End If


        If chkShift.CheckState = System.Windows.Forms.CheckState.Unchecked And cboShift.Text <> "" Then
            mSubTitle = mSubTitle & " - " & cboShift.Text
        End If

        Call ShowReport(SqlStr, "DailyAttnEmpCheckList_OR.Rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click


        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtEmpCode.Text) = "" Then
                MsgInformation("Please Select Operator Code")
                txtEmpCode.Focus()
                Exit Sub
            End If

            txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
            If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Employee Code ")
                txtEmpCode.Focus()
                Exit Sub
            End If
        End If

        If VB6.Format(txtFrom.Text, "YYYYMM") <> VB6.Format(txtTo.Text, "YYYYMM") Then
            MsgInformation("From & To Date Should be Same Month.")
            Exit Sub
        End If

        If CDate(txtFrom.Text) > CDate(txtTo.Text) Then
            MsgInformation("To Date Cann't be Less Than From Date.")
            Exit Sub
        End If
        MainClass.ClearGrid(sprdAttn)
        FillHeading()
        RefreshScreenNew()
        PrintStatus(True)
        MainClass.ProtectCell(sprdAttn, 0, sprdAttn.MaxRows, 0, ColSHalf)
    End Sub

    Private Function GetEmpShiftTime(ByRef pEmpCode As String, ByRef pAttnDate As String, ByRef mShiftInTime As String, ByRef mShiftOutTime As String) As String

        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mInStr As String

        mShiftInTime = ""
        mShiftOutTime = ""

        SqlStr = " SELECT IN_TIME, OUT_TIME "

        SqlStr = SqlStr & vbCrLf _
            & " FROM PAY_SHIFT_TRN SMST " & vbCrLf _
            & " WHERE SMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SMST.EMP_CODE ='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf _
            & " AND SMST.SHIFT_DATE= ("

        SqlStr = SqlStr & vbCrLf _
            & " SELECT MAX(SHIFT_DATE) " & vbCrLf _
            & " FROM PAY_SHIFT_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE ='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf _
            & " AND SHIFT_DATE<= TO_DATE('" & VB6.Format(pAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            ''mInStr = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "HH:MM")
            mInStr = IIf(IsDBNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value)
            If IsDate(mInStr) = True Then
                mShiftInTime = DateAdd("n", 5, mInStr)
            End If

            mInStr = IIf(IsDBNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value)
            If IsDate(mInStr) = True Then
                mShiftOutTime = DateAdd("n", -5, mInStr)
            End If
        End If

        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function GetLeaveMark(ByRef pEmpCode As String, ByRef pAttnDate As String, ByRef mFHalf As String, ByRef mSHalf As String) As Boolean

        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFMark As Integer
        Dim mSMark As Integer

        mFHalf = ""
        mSHalf = ""
        GetLeaveMark = False
        SqlStr = " SELECT FIRSTHALF, SECONDHALF " & vbCrLf _
            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE ='" & pEmpCode & "'" & vbCrLf _
            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mFMark = IIf(IsDBNull(RsTemp.Fields("FIRSTHALF").Value), -1, RsTemp.Fields("FIRSTHALF").Value)
            mSMark = IIf(IsDBNull(RsTemp.Fields("SECONDHALF").Value), -1, RsTemp.Fields("SECONDHALF").Value)

            If mFMark = ABSENT Then
                mFHalf = "UNAPPROVED"
            ElseIf mFMark = CASUAL Then
                mFHalf = "CASUAL"
            ElseIf mFMark = EARN Then
                mFHalf = "EARN"
            ElseIf mFMark = SICK Then
                mFHalf = "SICK"
            ElseIf mFMark = MATERNITY Then
                mFHalf = "MATERNITY"
            ElseIf mFMark = CPLEARN Then
                mFHalf = "CPLEARN"
            ElseIf mFMark = WOPAY Then
                mFHalf = "APPROVED LEAVE"
            ElseIf mFMark = CPLAVAIL Then
                mFHalf = "CPLAVAIL"
            ElseIf mFMark = SUNDAY Then
                mFHalf = "SUNDAY"
            ElseIf mFMark = HOLIDAY Then
                mFHalf = "HOLIDAY"
            ElseIf mFMark = PRESENT Then
                mFHalf = "PRESENT"
            ElseIf mFMark = WFH Then
                mFHalf = "WFH"
            End If

            If mSMark = ABSENT Then
                mSHalf = "UNAPPROVED"
            ElseIf mSMark = CASUAL Then
                mSHalf = "CASUAL"
            ElseIf mSMark = EARN Then
                mSHalf = "EARN"
            ElseIf mSMark = SICK Then
                mSHalf = "SICK"
            ElseIf mSMark = MATERNITY Then
                mSHalf = "MATERNITY"
            ElseIf mSMark = CPLEARN Then
                mSHalf = "CPLEARN"
            ElseIf mSMark = WOPAY Then
                mSHalf = "APPROVED LEAVE"
            ElseIf mSMark = CPLAVAIL Then
                mSHalf = "CPLAVAIL"
            ElseIf mSMark = SUNDAY Then
                mSHalf = "SUNDAY"
            ElseIf mSMark = HOLIDAY Then
                mSHalf = "HOLIDAY"
            ElseIf mSMark = PRESENT Then
                mSHalf = "PRESENT"
            ElseIf mSMark = WFH Then
                mSHalf = "WFH"
            End If
        End If
        GetLeaveMark = True
        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Sub frmCheckDailyAttnEmp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
        'Me.Text = "Daily Attendance Report"

    End Sub

    Private Sub frmCheckDailyAttnEmp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        MainClass.ClearGrid(sprdAttn)
        FillHeading()

        optCoctC.Checked = True
        FillDeptCombo()

        txtFrom.Text = VB6.Format(RunDate, "dd/mm/yyyy")
        txtTo.Text = VB6.Format(RunDate, "dd/mm/yyyy")

        chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        txtEmpCode.Enabled = False
        cmdsearch.Enabled = False

        'chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkBookNo.CheckState = System.Windows.Forms.CheckState.Checked
        chkPageNo.CheckState = System.Windows.Forms.CheckState.Checked

        cboDept.Enabled = True
        txtBookNo.Enabled = False
        txtPageNo.Enabled = False

        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False

        chkShow.CheckState = System.Windows.Forms.CheckState.Checked
        cboShow.Enabled = False

        chkShift.CheckState = System.Windows.Forms.CheckState.Checked
        cboShift.Enabled = False

        chkMinorShift.CheckState = System.Windows.Forms.CheckState.Checked
        cboMinorShift.Enabled = False

        chkDivision.CheckState = System.Windows.Forms.CheckState.Checked
        cboDivision.Enabled = False

        PrintStatus(False)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub frmCheckDailyAttnEmp_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdAttn.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(sprdAttn, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub txtBookNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBookNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtFrom.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
            '    ElseIf FYChk(txtFrom.Text) = False Then
            '        Cancel = True
        End If
        txtFrom.Text = VB6.Format(txtFrom.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtTo.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
            '    ElseIf FYChk(txtTo.Text) = False Then
            '        Cancel = True
        End If
        txtTo.Text = VB6.Format(txtTo.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPageNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPageNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub



    Private Sub RefreshScreenNew()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim mCode As String
        Dim mEmpName As String
        Dim cntRow As Integer

        Dim mDeptCode As String
        Dim mAttnDate As String
        Dim mDeptName As String
        Dim mContCode As String
        Dim mBookNo As String
        Dim mPageNo As String
        Dim mShiftInTime As String
        Dim mShiftOutTime As String
        Dim mInTime As String
        Dim mOutime As String
        Dim mODFrom As String
        Dim mODTo As String
        Dim mDivisionCode As Double
        Dim mShiftCode As String
        Dim mFHalf As String
        Dim mSHalf As String
        Dim CntDay As Integer
        Dim mSHortTimeTot As Double
        Dim mFromDate As String
        Dim mToDate As String
        Dim mDateDiff As Long

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDivision.Text = "" Then
                If cboDivision.Enabled = True Then cboDivision.Focus()
                MsgInformation("Please Select Division.")
                Exit Sub
            End If
        End If

        mFromDate = VB6.Format(txtFrom.Text, "YYYY-MM-DD")
        mToDate = VB6.Format(txtTo.Text, "YYYY-MM-DD")

        mDateDiff = DateDiff("d", mFromDate, mToDate) + 2

        If (cboShow.SelectedIndex = 7 Or cboShow.SelectedIndex = 8) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = "SELECT DISTINCT TO_CHAR(DAY,'MON-YYYY') , '',"
        Else
            SqlStr = "SELECT  DISTINCT DAY, BOOKNO, "
        End If

        SqlStr = SqlStr & vbCrLf & "EMP_CODE, EMP_NAME, DEPT_DESC, "

        If (cboShow.SelectedIndex = 7 Or cboShow.SelectedIndex = 8) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " '' , '', '',  "
        Else
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(IN_TIME,'HH24:MI') , TO_CHAR(OUT_TIME,'HH24:MI'), TO_CHAR(SHIFT_IN_TIME,'HH24:MI'),  "
        End If

        If (cboShow.SelectedIndex = 7 Or cboShow.SelectedIndex = 8) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " SUM(SHORT_TIME) SHORT_TIME,"
        Else
            SqlStr = SqlStr & vbCrLf & " SHORT_TIME, "
        End If

        If (cboShow.SelectedIndex = 7 Or cboShow.SelectedIndex = 8) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "'', '',''"
        Else
            SqlStr = SqlStr & vbCrLf _
                & " CASE WHEN FIRSTHALF = -1 THEN '' " & vbCrLf _
                & " WHEN FIRSTHALF = 0 THEN 'UNAPPROVED' " & vbCrLf _
                & " WHEN FIRSTHALF = 1 THEN 'CASUAL' " & vbCrLf _
                & " WHEN FIRSTHALF = 2 THEN 'EARN' " & vbCrLf _
                & " WHEN FIRSTHALF = 3 THEN 'SICK' " & vbCrLf _
                & " WHEN FIRSTHALF = 4 THEN 'MATERNITY' " & vbCrLf _
                & " WHEN FIRSTHALF = 5 THEN 'CPLEARN' " & vbCrLf _
                & " WHEN FIRSTHALF = 6 THEN 'APPROVED' " & vbCrLf _
                & " WHEN FIRSTHALF = 7 THEN 'CPLAVAIL' " & vbCrLf _
                & " WHEN FIRSTHALF = 8 THEN 'SUNDAY' " & vbCrLf _
                & " WHEN FIRSTHALF = 9 THEN 'HOLIDAY' " & vbCrLf _
                & " WHEN FIRSTHALF = 10 THEN 'PRESENT' " & vbCrLf _
                & " WHEN FIRSTHALF = 11 THEN 'WFH' " & vbCrLf _
                & " ELSE '' " & vbCrLf _
                & " END FIRSTHALF,"

            SqlStr = SqlStr & vbCrLf _
                    & " CASE WHEN SECONDHALF = -1 THEN '' " & vbCrLf _
                    & " WHEN SECONDHALF = 0 THEN 'UNAPPROVED' " & vbCrLf _
                    & " WHEN SECONDHALF = 1 THEN 'CASUAL' " & vbCrLf _
                    & " WHEN SECONDHALF = 2 THEN 'EARN' " & vbCrLf _
                    & " WHEN SECONDHALF = 3 THEN 'SICK' " & vbCrLf _
                    & " WHEN SECONDHALF = 4 THEN 'MATERNITY' " & vbCrLf _
                    & " WHEN SECONDHALF = 5 THEN 'CPLEARN' " & vbCrLf _
                    & " WHEN SECONDHALF = 6 THEN 'APPROVED' " & vbCrLf _
                    & " WHEN SECONDHALF = 7 THEN 'CPLAVAIL' " & vbCrLf _
                    & " WHEN SECONDHALF = 8 THEN 'SUNDAY' " & vbCrLf _
                    & " WHEN SECONDHALF = 9 THEN 'HOLIDAY' " & vbCrLf _
                    & " WHEN SECONDHALF = 10 THEN 'PRESENT' " & vbCrLf _
                    & " WHEN SECONDHALF = 11 THEN 'WFH' " & vbCrLf _
                    & " ELSE '' " & vbCrLf _
                    & " END SECONDHALF, MOVE_TYPE"
        End If

        SqlStr = SqlStr & vbCrLf _
                & " FROM ( "

        SqlStr = SqlStr & vbCrLf _
                & " SELECT DAY, ATTN.BOOKNO, EMP.EMP_CODE, EMP.EMP_NAME, " & vbCrLf _
                & " DEPT.DEPT_DESC, NVL(ATTN.IN_TIME,'') AS IN_TIME, NVL(ATTN.OUT_TIME,'') AS OUT_TIME," & vbCrLf _
                & " NVL(SMST.IN_TIME,'') AS SHIFT_IN_TIME, NVL(SMST.OUT_TIME,'')  AS SHIFT_OUT_TIME,"

        ''(endingDateTime - startingDateTime) * 1440 
        '(cboShow.SelectedIndex = 7 Or cboShow.SelectedIndex = 8) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked 

        If (cboShow.SelectedIndex = 3 Or cboShow.SelectedIndex = 7) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " CASE WHEN ATTN.IN_TIME > SMST.IN_TIME THEN ROUND((ATTN.IN_TIME - SMST.IN_TIME) * 1440,0) ELSE 0 END AS SHORT_TIME, "
        ElseIf (cboShow.SelectedIndex = 6 Or cboShow.SelectedIndex = 8) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " CASE WHEN ATTN.OUT_TIME < SMST.OUT_TIME THEN ROUND((SMST.OUT_TIME - ATTN.OUT_TIME) * 1440,0) ELSE 0 END AS SHORT_TIME, "
        Else
            SqlStr = SqlStr & vbCrLf & " CASE WHEN TO_CHAR(ATTN.IN_TIME,'HH24:MI')<>'00:00' AND ATTN.IN_TIME > (SMST.IN_TIME+ (1/1440*5)) THEN ROUND((ATTN.IN_TIME - (SMST.IN_TIME+ (1/1440*5))) * 1440,0) ELSE 0 END  + CASE WHEN TO_CHAR(ATTN.OUT_TIME,'HH24:MI')<>'00:00' AND ATTN.OUT_TIME < (SMST.OUT_TIME - (1/1440*5)) THEN ROUND(((SMST.OUT_TIME- (1/1440*5)) - ATTN.OUT_TIME) * 1440,0) ELSE 0 END SHORT_TIME, "
        End If

        ''NVL(SMST.IN_TIME,'')<NVL(ATTN.IN_TIME,'')

        SqlStr = SqlStr & vbCrLf _
                & " NVL(FIRSTHALF, -1) As FIRSTHALF, NVL(SECONDHALF,-1) SECONDHALF, " & vbCrLf _
                & " (Select MAX(NVL(MOVE_TYPE,'')) FROM PAY_MOVEMENT_TRN WHERE COMPANY_CODE=EMP.COMPANY_CODE AND EMP_CODE=EMP.EMP_CODE AND MOVE_TYPE='O' AND REF_DATE=DAY AND HR_APPROVAL='Y') As MOVE_TYPE" & vbCrLf _
                & " FROM (" & vbCrLf _
                & " SELECT TRUNC(TO_DATE('" & VB6.Format(mFromDate, "DD/MM/YYYY") & "','DD/MM/YYYY'), 'MM') + LEVEL - 1 AS DAY" & vbCrLf _
                & " FROM DUAL" & vbCrLf _
                & " CONNECT BY LEVEL <= 32" & vbCrLf _
                & " ) CAL_MST, PAY_EMPLOYEE_MST EMP, PAY_DEPT_MST DEPT, PAY_DALIY_ATTN_TRN ATTN, PAY_SHIFT_TRN SMST, PAY_ATTN_MST PMST" & vbCrLf _
                & " WHERE EXTRACT(Month FROM day) = EXTRACT(Month FROM TO_DATE('" & VB6.Format(mFromDate, "DD/MM/YYYY") & "','DD/MM/YYYY'))"

        ', PAY_MOVEMENT_TRN MTRN SqlStr = " SELECT MIN(TIME_FROM) AS TIME_FROM, MAX(TIME_TO) AS TIME_TO FROM PAY_MOVEMENT_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(xCode) & "' AND MOVE_TYPE='O'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(xAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        SqlStr = SqlStr & vbCrLf _
                & " AND EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And EMP.COMPANY_CODE=DEPT.COMPANY_CODE " & vbCrLf _
                & " And EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE "

        SqlStr = SqlStr & vbCrLf _
                & " AND EMP.COMPANY_CODE =ATTN.COMPANY_CODE(+)" & vbCrLf _
                & " And EMP.EMP_CODE=ATTN.EMP_CODE(+) " & vbCrLf _
                & " And DAY=ATTN.ATTN_DATE (+)"

        SqlStr = SqlStr & vbCrLf _
                & " AND EMP.COMPANY_CODE =SMST.COMPANY_CODE(+)" & vbCrLf _
                & " And EMP.EMP_CODE=SMST.EMP_CODE(+) " & vbCrLf _
                & " And DAY=SMST.SHIFT_DATE(+) "

        SqlStr = SqlStr & vbCrLf _
                & " AND EMP.COMPANY_CODE =PMST.COMPANY_CODE(+)" & vbCrLf _
                & " And EMP.EMP_CODE=PMST.EMP_CODE(+) " & vbCrLf _
                & " And DAY=PMST.ATTN_DATE(+) "

        SqlStr = SqlStr & vbCrLf _
            & " And (EMP_LEAVE_DATE Is NULL Or EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


        If chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboShow.SelectedIndex = 0 Then
                SqlStr = SqlStr & vbCrLf _
                    & " AND (ATTN.IN_TIME IS NOT NULL AND TO_CHAR(ATTN.IN_TIME,'HH24:MI')<>'00:00') AND   (ATTN.OUT_TIME IS NOT NULL AND TO_CHAR(ATTN.OUT_TIME,'HH24:MI')<>'00:00')"
            ElseIf cboShow.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf _
                   & " AND (ATTN.IN_TIME IS NULL OR TO_CHAR(ATTN.IN_TIME,'HH24:MI')='00:00') AND  (ATTN.OUT_TIME IS NULL OR TO_CHAR(ATTN.OUT_TIME,'HH24:MI')='00:00')" & vbCrLf _
                   & " AND (NVL(FIRSTHALF,-1) IN (-1,0,6) OR NVL(SECONDHALF,-1) IN (-1,0,6))"
            ElseIf cboShow.SelectedIndex = 2 Then
                SqlStr = SqlStr & vbCrLf _
                  & " AND (FIRSTHALF IN (1,2,3,4) OR SECONDHALF IN (1,2,3,4))"
            ElseIf cboShow.SelectedIndex = 3 Or cboShow.SelectedIndex = 7 Then
                SqlStr = SqlStr & vbCrLf _
                  & " AND (NVL(FIRSTHALF,-1) IN (-1,10,11) AND ((ATTN.IN_TIME IS NOT NULL OR TO_CHAR(ATTN.IN_TIME,'HH24:MI')='00:00') AND NVL(SMST.IN_TIME + (1/1440*5),'')<NVL(ATTN.IN_TIME,'')))" & vbCrLf _
                 & " AND EMP.EMP_CODE NOT IN (SELECT EMP_CODE FROM PAY_MOVEMENT_TRN WHERE COMPANY_CODE=EMP.COMPANY_CODE AND EMP_CODE=EMP.EMP_CODE AND REF_DATE=DAY AND HR_APPROVAL='Y' AND MOVE_TYPE <> 'M' AND TIME_FROM<=NVL(ATTN.IN_TIME,''))"

                ''& " AND NVL(FIRSTHALF,-1) IN (-1,10,11) AND ((ATTN.IN_TIME IS NOT NULL OR TO_CHAR(ATTN.IN_TIME,'HH24:MI')='00:00') AND NVL(SMST.IN_TIME + (1/1440*5),'')<NVL(ATTN.IN_TIME,''))"
            ElseIf cboShow.SelectedIndex = 4 Then
                SqlStr = SqlStr & vbCrLf _
                  & " AND NVL((Select MAX(NVL(MOVE_TYPE,'')) FROM PAY_MOVEMENT_TRN WHERE COMPANY_CODE=EMP.COMPANY_CODE AND EMP_CODE=EMP.EMP_CODE AND MOVE_TYPE='O' AND REF_DATE=DAY AND HR_APPROVAL='Y'),'')='O'"
            ElseIf cboShow.SelectedIndex = 5 Then
                SqlStr = SqlStr & vbCrLf _
                  & " AND (TO_CHAR(ATTN.IN_TIME,'HH24:MI')<>'00:00')  AND (ATTN.OUT_TIME IS NULL OR TO_CHAR(ATTN.OUT_TIME,'HH24:MI')='00:00')  AND (NVL(FIRSTHALF,-1) IN (-1) OR NVL(SECONDHALF,-1) IN (-1))"
            ElseIf cboShow.SelectedIndex = 6 Or cboShow.SelectedIndex = 8 Then
                SqlStr = SqlStr & vbCrLf _
                 & " AND NVL(SECONDHALF,-1) IN (-1,10,11) AND NVL(TO_CHAR(ATTN.OUT_TIME,'HH24:MI'),'00:00')<>'00:00' AND NVL(SMST.OUT_TIME + (1/1440*-5),'')>NVL(ATTN.OUT_TIME,'')"
            End If
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        If cboDept.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboDept.CheckedRows
                If mDeptName <> "" Then
                    mDeptName += "," & "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                Else
                    mDeptName += "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                End If
            Next
        End If

        If mDeptName <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND DEPT.DEPT_CODE IN (" & mDeptName & ")"
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
                SqlStr = SqlStr & vbCrLf & "AND EMP.DIV_CODE=" & mDivisionCode & ""
            End If
        End If

        If chkShift.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE IN (SELECT EMP_CODE FROM PAY_SHIFT_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SHIFT_CODE='" & cboShift.Text & "')"
        End If

        SqlStr = SqlStr & vbCrLf _
                & " ) WHERE DAY BETWEEN TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD/MM/YYYY') AND TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD/MM/YYYY')"

        If (cboShow.SelectedIndex = 7 Or cboShow.SelectedIndex = 8) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "GROUP BY TO_CHAR(DAY,'MON-YYYY'),EMP_CODE, EMP_NAME, DEPT_DESC"
        End If

        If (cboShow.SelectedIndex = 7 Or cboShow.SelectedIndex = 8) And chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If OptName.Checked = True Then
                SqlStr = SqlStr & vbCrLf & "Order by EMP_NAME"
            ElseIf optCode.Checked = True Then
                SqlStr = SqlStr & vbCrLf & "Order by  EMP_CODE" ''CC.CC_DESC,
            ElseIf optCoctC.Checked = True Then
                SqlStr = SqlStr & vbCrLf & "Order by  DEPT_DESC,EMP_CODE"
            Else
                SqlStr = SqlStr & vbCrLf & "Order by EMP_DEPT_CODE,EMP_CODE"
            End If
        Else
            If OptName.Checked = True Then
                SqlStr = SqlStr & vbCrLf & "Order by EMP_NAME,DAY"
            ElseIf optCode.Checked = True Then
                SqlStr = SqlStr & vbCrLf & "Order by  EMP_CODE,DAY" ''CC.CC_DESC,
            ElseIf optCoctC.Checked = True Then
                SqlStr = SqlStr & vbCrLf & "Order by  DEPT_DESC,EMP_CODE,DAY"
            Else
                SqlStr = SqlStr & vbCrLf & "Order by EMP_DEPT_CODE,EMP_CODE,DAY"
            End If
        End If


        MainClass.AssignDataInSprd8(SqlStr, sprdAttn, StrConn, "Y")
        FillHeading()

        Exit Sub
refreshErrPart:

        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Sub PrintStatus(ByRef PrintFlag As Boolean)
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub
    Private Function GetINOUTTime(ByRef mCode As String, ByRef mAttnDate As String, ByRef mInTime As String, ByRef mOutTime As String, ByRef mBookNo As String, ByRef mShiftInTime As String, ByRef mShiftOutTime As String) As Boolean

        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing

        GetINOUTTime = False
        mInTime = "00:00"
        mOutTime = "00:00"
        mBookNo = "0.0"

        SqlStr = " SELECT ATTN.BOOKNO, ATTN.PAGENO, ATTN.IN_TIME, ATTN.OUT_TIME" & vbCrLf _
            & " FROM PAY_DALIY_ATTN_TRN ATTN" & vbCrLf _
            & " WHERE ATTN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ATTN.ATTN_DATE=TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ATTN.EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'"

        'If chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '    If cboShow.SelectedIndex = 0 Then
        '        SqlStr = SqlStr & vbCrLf _
        '            & " AND (ATTN.IN_TIME IS NOT NULL OR ATTN.IN_TIME<>'') AND   (ATTN.OUT_TIME IS NOT NULL OR ATTN.OUT_TIME<>'')"
        '    ElseIf cboShow.SelectedIndex = 1 Then

        '    End If
        'End If


        'ElseIf cboShow.SelectedIndex = 1 Then
        'If mInTime = "00:00" And mInTime = "00:00" Then
        '    If mFHalf = "" Or mSHalf = "" Or mFHalf = "ABSENT" Or mSHalf = "ABSENT" Or mFHalf = "WOPAY" Or mSHalf = "WOPAY" Then

        '    Else
        '        GoTo MoveNextDate
        '    End If
        'Else
        '    GoTo MoveNextDate
        'End If

        'cboShow.Items.Clear()
        'cboShow.Items.Add("Present")
        'cboShow.Items.Add("Absent")
        'cboShow.Items.Add("Leave")
        'cboShow.Items.Add("Late Comers")
        'cboShow.Items.Add("Out Duty")
        'cboShow.Items.Add("Blank Out Time")
        'cboShow.Items.Add("Early Going")
        'cboShow.SelectedIndex = 0


        '    If chkBookNo.Value = vbUnchecked Then
        '        SqlStr = SqlStr & vbCrLf & "AND ATTN.BOOKNO=" & Val(txtBookNo.Text) & ""
        '    End If
        '
        '    If chkPageNo.Value = vbUnchecked Then
        '        SqlStr = SqlStr & vbCrLf & "AND ATTN.PAGENO=" & Val(txtPageNo.Text) & ""
        '    End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mInTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "DD/MM/YYYY HH:MM")
            mOutTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "DD/MM/YYYY HH:MM")
            mBookNo = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BOOKNO").Value), "", RsTemp.Fields("BOOKNO").Value), "0")
            mBookNo = mBookNo & "." & VB6.Format(IIf(IsDBNull(RsTemp.Fields("PageNo").Value), "", RsTemp.Fields("PageNo").Value), "0")
        End If
        GetINOUTTime = True
        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        GetINOUTTime = True
        '    Resume
    End Function

    Private Function GetODTime(ByRef xCode As String, ByRef xAttnDate As String, ByRef xODFrom As String, ByRef xODTo As String) As Boolean

        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing

        xODFrom = "00:00"
        xODTo = "00:00"
        SqlStr = " SELECT MIN(TIME_FROM) AS TIME_FROM, MAX(TIME_TO) AS TIME_TO FROM PAY_MOVEMENT_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(xCode) & "' AND MOVE_TYPE='O'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(xAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            xODFrom = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_FROM").Value), "", RsTemp.Fields("TIME_FROM").Value), "HH:MM")
            xODTo = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_TO").Value), "", RsTemp.Fields("TIME_TO").Value), "HH:MM")
        End If
        GetODTime = True
        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        GetODTime = False
        '    Resume
    End Function
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        Dim RS As ADODB.Recordset = Nothing

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

        oledbCnn = New OleDbConnection(StrConn)

        SqlStr = "Select DEPT_DESC, DEPT_CODE " & vbCrLf _
            & " FROM PAY_DEPT_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " Order by DEPT_DESC"
        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        cboDept.DataSource = ds
        cboDept.DataMember = ""
        Dim c As UltraGridColumn = Me.cboDept.DisplayLayout.Bands(0).Columns.Add()
        With c
            .Key = "Selected"
            .Header.Caption = String.Empty
            .Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always
            .DataType = GetType(Boolean)
            .DataType = GetType(Boolean)
            .Header.VisiblePosition = 0
        End With
        cboDept.CheckedListSettings.CheckStateMember = "Selected"
        cboDept.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems
        ' Set up the control to use a custom list delimiter 
        cboDept.CheckedListSettings.ListSeparator = " , "
        ' Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
        cboDept.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item
        cboDept.DisplayMember = "DEPT_DESC"
        cboDept.ValueMember = "DEPT_CODE"

        cboDept.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Dept Name"
        cboDept.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Dept Code"
        'cboDepartment.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Address"
        'cboDepartment.DisplayLayout.Bands(0).Columns(3).Header.Caption = "City"
        'cboDepartment.DisplayLayout.Bands(0).Columns(4).Header.Caption = "State"

        cboDept.DisplayLayout.Bands(0).Columns(0).Width = 350
        cboDept.DisplayLayout.Bands(0).Columns(1).Width = 100
        'cboDepartment.DisplayLayout.Bands(0).Columns(2).Width = 350
        'cboDepartment.DisplayLayout.Bands(0).Columns(3).Width = 100
        'cboDepartment.DisplayLayout.Bands(0).Columns(4).Width = 100

        cboDept.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown



        oledbAdapter.Dispose()
        oledbCnn.Close()

        cboMinorShift.Items.Clear()

        SqlStr = "SELECT SHIFT_CODE FROM PAY_SHIFT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY SHIFT_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboMinorShift.Items.Add(RS.Fields("SHIFT_CODE").Value)
                RS.MoveNext()
            Loop
        End If
        cboMinorShift.SelectedIndex = 0

        cboShift.Items.Clear()
        cboShift.Items.Add("G")
        cboShift.Items.Add("A")
        cboShift.Items.Add("B")
        cboShift.Items.Add("C")
        cboShift.SelectedIndex = 0

        SqlStr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CATEGORY_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboCategory.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboCategory.Items.Add(RsDept.Fields("CATEGORY_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboCategory.SelectedIndex = 0

        '    cboCategory.Clear
        '    cboCategory.AddItem "General Staff"
        '    cboCategory.AddItem "Production Staff"
        '    cboCategory.AddItem "Export Staff"
        '    cboCategory.AddItem "Regular Worker"
        '    cboCategory.AddItem "Staff R & D"
        ''    cboCategory.AddItem "Contratcor Staff"
        '    cboCategory.AddItem "Director"
        '    cboCategory.AddItem "Trainee Staff"
        '    cboCategory.ListIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("Present")
        cboShow.Items.Add("Absent")
        cboShow.Items.Add("Leave")
        cboShow.Items.Add("Late Comers")
        cboShow.Items.Add("Out Duty")
        cboShow.Items.Add("Blank Out Time")
        cboShow.Items.Add("Early Going")
        cboShow.Items.Add("Late Comers Summary")
        cboShow.Items.Add("Early Going Summary")
        cboShow.SelectedIndex = 0

        cboEmpCatType.Items.Clear()
        cboEmpCatType.Items.Add("ALL")
        cboEmpCatType.Items.Add("1 : Staff")
        cboEmpCatType.Items.Add("2 : Workers")
        cboEmpCatType.SelectedIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub cboDept_KeyDown(sender As Object, e As KeyEventArgs) Handles cboDept.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            ElseIf e.KeyCode = Keys.Down Then
                cboDept.PerformAction(UltraComboAction.Dropdown)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
