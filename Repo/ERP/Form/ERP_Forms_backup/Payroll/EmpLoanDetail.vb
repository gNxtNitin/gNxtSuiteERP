Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmpLoanDetail
    Inherits System.Windows.Forms.Form
    Dim RsEmp As ADODB.Recordset = Nothing


    Dim XRIGHT As String


    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Dim xEmpCode As String

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub frmEmpLoanDetail_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmEmpLoanDetail_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim mPunchOption As String
        Dim mShiftOption As String
        Dim mDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        mDate = VB6.Format(txtLoanDate.Text, "DD/MM/YYYY")


        SqlStr = "UPDATE PAY_EMPLOYEE_MST SET" & vbCrLf _
            & " LOAN_AMOUNT = " & Val(txtLoanAmount.Text) & ", " & vbCrLf _
            & " LOAN_DATE = TO_DATE('" & VB6.Format(txtLoanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " ADV_LOAN_DED = " & Val(txtInstallmentAmount.Text) & ", " & vbCrLf _
            & " LOAN_PERIOD = " & Val(txtInstallmentPeriod.Text) & ", " & vbCrLf _
            & " LOAN_FROM_DATE = TO_DATE('" & VB6.Format(txtLoanFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " LOAN_TO_DATE = TO_DATE('" & VB6.Format(txtLoanToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & txtEmpCode.Text & "'"

        PubDBCn.Execute(SqlStr)


        PubDBCn.CommitTrans()
        Update1 = True
        '    Unload Me
        Exit Function
UpdateError:
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub Clear1()


        txtEmpCode.Text = ""
        TxtEmpName.Text = ""
        txtDept.Text = ""

        txtLoanAmount.Text = ""
        txtLoanDate.Text = ""
        txtInstallmentAmount.Text = ""
        txtInstallmentPeriod.Text = ""
        txtLoanFromDate.Text = ""
        txtLoanToDate.Text = ""

        CmdSave.Enabled = True
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmpCode.Text = AcName1
            TxtEmpName.Text = AcName
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
    End Sub

    Private Sub frmEmpLoanDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub


        'Me.Text = "Employee Punch Option"

        SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST WHERE  1<>1"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockReadOnly)
        Clear1()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmEmpLoanDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(3675)
        Me.Width = VB6.TwipsToPixelsX(8340)
        Me.Text = "Employee Punch Option"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmEmpLoanDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsEmp = Nothing
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
            CmdSave.Enabled = False
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1

        FieldsVarification = True

        If txtEmpCode.Text = "" Then
            MsgInformation("Please Entered Emp Code.")
            txtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtLoanDate.Text) = "" Then
            MsgInformation("Please Enter the Loan Date.")
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtLoanDate.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtLoanFromDate.Text) = "" Then
            MsgInformation("Please Enter the Loan Date From.")
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtLoanFromDate.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtLoanToDate.Text) = "" Then
            MsgInformation("Please Enter the Loan Date To.")
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtLoanToDate.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1


        txtEmpCode.MaxLength = MainClass.SetMaxLength("EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn)
        TxtEmpName.MaxLength = MainClass.SetMaxLength("EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn)
        txtDept.MaxLength = MainClass.SetMaxLength("EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn)


        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '' Resume
    End Sub


    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged
        CmdSave.Enabled = True
    End Sub

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged
        CmdSave.Enabled = True
    End Sub

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim mPunchOption As String
        Dim mShiftOption As String

        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub


        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")

        SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        If RS.EOF = False Then
            txtEmpCode.Text = RS.Fields("EMP_CODE").Value
            TxtEmpName.Text = IIf(IsDBNull(RS.Fields("EMP_NAME").Value), "", RS.Fields("EMP_NAME").Value)
            txtDept.Text = IIf(IsDBNull(RS.Fields("EMP_DEPT_CODE").Value), "", RS.Fields("EMP_DEPT_CODE").Value)
            'mPunchOption = IIf(IsDBNull(RS.Fields("PUNCH_OPT").Value), "P", RS.Fields("PUNCH_OPT").Value)
            'mShiftOption = IIf(IsDBNull(RS.Fields("SHIFT_OPTION").Value), "G", RS.Fields("SHIFT_OPTION").Value)
            'txtLoanDate.Text = VB6.Format(IIf(IsDBNull(RS.Fields("PUNCH_STOP_DATE").Value), "", RS.Fields("PUNCH_STOP_DATE").Value), "DD/MM/YYYY")

            txtLoanAmount.Text = Val(IIf(IsDBNull(RS.Fields("LOAN_AMOUNT").Value), 0, RS.Fields("LOAN_AMOUNT").Value))
            txtLoanDate.Text = VB6.Format(IIf(IsDBNull(RS.Fields("LOAN_DATE").Value), "", RS.Fields("LOAN_DATE").Value), "DD/MM/YYYY")
            txtInstallmentAmount.Text = Val(IIf(IsDBNull(RS.Fields("ADV_LOAN_DED").Value), 0, RS.Fields("ADV_LOAN_DED").Value))
            txtInstallmentPeriod.Text = Val(IIf(IsDBNull(RS.Fields("LOAN_PERIOD").Value), 0, RS.Fields("LOAN_PERIOD").Value))
            txtLoanFromDate.Text = VB6.Format(IIf(IsDBNull(RS.Fields("LOAN_FROM_DATE").Value), "", RS.Fields("LOAN_FROM_DATE").Value), "DD/MM/YYYY")
            txtLoanToDate.Text = VB6.Format(IIf(IsDBNull(RS.Fields("LOAN_TO_DATE").Value), "", RS.Fields("LOAN_TO_DATE").Value), "DD/MM/YYYY")

        Else
            MsgBox("This Employee Code does not exsits in Master.", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:

        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtEmpName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtEmpName.TextChanged
        CmdSave.Enabled = True
    End Sub

    Private Sub TxtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtLoanDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoanDate.TextChanged
        CmdSave.Enabled = True
    End Sub
    Private Sub txtLoanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLoanDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtLoanDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtLoanDate.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtLoanFromDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoanFromDate.TextChanged
        CmdSave.Enabled = True
    End Sub


    Private Sub txtLoanFromDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLoanFromDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtLoanFromDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtLoanFromDate.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtLoanToDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoanToDate.TextChanged
        CmdSave.Enabled = True
    End Sub

    Private Sub txtLoanAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoanAmount.TextChanged, txtInstallmentAmount.TextChanged, txtInstallmentPeriod.TextChanged
        CmdSave.Enabled = True
    End Sub
    Private Sub txtLoanToDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLoanToDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtLoanToDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtLoanToDate.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtInstallmentAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInstallmentAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtInstallmentPeriod_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInstallmentPeriod.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtLoanAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLoanAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
