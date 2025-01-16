Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmUpdateHoliday
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection
    Private Const RowHeight As Short = 12

    Dim mActiveRow As Integer

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub


    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
        On Error GoTo DataErr


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    Me.Height = 4020
        cmdOk.Enabled = False

        If Update1 = False Then GoTo DataErr

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub

DataErr:
        '    Resume
        MsgInformation(" Unable to Processed")
        cmdOk.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Me.Height = VB6.TwipsToPixelsY(3015)
        ''Resume Next
    End Sub

    Private Sub cmdUnProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUnProcess.Click

        On Error GoTo ErrPart1
        Dim SqlStr As String = ""

        If FieldsVarification = False Then GoTo ErrPart1

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "UPDATE PAY_ATTN_MST SET FIRSTHALF=-1" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIRSTHALF IN (" & SUNDAY & "," & HOLIDAY & ") " & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If OptParticularAccount.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtName.Text) & "' "
        End If

        PubDBCn.Execute(SqlStr)

        SqlStr = "UPDATE PAY_ATTN_MST SET SECONDHALF=-1" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SECONDHALF IN (" & SUNDAY & "," & HOLIDAY & ") " & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If OptParticularAccount.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtName.Text) & "' "
        End If

        PubDBCn.Execute(SqlStr)


        PubDBCn.CommitTrans()
        cmdUnProcess.Enabled = False
        Exit Sub
