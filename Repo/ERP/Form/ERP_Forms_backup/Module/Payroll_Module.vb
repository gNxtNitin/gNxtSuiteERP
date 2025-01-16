Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Module Payroll_Module
    'Public crapp As New CRAXDDRT.Application
    Public Structure EmpArray
        Dim mHeadingDesc As String
        Dim mRate As Double
        Dim mPayable As Double
        Dim mTitle As String
    End Structure
    Public mEmpEarnData() As EmpArray
    Public mEmpDeductData() As EmpArray
    Public mEmpArrearEarnData() As EmpArray
    Public mEmpArrearDeductData() As EmpArray
    'Global bAuthLogin      As Boolean
    'Global bPopLogin       As Boolean
    'Global bHtml           As Boolean
    ''Global MyEncodeType    As ENCODE_METHOD
    ''Global etPriority      As MAIL_PRIORITY
    'Global bReceipt        As Boolean
    'Global strServerPop3 As String
    'Global strServerSmtp As String
    'Global strAccount As String
    'Global strPassword As String
    Public Function GetOpeningPerksDate() As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        GetOpeningPerksDate = ""
        mSqlStr = "SELECT MAX(SAl_DATE) AS SAl_DATE FROM PAY_PERKS_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BOOKTYPE='O'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetOpeningPerksDate = IIf(IsDbNull(RsTemp.Fields("SAL_DATE").Value), "", RsTemp.Fields("SAL_DATE").Value)
        End If
        Exit Function
ErrPart:
        GetOpeningPerksDate = ""
    End Function
    Public Function CalcAttn(ByRef mCode As String, ByRef mDOJ As String, ByRef mDOL As String, ByRef mSalDate As String,
                             Optional ByRef mLeaveWop As Double = 0, Optional ByRef mAttnDate As String = "", Optional ByRef pAddDate As Double = 0,
                             Optional ByRef mWOP As Double = 0, Optional ByRef mAbsent As Double = 0, Optional ByRef pSalaryType As String = "", Optional ByRef pWithOutExtraLeave As String = "") As Double
        On Error GoTo CalcAttnErr
        Dim RsTempAttn As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mFHalf As Double = 0
        Dim mSHalf As Double = 0
        Dim mDate As String
        Dim xDOJ As Integer
        Dim xDOL As Integer
        Dim mIsCurrentMonthJoining As Boolean
        Dim mCheckDate As String
        Dim mNotMark As Double
        Dim mExtraLeave As Double = 0

        mDate = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) & "/" & VB6.Format(mSalDate, "MM/YYYY")
        mCheckDate = "01" & "/" & VB6.Format(mSalDate, "MM/YYYY")

        SqlStr = " SELECT FIRSTHALF,SECONDHALF, EXTRA_LEAVE, EXTRA_LEAVE_2 " & vbCrLf _
            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
            & " AND TO_CHAR(Attn_Date,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "')"

        If IsDate(mAttnDate) = True Then
            SqlStr = SqlStr & vbCrLf & " AND Attn_Date>=TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If CDate(mDOJ) > CDate(mCheckDate) Then
            SqlStr = SqlStr & vbCrLf & " AND Attn_Date>=TO_DATE('" & VB6.Format(mDOJ, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If mDOL <> "" Then
            If VB6.Format(mSalDate, "YYYYMM") = VB6.Format(mDOL, "YYYYMM") Then
                SqlStr = SqlStr & vbCrLf & " AND Attn_Date<=TO_DATE('" & VB6.Format(mDOL, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempAttn, ADODB.LockTypeEnum.adLockOptimistic)

        xDOJ = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mDOJ), CDate(VB6.Format(mDate, "dd/mm/yyyy")))

        If mDOL <> "" Then
            xDOL = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mDOL), CDate(VB6.Format(mDate, "dd/mm/yyyy")))
        End If

        If VB6.Format(mDOJ, "mm yyyy") = VB6.Format(mDOL, "mm yyyy") Then
            xDOJ = xDOJ - xDOL + 1
        ElseIf VB6.Format(mDOJ, "mm yyyy") = VB6.Format(mDate, "mm yyyy") Then
            xDOJ = xDOJ + 1
        ElseIf VB6.Format(mDOL, "mm yyyy") = VB6.Format(mDate, "mm yyyy") Then
            xDOJ = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) - xDOL
        End If

        If RsTempAttn.EOF = False Then
            Do While Not RsTempAttn.EOF
                If RsTempAttn.Fields("FIRSTHALF").Value = ABSENT Or RsTempAttn.Fields("FIRSTHALF").Value = WOPAY Then
                    mFHalf = mFHalf + 0.5
                End If
                If RsTempAttn.Fields("FIRSTHALF").Value = ABSENT Then
                    mAbsent = mAbsent + 0.5
                End If
                If RsTempAttn.Fields("FIRSTHALF").Value = -1 Then
                    mNotMark = mNotMark + 0.5
                End If
                If RsTempAttn.Fields("FIRSTHALF").Value = WOPAY Then
                    mWOP = mWOP + 0.5
                End If
                If RsTempAttn.Fields("SECONDHALF").Value = ABSENT Then
                    mAbsent = mAbsent + 0.5
                End If
                If RsTempAttn.Fields("SECONDHALF").Value = WOPAY Then
                    mWOP = mWOP + 0.5
                End If
                If RsTempAttn.Fields("SECONDHALF").Value = -1 Then
                    mNotMark = mNotMark + 0.5
                End If
                If RsTempAttn.Fields("SECONDHALF").Value = ABSENT Or RsTempAttn.Fields("SECONDHALF").Value = WOPAY Then
                    mSHalf = mSHalf + 0.5
                End If

                If RsTempAttn.Fields("EXTRA_LEAVE").Value = "Y" Then
                    mExtraLeave = mExtraLeave + 0.5 ''pSalaryType = pSalaryType + 1
                End If

                If RsTempAttn.Fields("EXTRA_LEAVE_2").Value = "Y" Then
                    mExtraLeave = mExtraLeave + 0.5 ''pSalaryType = pSalaryType + 1
                End If

                RsTempAttn.MoveNext()
            Loop
        End If
        If IsDate(mAttnDate) = True Then
            CalcAttn = pAddDate
        ElseIf MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) > xDOJ Then
            CalcAttn = xDOJ
        Else
            CalcAttn = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate)))
        End If

        If pWithOutExtraLeave = "Y" Then
            CalcAttn = CalcAttn - (mFHalf + mSHalf)     '- IIf(pSalaryType = "F", mExtraLeave, 0)
        Else
            CalcAttn = CalcAttn - (mFHalf + mSHalf) - IIf(pSalaryType = "F", mExtraLeave, 0)       ''IIf(pSalaryType = "F", mExtraLeave, 0)
        End If


        mLeaveWop = mFHalf + mSHalf

        Exit Function
