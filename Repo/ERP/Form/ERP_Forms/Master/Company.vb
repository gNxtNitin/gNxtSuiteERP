Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCompany
   Inherits System.Windows.Forms.Form
   Private mCompanyCode As Integer
   Private ADDMode As Boolean
   Private MODIFYMode As Boolean
   Private XRIGHT As String
   'Private PvtDBCn As ADODB.Connection	

   Private Sub Update1()
      '		
      On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mHOCode As Integer
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " UPDATE GEN_COMPANY_MST Set COMPANY_NAME='" & MainClass.AllowSingleQuote(txtCompanyName.Text) & "', " & vbCrLf _
            & " COMPANY_ADDR='" & MainClass.AllowSingleQuote(txtaddress.Text) & "', " & vbCrLf _
            & " COMPANY_CITY='" & MainClass.AllowSingleQuote(txtCity.Text) & "', " & vbCrLf _
            & " COMPANY_STATE='" & MainClass.AllowSingleQuote(txtState.Text) & "', " & vbCrLf _
            & " COMPANY_PIN='" & MainClass.AllowSingleQuote(txtPin.Text) & "', " & vbCrLf _
            & " COMPANY_PHONE='" & MainClass.AllowSingleQuote(txtPhone.Text) & "', " & vbCrLf _
            & " COMPANY_FAXNO='" & MainClass.AllowSingleQuote(txtFax.Text) & "', " & vbCrLf _
            & " COMPANY_MAILID='" & MainClass.AllowSingleQuote(txtEmail.Text) & "', " & vbCrLf _
            & " REGD_ADDR1='" & MainClass.AllowSingleQuote(txtRegdAdd1.Text) & "', " & vbCrLf _
            & " REGD_ADDR2='" & MainClass.AllowSingleQuote(txtRegdAdd2.Text) & "', " & vbCrLf _
            & " REGD_CITY='" & MainClass.AllowSingleQuote(txtRegdCity.Text) & "', " & vbCrLf _
            & " REGD_STATE='" & MainClass.AllowSingleQuote(txtRegdState.Text) & "', " & vbCrLf _
            & " REGD_PIN='" & MainClass.AllowSingleQuote(txtRegdPin.Text) & "', " & vbCrLf _
            & " REGD_PHONE='" & MainClass.AllowSingleQuote(txtRegdPhone.Text) & "', " & vbCrLf _
            & " REGD_FAXNO='" & MainClass.AllowSingleQuote(txtRegdFax.Text) & "', " & vbCrLf _
            & " REGD_MAILID='" & MainClass.AllowSingleQuote(txtRegdEmail.Text) & "', " & vbCrLf _
            & " COMPANY_SHORTNAME='" & MainClass.AllowSingleQuote(txtCompanyShortName.Text) & "'," & vbCrLf _
            & " COMPANY_BANK_NAME='" & MainClass.AllowSingleQuote(txtBankName.Text) & "'," & vbCrLf _
            & " COMPANY_BANK_BRANCH='" & MainClass.AllowSingleQuote(txtBranchName.Text) & "'," & vbCrLf _
            & " COMPANY_BANK_ACCOUNT='" & MainClass.AllowSingleQuote(txtAccountNo.Text) & "'," & vbCrLf _
            & " COMPANY_BANK_IFSC='" & MainClass.AllowSingleQuote(txtIFSCCode.Text) & "'" & vbCrLf _
            & " WHERE COMPANY_CODE = " & mCompanyCode & ""




        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsCompany.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans() ''Trans		
        RsCompany.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdcancel.Click
        Me.Hide()
        Me.Close()
        'Me = Nothing		
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCompanyName As String
        Dim mDataDir As String
        Dim aPath As String
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If txtCompanyName.Text = "" Then
            GoTo ExitProc
        Else
            Call Update1()
            cmdSave.Enabled = False
            GoTo ExitProc
        End If
        Exit Sub
