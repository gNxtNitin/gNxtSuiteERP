Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmMarkAbsentFromMc
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim mLoanDate As String
    Dim mLoanAmount As Double

    Dim mPFRate As Double
    Dim mPFEPFRate As Double
    Dim mPFPensionRate As Double
    Dim mPFCeiling As Double
    Dim mESICeiling As Double
    Dim mESIRate As Double
    Dim mEmplerPFCont As String
    Dim ConWorkDay As Double
    Private Const ConWorkHour As Short = 8
    Dim mCurrentFYNo As Integer


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdOK.Click

        On Error GoTo ErrPart
        Dim mDate As String
        If FieldVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PBar.Visible = True
        mDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & Month(CDate(lblNewDate.Text)) & "/" & Year(CDate(lblNewDate.Text))

        mCurrentFYNo = GetCurrentFYNo(PubDBCn, mDate)

        If mCurrentFYNo = -1 Then
            Exit Sub
        End If


        If CalcAbsentMark = False Then GoTo ErrPart
        MsgInformation("Attendance Process Complete.")

        PBar.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        PBar.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation("Attendance Not Process.")
    End Sub
    Private Function CalcAbsentMark() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mSalDate As String
        Dim mDOJ As String
        Dim mDOL As String
        Dim mEmpCode As String
        Dim mEmpDOJ As String
        Dim mEmpDOL As String
        Dim mEmpShiftIN As String
        Dim mEmpShiftOUT As String
        Dim mEmpShiftBreak As String
        Dim mLastDayofMonth As Integer
        Dim mCntDays As Integer
        Dim mMonthDate As String
        Dim mCountSLMin As Double
        Dim mCountTotSLMin As Double
        Dim mCountSL As Double
        Dim mCountTotSL As Double

        Dim mDataNotFound As Boolean
        Dim mMarginsMinute As Double
        Dim mEmpInTime As String
        Dim mEmpOutTime As String
        Dim mFirstHalf As Boolean
        Dim mSecondHalf As Boolean
        Dim mLeaveType As String

        Dim xFirstHalf As String
        Dim xSecondHalf As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        'PBar.Min = 0

        PBar.Visible = True

        'PBar.Min = 0
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtCardNo.Text & "'"
        End If

        'PBar.Max = MainClass.GetMaxRecord("PAY_EMPLOYEE_MST", PubDBCn, SqlStr)
        'PBar.Value = PBar.Min

        mMarginsMinute = IIf(IsDbNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)
        mSalDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOJ = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOL = "01" & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mLastDayofMonth = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text)))

        SqlStr = " SELECT * FROM " & vbCrLf & " PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        SqlStr = SqlStr & vbCrLf & " AND EMP_CATG<>'C'"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtCardNo.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE NOT IN (SELECT EMP_CODE FROM " & vbCrLf & " PAY_FFSETTLE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(EMP_LEAVE_DATE,'MM-YYYY')='" & VB6.Format(mDOL, "MM-YYYY") & "')"

        SqlStr = SqlStr & vbCrLf & " Order By EMP_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmp.EOF = False Then
            Do While RsEmp.EOF = False
                mEmpCode = IIf(IsDbNull(RsEmp.Fields("EMP_CODE").Value), "", RsEmp.Fields("EMP_CODE").Value)
                mEmpDOJ = IIf(IsDbNull(RsEmp.Fields("EMP_DOJ").Value), "", RsEmp.Fields("EMP_DOJ").Value)
                mEmpDOL = IIf(IsDbNull(RsEmp.Fields("EMP_LEAVE_DATE").Value), "", RsEmp.Fields("EMP_LEAVE_DATE").Value)
                mEmpDOL = IIf(mEmpDOL = "", DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSalDate)), mEmpDOL)
                mCountTotSLMin = 0
                mCountTotSL = 0

                For mCntDays = 1 To mLastDayofMonth
                    mMonthDate = CStr(CDate(VB6.Format(mCntDays & "/" & VB6.Format(mSalDate, "MM/YYYY"), "DD/MM/YYYY")))
                    mLeaveType = ""
                    xFirstHalf = ""
                    xSecondHalf = ""

                    If CDate(mMonthDate) < CDate(mEmpDOJ) Then
                        mLeaveType = ""
                    ElseIf CDate(mMonthDate) > CDate(mEmpDOL) Then
                        mLeaveType = ""
                    ElseIf CDate(mMonthDate) >= CDate(PubCurrDate) Then
                        mLeaveType = ""
                    Else
                        mLeaveType = GetLeaveType(mEmpCode, VB6.Format(mMonthDate, "DD/MM/YYYY"))
                        xFirstHalf = VB.Left(mLeaveType, 1)
                        xSecondHalf = VB.Right(mLeaveType, 1)
                    End If

                    mLeaveType = IIf(optMark(0).Checked = True, ABSENT, WOPAY)

                    If xFirstHalf = "Z" Or xSecondHalf = "Z" Then
                        If CheckLeaveData(mEmpCode, mMonthDate) = True Then
                            If xFirstHalf = "Z" Then

                                SqlStr = " UPDATE PAY_ATTN_MST SET FIRSTHALF=" & mLeaveType & "" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND (FIRSTHALF=-1 or FIRSTHALF IS NULL)"

                                PubDBCn.Execute(SqlStr)
                            End If

                            If xSecondHalf = "Z" Then
                                SqlStr = " UPDATE PAY_ATTN_MST SET SECONDHALF=" & mLeaveType & "" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND (SECONDHALF=-1 or SECONDHALF IS NULL)"

                                PubDBCn.Execute(SqlStr)
                            End If
                        Else
                            SqlStr = " INSERT INTO PAY_ATTN_MST ( " & vbCrLf & " COMPANY_CODE, PAYYEAR, EMP_CODE, ATTN_DATE, " & vbCrLf & " FIRSTHALF, SECONDHALF, ADDUSER, ADDDATE, AGT_LATE) VALUES (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(mMonthDate)) & ", '" & mEmpCode & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & IIf(xFirstHalf = "Z", mLeaveType, -1) & ", " & IIf(xSecondHalf = "Z", mLeaveType, -1) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'N')"
                            PubDBCn.Execute(SqlStr)
                        End If
                    End If
                Next

                RsEmp.MoveNext()
                PBar.Value = PBar.Value + 1
            Loop
        End If
        PubDBCn.CommitTrans()
        CalcAbsentMark = True
        Exit Function
