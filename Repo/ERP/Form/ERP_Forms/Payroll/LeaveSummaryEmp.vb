Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLeaveSummaryEmp
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColSNO As Short = 0
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColDepartment As Short = 3
    Private Const ColDesign As Short = 4
    Private Const ColAttnMonth As Short = 5
    Private Const ColWDays As Short = 6
    Private Const ColHolidays As Short = 7
    Private Const ColCASUAL As Short = 8
    Private Const ColEARN As Short = 9
    Private Const ColSICK As Short = 10
    Private Const ColOtherLeave As Short = 11
    Private Const ColTotLeaves As Short = 12
    Private Const ColWOPAY As Short = 13
    Private Const ColAbsent As Short = 14
    Private Const ColTotalAbsent As Short = 15
    Private Const ColAbsentPer As Short = 16
    Private Const ColTotPresent As Short = 17
    Private Const ColTotInventive As Short = 18
    Private Const ColCPLEarn As Short = 19
    Private Const ColCPLAVAIL As Short = 20
    Private Const ColTotDays As Short = 21



    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprd(ByRef mRow As Integer)
        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdLeave
            .MaxCols = ColTotDays
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2)

            .set_RowHeight(-1, ConRowHeight * 1.3)

            .set_ColWidth(ColSNO, 5)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCode, 6)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 12)

            .Col = ColAttnMonth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColAttnMonth, 12)

            For cntCol = ColWDays To ColTotDays
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditMultiLine = True
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 7)
            Next
        End With

        MainClass.ProtectCell(sprdLeave, 1, sprdLeave.MaxRows, 1, sprdLeave.MaxCols)
        sprdLeave.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        MainClass.SetSpreadColor(sprdLeave, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1

        FieldsVarification = True


        If Trim(txtEmpCode.Text) = "" And chkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("Please Enter the Emp Code.")
            txtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtFrom.Text) Then
            MsgInformation("From Date cann't be blank.")
            txtFrom.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtTo.Text) Then
            MsgInformation("To Date cann't be blank.")
            txtTo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        '    Resume					
    End Function

    Private Sub FillHeading()
        Dim RsTemp As ADODB.Recordset
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(sprdLeave)

        With sprdLeave
            .MaxCols = ColTotDays
            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Emp Code"

            .Col = ColName
            .Text = "Emp Name"

            .Col = ColAttnMonth
            .Text = "Attn. Month"

            .Col = ColCASUAL
            .Text = "Casual"

            .Col = ColEARN
            .Text = "Earn"

            .Col = ColDepartment
            .Text = "Department"

            .Col = ColDesign
            .Text = "Design"

            .Col = ColOtherLeave
            .Text = "Other Leave"

            .Col = ColAbsent
            .Text = "Absent"

            .Col = ColTotalAbsent
            .Text = "Total Absent"

            .Col = ColAbsentPer
            .Text = "Absent %"

            .Col = ColTotInventive
            .Text = "Inventive"

            .Col = ColSICK
            .Text = "Sick"

            .Col = ColWOPAY
            .Text = "W/o Pay"

            .Col = ColCPLEarn
            .Text = "CPL Earn"

            .Col = ColCPLAVAIL
            .Text = "CPL Availed"

            .Col = ColTotLeaves
            .Text = "Total Leaves"

            .Col = ColHolidays
            .Text = "Holiday"

            .Col = ColWDays
            .Text = "W. Days"

            .Col = ColTotPresent
            .Text = "Total Present"

            .Col = ColTotDays
            .Text = "Total Days"
        End With
    End Sub

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkALL.CheckStateChanged
        txtEmpCode.Enabled = IIf(chkALL.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearch.Enabled = IIf(chkALL.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Close()
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
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        PubDBCn.Errors.Clear()

        '''''Insert Data from Grid to PrintDummyData Table...					

        If FillPrintDummyData(sprdLeave, 1, sprdLeave.MaxRows, 1, sprdLeave.MaxCols, PubDBCn) = False Then GoTo ERR1

        '''''Select Record for print...					

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "Leave Summary (Employee Wise)"
        mSubTitle = "Emp :  " & txtEmpCode.Text & "  " & txtName.Text & "  [ From " & VB6.Format(txtFrom.Text, "DD/MM/YYYY") & " To " & VB6.Format(txtTo.Text, "DD/MM/YYYY") & " ]"
        Call ShowReport(SqlStr, "LeaveSummaryEmp.Rpt", Mode, mTitle, mSubTitle)

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

        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        If FieldsVarification() = False Then
            Exit Sub
        End If
        MainClass.ClearGrid(sprdLeave)
        RefreshScreen()
        Call FormatSprd(-1)
    End Sub

    Private Sub frmLeaveSummaryEmp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
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

        FillHeading()

        txtFrom.Text = "01/01/" & VB6.Format(RunDate, "YYYY")
        txtTo.Text = "31/12/" & VB6.Format(RunDate, "YYYY")

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub RefreshScreen()
        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset
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
        Dim mDeptDesc As String
        Dim mDesgDesc As String

        Dim mStartMonth As String
        Dim mEndMonth As String
        Dim mCntMonth As String

        Dim mCasual As Double
        Dim mSick As Double
        Dim mEarn As Double
        Dim mCPLEarn As Double
        Dim mWopay As Double
        Dim mCPLAvail As Double
        Dim mHoliday As Double

        Dim mJoiningDate As String
        Dim mLeaveDate As String
        Dim mName As String
        Dim mNewRow As Integer

        Dim mOtherLeave As Double
        Dim mAbsent As Double
        Dim mTotalAbsent As Double
        Dim mAbsentPer As Double
        Dim mTotInventive As Double

        SqlStr = " SELECT EMP.EMP_NAME, EMP.EMP_CODE, DEPT.DEPT_DESC, DESG.DESG_DESC" & vbCrLf _
            & " EMP.EMP_DOJ, EMP.EMP_LEAVE_DATE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP, PAY_DEPT_MST DEPT, PAY_DESG_MSY DESG" & vbCrLf _
            & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf _
            & " AND EMP.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
            & " AND EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " AND EMP.COMPANY_CODE=DESG.COMPANY_CODE" & vbCrLf _
            & " AND EMP.EMP_DESG_CODE=DESG.DESG_CODE"

        If chkALL.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf _
                & " AND EMP.EMP_DOJ <=TO_DATE('" & VB6.Format(txtTo.Text, "dd-mmm-yyyy") & "') " & vbCrLf _
                & " AND ((EMP.EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(txtFrom.Text, "dd-mmm-yyyy") & "') AND EMP.EMP_LEAVE_DATE <=TO_DATE('" & VB6.Format(txtTo.Text, "dd-mmm-yyyy") & "')) OR EMP.EMP_LEAVE_DATE IS NULL) "
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY EMP.EMP_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = True Then
            Exit Sub
        End If

        cntRow = 1

        With sprdLeave
            Do While RsAttn.EOF = False
                mNewRow = cntRow
                mCode = IIf(IsDBNull(RsAttn.Fields("EMP_CODE").Value), "", RsAttn.Fields("EMP_CODE").Value)
                mName = IIf(IsDBNull(RsAttn.Fields("EMP_NAME").Value), "", RsAttn.Fields("EMP_NAME").Value)
                mJoiningDate = IIf(IsDBNull(RsAttn.Fields("EMP_DOJ").Value), "", RsAttn.Fields("EMP_DOJ").Value)
                mDeptDesc = IIf(IsDBNull(RsAttn.Fields("DEPT_DESC").Value), "", RsAttn.Fields("DEPT_DESC").Value)
                mDesgDesc = IIf(IsDBNull(RsAttn.Fields("DESG_DESC").Value), "", RsAttn.Fields("DESG_DESC").Value)

                mLeaveDate = IIf(IsDBNull(RsAttn.Fields("EMP_LEAVE_DATE").Value), "", RsAttn.Fields("EMP_LEAVE_DATE").Value)

                mStartMonth = "01" & VB6.Format(txtFrom.Text, "/MM/YYYY")
                mEndMonth = MainClass.LastDay(Month(CDate(txtTo.Text)), Year(CDate(txtTo.Text))) & VB6.Format(txtTo.Text, "/MM/YYYY")

                mCntMonth = mStartMonth
                Do While CDate(mCntMonth) <= CDate(mEndMonth)

                    mMonth = CShort(VB6.Format(Month(CDate(mCntMonth)), "00"))
                    mYear = Year(CDate(mCntMonth))

                    LastDayofMon = MainClass.LastDay(mMonth, Year(CDate(mCntMonth))) & "/" & Month(CDate(mCntMonth)) & "/" & Year(CDate(mCntMonth))
                    mDOJ = MainClass.LastDay(mMonth, Year(CDate(mCntMonth))) & "/" & mMonth & "/" & Year(CDate(mCntMonth))
                    mDOL = "01" & "/" & mMonth & "/" & Year(CDate(mCntMonth))

                    If VB6.Format(mJoiningDate, "YYYYMM") > VB6.Format(mCntMonth, "YYYYMM") Then
                        GoTo NextRow
                    End If

                    If mLeaveDate <> "" Then
                        If VB6.Format(mLeaveDate, "YYYYMM") < VB6.Format(mCntMonth, "YYYYMM") Then
                            GoTo NextRow
                        End If
                    End If

                    If CheckSalaryMade(mCode, mCntMonth) = False Then
                        GoTo NextRow
                    End If

                    mCasual = 0
                    mEarn = 0
                    mSick = 0
                    mWopay = 0
                    mCPLEarn = 0
                    mCPLAvail = 0
                    mHoliday = 0
                    mTotLeave = 0

                    mOtherLeave = 0
                    mAbsent = 0
                    mTotalAbsent = 0
                    mAbsentPer = 0
                    mTotInventive = 0

                    .MaxRows = cntRow
                    .Row = cntRow

                    .Col = ColCode
                    .Text = mCode

                    .Col = ColName
                    .Text = mName

                    .Col = ColDepartment
                    .Text = mDeptDesc


                    .Col = ColDesign
                    .Text = mDesgDesc



                    .Col = ColAttnMonth
                    .Text = VB6.Format(mCntMonth, "MMMM, YYYY")

                    Call CalcLeaves(mCode, LastDayofMon, mCasual, mSick, mEarn, mCPLEarn, mWopay, mCPLAvail, mHoliday, mAbsent, mLeaveDate)

                    .Col = ColCASUAL
                    .Text = CStr(Val(CStr(mCasual)))

                    .Col = ColEARN
                    .Text = CStr(Val(CStr(mEarn)))

                    .Col = ColSICK
                    .Text = CStr(Val(CStr(mSick)))

                    .Col = ColWOPAY
                    .Text = CStr(Val(CStr(mWopay)))

                    .Col = ColOtherLeave
                    .Text = CStr(Val(CStr(mOtherLeave)))

                    .Col = ColAbsent
                    .Text = CStr(Val(CStr(mAbsent)))  '

                    mTotalAbsent = mWopay + mAbsent
                    .Col = ColTotalAbsent
                    .Text = CStr(Val(CStr(mTotalAbsent)))  '


                    .Col = ColCPLEarn
                    .Text = CStr(Val(CStr(mCPLEarn)))

                    .Col = ColCPLAVAIL
                    .Text = CStr(Val(CStr(mCPLAvail)))

                    .Col = ColTotLeaves
                    mTotLeave = mCasual + mSick + mEarn
                    .Text = CStr(mTotLeave)

                    .Col = ColHolidays
                    .Text = CStr(mHoliday)

                    .Col = ColTotPresent
                    mJDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, RsAttn.Fields("EMP_DOJ").Value, CDate(VB6.Format(LastDayofMon, "DD/MM/YYYY")))
                    mThisMonAttn = mJDays
                    If Not IsDBNull(RsAttn.Fields("EMP_LEAVE_DATE").Value) Then
                        mLDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, RsAttn.Fields("EMP_LEAVE_DATE").Value, CDate(VB6.Format(LastDayofMon, "DD/MM/YYYY")))
                    End If
                    If VB6.Format(RsAttn.Fields("EMP_DOJ").Value, "MMYYYY") = VB6.Format(RsAttn.Fields("EMP_LEAVE_DATE").Value, "MMYYYY") Then
                        mThisMonAttn = mJDays - mLDays + 1
                    ElseIf VB6.Format(RsAttn.Fields("EMP_DOJ").Value, "MMYYYY") = VB6.Format(LastDayofMon, "MMYYYY") Then
                        mThisMonAttn = mJDays + 1
                    ElseIf VB6.Format(RsAttn.Fields("EMP_LEAVE_DATE").Value, "MMYYYY") = VB6.Format(LastDayofMon, "MMYYYY") Then
                        mThisMonAttn = MainClass.LastDay(mMonth, mYear) - mLDays
                    End If

                    If MainClass.LastDay(mMonth, mYear) < mThisMonAttn Then
                        mThisMonAttn = MainClass.LastDay(mMonth, mYear)
                    End If
                    .Text = CStr(mThisMonAttn - mWopay)



                    .Col = ColTotInventive
                    .Text = mTotInventive

                    .Col = ColWDays
                    .Text = CStr(mThisMonAttn - (mTotLeave + mWopay + mHoliday))

                    .Col = ColAbsentPer
                    mAbsentPer = mTotalAbsent * 100 / LastDayofMon
                    .Text = CStr(Val(CStr(mAbsentPer)))  'LastDayofMon

                    .Col = ColTotDays
                    .Text = CStr(mThisMonAttn)

                    cntRow = cntRow + 1
