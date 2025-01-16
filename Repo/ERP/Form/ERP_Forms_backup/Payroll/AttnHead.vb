Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAttnHead
    Inherits System.Windows.Forms.Form
    Dim RsEmp As ADODB.Recordset = Nothing

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection


    Dim SqlStr As String = ""
    Dim FormActive As Boolean

    Private Sub chkFHClear_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFHClear.CheckStateChanged
        Dim cntCount As Short
        If chkFHClear.CheckState = System.Windows.Forms.CheckState.Checked Then
            For cntCount = 0 To 9
                Call ClearOption(optFHalf(cntCount), True)
            Next
            chkSHClear.CheckState = System.Windows.Forms.CheckState.Checked
        Else
            For cntCount = 0 To 9
                Call ClearOption(optFHalf(cntCount), False)
            Next
            optFHalf(0).Checked = True
            chkSHClear.CheckState = System.Windows.Forms.CheckState.Unchecked
        End If
    End Sub

    Private Sub chkSHClear_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSHClear.CheckStateChanged
        Dim cntCount As Short
        If chkSHClear.CheckState = System.Windows.Forms.CheckState.Checked Then
            For cntCount = 0 To 9
                Call ClearOption(optSHalf(cntCount), True)
            Next
        Else
            For cntCount = 0 To 9
                Call ClearOption(optSHalf(cntCount), False)
            Next
            optSHalf(0).Checked = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click

        If PubSuperUser <> "S" Then
            If CheckSalaryMade((lblCode.Text), VB6.Format(lblDate.Text, "DD/MM/YYYY")) = True Then
                MsgInformation("Salary Made Againt This Month. So Cann't be Modified")
                Exit Sub
            End If
        End If

        If CDbl(lblType.Text) = 1 Then
            Update1()
        ElseIf CDbl(lblType.Text) = 2 Then
            Update2()
        End If
    End Sub

    Private Sub frmAttnHead_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Show1()
    End Sub

    Private Sub frmAttnHead_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmAttnHead_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Me.Height = VB6.TwipsToPixelsY(5760)
        'Me.Width = VB6.TwipsToPixelsX(4455)
        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)

        If ConAttnDataFromMC = True Then
            cmdOk.Enabled = False
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub ClearOption(ByRef moptButton As System.Windows.Forms.RadioButton, ByRef mCheck As Boolean)
        If mCheck = True Then
            moptButton.Checked = 0
            moptButton.Enabled = False
        Else
            moptButton.Enabled = True
        End If
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim mFHalf As Integer
        Dim mFSecond As Integer
        Dim mCount As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM PAY_ATTN_MST " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & lblCode.Text & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        If chkFHClear.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            For mCount = 0 To 9
                If optFHalf(mCount).Checked = True Then
                    mFHalf = mCount
                End If
            Next
        Else
            mFHalf = -1
        End If
        mCount = 0
        If chkSHClear.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            For mCount = 0 To 9
                If optSHalf(mCount).Checked = True Then
                    mFSecond = mCount
                End If
            Next
        Else
            mFSecond = -1
        End If

        SqlStr = "INSERT INTO PAY_ATTN_MST (" & vbCrLf & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf & " ATTN_DATE, FIRSTHALF, SECONDHALF, " & vbCrLf & " ADDUSER, ADDDATE) VALUES (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(lblDate.Text)) & ", " & vbCrLf & " '" & lblCode.Text & "', TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & "  " & mFHalf & ", " & mFSecond & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        Me.Hide()
        Exit Function
