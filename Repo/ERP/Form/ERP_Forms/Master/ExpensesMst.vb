Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmExpensesMst
   Inherits System.Windows.Forms.Form
   Dim RsGeneral As ADODB.Recordset
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   'Private PvtDBCn As ADODB.Connection

   Dim xCode As String
   Dim FormActive As Boolean
   Dim Shw As Boolean
   Dim MasterNo As Object
    Dim SqlStr As String = ""
    Private Sub ViewGrid()

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
        MainClass.ButtonStatus(Me, XRIGHT, RsGeneral, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtCode.Text = ""
        txtDesc.Text = ""
        txtCode.Enabled = True
        OptStatus(0).Checked = True

        Call AutoCompleteSearch("CST_CENTER_MST", "COST_CENTER_CODE", "", txtCode)
        Call AutoCompleteSearch("CST_CENTER_MST", "COST_CENTER_DESC", "", txtDesc)
        MainClass.ButtonStatus(Me, XRIGHT, RsGeneral, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        Sqlstr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "CST_CENTER_MST", (txtCode.Text), RsGeneral, "COST_CENTER_DESC") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "CST_CENTER_MST", "COST_CENTER_CODE", (txtCode.Text)) = False Then GoTo DeleteErr

        Sqlstr = " DELETE FROM CST_CENTER_MST " & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
              & " AND COST_CENTER_CODE='" & MainClass.AllowSingleQuote(UCase((txtCode.Text))) & "'"

        PubDBCn.Execute(Sqlstr)
        PubDBCn.CommitTrans()
        RsGeneral.Requery() ''.Refresh	
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''	
        RsGeneral.Requery() ''.Refresh	
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtCode.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            MsgInformation("Cann't be Delete.")
            Exit Sub
        End If

        If Not RsGeneral.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                If Delete1 = False Then GoTo DelErrPart
                If RsGeneral.EOF = True Then
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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo SearchError
        If MainClass.SearchGridMaster(txtCode.Text, "CST_CENTER_MST", "COST_CENTER_CODE", "COST_CENTER_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCode.Text = AcName
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub
SearchError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmExpensesMst_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmExpensesMst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtCode.Text = Trim(SprdView.Text)
        txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
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
        Sqlstr = ""
        If Trim(txtCode.Text) = "" Then GoTo EventExitSub
        txtCode.Text = VB6.Format(txtCode.Text, "000")
        If MODIFYMode = True And RsGeneral.EOF = False Then xCode = RsGeneral.Fields("COST_CENTER_CODE").Value

        Sqlstr = "SELECT * FROM CST_CENTER_MST " & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
              & " AND COST_CENTER_CODE='" & MainClass.AllowSingleQuote(UCase((Trim(txtCode.Text)))) & "'"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)

        If RsGeneral.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Category Code Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                Sqlstr = ""
                Sqlstr = "SELECT * FROM CST_CENTER_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND COST_CENTER_CODE='" & xCode & "'"

                MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmExpensesMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From CST_CENTER_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)

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
    Private Sub frmExpensesMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

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
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmExpensesMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsGeneral = Nothing
        RsGeneral.Close()
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        If Not RsGeneral.EOF Then

            txtCode.Text = IIf(IsDbNull(RsGeneral.Fields("COST_CENTER_CODE").Value), "", RsGeneral.Fields("COST_CENTER_CODE").Value)
            txtDesc.Text = IIf(IsDbNull(RsGeneral.Fields("COST_CENTER_DESC").Value), "", RsGeneral.Fields("COST_CENTER_DESC").Value)
            xCode = RsGeneral.Fields("COST_CENTER_CODE").Value

            If RsGeneral.Fields("STATUS").Value = "O" Then
                OptStatus(0).Checked = True
            Else
                OptStatus(1).Checked = True
            End If

        End If
        txtCode.Enabled = True
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsGeneral, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
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
        Dim mStatus As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Long

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        Sqlstr = ""
        mStatus = IIf(OptStatus(0).Checked = True, "O", "C")

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                If ADDMode = True Then
                    '        mCode = MainClass.AutoGenRowNo("FIN_TARRIF_MST", "Code", PubDBCn)	
                    SqlStr = "INSERT INTO CST_CENTER_MST (" & vbCrLf _
                            & " COMPANY_CODE, COST_CENTER_CODE, " & vbCrLf _
                            & " COST_CENTER_TYPE, COST_CENTER_DESC, STATUS " & vbCrLf _
                            & " ) VALUES ( " & vbCrLf _
                            & " " & xCompanyCode & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                            & " 'C','" & MainClass.AllowSingleQuote(txtDesc.Text) & "'," & vbCrLf _
                            & " '" & mStatus & "')"

                Else
                    SqlStr = " UPDATE CST_CENTER_MST  SET " & vbCrLf _
                            & " COST_CENTER_DESC='" & MainClass.AllowSingleQuote(txtDesc.Text) & "'," & vbCrLf _
                            & " STATUS='" & mStatus & "'" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
                            & " AND COST_CENTER_CODE= '" & xCode & "'" & vbCrLf _
                            & " AND COST_CENTER_TYPE='C'"
                End If

