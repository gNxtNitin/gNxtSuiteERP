Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamNonMovingStock
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
    Private Const ColOpening As Short = 7
    Private Const ColReceipt As Short = 8
    Private Const ColIssue As Short = 9
    Private Const ColClosing As Short = 10
    Private Const ColRejection As Short = 11
    Private Const ColUnderQC As Short = 12
    Private Const ColDeptStock As Short = 13
    Private Const ColTotalClosing As Short = 14
    Private Const ColRate As Short = 15
    Private Const ColValue As Short = 16
    Private Const ColDateofTRN As Short = 17
    Private Const ColDays As Short = 18

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

            .Col = ColOpening
            .Text = "Opening"

            .Col = ColReceipt
            .Text = "Receipt"

            .Col = ColIssue
            .Text = "Issue"

            .Col = ColClosing
            .Text = "Closing"

            .Col = ColRejection
            .Text = "Rejection"

            .Col = ColUnderQC
            .Text = "Under QC"

            .Col = ColDeptStock
            .Text = "Dept. Closing"

            .Col = ColTotalClosing
            .Text = "Total Closing"

            .Col = ColRate
            .Text = "Rate"

            .Col = ColValue
            .Text = "Value"

            .Col = ColDateofTRN
            .Text = "Date of Last Transaction"

            .Col = ColDays
            .Text = "Days"

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

            For I = ColOpening To ColValue
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                If I = ColValue Then
                    .set_ColWidth(I, 11)
                Else
                    .set_ColWidth(I, 8)
                End If
            Next

            .Col = ColDateofTRN
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColDateofTRN, 10)

            .Col = ColDays
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeNumberMax = CDbl("999999999")
            .TypeNumberMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColDays, 4.5)

            .ColsFrozen = ColClosing

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

        If InsertIntoTempTable = False Then GoTo ERR1

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)


        mRPTName = "StockOnHand.rpt"
        If optNonMoving(0).Checked = True Then
            mTitle = "Non-Moving Stock"
        Else
            mTitle = "Non-Issue Stock"
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
        Dim mSqlStr As String = ""
        'Dim PvtDBCn As ADODB.Connection

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_STOCKONHAND NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        If lblBookType.Text = "N" Then
            mSqlStr = MakeSQL2
        Else
            '        mSqlStr = MakeSQL
        End If

        SqlStr = " INSERT INTO TEMP_STOCKONHAND (USERID, CATEGORY_CODE, " & vbCrLf & " SUBCATEGORY_CODE, ITEM_CODE, ITEM_NAME, ITEM_UOM, " & vbCrLf & " OPENING, RECEIPT, ISSUE, CLOSING, " & vbCrLf & " REJECTION, UNDERQC, DEPT_STOCK, TOT_CLOSING, RATE," & vbCrLf & " VALUE) " & vbCrLf & mSqlStr

        PubDBCn.Execute(SqlStr)
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

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Show1()
        SprdMain.Refresh()
        FormatSprdMain(-1)
        FillSprdMain()
        Call FillDatainGrid()

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

        If lblBookType.Text = "N" Then
            SqlStr = MakeSQL2
        Else
            SqlStr = MakeSQL
        End If
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
        Dim mHavingClause As Boolean
        Dim mTableName As String
        Dim mToDate As String
        Dim mDeptFunction As String

        Dim mRMCatCode As String = ""
        Dim mRMCatCodeStr As String = ""
        Dim CntLst As Integer
        Dim mMaterialType As String

        mHavingClause = False
        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        mTableName = ConInventoryTable

        mDeptFunction = "GETDEPTSTOCK"


        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, "

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS Receipt, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS Issue, " & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * DECODE(STOCK_TYPE,'ST',1,DECODE(STOCK_TYPE,'CS',1,0)) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as Closing, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE='RJ'  " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Rejection, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND (STOCK_TYPE='QC' OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS UnderQC, " & vbCrLf & " TO_CHAR(" & mDeptFunction & "(" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,TO_DATE('" & mToDate & "','DD-MON-YYYY'))) as DeptClosing, " & vbCrLf & " TO_CHAR(" & mDeptFunction & "(" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,TO_DATE('" & mToDate & "','DD-MON-YYYY'))+SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as TotClosing, " & vbCrLf & " TO_CHAR(AVG(NVL(INV.PURCHASE_COST,0))) as Rate, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>=0 THEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))*AVG(NVL(INV.PURCHASE_COST,0)) ELSE 0 END) as Value "


        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " INV, " & vbCrLf & " INV_ITEM_MST ITEM "


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " WHERE INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'"

        If chkCS.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('CS','ST','RJ','QC') "
        Else
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('ST','RJ','QC') "
        End If

        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        For CntLst = 0 To lstMaterialType.Items.Count - 1
            If lstMaterialType.GetItemChecked(CntLst) = True Then
                mMaterialType = VB6.GetItemString(lstMaterialType, CntLst)
                If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mRMCatCode = IIf(IsDbNull(MasterNo), "", MasterNo)
                End If
                mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
            End If
        Next

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN " & mRMCatCodeStr & ""
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

        ''AND DEPT_CODE_TO='STR'
        If cboShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        ElseIf cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        End If

        If Val(txtLocation.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_LOCATION ='" & txtLocation.Text & "'"
        End If

        If cboExportItem.SelectedIndex >= 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.IS_EXPORT_ITEM = '" & VB.Left(cboExportItem.Text, 1) & "'"
        End If
        SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"

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
            Else
                SqlStr = SqlStr & vbCrLf & " AND "
            End If

            SqlStr = SqlStr & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"
        End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM "

        If lblBookType.Text = "B" Then
            SqlStr = SqlStr & vbCrLf & " , ITEM.MINIMUM_QTY"
        ElseIf lblBookType.Text = "A" Then
            SqlStr = SqlStr & vbCrLf & " , ITEM.MAXIMUM_QTY"
        End If

        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE "


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

        Dim mRMCatCode As String = ""
        Dim mRMCatCodeStr As String = ""
        Dim CntLst As Integer
        Dim mMaterialType As String

        mFromDate = txtDateFrom.Text

        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        mTableName = ConInventoryTable

        mDeptFunction = "GETDEPTSTOCK"

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, "

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS') AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS') AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS Receipt, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','CS') AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS Issue, " & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * DECODE(STOCK_TYPE,'ST',1,DECODE(STOCK_TYPE,'CS',1,0)) * DECODE(INV.FYEAR," & RsCompany.Fields("FYEAR").Value & ",1,0) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END)) as Closing, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE='RJ' AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Rejection, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND (STOCK_TYPE='QC' OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS UnderQC, " & vbCrLf & " TO_CHAR(" & mDeptFunction & "(" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,'" & mToDate & "')) as DeptClosing, " & vbCrLf & " TO_CHAR(" & mDeptFunction & "(" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ",ITEM.ITEM_CODE,'" & mToDate & "')+SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * DECODE(INV.FYEAR," & RsCompany.Fields("FYEAR").Value & ",1,0))) as TotClosing, " & vbCrLf & " TO_CHAR(AVG(NVL(INV.PURCHASE_COST,0))) as Rate, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>=0 THEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))*AVG(NVL(INV.PURCHASE_COST,0)) ELSE 0 END) as Value," & vbCrLf & " '' AS TransDate, 0"

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " INV, " & vbCrLf & " INV_ITEM_MST ITEM "


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " WHERE "

        SqlStr = SqlStr & vbCrLf & " INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'"

        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        If cboShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        ElseIf cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        End If

        For CntLst = 0 To lstMaterialType.Items.Count - 1
            If lstMaterialType.GetItemChecked(CntLst) = True Then
                mMaterialType = VB6.GetItemString(lstMaterialType, CntLst)
                If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mRMCatCode = IIf(IsDbNull(MasterNo), "", MasterNo)
                End If
                mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
            End If
        Next

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN " & mRMCatCodeStr & ""
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

        If chkCS.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('CS','ST','RJ','QC') "
        Else
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('ST','RJ','QC') "
        End If

        ''AND DEPT_CODE_TO='STR'

        If Val(txtLocation.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_LOCATION ='" & txtLocation.Text & "'"
        End If

        If cboExportItem.SelectedIndex >= 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.IS_EXPORT_ITEM = '" & VB.Left(cboExportItem.Text, 1) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"

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

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM "


        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE "


        MakeSQL2 = SqlStr
        Exit Function
InsertErr:
        MakeSQL2 = ""
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Sub frmParamNonMovingStock_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

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

        FormLoaded = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub frmParamNonMovingStock_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        Dim mFromDate As String
        Dim RS As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim CntLst As Integer

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)
        MainClass.SetControlsColor(Me)

        txtDateTo.Text = CStr(RunDate)

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

        cboExportItem.Items.Clear()
        cboExportItem.Items.Add("All")
        cboExportItem.Items.Add("Yes")
        cboExportItem.Items.Add("No")
        cboExportItem.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("Active Only")
        cboShow.Items.Add("Inactive Only")
        cboShow.Items.Add("Both")
        cboShow.SelectedIndex = 0

        lstMaterialType.Items.Clear()
        SqlStr = "SELECT GEN_DESC FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C' ORDER BY GEN_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            Do While RS.EOF = False
                lstMaterialType.Items.Add(RS.Fields("GEN_DESC").Value)
                lstMaterialType.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstMaterialType.SelectedIndex = 0

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
    Private Sub frmParamNonMovingStock_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub PrintStatus(ByRef PrintFlag As Boolean)
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub

    Private Sub lstMaterialType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstMaterialType.SelectedIndexChanged
        PrintStatus(False)
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

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtDateFrom.Text = "" Then GoTo EventExitSub
        If IsDate(txtDateFrom.Text) = False Then
            MsgBox("Invalid Date")
            Cancel = True
            '    ElseIf FYChk(txtDateFrom.Text) = False Then
            '        Cancel = True
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
    Private Sub FillDatainGrid()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim CntRow As Integer
        Dim mItemCode As String
        Dim mLastTransactionDate As String

        'Temp commit
        'With SprdMain
        '    For CntRow = 1 To .MaxRows
        '        .Row = CntRow
        '        .Col = ColItemCode
        '        mItemCode = Trim(.Text)

        '        mLastTransactionDate = GetLastTRNDate(mItemCode)

        '        .Row = CntRow
        '        .Col = ColDateofTRN
        '        .Text = Trim(mLastTransactionDate)

        '    Next
        'End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function GetLastTRNDate(ByRef pItemCode As String) As Object

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTableName As String

        mTableName = ConInventoryTable

        GetLastTRNDate = ""
        SqlStr = "SELECT MAX(REF_DATE) AS REF_DATE " & vbCrLf & " FROM " & mTableName & "" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_TYPE='" & ConStockRefType_ISS & "' AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetLastTRNDate = IIf(IsDbNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
End Class