UpdateError:
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function Update2() As Boolean

        On Error GoTo UpdateError
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mFHalf As Integer
        Dim mFSecond As Integer
        Dim mCount As Integer
        Dim mCode As String
        Dim AttnDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM PAY_ATTN_MST " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        If chkFHClear.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            For mCount = 0 To 9
                If optFHalf(mCount).Checked = True Then
                    mFHalf = mCount
                End If
            Next
        Else
            mFHalf = -1
        End If
        mCount = 0
        If chkSHClear.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            For mCount = 0 To 9
                If optSHalf(mCount).Checked = True Then
                    mFSecond = mCount
                End If
            Next
        Else
            mFSecond = -1
        End If

        AttnDate = VB6.Format(lblDate.Text, "DD/MM/YYYY")

        SqlStr = "SELECT EMP_CODE FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(AttnDate, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " AND (EMP_LEAVE_DATE>TO_DATE('" & VB6.Format(AttnDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        SqlStr = SqlStr & " AND EMP_CATG<>'C' AND EMP_STOP_SALARY='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenForwardOnly, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        Do While Not RsEmp.EOF

            SqlStr = "INSERT INTO PAY_ATTN_MST ( " & vbCrLf & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf & " ATTN_DATE, FIRSTHALF, SECONDHALF, " & vbCrLf & " ADDUSER, ADDDATE) VALUES (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(lblDate.Text)) & ", " & vbCrLf & " '" & RsEmp.Fields("EMP_CODE").Value & "', TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & "  " & mFHalf & ", " & mFSecond & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            PubDBCn.Execute(SqlStr)

            RsEmp.MoveNext()
        Loop


        PubDBCn.CommitTrans()
        Me.Hide()
        Exit Function
UpdateError:
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub Show1()

        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mCode As String

        SqlStr = " SELECT FIRSTHALF , SECONDHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & lblCode.Text & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            If RsAttn.Fields("FIRSTHALF").Value = -1 Or IsDbNull(RsAttn.Fields("FIRSTHALF").Value) Then
                chkFHClear.CheckState = System.Windows.Forms.CheckState.Checked
            Else
                optFHalf(RsAttn.Fields("FIRSTHALF").Value).Checked = True
            End If

            If RsAttn.Fields("SECONDHALF").Value = -1 Or IsDbNull(RsAttn.Fields("SECONDHALF").Value) Then
                chkSHClear.CheckState = System.Windows.Forms.CheckState.Checked
            Else
                optSHalf(RsAttn.Fields("SECONDHALF").Value).Checked = True
            End If
        Else
            chkFHClear.CheckState = System.Windows.Forms.CheckState.Unchecked
            chkSHClear.CheckState = System.Windows.Forms.CheckState.Unchecked
        End If

        Call FillLeaves((lblCode.Text))
    End Sub

    Private Sub optFHalf_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optFHalf.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optFHalf.GetIndex(eventSender)
            optSHalf(Index).Checked = optFHalf(Index).Checked
        End If
    End Sub

    Private Sub FillLeaves(ByRef mCode As String)

        Dim RsOpLeave As ADODB.Recordset
        Dim RsLeave As ADODB.Recordset
        Dim mOpSick As Double
        Dim mOpCasual As Double
        Dim mOpEL As Double

        Dim mSick As Double
        Dim mCasual As Double
        Dim mEL As Double
        Dim mCPL As Double
        Dim mCPL_A As Double
        Dim mDOJ As String

        Dim mMonth As Short
        Dim mYear As Short

        Dim I As Integer
        Dim mMonField As Object
        Dim mon As String
        Dim mPeriod As Double

        Dim mStartDate As String
        Dim mYearDays As Integer
        Dim xSalDate As String

        If MainClass.ValidateWithMasterTable((lblCode.Text), "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOJ = MasterNo
        End If
        xSalDate = VB6.Format(lblDate.Text, "DD/MM/YYYY")

        mOpEL = GETEntitleEarnLeave(PubDBCn, mCode, EARN, xSalDate)
        mCPL = GETCPL(PubDBCn, mCode, xSalDate)


        If Year(CDate(xSalDate)) = Year(CDate(mDOJ)) Then
            mStartDate = mDOJ
        Else
            mStartDate = "01/01/" & VB6.Format(xSalDate, "YYYY")
        End If

        mYearDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate("01/01/" & VB6.Format(xSalDate, "YYYY")), CDate("31/12/" & VB6.Format(xSalDate, "YYYY"))) + 1

        mPeriod = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(xSalDate)) + 1
        mPeriod = CDbl(VB6.Format(mPeriod / mYearDays, "0.00000"))

        '    mPeriod = Round(Month(lblDate.Caption) / 12, 2)

        SqlStr = " SELECT NVL(OPENING,0) AS OPENING, NVL(TOTENTITLE,0) AS  TOTENTITLE, LEAVECODE " & vbCrLf & " FROM PAY_OPLEAVE_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR =" & Year(CDate(lblDate.Text)) & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOpLeave.EOF = False Then
            Do While Not RsOpLeave.EOF
                If RsOpLeave.Fields("LeaveCode").Value = SICK Then
                    mOpSick = IIf(IsDbNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                    mOpSick = mOpSick + IIf(IsDbNull(RsOpLeave.Fields("TOTENTITLE").Value), 0, RsOpLeave.Fields("TOTENTITLE").Value) * mPeriod
                    mOpSick = System.Math.Round(mOpSick * 2, 0) / 2
                ElseIf RsOpLeave.Fields("LeaveCode").Value = CASUAL Then
                    mOpCasual = IIf(IsDbNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                    mOpCasual = mOpCasual + IIf(IsDbNull(RsOpLeave.Fields("TOTENTITLE").Value), 0, RsOpLeave.Fields("TOTENTITLE").Value) * mPeriod
                    mOpCasual = System.Math.Round(mOpCasual * 2, 0) / 2
                ElseIf RsOpLeave.Fields("LeaveCode").Value = EARN Then
                    mOpEL = mOpEL + IIf(IsDbNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                End If

                RsOpLeave.MoveNext()
            Loop
        End If

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR =" & Year(CDate(lblDate.Text)) & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLeave.EOF = False Then
            Do While Not RsLeave.EOF
                If RsLeave.Fields("FIRSTHALF").Value = SICK And RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mSick = mSick + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = SICK Or RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mSick = mSick + 0.5
                End If

                If RsLeave.Fields("FIRSTHALF").Value = CASUAL And RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                    mCasual = mCasual + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = CASUAL Or RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                End If

                If RsLeave.Fields("FIRSTHALF").Value = EARN And RsLeave.Fields("SECONDHALF").Value = EARN Then
                    mEL = mEL + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = EARN Or RsLeave.Fields("SECONDHALF").Value = EARN Then
                    mEL = mEL + 0.5
                End If

                '            If RsLeave!FIRSTHALF = CPLEARN And RsLeave!SECONDHALF = CPLEARN Then
                '                mCPL = mCPL + 1
                '            ElseIf RsLeave!FIRSTHALF = CPLEARN Or RsLeave!SECONDHALF = CPLEARN Then
                '                mCPL = mCPL + 0.5
                '            End If

                If RsLeave.Fields("FIRSTHALF").Value = CPLAVAIL And RsLeave.Fields("SECONDHALF").Value = CPLAVAIL Then
                    mCPL_A = mCPL_A + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = CPLAVAIL Or RsLeave.Fields("SECONDHALF").Value = CPLAVAIL Then
                    mCPL_A = mCPL_A + 0.5
                End If

                mCPL = mCPL + IIf(IsDbNull(RsLeave.Fields("CPL_EARN").Value), 0, RsLeave.Fields("CPL_EARN").Value) * 0.5
                RsLeave.MoveNext()
            Loop
        End If

        lblBalSL.Text = VB6.Format(mOpSick - mSick, "0.0")
        lblBalCL.Text = VB6.Format(mOpCasual - mCasual, "0.0")
        lblBalEL.Text = VB6.Format(mOpEL - mEL, "0.0")
        lblBalCPL.Text = VB6.Format(mCPL - mCPL_A, "0.0")

        lblAvlSL.Text = VB6.Format(mSick, "0.0")
        lblAvlCL.Text = VB6.Format(mCasual, "0.0")
        lblAvlEL.Text = VB6.Format(mEL, "0.0")
        lblAvlCPL.Text = VB6.Format(mCPL_A, "0.0")

    End Sub

    Private Function CheckSalaryMade(ByRef xEmpCode As String, ByRef xSalDate As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckDate As String

        CheckSalaryMade = False
        mCheckDate = MainClass.LastDay(Month(CDate(xSalDate)), Year(CDate(xSalDate))) & "/" & VB6.Format(xSalDate, "MM/YYYY")

        SqlStr = " SELECT * FROM PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(xEmpCode) & "'" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSalaryMade = True
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