CalcAttnErr:
        CalcAttn = 0
    End Function
    Public Function GetLayoffMonth(ByRef mSalDate As String, ByRef pLayOffDateStart As String, ByRef pLayOffDateEnd As String) As Boolean
        On Error GoTo CalcAttnErr
        Dim RsTempAttn As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mDate As String
        Dim mCheckDate As String
        'Dim mDateCount As String
        Dim CntDate As Integer
        mDate = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) & "/" & VB6.Format(mSalDate, "MM/YYYY")
        mCheckDate = "01" & "/" & VB6.Format(mSalDate, "MM/YYYY")
        pLayOffDateStart = ""
        pLayOffDateEnd = ""
        GetLayoffMonth = False
        For CntDate = CInt(mCheckDate) To CInt(mDate)
            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_LAYOFF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DATE_FROM<=TO_DATE('" & VB6.Format(CntDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND DATE_TO>=TO_DATE('" & VB6.Format(CntDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempAttn, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTempAttn.EOF = False Then
                pLayOffDateStart = IIf(IsDbNull(RsTempAttn.Fields("DATE_FROM").Value), "", RsTempAttn.Fields("DATE_FROM").Value)
                pLayOffDateEnd = IIf(IsDbNull(RsTempAttn.Fields("DATE_TO").Value), "", RsTempAttn.Fields("DATE_TO").Value)
                pLayOffDateEnd = IIf(CDate(pLayOffDateEnd) > CDate(mDate), mDate, pLayOffDateEnd)
                GetLayoffMonth = True
                Exit Function
            End If
        Next
        Exit Function
CalcAttnErr:
        GetLayoffMonth = False
    End Function
    Public Function GetLayoffDate(ByRef mSalDate As String) As Boolean
        On Error GoTo CalcAttnErr
        Dim RsTempAttn As ADODB.Recordset
        Dim SqlStr As String = ""
        GetLayoffDate = False
        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_LAYOFF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DATE_FROM<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND DATE_TO>=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempAttn, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTempAttn.EOF = False Then
            GetLayoffDate = True
            Exit Function
        End If
        Exit Function
CalcAttnErr:
        GetLayoffDate = False
    End Function
    Public Function GetMarkAbsent(ByRef mEmpCode As String, ByRef mAttnDate As String, ByRef pCheckType As String) As Boolean
        On Error GoTo CalcAttnErr
        Dim RsTempAttn As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mAttnDateFrom As String
        Dim mAttnDateTo As String
        Dim mPreviousAbsent As Boolean = False
        Dim mNextAbsent As Boolean = False

        GetMarkAbsent = False

        If pCheckType = "S" Then
            mAttnDateFrom = DateAdd("d", -4, mAttnDate)
            mAttnDateTo = DateAdd("d", -1, mAttnDate)

            SqlStr = " SELECT * " & vbCrLf _
            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND DATE_FROM<=TO_DATE('" & VB6.Format(mAttnDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND DATE_TO>=TO_DATE('" & VB6.Format(mAttnDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

            SqlStr = SqlStr & vbCrLf _
            & " AND FIRSTHALF IN (" & PRESENT & "," & WFH & "," & HOLIDAY & ")"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempAttn, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTempAttn.EOF = False Then
                GetMarkAbsent = False
            Else
                GetMarkAbsent = True
            End If
        Else
            mAttnDateFrom = DateAdd("d", -1, mAttnDate)
            mAttnDateTo = DateAdd("d", 1, mAttnDate)

            SqlStr = " SELECT * " & vbCrLf _
            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND DATE_FROM=TO_DATE('" & VB6.Format(mAttnDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND FIRSTHALF IN (" & PRESENT & "," & WFH & "," & HOLIDAY & ")"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempAttn, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTempAttn.EOF = False Then
                mPreviousAbsent = False
            Else
                mPreviousAbsent = True
            End If

            SqlStr = " SELECT * " & vbCrLf _
            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND DATE_FROM=TO_DATE('" & VB6.Format(mAttnDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND FIRSTHALF IN (" & PRESENT & "," & WFH & ")"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempAttn, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTempAttn.EOF = False Then
                mNextAbsent = False
            Else
                mNextAbsent = True
            End If

            If mPreviousAbsent = True And mNextAbsent = True Then
                GetMarkAbsent = True
            Else
                GetMarkAbsent = False
            End If
        End If

        Exit Function
CalcAttnErr:
        GetMarkAbsent = False
    End Function
    Public Function CalcAttnPresent(ByRef mCode As String, ByRef mFromDate As String, ByRef mToDate As String, ByRef mDOJ As String) As Double
        On Error GoTo CalcAttnErr
        Dim RsTempAttn As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mFHalf As Double
        Dim mSHalf As Double
        Dim xStartDate As String
        If CDate(mDOJ) > CDate(mFromDate) Then
            xStartDate = mDOJ
        Else
            xStartDate = mFromDate
        End If
        SqlStr = " SELECT FIRSTHALF,SECONDHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'"
        SqlStr = SqlStr & vbCrLf & " AND Attn_Date > = TO_DATE('" & VB6.Format(xStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND Attn_Date < = TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempAttn, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTempAttn.EOF = False Then
            Do While Not RsTempAttn.EOF
                If RsTempAttn.Fields("FIRSTHALF").Value = CASUAL Or RsTempAttn.Fields("FIRSTHALF").Value = EARN Or RsTempAttn.Fields("FIRSTHALF").Value = SICK Or RsTempAttn.Fields("FIRSTHALF").Value = MATERNITY Or RsTempAttn.Fields("FIRSTHALF").Value = CPLEARN Or RsTempAttn.Fields("FIRSTHALF").Value = CPLAVAIL Or RsTempAttn.Fields("FIRSTHALF").Value = SUNDAY Or RsTempAttn.Fields("FIRSTHALF").Value = HOLIDAY Or RsTempAttn.Fields("FIRSTHALF").Value = PRESENT Then
                    mFHalf = mFHalf + 0.5
                End If
                If RsTempAttn.Fields("SECONDHALF").Value = CASUAL Or RsTempAttn.Fields("SECONDHALF").Value = EARN Or RsTempAttn.Fields("SECONDHALF").Value = SICK Or RsTempAttn.Fields("SECONDHALF").Value = MATERNITY Or RsTempAttn.Fields("SECONDHALF").Value = CPLEARN Or RsTempAttn.Fields("SECONDHALF").Value = CPLAVAIL Or RsTempAttn.Fields("SECONDHALF").Value = SUNDAY Or RsTempAttn.Fields("SECONDHALF").Value = HOLIDAY Or RsTempAttn.Fields("SECONDHALF").Value = PRESENT Then
                    mSHalf = mSHalf + 0.5
                End If
                RsTempAttn.MoveNext()
            Loop
        End If
        '    CalcAttnPresent = DateDiff("d", xStartDate, mTodate) + 1
        CalcAttnPresent = (mFHalf + mSHalf) ' CalcAttnPresent - (mFHalf + mSHalf)
        Exit Function
CalcAttnErr:
        CalcAttnPresent = 0
    End Function
    Public Function GetMonthlyWorkingDays(ByRef pDBCn As ADODB.Connection, ByRef pEmpCode As String, ByRef pRunDate As String, ByRef mDOJ As String, ByRef mDOL As String) As Double
        On Error GoTo ErrPart
        Dim RsBalEL As ADODB.Recordset
        Dim mFHalf As Double
        Dim mSHalf As Double
        Dim xRunDate As String
        Dim mTotalLeaves As Double
        Dim mTotalHoliDays As Double
        Dim mTotalRunningDays As Double
        Dim mStartingDate As String
        Dim mEndingDate As String
        Dim SqlStr As String = ""
        GetMonthlyWorkingDays = 0
        If VB6.Format(pRunDate, "YYYYMM") < VB6.Format(mDOJ, "YYYYMM") Then
            Exit Function
        End If
        xRunDate = VB6.Format(pRunDate, "DD/MM/YYYY")
        mStartingDate = VB6.Format("01/" & Month(CDate(pRunDate)) & "/" & Year(CDate(xRunDate)), "DD/MM/YYYY")
        mEndingDate = VB6.Format(MainClass.LastDay(Month(CDate(pRunDate)), Year(CDate(pRunDate))) & "/" & Month(CDate(pRunDate)) & "/" & Year(CDate(xRunDate)), "DD/MM/YYYY")
        If mDOL <> "" Then
            If CDate(mDOL) < CDate(mStartingDate) Then
                Exit Function
            End If
        End If
        If mDOJ = "" Then
        ElseIf CDate(mStartingDate) < CDate(mDOJ) Then
            mStartingDate = mDOJ
        End If
        If mDOL = "" Then
        ElseIf CDate(mEndingDate) > CDate(mDOL) Then
            mEndingDate = mDOL
        End If
        mTotalRunningDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartingDate), CDate(mEndingDate)) + 1
        SqlStr = " SELECT FIRSTHALF, SECONDHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & pEmpCode & "' " & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalEL, ADODB.LockTypeEnum.adLockOptimistic)
        If RsBalEL.EOF = False Then
            Do While Not RsBalEL.EOF
                '            If RsBalEL!FIRSTHALF <> -1 Then
                If RsBalEL.Fields("FIRSTHALF").Value = CPLEARN Or RsBalEL.Fields("FIRSTHALF").Value = CPLAVAIL Or RsBalEL.Fields("FIRSTHALF").Value = PRESENT Or RsBalEL.Fields("FIRSTHALF").Value = -1 Then
                ElseIf RsBalEL.Fields("FIRSTHALF").Value = SUNDAY Or RsBalEL.Fields("FIRSTHALF").Value = HOLIDAY Then
                    mTotalHoliDays = mTotalHoliDays + 0.5
                Else
                    mFHalf = mFHalf + 0.5
                End If
                '            End If
                '            If RsBalEL!SECONDHALF <> -1 Then
                If RsBalEL.Fields("SECONDHALF").Value = CPLEARN Or RsBalEL.Fields("SECONDHALF").Value = CPLAVAIL Or RsBalEL.Fields("SECONDHALF").Value = PRESENT Or RsBalEL.Fields("SECONDHALF").Value = -1 Then
                ElseIf RsBalEL.Fields("SECONDHALF").Value = SUNDAY Or RsBalEL.Fields("SECONDHALF").Value = HOLIDAY Then
                    mTotalHoliDays = mTotalHoliDays + 0.5
                Else
                    mSHalf = mSHalf + 0.5
                End If
                '            End If
                RsBalEL.MoveNext()
            Loop
        End If
        mTotalLeaves = mFHalf + mSHalf
        If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
            If Year(CDate(xRunDate)) < 2006 Then
                SqlStr = " SELECT COUNT(1) AS HOLIDAYCNT " & vbCrLf & " FROM PAY_HOLIDAY_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HOLIDAY_DATE>=TO_DATE('" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND HOLIDAY_DATE<=TO_DATE('" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
                MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalEL, ADODB.LockTypeEnum.adLockOptimistic)
                If RsBalEL.EOF = False Then
                    mTotalHoliDays = IIf(IsDBNull(RsBalEL.Fields("HOLIDAYCNT").Value), 0, RsBalEL.Fields("HOLIDAYCNT").Value)
                End If
            End If
        Else
            If Year(CDate(xRunDate)) <= 2006 Then
                SqlStr = " SELECT COUNT(1) AS HOLIDAYCNT " & vbCrLf & " FROM PAY_HOLIDAY_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HOLIDAY_DATE>=TO_DATE('" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND HOLIDAY_DATE<=TO_DATE('" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
                MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalEL, ADODB.LockTypeEnum.adLockOptimistic)
                If RsBalEL.EOF = False Then
                    mTotalHoliDays = IIf(IsDbNull(RsBalEL.Fields("HOLIDAYCNT").Value), 0, RsBalEL.Fields("HOLIDAYCNT").Value)
                End If
            End If
        End If
        If RsBalEL.EOF = False Then
            mTotalHoliDays = IIf(IsDbNull(RsBalEL.Fields("HOLIDAYCNT").Value), 0, RsBalEL.Fields("HOLIDAYCNT").Value)
        End If
        GetMonthlyWorkingDays = mTotalRunningDays - mTotalLeaves - mTotalHoliDays
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Public Function FillPrintDummyData(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer, ByRef mPvtDBCn As ADODB.Connection) As Boolean
        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr
        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String = ""
        mPvtDBCn.Errors.Clear()
        mPvtDBCn.BeginTrans()
        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        mPvtDBCn.Execute(SqlStr)
        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 1
            SetData = ""
            GetData = ""
            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                GridName.Col = FieldNum
                If FieldNum = prmStartGridCol Then
                    SetData = "FIELD" & FieldCnt
                    GetData = "'" & MainClass.AllowSingleQuote(Mid(GridName.Text, 1, 254)) & "'"
                Else
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(Mid(GridName.Text, 1, 254)) & "'"
                End If
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf _
                & " " & SetData & ") " & vbCrLf _
                & " VALUES (" & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf _
                & " " & GetData & ") "

            mPvtDBCn.Execute(SqlStr)
NextRec:
        Next
        mPvtDBCn.CommitTrans()
        FillPrintDummyData = True
        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillPrintDummyData = False
        mPvtDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function FetchRecordForReport(ByRef mSqlStr As String) As String
        mSqlStr = " SELECT * " & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf _
            & " WHERE  " & vbCrLf _
            & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf _
            & " ORDER BY SUBROW"
        FetchRecordForReport = mSqlStr
    End Function
    Public Sub ColTotal(ByRef sprd As Object, ByRef Col As Integer, ByRef col2 As Integer)
        Dim cntCol As Integer
        Dim cntRow As Integer
        Dim TotCol As Double
        With sprd
            .MaxRows = .MaxRows + 2
            For cntCol = Col To col2
                .Col = cntCol
                For cntRow = 1 To .MaxRows - 2
                    .Row = cntRow
                    TotCol = TotCol + IIf(IsNumeric(.Text), .Text, 0)
                Next
                .Row = .MaxRows
                .Text = VB6.Format(TotCol, "0.00")
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)        ''.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 
                TotCol = 0
            Next
        End With
    End Sub
    Public Sub FlxGridColTotal(ByRef flxGrid As Object, ByRef Col As Integer, ByRef col2 As Integer)
        Dim cntCol As Integer
        Dim cntRow As Integer
        Dim ColTotal As Double
        With flxGrid
            .rows = .rows + 2
            For cntCol = Col To col2
                .Col = cntCol
                For cntRow = 1 To .rows - 2
                    .Row = cntRow
                    ColTotal = ColTotal + IIf(IsNumeric(.Text), .Text, 0)
                Next
                .Row = .rows - 1
                .Text = MainClass.FormatRupees(ColTotal)
                ColTotal = 0
            Next
        End With
    End Sub
    'Public Function PaiseRound(Num As Double, Rnm As Double) As Double
    '    ' Num = 12.23 and Rnm = .05
    '    Dim nn As Double
    '    Dim mm As Double
    '    Dim a As Double
    '    Dim b As Double
    '    Dim c As Double
    '    Dim f As Double
    '    If Rnm = 0 Then
    '        PaiseRound = Num
    '    ElseIf Rnm = 1 Then
    '        PaiseRound = Format(Str(Num), "#######0")
    '    Else
    '        If Num <> 0 Then
    '             nn = (Num - Fix(Num)) * 100   ' nn = 23  paise
    '             mm = nn / (Rnm * 100)          ' mm = 23/5 = 4.60
    '             a = Fix(mm) * Rnm * 100        ' a = 20
    '             If nn = a Then
    '                c = 0
    '             Else
    '                b = nn - a                               ' b = 23- 20 = 3
    '                c = (Rnm * 100) - b                 'c = 2
    '             End If
    '             f = Num + c / 100
    '        End If
    '        PaiseRound = f
    '    End If
    'End Function
    'Public Function GETEntitleEarnLeave(pDBCn As ADODB.Connection, pEmpCode As String, pLeaveCode As Long, pRunDate As String) As Double
    'On Error GoTo ErrPart
    'Dim RsBalEL As ADODB.Recordset
    'Dim RsEmp As ADODB.Recordset = Nothing
    'Dim mFHalf As Double
    'Dim mSHalf As Double
    'Dim xRunDate As String
    'Dim mTotalLeaves As Double
    'Dim mTotalHoliDays As Double
    'Dim mTotalRunningDays As Double
    'Dim mDOJ As String
    'Dim mDOL As String
    'Dim mStartingDate As String
    'Dim mEndingDate As String
    'Dim SqlStr As String=""=""
    'Dim mCategory As String
    '    GETEntitleEarnLeave = 0
    '
    '
    ''    If pLeaveCode <> EARN Then Exit Function
    '
    '    xRunDate = Format(pRunDate, "DD/MM/YYYY")
    '
    '    mStartingDate = "01/01/" & Year(xRunDate)
    '    mEndingDate = xRunDate      ''  11-01-2007  MainClass.LastDay(Month(xRunDate), Year(xRunDate)) & "/" & vb6.Format(xRunDate, "MM/YYYY")
    ''    mEndingDate = "31/12/" & Year(xRunDate)
    '
    '    SqlStr = " SELECT EMP_DOJ, EMP_LEAVE_DATE,EMP_CATG " & vbCrLf _
    ''            & " FROM PAY_EMPLOYEE_MST WHERE " & vbCrLf _
    ''            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''            & " AND EMP_CODE ='" & pEmpCode & "'"
    '
    '    MainClass.UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RsEmp, adLockOptimistic
    '
    '    If RsEmp.EOF = False Then
    '        mDOJ = IIf(IsNull(RsEmp!EMP_DOJ), "", RsEmp!EMP_DOJ)
    '        mDOL = IIf(IsNull(RsEmp!EMP_LEAVE_DATE), "", RsEmp!EMP_LEAVE_DATE)
    '        mCategory = IIf(IsNull(RsEmp!EMP_CATG), "G", RsEmp!EMP_CATG)
    '    End If
    '
    '    If mDOJ = "" Then
    '
    '    ElseIf CDate(mStartingDate) < CDate(mDOJ) Then
    '        mStartingDate = mDOJ
    '    End If
    '
    '    If mDOL = "" Then
    '
    '    ElseIf CDate(mEndingDate) > CDate(mDOL) Then
    '        mEndingDate = mDOL
    '    End If
    '
    '    mTotalRunningDays = DateDiff("d", mStartingDate, mEndingDate) + 1
    '
    '    SqlStr = " SELECT FIRSTHALF, SECONDHALF " & vbCrLf _
    ''            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
    ''            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''            & " AND PAYYEAR=" & Year(xRunDate) & " " & vbCrLf _
    ''            & " AND EMP_CODE ='" & pEmpCode & "'" & vbCrLf _
    ''            & " AND ATTN_DATE<='" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "'"
    '
    '
    '    ''Not Required.....
    '    'SqlStr = SqlStr & vbCrLf _
    ''            & " AND ATTN_DATE NOT IN " & vbCrLf _
    ''            & " (SELECT HOLIDAY_DATE AS HOLIDAYCNT " & vbCrLf _
    ''            & " FROM PAY_HOLIDAY_MST WHERE " & vbCrLf _
    ''            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''            & " AND HOLIDAY_DATE>='" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "'" & vbCrLf _
    ''            & " AND HOLIDAY_DATE<='" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "')"
    '
    '
    '    MainClass.UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RsBalEL, adLockOptimistic
    '
    '    If RsBalEL.EOF = False Then
    '        Do While Not RsBalEL.EOF
    '            If RsBalEL!FIRSTHALF <> -1 Then
    '                If RsBalEL!FIRSTHALF = CPLEARN Or RsBalEL!FIRSTHALF = CPLAVAIL Then
    '
    '                ElseIf RsBalEL!FIRSTHALF = SUNDAY Or RsBalEL!FIRSTHALF = HOLIDAY Then
    '                    mTotalHoliDays = mTotalHoliDays + 0.5
    '                Else
    '                    mFHalf = mFHalf + 0.5
    '                End If
    '            End If
    '
    '            If RsBalEL!SECONDHALF <> -1 Then
    '                If RsBalEL!SECONDHALF = CPLEARN Or RsBalEL!SECONDHALF = CPLAVAIL Then
    '
    '                ElseIf RsBalEL!SECONDHALF = SUNDAY Or RsBalEL!SECONDHALF = HOLIDAY Then
    '                    mTotalHoliDays = mTotalHoliDays + 0.5
    '                Else
    '                    mSHalf = mSHalf + 0.5
    '                End If
    '            End If
    '            RsBalEL.MoveNext
    '        Loop
    '    End If
    '
    '    mTotalLeaves = mFHalf + mSHalf
    '
    '    If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
    '        If Year(xRunDate) < 2006 Then
    '            SqlStr = " SELECT COUNT(1) AS HOLIDAYCNT " & vbCrLf _
    ''                & " FROM PAY_HOLIDAY_MST WHERE " & vbCrLf _
    ''                & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''                & " AND HOLIDAY_DATE>='" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "' AND HOLIDAY_DATE<='" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "' "
    '
    '            MainClass.UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RsBalEL, adLockOptimistic
    '
    '            If RsBalEL.EOF = False Then
    '                mTotalHoliDays = IIf(IsNull(RsBalEL!HOLIDAYCNT), 0, RsBalEL!HOLIDAYCNT)
    '            End If
    '        End If
    '    Else
    '        If Year(xRunDate) <= 2006 Then
    '            SqlStr = " SELECT COUNT(1) AS HOLIDAYCNT " & vbCrLf _
    ''                    & " FROM PAY_HOLIDAY_MST WHERE " & vbCrLf _
    ''                    & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''                    & " AND HOLIDAY_DATE>='" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "' AND HOLIDAY_DATE<='" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "' "
    '
    '            MainClass.UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RsBalEL, adLockOptimistic
    '
    '            If RsBalEL.EOF = False Then
    '                mTotalHoliDays = IIf(IsNull(RsBalEL!HOLIDAYCNT), 0, RsBalEL!HOLIDAYCNT)
    '            End If
    '        End If
    '    End If
    '
    '    If RsBalEL.EOF = False Then
    '        mTotalHoliDays = IIf(IsNull(RsBalEL!HOLIDAYCNT), 0, RsBalEL!HOLIDAYCNT)
    '    End If
    '
    '    GETEntitleEarnLeave = mTotalRunningDays - mTotalLeaves - mTotalHoliDays
    '
    '    If mCategory = "G" Or mCategory = "P" Or mCategory = "D" Then
    '         GETEntitleEarnLeave = GETEntitleEarnLeave / IIf(RsCompany!STAFF_EL_PER_DAYS = 0, 15, RsCompany!STAFF_EL_PER_DAYS)
    '    ElseIf mCategory = "S" Or mCategory = "E" Then
    '        If Val(pEmpCode) < 1000 Then
    '            GETEntitleEarnLeave = GETEntitleEarnLeave / IIf(RsCompany!STAFF_EL_PER_DAYS = 0, 15, RsCompany!STAFF_EL_PER_DAYS)
    '        Else
    '            GETEntitleEarnLeave = GETEntitleEarnLeave / IIf(RsCompany!WORKER_EL_PER_DAYS = 0, 15, RsCompany!WORKER_EL_PER_DAYS)
    '        End If
    '    Else
    '        GETEntitleEarnLeave = GETEntitleEarnLeave / IIf(RsCompany!WORKER_EL_PER_DAYS = 0, 15, RsCompany!WORKER_EL_PER_DAYS)      '20
    '    End If
    '
    '    GETEntitleEarnLeave = Round(GETEntitleEarnLeave + 0.01, 0)
    '
    '    Exit Function
    'ErrPart:
    '    ErrorMsg err.Description, err.Number, vbCritical
    'End Function
    'Public Function GETCPL(pDBCn As ADODB.Connection, pEmpCode As String, pRunDate As String) As Double
    'On Error GoTo ErrPart
    'Dim RsBalEL As ADODB.Recordset
    'Dim RsEmp As ADODB.Recordset = Nothing
    'Dim mFHalf As Double
    'Dim mSHalf As Double
    'Dim xRunDate As String
    'Dim SqlStr As String=""=""
    '
    '    GETCPL = 0
    '
    '
    ''    If pLeaveCode <> EARN Then Exit Function
    '
    '    xRunDate = Format(pRunDate, "DD/MM/YYYY")
    '
    '    SqlStr = " SELECT FIRSTHALF, SECONDHALF " & vbCrLf _
    ''            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
    ''            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''            & " AND PAYYEAR=" & Year(xRunDate) & " " & vbCrLf _
    ''            & " AND EMP_CODE ='" & pEmpCode & "'" & vbCrLf _
    ''            & " AND ATTN_DATE<='" & VB6.Format(xRunDate, "DD-MMM-YYYY") & "'"
    '
    '    MainClass.UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RsBalEL, adLockOptimistic
    '
    '    If RsBalEL.EOF = False Then
    '        Do While Not RsBalEL.EOF
    '            If RsBalEL!FIRSTHALF = CPLEARN Then
    '                 mFHalf = mFHalf + 0.5
    '            End If
    '
    '            If RsBalEL!SECONDHALF = CPLEARN Then
    '                 mSHalf = mSHalf + 0.5
    '            End If
    '            RsBalEL.MoveNext
    '        Loop
    '    End If
    '
    '    GETCPL = mFHalf + mSHalf
    '
    '    Exit Function
    'ErrPart:
    '    ErrorMsg err.Description, err.Number, vbCritical
    'End Function
    '
    Public Function GetCPLPaid(ByRef mCode As String, ByRef xSalDate As String, ByRef pDBCn As ADODB.Connection) As Double
        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        GetCPLPaid = 0
        SqlStr = " SELECT SUM(CPLPAIDDAYS) As CPLPAIDDAYS " & vbCrLf & " FROM PAY_CPL_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(xSalDate)) & " AND TO_CHAR(PAID_MONTH,'YYYYMM')='" & VB6.Format(xSalDate, "YYYYMM") & "'" & vbCrLf & " AND EMP_CODE ='" & mCode & "'"
        MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            GetCPLPaid = IIf(IsDBNull(RsTemp.Fields("CPLPAIDDAYS").Value), 0, RsTemp.Fields("CPLPAIDDAYS").Value)
        End If
        Exit Function
ErrPart:
        GetCPLPaid = 0
    End Function
    Public Function CheckESICeiling(ByRef mDate As String) As Double
        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConESI & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        SqlStr = " SELECT * FROM PAY_PFESICEILING_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConESI & " AND WEF=(" & mSqlStr & ") "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            CheckESICeiling = IIf(IsDBNull(RsCeiling.Fields("CEILING").Value), 0, RsCeiling.Fields("CEILING").Value)
        Else
            CheckESICeiling = 21000
        End If
        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Public Function CheckPFCeiling(ByRef mDate As String) As Double
        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1

        CheckPFCeiling = 0
        If Trim(mDate) = "" Then Exit Function

        Dim SqlStr As String = ""
        Dim mSqlStr As String
        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConPF & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        SqlStr = " SELECT * FROM PAY_PFESICEILING_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConPF & " AND WEF=(" & mSqlStr & ") "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            CheckPFCeiling = IIf(IsDBNull(RsCeiling.Fields("CEILING").Value), 0, RsCeiling.Fields("CEILING").Value)
        Else
            CheckPFCeiling = 15000
        End If
        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Public Function CheckPFCeilingOn(ByRef mDate As String) As String
        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String

        CheckPFCeilingOn = "B"
        If Trim(mDate) = "" Then Exit Function

        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf _
            & " CODE=" & ConPF & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = " SELECT EMPER_CONT FROM PAY_PFESICEILING_MST WHERE " & vbCrLf _
            & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf _
            & " CODE=" & ConPF & " AND WEF=(" & mSqlStr & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            CheckPFCeilingOn = IIf(IsDBNull(RsCeiling.Fields("EMPER_CONT").Value), "B", RsCeiling.Fields("EMPER_CONT").Value)
        Else
            CheckPFCeilingOn = "B"
        End If
        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Public Function GetEMPWEFDate(ByRef pEmpCode As String, ByRef pSalDate As String) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFromDate As String
        Dim mToDate As String
        Dim mTotMonth As Double
        Dim mAddDays As Double
        mSqlStr = ""
        GetEMPWEFDate = ""
        mSqlStr = " SELECT SALARY_EFF_DATE, SALARY_APP_DATE,TOT_ARR_MONTH,ADDDAYS_IN " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & pEmpCode & "'" & vbCrLf & " AND SALARYDEF.ARREAR_DATE=TO_DATE('" & VB6.Format(pSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IS_ARREAR ='Y'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mFromDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALARY_EFF_DATE").Value), "", RsTemp.Fields("SALARY_EFF_DATE").Value), "DD/MM/YYYY")
            mTotMonth = IIf(IsDBNull(RsTemp.Fields("TOT_ARR_MONTH").Value), 0, RsTemp.Fields("TOT_ARR_MONTH").Value)
            mAddDays = IIf(IsDBNull(RsTemp.Fields("ADDDAYS_IN").Value), 0, RsTemp.Fields("ADDDAYS_IN").Value)
            mTotMonth = mTotMonth - 1
            If mTotMonth = 0 Then
                mToDate = mFromDate
            Else
                mToDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, mTotMonth, CDate(mFromDate)))
            End If
            If mAddDays > 0 Then
                mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1 * mAddDays, CDate(mFromDate)))
            End If
            mFromDate = VB6.Format(mFromDate, "MMM-YYYY")
            mToDate = VB6.Format(mToDate, "MMM-YYYY")
            GetEMPWEFDate = "Arrear : " & mFromDate & IIf(CDate(mFromDate) = CDate(mToDate), "", " To " & mToDate)
        End If
        Exit Function
