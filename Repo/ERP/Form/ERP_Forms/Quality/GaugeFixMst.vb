Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGaugeFixMst
    Inherits System.Windows.Forms.Form
    Dim RsGaugeFix As ADODB.Recordset
    Dim RsGaugeInspStd As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim PvtDBCn As ADODB.Connection
    Dim FormActive As Boolean
    Dim SqlStr As String

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Dim xMenuID As String

    Private Const ColParamDesc As Short = 1
    Private Const ColSpecification As Short = 2
    Private Const ColSpecPlus As Short = 3
    Private Const ColSpecMinus As Short = 4
    Private Const ColWearLimit As Short = 5
    Private Const ColInspMth As Short = 6

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
        MainClass.ButtonStatus(Me, XRIGHT, RsGaugeFix, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        lblMkey.Text = ""
        txtNumber.Text = ""
        txtModel.Text = ""
        txtDescription.Text = ""
        txtComponent.Text = ""
        txtCustomer.Text = ""
        cboType.SelectedIndex = 0
        txtTypeNo.Text = ""
        txtVDoneOn.Text = ""
        txtLocation.Text = ""
        txtDRGNo.Text = ""
        txtGoSize.Text = ""
        txtNogoSize.Text = ""
        txtSize.Text = ""
        txtRemarks.Text = ""
        txtWearSize.Text = ""
        txtValFrequency.Text = ""
        txtQty.Text = ""
        txtVDueOn.Text = ""
        txtIssueDate.Text = ""
        txtPartName.Text = ""
        optStatus(0).Checked = True
        Call MakeEnableDeField(True)
        MainClass.ClearGrid(SprdStd, ConRowHeight)
        FormatSprdStd(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsGaugeFix, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub MakeEnableDeField(ByRef mMode As Boolean)
        txtVDueOn.Enabled = False
        'cboType.Enabled = mMode
    End Sub

    Private Sub FormatSprdStd(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdStd
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColParamDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsGaugeInspStd.Fields("PARAM_DESC").DefinedSize

            .Col = ColSpecification
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.999")
            .TypeFloatMin = CDbl("-99999.999")
            .TypeEditLen = RsGaugeInspStd.Fields("SPECIFICATION").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 3


            .Col = ColSpecPlus
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.999")
            .TypeFloatMin = CDbl("-99999.999")
            .TypeEditLen = RsGaugeInspStd.Fields("SPEC_PLUS").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 3

            .Col = ColSpecMinus
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.999")
            .TypeFloatMin = CDbl("-99999.999")
            .TypeEditLen = RsGaugeInspStd.Fields("SPEC_MINUS").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 3

            .Col = ColWearLimit
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.999")
            .TypeFloatMin = CDbl("-99999.999")
            .TypeEditLen = RsGaugeInspStd.Fields("WEAR_LIMIT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 3

            .Col = ColInspMth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsGaugeInspStd.Fields("INSP_MTH").DefinedSize

            MainClass.SetSpreadColor(SprdStd, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Function CheckDate(ByRef pTxtDate As System.Windows.Forms.TextBox) As Boolean
        CheckDate = True
        If Trim(pTxtDate.Text) = "" Then Exit Function
        If Not IsDate(pTxtDate.Text) Then
            MsgBox("Not a Valid Date")
            CheckDate = False
        Else
            pTxtDate.Text = VB6.Format(pTxtDate.Text, "DD/MM/YYYY")
        End If
    End Function

    Private Sub cboType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.SelectedIndexChanged
        If cboType.Text = "GAUGE" Or cboType.Text = "PGG" Then
            fraStd.Visible = False
            '        fraStd.Enabled = False
            fraSize.Visible = True
            fraSize.Enabled = True
        Else
            fraSize.Enabled = False
            fraSize.Visible = False
            fraStd.Visible = True
            '        fraStd.Enabled = True
        End If
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsGaugeFix, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdStd.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub CalcVDueOn()
        If Trim(txtVDoneOn.Text) = "" Then txtVDueOn.Text = "" : Exit Sub
        txtVDueOn.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, Val(txtValFrequency.Text) * 30, CDate(txtVDoneOn.Text)))
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Call ShowReport(Crystal.DestinationConstants.crptToWindow)
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdSearchNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNumber.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "QAL_GAUGEFIX_MST", "DocNo", "Model", "Description", "Customer", SqlStr) = True Then
            txtNumber.Text = AcName
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''__Validating(XXXX, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
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
            SprdStd.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsGaugeFix.EOF = False Then RsGaugeFix.MoveFirst()
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

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsGaugeFix.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_GAUGEFIX_MST", (txtNumber.Text), RsGaugeFix) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM QAL_IMTE_SCHD_DET WHERE DocNo=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DOC_TYPE='G'")

                PubDBCn.Execute("DELETE FROM QAL_GAUGE_CALIB_STD WHERE DocNo=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ")
                PubDBCn.Execute("DELETE FROM QAL_GAUGEFIX_MST WHERE DocNo=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ")
                PubDBCn.CommitTrans()
                RsGaugeFix.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsGaugeFix.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub frmGaugeFixMst_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmGaugeFixMst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub optStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdStd_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdStd.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdStd_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdStd.ClickEvent

        Dim SqlStr As String
        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdStd, eventArgs.Row, ColParamDesc)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdStd_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdStd.LeaveCell

        On Error GoTo ErrPart
        Dim xParamDesc As String
        Dim xInspMth As String

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdStd.Row = SprdStd.ActiveRow

        SprdStd.Col = ColInspMth
        xInspMth = Trim(SprdStd.Text)
        SprdStd.Col = ColParamDesc
        xParamDesc = Trim(SprdStd.Text)
        If xParamDesc = "" Then Exit Sub
        MainClass.AddBlankSprdRow(SprdStd, ColParamDesc, ConRowHeight)
        If eventArgs.NewRow = SprdStd.MaxRows And eventArgs.NewCol = ColParamDesc Then
            SprdStd.Col = ColParamDesc
            SprdStd.Text = xParamDesc
            SprdStd.SelText = xParamDesc
            SprdStd.Col = ColInspMth
            SprdStd.Text = xInspMth
        End If
        FormatSprdStd((SprdStd.MaxRows))

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdStd_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdStd.Leave
        With SprdStd
            SprdStd_LeaveCell(SprdStd, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
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
    Public Sub frmGaugeFixMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Gauge Fixture Master"

        SqlStr = " Select * From QAL_GAUGEFIX_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeFix, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = " Select * From QAL_GAUGE_CALIB_STD Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeInspStd, ADODB.LockTypeEnum.adLockReadOnly)

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
    Private Sub frmGaugeFixMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
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
        Me.Height = VB6.TwipsToPixelsY(7275)
        Me.Width = VB6.TwipsToPixelsX(9540)
        Call FillCboType()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub FillCboType()
        cboType.Items.Clear()
        cboType.Items.Add("FIXT")
        cboType.Items.Add("GAUGE")
        cboType.Items.Add("JIG")
        cboType.Items.Add("PGG")
        cboType.Items.Add("REC GAUGE")
        cboType.Items.Add("RING GAUGE")
        cboType.Items.Add("SNAP GAUGE")
        cboType.Items.Add("SPG")
        cboType.SelectedIndex = 0
    End Sub
    Private Sub frmGaugeFixMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsGaugeFix.Close()
        RsGaugeFix = Nothing
        RsGaugeInspStd.Close()
        RsGaugeInspStd = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        If Not RsGaugeFix.EOF Then
            IsShowing = True

            lblMkey.Text = IIf(IsDbNull(RsGaugeFix.Fields("DOCNO").Value), "", RsGaugeFix.Fields("DOCNO").Value)
            txtNumber.Text = IIf(IsDbNull(RsGaugeFix.Fields("DOCNO").Value), "", RsGaugeFix.Fields("DOCNO").Value)
            txtModel.Text = IIf(IsDbNull(RsGaugeFix.Fields("MODEL").Value), "", RsGaugeFix.Fields("MODEL").Value)
            txtDescription.Text = IIf(IsDbNull(RsGaugeFix.Fields("Description").Value), "", RsGaugeFix.Fields("Description").Value)
            txtComponent.Text = IIf(IsDbNull(RsGaugeFix.Fields("COMPONENT_DESC").Value), "", RsGaugeFix.Fields("COMPONENT_DESC").Value)
            txtCustomer.Text = IIf(IsDbNull(RsGaugeFix.Fields("Customer").Value), "", RsGaugeFix.Fields("Customer").Value)
            cboType.Text = IIf(IsDbNull(RsGaugeFix.Fields("Type").Value), "", RsGaugeFix.Fields("Type").Value)
            txtTypeNo.Text = IIf(IsDbNull(RsGaugeFix.Fields("TypeNo").Value), "", RsGaugeFix.Fields("TypeNo").Value)
            txtVDoneOn.Text = IIf(IsDbNull(RsGaugeFix.Fields("VDoneOn").Value), "", RsGaugeFix.Fields("VDoneOn").Value)
            txtLocation.Text = IIf(IsDbNull(RsGaugeFix.Fields("Location").Value), "", RsGaugeFix.Fields("Location").Value)
            txtDRGNo.Text = IIf(IsDbNull(RsGaugeFix.Fields("DrgNo").Value), "", RsGaugeFix.Fields("DrgNo").Value)
            txtGoSize.Text = IIf(IsDbNull(RsGaugeFix.Fields("ReqGoSize").Value), "", RsGaugeFix.Fields("ReqGoSize").Value)
            txtNogoSize.Text = IIf(IsDbNull(RsGaugeFix.Fields("ReqNoGoSize").Value), "", RsGaugeFix.Fields("ReqNoGoSize").Value)
            txtSize.Text = IIf(IsDbNull(RsGaugeFix.Fields("CompSize").Value), "", RsGaugeFix.Fields("CompSize").Value)
            txtRemarks.Text = IIf(IsDbNull(RsGaugeFix.Fields("REMARKS").Value), "", RsGaugeFix.Fields("REMARKS").Value)
            txtWearSize.Text = IIf(IsDbNull(RsGaugeFix.Fields("WearSize").Value), "", RsGaugeFix.Fields("WearSize").Value)
            txtValFrequency.Text = CStr(Val(IIf(IsDbNull(RsGaugeFix.Fields("ValFrequency").Value), "", RsGaugeFix.Fields("ValFrequency").Value)))
            txtQty.Text = VB6.Format(IIf(IsDbNull(RsGaugeFix.Fields("CHECK_QTY").Value), "", RsGaugeFix.Fields("CHECK_QTY").Value), "0")

            txtPartName.Text = IIf(IsDbNull(RsGaugeFix.Fields("PartName").Value), "", RsGaugeFix.Fields("PartName").Value)

            txtVDueOn.Text = IIf(IsDbNull(RsGaugeFix.Fields("VDUEON").Value), "", RsGaugeFix.Fields("VDUEON").Value)
            txtIssueDate.Text = IIf(IsDbNull(RsGaugeFix.Fields("ISSUEDATE").Value), "", RsGaugeFix.Fields("ISSUEDATE").Value)
            If RsGaugeFix.Fields("Status").Value = "O" Then
                optStatus(0).Checked = True
            Else
                optStatus(1).Checked = True
            End If

            Call ShowStd()
            Call MakeEnableDeField(False)
            IsShowing = False
        End If
        txtNumber.Enabled = True
        cmdSearchNumber.Enabled = True
        ADDMode = False
        MODIFYMode = False
        SprdStd.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsGaugeFix, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub ShowStd()

        On Error GoTo ERR1
        Dim i As Integer

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_GAUGE_CALIB_STD " & vbCrLf & " WHERE DOCNO=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeInspStd, ADODB.LockTypeEnum.adLockReadOnly)
        With RsGaugeInspStd
            If .EOF = True Then Exit Sub
            FormatSprdStd(-1)
            i = 1
            Do While Not .EOF
                SprdStd.Row = i

                SprdStd.Col = ColParamDesc
                SprdStd.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdStd.Col = ColSpecification
                SprdStd.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                SprdStd.Col = ColSpecPlus
                SprdStd.Text = Trim(IIf(IsDbNull(.Fields("SPEC_PLUS").Value), "", .Fields("SPEC_PLUS").Value))

                SprdStd.Col = ColSpecMinus
                SprdStd.Text = Trim(IIf(IsDbNull(.Fields("SPEC_MINUS").Value), "", .Fields("SPEC_MINUS").Value))

                SprdStd.Col = ColWearLimit
                SprdStd.Text = Trim(IIf(IsDbNull(.Fields("WEAR_LIMIT").Value), "", .Fields("WEAR_LIMIT").Value))

                SprdStd.Col = ColInspMth
                SprdStd.Text = Trim(IIf(IsDbNull(.Fields("INSP_MTH").Value), "", .Fields("INSP_MTH").Value))

                .MoveNext()
                i = i + 1
                SprdStd.MaxRows = i
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(DocNo)  " & vbCrLf & " FROM QAL_GAUGEFIX_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    mAutoGen = .Fields(0).Value
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenKeyNo = mAutoGen
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
        Dim mStatus As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)
        mStatus = IIf(optStatus(0).Checked = True, "O", "C")

        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_GAUGEFIX_MST " & vbCrLf _
                            & " (COMPANY_CODE,FYEAR,DocNo,Model," & vbCrLf _
                            & " Description,COMPONENT_DESC,Customer,Type,TypeNo," & vbCrLf _
                            & " VDoneOn,Location,DrgNo,ReqGoSize,ReqNoGoSize, " & vbCrLf _
                            & " CompSize,WearSize,ValFrequency,VDueOn,IssueDate, " & vbCrLf _
                            & " STATUS, CHECK_QTY," & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE,REMARKS,PartName) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & "," & mSlipNo & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtModel.Text) & "','" & MainClass.AllowSingleQuote(txtDescription.Text) & "', '" & MainClass.AllowSingleQuote(txtComponent.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCustomer.Text) & "','" & MainClass.AllowSingleQuote(cboType.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtTypeNo.Text) & "',TO_DATE('" & vb6.Format(txtVDoneOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtLocation.Text) & "','" & MainClass.AllowSingleQuote(txtDRGNo.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtGoSize.Text) & "','" & MainClass.AllowSingleQuote(txtNogoSize.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtSize.Text) & "','" & MainClass.AllowSingleQuote(txtWearSize.Text) & "', " & vbCrLf _
                            & " " & Val(txtValFrequency.Text) & ",TO_DATE('" & vb6.Format(txtVDueOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtIssueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & mStatus & "', " & vb6.Format(Val(txtQty.Text), "0") & "," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & MainClass.AllowSingleQuote(txtPartName.Text) & "')"


        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_GAUGEFIX_MST SET " & vbCrLf & " DocNo=" & mSlipNo & ",Model='" & MainClass.AllowSingleQuote(txtModel.Text) & "'," & vbCrLf & " Description='" & MainClass.AllowSingleQuote(txtDescription.Text) & "', " & vbCrLf & " COMPONENT_DESC='" & MainClass.AllowSingleQuote(txtComponent.Text) & "', " & vbCrLf & " Customer='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf & " Type='" & MainClass.AllowSingleQuote(cboType.Text) & "', " & vbCrLf & " TypeNo='" & MainClass.AllowSingleQuote(txtTypeNo.Text) & "'," & vbCrLf & " VDoneOn=TO_DATE('" & VB6.Format(txtVDoneOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " Location='" & MainClass.AllowSingleQuote(txtLocation.Text) & "', " & vbCrLf & " DrgNo='" & MainClass.AllowSingleQuote(txtDRGNo.Text) & "', PartName='" & MainClass.AllowSingleQuote(txtPartName.Text) & "'," & vbCrLf & " ReqGoSize='" & MainClass.AllowSingleQuote(txtGoSize.Text) & "', " & vbCrLf & " ReqNoGoSize='" & MainClass.AllowSingleQuote(txtNogoSize.Text) & "', " & vbCrLf & " CompSize='" & MainClass.AllowSingleQuote(txtSize.Text) & "', " & vbCrLf & " WearSize='" & MainClass.AllowSingleQuote(txtWearSize.Text) & "', " & vbCrLf & " ValFrequency=" & Val(txtValFrequency.Text) & ",VDueOn=TO_DATE('" & VB6.Format(txtVDueOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " IssueDate=TO_DATE('" & VB6.Format(txtIssueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " STATUS='" & mStatus & "', CHECK_QTY=" & VB6.Format(Val(txtQty.Text), "0") & "," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DocNo =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateStd = False Then GoTo ErrPart
        '    If ADDMode = True Then
        If mStatus = "O" Then
            If UpdateSchedule = False Then GoTo ErrPart
        End If
        '    End If
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsGaugeFix.Requery()
        RsGaugeInspStd.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function UpdateStd() As Boolean

        On Error GoTo UpdateStdERR
        Dim SqlStr As String
        Dim i As Integer
        Dim mParamDesc As String
        Dim mSpecification As Double
        Dim mSpecPlus As Double
        Dim mSpecMinus As Double
        Dim mWearLimit As Double
        Dim mInspMth As String

        PubDBCn.Execute("DELETE FROM QAL_GAUGE_CALIB_STD WHERE DocNo=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ")

        If Not (cboType.Text = "GAUGE" Or cboType.Text = "PGG") Then
            With SprdStd
                For i = 1 To .MaxRows
                    .Row = i

                    .Col = ColParamDesc
                    mParamDesc = MainClass.AllowSingleQuote(Trim(.Text))

                    .Col = ColSpecification
                    mSpecification = Val(.Text)

                    .Col = ColSpecPlus
                    mSpecPlus = Val(.Text)

                    .Col = ColSpecMinus
                    mSpecMinus = Val(.Text)

                    .Col = ColWearLimit
                    mWearLimit = Val(.Text)

                    .Col = ColInspMth
                    mInspMth = MainClass.AllowSingleQuote(Trim(.Text))

                    SqlStr = ""
                    If mParamDesc <> "" Then
                        SqlStr = " INSERT INTO  QAL_GAUGE_CALIB_STD ( " & vbCrLf & " COMPANY_CODE,DOCNO,SERIAL_NO,PARAM_DESC, " & vbCrLf & " SPECIFICATION,SPEC_PLUS,SPEC_MINUS,WEAR_LIMIT,INSP_MTH ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & "," & i & "," & vbCrLf & " '" & mParamDesc & "'," & mSpecification & "," & vbCrLf & " " & mSpecPlus & "," & mSpecMinus & "," & mWearLimit & ",'" & mInspMth & "')"
                        PubDBCn.Execute(SqlStr)
                    End If
                Next
            End With
        End If

        UpdateStd = True
        Exit Function
UpdateStdERR:
        UpdateStd = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateSchedule() As Boolean

        On Error GoTo UpdateStdERR
        Dim mNextDue As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mScheduleMonth As Integer
        Dim mScheduleYear As Integer
        Dim mSchdNo As Double

        Dim RsTempDet As ADODB.Recordset

        mScheduleMonth = Month(CDate(VB6.Format(txtVDoneOn.Text, "DD/MM/YYYY")))
        mScheduleYear = Year(CDate(VB6.Format(txtVDoneOn.Text, "DD/MM/YYYY")))

        mSqlStr = " SELECT AUTO_KEY_SCHD FROM QAL_IMTE_SCHD_HDR " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND DOC_TYPE='G'" & vbCrLf & " AND SCHD_MONTH=" & mScheduleMonth & "" & vbCrLf & " AND SCHD_YEAR=" & mScheduleYear & ""

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mSchdNo = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_SCHD").Value), -1, RsTemp.Fields("AUTO_KEY_SCHD").Value)
            mNextDue = "" ''DateAdd("m", Val(txtValFrequency.Text), Format(txtVDoneOn.Text, "DD/MM/YYYY"))

            mSqlStr = " SELECT AUTO_KEY_SCHD FROM QAL_IMTE_SCHD_DET " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND DOC_TYPE='G'" & vbCrLf & " AND AUTO_KEY_SCHD=" & mSchdNo & " AND DOCNO='" & txtNumber.Text & "'"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTempDet.EOF = True Then
                SqlStr = " INSERT INTO QAL_IMTE_SCHD_DET ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_SCHD,DOCNO,CHECK_TYPE,RESPONSIBILITY, " & vbCrLf & " REMARKS,PM_DUE,PM_DONE,NOT_ACH_REASON,NEXT_DUE,DOC_TYPE) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(CStr(mSchdNo)) & ",'" & txtNumber.Text & "','PM', " & vbCrLf & " '','', " & vbCrLf & " TO_DATE('" & VB6.Format(txtVDoneOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'', " & vbCrLf & " '',TO_DATE('" & VB6.Format(mNextDue, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'G') "

                PubDBCn.Execute(SqlStr)
            End If
        End If

        UpdateSchedule = True
        Exit Function
UpdateStdERR:
        UpdateSchedule = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtModel.Text) = "" Then
            MsgInformation("Model is empty, So unable to Save")
            txtModel.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDescription.Text) = "" Then
            MsgInformation("Description is empty, So unable to Save")
            txtDescription.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtComponent.Text) = "" Then
            MsgInformation("Component Name is empty, So unable to Save")
            txtComponent.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCustomer.Text) = "" Then
            MsgInformation("Customer is empty, So unable to Save")
            txtCustomer.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(cboType.Text) = "" Then
            MsgInformation("Type is empty, So unable to Save")
            cboType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtTypeNo.Text) = "" Then
            MsgInformation("Type No is empty, So unable to Save")
            txtTypeNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboType.Text = "GAUGE" Or cboType.Text = "PGG" Then
            If Trim(txtGoSize.Text) = "" Then
                MsgInformation("Go Size is empty, So unable to Save")
                txtGoSize.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtNogoSize.Text) = "" Then
                MsgInformation("NoGo Size is empty, So unable to Save")
                txtNogoSize.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtSize.Text) = "" Then
                MsgInformation("Size is empty, So unable to Save")
                txtSize.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtWearSize.Text) = "" Then
                MsgInformation("Wear Size is empty, So unable to Save")
                txtWearSize.Focus()
                FieldsVarification = False
                Exit Function
            End If
        Else
            If MainClass.ValidDataInGrid(SprdStd, ColParamDesc, "S", "Please Check Parameter Description.") = False Then FieldsVarification = False
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Machine Details or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsGaugeFix.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = " SELECT DocNo,Model,Description, " & vbCrLf & " Customer,Type,TypeNo,TO_CHAR(VDoneOn,'DD/MM/YYYY') AS VDoneOn, " & vbCrLf & " Location,ValFrequency,TO_CHAR(VDueOn,'DD/MM/YYYY') AS VDueOn,TO_CHAR(IssueDate,'DD/MM/YYYY') AS IssueDate " & vbCrLf & " FROM QAL_GAUGEFIX_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY DocNo"
        Call MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()

    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "Gauge Fixture Master"


        If cboType.Text = "GAUGE" Or cboType.Text = "PGG" Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\GaugeFixMst1.rpt"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\GaugeFixMst.rpt"
        End If
        SqlStr = " SELECT QAL_GAUGEFIX_MST.*, QAL_GAUGE_CALIB_STD.* " & vbCrLf & " FROM QAL_GAUGEFIX_MST, QAL_GAUGE_CALIB_STD " & vbCrLf & " WHERE QAL_GAUGEFIX_MST.COMPANY_CODE= QAL_GAUGE_CALIB_STD.COMPANY_CODE(+) " & vbCrLf & " AND QAL_GAUGEFIX_MST.DocNo= QAL_GAUGE_CALIB_STD.DocNo(+) " & vbCrLf & " AND QAL_GAUGEFIX_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND QAL_GAUGEFIX_MST.DocNo=" & Val(lblMkey.Text) & " "

        Report1.SQLQuery = SqlStr
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtComponent_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtComponent.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtComponent_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtComponent.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtComponent.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
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

    Private Sub txtDRGNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDRGNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGoSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGoSize.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIssueDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssueDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIssueDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIssueDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtIssueDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtModel_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModel.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDescription_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDescription.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtModel_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtModel.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtModel.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNogoSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNogoSize.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPartName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartName.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPartName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQty.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

    Private Sub txtSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSize.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTypeNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTypeNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTypeNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTypeNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTypeNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTypeNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTypeNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ShowRecord(False) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtValFrequency_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtValFrequency.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtValFrequency_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtValFrequency.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtValFrequency_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtValFrequency.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcVDueOn()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVDoneOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDoneOn.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVDoneOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDoneOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtVDoneOn) = False Then Cancel = True : GoTo EventExitSub
        Call CalcVDueOn()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVDueOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDueOn.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVDueOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDueOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtVDueOn) = False Then Cancel = True
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
        If ShowRecord(True) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub
    Private Function ShowRecord(ByRef pFromDoc As Boolean) As Boolean

        On Error GoTo ERR1
        Dim xMKey As Double
        ShowRecord = True
        If pFromDoc = True Then
            If Trim(txtNumber.Text) = "" Then Exit Function
        Else
            If Trim(txtTypeNo.Text) = "" Then Exit Function
        End If
        If MODIFYMode = True And RsGaugeFix.EOF = False Then xMKey = RsGaugeFix.Fields("DOCNO").Value
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_GAUGEFIX_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If pFromDoc = True Then
            SqlStr = SqlStr & vbCrLf & " AND DocNo=" & Val(txtNumber.Text) & " "
        Else
            SqlStr = SqlStr & vbCrLf & " AND UPPER(TRIM(TypeNo))='" & MainClass.AllowSingleQuote(UCase(txtTypeNo.Text)) & "' "
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeFix, ADODB.LockTypeEnum.adLockReadOnly)
        If RsGaugeFix.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            xMKey = RsGaugeFix.Fields("DOCNO").Value
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Such Number Does Not Exist" & vbCrLf & "Click Add To Add for New.", MsgBoxStyle.Information)
                ShowRecord = False
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * " & vbCrLf & " FROM QAL_GAUGEFIX_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DocNo=" & xMKey & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeFix, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtNumber.Maxlength = RsGaugeFix.Fields("DocNo").DefinedSize
        txtModel.Maxlength = RsGaugeFix.Fields("Model").DefinedSize
        txtDescription.Maxlength = RsGaugeFix.Fields("Description").DefinedSize
        txtComponent.Maxlength = RsGaugeFix.Fields("COMPONENT_DESC").DefinedSize
        txtCustomer.Maxlength = RsGaugeFix.Fields("Customer").DefinedSize
        txtTypeNo.Maxlength = RsGaugeFix.Fields("TypeNo").DefinedSize
        txtVDoneOn.Maxlength = RsGaugeFix.Fields("VDoneOn").DefinedSize - 6
        txtLocation.Maxlength = RsGaugeFix.Fields("Location").DefinedSize
        txtDRGNo.Maxlength = RsGaugeFix.Fields("DrgNo").DefinedSize
        txtGoSize.Maxlength = RsGaugeFix.Fields("ReqGoSize").DefinedSize
        txtNogoSize.Maxlength = RsGaugeFix.Fields("ReqNoGoSize").DefinedSize
        txtSize.Maxlength = RsGaugeFix.Fields("CompSize").DefinedSize
        txtRemarks.Maxlength = RsGaugeFix.Fields("REMARKS").DefinedSize
        txtWearSize.Maxlength = RsGaugeFix.Fields("WearSize").DefinedSize
        txtValFrequency.Maxlength = RsGaugeFix.Fields("ValFrequency").DefinedSize
        txtVDueOn.Maxlength = RsGaugeFix.Fields("VDueOn").DefinedSize - 6
        txtIssueDate.Maxlength = RsGaugeFix.Fields("IssueDate").DefinedSize - 6
        txtQty.Maxlength = RsGaugeFix.Fields("CHECK_QTY").Precision
        txtPartName.Maxlength = RsGaugeFix.Fields("PARTNAME").DefinedSize
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
            .set_ColWidth(3, 500 * 5)
            .set_ColWidth(4, 500 * 5)
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

    Private Sub txtWearSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWearSize.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
End Class
