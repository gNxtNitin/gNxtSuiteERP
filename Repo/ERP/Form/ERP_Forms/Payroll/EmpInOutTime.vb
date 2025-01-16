Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmpInOutTime
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColDate As Short = 1
    Private Const ColCode As Short = 2
    Private Const ColName As Short = 3
    Private Const ColFName As Short = 4
    Private Const ColDept As Short = 5
    Private Const ColBookNo As Short = 6
    Private Const ColPageNo As Short = 7
    Private Const ColIN As Short = 8
    Private Const ColOUT As Short = 9
    Private Const ColTotHours As Short = 10
    Private Const ColWorkHours As Short = 11
    Private Const ColOTHours As Short = 12
    Private Const ColPunchData As Short = 13

    'Private Const ColODFrom = 13
    'Private Const ColODTo = 14
    Dim mCurrRow As Integer
    Dim mSearchKey As String

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        cmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
        CmdSave.Enabled = Not mPrintEnable
    End Sub
    Private Function CalcBSalary(ByRef mCode As String, ByRef mISBasicSalary As String) As Double

        On Error GoTo ERR1
        Dim RSSalDef As ADODB.Recordset
        Dim mCheckDate As String

        CalcBSalary = 0

        mCheckDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mCheckDate = VB6.Format(mCheckDate, "DD/MM/YYYY")

        SqlStr = " SELECT BASICSALARY from PAY_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) From PAY_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE<= TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalDef, ADODB.LockTypeEnum.adLockOptimistic)

        If RSSalDef.EOF = False Then
            CalcBSalary = IIf(IsDbNull(RSSalDef.Fields("BASICSALARY").Value), 0, RSSalDef.Fields("BASICSALARY").Value)
        End If

        If mISBasicSalary = "N" Then
            SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALDEF, PAY_SALARYHEAD_MST SMAST " & vbCrLf & " WHERE SALDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALDEF.COMPANY_CODE=SMAST.COMPANY_CODE " & vbCrLf & " AND SALDEF.ADD_DEDUCTCODE=SMAST.CODE " & vbCrLf & " AND SMAST.ADDDEDUCT=" & ConEarning & " " & vbCrLf & " AND SALDEF.EMP_CODE='" & mCode & "'" & vbCrLf & " AND SALDEF.SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) From PAY_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE<= TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalDef, ADODB.LockTypeEnum.adLockOptimistic)

            If RSSalDef.EOF = False Then
                CalcBSalary = CalcBSalary + IIf(IsDbNull(RSSalDef.Fields("Amount").Value), 0, RSSalDef.Fields("Amount").Value)
            End If
        End If

        CalcBSalary = MainClass.FormatRupees(CalcBSalary)

        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        'MainClass.ClearGrid(SprdMain)

        With SprdMain
            .MaxCols = ColPunchData

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColDate
            .Text = "Date"

            .Col = ColCode
            .Text = "Emp Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColFName
            .Text = "Father's Name "
            .ColsFrozen = ColFName

            .ColsFrozen = ColName

            .Col = ColDept
            .Text = "Dept"

            .Col = ColBookNo
            .Text = "Book No"

            .Col = ColPageNo
            .Text = "Page No"

            .Col = ColIN
            .Text = "IN Time"

            .Col = ColOUT
            .Text = "OUT Time"

            .Col = ColTotHours
            .Text = "Total Hours"

            .Col = ColWorkHours
            .Text = "Work Hours"

            .Col = ColOTHours
            .Text = "OT Hours"

            .Col = ColPunchData
            .Text = "Punch Data"

            '        .Col = ColODFrom
            '        .Text = "OD From"
            '
            '        .Col = ColODTo
            '        .Text = "OD To"

        End With
    End Sub
    Private Function GetBackAttnData() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCount As Double

        GetBackAttnData = False

        SqlStr = " SELECT COUNT(1) AS CNTREC " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND ATTN_DATE>TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TOT_HOURS<>0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mCount = IIf(IsDbNull(RsTemp.Fields("CNTREC").Value), 0, RsTemp.Fields("CNTREC").Value)
            If mCount > 0 Then
                GetBackAttnData = True
                Exit Function
            End If
        End If
        Exit Function
