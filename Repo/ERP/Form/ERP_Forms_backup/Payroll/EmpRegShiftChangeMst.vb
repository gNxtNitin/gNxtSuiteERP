Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO

Friend Class frmEmpRegShiftChange
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
    Private Const ColCategory As Short = 8
    Private Const ColShift As Short = 9
    Private Const ColIN As Short = 10
    Private Const ColOUT As Short = 11
    Private Const ColBStart As Short = 12
    Private Const ColBEnd As Short = 13
    Private Const ColRoundClock As Short = 14
    Private Const ColWEEKLYOFF As Short = 15
    Dim mCurrRow As Integer
    Dim mSearchKey As String

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
        cmdSave.Enabled = Not mPrintEnable
        cmdSaveMonthly.Enabled = Not mPrintEnable
    End Sub
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(sprdMain)

        With sprdMain
            .MaxCols = ColWEEKLYOFF

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

            .Col = ColCategory
            .Text = "Category"

            .Col = ColShift
            .Text = "Shift"

            .Col = ColIN
            .Text = "IN Time"

            .Col = ColOUT
            .Text = "OUT Time"

            .Col = ColBStart
            .Text = "Break Start Time"

            .Col = ColBEnd
            .Text = "Break End Time"

            .Col = ColRoundClock
            .Text = "Round Clock"

            .Col = ColWEEKLYOFF
            .Text = "Weekly Off"
        End With
    End Sub
    Private Sub FillShift()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mShiftCode As String

        txtShiftG_IN.Text = "00:00"
        txtShiftG_OUT.Text = "00:00"
        txtShiftG_BS.Text = "00:00"
        txtShiftG_BE.Text = "00:00"



        SqlStr = " SELECT SHIFT_CODE, SHIFT_DESC," & vbCrLf _
            & " FROM_TIME, TO_TIME," & vbCrLf _
            & " BS_TIME, BE_TIME " & vbCrLf _
            & " FROM PAY_SHIFT_MST " & vbCrLf _
            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SHIFT_CODE='" & MainClass.AllowSingleQuote((cboShift.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtShiftG_IN.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("FROM_TIME").Value), "00:00", RsTemp.Fields("FROM_TIME").Value), "hh:mm")
            txtShiftG_OUT.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TO_TIME").Value), "00:00", RsTemp.Fields("TO_TIME").Value), "hh:mm")
            txtShiftG_BS.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BS_TIME").Value), "00:00", RsTemp.Fields("BS_TIME").Value), "hh:mm")
            txtShiftG_BE.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BE_TIME").Value), "00:00", RsTemp.Fields("BE_TIME").Value), "hh:mm")
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
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

    Private Sub cboShift_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShift.TextChanged
        Call PrintCommand(False)
        FillShift()
    End Sub

    Private Sub cboShift_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShift.SelectedIndexChanged
        Call PrintCommand(False)
        FillShift()
    End Sub


    Private Sub cboShift_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboShift.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call PrintCommand(False)
        FillShift()
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub cboShowShift_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShowShift.TextChanged
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
    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
    End Sub
    Private Function Update1(ByRef mType As String) As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mCode As String
        Dim mDate As String
        Dim mDept As String
        Dim mBookNo As Double
        Dim mPageNo As Double
        Dim mCategory As String
        Dim mShift As String

        Dim mInTime As String
        Dim mOutTime As String
        Dim mBStart As String
        Dim mBEnd As String
        Dim mRoundClock As String
        Dim mLastDay As Integer

        Dim mDaysCnt As Integer
        Dim mNewDate As String
        Dim mStartDay As Integer
        Dim mWeeklyOff As String

        SqlStr = ""

        PubDBCn.BeginTrans()

        For cntRow = 1 To sprdMain.MaxRows

            sprdMain.Row = cntRow
            sprdMain.Col = ColCode
            mCode = Trim(sprdMain.Text)

            If mCode <> "" Then
                sprdMain.Row = cntRow

                '            sprdMain.Col = ColDate
                mDate = VB6.Format(Trim(lblRunDate.Text), "DD/MM/YYYY")

                sprdMain.Col = ColCode
                mCode = Trim(sprdMain.Text)

                sprdMain.Col = ColDept
                mDept = Trim(sprdMain.Text)

                sprdMain.Col = ColBookNo
                mBookNo = Val(sprdMain.Text)

                sprdMain.Col = ColPageNo
                mPageNo = Val(sprdMain.Text)

                sprdMain.Col = ColCategory
                mCategory = VB.Left(sprdMain.Text, 1)

                sprdMain.Col = ColShift
                mShift = VB.Left(sprdMain.Text, 1)

                sprdMain.Col = ColIN
                mInTime = VB6.Format(sprdMain.Text, "hh:mm")

                sprdMain.Col = ColOUT
                mOutTime = VB6.Format(sprdMain.Text, "hh:mm")

                sprdMain.Col = ColBStart
                mBStart = VB6.Format(sprdMain.Text, "hh:mm")

                sprdMain.Col = ColBEnd
                mBEnd = VB6.Format(sprdMain.Text, "hh:mm")

                sprdMain.Col = ColRoundClock
                mRoundClock = IIf(sprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                sprdMain.Col = ColWEEKLYOFF
                mWeeklyOff = IIf(sprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                If mCode <> "" Then
                    If mInTime = "00:00" Or mOutTime = "00:00" Then
                    Else
                        If mType = "D" Then
                            If UpdateTable(mCode, mDate, mDept, mBookNo, mPageNo, mCategory, mShift, mInTime, mOutTime, mBStart, mBEnd, mRoundClock, mType, mWeeklyOff) = False Then GoTo UpdateError
                        Else
                            mLastDay = VB.Day(CDate(txtUpdateTo.Text)) '' MainClass.LastDay(Month(mDate), Year(mDate))
                            mStartDay = VB.Day(CDate(txtUpdateFrom.Text))
                            For mDaysCnt = mStartDay To mLastDay ''mStartDay
                                mNewDate = VB6.Format(mDaysCnt & "/" & VB6.Format(mDate, "MM/YYYY"), "DD/MM/YYYY")
                                If UpdateTable(mCode, mNewDate, mDept, mBookNo, mPageNo, mCategory, mShift, mInTime, mOutTime, mBStart, mBEnd, mRoundClock, mType, mWeeklyOff) = False Then GoTo UpdateError
                            Next
                        End If
                    End If
                    '                SqlStr = " DELETE FROM PAY_SHIFT_TRN " & vbCrLf _
                    ''                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    ''                        & " AND SHIFT_DATE='" & VB6.Format(mDate, "DD-MMM-YYYY") & "'" & vbCrLf _
                    ''                        & " AND EMP_CODE='" & mCode & "'"
                    '
                    '                PubDBCn.Execute (SqlStr)
                    '
                    '                SqlStr = " INSERT INTO PAY_SHIFT_TRN ( " & vbCrLf _
                    ''                        & " COMPANY_CODE, PAYYEAR, " & vbCrLf _
                    ''                        & " EMP_CODE, SHIFT_DATE, " & vbCrLf _
                    ''                        & " EMP_DEPT_CODE, BOOKNO," & vbCrLf _
                    ''                        & " PAGENO, EMP_CAT, " & vbCrLf _
                    ''                        & " SHIFT_CODE, " & vbCrLf _
                    ''                        & " IN_TIME, OUT_TIME, " & vbCrLf _
                    ''                        & " B_START_TIME, B_END_TIME," & vbCrLf _
                    ''                        & " ADDUSER, ADDDATE, ROUND_CLOCK ) VALUES ( " & vbCrLf _
                    ''                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(mDate) & ", " & vbCrLf _
                    ''                        & " '" & mCode & "', '" & VB6.Format(mDate, "DD-MMM-YYYY") & "', " & vbCrLf _
                    ''                        & " '" & MainClass.AllowSingleQuote(mDept) & "', " & Val(mBookNo) & ", " & vbCrLf _
                    ''                        & " " & Val(mPageNo) & ", '" & MainClass.AllowSingleQuote(mCategory) & "'," & vbCrLf _
                    ''                        & " '" & MainClass.AllowSingleQuote(mShift) & "', " & vbCrLf _
                    ''                        & " TO_DATE('" & mInTime & "','HH24:MI'), TO_DATE('" & mOutTime & "','HH24:MI'), " & vbCrLf _
                    ''                        & " TO_DATE('" & mBStart & "','HH24:MI'), TO_DATE('" & mBEnd & "','HH24:MI'), " & vbCrLf _
                    ''                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "'), '" & mRoundClock & "') "
                    '
                    '                    PubDBCn.Execute (SqlStr)
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
    Private Function UpdateTable(ByRef mCode As String, ByRef mDate As String, ByRef mDept As String, ByRef mBookNo As Double, ByRef mPageNo As Double,
                                 ByRef mCategory As String, ByRef mShift As String, ByRef mInTime As String, ByRef mOutTime As String, ByRef mBStart As String, ByRef mBEnd As String, ByRef mRoundClock As String, ByRef mType As String, ByRef mWeeklyOff As String) As Boolean
        On Error GoTo UpdateError
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mFHalf As Integer
        Dim mSHalf As Integer

        Dim xInTime As String
        Dim xOutTime As String

        Dim xBStart As String
        Dim xBEnd As String



        If mCode <> "" Then

            xInTime = VB6.Format(mDate, "DD-MMM-YYYY") & " " & VB6.Format(mInTime, "HH:MM")

            If VB6.Format(mInTime, "HH:MM") < VB6.Format(mOutTime, "HH:MM") Then
                xOutTime = VB6.Format(mDate, "DD-MMM-YYYY") & " " & VB6.Format(mOutTime, "HH:MM")
            Else
                xOutTime = VB6.Format(DateAdd("d", 1, mDate), "DD-MMM-YYYY") & " " & VB6.Format(mOutTime, "HH:MM")
            End If

            xBStart = VB6.Format(mDate, "DD-MMM-YYYY") & " " & VB6.Format(mBStart, "HH:MM")
            xBEnd = VB6.Format(mDate, "DD-MMM-YYYY") & " " & VB6.Format(mBEnd, "HH:MM")

            '            If mType = "D" Then
            SqlStr = " DELETE FROM PAY_SHIFT_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And SHIFT_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND EMP_CODE='" & mCode & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = " INSERT INTO PAY_SHIFT_TRN ( " & vbCrLf _
                & " COMPANY_CODE, PAYYEAR, " & vbCrLf _
                & " EMP_CODE, SHIFT_DATE, " & vbCrLf _
                & " EMP_DEPT_CODE, BOOKNO," & vbCrLf _
                & " PAGENO, EMP_CAT, " & vbCrLf _
                & " SHIFT_CODE, " & vbCrLf _
                & " IN_TIME, OUT_TIME, " & vbCrLf _
                & " B_START_TIME, B_END_TIME," & vbCrLf _
                & " ADDUSER, ADDDATE, ROUND_CLOCK ,WEEKLY_OFF) VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(mDate)) & ", " & vbCrLf _
                & " '" & mCode & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mDept) & "', " & Val(CStr(mBookNo)) & ", " & vbCrLf _
                & " " & Val(CStr(mPageNo)) & ", '" & MainClass.AllowSingleQuote(mCategory) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mShift) & "', " & vbCrLf _
                & " TO_DATE('" & xInTime & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & xOutTime & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                & " TO_DATE('" & xBStart & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & xBEnd & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mRoundClock & "','" & mWeeklyOff & "') "

            PubDBCn.Execute(SqlStr)

            If RsCompany.Fields("WEEKLYOFF_TYPE").Value = "S" Then

                If mWeeklyOff = "Y" Then
                    mFHalf = SUNDAY
                    mSHalf = SUNDAY
                    If CheckAttnData(Trim(mCode), mDate) = False Then
                        SqlStr = "INSERT INTO PAY_ATTN_MST (" & vbCrLf & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
                            & " ATTN_DATE, FIRSTHALF, SECONDHALF, AGT_LATE," & vbCrLf & " ADDUSER, ADDDATE) VALUES (" & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(mDate)) & ", " & vbCrLf _
                            & " '" & Trim(mCode) & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & "  " & mFHalf & ", " & mSHalf & ", 'N'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                        PubDBCn.Execute(SqlStr)
                    Else
                        SqlStr = "UPDATE PAY_ATTN_MST SET " & vbCrLf _
                            & " FIRSTHALF=" & mFHalf & " " & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND EMP_CODE='" & Trim(mCode) & "'" & vbCrLf _
                            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " AND FIRSTHALF IN (" & SUNDAY & ",-1)"

                        PubDBCn.Execute(SqlStr)

                        SqlStr = "UPDATE PAY_ATTN_MST SET " & vbCrLf _
                            & " SECONDHALF=" & mSHalf & " " & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND EMP_CODE='" & Trim(mCode) & "'" & vbCrLf _
                            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " AND SECONDHALF IN (" & SUNDAY & ",-1)"

                        PubDBCn.Execute(SqlStr)
                    End If
                Else
                    mFHalf = -1
                    mSHalf = -1
                    If CheckAttnData(Trim(mCode), mDate) = True Then
                        SqlStr = "UPDATE PAY_ATTN_MST SET " & vbCrLf _
                            & " FIRSTHALF=" & mFHalf & " " & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND EMP_CODE='" & Trim(mCode) & "'" & vbCrLf _
                            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " AND FIRSTHALF = " & SUNDAY & ""

                        PubDBCn.Execute(SqlStr)

                        SqlStr = "UPDATE PAY_ATTN_MST SET " & vbCrLf _
                            & " SECONDHALF=" & mSHalf & " " & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND EMP_CODE='" & Trim(mCode) & "'" & vbCrLf _
                            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " AND SECONDHALF = " & SUNDAY & ""

                        PubDBCn.Execute(SqlStr)
                    End If
                End If
            End If
        End If

        'Public Const ABSENT = 0
        'Public Const CASUAL = 1
        'Public Const EARN = 2
        'Public Const SICK = 3
        'Public Const MATERNITY = 4
        'Public Const CPLEARN = 5
        'Public Const WOPAY = 6
        'Public Const CPLAVAIL = 7
        'Public Const SUNDAY = 8
        'Public Const HOLIDAY = 9
        'Public Const PRESENT = 10

        '    mFHalf = IIf(cboFHalf.Text = "", -1, Val(Left(cboFHalf.Text, 2)))
        '        mSHalf = IIf(cboSHalf.Text = "", -1, Val(Left(cboSHalf.Text, 2)))
        '
        '        If mFHalf <> -1 Or mSHalf <> -1 Then
        '            If CheckAttnData(Trim(txtEmpCode.Text), txtRefDate.Text) = False Then
        '                SqlStr = "INSERT INTO PAY_ATTN_MST (" & vbCrLf _
        ''                        & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
        ''                        & " ATTN_DATE, FIRSTHALF, SECONDHALF, AGT_LATE," & vbCrLf _
        ''                        & " ADDUSER, ADDDATE) VALUES (" & vbCrLf _
        ''                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(txtRefDate.Text) & ", " & vbCrLf _
        ''                        & " '" & Trim(txtEmpCode.Text) & "', TO_DATE('" & VB6.Format(txtRefDate, "DD-MMM-YYYY") & "'), " & vbCrLf _
        ''                        & "  " & mFHalf & ", " & mSHalf & ", 'N'," & vbCrLf _
        ''                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "','" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "')"
        '            Else
        '                SqlStr = "UPDATE PAY_ATTN_MST SET " & vbCrLf _
        ''                        & " FIRSTHALF=" & mFHalf & ", " & vbCrLf _
        ''                        & " SECONDHALF=" & mSHalf & " " & vbCrLf _
        ''                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''                        & " AND EMP_CODE='" & txtEmpCode.Text & "'" & vbCrLf _
        ''                        & " AND ATTN_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "')"
        '            End If
        '            PubDBCn.Execute SqlStr
        '
        '
        UpdateTable = True
        Exit Function
