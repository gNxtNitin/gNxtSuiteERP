Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamDeptWiseProdReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const ColProdDate As Short = 4
    Private Const ColDeptCode As Short = 5
    Private Const ColItemCode As Short = 6
    Private Const ColItemDesc As Short = 7
    Private Const ColUnit As Short = 8
    Private Const ColShift As Short = 9
    Private Const ColOperatorCode As Short = 10
    Private Const ColOperatorName As Short = 11
    Private Const ColOperation As Short = 12
    Private Const ColProdQty As Short = 13
    Private Const ColBreakageQty As Short = 14
    Private Const ColOkProdQty As Short = 15
    Private Const ColReWorkQty As Short = 16
    Private Const ColMRQty As Short = 17
    Private Const ColMachineNo As Short = 18
    Private Const ColMachineName As Short = 19
    Private Const ColMachineTime As Short = 20
    Private Const ColBreakDownTime As Short = 21
    Private Const ColNoTool As Short = 22
    Private Const ColNoMaterial As Short = 23
    Private Const ColNoOperator As Short = 24
    Private Const ColPowerCutTime As Short = 25
    Private Const ColToolChangeTime As Short = 26
    Private Const ColSetupChangeTime As Short = 27
    Private Const ColQAIssue As Short = 28
    Private Const ColProdQtySQM As Short = 29
    Private Const ColBreakageQtySQM As Short = 30
    Private Const ColOKProdQtySQM As Short = 31
    Private Const ColReason As Short = 32
    Private Const ColRemarks As Short = 33

    Private Const ColCompanyName As Short = 34
    Private Const ColMKEY As Short = 35

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim mClickProcess As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If ChkALL.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdSearch.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdSearch.Enabled = True
        End If
    End Sub

    Private Sub chkTime_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTime.CheckStateChanged
        Call PrintStatus(False)
        If chkTime.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTMFrom.Enabled = False
            txtTMTo.Enabled = False
        Else
            txtTMFrom.Enabled = True
            txtTMTo.Enabled = True
        End If
        txtTMFrom.Text = GetServerTime
        txtTMTo.Text = GetServerTime
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
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
        Dim mTitle As String
        Dim mSubTitle As String


        Report1.Reset()

        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        If OptDetSumm(0).Checked = True Then
            mTitle = "Production Register " & " [ Detailed ] "
            mTitle = mTitle & "[" & cboDept.Text & "]" & "[" & cboShift.Text & "]"
            '        If OptOrderBy(0).Value = True Then	
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ProdReg.rpt"
            '        Else	
            '            Report1.ReportFileName = App.path & "\Reports\MatIssueReg.rpt"	
            '        End If	
            SqlStr = MakeSQLDet
        Else
            mTitle = "Production Register " & " [ Summarised ] "
            mTitle = mTitle & "[" & cboDept.Text & "]" & "[" & cboShift.Text & "]"

            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ProdRegSumm.rpt"
            SqlStr = MakeSQLSumm

            '        MainClass.AssignCRptFormulas Report1, "STATUS=""" & vb.Left(cboShift.Text, 1) & """"	
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
    Private Sub frmParamDeptWiseProdReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Production Register " ''& IIf(lblApproval.text = "N", " - Approval", "")	

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDeptWiseProdReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        'Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        ChkALL.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdSearch.Enabled = False

        chkOprAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtOpr.Enabled = False
        cmdSearchOPR.Enabled = False

        chkOperatorAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtOperator.Enabled = False
        cmdSearchOperator.Enabled = False


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
    Private Sub frmParamDeptWiseProdReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamDeptWiseProdReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

        If OptDetSumm(1).Checked = True Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColRefNo
        xIssueNo = Val(SprdMain.Text)


        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, "mnuMatIssueNote", PubDBCn)
        If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
            Exit Sub
        End If

        FrmPMemoDeptWise.MdiParent = Me.MdiParent
        FrmPMemoDeptWise.lblBookType.Text = "P"
        FrmPMemoDeptWise.Show()

        FrmPMemoDeptWise.FrmPMemoDeptWise_Activated(Nothing, New System.EventArgs())

        FrmPMemoDeptWise.txtPMemoNo.Text = CStr(xIssueNo)

        FrmPMemoDeptWise.txtPMemoNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
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

    Private Sub SearchOPR()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtOpr.Text, "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , SqlStr)
        If AcName <> "" Then
            txtOpr.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub SearchOperator()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = " SELECT EMP_NAME, EMP_CODE, EMP_FNAME" & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE=2 AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"

        SqlStr = SqlStr & vbCrLf & " UNION "

        SqlStr = SqlStr & vbCrLf & " SELECT EMP_NAME, EMP_CODE,EMP_FNAME " & vbCrLf & " FROM PAY_CONT_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT='P' AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')" & vbCrLf & " ORDER BY 1"


        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then

            If AcName <> "" Then
                txtOperator.Text = AcName1
                txtOperatorName.Text = AcName
            End If
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
    Private Sub txtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If TxtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.text = MasterNo
            TxtItemName.Text = UCase(Trim(TxtItemName.Text))
        Else
            lblAcCode.text = ""
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

            .Col = ColProdDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColProdDate, 9)
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
            If OptDetSumm(0).Checked = True Then
                .set_ColWidth(ColItemDesc, 22)
            Else
                .set_ColWidth(ColItemDesc, 25)
            End If

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDeptCode, 4)

            .Col = ColShift
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColShift, 4)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = False
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColOperatorCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColOperatorCode, 8)

            .Col = ColOperatorName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColOperatorName, 15)

            .Col = ColOperation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            If OptDetSumm(0).Checked = True Then
                .set_ColWidth(ColOperation, 10)
            Else
                .set_ColWidth(ColOperation, 20)
            End If


            .Col = ColProdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColProdQty, 9)

            .Col = ColBreakageQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBreakageQty, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)



            .Col = ColOkProdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColOkProdQty, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColReWorkQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColReWorkQty, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColMRQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColMRQty, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColMachineNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMachineNo, 8)

            .Col = ColMachineName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMachineName, 8)

            .Col = ColMachineTime
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColMachineTime, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)


            .Col = ColBreakDownTime
            .CellType = SS_CELL_TYPE_INTEGER
            .set_ColWidth(ColBreakDownTime, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColNoTool
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColNoTool, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColNoMaterial
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColNoMaterial, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColNoOperator
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColNoOperator, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColPowerCutTime
            .CellType = SS_CELL_TYPE_INTEGER
            .set_ColWidth(ColPowerCutTime, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColToolChangeTime
            .CellType = SS_CELL_TYPE_INTEGER
            .set_ColWidth(ColToolChangeTime, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColSetupChangeTime
            .CellType = SS_CELL_TYPE_INTEGER
            .set_ColWidth(ColSetupChangeTime, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColQAIssue
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColQAIssue, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColReason, 12)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = False '' IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 12)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If

            .Col = ColCompanyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCompanyName, 12)

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True

            .Col = ColProdQtySQM
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColProdQtySQM, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColBreakageQtySQM
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBreakageQtySQM, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColOKProdQtySQM
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColOKProdQtySQM, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' = OperationModeSingle	
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
        Dim mOPRCode As String
        Dim mItemCode As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String

        MakeSQLDet = " SELECT ''," & vbCrLf _
            & " IGH.AUTO_KEY_REF," & vbCrLf _
            & " TO_CHAR(IGH.REF_DATE,'DD/MM/YYYY')," & vbCrLf _
            & " TO_CHAR(IGH.PROD_DATE,'DD/MM/YYYY')," & vbCrLf _
            & " IGH.DEPT_CODE, IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " IGD.ITEM_UOM, IGH.SHIFT_CODE, IGD.OPERATOR_CODE, "

        MakeSQLDet = MakeSQLDet & vbCrLf & " EMP.EMP_NAME,"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " OPR.OPR_DESC, " & vbCrLf _
            & " TO_CHAR(IGD.PROD_QTY), TO_CHAR(IGD.SCRAP_QTY), TO_CHAR(IGD.PROD_QTY-IGD.SCRAP_QTY),"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114 Then
            MakeSQLDet = MakeSQLDet & vbCrLf _
            & " TO_CHAR(IGD.REWORK_QTY), TO_CHAR(IGD.MR_QTY), "
        Else
            MakeSQLDet = MakeSQLDet & vbCrLf & " 0, 0, "
        End If

        MakeSQLDet = MakeSQLDet & vbCrLf & " IGD.MACHINE_NO,"

        MakeSQLDet = MakeSQLDet & vbCrLf & " MACHINE_DESC,"

        MakeSQLDet = MakeSQLDet & vbCrLf & " NVL(MACHINE_WORKING_HOURS,0) MACHINE_WORKING_HOURS,"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114 Then
            MakeSQLDet = MakeSQLDet & vbCrLf _
                        & "  BREAKDOWN_TIME, NO_TOOL, NO_MATERIAL,  " & vbCrLf _
                        & " NO_OPERATOR, POWER_CUT_TIME, TOOL_CHANGE_TIME, SETUP_CHANGE_TIME, QA_ISSUE,"
        Else
            MakeSQLDet = MakeSQLDet & vbCrLf & " 0, '','','',0, 0, 0 ,'', "
        End If

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " TO_CHAR(IGD.PROD_QTY * (MAT_LEN/1000) * (MAT_WIDTH/1000)), TO_CHAR(IGD.SCRAP_QTY * (MAT_LEN/1000) * (MAT_WIDTH/1000)), TO_CHAR((IGD.PROD_QTY-IGD.SCRAP_QTY)* (MAT_LEN/1000) * (MAT_WIDTH/1000)),"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " IGD.REASON, IGD.REMARKS, GEN.COMPANY_SHORTNAME, IGH.AUTO_KEY_REF"




        ''FROM CLAUSE...	
        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " FROM PRD_PMEMODEPT_HDR IGH, PRD_PMEMODEPT_DET IGD," & vbCrLf _
            & " INV_ITEM_MST INVMST, PRD_OPR_MST OPR, GEN_COMPANY_MST GEN, MAN_MACHINE_MST MMST, PAY_CONT_EMPLOYEE_MST EMP"

        ''WHERE CLAUSE...	
        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IGH.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " And IGH.AUTO_KEY_REF=IGD.AUTO_KEY_REF" & vbCrLf _
            & " And IGD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " And IGD.ITEM_CODE=INVMST.ITEM_CODE And IGH.BOOKTYPE<>'C'"

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQLDet = MakeSQLDet & vbCrLf & " AND GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " AND IGD.COMPANY_CODE=OPR.COMPANY_CODE(+)" & vbCrLf _
            & " AND IGD.OPR_CODE=OPR.OPR_CODE(+) "

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " AND IGD.COMPANY_CODE=MMST.COMPANY_CODE(+)" & vbCrLf _
            & " AND IGD.MACHINE_NO=MMST.MACHINE_NO(+) "

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " AND IGD.COMPANY_CODE=EMP.COMPANY_CODE(+)" & vbCrLf _
            & " AND IGD.OPERATOR_CODE=EMP.EMP_CODE(+) "

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If chkOprAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtOpr.Text, "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mOPRCode = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGD.OPR_CODE='" & MainClass.AllowSingleQuote(mOPRCode) & "'"
            End If
        End If

        If chkOperatorAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGD.OPERATOR_CODE='" & MainClass.AllowSingleQuote(txtOperator.Text) & "'"
        End If

        If cboMachineNo.Text <> "ALL" Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGD.MACHINE_NO='" & MainClass.AllowSingleQuote(cboMachineNo.Text) & "'"
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboShift.Text <> "ALL" Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.SHIFT_CODE='" & VB.Left(cboShift.Text, 1) & "'"
        End If

        If chkSPD.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.IS_SPD='Y'"
        End If

        If cboApproved.Text <> "ALL" Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.IS_APPROVED='" & VB.Left(cboApproved.Text, 1) & "'"
        End If

        If cboType.Text <> "ALL" Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.PROD_TYPE='" & VB.Left(cboType.Text, 1) & "'"
        End If

        ''- DECODE(IGH.SHIFT_CODE,'C',1,0)	

        If chkShowFinal.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLDet = MakeSQLDet & vbCrLf _
                & " AND (GETFINALOPRNEW(IGH.COMPANY_CODE, IGH.DEPT_CODE, IGD.ITEM_CODE,IGD.OPR_CODE,IGH.REF_DATE)='Y' OR IGD.OPR_CODE IS NULL)"
        End If

        If optDate(0).Checked = True Then
            MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.PROD_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.PROD_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.REF_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.REF_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If chkTime.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.PREP_TIME >=TO_DATE('" & txtTMFrom.Text & "', 'HH24:MI')" & vbCrLf & " AND IGH.PREP_TIME <=TO_DATE('" & txtTMTo.Text & "', 'HH24:MI')"
        End If

        '        MakeSQL = MakeSQL & vbCrLf _	
        ''                & " AND IH.REMOVAL_TIME>=TO_DATE('" & txtTMFrom.Text & "', 'HH24:MI')" & vbCrLf _	
        ''                & " AND IH.REMOVAL_TIME<=TO_DATE('" & txtTMTo.Text & "', 'HH24:MI')"	



        If OptOrderBy(0).Checked = True Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY IGH.REF_DATE ,IGH.AUTO_KEY_REF, IGD.SERIAL_NO"
        ElseIf OptOrderBy(1).Checked = True Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC, IGH.REF_DATE,IGH.AUTO_KEY_REF"
        End If
        'End If	
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLSumm() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mOPRCode As String
        Dim mItemCode As String

        Dim mSupplier As String
        Dim mEmployee As String

        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String

        ''SELECT CLAUSE...	



        MakeSQLSumm = " SELECT ''," & vbCrLf _
            & " ''," & vbCrLf _
            & " '',''," & vbCrLf _
            & " IGH.DEPT_CODE, IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " IGD.ITEM_UOM, '', "

        If OptDetSumm(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & "'','',"
        Else
            MakeSQLSumm = MakeSQLSumm & " IGD.OPERATOR_CODE, EMP.EMP_NAME,"
        End If

        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " OPR.OPR_DESC , " & vbCrLf _
            & " TO_CHAR(SUM(IGD.PROD_QTY)), TO_CHAR(SUM(IGD.SCRAP_QTY)), TO_CHAR(SUM(IGD.PROD_QTY-IGD.SCRAP_QTY)),"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114 Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " TO_CHAR(SUM(IGD.REWORK_QTY)), TO_CHAR(SUM(IGD.MR_QTY)),"
        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " 0, 0,"
        End If

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " IGD.MACHINE_NO, MMST.MACHINE_DESC, SUM(MACHINE_WORKING_HOURS) MACHINE_WORKING_HOURS,"

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " 0, '', '', '', 0, 0, 0, '',"

        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " TO_CHAR(SUM(IGD.PROD_QTY * (MAT_LEN/1000) * (MAT_WIDTH/1000))), TO_CHAR(SUM(IGD.SCRAP_QTY * (MAT_LEN/1000) * (MAT_WIDTH/1000))), TO_CHAR(SUM((IGD.PROD_QTY-IGD.SCRAP_QTY) * (MAT_LEN/1000) * (MAT_WIDTH/1000))),"

        ''& " TO_CHAR(IGD.PROD_QTY * (MAT_LEN/1000) * (MAT_WIDTH/1000)), TO_CHAR(IGD.SCRAP_QTY * (MAT_LEN/1000) * (MAT_WIDTH/1000)), TO_CHAR((IGD.PROD_QTY-IGD.SCRAP_QTY)* (MAT_LEN/1000) * (MAT_WIDTH/1000)),"


        MakeSQLSumm = MakeSQLSumm & vbCrLf & " '','',GEN.COMPANY_SHORTNAME,''"

        ''FROM CLAUSE...	
        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " FROM PRD_PMEMODEPT_HDR IGH, PRD_PMEMODEPT_DET IGD," & vbCrLf _
            & " INV_ITEM_MST INVMST, PRD_OPR_MST OPR, GEN_COMPANY_MST GEN, MAN_MACHINE_MST MMST, PAY_CONT_EMPLOYEE_MST EMP"



        ''WHERE CLAUSE...	
        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IGH.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " AND IGH.AUTO_KEY_REF=IGD.AUTO_KEY_REF" & vbCrLf _
            & " AND IGH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE  AND IGH.BOOKTYPE<>'C'"


        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " AND IGD.COMPANY_CODE=OPR.COMPANY_CODE(+)" & vbCrLf _
            & " AND IGD.OPR_CODE=OPR.OPR_CODE(+) "

        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " AND IGD.COMPANY_CODE=MMST.COMPANY_CODE(+)" & vbCrLf _
            & " AND IGD.MACHINE_NO=MMST.MACHINE_NO(+) "

        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " AND IGD.COMPANY_CODE=EMP.COMPANY_CODE(+)" & vbCrLf _
            & " AND IGD.OPERATOR_CODE=EMP.EMP_CODE(+) "

        ''- DECODE(IGH.SHIFT_CODE,'C',1,0)	

        If chkShowFinal.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND (GETFINALOPRNEW(IGH.COMPANY_CODE, IGH.DEPT_CODE, IGD.ITEM_CODE,IGD.OPR_CODE,IGH.REF_DATE)='Y' OR IGD.OPR_CODE IS NULL)"
        End If

        If optDate(0).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND IGH.PROD_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.PROD_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND IGH.REF_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.REF_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If chkOprAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtOpr.Text, "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mOPRCode = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGD.OPR_CODE='" & MainClass.AllowSingleQuote(mOPRCode) & "'"
            End If
        End If

        If chkOperatorAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGD.OPERATOR_CODE='" & MainClass.AllowSingleQuote(txtOperator.Text) & "'"
        End If

        If cboMachineNo.Text <> "ALL" Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGD.MACHINE_NO='" & MainClass.AllowSingleQuote(cboMachineNo.Text) & "'"
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If chkSPD.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.IS_SPD='Y'"
        End If

        If cboApproved.Text <> "ALL" Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.IS_APPROVED='" & VB.Left(cboApproved.Text, 1) & "'"
        End If

        If cboShift.Text <> "ALL" Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.SHIFT_CODE='" & VB.Left(cboShift.Text, 1) & "'"
        End If

        If cboType.Text <> "ALL" Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.PROD_TYPE='" & VB.Left(cboType.Text, 1) & "'"
        End If

        If OptDetSumm(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "GROUP BY IGH.DEPT_CODE, IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC,IGD.ITEM_UOM,OPR.OPR_DESC,GEN.COMPANY_SHORTNAME,IGD.MACHINE_NO, MACHINE_DESC "
        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "GROUP BY IGH.DEPT_CODE, IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC,IGD.ITEM_UOM,IGD.OPERATOR_CODE,EMP.EMP_NAME,OPR.OPR_DESC,GEN.COMPANY_SHORTNAME,IGD.MACHINE_NO, MACHINE_DESC"
        End If
        If OptOrderBy(0).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY IGH.DEPT_CODE, IGD.MACHINE_NO,IGD.ITEM_CODE,INVMST.ITEM_SHORT_DESC"
        ElseIf OptOrderBy(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY IGH.DEPT_CODE, IGD.MACHINE_NO, INVMST.ITEM_SHORT_DESC,IGD.ITEM_CODE"
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

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.text = MasterNo
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
            Exit Sub
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
            Exit Sub
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

        Dim CntLst As Long

        cboDept.Items.Clear()

        SqlStr = "SELECT DEPT_DESC FROM PAY_DEPT_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY DEPT_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDept.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDept.Items.Add(RS.Fields("DEPT_DESC").Value)
                RS.MoveNext()
            Loop
        End If


        cboMachineNo.Items.Clear()

        SqlStr = "SELECT MACHINE_NO FROM MAN_MACHINE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY MACHINE_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboMachineNo.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboMachineNo.Items.Add(RS.Fields("MACHINE_NO").Value)
                RS.MoveNext()
            Loop
        End If

        cboMachineNo.SelectedIndex = 0

        cboShift.Items.Clear()
        cboShift.Items.Add("ALL")
        cboShift.Items.Add("A")
        cboShift.Items.Add("B")
        cboShift.Items.Add("C")
        cboShift.SelectedIndex = 0

        cboType.Items.Clear()
        cboType.Items.Add("ALL")
        cboType.Items.Add("PRODUCTION")
        cboType.Items.Add("JOBWORK")
        cboType.SelectedIndex = 0

        cboApproved.Items.Clear()
        cboApproved.Items.Add("ALL")
        cboApproved.Items.Add("YES")
        cboApproved.Items.Add("NO")
        cboApproved.SelectedIndex = 1

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        Dim mCompanyName As String
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                mCompanyName = IIf(IsDBNull(RS.Fields("COMPANY_SHORTNAME").Value), "", RS.Fields("COMPANY_SHORTNAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(mCompanyName = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                'lstCompanyName.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub lstCompanyName_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstCompanyName.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCompanyName.GetItemChecked(0) = True Then
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCompanyName.GetItemChecked(e.Index - 1) = False Then
                    lstCompanyName.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtOperator_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOperator.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtOperator_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOperator.DoubleClick
        SearchOperator()
    End Sub

    Private Sub txtOperator_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOperator.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOperator.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOperator_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOperator.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchOPR()
    End Sub

    Private Sub txtOperator_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOperator.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtOperator.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(txtOperator.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtOperator.Text = UCase(Trim(txtOperator.Text))
            txtOperatorName.Text = MasterNo
        Else
            If MainClass.ValidateWithMasterTable(txtOperator.Text, "EMP_CODE", "EMP_NAME", "PAY_CONT_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                txtOperator.Text = UCase(Trim(txtOperator.Text))
                txtOperatorName.Text = MasterNo
            Else
                MsgInformation("No Such Operation in Operation Master")
                Cancel = True
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchOperator_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchOperator.Click
        SearchOperator()
    End Sub

    Private Sub chkOperatorAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOperatorAll.CheckStateChanged
        Call PrintStatus(False)
        If chkOperatorAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtOperator.Enabled = False
            cmdSearchOperator.Enabled = False
        Else
            txtOperator.Enabled = True
            cmdSearchOperator.Enabled = True
        End If
    End Sub

    Private Sub txtOpr_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOpr.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtOpr_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOpr.DoubleClick
        SearchOPR()
    End Sub

    Private Sub txtOpr_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOpr.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOpr.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOpr_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOpr.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchOPR()
    End Sub

    Private Sub txtOpr_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOpr.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtOpr.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(txtOpr.Text, "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtOpr.Text = UCase(Trim(txtOpr.Text))
        Else
            MsgInformation("No Such Operation in Operation Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchOPR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchOpr.Click
        SearchOPR()
    End Sub

    Private Sub chkOprAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOprAll.CheckStateChanged
        Call PrintStatus(False)
        If chkOprAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtOpr.Enabled = False
            cmdSearchOPR.Enabled = False
        Else
            txtOpr.Enabled = True
            cmdSearchOPR.Enabled = True
        End If
    End Sub
End Class
