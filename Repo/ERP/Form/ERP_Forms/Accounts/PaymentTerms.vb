Option Strict Off
Option Explicit On
Imports System.Data.SqlClient   '' System.Data.OleDb	
Imports System.Data.OleDb
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPayTerms
   Inherits System.Windows.Forms.Form
   Dim RsPaymentTerms As ADODB.Recordset
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   Private PvtDBCn As ADODB.Connection

   Dim xCode As String
   Dim FormActive As Boolean
   Dim Shw As Boolean
   Dim MasterNo As Object
    Dim SqlStr As String = ""
    Private Sub ViewGrid()

        On Error GoTo ErrorPart
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            'ADataGrid.Refresh()	
            SprdView.Refresh()
            FraGridView.Visible = True
            FraView.Visible = False
            SprdView.Focus()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraGridView.Visible = False
            FraView.Visible = True
        End If


        MainClass.ButtonStatus(Me, XRIGHT, RsPaymentTerms, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtCode.Text = ""
        txtDesc.Text = ""
        txtFromDays.Text = ""
        txtToDays.Text = ""
        txtCode.Enabled = True

        chkMSMEApp.CheckState = CheckState.Unchecked
        chkMSMEApp.Enabled = True
        Call AutoCompleteSearch("FIN_PAYTERM_MST", "PAY_TERM_CODE", "", txtCode)
        Call AutoCompleteSearch("FIN_PAYTERM_MST", "PAY_TERM_DESC", "", txtDesc)

        MainClass.ButtonStatus(Me, XRIGHT, RsPaymentTerms, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub chkMSMEApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMSMEApp.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPaymentTerms, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
            txtCode.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        On Error GoTo ErrorPart
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh	
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            FraGridView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraGridView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsPaymentTerms, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo ERR1
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtCode.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide() ''me.hide 
        Me.Dispose()
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""

        PvtDBCn.Errors.Clear()
        PvtDBCn.BeginTrans()
        If InsertIntoDelAudit(PvtDBCn, "FIN_PAYTERM_MST", (txtCode.Text), RsPaymentTerms, "PAY_TERM_DESC", "D") = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM FIN_PAYTERM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PAY_TERM_CODE='" & MainClass.AllowSingleQuote(UCase((txtCode.Text))) & "'"

        PvtDBCn.Execute(SqlStr)
        PvtDBCn.CommitTrans()
        RsPaymentTerms.Requery() ''.Refresh	
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PvtDBCn.RollbackTrans() ''	
        RsPaymentTerms.Requery() ''.Refresh	
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtCode.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsPaymentTerms.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                If Delete1 = False Then GoTo DelErrPart
                If RsPaymentTerms.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        ErrorMsg("Record Not Deleted", "DELETE", MsgBoxStyle.Critical)
    End Sub
    Private Sub frmPayTerms_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmPayTerms_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFromDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromDays.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFromDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFromDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtToDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToDays.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtCode.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsPaymentTerms.EOF = False Then xCode = RsPaymentTerms.Fields("PAY_TERM_CODE").Value

        SqlStr = "SELECT * FROM FIN_PAYTERM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PAY_TERM_CODE='" & MainClass.AllowSingleQuote(UCase((Trim(txtCode.Text)))) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPaymentTerms, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPaymentTerms.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Payment Terms Code Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM FIN_PAYTERM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAY_TERM_CODE='" & xCode & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPaymentTerms, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmPayTerms_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From FIN_PAYTERM_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPaymentTerms, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmPayTerms_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        PvtDBCn = New ADODB.Connection
        PvtDBCn.Open(StrConn)

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CmdView.Text = ConCmdGridViewCaption
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        'Me.Left = 0
        'Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(5220)
        ''Me.Width = VB6.TwipsToPixelsX(8265)
        Call frmPayTerms_Activated(eventSender, eventArgs)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmPayTerms_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsPaymentTerms = Nothing
        RsPaymentTerms.Close()
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mForMSME As String
        If Not RsPaymentTerms.EOF Then

            txtCode.Text = IIf(IsDbNull(RsPaymentTerms.Fields("PAY_TERM_CODE").Value), "", RsPaymentTerms.Fields("PAY_TERM_CODE").Value)
            txtDesc.Text = IIf(IsDbNull(RsPaymentTerms.Fields("PAY_TERM_DESC").Value), "", RsPaymentTerms.Fields("PAY_TERM_DESC").Value)

            txtFromDays.Text = IIf(IsDbNull(RsPaymentTerms.Fields("FROM_DAYS").Value), "", RsPaymentTerms.Fields("FROM_DAYS").Value)
            txtToDays.Text = IIf(IsDbNull(RsPaymentTerms.Fields("TO_DAYS").Value), "", RsPaymentTerms.Fields("TO_DAYS").Value)

            mForMSME = IIf(IsDBNull(RsPaymentTerms.Fields("FOR_MSME").Value), "N", RsPaymentTerms.Fields("FOR_MSME").Value)
            chkMSMEApp.CheckState = IIf(mForMSME = "Y", CheckState.Checked, CheckState.Unchecked)

            xCode = RsPaymentTerms.Fields("PAY_TERM_CODE").Value
        End If
        txtCode.Enabled = True
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPaymentTerms, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        Resume
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
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

        PvtDBCn.Errors.Clear()
        PvtDBCn.BeginTrans()

        Dim RsTemp As ADODB.Recordset
        Dim xCompanyCode As Long
        Dim mForMSME As String

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mForMSME = IIf(chkMSMEApp.CheckState = CheckState.Checked, "Y", "N")

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value

                SqlStr = ""
                If ADDMode = True Then
                    SqlStr = "INSERT INTO FIN_PAYTERM_MST (" & vbCrLf _
                        & " COMPANY_CODE, PAY_TERM_CODE, PAY_TERM_DESC, " & vbCrLf _
                        & " FROM_DAYS,TO_DAYS, FOR_MSME) VALUES ( " & vbCrLf _
                        & " " & xCompanyCode & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtDesc.Text) & "'," & vbCrLf _
                        & " " & Val(txtFromDays.Text) & "," & vbCrLf _
                        & " " & Val(txtToDays.Text) & " ,'" & mForMSME & "')"
                Else
                    SqlStr = " UPDATE FIN_PAYTERM_MST  SET " & vbCrLf _
                        & " PAY_TERM_DESC='" & MainClass.AllowSingleQuote(txtDesc.Text) & "'," & vbCrLf _
                        & " FROM_DAYS=" & Val(txtFromDays.Text) & ",FOR_MSME='" & mForMSME & "'," & vbCrLf _
                        & " TO_DAYS=" & Val(txtToDays.Text) & "" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
                        & " AND PAY_TERM_CODE= '" & xCode & "'"
                End If
UpdatePart:
                PvtDBCn.Execute(SqlStr)
                RsTemp.MoveNext()
            Loop
        End If

        PvtDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PvtDBCn.RollbackTrans() ''	
        RsPaymentTerms.Requery() ''.Refresh	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtCode.Maxlength = RsPaymentTerms.Fields("PAY_TERM_CODE").DefinedSize
        txtDesc.Maxlength = RsPaymentTerms.Fields("PAY_TERM_DESC").DefinedSize
        txtFromDays.Maxlength = RsPaymentTerms.Fields("FROM_DAYS").Precision
        txtToDays.Maxlength = RsPaymentTerms.Fields("TO_DAYS").Precision
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If Trim(txtCode.Text) = "" Then
            MsgInformation(" Payment terms code is empty. Cannot Save")
            txtCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDesc.Text) = "" Then
            MsgInformation(" Payment terms Description is empty. Cannot Save")
            txtDesc.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtFromDays.Text) = 0 Then
            MsgInformation("From Days Cann't be blank. Cannot Save")
            txtFromDays.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtToDays.Text) = 0 Then
            MsgInformation("To Days Cann't be blank. Cannot Save")
            txtToDays.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtToDays.Text) < Val(txtFromDays.Text) Then
            MsgInformation("To Days Cann't be less than From Days. Cannot Save")
            txtToDays.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsPaymentTerms.EOF = True Then Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT PAY_TERM_CODE AS CODE , PAY_TERM_DESC, FROM_DAYS, TO_DAYS, DECODE(FOR_MSME,'Y','YES','NO') FOR_MSME " & vbCrLf _
            & " FROM FIN_PAYTERM_MST" & vbCrLf _
            & " WHERE FIN_PAYTERM_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & "ORDER BY PAY_TERM_CODE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub
   Private Sub FormatSprdView()

      With SprdView
         .Row = -1
         .set_RowHeight(0, 12)
         .set_ColWidth(0, 5)
         .set_ColWidth(1, 8)
         .set_ColWidth(2, 25)
         .set_ColWidth(3, 10)
         .set_ColWidth(4, 10)
         .ColsFrozen = 1
         MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
         MainClass.SetSpreadColor(SprdView, -1)
         .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
         MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
      End With
   End Sub
   Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        Dim mTitle As String = ""
      On Error GoTo ERR1
      mTitle = ""
      Report1.Reset()
      mTitle = "Payment Terms Master"
      Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PayTerms.rpt"
      SetCrpt(Report1, Mode, 1, mTitle)
      Report1.WindowShowGroupTree = False
      Report1.Action = 1
      Exit Sub
