Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmeMailAddress
    Inherits System.Windows.Forms.Form

    Dim XRIGHT As String
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim FormActive As Boolean

    Dim mADDMode As Boolean
    Dim RseMail As ADODB.Recordset

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdcancel.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        If FieldVerification = False Then Exit Sub
        If Update1 = True Then cmdSave.Enabled = False
    End Sub

    Private Sub frmeMailAddress_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.UOpenRecordSet("Select * From GEN_EMAIL_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RseMail, ADODB.LockTypeEnum.adLockReadOnly)
        Call SetMaxLength()
        Call Show1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmeMailAddress_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Call SetMainFormCordinate(Me)

        ''Set PvtDBCn = New ADODB.Connection		
        ''PvtDBCn.Open StrConn		
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0

        cboEnableSSL.Items.Add("FALSE")
        cboEnableSSL.Items.Add("TRUE")
        cboEnableSSL.SelectedIndex = -1

        ADDMode = False
        MODIFYMode = False
        If XRIGHT <> "" Then MODIFYMode = True
    End Sub
    Private Sub SetMaxLength()

        txtDespeMail.Maxlength = RseMail.Fields("DSP_MAIL_TO").DefinedSize
        txtPureMail.Maxlength = RseMail.Fields("PUR_MAIL_TO").DefinedSize
        txtMainteMail.Maxlength = RseMail.Fields("MNT_MAIL_TO").DefinedSize
        txtHReMail.Maxlength = RseMail.Fields("HRD_MAIL_TO").DefinedSize
        txtStockeMail.Maxlength = RseMail.Fields("STR_MAIL_TO").DefinedSize
        txtPaySlipeMail.Maxlength = RseMail.Fields("PAY_MAIL_TO").DefinedSize
        txtSecurity.Maxlength = RseMail.Fields("SECURITY_MAIL").DefinedSize
        txtIndentAppID.Maxlength = RseMail.Fields("INDENT_MAIL_TO").DefinedSize
        txtITBDId.Maxlength = RseMail.Fields("IT_MAIL_TO").DefinedSize

        TxtSMTP.Maxlength = RseMail.Fields("SMTP_ID").DefinedSize
        TxtPOP3.Maxlength = RseMail.Fields("POP_ID").DefinedSize
        TxtAccount.Maxlength = RseMail.Fields("MAIL_ACCOUNT").DefinedSize
        TxtPassword.Maxlength = RseMail.Fields("Password").DefinedSize
        txtPort.MaxLength = RseMail.Fields("MAIL_PORT").DefinedSize

        txtToolBrkDown.MaxLength = RseMail.Fields("TOOL_MAIL_TO").DefinedSize
        txtFFeMail.MaxLength = RseMail.Fields("FNF_MAIL_TO").DefinedSize
        txtInsuranceID.MaxLength = RseMail.Fields("INSURANCE_MAIL_TO").DefinedSize



    End Sub
    Private Sub Show1()
        On Error GoTo ERR1
        ShowAddress()
        cmdSave.Enabled = False
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Sub ShowAddress()
        On Error GoTo ERR1
        Dim mEnableSSl As String

        If RseMail.EOF = False Then
            txtDespeMail.Text = IIf(IsDbNull(RseMail.Fields("DSP_MAIL_TO").Value), "", RseMail.Fields("DSP_MAIL_TO").Value)
            txtPureMail.Text = IIf(IsDbNull(RseMail.Fields("PUR_MAIL_TO").Value), "", RseMail.Fields("PUR_MAIL_TO").Value)
            txtMainteMail.Text = IIf(IsDbNull(RseMail.Fields("MNT_MAIL_TO").Value), "", RseMail.Fields("MNT_MAIL_TO").Value)
            txtHReMail.Text = IIf(IsDbNull(RseMail.Fields("HRD_MAIL_TO").Value), "", RseMail.Fields("HRD_MAIL_TO").Value)
            txtStockeMail.Text = IIf(IsDbNull(RseMail.Fields("STR_MAIL_TO").Value), "", RseMail.Fields("STR_MAIL_TO").Value)
            txtPaySlipeMail.Text = IIf(IsDbNull(RseMail.Fields("PAY_MAIL_TO").Value), "", RseMail.Fields("PAY_MAIL_TO").Value)
            txtSecurity.Text = IIf(IsDbNull(RseMail.Fields("SECURITY_MAIL").Value), "", RseMail.Fields("SECURITY_MAIL").Value)
            txtIndentAppID.Text = IIf(IsDbNull(RseMail.Fields("INDENT_MAIL_TO").Value), "", RseMail.Fields("INDENT_MAIL_TO").Value)
            txtITBDId.Text = IIf(IsDbNull(RseMail.Fields("IT_MAIL_TO").Value), "", RseMail.Fields("IT_MAIL_TO").Value)

            TxtSMTP.Text = IIf(IsDbNull(RseMail.Fields("SMTP_ID").Value), "", RseMail.Fields("SMTP_ID").Value)
            TxtPOP3.Text = IIf(IsDbNull(RseMail.Fields("POP_ID").Value), "", RseMail.Fields("POP_ID").Value)
            TxtAccount.Text = IIf(IsDbNull(RseMail.Fields("MAIL_ACCOUNT").Value), "", RseMail.Fields("MAIL_ACCOUNT").Value)
            TxtPassword.Text = IIf(IsDBNull(RseMail.Fields("Password").Value), "", RseMail.Fields("Password").Value)

            txtToolBrkDown.Text = IIf(IsDBNull(RseMail.Fields("TOOL_MAIL_TO").Value), "", RseMail.Fields("TOOL_MAIL_TO").Value)
            txtFFeMail.Text = IIf(IsDBNull(RseMail.Fields("FNF_MAIL_TO").Value), "", RseMail.Fields("FNF_MAIL_TO").Value)
            txtInsuranceID.Text = IIf(IsDBNull(RseMail.Fields("INSURANCE_MAIL_TO").Value), "", RseMail.Fields("INSURANCE_MAIL_TO").Value)


            txtPort.Text = IIf(IsDBNull(RseMail.Fields("MAIL_PORT").Value), "", RseMail.Fields("MAIL_PORT").Value)
            mEnableSSl = IIf(IsDBNull(RseMail.Fields("SSL_ENABLE").Value), "0", RseMail.Fields("SSL_ENABLE").Value)

            cboEnableSSL.Text = IIf(mEnableSSl = "0", "FALSE", "TRUE")

        Else
            txtDespeMail.Text = ""
            txtPureMail.Text = ""
            txtMainteMail.Text = ""
            txtHReMail.Text = ""
            txtPaySlipeMail.Text = ""
            txtStockeMail.Text = ""
            TxtSMTP.Text = ""
            TxtPOP3.Text = ""
            TxtAccount.Text = ""
            TxtPassword.Text = ""
            txtSecurity.Text = ""
            txtIndentAppID.Text = ""
            txtITBDId.Text = ""
            txtPort.Text = ""


            txtToolBrkDown.Text = ""
            txtFFeMail.Text = ""
            txtInsuranceID.Text = ""

            cboEnableSSL.SelectedIndex = -1
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim xCode As Integer
        Dim mEnableSSL As String


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        xCode = RsCompany.Fields("Company_Code").Value

        If MainClass.ValidateWithMasterTable(xCode, "Company_Code", "Company_Code", "GEN_EMAIL_MST", PubDBCn, MasterNo) = True Then
            mADDMode = False
        Else
            mADDMode = True
        End If

        mEnableSSL = IIf(cboEnableSSL.Text = "TRUE", "1", "0")

        If mADDMode = True Then
            SqlStr = "INSERT INTO GEN_EMAIL_MST ( " & vbCrLf _
                    & " COMPANY_CODE, DSP_MAIL_TO, PUR_MAIL_TO, " & vbCrLf _
                    & " MNT_MAIL_TO, HRD_MAIL_TO, STR_MAIL_TO, PAY_MAIL_TO," & vbCrLf _
                    & " SMTP_ID, POP_ID, " & vbCrLf _
                    & " MAIL_ACCOUNT, Password,SECURITY_MAIL,INDENT_MAIL_TO, SSL_ENABLE,MAIL_PORT,IT_MAIL_TO, TOOL_MAIL_TO, FNF_MAIL_TO, INSURANCE_MAIL_TO"

            SqlStr = SqlStr & vbCrLf _
                    & " ) VALUES ( "

            SqlStr = SqlStr & vbCrLf _
                    & " " & xCode & ", '" & MainClass.AllowSingleQuote(txtDespeMail.Text) & "', '" & MainClass.AllowSingleQuote(txtPureMail.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtMainteMail.Text) & "','" & MainClass.AllowSingleQuote(txtHReMail.Text) & "','" & MainClass.AllowSingleQuote(txtStockeMail.Text) & "','" & MainClass.AllowSingleQuote(txtPaySlipeMail.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(TxtSMTP.Text) & "','" & MainClass.AllowSingleQuote(TxtPOP3.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(TxtAccount.Text) & "','" & MainClass.AllowSingleQuote(TxtPassword.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtSecurity.Text) & "', '" & MainClass.AllowSingleQuote(txtIndentAppID.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mEnableSSL) & "', '" & MainClass.AllowSingleQuote(txtPort.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtITBDId.Text) & "','" & MainClass.AllowSingleQuote(txtToolBrkDown.Text) & "','" & MainClass.AllowSingleQuote(txtFFeMail.Text) & "','" & MainClass.AllowSingleQuote(txtInsuranceID.Text) & "')"
        Else



            SqlStr = "UPDATE  GEN_EMAIL_MST SET " & vbCrLf _
                    & " TOOL_MAIL_TO= '" & MainClass.AllowSingleQuote(txtToolBrkDown.Text) & "'," & vbCrLf _
                    & " FNF_MAIL_TO= '" & MainClass.AllowSingleQuote(txtFFeMail.Text) & "'," & vbCrLf _
                    & " INSURANCE_MAIL_TO= '" & MainClass.AllowSingleQuote(txtInsuranceID.Text) & "'," & vbCrLf _
                    & " DSP_MAIL_TO= '" & MainClass.AllowSingleQuote(txtDespeMail.Text) & "'," & vbCrLf _
                    & " PUR_MAIL_TO= '" & MainClass.AllowSingleQuote(txtPureMail.Text) & "'," & vbCrLf _
                    & " MNT_MAIL_TO= '" & MainClass.AllowSingleQuote(txtMainteMail.Text) & "'," & vbCrLf _
                    & " HRD_MAIL_TO= '" & MainClass.AllowSingleQuote(txtHReMail.Text) & "'," & vbCrLf _
                    & " PAY_MAIL_TO= '" & MainClass.AllowSingleQuote(txtPaySlipeMail.Text) & "'," & vbCrLf _
                    & " STR_MAIL_TO= '" & MainClass.AllowSingleQuote(txtStockeMail.Text) & "'," & vbCrLf _
                    & " SECURITY_MAIL= '" & MainClass.AllowSingleQuote(txtSecurity.Text) & "'," & vbCrLf _
                    & " SMTP_ID= '" & MainClass.AllowSingleQuote(TxtSMTP.Text) & "'," & vbCrLf _
                    & " POP_ID= '" & MainClass.AllowSingleQuote(TxtPOP3.Text) & "'," & vbCrLf _
                    & " MAIL_ACCOUNT= '" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'," & vbCrLf _
                    & " INDENT_MAIL_TO= '" & MainClass.AllowSingleQuote(txtIndentAppID.Text) & "'," & vbCrLf _
                    & " IT_MAIL_TO= '" & MainClass.AllowSingleQuote(txtITBDId.Text) & "'," & vbCrLf _
                    & " SSL_ENABLE= '" & MainClass.AllowSingleQuote(mEnableSSL) & "'," & vbCrLf _
                    & " MAIL_PORT= '" & MainClass.AllowSingleQuote(txtPort.Text) & "'," & vbCrLf _
                    & " Password= '" & MainClass.AllowSingleQuote(TxtPassword.Text) & "'"

            SqlStr = SqlStr & vbCrLf & " WHERE Company_Code=" & xCode & ""

        End If

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        Update1 = True
        RseMail.Requery() ''.Refresh		

        Exit Function
err_Renamed:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Update1 = False
        PubDBCn.RollbackTrans() ''		
        RseMail.Requery() ''.Refresh		


    End Function
    Private Sub frmeMailAddress_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        FormActive = False
        RseMail = Nothing
    End Sub
    Private Function FieldVerification() As Boolean
        On Error GoTo ERR1
        FieldVerification = True

        '
        '
        '    If Trim(txtModvatRAcct.Text) = "" Then
        '        MsgInformation "Modvat (Raw) Account cann't be blank."
        '        txtModvatRAcct.SetFocus
        '        FieldVerification = False
        '        Exit Function
        '    End If


        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDespeMail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDespeMail.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHReMail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHReMail.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIndentAppID_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIndentAppID.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtITBDId_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtITBDId.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMainteMail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMainteMail.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtPassword_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtPassword.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPaySlipeMail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaySlipeMail.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtPOP3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtPOP3.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPureMail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPureMail.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSecurity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSecurity.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtSMTP_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSMTP.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtStockeMail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStockeMail.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPort_TextChanged(sender As Object, e As EventArgs) Handles txtPort.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboEnableSSL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEnableSSL.SelectedIndexChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtInsuranceID_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInsuranceID.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtFFeMail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFFeMail.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolBrkDown_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolBrkDown.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
End Class