ErrPart:
        ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
    End Function
    Public Function GetMonthHolidays(ByRef pDate As String, ByRef mEmpCode As String, Optional ByRef pJoiningDate As String = "") As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCategory As String
        GetMonthHolidays = 0
        If CurrModuleName = mContPayrollModule Then
            mCategory = "C"
        Else
            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CAT_TYPE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCategory = MasterNo
            Else
                mCategory = "1"
            End If
            mCategory = IIf(mCategory = "1", "S", "W")
        End If
        SqlStr = " SELECT COUNT(1) AS LDAYS FROM PAY_HOLIDAY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(HOLIDAY_DATE,'MON-YYYY')='" & UCase(VB6.Format(pDate, "MMM-YYYY")) & "'"
        If CurrModuleName = mContPayrollModule Then
            SqlStr = SqlStr & vbCrLf & " AND APP_CONTRACTOR='Y'"
        Else
            If mCategory = "S" Then
                SqlStr = SqlStr & vbCrLf & " AND APP_STAFF='Y'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND APP_RW='Y'"
            End If
        End If
        If pJoiningDate <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND HOLIDAY_DATE>=TO_DATE('" & VB6.Format(pJoiningDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetMonthHolidays = IIf(IsDBNull(RsTemp.Fields("LDAYS").Value), 0, RsTemp.Fields("LDAYS").Value)
        End If
        Exit Function
ErrPart:
        GetMonthHolidays = 0
    End Function
    Public Function CheckSalVoucher(ByRef mYM As Integer, ByRef mVNo As String, ByRef mVDate As String, ByRef mVType As String, ByRef mVSeqNo As Integer, ByRef mVNoSuffix As String, ByRef mBankCode As Integer, ByRef mBookType As String, ByRef mBookSubType As String, ByRef mDivisionCode As Double, Optional ByRef mELYear As Integer = 0) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mKey As String
        CheckSalVoucher = False

        SqlStr = " SELECT * FROM FIN_SalVoucher_TRN  " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BookType='" & mBookType & "'" & vbCrLf _
            & " AND BookSubType='" & mBookSubType & "' AND DIV_CODE=" & mDivisionCode & "" '& vbCrLf |            & " AND YM=" & mYM & ""
        If mBookType = "F" Or mBookType = "L" Then
            SqlStr = SqlStr & vbCrLf & " AND BANKCODE=" & Val(CStr(mBankCode)) & " "
            If mELYear <> 0 And mBookType = "L" Then
                SqlStr = SqlStr & vbCrLf & " AND EL_YEAR=" & mELYear & ""
            End If
        ElseIf mBookType = "Q" Then
            SqlStr = SqlStr & vbCrLf & " AND BANKCODE=" & Val(CStr(mBankCode)) & " AND YM=" & mYM & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND YM=" & mYM & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc)
        If RsMisc.EOF = False Then
            mKey = IIf(IsDBNull(RsMisc.Fields("mKey").Value), "", RsMisc.Fields("mKey").Value)
            mBankCode = RsMisc.Fields("BANKCODE").Value
            If mKey <> "" Then
                'FYEAR=" & RsCompany.Fields("FYEAR").Value & "
                SqlStr = " SELECT * FROM FIN_VOUCHER_HDR  " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND " & vbCrLf & " MKEY='" & mKey & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc)
                If RsMisc.EOF = False Then
                    mVType = IIf(IsDBNull(RsMisc.Fields("VTYPE").Value), "", RsMisc.Fields("VTYPE").Value)
                    mVSeqNo = RsMisc.Fields("VNOSEQ").Value
                    mVNoSuffix = IIf(IsDBNull(RsMisc.Fields("VNOSUFFIX").Value), "", RsMisc.Fields("VNOSUFFIX").Value)
                    mVNo = RsMisc.Fields("VNO").Value
                    mVDate = RsMisc.Fields("VDATE").Value
                    CheckSalVoucher = True
                End If
            End If
        Else
            CheckSalVoucher = False
        End If
        Exit Function
ErrPart:
        MsgInformation(Err.Description)
        CheckSalVoucher = False
    End Function
    Public Function CreateTxtFileForBank(ByRef pSprdAttn As AxFPSpreadADO.AxfpSpread, ByRef pCardCol As Integer, ByRef pNameCol As Integer, ByRef pPaymentTypeCol As Integer, ByRef pBankNoCol As Integer, ByRef pNetAmountCol As Integer, ByRef pBankName As String, ByRef pNarration As String, ByRef pMaxRow As Integer) As Boolean
        On Error GoTo ErrPart
        Dim mLineCount As Integer
        Dim pPageNo As Integer
        Dim cntRow As Double
        Dim pFileName As String
        Dim mAmount As String
        Dim mEmpName As String
        Dim mCardNo As String
        Dim mCheckBankName As String
        Dim mFileFormat As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        If FillDummyTableForBank(pSprdAttn, pCardCol, pNameCol, pPaymentTypeCol, pBankNoCol, pNetAmountCol, pBankName, pNarration, pMaxRow) = False Then
            CreateTxtFileForBank = False
            Exit Function
        End If
        SqlStr = "SELECT FIELD1 AS EMPCODE, FIELD2 AS EMPNAME, FIELD3 As PAYMENTTYPE, FIELD4 AS BANKNO, " & vbCrLf & " SUM(FIELD5) AS AMOUNT, FIELD6 As BANKNAME, FIELD7 AS NARR, SUBROW " & vbCrLf & " FROM TEMP_PRINTDUMMYDATA " & vbCrLf & " WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " GROUP BY FIELD1, FIELD2, FIELD3, FIELD4, FIELD6, FIELD7, SUBROW ORDER BY SUBROW"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = True Then
            MsgInformation("Nothing to Print.")
            CreateTxtFileForBank = True
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable(pBankName, "BANK_NAME", "BANK_FORMAT", "PAY_BANK_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mFileFormat = Val(MasterNo)
        Else
            mFileFormat = IIf(RsCompany.Fields("COMPANY_CODE").Value = 16, 4, 1)
        End If
        If mFileFormat = 2 Then ''KOTAK MAHINDRA BANK
            If CreateXLSFileForBank(RsTemp, pBankName, pNarration) = False Then GoTo ErrPart
            CreateTxtFileForBank = True
            Exit Function
        End If
        If mFileFormat = 4 Then ''Sabo
            If CreateXLSFileForSabo(RsTemp, pBankName, pNarration) = False Then GoTo ErrPart
            CreateTxtFileForBank = True
            Exit Function
        End If
        mLineCount = 1
        pFileName = mLocalPath & "\BankList.txt"
        ''Shell "ATTRIB +A -R " & pFileName
        Call ShellAndContinue("ATTRIB +A -R " & pFileName)
        If RsTemp.EOF = False Then
            FileOpen(1, pFileName, OpenMode.Output)
            Do While RsTemp.EOF = False '.MaxRows - 2
                If mLineCount = 1 Then
                    pPageNo = pPageNo + 1
                End If
                mCardNo = Trim(IIf(IsDBNull(RsTemp.Fields("EMPCODE").Value), "", RsTemp.Fields("EMPCODE").Value))
                If MainClass.ValidateWithMasterTable(mCardNo, "EMP_CODE", "EMP_BANK_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCheckBankName = MasterNo
                Else
                    mCheckBankName = ""
                End If
                If Trim(UCase(pBankName)) = Trim(UCase(mCheckBankName)) Or pBankName = "" Then
                    If UCase(Trim(IIf(IsDBNull(RsTemp.Fields("PAYMENTTYPE").Value), "", RsTemp.Fields("PAYMENTTYPE").Value))) = "CHEQUE" Then
                        If Val(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)) > 0 Then
                            Print(1, TAB(0), Trim(Trim(IIf(IsDBNull(RsTemp.Fields("BANKNO").Value), "", RsTemp.Fields("BANKNO").Value))))
                            mEmpName = Left(Trim(IIf(IsDBNull(RsTemp.Fields("EMPNAME").Value), "", RsTemp.Fields("EMPNAME").Value)), 60)
                            Print(1, TAB(17), mEmpName)
                            mAmount = VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value), "0.00")
                            mAmount = New String(" ", 18 - Len(mAmount)) & mAmount
                            Print(1, TAB(76), mAmount)
                            Print(1, TAB(94), pNarration) ''& UCase(plblYear);     '"BY SALARY OF "
                            PrintLine(1, TAB(124), "C")
                            mLineCount = mLineCount + 1
                            If mLineCount = 60 Then
                                mLineCount = 1
                            End If
                        End If
                    End If
                End If
                RsTemp.MoveNext()
            Loop
            FileClose(1)
        End If
        Shell("ATTRIB +R -A " & pFileName)
        Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
        CreateTxtFileForBank = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        CreateTxtFileForBank = False
        ''Resume
        FileClose(1)
    End Function
    Public Function FillDummyTableForBank(ByRef pSprdAttn As AxFPSpreadADO.AxfpSpread, ByRef pCardCol As Integer, ByRef pNameCol As Integer, ByRef pPaymentTypeCol As Integer, ByRef pBankNoCol As Integer, ByRef pNetAmountCol As Integer, ByRef pBankName As String, ByRef pNarration As String, ByRef pMaxRow As Integer) As Boolean
        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr
        Dim RSPrintDummy As ADODB.Recordset
        Dim RowNum As Integer
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mPaymentType As String
        Dim mBankNo As String
        Dim mNetAmount As String
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        '    lblNetPay.Caption = 0
        For RowNum = 1 To pMaxRow
            pSprdAttn.Row = RowNum
            pSprdAttn.Col = pCardCol
            mEmpCode = Trim(pSprdAttn.Text)
            pSprdAttn.Col = pNameCol
            mEmpName = Trim(pSprdAttn.Text)
            pSprdAttn.Col = pPaymentTypeCol
            mPaymentType = Trim(pSprdAttn.Text)
            pSprdAttn.Col = pBankNoCol
            mBankNo = Trim(pSprdAttn.Text)
            pSprdAttn.Col = pNetAmountCol
            mNetAmount = Trim(pSprdAttn.Text)
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW,FIELD1, " & vbCrLf & " FIELD2, FIELD3, FIELD4, FIELD5, FIELD6, FIELD7) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "','" & mPaymentType & "', " & vbCrLf & " '" & mBankNo & "','" & mNetAmount & "','" & MainClass.AllowSingleQuote(pBankName) & "','" & pNarration & "') "
            PubDBCn.Execute(SqlStr)
        Next
        PubDBCn.CommitTrans()
        FillDummyTableForBank = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        FillDummyTableForBank = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function CreateXLSFileForBank(ByRef RsTemp As ADODB.Recordset, ByRef pBankName As String, ByRef pNarration As String) As Boolean
        On Error GoTo ErrPart
        'Dim mLineCount As Long
        'Dim pPageNo As Long
        Dim cntRow As Double
        'Dim pFileName As String
        Dim mAmount As String
        Dim mEmpName As String
        Dim mCardNo As String
        Dim mCheckBankName As String
        Dim mBankAccountNo As String
        Dim mHeadingline As Integer
        Dim mSNo As Integer
        Dim mNetAmount As Double
        Dim exlobj As Object
        mHeadingline = 1
        mNetAmount = 0
        mSNo = 0
        exlobj = CreateObject("excel.application")
        exlobj.Visible = True
        exlobj.Workbooks.Add()
        With exlobj.ActiveSheet
            .Cells(mHeadingline, 3).Value = "(This has to be submitted in soft copy and  hard copy)"
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 6).Value = "Format for Salary / Reimbursement Up-load"
            .Cells(mHeadingline, 6).Font.Name = "Verdana"
            .Cells(mHeadingline, 6).Font.bold = True
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 5).Value = "COMPANY NAME : "
            .Cells(mHeadingline, 5).Font.Name = "Verdana"
            .Cells(mHeadingline, 5).Font.bold = True
            .Cells(mHeadingline, 6).Value = RsCompany.Fields("Company_Name").Value
            .Cells(mHeadingline, 6).Font.Name = "Verdana"
            .Cells(mHeadingline, 6).Font.bold = True
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 5).Value = "DATE : "
            .Cells(mHeadingline, 5).Font.Name = "Verdana"
            .Cells(mHeadingline, 5).Font.bold = True
            .Cells(mHeadingline, 6).Value = VB6.Format(PubCurrDate, "DD-MMM-YYYY")
            .Cells(mHeadingline, 6).Font.Name = "Verdana"
            .Cells(mHeadingline, 6).Font.bold = True
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 2).Value = "S. No."
            .Cells(mHeadingline, 2).Font.Name = "Verdana"
            .Cells(mHeadingline, 2).Font.bold = True
            .Cells(mHeadingline, 3).Value = "Employee Name"
            .Cells(mHeadingline, 3).Font.Name = "Verdana"
            .Cells(mHeadingline, 3).Font.bold = True
            .Cells(mHeadingline, 4).Value = "Employee Account No"
            .Cells(mHeadingline, 4).Font.Name = "Verdana"
            .Cells(mHeadingline, 4).Font.bold = True
            .Cells(mHeadingline, 5).Value = "Amount"
            .Cells(mHeadingline, 5).Font.Name = "Verdana"
            .Cells(mHeadingline, 5).Font.bold = True
            .Cells(mHeadingline, 6).Value = "Narration"
            .Cells(mHeadingline, 6).Font.Name = "Verdana"
            .Cells(mHeadingline, 6).Font.bold = True
            Do While RsTemp.EOF = False ''pSprdAttn.MaxRows - 2
                mCardNo = Trim(IIf(IsDBNull(RsTemp.Fields("EMPCODE").Value), "", RsTemp.Fields("EMPCODE").Value))
                If MainClass.ValidateWithMasterTable(mCardNo, "EMP_CODE", "EMP_BANK_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCheckBankName = MasterNo
                Else
                    mCheckBankName = ""
                End If
                If Trim(UCase(pBankName)) = Trim(UCase(mCheckBankName)) Then
                    If UCase(Trim(IIf(IsDBNull(RsTemp.Fields("PAYMENTTYPE").Value), "", RsTemp.Fields("PAYMENTTYPE").Value))) = "CHEQUE" Then
                        If Val(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)) > 0 Then
                            mBankAccountNo = IIf(IsDBNull(RsTemp.Fields("BANKNO").Value), "", RsTemp.Fields("BANKNO").Value)
                            If InStr(1, mBankAccountNo, vbNewLine) > 0 Then
                                mBankAccountNo = Left(mBankAccountNo, InStr(1, mBankAccountNo, vbNewLine) - 1)
                            End If
                            mEmpName = Trim(IIf(IsDBNull(RsTemp.Fields("EMPNAME").Value), "", RsTemp.Fields("EMPNAME").Value)) ''Left(Trim(IIf(IsNull(RsTemp!EMPNAME), "", RsTemp!EMPNAME)), 40)
                            mAmount = CStr(Val(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)))
                            mSNo = mSNo + 1
                            mHeadingline = mHeadingline + 1
                            mNetAmount = mNetAmount + CDbl(mAmount)
                            .Cells(mHeadingline, 2).Value = mSNo
                            .Cells(mHeadingline, 3).Value = mEmpName
                            .Cells(mHeadingline, 4).Value = "'" & mBankAccountNo
                            .Cells(mHeadingline, 5).Value = VB6.Format(mAmount, "0.00")
                            .Cells(mHeadingline, 6).Value = pNarration
                        End If
                    End If
                End If
                RsTemp.MoveNext()
            Loop
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 6).Value = "Total Number of Debit Entries"
            .Cells(mHeadingline, 6).Font.Name = "Verdana"
            .Cells(mHeadingline, 6).Font.bold = True
            .Cells(mHeadingline, 7).Value = mSNo
            .Cells(mHeadingline, 7).Font.Name = "Verdana"
            .Cells(mHeadingline, 7).Font.bold = True
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 6).Value = "Total Amount of Debit"
            .Cells(mHeadingline, 6).Font.Name = "Verdana"
            .Cells(mHeadingline, 6).Font.bold = True
            .Cells(mHeadingline, 7).Value = mNetAmount
            .Cells(mHeadingline, 7).Font.Name = "Verdana"
            .Cells(mHeadingline, 7).Font.bold = True
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 6).Value = "Total Number of Credit Entries"
            .Cells(mHeadingline, 6).Font.Name = "Verdana"
            .Cells(mHeadingline, 6).Font.bold = True
            .Cells(mHeadingline, 7).Value = 0
            .Cells(mHeadingline, 7).Font.Name = "Verdana"
            .Cells(mHeadingline, 7).Font.bold = True
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 6).Value = "Total Amount of Credit"
            .Cells(mHeadingline, 6).Font.Name = "Verdana"
            .Cells(mHeadingline, 6).Font.bold = True
            .Cells(mHeadingline, 7).Value = 0
            .Cells(mHeadingline, 7).Font.Name = "Verdana"
            .Cells(mHeadingline, 7).Font.bold = True
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 6).Value = "Authorised Representative(s)"
            .Cells(mHeadingline, 6).Font.Name = "Verdana"
            .Cells(mHeadingline, 6).Font.bold = True
        End With
        CreateXLSFileForBank = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        CreateXLSFileForBank = False
        ''Resume
        '    Close #1
    End Function
    Public Function CreateXLSFileForSabo(ByRef RsTemp As ADODB.Recordset, ByRef pBankName As String, ByRef pNarration As String) As Boolean
        On Error GoTo ErrPart
        'Dim mLineCount As Long
        'Dim pPageNo As Long
        Dim cntRow As Double
        'Dim pFileName As String
        Dim mAmount As String
        Dim mEmpName As String
        Dim mCardNo As String
        Dim mCheckBankName As String
        Dim mBankAccountNo As String
        Dim mHeadingline As Integer
        Dim mSNo As Integer
        Dim mNetAmount As Double
        Dim exlobj As Object
        Dim mColHeader As String
        mHeadingline = 1
        mNetAmount = 0
        mSNo = 0
        exlobj = CreateObject("excel.application")
        exlobj.Visible = True
        exlobj.Workbooks.Add()
        With exlobj.ActiveSheet
            .Cells(mHeadingline, 1).Value = RsCompany.Fields("Company_Name").Value
            mColHeader = "A" & mHeadingline & ":" & "E" & mHeadingline
            .Cells.Range("" & mColHeader & "").Merge()
            .Cells.Range("" & mColHeader & "").HorizontalAlignment = 3
            .Cells.Range("" & mColHeader & "").VerticalAlignment = 2
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 1).Value = RsCompany.Fields("COMPANY_ADDR").Value
            mColHeader = "A" & mHeadingline & ":" & "E" & mHeadingline
            .Cells.Range("" & mColHeader & "").Merge()
            .Cells.Range("" & mColHeader & "").HorizontalAlignment = 3
            .Cells.Range("" & mColHeader & "").VerticalAlignment = 2
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 1).Value = "Bank Transfer Sheet"
            mColHeader = "A" & mHeadingline & ":" & "E" & mHeadingline
            .Cells.Range("" & mColHeader & "").Merge()
            .Cells.Range("" & mColHeader & "").HorizontalAlignment = 3
            .Cells.Range("" & mColHeader & "").VerticalAlignment = 2
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 1).Value = pNarration
            mColHeader = "A" & mHeadingline & ":" & "E" & mHeadingline
            .Cells.Range("" & mColHeader & "").Merge()
            .Cells.Range("" & mColHeader & "").HorizontalAlignment = 3
            .Cells.Range("" & mColHeader & "").VerticalAlignment = 2
            'Salary For the Month : May, 2017
            '        .Cells(mHeadingline, 1).Font.Name = "Verdana"
            '        .Cells(mHeadingline, 1).Font.bold = True:
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 1).Value = "S. No."
            '        .Cells(mHeadingline, 1).Font.Name = "Verdana"
            .Cells(mHeadingline, 1).Font.bold = True
            .Cells(mHeadingline, 2).Value = "Employee No"
            '        .Cells(mHeadingline, 2).Font.Name = "Verdana"
            .Cells(mHeadingline, 2).Font.bold = True
            .Cells(mHeadingline, 3).Value = "Employee A/c No"
            '        .Cells(mHeadingline, 3).Font.Name = "Verdana"
            .Cells(mHeadingline, 3).Font.bold = True
            .Cells(mHeadingline, 4).Value = "Employee Name"
            '        .Cells(mHeadingline, 4).Font.Name = "Verdana"
            .Cells(mHeadingline, 4).Font.bold = True
            .Cells(mHeadingline, 5).Value = "Amount"
            '        .Cells(mHeadingline, 5).Font.Name = "Verdana"
            .Cells(mHeadingline, 5).Font.bold = True
            Do While RsTemp.EOF = False ''pSprdAttn.MaxRows - 2
                mCardNo = Trim(IIf(IsDBNull(RsTemp.Fields("EMPCODE").Value), "", RsTemp.Fields("EMPCODE").Value))
                If MainClass.ValidateWithMasterTable(mCardNo, "EMP_CODE", "EMP_BANK_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCheckBankName = MasterNo
                Else
                    mCheckBankName = ""
                End If
                If Trim(UCase(pBankName)) = Trim(UCase(mCheckBankName)) Then
                    If UCase(Trim(IIf(IsDBNull(RsTemp.Fields("PAYMENTTYPE").Value), "", RsTemp.Fields("PAYMENTTYPE").Value))) = "CHEQUE" Then
                        If Val(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)) > 0 Then
                            mBankAccountNo = IIf(IsDBNull(RsTemp.Fields("BANKNO").Value), "", RsTemp.Fields("BANKNO").Value)
                            If InStr(1, mBankAccountNo, vbNewLine) > 0 Then
                                mBankAccountNo = Left(mBankAccountNo, InStr(1, mBankAccountNo, vbNewLine) - 1)
                            End If
                            mEmpName = Trim(IIf(IsDBNull(RsTemp.Fields("EMPNAME").Value), "", RsTemp.Fields("EMPNAME").Value)) ''Left(Trim(IIf(IsNull(RsTemp!EMPNAME), "", RsTemp!EMPNAME)), 40)
                            mAmount = CStr(Val(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)))
                            mSNo = mSNo + 1
                            mHeadingline = mHeadingline + 1
                            mNetAmount = mNetAmount + CDbl(mAmount)
                            .Cells(mHeadingline, 1).Value = mSNo
                            .Cells(mHeadingline, 2).Value = "'" & mCardNo
                            .Cells(mHeadingline, 3).Value = "'" & mBankAccountNo
                            .Cells(mHeadingline, 4).Value = mEmpName
                            .Cells(mHeadingline, 5).Value = VB6.Format(mAmount, "0.00")
                            .Columns("A").ColumnWidth = 10
                            .Columns("B").ColumnWidth = 15
                            .Columns("C").ColumnWidth = 20
                            .Columns("D").ColumnWidth = 40
                            .Columns("E").ColumnWidth = 10
                        End If
                    End If
                End If
                RsTemp.MoveNext()
            Loop
            mHeadingline = mHeadingline + 1
            .Cells(mHeadingline, 4).Value = "Total"
            .Cells(mHeadingline, 4).Font.Name = "Verdana"
            .Cells(mHeadingline, 4).Font.bold = True
            .Cells(mHeadingline, 5).Value = mNetAmount
            .Cells(mHeadingline, 5).Font.Name = "Verdana"
            .Cells(mHeadingline, 5).Font.bold = True
            With exlobj.ActiveSheet
                mColHeader = "A1" & ":" & "E" & mHeadingline ''- 1
                .Cells.Range("" & mColHeader & "").Borders(1).LineStyle = 1
                .Cells.Range("" & mColHeader & "").Borders(3).LineStyle = 1
                .Cells.Range("" & mColHeader & "").BorderAround(LineStyle:=1, Weight:=3)
                '        .LineStyle = 1
                '        .Weight = 1
                '        .ColorIndex = 3
            End With
        End With
        CreateXLSFileForSabo = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        CreateXLSFileForSabo = False
        ''Resume
        '    Close #1
    End Function
    Public Function FillBankSheetIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByRef pColCard As Integer, ByRef pColName As Integer, ByRef pColDays As Integer, ByRef pColPaymentType As Integer, ByRef pColAmount As Integer, ByRef pColBankNo As Integer, ByRef mPaymentType As String, ByRef pBankName As String, ByRef pColBankIFSC As Integer) As Boolean
        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr
        Dim RSPrintDummy As ADODB.Recordset
        Dim RowNum As Integer
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mWDays As String
        Dim mNetPay As String
        Dim mBankAcct As String
        Dim mCheckBankName As String
        Dim mCategory As String
        Dim pBankIFSC As String
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        PubDBCn.BeginTrans()
        '    lblNetPay.Caption = 0
        For RowNum = prmStartGridRow To prmEndGridRow
            GridName.Row = RowNum
            GridName.Col = pColPaymentType
            If UCase(GridName.Text) = UCase(mPaymentType) Then
                GridName.Col = pColCard
                mEmpCode = MainClass.AllowSingleQuote(GridName.Text)
                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_BANK_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCheckBankName = MasterNo
                Else
                    mCheckBankName = ""
                End If
                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCategory = MasterNo
                Else
                    mCategory = ""
                End If
                If Trim(UCase(pBankName)) = Trim(UCase(mCheckBankName)) Or pBankName = "" Then
                    GridName.Col = pColName
                    mEmpName = MainClass.AllowSingleQuote(GridName.Text)
                    If pColDays = 0 Then
                        mWDays = ""
                    Else
                        GridName.Col = pColDays
                        mWDays = GridName.Text
                    End If
                    GridName.Col = pColAmount 'GridName.MaxCols
                    mNetPay = GridName.Text
                    '                lblNetPay.Caption = Val(lblNetPay.Caption) + mNetPay
                    GridName.Col = pColBankNo
                    '                If lblIsArrear.Caption = "N" Then
                    mBankAcct = MainClass.AllowSingleQuote(Trim(GridName.Text))
                    '                Else
                    '                    mBankAcct = MainClass.AllowSingleQuote(Left(Trim(GridName.Text), 15))
                    '                End If
                    GridName.Col = pColBankIFSC
                    pBankIFSC = MainClass.AllowSingleQuote(Trim(GridName.Text))
                    If Val(mNetPay) > 0 Then
                        SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW,FIELD1, " & vbCrLf & " FIELD2, FIELD3, FIELD4, FIELD5, FIELD6, FIELD7, FIELD8) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "','" & mWDays & "', " & vbCrLf & " '" & mNetPay & "','" & mBankAcct & "','" & MainClass.AllowSingleQuote(pBankName) & "','" & mCategory & "','" & MainClass.AllowSingleQuote(pBankIFSC) & "') "
                        PubDBCn.Execute(SqlStr)
                    End If
                End If
            End If
        Next
        PubDBCn.CommitTrans()
        FillBankSheetIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        FillBankSheetIntoPrintDummy = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function FillInaamIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByRef pColCard As Integer, ByRef pColName As Integer, ByRef pColInaam As Integer, ByRef pColAmount As Integer) As Boolean
        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr
        Dim RSPrintDummy As ADODB.Recordset
        Dim RowNum As Integer
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mScale As Double
        Dim mInaamAmount As Double
        Dim mNetPay As Double
        Dim mEffScale As Double
        Dim mQualityScale As Double
        Dim mQuantityScale As Double
        Dim mAppreScale As Double
        Dim mRelScale As Double
        Dim mGLScale As Double
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        PubDBCn.BeginTrans()
        '    lblNetPay.Caption = 0
        For RowNum = prmStartGridRow To prmEndGridRow
            GridName.Row = RowNum
            GridName.Col = pColCard
            mEmpCode = MainClass.AllowSingleQuote(GridName.Text)
            GridName.Col = pColName
            mEmpName = MainClass.AllowSingleQuote(GridName.Text)
            GridName.Col = pColInaam
            mInaamAmount = Val(GridName.Text)
            GridName.Col = pColAmount 'GridName.MaxCols
            mNetPay = Val(GridName.Text)
            mScale = GetInaamScale(mInaamAmount)
            mEffScale = System.Math.Round(mScale * 20 / 100, 0)
            mQualityScale = System.Math.Round(mScale * 20 / 100, 0)
            mQuantityScale = System.Math.Round(mScale * 20 / 100, 0)
            mAppreScale = System.Math.Round(mScale * 20 / 100, 0)
            mRelScale = System.Math.Round(mScale * 10 / 100, 0)
            mGLScale = mScale - (mEffScale + mQualityScale + mQuantityScale + mAppreScale + mRelScale)
            If Val(CStr(mInaamAmount)) > 0 Then
                SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW,FIELD1, " & vbCrLf & " FIELD2, FIELD3, FIELD4, FIELD5, FIELD6, FIELD7,FIELD8, FIELD9, FIELD10, FIELD11) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "','" & mInaamAmount & "', " & vbCrLf & " '" & mNetPay & "','" & mScale & "','" & mEffScale & "','" & mQualityScale & "'," & vbCrLf & " '" & mQuantityScale & "','" & mAppreScale & "','" & mRelScale & "','" & mGLScale & "'" & vbCrLf & " ) "
                PubDBCn.Execute(SqlStr)
            End If
        Next
        PubDBCn.CommitTrans()
        FillInaamIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        FillInaamIntoPrintDummy = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function GetEmployeePFContOn(ByRef pEmpCode As String, ByRef pSalDate As String, Optional ByRef mPFNo As String = "") As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        If pEmpCode = "" Then
            If MainClass.ValidateWithMasterTable(mPFNo, "EMP_PF_ACNO", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pEmpCode = Trim(MasterNo)
            Else
                GetEmployeePFContOn = "B"
                Exit Function
            End If
        End If
        SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf & " ADD_DEDUCT.ROUNDING AS ROUNDING,SALARYDEF.EMP_CONT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & pEmpCode & "'" & vbCrLf & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & pEmpCode & "'" & vbCrLf & " AND SALARY_APP_DATE <= TO_DATE('" & VB6.Format(pSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "
        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")" & vbCrLf & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(pSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " UNION " & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")" & vbCrLf & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(pSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetEmployeePFContOn = IIf(IsDBNull(RsTemp.Fields("EMP_CONT").Value), "B", RsTemp.Fields("EMP_CONT").Value)
        Else
            GetEmployeePFContOn = "B"
        End If
        Exit Function
ErrPart:
        ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
    End Function
    Public Function GetShiftTimeNew(ByRef mCode As String, ByRef mDate As String, ByRef mMarginsMinute As Double, ByRef mIO As String, ByRef mIsRoundClock As String, ByRef pEmpType As String) As String
        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFieldName As String
        Dim SqlStr As String = ""
        Dim mTableName As String
        Dim xShiftTime As String
        Dim xIsRoundClock As String
        If mIO = "I" Then
            mFieldName = "IN_TIME"
        ElseIf mIO = "B" Then
            mFieldName = "B_END_TIME"
        Else
            mFieldName = "OUT_TIME"
        End If
        If pEmpType = "E" Then
            mTableName = "PAY_SHIFT_TRN"
        Else
            mTableName = "PAY_CONT_SHIFT_MST"
        End If
        If mIO = "O" Then
            xShiftTime = GetShiftTimeNew(mCode, mDate, mMarginsMinute, "I", "N", pEmpType)
            xShiftTime = VB6.Format(xShiftTime, "HH:MM")
            If xShiftTime >= "16:00" Then
                xIsRoundClock = "Y"
            Else
                xIsRoundClock = "N"
            End If
        End If
        SqlStr = " SELECT " & mFieldName & " AS SHIFT_TIME " & vbCrLf & " FROM " & mTableName & " TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'" & vbCrLf & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            GetShiftTimeNew = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SHIFT_TIME").Value), "", RsTemp.Fields("SHIFT_TIME").Value), "HH:MM")
            If mIO = "I" Then
                GetShiftTimeNew = VB6.Format(DateSerial(Year(CDate(mDate)), Month(CDate(mDate)), VB.Day(CDate(mDate))) & " " & TimeSerial(Hour(CDate(GetShiftTimeNew)), Minute(CDate(GetShiftTimeNew)), 0), "DD/MM/YYYY HH:MM")
            ElseIf mIO = "B" Then
                If xIsRoundClock = "Y" Then
                    GetShiftTimeNew = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate)))) & " " & TimeSerial(Hour(CDate(GetShiftTimeNew)), Minute(CDate(GetShiftTimeNew)), 0), "DD/MM/YYYY HH:MM")
                Else
                    GetShiftTimeNew = VB6.Format(DateSerial(Year(CDate(mDate)), Month(CDate(mDate)), VB.Day(CDate(mDate))) & " " & TimeSerial(Hour(CDate(GetShiftTimeNew)), Minute(CDate(GetShiftTimeNew)), 0), "DD/MM/YYYY HH:MM")
                End If
            Else
                If xIsRoundClock = "Y" Then
                    GetShiftTimeNew = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate)))) & " " & TimeSerial(Hour(CDate(GetShiftTimeNew)), Minute(CDate(GetShiftTimeNew)), 0), "DD/MM/YYYY HH:MM")
                Else
                    GetShiftTimeNew = VB6.Format(DateSerial(Year(CDate(mDate)), Month(CDate(mDate)), VB.Day(CDate(mDate))) & " " & TimeSerial(Hour(CDate(GetShiftTimeNew)), Minute(CDate(GetShiftTimeNew)), 0), "DD/MM/YYYY HH:MM")
                End If
            End If
            If mIO = "I" Then
                If mIO = "B" Then
                    GetShiftTimeNew = VB6.Format(DateSerial(Year(CDate(GetShiftTimeNew)), Month(CDate(GetShiftTimeNew)), VB.Day(CDate(GetShiftTimeNew))) & " " & TimeSerial(Hour(CDate(GetShiftTimeNew)), Minute(CDate(GetShiftTimeNew)), 0), "DD/MM/YYYY HH:MM")
                Else
                    GetShiftTimeNew = VB6.Format(DateSerial(Year(CDate(GetShiftTimeNew)), Month(CDate(GetShiftTimeNew)), VB.Day(CDate(GetShiftTimeNew))) & " " & TimeSerial(Hour(CDate(GetShiftTimeNew)), Minute(CDate(GetShiftTimeNew)) + mMarginsMinute, 0), "DD/MM/YYYY HH:MM")
                End If
            Else
                If mIO = "B" Then
                    GetShiftTimeNew = VB6.Format(DateSerial(Year(CDate(GetShiftTimeNew)), Month(CDate(GetShiftTimeNew)), VB.Day(CDate(GetShiftTimeNew))) & " " & TimeSerial(Hour(CDate(GetShiftTimeNew)), Minute(CDate(GetShiftTimeNew)) + mMarginsMinute, 0), "DD/MM/YYYY HH:MM")
                Else
                    GetShiftTimeNew = VB6.Format(DateSerial(Year(CDate(GetShiftTimeNew)), Month(CDate(GetShiftTimeNew)), VB.Day(CDate(GetShiftTimeNew))) & " " & TimeSerial(Hour(CDate(GetShiftTimeNew)), Minute(CDate(GetShiftTimeNew)) - mMarginsMinute, 0), "DD/MM/YYYY HH:MM")
                End If
            End If
        Else
            GetShiftTimeNew = "00:00"
        End If
        ''DateSerial(year(GetShiftTimeNew), month(GetShiftTimeNew), day(GetShiftTimeNew))
        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Public Function ValidateShiftEnter(ByRef mDate As String, ByRef pEmpType As String, ByRef mCategoryDesc As String, ByRef mDeptDesc As String, ByRef mShift As String, ByRef mEmpCode As String, ByRef xContractorCode As Integer) As Boolean
        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFieldName As String
        Dim SqlStr As String = ""
        Dim mTableName As String
        Dim mEmpTableName As String
        Dim pEmpCode As String
        Dim pDeptCode As String
        '
        '        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Then
        '            ValidateShiftEnter = True
        '            Exit Function
        '        End If
        If pEmpType = "E" Then
            mTableName = "PAY_SHIFT_TRN"
            mEmpTableName = "PAY_EMPLOYEE_MST"
        Else
            If PubSuperUser = "S" Or PubSuperUser = "A" Then
                ValidateShiftEnter = True
                Exit Function
            End If
            mTableName = "PAY_CONT_SHIFT_MST"
            mEmpTableName = "PAY_CONT_EMPLOYEE_MST"
        End If
        ValidateShiftEnter = False
        SqlStr = " SELECT EMP_CODE " & vbCrLf & " FROM " & mEmpTableName & " EMP " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DOJ<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND (EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "
        If pEmpType = "E" Then
            If mCategoryDesc <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CATG = '" & MainClass.AllowSingleQuote(Left(mCategoryDesc, 1)) & "'"
            End If
        End If
        If mDeptDesc <> "" Then
            If MainClass.ValidateWithMasterTable(mDeptDesc, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND EMP_DEPT_CODE = '" & MainClass.AllowSingleQuote(pDeptCode) & "'"
            End If
        End If
        If mEmpCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE = '" & MainClass.AllowSingleQuote(mEmpCode) & "'"
        End If
        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE NOT IN (" & vbCrLf & " SELECT DISTINCT EMP_CODE " & vbCrLf & " FROM " & mTableName & " TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If mEmpCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE = '" & MainClass.AllowSingleQuote(mEmpCode) & "'"
        End If
        If mShift <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND SHIFT_CODE = '" & MainClass.AllowSingleQuote(mShift) & "'"
        End If
        SqlStr = SqlStr & vbCrLf & ")"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                pEmpCode = IIf(pEmpCode = "", "", pEmpCode & ",") & IIf(IsDBNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
                RsTemp.MoveNext()
            Loop
            MsgInformation("Shift not Define for following Employee. Please Define it." & pEmpCode)
            ValidateShiftEnter = False
            Exit Function
        End If
        ValidateShiftEnter = True
        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Public Function CalcMachineData(ByRef mCode As String, ByRef mTotInTime As Double, ByRef mTotLateTime As Double, ByRef mTotOTTime As Double, ByRef mTotODTime As Double, ByRef mNoDataFound As Double, ByRef mMonthDate As String, ByRef pCheckWeeklyOffFromShift As String) As Boolean
        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAttnDate As String
        Dim mLastDay As Integer
        Dim mDays As Integer
        Dim mShiftInTime As String
        Dim mShiftOutTime As String
        Dim mInTime As String
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
        Dim mIsINOD As Boolean
        Dim mIsOUTOD As Boolean
        Dim mShortLeaveCount As Double


        mLateReliefHours = IIf(IsDBNull(RsCompany.Fields("SHORT_LEAVE").Value), 0, RsCompany.Fields("SHORT_LEAVE").Value) / 60
        mMarginsMinute = IIf(IsDBNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)
        mShortLeaveCount = 0
        mLastDay = MainClass.LastDay(Month(CDate(mMonthDate)), Year(CDate(mMonthDate)))
        mTotODTime = 0
        xDesgCode = GetEmpCurrentDesg(mCode, mMonthDate)
        If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCat = MasterNo
        End If
        For mDays = 1 To mLastDay
            mAttnDate = VB6.Format(mDays & "/" & VB6.Format(mMonthDate, "MM/YYYY"), "DD/MM/YYYY")
            mIsINOD = False
            mIsOUTOD = False
            mInTime = GetTime(mCode, mAttnDate, "I", mIsINOD)
            mOutTime = GetTime(mCode, mAttnDate, "O", mIsOUTOD)
            If mIsINOD = True And mIsOUTOD = True Then
                mTotODTime = mTotODTime + 1
            End If
            mShiftInTime = GetShiftTime(mCode, mAttnDate, 0, "I", "E")
            mShiftOutTime = GetShiftTime(mCode, mAttnDate, 0, "O", "E")



            If GetTotatHours(CDate(mInTime), CDate(mOutTime), CDate(mInTime), CDate(mOutTime), mTotDateTime, mWorkHours, mOTHours, mSundayOTHours, CDate(mShiftInTime), CDate(mShiftOutTime), mAttnDate, mCode) = False Then GoTo refreshErrPart
            mShiftInTime = GetShiftTime(mCode, mAttnDate, mMarginsMinute, "I", "E")
            mShiftOutTime = GetShiftTime(mCode, mAttnDate, mMarginsMinute, "O", "E")
            mWHours = ((Hour(mWorkHours) * 60) + Minute(mWorkHours) + mMarginsMinute) / 60
            If mWorkHours <> CDate("00:00") Then
                If GetIsHolidays(mAttnDate, mHType, mCode, "", pCheckWeeklyOffFromShift) = False Then
                    If CDate(mInTime) <= CDate(mShiftInTime) And CDate(mOutTime) >= CDate(mShiftOutTime) Then
                        mTotInTime = mTotInTime + 1
                    ElseIf CDate(mInTime) <= CDate(mShiftInTime) And CDate(mOutTime) < CDate(mShiftOutTime) Then
                        If mWHours >= 6 Then
                            mTotInTime = mTotInTime + 1
                            mShortLeaveCount = mShortLeaveCount + 1
                            mTotLateTime = mTotLateTime + 1
                        ElseIf mWHours >= 4 Then
                            mTotInTime = mTotInTime + 0.5
                            mNoDataFound = mNoDataFound + 0.5
                        Else
                            mNoDataFound = mNoDataFound + 1
                        End If
                    ElseIf CDate(mInTime) > CDate(mShiftInTime) And CDate(mOutTime) >= CDate(mShiftOutTime) Then
                        If mInTime >= VB6.Format(TimeSerial(Hour(CDate(mShiftInTime)) + 2, 0, 0), "HH:MM") Then
                            mTotInTime = mTotInTime + 0.5
                            mNoDataFound = mNoDataFound + 0.5
                        Else
                            If mWHours >= 6 Then
                                mTotInTime = mTotInTime + 1
                                mShortLeaveCount = mShortLeaveCount + 1
                                mTotLateTime = mTotLateTime + 1
                            ElseIf mWHours >= 4 Then
                                mTotInTime = mTotInTime + 0.5
                                mNoDataFound = mNoDataFound + 0.5
                            Else
                                mNoDataFound = mNoDataFound + 1
                            End If
                        End If
                    ElseIf CDate(mInTime) > CDate(mShiftInTime) And CDate(mOutTime) < CDate(mShiftOutTime) Then
                        If mWHours >= 6 Then
                            mTotInTime = mTotInTime + 1
                            mShortLeaveCount = mShortLeaveCount + 1
                            mTotLateTime = mTotLateTime + 1
                        ElseIf mWHours >= 4 Then
                            mTotInTime = mTotInTime + 0.5
                            mNoDataFound = mNoDataFound + 0.5
                        Else
                            mNoDataFound = mNoDataFound + 1
                        End If
                    Else
                        mNoDataFound = mNoDataFound + 1
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
            Else
                If GetIsHolidays(mAttnDate, mHType, mCode, "", pCheckWeeklyOffFromShift) = False Then
                    mNoDataFound = mNoDataFound + 1
                End If
            End If
        Next
        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Public Function GetTime(ByRef mCode As String, ByRef mDate As String, ByRef mIO As Object, ByRef mIsOD As Boolean) As String
        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mODTime As String
        Dim mGateTime As String
        Dim SqlStr As String = ""
        Dim mFieldName As String
        If mIO = "I" Then
            mFieldName = "TIME_FROM"
        Else
            mFieldName = "TIME_TO"
        End If
        SqlStr = " SELECT " & mFieldName & " AS OD_TIME" & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            mODTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OD_TIME").Value), "", RsTemp.Fields("OD_TIME").Value), "HH:MM")
        Else
            mODTime = "00:00"
        End If
        If mIO = "I" Then
            mFieldName = "IN_TIME"
        Else
            mFieldName = "OUT_TIME"
        End If
        SqlStr = " SELECT " & mFieldName & " AS GATE_TIME " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN ATTN " & vbCrLf & " WHERE ATTN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ATTN.EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            mGateTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("GATE_TIME").Value), "", RsTemp.Fields("GATE_TIME").Value), "HH:MM")
        Else
            mGateTime = "00:00"
        End If
        If mIO = "I" Then
            If mODTime = "00:00" Then
                GetTime = mGateTime
                mIsOD = False
            Else
                If CDate(mODTime) < CDate(mGateTime) Then
                    GetTime = mODTime
                    mIsOD = IIf(mODTime = "00:00", False, True)
                Else
                    GetTime = mGateTime
                    mIsOD = False
                End If
            End If
        Else
            If mGateTime = "00:00" Then
                GetTime = mODTime
                mIsOD = IIf(mODTime = "00:00", False, True)
            Else
                If CDate(mODTime) > CDate(mGateTime) Then
                    GetTime = mODTime
                    mIsOD = IIf(mODTime = "00:00", False, True)
                Else
                    GetTime = mGateTime
                    mIsOD = False
                End If
            End If
        End If
        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Public Function GetDesgCode(ByVal mCompanyCode As Integer, ByVal mCode As String, ByVal pWEFDate As String) As String
        On Error GoTo ErrGetLTAAmount
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        SqlStr = " SELECT EMP_DESG_CODE " & vbCrLf & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf & " WHERE A.COMPANY_CODE = " & mCompanyCode & "" & vbCrLf & " AND A.COMPANY_CODE = B.COMPANY_CODE" & vbCrLf & " AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf & " AND A.EMP_CODE = '" & mCode & "'"
        '' AND B.NAME='" & MainClass.AllowSingleQuote(mSalHeadName) & "'
        SqlStr = SqlStr & vbCrLf & " AND B.ISSALPART='N'"
        ''AND B.ADDDEDUCT=" & pADDDeduct & "
        SqlStr = SqlStr & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) " & vbCrLf & " FROM PAY_SALARYDEF_MST" & vbCrLf & " WHERE COMPANY_CODE = " & mCompanyCode & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<=TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            GetDesgCode = IIf(IsDBNull(RsTemp.Fields("EMP_DESG_CODE").Value), "", RsTemp.Fields("EMP_DESG_CODE").Value)
        Else
            GetDesgCode = ""
        End If
        Exit Function
