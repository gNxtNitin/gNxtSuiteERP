Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamCustCostVsActual
    Inherits System.Windows.Forms.Form

    'Dim PvtDBCn As ADODB.Connection		

    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    Private Const ColSNo As Short = 1
    Private Const ColCustomerCode As Short = 2
    Private Const ColCustomerName As Short = 3
    Private Const ColProdCode As Short = 4
    Private Const ColProdDesc As Short = 5
    Private Const ColCustPartNo As Short = 6
    Private Const ColCostWEF As Short = 7
    Private Const ColRMCode As Short = 8
    Private Const ColRMDesc As Short = 9
    Private Const ColRMUOM As Short = 10
    Private Const colStdQty As Short = 11
    Private Const ColScrapWt As Short = 12
    Private Const ColCostingRate As Short = 13
    Private Const ColSuppCode As Short = 14
    Private Const ColSuppName As Short = 15
    Private Const ColPONO As Short = 16
    Private Const ColPOWEF As Short = 17
    Private Const ColPORate As Short = 18
    Private Const ColRateDiff As Short = 19
    Private Const ColAmountDiff As Short = 20
    Private Const ColSOB As Short = 21

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub


    Private Sub chkAllCustomer_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCustomer.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCustomer.Enabled = False
            cmdsearchCustomer.Enabled = False
        Else
            txtCustomer.Enabled = True
            cmdsearchCustomer.Enabled = True
        End If
    End Sub

    Private Sub chkAllProduct_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllProduct.CheckStateChanged
        Call PrintStatus(False)
        If chkAllProduct.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtProduct.Enabled = False
            cmdsearchProduct.Enabled = False
        Else
            txtProduct.Enabled = True
            cmdsearchProduct.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()


        mTitle = "Customer Costing Vs Actual Register"
        mSubTitle = "As On Date : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") ''& " To : " & vb6.Format(txtDateTo.Text, "DD/MM/YYYY")		
        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\CCVsActReg.rpt"
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume		
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, False)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdSearchCustomer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCustomer.Click
        SearchDept()
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        MainClass.SearchGridMaster(txtProduct.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            txtProduct.Text = AcName
            txtProduct.Focus()
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchProduct_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchProduct.Click
        SearchItem()
    End Sub

    Private Sub cmdsearchRM_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchRM.Click
        SearchRM()
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1() = False Then GoTo ErrPart
        Call PrintStatus(True)
        ''MainClass.setfocusToCell SprdMain, mActiveRow, 4		
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamCustCostVsActual_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Customer Costing Vs Actual Register"

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamCustCostVsActual_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmParamCustCostVsActual_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError

        'Set PvtDBCn = New ADODB.Connection		
        'PvtDBCn.Open StrConn		

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)		
        'Me.Width = VB6.TwipsToPixelsX(11355)		


        chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked
        txtCustomer.Enabled = False
        cmdsearchCustomer.Enabled = False

        chkAllProduct.CheckState = System.Windows.Forms.CheckState.Checked
        txtProduct.Enabled = False
        cmdsearchProduct.Enabled = False

        chkAllRM.CheckState = System.Windows.Forms.CheckState.Checked
        txtRM.Enabled = False
        cmdsearchRM.Enabled = False

        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        '    txtDateTo.Text = Format(RunDate, "DD/MM/YYYY")		

        Call PrintStatus(True)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamCustCostVsActual_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamCustCostVsActual_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDateFrom.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDateFrom.Text) = True Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        SearchDept()
    End Sub
    Private Sub txtCustomer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchDept()
    End Sub
    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String


        If txtCustomer.Text = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCustomer.Text = UCase(Trim(txtCustomer.Text))
        Else
            MsgInformation("No Such Department in Department Master")
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
            .MaxCols = ColSOB
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColSNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColSNo, 6)
            .ColHidden = True

            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustomerCode, 6)

            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustomerName, 25)
            .ColsFrozen = ColCustomerName

            .Col = ColProdCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColProdCode, 6)

            .Col = ColProdDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColProdDesc, 25)

            .Col = ColCustPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustPartNo, 12)
            .ColHidden = False

            .Col = ColCostWEF
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColCostWEF, 12)
            .ColHidden = False

            .Col = ColRMCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColRMCode, 8)

            .Col = ColRMDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColRMDesc, 20)

            .Col = ColRMUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColRMUOM, 5)

            .Col = ColSuppCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColSuppCode, 8)

            .Col = ColSuppName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColSuppName, 20)

            For cntCol = colStdQty To ColCostingRate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .set_ColWidth(cntCol, 8)
            Next

            .Col = ColPONO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColPONO, 12)

            .Col = ColPOWEF
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColPOWEF, 8)
            .ColHidden = False

            For cntCol = ColPORate To ColAmountDiff
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .set_ColWidth(cntCol, 8)
            Next

            .Col = ColAmountDiff
            .ColHidden = False

            For cntCol = ColSOB To ColSOB
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 0
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999")
                .TypeFloatMin = CDbl("-999")
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .set_ColWidth(cntCol, 8)
            Next

            '        .Col = ColCustomerCode		
            '        .ColMerge = MergeCellsSettings.flexMergeRestrictColumns		
            '        .Col = ColCustomerName		
            '        .ColMerge = MergeAlways		
            '        .Col = ColProdCode		
            '        .ColMerge = MergeAlways		
            '        .Col = ColProdDesc		
            '        .ColMerge = MergeAlways		
            '        .Col = ColCustPartNo		
            '        .ColMerge = MergeAlways		
            '        .Col = ColCostWEF		
            '        .ColMerge = MergeAlways		
            '        .Col = ColRMCode		
            '        .ColMerge = MergeAlways		
            '        .Col = ColRMDesc		
            '        .ColMerge = MergeAlways		
            '        .Col = ColRMUOM		
            '        .ColMerge = MergeAlways		


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''' = OperationModeSingle		
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer
        Dim mCustCode As String
        Dim mProdCode As String
        Dim mCostingRate As Double
        Dim pPORate() As Double
        Dim pSOB() As Double
        Dim pPONO() As String
        Dim pPOWEF() As String
        Dim mSuppCode() As String
        Dim mSuppName As String
        Dim mRateDiff As Double
        Dim mAmountDiff As Double
        Dim mRMCode As String
        Dim mRMUOM As String

        Dim xSuppCode As String
        Dim xSuppName As String
        Dim lSuppName As String
        Dim xPONO As String
        Dim xPOWEF As String
        Dim xPORate As String
        Dim mMaxPORate As Double
        Dim mCnt As Integer
        Dim mMaxSupp As Integer

        Dim mCustName As String
        Dim mProdDesc As String
        Dim mProdPartNo As String
        Dim mWef As String
        Dim mSNO As Integer

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = MakeSQL()
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        I = 1


        If RsTemp.EOF = False Then
            With SprdMain
                Do While RsTemp.EOF = False
                    .Row = I
                    .Col = ColSNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("SUBROWNO").Value), 0, RsTemp.Fields("SUBROWNO").Value)
                    mSNO = IIf(IsDBNull(RsTemp.Fields("SUBROWNO").Value), 0, RsTemp.Fields("SUBROWNO").Value)

                    .Col = ColCustomerCode
                    .Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                    mCustCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)

                    .Col = ColCustomerName
                    .Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    mCustName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                    .Col = ColProdCode
                    .Text = IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value)
                    mProdCode = IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value)

                    .Col = ColProdDesc
                    .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
                    mProdDesc = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                    .Col = ColCustPartNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)
                    mProdPartNo = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)

                    .Col = ColCostWEF
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value), "DD/MM/YYYY")
                    mWef = VB6.Format(IIf(IsDBNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value), "DD/MM/YYYY")

                    .Col = ColRMCode
                    .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    mRMCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                    .Col = ColRMDesc
                    .Text = IIf(IsDBNull(RsTemp.Fields("RM_SHORT_DESC").Value), "", RsTemp.Fields("RM_SHORT_DESC").Value)

                    .Col = ColRMUOM
                    .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                    mRMUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                    .Col = colStdQty
                    .Text = IIf(IsDBNull(RsTemp.Fields("GROSS_WT").Value), 0, RsTemp.Fields("GROSS_WT").Value)

                    .Col = ColScrapWt
                    .Text = IIf(IsDBNull(RsTemp.Fields("SCRAP_WT").Value), 0, RsTemp.Fields("SCRAP_WT").Value)

                    .Col = ColCostingRate
                    .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
                    mCostingRate = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)

                    Dim pSuppCode(0) As Object
                    ReDim pPONO(0)
                    ReDim pPOWEF(0)
                    ReDim pPORate(0)
                    ReDim pSOB(0)

                    mSuppName = ""
                    mMaxPORate = 0

                    If GetPODetail(mRMCode, mRMUOM, mSuppCode, pPONO, pPOWEF, pPORate, pSOB, mMaxSupp) = False Then GoTo LedgError

                    For mCnt = 0 To mMaxSupp
                        .Row = I

                        .Col = ColSNo
                        .Text = mSNO & "." & mCnt

                        .Col = ColCustomerCode
                        .Text = mCustCode

                        .Col = ColCustomerName
                        .Text = mCustName

                        .Col = ColProdCode
                        .Text = mProdCode

                        .Col = ColProdDesc
                        .Text = mProdDesc

                        .Col = ColCustPartNo
                        .Text = mProdPartNo

                        .Col = ColCostWEF
                        .Text = mWef

                        .Col = ColSuppCode
                        .Text = Trim(mSuppCode(mCnt))

                        .Col = ColSuppName
                        xSuppName = ""
                        If MainClass.ValidateWithMasterTable(mSuppCode(mCnt), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            xSuppName = Trim(MasterNo)
                        End If
                        .Text = Trim(xSuppName)

                        .Col = ColPONO
                        .Text = pPONO(mCnt)

                        .Col = ColPOWEF
                        .Text = pPOWEF(mCnt)

                        .Col = ColPORate
                        .Text = CStr(pPORate(mCnt)) ''Format(pPORate, "0.000")		
                        mMaxPORate = pPORate(mCnt)

                        .Col = ColSOB
                        .Text = CStr(pSOB(mCnt)) ''Format(pPORate, "0.000")		

                        .Col = ColRateDiff
                        .Text = VB6.Format(mCostingRate - mMaxPORate, "0.000")

                        If mCnt < mMaxSupp Then
                            I = I + 1
                            .MaxRows = I
                        End If

                    Next

                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        I = I + 1
                        .MaxRows = I
                    End If
                Loop
            End With
        End If

        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"		
        '		
        '    With SprdMain		
        '        For I = 1 To .MaxRows		
        '            .Row = I		
        '            .Col = ColCustomerCode		
        '            mCustCode = Trim(.Text)		
        '		
        '            .Col = ColProdCode		
        '            mProdCode = Trim(.Text)		
        '		
        '            .Col = ColProdCode		
        '            mProdCode = Trim(.Text)		
        '		
        '            .Col = ColCostingRate		
        '            mCostingRate = Val(.Text)		
        '		
        '            .Col = ColRMCode		
        '            mRMCode = Trim(.Text)		
        '		
        '            .Col = ColRMUOM		
        '            mRMUOM = Trim(.Text)		
        '		
        '            ReDim pSuppCode(0)		
        '            ReDim pPONO(0)		
        '            ReDim pPOWEF(0)		
        '            ReDim pPORate(0)		
        '            ReDim pSOB(0)		
        '		
        '            mSuppName = ""		
        '            mMaxPORate = 0		
        '		
        '            If GetPODetail(mRMCode, mRMUOM, mSuppCode(), pPONO(), pPOWEF(), pPORate(), pSOB(), mMaxSupp) = False Then GoTo LedgError		
        '		
        '            .MaxRows = .MaxRows + mMaxSupp		
        '		
        '            For mCnt = 0 To mMaxSupp		
        '                .Row = I		
        '                .Col = ColSuppCode		
        '                .Text = Trim(mSuppCode(mCnt))		
        '		
        '                .Col = ColSuppName		
        '                xSuppName = ""		
        '                If MainClass.ValidateWithMasterTable(mSuppCode(mCnt), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then		
        '                    xSuppName = Trim(MasterNo)		
        '                End If		
        '                .Text = Trim(xSuppName)		
        '		
        '                .Col = ColPONO		
        '                .Text = pPONO(mCnt)		
        '		
        '                .Col = ColPOWEF		
        '                .Text = pPOWEF(mCnt)		
        '		
        '                .Col = ColPORate		
        '                .Text = pPORate(mCnt)  ''Format(pPORate, "0.000")		
        '                mMaxPORate = pPORate(mCnt)		
        '		
        '                .Col = ColSOB		
        '                .Text = pSOB(mCnt)  ''Format(pPORate, "0.000")		
        '		
        '                .Col = ColRateDiff		
        '                .Text = Format(mCostingRate - mMaxPORate, "0.000")		
        '		
        '                If mCnt < mMaxSupp Then		
        '                    I = I + 1		
        '                    .Row = I		
        '                    .Action = SS_ACTION_INSERT_ROW		
        '                End If		
        '		
        '            Next		
        '        Next		
        '    End With		


        '''********************************		
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        'Resume		
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetPODetail(ByRef pItemCode As String, ByRef pUOM As String, ByRef pSuppCode() As String, ByRef pPONO() As String, ByRef pPOWEF() As String, ByRef pPORate() As Double, ByRef pSOB() As Double, ByRef mMaxSupp As Integer) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        Dim RsTempSOB As ADODB.Recordset

        Dim mItemIssueUOM As String
        Dim mPurchaseUOM As String
        Dim mPOUOM As String
        Dim mItemFactor As Double
        Dim mSuppCode As String
        Dim mSOB As Double
        Dim mMaxArray As Integer
        Dim mCount As Integer
        Dim xPORate As Double
        Dim mFromDate As String
        Dim mToDate As String
        Dim xJWRate As Double

        mMaxArray = 2
        ReDim pSuppCode(mMaxArray)
        ReDim pPONO(mMaxArray)
        ReDim pPOWEF(mMaxArray)
        ReDim pPORate(mMaxArray)
        ReDim pSOB(mMaxArray)

        mToDate = VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")
        mFromDate = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -90, CDate(mToDate)), "DD/MM/YYYY")

        SqlStr = " SELECT IH.REF_TYPE, IH.SUPP_CUST_CODE, OP_QTY, SUM(RECEIVED_QTY) As RECEIVED_QTY " & vbCrLf _
        & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, FIN_SUPP_CUST_DET TRN " & vbCrLf _
        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _
        & " AND IH.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf _
        & " AND IH.SUPP_CUST_CODE=TRN.SUPP_CUST_CODE" & vbCrLf _
        & " AND ID.ITEM_CODE=TRN.ITEM_CODE" & vbCrLf _
        & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
        & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.REF_TYPE IN ('P') " & vbCrLf _
        & " GROUP BY IH.REF_TYPE, IH.SUPP_CUST_CODE, OP_QTY" & vbCrLf _
        & " ORDER BY 4 DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempSOB, ADODB.LockTypeEnum.adLockReadOnly)

        mCount = 0
        mMaxSupp = 0
        If RsTempSOB.EOF = False Then
            Do While RsTempSOB.EOF = False
                mSuppCode = IIf(IsDBNull(RsTempSOB.Fields("SUPP_CUST_CODE").Value), "", RsTempSOB.Fields("SUPP_CUST_CODE").Value)
                mSOB = IIf(IsDBNull(RsTempSOB.Fields("OP_QTY").Value), 0, RsTempSOB.Fields("OP_QTY").Value)
                pSOB(mCount) = mSOB

                SqlStr = " SELECT (NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2)) PURCHASE_COST, EXCHANGERATE," & vbCrLf & " ITEM_UOM,IH.PUR_TYPE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_PO, ID.PO_WEF_DATE, ID.ITEM_UOM  " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.PO_STATUS='Y' AND IH.PUR_TYPE IN ('P')"

                SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.MKEY = ( " & vbCrLf & " SELECT MAX(SIH.MKEY) " & vbCrLf & " FROM PUR_PURCHASE_HDR SIH, PUR_PURCHASE_DET SID" & vbCrLf & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SIH.MKEY=SID.MKEY AND SIH.PO_STATUS='Y' AND SIH.PUR_TYPE IN ('P')" & vbCrLf & " AND SUBSTR(SIH.AUTO_KEY_PO,LENGTH(SIH.AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & "" '','J'		

                SqlStr = SqlStr & vbCrLf & " AND SIH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'" & vbCrLf & " AND SID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND SID.PO_WEF_DATE <= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PO_CLOSED, ID.PO_WEF_DATE DESC"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    pPONO(mCount) = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_PO").Value), "", RsTemp.Fields("AUTO_KEY_PO").Value)
                    xPORate = IIf(IsDBNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value) * IIf(IsDBNull(RsTemp.Fields("EXCHANGERATE").Value), 1, RsTemp.Fields("EXCHANGERATE").Value)
                    pPOWEF(mCount) = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PO_WEF_DATE").Value), "", RsTemp.Fields("PO_WEF_DATE").Value), "DD/MM/YYYY")
                    pSuppCode(mCount) = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                    mPOUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)

                    If mPOUOM <> pUOM Then
                        SqlStr = "SELECT PURCHASE_COST, PURCHASE_UOM, ISSUE_UOM,UOM_FACTOR " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & Trim(pItemCode) & "'"

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            mItemIssueUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), 0, RsTemp.Fields("ISSUE_UOM").Value)
                            mPurchaseUOM = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                            mItemFactor = IIf(IsDBNull(RsTemp.Fields("UOM_FACTOR").Value), 0, RsTemp.Fields("UOM_FACTOR").Value)

                            If pUOM = mItemIssueUOM Then
                                xPORate = xPORate / mItemFactor
                            End If
                        End If
                    End If
                End If
                pPORate(mCount) = xPORate
                If mCount = 2 Then Exit Do
                RsTempSOB.MoveNext()
                If RsTempSOB.EOF = False Then
                    mCount = mCount + 1
                    mMaxSupp = mMaxSupp + 1
                End If
            Loop
        Else
            SqlStr = " SELECT IH.REF_TYPE, IH.SUPP_CUST_CODE, SUM(RECEIVED_QTY) As RECEIVED_QTY " & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.REF_TYPE IN ('R') " & vbCrLf & " GROUP BY IH.REF_TYPE, IH.SUPP_CUST_CODE" & vbCrLf & " ORDER BY 3 DESC"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempSOB, ADODB.LockTypeEnum.adLockReadOnly)

            mCount = 0
            mMaxSupp = 0
            If RsTempSOB.EOF = False Then
                Do While RsTempSOB.EOF = False
                    mSuppCode = IIf(IsDBNull(RsTempSOB.Fields("SUPP_CUST_CODE").Value), "", RsTempSOB.Fields("SUPP_CUST_CODE").Value)
                    mSOB = 0 ''IIf(IsNull(RsTempSOB!OP_QTY), 0, RsTempSOB!OP_QTY)		
                    pSOB(mCount) = mSOB

                    SqlStr = " SELECT IH.MKEY, (NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2)) PURCHASE_COST, EXCHANGERATE," & vbCrLf & " ITEM_UOM,IH.PUR_TYPE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_PO, ID.PO_WEF_DATE, ID.ITEM_UOM  " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.PO_STATUS='Y' AND IH.PUR_TYPE IN ('J')"


                    SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND PO_WEF_DATE = ( " & vbCrLf & " SELECT MAX(PO_WEF_DATE) " & vbCrLf & " FROM PUR_PURCHASE_HDR SIH, PUR_PURCHASE_DET SID" & vbCrLf & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SIH.MKEY=SID.MKEY AND SIH.PO_STATUS='Y' AND SIH.PUR_TYPE IN ('J')" & vbCrLf & " AND SUBSTR(SIH.AUTO_KEY_PO,LENGTH(SIH.AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & "" '','J'		

                    SqlStr = SqlStr & vbCrLf & " AND SID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND SID.PO_WEF_DATE <= '" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')"

                    SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PO_CLOSED, ID.PO_WEF_DATE DESC"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        pPONO(0) = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_PO").Value), "", RsTemp.Fields("AUTO_KEY_PO").Value)
                        xPORate = GetMaterialCostFromBOM(pItemCode, "ST", "", (txtDateFrom.Text), "L")

                        xJWRate = IIf(IsDBNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value) * IIf(IsDBNull(RsTemp.Fields("EXCHANGERATE").Value), 1, RsTemp.Fields("EXCHANGERATE").Value)
                        xPORate = xPORate + xJWRate
                        pPOWEF(0) = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PO_WEF_DATE").Value), "", RsTemp.Fields("PO_WEF_DATE").Value), "DD/MM/YYYY")
                        pSuppCode(0) = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                        mPOUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)

                        If mPOUOM <> pUOM Then
                            SqlStr = "SELECT PURCHASE_COST, PURCHASE_UOM, ISSUE_UOM,UOM_FACTOR " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & Trim(pItemCode) & "'"

                            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                            If RsTemp.EOF = False Then
                                mItemIssueUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), 0, RsTemp.Fields("ISSUE_UOM").Value)
                                mPurchaseUOM = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                                mItemFactor = IIf(IsDBNull(RsTemp.Fields("UOM_FACTOR").Value), 0, RsTemp.Fields("UOM_FACTOR").Value)

                                If pUOM = mItemIssueUOM Then
                                    xPORate = xPORate / mItemFactor
                                End If
                            End If
                        End If
                        pPORate(0) = xPORate
                    End If
                    RsTempSOB.MoveNext()
                Loop
            End If
        End If




        GetPODetail = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetPODetail = False
    End Function
    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim RMSQL As String
        Dim BOPSQL As String
        Dim SqlCond As String
        Dim PNTSQL As String

        ''''SELECT CLAUSE...		

        RMSQL = " SELECT  ID.SUBROWNO," & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " IH.PRODUCT_CODE, IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO, " & vbCrLf & " IH.WEF, ID.ITEM_CODE, IMST2.ITEM_SHORT_DESC AS RM_SHORT_DESC, IMST2.ISSUE_UOM, " & vbCrLf & " DECODE(IMST2.ISSUE_UOM,'TON',ID.GROSS_WT/1000000,DECODE(IMST2.ISSUE_UOM,'KGS',ID.GROSS_WT/1000,ID.GROSS_WT)) As GROSS_WT, " & vbCrLf & " DECODE(IMST2.ISSUE_UOM,'TON',ID.SCRAP_WT/1000000,DECODE(IMST2.ISSUE_UOM,'KGS',ID.SCRAP_WT/1000,ID.SCRAP_WT)) AS SCRAP_WT, " & vbCrLf & " CASE WHEN IMST2.ISSUE_UOM='TON' OR IMST2.ISSUE_UOM='KGS' OR IMST2.ISSUE_UOM='LTR' THEN DECODE(IMST2.ISSUE_UOM,'TON',ID.ITEM_RATE*1000,ID.ITEM_RATE) ELSE ITEM_AMOUNT END As ITEM_RATE, " & vbCrLf & " '', '', '', '', 0, 0, 0, 0 " & vbCrLf & " FROM PRD_CUST_FG_COST_HDR IH, PRD_CUST_FG_COST_RM_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST, INV_ITEM_MST IMST2"

        BOPSQL = " SELECT  ID.SUBROWNO+100 AS SUBROWNO," & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " IH.PRODUCT_CODE, IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO, " & vbCrLf & " IH.WEF, ID.ITEM_CODE, IMST2.ITEM_SHORT_DESC AS RM_SHORT_DESC, IMST2.ISSUE_UOM, ID.ITEM_QTY AS GROSS_WT, 0 AS SCRAP_WT, ID.ITEM_RATE, " & vbCrLf & " '', '', '', '', 0, 0, 0, 0 " & vbCrLf & " FROM PRD_CUST_FG_COST_HDR IH, PRD_CUST_FG_COST_BOP_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST, INV_ITEM_MST IMST2"

        '    PNTSQL = " SELECT  ID.SUBROWNO+200 AS SUBROWNO," & vbCrLf _		
        ''            & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf _		
        ''            & " IH.PRODUCT_CODE, IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO, " & vbCrLf _		
        ''            & " IH.WEF, ID.ITEM_CODE, IMST2.ITEM_SHORT_DESC AS RM_SHORT_DESC, IMST2.ISSUE_UOM, ID.ITEM_QTY AS GROSS_WT, 0 AS SCRAP_WT, ID.ITEM_RATE, " & vbCrLf _		
        ''            & " '', '', '', '', 0, 0, 0, 0 " & vbCrLf _		
        ''            & " FROM PRD_CUST_FG_COST_HDR IH, PRD_CUST_FG_COST_PNT_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST, INV_ITEM_MST IMST2"		

        ''''WHERE CLAUSE...		
        SqlCond = " WHERE IH.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf & " AND IH.PRODUCT_CODE=IMST.ITEM_CODE" & vbCrLf & " AND ID.COMPANY_CODE=IMST2.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=IMST2.ITEM_CODE"

        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlCond = SqlCond & vbCrLf & " AND CMST.SUPP_CUST_NAME ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' "
        End If

        If chkAllProduct.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlCond = SqlCond & vbCrLf & " AND IMST.ITEM_SHORT_DESC ='" & MainClass.AllowSingleQuote(txtProduct.Text) & "' "
        End If

        If chkAllRM.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlCond = SqlCond & vbCrLf & " AND IMST2.ITEM_SHORT_DESC ='" & MainClass.AllowSingleQuote(txtRM.Text) & "' "
        End If

        SqlCond = SqlCond & vbCrLf & " AND IH.MKEY = (SELECT MAX(MKEY) FROM PRD_CUST_FG_COST_HDR A" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND A.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND A.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf & " AND A.PRODUCT_CODE=IMST.ITEM_CODE " & vbCrLf & " AND A.WEF <= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')) "

        MakeSQL = RMSQL & vbCrLf & SqlCond & vbCrLf & " UNION " & BOPSQL & vbCrLf & SqlCond ''& vbCrLf |            & " UNION " & PNTSQL & vbCrLf & SqlCond		

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY 3, 4, 1 "
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1


        If Trim(txtDateFrom.Text) = "" Then
            MsgInformation("Date is blank.")
            txtDateFrom.Focus()
            FieldsVerification = False
            Exit Function
        End If

        '    If Trim(txtDateTo.Text) = "" Then		
        '        MsgInformation "Date is blank."		
        '        txtDateTo.focus		
        '        FieldsVerification = False		
        '        Exit Function		
        '    End If		

        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtCustomer.Text) = "" Then
                MsgInformation("Department is blank.")
                txtCustomer.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        If chkAllProduct.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtProduct.Text) = "" Then
                MsgInformation("Product is blank.")
                txtProduct.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkAllRM.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtRM.Text) = "" Then
                MsgInformation("Raw Material is blank.")
                txtRM.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub txtProduct_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProduct.DoubleClick
        SearchItem()
    End Sub

    Private Sub txtProduct_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProduct.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProduct.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SearchDept()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtCustomer.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            txtCustomer.Text = AcName
            txtCustomer.Focus()
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtProduct_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProduct.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub

    Private Sub txtProduct_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProduct.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If txtProduct.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "


        If MainClass.ValidateWithMasterTable(txtProduct.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtProduct.Text = UCase(Trim(txtProduct.Text))
        Else
            MsgInformation("No Such Product in Product Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRM_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRM.DoubleClick
        SearchRM()
    End Sub

    Private Sub txtRM_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRM.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRM.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRM_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRM.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchRM()
    End Sub

    Private Sub txtRM_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRM.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If txtRM.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable(txtRM.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtRM.Text = UCase(Trim(txtRM.Text))
        Else
            MsgInformation("No Such Product in Product Master")
            Cancel = True

        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub chkAllRM_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllRM.CheckStateChanged
        Call PrintStatus(False)
        If chkAllRM.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtRM.Enabled = False
            cmdsearchRM.Enabled = False
        Else
            txtRM.Enabled = True
            cmdsearchRM.Enabled = True
        End If
    End Sub

    Private Sub SearchRM()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        MainClass.SearchGridMaster(txtRM.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            txtRM.Text = AcName
            txtRM.Focus()
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class
