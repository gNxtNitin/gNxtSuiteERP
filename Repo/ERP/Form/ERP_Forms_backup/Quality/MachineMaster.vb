Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel

Friend Class frmMachineMaster
    Inherits System.Windows.Forms.Form
    Dim RsMMST As ADODB.Recordset
    Dim RsMachMaint As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim PvtDBCn As ADODB.Connection
    Dim Shw As Boolean
    Dim FormActive As Boolean
    Dim xMachineno As String
    Dim SqlStr As String

    Private Const ConRowHeight As Short = 14

    Private Const ColCheckType As Short = 1
    Private Const ColFrequency As Short = 2
    Private Const ColCheckHours As Short = 3
    Private Const ColLastPM As Short = 4
    Private Const ColDuePM As Short = 5

    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsMMST, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()

        txtMachineNo.Text = ""
        txtInsDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtItemCode.Text = ""
        txtItemName.Text = ""
        txtMachineDesc.Text = ""
        txtSpec.Text = ""
        txtCapacity.Text = ""
        txtDept.Text = ""
        txtDeptName.Text = ""
        txtLocation.Text = ""
        txtAssetNo.Text = ""
        txtMake.Text = ""
        chkKeyMachine.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkMchbkDown.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtOperation.Text = ""
        txtOperationName.Text = ""
        txtPieces.Text = ""
        txtWorkingHrs.Text = ""
        txtUnit.Text = ""
        cboStatus.Text = "OPEN/ACTIVE"
        cboMaintType.Text = "Preventive"
        cboFuelType.Text = "Electricity"
        cboFuelConsOn.Text = "Hour Basis"

        cboRefType.SelectedIndex = -1
        txtRefNo.Text = ""
        txtRefDate.Text = ""
        txtUnitName.Text = ""

        txtFuelCons.Text = ""
        txtRemarks.Text = ""


        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        Call MakeEnableDeField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsMMST, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim i As Short

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColCheckType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachMaint.Fields("CHECK_TYPE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColCheckType, 15)

            .Col = ColFrequency
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeIntegerMax = CInt("99")
            .TypeIntegerMin = CInt("0")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColFrequency, 15)

            .Col = ColCheckHours
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeIntegerMax = CInt("99999")
            .TypeIntegerMin = CInt("0")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCheckHours, 15)

            .Col = ColLastPM
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColLastPM, 15)

            .Col = ColDuePM
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDuePM, 15)

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDuePM, ColDuePM)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub MakeEnableDeField(ByRef mMode As Boolean)
        '    txtMachineNo.Enabled = mMode
        '    TxtInsDate.Enabled = mMode
        '    cmdSearchMacNo.Enabled = mMode
        '    txtItemCode.Enabled = mMode
        '    cmdSearchItemCode.Enabled = mMode
        '    TxtItemName.Enabled = False
        '    TxtDept.Enabled = mMode
        '    CmdSearchDept.Enabled = mMode
        '    txtLocation.Enabled = False
        '    txtOperation.Enabled = mMode
        '    CmdSearchOp.Enabled = mMode
        '    TxtOperationName.Enabled = False
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboFuelConsOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFuelConsOn.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboFuelConsOn_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFuelConsOn.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboFuelType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFuelType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboFuelType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFuelType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboMaintType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMaintType.TextChanged
        cboMaintType_Validating(cboMaintType, New System.ComponentModel.CancelEventArgs(False))
    End Sub
    Private Sub cboRefType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRefType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboRefType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRefType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboMaintType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMaintType.SelectedIndexChanged
        cboMaintType_Validating(cboMaintType, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Sub cboMaintType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboMaintType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If cboMaintType.Text = "Preventive" Then
            If ADDMode = True Or MODIFYMode = True Then
                SprdMain.Enabled = True
            End If
            SetSprdHeading()
        ElseIf cboMaintType.Text = "Preductive" Then
            SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        ElseIf cboMaintType.Text = "Hour Basis" Then
            If ADDMode = True Or MODIFYMode = True Then
                SprdMain.Enabled = True
            End If
            SetSprdHeading()
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub ChkKeyMachine_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkKeyMachine.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub ChkMchbkDown_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMchbkDown.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsMMST, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
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

    Private Sub CmdSearchItemCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchItemCode.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemName.Text = AcName
            txtItemCode.Text = AcName1
            If txtItemCode.Enabled = True Then txtItemCode.Focus()
        End If
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchDept.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            txtDeptName.Text = AcName
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
    End Sub

    Private Sub cmdSearchMacNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMacNo.Click
        SqlStr = "(COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " OR TRANSFER_UNIT_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"
        If MainClass.SearchGridMaster("", "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", , , SqlStr) = True Then
            txtItemName.Text = AcName
            txtMachineNo.Text = AcName1
            txtMachineNo_Validating(txtMachineNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub CmdSearchOp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchOp.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , SqlStr) = True Then
            txtOperationName.Text = AcName
            txtOperation.Text = AcName1
            If txtOperation.Enabled = True Then txtOperation.Focus()
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
            txtMachineNo.Focus()
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsMMST.EOF = False Then RsMMST.MoveFirst()
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
        If txtMachineNo.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If RsMMST.EOF Then Exit Sub
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
            If Delete1() = False Then GoTo DelErrPart
            If RsMMST.EOF = True Then
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
        If InsertIntoDelAudit(PubDBCn, "MAN_MACHINE_MST", (txtMachineNo.Text), RsMMST) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "MAN_MACHINE_MST", "MACHINE_NO", (txtMachineNo.Text)) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM MAN_MACHINE_MAINT_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM MAN_MACHINE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsMMST.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsMMST.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This MACHINE NO.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function

    Private Sub frmMachineMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmMachineMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        txtMachineNo.Text = SprdView.Text
        txtMachineNo_Validating(txtMachineNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Public Sub frmMachineMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "Select * From Man_Machine_Mst Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMMST, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = " Select * From MAN_MACHINE_MAINT_TRN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachMaint, ADODB.LockTypeEnum.adLockReadOnly)

        Call SetTextLengths()
        Call AssignGrid(False)
        Call Clear1()
        Call SetSprdHeading()

        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '   Resume
    End Sub

    Private Sub SetSprdHeading()
        On Error GoTo ERR1
        Dim i As Short

        With SprdMain
            For i = ColCheckType To ColDuePM
                .Col = i
                .ColHidden = False
            Next
            If cboMaintType.Text = "Preventive" Then
                .Col = ColCheckHours
                .ColHidden = True
            ElseIf cboMaintType.Text = "Hour Basis" Then
                .Col = ColFrequency
                .ColHidden = True
                .Col = ColDuePM
                .ColHidden = True
            End If
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Public Sub frmMachineMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(10935)
        chkMchbkDown.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        cboStatus.Items.Clear()
        cboStatus.Items.Add("OPEN/ACTIVE")
        cboStatus.Items.Add("TRANSFER SALE")
        cboStatus.Items.Add("SCRAP SALE")
        cboStatus.Items.Add("CLOSE/INACTIVE")
        cboStatus.SelectedIndex = 0

        cboMaintType.Items.Clear()
        cboMaintType.Items.Add("Preventive")
        cboMaintType.Items.Add("Preductive")
        cboMaintType.Items.Add("Hour Basis")
        cboMaintType.SelectedIndex = 0

        cboRefType.Items.Clear()
        cboRefType.Items.Add("RGP")
        cboRefType.Items.Add("NRGP")
        cboRefType.Items.Add("SALE")
        cboRefType.SelectedIndex = 0

        cboFuelType.Items.Clear()
        cboFuelType.Items.Add("Electricity")
        cboFuelType.Items.Add("Diesel")
        cboFuelType.SelectedIndex = 0

        cboFuelConsOn.Items.Clear()
        cboFuelConsOn.Items.Add("Hour Basis")
        cboFuelConsOn.Items.Add("Unit Basis")
        cboFuelConsOn.SelectedIndex = 0

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmMachineMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsMMST.Close()
        RsMMST = Nothing
        RsMachMaint.Close()
        RsMachMaint = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String
        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColCheckType)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mActiveCol As Integer
        Dim mCheckType As String

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColLastPM Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColCheckType
                mCheckType = Trim(SprdMain.Text)
                SprdMain.Col = ColLastPM
                If mCheckType <> "" And SprdMain.MaxRows = SprdMain.ActiveRow Then
                    MainClass.AddBlankSprdRow(SprdMain, ColCheckType, ConRowHeight)
                End If
            End If
        End If
        eventArgs.keyCode = 9999
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim mFrequency As Short
        Dim mLastPM As String

        If eventArgs.newRow = -1 Then Exit Sub

        With SprdMain
            .Row = .ActiveRow
            Select Case eventArgs.col
                Case ColCheckType
                    .Col = ColCheckType
                    If Trim(.Text) = "" Then Exit Sub
                    If DuplicateCheck() = False Then
                        FormatSprdMain(-1)
                    End If
                Case ColFrequency
                    .Row = .ActiveRow
                    .Col = ColFrequency
                    mFrequency = Val(.Text)
                    .Col = ColLastPM
                    mLastPM = Trim(.Text)
                    If mFrequency <> 0 And mLastPM <> "" Then
                        If cboMaintType.Text = "Preventive" Then
                            .Col = ColDuePM
                            .Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, mFrequency, CDate(mLastPM)))
                        Else
                            .Col = ColDuePM
                            .Text = ""
                        End If
                    Else
                        .Col = ColDuePM
                        .Text = ""
                    End If
                Case ColLastPM
                    .Col = ColFrequency
                    mFrequency = Val(.Text)
                    .Col = ColLastPM
                    mLastPM = Trim(.Text)
                    If mFrequency <> 0 And mLastPM <> "" Then
                        If cboMaintType.Text = "Preventive" Then
                            .Col = ColDuePM
                            .Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, mFrequency, CDate(mLastPM)))
                        Else
                            .Col = ColDuePM
                            .Text = ""
                        End If
                    Else
                        .Col = ColDuePM
                        .Text = ""
                    End If
            End Select
        End With
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function DuplicateCheck() As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckCheckType As String
        Dim mCheckType As String

        DuplicateCheck = False

        With SprdMain
            .Row = .ActiveRow
            .Col = ColCheckType
            mCheckCheckType = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCheckType
                mCheckType = Trim(UCase(.Text))

                If (mCheckType = mCheckCheckType And mCheckCheckType <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateCheck = True
                    MsgInformation("Duplicate Check Type : " & mCheckCheckType)
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColCheckType)
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        Shw = True

        Clear1()

        If Not RsMMST.EOF Then
            txtMachineNo.Text = IIf(IsDBNull(RsMMST.Fields("MACHINE_NO").Value), "", RsMMST.Fields("MACHINE_NO").Value)
            txtInsDate.Text = IIf(IsDBNull(RsMMST.Fields("MACHINE_INST_DATE").Value), "", RsMMST.Fields("MACHINE_INST_DATE").Value)
            txtItemCode.Text = Trim(IIf(IsDBNull(RsMMST.Fields("MACHINE_ITEM_CODE").Value), "", RsMMST.Fields("MACHINE_ITEM_CODE").Value))
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                txtItemName.Text = MasterNo
            Else
                txtItemName.Text = ""
            End If
            txtMachineDesc.Text = IIf(IsDBNull(RsMMST.Fields("MACHINE_DESC").Value), "", RsMMST.Fields("MACHINE_DESC").Value)
            txtSpec.Text = IIf(IsDBNull(RsMMST.Fields("MACHINE_SPEC").Value), "", RsMMST.Fields("MACHINE_SPEC").Value)
            txtCapacity.Text = IIf(IsDBNull(RsMMST.Fields("Capacity").Value), "", RsMMST.Fields("Capacity").Value)
            txtDept.Text = IIf(IsDBNull(RsMMST.Fields("DEPT_CODE").Value), "", RsMMST.Fields("DEPT_CODE").Value)
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                txtDeptName.Text = MasterNo
            Else
                txtDeptName.Text = ""
            End If
            txtLocation.Text = IIf(IsDBNull(RsMMST.Fields("Location").Value), "", RsMMST.Fields("Location").Value)
            txtAssetNo.Text = IIf(IsDBNull(RsMMST.Fields("MACH_ASSET_NO").Value), "", RsMMST.Fields("MACH_ASSET_NO").Value)

            txtMake.Text = IIf(IsDBNull(RsMMST.Fields("MAKE").Value), "", RsMMST.Fields("MAKE").Value)
            txtOperation.Text = IIf(IsDBNull(RsMMST.Fields("OPR_CODE").Value), "", RsMMST.Fields("OPR_CODE").Value)
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If MainClass.ValidateWithMasterTable(txtOperation.Text, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                txtOperationName.Text = MasterNo
            Else
                txtOperationName.Text = ""
            End If
            txtPieces.Text = IIf(IsDBNull(RsMMST.Fields("NO_OF_PIECES").Value), "", VB6.Format(RsMMST.Fields("NO_OF_PIECES").Value, "0.000"))
            txtWorkingHrs.Text = IIf(IsDBNull(RsMMST.Fields("NO_OF_WORKHRS").Value), "", VB6.Format(RsMMST.Fields("NO_OF_WORKHRS").Value, "0.00"))
            txtUnit.Text = IIf(IsDBNull(RsMMST.Fields("NO_OF_UNITS").Value), "", VB6.Format(RsMMST.Fields("NO_OF_UNITS").Value, "0.00"))
            chkKeyMachine.CheckState = IIf(RsMMST.Fields("KEY_MACHINE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkMchbkDown.CheckState = IIf(RsMMST.Fields("MACHINE_UB").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            If RsMMST.Fields("Status").Value = "O" Then
                cboStatus.Text = "OPEN/ACTIVE"
            ElseIf RsMMST.Fields("Status").Value = "T" Then
                cboStatus.Text = "TRANSFER SALE"
            ElseIf RsMMST.Fields("Status").Value = "S" Then
                cboStatus.Text = "SCRAP SALE"
            ElseIf RsMMST.Fields("Status").Value = "C" Then
                cboStatus.Text = "CLOSE/INACTIVE"
            End If
            If RsMMST.Fields("MAINT_TYPE").Value = "P" Then
                cboMaintType.Text = "Preventive"
            ElseIf RsMMST.Fields("MAINT_TYPE").Value = "D" Then
                cboMaintType.Text = "Preductive"
            ElseIf RsMMST.Fields("MAINT_TYPE").Value = "H" Then
                cboMaintType.Text = "Hour Basis"
            End If




            If RsMMST.Fields("FUEL_TYPE").Value = "E" Then
                cboFuelType.Text = "Electricity"
            ElseIf RsMMST.Fields("FUEL_TYPE").Value = "D" Then
                cboFuelType.Text = "Diesel"
            End If
            If RsMMST.Fields("FUEL_CONS_ON").Value = "H" Then
                cboFuelConsOn.Text = "Hour Basis"
            ElseIf RsMMST.Fields("FUEL_CONS_ON").Value = "U" Then
                cboFuelConsOn.Text = "Unit Basis"
            End If
            txtFuelCons.Text = IIf(IsDBNull(RsMMST.Fields("FUEL_CONS").Value), "", VB6.Format(RsMMST.Fields("FUEL_CONS").Value, "0.00"))
            txtRemarks.Text = IIf(IsDBNull(RsMMST.Fields("REMARKS").Value), "", RsMMST.Fields("REMARKS").Value)

            mDivisionCode = IIf(IsDBNull(RsMMST.Fields("DIV_CODE").Value), -1, RsMMST.Fields("DIV_CODE").Value)

            If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionDesc = Trim(MasterNo)
                cboDivision.Text = mDivisionDesc
            End If
            cboDivision.Enabled = True

            If IsDBNull(RsMMST.Fields("TRANSFER_REF_TYPE").Value) Then
                cboRefType.Text = ""
            ElseIf RsMMST.Fields("TRANSFER_REF_TYPE").Value = "R" Then
                cboRefType.Text = "RGP"
            ElseIf RsMMST.Fields("TRANSFER_REF_TYPE").Value = "N" Then
                cboRefType.Text = "NRGP"
            ElseIf RsMMST.Fields("TRANSFER_REF_TYPE").Value = "S" Then
                cboRefType.Text = "SALE"
            End If
            txtRefNo.Text = IIf(IsDBNull(RsMMST.Fields("TRANSFER_REF_NO").Value), "", RsMMST.Fields("TRANSFER_REF_NO").Value)
            txtRefDate.Text = IIf(IsDBNull(RsMMST.Fields("TRANSFER_REF_DATE").Value), "", RsMMST.Fields("TRANSFER_REF_DATE").Value)

            Dim mUnitCode As Long
            Dim mUnitName As String = ""

            mUnitCode = IIf(IsDBNull(RsMMST.Fields("TRANSFER_UNIT_CODE").Value), 0, RsMMST.Fields("TRANSFER_UNIT_CODE").Value)

            If MainClass.ValidateWithMasterTable(mUnitCode, "COMPANY_CODE", "COMPANY_NAME", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True Then
                mUnitName = Trim(MasterNo)
            End If

            txtUnitName.Text = mUnitName

            Call ShowDetail1()
            Call MakeEnableDeField(False)
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        MainClass.ButtonStatus(Me, XRIGHT, RsMMST, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
                    & " FROM MAN_MACHINE_MAINT_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachMaint, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMachMaint
            If .EOF = True Then Exit Sub
            i = 1
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColCheckType
                SprdMain.Text = IIf(IsDBNull(.Fields("CHECK_TYPE").Value), "", .Fields("CHECK_TYPE").Value)

                SprdMain.Col = ColFrequency
                SprdMain.Text = IIf(IsDBNull(.Fields("FREQUENCY").Value), "", CStr(.Fields("FREQUENCY").Value))

                SprdMain.Col = ColCheckHours
                SprdMain.Text = IIf(IsDBNull(.Fields("CHECK_HOURS").Value), "", CStr(.Fields("CHECK_HOURS").Value))

                SprdMain.Col = ColLastPM
                SprdMain.Text = IIf(IsDBNull(.Fields("LAST_PM").Value), "", VB6.Format(.Fields("LAST_PM").Value, "DD/MM/YYYY"))

                SprdMain.Col = ColDuePM
                SprdMain.Text = IIf(IsDBNull(.Fields("DUE_PM").Value), "", VB6.Format(.Fields("DUE_PM").Value, "DD/MM/YYYY"))

                .MoveNext()
                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
            txtMachineNo_Validating(txtMachineNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mMchbkdown As String
        Dim MKeyMachine As String
        Dim mStatus As String
        Dim mMaintType As String
        Dim mFuelType As String
        Dim mFuelConsOn As String
        Dim mDivisionCode As Double
        Dim mRefType As String
        Dim mRefNo As String
        Dim mRefDate As String
        Dim mUnitCode As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = ""

        MKeyMachine = IIf(chkKeyMachine.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mMchbkdown = IIf(chkMchbkDown.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mStatus = VB.Left(cboStatus.Text, 1)
        If cboMaintType.Text = "Preventive" Then
            mMaintType = "P"
        ElseIf cboMaintType.Text = "Preductive" Then
            mMaintType = "D"
        ElseIf cboMaintType.Text = "Hour Basis" Then
            mMaintType = "H"
        End If
        mFuelType = VB.Left(cboFuelType.Text, 1)
        mFuelConsOn = VB.Left(cboFuelConsOn.Text, 1)

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If


        mRefType = IIf(cboRefType.Text = "", "", Mid(cboRefType.Text, 1, 1))
        mRefNo = txtRefNo.Text
        mRefDate = VB6.Format(txtRefDate.Text, "DD/MMM/YYYY")

        If Trim(txtUnitName.Text) = "" Then
            mUnitCode = "NULL"
        Else
            If MainClass.ValidateWithMasterTable(txtUnitName.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                mUnitCode = MasterNo
            End If
        End If

        If ADDMode = True Then
            SqlStr = " INSERT INTO MAN_MACHINE_MST ( " & vbCrLf _
                    & " COMPANY_CODE, MACHINE_NO, MACHINE_ITEM_CODE, OPR_CODE, " & vbCrLf _
                    & " DEPT_CODE, NO_OF_PIECES, NO_OF_UNITS, MACHINE_DESC, " & vbCrLf _
                    & " LOCATION, MAKE, MACHINE_INST_DATE," & vbCrLf _
                    & " MACHINE_SPEC,CAPACITY,NO_OF_WORKHRS," & vbCrLf _
                    & " MACHINE_UB,KEY_MACHINE,STATUS,REMARKS,MAINT_TYPE, " & vbCrLf _
                    & " FUEL_TYPE,FUEL_CONS_ON,FUEL_CONS, " & vbCrLf _
                    & " ADDUSER,ADDDATE,MODUSER,MODDATE,DIV_CODE,MACH_ASSET_NO," & vbCrLf _
                    & " TRANSFER_REF_TYPE, TRANSFER_REF_NO, TRANSFER_REF_DATE, TRANSFER_UNIT_CODE) "

            SqlStr = SqlStr & vbCrLf _
                    & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', '" & MainClass.AllowSingleQuote(txtOperation.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & Val(txtPieces.Text) & ", " & vbCrLf _
                    & " " & Val(txtUnit.Text) & ", '" & MainClass.AllowSingleQuote(txtMachineDesc.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtLocation.Text) & "', '" & MainClass.AllowSingleQuote(txtMake.Text) & "', " & vbCrLf _
                    & " TO_DATE('" & VB6.Format(txtInsDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtSpec.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtCapacity.Text) & "', " & Val(txtWorkingHrs.Text) & ", " & vbCrLf _
                    & " '" & mMchbkdown & "','" & MKeyMachine & "', " & vbCrLf _
                    & " '" & mStatus & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & mMaintType & "', " & vbCrLf _
                    & " '" & mFuelType & "','" & mFuelConsOn & "'," & Val(txtFuelCons.Text) & ", " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & mDivisionCode & ",'" & MainClass.AllowSingleQuote(txtAssetNo.Text) & "'," & vbCrLf _
                    & " '" & mRefType & "', '" & mRefNo & "', TO_DATE('" & mRefDate & "','DD-MON-YYYY'), " & mUnitCode & ") "

        Else

            SqlStr = " UPDATE MAN_MACHINE_MST SET " & vbCrLf _
                & " MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "', " & vbCrLf _
                & " MACHINE_ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                & " OPR_CODE='" & MainClass.AllowSingleQuote(txtOperation.Text) & "', " & vbCrLf _
                & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                & " NO_OF_PIECES=" & Val(txtPieces.Text) & ", " & vbCrLf _
                & " NO_OF_UNITS=" & Val(txtUnit.Text) & ", " & vbCrLf _
                & " MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachineDesc.Text) & "', " & vbCrLf _
                & " LOCATION='" & MainClass.AllowSingleQuote(txtLocation.Text) & "', " & vbCrLf _
                & " MAKE='" & MainClass.AllowSingleQuote(txtMake.Text) & "', " & vbCrLf _
                & " MACHINE_INST_DATE=TO_DATE('" & VB6.Format(txtInsDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " KEY_MACHINE='" & MKeyMachine & "', " & vbCrLf _
                & " MACHINE_SPEC='" & MainClass.AllowSingleQuote(txtSpec.Text) & "', " & vbCrLf _
                & " MACHINE_UB='" & mMchbkdown & "', " & vbCrLf _
                & " CAPACITY='" & MainClass.AllowSingleQuote(txtCapacity.Text) & "', " & vbCrLf _
                & " NO_OF_WORKHRS=" & Val(txtWorkingHrs.Text) & ", " & vbCrLf _
                & " STATUS='" & mStatus & "', " & vbCrLf _
                & " MAINT_TYPE='" & mMaintType & "', " & vbCrLf _
                & " FUEL_TYPE='" & mFuelType & "', " & vbCrLf _
                & " FUEL_CONS_ON='" & mFuelConsOn & "', " & vbCrLf _
                & " FUEL_CONS=" & Val(txtFuelCons.Text) & ", MACH_ASSET_NO='" & MainClass.AllowSingleQuote(txtAssetNo.Text) & "' ," & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', DIV_CODE=" & mDivisionCode & "," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " TRANSFER_REF_TYPE='" & mRefType & "', TRANSFER_REF_NO='" & mRefNo & "', TRANSFER_REF_DATE=TO_DATE('" & mRefDate & "','DD-MON-YYYY'), TRANSFER_UNIT_CODE=" & mUnitCode & ""

            SqlStr = SqlStr & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'"
        End If



        PubDBCn.Execute(SqlStr)
        If UpdateDetail() = False Then GoTo UpdateError

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

        If Trim(txtMachineNo.Text) = "" Then
            MsgInformation("Machine Number is empty, So unable to Save")
            txtMachineNo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If Len(Trim(txtMachineNo.Text)) < 4 Then
                MsgInformation("Machine Number Cann't be less than 4 charactor, So unable to Save")
                txtMachineNo.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If
        If Trim(txtInsDate.Text) = "" Then
            MsgInformation("Installation date is empty, So unable to Save")
            txtInsDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtItemCode.Text) = "" Then
            MsgInformation("Item Code is empty. Cannot Save")
            txtItemCode.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtMachineDesc.Text) = "" Then
            MsgInformation("Item Name is empty. Cannot Save")
            txtItemCode.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDept.Text) = "" Then
            MsgInformation("Dept Name is empty. Cannot Save")
            txtDept.Focus()
            FieldsVarification = False
            Exit Function
        End If
        'If Trim(txtOperation.Text) = "" Then
        '    MsgInformation("Operation Name is empty. Cannot Save")
        '    txtOperation.Focus()
        '    FieldsVarification = False
        '    Exit Function
        'End If
        If Trim(txtPieces.Text) = "" Then
            MsgInformation("Pieces/Hrs is empty. Cannot Save.")
            txtPieces.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtUnit.Text) = "" Then
            MsgInformation("Unit/Hrs is empty. Cannot Save.")
            txtUnit.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtWorkingHrs.Text) = "" Then
            MsgInformation("Working Hrs is empty. Cannot Save.")
            txtWorkingHrs.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(cboMaintType.Text) = "" Then
            MsgInformation("Maintenance Type is empty, So unable to Save")
            cboMaintType.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        '    If Trim(cboMaintType.Text) <> "Preductive" Then
        '        If Trim(txtFrequency.Text) = "" Then
        '            MsgInformation "PM Frequency is empty. Cannot Save."
        '            txtFrequency.SetFocus
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '        If Trim(txtLastPM.Text) = "" Then
        '            MsgInformation "Last PM Date is empty. Cannot Save."
        '            txtLastPM.SetFocus
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        If Trim(cboStatus.Text) = "" Then
            MsgInformation("Status is empty, So unable to Save")
            cboStatus.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(cboFuelType.Text) = "" Then
            MsgInformation("Fuel Type is empty, So unable to Save")
            cboFuelType.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(cboFuelConsOn.Text) = "" Then
            MsgInformation("Fuel Consumption On is empty, So unable to Save")
            cboFuelConsOn.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtUnitName.Text) = "" Then
            MsgInformation("Unit Location Can't be empty. Cannot Save")
            txtItemCode.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If MainClass.ValidateWithMasterTable(txtUnitName.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = False Then
            MsgInformation("Invaild Unit Name.")
            txtUnitName.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Machine Details or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsMMST.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String
        Dim i As Integer
        Dim mCheckType As String
        Dim mFrequency As Double
        Dim mCheckHours As Double
        Dim mLastPM As String
        Dim mDuePM As String

        PubDBCn.Execute(" DELETE FROM MAN_MACHINE_MAINT_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'")

        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColCheckType
                mCheckType = MainClass.AllowSingleQuote(.Text)

                .Col = ColFrequency
                mFrequency = Val(.Text)

                .Col = ColCheckHours
                mCheckHours = Val(.Text)

                .Col = ColLastPM
                mLastPM = Trim(.Text)

                .Col = ColDuePM
                mDuePM = Trim(.Text)

                SqlStr = ""

                If mCheckType <> "" And mLastPM <> "" Then
                    SqlStr = " INSERT INTO MAN_MACHINE_MAINT_TRN ( " & vbCrLf & " COMPANY_CODE,MACHINE_NO,SERIAL_NO, " & vbCrLf & " CHECK_TYPE,FREQUENCY,CHECK_HOURS, " & vbCrLf & " LAST_PM,DUE_PM )" & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'," & i & ", " & vbCrLf & " '" & mCheckType & "'," & mFrequency & "," & mCheckHours & ", " & vbCrLf & " TO_DATE('" & VB6.Format(mLastPM, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mDuePM, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        MainClass.ClearGrid(SprdView)

        SqlStr = " SELECT A.MACHINE_NO, A.MACHINE_ITEM_CODE CODE, A.MACHINE_DESC, A.OPR_CODE, A.DEPT_CODE," & vbCrLf _
            & " A.NO_OF_PIECES, A.NO_OF_UNITS, A.MAKE, A.MACHINE_INST_DATE, A.KEY_MACHINE," & vbCrLf _
            & " A.MACHINE_UB,A.CAPACITY, A.NO_OF_WORKHRS, A.LOCATION, A.MACHINE_SPEC, B.COMPANY_NAME" & vbCrLf _
            & " FROM MAN_MACHINE_MST A, GEN_COMPANY_MST B" & vbCrLf _
            & " Where A.TRANSFER_UNIT_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND (A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " OR a.TRANSFER_UNIT_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf _
            & " ORDER BY A.MACHINE_NO"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "MACHINE MASTER"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\MCHMaster.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub TxtCapacity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCapacity.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtCapacity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCapacity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCapacity.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(CmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Deparment Does Not Exist In Master.")
            Cancel = True
        Else
            txtDeptName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtFuelCons_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFuelCons.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFuelCons_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFuelCons.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFuelCons_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFuelCons.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtFuelCons.Text = VB6.Format(txtFuelCons.Text, "0.00")
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLocation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLocation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAssetNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAssetNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAssetNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAssetNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAssetNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtInsDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInsDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtInsDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInsDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtInsDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtInsDate.Text) Then
            MsgBox("Not a Valid Date")
            Cancel = True
        End If
        txtInsDate.Text = VB6.Format(txtInsDate.Text, "DD/MM/YYYY")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchItemCode_Click(CmdSearchItemCode, New System.EventArgs())
    End Sub

    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Item Code Does Not Exist In Master.")
            Cancel = True
        Else
            txtItemName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMachineDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineDesc.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMachineDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachineDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Public Sub txtMachineNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1

        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsMMST.EOF = False Then xMachineno = RsMMST.Fields("MACHINE_NO").Value
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
                    & " FROM MAN_MACHINE_MST" & vbCrLf _
                    & " WHERE (COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " OR TRANSFER_UNIT_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMMST, ADODB.LockTypeEnum.adLockReadOnly)

        If RsMMST.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            xMachineno = RsMMST.Fields("MACHINE_NO").Value
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Such Machine No Does Not Exist" & vbCrLf & "Click Add To Add for New.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * " & vbCrLf _
                    & " FROM MAN_MACHINE_MST" & vbCrLf _
                    & " WHERE (COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " OR TRANSFER_UNIT_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(xMachineno) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMMST, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtMachineNo.MaxLength = RsMMST.Fields("MACHINE_NO").DefinedSize
        txtInsDate.MaxLength = RsMMST.Fields("MACHINE_INST_DATE").DefinedSize - 6
        txtItemCode.MaxLength = RsMMST.Fields("MACHINE_ITEM_CODE").DefinedSize
        txtItemName.MaxLength = 255
        txtMachineDesc.MaxLength = RsMMST.Fields("MACHINE_DESC").DefinedSize
        txtSpec.MaxLength = RsMMST.Fields("MACHINE_SPEC").DefinedSize
        txtCapacity.MaxLength = RsMMST.Fields("CAPACITY").DefinedSize
        txtDept.MaxLength = RsMMST.Fields("DEPT_CODE").DefinedSize
        txtDeptName.MaxLength = 255
        txtLocation.MaxLength = RsMMST.Fields("LOCATION").DefinedSize
        txtAssetNo.MaxLength = RsMMST.Fields("MACH_ASSET_NO").DefinedSize

        txtRefNo.MaxLength = RsMMST.Fields("TRANSFER_REF_NO").DefinedSize
        txtRefDate.MaxLength = 10

        txtMake.MaxLength = RsMMST.Fields("MAKE").DefinedSize
        txtOperation.MaxLength = RsMMST.Fields("OPR_CODE").DefinedSize
        txtOperationName.MaxLength = 255
        txtPieces.MaxLength = RsMMST.Fields("NO_OF_PIECES").Precision
        txtWorkingHrs.MaxLength = RsMMST.Fields("NO_OF_WORKHRS").Precision
        txtUnit.MaxLength = RsMMST.Fields("NO_OF_UNITS").Precision
        txtRemarks.MaxLength = RsMMST.Fields("REMARKS").Precision
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 900)
            .set_ColWidth(2, 900)
            .ColsFrozen = 2

            .set_ColWidth(3, 1500)
            .set_ColWidth(4, 900)
            .set_ColWidth(5, 900)
            .set_ColWidth(6, 900)
            .set_ColWidth(7, 900)
            .set_ColWidth(8, 1500)
            .set_ColWidth(9, 900)
            .set_ColWidth(10, 900)
            .set_ColWidth(11, 900)
            .set_ColWidth(12, 900)
            .set_ColWidth(13, 900)
            .set_ColWidth(14, 3500)
            .set_ColWidth(15, 3500)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMachineNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMachineNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachineNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMake_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMake.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMake_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMake.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMake.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOperation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOperation.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOperation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOperation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOperation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOperation_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOperation.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchOp_Click(CmdSearchOp, New System.EventArgs())
    End Sub

    Private Sub txtOperation_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOperation.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtOperation.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtOperation.Text, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Operation Does Not Exist In Master.")
            Cancel = True
        Else
            txtOperationName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtPieces_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPieces.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtPieces_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPieces.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtPieces_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPieces.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtPieces.Text = VB6.Format(txtPieces.Text, "0.000")
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtSpec_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSpec.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtSpec_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSpec.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSpec.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtUnit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUnit.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtUnit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUnit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtUnit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtUnit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtUnit.Text = VB6.Format(txtUnit.Text, "0.00")
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtWorkingHrs_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWorkingHrs.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtWorkingHrs_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWorkingHrs.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtWorkingHrs_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWorkingHrs.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtWorkingHrs.Text = VB6.Format(txtWorkingHrs.Text, "0.00")
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRefNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtUnitName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUnitName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtUnitName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUnitName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtUnitName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtRefDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRefDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtRefDate.Text) Then
            MsgBox("Not a Valid Date")
            Cancel = True
        End If
        txtRefDate.Text = VB6.Format(txtRefDate.Text, "DD/MM/YYYY")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchUnit_Click(sender As Object, e As EventArgs) Handles cmdSearchUnit.Click
        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtUnitName.Text, "GEN_COMPANY_MST", "COMPANY_NAME", "", , , "") = True Then
            txtUnitName.Text = AcName
            If txtUnitName.Enabled = True Then txtUnitName.Focus()
        End If
    End Sub
    Private Sub txtUnitName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtUnitName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchUnit_Click(cmdSearchUnit, New System.EventArgs())
    End Sub

    Private Sub txtUnitName_DoubleClick(sender As Object, e As EventArgs) Handles txtUnitName.DoubleClick
        Call cmdSearchUnit_Click(cmdSearchUnit, New System.EventArgs())
    End Sub

    Private Sub txtUnitName_Validating(sender As Object, e As CancelEventArgs) Handles txtUnitName.Validating
        Dim Cancel As Boolean = e.Cancel

        If txtUnitName.Text = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable(txtUnitName.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = False Then
            MsgInformation("Invaild Unit Name.")
            txtUnitName.Focus()
            Cancel = True
        End If

        e.Cancel = Cancel
    End Sub
End Class
