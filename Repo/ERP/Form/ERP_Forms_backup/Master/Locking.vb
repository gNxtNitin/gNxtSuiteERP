Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLocking
   Inherits System.Windows.Forms.Form
   'Dim PvtDBCN As ADODB.Connection			
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
   End Sub
   Private Function FieldVarification() As Boolean
      On Error GoTo ERR1
      Dim mLockingRights As String

      FieldVarification = True
      If TxtBDateFrom.Text <> "" Then
         If TxtBDateTo.Text = "" Then
            MsgBox("Company DateTo Is Blank.")
            FieldVarification = False
            TxtBDateTo.Focus()
            Exit Function
         End If
      End If
      If TxtBDateTo.Text <> "" Then
         If TxtBDateFrom.Text = "" Then
            MsgBox("Company DateFrom Is Blank.")
            FieldVarification = False
            TxtBDateFrom.Focus()
            Exit Function
         End If
      End If
      If IsDate(TxtBDateTo.Text) = True And IsDate(TxtBDateFrom.Text) = True Then
         If CDate(TxtBDateTo.Text) < CDate(TxtBDateFrom.Text) Then
            MsgBox("Company DateTo is greater than Company DateFrom .")
            FieldVarification = False
            Exit Function
         End If
      End If

      If txtDateFrom.Text <> "" Then
         If txtDateTo.Text = "" Then
            MsgBox("Accounts DateTo Is Blank.")
            FieldVarification = False
            txtDateTo.Focus()
            Exit Function
         End If
      End If
      If txtDateTo.Text <> "" Then
         If txtDateFrom.Text = "" Then
            MsgBox("Accounts DateFrom Is Blank.")
            FieldVarification = False
            txtDateFrom.Focus()
            Exit Function
         End If
      End If

      mLockingRights = GetUserPermission("BOOK_LOCKING", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)

      If mLockingRights = "N" Then
         MsgInformation("You Have no enough Rights.")
         FieldVarification = False
         Exit Function
      End If

      '    If TxtName = "" Then		
      '        MsgBox "Account Name is Blank"		
      '        FieldVarification = False		
      '        TxtName.SetFocus		
      '        Exit Function		
      '    End If		

      '    If TxtDateto < TxtDatefrom Then		
      '        MsgBox "Account DateTo is greater than Account DateFrom ."		
      '        FieldVarification = False		
      '        Exit Function		
      '    End If		
      Exit Function
ERR1:
      MsgBox(Err.Description)
      FieldVarification = False
   End Function
   Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
      On Error GoTo ErrorHandler
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
      If FieldVarification = False Then GoTo ExitProc
      Update1()
      CmdSave.Enabled = False
      GoTo ExitProc
ErrorHandler:
      If Err.Number = 75 Then
         MsgBox(Err.Description)
         CmdSave.Enabled = False
      Else
         MsgBox(Err.Description)
      End If
