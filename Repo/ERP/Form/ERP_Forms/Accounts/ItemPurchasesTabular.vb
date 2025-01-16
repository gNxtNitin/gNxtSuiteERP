Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmItemPurchasesTabular
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String

    Private Const ColPartyCode As Short = 1
    Private Const ColPartyName As Short = 2
    Private Const ColItemCode As Short = 3
    Private Const ColItemName As Short = 4
    Private Const ColUOM As Short = 5
    Private Const ColQty As Short = 6
    Private Const ColRate As Short = 7
    Private Const ColAmount As Short = 8
    Private Const ColAprQty As Short = 9
    Private Const ColAprRate As Short = 10
    Private Const ColAprAmount As Short = 11
    Private Const ColMayQty As Short = 12
    Private Const ColMayRate As Short = 13
    Private Const ColMayAmount As Short = 14
    Private Const ColJunQty As Short = 15
    Private Const ColJunRate As Short = 16
    Private Const ColJunAmount As Short = 17
    Private Const ColJulQty As Short = 18
    Private Const ColJulRate As Short = 19
    Private Const ColJulAmount As Short = 20
    Private Const ColAugQty As Short = 21
    Private Const ColAugRate As Short = 22
    Private Const ColAugAmount As Short = 23
    Private Const ColSepQty As Short = 24
    Private Const ColSepRate As Short = 25
    Private Const ColSepAmount As Short = 26
    Private Const ColOctQty As Short = 27
    Private Const ColOctRate As Short = 28
    Private Const ColOctAmount As Short = 29
    Private Const ColNovQty As Short = 30
    Private Const ColNovRate As Short = 31
    Private Const ColNovAmount As Short = 32
    Private Const ColDecQty As Short = 33
    Private Const ColDecRate As Short = 34
    Private Const ColDecAmount As Short = 35
    Private Const ColJanQty As Short = 36
    Private Const ColJanRate As Short = 37
    Private Const ColJanAmount As Short = 38
    Private Const ColFebQty As Short = 39
    Private Const ColFebRate As Short = 40
    Private Const ColFebAmount As Short = 41
    Private Const ColMarQty As Short = 42
    Private Const ColMarRate As Short = 43
    Private Const ColMarAmount As Short = 44

    Dim mClickProcess As Boolean

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
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
        Dim Sqlstr As String
        Dim mCatCode As String

        If txtCategory.Text = "" Then
            MsgInformation("Please Select category .")
            txtCategory.Focus()
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , Sqlstr) = True Then
            mCatCode = MasterNo
        End If
        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"


        If MainClass.SearchGridMaster((txtSubCategory.Text), "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , Sqlstr) = True Then
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
        Dim Sqlstr As String
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
        '
        '    If optType(0).Value = True Then
        '        If optOrderBy(0).Value = True Then
        '            Report1.ReportFileName = App.path & "\Reports\ItemPurchases.RPT"
        '        Else
        '            Report1.ReportFileName = App.path & "\Reports\IPBillWise.RPT"
        '        End If
        '    Else
        '        If chkMonthWise.Value = vbUnchecked Then
        '            Report1.ReportFileName = App.path & "\Reports\ItemPurchasesSumm.RPT"
        '        Else
        '            Report1.ReportFileName = App.path & "\Reports\ItemPurMonthSumm.RPT"
        '        End If
        '    End If

        Sqlstr = MakeSQL
        Call ShowReport(Sqlstr, Mode, mTitle, mSubTitle)
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

    Private Sub frmItemPurchasesTabular_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Item Purchases (Tabular Format Month Wise)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmItemPurchasesTabular_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        cboCancelled.SelectedIndex = 0
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

        Call frmItemPurchasesTabular_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmItemPurchasesTabular_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmItemPurchasesTabular_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
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
        Dim Sqlstr As String

        If txtCategory.Text = "" Then GoTo EventExitSub

        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , Sqlstr) = True Then
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
        Dim Sqlstr As String

        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster((txtCategory.Text), "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , Sqlstr) = True Then
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
        Dim Sqlstr As String

        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , Sqlstr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim Sqlstr As String

        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , Sqlstr)
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
        Dim Sqlstr As String

        If TxtAccount.Text = "" Then GoTo EventExitSub

        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , Sqlstr) = True Then
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
        'Dim mMaxCol As Long

        With SprdMain

            '        If optShow(0).Value = True Then
            '            mMaxCol = ColMarAmount
            '        Else
            '            mMaxCol = ColJun
            '        End If

            .MaxCols = ColMarAmount

            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

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
            .ColHidden = IIf(optOrderBy(0).Checked = True, True, False)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 8)
            .ColsFrozen = ColItemCode
            .ColHidden = IIf(optOrderBy(1).Checked = True, True, False)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 35)
            .ColsFrozen = ColItemName
            .ColHidden = IIf(optOrderBy(1).Checked = True, True, False)

            .Col = ColUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUOM, 5)
            .ColHidden = IIf(optOrderBy(1).Checked = True, True, False)

            For cntCol = ColQty To ColMarAmount
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


            '        Call FillHeading

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''' OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Sub FillHeading()
        On Error GoTo ErrPart
        Dim mFYear1 As Integer
        Dim mFYear2 As Integer
        Dim mFYear3 As Integer

        ''''SELECT CLAUSE...

        '    mFYear3 = RsCompany.fields("FYEAR").value
        '    mFYear2 = RsCompany.fields("FYEAR").value - 1
        '    mFYear1 = RsCompany.fields("FYEAR").value - 2
        '
        '    With SprdMain
        '
        '        .Row = 0
        '
        '        .Col = ColPartyCode
        '        .Text = "Party Code"
        '
        '        .Col = ColPartyName
        '        .Text = "Party Name"
        '
        '        .Col = ColItemCode
        '        .Text = "Item Code"
        '
        '        .Col = ColItemName
        '        .Text = "Item Name"
        '
        '        .Col = ColUOM
        '        .Text = "UOM"
        '
        '        .Col = ColAmount
        '        .Text = "Total Amount"
        '
        '        If optShow(1).Value = True Then
        '            .Col = ColApr
        '            .Text = "FYear " & mFYear1
        '
        '            .Col = ColMay
        '            .Text = "FYear " & mFYear2
        '
        '            .Col = ColJun
        '            .Text = "FYear " & mFYear3
        '        Else
        '            .Col = ColApr
        '            .Text = "April"
        '
        '            .Col = ColMay
        '            .Text = "May"
        '
        '            .Col = ColJun
        '            .Text = "June"
        '
        '            .Col = ColJul
        '            .Text = "July"
        '
        '            .Col = ColAug
        '            .Text = "August"
        '
        '            .Col = ColSep
        '            .Text = "September"
        '
        '            .Col = ColOct
        '            .Text = "October"
        '
        '            .Col = ColNov
        '            .Text = "November"
        '
        '            .Col = ColDec
        '            .Text = "December"
        '
        '            .Col = ColJan
        '            .Text = "January"
        '
        '            .Col = ColFeb
        '            .Text = "February"
        '
        '            .Col = ColMar
        '            .Text = "March"
        '        End If
        '
        '    End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim Sqlstr As String
        Dim cntRow As Integer
        Dim mAmount As Double
        Dim mQty As Double
        Dim cntCol As Double


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Sqlstr = MakeSQL
        MainClass.AssignDataInSprd8(Sqlstr, SprdMain, StrConn, "Y")

        FormatSprdMain(-1)
        '''********************************

        With SprdMain
            For cntRow = 1 To .MaxRows
                For cntCol = ColQty To ColMarAmount Step 3
                    .Row = cntRow

                    .Col = cntCol + 2 'ColAprAmount
                    mAmount = Val(.Text)

                    .Col = cntCol 'ColAprQty
                    mQty = Val(.Text)

                    .Col = cntCol + 1 'ColAprRate
                    .Text = CStr(CalcRate(mQty, mAmount))
                Next
            Next
        End With
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function CalcRate(ByRef pQty As Double, ByRef pAmount As Double) As Double
        On Error GoTo LedgError

        If pQty = 0 Then
            CalcRate = 0
        Else
            CalcRate = CDbl(VB6.Format(pAmount / pQty, "0.000"))
        End If

        Exit Function
LedgError:
        CalcRate = 0
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

        Dim mFYear1 As Integer
        Dim mFYear2 As Integer
        Dim mFYear3 As Integer
        Dim mDivision As Double
        Dim mAmountField As String
        Dim mDateField As String
        Dim mTrnTypeSelect As Boolean

        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String

        ''''SELECT CLAUSE...

        If optOrderBy(0).Checked = True Then
            MakeSQL = " SELECT '', '', " & vbCrLf & " ID.ITEM_CODE, ITEMMST.ITEM_SHORT_DESC, ID.ITEM_UOM, "

        ElseIf optOrderBy(1).Checked = True Then
            MakeSQL = " SELECT CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, " & vbCrLf _
                & " '', '', '', "
        Else
            MakeSQL = " SELECT CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, " & vbCrLf _
                & " ID.ITEM_CODE, ITEMMST.ITEM_SHORT_DESC, ID.ITEM_UOM, "
        End If

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(ID.ITEM_QTY)) AS QTY," & vbCrLf _
            & " 0 AS RATE,"


        If optRate(0).Checked = True Then
            mAmountField = "(ID.ITEM_QTY * ID.ITEM_RATE)"
        Else
            mAmountField = "(CASE WHEN IH.ITEMVALUE*ID.ITEM_QTY=0 THEN 0 ELSE ID.ITEM_QTY * (ID.ITEM_RATE + ((IH.TOTEDAMOUNT-IH.MODVATAMOUNT)*ID.ITEM_AMT/(IH.ITEMVALUE*ID.ITEM_QTY))+((IH.TOTEDUAMOUNT-IH.CESSAMOUNT)*ID.ITEM_AMT/(IH.ITEMVALUE*ID.ITEM_QTY))+((IH.SHECAMOUNT-IH.SHECMODVATAMOUNT)*ID.ITEM_AMT/(IH.ITEMVALUE*ID.ITEM_QTY))+((IH.ADEAMOUNT-IH.ADEMODVATAMOUNT)*ID.ITEM_AMT/(IH.ITEMVALUE*ID.ITEM_QTY))+((IH.TOTSTAMT-IH.STCLAIMAMOUNT)*ID.ITEM_AMT/(IH.ITEMVALUE*ID.ITEM_QTY))+((IH.TOTSURCHARGEAMT-IH.SUR_VATCLAIMAMOUNT)*ID.ITEM_AMT/(IH.ITEMVALUE*ID.ITEM_QTY))) END)"
            'mField = "ID.ITEM_QTY * (ID.ITEM_RATE + ((IH.TOTEXPAMT-IH.MODVATAMOUNT-IH.CESSAMOUNT-IH.SHECMODVATAMOUNT-IH.ADEMODVATAMOUNT-IH.STCLAIMAMOUNT-IH.SUR_VATCLAIMAMOUNT)*ID.ITEM_AMT/(IH.ITEMVALUE*ID.ITEM_QTY)))"
        End If

        If chkDebit.CheckState = System.Windows.Forms.CheckState.Checked Then
            mAmountField = mAmountField & " - (ID.ITEM_QTY *(NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0)))"
        End If

        If chkSupp.CheckState = System.Windows.Forms.CheckState.Checked Then
            mAmountField = mAmountField & " + (ID.ITEM_QTY *(NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0)))"
        End If

        '    mAmountField = " (ID.ITEM_QTY *(NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0)))"
        '    mAmountField = "(ID.ITEM_QTY *(NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0)))"


        If optDate(0).Checked = True Then
            mDateField = "IH.VDATE"
        Else
            mDateField = "IH.VDATE"
        End If


        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(" & mAmountField & ")) AS AMOUNT,"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='04' THEN ID.ITEM_QTY ELSE 0 END)) AS APR_QTY," & vbCrLf & " TO_CHAR(0) AS APR_RATE," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='04' THEN " & mAmountField & " ELSE 0 END)) AS APR_AMT,"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='05' THEN ID.ITEM_QTY ELSE 0 END)) AS MAY_QTY," & vbCrLf & " TO_CHAR(0) AS MAY_RATE," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='05' THEN " & mAmountField & " ELSE 0 END)) AS MAY_AMT,"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='06' THEN ID.ITEM_QTY ELSE 0 END)) AS JUN_QTY," & vbCrLf & " TO_CHAR(0) AS JUN_RATE," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='06' THEN " & mAmountField & " ELSE 0 END)) AS JUN_AMT,"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='07' THEN ID.ITEM_QTY ELSE 0 END)) AS JUL_QTY," & vbCrLf & " TO_CHAR(0) AS JUL_RATE," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='07' THEN " & mAmountField & " ELSE 0 END)) AS JUL_AMT,"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='08' THEN ID.ITEM_QTY ELSE 0 END)) AS AUG_QTY," & vbCrLf & " TO_CHAR(0) AS AUG_RATE," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='08' THEN " & mAmountField & " ELSE 0 END)) AS AUG_AMT,"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='09' THEN ID.ITEM_QTY ELSE 0 END)) AS SEP_QTY," & vbCrLf & " TO_CHAR(0) AS SEP_RATE," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='09' THEN " & mAmountField & " ELSE 0 END)) AS SEP_AMT,"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='10' THEN ID.ITEM_QTY ELSE 0 END)) AS OCT_QTY," & vbCrLf & " TO_CHAR(0) AS OCT_RATE," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='10' THEN " & mAmountField & " ELSE 0 END)) AS OCT_AMT,"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='11' THEN ID.ITEM_QTY ELSE 0 END)) AS NOV_QTY," & vbCrLf & " TO_CHAR(0) AS NOV_RATE," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='11' THEN " & mAmountField & " ELSE 0 END)) AS NOV_AMT,"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='12' THEN ID.ITEM_QTY ELSE 0 END)) AS DEC_QTY," & vbCrLf & " TO_CHAR(0) AS DEC_RATE," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='12' THEN " & mAmountField & " ELSE 0 END)) AS DEC_AMT,"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='01' THEN ID.ITEM_QTY ELSE 0 END)) AS JAN_QTY," & vbCrLf & " TO_CHAR(0) AS JAN_RATE," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='01' THEN " & mAmountField & " ELSE 0 END)) AS JAN_AMT,"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='02' THEN ID.ITEM_QTY ELSE 0 END)) AS FEB_QTY," & vbCrLf & " TO_CHAR(0) AS FEB_RATE," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='02' THEN " & mAmountField & " ELSE 0 END)) AS FEB_AMT,"

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='03' THEN ID.ITEM_QTY ELSE 0 END)) AS MAR_QTY," & vbCrLf & " TO_CHAR(0) AS MAR_RATE," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(" & mDateField & ",'MM')='03' THEN " & mAmountField & " ELSE 0 END)) AS MAR_AMT"



        ''''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST ITEMMST, FIN_INVTYPE_MST INVMST"

        ''''WHERE CLAUSE...
        'MakeSQL = MakeSQL & vbCrLf _
        '    & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME || ', ' || COMPANY_ADDR", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If

            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        MakeSQL = MakeSQL & vbCrLf _
            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE=ITEMMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=ITEMMST.ITEM_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_TRNTYPE=INVMST.CODE"

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

        mTrnTypeSelect = True
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mTrnTypeSelect = False
            End If
        Next

        If mTrnTypeSelect = False Then
            If mTrnTypeStr <> "" Then
                mTrnTypeStr = "(" & mTrnTypeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " AND ID.ITEM_TRNTYPE IN " & mTrnTypeStr & ""
            End If
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

        MakeSQL = MakeSQL & vbCrLf & " GROUP BY "


        If optOrderBy(0).Checked = True Then
            MakeSQL = MakeSQL & " ID.ITEM_CODE, ITEMMST.ITEM_SHORT_DESC, ID.ITEM_UOM "

        ElseIf optOrderBy(1).Checked = True Then
            MakeSQL = MakeSQL & "CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME "
        ElseIf optOrderBy(2).Checked = True Then
            MakeSQL = MakeSQL & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, " & vbCrLf & " ID.ITEM_CODE, ITEMMST.ITEM_SHORT_DESC, ID.ITEM_UOM "
        End If


        ''''ORDER BY CLAUSE...


        MakeSQL = MakeSQL & vbCrLf & "ORDER BY "

        If optOrderBy(0).Checked = True Then
            MakeSQL = MakeSQL & " ID.ITEM_CODE, ITEMMST.ITEM_SHORT_DESC, ID.ITEM_UOM "

        ElseIf optOrderBy(1).Checked = True Then
            MakeSQL = MakeSQL & "CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME "
        ElseIf optOrderBy(2).Checked = True Then
            MakeSQL = MakeSQL & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, " & vbCrLf & " ID.ITEM_CODE, ITEMMST.ITEM_SHORT_DESC, ID.ITEM_UOM "
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
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


    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim Sqlstr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer
        Dim mCompanyName As String
        Dim mCompanyAdd As String

        lstInvoiceType.Items.Clear()
        Sqlstr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='P' ORDER BY NAME"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstInvoiceType.Items.Add("ALL")
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("Name").Value)
                lstInvoiceType.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0

        cboDivision.Items.Clear()

        Sqlstr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDivision.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0


        mCompanyAdd = IIf(IsDBNull(RsCompany.Fields("COMPANY_SHORTNAME").Value), "", RsCompany.Fields("COMPANY_SHORTNAME").Value)
        mCompanyAdd = mCompanyAdd & ", " & IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)

        lstCompanyName.Items.Clear()
        Sqlstr = "SELECT COMPANY_SHORTNAME || ', ' ||  COMPANY_ADDR AS COMPANY_NAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_NAME").Value)
                mCompanyName = IIf(IsDBNull(RS.Fields("COMPANY_NAME").Value), "", RS.Fields("COMPANY_NAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(mCompanyName = mCompanyAdd, True, False))      '' RsCompany.Fields("COMPANY_NAME").Value
                'lstCompanyName.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
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
        Dim Sqlstr As String

        If txtItemName.Text = "" Then GoTo EventExitSub

        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , Sqlstr) = True Then
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
        Dim Sqlstr As String
        Dim mCatCode As String

        If txtSubCategory.Text = "" Then GoTo EventExitSub


        If txtCategory.Text = "" Then
            Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Else
            If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , Sqlstr) = True Then
                mCatCode = MasterNo
            End If
            Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"
        End If

        If MainClass.ValidateWithMasterTable((txtSubCategory.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , Sqlstr) = False Then
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
End Class
