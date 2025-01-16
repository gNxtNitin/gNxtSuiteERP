Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmpManualBulkEntry
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColRefNo As Short = 1
    Private Const ColDate As Short = 2
    Private Const ColCode As Short = 3
    Private Const ColName As Short = 4
    Private Const ColFName As Short = 5
    Private Const ColDept As Short = 6
    Private Const ColIN As Short = 7
    Private Const ColOUT As Short = 8
    Private Const ColTotHours As Short = 9

    Dim mCurrRow As Integer
    Dim mSearchKey As String

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim FileDBCn As ADODB.Connection
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
        CmdSave.Enabled = Not mPrintEnable
    End Sub
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(SprdMain)

        With SprdMain
            .MaxCols = ColTotHours

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColRefNo
            .Text = "Ref No"

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

            .Col = ColIN
            .Text = "IN Time"

            .Col = ColOUT
            .Text = "OUT Time"

            .Col = ColTotHours
            .Text = "Total Hours"

        End With
    End Sub
    Private Function GetBackAttnData() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCount As Double

        GetBackAttnData = False

        SqlStr = " SELECT COUNT(1) AS CNTREC " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ATTN_DATE>TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TOT_HOURS<>0"

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

    Private Sub cboConName_Change()
        Call PrintCommand(False)
    End Sub

    Private Sub cboConName_Click()
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

    Private Sub cmdCalculate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCalculate.Click

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mEmpFName As String
        Dim mDate As String
        Dim mDeptName As String
        Dim mESINo As String
        Dim mUIDNo As String
        Dim xSqlStr As String
        'Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTemp1 As ADODB.Recordset
        'Dim RsFile As ADODB.Recordset
        'Dim FileConnStr As String
        '
        'Dim strTemp As String
        'Dim strWkShName As String
        'Dim strError As String

        Dim mInTime As String
        Dim mOutTime As String
        Dim mDateInTime As String
        Dim mDateOutTime As String
        Dim mHrTime As String
        Dim mMINTime As String
        Dim mDiffTime As String


        FormatSprd(-1)

        For cntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = cntRow
            SprdMain.Col = ColCode
            mEmpCode = Trim(SprdMain.Text)

            SprdMain.Col = ColDate
            mDate = VB6.Format(SprdMain.Text, "DD/MM/YYYY")

            SprdMain.Col = ColIN
            mInTime = VB6.Format(SprdMain.Text, "hh:mm")

            SprdMain.Col = ColOUT
            mOutTime = VB6.Format(SprdMain.Text, "hh:mm")

            If Trim(mEmpCode) = "" Then GoTo NextRow
            If Trim(mDate) = "" Then GoTo NextRow
            If Trim(mInTime) = "" Then GoTo NextRow
            If Trim(mOutTime) = "" Then GoTo NextRow

            If CDate(mDate) <> CDate(VB6.Format(lblRunDate.Text, "DD/MM/YYYY")) Then
                MsgInformation("Invalid Date of Employee Code : " & mEmpCode & ". In Line No : " & cntRow)
                Exit Sub
            End If

            xSqlStr = " SELECT EMP_NAME, EMP_FNAME, EMP_DEPT_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(EMP_CODE))='" & MainClass.AllowSingleQuote(mEmpCode) & "'"
            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mEmpName = Trim(IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value))
                mEmpFName = Trim(IIf(IsDbNull(RsTemp.Fields("EMP_FNAME").Value), "", RsTemp.Fields("EMP_FNAME").Value))
                mDeptName = Trim(IIf(IsDbNull(RsTemp.Fields("EMP_DEPT_CODE").Value), "", RsTemp.Fields("EMP_DEPT_CODE").Value))
            Else
                MsgInformation("Invalid Emp / Worker Code : " & mEmpCode & ". In Line No : " & cntRow)
                Exit Sub
            End If
            If DuplicateEmp = True Then
                MsgInformation("Duplicate Emp / Worker Code : " & mEmpCode & ". In Line No : " & cntRow)
                Exit Sub
            End If

            SprdMain.Row = cntRow

            SprdMain.Col = ColDate
            SprdMain.Text = VB6.Format(mDate, "DD/MM/YYYY")

            SprdMain.Col = ColCode
            SprdMain.Text = mEmpCode

            SprdMain.Col = ColName
            SprdMain.Text = mEmpName

            SprdMain.Col = ColFName
            SprdMain.Text = mEmpFName

            SprdMain.Col = ColDept
            SprdMain.Text = mDeptName

            If mOutTime = "" Or mOutTime = "" Then
                SprdMain.Col = ColIN
                SprdMain.Text = VB6.Format("", "hh:mm")

                SprdMain.Col = ColOUT
                SprdMain.Text = VB6.Format("", "hh:mm")

                SprdMain.Col = ColTotHours
                SprdMain.Text = VB6.Format("", "hh:mm")
            Else
                If CDate(mInTime) <= CDate(mOutTime) Or mInTime = "00:00" Or mOutTime = "00:00" Then
                    mDateInTime = VB6.Format(mDate & " " & mInTime, "DD-MMM-YYYY hh:mm")
                    mDateOutTime = VB6.Format(mDate & " " & mOutTime, "DD-MMM-YYYY hh:mm")
                Else
                    mDateInTime = VB6.Format(mDate & " " & mInTime, "DD-MMM-YYYY hh:mm")
                    mDateOutTime = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate)) & " " & mOutTime, "DD-MMM-YYYY hh:mm")
                End If

                If DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mDateInTime), CDate(mDateOutTime)) < 0 Then
                    SprdMain.Col = ColIN
                    SprdMain.Text = VB6.Format("", "hh:mm")

                    SprdMain.Col = ColOUT
                    SprdMain.Text = VB6.Format("", "hh:mm")

                    SprdMain.Col = ColTotHours
                    SprdMain.Text = VB6.Format("", "hh:mm")
                Else
                    SprdMain.Col = ColIN
                    SprdMain.Text = VB6.Format(mInTime, "hh:mm")

                    SprdMain.Col = ColOUT
                    SprdMain.Text = VB6.Format(mOutTime, "hh:mm")

                    mHrTime = CStr(Int(DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mDateInTime), CDate(mDateOutTime)) / 60))
                    mMINTime = CStr(DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mDateInTime), CDate(mDateOutTime)) - (60 * CDbl(mHrTime)))
                    mDiffTime = VB6.Format(mHrTime, "00") & ":" & VB6.Format(mMINTime, "00")
                    SprdMain.Col = ColTotHours
                    SprdMain.Text = mDiffTime
                End If
            End If

            SqlStr = "SELECT AUTO_KEY_NO FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "' AND MOVE_TYPE='B'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp1.EOF = False Then
                SprdMain.Col = ColRefNo
                SprdMain.Text = VB6.Format(IIf(IsDbNull(RsTemp1.Fields("AUTO_KEY_NO").Value), "", RsTemp1.Fields("AUTO_KEY_NO").Value))
            Else
                SprdMain.Col = ColRefNo
                SprdMain.Text = ""
            End If
