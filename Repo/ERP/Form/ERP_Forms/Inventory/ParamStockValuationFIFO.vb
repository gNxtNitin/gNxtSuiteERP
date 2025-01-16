Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamStockValuationFIFO
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

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
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
            .Text = "Dept Closing"

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

            For I = ColOpening To ColValue
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeFloatDecimalPlaces = 2
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                If I = ColValue Then
                    .set_ColWidth(I, 11)
                Else
                    .set_ColWidth(I, 8)
                End If
            Next

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
        mTitle = "Inventory on Hand - as on " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

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
    Private Function InsertIntoTempTable() As Boolean

        On Error GoTo PrintDummyErr

        Dim SqlStr As String = ""
        Dim mSqlStr As String
        'Dim PvtDBCn As ADODB.Connection
        Dim CntRow As Integer

        Dim mUserId As String
        Dim mCATEGORY_CODE As String
        Dim mSUBCATEGORY_CODE As String
        Dim mITEM_CODE As String
        Dim mITEM_NAME As String
        Dim mITEM_UOM As String
        Dim mOpening As Double
        Dim mReceipt As Double
        Dim mIssue As Double
        Dim mClosing As Double
        Dim mREJECTION As Double
        Dim mUnderQC As Double
        Dim mDEPTCLOSING As Double
        Dim mTOT_CLOSING As Double
        Dim mRate As Double
        Dim mValue As Double


        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_STOCKONHAND NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        For CntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = CntRow

            SprdMain.Col = ColUserID
            mUserId = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColCatgeory
            mCATEGORY_CODE = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColSubCategory
            mSUBCATEGORY_CODE = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColItemCode
            mITEM_CODE = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColItemName
            mITEM_NAME = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColUnit
            mITEM_UOM = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColOpening
            mOpening = Val(SprdMain.Text)

            SprdMain.Col = ColReceipt
            mReceipt = Val(SprdMain.Text)

            SprdMain.Col = ColIssue
            mIssue = Val(SprdMain.Text)

            SprdMain.Col = ColClosing
            mClosing = Val(SprdMain.Text)

            SprdMain.Col = ColRejection
            mREJECTION = Val(SprdMain.Text)

            SprdMain.Col = ColUnderQC
            mUnderQC = Val(SprdMain.Text)

            SprdMain.Col = ColDeptStock
            mDEPTCLOSING = Val(SprdMain.Text)

            SprdMain.Col = ColTotalClosing
            mTOT_CLOSING = Val(SprdMain.Text)

            SprdMain.Col = ColRate
            mRate = Val(SprdMain.Text)

            SprdMain.Col = ColValue
            mValue = Val(SprdMain.Text)

            SqlStr = " INSERT INTO TEMP_STOCKONHAND (USERID, CATEGORY_CODE, " & vbCrLf & " SUBCATEGORY_CODE, ITEM_CODE, ITEM_NAME, ITEM_UOM, " & vbCrLf & " OPENING, RECEIPT, ISSUE, CLOSING, " & vbCrLf & " REJECTION, UNDERQC, TOT_CLOSING, RATE, " & vbCrLf & " VALUE, DEPT_STOCK) VALUES (" & vbCrLf & " '" & mUserId & "', '" & mCATEGORY_CODE & "', " & vbCrLf & " '" & mSUBCATEGORY_CODE & "', '" & mITEM_CODE & "', '" & mITEM_NAME & "', '" & mITEM_UOM & "', " & vbCrLf & " " & mOpening & ", " & mReceipt & ", " & mIssue & ", " & mClosing & ", " & vbCrLf & " " & mREJECTION & ", " & mUnderQC & ", " & mTOT_CLOSING & ", " & mRate & ", " & vbCrLf & " " & mValue & ", " & mDEPTCLOSING & ")"

            PubDBCn.Execute(SqlStr)
        Next
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


        'Dim SqlStr As String = ""
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
        Dim mCostType As String
        Dim mDeptStock As Double
        Dim mDivisionCode As Double
        Dim mStockType As String = ""
        FormatSprdMain(-1)


        If cboDivision.Items.Count = 0 Then
            mDivisionCode = -1
        Else
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        End If

        SqlStr = MakeSQL
        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsStock, ADODB.LockTypeEnum.adLockReadOnly)
        cnt = 1
        With SprdMain
            If RsStock.EOF = False Then
                Do While Not RsStock.EOF
                    .Row = cnt

                    .Col = ColUserID
                    .Text = IIf(IsDbNull(RsStock.Fields("UserID").Value), "", RsStock.Fields("UserID").Value)

                    .Col = ColCatgeory
                    .Text = IIf(IsDbNull(RsStock.Fields("CATEGORY_CODE").Value), "", RsStock.Fields("CATEGORY_CODE").Value)

                    .Col = ColSubCategory
                    .Text = IIf(IsDbNull(RsStock.Fields("SUBCATEGORY_CODE").Value), "", RsStock.Fields("SUBCATEGORY_CODE").Value)

                    .Col = ColItemCode
                    mItemCode = Trim(IIf(IsDbNull(RsStock.Fields("ITEM_CODE").Value), "", RsStock.Fields("ITEM_CODE").Value))
                    .Text = mItemCode

                    .Col = ColItemName
                    .Text = IIf(IsDbNull(RsStock.Fields("ITEM_SHORT_DESC").Value), "", RsStock.Fields("ITEM_SHORT_DESC").Value)

                    .Col = ColUnit
                    mUOM = Trim(IIf(IsDbNull(RsStock.Fields("ITEM_UOM").Value), "", RsStock.Fields("ITEM_UOM").Value))
                    .Text = mUOM

                    .Col = ColOpening
                    .Text = CStr(Val(IIf(IsDbNull(RsStock.Fields("OPENING").Value), 0, RsStock.Fields("OPENING").Value)))

                    .Col = ColReceipt
                    .Text = CStr(Val(IIf(IsDbNull(RsStock.Fields("Receipt").Value), 0, RsStock.Fields("Receipt").Value)))

                    .Col = ColIssue
                    .Text = CStr(Val(IIf(IsDbNull(RsStock.Fields("Issue").Value), 0, RsStock.Fields("Issue").Value)))

                    .Col = ColClosing
                    .Text = CStr(Val(IIf(IsDbNull(RsStock.Fields("Closing").Value), 0, RsStock.Fields("Closing").Value)))

                    .Col = ColRejection
                    .Text = CStr(Val(IIf(IsDbNull(RsStock.Fields("Rejection").Value), 0, RsStock.Fields("Rejection").Value)))

                    .Col = ColUnderQC
                    .Text = CStr(Val(IIf(IsDbNull(RsStock.Fields("UnderQC").Value), 0, RsStock.Fields("UnderQC").Value)))

                    .Col = ColDeptStock
                    If chkWIP.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mDeptStock = GetDeptStock(mItemCode, mUOM, mDivisionCode)
                    Else
                        mDeptStock = 0
                    End If
                    .Text = CStr(mDeptStock)

                    .Col = ColTotalClosing
                    mTotClosing = mDeptStock + Val(IIf(IsDbNull(RsStock.Fields("TotClosing").Value), 0, RsStock.Fields("TotClosing").Value))
                    .Text = CStr(mTotClosing)


                    If optShow(0).Checked = True Then
                        mCostType = "P"
                    ElseIf optShow(1).Checked = True Then
                        mCostType = "L"
                    ElseIf optShow(2).Checked = True Then
                        mCostType = "S"
                    Else
                        mCostType = "C"
                    End If

                    mStockType = GetStockType(PubDBCn, mItemCode, mDivisionCode)

                    '                If mStockType = "FG" Then
                    mTotValue = GetLatestItemCostFromMRR(mItemCode, mUOM, mTotClosing, (txtDateTo.Text), mCostType, mStockType)
                    '                Else
                    '                    mTotValue = GetLatestItemCostFromMRR(mItemCode, mUOM, mTotClosing, txtDateTo.Text, mCostType)
                    '                End If

                    .Row = cnt
                    .Col = ColValue
                    .Text = CStr(mTotValue)

                    .Col = ColRate
                    If mTotClosing > 0 Then
                        .Text = CStr(mTotValue / mTotClosing)
                    Else
                        .Text = "0.00"
                    End If

                    RsStock.MoveNext()
                    If RsStock.EOF = False Then
                        cnt = cnt + 1
                        .MaxRows = cnt
                    End If
                Loop
            End If
        End With
        Show1 = True
        Exit Function
