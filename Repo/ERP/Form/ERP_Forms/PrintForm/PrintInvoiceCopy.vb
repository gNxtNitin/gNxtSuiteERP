Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPrintInvCopy
    Inherits System.Windows.Forms.Form
    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        G_PrintLedg = False
        Me.Close()
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        G_PrintLedg = True
        Me.Hide()
    End Sub
    Private Sub frmPrintInvCopy_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmPrintInvCopy_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
    End Sub
    Private Sub optA4_CheckedChanged(sender As Object, e As EventArgs) Handles optA4.CheckedChanged
        chkPrePrint.Visible = IIf(optA4.Checked = True, False, True)
        chkPrePrint.CheckState = System.Windows.Forms.CheckState.Unchecked
    End Sub

    Private Sub optA3_CheckedChanged(sender As Object, e As EventArgs) Handles optA3.CheckedChanged
        chkPrePrint.Visible = IIf(optA3.Checked = True, True, False)
        chkPrePrint.CheckState = System.Windows.Forms.CheckState.Checked
    End Sub
End Class