NextRow:
        Next

        FormatSprd(-1)

        CmdSave.Enabled = True
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
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

        Dim mTotalTime As String
        'Dim mOutTime_O As String

        Dim mInTime As String
        Dim mOutTime As String
        Dim mTOTHours As Double
        Dim mWorksHours As Double
        Dim mOTHours As Double
        Dim mSundayOTHours As Double

        Dim mFMark As String
        Dim mSMark As String
        Dim mRefType As String
        Dim xAddMode As Boolean
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mRefNo As Double

        SqlStr = ""
        PubDBCn.BeginTrans()

        For cntRow = 1 To SprdMain.MaxRows

            xAddMode = True
            SprdMain.Row = cntRow
            SprdMain.Col = ColCode
            mCode = Trim(SprdMain.Text)

            If mCode <> "" Then


                SprdMain.Row = cntRow

                SqlStr = "SELECT AUTO_KEY_NO FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mCode & "' AND MOVE_TYPE='M'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                If RsTemp.EOF = False Then
                    xAddMode = False
                    mRefNo = Val(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_NO").Value), "", RsTemp.Fields("AUTO_KEY_NO").Value)) - CDbl(-VB6.Format(mRefNo, "00000"))
                    SprdMain.Col = ColRefNo
                    SprdMain.Text = VB6.Format(mRefNo, "00000")

                Else
                    mRefNo = 0
                    xAddMode = True
                End If


                SprdMain.Col = ColDate
                mDate = Trim(SprdMain.Text)

                '            CalcTotatHours cntRow, mDate

                SprdMain.Col = ColCode
                mCode = Trim(SprdMain.Text)

                SprdMain.Col = ColIN
                mInTime = VB6.Format(SprdMain.Text, "hh:mm")

                SprdMain.Col = ColOUT
                mOutTime = VB6.Format(SprdMain.Text, "hh:mm")


                SprdMain.Col = ColTotHours
                mTotalTime = VB6.Format(SprdMain.Text, "hh:mm")

                If mInTime = "" Or mOutTime = "" Or mInTime = "00:00" Or mOutTime = "00:00" Then

                Else

                    If CDate(mInTime) <= CDate(mOutTime) Or mInTime = "00:00" Or mOutTime = "00:00" Then
                        mInTime = VB6.Format(mDate & " " & mInTime, "DD-MMM-YYYY hh:mm")
                        mOutTime = VB6.Format(mDate & " " & mOutTime, "DD-MMM-YYYY hh:mm")
                    Else
                        mInTime = VB6.Format(mDate & " " & mInTime, "DD-MMM-YYYY hh:mm")
                        mOutTime = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate)) & " " & mOutTime, "DD-MMM-YYYY hh:mm")
                    End If


                    If mCode <> "" Then

                        If xAddMode = True Then

                            If Val(CStr(mRefNo)) = 0 Then
                                mRefNo = MaxRefNo
                                SprdMain.Row = cntRow
                                SprdMain.Col = ColRefNo
                                SprdMain.Text = VB6.Format(mRefNo, "00000")
                            End If

                            SqlStr = " INSERT INTO PAY_MOVEMENT_TRN ( " & vbCrLf & " COMPANY_CODE, AUTO_KEY_NO, " & vbCrLf & " REF_DATE, EMP_CODE, " & vbCrLf & " PLACE_VISIT, TIME_FROM, " & vbCrLf & " TIME_TO, TOTAL_HRS, MOVE_TYPE," & vbCrLf & " ATH_CODE, VISIT_FROM, VEHICLE_MODE, HR_APPROVAL, VISIT_DISTANCE, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE, AGT_LEAVE ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mRefNo)) & "," & vbCrLf & " TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & Trim(mCode) & "'," & vbCrLf & " 'MANNUAL ENTRY', TO_DATE('" & VB6.Format(mInTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf & " TO_DATE('" & VB6.Format(mOutTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & VB6.Format(mTotalTime, "HH:MM") & "','HH24:MI')," & vbCrLf & " 'M', '" & Trim(mCode) & "', 1, 1,'Y',0," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '','','N')"
                        Else
                            SqlStr = " UPDATE PAY_MOVEMENT_TRN SET AUTO_KEY_NO=" & Val(CStr(mRefNo)) & "," & vbCrLf & " REF_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EMP_CODE='" & Trim(mCode) & "', " & vbCrLf & " TIME_FROM=TO_DATE('" & VB6.Format(mInTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " TIME_TO=TO_DATE('" & VB6.Format(mOutTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " TOTAL_HRS=TO_DATE('" & VB6.Format(mTotalTime, "HH:MM") & "','HH24:MI'), " & vbCrLf & " HR_APPROVAL='Y'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " AGT_LEAVE='N'" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_NO=" & Val(CStr(mRefNo)) & ""

                        End If

                        PubDBCn.Execute(SqlStr)
                    End If
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
    Private Function MaxRefNo() As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = "SELECT MAX(AUTO_KEY_NO) AS AUTO_KEY_NO FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MaxRefNo = 1
        Else
            MaxRefNo = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_NO").Value), 1, RsTemp.Fields("AUTO_KEY_NO").Value + 1)
        End If

        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Function



    Private Sub cmdInsertRow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdInsertRow.Click
        On Error GoTo ErrPart
        Dim mMaxRow As Integer
        Dim ColRow As Integer

        mMaxRow = Val(txtRows.Text)
        If Val(CStr(mMaxRow)) <= 1 Then
            Exit Sub
        End If
        With SprdMain
            For ColRow = 1 To mMaxRow
                .MaxRows = ColRow ''.MaxRows + 1
                .Row = .MaxRows
                .Action = SS_ACTION_INSERT_ROW
                .set_RowHeight(.MaxRows, ConRowHeight)
            Next
        End With
        FormatSprd(-1)
        CmdSave.Enabled = False
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        CmdSave.Enabled = False
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler
        Dim mYM As Integer
        Dim mAuthorisation As String
        Dim pChangeApproval As Boolean
        Dim pCheckDate As String

        Dim cntRow As Integer
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mEmpFName As String
        Dim mDate As String
        Dim mDeptName As String

        Dim mInTime As String
        Dim mOutTime As String
        Dim mDateInTime As String
        Dim mDateOutTime As String
        Dim mHrTime As String
        Dim mMINTime As String
        Dim mTotalHrs As String


        '    mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")

        pCheckDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, PubCurrDate))

        If CDate(lblRunDate.Text) < CDate(pCheckDate) Then
            pChangeApproval = GetAttnChangeApproval(PubUserID, "M", (lblRunDate.Text), (lblRunDate.Text))
            If pChangeApproval = False Then ''If mAuthorisation = "N" Then
                If GetBackAttnData = True Then
                    MsgBox("You Cann't Change Back Entry. ", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            mYM = CInt(VB6.Format(Year(CDate(lblRunDate.Text)), "0000") & VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
            If SalProcess(mYM) = False Then
                MsgBox("You are enable to process. ", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        For cntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = cntRow
            SprdMain.Col = ColCode
            mEmpCode = Trim(SprdMain.Text)

            SprdMain.Col = ColDate
            mDate = VB6.Format(SprdMain.Text, "DD/MM/YYYY")

            SprdMain.Col = ColIN
            mInTime = VB6.Format(SprdMain.Text, "hh:mm")

            SprdMain.Col = ColOUT
            mOutTime = VB6.Format(SprdMain.Text, "hh:mm")

            If Trim(mEmpCode) = "" Then
                MsgBox("Invalid Emp/Worker Code. ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Trim(mDate) = "" Then
                MsgBox("Invalid Date. ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Trim(mInTime) = "" Then
                MsgBox("Invalid IN Time. ", MsgBoxStyle.Critical)
                Exit Sub
            End If
            If Trim(mOutTime) = "" Then
                MsgBox("Invalid Out Time. ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If CDate(mDate) <> CDate(VB6.Format(lblRunDate.Text, "DD/MM/YYYY")) Then
                MsgInformation("Invalid Date of Employee Code : " & mEmpCode & ". In Line No : " & cntRow)
                Exit Sub
            End If

            SprdMain.Col = ColName
            mEmpName = Trim(SprdMain.Text)
            If Trim(mEmpName) = "" Then
                MsgBox("Invalid Emp/Worker's Name. ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            SprdMain.Col = ColFName
            mEmpFName = Trim(SprdMain.Text)
            If Trim(mEmpFName) = "" Then
                MsgBox("Invalid Emp/Worker's Father Name. ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            SprdMain.Col = ColDept
            mDeptName = Trim(SprdMain.Text)
            If Trim(mDeptName) = "" Then
                MsgBox("Invalid Emp/Worker's Dept Name. ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            SprdMain.Col = ColTotHours
            mTotalHrs = Trim(SprdMain.Text)
            If Trim(mTotalHrs) = "" Then
                MsgBox("Invalid Time. ", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Next

        If Update1 = True Then
            '        Call RefreshScreen
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

        Exit Sub

        PubDBCn.Errors.Clear()


        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows, ColDate, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        Call ShowReport(SqlStr, "DailyContAttn.Rpt", Mode, mTitle, mSubTitle)

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

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDivision.Text = "" Then
                If cboDivision.Enabled = True Then cboDivision.Focus()
                MsgInformation("Please Select Division.")
                Exit Sub
            End If
        End If

        FormatSprd(-1)
        RefreshScreen()
    End Sub

    Private Sub cmdUploadFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUploadFile.Click
        On Error GoTo ErrPart
        Dim strFilePath As String


        strFilePath = My.Application.Info.DirectoryPath
        If Not fOpenFile(strFilePath, "*.xls", "Excel Data", CommonDialogOpen) Then
            GoTo NormalExit
        End If

        Call PopulateFromXLSFile(strFilePath)

        CmdSave.Enabled = True
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mEmpFName As String
        Dim mDate As String
        Dim mDeptName As String
        Dim mESINo As String
        Dim mUIDNo As String
        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTemp1 As ADODB.Recordset
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String

        Dim mInTime As String
        Dim mOutTime As String
        Dim mDateInTime As String
        Dim mDateOutTime As String
        Dim mHrTime As String
        Dim mMINTime As String
        Dim mDiffTime As String

        MainClass.ClearGrid(SprdMain)
        FormatSprd(-1)


        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
        strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
            GoTo ErrPart
        End If

        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        strWkShName = RsFile.Fields("Table_Name").Value

        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"
        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then

            If RsFile.EOF = False Then
                Do While Not RsFile.EOF
                    mEmpCode = VB6.Format(Trim(IIf(IsDbNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value)), "000000")
                    mDate = VB6.Format(Trim(IIf(IsDbNull(RsFile.Fields(2).Value), "", RsFile.Fields(2).Value)), "DD/MM/YYYY")
                    mInTime = VB6.Format(Trim(IIf(IsDbNull(RsFile.Fields(3).Value), "", RsFile.Fields(3).Value)), "hh:mm")
                    mOutTime = VB6.Format(Trim(IIf(IsDbNull(RsFile.Fields(4).Value), "", RsFile.Fields(4).Value)), "hh:mm")

                    If CDate(mDate) <> CDate(VB6.Format(lblRunDate.Text, "DD/MM/YYYY")) Then
                        GoTo NextRecord
                    End If
                    xSqlStr = " SELECT EMP_NAME, EMP_FNAME, EMP_DEPT_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(EMP_CODE))='" & MainClass.AllowSingleQuote(mEmpCode) & "'"
                    MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mEmpName = Trim(IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value))
                        mEmpFName = Trim(IIf(IsDbNull(RsTemp.Fields("EMP_FNAME").Value), "", RsTemp.Fields("EMP_FNAME").Value))
                        mDeptName = Trim(IIf(IsDbNull(RsTemp.Fields("EMP_DEPT_CODE").Value), "", RsTemp.Fields("EMP_DEPT_CODE").Value))
                    Else
                        GoTo NextRecord
                    End If
                    If DuplicateEmp = True Then GoTo NextRecord

                    SprdMain.Row = SprdMain.MaxRows

                    SprdMain.Col = ColDate
                    SprdMain.Text = VB6.Format(mDate, "DD/MM/YYYY")

                    SprdMain.Col = ColCode
                    SprdMain.Text = mEmpCode

                    SprdMain.Col = ColName
                    SprdMain.Text = mEmpName

                    SprdMain.Col = ColFName
                    SprdMain.Text = mEmpFName

                    SprdMain.Col = ColDept
                    SprdMain.Text = mDeptName

                    If mOutTime = "" Or mOutTime = "" Then
                        SprdMain.Col = ColIN
                        SprdMain.Text = VB6.Format("", "hh:mm")

                        SprdMain.Col = ColOUT
                        SprdMain.Text = VB6.Format("", "hh:mm")

                        SprdMain.Col = ColTotHours
                        SprdMain.Text = VB6.Format("", "hh:mm")
                    Else
                        If CDate(mInTime) <= CDate(mOutTime) Or mInTime = "00:00" Or mOutTime = "00:00" Then
                            mDateInTime = VB6.Format(mDate & " " & mInTime, "DD-MMM-YYYY hh:mm")
                            mDateOutTime = VB6.Format(mDate & " " & mOutTime, "DD-MMM-YYYY hh:mm")
                        Else
                            mDateInTime = VB6.Format(mDate & " " & mInTime, "DD-MMM-YYYY hh:mm")
                            mDateOutTime = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate)) & " " & mOutTime, "DD-MMM-YYYY hh:mm")
                        End If

                        If DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mDateInTime), CDate(mDateOutTime)) < 0 Then
                            SprdMain.Col = ColIN
                            SprdMain.Text = VB6.Format("", "hh:mm")

                            SprdMain.Col = ColOUT
                            SprdMain.Text = VB6.Format("", "hh:mm")

                            SprdMain.Col = ColTotHours
                            SprdMain.Text = VB6.Format("", "hh:mm")
                        Else
                            SprdMain.Col = ColIN
                            SprdMain.Text = VB6.Format(mInTime, "hh:mm")

                            SprdMain.Col = ColOUT
                            SprdMain.Text = VB6.Format(mOutTime, "hh:mm")

                            mHrTime = CStr(Int(DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mDateInTime), CDate(mDateOutTime)) / 60))
                            mMINTime = CStr(DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mDateInTime), CDate(mDateOutTime)) - (60 * CDbl(mHrTime)))
                            mDiffTime = VB6.Format(mHrTime, "00") & ":" & VB6.Format(mMINTime, "00")
                            SprdMain.Col = ColTotHours
                            SprdMain.Text = mDiffTime
                        End If
                    End If

                    SqlStr = "SELECT AUTO_KEY_NO FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "' AND MOVE_TYPE='B'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockOptimistic)

                    If RsTemp1.EOF = False Then
                        SprdMain.Col = ColRefNo
                        SprdMain.Text = VB6.Format(IIf(IsDbNull(RsTemp1.Fields("AUTO_KEY_NO").Value), "", RsTemp1.Fields("AUTO_KEY_NO").Value))


                    Else
                        SprdMain.Col = ColRefNo
                        SprdMain.Text = ""
                    End If


                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    '               FormatSprdMain -1, False

NextRecord:
                    RsFile.MoveNext()
                Loop
            End If
        End If

        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
        End If

        FormatSprd(-1)

        '    CmdPopFromFile.Enabled = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub

    Private Function DuplicateEmp() As Boolean
        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckEmpCode As String
        Dim mEmpCode As String


        With SprdMain
            .Row = .ActiveRow
            .Col = ColCode
            mCheckEmpCode = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                mEmpCode = Trim(UCase(.Text))

                If (mEmpCode = mCheckEmpCode And mCheckEmpCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateEmp = True
                    MsgInformation("Duplicate Code : " & DuplicateEmp)
                    '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColItemCode
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Sub frmEmpManualBulkEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        Me.Text = "Daily Attendance Mannual Entry (bulk)" ''"Daily Attendance Register"
        '    If lblCategory.Caption = "G" Then
        '        Me.Caption = Me.Caption & " (General)"
        '    Else
        '        Me.Caption = Me.Caption & " (P. Rate)"
        '    End If
    End Sub

    Private Sub frmEmpManualBulkEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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


        chkDivision.CheckState = System.Windows.Forms.CheckState.Checked
        cboDivision.Enabled = False



        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub
    Private Sub frmEmpManualBulkEntry_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdMain.Change
        CmdSave.Enabled = True
        Call PrintCommand(False)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles sprdMain.ClickEvent

        Dim cntSearchRow As Integer
        cntSearchRow = 1
        mCurrRow = 1
        If eventArgs.Row = 0 And eventArgs.Col = ColName Then
            mSearchKey = ""
            mSearchKey = InputBox("Enter Emp Name :", "Search", mSearchKey)
            MainClass.SearchIntoGrid(SprdMain, ColName, mSearchKey, mCurrRow)
            cntSearchRow = cntSearchRow + 1
            mCurrRow = mCurrRow + 1
            SprdMain.Focus()
        End If
        CmdSave.Enabled = True
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles sprdMain.KeyUpEvent


        If eventArgs.KeyCode = System.Windows.Forms.Keys.F3 Then
            MainClass.SearchIntoGrid(SprdMain, ColName, mSearchKey, mCurrRow)
            mCurrRow = mCurrRow + 1
            SprdMain.Focus()
        End If
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim xBasicSalary As Double
        Dim mTime As String
        Dim mDate As String

        If eventArgs.NewRow = -1 Then Exit Sub

        '    Select Case Col
        '         Case ColIN
        '            sprdMain.Row = sprdMain.ActiveRow
        '
        '            sprdMain.Col = ColDate
        '            mDate = Format(sprdMain.Text, "DD/MM/YYYY")
        '
        '            sprdMain.Col = ColBSalary
        '            xBasicSalary = Val(sprdMain.Text)
        '
        '            sprdMain.Col = ColIN
        '            If Trim(sprdMain.Text) <> "" And Trim(sprdMain.Text) <> "00:00" Then
        '                mTime = sprdMain.Value
        '                sprdMain.Value = mTime
        ''                If xBasicSalary = 0 Then
        ''                    MsgInformation "Basic Salary is not Defined."
        ''                    MainClass.SetFocusToCell sprdMain, sprdMain.ActiveRow, ColIN
        ''                    Cancel = True
        ''                    Exit Sub
        ''                End If
        '            End If
        '            CalcTotatHours sprdMain.Row, mDate
        '        Case ColOUT
        '            sprdMain.Row = sprdMain.ActiveRow
        '
        '            sprdMain.Col = ColDate
        '            mDate = Format(sprdMain.Text, "DD/MM/YYYY")
        '
        '            sprdMain.Col = ColBSalary
        '            xBasicSalary = Val(sprdMain.Text)
        '
        '            sprdMain.Col = ColOUT
        '            If Trim(sprdMain.Text) <> "" And Trim(sprdMain.Text) <> "00:00" Then
        '                mTime = sprdMain.Value
        '                sprdMain.Value = mTime
        ''                If xBasicSalary = 0 Then
        ''                    MsgInformation "Basic Salary is not Defined."
        ''                    MainClass.SetFocusToCell sprdMain, sprdMain.ActiveRow, ColOUT
        ''                    Cancel = True
        ''                    Exit Sub
        ''                End If
        '            End If
        '            CalcTotatHours sprdMain.Row, mDate
        '    End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles sprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim xBasicSalary As Double
        Dim mDate As String

        '    Select Case sprdMain.ActiveCol
        '         Case ColIN
        '            sprdMain.Row = sprdMain.ActiveRow
        '
        '            sprdMain.Col = ColDate
        '            mDate = Format(sprdMain.Text, "DD/MM/YYYY")
        '
        '            sprdMain.Col = ColBSalary
        '            xBasicSalary = Val(sprdMain.Text)
        '
        '            sprdMain.Col = ColIN
        '            If Trim(sprdMain.Text) <> "" And Trim(sprdMain.Text) <> "00:00" Then
        '                sprdMain.Text = Format(sprdMain.Text, "HH:MM")
        ''                If xBasicSalary = 0 Then
        ''                    MsgInformation "Basic Salary is not Defined."
        ''                    MainClass.SetFocusToCell sprdMain, sprdMain.ActiveRow, ColIN
        ''                    Cancel = True
        ''                    Exit Sub
        ''                End If
        '            End If
        '            CalcTotatHours sprdMain.Row, mDate
        '        Case ColOUT
        '            sprdMain.Row = sprdMain.ActiveRow
        '
        '            sprdMain.Col = ColDate
        '            mDate = Format(sprdMain.Text, "DD/MM/YYYY")
        '
        '            sprdMain.Col = ColBSalary
        '            xBasicSalary = Val(sprdMain.Text)
        '
        '            sprdMain.Col = ColOTHours
        '            If Trim(sprdMain.Text) <> "" And Trim(sprdMain.Text) <> "00:00" Then
        '                sprdMain.Text = Format(sprdMain.Text, "HH:MM")
        ''                If xBasicSalary = 0 Then
        ''                    MsgInformation "Basic Salary is not Defined."
        ''                    MainClass.SetFocusToCell sprdMain, sprdMain.ActiveRow, ColOUT
        ''                    Cancel = True
        ''                    Exit Sub
        ''                End If
        '            End If
        '            CalcTotatHours sprdMain.Row, mDate
        '    End Select
        '    Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub UpDYear_DownClick()

        lblRunDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(lblRunDate.Text)), "DD-MMMM-YYYY")

        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(SprdMain, -1)
        '' RefreshScreen
    End Sub
    Private Sub UpDYear_UpClick()

        lblRunDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(lblRunDate.Text)), "DD-MMMM-YYYY")

        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(SprdMain, -1)
        ''RefreshScreen
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo ErrRefreshScreen
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsEmpSal As ADODB.Recordset
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mMonth As Short
        Dim mYear As Short
        Dim mYYMM As Integer
        'Dim mDOJ As String
        'Dim mDOL As String
        Dim mDeptName As String
        Dim mDeptCode As String
        Dim mContCode As Double
        Dim mCheckCond As Boolean
        Dim mSqlStr As String
        Dim mDivisionCode As Double
        'Dim mCntDateFrom As Long
        'Dim mCntDateTo As Long
        'Dim CntDay As Long


        '    mDOJ = Format(lblRunDate.Caption, "DD/MM/YYYY")      ''MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear
        '    mDOL = Format(lblRunDate.Caption, "DD/MM/YYYY")
        '        mCntDateFrom = Day(mDOL)
        '        mCntDateTo = Day(mDOJ)

        SqlStr = " SELECT EMP.EMP_NAME, EMP.EMP_CODE, EMP.EMP_DEPT_CODE, EMP.EMP_FNAME " & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_DOJ <=TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP.EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') OR EMP.EMP_LEAVE_DATE IS NULL) "


        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
                SqlStr = SqlStr & vbCrLf & "AND EMP.DIV_CODE=" & mDivisionCode & ""
            End If
        End If
        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME"
        ElseIf optCardNo.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpSal, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmpSal.EOF = False Then
            With SprdMain
                cntRow = 1
                .MaxRows = cntRow
                Do While Not RsEmpSal.EOF
                    .Row = cntRow

                    .Col = ColDate
                    .Text = VB6.Format(lblRunDate.Text, "DD-MM-YYYY")

                    .Col = ColCode
                    mCode = RsEmpSal.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsEmpSal.Fields("EMP_NAME").Value

                    .Col = ColFName
                    .Text = IIf(IsDbNull(RsEmpSal.Fields("EMP_FNAME").Value), "", RsEmpSal.Fields("EMP_FNAME").Value)

                    .Col = ColDept
                    mDeptCode = IIf(IsDbNull(RsEmpSal.Fields("EMP_DEPT_CODE").Value), "", RsEmpSal.Fields("EMP_DEPT_CODE").Value)
                    .Text = mDeptCode

                    SqlStr = "SELECT * FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mCode & "' AND MOVE_TYPE='B'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                    If RsTemp.EOF = False Then
                        .Col = ColRefNo
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_NO").Value), "", RsTemp.Fields("AUTO_KEY_NO").Value))

                        .Col = ColIN
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_FROM").Value), "", RsTemp.Fields("TIME_FROM").Value), "hh:mm")

                        .Col = ColOUT
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_TO").Value), "0", RsTemp.Fields("TIME_TO").Value), "hh:mm")
                    Else
                        .Col = ColRefNo
                        .Text = ""

                        .Col = ColIN
                        .Text = ""

                        .Col = ColOUT
                        .Text = ""
                    End If

                    cntRow = cntRow + 1

                    RsEmpSal.MoveNext()
                    If RsEmpSal.EOF = False Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop

                '             ColTotal sprdMain, ColTotHours, ColOTHours
                '            .Col = ColName
                '            .Row = .MaxRows
                '            .Text = "TOTAL :"

                FormatSprd(-1)

                '            MainClass.ProtectCell sprdMain, .MaxRows, .MaxRows, 0, .MaxCols
            End With
        End If
        CmdSave.Enabled = True
        Call PrintCommand(True)
        Exit Sub