ErrPart:
        GetBackAttnData = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cboCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub
    Private Sub cboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboDept_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub
    Private Sub chkDivision_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDivision.CheckStateChanged
        If chkDivision.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDivision.Enabled = False
        Else
            cboDivision.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub
    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub

    Private Sub chkBookNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBookNo.CheckStateChanged
        CmdSave.Enabled = True
        If chkBookNo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtBookNo.Enabled = False
        Else
            txtBookNo.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub
    Private Sub chkPageNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPageNo.CheckStateChanged
        CmdSave.Enabled = True
        If chkPageNo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPageNo.Enabled = False
        Else
            txtPageNo.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub

    Private Sub chkPunchData_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPunchData.CheckStateChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mCode As String
        Dim mGSalary As Double
        Dim mDate As String
        Dim mInTime As String
        Dim mOutTime As String
        Dim mTOTHours As Double
        Dim mWorksHours As Double
        Dim mOTHours As Double

        SqlStr = ""
        PubDBCn.BeginTrans()

        For cntRow = 1 To SprdMain.MaxRows

            SprdMain.Row = cntRow
            SprdMain.Col = ColCode
            mCode = Trim(SprdMain.Text)

            If mCode <> "" Then
                SprdMain.Row = cntRow
                SprdMain.Col = ColDate
                mDate = Trim(SprdMain.Text)

                CalcTotatHours(cntRow, mDate)

                SprdMain.Row = cntRow

                SprdMain.Col = ColCode
                mCode = Trim(SprdMain.Text)

                SprdMain.Col = ColIN
                mInTime = VB6.Format(SprdMain.Text, "hh:mm")

                SprdMain.Col = ColOUT
                mOutTime = VB6.Format(SprdMain.Text, "hh:mm")

                If CDate(mInTime) <= CDate(mOutTime) Then
                    mOutTime = VB6.Format(DateSerial(Year(CDate(mDate)), Month(CDate(mDate)), VB.Day(CDate(mDate))) & " " & TimeSerial(Hour(CDate(mOutTime)), Minute(CDate(mOutTime)), 0), "DD/MM/YYYY HH:MM")
                Else
                    mOutTime = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate)))) & " " & TimeSerial(Hour(CDate(mOutTime)), Minute(CDate(mOutTime)), 0), "DD/MM/YYYY HH:MM")
                End If
                mInTime = VB6.Format(DateSerial(Year(CDate(mDate)), Month(CDate(mDate)), VB.Day(CDate(mDate))) & " " & TimeSerial(Hour(CDate(mInTime)), Minute(CDate(mInTime)), 0), "DD/MM/YYYY HH:MM")

                SprdMain.Col = ColTotHours
                mTOTHours = Val(VB.Left(SprdMain.Text, 2)) + (CDbl(VB.Right(SprdMain.Text, 2)) / 60)


                SprdMain.Col = ColWorkHours
                mWorksHours = Val(VB.Left(SprdMain.Text, 2)) + (CDbl(VB.Right(SprdMain.Text, 2)) / 60)

                SprdMain.Col = ColOTHours
                mOTHours = Val(VB.Left(SprdMain.Text, 2)) + (CDbl(VB.Right(SprdMain.Text, 2)) / 60)

                If mCode <> "" Then
                    SqlStr = " DELETE FROM PAY_DALIY_ATTN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mCode & "'"

                    PubDBCn.Execute(SqlStr)
                    '                If Val(mTOTHours) <> 0 Then
                    SqlStr = " INSERT INTO PAY_DALIY_ATTN_TRN ( " & vbCrLf & " COMPANY_CODE, PAYYEAR, " & vbCrLf & " EMP_CODE, ATTN_DATE, " & vbCrLf & " IN_TIME, OUT_TIME, TOT_HOURS," & vbCrLf & " WORKS_HOURS, OT_HOURS," & vbCrLf & " ADDUSER, ADDDATE ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(mDate)) & ", " & vbCrLf & " '" & mCode & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TO_DATE('" & VB6.Format(mInTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & VB6.Format(mOutTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), " & mTOTHours & ", " & vbCrLf & " " & mWorksHours & ", " & mOTHours & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                    PubDBCn.Execute(SqlStr)
                    '                End If
                    If UpdateLeave(Trim(mCode), mDate) = False Then GoTo UpdateError
                End If
            End If
        Next
        PubDBCn.CommitTrans()

        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        '    Resume
        PubDBCn.Errors.Clear()
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateLeave(ByRef mCode As String, ByRef xDate As String) As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String = ""

        Dim pFHalf As String
        Dim pSHalf As String
        Dim mEmpShiftBreak As String

        Dim pFHalfPresent As Integer
        Dim pSHalfPresent As Integer

        Dim mShiftInTime As String
        Dim mShiftOutTime As String

        Dim mIsRoundClock As Boolean
        Dim mIsPrevRoundClock As Boolean

        Dim mInTime As String
        Dim mOutTime As String
        Dim mFirstIsO As Boolean
        Dim mSecondIsOD As Boolean
        Dim mPresentMirginIn As String
        Dim mPresentMirginOut As String

        If CheckEmpTime(mCode, xDate, mInTime, mOutTime, IIf(mIsRoundClock = True, "Y", "N"), mFirstIsO, mSecondIsOD, mEmpShiftBreak) = False Then GoTo UpdateError

        If CDate(VB6.Format(mInTime, "HH:MM")) = CDate("00:00") Or CDate(VB6.Format(mOutTime, "HH:MM")) = CDate("00:00") Then
            UpdateLeave = True
            Exit Function
        End If

        mShiftInTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 5, "I", "E")
        mShiftOutTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 5, "O", "E")
        mIsRoundClock = GetRoundClock(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), "E")
        mIsPrevRoundClock = GetRoundClock(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(xDate, "DD-MMM-YYYY")))), "E")

        mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 4, CDate(mShiftInTime)), "DD/MM/YYYY HH:MM")))
        mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, 30, CDate(mEmpShiftBreak)), "DD/MM/YYYY HH:MM")))

        mPresentMirginIn = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 120, "I", "E")
        mPresentMirginOut = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 120, "O", "E")



        mFirstIsO = False
        mSecondIsOD = False
        pFHalf = ""
        pSHalf = ""

        If VB6.Format(mInTime, "HH:MM") = "00:00" Or VB6.Format(mOutTime, "HH:MM") = "00:00" Then

        Else
            If CDate(VB6.Format(mInTime, "HH:MM")) <= CDate(mPresentMirginIn) And CDate(VB6.Format(mOutTime, "HH:MM")) >= CDate(mEmpShiftBreak) Then
                pFHalf = "P"
            End If

            If CDate(VB6.Format(mInTime, "HH:MM")) <= CDate(mEmpShiftBreak) And CDate(VB6.Format(mOutTime, "HH:MM")) >= CDate(mPresentMirginOut) Then
                pSHalf = "P"
            End If


            If pFHalf = "P" Or pSHalf = "P" Then
                If UpdateEmpPresent(mCode, xDate, pFHalf, pSHalf, PubDBCn) = False Then GoTo UpdateError
            End If
        End If

        UpdateLeave = True
        Exit Function
