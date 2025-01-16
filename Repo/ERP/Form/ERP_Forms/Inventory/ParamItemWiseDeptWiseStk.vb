Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmParamItemWiseDeptWiseStk
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
    Private Const ColRate As Short = 7
    'Private Const ColClosingSTR = 7
    'Private Const ColClosingPRS = 8
    'Private Const ColClosingMWS = 9
    'Private Const ColClosingPSW = 10
    'Private Const ColClosingSTS = 11
    'Private Const ColClosingPPS = 12
    'Private Const ColClosingPLT = 13
    'Private Const ColClosingASY = 14
    'Private Const ColClosingOTH = 15
    'Private Const ColRework = 16
    'Private Const ColScrap = 17

    Dim ColTotalClosing As Integer

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

    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub cboShow_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.SelectedIndexChanged
        PrintStatus(False)
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        txtCatName.Enabled = IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdsearchCategory.Enabled = IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub chkDept_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDept.CheckStateChanged
        PrintStatus(False)
        txtDept.Enabled = IIf(chkDept.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        CmdSearchDept.Enabled = IIf(chkDept.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub chkItemAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkItemAll.CheckStateChanged
        TxtItemName.Enabled = IIf(chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdItemDesc.Enabled = IIf(chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub chkOption_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOption.CheckStateChanged
        LblQty.Visible = IIf(chkOption.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        cboCond.Visible = IIf(chkOption.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        txtCondQty.Visible = IIf(chkOption.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
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
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        With SprdMain
            ColTotalClosing = ColRate + 1
            .MaxCols = ColTotalClosing

            .Row = 0

            '        .Col = 0
            '        .Text = "S.No."

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

            .Col = ColRate
            .Text = "Rate"

            If FormLoaded = True Then
                SqlStr = MakeDeptSQL
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    Do While RsTemp.EOF = False
                        .Row = 0
                        .Col = ColTotalClosing
                        .Text = Trim(IIf(IsDbNull(RsTemp.Fields("DEPT_CODE_TO").Value), "", RsTemp.Fields("DEPT_CODE_TO").Value)) & " QTY"

                        ColTotalClosing = ColTotalClosing + 1
                        .MaxCols = ColTotalClosing

                        '                    .Col = ColTotalClosing
                        '                    .Text = IIf(IsNull(RsTemp!DEPT_CODE_TO), "", RsTemp!DEPT_CODE_TO) & " Value"
                        '
                        '                    ColTotalClosing = ColTotalClosing + 1
                        '                    .MaxCols = ColTotalClosing
                        RsTemp.MoveNext()
                    Loop
                End If
            End If

            .Row = 0
            .Col = ColTotalClosing
            .Text = "Total Closing Qty"

            ColTotalClosing = ColTotalClosing + 1
            .MaxCols = ColTotalClosing

            .Col = ColTotalClosing
            .Text = "Total Closing Value"

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

        If ColTotalClosing = 0 Then
            ColTotalClosing = ColRate + 1
        End If
        With SprdMain
            .MaxCols = ColTotalClosing

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

            For I = ColRate To ColTotalClosing
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(I, 8)
            Next

            .Col = ColRate
            .ColHidden = True

            .Col = ColTotalClosing
            .ColHidden = True

            .ColsFrozen = ColItemName

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle

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

        If MainClass.SearchGridMaster((TxtItemName.Text), "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            TxtItemName.Text = AcName
            TxtItemName_Validating(TxtItemName, New System.ComponentModel.CancelEventArgs(False))
            TxtItemName.Focus()
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

        SqlStr = ""
        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, ColTotalClosing, PubDBCn) = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")


        mRPTName = "DeptStockSumm.rpt"
        mTitle = Me.Text

        mTitle = mTitle & " - as on " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

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

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtDept.Text), "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName
            txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
            txtDept.Focus()
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

        FillSprdMain()
        Show1()
        SprdMain.Refresh()
        FormatSprdMain(-1)
        '    FillSprdMain

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
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mRate As Double
        Dim cntCol As Integer
        Dim mDept As String
        Dim mItemCode As String
        Dim mTotalQty As Double
        Dim mTotalValue As Double
        Dim mGrandTotalValue As Double
        Dim mDeptBalanceQty As Double
        Dim mFromDate As String

        If PubUserID = "A00001" Then Exit Function

        SqlStr = MakeSQL
        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtDateFrom.Text, "DD/MM/YYYY"))))
        cntRow = 1
        If RsTemp.EOF = False Then
            With SprdMain
                Do While RsTemp.EOF = False
                    mTotalQty = 0
                    mTotalValue = 0
                    .Row = cntRow
                    .Col = ColCatgeory
                    .Text = IIf(IsDbNull(RsTemp.Fields("GEN_DESC").Value), "", RsTemp.Fields("GEN_DESC").Value)

                    .Col = ColSubCategory
                    .Text = IIf(IsDbNull(RsTemp.Fields("SUBCATEGORY_CODE").Value), "", RsTemp.Fields("SUBCATEGORY_CODE").Value)

                    .Col = ColItemCode
                    .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                    mItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                    .Col = ColItemName
                    .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                    .Col = ColUnit
                    .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)

                    .Col = ColRate

                    If CheckItemBom(mItemCode) = True Then
                        mRate = GetLatestWIPCost(mItemCode, IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value), 1, (txtDateTo.Text), "L", "ST", "STR")
                    Else
                        mRate = GetLatestItemCostFromMRR(mItemCode, IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value), 1, (txtDateTo.Text), "L", "ST", "STR")
                    End If

                    .Text = CStr(mRate)

                    Do While mItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                        mDept = IIf(IsDbNull(RsTemp.Fields("DEPT_CODE_TO").Value), "", RsTemp.Fields("DEPT_CODE_TO").Value) & " QTY"
                        For cntCol = ColRate + 1 To .MaxCols - 1
                            .Row = 0
                            .Col = cntCol
                            If Trim(mDept) = Trim(.Text) Then
                                .Row = cntRow
                                .Col = cntCol

                                '                            If chkDeptQty.Value = vbChecked Then
                                '                                mDeptBalanceQty = GetBalanceStockQty(mItemCode, VB6.Format(mFromDate, "DD/MM/YYYY"), IIf(IsNull(RsTemp!ITEM_UOM), "", RsTemp!ITEM_UOM), Trim(IIf(IsNull(RsTemp!DEPT_CODE_TO), "", RsTemp!DEPT_CODE_TO)), "ST", "", ConPH)
                                ''                                mDeptBalanceQty = mDeptBalanceQty - GetBalanceStockQty(mItemCode, VB6.Format(txtDateTo.Text, "DD/MM/YYYY"), IIf(IsNull(RsTemp!ITEM_UOM), "", RsTemp!ITEM_UOM), Trim(IIf(IsNull(RsTemp!DEPT_CODE_TO), "", RsTemp!DEPT_CODE_TO)), "ST", "", ConPH)
                                '                            Else
                                '                                mDeptBalanceQty = 0
                                '                            End If
                                '
                                .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("QTY").Value), "", RsTemp.Fields("QTY").Value)) + mDeptBalanceQty)
                                mTotalQty = mTotalQty + Val(IIf(IsDbNull(RsTemp.Fields("QTY").Value), "", RsTemp.Fields("QTY").Value)) ''+ mDeptBalanceQty)

                                '                            .Col = cntCol + 1
                                '                            .Text = Val(IIf(IsNull(RsTemp!QTY), "", RsTemp!QTY)) * mRate
                                mTotalValue = mTotalValue + ((Val(IIf(IsDbNull(RsTemp.Fields("QTY").Value), "", RsTemp.Fields("QTY").Value)) + mDeptBalanceQty) * mRate)

                            End If
                        Next
                        RsTemp.MoveNext()
                        If RsTemp.EOF = True Then GoTo NextRec
                    Loop
