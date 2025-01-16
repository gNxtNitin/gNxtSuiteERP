Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamStorePhysicalReg
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean

    Dim GroupOnItem As Boolean

    Private Const RowHeight As Short = 12

    Private Const ColRefNo As Short = 1
    Private Const ColUserID As Short = 2
    Private Const ColCatgeory As Short = 3
    Private Const ColSubCategory As Short = 4
    Private Const ColItemCode As Short = 5
    Private Const ColItemName As Short = 6
    Private Const ColUnit As Short = 7
    Private Const ColStockType As Short = 8
    Private Const ColStoreQty As Short = 9
    Private Const ColPhysicalQty As Short = 10
    Private Const ColVariance As Short = 11
    Private Const ColRate As Short = 12
    Private Const ColValue As Short = 13
    Private Const ColDivision As Short = 14
    Private Const ColDept As Short = 15
    Private Const ColTagNo As Short = 16

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

    Private Sub cboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub cboDept_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.SelectedIndexChanged
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

    Private Sub CboWareHouse_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboWareHouse.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub CboWareHouse_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboWareHouse.SelectedIndexChanged
        PrintStatus(False)
    End Sub


    'Private Sub chkCategory_Click()
    '    txtCatName.Enabled = IIf(chkCategory.Value = vbChecked, False, True)
    '    cmdSearchCategory.Enabled = IIf(chkCategory.Value = vbChecked, False, True)
    '    PrintStatus False
    'End Sub

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

    Private Sub chkOption_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOption.CheckStateChanged
        FraOption.Visible = IIf(chkOption.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        PrintStatus(False)
    End Sub

    'Private Sub chkSubCategory_Click()
    '    txtSubCatName.Enabled = IIf(chkSubCategory.Value = vbChecked, False, True)
    '    cmdSearchSubCat.Enabled = IIf(chkSubCategory.Value = vbChecked, False, True)
    '    PrintStatus False
    'End Sub

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

            .Col = ColRefNo
            .Text = "Ref No"

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

            .Col = ColStockType
            .Text = "Stock Type"

            .Col = ColStoreQty
            .Text = "Actual Qty"

            .Col = ColPhysicalQty
            .Text = "Physical Qty"

            .Col = ColVariance
            .Text = "Difference"

            .Col = ColRate
            .Text = "Rate"

            .Col = ColValue
            .Text = "Value"

            .Col = ColDivision
            .Text = "Division"

            .Col = ColDept
            .Text = "Dept."

            .Col = ColTagNo
            .Text = "Tag No"
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

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColRefNo, 12)
            .ColHidden = IIf(optType(0).Checked = True, False, True)

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
            .set_ColWidth(ColItemName, 30)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColUnit, 4.5)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColStockType, 6)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColDept, 6)

            .Col = ColTagNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColTagNo, 6)

            .Col = ColDivision
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColDivision, 6)

            For I = ColStoreQty To ColValue
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
                    .set_ColWidth(I, 9)
                End If
            Next

            .ColsFrozen = ColStoreQty
            .Col = ColRate
            .TypeFloatDecimalPlaces = 4

            .Col = ColRate
            .ColHidden = IIf(chkRate.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

            .Col = ColValue
            .ColHidden = IIf(chkRate.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

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

        'Select Record for print...

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")


        mRPTName = "PhyInvReg.rpt"

        mTitle = "Physical Inventory Report"

        If Trim(cboDivision.Text) <> "ALL" Then
            mTitle = mTitle & "[" & cboDivision.Text & "]"
        End If

        mTitle = mTitle & " - From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " - To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        '    If chkCategory.Value = vbUnchecked Then
        '        mSubTitle = "(Category : " & txtCatName.Text & ")"
        '    End If
        '
        '    If chkSubCategory.Value = vbUnchecked Then
        '        mSubTitle = mSubTitle & " (Sub Category : " & txtSubCatName.Text & ")"
        '    End If

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
        'Dim PvtDBCn As ADODB.Connection

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_STOCKONHAND NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        ''Stock Insert
        mSqlStr = MakeSQL()

        SqlStr = " INSERT INTO TEMP_STOCKONHAND (USERID, CATEGORY_CODE, " & vbCrLf _
            & " SUBCATEGORY_CODE, ITEM_CODE, ITEM_NAME, ITEM_UOM, " & vbCrLf _
            & " STOCK_TYPE, OPENING, RATE," & vbCrLf & " VALUE,DEPT_CODE,DIV_CODE) " & vbCrLf _
            & mSqlStr

        PubDBCn.Execute(SqlStr)

        ''Physical Insert
        mSqlStr = MakeSQL2()

        SqlStr = " INSERT INTO TEMP_STOCKONHAND (USERID, CATEGORY_CODE, " & vbCrLf _
            & " SUBCATEGORY_CODE, ITEM_CODE, ITEM_NAME, ITEM_UOM, " & vbCrLf _
            & " STOCK_TYPE, RECEIPT, RATE," & vbCrLf _
            & " VALUE,DEPT_CODE,DIV_CODE) " & vbCrLf & mSqlStr

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
        'Resume
        InsertIntoTempTable = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
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

    Private Sub cmdSearchCategory_Click()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"
        '
        '    If MainClass.SearchGridMaster(txtCatName.Text, "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
        '        txtCatName.Text = AcName
        '        txtCatName_Validate False
        '        txtCatName.SetFocus
        '    End If
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

    Private Sub cmdSearchSubCat_Click()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""


        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        '
        '    If chkCategory.Value = vbUnchecked Then
        '        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
        '            mCategoryCode = MasterNo
        '            SqlStr = SqlStr & " AND CATEGORY_CODE='" & mCategoryCode & "'"
        '        End If
        '    End If
        '
        '    If MainClass.SearchGridMaster(txtSubCatName.Text, "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
        '        txtSubCatName.Text = AcName
        '        txtSubCatName_Validate False
        '        txtSubCatName.SetFocus
        '    End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim CntRow As Integer
        Dim mItemCode As String
        Dim mDeptCode As String
        Dim mStockType As String = ""
        Dim mRefNo As String
        Dim mTagNo As String

        PrintStatus(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdMain)

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Show1()
        '    SprdMain.Refresh

        If optType(0).Checked = True Then
            With SprdMain
                For CntRow = 1 To .MaxRows
                    .Row = CntRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    .Col = ColDept
                    mDeptCode = Trim(.Text)

                    .Col = ColStockType
                    mStockType = Trim(.Text)

                    mRefNo = GetRefNo(mItemCode, mDeptCode, mStockType)

                    .Col = ColRefNo
                    .Text = mRefNo

                    mTagNo = GetTagNo(mItemCode, mDeptCode, mStockType)

                    .Col = ColTagNo
                    .Text = mTagNo
                Next
            End With
        End If

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

        'If Not IsDate(txtDateFrom.Text) Then
        '    FieldsVarification = False
        '    MsgInformation("Invaild Date")
        '    txtDateFrom.Focus()
        '    Exit Function
        'ElseIf FYChk((txtDateFrom.Text)) = False Then
        '    FieldsVarification = False
        '    txtDateFrom.Focus()
        '    Exit Function
        'End If


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
        Dim mHavingClause As Boolean

        If InsertIntoTempTable = False Then GoTo InsertErr

        SqlStr = " SELECT '', USERID, CATMST.GEN_DESC, SUBCATMST.SUBCATEGORY_DESC, " & vbCrLf _
            & " STOCK.ITEM_CODE, STOCK.ITEM_NAME, STOCK.ITEM_UOM, STOCK.STOCK_TYPE," & vbCrLf _
            & " SUM(NVL(OPENING,0)) AS STOCK_QTY, SUM(NVL(RECEIPT,0)) AS PHY_QTY, " & vbCrLf _
            & " SUM(NVL(RECEIPT,0)-NVL(OPENING,0)) AS PHY_QTY, 0, 0, DMST.DIV_DESC, STOCK.DEPT_CODE,''" & vbCrLf _
            & " FROM TEMP_STOCKONHAND STOCK, " & vbCrLf _
            & " INV_GENERAL_MST CATMST, INV_SUBCATEGORY_MST SUBCATMST, INV_DIVISION_MST DMST "

        SqlStr = SqlStr & vbCrLf _
            & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf _
            & " AND CATMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND STOCK.CATEGORY_CODE=CATMST.GEN_CODE" & vbCrLf _
            & " AND SUBCATMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND STOCK.SUBCATEGORY_CODE=SUBCATMST.SUBCATEGORY_CODE" & vbCrLf _
            & " AND CATMST.COMPANY_CODE=SUBCATMST.COMPANY_CODE" & vbCrLf _
            & " AND CATMST.GEN_CODE=SUBCATMST.CATEGORY_CODE" & vbCrLf _
            & " AND CATMST.GEN_TYPE='C' AND DMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOCK.DIV_CODE=DMST.DIV_CODE"

        If chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "HAVING SUM(NVL(OPENING,0)-NVL(RECEIPT,0))" & cboCond.Text & Val(txtCondQty.Text) & ""
            mHavingClause = True
        End If

        If chkZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            If mHavingClause = False Then
                SqlStr = SqlStr & vbCrLf & " HAVING "
            Else
                SqlStr = SqlStr & vbCrLf & " AND "
            End If
            mHavingClause = True
            SqlStr = SqlStr & vbCrLf & " SUM(NVL(OPENING,0)-NVL(RECEIPT,0))<>0"
        End If


        If chkPhyItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            If mHavingClause = False Then
                SqlStr = SqlStr & vbCrLf & " HAVING "
            Else
                SqlStr = SqlStr & vbCrLf & " AND "
            End If

            SqlStr = SqlStr & vbCrLf & " SUM(NVL(RECEIPT,0))<>0"
        End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY USERID,CATMST.GEN_DESC, SUBCATMST.SUBCATEGORY_DESC, STOCK.ITEM_CODE, STOCK.ITEM_NAME, STOCK.ITEM_UOM, STOCK.STOCK_TYPE,DMST.DIV_DESC,STOCK.DEPT_CODE "
        SqlStr = SqlStr & vbCrLf & " ORDER BY DMST.DIV_DESC, STOCK.DEPT_CODE, CATMST.GEN_DESC, SUBCATMST.SUBCATEGORY_DESC, STOCK.ITEM_CODE, STOCK.STOCK_TYPE "


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Show1 = True
        Exit Function
InsertErr:
        Show1 = False
        MsgBox(Err.Description)
        '    Resume
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
        Dim mFromDate As String

        Dim mRMCatCode As String = ""
        Dim mRMCatCodeStr As String = ""
        Dim CntLst As Integer
        Dim mMaterialType As String
        Dim mDivision As Double

        mHavingClause = False
        'mFromDate = VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY")
        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        mTableName = ConInventoryTable

        '
        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf _
            & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf _
            & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, INV.STOCK_TYPE,"

        ''CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE


        SqlStr = SqlStr & vbCrLf _
            & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) as TotClosing, " & vbCrLf _
            & " TO_CHAR(AVG(NVL(INV.PURCHASE_COST,0))) as Rate, " & vbCrLf _
            & " TO_CHAR(CASE WHEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>=0 THEN SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))*AVG(NVL(INV.PURCHASE_COST,0)) ELSE 0 END) as Value, "

        ''GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.PARENT_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * DECODE(STOCK_TYPE,'CS',0,1)  * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END ))

        If CboWareHouse.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & "'STR',INV.DIV_CODE"
        Else
            If _optType_2.Checked = True Then
                SqlStr = SqlStr & vbCrLf & "'PRD',INV.DIV_CODE"
            Else
                SqlStr = SqlStr & vbCrLf & "DECODE(INV.DEPT_CODE_TO,NULL,'PAD',INV.DEPT_CODE_TO),INV.DIV_CODE"
            End If

        End If

        SqlStr = SqlStr & vbCrLf _
            & " FROM " & mTableName & " INV, " & vbCrLf _
            & " INV_ITEM_MST ITEM "


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf _
            & " WHERE INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


        If CboWareHouse.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'"
        ElseIf CboWareHouse.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConPH & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConSH & "'"
        End If

        If cboDept.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & vbCrLf & " AND ( INV.DEPT_CODE_TO='" & MasterNo & "' OR INV.DEPT_CODE_FROM='" & MasterNo & "')"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

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

        If CboSType.SelectedIndex > 0 Then
            If CboSType.Text = "QC" Then
                SqlStr = SqlStr & vbCrLf & " AND (INV.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "' OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            ElseIf CboSType.Text = "ST" Or CboSType.Text = "RJ" Then
                SqlStr = SqlStr & vbCrLf & " AND (INV.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "' AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            Else
                SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "'"
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

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INV.DIV_CODE=" & mDivision & ""
            End If
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

        If chkAfterUpdate.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE<> CASE WHEN REF_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN '" & ConStockRefType_ADJ & "' ELSE 'X' END"
        End If

        SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If


        '    If chkOption.Value = vbChecked Then
        '        SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        '        mHavingClause = True
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

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM,INV.STOCK_TYPE, "

        If CboWareHouse.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & "'STR',INV.DIV_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "DECODE(INV.DEPT_CODE_TO,NULL,'PAD',INV.DEPT_CODE_TO),INV.DIV_CODE"
        End If

        SqlStr = SqlStr & vbCrLf & "ORDER BY INV.DIV_CODE," & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE,INV.STOCK_TYPE "


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
        Dim mHavingClause As Boolean
        Dim mToDate As String
        Dim mRMCatCode As String = ""
        Dim mRMCatCodeStr As String = ""
        Dim CntLst As Integer
        Dim mMaterialType As String
        Dim mDivision As Double

        mHavingClause = False
        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, ID.ITEM_UOM, ID.STOCK_TYPE,"

        SqlStr = SqlStr & vbCrLf & " SUM(PHY_QTY * DECODE(ITEM_IO,'I',1,-1)) as TotClosing, " & vbCrLf & " '0' as Rate, " & vbCrLf & " '0' as Value, "

        If CboWareHouse.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & "'STR',IH.DIV_CODE"
        Else
            If _optType_2.Checked = True Then
                SqlStr = SqlStr & vbCrLf & "'PRD',IH.DIV_CODE"
            Else
                SqlStr = SqlStr & vbCrLf & "IH.DEPT_CODE,IH.DIV_CODE"
            End If

        End If

        SqlStr = SqlStr & vbCrLf & " FROM INV_PHY_HDR IH, INV_PHY_DET ID, " & vbCrLf & " INV_ITEM_MST ITEM "


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_PHY=ID.AUTO_KEY_PHY"

        SqlStr = SqlStr & vbCrLf & " AND ID.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=ITEM.ITEM_CODE "

        If CboWareHouse.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & ConWH & "'" 'IH.DEPT_CODE='STR'"
        ElseIf CboWareHouse.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & ConPH & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & ConSH & "'"
        End If

        If cboDept.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE='" & MasterNo & "'"
            End If
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

        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & mItemCode & "'"
            End If
        End If

        If chkModel.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_MODEL='" & MainClass.AllowSingleQuote(txtModel.Text) & "'"
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        ''AND DEPT_CODE_TO='STR'
        If cboShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        ElseIf cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        End If

        If CboSType.SelectedIndex > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ID.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "'"
        End If

        If Val(txtLocation.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_LOCATION ='" & Trim(txtLocation.Text) & "'"
        End If

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND IH.PHY_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        '    If chkOption.Value = vbChecked Then
        '        SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        '        mHavingClause = True
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

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, ID.ITEM_UOM,ID.STOCK_TYPE, "

        If CboWareHouse.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & "'STR',IH.DIV_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "IH.DEPT_CODE,IH.DIV_CODE"
        End If

        SqlStr = SqlStr & vbCrLf & "ORDER BY IH.DIV_CODE," & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE,ID.STOCK_TYPE "


        MakeSQL2 = SqlStr
        Exit Function
InsertErr:
        MakeSQL2 = ""
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Function GetRefNo(ByRef pItemCode As String, ByRef pDeptCode As String, ByRef pStockType As String) As String

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetRefNo = ""
        SqlStr = " SELECT DISTINCT IH.AUTO_KEY_PHY " & vbCrLf & " FROM INV_PHY_HDR IH, INV_PHY_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_PHY=ID.AUTO_KEY_PHY"

        If CboWareHouse.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & ConWH & "'" 'IH.DEPT_CODE='STR'"
        ElseIf CboWareHouse.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & ConPH & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & ConSH & "'"
        End If

        If cboDept.SelectedIndex > 0 Then
            If Trim(pDeptCode) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE='" & pDeptCode & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "'"

        SqlStr = SqlStr & vbCrLf & " AND ID.STOCK_TYPE='" & pStockType & "'"

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND IH.PHY_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False

                If GetRefNo = "" Then
                    GetRefNo = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_PHY").Value), "", RsTemp.Fields("AUTO_KEY_PHY").Value)
                Else
                    GetRefNo = GetRefNo & ", " & IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_PHY").Value), "", RsTemp.Fields("AUTO_KEY_PHY").Value)
                End If
                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
InsertErr:
        GetRefNo = ""
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Function GetTagNo(ByRef pItemCode As String, ByRef pDeptCode As String, ByRef pStockType As String) As String

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetTagNo = ""
        SqlStr = " SELECT DISTINCT ID.TAG_NO " & vbCrLf & " FROM INV_PHY_HDR IH, INV_PHY_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_PHY=ID.AUTO_KEY_PHY"

        If CboWareHouse.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & ConWH & "'" 'IH.DEPT_CODE='STR'"
        ElseIf CboWareHouse.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & ConPH & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & ConSH & "'"
        End If

        If cboDept.SelectedIndex > 0 Then
            If Trim(pDeptCode) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE='" & pDeptCode & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "'"

        SqlStr = SqlStr & vbCrLf & " AND ID.STOCK_TYPE='" & pStockType & "'"

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND IH.PHY_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False

                If GetTagNo = "" Then
                    GetTagNo = IIf(IsDbNull(RsTemp.Fields("TAG_NO").Value), "", RsTemp.Fields("TAG_NO").Value)
                Else
                    GetTagNo = GetTagNo & ", " & IIf(IsDbNull(RsTemp.Fields("TAG_NO").Value), "", RsTemp.Fields("TAG_NO").Value)
                End If
                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
InsertErr:
        GetTagNo = ""
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmParamStorePhysicalReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        Dim SqlStr As String = ""
        If FormLoaded = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Physical Inventory Report"

        FormLoaded = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub frmParamStorePhysicalReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        Dim mFromDate As String

        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)
        MainClass.SetControlsColor(Me)

        txtDateTo.Text = CStr(RunDate)

        '    chkCategory.Value = vbChecked
        '    txtCatName.Enabled = False
        '    cmdSearchCategory.Enabled = False
        '
        '    chkSubCategory.Value = vbChecked
        '    txtSubCatName.Enabled = False
        '    cmdSearchSubCat.Enabled = False

        chkAfterUpdate.CheckState = System.Windows.Forms.CheckState.Checked

        chkModel.CheckState = System.Windows.Forms.CheckState.Checked
        txtModel.Enabled = False
        cmdSearchModel.Enabled = False

        txtCondQty.Text = CStr(0)

        CboSType.Items.Clear()
        Call MainClass.FillCombo(CboSType, "INV_TYPE_MST", "STOCK_TYPE_CODE", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")

        CboSType.SelectedIndex = 0

        cboCond.Items.Clear()
        cboCond.Items.Add("=")
        cboCond.Items.Add("<=")
        cboCond.Items.Add(">=")
        cboCond.Items.Add("<")
        cboCond.Items.Add(">")
        cboCond.Items.Add("<>")
        cboCond.SelectedIndex = 0

        CboWareHouse.Items.Clear()
        CboWareHouse.Items.Add("Store")
        CboWareHouse.Items.Add("Production")
        CboWareHouse.Items.Add("Sub-Store")
        CboWareHouse.SelectedIndex = 0

        cboDept.Items.Clear()
        Call MainClass.FillCombo(cboDept, "PAY_DEPT_MST", "DEPT_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        cboDept.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("Active Only")
        cboShow.Items.Add("Inactive Only")
        cboShow.Items.Add("Both")
        cboShow.SelectedIndex = 2

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

        mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -3, CDate(txtDateTo.Text)))
        txtDateFrom.Text = IIf(CDate(mFromDate) < CDate(RsCompany.Fields("Start_Date").Value), RsCompany.Fields("Start_Date").Value, mFromDate)


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

        FormatSprdMain(-1)
        FillSprdMain()
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub frmParamStorePhysicalReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        'Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamStorePhysicalReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    'Private Sub txtCatName_Change()
    '    PrintStatus False
    'End Sub
    'Private Sub txtCatName_DblClick()
    '    Call cmdSearchCategory_Click
    'End Sub

    'Private Sub txtCatName_KeyPress(KeyAscii As Integer)
    '    KeyAscii = MainClass.UpperCase(KeyAscii, txtCatName.Text)
    'End Sub

    'Private Sub txtCatName_KeyUp(KeyCode As Integer, Shift As Integer)
    '    If KeyCode = vbKeyF1 Then cmdSearchCategory_Click
    'End Sub

    'Private Sub txtCatName_Validate(Cancel As Boolean)
    '    If Trim(txtCatName.Text) = "" Then: Exit Sub
    '
    '    If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = False Then
    '        ErrorMsg "Invalid Category Code.", , vbInformation
    '        Cancel = True
    '    End If
    '
    'End Sub
    '

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
        Dim CntRow As Integer
        Dim mItemCode As String
        Dim mRate As Double
        Dim mDiffAmount As Double
        Dim mDiffQty As Double
        Dim mUOM As String = ""
        Dim xClosing As Double
        Dim mCostType As String
        Dim mStockType As String = ""
        Dim mDept As String
        Dim mTotValue As Double

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColUnit
                mUOM = (.Text)

                If chkRate.CheckState = System.Windows.Forms.CheckState.Checked Then
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    '                mRate = GetLatestItemCostFromMRR(mItemCode, mUOM, 1, VB6.Format(txtDateTo.Text, "DD/MM/YYYY"), "P")

                    .Col = ColStockType
                    mStockType = Trim(.Text)

                    .Col = ColDept
                    mDept = Trim(.Text)

                    .Col = ColPhysicalQty
                    xClosing = Val(.Text)
                    xClosing = IIf(xClosing = 0, 1, xClosing)

                    mCostType = IIf(optVal(0).Checked = True, "L", IIf(optVal(1).Checked = True, "P", IIf(optVal(3).Checked = True, "C", "S")))
                    '                If CboWareHouse.ListIndex = 1 And cboDept.Text <> "ALL" Then

                    ''22-05-2012
                    '                If CheckItemBom(mItemCode) = True Then ''GoTo NextVal
                    '                    mRate = GetLatestWIPCost(mItemCode, mUOM, Abs(xClosing), VB6.Format(txtDateTo.Text, "DD/MM/YYYY"), mCostType, mStockType, Trim(mDept))
                    '                Else
                    '                    mRate = GetLatestItemCostFromMRR(mItemCode, mUOM, Abs(xClosing), VB6.Format(txtDateTo.Text, "DD/MM/YYYY"), mCostType, mStockType)
                    '                End If


                    mTotValue = GetLatestItemCostFromMRR(mItemCode, mUOM, xClosing, (txtDateTo.Text), mCostType, mStockType, IIf(CboWareHouse.SelectedIndex = 0, "", mDept))
                    If xClosing > 0 Then
                        mRate = mTotValue / xClosing
                    Else
                        mRate = CDbl("0.00")
                    End If

                    .Col = ColRate
                    .Text = VB6.Format(mRate, "0.0000")

                    .Col = ColVariance
                    mDiffQty = CDbl(VB6.Format(Val(.Text), "0.0000"))

                    mDiffAmount = CDbl(VB6.Format(mRate * mDiffQty, "0.0000"))

                    .Col = ColValue
                    .Text = VB6.Format(mDiffAmount, "0.0000")
                Else
                    .Col = ColRate
                    .Text = "0.00"

                    .Col = ColValue
                    .Text = "0.00"
                End If
            Next
        End With
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
