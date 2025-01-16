Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamStockQDetail
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean

    Dim GroupOnItem As Boolean

    Private Const RowHeight As Short = 12

    Private Const ColUserID As Short = 1
    Private Const ColCatgeory As Short = 2
    Private Const ColSubCategory As Short = 3
    Private Const ColItemCode As Short = 4
    Private Const ColItemName As Short = 5
    Private Const ColUnit As Short = 6
    Private Const ColOpeningQty As Short = 7
    Private Const ColOpeningValue As Short = 8
    Private Const ColPurchaseQty As Short = 9
    Private Const ColPurchaseValue As Short = 10
    Private Const ColIssueQty As Short = 11
    Private Const ColIssueValue As Short = 12
    Private Const ColSRNQty As Short = 13
    Private Const ColSRNValue As Short = 14
    Private Const ColRejQty As Short = 15
    Private Const ColRejValue As Short = 16
    Private Const ColSaleQty As Short = 17
    Private Const ColSaleValue As Short = 18
    Private Const ColClosingQty As Short = 19
    Private Const ColClosingValue As Short = 20

    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean
    Private Sub cboCond_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub cboCond_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.SelectedIndexChanged
        PrintStatus(False)
    End Sub

    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub cboShow_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.SelectedIndexChanged
        PrintStatus(False)
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        txtCatName.Enabled = IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearchCategory.Enabled = IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub chkItemAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkItemAll.CheckStateChanged
        txtItemName.Enabled = IIf(chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdItemDesc.Enabled = IIf(chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub chkOption_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOption.CheckStateChanged
        FraOption.Visible = IIf(chkOption.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        PrintStatus(False)
    End Sub

    Private Sub chkSubCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSubCategory.CheckStateChanged
        txtSubCatName.Enabled = IIf(chkSubCategory.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearchSubCat.Enabled = IIf(chkSubCategory.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub chkZeroBal_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkZeroBal.CheckStateChanged
        PrintStatus(False)
    End Sub
    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub FillSprdMain()
        On Error GoTo ERR1
        Dim I As Integer
        Dim mField As String

        With SprdMain
            .Row = 0

            .Col = 0
            .Text = "S.No."

            .Col = ColUserID
            .Text = "User ID"

            .Col = ColCatgeory
            .Text = "Category"

            .Col = ColSubCategory
            .Text = "Sub Category"

            .Col = ColItemCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Short Name"

            .Col = ColUnit
            .Text = "Unit"

            .Col = ColOpeningQty
            .Text = "Opening Qty"

            .Col = ColOpeningValue
            .Text = "Opening Value"

            .Col = ColPurchaseQty
            .Text = "Purchase Qty"

            .Col = ColPurchaseValue
            .Text = "Purchase Value"

            .Col = ColIssueQty
            .Text = "Issue Qty"

            .Col = ColIssueValue
            .Text = "Issue Value"

            .Col = ColSRNQty
            .Text = "SRN Qty"

            .Col = ColSRNValue
            .Text = "SRN Value"

            .Col = ColRejQty
            .Text = "Rejection Qty"

            .Col = ColRejValue
            .Text = "Rejection Value"

            .Col = ColSaleQty
            .Text = "Sale Qty"

            .Col = ColSaleValue
            .Text = "Sale Value"

            .Col = ColClosingQty
            .Text = "Closing Qty"

            .Col = ColClosingValue
            .Text = "Closing Value"

        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer
        Dim mColWidth As Integer

        With SprdMain
            .set_RowHeight(0, 1.75 * RowHeight)
            .Row = Arow
            .set_ColWidth(0, 5)

            .Col = ColUserID
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColUserID, 12)
            .ColHidden = True

            .Col = ColCatgeory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColCatgeory, 12)
            .ColHidden = True

            .Col = ColSubCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColSubCategory, 12)
            .ColHidden = True

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColItemCode, 6.5)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColItemName, 22)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColUnit, 4.5)

            For I = ColOpeningQty To ColClosingValue
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeFloatDecimalPlaces = 2
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(I, 10)
            Next

            .ColsFrozen = ColItemName

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub cmdItemDesc_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdItemDesc.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtItemName.Text), "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemName.Text = AcName
            TxtItemName_Validating(TxtItemName, New System.ComponentModel.CancelEventArgs(False))
            txtItemName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForStockOnHand(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForStockOnHand(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mRPTName As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mRPTName = "StockQDetailRM.rpt"
        mTitle = "Quantitaive Detail "

        mSubTitle = " From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")
        mSubTitle = mSubTitle & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = "(Category : " & txtCatName.Text & ")"
        End If

        If chkSubCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & " (Sub Category : " & txtSubCatName.Text & ")"
        End If

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)
        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        '    Resume
    End Sub


    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForStockOnHand(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCategory.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster((txtCatName.Text), "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
            txtCatName.Text = AcName
            txtCatName_Validating(txtCatName, New System.ComponentModel.CancelEventArgs(False))
            txtCatName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchSubCat_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSubCat.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((txtCatName.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCategoryCode = MasterNo
                SqlStr = SqlStr & " AND CATEGORY_CODE='" & mCategoryCode & "'"
            End If
        End If

        If MainClass.SearchGridMaster((txtSubCatName.Text), "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
            txtSubCatName.Text = AcName
            txtSubCatName_Validating(txtSubCatName, New System.ComponentModel.CancelEventArgs(False))
            txtSubCatName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click


        Dim SqlStr As String = ""
        PrintStatus(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdMain)

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Show1()
        SprdMain.Refresh()
        FormatSprdMain(-1)
        FillSprdMain()
        GroupByColor()

        PrintStatus(True)
        '    SprdMain.SetFocus
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim I As Integer
        Dim mCategoryCode As String = ""
        Dim mCond As String

        FieldsVarification = True


        If Not IsDate(txtDateFrom.Text) Then
            FieldsVarification = False
            MsgInformation("Invaild Date")
            txtDateFrom.Focus()
            Exit Function
        ElseIf FYChk((txtDateFrom.Text)) = False Then
            FieldsVarification = False
            txtDateFrom.Focus()
            Exit Function
        End If

        If Not IsDate(txtDateTo.Text) Then
            FieldsVarification = False
            MsgInformation("Invaild Date")
            txtDateTo.Focus()
            Exit Function
        ElseIf FYChk((txtDateTo.Text)) = False Then
            FieldsVarification = False
            txtDateTo.Focus()
            Exit Function
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtCatName.Text) = "" Then
                MsgInformation("Please Select Catgeory Name.")
                FieldsVarification = False
                txtCatName.Focus()
            Else
                If MainClass.ValidateWithMasterTable((txtCatName.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mCategoryCode = MasterNo
                Else
                    MsgInformation("Invalid Catgeory Name.")
                    FieldsVarification = False
                    txtCatName.Focus()
                End If

            End If
        End If

        If chkSubCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtSubCatName.Text) = "" Then
                MsgInformation("Please Select Sub-Catgeory Name.")
                FieldsVarification = False
                txtSubCatName.Focus()
            Else

                mCond = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    mCond = mCond & " AND CATEGORY_CODE='" & mCategoryCode & "'"
                End If
                If MainClass.ValidateWithMasterTable((txtSubCatName.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , mCond) = False Then
                    MsgInformation("Invalid Sub-Catgeory Name.")
                    FieldsVarification = False
                    txtSubCatName.Focus()
                End If
            End If
        End If
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function Show1() As Boolean

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim RsStock As ADODB.Recordset = Nothing
        Dim cnt As Integer
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mTotClosing As Double
        Dim mTotValue As Double

        FormatSprdMain(-1)
        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsStock, adLockReadOnly
        '    cnt = 1
        '    With SprdMain
        '        If RsStock.EOF = False Then
        '            Do While Not RsStock.EOF
        '                .Row = cnt
        '
        '                .Col = ColUserID
        '                .Text = IIf(IsNull(RsStock!UserID), "", RsStock!UserID)
        '
        '                .Col = ColCatgeory
        '                .Text = IIf(IsNull(RsStock!CATEGORY_CODE), "", RsStock!CATEGORY_CODE)
        '
        '                .Col = ColSubCategory
        '                .Text = IIf(IsNull(RsStock!SUBCATEGORY_CODE), "", RsStock!SUBCATEGORY_CODE)
        '
        '                .Col = ColItemCode
        '                mItemCode = Trim(IIf(IsNull(RsStock!ITEM_CODE), "", RsStock!ITEM_CODE))
        '                .Text = mItemCode
        '
        '                .Col = ColItemName
        '                .Text = IIf(IsNull(RsStock!Item_Short_Desc), "", RsStock!Item_Short_Desc)
        '
        '                .Col = ColUnit
        '                mUOM = Trim(IIf(IsNull(RsStock!ITEM_UOM), "", RsStock!ITEM_UOM))
        '                .Text = mUOM
        '
        '                .Col = ColOpening
        '                .Text = Val(IIf(IsNull(RsStock!Opening), 0, RsStock!Opening))
        '
        '                .Col = ColReceipt
        '                .Text = Val(IIf(IsNull(RsStock!Receipt), 0, RsStock!Receipt))
        '
        '                .Col = ColIssue
        '                .Text = Val(IIf(IsNull(RsStock!Issue), 0, RsStock!Issue))
        '
        '                .Col = ColClosing
        '                .Text = Val(IIf(IsNull(RsStock!Closing), 0, RsStock!Closing))
        '
        '                .Col = ColRejection
        '                .Text = Val(IIf(IsNull(RsStock!Rejection), 0, RsStock!Rejection))
        '
        '                .Col = ColUnderQC
        '                .Text = Val(IIf(IsNull(RsStock!UnderQC), 0, RsStock!UnderQC))
        '
        '                .Col = ColTotalClosing
        '                mTotClosing = Val(IIf(IsNull(RsStock!TotClosing), 0, RsStock!TotClosing))
        '                .Text = mTotClosing
        '
        '
        ''                mTotValue = GetLatestItemCostFromMRR(mItemCode, mUOM, mTotClosing, txtDateTo.Text, IIf(optShow(0).Value = True, "P", IIf(optShow(1).Value = True, "L", "S")))
        ''
        ''                .Row = cnt
        ''                .Col = ColValue
        ''                .Text = mTotValue
        ''
        ''                .Col = ColRate
        ''                If mTotClosing > 0 Then
        ''                    .Text = mTotValue / mTotClosing
        ''                Else
        ''                    .Text = "0.00"
        ''                End If
        '
        '                RsStock.MoveNext
        '                If RsStock.EOF = False Then
        '                    cnt = cnt + 1
        '                    .MaxRows = cnt
        '                End If
        '            Loop
        '        End If
        '    End With
        Show1 = True
        Exit Function
InsertErr:
        Show1 = False
        MsgBox(Err.Description)
        ''Resume
    End Function



    Private Function MakeSQL() As String

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""
        Dim mSubCategoryCode As String
        Dim mCond As String
        Dim mItemCode As String
        Dim mStockType As String = ""
        Dim mRejStockType As String
        Dim mCheckStockType As String
        Dim mCostField As String
        Dim mTableName As String

        mStockType = "('ST','QC')"
        mRejStockType = "'RJ'"
        mCheckStockType = "('ST','RJ','QC')"

        mTableName = ConInventoryTable

        If optShow(0).Checked = True Then
            mCostField = "INV.PURCHASE_COST"
        Else
            mCostField = "INV.LANDED_COST"
        End If

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' AS USERID, " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, "

        ''ColOpeningQty
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND STOCK_TYPE IN " & mStockType & " " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS OpeningQty, "

        ''ColOpeningValue
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND STOCK_TYPE IN " & mStockType & " " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * " & mCostField & " ELSE 0 END)) AS OpeningValue, "

        ''ColPurchaseQty
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_TYPE IN ('" & ConStockRefType_MRR & "','" & ConStockRefType_REOFFER & "') AND STOCK_TYPE IN " & mStockType & "" & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS ReceiptQty, "

        ''ColPurchaseValue
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_TYPE IN ('" & ConStockRefType_MRR & "','" & ConStockRefType_REOFFER & "') AND STOCK_TYPE IN " & mStockType & "" & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) * " & mCostField & " ELSE 0 END)) AS ReceiptValue, "

        ''ColIssueQty
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_TYPE IN ('" & ConStockRefType_ISS & "','" & ConStockRefType_PMEMODEPT & "') AND STOCK_TYPE IN " & mStockType & "" & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'O',1,0) ELSE 0 END)) AS IssueQty, "

        ''ColIssueValue
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_TYPE IN ('" & ConStockRefType_ISS & "','" & ConStockRefType_PMEMODEPT & "') AND STOCK_TYPE IN " & mStockType & "" & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'O',1,0) * " & mCostField & " ELSE 0 END)) AS IssueValue, "

        ''ColSRNQty
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_TYPE='" & ConStockRefType_SRN & "' AND STOCK_TYPE IN " & mStockType & "" & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS SRNQty, "

        ''ColSRNValue
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_TYPE='" & ConStockRefType_SRN & "' AND STOCK_TYPE IN " & mStockType & "" & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * " & mCostField & " ELSE 0 END)) AS SRNValue, "

        ''ColRejQty
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN STOCK_TYPE IN " & mRejStockType & "" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS RejQty, "

        ''ColRejValue
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN STOCK_TYPE IN " & mRejStockType & "" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * " & mCostField & " ELSE 0 END)) AS RejValue, "

        ''ColSaleQty
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_TYPE IN ('" & ConStockRefType_DSP & "','" & ConStockRefType_NRG & "','" & ConStockRefType_RGP & "') AND STOCK_TYPE IN " & mStockType & "" & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'O',1,0) ELSE 0 END)) AS SaleQty, "

        ''ColSaleValue
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_TYPE IN ('" & ConStockRefType_DSP & "','" & ConStockRefType_NRG & "','" & ConStockRefType_RGP & "') AND STOCK_TYPE IN " & mStockType & "" & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'O',1,0) * " & mCostField & " ELSE 0 END)) AS SaleValue, "

        ''ColClosingQty
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as TotClosingQty, "

        ''ColClosingValue
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * " & mCostField & ")) as TotClosingValue "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " INV, " & vbCrLf & " INV_ITEM_MST ITEM "


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf & " INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND ITEM.COMPANY_CODE=INV.COMPANY_CODE" & vbCrLf & " AND ITEM.ITEM_CODE=INV.ITEM_CODE "

        If cboShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        ElseIf cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        End If
        SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCategoryCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE='" & mCategoryCode & "'"
            End If
        End If

        If chkSubCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mCond = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mCond = mCond & " AND CATEGORY_CODE='" & mCategoryCode & "'"
            End If
            If MainClass.ValidateWithMasterTable(txtSubCatName.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , mCond) = True Then
                mSubCategoryCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND ITEM.SUBCATEGORY_CODE='" & mSubCategoryCode & "'"
            End If
        End If

        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'"


        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN " & mCheckStockType & ""

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If


        If chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        End If

        If chkZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"
        End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM "

        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE "


        MakeSQL = SqlStr
        Exit Function
InsertErr:
        MakeSQL = ""
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmParamStockQDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        Dim SqlStr As String = ""
        If FormLoaded = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        FormLoaded = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub frmParamStockQDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)
        MainClass.SetControlsColor(Me)

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
        txtDateTo.Text = CStr(RunDate)

        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCatName.Enabled = False
        cmdSearchCategory.Enabled = False

        chkSubCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtSubCatName.Enabled = False
        cmdSearchSubCat.Enabled = False

        txtCondQty.Text = CStr(0)

        cboCond.Items.Clear()
        cboCond.Items.Add("=")
        cboCond.Items.Add("<=")
        cboCond.Items.Add(">=")
        cboCond.Items.Add("<")
        cboCond.Items.Add(">")
        cboCond.Items.Add("<>")
        cboCond.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("Active Only")
        cboShow.Items.Add("Inactive Only")
        cboShow.Items.Add("Both")
        cboShow.SelectedIndex = 0

        FormatSprdMain(-1)
        FillSprdMain()
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub frmParamStockQDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub PrintStatus(ByRef PrintFlag As Boolean)
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub

    Private Sub optShow_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optShow.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShow.GetIndex(eventSender)
            PrintStatus(False)
        End If
    End Sub


    Private Sub txtCatName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCatName.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtCatName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCatName.DoubleClick
        Call cmdSearchCategory_Click(cmdSearchCategory, New System.EventArgs())
    End Sub

    Private Sub txtCatName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCatName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCatName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCatName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCatName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchCategory_Click(cmdSearchCategory, New System.EventArgs())
    End Sub

    Private Sub txtCatName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCatName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtCatName.Text) = "" Then
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable((txtCatName.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = False Then
            ErrorMsg("Invalid Category Code.", , MsgBoxStyle.Information)
            Cancel = True
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtCondQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCondQty.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtCondQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCondQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtDateFrom.Text = "" Then GoTo EventExitSub
        If IsDate(txtDateFrom.Text) = False Then
            MsgBox("Invalid Date")
            Cancel = True
        ElseIf FYChk((txtDateFrom.Text)) = False Then
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtDateTo.Text = "" Then GoTo EventExitSub
        If IsDate(txtDateTo.Text) = False Then
            MsgBox("Invalid Date")
            Cancel = True
        ElseIf FYChk((txtDateTo.Text)) = False Then
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub GroupByColor()
        'Dim mGroup As String
        'Dim CntRow As Long
        'Dim mBlackColor As Long
        'Dim mOpening As Double
        'Dim mReceipt As Double
        'Dim mIssue As Double
        'Dim mTotClosing As Double
        'Dim mClosing As Double
        '
        '    With SprdMain
        '        For CntRow = 1 To .MaxRows
        '            .Row = CntRow
        '            .Col = ColGrouping
        '            If mGroup <> Trim(.Text) Then
        '                If mBlackColor = &HFFFF00 Then
        '                    mBlackColor = &H80FF80
        '                Else
        '                    mBlackColor = &HFFFF00
        '                End If
        '                mGroup = Trim(.Text)
        '                mTotClosing = 0
        '            End If
        '
        '            .Row = CntRow
        '            .Row2 = CntRow
        '            .Col = 1
        '            .Col2 = .MaxCols
        '            .BlockMode = True
        '            .BackColor = mBlackColor            ''&HFFFF00
        '            .BlockMode = False
        '
        '            If lblLabelType.text = "StockReg" Then
        '                .Col = ColOpening
        '                mOpening = Val(.Text)
        '
        '                .Col = ColReceipt
        '                mReceipt = Val(.Text)
        '
        '                .Col = ColIssue
        '                mIssue = Val(.Text)
        '
        '                mClosing = mOpening + mReceipt - mIssue
        '                mTotClosing = mTotClosing + mClosing
        '
        '                .Col = ColClosing
        '                .Text = VB6.Format(mTotClosing, "0.00")
        '            End If
        '        Next
        '    End With
    End Sub

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        Call cmdItemDesc_Click(cmdItemDesc, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdItemDesc_Click(cmdItemDesc, New System.EventArgs())
    End Sub

    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtItemName.Text) = "" Then
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            ErrorMsg("Invalid Item Name.", , MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSubCatName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCatName.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtSubCatName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCatName.DoubleClick
        Call cmdSearchSubCat_Click(cmdSearchSubCat, New System.EventArgs())
    End Sub
    Private Sub txtSubCatName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCatName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCatName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSubCatName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCatName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchSubCat_Click(cmdSearchSubCat, New System.EventArgs())
    End Sub

    Private Sub txtSubCatName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCatName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""

        If Trim(txtSubCatName.Text) = "" Then
            GoTo EventExitSub
        End If

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((txtCatName.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCategoryCode = MasterNo
                SqlStr = SqlStr & " AND CATEGORY_CODE='" & mCategoryCode & "'"
            End If
        End If

        If MainClass.ValidateWithMasterTable((txtSubCatName.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Invalid Sub Category Code.", , MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
