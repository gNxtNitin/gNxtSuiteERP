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
Friend Class frmParamStockSummary
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
    Private Const ColMRRIUQty As Short = 11
    Private Const ColIssue As Short = 12
    Private Const ColNetPurchase As Short = 13
    Private Const ColConsumptionQty As Short = 14
    Private Const ColScrap As Short = 15
    Private Const ColSaleIUQty As Short = 16
    Private Const ColTotalClosing As Short = 17

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

            .Col = ColOpening
            .Text = "Opening"

            .Col = ColMRRQty
            .Text = "MRR Qty"

            .Col = ColMRRIUQty
            .Text = "MRR Inter Unit Qty"

            .Col = ColIssue
            .Text = "Sales Return"

            .Col = ColNetPurchase
            .Text = "Net Purchase"

            .Col = ColConsumptionQty
            .Text = "Consumption Qty"

            .Col = ColScrap
            .Text = "Scrap Qty"

            .Col = ColSaleIUQty
            .Text = "Sale Inter Unit Qty"

            .Col = ColTotalClosing
            .Text = "Total Closing Qty"


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

            .Row = 0
            .Col = ColItemCode
            .Text = IIf(optType(1).Checked = True, "Item Code", "Month")

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
            .ColHidden = False ''IIf(optType(1).Checked = True, True, False)

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


            For I = ColOpening To ColTotalClosing
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(I, 9)

            Next


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

        '        PubDBCn.Errors.Clear()
        '        PubDBCn.BeginTrans()

        '        SqlStr = "DELETE FROM TEMP_STOCKONHAND NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        '        PubDBCn.Execute(SqlStr)

        '        '    If lblbookType.text = "N" Then
        '        '        mSqlStr = MakeSQL2
        '        '    Else
        '        '        mSqlStr = MakeSQL
        '        '    End If

        '        With SprdMain
        '            For CntRow = 1 To .MaxRows
        '                .Row = CntRow
        '                .Col = ColCatgeory
        '                mCategoryCode = Trim(.Text)

        '                .Col = ColCatgeoryDesc
        '                mCategoryDesc = Trim(.Text)

        '                .Col = ColSubCategory
        '                mSubCategoryCode = Trim(.Text)

        '                .Col = ColItemCode
        '                mItemCode = Trim(.Text)

        '                .Col = ColItemName
        '                mItemName = Trim(.Text)

        '                .Col = ColUnit
        '                mItemUOM = Trim(.Text)

        '                .Col = ColOpening
        '                mOpening = Val(.Text)

        '                .Col = ColOpeningValue
        '                mOpeningValue = Val(.Text)

        '                .Col = ColMRRQty
        '                mMRRQty = Val(.Text)

        '                .Col = ColMRRValue
        '                mMRRValue = Val(.Text)

        '                .Col = ColReceipt
        '                mReceipt = Val(.Text)

        '                .Col = ColIssue
        '                mIssue = Val(.Text)

        '                .Col = ColClosing
        '                mClosing = Val(.Text)

        '                .Col = ColRejection
        '                mRej = Val(.Text)

        '                .Col = ColUnderQC
        '                mQC = Val(.Text)

        '                .Col = ColTotalClosing
        '                mTotClosing = Val(.Text)

        '                .Col = ColRate
        '                mRate = Val(.Text)

        '                .Col = ColValue
        '                mValue = Val(.Text)

        '                .Col = ColTransDate
        '                mTransDate = Trim(.Text)

        '                .Col = ColMaxQty
        '                mMaxQty = Val(.Text)

        '                .Col = ColMinQty
        '                mMinQty = Val(.Text)

        '                '.Col = ColConsumptionValue
        '                'mConsumptionValue = Trim(.Text)

        '                SqlStr = " INSERT INTO TEMP_STOCKONHAND (USERID, CATEGORY_CODE, " & vbCrLf _
        '                    & " SUBCATEGORY_CODE, ITEM_CODE, ITEM_NAME, ITEM_UOM, " & vbCrLf _
        '                    & " OPENING, RECEIPT, ISSUE, CLOSING, " & vbCrLf _
        '                    & " REJECTION, UNDERQC, DEPT_STOCK, TOT_CLOSING, RATE," & vbCrLf _
        '                    & " VALUE, LAST_TRANS_DATE, OPENING_VALUE, MRR_QTY, MRR_VALUE,MINIMUM_QTY, MAXIMUM_QTY) VALUES ( " & vbCrLf _
        '                    & " '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', '" & MainClass.AllowSingleQuote(mCategoryCode) & "', " & vbCrLf _
        '                    & " '" & MainClass.AllowSingleQuote(mSubCategoryCode) & "', '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
        '                    & " '" & MainClass.AllowSingleQuote(mItemName) & "', '" & MainClass.AllowSingleQuote(mItemUOM) & "', " & vbCrLf _
        '                    & " " & Val(CStr(mOpening)) & ", " & Val(CStr(mReceipt)) & "," & Val(CStr(mIssue)) & "," & Val(CStr(mClosing)) & "," & vbCrLf _
        '                    & " " & Val(CStr(mRej)) & ", " & Val(CStr(mQC)) & ", " & Val(CStr(mDeptStock)) & "," & Val(CStr(mTotClosing)) & ", " & Val(CStr(mRate)) & "," & vbCrLf _
        '                    & " " & Val(CStr(mValue)) & ",'" & mTransDate & "', " & Val(mOpeningValue) & ", " & Val(mMRRQty) & ", " & Val(mMRRValue) & ", " & Val(mMinQty) & ", " & Val(mMaxQty) & ")"

        '                PubDBCn.Execute(SqlStr)

        '            Next
        '        End With
        'NextRec:

        '        PubDBCn.CommitTrans()


        '        'If PvtDBCn.State = adStateOpen Then
        '        'PvtDBCn.Close
        '        'Set PvtDBCn = Nothing
        '        '    End If

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

        If optType(0).Checked = True Then
            SqlStr = MakeSQL()
        Else
            SqlStr = MakeSQLSumm()
        End If



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
        Dim mStockType As String=""


        mHavingClause = False
        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        mTableName = ConInventoryTable


        mDeptFunction = "GETDEPTSTOCK"

        'Dim mWareHouse As String

        'If cboExportItem.Text.Trim <> "" Then
        '    For Each r As UltraGridRow In cboExportItem.CheckedRows
        '        If mWareHouse <> "" Then
        '            mWareHouse += "," & "'" & r.Cells("STOCK_ID").Value.ToString() & "'"
        '        Else
        '            mWareHouse += "'" & r.Cells("STOCK_ID").Value.ToString() & "'"
        '        End If
        '    Next
        'End If


        If optType(0).Checked = True Then
            SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "',  " & vbCrLf _
                       & " ITEM.CATEGORY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_CODE, SC.SUBCATEGORY_DESC, ITEM.ITEM_CODE, " & vbCrLf _
                       & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, "
        Else
            SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "',  " & vbCrLf _
                       & " ITEM.CATEGORY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_CODE, SC.SUBCATEGORY_DESC, TO_CHAR(REF_DATE,'MON-YYYY'), " & vbCrLf _
                       & " '', '', "
        End If


        'If optType(0).Checked = True Then
        SqlStr = SqlStr & vbCrLf _
                   & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE NOT IN ('SC')" & vbCrLf _
                   & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening, "
        'Else
        '    SqlStr = SqlStr & vbCrLf _
        '            & " TO_CHAR(SUM(CASE WHEN E_DATE < TO_CHAR(REF_DATE,'YYYYMM') AND STOCK_TYPE NOT IN ('SC')" & vbCrLf _
        '            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening, "
        'End If


        SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('MRR') AND REF_FLAG='P' AND NVL(ACM.INTER_UNIT,'N')='N' AND STOCK_TYPE NOT IN ('SC')" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRQty, "

        SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('MRR') AND REF_FLAG='P' AND NVL(ACM.INTER_UNIT,'N')='Y'" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRIUQty, "

        SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('DSP','RGP','NRG')" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) ELSE 0 END)) AS Issue, "

        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE NOT IN ('SC')" & vbCrLf _
            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) + "

        SqlStr = SqlStr & vbCrLf _
                    & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('MRR') AND REF_FLAG='P' AND STOCK_TYPE NOT IN ('SC')" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) -  "

        SqlStr = SqlStr & vbCrLf _
                    & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('DSP','RGP','NRG')" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) ELSE 0 END)) AS NetPurchase, "


        SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('PMD')" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) ELSE 0 END)) AS Consumption, "


        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE='CON'" & vbCrLf _
            & " THEN ITEM_QTY * DECODE(ITEM_IO,'O',1,0) ELSE 0 END)) AS Scrap, "

        'SqlStr = SqlStr & vbCrLf _
        '            & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('MRR') AND REF_FLAG='P' AND NVL(ACM.INTER_UNIT,'N')='N' AND STOCK_TYPE NOT IN ('SC')" & vbCrLf _
        '            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRQty, "
        '''CON

        SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('DSP') AND REF_FLAG='P' AND NVL(ACM.INTER_UNIT,'N')='Y'" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) ELSE 0 END)) AS SALEIUQty, "

        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as Closing " & vbCrLf

        SqlStr = SqlStr & vbCrLf _
            & " FROM " & mTableName & " INV, " & vbCrLf _
            & " INV_ITEM_MST ITEM, FIN_SUPP_CUST_MST ACM, INV_GENERAL_MST GMST, INV_SUBCATEGORY_MST SC "

        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf _
            & " WHERE INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID IN  ('WH','PH')"


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

        If optType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf _
                & "  INV.COMPANY_CODE, ITEM.CATEGORY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_CODE,SC.SUBCATEGORY_DESC, ITEM.ITEM_CODE, " & vbCrLf _
                & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM "
        Else
            SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf _
                & "  INV.COMPANY_CODE, ITEM.CATEGORY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_CODE, SC.SUBCATEGORY_DESC, TO_CHAR(REF_DATE,'MON-YYYY'),TO_CHAR(REF_DATE,'YYYYMM')"
        End If


        If optType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf _
                    & "  INV.COMPANY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_DESC, ITEM.ITEM_CODE "
        Else
            SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf _
                    & "  INV.COMPANY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_DESC, TO_CHAR(REF_DATE,'YYYYMM') "
        End If

        MakeSQL = SqlStr
        Exit Function
InsertErr:
        MakeSQL = ""
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function MakeSQLSumm() As String

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""
        Dim mSubCategoryCode As String
        Dim mCond As String
        Dim mItemCode As String
        Dim mHavingClause As Boolean
        Dim mTableName As String
        'Dim mToDate As String
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

        Dim CntMonth As Long


        mHavingClause = False
        'mToDate = VB6.Format(pToDate, "DD-MMM-YYYY")

        mTableName = ConInventoryTable


        mDeptFunction = "GETDEPTSTOCK"

        Dim pDate As String
        Dim pToDate As String
        Dim mCurrentFY As Long


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


        mCurrentFY = RsCompany.Fields("FYEAR").Value

        SqlStr = ""

        SqlStr = " SELECT USER_ID,  " & vbCrLf _
                       & " CATEGORY_CODE, GEN_DESC, SUBCATEGORY_CODE, SUBCATEGORY_DESC, REF_DATE, " & vbCrLf _
                       & " ITEM_DESC,  ITEM_UOM , " & vbCrLf _
                       & " Opening, MRRQty, MRRIUQty, Issue, NetPurchase, Consumption, Scrap, SALEIUQty, Closing FROM ("


        Dim mStartFrom As Date
        Dim mEndTo As Date

        mStartFrom = CDate("01" & "/" & VB6.Format(txtDateFrom.Text, "MM/YYYY"))
        mEndTo = CDate(VB6.Format(MainClass.LastDay(Month(txtDateTo.Text), Year(txtDateTo.Text)) & "/" & VB6.Format(txtDateTo.Text, "MM/YYYY"))) '' CDate(txtDateTo.Text)

        'For mCntMonth = mStartFrom To mEndTo Step 30
        CntMonth = 1
        Do While mStartFrom <= mEndTo
            pDate = VB6.Format("01/" & VB6.Format(mStartFrom, "MM/YYYY"))
            pToDate = VB6.Format(MainClass.LastDay(Month(mStartFrom), Year(mStartFrom)) & "/" & VB6.Format(mStartFrom, "MM/YYYY"))

            If CntMonth > 1 Then
                SqlStr = SqlStr & vbCrLf & " UNION ALL "
            End If
            CntMonth = CntMonth + 1
            SqlStr = SqlStr & vbCrLf _
                       & " Select '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' USER_ID,  " & vbCrLf _
                       & " ITEM.CATEGORY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_CODE, SC.SUBCATEGORY_DESC, '" & VB6.Format(pDate, "MMM-YYYY") & "' AS REF_DATE, " & vbCrLf _
                       & " '' AS ITEM_DESC, '" & VB6.Format(pDate, "YYYYMM") & "' AS ITEM_UOM, "

            SqlStr = SqlStr & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN E_DATE < TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE NOT IN ('SC')" & vbCrLf _
                & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening, "


            SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('MRR') AND REF_FLAG='P' AND NVL(ACM.INTER_UNIT,'N')='N' AND STOCK_TYPE NOT IN ('SC')" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRQty, "

            SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('MRR') AND REF_FLAG='P' AND NVL(ACM.INTER_UNIT,'N')='Y'" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS MRRIUQty, "

            SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('DSP','RGP','NRG')" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) ELSE 0 END)) AS Issue, "

            SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE NOT IN ('SC')" & vbCrLf _
            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) + "

            '
            SqlStr = SqlStr & vbCrLf _
                    & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('MRR') AND REF_FLAG='P' AND STOCK_TYPE NOT IN ('SC')" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) -  "

            SqlStr = SqlStr & vbCrLf _
                    & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('DSP','RGP','NRG')" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) ELSE 0 END)) AS NetPurchase, "


            SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('PMD')" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) ELSE 0 END)) AS Consumption, "


            SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE='CON'" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'O',1,0) ELSE 0 END)) AS Scrap, "


            SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND E_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE IN ('DSP') AND REF_FLAG='P' AND NVL(ACM.INTER_UNIT,'N')='Y'" & vbCrLf _
                    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',-1,1) ELSE 0 END)) AS SALEIUQty, "

            SqlStr = SqlStr & vbCrLf _
                    & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as Closing "

            SqlStr = SqlStr & vbCrLf _
                    & " FROM " & mTableName & " INV, " & vbCrLf _
                    & " INV_ITEM_MST ITEM, FIN_SUPP_CUST_MST ACM, INV_GENERAL_MST GMST, INV_SUBCATEGORY_MST SC "

            ''**********WHERE CLAUSE .......*************

            SqlStr = SqlStr & vbCrLf _
                    & " WHERE INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID IN  ('WH','PH')"


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

            If cboDivision.Text <> "ALL" Then
                If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivision = MasterNo
                    SqlStr = SqlStr & vbCrLf & "AND INV.DIV_CODE=" & mDivision & ""
                End If
            End If

            If cboSubCategory.Text <> "ALL" Then
                SqlStr = SqlStr & vbCrLf & "AND SC.SUBCATEGORY_DESC='" & MainClass.AllowSingleQuote(cboSubCategory.Text) & "'"
            End If

            If mRMCatCodeStr <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN " & "(" & mRMCatCodeStr & ")" & ""
            End If


            If mCompanyCodeStr <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE IN " & "(" & mCompanyCodeStr & ")" & ""
            End If


            If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_SHORT_DESC='" & txtItemName.Text & "'"
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

            If IsDate(pToDate) Then
                SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
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
                & "  ITEM.CATEGORY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_CODE, SC.SUBCATEGORY_DESC"

            mStartFrom = mStartFrom.AddMonths(1)
        Loop

        SqlStr = SqlStr & vbCrLf & ") ORDER BY " & vbCrLf _
                    & "  GEN_DESC, SUBCATEGORY_DESC, ITEM_UOM "

        MakeSQLSumm = SqlStr
        Exit Function
InsertErr:
        MakeSQLSumm = ""
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Sub frmParamStockSummary_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

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

    Private Sub frmParamStockSummary_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
    Private Sub frmParamStockSummary_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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


    Private Sub frmParamStockSummary_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
                    .Col = ColOpening
                    mOpeningValue = VB6.Format(.Text, "0.000")
                    mTotalOpeningValue = mTotalOpeningValue + mOpeningValue

                    .Col = ColMRRQty
                    mMRRValue = VB6.Format(.Text, "0.000")
                    mTotalMRRValue = mTotalMRRValue + mMRRValue

                    .Col = ColMRRIUQty
                    mTotValue = VB6.Format(.Text, "0.000")
                    mTotalValue = mTotalValue + mTotValue

                    .Col = ColIssue
                    mTotConsumptionValue = VB6.Format(.Text, "0.000")
                    mTotalConsumptionValue = mTotalConsumptionValue + mTotConsumptionValue

                    .Col = ColNetPurchase
                    mTotRGPValue = VB6.Format(.Text, "0.000")
                    mTotalRGPValue = mTotalRGPValue + mTotRGPValue

                    .Col = ColConsumptionQty
                    mTotAssetsValue = VB6.Format(.Text, "0.000")
                    mTotalAssetsValue = mTotalAssetsValue + mTotAssetsValue

                    .Col = ColScrap
                    mLedgerAmount = VB6.Format(.Text, "0.000")
                    mTotLedgerAmount = mTotLedgerAmount + mLedgerAmount

                    .Col = ColSaleIUQty
                    mClosingNos = VB6.Format(.Text, "0.000")
                    mTotalClosingNos = mTotalClosingNos + mClosingNos

                    .Col = ColTotalClosing
                    mReceiptValue = VB6.Format(.Text, "0.000")
                    mTotReceiptValue = mTotReceiptValue + mReceiptValue


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

            '.Col = ColOpening
            '.Text = VB6.Format(mTotalOpeningValue, "0.00")

            .Col = ColMRRQty
            .Text = VB6.Format(mTotalMRRValue, "0.00")

            .Col = ColMRRIUQty
            .Text = VB6.Format(mTotalValue, "0.00")

            .Col = ColIssue
            .Text = VB6.Format(mTotalConsumptionValue, "0.00")

            .Col = ColNetPurchase
            .Text = VB6.Format(mTotalRGPValue, "0.00")

            .Col = ColConsumptionQty
            .Text = VB6.Format(mTotalAssetsValue, "0.00")

            .Col = ColScrap
            .Text = VB6.Format(mTotLedgerAmount, "0.00")

            .Col = ColSaleIUQty
            .Text = VB6.Format(mTotalClosingNos, "0.00")

            '.Col = ColTotalClosing
            '.Text = VB6.Format(mTotReceiptValue, "0.00")

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

    Private Sub Frame4_Enter(sender As Object, e As EventArgs) Handles Frame4.Enter

    End Sub
End Class
