Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmItemLock
   Inherits System.Windows.Forms.Form
   'Dim PvtDBCn As ADODB.Connection

   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String

   Dim FormActive As Boolean
   Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
   End Sub
   Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click

      On Error GoTo ErrPart
      Dim mAuthorisation As String


      If chkUpdate.CheckState = System.Windows.Forms.CheckState.Unchecked Then
         If lblBookType.Text = "O" Then
            MsgBox("You have no Right to uncheck over max level check. ", MsgBoxStyle.Critical)
            Exit Sub
         Else
            XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
            mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")
            If mAuthorisation = "N" Then
               MsgBox("You have no Right to Update. ", MsgBoxStyle.Critical)
               Exit Sub
            End If
         End If
      End If

      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

      If Update1 = False Then GoTo ErrPart
      CmdSave.Enabled = False
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
      Exit Sub
ErrPart:
      ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub frmItemLock_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
      On Error GoTo ERR1

      If FormActive = True Then Exit Sub

      If lblBookType.Text = "M" Then
         Me.Text = "Item Wise MRR Lock / Unlock"
      ElseIf lblBookType.Text = "O" Then
         Me.Text = "Item Wise MRR Lock Over Max Level"
      ElseIf lblBookType.Text = "S" Then
         Me.Text = "Item Wise Schedule Lock / Unlock"
      ElseIf lblBookType.Text = "L" Then
         Me.Text = "Stock Qty Lock for MRR"
      End If

      chkUpdate.CheckState = System.Windows.Forms.CheckState.Unchecked
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

      FormActive = True
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
      Exit Sub
ERR1:
      MsgBox(Err.Description)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub frmItemLock_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub frmItemLock_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

      On Error GoTo ErrPart
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
      Call SetMainFormCordinate(Me)
      'Me.Top = 0
      'Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(2400)
        ''Me.Width = VB6.TwipsToPixelsX(8640)

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)

        Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_CODE", "", txtItemCode)
        Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_SHORT_DESC", "", txtItemName)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Show1(ByRef pFieldName As String, ByRef pFieldValue As String)

        On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mLock As String

        mLock = "N"
        txtQty.Text = "0"

        SqlStr = " SELECT ITEM_CODE,ITEM_SHORT_DESC, ISSUE_UOM,MRR_LOCK, MRR_LOCK_OVERMAX, SCHEDULE_LOCK, STOCK_LOCK_QTY" & vbCrLf _
              & " FROM INV_ITEM_MST" & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
              & " AND " & pFieldName & "='" & MainClass.AllowSingleQuote(pFieldValue) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtItemCode.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
            txtItemName.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)


            If lblBookType.Text = "M" Then
                mLock = IIf(IsDBNull(RsTemp.Fields("MRR_LOCK").Value), "N", RsTemp.Fields("MRR_LOCK").Value)
            ElseIf lblBookType.Text = "O" Then
                mLock = IIf(IsDBNull(RsTemp.Fields("MRR_LOCK_OVERMAX").Value), "N", RsTemp.Fields("MRR_LOCK_OVERMAX").Value)
            ElseIf lblBookType.Text = "S" Then
                mLock = IIf(IsDBNull(RsTemp.Fields("SCHEDULE_LOCK").Value), "N", RsTemp.Fields("SCHEDULE_LOCK").Value)
            ElseIf lblBookType.Text = "L" Then
                txtQty.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("STOCK_LOCK_QTY").Value), 0, RsTemp.Fields("STOCK_LOCK_QTY").Value), "0.00")
                mLock = "N"
            End If
            chkUpdate.CheckState = IIf(mLock = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            CmdSave.Enabled = True
        End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateErr
        Dim SqlStr As String = ""
        Dim mFieldName As String = ""
        Dim mFieldValue As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()



        If lblBookType.Text = "M" Then
            mFieldName = "MRR_LOCK" 'MRR_LOCK_OVERMAX,SCHEDULE_LOCK
            mFieldValue = IIf(chkUpdate.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", "Y")
        ElseIf lblBookType.Text = "O" Then
            mFieldName = "MRR_LOCK_OVERMAX" ',SCHEDULE_LOCK
            mFieldValue = IIf(chkUpdate.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", "Y")
        ElseIf lblBookType.Text = "S" Then
            mFieldName = "SCHEDULE_LOCK" 'MRR_LOCK_OVERMAX,
            mFieldValue = IIf(chkUpdate.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", "Y")
        ElseIf lblBookType.Text = "L" Then
            mFieldName = "STOCK_LOCK_QTY" 'MRR_LOCK_OVERMAX,
            mFieldValue = VB6.Format(txtQty.Text, "0.00")
        End If

        SqlStr = " UPDATE INV_ITEM_MST " & vbCrLf _
           & " SET " & mFieldName & "='" & mFieldValue & "'," & vbCrLf _
           & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
           & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
           & " WHERE Company_Code= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
           & " AND ITEM_CODE= '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "' "


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
    Private Sub frmItemLock_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        Call ItemSearch("N")
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call ItemSearch("N")
    End Sub
    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtItemName.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Item Name Does Not Exist In Master")
            Cancel = True
            Exit Sub
        Else
            Call Show1("ITEM_SHORT_DESC", txtItemName.Text)
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        Call ItemSearch("Y")
    End Sub
    Private Sub txtItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call ItemSearch("Y")
    End Sub
    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Item Name Does Not Exist In Master")
            Cancel = True
            Exit Sub
        Else
            Call Show1("ITEM_CODE", txtItemCode.Text)
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub ItemSearch(ByRef pIsItemCode As String)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If pIsItemCode = "Y" Then
            If MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtItemCode.Text = AcName
                txtItemCode_Validating(txtItemCode, New System.ComponentModel.CancelEventArgs(False))
                If txtItemCode.Enabled = True Then txtItemCode.Focus()
            End If
        Else
            If MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtItemName.Text = AcName
                TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(False))
                If txtItemName.Enabled = True Then txtItemName.Focus()
            End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

   Private Sub txtQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQty.TextChanged

      MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

      KeyAscii = MainClass.SetNumericField(KeyAscii)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
End Class
