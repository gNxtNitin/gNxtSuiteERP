Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMrrRegYearly
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20


    Private Const colSupplier As Short = 1
    Private Const ColCategory As Short = 2
    Private Const ColItemCode As Short = 3
    Private Const ColItemDesc As Short = 4
    Private Const ColUnit As Short = 5

    Private Const ColMaxLevel As Short = 6
    Private Const ColMinLevel As Short = 7
    Private Const ColReorderLevel As Short = 8
    Private Const ColStock As Short = 9

    Private Const ColRate As Short = 10
    Private Const ColAcceptQty_Apr As Short = 11
    Private Const ColAmount_Apr As Short = 12
    Private Const ColAcceptQty_May As Short = 13
    Private Const ColAmount_May As Short = 14
    Private Const ColAcceptQty_Jun As Short = 15
    Private Const ColAmount_Jun As Short = 16
    Private Const ColAcceptQty_Jul As Short = 17
    Private Const ColAmount_Jul As Short = 18
    Private Const ColAcceptQty_Aug As Short = 19
    Private Const ColAmount_Aug As Short = 20
    Private Const ColAcceptQty_Sep As Short = 21
    Private Const ColAmount_Sep As Short = 22
    Private Const ColAcceptQty_Oct As Short = 23
    Private Const ColAmount_Oct As Short = 24
    Private Const ColAcceptQty_Nov As Short = 25
    Private Const ColAmount_Nov As Short = 26
    Private Const ColAcceptQty_Dec As Short = 27
    Private Const ColAmount_Dec As Short = 28
    Private Const ColAcceptQty_Jan As Short = 29
    Private Const ColAmount_Jan As Short = 30
    Private Const ColAcceptQty_Feb As Short = 31
    Private Const ColAmount_Feb As Short = 32
    Private Const ColAcceptQty_Mar As Short = 33
    Private Const ColAmount_Mar As Short = 34
    Private Const ColAcceptQty As Short = 35
    Private Const ColAmount As Short = 36



    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboRefType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRefType.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboRefType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRefType.SelectedIndexChanged
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

    Private Sub chkAllSupp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSupp.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSupplier.Enabled = False
            cmdsearchSupp.Enabled = False
        Else
            txtSupplier.Enabled = True
            cmdsearchSupp.Enabled = True
        End If
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportONMRR(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportONMRR(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportONMRR(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()
        mTitle = mTitle
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MrrRegYearly.rpt"

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

    Private Sub cmdsearchSupp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSupp.Click
        SearchSupplier()
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


    Private Sub frmParamMrrRegYearly_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "MRR Register - Yearly"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamMrrRegYearly_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

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



        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False
        txtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False



        Call PrintStatus(True)
        'Call FillPOCombo
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        cboRefType.Items.Clear()
        cboRefType.Items.Add("ALL")
        cboRefType.Items.Add("Purchase Order")
        cboRefType.Items.Add("Job Work Order")
        cboRefType.Items.Add("Invoice-Sale Return")
        cboRefType.Items.Add("Free of Cost")
        cboRefType.Items.Add("Returnable Gate Pass")
        cboRefType.Items.Add("Cash Purchase")
        cboRefType.Items.Add("1 : Job Work Return")
        cboRefType.Items.Add("2 : Sale Return Under Warranty")
        cboRefType.Items.Add("3 : Sale Return RM/BOP")
        cboRefType.SelectedIndex = 0

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



        lstMaterialType.Items.Clear()
        SqlStr = "SELECT GEN_DESC FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C' ORDER BY GEN_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            Do While RS.EOF = False
                lstMaterialType.Items.Add(RS.Fields("GEN_DESC").Value)
                lstMaterialType.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstMaterialType.SelectedIndex = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamMrrRegYearly_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamMrrRegYearly_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent

        Dim cntSearchRow As Integer
        Dim mSearchKey As String
        Dim mCol As Integer

        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            cntSearchRow = 1
            mSearchKey = ""
            mSearchKey = InputBox("Enter Search String :", "Search", mSearchKey)
            If mSearchKey <> "" Then
                MainClass.SearchIntoGrid(SprdMain, mCol, mSearchKey, cntSearchRow)
                cntSearchRow = cntSearchRow + 1
            End If
            SprdMain.Focus()
        End If
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
            TxtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchSupplier()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtSupplier.Text = AcName
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

        If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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
            .MaxCols = ColAmount
            .set_RowHeight(0, RowHeight * 1.2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = colSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colSupplier, 20)

            .Col = ColCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCategory, 20)

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

            For cntCol = ColMaxLevel To ColAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9)
            Next

            If OptShow(0).Checked = True Then
                .Col = colSupplier
                .ColHidden = False
                .Col = ColStock
                .ColHidden = True
                .Col = ColRate
                .ColHidden = False
                .Col = ColAmount_Apr
                .ColHidden = False
                .Col = ColAmount_May
                .ColHidden = False
                .Col = ColAmount_Jun
                .ColHidden = False
                .Col = ColAmount_Jul
                .ColHidden = False
                .Col = ColAmount_Aug
                .ColHidden = False
                .Col = ColAmount_Sep
                .ColHidden = False
                .Col = ColAmount_Oct
                .ColHidden = False
                .Col = ColAmount_Nov
                .ColHidden = False
                .Col = ColAmount_Dec
                .ColHidden = False
                .Col = ColAmount_Jan
                .ColHidden = False
                .Col = ColAmount_Feb
                .ColHidden = False
                .Col = ColAmount_Mar
                .ColHidden = False
                .Col = ColAmount
                .ColHidden = False
            Else
                .Col = colSupplier
                .ColHidden = True
                .Col = ColStock
                .ColHidden = False
                .Col = ColRate
                .ColHidden = True
                .Col = ColAmount_Apr
                .ColHidden = True
                .Col = ColAmount_May
                .ColHidden = True
                .Col = ColAmount_Jun
                .ColHidden = True
                .Col = ColAmount_Jul
                .ColHidden = True
                .Col = ColAmount_Aug
                .ColHidden = True
                .Col = ColAmount_Sep
                .ColHidden = True
                .Col = ColAmount_Oct
                .ColHidden = True
                .Col = ColAmount_Nov
                .ColHidden = True
                .Col = ColAmount_Dec
                .ColHidden = True
                .Col = ColAmount_Jan
                .ColHidden = True
                .Col = ColAmount_Feb
                .ColHidden = True
                .Col = ColAmount_Mar
                .ColHidden = True
                .Col = ColAmount
                .ColHidden = True
            End If

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            '        SprdMain.OperationMode = OperationModeSingle
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

        If OptShow(0).Checked = True Then
            SqlStr = MakeSQL()
        Else
            SqlStr = MakeSQLISS()
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

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mDivision As Double
        Dim mItemCode As String
        Dim mRMCatCode As String = ""
        Dim mRMCatCodeStr As String = ""
        Dim CntLst As Integer
        Dim mMaterialType As String

        ''SELECT CLAUSE...


        MakeSQL = " SELECT CMST.SUPP_CUST_NAME, GEN.GEN_DESC," & vbCrLf & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.ITEM_UOM, " & vbCrLf _
            & " INVMST.MINIMUM_QTY, INVMST.MAXIMUM_QTY, INVMST.REORDER_QTY, 0, "

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(MAX(ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='04' THEN ID.APPROVED_QTY ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='04' THEN ID.APPROVED_QTY ELSE 0 END*ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='05' THEN ID.APPROVED_QTY ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='05' THEN ID.APPROVED_QTY ELSE 0 END*ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='06' THEN ID.APPROVED_QTY ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='06' THEN ID.APPROVED_QTY ELSE 0 END*ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='07' THEN ID.APPROVED_QTY ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='07' THEN ID.APPROVED_QTY ELSE 0 END*ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='08' THEN ID.APPROVED_QTY ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='08' THEN ID.APPROVED_QTY ELSE 0 END*ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='09' THEN ID.APPROVED_QTY ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='09' THEN ID.APPROVED_QTY ELSE 0 END*ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='10' THEN ID.APPROVED_QTY ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='10' THEN ID.APPROVED_QTY ELSE 0 END*ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='11' THEN ID.APPROVED_QTY ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='11' THEN ID.APPROVED_QTY ELSE 0 END*ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='12' THEN ID.APPROVED_QTY ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='12' THEN ID.APPROVED_QTY ELSE 0 END*ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='01' THEN ID.APPROVED_QTY ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='01' THEN ID.APPROVED_QTY ELSE 0 END*ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='02' THEN ID.APPROVED_QTY ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='02' THEN ID.APPROVED_QTY ELSE 0 END*ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='03' THEN ID.APPROVED_QTY ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'MM')='03' THEN ID.APPROVED_QTY ELSE 0 END*ID.ITEM_RATE)),"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(ID.APPROVED_QTY)), " & vbCrLf & " TO_CHAR(SUM(ID.APPROVED_QTY*ID.ITEM_RATE))"


        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID," & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, INV_GENERAL_MST GEN"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND INVMST.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf & " AND INVMST.CATEGORY_CODE=GEN.GEN_CODE "


        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboRefType.SelectedIndex <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.REF_TYPE='" & VB.Left(cboRefType.Text, 1) & "' "
        End If

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        For CntLst = 0 To lstMaterialType.Items.Count - 1
            If lstMaterialType.GetItemChecked(CntLst) = True Then
                mMaterialType = VB6.GetItemString(lstMaterialType, CntLst)
                If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mRMCatCode = IIf(IsDbNull(MasterNo), "", MasterNo)
                End If
                mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
            End If
        Next

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " AND GEN.GEN_CODE IN " & mRMCatCodeStr & ""
        End If


        MakeSQL = MakeSQL & vbCrLf & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MakeSQL = MakeSQL & vbCrLf & "GROUP BY CMST.SUPP_CUST_NAME, GEN.GEN_DESC,ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.ITEM_UOM,INVMST.MINIMUM_QTY, INVMST.MAXIMUM_QTY, INVMST.REORDER_QTY "


        'ORDER CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME, GEN.GEN_DESC,ID.ITEM_CODE"
        'End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLISS() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mDivision As Double
        Dim mItemCode As String
        Dim mRMCatCode As String = ""
        Dim mRMCatCodeStr As String = ""
        Dim CntLst As Integer
        Dim mMaterialType As String

        ''SELECT CLAUSE...


        MakeSQLISS = " SELECT '' SUPP_CUST_NAME, GEN.GEN_DESC," & vbCrLf _
            & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.ITEM_UOM, " & vbCrLf _
            & " INVMST.MINIMUM_QTY, INVMST.MAXIMUM_QTY, INVMST.REORDER_QTY, 0, "

        MakeSQLISS = MakeSQLISS & vbCrLf & " '',"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.ISSUE_DATE,'MM')='04' THEN ID.ISSUE_QTY ELSE 0 END)), " & vbCrLf _
            & " 0,"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.ISSUE_DATE,'MM')='05' THEN ID.ISSUE_QTY ELSE 0 END)), " & vbCrLf _
            & " 0,"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.ISSUE_DATE,'MM')='06' THEN ID.ISSUE_QTY ELSE 0 END)), " & vbCrLf _
            & " 0,"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.ISSUE_DATE,'MM')='07' THEN ID.ISSUE_QTY ELSE 0 END)), " & vbCrLf _
            & " 0,"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.ISSUE_DATE,'MM')='08' THEN ID.ISSUE_QTY ELSE 0 END)), " & vbCrLf _
            & " 0,"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.ISSUE_DATE,'MM')='09' THEN ID.ISSUE_QTY ELSE 0 END)), " & vbCrLf _
            & " 0,"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.ISSUE_DATE,'MM')='10' THEN ID.ISSUE_QTY ELSE 0 END)), " & vbCrLf _
            & " 0,"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.ISSUE_DATE,'MM')='11' THEN ID.ISSUE_QTY ELSE 0 END)), " & vbCrLf _
            & " 0,"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.ISSUE_DATE,'MM')='12' THEN ID.ISSUE_QTY ELSE 0 END)), " & vbCrLf _
            & " 0,"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.ISSUE_DATE,'MM')='01' THEN ID.ISSUE_QTY ELSE 0 END)), " & vbCrLf _
            & " 0,"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.ISSUE_DATE,'MM')='02' THEN ID.ISSUE_QTY ELSE 0 END)), " & vbCrLf _
            & " 0,"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.ISSUE_DATE,'MM')='03' THEN ID.ISSUE_QTY ELSE 0 END)), " & vbCrLf _
            & " 0,"

        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " TO_CHAR(SUM(ID.ISSUE_QTY)), " & vbCrLf _
            & " 0"


        ''FROM CLAUSE...
        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID," & vbCrLf _
            & " INV_ITEM_MST INVMST, INV_GENERAL_MST GEN"

        ''WHERE CLAUSE...
        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND INVMST.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " AND INVMST.CATEGORY_CODE=GEN.GEN_CODE "


        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQLISS = MakeSQLISS & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        'If cboRefType.SelectedIndex <> 0 Then
        '    MakeSQLISS = MakeSQLISS & vbCrLf & "AND IH.REF_TYPE='" & VB.Left(cboRefType.Text, 1) & "' "
        'End If

        'If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '    If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mSupplier = MasterNo
        '        MakeSQLISS = MakeSQLISS & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
        '    End If
        'End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQLISS = MakeSQLISS & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        For CntLst = 0 To lstMaterialType.Items.Count - 1
            If lstMaterialType.GetItemChecked(CntLst) = True Then
                mMaterialType = VB6.GetItemString(lstMaterialType, CntLst)
                If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mRMCatCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
            End If
        Next

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            MakeSQLISS = MakeSQLISS & vbCrLf & " AND GEN.GEN_CODE IN " & mRMCatCodeStr & ""
        End If


        MakeSQLISS = MakeSQLISS & vbCrLf _
            & " AND IH.ISSUE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.ISSUE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQLISS = MakeSQLISS & vbCrLf & "GROUP BY GEN.GEN_DESC,ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.ITEM_UOM,INVMST.MINIMUM_QTY, INVMST.MAXIMUM_QTY, INVMST.REORDER_QTY "

        'ORDER CLAUSE...
        MakeSQLISS = MakeSQLISS & vbCrLf & "ORDER BY GEN.GEN_DESC,ID.ITEM_CODE"
        'End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtSupplier.Text) = "" Then
                MsgInformation("Invaild Supplier Name")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Supplier Name")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
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
    'Private Sub FillPOCombo()
    'On Error GoTo FillErr2
    'Dim SqlStr As String = ""
    'Dim RS As ADODB.Recordset=Nothing
    '
    ''    cboPurType.Clear
    ''    cboPurType.AddItem "ALL"
    ''    cboPurType.AddItem "Purchase Order"
    ''    cboPurType.AddItem "Work Order"
    ''    cboPurType.AddItem "Job Order"
    ''    cboPurType.ListIndex = 0
    ''
    ''    cboOrderType.Clear
    ''    cboOrderType.AddItem "ALL"
    ''    cboOrderType.AddItem "Close"
    ''    cboOrderType.AddItem "Open"
    ''    cboOrderType.ListIndex = 0
    '
    '
    ''    Exit Sub
    'FillErr2:
    '    MsgBox err.Description
    'End Sub
    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        SearchSupplier()
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSupplier()
    End Sub
End Class