NextRec:
                    .Row = cntRow
                    .Col = .MaxCols - 1
                    .Text = CStr(Val(CStr(mTotalQty)))

                    .Col = .MaxCols
                    .Text = CStr(Val(CStr(mTotalValue)))
                    mGrandTotalValue = mGrandTotalValue + mTotalValue
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                Loop

                .Row = .MaxRows
                .Col = .MaxCols
                .Text = CStr(Val(CStr(mGrandTotalValue)))
            End With
        End If

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) AS QTY "

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


        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " CMST.GEN_DESC, " & vbCrLf & " ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, DECODE(STOCK_ID,'WH','STR',DEPT_CODE_FROM) AS DEPT_CODE_TO, STOCK_ID, "

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) AS QTY "

        SqlStr = SqlStr & vbCrLf & CondSQL

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " CMST.GEN_DESC, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, DECODE(STOCK_ID,'WH','STR',DEPT_CODE_FROM),STOCK_ID "


        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & " CMST.GEN_DESC,  ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE,STOCK_ID,DECODE(STOCK_ID,'WH','STR',DEPT_CODE_FROM) "


        MakeSQL = SqlStr
        Exit Function
InsertErr:
        MakeSQL = ""
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Function MakeDeptSQL() As String
        On Error GoTo InsertErr
        Dim SqlStr As String = ""

        SqlStr = " SELECT DISTINCT DECODE(STOCK_ID,'WH','STR',DEPT_CODE_FROM) AS DEPT_CODE_TO "


        SqlStr = SqlStr & vbCrLf & CondSQL

        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & " DECODE(STOCK_ID,'WH','STR',DEPT_CODE_FROM)"


        MakeDeptSQL = SqlStr
        Exit Function
