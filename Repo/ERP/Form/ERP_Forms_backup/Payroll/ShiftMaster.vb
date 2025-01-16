Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmShiftMaster
    Inherits System.Windows.Forms.Form
    Dim RsShiftMst As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection
    Dim Shw As Boolean
    Dim xCode As String
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Sub ViewGrid()

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
        MainClass.ButtonStatus(Me, XRIGHT, RsShiftMst, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        txtName.Text = ""
        txtCode.Text = ""

        txtShiftIN.Text = "00:00"
        txtShiftOUT.Text = "00:00"
        txtShiftBS.Text = "00:00"
        txtShiftBE.Text = "00:00"
        txtCode.Enabled = True
        chkRoundClock.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkDefaultShift.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkBreakApp.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboMajorShift.SelectedIndex = 0
        OptStatus(0).Checked = True


        MainClass.ButtonStatus(Me, XRIGHT, RsShiftMst, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub cboMajorShift_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMajorShift.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboMajorShift_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMajorShift.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRoundClock_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRoundClock.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkDefaultShift_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDefaultShift.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkBreakApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBreakApp.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsShiftMst, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            If txtCode.Enabled = True Then txtCode.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsShiftMst.EOF = False Then RsShiftMst.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsShiftMst.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsShiftMst.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        If MainClass.SearchGridMaster((txtName.Text), "PAY_SHIFT_MST", "SHIFT_DESC", "SHIFT_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtName.Text = AcName
            txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
            txtName.Focus()
        End If
    End Sub
    Private Sub SearchCode()
        If MainClass.SearchGridMaster((txtCode.Text), "PAY_SHIFT_MST", "SHIFT_CODE", "SHIFT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCode.Text = AcName
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
            If txtCode.Enabled = True Then txtCode.Focus()
        End If
    End Sub
    Private Sub frmShiftMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmShiftMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SqlStr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        SqlStr = " SELECT * from PAY_SHIFT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SHIFT_CODE='" & MainClass.AllowSingleQuote(UCase(SprdView.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShiftMst, ADODB.LockTypeEnum.adLockReadOnly)

        If RsShiftMst.EOF = False Then
            Show1()
            CmdView_Click(CmdView, New System.EventArgs())
        End If
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.DoubleClick
        Call SearchCode()
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchCode()
    End Sub

    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        If Trim(txtCode.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsShiftMst.EOF = False Then xCode = RsShiftMst.Fields("SHIFT_CODE").Value
        SqlStr = ""
        SqlStr = " SELECT * from  PAY_SHIFT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SHIFT_CODE='" & MainClass.AllowSingleQuote(Trim(txtCode.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShiftMst, ADODB.LockTypeEnum.adLockReadOnly)
        If RsShiftMst.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("New Entry, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from  PAY_SHIFT_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SHIFT_CODE='" & xCode & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShiftMst, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmShiftMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From PAY_SHIFT_MST Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShiftMst, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmShiftMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = VB6.TwipsToPixelsX(20)
        Me.Top = VB6.TwipsToPixelsY(20)
        'Me.Height = VB6.TwipsToPixelsY(3090)
        'Me.Width = VB6.TwipsToPixelsX(7065)

        cboMajorShift.Items.Clear()
        cboMajorShift.Items.Add("G")
        cboMajorShift.Items.Add("A")
        cboMajorShift.Items.Add("B")
        cboMajorShift.Items.Add("C")
        cboMajorShift.SelectedIndex = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmShiftMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsShiftMst = Nothing
        'frmDeptMaster = Nothing
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Shw = True
        If Not RsShiftMst.EOF Then
            txtName.Text = IIf(IsDbNull(RsShiftMst.Fields("SHIFT_DESC").Value), "", RsShiftMst.Fields("SHIFT_DESC").Value)
            txtCode.Text = IIf(IsDbNull(RsShiftMst.Fields("SHIFT_CODE").Value), "", RsShiftMst.Fields("SHIFT_CODE").Value)

            txtShiftIN.Text = VB6.Format(IIf(IsDbNull(RsShiftMst.Fields("FROM_TIME").Value), "00:00", RsShiftMst.Fields("FROM_TIME").Value), "hh:mm")
            txtShiftOUT.Text = VB6.Format(IIf(IsDbNull(RsShiftMst.Fields("TO_TIME").Value), "00:00", RsShiftMst.Fields("TO_TIME").Value), "hh:mm")
            txtShiftBS.Text = VB6.Format(IIf(IsDbNull(RsShiftMst.Fields("BS_TIME").Value), "00:00", RsShiftMst.Fields("BS_TIME").Value), "hh:mm")
            txtShiftBE.Text = VB6.Format(IIf(IsDbNull(RsShiftMst.Fields("BE_TIME").Value), "00:00", RsShiftMst.Fields("BE_TIME").Value), "hh:mm")
            chkRoundClock.CheckState = IIf(RsShiftMst.Fields("ROUND_CLOCK").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkDefaultShift.CheckState = IIf(RsShiftMst.Fields("DEFAULT_SHIFT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkBreakApp.CheckState = IIf(RsShiftMst.Fields("BREAK_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            cboMajorShift.Text = IIf(IsDbNull(RsShiftMst.Fields("MAJOR_SHIFT").Value), "", RsShiftMst.Fields("MAJOR_SHIFT").Value)

            If RsShiftMst.Fields("STATUS").Value = "O" Then
                OptStatus(0).Checked = True
            Else
                OptStatus(1).Checked = True
            End If

            txtCode.Enabled = False
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False
        If RsShiftMst.EOF = False Then
            xCode = RsShiftMst.Fields("SHIFT_CODE").Value
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsShiftMst, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mRoundClock As String
        Dim mDefaultShift As String
        Dim mBreakApp As String
        Dim mStatus As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mRoundClock = IIf(chkRoundClock.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mBreakApp = IIf(chkBreakApp.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mDefaultShift = IIf(chkDefaultShift.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        ''chkDefaultShift
        mStatus = IIf(OptStatus(0).Checked = True, "O", "C")


        SqlStr = ""
        If ADDMode = True Then
            SqlStr = " INSERT INTO PAY_SHIFT_MST ( " & vbCrLf _
                & " COMPANY_CODE, SHIFT_CODE, SHIFT_DESC, " & vbCrLf _
                & " FROM_TIME, TO_TIME, " & vbCrLf _
                & " BS_TIME, BE_TIME, " & vbCrLf _
                & " ADDUSER, ADDDATE," & vbCrLf _
                & " MODUSER, MODDATE, UPDATE_FROM, ROUND_CLOCK,MAJOR_SHIFT,BREAK_APP,STATUS,DEFAULT_SHIFT" & vbCrLf _
                & " ) VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote((txtCode.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtName.Text)) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtShiftIN.Text, "hh:mm") & "','HH24:MI'), TO_DATE('" & VB6.Format(txtShiftOUT.Text, "hh:mm") & "','HH24:MI')," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtShiftBS.Text, "hh:mm") & "','HH24:MI'), TO_DATE('" & VB6.Format(txtShiftBE.Text, "hh:mm") & "','HH24:MI')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','H','" & mRoundClock & "','" & Trim(cboMajorShift.Text) & "','" & mBreakApp & "','" & mStatus & "','" & mDefaultShift & "')"
        Else
            SqlStr = " UPDATE PAY_SHIFT_MST SET " & vbCrLf _
                & " SHIFT_DESC='" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf _
                & " FROM_TIME=TO_DATE('" & VB6.Format(txtShiftIN.Text, "hh:mm") & "','HH24:MI'), " & vbCrLf _
                & " TO_TIME=TO_DATE('" & VB6.Format(txtShiftOUT.Text, "hh:mm") & "','HH24:MI')," & vbCrLf _
                & " BS_TIME=TO_DATE('" & VB6.Format(txtShiftBS.Text, "hh:mm") & "','HH24:MI')," & vbCrLf _
                & " BE_TIME=TO_DATE('" & VB6.Format(txtShiftBE.Text, "hh:mm") & "','HH24:MI')," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "',DEFAULT_SHIFT='" & mDefaultShift & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), STATUS ='" & mStatus & "'," & vbCrLf _
                & " UPDATE_FROM='H',ROUND_CLOCK='" & mRoundClock & "',MAJOR_SHIFT='" & Trim(cboMajorShift.Text) & "', BREAK_APP='" & mBreakApp & "'" & vbCrLf _
                & " WHERE COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SHIFT_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"
        End If

UpdatePart:
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        RsShiftMst.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        SqlStr = ""
        FieldsVarification = True
        If Trim(txtName.Text) = "" Then
            MsgInformation("Name is empty. Cannot Save")
            txtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCode.Text) = "" Then
            MsgInformation("Code is empty. Cannot Save")
            txtCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtShiftIN.Text) = "00:00" Then
            MsgInformation("Shift IN Time is empty. Cannot Save")
            txtShiftIN.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtShiftOUT.Text) = "00:00" Then
            MsgInformation("Shift OUT Time is empty. Cannot Save")
            txtShiftOUT.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And (RsShiftMst.EOF = 0 Or RsShiftMst.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub settextlength()
        On Error GoTo ERR1



        txtName.Maxlength = RsShiftMst.Fields("SHIFT_DESC").DefinedSize
        txtCode.Maxlength = RsShiftMst.Fields("SHIFT_CODE").DefinedSize


        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsShiftMst.EOF = False Then xCode = RsShiftMst.Fields("SHIFT_CODE").Value
        SqlStr = ""
        SqlStr = " SELECT * from  PAY_SHIFT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SHIFT_DESC='" & MainClass.AllowSingleQuote(Trim(txtName.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShiftMst, ADODB.LockTypeEnum.adLockReadOnly)
        If RsShiftMst.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("New Entry, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from  PAY_SHIFT_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SHIFT_CODE='" & xCode & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShiftMst, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)


        SqlStr = "SELECT SHIFT_CODE AS SHIFT, SHIFT_DESC AS DESCRIPTION,MAJOR_SHIFT, DECODE(STATUS,'O','OPEN','CLOSED') STATUS," & vbCrLf _
            & " TO_CHAR(FROM_TIME,'HH24:MI') AS FROM_TIME,  TO_CHAR(TO_TIME,'HH24:MI') AS TO_TIME," & vbCrLf _
            & " TO_CHAR(BS_TIME,'HH24:MI') AS BS_TIME,  TO_CHAR(BE_TIME,'HH24:MI') AS BE_TIME" & vbCrLf _
            & " FROM PAY_SHIFT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY SHIFT_CODE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 6)
            .set_ColWidth(2, 13)
            .set_ColWidth(3, 7)
            .set_ColWidth(4, 7)
            .set_ColWidth(5, 7)
            .set_ColWidth(6, 7)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""
        If Trim(txtCode.Text) = "" Then Delete1 = False : Exit Function

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "PAY_SHIFT_MST", (txtName.Text), RsShiftMst) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "PAY_SHIFT_MST", "SHIFT_CODE", (txtCode.Text)) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM PAY_SHIFT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SHIFT_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsShiftMst.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsShiftMst.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "Department Listing"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ShiftMst.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtStrength_KeyPress(ByRef KeyAscii As Short)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
    End Sub

    Private Sub txtShiftBE_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShiftBE.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtShiftBS_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShiftBS.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtShiftIN_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShiftIN.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtShiftOUT_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShiftOUT.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
End Class
