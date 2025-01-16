Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmELReg
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNo As Short = 1
    Private Const ColCard As Short = 2
    Private Const ColName As Short = 3
    Private Const ColFName As Short = 4
    Private Const ColDept As Short = 5
    Private Const ColDOJ As Short = 6
    Private Const ColDOL As Short = 7
    Private Const ColField1 As Short = 8
    Private Const ColField2 As Short = 9
    Private Const ColField3 As Short = 10
    Private Const ColField4 As Short = 11
    Private Const ColField5 As Short = 12
    Private Const ColField6 As Short = 13
    Private Const ColField7 As Short = 14
    Private Const ColField8 As Short = 15
    Private Const ColField9 As Short = 16
    Private Const ColField10 As Short = 17
    Private Const ColField11 As Short = 18
    Private Const ColField12 As Short = 19
    Private Const ColField13 As Short = 20
    Private Const ColField14 As Short = 21
    Private Const ColField15 As Short = 22
    Private Const ColField16 As Short = 23
    Private Const ColField17 As Short = 24
    Private Const ColField18 As Short = 25

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer
        With sprdLeave
            .MaxCols = ColField18
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)
            .set_RowHeight(0, ConRowHeight * 2.5)

            .Col = ColSNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColSNo, 6)
            .ColHidden = True

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCard, 6)
            .ColHidden = True

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 25)

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColFName, 15)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDept, 6)

            .Col = ColDOJ
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDOJ, 6)

            .Col = ColDOL
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDOL, 6)

            For cntCol = ColField1 To ColField18
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditMultiLine = True
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 10)
            Next
        End With

        MainClass.ProtectCell(sprdLeave, 1, sprdLeave.MaxRows, 1, sprdLeave.MaxCols)
        sprdLeave.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
        sprdLeave.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        MainClass.SetSpreadColor(sprdLeave, mRow)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(sprdLeave)

        With sprdLeave
            .MaxCols = ColField18
            .Row = 0

            .Col = ColSNo
            .Text = "S. No."

            .Col = ColCard
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColFName
            .Text = "Father Name"

            .Col = ColDept
            .Text = "Department"

            .Col = ColDOJ
            .Text = "Date of Joining"

            .Col = ColDOL
            .Text = "Date of Leaving"

            .Col = ColField1
            .Text = "Calander year of Service"

            .Col = ColField2
            .Text = "Wages during the Period"

            .Col = ColField3
            .Text = "Wages earned during the performed"

            .Col = ColField4
            .Text = "No.of days work performed"

            .Col = ColField5
            .Text = "No. of days of lay off"

            .Col = ColField6
            .Text = "No. of days of maternity leave"

            .Col = ColField7
            .Text = "No. of days of leave"

            .Col = ColField8
            .Text = "Total of cols 4 to 7"

            .Col = ColField9
            .Text = "Balance of leave from proceeding year"

            .Col = ColField10
            .Text = "Leave earned during the mentioned in col.1"

            .Col = ColField11
            .Text = "Total of col 9& 10"

            .Col = ColField12
            .Text = "Whether Leave in accordance with scheme under section 79(8) was referred"

            .Col = ColField13
            .Text = "Leave enjoyed from                     to                             No. of days."

            .Col = ColField14
            .Text = "Balance of Leave of wages"

            .Col = ColField15
            .Text = "Normal rate of wages"

            .Col = ColField16
            .Text = "Cash equivalent of advantage occuring through concessional sales of food grain & other articles"

            .Col = ColField17
            .Text = "Rate of wages forthe leave period ( Total of col. 15 & 16)"

            .Col = ColField18
            .Text = "Remarks"

        End With
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        PubDBCn.Errors.Clear()


        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(sprdLeave, 1, sprdLeave.MaxRows, 1, sprdLeave.MaxCols, PubDBCn) = False Then GoTo ERR1



        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForLReport(SqlStr)

        mSubTitle = ""
        mTitle = ""
        Call ShowReport(SqlStr, "ELReg.Rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Function FetchRecordForLReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW, FIELD5"

        FetchRecordForLReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        'SetDate(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub

    Private Sub frmELReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub

    Private Sub frmELReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
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
        lblRunDate.Text = VB6.Format(RunDate, "DD-MMMM-YYYY")
        OptName.Checked = True
        SetDate(CDate(lblRunDate.Text))
        FillHeading()
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        optAll(0).Checked = True
        txtEmpCode.Enabled = False
        cmdsearch.Enabled = False

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub frmELReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdLeave.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub optAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optAll.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optAll.GetIndex(eventSender)
            If optAll(0).Checked = True Then
                txtEmpCode.Enabled = False
                cmdsearch.Enabled = False
            ElseIf optAll(1).Checked = True Then
                txtEmpCode.Enabled = True
                cmdsearch.Enabled = True
            End If
        End If
    End Sub
    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub UpDYear_DownClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Year, -1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        '    RefreshScreen
    End Sub
    Private Sub UpDYear_UpClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        '    RefreshScreen
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mMonth As Short
        Dim mYear As Short
        Dim CntRow As Integer
        'Dim mDOJ As Date
        'Dim mDOL As Date
        Dim mRow As Integer
        Dim mDeptCode As String

        Dim mSNo As String
        Dim xEmpCode As String
        Dim mEmpName As String
        Dim mFName As String
        Dim mDeptName As String
        Dim mDOJ As String
        Dim mDOL As String
        Dim mCurrentRow As Integer
        Dim mCategory As String

        MainClass.ClearGrid(sprdLeave)

        If optAll(1).Checked = True Then
            If txtEmpCode.Text = "" Then
                MsgInformation("Please select the Employee Code.")
                txtEmpCode.Focus()
                Exit Sub
            End If
        End If


        lblRunDate.Text = VB6.Format("31/12/" & lblYear.Text, "DD-MMMM-YYYY")
        mMonth = Month(CDate(lblRunDate.Text))
        mYear = Year(CDate(lblRunDate.Text))

        mDOJ = "31/12/" & mYear 'MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear
        mDOL = "01" & "/" & mMonth & "/" & mYear

        SqlStr = " Select * From PAY_EMPLOYEE_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND EMP_STOP_SALARY='N' AND " & vbCrLf & " EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "


        If optAll(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpCode.Text)) & "' "
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP_DOJ"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmp.EOF = False Then
            With sprdLeave
                .MaxRows = 1
                mRow = 1
                CntRow = 1
                Do While Not RsEmp.EOF
                    mSNo = CStr(CntRow)
                    xEmpCode = Trim(IIf(IsDbNull(RsEmp.Fields("EMP_Code").Value), "", RsEmp.Fields("EMP_Code").Value))
                    mEmpName = Trim(IIf(IsDbNull(RsEmp.Fields("EMP_NAME").Value), "", RsEmp.Fields("EMP_NAME").Value))
                    mFName = Trim(IIf(IsDbNull(RsEmp.Fields("EMP_FNAME").Value), "", RsEmp.Fields("EMP_FNAME").Value))
                    mCategory = Trim(IIf(IsDbNull(RsEmp.Fields("EMP_CATG").Value), "", RsEmp.Fields("EMP_CATG").Value))
                    mDeptCode = Trim(IIf(IsDbNull(RsEmp.Fields("EMP_DEPT_CODE").Value), "", RsEmp.Fields("EMP_DEPT_CODE").Value))

                    If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDeptName = MasterNo
                    Else
                        mDeptName = ""
                    End If

                    mDOJ = VB6.Format(IIf(IsDbNull(RsEmp.Fields("EMP_DOJ").Value), "", RsEmp.Fields("EMP_DOJ").Value), "DD/MM/YYYY")
                    mDOL = VB6.Format(IIf(IsDbNull(RsEmp.Fields("EMP_LEAVE_DATE").Value), "", RsEmp.Fields("EMP_LEAVE_DATE").Value), "DD/MM/YYYY")

                    .Row = mRow
                    mCurrentRow = mRow

                    .Col = ColSNo
                    .Text = mSNo

                    .Col = ColCard
                    .Text = xEmpCode

                    .Col = ColName
                    .Text = mEmpName

                    .Col = ColFName
                    .Text = mFName

                    .Col = ColDept
                    .Text = Trim(mDeptName)

                    .Col = ColDOJ
                    .Text = mDOJ

                    .Col = ColDOL
                    .Text = mDOL

                    .Col = ColField1
                    .Text = Str(CDbl(lblYear.Text))

                    .Col = ColField2 'Month
                    .Text = ""

                    .Col = ColField3
                    .Text = ""

                    .Col = ColField4 ' WDays
                    .Text = ""

                    .Col = ColField5
                    .Text = ""

                    .Col = ColField6
                    .Text = ""

                    .Col = ColField7
                    .Text = ""

                    .Col = ColField8
                    .Text = ""

                    .Col = ColField9
                    .Text = "" 'Balance Leave

                    .Col = ColField10
                    .Text = ""

                    .Col = ColField11
                    .Text = ""

                    .Col = ColField12
                    .Text = ""

                    .Col = ColField13
                    .Text = "" 'No of Leave

                    .Col = ColField14
                    .Text = "" 'Period leave from

                    .Col = ColField15
                    .Text = "" 'Period leave from

                    .Col = ColField16
                    .Text = ""

                    .Col = ColField17
                    .Text = ""

                    .Col = ColField18
                    .Text = ""

                    Call FillDataInSprd(mCurrentRow, mRow, mSNo, xEmpCode, mEmpName, mCategory, mFName, mDeptName, mDOJ, mDOL)

                    mRow = mRow + 1
                    .MaxRows = .MaxRows + 1

                    RsEmp.MoveNext()
                    If Not RsEmp.EOF Then
                        If xEmpCode <> RsEmp.Fields("EMP_Code").Value Then
                            CntRow = CntRow + 1
                        End If
                    End If
                Loop
                MainClass.ProtectCell(sprdLeave, 0, .MaxRows, 0, .MaxCols)
            End With
        End If
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Sub SetDate(ByRef xDate As Date)
        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        '    Daysinmonth = MainClass.LastDay(Format(xDate, "mm"), Format(xDate, "yyyy"))
        '    Tempdate = Daysinmonth & "/" & Month(xDate) & "/" & Year(xDate)
        Tempdate = "01/01/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = NewDate

        'lblYear.Text = CStr(Year(NewDate))

    End Sub

    Private Sub FillDataInSprd(ByRef mCurrentRow As Integer, ByRef mRow As Integer, ByRef mSNo As String, ByRef xEmpCode As String, ByRef mEmpName As String, ByRef mCategory As String, ByRef mFName As String, ByRef mDeptName As String, ByRef mDOJ As String, ByRef mDOL As String)
        On Error GoTo ErrPart
        Dim CntRow As Integer
        Dim mSubTotal As Double
        Dim mGrandTotal As Double
        Dim mELPerDays As Double


        mSubTotal = 0
        mGrandTotal = 0

        mELPerDays = 15
        If mCategory = "G" Or mCategory = "P" Or mCategory = "D" Or mCategory = "T" Then
            mELPerDays = IIf(RsCompany.Fields("STAFF_EL_PER_DAYS").Value = 0, 15, RsCompany.Fields("STAFF_EL_PER_DAYS").Value)
        ElseIf mCategory = "S" Or mCategory = "E" Then
            If Val(xEmpCode) < 1000 Then
                mELPerDays = IIf(RsCompany.Fields("STAFF_EL_PER_DAYS").Value = 0, 15, RsCompany.Fields("STAFF_EL_PER_DAYS").Value)
            Else
                mELPerDays = IIf(RsCompany.Fields("WORKER_EL_PER_DAYS").Value = 0, 15, RsCompany.Fields("WORKER_EL_PER_DAYS").Value)
            End If
        Else
            mELPerDays = IIf(RsCompany.Fields("WORKER_EL_PER_DAYS").Value = 0, 15, RsCompany.Fields("WORKER_EL_PER_DAYS").Value) '20
        End If



        With sprdLeave
            For CntRow = 1 To 12
                If CntRow > 1 Then
                    mRow = mRow + 1
                End If
                .MaxRows = mRow
                .Row = mRow

                .Col = ColSNo
                .Text = mSNo

                .Col = ColCard
                .Text = xEmpCode

                .Col = ColName
                .Text = mEmpName

                .Col = ColFName
                .Text = mFName

                .Col = ColDept
                .Text = Trim(mDeptName)

                .Col = ColDOJ
                .Text = mDOJ

                .Col = ColDOL
                .Text = mDOL

                .Col = ColField1
                .Text = Str(CDbl(lblYear.Text))

                .Col = ColField2
                .Text = MonthName(CntRow)

                .Col = ColField4

                If VB6.Format("01/" & CntRow & "/" & lblYear.Text, "YYYYMM") < VB6.Format(PubCurrDate, "YYYYMM") Then
                    mSubTotal = GetMonthlyWorkingDays(PubDBCn, xEmpCode, VB6.Format("01/" & CntRow & "/" & lblYear.Text, "DD/MM/YYYY"), mDOJ, mDOL) ''CalcAttn(xEmpCode, mDOJ, mDOL, Format("01/" & CntRow & "/" & lblYear, "DD/MM/YYYY")) - GetMonthHolidays(Format("01/" & CntRow & "/" & lblYear, "DD/MM/YYYY"), mDOJ)
                Else
                    mSubTotal = 0
                End If
                mGrandTotal = mGrandTotal + mSubTotal
                .Text = VB6.Format(mSubTotal, "0.00")

                If CntRow = 1 Then
                    .Col = ColField9
                    .Text = CStr(GetOpeningEL(xEmpCode, VB6.Format("01/" & CntRow & "/" & lblYear.Text, "DD/MM/YYYY")))
                End If
            Next

            mRow = mRow + 1
            .MaxRows = mRow
            .Row = mRow

            .Col = ColSNo
            .Text = mSNo

            .Col = ColCard
            .Text = xEmpCode

            .Col = ColName
            .Text = mEmpName

            .Col = ColFName
            .Text = mFName

            .Col = ColDept
            .Text = Trim(mDeptName)

            .Col = ColDOJ
            .Text = mDOJ

            .Col = ColDOL
            .Text = mDOL

            .Col = ColField1
            .Text = Str(CDbl(lblYear.Text))

            .Col = ColField2
            .Text = "-----------------"

            .Col = ColField4
            .Text = VB6.Format(mGrandTotal, "0.00")

            .Col = ColField5
            .Text = VB6.Format(System.Math.Round(mGrandTotal / mELPerDays), "0.00")

        End With

        Call FillLeaveInSprd(mCurrentRow, mRow, mSNo, xEmpCode, mEmpName, mCategory, mFName, mDeptName, mDOJ, mDOL)

        Exit Sub
