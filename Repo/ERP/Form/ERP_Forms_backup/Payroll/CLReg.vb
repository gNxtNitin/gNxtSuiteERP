Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCLReg
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 1
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

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer
        With sprdLeave
            .MaxCols = ColField11
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)
            .set_RowHeight(0, ConRowHeight * 2.5)

            .Col = ColSNO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColSNO, 6)
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

            For cntCol = ColField1 To ColField11
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
            .MaxCols = ColField11
            .Row = 0

            .Col = ColSNO
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
            .Text = "Serial No."

            .Col = ColField2
            .Text = "Casual Leave at the begining of the year"

            .Col = ColField3
            .Text = "Sick Leave at the begining of the year"

            .Col = ColField4
            .Text = "Festival"

            .Col = ColField5
            .Text = "Casual"

            .Col = ColField6
            .Text = "Sick"

            .Col = ColField7
            .Text = "From"

            .Col = ColField8
            .Text = "To"

            .Col = ColField9
            .Text = "Kind of Leave"

            .Col = ColField10
            .Text = "Whether Granted or refused"

            .Col = ColField11
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
        Call ShowReport(SqlStr, "HolidayReg.Rpt", Mode, mTitle, mSubTitle)

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
        SetDate(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub

    Private Sub frmCLReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub

    Private Sub frmCLReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        lblRunDate.Text = CStr(RunDate)
        OptName.Checked = True
        SetDate(CDate(lblRunDate.Text))
        FillHeading()
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        optAll(0).Checked = True
        txtEmpCode.Enabled = False
        cmdSearch.Enabled = False

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub frmCLReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
                cmdSearch.Enabled = False
            ElseIf optAll(1).Checked = True Then
                txtEmpCode.Enabled = True
                cmdSearch.Enabled = True
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
        Dim cntRow As Integer
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
        Dim mCLEntitile As Double
        Dim mSLEntitile As Double

        MainClass.ClearGrid(sprdLeave)

        If optAll(1).Checked = True Then
            If txtEmpCode.Text = "" Then
                MsgInformation("Please select the Employee Code.")
                txtEmpCode.Focus()
                Exit Sub
            End If
        End If

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
                cntRow = 1
                Do While Not RsEmp.EOF
                    mSNo = CStr(cntRow)
                    xEmpCode = Trim(IIf(IsDbNull(RsEmp.Fields("EMP_CODE").Value), "", RsEmp.Fields("EMP_CODE").Value))
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

                    .Col = ColSNO
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

                    mCLEntitile = GetEL_CLEntitle(xEmpCode, CASUAL, mDOJ)
                    mSLEntitile = GetEL_CLEntitle(xEmpCode, SICK, mDOJ)

                    Call FillLeaveInSprd(mCurrentRow, mRow, mSNo, xEmpCode, mEmpName, mCategory, mFName, mDeptName, mDOJ, mDOL, mCLEntitile, mSLEntitile)

                    mRow = mRow + 1
                    .MaxRows = .MaxRows + 1

                    RsEmp.MoveNext()
                    If Not RsEmp.EOF Then
                        If xEmpCode <> RsEmp.Fields("EMP_CODE").Value Then
                            cntRow = cntRow + 1
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

    Private Function GetEL_CLEntitle(ByRef pEmpCode As String, ByRef pLeaveCode As Integer, ByRef pDOJDate As String) As Double

        On Error GoTo ErrPart
        Dim RsLeave As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mYear As String
        Dim mEntitle As Double
        Dim mBalMonth As Double
        Dim mCategory As String

        mYear = CStr(Year(CDate(lblRunDate.Text)))
        GetEL_CLEntitle = 0

        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCategory = MasterNo
        Else
            mCategory = ""
        End If


        SqlStr = " SELECT COMPANY_CODE, PAYYEAR,LEAVECODE,TOTENTITLE,TOTENTITLE_WRKS   from PAY_LEAVEDTL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & PubPAYYEAR & " AND LEAVECODE=" & pLeaveCode & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLeave.EOF = False Then
            If mCategory = "R" Then
                If RsLeave.Fields("TOTENTITLE_WRKS").Value = 0 Or IsDbNull(RsLeave.Fields("TOTENTITLE_WRKS").Value) Then
                    mEntitle = IIf(IsDbNull(RsLeave.Fields("TOTENTITLE").Value), 0, RsLeave.Fields("TOTENTITLE").Value)
                Else
                    mEntitle = IIf(IsDbNull(RsLeave.Fields("TOTENTITLE_WRKS").Value), 0, RsLeave.Fields("TOTENTITLE_WRKS").Value)
                End If
            Else
                mEntitle = IIf(IsDbNull(RsLeave.Fields("TOTENTITLE").Value), 0, RsLeave.Fields("TOTENTITLE").Value)
            End If

            If IsDate(pDOJDate) Then
                If Year(CDate(pDOJDate)) = CDbl(mYear) Then
                    If VB.Day(CDate(pDOJDate)) <= 15 Then
                        mBalMonth = 12 - Month(CDate(pDOJDate)) + 1
                    Else
                        mBalMonth = 12 - Month(CDate(pDOJDate))
                    End If

                    mEntitle = mEntitle / 12 * mBalMonth

                    mEntitle = System.Math.Round(mEntitle / 0.5, 0) * 0.5
                End If
            End If
        End If

        GetEL_CLEntitle = mEntitle
        Exit Function
ErrPart:
        GetEL_CLEntitle = 0
    End Function
    Private Sub SetDate(ByRef xDate As Date)
        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        '    Daysinmonth = MainClass.LastDay(Format(xDate, "mm"), Format(xDate, "yyyy"))
        '    Tempdate = Daysinmonth & "/" & Month(xDate) & "/" & Year(xDate)
        Tempdate = "01/01/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = CStr(Year(NewDate))

    End Sub


    Private Sub FillLeaveInSprd(ByRef mCurrentRow As Integer, ByRef mRow As Integer, ByRef mSNo As String, ByRef xEmpCode As String, ByRef mEmpName As String, ByRef mCategory As String, ByRef mFName As String, ByRef mDeptName As String, ByRef mDOJ As String, ByRef mDOL As String, ByRef mCLEntitile As Double, ByRef mSLEntitile As Double)

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim RsLeave As ADODB.Recordset
        Dim mStartDate As String
        Dim mEndDate As String
        Dim mLeave As Double

        Dim mDate As String
        Dim mNextDate As String
        Dim mFromDate As String
        Dim mToDate As String
        Dim mCont As Boolean
        Dim mNewRowCnt As Integer
        Dim mFLeaveType As Integer
        Dim mSLeaveType As Integer

        Dim mCL As Double
        Dim mSL As Double
        Dim mTotalCLLeave As Double
        Dim mTotalSLLeave As Double

        mStartDate = VB6.Format("01/01/" & lblYear.Text, "DD/MM/YYYY")
        mEndDate = VB6.Format("31/12/" & lblYear.Text, "DD/MM/YYYY")
        mCont = False

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE" & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & xEmpCode & "'" & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND (FIRSTHALF = " & SICK & " OR SECONDHALF = " & SICK & " OR FIRSTHALF = " & CASUAL & " OR SECONDHALF = " & CASUAL & ")" & vbCrLf & " ORDER BY ATTN_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLeave.EOF = False Then
            Do While Not RsLeave.EOF
                mDate = IIf(IsDbNull(RsLeave.Fields("ATTN_DATE").Value), "", RsLeave.Fields("ATTN_DATE").Value)
                If mCont = False Then
                    mFromDate = IIf(IsDbNull(RsLeave.Fields("ATTN_DATE").Value), "", RsLeave.Fields("ATTN_DATE").Value)
                End If
                mToDate = IIf(IsDbNull(RsLeave.Fields("ATTN_DATE").Value), "", RsLeave.Fields("ATTN_DATE").Value)
                mFLeaveType = RsLeave.Fields("FIRSTHALF").Value
                mSLeaveType = RsLeave.Fields("SECONDHALF").Value

                If RsLeave.Fields("FIRSTHALF").Value = CASUAL And RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                    mCL = mCL + 1
                    mTotalCLLeave = mTotalCLLeave + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = CASUAL Or RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                    mCL = mCL + 0.5
                    mTotalCLLeave = mTotalCLLeave + 0.5
                End If

                If RsLeave.Fields("FIRSTHALF").Value = SICK And RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mSL = mSL + 1
                    mTotalSLLeave = mTotalSLLeave + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = SICK Or RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mSL = mSL + 0.5
                    mTotalSLLeave = mTotalSLLeave + 0.5
                End If

                RsLeave.MoveNext()
                If RsLeave.EOF = False Then
                    If DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate)) & mFLeaveType = IIf(IsDbNull(RsLeave.Fields("ATTN_DATE").Value), "", RsLeave.Fields("ATTN_DATE").Value) & RsLeave.Fields("FIRSTHALF").Value Then
                        mCont = True
                    Else
                        mCont = False
                    End If
                Else
                    mCont = False
                End If
                If mCont = False Then
                    With sprdLeave
                        .Row = mRow


                        .Col = ColSNO
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

                        .Col = ColField2
                        .Text = Str(mCLEntitile)

                        .Col = ColField3
                        .Text = Str(mSLEntitile)

                        .Col = ColField5
                        .Text = VB6.Format(mCL, "0.0")

                        .Col = ColField6
                        .Text = VB6.Format(mSL, "0.0")

                        .Col = ColField7
                        .Text = VB6.Format(mFromDate, "DD/MM/YYYY")

                        .Col = ColField8
                        .Text = VB6.Format(mToDate, "DD/MM/YYYY")

                        .Col = ColField9
                        .Text = IIf(mFLeaveType = mSLeaveType, IIf(mFLeaveType = CASUAL, "CL", "SL"), IIf(mFLeaveType = CASUAL, "CL", IIf(mFLeaveType = SICK, "SL", "")) & IIf(mFLeaveType = -1, "", IIf(mSLeaveType = -1, "", "/")) & IIf(mSLeaveType = CASUAL, "CL", IIf(mSLeaveType = SICK, "SL", "")))

                        mCL = 0
                        mSL = 0

                        mRow = mRow + 1
                        .MaxRows = mRow
                        .Row = mRow
                    End With
                End If
            Loop

            With sprdLeave
                .Row = mRow


                .Col = ColSNO
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

                .Col = ColField2
                .Text = Str(mCLEntitile)

                .Col = ColField3
                .Text = Str(mSLEntitile)

                .Col = ColField5
                .Text = "--------------------"

                .Col = ColField6
                .Text = "--------------------"

                .Col = ColField7
                .Text = ""

                .Col = ColField8
                .Text = ""

                .Col = ColField9
                .Text = ""


                mRow = mRow + 1
                .MaxRows = mRow
                .Row = mRow

                .Row = mRow


                .Col = ColSNO
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

                .Col = ColField2
                .Text = Str(mCLEntitile)

                .Col = ColField3
                .Text = Str(mSLEntitile)

                .Col = ColField5
                .Text = VB6.Format(mTotalCLLeave, "0.0")

                .Col = ColField6
                .Text = VB6.Format(mTotalSLLeave, "0.0")

                .Col = ColField7
                .Text = ""

                .Col = ColField8
                .Text = ""

                .Col = ColField9
                .Text = ""

                ''Balance Total

                mRow = mRow + 1
                .MaxRows = mRow
                .Row = mRow

                .Row = mRow


                .Col = ColSNO
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

                .Col = ColField2
                .Text = Str(mCLEntitile)

                .Col = ColField3
                .Text = Str(mSLEntitile)

                .Col = ColField4
                .Text = "Balance Leave"

                .Col = ColField5
                .Text = VB6.Format(mCLEntitile - mTotalCLLeave, "0.0")

                .Col = ColField6
                .Text = VB6.Format(mSLEntitile - mTotalSLLeave, "0.0")

                .Col = ColField7
                .Text = ""

                .Col = ColField8
                .Text = ""

                .Col = ColField9
                .Text = ""
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
