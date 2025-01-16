Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCheckAttnMachineData
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColSNo As Short = 0
    Private Const ColCard As Short = 1
    Private Const ColName As Short = 2
    Private Const ColOnTime As Short = 3
    Private Const ColLate As Short = 4
    Private Const ColDedAgtLate As Short = 5
    Private Const ColOD As Short = 6
    Private Const ColOT As Short = 7
    Private Const ColABSENT As Short = 8
    Private Const ColCASUAL As Short = 9
    Private Const ColEARN As Short = 10
    Private Const ColSICK As Short = 11
    Private Const ColOTHERLEAVE As Short = 12
    Private Const ColWOPAY As Short = 13
    Private Const ColTotLeaves As Short = 14
    Private Const ColAgtLate As Short = 15
    Private Const ColHolidays As Short = 16
    Private Const ColTotWDays As Short = 17
    Private Const ColTotPresent As Short = 18
    Private Const ColLeaveDate As Short = 19
    Private Sub FillHeading(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim cntCol As Integer
        Dim Tempdate As String

        Dim NewDate As Date

        MainClass.ClearGrid(sprdAttn)

        Tempdate = "01/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Daysinmonth = MainClass.LastDay(VB6.Format(lblRunDate.Text, "mm"), VB6.Format(lblRunDate.Text, "yyyy"))

        With sprdAttn
            .MaxCols = ColLeaveDate

            .Row = -1

            For cntCol = ColOnTime To ColTotPresent
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 1
            Next

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(-1, ConRowHeight * 1.5)

            .Col = ColSNo
            .Text = "S. No."
            .set_ColWidth(ColSNo, 5)

            .Col = ColCard
            .Text = "Emp Card No"
            .set_ColWidth(ColCard, 6)


            .Col = ColName
            .Text = "Employees' Name "
            .set_ColWidth(ColName, 24)
            .ColsFrozen = 3

            .Col = ColOnTime
            .Text = "Present"
            .set_ColWidth(ColOnTime, 6)

            .Col = ColLate
            .Text = "Nos. of Late/S.L."
            .set_ColWidth(ColLate, 6)

            .Col = ColDedAgtLate
            .Text = "Ded. Agt. Late/S.L."
            .set_ColWidth(ColDedAgtLate, 6)

            .Col = ColOD
            .Text = "Total Out Duty"
            .set_ColWidth(ColOD, 6)

            .Col = ColOT
            .Text = "Total Over Time"
            .set_ColWidth(ColOT, 6)

            .Col = ColABSENT
            .Text = "No Data"
            .set_ColWidth(ColABSENT, 6)
            .ColHidden = False

            .Col = ColCASUAL
            .Text = "Casual"
            .set_ColWidth(ColCASUAL, 6)

            .Col = ColEARN
            .Text = "Earn"
            .set_ColWidth(ColEARN, 6)

            .Col = ColSICK
            .Text = "Sick"
            .set_ColWidth(ColSICK, 6)

            .Col = ColOTHERLEAVE
            .Text = "Other Leave"
            .set_ColWidth(ColOTHERLEAVE, 6)

            .Col = ColWOPAY
            .Text = "W/o Pay"
            .set_ColWidth(ColWOPAY, 6)

            .Col = ColAgtLate
            .Text = "Leave Agt Late"
            .set_ColWidth(ColAgtLate, 6)

            .Col = ColTotLeaves
            .Text = "Total Leaves"
            .set_ColWidth(ColTotLeaves, 6)

            .Col = ColHolidays
            .Text = "Holidays"
            .set_ColWidth(ColHolidays, 6)

            .Col = ColTotWDays
            .Text = "Working Days"
            .set_ColWidth(ColTotWDays, 6)

            .Col = ColTotPresent
            .Text = "Total Present"
            .set_ColWidth(ColTotPresent, 6)

            .Col = ColLeaveDate
            .Text = "Leave Date"
            .set_ColWidth(ColLeaveDate, 6)

            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, ColLeaveDate)
            MainClass.SetSpreadColor(sprdAttn, -1)
            sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
        End With
    End Sub

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
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


        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows - 2, ColCard, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1



        'Select Record for print...

        Sqlstr = ""

        Sqlstr = FetchRecordForReport(Sqlstr)

        mSubTitle = "For the period : " & lblYear.Text
        mTitle = "Attendance - Check List (M/c Data)"

        If cboCategory.Text <> "" And chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & " - " & cboCategory.Text
        End If

        If cboDept.Text <> "" And chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & " (Dept : " & cboDept.Text & ")"
        End If

        Call ShowReport(Sqlstr, "AttnCheckListMcData.Rpt", Mode, mTitle, mSubTitle)

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

        FillHeading(CDate(lblRunDate.Text))
        RefreshScreen()
        MainClass.ProtectCell(sprdAttn, 0, sprdAttn.MaxRows, 0, ColLeaveDate)
    End Sub


    Private Sub frmCheckAttnMachineData_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
    End Sub

    Private Sub frmCheckAttnMachineData_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        lblRunDate.Text = CStr(RunDate)
        FillHeading(CDate(lblRunDate.Text))
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        optCardNo.Checked = True
        FillDeptCombo()

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False
        cboCategory.Enabled = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub UpDYear_DownClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        FillHeading(CDate(lblRunDate.Text))
        'RefreshScreen
    End Sub

    Private Sub UpDYear_UpClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        FillHeading(CDate(lblRunDate.Text))
        'RefreshScreen
    End Sub

    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim mCode As String
        Dim mDOJ As String
        Dim mDOL As String
        Dim mMonth As Short
        Dim mYear As Short
        Dim LastDayofMon As String
        Dim mTotLeave As Double
        Dim cntRow As Integer
        Dim mJDays As Short
        Dim mThisMonAttn As Short
        Dim mLDays As Short
        Dim mDeptCode As String

        Dim mAbsent As Double
        Dim mCasual As Double
        Dim mEarn As Double
        Dim mSick As Double
        Dim mOtherLeave As Double
        Dim mWopay As Double
        Dim mRH As Double
        Dim mHoliday As Double
        Dim mLeaveDate As String
        Dim mOnTime As Double
        Dim mLateTime As Double
        Dim mOTTime As Double
        Dim mODTime As Double
        Dim mNotEntry As Double
        Dim mAgtLate As Double

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If


        mMonth = CShort(VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        mYear = Year(CDate(lblRunDate.Text))

        LastDayofMon = MainClass.LastDay(mMonth, Year(CDate(lblRunDate.Text))) & "/" & Month(CDate(lblRunDate.Text)) & "/" & Year(CDate(lblRunDate.Text))
        mDOJ = MainClass.LastDay(mMonth, Year(CDate(lblRunDate.Text))) & "/" & mMonth & "/" & Year(CDate(lblRunDate.Text))
        mDOL = "01" & "/" & mMonth & "/" & Year(CDate(lblRunDate.Text))

        Sqlstr = " SELECT EMP_NAME, EMP_CODE, " & vbCrLf & " EMP_DOJ,EMP_LEAVE_DATE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_STOP_SALARY='N'" & vbCrLf & " AND EMP_DOJ<=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " AND (EMP_LEAVE_DATE>TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL)"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                Sqlstr = Sqlstr & vbCrLf & "AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            Sqlstr = Sqlstr & vbCrLf & "AND EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            Sqlstr = Sqlstr & vbCrLf & "AND EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If OptName.Checked = True Then
            Sqlstr = Sqlstr & vbCrLf & "Order by EMP_NAME"
        Else
            Sqlstr = Sqlstr & vbCrLf & "Order by EMP_CODE"
        End If


        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            With sprdAttn
                cntRow = 1
                Do While Not RsAttn.EOF
                    .MaxRows = cntRow

                    mTotLeave = 0
                    mAbsent = 0
                    mCasual = 0
                    mEarn = 0
                    mSick = 0
                    mOtherLeave = 0
                    mWopay = 0
                    mRH = 0
                    mHoliday = 0
                    mOnTime = 0
                    mLateTime = 0
                    mOTTime = 0
                    mODTime = 0
                    mAgtLate = 0
                    mNotEntry = 0

                    .Row = cntRow
                    .Col = ColCard
                    mCode = RsAttn.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColLeaveDate
                    mLeaveDate = IIf(IsDbNull(RsAttn.Fields("EMP_LEAVE_DATE").Value), "", RsAttn.Fields("EMP_LEAVE_DATE").Value)

                    Call CalcMachineData(mCode, mOnTime, mLateTime, mOTTime, mODTime, mNotEntry, (lblRunDate.Text), "Y")

                    .Col = ColOnTime
                    .Text = CStr(Val(CStr(mOnTime)))

                    .Col = ColLate
                    .Text = CStr(Val(CStr(mLateTime)))

                    .Col = ColDedAgtLate
                    If Val(CStr(mLateTime)) - 3 > 0 Then
                        .Text = CStr(Val(CStr(mLateTime - 3)) / 2)
                    Else
                        .Text = "0.0"
                    End If

                    .Col = ColOD
                    .Text = CStr(Val(CStr(mODTime)))

                    .Col = ColOT
                    .Text = CStr(Val(CStr(mOTTime)))

                    Call CalcLeaves(mCode, LastDayofMon, mAbsent, mCasual, mEarn, mSick, mOtherLeave, mWopay, mRH, mHoliday, mLeaveDate, mAgtLate)

                    .Col = ColABSENT
                    '                mNotEntry = GetAbsentData(mCode)
                    .Text = CStr(Val(CStr(mNotEntry)))

                    .Col = ColCASUAL
                    .Text = CStr(Val(CStr(mCasual)))

                    .Col = ColEARN
                    .Text = CStr(Val(CStr(mEarn)))

                    .Col = ColSICK
                    .Text = CStr(Val(CStr(mSick)))

                    .Col = ColOTHERLEAVE
                    .Text = CStr(Val(CStr(mRH - mOtherLeave)))

                    .Col = ColWOPAY
                    .Text = CStr(Val(CStr(mWopay + mAbsent)))

                    .Col = ColAgtLate
                    .Text = CStr(Val(CStr(mAgtLate)))

                    .Col = ColTotLeaves
                    mTotLeave = mCasual + mEarn + mSick ''- mOtherLeave + mRH
                    .Text = CStr(mTotLeave)

                    '                mHoliday = GetMonthHolidays(lblRunDate.Caption, RsAttn!EMP_DOJ)

                    .Col = ColHolidays
                    .Text = CStr(mHoliday)

                    .Col = ColTotPresent
                    '                mJDays = DateDiff("d", RsAttn!EMP_DOJ, Format(LastDayofMon, "dd/mm/yyyy"))
                    '                mThisMonAttn = mJDays
                    '                If Not IsNull(RsAttn!EMP_LEAVE_DATE) Then
                    '                    mLDays = DateDiff("d", RsAttn!EMP_LEAVE_DATE, Format(LastDayofMon, "dd/mm/yyyy"))
                    '                End If
                    '                If Format(RsAttn!EMP_DOJ, "mm yyyy") = Format(RsAttn!EMP_LEAVE_DATE, "mm yyyy") Then
                    '                    mThisMonAttn = mJDays - mLDays + 1
                    '                ElseIf Format(RsAttn!EMP_DOJ, "mm yyyy") = Format(LastDayofMon, "mm yyyy") Then
                    '                    mThisMonAttn = mJDays + 1
                    '                ElseIf Format(RsAttn!EMP_LEAVE_DATE, "mm yyyy") = Format(LastDayofMon, "mm yyyy") Then
                    '                    mThisMonAttn = MainClass.LastDay(mMonth, mYear) - mLDays
                    '                End If
                    '
                    '                If MainClass.LastDay(mMonth, mYear) < mThisMonAttn Then
                    '                    mThisMonAttn = MainClass.LastDay(mMonth, mYear)
                    '                End If
                    .Text = CStr(mOnTime + mODTime + mHoliday + mTotLeave)

                    .Col = ColTotWDays
                    .Text = CStr(mOnTime + mNotEntry + mODTime) ''mThisMonAttn - (mTotLeave + mAbsent + mWopay + mHoliday)

                    .Col = ColLeaveDate
                    If UCase(VB6.Format(mLeaveDate, "YYYYMM")) = UCase(VB6.Format(lblRunDate.Text, "YYYYMM")) Then
                        .Text = mLeaveDate
                    End If

                    cntRow = cntRow + 1
                    RsAttn.MoveNext()
                Loop
            End With
        End If

        With sprdAttn
            ColTotal(sprdAttn, ColABSENT, .MaxCols - 1)
            .Col = ColName
            .Row = .MaxRows
            .Text = "TOTAL :"
            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '' &H8000000B             ''&H80FF80
            .BlockMode = False
        End With

        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub



    Private Function CalcMachineDataOld(ByRef mCode As String, ByRef mTotInTime As Double, ByRef mTotLateTime As Double, ByRef mTotOTTime As Double, ByRef mTotODTime As Double) As Boolean

        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAttnDate As String
        Dim mLastDay As Integer
        Dim mDays As Integer
        Dim mShiftInTime As String
        Dim mShiftOutTime As String
        Dim mInTime As String
        Dim mInTimeStr As String
        Dim mOutTimeStr As String
        Dim mOutTime As String
        Dim mHType As String
        Dim mTotDateTime As Date
        Dim mWorkHours As Date
        Dim mOTHours As Date
        Dim mSundayOTHours As Date
        Dim mTotOD As Date
        Dim mLateReliefHours As Double
        Dim mWHours As Double
        Dim mMarginsMinute As Double
        Dim xDesgCode As String
        Dim mCat As String
        Dim mShortLeaveCount As Double

        mLateReliefHours = IIf(IsDbNull(RsCompany.Fields("SHORT_LEAVE").Value), 0, RsCompany.Fields("SHORT_LEAVE").Value) / 60
        mMarginsMinute = IIf(IsDbNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)
        mShortLeaveCount = 0
        mLastDay = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))

        xDesgCode = GetEmpCurrentDesg(mCode, (lblRunDate.Text))
        If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCat = MasterNo
        End If

        For mDays = 1 To mLastDay
            mAttnDate = VB6.Format(mDays & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")



            SqlStr = " SELECT TOTAL_HRS,TIME_FROM " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'" & vbCrLf & " AND MOVE_TYPE='O'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                mTotOD = CDate(VB6.Format(IIf(IsDBNull(RsTemp.Fields("TOTAL_HRS").Value), "", RsTemp.Fields("TOTAL_HRS").Value), "HH:MM"))
            Else
                mTotOD = CDate("00:00")
            End If

            SqlStr = " SELECT IN_TIME, OUT_TIME " & vbCrLf & " FROM PAY_SHIFT_TRN TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'" & vbCrLf & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                mShiftInTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "HH:MM")
                mShiftInTime = VB6.Format(TimeSerial(Hour(CDate(mShiftInTime)), Minute(CDate(mShiftInTime)) + mMarginsMinute, 0), "HH:MM")
                mShiftOutTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "HH:MM")
            Else
                mShiftInTime = "00:00"
                mShiftOutTime = "00:00"
            End If


            SqlStr = " SELECT IN_TIME, OUT_TIME " & vbCrLf _
                & " FROM PAY_DALIY_ATTN_TRN ATTN " & vbCrLf _
                & " WHERE ATTN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND ATTN.EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'" & vbCrLf _
                & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                mInTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "HH:MM")
                mOutTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "HH:MM")
                mInTimeStr = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "DD/MM/YYYY HH:MM")
                mOutTimeStr = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "DD/MM/YYYY HH:MM")
            Else
                mInTime = "00:00"
                mOutTime = "00:00"
                mInTimeStr = "00:00"
                mOutTimeStr = "00:00"
            End If


            If GetTotatHours(CDate(mInTime), CDate(mOutTime), CDate(mInTimeStr), CDate(mOutTimeStr), mTotDateTime, mWorkHours, mOTHours, mSundayOTHours, CDate(mInTime), CDate(mOutTime), "", "") = False Then GoTo refreshErrPart

            mWHours = ((Hour(mWorkHours) * 60) + Minute(mWorkHours) + mMarginsMinute) / 60

            If mWorkHours <> CDate("00:00") Then
                If GetIsHolidays(mAttnDate, mHType, mCode, "", "Y") = False Then

                    If mInTime <= mShiftInTime Then
                        If mWHours >= 8 Then
                            mTotInTime = mTotInTime + 1
                        Else
                            If mWHours >= 6 And mShortLeaveCount <= 3 Then
                                mTotInTime = mTotInTime + 1
                                mShortLeaveCount = mShortLeaveCount + 1
                            Else
                                If mWHours >= 4 Then
                                    mTotInTime = mTotInTime + 0.5
                                End If
                            End If
                        End If
                    Else
                        If mInTime >= VB6.Format(TimeSerial(Hour(CDate(mShiftInTime)) + 2, 0, 0), "HH:MM") Then
                            mTotInTime = mTotInTime + 0.5
                        Else
                            mTotLateTime = mTotLateTime + 1
                            If mWHours >= 8 Then
                                mTotInTime = mTotInTime + 1
                                mShortLeaveCount = mShortLeaveCount + 1
                            Else
                                If mWHours >= 6 And mShortLeaveCount <= 3 Then
                                    mTotInTime = mTotInTime + 1
                                    mShortLeaveCount = mShortLeaveCount + 1
                                Else
                                    If mWHours >= 4 Then
                                        mTotInTime = mTotInTime + 0.5
                                    End If
                                End If
                            End If
                        End If
                    End If
                    If mCat = "M" Or mCat = "D" Then
                        mTotOTTime = 0
                    Else
                        mTotOTTime = mTotOTTime + Hour(mOTHours) + (Minute(mOTHours) / 60)
                    End If
                Else
                    If mCat = "M" Or mCat = "D" Then
                        mTotOTTime = 0
                    Else
                        mTotOTTime = mTotOTTime + Hour(mWorkHours) + (Minute(mWorkHours) / 60) + Hour(mOTHours) + (Minute(mOTHours) / 60)
                    End If
                End If
            End If
            If mTotOD <> CDate("00:00") Then
                mTotODTime = mTotODTime + 1
            End If
        Next

        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function GetAbsentData(ByRef mCode As String) As Double

        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAttnDate As String
        Dim mLastDay As Integer
        Dim mDays As Integer
        Dim mInTime As String
        Dim mOutTime As String
        Dim mHType As String
        Dim mTotOD As Date
        mLastDay = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))

        For mDays = 1 To mLastDay
            mAttnDate = VB6.Format(mDays & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")

            If GetIsHolidays(mAttnDate, mHType, mCode, "", "Y") = False Then
                SqlStr = " SELECT IN_TIME, OUT_TIME " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN ATTN " & vbCrLf & " WHERE ATTN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ATTN.EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                If RsTemp.EOF = False Then
                    mInTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "HH:MM")
                    mOutTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "HH:MM")
                Else
                    mInTime = "00:00"
                    mOutTime = "00:00"
                End If

                If mInTime = "00:00" Or mOutTime = "00:00" Then
                    SqlStr = " SELECT TOTAL_HRS " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
                    If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                        Sqlstr = Sqlstr & vbCrLf & " AND HR_APPROVAL='Y'"
                    End If

                    MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                    If RsTemp.EOF = False Then
                        mTotOD = CDate(VB6.Format(IIf(IsDbNull(RsTemp.Fields("TOTAL_HRS").Value), "", RsTemp.Fields("TOTAL_HRS").Value), "HH:MM"))
                    Else
                        mTotOD = CDate("00:00")
                    End If

                    If mTotOD = CDate("00:00") Then
                        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "' AND AGT_LATE='N'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
                        If RsTemp.EOF = True Then
                            GetAbsentData = GetAbsentData + 1
                        End If
                    End If
                End If
            End If
        Next

        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        Sqlstr = "Select DEPT_DESC " & vbCrLf & " FROM PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " Order by DEPT_DESC"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboDept.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        Sqlstr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CATEGORY_DESC"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

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
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function ChechJoinLeaveDate(ByRef mDays As String, ByRef mCode As String) As Boolean

        Dim SqlStr As String = ""
        Dim RsTempJL As ADODB.Recordset = Nothing

        Sqlstr = " SELECT " & vbCrLf & " EMP_DOJ,EMP_LEAVE_DATE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempJL, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTempJL.EOF = False Then
            If DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(VB6.Format(RsTempJL.Fields("EMP_DOJ").Value, "dd/mm/yyyy")), CDate(VB6.Format(mDays, "dd/mm/yyyy"))) > 0 Then
                ChechJoinLeaveDate = True
            Else
                MsgInformation("Employee Joining Date is Greater then Current Date.")
                ChechJoinLeaveDate = False
                Exit Function
            End If
            If IsDbNull(RsTempJL.Fields("EMP_LEAVE_DATE").Value) Then
                ChechJoinLeaveDate = True
            ElseIf DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(VB6.Format(mDays, "dd/mm/yyyy")), CDate(VB6.Format(RsTempJL.Fields("EMP_LEAVE_DATE").Value, "dd/mm/yyyy"))) < 0 Then
                MsgInformation("Employee Leaving Date is Less then Current Date.")
                ChechJoinLeaveDate = False
                Exit Function
            Else
                ChechJoinLeaveDate = True
            End If
        End If
    End Function

    Private Function CalcLeaves(ByRef mCode As String, ByRef mMMYYYY As String, ByRef mAbsent As Double, ByRef mCasual As Double, ByRef mEarn As Double, ByRef mSick As Double, ByRef mCPLEarn As Double, ByRef mWopay As Double, ByRef mCPLAvail As Double, ByRef mHoliday As Double, ByRef mLeaveDate As String, ByRef mAgtLate As Double) As Boolean

        On Error GoTo ErrFillLeaves
        Dim RsLeaves As ADODB.Recordset


        Sqlstr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND TO_CHAR(ATTN_DATE,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mMMYYYY, "MMM-YYYY")) & "')"


        If mLeaveDate <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mLeaveDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeaves, ADODB.LockTypeEnum.adLockOptimistic)
        If RsLeaves.EOF = False Then
            Do While Not RsLeaves.EOF
                If RsLeaves.Fields("FIRSTHALF").Value = ABSENT Then
                    mAbsent = mAbsent + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = EARN Then
                    mEarn = mEarn + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = SICK Then
                    mSick = mSick + 0.5
                    '            ElseIf RsLeaves!FIRSTHALF = CPLEARN Then
                    '                mCPLEARN = mCPLEARN + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = WOPAY Then
                    mWopay = mWopay + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = CPLAVAIL Then
                    mCPLAvail = mCPLAvail + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = SUNDAY Or RsLeaves.Fields("FIRSTHALF").Value = HOLIDAY Then
                    mHoliday = mHoliday + 0.5
                End If

                If RsLeaves.Fields("SECONDHALF").Value = ABSENT Then
                    mAbsent = mAbsent + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = EARN Then
                    mEarn = mEarn + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = SICK Then
                    mSick = mSick + 0.5
                    '            ElseIf RsLeaves!SECONDHALF = CPLEARN Then
                    '                mCPLEARN = mCPLEARN + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = WOPAY Then
                    mWopay = mWopay + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = CPLAVAIL Then
                    mCPLAvail = mCPLAvail + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = SUNDAY Or RsLeaves.Fields("SECONDHALF").Value = HOLIDAY Then
                    mHoliday = mHoliday + 0.5
                End If
                mCPLEarn = mCPLEarn + IIf(IsDbNull(RsLeaves.Fields("CPL_EARN").Value), 0, RsLeaves.Fields("CPL_EARN").Value) * 0.5
                RsLeaves.MoveNext()
            Loop
        End If

        Sqlstr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "' AND AGT_LATE='Y'" & vbCrLf & " AND TO_CHAR(ATTN_DATE,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mMMYYYY, "MMM-YYYY")) & "')"


        If mLeaveDate <> "" Then
            Sqlstr = Sqlstr & vbCrLf & " AND ATTN_DATE<='" & VB6.Format(mLeaveDate, "DD-MMM-YYYY") & "'"
        End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeaves, ADODB.LockTypeEnum.adLockOptimistic)
        If RsLeaves.EOF = False Then
            Do While Not RsLeaves.EOF
                If RsLeaves.Fields("FIRSTHALF").Value = ABSENT Or RsLeaves.Fields("FIRSTHALF").Value = CASUAL Or RsLeaves.Fields("FIRSTHALF").Value = EARN Or RsLeaves.Fields("FIRSTHALF").Value = SICK Or RsLeaves.Fields("FIRSTHALF").Value = CPLEARN Or RsLeaves.Fields("FIRSTHALF").Value = WOPAY Or RsLeaves.Fields("FIRSTHALF").Value = CPLAVAIL Then
                    mAgtLate = mAgtLate + 0.5
                End If

                If RsLeaves.Fields("SECONDHALF").Value = ABSENT Or RsLeaves.Fields("SECONDHALF").Value = CASUAL Or RsLeaves.Fields("SECONDHALF").Value = EARN Or RsLeaves.Fields("SECONDHALF").Value = SICK Or RsLeaves.Fields("SECONDHALF").Value = CPLEARN Or RsLeaves.Fields("SECONDHALF").Value = WOPAY Or RsLeaves.Fields("SECONDHALF").Value = CPLAVAIL Then
                    mAgtLate = mAgtLate + 0.5
                End If
                RsLeaves.MoveNext()
            Loop
        End If

        CalcLeaves = True
        Exit Function
ErrFillLeaves:
        CalcLeaves = False
    End Function
End Class