UpdateError:
        UpdateLeave = False
    End Function


    Private Function CheckEmpTime(ByRef mEmpCode As String, ByRef mMonthDate As String, ByRef mEmpInTime As String, ByRef mEmpOutTime As String, ByRef mIsRound As String, ByRef mFirstIsOD As Boolean, ByRef mSecondIsOD As Boolean, ByRef mEmpShiftBreak As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mEMPODOut As String
        Dim mEmpODIn As String
        Dim mIsODLocal1 As Boolean
        Dim mIsODLocal2 As Boolean

        mEmpInTime = "00:00"
        mEmpOutTime = "00:00"

        mIsODLocal1 = False
        mIsODLocal2 = False
        mFirstIsOD = False
        mSecondIsOD = False

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
        Else
            mEmpInTime = "00:00"
            mEmpOutTime = "00:00"
        End If
        mEMPODOut = "00:00"
        mEmpODIn = "00:00"

        SqlStr = " SELECT MIN(TO_CHAR(TIME_FROM,'DD-MON-YYYY HH24:MI')) AS TIME_FROM " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields("TIME_FROM").Value) = False Then
                mIsODLocal1 = True
                mEMPODOut = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_FROM").Value), "00:00", RsTemp.Fields("TIME_FROM").Value), "HH:MM")
                mEMPODOut = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEMPODOut)), Minute(CDate(mEMPODOut)), 0), "DD/MM/YYYY HH:MM")
            End If
        End If

        If mIsRound = "Y" Then
            SqlStr = " SELECT MIN(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE='" & UCase(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)), "DD-MMM-YYYY")) & "'"

            SqlStr = SqlStr & vbCrLf & " AND TO_DATE(TIME_TO,'DD-MON-YYYY HH24:MI')<='" & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 8, CDate(mEmpInTime)), "DD-MMM-YYYY hh:MM") & "'"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDbNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mIsODLocal2 = True
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
                    mIsODLocal2 = True
                    mEmpODIn = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:MM")
                    mEmpODIn = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:MM")
                End If
            End If
        End If

        If VB6.Format(mEmpInTime, "HH:MM") = "00:00" And VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Then
            If mIsODLocal1 = True Then
                If VB6.Format(mEMPODOut, "HH:MM") <= VB6.Format(mEmpShiftBreak, "HH:MM") And VB6.Format(mEmpODIn, "HH:MM") <= VB6.Format(mEmpShiftBreak, "HH:MM") Then
                    mFirstIsOD = True
                    mEmpInTime = mEMPODOut
                Else
                    If VB6.Format(mEMPODOut, "HH:MM") <= VB6.Format(mEmpShiftBreak, "HH:MM") Then
                        mFirstIsOD = True
                        mEmpInTime = mEMPODOut
                    Else
                        mFirstIsOD = False
                    End If
                End If

                If VB6.Format(mEmpODIn, "HH:MM") > VB6.Format(mEmpShiftBreak, "HH:MM") Then
                    mSecondIsOD = True
                    mEmpOutTime = mEmpODIn
                Else
                    mSecondIsOD = False
                End If
            Else
                mFirstIsOD = False
            End If
        Else
            If VB6.Format(mEmpInTime, "HH:MM") = "00:00" Then
                mEmpInTime = mEMPODOut
                mFirstIsOD = IIf(mIsODLocal1 = True, True, False)
                mEmpInTime = IIf(mIsODLocal1 = True, mEMPODOut, mEmpInTime)
            Else
                If VB6.Format(mEMPODOut, "HH:MM") <> "00:00" Then
                    If CDate(mEMPODOut) < CDate(mEmpInTime) Then
                        mEmpInTime = mEMPODOut
                        mFirstIsOD = True
                    End If
                End If
            End If

            If VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Then
                mEmpOutTime = mEmpODIn
                mSecondIsOD = IIf(mIsODLocal2 = True, True, False)
                mEmpOutTime = IIf(mIsODLocal2 = True, mEmpODIn, mEmpOutTime)
            Else
                If VB6.Format(mEmpODIn, "HH:MM") <> "00:00" Then
                    If CDate(mEmpODIn) > CDate(mEmpOutTime) Then
                        mEmpOutTime = mEmpODIn
                        mSecondIsOD = True
                    End If
                End If
            End If
        End If

        '    If Format(mEmpInTime, "HH:MM") = "00:00" Then
        ''        mEmpInTime = mEMPODOut
        '        mFirstIsOD = IIf(mIsODLocal1 = True, True, False)
        '    End If
        '
        '    If Format(mEmpOutTime, "HH:MM") = "00:00" Then
        ''        mEmpOutTime = mEmpODIn
        '        mSecondIsOD = IIf(mIsODLocal2 = True, True, False)
        '    End If
        '
        '    If Format(mEMPODOut, "HH:MM") <> "00:00" Then
        '        If CVDate(mEMPODOut) < CVDate(mEmpInTime) Then
        ''            mEmpInTime = mEMPODOut
        '            mFirstIsOD = True
        '        End If
        '    End If
        '
        '    If Format(mEmpODIn, "HH:MM") <> "00:00" Then
        '        If CVDate(mEmpODIn) > CVDate(mEmpOutTime) Then
        ''            mEmpOutTime = mEmpODIn
        '            mSecondIsOD = True
        '        End If
        '    End If

        CheckEmpTime = True
        Exit Function
