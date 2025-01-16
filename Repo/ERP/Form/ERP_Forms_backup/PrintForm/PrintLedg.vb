Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPrintLedg
	Inherits System.Windows.Forms.Form
	
	Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
		G_PrintLedg = False
		Me.Close()
	End Sub
	
	Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        If OptGroup.Checked = True Then
            If Trim(txtLedgerGroup.Text) = "" Then
                MsgBox("Ledger Group Cann't be blank.", MsgBoxStyle.Information)
                Exit Sub
            End If
        End If

        If optSalesPerson.Checked = True Then
            If Trim(txtSalesPerson.Text) = "" Then
                MsgBox("Sales Person Cann't be blank.", MsgBoxStyle.Information)
                Exit Sub
            End If
        End If

        G_PrintLedg = True
		Me.Hide()
	End Sub
	
	Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
		SearchGroup()
	End Sub
    Private Sub frmPrintLedg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
	
	Private Sub frmPrintLedg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
		Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
		
	End Sub
    Private Sub OptAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAll.CheckedChanged
        If eventSender.Checked Then
            txtLedgerGroup.Enabled = False
            txtSalesPerson.Enabled = False
            cmdsearch.Enabled = False
            cmdsearchSale.Enabled = False
        End If
    End Sub
    Private Sub OptSelected_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelected.CheckedChanged
        If eventSender.Checked Then
            txtLedgerGroup.Enabled = False
            txtSalesPerson.Enabled = False
            cmdsearch.Enabled = False
            cmdsearchSale.Enabled = False
        End If
    End Sub

    'UPGRADE_WARNING: Event OptGroup.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub OptGroup_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptGroup.CheckedChanged
        If eventSender.Checked Then
            txtLedgerGroup.Enabled = True
            txtSalesPerson.Enabled = False

            cmdsearch.Enabled = True
            cmdsearchSale.Enabled = False
        End If
    End Sub

    Private Sub optSalesPerson_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optSalesPerson.CheckedChanged
        If eventSender.Checked Then
            txtSalesPerson.Enabled = True
            txtLedgerGroup.Enabled = False

            cmdsearch.Enabled = False
            cmdsearchSale.Enabled = True
        End If
    End Sub

    Private Sub txtLedgerGroup_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLedgerGroup.DoubleClick
		SearchGroup()
	End Sub
	Private Sub SearchGroup()
		Dim SqlStr As String
		
		SqlStr = ""
		SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		If MainClass.SearchMaster((txtLedgerGroup.Text), "FIN_GROUP_MST", "GROUP_NAME", SqlStr) = True Then
			txtLedgerGroup.Text = AcName
			txtLedgerGroup.Focus()
		End If
	End Sub
    Private Sub txtLedgerGroup_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLedgerGroup.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtLedgerGroup.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
	
	Private Sub txtLedgerGroup_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtLedgerGroup.KeyUp
		Dim KeyCode As Short = eventArgs.KeyCode
		Dim Shift As Short = eventArgs.KeyData \ &H10000
		If KeyCode = System.Windows.Forms.Keys.F1 Then SearchGroup()
	End Sub

    Private Sub txtLedgerGroup_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLedgerGroup.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtLedgerGroup.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtLedgerGroup.Text), "Group_Name", "Group_Code", "FIN_GROUP_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invaild Group Name", MsgBoxStyle.Critical)
            txtLedgerGroup.Focus()
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSalesPerson_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSalesPerson.DoubleClick
        SearchSalesPerson()
    End Sub
    Private Sub SearchSalesPerson()
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'If MainClass.SearchMaster((txtSalesPerson.Text), "FIN_GROUP_MST", "GROUP_NAME", SqlStr) = True Then
        '    txtSalesPerson.Text = AcName
        '    txtSalesPerson.Focus()
        'End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            If MainClass.SearchGridMaster((txtSalesPerson.Text), "FIN_SALESPERSON_MST", "NAME", "CODE", , , "") = True Then
                If AcName <> "" Then
                    txtSalesPerson.Text = AcName
                End If
            End If
        Else
            If MainClass.SearchGridMaster((txtSalesPerson.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
                If AcName <> "" Then
                    txtSalesPerson.Text = AcName
                End If
            End If
        End If

    End Sub
    Private Sub txtSalesPerson_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSalesPerson.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSalesPerson.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSalesPerson_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSalesPerson.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSalesPerson()
    End Sub

    Private Sub txtSalesPerson_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSalesPerson.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSalesPerson.Text) = "" Then GoTo EventExitSub
        'If MainClass.ValidateWithMasterTable((txtSalesPerson.Text), "Group_Name", "Group_Code", "FIN_GROUP_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '    MsgBox("Invaild Group Name", MsgBoxStyle.Critical)
        '    txtSalesPerson.Focus()
        '    Cancel = True
        'End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            If MainClass.ValidateWithMasterTable((txtSalesPerson.Text), "NAME", "CODE", "FIN_SALESPERSON_MST", PubDBCn, MasterNo,  , "") = False Then
                MsgBox("Invaild Group Name", MsgBoxStyle.Critical)
                txtSalesPerson.Focus()
                Cancel = True
            End If
        Else
            If MainClass.ValidateWithMasterTable((txtSalesPerson.Text), "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invaild Group Name", MsgBoxStyle.Critical)
                txtSalesPerson.Focus()
                Cancel = True
            End If
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdsearchSale_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSale.Click
        SearchSalesPerson()
    End Sub

End Class