ErrGetLTAAmount:
        GetDesgCode = ""
    End Function
    'Public Function SendMailProcess(pFrom As String, pRecipient As String, pCcRecipient As String, pBccRecipient As String, pUserName As String, pUserPassword As String, mAttachmentFile As String, mSubject As String, mBodyText As String) As Boolean
    '
    ''Private Sub SendMail(strServer$, strFrom$, strTo$, strSubject$, strBodyText$)
    'On Error GoTo SendMailErr
    'Dim x%, y
    'Dim SMTP As Object
    'Dim Msg As String
    'Dim strToArray() As String
    'Dim strCCArray() As String
    'Dim strBCCArray() As String
    '
    '    Set SMTP = CreateObject("EasyMail.SMTP.5")
    '    SMTP.LicenseKey = "brain/S1cI500R1AX50C0R0200"
    '
    '    SMTP.MailServer = strServerSmtp
    '    SMTP.FromAddr = pFrom
    ''    SMTP.AddRecipient "", pRecipient, 1
    '    strToArray = Split(pRecipient, ";")
    '    strCCArray = Split(pCcRecipient, ";")
    '    strBCCArray = Split(pBccRecipient, ";")
    '
    '    For y = 0 To UBound(strToArray)
    '        If Trim(pRecipient) <> "" Then
    '            SMTP.AddRecipient strToArray(y), strToArray(y), 1
    '        End If
    '    Next y
    '    For y = 0 To UBound(strCCArray)
    '        If Trim(pCcRecipient) <> "" Then
    '            SMTP.AddRecipient strCCArray(y), strCCArray(y), 2
    '        End If
    '    Next y
    '    For y = 0 To UBound(strBCCArray)
    '        If Trim(pBccRecipient) <> "" Then
    '            SMTP.AddRecipient strBCCArray(y), strBCCArray(y), 3
    '        End If
    '    Next y
    '
    '
    '    SMTP.Subject = mSubject
    '    SMTP.BodyText = mBodyText
    '    outSourec = mAttachmentFile
    '    If outSourec <> "" Then
    '        y = SMTP.AddAttachment(outSourec, 0)
    '    End If
    ''    SMTP.Html = True
    '    SMTP.BodyFormat = 1
    '
    '   'Always set AutoWrap to zero for HTML messages
    '   SMTP.AutoWrap = 0
    '
    '
    '    SMTP.BodyEncoding = 2
    '    SMTP.TimeOut = 3600
    '    x% = SMTP.Send
    '    If x% = 0 Then
    '       Msg = "Message sent successfully."
    '    Else
    '       Msg = "There was an error sending your message.  Error: "
    '       GoTo SendMailErr
    '    End If
    '    If y = 0 Then
    '    Else
    '        Msg = "Error with attachment. Error: "
    '        GoTo SendMailErr
    '    End If
    '
    '    Set SMTP = Nothing
    '    outSourec = ""
    '    SendMailProcess = True
    'Exit Function
    'SendMailErr:
    '    MsgBox Msg & CStr(x%) & " " & "" & GetErrorMSG(Int(x%)), vbCritical
    '    ErrorMsg err.Description, err.Number
    '    SendMailProcess = False
    'End Function
    Public Function CheckSalaryMade(ByRef xEmpCode As String, ByRef xSalDate As String) As Boolean
        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckDate As String
        Dim SqlStr As String = ""
        CheckSalaryMade = False

        If Trim(xSalDate) = "" Then
            SqlStr = " SELECT * FROM PAY_SAL_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            If xEmpCode <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(xEmpCode) & "'"
            End If

        Else
            mCheckDate = MainClass.LastDay(Month(CDate(xSalDate)), Year(CDate(xSalDate))) & "/" & VB6.Format(xSalDate, "MM/YYYY")
            SqlStr = " SELECT * FROM PAY_SAL_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IsArrear<>'F'"

            If xEmpCode <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(xEmpCode) & "'"
            End If

        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            CheckSalaryMade = True
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Function GetPensionFund(ByRef pEmpCode As String, ByRef pSalDate As Object) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        SqlStr = " SELECT SUM(PENSIONFUND) AS PENSIONFUND" & vbCrLf & " FROM PAY_PFESI_TRN " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND SAL_DATE=TO_DATE('" & VB6.Format(pSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISARREAR='N'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            GetPensionFund = IIf(IsDBNull(RsTemp.Fields("PENSIONFUND").Value), 0, RsTemp.Fields("PENSIONFUND").Value)
        Else
            GetPensionFund = 0
        End If
        Exit Function
ErrPart:
        ''Resume
        GetPensionFund = 0
    End Function
    Public Function mMark(ByRef mCount As Short) As String
        If mCount = ABSENT Then
            mMark = "A"
        ElseIf mCount = CASUAL Then
            mMark = "C"
        ElseIf mCount = EARN Then
            mMark = "E"
        ElseIf mCount = SICK Then
            mMark = "S"
        ElseIf mCount = MATERNITY Then
            mMark = "M"
        ElseIf mCount = WOPAY Then
            mMark = "W"
        ElseIf mCount = CPLEARN Then
            mMark = "P"
        ElseIf mCount = CPLAVAIL Then
            mMark = "L"
        ElseIf mCount = SUNDAY Then
            mMark = "U"
        ElseIf mCount = HOLIDAY Then
            mMark = "H"
        ElseIf mCount = WFH Then
            mMark = "F"
        ElseIf mCount = -1 Or mCount = PRESENT Then
            mMark = "P"
        End If
    End Function
    Public Sub Report8Window(ByRef Rept1 As CRAXDRT.Report, Optional ByRef mTitle As String = "") '' CrystalReport
        '    Rept1.WindowShowRefreshBtn = True
        '    Rept1.WindowShowPrintBtn = True
        '    Rept1.WindowTitle = mTitle
        '    Rept1.ProgressDialog = True
        '    Rept1.WindowMaxButton = True
        '    Rept1.WindowMinButton = True
        '    Rept1.WindowShowGroupTree = True
        '    Rept1.WindowShowNavigationCtls = True
        '    Rept1.WindowAllowDrillDown = True
        '    Rept1.WindowShowPrintSetupBtn = True
        '    Rept1.WindowShowProgressCtls = True
        '    Rept1.WindowShowSearchBtn = True
        '    Rept1.WindowShowZoomCtl = True
        '    Rept1.WindowState = crptMaximized
        '    Rept1.WindowBorderStyle = crptSizable
    End Sub
    Public Function GetEmpBonusAmount(ByRef mCode As String, ByRef mFromDate As String, ByRef mToDate As String, Optional ByRef pDOJ As String = "", Optional ByRef pDOL As String = "") As Double
        On Error GoTo ErrCalcBonus
        '        Dim mBonusPayableAmount As Double
        '        Dim mBonusPer As Double
        '        Dim RsSal As ADODB.Recordset
        '        Dim RsTemp As ADODB.Recordset = Nothing
        '        Dim RsEmpTemp As ADODB.Recordset
        '        Dim mToEmpCompany As Integer
        '        Dim mToEmpCode As String
        '        Dim SqlStr As String=""=""
        '        Dim CntMonth As Integer
        '        Dim CntMonthEndDate As Date
        '        Dim mWDays As Double
        '        Dim mLastDay As Double
        '        Dim CntMonthStartDate As String
        '        Dim mFromMonth As Integer
        '        Dim mToMonth As Integer
        '        Dim mToCalcDate As String
        '        Dim pEmpFixBonusAmt As Double
        '        GetEmpBonusAmount = 0
        '        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
        '            mBonusPer = CDbl("0.01")
        '        End If
        '        mToCalcDate = MainClass.LastDay(Month(CDate(mToDate)), Year(CDate(mToDate))) & "/" & VB6.Format(mToDate, "MM/YYYY")
        '        For CntMonth = CInt(mFromDate) To CInt(mToCalcDate)
        '            CntMonthEndDate = CDate(MainClass.LastDay(Month(CntMonth), Year(CntMonth)) & "/" & VB6.Format(CntMonth, "MM/YYYY"))
        '            CntMonthStartDate = "01/" & VB6.Format(CntMonth, "MM/YYYY")
        '            If RsCompany.Fields("BONUS_TYPE").Value = "B" Then
        '                mBonusPer = GetBonusPer(RsCompany.Fields("COMPANY_CODE").Value, mCode, VB6.Format(CntMonthEndDate, "DD/MM/YYYY"), pEmpFixBonusAmt)
        '                If mBonusPer = 0 Then
        '                    If pEmpFixBonusAmt <> 0 Then
        '                        mWDays = CalcAttn(mCode, pDOJ, pDOL, CStr(CDate(CntMonthEndDate)))
        '                        GetEmpBonusAmount = GetEmpBonusAmount + (pEmpFixBonusAmt * mWDays / MainClass.LastDay(Month(CDate(CntMonthStartDate)), Year(CDate(CntMonthStartDate))))
        '                        GoTo NextRow
        '                    Else
        '                        GoTo NextRow
        '                    End If
        '                End If
        '                SqlStr = " SELECT DISTINCT PAYABLESALARY,SAL_DATE, ISARREAR " & vbCrLf & " FROM PAY_SAL_TRN SALTRN" & vbCrLf & " WHERE SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(CntMonthEndDate, "YYYYMM") & "' AND ISARREAR<>'F'"
        '                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)
        '                If RsSal.EOF = False Then
        '                    Do While Not RsSal.EOF
        '                        If RsSal.Fields("IsArrear").Value = "V" Then
        '                            If GetBonusPayableVoucherPayment(RsCompany.Fields("COMPANY_CODE").Value, mCode, VB6.Format(IIf(IsDBNull(RsSal.Fields("SAL_DATE").Value), "", RsSal.Fields("SAL_DATE").Value))) = True Then
        '                                GetEmpBonusAmount = GetEmpBonusAmount + (IIf(IsDBNull(RsSal.Fields("PAYABLESALARY").Value), 0, RsSal.Fields("PAYABLESALARY").Value) * mBonusPer / 100)
        '                            End If
        '                        Else
        '                            GetEmpBonusAmount = GetEmpBonusAmount + (IIf(IsDBNull(RsSal.Fields("PAYABLESALARY").Value), 0, RsSal.Fields("PAYABLESALARY").Value) * mBonusPer / 100)
        '                        End If
        '                        If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
        '                            SqlStr = " SELECT GETPayableBonusAmount (" & RsCompany.Fields("COMPANY_CODE").Value & ", '" & mCode & "','" & VB6.Format(IIf(IsDBNull(RsSal.Fields("SAL_DATE").Value), "", RsSal.Fields("SAL_DATE").Value), "DD-MMM-YYYY") & "', '" & RsSal.Fields("IsArrear").Value & "') AS SALPART FROM DUAL "
        '                            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        '                            If RsTemp.EOF = False Then
        '                                GetEmpBonusAmount = GetEmpBonusAmount + (IIf(IsDBNull(RsTemp.Fields("SALPART").Value), 0, RsTemp.Fields("SALPART").Value) * mBonusPer / 100)
        '                            End If
        '                        End If
        '                        RsSal.MoveNext()
        '                    Loop
        '                End If
        'NextRow:
        '                mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        '                mToEmpCode = mCode
        'SearchRow:
        '                SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        '                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpTemp, ADODB.LockTypeEnum.adLockOptimistic)
        '                If RsEmpTemp.EOF = False Then
        '                    mToEmpCompany = IIf(IsDBNull(RsEmpTemp.Fields("FROM_COMPANY_CODE").Value), "", RsEmpTemp.Fields("FROM_COMPANY_CODE").Value)
        '                    mToEmpCode = IIf(IsDBNull(RsEmpTemp.Fields("FROM_EMP_CODE").Value), "", RsEmpTemp.Fields("FROM_EMP_CODE").Value)
        '                    mBonusPer = GetBonusPer(mToEmpCompany, mToEmpCode, VB6.Format(CntMonthEndDate, "DD/MM/YYYY"), pEmpFixBonusAmt)
        '                    If mBonusPer = 0 Then
        '                        If pEmpFixBonusAmt <> 0 Then
        '                            GetEmpBonusAmount = GetEmpBonusAmount + pEmpFixBonusAmt
        '                            GoTo NextRow1
        '                        Else
        '                            GoTo NextRow1
        '                        End If
        '                    End If
        '                    SqlStr = " SELECT DISTINCT PAYABLESALARY, SAL_DATE, ISARREAR" & vbCrLf & " FROM PAY_SAL_TRN SALTRN" & vbCrLf & " WHERE SALTRN.Company_Code = " & mToEmpCompany & "" & vbCrLf & " AND EMP_CODE = '" & mToEmpCode & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(CntMonthEndDate, "YYYYMM") & "' AND ISARREAR<>'F'"
        '                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)
        '                    If RsSal.EOF = False Then
        '                        Do While Not RsSal.EOF
        '                            If RsSal.Fields("IsArrear").Value = "V" Then
        '                                If GetBonusPayableVoucherPayment(mToEmpCompany, mToEmpCode, VB6.Format(IIf(IsDBNull(RsSal.Fields("SAL_DATE").Value), "", RsSal.Fields("SAL_DATE").Value))) = True Then
        '                                    GetEmpBonusAmount = GetEmpBonusAmount + (IIf(IsDBNull(RsSal.Fields("PAYABLESALARY").Value), 0, RsSal.Fields("PAYABLESALARY").Value) * mBonusPer / 100)
        '                                End If
        '                            Else
        '                                GetEmpBonusAmount = GetEmpBonusAmount + (IIf(IsDBNull(RsSal.Fields("PAYABLESALARY").Value), 0, RsSal.Fields("PAYABLESALARY").Value) * mBonusPer / 100)
        '                            End If
        '                            If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
        '                                SqlStr = " SELECT GETPayableBonusAmount (" & RsCompany.Fields("COMPANY_CODE").Value & ", '" & mToEmpCode & "','" & VB6.Format(IIf(IsDBNull(RsSal.Fields("SAL_DATE").Value), "", RsSal.Fields("SAL_DATE").Value), "DD-MMM-YYYY") & "', '" & RsSal.Fields("IsArrear").Value & "') AS SALPART FROM DUAL "
        '                                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        '                                If RsTemp.EOF = False Then
        '                                    GetEmpBonusAmount = GetEmpBonusAmount + (IIf(IsDBNull(RsTemp.Fields("SALPART").Value), 0, RsTemp.Fields("SALPART").Value) * mBonusPer / 100)
        '                                End If
        '                            End If
        '                            RsSal.MoveNext()
        '                        Loop
        '                    End If
        '                    GoTo SearchRow
        '                End If
        'NextRow1:
        '            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 16 Then
        '                mWDays = CalcAttn(mCode, pDOJ, pDOL, CStr(CDate(CntMonthEndDate)))
        '                GetEmpBonusAmount = GetEmpBonusAmount + (GetBonusCeilingAmount(mCode, CntMonthStartDate) * mWDays / MainClass.LastDay(Month(CDate(CntMonthStartDate)), Year(CDate(CntMonthStartDate))))
        '            Else
        '                mWDays = CalcAttn(mCode, pDOJ, pDOL, CStr(CDate(CntMonthEndDate)))
        '                mLastDay = MainClass.LastDay(Month(CntMonthEndDate), Year(CntMonthEndDate))
        '                GetEmpBonusAmount = GetEmpBonusAmount + (GetBonusAmount_Ceiling(mCode, CStr(CDate(CntMonthEndDate))) * mWDays / mLastDay)
        '            End If
        '            CntMonth = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CntMonth)
        '        Next
        '        '    If mIsCurrentYear = "Y" Then
        '        '        mBonusAmount = mBonusAmount + Val(txtBSalary.Text)
        '        '    End If
        '        '
        '        '    If mIsCurrentYear = "Y" Then
        '        '        mArrearAmount = GetCurrentArrearPayable(mCode, "Y")
        '        '        mBonusAmount = mBonusAmount + mArrearAmount
        '        '    End If
        '        '
        '        '    GetEmpBonusAmount = (mBonusAmount * mBonusPer) / 100
        '        '    GetEmpBonusAmount = Round(GetEmpBonusAmount, 0)
        Exit Function
