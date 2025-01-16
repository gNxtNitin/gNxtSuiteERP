Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamProdWiseCons
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColProdCode As Short = 3
    Private Const ColProdName As Short = 4
    Private Const ColAmendWEF As Short = 5
    Private Const ColFromDate As Short = 6
    Private Const ColToDate As Short = 7
    Private Const ColDept As Short = 8
    Private Const ColProdQty As Short = 9
    Private Const ColStdQtyBOM As Short = 10
    Private Const ColConsQty As Short = 11

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

    Private Sub chkAllItemCode_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItemCode.CheckStateChanged
        Call PrintStatus(False)
        If chkAllItemCode.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemCode.Enabled = False
            cmdSearchItemCode.Enabled = False
        Else
            txtItemCode.Enabled = True
            cmdSearchItemCode.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
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

        mTitle = Me.Text

        mSubTitle = txtItemCode.Text & " : " & txtItemName.Text
        mSubTitle = mSubTitle & "  [ From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & " ]"

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ProdWiseCons.rpt"

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

    Private Sub cmdSearchItemCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItemCode.Click
        SearchItemCode()
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

    Private Sub frmParamProdWiseCons_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Production Wise Consumption (Child Part)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamProdWiseCons_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtItemName.Enabled = True
        cmdsearch.Enabled = False

        chkAllItemCode.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtItemCode.Enabled = True
        cmdSearchItemCode.Enabled = False

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

    Private Sub frmParamProdWiseCons_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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

    Private Sub frmParamProdWiseCons_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        SearchItemCode()
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItemCode()
    End Sub

    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtItemCode.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtItemCode.Text = UCase(Trim(txtItemCode.Text))
            txtItemName.Text = MasterNo
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

    Private Sub SearchItemCode()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtItemCode.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , SqlStr)
        If AcName <> "" Then
            txtItemCode.Text = AcName
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

        If txtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtItemName.Text = UCase(Trim(txtItemName.Text))
            txtItemCode.Text = MasterNo
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
            .MaxCols = ColConsQty
            .set_RowHeight(0, RowHeight * 1.2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 11)
            .ColHidden = True

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 30)
            .ColHidden = True

            .Col = ColProdCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColProdCode, 11)

            .Col = ColProdName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColProdName, 30)

            .Col = ColAmendWEF
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAmendWEF, 9)

            .Col = ColFromDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColFromDate, 9)

            .Col = ColToDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColToDate, 9)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDept, 4)

            For cntCol = ColProdQty To ColConsQty
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 11)
            Next

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
        Dim mPDIQty As Double


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If ShowIssue = False Then GoTo LedgError

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetIssueQty(ByRef pRMCode As String, ByRef pDeptCode As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mOPStock As Double
        Dim mIssueStock As Double
        Dim mClosingStock As Double

        GetIssueQty = 0
        mOPStock = 0
        mIssueStock = 0
        mClosingStock = 0

        mTable = ConInventoryTable

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        mTable = "INV_STOCK_REC_TRN" & RsCompany.fields("FYEAR").value
        '    ElseIf RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10 Or RsCompany.fields("COMPANY_CODE").value = 12 Then
        '        mTable = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.fields("COMPANY_CODE").value, "00") & RsCompany.fields("FYEAR").value
        '    Else
        '        mTable = "INV_STOCK_REC_TRN"
        '    End If
        SqlStr = " SELECT SUM(DECODE(ITEM_IO,'O',1,-1) * ITEM_QTY) AS ITEM_QTY " & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='" & ConPH & "'" & vbCrLf & " AND STOCK_TYPE='ST' " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pRMCode) & "' " & vbCrLf & " AND DEPT_CODE_FROM='" & MainClass.AllowSingleQuote(pDeptCode) & "' "

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE < TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mOPStock = System.Math.Abs(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value))
        End If

        SqlStr = " SELECT SUM(DECODE(ITEM_IO,'O',1,-1) * ITEM_QTY) AS ITEM_QTY " & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='" & ConPH & "'" & vbCrLf & " AND STOCK_TYPE='ST' " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pRMCode) & "' " & vbCrLf & " AND DEPT_CODE_FROM='" & MainClass.AllowSingleQuote(pDeptCode) & "' "

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mClosingStock = System.Math.Abs(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value))
        End If


        SqlStr = " SELECT SUM(DECODE(ITEM_IO,'O',1,-1) * ITEM_QTY) AS ITEM_QTY " & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='" & ConPH & "'" & vbCrLf & " AND STOCK_TYPE='ST' " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pRMCode) & "' " & vbCrLf & " AND DEPT_CODE_FROM='" & MainClass.AllowSingleQuote(pDeptCode) & "' " & vbCrLf & " AND REF_TYPE IN ('" & ConStockRefType_ISS & "','" & ConStockRefType_SRN & "')"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mIssueStock = System.Math.Abs(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value))
        End If


        GetIssueQty = mOPStock + mIssueStock - mClosingStock


        '    & vbCrLf _
        ''            & " AND REF_TYPE IN ('" & ConStockRefType_ISS & "','" & ConStockRefType_SRN & "')"


        Exit Function
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetProductionQty(ByRef mDeptCode As String, ByRef mProductCode As String, ByRef mFromDate As String, ByRef mToDate As String) As Double

        On Error GoTo ERR1
        Dim cntCol As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String

        mTable = ConInventoryTable

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        mTable = "INV_STOCK_REC_TRN" & RsCompany.fields("FYEAR").value
        '    ElseIf RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10 Or RsCompany.fields("COMPANY_CODE").value = 12 Then
        '        mTable = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.fields("COMPANY_CODE").value, "00") & RsCompany.fields("FYEAR").value
        '    Else
        '        mTable = "INV_STOCK_REC_TRN"
        '    End If

        SqlStr = " SELECT SUM(ITEM_QTY) AS ITEM_QTY " & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='" & ConPH & "'" & vbCrLf & " AND STOCK_TYPE='ST' " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' " & vbCrLf & " AND REF_TYPE='" & ConStockRefType_PMEMODEPT & "'" & vbCrLf & " AND DEPT_CODE_FROM='" & MainClass.AllowSingleQuote(mDeptCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetProductionQty = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.000"))
        Else
            GetProductionQty = 0
        End If

        Exit Function
ERR1:
        GetProductionQty = 0
        MsgInformation(Err.Description)
    End Function

    Private Function ShowIssue() As Boolean

        On Error GoTo ERR1
        Dim cntCol As Integer
        Dim mDeptCode As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim I As Integer
        Dim mProductionQty As Double
        Dim mProductCode As String = ""
        Dim mNextProductCode As String
        Dim mPrevProductCode As String
        Dim mAmendWEF As String = ""
        Dim mNextAmendWEF As String
        Dim mPrevAmendWEF As String
        Dim mFromDate As String
        Dim mToDate As String
        Dim mStdQtyBOM As Double

        mTable = ConInventoryTable

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        mTable = "INV_STOCK_REC_TRN" & RsCompany.fields("FYEAR").value
        '    ElseIf RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10 Or RsCompany.fields("COMPANY_CODE").value = 12 Then
        '        mTable = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.fields("COMPANY_CODE").value, "00") & RsCompany.fields("FYEAR").value
        '    Else
        '        mTable = "INV_STOCK_REC_TRN"
        '    End If

        SqlStr = " SELECT ID.RM_CODE AS RM_CODE, IH.PRODUCT_CODE AS PRODUCT_CODE, IH.WEF AS WEF, ID.DEPT_CODE AS DEPT_CODE, ID.STD_QTY AS STD_QTY" & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.RM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtItemCode.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.WEF = (SELECT MAX(WEF) FROM  PRD_NEWBOM_HDR WHERE COMPANY_CODE=IH.COMPANY_CODE AND PRODUCT_CODE=IH.PRODUCT_CODE AND WEF<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & " UNION "

        SqlStr = SqlStr & vbCrLf _
            & " SELECT ID.ALTER_RM_CODE AS RM_CODE, IH.PRODUCT_CODE AS PRODUCT_CODE, IH.WEF AS WEF, ID.DEPT_CODE AS DEPT_CODE, ID.ALTER_STD_QTY AS STD_QTY" & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ID.ALTER_RM_CODE='" & MainClass.AllowSingleQuote((txtItemCode.Text)) & "'" & vbCrLf _
            & " AND IH.WEF = (SELECT MAX(WEF) FROM  PRD_NEWBOM_HDR WHERE COMPANY_CODE=IH.COMPANY_CODE AND PRODUCT_CODE=IH.PRODUCT_CODE AND WEF<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" ''<= '" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY 1,2,3,4" ''RM_CODE, PRODUCT_CODE, WEF, DEPT_CODE "  AND STATUS='O'

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If RsTemp.EOF = False Then
                I = 0
                Do While RsTemp.EOF = False
                    I = I + 1

                    SprdMain.MaxRows = I
                    SprdMain.Row = I

                    SprdMain.Col = ColItemCode
                    SprdMain.Text = Trim(IIf(IsDbNull(.Fields("RM_CODE").Value), "", .Fields("RM_CODE").Value))

                    SprdMain.Col = ColItemName
                    If MainClass.ValidateWithMasterTable(.Fields("RM_CODE").Value, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "company_code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    Else
                        SprdMain.Text = ""
                    End If

                    SprdMain.Col = ColProdCode
                    SprdMain.Text = IIf(IsDbNull(.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value)

                    SprdMain.Col = ColProdName
                    If MainClass.ValidateWithMasterTable(.Fields("PRODUCT_CODE").Value, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "company_code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    Else
                        SprdMain.Text = ""
                    End If

                    SprdMain.Col = ColAmendWEF
                    SprdMain.Text = IIf(IsDbNull(.Fields("WEF").Value), "", .Fields("WEF").Value)

                    SprdMain.Col = ColDept
                    SprdMain.Text = IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)

                    SprdMain.Col = ColStdQtyBOM
                    SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("STD_QTY").Value), "", .Fields("STD_QTY").Value)))

                    RsTemp.MoveNext()
                Loop
            End If
        End With

        Dim counter As Short
        With SprdMain
            counter = .MaxRows
            For I = 1 To counter
