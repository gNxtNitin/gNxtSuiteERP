Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamCustProdVsOurDesp
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Dim PvtDBCn As ADODB.Connection					


    Private Const ColLocked As Short = 1
    Private Const ColCust_Code As Short = 2
    Private Const ColCustomer As Short = 3
    Private Const ColItemCode As Short = 4
    Private Const ColItemName As Short = 5
    Private Const ColItemPartNo As Short = 6
    Private Const ColItemModel As Short = 7
    Private Const ColRate As Short = 8
    Private Const ColSharingPer As Short = 9
    Private Const ColCustProd As Short = 10
    Private Const ColDesp As Short = 11
    Private Const ColDespPer As Short = 12
    Private Const ColLoss As Short = 13
    Private Const ColMKEY As Short = 14

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
        mTitle = "DAILY SHORTAGE REPORT"
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

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
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Show1(RsShow) = False Then GoTo ErrPart
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4					
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmParamCustProdVsOurDesp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Customer Production Vs Our Despatch"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamCustProdVsOurDesp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False

        txtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False


        Call PrintStatus(True)
        'Call FillPOCombo					
        txtDateFrom.Text = VB6.Format(System.DateTime.FromOADate(RunDate.ToOADate - 1), "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(System.DateTime.FromOADate(RunDate.ToOADate - 1), "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamCustProdVsOurDesp_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamCustProdVsOurDesp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
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
            .set_ColWidth(ColCustomer, 15)

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
            .set_ColWidth(ColItemName, 15)

            .Col = ColItemPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemPartNo, 15)

            .Col = ColItemModel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemModel, 15)

            .ColsFrozen = ColItemName

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColRate, 10)

            For cntCol = ColSharingPer To ColLoss
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
    Private Function Show1(ByRef mRs As ADODB.Recordset) As Boolean
        On Error GoTo LedgError
        Dim cntRow As Integer
        Dim pCustProduction As Double
        Dim mCustCode As String
        Dim mItemCode As String
        Dim mSharingPer As Double
        Dim mDespatch As Double
        Dim mDespatchPer As Double
        Dim mModelName As String

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

                    .Col = ColRate
                    .Text = VB6.Format(IIf(IsDBNull(mRs.Fields("AVG_RATE").Value), 0, mRs.Fields("AVG_RATE").Value), "0.00")

                    mModelName = GetModel(mItemCode)
                    mSharingPer = GetCustomerSharingPer(mCustCode, mItemCode)
                    pCustProduction = GetCustData(mCustCode, mItemCode)

                    .Col = ColItemModel
                    .Text = mModelName

                    .Col = ColSharingPer
                    .Text = VB6.Format(mSharingPer, "0")

                    .Col = ColCustProd
                    .Text = VB6.Format(pCustProduction, "0")

                    .Col = ColDesp
                    mDespatch = CDbl(VB6.Format(IIf(IsDBNull(mRs.Fields("DESPQTY").Value), "", mRs.Fields("DESPQTY").Value), "0"))
                    .Text = VB6.Format(IIf(IsDBNull(mRs.Fields("DESPQTY").Value), "", mRs.Fields("DESPQTY").Value), "0")

                    If pCustProduction > 0 Then
                        mDespatchPer = mDespatch * 100 / pCustProduction
                    Else
                        mDespatchPer = 0
                    End If

                    .Col = ColDespPer
                    .Text = VB6.Format(mDespatchPer, "0")

                    .Col = ColLoss
                    .Text = VB6.Format(IIf(mDespatchPer < mSharingPer, mSharingPer - mDespatchPer, 0), "0")


                    cntRow = cntRow + 1
NextRow:
                    mRs.MoveNext()
                Loop
            End If
        End With
        '''********************************					
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


        MakeSQL = " SELECT CMST.SUPP_CUST_CODE," & vbCrLf & " CMST.SUPP_CUST_NAME," & vbCrLf & " INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " SUM(ITEM_QTY) AS DESPQTY, AVG(ITEM_AMT/ITEM_QTY) AS AVG_RATE "

        ''''FROM CLAUSE...					
        MakeSQL = MakeSQL & vbCrLf & " FROM " & vbCrLf & " FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, FIN_INVTYPE_MST INVTYPE"

        ''''WHERE CLAUSE...					
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=INVTYPE.COMPANY_CODE " & vbCrLf & " AND IH.TRNTYPE=INVTYPE.CODE AND IH.CANCELLED='N'" & vbCrLf & " AND INVTYPE.ISSUPPBILL='N' AND INVTYPE.ISSALECOMP='Y'"

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        '''''GROUP BY CLAUSE...					
        MakeSQL = MakeSQL & vbCrLf & " GROUP BY CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC,INVMST.CUSTOMER_PART_NO"

        '''''ORDER CLAUSE...					
        MakeSQL = MakeSQL & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME, INVMST.ITEM_CODE"


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

    Private Function GetCustData(ByRef pCustCode As String, ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetCustData = 0


        ''Production					

        SqlStr = "SELECT SUM(ID.TOTAL_QTY) AS ITEM_QTY" & vbCrLf & " FROM PPC_CUST_PROD_HDR IH, PPC_CUST_PROD_DET ID,INV_MODELWISE_PROD_DET MD " & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF = ID.AUTO_KEY_REF" & vbCrLf & " AND ID.COMPANY_CODE = MD.COMPANY_CODE" & vbCrLf & " AND ID.MODEL_CODE = MD.MODEL_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & pCustCode & "'" & vbCrLf & " AND MD.ITEM_CODE='" & pItemCode & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetCustData = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        RsTemp.Close()

        Exit Function
ErrPart:
        '    Resume					
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetCustData = 0
        RsTemp.Close()
    End Function

    Private Function GetModel(ByRef pItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetModel = ""
        SqlStr = "SELECT DISTINCT GM.MODEL_DESC  " & vbCrLf & " FROM INV_MODELWISE_PROD_DET MD, GEN_MODEL_MST GM  " & vbCrLf & " WHERE MD.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MD.COMPANY_CODE = GM.COMPANY_CODE" & vbCrLf & " AND MD.MODEL_CODE = GM.MODEL_CODE" & vbCrLf & " AND MD.ITEM_CODE='" & pItemCode & "'"

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
    Private Function GetProductionPlan(ByRef pCustCode As String, ByRef pItemCode As String, ByRef pTodayPlan As Double, ByRef pNextDayPlan As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        'Dim pTodayPlan As Double					
        'Dim pNextDayPlan As Double					
        Dim mToDate As String
        Dim mNextToDate As String

        pTodayPlan = 0
        pNextDayPlan = 0

        mToDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(txtDateTo.Text, "DD/MM/YYYY"))))
        mNextToDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mToDate, "DD/MM/YYYY"))))

        SqlStr = "SELECT SUM(DPLAN_QTY) AS ITEM_QTY " & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pCustCode & "'" & vbCrLf & " AND PRODUCT_CODE='" & pItemCode & "'" & vbCrLf & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pTodayPlan = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        SqlStr = "SELECT SUM(IPLAN_QTY) AS ITEM_QTY " & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pCustCode & "'" & vbCrLf & " AND PRODUCT_CODE='" & pItemCode & "'" & vbCrLf & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(mNextToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pNextDayPlan = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        GetProductionPlan = True
        RsTemp.Close()
        Exit Function
ErrPart:
        GetProductionPlan = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        RsTemp.Close()
    End Function
End Class