NextRow:
                    mCntMonth = "01/" & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(mCntMonth)), "MM/YYYY")
                Loop

                .MaxRows = cntRow

                ColSubTotal(mCode, mName, mNewRow, cntRow - 1, cntRow, ColCASUAL, ColTotDays)

                RsAttn.MoveNext()
                cntRow = cntRow + 1
            Loop
        End With

        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume					
    End Sub

    Private Sub ColSubTotal(ByRef mCode As String, ByRef mName As String, ByRef Row1 As Integer, ByRef Row2 As Integer, ByRef RowTotal As Integer, ByRef Col As Integer, ByRef col2 As Integer)
        Dim cntCol As Integer
        Dim cntRow As Integer
        Dim TotCol As Double

        With sprdLeave
            For cntCol = Col To col2
                .Col = cntCol
                For cntRow = Row1 To Row2
                    .Row = cntRow
                    TotCol = TotCol + IIf(IsNumeric(.Text), .Text, 0)
                Next
                .Row = RowTotal
                .Text = VB6.Format(TotCol, "0.00")
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
                TotCol = 0
            Next

            .Row = RowTotal
            .Col = ColAttnMonth
            .Text = "TOTAL :"

            .Col = ColCode
            .Text = mCode

            .Col = ColName
            .Text = mName

            MainClass.ProtectCell(sprdLeave, 0, .MaxRows, 0, .MaxCols)

            .Row = RowTotal
            .Row2 = RowTotal
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F)
            .BlockMode = False
        End With
    End Sub
    Private Function CheckSalaryMade(ByRef mEmpCode As String, ByRef mSalDate As String) As Boolean
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String

        SqlStr = " Select * From PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MM-YYYY')='" & VB6.Format(mSalDate, "MM-YYYY") & "' AND ISARREAR IN ('V', 'N','F')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            CheckSalaryMade = True
        Else
            CheckSalaryMade = False
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
        CheckSalaryMade = True
    End Function

    Private Function CalcLeaves(ByRef mCode As String, ByRef mLastDay As String, ByRef mCasual As Double, ByRef mSick As Double,
                                ByRef mEarn As Double, ByRef mCPLEarn As Double, ByRef mWopay As Double,
                                ByRef mCPLAvail As Double, ByRef mHoliday As Double, ByRef mAbsent As Double, ByRef mLeaveDate As String) As Boolean

        On Error GoTo ErrFillLeaves
        Dim RsLeaves As ADODB.Recordset
        Dim SqlStr As String

        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & mCode & "'" & vbCrLf _
            & " AND TO_CHAR(ATTN_DATE,'MON-YYYY')='" & UCase(VB6.Format(mLastDay, "MMM-YYYY")) & "'"

        If mLeaveDate <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ATTN_DATE<='" & VB6.Format(mLeaveDate, "DD-MMM-YYYY") & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeaves, ADODB.LockTypeEnum.adLockOptimistic)
        If RsLeaves.EOF = False Then
            Do While Not RsLeaves.EOF
                If RsLeaves.Fields("FIRSTHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = SICK Then
                    mSick = mSick + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = EARN Then
                    mEarn = mEarn + 0.5
                    '            ElseIf RsLeaves!FIRSTHALF = CPLEARN Then					
                    '                mCPLEARN = mCPLEARN + 0.5					
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = WOPAY Then
                    mWopay = mWopay + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = ABSENT Then
                    mAbsent = mAbsent + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = CPLAVAIL Then
                    mCPLAvail = mCPLAvail + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = SUNDAY Or RsLeaves.Fields("FIRSTHALF").Value = HOLIDAY Then
                    If GetHolidayAgtWorking(RsLeaves.Fields("ATTN_DATE").Value) = "N" Then
                        mHoliday = mHoliday + 0.5
                    End If
                End If

                If RsLeaves.Fields("SECONDHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = SICK Then
                    mSick = mSick + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = EARN Then
                    mEarn = mEarn + 0.5
                    '            ElseIf RsLeaves!SECONDHALF = CPLEARN Then					
                    '                mCPLEARN = mCPLEARN + 0.5					
                ElseIf RsLeaves.Fields("SECONDHALF").Value = WOPAY Then
                    mWopay = mWopay + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = ABSENT Then
                    mAbsent = mAbsent + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = CPLAVAIL Then
                    mCPLAvail = mCPLAvail + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = SUNDAY Or RsLeaves.Fields("SECONDHALF").Value = HOLIDAY Then
                    If GetHolidayAgtWorking(RsLeaves.Fields("ATTN_DATE").Value) = "N" Then
                        mHoliday = mHoliday + 0.5
                    End If
                End If
                mCPLEarn = mCPLEarn + (IIf(IsDBNull(RsLeaves.Fields("CPL_EARN").Value), 0, RsLeaves.Fields("CPL_EARN").Value) * 0.5)
                RsLeaves.MoveNext()
            Loop
        End If

        CalcLeaves = True
        Exit Function
ErrFillLeaves:
        CalcLeaves = False
    End Function

    Private Sub frmLeaveSummaryEmp_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE",  ,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub
End Class
