Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmOverTimeHead
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
        txtOTHour.Maxlength = 2
        txtOTMin.Maxlength = 2
        txtPrevOTHour.MaxLength = 3
        txtPrevOTMin.Maxlength = 2
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub chkClear_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkClear.CheckStateChanged
        If chkClear.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtOTHour.Text = ""
            txtOTMin.Text = ""
            txtOTHour.Enabled = False
            txtOTMin.Enabled = False

            txtPrevOTHour.Text = ""
            txtPrevOTMin.Text = ""
            txtPrevOTHour.Enabled = False
            txtPrevOTMin.Enabled = False

        Else
            txtOTHour.Enabled = True
            txtOTMin.Enabled = True

            If VB6.Format(lblDate.Text, "DD") = "01" Then
                txtPrevOTHour.Enabled = True
                txtPrevOTMin.Enabled = True
            Else
                txtPrevOTHour.Enabled = False
                txtPrevOTMin.Enabled = False
            End If
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        On Error GoTo UpdateErr
        If FieldsVarification = False Then Exit Sub
        If CDbl(lblType.Text) = 1 Then
            Update1()
        ElseIf CDbl(lblType.Text) = 2 Then
            Update2()
        End If
        Exit Sub
UpdateErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub frmOverTimeHead_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Show1()
    End Sub
    Private Sub frmOverTimeHead_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmOverTimeHead_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
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

        settextlength()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim mOTHour As Integer
        Dim mOTMin As Integer
        Dim mOTType As String
        Dim mPrevOTHour As Integer
        Dim mPrevOTMin As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM PAY_OVERTIME_MST  WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & lblCode.Text & "'" & vbCrLf _
            & " AND OT_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        If chkClear.CheckState = System.Windows.Forms.CheckState.Checked Then GoTo TransCommit

        mOTHour = Val(txtOTHour.Text)
        mOTMin = Val(txtOTMin.Text)

        mPrevOTHour = Val(txtPrevOTHour.Text)
        mPrevOTMin = Val(txtPrevOTMin.Text)

        If optOTType(0).Checked = True Then
            mOTType = "0"
        ElseIf optOTType(1).Checked = True Then
            mOTType = "1"
        Else
            mOTType = "2"
        End If

        SqlStr = "INSERT INTO PAY_OVERTIME_MST ( " & vbCrLf _
            & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
            & " OT_DATE, OTHOUR, OTMIN, OTTYPE," & vbCrLf _
            & " PREV_OTHOUR,  PREV_OTMIN, " & vbCrLf _
            & " ADDUSER, ADDDATE) VALUES (" & vbCrLf _
            & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Year(CDate(lblDate.Text)) & ", " & vbCrLf _
            & " '" & lblCode.Text & "', " & vbCrLf _
            & " TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & "  " & mOTHour & ", " & mOTMin & ", '" & mOTType & "'," & vbCrLf _
            & "  " & mPrevOTHour & ", " & mPrevOTMin & ", " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

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
    Private Function Update2() As Boolean

        On Error GoTo UpdateError
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mOTHour As Integer
        Dim mOTMin As Integer
        Dim mOTType As String
        Dim mKey As String
        Dim mPrevOTHour As Integer
        Dim mPrevOTMin As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM PAY_OVERTIME_MST  WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND OT_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        If chkClear.CheckState = System.Windows.Forms.CheckState.Unchecked Then GoTo TransCommit

        mOTHour = Val(txtOTHour.Text)
        mOTMin = Val(txtOTMin.Text)

        mPrevOTHour = Val(txtPrevOTHour.Text)
        mPrevOTMin = Val(txtPrevOTHour.Text)

        If optOTType(0).Checked = True Then
            mOTType = "0"
        ElseIf optOTType(1).Checked = True Then
            mOTType = "1"
        Else
            mOTType = "2"
        End If

        SqlStr = "SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE" & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf _
            & " EMP.EMP_DOJ <=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf _
            & " (EMP.DOL>TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') OR EMP.DOL IS NULL) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenForwardOnly, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        Do While Not RsEmp.EOF
            SqlStr = "INSERT INTO PAY_OVERTIME_MST ( " & vbCrLf _
                & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
                & " OT_DATE, OTHOUR, OTMIN, OTTYPE, " & vbCrLf _
                & " PREV_OTHOUR,  PREV_OTMIN, " & vbCrLf _
                & " ADDUSER, ADDDATE) VALUES (" & vbCrLf _
                & " " & RsCompany.Fields("CompanyCode").Value & "," & Year(CDate(lblDate.Text)) & ", " & vbCrLf _
                & " '" & RsEmp.Fields("EMP_CODE").Value & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & "  " & mOTHour & ", " & mOTMin & ", '" & mOTType & "'," & vbCrLf _
                & "  " & mPrevOTHour & ", " & mPrevOTMin & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            PubDBCn.Execute(SqlStr)

            RsEmp.MoveNext()
        Loop

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
    Private Sub Show1()

        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mCode As String
        Dim mOTType As Short

        If VB6.Format(lblDate.Text, "DD") = "01" Then
            txtPrevOTHour.Enabled = True
            txtPrevOTMin.Enabled = True
        Else
            txtPrevOTHour.Enabled = False
            txtPrevOTMin.Enabled = False
        End If

        SqlStr = " SELECT OTHOUR , OTMIN, OTTYPE,PREV_OTHOUR,PREV_OTMIN " & vbCrLf & " FROM PAY_OVERTIME_MST WHERE " & vbCrLf & " Emp_Code ='" & lblCode.Text & "' AND  " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND  " & vbCrLf & " OT_DATE=TO_DATE('" & VB6.Format(lblDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockReadOnly)

        If RsAttn.EOF = False Then
            txtOTHour.Text = IIf(IsDbNull(RsAttn.Fields("OTHOUR").Value), "", RsAttn.Fields("OTHOUR").Value)
            txtOTMin.Text = IIf(IsDbNull(RsAttn.Fields("OTMIN").Value), "", RsAttn.Fields("OTMIN").Value)
            txtPrevOTHour.Text = IIf(IsDbNull(RsAttn.Fields("PREV_OTHOUR").Value), "", RsAttn.Fields("PREV_OTHOUR").Value)
            txtPrevOTMin.Text = IIf(IsDbNull(RsAttn.Fields("PREV_OTMIN").Value), "", RsAttn.Fields("PREV_OTMIN").Value)
            mOTType = Val(IIf(IsDbNull(RsAttn.Fields("OTTYPE").Value), "0", RsAttn.Fields("OTTYPE").Value))
            optOTType(mOTType).Checked = True
        Else
            chkClear.CheckState = System.Windows.Forms.CheckState.Unchecked
        End If
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1

        FieldsVarification = True
        If chkClear.CheckState = System.Windows.Forms.CheckState.Checked Then Exit Function

        If Val(txtOTHour.Text) = 0 And Val(txtOTMin.Text) = 0 And Val(txtPrevOTHour.Text) = 0 And Val(txtPrevOTMin.Text) = 0 Then
            MsgInformation("Over Time Hour is empty. Cannot Save")
            txtOTHour.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtOTHour.Text) > 23 Then
            MsgInformation("Invaild Over Time Hour. Cannot Save")
            txtOTHour.Focus()
            FieldsVarification = False
            Exit Function
        End If


        'If PubSuperUser = "U" Then
        '    If Val(txtOTHour.Text) > 16 Then
        '        MsgInformation("Plant Head Approval for More than 16 Hours Over Time. Cannot Save")
        '        txtOTHour.Focus()
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If

        If Val(txtOTMin.Text) > 59 Then
            MsgInformation("Invaild Over Time Min.")
            txtOTMin.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtPrevOTMin.Text) > 59 Then
            MsgInformation("Invaild Previous Month Over Time Min.")
            txtPrevOTMin.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If CheckOTMade((lblCode.Text), VB6.Format(lblDate.Text, "DD/MM/YYYY")) = True Then
            MsgInformation("Over Time Made Againt This Month. So Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If

        '    If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
        '
        '    Else
        If ValidateOTFromMachine((lblCode.Text), VB6.Format(lblDate.Text, "DD/MM/YYYY")) = False Then
            'If RsCompany.Fields("COMPANY_CODE").Value = 1 Then ''Or PubUserID = "G0416"  ''And CDate(Format(lblDate, "DD/MM/YYYY")) < CDate("01/04/2014"))
            If MsgQuestion("Over Time Hours Not Match With Daily Attn Record. Are you want to proceed? ") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                Exit Function
            End If
            'Else
            '    MsgInformation("Over Time Hours Not Match With Daily Attn Record, So cann't be Saved.")
            '    FieldsVarification = False
            '    Exit Function
            'End If
        End If
        '    End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        'Resume
    End Function

    Private Function CheckOTMade(ByRef xEmpCode As String, ByRef xSalDate As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckDate As String

        CheckOTMade = False
        mCheckDate = MainClass.LastDay(Month(CDate(xSalDate)), Year(CDate(xSalDate))) & "/" & VB6.Format(xSalDate, "MM/YYYY")

        SqlStr = "SELECT * FROM PAY_MONTHLY_OT_TRN WHERE " & vbCrLf _
            & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(OT_DATE,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mCheckDate, "MMM-YYYY")) & "')"

        ''& " AND EMP_CODE='" & MainClass.AllowSingleQuote(xEmpCode) & "'" & vbCrLf _
        '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckOTMade = True
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function ValidateOTFromMachine(ByRef xEmpCode As String, ByRef xSalDate As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTotOTHours As Double
        Dim mHours As Double
        Dim mMin As Double
        Dim mActualTime As Date
        Dim mPaidTime As Date

        mHours = 0
        mMin = 0
        ValidateOTFromMachine = False

        If RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 15 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Then

        Else
            ValidateOTFromMachine = True
            Exit Function
        End If


        If chkClear.CheckState = System.Windows.Forms.CheckState.Checked Then
            ValidateOTFromMachine = True
            Exit Function
        End If

        If Val(txtOTHour.Text) + Val(txtOTMin.Text) = 0 Then
            ValidateOTFromMachine = True
            Exit Function
        End If

        mPaidTime = CDate(VB6.Format(Val(txtOTHour.Text) & ":" & Val(txtOTMin.Text), "HH:MM"))

        SqlStr = " SELECT TRN.OT_HOURS " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN TRN" & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.EMP_CODE = '" & MainClass.AllowSingleQuote(xEmpCode) & "'" & vbCrLf & " AND TRN.ATTN_DATE = TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mTotOTHours = Val(IIf(IsDbNull(RsTemp.Fields("OT_HOURS").Value), 0, RsTemp.Fields("OT_HOURS").Value))
            mTotOTHours = mTotOTHours * 0.5
            mHours = Int(mTotOTHours)
            mMin = (mTotOTHours - Int(mTotOTHours)) * 60
            mActualTime = CDate(VB6.Format(Val(CStr(mHours)) & ":" & Val(CStr(mMin)), "HH:MM"))
        End If

        If Not IsDate(mActualTime) Then
            ValidateOTFromMachine = False
            Exit Function
        End If
        If CDate(mPaidTime) > CDate(mActualTime) Then
            ValidateOTFromMachine = False
            Exit Function
        Else
            ValidateOTFromMachine = True
            Exit Function
        End If
        '    If Val(txtOTHour.Text) <> mHours Then
        '        ValidateOTFromMachine = False
        '        Exit Function
        '    End If
        '
        '    If Val(txtOTMin.Text) <> mMin Then
        '        ValidateOTFromMachine = False
        '        Exit Function
        '    End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub txtOTHour_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOTHour.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOTMin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOTMin.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOTMin_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOTMin.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtOTMin.Text = VB6.Format(txtOTMin.Text, "00")
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPrevOTHour_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPrevOTHour.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPrevOTMin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPrevOTMin.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPrevOTMin_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPrevOTMin.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtPrevOTMin.Text = VB6.Format(txtPrevOTMin.Text, "00")
        eventArgs.Cancel = Cancel
    End Sub
End Class
