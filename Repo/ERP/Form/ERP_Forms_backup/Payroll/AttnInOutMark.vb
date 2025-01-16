Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmAttnInOutMark
    Inherits System.Windows.Forms.Form
    Dim RsEmp As ADODB.Recordset = Nothing

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection


    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Sub settextlength()
        On Error GoTo ERR1
        txtINTime.MaxLength = 5
        txtOUTTime.MaxLength = 5
        txtRemarks.MaxLength = 100

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub chkClear_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkClear.CheckStateChanged
        If chkClear.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtINTime.Text = ""
            txtOUTTime.Text = ""
            txtRemarks.Text = ""
            txtINTime.Enabled = False
            txtOUTTime.Enabled = False
        Else
            txtINTime.Enabled = True
            txtOUTTime.Enabled = True
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        On Error GoTo UpdateErr
        If FieldsVarification() = False Then Exit Sub


        Update1()


        Exit Sub
UpdateErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub frmAttnInOutMark_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Show1()
    End Sub
    Private Sub frmAttnInOutMark_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmAttnInOutMark_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim RsDept As ADODB.Recordset

        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Me.Height = VB6.TwipsToPixelsY(3465)
        'Me.Width = VB6.TwipsToPixelsX(4455)
        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)

        SqlStr = "SELECT SHIFT_CODE FROM PAY_SHIFT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)

        cboShift.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboShift.Items.Add(RsDept.Fields("SHIFT_CODE").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboShift.SelectedIndex = 0

        settextlength()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub cboShift_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShift.TextChanged
        FillShift()
    End Sub

    Private Sub cboShift_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShift.SelectedIndexChanged
        FillShift()
    End Sub


    Private Sub cboShift_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboShift.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        FillShift()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillShift()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mShiftCode As String

        txtINShift.Text = "00:00"
        txtOutShift.Text = "00:00"


        SqlStr = " SELECT SHIFT_CODE, SHIFT_DESC," & vbCrLf _
            & " FROM_TIME, TO_TIME," & vbCrLf _
            & " BS_TIME, BE_TIME " & vbCrLf _
            & " FROM PAY_SHIFT_MST " & vbCrLf _
            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SHIFT_CODE='" & MainClass.AllowSingleQuote((cboShift.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtINShift.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("FROM_TIME").Value), "00:00", RsTemp.Fields("FROM_TIME").Value), "hh:mm")
            txtOutShift.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TO_TIME").Value), "00:00", RsTemp.Fields("TO_TIME").Value), "hh:mm")
            txtBreakFrom.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BS_TIME").Value), "00:00", RsTemp.Fields("BS_TIME").Value), "hh:mm")
            txtBreakTo.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BE_TIME").Value), "00:00", RsTemp.Fields("BE_TIME").Value), "hh:mm")
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function MaxRefNo() As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMaxRef As Double

        SqlStr = "SELECT MAX(AUTO_KEY_NO) AS AUTO_KEY_NO FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MaxRefNo = 1
        Else
            mMaxRef = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_NO").Value), 0, RsTemp.Fields("AUTO_KEY_NO").Value)
            MaxRefNo = mMaxRef + 1
        End If

        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Function
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mRefNo As Long
        Dim mAddMode As Boolean
        Dim mFromTime As String
        Dim mToTime As String
        Dim mTotalTime As String
        Dim mRemarks As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mFromTime = VB6.Format(lblDate.Text & " " & txtINTime.Text, "DD/MM/YYYY HH:mm")
        mToTime = VB6.Format(lblDate.Text & " " & txtOUTTime.Text, "DD/MM/YYYY HH:mm")
        mTotalTime = CalcTotalHrs()

        If chkClear.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = " DELETE FROM PAY_MOVEMENT_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND REF_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND EMP_CODE='" & lblCode.Text & "' AND MOVE_TYPE='M'"

            PubDBCn.Execute(SqlStr)

            SqlStr = " DELETE FROM PAY_DALIY_ATTN_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ATTN_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND EMP_CODE='" & lblCode.Text & "'"

            PubDBCn.Execute(SqlStr)


            SqlStr = "DELETE FROM PAY_OVERTIME_MST  WHERE " & vbCrLf _
                        & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND EMP_CODE='" & lblCode.Text & "'" & vbCrLf _
                        & " AND OT_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            PubDBCn.Execute(SqlStr)


            SqlStr = "DELETE FROM PAY_ATTN_MST  WHERE " & vbCrLf _
                        & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND EMP_CODE='" & lblCode.Text & "'" & vbCrLf _
                        & " AND ATTN_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            PubDBCn.Execute(SqlStr)

            GoTo TransCommit
        End If

        SqlStr = "SELECT * FROM PAY_MOVEMENT_TRN" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & Trim(lblCode.Text) & "'" & vbCrLf _
            & " AND REF_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND MOVE_TYPE='M'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenForwardOnly, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


        If RsTemp.EOF = False Then
            mRefNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_NO").Value), 0, RsTemp.Fields("AUTO_KEY_NO").Value)
            mAddMode = False
        Else
            mRefNo = MaxRefNo()
            mAddMode = True
        End If

        mRemarks = IIf(txtRemarks.Text = "", "MANNUAL IN-OUT", txtRemarks.Text)

        If mAddMode = True Then
            SqlStr = " INSERT INTO PAY_MOVEMENT_TRN ( " & vbCrLf _
            & " COMPANY_CODE, AUTO_KEY_NO, " & vbCrLf _
            & " REF_DATE, EMP_CODE, " & vbCrLf _
            & " PLACE_VISIT, TIME_FROM, " & vbCrLf _
            & " TIME_TO, TOTAL_HRS, MOVE_TYPE," & vbCrLf _
            & " ATH_CODE, VISIT_FROM, VEHICLE_MODE, HR_APPROVAL, VISIT_DISTANCE, " & vbCrLf _
            & " ADDUSER, ADDDATE, MODUSER, MODDATE, AGT_LEAVE ) VALUES ( " & vbCrLf _
            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mRefNo)) & "," & vbCrLf _
            & " TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & Trim(lblCode.Text) & "'," & vbCrLf _
            & " '" & mRemarks & "', TO_DATE('" & VB6.Format(mFromTime, "DD-MMM-YYYY HH:mm") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
            & " TO_DATE('" & VB6.Format(mToTime, "DD-MMM-YYYY HH:mm") & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & VB6.Format(mTotalTime, "HH:mm") & "','HH24:MI')," & vbCrLf _
            & " 'M', '', 1, 1,'Y',0," & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " '','','N')"

        Else
            SqlStr = " UPDATE PAY_MOVEMENT_TRN SET AUTO_KEY_NO=" & Val(CStr(mRefNo)) & "," & vbCrLf _
                & " REF_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EMP_CODE='" & Trim(lblCode.Text) & "', " & vbCrLf _
                & " PLACE_VISIT='" & mRemarks & "', " & vbCrLf _
                & " TIME_FROM=TO_DATE('" & VB6.Format(mFromTime, "DD-MMM-YYYY HH:mm") & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                & " TIME_TO=TO_DATE('" & VB6.Format(mToTime, "DD-MMM-YYYY HH:mm") & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                & " TOTAL_HRS=TO_DATE('" & VB6.Format(mTotalTime, "HH:mm") & "','HH24:MI'), " & vbCrLf _
                & " MOVE_TYPE='M', " & vbCrLf _
                & " ATH_CODE='', " & vbCrLf _
                & " VISIT_FROM=1, " & vbCrLf _
                & " VEHICLE_MODE=1, HR_APPROVAL='Y', VISIT_DISTANCE=0," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " AGT_LEAVE='N'" & vbCrLf _
                & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_NO=" & Val(CStr(mRefNo)) & ""

        End If

        PubDBCn.Execute(SqlStr)


        If UpdateDailyAttnTrn(Trim(lblCode.Text), (lblDate.Text), CDate(mFromTime), CDate(mToTime)) = False Then GoTo UpdateError

        If UpdateLeave(Trim(lblCode.Text), lblDate.Text) = False Then GoTo UpdateError


