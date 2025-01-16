Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmItemWisePurchaseDetail
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String
    Private Const ColItemCode As Short = 1
    Private Const ColPartyCode As Short = 2
    Private Const ColItemName As Short = 3
    Private Const ColMRRNo As Short = 4
    Private Const ColVNo As Short = 5
    Private Const ColVDate As Short = 6
    Private Const ColBillNo As Short = 7
    Private Const ColBillDate As Short = 8
    Private Const ColQuantity As Short = 9
    Private Const ColRate As Short = 10
    Private Const ColPurchaseAmount As Short = 11
    Private Const ColPurExpAmount As Short = 12
    Private Const ColSuppAmount As Short = 13
    Private Const ColDNCNAmount As Short = 14
    Private Const CoPurchaseReturnQty As Short = 15
    Private Const CoPurchaseReturnAmount As Short = 16
    Private Const ColTotalAmount As Short = 17
    Private Const ColPurchaseHead As Short = 18
    Private Const ColMKEY As Short = 19
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
            TxtItemName.Enabled = False
            cmdsearchItem.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdsearchItem.Enabled = True
        End If
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
        Dim SqlStr As String = ""
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

        mSubTitle = mSubTitle & "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text

        If optType(0).Checked = True Then
            mTitle = mTitle & "-Detailed"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemWiseBillWise.RPT"
        Else
            mTitle = mTitle & "-Summarised"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemWiseBillWiseSumm.RPT"
        End If

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, ColMKEY, PubDBCn) = False Then GoTo ReportErr

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

    Private Sub cmdSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchItem.Click
        SearchItem()
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart

        FormatSprdMain(-1)
        Call CalcSprdTotal()
        Call PrintStatus(True)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmItemWisePurchaseDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Item Wise - Bill Wise Purchase Detail"

        Call FillInvoiceType()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmItemWisePurchaseDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

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
        '	
        '	
        '	
        '    cboShow.Clear	
        '    cboShow.AddItem "ALL"	
        '    cboShow.AddItem "Decrease Rate"	
        '    cboShow.AddItem "Increase rate"	
        '    cboShow.ListIndex = 0	
        '	
        '    cboShowAgt.Clear	
        '    cboShowAgt.AddItem "Purchase"	
        '    cboShowAgt.AddItem "Return"	
        '    cboShowAgt.AddItem "RGP"	
        '    cboShowAgt.AddItem "Others"	
        '    cboShowAgt.ListIndex = 0	

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
        TxtItemName.Enabled = False
        cmdsearchItem.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        Call FormatSprdMain(-1)
        Call frmItemWisePurchaseDetail_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        lstInvoiceType.Items.Clear()
        'SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        'SqlStr = SqlStr & vbCrLf & " AND CATEGORY='P'"

        'SqlStr = SqlStr & vbCrLf & " ORDER BY NAME"

        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        'CntLst = 0
        'If RS.EOF = False Then
        '    lstInvoiceType.Items.Add("ALL")
        '    Do While RS.EOF = False
        '        lstInvoiceType.Items.Add(RS.Fields("Name").Value)
        '        lstInvoiceType.SetItemChecked(CntLst, True)
        '        RS.MoveNext()
        '        CntLst = CntLst + 1
        '    Loop
        'End If

        SqlStr = "SELECT DISTINCT B.SUPP_CUST_NAME FROM FIN_INVTYPE_MST A, FIN_SUPP_CUST_MST B" & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.ACCOUNTPOSTCODE=B.SUPP_CUST_CODE" & vbCrLf _
            & " AND A.CATEGORY='P' ORDER BY SUPP_CUST_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstInvoiceType.Items.Add("ALL")
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("SUPP_CUST_NAME").Value)
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
    Private Sub frmItemWisePurchaseDetail_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmItemWisePurchaseDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
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
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr	
        MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
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
        Dim SqlStr As String = ""

        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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
            .set_ColWidth(ColPartyCode, 10)
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
            .set_ColWidth(ColItemName, 15)
            .ColsFrozen = ColItemName

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDate, 8)
            .ColHidden = IIf(optType(1).Checked = True, True, False)


            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 6)
            .ColHidden = IIf(optType(1).Checked = True, True, False)

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


            For cntCol = ColQuantity To ColTotalAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 12)
            Next

            For cntCol = ColMKEY To ColMKEY
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(cntCol, 8)
                .ColHidden = True
            Next


            .Col = ColPurchaseHead
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(cntCol, 20)
            .ColHidden = False

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
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mData As Double
        Dim SqlStr As String = ""
        Dim cntRow As Integer

        Dim mPurchaseAmount As Double
        Dim mPurExpAmount As Double
        Dim mSuppAmount As Double
        Dim mDNCNAmount As Double
        Dim mPurReturnAmount As Double
        Dim mTotalAmount As Double
        Dim mMRRNO As Double
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mItemCode As String
        Dim mPartyCode As String
        Dim mTRNTYPE As Double
        Dim mMkey As String

        Dim mTotPurchaseAmount As Double
        Dim mTotPurExpAmount As Double
        Dim mTotSuppAmount As Double
        Dim mTotDebitAmount As Double
        Dim mTotCreditAmount As Double
        Dim mGrandAmount As Double
        Dim mDNCNQty As Double
        Dim mReturnQty As Double
        Dim mAccountCode As String
        Dim mTotReturnQty As Double
        Dim mLastYearDNCNAmount As Double

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mTotPurchaseAmount = 0
        mTotPurExpAmount = 0
        mTotSuppAmount = 0
        mTotDebitAmount = 0
        mTotCreditAmount = 0
        mGrandAmount = 0
        mTotReturnQty = 0


        SqlStr = MakeSQL_S
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1
        With SprdMain
            Do While RsTemp.EOF = False
                .Row = cntRow

                mTRNTYPE = IIf(IsDbNull(RsTemp.Fields("TRNTYPE").Value), -1, RsTemp.Fields("TRNTYPE").Value)

                .Col = ColItemCode
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                mItemCode = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                .Col = ColPartyCode
                .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                mPartyCode = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                .Col = ColItemName
                .Text = IIf(IsDbNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                .Col = ColMRRNo
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_MRR").Value), "0", RsTemp.Fields("AUTO_KEY_MRR").Value), "0.00")
                mMRRNO = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_MRR").Value), -1, RsTemp.Fields("AUTO_KEY_MRR").Value), "0.00"))

                .Col = ColVNo
                .Text = IIf(IsDbNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)

                .Col = ColVDate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")

                .Col = ColBillNo
                .Text = IIf(IsDbNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                mBillNo = IIf(IsDbNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)

                .Col = ColBillDate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                mBillDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")

                .Col = ColQuantity
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), "0", RsTemp.Fields("ITEM_QTY").Value), "0.00")

                .Col = ColRate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_RATE").Value), "0", RsTemp.Fields("ITEM_RATE").Value), "0.00")

                .Col = ColPurchaseAmount
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT").Value), "0", RsTemp.Fields("AMOUNT").Value), "0.00")

                mPurchaseAmount = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT").Value), "0", RsTemp.Fields("AMOUNT").Value), "0.00"))
                mMkey = IIf(IsDbNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
                mPurExpAmount = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("PUR_EXP").Value), "0", RsTemp.Fields("PUR_EXP").Value), "0.00"))
                mSuppAmount = GetSuppAmount(mTRNTYPE, mMkey, mItemCode)
                mAccountCode = IIf(IsDbNull(RsTemp.Fields("ACCOUNTCODE").Value), "", RsTemp.Fields("ACCOUNTCODE").Value)
                mDNCNQty = 0
                mReturnQty = 0
                mDNCNAmount = GetDnCnAmount(mAccountCode, mMRRNO, mItemCode, "", mDNCNQty)
                mPurReturnAmount = GetDnCnAmount(mAccountCode, mMRRNO, mItemCode, "R", mReturnQty)
                mTotalAmount = mPurchaseAmount + mPurExpAmount + mSuppAmount + mDNCNAmount + mPurReturnAmount

                .Col = ColPurExpAmount
                .Text = VB6.Format(mPurExpAmount, "0.00")

                .Col = ColSuppAmount
                .Text = VB6.Format(mSuppAmount, "0.00")

                .Col = ColDNCNAmount
                .Text = VB6.Format(mDNCNAmount, "0.00")

                .Col = CoPurchaseReturnQty
                .Text = VB6.Format(mReturnQty, "0.00")

                .Col = CoPurchaseReturnAmount
                .Text = VB6.Format(mPurReturnAmount, "0.00")

                .Col = ColTotalAmount
                .Text = VB6.Format(mTotalAmount, "0.00")

                .Col = ColPurchaseHead
                .Text = IIf(IsDbNull(RsTemp.Fields("Name").Value), "", RsTemp.Fields("Name").Value)

                .Col = ColMKEY
                .Text = IIf(IsDbNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)

                mTotPurchaseAmount = mTotPurchaseAmount + mPurchaseAmount
                mTotPurExpAmount = mTotPurExpAmount + mPurExpAmount
                mTotSuppAmount = mTotSuppAmount + mSuppAmount
                mTotDebitAmount = mTotDebitAmount + mDNCNAmount
                mTotCreditAmount = mTotCreditAmount + mPurReturnAmount
                mGrandAmount = mGrandAmount + mTotalAmount
                mTotReturnQty = mTotReturnQty + mReturnQty

                RsTemp.MoveNext()
                cntRow = cntRow + 1
                .MaxRows = cntRow
            Loop


            .Row = cntRow
            .Col = ColItemName
            .Text = "DEBIT / CREDIT NOTE RELATED TO LAST YEAR :"

            mLastYearDNCNAmount = GetDnCnAmount("", -1, "", "L", 0)
            .Col = ColDNCNAmount
            .Text = VB6.Format(mLastYearDNCNAmount, "0.00")

            .Col = ColTotalAmount
            .Text = VB6.Format(mLastYearDNCNAmount, "0.00")

            mTotDebitAmount = mTotDebitAmount + mLastYearDNCNAmount
            mGrandAmount = mGrandAmount + mLastYearDNCNAmount

            cntRow = cntRow + 1
            .MaxRows = cntRow

            .Row = cntRow
            .Col = ColItemName
            .Text = "DEBIT / CREDIT NOTE OTHER THAN MATERIAL :"

            mLastYearDNCNAmount = GetDNCNAmountOthers()
            .Col = ColDNCNAmount
            .Text = VB6.Format(mLastYearDNCNAmount, "0.00")

            .Col = ColTotalAmount
            .Text = VB6.Format(mLastYearDNCNAmount, "0.00")

            mTotDebitAmount = mTotDebitAmount + mLastYearDNCNAmount
            mGrandAmount = mGrandAmount + mLastYearDNCNAmount

            cntRow = cntRow + 1
            .MaxRows = cntRow

            .Row = cntRow
            .Col = ColItemName
            .Text = "OTHER VOUCHER :"

            mLastYearDNCNAmount = GetJVAmount()
            '            .Col = ColDNCNAmount	
            '            .Text = Format(mLastYearDNCNAmount, "0.00")	

            .Col = ColTotalAmount
            .Text = VB6.Format(mLastYearDNCNAmount, "0.00")

            '            mTotDebitAmount = mTotDebitAmount + mLastYearDNCNAmount	
            mGrandAmount = mGrandAmount + mLastYearDNCNAmount

            cntRow = cntRow + 1
            .MaxRows = cntRow

            .Row = cntRow

            .Col = ColItemName
            .Text = "GRAND TOTAL"

            .Col = ColPurchaseAmount
            .Text = VB6.Format(mTotPurchaseAmount, "0.00")

            .Col = ColPurExpAmount
            .Text = VB6.Format(mTotPurExpAmount, "0.00")

            .Col = ColSuppAmount
            .Text = VB6.Format(mTotSuppAmount, "0.00")

            .Col = ColDNCNAmount
            .Text = VB6.Format(mTotDebitAmount, "0.00")

            .Col = CoPurchaseReturnQty
            .Text = VB6.Format(mTotReturnQty, "0.00")

            .Col = CoPurchaseReturnAmount
            .Text = VB6.Format(mTotCreditAmount, "0.00")

            .Col = ColTotalAmount
            .Text = VB6.Format(mGrandAmount, "0.00")



        End With


        '********************************	
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        '    Resume	
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetSuppAmount(ByRef mTRNTYPE As Double, ByRef mMkey As String, ByRef mItemCode As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetSuppAmount = 0

        SqlStr = " SELECT ((((ID.QTY * ID.RATE)" & vbCrLf & " * ((ITEMVALUE + TOTEXPAMT) " & vbCrLf & " - DECODE(ISMODVAT,'Y',(MODVATAMOUNT + CESSAMOUNT +SHECMODVATAMOUNT ) * DECODE(ISCAPITAL,'Y',2,1) ,0)" & vbCrLf & " - DECODE(ISSTREFUND,'Y', (STCLAIMAMOUNT + SUR_VATCLAIMAMOUNT),0)" & vbCrLf & " ))/ITEMVALUE)) AS SUPP_AMOUNT"

        ''FROM CLAUSE... 'ADEMODVATAMOUNT	
        SqlStr = SqlStr & vbCrLf & " FROM FIN_SUPP_PURCHASE_HDR IH, FIN_SUPP_PURCHASE_DET ID "
        ''WHERE CLAUSE...	

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.TRNTYPE=" & mTRNTYPE & "" & vbCrLf _
            & " AND ID.PUR_MKEY='" & mMkey & "'" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & "AND IH.ISFINALPOST='Y' AND CANCELLED='N'"


        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetSuppAmount = GetSuppAmount + IIf(IsDbNull(RsTemp.Fields("SUPP_AMOUNT").Value), 0, RsTemp.Fields("SUPP_AMOUNT").Value)
                RsTemp.MoveNext()
            Loop
        End If
        '********************************	
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        GetSuppAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetDnCnAmount(ByRef mAccountCode As String, ByRef mMRRNO As Double, ByRef mItemCode As String, ByRef pDNCNTYPE As String, ByRef pDNCNQty As Double) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTrnCode As String
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAllTrnType As Boolean

        GetDnCnAmount = 0
        pDNCNQty = 0

        SqlStr = " SELECT ITEM_QTY * DECODE(BOOKCODE," & ConDebitNoteBookCode & ",-1,1) AS ITEM_QTY," & vbCrLf & " ((((ID.ITEM_QTY * ID.ITEM_RATE)" & vbCrLf & " * ((ITEMVALUE + TOTEXPAMT - (CASE WHEN DNCNTYPE='R' THEN TOTEDAMOUNT ELSE 0 END)) " & vbCrLf & " - DECODE(ISMODVAT,'Y',(MODVATAMOUNT) ,0)" & vbCrLf & " - DECODE(ISSTREFUND,'Y', (STCLAIMAMOUNT + SUR_VATCLAIMAMOUNT),0)" & vbCrLf & " ))/ITEMVALUE)) * DECODE(BOOKCODE," & ConDebitNoteBookCode & ",-1,1) AS SUPP_AMOUNT"

        ''FROM CLAUSE... 'ADEMODVATAMOUNT	
        SqlStr = SqlStr & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID "
        ''WHERE CLAUSE...	


        If pDNCNTYPE = "L" Then
            SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.MRR_REF_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            mAllTrnType = True

            'For CntLst = 1 To lstInvoiceType.Items.Count - 1
            '    If lstInvoiceType.GetItemChecked(CntLst) = True Then
            '        mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
            '        If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "ACCOUNTPOSTCODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '            mTrnCode = "'" & IIf(IsDBNull(MasterNo), "", MasterNo) & "'"
            '        End If
            '        mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            '    Else
            '        mAllTrnType = False
            '    End If
            'Next

            For CntLst = 1 To lstInvoiceType.Items.Count - 1
                If lstInvoiceType.GetItemChecked(CntLst) = True Then
                    '            lstInvoiceType.ListIndex = CntLst
                    mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                    If MainClass.ValidateWithMasterTable(mInvoiceType, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
                Else
                    mAllTrnType = False
                End If
            Next

            If mTrnTypeStr <> "" And mAllTrnType = False Then
                mTrnTypeStr = "(" & mTrnTypeStr & ")"
                SqlStr = SqlStr & vbCrLf & "AND DECODE(BOOKCODE," & ConDebitNoteBookCode & ",CREDITACCOUNTCODE,DEBITACCOUNTCODE) IN " & mTrnTypeStr & ""
            End If



            SqlStr = SqlStr & vbCrLf & "AND IH.APPROVED='Y' AND CANCELLED='N'"
        Else
            SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
                & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.MKEY=ID.MKEY " & vbCrLf _
                & " AND ID.MRR_REF_NO=" & mMRRNO & "" & vbCrLf _
                & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"


            SqlStr = SqlStr & vbCrLf & "AND DECODE(BOOKCODE," & ConDebitNoteBookCode & ",CREDITACCOUNTCODE,DEBITACCOUNTCODE)='" & MainClass.AllowSingleQuote(mAccountCode) & "'"

            SqlStr = SqlStr & vbCrLf & "AND IH.APPROVED='Y' AND CANCELLED='N'"

            If pDNCNTYPE <> "" Then
                SqlStr = SqlStr & vbCrLf & "AND DNCNTYPE='" & pDNCNTYPE & "'"
            Else
                SqlStr = SqlStr & vbCrLf & "AND DNCNTYPE<>'R'"
            End If
        End If


        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetDnCnAmount = GetDnCnAmount + IIf(IsDbNull(RsTemp.Fields("SUPP_AMOUNT").Value), 0, RsTemp.Fields("SUPP_AMOUNT").Value)
                pDNCNQty = pDNCNQty + IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                RsTemp.MoveNext()
            Loop
        End If
        '********************************	
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        GetDnCnAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetJVAmount() As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTrnCode As String
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAllTrnType As Boolean

        GetJVAmount = 0

        SqlStr = " SELECT SUM(AMOUNT * DECODE(DC,'D',1,-1)) AS AMOUNT " & vbCrLf & " FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID "

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANYCODE AND IH.MKEY=ID.MKEY "

        SqlStr = SqlStr & vbCrLf & " AND IH.MKEY NOT IN (" & vbCrLf & " SELECT JVMKEY FROM FIN_SUPP_PURCHASE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISFINALPOST='Y' AND CANCELLED='N')"




        mAllTrnType = True

        'For CntLst = 1 To lstInvoiceType.Items.Count - 1
        '    If lstInvoiceType.GetItemChecked(CntLst) = True Then
        '        mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
        '        If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "ACCOUNTPOSTCODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mTrnCode = "'" & IIf(IsDBNull(MasterNo), "", MasterNo) & "'"
        '        End If
        '        mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
        '    Else
        '        mAllTrnType = False
        '    End If
        'Next

        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mAllTrnType = False
            End If
        Next

        If mTrnTypeStr <> "" And mAllTrnType = False Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            SqlStr = SqlStr & vbCrLf & "AND ID.ACCOUNTCODE IN " & mTrnTypeStr & ""
        End If



        SqlStr = SqlStr & vbCrLf & "AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetJVAmount = GetJVAmount + IIf(IsDbNull(RsTemp.Fields("AMOUNT").Value), 0, RsTemp.Fields("AMOUNT").Value)
                RsTemp.MoveNext()
            Loop
        End If
        '********************************	
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        GetJVAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetDNCNAmountOthers() As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTrnCode As String
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAllTrnType As Boolean

        GetDNCNAmountOthers = 0

        SqlStr = " SELECT SUM(NETVALUE) AS NETVALUE"

        ''FROM CLAUSE... 'ADEMODVATAMOUNT	
        SqlStr = SqlStr & vbCrLf & " FROM FIN_DNCN_HDR IH"
        ''WHERE CLAUSE...	

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.MKEY NOT IN (" & vbCrLf & " SELECT MKEY FROM FIN_DNCN_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"

        mAllTrnType = True

        'For CntLst = 1 To lstInvoiceType.Items.Count - 1
        '    If lstInvoiceType.GetItemChecked(CntLst) = True Then
        '        mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
        '        If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "ACCOUNTPOSTCODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mTrnCode = "'" & IIf(IsDBNull(MasterNo), "", MasterNo) & "'"
        '        End If
        '        mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
        '    Else
        '        mAllTrnType = False
        '    End If
        'Next

        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mAllTrnType = False
            End If
        Next

        If mTrnTypeStr <> "" And mAllTrnType = False Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            SqlStr = SqlStr & vbCrLf & "AND DECODE(BOOKCODE," & ConDebitNoteBookCode & ",CREDITACCOUNTCODE,DEBITACCOUNTCODE) IN " & mTrnTypeStr & ""
        End If



        SqlStr = SqlStr & vbCrLf & "AND IH.APPROVED='Y' AND CANCELLED='N'"



        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetDNCNAmountOthers = GetDNCNAmountOthers + IIf(IsDbNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                RsTemp.MoveNext()
            Loop
        End If
        '********************************	
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        GetDNCNAmountOthers = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function MakeSQL_S() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mSuppCustCode As String
        Dim mDivisionCode As Double
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAllTrnType As Boolean
        Dim mFYearFrom As Integer
        Dim mFYearTo As Integer


        mFYearFrom = GetCurrentFYNo(PubDBCn, VB6.Format(txtDateFrom.Text, "DD/MM/YYYY"))
        mFYearTo = GetCurrentFYNo(PubDBCn, VB6.Format(txtDateTo.Text, "DD/MM/YYYY"))

        ''SELECT CLAUSE...	


        MakeSQL_S = " SELECT IH.COMPANY_CODE, IH.FYEAR, " & vbCrLf & " ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.ACCOUNTCODE," & vbCrLf & " ID.ITEM_DESC, IH.VNO, IH.VDATE, " & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf & " IH.AUTO_KEY_MRR, IH.MRRDATE, " & vbCrLf & " SUM(ID.ITEM_QTY) AS ITEM_QTY, SUM(ID.ITEM_RATE) AS ITEM_RATE, SUM(ID.ITEM_QTY * ID.ITEM_RATE)  AS AMOUNT," & vbCrLf & " IH.MKEY,ITYPE.NAME,IH.TRNTYPE, "

        MakeSQL_S = MakeSQL_S & vbCrLf & " (((SUM(ID.ITEM_QTY * ID.ITEM_RATE)" & vbCrLf & " * ((ITEMVALUE + TOTEXPAMT) " & vbCrLf & " - DECODE(ISMODVAT,'Y',(MODVATAMOUNT + CESSAMOUNT +SHECMODVATAMOUNT + ADEMODVATAMOUNT) * DECODE(ISCAPITAL,'Y',2,1) ,0)" & vbCrLf & " - DECODE(ISSTREFUND,'Y', (STCLAIMAMOUNT + SUR_VATCLAIMAMOUNT),0)" & vbCrLf & " ))/ITEMVALUE)-SUM(ID.ITEM_QTY * ID.ITEM_RATE)) AS PUR_EXP"

        ''FROM CLAUSE...	
        MakeSQL_S = MakeSQL_S & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, " & vbCrLf & " INV_GATE_HDR GH, FIN_SUPP_CUST_MST CMST, FIN_INVTYPE_MST ITYPE,"

        MakeSQL_S = MakeSQL_S & vbCrLf & " INV_ITEM_MST IMST"
        ''WHERE CLAUSE...	

        MakeSQL_S = MakeSQL_S & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR>=" & mFYearFrom & " AND FYEAR<=" & mFYearTo & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        MakeSQL_S = MakeSQL_S & vbCrLf & " AND IH.COMPANY_CODE=GH.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=GH.SUPP_CUST_CODE" & vbCrLf & " AND IH.AUTO_KEY_MRR=GH.AUTO_KEY_MRR "

        MakeSQL_S = MakeSQL_S & vbCrLf & " AND IH.COMPANY_CODE=ITYPE.COMPANY_CODE" & vbCrLf & " AND IH.TRNTYPE=ITYPE.CODE "

        MakeSQL_S = MakeSQL_S & vbCrLf & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=IMST.ITEM_CODE "

        mAllTrnType = True

        'For CntLst = 1 To lstInvoiceType.Items.Count - 1
        '    If lstInvoiceType.GetItemChecked(CntLst) = True Then
        '        mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
        '        If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
        '        End If
        '        mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
        '    Else
        '        mAllTrnType = False
        '    End If
        'Next

        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mAllTrnType = False
            End If
        Next

        If mTrnTypeStr <> "" And mAllTrnType = False Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            MakeSQL_S = MakeSQL_S & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
        End If

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = Trim(MasterNo)
            End If
            MakeSQL_S = MakeSQL_S & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCustCode = MasterNo
                MakeSQL_S = MakeSQL_S & vbCrLf & "AND CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'"
            End If
        End If

        '    MakeSQL_S = MakeSQL_S & vbCrLf & "AND CMST.SUPP_CUST_NAME LIKE 'D%'"	

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL_S = MakeSQL_S & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        MakeSQL_S = MakeSQL_S & vbCrLf & "AND IH.AUTO_KEY_MRR<>-1 AND IH.TRNTYPE>0 AND IH.VNO<>'-1' AND IH.ISFINALPOST='Y' AND CANCELLED='N'"


        '    MakeSQL_S = MakeSQL_S & vbCrLf & "AND IH.AUTO_KEY_MRR='40633201201'"	

        If optDate(0).Checked = True Then
            MakeSQL_S = MakeSQL_S & vbCrLf & " AND IH.MRRDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.MRRDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf optDate(1).Checked = True Then
            MakeSQL_S = MakeSQL_S & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQL_S = MakeSQL_S & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MakeSQL_S = MakeSQL_S & vbCrLf & " GROUP BY IH.COMPANY_CODE, IH.FYEAR, " & vbCrLf & " ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.ACCOUNTCODE," & vbCrLf & " ID.ITEM_DESC, IH.VNO, IH.VDATE, " & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf & " IH.AUTO_KEY_MRR, IH.MRRDATE, " & vbCrLf & " IH.MKEY,ITYPE.NAME,IH.TRNTYPE," & vbCrLf & " ITEMVALUE, TOTEXPAMT, ISMODVAT,MODVATAMOUNT,CESSAMOUNT,SHECMODVATAMOUNT,ADEMODVATAMOUNT,ISCAPITAL,ISSTREFUND,STCLAIMAMOUNT,SUR_VATCLAIMAMOUNT"
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

        '    mDelRow = 0	
        '    With SprdMain	
        '        For cntRow = 1 To .MaxRows	
        '            .Row = cntRow	
        '	
        '            .Col = ColRate	
        '            mPurRate = Val(.Text)	
        '	
        '            .Col = ColDNCNRate	
        '            mDNCNRate = Val(.Text)	
        '	
        '            .Col = ColSuppRate	
        '            mSuppRate = Val(.Text)	
        '	
        '            .Col = ColNetRate	
        '            mNetRate = Format(mPurRate - mDNCNRate + mSuppRate, "0.000")	
        '            .Text = Format(mPurRate - mDNCNRate + mSuppRate, "0.000")	
        '	
        '            .Col = ColPORate	
        '            mPORate = Val(.Text)	
        '	
        '            .Col = ColDiff	
        '            mDiffRate = mPORate - mNetRate	
        '            .Text = Format(mDiffRate, "0.000")	
        '	
        ''            If cboShow.ListIndex = 1 Then	
        ''                If mDiffRate >= 0 Then	
        ''                    .Row = cntRow	
        ''                    .Action = SS_ACTION_DELETE_ROW	
        '                    If .MaxRows > 1 Then .MaxRows = .MaxRows - 1	
        ''                    mDelRow = mDelRow + 1	
        ''                End If	
        ''            ElseIf cboShow.ListIndex = 2 Then	
        ''                 If mDiffRate <= 0 Then	
        ''                    .Row = cntRow	
        ''                    .Action = SS_ACTION_DELETE_ROW	
        '                    If .MaxRows > 1 Then .MaxRows = .MaxRows - 1	
        ''                    mDelRow = mDelRow + 1	
        ''                End If	
        ''            End If	
        '	
        '        Next	
        ''        If .MaxRows > mDelRow Then .MaxRows = .MaxRows - mDelRow	
        '    End With	
        '	


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
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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
        Dim xMkey As String = ""
        Dim xVNo As String
        Dim xBookType As String = ""
        Dim xBookSubType As String


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColVDate
        xVDate = Me.SprdMain.Text

        SprdMain.Col = ColMKEY
        xMkey = Me.SprdMain.Text

        SprdMain.Col = ColVNo
        xVNo = Me.SprdMain.Text

        Call ShowTrn(xMkey, xVDate, "", xVNo, "P", "", Me)

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

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtItemName.Text)
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
        Dim SqlStr As String = ""


        If TxtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            Exit Sub
        End If

        '    If FYChk(CDate(txtDateFrom.Text)) = False Then	
        '        txtDateFrom.SetFocus	
        '        Cancel = True	
        '        Exit Sub	
        '    End If	
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
End Class
