Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPrintSalReg
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As Connection
    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        G_PrintLedg = False
        Me.hide()
    End Sub

    Private Sub cmdDeductionSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDeductionSearch.Click
        If MainClass.SearchMaster((txtDeductionName.Text), "PAY_SALARYHEAD_MST", "NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ADDDEDUCT IN (" & ConDeduct & "," & ConEarning & ") AND STATUS='O'") = True Then
            txtDeductionName.Text = AcName
            cmdOk.Focus()
        End If
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click

        If optPaySlip.Checked = True Or OptIncentive.Checked = True Then
            If optAll(1).Checked = True Then
                If Trim(txtEmpCode.Text) = "" Then
                    MsgBox("Emp Name Cann't be blank. Please select the Emp Name", MsgBoxStyle.Information)
                    If txtEmpCode.Enabled = True Then txtEmpCode.Focus()
                    Exit Sub
                End If

                If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("Please select the valid Emp name.", MsgBoxStyle.Information)
                    txtEmpCode.Focus()
                    Exit Sub
                End If
            End If
        ElseIf OptDeductionList.Checked = True Then
            If Trim(txtDeductionName.Text) = "" Then
                MsgBox("Please select Deduction Head Name.", MsgBoxStyle.Information)
                txtDeductionName.Focus()
                Exit Sub
            End If
        End If

        If optBankTxt.Checked = True Or OptSalSheet.Checked = True Then
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

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click

        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
            cmdOk.Focus()
        End If
    End Sub

    Private Sub cmdSearchBank_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchBank.Click
        If MainClass.SearchGridMaster((txtBankName.Text), "PAY_BANK_MST", "BANK_NAME", "", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtBankName.Text = AcName
            cmdOk.Focus()
        End If
    End Sub

    Private Sub frmPrintSalReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmPrintSalReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        FraSelection.Enabled = False
        FraSelection.Visible = True
        fraBankName.Visible = True
        txtEmpCode.Enabled = False
        cmdsearch.Enabled = False

        txtBankName.Enabled = False
        cmdSearchBank.Enabled = False

    End Sub

    Private Sub optAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optAll.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optAll.GetIndex(eventSender)
            If optAll(0).Checked = True Then
                txtEmpCode.Enabled = False
                cmdsearch.Enabled = False
            ElseIf optAll(1).Checked = True Then
                txtEmpCode.Enabled = True
                cmdsearch.Enabled = True
            End If
        End If
    End Sub
    Private Sub optAllBank_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optAllBank.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optAllBank.GetIndex(eventSender)
            txtBankName.Enabled = IIf(Index = 0, False, True)
            cmdSearchBank.Enabled = IIf(Index = 0, False, True)

        End If
    End Sub

    Private Sub optBankTxt_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBankTxt.CheckedChanged
        If eventSender.Checked Then
            FraSelection.Enabled = False
            cmdDeductionSearch.Enabled = False
            txtDeductionName.Enabled = False
            fraBankName.Enabled = True
        End If
    End Sub

    Private Sub optCashSheet_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCashSheet.CheckedChanged
        If eventSender.Checked Then
            FraSelection.Enabled = False
            cmdDeductionSearch.Enabled = False
            txtDeductionName.Enabled = False
            fraBankName.Enabled = False
        End If
    End Sub

    Private Sub OptDeductionList_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptDeductionList.CheckedChanged
        If eventSender.Checked Then
            FraSelection.Enabled = False
            cmdDeductionSearch.Enabled = True
            txtDeductionName.Enabled = True
        End If
    End Sub

    Private Sub OptIncentive_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptIncentive.CheckedChanged
        If eventSender.Checked Then
            FraSelection.Enabled = True
            cmdDeductionSearch.Enabled = False
            txtDeductionName.Enabled = False
            fraBankName.Enabled = False
        End If
    End Sub

    Private Sub optPaySlip_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPaySlip.CheckedChanged
        If eventSender.Checked Then
            FraSelection.Enabled = True
            cmdDeductionSearch.Enabled = False
            txtDeductionName.Enabled = False
            fraBankName.Enabled = False
        End If
    End Sub
    Private Sub OptSalReg_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSalReg.CheckedChanged
        If eventSender.Checked Then
            FraSelection.Enabled = False
            cmdDeductionSearch.Enabled = False
            txtDeductionName.Enabled = False
            fraBankName.Enabled = False
        End If
    End Sub
    Private Sub OptSalSheet_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSalSheet.CheckedChanged
        If eventSender.Checked Then
            FraSelection.Enabled = False
            cmdDeductionSearch.Enabled = False
            txtDeductionName.Enabled = False
            fraBankName.Enabled = True
        End If
    End Sub

    Private Sub txtBankName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.DoubleClick
        cmdSearchBank_Click(cmdSearchBank, New System.EventArgs())
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
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