ErrPart:
        '    ErrorMsg err.Description, err.Number, vbCritical
        '    Resume
        CalcAbsentMark = False
        PubDBCn.RollbackTrans()
    End Function
    Private Function GetLeaveType(ByRef pCode As String, ByRef pCheckDate As String) As String

        On Error GoTo ErrRefreshScreen
        Dim RS As ADODB.Recordset = Nothing
        Dim xFirstHalf As String
        Dim xSecondHalf As String
        Dim SqlStr As String = ""

        GetLeaveType = ""
        xFirstHalf = ""
        xSecondHalf = ""

        SqlStr = " SELECT ATTN_DATE,SECONDHALF, FIRSTHALF " & vbCrLf & " FROM  PAY_ATTN_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)


        If RS.EOF = False Then
            '            GetLeaveType = mMark(RS!FIRSTHALF)
            '            GetLeaveType = IIf(mMark(RS!SECONDHALF) = "", GetLeaveType, GetLeaveType & ", ") & mMark(RS!SECONDHALF)
            If RS.Fields("FIRSTHALF").Value = HOLIDAY Or RS.Fields("FIRSTHALF").Value = SUNDAY Then
                GetLeaveType = mMark(RS.Fields("FIRSTHALF").Value) & "," & mMark(RS.Fields("SECONDHALF").Value)
            Else
                GetLeaveType = GetMarkFromMachine(pCode, pCheckDate, IIf(RS.Fields("FIRSTHALF").Value = -1, "", mMark(RS.Fields("FIRSTHALF").Value)), IIf(RS.Fields("SECONDHALF").Value = -1, "", mMark(RS.Fields("SECONDHALF").Value)))
            End If
        Else
            GetLeaveType = GetMarkFromMachine(pCode, pCheckDate, "", "")
        End If

        xFirstHalf = VB.Left(GetLeaveType, 1)
        xFirstHalf = IIf(xFirstHalf = ",", "Z", xFirstHalf)

        xSecondHalf = VB.Right(GetLeaveType, 1)
        xSecondHalf = IIf(xSecondHalf = ",", "Z", xSecondHalf)

        GetLeaveType = xFirstHalf & xSecondHalf

        Exit Function
