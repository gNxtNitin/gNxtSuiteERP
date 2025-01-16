Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProcessMaster
    Inherits System.Windows.Forms.Form
    Dim RsProcess As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim Shw As Boolean
    Dim FormActive As Boolean
    Dim xProcessNo As String
    Dim SqlStr As String = ""
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh
            SprdView.Refresh()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsProcess, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        txtProcessNo.Text = ""
        txtDescription.Text = ""
        txtCostCenter.Text = ""
        lblCostCenter.Text = ""
        txtDept.Text = ""
        lblDept.Text = ""
        txtOprStd.Text = ""
        cboOprUnit.SelectedIndex = 0
        txtOprRate.Text = ""
        Call MakeEnableDeField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsProcess, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub MakeEnableDeField(ByRef mMode As Boolean)
        txtProcessNo.Enabled = mMode
        txtCostCenter.Enabled = mMode
        CmdSearchCC.Enabled = mMode
        txtDept.Enabled = mMode
        CmdSearchDept.Enabled = mMode
    End Sub

    Private Sub cboOprUnit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOprUnit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboOprUnit_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOprUnit.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsProcess, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        Call ShowReport(Crystal.DestinationConstants.crptToWindow)
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchDept.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDept.Text = AcName
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
    End Sub

    Private Sub cmdSearchProNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProNo.Click

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "
        If Trim(txtDept.Text) <> "" Then
            SqlStr = SqlStr & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' "
        End If
        If MainClass.SearchGridMaster(txtProcessNo.Text, "PRD_OPR_MST", "OPR_CODE", "OPR_DESC", "DEPT_CODE", , SqlStr) = True Then
            txtProcessNo.Text = AcName
            txtDescription.Text = AcName1
            txtProcessNo_Validating(txtProcessNo, New System.ComponentModel.CancelEventArgs((False)))
        End If
    End Sub

    Private Sub CmdSearchCC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchCC.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "FIN_CCENTER_HDR", "CC_DESC", "CC_CODE", , , SqlStr) = True Then
            txtCostCenter.Text = AcName1
            lblCostCenter.Text = AcName
            If txtCostCenter.Enabled = True Then txtCostCenter.Focus()
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        Call ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtProcessNo.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsProcess.EOF = False Then RsProcess.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        ''Resume
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtProcessNo.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If RsProcess.EOF Then Exit Sub
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
            If Delete1 = False Then GoTo DelErrPart
            If RsProcess.EOF = True Then
                Clear1()
            Else
                Show1()
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "PRD_OPR_MST", (txtProcessNo.Text), RsProcess) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "PRD_OPR_MST", "OPR_CODE", (txtProcessNo.Text)) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM PRD_OPR_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND OPR_CODE='" & MainClass.AllowSingleQuote(txtProcessNo.Text) & "'"
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsProcess.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsProcess.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This MACHINE NO.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function

    Private Sub frmProcessMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmProcessMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        txtProcessNo.Text = SprdView.Text
        txtProcessNo_Validating(txtProcessNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub frmProcessMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        SqlStr = " Select * From PRD_OPR_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcess, ADODB.LockTypeEnum.adLockReadOnly)
        Call SetTextLengths()
        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '   Resume
    End Sub
    Private Sub frmProcessMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        cboOprUnit.Items.Clear()
        cboOprUnit.Items.Add("Nos")
        cboOprUnit.Items.Add("Hour")
        cboOprUnit.Items.Add("KiloGram")
        cboOprUnit.Items.Add("CFM")
        cboOprUnit.Items.Add("LTR")
        cboOprUnit.Items.Add("1-KWH")

        cboOprUnit.SelectedIndex = 0


        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(3780)
        'Me.Width = VB6.TwipsToPixelsX(8340)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmProcessMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False

        RsProcess.Close()
        RsProcess = Nothing
        Me.Hide()
        Me.Close()
        ''PvtDBCn.Close
        ''Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Shw = True
        If Not RsProcess.EOF Then
            txtProcessNo.Text = IIf(IsDbNull(RsProcess.Fields("OPR_CODE").Value), "", RsProcess.Fields("OPR_CODE").Value)
            txtDescription.Text = IIf(IsDbNull(RsProcess.Fields("OPR_DESC").Value), "", RsProcess.Fields("OPR_DESC").Value)
            txtCostCenter.Text = IIf(IsDbNull(RsProcess.Fields("COST_CENTER_CODE").Value), "", RsProcess.Fields("COST_CENTER_CODE").Value)
            txtCostCenter_Validating(txtCostCenter, New System.ComponentModel.CancelEventArgs(False))
            txtDept.Text = IIf(IsDbNull(RsProcess.Fields("DEPT_CODE").Value), "", RsProcess.Fields("DEPT_CODE").Value)
            txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
            txtOprStd.Text = IIf(IsDbNull(RsProcess.Fields("INSP_STD_NO").Value), "", RsProcess.Fields("INSP_STD_NO").Value)
            If RsProcess.Fields("OPR_UNIT").Value = "H" Then
                cboOprUnit.Text = "Hour"
            ElseIf RsProcess.Fields("OPR_UNIT").Value = "K" Then
                cboOprUnit.Text = "KiloGram"
            ElseIf RsProcess.Fields("OPR_UNIT").Value = "N" Then
                cboOprUnit.Text = "Nos"
            ElseIf RsProcess.Fields("OPR_UNIT").Value = "C" Then
                cboOprUnit.Text = "CFM"
            ElseIf RsProcess.Fields("OPR_UNIT").Value = "L" Then
                cboOprUnit.Text = "LTR"
            ElseIf RsProcess.Fields("OPR_UNIT").Value = "1" Then
                cboOprUnit.Text = "1-KWH"
            End If


            txtOprRate.Text = CStr(Val(IIf(IsDbNull(RsProcess.Fields("OPR_RATE_HRS").Value), "", RsProcess.Fields("OPR_RATE_HRS").Value)))
            Call MakeEnableDeField(False)
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsProcess, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            txtProcessNo_Validating(txtProcessNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mOPRUnit As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mOPRUnit = VB.Left(cboOprUnit.Text, 1)

        SqlStr = ""

        If ADDMode = True Then
            SqlStr = " INSERT INTO PRD_OPR_MST ( " & vbCrLf _
                    & " COMPANY_CODE, OPR_CODE,OPR_DESC,COST_CENTER_CODE, " & vbCrLf _
                    & " DEPT_CODE,INSP_STD_NO,OPR_RATE_HRS,OPR_UNIT," & vbCrLf _
                    & " ADDUSER,ADDDATE,MODUSER,MODDATE) " & vbCrLf _
                    & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtProcessNo.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtDescription.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtCostCenter.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtOprStd.Text) & "'," & vbCrLf _
                    & " " & Val(txtOprRate.Text) & ", " & vbCrLf _
                    & " '" & mOPRUnit & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','') "
        Else
            SqlStr = " UPDATE PRD_OPR_MST SET " & vbCrLf _
                     & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                     & " OPR_CODE='" & MainClass.AllowSingleQuote(txtProcessNo.Text) & "', " & vbCrLf _
                     & " OPR_DESC='" & MainClass.AllowSingleQuote(txtDescription.Text) & "', " & vbCrLf _
                     & " COST_CENTER_CODE='" & MainClass.AllowSingleQuote(txtCostCenter.Text) & "', " & vbCrLf _
                     & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                     & " INSP_STD_NO='" & MainClass.AllowSingleQuote(txtOprStd.Text) & "', " & vbCrLf _
                     & " OPR_RATE_HRS=" & Val(txtOprRate.Text) & ", " & vbCrLf _
                     & " OPR_UNIT='" & mOPRUnit & "', " & vbCrLf _
                     & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                     & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                     & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                     & " AND OPR_CODE='" & MainClass.AllowSingleQuote(txtProcessNo.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtProcessNo.Text) = "" Then
            MsgInformation("Code is empty, So unable to Save")
            txtProcessNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDescription.Text) = "" Then
            MsgInformation("Operation Description is empty. Cannot Save")
            txtDescription.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtCostCenter.Text) = "" Then
            MsgInformation("Cost Center Code is empty. Cannot Save")
            txtCostCenter.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDept.Text) = "" Then
            MsgInformation("Department is empty. Cannot Save")
            txtDept.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtOprStd.Text) = "" Then
            MsgInformation("Operation Std. empty. Cannot Save")
            txtOprStd.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(cboOprUnit.Text) = "" Then
            MsgInformation("Operation Unit empty. Cannot Save")
            txtOprStd.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Machine Details or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsProcess.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = " SELECT OPR_CODE,OPR_DESC,COST_CENTER_CODE,DEPT_CODE,INSP_STD_NO " & vbCrLf & " FROM PRD_OPR_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY OPR_CODE"
        Call MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()

    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "PROCESS MASTER"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ProcMaster.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub


    Private Sub txtDescription_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDescription.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDescription_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDescription.DoubleClick

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "
        If Trim(txtDept.Text) <> "" Then
            SqlStr = SqlStr & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' "
        End If
        If MainClass.SearchGridMaster(txtDescription.Text, "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", "DEPT_CODE", , SqlStr) = True Then
            txtProcessNo.Text = AcName1
            txtDescription.Text = AcName
            txtProcessNo_Validating(txtProcessNo, New System.ComponentModel.CancelEventArgs((False)))
        End If
    End Sub

    Private Sub txtDescription_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDescription.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDescription.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Deparment Does Not Exist In Master.")
            Cancel = True
        Else
            lblDept.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDescription_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtDescription_DoubleClick(txtDescription, New System.EventArgs())
    End Sub

    Private Sub txtOprRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOprRate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOprRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOprRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOprStd_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOprStd.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtOprStd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOprStd.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOprStd.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtProcessNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcessNo.DoubleClick
        Call cmdSearchProNo_Click(cmdSearchProNo, New System.EventArgs())
    End Sub

    Private Sub txtProcessNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProcessNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchProNo_Click(cmdSearchProNo, New System.EventArgs())
    End Sub

    Private Sub txtProcessNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProcessNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1

        If Trim(txtProcessNo.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsProcess.EOF = False Then xProcessNo = RsProcess.Fields("OPR_CODE").Value
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
                & " FROM PRD_OPR_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND OPR_CODE='" & MainClass.AllowSingleQuote(txtProcessNo.Text) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcess, ADODB.LockTypeEnum.adLockReadOnly)

        If RsProcess.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            xProcessNo = RsProcess.Fields("OPR_CODE").Value
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Such Code Does Not Exist" & vbCrLf & "Click Add To Add for New.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * " & vbCrLf & " FROM PRD_OPR_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND OPR_CODE='" & MainClass.AllowSingleQuote(xProcessNo) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcess, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtProcessNo.Maxlength = RsProcess.Fields("OPR_CODE").DefinedSize
        txtDescription.Maxlength = RsProcess.Fields("OPR_DESC").DefinedSize
        txtCostCenter.Maxlength = RsProcess.Fields("COST_CENTER_CODE").DefinedSize
        txtDept.Maxlength = RsProcess.Fields("DEPT_CODE").DefinedSize
        txtOprStd.Maxlength = RsProcess.Fields("INSP_STD_NO").DefinedSize
        txtOprRate.Maxlength = RsProcess.Fields("OPR_RATE_HRS").Precision - 4
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 500 * 3)
            .set_ColWidth(2, 500 * 5)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 5)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub txtProcessNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcessNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtProcessNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProcessNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProcessNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCostCenter_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCostCenter.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCostCenter_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCostCenter.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCostCenter.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCostCenter_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCostCenter.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchCC_Click(CmdSearchCC, New System.EventArgs())
    End Sub

    Private Sub txtCostCenter_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCostCenter.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtCostCenter.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtCostCenter.Text, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Cost Center Does Not Exist In Master.")
            Cancel = True
        Else
            lblCostCenter.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