StartRow:
                .Row = I

                mPrevProductCode = mProductCode
                mPrevAmendWEF = mAmendWEF

                .Col = ColProdCode
                mProductCode = Trim(.Text)

                If mProductCode = "" Then
                    Exit For
                End If

                .Col = ColAmendWEF
                mAmendWEF = Trim(.Text)

                If .Row = .MaxRows Then
                    mNextProductCode = ""
                    mNextAmendWEF = ""
                Else
                    .Row = I + 1

                    .Col = ColProdCode
                    mNextProductCode = Trim(.Text)

                    .Col = ColAmendWEF
                    mNextAmendWEF = Trim(.Text)
                End If

                .Row = I

                .Col = ColFromDate
                If mPrevProductCode = mProductCode Then
                    .Text = mAmendWEF
                Else
                    If DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mAmendWEF), CDate(txtDateFrom.Text)) > 0 Then
                        .Text = txtDateFrom.Text
                    Else
                        .Text = mAmendWEF
                    End If
                End If
                mFromDate = Trim(.Text)

                .Col = ColToDate
                If mProductCode = mNextProductCode Then
                    .Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(mNextAmendWEF)))
                Else
                    .Text = txtDateTo.Text
                End If
                mToDate = Trim(.Text)

                If DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mToDate), CDate(txtDateFrom.Text)) > 0 Then
                    .Action = SS_ACTION_DELETE_ROW
                    If .MaxRows > 1 Then .MaxRows = .MaxRows - 1
                    GoTo StartRow
                End If

                .Col = ColDept
                mDeptCode = Trim(.Text)

                .Col = ColProdQty
                mProductionQty = GetProductionQty(mDeptCode, mProductCode, mFromDate, mToDate)
                .Text = VB6.Format(mProductionQty, "0.000")

                .Col = ColStdQtyBOM
                mStdQtyBOM = CDbl(.Text)

                SprdMain.Col = ColConsQty
                .Text = VB6.Format(mProductionQty * mStdQtyBOM, "0.000")
            Next
        End With

        ShowIssue = True
        Exit Function