TransCommit:
        PubDBCn.CommitTrans()
        Me.Hide()
        Me.Close()
        Me.Dispose()
        Exit Function
UpdateError:
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateDailyAttnTrn(ByRef mCode As String, ByRef mDate As String, ByRef mInTime As Date, ByRef mOutTime As Date) As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mGSalary As Double
        Dim SqlStr As String = ""
        Dim mTOTHours As Date
        Dim mWorksHours As Date
        Dim mOTHours As Date

        Dim mTOTHoursValue As Double
        Dim mWorksHoursValue As Double
        Dim mOTHoursValue As Double
        Dim mOTApp As String

        If CDate(mInTime) <= CDate(mOutTime) Then
            mOutTime = CDate(VB6.Format(DateSerial(Year(CDate(mDate)), Month(CDate(mDate)), VB.Day(CDate(mDate))) & " " & TimeSerial(Hour(mOutTime), Minute(mOutTime), 0), "DD/MM/YYYY HH:mm"))
        Else
            mOutTime = CDate(VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate)))) & " " & TimeSerial(Hour(mOutTime), Minute(mOutTime), 0), "DD/MM/YYYY HH:mm"))
        End If

        mInTime = CDate(VB6.Format(DateSerial(Year(CDate(mDate)), Month(CDate(mDate)), VB.Day(CDate(mDate))) & " " & TimeSerial(Hour(mInTime), Minute(mInTime), 0), "DD/MM/YYYY HH:mm"))


        'CalcTotatHours cntRow, mDate

        Call CalcTotatHours(mCode, mInTime, mOutTime, mDate, mTOTHours, mWorksHours, mOTHours)

        mTOTHours = CDate(VB6.Format(mTOTHours, "HH:mm"))
        mWorksHours = CDate(VB6.Format(mWorksHours, "HH:mm"))
        mOTHours = CDate(VB6.Format(mOTHours, "HH:mm"))

        mTOTHoursValue = Val(VB.Left(CStr(mTOTHours), 2)) + (CDbl(VB.Right(CStr(mTOTHours), 2)) / 60)

        mWorksHoursValue = Val(VB.Left(CStr(mWorksHours), 2)) + (CDbl(VB.Right(CStr(mWorksHours), 2)) / 60)
        mOTHoursValue = Val(VB.Left(CStr(mOTHours), 2)) + (CDbl(VB.Right(CStr(mOTHours), 2)) / 60)

        If mCode <> "" Then
            SqlStr = " DELETE FROM PAY_DALIY_ATTN_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND EMP_CODE='" & mCode & "'"

            PubDBCn.Execute(SqlStr)
            '                If Val(mTOTHours) <> 0 Then
            SqlStr = " INSERT INTO PAY_DALIY_ATTN_TRN ( " & vbCrLf & " COMPANY_CODE, PAYYEAR, " & vbCrLf _
                & " EMP_CODE, ATTN_DATE, " & vbCrLf _
                & " IN_TIME, OUT_TIME, TOT_HOURS," & vbCrLf _
                & " WORKS_HOURS, OT_HOURS," & vbCrLf _
                & " ADDUSER, ADDDATE ) VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(mDate)) & ", " & vbCrLf _
                & " '" & mCode & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mInTime, "DD-MMM-YYYY HH:mm") & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & VB6.Format(mOutTime, "DD-MMM-YYYY HH:mm") & "','DD-MON-YYYY HH24:MI'), " & mTOTHoursValue & ", " & vbCrLf _
                & " " & mWorksHoursValue & ", " & mOTHoursValue & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

            PubDBCn.Execute(SqlStr)
            '                End If

            If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "OVERTIME_APP", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND OVERTIME_APP='0'") = True Then
                mOTApp = False
            Else
                mOTApp = True
            End If

            If mOTApp = True Then
                SqlStr = "DELETE FROM PAY_OVERTIME_MST  WHERE " & vbCrLf _
                        & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND EMP_CODE='" & mCode & "'" & vbCrLf _
                        & " AND OT_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)

                SqlStr = "INSERT INTO PAY_OVERTIME_MST ( " & vbCrLf _
                        & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
                        & " OT_DATE, OTHOUR, OTMIN, OTTYPE," & vbCrLf _
                        & " PREV_OTHOUR,  PREV_OTMIN, " & vbCrLf _
                        & " ADDUSER, ADDDATE) VALUES (" & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Year(CDate(lblDate.Text)) & ", " & vbCrLf _
                        & " '" & lblCode.Text & "', " & vbCrLf _
                        & " TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & "  " & Val(VB.Left(CStr(mOTHours), 2)) & ", " & (CDbl(VB.Right(CStr(mOTHours), 2)) / 60) & ", '0'," & vbCrLf _
                        & "  0, 0, " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                PubDBCn.Execute(SqlStr)
            End If


        End If

            UpdateDailyAttnTrn = True
        Exit Function
