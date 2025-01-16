Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmParamMaterialSSReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const ColItemCode As Short = 4
    Private Const ColItemDesc As Short = 5
    Private Const ColUnit As Short = 6
    Private Const ColCategory As Short = 7
    Private Const ColDept As Short = 8
    Private Const ColDeptName As Short = 9
    Private Const ColCostCenter As Short = 10
    Private Const ColDemandQty As Short = 11
    Private Const ColQty As Short = 12
    Private Const ColRate As Short = 13
    Private Const ColAmount As Short = 14
    Private Const ColPupose As Short = 15
    Private Const ColAddUser As Short = 16
    Private Const ColAddDate As Short = 17
    Private Const ColModUser As Short = 18
    Private Const ColModDate As Short = 19

    Private Const ColMKEY As Short = 20
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
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

    Private Sub cboToDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboToDept.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboToDept_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboToDept.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemName.Enabled = False
            cmdsearch.Enabled = False
        Else
            txtItemName.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub chkAllName_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllName.CheckStateChanged
        Call PrintStatus(False)
        If chkAllName.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtEmpName.Enabled = False
            cmdSearchName.Enabled = False
        Else
            TxtEmpName.Enabled = True
            cmdSearchName.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
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
            mTitle = "Material Issue Register (Sub Store)" & " [ Detailed ] "

            If optOrderBy(0).Checked = True Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MatIssueReg_Refno.rpt"
            Else
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MatIssueReg.rpt"
            End If
            SqlStr = MakeSQLDet
        Else
            mTitle = "Material Issue Register (Sub Store)" & " [ Summarised ] "

            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MatIssueRegSumm.rpt"
            SqlStr = MakeSQLSumm

            MainClass.AssignCRptFormulas(Report1, "STATUS=""" & VB.Left(cboStatus.Text, 1) & """")
        End If


        If Trim(cboStatus.Text) <> "BOTH" Then
            mTitle = mTitle & "[" & cboStatus.Text & "]"
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
            TxtEmpName.Text = AcName
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
    Private Sub frmParamMaterialSSReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Material Issue Register (Sub Store)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamMaterialSSReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        txtItemName.Enabled = False
        cmdsearch.Enabled = False

        TxtEmpName.Enabled = False
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
    Private Sub frmParamMaterialSSReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamMaterialSSReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub


    Private Sub OptDetSumm_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptDetSumm.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptDetSumm.GetIndex(eventSender)
            optOrderBy(0).Text = IIf(Index = 0, "Ref No.", "Item Code")
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

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColRefNo
        xIssueNo = Val(SprdMain.Text)

        myMenu = "mnuMatIssueNotesub"
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, "mnuMatIssueNotesub", PubDBCn)
        If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
            Exit Sub
        End If
        FrmStoreReqSub.MdiParent = Me.MdiParent
        FrmStoreReqSub.Show()
        FrmStoreReqSub.lblBookType.Text = "I"

        FrmStoreReqSub.FrmStoreReqSub_Activated(Nothing, New System.EventArgs())

        FrmStoreReqSub.txtReqNo.Text = CStr(xIssueNo)
        FrmStoreReqSub.txtReqNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub


    Private Sub txtEmpName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtEmpName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.DoubleClick
        SearchEmpName()
    End Sub

    Private Sub txtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtEmpName.Text)
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

        If TxtEmpName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((TxtEmpName.Text), "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtEmpName.Text = UCase(Trim(TxtEmpName.Text))
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
            txtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
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
        If txtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            txtItemName.Text = UCase(Trim(txtItemName.Text))
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
            ElseIf OptDetSumm(1).Checked = True Then
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
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
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

            .Col = ColCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCategory, 18)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDept, 5)

            .Col = ColDeptName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDeptName, 15)

            .Col = ColCostCenter
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCostCenter, 15)

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

            .Col = ColPupose
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPupose, 22)
            .ColHidden = False


            For cntCol = ColAddUser To ColModDate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(cntCol, 8)
                If OptDetSumm(0).Checked = True Then
                    .ColHidden = False
                ElseIf OptDetSumm(1).Checked = True Then
                    .ColHidden = True
                End If
            Next

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
        Else
            SqlStr = MakeSQLSumm
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

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

        ''SELECT CLAUSE...


        MakeSQLDet = " SELECT ''," & vbCrLf & " IGH.AUTO_KEY_ISS," & vbCrLf & " TO_CHAR(IGH.ISSUE_DATE,'DD/MM/YYYY')," & vbCrLf _
            & " IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC, " & vbCrLf _
            & " IGD.ITEM_UOM, CMST.GEN_DESC, IGH.DEPT_CODE, DMST.DEPT_DESC, CC.CC_DESC, TO_CHAR(IGD.DEMAND_QTY), " & vbCrLf _
            & " TO_CHAR(IGD.ISSUE_QTY), " & vbCrLf _
            & " TO_CHAR(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)/ DECODE(ISSUE_QTY,0,1,IGD.ISSUE_QTY)) AS RATE, " & vbCrLf _
            & " TO_CHAR(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)) AS AMOUNT, IGD.ISSUE_PURPOSE," & vbCrLf _
            & " IGH.ADDUSER,IGH.ADDDATE,IGH.MODUSER,IGH.MODDATE,IGH.AUTO_KEY_ISS"

        'MakeSQLDet = " SELECT ''," & vbCrLf & " IGH.AUTO_KEY_ISS," & vbCrLf & " TO_CHAR(IGH.ISSUE_DATE,'DD/MM/YYYY')," & vbCrLf _
        '    & " IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
        '    & " IGD.ITEM_UOM, IGH.DEPT_CODE, TO_CHAR(IGD.DEMAND_QTY), " & vbCrLf _
        '    & " TO_CHAR(IGD.ISSUE_QTY), " & vbCrLf _
        '    & " TO_CHAR(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, " & vbCrLf _
        '    & " SELECT SUM() FROM INV_STOCK_REC_TRN WHERE COMPANY_CODE=IGH.COMPANY_CODE FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND ITEM_CODE=IGD.ITEM_CODE AND STOCK_ID='WH' AND REF_DATE<TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
        '    & " )/ DECODE(ISSUE_QTY,0,1,IGD.ISSUE_QTY)) AS RATE, " & vbCrLf _
        '    & " TO_CHAR(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)) AS AMOUNT, IGD.ISSUE_PURPOSE," & vbCrLf _
        '    & " IGH.ADDUSER,IGH.ADDDATE,IGH.MODUSER,IGH.MODDATE,IGH.AUTO_KEY_ISS"


        ''FROM CLAUSE...
        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " FROM INV_SUB_ISSUE_HDR IGH, INV_SUB_ISSUE_DET IGD," & vbCrLf _
            & " INV_ITEM_MST INVMST, INV_GENERAL_MST CMST, PAY_DEPT_MST DMST, FIN_CCENTER_HDR CC"

        ''WHERE CLAUSE...
        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And IGH.AUTO_KEY_ISS=IGD.AUTO_KEY_ISS" & vbCrLf _
            & " And IGD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " And IGD.ITEM_CODE=INVMST.ITEM_CODE "

        '
        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " And IGH.COMPANY_CODE=DMST.COMPANY_CODE" & vbCrLf _
            & " And IGH.DEPT_CODE=DMST.DEPT_CODE "

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " And IGH.COMPANY_CODE=CC.COMPANY_CODE" & vbCrLf _
            & " And IGH.COST_CENTER_CODE=CC.CC_CODE "

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " And INVMST.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And INVMST.CATEGORY_CODE=CMST.GEN_CODE AND GEN_TYPE='C'"

        ''  SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "And IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        If chkAllName.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtEmpName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmployee = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.EMP_CODE='" & MainClass.AllowSingleQuote(mEmployee) & "'"
            End If
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.SUB_STORE_DEPT='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboToDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboToDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboStatus.SelectedIndex = 1 Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.ISSUE_STATUS='Y'"
        ElseIf cboStatus.SelectedIndex = 2 Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND (IGD.DEMAND_QTY>IGD.ISSUE_QTY AND IGH.ISSUE_STATUS='N')"
        End If



        MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.ISSUE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.ISSUE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        '    If OptOrderBy(0).Value = True Then
        '        MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY IGH.AUTO_KEY_ISS, IGH.ISSUE_DATE ,IGD.SERIAL_NO"
        '    ElseIf OptOrderBy(1).Value = True Then
        '        MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC,IGH.AUTO_KEY_ISS, IGH.ISSUE_DATE"
        '    End If
        '
        If OptGroupBy(0).Checked = True Then
            If optOrderBy(0).Checked = True Then
                MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY IGH.AUTO_KEY_ISS, IGH.ISSUE_DATE ,IGD.SERIAL_NO"
            ElseIf optOrderBy(1).Checked = True Then
                MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC,IGH.AUTO_KEY_ISS, IGH.ISSUE_DATE"
            End If
        Else
            If optOrderBy(0).Checked = True Then
                MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY IGH.DEPT_CODE,IGH.AUTO_KEY_ISS, IGH.ISSUE_DATE ,IGD.SERIAL_NO"
            ElseIf optOrderBy(1).Checked = True Then
                MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY IGH.DEPT_CODE,INVMST.ITEM_SHORT_DESC,IGH.AUTO_KEY_ISS, IGH.ISSUE_DATE"
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

        ''SELECT CLAUSE...

        If OptGroupBy(0).Checked = True Then
            MakeSQLSumm = " SELECT ''," & vbCrLf & " ''," & vbCrLf & " ''," & vbCrLf _
                & " IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
                & " IGD.ITEM_UOM, CMST.GEN_DESC, '', '','',TO_CHAR(SUM(IGD.DEMAND_QTY)), " & vbCrLf _
                & " TO_CHAR(SUM(IGD.ISSUE_QTY)), " & vbCrLf _
                & " TO_CHAR(SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY))/ DECODE(SUM(IGD.ISSUE_QTY),0,1,SUM(IGD.ISSUE_QTY))) AS RATE, " & vbCrLf _
                & " TO_CHAR(SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY))) AS AMOUNT,'','','','','',''"
        Else
            MakeSQLSumm = " SELECT ''," & vbCrLf & " ''," & vbCrLf & " ''," & vbCrLf & " IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
                & " IGD.ITEM_UOM, CMST.GEN_DESC, IGH.DEPT_CODE, DMST.DEPT_DESC, '', TO_CHAR(SUM(IGD.DEMAND_QTY)), " & vbCrLf _
                & " TO_CHAR(SUM(IGD.ISSUE_QTY)), " & vbCrLf _
                & " TO_CHAR(SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY))/ DECODE(SUM(IGD.ISSUE_QTY),0,1,SUM(IGD.ISSUE_QTY))) AS RATE, " & vbCrLf _
                & " TO_CHAR(SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY))) AS AMOUNT,'','','','',''"
        End If

        'DMAT.DEPT_DESC, CC.CC_DESC, 
        ''SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY))/ DECODE(SUM(IGD.ISSUE_QTY),0,1,SUM(IGD.ISSUE_QTY)), SUM(GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY))

        ''  ''GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)/ DECODE(ISSUE_QTY,0,1,IGD.ISSUE_QTY), GETFIFOITEMRATE(IGH.ISSUE_DATE,IGD.ITEM_CODE,  IGH.COMPANY_CODE, IGD.ISSUE_QTY)


        ''FROM CLAUSE...
        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " FROM INV_SUB_ISSUE_HDR IGH, INV_SUB_ISSUE_DET IGD," & vbCrLf _
            & " INV_ITEM_MST INVMST, INV_GENERAL_MST CMST, PAY_DEPT_MST DMST, FIN_CCENTER_HDR CC"

        ''WHERE CLAUSE...
        MakeSQLSumm = MakeSQLSumm & vbCrLf & " WHERE " & vbCrLf _
            & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IGH.AUTO_KEY_ISS=IGD.AUTO_KEY_ISS" & vbCrLf _
            & " AND IGD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE "

        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " And IGH.COMPANY_CODE=DMST.COMPANY_CODE" & vbCrLf _
            & " And IGH.DEPT_CODE=DMST.DEPT_CODE "

        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " And IGH.COMPANY_CODE=CC.COMPANY_CODE" & vbCrLf _
            & " And IGH.COST_CENTER_CODE=CC.CC_CODE "

        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " And INVMST.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And INVMST.CATEGORY_CODE=CMST.GEN_CODE AND GEN_TYPE='C'"

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND IGH.ISSUE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.ISSUE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        End If

        If chkAllName.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtEmpName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmployee = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.EMP_CODE='" & MainClass.AllowSingleQuote(mEmployee) & "'"
            End If
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.SUB_STORE_DEPT='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboToDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboToDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.DIV_CODE=" & mDivision & ""
            End If
        End If


        If cboStatus.SelectedIndex = 1 Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.ISSUE_STATUS='Y'"
        End If

        If OptGroupBy(0).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "GROUP BY IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC,IGD.ITEM_UOM,CMST.GEN_DESC "
        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "GROUP BY IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC,IGD.ITEM_UOM,CMST.GEN_DESC,IGH.DEPT_CODE,DMST.DEPT_DESC "
        End If

        If cboStatus.SelectedIndex = 2 Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "HAVING SUM(IGD.DEMAND_QTY)>SUM(IGD.ISSUE_QTY)"
        End If

        If OptGroupBy(0).Checked = True Then
            If optOrderBy(0).Checked = True Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY IGD.ITEM_CODE,INVMST.ITEM_SHORT_DESC"
            ElseIf optOrderBy(1).Checked = True Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC,IGD.ITEM_CODE"
            End If
        Else
            If optOrderBy(0).Checked = True Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY IGH.DEPT_CODE,IGD.ITEM_CODE,INVMST.ITEM_SHORT_DESC"
            ElseIf optOrderBy(1).Checked = True Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY IGH.DEPT_CODE,INVMST.ITEM_SHORT_DESC,IGD.ITEM_CODE"
            End If
        End If

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
            If Trim(txtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
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
        cboToDept.Items.Clear()
        SqlStr = "SELECT DEPT_DESC,ISSUBSTORE FROM PAY_DEPT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDept.Items.Add("ALL")
        cboToDept.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                If RS.Fields("ISSUBSTORE").Value = "Y" Then
                    cboDept.Items.Add(RS.Fields("DEPT_DESC").Value)
                End If
                cboToDept.Items.Add(RS.Fields("DEPT_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDept.SelectedIndex = 0
        cboToDept.SelectedIndex = 0

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

        cboStatus.Items.Clear()
        cboStatus.Items.Add("BOTH")
        cboStatus.Items.Add("Complete")
        cboStatus.Items.Add("Pending")
        cboStatus.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
End Class