ExitProc:
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub

   Private Sub frmLocking_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

      On Error GoTo ErrPart
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

      Call SetMainFormCordinate(Me)

        'Me.Top = VB6.TwipsToPixelsY(25)
        'Me.Left = VB6.TwipsToPixelsX(25)
        'Me.Height = VB6.TwipsToPixelsY(2940)
        ''Me.Width = VB6.TwipsToPixelsX(5400)
        'Set PvtDBCN = New ADODB.Connection		
        'PvtDBCN.Open StrConn		
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        If XRIGHT <> "" Then MODIFYMode = True
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", TxtName)
        ADDMode = False
        Show1()
        CmdSave.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub frmLocking_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'PvtDBCN.Cancel		
        'PvtDBCN.Close		
        'Set PvtDBCN = Nothing		
    End Sub

    Private Sub txtbdatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtBDateFrom.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtbdatefrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtBDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If TxtBDateFrom.Text = "__/__/____" Then GoTo EventExitSub
        If IsDate(TxtBDateFrom.Text) = False Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            TxtBDateFrom.Focus()
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtbdateto_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtBDateTo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtbdateto_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtBDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If TxtBDateTo.Text = "__/__/____" Then GoTo EventExitSub
        If IsDate(TxtBDateTo.Text) = False Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            TxtBDateTo.Focus()
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtDateFrom.Text = "__/__/____" Then GoTo EventExitSub
        If IsDate(txtDateFrom.Text) = False Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            txtDateFrom.Focus()
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtDateTo.Text = "__/__/____" Then GoTo EventExitSub
        If IsDate(txtDateTo.Text) = False Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            txtDateFrom.Focus()
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Show1()

        On Error GoTo ErrPart
        Dim RsBranch As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = "Select * FROM FIN_PRINT_MST  WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBranch, ADODB.LockTypeEnum.adLockReadOnly)
        If RsBranch.EOF = False Then
            TxtBDateFrom.Text = IIf(IsDBNull(RsBranch.Fields("LockDatefrom").Value), "__/__/____", RsBranch.Fields("LockDatefrom").Value)
            TxtBDateTo.Text = IIf(IsDBNull(RsBranch.Fields("LockDateTo").Value), "__/__/____", RsBranch.Fields("LockDateTo").Value)
        End If
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
        ' Resume		
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(TxtName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            TxtName.Text = AcName
            txtName_Validating(TxtName, New System.ComponentModel.CancelEventArgs(False))
        End If
    End Sub

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtName_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.Leave

        Dim RsAccountname As ADODB.Recordset = Nothing
        MainClass.UOpenRecordSet("Select  *  from FIN_SUPP_CUST_MST WHERE SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtName.Text) & "'", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAccountname, ADODB.LockTypeEnum.adLockReadOnly)
        If RsAccountname.EOF = False Then
            txtDateFrom.Text = IIf(IsDBNull(RsAccountname.Fields("LockDatefrom").Value), "__/__/____", RsAccountname.Fields("LockDatefrom").Value)
            txtDateTo.Text = IIf(IsDBNull(RsAccountname.Fields("LockDateTo").Value), "__/__/____", RsAccountname.Fields("LockDateTo").Value)
        End If
    End Sub

    Private Sub txtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim MiscRs As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        If TxtName.Text = "" Then GoTo EventExitSub
        SqlStr = ""
        SqlStr = "Select SUPP_CUST_NAME from FIN_SUPP_CUST_MST  Where SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtName.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, MiscRs, ADODB.LockTypeEnum.adLockReadOnly)
        If MiscRs.EOF = True Then
            MsgBox("Invalid Account Name", MsgBoxStyle.Information)
            Cancel = True
        End If
        MiscRs.Close()
        MiscRs = Nothing
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mDateFrom As String
        Dim mDateTo As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        If IsDate(TxtBDateFrom.Text) And IsDate(TxtBDateTo.Text) Then
            mDateFrom = TxtBDateFrom.Text
            mDateTo = TxtBDateTo.Text
        Else
            mDateTo = ""
            mDateFrom = ""
        End If
        SqlStr = "UPDATE FIN_PRINT_MST Set LockDatefrom=TO_DATE('" & VB6.Format(mDateFrom, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
           & " LockDateto=TO_DATE('" & VB6.Format(mDateTo, "dd-MMM-yyyy") & "','DD-MON-YYYY'), UPDATE_FROM='N'" & vbCrLf _
           & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & ""

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1() = False Then GoTo ERR1
        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        Update1 = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function
    Private Function UpdateDetail1() As Boolean
        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim mDateFrom As String
        Dim mDateTo As String

        If IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then
            mDateFrom = txtDateFrom.Text
            mDateTo = txtDateTo.Text
        Else
            mDateTo = ""
            mDateFrom = ""
        End If

        SqlStr = ""

        SqlStr = "Update FIN_SUPP_CUST_MST Set LockDatefrom=TO_DATE('" & VB6.Format(mDateFrom, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
              & "Lockdateto=TO_DATE('" & VB6.Format(mDateTo, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
              & " Where SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtName.Text) & "'"

        PubDBCn.Execute(SqlStr)

        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateDetail1 = False
    End Function

   Private Sub txtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtName.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then
         cmdsearch_Click(cmdsearch, New System.EventArgs())
      End If
   End Sub
End Class
