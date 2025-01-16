Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmMachineCP
    Inherits System.Windows.Forms.Form
    Dim RsMachineCPHdr As ADODB.Recordset
    Dim RsMachineCPDet As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim PvtDBCn As ADODB.Connection
    Dim FormActive As Boolean
    Dim SqlStr As String

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Dim xMenuID As String

    Private Const ColCategory As Short = 1
    Private Const ColCheckPoint As Short = 2
    Private Const ColRequirment As Short = 3
    Private Const ColCheckingMethod As Short = 4

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
        MainClass.ButtonStatus(Me, XRIGHT, RsMachineCPHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()

        lblMkey.Text = ""
        txtNumber.Text = ""
        txtMachineDesc.Text = ""
        txtMachineSpec.Text = ""
        txtCheckType.Text = ""
        Call MakeEnableDeField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight * 1.5)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsMachineCPHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub MakeEnableDeField(ByRef mMode As Boolean)
        txtMachineDesc.Enabled = mMode
        cmdSearchMachineDesc.Enabled = mMode
        txtMachineSpec.Enabled = mMode
        cmdSearchMachineSpec.Enabled = mMode
        txtCheckType.Enabled = mMode
        cmdSearchCheckType.Enabled = mMode
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdMain
            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Row = Arow

            .Col = ColCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsMachineCPDet.Fields("CATEGORY").DefinedSize
            .set_ColWidth(ColCategory, 20)
            .FontBold = True

            .Col = ColCheckPoint
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_NO_CASE
            .TypeEditLen = RsMachineCPDet.Fields("CHECK_POINT").DefinedSize
            .TypeEditMultiLine = True
            .set_ColWidth(ColCheckPoint, 35)

            .Col = ColRequirment
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_NO_CASE
            .TypeEditLen = RsMachineCPDet.Fields("CHECK_REQUIRMENT").DefinedSize
            .TypeEditMultiLine = True
            .set_ColWidth(ColRequirment, 35)

            .Col = ColCheckingMethod
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_NO_CASE
            .TypeEditLen = RsMachineCPDet.Fields("CHECK_METHOD").DefinedSize
            .TypeEditMultiLine = True
            .set_ColWidth(ColCheckingMethod, 30)

            .Col = ColCategory
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsMachineCPHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            MakeEnableDeField((True))
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

    Private Sub cmdSearchCheckType_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCheckType.Click

        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = " SELECT DISTINCT CHECK_TYPE FROM MAN_MACHINE_MAINT_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf _
            & " AND MACHINE_NO IN ( " & vbCrLf _
            & " SELECT MACHINE_NO FROM MAN_MACHINE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf _
            & " AND STATUS='O' AND MAINT_TYPE IN ('P','H') "

        If Trim(txtMachineDesc.Text) <> "" Then
            SqlStr = SqlStr & " AND MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachineDesc.Text) & "' "
        End If
        If Trim(txtMachineSpec.Text) <> "" Then
            SqlStr = SqlStr & " AND MACHINE_SPEC='" & MainClass.AllowSingleQuote(txtMachineSpec.Text) & "' "
        End If
        SqlStr = SqlStr & vbCrLf & " ) "

        If MainClass.SearchGridMasterBySQL2(txtCheckType.Text, SqlStr) = True Then
            txtCheckType.Text = AcName
        End If
        txtCheckType.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchMachineDesc_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachineDesc.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' AND MAINT_TYPE IN ('P','H') "
        If MainClass.SearchGridMaster(txtMachineDesc.Text, "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_ITEM_CODE", , , SqlStr) = True Then
            txtMachineDesc.Text = AcName
        End If
        txtMachineDesc.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchMachineSpec_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachineSpec.Click

        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' AND MAINT_TYPE IN ('P','H') "
        If Trim(txtMachineDesc.Text) <> "" Then
            SqlStr = SqlStr & " AND MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachineDesc.Text) & "' "
        End If
        If MainClass.SearchGridMaster(txtMachineSpec.Text, "MAN_MACHINE_MST", "MACHINE_SPEC", , , , SqlStr) = True Then
            txtMachineSpec.Text = AcName
        End If
        txtMachineSpec.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNumber.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
        If MainClass.SearchGridMaster("", "MAN_MACHINE_CP_HDR", "Auto_Key_CP", "MACHINE_DESC", "MACHINE_SPEC", "CHECK_TYPE", SqlStr) = True Then
            txtNumber.Text = AcName
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
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
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsMachineCPHdr.EOF = False Then RsMachineCPHdr.MoveFirst()
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
        Me.Dispose()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsMachineCPHdr.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_MACHINE_CP_HDR", (txtNumber.Text), RsMachineCPHdr) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_MACHINE_CP_DET WHERE AUTO_KEY_CP=" & Val(lblMkey.Text) & " ")
                PubDBCn.Execute("DELETE FROM MAN_MACHINE_CP_HDR WHERE AUTO_KEY_CP=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ")
                PubDBCn.CommitTrans()
                RsMachineCPHdr.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsMachineCPHdr.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub frmMachineCP_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmMachineCP_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String
        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColCategory)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xCategory As String

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColCategory
        xCategory = Trim(SprdMain.Text)
        If xCategory = "" Then Exit Sub
        MainClass.AddBlankSprdRow(SprdMain, ColCategory, ConRowHeight * 1.5)
        If eventArgs.NewRow = SprdMain.MaxRows And eventArgs.NewCol = ColCategory Then
            SprdMain.Col = ColCategory
            SprdMain.Text = xCategory
        End If
        FormatSprdMain((SprdMain.MaxRows))

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        'With SprdMain
        '    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        txtNumber.Text = SprdView.Text
        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Public Sub frmMachineCP_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        'Me.Text = "Preventive Maintenance Check Points"

        SqlStr = " Select * From MAN_MACHINE_CP_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineCPHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = " Select * From MAN_MACHINE_CP_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineCPDet, ADODB.LockTypeEnum.adLockReadOnly)

        Call SetTextLengths()
        Call AssignGrid(False)
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

    Private Sub frmMachineCP_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMenuID = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(7515)
        'Me.Width = VB6.TwipsToPixelsX(8460)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmMachineCP_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsMachineCPHdr.Close()
        RsMachineCPHdr = Nothing
        RsMachineCPDet.Close()
        RsMachineCPDet = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Clear1()
        If Not RsMachineCPHdr.EOF Then
            IsShowing = True

            lblMkey.Text = IIf(IsDbNull(RsMachineCPHdr.Fields("AUTO_KEY_CP").Value), "", RsMachineCPHdr.Fields("AUTO_KEY_CP").Value)
            txtNumber.Text = IIf(IsDbNull(RsMachineCPHdr.Fields("AUTO_KEY_CP").Value), "", RsMachineCPHdr.Fields("AUTO_KEY_CP").Value)
            txtMachineDesc.Text = IIf(IsDbNull(RsMachineCPHdr.Fields("MACHINE_DESC").Value), "", RsMachineCPHdr.Fields("MACHINE_DESC").Value)
            txtMachineSpec.Text = IIf(IsDbNull(RsMachineCPHdr.Fields("MACHINE_SPEC").Value), "", RsMachineCPHdr.Fields("MACHINE_SPEC").Value)
            txtCheckType.Text = IIf(IsDbNull(RsMachineCPHdr.Fields("CHECK_TYPE").Value), "", RsMachineCPHdr.Fields("CHECK_TYPE").Value)

            Call ShowCP()
            Call MakeEnableDeField(False)
            IsShowing = False
        End If
        txtNumber.Enabled = True
        cmdSearchNumber.Enabled = True
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        MainClass.ButtonStatus(Me, XRIGHT, RsMachineCPHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub ShowCP()

        On Error GoTo ERR1
        Dim I As Integer

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM MAN_MACHINE_CP_DET " & vbCrLf _
            & " WHERE AUTO_KEY_CP=" & Val(lblMkey.Text) & " " & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineCPDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMachineCPDet
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColCategory
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("CATEGORY").Value), "", .Fields("CATEGORY").Value))

                SprdMain.Col = ColCheckPoint
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CHECK_POINT").Value), "", .Fields("CHECK_POINT").Value))

                SprdMain.Col = ColRequirment
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CHECK_REQUIRMENT").Value), "", .Fields("CHECK_REQUIRMENT").Value))

                SprdMain.Col = ColCheckingMethod
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CHECK_METHOD").Value), "", .Fields("CHECK_METHOD").Value))


                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        If IsRecordExist = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
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

    Private Function IsRecordExist() As Boolean

        On Error GoTo IsRecordExistERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        IsRecordExist = False
        If MODIFYMode = True Then Exit Function
        SqlStr = " SELECT AUTO_KEY_CP " & vbCrLf _
                    & " From MAN_MACHINE_CP_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND MACHINE_DESC ='" & MainClass.AllowSingleQuote(txtMachineDesc.Text) & "' " & vbCrLf _
                    & " AND MACHINE_SPEC = '" & MainClass.AllowSingleQuote(txtMachineSpec.Text) & "' " & vbCrLf _
                    & " AND CHECK_TYPE= '" & MainClass.AllowSingleQuote(txtCheckType.Text) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_CP").Value)
                IsRecordExist = True
            End If
        End With
        Exit Function
