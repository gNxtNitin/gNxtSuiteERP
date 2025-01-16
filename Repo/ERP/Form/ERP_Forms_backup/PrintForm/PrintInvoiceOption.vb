Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPrintInvoice
	Inherits System.Windows.Forms.Form
	Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
		G_PrintLedg = False
        Me.hide()
	End Sub
	Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
		G_PrintLedg = True
		Me.Hide()
	End Sub
    Private Sub frmPrintInvoice_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
         System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
	Private Sub frmPrintInvoice_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
		Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
		txtF4no.Enabled = False
		txtF4no.Visible = False
	End Sub
    Private Sub OptInvoice_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptInvoice.CheckedChanged
        If eventSender.Checked Then
            FraF4.Enabled = False
        End If
    End Sub
    Private Sub OptInvoiceAnnex_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptInvoiceAnnex.CheckedChanged
        If eventSender.Checked Then
            FraF4.Enabled = False
        End If
    End Sub
    Private Sub optSCOption_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optSCOption.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optSCOption.GetIndex(eventSender)
            txtF4no.Enabled = IIf(Index = 0, False, True)
            txtF4no.Visible = IIf(Index = 0, False, True)
        End If
    End Sub
    Private Sub optSubsidiaryChallan_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optSubsidiaryChallan.CheckedChanged
        If eventSender.Checked Then
            FraF4.Enabled = True
        End If
    End Sub
End Class