Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Friend Class frmParamStockOnHand
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean

    Dim GroupOnItem As Boolean

    Private Const RowHeight As Short = 12

    Private Const ColUserID As Short = 1
    Private Const ColCatgeory As Short = 2
    Private Const ColCatgeoryDesc As Short = 3
    Private Const ColSubCategory As Short = 4
    Private Const ColSubCategoryDesc As Short = 5
    Private Const ColItemCode As Short = 6
    Private Const ColItemName As Short = 7
    Private Const ColUnit As Short = 8

    Private Const ColOpening As Short = 9
    Private Const ColMRRQty As Short = 10
    Private Const ColIssue As Short = 11
    Private Const ColClosing As Short = 12
    Private Const ColRejection As Short = 13
    Private Const ColRate As Short = 14

    Private Const ColMinQty As Short = 15
    Private Const ColMaxQty As Short = 16


    Private Const ColOpeningValue As Short = 17
    Private Const ColMRRValue As Short = 18
    Private Const ColReceipt As Short = 19
    Private Const ColReceiptValue As Short = 20
    Private Const ColIssueValue As Short = 21
    Private Const ColUnderQC As Short = 22
    Private Const ColDeptStock As Short = 23
    Private Const ColTotalClosing As Short = 24
    Private Const ColValue As Short = 25
    Private Const ColConsumptionQty As Short = 26
    Private Const ColConsumptionValue As Short = 27
    Private Const ColRGPQty As Short = 28
    Private Const ColRGPValue As Short = 29
    Private Const ColAssetsValue As Short = 30

    Private Const ColThickness As Short = 31
    Private Const ColColor As Short = 32

    Private Const ColTransDate As Short = 33
    Private Const ColLedgerHead As Short = 34
    Private Const ColLedgerAmount As Short = 35
    Private Const ColTotalClosingNos As Short = 36

    Dim mClickProcess As Boolean

    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub cboCond_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub cboCond_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.SelectedIndexChanged
        PrintStatus(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboExportItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExportItem.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub cboShow_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.SelectedIndexChanged
        PrintStatus(False)
    End Sub

    Private Sub chkFGStockRequired_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        PrintStatus(False)
    End Sub
    Private Sub chkCS_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
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

    Private Sub chkViewAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkViewAll.CheckStateChanged
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

            .Col = ColCatgeoryDesc
            .Text = "Category Desc"

            .Col = ColSubCategory
            .Text = "Sub Category"

            .Col = ColSubCategoryDesc
            .Text = "Sub Category Desc"

            .Col = ColItemCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Short Name"

            .Col = ColUnit
            .Text = "Unit"

            .Col = ColThickness
            .Text = "Thickness"

            .Col = ColColor
            .Text = "Color"

            .Col = ColMaxQty
            .Text = "Max Qty"

            .Col = ColMinQty
            .Text = "Min Qty"

            .Col = ColOpening
            .Text = "Opening"

            .Col = ColOpeningValue
            .Text = "Opening Value"

            .Col = ColMRRQty
            .Text = "MRR Qty"

            .Col = ColMRRValue
            .Text = "MRR Value"

            .Col = ColReceipt
            .Text = "Receipt"

            .Col = ColReceiptValue
            .Text = "Receipt Value"

            .Col = ColIssue
            .Text = "Issue"

            .Col = ColIssueValue
            .Text = "Issue Value"

            .Col = ColClosing
            .Text = "Closing"

            .Col = ColRejection
            .Text = "Rejection"

            .Col = ColUnderQC
            .Text = "Under QC"

            .Col = ColDeptStock
            .Text = "Depatment Stock"

            .Col = ColTotalClosing
            .Text = "Total Closing"

            .Col = ColRate
            .Text = "Rate"

            .Col = ColValue
            .Text = "Value"

            .Col = ColConsumptionQty
            .Text = "Consumption Qty"

            .Col = ColConsumptionValue
            .Text = "Consumption Value"

            .Col = ColRGPQty
            .Text = "Other Issue Qty"

            .Col = ColRGPValue
            .Text = "Other Issue Value"


            .Col = ColAssetsValue
            .Text = "Issue For Capital"

            .Col = ColTransDate
            .Text = "Last Transaction Date"

            .Col = ColLedgerHead
            .Text = "Ledger Head"

            .Col = ColLedgerAmount
            .Text = "Ledger Amount"

            .Col = ColTotalClosingNos
            .Text = "Closing Qty (in Nos)"

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
            .set_RowHeight(0, 1.5 * RowHeight)
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
            .set_ColWidth(ColCatgeory, 10)
            .ColHidden = True

            .Col = ColCatgeoryDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColCatgeoryDesc, 20)

            .Col = ColSubCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColSubCategory, 12)
            .ColHidden = True

            .Col = ColSubCategoryDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColSubCategoryDesc, 12)
            .ColHidden = False

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColItemCode, 8)
            .ColHidden = IIf(optType(1).Checked = True, True, False)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColItemName, 28)
            .ColHidden = IIf(optType(1).Checked = True, True, False)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColUnit, 4.5)
            .ColHidden = IIf(optType(1).Checked = True, True, False)

            .Col = ColThickness
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColThickness, 4.5)
            .ColHidden = IIf(optType(1).Checked = True, True, IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True))

            .Col = ColColor
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColColor, 6)
            .ColHidden = IIf(optType(1).Checked = True, True, IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True))

            For I = ColOpening To ColAssetsValue
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(I, 9)

            Next

            .Col = ColTransDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColTransDate, 9)
            .ColHidden = IIf(lblBookType.Text = "N", False, True)

            .Col = ColLedgerHead
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColLedgerHead, 25)
            .ColHidden = IIf(chkShowLedgerBalance.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

            .Col = ColLedgerAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColLedgerAmount, 9)
            .ColHidden = IIf(chkShowLedgerBalance.CheckState = System.Windows.Forms.CheckState.Checked, False, True)


            .Col = ColTotalClosingNos
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColTotalClosingNos, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .ColsFrozen = IIf(optType(1).Checked = True, ColCatgeoryDesc, ColUnit)

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
            TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(False))
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

        If InsertIntoTempTable() = False Then GoTo ERR1

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)


        mRPTName = "StockOnHand.rpt"
        If lblBookType.Text = "H" Then
            mTitle = "Inventory On Hand"
        ElseIf lblBookType.Text = "B" Then
            mTitle = "Below than Minimum Inventory"
        ElseIf lblBookType.Text = "A" Then
            mTitle = "Above than Maximum Inventory"
        ElseIf lblBookType.Text = "N" Then
            If optNonMoving(0).Checked = True Then
                mTitle = "Non-Moving Stock"
            Else
                mTitle = "Non-Issue Stock"
            End If
        End If

        If lblBookType.Text = "N" Then
            mTitle = mTitle & " From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        Else
            mTitle = mTitle & " - as on " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        End If

        '    If chkCategory.Value = vbUnchecked Then
        '        mSubTitle = "(Category : " & txtCatName.Text & ")"
        '    End If
        '
        '    If chkSubCategory.Value = vbUnchecked Then
        '        mSubTitle = mSubTitle & " (Sub Category : " & txtSubCatName.Text & ")"
        '    End If


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
    Private Function InsertIntoTempTable() As Boolean

        On Error GoTo PrintDummyErr

        Dim SqlStr As String = ""
        Dim mSqlStr As String
        Dim CntRow As Integer

        Dim mCategoryCode As String = ""
        Dim mSubCategoryCode As String
        Dim mItemCode As String
        Dim mItemName As String
        Dim mItemUOM As String = ""
        Dim mOpening As Double
        Dim mOpeningValue As Double
        Dim mMRRQty As Double
        Dim mMRRValue As Double

        Dim mReceipt As Double
        Dim mIssue As Double
        Dim mClosing As Double
        Dim mRej As Double
        Dim mQC As Double
        Dim mDeptStock As Double
        Dim mTotClosing As Double
        Dim mRate As Double
        Dim mValue As Double
        Dim mCategoryDesc As String
        Dim mTransDate As String
        Dim mMaxQty As Double
        Dim mMinQty As Double

        'Dim PvtDBCn As ADODB.Connection

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_STOCKONHAND NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        '    If lblbookType.text = "N" Then
        '        mSqlStr = MakeSQL2
        '    Else
        '        mSqlStr = MakeSQL
        '    End If

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColCatgeory
                mCategoryCode = Trim(.Text)

                .Col = ColCatgeoryDesc
                mCategoryDesc = Trim(.Text)

                .Col = ColSubCategory
                mSubCategoryCode = Trim(.Text)

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColItemName
                mItemName = Trim(.Text)

                .Col = ColUnit
                mItemUOM = Trim(.Text)

                .Col = ColOpening
                mOpening = Val(.Text)

                .Col = ColOpeningValue
                mOpeningValue = Val(.Text)

                .Col = ColMRRQty
                mMRRQty = Val(.Text)

                .Col = ColMRRValue
                mMRRValue = Val(.Text)

                .Col = ColReceipt
                mReceipt = Val(.Text)

                .Col = ColIssue
                mIssue = Val(.Text)

                .Col = ColClosing
                mClosing = Val(.Text)

                .Col = ColRejection
                mRej = Val(.Text)

                .Col = ColUnderQC
                mQC = Val(.Text)

                .Col = ColTotalClosing
                mTotClosing = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColValue
                mValue = Val(.Text)

                .Col = ColTransDate
                mTransDate = Trim(.Text)

                .Col = ColMaxQty
                mMaxQty = Val(.Text)

                .Col = ColMinQty
                mMinQty = Val(.Text)

                '.Col = ColConsumptionValue
                'mConsumptionValue = Trim(.Text)
                If (mCategoryCode.Length > 0) Then

                    SqlStr = " INSERT INTO TEMP_STOCKONHAND (USERID, CATEGORY_CODE, " & vbCrLf _
                    & " SUBCATEGORY_CODE, ITEM_CODE, ITEM_NAME, ITEM_UOM, " & vbCrLf _
                    & " OPENING, RECEIPT, ISSUE, CLOSING, " & vbCrLf _
                    & " REJECTION, UNDERQC, DEPT_STOCK, TOT_CLOSING, RATE," & vbCrLf _
                    & " VALUE, LAST_TRANS_DATE, OPENING_VALUE, MRR_QTY, MRR_VALUE,MINIMUM_QTY, MAXIMUM_QTY) VALUES ( " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', '" & MainClass.AllowSingleQuote(mCategoryCode) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mSubCategoryCode) & "', '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mItemName) & "', '" & MainClass.AllowSingleQuote(mItemUOM) & "', " & vbCrLf _
                    & " " & Val(CStr(mOpening)) & ", " & Val(CStr(mReceipt)) & "," & Val(CStr(mIssue)) & "," & Val(CStr(mClosing)) & "," & vbCrLf _
                    & " " & Val(CStr(mRej)) & ", " & Val(CStr(mQC)) & ", " & Val(CStr(mDeptStock)) & "," & Val(CStr(mTotClosing)) & ", " & Val(CStr(mRate)) & "," & vbCrLf _
                    & " " & Val(CStr(mValue)) & ",'" & mTransDate & "', " & Val(mOpeningValue) & ", " & Val(mMRRQty) & ", " & Val(mMRRValue) & ", " & Val(mMinQty) & ", " & Val(mMaxQty) & ")"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