ErrRefreshScreen:
        MsgInformation(Err.Description)
        '    Resume
    End Sub
    Private Function CalcVariable(ByRef mCode As String, ByRef mRow As Integer, ByRef CntDay As Integer) As Boolean
        'On Error GoTo ERR1
        'Dim RSSalVar As ADODB.Recordset
        'Dim cntCol As Long
        'Dim mHeadTitle As String
        'Dim mDate As String
        '
        '
        '    CalcVariable = True
        '    mDate = Format(CntDay & "/" & vb6.Format(lblRunDate.Caption, "MM/YYYY"), "DD/MM/YYYY")
        '
        '    SqlStr = " SELECT * " & vbCrLf _
        ''         & " FROM PAY_DALIY_ATTN_TRN TRN " & vbCrLf _
        ''         & " WHERE " & vbCrLf _
        ''         & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        ''         & " AND EMP_Code='" & mCode & "'" & vbCrLf _
        ''         & " AND ATTN_DATE='" & VB6.Format(mDate, "DD-MMM-YYYY") & "'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RSSalVar, adLockOptimistic
        '
        '    If RSSalVar.EOF = False Then
        '        sprdMain.Row = mRow
        '        sprdMain.Col = ColDate
        '        sprdMain.Text = Format(IIf(IsNull(RSSalVar!ATTN_DATE), "", RSSalVar!ATTN_DATE), "DD/MM/YYYY")
        '
        '        sprdMain.Col = ColIN_O
        '        sprdMain.Text = Format(IIf(IsNull(RSSalVar!IN_TIME_O), "", RSSalVar!IN_TIME_O), "hh:mm")
        '
        '        sprdMain.Col = ColOUT_O
        '        sprdMain.Text = Format(IIf(IsNull(RSSalVar!OUT_TIME_O), "0", RSSalVar!OUT_TIME_O), "hh:mm")
        '
        '        sprdMain.Col = ColIN
        '        sprdMain.Text = Format(IIf(IsNull(RSSalVar!IN_TIME), "", RSSalVar!IN_TIME), "hh:mm")
        '
        '        sprdMain.Col = ColOUT
        '        sprdMain.Text = Format(IIf(IsNull(RSSalVar!OUT_TIME), "0", RSSalVar!OUT_TIME), "hh:mm")
        '
        '        CalcTotatHours mRow, mDate
        '    Else
        '        sprdMain.Row = mRow
        '
        '        sprdMain.Col = ColDate
        '        sprdMain.Text = Format(mDate, "DD/MM/YYYY")
        '
        '        sprdMain.Col = ColIN
        '        sprdMain.Text = "00:00"
        '
        '        sprdMain.Col = ColOUT
        '        sprdMain.Text = "00:00"
        '
        '        CalcTotatHours mRow, mDate
        '    End If
        '
        '    sprdMain.Row = mRow
        '    sprdMain.Col = ColShiftInTime
        '    sprdMain.Text = GetShiftTime(mCode, Format(mDate, "DD-MMM-YYYY"), 0, "I", "C")
        '
        '    If chkPunchData.Value = vbChecked Then
        '        sprdMain.Row = mRow
        '        sprdMain.Col = ColPunchData
        '        sprdMain.Text = GetPunchTime(mCode, mDate, "PAY_EMPLOYEE_MST")
        '    End If
        '
        '    Exit Function
        'ERR1:
        '    CalcVariable = False
    End Function
    'Private Function GetPunchTime(mEmpCode As String, mDate As String, mTable As String) As String
    'On Error GoTo refreshErrPart
    'Dim RsAttn As ADODB.Recordset = Nothing
    '
    '
    '    GetPunchTime = ""
    '
    '    SqlStr = " SELECT DISTINCT EMP.EMP_NAME, EMP.EMP_CODE, " & vbCrLf _
    ''            & " EMP.EMP_DEPT_CODE, TO_CHAR(OFFICEPUNCH,'DD-MON-YYYY HH24:MI') AS OFFICEPUNCH " & vbCrLf _
    ''            & " FROM " & mTable & " EMP, TEMPDATA SMST " & vbCrLf _
    ''            & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
    '
    '    If RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Then
    '        ''SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=TO_NUMBER(SUBSTR(SMST.CARDNO,1,LENGTH(trim(SMST.CARDNO))-6)) AND TRIM(EMP.EMP_CODE) = TRIM(SUBSTR(SMST.CARDNO,3)) AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"
    '        SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=TO_NUMBER(SUBSTR(SMST.CARDNO,1,LENGTH(trim(SMST.CARDNO))-6))"
    '        SqlStr = SqlStr & vbCrLf & " AND TRIM(EMP.EMP_CODE) = TRIM(SUBSTR(SMST.CARDNO,LENGTH(trim(SMST.CARDNO))-5)) "
    '        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"
    '
    '
    '    Else
    '        SqlStr = SqlStr & vbCrLf _
    ''                & " AND TRIM(EMP.EMP_CODE) = TRIM(SMST.CARDNO) " & vbCrLf _
    ''                & " AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"
    '
    '    End If
    '
    '    SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"
    '
    '    SqlStr = SqlStr & vbCrLf & "Order by OFFICEPUNCH"
    '
    '
    '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsAttn, adLockOptimistic
    '
    '    If RsAttn.EOF = False Then
    '        Do While Not RsAttn.EOF
    '            If GetPunchTime = "" Then
    '                GetPunchTime = Format(IIf(IsNull(RsAttn!OFFICEPUNCH), "", RsAttn!OFFICEPUNCH), "HH:MM")
    '            Else
    '                GetPunchTime = GetPunchTime + " " + Format(IIf(IsNull(RsAttn!OFFICEPUNCH), "", RsAttn!OFFICEPUNCH), "HH:MM")
    '            End If
    '            RsAttn.MoveNext
    '        Loop
    '    End If
    '    Exit Function
    'refreshErrPart:
    '    MsgBox err.Description
    ''    Resume
    'End Function

    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)

        If RsDept.EOF = False Then
            Do While RsDept.EOF = False
                cboDivision.Items.Add(RsDept.Fields("DIV_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1


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
        On Error GoTo ERR1
        With SprdMain
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)

            .set_ColWidth(ColSNO, 4)

            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDate, 10)

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColCode, 6)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColName, 20)

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColFName, 18)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColDept, 6)


            .Col = ColIN
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeTime
            .TypeTime24Hour = FPSpreadADO.TypeTime24HourConstants.TypeTime24Hour24HourClock
            .TypeTimeSeconds = False
            .TypeTimeSeparator = Asc(":")
            .TypeTimeMin = "000000"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            '        .TypeSpin = True
            .set_ColWidth(ColIN, 6)

            .Col = ColOUT
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeTime
            .TypeTime24Hour = FPSpreadADO.TypeTime24HourConstants.TypeTime24Hour24HourClock
            .TypeTimeSeconds = False
            .TypeTimeSeparator = Asc(":")
            .TypeTimeMin = "000000"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            ''        .TypeSpin = True
            .set_ColWidth(ColOUT, 6)

            .Col = ColTotHours
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeTime
            .TypeTime24Hour = FPSpreadADO.TypeTime24HourConstants.TypeTime24Hour24HourClock
            .TypeTimeSeconds = False
            .TypeTimeSeparator = Asc(":")
            .TypeTimeMin = "000000"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColTotHours, 6)
            '        .ColHidden = IIf(lblCategory.Caption = "G", False, True)


            '        .ColHidden = IIf(lblCategory.Caption = "G", False, True)

        End With

        MainClass.ProtectCell(SprdMain, 0, SprdMain.MaxRows, ColSNO, ColRefNo)
        MainClass.ProtectCell(SprdMain, 0, SprdMain.MaxRows, ColName, ColDept)
        MainClass.ProtectCell(SprdMain, 0, SprdMain.MaxRows, ColTotHours, ColTotHours)

        '    MainClass.ProtectCell sprdMain, 0, sprdMain.MaxRows, 0, ColTotHours
        '    MainClass.ProtectCell sprdMain, 0, sprdMain.MaxRows, ColTotHours, ColTotHours


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
    Private Function SalProcess(ByRef mYM As Integer) As Boolean

        On Error GoTo ErrSalProcess
        Dim RsMain As ADODB.Recordset
        SalProcess = True
        SqlStr = " SELECT EMP_CODE FROM PAY_SAL_TRN WHERE " & vbCrLf & " TO_CHAR(SAL_DATE,'YYYYMM') > " & mYM & "" & vbCrLf & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISARREAR IN ('N','Y')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockOptimistic)

        If RsMain.EOF = False Then
            SalProcess = False
        End If
        Exit Function
ErrSalProcess:
        SalProcess = False
    End Function
End Class