InsertErr:
        MakeDeptSQL = ""
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function CondSQL() As String
        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim mDeptCode As String
        Dim mCategoryCode As String = ""
        Dim mSubCategoryCode As String
        Dim mCond As String
        Dim mItemCode As String
        Dim mHavingClause As Boolean
        Dim mTableName As String

        mHavingClause = False

        mTableName = ConInventoryTable

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " INV, " & vbCrLf & " INV_ITEM_MST ITEM, INV_GENERAL_MST CMST, PAY_DEPT_MST DEPT "

        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf & " AND ITEM.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND ITEM.CATEGORY_CODE=CMST.GEN_CODE "

        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=DEPT.COMPANY_CODE(+)" & vbCrLf & " AND INV.DEPT_CODE_TO=DEPT.DEPT_CODE(+) "

        SqlStr = SqlStr & vbCrLf & " AND CMST.GEN_TYPE='C' "

        If cboShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        ElseIf cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        End If

        '    If chkDept.Value = vbUnchecked Then
        '        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '            mDeptCode = MasterNo
        ''            SqlStr = SqlStr & vbCrLf & " AND INV.DEPT_CODE_FROM='" & mDeptCode & "'"
        '            SqlStr = SqlStr & vbCrLf & " AND (INV.DEPT_CODE_TO=CASE WHEN INV.STOCK_ID='" & ConPH & "' THEN '" & mDeptCode & "' ELSE INV.DEPT_CODE_TO END OR INV.DEPT_CODE_FROM=CASE WHEN INV.STOCK_ID='" & ConPH & "' THEN  '" & mDeptCode & "' ELSE INV.DEPT_CODE_FROM END)"
        '        End If
        '    End If
        '
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((txtCatName.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCategoryCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE='" & mCategoryCode & "'"
            End If
        End If

        If chkSubCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mCond = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mCond = mCond & " AND CATEGORY_CODE='" & mCategoryCode & "'"
            End If
            If MainClass.ValidateWithMasterTable((txtSubCatName.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , mCond) = True Then
                mSubCategoryCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND ITEM.SUBCATEGORY_CODE='" & mSubCategoryCode & "'"
            End If
        End If

        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND INV.STOCK_ID IN ('" & ConWH & "','" & ConPH & "','" & ConSH & "')"


        '    SqlStr = SqlStr & vbCrLf & " AND INV.REF_TYPE IN ('" & ConStockRefType_ISS & "', '" & ConStockRefType_SRN & "') "

        If CboSType.Text <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE = '" & Trim(CboSType.Text) & "'"
        End If

        If cboExportItem.SelectedIndex >= 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.IS_EXPORT_ITEM = '" & VB.Left(cboExportItem.Text, 1) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"

        '    SqlStr = SqlStr & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') "



        '    If chkOption.Value = vbChecked Then
        ''        SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        '
        '        SqlStr = SqlStr & vbCrLf & " HAVING "
        '
        '        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','FG','CS') " & vbCrLf _
        ''            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)"
        '
        '        SqlStr = SqlStr & vbCrLf & cboCond.Text & Val(txtCondQty.Text) & ""
        '
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
        '        If chkDept.Value = vbChecked Then
        '            SqlStr = SqlStr & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"
        '        Else
        '            SqlStr = SqlStr & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * DECODE(INV.STOCK_ID,'" & ConPH & "',1,0) * DECODE(DEPT.DEPT_DESC,'" & Trim(txtDept.Text) & "',1,0))<>0"
        '        End If
        '    End If
        '
        CondSQL = SqlStr
        Exit Function
