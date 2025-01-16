Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamStockSTRReport
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
    Private Const ColFYOpening As Short = 7
    Private Const ColOpPhyVariance As Short = 8
    Private Const ColOpening As Short = 9
    Private Const ColReceipt1 As Short = 10
    Private Const ColIssue1 As Short = 11
    Private Const ColAdj As Short = 12
    'Private Const ColClosing = 13
    Private Const ColRejectionOP As Short = 13
    Private Const ColRejectionRecd As Short = 14
    Private Const ColRejectionSend As Short = 15
    Private Const ColRejection As Short = 16
    Private Const ColDeptStock As Short = 17
    Private Const ColTotalClosing As Short = 18
    Private Const ColRate As Short = 19
    Private Const ColClosingValue As Short = 20
    Private Const ColPhyBal As Short = 21
    Private Const ColPhyDiff As Short = 22
    Private Const ColDespatch As Short = 23

    Private Const ColVarianceAmount As Short = 24
    Private Const ColDeptCode As Short = 25

    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub cboCatType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCatType.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub cboCatType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCatType.SelectedIndexChanged
        PrintStatus(False)
    End Sub

    Private Sub cboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
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

    Private Sub chkDespatch_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDespatch.CheckStateChanged
        PrintStatus(False)
    End Sub

    Private Sub chkItemAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkItemAll.CheckStateChanged
        txtItemName.Enabled = IIf(chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdItemDesc.Enabled = IIf(chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub chkModel_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkModel.CheckStateChanged
        txtModel.Enabled = IIf(chkModel.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearchModel.Enabled = IIf(chkModel.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub


    Private Sub chkPhyInventory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPhyInventory.CheckStateChanged
        PrintStatus(False)
    End Sub

    Private Sub chkShowDeptQty_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShowDeptQty.CheckStateChanged
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

            .Col = ColFYOpening
            If lblBookType.Text = "F" Then
                .Text = "FG FY Opening"
            ElseIf lblBookType.Text = "W" Then
                .Text = "Dept FY Opening"
            Else
                .Text = "FY Opening"
            End If

            .Col = ColOpPhyVariance
            If lblBookType.Text = "F" Then
                .Text = "FG Opening Physical Variance"
            ElseIf lblBookType.Text = "W" Then
                .Text = "Dept Opening Physical Variance"
            Else
                .Text = "Opening Physical Variance"
            End If

            .Col = ColOpening
            If chkPhyOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
                If lblBookType.Text = "F" Then
                    .Text = "FG Physical Opening"
                ElseIf lblBookType.Text = "W" Then
                    .Text = "Dept Physical Opening"
                Else
                    .Text = "Physical Opening"
                End If
            Else
                If lblBookType.Text = "F" Then
                    .Text = "FG Opening"
                ElseIf lblBookType.Text = "W" Then
                    .Text = "Dept Opening"
                Else
                    .Text = "Opening"
                End If
            End If

            ''If lblBookType.text = "S" Then
            '        .Col = ColTodayReceipt
            '        If lblBookType.text = "F" Then
            '            .Text = "Today FG IN (From PDI)"
            '        ElseIf lblBookType.text = "W" Then
            '            .Text = "Today Receipt from Store"
            '        Else
            '            .Text = "Today Purchase"
            '        End If

            .Col = ColReceipt1
            If lblBookType.Text = "F" Then
                .Text = "FG IN (From PDI)"
            ElseIf lblBookType.Text = "W" Then
                .Text = "Receipt from Store / Production"
            Else
                .Text = "Purchase / Under QC"
            End If
            '
            '        .Col = ColReceipt2
            '        If lblBookType.text = "F" Then
            '            .Text = "Other Receipt"
            '        ElseIf lblBookType.text = "W" Then
            '            .Text = "Production"
            '        Else
            '            .Text = "Receipt Other Than Purchase"
            '        End If

            '        .Col = ColTodayIssue
            '        If lblBookType.text = "F" Then
            '            .Text = "Today FG Sale"
            '        ElseIf lblBookType.text = "W" Then
            '            .Text = "Today Issue to Next Shop"
            '        Else
            '            .Text = "Today Issue to Floor"
            '        End If

            .Col = ColIssue1
            If lblBookType.Text = "F" Then
                .Text = "FG Sale"
            ElseIf lblBookType.Text = "W" Then
                .Text = "Issue to Store / PAD/ Next Shop"
            Else
                .Text = "Issue to Floor"
            End If

            '        .Col = ColIssue2
            '        If lblBookType.text = "F" Then
            '            .Text = "Other Issue"
            '        ElseIf lblBookType.text = "W" Then
            '            .Text = "Less from Production / Next Dept"
            '        Else
            '            .Text = "Issue Other Than Floor"
            '        End If

            .Col = ColAdj
            .Text = "Adjustment"

            '        .Col = ColClosing
            '        If lblBookType.text = "F" Then
            '            .Text = "FG Closing"
            '        Else
            '            .Text = "Closing"
            '        End If

            .Col = ColRejectionOP
            If lblBookType.Text = "F" Then
                .Text = "Opening Rejection / Scrap"
            ElseIf lblBookType.Text = "W" Then
                .Text = "Opening Rejection / Rework"
            Else
                .Text = "Opening Rejection (Party / Floor)"
            End If

            .Col = ColRejectionRecd
            If lblBookType.Text = "F" Then
                .Text = "Rejection / Scrap Received"
            ElseIf lblBookType.Text = "W" Then
                .Text = "Rejection / Rework Received"
            Else
                .Text = "Rejection Received (Party / Floor)"
            End If

            .Col = ColRejectionSend
            If lblBookType.Text = "F" Then
                .Text = "Rejection / Scrap Done"
            ElseIf lblBookType.Text = "W" Then
                .Text = "Rejection / Rework Send to Store"
            Else
                .Text = "Rejection Send to Party"
            End If


            .Col = ColRejection
            If lblBookType.Text = "F" Then
                .Text = "Customer Rejection / Scrap Balance"
            ElseIf lblBookType.Text = "W" Then
                .Text = "Rejection / Rework Balance"
            Else
                .Text = "Rejection Balance"
            End If

            '        .Col = ColUnderQC
            '        If lblBookType.text = "W" Then
            '            .Text = "Under WIP"
            '        Else
            '            .Text = "Under QC"
            '        End If

            .Col = ColDeptStock
            .Text = "Dept. Closing"

            '        .Col = ColAdjQty
            '        .Text = "Adjustment Qty"

            .Col = ColTotalClosing
            .Text = "Total Closing"

            .Col = ColPhyBal
            .Text = "Physical Balance"

            .Col = ColPhyDiff
            .Text = "Variance on Physical"

            .Col = ColDespatch
            .Text = "FG Despatch"

            .Col = ColRate
            .Text = "Rate"

            .Col = ColClosingValue
            .Text = "Closing Value"

            .Col = ColVarianceAmount
            .Text = "Variance Amount"

            .Col = ColDeptCode
            .Text = "Dept Code"
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
            .set_RowHeight(0, 2.5 * RowHeight)
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

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColDeptCode, 4.5)

            For I = ColFYOpening To ColVarianceAmount
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(I, 9)
            Next

            .ColsFrozen = ColUnit

            If lblBookType.Text = "F" Then
                .Col = ColDespatch
                .ColHidden = True
            End If

            If lblBookType.Text = "W" Then
                .Col = ColDeptCode
                .ColHidden = False
            Else
                .Col = ColDeptCode
                .ColHidden = True
            End If

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

        '    If InsertIntoTempTable = False Then GoTo ERR1

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1
        'Select Record for print...

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        If chkPhyInventory.CheckState = System.Windows.Forms.CheckState.Checked Then
            mRPTName = "InvReportPhyStr.rpt"
        Else
            mRPTName = "InvReportStr.rpt"
        End If
        If lblBookType.Text = "S" Then
            mTitle = "Inventory Report (Store)"
        ElseIf lblBookType.Text = "F" Then
            mTitle = "Inventory Report (Finished Goods)"
        ElseIf lblBookType.Text = "W" Then
            mTitle = "Inventory Report (WIP)"
        ElseIf lblBookType.Text = "R" Then
            mTitle = "Inventory Report (Rework)"
        End If

        mTitle = mTitle & " From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")


        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = "(Category : " & txtCatName.Text & ")"
        End If

        If chkSubCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & " (Sub Category : " & txtSubCatName.Text & ")"
        End If

        If chkModel.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & " (Model : " & txtModel.Text & ")"
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
        Dim CntRow As Integer

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
        Dim mDeptStock As Double
        Dim mTotClosing As Double
        Dim mRate As Double
        Dim mValue As Double

        'Dim PvtDBCn As ADODB.Connection

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_STOCKONHAND NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        '    If lblbookType.text = "N" Then
        '        mSqlStr = MakeSQLS2
        '    Else
        '        mSqlStr = MakeSQLS
        '    End If

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

                .Col = ColReceipt1
                mReceipt = Val(.Text)

                .Col = ColIssue1
                mIssue = Val(.Text)

                '            .Col = ColClosing
                '            mClosing = Val(.Text)

                .Col = ColRejection
                mRej = Val(.Text)

                '            .Col = ColUnderQC
                '            mQC = Val(.Text)

                .Col = ColDeptStock
                mDeptStock = Val(.Text)

                .Col = ColTotalClosing
                mTotClosing = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColClosingValue
                mValue = Val(.Text)

                SqlStr = " INSERT INTO TEMP_STOCKONHAND (USERID, CATEGORY_CODE, " & vbCrLf & " SUBCATEGORY_CODE, ITEM_CODE, ITEM_NAME, ITEM_UOM, " & vbCrLf & " OPENING, RECEIPT, ISSUE, CLOSING, " & vbCrLf & " REJECTION, UNDERQC, DEPT_STOCK, TOT_CLOSING, RATE," & vbCrLf & " VALUE) VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', '" & MainClass.AllowSingleQuote(mCategoryCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mSubCategoryCode) & "', '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemName) & "', '" & MainClass.AllowSingleQuote(mItemUOM) & "', " & vbCrLf & " " & Val(CStr(mOpening)) & ", " & Val(CStr(mReceipt)) & "," & Val(CStr(mIssue)) & "," & Val(CStr(mClosing)) & "," & vbCrLf & " " & Val(CStr(mRej)) & ", " & Val(CStr(mQC)) & ", " & Val(CStr(mDeptStock)) & "," & Val(CStr(mTotClosing)) & ", " & Val(CStr(mRate)) & "," & vbCrLf & " " & Val(CStr(mValue)) & ")"

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


        mSqlStr = " SELECT STOCK.*, " & vbCrLf & " CATMST.GEN_DESC, SUBCATMST.SUBCATEGORY_DESC " & vbCrLf & " FROM TEMP_STOCKONHAND STOCK, INV_GENERAL_MST CATMST, " & vbCrLf & " INV_SUBCATEGORY_MST SUBCATMST " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND CATMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND STOCK.CATEGORY_CODE=CATMST.GEN_CODE" & vbCrLf & " AND SUBCATMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND STOCK.SUBCATEGORY_CODE=SUBCATMST.SUBCATEGORY_CODE" & vbCrLf & " AND CATMST.COMPANY_CODE=SUBCATMST.COMPANY_CODE" & vbCrLf & " AND CATMST.GEN_CODE=SUBCATMST.CATEGORY_CODE" & vbCrLf & " AND CATMST.GEN_TYPE='C' " & vbCrLf & " ORDER BY STOCK.CATEGORY_CODE, SUBCATMST.SUBCATEGORY_DESC, STOCK.ITEM_CODE "

        FetchRecordForReport = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName


        MainClass.AssignCRptFormulas(Report1, "InvFlag=""" & UCase(Trim(lblBookType.Text)) & """")


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

    Private Sub cmdSearchModel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchModel.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        If MainClass.SearchGridMaster((txtModel.Text), "GEN_MODEL_MST", "MODEL_DESC", "MODEL_CODE", , , SqlStr) = True Then
            txtModel.Text = AcName
            txtModel_Validating(txtModel, New System.ComponentModel.CancelEventArgs(False))
            txtModel.Focus()
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

        If chkModel.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtModel.Text) = "" Then
                MsgInformation("Please Select Model Name.")
                FieldsVarification = False
                txtModel.Focus()
            Else
                If MainClass.ValidateWithMasterTable((txtModel.Text), "MODEL_DESC", "MODEL_CODE", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("Invalid Model Name.")
                    FieldsVarification = False
                    txtModel.Focus()
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

        If lblBookType.Text = "S" Then
            SqlStr = MakeSQLS
        ElseIf lblBookType.Text = "F" Then
            SqlStr = MakeSQLF
        ElseIf lblBookType.Text = "W" Then
            SqlStr = MakeSQLW
        ElseIf lblBookType.Text = "R" Then
            SqlStr = ""
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Show1 = True
        Exit Function
InsertErr:
        Show1 = False
        MsgBox(Err.Description)
        ''Resume
    End Function



    Private Function MakeSQLS() As String

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

        mHavingClause = False
        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        mTableName = ConInventoryTable

        mDeptFunction = "GETDEPTSTOCK"

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, "

        '    'ColFYOpening
        '    SqlStr = SqlStr & vbCrLf & " 0, 0, "


        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS FYOpening,"

        'ColOPAdjustment ..ColOpPhyVariance
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE = ('ADJ')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)" & vbCrLf & ") AS OPAdjQTY, "

        If chkPhyOpening.CheckState = System.Windows.Forms.CheckState.Checked And CDate(RsCompany.Fields("Start_Date").Value) <> CDate(VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")) Then
            SqlStr = SqlStr & vbCrLf & " 0, "
        Else
            'ColOpening  'AND (STOCK_TYPE ='ST') Gross Opening
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening,"
        End If

        '    'ColtodayReceipt
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " TO_CHAR(SUM(CASE WHEN E_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE='MRR'" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS TodayReceipt, "

        ''AND STOCK_TYPE IN ('ST','RJ')
        'ColReceipt1
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND STOCK_TYPE IN ('ST','QC') AND REF_TYPE='MRR'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END) +  "

        ''Division Transfer IN ..
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND REF_TYPE='DTN'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END) +  "

        'Despatch
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE  IN ('RGP','NRG','DSP') THEN ITEM_QTY * DECODE(ITEM_IO,'O',-1,0) ELSE 0 END) +"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE ='SRN' THEN ITEM_QTY * DECODE(ITEM_IO,'O',-1,0) ELSE 0 END) +"
        '    SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='RJ' AND REF_TYPE ='SRN' THEN ITEM_QTY * DECODE(ITEM_IO,'I',-1,0) ELSE 0 END) +"

        ' Add Receipt2
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE NOT IN ('MRR','ADJ','SRN','DTN')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS RECEIPT, "

        '      'Add UnderQC
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND (STOCK_TYPE='QC' OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS RECEIPT,"

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " CASE WHEN SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE = ('ADJ')" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) > 0 THEN " & vbCrLf _
        ''            & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE = ('ADJ')" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) ELSE 0 END " & vbCrLf _
        ''            & ") AS Receipt, "

        '    'ColTodayIssue
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " TO_CHAR(SUM(CASE WHEN E_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE IN ('ISS','PMD')" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS TodayIssue, "

        'ColIssue1
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE IN ('ISS','PMD')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END) + "

        '    'Store Return
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE IN ('SRN')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',-1,0) ELSE 0 END) + "

        ''Division Transfer OUT ..
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND REF_TYPE='DTN'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END) +  "

        'Add Issue2
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE NOT IN ('ISS','PMD','ADJ','SRN','DTN','DSP','RGP','NRG')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS ISSUE,"


        'ColAdjustment
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE = ('ADJ')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * DECODE(STOCK_TYPE,'RJ',0,1) ELSE 0 END)" & vbCrLf & ") AS AdjQTY, "

        '    'ColClosing
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN STOCK_TYPE='ST' OR STOCK_TYPE='QC' THEN 1 ELSE 0 END )) as Closing, "

        ''E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND

        '    'ColOPRejection
        '
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE='RJ'  " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS RejOP, "

        'ColRejectionRecd5
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,0) * DECODE(STOCK_TYPE,'RJ',1,0) * CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as RejectionRecd, "

        '    ColRejectionSend

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,0) * DECODE(STOCK_TYPE,'RJ',1,0) * CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as RejectionSend, "


        'ColRejection
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE='RJ'  " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Rejection, "



        'ColDeptStock
        If chkShowDeptQty.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(" & mDeptFunction & "(" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,TO_DATE('" & mToDate & "','DD-MON-YYYY'))) as DeptClosing, "
        Else
            SqlStr = SqlStr & vbCrLf & " TO_CHAR('0.00'),"
        End If
        'ColTotalClosing

        SqlStr = SqlStr & vbCrLf & " TO_CHAR('0.00'),"

        '    If chkShowDeptQty.Value = vbChecked Then
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " TO_CHAR(" & mDeptFunction & "(" & RsCompany.fields("COMPANY_CODE").value & "," & RsCompany.fields("FYEAR").value & ",ITEM.ITEM_CODE,'" & mToDate & "')+SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as TotClosing, "
        '    Else
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as TotClosing, "
        '    End If
        '
        'ColPhyBal,ColPhyDiff,ColDespatch,


        SqlStr = SqlStr & vbCrLf & " TO_CHAR(MAX(NVL(INV.PURCHASE_COST,0))) as Rate, 0, " ''& vbCrLf |            & " TO_CHAR(CASE WHEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>=0 THEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))*AVG(NVL(INV.PURCHASE_COST,0)) ELSE 0 END) as Value "

        SqlStr = SqlStr & vbCrLf & " 0, 0, 0,0, 'STR' AS DEPT"

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " INV, " & vbCrLf & " INV_ITEM_MST ITEM, INV_GENERAL_MST CMST "


        ''********** WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " WHERE INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'"


        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('ST','RJ','QC') "


        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf & " AND ITEM.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND ITEM.CATEGORY_CODE=CMST.GEN_CODE AND GEN_TYPE='C' "

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INV.DIV_CODE=" & mDivision & ""
            End If
        End If

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
                mItemCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
            End If
        End If

        If chkModel.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_MODEL='" & MainClass.AllowSingleQuote(txtModel.Text) & "'"
        End If

        If CboItemClass.SelectedIndex <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_CLASS='" & VB.Left(CboItemClass.Text, 1) & "'"
        End If

        If cboCatType.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND CMST.PRD_TYPE='" & VB.Left(cboCatType.Text, 1) & "' "
        End If

        ''AND DEPT_CODE_TO='STR'
        If cboShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        ElseIf cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        End If

        If Val(txtLocation.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_LOCATION ='" & Trim(txtLocation.Text) & "'"
        End If

        '    If cboExportItem.ListIndex >= 1 Then
        '        SqlStr = SqlStr & vbCrLf & " AND ITEM.IS_EXPORT_ITEM = '" & vb.Left(cboExportItem.Text, 1) & "'"
        '    End If

        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"
        End If

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        '    If chkOption.Value = vbChecked Then
        '        SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        '        mHavingClause = True
        '    Else
        '        If lblBookType.text = "B" Then
        '            SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<ITEM.MINIMUM_QTY"
        '            mHavingClause = True
        '        ElseIf lblBookType.text = "A" Then
        '            SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>ITEM.MAXIMUM_QTY"
        '            mHavingClause = True
        '        End If
        '    End If
        '
        '    If chkZeroBal.Value = vbChecked Then
        '        If mHavingClause = False Then
        '            SqlStr = SqlStr & vbCrLf & " HAVING "
        '        Else
        '            SqlStr = SqlStr & vbCrLf & " AND "
        '        End If
        '
        '        SqlStr = SqlStr & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"
        '    End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM "

        If lblBookType.Text = "B" Then
            SqlStr = SqlStr & vbCrLf & " , ITEM.MINIMUM_QTY"
        ElseIf lblBookType.Text = "A" Then
            SqlStr = SqlStr & vbCrLf & " , ITEM.MAXIMUM_QTY"
        End If

        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE "


        MakeSQLS = SqlStr
        Exit Function
InsertErr:
        MakeSQLS = ""
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Function MakeSQLF() As String

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

        mHavingClause = False
        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        mTableName = ConInventoryTable

        mDeptFunction = "GETDEPTSTOCK"

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, "

        'ColOpening
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='FG' " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening,"

        '    'ColTodayReceipt
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " TO_CHAR(SUM(CASE WHEN E_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='FG' AND REF_TYPE='PMO'" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS TodayReceipt, "

        'ColReceipt1
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='FG' AND REF_TYPE='PMO'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END) +  "

        'ColReceipt2
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE NOT IN ('FG','RJ','SC','CR') AND REF_TYPE<>'PMO'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END) + "

        'ColUnderQC
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND (STOCK_TYPE='QC' OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Receipt, "

        '    'ColTodayIssue
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " TO_CHAR(SUM(CASE WHEN E_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='FG' AND REF_TYPE ='DSP'" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS TodayIssue, "

        'ColIssue1
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='FG' AND REF_TYPE ='DSP'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END) + "

        'ColIssue2
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE NOT IN ('FG','RJ','SC','CR') AND REF_TYPE <> 'DSP'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS Issue2, "

        'ColAdjustment
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE = ('ADJ')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)" & vbCrLf & ") AS AdjQTY, "

        'ColClosing
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * DECODE(STOCK_TYPE,'FG',1,0) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as Closing, "

        'ColRejectionRecd

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('RJ','SC','CR')  " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) + "

        SqlStr = SqlStr & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,0) * DECODE(STOCK_TYPE,'RJ',1,DECODE(STOCK_TYPE,'CR',1,DECODE(STOCK_TYPE,'SC',1,0))) * CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as RejectionRecd, "

        '    ColRejectionSend
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,0) * DECODE(STOCK_TYPE,'RJ',1,DECODE(STOCK_TYPE,'CR',1,DECODE(STOCK_TYPE,'SC',1,0))) * CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as RejectionSend, "


        'ColRejection
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('RJ','SC','CR')  " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Rejection, "


        'ColDeptStock
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(" & mDeptFunction & "(" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,TO_DATE('" & mToDate & "','DD-MON-YYYY'))) as DeptClosing, "

        'ColTotalClosing
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(" & mDeptFunction & "(" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,TO_DATE('" & mToDate & "','DD-MON-YYYY'))+SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as TotClosing, "

        'ColPhyBal,ColPhyDiff,ColDespatch,
        SqlStr = SqlStr & vbCrLf & " 0, 0, 0,"

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(MAX(NVL(INV.PURCHASE_COST,0))) as Rate, 0, '' " ''& vbCrLf |            & " TO_CHAR(CASE WHEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>=0 THEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))*AVG(NVL(INV.PURCHASE_COST,0)) ELSE 0 END) as Value "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " INV, " & vbCrLf & " INV_ITEM_MST ITEM, INV_GENERAL_MST GMST "


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " WHERE INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'"


        SqlStr = SqlStr & vbCrLf & " AND GMST.STOCKTYPE ='FG' "


        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf & " AND ITEM.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf & " AND ITEM.CATEGORY_CODE=GMST.GEN_CODE AND GMST.GEN_TYPE='C' "

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INV.DIV_CODE=" & mDivision & ""
            End If
        End If


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
                mItemCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
            End If
        End If

        If chkModel.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_MODEL='" & MainClass.AllowSingleQuote(txtModel.Text) & "'"
        End If

        If cboCatType.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND CMST.PRD_TYPE='" & VB.Left(cboCatType.Text, 1) & "' "
        End If

        ''AND DEPT_CODE_TO='STR'
        If cboShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        ElseIf cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        End If

        If Val(txtLocation.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_LOCATION ='" & Trim(txtLocation.Text) & "'"
        End If

        '    If cboExportItem.ListIndex >= 1 Then
        '        SqlStr = SqlStr & vbCrLf & " AND ITEM.IS_EXPORT_ITEM = '" & vb.Left(cboExportItem.Text, 1) & "'"
        '    End If

        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"
        End If

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        '    If chkOption.Value = vbChecked Then
        '        SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        '        mHavingClause = True
        '    Else
        '        If lblBookType.text = "B" Then
        '            SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<ITEM.MINIMUM_QTY"
        '            mHavingClause = True
        '        ElseIf lblBookType.text = "A" Then
        '            SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>ITEM.MAXIMUM_QTY"
        '            mHavingClause = True
        '        End If
        '    End If
        '
        '    If chkZeroBal.Value = vbChecked Then
        '        If mHavingClause = False Then
        '            SqlStr = SqlStr & vbCrLf & " HAVING "
        '        Else
        '            SqlStr = SqlStr & vbCrLf & " AND "
        '        End If
        '
        '        SqlStr = SqlStr & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"
        '    End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM "

        If lblBookType.Text = "B" Then
            SqlStr = SqlStr & vbCrLf & " , ITEM.MINIMUM_QTY"
        ElseIf lblBookType.Text = "A" Then
            SqlStr = SqlStr & vbCrLf & " , ITEM.MAXIMUM_QTY"
        End If

        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE "


        MakeSQLF = SqlStr
        Exit Function
InsertErr:
        MakeSQLF = ""
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Function MakeSQLW() As String

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

        mHavingClause = False
        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        mTableName = ConInventoryTable

        ''        mDeptFunction = "GETDEPTSTOCK"



        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, "

        '    'ColFYOpening
        '    SqlStr = SqlStr & vbCrLf & " 0, 0, "

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS FYOpening,"

        'ColOPAdjustment ..ColOpPhyVariance
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE = ('ADJ')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)" & vbCrLf & ") AS OPAdjQTY, "

        'ColOpening ''AND STOCK_TYPE ='ST' ''Gross
        If chkPhyOpening.CheckState = System.Windows.Forms.CheckState.Checked And CDate(RsCompany.Fields("Start_Date").Value) <> CDate(VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")) Then
            SqlStr = SqlStr & vbCrLf & " 0, "
        Else
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening,"
        End If


        '    'ColTodayReceipt
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " TO_CHAR(SUM(CASE WHEN E_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE IN ('ISS','PBU')" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS TodayReceipt, "

        SqlStr = SqlStr & vbCrLf & "TO_CHAR("
        'ISS
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE IN ('ISS','PBU')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END) + "

        'ColStore Return
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE IN ('SRN')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'O',-1,0) ELSE 0 END) + "

        'ColProductionIN
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE IN ('PMD')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END) + "

        'Other Than
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE NOT IN ('ISS','PBU','SRN','ADJ','PMD')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END) + "

        'ColWP
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND (STOCK_TYPE='WP' OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)"

        SqlStr = SqlStr & vbCrLf & ") AS Receipt,"

        '     'ColAdjustment
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " CASE WHEN SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE = ('ADJ')" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) < 0 THEN " & vbCrLf _
        ''            & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE = ('ADJ')" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) ELSE 0 END " & vbCrLf _
        ''            & ") AS Receipt, "

        '    'ColTodayIssue
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " TO_CHAR(SUM(CASE WHEN E_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE ='PMD'" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS TodayIssue, "

        'ColIssue1
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE  NOT IN ('ADJ','SRN')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS Issue, "

        '    'ColIssue2
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE ='ST' AND REF_TYPE ='PMD'" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS Issue, "

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " CASE WHEN SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE = ('ADJ')" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) < 0 THEN " & vbCrLf _
        ''            & " SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE = ('ADJ')" & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) ELSE 0 END " & vbCrLf _
        ''            & ") AS Issue, "

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE = ('ADJ')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)" & vbCrLf & ") AS AdjQTY, "

        '    'ColClosing
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * DECODE(STOCK_TYPE,'ST',1,0) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REF_TYPE <>'ADJ' THEN 1 ELSE 0 END )) as Closing, "

        'ColRejectionRecd
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('WR','RJ','SC')  " & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END) + "

        'ColRejectionOp
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('WR','RJ','SC')  " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS RejectionOP, "

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,0) * DECODE(STOCK_TYPE,'RJ',1,0) * CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as RejectionRecd, "

        '    ColRejectionSend
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,0) * DECODE(STOCK_TYPE,'RJ',1,0) * CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as RejectionSend, "

        'ColRejection
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('WR','RJ','SC')  " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Rejection, "

        'ColDeptStock
        SqlStr = SqlStr & vbCrLf & " TO_CHAR('0.00') as DeptClosing, "

        'ColTotalClosing
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as TotClosing, "



        'ColRate,ColClosingValue,ColPhyBal,ColPhyDiff,ColDespatch,ColVarianceAmount,ColDeptCode
        SqlStr = SqlStr & vbCrLf & " 0, 0, 0,0, 0, 0,DECODE(INV.DEPT_CODE_TO,NULL,'PAD',INV.DEPT_CODE_TO) AS DEPT_CODE"

        '', DECODE(INV.DEPT_CODE_TO,NULL,'PAD',INV.DEPT_CODE_TO) AS DEPT_CODE

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " TO_CHAR(MAX(NVL(INV.PURCHASE_COST,0))) as Rate, 0"    ''& vbCrLf _
        ''            & " TO_CHAR(CASE WHEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>=0 THEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))*AVG(NVL(INV.PURCHASE_COST,0)) ELSE 0 END) as Value "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " INV, " & vbCrLf & " INV_ITEM_MST ITEM , INV_GENERAL_MST CMST "


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " WHERE INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND INV.STOCK_ID='" & ConPH & "'"


        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('ST','RJ','QC','WP') "


        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf & " AND ITEM.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND ITEM.CATEGORY_CODE=CMST.GEN_CODE AND GEN_TYPE='C' "

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INV.DIV_CODE=" & mDivision & ""
            End If
        End If

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
                mItemCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
            End If
        End If

        If chkModel.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_MODEL='" & MainClass.AllowSingleQuote(txtModel.Text) & "'"
        End If

        ''AND DEPT_CODE_TO='STR'
        If cboShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        ElseIf cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        End If

        If cboCatType.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND CMST.PRD_TYPE='" & VB.Left(cboCatType.Text, 1) & "' "
        End If

        If Val(txtLocation.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_LOCATION ='" & Trim(txtLocation.Text) & "'"
        End If

        '    If cboExportItem.ListIndex >= 1 Then
        '        SqlStr = SqlStr & vbCrLf & " AND ITEM.IS_EXPORT_ITEM = '" & vb.Left(cboExportItem.Text, 1) & "'"
        '    End If

        If cboDept.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & vbCrLf & " AND ( INV.DEPT_CODE_TO='" & Trim(MasterNo) & "' OR INV.DEPT_CODE_FROM='" & Trim(MasterNo) & "')"
            End If
        End If

        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"
        End If

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        '    If chkOption.Value = vbChecked Then
        '        SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        '        mHavingClause = True
        '    Else
        '        If lblBookType.text = "B" Then
        '            SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<ITEM.MINIMUM_QTY"
        '            mHavingClause = True
        '        ElseIf lblBookType.text = "A" Then
        '            SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>ITEM.MAXIMUM_QTY"
        '            mHavingClause = True
        '        End If
        '    End If
        '
        '    If chkZeroBal.Value = vbChecked Then
        '        If mHavingClause = False Then
        '            SqlStr = SqlStr & vbCrLf & " HAVING "
        '        Else
        '            SqlStr = SqlStr & vbCrLf & " AND "
        '        End If
        '
        '        SqlStr = SqlStr & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"
        '    End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM,DECODE(INV.DEPT_CODE_TO,NULL,'PAD',INV.DEPT_CODE_TO) " '',DECODE(INV.DEPT_CODE_TO,NULL,'PAD',INV.DEPT_CODE_TO)

        If lblBookType.Text = "B" Then
            SqlStr = SqlStr & vbCrLf & " , ITEM.MINIMUM_QTY"
        ElseIf lblBookType.Text = "A" Then
            SqlStr = SqlStr & vbCrLf & " , ITEM.MAXIMUM_QTY"
        End If

        SqlStr = SqlStr & vbCrLf & "ORDER BY DECODE(INV.DEPT_CODE_TO,NULL,'PAD',INV.DEPT_CODE_TO)," & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE "


        MakeSQLW = SqlStr
        Exit Function
InsertErr:
        MakeSQLW = ""
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Sub frmParamStockSTRReport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim mIsAuthorisedUser As String
        Dim SqlStr As String = ""
        If FormLoaded = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblBookType.Text = "S" Then
            Me.Text = "Inventory Report (Store)"
            chkShowDeptQty.Enabled = True
            chkShowDeptQty.Visible = True
        ElseIf lblBookType.Text = "F" Then
            Me.Text = "Inventory Report (Finished Goods)"
            chkShowDeptQty.Enabled = False
            chkShowDeptQty.Visible = False
        ElseIf lblBookType.Text = "W" Then
            Me.Text = "Inventory Report (WIP)"
            chkShowDeptQty.Enabled = False
            chkShowDeptQty.Visible = False
        ElseIf lblBookType.Text = "R" Then
            Me.Text = "Inventory Report (Rework)"
            chkShowDeptQty.Enabled = False
            chkShowDeptQty.Visible = False
        End If

        mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        If InStr(1, mIsAuthorisedUser, "S") = 0 Then
            chkViewAll.Enabled = False
        Else
            chkViewAll.Enabled = True
        End If

        If lblBookType.Text = "F" Then
            chkDespatch.Enabled = False
            chkDespatch.Visible = False
        End If
        FormLoaded = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub frmParamStockSTRReport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        Dim mFromDate As String
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

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

        chkModel.CheckState = System.Windows.Forms.CheckState.Checked
        txtModel.Enabled = False
        cmdSearchModel.Enabled = False


        '    cboExportItem.Clear
        '    cboExportItem.AddItem "All"
        '    cboExportItem.AddItem "Yes"
        '    cboExportItem.AddItem "No"
        '    cboExportItem.ListIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("Active Only")
        cboShow.Items.Add("Inactive Only")
        cboShow.Items.Add("Both")
        cboShow.SelectedIndex = 0

        cboCatType.Items.Clear()
        cboCatType.Items.Add("All")
        cboCatType.Items.Add("General")
        cboCatType.Items.Add("Production")
        cboCatType.Items.Add("Jobwork")
        cboCatType.Items.Add("Consumable (Production)")
        cboCatType.Items.Add("Tool")
        cboCatType.Items.Add("Assets")
        cboCatType.Items.Add("Raw Material")
        cboCatType.Items.Add("BOP")
        cboCatType.Items.Add("InHouse")

        cboCatType.SelectedIndex = 0

        CboItemClass.Items.Clear()
        CboItemClass.Items.Add("ALL")
        CboItemClass.Items.Add("A Class")
        CboItemClass.Items.Add("B Class")
        CboItemClass.Items.Add("C Class")
        CboItemClass.Items.Add("DOL Class")
        CboItemClass.SelectedIndex = 0

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

        Call MainClass.FillCombo(cboDept, "PAY_DEPT_MST", "DEPT_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        cboDept.SelectedIndex = 0

        mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -3, CDate(txtDateTo.Text)))
        txtDateFrom.Text = IIf(CDate(mFromDate) < CDate(RsCompany.Fields("Start_Date").Value), RsCompany.Fields("Start_Date").Value, mFromDate)

        FormatSprdMain(-1)
        FillSprdMain()
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub frmParamStockSTRReport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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


    Private Sub frmParamStockSTRReport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

        Dim mPhyBal As Double
        Dim pDespQty As Double
        Dim mPhyDiff As Double
        Dim mLastPhyDate As String


        Dim mOpening As Double
        Dim mReceipt1 As Double
        Dim mIssue1 As Double
        Dim mAdj As Double
        Dim mRejectionRecd As Double
        Dim mRejectionSend As Double
        Dim mREJECTION As Double
        Dim mDeptStock As Double
        Dim mDeptCode As String
        Dim mProdType As String
        Dim mTotValue As Double
        Dim mRejectionOP As Double
        Dim mDivision As Double

        If CDate(RsCompany.Fields("Start_Date").Value) = CDate(VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")) Then
            mLastPhyDate = VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")
        Else
            mLastPhyDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtDateFrom.Text, "DD/MM/YYYY"))))
        End If

        mDivision = -1
        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
            End If
        End If

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColUnit
                mItemUOM = Trim(.Text)

                .Col = ColDeptCode
                mDeptCode = Trim(.Text)

                If CDate(RsCompany.Fields("Start_Date").Value) = CDate(VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")) Then
                    .Col = ColOpening
                    mOpening = Val(.Text)
                Else
                    If chkPhyOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
                        .Col = ColOpening
                        If lblBookType.Text = "S" Then
                            .Text = CStr(GetPhysicalBalance(mItemCode, mLastPhyDate, mItemUOM, ConWH, "ST", "", mDivision) + GetPhysicalBalance(mItemCode, mLastPhyDate, mItemUOM, ConWH, "RJ", "", mDivision))
                        ElseIf lblBookType.Text = "F" Then
                            .Text = CStr(0)
                        ElseIf lblBookType.Text = "W" Then
                            .Text = CStr(GetPhysicalBalance(mItemCode, mLastPhyDate, mItemUOM, ConPH, "ST", mDeptCode, mDivision))
                        ElseIf lblBookType.Text = "R" Then
                            .Text = CStr(0)
                        End If

                        mOpening = Val(.Text)

                    Else
                        .Col = ColOpening
                        mOpening = Val(.Text)

                    End If
                End If

                If lblBookType.Text = "W" And chkDespatch.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mProdType = GetProductionType(mItemCode)
                    If mProdType = "P" Then
                        pDespQty = GetProductionQty_Seq1(mItemCode, (txtDateFrom.Text), (txtDateTo.Text))
                        .Col = ColReceipt1
                        .Text = VB6.Format(pDespQty, "0.00")
                        pDespQty = 0

                        pDespQty = GetFGDespatchQty(mItemCode, mItemUOM, (txtDateFrom.Text), (txtDateTo.Text), PubDBCn)
                        .Col = ColIssue1
                        .Text = VB6.Format(pDespQty, "0.00")
                        pDespQty = 0
                    End If
                End If

                .Col = ColReceipt1
                mReceipt1 = Val(.Text)

                .Col = ColIssue1
                mIssue1 = Val(.Text)

                .Col = ColAdj
                mAdj = Val(.Text)

                .Col = ColRejectionOP
                mRejectionOP = Val(.Text)

                .Col = ColRejectionRecd
                mRejectionRecd = Val(.Text)

                .Col = ColRejectionSend
                mRejectionSend = Val(.Text)

                .Col = ColDeptStock
                mDeptStock = Val(.Text)

                '            mClosing = mOpening + mReceipt1 - mIssue1 + mAdj + mRejectionRecd - mRejectionSend + mDeptStock
                mClosing = mOpening + mReceipt1 - mIssue1 + mAdj + mRejectionOP + mRejectionRecd - mRejectionSend + mDeptStock

                .Col = ColTotalClosing
                .Text = VB6.Format(mClosing, "0.00")


                .Col = ColDeptCode
                mDeptCode = Trim(.Text)

                .Col = ColPhyBal
                If chkPhyInventory.CheckState = System.Windows.Forms.CheckState.Checked Then
                    If lblBookType.Text = "S" Then
                        mPhyBal = GetPhysicalBalance(mItemCode, (txtDateTo.Text), mItemUOM, ConWH, "ST", "", mDivision)
                        mPhyBal = mPhyBal + GetPhysicalBalance(mItemCode, (txtDateTo.Text), mItemUOM, ConWH, "RJ", "", mDivision)
                        If chkShowDeptQty.CheckState = System.Windows.Forms.CheckState.Checked Then
                            mPhyBal = mPhyBal + GetPhysicalBalance(mItemCode, (txtDateTo.Text), mItemUOM, ConPH, "ST", "", mDivision)
                        End If
                    ElseIf lblBookType.Text = "F" Then
                        mPhyBal = 0
                    ElseIf lblBookType.Text = "W" Then
                        mPhyBal = GetPhysicalBalance(mItemCode, (txtDateTo.Text), mItemUOM, ConPH, "ST", mDeptCode, mDivision)
                        '                mPhyBal = mPhyBal + GetPhysicalWIPQty(mItemCode, ConPH, cboDept.Text, txtDateTo.Text)
                    ElseIf lblBookType.Text = "R" Then
                        mPhyBal = 0
                    End If
                Else
                    mPhyBal = 0
                End If

                .Text = VB6.Format(mPhyBal, "0.000")

                .Col = ColPhyDiff
                .Text = IIf(chkPhyInventory.CheckState = System.Windows.Forms.CheckState.Checked, mClosing - mPhyBal, 0)
                mPhyDiff = IIf(chkPhyInventory.CheckState = System.Windows.Forms.CheckState.Checked, mClosing - mPhyBal, 0)

                If chkDespatch.CheckState = System.Windows.Forms.CheckState.Checked Then
                    .Col = ColDespatch
                    If GetDespatchDetail(mItemCode, mItemUOM, (txtDateFrom.Text), (txtDateTo.Text), pDespQty, PubDBCn) = False Then GoTo ErrPart
                    .Text = VB6.Format(pDespQty, "0.00")
                End If

                SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR,PURCHASE_COST FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mIssueUOM = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                    mPurchaseUOM = IIf(IsDbNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                    mFactor = IIf(IsDbNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)
                    mItemCost = IIf(IsDbNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value)
                End If


                '            If GetLatestItemCostFromPO(mItemCode, mPurchaseRate, mLandedCost, txtDateTo.Text, "ST", "", mItemUOM, mFactor) = False Then GoTo ErrPart
                '
                '            mRate = IIf(mPurchaseRate = 0, mItemCost, mPurchaseRate)

                mTotValue = GetLatestItemCostFromMRR(mItemCode, mItemUOM, 1, (txtDateTo.Text), "L")

                '            If mClosing > 0 Then
                mRate = mTotValue ''/ mClosing
                '            Else
                '                mRate = "0.00"
                '            End If
                '
                .Col = ColRate
                .Text = VB6.Format(mRate, "0.000")

                .Col = ColClosingValue
                .Text = VB6.Format(mClosing * mRate, "0.000")

                .Col = ColVarianceAmount
                .Text = VB6.Format(mPhyDiff * mRate, "0.000")

                '            .Col = ColValue
                '            .Text = VB6.Format(mClosing * mRate, "0.000")

            Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function GetProductionQty_Seq1(ByRef mProductCode As String, ByRef mFromDate As String, ByRef mToDate As String) As Double

        On Error GoTo ERR1
        Dim cntCol As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mDeptCode As String

        mTable = ConInventoryTable


        mDeptCode = GetProductDept(mProductCode, 1, (txtDateTo.Text))

        SqlStr = " SELECT SUM(ITEM_QTY) AS ITEM_QTY " & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='" & ConPH & "' AND ITEM_IO='I'" & vbCrLf & " AND STOCK_TYPE='ST' " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' " & vbCrLf & " AND REF_TYPE='" & ConStockRefType_PMEMODEPT & "'" & vbCrLf & " AND DEPT_CODE_FROM='" & MainClass.AllowSingleQuote(mDeptCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetProductionQty_Seq1 = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.000"))
        Else
            GetProductionQty_Seq1 = 0
        End If

        Exit Function
ERR1:
        GetProductionQty_Seq1 = 0
        MsgInformation(Err.Description)
    End Function


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
    Private Sub txtModel_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModel.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtModel_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModel.DoubleClick
        Call cmdSearchModel_Click(cmdSearchModel, New System.EventArgs())
    End Sub
    Private Sub txtModel_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtModel.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtModel.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtModel_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtModel.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchModel_Click(cmdSearchModel, New System.EventArgs())
    End Sub

    Private Sub txtModel_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtModel.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String = ""
        'Dim mModel As String

        If Trim(txtModel.Text) = "" Then
            GoTo EventExitSub
        End If

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        If MainClass.ValidateWithMasterTable((txtModel.Text), "MODEL_DESC", "MODEL_CODE", "GEN_MODEL_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Invalid Model Description.", , MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