NextRec:

        PubDBCn.CommitTrans()


        'If PvtDBCn.State = adStateOpen Then
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        '    End If

        InsertIntoTempTable = True
        Exit Function
PrintDummyErr:
        InsertIntoTempTable = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = " SELECT STOCK.*, " & vbCrLf _
            & " CATMST.GEN_DESC, SUBCATMST.SUBCATEGORY_DESC " & vbCrLf _
            & " FROM TEMP_STOCKONHAND STOCK, INV_GENERAL_MST CATMST, " & vbCrLf _
            & " INV_SUBCATEGORY_MST SUBCATMST " & vbCrLf _
            & " WHERE  " & vbCrLf _
            & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
            & " AND CATMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND STOCK.CATEGORY_CODE=CATMST.GEN_CODE" & vbCrLf _
            & " AND SUBCATMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND STOCK.SUBCATEGORY_CODE=SUBCATMST.SUBCATEGORY_CODE" & vbCrLf _
            & " AND CATMST.COMPANY_CODE=SUBCATMST.COMPANY_CODE" & vbCrLf _
            & " AND CATMST.GEN_CODE=SUBCATMST.CATEGORY_CODE" & vbCrLf _
            & " AND CATMST.GEN_TYPE='C' " & vbCrLf _
            & " ORDER BY STOCK.CATEGORY_CODE, SUBCATMST.SUBCATEGORY_DESC, STOCK.ITEM_CODE "

        FetchRecordForReport = mSqlStr
    End Function
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

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click


        Dim SqlStr As String = ""
        PrintStatus(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdMain)

        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Show1()
        '    SprdMain.Refresh
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

        '    If chkCategory.Value = vbUnchecked Then
        '        If Trim(txtCatName.Text) = "" Then
        '            MsgInformation "Please Select Catgeory Name."
        '            FieldsVarification = False
        '            txtCatName.SetFocus
        '        Else
        '            If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND GEN_TYPE='C'") = True Then
        '                mCategoryCode = MasterNo
        '            Else
        '                MsgInformation "Invalid Catgeory Name."
        '                FieldsVarification = False
        '                txtCatName.SetFocus
        '            End If
        '
        '        End If
        '    End If
        '
        '    If chkSubCategory.Value = vbUnchecked Then
        '        If Trim(txtSubCatName.Text) = "" Then
        '            MsgInformation "Please Select Sub-Catgeory Name."
        '            FieldsVarification = False
        '            txtSubCatName.SetFocus
        '        Else
        '
        '            mCond = "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""
        '            If chkCategory.Value = vbUnchecked Then
        '                mCond = mCond & " AND CATEGORY_CODE='" & mCategoryCode & "'"
        '            End If
        '            If MainClass.ValidateWithMasterTable(txtSubCatName.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , mCond) = False Then
        '                MsgInformation "Invalid Sub-Catgeory Name."
        '                FieldsVarification = False
        '                txtSubCatName.SetFocus
        '            End If
        '        End If
        '    End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function Show1() As Boolean

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If lblBookType.Text = "N" Then
            SqlStr = MakeSQL2()
        Else
            SqlStr = MakeSQL()
        End If

        'If optType(1).Checked = True Then
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mSqlStr = "DELETE FROM TEMP_STOCKREG WHERE USERID ='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(mSqlStr)

        SqlStr = " INSERT INTO TEMP_STOCKREG ( USERID, COMPANY_CODE, GROUP_NAME, CATEGORY_NAME,SUBCATEGORY_CODE,SUBCATEGORY_NAME, " & vbCrLf _
                    & " ITEM_CODE,ITEM_NAME,ITEM_UOM, MAT_THICHNESS, ITEM_COLOR, MINIMUM_QTY, MAXIMUM_QTY, " & vbCrLf _
                    & " OPENING,OPENING_VALUE, MRR_QTY, MRR_VALUE, RECEIPT,RECEIPT_VALUE ,ISSUE,ISSUE_VALUE,CLOSING," & vbCrLf _
                    & " REJ_QTY,UNDERQC_QTY,DEPT_QTY,TOTAL_QTY," & vbCrLf _
                    & " RATE,VALUE, CONSUMPTION_QTY, CONSUMPTION_VALUE,OTHER_ISSUE_QTY, OTHER_ISSUE_VALUE, " & vbCrLf _
                    & " ASSETS_VALUE, LAST_TRANS_DATE,LEDGER_HEAD,LEDGER_AMT,CLOSING_NOS )" & vbCrLf _
                    & SqlStr


        PubDBCn.Execute(SqlStr)

        'If chkRate.CheckState = System.Windows.Forms.CheckState.Checked Then

        '    mSqlStr = "SELECT * FROM TEMP_STOCKREG  WHERE USERID ='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' AND CLOSING>0"
        '    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        '    Dim mItemCode As String
        '    Dim mItemUOM As String = ""
        '    Dim mClosing As Double
        '    Dim mRate As Double
        '    Dim mTotValue As Double
        '    Dim mTotalValue As Double
        '    Dim mCompanyCode As Long
        '    Dim mProdType As String

        '    If RsTemp.EOF = False Then
        '        Do While RsTemp.EOF = False
        '            mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
        '            mItemUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
        '            mClosing = IIf(IsDBNull(RsTemp.Fields("CLOSING").Value), 0, RsTemp.Fields("CLOSING").Value)
        '            mCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), -1, RsTemp.Fields("COMPANY_CODE").Value)


        '            If mClosing <= 0 Then
        '                mRate = CDbl("0.00")
        '            Else
        '                mProdType = GetProductionType(mItemCode)
        '                If mProdType = "I" Or mProdType = "P" Then
        '                    mClosing = 1
        'mTotValue = GetLatestFIFORate(mCompanyCode, mItemCode, mItemUOM, mClosing, (txtDateTo.Text), "L")
        '                Else
        '                    If chkRate.CheckState = System.Windows.Forms.CheckState.Checked Then
        '                        mTotValue = GetLatestFIFORate(mCompanyCode, mItemCode, mItemUOM, mClosing, (txtDateTo.Text), "L")
        '                    Else
        '                        mTotValue = 0
        '                    End If
        '                End If
        '                mRate = VB6.Format(mTotValue / mClosing, "0.00")
        '            End If

        '            mSqlStr = "UPDATE TEMP_STOCKREG SET RATE=" & mRate & ", VALUE=" & mTotValue & "" & vbCrLf _
        '                & " WHERE USERID ='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' AND COMPANY_CODE=" & mCompanyCode & " AND ITEM_CODE='" & mItemCode & "'"


        '            PubDBCn.Execute(mSqlStr)

        '            RsTemp.MoveNext()
        '        Loop
        '    End If
        'End If

        PubDBCn.CommitTrans()

        If optType(0).Checked = True Then
            SqlStr = " SELECT '', GROUP_NAME, CATEGORY_NAME, SUBCATEGORY_CODE, SUBCATEGORY_NAME, " & vbCrLf _
                    & " ITEM_CODE,ITEM_NAME,ITEM_UOM,SUM(OPENING) AS OPENING,SUM(MRR_QTY),SUM(ISSUE),SUM(CLOSING),SUM(REJ_QTY),SUM(DECODE(TOTAL_QTY,0,RATE,VALUE/TOTAL_QTY)),  SUM(MINIMUM_QTY), SUM(MAXIMUM_QTY), " & vbCrLf _
                    & " SUM(OPENING_VALUE), SUM(MRR_VALUE), SUM(RECEIPT), SUM(RECEIPT_VALUE),SUM(ISSUE_VALUE)," & vbCrLf _
                    & " SUM(UNDERQC_QTY),SUM(DEPT_QTY),SUM(TOTAL_QTY)," & vbCrLf _
                    & " SUM(VALUE), SUM(CONSUMPTION_QTY),SUM(CONSUMPTION_VALUE), " & vbCrLf _
                    & " SUM(OTHER_ISSUE_QTY), SUM(OTHER_ISSUE_VALUE), SUM(ASSETS_VALUE),MAT_THICHNESS, ITEM_COLOR,MAX(LAST_TRANS_DATE),LEDGER_HEAD, " & vbCrLf _
                    & " SUM(LEDGER_AMT) AS LEDGER_AMT, SUM(CLOSING_NOS) AS CLOSING_NOS FROM TEMP_STOCKREG  " & vbCrLf _
                    & " WHERE USERID ='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                    & " GROUP BY GROUP_NAME, CATEGORY_NAME,SUBCATEGORY_CODE,SUBCATEGORY_NAME,ITEM_CODE,ITEM_NAME,ITEM_UOM,ITEM_UOM, MAT_THICHNESS,ITEM_COLOR,LEDGER_HEAD ORDER BY ITEM_CODE  "
        Else
            SqlStr = " SELECT '', GROUP_NAME, CATEGORY_NAME,'', ''," & vbCrLf _
                    & " '','','', '','','',''," & vbCrLf _
                    & " SUM(OPENING) AS OPENING,SUM(OPENING_VALUE), SUM(MRR_QTY), SUM(MRR_VALUE),SUM(RECEIPT),SUM(ISSUE),SUM(RECEIPT_VALUE),SUM(ISSUE_VALUE),SUM(CLOSING)," & vbCrLf _
                    & " SUM(REJ_QTY),SUM(UNDERQC_QTY),SUM(DEPT_QTY),SUM(TOTAL_QTY)," & vbCrLf _
                    & " 0,SUM(VALUE), SUM(CONSUMPTION_QTY),SUM(CONSUMPTION_VALUE), " & vbCrLf _
                    & " SUM(OTHER_ISSUE_QTY), SUM(OTHER_ISSUE_VALUE), SUM(ASSETS_VALUE), MAX(LAST_TRANS_DATE)," & vbCrLf _
                    & " LEDGER_HEAD,SUM(LEDGER_AMT), SUM(CLOSING_NOS) AS CLOSING_NOS FROM TEMP_STOCKREG  " & vbCrLf _
                    & " WHERE USERID ='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                    & " GROUP BY GROUP_NAME, CATEGORY_NAME,LEDGER_HEAD " 'ORDER BY ITEM_CODE  "
        End If

        'End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Show1 = True
        Exit Function
