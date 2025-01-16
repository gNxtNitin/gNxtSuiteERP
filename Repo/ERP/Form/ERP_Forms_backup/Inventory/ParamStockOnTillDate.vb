Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmParamStockTillMonth
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean
    Dim XRIGHT As String

    Dim GroupOnItem As Boolean

    Private Const RowHeight As Short = 12

    Private Const ColUserID As Short = 1
    Private Const ColCatgeory As Short = 2
    Private Const ColSubCategory As Short = 3
    Private Const ColItemCode As Short = 4
    Private Const ColItemName As Short = 5
    Private Const ColUnit As Short = 6
    Private Const ColMin As Short = 7
    Private Const ColMax As Short = 8
    Private Const ColOpening As Short = 9
    Private Const ColReceipt As Short = 10
    Private Const ColTillReceipt As Short = 11
    Private Const ColIssue As Short = 12
    Private Const ColTillIssue As Short = 13
    Private Const ColClosing As Short = 14
    Private Const ColRejection As Short = 15
    Private Const ColUnderQC As Short = 16
    Private Const ColTotalClosing As Short = 17
    Private Const ColRate As Short = 18
    Private Const ColValue As Short = 19
    Dim xMyMenu As String

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean
    Private Sub cboCond_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub cboCond_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.SelectedIndexChanged
        PrintStatus(False)
    End Sub



    Private Sub cboExportItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExportItem.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub cboExportItem_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExportItem.SelectedIndexChanged
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

    Private Sub chkShowMinMax_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShowMinMax.CheckStateChanged
        PrintStatus(False)
    End Sub

    Private Sub chkSubCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSubCategory.CheckStateChanged
        txtSubCatName.Enabled = IIf(chkSubCategory.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearchSubCat.Enabled = IIf(chkSubCategory.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub chkViewAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkViewAll.CheckStateChanged
        PrintStatus(False)
    End Sub

    Private Sub chkZeroBal_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkZeroBal.CheckStateChanged
        PrintStatus(False)
    End Sub

    Private Sub cmdDivision_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDivision.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtDivision.Text), "INV_DIVISION_MST", "DIV_DESC", "DIV_CODE", , , SqlStr) = True Then
            txtDivision.Text = AcName
            txtDivision_Validating(txtDivision, New System.ComponentModel.CancelEventArgs(False))
            txtDivision.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

            .Col = ColMin
            .Text = "Min Qty"

            .Col = ColMax
            .Text = "Max Qty"

            .Col = ColOpening
            .Text = "Month Opening"

            .Col = ColReceipt
            .Text = "Receipt"

            .Col = ColTillReceipt
            .Text = "Till Date Receipt"

            .Col = ColIssue
            .Text = "Issue"

            .Col = ColTillIssue
            .Text = "Till Date Issue"

            .Col = ColClosing
            .Text = "Closing"

            .Col = ColRejection
            .Text = "Rejection"

            .Col = ColUnderQC
            .Text = "Under QC"

            .Col = ColTotalClosing
            .Text = "Total Closing"

            .Col = ColRate
            .Text = "Rate"

            .Col = ColValue
            .Text = "Value"

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
            .set_RowHeight(0, 2 * RowHeight)
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

            For I = ColMin To ColValue
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                If I = ColValue Then
                    .set_ColWidth(I, 10)
                    .TypeFloatDecimalPlaces = 2
                Else
                    .TypeFloatDecimalPlaces = 3
                    .set_ColWidth(I, 9)
                End If
            Next

            .Col = ColMin
            .ColHidden = IIf(chkShowMinMax.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

            .Col = ColMax
            .ColHidden = IIf(chkShowMinMax.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

            .ColsFrozen = ColUnit

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' OperationModeSingle

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

        If InsertIntoTempTable = False Then GoTo ERR1

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mRPTName = IIf(chkShowMinMax.CheckState = System.Windows.Forms.CheckState.Checked, "StockOnHandMinMax.rpt", "StockOnHandMonthly.rpt")
        mTitle = "Inventory on Hand - as on " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        '    If chkCategory.Value = vbUnchecked Then
        mSubTitle = "" ''"(Category : " & txtCatName.Text & ")"
        '    End If

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
    Private Function InsertIntoTempTable() As Boolean

        On Error GoTo PrintDummyErr

        Dim SqlStr As String = ""
        Dim mSqlStr As String
        'Dim PvtDBCn As ADODB.Connection

        Dim mCategoryCode As String = ""
        Dim mSubCategoryCode As String
        Dim mItemCode As String
        Dim mItemName As String
        Dim mItemUOM As String = ""
        Dim mOpening As Double
        Dim mReceipt As Double
        Dim mIssue As Double
        Dim mClosing As Double
        Dim mRej As Double
        Dim mQC As Double
        Dim mTotClosing As Double
        Dim mRate As Double
        Dim mValue As Double

        Dim mMin As Double
        Dim mMax As Double
        Dim mTillReceipt As Double
        Dim mTillIssue As Double
        Dim CntRow As Integer

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_STOCKONHAND NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        '    mSqlStr = MakeSQL
        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColCatgeory
                mCategoryCode = Trim(.Text)

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

                .Col = ColMin
                mMin = Val(.Text)

                .Col = ColMax
                mMax = Val(.Text)

                .Col = ColTillReceipt
                mTillReceipt = Val(.Text)

                .Col = ColTillIssue
                mTillIssue = Val(.Text)


                SqlStr = " INSERT INTO TEMP_STOCKONHAND (USERID, CATEGORY_CODE, " & vbCrLf & " SUBCATEGORY_CODE, ITEM_CODE, ITEM_NAME, ITEM_UOM, " & vbCrLf & " MINIMUM_QTY, MAXIMUM_QTY, " & vbCrLf & " OPENING, RECEIPT, TILLRECEIPT, ISSUE, TILLISSUE, CLOSING, " & vbCrLf & " REJECTION, UNDERQC, TOT_CLOSING, RATE, " & vbCrLf & " VALUE)  VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', '" & MainClass.AllowSingleQuote(mCategoryCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mSubCategoryCode) & "', '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemName) & "', '" & MainClass.AllowSingleQuote(mItemUOM) & "', " & vbCrLf & " " & Val(CStr(mMin)) & ", " & Val(CStr(mMax)) & "," & vbCrLf & " " & Val(CStr(mOpening)) & ", " & Val(CStr(mReceipt)) & ", " & Val(CStr(mTillReceipt)) & ", " & Val(CStr(mIssue)) & "," & vbCrLf & " " & Val(CStr(mTillIssue)) & "," & Val(CStr(mClosing)) & "," & vbCrLf & " " & Val(CStr(mRej)) & ", " & Val(CStr(mQC)) & ", " & Val(CStr(mTotClosing)) & ", " & Val(CStr(mRate)) & "," & vbCrLf & " " & Val(CStr(mValue)) & ")"

                PubDBCn.Execute(SqlStr)

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


        mSqlStr = " SELECT STOCK.*, " & vbCrLf & " CATMST.GEN_DESC, SUBCATMST.SUBCATEGORY_DESC " & vbCrLf & " FROM TEMP_STOCKONHAND STOCK, INV_GENERAL_MST CATMST, " & vbCrLf & " INV_SUBCATEGORY_MST SUBCATMST " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND CATMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND STOCK.CATEGORY_CODE=CATMST.GEN_CODE" & vbCrLf & " AND SUBCATMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND STOCK.SUBCATEGORY_CODE=SUBCATMST.SUBCATEGORY_CODE" & vbCrLf & " AND CATMST.COMPANY_CODE=SUBCATMST.COMPANY_CODE" & vbCrLf & " AND CATMST.GEN_CODE=SUBCATMST.CATEGORY_CODE" & vbCrLf & " AND CATMST.GEN_TYPE='C' " & vbCrLf & " ORDER BY STOCK.CATEGORY_CODE, STOCK.SUBCATEGORY_CODE, STOCK.ITEM_CODE "

        FetchRecordForReport = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)

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

        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

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
        Dim mMonthStartDate As String
        Dim mTableName As String
        Dim mDivisionCode As Double

        mMonthStartDate = "01/" & VB6.Format(txtDateTo.Text, "MM/YYYY")

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, " & vbCrLf & " TO_CHAR(MINIMUM_QTY) AS MINQTY, TO_CHAR(MAXIMUM_QTY) AS MAXQTY, "

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS Receipt, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(E_DATE,'MON-YYYY')='" & UCase(VB6.Format(txtDateTo.Text, "MMM-YYYY")) & "' AND STOCK_TYPE IN ('ST','CS') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS TillReceipt, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS Issue, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(E_DATE,'MON-YYYY')='" & UCase(VB6.Format(txtDateTo.Text, "MMM-YYYY")) & "' AND STOCK_TYPE IN ('ST','CS') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS TillIssue, " & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * DECODE(STOCK_TYPE,'ST',1,DECODE(STOCK_TYPE,'CS',1,0))* CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END)) as Closing, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE='RJ' " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Rejection, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND (STOCK_TYPE='QC'  OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS UnderQC, " & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as TotClosing, " & vbCrLf & " TO_CHAR(MAX(NVL(INV.LANDED_COST,0))) as Rate, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>=0 THEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))*AVG(NVL(INV.LANDED_COST,0)) ELSE 0 END) as Value "

        mTableName = ConInventoryTable

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " INV, " & vbCrLf & " INV_ITEM_MST ITEM, INV_GENERAL_MST CAT "


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf & " INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'"

        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND INV.ITEM_CODE=ITEM.ITEM_CODE " & vbCrLf & " AND ITEM.ITEM_STATUS='A'"

        SqlStr = SqlStr & vbCrLf & " AND ITEM.COMPANY_CODE=CAT.COMPANY_CODE AND ITEM.CATEGORY_CODE=CAT.GEN_CODE AND CAT.GEN_TYPE='C' "

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            '        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND GEN_TYPE='C'") = True Then
            '            mCategoryCode = MasterNo
            '            SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE='" & mCategoryCode & "'"
            '        End If
            SqlStr = SqlStr & vbCrLf & " AND CAT.GEN_DESC='" & MainClass.AllowSingleQuote(txtCatName.Text) & "'"
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

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = Val(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND INV.DIV_CODE=" & mDivisionCode & ""
            End If
        End If

        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
            End If
        End If

        If chkCS.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('CS','ST','RJ','QC') "
        Else
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('ST','RJ','QC') "
        End If

        ''AND DEPT_CODE_TO='STR'
        If cboExportItem.SelectedIndex >= 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.IS_EXPORT_ITEM = '" & VB.Left(cboExportItem.Text, 1) & "'"
        End If

        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"
        End If

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        If chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        End If

        If chkZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"
        End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM,MINIMUM_QTY,MAXIMUM_QTY "

        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE "


        MakeSQL = SqlStr
        Exit Function
InsertErr:
        MakeSQL = ""
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmParamStockTillMonth_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim mIsAuthorisedUser As String
        Dim SqlStr As String = ""

        If FormLoaded = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


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

    Private Sub frmParamStockTillMonth_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)
        MainClass.SetControlsColor(Me)

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

        cboExportItem.Items.Clear()
        cboExportItem.Items.Add("All")
        cboExportItem.Items.Add("Yes")
        cboExportItem.Items.Add("No")
        cboExportItem.SelectedIndex = 0

        FormatSprdMain(-1)
        FillSprdMain()
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub frmParamStockTillMonth_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        '    SprdOption.Width = IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth)
        '    Frame4.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamStockTillMonth_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub PrintStatus(ByRef PrintFlag As Boolean)
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
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

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColUnit
                mItemUOM = Trim(.Text)

                .Col = ColTotalClosing
                mClosing = Val(.Text)

                SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR,PURCHASE_COST FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mIssueUOM = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                    mPurchaseUOM = IIf(IsDbNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                    mFactor = IIf(IsDbNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)
                    mItemCost = IIf(IsDbNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value)
                End If

                '            If pPurchaseCost = 0 Or pLandedCost = 0 Then
                '                If GetLatestItemCostFromPO(pItemCode, xPurchaseCost, xLandedCost, pRefDate, xStockType, pPartyCode, pItemUOM, mFactor) = False Then GoTo UpdateStockTRNErr
                '                pPurchaseCost = IIf(xPurchaseCost = 0, mItemCost, xPurchaseCost)
                '                pLandedCost = IIf(xLandedCost = 0, mItemCost, xLandedCost)
                '            End If
                '
                '            If pItemUOM = mPurchaseUOM Then
                '                pItemUOM = mIssueUOM
                '                pItemQty = pItemQty * mFactor
                '                pRejQty = pRejQty * mFactor
                '                pPurchaseCost = pPurchaseCost / mFactor
                '                pLandedCost = pLandedCost / mFactor
                '                mItemCost = mItemCost / mFactor
                '            End If

                '            If GetLatestItemCostFromPO(mItemCode, mPurchaseRate, mLandedCost, txtDateTo.Text, "ST", "", mItemUOM, mFactor) = False Then GoTo ErrPart
                '
                '            mRate = mPurchaseRate

                mTotValue = GetLatestItemCostFromMRR(mItemCode, mItemUOM, mClosing, (txtDateTo.Text), "L")

                .Row = CntRow
                .Col = ColValue
                .Text = CStr(mTotValue)

                .Col = ColRate
                If mClosing > 0 Then
                    mRate = mTotValue / mClosing
                Else
                    mRate = CDbl("0.00")
                End If

                .Col = ColRate
                .Text = VB6.Format(mRate, "0.000")

                .Col = ColValue
                .Text = VB6.Format(mTotValue, "0.000")

            Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtDivision_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.DoubleClick
        Call cmdDivision_Click(cmdDivision, New System.EventArgs())
    End Sub

    Private Sub txtDivision_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDivision.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDivision.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDivision_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDivision.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdDivision_Click(cmdDivision, New System.EventArgs())
    End Sub

    Private Sub txtDivision_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDivision.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDivision.Text) = "" Then
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable((txtDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            ErrorMsg("Invalid Division Name.", , MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub chkDivision_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDivision.CheckStateChanged
        txtDivision.Enabled = IIf(chkDivision.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdDivision.Enabled = IIf(chkDivision.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
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