ErrRefreshScreen:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function GetMarkFromMachine(ByRef mEmpCode As String, ByRef pDate As String, ByRef mFirstHalf As String, ByRef mSecondHalf As String) As String
        On Error GoTo ErrPart

        Dim mEmpShiftIN As String
        Dim mEmpShiftOUT As String
        Dim mEmpShiftBreak As String

        Dim mMarginsMinute As Double
        Dim mEmpInTime As String
        Dim mEmpOutTime As String
        Dim mSLTime As String
        Dim mSLOutTime As String
        Dim mIsRoundClock As String
        Dim mShortLeave As Boolean

        mMarginsMinute = IIf(IsDbNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)
        mIsRoundClock = IIf(GetRoundClock(mEmpCode, pDate, "E") = True, "Y", "N")

        mEmpShiftIN = GetShiftTimeNew(mEmpCode, pDate, mMarginsMinute, "I", mIsRoundClock, "E")
        mEmpShiftOUT = GetShiftTimeNew(mEmpCode, pDate, mMarginsMinute, "O", mIsRoundClock, "E")
        '    mEmpShiftBreak = CVDate(Format(DateSerial(Year(mEmpShiftIN), Month(mEmpShiftIN), Day(mEmpShiftIN)) & " " & TimeSerial(Hour(mEmpShiftIN) + 4, Minute(mEmpShiftIN), 0), "DD/MM/YYYY HH:MM"))    ''GetShiftTimeNew(mEmpCode, pDate, mMarginsMinute, "B", "E")
        mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 4, CDate(mEmpShiftIN)), "DD/MM/YYYY HH:MM")))
        mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, 30, CDate(mEmpShiftBreak)), "DD/MM/YYYY HH:MM")))
        mSLTime = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 2, CDate(mEmpShiftIN)), "DD/MM/YYYY HH:MM")))
        mSLOutTime = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, -2, CDate(mEmpShiftOUT)), "DD/MM/YYYY HH:MM")))
        mShortLeave = False

        'DateSerial(year(mEmpShiftOUT), month(mEmpShiftOUT), day(mEmpShiftOUT))

        If CheckEmpTime(mEmpCode, pDate, mEmpInTime, mEmpOutTime, mIsRoundClock) = False Then GoTo ErrPart

        If VB6.Format(mEmpInTime, "HH:MM") = "00:00" Then
            If mFirstHalf = "" Then
                GetMarkFromMachine = "Z"
            Else
                GetMarkFromMachine = mFirstHalf
            End If
        Else
            If mFirstHalf = "" Then
                If CDate(mEmpInTime) <= CDate(mSLTime) Then
                    GetMarkFromMachine = "P"
                Else
                    GetMarkFromMachine = "Z"
                End If
                If CDate(mEmpInTime) > CDate(mEmpShiftIN) And CDate(mEmpInTime) <= CDate(mSLTime) Then
                    mShortLeave = True
                End If
            Else
                GetMarkFromMachine = mFirstHalf
            End If
        End If

        If VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Then
            If mSecondHalf = "" Then
                If mEmpInTime = "00:00" Then
                    GetMarkFromMachine = GetMarkFromMachine & "," & "Z"
                Else
                    GetMarkFromMachine = GetMarkFromMachine & "," & ""
                End If
            ElseIf mSecondHalf <> "" Then
                GetMarkFromMachine = GetMarkFromMachine & "," & mSecondHalf
            End If
        Else
            If mSecondHalf = "" Then
                If mFirstHalf = "" Then
                    '                If mShortLeave = False Then
                    If CDate(mEmpInTime) <= CDate(mEmpShiftBreak) And CDate(mEmpOutTime) >= CDate(mSLOutTime) Then
                        GetMarkFromMachine = GetMarkFromMachine & "," & "P"
                    Else
                        GetMarkFromMachine = GetMarkFromMachine & "," & "Z"
                    End If
                    '                Else
                    '                    If CVDate(mEmpInTime) <= CVDate(mEmpShiftBreak) And CVDate(mEmpOutTime) >= CVDate(mEmpShiftOUT) Then
                    '                        GetMarkFromMachine = GetMarkFromMachine & "," & "P"
                    '                    Else
                    '                        GetMarkFromMachine = GetMarkFromMachine & "," & "Z"
                    '                    End If
                    '                End If

                Else
                    If CDate(mEmpInTime) <= CDate(mEmpShiftBreak) And CDate(mEmpOutTime) >= CDate(mSLOutTime) Then
                        GetMarkFromMachine = GetMarkFromMachine & "," & "P"
                    Else
                        GetMarkFromMachine = GetMarkFromMachine & "," & "Z"
                    End If
                End If
            Else
                GetMarkFromMachine = GetMarkFromMachine & "," & mSecondHalf
            End If
        End If
        Exit Function