InsertErr:
        Show1 = False
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Function GetDeptStock(ByRef mItemCode As String, ByRef xItemUOM As String, ByRef mDivisionCode As Double) As Double

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim RsSeq As ADODB.Recordset = Nothing
        Dim mProductCode As String = ""
        Dim mDeptCode As String
        Dim mProdDeptSeq As Integer
        Dim mDeptSeq As Integer
        Dim mSeqDeptCode As String = ""
        Dim mStdQty As Double
        Dim mProdQty As Double


        GetDeptStock = 0
        GetDeptStock = GetBalanceStockQty(mItemCode, (txtDateTo.Text), xItemUOM, Trim(mSeqDeptCode), "", "", ConPH, mDivisionCode)

        SqlStr = "SELECT DISTINCT PRODUCT_CODE " & vbCrLf & " FROM PRD_NEWBOM_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mProductCode = IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value)
                SqlStr = "SELECT IH.PRODUCT_CODE, ID.DEPT_CODE, (STD_QTY + GROSS_WT_SCRAP) STD_QTY " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) " & vbCrLf & " FROM PRD_NEWBOM_HDR" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOM, ADODB.LockTypeEnum.adLockReadOnly)

                If RsBOM.EOF = False Then
                    Do While RsBOM.EOF = False
                        mProductCode = IIf(IsDbNull(RsBOM.Fields("PRODUCT_CODE").Value), "", RsBOM.Fields("PRODUCT_CODE").Value)
                        mDeptCode = IIf(IsDbNull(RsBOM.Fields("DEPT_CODE").Value), "", RsBOM.Fields("DEPT_CODE").Value)
                        mStdQty = IIf(IsDbNull(RsBOM.Fields("STD_QTY").Value), 0, RsBOM.Fields("STD_QTY").Value)
                        If xItemUOM = "KGS" Then
                            mStdQty = mStdQty / 1000
                        ElseIf xItemUOM = "TON" Then
                            mStdQty = mStdQty / 1000
                            mStdQty = mStdQty / 1000
                        ElseIf xItemUOM = "MT" Then
                            mStdQty = mStdQty / 1000
                            mStdQty = mStdQty / 1000
                        End If
                        mProdDeptSeq = GetProductSeqNo(mProductCode, mDeptCode, (txtDateTo.Text))

                        SqlStr = " SELECT SERIAL_NO, DEPT_CODE " & vbCrLf & " FROM PRD_PRODSEQUENCE_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSeq, ADODB.LockTypeEnum.adLockReadOnly)
                        mProdQty = 0
                        If RsSeq.EOF = False Then
                            Do While RsSeq.EOF = False
                                mSeqDeptCode = IIf(IsDbNull(RsSeq.Fields("DEPT_CODE").Value), "", RsSeq.Fields("DEPT_CODE").Value)
                                mDeptSeq = IIf(IsDbNull(RsSeq.Fields("SERIAL_NO").Value), 0, RsSeq.Fields("SERIAL_NO").Value)

                                If mDeptSeq = mProdDeptSeq Then
                                    mProdQty = mProdQty + GetBalanceStockQty(mProductCode, (txtDateTo.Text), xItemUOM, Trim(mSeqDeptCode), "", "", ConPH, mDivisionCode)
                                    mProdQty = mProdQty - GetBalanceStockQty(mProductCode, (txtDateTo.Text), xItemUOM, Trim(mSeqDeptCode), "WP", "", ConPH, mDivisionCode)
                                ElseIf mDeptSeq > mProdDeptSeq Then
                                    mProdQty = mProdQty + GetBalanceStockQty(mProductCode, (txtDateTo.Text), xItemUOM, Trim(mSeqDeptCode), "", "", ConPH, mDivisionCode)
                                End If
                                RsSeq.MoveNext()
                            Loop
                        End If
                        GetDeptStock = GetDeptStock + (mProdQty * mStdQty)
                        RsBOM.MoveNext()
                    Loop
                End If
                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