UpdateError:
        UpdateDailyAttnTrn = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        '    Resume
        PubDBCn.Errors.Clear()
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function CalcTotatHours(ByRef mCode As String, ByRef mInDateTime As Date, ByRef mOutDateTime As Date, ByRef mDate As String, ByRef mTotDateTime As Date, ByRef mWorkHours As Date, ByRef mOTHours As Date) As Object
        On Error GoTo ERR1
        'Dim mInDateTime As Date
        'Dim mOutDateTime As Date

        Dim mBalHours As Date
        Dim mHour As Short
        Dim mMin As Short
        Dim mShiftInTime As Date
        Dim mShiftOutTime As Date
        Dim mMarginsMinute As Double
        Dim mSundayOTHours As Date
        Dim mISHoliday As Boolean
        Dim mHolidayType As String

        mShiftInTime = CDate(GetShiftTime(mCode, VB6.Format(mDate, "DD/MM/YYYY"), mMarginsMinute, "I", "E"))
        mShiftOutTime = CDate(GetShiftTime(mCode, VB6.Format(mDate, "DD/MM/YYYY"), mMarginsMinute, "O", "E"))

        If GetTotatHours(mInDateTime, mOutDateTime, mInDateTime, mOutDateTime, mTotDateTime, mWorkHours, mOTHours, mSundayOTHours, mShiftInTime, mShiftOutTime, mDate, mCode) = False Then GoTo ERR1