ErrPart:
        '    ErrorMsg err.Description, err.Number, vbCritical
        '    Resume
        GetMarkFromMachine = ""
    End Function

    Private Function CheckEmpInLeave(ByRef mEmpCode As String, ByRef mMonthDate As String, ByRef mFirstHalf As Boolean, ByRef mSecondHalf As Boolean) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        mFirstHalf = False
        mSecondHalf = False
        SqlStr = " SELECT FIRSTHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        '    SqlStr = SqlStr & vbCrLf & " AND FIRSTHALF IN (ABSENT,CASUAL,EARN,SICK,CPLEARN,WOPAY,CPLAVAIL,SUNDAY,HOLIDAY)"
        SqlStr = SqlStr & vbCrLf & " AND FIRSTHALF IN (" & ABSENT & "," & CASUAL & "," & EARN & "," & SICK & "," & WOPAY & "," & SUNDAY & "," & HOLIDAY & "," & CPLEARN & "," & CPLAVAIL & ")"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            mFirstHalf = True
        End If

        SqlStr = " SELECT SECONDHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        '    SqlStr = SqlStr & vbCrLf & " AND FIRSTHALF IN (ABSENT,CASUAL,EARN,SICK,CPLEARN,WOPAY,CPLAVAIL,SUNDAY,HOLIDAY)"
        SqlStr = SqlStr & vbCrLf & " AND FIRSTHALF IN (" & ABSENT & "," & CASUAL & "," & EARN & "," & SICK & "," & WOPAY & "," & SUNDAY & "," & HOLIDAY & "," & CPLEARN & "," & CPLAVAIL & ")"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            mSecondHalf = True
        End If

        CheckEmpInLeave = True
        Exit Function
ErrPart:
        CheckEmpInLeave = False

    End Function

    Private Function CheckLeaveData(ByRef mEmpCode As String, ByRef mMonthDate As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckLeaveData = False
        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            CheckLeaveData = True
        End If

        Exit Function
ErrPart:
        CheckLeaveData = False
    End Function


    Private Function GetAttnRecord(ByRef mEmpCode As String, ByRef mMonthDate As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetAttnRecord = False

        SqlStr = " SELECT FIRSTHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            GetAttnRecord = True
        End If

        Exit Function
ErrPart:
        GetAttnRecord = False

    End Function

    Private Function CheckEmpTime(ByRef mEmpCode As String, ByRef mMonthDate As String, ByRef mEmpInTime As String, ByRef mEmpOutTime As String, ByRef mIsRound As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mEMPODOut As String
        Dim mEmpODIn As String

        mEmpInTime = "00:00"
        mEmpOutTime = "00:00"
        mEMPODOut = "00:00"
        mEmpODIn = "00:00"

        SqlStr = " SELECT IN_TIME, OUT_TIME " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN ATTN " & vbCrLf & " WHERE ATTN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ATTN.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mEmpInTime = VB6.Format(IIf(IsDbNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "HH:MM")
            mEmpOutTime = VB6.Format(IIf(IsDbNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "HH:MM")

            mEmpInTime = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpInTime)), Minute(CDate(mEmpInTime)), 0), "DD/MM/YYYY HH:MM")

            If mIsRound = "Y" Then
                mEmpOutTime = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:MM")
            Else
                mEmpOutTime = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:MM")
            End If
        End If

        SqlStr = " SELECT MIN(TO_CHAR(TIME_FROM,'DD-MON-YYYY HH24:MI')) AS TIME_FROM " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields("TIME_FROM").Value) = False Then
                mEMPODOut = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_FROM").Value), "00:00", RsTemp.Fields("TIME_FROM").Value), "HH:MM")
                mEMPODOut = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEMPODOut)), Minute(CDate(mEMPODOut)), 0), "DD/MM/YYYY HH:MM")
            End If
        End If

        If mIsRound = "Y" Then
            SqlStr = " SELECT MIN(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & UCase(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)), "DD-MMM-YYYY")) & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & " AND TO_DATE(TIME_TO,'DD-MON-YYYY HH24:MI')<='" & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 8, CDate(mEmpInTime)), "DD-MMM-YYYY hh:MM") & "'"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDbNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mEmpODIn = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:MM")
                    mEmpODIn = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:MM")
                End If
            End If
        Else
            SqlStr = " SELECT MAX(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDbNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mEmpODIn = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:MM")
                    mEmpODIn = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:MM")
                End If
            End If
        End If

        If VB6.Format(mEmpInTime, "HH:MM") = "00:00" Then
            mEmpInTime = mEMPODOut
        End If

        If VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Then
            mEmpOutTime = mEmpODIn
        End If

        If VB6.Format(mEMPODOut, "HH:MM") <> "00:00" Then
            If CDate(mEMPODOut) < CDate(mEmpInTime) Then
                mEmpInTime = mEMPODOut
            End If
        End If

        If VB6.Format(mEmpODIn, "HH:MM") <> "00:00" Then
            If CDate(mEmpODIn) > CDate(mEmpOutTime) Then
                mEmpOutTime = mEmpODIn
            End If
        End If

        CheckEmpTime = True
        Exit Function