InsertErr:
        Show1 = False
        PubDBCn.RollbackTrans()
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
        Dim mHavingClause As Boolean
        Dim mTableName As String
        Dim mToDate As String
        Dim mDeptFunction As String
        Dim mDivision As Double

        Dim mRMCatCode As String = ""
        Dim mRMCatCodeStr As String = ""
        Dim CntLst As Integer
        Dim mMaterialType As String
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""

        Dim mCompanyCodeStr As String = ""
        Dim mStockTypeStr As String = ""
        Dim mStockType As String = ""


        mHavingClause = False
        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        mTableName = ConInventoryTable


        mDeptFunction = "GETDEPTSTOCK"

        Dim mWareHouse As String

        If cboExportItem.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboExportItem.CheckedRows
                If mWareHouse <> "" Then
                    mWareHouse += "," & "'" & r.Cells("STOCK_ID").Value.ToString() & "'"
                Else
                    mWareHouse += "'" & r.Cells("STOCK_ID").Value.ToString() & "'"
                End If
            Next
        End If

        ''SUM(GETDEPTSTOCK(INV.COMPANY_CODE," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,'" & mToDate & "')))
        '& " TO_CHAR(SUM(GETDEPTSTOCK(INV.COMPANY_CODE," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,'" & mToDate & "'))) as DeptClosing, " & vbCrLf _
        '   & " TO_CHAR(SUM(" & mDeptFunction & "(INV.COMPANY_CODE," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,'" & mToDate & "'))+SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as TotClosing, " & vbCrLf _

        'GETDEPTSTOCK(INV.COMPANY_CODE," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,'" & mToDate & "') 

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "',  INV.COMPANY_CODE, " & vbCrLf _
            & " ITEM.CATEGORY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_CODE, SC.SUBCATEGORY_DESC, ITEM.ITEM_CODE, " & vbCrLf _
            & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, ITEM.MAT_THICHNESS, ITEM.ITEM_COLOR, ITEM.MINIMUM_QTY, ITEM.MAXIMUM_QTY, "

        ''AND STOCK_TYPE IN ('ST','CS','FG')

        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening, "

        ''AND STOCK_TYPE IN ('ST','CS','FG')

        SqlStr = SqlStr & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE,SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS OpeningValue, "


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','FG') AND REF_TYPE IN ('MRR') AND REF_FLAG='P' AND NVL(ACM.INTER_UNIT,'N')='N'" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRQty, "

            'GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, )

            SqlStr = SqlStr & vbCrLf _
                     & " GETMRRITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),ITEM.ITEM_CODE,  INV.COMPANY_CODE, SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','FG') AND REF_TYPE IN ('MRR') AND REF_FLAG='P' AND NVL(ACM.INTER_UNIT,'N')='N' THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRValue, "
        Else
            SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','FG') AND REF_TYPE IN ('MRR') AND REF_FLAG='P'" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRQty, "

            'GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, )

            SqlStr = SqlStr & vbCrLf _
                     & " GETMRRITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),ITEM.ITEM_CODE,  INV.COMPANY_CODE, SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','FG') AND REF_TYPE IN ('MRR') AND REF_FLAG='P' THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRValue, "
        End If

        If cboExportItem.Text.Trim = "STORE" Or cboExportItem.Text.Trim = "PRODUCTION" Or cboExportItem.Text.Trim = "SUB STORE" Then
            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
                & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS Receipt, "

            ''AND STOCK_TYPE IN ('ST','CS','FG')
            SqlStr = SqlStr & vbCrLf _
                & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, " & vbCrLf _
                & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END))  " & vbCrLf _
                & " as ReceiptValue,"

            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
                & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS Issue, " ''AND STOCK_TYPE IN ('ST','CS','FG')

            SqlStr = SqlStr & vbCrLf _
                & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, " & vbCrLf _
                & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END) + SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END)) - " & vbCrLf _
                & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, " & vbCrLf _
                & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) " & vbCrLf _
                & " as ISSUEValue,"
        Else
            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')   " & vbCrLf _
                & " THEN ITEM_QTY * CASE WHEN REF_TYPE='ISS' THEN DECODE(ITEM_IO,'I',1,-1) ELSE DECODE(ITEM_IO,'I',1,0) END ELSE 0 END)) AS Receipt, "  ''AND STOCK_TYPE IN ('ST','CS','FG')

            SqlStr = SqlStr & vbCrLf _
                & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, " & vbCrLf _
                & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ITEM_QTY * CASE WHEN REF_TYPE='ISS' THEN DECODE(ITEM_IO,'I',1,-1) ELSE DECODE(ITEM_IO,'I',1,0) END ELSE 0 END))  " & vbCrLf _
                & " as ReceiptValue,"

            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
                & " THEN ITEM_QTY * CASE WHEN REF_TYPE='ISS' THEN DECODE(ITEM_IO,'I',-1,1) ELSE DECODE(ITEM_IO,'I',0,1) END ELSE 0 END)) AS Issue, "  ''AND STOCK_TYPE IN ('ST','CS','FG')

            SqlStr = SqlStr & vbCrLf _
                & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, " & vbCrLf _
                & " SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) + SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ITEM_QTY * CASE WHEN REF_TYPE='ISS' THEN DECODE(ITEM_IO,'I',1,-1) ELSE DECODE(ITEM_IO,'I',1,0) END ELSE 0 END)) - " & vbCrLf _
                & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, " & vbCrLf _
                & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) " & vbCrLf _
                & " as ISSUEValue,"

        End If

        ''DECODE(STOCK_TYPE,'ST',1,DECODE(STOCK_TYPE,'CS',1,DECODE(STOCK_TYPE,'FG',1,0))) * 
        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as Closing, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE='RJ'  " & vbCrLf _
            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Rejection, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND (STOCK_TYPE='QC' OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS UnderQC, " & vbCrLf _
            & " 0  as DEPT_QTY, " & vbCrLf _
            & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))  as TotClosing, " & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, 1) as Rate, " & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as Value,"

        SqlStr = SqlStr & vbCrLf _
            & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) * CASE WHEN (INV.REF_TYPE IN ('DTN','FBU','PDM','PIS','PMD','PMO','PMS','PRW','RWP','SCP','SIS','WBU') OR INV.REF_TYPE || INV.ITEM_IO IN ('ADJO') OR INV.REF_TYPE||INV.REF_FLAG IN ('ISSG')) AND E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END ) as ConQty, " & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) * CASE WHEN (INV.REF_TYPE IN ('DTN','FBU','PDM','PIS','PMD','PMS','PRW','RWP','SCP','SIS','WBU') OR INV.REF_TYPE || INV.ITEM_IO IN ('ADJO') OR INV.REF_TYPE||INV.REF_FLAG IN ('ISSG')) AND E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as ConValue,"

        SqlStr = SqlStr & vbCrLf _
            & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) * CASE WHEN (INV.REF_TYPE IN ('RGP','NRG','DSP') OR (INV.REF_TYPE IN ('MRR') AND REF_FLAG='R')) AND E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END ) as OtherQty, " & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) * CASE WHEN (INV.REF_TYPE IN ('RGP','NRG','DSP') OR (INV.REF_TYPE IN ('MRR') AND REF_FLAG='R')) AND E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END ) ) as OtherValue,"

        SqlStr = SqlStr & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) * CASE WHEN INV.REF_TYPE||INV.REF_FLAG IN ('ISSC') AND E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as ASSETS_VALUE," & vbCrLf _
            & " '' "

        ''ASSETS_VALUE
        '''ISSC',
        ''CASE WHEN PRD_TYPE IN ('3','B','R') THEN GETDEPTSTOCK(INV.COMPANY_CODE," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) ELSE 0 END

        '& " TO_CHAR(0) as Rate, " & vbCrLf _
        '  & " TO_CHAR(CASE WHEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>=0 THEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))*AVG(NVL(INV.NEW_RATE,0)) ELSE 0 END) as Value "

        '& " REJ_QTY,UNDERQC_QTY,DEPT_QTY,TOTAL_QTY," & vbCrLf _
        '           & " RATE,VALUE,LAST_TRANS_DATE )" & vbCrLf _
        '           & SqlStr
        ''     & " GETMRRITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS','FG') AND REF_TYPE IN ('MRR') AND NVL(INTER_UNIT,'N')='N' THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRValue, "


        If chkShowLedgerBalance.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf _
                & " , NVL(ACCMST.SUPP_CUST_NAME,'') AS LEDGER_HEAD" & vbCrLf _
                & " , GETITEMLEDGERAMOUNT(INV.COMPANY_CODE, INV.FYEAR, ITEM.ITEM_CODE, NVL(GMST.ACCT_CONSUM_CODE,''), TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS LEDGER_AMT"
        Else
            SqlStr = SqlStr & vbCrLf _
                & " , '' AS LEDGER_HEAD" & vbCrLf _
                & " , '' AS LEDGER_AMT"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " , SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * (CASE WHEN INV.ITEM_UOM ='SQM' AND NVL(MAT_LEN,0) * NVL(MAT_WIDTH,0) > 0 THEN 1 ELSE 0 END)/ CASE WHEN NVL(MAT_LEN,0) * NVL(MAT_WIDTH,0) > 0 THEN MAT_LEN * MAT_WIDTH * 0.000001 ELSE 1 END) AS CLOSING_NOS"


        SqlStr = SqlStr & vbCrLf _
            & " FROM " & mTableName & " INV, " & vbCrLf _
            & " INV_ITEM_MST ITEM, FIN_SUPP_CUST_MST ACM, INV_GENERAL_MST GMST, INV_SUBCATEGORY_MST SC "

        If chkShowLedgerBalance.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & ", FIN_SUPP_CUST_MST ACCMST"
        End If
        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf _
            & " WHERE INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


            'If cboExportItem.SelectedIndex = 0 Then
            '    SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'"
            'ElseIf cboExportItem.SelectedIndex = 1 Then
            '    SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConPH & "'"
            'Else
            '    SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConSH & "'"
            'End If



            If mWareHouse = "" Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID = 'WH'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID IN (" & mWareHouse & ")"
        End If


        If lstStockType.GetItemChecked(0) = True Then
            mStockTypeStr = ""
        Else
            For CntLst = 1 To lstStockType.Items.Count - 1
                If lstStockType.GetItemChecked(CntLst) = True Then
                    mStockType = VB6.GetItemString(lstStockType, CntLst)

                    mStockTypeStr = IIf(mStockTypeStr = "", "'" & mStockType & "'", mStockTypeStr & "," & "'" & mStockType & "'")
                End If
            Next
        End If

        If mStockTypeStr = "" Then
            'SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE NOT IN ('CS','SC')"
        Else
            mStockTypeStr = "(" & mStockTypeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN " & mStockTypeStr & ""
        End If

        'SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('ST','RJ','QC'"

        'If chkCS.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    SqlStr = SqlStr & vbCrLf & " ,'CS'"
        'End If

        'If chkFGStockRequired.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    SqlStr = SqlStr & vbCrLf & " ,'FG'"
        'End If

        'SqlStr = SqlStr & vbCrLf & " ) "

        SqlStr = SqlStr & vbCrLf _
            & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf _
            & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf _
            & " AND INV.COMPANY_CODE=ACM.COMPANY_CODE(+)" & vbCrLf _
            & " AND INV.PARTYCODE=ACM.SUPP_CUST_CODE(+) "

        If chkShowLedgerBalance.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf _
                & " AND GMST.COMPANY_CODE=ACCMST.COMPANY_CODE(+)" & vbCrLf _
                & " AND GMST.ACCT_CONSUM_CODE=ACCMST.SUPP_CUST_CODE(+) "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND ITEM.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND ITEM.CATEGORY_CODE=GMST.GEN_CODE "

        SqlStr = SqlStr & vbCrLf _
            & " AND ITEM.COMPANY_CODE=SC.COMPANY_CODE" & vbCrLf _
            & " AND ITEM.CATEGORY_CODE=SC.CATEGORY_CODE " & vbCrLf _
            & " AND ITEM.SUBCATEGORY_CODE=SC.SUBCATEGORY_CODE "

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INV.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboSubCategory.Text <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND SC.SUBCATEGORY_DESC='" & MainClass.AllowSingleQuote(cboSubCategory.Text) & "'"
        End If


        If lstMaterialType.GetItemChecked(0) = True Then
            mRMCatCodeStr = ""
        Else
            For CntLst = 1 To lstMaterialType.Items.Count - 1
                If lstMaterialType.GetItemChecked(CntLst) = True Then
                    mMaterialType = VB6.GetItemString(lstMaterialType, CntLst)
                    If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                        mRMCatCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
                End If
            Next
        End If

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN " & mRMCatCodeStr & ""
        End If

        Dim mAccountName As String
        Dim mAccountCode As String
        Dim mAccountCodeStr As String

        If lstAccountMapping.GetItemChecked(0) = True Then
            mAccountCodeStr = ""
        Else
            For CntLst = 1 To lstAccountMapping.Items.Count - 1
                If lstAccountMapping.GetItemChecked(CntLst) = True Then
                    mAccountName = VB6.GetItemString(lstAccountMapping, CntLst)
                    If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mAccountCodeStr = IIf(mAccountCodeStr = "", "'" & mAccountCode & "'", mAccountCodeStr & "," & "'" & mAccountCode & "'")
                End If
            Next
        End If

        If mAccountCodeStr <> "" Then
            mAccountCodeStr = "(" & mAccountCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND GMST.ACCT_CONSUM_CODE IN " & mAccountCodeStr & ""
        End If

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
            SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
            End If
        End If


        ''AND DEPT_CODE_TO='STR'
        If cboShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        ElseIf cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        End If

        If Val(txtLocation.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_LOCATION ='" & txtLocation.Text & "'"
        End If

        'If cboExportItem.SelectedIndex >= 1 Then
        '    SqlStr = SqlStr & vbCrLf & " AND ITEM.IS_EXPORT_ITEM = '" & VB.Left(cboExportItem.Text, 1) & "'"
        'End If

        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"
        End If

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        If chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
            mHavingClause = True
        Else
            If lblBookType.Text = "B" Then
                SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<ITEM.MINIMUM_QTY"
                mHavingClause = True
            ElseIf lblBookType.Text = "A" Then
                SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>ITEM.MAXIMUM_QTY"
                mHavingClause = True
            End If
        End If

        If chkZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            If mHavingClause = False Then
                SqlStr = SqlStr & vbCrLf & " HAVING "
                mHavingClause = True
            Else
                SqlStr = SqlStr & vbCrLf & " AND "
            End If

            SqlStr = SqlStr & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"
        End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf _
            & "  INV.COMPANY_CODE, ITEM.CATEGORY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_CODE,SC.SUBCATEGORY_DESC, ITEM.ITEM_CODE, " & vbCrLf _
            & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, ITEM.MAT_THICHNESS, ITEM.ITEM_COLOR, ITEM.MINIMUM_QTY, ITEM.MAXIMUM_QTY,PRD_TYPE,NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE) "

        If chkShowLedgerBalance.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf _
                & " , NVL(ACCMST.SUPP_CUST_NAME,''), GETITEMLEDGERAMOUNT(INV.COMPANY_CODE, INV.FYEAR,ITEM.ITEM_CODE, NVL(GMST.ACCT_CONSUM_CODE,''), TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        End If

        If lblBookType.Text = "B" Then
            SqlStr = SqlStr & vbCrLf & " , ITEM.MINIMUM_QTY"
        ElseIf lblBookType.Text = "A" Then
            SqlStr = SqlStr & vbCrLf & " , ITEM.MAXIMUM_QTY"
        End If

        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & "  INV.COMPANY_CODE, ITEM.CATEGORY_CODE, SC.SUBCATEGORY_CODE, ITEM.ITEM_CODE "


        MakeSQL = SqlStr
        Exit Function
InsertErr:
        MakeSQL = ""
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Function MakeSQL2() As String

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""
        Dim mSubCategoryCode As String
        Dim mCond As String
        Dim mItemCode As String
        Dim mFromDate As String
        Dim mToDate As String
        Dim mTableName As String
        Dim mDeptFunction As String
        Dim mDivision As Double
        Dim mRMCatCode As String = ""
        Dim mRMCatCodeStr As String = ""
        Dim CntLst As Integer
        Dim mMaterialType As String
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""
        Dim mStockTypeStr As String = ""
        Dim mStockType As String = ""
        Dim mCompanyCodeStr As String = ""

        mFromDate = txtDateFrom.Text

        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        mTableName = ConInventoryTable

        mDeptFunction = "GETDEPTSTOCK"

        '& " TO_CHAR(SUM(" & mDeptFunction & "(INV.COMPANY_CODE," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,'" & mToDate & "'))) as DeptClosing, " & vbCrLf _
        '   & " TO_CHAR(SUM(" & mDeptFunction & "(INV.COMPANY_CODE," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,'" & mToDate & "'))+SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as TotClosing, " & vbCrLf _

        ''GETDEPTSTOCK(INV.COMPANY_CODE," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,'" & mToDate & "')

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "',  INV.COMPANY_CODE, " & vbCrLf _
            & " ITEM.CATEGORY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_CODE, SC.SUBCATEGORY_DESC, ITEM.ITEM_CODE, " & vbCrLf _
            & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, ITEM.MAT_THICHNESS, ITEM.ITEM_COLOR,ITEM.MINIMUM_QTY, ITEM.MAXIMUM_QTY, "

        ''AND STOCK_TYPE IN ('ST','CS','FG') 

        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening, "

        'SqlStr = SqlStr & vbCrLf _
        '    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS','FG') " & vbCrLf _
        '    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS Receipt, " & vbCrLf _
        '    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS','FG') " & vbCrLf _
        '    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS Issue, "

        SqlStr = SqlStr & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE,SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS OpeningValue, "   ''AND STOCK_TYPE IN ('ST','CS','FG')

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND REF_TYPE IN ('MRR') AND REF_FLAG='P' AND NVL(ACM.INTER_UNIT,'N')='N'" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRQty, "

            ''AND STOCK_TYPE IN ('ST','CS','FG')
            'GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, )

            SqlStr = SqlStr & vbCrLf _
                    & " GETMRRITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('MRR') AND REF_FLAG='P' AND NVL(ACM.INTER_UNIT,'N')='N' THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRValue, "

            ''AND STOCK_TYPE IN ('ST','CS','FG') 
        Else
            SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND REF_TYPE IN ('MRR') AND REF_FLAG='P' " & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRQty, "

            ''AND STOCK_TYPE IN ('ST','CS','FG')
            'GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, )

            SqlStr = SqlStr & vbCrLf _
                    & " GETMRRITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('MRR') AND REF_FLAG='P' THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRValue, "

            ''AND STOCK_TYPE IN ('ST','CS','FG') 
        End If


        If cboExportItem.Text.Trim = "STORE" Or cboExportItem.Text.Trim = "PRODUCTION" Or cboExportItem.Text.Trim = "SUB STORE" Then
            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
                & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS Receipt, "

            SqlStr = SqlStr & vbCrLf _
                & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, " & vbCrLf _
                & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END))  " & vbCrLf _
                & " as ReceiptValue,"


            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS Issue, "

            SqlStr = SqlStr & vbCrLf _
               & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, " & vbCrLf _
               & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END) + SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END)) - " & vbCrLf _
               & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, " & vbCrLf _
               & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) " & vbCrLf _
               & " as ISSUEValue,"

        Else
            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " THEN ITEM_QTY * CASE WHEN REF_TYPE='ISS' THEN DECODE(ITEM_IO,'I',1,-1) ELSE DECODE(ITEM_IO,'I',1,0) END ELSE 0 END)) AS Receipt, "

            SqlStr = SqlStr & vbCrLf _
                & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, " & vbCrLf _
                & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ITEM_QTY * CASE WHEN REF_TYPE='ISS' THEN DECODE(ITEM_IO,'I',1,-1) ELSE DECODE(ITEM_IO,'I',1,0) END ELSE 0 END))  " & vbCrLf _
                & " as ReceiptValue,"

            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " THEN ITEM_QTY * CASE WHEN REF_TYPE='ISS' THEN DECODE(ITEM_IO,'I',-1,1) ELSE DECODE(ITEM_IO,'I',0,1) END ELSE 0 END)) AS Issue, "

            SqlStr = SqlStr & vbCrLf _
              & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, " & vbCrLf _
              & " SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) + SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ITEM_QTY * CASE WHEN REF_TYPE='ISS' THEN DECODE(ITEM_IO,'I',1,-1) ELSE DECODE(ITEM_IO,'I',1,0) END ELSE 0 END)) - " & vbCrLf _
              & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, " & vbCrLf _
              & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) " & vbCrLf _
              & " as ISSUEValue,"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END)) as Closing, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE='RJ' " & vbCrLf _
            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Rejection, " & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND (STOCK_TYPE='QC' OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS UnderQC, " & vbCrLf _
            & " 0 as DEPT_QTY, " & vbCrLf _
            & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) as TotClosing, " & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, 1) as Rate, " & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) *  CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as Value, " & vbCrLf _
            & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',-1,1)  * CASE WHEN (INV.REF_TYPE IN ('DTN','FBU','PDM','PIS','PMD','PMO','PMS','PRW','RWP','SCP','SIS','WBU') OR INV.REF_TYPE || INV.ITEM_IO IN ('ADJO') OR INV.REF_TYPE||INV.REF_FLAG IN ('ISSG')) AND E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END ) as ConQty, " & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',-1,1)  * CASE WHEN (INV.REF_TYPE IN ('DTN','FBU','PDM','PIS','PMD','PMO','PMS','PRW','RWP','SCP','SIS','WBU') OR INV.REF_TYPE || INV.ITEM_IO IN ('ADJO') OR INV.REF_TYPE||INV.REF_FLAG IN ('ISSG')) AND E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as ConValue, "

        SqlStr = SqlStr & vbCrLf _
            & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) * CASE WHEN (INV.REF_TYPE IN ('RGP','NRG','DSP') OR (INV.REF_TYPE IN ('MRR') AND REF_FLAG='R')) AND E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END ) as OtherQty, " & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) * CASE WHEN (INV.REF_TYPE IN ('RGP','NRG','DSP') OR (INV.REF_TYPE IN ('MRR') AND REF_FLAG='R')) AND E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END ) ) as OtherValue,"


        SqlStr = SqlStr & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) *  CASE WHEN INV.REF_TYPE||INV.REF_FLAG IN ('ISSC') AND E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as ASSETS_VALUE," & vbCrLf _
            & " MAX(REF_DATE) AS LAST_TRANS_DATE "

        '''ISSC',
        'If chkShowLedgerBalance.CheckState = System.Windows.Forms.CheckState.Checked   Then
        '    SqlStr = SqlStr & vbCrLf _
        '        & " , NVL(ACCMST.SUPP_CUST_NAME,'') AS LEDGER_HEAD" & vbCrLf _
        '        & " , '' AS LEDGER_AMT"
        'Else
        SqlStr = SqlStr & vbCrLf _
            & " , '' AS LEDGER_HEAD" & vbCrLf _
            & " , '' AS LEDGER_AMT"
        'End If

        SqlStr = SqlStr & vbCrLf _
            & " , SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * (CASE WHEN INV.ITEM_UOM ='SQM' AND NVL(MAT_LEN,0) * NVL(MAT_WIDTH,0) > 0 THEN 1 ELSE 0 END)/ CASE WHEN NVL(MAT_LEN,0) * NVL(MAT_WIDTH,0) > 0 THEN MAT_LEN * MAT_WIDTH * 0.000001 ELSE 1 END) AS CLOSING_NOS"


        SqlStr = SqlStr & vbCrLf _
            & " FROM " & mTableName & " INV, " & vbCrLf _
            & " INV_ITEM_MST ITEM, FIN_SUPP_CUST_MST ACM, INV_GENERAL_MST GMST, INV_SUBCATEGORY_MST SC "

        ''CASE WHEN PRD_TYPE IN ('3','B','R') THEN GETDEPTSTOCK(INV.COMPANY_CODE," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) ELSE 0 END

        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " WHERE "

        'SqlStr = SqlStr & vbCrLf _
        '    & " INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " And INV.STOCK_ID='" & ConWH & "'"

        SqlStr = SqlStr & vbCrLf _
            & " INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

            'If cboExportItem.SelectedIndex = 0 Then
            '    SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'"
            'ElseIf cboExportItem.SelectedIndex = 1 Then
            '    SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConPH & "'"
            'Else
            '    SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConSH & "'"
            'End If

            Dim mWareHouse As String

        If cboExportItem.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboExportItem.CheckedRows
                If mWareHouse <> "" Then
                    mWareHouse += "," & "'" & r.Cells("STOCK_ID").Value.ToString() & "'"
                Else
                    mWareHouse += "'" & r.Cells("STOCK_ID").Value.ToString() & "'"
                End If
            Next
        End If

        If mWareHouse = "" Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID = 'WH'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID IN (" & mWareHouse & ")"
        End If


        SqlStr = SqlStr & vbCrLf _
            & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf _
            & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf _
            & " AND INV.COMPANY_CODE=ACM.COMPANY_CODE(+)" & vbCrLf _
            & " AND INV.PARTYCODE=ACM.SUPP_CUST_CODE(+) "

        SqlStr = SqlStr & vbCrLf _
            & " AND ITEM.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND ITEM.CATEGORY_CODE=GMST.GEN_CODE "

        SqlStr = SqlStr & vbCrLf _
            & " AND ITEM.COMPANY_CODE=SC.COMPANY_CODE" & vbCrLf _
            & " AND ITEM.CATEGORY_CODE=SC.CATEGORY_CODE " & vbCrLf _
            & " AND ITEM.SUBCATEGORY_CODE=SC.SUBCATEGORY_CODE "

        If cboShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        ElseIf cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INV.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboSubCategory.Text <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND SC.SUBCATEGORY_DESC='" & MainClass.AllowSingleQuote(cboSubCategory.Text) & "'"
        End If

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
            SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If lstMaterialType.GetItemChecked(0) = True Then
            mRMCatCodeStr = ""
        Else
            For CntLst = 1 To lstMaterialType.Items.Count - 1
                If lstMaterialType.GetItemChecked(CntLst) = True Then
                    mMaterialType = VB6.GetItemString(lstMaterialType, CntLst)
                    If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                        mRMCatCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
                End If
            Next
        End If

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN " & mRMCatCodeStr & ""
        End If

        Dim mAccountName As String
        Dim mAccountCode As String
        Dim mAccountCodeStr As String

        If lstAccountMapping.GetItemChecked(0) = True Then
            mAccountCodeStr = ""
        Else
            For CntLst = 1 To lstAccountMapping.Items.Count - 1
                If lstAccountMapping.GetItemChecked(CntLst) = True Then
                    mAccountName = VB6.GetItemString(lstAccountMapping, CntLst)
                    If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mAccountCodeStr = IIf(mAccountCodeStr = "", "'" & mAccountCode & "'", mAccountCodeStr & "," & "'" & mAccountCode & "'")
                End If
            Next
        End If

        If mAccountCodeStr <> "" Then
            mAccountCodeStr = "(" & mAccountCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND GMST.ACCT_CONSUM_CODE IN " & mAccountCodeStr & ""
        End If


        '    If chkCategory.Value = vbUnchecked Then
        '        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND GEN_TYPE='C'") = True Then
        '            mCategoryCode = MasterNo
        '            SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE='" & mCategoryCode & "'"
        '        End If
        '    End If
        '
        '    If chkSubCategory.Value = vbUnchecked Then
        '        mCond = "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""
        '        If chkCategory.Value = vbUnchecked Then
        '            mCond = mCond & " AND CATEGORY_CODE='" & mCategoryCode & "'"
        '        End If
        '        If MainClass.ValidateWithMasterTable(txtSubCatName.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , mCond) = True Then
        '            mSubCategoryCode = MasterNo
        '            SqlStr = SqlStr & vbCrLf & " AND ITEM.SUBCATEGORY_CODE='" & mSubCategoryCode & "'"
        '        End If
        '    End If

        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
            End If
        End If

        'If chkCS.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('CS','ST','RJ','QC') "
        'Else
        '    SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('ST','RJ','QC') "
        'End If

        If lstStockType.GetItemChecked(0) = True Then
            mStockTypeStr = ""
        Else
            For CntLst = 1 To lstStockType.Items.Count - 1
                If lstStockType.GetItemChecked(CntLst) = True Then
                    mStockType = VB6.GetItemString(lstStockType, CntLst)

                    mStockTypeStr = IIf(mStockTypeStr = "", "'" & mStockType & "'", mStockTypeStr & "," & "'" & mStockType & "'")
                End If
            Next
        End If

        If mStockTypeStr = "" Then
            'SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE NOT IN ('CS','SC')"
        Else
            mStockTypeStr = "(" & mStockTypeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN " & mStockTypeStr & ""
        End If

        ''AND DEPT_CODE_TO='STR'

        If Val(txtLocation.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_LOCATION ='" & txtLocation.Text & "'"
        End If

        'If cboExportItem.SelectedIndex >= 1 Then
        '    SqlStr = SqlStr & vbCrLf & " AND ITEM.IS_EXPORT_ITEM = '" & VB.Left(cboExportItem.Text, 1) & "'"
        'End If

        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"
        End If

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If


        If chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        Else
            If optNonMoving(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & "HAVING MAX(REF_DATE)<TO_DATE('" & VB6.Format(mFromDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') "
            Else
                SqlStr = SqlStr & vbCrLf & "HAVING MAX(REF_DATE) >TO_DATE('" & VB6.Format(mFromDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,0))<>0 AND SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,0))=0"
            End If
        End If

        If chkZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"
        End If

        SqlStr = SqlStr & vbCrLf _
            & "GROUP BY " & vbCrLf _
            & " INV.COMPANY_CODE, ITEM.CATEGORY_CODE, GMST.GEN_DESC,SC.SUBCATEGORY_CODE, SC.SUBCATEGORY_DESC, ITEM.ITEM_CODE, " & vbCrLf _
            & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, ITEM.MAT_THICHNESS, ITEM.ITEM_COLOR,ITEM.MINIMUM_QTY, ITEM.MAXIMUM_QTY, PRD_TYPE,NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE)"


        SqlStr = SqlStr & vbCrLf _
            & "ORDER BY " & vbCrLf _
            & "  INV.COMPANY_CODE, ITEM.CATEGORY_CODE, SC.SUBCATEGORY_CODE, ITEM.ITEM_CODE "

        ''NVL(ITEM.PARENT_CODE,ITEM.ITEM_CODE)

        MakeSQL2 = SqlStr
        Exit Function