CalcPart:

        mHolidayType = ""
        mISHoliday = GetIsHolidays(VB6.Format(mDate, "DD/MM/YYYY"), mHolidayType, mCode, "", "N")

        If mISHoliday = False Then
            mOTHours = mOTHours
        Else
            mOTHours = System.DateTime.FromOADate(mWorkHours.ToOADate + mOTHours.ToOADate)
        End If

        If mISHoliday = False Then
            mWorkHours = mWorkHours
        Else
            mWorkHours = System.DateTime.FromOADate(0)
        End If




        Exit Function
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateLeave(ByRef mCode As String, ByRef xDate As String) As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String = ""

        Dim pFHalf As String
        Dim pSHalf As String
        Dim mEmpShiftBreak As String
        Dim xDayShiftBreak As String

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

        mShiftInTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 5, "I", "E")
        mShiftOutTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 5, "O", "E")
        mIsRoundClock = GetRoundClock(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), "E")
        mIsPrevRoundClock = GetRoundClock(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(xDate, "DD-MMM-YYYY")))), "E")

        xDayShiftBreak = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 0, "I", "E")
        xDayShiftBreak = VB6.Format(xDate & " " & xDayShiftBreak, "DD-MMM-YYYY  HH:mm")

        mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 4, CDate(xDayShiftBreak)), "DD/MM/YYYY HH:mm")))
        mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, 30, CDate(mEmpShiftBreak)), "DD/MM/YYYY HH:mm")))
        mFirstIsO = False
        mSecondIsOD = False
        If CheckEmpTime(mCode, xDate, mInTime, mOutTime, IIf(mIsRoundClock = True, "Y", "N"), mFirstIsO, mSecondIsOD, mEmpShiftBreak) = False Then GoTo UpdateError

        If CDate(VB6.Format(mInTime, "HH:mm")) <= CDate(mShiftInTime) And CDate(VB6.Format(mOutTime, "HH:mm")) >= CDate(VB6.Format(mEmpShiftBreak, "HH:mm")) Then 'VB6.Format(mEmpShiftBreak, "HH:mm")
            pFHalf = "P"
        End If

        If CDate(VB6.Format(mInTime, "HH:mm")) <= CDate(VB6.Format(mEmpShiftBreak, "HH:mm")) And CDate(VB6.Format(mOutTime, "HH:mm")) >= CDate(mShiftOutTime) Then
            pSHalf = "P"
        End If


        If pFHalf = "P" Or pSHalf = "P" Then
            If UpdateEmpPresent(mCode, xDate, pFHalf, pSHalf, PubDBCn) = False Then GoTo UpdateError
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

        mEmpInTime = ""
        mEmpOutTime = ""

        mIsODLocal1 = False
        mIsODLocal2 = False
        mFirstIsOD = False
        mSecondIsOD = False

        SqlStr = " SELECT IN_TIME, OUT_TIME " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN ATTN " & vbCrLf & " WHERE ATTN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ATTN.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mEmpInTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "HH:mm")
            mEmpOutTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "HH:mm")

            mEmpInTime = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpInTime)), Minute(CDate(mEmpInTime)), 0), "DD/MM/YYYY HH:mm")

            If mIsRound = "Y" Then
                mEmpOutTime = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:mm")
            Else
                mEmpOutTime = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:mm")
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
            If IsDBNull(RsTemp.Fields("TIME_FROM").Value) = False Then
                mIsODLocal1 = True
                mEMPODOut = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_FROM").Value), "00:00", RsTemp.Fields("TIME_FROM").Value), "HH:mm")
                mEMPODOut = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEMPODOut)), Minute(CDate(mEMPODOut)), 0), "DD/MM/YYYY HH:mm")
            End If
        End If

        If mIsRound = "Y" Then
            SqlStr = " SELECT MIN(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & UCase(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)), "DD-MMM-YYYY")) & "','DD-MON-YYYY')"

            '        SqlStr = SqlStr & vbCrLf & " AND TO_DATE(TIME_TO,'DD-MON-YYYY HH24:MI')<='" & VB6.Format(DateAdd("h", 8, mEmpInTime), "DD-MMM-YYYY HH:mm") & "'"

            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(TIME_TO,'YYYYMMDDHH24MI')<=TO_CHAR('" & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 8, CDate(mEmpInTime)), "YYYYMMDDhhMM") & "')"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDBNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mIsODLocal2 = True
                    mEmpODIn = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:mm")
                    mEmpODIn = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:mm")
                End If
            End If
        Else
            SqlStr = " SELECT MAX(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDBNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mIsODLocal2 = True
                    mEmpODIn = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:mm")
                    mEmpODIn = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:mm")
                End If
            End If
        End If

        If VB6.Format(mEmpInTime, "HH:mm") = "00:00" And VB6.Format(mEmpOutTime, "HH:mm") = "00:00" Then
            If mIsODLocal1 = True Then
                If VB6.Format(mEMPODOut, "HH:mm") <= VB6.Format(mEmpShiftBreak, "HH:mm") And VB6.Format(mEmpODIn, "HH:mm") <= VB6.Format(mEmpShiftBreak, "HH:mm") Then
                    mFirstIsOD = True
                    mEmpInTime = mEMPODOut
                Else
                    If VB6.Format(mEMPODOut, "HH:mm") <= VB6.Format(mEmpShiftBreak, "HH:mm") Then
                        mFirstIsOD = True
                        mEmpInTime = mEMPODOut
                    Else
                        mFirstIsOD = False
                    End If
                End If

                If VB6.Format(mEmpODIn, "HH:mm") > VB6.Format(mEmpShiftBreak, "HH:mm") Then
                    mSecondIsOD = True
                    mEmpOutTime = mEmpODIn
                Else
                    mSecondIsOD = False
                End If
            Else
                mFirstIsOD = False
            End If
        Else
            If VB6.Format(mEmpInTime, "HH:mm") = "00:00" Then
                mEmpInTime = mEMPODOut
                mFirstIsOD = IIf(mIsODLocal1 = True, True, False)
                mEmpInTime = IIf(mIsODLocal1 = True, mEMPODOut, mEmpInTime)
            Else
                If VB6.Format(mEMPODOut, "HH:mm") <> "00:00" Then
                    If CDate(mEMPODOut) < CDate(mEmpInTime) Then
                        mEmpInTime = mEMPODOut
                        mFirstIsOD = True
                    End If
                End If
            End If

            If VB6.Format(mEmpOutTime, "HH:mm") = "00:00" Then
                mEmpOutTime = mEmpODIn
                mSecondIsOD = IIf(mIsODLocal2 = True, True, False)
                mEmpOutTime = IIf(mIsODLocal2 = True, mEmpODIn, mEmpOutTime)
            Else
                If VB6.Format(mEmpODIn, "HH:mm") <> "00:00" Then
                    If CDate(mEmpODIn) > CDate(mEmpOutTime) Then
                        mEmpOutTime = mEmpODIn
                        mSecondIsOD = True
                    End If
                End If
            End If
        End If

        '    If Format(mEmpInTime, "HH:mm") = "00:00" Then
        ''        mEmpInTime = mEMPODOut
        '        mFirstIsOD = IIf(mIsODLocal1 = True, True, False)
        '    End If
        '
        '    If Format(mEmpOutTime, "HH:mm") = "00:00" Then
        ''        mEmpOutTime = mEmpODIn
        '        mSecondIsOD = IIf(mIsODLocal2 = True, True, False)
        '    End If
        '
        '    If Format(mEMPODOut, "HH:mm") <> "00:00" Then
        '        If CVDate(mEMPODOut) < CVDate(mEmpInTime) Then
        ''            mEmpInTime = mEMPODOut
        '            mFirstIsOD = True
        '        End If
        '    End If
        '
        '    If Format(mEmpODIn, "HH:mm") <> "00:00" Then
        '        If CVDate(mEmpODIn) > CVDate(mEmpOutTime) Then
        ''            mEmpOutTime = mEmpODIn
        '            mSecondIsOD = True
        '        End If
        '    End If

        CheckEmpTime = True
        Exit Function