ErrCalcBonus:
        GetEmpBonusAmount = 0
    End Function
    Public Function GetBonusPayableVoucherPayment(ByRef mToEmpCompany As Integer, ByRef mCode As String, ByRef xVDate As String) As Boolean
        On Error GoTo ErrCalcBonus
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        SqlStr = " SELECT DISTINCT SAL_DATE " & vbCrLf & " FROM PAY_SALVOUCHER_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & mToEmpCompany & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(xVDate, "YYYYMM") & "'" & vbCrLf & " AND SAL_TYPE IN ('S','B')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            GetBonusPayableVoucherPayment = True
        End If
        Exit Function
ErrCalcBonus:
        GetBonusPayableVoucherPayment = False
    End Function
    Public Function GetBonusCeilingAmount(ByRef mCode As String, ByRef mSalDate As String) As Double
        On Error GoTo ErrPart1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xSalDate As String
        Dim mCeilingAmount As Double
        Dim mBasicAmount As Double
        Dim SqlStr As String = ""
        Dim mBonusRate As Double
        mCeilingAmount = 0 ' IIf(IsNull(RsCompany!BONUS_CEIL_AMT), 0, RsCompany!BONUS_CEIL_AMT)
        mBonusRate = 0
        xSalDate = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) & "/" & VB6.Format(mSalDate, "MM/YYYY")
        xSalDate = VB6.Format(xSalDate, "DD/MM/YYYY")
        SqlStr = " SELECT CEILING, RATE " & vbCrLf & " FROM PAY_PFESICeiling_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE = " & ConBonus & "" & vbCrLf & " AND WEF=( SELECT MAX(WEF)" & vbCrLf & " FROM PAY_PFESICeiling_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE = " & ConBonus & "" & vbCrLf & " AND WEF <= TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mCeilingAmount = IIf(IsDBNull(RsTemp.Fields("CEILING").Value), 0, RsTemp.Fields("CEILING").Value)
            mBonusRate = IIf(IsDBNull(RsTemp.Fields("Rate").Value), 0, RsTemp.Fields("Rate").Value)
        End If
        SqlStr = " SELECT DISTINCT BASICSALARY " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_EFF_DATE <= TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mBasicAmount = IIf(IsDBNull(RsTemp.Fields("BASICSALARY").Value), 0, RsTemp.Fields("BASICSALARY").Value)
            If mBasicAmount < mCeilingAmount Then
                GetBonusCeilingAmount = mBasicAmount * mBonusRate * 0.01
            Else
                GetBonusCeilingAmount = mCeilingAmount * mBonusRate * 0.01
            End If
        Else
            GetBonusCeilingAmount = 0
        End If
        GetBonusCeilingAmount = CDbl(VB6.Format(GetBonusCeilingAmount, "0.00"))
        Exit Function
