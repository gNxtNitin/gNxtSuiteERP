Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamProdInsp
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColDeptCode As Short = 2
    Private Const ColItemCode As Short = 3
    Private Const ColItemDesc As Short = 4
    Private Const ColUnit As Short = 5
    Private Const ColProdQty As Short = 6
    Private Const ColFault1 As Short = 7
    Private Const ColFault2 As Short = 8
    Private Const ColFault3 As Short = 9
    Private Const ColFault4 As Short = 10
    Private Const ColFault5 As Short = 11
    Private Const ColFault6 As Short = 12
    Private Const ColFault7 As Short = 13
    Private Const ColFault8 As Short = 14
    Private Const ColFault9 As Short = 15
    Private Const ColTotQty As Short = 16
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Function GetFaultQty(ByRef mItemCode As String, ByRef mDeptCode As String, ByRef mFault1Qty As Double, ByRef mFault2Qty As Double, ByRef mFault3Qty As Double, ByRef mFault4Qty As Double, ByRef mFault5Qty As Double, ByRef mFault6Qty As Double, ByRef mFault7Qty As Double, ByRef mFault8Qty As Double, ByRef mFault9Qty As Double, ByRef mFaultTotQty As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        mFault1Qty = 0
        mFault2Qty = 0
        mFault3Qty = 0
        mFault4Qty = 0
        mFault5Qty = 0
        mFault6Qty = 0
        mFault7Qty = 0
        mFault8Qty = 0
        mFault9Qty = 0
        mFaultTotQty = 0

        SqlStr = " SELECT " & vbCrLf & " SUM(CASE WHEN FAULT_TYPE='1' THEN RECD_QTY ELSE 0 END) AS FAULT1, " & vbCrLf & " SUM(CASE WHEN FAULT_TYPE='2' THEN RECD_QTY ELSE 0 END) AS FAULT2, " & vbCrLf & " SUM(CASE WHEN FAULT_TYPE='3' THEN RECD_QTY ELSE 0 END) AS FAULT3, " & vbCrLf & " SUM(CASE WHEN FAULT_TYPE='4' THEN RECD_QTY ELSE 0 END) AS FAULT4, " & vbCrLf & " SUM(CASE WHEN FAULT_TYPE='5' THEN RECD_QTY ELSE 0 END) AS FAULT5, " & vbCrLf & " SUM(CASE WHEN FAULT_TYPE='6' THEN RECD_QTY ELSE 0 END) AS FAULT6, " & vbCrLf & " SUM(CASE WHEN FAULT_TYPE='7' THEN RECD_QTY ELSE 0 END) AS FAULT7, " & vbCrLf & " SUM(CASE WHEN FAULT_TYPE='8' THEN RECD_QTY ELSE 0 END) AS FAULT8, " & vbCrLf & " SUM(CASE WHEN FAULT_TYPE='0' OR FAULT_TYPE='9' THEN RECD_QTY ELSE 0 END) AS FAULT9, " & vbCrLf & " SUM(RECD_QTY) AS TOTFAULT "

        SqlStr = SqlStr & vbCrLf _
            & " FROM PRD_SENDBACKFORRWK_DET" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
            & " AND FROM_DEPT='" & MainClass.AllowSingleQuote(mDeptCode) & "'" & vbCrLf _
            & " AND SB_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SB_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mFault1Qty = IIf(IsDbNull(RsTemp.Fields("FAULT1").Value), 0, RsTemp.Fields("FAULT1").Value)
            mFault2Qty = IIf(IsDbNull(RsTemp.Fields("FAULT2").Value), 0, RsTemp.Fields("FAULT2").Value)
            mFault3Qty = IIf(IsDbNull(RsTemp.Fields("FAULT3").Value), 0, RsTemp.Fields("FAULT3").Value)
            mFault4Qty = IIf(IsDbNull(RsTemp.Fields("FAULT4").Value), 0, RsTemp.Fields("FAULT4").Value)
            mFault5Qty = IIf(IsDbNull(RsTemp.Fields("FAULT5").Value), 0, RsTemp.Fields("FAULT5").Value)
            mFault6Qty = IIf(IsDbNull(RsTemp.Fields("FAULT6").Value), 0, RsTemp.Fields("FAULT6").Value)
            mFault7Qty = IIf(IsDbNull(RsTemp.Fields("FAULT7").Value), 0, RsTemp.Fields("FAULT7").Value)
            mFault8Qty = IIf(IsDbNull(RsTemp.Fields("FAULT8").Value), 0, RsTemp.Fields("FAULT8").Value)
            mFault9Qty = IIf(IsDbNull(RsTemp.Fields("FAULT9").Value), 0, RsTemp.Fields("FAULT9").Value)
            mFaultTotQty = IIf(IsDbNull(RsTemp.Fields("TOTFAULT").Value), 0, RsTemp.Fields("TOTFAULT").Value)
        End If
        GetFaultQty = True
        Exit Function
ErrPart:
        GetFaultQty = False
    End Function

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
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


        mTitle = Me.Text
        mTitle = mTitle & "[" & cboDept.Text & "]"

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ProdInspReport.rpt"

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr


        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

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
    Private Sub frmParamProdInsp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Production Inspection Report"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamProdInsp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)


        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
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
    Private Sub frmParamProdInsp_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamProdInsp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
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

        If TxtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtItemName.Text = UCase(Trim(TxtItemName.Text))
        Else
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
            .MaxCols = ColTotQty
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

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDeptCode, 4)

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

            For cntCol = ColProdQty To ColTotQty
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9)
            Next

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
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
        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Call CalcTot()
        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mItemCode As String

        ''SELECT CLAUSE...

        MakeSQL = " SELECT ''," & vbCrLf & " IGH.DEPT_CODE, IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf & " IGD.ITEM_UOM, TO_CHAR(SUM(IGD.PROD_QTY)), " & vbCrLf & " TO_CHAR(SUM(IGD.REWORK_QTY)),"

        MakeSQL = MakeSQL & vbCrLf & " 0, 0, 0, 0, 0, " & vbCrLf & " 0, 0, 0, 0, 0 "

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM PRD_PMEMODEPT_HDR IGH, PRD_PMEMODEPT_DET IGD," & vbCrLf & " INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IGH.AUTO_KEY_REF=IGD.AUTO_KEY_REF" & vbCrLf & " AND IGH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE "

        MakeSQL = MakeSQL & vbCrLf & " AND IGH.PROD_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IGH.PROD_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        ''PRODUCTION DATE   ''12-14-2007

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboType.Text <> "ALL" Then
            MakeSQL = MakeSQL & vbCrLf & "AND IGH.PROD_TYPE='" & VB.Left(cboType.Text, 1) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & "GROUP BY IGH.DEPT_CODE, IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC,IGD.ITEM_UOM "

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY IGH.DEPT_CODE, IGD.ITEM_CODE,INVMST.ITEM_SHORT_DESC"

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
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
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

        cboType.Items.Clear()
        cboType.Items.Add("ALL")
        cboType.Items.Add("PRODUCTION")
        cboType.Items.Add("JOBWORK")
        cboType.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtTMTo_Change()

    End Sub

    Private Sub CalcTot()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mDeptCode As String
        Dim mFault1Qty As Double
        Dim mFault2Qty As Double
        Dim mFault3Qty As Double
        Dim mFault4Qty As Double
        Dim mFault5Qty As Double
        Dim mFault6Qty As Double
        Dim mFault7Qty As Double
        Dim mFault8Qty As Double
        Dim mFault9Qty As Double
        Dim mFaultTotQty As Double

        With SprdMain
            For cntRow = 1 To .MaxRows

                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColDeptCode
                mDeptCode = Trim(.Text)

                If GetFaultQty(mItemCode, mDeptCode, mFault1Qty, mFault2Qty, mFault3Qty, mFault4Qty, mFault5Qty, mFault6Qty, mFault7Qty, mFault8Qty, mFault9Qty, mFaultTotQty) = True Then

                    .Col = ColFault1
                    .Text = VB6.Format(mFault1Qty, "0.00")

                    .Col = ColFault2
                    .Text = VB6.Format(mFault2Qty, "0.00")

                    .Col = ColFault3
                    .Text = VB6.Format(mFault3Qty, "0.00")

                    .Col = ColFault4
                    .Text = VB6.Format(mFault4Qty, "0.00")

                    .Col = ColFault5
                    .Text = VB6.Format(mFault5Qty, "0.00")

                    .Col = ColFault6
                    .Text = VB6.Format(mFault6Qty, "0.00")

                    .Col = ColFault7
                    .Text = VB6.Format(mFault7Qty, "0.00")

                    .Col = ColFault8
                    .Text = VB6.Format(mFault8Qty, "0.00")

                    .Col = ColFault9
                    .Text = VB6.Format(mFault9Qty, "0.00")

                    .Col = ColTotQty
                    .Text = VB6.Format(mFaultTotQty, "0.00")

                End If
            Next
        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