ErrPart:
        '    Resume
        CheckEmpTime = False

    End Function
    Private Sub Show1()
        Try
            Dim RsAttn As ADODB.Recordset = Nothing
            Dim cntRow As Integer
            Dim mCode As String
            Dim mOTType As Short
            Dim RSSalVar As ADODB.Recordset = Nothing

            SqlStr = "SELECT * FROM PAY_MOVEMENT_TRN" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & Trim(lblCode.Text) & "'" & vbCrLf _
            & " AND REF_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND MOVE_TYPE='M'" & vbCrLf

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockReadOnly)

            If RsAttn.EOF = False Then
                txtINTime.Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("TIME_FROM").Value), "", RsAttn.Fields("TIME_FROM").Value), "HH:mm")
                txtOUTTime.Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("TIME_TO").Value), "", RsAttn.Fields("TIME_TO").Value), "HH:mm")
                txtRemarks.Text = IIf(IsDBNull(RsAttn.Fields("PLACE_VISIT").Value), "", RsAttn.Fields("PLACE_VISIT").Value) ''

            Else
                chkClear.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If




            SqlStr = " SELECT * " & vbCrLf _
            & " FROM PAY_SHIFT_TRN TRN " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND EMP_CODE='" & Trim(lblCode.Text) & "'" & vbCrLf _
            & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalVar, ADODB.LockTypeEnum.adLockOptimistic)

            If RSSalVar.EOF = False Then

                cboShift.Text = CStr(IIf(IsDBNull(RSSalVar.Fields("SHIFT_CODE").Value), "", RSSalVar.Fields("SHIFT_CODE").Value))
                txtINShift.Text = VB6.Format(IIf(IsDBNull(RSSalVar.Fields("IN_TIME").Value), "", RSSalVar.Fields("IN_TIME").Value), "hh:mm")
                txtOutShift.Text = VB6.Format(IIf(IsDBNull(RSSalVar.Fields("OUT_TIME").Value), "", RSSalVar.Fields("OUT_TIME").Value), "hh:mm")
                txtBreakFrom.Text = VB6.Format(IIf(IsDBNull(RSSalVar.Fields("B_START_TIME").Value), "", RSSalVar.Fields("B_START_TIME").Value), "hh:mm")
                txtBreakTo.Text = VB6.Format(IIf(IsDBNull(RSSalVar.Fields("B_END_TIME").Value), "", RSSalVar.Fields("B_END_TIME").Value), "hh:mm")
                chkRoundClock.CheckState = IIf(RSSalVar.Fields("ROUND_CLOCK").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function FieldsVarificationShift() As Boolean
        On Error GoTo ERR1

        FieldsVarificationShift = True


        If Trim(txtINShift.Text) = "" Then
            MsgInformation("Invaild In Time. Cannot Save")
            txtINShift.Focus()
            FieldsVarificationShift = False
            Exit Function
        End If


        If Not IsDate(txtINShift.Text) Then
            MsgInformation("Invaild In Time. Cannot Save")
            txtINShift.Focus()
            FieldsVarificationShift = False
            Exit Function
        End If

        If Trim(txtOutShift.Text) = "" Then
            MsgInformation("Invaild OUT Time. Cannot Save")
            txtOutShift.Focus()
            FieldsVarificationShift = False
            Exit Function
        End If

        If Not IsDate(txtOutShift.Text) Then
            MsgInformation("Invaild OUT Time. Cannot Save")
            txtOutShift.Focus()
            FieldsVarificationShift = False
            Exit Function
        End If

        If Trim(txtOutShift.Text) = "" Then
            MsgInformation("Invaild OUT Time. Cannot Save")
            txtOutShift.Focus()
            FieldsVarificationShift = False
            Exit Function
        End If

        If Not IsDate(txtOutShift.Text) Then
            MsgInformation("Invaild OUT Time. Cannot Save")
            txtOutShift.Focus()
            FieldsVarificationShift = False
            Exit Function
        End If

        If Trim(txtBreakFrom.Text) = "" Then
            MsgInformation("Invaild Break Time. Cannot Save")
            txtBreakFrom.Focus()
            FieldsVarificationShift = False
            Exit Function
        End If

        If Not IsDate(txtBreakFrom.Text) Then
            MsgInformation("Invaild Break Time. Cannot Save")
            txtBreakFrom.Focus()
            FieldsVarificationShift = False
            Exit Function
        End If


        If Trim(txtBreakTo.Text) = "" Then
            MsgInformation("Invaild Break To Time. Cannot Save")
            txtBreakTo.Focus()
            FieldsVarificationShift = False
            Exit Function
        End If

        If Not IsDate(txtBreakTo.Text) Then
            MsgInformation("Invaild Break Time. Cannot Save")
            txtBreakTo.Focus()
            FieldsVarificationShift = False
            Exit Function
        End If


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarificationShift = False
        'Resume
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1

        FieldsVarification = True
        If chkClear.CheckState = System.Windows.Forms.CheckState.Checked Then Exit Function

        If Trim(txtINTime.Text) = "" Then
            MsgInformation("Invaild In Time. Cannot Save")
            txtINTime.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If Not IsDate(txtINTime.Text) Then
            MsgInformation("Invaild In Time. Cannot Save")
            txtINTime.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtOUTTime.Text) = "" Then
            MsgInformation("Invaild OUT Time. Cannot Save")
            txtOUTTime.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtOUTTime.Text) Then
            MsgInformation("Invaild OUT Time. Cannot Save")
            txtOUTTime.Focus()
            FieldsVarification = False
            Exit Function
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        'Resume
    End Function
    Private Sub txtINTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtINTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtINTime.Text) = "" Or Trim(txtINTime.Text) = "__:__" Then GoTo EventExitSub
        If Not IsDate(txtINTime.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            txtINTime.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        'Call CalcTotalHrs()
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtOUTTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOUTTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtOUTTime.Text) = "" Or Trim(txtINTime.Text) = "__:__" Then GoTo EventExitSub

        If Not IsDate(txtOUTTime.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
        End If
        'Call CalcTotalHrs()
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function CalcTotalHrs() As String
        On Error GoTo ErrPart
        Dim mTotalHrs As String
        Dim mMin1 As Integer
        Dim mMin2 As Integer

        If Trim(txtINTime.Text) = "" Or Trim(txtINTime.Text) = "__:__" Or Trim(txtOUTTime.Text) = "" Or Trim(txtOUTTime.Text) = "__:__" Then Exit Function

        mMin1 = Hour(CDate(txtINTime.Text)) * 60 + Minute(CDate(txtINTime.Text))
        mMin2 = Hour(CDate(txtOUTTime.Text)) * 60 + Minute(CDate(txtOUTTime.Text))

        If mMin1 = 0 Or mMin2 = 0 Then Exit Function

        If CDate(txtINTime.Text) <= CDate(txtOUTTime.Text) Then
            mTotalHrs = VB6.Format(Int((mMin2 - mMin1) / 60), "00") & ":" & VB6.Format((mMin2 - mMin1) Mod 60, "00")
        Else
            mMin2 = mMin2 + (24 * 60)
            mTotalHrs = VB6.Format(Int((mMin2 - mMin1) / 60), "00") & ":" & VB6.Format((mMin2 - mMin1) Mod 60, "00")
        End If

        CalcTotalHrs = VB6.Format(mTotalHrs, "HH:mm")

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cmdShiftChange_Click(sender As Object, e As EventArgs) Handles cmdShiftChange.Click
        On Error GoTo UpdateErr
        If FieldsVarificationShift() = False Then Exit Sub
        UpdateShift1()
        Exit Sub
UpdateErr:
        MsgBox(Err.Description)
    End Sub
    Private Function UpdateShift1() As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        Dim mAddMode As Boolean
        Dim mCategory As String
        Dim mDept As String

        Dim xInTime As String
        Dim xOutTime As String
        Dim xBStart As String
        Dim xBEnd As String
        Dim mDate As String

        Dim mInTime As String
        Dim mOutTime As String
        Dim mBStart As String
        Dim mBEnd As String


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mdate = lblDate.Text

        mInTime = txtINShift.Text
        mOutTime = txtOutShift.Text
        mBStart = txtBreakFrom.Text
        mBEnd = txtBreakTo.Text

        xInTime = VB6.Format(mDate, "DD-MMM-YYYY") & " " & VB6.Format(mInTime, "HH:MM")

        If VB6.Format(mInTime, "HH:MM") < VB6.Format(mOutTime, "HH:MM") Then
            xOutTime = VB6.Format(mDate, "DD-MMM-YYYY") & " " & VB6.Format(mOutTime, "HH:MM")
        Else
            xOutTime = VB6.Format(DateAdd("d", 1, mDate), "DD-MMM-YYYY") & " " & VB6.Format(mOutTime, "HH:MM")
        End If

        xBStart = VB6.Format(mDate, "DD-MMM-YYYY") & " " & VB6.Format(mBStart, "HH:MM")
        xBEnd = VB6.Format(mDate, "DD-MMM-YYYY") & " " & VB6.Format(mBEnd, "HH:MM")


        SqlStr = "SELECT EMP_DEPT_CODE , EMP_CATG FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & Trim(lblCode.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenForwardOnly, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mDept = IIf(IsDBNull(RsTemp.Fields("EMP_DEPT_CODE").Value), "", RsTemp.Fields("EMP_DEPT_CODE").Value)
            mCategory = IIf(IsDBNull(RsTemp.Fields("EMP_CATG").Value), "", RsTemp.Fields("EMP_CATG").Value)
        End If


        SqlStr = "SELECT * FROM PAY_SHIFT_TRN" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & Trim(lblCode.Text) & "'" & vbCrLf _
            & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenForwardOnly, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


        If RsTemp.EOF = False Then
            mAddMode = False
        Else
            mAddMode = True
        End If

        If mAddMode = True Then
            SqlStr = " INSERT INTO PAY_SHIFT_TRN ( " & vbCrLf _
                    & " COMPANY_CODE, PAYYEAR, " & vbCrLf _
                    & " EMP_CODE, SHIFT_DATE, " & vbCrLf _
                    & " EMP_DEPT_CODE, BOOKNO," & vbCrLf _
                    & " PAGENO, EMP_CAT, " & vbCrLf _
                    & " SHIFT_CODE, " & vbCrLf _
                    & " IN_TIME, OUT_TIME, " & vbCrLf _
                    & " B_START_TIME, B_END_TIME," & vbCrLf _
                    & " ADDUSER, ADDDATE, ROUND_CLOCK ,WEEKLY_OFF) VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(lblDate.Text)) & ", " & vbCrLf _
                    & " '" & Trim(lblCode.Text) & "', TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mDept) & "', 0, " & vbCrLf _
                    & " 0, '" & MainClass.AllowSingleQuote(mCategory) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(cboShift.Text) & "', " & vbCrLf _
                    & " TO_DATE('" & xInTime & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & xOutTime & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                    & " TO_DATE('" & xBStart & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & xBEnd & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & IIf(chkRoundClock.Checked = True, "Y", "N") & "','N') "

            PubDBCn.Execute(SqlStr)

        Else
            SqlStr = " UPDATE PAY_SHIFT_TRN SET SHIFT_CODE='" & MainClass.AllowSingleQuote(cboShift.Text) & "'," & vbCrLf _
                & " IN_TIME=TO_DATE('" & xInTime & "','DD-MON-YYYY HH24:MI'), OUT_TIME=TO_DATE('" & xOutTime & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                & " B_START_TIME=TO_DATE('" & xBStart & "','DD-MON-YYYY HH24:MI'), B_END_TIME=TO_DATE('" & xBEnd & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                & " ROUND_CLOCK ='" & IIf(chkRoundClock.Checked = True, "Y", "N") & "' " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CODE='" & Trim(lblCode.Text) & "'" & vbCrLf _
                & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        End If

        PubDBCn.Execute(SqlStr)

TransCommit:
        PubDBCn.CommitTrans()
        Me.Hide()
        Me.Close()
        Me.Dispose()
        Exit Function
UpdateError:
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
End Class
