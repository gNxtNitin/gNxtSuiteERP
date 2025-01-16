Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCRHoldLock
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

   Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
      Call txtDept_DoubleClick(txtDept, New System.EventArgs())
   End Sub

   Private Sub frmCRHoldLock_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
      On Error GoTo ERR1

      If FormActive = True Then Exit Sub

      Me.Text = "CR Hold Material Lock"

      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

      FormActive = True
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
      Exit Sub
ERR1:
      MsgBox(Err.Description)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub frmCRHoldLock_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub frmCRHoldLock_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        Call AutoCompleteSearch("PAY_DEPT_MST", "DEPT_CODE", "", txtDept)

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

        SqlStr = " SELECT * FROM GEN_CR_STOCK_LOCK " & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
              & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
              & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _
              & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
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

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " DELETE FROM GEN_CR_STOCK_LOCK " & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
              & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" & vbCrLf _
              & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
              & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO GEN_CR_STOCK_LOCK ( " & vbCrLf _
              & " COMPANY_CODE, DEPT_CODE, AUTO_KEY_MRR, ITEM_CODE, " & vbCrLf _
              & " TILL_DATE, REMARKS) VALUES  (" & vbCrLf _
              & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtDept.Text) & "'," & Val(txtMRRNo.Text) & ", " & vbCrLf _
              & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf _
              & " TO_DATE('" & VB6.Format(txtDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtRemarks.Text) & "')"


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
    Private Sub frmCRHoldLock_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = "" 

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtDept_DoubleClick(txtDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call AutoCompleteSearch("DSP_CR_TRN", "AUTO_KEY_MRR", "DEPT_CODE='" & txtDept.Text & "' AND STOCK_TYPE='WC'", txtMRRNo)

        'On Error GoTo ErrPart
        '
        '    If Trim(txtDept.Text) = "" Then Exit Sub
        '
        '    If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        Call Show1
        '    Else
        '        MsgInformation "Invalid Depatment Code"
        '        Cancel = True
        '    End If
        '
        'Exit Sub
        'ErrPart:
        '    ErrorMsg err.Description, err.Number, vbCritical
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtProductCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.DoubleClick
        '    Call ItemSearch("Y")
    End Sub
    Private Sub txtProductCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProductCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProductCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtProductCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProductCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    If KeyCode = vbKeyF1 Then Call ItemSearch("Y")
    End Sub
    Private Sub txtProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Item Name Does Not Exist In Master")
            Cancel = True
            Exit Sub
        Else
            lblProductCode.Text = MasterNo
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    'Private Sub ItemSearch(pIsItemCode As String)
    'On Error GoTo ErrPart
    'Dim SqlStr  As String
    '
    '    If pIsItemCode = "Y" Then
    '        If MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '            txtProductCode.Text = AcName
    '            txtProductCode_Validate False
    '            If txtProductCode.Enabled = True Then txtProductCode.SetFocus
    '        End If
    '    Else
    '        If MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '            txtItemName.Text = AcName
    '            TxtItemName_Validate False
    '            If txtItemName.Enabled = True Then txtItemName.SetFocus
    '        End If
    '    End If
    'Exit Sub
    'ErrPart:
    '    ErrorMsg err.Description, err.Number, vbCritical
    'End Sub

    Private Sub TxtMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtMRRNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.DoubleClick
        cmdMRRSearch_Click(cmdMRRSearch, New System.EventArgs())
    End Sub
    Private Sub TxtMRRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtMRRNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMRRNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdMRRSearch_Click(cmdMRRSearch, New System.EventArgs())
    End Sub

    Public Sub TxtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mMRRNo As String
        Dim SqlStr As String = "" 
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDivisionCode As Integer

        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub
        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub
        '    If Trim(cboDivision.Text) = "" Then Exit Sub
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub

        '    If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mDivisionCode = Trim(MasterNo)
        '    End If

        If Len(txtMRRNo.Text) < 6 Then
            txtMRRNo.Text = Val(txtMRRNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mMRRNo = Trim(txtMRRNo.Text)

        SqlStr = "SELECT AUTO_KEY_MRR, ITEM_CODE, ITEM_UOM, MRR_DATE, " & vbCrLf _
              & " SUM(DECODE(ITEM_IO,'I',1,-1) * DECODE(STOCK_TYPE,'SR',1,0) * ITEM_QTY) As ITEM_QTY," & vbCrLf _
              & " SUM(DECODE(ITEM_IO,'I',1,-1) * DECODE(STOCK_TYPE,'WC',1,0) * ITEM_QTY) As ITEM_WC_QTY" & vbCrLf _
              & " FROM DSP_CR_TRN" & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
              & " AND AUTO_KEY_MRR=" & mMRRNo & "" & vbCrLf _
              & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
              & " AND DEPT_CODE='" & txtDept.Text & "'" & vbCrLf _
              & " AND STOCK_TYPE IN ('SR','WC')" & vbCrLf _
              & " GROUP BY AUTO_KEY_MRR, MRR_DATE, ITEM_CODE,ITEM_UOM " & vbCrLf _
              & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY)>0"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtMRRDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("MRR_DATE").Value), "", RsTemp.Fields("MRR_DATE").Value), "dd/MM/yyyy")
            If MainClass.ValidateWithMasterTable(Trim(txtProductCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblProductCode.Text = Trim(MasterNo)
            End If
            Call ShowExitsData()
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdMRRSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMRRSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = "" 
        Dim mDivisionCode As Double


        '    If Trim(cboDivision.Text) = "" Then MsgInformation "Please select the Division Code": Exit Sub
        If Trim(txtDept.Text) = "" Then MsgInformation("Please select the Dept Code") : Exit Sub

        '    If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mDivisionCode = Trim(MasterNo)
        '    End If

        SqlStr = "SELECT AUTO_KEY_MRR, ITEM_CODE, MRR_DATE, SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY) As ITEM_QTY" & vbCrLf _
           & " FROM DSP_CR_TRN" & vbCrLf _
           & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
           & " AND DEPT_CODE='" & txtDept.Text & "'" & vbCrLf _
           & " AND STOCK_TYPE='WC'" & vbCrLf _
           & " GROUP BY AUTO_KEY_MRR, MRR_DATE, ITEM_CODE " & vbCrLf _
           & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY)>0"

        If MainClass.SearchGridMasterBySQL2(txtMRRNo.Text, SqlStr) = True Then
            txtMRRNo.Text = AcName
            txtProductCode.Text = AcName1
            TxtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(True))
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
