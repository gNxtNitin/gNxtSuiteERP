Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmItemWiseBillWise
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String
    Private Const ColLockCode As Short = 1
    Private Const ColPartyCode As Short = 2
    Private Const ColVendorCode As Short = 3
    Private Const ColPONo As Short = 4
    Private Const ColCustomerAmendNo As Short = 5
    Private Const ColItemSNo As Short = 6
    Private Const ColItemCode As Short = 7
    Private Const ColItemPartNo As Short = 8
    Private Const ColItemName As Short = 9
    Private Const ColHSNCode As Short = 10
    Private Const ColVNo As Short = 11
    Private Const ColVDate As Short = 12
    Private Const ColBillNo As Short = 13
    Private Const ColBillDate As Short = 14
    Private Const ColGRNNo As Short = 15
    Private Const ColQuantity As Short = 16
    Private Const ColAcctQuantity As Short = 17
    Private Const ColRejQuantity As Short = 18
    Private Const ColRate As Short = 19
    Private Const ColDNCNRate As Short = 20
    Private Const ColSuppRate As Short = 21
    Private Const ColNetRate As Short = 22
    Private Const ColPORate As Short = 23
    Private Const ColDiff As Short = 24
    Private Const ColDiffAmount As Short = 25
    Private Const ColCGST As Short = 26
    Private Const ColSGST As Short = 27
    Private Const ColIGST As Short = 28
    Private Const ColNetDiffAmount As Short = 29
    Private Const ColFyear As Short = 30
    Private Const ColMRRDate As Short = 31
    Private Const ColCustRefNo As Short = 32
    Private Const ColMRRNo As Short = 33
    Private Const ColTDSPer As Short = 34
    Private Const ColESIPer As Short = 35
    Private Const ColPurchaseHead As Short = 36
    Private Const ColSuppBillNo As Short = 37
    Private Const ColSuppBillDate As Short = 38
    Private Const ColMKEY As Short = 39


    Dim mClickProcess As Boolean
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


    Private Sub chkHideZeroPORate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkHideZeroPORate.CheckStateChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkHideZeroQty_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkHideZeroQty.CheckStateChanged
        Call PrintStatus(False)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
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

        Report1.Reset()
        mTitle = "Item Wise - Bill Wise Detail"

        If Trim(cboDivision.Text) <> "ALL" Then
            mTitle = mTitle & "[" & cboDivision.Text & "]"
        End If

        If optDate(0).Checked = True Then
            mSubTitle = "MRR Date "
        Else
            mSubTitle = "VDate "
        End If

        mSubTitle = mSubTitle & "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text & " As On : " & txtAsOn.Text

        If optType(0).Checked = True Then
            mTitle = mTitle & "-Detailed"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemWiseBillWise.RPT"
        Else
            mTitle = mTitle & "-Summarised"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemWiseBillWiseSumm.RPT"
        End If

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, ColDiffAmount, PubDBCn) = False Then GoTo ReportErr
        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        '    SqlStr = MakeSQL
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

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchItem.Click
        SearchItem()
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim SqlStr As String

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Call CalcSprdTotal()
        Call PrintStatus(True)

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_DNCN_PROCESS WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmItemWiseBillWise_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Item Wise - Bill Wise Detail"

        If lblBookType.Text = "S" Then
            Me.Text = Me.Text & " (Supplier)"
        Else
            Me.Text = Me.Text & " (Customer)"
        End If

        Call FillInvoiceType()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmItemWiseBillWise_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

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

        lblTrnType.Text = CStr(-1)

        cboShow.Items.Clear()
        cboShow.Items.Add("ALL")
        cboShow.Items.Add("Decrease Rate")
        cboShow.Items.Add("Increase rate")
        cboShow.SelectedIndex = 0

        cboShowAgt.Items.Clear()
        cboShowAgt.Items.Add("Purchase")
        cboShowAgt.Items.Add("Return")
        cboShowAgt.Items.Add("RGP")
        cboShowAgt.Items.Add("Others")
        cboShowAgt.SelectedIndex = 0

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

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        txtItemName.Enabled = False
        cmdsearchItem.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtAsOn.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        Call FormatSprdMain(-1)
        Call frmItemWiseBillWise_Activated(eventSender, eventArgs) 'frmItemWiseBillWise
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer

        lstInvoiceType.Items.Clear()
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lblBookType.Text = "S" Then
            SqlStr = SqlStr & vbCrLf & " AND CATEGORY='P'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND CATEGORY='S'"
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

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
    Private Sub frmItemWiseBillWise_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmItemWiseBillWise_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub txtAsOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAsOn.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtAsOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAsOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtAsOn) = False Then
            txtAsOn.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtAsOn.Text)) = False Then
        '        txtAsOn.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged
        Call PrintStatus(False)
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
            lblAcCode.Text = AcName1
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
            lblItemCode.Text = AcName1
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

        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
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
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 7)
            .ColHidden = IIf(optType(0).Checked = True, True, False)

            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyCode, 30)
            If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 25)
            .ColsFrozen = ColItemName

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDate, 8)
            If lblBookType.Text = "S" Then
                .ColHidden = IIf(optType(1).Checked = True, True, False)
            Else
                .ColHidden = True
            End If

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 6)
            If lblBookType.Text = "S" Then
                .ColHidden = IIf(optType(1).Checked = True, True, False)
            Else
                .ColHidden = True
            End If

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 8)
            .ColHidden = IIf(optType(1).Checked = True, True, False)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 6)
            .ColHidden = IIf(optType(1).Checked = True, True, False)


            For cntCol = ColQuantity To ColNetDiffAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, IIf(optType(0).Checked = True, 7, 9))
            Next

            For cntCol = ColFyear To ColMKEY
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(cntCol, 8)
                .ColHidden = True
            Next

            For cntCol = ColTDSPer To ColESIPer
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 7)
                .ColHidden = IIf(lblBookType.Text = "S", False, True)
            Next

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(cntCol, 15)
            .ColHidden = False

            .Col = ColCustomerAmendNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(cntCol, 4)
            .ColHidden = IIf(lblBookType.Text = "C", False, True)

            .Col = ColItemPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(cntCol, 12)
            .ColHidden = False

            .Col = ColItemSNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(cntCol, 8)
            .ColHidden = False

            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(cntCol, 8)
            .ColHidden = False

            .Col = ColVendorCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(cntCol, 8)
            .ColHidden = False

            .Col = ColGRNNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(cntCol, 8)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)

            'For cntCol = ColCGST To ColIGST
            '    .Col = cntCol
            '    .CellType = SS_CELL_TYPE_FLOAT
            '    .TypeFloatDecimalPlaces = 3
            '    .TypeFloatMin = CDbl("-99999999999")
            '    .TypeFloatMax = CDbl("99999999999")
            '    .TypeFloatMoney = False
            '    .TypeFloatSeparator = False
            '    .TypeFloatDecimalChar = Asc(".")
            '    .TypeFloatSepChar = Asc(",")
            '    .set_ColWidth(cntCol, 7)
            '    .ColHidden = IIf(lblBookType.Text = "S", True, False)
            'Next

            .Col = ColPurchaseHead
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(cntCol, 20)
            .ColHidden = IIf(lblBookType.Text = "S", False, True)


            .Col = ColSuppBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(cntCol, 20)
            .ColHidden = IIf(lblBookType.Text = "C", IIf(chkSuppBillDetails.Checked = True, False, True), True)

            .Col = ColSuppBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(cntCol, 20)
            .ColHidden = IIf(lblBookType.Text = "C", IIf(chkSuppBillDetails.Checked = True, False, True), True)

            Call FillHeading()

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset
        Dim mData As Double
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim mMRRDate As String
        Dim mBillDate As String
        Dim mCustRefNo As String
        Dim mItemCode As String
        Dim mSuppCode As String
        Dim mFYear As Integer
        Dim mMRRNO As String
        Dim mBillNo As String
        Dim mMKey As String
        Dim mVNO As String
        Dim mVDate As String
        Dim mValue As Double
        Dim mAmount As Double
        Dim mRejQty As Double
        Dim mNetBasicAmount As Double


        Dim mNetRate As Double
        Dim mPurRate As Double
        Dim mDNCNRate As Double
        Dim mSuppRate As Double
        Dim mPORate As Double
        Dim mDiffRate As Double
        Dim mAcctQuantity As Double

        Dim mSuppBillNo As String
        Dim mSuppBillDate As String


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblBookType.Text = "S" Then
            If InsertIntoTemp = False Then GoTo LedgError
            SqlStr = MakeSQL_S
            '        Sqlstr = MakeSQL_S_Old
        Else
            SqlStr = MakeSQL_C
        End If
        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1
        With SprdMain
            Do While RsTemp.EOF = False
                .Row = cntRow
                .Col = ColItemCode
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                .Col = ColPartyCode
                .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                .Col = ColItemName
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_NAME").Value), "", RsTemp.Fields("ITEM_NAME").Value)



                .Col = ColQuantity
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("BILLQTY").Value), "0", RsTemp.Fields("BILLQTY").Value), "0.00")
                mRejQty = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLQTY").Value), "0", RsTemp.Fields("BILLQTY").Value), "0.00")

                .Col = ColAcctQuantity
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ACCPETED").Value), "0", RsTemp.Fields("ACCPETED").Value), "0.00")
                'mAmount = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ACCPETED").Value), "0", RsTemp.Fields("ACCPETED").Value), "0.00")
                mRejQty = mRejQty - VB6.Format(IIf(IsDBNull(RsTemp.Fields("ACCPETED").Value), "0", RsTemp.Fields("ACCPETED").Value), "0.00")

                .Col = ColRejQuantity
                .Text = mRejQty

                .Col = ColRate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("Rate").Value), "0", RsTemp.Fields("Rate").Value), "0.00")
                'mAmount = mAmount * VB6.Format(IIf(IsDBNull(RsTemp.Fields("Rate").Value), "0", RsTemp.Fields("Rate").Value), "0.00")

                .Col = ColDNCNRate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("DNCN_RATE").Value), "0", RsTemp.Fields("DNCN_RATE").Value), "0.00")

                .Col = ColSuppRate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SUPP_RATE").Value), "0", RsTemp.Fields("SUPP_RATE").Value), "0.00")

                .Col = ColPORate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("PORATE").Value), "0", RsTemp.Fields("PORATE").Value), "0.00")


                If optType(0).Checked = True Then
                    .Col = ColVNo
                    .Text = IIf(IsDbNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)

                    .Col = ColVDate
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")

                    .Col = ColBillNo
                    .Text = IIf(IsDbNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                    mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)

                    .Col = ColBillDate
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")
                    mBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")

                    .Col = ColFyear
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("FYEAR").Value), "0", RsTemp.Fields("FYEAR").Value), "0000")

                    .Col = ColMRRDate
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("MRRDATE").Value), "", RsTemp.Fields("MRRDATE").Value), "DD/MM/YYYY")

                    .Col = ColCustRefNo
                    .Text = IIf(IsDbNull(RsTemp.Fields("CUST_REF_NO").Value), "", RsTemp.Fields("CUST_REF_NO").Value)

                    .Col = ColMRRNo
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_MRR").Value), "0", RsTemp.Fields("AUTO_KEY_MRR").Value), "0.00")

                    .Col = ColTDSPer
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TDSPER").Value), "0", RsTemp.Fields("TDSPER").Value), "0.00")

                    .Col = ColESIPer
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ESIPER").Value), "0", RsTemp.Fields("ESIPER").Value), "0.00")

                    .Col = ColItemPartNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)

                    .Col = ColItemSNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SNO").Value), "", RsTemp.Fields("ITEM_SNO").Value)

                    .Col = ColHSNCode
                    .Text = IIf(IsDBNull(RsTemp.Fields("HSNCODE").Value), "", RsTemp.Fields("HSNCODE").Value)

                    .Col = ColVendorCode
                    .Text = IIf(IsDBNull(RsTemp.Fields("VENDOR_CODE").Value), "", RsTemp.Fields("VENDOR_CODE").Value)

                    .Col = ColGRNNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("GRNNO").Value), "", RsTemp.Fields("GRNNO").Value)



                    .Col = ColPONo
                    .Text = IIf(IsDBNull(RsTemp.Fields("CUST_PO_NO").Value), "", RsTemp.Fields("CUST_PO_NO").Value)

                    .Col = ColCustomerAmendNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("CUST_AMEND_NO").Value), "", RsTemp.Fields("CUST_AMEND_NO").Value)

                    .Col = ColPurchaseHead
                    .Text = IIf(IsDbNull(RsTemp.Fields("INVOICE_HEAD").Value), "", RsTemp.Fields("INVOICE_HEAD").Value)

                    .Col = ColMKEY
                    .Text = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)

                    If lblBookType.Text = "C" And chkSuppBillDetails.Checked = True Then
                        mSuppBillNo = ""
                        mSuppBillDate = ""

                        If GetSuppBillDetails(mBillNo, mBillDate, mSuppBillNo, mSuppBillDate, mItemCode) = False Then GoTo LedgError

                        .Col = ColSuppBillNo
                        .Text = mSuppBillNo

                        .Col = ColSuppBillDate
                        .Text = mSuppBillDate





                    End If
                End If

                If lblBookType.Text = "C" And optType(1).Checked = True Then

                    .Col = ColItemPartNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)

                    .Col = ColItemSNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SNO").Value), "", RsTemp.Fields("ITEM_SNO").Value)

                    .Col = ColHSNCode
                    .Text = IIf(IsDBNull(RsTemp.Fields("HSNCODE").Value), "", RsTemp.Fields("HSNCODE").Value)

                    .Col = ColVendorCode
                    .Text = IIf(IsDBNull(RsTemp.Fields("VENDOR_CODE").Value), "", RsTemp.Fields("VENDOR_CODE").Value)

                    .Col = ColPONo
                    .Text = IIf(IsDBNull(RsTemp.Fields("CUST_PO_NO").Value), "", RsTemp.Fields("CUST_PO_NO").Value)

                    .Col = ColCustomerAmendNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("CUST_AMEND_NO").Value), "", RsTemp.Fields("CUST_AMEND_NO").Value)
                End If

                .Col = ColAcctQuantity
                mAcctQuantity = Val(.Text)

                .Col = ColRate
                mPurRate = Val(.Text)

                .Col = ColDNCNRate
                mDNCNRate = Val(.Text)

                .Col = ColSuppRate
                mSuppRate = Val(.Text)

                .Col = ColNetRate
                mNetRate = CDbl(VB6.Format(mPurRate - mDNCNRate + mSuppRate, "0.000"))
                .Text = VB6.Format(mPurRate - mDNCNRate + mSuppRate, "0.000")

                .Col = ColPORate
                mPORate = Val(.Text)

                .Col = ColDiff
                mDiffRate = mPORate - mNetRate
                .Text = VB6.Format(mDiffRate, "0.000")

                .Col = ColDiffAmount
                .Text = VB6.Format(mDiffRate * mAcctQuantity, "0.000")
                mAmount = VB6.Format(mDiffRate * mAcctQuantity, "0.000")
                mNetBasicAmount = mAmount

                .Col = ColCGST
                mValue = mAmount * VB6.Format(IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value), "0.00") / 100
                mNetBasicAmount = mNetBasicAmount + mValue
                .Text = VB6.Format(mValue, "0.000")

                .Col = ColSGST
                mValue = mAmount * VB6.Format(IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value), "0.00") / 100
                mNetBasicAmount = mNetBasicAmount + mValue
                .Text = VB6.Format(mValue, "0.000")

                .Col = ColIGST
                mValue = mAmount * VB6.Format(IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value), "0.00") / 100
                mNetBasicAmount = mNetBasicAmount + mValue
                .Text = VB6.Format(mValue, "0.000")

                .Col = ColNetDiffAmount
                .Text = VB6.Format(mNetBasicAmount, "0.000")

                If lblBookType.Text = "C" And chkSuppBillDetails.Checked = True Then

                End If

                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End If
            Loop
        End With


        '''********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Function GetSuppBillDetails(ByRef mBillNo As String, ByRef mBillDate As String, ByRef mSuppBillNo As String, ByRef mSuppBillDate As String, ByRef mItemCode As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim pInvoiceAutoKey As Double

        GetSuppBillDetails = False

        If MainClass.ValidateWithMasterTable(mBillNo, "BILLNO", "AUTO_KEY_INVOICE", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pInvoiceAutoKey = Val(IIf(IsDBNull(MasterNo), -1, MasterNo))
        End If


        SqlStr = "SELECT DISTINCT IH.BILLNO, IH.INVOICE_DATE" & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID " & vbCrLf _
             & " WHERE IH.MKEY=ID.MKEY "

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INVOICESEQTYPE=9"

        SqlStr = SqlStr & vbCrLf _
        & " AND  ID.OUR_REF_NO=" & pInvoiceAutoKey & "" & vbCrLf _
        & " AND  ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                mSuppBillNo = IIf(mSuppBillNo = "", "", mSuppBillNo & ",")
                mSuppBillDate = IIf(mSuppBillDate = "", "", mSuppBillDate & ",")

                mSuppBillNo = mSuppBillNo & IIf(IsDBNull(RS.Fields("BILLNO").Value), "", RS.Fields("BILLNO").Value)
                mSuppBillDate = mSuppBillDate & VB6.Format(IIf(IsDBNull(RS.Fields("INVOICE_DATE").Value), "", RS.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")

                RS.MoveNext()
            Loop
        End If

        GetSuppBillDetails = True
        Exit Function
ErrPart:
        GetSuppBillDetails = False
    End Function
    Private Function InsertIntoTemp() As Boolean

        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mItemCode As String
        Dim mSuppCustCode As String
        Dim mDivisionCode As Double
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAllTrnType As Boolean
        Dim mFyearFrom As Integer
        Dim mFyearTo As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mFyearFrom = GetCurrentFYNo(PubDBCn, VB6.Format(txtDateFrom.Text, "DD/MM/YYYY"))
        mFyearTo = GetCurrentFYNo(PubDBCn, VB6.Format(txtDateTo.Text, "DD/MM/YYYY"))

        SqlStr = "DELETE FROM TEMP_DNCN_PROCESS WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " INSERT INTO TEMP_DNCN_PROCESS ( " & vbCrLf & " USER_ID, COMPANY_CODE, FYEAR, " & vbCrLf & " ITEM_CODE, SUPP_CUST_CODE, SUPP_CUST_NAME, " & vbCrLf & " ITEM_NAME, VNO, VDATE, " & vbCrLf & " BILLNO, BILLDATE, " & vbCrLf & " AUTO_KEY_MRR, MRRDATE, " & vbCrLf & " BILLQTY, RECDQTY, REOFFERQTY, RATE, " & vbCrLf & " DNCNRATE, SUPPRATE, NETRATE, " & vbCrLf & " CUST_REF_NO, CUST_REF_DATE, PORATE, " & vbCrLf & " MKEY,TDSPER,ESIPER,INVOICE_HEAD,SERIAL_NO) "

        SqlStr = SqlStr & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', IH.COMPANY_CODE, IH.FYEAR, " & vbCrLf & " ID.ITEM_CODE, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " ID.ITEM_DESC, IH.VNO, IH.VDATE, " & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf & " IH.AUTO_KEY_MRR, IH.MRRDATE, " & vbCrLf & " ID.ITEM_QTY, (NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0)), 0, ID.ITEM_RATE, " & vbCrLf & " 0, 0, 0, " & vbCrLf & " ID.CUST_REF_NO, ID.CUST_REF_DATE, 0, " & vbCrLf & " IH.MKEY,TDSPER,ESIPER,ITYPE.NAME,ID.SUBROWNO"

        'GETREOFFERQTY_NEW (IH.COMPANY_CODE, IH.AUTO_KEY_MRR, IH.MRRDATE, IH.SUPP_CUST_CODE,ID.ITEM_CODE,ID.CUST_REF_NO)

        ''''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, " & vbCrLf & " INV_GATE_HDR GH, FIN_SUPP_CUST_MST CMST, FIN_INVTYPE_MST ITYPE"

        ''''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.FYEAR>=" & mFyearFrom & " AND IH.FYEAR<=" & mFyearTo & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND TRIM(IH.SUPP_CUST_CODE)=TRIM(CMST.SUPP_CUST_CODE)"

        ''ONLY CHECK PO....15-03-2008
        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=GH.COMPANY_CODE" & vbCrLf & " AND TRIM(IH.SUPP_CUST_CODE)=TRIM(GH.SUPP_CUST_CODE)" & vbCrLf & " AND IH.AUTO_KEY_MRR=GH.AUTO_KEY_MRR "

        If Trim(txtBillNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BILLNO='" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'"
        End If

        '    Sqlstr = Sqlstr & vbCrLf & " AND ITEM_CODE IN ('RM0051')" '' ('R80051','R00056','R00237','R00238','R80072','R00564','R00297','R00428','R00700','R00701','R80071','R00565','R00297','R00700','R00052','R00004','R00049','R00057','R00016','R00060','R00017','R00058','R00030','R00023','R00296','R00003','R00006','R00019','R00029','R00274')"
        SqlStr = SqlStr & vbCrLf & " AND ID.COMPANY_CODE=ITYPE.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_TRNTYPE=ITYPE.CODE "

        If cboShowAgt.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE='P'"
        ElseIf cboShowAgt.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE='I'"
        ElseIf cboShowAgt.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE='R'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE NOT IN ('P','I','R')"
        End If

        mAllTrnType = True

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
            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_TRNTYPE IN " & mTrnTypeStr & ""
        End If

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCustCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'"
            End If
        End If

        '    Sqlstr = Sqlstr & vbCrLf & "AND CMST.SUPP_CUST_NAME LIKE 'D%'"

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.AUTO_KEY_MRR<>-1 AND IH.TRNTYPE>0 AND IH.VNO<>'-1' AND IH.ISFINALPOST='Y' AND CANCELLED='N'"

        If optDate(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MRRDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.MRRDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf optDate(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        PubDBCn.Execute(SqlStr)

        SqlStr = "UPDATE TEMP_DNCN_PROCESS SET " & vbCrLf
        If cboShowAgt.SelectedIndex = 0 Then
            If optDate(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMPRICE_NEW(FYEAR, " & RsCompany.Fields("FYEAR").Value & ", MRRDATE,CUST_REF_NO,ITEM_CODE) "
            Else
                SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMPRICE_NEW(FYEAR, " & RsCompany.Fields("FYEAR").Value & ", BILLDATE,CUST_REF_NO,ITEM_CODE) "
            End If
        ElseIf cboShowAgt.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " PORATE=GetSALEITEMPRICE(-1,CUST_REF_NO, SUPP_CUST_CODE,ITEM_CODE) "
        ElseIf cboShowAgt.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMJWRate(" & RsCompany.Fields("COMPANY_CODE").Value & ",1,MRRDATE,CUST_REF_NO,AUTO_KEY_MRR,ITEM_CODE,SERIAL_NO) "
            ''GetITEMJWRate(AUTO_KEY_MRR,ITEM_CODE,SERIAL_NO)
        Else
            SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMPRICE_NEW(FYEAR, " & RsCompany.Fields("FYEAR").Value & ", BILLDATE,-1,ITEM_CODE) "
        End If

        SqlStr = SqlStr & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = "UPDATE TEMP_DNCN_PROCESS SET " & vbCrLf & " REOFFERQTY=GETREOFFERQTY(COMPANY_CODE,AUTO_KEY_MRR,MRRDATE,SUPP_CUST_CODE,ITEM_CODE), " & vbCrLf & " RECDQTY=RECDQTY - GETLINEREJECTIONQTY(COMPANY_CODE,AUTO_KEY_MRR,MRRDATE,SUPP_CUST_CODE,ITEM_CODE)" & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        PubDBCn.Execute(SqlStr)
        ''mCompanyCode NUMBER, mMRRNo Number, mMRRDate Char, mSupplierCode CHAR, mItemCode CHAR

        If cboShowAgt.SelectedIndex <> 3 Then
            SqlStr = "UPDATE TEMP_DNCN_PROCESS SET " & vbCrLf & " DNCNRATE=(NVL(GETDNCNRATE_ASON(COMPANY_CODE, FYEAR, SUPP_CUST_CODE, BILLNO, BILLDATE, ITEM_CODE,'R',CUST_REF_NO,TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')),0)) " & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = "UPDATE TEMP_DNCN_PROCESS SET " & vbCrLf & " SUPPRATE=(NVL(GETSUPPRATE_ASON(COMPANY_CODE, FYEAR, MKEY, SUPP_CUST_CODE, VNO, VDATE, ITEM_CODE,'R',TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')),0)) " & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

            PubDBCn.Execute(SqlStr)
        End If

        PubDBCn.CommitTrans()

        '    PubDBCn.Errors.Clear
        '    PubDBCn.BeginTrans

        '    SqlStr = "UPDATE TEMP_DNCN_PROCESS SET " & vbCrLf _
        ''            & " REOFFERQTY=GETREOFFERQTY(COMPANY_CODE,AUTO_KEY_MRR,MRRDATE,SUPP_CUST_CODE,ITEM_CODE), " & vbCrLf _
        ''            & " DNCNRATE=(NVL(GETDNCNRATE(COMPANY_CODE, FYEAR, SUPP_CUST_CODE, BILLNO, BILLDATE, ITEM_CODE,'R'),0)), " & vbCrLf _
        ''            & " SUPPRATE=(NVL(GETSUPPRATE(COMPANY_CODE, FYEAR, MKEY, SUPP_CUST_CODE, VNO, VDATE, ITEM_CODE,'R'),0)), "
        '
        '    If cboShowAgt.ListIndex = 0 Then
        '        If optDate(0).Value = True Then
        '            SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMPRICE_NEW(FYEAR, " & RsCompany.fields("FYEAR").value & ", MRRDATE,CUST_REF_NO,ITEM_CODE) "
        '        Else
        '            SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMPRICE_NEW(FYEAR, " & RsCompany.fields("FYEAR").value & ", BILLDATE,CUST_REF_NO,ITEM_CODE) "
        '        End If
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " PORATE=GetSALEITEMPRICE(-1,CUST_REF_NO, SUPP_CUST_CODE,ITEM_CODE) "
        '    End If
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        '
        '    PubDBCn.Execute SqlStr

        '    PubDBCn.CommitTrans
        InsertIntoTemp = True
        Exit Function
LedgError:
        'Resume
        InsertIntoTemp = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Function

    Private Sub FillHeading()

        With SprdMain
            .Row = 0

            .Col = ColItemCode
            .Text = "Item Code"

            .Col = ColPartyCode
            .Text = "Account Name"

            .Col = ColItemName
            .Text = "Item Name"

            .Col = ColVNo
            .Text = "VNo"

            .Col = ColVDate
            .Text = "VDate"

            .Col = ColBillNo
            .Text = "Bill No."

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColQuantity
            .Text = "Bill Qty"

            .Col = ColAcctQuantity
            .Text = "Accepted Qty"

            .Col = ColRate
            .Text = IIf(optType(0).Checked = True, "Rate (Rs)", "Amount (Rs)")

            .Col = ColDNCNRate
            .Text = IIf(optType(0).Checked = True, "DN/CN Rate (Rs)", "DN/CN Amount (Rs)")

            .Col = ColSuppRate
            .Text = IIf(optType(0).Checked = True, "Supp. Rate (Rs)", "Supp. Amount (Rs)")

            .Col = ColNetRate
            .Text = IIf(optType(0).Checked = True, "Net Rate (Rs)", "Net Amount (Rs)")

            .Col = ColPORate
            .Text = IIf(optType(0).Checked = True, "PO Rate (Rs)", "PO Amount (Rs)")

            .Col = ColDiff
            .Text = "Basic Rate Diff"

            .Col = ColDiffAmount
            .Text = "Basic Diff Amount"

            .Col = ColRejQuantity
            .Text = "Rejection Qty"

            .Col = ColNetDiffAmount
            .Text = "Net Diff Amount"

            .Col = ColMKEY
            .Text = "MKey"

            .Col = ColFyear
            .Text = "Year"

            .Col = ColMRRDate
            .Text = "MRR Date"

            .Col = ColCustRefNo
            .Text = "Cust Ref No"

            .Col = ColMRRNo
            .Text = "MRR No"

            .Col = ColTDSPer
            .Text = "TDS %"

            .Col = ColESIPer
            .Text = "ESI %"

            .Col = ColPurchaseHead
            .Text = "Purchase Head Desc"

            .Col = ColPONo
            .Text = "PO No"

            .Col = ColCustomerAmendNo
            .Text = "Customer Amend No"

            .Col = ColItemPartNo
            .Text = "Part No"

            .Col = ColItemSNo
            .Text = "Item Serial No"

            .Col = ColHSNCode
            .Text = "HSN Code"

            .Col = ColVendorCode
            .Text = "Vendor Code"

            .Col = ColGRNNo
            .Text = "GRN No"

            .Col = ColCGST
            .Text = "CGST Amount"

            .Col = ColSGST
            .Text = "SGST Amount"

            .Col = ColIGST
            .Text = "IGST Amount"

            .Col = ColSuppBillNo
            .Text = "Supp Bill Nos"

            .Col = ColSuppBillDate
            .Text = "Supp Bill Dates"

        End With
    End Sub
    Private Function MakeSQL_S_Old() As String

        On Error GoTo ERR1

        ''''SELECT CLAUSE...

        ''& " NVL(GETCNFROMDN(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE),0)) AS DNCN_RATE," & vbCrLf _
        '''GETREOFFERQTY (mCompanyCode NUMBER, mMRRNo Number, mMRRDate Char, mSupplierCode CHAR, mItemCode CHAR)

        '

        If optType(0).Checked = True Then
            MakeSQL_S_Old = " SELECT ID.ITEM_CODE,CMST.SUPP_CUST_NAME, ID.ITEM_DESC, " & vbCrLf & " IH.VNO,IH.VDATE, IH.BILLNO, IH.INVOICE_DATE," & vbCrLf & " ID.ITEM_QTY, " & vbCrLf & " TO_CHAR(NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0)+ " & vbCrLf & " GETREOFFERQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.AUTO_KEY_MRR,IH.MRRDATE,IH.SUPP_CUST_CODE,ID.ITEM_CODE)) AS ACCPETED, " & vbCrLf & " ID.ITEM_RATE, " & vbCrLf & " TO_CHAR(NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0)) AS DNCN_RATE,  " & vbCrLf & " TO_CHAR(NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0)) AS SUPP_RATE, " & vbCrLf & " '0.000', " & vbCrLf & " TO_CHAR(CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)='S' THEN GetSALEITEMPRICE(-1,ID.CUST_REF_NO, IH.SUPP_CUST_CODE,ITEM_CODE) ELSE GetITEMPRICE_NEW(IH.FYEAR, " & RsCompany.Fields("FYEAR").Value & ", IH.INVOICE_DATE,ID.CUST_REF_NO,ID.ITEM_CODE) END) AS PORATE," & vbCrLf & " '0.000', IH.MKEY "
        Else
            MakeSQL_S_Old = " SELECT ID.ITEM_CODE,CMST.SUPP_CUST_NAME, ID.ITEM_DESC, " & vbCrLf & " '','', '', ''," & vbCrLf & " SUM(ID.ITEM_QTY), " & vbCrLf & " TO_CHAR(SUM(NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0)+ " & vbCrLf & " GETREOFFERQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.AUTO_KEY_MRR,IH.MRRDATE,IH.SUPP_CUST_CODE,ID.ITEM_CODE))) AS ACCPETED, " & vbCrLf & " SUM(ID.ITEM_AMT), " & vbCrLf & " TO_CHAR(SUM(NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'A',ID.CUST_REF_NO),0))) AS DNCN_RATE,  " & vbCrLf & " TO_CHAR(SUM(NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'A'),0))) AS SUPP_RATE, " & vbCrLf & " '0.000', " & vbCrLf & " TO_CHAR(SUM(CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)='S' THEN GetSALEITEMPRICE(-1,ID.CUST_REF_NO, IH.SUPP_CUST_CODE,ITEM_CODE) ELSE GetITEMPRICE_NEW(IH.FYEAR, " & RsCompany.Fields("FYEAR").Value & ", IH.INVOICE_DATE,ID.CUST_REF_NO,ID.ITEM_CODE) END * ID.ITEM_QTY)) AS PORATE," & vbCrLf & " '0.000', '' "
        End If

        ''''FROM CLAUSE...
        MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & " FROM TEMP_FIN_PURCHASE_HDR IH, TEMP_FIN_PURCHASE_DET ID, " & vbCrLf & " PUR_PURCHASE_HDR PH, FIN_SUPP_CUST_MST CMST"

        ''''WHERE CLAUSE...


        If cboShow.SelectedIndex = 1 Then
            MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & " WHERE CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)<>'S' THEN GetITEMPRICE_NEW(IH.FYEAR, " & RsCompany.Fields("FYEAR").Value & ", IH.INVOICE_DATE,ID.CUST_REF_NO,ID.ITEM_CODE) ELSE 0 END< " & vbCrLf & " (ID.ITEM_RATE - NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0) " & vbCrLf & " + NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0)) "

            ''& " + NVL(GETCNFROMDN(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE),0)) "

        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & " AND CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)<>'S' THEN GetITEMPRICE_NEW(IH.FYEAR, " & RsCompany.Fields("FYEAR").Value & ", IH.INVOICE_DATE,ID.CUST_REF_NO,ID.ITEM_CODE) ELSE 0 END >  " & vbCrLf & " (ID.ITEM_RATE -NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0) " & vbCrLf & " + NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0)) "

            ''& " + NVL(GETCNFROMDN(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE),0)) "
        End If


        If chkHideZeroPORate.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "AND CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)='S' THEN 0 ELSE GetITEMPRICE_NEW(IH.FYEAR, " & RsCompany.Fields("FYEAR").Value & ", IH.INVOICE_DATE,ID.CUST_REF_NO,ID.ITEM_CODE) END>0"
        End If

        If chkHideZeroQty.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "AND NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0) + GETREOFFERQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.AUTO_KEY_MRR,IH.MRRDATE,IH.SUPP_CUST_CODE,ID.ITEM_CODE)<>0"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "AND CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If


        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(lblItemCode.Text) & "'"
        End If


        MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "AND IH.VNO<>'-1' AND IH.ISFINALPOST='Y' AND CANCELLED='N'"

        '    MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "AND CMST.SUPP_CUST_NAME LIKE 'G%'"

        MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & " AND " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" '''06-05-2006

        ''ONLY CHECK PO....15-03-2008
        MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & " AND ID.COMPANY_CODE=PH.COMPANY_CODE" & vbCrLf & " AND ID.CUST_REF_NO=PH.AUTO_KEY_PO AND PH.PUR_TYPE='P' AND PH.ORDER_TYPE='O'"

        '     MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=MRR.COMPANY_CODE" & vbCrLf _
        ''            & " AND IH.AUTO_KEY_MRR=MRR.AUTO_KEY_MRR"

        If optDate(0).Checked = True Then
            MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & " AND IH.MRRDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.MRRDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf optDate(1).Checked = True Then
            MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        ''GROUP BY CLAUSE
        If optType(1).Checked = True Then
            MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & " GROUP BY ID.ITEM_CODE , CMST.SUPP_CUST_NAME, ID.ITEM_DESC"
        End If

        ''''ORDER CLAUSE...
        If optType(0).Checked = True Then
            If OptOrderBy(0).Checked = True Then
                MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,ID.ITEM_DESC,IH.BILLNO, IH.INVOICE_DATE"
            ElseIf OptOrderBy(1).Checked = True Then
                MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "ORDER BY IH.MRRDATE,IH.AUTO_KEY_MRR,ID.ITEM_DESC, CMST.SUPP_CUST_NAME"
            ElseIf OptOrderBy(2).Checked = True Then
                MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "ORDER BY IH.VDATE,IH.VNO,ID.ITEM_DESC, CMST.SUPP_CUST_NAME"
            ElseIf OptOrderBy(3).Checked = True Then
                MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "ORDER BY IH.INVOICE_DATE,IH.BILLNO,ID.ITEM_DESC, CMST.SUPP_CUST_NAME"
            End If
        Else
            If OptOrderBy(0).Checked = True Then
                MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,ID.ITEM_DESC"
            ElseIf OptOrderBy(1).Checked = True Then
                MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "ORDER BY ID.ITEM_DESC, CMST.SUPP_CUST_NAME"
            Else
                MakeSQL_S_Old = MakeSQL_S_Old & vbCrLf & "ORDER BY ID.ITEM_DESC, CMST.SUPP_CUST_NAME"
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQL_S() As String

        On Error GoTo ERR1

        ''''SELECT CLAUSE...

        If optType(0).Checked = True Then
            MakeSQL_S = " SELECT ITEM_CODE,SUPP_CUST_NAME, ITEM_NAME, " & vbCrLf _
                & " VNO,VDATE, BILLNO, BILLDATE," & vbCrLf _
                & " BILLQTY, " & vbCrLf _
                & " TO_CHAR(RECDQTY+REOFFERQTY) AS ACCPETED, " & vbCrLf _
                & " RATE, " & vbCrLf _
                & " TO_CHAR(DNCNRATE) AS DNCN_RATE,  " & vbCrLf _
                & " TO_CHAR(SUPPRATE) AS SUPP_RATE, " & vbCrLf _
                & " '0.000', " & vbCrLf _
                & " TO_CHAR(PORATE) AS PORATE," & vbCrLf _
                & " '0.000', FYEAR, MRRDATE, CUST_REF_NO, AUTO_KEY_MRR,TDSPER,ESIPER,INVOICE_HEAD,'' AS CUSTOMER_PART_NO, '' AS ITEM_SNO,'' AS  HSNCODE,'' AS VENDOR_CODE, '' AS GRNNO, 0 AS CGST_PER, 0 AS SGST_PER, 0 AS IGST_PER, '' AS CUST_PO_NO, MKEY, '' AS CUST_AMEND_NO "
        Else
            MakeSQL_S = " SELECT ITEM_CODE,SUPP_CUST_NAME, ITEM_NAME, " & vbCrLf & " '','', '', ''," & vbCrLf & " SUM(BILLQTY) AS BILLQTY, " & vbCrLf & " TO_CHAR(SUM(RECDQTY+REOFFERQTY)) AS ACCPETED, " & vbCrLf & " SUM(RATE*BILLQTY) As RATE, " & vbCrLf & " TO_CHAR(SUM(DNCNRATE)) AS DNCN_RATE,  " & vbCrLf _
                & " TO_CHAR(SUM(SUPPRATE)) AS SUPP_RATE, " & vbCrLf _
                & " '0.000', " & vbCrLf & " SUM(PORATE * BILLQTY) AS PORATE," & vbCrLf _
                & " '0.000', '','','','0','0','','','','','','',0 AS CGST_PER,0 AS SGST_PER,0 AS IGST_PER,'' AS CUST_AMEND_NO "
        End If


        ''''FROM CLAUSE...
        MakeSQL_S = MakeSQL_S & vbCrLf & " FROM TEMP_DNCN_PROCESS"

        ''''WHERE CLAUSE...

        MakeSQL_S = MakeSQL_S & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        If cboShow.SelectedIndex = 1 Then
            MakeSQL_S = MakeSQL_S & vbCrLf & " AND PORATE< (RATE - DNCNRATE + SUPPRATE) "
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL_S = MakeSQL_S & vbCrLf & " AND PORATE > (RATE - DNCNRATE + SUPPRATE) "
        End If


        If chkHideZeroPORate.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL_S = MakeSQL_S & vbCrLf & "AND PORATE>0"
        End If

        If chkHideZeroQty.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL_S = MakeSQL_S & vbCrLf & "AND RECDQTY+REOFFERQTY<>0"
        End If

        ''GROUP BY CLAUSE
        If optType(1).Checked = True Then
            MakeSQL_S = MakeSQL_S & vbCrLf & " GROUP BY ITEM_CODE , SUPP_CUST_NAME, ITEM_NAME"
        End If

        ''''ORDER CLAUSE...
        If optType(0).Checked = True Then
            If OptOrderBy(0).Checked = True Then
                MakeSQL_S = MakeSQL_S & vbCrLf & "ORDER BY SUPP_CUST_NAME,ITEM_NAME,BILLNO, BILLDATE"
            ElseIf OptOrderBy(1).Checked = True Then
                MakeSQL_S = MakeSQL_S & vbCrLf & "ORDER BY MRRDATE,AUTO_KEY_MRR,ITEM_NAME, SUPP_CUST_NAME"
            ElseIf OptOrderBy(2).Checked = True Then
                MakeSQL_S = MakeSQL_S & vbCrLf & "ORDER BY VDATE,VNO,ITEM_NAME, SUPP_CUST_NAME"
            ElseIf OptOrderBy(3).Checked = True Then
                MakeSQL_S = MakeSQL_S & vbCrLf & "ORDER BY BILLDATE,BILLNO,ITEM_NAME, SUPP_CUST_NAME"
            End If
        Else
            If OptOrderBy(0).Checked = True Then
                MakeSQL_S = MakeSQL_S & vbCrLf & "ORDER BY SUPP_CUST_NAME,ITEM_NAME"
            ElseIf OptOrderBy(1).Checked = True Then
                MakeSQL_S = MakeSQL_S & vbCrLf & "ORDER BY ITEM_NAME, SUPP_CUST_NAME"
            Else
                MakeSQL_S = MakeSQL_S & vbCrLf & "ORDER BY ITEM_NAME, SUPP_CUST_NAME"
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQL_C() As String

        On Error GoTo ERR1
        Dim mDivisionCode As Double
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAllTrnType As Boolean

        ''''SELECT CLAUSE...

        '      CREATE Or Replace() FUNCTION GETSALEREJECTIONQTY (
        '2  mCompanyCode NUMBER,mFYEAR NUMBER, mMKEY CHAR, mSUPPLIERCODE char, mPONO NUMBER, mItemCode CHAR)

        If optType(0).Checked = True Then
            MakeSQL_C = " SELECT ID.ITEM_CODE,CMST.SUPP_CUST_NAME, ID.ITEM_DESC AS ITEM_NAME, " & vbCrLf _
                & " IH.AUTO_KEY_INVOICE AS VNO,IH.INVOICE_DATE AS VDATE, IH.BILLNO, IH.INVOICE_DATE AS BILLDATE," & vbCrLf _
                & " SUM(ID.ITEM_QTY) As BILLQTY, SUM(ID.ITEM_QTY " & vbCrLf _
                & " - GETSALEREJECTIONQTY(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, IH.AUTO_KEY_INVOICE, ID.ITEM_CODE) " & vbCrLf _
                & " - GETSALESHORTAGEQTY(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE)) AS ACCPETED, " & vbCrLf _
                & " ID.ITEM_RATE As RATE, " & vbCrLf _
                & " TO_CHAR(GETSALEDEBITRATE(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE)) AS DNCN_RATE," & vbCrLf _
                & " TO_CHAR(GETSALESUPPBILLPRICE(IH.COMPANY_CODE, ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE)) AS SUPP_RATE, " & vbCrLf _
                & " '0.000', TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE)) AS PORATE, " & vbCrLf _
                & " '0.000',FYEAR,'' AS MRRDATE,'' AS CUST_REF_NO,'' AS AUTO_KEY_MRR,'' AS TDSPER,'' ESIPER, ID.CUSTOMER_PART_NO, " & vbCrLf _
                & " ID.ITEM_SNO, ID.HSNCODE, IH.VENDOR_CODE, IH.GRNNO, ID.CGST_PER,ID.SGST_PER,ID.IGST_PER, IH.CUST_PO_NO, " & vbCrLf _
                & " '' AS INVOICE_HEAD,IH.MKEY, GetSOCustomerAmendNo(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE) AS CUST_AMEND_NO "
        Else
            MakeSQL_C = " SELECT ID.ITEM_CODE,CMST.SUPP_CUST_NAME, ID.ITEM_DESC AS ITEM_NAME, " & vbCrLf _
                & " '' AS VNO,'' AS VDATE, '' BILLNO, '' AS BILLDATE," & vbCrLf _
                & " SUM(ID.ITEM_QTY) As BILLQTY, SUM(ID.ITEM_QTY " & vbCrLf _
                & " - GETSALEREJECTIONQTY(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, IH.AUTO_KEY_INVOICE, ID.ITEM_CODE) " & vbCrLf _
                & " - GETSALESHORTAGEQTY(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE)) AS ACCPETED, " & vbCrLf _
                & " ID.ITEM_RATE As RATE, " & vbCrLf _
                & " TO_CHAR(GETSALEDEBITRATE(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE)) AS DNCN_RATE," & vbCrLf _
                & " TO_CHAR(GETSALESUPPBILLPRICE(IH.COMPANY_CODE, ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE)) AS SUPP_RATE, " & vbCrLf _
                & " '0.000', TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE)) AS PORATE, " & vbCrLf _
                & " '0.000',FYEAR,'' AS MRRDATE,'' AS CUST_REF_NO,'' AS AUTO_KEY_MRR,'' AS TDSPER,'' ESIPER, ID.CUSTOMER_PART_NO, " & vbCrLf _
                & " '' ITEM_SNO, ID.HSNCODE, '' VENDOR_CODE, '' GRNNO, ID.CGST_PER,ID.SGST_PER,ID.IGST_PER, '' CUST_PO_NO, " & vbCrLf _
                & " '' AS INVOICE_HEAD,'' MKEY, GetSOCustomerAmendNo(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE) AS CUST_AMEND_NO "
            'MakeSQL_C = " SELECT ID.ITEM_CODE,CMST.SUPP_CUST_NAME, ID.ITEM_DESC As ITEM_NAME, " & vbCrLf _
            '    & " '','', '', ''," & vbCrLf _
            '    & " SUM(ID.ITEM_QTY) As BILLQTY, SUM(ID.ITEM_QTY " & vbCrLf _
            '    & " - GETSALEREJECTIONQTY(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, IH.AUTO_KEY_INVOICE, ID.ITEM_CODE) " & vbCrLf _
            '    & " - GETSALESHORTAGEQTY(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE)) AS ACCPETED, " & vbCrLf _
            '    & " ID.ITEM_RATE AS RATE, " & vbCrLf _
            '    & " TO_CHAR(GETSALEDEBITRATE(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE)) AS DNCN_RATE," & vbCrLf _
            '    & " TO_CHAR(GETSALESUPPBILLPRICE(IH.COMPANY_CODE, ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE)) AS SUPP_RATE, " & vbCrLf _
            '    & " '0.000', TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE)) AS PORATE, '0.000','0','0','','','','','','','',0 AS CGST_PER,0 AS SGST_PER,0 AS IGST_PER,'',IH.VENDOR_CODE, ID.CUSTOMER_PART_NO, IH.CUST_PO_NO, ID.ITEM_SNO,ID.HSNCODE, GetSOCustomerAmendNo(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE) AS CUST_AMEND_NO "
        End If



        ''GETSALEDEBITRATE (mCompanyCode NUMBER,mFYEAR NUMBER, mMKEY CHAR, mSUPPLIERCODE char, mItemCode CHAR)

        ''''FROM CLAUSE...
        MakeSQL_C = MakeSQL_C & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST"

        ''AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "

        ''''WHERE CLAUSE...
        MakeSQL_C = MakeSQL_C & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND" & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        If Trim(txtBillNo.Text) <> "" Then
            MakeSQL_C = MakeSQL_C & vbCrLf & " AND IH.BILLNO='" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'"
        End If

        If Trim(txtPONo.Text) <> "" Then
            MakeSQL_C = MakeSQL_C & vbCrLf & " AND IH.CUST_PO_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "'"
        End If

        If Trim(txtVendorCode.Text) <> "" Then
            MakeSQL_C = MakeSQL_C & vbCrLf & " AND IH.VENDOR_CODE='" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "'"
        End If

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            MakeSQL_C = MakeSQL_C & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL_C = MakeSQL_C & vbCrLf & "AND CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL_C = MakeSQL_C & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(lblItemCode.Text) & "'"
        End If

        mAllTrnType = True

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
            MakeSQL_C = MakeSQL_C & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
        End If

        MakeSQL_C = MakeSQL_C & vbCrLf & "AND CANCELLED='N' AND IH.REF_DESP_TYPE<>'U' AND AGTD3='N'"

        If cboShow.SelectedIndex = 1 Then
            MakeSQL_C = MakeSQL_C & vbCrLf & " AND GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE)< " & vbCrLf & " ID.ITEM_RATE + GETSALESUPPBILLPRICE(IH.COMPANY_CODE, ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE)-GETSALEDEBITRATE(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE)"

        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL_C = MakeSQL_C & vbCrLf & " AND GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE) >  " & vbCrLf & " ID.ITEM_RATE + GETSALESUPPBILLPRICE(IH.COMPANY_CODE, ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE)-GETSALEDEBITRATE(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE) "
        End If

        MakeSQL_C = MakeSQL_C & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If optType(0).Checked = True Then
            MakeSQL_C = MakeSQL_C & vbCrLf _
                & " GROUP BY ID.ITEM_CODE, CMST.SUPP_CUST_NAME, ID.ITEM_DESC, " & vbCrLf _
                & " IH.AUTO_KEY_INVOICE,IH.INVOICE_DATE, IH.BILLNO, IH.INVOICE_DATE," & vbCrLf _
                & " ID.ITEM_RATE, GetSOCustomerAmendNo(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE)," & vbCrLf _
                & " TO_CHAR(GETSALEDEBITRATE(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE))," & vbCrLf _
                & " TO_CHAR(GETSALESUPPBILLPRICE(IH.COMPANY_CODE, ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE)), " & vbCrLf _
                & " TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE)), " & vbCrLf _
                & " FYEAR,ID.CUSTOMER_PART_NO,ID.ITEM_SNO, ID.HSNCODE, IH.VENDOR_CODE,  IH.GRNNO, ID.CGST_PER,ID.SGST_PER,ID.IGST_PER, IH.CUST_PO_NO, IH.MKEY "

        Else
            MakeSQL_C = MakeSQL_C & vbCrLf _
                & " GROUP BY ID.ITEM_CODE,CMST.SUPP_CUST_NAME, ID.ITEM_DESC, " & vbCrLf _
                & " ID.ITEM_RATE, GetSOCustomerAmendNo(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE) ," & vbCrLf _
                & " TO_CHAR(GETSALEDEBITRATE(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE))," & vbCrLf _
                & " TO_CHAR(GETSALESUPPBILLPRICE(IH.COMPANY_CODE, ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE)), " & vbCrLf _
                & " TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE)), ID.CUSTOMER_PART_NO,  ID.HSNCODE,ID.CGST_PER,ID.SGST_PER,ID.IGST_PER,IH.FYEAR"
        End If


        ''''ORDER CLAUSE...

        If optType(0).Checked = True Then
            If optOrderBy(0).Checked = True Then
                MakeSQL_C = MakeSQL_C & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,ID.ITEM_DESC,IH.BILLNO, IH.INVOICE_DATE"
            ElseIf optOrderBy(1).Checked = True Then
                MakeSQL_C = MakeSQL_C & vbCrLf & "ORDER BY IH.INVOICE_DATE,IH.BILLNO,ID.ITEM_DESC, CMST.SUPP_CUST_NAME"
            ElseIf optOrderBy(2).Checked = True Then
                MakeSQL_C = MakeSQL_C & vbCrLf & "ORDER BY IH.INVOICE_DATE,IH.BILLNO,ID.ITEM_DESC, CMST.SUPP_CUST_NAME"
            ElseIf optOrderBy(3).Checked = True Then
                MakeSQL_C = MakeSQL_C & vbCrLf & "ORDER BY IH.INVOICE_DATE,IH.BILLNO,ID.ITEM_DESC, CMST.SUPP_CUST_NAME"
            End If
        Else
            MakeSQL_C = MakeSQL_C & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME,ID.ITEM_DESC"
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub CalcSprdTotal()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mPurRate As Double
        Dim mDNCNRate As Double
        Dim mSuppRate As Double
        Dim mNetRate As Double
        Dim mPORate As Double
        Dim mDiffRate As Double
        Dim mDelRow As Double
        Dim mAcctQuantity As Double
        Dim mAmount As Double

        mDelRow = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColAcctQuantity
                mAcctQuantity = Val(.Text)

                .Col = ColRate
                mPurRate = Val(.Text)

                .Col = ColDNCNRate
                mDNCNRate = Val(.Text)

                .Col = ColSuppRate
                mSuppRate = Val(.Text)

                .Col = ColNetRate
                mNetRate = CDbl(VB6.Format(mPurRate - mDNCNRate + mSuppRate, "0.000"))
                .Text = VB6.Format(mPurRate - mDNCNRate + mSuppRate, "0.000")

                .Col = ColPORate
                mPORate = Val(.Text)

                .Col = ColDiff
                mDiffRate = mPORate - mNetRate
                .Text = VB6.Format(mDiffRate, "0.000")

                .Col = ColDiffAmount
                .Text = VB6.Format(mDiffRate * mAcctQuantity, "0.000")
                mAmount = VB6.Format(mDiffRate * mAcctQuantity, "0.000")

                .Col = ColCGST
                mAmount = mAmount + Val(.Text)

                .Col = ColSGST
                mAmount = mAmount + Val(.Text)

                .Col = ColIGST
                mAmount = mAmount + Val(.Text)

                .Col = ColNetDiffAmount
                .Text = VB6.Format(mAmount, "0.000")


                '            If cboShow.ListIndex = 1 Then
                '                If mDiffRate >= 0 Then
                '                    .Row = cntRow
                '                    .Action = SS_ACTION_DELETE_ROW
                ''                    If .MaxRows > 1 Then .MaxRows = .MaxRows - 1
                '                    mDelRow = mDelRow + 1
                '                End If
                '            ElseIf cboShow.ListIndex = 2 Then
                '                 If mDiffRate <= 0 Then
                '                    .Row = cntRow
                '                    .Action = SS_ACTION_DELETE_ROW
                ''                    If .MaxRows > 1 Then .MaxRows = .MaxRows - 1
                '                    mDelRow = mDelRow + 1
                '                End If
                '            End If

            Next
            '        If .MaxRows > mDelRow Then .MaxRows = .MaxRows - mDelRow
        End With



        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
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


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColVDate
        xVDate = Me.SprdMain.Text

        SprdMain.Col = ColMKEY
        xMKey = Me.SprdMain.Text

        SprdMain.Col = IIf(lblBookType.Text = "S", ColVNo, ColBillNo)
        xVNo = Me.SprdMain.Text

        Call ShowTrn(xMKey, xVDate, "", xVNo, IIf(lblBookType.Text = "S", "P", "S"), "", Me)

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

        lblItemCode.Text = ""
        If txtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblItemCode.Text = MasterNo
            txtItemName.Text = UCase(Trim(txtItemName.Text))
        Else
            lblItemCode.Text = ""
            MsgInformation("No Such Item in Item Master")
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