InsertErr:
        GetDeptStock = 0
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
        Dim mStockType As String = ""
        Dim mRejStockType As String
        Dim mCheckStockType As String
        Dim mTableName As String
        Dim mDivision As Double

        mTableName = ConInventoryTable


        mMonthStartDate = "01/" & VB6.Format(txtDateTo.Text, "MM/YYYY")

        If optShow(2).Checked = True Then
            mStockType = "('FG')"
            mRejStockType = "'CR'"
            mCheckStockType = "('FG','CR')"
        Else
            If chkCS.CheckState = System.Windows.Forms.CheckState.Checked Then
                mRejStockType = "'RJ'"
                mStockType = "('ST','CS')"
                mCheckStockType = "('CS','ST','RJ','QC')"
            Else
                If chkFG.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mRejStockType = "('RJ','CR')"
                    mStockType = "('ST','CS','FG')"
                    mCheckStockType = "('ST','RJ','QC','FG','CR')"
                Else
                    mRejStockType = "'RJ'"
                    mStockType = "('ST','CS')"
                    mCheckStockType = "('ST','RJ','QC')"
                End If
            End If
        End If

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' AS USERID, " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, "

        ''ColOpening
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(mMonthStartDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND STOCK_TYPE IN " & mStockType & " " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening, "

        ''ColReceipt
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(E_DATE,'MON-YYYY')='" & UCase(VB6.Format(txtDateTo.Text, "MMM-YYYY")) & "'" & vbCrLf & " AND STOCK_TYPE IN " & mStockType & " AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,0) ELSE 0 END)) AS Receipt, "

        ''ColIssue
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(E_DATE,'MON-YYYY')='" & UCase(VB6.Format(txtDateTo.Text, "MMM-YYYY")) & "'" & vbCrLf & " AND STOCK_TYPE IN " & mStockType & " " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',0,1) ELSE 0 END)) AS Issue, "

        ''ColClosing
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * " & vbCrLf & " CASE WHEN STOCK_TYPE IN " & mStockType & " THEN 1 ELSE 0 END * " & vbCrLf & " CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as Closing, "

        '& " DECODE(STOCK_TYPE,'ST',1,DECODE(STOCK_TYPE,'CS',1,0))* " & vbCrLf _
        '
        ''ColRejection
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND STOCK_TYPE IN " & mRejStockType & " " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Rejection, "

        ''ColUnderQC
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND (STOCK_TYPE IN ('QC') OR E_DATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS UnderQC, "

        ''ColTotalClosing
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) as TotClosing, "

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(0) as Rate, " & vbCrLf & " TO_CHAR(0) as Value "

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

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND INV.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''            & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        ''            & " AND INV.STOCK_ID='" & ConWH & "'"

        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'"

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = Val(MasterNo)
                SqlStr = SqlStr & vbCrLf & "AND INV.DIV_CODE=" & mDivision & ""
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_TYPE IN " & mCheckStockType & ""

        ''AND DEPT_CODE_TO='STR'

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

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE, " & vbCrLf & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM "

        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & " ITEM.CATEGORY_CODE, ITEM.SUBCATEGORY_CODE, ITEM.ITEM_CODE "


        MakeSQL = SqlStr
        Exit Function
InsertErr:
        MakeSQL = ""
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmParamStockValuationFIFO_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

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

    Private Sub frmParamStockValuationFIFO_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
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

        FormatSprdMain(-1)
        FillSprdMain()
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub frmParamStockValuationFIFO_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        '    Frame4.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamStockValuationFIFO_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        'Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mClosing As String

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColItemCode
        mItemCode = Trim(SprdMain.Text)

        SprdMain.Col = ColUnit
        mItemUOM = Trim(SprdMain.Text)

        SprdMain.Col = ColItemName
        mItemDesc = Trim(SprdMain.Text)

        SprdMain.Col = ColTotalClosing
        mClosing = Trim(SprdMain.Text)

        If CDbl(mClosing) > 0 Then
            frmParamStockDetail.LblItemCode.Text = mItemCode
            frmParamStockDetail.txtItemName.Text = mItemCode & " - " & mItemDesc
            frmParamStockDetail.lblItemUom.Text = mItemUOM

            frmParamStockDetail.txtAsOn.Text = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
            frmParamStockDetail.txtClosing.Text = CStr(Val(mClosing))

            frmParamStockDetail.ShowDialog()
            frmParamStockDetail.frmParamStockDetail_Activated(Nothing, New System.EventArgs())
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