ErrPart1:
        GetBonusCeilingAmount = 0
    End Function
    Public Function GetBonusAmount_Ceiling(ByRef mCode As String, ByRef mSalDate As String) As Double
        On Error GoTo ErrPart1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xSalDate As String
        Dim mCeilingAmount As Double
        Dim mBasicAmount As Double
        Dim SqlStr As String = ""
        mCeilingAmount = IIf(IsDBNull(RsCompany.Fields("BONUS_CEIL_AMT").Value), 0, RsCompany.Fields("BONUS_CEIL_AMT").Value)
        xSalDate = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) & "/" & VB6.Format(mSalDate, "MM/YYYY")
        xSalDate = VB6.Format(xSalDate, "DD/MM/YYYY")
        SqlStr = " SELECT DISTINCT AMOUNT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND ADD_DEDUCT.TYPE = " & ConBonus & "" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_EFF_DATE <= TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetBonusAmount_Ceiling = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
            '        If mBasicAmount < mCeilingAmount Then
            '            GetBonusAmount_Ceiling = mBasicAmount
            '        Else
            '            GetBonusAmount_Ceiling = mCeilingAmount
            '        End If
        Else
            GetBonusAmount_Ceiling = 0
        End If
        Exit Function
ErrPart1:
        GetBonusAmount_Ceiling = 0
    End Function
    Public Function GetBonusPer(ByRef mCompanyCode As Integer, ByRef pCode As String, ByRef pDate As String, ByRef pAmount As Double) As Double
        On Error GoTo ErrCalcBonus
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetBonusPer = 0
        pAmount = 0
        '    pEffDate = ""       '',SALARY_EFF_DATE-ADDDAYS_IN AS EFFECT_DATE
        SqlStr = " SELECT SALARY_EFF_DATE,SALARYDEF.PERCENTAGE,SALARYDEF.AMOUNT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE SALARYDEF.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & pCode & "'" & vbCrLf & " AND ADD_DEDUCT.TYPE = " & ConBonus & " AND AMOUNT<>0" & vbCrLf & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND EMP_CODE = '" & pCode & "'" & vbCrLf & " AND SALARY_EFF_DATE - ADDDAYS_IN <= TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            GetBonusPer = IIf(IsDBNull(RsTemp.Fields("PERCENTAGE").Value), 0, RsTemp.Fields("PERCENTAGE").Value)
            pAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
            '        pEffDate = IIf(IsNull(RsTemp!EFFECT_DATE), "", RsTemp!EFFECT_DATE)
        Else
            GetBonusPer = 0
        End If
        Exit Function