ErrPart:

    End Sub

    Private Sub FillLeaveInSprd(ByRef mCurrentRow As Integer, ByRef mRow As Integer, ByRef mSNo As String, ByRef xEmpCode As String, ByRef mEmpName As String, ByRef mCategory As String, ByRef mFName As String, ByRef mDeptName As String, ByRef mDOJ As String, ByRef mDOL As String)

        On Error GoTo ErrPart
        Dim CntRow As Integer
        Dim RsLeave As ADODB.Recordset
        Dim mStartDate As String
        Dim mEndDate As String
        Dim mLeave As Double
        Dim mTotalLeave As Double
        Dim mDate As String
        Dim mNextDate As String
        Dim mFromDate As String
        Dim mToDate As String
        Dim mCont As Boolean
        Dim mNewRowCnt As Integer



        mStartDate = VB6.Format("01/01/" & lblYear.Text, "DD/MM/YYYY")
        mEndDate = VB6.Format("31/12/" & lblYear.Text, "DD/MM/YYYY")
        mCont = False

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE" & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & xEmpCode & "'" & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND (FIRSTHALF = " & EARN & " OR SECONDHALF = " & EARN & " )" & vbCrLf & " ORDER BY ATTN_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLeave.EOF = False Then
            Do While Not RsLeave.EOF
                mDate = IIf(IsDbNull(RsLeave.Fields("ATTN_DATE").Value), "", RsLeave.Fields("ATTN_DATE").Value)
                If mCont = False Then
                    mFromDate = IIf(IsDbNull(RsLeave.Fields("ATTN_DATE").Value), "", RsLeave.Fields("ATTN_DATE").Value)
                End If
                mToDate = IIf(IsDbNull(RsLeave.Fields("ATTN_DATE").Value), "", RsLeave.Fields("ATTN_DATE").Value)
                If RsLeave.Fields("FIRSTHALF").Value = EARN And RsLeave.Fields("SECONDHALF").Value = EARN Then
                    mLeave = mLeave + 1
                    mTotalLeave = mTotalLeave + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = EARN Or RsLeave.Fields("SECONDHALF").Value = EARN Then
                    mLeave = mLeave + 0.5
                    mTotalLeave = mTotalLeave + 0.5
                End If

                RsLeave.MoveNext()
                If RsLeave.EOF = False Then
                    If DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate)) = IIf(IsDbNull(RsLeave.Fields("ATTN_DATE").Value), "", RsLeave.Fields("ATTN_DATE").Value) Then
                        mCont = True
                    Else
                        mCont = False
                    End If
                Else
                    mCont = False
                End If
                If mCont = False Then
                    With sprdLeave
                        mNewRowCnt = mNewRowCnt + 1

                        If mNewRowCnt <= 13 Then
                            .Row = mCurrentRow + mNewRowCnt - 1
                        Else
                            mRow = mRow + 1
                            .MaxRows = mRow
                            .Row = mRow
                        End If

                        .Col = ColSNo
                        .Text = mSNo

                        .Col = ColCard
                        .Text = xEmpCode

                        .Col = ColName
                        .Text = mEmpName

                        .Col = ColFName
                        .Text = mFName

                        .Col = ColDept
                        .Text = Trim(mDeptName)

                        .Col = ColDOJ
                        .Text = mDOJ

                        .Col = ColDOL
                        .Text = mDOL

                        .Col = ColField1
                        .Text = Str(CDbl(lblYear.Text))

                        .Col = ColField13
                        .Text = Str(mLeave)

                        .Col = ColField14
                        .Text = VB6.Format(mFromDate, "DD/MM/YYYY")

                        .Col = ColField15
                        .Text = VB6.Format(mToDate, "DD/MM/YYYY")

                        mLeave = 0
                    End With
                End If
            Loop

            With sprdLeave
                mNewRowCnt = mNewRowCnt + 1

                If mNewRowCnt <= 13 Then
                    .Row = mCurrentRow + mNewRowCnt - 1
                Else
                    mRow = mRow + 1
                    .MaxRows = mRow
                    .Row = mRow
                End If

                .Col = ColSNo
                .Text = mSNo

                .Col = ColCard
                .Text = xEmpCode

                .Col = ColName
                .Text = mEmpName

                .Col = ColFName
                .Text = mFName

                .Col = ColDept
                .Text = Trim(mDeptName)

                .Col = ColDOJ
                .Text = mDOJ

                .Col = ColDOL
                .Text = mDOL

                .Col = ColField1
                .Text = Str(CDbl(lblYear.Text))

                .Col = ColField13
                .Text = "------------------"
            End With

            With sprdLeave
                mNewRowCnt = mNewRowCnt + 1

                If mNewRowCnt <= 13 Then
                    .Row = mCurrentRow + mNewRowCnt - 1
                Else
                    mRow = mRow + 1
                    .MaxRows = mRow
                    .Row = mRow
                End If

                .Col = ColSNo
                .Text = mSNo

                .Col = ColCard
                .Text = xEmpCode

                .Col = ColName
                .Text = mEmpName

                .Col = ColFName
                .Text = mFName

                .Col = ColDept
                .Text = Trim(mDeptName)

                .Col = ColDOJ
                .Text = mDOJ

                .Col = ColDOL
                .Text = mDOL

                .Col = ColField1
                .Text = Str(CDbl(lblYear.Text))

                .Col = ColField13
                .Text = Str(mTotalLeave)
            End With
        End If
        Exit Sub
ErrPart:

    End Sub
    Private Function GetOpeningEL(ByRef xEmpCode As String, ByRef pDate As String) As Double

        On Error GoTo ErrPart
        Dim RsOpLeave As ADODB.Recordset

        GetOpeningEL = 0

        SqlStr = " SELECT SUM(OPENING) AS OPENING, SUM(TOTENTITLE) AS TOTENTITLE " & vbCrLf & " FROM PAY_OPLEAVE_TRN  WHERE" & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR = " & Year(CDate(pDate)) & "" & vbCrLf & " AND EMP_CODE ='" & xEmpCode & "' AND LEAVECODE=" & EARN & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOpLeave.EOF = False Then
            GetOpeningEL = IIf(IsDbNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
        End If
        Exit Function
ErrPart:
        GetOpeningEL = 0
    End Function
End Class