ErrPart:
        CheckEmpTime = False

    End Function
    Private Function GetShortLeave(ByRef mEmpCode As String, ByRef mMonthDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSLTime As String

        SqlStr = " SELECT TOTAL_HRS " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE='P' AND AGT_LEAVE='N'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            mSLTime = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TOTAL_HRS").Value), "", RsTemp.Fields("TOTAL_HRS").Value), "HH:MM")
        End If

        If mSLTime <> "" Then
            GetShortLeave = (Hour(CDate(mSLTime)) * 60) + Minute(CDate(mSLTime))
        End If

        Exit Function
ErrPart:
        GetShortLeave = 0

    End Function


    Private Function FieldVarification() As Boolean
        FieldVarification = True

        If OptParti.Checked = True Then
            If txtCardNo.Text = "" Then
                MsgBox("CardNo. Not Selected.")
                FieldVarification = False
                txtCardNo.Focus()
                Exit Function
            End If
        End If
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SearchErr
        Dim SqlStr As String = ""

        SqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtCardNo.Text = AcName1
            txtName.Text = AcName
            TxtCardNo_Validating(TxtCardNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub FrmMarkAbsentFromMc_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Me.Text = "Mark Absent/Without Pay From M/c Data"
    End Sub

    Private Sub FrmMarkAbsentFromMc_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Me.Left = VB6.TwipsToPixelsX(25)
        Me.Top = VB6.TwipsToPixelsY(25)
        MainClass.SetControlsColor(Me)
        Me.Height = VB6.TwipsToPixelsY(3855)
        Me.Width = VB6.TwipsToPixelsX(5475)

        txtMonth.Enabled = False
        '    TxtYear.Enabled = False

        lblNewDate.Text = CStr(RunDate)

        txtMonth.Text = MonthName(Month(RunDate)) & ", " & Year(RunDate)
        '    TxtYear.Text = Year(RunDate)

        optAll.Checked = True
        HideUnHide(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub FrmMarkAbsentFromMc_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub optAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAll.CheckedChanged
        If eventSender.Checked Then
            HideUnHide(False)
        End If
    End Sub

    Private Sub OptParti_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptParti.CheckedChanged
        If eventSender.Checked Then
            HideUnHide(True)
        End If
    End Sub

    Private Sub TxtCardNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCardNo.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtCardNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtCardNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtCardNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtCardNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtCardNo.Text = "" Then GoTo EventExitSub
        txtCardNo.Text = VB6.Format(txtCardNo.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtCardNo.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("CardNo. Does Not Exist In Master.", MsgBoxStyle.Information)
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub



    'Private Sub UpDMonth_DownClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.DownClick
    '    SetNewDate(-1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text))) & ", " & Year(CDate(lblNewDate.Text))
    'End Sub
    Sub SetNewDate(ByRef prmSpinDirection As Short)
        lblNewDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, prmSpinDirection, CDate(lblNewDate.Text)))
    End Sub
    'Private Sub UpDMonth_UpClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.UpClick
    '    SetNewDate(1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text))) & ", " & Year(CDate(lblNewDate.Text))
    'End Sub


    Private Sub HideUnHide(ByRef mCheck As Boolean)
        txtCardNo.Enabled = mCheck
        cmdsearch.Enabled = mCheck
    End Sub
End Class
