Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmMrrCorrection
    Inherits System.Windows.Forms.Form
    Dim RsMrrCorr As ADODB.Recordset
    '''Private PvtDBCn As ADODB.Connection		
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim SqlStr As String
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frmMrrCorrection_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        'MainClass.DoFunctionKey Me, KeyCode		
    End Sub
    Private Sub frmMrrCorrection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMrrNo.TextChanged
        CmdSave.Enabled = True
    End Sub
    Private Sub txtMRRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMrrNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtMrrNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMrrNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtMrrNo.Text) = "" Then GoTo EventExitSub
        If Len(txtMrrNo.Text) < 6 Then
            txtMrrNo.Text = Val(txtMrrNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        SqlStr = "SELECT * FROM INV_GATE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR='" & MainClass.AllowSingleQuote(UCase(Trim(txtMrrNo.Text))) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMrrCorr, ADODB.LockTypeEnum.adLockReadOnly)

        If RsMrrCorr.EOF = False Then
            txtMrrNo.Text = RsMrrCorr.Fields("AUTO_KEY_MRR").Value
            FocCheck.CheckState = IIf(RsMrrCorr.Fields("MRR_FINAL_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        Else
            MsgBox("Invalid MRR.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmMrrCorrection_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmMrrCorrection_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection		
        ''PvtDBCn.Open StrConn		


        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(2145)
        'Me.Width = VB6.TwipsToPixelsX(4425)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmMrrCorrection_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsMrrCorr = Nothing
        RsMrrCorr.Close()
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            CmdSave.Enabled = False
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim RsTemp As ADODB.Recordset
        Dim mFocCheck As String
        Dim mISFINALPOST As String
        Dim mInPurchase As Boolean


        mFocCheck = IIf(FocCheck.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mInPurchase = False

        SqlStr = " Select ISFINALPOST from fin_purchase_hdr " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR= " & Val(txtMrrNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mInPurchase = True
            If mFocCheck = "Y" Then
                MsgBox("You Cann't FOC This MRR, Because Excise/Sale Tax Entry are Made Againt This MRR....")
                Update1 = True
                Exit Function
            Else
                mISFINALPOST = IIf(IsDBNull(RsTemp.Fields("ISFINALPOST").Value), "N", RsTemp.Fields("ISFINALPOST").Value)
                If mISFINALPOST = "Y" Then
                    MsgBox("This Bill is Posted, So You Cann't Update MRR....")
                    Update1 = True
                    Exit Function
                End If
            End If
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""

        SqlStr = " UPDATE INV_GATE_HDR  SET " & vbCrLf & " MRR_FINAL_FLAG='" & mFocCheck & "', UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
            & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR= " & Val(txtMrrNo.Text) & ""

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''		
        RsMrrCorr.Requery() ''.Refresh		
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
End Class