ErrorHandler:
        If Err.Number = 75 Then
            MsgBox(Err.Description)
            cmdSave.Enabled = False
        Else
            MsgBox(Err.Description)
        End If
ExitProc:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmCompany_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        '		

        ''Set PvtDBCn = New ADODB.Connection		
        'PvtDBCn.Open StrConn		

        Call SetMainFormCordinate(Me)

        XRIGHT = MainClass.STRMenuRight(PubUserID, 1, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        If XRIGHT <> "" Then MODIFYMode = True
        ADDMode = False
        Call SetTextLengths()
        Call Show1()

        Call AutoCompleteSearch("GEN_STATE_MST", "NAME", "", txtState)
        Call AutoCompleteSearch("GEN_STATE_MST", "NAME", "", txtRegdState)

        FraCompany.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        FraRegdInfo.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        cmdSave.Visible = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        'cmdSavePrint.Visible = False
        cmdSave.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtCompanyName.MaxLength = RsCompany.Fields("COMPANY_NAME").DefinedSize
        txtCompanyShortName.MaxLength = RsCompany.Fields("COMPANY_SHORTNAME").DefinedSize
        txtaddress.MaxLength = RsCompany.Fields("COMPANY_ADDR").DefinedSize
        txtCity.MaxLength = RsCompany.Fields("COMPANY_CITY").DefinedSize
        txtState.MaxLength = RsCompany.Fields("COMPANY_STATE").DefinedSize
        txtPin.MaxLength = RsCompany.Fields("COMPANY_PIN").DefinedSize
        txtPhone.MaxLength = RsCompany.Fields("COMPANY_PHONE").DefinedSize
        txtFax.MaxLength = RsCompany.Fields("COMPANY_FAXNO").DefinedSize
        txtEmail.MaxLength = RsCompany.Fields("COMPANY_MAILID").DefinedSize
        txtRegdAdd1.MaxLength = RsCompany.Fields("REGD_ADDR1").DefinedSize
        txtRegdAdd2.MaxLength = RsCompany.Fields("REGD_ADDR2").DefinedSize
        txtRegdCity.MaxLength = RsCompany.Fields("REGD_CITY").DefinedSize
        txtRegdState.MaxLength = RsCompany.Fields("REGD_STATE").DefinedSize
        txtRegdPhone.MaxLength = RsCompany.Fields("REGD_PHONE").DefinedSize
        txtRegdPin.MaxLength = RsCompany.Fields("REGD_PIN").DefinedSize
        txtRegdFax.MaxLength = RsCompany.Fields("REGD_FAXNO").DefinedSize
        txtRegdEmail.MaxLength = RsCompany.Fields("REGD_MAILID").DefinedSize

        txtBankName.MaxLength = RsCompany.Fields("COMPANY_BANK_NAME").DefinedSize
        txtBranchName.MaxLength = RsCompany.Fields("COMPANY_BANK_BRANCH").DefinedSize
        txtAccountNo.MaxLength = RsCompany.Fields("COMPANY_BANK_ACCOUNT").DefinedSize
        txtIFSCCode.MaxLength = RsCompany.Fields("COMPANY_BANK_IFSC").DefinedSize

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmCompany_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        'Me = Nothing		
    End Sub

    Private Sub txtaddress_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtaddress.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtaddress_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtaddress.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtaddress.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBranchName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBranchName.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBranchName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBranchName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBranchName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAccountNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAccountNo.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAccountNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAccountNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtAccountNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtIFSCCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIFSCCode.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIFSCCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIFSCCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtIFSCCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtCity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCity.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCity.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCompanyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompanyName.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCompanyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCompanyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCompanyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCompanyShortName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompanyShortName.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCompanyShortName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCompanyShortName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCompanyShortName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmail.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtFax_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFax.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFax_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFax.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtFax.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtphone_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPhone.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()
        On Error GoTo ErrPart

        mCompanyCode = RsCompany.Fields("Company_Code").Value
        txtCompanyName.Text = RsCompany.Fields("Company_Name").Value
        txtaddress.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)
        txtCity.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        txtState.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
        txtPin.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)
        txtPhone.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", RsCompany.Fields("COMPANY_PHONE").Value)
        txtFax.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_FAXNO").Value), "", RsCompany.Fields("COMPANY_FAXNO").Value)
        txtEmail.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", RsCompany.Fields("COMPANY_MAILID").Value)

        txtRegdAdd1.Text = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        txtRegdAdd2.Text = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        txtRegdCity.Text = IIf(IsDBNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        txtRegdState.Text = IIf(IsDBNull(RsCompany.Fields("REGD_STATE").Value), "", RsCompany.Fields("REGD_STATE").Value)
        txtRegdPin.Text = IIf(IsDBNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)
        txtRegdPhone.Text = IIf(IsDBNull(RsCompany.Fields("REGD_PHONE").Value), "", RsCompany.Fields("REGD_PHONE").Value)
        txtRegdFax.Text = IIf(IsDBNull(RsCompany.Fields("REGD_FAXNO").Value), "", RsCompany.Fields("REGD_FAXNO").Value)
        txtRegdEmail.Text = IIf(IsDBNull(RsCompany.Fields("REGD_MAILID").Value), "", RsCompany.Fields("REGD_MAILID").Value)

        txtCompanyShortName.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_SHORTNAME").Value), "", RsCompany.Fields("COMPANY_SHORTNAME").Value)

        txtBankName.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_BANK_NAME").Value), "", RsCompany.Fields("COMPANY_BANK_NAME").Value)
        txtBranchName.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_BANK_BRANCH").Value), "", RsCompany.Fields("COMPANY_BANK_BRANCH").Value)
        txtAccountNo.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_BANK_ACCOUNT").Value), "", RsCompany.Fields("COMPANY_BANK_ACCOUNT").Value)
        txtIFSCCode.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_BANK_IFSC").Value), "", RsCompany.Fields("COMPANY_BANK_IFSC").Value)




        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtPhone_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPhone.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPin_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPin.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPin.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRegdAdd1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRegdAdd1.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRegdAdd1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRegdAdd1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtRegdAdd1.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRegdAdd2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRegdAdd2.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRegdAdd2_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRegdAdd2.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtRegdAdd2.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRegdCity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRegdCity.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRegdCity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRegdCity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtRegdCity.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRegdEmail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRegdEmail.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRegdFax_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRegdFax.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRegdFax_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRegdFax.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtRegdFax.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRegdPhone_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRegdPhone.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRegdPhone_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRegdPhone.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtRegdPhone.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRegdPin_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRegdPin.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRegdPin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRegdPin.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRegdState_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRegdState.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRegdState_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRegdState.DoubleClick
        Call SearchState(txtRegdState)
    End Sub

    Private Sub txtRegdState_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRegdState.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtRegdState.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRegdState_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRegdState.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchState(txtRegdState)
    End Sub

    Private Sub txtRegdState_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRegdState.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRegdState.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtRegdState.Text, "NAME", "NAME", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            ErrorMsg("Invalid State Name", , vbInformation)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtstate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtState.TextChanged
        '		
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtState_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtState.DoubleClick
        Call SearchState(txtState)
    End Sub
    Private Sub txtState_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtState.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        ''		
        KeyAscii = MainClass.UpperCase(KeyAscii, txtState.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtState_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtState.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchState(txtState)
    End Sub
    Private Sub SearchState(ByRef mtxtBox As System.Windows.Forms.TextBox)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(mtxtBox.Text, "GEN_STATE_MST", "NAME", , , , SqlStr) = True Then
            mtxtBox.Text = AcName
            mtxtBox.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

   Private Sub txtState_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtState.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      If Trim(txtState.Text) = "" Then GoTo EventExitSub
      If MainClass.ValidateWithMasterTable(txtState.Text, "NAME", "NAME", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
         ErrorMsg("Invalid State Name", , vbInformation)
         Cancel = True
      End If

EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
End Class
