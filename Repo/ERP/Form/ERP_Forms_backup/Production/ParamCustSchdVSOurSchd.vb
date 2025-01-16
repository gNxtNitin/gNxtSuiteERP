Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility

Friend Class frmParamCustSchdVsOurSchd
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Dim PvtDBCn As ADODB.Connection					


    Private Const ColLocked As Short = 1
    Private Const ColCust_Code As Short = 2
    Private Const ColCustomer As Short = 3
    Private Const ColItemModel As Short = 4
    Private Const ColItemModelDesc As Short = 5
    Private Const ColItemCode As Short = 6
    Private Const ColItemName As Short = 7
    Private Const ColItemPartNo As Short = 8
    Private Const ColSOB As Short = 9
    Private Const ColStoreLoc As Short = 10
    Private Const ColModelSchdQty As Short = 11
    Private Const ColStdQty As Short = 12
    Private Const ColProdSchdQty As Short = 13

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim pMenu As String
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
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnDDR(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnDDR(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnDDR(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String



        SqlStr = ""

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr


        '''''Select Record for print...					

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        Report1.Reset()
        mTitle = "Customer Wise Product Schedule Qty"
        mSubTitle = "From : " & VB6.Format(lblYear.Text, "MMMM-YYYY") ''& " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\CustDailyShort.RPT"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, pMenu)

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
        Dim SqlStr As String
        Dim RsShow As ADODB.Recordset

        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        'If Show1(RsShow) = False Then GoTo ErrPart
        Call PrintStatus(True)
        cmdGenerateSchedule.Enabled = IIf(optShow(0).Checked = True, False, True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4					
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmParamCustSchdVsOurSchd_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Product Wise Schedule Report"
        cmdGenerateSchedule.Enabled = False  '' IIf(optShow(0).Checked = True, False, True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamCustSchdVsOurSchd_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        pMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        ''Set PvtDBCn = New ADODB.Connection					
        ''PvtDBCn.Open StrConn					

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False

        txtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False


        Call PrintStatus(True)
        'Call FillPOCombo		
        lblYear.Text = VB6.Format(System.DateTime.FromOADate(RunDate.ToOADate - 1), "MMMM ,YYYY")
        'txtDateFrom.Text = VB6.Format(System.DateTime.FromOADate(RunDate.ToOADate - 1), "DD/MM/YYYY")
        'txtDateTo.Text = VB6.Format(System.DateTime.FromOADate(RunDate.ToOADate - 1), "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamCustSchdVsOurSchd_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamCustSchdVsOurSchd_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        Dim xVDate As String
        Dim xMkey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String


        '    SprdMain.Row = SprdMain.ActiveRow					
        '					
        '    SprdMain.Col = ColMkey					
        '    xMkey = Me.SprdMain.Text					
        '    sqlstr = "SELECT * from FIN_INVOICE_HDR WHERE MKEY='" & xMkey & "'"					
        '    MainClass.UOpenRecordSet sqlstr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly					
        '					
        '    If RsTemp.EOF = False Then					
        '        xVDate = RsTemp!INVOICE_DATE					
        '        xVNo = RsTemp!BILLNO					
        '					
        '    Call ShowTrn(xMkey, xVDate, "", xVNo, "S", "")					
        '    End If					
    End Sub


    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub

    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchSupplier()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , SqlStr)
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
    Private Sub txtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

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
            .MaxCols = ColProdSchdQty
            .set_RowHeight(0, RowHeight * 1.2)
            .set_ColWidth(0, 1)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColCust_Code
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCust_Code, 15)
            .ColHidden = True

            .Col = ColCustomer
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomer, 25)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 25)

            .Col = ColItemPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemPartNo, 10)

            .Col = ColItemModel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemModel, 8)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)


            .Col = ColItemModelDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemModelDesc, 8)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)


            .ColsFrozen = ColItemName

            .Col = ColSOB
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(cntCol, 7)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColStoreLoc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColStoreLoc, 7)
            .ColHidden = False ''IIf(optShow(0).Checked = True, False, True)


            For cntCol = ColModelSchdQty To ColProdSchdQty
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 10)
            Next

            .Col = ColStdQty
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1(ByRef mRs As ADODB.Recordset) As Boolean
        On Error GoTo LedgError
        Dim cntRow As Integer
        Dim pCustSchd As Double
        Dim mCustCode As String
        Dim mItemCode As String
        Dim mStoreLoc As String
        Dim mProdSchdQty As Double
        Dim pModelSchdQty As Double
        Dim mModelName As String
        Dim pStdQty As Double

        Show1 = False
        cntRow = 1
        With SprdMain
            If mRs.EOF = False Then

                System.Windows.Forms.Application.DoEvents()

                Do While Not mRs.EOF


                    .MaxRows = cntRow
                    .Row = cntRow

                    .Col = ColLocked
                    .Text = ""

                    .Col = ColCust_Code
                    .Text = Trim(IIf(IsDBNull(mRs.Fields("SUPP_CUST_CODE").Value), "", mRs.Fields("SUPP_CUST_CODE").Value))
                    mCustCode = Trim(IIf(IsDBNull(mRs.Fields("SUPP_CUST_CODE").Value), "", mRs.Fields("SUPP_CUST_CODE").Value))

                    .Col = ColCustomer
                    .Text = IIf(IsDBNull(mRs.Fields("SUPP_CUST_NAME").Value), "", mRs.Fields("SUPP_CUST_NAME").Value)

                    .Col = ColItemCode
                    .Text = Trim(IIf(IsDBNull(mRs.Fields("ITEM_CODE").Value), "", mRs.Fields("ITEM_CODE").Value))
                    mItemCode = Trim(IIf(IsDBNull(mRs.Fields("ITEM_CODE").Value), "", mRs.Fields("ITEM_CODE").Value))

                    .Col = ColItemName
                    .Text = Trim(IIf(IsDBNull(mRs.Fields("ITEM_SHORT_DESC").Value), "", mRs.Fields("ITEM_SHORT_DESC").Value))

                    .Col = ColItemPartNo
                    .Text = Trim(IIf(IsDBNull(mRs.Fields("CUSTOMER_PART_NO").Value), "", mRs.Fields("CUSTOMER_PART_NO").Value))

                    .Col = ColItemModel
                    .Text = Trim(IIf(IsDBNull(mRs.Fields("MODEL_CODE").Value), "", mRs.Fields("MODEL_CODE").Value))

                    'mModelName = GetModel(mItemCode)
                    'mSharingPer = GetCustomerSharingPer(mCustCode, mItemCode)
                    'pCustSchd = GetCustData(mCustCode, mItemCode)
                    .Col = ColSOB
                    .Text = Trim(IIf(IsDBNull(mRs.Fields("PROD_SOB").Value), "", mRs.Fields("PROD_SOB").Value))

                    .Col = ColStoreLoc
                    .Text = Trim(IIf(IsDBNull(mRs.Fields("LOC_CODE").Value), "", mRs.Fields("LOC_CODE").Value))

                    '.Col = ColModelSchdQty
                    '.Text = VB6.Format(IIf(IsDBNull(mRs.Fields("TOTAL_QTY").Value), 0, mRs.Fields("TOTAL_QTY").Value), "0.00")

                    '.Col = ColStdQty
                    '.Text = VB6.Format(IIf(IsDBNull(mRs.Fields("PROD_STD_QTY").Value), 0, mRs.Fields("PROD_STD_QTY").Value), "0.00")

                    '.Col = ColProdSchdQty
                    'mProdSchdQty = VB6.Format(IIf(IsDBNull(mRs.Fields("TOTAL_QTY").Value), 0, mRs.Fields("TOTAL_QTY").Value), "0.00") * VB6.Format(IIf(IsDBNull(mRs.Fields("PROD_STD_QTY").Value), 0, mRs.Fields("PROD_STD_QTY").Value), "0.00")
                    '.Text = VB6.Format(IIf(IsDBNull(mRs.Fields("ITEM_SCHD").Value), 0, mRs.Fields("ITEM_SCHD").Value), "0.00") ''VB6.Format(mProdSchdQty, "0.00")  ''


                    cntRow = cntRow + 1
NextRow:
                    mRs.MoveNext()
                Loop
            End If
        End With
        ''********************************					
        Show1 = True
        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mSupplier As String
        Dim mStartDate As String
        Dim mEndDate As String

        If optShow(0).Checked = True Then
            MakeSQL = " SELECT '', SOB.SUPP_CUST_CODE , CMST.SUPP_CUST_NAME, " & vbCrLf _
                    & " SOB.MODEL_CODE, MODEL.MODEL_DESC, SOB.ITEM_CODE, INVMST.ITEM_SHORT_DESC,  " & vbCrLf _
                    & " INVMST.CUSTOMER_PART_NO, SOB.PROD_SOB, MODEL.LOC_CODE, " & vbCrLf _
                    & " SUM(TOTAL_QTY) AS TOTAL_QTY, PROD_STD_QTY, SUM(TOTAL_QTY * PROD_STD_QTY * SOB.PROD_SOB * .01) AS ITEM_SCHD"

        Else
            MakeSQL = " SELECT '', SOB.SUPP_CUST_CODE , CMST.SUPP_CUST_NAME, " & vbCrLf _
                    & " '', SOB.ITEM_CODE, INVMST.ITEM_SHORT_DESC,  " & vbCrLf _
                    & " INVMST.CUSTOMER_PART_NO, '',  MODEL.LOC_CODE, " & vbCrLf _
                    & " SUM(TOTAL_QTY) AS TOTAL_QTY, '', SUM(TOTAL_QTY * PROD_STD_QTY * SOB.PROD_SOB * .01) AS ITEM_SCHD"
        End If

        ''''FROM CLAUSE...					
        MakeSQL = MakeSQL & vbCrLf & " FROM " & vbCrLf _
                    & " DSP_CUST_SOB_DET SOB, INV_ITEM_MST INVMST, FIN_SUPP_CUST_MST CMST , GEN_MODEL_MST MODEL , PPC_MODELWISE_MON_SCHD_HDR SH, PPC_MODELWISE_MON_SCHD_DET SD, INV_MODELWISE_PROD_DET MP"

        '''''WHERE CLAUSE...					
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " SOB.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And SOB.COMPANY_CODE=MODEL.COMPANY_CODE" & vbCrLf _
            & " And SOB.MODEL_CODE=MODEL.MODEL_CODE " & vbCrLf _
            & " And SOB.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " And SOB.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " And SOB.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And SOB.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " And SOB.COMPANY_CODE=SH.COMPANY_CODE" & vbCrLf _
            & " And SOB.SUPP_CUST_CODE=SH.SUPP_CUST_CODE" & vbCrLf _
            & " And SH.AUTO_KEY_REF=SD.AUTO_KEY_REF" & vbCrLf _
            & " And SOB.MODEL_CODE=SD.MODEL_CODE " & vbCrLf _
            & " And SOB.COMPANY_CODE=MP.COMPANY_CODE" & vbCrLf _
            & " And SOB.MODEL_CODE=MP.MODEL_CODE" & vbCrLf _
            & " And SOB.ITEM_CODE=MP.ITEM_CODE "    ''AND POST_FLAG='Y'


        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "And CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If chkAllModel.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtModel.Text, "MODEL_CODE", "MODEL_CODE", "GEN_MODEL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                MakeSQL = MakeSQL & vbCrLf & "AND  MODEL.MODEL_CODE='" & MainClass.AllowSingleQuote(txtModel.Text) & "'"
            End If
        End If

        If chkAllLoc.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtLoc.Text, "LOC_CODE", "LOC_CODE", "DSP_CUST_STORE_LOC_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                MakeSQL = MakeSQL & vbCrLf & "AND MODEL.LOC_CODE='" & MainClass.AllowSingleQuote(txtLoc.Text) & "'"
            End If
        End If

        mStartDate = "01/" & VB6.Format(lblYear.Text, "MM/YYYY")
        mEndDate = MainClass.LastDay(Month(lblYear.Text), Year(lblYear.Text)) & "/" & VB6.Format(lblYear.Text, "MM/YYYY")
        MakeSQL = MakeSQL & vbCrLf _
            & " AND SH.PLAN_MONTH>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SH.PLAN_MONTH<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        ''''''GROUP BY CLAUSE...					
        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                    & " GROUP BY SOB.SUPP_CUST_CODE , CMST.SUPP_CUST_NAME, MODEL.MODEL_DESC," & vbCrLf _
                    & " SOB.MODEL_CODE, SOB.ITEM_CODE, INVMST.ITEM_SHORT_DESC,  " & vbCrLf _
                    & " INVMST.CUSTOMER_PART_NO, SOB.PROD_SOB, MODEL.LOC_CODE, PROD_STD_QTY, MODEL.LOC_CODE "

        Else
            MakeSQL = MakeSQL & vbCrLf _
                    & " GROUP BY SOB.SUPP_CUST_CODE , CMST.SUPP_CUST_NAME, " & vbCrLf _
                    & " SOB.ITEM_CODE, INVMST.ITEM_SHORT_DESC,  " & vbCrLf _
                    & " INVMST.CUSTOMER_PART_NO, MODEL.LOC_CODE"
        End If

        ''''''ORDER CLAUSE...					
        MakeSQL = MakeSQL & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME, SOB.ITEM_CODE"


        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        'If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        'If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        'If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        'If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtSupplier.Text) = "" Then
                MsgInformation("Invaild Supplier Name")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
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
    '    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs)
    '        Dim Cancel As Boolean = eventArgs.Cancel
    '        If MainClass.ChkIsdateF(txtDateFrom) = False Then
    '            txtDateFrom.Focus()
    '            Cancel = True
    '            Exit Sub
    '        End If
    '        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
    '            txtDateFrom.Focus()
    '            Cancel = True
    '            GoTo EventExitSub
    '        End If
    'EventExitSub:
    '        eventArgs.Cancel = Cancel
    '    End Sub
    '    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs)
    '        Dim Cancel As Boolean = eventArgs.Cancel
    '        If MainClass.ChkIsdateF(txtDateTo) = False Then
    '            txtDateTo.Focus()
    '            Cancel = True
    '            Exit Sub
    '        End If
    '        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
    '            txtDateTo.Focus()
    '            Cancel = True
    '            GoTo EventExitSub
    '        End If

    'EventExitSub:
    '        eventArgs.Cancel = Cancel
    '    End Sub
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
    Private Function GetModel(ByRef pItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetModel = ""
        SqlStr = "SELECT DISTINCT GM.MODEL_DESC  " & vbCrLf _
            & " FROM INV_MODELWISE_PROD_DET MD, GEN_MODEL_MST GM  " & vbCrLf _
            & " WHERE MD.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND MD.COMPANY_CODE = GM.COMPANY_CODE" & vbCrLf _
            & " AND MD.MODEL_CODE = GM.MODEL_CODE" & vbCrLf & " AND MD.ITEM_CODE='" & pItemCode & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY GM.MODEL_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                If GetModel = "" Then
                    GetModel = IIf(IsDBNull(RsTemp.Fields("MODEL_DESC").Value), "", RsTemp.Fields("MODEL_DESC").Value)
                Else
                    GetModel = GetModel & ", " & IIf(IsDBNull(RsTemp.Fields("MODEL_DESC").Value), "", RsTemp.Fields("MODEL_DESC").Value)
                End If

                RsTemp.MoveNext()
            Loop
        End If

        RsTemp.Close()
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetModel = ""
        RsTemp.Close()
    End Function

    Private Function GetCustomerSharingPer(ByRef pCustCode As String, ByRef pItemCode As String) As Double
        On Error GoTo ErrPart

        GetCustomerSharingPer = 0

        If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "OP_QTY", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustCode) & "'") = True Then
            GetCustomerSharingPer = Val(MasterNo)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetCustomerSharingPer = 0
    End Function

    Private Sub cmdSearchModel_Click(sender As Object, e As EventArgs) Handles cmdSearchModel.Click
        SearchModel()
    End Sub

    Private Sub cmdSearchLoc_Click(sender As Object, e As EventArgs) Handles cmdSearchLoc.Click
        SearchLoc()
    End Sub

    Private Sub chkAllModel_CheckStateChanged(sender As Object, e As EventArgs) Handles chkAllModel.CheckStateChanged
        Call PrintStatus(False)
        If chkAllModel.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtModel.Enabled = False
            cmdSearchModel.Enabled = False
        Else
            txtModel.Enabled = True
            cmdSearchModel.Enabled = True
        End If
    End Sub
    Private Sub chkAllLoc_CheckStateChanged(sender As Object, e As EventArgs) Handles chkAllLoc.CheckStateChanged
        Call PrintStatus(False)
        If chkAllLoc.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtLoc.Enabled = False
            cmdSearchLoc.Enabled = False
        Else
            txtLoc.Enabled = True
            cmdSearchLoc.Enabled = True
        End If
    End Sub
    Private Sub txtModel_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModel.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtModel_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModel.DoubleClick
        SearchModel()
    End Sub
    Private Sub txtModel_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtModel.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtModel.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtModel_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtModel.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchModel()
    End Sub
    Private Sub txtLoc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoc.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtLoc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoc.DoubleClick
        SearchLoc()
    End Sub
    Private Sub txtLoc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLoc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLoc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtLoc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtLoc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchLoc()
    End Sub
    Private Sub SearchModel()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtModel.Text, "GEN_MODEL_MST", "MODEL_CODE", "MODEL_DESC",  ,  , SqlStr)
        If AcName <> "" Then
            txtModel.Text = AcName
        End If
        Exit Sub

ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchLoc()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtLoc.Text, "DSP_CUST_STORE_LOC_MST", "LOC_CODE", "LOC_DESCRIPTION",  ,  , SqlStr)
        If AcName <> "" Then
            txtLoc.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub optShow_CheckedChanged(sender As Object, e As EventArgs) Handles optShow.CheckedChanged

    End Sub

    Private Sub cmdGenerateSchedule_Click(sender As Object, e As EventArgs) Handles cmdGenerateSchedule.Click
        On Error GoTo ERR1
        Dim CntRow As Integer
        Dim mItemCode As String
        Dim mUnit As String
        Dim mPartyCode As String
        Dim mSchdQty As Double
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pAddMode As Boolean
        Dim pPONO As Double
        Dim pPODate As String
        Dim pAmendNo As Integer
        Dim pAmendDate As String
        Dim pWEFDate As String
        Dim RsTempPO As ADODB.Recordset
        Dim mDSPost As String
        Dim pDSNo As Double

        Dim pDSdate As String
        Dim pDSAmendNo As Integer
        Dim pDSAmendDate As String
        Dim mSchdStatus As String
        Dim pScheduleDate As String
        'Dim mPackingStd As Double
        Dim mActualSchdQty As Double
        Dim mTillDatePurQty As Double
        Dim mStoreLoc As String

        Dim mCustomerPONo As String
        Dim mCustomerPODate As String

        Dim mString As String
        Dim mCustomer As String

        If optShow(1).Checked = False Then
            MsgInformation("Please Select Summary for Process Schedule.")
            Exit Sub
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("Please Select All Product for Process Schedule.")
            Exit Sub
        End If

        If chkAllModel.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("Please Select All Model for Process Schedule.")
            Exit Sub
        End If

        If chkAllLoc.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("Please Select All Location for Process Schedule.")
            Exit Sub
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        pScheduleDate = "01/" & VB6.Format(lblYear.Text, "MM/YYYY")
        mString = " Sales Order Not Found of Following Part No : "
        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                mCustomer = ""

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mUnit = Trim(MasterNo)
                End If

                .Col = ColCust_Code
                mPartyCode = Trim(.Text)

                '.Col = ColCustomer
                'mCustomer = "Customer Name : " & Trim(.Text)

                .Col = ColItemName
                mCustomer = mCustomer & " Item Name : " & Trim(.Text)

                .Col = ColItemPartNo
                mCustomer = mCustomer & " Part No : " & Trim(.Text)

                .Col = ColProdSchdQty
                mSchdQty = Val(.Text)
                mCustomer = mCustomer & " Schedule Qty : " & Trim(.Text)

                .Col = ColStoreLoc
                mStoreLoc = Trim(.Text)
                mCustomer = mCustomer & " Location : " & Trim(.Text)

                If mItemCode <> "" Then
                    'mTillDatePurQty = GetTotalDespatchQty(mItemCode, mUnit, mPartyCode, pScheduleDate)
                    '                mExtraApprovalQty = GetExtraApprovalQty(pItemCode, mItemUOM, pPartyCode, pSchdDate)						
                End If

                '						
                '						
                If mTillDatePurQty > mSchdQty Then
                    mSchdQty = mTillDatePurQty
                End If

                If mItemCode <> "" And mPartyCode <> "" And (mSchdQty - mActualSchdQty) <> 0 Then ''mSchdQty > 0 And						

                    SqlStr = " SELECT AUTO_KEY_SO, SO_DATE, AMEND_NO, AMEND_DATE, " & vbCrLf _
                        & " CUST_PO_NO, CUST_PO_DATE, AMEND_WEF_FROM" & vbCrLf _
                        & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND IH.MKEY=ID.MKEY" & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'" & vbCrLf _
                        & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                        & " AND ID.CUST_STORE_LOC='" & MainClass.AllowSingleQuote(mStoreLoc) & "'" & vbCrLf _
                        & " And ORDER_TYPE='O'" & vbCrLf _
                        & " AND IH.SO_APPROVED='Y'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPO, ADODB.LockTypeEnum.adLockReadOnly)

                    mDSPost = "N"
                    pDSNo = 0
                    pDSdate = ""
                    pDSAmendNo = 0
                    pDSAmendDate = ""
                    mSchdStatus = "N"
                    mCustomerPONo = ""
                    mCustomerPODate = ""


                    If RsTempPO.EOF = False Then
                        pPONO = IIf(IsDBNull(RsTempPO.Fields("AUTO_KEY_SO").Value), -1, RsTempPO.Fields("AUTO_KEY_SO").Value)
                        pPODate = VB6.Format(IIf(IsDBNull(RsTempPO.Fields("SO_DATE").Value), "", RsTempPO.Fields("SO_DATE").Value), "DD/MM/YYYY")
                        pAmendNo = IIf(IsDBNull(RsTempPO.Fields("AMEND_NO").Value), -1, RsTempPO.Fields("AMEND_NO").Value)
                        pAmendDate = VB6.Format(IIf(IsDBNull(RsTempPO.Fields("AMEND_DATE").Value), "", RsTempPO.Fields("AMEND_DATE").Value), "DD/MM/YYYY")
                        pWEFDate = VB6.Format(IIf(IsDBNull(RsTempPO.Fields("AMEND_WEF_FROM").Value), "", RsTempPO.Fields("AMEND_WEF_FROM").Value), "DD/MM/YYYY")
                        mCustomerPONo = IIf(IsDBNull(RsTempPO.Fields("CUST_PO_NO").Value), -1, RsTempPO.Fields("CUST_PO_NO").Value)
                        mCustomerPODate = VB6.Format(IIf(IsDBNull(RsTempPO.Fields("CUST_PO_DATE").Value), "", RsTempPO.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                        SqlStr = " SELECT AUTO_KEY_DELV,DELV_SCHLD_DATE,DELV_AMEND_NO, " & vbCrLf _
                            & " DELV_AMEND_DATE,SCHLD_STATUS FROM DSP_DELV_SCHLD_HDR" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'" & vbCrLf _
                            & " AND TO_CHAR(SCHLD_DATE,'YYYYMM')='" & VB6.Format(pScheduleDate, "YYYYMM") & "'" & vbCrLf _
                            & " AND AUTO_KEY_SO=" & Val(CStr(pPONO)) & ""

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = True Then
                            pAddMode = True
                            pDSNo = AutoGenPONoSeq()
                            pDSdate = CStr(PubCurrDate)
                            mDSPost = "N"
                            mSchdStatus = "O"
                            pDSAmendNo = 0
                            pDSAmendDate = CStr(PubCurrDate)
                        Else
                            pAddMode = False
                            pDSNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_DELV").Value), -1, RsTemp.Fields("AUTO_KEY_DELV").Value)
                            pDSdate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DELV_SCHLD_DATE").Value), "", RsTemp.Fields("DELV_SCHLD_DATE").Value), "DD/MM/YYYY")
                            mDSPost = "Y"
                            mSchdStatus = "O"
                            If mDSPost = "Y" Then
                                pDSAmendNo = IIf(IsDBNull(RsTemp.Fields("DELV_AMEND_NO").Value), 0, RsTemp.Fields("DELV_AMEND_NO").Value) + 1
                                pDSAmendDate = CStr(PubCurrDate)
                            Else
                                pDSAmendNo = IIf(IsDBNull(RsTemp.Fields("DELV_AMEND_NO").Value), 0, RsTemp.Fields("DELV_AMEND_NO").Value)
                                pDSAmendDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DELV_AMEND_DATE").Value), "", RsTemp.Fields("DELV_AMEND_DATE").Value), "DD/MM/YYYY")
                            End If
                        End If
                        '                Else						
                        If UpdateDS(pAddMode, pDSNo, pDSdate, pDSAmendNo, pDSAmendDate, pPONO, mPartyCode, pScheduleDate, mDSPost, mSchdStatus, pPODate, pAmendNo, pAmendDate, pWEFDate, mItemCode, mUnit, mSchdQty, mCustomerPONo, mCustomerPODate, mStoreLoc) = False Then GoTo ERR1
                    Else
                        mString = mString & vbCrLf _
                                & mCustomer
                    End If
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        MsgInformation(mString)
        cmdGenerateSchedule.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Function AutoGenPONoSeq() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        mAutoGen = 1

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_DELV)  " & vbCrLf _
            & " FROM DSP_DELV_SCHLD_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_DELV,LENGTH(AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With

        AutoGenPONoSeq = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function
    Private Function UpdateDS(ByRef pAddMode As Boolean, ByRef pDSNo As Double, ByRef pDSdate As String, ByRef pDSAmendNo As Integer, ByRef pDSAmendDate As String,
                              ByRef pPONO As Double, ByRef mPartyCode As String, ByRef pSchdDate As String, ByRef mDSPost As String, ByRef mSchdStatus As String,
                              ByRef pPODate As String, ByRef pAmendNo As Integer, ByRef pAmendDate As String, ByRef pWEFDate As String, ByRef pItemCode As String,
                              ByRef pUnit As String, ByRef pDSQty As Double, ByRef mCustomerPONo As String, ByRef mCustomerPODate As String, ByRef mStoreLoc As String) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart

        Dim SqlStr As String

        If pAddMode = True Then

            SqlStr = " INSERT INTO DSP_DELV_SCHLD_HDR ( " & vbCrLf _
                & " COMPANY_CODE , AUTO_KEY_DELV," & vbCrLf _
                & " DELV_SCHLD_DATE ,  CUST_DELV_NO," & vbCrLf _
                & " CUST_DELV_DATE , AUTO_KEY_SO," & vbCrLf _
                & " SO_DATE , CUST_SO_NO," & vbCrLf _
                & " CUST_SO_DATE , SO_AMEND_NO," & vbCrLf _
                & " AMEND_DATE , AMEND_WEF_DATE," & vbCrLf _
                & " SUPP_CUST_CODE , SCHLD_DATE," & vbCrLf _
                & " DELV_AMEND_NO , DELV_AMEND_DATE, " & vbCrLf _
                & " SCHLD_STATUS , REMARKS, IS_MAIL, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE, APPROVAL_BH, APPROVAL_PH) "


            SqlStr = SqlStr & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & " , " & pDSNo & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(pDSdate, "DD-MMM-YYYY") & "','DD-MON-YYYY') ," & vbCrLf _
                & " '0'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(pDSdate, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & Val(pPONO) & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(pPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY') , '" & MainClass.AllowSingleQuote(mCustomerPONo) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(mCustomerPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & Val(pAmendNo) & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(pAmendDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') , TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mPartyCode) & "' , TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " " & Val(pDSAmendNo) & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(pDSAmendDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & mSchdStatus & "' , '', 'N', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','Y','Y')"

            PubDBCn.Execute(SqlStr)
        ElseIf mDSPost = "N" Then
            SqlStr = " UPDATE DSP_DELV_SCHLD_HDR SET " & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND AUTO_KEY_DELV =" & Val(CStr(pDSNo)) & ""

            PubDBCn.Execute(SqlStr)
        ElseIf mDSPost = "Y" Then

            SqlStr = " UPDATE DSP_DELV_SCHLD_HDR SET " & vbCrLf _
                & " AUTO_KEY_DELV=" & pDSNo & "," & vbCrLf _
                & " DELV_SCHLD_DATE=TO_DATE('" & VB6.Format(pDSdate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " CUST_DELV_NO='0'," & vbCrLf _
                & " CUST_DELV_DATE=TO_DATE('" & VB6.Format(pDSdate, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf _
                & " DELV_AMEND_NO=" & Val(pDSAmendNo) & ", " & vbCrLf _
                & " DELV_AMEND_DATE=TO_DATE('" & VB6.Format(pDSAmendDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " AUTO_KEY_SO=" & Val(pPONO) & "," & vbCrLf _
                & " SO_DATE=TO_DATE('" & VB6.Format(pPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf _
                & " CUST_SO_NO='" & MainClass.AllowSingleQuote((mCustomerPONo)) & "'," & vbCrLf _
                & " CUST_SO_DATE=TO_DATE('" & VB6.Format(mCustomerPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf _
                & " SO_AMEND_NO=" & Val(pAmendNo) & "," & vbCrLf _
                & " AMEND_DATE=TO_DATE('" & VB6.Format(pAmendDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf _
                & " AMEND_WEF_DATE=TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((mPartyCode)) & "' , " & vbCrLf _
                & " SCHLD_DATE=TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " SCHLD_STATUS='" & mSchdStatus & "' , " & vbCrLf _
                & " REMARKS='', IS_MAIL='N', " & vbCrLf _
                & " APPROVAL_BH='Y', APPROVAL_PH='Y'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND AUTO_KEY_DELV =" & Val(CStr(pDSNo)) & ""

            PubDBCn.Execute(SqlStr)
        End If

        If UpdateDetail1(pDSNo, pItemCode, pUnit, pDSQty, mPartyCode, pSchdDate, pDSAmendNo, mDSPost, mStoreLoc) = False Then GoTo ErrPart


        UpdateDS = True
        Exit Function
ErrPart:
        UpdateDS = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Function
    Private Function UpdateDetail1(ByRef pDSNo As Double, ByRef pItemCode As String, ByRef mItemUOM As String, ByRef pDSQty As Double,
                                   ByRef pPartyCode As String, ByRef pSchdDate As String, ByRef pDSAmendNo As Integer, ByRef mDSPost As String, ByRef mStoreLoc As String) As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mWeek1Qty As Double
        Dim mWeek2Qty As Double
        Dim mWeek3Qty As Double
        Dim mWeek4Qty As Double
        Dim mWeek5Qty As Double


        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mDay As Integer
        Dim mDate As String
        Dim mLastDay As Integer
        Dim mWorkingDays As Double
        Dim mDailyPlanQty As Double
        Dim mDailySchdQty As Double
        Dim mBalQty As Double
        Dim RsTempUOM As ADODB.Recordset
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mExtraApprovalQty As Double
        Dim mTillDatePurQty As Double

        SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR,PURCHASE_COST FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempUOM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempUOM.EOF = False Then
            mPurchaseUOM = IIf(IsDBNull(RsTempUOM.Fields("PURCHASE_UOM").Value), "", RsTempUOM.Fields("PURCHASE_UOM").Value)
            mFactor = IIf(IsDBNull(RsTempUOM.Fields("UOM_FACTOR").Value) Or RsTempUOM.Fields("UOM_FACTOR").Value = 0, 1, RsTempUOM.Fields("UOM_FACTOR").Value)
        End If


        pDSQty = System.Math.Round(pDSQty / mFactor, 0)

        mLastDay = MainClass.LastDay(Month(CDate(pSchdDate)), Year(CDate(pSchdDate)))

        I = 1
        SqlStr = "SELECT SERIAL_NO FROM DSP_DELV_SCHLD_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_DELV=" & Val(CStr(pDSNo)) & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            I = IIf(IsDBNull(RsTemp.Fields("SERIAL_NO").Value), 1, RsTemp.Fields("SERIAL_NO").Value)
        Else
            SqlStr = "SELECT MAX(SERIAL_NO) AS SERIAL_NO FROM DSP_DELV_SCHLD_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_DELV=" & Val(CStr(pDSNo)) & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                I = IIf(IsDBNull(RsTemp.Fields("SERIAL_NO").Value), 1, RsTemp.Fields("SERIAL_NO").Value) + 1
            End If
        End If

        SqlStr = "DELETE FROM TEMP_PUR_DAILY_SCHLD_DET WHERE UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM DSP_DELV_SCHLD_DET  " & vbCrLf _
            & " WHERE AUTO_KEY_DELV=" & Val(CStr(pDSNo)) & " " & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "'"

        PubDBCn.Execute(SqlStr)


        SqlStr = "DELETE FROM DSP_DAILY_SCHLD_DET " & vbCrLf _
            & " WHERE AUTO_KEY_DELV=" & Val(CStr(pDSNo)) & " " & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "'"

        PubDBCn.Execute(SqlStr)


        For mDay = 1 To mLastDay
            mDate = VB6.Format(mDay & "/" & VB6.Format(pSchdDate, "MM/YYYY"), "DD/MM/YYYY")
            If IsHoliday(mDate) = False Then
                mWorkingDays = mWorkingDays + 1
            End If
        Next

        If mWorkingDays > 0 Then
            mDailyPlanQty = System.Math.Round(pDSQty / mWorkingDays, 0)
        End If


        mBalQty = pDSQty
        For mDay = 1 To mLastDay
            mDate = VB6.Format(mDay & "/" & VB6.Format(pSchdDate, "MM/YYYY"), "DD/MM/YYYY")
            If IsHoliday(mDate) = False Then
                If mBalQty > mDailyPlanQty Then
                    mDailySchdQty = mDailyPlanQty
                Else
                    mDailySchdQty = mBalQty
                End If
                mBalQty = mBalQty - mDailySchdQty
                If mBalQty < 0 Then
                    mBalQty = 0
                End If
            Else
                mDailySchdQty = 0
            End If

            If mDay < 8 Then
                mWeek1Qty = mWeek1Qty + mDailySchdQty
            ElseIf mDay < 15 Then
                mWeek2Qty = mWeek2Qty + mDailySchdQty
            ElseIf mDay < 22 Then
                mWeek3Qty = mWeek3Qty + mDailySchdQty
            ElseIf mDay < 29 Then
                mWeek4Qty = mWeek4Qty + mDailySchdQty
            Else
                mWeek5Qty = mWeek5Qty + mDailySchdQty
            End If

            SqlStr = "INSERT INTO TEMP_PUR_DAILY_SCHLD_DET (" & vbCrLf _
                & " USERID, AUTO_KEY_DELV,  ITEM_CODE, " & vbCrLf _
                & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE )" & vbCrLf & " VALUES ( " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & Val(CStr(pDSNo)) & ", '" & MainClass.AllowSingleQuote(pItemCode) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mDailySchdQty & ", 0, " & vbCrLf _
                & " 0, '" & MainClass.AllowSingleQuote(pPartyCode) & "', TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

            PubDBCn.Execute(SqlStr)
        Next

        ''SERIAL_NO, " & mDay & ",						

        SqlStr = ""

        If pItemCode <> "" Then 'And mTotQty > 0 '''If DS Amend Then Print ...						
            SqlStr = " INSERT INTO DSP_DELV_SCHLD_DET ( " & vbCrLf _
                & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                & " ITEM_UOM, WEEK1_QTY, WEEK2_QTY, " & vbCrLf _
                & " WEEK3_QTY, WEEK4_QTY, " & vbCrLf _
                & " WEEK5_QTY, ITEM_QTY, " & vbCrLf _
                & " AMEND_NO, COMPANY_CODE, LOC_CODE, AMEND_REASON) "

            SqlStr = SqlStr & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & Val(CStr(pDSNo)) & "," & I & ", " & vbCrLf _
                & " '" & pItemCode & "','" & mPurchaseUOM & "', " & vbCrLf _
                & " " & mWeek1Qty & ", " & mWeek2Qty & ", " & vbCrLf _
                & " " & mWeek3Qty & "," & mWeek4Qty & "," & mWeek5Qty & ", " & vbCrLf _
                & " " & pDSQty & "," & vbCrLf _
                & " " & pDSAmendNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mStoreLoc & "','') "

            PubDBCn.Execute(SqlStr)

            SqlStr = "INSERT INTO DSP_DAILY_SCHLD_DET (" & vbCrLf _
                & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE, REQ_DATE, LOC_CODE )" & vbCrLf _
                & " SELECT " & vbCrLf _
                & " AUTO_KEY_DELV, " & I & ", ITEM_CODE, " & vbCrLf _
                & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE, SCHLD_DATE, '" & MainClass.AllowSingleQuote(mStoreLoc) & "' " & vbCrLf _
                & " FROM TEMP_PUR_DAILY_SCHLD_DET " & vbCrLf _
                & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pPartyCode) & "'" & vbCrLf _
                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            PubDBCn.Execute(SqlStr)

            SqlStr = "INSERT INTO DSP_DAILY_SCHLD_LOG_DET (" & vbCrLf _
                & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                & " SERIAL_DATE, PLANNED_QTY,  " & vbCrLf _
                & " AMEND_NO, LOC_CODE )" & vbCrLf _
                & " SELECT " & vbCrLf _
                & " " & Val(CStr(pDSNo)) & ", " & I & ", ITEM_CODE, " & vbCrLf _
                & " SERIAL_DATE, PLANNED_QTY,  " & vbCrLf _
                & " " & Val(CStr(pDSAmendNo)) & ",'" & MainClass.AllowSingleQuote(mStoreLoc) & "' " & vbCrLf _
                & " FROM TEMP_PUR_DAILY_SCHLD_DET " & vbCrLf _
                & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pPartyCode) & "'" & vbCrLf _
                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            PubDBCn.Execute(SqlStr)

        End If

        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume						
    End Function
    Private Function IsHoliday(pDate As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        IsHoliday = True
        If IsDate(pDate) Then
            SqlStr = " SELECT HOLIDAY_DATE FROM PAY_HOLIDAY_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND HOLIDAY_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                IsHoliday = True
            Else
                IsHoliday = False
            End If
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Function
End Class
