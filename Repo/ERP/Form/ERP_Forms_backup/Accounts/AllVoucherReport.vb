Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAllVoucherReport
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Private PvtDBCn As ADODB.Connection				

    Dim mAccountCode As Integer
    Private Const ColLocked As Short = 1
    Private Const ColVNo As Short = 2
    Private Const ColVDate As Short = 3
    Private Const ColSupplierCode As Short = 4
    Private Const ColSupplierName As Short = 5
    Private Const ColAccountCode As Short = 6
    Private Const ColAccountName As Short = 7
    Private Const ColItemCode As Short = 8
    Private Const ColItemName As Short = 9
    Private Const ColItemPartNo As Short = 10
    Private Const ColQty As Short = 11
    Private Const ColRate As Short = 12
    Private Const ColAmount As Short = 13
    Private Const ColTaxableAmount As Short = 14
    Private Const ColGSTRefundAmount As Short = 15
    Private Const ColCGST As Short = 16
    Private Const ColSGST As Short = 17
    Private Const ColIGST As Short = 18
    Private Const ColOthers As Short = 19
    Private Const ColDrCr As Short = 20
    Private Const colRemarks As Short = 21
    Private Const ColBookCode As Short = 22
    Private Const ColBookType As Short = 23
    Private Const ColVType As Short = 24
    Private Const ColMKEY As Short = 25


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
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()

    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1() = False Then GoTo ErrPart
        Call PrintStatus(True)
        CalcSprdTotal()
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4				
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmAllVoucherReport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmAllVoucherReport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)


        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        Call FillInvoiceType()
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub FillInvoiceType()
        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer

        lstInvoiceType.Items.Clear()
        SqlStr = "SELECT DISTINCT CMST.SUPP_CUST_NAME " & vbCrLf & " FROM FIN_INVTYPE_MST A, FIN_SUPP_CUST_MST CMST" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND A.ACCOUNTPOSTCODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND CATEGORY IN ('S','P') ORDER BY CMST.SUPP_CUST_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("SUPP_CUST_NAME").Value)
                lstInvoiceType.SetItemChecked(CntLst, False) '' True				
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0
        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub frmAllVoucherReport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmAllVoucherReport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()

    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        '    SprdMain.Row = -1				
        '    SprdMain.Col = Col				
        '    SprdMain.DAutoCellTypes = True				
        '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH				
        '    SprdMain.TypeEditLen = 1000				
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub

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

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S', 'C')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr				
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String


        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S', 'C')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            MsgInformation("No Such Account in Account Master")
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
            .set_RowHeight(0, RowHeight * 2.5)
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


            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 8)
            .ColHidden = False

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 8)
            .ColHidden = False

            .Col = ColSupplierCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 6)
            .ColHidden = False

            .Col = ColSupplierName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 15)
            .ColHidden = False

            .Col = ColAccountCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 6)
            .ColHidden = False

            .Col = ColAccountName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 15)
            .ColHidden = False

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 15)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColItemPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            For cntCol = ColQty To ColOthers
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(.Col, 9)
            Next

            .Col = ColDrCr
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 4)

            .Col = colRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 16)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColBookCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 5)
            .ColHidden = True

            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 5)
            .ColHidden = True

            .Col = ColVType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 5)
            .ColHidden = False

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 12)
            .ColHidden = True

            Call FillHeading()

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
        Dim SqlStr As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '''********************************				
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String
        On Error GoTo ERR1


        Dim CntLst As Integer
        Dim mAccountName As String
        Dim mAccountCode As String
        Dim mAccountCodeStr As String
        Dim mShowAll As Boolean
        Dim mChkType As Boolean
        Dim mCompanyGSTNo As String

        mShowAll = True
        mAccountCodeStr = ""

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)

        mChkType = lstInvoiceType.GetItemChecked(0)

        For CntLst = 0 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = Not mChkType Then
                mShowAll = False
                Exit For
            End If
        Next

        If mShowAll = False Then
            For CntLst = 0 To lstInvoiceType.Items.Count - 1
                If lstInvoiceType.GetItemChecked(CntLst) = True Then
                    mAccountName = VB6.GetItemString(lstInvoiceType, CntLst)
                    If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = "'" & IIf(IsDBNull(MasterNo), "", MasterNo) & "'"
                    End If
                    mAccountCodeStr = IIf(mAccountCodeStr = "", mAccountCode, mAccountCodeStr & "," & mAccountCode)
                End If
            Next
        End If

        If optShow(0).Checked = True Then
            MakeSQL = " SELECT  VLOCK, VNO, TO_CHAR(VDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf _
                & " SUPP_CUST_CODE, SUPP_CUST_NAME, ACCOUNT_CODE, ACCOUNT_NAME,  " & vbCrLf _
                & " ITEM_CODE, ITEM_SHORT_DESC, CUSTOMER_PART_NO, QTY, RATE,   " & vbCrLf _
                & " ITEM_AMOUNT, GSTABLE_AMT, GST_REFUND_AMOUNT, CGST_AMOUNT, SGST_AMOUNT,  IGST_AMOUNT, OTHERS_AMOUNT, DRCR, " & vbCrLf _
                & " REMARKS, BOOKCODE, BOOKTYPE,  " & vbCrLf _
                & " VTYPE, MKEY " & vbCrLf _
                & " FROM ("
        Else
            MakeSQL = " SELECT  VLOCK, VNO, TO_CHAR(VDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf _
                & " SUPP_CUST_CODE, SUPP_CUST_NAME, ACCOUNT_CODE, ACCOUNT_NAME,  " & vbCrLf _
                & " '' AS ITEM_CODE, '' AS ITEM_SHORT_DESC, '' As CUSTOMER_PART_NO, SUM(QTY) AS QTY, 0 AS RATE,   " & vbCrLf _
                & " SUM(ITEM_AMOUNT) AS ITEM_AMOUNT, SUM(GSTABLE_AMT) AS GSTABLE_AMT, SUM(GST_REFUND_AMOUNT) AS GST_REFUND_AMOUNT, SUM(CGST_AMOUNT) AS CGST_AMOUNT, SUM(SGST_AMOUNT) AS SGST_AMOUNT, SUM(IGST_AMOUNT) AS IGST_AMOUNT, SUM(OTHERS_AMOUNT) AS OTHERS_AMOUNT, " & vbCrLf & " DRCR, '' AS REMARKS, BOOKCODE, BOOKTYPE,  " & vbCrLf _
                & " VTYPE, MKEY " & vbCrLf _
                & " FROM ("
        End If

        MakeSQL = MakeSQL & vbCrLf & " SELECT '0' AS VLOCK, " & vbCrLf _
            & " '' AS VNO, NULL AS VDATE, " & vbCrLf _
            & " '' AS SUPP_CUST_CODE, '' AS SUPP_CUST_NAME, '' AS ACCOUNT_CODE, '' AS ACCOUNT_NAME,  " & vbCrLf _
            & " '' AS ITEM_CODE, '' AS ITEM_SHORT_DESC,'' AS CUSTOMER_PART_NO, 0 AS QTY, 0 AS RATE,   " & vbCrLf _
            & " 0 AS ITEM_AMOUNT, 0 AS GSTABLE_AMT, 0 AS GST_REFUND_AMOUNT, 0 AS CGST_AMOUNT, 0 AS SGST_AMOUNT,  0 AS IGST_AMOUNT, 0 AS OTHERS_AMOUNT, " & vbCrLf & " '' AS DRCR, '' AS REMARKS, -1 AS BOOKCODE, '' AS BOOKTYPE,  " & vbCrLf & " '' AS VTYPE, '' AS MKEY "

        MakeSQL = MakeSQL & vbCrLf & " FROM DUAL WHERE 1=2 "

        If chkSale.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "
            MakeSQL = MakeSQL & vbCrLf & MakeSQLSale(mShowAll, mAccountCodeStr, "01", "SALE", "DR", mCompanyGSTNo)
        End If

        If chkSaleDN.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "
            MakeSQL = MakeSQL & vbCrLf & MakeSQLSaleDN(mShowAll, mAccountCodeStr, "02", "SALE DN", "CR", mCompanyGSTNo)
        End If

        If chkPurchase.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "
            MakeSQL = MakeSQL & vbCrLf & MakeSQLPur(mShowAll, mAccountCodeStr, "03", "PURCHASE", "CR", mCompanyGSTNo)
        End If

        If chkPurSupp.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "
            MakeSQL = MakeSQL & vbCrLf & MakeSQLPurSupp(mShowAll, mAccountCodeStr, "04", "PURCHASE SUPP", "CR", mCompanyGSTNo)
        End If

        If chkDNCN.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "
            MakeSQL = MakeSQL & vbCrLf & MakeSQLPurDN(mShowAll, mAccountCodeStr, "05", "PURCHASE DEBIT", "DR", mCompanyGSTNo)

            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "
            MakeSQL = MakeSQL & vbCrLf & MakeSQLPurDN(mShowAll, mAccountCodeStr, "06", "PURCHASE CREDIT", "CR", mCompanyGSTNo)
        End If

        If chkJournal.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "
            MakeSQL = MakeSQL & vbCrLf & MakeSQLJournal(mShowAll, mAccountCodeStr, "07", "JOURNAL", "CR", mCompanyGSTNo)

            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "
            MakeSQL = MakeSQL & vbCrLf & MakeSQLJournalDetails(mShowAll, mAccountCodeStr, "07", "JOURNAL", "CR", mCompanyGSTNo)
        End If

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & ") ORDER BY 3,2 "
        Else
            MakeSQL = MakeSQL & vbCrLf & ") GROUP BY VLOCK, VNO, VDATE,SUPP_CUST_CODE, SUPP_CUST_NAME, ACCOUNT_CODE, ACCOUNT_NAME, DRCR, BOOKCODE, BOOKTYPE,VTYPE, MKEY"
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY VDATE,VLOCK "
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLPur(ByRef mShowAll As Boolean, ByRef mAccountCodeStr As String, ByRef pReportNo As String, ByRef pReportName As String, ByRef pDrCrType As String, ByRef mCompanyGSTNo As String) As String
        On Error GoTo ERR1


        MakeSQLPur = " SELECT '" & pReportNo & "' AS VLOCK, " & vbCrLf & " IH.VNO AS VNO, " & vbCrLf & " IH.VDATE AS VDATE, " & vbCrLf & " CMST.SUPP_CUST_CODE AS SUPP_CUST_CODE, CMST.SUPP_CUST_NAME AS SUPP_CUST_NAME, " & vbCrLf & " AMST.SUPP_CUST_CODE AS ACCOUNT_CODE, AMST.SUPP_CUST_NAME AS ACCOUNT_NAME," & vbCrLf & " INVMST.ITEM_CODE AS ITEM_CODE, INVMST.ITEM_SHORT_DESC As ITEM_SHORT_DESC,INVMST.CUSTOMER_PART_NO AS CUSTOMER_PART_NO, ID.ITEM_QTY AS QTY," & vbCrLf & " ID.ITEM_RATE AS RATE, ID.ITEM_AMT AS ITEM_AMOUNT, ID.GSTABLE_AMT AS GSTABLE_AMT," & vbCrLf & " CASE WHEN TOTCGST_REFUNDAMT + TOTSGST_REFUNDAMT + TOTIGST_REFUNDAMT >0 THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END AS GST_REFUND_AMOUNT," & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.CGST_AMOUNT END AS CGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.SGST_AMOUNT END AS SGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.IGST_AMOUNT END AS IGST_AMOUNT, " & vbCrLf & " DECODE(IH.ITEMVALUE,0,0,IH.TOTEXPAMT*ID.ITEM_AMT/IH.ITEMVALUE) AS OTHERS_AMOUNT, '" & pDrCrType & "' AS DRCR," & vbCrLf & " '' AS REMARKS, IH.BOOKCODE AS BOOKCODE, IH.BOOKTYPE AS BOOKTYPE, " & vbCrLf & " '" & pReportName & "' AS VTYPE, " & vbCrLf & " IH.Mkey AS MKEY"



        MakeSQLPur = MakeSQLPur & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST AMST, FIN_INVTYPE_MST IMST, INV_ITEM_MST INVMST" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_TRNTYPE=IMST.CODE " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IMST.COMPANY_CODE=AMST.COMPANY_CODE " & vbCrLf & " AND IMST.ACCOUNTPOSTCODE=AMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.CANCELLED='N'"

        MakeSQLPur = MakeSQLPur & vbCrLf & " AND IH.PURCHASE_TYPE IN ('G','R','J')"

        If mAccountCodeStr <> "" And mShowAll = False Then
            MakeSQLPur = MakeSQLPur & vbCrLf & " AND AMST.SUPP_CUST_CODE IN (" & mAccountCodeStr & ")"
        End If

        MakeSQLPur = MakeSQLPur & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQLPur = MakeSQLPur & vbCrLf & " UNION ALL " & vbCrLf & " SELECT '" & pReportNo & "' AS VLOCK, " & vbCrLf & " IH.VNO AS VNO, " & vbCrLf & " IH.VDATE AS VDATE, " & vbCrLf & " CMST.SUPP_CUST_CODE AS SUPP_CUST_CODE, CMST.SUPP_CUST_NAME AS SUPP_CUST_NAME, " & vbCrLf & " AMST.SUPP_CUST_CODE AS ACCOUNT_CODE, AMST.SUPP_CUST_NAME AS ACCOUNT_NAME," & vbCrLf & " '' AS ITEM_CODE, ID.ITEM_DESC As ITEM_SHORT_DESC, '' AS CUSTOMER_PART_NO,  ID.ITEM_QTY AS QTY," & vbCrLf & " ID.ITEM_RATE AS RATE, ID.ITEM_AMT AS ITEM_AMOUNT, ID.GSTABLE_AMT AS GSTABLE_AMT," & vbCrLf & " CASE WHEN GST_CLAIM='Y' AND GST_CREDITAPP='Y' AND GST_RCAPP='N' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END AS GST_REFUND_AMOUNT," & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.CGST_AMOUNT END AS CGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.SGST_AMOUNT END AS SGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.IGST_AMOUNT END AS IGST_AMOUNT, " & vbCrLf & " DECODE(IH.ITEMVALUE,0,0,IH.TOTEXPAMT*ID.ITEM_AMT/IH.ITEMVALUE) AS OTHERS_AMOUNT, '" & pDrCrType & "' AS DRCR," & vbCrLf & " '' AS REMARKS, IH.BOOKCODE AS BOOKCODE, IH.BOOKTYPE AS BOOKTYPE, " & vbCrLf & " '" & pReportName & "' AS VTYPE, " & vbCrLf & " IH.Mkey AS MKEY"


        MakeSQLPur = MakeSQLPur & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST AMST, FIN_INVTYPE_MST IMST" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_TRNTYPE=IMST.CODE " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IMST.COMPANY_CODE=AMST.COMPANY_CODE " & vbCrLf & " AND IMST.ACCOUNTPOSTCODE=AMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.CANCELLED='N'"

        MakeSQLPur = MakeSQLPur & vbCrLf & " AND IH.PURCHASE_TYPE NOT IN ('G','R','J')"

        If mAccountCodeStr <> "" And mShowAll = False Then
            MakeSQLPur = MakeSQLPur & vbCrLf & " AND AMST.SUPP_CUST_CODE IN (" & mAccountCodeStr & ")"
        End If

        MakeSQLPur = MakeSQLPur & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLJournal(ByRef mShowAll As Boolean, ByRef mAccountCodeStr As String, ByRef pReportNo As String, ByRef pReportName As String, ByRef pDrCrType As String, ByRef mCompanyGSTNo As String) As String
        On Error GoTo ERR1


        MakeSQLJournal = " SELECT '" & pReportNo & "' AS VLOCK, " & vbCrLf & " IH.VNO AS VNO, " & vbCrLf & " IH.VDATE AS VDATE, " & vbCrLf & " '' AS SUPP_CUST_CODE, '' AS SUPP_CUST_NAME, " & vbCrLf & " CMST.SUPP_CUST_CODE AS ACCOUNT_CODE, CMST.SUPP_CUST_NAME AS ACCOUNT_NAME," & vbCrLf & " '' AS ITEM_CODE, '' As ITEM_SHORT_DESC, '' AS CUSTOMER_PART_NO, 0 AS QTY," & vbCrLf & " 0 AS RATE, IH.AMOUNT AS ITEM_AMOUNT, IH.AMOUNT AS GSTABLE_AMT," & vbCrLf & " 0 AS GST_REFUND_AMOUNT," & vbCrLf & " 0 AS CGST_AMOUNT, 0 AS SGST_AMOUNT, 0 AS IGST_AMOUNT, " & vbCrLf & " 0 AS OTHERS_AMOUNT, DECODE(IH.DC,'D','CR','DR') AS DRCR," & vbCrLf & " '' AS REMARKS, -1 AS BOOKCODE, IH.BOOKTYPE AS BOOKTYPE, " & vbCrLf & " '" & pReportName & "' AS VTYPE, " & vbCrLf & " IH.Mkey AS MKEY"



        MakeSQLJournal = MakeSQLJournal & vbCrLf & " FROM FIN_POSTED_TRN IH, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.ACCOUNTCODE=CMST.SUPP_CUST_CODE " ''& vbCrLf |            & " AND IH.CANCELLED='N'"				

        MakeSQLJournal = MakeSQLJournal & vbCrLf & " AND IH.BOOKTYPE IN ('B','J','C','F')"

        If mAccountCodeStr <> "" And mShowAll = False Then
            MakeSQLJournal = MakeSQLJournal & vbCrLf & " AND CMST.SUPP_CUST_CODE IN (" & mAccountCodeStr & ")"
        End If

        MakeSQLJournal = MakeSQLJournal & vbCrLf & " AND IH.ACCOUNTCODE||IH.MKEY NOT IN (" & vbCrLf & " SELECT ACCOUNTCODE||MKEY FROM FIN_JOURNAL_ITEM_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"

        MakeSQLJournal = MakeSQLJournal & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLJournalDetails(ByRef mShowAll As Boolean, ByRef mAccountCodeStr As String, ByRef pReportNo As String, ByRef pReportName As String, ByRef pDrCrType As String, ByRef mCompanyGSTNo As String) As String
        On Error GoTo ERR1

        MakeSQLJournalDetails = " SELECT '" & pReportNo & "' AS VLOCK, " & vbCrLf & " IH.VNO AS VNO, " & vbCrLf & " IH.VDATE AS VDATE, " & vbCrLf & " TRN.SUPP_CUST_CODE AS SUPP_CUST_CODE, SMST.SUPP_CUST_NAME AS SUPP_CUST_NAME, " & vbCrLf & " CMST.SUPP_CUST_CODE AS ACCOUNT_CODE, CMST.SUPP_CUST_NAME AS ACCOUNT_NAME," & vbCrLf & " IMST.ITEM_CODE AS ITEM_CODE, IMST.ITEM_SHORT_DESC As ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO AS CUSTOMER_PART_NO, TRN.ITEM_QTY AS QTY," & vbCrLf & " TRN.ITEM_RATE AS RATE, TRN.ITEM_AMT AS ITEM_AMOUNT, TRN.ITEM_AMT AS GSTABLE_AMT," & vbCrLf & " TRN.CGST_AMOUNT + TRN.SGST_AMOUNT + TRN.IGST_AMOUNT AS GST_REFUND_AMOUNT," & vbCrLf & " CASE WHEN SMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE TRN.CGST_AMOUNT END AS CGST_AMOUNT, " & vbCrLf & " CASE WHEN SMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE TRN.SGST_AMOUNT END AS SGST_AMOUNT, " & vbCrLf & " CASE WHEN SMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE TRN.IGST_AMOUNT END AS IGST_AMOUNT, " & vbCrLf & " 0 AS OTHERS_AMOUNT, DECODE(IH.DC,'D','CR','DR') AS DRCR," & vbCrLf & " TRN.PARTICULARS AS REMARKS, -1 AS BOOKCODE, IH.BOOKTYPE AS BOOKTYPE, " & vbCrLf & " '" & pReportName & "' AS VTYPE, " & vbCrLf & " IH.Mkey AS MKEY"



        MakeSQLJournalDetails = MakeSQLJournalDetails & vbCrLf & " FROM FIN_POSTED_TRN IH, FIN_JOURNAL_ITEM_TRN TRN, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST SMST, INV_ITEM_MST IMST " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=TRN.COMPANY_CODE " & vbCrLf & " AND IH.MKEY=TRN.MKEY " & vbCrLf & " AND IH.ACCOUNTCODE=TRN.ACCOUNTCODE " & vbCrLf & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND TRN.ACCOUNTCODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND TRN.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND TRN.SUPP_CUST_CODE=SMST.SUPP_CUST_CODE " & vbCrLf & " AND TRN.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf & " AND TRN.ITEM_CODE=IMST.ITEM_CODE "


        MakeSQLJournalDetails = MakeSQLJournalDetails & vbCrLf & " AND IH.BOOKTYPE IN ('B','J','C','F')"

        If mAccountCodeStr <> "" And mShowAll = False Then
            MakeSQLJournalDetails = MakeSQLJournalDetails & vbCrLf & " AND CMST.SUPP_CUST_CODE IN (" & mAccountCodeStr & ")"
        End If

        '    MakeSQLJournalDetails = MakeSQLJournalDetails & vbCrLf & " AND IH.ACCOUNTCODE||IH.MKEY IN (" & vbCrLf _				
        ''            & " SELECT ACCOUNTCODE||MKEY FROM FIN_JOURNAL_ITEM_TRN" & vbCrLf _				
        ''            & " WHERE COMPANY_CODE=" & RsCompany!COMPANY_CODE & ")"				

        MakeSQLJournalDetails = MakeSQLJournalDetails & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLPurDN(ByRef mShowAll As Boolean, ByRef mAccountCodeStr As String, ByRef pReportNo As String, ByRef pReportName As String, ByRef pDrCrType As String, ByRef mCompanyGSTNo As String) As String
        On Error GoTo ERR1


        MakeSQLPurDN = " SELECT '" & pReportNo & "' AS VLOCK, " & vbCrLf & " IH.VNO AS VNO, " & vbCrLf & " IH.VDATE AS VDATE, " & vbCrLf & " CMST.SUPP_CUST_CODE AS SUPP_CUST_CODE, CMST.SUPP_CUST_NAME AS SUPP_CUST_NAME, " & vbCrLf & " AMST.SUPP_CUST_CODE AS ACCOUNT_CODE, AMST.SUPP_CUST_NAME AS ACCOUNT_NAME," & vbCrLf & " INVMST.ITEM_CODE AS ITEM_CODE, INVMST.ITEM_SHORT_DESC As ITEM_SHORT_DESC,INVMST.CUSTOMER_PART_NO AS CUSTOMER_PART_NO, ID.ITEM_QTY AS QTY," & vbCrLf & " ID.ITEM_RATE AS RATE, ID.ITEM_AMT AS ITEM_AMOUNT, ID.ITEM_AMT AS GSTABLE_AMT," & vbCrLf & " CASE WHEN CGST_REFUNDAMOUNT + SGST_REFUNDAMOUNT + IGST_REFUNDAMOUNT >0 THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END AS GST_REFUND_AMOUNT," & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.CGST_AMOUNT END AS CGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.SGST_AMOUNT END AS SGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.IGST_AMOUNT END AS IGST_AMOUNT, " & vbCrLf & " DECODE(IH.ITEMVALUE,0,0,IH.TOTEXPAMT*ID.ITEM_AMT/IH.ITEMVALUE) AS OTHERS_AMOUNT, '" & pDrCrType & "' AS DRCR," & vbCrLf & " CASE WHEN DNCNTYPE='P' OR DNCNTYPE='A' THEN 'PO RATE DIFF.'  " & vbCrLf & " WHEN DNCNTYPE='S' THEN 'SHORTAGE'  " & vbCrLf & " WHEN DNCNTYPE='R' THEN 'REJECTION'  " & vbCrLf & " WHEN DNCNTYPE='D' THEN 'DISCOUNT'  " & vbCrLf & " WHEN DNCNTYPE='V' THEN 'VOLUME DISCOUNT'  " & vbCrLf & " WHEN DNCNTYPE='O' THEN 'OTHERS' END AS REMARKS, " & vbCrLf & " IH.BOOKCODE AS BOOKCODE, IH.BOOKTYPE AS BOOKTYPE, " & vbCrLf & " '" & pReportName & "' AS VTYPE, " & vbCrLf & " IH.Mkey AS MKEY"



        MakeSQLPurDN = MakeSQLPurDN & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST AMST, INV_ITEM_MST INVMST" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND DECODE(BOOKCODE," & ConDebitNoteBookCode & ",IH.DEBITACCOUNTCODE,IH.CREDITACCOUNTCODE)=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=AMST.COMPANY_CODE " & vbCrLf & " AND DECODE(BOOKCODE," & ConDebitNoteBookCode & ",IH.CREDITACCOUNTCODE,IH.DEBITACCOUNTCODE)=AMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE(+) " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE(+) " & vbCrLf & " AND IH.CANCELLED='N' AND IH.APPROVED='Y'"

        If pDrCrType = "DR" Then
            MakeSQLPurDN = MakeSQLPurDN & vbCrLf & " AND IH.BOOKCODE = " & ConDebitNoteBookCode & ""
        Else
            MakeSQLPurDN = MakeSQLPurDN & vbCrLf & " AND IH.BOOKCODE = " & ConCreditNoteBookCode & ""
        End If

        If mAccountCodeStr <> "" And mShowAll = False Then
            MakeSQLPurDN = MakeSQLPurDN & vbCrLf & " AND AMST.SUPP_CUST_CODE IN (" & mAccountCodeStr & ")"
        End If

        MakeSQLPurDN = MakeSQLPurDN & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLPurSupp(ByRef mShowAll As Boolean, ByRef mAccountCodeStr As String, ByRef pReportNo As String, ByRef pReportName As String, ByRef pDrCrType As String, ByRef mCompanyGSTNo As String) As String
        On Error GoTo ERR1


        MakeSQLPurSupp = " SELECT '" & pReportNo & "' AS VLOCK, " & vbCrLf & " IH.VNO AS VNO, " & vbCrLf & " IH.VDATE AS VDATE, " & vbCrLf & " CMST.SUPP_CUST_CODE AS SUPP_CUST_CODE, CMST.SUPP_CUST_NAME AS SUPP_CUST_NAME, " & vbCrLf & " AMST.SUPP_CUST_CODE AS ACCOUNT_CODE, AMST.SUPP_CUST_NAME AS ACCOUNT_NAME," & vbCrLf & " INVMST.ITEM_CODE AS ITEM_CODE, INVMST.ITEM_SHORT_DESC As ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO AS CUSTOMER_PART_NO, ID.QTY AS QTY," & vbCrLf & " ID.RATE AS RATE, ID.AMOUNT AS ITEM_AMOUNT, ID.AMOUNT AS GSTABLE_AMT," & vbCrLf & " CASE WHEN TOTCGST_REFUNDAMT + TOTSGST_REFUNDAMT + TOTIGST_REFUNDAMT >0 THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END AS GST_REFUND_AMOUNT," & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.CGST_AMOUNT END AS CGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.SGST_AMOUNT END AS SGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.IGST_AMOUNT END AS IGST_AMOUNT, " & vbCrLf & " DECODE(IH.ITEMVALUE,0,0,IH.TOTEXPAMT*ID.AMOUNT/IH.ITEMVALUE) AS OTHERS_AMOUNT, '" & pDrCrType & "' AS DRCR," & vbCrLf & " 'PO RATE DIFF.' AS REMARKS, -1 AS BOOKCODE, IH.BOOKTYPE AS BOOKTYPE, " & vbCrLf & " '" & pReportName & "' AS VTYPE, " & vbCrLf & " IH.Mkey AS MKEY"



        MakeSQLPurSupp = MakeSQLPurSupp & vbCrLf & " FROM FIN_SUPP_PURCHASE_HDR IH, FIN_SUPP_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST AMST, FIN_INVTYPE_MST IMST, INV_ITEM_MST INVMST" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_TRNTYPE=IMST.CODE " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=AMST.COMPANY_CODE " & vbCrLf & " AND ID.PUR_ACCOUNT_CODE=AMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.CANCELLED='N'"

        If mAccountCodeStr <> "" And mShowAll = False Then
            MakeSQLPurSupp = MakeSQLPurSupp & vbCrLf & " AND AMST.SUPP_CUST_CODE IN (" & mAccountCodeStr & ")"
        End If

        MakeSQLPurSupp = MakeSQLPurSupp & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLSale(ByRef mShowAll As Boolean, ByRef mAccountCodeStr As String, ByRef pReportNo As String, ByRef pReportName As String, ByRef pDrCrType As String, ByRef mCompanyGSTNo As String) As String
        On Error GoTo ERR1


        MakeSQLSale = " SELECT '" & pReportNo & "' AS VLOCK, " & vbCrLf & " IH.BILLNO AS VNO, " & vbCrLf & " IH.INVOICE_DATE AS VDATE, " & vbCrLf & " CMST.SUPP_CUST_CODE AS SUPP_CUST_CODE, CMST.SUPP_CUST_NAME AS SUPP_CUST_NAME, " & vbCrLf & " AMST.SUPP_CUST_CODE AS ACCOUNT_CODE, AMST.SUPP_CUST_NAME AS ACCOUNT_NAME," & vbCrLf & " INVMST.ITEM_CODE AS ITEM_CODE, INVMST.ITEM_SHORT_DESC As ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO AS CUSTOMER_PART_NO, ID.ITEM_QTY AS QTY," & vbCrLf & " ID.ITEM_RATE AS RATE, ID.ITEM_AMT AS ITEM_AMOUNT, ID.GSTABLE_AMT AS GSTABLE_AMT, " & vbCrLf & " ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT AS GST_REFUND_AMOUNT," & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.CGST_AMOUNT END AS CGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.SGST_AMOUNT END AS SGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.IGST_AMOUNT END AS IGST_AMOUNT, " & vbCrLf & " DECODE(IH.ITEMVALUE,0,0,IH.TOTEXPAMT*ID.ITEM_AMT/IH.ITEMVALUE) AS OTHERS_AMOUNT, '" & pDrCrType & "' AS DRCR," & vbCrLf & " '' AS REMARKS, IH.BOOKCODE AS BOOKCODE, IH.BOOKTYPE AS BOOKTYPE, " & vbCrLf & " '" & pReportName & "' AS VTYPE, " & vbCrLf & " IH.Mkey AS MKEY"



        MakeSQLSale = MakeSQLSale & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST AMST, FIN_INVTYPE_MST IMST, INV_ITEM_MST INVMST" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf & " AND IH.TRNTYPE=IMST.CODE " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IMST.COMPANY_CODE=AMST.COMPANY_CODE " & vbCrLf & " AND IMST.ACCOUNTPOSTCODE=AMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.CANCELLED='N'"

        MakeSQLSale = MakeSQLSale & vbCrLf & " AND IH.INVOICESEQTYPE IN (1,2,3,5,6,9) AND IH.REF_DESP_TYPE NOT IN ('Q','L')"

        If mAccountCodeStr <> "" And mShowAll = False Then
            MakeSQLSale = MakeSQLSale & vbCrLf & " AND AMST.SUPP_CUST_CODE IN (" & mAccountCodeStr & ")"
        End If

        MakeSQLSale = MakeSQLSale & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQLSale = MakeSQLSale & vbCrLf & " UNION ALL " & vbCrLf _
            & " SELECT '" & pReportNo & "' AS VLOCK, " & vbCrLf & " IH.BILLNO AS VNO, " & vbCrLf & " IH.INVOICE_DATE AS VDATE, " & vbCrLf & " CMST.SUPP_CUST_CODE AS SUPP_CUST_CODE, CMST.SUPP_CUST_NAME AS SUPP_CUST_NAME, " & vbCrLf & " AMST.SUPP_CUST_CODE AS ACCOUNT_CODE, AMST.SUPP_CUST_NAME AS ACCOUNT_NAME," & vbCrLf & " '' AS ITEM_CODE, IH.REMARKS As ITEM_SHORT_DESC, '' AS CUSTOMER_PART_NO, IH.TOTQTY AS QTY," & vbCrLf & " IH.ITEMVALUE AS RATE, IH.ITEMVALUE AS ITEM_AMOUNT, IH.TOTTAXABLEAMOUNT AS GSTABLE_AMT, " & vbCrLf & " IH.NETCGST_AMOUNT + IH.NETSGST_AMOUNT + IH.NETIGST_AMOUNT AS GST_REFUND_AMOUNT," & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE IH.NETCGST_AMOUNT END AS CGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE IH.NETSGST_AMOUNT END AS SGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE IH.NETIGST_AMOUNT END AS IGST_AMOUNT, " & vbCrLf & " IH.TOTEXPAMT AS OTHERS_AMOUNT, '" & pDrCrType & "' AS DRCR," & vbCrLf & " '' AS REMARKS, IH.BOOKCODE AS BOOKCODE, IH.BOOKTYPE AS BOOKTYPE, " & vbCrLf & " '" & pReportName & "' AS VTYPE, " & vbCrLf & " IH.Mkey AS MKEY"



        MakeSQLSale = MakeSQLSale & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST AMST, FIN_INVTYPE_MST IMST" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf & " AND IH.TRNTYPE=IMST.CODE " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IMST.COMPANY_CODE=AMST.COMPANY_CODE " & vbCrLf & " AND IMST.ACCOUNTPOSTCODE=AMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.CANCELLED='N'"

        MakeSQLSale = MakeSQLSale & vbCrLf & " AND IH.INVOICESEQTYPE IN (0,4)"

        If mAccountCodeStr <> "" And mShowAll = False Then
            MakeSQLSale = MakeSQLSale & vbCrLf & " AND AMST.SUPP_CUST_CODE IN (" & mAccountCodeStr & ")"
        End If

        MakeSQLSale = MakeSQLSale & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLSaleDN(ByRef mShowAll As Boolean, ByRef mAccountCodeStr As String, ByRef pReportNo As String, ByRef pReportName As String, ByRef pDrCrType As String, ByRef mCompanyGSTNo As String) As String
        On Error GoTo ERR1


        MakeSQLSaleDN = " SELECT '" & pReportNo & "' AS VLOCK, " & vbCrLf & " IH.VNO AS VNO, " & vbCrLf & " IH.VDATE AS VDATE, " & vbCrLf & " CMST.SUPP_CUST_CODE AS SUPP_CUST_CODE, CMST.SUPP_CUST_NAME AS SUPP_CUST_NAME, " & vbCrLf & " AMST.SUPP_CUST_CODE AS ACCOUNT_CODE, AMST.SUPP_CUST_NAME AS ACCOUNT_NAME," & vbCrLf & " INVMST.ITEM_CODE AS ITEM_CODE, INVMST.ITEM_SHORT_DESC As ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO AS CUSTOMER_PART_NO, ID.QTY AS QTY," & vbCrLf & " ID.RATE AS RATE, ID.AMOUNT AS ITEM_AMOUNT, ID.AMOUNT AS GSTABLE_AMT, " & vbCrLf & " ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT As GST_REFUND_AMOUNT," & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.CGST_AMOUNT END AS CGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.SGST_AMOUNT END AS SGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE ID.IGST_AMOUNT END AS IGST_AMOUNT, " & vbCrLf & " DECODE(IH.ITEMVALUE,0,0,IH.TOTEXPAMT*ID.AMOUNT/IH.ITEMVALUE) AS OTHERS_AMOUNT, '" & pDrCrType & "' AS DRCR," & vbCrLf & " CASE WHEN REASON='1' THEN 'PO RATE DIFF.'  " & vbCrLf & " WHEN REASON='2' THEN 'SHORTAGE'  " & vbCrLf & " WHEN REASON='3' THEN 'OTHERS' END AS REMARKS, " & vbCrLf & " -21 AS BOOKCODE, IH.BOOKTYPE AS BOOKTYPE, " & vbCrLf & " '" & pReportName & "' AS VTYPE, " & vbCrLf & " IH.Mkey AS MKEY"



        MakeSQLSaleDN = MakeSQLSaleDN & vbCrLf & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_SALE_DET ID, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST AMST, FIN_INVTYPE_MST IMST, INV_ITEM_MST INVMST" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf & " AND IH.TRNTYPE=IMST.CODE " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IMST.COMPANY_CODE=AMST.COMPANY_CODE " & vbCrLf & " AND IMST.ACCOUNTPOSTCODE=AMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.CANCELLED='N'"

        MakeSQLSaleDN = MakeSQLSaleDN & vbCrLf & " AND GOODS_SERVICE='G' AND IH.ISFINALPOST='Y'"

        If mAccountCodeStr <> "" And mShowAll = False Then
            MakeSQLSaleDN = MakeSQLSaleDN & vbCrLf & " AND AMST.SUPP_CUST_CODE IN (" & mAccountCodeStr & ")"
        End If

        MakeSQLSaleDN = MakeSQLSaleDN & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQLSaleDN = MakeSQLSaleDN & vbCrLf & " UNION ALL " & vbCrLf & " SELECT '" & pReportNo & "' AS VLOCK, " & vbCrLf & " IH.VNO AS VNO, " & vbCrLf & " IH.VDATE AS VDATE, " & vbCrLf & " CMST.SUPP_CUST_CODE AS SUPP_CUST_CODE, CMST.SUPP_CUST_NAME AS SUPP_CUST_NAME, " & vbCrLf & " AMST.SUPP_CUST_CODE AS ACCOUNT_CODE, AMST.SUPP_CUST_NAME AS ACCOUNT_NAME," & vbCrLf & " '' AS ITEM_CODE, IH.REMARKS As ITEM_SHORT_DESC, '' AS CUSTOMER_PART_NO, IH.TOTQTY AS QTY," & vbCrLf & " IH.ITEMVALUE AS RATE, IH.ITEMVALUE AS ITEM_AMOUNT, IH.TOTTAXABLEAMOUNT AS GSTABLE_AMT, " & vbCrLf & " IH.TOTCGST_AMOUNT + IH.TOTSGST_AMOUNT + IH.TOTIGST_AMOUNT As GST_REFUND_AMOUNT," & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE IH.TOTCGST_AMOUNT END AS CGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE IH.TOTSGST_AMOUNT END AS SGST_AMOUNT, " & vbCrLf & " CASE WHEN CMST.GST_RGN_NO = '" & mCompanyGSTNo & "' THEN 0 ELSE IH.TOTIGST_AMOUNT END AS IGST_AMOUNT, " & vbCrLf & " IH.TOTEXPAMT AS OTHERS_AMOUNT, '" & pDrCrType & "' AS DRCR," & vbCrLf & " '' AS REMARKS, -21 AS BOOKCODE, IH.BOOKTYPE AS BOOKTYPE, " & vbCrLf & " '" & pReportName & "' AS VTYPE, " & vbCrLf & " IH.Mkey AS MKEY"



        MakeSQLSaleDN = MakeSQLSaleDN & vbCrLf & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST AMST, FIN_INVTYPE_MST IMST" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf & " AND IH.TRNTYPE=IMST.CODE " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IMST.COMPANY_CODE=AMST.COMPANY_CODE " & vbCrLf & " AND IMST.ACCOUNTPOSTCODE=AMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.CANCELLED='N'"

        MakeSQLSaleDN = MakeSQLSaleDN & vbCrLf & " AND GOODS_SERVICE='S' AND IH.ISFINALPOST='Y'"

        If mAccountCodeStr <> "" And mShowAll = False Then
            MakeSQLSaleDN = MakeSQLSaleDN & vbCrLf & " AND AMST.SUPP_CUST_CODE IN (" & mAccountCodeStr & ")"
        End If

        MakeSQLSaleDN = MakeSQLSaleDN & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcSprdTotal()
        On Error GoTo ErrPart
        Dim CntRow As Integer
        Dim mCGST As Double
        Dim mSGST As Double
        Dim mIGST As Double
        Dim mAmount As Double
        Dim mOthers As Double
        Dim mTaxableAmount As Double
        Dim mGSTRefundAmount As Double
        Dim mDC As String

        With SprdMain

            mCGST = 0
            mSGST = 0
            mIGST = 0
            mAmount = 0
            mOthers = 0
            mTaxableAmount = 0

            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColDrCr
                mDC = VB.Left(.Text, 1)

                .Col = ColAmount
                mAmount = mAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0)) * IIf(mDC = "D", 1, -1)))

                .Col = ColTaxableAmount
                mTaxableAmount = mTaxableAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0)) * IIf(mDC = "D", 1, -1)))

                .Col = ColGSTRefundAmount
                mGSTRefundAmount = mGSTRefundAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0)) * IIf(mDC = "D", 1, -1)))

                .Col = ColCGST
                mCGST = mCGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0)) * IIf(mDC = "D", 1, -1)))

                .Col = ColSGST
                mSGST = mSGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0)) * IIf(mDC = "D", 1, -1)))

                .Col = ColIGST
                mIGST = mIGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0)) * IIf(mDC = "D", 1, -1)))

                .Col = ColOthers
                mOthers = mOthers + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0)) * IIf(mDC = "D", 1, -1)))

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColVNo)
            .Row = .MaxRows
            .Col = ColAccountName
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColAmount
            .Text = VB6.Format(mAmount, "0.00")

            .Col = ColTaxableAmount
            .Text = VB6.Format(mTaxableAmount, "0.00")

            .Col = ColGSTRefundAmount
            .Text = VB6.Format(mGSTRefundAmount, "0.00")

            .Col = ColCGST
            .Text = VB6.Format(mCGST, "0.00")

            .Col = ColSGST
            .Text = VB6.Format(mSGST, "0.00")

            .Col = ColIGST
            .Text = VB6.Format(mIGST, "0.00")

            .Col = ColOthers
            .Text = VB6.Format(mOthers, "0.00")

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80				
            .Font = VB6.FontChangeBold(.Font, True)
            .BlockMode = False





        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FillHeading()

        With SprdMain
            .Row = 0

            .Col = ColLocked
            .Text = "Locked"

            .Col = ColVNo
            .Text = "VNo."

            .Col = ColVDate
            .Text = "VDate"

            .Col = ColSupplierCode
            .Text = "Supplier Code"

            .Col = ColSupplierName
            .Text = "Supplier Name"

            .Col = ColAccountCode
            .Text = "Account Code"

            .Col = ColAccountName
            .Text = "Account Name"

            .Col = ColItemCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Name"

            .Col = ColItemPartNo
            .Text = "Item Part No"

            .Col = ColQty
            .Text = "Quantity"

            .Col = ColRate
            .Text = "Rate"


            .Col = ColAmount
            .Text = "Amount"

            .Col = ColTaxableAmount
            .Text = "Taxable Amount"

            .Col = ColGSTRefundAmount
            .Text = "GST Refund Amount"

            .Col = ColCGST
            .Text = "CGST Amount"

            .Col = ColSGST
            .Text = "SGST Amount"

            .Col = ColIGST
            .Text = "IGST Amount"

            .Col = ColOthers
            .Text = "Other Amount"

            .Col = ColDrCr
            .Text = "Dr/Cr"

            .Col = colRemarks
            .Text = "Remarks"

            .Col = ColBookCode
            .Text = "Book Code"

            .Col = ColBookType
            .Text = "Book Type"

            .Col = ColVType
            .Text = "Voucher Type"

            .Col = ColMKEY
            .Text = "Mkey"

        End With

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



    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String

        PubDBCn.Errors.Clear()


        'If TxtName.Text = "" Then Exit Sub				

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        Call InsertPrintDummy()


        '''''Select Record for print...				

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "All Voucher Register"
        mSubTitle = ""

        mRPTName = "AllVoucherReport.Rpt"

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        ''Resume				
    End Sub
    Private Sub InsertPrintDummy()

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim CntRow As Integer
        Dim mColLocked As String
        'Dim mColSNO As String				
        'Dim mColSDate As String				
        'Dim mCol3A As String				
        'Dim mCol3B As String				
        'Dim mCol3C As String				
        'Dim mCol4A As String				
        'Dim mCol4B As String				
        'Dim mCol5 As String				
        'Dim mCol6A As String				
        'Dim mCol6B As String				
        'Dim mCol6C As String				
        'Dim mCol6D As String				
        'Dim mCol7A As String				
        'Dim mCol7B As String				
        'Dim mCol8A As String				
        'Dim mCol8B As String				
        'Dim mCol8C As String				
        'Dim mCol9A As String				
        'Dim mCol9B As String				
        'Dim mCol9C As String				
        'Dim mCol10 As String				
        '				
        'Dim mCol3AA As String				
        'Dim mCol8AA As String				
        'Dim mCol9AA As String				
        '				
        '    PubDBCn.Errors.Clear				
        '				
        '    PubDBCn.BeginTrans				
        '				
        '    SqlStr = ""				
        '    With SprdMain				
        '        For cntRow = 1 To .MaxRows - 1				
        '            .Row = cntRow				
        '				
        '            .Col = ColSNO				
        '            mColSNO = Trim(.Text)				
        '            .Col = ColSDate				
        '            mColSDate = Trim(.Text)				
        '            .Col = Col3A				
        '            mCol3A = Trim(.Text)				
        '            .Col = Col3B				
        '            mCol3B = Trim(.Text)				
        '            .Col = Col3C				
        '            mCol3C = Trim(.Text)				
        '				
        '            .Col = Col4A				
        '            mCol4A = Trim(.Text)				
        '            .Col = Col4B				
        '            mCol4B = MainClass.AllowSingleQuote(Trim(.Text))				
        '            .Col = Col5				
        '            mCol5 = MainClass.AllowSingleQuote(Trim(.Text))				
        '				
        '            .Col = Col6A				
        '            mCol6A = Trim(.Text)				
        '            .Col = Col6B				
        '            mCol6B = Trim(.Text)				
        '            .Col = Col6C				
        '            mCol6C = Trim(.Text)				
        '            .Col = Col6D				
        '            mCol6D = Trim(.Text)				
        '				
        '            .Col = Col7A				
        '            mCol7A = Trim(.Text)				
        '            .Col = Col7B				
        '            mCol7B = Trim(.Text)				
        '            .Col = Col8A				
        '            mCol8A = Trim(.Text)				
        '            .Col = Col8B				
        '            mCol8B = Trim(.Text)				
        '            .Col = Col8C				
        '            mCol8C = Trim(.Text)				
        '            .Col = Col9A				
        '            mCol9A = Trim(.Text)				
        '            .Col = Col9B				
        '            mCol9B = Trim(.Text)				
        '            .Col = Col9C				
        '            mCol9C = Trim(.Text)				
        '            .Col = Col10				
        '            mCol10 = Trim(.Text)				
        '				
        '             .Col = Col3AA				
        '            mCol3AA = Trim(.Text)				
        '				
        '             .Col = Col8AA				
        '            mCol8AA = Trim(.Text)				
        '				
        '             .Col = Col9AA				
        '            mCol9AA = Trim(.Text)				
        '				
        '				
        '            SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow," & vbCrLf _				
        ''                & " Field1,Field2,Field3,Field4,Field5, " & vbCrLf _				
        ''                & " Field6,Field7,Field8,Field9,Field10 ," & vbCrLf _				
        ''                & " Field11,Field12,Field13,Field14,Field15, " & vbCrLf _				
        ''                & " Field16,Field17,Field18,Field19,Field20, " & vbCrLf _				
        ''                & " Field21,Field22,Field23,Field24 " & vbCrLf _				
        ''                & " ) Values ("				
        '				
        '            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _				
        ''                & " " & cntRow & ", " & vbCrLf _				
        ''                & " '" & mColSNO & "', " & vbCrLf _				
        ''                & " '" & mColSDate & "', " & vbCrLf _				
        ''                & " '" & mCol3A & "', " & vbCrLf _				
        ''                & " '" & mCol3B & "', " & vbCrLf _				
        ''                & " '" & mCol3C & "', " & vbCrLf _				
        ''                & " '" & mCol4A & "', " & vbCrLf _				
        ''                & " '" & mCol4B & "', " & vbCrLf _				
        ''                & " '" & mCol5 & "', " & vbCrLf _				
        ''                & " '" & mCol6A & "', " & vbCrLf _				
        ''                & " '" & mCol6B & "', " & vbCrLf _				
        ''                & " '" & mCol6C & "', " & vbCrLf _				
        ''                & " '" & mCol6D & "', " & vbCrLf _				
        ''                & " '" & mCol7A & "', " & vbCrLf _				
        ''                & " '" & mCol7B & "', " & vbCrLf _				
        ''                & " '" & mCol8A & "', " & vbCrLf _				
        ''                & " '" & mCol8B & "', " & vbCrLf _				
        ''                & " '" & mCol8C & "', " & vbCrLf _				
        ''                & " '" & mCol9A & "', " & vbCrLf _				
        ''                & " '" & mCol9B & "', " & vbCrLf _				
        ''                & " '" & mCol9C & "', " & vbCrLf _				
        ''                & " '" & MainClass.AllowSingleQuote(mCol10) & "','" & mCol3AA & "','" & mCol8AA & "','" & mCol9AA & "') "				
        '				
        '            PubDBCn.Execute SqlStr				
        '        Next				
        '				
        '    End With				
        '    PubDBCn.CommitTrans				
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim mMonth As String
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle,  ,  , "Y")

        If VB6.Format(txtDateTo.Text, "MMMM, YYYY") = VB6.Format(txtDateFrom.Text, "MMMM, YYYY") Then
            mMonth = "Month : " & VB6.Format(txtDateTo.Text, "MMMM, YYYY")
        Else
            mMonth = "Month : FROM " & VB6.Format(txtDateFrom.Text, "MMMM, YYYY") & " To " & VB6.Format(txtDateTo.Text, "MMMM, YYYY")
        End If

        MainClass.AssignCRptFormulas(Report1, "MonthTitle=""" & mMonth & """")

        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
End Class
