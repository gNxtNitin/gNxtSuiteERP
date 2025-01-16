Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAddCompany
   Inherits System.Windows.Forms.Form
   'Dim PvtDBCN As ADODB.Connection			
   Dim RsNewCompany As ADODB.Recordset
   Dim XRIGHT As String
   Dim xCompanyCode As Integer
   Dim FormActive As Boolean
    Dim SqlStr As String = ""
    Private Sub Clear1()
        txtCompanyName.Text = ""
        txtAdd.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtPin.Text = ""
        txtPhone.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
        txtCommissionerate.Text = ""
        TxtTDSNo.Text = ""
        txtTINNo.Text = ""
        txtPFEst.Text = ""
        txtESIEst.Text = ""
        'txtCSTNo.Text = ""
        'txtLSTNo.Text = ""
        txtPAN.Text = ""
        txtJurisdiction.Text = ""
        'txtExciseDiv.Text = ""
        'txtExciseRange.Text = ""
        txtRegnNo.Text = ""
        txtGSTRegnNo.Text = ""

        txtECCNo.Text = ""
        'txtServRegnNo.Text = ""
        txtIECNo.Text = ""
        txtTANNo.Text = ""
        txtCIN.Text = ""
        txtPrintCompanyName.Text = ""
        chkEOU.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkExempted.CheckState = System.Windows.Forms.CheckState.Unchecked
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide() ''me.hide 		
        Me.Close()
    End Sub
    Private Sub frmAddCompany_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        '				
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.UOpenRecordSet("Select * From GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsNewCompany, ADODB.LockTypeEnum.adLockReadOnly)
        Call SetTextLengths()
        Call Clear1()
        Call Show1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmAddCompany_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmAddCompany_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        '				
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call SetMainFormCordinate(Me)
        'Me.Left = 0
        'Me.Top = 0
        'Me.Height = 7680
        'Me.Width = 8610
        'Set PvtDBCN = New ADODB.Connection				
        'PvtDBCN.Open StrConn				

        XRIGHT = MainClass.STRMenuRight(PubUserID, 1, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmAddCompany_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsNewCompany = Nothing
        Me.Hide()
        Me.Close()
        'PvtDBCN.Cancel				
        'PvtDBCN.Close				
        'Set PvtDBCN = Nothing				
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        If RsNewCompany.EOF = False Then
            xCompanyCode = RsNewCompany.Fields("COMPANY_CODE").Value
            txtCompanyName.Text = IIf(IsDBNull(RsNewCompany.Fields("Company_Name").Value), "", RsNewCompany.Fields("Company_Name").Value)
            txtAdd.Text = IIf(IsDBNull(RsNewCompany.Fields("COMPANY_ADDR").Value), "", RsNewCompany.Fields("COMPANY_ADDR").Value)
            txtCity.Text = IIf(IsDBNull(RsNewCompany.Fields("COMPANY_CITY").Value), "", RsNewCompany.Fields("COMPANY_CITY").Value)
            txtState.Text = IIf(IsDBNull(RsNewCompany.Fields("COMPANY_STATE").Value), "", RsNewCompany.Fields("COMPANY_STATE").Value)
            txtPin.Text = IIf(IsDBNull(RsNewCompany.Fields("COMPANY_PIN").Value), "", RsNewCompany.Fields("COMPANY_PIN").Value)
            txtPhone.Text = IIf(IsDBNull(RsNewCompany.Fields("COMPANY_PHONE").Value), "", RsNewCompany.Fields("COMPANY_PHONE").Value)
            txtFax.Text = IIf(IsDBNull(RsNewCompany.Fields("COMPANY_FAXNO").Value), "", RsNewCompany.Fields("COMPANY_FAXNO").Value)

            txtEmail.Text = IIf(IsDBNull(RsNewCompany.Fields("COMPANY_MAILID").Value), "", RsNewCompany.Fields("COMPANY_MAILID").Value)
            txtCommissionerate.Text = IIf(IsDBNull(RsNewCompany.Fields("COMMISIONER_RATE").Value), "", RsNewCompany.Fields("COMMISIONER_RATE").Value)
            TxtTDSNo.Text = IIf(IsDBNull(RsNewCompany.Fields("TDSNO").Value), "", RsNewCompany.Fields("TDSNO").Value)
            txtPFEst.Text = IIf(IsDBNull(RsNewCompany.Fields("PFEST").Value), "", RsNewCompany.Fields("PFEST").Value)
            txtESIEst.Text = IIf(IsDBNull(RsNewCompany.Fields("ESIEST").Value), "", RsNewCompany.Fields("ESIEST").Value)
            'txtCSTNo.Text = IIf(IsDbNull(RsNewCompany.Fields("CST_NO").Value), "", RsNewCompany.Fields("CST_NO").Value)
            'txtLSTNo.Text = IIf(IsDbNull(RsNewCompany.Fields("LST_NO").Value), "", RsNewCompany.Fields("LST_NO").Value)
            txtPAN.Text = IIf(IsDBNull(RsNewCompany.Fields("PAN_NO").Value), "", RsNewCompany.Fields("PAN_NO").Value)
            txtJurisdiction.Text = IIf(IsDBNull(RsNewCompany.Fields("JURISDICTION").Value), "", RsNewCompany.Fields("JURISDICTION").Value)
            'txtExciseDiv.Text = IIf(IsDbNull(RsNewCompany.Fields("EXCISE_DIV").Value), "", RsNewCompany.Fields("EXCISE_DIV").Value)
            'txtExciseRange.Text = IIf(IsDbNull(RsNewCompany.Fields("EXCISE_RANGE").Value), "", RsNewCompany.Fields("EXCISE_RANGE").Value)
            txtRegnNo.Text = IIf(IsDBNull(RsNewCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsNewCompany.Fields("CENT_EXC_RGN_NO").Value)

            txtGSTRegnNo.Text = IIf(IsDBNull(RsNewCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsNewCompany.Fields("COMPANY_GST_RGN_NO").Value)

            txtPrintCompanyName.Text = IIf(IsDBNull(RsNewCompany.Fields("PRINT_COMPANY_NAME").Value), "", RsNewCompany.Fields("PRINT_COMPANY_NAME").Value)
            'txtServRegnNo.Text = IIf(IsDbNull(RsNewCompany.Fields("SERV_REGN_NO").Value), "", RsNewCompany.Fields("SERV_REGN_NO").Value)
            txtECCNo.Text = IIf(IsDBNull(RsNewCompany.Fields("ECC_NO").Value), "", RsNewCompany.Fields("ECC_NO").Value)
            txtIECNo.Text = IIf(IsDBNull(RsNewCompany.Fields("IEC_NO").Value), "", RsNewCompany.Fields("IEC_NO").Value)
            chkEOU.CheckState = IIf(RsNewCompany.Fields("ISEOU").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkExempted.CheckState = IIf(RsNewCompany.Fields("EXEMPTED_UNIT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            txtCIN.Text = IIf(IsDBNull(RsNewCompany.Fields("CIN_NO").Value), "", RsNewCompany.Fields("CIN_NO").Value)

            If MainClass.ValidateWithMasterTable(xCompanyCode, "COMPANY_CODE", "TINNO", "FIN_PRINT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
                txtTINNo.Text = MasterNo
            Else
                txtTINNo.Text = ""
            End If

            If MainClass.ValidateWithMasterTable(xCompanyCode, "COMPANY_CODE", "TAN_NO", "FIN_PRINT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
                txtTANNo.Text = MasterNo
            Else
                txtTANNo.Text = ""
            End If

        End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = False Then
            MsgInformation("Record not saved")
        Else
            CmdSave.Enabled = False
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean
        '				
        On Error GoTo UpdateError
        Dim mCompanyCode As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = ""

        SqlStr = "UPDATE GEN_COMPANY_MST " & vbCrLf _
            & " SET COMPANY_NAME='" & MainClass.AllowSingleQuote(txtCompanyName.Text) & "', " & vbCrLf _
            & " COMPANY_ADDR='" & MainClass.AllowSingleQuote(txtAdd.Text) & "', " & vbCrLf _
            & " COMPANY_CITY='" & MainClass.AllowSingleQuote(txtCity.Text) & "', " & vbCrLf _
            & " COMPANY_STATE='" & MainClass.AllowSingleQuote(txtState.Text) & "', " & vbCrLf _
            & " COMPANY_PIN='" & MainClass.AllowSingleQuote(txtPin.Text) & "', " & vbCrLf _
            & " COMPANY_PHONE='" & MainClass.AllowSingleQuote(txtPhone.Text) & "', " & vbCrLf _
            & " COMPANY_FAXNO='" & MainClass.AllowSingleQuote(txtFax.Text) & "', " & vbCrLf _
            & " COMPANY_MAILID='" & MainClass.AllowSingleQuote(txtEmail.Text) & "', " & vbCrLf _
            & " COMMISIONER_RATE='" & MainClass.AllowSingleQuote(txtCommissionerate.Text) & "', " & vbCrLf _
            & " TDSNO='" & MainClass.AllowSingleQuote(TxtTDSNo.Text) & "', " & vbCrLf _
            & " PFEST='" & MainClass.AllowSingleQuote(txtPFEst.Text) & "', " & vbCrLf _
            & " ESIEST='" & MainClass.AllowSingleQuote(txtESIEst.Text) & "', " & vbCrLf _
            & " PAN_NO='" & MainClass.AllowSingleQuote(txtPAN.Text) & "', " & vbCrLf _
            & " JURISDICTION='" & MainClass.AllowSingleQuote(txtJurisdiction.Text) & "', "

        SqlStr = SqlStr & vbCrLf & " CENT_EXC_RGN_NO='" & MainClass.AllowSingleQuote(txtRegnNo.Text) & "', " & vbCrLf _
            & " COMPANY_GST_RGN_NO='" & MainClass.AllowSingleQuote(txtGSTRegnNo.Text) & "', " & vbCrLf _
            & " ECC_NO='" & MainClass.AllowSingleQuote(txtECCNo.Text) & "', " & vbCrLf _
            & " PRINT_COMPANY_NAME='" & MainClass.AllowSingleQuote(txtPrintCompanyName.Text) & "', " & vbCrLf _
            & " IEC_NO='" & MainClass.AllowSingleQuote(txtIECNo.Text) & "', " & vbCrLf _
            & " CIN_NO='" & MainClass.AllowSingleQuote(txtCIN.Text) & "', " & vbCrLf _
            & " ISEOU='" & IIf(chkEOU.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
            & " EXEMPTED_UNIT='" & IIf(chkExempted.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "' "

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE= " & xCompanyCode & ""


        PubDBCn.Execute(SqlStr)

        SqlStr = "UPDATE FIN_PRINT_MST SET " & vbCrLf & " TINNO='" & MainClass.AllowSingleQuote(txtTINNo.Text) & "', TAN_NO='" & MainClass.AllowSingleQuote(txtTANNo.Text) & "'" & vbCrLf & " WHERE COMPANY_CODE= " & xCompanyCode & ""

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        Update1 = True
        RsCompany.Requery()
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        'PvtDBCN.Errors.Clear				
        RsNewCompany.Requery()
        RsCompany.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume				
    End Function
    Private Sub SetTextLengths()
        '				
        On Error GoTo ERR1

        txtCompanyName.MaxLength = RsNewCompany.Fields("COMPANY_NAME").DefinedSize
        txtAdd.MaxLength = RsNewCompany.Fields("COMPANY_ADDR").DefinedSize
        txtCity.MaxLength = RsNewCompany.Fields("COMPANY_CITY").DefinedSize
        txtState.MaxLength = RsNewCompany.Fields("COMPANY_STATE").DefinedSize
        txtPin.MaxLength = RsNewCompany.Fields("COMPANY_PIN").DefinedSize
        txtPhone.MaxLength = RsNewCompany.Fields("COMPANY_PHONE").DefinedSize
        txtFax.MaxLength = RsNewCompany.Fields("COMPANY_FAXNO").DefinedSize
        txtEmail.MaxLength = RsNewCompany.Fields("COMPANY_MAILID").DefinedSize
        txtCommissionerate.MaxLength = RsNewCompany.Fields("COMMISIONER_RATE").DefinedSize

        TxtTDSNo.MaxLength = RsNewCompany.Fields("TDSNO").DefinedSize
        txtPFEst.MaxLength = RsNewCompany.Fields("PFEST").DefinedSize
        txtESIEst.MaxLength = RsNewCompany.Fields("ESIEST").DefinedSize
        'txtCSTNo.Maxlength = RsNewCompany.Fields("CST_NO").DefinedSize
        'txtLSTNo.Maxlength = RsNewCompany.Fields("LST_NO").DefinedSize
        txtPAN.MaxLength = RsNewCompany.Fields("PAN_NO").DefinedSize
        txtJurisdiction.MaxLength = RsNewCompany.Fields("JURISDICTION").DefinedSize
        'txtExciseDiv.Maxlength = RsNewCompany.Fields("EXCISE_DIV").DefinedSize
        'txtExciseRange.Maxlength = RsNewCompany.Fields("EXCISE_RANGE").DefinedSize
        txtRegnNo.MaxLength = RsNewCompany.Fields("CENT_EXC_RGN_NO").DefinedSize
        txtGSTRegnNo.MaxLength = RsNewCompany.Fields("COMPANY_GST_RGN_NO").DefinedSize
        'txtServRegnNo.Maxlength = RsNewCompany.Fields("SERV_REGN_NO").DefinedSize

        txtECCNo.MaxLength = RsNewCompany.Fields("ECC_NO").DefinedSize
        txtIECNo.MaxLength = RsNewCompany.Fields("IEC_NO").DefinedSize
        txtCIN.MaxLength = RsNewCompany.Fields("CIN_NO").DefinedSize
        txtPrintCompanyName.MaxLength = RsNewCompany.Fields("PRINT_COMPANY_NAME").DefinedSize

        txtTINNo.MaxLength = MainClass.SetMaxLength("TINNO", "FIN_PRINT_MST", PubDBCn)
        txtTANNo.MaxLength = MainClass.SetMaxLength("TAN_NO", "FIN_PRINT_MST", PubDBCn)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtAdd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdd.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtAdd.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub



    Private Sub txtCIN_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCIN.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCIN.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

    Private Sub txtCommissionerate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCommissionerate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCommissionerate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If Trim(txtCompanyName.Text) = "" Then
            MsgInformation(" Name is empty. Cannot Save")
            txtCompanyName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtState.Text) = "" Then
            MsgInformation("Please Enter State.")
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtState.Text, "NAME", "NAME", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid State Name")
            If txtState.Enabled = True Then txtState.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtGSTRegnNo.Text) <> "" Then
            If CheckGSTValidation(Trim(txtGSTRegnNo.Text), Trim(txtState.Text)) = False Then
                MsgBox("Invalid GST Regn No., so that cann't be save.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            Else
                txtGSTRegnNo.Text = Trim(txtGSTRegnNo.Text)
            End If
        End If

        Exit Function
err_Renamed:
        FieldsVarification = False
        MsgBox(Err.Description)
    End Function
    Private Sub frmAddCompany_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode				
    End Sub

    Private Sub txtECCNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtECCNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtECCNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmail_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmail.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtESIEst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESIEst.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtESIEst.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

    Private Sub txtGSTRegnNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGSTRegnNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtGSTRegnNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtIECNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIECNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        ''				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtIECNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtJurisdiction_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtJurisdiction.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtJurisdiction.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPAN_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPAN.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPAN.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPFEst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPFEst.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPFEst.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
    Private Sub txtPin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPin.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPin.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtRegnNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRegnNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtRegnNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtState_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtState.DoubleClick
        SearchState()
    End Sub

    Private Sub txtState_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtState.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtState.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtState_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtState.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchState()
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

    Private Sub txtTANNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTANNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtTANNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtTDSNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtTDSNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtTDSNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SearchState()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtState.Text, "GEN_STATE_MST", "NAME", "STATE_CODE", , , SqlStr) = True Then
            txtState.Text = AcName
            txtState.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtTINNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTINNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '				
        KeyAscii = MainClass.UpperCase(KeyAscii, txtTINNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub
End Class
