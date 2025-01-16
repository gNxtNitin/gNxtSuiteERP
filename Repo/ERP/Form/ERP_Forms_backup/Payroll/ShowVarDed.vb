Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmShowVarDed
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As Connection
    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        G_ShowVar = False
        Me.hide()
    End Sub

    Private Sub cmdDeductionSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDeductionSearch.Click
        If MainClass.SearchMaster((txtDeductionName.Text), "PAY_SALARYHEAD_MST", "NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CALC_ON=" & ConCalcVariable & " AND PAYMENT_TYPE='M' AND STATUS='O'") = True Then
            txtDeductionName.Text = AcName
            cmdOk.Focus()
        End If
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        Dim strFilePath As String

        If Trim(txtDeductionName.Text) = "" Then
            MsgBox("Please select Deduction Head Name.", MsgBoxStyle.Information)
            txtDeductionName.Focus()
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtDeductionName.Text), "NAME", "NAME", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Deduction Name.")
            txtDeductionName.Focus()
            Exit Sub
        End If

        strFilePath = My.Application.Info.DirectoryPath
        If Not fOpenFile(strFilePath, "*.xls", "Excel Data", CommonDialogOpen) Then
            GoTo NormalExit
        End If

        lblFilePath.Text = strFilePath

        G_ShowVar = True
        Me.Hide()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub
    Private Sub frmShowVarDed_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmShowVarDed_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        '    FraSelection.Enabled = False
        '    FraSelection.Visible = True
        '    fraBankName.Visible = True
        '    txtEmpCode.Enabled = False
        '    cmdSearch.Enabled = False

        '    txtBankName.Enabled = False
        '    cmdSearchBank.Enabled = False

    End Sub
    Private Sub txtDeductionName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeductionName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeductionName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDeductionName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeductionName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDeductionName.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtDeductionName.Text), "NAME", "NAME", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ADDDEDUCT=" & ConDeduct & "") = False Then
            MsgInformation("Invalid Deduction Name")
            Cancel = True
            '    Else
            '        TxtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
