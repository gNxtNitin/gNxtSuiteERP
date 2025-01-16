Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmParamMaterialReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const ColItemCode As Short = 4
    Private Const ColItemDesc As Short = 5
    Private Const ColUnit As Short = 6
    Private Const ColDept As Short = 7
    Private Const ColDemandQty As Short = 8
    Private Const ColQty As Short = 9
    Private Const ColRate As Short = 10
    Private Const ColAmount As Short = 11
    Private Const ColAddUser As Short = 12
    Private Const ColAddDate As Short = 13
    Private Const ColModUser As Short = 14
    Private Const ColModDate As Short = 15
    Private Const ColIssueType As Short = 16
    Private Const ColMaterialType As Short = 17
    Private Const ColCategoryDesc As Short = 18
    Private Const ColCostC As Short = 19
    Private Const ColPupose As Short = 20
    Private Const ColStockFor As Short = 21
    Private Const ColMKEY As Short = 22
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboCostCenter_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCostCenter.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCostCenter_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCostCenter.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDept_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call PrintStatus(False)
    End Sub

    Private Sub cboShow_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call PrintStatus(False)
    End Sub

    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub chkAllName_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllName.CheckStateChanged
        Call PrintStatus(False)
        If chkAllName.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtEmpName.Enabled = False
            cmdSearchName.Enabled = False
        Else
            txtEmpName.Enabled = True
            cmdSearchName.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnIssue(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnIssue(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnIssue(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()

        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        If OptDetSumm(0).Checked = True Then
            mTitle = "Material Issue Register " & " [ Detailed ] "
            mTitle = mTitle & "[" & cboStatus.Text & "]"
            If OptOrderBy(0).Checked = True Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MatIssueReg_Refno.rpt"
            Else
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MatIssueReg.rpt"
            End If
            SqlStr = MakeSQLDet
        Else
            mTitle = "Material Issue Register " & " [ Summarised ] "
            mTitle = mTitle & "[" & cboStatus.Text & "]"

            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MatIssueRegSumm.rpt"
            SqlStr = MakeSQLSumm

            MainClass.AssignCRptFormulas(Report1, "STATUS=""" & VB.Left(cboStatus.Text, 1) & """")
        End If

        If Trim(cboDivision.Text) <> "ALL" Then
            mTitle = mTitle & "[" & cboDivision.Text & "]"
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub

    Private Sub cmdSearchName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchName.Click
        SearchEmpName()
    End Sub
    Private Sub SearchEmpName()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtEmpName.Text, "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr)
        If AcName <> "" Then
            txtEmpName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cboStockFor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStockFor.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboStockFor_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStockFor.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub frmParamMaterialReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Material Issue Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamMaterialReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False

        txtEmpName.Enabled = False
        cmdSearchName.Enabled = False

        Call FillIssueCombo()

        Call PrintStatus(True)
        'Call FillPOCombo
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamMaterialReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamMaterialReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub


    Private Sub OptDetSumm_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptDetSumm.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptDetSumm.GetIndex(eventSender)
            OptOrderBy(0).Text = IIf(Index = 0, "Ref No.", "Item Code")
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick

        Dim SqlStr As String = ""
        Dim xIssueNo As Double
        Dim xIssueType As String
        Dim xIsSuppIssue As String

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColRefNo
        xIssueNo = Val(SprdMain.Text)

        SprdMain.Col = ColIssueType
        xIssueType = Trim(SprdMain.Text)

        'SprdMain.Col = ColIsSuppIssue
        xIsSuppIssue = "N"


        If xIssueType = "O" Then
            myMenu = "mnuMatIssueNote"
            XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
            If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
                Exit Sub
            End If
            FrmStoreReq.MdiParent = Me.MdiParent
            FrmStoreReq.Show()
            FrmStoreReq.lblBookType.Text = "I"

            FrmStoreReq.FrmStoreReq_Activated(Nothing, New System.EventArgs())

            FrmStoreReq.txtReqNo.Text = CStr(xIssueNo)
            FrmStoreReq.txtReqNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        Else
            If xIsSuppIssue = "N" Then
                myMenu = "mnuBOPMatIssueNote"
            Else
                myMenu = "mnuBOPMatIssueNoteSupp"
            End If

            XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
            If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
                Exit Sub
            End If
            FrmStoreReqBOP.MdiParent = Me.MdiParent
            FrmStoreReqBOP.Show()
            FrmStoreReqBOP.lblBookType.Text = "I"


            FrmStoreReqBOP.lblIsSuppIssue.Text = xIsSuppIssue

            FrmStoreReqBOP.FrmStoreReqBOP_Activated(Nothing, New System.EventArgs())

            FrmStoreReqBOP.txtReqNo.Text = CStr(xIssueNo)
            FrmStoreReqBOP.txtReqNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        End If
    End Sub


    Private Sub txtEmpName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtEmpName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.DoubleClick
        SearchEmpName()
    End Sub

    Private Sub txtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchEmpName()
    End Sub

    Private Sub txtEmpName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtEmpName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtEmpName.Text), "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtEmpName.Text = UCase(Trim(txtEmpName.Text))
        Else
            MsgInformation("No Such Emp in Master.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub
    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If TxtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            TxtItemName.Text = UCase(Trim(TxtItemName.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Item in Item Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 1.2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefNo, 9)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = False
            ElseIf OptDetSumm(1).Checked = True Or OptDetSumm(2).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefDate, 9)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = False
            ElseIf OptDetSumm(1).Checked = True Or OptDetSumm(2).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 8)
            If OptDetSumm(0).Checked = True Or OptDetSumm(1).Checked = True Then
                .ColHidden = False
            ElseIf OptDetSumm(2).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColItemDesc


            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 25)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)
            If OptDetSumm(0).Checked = True Or OptDetSumm(1).Checked = True Then
                .ColHidden = False
            ElseIf OptDetSumm(2).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDept, 15)
            If OptDetSumm(0).Checked = True Or OptDetSumm(1).Checked = True Then
                .ColHidden = False
            ElseIf OptDetSumm(2).Checked = True Then
                .ColHidden = False
            End If

            .Col = ColDemandQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDemandQty, 9)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColQty, 9)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColRate, 10)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColAmount, 10)

            For cntCol = ColAddUser To ColModDate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(cntCol, 8)
                If OptDetSumm(0).Checked = True Then
                    .ColHidden = False
                ElseIf OptDetSumm(1).Checked = True Or OptDetSumm(2).Checked = True Then
                    .ColHidden = True
                End If
            Next

            .Col = ColIssueType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColIssueType, 8)
            .ColHidden = True

            .Col = ColMaterialType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMaterialType, 8)
            .ColHidden = False


            .Col = ColCategoryDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCategoryDesc, 22)
            .ColHidden = False

            .Col = ColCostC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCostC, 22)
            .ColHidden = False


            .Col = ColPupose
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPupose, 22)
            .ColHidden = False

            .Col = ColStockFor
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColStockFor, 22)
            .ColHidden = False

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If OptDetSumm(0).Checked = True Then
            SqlStr = MakeSQLDet
        ElseIf OptDetSumm(1).Checked = True Then
            SqlStr = MakeSQLSumm()
        Else
            SqlStr = MakeSQLGroupSumm()
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Call CalcSprdTotal()

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQLDet() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mDivision As Double
        Dim mCostCCode As String
        ''SELECT CLAUSE...

        '' TO_CHAR(IGD.RATE), TO_CHAR(IGD.ISSUE_QTY * IGD.RATE),

        MakeSQLDet = " SELECT ''," & vbCrLf & " IGH.AUTO_KEY_ISS," & vbCrLf & " TO_CHAR(IGH.ISSUE_DATE,'DD/MM/YYYY')," & vbCrLf _
            & " IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " IGD.ITEM_UOM, DEPT.DEPT_DESC, TO_CHAR(IGD.DEMAND_QTY), " & vbCrLf _
            & " TO_CHAR(IGD.ISSUE_QTY), GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)/ DECODE(ISSUE_QTY,0,1,IGD.ISSUE_QTY), GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY), " & vbCrLf _
            & " IGH.ADDUSER,IGH.ADDDATE,IGH.MODUSER,IGH.MODDATE,IGH.ISSUE_TYPE,DECODE(IGH.MATERIAL_TYPE,'N','NEW','OLD') AS IS_SUPP_ISSUE , CATMST.GEN_DESC,CC.CC_DESC, IGD.ISSUE_PURPOSE,"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " CASE WHEN IGH.ISSUE_FOR='G' THEN 'GENERAL'" & vbCrLf _
            & " WHEN IGH.ISSUE_FOR='P' THEN 'PRODUCTION'" & vbCrLf _
            & " WHEN IGH.ISSUE_FOR='S' THEN 'SUB STORE'" & vbCrLf _
            & " ELSE 'NEW DEVELOPMENT' END AS ISSUE_FOR,"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " IGH.AUTO_KEY_ISS"
        ''

        ''FROM CLAUSE...
        MakeSQLDet = MakeSQLDet & vbCrLf & " FROM INV_ISSUE_HDR IGH, INV_ISSUE_DET IGD, PAY_DEPT_MST DEPT," & vbCrLf _
            & " INV_ITEM_MST INVMST, INV_GENERAL_MST CATMST, FIN_CCENTER_HDR CC"

        ''WHERE CLAUSE...
        MakeSQLDet = MakeSQLDet & vbCrLf & " WHERE " & vbCrLf _
            & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And IGH.AUTO_KEY_ISS=IGD.AUTO_KEY_ISS" & vbCrLf _
            & " And IGD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " And IGD.ITEM_CODE=INVMST.ITEM_CODE "

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " And INVMST.COMPANY_CODE=CATMST.COMPANY_CODE" & vbCrLf _
            & " And INVMST.CATEGORY_CODE=CATMST.GEN_CODE And CATMST.GEN_TYPE='C'"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " AND IGH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
            & " AND IGH.DEPT_CODE=DEPT.DEPT_CODE"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " AND IGH.COMPANY_CODE=CC.COMPANY_CODE" & vbCrLf _
            & " AND  IGH.COST_CENTER_CODE=CC.CC_CODE"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        If chkAllName.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtEmpName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmployee = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.EMP_CODE='" & MainClass.AllowSingleQuote(mEmployee) & "'"
            End If
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboMaterial.Text <> "ALL" Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.MATERIAL_TYPE='" & VB.Left(cboMaterial.Text, 1) & "'"
        End If

        If cboCostCenter.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboCostCenter.Text, "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCostCCode = Trim(MasterNo)
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.COST_CENTER_CODE='" & MainClass.AllowSingleQuote(Trim(mCostCCode)) & "' "
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboCategory.Text <> "ALL" Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND CATMST.GEN_DESC='" & Trim(cboCategory.Text) & "'"
        End If

        If cboStatus.SelectedIndex = 1 Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.ISSUE_STATUS='Y'"
        ElseIf cboStatus.SelectedIndex = 2 Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND (IGD.DEMAND_QTY>IGD.ISSUE_QTY AND IGH.ISSUE_STATUS='N')"
        End If

        If cboStockFor.Text <> "ALL" Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.ISSUE_FOR='" & VB.Left(cboStockFor.Text, 1) & "'"
        End If

        MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.ISSUE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.ISSUE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If OptGroupBy(0).Checked = True Then
            If OptOrderBy(0).Checked = True Then
                MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY IGH.AUTO_KEY_ISS, IGH.ISSUE_DATE ,IGD.SERIAL_NO"
            ElseIf OptOrderBy(1).Checked = True Then
                MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC,IGH.AUTO_KEY_ISS, IGH.ISSUE_DATE"
            End If
        Else
            If OptOrderBy(0).Checked = True Then
                MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY DEPT.DEPT_DESC,IGH.AUTO_KEY_ISS, IGH.ISSUE_DATE ,IGD.SERIAL_NO"
            ElseIf OptOrderBy(1).Checked = True Then
                MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY DEPT.DEPT_DESC,INVMST.ITEM_SHORT_DESC,IGH.AUTO_KEY_ISS, IGH.ISSUE_DATE"
            End If
        End If
        'End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLSumm() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mDivision As Double
        Dim mCostCCode As String

        ''SELECT CLAUSE...
        ''GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)/ DECODE(ISSUE_QTY,0,1,IGD.ISSUE_QTY), GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)
        ''TO_CHAR(MAX(IGD.RATE)), TO_CHAR(SUM(IGD.ISSUE_QTY * IGD.RATE))
        'SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)/ DECODE(ISSUE_QTY,0,1,IGD.ISSUE_QTY)), SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY))
        ' GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IGD.ITEM_CODE,  IGH.COMPANY_CODE, SUM(IGD.ISSUE_QTY))/ DECODE(SUM(IGD.ISSUE_QTY),0,1,SUM(IGD.ISSUE_QTY)), GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),IGD.ITEM_CODE,  IGH.COMPANY_CODE, SUM(IGD.ISSUE_QTY))

        If OptGroupBy(0).Checked = True Then
            MakeSQLSumm = " SELECT ''," & vbCrLf & " ''," & vbCrLf & " ''," & vbCrLf & " IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
                & " IGD.ITEM_UOM, '', TO_CHAR(SUM(IGD.DEMAND_QTY)), " & vbCrLf _
                & " TO_CHAR(SUM(IGD.ISSUE_QTY)),SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY))/  DECODE(SUM(IGD.ISSUE_QTY),0,1,SUM(IGD.ISSUE_QTY)), SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)),'','','','','','', CATMST.GEN_DESC,CC.CC_DESC,'',''"
        Else
            MakeSQLSumm = " SELECT ''," & vbCrLf & " ''," & vbCrLf & " ''," & vbCrLf & " IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
                & " IGD.ITEM_UOM, DEPT.DEPT_DESC, TO_CHAR(SUM(IGD.DEMAND_QTY)), " & vbCrLf _
                & " TO_CHAR(SUM(IGD.ISSUE_QTY)),SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY))/ DECODE(SUM(IGD.ISSUE_QTY),0,1,SUM(IGD.ISSUE_QTY)), SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)),'','','','','','', CATMST.GEN_DESC,CC.CC_DESC,'','',''"
        End If
        ''FROM CLAUSE...
        MakeSQLSumm = MakeSQLSumm & vbCrLf & " FROM INV_ISSUE_HDR IGH, INV_ISSUE_DET IGD," & vbCrLf _
            & " INV_ITEM_MST INVMST, INV_GENERAL_MST CATMST, PAY_DEPT_MST DEPT, FIN_CCENTER_HDR CC"

        ''WHERE CLAUSE...
        MakeSQLSumm = MakeSQLSumm & vbCrLf & " WHERE " & vbCrLf & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IGH.AUTO_KEY_ISS=IGD.AUTO_KEY_ISS" & vbCrLf & " AND IGH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE "

        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " AND INVMST.COMPANY_CODE=CATMST.COMPANY_CODE" & vbCrLf _
            & " AND INVMST.CATEGORY_CODE=CATMST.GEN_CODE AND CATMST.GEN_TYPE='C'"

        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " AND IGH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
            & " AND IGH.DEPT_CODE=DEPT.DEPT_CODE"

        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " AND IGH.COMPANY_CODE=CC.COMPANY_CODE" & vbCrLf _
            & " AND  IGH.COST_CENTER_CODE=CC.CC_CODE"

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND IGH.ISSUE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.ISSUE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        If chkAllName.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtEmpName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmployee = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.EMP_CODE='" & MainClass.AllowSingleQuote(mEmployee) & "'"
            End If
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboMaterial.Text <> "ALL" Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.MATERIAL_TYPE='" & VB.Left(cboMaterial.Text, 1) & "'"
        End If

        If cboCostCenter.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboCostCenter.Text, "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCostCCode = Trim(MasterNo)
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.COST_CENTER_CODE='" & MainClass.AllowSingleQuote(Trim(mCostCCode)) & "' "
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboCategory.Text <> "ALL" Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND CATMST.GEN_DESC='" & Trim(cboCategory.Text) & "'"
        End If

        If cboStatus.SelectedIndex = 1 Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.ISSUE_STATUS='Y'"
        End If

        If cboStockFor.Text <> "ALL" Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.ISSUE_FOR='" & VB.Left(cboStockFor.Text, 1) & "'"
        End If

        If OptGroupBy(0).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "GROUP BY IGH.COMPANY_CODE,IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC,IGD.ITEM_UOM, CATMST.GEN_DESC , CC.CC_DESC"
        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "GROUP BY IGH.COMPANY_CODE,IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC,IGD.ITEM_UOM, DEPT_DESC, CATMST.GEN_DESC,CC.CC_DESC "
        End If
        If cboStatus.SelectedIndex = 2 Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "HAVING SUM(IGD.DEMAND_QTY)>SUM(IGD.ISSUE_QTY)"
        End If

        If OptGroupBy(0).Checked = True Then
            If OptOrderBy(0).Checked = True Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY IGD.ITEM_CODE,INVMST.ITEM_SHORT_DESC"
            ElseIf OptOrderBy(1).Checked = True Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC,IGD.ITEM_CODE"
            End If
        Else
            If OptOrderBy(0).Checked = True Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY DEPT_DESC,IGD.ITEM_CODE,INVMST.ITEM_SHORT_DESC"
            ElseIf OptOrderBy(1).Checked = True Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY DEPT_DESC,INVMST.ITEM_SHORT_DESC,IGD.ITEM_CODE"
            End If
        End If
        'End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLGroupSumm() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mDivision As Double
        Dim mCostCCode As String

        ''SELECT CLAUSE...

        ', GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)

        MakeSQLGroupSumm = " SELECT ''," & vbCrLf _
                & " ''," & vbCrLf _
                & " ''," & vbCrLf _
                & " '', CATMST.GEN_DESC," & vbCrLf _
                & " '', DEPT.DEPT_DESC, TO_CHAR(SUM(IGD.DEMAND_QTY)), " & vbCrLf _
                & " TO_CHAR(SUM(IGD.ISSUE_QTY)), SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY))/ DECODE(SUM(IGD.ISSUE_QTY),0,1,SUM(IGD.ISSUE_QTY)), " & vbCrLf _
                & " SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)),'','','','','','', CATMST.GEN_DESC,'','','',''"


        ''FROM CLAUSE...
        MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf _
            & " FROM INV_ISSUE_HDR IGH, INV_ISSUE_DET IGD," & vbCrLf _
            & " INV_ITEM_MST INVMST, INV_GENERAL_MST CATMST, PAY_DEPT_MST DEPT"  '', FIN_CCENTER_HDR CC"

        ''WHERE CLAUSE...
        MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IGH.AUTO_KEY_ISS=IGD.AUTO_KEY_ISS" & vbCrLf _
            & " AND IGH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE "

        MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf _
            & " AND INVMST.COMPANY_CODE=CATMST.COMPANY_CODE" & vbCrLf _
            & " AND INVMST.CATEGORY_CODE=CATMST.GEN_CODE AND CATMST.GEN_TYPE='C'"

        MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf _
            & " AND IGH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
            & " AND IGH.DEPT_CODE=DEPT.DEPT_CODE"

        'MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf _
        '    & " AND IGH.COMPANY_CODE=CC.COMPANY_CODE" & vbCrLf _
        '    & " AND IGH.COST_CENTER_CODE=CC.CC_CODE"


        MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & " AND IGH.ISSUE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.ISSUE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & "AND IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        If chkAllName.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtEmpName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmployee = MasterNo
                MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & "AND IGH.EMP_CODE='" & MainClass.AllowSingleQuote(mEmployee) & "'"
            End If
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboMaterial.Text <> "ALL" Then
            MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & "AND IGH.MATERIAL_TYPE='" & VB.Left(cboMaterial.Text, 1) & "'"
        End If

        If cboCostCenter.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboCostCenter.Text, "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCostCCode = Trim(MasterNo)
                MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & "AND IGH.COST_CENTER_CODE='" & MainClass.AllowSingleQuote(Trim(mCostCCode)) & "' "
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & "AND IGH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboCategory.Text <> "ALL" Then
            MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & "AND CATMST.GEN_DESC='" & Trim(cboCategory.Text) & "'"
        End If

        If cboStatus.SelectedIndex = 1 Then
            MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & "AND IGH.ISSUE_STATUS='Y'"
        End If

        If cboStockFor.Text <> "ALL" Then
            MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & "AND IGH.ISSUE_FOR='" & VB.Left(cboStockFor.Text, 1) & "'"
        End If


        MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & "GROUP BY CATMST.GEN_DESC, DEPT.DEPT_DESC "

        If cboStatus.SelectedIndex = 2 Then
            MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & "HAVING SUM(IGD.DEMAND_QTY)>SUM(IGD.ISSUE_QTY)"
        End If


        MakeSQLGroupSumm = MakeSQLGroupSumm & vbCrLf & "ORDER BY CATMST.GEN_DESC, DEPT.DEPT_DESC"

        'End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillIssueCombo()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        cboDept.Items.Clear()

        SqlStr = "SELECT DEPT_DESC FROM PAY_DEPT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDept.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDept.Items.Add(RS.Fields("DEPT_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDept.SelectedIndex = 0

        cboCostCenter.Items.Clear()
        SqlStr = "Select CC_DESC FROM FIN_CCENTER_HDR WHERE COMPANy_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY CC_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)
        cboCostCenter.Items.Add("ALL")

        If RS.EOF = False Then
            Do While Not RS.EOF
                cboCostCenter.Items.Add(RS.Fields("CC_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboCostCenter.SelectedIndex = 0

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDivision.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0

        cboStockFor.Items.Clear()
        cboStockFor.Items.Add("ALL")
        cboStockFor.Items.Add("General")
        cboStockFor.Items.Add("Production")
        cboStockFor.Items.Add("Sub Store")
        cboStockFor.Items.Add("New Development")
        cboStockFor.SelectedIndex = 0

        cboStatus.Items.Clear()
        cboStatus.Items.Add("BOTH")
        cboStatus.Items.Add("Complete")
        cboStatus.Items.Add("Pending")
        cboStatus.SelectedIndex = 0

        cboMaterial.Items.Clear()
        cboMaterial.Items.Add("ALL")
        cboMaterial.Items.Add("New")
        cboMaterial.Items.Add("Old")
        cboMaterial.SelectedIndex = 0

        cboCategory.Items.Clear()

        SqlStr = "SELECT GEN_DESC FROM INV_GENERAL_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'" & vbCrLf & " ORDER BY GEN_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboCategory.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboCategory.Items.Add(RS.Fields("GEN_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboCategory.SelectedIndex = 0


        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub CalcSprdTotal()

        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mQty As Double = 0
        Dim mAmount As Double = 0

        With SprdMain
            For cntRow = 1 To SprdMain.MaxRows
                .Row = cntRow
                .Col = ColQty
                mQty = mQty + Val(.Text)

                .Col = ColAmount
                mAmount = mAmount + Val(.Text)
            Next
            Call MainClass.AddBlankfpSprdRow(SprdMain, ColQty)
            .Col = ColItemDesc
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows

            .Col = ColQty
            .Text = VB6.Format(mQty, "0.00")

            .Col = ColAmount
            .Text = VB6.Format(mAmount, "0.00")

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


End Class