ERR1:
      MsgInformation(Err.Description)
   End Sub

   Private Sub txtDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDesc.TextChanged

      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub
    Private Sub chkMSMEApp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMSMEApp.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDesc.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

      KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub txtDesc_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtDesc.Validating
      Dim Cancel As Boolean = EventArgs.Cancel

      On Error GoTo ERR1
      SqlStr = ""
      If Trim(txtDesc.Text) = "" Then GoTo EventExitSub
      If MODIFYMode = True And RsPaymentTerms.EOF = False Then xCode = RsPaymentTerms.Fields("PAY_TERM_CODE").Value

      SqlStr = "SELECT * FROM FIN_PAYTERM_MST " & vbCrLf _
          & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
          & " AND PAY_TERM_DESC='" & MainClass.AllowSingleQuote(UCase((Trim(txtDesc.Text)))) & "'"
      MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPaymentTerms, ADODB.LockTypeEnum.adLockReadOnly)

      If RsPaymentTerms.EOF = False Then
         ADDMode = False
         MODIFYMode = False
         Show1()
      Else
         If ADDMode = False And MODIFYMode = False Then
            MsgBox("Payment Terms Code Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
            Cancel = True
         ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "SELECT * FROM FIN_PAYTERM_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND PAY_TERM_CODE='" & xCode & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPaymentTerms, ADODB.LockTypeEnum.adLockReadOnly)
         End If
      End If
      GoTo EventExitSub
ERR1:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      EventArgs.Cancel = Cancel
   End Sub

   Private Sub SprdView_DblClick(sender As Object, e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
      SprdView.Col = 1
      SprdView.Row = SprdView.ActiveRow
      txtCode.Text = Trim(SprdView.Text)
      txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
      CmdView_Click(CmdView, New System.EventArgs())
   End Sub
End Class