UpdateError:
        UpdateTable = False
        '    MsgBox err.Description + " Error No.: " + Str(err.Number)
        ''    Resume
        '    PubDBCn.Errors.Clear
        '    PubDBCn.RollbackTrans
        '    Screen.MousePointer = vbDefault
    End Function
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler
        Dim mYM As Integer
        Dim cntRow As Integer


        '    If GetUserAuthorizedRight(XRIGHT) = False Then
        '        If GetBackAttnData = True Then
        '            MsgBox "You Cann't Change Back Entry. ", vbCritical
        '            Exit Sub
        '        End If
        '    End If

        mYM = CInt(VB6.Format(Year(CDate(lblRunDate.Text)), "0000") & VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        If PubSuperUser <> "S" Then
            If SalProcess(mYM) = False Then
                MsgBox("Salary Process so cann't be Change. ", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        '    If MainClass.ValidDataInGrid(SprdMain, ColIN, "S", "Please Check IN Time.") = False Then Exit Sub
        '    If MainClass.ValidDataInGrid(SprdMain, ColOUT, "S", "Please Check OUT Time.") = False Then Exit Sub
        '    If MainClass.ValidDataInGrid(SprdMain, ColBStart, "S", "Please Check Break Start Time.") = False Then Exit Sub
        '    If MainClass.ValidDataInGrid(SprdMain, ColBEnd, "S", "Please Check reak End Time.") = False Then Exit Sub

        With sprdMain
            For cntRow = 1 To sprdMain.MaxRows
                .Col = ColWEEKLYOFF
                If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                    .Col = ColCode
                    If Trim(.Text) <> "" Then
                        .Col = ColIN
                        If Trim(.Text) = "" Then
                            MsgInformation("Please Check IN Time.")
                            Exit Sub
                        End If

                        .Col = ColOUT
                        If Trim(.Text) = "" Then
                            MsgInformation("Please Check OUT Time.")
                            Exit Sub
                        End If

                        .Col = ColBStart
                        If Trim(.Text) = "" Then
                            MsgInformation("Please Check Break Start Time.")
                            Exit Sub
                        End If

                        .Col = ColBEnd
                        If Trim(.Text) = "" Then
                            MsgInformation("Please Break End Time.")
                            Exit Sub
                        End If
                    End If
                End If
            Next
        End With

        If Update1("D") = True Then
            Call RefreshScreen()
            cmdSave.Enabled = False
            Call PrintCommand(True)
        Else
            cmdSave.Enabled = True
            Call PrintCommand(False)
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function GetBackAttnData() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCount As Double

        GetBackAttnData = False

        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            Exit Function
        End If

        SqlStr = " SELECT COUNT(1) AS CNTREC " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ATTN_DATE>TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TOT_HOURS<>0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mCount = IIf(IsDBNull(RsTemp.Fields("CNTREC").Value), 0, RsTemp.Fields("CNTREC").Value)
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


        If FillPrintDummyData(sprdMain, 1, sprdMain.MaxRows, ColDate, sprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "Shift Change List "
        mSubTitle = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, "", " Dept : " & cboDept.Text)
        mSubTitle = mSubTitle & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Checked, "", " Category : " & cboCategory.Text)

        Call ShowReport(SqlStr, "ShiftChangeList.Rpt", Mode, mTitle, mSubTitle)

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
        MainClass.ClearGrid(sprdMain)

        If optAll(1).Checked = True Then
            If txtEmpCode.Text = "" Then
                MsgInformation("Please select the Employee Code.")
                txtEmpCode.Focus()
                Exit Sub
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        RefreshScreen()
    End Sub



    Private Sub cmdSaveMonthly_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSaveMonthly.Click
        On Error GoTo ErrorHandler
        Dim mYM As Integer
        Dim cntRow As Integer
        Dim mAuthorisation As String

        mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")
        If PubSuperUser = "U" And mAuthorisation = "N" Then
            If GetBackAttnData() = True Then
                MsgBox("You Cann't Change Back Entry. ", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        mYM = CInt(VB6.Format(Year(CDate(lblRunDate.Text)), "0000") & VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        If SalProcess(mYM) = False Then
            MsgBox("Salary Made So that Process not be done. ", MsgBoxStyle.Critical)
            Exit Sub
        End If


        If Trim(txtUpdateFrom.Text) = "__/__/____" Or Trim(txtUpdateTo.Text) = "__/__/____" Then
            MsgBox("Date Field is Blank. Please Enter Date.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Not IsDate(txtUpdateFrom.Text) Then
            MsgInformation("Please enter the vaild date.")
            Exit Sub
        ElseIf VB6.Format(txtUpdateFrom.Text, "YYYYMM") <> VB6.Format(lblRunDate.Text, "YYYYMM") Then
            MsgInformation("Date should be in Selected Month.")
            Exit Sub
        End If

        If Not IsDate(txtUpdateTo.Text) Then
            MsgInformation("Please enter the vaild date.")
            Exit Sub
        ElseIf VB6.Format(txtUpdateTo.Text, "YYYYMM") <> VB6.Format(lblRunDate.Text, "YYYYMM") Then
            MsgInformation("Date should be in Selected Month.")
            Exit Sub
        End If

        '    If MainClass.ValidDataInGrid(SprdMain, ColIN, "S", "Please Check IN Time.") = False Then Exit Sub
        '    If MainClass.ValidDataInGrid(SprdMain, ColOUT, "S", "Please Check OUT Time.") = False Then Exit Sub
        '    If MainClass.ValidDataInGrid(SprdMain, ColBStart, "S", "Please Check Break Start Time.") = False Then Exit Sub
        '    If MainClass.ValidDataInGrid(SprdMain, ColBEnd, "S", "Please Check reak End Time.") = False Then Exit Sub

        With sprdMain
            For cntRow = 1 To sprdMain.MaxRows
                .Col = ColCode
                .Row = cntRow
                If Trim(.Text) <> "" Then
                    .Col = ColIN
                    If Trim(.Text) = "" Then
                        MsgInformation("Please Check IN Time.")
                        Exit Sub
                    End If

                    .Col = ColOUT
                    If Trim(.Text) = "" Then
                        MsgInformation("Please Check OUT Time.")
                        Exit Sub
                    End If

                    .Col = ColBStart
                    If Trim(.Text) = "" Then
                        MsgInformation("Please Check Break Start Time.")
                        Exit Sub
                    End If

                    .Col = ColBEnd
                    If Trim(.Text) = "" Then
                        MsgInformation("Please Break End Time.")
                        Exit Sub
                    End If

                    .Col = ColWEEKLYOFF
                    If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                        MsgInformation("Please Check Weekly OFF.")
                        Exit Sub
                    End If
                End If
            Next
        End With

        If MsgQuestion("Are you sure to Continue ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.

            If Update1("M") = True Then
                Call RefreshScreen()
                cmdSaveMonthly.Enabled = True
                Call PrintCommand(True)
            Else
                cmdSaveMonthly.Enabled = True
                Call PrintCommand(False)
                MsgInformation("Record not saved")
            End If
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSet_G_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSet_G.Click
        On Error GoTo ErrPart
        Dim I As Integer
        Dim mShift As String
        Dim mTimeValue As String
        Dim mRoundClock As String

        With sprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColShift
                mShift = Trim(.Text)

                If mShift = Trim(cboShift.Text) Then
                    .Col = ColIN
                    mTimeValue = VB.Left(txtShiftG_IN.Text, 2) & Mid(txtShiftG_IN.Text, 4, 2) & "00"
                    mRoundClock = IIf(mTimeValue >= "160000", "Y", "N")
                    .Value = mTimeValue

                    .Col = ColOUT
                    mTimeValue = VB.Left(txtShiftG_OUT.Text, 2) & Mid(txtShiftG_OUT.Text, 4, 2) & "00"
                    .Value = mTimeValue

                    .Col = ColBStart
                    mTimeValue = VB.Left(txtShiftG_BS.Text, 2) & Mid(txtShiftG_BS.Text, 4, 2) & "00"
                    .Value = mTimeValue

                    .Col = ColBEnd
                    mTimeValue = VB.Left(txtShiftG_BE.Text, 2) & Mid(txtShiftG_BE.Text, 4, 2) & "00"
                    .Value = mTimeValue

                    .Col = ColRoundClock
                    .Value = IIf(mRoundClock = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                End If
            Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdShiftChange_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShiftChange.Click

        On Error GoTo ErrPart
        Dim I As Integer
        Dim mCode As String
        Dim mShift As String
        Dim mTimeValue As String
        Dim mRoundClock As String
        Dim mPrevDate As String
        Dim mHoliday As Boolean
        Dim mHType As String
        Dim mInTime As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mShift_IN As Date
        Dim mShift_OUT As Date
        Dim mShift_BS As Date
        Dim mShift_BE As Date
        Dim pBookNo As String
        Dim pPageNo As String

        mPrevDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(lblRunDate.Text)))
        mHoliday = GetIsHolidays(mPrevDate, mHType, "", "Y", "N")

        If chkBlankShift.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If mHoliday = False Then Exit Sub
        End If

        With sprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColCode
                mCode = Trim(.Text)

                mInTime = CStr(GetIOTime(mCode, VB6.Format(lblRunDate.Text, "DD/MM/YYYY")))

                If mInTime = "00:00:00" Then GoTo NextRec

                SqlStr = " SELECT SHIFT_CODE, SHIFT_DESC," & vbCrLf _
                    & " FROM_TIME, TO_TIME," & vbCrLf _
                    & " BS_TIME, BE_TIME, ROUND_CLOCK " & vbCrLf _
                    & " FROM PAY_SHIFT_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TO_CHAR(FROM_TIME,'HH24:MI') = (" & vbCrLf _
                    & " SELECT MIN(TO_CHAR(FROM_TIME,'HH24:MI')) " & vbCrLf _
                    & " FROM PAY_SHIFT_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TO_CHAR(FROM_TIME,'HH24:MI')>='" & VB6.Format(mInTime, "hh:mm") & "')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mShift = IIf(IsDBNull(RsTemp.Fields("SHIFT_CODE").Value), "", RsTemp.Fields("SHIFT_CODE").Value)
                    mShift_IN = CDate(VB6.Format(IIf(IsDBNull(RsTemp.Fields("FROM_TIME").Value), "00:00", RsTemp.Fields("FROM_TIME").Value), "hh:mm"))
                    mShift_OUT = CDate(VB6.Format(IIf(IsDBNull(RsTemp.Fields("TO_TIME").Value), "00:00", RsTemp.Fields("TO_TIME").Value), "hh:mm"))
                    mShift_BS = CDate(VB6.Format(IIf(IsDBNull(RsTemp.Fields("BS_TIME").Value), "00:00", RsTemp.Fields("BS_TIME").Value), "hh:mm"))
                    mShift_BE = CDate(VB6.Format(IIf(IsDBNull(RsTemp.Fields("BE_TIME").Value), "00:00", RsTemp.Fields("BE_TIME").Value), "hh:mm"))
                    mRoundClock = IIf(IsDBNull(RsTemp.Fields("ROUND_CLOCK").Value), "N", RsTemp.Fields("ROUND_CLOCK").Value)
                Else
NextRec:
                    mShift = GetDefaultShift()
                    mShift_IN = CDate("00:00")
                    mShift_OUT = CDate("00:00")
                    mShift_BS = CDate("00:00")
                    mShift_BE = CDate("00:00")
                    mRoundClock = "N"
                End If

                .Col = ColShift
                .Text = Trim(mShift)

                .Col = ColIN
                mTimeValue = VB.Left(CStr(mShift_IN), 2) & Mid(CStr(mShift_IN), 4, 2) & "00"
                mRoundClock = IIf(mTimeValue >= "160000", "Y", "N")
                .Value = mTimeValue

                .Col = ColOUT
                mTimeValue = VB.Left(CStr(mShift_OUT), 2) & Mid(CStr(mShift_OUT), 4, 2) & "00"
                .Value = mTimeValue

                .Col = ColBStart
                mTimeValue = VB.Left(CStr(mShift_BS), 2) & Mid(CStr(mShift_BS), 4, 2) & "00"
                .Value = mTimeValue

                .Col = ColBEnd
                mTimeValue = VB.Left(CStr(mShift_BE), 2) & Mid(CStr(mShift_BE), 4, 2) & "00"
                .Value = mTimeValue

                .Col = ColRoundClock
                .Value = IIf(mRoundClock = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function GetIOTime(ByRef mCode As String, ByRef mAttnDate As String) As Date

        On Error GoTo ErrPart

        Dim mNewCode As String
        Dim GateConnStr As String
        Dim GateDBCn As ADODB.Connection
        Dim RsGate As ADODB.Recordset
        Dim mTableName As String

        GetIOTime = CDate("00:00")
        GateConnStr = StrConn ''"DSN=" & DBConDSN & ""

        GateDBCn = New ADODB.Connection
        GateDBCn.Open(GateConnStr)

        'If RsCompany.Fields("COMPANY_CODE").Value = 35 Then
        '    mTableName = "TEMPDATA_CORP"
        'Else
        mTableName = "TEMPDATA"
        'End If

        'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Then
        '    mNewCode = RsCompany.Fields("COMPANY_CODE").Value & mCode
        'ElseIf RsCompany.Fields("COMPANY_CODE").Value = 35 Or RsCompany.Fields("COMPANY_CODE").Value = 41 Then
        '    mNewCode = CStr(Val(mCode))
        'Else
        mNewCode = mCode
        'End If

        SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM " & mTableName & " " & vbCrLf _
            & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf _
            & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "'"

        SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "


        MainClass.UOpenRecordSet(SqlStr, GateDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGate, ADODB.LockTypeEnum.adLockReadOnly)

        If RsGate.EOF = False Then
            If IsDBNull(RsGate.Fields("ATTN_TIME").Value) Then
                GetIOTime = CDate("00:00:00")
            Else
                GetIOTime = CDate(VB6.Format(IIf(IsDBNull(RsGate.Fields("ATTN_TIME").Value), "00:00:00", RsGate.Fields("ATTN_TIME").Value), "DD/MM/YYYY HH:MM:SS"))
                GetIOTime = CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, -15, GetIOTime), "HH:MM"))
            End If
        End If

        Exit Function

ErrPart:
        'Resume
        '    MsgInformation "Attendance Process Not Complete, Try Again."
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub cmdUpdateSD_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdateSD.Click
        On Error GoTo ErrPart
        Dim I As Integer

        If RsCompany.Fields("WEEKLYOFF_TYPE").Value = "C" Then
            Exit Sub
        End If

        With sprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColWEEKLYOFF
                .Value = CStr(System.Windows.Forms.CheckState.Checked)
            Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub cmpPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmpPopulate.Click
        '    SetDate lblRunDate.Caption
        '    MainClass.ClearGrid sprdMain

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        Call Populate()
    End Sub


    Private Sub frmEmpRegShiftChange_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        Me.Text = "Employee Shift Change Entry"
        FormActive = True
        '    If lblCategory.Caption = "G" Then
        '        Me.Caption = Me.Caption & " (General)"
        '    Else
        '        Me.Caption = Me.Caption & " (P. Rate)"
        '    End If
    End Sub

    Private Sub frmEmpRegShiftChange_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        txtUpdateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtUpdateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

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

        Call FillShift()

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub



    Private Sub frmEmpRegShiftChange_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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

    Private Sub optAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optAll.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optAll.GetIndex(eventSender)
            Call PrintCommand(False)
            If optAll(0).Checked = True Then
                txtEmpCode.Enabled = False
                cmdsearch.Enabled = False
            ElseIf optAll(1).Checked = True Then
                txtEmpCode.Enabled = True
                cmdsearch.Enabled = True
            End If
        End If
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdMain.Change
        cmdSave.Enabled = True
        cmdSaveMonthly.Enabled = True
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
        cmdSaveMonthly.Enabled = True
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
        Dim xBasicSalary As Double
        Dim mTime As Integer

        If eventArgs.newRow = -1 Then Exit Sub

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles sprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim xBasicSalary As Double


        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub txtShiftG_BE_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShiftG_BE.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mTimeValue As String

        If Val(VB.Left(txtShiftG_BE.Text, 2)) >= 24 Then
            MsgBox("Hours Cann't be Greater Than 24")
            txtShiftG_BE.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If CDbl(Mid(txtShiftG_BE.Text, 4, 2)) >= 60 Then
            MsgBox("Minute Cann't be Greater Than 60")
            txtShiftG_BE.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtShiftG_BS_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShiftG_BS.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mTimeValue As String

        If Val(VB.Left(txtShiftG_BS.Text, 2)) >= 24 Then
            MsgBox("Hours Cann't be Greater Than 24")
            txtShiftG_BS.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If CDbl(Mid(txtShiftG_BS.Text, 4, 2)) >= 60 Then
            MsgBox("Minute Cann't be Greater Than 60")
            txtShiftG_BS.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtShiftG_IN_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShiftG_IN.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mTimeValue As String

        If Val(VB.Left(txtShiftG_IN.Text, 2)) >= 24 Then
            MsgBox("Hours Cann't be Greater Than 24")
            txtShiftG_IN.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If CDbl(Mid(txtShiftG_IN.Text, 4, 2)) >= 60 Then
            MsgBox("Minute Cann't be Greater Than 60")
            txtShiftG_IN.Focus()
            Cancel = True
            GoTo EventExitSub
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtShiftG_OUT_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShiftG_OUT.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mTimeValue As String

        If Val(VB.Left(txtShiftG_OUT.Text, 2)) >= 24 Then
            MsgBox("Hours Cann't be Greater Than 24")
            txtShiftG_OUT.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If CDbl(Mid(txtShiftG_OUT.Text, 4, 2)) >= 60 Then
            MsgBox("Minute Cann't be Greater Than 60")
            txtShiftG_OUT.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtUpdateFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUpdateFrom.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub txtUpdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtUpdateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtUpdateFrom.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        ElseIf VB6.Format(txtUpdateFrom.Text, "YYYYMM") <> VB6.Format(lblRunDate.Text, "YYYYMM") Then
            MsgInformation("Date should be in Selected Month.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtUpdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUpdateTo.TextChanged
        Call PrintCommand(False)
    End Sub


    Private Sub txtUpdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtUpdateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtUpdateTo.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        ElseIf VB6.Format(txtUpdateTo.Text, "YYYYMM") <> VB6.Format(lblRunDate.Text, "YYYYMM") Then
            MsgInformation("Date should be in Selected Month.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
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
        'Dim mBasicSalary As Double
        'Dim mGrossSalary As Double
        Dim mDeptCode As String
        Dim mContCode As Double
        Dim mBookNo As String
        Dim mCheckCond As Boolean
        Dim mSqlStr As String
        Dim mCatgeory As String
        Dim mShift As String


        mDOJ = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mDOL = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")

        SqlStr = " SELECT EMP.EMP_NAME, EMP.EMP_CODE, EMP.EMP_DEPT_CODE, EMP.EMP_FNAME, EMP.EMP_DEPT_CODE, EMP.SHIFT_CODE, EMP.EMP_CATG " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP WHERE " & vbCrLf _
            & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & ""


        SqlStr = SqlStr & vbCrLf _
            & " AND EMP.EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP.EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP.EMP_LEAVE_DATE IS NULL) "


        If optAll(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpCode.Text)) & "' "
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptName = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptName)) & "' "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If cboShowShift.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.SHIFT_CODE='G' "
        ElseIf cboShowShift.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG <>'G' "
        End If

        If chkBlankShift.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE NOT IN ( " & vbCrLf & " SELECT EMP_CODE " & vbCrLf & " FROM PAY_SHIFT_TRN TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME"
        ElseIf optCardNo.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE"
        ElseIf optDepartment.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_DEPT_CODE, EMP.EMP_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by TRN.BOOKNO, TRN.SNO"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpSal, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmpSal.EOF = False Then
            With sprdMain
                cntRow = 1
                .MaxRows = cntRow
                Do While Not RsEmpSal.EOF
                    .Row = cntRow

                    .Col = ColCode
                    mCode = RsEmpSal.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsEmpSal.Fields("EMP_NAME").Value

                    .Col = ColFName
                    .Text = IIf(IsDBNull(RsEmpSal.Fields("EMP_FNAME").Value), "", RsEmpSal.Fields("EMP_FNAME").Value)

                    mDeptCode = IIf(IsDBNull(RsEmpSal.Fields("EMP_DEPT_CODE").Value), "", RsEmpSal.Fields("EMP_DEPT_CODE").Value)
                    mCatgeory = IIf(IsDBNull(RsEmpSal.Fields("EMP_CATG").Value), "", RsEmpSal.Fields("EMP_CATG").Value)
                    mCatgeory = IIf(Trim(mCatgeory) = "", "G", mCatgeory)

                    mShift = IIf(IsDBNull(RsEmpSal.Fields("SHIFT_CODE").Value), "G", RsEmpSal.Fields("SHIFT_CODE").Value)
                    mShift = IIf(Trim(mShift) = "", "G", mShift)

                    If CalcVariable(mCode, cntRow, mDeptCode, mCatgeory, mShift, "Y") = False Then GoTo NextRow


NextRow:
                    cntRow = cntRow + 1
                    RsEmpSal.MoveNext()
                    If RsEmpSal.EOF = False Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop
                FormatSprd(-1)
            End With
        End If


        cmdSave.Enabled = True
        cmdSaveMonthly.Enabled = True
        Call PrintCommand(True)
        Exit Sub

ErrRefreshScreen:
        MsgInformation(Err.Description)
    End Sub
    Private Sub RefreshScreen004032023()

        On Error GoTo ErrRefreshScreen004032023

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
        'Dim mBasicSalary As Double
        'Dim mGrossSalary As Double
        Dim mDeptCode As String
        Dim mContCode As Double
        Dim mBookNo As String
        Dim mCheckCond As Boolean
        Dim mSqlStr As String
        Dim mCatgeory As String
        Dim mShift As String


        mDOJ = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mDOL = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")

        SqlStr = " SELECT EMP.EMP_NAME, EMP.EMP_CODE, EMP.EMP_DEPT_CODE, EMP.EMP_FNAME, EMP.EMP_DEPT_CODE, EMP.SHIFT_CODE, EMP.EMP_CATG " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP WHERE " & vbCrLf _
            & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & ""


        SqlStr = SqlStr & vbCrLf _
            & " AND EMP.EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP.EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP.EMP_LEAVE_DATE IS NULL) "


        If optAll(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpCode.Text)) & "' "
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptName = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptName)) & "' "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If cboShowShift.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.SHIFT_CODE='G' "
        ElseIf cboShowShift.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG <>'G' "
        End If

        If chkBlankShift.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE NOT IN ( " & vbCrLf & " SELECT EMP_CODE " & vbCrLf & " FROM PAY_SHIFT_TRN TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME"
        ElseIf optCardNo.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE"
        ElseIf optDepartment.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_DEPT_CODE, EMP.EMP_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by TRN.BOOKNO, TRN.SNO"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpSal, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmpSal.EOF = False Then
            With sprdMain
                cntRow = 1
                .MaxRows = cntRow
                Do While Not RsEmpSal.EOF
                    .Row = cntRow

                    .Col = ColCode
                    mCode = RsEmpSal.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsEmpSal.Fields("EMP_NAME").Value

                    .Col = ColFName
                    .Text = IIf(IsDBNull(RsEmpSal.Fields("EMP_FNAME").Value), "", RsEmpSal.Fields("EMP_FNAME").Value)

                    mDeptCode = IIf(IsDBNull(RsEmpSal.Fields("EMP_DEPT_CODE").Value), "", RsEmpSal.Fields("EMP_DEPT_CODE").Value)
                    mCatgeory = IIf(IsDBNull(RsEmpSal.Fields("EMP_CATG").Value), "", RsEmpSal.Fields("EMP_CATG").Value)
                    mCatgeory = IIf(Trim(mCatgeory) = "", "G", mCatgeory)

                    mShift = IIf(IsDBNull(RsEmpSal.Fields("SHIFT_CODE").Value), "G", RsEmpSal.Fields("SHIFT_CODE").Value)
                    mShift = IIf(Trim(mShift) = "", "G", mShift)

                    If CalcVariable(mCode, cntRow, mDeptCode, mCatgeory, mShift, "Y") = False Then GoTo NextRow


NextRow:
                    cntRow = cntRow + 1
                    RsEmpSal.MoveNext()
                    If RsEmpSal.EOF = False Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop
                FormatSprd(-1)
            End With
        End If
        cmdSave.Enabled = True
        cmdSaveMonthly.Enabled = True
        Call PrintCommand(True)
        Exit Sub

ErrRefreshScreen004032023:
        MsgInformation(Err.Description)
    End Sub
    Private Sub Populate()
        On Error GoTo ErrRefreshScreen
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mMonth As Short
        Dim mYear As Short
        Dim mYYMM As Integer
        Dim mDOJ As String
        Dim mDOL As String
        Dim mDeptName As String
        'Dim mBasicSalary As Double
        'Dim mGrossSalary As Double
        Dim mDeptCode As String
        Dim mContCode As Double
        Dim mBookNo As String
        Dim mCheckCond As Boolean
        Dim mSqlStr As String
        Dim mCatgeory As String
        Dim mShift As String

        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColCode
                mCode = Trim(.Text)

                .Col = ColDept
                mDeptCode = Trim(.Text)

                .Col = ColCategory
                mCatgeory = Trim(.Text)

                .Col = ColShift
                mShift = Trim(.Text)

                If CalcVariable(mCode, cntRow, mDeptCode, mCatgeory, mShift, "N") = False Then GoTo ErrRefreshScreen
            Next
            FormatSprd(-1)
        End With
        cmdSave.Enabled = True
        cmdSaveMonthly.Enabled = True
        Call PrintCommand(True)
        Exit Sub

ErrRefreshScreen:
        MsgInformation(Err.Description)
    End Sub
    Private Function CalcVariable(ByRef mCode As String, ByRef mRow As Integer, ByRef pDeptCode As String, ByRef pCategory As String, ByRef pShift As String, ByRef pIsCurrentMonth As String) As Boolean

        On Error GoTo ERR1
        Dim RSSalVar As ADODB.Recordset
        Dim cntCol As Integer
        Dim mHeadTitle As String
        Dim mLastMonthDate As String
        Dim mDefaultInTime As String
        Dim mDefaultOutTime As String
        Dim mDefaultBStart As String
        Dim mDefaultBEnd As String
        Dim mCategory As String

        CalcVariable = True
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PAY_SHIFT_TRN TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND EMP_Code='" & mCode & "'"

        If pIsCurrentMonth = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            '        mLastMonthDate = DateAdd("d", -1, CVDate(lblRunDate.Caption))
            SqlStr = SqlStr & vbCrLf & " AND SHIFT_DATE=( " & vbCrLf & " SELECT MAX(SHIFT_DATE) " & vbCrLf & " FROM PAY_SHIFT_TRN TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND EMP_Code='" & mCode & "'" & vbCrLf & " AND SHIFT_DATE<TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalVar, ADODB.LockTypeEnum.adLockOptimistic)

        If RSSalVar.EOF = False Then
            sprdMain.Row = mRow
            sprdMain.Col = ColDate
            sprdMain.Text = VB6.Format(IIf(IsDBNull(RSSalVar.Fields("SHIFT_DATE").Value), "", RSSalVar.Fields("SHIFT_DATE").Value), "DD/MM/YYYY")

            '        SprdMain.Col = ColDept
            '        SprdMain.Text = CStr(IIf(IsNull(RSSalVar!EMP_DEPT_CODE), "0", RSSalVar!EMP_DEPT_CODE))
            '
            '        SprdMain.Col = ColCategory
            '        mCategory = IIf(IsNull(RSSalVar!EMP_CAT), "", RSSalVar!EMP_CAT)
            '        SprdMain.TypeComboBoxCurSel = IIf(mCategory = "G", 0, 1)

            sprdMain.Col = ColDept
            sprdMain.Text = pDeptCode

            sprdMain.Col = ColCategory
            mCategory = IIf(pCategory = "", "G", pCategory)
            sprdMain.Text = mCategory

            sprdMain.Col = ColShift
            sprdMain.Text = CStr(IIf(IsDBNull(RSSalVar.Fields("SHIFT_CODE").Value), "0", RSSalVar.Fields("SHIFT_CODE").Value))

            sprdMain.Col = ColBookNo
            sprdMain.Text = CStr(IIf(IsDBNull(RSSalVar.Fields("BOOKNO").Value), "0", RSSalVar.Fields("BOOKNO").Value))

            sprdMain.Col = ColPageNo
            sprdMain.Text = CStr(IIf(IsDBNull(RSSalVar.Fields("PageNo").Value), "0", RSSalVar.Fields("PageNo").Value))

            sprdMain.Col = ColIN
            sprdMain.Text = VB6.Format(IIf(IsDBNull(RSSalVar.Fields("IN_TIME").Value), "", RSSalVar.Fields("IN_TIME").Value), "hh:mm")

            sprdMain.Col = ColOUT
            sprdMain.Text = VB6.Format(IIf(IsDBNull(RSSalVar.Fields("OUT_TIME").Value), "0", RSSalVar.Fields("OUT_TIME").Value), "hh:mm")

            sprdMain.Col = ColBStart
            sprdMain.Text = VB6.Format(IIf(IsDBNull(RSSalVar.Fields("B_START_TIME").Value), "", RSSalVar.Fields("B_START_TIME").Value), "hh:mm")

            sprdMain.Col = ColBEnd
            sprdMain.Text = VB6.Format(IIf(IsDBNull(RSSalVar.Fields("B_END_TIME").Value), "0", RSSalVar.Fields("B_END_TIME").Value), "hh:mm")

            sprdMain.Col = ColRoundClock
            sprdMain.Value = IIf(RSSalVar.Fields("ROUND_CLOCK").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            sprdMain.Col = ColWEEKLYOFF
            If RsCompany.Fields("WEEKLYOFF_TYPE").Value = "C" Then
                sprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            Else
                sprdMain.Value = IIf(RSSalVar.Fields("WEEKLY_OFF").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            End If

        Else
            sprdMain.Row = mRow

            sprdMain.Col = ColDate
            sprdMain.Text = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")

            sprdMain.Col = ColDept
            sprdMain.Text = pDeptCode

            sprdMain.Col = ColCategory
            mCategory = IIf(pCategory = "", "G", pCategory)
            sprdMain.Text = mCategory
            '        SprdMain.TypeComboBoxCurSel = IIf(mCategory = "G", 0, 1)

            sprdMain.Col = ColShift
            sprdMain.Text = IIf(pShift = "", "G", pShift)

            '        If pShift = "G" Then
            '            mDefaultInTime = "09:00"
            '            mDefaultOutTime = "17:30"
            '            mDefaultBStart = "12:30"
            '            mDefaultBEnd = "13:00"
            '        ElseIf pShift = "A" Then
            '            mDefaultInTime = "08:00"
            '            mDefaultOutTime = "20:00"
            '            mDefaultBStart = "12:30"
            '            mDefaultBEnd = "13:00"
            '        ElseIf pShift = "B" Then
            '            mDefaultInTime = "20:00"
            '            mDefaultOutTime = "08:00"
            '            mDefaultBStart = "00:00"
            '            mDefaultBEnd = "00:30"
            '        ElseIf pShift = "C" Then
            '            mDefaultInTime = "00:00"
            '            mDefaultOutTime = "00:00"
            '            mDefaultBStart = "00:00"
            '            mDefaultBEnd = "00:00"
            '        End If

            sprdMain.Col = ColBookNo
            sprdMain.Text = "0.0"

            sprdMain.Col = ColPageNo
            sprdMain.Text = "0.0"

            '        sprdMain.Col = ColIN
            '        sprdMain.Text = mDefaultInTime
            '
            '        sprdMain.Col = ColOUT
            '        sprdMain.Text = mDefaultOutTime
            '
            '        sprdMain.Col = ColBStart
            '        sprdMain.Text = mDefaultBStart
            '
            '        sprdMain.Col = ColBEnd
            '        sprdMain.Text = mDefaultBEnd
        End If
        Exit Function
ERR1:
        CalcVariable = False
    End Function

    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing

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

        SqlStr = "SELECT SHIFT_CODE FROM PAY_SHIFT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)

        cboShift.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboShift.Items.Add(RsDept.Fields("SHIFT_CODE").Value)
                RsDept.MoveNext()
            Loop
        End If
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

        cboShowShift.Items.Clear()
        cboShowShift.Items.Add("ALL")
        cboShowShift.Items.Add("General Shift Only")
        cboShowShift.Items.Add("Not General Shift Only")
        cboShowShift.SelectedIndex = 0


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
        '    lblRunDate.Caption = NewDate

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
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mShift As String

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
            .ColHidden = True

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
            .set_ColWidth(ColName, 28)

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
            .set_ColWidth(ColDept, 6)
            .ColHidden = True

            .Col = ColBookNo
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeNumberMax = CDbl("9999999")
            .TypeNumberMin = CDbl("-9999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBookNo, 3.7)

            .Col = ColPageNo
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeNumberMax = CDbl("9999999")
            .TypeNumberMin = CDbl("-9999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPageNo, 3.7)

            .Col = ColCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColCategory, 15)
            .ColHidden = True

            .Col = ColShift
            If FormActive = False Then
                mSqlStr = "SELECT SHIFT_CODE FROM PAY_SHIFT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    Do While RsTemp.EOF = False
                        mShift = CStr(IIf(mShift = "", "", mShift & Chr(9)) + IIf(IsDBNull(RsTemp.Fields("SHIFT_CODE").Value), "", RsTemp.Fields("SHIFT_CODE").Value))
                        RsTemp.MoveNext()
                    Loop
                Else
                    mShift = "G" & Chr(9) & "A" & Chr(9) & "B" & Chr(9) & "C"
                End If
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxList = mShift ''"G" + Chr(9) + "A" + Chr(9) + "B" + Chr(9) + "C"
                .TypeComboBoxCurSel = 0
            End If
            .set_ColWidth(ColShift, 6)

            .Col = ColIN
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeTime
            .TypeTime24Hour = FPSpreadADO.TypeTime24HourConstants.TypeTime24Hour24HourClock
            .TypeTimeSeconds = False
            .TypeTimeSeparator = Asc(":")
            .TypeTimeMin = "000000"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            '        .TypeSpin = True
            .set_ColWidth(ColIN, 8)

            .Col = ColOUT
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeTime
            .TypeTime24Hour = FPSpreadADO.TypeTime24HourConstants.TypeTime24Hour24HourClock
            .TypeTimeSeconds = False
            .TypeTimeSeparator = Asc(":")
            .TypeTimeMin = "000000"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            ''        .TypeSpin = True
            .set_ColWidth(ColOUT, 8)


            .Col = ColBStart
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeTime
            .TypeTime24Hour = FPSpreadADO.TypeTime24HourConstants.TypeTime24Hour24HourClock
            .TypeTimeSeconds = False
            .TypeTimeSeparator = Asc(":")
            .TypeTimeMin = "000000"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            '        .TypeSpin = True
            .set_ColWidth(ColBStart, 8)

            .Col = ColBEnd
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeTime
            .TypeTime24Hour = FPSpreadADO.TypeTime24HourConstants.TypeTime24Hour24HourClock
            .TypeTimeSeconds = False
            .TypeTimeSeparator = Asc(":")
            .TypeTimeMin = "000000"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            ''        .TypeSpin = True
            .set_ColWidth(ColBEnd, 8)

            .Col = ColRoundClock
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '        .Value = vbUnchecked
            .set_ColWidth(ColRoundClock, 6)

            .Col = ColWEEKLYOFF
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '        .Value = vbUnchecked
            .set_ColWidth(ColWEEKLYOFF, 6)

        End With

        MainClass.ProtectCell(sprdMain, 0, sprdMain.MaxRows, 0, ColFName)
        MainClass.ProtectCell(sprdMain, 0, sprdMain.MaxRows, ColDept, ColDept)
        MainClass.ProtectCell(sprdMain, 0, sprdMain.MaxRows, ColCategory, ColCategory)

        If RsCompany.Fields("WEEKLYOFF_TYPE").Value = "C" Then
            MainClass.ProtectCell(sprdMain, 0, sprdMain.MaxRows, ColWEEKLYOFF, ColWEEKLYOFF)
        End If
        '    MainClass.ProtectCell sprdMain, 0, sprdMain.MaxRows, ColTotHours, ColOTHours

        MainClass.SetSpreadColor(sprdMain, mRow)

        sprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        sprdMain.DAutoCellTypes = True
        sprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        sprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
End Class