UpdatePart:
                PubDBCn.Execute(SqlStr)
                RsTemp.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''	
        RsGeneral.Requery() ''.Refresh	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtCode.Maxlength = RsGeneral.Fields("COST_CENTER_CODE").DefinedSize
        txtDesc.Maxlength = RsGeneral.Fields("COST_CENTER_DESC").DefinedSize ''	
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If Trim(txtCode.Text) = "" Then
            MsgInformation(" Category code is empty. Cannot Save")
            txtCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDesc.Text) = "" Then
            MsgInformation(" Category Description is empty. Cannot Save")
            txtDesc.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsGeneral.EOF = True Then Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        Sqlstr = ""

        Sqlstr = " SELECT COST_CENTER_CODE,COST_CENTER_DESC " & vbCrLf & " FROM CST_CENTER_MST" & vbCrLf & " WHERE CST_CENTER_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        Sqlstr = Sqlstr & vbCrLf & "ORDER BY COST_CENTER_DESC"

        MainClass.AssignDataInSprd8(Sqlstr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub
   Private Sub FormatSprdView()

      With SprdView
         .Row = -1
         .set_RowHeight(0, 12)
         .set_ColWidth(0, 5)
         .set_ColWidth(1, 8)
         .set_ColWidth(2, 30)
         .ColsFrozen = 1
         MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
         MainClass.SetSpreadColor(SprdView, -1)
         .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle	
         MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
      End With
   End Sub
   Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        Dim mTitle As String = ""
      On Error GoTo ERR1
      mTitle = ""
      Report1.Reset()
      mTitle = "Category Master"
      Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\Tariff.rpt"
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

   Private Sub txtDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDesc.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

      KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub CmdAdd_Click(sender As Object, e As System.EventArgs) Handles CmdAdd.Click
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

   Private Sub CmdModify_Click(sender As Object, e As System.EventArgs) Handles CmdModify.Click
      On Error GoTo ModifyErr
      If CmdModify.Text = ConcmdmodifyCaption Then
         ADDMode = False
         MODIFYMode = True
         MainClass.ButtonStatus(Me, XRIGHT, RsGeneral, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
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
   Private Sub cmdPreview_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles cmdPreview.Click
      Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
      ShowReport(Crystal.DestinationConstants.crptToWindow)
      Me.Cursor = System.Windows.Forms.Cursors.Default
   End Sub

   Private Sub cmdPrint_Click(sender As Object, e As System.EventArgs) Handles cmdPrint.Click
      Me.Cursor = System.Windows.Forms.Cursors.Arrow
      ShowReport(Crystal.DestinationConstants.crptToPrinter)
      Me.Cursor = System.Windows.Forms.Cursors.Default
   End Sub

   Private Sub CmdView_Click(sender As Object, e As System.EventArgs) Handles CmdView.Click
      ViewGrid()
   End Sub

   Private Sub txtDesc_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtDesc.Validating
      Dim Cancel As Boolean = EventArgs.Cancel

      On Error GoTo ERR1
      Sqlstr = ""
      If Trim(txtDesc.Text) = "" Then GoTo EventExitSub

      If MODIFYMode = True And RsGeneral.EOF = False Then xCode = RsGeneral.Fields("COST_CENTER_CODE").Value

      Sqlstr = "SELECT * FROM CST_CENTER_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND COST_CENTER_DESC='" & MainClass.AllowSingleQuote(UCase((Trim(txtDesc.Text)))) & "'"

      MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)

      If RsGeneral.EOF = False Then
         ADDMode = False
         MODIFYMode = False
         Show1()
      Else
         If ADDMode = False And MODIFYMode = False Then
            MsgBox("Category Code Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
            Cancel = True
         ElseIf MODIFYMode = True Then
            Sqlstr = ""
            Sqlstr = "SELECT * FROM CST_CENTER_MST " & vbCrLf _
               & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
               & " AND COST_CENTER_CODE='" & xCode & "'"

            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)
         End If
      End If
      GoTo EventExitSub
ERR1:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      EventArgs.Cancel = Cancel
   End Sub
End Class