InsertErr:
        MakeSQL2 = ""
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Sub frmParamStockOnHand_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim mIsAuthorisedUser As String
        Dim SqlStr As String = ""
        If FormLoaded = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblBookType.Text = "H" Then
            Me.Text = "Inventory On Hand"
            FraConditional.Visible = True
            FraNonMoving.Visible = False
        ElseIf lblBookType.Text = "B" Then
            Me.Text = "Below than Minimum Inventory"
            FraConditional.Visible = True
            FraNonMoving.Visible = False
        ElseIf lblBookType.Text = "A" Then
            Me.Text = "Above than Maximum Inventory"
            FraConditional.Visible = True
            FraNonMoving.Visible = False
        ElseIf lblBookType.Text = "N" Then
            Me.Text = "Non-Moving Stock"
            FraConditional.Visible = False
            FraNonMoving.Visible = True
        End If


        mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        If InStr(1, mIsAuthorisedUser, "S") = 0 Then
            chkViewAll.Enabled = False
        Else
            chkViewAll.Enabled = True
        End If


        FormLoaded = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub frmParamStockOnHand_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        Dim mFromDate As String
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim dsSC As New DataSet

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)
        MainClass.SetControlsColor(Me)



        '    chkCategory.Value = vbChecked
        '    txtCatName.Enabled = False
        '    cmdsearchCategory.Enabled = False
        '
        '    chkSubCategory.Value = vbChecked
        '    txtSubCatName.Enabled = False
        '    cmdSearchSubCat.Enabled = False

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

        'mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(txtDateTo.Text)))
        txtDateFrom.Text = CStr(RunDate) ''IIf(CDate(mFromDate) < CDate(RsCompany.Fields("Start_Date").Value), RsCompany.Fields("Start_Date").Value, mFromDate)
        txtDateTo.Text = CStr(RunDate)

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

        cboClass.Items.Clear()

        cboClass.Items.Add("All")
        cboClass.Items.Add("A")
        cboClass.Items.Add("B")
        cboClass.Items.Add("C")
        cboClass.Items.Add("D")

        cboClass.SelectedIndex = 0


        lstMaterialType.Items.Clear()
        SqlStr = "SELECT GEN_DESC FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C' ORDER BY GEN_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstMaterialType.Items.Add("ALL")
            lstMaterialType.SetItemChecked(CntLst, True)
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstMaterialType.Items.Add(RS.Fields("GEN_DESC").Value)
                lstMaterialType.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstStockType.Items.Clear()
        SqlStr = "SELECT STOCK_TYPE_CODE FROM INV_TYPE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & "  ORDER BY STOCK_TYPE_CODE"
        ''AND STOCK_TYPE_CODE NOT IN ('CS','SC')

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstStockType.Items.Add("ALL")
            lstStockType.SetItemChecked(CntLst, True)
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstStockType.Items.Add(RS.Fields("STOCK_TYPE_CODE").Value)
                lstStockType.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstAccountMapping.Items.Clear()
        SqlStr = "SELECT DISTINCT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST A, INV_GENERAL_MST B" & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.SUPP_CUST_CODE=B.ACCT_CONSUM_CODE" & vbCrLf _
            & " AND GEN_TYPE='C' ORDER BY SUPP_CUST_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstAccountMapping.Items.Add("ALL")
            lstAccountMapping.SetItemChecked(CntLst, True)
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstAccountMapping.Items.Add(RS.Fields("SUPP_CUST_NAME").Value)
                lstAccountMapping.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        'lstMaterialType.SelectedIndex = 0

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

        oledbCnn = New OleDbConnection(StrConn)


        SqlStr = " Select 'STORE' AS WARE_HOUSE , 'WH' AS STOCK_ID  FROM DUAL " & vbCrLf _
            & " UNION ALL" & vbCrLf _
            & " Select 'PRODUCTION' AS WARE_HOUSE, 'PH' AS STOCK_ID FROM DUAL" & vbCrLf _
            & " UNION ALL" & vbCrLf _
            & " Select 'SUB STORE' AS WARE_HOUSE, 'SH' AS STOCK_ID FROM DUAL"

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        cboExportItem.DataSource = ds
        cboExportItem.DataMember = ""
        Dim c As UltraGridColumn = Me.cboExportItem.DisplayLayout.Bands(0).Columns.Add()
        With c
            .Key = "Selected"
            .Header.Caption = String.Empty
            .Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always
            .DataType = GetType(Boolean)
            .DataType = GetType(Boolean)
            .Header.VisiblePosition = 0
        End With
        cboExportItem.CheckedListSettings.CheckStateMember = "Selected"
        cboExportItem.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems
        ' Set up the control to use a custom list delimiter 
        cboExportItem.CheckedListSettings.ListSeparator = " , "
        ' Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
        cboExportItem.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item
        cboExportItem.DisplayMember = "WARE_HOUSE"
        cboExportItem.ValueMember = "STOCK_ID"

        cboExportItem.DisplayLayout.Bands(0).Columns(0).Header.Caption = "WareHouse"
        cboExportItem.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Stock ID"

        cboExportItem.DisplayLayout.Bands(0).Columns(0).Width = 100
        cboExportItem.DisplayLayout.Bands(0).Columns(1).Width = 50

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            cboExportItem.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown
        Else
            cboExportItem.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown
            'cboExportItem.CheckedRows
        End If



        oledbAdapter.Dispose()

        oledbCnn.Close()


        SqlStr = " Select DISTINCT SUBCATEGORY_DESC  FROM INV_SUBCATEGORY_MST " & vbCrLf _
             & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & "  ORDER BY SUBCATEGORY_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboSubCategory.Items.Clear()

        cboSubCategory.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboSubCategory.Items.Add(RS.Fields("SUBCATEGORY_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboSubCategory.SelectedIndex = 0

        FormatSprdMain(-1)
        FillSprdMain()
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub lstMaterialType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstMaterialType.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstMaterialType.GetItemChecked(0) = True Then
                    For I = 1 To lstMaterialType.Items.Count - 1
                        lstMaterialType.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstMaterialType.Items.Count - 1
                        lstMaterialType.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstMaterialType.GetItemChecked(e.Index - 1) = False Then
                    lstMaterialType.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub lstStockType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstStockType.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstMaterialType.GetItemChecked(0) = True Then
                    For I = 1 To lstStockType.Items.Count - 1
                        lstStockType.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstStockType.Items.Count - 1
                        lstStockType.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstMaterialType.GetItemChecked(e.Index - 1) = False Then
                    lstStockType.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub lstAccountMapping_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstAccountMapping.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstAccountMapping.GetItemChecked(0) = True Then
                    For I = 1 To lstAccountMapping.Items.Count - 1
                        lstAccountMapping.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstAccountMapping.Items.Count - 1
                        lstAccountMapping.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstAccountMapping.GetItemChecked(e.Index - 1) = False Then
                    lstAccountMapping.SetItemChecked(0, False)
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
    Private Sub frmParamStockOnHand_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        '    Frame4.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub frmParamStockOnHand_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub PrintStatus(ByRef PrintFlag As Boolean)
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub

    Private Sub optNonMoving_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optNonMoving.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optNonMoving.GetIndex(eventSender)
            PrintStatus(False)
        End If
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
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        PrintStatus(False)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs)
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

        On Error GoTo ErrPart
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim mClosing As Double
        Dim mPurchaseRate As Double
        Dim mRate As Double
        Dim mFactor As Double
        Dim CntRow As Integer
        Dim mLandedCost As Double
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mItemCost As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTotValue As Double
        Dim mTotalValue As Double
        Dim mCatDesc As String
        Dim mCategoryCode As String
        Dim mProdType As String
        Dim mTotConsumptionValue As Double
        Dim mTotalConsumptionValue As Double
        Dim mTotalAssetsValue As Double
        Dim mTotAssetsValue As Double
        Dim mTotalOpeningValue As Double
        Dim mTotalMRRValue As Double
        Dim mOpeningValue As Double
        Dim mMRRValue As Double
        Dim mReceiptValue As Double
        Dim mIssueValue As Double
        Dim mTotLedgerAmount As Double
        Dim mLedgerAmount As Double
        Dim mTotRGPValue As Double
        Dim mTotalRGPValue As Double
        Dim mClosingNos As Double
        Dim mTotalClosingNos As Double

        Dim mTotReceiptValue As Double
        Dim mTotIssueValue As Double
        With SprdMain
            'If optType(0).Checked = True Then
            '    For CntRow = 1 To .MaxRows
            '        .Row = CntRow

            '        .Col = ColItemCode
            '        mItemCode = Trim(.Text)

            '        .Col = ColItemCode
            '        mItemCode = Trim(.Text)

            '        .Col = ColItemCode
            '        mItemCode = Trim(.Text)

            '        'Private Const ColSubCategory As Short = 4
            '        'Private Const ColItemCode As Short = 5
            '        'Private Const ColItemName As Short = 6
            '        'Private Const ColUnit As Short = 7
            '        'Private Const ColOpening As Short = 8
            '        'Private Const ColReceipt As Short = 9
            '        'Private Const ColIssue As Short = 10
            '        'Private Const ColClosing As Short = 11
            '        'Private Const ColRejection As Short = 12
            '        'Private Const ColUnderQC As Short = 13
            '        'Private Const ColDeptStock As Short = 14
            '        'Private Const ColTotalClosing As Short = 15
            '        'Private Const ColRate As Short = 16
            '        'Private Const ColValue As Short = 17

            '        'mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
            '        'mItemUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
            '        'mClosing = IIf(IsDBNull(RsTemp.Fields("CLOSING").Value), 0, RsTemp.Fields("CLOSING").Value)
            '        'mCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), -1, RsTemp.Fields("COMPANY_CODE").Value)
            '        'mProdType = GetProductionType(mItemCode)

            '        'If mProdType = "I" Or mProdType = "P" Then
            '        '    mClosing = 1
            '        '    mTotValue = GetLatestFIFORate(mCompanyCode, mItemCode, mItemUOM, mClosing, (txtDateTo.Text), "L")
            '        'Else
            '        '    If chkRate.CheckState = System.Windows.Forms.CheckState.Checked Then
            '        '        mTotValue = GetLatestFIFORate(mCompanyCode, mItemCode, mItemUOM, mClosing, (txtDateTo.Text), "L")
            '        '    Else
            '        '        mTotValue = 0
            '        '    End If
            '        'End If

            '        'If mClosing > 0 Then
            '        '    mRate = VB6.Format(mTotValue / mClosing, "0.00")
            '        'Else
            '        '    mRate = CDbl("0.00")
            '        'End If

            '    Next
            'Else
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColCatgeoryDesc
                mCatDesc = Trim(.Text)

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If mCatDesc <> "" Then
                    .Col = ColOpeningValue
                    mOpeningValue = VB6.Format(.Text, "0.000")
                    mTotalOpeningValue = mTotalOpeningValue + mOpeningValue

                    .Col = ColMRRValue
                    mMRRValue = VB6.Format(.Text, "0.000")
                    mTotalMRRValue = mTotalMRRValue + mMRRValue

                    .Col = ColValue
                    mTotValue = VB6.Format(.Text, "0.000")
                    mTotalValue = mTotalValue + mTotValue

                    .Col = ColConsumptionValue
                    mTotConsumptionValue = VB6.Format(.Text, "0.000")
                    mTotalConsumptionValue = mTotalConsumptionValue + mTotConsumptionValue

                    .Col = ColRGPValue
                    mTotRGPValue = VB6.Format(.Text, "0.000")
                    mTotalRGPValue = mTotalRGPValue + mTotRGPValue

                    .Col = ColAssetsValue
                    mTotAssetsValue = VB6.Format(.Text, "0.000")
                    mTotalAssetsValue = mTotalAssetsValue + mTotAssetsValue

                    .Col = ColLedgerAmount
                    mLedgerAmount = VB6.Format(.Text, "0.000")
                    mTotLedgerAmount = mTotLedgerAmount + mLedgerAmount

                    .Col = ColTotalClosingNos
                    mClosingNos = VB6.Format(.Text, "0.000")
                    mTotalClosingNos = mTotalClosingNos + mClosingNos

                    .Col = ColReceiptValue
                    mReceiptValue = VB6.Format(.Text, "0.000")
                    mTotReceiptValue = mTotReceiptValue + mReceiptValue

                    .Col = ColIssueValue
                    mIssueValue = VB6.Format(.Text, "0.000")
                    mTotIssueValue = mTotIssueValue + mIssueValue

                End If
            Next
            'End If
            Call MainClass.AddBlankfpSprdRow(SprdMain, ColCatgeory)
            .Row = .MaxRows
            .Col = ColCatgeoryDesc
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)
            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False
            .Row = .MaxRows

            .Col = ColOpeningValue
            .Text = VB6.Format(mTotalOpeningValue, "0.00")

            .Col = ColMRRValue
            .Text = VB6.Format(mTotalMRRValue, "0.00")

            .Col = ColReceiptValue
            .Text = VB6.Format(mTotReceiptValue, "0.00")

            .Col = ColIssueValue
            .Text = VB6.Format(mTotIssueValue, "0.00")


            .Col = ColValue
            .Text = VB6.Format(mTotalValue, "0.00")

            .Col = ColConsumptionValue
            .Text = VB6.Format(mTotalConsumptionValue, "0.00")

            .Col = ColRGPValue
            .Text = VB6.Format(mTotalRGPValue, "0.00")

            .Col = ColAssetsValue
            .Text = VB6.Format(mTotalAssetsValue, "0.00")

            .Col = ColLedgerAmount
            .Text = VB6.Format(mTotLedgerAmount, "0.00")

            .Col = ColTotalClosingNos
            .Text = VB6.Format(mTotalClosingNos, "0.00")

            FormatSprdMain(-1)

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtLocation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        'Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mClosing As String
        Dim CntLst As Long
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColItemCode
        mItemCode = Trim(SprdMain.Text)

        SprdMain.Col = ColUnit
        mItemUOM = Trim(SprdMain.Text)

        SprdMain.Col = ColItemName
        mItemDesc = Trim(SprdMain.Text)

        SprdMain.Col = ColTotalClosing
        mClosing = Trim(SprdMain.Text)

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
        End If

        If CDbl(mClosing) > 0 Then
            frmParamStockDetail.lblItemCode.Text = mItemCode
            frmParamStockDetail.txtItemName.Text = mItemCode & " - " & mItemDesc
            frmParamStockDetail.lblItemUOM.Text = mItemUOM
            frmParamStockDetail.lblCompanyCode.Text = mCompanyCodeStr

            frmParamStockDetail.txtAsOn.Text = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
            frmParamStockDetail.txtClosing.Text = CStr(Val(mClosing))

            frmParamStockDetail.ShowDialog()
            frmParamStockDetail.frmParamStockDetail_Activated(Nothing, New System.EventArgs())
        End If
    End Sub

    Private Sub cboExportItem_RowSelected(sender As Object, e As RowSelectedEventArgs) Handles cboExportItem.RowSelected
        PrintStatus(False)
    End Sub
    Private Sub cboSubCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSubCategory.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboSubCategory_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSubCategory.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
End Class