ErrPart:
        CheckEmpTime = False

    End Function

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler
        Dim mYM As Integer
        Dim mAuthorisation As String

        mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")
        If mAuthorisation = "N" Then
            If RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 16 Then

            Else
                If GetBackAttnData = True Then
                    MsgBox("You Cann't Change Back Entry. ", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If
        End If

        mYM = CInt(VB6.Format(Year(CDate(lblRunDate.Text)), "0000") & VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        If SalProcess(mYM) = False Then
            MsgBox("Salary Already Process for this Month, so you are unable to process. ", MsgBoxStyle.Critical)
            Exit Sub
        End If


        If Update1 = True Then
            Call RefreshScreen()
            CmdSave.Enabled = False
            Call PrintCommand(True)
        Else
            CmdSave.Enabled = True
            Call PrintCommand(False)
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForDeduction(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForDeduction(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String


        PubDBCn.Errors.Clear()


        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows, ColDate, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "Daily Attendance List "

        mSubTitle = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, "", " Dept : " & cboDept.Text)
        mSubTitle = mSubTitle & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Checked, "", " Category : " & cboCategory.Text)

        Call ShowReport(SqlStr, "DailyEmpAttn.Rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        'Resume
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
        Call ReportForDeduction(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(SprdMain)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        If optAll(1).Checked = True Then
            If txtEmpCode.Text = "" Then
                MsgInformation("Please select the Employee Code.")
                txtEmpCode.Focus()
                Exit Sub
            End If
        End If

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDivision.Text = "" Then
                If cboDivision.Enabled = True Then cboDivision.Focus()
                MsgInformation("Please Select Division.")
                Exit Sub
            End If
        End If


        RefreshScreen()
    End Sub
    Private Sub frmEmpInOutTime_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        Me.Text = "Daily Attendance Entry"
        '    If lblCategory.text = "G" Then
        '        Me.text = Me.Caption & " (General)"
        '    Else
        '        Me.text = Me.Caption & " (P. Rate)"
        '    End If
    End Sub

    Private Sub frmEmpInOutTime_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        lblRunDate.Text = VB6.Format(RunDate, "DD-MMMM-YYYY")
        optCardNo.Checked = True
        SetDate(CDate(lblRunDate.Text))
        FillHeading()
        FormatSprd(-1)
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblRunDate.Height) + 15)
        FillDeptCombo()
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False

        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False

        optAll(0).Checked = True
        txtEmpCode.Enabled = False
        cmdsearch.Enabled = False

        chkDivision.CheckState = System.Windows.Forms.CheckState.Checked
        cboDivision.Enabled = False

        chkMonth.Enabled = True
        chkMonth.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkPunchData.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub

    Private Sub frmEmpInOutTime_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdMain.Change
        cmdSave.Enabled = True
        Call PrintCommand(False)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles sprdMain.ClickEvent

        Dim cntSearchRow As Integer
        cntSearchRow = 1
        mCurrRow = 1
        If eventArgs.row = 0 And eventArgs.col = ColName Then
            mSearchKey = ""
            mSearchKey = InputBox("Enter Emp Name :", "Search", mSearchKey)
            MainClass.SearchIntoGrid(sprdMain, ColName, mSearchKey, mCurrRow)
            cntSearchRow = cntSearchRow + 1
            mCurrRow = mCurrRow + 1
            sprdMain.Focus()
        End If
        cmdSave.Enabled = True
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles sprdMain.KeyUpEvent


        If eventArgs.keyCode = System.Windows.Forms.Keys.F3 Then
            MainClass.SearchIntoGrid(sprdMain, ColName, mSearchKey, mCurrRow)
            mCurrRow = mCurrRow + 1
            sprdMain.Focus()
        End If
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim mTime As String
        Dim mDate As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColIN
                sprdMain.Row = sprdMain.ActiveRow

                sprdMain.Col = ColDate
                mDate = VB6.Format(sprdMain.Text, "DD/MM/YYYY")

                sprdMain.Col = ColIN
                If Trim(sprdMain.Text) <> "" And Trim(sprdMain.Text) <> "00:00" Then
                    mTime = sprdMain.Value
                    sprdMain.Value = mTime
                    '                If xBasicSalary = 0 Then
                    '                    MsgInformation "Basic Salary is not Defined."
                    '                    MainClass.SetFocusToCell sprdMain, sprdMain.ActiveRow, ColIN
                    '                    Cancel = True
                    '                    Exit Sub
                    '                End If
                End If
                CalcTotatHours((sprdMain.Row), mDate)
            Case ColOUT
                sprdMain.Row = sprdMain.ActiveRow

                sprdMain.Col = ColDate
                mDate = VB6.Format(sprdMain.Text, "DD/MM/YYYY")

                sprdMain.Col = ColOUT
                If Trim(sprdMain.Text) <> "" And Trim(sprdMain.Text) <> "00:00" Then
                    mTime = sprdMain.Value
                    sprdMain.Value = mTime
                    '                If xBasicSalary = 0 Then
                    '                    MsgInformation "Basic Salary is not Defined."
                    '                    MainClass.SetFocusToCell sprdMain, sprdMain.ActiveRow, ColOUT
                    '                    Cancel = True
                    '                    Exit Sub
                    '                End If
                End If
                CalcTotatHours((sprdMain.Row), mDate)
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles sprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim mDate As String

        Select Case sprdMain.ActiveCol
            Case ColIN
                sprdMain.Row = sprdMain.ActiveRow

                sprdMain.Col = ColDate
                mDate = VB6.Format(sprdMain.Text, "DD/MM/YYYY")

                sprdMain.Col = ColIN
                If Trim(sprdMain.Text) <> "" And Trim(sprdMain.Text) <> "00:00" Then
                    sprdMain.Text = VB6.Format(sprdMain.Text, "HH:MM")
                    '                If xBasicSalary = 0 Then
                    '                    MsgInformation "Basic Salary is not Defined."
                    '                    MainClass.SetFocusToCell sprdMain, sprdMain.ActiveRow, ColIN
                    '                    Cancel = True
                    '                    Exit Sub
                    '                End If
                End If
                CalcTotatHours((sprdMain.Row), mDate)
            Case ColOUT
                sprdMain.Row = sprdMain.ActiveRow

                sprdMain.Col = ColDate
                mDate = VB6.Format(sprdMain.Text, "DD/MM/YYYY")

                sprdMain.Col = ColOTHours
                If Trim(sprdMain.Text) <> "" And Trim(sprdMain.Text) <> "00:00" Then
                    sprdMain.Text = VB6.Format(sprdMain.Text, "HH:MM")
                    '                If xBasicSalary = 0 Then
                    '                    MsgInformation "Basic Salary is not Defined."
                    '                    MainClass.SetFocusToCell sprdMain, sprdMain.ActiveRow, ColOUT
                    '                    Cancel = True
                    '                    Exit Sub
                    '                End If
                End If
                CalcTotatHours((sprdMain.Row), mDate)
        End Select
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBookNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBookNo.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtBookNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBookNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged
        Call PrintCommand(False)
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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub

    Private Sub optAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optAll.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optAll.GetIndex(eventSender)
            Call PrintCommand(False)
            If optAll(0).Checked = True Then
                txtEmpCode.Enabled = False
                cmdsearch.Enabled = False
                chkMonth.Enabled = True '' IIf(PubSuperUser = "S", True, False)
                chkMonth.CheckState = System.Windows.Forms.CheckState.Unchecked
            ElseIf optAll(1).Checked = True Then
                txtEmpCode.Enabled = True
                cmdsearch.Enabled = True
                chkMonth.Enabled = True
            End If

        End If
    End Sub

    Private Sub txtPageNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPageNo.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtPageNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPageNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub UpDYear_DownClick()

        lblRunDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(lblRunDate.Text)), "DD-MMMM-YYYY")

        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdMain, -1)
        '' RefreshScreen
    End Sub
    Private Sub UpDYear_UpClick()

        lblRunDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(lblRunDate.Text)), "DD-MMMM-YYYY")

        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdMain, -1)
        ''RefreshScreen
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo ErrRefreshScreen

        Dim RsEmpSal As ADODB.Recordset
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mMonth As Short
        Dim mYear As Short
        Dim mYYMM As Integer
        Dim mDOJ As String
        Dim mDOL As String
        Dim mDeptName As String
        Dim mDeptCode As String
        Dim mBookNo As String
        Dim mCheckCond As Boolean
        Dim mSqlStr As String
        Dim mDivisionCode As Double

        Dim mCntDateFrom As Integer
        Dim mCntDateTo As Integer
        Dim CntDay As Integer
        Dim mDate As String

        '    mMonth = Month(lblRunDate.Caption)
        '    mYear = Year(lblRunDate.Caption)
        '    mYYMM = Val(Str(mYear) + Format(mMonth, "00"))
        '    mCheckCond = False

        If chkMonth.CheckState = System.Windows.Forms.CheckState.Checked Then
            mDOJ = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY") ''"01/" & vb6.Format(lblRunDate.Caption, "MM/YYYY")
            mDOL = "01/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
            mCntDateFrom = VB.Day(CDate(mDOL))
            mCntDateTo = VB.Day(CDate(mDOJ))
        Else
            mDOJ = VB6.Format(lblRunDate.Text, "DD/MM/YYYY") ''MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear
            mDOL = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")
            mCntDateFrom = VB.Day(CDate(mDOL))
            mCntDateTo = VB.Day(CDate(mDOJ))
        End If

        SqlStr = " SELECT '" & mDOJ & "' AS Attn_Date, EMP.EMP_CODE, EMP.EMP_NAME, EMP.EMP_FNAME, " & vbCrLf _
            & " DEPT.DEPT_DESC, SMST.BOOKNO, SMST.PAGENO, " & vbCrLf _
            & " GETEMP_ATTN(EMP.COMPANY_CODE, TO_DATE('" & VB6.Format(mDOJ, "DD/MMM/YYYY") & "','DD-MON-YYYY'), EMP.EMP_CODE, 'I') AS IN_TIME, " & vbCrLf _
            & " GETEMP_ATTN(EMP.COMPANY_CODE, TO_DATE('" & VB6.Format(mDOJ, "DD/MMM/YYYY") & "','DD-MON-YYYY'), EMP.EMP_CODE, 'O') AS OUT_TIME, " & vbCrLf _
            & " 0, 0, 0, " & vbCrLf _
            & " GETEMP_PUNCHDATA(EMP.COMPANY_CODE, TO_DATE('" & VB6.Format(mDOJ, "DD/MMM/YYYY") & "','DD-MON-YYYY'), EMP.EMP_CODE, 'O') AS PUNCH_TIME " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP, PAY_SHIFT_TRN SMST, PAY_DEPT_MST DEPT " & vbCrLf _
            & " WHERE EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And EMP.COMPANY_CODE = DEPT.COMPANY_CODE " & vbCrLf _
            & " And EMP.EMP_DEPT_CODE = DEPT.DEPT_CODE " & vbCrLf _
            & " And EMP.COMPANY_CODE = SMST.COMPANY_CODE(+) " & vbCrLf _
            & " And EMP.EMP_CODE = SMST.EMP_CODE(+) " & vbCrLf _
            & " And SMST.SHIFT_DATE(+) = '" & VB6.Format(mDOL, "DD-MMM-YYYY") & "'"

        SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP.EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP.EMP_LEAVE_DATE IS NULL) "

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(cboCategory.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
                SqlStr = SqlStr & vbCrLf & "AND EMP.DIV_CODE=" & mDivisionCode & ""
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND DEPT.DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(cboDept.Text)) & "' "
        End If


        If optAll(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpCode.Text)) & "' "
        End If

        If chkBookNo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtBookNo.Text) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND SMST.BOOKNO=" & Val(txtBookNo.Text) & ""
            End If
        End If

        If chkPageNo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtPageNo.Text) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND SMST.PAGENO=" & Val(txtPageNo.Text) & ""
            End If
        End If


        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME"
        ElseIf optCardNo.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by SMST.BOOKNO, SMST.PAGENO"
        End If

        MainClass.AssignDataInSprd8(SqlStr, sprdMain, StrConn, "Y")

        FillHeading()
        FormatSprd(-1)

        '        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpSal, ADODB.LockTypeEnum.adLockOptimistic)

        '        If RsEmpSal.EOF = False Then
        '            With SprdMain
        '                cntRow = 1
        '                .MaxRows = cntRow
        '                Do While Not RsEmpSal.EOF
        '                    For CntDay = mCntDateFrom To mCntDateTo
        '                        .Row = cntRow

        '                        .Col = ColCode
        '                        mCode = RsEmpSal.Fields("EMP_CODE").Value
        '                        .Text = CStr(mCode)

        '                        .Col = ColName
        '                        .Text = RsEmpSal.Fields("EMP_NAME").Value

        '                        .Col = ColFName
        '                        .Text = IIf(IsDbNull(RsEmpSal.Fields("EMP_FNAME").Value), "", RsEmpSal.Fields("EMP_FNAME").Value)

        '                        .Col = ColDept
        '                        mDeptCode = IIf(IsDbNull(RsEmpSal.Fields("EMP_DEPT_CODE").Value), "", RsEmpSal.Fields("EMP_DEPT_CODE").Value)
        '                        '                If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                        '                    mDeptName = MasterNo
        '                        '                Else
        '                        '                    mDeptName = ""
        '                        '                End If
        '                        .Text = mDeptCode

        '                        .Col = ColBookNo
        '                        .Text = CStr(IIf(IsDbNull(RsEmpSal.Fields("BOOKNO").Value), "0", RsEmpSal.Fields("BOOKNO").Value))

        '                        .Col = ColPageNo
        '                        .Text = CStr(IIf(IsDbNull(RsEmpSal.Fields("PageNo").Value), "0", RsEmpSal.Fields("PageNo").Value))

        '                        If CalcVariable(mCode, cntRow, CntDay) = False Then GoTo NextRow
        '                        cntRow = cntRow + 1
        '                        If CntDay < mCntDateTo Then
        '                            .MaxRows = .MaxRows + 1
        '                            '                        FormatSprd -1
        '                        End If
        '                    Next

        'NextRow:
        '                    cntRow = cntRow + 1
        '                    RsEmpSal.MoveNext()
        '                    If RsEmpSal.EOF = False Then
        '                        .MaxRows = .MaxRows + 1
        '                        '                    FormatSprd -1
        '                    End If
        '                Loop

        '                '             ColTotal sprdMain, ColTotHours, ColOTHours
        '                '            .Col = ColName
        '                '            .Row = .MaxRows
        '                '            .Text = "TOTAL :"

        '                FormatSprd(-1)

        '                '            MainClass.ProtectCell sprdMain, .MaxRows, .MaxRows, 0, .MaxCols
        '            End With
        '        End If

        With sprdMain
            For CntDay = 1 To .MaxRows
                sprdMain.Row = CntDay
                sprdMain.Col = ColDate
                mDate = VB6.Format(sprdMain.Text, "DD/MM/YYYY")
                CalcTotatHours(CntDay, mDate)
            Next
        End With

        cmdSave.Enabled = True
        Call PrintCommand(True)
        Exit Sub

