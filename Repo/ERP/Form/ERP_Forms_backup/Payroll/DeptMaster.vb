Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDeptMaster
    Inherits System.Windows.Forms.Form
    Dim RsDept As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection
    Dim Shw As Boolean
    Dim xCode As String
    Dim Sqlstr As String
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
        MainClass.ButtonStatus(Me, XRIGHT, RsDept, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        txtName.Text = ""
        txtCode.Text = ""
        txtStrength.Text = ""
        txtCost.Text = ""
        txtCode.Enabled = True
        chkSubStore.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboIndentification.SelectedIndex = -1
        MainClass.ButtonStatus(Me, XRIGHT, RsDept, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cboIndentification_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboIndentification.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboIndentification_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboIndentification.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Sub chkSubStore_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSubStore.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsDept, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
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

    Private Sub cmdSearchCC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCC.Click
        On Error GoTo ErrPart
        Dim Sqlstr As String
        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster((txtCost.Text), "FIN_CCENTER_HDR", "CC_CODE", "CC_DESC",  ,  , Sqlstr) = True Then
            txtCost.Text = AcName
            lblCostctr.Text = AcName1
            If txtCost.Enabled = True Then txtCost.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
            If RsDept.EOF = False Then RsDept.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsDept.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1() = False Then GoTo DelErrPart
                If RsDept.EOF = True Then
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
        If MainClass.SearchGridMaster((txtName.Text), "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE",  ,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtName.Text = AcName
            txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
            txtName.Focus()
        End If
    End Sub
    Private Sub SearchCode()
        If MainClass.SearchGridMaster((txtCode.Text), "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC",  ,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCode.Text = AcName
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
            If txtCode.Enabled = True Then txtCode.Focus()
        End If
    End Sub
    Private Sub frmDeptMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmDeptMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        Sqlstr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        Sqlstr = " SELECT * from PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(SprdView.Text)) & "'"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)

        If RsDept.EOF = False Then
            Show1()
            CmdView_Click(CmdView, New System.EventArgs())
        End If
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
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
        If MODIFYMode = True And RsDept.EOF = False Then xCode = RsDept.Fields("DEPT_CODE").Value
        Sqlstr = ""
        Sqlstr = " SELECT * from  PAY_DEPT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(txtCode.Text)) & "' "

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDept.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("New Entry, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                Sqlstr = "Select * from  PAY_DEPT_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE='" & xCode & "' "
                MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCost_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.DoubleClick
        Call cmdSearchCC_Click(cmdSearchCC, New System.EventArgs())
    End Sub

    Private Sub txtCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCost.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCost_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCost.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub txtCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtCost.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtCost.Text), "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblCostctr.Text = MasterNo
        Else
            MsgInformation("Invalid CostC Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtStrength_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStrength.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmDeptMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Sqlstr = ""
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From PAY_DEPT_MST Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()

        cboIndentification.Items.Clear()

        'If RsCompany.Fields("COMPANY_CODE").Value = 10 Then
        cboIndentification.Items.Add("1. General")
        cboIndentification.Items.Add("2. Store")
        cboIndentification.Items.Add("3. Out Source Operation")

        '    cboIndentification.Items.Add("3. TVSM Weld Shop")
        '    cboIndentification.Items.Add("4. Paint Shop")
        '    cboIndentification.Items.Add("5. Powder Coating Shop")
        '    cboIndentification.Items.Add("6. Press Shop")
        '    cboIndentification.Items.Add("7. Assembly Shop")
        '    cboIndentification.Items.Add("8. Electro Plating")
        '    cboIndentification.Items.Add("9. Zinc Plating")
        '    cboIndentification.Items.Add("A. RE Weld Shop")
        '    cboIndentification.Items.Add("B. H/E Weld Shop")
        '    cboIndentification.Items.Add("C. Chain Case Shop")
        '    cboIndentification.Items.Add("D. New Development")
        '    cboIndentification.Items.Add("E. BMW Shop")
        '    cboIndentification.Items.Add("F. Electro Polishing")
        'ElseIf RsCompany.Fields("COMPANY_CODE").Value = 3 Then
        '    cboIndentification.Items.Add("1. General")
        '    cboIndentification.Items.Add("2. Store")
        '    cboIndentification.Items.Add("3. TVSM Weld Shop")
        '    cboIndentification.Items.Add("4. Paint Shop")
        '    cboIndentification.Items.Add("5. Powder Coating Shop")
        '    cboIndentification.Items.Add("6. Press Shop")
        '    cboIndentification.Items.Add("7. Assembly Shop")
        '    cboIndentification.Items.Add("8. Electro Plating")
        '    cboIndentification.Items.Add("9. Zinc Plating")
        '    cboIndentification.Items.Add("A. RE Frame Shop")
        '    cboIndentification.Items.Add("B. RE Weld Shop")
        '    cboIndentification.Items.Add("C. Chain Case Shop")
        '    cboIndentification.Items.Add("D. New Development")
        '    cboIndentification.Items.Add("E. BMW Shop")
        '    cboIndentification.Items.Add("F. Electro Polishing")
        'ElseIf RsCompany.Fields("COMPANY_CODE").Value = 32 Then
        '    cboIndentification.Items.Add("1. General")
        '    cboIndentification.Items.Add("2. Store")
        '    cboIndentification.Items.Add("3. HMCL Weld Shop")
        '    cboIndentification.Items.Add("4. Paint Shop")
        '    cboIndentification.Items.Add("5. Powder Coating Shop")
        '    cboIndentification.Items.Add("6. Press Shop")
        '    cboIndentification.Items.Add("7. Assembly Shop")
        '    cboIndentification.Items.Add("8. Electro Plating")
        '    cboIndentification.Items.Add("9. Zinc Plating")
        '    cboIndentification.Items.Add("A. H/E Press Shop")
        '    cboIndentification.Items.Add("B. H/E Weld Shop")
        '    cboIndentification.Items.Add("C. Shot Blasting Shop")
        '    cboIndentification.Items.Add("D. New Development")
        '    cboIndentification.Items.Add("E. BMW Shop")
        '    cboIndentification.Items.Add("F. Electro Polishing")
        'Else
        '    cboIndentification.Items.Add("1. General")
        '    cboIndentification.Items.Add("2. Store")
        '    cboIndentification.Items.Add("3. Weld Shop")
        '    cboIndentification.Items.Add("4. Paint Shop")
        '    cboIndentification.Items.Add("5. Powder Coating Shop")
        '    cboIndentification.Items.Add("6. Press Shop")
        '    cboIndentification.Items.Add("7. Assembly Shop")
        '    cboIndentification.Items.Add("8. Electro Plating")
        '    cboIndentification.Items.Add("9. Zinc Plating")
        '    cboIndentification.Items.Add("A. Frame Shop")
        '    cboIndentification.Items.Add("B. Handle Shop")
        '    cboIndentification.Items.Add("C. Chain Case Shop")
        '    cboIndentification.Items.Add("D. New Development")
        '    cboIndentification.Items.Add("E. BMW Shop")
        '    cboIndentification.Items.Add("F. Electro Polishing")
        'End If

        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmDeptMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(3945)
        Me.Width = VB6.TwipsToPixelsX(7065)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmDeptMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsDept = Nothing
        'Me = Nothing
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mIsSubStore As String
        Dim mDDept As String
        Dim mIdentification As Integer

        Shw = True
        If Not RsDept.EOF Then
            txtName.Text = IIf(IsDBNull(RsDept.Fields("DEPT_DESC").Value), "", RsDept.Fields("DEPT_DESC").Value)
            txtCode.Text = IIf(IsDBNull(RsDept.Fields("DEPT_CODE").Value), "", RsDept.Fields("DEPT_CODE").Value)
            txtStrength.Text = CStr(Val(IIf(IsDBNull(RsDept.Fields("DEPT_STRENGTH").Value), 0, RsDept.Fields("DEPT_STRENGTH").Value)))
            txtCost.Text = IIf(IsDBNull(RsDept.Fields("CCCODE").Value), "", RsDept.Fields("CCCODE").Value)
            mIsSubStore = IIf(IsDBNull(RsDept.Fields("ISSUBSTORE").Value), "N", RsDept.Fields("ISSUBSTORE").Value)
            mDDept = IIf(IsDBNull(RsDept.Fields("DEPT_TYPE").Value), "1", RsDept.Fields("DEPT_TYPE").Value)

            If Val(mDDept) > 0 Then
                mIdentification = Val(RsDept.Fields("DEPT_TYPE").Value)
            Else
                If mDDept = "N" Then
                    mIdentification = 1
                    'ElseIf mDDept = "A" Then
                    '    mIdentification = 10
                    'ElseIf mDDept = "B" Then
                    '    mIdentification = 11
                    'ElseIf mDDept = "C" Then
                    '    mIdentification = 12
                    'ElseIf mDDept = "D" Then
                    '    mIdentification = 13
                    'ElseIf mDDept = "E" Then
                    '    mIdentification = 14
                    'ElseIf mDDept = "F" Then
                    '    mIdentification = 15
                End If
            End If


            cboIndentification.SelectedIndex = mIdentification - 1

            chkSubStore.CheckState = IIf(mIsSubStore = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            txtCode.Enabled = False
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False
        If RsDept.EOF = False Then
            xCode = RsDept.Fields("DEPT_CODE").Value
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsDept, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
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
        Dim mIsSubStore As String
        Dim mDDept As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mIsSubStore = IIf(chkSubStore.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mDDept = VB.Left(cboIndentification.Text, 1) ''IIf(chkDDept.Value = vbChecked, "D", "N")

        Sqlstr = ""

        If ADDMode = True Then
            Sqlstr = " INSERT INTO PAY_DEPT_MST ( " & vbCrLf & " COMPANY_CODE,DEPT_CODE,DEPT_DESC,DEPT_STRENGTH," & vbCrLf & " CCCODE,ISSUBSTORE,DEPT_TYPE) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote((txtCode.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtName.Text)) & "', " & vbCrLf & " " & Val(txtStrength.Text) & ",'" & MainClass.AllowSingleQuote((txtCost.Text)) & "','" & mIsSubStore & "'," & vbCrLf & " '" & mDDept & "')"
        Else
            Sqlstr = " UPDATE PAY_DEPT_MST SET " & vbCrLf & " DEPT_DESC='" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf & " DEPT_STRENGTH=" & Val(txtStrength.Text) & ", " & vbCrLf & " CCCODE='" & MainClass.AllowSingleQuote(txtCost.Text) & "', " & vbCrLf & " ISSUBSTORE='" & mIsSubStore & "', " & vbCrLf & " DEPT_TYPE='" & mDDept & "'" & vbCrLf & " WHERE COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"
        End If

UpdatePart:
        PubDBCn.Execute(Sqlstr)
        PubDBCn.CommitTrans()
        RsDept.Requery()
        '    Call CreateLogFile(RsDept, "PAY_DEPT_MST", txtCode.Text)
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        RsDept.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        cmdsearch_Click(cmdSearch, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        Sqlstr = ""
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

        If Val(txtStrength.Text) = 0 Then
            MsgInformation("Strength is empty. Cannot Save")
            txtStrength.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboIndentification.SelectedIndex = -1 Then
            MsgInformation("Please Select Department Indentification.")
            cboIndentification.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And (RsDept.EOF = 0 Or RsDept.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub settextlength()
        On Error GoTo ERR1



        txtName.MaxLength = RsDept.Fields("DEPT_DESC").DefinedSize
        txtCode.MaxLength = RsDept.Fields("DEPT_CODE").DefinedSize
        txtStrength.MaxLength = RsDept.Fields("DEPT_STRENGTH").Precision
        txtCost.MaxLength = RsDept.Fields("CCCODE").DefinedSize

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsDept.EOF = False Then xCode = RsDept.Fields("DEPT_CODE").Value
        Sqlstr = ""
        Sqlstr = " SELECT * from  PAY_DEPT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DEPT_DESC='" & MainClass.AllowSingleQuote(Trim(txtName.Text)) & "' "

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDept.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("New Entry, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                Sqlstr = "Select * from  PAY_DEPT_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE='" & xCode & "' "
                MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)


        Sqlstr = "SELECT DEPT_CODE,DEPT_DESC,CCCODE,DEPT_STRENGTH, " & vbCrLf & " DECODE(ISSUBSTORE,'Y','Yes','No') AS ISSUBSTORE, " & vbCrLf & " CASE WHEN DEPT_TYPE='1' THEN 'General' " & vbCrLf & " WHEN DEPT_TYPE='2' THEN 'Store' " & vbCrLf _
            & " WHEN DEPT_TYPE='3' THEN 'Out Source' " & vbCrLf _
            & " WHEN DEPT_TYPE='4' THEN 'Paint Shop' " & vbCrLf _
            & " WHEN DEPT_TYPE='5' THEN 'Powder Coating Shop' " & vbCrLf & " WHEN DEPT_TYPE='6' THEN 'Press Shop' " & vbCrLf & " WHEN DEPT_TYPE='7' THEN 'Assembly Shop' " & vbCrLf & " WHEN DEPT_TYPE='D' THEN 'New Development' WHEN DEPT_TYPE='E' THEN 'BMW Shop' WHEN DEPT_TYPE='D' THEN 'Electro Polishing'  " & vbCrLf & " END AS IDENTIFICATION" & vbCrLf & " FROM PAY_DEPT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY DEPT_CODE"

        MainClass.AssignDataInSprd8(Sqlstr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 16)
            .set_ColWidth(3, 5)
            .set_ColWidth(4, 5)
            .set_ColWidth(5, 5)
            .set_ColWidth(6, 12)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        Sqlstr = ""
        If Trim(txtCode.Text) = "" Then Delete1 = False : Exit Function

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "PAY_DEPT_MST", (txtName.Text), RsDept) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "PAY_DEPT_MST", "DEPT_CODE", (txtCode.Text)) = False Then GoTo DeleteErr

        Sqlstr = " DELETE FROM PAY_DEPT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "'"
        PubDBCn.Execute(Sqlstr)

        PubDBCn.CommitTrans()
        RsDept.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsDept.Requery()
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
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\Department.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtStrength_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStrength.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
