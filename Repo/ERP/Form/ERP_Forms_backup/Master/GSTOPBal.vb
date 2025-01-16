Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGSTOPBal
    Inherits System.Windows.Forms.Form
    Dim RsGSTOPBal As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim Shw As Boolean
    Dim SqlStr As String = ""
    Private Sub Clear1()
        txtCGST.Text = ""
        txtIGST.Text = ""
        txtSGST.Text = ""

    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub frmGSTOPBal_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub frmGSTOPBal_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmGSTOPBal_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        SqlStr = "Select * From FIN_GSTOPAMT_MST " & vbCrLf _
            & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGSTOPBal, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SetTextLengths()
        Clear1()
        Show1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmGSTOPBal_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Call SetMainFormCordinate(Me)
        'Me.Left = 0
        'Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(5295)
        ''Me.Width = VB6.TwipsToPixelsX(8415)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmGSTOPBal_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsGSTOPBal = Nothing
        RsGSTOPBal.Close()
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        If Not RsGSTOPBal.EOF Then
            txtSGST.Text = VB6.Format(IIf(IsDBNull(RsGSTOPBal.Fields("SGST_OP_AMT").Value), 0, RsGSTOPBal.Fields("SGST_OP_AMT").Value), "0.00")
            txtIGST.Text = VB6.Format(IIf(IsDBNull(RsGSTOPBal.Fields("IGST_OP_AMT").Value), 0, RsGSTOPBal.Fields("IGST_OP_AMT").Value), "0.00")
            txtCGST.Text = VB6.Format(IIf(IsDBNull(RsGSTOPBal.Fields("CGST_OP_AMT").Value), 0, RsGSTOPBal.Fields("CGST_OP_AMT").Value), "0.00")
        End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = False Then
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
        On Error GoTo UpdateError


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM FIN_GSTOPAMT_MST WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        SqlStr = "INSERT INTO FIN_GSTOPAMT_MST (" & vbCrLf _
            & " COMPANY_CODE, FYEAR,   " & vbCrLf _
            & " SGST_OP_AMT, IGST_OP_AMT, CGST_OP_AMT " & vbCrLf _
            & " ) VALUES ( " & vbCrLf _
            & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
            & " " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf _
            & " " & Val(txtSGST.Text) & "," & vbCrLf _
            & " " & Val(txtIGST.Text) & "," & vbCrLf _
            & " " & Val(txtCGST.Text) & ")"

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''
        RsGSTOPBal.Requery() '.Refresh
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtSGST.MaxLength = RsGSTOPBal.Fields("SGST_OP_AMT").Precision
        txtIGST.MaxLength = RsGSTOPBal.Fields("IGST_OP_AMT").Precision
        txtCGST.MaxLength = RsGSTOPBal.Fields("CGST_OP_AMT").Precision


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub txtCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCGST.TextChanged
        CmdSave.Enabled = True
    End Sub
    Private Sub txtCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCGST.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCGST.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtCGST.Text = VB6.Format(txtCGST.Text, "0.00")
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIGST.TextChanged
        CmdSave.Enabled = True
    End Sub
    Private Sub txtIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIGST.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIGST.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtIGST.Text = VB6.Format(txtIGST.Text, "0.00")
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSGST.TextChanged
        CmdSave.Enabled = True
    End Sub
    Private Sub txtSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSGST.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSGST.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtSGST.Text = VB6.Format(txtSGST.Text, "0.00")
        eventArgs.Cancel = Cancel
    End Sub
End Class