Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCurrencyMaster
    Inherits System.Windows.Forms.Form
    Dim RsCurr As ADODB.Recordset
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
        MainClass.ButtonStatus(Me, XRIGHT, RsCurr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtCode.Text = ""
        txtDesc.Text = ""
        txtMinorCurrency.Text = ""
        txtRate.Text = "0.0000"

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        txtCode.Enabled = True

        Call AutoCompleteSearch("FIN_CURRENCY_MST", "CURR_CODE", "", txtCode)
        Call AutoCompleteSearch("FIN_CURRENCY_MST", "CURR_DESC", "", txtDesc)

        MainClass.ButtonStatus(Me, XRIGHT, RsCurr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Currency is Closed. So you Cann't be Modify.")
                Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsCurr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
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
        ViewGrid()
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
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide() ''me.hide 
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "FIN_CURRENCY_MST", (txtCode.Text), RsCurr, "CURR_DESC") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "FIN_CURRENCY_MST", "CURR_CODE", (txtCode.Text)) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM FIN_CURRENCY_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND CURR_CODE='" & MainClass.AllowSingleQuote(UCase((txtCode.Text))) & "'"

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsCurr.Requery() ''.Refresh		
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''		
        RsCurr.Requery() ''.Refresh		
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
        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Currency is Closed. So you Cann't be deleted.")
            Exit Sub
        End If
        If Not RsCurr.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.		
                If Delete1() = False Then GoTo DelErrPart
                If RsCurr.EOF = True Then
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
    Private Sub frmCurrencyMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmCurrencyMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
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
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
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
        If MODIFYMode = True And RsCurr.EOF = False Then xCode = RsCurr.Fields("CURR_CODE").Value

        SqlStr = "SELECT * FROM FIN_CURRENCY_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND CURR_CODE='" & MainClass.AllowSingleQuote(UCase((Trim(txtCode.Text)))) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCurr, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCurr.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Currency Code Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM FIN_CURRENCY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CURR_CODE='" & xCode & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCurr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmCurrencyMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From FIN_CURRENCY_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCurr, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmCurrencyMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection		
        ''PvtDBCn.Open StrConn		

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
    Private Sub frmCurrencyMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsCurr = Nothing
        RsCurr.Close()
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        If Not RsCurr.EOF Then

            txtCode.Text = IIf(IsDBNull(RsCurr.Fields("CURR_CODE").Value), "", RsCurr.Fields("CURR_CODE").Value)
            txtDesc.Text = IIf(IsDBNull(RsCurr.Fields("CURR_DESC").Value), "", RsCurr.Fields("CURR_DESC").Value)
            txtMinorCurrency.Text = IIf(IsDBNull(RsCurr.Fields("MINOR_CURR").Value), "", RsCurr.Fields("MINOR_CURR").Value)
            txtRate.Text = VB6.Format(IIf(IsDBNull(RsCurr.Fields("CON_FACTOR").Value), 0, RsCurr.Fields("CON_FACTOR").Value), "0.0000")
            chkStatus.CheckState = IIf(RsCurr.Fields("Status").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

            '        lblAddUser.text = IIf(IsdbNull(RsCurr!ADDUSER), "", RsCurr!ADDUSER)		
            '        lblAddDate.text = VB6.Format(IIf(IsdbNull(RsCurr!ADDDATE), "", RsCurr!ADDDATE), "dd/MM/yyyy")		
            '        lblModUser.text = IIf(IsdbNull(RsCurr!MODUSER), "", RsCurr!MODUSER)		
            '        lblModDate.text = VB6.Format(IIf(IsdbNull(RsCurr!MODDATE), "", RsCurr!MODDATE), "dd/MM/yyyy")		

            xCode = RsCurr.Fields("CURR_CODE").Value
        End If
        txtCode.Enabled = True
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsCurr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        Resume
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
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

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O")

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                SqlStr = ""
                If ADDMode = True Then
                    SqlStr = "INSERT INTO FIN_CURRENCY_MST (" & vbCrLf _
                        & " COMPANY_CODE, CURR_CODE, " & vbCrLf _
                        & " CURR_DESC, MINOR_CURR, CON_FACTOR, STATUS) VALUES ( " & vbCrLf _
                        & " " & xCompanyCode & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtDesc.Text) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtMinorCurrency.Text) & "'," & vbCrLf _
                        & " " & Val(txtRate.Text) & ", '" & mStatus & "' )"
                Else
                    SqlStr = " UPDATE FIN_CURRENCY_MST  SET " & vbCrLf _
                        & " CURR_DESC='" & MainClass.AllowSingleQuote(txtDesc.Text) & "'," & vbCrLf _
                        & " MINOR_CURR='" & MainClass.AllowSingleQuote(txtMinorCurrency.Text) & "'," & vbCrLf _
                        & " CON_FACTOR=" & Val(txtRate.Text) & "," & vbCrLf _
                        & " STATUS='" & mStatus & "'" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
                        & " AND CURR_CODE= '" & xCode & "'"
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
        RsCurr.Requery() ''.Refresh		
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtCode.MaxLength = RsCurr.Fields("CURR_CODE").DefinedSize
        txtDesc.MaxLength = RsCurr.Fields("CURR_DESC").DefinedSize
        txtMinorCurrency.MaxLength = RsCurr.Fields("MINOR_CURR").DefinedSize
        txtRate.MaxLength = RsCurr.Fields("CON_FACTOR").Precision
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If Trim(txtCode.Text) = "" Then
            MsgInformation("Currency code is empty. Cannot Save")
            txtCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDesc.Text) = "" Then
            MsgInformation("Currency Description is empty. Cannot Save")
            txtDesc.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtMinorCurrency.Text) = "" Then
            MsgInformation("Minor Currency Description is empty. Cannot Save")
            txtMinorCurrency.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtRate.Text) = 0 Then
            MsgInformation("Conversion Rate Cann't be Zero. Cannot Save")
            txtRate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsCurr.EOF = True Then Exit Function
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

        Sqlstr = " SELECT CURR_CODE,CURR_DESC,CON_FACTOR,STATUS " & vbCrLf & " FROM FIN_CURRENCY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        Sqlstr = Sqlstr & vbCrLf & "ORDER BY CURR_DESC"

        MainClass.AssignDataInSprd8(Sqlstr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 12)
            .set_ColWidth(2, 12)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)
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
        mTitle = "Category Master"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\Currency.rpt"
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

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMinorCurrency_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMinorCurrency.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMinorCurrency_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMinorCurrency.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMinorCurrency.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDesc_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtDesc.Validating
        Dim Cancel As Boolean = EventArgs.Cancel
        'Dim mDesc As String = ""
        On Error GoTo ERR1
        Sqlstr = ""
        If Trim(txtDesc.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsCurr.EOF = False Then xCode = RsCurr.Fields("CURR_CODE").Value

        Sqlstr = "SELECT * FROM FIN_CURRENCY_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND CURR_DESC='" & MainClass.AllowSingleQuote(UCase((Trim(txtDesc.Text)))) & "'"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCurr, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCurr.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Currency Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                Sqlstr = ""
                Sqlstr = "SELECT * FROM FIN_CURRENCY_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND CURR_CODE='" & xCode & "'"

                MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCurr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub
End Class
