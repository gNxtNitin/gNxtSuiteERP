Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Module LeaveModule
    Public Function GetOpeningCPLOld(ByRef mCode As String, ByRef pAsOnDate As String) As Double

        On Error GoTo ErrFillLeaves
        Dim RsLeaves As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mStartDate As String
        Dim mShowOpening As Boolean
        Dim mMonthStartDate As String
        GetOpeningCPLOld = 0

        mMonthStartDate = VB6.Format(pAsOnDate, "DD/MM/YYYY")
        mStartDate = VB6.Format("01/01/" & Year(CDate(mMonthStartDate)), "DD/MM/YYYY")
        mShowOpening = True
        If RsCompany.Fields("COMPANY_CODE").Value = 15 And CDate(mMonthStartDate) >= CDate("01/09/2012") Then
            mShowOpening = False
            mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -120, CDate(mMonthStartDate))) ''"01/09/2012"
            If CDate(mStartDate) < CDate("01/09/2012") Then
                mStartDate = "01/09/2012"
            End If
        End If

        If mShowOpening = True Then
            SqlStr = " SELECT SUM(NVL(OPENING,0)) AS OPENING " & vbCrLf & " FROM PAY_OPLEAVE_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR =" & Year(CDate(pAsOnDate)) & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "' AND  LEAVECODE=" & CPLEARN & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeaves, ADODB.LockTypeEnum.adLockOptimistic)

            If RsLeaves.EOF = False Then
                GetOpeningCPLOld = IIf(IsDBNull(RsLeaves.Fields("OPENING").Value), 0, RsLeaves.Fields("OPENING").Value)
            End If
        End If

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND " & vbCrLf & " EMP_CODE='" & mCode & "'" & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<TO_DATE('" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeaves, ADODB.LockTypeEnum.adLockOptimistic)
        If RsLeaves.EOF = False Then
            Do While Not RsLeaves.EOF
                If RsLeaves.Fields("FIRSTHALF").Value = CPLEARN Then
                    GetOpeningCPLOld = GetOpeningCPLOld + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = CPLAVAIL Then
                    GetOpeningCPLOld = GetOpeningCPLOld - 0.5
                End If

                If RsLeaves.Fields("SECONDHALF").Value = CPLEARN Then
                    GetOpeningCPLOld = GetOpeningCPLOld + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = CPLAVAIL Then
                    GetOpeningCPLOld = GetOpeningCPLOld - 0.5
                End If
                RsLeaves.MoveNext()
            Loop
        End If
        Exit Function