ErrRefreshScreen:
        MsgInformation(Err.Description)
    End Sub
    Private Function CalcVariable(ByRef mCode As String, ByRef mRow As Integer, ByRef CntDay As Integer) As Boolean

        On Error GoTo ERR1
        Dim RSSalVar As ADODB.Recordset
        Dim cntCol As Integer
        Dim mHeadTitle As String
        Dim mDate As String


        CalcVariable = True
        mDate = VB6.Format(CntDay & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN TRN " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND EMP_Code='" & mCode & "'" & vbCrLf _
            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalVar, ADODB.LockTypeEnum.adLockOptimistic)

        If RSSalVar.EOF = False Then
            FormatSprd(mRow)
            sprdMain.Row = mRow
            sprdMain.Col = ColDate
            sprdMain.Text = VB6.Format(IIf(IsDBNull(RSSalVar.Fields("ATTN_DATE").Value), "", RSSalVar.Fields("ATTN_DATE").Value), "DD/MM/YYYY")

            sprdMain.Col = ColIN
            sprdMain.Text = VB6.Format(IIf(IsDBNull(RSSalVar.Fields("IN_TIME").Value), "", RSSalVar.Fields("IN_TIME").Value), "hh:mm")

            sprdMain.Col = ColOUT
            sprdMain.Text = VB6.Format(IIf(IsDBNull(RSSalVar.Fields("OUT_TIME").Value), "0", RSSalVar.Fields("OUT_TIME").Value), "hh:mm")

            CalcTotatHours(mRow, mDate)
        Else
            sprdMain.Row = mRow

            sprdMain.Col = ColDate
            sprdMain.Text = VB6.Format(mDate, "DD/MM/YYYY")

            sprdMain.Col = ColBookNo
            sprdMain.Text = "0.0"

            sprdMain.Col = ColPageNo
            sprdMain.Text = "0.0"

            sprdMain.Col = ColIN
            sprdMain.Text = "00:00"

            sprdMain.Col = ColOUT
            sprdMain.Text = "00:00"

            CalcTotatHours(mRow, mDate)
        End If

        If chkPunchData.CheckState = System.Windows.Forms.CheckState.Checked Then
            sprdMain.Row = mRow
            sprdMain.Col = ColPunchData
            sprdMain.Text = GetPunchTime(mCode, mDate, "PAY_EMPLOYEE_MST")
        End If

        Exit Function