IsRecordExistERR:
        IsRecordExist = True
        MsgBox(Err.Description)
    End Function

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_CP)  " & vbCrLf & " FROM MAN_MACHINE_CP_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    '                mAutoGen = Mid(.Fields(0), 1, Len(.Fields(0)) - 6)
                    mAutoGen = .Fields(0).Value + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenKeyNo = mAutoGen
        '    AutoGenKeyNo = mAutoGen & vb6.Format(RsCompany.fields("FYEAR").value, "0000") & vb6.Format(RsCompany.fields("COMPANY_CODE").value, "00")
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)

        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO MAN_MACHINE_CP_HDR " & vbCrLf _
                            & " (COMPANY_CODE,AUTO_KEY_CP,MACHINE_DESC,MACHINE_SPEC,CHECK_TYPE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & RsCompany.fields("COMPANY_CODE").value & "," & mSlipNo & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMachineDesc.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMachineSpec.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCheckType.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"


        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MAN_MACHINE_CP_HDR SET " & vbCrLf & " AUTO_KEY_CP=" & mSlipNo & "," & vbCrLf & " MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachineDesc.Text) & "', " & vbCrLf & " MACHINE_SPEC='" & MainClass.AllowSingleQuote(txtMachineSpec.Text) & "', " & vbCrLf & " CHECK_TYPE='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_CP =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateCP = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsMachineCPHdr.Requery()
        RsMachineCPDet.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function UpdateCP() As Boolean

        On Error GoTo UpdateCPERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mCategory As String
        Dim mCheckPoint As String
        Dim mRequirment As String
        Dim mCheckingMethod As String

        PubDBCn.Execute("DELETE FROM MAN_MACHINE_CP_DET WHERE AUTO_KEY_CP=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value)

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColCategory
                mCategory = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColCheckPoint
                mCheckPoint = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColRequirment
                mRequirment = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColCheckingMethod
                mCheckingMethod = MainClass.AllowSingleQuote(Trim(.Text))

                SqlStr = ""
                If mCategory <> "" Then
                    SqlStr = " INSERT INTO  MAN_MACHINE_CP_DET ( " & vbCrLf _
                        & " COMPANY_CODE,AUTO_KEY_CP,SERIAL_NO,CATEGORY,CHECK_POINT,CHECK_REQUIRMENT, CHECK_METHOD ) " & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & "," & I & "," & vbCrLf _
                        & " '" & mCategory & "','" & mCheckPoint & "','" & mRequirment & "','" & mCheckingMethod & "')"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        UpdateCP = True
        Exit Function
UpdateCPERR:
        UpdateCP = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtMachineDesc.Text) = "" Then
            MsgInformation("Machine Desc is empty, So unable to Save")
            txtMachineDesc.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtMachineSpec.Text) = "" Then
            MsgInformation("Specification is empty, So unable to Save")
            txtMachineSpec.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtCheckType.Text) = "" Then
            MsgInformation("Check Type is empty, So unable to Save")
            txtCheckType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColCategory, "S", "Please Check Category.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColCheckPoint, "S", "Please Check Check Point.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColRequirment, "S", "Please Check Requirment.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColCheckingMethod, "S", "Please Check Checking Method.") = False Then FieldsVarification = False

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Machine Details or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsMachineCPHdr.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = " SELECT AUTO_KEY_CP as Slip_No,MACHINE_DESC,MACHINE_SPEC,CHECK_TYPE " & vbCrLf _
            & " FROM MAN_MACHINE_CP_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " ORDER BY AUTO_KEY_CP"


        Call MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()
    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "Preventive Maintenance Check Points"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\MachineCP.rpt"

        SqlStr = " SELECT MAN_MACHINE_CP_HDR.*,MAN_MACHINE_CP_DET.* " & vbCrLf & " FROM MAN_MACHINE_CP_HDR, MAN_MACHINE_CP_DET " & vbCrLf & " WHERE MAN_MACHINE_CP_HDR.AUTO_KEY_CP=MAN_MACHINE_CP_DET.AUTO_KEY_CP " & vbCrLf & " AND MAN_MACHINE_CP_HDR.COMPANY_CODE=MAN_MACHINE_CP_DET.COMPANY_CODE " & vbCrLf & " AND MAN_MACHINE_CP_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MAN_MACHINE_CP_HDR.AUTO_KEY_CP=" & Val(lblMkey.Text) & " "

        Report1.SQLQuery = SqlStr
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtCheckType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCheckType.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCheckType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCheckType.DoubleClick
        Call cmdSearchCheckType_Click(cmdSearchCheckType, New System.EventArgs())
    End Sub

    Private Sub txtCheckType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCheckType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCheckType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCheckType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCheckType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCheckType_Click(cmdSearchCheckType, New System.EventArgs())
    End Sub

    Private Sub txtCheckType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCheckType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValProbERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If Trim(txtCheckType.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT DISTINCT CHECK_TYPE FROM MAN_MACHINE_MAINT_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & vbCrLf _
                    & " AND CHECK_TYPE='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "' " & vbCrLf _
                    & " AND MACHINE_NO IN ( " & vbCrLf _
                    & " SELECT MACHINE_NO FROM MAN_MACHINE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & vbCrLf _
                    & " AND STATUS='O' AND MAINT_TYPE IN ('P','H') "
        If Trim(txtMachineDesc.Text) <> "" Then
            SqlStr = SqlStr & " AND MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachineDesc.Text) & "' "
        End If
        If Trim(txtMachineSpec.Text) <> "" Then
            SqlStr = SqlStr & " AND MACHINE_SPEC='" & MainClass.AllowSingleQuote(txtMachineSpec.Text) & "' "
        End If
        SqlStr = SqlStr & vbCrLf & " ) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF Then
            MsgBox("Not a valid Check Type", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMachineDesc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineDesc.DoubleClick
        Call cmdSearchMachineDesc_Click(cmdSearchMachineDesc, New System.EventArgs())
    End Sub

    Private Sub txtMachineDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachineDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMachineDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineDesc.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMachineDesc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachineDesc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachineDesc_Click(cmdSearchMachineDesc, New System.EventArgs())
    End Sub

    Private Sub txtMachineDesc_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineDesc.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtMachineDesc.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' AND MAINT_TYPE IN ('P','H') "
        If MainClass.ValidateWithMasterTable(txtMachineDesc.Text, "MACHINE_DESC", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Machine Desc", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMachineSpec_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineSpec.DoubleClick
        Call cmdSearchMachineSpec_Click(cmdSearchMachineSpec, New System.EventArgs())
    End Sub

    Private Sub txtMachineSpec_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineSpec.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachineSpec.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMachineSpec_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineSpec.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMachineSpec_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachineSpec.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachineSpec_Click(cmdSearchMachineSpec, New System.EventArgs())
    End Sub

    Private Sub txtMachineSpec_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineSpec.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtMachineSpec.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' AND MAINT_TYPE IN ('P','H') "
        If Trim(txtMachineDesc.Text) <> "" Then
            SqlStr = SqlStr & " AND MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachineDesc.Text) & "' "
        End If
        If MainClass.ValidateWithMasterTable(txtMachineSpec.Text, "MACHINE_SPEC", "MACHINE_SPEC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Specification", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Public Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String

        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub
        mSlipNo = Val(txtNumber.Text)

        If MODIFYMode = True And RsMachineCPHdr.BOF = False Then xMKey = RsMachineCPHdr.Fields("AUTO_KEY_CP").Value

        SqlStr = "SELECT * FROM MAN_MACHINE_CP_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_CP=" & mSlipNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineCPHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMachineCPHdr.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_MACHINE_CP_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_CP=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineCPHdr, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtNumber.Maxlength = RsMachineCPHdr.Fields("AUTO_KEY_CP").DefinedSize
        txtMachineDesc.Maxlength = RsMachineCPHdr.Fields("MACHINE_DESC").DefinedSize
        txtMachineSpec.Maxlength = RsMachineCPHdr.Fields("MACHINE_SPEC").DefinedSize
        txtCheckType.Maxlength = RsMachineCPHdr.Fields("CHECK_TYPE").DefinedSize
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 500 * 2)
            .set_ColWidth(2, 500 * 10)
            .set_ColWidth(3, 500 * 8)
            .set_ColWidth(4, 500 * 2)
            .set_ColWidth(5, 500 * 4)
            .set_ColWidth(6, 500 * 4)
            .set_ColWidth(7, 500 * 3)
            .set_ColWidth(8, 500 * 5)
            .set_ColWidth(9, 500 * 1)
            .set_ColWidth(10, 500 * 3)
            .set_ColWidth(11, 500 * 3)




            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNumber_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNumber.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNumber.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