InsertErr:
        CondSQL = ""
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function MakeSQLOld() As String

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim mDeptCode As String
        Dim mCategoryCode As String = ""
        Dim mSubCategoryCode As String
        Dim mCond As String
        Dim mItemCode As String
        Dim mHavingClause As Boolean
        Dim mTableName As String

        mHavingClause = False

        mTableName = ConInventoryTable


        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " CMST.GEN_DESC, " & vbCrLf & " ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, "


        ''Closing ''STORE  ''AND STOCK_TYPE IN ('ST','QC','CS','FG')

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS ClosingSTR, "

        ''Closing ''PRESS SHOP
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND STOCK_TYPE IN ('ST','FG','CS','WP') AND DEPT.DEPT_CODE='PRS' AND INV.STOCK_ID='" & ConPH & "'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS ClosingPRS, "

        ''Closing ''WELD SHOP
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')  " & vbCrLf & " AND STOCK_TYPE IN ('ST','FG','CS','WP') AND DEPT.DEPT_CODE='MWS' AND INV.STOCK_ID='" & ConPH & "'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS ClosingMWS, "

        ''Closing ''PSW SHOP
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')  " & vbCrLf & " AND STOCK_TYPE IN ('ST','FG','CS','WP') AND DEPT.DEPT_CODE='PSW' AND INV.STOCK_ID='" & ConPH & "'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS ClosingPSW, "

        ''Closing ''STS SHOP
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')  " & vbCrLf & " AND STOCK_TYPE IN ('ST','FG','CS','WP') AND DEPT.DEPT_CODE='STS' AND INV.STOCK_ID='" & ConPH & "'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS ClosingSTS, "

        ''Closing ''PPS SHOP
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')  " & vbCrLf & " AND STOCK_TYPE IN ('ST','FG','CS','WP') AND DEPT.DEPT_CODE='PPS' AND INV.STOCK_ID='" & ConPH & "'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS ClosingPPS, "

        ''Closing ''PLT SHOP
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')  " & vbCrLf & " AND STOCK_TYPE IN ('ST','FG','CS','WP') AND DEPT.DEPT_CODE='PLT' AND INV.STOCK_ID='" & ConPH & "'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS ClosingPLT, "

        ''Closing ''ASY SHOP
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')  " & vbCrLf & " AND STOCK_TYPE IN ('ST','FG','CS','WP') AND DEPT.DEPT_CODE='ASY' AND INV.STOCK_ID='" & ConPH & "'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS ClosingASY, "

        ''Closing ''OTHERS SHOP
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')  " & vbCrLf & " AND STOCK_TYPE IN ('ST','FG','CS','WP') AND DEPT.DEPT_CODE NOT IN ('PRS','MWS','STS','PPS','PSW','PLT','ASY') AND INV.STOCK_ID='" & ConPH & "'" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS ClosingPSW, "

        ''Rework
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('WR','RJ') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Rework, "

        ''Scrap
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('SC') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Scrap, "

        ''Closing
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as TotClosing " ''& vbCrLf |
        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " INV, " & vbCrLf & " INV_ITEM_MST ITEM, INV_GENERAL_MST CMST, PAY_DEPT_MST DEPT "


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf & " AND ITEM.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND ITEM.CATEGORY_CODE=CMST.GEN_CODE "

        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=DEPT.COMPANY_CODE(+)" & vbCrLf & " AND INV.DEPT_CODE_TO=DEPT.DEPT_CODE(+) "

        SqlStr = SqlStr & vbCrLf & " AND CMST.GEN_TYPE='C' "

        If cboShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        ElseIf cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        End If

        If chkDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                '            SqlStr = SqlStr & vbCrLf & " AND INV.DEPT_CODE_FROM='" & mDeptCode & "'"
                SqlStr = SqlStr & vbCrLf & " AND (INV.DEPT_CODE_TO=CASE WHEN INV.STOCK_ID='" & ConPH & "' THEN '" & mDeptCode & "' ELSE INV.DEPT_CODE_TO END OR INV.DEPT_CODE_FROM=CASE WHEN INV.STOCK_ID='" & ConPH & "' THEN  '" & mDeptCode & "' ELSE INV.DEPT_CODE_FROM END)"
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
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND INV.STOCK_ID IN ('" & ConPH & "','" & ConWH & "')"


        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN ('ST','WP','WR','SC','FG','CS','RJ') "

        If cboExportItem.SelectedIndex >= 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.IS_EXPORT_ITEM = '" & VB.Left(cboExportItem.Text, 1) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') "
        End If


        If chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            '        SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""

            SqlStr = SqlStr & vbCrLf & " HAVING "

            SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') AND STOCK_TYPE IN ('ST','FG','CS') " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)"

            SqlStr = SqlStr & vbCrLf & cboCond.Text & Val(txtCondQty.Text) & ""

            mHavingClause = True
        End If

        If chkZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            If mHavingClause = False Then
                SqlStr = SqlStr & vbCrLf & " HAVING "
            Else
                SqlStr = SqlStr & vbCrLf & " AND "
            End If

            If chkDept.CheckState = System.Windows.Forms.CheckState.Checked Then
                SqlStr = SqlStr & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"
            Else
                SqlStr = SqlStr & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * DECODE(INV.STOCK_ID,'" & ConPH & "',1,0) * DECODE(DEPT.DEPT_DESC,'" & Trim(txtDept.Text) & "',1,0))<>0"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " CMST.GEN_DESC, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM "


        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & " CMST.GEN_DESC,  ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE "


        MakeSQLOld = SqlStr
        Exit Function