ErrCalcBonus:
        GetBonusPer = 0
    End Function
    Public Function SalProcess(ByRef mYM As Integer) As Boolean
        On Error GoTo ErrSalProcess
        Dim RsMain As ADODB.Recordset
        Dim SqlStr As String = ""
        SalProcess = True
        SqlStr = " SELECT EMP_CODE FROM PAY_SAL_TRN WHERE " & vbCrLf & " TO_CHAR(SAL_DATE,'YYYYMM') >= " & mYM & "" & vbCrLf & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISARREAR IN ('N','Y')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockOptimistic)
        If RsMain.EOF = False Then
            SalProcess = False
        End If
        Exit Function
ErrSalProcess:
        SalProcess = False
    End Function
    Public Function GetGrossSalary(ByRef xCode As String, ByRef xWEF As String) As Double

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object

        GetGrossSalary = 0

        SqlStr = " SELECT BASICSALARY, AMOUNT FROM PAY_SalaryDef_MST A, PAY_SALARYHEAD_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=B.COMPANY_CODE AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf _
            & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And A.EMP_CODE='" & xCode & "' AND B.ADDDEDUCT=" & ConEarning & "" & vbCrLf _
            & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SalaryDef_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
            & " AND SALARY_EFF_DATE< TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = True Then Exit Function
        GetGrossSalary = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")
        If RsADD.EOF = False Then
            Do While RsADD.EOF = False
                GetGrossSalary = GetGrossSalary + IIf(IsDBNull(RsADD.Fields("Amount").Value), 0, RsADD.Fields("Amount").Value)
                RsADD.MoveNext()
            Loop
        End If
    End Function
    Public Function GetGrossSalaryActual(ByRef xCode As String, ByRef xWEF As String) As Double

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object

        GetGrossSalaryActual = 0

        SqlStr = " SELECT FORM1_BASICSALARY AS BASICSALARY, FORM1_AMOUNT AS AMOUNT FROM PAY_SalaryDef_MST A, PAY_SALARYHEAD_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=B.COMPANY_CODE AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf _
            & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And A.EMP_CODE='" & xCode & "' AND B.ADDDEDUCT=" & ConEarning & "" & vbCrLf _
            & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SalaryDef_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
            & " AND SALARY_EFF_DATE< TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = True Then Exit Function
        GetGrossSalaryActual = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")
        If RsADD.EOF = False Then
            Do While RsADD.EOF = False
                GetGrossSalaryActual = GetGrossSalaryActual + IIf(IsDBNull(RsADD.Fields("Amount").Value), 0, RsADD.Fields("Amount").Value)
                RsADD.MoveNext()
            Loop
        End If
    End Function
    Public Function GetTOTOverTime(ByRef xTotOTHOUR As Double, ByRef xTotOTMIN As Double) As Double
        On Error GoTo ErrPart
        Dim mHour As Double
        Dim mTempMin As Double
        Dim mMin As Double
        Dim mFactor As Double

        mHour = (xTotOTHOUR)
        mTempMin = (xTotOTMIN)

        mHour = mHour + Int(mTempMin / 60)
        mMin = (mTempMin Mod 60)
        mFactor = IIf(IsDBNull(RsCompany.Fields("OTFACTOR").Value), 0, RsCompany.Fields("OTFACTOR").Value)
        mMin = Int(mMin / mFactor) * mFactor
        GetTOTOverTime = (mHour) + (mMin * 0.01)


        Exit Function