ERR1:
        CalcVariable = False
    End Function
    Private Function GetPunchTime(ByRef mEmpCode As String, ByRef mDate As String, ByRef mTable As String) As String

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim mTableName As String

        GetPunchTime = ""


        mTableName = "TEMPDATA"


        SqlStr = " SELECT DISTINCT EMP.EMP_NAME, EMP.EMP_CODE, " & vbCrLf _
            & " EMP.EMP_DEPT_CODE, TO_CHAR(OFFICEPUNCH,'DD-MON-YYYY HH24:MI') AS OFFICEPUNCH " & vbCrLf _
            & " FROM " & mTable & " EMP, " & mTableName & " SMST " & vbCrLf _
            & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND TRIM(EMP.EMP_CODE) = TRIM(SMST.CARDNO) " & vbCrLf _
            & " AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"


        SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"

        SqlStr = SqlStr & vbCrLf & "Order by OFFICEPUNCH"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            Do While Not RsAttn.EOF
                If GetPunchTime = "" Then
                    GetPunchTime = VB6.Format(IIf(IsDBNull(RsAttn.Fields("OFFICEPUNCH").Value), "", RsAttn.Fields("OFFICEPUNCH").Value), "HH:MM")
                Else
                    GetPunchTime = GetPunchTime & ", " & VB6.Format(IIf(IsDBNull(RsAttn.Fields("OFFICEPUNCH").Value), "", RsAttn.Fields("OFFICEPUNCH").Value), "HH:MM")
                End If
                RsAttn.MoveNext()
            Loop
        End If
        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function CalcTotatHours(ByRef mRow As Integer, ByRef mDate As String) As Object
        On Error GoTo ERR1
        Dim mInDateTime As Date
        Dim mOutDateTime As Date
        Dim mTotDateTime As Date
        Dim mWorkHours As Date
        Dim mOTHours As Date
        Dim mSundayOTHours As Date
        Dim mBalHours As Date
        Dim mHour As Short
        Dim mMin As Short
        Dim mShiftInTime As Date
        Dim mShiftOutTime As Date
        Dim mMarginsMinute As Double
        Dim mCode As String
        Dim mIsHoliday As Boolean
        Dim mHolidayType As String

        With sprdMain
            mMarginsMinute = 0
            .Row = mRow
            .Col = ColCode
            mCode = Trim(.Text)

            .Col = ColIN
            If Trim(.Text) = "" Or Trim(.Text) = "00:00" Then GoTo CalcPart
            mInDateTime = CDate(VB6.Format(.Text, "hh:mm"))

            .Col = ColOUT
            If Trim(.Text) = "" Or Trim(.Text) = "00:00" Then GoTo CalcPart
            mOutDateTime = CDate(VB6.Format(.Text, "hh:mm"))

            mShiftInTime = CDate(GetShiftTime(mCode, VB6.Format(mDate, "DD/MM/YYYY"), mMarginsMinute, "I", "E"))
            mShiftOutTime = CDate(GetShiftTime(mCode, VB6.Format(mDate, "DD/MM/YYYY"), mMarginsMinute, "O", "E"))

            If GetTotatHours(mInDateTime, mOutDateTime, mInDateTime, mOutDateTime, mTotDateTime, mWorkHours, mOTHours, mSundayOTHours, mShiftInTime, mShiftOutTime, mDate, mCode) = False Then GoTo ERR1