InsertErr:
        MakeSQLOld = ""
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmParamItemWiseDeptWiseStk_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        Dim SqlStr As String = ""
        If FormLoaded = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Item Wise Department Wise Stock Report"
        chkOption.CheckState = System.Windows.Forms.CheckState.Unchecked
        LblQty.Visible = IIf(chkOption.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        cboCond.Visible = IIf(chkOption.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        txtCondQty.Visible = IIf(chkOption.CheckState = System.Windows.Forms.CheckState.Checked, True, False)

        FormLoaded = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub frmParamItemWiseDeptWiseStk_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        Dim mFromDate As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)
        MainClass.SetControlsColor(Me)

        txtDateFrom.Text = CStr(RunDate)
        txtDateTo.Text = CStr(RunDate)

        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCatName.Enabled = False
        cmdsearchCategory.Enabled = False

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

        cboExportItem.Items.Clear()
        cboExportItem.Items.Add("All")
        cboExportItem.Items.Add("Yes")
        cboExportItem.Items.Add("No")
        cboExportItem.SelectedIndex = 0

        Call MainClass.FillCombo(CboSType, "INV_TYPE_MST", "STOCK_TYPE_CODE", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        CboSType.SelectedIndex = 0

        FormatSprdMain(-1)
        FillSprdMain()
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub frmParamItemWiseDeptWiseStk_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamItemWiseDeptWiseStk_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub TxtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub


    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDept.Text) = "" Then
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            ErrorMsg("Invalid Dept Name.", , MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        Call cmdItemDesc_Click(cmdItemDesc, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdItemDesc_Click(cmdItemDesc, New System.EventArgs())
    End Sub

    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(TxtItemName.Text) = "" Then
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
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
