Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmIMTEMst
    Inherits System.Windows.Forms.Form
    Dim RsIMTE As ADODB.Recordset
    Dim RsIssueDetail As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim PvtDBCn As ADODB.Connection
    Dim FormActive As Boolean
    Dim SqlStr As String

    Private Const ConRowHeight As Short = 15

    Private Const ColIssueDate As Short = 1
    Private Const ColIssueFrom As Short = 2
    Private Const ColIssueTo As Short = 3
    Private Const ColReceivedDate As Short = 4
    Private Const ColReceivedName As Short = 5
    Private Const ColRemarks As Short = 6

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1)
            .set_RowHeight(0, ConRowHeight * 1.5)
            '        .RowHeight(Arow) = ConRowHeight * 1.5

            .Col = ColIssueDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColIssueDate, 8)

            .Col = ColIssueFrom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsIssueDetail.Fields("ISSUE_EMP").DefinedSize
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColIssueFrom, 12)

            .Col = ColIssueTo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsIssueDetail.Fields("ISSUE_TO").DefinedSize
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColIssueTo, 12)

            .Col = ColReceivedDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColReceivedDate, 8)

            .Col = ColReceivedName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsIssueDetail.Fields("RECD_EMP").DefinedSize
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColReceivedName, 12)


            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsIssueDetail.Fields("REMARKS").DefinedSize
            .set_ColWidth(ColRemarks, 11)

            '        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColStock

        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume
    End Sub
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
        MainClass.ButtonStatus(Me, XRIGHT, RsIMTE, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        lblMkey.Text = ""
        txtNumber.Text = ""
        txtItemCode.Text = ""
        TxtItemName.Text = ""
        txtDescription.Text = ""
        txtENo.Text = ""
        txtMarkersNo.Text = ""
        txtLC.Text = ""
        txtMake.Text = ""
        txtRange.Text = ""
        txtLocation.Text = ""
        txtValFrequency.Text = ""
        txtLCDate.Text = ""
        cboCaliFacil.SelectedIndex = -1
        cboType.SelectedIndex = -1
        chkMasterInst.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtIssueDate.Text = ""
        txtCDate.Text = ""
        txtIssueTo.Text = ""
        txtMinRange.Text = ""
        txtMaxRange.Text = ""
        txtUnitRange.Text = ""
        txtGoSize.Text = ""
        txtNogoSize.Text = ""
        txtBasicSize.Text = ""
        txtWearSize.Text = ""
        txtSuppCustCode.Text = ""
        txtSuppCustName.Text = ""

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsIMTE, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

    Private Sub cboCaliFacil_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCaliFacil.SelectedIndexChanged
        cboCaliFacil_TextChanged(cboCaliFacil, New System.EventArgs())
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.SelectedIndexChanged
        If cboType.Text = "VARIABLE" Then
            fraSize.Enabled = False
            fraSize.Visible = False
            fraRange.Visible = True
            fraRange.Enabled = True
        Else
            fraRange.Visible = False
            fraRange.Enabled = False
            fraSize.Visible = True
            fraSize.Enabled = True
        End If
    End Sub

    Private Sub chkMasterInst_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMasterInst.CheckStateChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        If chkMasterInst.CheckState = System.Windows.Forms.CheckState.Checked Then
            fraMaster.Enabled = True
        Else
            fraMaster.Enabled = False
        End If
    End Sub

    Private Sub cmdItemCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdItemCode.Click
        Call SearchCode()
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsIMTE, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
            txtNumber.Enabled = True
            cmdSearchNumber.Enabled = True
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

    Private Sub cmdSearchNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNumber.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "QAL_IMTE_MST", "DocNo", "ITEMCODE", "Description", "MAKE_NAME", SqlStr) = True Then
            txtNumber.Text = AcName
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub cmdSearchSuppCustCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSuppCustCode.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtSuppCustCode.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtSuppCustCode.Text = AcName1
            txtSuppCustName.Text = AcName
            If txtSuppCustCode.Enabled = True Then txtSuppCustCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        Else
            ADDMode = False
            MODIFYMode = False
            If RsIMTE.EOF = False Then RsIMTE.MoveFirst()
            Show1()
            txtNumber.Enabled = True
            cmdSearchNumber.Enabled = True
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
        If Not RsIMTE.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_IMTE_MST", (txtNumber.Text), RsIMTE) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM QAL_IMTE_SCHD_DET WHERE DocNo=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DOC_TYPE='I'")

                PubDBCn.Execute("DELETE FROM QAL_IMTE_ISS_DET WHERE DocNo=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ")
                PubDBCn.Execute("DELETE FROM QAL_IMTE_MST WHERE DocNo=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ")
                PubDBCn.CommitTrans()
                RsIMTE.Requery()
                RsIssueDetail.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsIMTE.Requery()
        RsIssueDetail.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub frmIMTEMst_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmIMTEMst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
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

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim mIssueDate As String
        Dim mRecdDate As String

        Dim DelStatus As Boolean
        If eventArgs.col = 0 And eventArgs.row > 0 Then


            SprdMain.Row = eventArgs.row

            SprdMain.Col = ColIssueDate
            mIssueDate = SprdMain.Text

            SprdMain.Col = ColReceivedDate
            mRecdDate = SprdMain.Text

            If (mIssueDate = "" And mRecdDate = "") Or PubSuperUser = "S" Or PubSuperUser = "A" Then
                If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                    MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColIssueDate, DelStatus)
                    MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
                End If
            End If
        End If
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ERR1
        Dim mIssueDate As String
        Dim cntRow As Integer
        Dim mReceivedDate As String

        If eventArgs.NewRow = -1 Then Exit Sub

        With SprdMain
            cntRow = .ActiveRow
            .Row = .ActiveRow
            .Col = ColIssueDate
            If Trim(.Text) = "" Then Exit Sub
            Select Case eventArgs.Col
                Case ColIssueDate
                    .Row = .ActiveRow
                    .Col = ColIssueDate
                    mIssueDate = VB6.Format(.Text, "DD/MM/YYYY")

                    If mIssueDate <> "" Then
                        If Not IsDate(mIssueDate) Then
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColIssueDate)
                        End If
                        If cntRow > 1 Then
                            .Row = cntRow - 1
                            .Col = ColReceivedDate
                            mReceivedDate = VB6.Format(.Text, "DD/MM/YYYY")
                            If mReceivedDate = "" Then
                                MainClass.SetFocusToCell(SprdMain, cntRow, ColIssueDate, "Receipt is Pending")
                            End If
                        End If
                    End If

                    MainClass.AddBlankSprdRow(SprdMain, ColIssueDate, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                Case ColReceivedDate
                    .Row = .ActiveRow
                    .Col = ColReceivedDate
                    mReceivedDate = VB6.Format(.Text, "DD/MM/YYYY")

                    If mReceivedDate <> "" Then
                        If Not IsDate(mReceivedDate) Then
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColReceivedDate)
                        End If
                    End If
            End Select
        End With
        '    MainClass.SaveStatus Me, ADDMode, MODIFYMode
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume
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

    Public Sub frmIMTEMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "IMTE Master"
        SqlStr = " Select * From QAL_IMTE_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTE, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from QAL_IMTE_ISS_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIssueDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

    Private Sub frmIMTEMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        'Me.Height = VB6.TwipsToPixelsY(8460)
        'Me.Width = VB6.TwipsToPixelsX(8565)

        cboCaliFacil.Items.Clear()
        cboCaliFacil.Items.Add("OUTSIDE")
        cboCaliFacil.Items.Add("INSIDE")
        cboCaliFacil.Items.Add("CUSTOMER")
        cboCaliFacil.SelectedIndex = 0

        cboType.Items.Clear()
        cboType.Items.Add("VARIABLE")
        cboType.Items.Add("ATTRIBUTE")
        cboType.SelectedIndex = 0

        chkMasterInst.CheckState = System.Windows.Forms.CheckState.Unchecked

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmIMTEMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsIMTE.Close()
        RsIMTE = Nothing

        RsIssueDetail.Close()
        RsIssueDetail = Nothing

        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        If Not RsIMTE.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsIMTE.Fields("DOCNO").Value), "", RsIMTE.Fields("DOCNO").Value)
            txtNumber.Text = IIf(IsDbNull(RsIMTE.Fields("DOCNO").Value), "", RsIMTE.Fields("DOCNO").Value)
            txtItemCode.Text = IIf(IsDbNull(RsIMTE.Fields("ITEMCODE").Value), "", RsIMTE.Fields("ITEMCODE").Value)

            If MainClass.ValidateWithMasterTable(Trim(txtItemCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                txtItemName.Text = MasterNo
            End If

            txtDescription.Text = IIf(IsDbNull(RsIMTE.Fields("Description").Value), "", RsIMTE.Fields("Description").Value)
            txtENo.Text = IIf(IsDbNull(RsIMTE.Fields("E_NO").Value), "", RsIMTE.Fields("E_NO").Value)
            txtMarkersNo.Text = IIf(IsDbNull(RsIMTE.Fields("Markers_No").Value), "", RsIMTE.Fields("Markers_No").Value)
            txtLC.Text = IIf(IsDbNull(RsIMTE.Fields("L_C").Value), "", RsIMTE.Fields("L_C").Value)
            txtMake.Text = IIf(IsDbNull(RsIMTE.Fields("Make_Name").Value), "", RsIMTE.Fields("Make_Name").Value)
            txtRange.Text = IIf(IsDbNull(RsIMTE.Fields("Range").Value), "", RsIMTE.Fields("Range").Value)
            txtLocation.Text = IIf(IsDbNull(RsIMTE.Fields("Location").Value), "", RsIMTE.Fields("Location").Value)
            txtValFrequency.Text = IIf(IsDbNull(RsIMTE.Fields("ValFrequency").Value), "", RsIMTE.Fields("ValFrequency").Value)
            txtLCDate.Text = IIf(IsDbNull(RsIMTE.Fields("LCDATE").Value), "", RsIMTE.Fields("LCDATE").Value)
            cboCaliFacil.Text = IIf(IsDbNull(RsIMTE.Fields("CALIFACIL").Value), "", RsIMTE.Fields("CALIFACIL").Value)
            cboType.Text = IIf(IsDbNull(RsIMTE.Fields("Type").Value), "", RsIMTE.Fields("Type").Value)
            txtIssueDate.Text = IIf(IsDbNull(RsIMTE.Fields("ISSUEDATE").Value), "", RsIMTE.Fields("ISSUEDATE").Value)
            txtCDate.Text = IIf(IsDbNull(RsIMTE.Fields("CDate").Value), "", RsIMTE.Fields("CDate").Value)
            txtIssueTo.Text = IIf(IsDbNull(RsIMTE.Fields("ISSUETO").Value), "", RsIMTE.Fields("ISSUETO").Value)
            txtMinRange.Text = IIf(IsDbNull(RsIMTE.Fields("Min_Range").Value), "", RsIMTE.Fields("Min_Range").Value)
            txtMaxRange.Text = IIf(IsDbNull(RsIMTE.Fields("Max_Range").Value), "", RsIMTE.Fields("Max_Range").Value)
            txtUnitRange.Text = IIf(IsDbNull(RsIMTE.Fields("Unit_Range").Value), "", RsIMTE.Fields("Unit_Range").Value)
            txtGoSize.Text = IIf(IsDbNull(RsIMTE.Fields("GOSIZE").Value), "", RsIMTE.Fields("GOSIZE").Value)
            txtNogoSize.Text = IIf(IsDbNull(RsIMTE.Fields("NOGOSIZE").Value), "", RsIMTE.Fields("NOGOSIZE").Value)
            txtBasicSize.Text = IIf(IsDbNull(RsIMTE.Fields("BASICSIZE").Value), "", RsIMTE.Fields("BASICSIZE").Value)
            txtWearSize.Text = IIf(IsDbNull(RsIMTE.Fields("WearSize").Value), "", RsIMTE.Fields("WearSize").Value)
            txtModel.Text = IIf(IsDbNull(RsIMTE.Fields("Model").Value), "", RsIMTE.Fields("Model").Value)
            txtCalibBy.Text = IIf(IsDbNull(RsIMTE.Fields("CALIB_BY").Value), "", RsIMTE.Fields("CALIB_BY").Value)
            txtCertNo.Text = IIf(IsDbNull(RsIMTE.Fields("CERT_NO").Value), "", RsIMTE.Fields("CERT_NO").Value)
            txtCalibValid.Text = IIf(IsDbNull(RsIMTE.Fields("Calib_Valid").Value), "", RsIMTE.Fields("Calib_Valid").Value)
            txtSuppCustCode.Text = IIf(IsDbNull(RsIMTE.Fields("SUPP_CUST_CODE").Value), "", RsIMTE.Fields("SUPP_CUST_CODE").Value)
            txtSuppCustCode_Validating(txtSuppCustCode, New System.ComponentModel.CancelEventArgs((False)))

            If RsIMTE.Fields("Status").Value = "O" Then
                optStatus(0).Checked = True
            Else
                optStatus(1).Checked = True
            End If

            If RsIMTE.Fields("CALIB_OK").Value = "Y" Then
                txtCalibOK.Text = "Last Calibration OK"
            ElseIf RsIMTE.Fields("CALIB_OK").Value = "N" Then
                txtCalibOK.Text = "Last Calibration Not OK"
            ElseIf RsIMTE.Fields("CALIB_OK").Value = "R" Then
                txtCalibOK.Text = "Instrument Repaired"
            End If

            If RsIMTE.Fields("MASTER_INST").Value = "Y" Then
                chkMasterInst.CheckState = System.Windows.Forms.CheckState.Checked
            Else
                chkMasterInst.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If

            Call ShowIssueDetail1(Val(lblMkey.Text))

        End If

        txtNumber.Enabled = True
        cmdSearchNumber.Enabled = True

        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsIMTE, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowIssueDetail1(ByRef mDocNo As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim mRecdDate As String
        Dim mIssueDate As String

        SqlStr = ""
        MainClass.ClearGrid(SprdMain)
        SqlStr = "SELECT * " & vbCrLf & " FROM QAL_IMTE_ISS_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND DocNo=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIssueDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsIssueDetail

            If .EOF = True Then Exit Sub
            I = 0
            .MoveFirst()
            Do While Not .EOF
                I = I + 1
                SprdMain.MaxRows = SprdMain.MaxRows + 1
                SprdMain.Row = I

                SprdMain.Col = ColIssueDate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("ISSUE_DATE").Value), "", .Fields("ISSUE_DATE").Value), "DD/MM/YYYY")
                mIssueDate = VB6.Format(IIf(IsDbNull(.Fields("ISSUE_DATE").Value), "", .Fields("ISSUE_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColIssueFrom
                SprdMain.Text = IIf(IsDbNull(.Fields("ISSUE_EMP").Value), "", .Fields("ISSUE_EMP").Value)

                SprdMain.Col = ColIssueTo
                SprdMain.Text = IIf(IsDbNull(.Fields("ISSUE_TO").Value), "", .Fields("ISSUE_TO").Value)

                SprdMain.Col = ColReceivedDate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("RECD_DATE").Value), "", .Fields("RECD_DATE").Value), "DD/MM/YYYY")
                mRecdDate = VB6.Format(IIf(IsDbNull(.Fields("RECD_DATE").Value), "", .Fields("RECD_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColReceivedName
                SprdMain.Text = IIf(IsDbNull(.Fields("RECD_EMP").Value), "", .Fields("RECD_EMP").Value)

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                If mRecdDate <> "" Then
                    MainClass.ProtectCell(SprdMain, 1, I, ColIssueDate, ColRemarks)
                Else
                    If PubSuperUser = "U" Then
                        MainClass.ProtectCell(SprdMain, 1, I, ColIssueDate, ColIssueTo)
                    End If
                End If

                .MoveNext()
            Loop

        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
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
        SqlStr = "SELECT Max(DocNo)  " & vbCrLf & " FROM QAL_IMTE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

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
        Dim mMaster As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)
        mStatus = IIf(optStatus(0).Checked = True, "O", "C")
        mMaster = IIf(chkMasterInst.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If SprdMain.MaxRows > 1 Then
            SprdMain.Row = SprdMain.MaxRows - 1
            SprdMain.Col = ColIssueTo
            txtIssueTo.Text = Trim(SprdMain.Text)

            SprdMain.Col = ColIssueDate
            txtIssueDate.Text = VB6.Format(SprdMain.Text, "DD/MM/YYYY")
        Else
            txtIssueDate.Text = ""
            txtIssueTo.Text = ""
        End If

        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_IMTE_MST (" & vbCrLf & " COMPANY_CODE, FYEAR, " & vbCrLf & " DOCNO, ITEMCODE, " & vbCrLf & " DESCRIPTION, E_NO, " & vbCrLf & " MARKERS_NO, L_C, " & vbCrLf & " MAKE_NAME, RANGE, " & vbCrLf & " LOCATION, VALFREQUENCY, " & vbCrLf & " LCDATE, CALIFACIL, TYPE, " & vbCrLf & " ISSUEDATE, CDATE, " & vbCrLf & " ISSUETO, STATUS, " & vbCrLf & " MIN_RANGE, MAX_RANGE, UNIT_RANGE, " & vbCrLf & " GOSIZE, NOGOSIZE, " & vbCrLf & " BASICSIZE, WEARSIZE, " & vbCrLf & " MASTER_INST, MODEL, CALIB_BY, " & vbCrLf & " CERT_NO, CALIB_VALID, " & vbCrLf & " SUPP_CUST_CODE, " & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE "

            SqlStr = SqlStr & vbCrLf _
                            & " ) VALUES ( " & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                            & " " & mSlipNo & ", '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDescription.Text) & "', '" & MainClass.AllowSingleQuote(txtENO.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMarkersNo.Text) & "','" & MainClass.AllowSingleQuote(txtLC.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMake.Text) & "', '" & MainClass.AllowSingleQuote(txtRange.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtLocation.Text) & "'," & Val(txtValFrequency.Text) & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtLCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(cboCaliFacil.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(cboType.Text) & "', " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtIssueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtIssueTo.Text) & "', '" & mStatus & "', " & vbCrLf _
                            & "  " & Val(txtMinRange.Text) & "," & Val(txtMaxRange.Text) & ",'" & MainClass.AllowSingleQuote(txtUnitRange.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtGoSize.Text) & "', '" & MainClass.AllowSingleQuote(txtNoGoSize.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtBasicSize.Text) & "', '" & MainClass.AllowSingleQuote(txtWearSize.Text) & "', " & vbCrLf _
                            & " '" & mMaster & "', '" & MainClass.AllowSingleQuote(txtModel.Text) & "', '" & MainClass.AllowSingleQuote(txtCalibBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCertNo.Text) & "', TO_DATE('" & vb6.Format(txtCalibValid.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"


        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_IMTE_MST SET " & vbCrLf & " DocNo=" & mSlipNo & ", " & vbCrLf & " ITEMCODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & " DESCRIPTION='" & MainClass.AllowSingleQuote(txtDescription.Text) & "', " & vbCrLf & " E_NO='" & MainClass.AllowSingleQuote(txtENO.Text) & "', " & " MARKERS_NO='" & MainClass.AllowSingleQuote(txtMarkersNo.Text) & "', " & vbCrLf & " L_C='" & MainClass.AllowSingleQuote(txtLC.Text) & "', " & " MAKE_NAME='" & MainClass.AllowSingleQuote(txtMake.Text) & "', " & vbCrLf & " RANGE='" & MainClass.AllowSingleQuote(txtRange.Text) & "', " & " LOCATION='" & MainClass.AllowSingleQuote(txtLocation.Text) & "', " & vbCrLf & " VALFREQUENCY=" & Val(txtValFrequency.Text) & ", " & " LCDATE=TO_DATE('" & VB6.Format(txtLCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " CALIFACIL='" & MainClass.AllowSingleQuote(cboCaliFacil.Text) & "', " & " TYPE='" & MainClass.AllowSingleQuote(cboType.Text) & "', " & vbCrLf & " ISSUEDATE=TO_DATE('" & VB6.Format(txtIssueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & " CDATE=TO_DATE('" & VB6.Format(txtCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ISSUETO='" & MainClass.AllowSingleQuote(txtIssueTo.Text) & "', " & " STATUS='" & mStatus & "'," & vbCrLf & " MIN_RANGE=" & Val(txtMinRange.Text) & ", " & " MAX_RANGE=" & Val(txtMaxRange.Text) & ", " & " UNIT_RANGE='" & MainClass.AllowSingleQuote(txtUnitRange.Text) & "', " & vbCrLf & " GOSIZE='" & MainClass.AllowSingleQuote(txtGoSize.Text) & "', " & " NOGOSIZE='" & MainClass.AllowSingleQuote(txtNoGoSize.Text) & "', " & vbCrLf & " BASICSIZE='" & MainClass.AllowSingleQuote(txtBasicSize.Text) & "', " & " WEARSIZE='" & MainClass.AllowSingleQuote(txtWearSize.Text) & "', " & vbCrLf & " MASTER_INST='" & mMaster & "', " & " MODEL='" & MainClass.AllowSingleQuote(txtModel.Text) & "', " & " CALIB_BY='" & MainClass.AllowSingleQuote(txtCalibBy.Text) & "', " & vbCrLf & " CERT_NO='" & MainClass.AllowSingleQuote(txtCertNo.Text) & "', " & " CALIB_VALID=TO_DATE('" & VB6.Format(txtCalibValid.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DocNo =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True

        If UpdateIssueDetail1(mSlipNo) = False Then GoTo ErrPart

        '    If ADDMode = True Then
        If mStatus = "O" Then
            If UpdateSchedule() = False Then GoTo ErrPart
        End If
        'endif
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsIMTE.Requery()
        RsIssueDetail.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function UpdateIssueDetail1(ByRef pDocNo As Double) As Boolean
        On Error GoTo UpdateDetail1Err
        Dim I As Short
        Dim mRow As Short
        Dim mIssueDate As String
        Dim mIssueFrom As String
        Dim mIssueTo As String
        Dim mReceivedDate As String
        Dim mReceivedName As String
        Dim mRemarks As String


        SqlStr = "DELETE FROM QAL_IMTE_ISS_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND DocNo=" & Val(lblMkey.Text) & ""
        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColIssueDate
                mIssueDate = Trim(.Text)

                .Col = ColIssueFrom
                mIssueFrom = Trim(.Text)

                .Col = ColIssueTo
                mIssueTo = Trim(.Text)

                .Col = ColReceivedDate
                mReceivedDate = Trim(.Text)

                .Col = ColReceivedName
                mReceivedName = Trim(.Text)

                .Col = ColRemarks
                mRemarks = Trim(.Text)

                SqlStr = ""

                If mIssueDate <> "" Then
                    SqlStr = " INSERT INTO QAL_IMTE_ISS_DET ( " & vbCrLf & " COMPANY_CODE, DocNo, SERIAL_NO, " & vbCrLf & " ISSUE_DATE, ISSUE_EMP, " & vbCrLf & " ISSUE_TO, RECD_DATE, " & vbCrLf & " RECD_EMP, REMARKS) VALUES ( "

                    SqlStr = SqlStr & vbCrLf _
                                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(pDocNo) & "," & I & ", " & vbCrLf _
                                        & " TO_DATE('" & vb6.Format(mIssueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                                        & " '" & MainClass.AllowSingleQuote(mIssueFrom) & "'," & vbCrLf _
                                        & " '" & MainClass.AllowSingleQuote(mIssueTo) & "', " & vbCrLf _
                                        & " TO_DATE('" & vb6.Format(mReceivedDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                                        & " '" & MainClass.AllowSingleQuote(mReceivedName) & "'," & vbCrLf _
                                        & " '" & MainClass.AllowSingleQuote(mRemarks) & "'" & vbCrLf _
                                        & " )"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateIssueDetail1 = True
        Exit Function
UpdateDetail1Err:
        MsgBox(Err.Description)
        UpdateIssueDetail1 = False
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

        mScheduleMonth = Month(CDate(VB6.Format(txtLCDate.Text, "DD/MM/YYYY")))
        mScheduleYear = Year(CDate(VB6.Format(txtLCDate.Text, "DD/MM/YYYY")))

        mSqlStr = " SELECT AUTO_KEY_SCHD FROM QAL_IMTE_SCHD_HDR " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND DOC_TYPE='I'" & vbCrLf & " AND SCHD_MONTH=" & mScheduleMonth & "" & vbCrLf & " AND SCHD_YEAR=" & mScheduleYear & ""

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mSchdNo = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_SCHD").Value), -1, RsTemp.Fields("AUTO_KEY_SCHD").Value)
            mNextDue = "" ''DateAdd("m", Val(txtValFrequency.Text), Format(txtLCDate.Text, "DD/MM/YYYY"))

            mSqlStr = " SELECT AUTO_KEY_SCHD FROM QAL_IMTE_SCHD_DET " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND DOC_TYPE='I'" & vbCrLf & " AND AUTO_KEY_SCHD=" & mSchdNo & " AND DOCNO='" & txtNumber.Text & "'"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTempDet.EOF = True Then
                SqlStr = " INSERT INTO QAL_IMTE_SCHD_DET ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_SCHD,DOCNO,CHECK_TYPE,RESPONSIBILITY, " & vbCrLf & " REMARKS,PM_DUE,PM_DONE,NOT_ACH_REASON,NEXT_DUE,DOC_TYPE) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(CStr(mSchdNo)) & ",'" & txtNumber.Text & "','PM', " & vbCrLf & " '','', " & vbCrLf & " TO_DATE('" & VB6.Format(txtLCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'', " & vbCrLf & " '',TO_DATE('" & VB6.Format(mNextDue, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'I') "

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
        Dim cntRow As Integer
        Dim mIssueDate As String
        Dim mRecdDate As String
        Dim mIssueFrom As String
        Dim mIssueTo As String
        Dim mReceivedName As String

        FieldsVarification = True

        If Trim(txtItemCode.Text) = "" Then
            MsgInformation("Item Code is empty, So unable to Save")
            txtItemCode.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDescription.Text) = "" Then
            MsgInformation("Description is empty, So unable to Save")
            txtDescription.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtENo.Text) = "" Then
            MsgInformation("E. No. is empty, So unable to Save")
            txtENo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtMake.Text) = "" Then
            MsgInformation("Make is empty, So unable to Save")
            txtMake.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(cboCaliFacil.Text) = "" Then
            MsgInformation("Cali. Facil. is empty, So unable to Save")
            cboCaliFacil.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(cboType.Text) = "" Then
            MsgInformation("Type is empty, So unable to Save")
            cboType.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtLC.Text) = "" Then
            MsgInformation("LC is empty, So unable to Save")
            txtLC.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtLCDate.Text) = "" Then
            MsgInformation("LC Date is empty, So unable to Save")
            txtLCDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Not IsDate(txtLCDate.Text) Then
            MsgInformation("Invalid LC Date, So unable to Save")
            txtLCDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtLocation.Text) = "" Then
            MsgInformation("Location is empty, So unable to Save")
            txtLocation.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtRange.Text) = "" Then
            MsgInformation("Range is empty, So unable to Save")
            txtRange.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Val(txtValFrequency.Text) = 0 Then
            MsgInformation("Frequency is empty, So unable to Save")
            txtValFrequency.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Machine Details or modify an existing item")
            FieldsVarification = False
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColIssueDate
                mIssueDate = VB6.Format(.Text, "DD/MM/YYYY")

                If mIssueDate = "" Then
                    MsgInformation("Issue Date is Blank, So unable to Save")
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColIssueDate)
                    FieldsVarification = False
                    Exit Function
                End If

                If Not IsDate(mIssueDate) Then
                    MsgInformation("Invalid Issue Date is Blank, So unable to Save")
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColIssueDate)
                    FieldsVarification = False
                    Exit Function
                End If

                If cntRow > 1 Then
                    If mRecdDate = "" Then
                        MsgInformation("Not Recieved, So Cann't be Issue.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColIssueDate)
                        FieldsVarification = False
                        Exit Function
                    End If

                    If CDate(mIssueDate) < CDate(mRecdDate) Then
                        MsgInformation("Issue Date cann't be Less Than Previous Recd Date. So unable to Save")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColIssueDate)
                        FieldsVarification = False
                        Exit Function
                    End If

                End If
                .Col = ColIssueFrom
                mIssueFrom = Trim(.Text)

                If mIssueFrom = "" Then
                    MsgInformation("Issue Employee is Blank, So unable to Save")
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColIssueFrom)
                    FieldsVarification = False
                    Exit Function
                End If

                .Col = ColIssueTo
                mIssueTo = Trim(.Text)

                If mIssueTo = "" Then
                    MsgInformation("Issue To is Blank, So unable to Save")
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColIssueTo)
                    FieldsVarification = False
                    Exit Function
                End If

                .Col = ColReceivedDate
                mRecdDate = VB6.Format(.Text, "DD/MM/YYYY")

                If mRecdDate <> "" Then
                    If Not IsDate(mRecdDate) Then
                        MsgInformation("Invalid Recevied Date is Blank, So unable to Save")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColReceivedDate)
                        FieldsVarification = False
                        Exit Function
                    End If

                    If CDate(mRecdDate) < CDate(mIssueDate) Then
                        MsgInformation("Recd Date cann't be Less Than RecdDate. So unable to Save")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColReceivedDate)
                        FieldsVarification = False
                        Exit Function
                    End If

                    .Col = ColReceivedName
                    mReceivedName = Trim(.Text)

                    If mReceivedName = "" Then
                        MsgInformation("Received Employee is Blank, So unable to Save")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColReceivedName)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

            Next
        End With

        If MODIFYMode = True And RsIMTE.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = " SELECT DocNo,ITEMCODE,Description,TYPE, " & vbCrLf & " E_NO,MARKERS_NO,L_C,TO_CHAR(LCDATE,'DD/MM/YYYY') AS LCDATE, " & vbCrLf & " Location,ValFrequency,TO_CHAR(CDATE,'DD/MM/YYYY') AS VDueOn,TO_CHAR(IssueDate,'DD/MM/YYYY') AS IssueDate " & vbCrLf & " FROM QAL_IMTE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY DocNo"

        Call MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()
    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "Gauge Fixture Master"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\IMTEMst.rpt"

        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_IMTE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DocNo=" & Val(lblMkey.Text) & " "

        Report1.SQLQuery = SqlStr
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub cboCaliFacil_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCaliFacil.TextChanged

        On Error GoTo ERR1
        If cboCaliFacil.Text = "CUSTOMER" Then
            txtSuppCustCode.Enabled = True
            cmdSearchSuppCustCode.Enabled = True
        Else
            txtSuppCustCode.Enabled = False
            cmdSearchSuppCustCode.Enabled = False
        End If

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub cboType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBasicSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBasicSize.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBasicSize_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBasicSize.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBasicSize.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCalibBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCalibBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCalibBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCalibBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCalibBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCalibValid_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCalibValid.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtCalibValid) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtCDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCertNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCertNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCertNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCertNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCertNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCalibValid_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCalibValid.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCalibValid_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCalibValid.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCalibValid.Text)
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

    Private Sub txtENO_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtENO.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtENo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtENO.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtENo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGoSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGoSize.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGoSize_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGoSize.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGoSize.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtIssueDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssueDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIssueDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIssueDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtIssueDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDescription_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDescription.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIssueTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssueTo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIssueTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIssueTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIssueTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub

        SqlStr = " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"

        If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Name Does Not Exist In Master.", vbInformation)
            Cancel = True
        Else
            txtItemName.Text = MasterNo
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        Call SearchCode()
    End Sub

    Private Sub txtItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCode()
    End Sub

    Private Sub SearchCode()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtItemCode.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , SqlStr) = True Then
            txtItemCode.Text = AcName
            txtItemCode_Validating(txtItemCode, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
            If txtItemCode.Enabled = True Then txtItemCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtLC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLC.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLC_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLC.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLC.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLCDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLCDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLCDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLCDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtLCDate) = False Then Cancel = True : GoTo EventExitSub
        Call CalcCDueOn()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLocation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLocation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMake_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMake.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMake_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMake.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMake.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMarkersNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMarkersNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMarkersNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMarkersNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMarkersNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMaxRange_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMaxRange.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMaxRange_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxRange.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMinRange_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMinRange.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMinRange_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMinRange.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtModel_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModel.TextChanged

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

    Private Sub txtNogoSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNoGoSize.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNoGoSize_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNoGoSize.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNogoSize.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRange_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRange.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRange_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRange.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRange.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSuppCustCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppCustCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuppCustCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppCustCode.DoubleClick
        cmdSearchSuppCustCode_Click(cmdSearchSuppCustCode, New System.EventArgs())
    End Sub

    Private Sub txtSuppCustCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSuppCustCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSuppCustCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSuppCustCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSuppCustCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchSuppCustCode_Click(cmdSearchSuppCustCode, New System.EventArgs())
    End Sub

    Private Sub txtSuppCustCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuppCustCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If Trim(txtSuppCustCode.Text) = "" Then txtSuppCustName.Text = "" : GoTo EventExitSub

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable(txtSuppCustCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Customer Does Not Exist In Master.", vbInformation)
            Cancel = True
        Else
            txtSuppCustName.Text = MasterNo
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtUnitRange_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUnitRange.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtUnitRange_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUnitRange.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '    KeyAscii = MainClass.UpperCase(KeyAscii, txtUnitRange.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
        If ShowRecord() = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Function ShowRecord() As Boolean

        On Error GoTo ERR1
        Dim xMKey As Double
        ShowRecord = True

        If Trim(txtNumber.Text) = "" Then Exit Function

        If MODIFYMode = True And RsIMTE.EOF = False Then xMKey = RsIMTE.Fields("DOCNO").Value

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_IMTE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND DocNo=" & Val(txtNumber.Text) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTE, ADODB.LockTypeEnum.adLockReadOnly)
        If RsIMTE.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            xMKey = RsIMTE.Fields("DOCNO").Value
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Such Number Does Not Exist" & vbCrLf & "Click Add To Add for New.", MsgBoxStyle.Information)
                ShowRecord = False
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * " & vbCrLf & " FROM QAL_IMTE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DocNo=" & xMKey & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTE, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtNumber.Maxlength = RsIMTE.Fields("DocNo").DefinedSize
        txtItemCode.Maxlength = RsIMTE.Fields("ITEMCODE").DefinedSize
        txtDescription.Maxlength = RsIMTE.Fields("Description").DefinedSize
        txtENo.Maxlength = RsIMTE.Fields("E_NO").DefinedSize
        txtMarkersNo.Maxlength = RsIMTE.Fields("MARKERS_NO").DefinedSize
        txtLC.Maxlength = RsIMTE.Fields("L_C").DefinedSize
        txtMake.Maxlength = RsIMTE.Fields("MAKE_NAME").DefinedSize
        txtRange.Maxlength = RsIMTE.Fields("RANGE").DefinedSize
        txtLocation.Maxlength = RsIMTE.Fields("LOCATION").DefinedSize
        txtValFrequency.Maxlength = RsIMTE.Fields("VALFREQUENCY").Precision
        txtLCDate.Maxlength = RsIMTE.Fields("LCDATE").DefinedSize - 6
        txtIssueDate.Maxlength = RsIMTE.Fields("IssueDate").DefinedSize - 6
        txtCDate.Maxlength = RsIMTE.Fields("CDATE").DefinedSize - 6
        txtIssueTo.Maxlength = RsIMTE.Fields("ISSUETO").DefinedSize
        txtMinRange.Maxlength = RsIMTE.Fields("MIN_RANGE").Precision
        txtMaxRange.Maxlength = RsIMTE.Fields("MAX_RANGE").Precision
        txtUnitRange.Maxlength = RsIMTE.Fields("UNIT_RANGE").DefinedSize
        txtGoSize.Maxlength = RsIMTE.Fields("GOSIZE").DefinedSize
        txtNogoSize.Maxlength = RsIMTE.Fields("NOGOSIZE").DefinedSize
        txtBasicSize.Maxlength = RsIMTE.Fields("BASICSIZE").DefinedSize
        txtWearSize.Maxlength = RsIMTE.Fields("WEARSIZE").DefinedSize
        txtModel.Maxlength = RsIMTE.Fields("MODEL").DefinedSize
        txtCalibBy.Maxlength = RsIMTE.Fields("CALIB_BY").DefinedSize
        txtCertNo.Maxlength = RsIMTE.Fields("CERT_NO").DefinedSize
        txtCalibValid.Maxlength = RsIMTE.Fields("CALIB_VALID").DefinedSize - 6
        txtSuppCustCode.Maxlength = RsIMTE.Fields("SUPP_CUST_CODE").DefinedSize

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 500 * 2)
            .set_ColWidth(2, 500 * 2)
            .set_ColWidth(3, 500 * 5)
            .set_ColWidth(4, 500 * 2)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 2)
            .set_ColWidth(7, 500 * 2)
            .set_ColWidth(8, 500 * 2)
            .set_ColWidth(9, 500 * 2)
            .set_ColWidth(10, 500 * 2)
            .set_ColWidth(11, 500 * 2)
            .set_ColWidth(12, 500 * 2)
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

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub CalcCDueOn()
        If Trim(txtLCDate.Text) = "" Then txtCDate.Text = "" : Exit Sub
        '    txtCDate.Text = DateAdd("d", (Val(txtValFrequency.Text) * 30), txtLCDate.Text)
        txtCDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, Val(txtValFrequency.Text), CDate(txtLCDate.Text)))
    End Sub

    Private Sub txtValFrequency_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtValFrequency.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcCDueOn()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtWearSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWearSize.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWearSize_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWearSize.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtWearSize.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