ErrPart1:
        '    Resume

        PubDBCn.RollbackTrans()
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If

    End Sub

    Private Sub frmUpdateHoliday_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmUpdateHoliday_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(3225)
        'Me.Width = VB6.TwipsToPixelsX(5775)
        txtDateFrom.Text = CStr(RunDate)
        txtDateTo.Text = CStr(RunDate)
        cmdUnProcess.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        optWeeklyType(0).Checked = IIf(RsCompany.Fields("WEEKLYOFF_TYPE").Value = "C", True, False) ''True
        optWeeklyType(1).Enabled = IIf(RsCompany.Fields("WEEKLYOFF_TYPE").Value = "C", False, True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Function Update1() As Boolean

        On Error GoTo ErrPart1
        Dim RsHoliday As ADODB.Recordset = Nothing
        Dim RsShift As ADODB.Recordset
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mHoliday As String
        Dim mHolidayType As String
        Dim mEmpCode As String
        Dim mFHalf As Short
        Dim mFSecond As Short
        Dim mCategory As String
        Dim mDept As String
        Dim mAgtLate As String
        Dim mCPLFH As String
        Dim mCPLSH As String
        Dim mCPLEarn As Double
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAppStaff As String
        Dim mAppRW As String
        Dim mCatType As String
        Dim mHolidayApp As String

        If FieldsVarification = False Then GoTo ErrPart1

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " SELECT HOLIDAY_DATE, LEAVE_TYPE,APP_STAFF,APP_RW  FROM PAY_HOLIDAY_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND HOLIDAY_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND HOLIDAY_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If optWeeklyType(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND LEAVE_TYPE<>'SD'"

            SqlStr = SqlStr & vbCrLf & " UNION "

            SqlStr = SqlStr & vbCrLf & " SELECT SHIFT_DATE AS HOLIDAY_DATE, 'SD' AS LEAVE_TYPE,'Y' AS APP_STAFF , 'Y' As APP_RW FROM PAY_SHIFT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SHIFT_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SHIFT_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND WEEKLY_OFF='Y'"

            If OptParticularAccount.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtName.Text) & "' "
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY 1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHoliday, ADODB.LockTypeEnum.adLockReadOnly)

        If RsHoliday.EOF = False Then
            Do While Not RsHoliday.EOF
                mHoliday = IIf(IsDbNull(RsHoliday.Fields("HOLIDAY_DATE").Value), "", RsHoliday.Fields("HOLIDAY_DATE").Value)
                mHolidayType = IIf(IsDbNull(RsHoliday.Fields("LEAVE_TYPE").Value), "", RsHoliday.Fields("LEAVE_TYPE").Value)
                mAppStaff = IIf(IsDbNull(RsHoliday.Fields("APP_STAFF").Value), "N", RsHoliday.Fields("APP_STAFF").Value)
                mAppRW = IIf(IsDbNull(RsHoliday.Fields("APP_RW").Value), "N", RsHoliday.Fields("APP_RW").Value)

                SqlStr = " SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_DOJ<=TO_DATE('" & VB6.Format(mHoliday, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mHoliday, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

                If OptParticularAccount.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtName.Text) & "' "
                End If

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsEmp.EOF = False Then
                    Do While Not RsEmp.EOF
                        mHolidayType = IIf(IsDbNull(RsHoliday.Fields("LEAVE_TYPE").Value), "", RsHoliday.Fields("LEAVE_TYPE").Value)
                        mCatType = IIf(IsDbNull(RsEmp.Fields("EMP_CAT_TYPE").Value), "1", RsEmp.Fields("EMP_CAT_TYPE").Value)

                        If mCatType = "1" Then
                            mHolidayApp = mAppStaff
                        Else
                            mHolidayApp = mAppRW
                        End If

                        mEmpCode = IIf(IsDbNull(RsEmp.Fields("EMP_CODE").Value), "", RsEmp.Fields("EMP_CODE").Value)
                        mDept = IIf(IsDbNull(RsEmp.Fields("EMP_DEPT_CODE").Value), "", RsEmp.Fields("EMP_DEPT_CODE").Value)
                        mCategory = IIf(IsDbNull(RsEmp.Fields("EMP_CATG").Value), "", RsEmp.Fields("EMP_CATG").Value)
                        If mHolidayType = "SD" Then
                            If optWeeklyType(1).Checked = True Then
                                If CheckWeeklyOff(mHoliday, mEmpCode) = False Then
                                    mHolidayType = ""
                                End If
                            Else
                                mHolidayType = IIf(mHolidayApp = "N", "", mHolidayType)
                            End If
                        Else
                            mHolidayType = IIf(mHolidayApp = "N", "", mHolidayType)
                        End If

                        If mHolidayType <> "" Then

                            If HoliDayExsits(mEmpCode, mHoliday, IIf(mHolidayType = "SD", SUNDAY, IIf(mHolidayType = "HH", HOLIDAY, ""))) = True Then
                                '                            MsgInformation "Attandance alreay enter for Emp Code : " & mEmpCode & " at Date : " & vb6.Format(mHoliday, "DD/MM/YYYY")
                                GoTo NextRec
                                Update1 = False
                                PubDBCn.RollbackTrans()
                                Exit Function
                            End If

                            If mHolidayType = "SD" Then
                                mFHalf = SUNDAY
                                mFSecond = SUNDAY
                            Else
                                mFHalf = HOLIDAY
                                mFSecond = HOLIDAY
                            End If

                            SqlStr = "SELECT * FROM PAY_ATTN_MST " & vbCrLf _
                                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf _
                                & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mHoliday, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                            If RsTemp.EOF = False Then
                                mAgtLate = IIf(IsDbNull(RsTemp.Fields("AGT_LATE").Value), "N", RsTemp.Fields("AGT_LATE").Value)
                                mCPLFH = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CPL_AGT_DATE_FH").Value), "", RsTemp.Fields("CPL_AGT_DATE_FH").Value), "DD/MM/YYYY")
                                mCPLSH = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CPL_AGT_DATE_SH").Value), "", RsTemp.Fields("CPL_AGT_DATE_SH").Value), "DD/MM/YYYY")
                                mCPLEarn = IIf(IsDbNull(RsTemp.Fields("CPL_EARN").Value), "0", RsTemp.Fields("CPL_EARN").Value)
                            Else
                                mAgtLate = "N"
                                mCPLFH = ""
                                mCPLSH = ""
                                mCPLEarn = 0
                            End If

                            SqlStr = "DELETE FROM PAY_ATTN_MST " & vbCrLf _
                                & " WHERE " & vbCrLf _
                                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf _
                                & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mHoliday, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                            PubDBCn.Execute(SqlStr)

                            SqlStr = "INSERT INTO PAY_ATTN_MST (" & vbCrLf _
                                & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
                                & " ATTN_DATE, FIRSTHALF, SECONDHALF, " & vbCrLf _
                                & " AGT_LATE, CPL_AGT_DATE_FH," & vbCrLf _
                                & " CPL_AGT_DATE_SH, CPL_EARN, " & vbCrLf _
                                & " ADDUSER, ADDDATE " & vbCrLf _
                                & ") VALUES (" & vbCrLf _
                                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(mHoliday)) & ", " & vbCrLf _
                                & " '" & mEmpCode & "', TO_DATE('" & VB6.Format(mHoliday, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                                & " " & mFHalf & ", " & mFSecond & ", " & vbCrLf _
                                & " '" & mAgtLate & "', TO_DATE('" & VB6.Format(mCPLFH, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                                & " TO_DATE('" & VB6.Format(mCPLSH, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mCPLEarn & ", " & vbCrLf _
                                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                            PubDBCn.Execute(SqlStr)
                        End If
NextRec:
                        RsEmp.MoveNext()
                    Loop
                End If
                RsHoliday.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
ErrPart1:
        '    Resume

        PubDBCn.RollbackTrans()
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
        Update1 = False
    End Function

    Private Function HoliDayExsits(ByRef pEmpCode As String, ByRef pHolidate As String, ByRef pHolidayType As Short) As Boolean

        On Error GoTo ErrPart1
        Dim RsHoliday As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mFHalf As Short
        Dim mFSecond As Short

        mFHalf = -1
        mFSecond = -1

        SqlStr = " SELECT FIRSTHALF, SECONDHALF FROM PAY_ATTN_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf _
            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pHolidate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHoliday, ADODB.LockTypeEnum.adLockReadOnly)

        If RsHoliday.EOF = False Then
            mFHalf = IIf(IsDbNull(RsHoliday.Fields("FIRSTHALF").Value), -1, RsHoliday.Fields("FIRSTHALF").Value)
            If mFHalf = pHolidayType Then
                mFHalf = -1
            End If

            mFSecond = IIf(IsDbNull(RsHoliday.Fields("SECONDHALF").Value), -1, RsHoliday.Fields("SECONDHALF").Value)
            If mFSecond = pHolidayType Then
                mFSecond = -1
            End If

        End If
        If (mFHalf = -1 And mFSecond = -1) Or (mFHalf = 10 And mFSecond = 10) Or (mFHalf = -1 And mFSecond = 10) Or (mFHalf = 10 And mFSecond = -1) Then
            HoliDayExsits = False
        Else
            HoliDayExsits = True
        End If
        Exit Function
ErrPart1:
        '    Resume
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
        HoliDayExsits = True
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo ErrPart1

        System.Windows.Forms.Application.DoEvents()

        If Trim(txtDateFrom.Text) = "" Then
            MsgInformation("From Date cann't be blank.")
            txtDateFrom.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtDateFrom.Text) Then
            MsgInformation("Invalid From Date.")
            txtDateFrom.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDateTo.Text) = "" Then
            MsgInformation("To Date cann't be blank.")
            txtDateTo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtDateTo.Text) Then
            MsgInformation("Invalid To Date.")
            txtDateTo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        Dim mFromdate As String
        Dim mToDate As String '

        mFromdate = "01/" & VB6.Format(txtDateFrom.Text, "MM/YYYY")
        mToDate = "01/" & VB6.Format(txtDateTo.Text, "MM/YYYY")

        If CDate(mFromdate) <> CDate(mToDate) Then
            MsgInformation("Please select the same Month.")
            FieldsVarification = False
            Exit Function
        End If

        If OptParticularAccount.Checked = True Then
            If Trim(txtName.Text) = "" Then
                MsgInformation("Please Select The Emp Name")
                If txtName.Enabled = True Then txtName.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If CheckSalaryMade("", VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")) = True Then
            MsgInformation("Salary already Made Againt This Month. So Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If

        FieldsVarification = True
        Exit Function
ErrPart1:
        MsgBox(Err.Description)
        FieldsVarification = False
    End Function

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        cmdOk.Enabled = True
    End Sub

    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        cmdOk.Enabled = True
    End Sub

    Private Sub OptAllAccount_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAllAccount.CheckedChanged
        If eventSender.Checked Then
            txtName.Enabled = False
            cmdsearch.Enabled = False
            cmdOk.Enabled = True
        End If
    End Sub
    Private Sub OptParticularAccount_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptParticularAccount.CheckedChanged
        If eventSender.Checked Then
            txtName.Enabled = True
            cmdsearch.Enabled = True
            cmdOk.Enabled = True
        End If
    End Sub
    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        Call cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo SearchErr
        Dim RsItem As ADODB.Recordset
        Dim SqlStr As String = ""
        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        txtName.Text = VB6.Format(txtName.Text, "000000")

        SqlStr = "SELECT EMP_CODE,EMP_NAME FROM PAY_EMPLOYEE_MST where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & MainClass.AllowSingleQuote(UCase(txtName.Text)) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItem, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItem.EOF = True Then
            MsgBox("Employee Name Not Exist In Master", MsgBoxStyle.Information)
            Cancel = True
        Else
            lblName.Text = IIf(IsDbNull(RsItem.Fields("EMP_NAME").Value), "", RsItem.Fields("EMP_NAME").Value)
        End If
        GoTo EventExitSub
SearchErr:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SearchErr
        Dim SqlStr As String = ""
        SqlStr = ""
        If MainClass.SearchGridMaster((txtName.Text), "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtName.Text = AcName
            lblName.Text = AcName1
            txtName.Focus()
        End If
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        On Error GoTo DataErr


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    Me.Height = 4020
        cmdOK.Enabled = False

        If UpdateAbsent1() = False Then GoTo DataErr

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub

DataErr:
        '    Resume
        MsgInformation(" Unable to Processed")
        cmdOK.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Me.Height = VB6.TwipsToPixelsY(3015)
        ''Resume Next
    End Sub
    Private Function UpdateAbsent1() As Boolean

        On Error GoTo ErrPart1
        Dim SqlStr As String = ""

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mEmpCode As String
        Dim mAttnDate As String
        Dim mIsAbsent As Boolean

        If FieldsVarification() = False Then GoTo ErrPart1

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " SELECT TRN.ATTN_DATE, TRN.EMP_CODE  FROM PAY_ATTN_MST TRN, PAY_EMPLOYEE_MST EMP" & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=EMP.COMPANY_CODE AND TRN.EMP_CODE=EMP.EMP_CODE" & vbCrLf _
            & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And TRN.FIRSTHALF=" & SUNDAY & "" & vbCrLf _
            & " And TRN.ATTN_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.ATTN_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf _
                    & " AND EMP.EMP_DOJ<=TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mAttnDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        SqlStr = SqlStr & vbCrLf & " ORDER BY EMP_CODE, ATTN_DATE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mEmpCode = IIf(IsDBNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
                mAttnDate = IIf(IsDBNull(RsTemp.Fields("ATTN_DATE").Value), "", RsTemp.Fields("ATTN_DATE").Value)

                mIsAbsent = GetMarkAbsent(mEmpCode, mAttnDate, "S")

                If mIsAbsent = True Then
                    SqlStr = "UPDATE PAY_ATTN_MST SET " & vbCrLf _
                        & " FIRSTHALF=" & ABSENT & "," & vbCrLf _
                        & " SECONDHALF=" & ABSENT & "" & vbCrLf _
                        & " WHERE " & vbCrLf _
                        & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf _
                        & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    PubDBCn.Execute(SqlStr)
                End If

                RsTemp.MoveNext()
            Loop
        End If

        SqlStr = " SELECT TRN.ATTN_DATE, TRN.EMP_CODE  FROM PAY_ATTN_MST TRN, PAY_EMPLOYEE_MST EMP" & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=EMP.COMPANY_CODE AND TRN.EMP_CODE=EMP.EMP_CODE" & vbCrLf _
            & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And TRN.FIRSTHALF=" & HOLIDAY & "" & vbCrLf _
            & " And TRN.ATTN_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.ATTN_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf _
                    & " AND EMP.EMP_DOJ<=TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mAttnDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        SqlStr = SqlStr & vbCrLf & " ORDER BY EMP_CODE, ATTN_DATE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mEmpCode = IIf(IsDBNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
                mAttnDate = IIf(IsDBNull(RsTemp.Fields("ATTN_DATE").Value), "", RsTemp.Fields("ATTN_DATE").Value)

                mIsAbsent = GetMarkAbsent(mEmpCode, mAttnDate, "H")

                If mIsAbsent = True Then
                    SqlStr = "UPDATE PAY_ATTN_MST SET " & vbCrLf _
                        & " FIRSTHALF=" & ABSENT & "," & vbCrLf _
                        & " SECONDHALF=" & ABSENT & "" & vbCrLf _
                        & " WHERE " & vbCrLf _
                        & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf _
                        & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    PubDBCn.Execute(SqlStr)
                End If

                RsTemp.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()
        UpdateAbsent1 = True
        Exit Function
ErrPart1:
        '    Resume

        PubDBCn.RollbackTrans()
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
        UpdateAbsent1 = False
    End Function
End Class
