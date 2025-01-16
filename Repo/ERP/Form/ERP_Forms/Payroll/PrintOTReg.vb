Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPrintOTReg
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As Connection
    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        G_PrintLedg = False
        Me.hide()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click

        If optBankTxt.Checked = True Or optBank.Checked = True Then
            If optAllBank(1).Checked = True Then
                If Trim(txtBankName.Text) = "" Then
                    MsgBox("Please Select Bank Name.", MsgBoxStyle.Information)
                    txtBankName.Focus()
                    Exit Sub
                Else
                    If MainClass.ValidateWithMasterTable((txtBankName.Text), "BANK_NAME", "BANK_NAME", "PAY_BANK_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgBox("Invalid Bank Name.", MsgBoxStyle.Information)
                        txtBankName.Focus()
                        Exit Sub
                    End If
                End If
            End If
        End If

        G_PrintLedg = True
        Me.Hide()
    End Sub
    Private Sub frmPrintOTReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmPrintOTReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        fraBankName.Visible = True
    End Sub
    Private Sub optAllBank_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optAllBank.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optAllBank.GetIndex(eventSender)
            txtBankName.Enabled = IIf(Index = 0, False, True)
            cmdSearchBank.Enabled = IIf(Index = 0, False, True)
        End If
    End Sub

    Private Sub optBank_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBank.CheckedChanged
        If eventSender.Checked Then
            fraBankName.Enabled = True
        End If
    End Sub

    Private Sub optBankTxt_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBankTxt.CheckedChanged
        If eventSender.Checked Then
            fraBankName.Enabled = True
        End If
    End Sub

    Private Sub optCash_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCash.CheckedChanged
        If eventSender.Checked Then
            fraBankName.Enabled = False
        End If
    End Sub

    Private Sub optCheckList_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCheckList.CheckedChanged
        If eventSender.Checked Then
            fraBankName.Enabled = False
        End If
    End Sub

    Private Sub txtBankName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBankName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtBankName.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtBankName.Text), "BANK_NAME", "BANK_NAME", "PAY_BANK_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtBankName.Text = AcName
            cmdOk.Focus()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub cmdSearchBank_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchBank.Click
        If MainClass.SearchGridMaster((txtBankName.Text), "PAY_BANK_MST", "BANK_NAME", "", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtBankName.Text = AcName
            cmdOk.Focus()
        End If
    End Sub

    Private Sub txtBankName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.DoubleClick
        cmdSearchBank_Click(cmdSearchBank, New System.EventArgs())
    End Sub
End Class
