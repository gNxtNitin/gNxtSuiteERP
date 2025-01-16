Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmChangePwd

    Inherits System.Windows.Forms.Form
    '''''''Private PvtDBCn As ADODB.Connection

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        Dim SqlStr As String
        On Error GoTo SaveErr
        Dim pNewPassword As String


        pNewPassword = ToHexDump(CryptRC4(UCase(txtNewPwd.Text), "password"))

        PubColorTheme = Mid(cboColorTheme.Text, 1, 1)

        ''            & " PASSWORD='" & txtNewPwd.Text & "', " & vbCrLf 

        SqlStr = " UPDATE ATH_PASSWORD_MST SET " & vbCrLf _
            & " NEWPASSWORD= '" & Trim(pNewPassword) & "', COLOR_THEME=" & PubColorTheme & "" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND USER_ID='" & PubUserID & "'"
        PubDBCn.Execute(SqlStr)

        MsgInformation("Password for User " & PubUserID & " has been changed")
        Me.Hide()
        Me.Close()
        Me.Dispose()
        Exit Sub
SaveErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        MsgInformation("Password for User " & PubUserID & " Not changed")
    End Sub

    Private Sub frmChangePwd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub frmChangePwd_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        SetMainFormCordinate(Me)
        MainClass.SetControlsColor(Me)

        cboColorTheme.Items.Clear()
        cboColorTheme.Items.Add("1 : Blue")
        cboColorTheme.Items.Add("2 : Brown")
        cboColorTheme.Items.Add("3 : Gray")
        cboColorTheme.Items.Add("4 : Green")
        cboColorTheme.Items.Add("5 : Dark")
        cboColorTheme.SelectedIndex = PubColorTheme - 1


        lblUserID.Text = PubUserID


        SetTextLength()
        txtConfirmPwd.Enabled = False
        txtNewPwd.Enabled = False
        CmdSave.Enabled = False
    End Sub
    Private Sub txtConfirmPwd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtConfirmPwd.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtConfirmPwd.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtConfirmPwd_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtConfirmPwd.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtConfirmPwd.Text) = "" Then GoTo EventExitSub

        If txtNewPwd.Text <> txtConfirmPwd.Text Then
            MsgInformation("Password Does Not Match ")
            Cancel = True
        Else
            CmdSave.Enabled = True
            CmdSave.Focus()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtNewPwd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNewPwd.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtNewPwd.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNewPwd_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNewPwd.Leave
        If txtNewPwd.Text <> "" Then
            txtConfirmPwd.Enabled = True
            txtConfirmPwd.Focus()
        End If
    End Sub
    Private Sub txtOldPwd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOldPwd.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtOldPwd.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOldPwd_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOldPwd.Leave
        If txtNewPwd.Enabled Then txtNewPwd.Focus()
    End Sub

    Private Sub txtOldPwd_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOldPwd.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mUserNewPassWord As String
        Dim mUserDecryptPassWord As String

        If Trim(txtOldPwd.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(PubUserID, "USER_ID", "NEWPASSWORD", "ATH_PASSWORD_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mUserNewPassWord = Trim(UCase(MasterNo))
        Else
            mUserNewPassWord = ""
        End If

        If mUserNewPassWord = "" Then
            MainClass.ValidateWithMasterTable(PubUserID, "USER_ID", "PASSWORD", "ATH_PASSWORD_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            If UCase(txtOldPwd.Text) <> UCase(MasterNo) Then
                MsgInformation("Old Password does Not Match")
                Cancel = True
                txtOldPwd.Focus()
            Else
                txtNewPwd.Enabled = True
                txtNewPwd.Focus()
            End If
        Else
            mUserDecryptPassWord = CryptRC4(FromHexDump(mUserNewPassWord), "password")
            If UCase(txtOldPwd.Text) <> UCase(mUserDecryptPassWord) Then
                MsgInformation("Old Password does Not Match")
                Cancel = True
                txtOldPwd.Focus()
            Else
                txtNewPwd.Enabled = True
                txtNewPwd.Focus()
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SetTextLength()
        On Error GoTo ERR1
        Dim RsUser As ADODB.Recordset

        MainClass.UOpenRecordSet("Select * From ATH_PASSWORD_MST Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsUser, ADODB.LockTypeEnum.adLockReadOnly)

        txtConfirmPwd.MaxLength = RsUser.Fields("PASSWORD").DefinedSize ''
        txtOldPwd.MaxLength = RsUser.Fields("PASSWORD").DefinedSize ''
        txtNewPwd.MaxLength = RsUser.Fields("PASSWORD").DefinedSize ''
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
End Class