ErrPart:
        GetTOTOverTime = 0
    End Function
    Public Function CheckCPLAvail(ByRef mEmpCode As String, ByRef mDate As String) As Boolean

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckCPLAvail = False


        SqlStr = "SELECT EMP_CODE, ATTN_DATE " & vbCrLf _
            & " FROM PAY_ATTN_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND CPL_EARN>0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckCPLAvail = True
            Exit Function
        End If

        Exit Function
ErrPart:
        CheckCPLAvail = False
    End Function
    Public Function GetOTRate(ByRef xCode As String, ByRef xRunDate As String, ByRef mESIApp As Boolean, ByRef mBasicSalary As Double, ByRef mESIRound As Double, ByRef IsArrear As Boolean, ByRef mOverTimeAppType As String, ByRef mGrossSalary As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsOTRate As ADODB.Recordset
        Dim mRound As String
        'Dim mGrossSalary As Double
        Dim ConWorkDay As Integer
        Dim ConWorkHour As Integer

        ConWorkHour = 8
        If MainClass.ValidateWithMasterTable(xCode, "EMP_CODE", "WORKING_HOURS", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            ConWorkHour = Val(MasterNo)
        End If

        ConWorkHour = IIf(ConWorkHour = 0, 8, ConWorkHour)

        ConWorkDay = MainClass.LastDay(Month(CDate(xRunDate)), Year(CDate(xRunDate)))
        xRunDate = ConWorkDay & VB6.Format(xRunDate, "/MM/YYYY")

        SqlStr = " SELECT "

        'If IsArrear = True Then
        '    SqlStr = SqlStr & vbCrLf & " (BASICSALARY-PREVIOUS_BASICSALARY) AS BASICSALARY, " & vbCrLf & " (AMOUNT-PREVIOUS_AMOUNT) AS AMOUNT, "
        'Else
        '    SqlStr = SqlStr & vbCrLf & " BASICSALARY, AMOUNT, "
        'End If

        If IsArrear = True Then
            SqlStr = SqlStr & vbCrLf & " (BASICSALARY-PREVIOUS_BASICSALARY) AS BASICSALARY, " & vbCrLf & " (AMOUNT-PREVIOUS_AMOUNT) AS AMOUNT, "
        Else
            SqlStr = SqlStr & vbCrLf & " DECODE(FORM1_BASICSALARY,0,BASICSALARY,FORM1_BASICSALARY) AS BASICSALARY, FORM1_AMOUNT AS AMOUNT, "
        End If

        SqlStr = SqlStr & vbCrLf & " ADD_DEDUCTCODE, ADDDEDUCT,TYPE, ROUNDING, EMP_DESG_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " FROM PAY_SALARYDEF_MST SD, PAY_SALARYHEAD_MST SH " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " SD.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SD.COMPANY_CODE=SH.COMPANY_CODE" & vbCrLf _
            & " AND SD.ADD_DEDUCTCODE=SH.CODE" & vbCrLf _
            & " AND SD.EMP_CODE='" & xCode & "'" & vbCrLf _
            & " AND SD.SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) From PAY_SalaryDef_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
            & " AND SALARY_APP_DATE<= TO_DATE('" & VB6.Format(xRunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        If IsArrear = True Then
            SqlStr = SqlStr & vbCrLf & " AND SD.IS_ARREAR='Y' AND TO_CHAR(SD.ARREAR_DATE,'MON-YYYY') ='" & UCase(VB6.Format(xRunDate, "MMM-YYYY")) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOTRate, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOTRate.EOF = False Then
            ''Manager or Director then exit function dt. 15-09-2006.............
            If MainClass.ValidateWithMasterTable(RsOTRate.Fields("EMP_DESG_CODE").Value, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DESG_CAT IN ('D','M')") = True Then
                GetOTRate = 0
                mBasicSalary = 0
                mESIApp = False
                Exit Function
            End If

            mBasicSalary = IIf(IsDBNull(RsOTRate.Fields("BASICSALARY").Value), 0, RsOTRate.Fields("BASICSALARY").Value)
            mGrossSalary = mBasicSalary

            Do While Not RsOTRate.EOF
                If RsOTRate.Fields("ADDDEDUCT").Value = 1 Then

                    mGrossSalary = mGrossSalary + IIf(IsDBNull(RsOTRate.Fields("Amount").Value), 0, RsOTRate.Fields("Amount").Value)

                Else
                    '                mGrossSalary = mGrossSalary - IIf(IsNull(RsOTRate!AMOUNT), 0, RsOTRate!AMOUNT)
                    If RsOTRate.Fields("Type").Value = ConESI Then
                        '                    mESIRound = RsOTRate!ROUNDING
                        mESIRound = IIf(CDate(xRunDate) > CDate("01/12/2004"), "10", RsOTRate.Fields("ROUNDING").Value)
                        If RsOTRate.Fields("Amount").Value = 0 Then
                            mESIApp = False
                        Else
                            mESIApp = True
                        End If
                    End If
                End If
                RsOTRate.MoveNext()
            Loop

            GetOTRate = mGrossSalary / (ConWorkDay * ConWorkHour)
        Else
            GetOTRate = 0
            mBasicSalary = 0
            mGrossSalary = 0
            mESIApp = False
        End If


        If MainClass.ValidateWithMasterTable(ConOT, "Type", "Rounding", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            GetOTRate = Int(GetOTRate) + IIf(GetOTRate > Int(GetOTRate), 1, 0)
        Else
            GetOTRate = CDbl(VB6.Format(GetOTRate, "0.00"))
        End If
        mBasicSalary = mGrossSalary
        Exit Function
ErrPart:
        GetOTRate = 0
        mBasicSalary = 0
        mESIApp = False
    End Function
    Public Function GetTillOTHours(ByRef pEmpCode As String, ByRef pAttnDate As String) As Double

        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTotOTHour As Double
        Dim mTotOTMIN As Double
        Dim mMonthStart As String

        Dim SqlStr As String = ""
        mTotOTHour = 0
        mTotOTMIN = 0
        GetTillOTHours = 0
        mMonthStart = "01/" & VB6.Format(pAttnDate, "MM/YYYY")

        SqlStr = "SELECT SUM(OT.OTHOUR) AS OTHOUR , SUM(OT.OTMIN) AS OTMIN" & vbCrLf _
                & " FROM PAY_OVERTIME_MST OT " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And OT.EMP_CODE='" & pEmpCode & "' " & vbCrLf _
                & " AND OT.OT_DATE>=TO_DATE('" & VB6.Format(mMonthStart, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND OT.OT_DATE<=TO_DATE('" & VB6.Format(pAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " ORDER BY OT.OT_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            '.Text = CStr(IIf(IsDBNull(RsOT.Fields("OTHOUR").Value), 0, RsOT.Fields("OTHOUR").Value))
            '.Text = CStr(IIf(IsDBNull(RsOT.Fields("OTMIN").Value), "", .Text & ".") & RsOT.Fields("OTMIN").Value)

            mTotOTHour = IIf(IsDBNull(RsTemp.Fields("OTHOUR").Value), 0, RsTemp.Fields("OTHOUR").Value)
            mTotOTMIN = IIf(IsDBNull(RsTemp.Fields("OTMIN").Value), 0, RsTemp.Fields("OTMIN").Value)

            GetTillOTHours = (mTotOTHour * 60) + mTotOTMIN
        End If


        SqlStr = "SELECT SUM(OT_HOURS) AS OT_HOURS" & vbCrLf _
                & " FROM PAY_MOVEMENT_TRN OT " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And OT.EMP_CODE='" & pEmpCode & "' AND HR_APPROVAL='Y'" & vbCrLf _
                & " AND OT.REF_DATE>=TO_DATE('" & VB6.Format(mMonthStart, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND OT.REF_DATE<TO_DATE('" & VB6.Format(pAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " ORDER BY OT.REF_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mTotOTHour = IIf(IsDBNull(RsTemp.Fields("OT_HOURS").Value), 0, RsTemp.Fields("OT_HOURS").Value)
            GetTillOTHours = GetTillOTHours - mTotOTHour
        End If

        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Public Function ConvertToLetter(ByRef iCol As Integer) As String

        Dim Reminder_Part As Integer = iCol Mod 26
        Dim Integer_Part As Integer = Int(iCol / 26)

        If Integer_Part = 0 Then
            ConvertToLetter = Chr(Reminder_Part + 64)
        ElseIf Integer_Part > 0 And Reminder_Part <> 0 Then
            ConvertToLetter = Chr(Integer_Part + 64) + Chr(Reminder_Part + 64)
        ElseIf Integer_Part > 0 And Reminder_Part = 0 Then
            ConvertToLetter = Chr(Integer_Part * 26 + 64)
        End If


    End Function
End Module
