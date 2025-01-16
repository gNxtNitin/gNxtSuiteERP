Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Friend Class frmParamSOReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    'Private PvtDBCn As ADODB.Connection

    Private Const ColLocked As Short = 1
    Private Const ColSONo As Short = 2
    Private Const ColSODate As Short = 3
    Private Const ColAmendNo As Short = 4
    Private Const ColCustomer As Short = 5
    Private Const ColVendorCode As Short = 6
    Private Const ColCustomerAdd As Short = 7
    Private Const ColCustomerCity As Short = 8
    Private Const ColCustomerState As Short = 9
    Private Const ColVSNo As Short = 10
    Private Const ColVSDate As Short = 11
    Private Const ColCustAmendNo As Short = 12
    Private Const ColWEF As Short = 13
    Private Const ColOrderValue As Short = 14
    Private Const ColItemCode As Short = 15
    Private Const ColItemPartNo As Short = 16
    Private Const ColItemDesc As Short = 17
    Private Const ColItemSize As Short = 18
    Private Const ColItemModel As Short = 19
    Private Const ColItemDrawing As Short = 20
    Private Const ColUnit As Short = 21
    Private Const ColStoreLoc As Short = 22
    Private Const ColItemPrice As Short = 23
    Private Const ColItemMRP As Short = 24
    Private Const ColItemMC As Short = 25
    Private Const ColItemPC As Short = 26
    Private Const ColItemMSC As Short = 27
    Private Const ColItemFC As Short = 28
    Private Const ColPackQty As Short = 29
    Private Const ColSOQty As Short = 30
    Private Const ColMRP As Short = 31


    Private Const ColDisc As Short = 32
    Private Const ColTODDisc As Short = 33
    Private Const ColOTHDisc As Short = 34

    Private Const ColSaleQty As Short = 35
    Private Const ColBalQty As Short = 36
    Private Const ColSOAmount As Short = 37
    Private Const ColSaleAmount As Short = 38
    Private Const ColBalAmount As Short = 39

    Private Const ColProjectName As Short = 40
    Private Const ColSalePersonName As Short = 41
    Private Const ColPaymentType As Short = 42
    Private Const ColChequeNo As Short = 43
    Private Const ColAccountHead As Short = 44
    Private Const ColPINO As Short = 45
    Private Const ColMKEY As Short = 46
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
        On Error GoTo ErrHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonPO(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '  Set frmCurrent = Me
        ''  If optRandom Then: If Check_ListValue(lstBmr) = False Then Exit Sub
        '  DoEvents
        '  iLineNo = 1
        '  iPageNo = 1
        '  If sPrintcurrent = False Then: Exit Sub
        '  sClosePort
        '  Set frmOutput = New frmReportViewer
        '  frmOutput.text = gStrReportHeading
        '  frmOutput.txtTotalPages.Text = CStr(iPageNo)
        '  frmOutput.wbrView.Navigate strFileName
        '  frmOutput.Tag = strFileName
        '  gStrReportHeading = ""
        '  frmOutput.Show vbModal
        '  DoEvents
        '  Screen.MousePointer = vbNormal
        '  Set frmOutput = Nothing
        Exit Sub
ErrHandler:
        MsgBox("Error Occured !!!..." & Chr(13) & "[" & Err.Number & "] " & Err.Description, MsgBoxStyle.Critical, My.Application.Info.Title)
        Exit Sub
    End Sub

    Private Function sPrintcurrent() As Boolean
        On Error GoTo ErrHandler
        'Dim adoRs As New ADODB.Recordset
        'Dim tamt As Double, Sql As String
        'Dim cnt As Integer, I As Integer, ed As String, st As String
        'Dim POTYPENo As String, disval As Double
        'Dim ChkStatus As String, tempval As String
        'Dim qty As Double, rate As Double
        'Dim Total As Double, DiscLab As String
        'Dim STVal As Double, EDVal As Double
        'Dim Item As String
        '
        '  Sql = MakeSQL
        '  MainClass.UOpenRecordSet Sql, PubDBCn, adOpenStatic, adoRs, adLockReadOnly
        '  If adoRs.EOF = True Then
        '    MsgBox "No records exist"
        '    sPrintcurrent = False
        '    Exit Function
        '  End If
        '  pheight = 71
        '  pPaper = vbPRPSFanfoldUS
        '  Orient = 1
        '  fOpenPort Me
        '  iLineNo = 1
        '  iPageWidth = 132
        '  tamt = 0
        '  sSendToPort ""
        '  sSendToPort Center(iPageWidth, ComName)
        '  sSendToPort ""
        '
        '  gStrReportHeading = gStrReportHeading & "Sales Order Listing between " & txtDateFrom.Text & " and " & txtDateTo.Text
        '  sSendToPort Center(iPageWidth, gStrReportHeading)
        '  sSendToPort ""
        '
        '  sPageHeader
        '  I = 1
        '  tamt = 0
        '  POTYPENo = ""
        '  ChkStatus = ""
        '  Do While Not adoRs.EOF
        '    disval = 0#
        '    Total = 0
        '    qty = 0
        '    rate = 0
        '    ed = 0
        '    st = 0
        '    EDVal = 0
        '    STVal = 0
        '    DiscLab = ""
        '    qty = 0
        '    rate = IIf(IsNull(adoRs!ITEM_PRICE), 0, adoRs!ITEM_PRICE)
        '    DiscLab = 0
        '
        '    'disval = IIf(IsNull(adoRs("distag")), IIf(IsNull(adoRs("discount")), 0, adoRs("discount")), (Qty * Rate * IIf(IsNull(adoRs("discount")), 0, adoRs("discount")) / 100))
        ''    ed = Find_Value("sal_stedma", "stedper", "edcd", adoRs("ED"), "stedtag", "E")
        ''    EDVal = (qty * rate * IIf(Trim(ed) = "", 0, ed)) / 100
        ''    st = Find_Value("sal_stedma", "stedper", "edcd", adoRs("st"), "stedtag", "S")
        ''    STVal = (qty * rate * IIf(Trim(st) = "", 0, st)) / 100
        ''    Total = ((qty * rate) - disval) + EDVal + STVal
        '
        '
        ''    Item = Find_Value("sal_custitemmast", "custpartno", "custcode", adoRs("custcode"), "item_code", adoRs("Item_Code"))
        '    sSendToPort RPad(15, IIf(POTYPENo = adoRs("AUTO_KEY_SO"), "", adoRs("AUTO_KEY_SO") & "[" & adoRs("CUST_PO_NO") & "]")) & Space(1) & RPad(25, IIf(POTYPENo = adoRs("AUTO_KEY_SO"), "", Trim(adoRs("SUPP_CUST_CODE")))) & Space(1) & RPad(25, Trim(Item)) & Space(1) & LPad(10, IIf(qty > 0, qty, "Open")) & Space(1) & LPad(10, (qty * rate)) & Space(1) & LPad(10, "") & Space(1) & LPad(10, ed) & Space(1) & LPad(8, st) & Space(1) & LPad(10, CStr(Total))
        '    sSendToPort RPad(15, IIf(POTYPENo = adoRs("AUTO_KEY_SO"), "", Format(adoRs("SO_DATE"), "dd/mm/yyyy"))) & Space(1) & RPad(25, IIf(POTYPENo = adoRs("AUTO_KEY_SO"), "", adoRs("SUPP_CUST_NAME"))) & Space(1) & RPad(25, Trim(adoRs("ITEM_SHORT_DESC"))) & Space(1) & LPad(10, IIf(IsNull(adoRs("ITEM_PRICE")), 0, adoRs("ITEM_PRICE"))) & Space(12) & LPad(10, CStr(disval)) & Space(1) & LPad(10, CStr(EDVal)) & Space(1) & LPad(8, CStr(STVal)) & Space(1) & LPad(10, "")
        '    tamt = tamt + Total
        '    I = I + 1
        '    POTYPENo = adoRs("AUTO_KEY_SO")
        '    ChkStatus = ""          ''IIf(IsNull(adoRs("authstatus")), "", adoRs("authstatus"))
        '    adoRs.MoveNext
        '    sSendToPort ""
        '  Loop
        '  If adoRs.EOF Then: sSendToPort ""
        '  sSendToPort Space(114) & RPad(5, "Total") & Space(2) & LPad(10, Format(tamt, "0.00"))
        '  adoRs.Close
        '  Call EndofReport(iPageWidth, I - 1)
        '  sPrintcurrent = True
        '  Set adoRs = Nothing
        Exit Function
ErrHandler:
        MsgBox("Error Occured !!!..." & Chr(13) & "[" & Err.Number & "] " & Err.Description, MsgBoxStyle.Critical, My.Application.Info.Title)
        Exit Function
    End Function

    Public Sub sPageHeader()
        '  sSendToPort "Print Date: " & vb6.Format(Date, "DD/MM/YYYY") & LPad(iPageWidth - 25, "Page No.: " & CStr(iPageNo))
        '  sSendToPort String(iPageWidth, "-")
        '  sSendToPort RPad(15, "Order No") & Space(1) & RPad(25, "Customer Code") & Space(1) & RPad(25, "Item Code") & Space(1) & LPad(10, "Order Qty") & Space(1) & LPad(10, "Goods") & Space(1) & LPad(10, "Disc.%") & Space(1) & LPad(10, "ED %") & Space(1) & LPad(8, "ST %") & Space(1) & LPad(10, "Total")
        '  sSendToPort RPad(15, "Order Date") & Space(1) & RPad(25, "Customer Name") & Space(1) & RPad(25, "Item Desc.") & Space(1) & LPad(10, "Rate") & Space(1) & LPad(10, "value") & Space(1) & LPad(10, "Disc. Val") & Space(1) & LPad(10, "ED val") & Space(1) & LPad(8, "ST Val") & Space(1) & LPad(10, "Amount")
        '  sSendToPort String(iPageWidth, "-")
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonPO(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonPO(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String


        Report1.Reset()
        SqlStr = MakeSQL("S")


        mTitle = "Sales Order Register"

        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        If OptOrderBy(0).Checked = True Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SOReg_POWise.RPT"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SOReg_CustWise.RPT"
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

    Private Sub cmdsearchSupp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSupp.Click
        SearchSupplier()
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1("S") = False Then GoTo ErrPart
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSOReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Sales Order Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSOReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

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
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False
        txtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False

        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False

        Call PrintStatus(True)
        Call FillPOCombo()
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Call Show1("L")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamSOReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSOReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub
    'Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent)
    '    SprdMain.Row = -1
    '    SprdMain.Col = eventArgs.col
    '    SprdMain.DAutoCellTypes = True
    '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
    '    SprdMain.TypeEditLen = 1000
    'End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xPoNo As Double
        Dim xAmendPONo As Double
        Dim mOrderType As String
        Dim mWEF As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        xPoNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColSONo - 1))
        xAmendPONo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmendNo - 1))
        mWEF = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColWEF - 1))


        mWEF = VB6.Format(mWEF, "DD/MM/YYYY")

        SqlStr = "SELECT * from DSP_SALEORDER_HDR WHERE AUTO_KEY_SO=" & xPoNo & " AND AMEND_NO=" & xAmendPONo & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mOrderType = IIf(IsDBNull(RsTemp.Fields("ORDER_TYPE").Value), "O", RsTemp.Fields("ORDER_TYPE").Value)

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                frmSalesOrderGSTNew.MdiParent = Me.MdiParent
                frmSalesOrderGSTNew.Show()
                frmSalesOrderGSTNew.lblType.Text = mOrderType
                frmSalesOrderGSTNew.lblAddItem.Text = "N"
                frmSalesOrderGSTNew.frmSalesOrderGSTNew_Activated(Nothing, New System.EventArgs())

                frmSalesOrderGSTNew.txtSONo.Text = RsTemp.Fields("AUTO_KEY_SO").Value
                frmSalesOrderGSTNew.txtAmendNo.Text = RsTemp.Fields("AMEND_NO").Value

                frmSalesOrderGSTNew.txtSONo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
            Else
                frmSalesOrderGST.MdiParent = Me.MdiParent
                frmSalesOrderGST.Show()
                frmSalesOrderGST.lblType.Text = mOrderType
                frmSalesOrderGST.lblAddItem.Text = "N"
                frmSalesOrderGST.frmSalesOrderGST_Activated(Nothing, New System.EventArgs())

                frmSalesOrderGST.txtSONo.Text = RsTemp.Fields("AUTO_KEY_SO").Value
                frmSalesOrderGST.txtAmendNo.Text = RsTemp.Fields("AMEND_NO").Value

                frmSalesOrderGST.txtSONo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
            End If

        End If
    End Sub
    'Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)

    '    Dim SqlStr As String = ""
    '    Dim RsTemp As ADODB.Recordset = Nothing
    '    Dim xPoNo As Double
    '    Dim xAmendPONo As Double
    '    Dim mOrderType As String
    '    Dim mWEF As String

    '    'Dim ss As New frmPO

    '    SprdMain.Row = SprdMain.ActiveRow

    '    SprdMain.Col = ColSONo
    '    xPoNo = Val(SprdMain.Text)

    '    SprdMain.Col = ColAmendNo
    '    xAmendPONo = Val(SprdMain.Text)

    '    SprdMain.Col = ColWEF
    '    mWEF = VB6.Format(SprdMain.Text, "DD/MM/YYYY")

    '    SqlStr = "SELECT * from DSP_SALEORDER_HDR WHERE AUTO_KEY_SO=" & xPoNo & " AND AMEND_NO=" & xAmendPONo & ""
    '    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

    '    If RsTemp.EOF = False Then
    '        mOrderType = IIf(IsDBNull(RsTemp.Fields("ORDER_TYPE").Value), "O", RsTemp.Fields("ORDER_TYPE").Value)

    '        frmSalesOrderGST.MdiParent = Me.MdiParent
    '        frmSalesOrderGST.Show()
    '        frmSalesOrderGST.lblType.Text = mOrderType
    '        frmSalesOrderGST.lblAddItem.Text = "N"
    '        frmSalesOrderGST.frmSalesOrderGST_Activated(Nothing, New System.EventArgs())

    '        frmSalesOrderGST.txtSONo.Text = RsTemp.Fields("AUTO_KEY_SO").Value
    '        frmSalesOrderGST.txtAmendNo.Text = RsTemp.Fields("AMEND_NO").Value

    '        frmSalesOrderGST.txtSONo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    '    End If


    'End Sub

    Private Sub txtCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.DoubleClick
        SearchCategory()
    End Sub
    Private Sub txtCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCategory()
    End Sub
    Private Sub txtCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            txtCategory.Text = UCase(Trim(txtCategory.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster((txtCategory.Text), "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtCategory.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
    End Sub

    Private Sub chkAllCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCategory.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCategory.Enabled = False
            cmdsearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdsearchCategory.Enabled = True
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
        Dim mAccountCode As String

        SqlStr = " SELECT DISTINCT ITEMMST.ITEM_SHORT_DESC, ID.ITEM_CODE,  ID.PART_NO "

        SqlStr = SqlStr & vbCrLf _
            & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, INV_ITEM_MST ITEMMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " And ID.COMPANY_CODE=ITEMMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ITEM_CODE=ITEMMST.ITEM_CODE"



        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
            End If
        End If

        MainClass.SearchGridMasterBySQL2(TxtItemName.Text, SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
        End If

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "CUSTOMER_PART_NO", , SqlStr)
        'If AcName <> "" Then
        '    TxtItemName.Text = AcName
        'End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchSupplier()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = " SELECT DISTINCT CMST.SUPP_CUST_NAME, ID.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, FIN_SUPP_CUST_MST CMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " And ID.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And ID.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"


        MainClass.SearchGridMasterBySQL2(txtSupplier.Text, SqlStr)
        If AcName <> "" Then
            txtSupplier.Text = AcName
        End If

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        'MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        'If AcName <> "" Then
        '    txtSupplier.Text = AcName
        'End If
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

        'Dim cntCol As Integer
        'With SprdMain
        '    .MaxCols = ColMKEY
        '    .set_RowHeight(0, RowHeight * 1.2)
        '    .set_ColWidth(0, 4.5)

        '    .set_RowHeight(-1, RowHeight)
        '    .Row = -1

        '    .Col = ColLocked
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColLocked, 15)
        '    .ColHidden = True

        '    .Col = ColSONo
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColSONo, 9)

        '    .Col = ColSODate
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColSODate, 9)

        '    .Col = ColCustomer
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColCustomer, 20)

        '    .Col = ColVSNo
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColSONo, 9)

        '    .Col = ColVSDate
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColVSDate, 9)

        '    .Col = ColAmendNo
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColAmendNo, 5)

        '    .Col = ColCustAmendNo
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColCustAmendNo, 5)

        '    .ColsFrozen = ColAmendNo

        '    .Col = ColWEF
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColWEF, 9)


        '    .Col = ColItemCode
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColItemCode, 8)

        '    .Col = ColItemPartNo
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColItemPartNo, 8)


        '    .Col = ColItemDesc
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColItemDesc, 30)

        '    .Col = ColUnit
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColUnit, 4)

        '    .Col = ColStoreLoc
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColStoreLoc, 4)

        '    For cntCol = ColItemPrice To ColItemFC
        '        .Col = cntCol
        '        .CellType = SS_CELL_TYPE_FLOAT
        '        .TypeFloatDecimalPlaces = 4
        '        .TypeFloatMin = CDbl("-99999999999")
        '        .TypeFloatMax = CDbl("99999999999")
        '        .TypeFloatMoney = False
        '        .TypeFloatSeparator = False
        '        .TypeFloatDecimalChar = Asc(".")
        '        .TypeFloatSepChar = Asc(",")
        '        .set_ColWidth(cntCol, 9)
        '    Next

        '    .Col = ColMKEY
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColMKEY, 8)
        '    .ColHidden = True


        '    MainClass.SetSpreadColor(SprdMain, -1)
        '    MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
        '    SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        '    SprdMain.DAutoCellTypes = True
        '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        '    SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        'End With
    End Sub
    Private Function Show1(pShowType As String) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL(pShowType)
        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        FillUltraGrid(SqlStr)
        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL(pShowType As String) As String

        On Error GoTo ERR1
        Dim mSupplier As String
        Dim mCatCode As String = ""

        ''SELECT CLAUSE...

        MakeSQL = " SELECT IH.SUPP_CUST_CODE," & vbCrLf _
            & " IH.AUTO_KEY_SO," & vbCrLf _
            & " TO_CHAR(IH.SO_DATE,'DD/MM/YYYY') AS SO_DATE, AMEND_NO," & vbCrLf _
            & " CMST.SUPP_CUST_NAME, IH.VENDOR_CODE, CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE," & vbCrLf _
            & " IH.CUST_PO_NO," & vbCrLf _
            & " TO_CHAR(IH.CUST_PO_DATE,'DD/MM/YYYY')," & vbCrLf _
            & " CUST_AMEND_NO, TO_CHAR(ID.AMEND_WEF,'DD/MM/YYYY'), "

        'MakeSQL = MakeSQL & vbCrLf _
        '    & " (SELECT SUM(GROSS_ITEMAMOUNT) AS GROSS_ITEMAMOUNT FROM DSP_SALEORDER_DET WHERE MKEY=IH.MKEY) ORDERVALUE,"

        MakeSQL = MakeSQL & vbCrLf _
            & " 0 ORDERVALUE,"

        MakeSQL = MakeSQL & vbCrLf _
            & " ID.ITEM_CODE, ID.PART_NO, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " ID.ITEM_SIZE, ID.ITEM_MODEL,ID.ITEM_DRAWINGNO,"

        MakeSQL = MakeSQL & vbCrLf _
            & " ID.UOM_CODE, ID.CUST_STORE_LOC, TO_CHAR(ITEM_PRICE) As ITEM_PRICE, ID.ITEM_MRP, " & vbCrLf _
            & " ID.MATERIAL_COST, ID.PROCESS_COST, ID.MSP_COST, ID.FREIGHT_COST, "



        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            MakeSQL = MakeSQL & vbCrLf _
                & " ID.PACK_QTY, ID.SO_QTY, ID.ITEM_MRP As MRP, ID.ITEM_DISC, ID.TOD_DISC, ID.OTH_DISC, "

            ''GETDSDESPATCHQTY(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE),TO_DATE('01-Dec-2022','DD-MON-YYYY'),TO_DATE('23-Jan-2023','DD-MON-YYYY')) * GetSORATE(IH.COMPANY_CODE,TO_DATE('23-Jan-2023','DD-MON-YYYY'),IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE)
            If chkDspDetail.CheckState = System.Windows.Forms.CheckState.Checked Then

                MakeSQL = MakeSQL & vbCrLf _
                    & " GETDESPATCHQTYAGTSO(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE), NVL(ID.ITEM_MODEL,' '), NVL(ID.CHARGEABLE_HEIGHT,0), NVL(ID.CHARGEABLE_WIDTH,0)) As DESP_QTY," & vbCrLf _
                    & " ID.SO_QTY-GETDESPATCHQTYAGTSO(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE), NVL(ID.ITEM_MODEL,' '), NVL(ID.CHARGEABLE_HEIGHT,0), NVL(ID.CHARGEABLE_WIDTH,0)) As BAL_QTY," & vbCrLf _
                    & " ID.SO_QTY*ITEM_PRICE As SO_AMT," & vbCrLf _
                    & " GETDESPATCHQTYAGTSO(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE), NVL(ID.ITEM_MODEL,' '), NVL(ID.CHARGEABLE_HEIGHT,0), NVL(ID.CHARGEABLE_WIDTH,0))*ITEM_PRICE As DESP_AMT," & vbCrLf _
                    & " (ID.SO_QTY-GETDESPATCHQTYAGTSO(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE), NVL(ID.ITEM_MODEL,' '), NVL(ID.CHARGEABLE_HEIGHT,0), NVL(ID.CHARGEABLE_WIDTH,0)))*ITEM_PRICE As BAL_AMT,"

            Else
                MakeSQL = MakeSQL & vbCrLf _
                    & " 0,0,ID.SO_QTY*ITEM_PRICE As SO_AMT,0,0,"

            End If
        Else
            MakeSQL = MakeSQL & vbCrLf _
                & " 0,0,0,0,0,0,0,0,0,0,0 ,"
        End If

        MakeSQL = MakeSQL & vbCrLf _
            & " PMST.NAME, EMP.EMP_NAME, PAYMENT_TYPE, CHEQUE_NO, "

        MakeSQL = MakeSQL & vbCrLf _
            & " (SELECT MAX(SUPP_CUST_NAME) FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=IH.COMPANY_CODE AND SUPP_CUST_CODE=INVTYPEMST.ACCOUNTPOSTCODE) AS ACCOUNTHEAD, "

        MakeSQL = MakeSQL & vbCrLf _
            & " IH.AUTO_KEY_PI,IH.AUTO_KEY_SO "

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID," & vbCrLf _
            & " FIN_SUPP_CUST_BUSINESS_MST CMST, INV_ITEM_MST INVMST, FIN_INVTYPE_MST INVTYPEMST, DSP_PROJECT_MST PMST, PAY_EMPLOYEE_MST EMP"

        ''

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And IH.MKEY=ID.MKEY" & vbCrLf _
            & " And IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " And IH.BILL_TO_LOC_ID=CMST.LOCATION_ID " & vbCrLf _
            & " And IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " And IH.COMPANY_CODE=INVTYPEMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ACCOUNT_POSTING_CODE=INVTYPEMST.CODE "

        MakeSQL = MakeSQL & vbCrLf _
            & " And IH.COMPANY_CODE=PMST.COMPANY_CODE(+)" & vbCrLf _
            & " And IH.PROJECT_CODE=PMST.CODE(+) " & vbCrLf _
            & " And IH.SALE_PERSON_CODE=EMP.EMP_CODE(+) "


        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IH.AMEND_WEF_FROM>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.AMEND_WEF_FROM<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        If cboStatus.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.SO_STATUS='O'"
        ElseIf cboStatus.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.SO_STATUS='C'"
        End If

        If cboApproval.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.SO_APPROVED='Y'"
        ElseIf cboApproval.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.SO_APPROVED='N'"
        End If

        If cboType.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.ORDER_TYPE='O'"
        ElseIf cboType.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.ORDER_TYPE='C'"
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND (ID.SO_QTY>GETDESPATCHQTYAGTSO(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE), NVL(ID.ITEM_MODEL,' '), NVL(ID.CHARGEABLE_HEIGHT,0), NVL(ID.CHARGEABLE_WIDTH,0)) AND SO_ITEM_STATUS='N')"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND ID.SO_QTY<=GETDESPATCHQTYAGTSO(IH.COMPANY_CODE,IH.SUPP_CUST_CODE, IH.AUTO_KEY_SO,TRIM(ID.ITEM_CODE), NVL(ID.ITEM_MODEL,' '), NVL(ID.CHARGEABLE_HEIGHT,0), NVL(ID.CHARGEABLE_WIDTH,0))"
        ElseIf cboShow.SelectedIndex = 3 Then
            MakeSQL = MakeSQL & vbCrLf & "AND SO_ITEM_STATUS='Y'"
        End If

        If pShowType = "L" Then
            MakeSQL = MakeSQL & vbCrLf & "AND 1=2"
        End If
        ''ORDER CLAUSE...
        If OptOrderBy(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.SO_DATE, IH.AUTO_KEY_SO, IH.AMEND_NO, ID.SERIAL_NO"
        ElseIf OptOrderBy(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME, IH.SO_DATE, IH.AUTO_KEY_SO, IH.AMEND_NO"
        ElseIf OptOrderBy(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME, ID.ITEM_CODE,IH.MKEY" ''AMEND_NO, TO_CHAR(ID.AMEND_WEF,'DD/MM/YYYY')"
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub FillUltraGrid(pMakeSql As String)
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        'UltraGrid1.DataSource.Rows.Clear()
        Me.UltraGrid1.DataSource = Nothing
        oledbCnn = New OleDbConnection(StrConn)
        Try

            ClearGroupFromUltraGrid(UltraGrid1)
            ClearFilterFromUltraGrid(UltraGrid1)
            oledbCnn.Open()
            oledbAdapter = New OleDbDataAdapter(pMakeSql, oledbCnn)

            oledbAdapter.Fill(ds)

            ' Set the data source and data member to bind the grid.
            Me.UltraGrid1.DataSource = ds
            Me.UltraGrid1.DataMember = ""

            CreateGridHeader()

            oledbAdapter.Dispose()
            oledbCnn.Close()
        Catch ex As Exception
            MsgBox("Can not open connection ! ")
        End Try
    End Sub
    Private Sub CreateGridHeader()
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header


            UltraGrid1.DisplayLayout.Bands(0).Columns(0).RowLayoutColumnInfo.PreferredLabelSize = New System.Drawing.Size(0, 40)
            UltraGrid1.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Caption = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSONo - 1).Header.Caption = "Sale Order No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSODate - 1).Header.Caption = "Sale Order Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmendNo - 1).Header.Caption = "Amend No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomer - 1).Header.Caption = "Customer"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVSNo - 1).Header.Caption = "Customer PO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVSDate - 1).Header.Caption = "Customer PO Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustAmendNo - 1).Header.Caption = "Customer Amend. No"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColWEF - 1).Header.Caption = "WEF"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOrderValue - 1).Header.Caption = "Order Value"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Header.Caption = "Item Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPartNo - 1).Header.Caption = "Item Part No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDesc - 1).Header.Caption = "Item Desciption"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemSize - 1).Header.Caption = "Item Size"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemModel - 1).Header.Caption = "Item Model"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDrawing - 1).Header.Caption = "Item Drawing No"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnit - 1).Header.Caption = "UOM"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStoreLoc - 1).Header.Caption = "Store Location"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPrice - 1).Header.Caption = "Item Price"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMRP - 1).Header.Caption = "Item MRP"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMC - 1).Header.Caption = "Item MC"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPC - 1).Header.Caption = "Item PC"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMSC - 1).Header.Caption = "Item MSC"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemFC - 1).Header.Caption = "Item FC"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSOQty - 1).Header.Caption = "Order Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMRP - 1).Header.Caption = "MRP"



            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDisc - 1).Header.Caption = "Discount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTODDisc - 1).Header.Caption = "TOD Discount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOTHDisc - 1).Header.Caption = "Other Discount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackQty - 1).Header.Caption = "Pack Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleQty - 1).Header.Caption = "Sale Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalQty - 1).Header.Caption = "Balance Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSOAmount - 1).Header.Caption = "Order Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleAmount - 1).Header.Caption = "Sale Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalAmount - 1).Header.Caption = "Balance Amount"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColProjectName - 1).Header.Caption = "Project Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSalePersonName - 1).Header.Caption = "Sale Person Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPaymentType - 1).Header.Caption = "Payment Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChequeNo - 1).Header.Caption = "Cheque No"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPINO - 1).Header.Caption = "PI No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAccountHead - 1).Header.Caption = "Account Head"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Header.Caption = "MKey"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVendorCode - 1).Header.Caption = "Vendor Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerAdd - 1).Header.Caption = "Address"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerCity - 1).Header.Caption = "City"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerState - 1).Header.Caption = "State"




            ''UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Appearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPrice - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMRP - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMC - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPC - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMSC - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemFC - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOrderValue - 1).Style = UltraWinGrid.ColumnStyle.Double

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSOQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMRP - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDisc - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTODDisc - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOTHDisc - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSOAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalAmount - 1).Style = UltraWinGrid.ColumnStyle.Double

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPrice - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMRP - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMC - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPC - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMSC - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemFC - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOrderValue - 1).CellAppearance.TextHAlign = HAlign.Right

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSOQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMRP - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDisc - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTODDisc - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOTHDisc - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSOAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalAmount - 1).CellAppearance.TextHAlign = HAlign.Right


            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Hidden = True

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMRP - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMC - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPC - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMSC - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemFC - 1).Hidden = True

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOrderValue - 1).Hidden = True ' IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSOQty - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMRP - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDisc - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTODDisc - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOTHDisc - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackQty - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleQty - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalQty - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSOAmount - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleAmount - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalAmount - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemSize - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemModel - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDrawing - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColProjectName - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSalePersonName - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPaymentType - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChequeNo - 1).Hidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)


            If lblReportType.Text = "P" Then
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerAdd - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerCity - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerState - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColWEF - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColOrderValue - 1).Hidden = True
                '
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColStoreLoc - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackQty - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColMRP - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColDisc - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColTODDisc - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColOTHDisc - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColProjectName - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColSalePersonName - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColPaymentType - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColChequeNo - 1).Hidden = True
            End If


            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSONo - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSODate - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmendNo - 1).Width = 50
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomer - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVSNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVSDate - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustAmendNo - 1).Width = 50
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColWEF - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOrderValue - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAccountHead - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPINO - 1).Width = 80

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPartNo - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDesc - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnit - 1).Width = 50

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStoreLoc - 1).Width = 50
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPrice - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMRP - 1).Width = 90

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMC - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPC - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemMSC - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemFC - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVendorCode - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerAdd - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerCity - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerState - 1).Width = 120

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemSize - 1).Width = 150
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemModel - 1).Width = 150
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDrawing - 1).Width = 150
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColProjectName - 1).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSalePersonName - 1).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPaymentType - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChequeNo - 1).Width = 120


            For inti = ColSOQty To ColBalAmount
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti - 1).Width = 100
            Next



            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Width = 90

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
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
    Private Sub FillPOCombo()
        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        cboStatus.Items.Clear()
        cboStatus.Items.Add("All")
        cboStatus.Items.Add("Open")
        cboStatus.Items.Add("Closed")
        cboStatus.SelectedIndex = 1
        cboStatus.Enabled = IIf(lblReportType.Text = "P", False, True)

        cboApproval.Items.Clear()
        cboApproval.Items.Add("All")
        cboApproval.Items.Add("Yes")
        cboApproval.Items.Add("No")
        cboApproval.SelectedIndex = 1
        cboApproval.Enabled = IIf(lblReportType.Text = "P", False, True)

        cboType.Items.Clear()
        cboType.Items.Add("All")
        cboType.Items.Add("Open")
        cboType.Items.Add("Closed")
        cboType.SelectedIndex = 1

        cboShow.Items.Clear()
        cboShow.Items.Add("All")
        cboShow.Items.Add("Pending")
        cboShow.Items.Add("Complete")
        cboShow.Items.Add("Short Close")
        cboShow.SelectedIndex = 1

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
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

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click

        Dim lngLoop As Integer  'loop variable

        Dim objMode As Object 'to store the mode of the row
        Dim objChk As Object 'to get the check status of the first column
        Dim strSplit() As String 'split variable
        Dim intAns As Integer ' to store the result from msgbox
        Dim lngRow As Long
        Try


            'If m_blnChangeInData = True Then
            '    MessageFromResFile(7284, MessageType.Information)
            '    GridSetFocus(UltraGrid1.ActiveRow.Tag.ToString, UltraGrid1.ActiveCell.Column.Index)
            '    Exit Sub
            'End If

            ''Please provide a location where you whould like to export the data to
            'MessageFromResFile(7304, MessageType.Information, GetLabelDes("7305"))

            Try
                SaveFileDialog1.FileName = Me.Text
            Catch
                SaveFileDialog1.FileName = "File1"
            End Try

            Dim strAction As String = ""
            Try
                strAction = SaveFileDialog1.ShowDialog()
            Catch
                SaveFileDialog1.FileName = "File1"
                strAction = SaveFileDialog1.ShowDialog()
            End Try

            If strAction = "1" Then
                ExportToExcel(SaveFileDialog1.FileName)
            End If
            'Me.Cursor = Cursors.Default

        Catch ex As Exception
            ErrorMsg(Err.Description, Err.Number)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ExportToExcel(ByVal strFileName As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   To export the report to excel/csv/text file
        'Comments       :   This function will be called from clickinvoked and enterpressed events
        '                   THIS FUNCTION HAS TO BE OVERRIDED IN THE DERIVED FORM   
        '----------------------------------------------------------------------------
        Me.Cursor = Cursors.WaitCursor
        Dim start As DateTime
        'Dim timespan As TimeSpan
        start = DateTime.Now
        Try
            Me.UltraGridExcelExporter1.FileLimitBehaviour = ExcelExport.FileLimitBehaviour.TruncateData
            Me.UltraGridExcelExporter1.ExportAsync(Me.UltraGrid1, strFileName & ".xls")
            ' timespan = DateTime.Now.Subtract(start)
            'Exported To File : 
            '  MessageFromResFile(7292, MessageType.Information, strFileName)
        Catch
            'Specified Path Does Not Exist,Invalid File Name
            ErrorMsg(Err.Description, Err.Number)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
End Class