ErrFillLeaves:
        GetOpeningCPLOld = 0
    End Function

    Public Function GetOpeningCPL(ByRef mCode As String, ByRef pAsOnDate As String, ByRef mCPLEarn As Double, ByRef mCPLAvail As Double, ByRef mBalance As Double) As Boolean

        On Error GoTo ErrFillLeaves
        Dim RsLeaves As ADODB.Recordset
        Dim SqlStr As String = ""

        mCPLEarn = 0
        mCPLAvail = 0
        mBalance = 0

        SqlStr = " SELECT SUM(CPL_EARN) AS CPL_EARN " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND CPL_EARN >0" & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(pAsOnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        '    SqlStr = SqlStr & vbCrLf & " UNION "
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " SELECT COUNT(SECONDHALF) AS CNTEARN, 'S' AS FHALF " & vbCrLf _
        ''            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
        ''            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND EMP_CODE='" & mCode & "'" & vbCrLf _
        ''            & " AND SECONDHALF= " & CPLEARN & "" & vbCrLf _
        ''            & " AND ATTN_DATE>='" & VB6.Format(pAsOnDate, "DD-MMM-YYYY") & "'"

        mCPLEarn = 0
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeaves, ADODB.LockTypeEnum.adLockOptimistic)
        If RsLeaves.EOF = False Then
            '        Do While Not RsLeaves.EOF
            mCPLEarn = IIf(IsDBNull(RsLeaves.Fields("CPL_EARN").Value), 0, RsLeaves.Fields("CPL_EARN").Value) * 0.5
            '            RsLeaves.MoveNext
            '        Loop
        End If

        '    mCPLEARN = mCPLEARN / 2


        SqlStr = " SELECT COUNT(CPL_AGT_DATE_FH) AS CNTEARN , 'F' AS FHALF" & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND FIRSTHALF= " & CPLAVAIL & "" & vbCrLf & " AND CPL_AGT_DATE_FH>=TO_DATE('" & VB6.Format(pAsOnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " UNION "

        SqlStr = SqlStr & vbCrLf & " SELECT COUNT(CPL_AGT_DATE_SH) AS CNTEARN, 'S' AS FHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND SECONDHALF= " & CPLAVAIL & "" & vbCrLf & " AND CPL_AGT_DATE_SH>=TO_DATE('" & VB6.Format(pAsOnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeaves, ADODB.LockTypeEnum.adLockOptimistic)
        If RsLeaves.EOF = False Then
            Do While Not RsLeaves.EOF
                mCPLAvail = mCPLAvail + IIf(IsDBNull(RsLeaves.Fields("CNTEARN").Value), 0, RsLeaves.Fields("CNTEARN").Value)
                RsLeaves.MoveNext()
            Loop
        End If

        mCPLAvail = mCPLAvail / 2

        mBalance = mCPLEarn - mCPLAvail

        GetOpeningCPL = True
        Exit Function
ErrFillLeaves:
        GetOpeningCPL = False
    End Function

    Public Function CheckCPLEarnBalance(ByRef mCode As String, ByRef pDate As String, ByRef pEarnDate As String, ByRef mMonthAvail As Double) As Boolean

        On Error GoTo ErrFillLeaves
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim xEarn As Double
        Dim mMonth As String
        Dim mBalance As Double

        CheckCPLEarnBalance = False

        xEarn = 0

        mMonth = "01/" & VB6.Format(pDate, "MM/YYYY")

        SqlStr = " SELECT SUM(CPL_EARN) AS CPL_EARN " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pEarnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND ATTN_DATE<TO_DATE('" & VB6.Format(mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            xEarn = IIf(IsDBNull(RsTemp.Fields("CPL_EARN").Value), 0, RsTemp.Fields("CPL_EARN").Value) * 0.5
        End If


        If xEarn > 0 Then

            SqlStr = " SELECT ATTN_DATE,FIRSTHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND CPL_AGT_DATE_FH =TO_DATE('" & VB6.Format(pEarnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & " AND ATTN_DATE<TO_DATE('" & VB6.Format(mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If RsTemp.Fields("FIRSTHALF").Value = CPLAVAIL Then
                    xEarn = xEarn - 0.5
                End If
            End If

            SqlStr = " SELECT ATTN_DATE,SECONDHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND CPL_AGT_DATE_SH =TO_DATE('" & VB6.Format(pEarnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            SqlStr = SqlStr & vbCrLf & " AND ATTN_DATE<TO_DATE('" & VB6.Format(mMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If RsTemp.Fields("SECONDHALF").Value = CPLAVAIL Then
                    xEarn = xEarn - 0.5
                End If
            End If
        End If

        xEarn = xEarn - mMonthAvail

        CheckCPLEarnBalance = IIf(xEarn < 0, False, True)
        Exit Function
ErrFillLeaves:
        CheckCPLEarnBalance = False
    End Function

    Public Function GetCPLAvailDate(ByRef mCode As String, ByRef pDate As String, Optional ByRef mTotalCPLAvail As Double = 0) As String

        On Error GoTo ErrFillLeaves
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        GetCPLAvailDate = ""
        mTotalCPLAvail = 0
        '',FIRSTHALF,SECONDHALF,CPL_AGT_DATE_FH AS EARN_DATE

        SqlStr = " SELECT ATTN_DATE,FIRSTHALF AS AVAIL_HALF, 'F' AS SHOWHALF" & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'"

        SqlStr = SqlStr & vbCrLf & " AND FIRSTHALF= " & CPLAVAIL & ""

        SqlStr = SqlStr & vbCrLf & " AND CPL_AGT_DATE_FH =TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " UNION "

        SqlStr = SqlStr & vbCrLf & " SELECT ATTN_DATE,SECONDHALF AS AVAIL_HALF, 'S' AS SHOWHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'"

        SqlStr = SqlStr & vbCrLf & " AND SECONDHALF= " & CPLAVAIL & ""

        SqlStr = SqlStr & vbCrLf & " AND CPL_AGT_DATE_SH =TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY  1"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                If RsTemp.Fields("AVAIL_HALF").Value = CPLAVAIL Then
                    GetCPLAvailDate = IIf(GetCPLAvailDate = "", "", GetCPLAvailDate & ", ") & IIf(IsDBNull(RsTemp.Fields("ATTN_DATE").Value), "", RsTemp.Fields("ATTN_DATE").Value)
                    mTotalCPLAvail = mTotalCPLAvail + 0.5
                End If
                '
                '            If RsTemp!SECONDHALF = CPLAVAIL Then
                '                GetCPLAvailDate = IIf(GetCPLAvailDate = "", "", GetCPLAvailDate & ", ") & IIf(IsNull(RsTemp!ATTN_DATE), "", RsTemp!ATTN_DATE)
                '            End If
                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
ErrFillLeaves:
        GetCPLAvailDate = ""
    End Function

    Public Function GETEntitleEarnLeave(ByRef pDBCn As ADODB.Connection, ByRef pEmpCode As String, ByRef pLeaveCode As Integer, ByRef pRunDate As String) As Double

        On Error GoTo ErrPart
        Dim RsBalEL As ADODB.Recordset
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mFHalf As Double
        Dim mSHalf As Double
        Dim xRunDate As String
        Dim mTotalLeaves As Double
        Dim mTotalHoliDays As Double
        Dim mTotalRunningDays As Double
        Dim mDOJ As String
        Dim mDOL As String
        Dim mStartingDate As String
        Dim mEndingDate As String
        Dim SqlStr As String = ""
        Dim mCategory As String
        GETEntitleEarnLeave = 0


        '    If pLeaveCode <> EARN Then Exit Function

        xRunDate = VB6.Format(pRunDate, "DD/MM/YYYY")

        mStartingDate = "01/01/" & Year(CDate(xRunDate))
        mEndingDate = xRunDate ''  11-01-2007  MainClass.LastDay(Month(xRunDate), Year(xRunDate)) & "/" & vb6.Format(xRunDate, "MM/YYYY")
        '    mEndingDate = "31/12/" & Year(xRunDate)

        SqlStr = " SELECT EMP_DOJ, EMP_LEAVE_DATE,EMP_CATG,EMP_CAT_TYPE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST WHERE " & vbCrLf _
            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE ='" & pEmpCode & "'"

        MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmp.EOF = False Then
            mDOJ = IIf(IsDBNull(RsEmp.Fields("EMP_DOJ").Value), "", RsEmp.Fields("EMP_DOJ").Value)
            mDOL = IIf(IsDBNull(RsEmp.Fields("EMP_LEAVE_DATE").Value), "", RsEmp.Fields("EMP_LEAVE_DATE").Value)
            mCategory = IIf(IsDBNull(RsEmp.Fields("EMP_CATG").Value), "G", RsEmp.Fields("EMP_CATG").Value)
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

        '--------------------------------------------------------------28/12/2018

        '    SqlStr = " SELECT COUNT(FIRSTHALF) AS FIRSTHALF" & vbCrLf _
        ''            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
        ''            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND PAYYEAR=" & Year(xRunDate) & " " & vbCrLf _
        ''            & " AND EMP_CODE ='" & pEmpCode & "'" & vbCrLf _
        ''            & " AND FIRSTHALF NOT IN (-1," & CPLEARN & "," & CPLAVAIL & "," & PRESENT & "," & SUNDAY & "," & HOLIDAY & ")" & vbCrLf _
        ''            & " AND ATTN_DATE<='" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "'"
        '
        '
        '    MainClass.UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RsBalEL, adLockOptimistic
        '
        '    mFHalf = 0
        '    If RsBalEL.EOF = False Then
        '        mFHalf = IIf(IsNull(RsBalEL!FIRSTHALF), 0, RsBalEL!FIRSTHALF) * 0.5
        '    End If
        '
        '    SqlStr = " SELECT COUNT(SECONDHALF) AS SECONDHALF" & vbCrLf _
        ''            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
        ''            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND PAYYEAR=" & Year(xRunDate) & " " & vbCrLf _
        ''            & " AND EMP_CODE ='" & pEmpCode & "'" & vbCrLf _
        ''            & " AND SECONDHALF NOT IN (-1," & CPLEARN & "," & CPLAVAIL & "," & PRESENT & "," & SUNDAY & "," & HOLIDAY & ")" & vbCrLf _
        ''            & " AND ATTN_DATE<='" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "'"
        '
        '    MainClass.UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RsBalEL, adLockOptimistic
        '
        '    mSHalf = 0
        '    If RsBalEL.EOF = False Then
        '        mSHalf = IIf(IsNull(RsBalEL!SECONDHALF), 0, RsBalEL!SECONDHALF) * 0.5
        '    End If
        '
        '    SqlStr = " SELECT COUNT(ATTN_DATE) AS ATTN_CNT " & vbCrLf _
        ''            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
        ''            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND PAYYEAR=" & Year(xRunDate) & " " & vbCrLf _
        ''            & " AND EMP_CODE ='" & pEmpCode & "'" & vbCrLf _
        ''            & " AND FIRSTHALF IN (" & SUNDAY & "," & HOLIDAY & ")" & vbCrLf _
        ''            & " AND ATTN_DATE<='" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''            & " AND ATTN_DATE IN (" & vbCrLf _
        ''            & " SELECT HOLIDAY_DATE FROM PAY_HOLIDAY_MST" & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND AGT_WORKING='N')"
        '
        '
        '    MainClass.UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RsBalEL, adLockOptimistic
        '    mTotalHoliDays = 0
        '    If RsBalEL.EOF = False Then
        '        mTotalHoliDays = IIf(IsNull(RsBalEL!ATTN_CNT), 0, RsBalEL!ATTN_CNT)
        '    End If
        '

        SqlStr = " SELECT ATTN_DATE,FIRSTHALF, SECONDHALF " & vbCrLf _
            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PAYYEAR=" & Year(CDate(xRunDate)) & " " & vbCrLf _
            & " AND EMP_CODE ='" & pEmpCode & "'" & vbCrLf _
            & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalEL, ADODB.LockTypeEnum.adLockOptimistic)

        If RsBalEL.EOF = False Then
            Do While Not RsBalEL.EOF
                If RsBalEL.Fields("FIRSTHALF").Value <> -1 Then
                    If RsBalEL.Fields("FIRSTHALF").Value = CPLEARN Or RsBalEL.Fields("FIRSTHALF").Value = CPLAVAIL Or RsBalEL.Fields("FIRSTHALF").Value = PRESENT Then

                    ElseIf RsBalEL.Fields("FIRSTHALF").Value = SUNDAY Or RsBalEL.Fields("FIRSTHALF").Value = HOLIDAY Then
                        If GetHolidayAgtWorking(RsBalEL.Fields("ATTN_DATE").Value) = "N" Then
                            mTotalHoliDays = mTotalHoliDays + 0.5
                        End If
                    Else
                        mFHalf = mFHalf + 0.5
                    End If
                End If

                If RsBalEL.Fields("SECONDHALF").Value <> -1 Then
                    If RsBalEL.Fields("SECONDHALF").Value = CPLEARN Or RsBalEL.Fields("SECONDHALF").Value = CPLAVAIL Or RsBalEL.Fields("SECONDHALF").Value = PRESENT Then

                    ElseIf RsBalEL.Fields("SECONDHALF").Value = SUNDAY Or RsBalEL.Fields("SECONDHALF").Value = HOLIDAY Then
                        If GetHolidayAgtWorking(RsBalEL.Fields("ATTN_DATE").Value) = "N" Then
                            mTotalHoliDays = mTotalHoliDays + 0.5
                        End If
                    Else
                        mSHalf = mSHalf + 0.5
                    End If
                End If
                RsBalEL.MoveNext()
            Loop
        End If

        '--------------------------------------------------------------28/12/2018

        mTotalLeaves = mFHalf + mSHalf

        'If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
        '    If Year(CDate(xRunDate)) < 2006 Then
        '        SqlStr = " SELECT COUNT(1) AS HOLIDAYCNT " & vbCrLf & " FROM PAY_HOLIDAY_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HOLIDAY_DATE>=TO_DATE('" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND HOLIDAY_DATE<=TO_DATE('" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        '        MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalEL, ADODB.LockTypeEnum.adLockOptimistic)

        '        If RsBalEL.EOF = False Then
        '            mTotalHoliDays = IIf(IsDBNull(RsBalEL.Fields("HOLIDAYCNT").Value), 0, RsBalEL.Fields("HOLIDAYCNT").Value)
        '        End If
        '    End If
        'Else
        '    If Year(CDate(xRunDate)) <= 2006 Then
        '        SqlStr = " SELECT COUNT(1) AS HOLIDAYCNT " & vbCrLf & " FROM PAY_HOLIDAY_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HOLIDAY_DATE>=TO_DATE('" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND HOLIDAY_DATE<=TO_DATE('" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        '        MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalEL, ADODB.LockTypeEnum.adLockOptimistic)

        '        If RsBalEL.EOF = False Then
        '            mTotalHoliDays = IIf(IsDBNull(RsBalEL.Fields("HOLIDAYCNT").Value), 0, RsBalEL.Fields("HOLIDAYCNT").Value)
        '        End If
        '    End If
        'End If

        '    If RsBalEL.EOF = False Then
        '        mTotalHoliDays = IIf(IsNull(RsBalEL!HOLIDAYCNT), 0, RsBalEL!HOLIDAYCNT)
        '    End If

        GETEntitleEarnLeave = mTotalRunningDays - mTotalLeaves - mTotalHoliDays

        If mCategory = "G" Or mCategory = "P" Or mCategory = "D" Or mCategory = "T" Then
            GETEntitleEarnLeave = GETEntitleEarnLeave / IIf(RsCompany.Fields("STAFF_EL_PER_DAYS").Value = 0, 15, RsCompany.Fields("STAFF_EL_PER_DAYS").Value)
        ElseIf mCategory = "S" Or mCategory = "E" Then
            'If Val(pEmpCode) < 1000 Then
            GETEntitleEarnLeave = GETEntitleEarnLeave / IIf(RsCompany.Fields("STAFF_EL_PER_DAYS").Value = 0, 15, RsCompany.Fields("STAFF_EL_PER_DAYS").Value)
            'Else
            '    GETEntitleEarnLeave = GETEntitleEarnLeave / IIf(RsCompany.Fields("WORKER_EL_PER_DAYS").Value = 0, 15, RsCompany.Fields("WORKER_EL_PER_DAYS").Value)
            'End If
        Else
            GETEntitleEarnLeave = GETEntitleEarnLeave / IIf(RsCompany.Fields("WORKER_EL_PER_DAYS").Value = 0, 15, RsCompany.Fields("WORKER_EL_PER_DAYS").Value) '20
        End If

        GETEntitleEarnLeave = System.Math.Round(GETEntitleEarnLeave + 0.01, 0)

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Function GetOpeningLeaves(ByRef mEmpCode As String, ByRef pRefDate As String, ByRef pLeaveCode As Integer, ByRef mIsOpening As String, ByRef mIsEntitle As String, ByRef mOPFromDate As String) As Double

        On Error GoTo ErrPart
        Dim RsOpLeave As ADODB.Recordset
        Dim mDOJ As String

        Dim mPeriod As Double

        Dim mStartDate As String
        Dim mYearStartDate As String
        Dim mYearDays As Integer
        Dim xMonthLastDate As String
        Dim xQtrLastDate As String
        Dim SqlStr As String = ""
        Dim mAvailLeave As Double
        Dim mDOL As String

        Dim pMonth As Integer
        Dim mNewDate As String
        Dim mELPeriod As Double
        Dim xPeriod As Double
        Dim xTillDateDays As Double
        Dim xELEntitle As Integer = 0


        GetOpeningLeaves = 0

        '

        pMonth = Month(CDate(pRefDate))
        xMonthLastDate = MainClass.LastDay(Month(CDate(pRefDate)), Year(CDate(pRefDate))) & "/" & VB6.Format(pRefDate, "MM/YYYY")
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            Select Case pMonth
                Case 1, 2, 3
                    xQtrLastDate = "31/03/" & VB6.Format(pRefDate, "YYYY")
                Case 4, 5, 6
                    xQtrLastDate = "30/06/" & VB6.Format(pRefDate, "YYYY")
                Case 7, 8, 9
                    xQtrLastDate = "30/09/" & VB6.Format(pRefDate, "YYYY")
                Case 10, 11, 12
                    xQtrLastDate = "31/12/" & VB6.Format(pRefDate, "YYYY")
            End Select
        End If

        If RsCompany.Fields("ENTITLE_AFTER_CONFIRM").Value = "Y" And (pLeaveCode = SICK Or pLeaveCode = CASUAL) Then
            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DOC", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDOJ = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDOL = MasterNo
            End If

            If CDate(mDOJ) > CDate(xMonthLastDate) Then
                GetOpeningLeaves = 0
                Exit Function
            End If
        Else
            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDOJ = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDOL = MasterNo
            End If
        End If




        If Year(CDate(pRefDate)) = Year(CDate(mDOJ)) Then
            mStartDate = mDOJ
        Else
            mStartDate = "01/01/" & VB6.Format(pRefDate, "YYYY")
        End If

        mYearStartDate = "01/01/" & VB6.Format(pRefDate, "YYYY")
        If Trim(mDOL) <> "" Then
            If CDate(mDOL) < CDate(xMonthLastDate) Then
                xMonthLastDate = mDOL
                xQtrLastDate = mDOL
            End If
        End If

        mYearDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate("01/01/" & VB6.Format(pRefDate, "YYYY")), CDate("31/12/" & VB6.Format(pRefDate, "YYYY"))) + 1


        mPeriod = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(xMonthLastDate)) + 1
        mPeriod = CDbl(VB6.Format(mPeriod / mYearDays, "0.00000"))

        If pLeaveCode = EARN Then
            If mIsEntitle = "Y" Then
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then

                    xPeriod = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(xQtrLastDate)) + 1


                    Select Case pMonth
                        Case 1, 2, 3
                            xELEntitle = 4
                            xTillDateDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate("01/01/" & VB6.Format(pRefDate, "YYYY")), CDate("31/03/" & VB6.Format(pRefDate, "YYYY"))) + 1
                        Case 4, 5, 6
                            xELEntitle = 8
                            xTillDateDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate("01/01/" & VB6.Format(pRefDate, "YYYY")), CDate("30/06/" & VB6.Format(pRefDate, "YYYY"))) + 1
                        Case 7, 8, 9
                            xELEntitle = 12
                            xTillDateDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate("01/01/" & VB6.Format(pRefDate, "YYYY")), CDate("30/09/" & VB6.Format(pRefDate, "YYYY"))) + 1
                        Case 10, 11, 12
                            xELEntitle = 15
                            xTillDateDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate("01/01/" & VB6.Format(pRefDate, "YYYY")), CDate("31/12/" & VB6.Format(pRefDate, "YYYY"))) + 1
                    End Select

                    xPeriod = CDbl(VB6.Format(xPeriod / xTillDateDays, "0.00000"))
                    GetOpeningLeaves = xELEntitle * xPeriod

                    'pMonth = ((Month(CDate(pRefDate)) \ 4) + 1) * 3
                    'mNewDate = VB6.Format(pRefDate, "DD") & "/" & VB6.Format(pMonth, "00") & "/" & VB6.Format(pRefDate, "YYYY")

                    'xQtrLastDate = MainClass.LastDay(pMonth, Year(CDate(mNewDate))) & "/" & VB6.Format(mNewDate, "MM/YYYY")

                    'mELPeriod = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(xQtrLastDate)) + 1
                    'mELPeriod = CDbl(VB6.Format(mELPeriod / mYearDays, "0.00000"))

                    'GetOpeningLeaves = GetLeaveEntitle(pLeaveCode, mEmpCode, pRefDate) * mELPeriod

                    'If pMonth < 4 Then
                    '    GetOpeningLeaves = GetOpeningLeaves + 0.5
                    'ElseIf mELPeriod > 0.3 Then
                    '    GetOpeningLeaves = GetOpeningLeaves + 0.5
                    'End If
                Else
                    GetOpeningLeaves = GETEntitleEarnLeave(PubDBCn, mEmpCode, EARN, xMonthLastDate)
                End If

            End If
        End If

        '    mPeriod = Round(Month(lblDate.Caption) / 12, 2)

        SqlStr = " SELECT NVL(OPENING,0) AS OPENING, NVL(TOTENTITLE,0) AS  TOTENTITLE, LEAVECODE " & vbCrLf _
            & " FROM PAY_OPLEAVE_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PAYYEAR =" & Year(CDate(pRefDate)) & "" & vbCrLf _
            & " AND LEAVECODE =" & pLeaveCode & "" & vbCrLf _
            & " AND EMP_CODE ='" & mEmpCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOpLeave.EOF = False Then
            Do While Not RsOpLeave.EOF

                If RsOpLeave.Fields("LeaveCode").Value = EARN Then
                    If mIsOpening = "Y" Then
                        GetOpeningLeaves = GetOpeningLeaves + IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                    End If
                Else
                    If mIsOpening = "Y" Then
                        GetOpeningLeaves = IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                    End If

                    If mIsEntitle = "Y" Then
                        GetOpeningLeaves = GetOpeningLeaves + GetLeaveEntitle(pLeaveCode, mEmpCode, pRefDate) * mPeriod
                        '                    GetOpeningLeaves = GetOpeningLeaves + IIf(IsNull(RsOpLeave!TOTENTITLE), 0, RsOpLeave!TOTENTITLE) * mPeriod
                    End If


                    GetOpeningLeaves = System.Math.Round(GetOpeningLeaves * 2, 0) / 2
                End If

                RsOpLeave.MoveNext()
            Loop
        Else '
            If mIsEntitle = "Y" Then
                If pLeaveCode = EARN Then

                Else
                    GetOpeningLeaves = GetOpeningLeaves + GetLeaveEntitle(pLeaveCode, mEmpCode, pRefDate) * mPeriod
                End If

            End If

            GetOpeningLeaves = System.Math.Round(GetOpeningLeaves * 2, 0) / 2
        End If
        mAvailLeave = 0


        If IsDate(mOPFromDate) = True Then
            If pLeaveCode = CPLEARN Then
                SqlStr = " SELECT SUM(CPL_EARN) AS CPL_EARN " & vbCrLf & " FROM PAY_ATTN_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR =" & Year(CDate(pRefDate)) & "" & vbCrLf & " AND EMP_CODE ='" & mEmpCode & "' AND CPL_EARN>0 " & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mOPFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" '' PAYFYEAR : mOPFromDate  change on 24/10/2020
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

                If RsOpLeave.EOF = False Then
                    mAvailLeave = (IIf(IsDBNull(RsOpLeave.Fields("CPL_EARN").Value), 0, RsOpLeave.Fields("CPL_EARN").Value)) * 0.5
                End If
            Else
                SqlStr = " SELECT COUNT(1) CNTLEAVE " & vbCrLf & " FROM PAY_ATTN_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR =" & Year(CDate(pRefDate)) & "" & vbCrLf & " AND EMP_CODE ='" & mEmpCode & "'"

                SqlStr = SqlStr & vbCrLf & "AND FIRSTHALF =" & pLeaveCode & ""

                SqlStr = SqlStr & vbCrLf & "AND ATTN_DATE<=TO_DATE('" & VB6.Format(mOPFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" ''PAYFYEAR : mOPFromDate  change on 24/10/2020
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

                If RsOpLeave.EOF = False Then
                    mAvailLeave = IIf(IsDBNull(RsOpLeave.Fields("CNTLEAVE").Value), 0, RsOpLeave.Fields("CNTLEAVE").Value)
                End If

                SqlStr = " SELECT COUNT(1) CNTLEAVE " & vbCrLf & " FROM PAY_ATTN_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR =" & Year(CDate(pRefDate)) & "" & vbCrLf & " AND EMP_CODE ='" & mEmpCode & "'"

                SqlStr = SqlStr & vbCrLf & "AND SECONDHALF =" & pLeaveCode & ""

                SqlStr = SqlStr & vbCrLf & "AND ATTN_DATE<TO_DATE('" & VB6.Format(mOPFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" ''PAYFYEAR : mOPFromDate  change on 24/10/2020
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

                If RsOpLeave.EOF = False Then
                    mAvailLeave = mAvailLeave + IIf(IsDBNull(RsOpLeave.Fields("CNTLEAVE").Value), 0, RsOpLeave.Fields("CNTLEAVE").Value)
                End If
                mAvailLeave = mAvailLeave / 2
            End If
        End If

        GetOpeningLeaves = GetOpeningLeaves - mAvailLeave

        'If pLeaveCode = EARN And mIsOpening = "Y" And RsCompany.Fields("COMPANY_CODE").Value = 11 Then
        '    GetOpeningLeaves = GetOpeningLeaves - GetELPaidDays(mEmpCode, pRefDate)
        'End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Public Function GetELPaidDays(ByRef mEmpCode As String, ByRef pRefDate As String, Optional ByRef pToRefDate As String = "") As Double

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        GetELPaidDays = 0

        If Not IsDate(pRefDate) Then Exit Function

        If pToRefDate = "" Then
            SqlStr = " SELECT PAID_LEAVES FROM PAY_ENCASH_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR= " & Year(CDate(pRefDate)) & "" & vbCrLf & " AND BOOKTYPE= 'E'" & vbCrLf & " AND PAID_MONTH<= TO_DATE('" & VB6.Format(pRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "'"
        Else
            SqlStr = " SELECT PAID_LEAVES FROM PAY_ENCASH_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR= " & Year(CDate(pRefDate)) & "" & vbCrLf & " AND BOOKTYPE= 'E'" & vbCrLf & " AND PAID_MONTH>= TO_DATE('" & VB6.Format(pRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND PAID_MONTH<= TO_DATE('" & VB6.Format(pToRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetELPaidDays = IIf(IsDBNull(RsTemp.Fields("PAID_LEAVES").Value), 0, RsTemp.Fields("PAID_LEAVES").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Function
    Public Function GetLeavesAvail(ByRef mEmpCode As String, ByRef pFromRefDate As String, ByRef pToRefDate As String, ByRef pLeaveCode As Integer) As Double

        On Error GoTo ErrPart
        Dim RsOpLeave As ADODB.Recordset
        Dim mDOJ As String

        Dim mPeriod As Double

        Dim mStartDate As String
        Dim mYearDays As Integer
        Dim xMonthLastDate As String
        Dim SqlStr As String = ""
        Dim mAvailLeave As Double

        GetLeavesAvail = 0

        If pLeaveCode = CPLEARN Then
            SqlStr = " SELECT SUM(CPL_EARN) AS CPL_EARN " & vbCrLf & " FROM PAY_ATTN_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR =" & Year(CDate(pToRefDate)) & "" & vbCrLf & " AND EMP_CODE ='" & mEmpCode & "' AND CPL_EARN>0 "

            If IsDate(pFromRefDate) = True Then
                SqlStr = SqlStr & vbCrLf & "AND ATTN_DATE>=TO_DATE('" & VB6.Format(pFromRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            SqlStr = SqlStr & vbCrLf & "AND ATTN_DATE<=TO_DATE('" & VB6.Format(pToRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

            If RsOpLeave.EOF = False Then
                GetLeavesAvail = IIf(IsDBNull(RsOpLeave.Fields("CPL_EARN").Value), 0, RsOpLeave.Fields("CPL_EARN").Value) * 0.5
            End If
        Else
            SqlStr = " SELECT COUNT(1) CNTLEAVE " & vbCrLf & " FROM PAY_ATTN_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR =" & Year(CDate(pToRefDate)) & "" & vbCrLf & " AND EMP_CODE ='" & mEmpCode & "'"

            SqlStr = SqlStr & vbCrLf & "AND FIRSTHALF =" & pLeaveCode & ""

            If IsDate(pFromRefDate) = True Then
                SqlStr = SqlStr & vbCrLf & "AND ATTN_DATE>=TO_DATE('" & VB6.Format(pFromRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            SqlStr = SqlStr & vbCrLf & "AND ATTN_DATE<=TO_DATE('" & VB6.Format(pToRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

            If RsOpLeave.EOF = False Then
                GetLeavesAvail = IIf(IsDBNull(RsOpLeave.Fields("CNTLEAVE").Value), 0, RsOpLeave.Fields("CNTLEAVE").Value)
            End If

            SqlStr = " SELECT COUNT(1) CNTLEAVE " & vbCrLf & " FROM PAY_ATTN_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR =" & Year(CDate(pToRefDate)) & "" & vbCrLf & " AND EMP_CODE ='" & mEmpCode & "'"

            SqlStr = SqlStr & vbCrLf & "AND SECONDHALF =" & pLeaveCode & ""

            If IsDate(pFromRefDate) = True Then
                SqlStr = SqlStr & vbCrLf & "AND ATTN_DATE>=TO_DATE('" & VB6.Format(pFromRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            SqlStr = SqlStr & vbCrLf & "AND ATTN_DATE<=TO_DATE('" & VB6.Format(pToRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

            If RsOpLeave.EOF = False Then
                GetLeavesAvail = GetLeavesAvail + IIf(IsDBNull(RsOpLeave.Fields("CNTLEAVE").Value), 0, RsOpLeave.Fields("CNTLEAVE").Value)
            End If
            GetLeavesAvail = GetLeavesAvail / 2
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Public Function CalcBalLeaves(ByRef mCode As String, ByRef xSalDate As String, ByRef pDBCn As ADODB.Connection, ByRef pBalEL As Double, ByRef pBalCL As Double, ByRef pBalSL As Double, ByRef pBalCPL As Double) As Double

        On Error GoTo ErrPart
        Dim RsOpLeave As ADODB.Recordset
        Dim RsLeave As ADODB.Recordset
        Dim mCPL As Double
        Dim mEL As Double
        Dim mCL As Double
        Dim mSL As Double
        Dim mFHalf As Double
        Dim mSHalf As Double
        Dim SqlStr As String = ""
        Dim mMonth As Integer
        Dim mPeriod As Double

        Dim mDOJ As String
        Dim xMonth As Integer

        Dim mStartDate As String
        Dim mYearDays As Integer
        Dim mEndDate As String
        Dim mEmpCat As String
        Dim mOnlyELCarry As String
        Dim mCPLCountFrom As String
        Dim mFYear As Integer
        Dim pPaidEL As Double
        Dim mEmpType As String

        mOnlyELCarry = "N"
        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOJ = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CAT_TYPE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mEmpType = IIf(MasterNo = "1", "S", "W")
        End If

        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            mFYear = Year(CDate(xSalDate))

            If mFYear >= 2014 Then
                mOnlyELCarry = "Y"
            Else
                If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "IS_EL_CARRY", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOnlyELCarry = MasterNo
                End If
            End If
        ElseIf RsCompany.Fields("COMPANY_CODE").Value = 27 Then
            mOnlyELCarry = "Y"
        End If

        '    If mCode = "000782" Then
        '        mEmpCat = "D"
        '    End If

        '11-01-2007
        '    If Year(xSalDate) = Year(mDOJ) Then
        '        If Month(xSalDate) = Month(mDOJ) Then
        '            mPeriod = 1
        '        Else
        '            xMonth = Month(xSalDate) - Month(mDOJ)
        '            mPeriod = Round(Val(xMonth) / (12 - Month(mDOJ)), 2)
        '        End If
        '    Else
        '        mPeriod = Round(Val(Month(xSalDate)) / 12, 2)
        '    End If

        If Year(CDate(xSalDate)) = Year(CDate(mDOJ)) Then
            mStartDate = mDOJ
        Else
            mStartDate = "01/01/" & VB6.Format(xSalDate, "YYYY")
        End If

        mEndDate = "31/12/" & VB6.Format(xSalDate, "YYYY")

        mYearDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate("01/01/" & VB6.Format(xSalDate, "YYYY")), CDate("31/12/" & VB6.Format(xSalDate, "YYYY"))) + 1

        '    If CDate(mEndDate) = CDate(xSalDate) Then
        '        mPeriod = 1
        '    Else
        mPeriod = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(xSalDate)) + 1
        mPeriod = CDbl(VB6.Format(mPeriod / mYearDays, "0.00000"))
        '    End If

        mEL = GETEntitleEarnLeave(pDBCn, mCode, EARN, xSalDate)

        SqlStr = " SELECT NVL(OPENING,0) AS OPENING , NVL(TOTENTITLE,0) AS TOTENTITLE, LEAVECODE " & vbCrLf & " FROM PAY_OPLEAVE_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(xSalDate)) & " " & vbCrLf & " AND EMP_CODE ='" & mCode & "'"

        MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOpLeave.EOF = False Then
            Do While RsOpLeave.EOF = False
                If RsOpLeave.Fields("LeaveCode").Value = EARN Then
                    mEL = mEL + IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                ElseIf RsOpLeave.Fields("LeaveCode").Value = SICK Then
                    mSL = IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                    '                mSL = mSL + (IIf(IsNull(RsOpLeave!TOTENTITLE), 0, RsOpLeave!TOTENTITLE) * mPeriod)
                    If mEmpCat = "D" Then
                        mSL = 0
                    Else
                        mSL = mSL + (GetLeaveEntitle(Val(RsOpLeave.Fields("LeaveCode").Value), mCode, xSalDate) * mPeriod)
                        '                mSL = PaiseRound(mSL, 0.5)
                        mSL = System.Math.Round(mSL * 2, 0) / 2
                    End If
                ElseIf RsOpLeave.Fields("LeaveCode").Value = CASUAL Then
                    mCL = IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                    '                mCL = mCL + (IIf(IsNull(RsOpLeave!TOTENTITLE), 0, RsOpLeave!TOTENTITLE) * mPeriod)
                    If mEmpCat = "D" Then
                        mCL = 0
                    Else
                        mCL = mCL + (GetLeaveEntitle(Val(RsOpLeave.Fields("LeaveCode").Value), mCode, xSalDate) * mPeriod)
                        '                mCL = PaiseRound(mCL, 0.5)
                        mCL = System.Math.Round(mCL * 2, 0) / 2
                    End If
                ElseIf RsOpLeave.Fields("LeaveCode").Value = CPLEARN Then
                    mCPL = IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                    '                mCPL = mCPL + (IIf(IsNull(RsOpLeave!TOTENTITLE), 0, RsOpLeave!TOTENTITLE) * mPeriod)
                    '                mCPL = PaiseRound(mCPL, 0.5)
                    mCPL = System.Math.Round(mCPL * 2, 0) / 2
                End If
                RsOpLeave.MoveNext()
            Loop
        End If

        mCPL = mCPL + GETCPL(pDBCn, mCode, xSalDate)
        mCPL = mCPL - GETPaidCPL(pDBCn, mCode, xSalDate) '02-4-2013

        If RsCompany.Fields("COMPANY_CODE").Value = 12 And mEmpType = "W" Then
            mCPLCountFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -180, CDate(xSalDate)))
        Else
            mCPLCountFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -120, CDate(xSalDate)))
        End If

        SqlStr = " SELECT FIRSTHALF, SECONDHALF,ATTN_DATE,CPL_AGT_DATE_FH,CPL_AGT_DATE_SH " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(xSalDate)) & " " & vbCrLf & " AND EMP_CODE ='" & mCode & "' AND ATTN_DATE<=TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLeave.EOF = False Then
            Do While RsLeave.EOF = False
                If RsLeave.Fields("FIRSTHALF").Value = EARN Then
                    mEL = mEL - 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = SICK Then
                    mSL = mSL - 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = CASUAL Then
                    mCL = mCL - 0.5
                End If

                If RsLeave.Fields("SECONDHALF").Value = EARN Then
                    mEL = mEL - 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mSL = mSL - 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                    mCL = mCL - 0.5
                End If
                RsLeave.MoveNext()
            Loop
        End If

        '', SECONDHALF,ATTN_DATE,CPL_AGT_DATE_FH,CPL_AGT_DATE_SH

        SqlStr = " SELECT FIRSTHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "' AND FIRSTHALF = " & CPLAVAIL & "" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND CPL_AGT_DATE_FH>=TO_DATE('" & VB6.Format(mCPLCountFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLeave.EOF = False Then
            Do While RsLeave.EOF = False
                mCPL = mCPL - 0.5
                RsLeave.MoveNext()
            Loop
        End If

        '', ,ATTN_DATE,CPL_AGT_DATE_FH,

        SqlStr = " SELECT SECONDHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "' AND SECONDHALF = " & CPLAVAIL & "" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND CPL_AGT_DATE_SH>=TO_DATE('" & VB6.Format(mCPLCountFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLeave.EOF = False Then
            Do While RsLeave.EOF = False
                mCPL = mCPL - 0.5
                RsLeave.MoveNext()
            Loop
        End If
        pPaidEL = GetPaidEL(mCode, xSalDate, pDBCn, "", "")
        mEL = mEL - pPaidEL

        pBalEL = PaiseRound(System.Math.Abs(mEL), 0.5) * IIf(mEL < 0, -1, 1)
        pBalCL = PaiseRound(System.Math.Abs(mCL), 0.5) * IIf(mCL < 0, -1, 1)
        pBalSL = PaiseRound(System.Math.Abs(mSL), 0.5) * IIf(mSL < 0, -1, 1)
        pBalCPL = PaiseRound(System.Math.Abs(mCPL), 0.5) * IIf(mCPL < 0, -1, 1)

        '    If RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 2 Then
        If mOnlyELCarry = "Y" Then
            CalcBalLeaves = pBalEL
        Else
            '        If RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 2 Then  ''12/01/2018
            '            CalcBalLeaves = (pBalEL + pBalCL + pBalSL + pBalCPL)
            '        Else
            CalcBalLeaves = (pBalEL + pBalCL + pBalSL)
            '        End If
        End If
        '    Else
        '        CalcBalLeaves = (pBalEL + pBalCL + pBalSL)
        '    End If

        Exit Function
ErrPart:
        CalcBalLeaves = 0
    End Function
    Public Function GetPaidEL(ByRef mCode As String, ByRef xSalDate As String, ByRef pDBCn As ADODB.Connection, ByRef pFromDate As String, ByRef pToDate As String) As Double

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPayYear As Integer
        Dim SqlStr As String = ""

        mPayYear = CInt(VB6.Format(xSalDate, "YYYY"))
        GetPaidEL = 0

        SqlStr = " SELECT SUM(PAID_LEAVEDAYS) AS PAID_LEAVEDAYS " & vbCrLf & " FROM PAY_LEAVEPAID_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "'" & vbCrLf & " AND PAYYEAR=" & mPayYear & ""

        If pFromDate <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND PAID_DATE>=TO_DATE('" & VB6.Format(pFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND PAID_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            If pToDate <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND PAID_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetPaidEL = IIf(IsDBNull(RsTemp.Fields("PAID_LEAVEDAYS").Value), 0, RsTemp.Fields("PAID_LEAVEDAYS").Value)
        End If

        Exit Function
ErrPart:
        GetPaidEL = 0
    End Function
    Public Function CalcPuneEL(ByRef mCode As String, ByRef xSalDate As String, ByRef pDBCn As ADODB.Connection) As Double
        On Error GoTo ErrPart
        Dim RsOpLeave As ADODB.Recordset
        Dim RsLeave As ADODB.Recordset
        Dim mELEntitle As Double
        Dim mPeriod As Double
        Dim mDOJ As String
        Dim mStartDate As String
        Dim mYearDays As Integer
        Dim mEndDate As String


        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOJ = MasterNo
        End If

        If Year(CDate(xSalDate)) = Year(CDate(mDOJ)) Then
            mStartDate = mDOJ
        Else
            mStartDate = "01/01/" & VB6.Format(xSalDate, "YYYY")
        End If

        mEndDate = "31/12/" & VB6.Format(xSalDate, "YYYY")

        mYearDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate("01/01/" & VB6.Format(xSalDate, "YYYY")), CDate("31/12/" & VB6.Format(xSalDate, "YYYY"))) + 1


        mPeriod = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(xSalDate)) + 1
        mPeriod = CDbl(VB6.Format(mPeriod / mYearDays, "0.00000"))

        mELEntitle = CDbl(VB6.Format(24 * mPeriod, "0.0"))

        CalcPuneEL = Int(mELEntitle) + IIf((mELEntitle - Int(mELEntitle)) >= 0.5, 0.5, 0)


        Exit Function
ErrPart:
        CalcPuneEL = 0
    End Function
    Public Function GETPaidCPL(ByRef pDBCn As ADODB.Connection, ByRef pEmpCode As String, ByRef pRunDate As String) As Double

        On Error GoTo ErrPart
        Dim RsBalEL As ADODB.Recordset
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim xRunDate As String
        Dim SqlStr As String = ""

        GETPaidCPL = 0

        xRunDate = VB6.Format(pRunDate, "DD/MM/YYYY")

        SqlStr = " SELECT SUM(CPLPAIDDAYS) AS CPLPAIDDAYS " & vbCrLf & " FROM PAY_CPL_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(xRunDate)) & " " & vbCrLf & " AND EMP_CODE ='" & pEmpCode & "'" & vbCrLf & " AND TO_CHAR(PAID_MONTH,'YYYYMM')<='" & VB6.Format(xRunDate, "YYYYMM") & "'"

        MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalEL, ADODB.LockTypeEnum.adLockOptimistic)

        If RsBalEL.EOF = False Then
            GETPaidCPL = IIf(IsDBNull(RsBalEL.Fields("CPLPAIDDAYS").Value), 0, RsBalEL.Fields("CPLPAIDDAYS").Value)
        Else
            GETPaidCPL = 0
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Module