CalcPart:

            '        If RsCompany.Fields("COMPANY_CODE").Value = 17 Or RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            mHolidayType = ""

            mIsHoliday = GetIsHolidays(VB6.Format(mDate, "DD/MM/YYYY"), mHolidayType, mCode, "", "N")


            If mIsHoliday = False Then
                mOTHours = mOTHours
            Else
                mOTHours = System.DateTime.FromOADate(mWorkHours.ToOADate + mOTHours.ToOADate)
            End If

            If mIsHoliday = False Then
                mWorkHours = mWorkHours
            Else
                mWorkHours = System.DateTime.FromOADate(0)
            End If
            '        End If

            .Col = ColTotHours
            .Text = VB6.Format(mTotDateTime, "HH:MM")

            .Col = ColWorkHours
            .Text = VB6.Format(mWorkHours, "HH:MM")

            .Col = ColOTHours
            .Text = VB6.Format(mOTHours, "HH:MM")

        End With

        Exit Function
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        Dim RS As ADODB.Recordset = Nothing

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


        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboDept.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboDept.SelectedIndex = 0

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

        Exit Sub

ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        '    Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        '    NewDate = Format(Tempdate, "dd/mm/yyyy")
        '    lblRunDate.text = NewDate

        lblRunDate.Text = VB6.Format(lblRunDate.Text, "DD-MMMM-YYYY")

        Daysinmonth = MainClass.LastDay(VB6.Format(lblRunDate.Text, "mm"), VB6.Format(lblRunDate.Text, "yyyy"))
    End Sub
    Private Function FillDataIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByRef mPaymentType As String) As Boolean
        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        FillDataIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        FillDataIntoPrintDummy = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub FormatSprd(ByRef mRow As Integer)

        Dim cntCol As Integer
        On Error GoTo ERR1
        With sprdMain
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)

            .set_ColWidth(ColSNO, 4)

            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDate, 6)
            .ColHidden = IIf(chkMonth.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColCode, 8)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColName, 25)

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColFName, 25)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColDept, 16)

            .Col = ColBookNo
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeNumberMax = CDbl("9999999")
            .TypeNumberMin = CDbl("-9999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBookNo, 4)
            .ColHidden = True

            .Col = ColPageNo
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeNumberMax = CDbl("9999999")
            .TypeNumberMin = CDbl("-9999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPageNo, 4)
            .ColHidden = True

            .Col = ColIN
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeTime
            .TypeTime24Hour = FPSpreadADO.TypeTime24HourConstants.TypeTime24Hour24HourClock
            .TypeTimeSeconds = False
            .TypeTimeSeparator = Asc(":")
            '        .TypeTimeMin = "000000"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            '        .TypeSpin = True
            .set_ColWidth(ColIN, 8)

            .Col = ColOUT
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeTime
            .TypeTime24Hour = FPSpreadADO.TypeTime24HourConstants.TypeTime24Hour24HourClock
            .TypeTimeSeconds = False
            .TypeTimeSeparator = Asc(":")
            '        .TypeTimeMin = "000000"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            ''        .TypeSpin = True
            .set_ColWidth(ColOUT, 8)

            .Col = ColTotHours
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeTime
            .TypeTime24Hour = FPSpreadADO.TypeTime24HourConstants.TypeTime24Hour24HourClock
            .TypeTimeSeconds = False
            .TypeTimeSeparator = Asc(":")
            '        .TypeTimeMin = "000000"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColTotHours, 8)
            '        .ColHidden = IIf(lblCategory.text = "G", False, True)

            .Col = ColWorkHours
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeTime
            .TypeTime24Hour = FPSpreadADO.TypeTime24HourConstants.TypeTime24Hour24HourClock
            .TypeTimeSeconds = False
            .TypeTimeSeparator = Asc(":")
            '        .TypeTimeMin = "000000"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColWorkHours, 8)

            .Col = ColOTHours
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeTime
            .TypeTime24Hour = FPSpreadADO.TypeTime24HourConstants.TypeTime24Hour24HourClock
            .TypeTimeSeconds = False
            .TypeTimeSeparator = Asc(":")
            '        .TypeTimeMin = "000000"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColOTHours, 8)
            '        .ColHidden = IIf(lblCategory.text = "G", False, True)

            .Col = ColPunchData
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColPunchData, 8)

        End With


        MainClass.ProtectCell(SprdMain, 0, SprdMain.MaxRows, 0, ColPunchData)

        '    MainClass.ProtectCell sprdMain, 0, sprdMain.MaxRows, ColTotHours, ColPunchData

        MainClass.SetSpreadColor(SprdMain, mRow)

        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    'Private Function SalProcess(mYM As Long) As Boolean
    'On Error GoTo ErrSalProcess
    'Dim RsMain As ADODB.Recordset
    '        SalProcess = True
    '        SqlStr = " SELECT EMP_CODE FROM PAY_CONT_SAL_TRN WHERE " & vbCrLf _
    ''                & " TO_CHAR(SAL_DATE,'YYYYMM') > " & mYM & "" & vbCrLf _
    ''                & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""
    '
    '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsMain, adLockOptimistic
    '
    '        If RsMain.EOF = False Then
    '            SalProcess = False
    '        End If
    'Exit Function
    'ErrSalProcess:
    '    SalProcess = False
    'End Function
End Class
