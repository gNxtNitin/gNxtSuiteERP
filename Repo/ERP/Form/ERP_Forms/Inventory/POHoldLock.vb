Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPOHoldLock
   Inherits System.Windows.Forms.Form
   'Dim PvtDBCn As ADODB.Connection

   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String

   Dim FormActive As Boolean
   Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
   End Sub
   Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
      On Error GoTo ErrPart


      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

      If Update1 = False Then GoTo ErrPart
      CmdSave.Enabled = False
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
      Exit Sub
ErrPart:
      ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub frmPOHoldLock_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
      On Error GoTo ERR1

      If FormActive = True Then Exit Sub

      Me.Text = "Purchase Order Hold For Posting"

      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

      FormActive = True
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
      Exit Sub
ERR1:
      MsgBox(Err.Description)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub frmPOHoldLock_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub frmPOHoldLock_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

      On Error GoTo ErrPart
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
      Call SetMainFormCordinate(Me)
      'Me.Top = 0
      'Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(3570)
        ''Me.Width = VB6.TwipsToPixelsX(8640)

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ShowExitsData()

        On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pMkey As Double

        pMkey = CDbl(Val(txtPONo.Text) & VB6.Format(Val(txtPOAmendNo.Text), "000"))
        SqlStr = " SELECT TRN.*, IH.AUTO_KEY_PO,  " & vbCrLf & " IH.SUPP_CUST_CODE, IH.PUR_ORD_DATE, IH.AMEND_NO" & vbCrLf & " FROM GEN_PO_UNLOCK TRN, PUR_PURCHASE_HDR IH " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=" & Val(CStr(pMkey)) & "" & vbCrLf & " AND IH.MKEY=TRN.MKEY"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtPONo.Text = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_PO").Value), "", RsTemp.Fields("AUTO_KEY_PO").Value)
            txtPOAmendNo.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("AMEND_NO").Value), 0, RsTemp.Fields("AMEND_NO").Value), "000")
            txtPODate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PUR_ORD_DATE").Value), "", RsTemp.Fields("PUR_ORD_DATE").Value), "dd/MM/yyyy")

            txtDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TILL_DATE").Value), "", RsTemp.Fields("TILL_DATE").Value), "dd/MM/yyyy")
            txtRemarks.Text = IIf(IsDBNull(RsTemp.Fields("REMARKS").Value), "", RsTemp.Fields("REMARKS").Value)
        End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateErr
        Dim SqlStr As String = ""
        Dim pMkey As Double

        pMkey = CDbl(Val(txtPONo.Text) & VB6.Format(Val(txtPOAmendNo.Text), "000"))

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " DELETE FROM GEN_PO_UNLOCK " & vbCrLf _
           & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
           & " AND MKEY=" & Val(CStr(pMkey)) & ""

        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO GEN_PO_UNLOCK ( " & vbCrLf _
              & " COMPANY_CODE, MKEY, AUTO_KEY_PO, AMEND_NO, " & vbCrLf _
              & " TILL_DATE, REMARKS,ADDUSER, ADDDATE) VALUES  (" & vbCrLf _
              & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(pMkey) & ", " & Val(txtPONo.Text) & " , " & Val(txtPOAmendNo.Text) & ", " & vbCrLf _
              & " TO_DATE('" & VB6.Format(txtDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
              & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))"


        PubDBCn.Execute(SqlStr)



        PubDBCn.Execute(SqlStr)

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
UpdateErr:
        Update1 = False
        PubDBCn.RollbackTrans()
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
        ''Resume
    End Function
    Private Sub frmPOHoldLock_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If

        If FYChk((txtDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If

        If CDate(txtDate.Text) < CDate(PubCurrDate) Then
            MsgInformation("Date Cann't be Less than Current Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPOAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPOAmendNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPOAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPOAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPOAmendNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPOAmendNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSuppCode As String
        If Trim(txtPONo.Text) = "" Then GoTo EventExitSub
        If Trim(txtPOAmendNo.Text) = "" Then GoTo EventExitSub

        txtSupplierName.Text = ""

        xMkey = Val(txtPONo.Text) & VB6.Format(txtPOAmendNo.Text, "000")


        SqlStr = "SELECT AUTO_KEY_PO, PUR_ORD_DATE, AMEND_NO, SUPP_CUST_CODE " & vbCrLf & " FROM PUR_PURCHASE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY=" & xMkey & " AND PO_STATUS='N'  AND PO_CLOSED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtPODate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PUR_ORD_DATE").Value), "", RsTemp.Fields("PUR_ORD_DATE").Value), "dd/MM/yyyy")
            mSuppCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)

            If MainClass.ValidateWithMasterTable(mSuppCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSupplierName.Text = Trim(MasterNo)
            End If

            Call ShowExitsData()
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPONo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.DoubleClick
        cmdPOSearch_Click(cmdPOSearch, New System.EventArgs())
    End Sub
    Private Sub txtPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPONo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPONo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdPOSearch_Click(cmdPOSearch, New System.EventArgs())
    End Sub

    Public Sub txtPONo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(txtPONo.Text) = "" Then GoTo EventExitSub
        If Trim(txtPOAmendNo.Text) = "" Then GoTo EventExitSub


        xMkey = Val(txtPONo.Text) & VB6.Format(txtPOAmendNo.Text, "000")


        SqlStr = "SELECT AUTO_KEY_PO, PUR_ORD_DATE, AMEND_NO " & vbCrLf & " FROM PUR_PURCHASE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY=" & xMkey & " AND PO_STATUS='N'  AND PO_CLOSED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtPODate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PUR_ORD_DATE").Value), "", RsTemp.Fields("PUR_ORD_DATE").Value), "dd/MM/yyyy")
            Call ShowExitsData()
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdPOSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPOSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivisionCode As Double


        SqlStr = "SELECT AUTO_KEY_PO, TO_CHAR(AMEND_NO,'000') AS AMEND_NO, PUR_ORD_DATE" & vbCrLf _
           & " FROM PUR_PURCHASE_HDR" & vbCrLf _
           & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
           & " AND PO_STATUS='N' AND PO_CLOSED='N'"

        If MainClass.SearchGridMasterBySQL2(txtPONo.Text, SqlStr) = True Then
            txtPONo.Text = AcName
            txtPOAmendNo.Text = VB6.Format(AcName1, "000")
            txtPONo_Validating(txtPONo, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

   Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

      MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

      KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
End Class
