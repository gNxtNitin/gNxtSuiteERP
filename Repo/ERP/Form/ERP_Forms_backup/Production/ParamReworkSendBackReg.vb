Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamReworkSendBackReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const ColFromDeptCode As Short = 4
    Private Const ColToDeptCode As Short = 5
    Private Const ColItemCode As Short = 6
    Private Const ColItemDesc As Short = 7
    Private Const ColUnit As Short = 8
    Private Const ColIssueQty As Short = 9
    Private Const ColRecdQty As Short = 10
    Private Const ColFaultType As Short = 11
    Private Const ColMKEY As Short = 12
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
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
            mTitle = mTitle & "[" & cboDept.Text & "]" & " [" & cboShift.Text & "]"
            '        If OptOrderBy(0).Value = True Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MatSBReg.rpt"
            '        Else
            '            Report1.ReportFileName = App.path & "\Reports\MatIssueReg.rpt"
            '        End If
            SqlStr = MakeSQLDet
        Else
            mTitle = "Production Register " & " [ Summarised ] "
            mTitle = mTitle & "[" & cboDept.Text & "]" & "[" & cboShift.Text & "]"

            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MatSBRegSumm.rpt"
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
    Private Sub frmParamReworkSendBackReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    Me.text = "Material Movement Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamReworkSendBackReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtItemName.Enabled = False
        cmdsearch.Enabled = False

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
    Private Sub frmParamReworkSendBackReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamReworkSendBackReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

        frmSBRework.MdiParent = Me.MdiParent
        frmSBRework.lblBookType.Text = "R"
        frmSBRework.Show()
        frmSBRework.frmSBRework_Activated(Nothing, New System.EventArgs())

        frmSBRework.txtSlipNo.Text = CStr(xIssueNo)

        frmSBRework.txtSlipNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
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
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
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
    Private Sub txtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If txtItemName.Text = "" Then GoTo EventExitSub

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

            .Col = ColFromDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColFromDeptCode, 4)

            .Col = ColToDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColToDeptCode, 4)

            .Col = ColFaultType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColFaultType, 10)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = False
            ElseIf OptDetSumm(1).Checked = True Then
                .ColHidden = True
            End If


            .Col = ColIssueQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColIssueQty, 9)

            .Col = ColRecdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColRecdQty, 9)


            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
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


        MakeSQLDet = " SELECT ''," & vbCrLf & " IGH.AUTO_KEY_SBRWK, TO_CHAR(IGH.SB_DATE,'DD/MM/YYYY'), " & vbCrLf & " IGH.FROM_DEPT, IGH.TO_DEPT, " & vbCrLf & " IGD.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, INVMST.ISSUE_UOM, " & vbCrLf & " TO_CHAR(IGD.SB_QTY), TO_CHAR(IGD.RECD_QTY), FAULT_TYPE," & vbCrLf & " IGH.AUTO_KEY_SBRWK"

        ''FROM CLAUSE...
        MakeSQLDet = MakeSQLDet & vbCrLf & " FROM PRD_SENDBACKFORRWK_HDR IGH, PRD_SENDBACKFORRWK_DET IGD," & vbCrLf & " INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQLDet = MakeSQLDet & vbCrLf & " WHERE " & vbCrLf & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IGH.AUTO_KEY_SBRWK=IGD.AUTO_KEY_SBRWK" & vbCrLf & " AND IGD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IGD.PRODUCT_CODE=INVMST.ITEM_CODE "


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGD.PRODUCT_CODE='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.TO_DEPT='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboShift.Text <> "ALL" Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.SHIFT_CODE='" & VB.Left(cboShift.Text, 1) & "'"
        End If

        If cboStatus.SelectedIndex = 1 Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.STATUS='P'"
        ElseIf cboStatus.SelectedIndex = 2 Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.STATUS='C'"
        End If

        MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.SB_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.SB_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkTime.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.PREP_TIME >=TO_DATE('" & txtTMFrom.Text & "', 'HH24:MI')" & vbCrLf & " AND IGH.PREP_TIME <=TO_DATE('" & txtTMTo.Text & "', 'HH24:MI')"
        End If

        '        MakeSQL = MakeSQL & vbCrLf _
        ''                & " AND IH.REMOVAL_TIME>=TO_DATE('" & txtTMFrom.Text & "', 'HH24:MI')" & vbCrLf _
        ''                & " AND IH.REMOVAL_TIME<=TO_DATE('" & txtTMTo.Text & "', 'HH24:MI')"



        If OptOrderBy(0).Checked = True Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY IGH.SB_DATE ,IGH.AUTO_KEY_SBRWK, IGD.PRODUCT_CODE"
        ElseIf OptOrderBy(1).Checked = True Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC, IGH.SB_DATE,IGH.AUTO_KEY_SBRWK"
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
        ''SELECT CLAUSE...

        MakeSQLSumm = " SELECT ''," & vbCrLf & " '', '', " & vbCrLf & " IGH.FROM_DEPT, IGH.TO_DEPT, " & vbCrLf & " IGD.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf & " INVMST.ISSUE_UOM, TO_CHAR(SUM(IGD.SB_QTY)), " & vbCrLf & " TO_CHAR(SUM(IGD.RECD_QTY)),''"

        ''FROM CLAUSE...
        MakeSQLSumm = MakeSQLSumm & vbCrLf & " FROM PRD_SENDBACKFORRWK_HDR IGH, PRD_SENDBACKFORRWK_DET IGD," & vbCrLf & " INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQLSumm = MakeSQLSumm & vbCrLf & " WHERE " & vbCrLf & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IGH.AUTO_KEY_SBRWK=IGD.AUTO_KEY_SBRWK" & vbCrLf & " AND IGH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IGD.PRODUCT_CODE=INVMST.ITEM_CODE "

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND IGH.SB_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.SB_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGD.PRODUCT_CODE='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.TO_DEPT='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboShift.Text <> "ALL" Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.SHIFT_CODE='" & VB.Left(cboShift.Text, 1) & "'"
        End If

        If cboStatus.SelectedIndex = 1 Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.STATUS='P'"
        ElseIf cboStatus.SelectedIndex = 2 Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.STATUS='C'"
        End If

        MakeSQLSumm = MakeSQLSumm & vbCrLf & "GROUP BY IGH.FROM_DEPT, IGH.TO_DEPT, IGD.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC,INVMST.ISSUE_UOM "

        If OptOrderBy(0).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY IGH.FROM_DEPT, IGH.TO_DEPT, IGD.PRODUCT_CODE,INVMST.ITEM_SHORT_DESC"
        ElseIf OptOrderBy(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY IGH.FROM_DEPT, IGH.TO_DEPT, INVMST.ITEM_SHORT_DESC,IGD.PRODUCT_CODE"
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

        cboShift.Items.Clear()
        cboShift.Items.Add("ALL")
        cboShift.Items.Add("A")
        cboShift.Items.Add("B")
        cboShift.Items.Add("C")
        cboShift.SelectedIndex = 0

        cboStatus.Items.Clear()
        cboStatus.Items.Add("ALL")
        cboStatus.Items.Add("Pending")
        cboStatus.Items.Add("Complete")
        cboStatus.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
End Class
