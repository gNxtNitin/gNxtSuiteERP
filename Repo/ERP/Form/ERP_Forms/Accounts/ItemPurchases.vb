Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmItemPurchases
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColVNo As Short = 2
    Private Const ColVDate As Short = 3
    Private Const ColCreditNoteNo As Short = 4
    Private Const ColCustomerRefNo As Short = 5
    Private Const ColHSNCode As Short = 6
    Private Const ColItemCode As Short = 7
    Private Const ColItemName As Short = 8
    Private Const ColItemPartNo As Short = 9
    Private Const ColUOM As Short = 10
    Private Const ColMRRNo As Short = 11
    Private Const ColMRRDate As Short = 12
    Private Const ColBillNo As Short = 13
    Private Const ColBillDate As Short = 14
    Private Const ColPartyCode As Short = 15
    Private Const ColPartyName As Short = 16
    Private Const ColQuantity As Short = 17
    Private Const ColRate As Short = 18
    Private Const ColAmount As Short = 19
    Private Const ColModvatAmount As Short = 20
    Private Const ColSTRefund As Short = 21
    Private Const ColCGSTRefundAmount As Short = 22
    Private Const ColCGST As Short = 23
    Private Const ColSSTRefundAmount As Short = 24
    Private Const ColSGST As Short = 25
    Private Const ColISTRefundAmount As Short = 26
    Private Const ColIGST As Short = 27
    Private Const ColGSTRefundAmount As Short = 28
    Private Const ColGST As Short = 29
    Private Const ColExciseDuty As Short = 30
    Private Const ColCess As Short = 31
    Private Const ColSHCess As Short = 32
    Private Const ColVAT As Short = 33
    Private Const ColCST As Short = 34
    Private Const ColOthers As Short = 35
    Private Const ColDiscountAmt As Short = 36
    Private Const ColAcceptWt As Short = 37
    Private Const ColRefNo As Short = 38
    Private Const ColRefDate As Short = 39
    Private Const ColInvoiceType As Short = 40
    Private Const ColAcctHeadName As Short = 41
    Private Const ColRMGrade As Short = 42
    Private Const ColDivision As Short = 43
    Private Const ColCompanyName As Short = 44
    Private Const ColMKEY As Short = 45

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim mClickProcess As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboAgtD3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAgtD3.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboAgtD3_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAgtD3.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboCancelled_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCancelled.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCancelled_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCancelled.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboFOC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFOC.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboFOC_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFOC.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboModvat_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboModvat.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboModvat_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboModvat.SelectedIndexChanged
        Call PrintStatus(False)
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

    Private Sub chkAllSubCat_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSubCat.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSubCategory.Enabled = False
            cmdSubCatsearch.Enabled = False
        Else
            txtSubCategory.Enabled = True
            cmdSubCatsearch.Enabled = True
        End If
    End Sub
    Private Sub SearchSubCategory()
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCatCode As String

        If txtCategory.Text = "" Then
            MsgInformation("Please Select category .")
            txtCategory.Focus()
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCatCode = MasterNo
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"


        If MainClass.SearchGridMaster((txtSubCategory.Text), "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtSubCategory.Text = AcName
            End If
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub chkAllItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItem.CheckStateChanged
        Call PrintStatus(False)
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemName.Enabled = False
            cmdsearchItem.Enabled = False
        Else
            txtItemName.Enabled = True
            cmdsearchItem.Enabled = True
        End If
    End Sub

    Private Sub chkMonthWise_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMonthWise.CheckStateChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub ReportonShow(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mSubTitle1 As String

        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mSelected As Boolean

        Report1.Reset()

        mTitle = "Item Purchases"

        mSelected = True
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                '            mSubTitle = IIf(mSubTitle = "", mInvoiceType, mSubTitle & "/" & mInvoiceType)
            Else
                mSelected = False
            End If
        Next
        If mSelected = True Then
            mSubTitle = ""
        Else
            mSubTitle = " (" & mSubTitle & ")"
        End If

        mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text & mSubTitle


        If cboDivision.Text <> "ALL" Then
            mSubTitle = mSubTitle & " Division : " & cboDivision.Text
        End If

        If cboDivision.Text <> "ALL" Then
            mSubTitle = mSubTitle & " Division : " & cboDivision.Text
        End If

        If cboAgtD3.SelectedIndex = 1 Then
            mSubTitle1 = "AGT D3"
        End If

        If cboFOC.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "FOC", "/FOC")
        End If

        If cboModvat.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "Modvat", "/Modvat")
        End If

        If cboCancelled.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "Cancelled", "/Cancelled")
        End If

        mSubTitle = mSubTitle & IIf(mSubTitle1 = "", "", " (" & mSubTitle1 & ")")

        mSubTitle = Mid(mSubTitle, 1, 254)

        If optType(0).Checked = True Then
            If optOrderBy(0).Checked = True Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemPurchases.RPT"
            Else
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\IPBillWise.RPT"
            End If
        Else
            If chkMonthWise.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemPurchasesSumm.RPT"
            Else
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemPurMonthSumm.RPT"
            End If
        End If

        SqlStr = MakeSQL
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchItem.Click
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

    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
    End Sub

    Private Sub frmItemPurchases_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Item Purchases"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmItemPurchases_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        chkMonthWise.Enabled = False

        cboAgtD3.Items.Clear()
        cboFOC.Items.Clear()
        cboCancelled.Items.Clear()
        cboModvat.Items.Clear()

        cboAgtD3.Items.Add("BOTH")
        cboAgtD3.Items.Add("YES")
        cboAgtD3.Items.Add("NO")

        cboFOC.Items.Add("BOTH")
        cboFOC.Items.Add("YES")
        cboFOC.Items.Add("NO")

        cboCancelled.Items.Add("BOTH")
        cboCancelled.Items.Add("YES")
        cboCancelled.Items.Add("NO")

        cboModvat.Items.Add("BOTH")
        cboModvat.Items.Add("YES")
        cboModvat.Items.Add("NO")

        cboAgtD3.SelectedIndex = 0
        cboFOC.SelectedIndex = 0
        cboCancelled.SelectedIndex = 2
        cboModvat.SelectedIndex = 0

        Call FillInvoiceType()

        '    lblTrnType.text = -1
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        txtItemName.Enabled = False
        cmdsearchItem.Enabled = False

        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False
        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False


        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Call frmItemPurchases_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmItemPurchases_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmItemPurchases_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
            Call PrintStatus(False)
            chkMonthWise.Enabled = IIf(Index = 0, False, True)
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub

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
        Dim SqlStr As String

        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCategory.Text = UCase(Trim(txtCategory.Text))
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
    Private Sub SearchCategory()
        On Error GoTo ERR1
        Dim SqlStr As String

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

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            txtItemName.Text = AcName
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            If optType(0).Checked = True Then
                .ColHidden = True
            Else
                .ColHidden = IIf(chkMonthWise.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            End If

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDate, 8)
            .ColHidden = False

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 8)
            .ColHidden = False

            .Col = ColCustomerRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerRefNo, 8)
            .ColHidden = False

            .Col = ColCreditNoteNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCreditNoteNo, 8)
            .ColHidden = False

            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColHSNCode, 10)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemPartNo, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, IIf(optType(0).Checked = True, 24, 35))
            .ColsFrozen = ColItemName

            .Col = ColUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUOM, 5)
            .ColsFrozen = ColUOM

            .Col = ColMRRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMRRDate, 8)
            .ColHidden = True

            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMRRNo, 8)
            .ColHidden = True

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 8)
            If optType(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefNo, 8)
            If optType(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefDate, 8)
            If optType(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColInvoiceType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInvoiceType, 12)

            .Col = ColAcctHeadName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAcctHeadName, 12)

            .Col = ColRMGrade
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRMGrade, 12)

            .Col = ColDivision
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDivision, 12)

            .Col = ColCompanyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCompanyName, 12)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 8)
            If optType(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyCode, 6)
            .ColHidden = True

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 25)


            For cntCol = ColQuantity To ColAcceptWt
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
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
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''' OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If optShowReg(0).Checked = True Then
            SqlStr = MakeSQL
        Else
            SqlStr = MakeSQLSupp
        End If



        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Call MainClass.AddBlankfpSprdRow(SprdMain, ColItemName)
        SprdMain.Col = ColItemName
        SprdMain.Row = SprdMain.MaxRows
        SprdMain.Text = "GRAND TOTAL :"
        SprdMain.Font = VB6.FontChangeBold(SprdMain.Font, True)

        SprdMain.Row = SprdMain.MaxRows
        SprdMain.Row2 = SprdMain.MaxRows
        SprdMain.Col = 1
        SprdMain.Col2 = SprdMain.MaxCols
        SprdMain.BlockMode = True
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
        SprdMain.Font = VB6.FontChangeBold(SprdMain.Font, False)
        SprdMain.BlockMode = False

        Call CalcRowTotal(SprdMain, ColQuantity, 1, ColQuantity, SprdMain.MaxRows - 1, (SprdMain.MaxRows), ColQuantity)
        Call CalcRowTotal(SprdMain, ColAmount, 1, ColAmount, SprdMain.MaxRows - 1, (SprdMain.MaxRows), ColAmount)
        Call CalcRowTotal(SprdMain, ColOthers, 1, ColOthers, SprdMain.MaxRows - 1, (SprdMain.MaxRows), ColOthers)
        Call CalcRowTotal(SprdMain, ColDiscountAmt, 1, ColDiscountAmt, SprdMain.MaxRows - 1, (SprdMain.MaxRows), ColDiscountAmt)

        'Private Const ColAmount As Short = 19
        'Private Const ColModvatAmount As Short = 20
        'Private Const ColSTRefund As Short = 21
        'Private Const ColCGSTRefundAmount As Short = 22
        'Private Const ColCGST As Short = 23
        'Private Const ColSSTRefundAmount As Short = 24
        'Private Const ColSGST As Short = 25
        'Private Const ColISTRefundAmount As Short = 26
        'Private Const ColIGST As Short = 27
        'Private Const ColGSTRefundAmount As Short = 28
        'Private Const ColGST As Short = 29
        'Private Const ColExciseDuty As Short = 30
        'Private Const ColCess As Short = 31
        'Private Const ColSHCess As Short = 32
        'Private Const ColVAT As Short = 33
        'Private Const ColCST As Short = 34
        'Private Const ColOthers As Short = 35
        'Private Const ColDiscountAmt As Short = 36
        'Private Const ColAcceptWt As Short = 37
        'Private Const ColRefNo As Short = 38
        'Private Const ColRefDate As Short = 39
        'Private Const ColInvoiceType As Short = 40
        'Private Const ColAcctHeadName As Short = 41
        'Private Const ColRMGrade As Short = 42
        'Private Const ColDivision As Short = 43
        'Private Const ColCompanyName As Short = 44
        'Private Const ColMKEY As Short = 45

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
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mCatCode As String
        Dim mSubCatCode As String
        Dim mSuppCode As String
        Dim mItemCode As String
        Dim mDivision As Double
        Dim mCompanyGSTNo As String
        Dim mAllTrnType As Boolean

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        mAllTrnType = True
        mCompanyGSTNo = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)

        ''''SELECT CLAUSE...


        If optType(0).Checked = True Then
            MakeSQL = " SELECT '', IH.VNO, IH.VDATE, IH.REJ_CREDITNOTE, IH.CUSTOMER_REF_NO, ID.HSNCODE,ITEMMST.ITEM_CODE, " & vbCrLf _
                & " ID.ITEM_DESC, ID.CUSTOMER_PART_NO, ID.ITEM_UOM, IH.AUTO_KEY_MRR,IH.MRRDATE,  " & vbCrLf _
                & " IH.BILLNO, IH.INVOICE_DATE," & vbCrLf _
                & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, " & vbCrLf _
                & " ID.ITEM_QTY, ID.ITEM_RATE, TO_CHAR(ID.ITEM_QTY * ID.ITEM_RATE)," & vbCrLf _
                & " CASE WHEN IH.ITEMVALUE=0 Or ISMODVAT='N' THEN 0 ELSE ((IH.MODVATAMOUNT+IH.ADEMODVATAMOUNT+IH.CESSAMOUNT+IH.SHECMODVATAMOUNT)*ID.ITEM_AMT/IH.ITEMVALUE) END AS MODVATAMOUNT, " & vbCrLf _
                & " CASE WHEN IH.ITEMVALUE=0 OR ISSTREFUND='N' THEN 0 ELSE ((IH.STCLAIMAMOUNT+IH.SUR_VATCLAIMAMOUNT)*ID.ITEM_AMT/IH.ITEMVALUE) END AS STREFUNDAMOUNT, "




            MakeSQL = MakeSQL & vbCrLf _
                & " CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTCGST_REFUNDAMT)*ID.CGST_AMOUNT/IH.TOTALGSTVALUE) END AS CGSTREFUNDAMOUNT," & vbCrLf _
                & " CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.CGST_AMOUNT END AS CGSTAMOUNT," & vbCrLf _
                & " CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTSGST_REFUNDAMT)*ID.SGST_AMOUNT/IH.TOTALGSTVALUE) END AS SGSTREFUNDAMOUNT," & vbCrLf _
                & " CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.SGST_AMOUNT END AS SGSTAMOUNT," & vbCrLf _
                & " CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTIGST_REFUNDAMT)*ID.IGST_AMOUNT/IH.TOTALGSTVALUE) END AS IGSTREFUNDAMOUNT," & vbCrLf _
                & " CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.IGST_AMOUNT END AS IGSTAMOUNT," & vbCrLf _
                & " CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTCGST_REFUNDAMT+IH.TOTSGST_REFUNDAMT+IH.TOTIGST_REFUNDAMT)*(ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT)/IH.TOTALGSTVALUE) END AS GSTREFUNDAMOUNT," & vbCrLf _
                & " CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT END AS GSTAMOUNT," & vbCrLf

            MakeSQL = MakeSQL & vbCrLf _
                & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.TOTEDAMOUNT+IH.ADEAMOUNT)*ID.ITEM_AMT/IH.ITEMVALUE) END AS EDAMOUNT, " & vbCrLf _
                & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.TOTEDUAMOUNT)*ID.ITEM_AMT/IH.ITEMVALUE) END AS CESS," & vbCrLf _
                & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.SHECAMOUNT)*ID.ITEM_AMT/IH.ITEMVALUE) END AS SHECESS," & vbCrLf _
                & " DECODE(CMST.WITHIN_STATE,'Y',CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE (IH.TOTSTAMT*ID.ITEM_AMT/IH.ITEMVALUE) END,0) AS VATTAX, " & vbCrLf _
                & " DECODE(CMST.WITHIN_STATE,'N',CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE (IH.TOTSTAMT*ID.ITEM_AMT/IH.ITEMVALUE) END,0) AS CSTTAX," & vbCrLf _
                & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.TOTEXPAMT - (IH.TOTEDAMOUNT+IH.ADEAMOUNT+IH.TOTEDUAMOUNT+IH.SHECAMOUNT+IH.TOTSTAMT+IH.TOTDISCAMOUNT))*ID.ITEM_AMT/IH.ITEMVALUE) END AS OTHERS, " & vbCrLf _
                & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((TOTDISCAMOUNT)*ID.ITEM_AMT/IH.ITEMVALUE) END AS DISCAMOUNT, ((ID.ITEM_QTY-SHORTAGE_QTY) * ITEMMST.ITEM_WEIGHT * .001) AS RM_WT, " & vbCrLf _
                & " ID.CUST_REF_NO, ID.CUST_REF_DATE, INVMST.NAME, ACMST.SUPP_CUST_NAME, ITEMMST.MAT_DESC,DIV.DIV_DESC,CC.COMPANY_SHORTNAME,IH.MKEY "

        Else
            If chkMonthWise.CheckState = System.Windows.Forms.CheckState.Checked Then
                MakeSQL = " SELECT TO_CHAR(IH.INVOICE_DATE,'MM-YYYY'),"
            Else
                MakeSQL = " SELECT '',"
            End If

            MakeSQL = MakeSQL & vbCrLf & " '','','','',ID.HSNCODE, ID.ITEM_CODE , ID.ITEM_DESC, ID.CUSTOMER_PART_NO, ID.ITEM_UOM, '','',  " & vbCrLf & " '', ''," & vbCrLf & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(SUM(ID.ITEM_QTY)) AS ITEM_QTY, ID.ITEM_RATE, " & vbCrLf & " TO_CHAR(SUM(ID.ITEM_QTY * ID.ITEM_RATE)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 OR ISMODVAT='N' THEN 0 ELSE ((IH.MODVATAMOUNT+IH.ADEMODVATAMOUNT+IH.CESSAMOUNT+IH.SHECMODVATAMOUNT)*ID.ITEM_AMT/IH.ITEMVALUE) END)) AS MODVATAMOUNT, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 OR ISSTREFUND='N' THEN 0 ELSE ((IH.STCLAIMAMOUNT+IH.SUR_VATCLAIMAMOUNT)*ID.ITEM_AMT/IH.ITEMVALUE) END)) AS STREFUNDAMOUNT, "

            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTCGST_REFUNDAMT)*ID.CGST_AMOUNT/IH.TOTALGSTVALUE) END)) AS CGSTREFUNDAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.CGST_AMOUNT END)) AS CGSTAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTSGST_REFUNDAMT)*ID.SGST_AMOUNT/IH.TOTALGSTVALUE) END)) AS SGSTREFUNDAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.SGST_AMOUNT END)) AS SGSTAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTIGST_REFUNDAMT)*ID.IGST_AMOUNT/IH.TOTALGSTVALUE) END)) AS IGSTREFUNDAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.IGST_AMOUNT END)) AS IGSTAMOUNT,"


            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTCGST_REFUNDAMT+IH.TOTSGST_REFUNDAMT+IH.TOTIGST_REFUNDAMT)*(ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT)/IH.TOTALGSTVALUE) END)) AS GSTREFUNDAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT END)) AS GSTAMOUNT,"

            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.TOTEDAMOUNT+IH.ADEAMOUNT)*ID.ITEM_AMT/IH.ITEMVALUE) END)) AS EDAMOUNT, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.TOTEDUAMOUNT)*ID.ITEM_AMT/IH.ITEMVALUE) END)) AS CESS," & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.SHECAMOUNT)*ID.ITEM_AMT/IH.ITEMVALUE) END)) AS SHECESS," & vbCrLf & " TO_CHAR(SUM(DECODE(CMST.WITHIN_STATE,'Y',CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE (IH.TOTSTAMT*ID.ITEM_AMT/IH.ITEMVALUE) END,0))) AS VATTAX, " & vbCrLf & " TO_CHAR(SUM(DECODE(CMST.WITHIN_STATE,'N',CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE (IH.TOTSTAMT*ID.ITEM_AMT/IH.ITEMVALUE) END,0))) AS CSTTAX," & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.TOTEXPAMT - (IH.TOTEDAMOUNT+IH.ADEAMOUNT+IH.TOTEDUAMOUNT+IH.SHECAMOUNT+IH.TOTSTAMT+IH.TOTDISCAMOUNT))*ID.ITEM_AMT/IH.ITEMVALUE) END)) AS OTHERS, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((TOTDISCAMOUNT)*ID.ITEM_AMT/IH.ITEMVALUE) END)) AS DISCAMOUNT, SUM((ID.ITEM_QTY-SHORTAGE_QTY) * ITEMMST.ITEM_WEIGHT * .001) AS RM_WT, " & vbCrLf _
                & " '','', INVMST.NAME, ACMST.SUPP_CUST_NAME,ITEMMST.MAT_DESC,DIV.DIV_DESC,CC.COMPANY_SHORTNAME,'' "

        End If

        ''''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST ITEMMST, FIN_INVTYPE_MST INVMST, FIN_SUPP_CUST_MST ACMST, INV_DIVISION_MST DIV, GEN_COMPANY_MST CC"

        ''''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf _
            & " IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CC.COMPANY_CODE AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND ID.COMPANY_CODE=ACMST.COMPANY_CODE" & vbCrLf & " AND ID.PUR_ACCOUNT_CODE=ACMST.SUPP_CUST_CODE" & vbCrLf & " AND ID.COMPANY_CODE=ITEMMST.COMPANY_CODE(+)" & vbCrLf & " AND ID.ITEM_CODE=ITEMMST.ITEM_CODE(+)" & vbCrLf & " AND IH.COMPANY_CODE=DIV.COMPANY_CODE" & vbCrLf & " AND IH.DIV_CODE=DIV.DIV_CODE" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_TRNTYPE=INVMST.CODE"

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
            MakeSQL = MakeSQL & vbCrLf & " AND CC.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'"
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND ITEMMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND ITEMMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        '    If cboInvoiceType.Text = "ALL" Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND INVMST.IDENTIFICATION<>'J'"
        '    Else
        '        MakeSQL = MakeSQL & vbCrLf & "AND IH.TRNTYPE=" & Val(lblTrnType.text) & ""
        '    End If


        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mAllTrnType = False
            End If
        Next

        If mTrnTypeStr <> "" And mAllTrnType = False Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " AND ID.ITEM_TRNTYPE IN " & mTrnTypeStr & ""
        End If

        If cboAgtD3.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboAgtD3.Text, 1) & "'"
        End If

        If cboFOC.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.ISFOC='" & VB.Left(cboFOC.Text, 1) & "'"
        End If

        If cboModvat.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.ISMODVAT='" & VB.Left(cboModvat.Text, 1) & "'"
        End If

        If cboCancelled.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If

        'If Trim(txtTariffHeading.Text) <> "" Then
        '    MakeSQL = MakeSQL & vbCrLf & "AND IH.TARIFFHEADING='" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & "'"
        'End If

        '    MakeSQL = MakeSQL & vbCrLf _
        ''            & " AND IH.INVOICE_DATE>=TO_DATE('" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " AND IH.INVOICE_DATE<=TO_DATE('" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"

        MakeSQL = MakeSQL & vbCrLf & " AND IH.ISFINALPOST='Y' AND IH.VNO<>'-1'"

        If optDate(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.VDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND IH.MRRDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        '''' GROUP BY CLAUSE
        If optType(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " GROUP BY "

            If chkMonthWise.CheckState = System.Windows.Forms.CheckState.Checked Then
                MakeSQL = MakeSQL & "TO_CHAR(IH.INVOICE_DATE,'MM-YYYY'), "
            End If

            MakeSQL = MakeSQL & vbCrLf & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, " & vbCrLf _
                & " ID.HSNCODE, ID.ITEM_CODE , ID.ITEM_DESC, ID.CUSTOMER_PART_NO, ID.ITEM_UOM, ID.ITEM_RATE,INVMST.NAME,ACMST.SUPP_CUST_NAME,ITEMMST.MAT_DESC,DIV.DIV_DESC,CC.COMPANY_SHORTNAME"
        End If



        ''''ORDER BY CLAUSE...

        If optType(0).Checked = True Then
            If optOrderBy(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY ID.ITEM_DESC,CMST.SUPP_CUST_NAME,IH.BILLNO, IH.INVOICE_DATE"
            Else
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.BILLNO,CMST.SUPP_CUST_NAME,ID.ITEM_DESC, IH.INVOICE_DATE"
            End If
        Else
            '        If optOrderBy(0).Value = True Then
            '            MakeSQL = MakeSQL & vbCrLf & "ORDER BY ID.ITEM_DESC,CMST.SUPP_CUST_NAME"
            '        Else
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY "

            If chkMonthWise.CheckState = System.Windows.Forms.CheckState.Checked Then
                MakeSQL = MakeSQL & "TO_CHAR(IH.INVOICE_DATE,'MM-YYYY'), "
            End If

            MakeSQL = MakeSQL & vbCrLf & " CMST.SUPP_CUST_NAME,ID.ITEM_CODE , ID.ITEM_DESC,ID.ITEM_RATE"
            '        End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLSupp() As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mCatCode As String
        Dim mSubCatCode As String
        Dim mSuppCode As String
        Dim mItemCode As String
        Dim mDivision As Double
        Dim mCompanyGSTNo As String
        Dim mAllTrnType As Boolean

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String
        mAllTrnType = True
        mCompanyGSTNo = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)


        ''''SELECT CLAUSE...


        If optType(0).Checked = True Then
            MakeSQLSupp = " SELECT '', IH.VNO, IH.VDATE,'','', ID.HSNCODE, ITEMMST.ITEM_CODE, ITEMMST.ITEM_SHORT_DESC,'', ID.ITEM_UOM, ID.AUTO_KEY_MRR,ID.MRRDATE,  " & vbCrLf _
                & " IH.BILLNO, IH.INVOICE_DATE," & vbCrLf _
                & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, " & vbCrLf _
                & " ID.QTY, ID.RATE, TO_CHAR(ID.QTY * ID.RATE)," & vbCrLf _
                & " CASE WHEN IH.ITEMVALUE=0 OR ISMODVAT='N' THEN 0 ELSE ((IH.MODVATAMOUNT+IH.CESSAMOUNT+IH.SHECMODVATAMOUNT)*ID.AMOUNT/IH.ITEMVALUE) END AS MODVATAMOUNT, " & vbCrLf _
                & " CASE WHEN IH.ITEMVALUE=0 OR ISSTREFUND='N' THEN 0 ELSE ((IH.STCLAIMAMOUNT+IH.SUR_VATCLAIMAMOUNT)*ID.AMOUNT/IH.ITEMVALUE) END AS STREFUNDAMOUNT, "

            MakeSQLSupp = MakeSQLSupp & vbCrLf _
                & " CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTCGST_REFUNDAMT)*ID.CGST_AMOUNT/IH.TOTALGSTVALUE) END AS CGSTREFUNDAMOUNT," & vbCrLf _
                & " CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.CGST_AMOUNT END AS CGSTAMOUNT," & vbCrLf _
                & " CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTSGST_REFUNDAMT)*ID.SGST_AMOUNT/IH.TOTALGSTVALUE) END AS SGSTREFUNDAMOUNT," & vbCrLf _
                & " CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.SGST_AMOUNT END AS SGSTAMOUNT," & vbCrLf _
                & " CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTIGST_REFUNDAMT)*ID.IGST_AMOUNT/IH.TOTALGSTVALUE) END AS IGSTREFUNDAMOUNT," & vbCrLf _
                & " CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.IGST_AMOUNT END AS IGSTAMOUNT," & vbCrLf _
                & " CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTCGST_REFUNDAMT+IH.TOTSGST_REFUNDAMT+IH.TOTIGST_REFUNDAMT)*(ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT)/IH.TOTALGSTVALUE) END AS GSTREFUNDAMOUNT," & vbCrLf _
                & " CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT END AS GSTAMOUNT,"

            MakeSQLSupp = MakeSQLSupp & vbCrLf _
                & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.TOTEDAMOUNT)*ID.AMOUNT/IH.ITEMVALUE) END AS EDAMOUNT, " & vbCrLf & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.TOTEDUAMOUNT)*ID.AMOUNT/IH.ITEMVALUE) END AS CESS," & vbCrLf & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.SHECAMOUNT)*ID.AMOUNT/IH.ITEMVALUE) END AS SHECESS," & vbCrLf & " DECODE(CMST.WITHIN_STATE,'Y',CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE (IH.TOTSTAMT*ID.AMOUNT/IH.ITEMVALUE) END,0) AS VATTAX, " & vbCrLf & " DECODE(CMST.WITHIN_STATE,'N',CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE (IH.TOTSTAMT*ID.AMOUNT/IH.ITEMVALUE) END,0) AS CSTTAX," & vbCrLf & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.TOTEXPAMT - (IH.TOTEDAMOUNT+IH.TOTEDUAMOUNT+IH.SHECAMOUNT+IH.TOTSTAMT+IH.TOTDISCAMOUNT))*ID.AMOUNT/IH.ITEMVALUE) END AS OTHERS, " & vbCrLf & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((TOTDISCAMOUNT)*ID.AMOUNT/IH.ITEMVALUE) END AS DISCAMOUNT, 0 AS RM_WT, " & vbCrLf & " ID.BILL_NO, ID.BILLDATE, INVMST.NAME, ACMST.SUPP_CUST_NAME,ITEMMST.MAT_DESC,DIV.DIV_DESC, CC.COMPANY_SHORTNAME,IH.MKEY "

        Else
            If chkMonthWise.CheckState = System.Windows.Forms.CheckState.Checked Then
                MakeSQLSupp = " SELECT TO_CHAR(IH.INVOICE_DATE,'MM-YYYY'),"
            Else
                MakeSQLSupp = " SELECT '',"
            End If

            MakeSQLSupp = MakeSQLSupp & vbCrLf & " '', '', '','',ID.HSNCODE, ID.ITEM_CODE , ITEMMST.ITEM_SHORT_DESC, '',ID.ITEM_UOM, '','',  " & vbCrLf & " '', ''," & vbCrLf & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(SUM(ID.QTY)) AS ITEM_QTY, ID.RATE, " & vbCrLf & " TO_CHAR(SUM(ID.QTY * ID.RATE)), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 OR ISMODVAT='N' THEN 0 ELSE ((IH.MODVATAMOUNT+IH.CESSAMOUNT+IH.SHECMODVATAMOUNT)*ID.AMOUNT/IH.ITEMVALUE) END)) AS MODVATAMOUNT, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 OR ISSTREFUND='N' THEN 0 ELSE ((IH.STCLAIMAMOUNT+IH.SUR_VATCLAIMAMOUNT)*ID.AMOUNT/IH.ITEMVALUE) END)) AS STREFUNDAMOUNT, "

            MakeSQLSupp = MakeSQLSupp & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTCGST_REFUNDAMT)*ID.CGST_AMOUNT/IH.TOTALGSTVALUE) END)) AS CGSTREFUNDAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.CGST_AMOUNT END)) AS CGSTAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTSGST_REFUNDAMT)*ID.SGST_AMOUNT/IH.TOTALGSTVALUE) END)) AS SGSTREFUNDAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.SGST_AMOUNT END)) AS SGSTAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTIGST_REFUNDAMT)*ID.IGST_AMOUNT/IH.TOTALGSTVALUE) END)) AS IGSTREFUNDAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.IGST_AMOUNT END)) AS IGSTAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.TOTALGSTVALUE=0 THEN 0 ELSE ((IH.TOTCGST_REFUNDAMT+IH.TOTSGST_REFUNDAMT+IH.TOTIGST_REFUNDAMT)*(ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT)/IH.TOTALGSTVALUE) END)) AS GSTREFUNDAMOUNT," & vbCrLf & " TO_CHAR(SUM(CASE WHEN '" & mCompanyGSTNo & "' = NVL(CMST.GST_RGN_NO,'') THEN 0 ELSE ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT END)) AS GSTAMOUNT,"

            MakeSQLSupp = MakeSQLSupp & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.TOTEDAMOUNT)*ID.AMOUNT/IH.ITEMVALUE) END)) AS EDAMOUNT, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.TOTEDUAMOUNT)*ID.AMOUNT/IH.ITEMVALUE) END)) AS CESS," & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.SHECAMOUNT)*ID.AMOUNT/IH.ITEMVALUE) END)) AS SHECESS," & vbCrLf & " TO_CHAR(SUM(DECODE(CMST.WITHIN_STATE,'Y',CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE (IH.TOTSTAMT*ID.AMOUNT/IH.ITEMVALUE) END,0))) AS VATTAX, " & vbCrLf & " TO_CHAR(SUM(DECODE(CMST.WITHIN_STATE,'N',CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE (IH.TOTSTAMT*ID.AMOUNT/IH.ITEMVALUE) END,0))) AS CSTTAX," & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((IH.TOTEXPAMT - (IH.TOTEDAMOUNT+IH.TOTEDUAMOUNT+IH.SHECAMOUNT+IH.TOTSTAMT+IH.TOTDISCAMOUNT))*ID.AMOUNT/IH.ITEMVALUE) END)) AS OTHERS, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ((TOTDISCAMOUNT)*ID.AMOUNT/IH.ITEMVALUE) END)) AS DISCAMOUNT, 0 AS RM_WT," & vbCrLf & " '','',INVMST.NAME,ACMST.SUPP_CUST_NAME,ITEMMST.MAT_DESC,DIV.DIV_DESC,CC.COMPANY_SHORTNAME,'' "

        End If

        ''''FROM CLAUSE...
        MakeSQLSupp = MakeSQLSupp & vbCrLf & " FROM FIN_SUPP_PURCHASE_HDR  IH, FIN_SUPP_PURCHASE_DET  ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST ITEMMST, FIN_INVTYPE_MST INVMST, FIN_SUPP_CUST_MST ACMST, INV_DIVISION_MST DIV, GEN_COMPANY_MST CC"

        ''''WHERE CLAUSE...
        MakeSQLSupp = MakeSQLSupp & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CC.COMPANY_CODE AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=DIV.COMPANY_CODE" & vbCrLf _
            & " AND IH.DIV_CODE=DIV.DIV_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE=ACMST.COMPANY_CODE (+)" & vbCrLf _
            & " AND ID.PUR_ACCOUNT_CODE=ACMST.SUPP_CUST_CODE (+)" & vbCrLf _
            & " AND ID.COMPANY_CODE=ITEMMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=ITEMMST.ITEM_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.TRNTYPE=INVMST.CODE"

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
            MakeSQLSupp = MakeSQLSupp & vbCrLf & " AND CC.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCode = MasterNo
                MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'"
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND ITEMMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND ITEMMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        '    If cboInvoiceType.Text = "ALL" Then
        '        MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND INVMST.IDENTIFICATION<>'J'"
        '    Else
        '        MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.TRNTYPE=" & Val(lblTrnType.text) & ""
        '    End If

        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mAllTrnType = False
            End If
        Next

        If mTrnTypeStr <> "" And mAllTrnType = False Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            MakeSQLSupp = MakeSQLSupp & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
        End If

        If cboAgtD3.SelectedIndex > 0 Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboAgtD3.Text, 1) & "'"
        End If

        If cboFOC.SelectedIndex > 0 Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.ISFOC='" & VB.Left(cboFOC.Text, 1) & "'"
        End If

        If cboModvat.SelectedIndex > 0 Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.ISMODVAT='" & VB.Left(cboModvat.Text, 1) & "'"
        End If

        If cboCancelled.SelectedIndex > 0 Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If

        'If Trim(txtTariffHeading.Text) <> "" Then
        '    MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.TARIFFHEADING='" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & "'"
        'End If

        '    MakeSQLSupp = MakeSQLSupp & vbCrLf _
        ''            & " AND IH.INVOICE_DATE>=TO_DATE('" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " AND IH.INVOICE_DATE<=TO_DATE('" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"

        MakeSQLSupp = MakeSQLSupp & vbCrLf & " AND IH.ISFINALPOST='Y' AND IH.VNO<>'-1'"

        If optDate(0).Checked = True Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & " AND IH.VDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQLSupp = MakeSQLSupp & vbCrLf & " AND IH.MRRDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        '''' GROUP BY CLAUSE
        If optType(1).Checked = True Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & " GROUP BY "

            If chkMonthWise.CheckState = System.Windows.Forms.CheckState.Checked Then
                MakeSQLSupp = MakeSQLSupp & "TO_CHAR(IH.INVOICE_DATE,'MM-YYYY'), "
            End If

            MakeSQLSupp = MakeSQLSupp & vbCrLf & " ID.HSNCODE, CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, " & vbCrLf _
                & " ID.ITEM_CODE , ITEMMST.ITEM_SHORT_DESC, ID.ITEM_UOM, ID.RATE,INVMST.NAME, ACMST.SUPP_CUST_NAME,ITEMMST.MAT_DESC,DIV.DIV_DESC,CC.COMPANY_SHORTNAME "
        End If

        ''''ORDER BY CLAUSE...

        If optType(0).Checked = True Then
            If optOrderBy(0).Checked = True Then
                MakeSQLSupp = MakeSQLSupp & vbCrLf & "ORDER BY ITEMMST.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME,IH.BILLNO, IH.INVOICE_DATE"
            Else
                MakeSQLSupp = MakeSQLSupp & vbCrLf & "ORDER BY IH.BILLNO,CMST.SUPP_CUST_NAME,ITEMMST.ITEM_SHORT_DESC, IH.INVOICE_DATE"
            End If
        Else
            '        If optOrderBy(0).Value = True Then
            '            MakeSQLSupp = MakeSQLSupp & vbCrLf & "ORDER BY ID.ITEM_DESC,CMST.SUPP_CUST_NAME"
            '        Else
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "ORDER BY "

            If chkMonthWise.CheckState = System.Windows.Forms.CheckState.Checked Then
                MakeSQLSupp = MakeSQLSupp & "TO_CHAR(IH.INVOICE_DATE,'MM-YYYY'), "
            End If

            MakeSQLSupp = MakeSQLSupp & vbCrLf & " CMST.SUPP_CUST_NAME,ID.ITEM_CODE , ITEMMST.ITEM_SHORT_DESC,ID.RATE"
            '        End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer

        lstInvoiceType.Items.Clear()
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='P' ORDER BY NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstInvoiceType.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("Name").Value)
                lstInvoiceType.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

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

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub lstInvoiceType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstInvoiceType.ItemCheck
        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstInvoiceType.GetItemChecked(0) = True Then
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstInvoiceType.GetItemChecked(e.Index - 1) = False Then
                    lstInvoiceType.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
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

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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
    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick

        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColBillDate
        xVDate = Me.SprdMain.Text

        SprdMain.Col = ColMKEY
        xMKey = Me.SprdMain.Text

        SqlStr = "SELECT VNO FROM FIN_PURCHASE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MKEY='" & xMKey & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            xVNo = IIf(IsDbNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)

            Call ShowTrn(xMKey, xVDate, "", xVNo, "P", "", Me)
        End If
    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then
            SprdMain_DblClick(SprdMain, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        '    If KeyCode = vbKeyEscape Then cmdClose = True
    End Sub

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        SearchItem()
    End Sub


    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub


    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If txtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtItemName.Text = UCase(Trim(txtItemName.Text))
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


    Private Sub txtSubCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtSubCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.DoubleClick
        SearchSubCategory()
    End Sub


    Private Sub txtSubCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSubCategory()
    End Sub

    Private Sub txtSubCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCatCode As String

        If txtSubCategory.Text = "" Then GoTo EventExitSub


        If txtCategory.Text = "" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Else
            If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mCatCode = MasterNo
            End If
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"
        End If

        If MainClass.ValidateWithMasterTable((txtSubCategory.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("No Such Sub Category ")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

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