ERR1:
        ShowIssue = False
        MsgInformation(Err.Description)
        '    Resume
    End Function

    Private Function GetCommonFinishedGood(ByRef pProductCode As String, ByRef mRMCode As String) As String

        On Error GoTo ErrPart
        Dim pSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetCommonFinishedGood = ""
        pSqlStr = "SELECT DISTINCT PRODUCT_CODE " & vbCrLf & " FROM PRD_NEWBOM_DET ID " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE<>'" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf & " AND RM_CODE='" & MainClass.AllowSingleQuote(mRMCode) & "'" & vbCrLf & " ORDER BY " & vbCrLf & " PRODUCT_CODE"

        'AND STATUS='O'

        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                If GetCommonFinishedGood = "" Then
                    GetCommonFinishedGood = Trim(IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                Else
                    GetCommonFinishedGood = GetCommonFinishedGood & ", " & Trim(IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                End If
                RsTemp.MoveNext()
            Loop

            RsTemp = Nothing
            '        RsTemp.Close
        End If
        Exit Function
ErrPart:
        GetCommonFinishedGood = ""
    End Function

    Private Function GetCommonRMProdQty(ByRef pMainProductCode As String, ByRef mRMCode As String, ByRef mDeptCode As String, ByRef mCommonTo As String) As Double

        On Error GoTo ErrPart
        Dim pSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTemp1 As ADODB.Recordset = Nothing
        Dim pProductCode As String
        Dim mTable As String
        Dim SqlStr As String = ""
        Dim mStdQty As Double
        Dim mCommonProdQty As Double

        GetCommonRMProdQty = 0
        mCommonTo = ""

        mTable = ConInventoryTable

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        mTable = "INV_STOCK_REC_TRN" & RsCompany.fields("FYEAR").value
        '    ElseIf RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10 Or RsCompany.fields("COMPANY_CODE").value = 12 Then
        '        mTable = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.fields("COMPANY_CODE").value, "00") & RsCompany.fields("FYEAR").value
        '    Else
        '        mTable = "INV_STOCK_REC_TRN"
        '    End If

        pSqlStr = "SELECT DISTINCT IH.PRODUCT_CODE, ID.STD_QTY " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID " & vbCrLf & " WHERE IH.MKEY=ID.MKEY AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE<>'" & MainClass.AllowSingleQuote(pMainProductCode) & "'" & vbCrLf & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(mRMCode) & "' AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'" & vbCrLf & " AND IH.STATUS='O' ORDER BY " & vbCrLf & " IH.PRODUCT_CODE"

        'AND STATUS='O'

        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                pProductCode = Trim(IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                mStdQty = Val(IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value))

                SqlStr = " SELECT SUM(ITEM_QTY)  AS ITEM_QTY " & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='" & ConPH & "'" & vbCrLf & " AND STOCK_TYPE='ST' " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND REF_TYPE='" & ConStockRefType_PMEMODEPT & "'" & vbCrLf & " AND DEPT_CODE_FROM='" & MainClass.AllowSingleQuote(mDeptCode) & "'"

                SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

                SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp1.EOF = False Then
                    mCommonProdQty = CDbl(VB6.Format(IIf(IsDbNull(RsTemp1.Fields("ITEM_QTY").Value), 0, RsTemp1.Fields("ITEM_QTY").Value) * mStdQty, "0.00"))
                    GetCommonRMProdQty = GetCommonRMProdQty + CDbl(VB6.Format(mCommonProdQty, "0.00"))

                    If mCommonProdQty <> 0 Then
                        If mCommonTo = "" Then
                            mCommonTo = pProductCode
                        Else
                            mCommonTo = mCommonTo & ", " & pProductCode
                        End If
                    End If
                End If

                RsTemp.MoveNext()
            Loop

            RsTemp = Nothing
            '        RsTemp.Close
        End If

        Exit Function
ErrPart:
        GetCommonRMProdQty = 0
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkAllItemCode.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtItemCode.Text) = "" Then
                MsgInformation("Invaild Item Code")
                txtItemCode.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtItemCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Item Code")
                txtItemCode.Focus()
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
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then
        '        txtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
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
        '    If FYChk(CDate(txtDateTo.Text)) = False Then
        '        txtDateTo.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
