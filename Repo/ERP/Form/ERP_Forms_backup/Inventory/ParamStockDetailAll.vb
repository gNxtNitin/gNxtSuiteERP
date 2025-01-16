Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamStockDetailAll
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean

    Private Const RowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUom As Short = 3
    Private Const colBalStock As Short = 4
    Private Const ColMRRNo As Short = 5
    Private Const ColMRRDate As Short = 6
    Private Const ColBillNo As Short = 7
    Private Const ColBillDate As Short = 8
    Private Const ColVNO As Short = 9
    Private Const ColVDate As Short = 10
    Private Const colSupplier As Short = 11

    Private Const ColQty As Short = 12
    Private Const ColPurchaseCost As Short = 13
    Private Const ColED As Short = 14
    Private Const ColCess As Short = 15
    Private Const ColST As Short = 16
    Private Const ColLandedCost As Short = 17
    Private Const ColAmount As Short = 18

    Private Const ColStoreStock As Short = 19
    Private Const ColProductionStock As Short = 20
    Private Const ColWIPStock As Short = 21
    Private Const ColFGStock As Short = 22
    Private Const ColOthers As Short = 23

    Private Const ColCategory As Short = 24



    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemName.Enabled = False
            cmdsearch.Enabled = False
        Else
            txtItemName.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub chkFGStock_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFGStock.CheckStateChanged
        PrintStatus(False)
    End Sub

    Private Sub chkOthersStock_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOthersStock.CheckStateChanged
        PrintStatus(False)
    End Sub


    Private Sub chkProductionStock_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkProductionStock.CheckStateChanged
        PrintStatus(False)
    End Sub

    Private Sub chkRateRequired_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRateRequired.CheckStateChanged
        PrintStatus(False)
    End Sub

    Private Sub chkViewAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkViewAll.CheckStateChanged
        PrintStatus(False)
    End Sub

    Private Sub chkWIPStock_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkWIPStock.CheckStateChanged
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

            .Col = ColItemCode
            .Text = "Item Code"

            .Col = ColItemDesc
            .Text = "Item Description"

            .Col = ColUom
            .Text = "UOM"

            .Col = colBalStock
            .Text = "Bal Stock"

            .Col = ColMRRNo
            .Text = "MRR No"

            .Col = ColMRRDate
            .Text = "MRR Date"

            .Col = ColBillNo
            .Text = "Bill No"

            .Col = ColBillDate
            .Text = "Bill Date"


            .Col = ColVNO
            .Text = "VNo"

            .Col = ColVDate
            .Text = "VDate"

            .Col = colSupplier
            .Text = "Supplier"

            .Col = ColQty
            .Text = "Qty"

            .Col = ColPurchaseCost
            .Text = "Purchase Cost"

            .Col = ColED
            .Text = "ED"

            .Col = ColCess
            .Text = "Cess"

            .Col = ColST
            .Text = "ST"

            .Col = ColLandedCost
            .Text = "Landed Cost"

            .Col = ColAmount
            .Text = "Amount"

            .Col = ColStoreStock
            .Text = "Store Stock"

            .Col = ColProductionStock
            .Text = "Production Stock"

            .Col = ColWIPStock
            .Text = "WIP Stock"

            .Col = ColFGStock
            .Text = "FG Stock"

            .Col = ColOthers
            .Text = "Others Qty"

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

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColItemDesc, 10)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColUom, 5)

            .Col = colBalStock
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(colBalStock, 12)
            .ColHidden = True

            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColMRRNo, 10)

            .Col = ColMRRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColMRRDate, 10)
            .ColHidden = True

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColBillNo, 7)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColBillDate, 12)
            .ColHidden = True


            .Col = ColVNO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColVNO, 7)

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColVDate, 12)
            .ColHidden = True

            .Col = colSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(colSupplier, 15)

            For I = ColQty To ColOthers
                .Col = I
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .set_RowHeight(Arow, RowHeight)
                .set_ColWidth(I, 8)
                '            If .Col = ColED Or .Col = ColCess Or .Col = ColST Then
                '                .ColHidden = True
                '            End If
            Next

            .ColsFrozen = ColVNO

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


        mRPTName = "StockDetail.rpt"
        mTitle = "Detail of Inventory - as on " & VB6.Format(txtAsOn.Text, "DD/MM/YYYY")
        '    mSubTitle = txtItemName.Text
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
        Dim mMRRDATE As String
        Dim mVNo As String
        Dim mVDate As String = ""
        Dim mQty As String
        Dim mPurchaseCost As String
        Dim mED As String
        Dim mCess As String
        Dim mST As String
        Dim mLandedCost As String
        Dim mAmount As String
        Dim mPartyName As String

        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""
        Dim mBalStock As String

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        For CntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = CntRow


            SprdMain.Col = ColItemCode
            mItemCode = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColItemDesc
            mItemDesc = MainClass.AllowSingleQuote(SprdMain.Text)
            If Trim(mItemCode) <> "" Then
                mItemDesc = mItemCode & " - " & mItemDesc
            End If
            SprdMain.Col = ColUom
            mUOM = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = colBalStock
            mBalStock = VB6.Format(SprdMain.Text, "0.000")

            SprdMain.Col = ColMRRDate
            mMRRDATE = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColVNO
            mVNo = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColVDate
            mVDate = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColQty
            mQty = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColPurchaseCost
            mPurchaseCost = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColED
            mED = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColCess
            mCess = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColST
            mST = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColLandedCost
            mLandedCost = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColAmount
            mAmount = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = colSupplier
            mPartyName = MainClass.AllowSingleQuote(SprdMain.Text)

            If Trim(mItemDesc) <> "TOTAL :" Then
                SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " FIELD1, FIELD2, " & vbCrLf & " FIELD3, FIELD4, " & vbCrLf & " FIELD5, FIELD6, " & vbCrLf & " FIELD7, FIELD8, " & vbCrLf & " FIELD9, FIELD10, " & vbCrLf & " FIELD11, FIELD12, " & vbCrLf & " FIELD13, FIELD14, FIELD15 " & vbCrLf & " ) VALUES (" & vbCrLf & " '" & PubUserID & "', " & CntRow & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemDesc) & "', '" & txtAsOn.Text & "'," & vbCrLf & " '" & mUOM & "', '" & mBalStock & "', " & vbCrLf & " '" & mMRRDATE & "', '" & mVNo & "', " & vbCrLf & " '" & mVDate & "', '" & mQty & "', " & vbCrLf & " '" & mPurchaseCost & "', '" & mED & "', " & vbCrLf & " '" & mCess & "', '" & mST & "', " & vbCrLf & " '" & mLandedCost & "', '" & mAmount & "','" & MainClass.AllowSingleQuote(mPartyName) & "')"

                PubDBCn.Execute(SqlStr)
            End If
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
        '    Resume
        InsertIntoTempTable = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String


        mSqlStr = " SELECT * " & vbCrLf & " FROM TEMP_PRINTDUMMYDATA PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW "

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
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mCategoryCode As String = ""
        Dim mCond As String

        If Not IsDate(txtAsOn.Text) Then
            FieldsVarification = False
            MsgInformation("Invaild Date")
            txtAsOn.Focus()
            Exit Function
        ElseIf FYChk((txtAsOn.Text)) = False Then
            FieldsVarification = False
            txtAsOn.Focus()
            Exit Function
        End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtCategory.Text) = "" Then
                MsgInformation("Please Select Catgeory Name.")
                FieldsVarification = False
                txtCategory.Focus()
            Else
                If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mCategoryCode = MasterNo
                Else
                    MsgInformation("Invalid Catgeory Name.")
                    FieldsVarification = False
                    txtCategory.Focus()
                End If

            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtSubCategory.Text) = "" Then
                MsgInformation("Please Select Sub-Catgeory Name.")
                FieldsVarification = False
                txtSubCategory.Focus()
            Else

                mCond = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    mCond = mCond & " AND CATEGORY_CODE='" & mCategoryCode & "'"
                End If
                If MainClass.ValidateWithMasterTable((txtSubCategory.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , mCond) = False Then
                    MsgInformation("Invalid Sub-Catgeory Name.")
                    FieldsVarification = False
                    txtSubCategory.Focus()
                End If
            End If
        End If

        FieldsVarification = True

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function Show1() As Boolean

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim CntRow As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mBalStock As Double
        Dim mCatDesc As String = ""
        Dim xCategoryCode As String

        Dim mPHBalStock As Double
        Dim mWIPBalStock As Double
        Dim mFGBalStock As Double
        Dim mOtherBalStock As Double
        Dim mStoreBal As Double

        SqlStr = " SELECT " & vbCrLf & " ITEM_CODE, ITEM_SHORT_DESC," & vbCrLf & " ISSUE_UOM, PURCHASE_UOM,UOM_FACTOR,CATEGORY_CODE  " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" '' AND ITEM_STATUS='A'"


        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(txtItemName.Text) & "'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND CATEGORY_CODE IN ('001','002','003','005','009','010','013','016','029','033','035','036','037','038','041','042','043','045','047')"
        SqlStr = SqlStr & vbCrLf & " ORDER BY ITEM_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CntRow = 1

            Do While RsTemp.EOF = False
                mItemCode = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                mItemDesc = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
                mIssueUOM = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mPurchaseUOM = IIf(IsDbNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                mFactor = IIf(IsDbNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)

                mPHBalStock = 0
                mFGBalStock = 0
                mOtherBalStock = 0
                mWIPBalStock = 0

                mStoreBal = GetItemSTStock(mItemCode, (txtAsOn.Text), ConWH)
                If chkProductionStock.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mPHBalStock = GetItemSTStock(mItemCode, (txtAsOn.Text), ConPH)
                End If

                If chkWIPStock.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mWIPBalStock = GetWIPQty(mItemCode, mIssueUOM, VB6.Format(txtAsOn.Text, "DD/MM/YYYY"), mFGBalStock, mOtherBalStock)
                End If

                mBalStock = mStoreBal + mPHBalStock + mWIPBalStock + mFGBalStock + mOtherBalStock

                xCategoryCode = IIf(IsDbNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value)

                If MainClass.ValidateWithMasterTable(xCategoryCode, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mCatDesc = MasterNo
                End If

                If mBalStock > 0 Then
                    If FillDatainGrid(mItemCode, mItemDesc, mIssueUOM, mIssueUOM, mPurchaseUOM, mFactor, mBalStock, CntRow, mCatDesc, mStoreBal, mPHBalStock, mWIPBalStock, mFGBalStock, mOtherBalStock) = False Then GoTo InsertErr
                End If

                RsTemp.MoveNext()
                If RsTemp.EOF = False And mBalStock > 0 Then
                    '                If cntRow = 78 Then MsgBox "Ok"
                    CntRow = CntRow + 1
                End If
            Loop
        End If
        Show1 = True
        Exit Function
InsertErr:
        Show1 = False
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function GetWIPQty(ByRef xItemCode As String, ByRef xItemUOM As String, ByRef pDate As String, ByRef mFGBalStock As Double, ByRef mOtherBalStock As Double) As Double

        ''GetDespatchQty(pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemLevelStdQty() As Double
        Dim mSqlStrRel As String
        Dim RsRel As ADODB.Recordset = Nothing
        Dim xProductRelCode As String
        Dim mStdQty As Double
        Dim mLevel As Integer
        Dim mParentcode As String
        Dim mDeptCode As String
        Dim pItemUOM As String = ""
        Dim mCLWIPQty As Double
        Dim lFGBalStock As Double
        Dim lOtherBalStock As Double

        GetWIPQty = 0
        lFGBalStock = 0
        lOtherBalStock = 0


        SqlStr = " SELECT  " & vbCrLf & " LEVEL,TRN.PRODUCT_CODE, TRN.STD_QTY+  GROSS_WT_SCRAP AS STD_QTY, DEPT_CODE" & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " START WITH  TRIM(RM_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE) || ' '=TRIM(RM_CODE) || COMPANY_CODE || ' '"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mStdQty = 1
        ReDim mItemLevelStdQty(1000)
        '    mIsFirstRecord = True
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF

                mLevel = Val(IIf(IsDbNull(RsTemp.Fields("Level").Value), 1, RsTemp.Fields("Level").Value))

                If mLevel = 1 Then
                    mStdQty = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), "", RsTemp.Fields("STD_QTY").Value), "0.0000"))
                Else
                    mStdQty = mItemLevelStdQty(mLevel - 1) * CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), "", RsTemp.Fields("STD_QTY").Value), "0.0000"))
                End If
                mItemLevelStdQty(mLevel) = mStdQty


                mParentcode = Trim(IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                mDeptCode = Trim(IIf(IsDbNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value))

                If MainClass.ValidateWithMasterTable(mParentcode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    pItemUOM = Trim(MasterNo)
                End If
                mCLWIPQty = GetWIPStockQty(mParentcode, pItemUOM, mDeptCode, "CL", pDate, lFGBalStock, lOtherBalStock)
                GetWIPQty = GetWIPQty + (mCLWIPQty * mStdQty)
                mFGBalStock = mFGBalStock + (lFGBalStock * mStdQty)
                mOtherBalStock = mOtherBalStock + (lOtherBalStock * mStdQty)

                mSqlStrRel = GetRelationItem(mParentcode)
                If mSqlStrRel <> "" Then
                    MainClass.UOpenRecordSet(mSqlStrRel, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRel, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsRel.EOF = False Then
                        Do While RsRel.EOF = False
                            xProductRelCode = Trim(IIf(IsDbNull(RsRel.Fields("REF_ITEM_CODE").Value), "", RsRel.Fields("REF_ITEM_CODE").Value))
                            mCLWIPQty = GetWIPStockQty(xProductRelCode, pItemUOM, mDeptCode, "CL", pDate, lFGBalStock, lOtherBalStock)


                            GetWIPQty = GetWIPQty + (mCLWIPQty * mStdQty)
                            mFGBalStock = mFGBalStock + (lFGBalStock * mStdQty)
                            mOtherBalStock = mOtherBalStock + (lOtherBalStock * mStdQty)

                            RsRel.MoveNext()
                        Loop
                    End If
                End If
                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
ErrPart:

    End Function

    Private Function GetWIPStockQty(ByRef mProductCode As String, ByRef mItemUOM As String, ByRef mDeptCode As String, ByRef mFieldName As String, ByRef pDate As String, ByRef lFGBalStock As Double, ByRef lOtherBalStock As Double) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDeptSeq As Integer
        Dim mMaxDepSeq As Integer
        Dim pDeptCode As String
        Dim I As Integer
        Dim xStoreQty As Double
        Dim xFGQty As Double
        Dim xOthersQty As Double

        GetWIPStockQty = 0
        mDeptSeq = GetProductSeqNo(mProductCode, mDeptCode, pDate)
        mMaxDepSeq = GetMaxProductSeqNo(mProductCode, pDate)
        lFGBalStock = 0
        lOtherBalStock = 0

        '        GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "CR", ConWH, "CL")

        For I = mDeptSeq To mMaxDepSeq
            pDeptCode = GetProductDept(mProductCode, I, pDate)
            GetWIPStockQty = GetWIPStockQty + GetStockQty(mProductCode, mItemUOM, pDeptCode, "ST", ConPH, mFieldName)
            GetWIPStockQty = GetWIPStockQty + GetStockQty(mProductCode, mItemUOM, pDeptCode, "WR", ConPH, mFieldName)
        Next
        xStoreQty = GetStockQty(mProductCode, mItemUOM, "", "", ConWH, mFieldName)
        xFGQty = GetStockQty(mProductCode, mItemUOM, "", "FG", ConWH, mFieldName)
        xOthersQty = xStoreQty - xFGQty

        GetWIPStockQty = GetWIPStockQty + xStoreQty
        lFGBalStock = lFGBalStock + xFGQty
        lOtherBalStock = lOtherBalStock + xOthersQty



        ''GetStockQty
        Exit Function
ErrPart:
        GetWIPStockQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetStockQty(ByRef pItemCode As String, ByRef pPackUnit As String, ByRef pDeptCode As String, ByRef pStockType As String, ByRef pStock_ID As String, ByRef xShowType As String, Optional ByRef pRefType As String = "", Optional ByRef pIO As String = "") As Double


        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsDept As ADODB.Recordset
        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mTableName As String
        Dim mDeptCode As String

        mDeptCode = ""

        SqlStr = ""

        If pIO = "I" Then
            SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,0)) AS BALQTY"
        ElseIf pIO = "O" Then
            SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',0,-1)) AS BALQTY"
        Else
            SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"
        End If

        mTableName = ConInventoryTable

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"

        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If pDeptCode <> "" And pStock_ID = ConPH Then
            SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & pDeptCode & "'"
        ElseIf pDeptCode = "PAD" And pStock_ID = ConWH And pStockType = "FG" Then
            ''02-08-2006
            'SqlStr = SqlStr & vbCrLf & "AND (DEPT_CODE_FROM='" & pDeptCode & "' OR DEPT_CODE_TO='" & pDeptCode & "')"
        End If

        If pRefType <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN (" & pRefType & ")"
        End If

        If pStockType = "QC" Then
            If xShowType = "OP" Or xShowType = "CL" Then
                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('SC','CR')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE IN ('ST','" & pStockType & "')"
            End If
        Else
            If pStockType = "" Then
                '            SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "')"
                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE <>'CR'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & pStockType & "'" '' AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "')"
            End If
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format((pDateTo), "DD-MMM-YYYY") & "')"


        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalStock, ADODB.LockTypeEnum.adLockReadOnly)

        If RsBalStock.EOF = False Then
            If IsDbNull(RsBalStock.Fields(0).Value) Then
                mBalQty = 0
            Else
                mBalQty = RsBalStock.Fields(0).Value
            End If
        Else
            mBalQty = 0
        End If

        RsBalStock = Nothing

        If mBalQty <> 0 Then
            RsTemp = Nothing

            SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mIssueUOM = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mPurchaseUOM = IIf(IsDbNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                mFactor = IIf(IsDbNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)

                If pPackUnit = mPurchaseUOM Then
                    mBalQty = mBalQty / mFactor
                End If

                RsTemp = Nothing
                '            RsTemp.Close
            End If
        End If

        GetStockQty = mBalQty

        Exit Function
ErrPart:
        GetStockQty = 0
    End Function
    Private Function GetItemSTStock(ByRef pItemCode As String, ByRef pAsOnDate As String, ByRef pStockID As String) As Double

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTableName As String

        GetItemSTStock = 0

        mTableName = ConInventoryTable


        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) as TotClosing " & vbCrLf & " FROM " & mTableName & " INV " & vbCrLf & " WHERE " & vbCrLf & " INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND INV.STOCK_ID='" & pStockID & "'" & vbCrLf & " AND INV.STOCK_TYPE IN ('ST','RJ','QC') " & vbCrLf & " AND INV.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"


        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(pAsOnDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') " '& vbCrLf |            & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetItemSTStock = IIf(IsDbNull(RsTemp.Fields("TotClosing").Value), 0, RsTemp.Fields("TotClosing").Value)
        End If
        Exit Function
InsertErr:
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function FillDatainGrid(ByRef pItemCode As String, ByRef pItemDesc As String, ByRef pItemUOM As String, ByRef mIssueUOM As String, ByRef mPurchaseUOM As String, ByRef mFactor As Double, ByRef pTotClosing As Double, ByRef CntRow As Integer, ByRef xCatDesc As String, ByRef mStoreBal As Double, ByRef mPHBalStock As Double, ByRef mWIPBalStock As Double, ByRef mFGBalStock As Double, ByRef mOtherBalStock As Double) As Boolean



        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTemp1 As ADODB.Recordset = Nothing

        Dim mRunningBal As Double
        Dim mApprovedQty As Double
        Dim mQtyValue As Double
        Dim mCalcQty As Double
        Dim pRefDate As String
        Dim xPurchaseCost As Double
        Dim xLandedCost As Double
        Dim mTotItemAmount As Double
        Dim mTotCalcQty As Double
        Dim mSuppName As String = ""
        Dim mTableName As String
        Dim mMKEY As String
        Dim mCostType As String
        Dim mItemValue As Double
        Dim pQty As Double

        FillDatainGrid = False
        If pTotClosing <= 0 Then
            FillDatainGrid = True
            Exit Function
        End If

        '
        SqlStr = "SELECT IH.MKEY, IH.AUTO_KEY_MRR, IH.BILLNO, IH.INVOICE_DATE, CMST.SUPP_CUST_NAME, IH.VNO,IH.VDATE, IH.MRRDATE,IH.ISMODVAT, IH.ISSTREFUND, IH.CESSAMOUNT, " & vbCrLf & " ID.ITEM_UOM, " & vbCrLf & " (ID.ITEM_QTY-ID.SHORTAGE_QTY-REJECTED_QTY) as APPROVED_QTY, " & vbCrLf & " ID.ITEM_RATE, ID.ITEM_ED, ID.ITEM_ST, ID.ITEM_CESS " & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST, FIN_INVTYPE_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IH.TRNTYPE=INVMST.CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE= CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND ISFOC='N' AND IH.CANCELLED='N' AND VNO<>'-1' " & vbCrLf & " AND ISFINALPOST='Y' AND INVMST.CATEGORY='P' AND ISSALEJW<>'Y'" & vbCrLf & " AND IH.MRRDATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        ''ORDER BY

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.MRRDATE DESC, TO_NUMBER(SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)) DESC,TO_NUMBER(SUBSTR(AUTO_KEY_MRR,1,LENGTH(AUTO_KEY_MRR)-6)) DESC"
        '    SqlStr = SqlStr & vbCrLf & " ORDER BY IH.AUTO_KEY_MRR DESC, IH.MRRDATE DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        SprdMain.MaxRows = CntRow
        If RsTemp.EOF = False Then
            With SprdMain
                mRunningBal = pTotClosing
                Do While Not RsTemp.EOF

                    mApprovedQty = IIf(IsDbNull(RsTemp.Fields("APPROVED_QTY").Value), 0, RsTemp.Fields("APPROVED_QTY").Value)
                    pRefDate = IIf(IsDbNull(RsTemp.Fields("MRRDATE").Value), "", RsTemp.Fields("MRRDATE").Value)
                    mMKEY = IIf(IsDbNull(RsTemp.Fields("mKey").Value), "-1", RsTemp.Fields("mKey").Value)

                    If mApprovedQty <= 0 Then GoTo NextRec

                    If chkRateRequired.CheckState = System.Windows.Forms.CheckState.Checked Then
                        xPurchaseCost = GetPurchasePORate(mMKEY, pRefDate, Trim(pItemCode)) '' IIf(IsNull(RsTemp!ITEM_RATE), 0, RsTemp!ITEM_RATE)

                        If xPurchaseCost = 0 Then
                            pQty = IIf(mApprovedQty <= 0, 1, mApprovedQty)
                            mCostType = IIf(optShow(0).Checked = True, "P", IIf(optShow(1).Checked = True, "L", "S"))
                            mItemValue = GetLatestItemCostFromMRR(pItemCode, pItemUOM, pQty, pRefDate, mCostType, "ST", "", , , "WH")
                            xPurchaseCost = mItemValue / pQty
                        End If
                    End If

                    If pItemUOM <> IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value) Then
                        mApprovedQty = mApprovedQty * mFactor
                        xPurchaseCost = xPurchaseCost / mFactor
                    End If


                    If mRunningBal <= mApprovedQty Then
                        mCalcQty = mRunningBal
                        mRunningBal = 0
                    ElseIf mRunningBal > mApprovedQty Then
                        mCalcQty = mApprovedQty
                        mRunningBal = mRunningBal - mApprovedQty
                    End If

                    .Row = CntRow

                    .Col = ColItemCode
                    .Text = MainClass.AllowSingleQuote(pItemCode)

                    .Col = ColItemDesc
                    .Text = MainClass.AllowSingleQuote(pItemDesc)

                    .Col = ColUom
                    .Text = MainClass.AllowSingleQuote(pItemUOM)

                    .Col = ColCategory
                    .Text = MainClass.AllowSingleQuote(xCatDesc)

                    .Col = colBalStock
                    .Text = VB6.Format(pTotClosing, "0.00")

                    .Col = ColMRRNo
                    .Text = CStr(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_MRR").Value), "", RsTemp.Fields("AUTO_KEY_MRR").Value))

                    .Col = ColMRRDate
                    .Text = IIf(IsDbNull(RsTemp.Fields("MRRDATE").Value), "", RsTemp.Fields("MRRDATE").Value)

                    .Col = ColBillNo
                    .Text = IIf(IsDbNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)

                    .Col = ColBillDate
                    .Text = IIf(IsDbNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value)


                    .Col = ColVNO
                    .Text = IIf(IsDbNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)

                    .Col = ColVDate
                    .Text = IIf(IsDbNull(RsTemp.Fields("VDate").Value), "", RsTemp.Fields("VDate").Value)

                    .Col = colSupplier
                    mSuppName = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                    .Col = ColQty
                    .Text = VB6.Format(mCalcQty, "0.00")
                    mTotCalcQty = mTotCalcQty + mCalcQty


                    .Col = ColPurchaseCost
                    .Text = VB6.Format(xPurchaseCost, "0.00")


                    xLandedCost = xPurchaseCost

                    .Col = ColED
                    If RsTemp.Fields("ISMODVAT").Value = "N" Then
                        .Text = CStr(IIf(IsDbNull(RsTemp.Fields("ITEM_ED").Value), 0, RsTemp.Fields("ITEM_ED").Value) / mApprovedQty)
                    Else
                        .Text = CStr(0)
                    End If

                    xLandedCost = xLandedCost + Val(.Text)

                    .Col = ColCess
                    If RsTemp.Fields("ISMODVAT").Value = "N" Then
                        .Text = CStr(IIf(IsDbNull(RsTemp.Fields("ITEM_CESS").Value), 0, RsTemp.Fields("ITEM_CESS").Value) / mApprovedQty)
                    Else
                        .Text = CStr(0)
                    End If

                    xLandedCost = xLandedCost + Val(.Text)

                    .Col = ColST
                    If RsTemp.Fields("ISSTREFUND").Value = "N" Then
                        .Text = CStr(IIf(IsDbNull(RsTemp.Fields("ITEM_ST").Value), 0, RsTemp.Fields("ITEM_ST").Value) / mApprovedQty)
                    Else
                        .Text = CStr(0)
                    End If
                    xLandedCost = xLandedCost + Val(.Text)

                    .Col = ColLandedCost
                    .Text = VB6.Format(xLandedCost, "0.00")

                    .Col = ColAmount

                    If optShow(0).Checked = True Then
                        mQtyValue = (mCalcQty * CDbl(VB6.Format(xPurchaseCost, "0.0000")))
                    Else
                        mQtyValue = (mCalcQty * CDbl(VB6.Format(xLandedCost, "0.0000")))
                    End If

                    .Text = VB6.Format(mQtyValue, "0.00")

                    mTotItemAmount = mTotItemAmount + mQtyValue

                    If mRunningBal = 0 Then
                        Exit Do
                    End If
NextRec:
                    RsTemp.MoveNext()
                    If RsTemp.EOF = True Then
                        If mRunningBal > 0 Then
                            CntRow = CntRow + 1
                            SprdMain.MaxRows = CntRow

                            .Row = CntRow

                            .Col = ColItemCode
                            .Text = MainClass.AllowSingleQuote(pItemCode)

                            .Col = ColItemDesc
                            .Text = MainClass.AllowSingleQuote(pItemDesc)

                            .Col = ColUom
                            .Text = MainClass.AllowSingleQuote(pItemUOM)

                            .Col = ColCategory
                            .Text = MainClass.AllowSingleQuote(xCatDesc)

                            .Col = colBalStock
                            .Text = VB6.Format(pTotClosing, "0.00")

                            .Col = ColMRRDate
                            .Text = "-1"

                            .Col = colSupplier
                            .Text = mSuppName ''IIf(IsNull(RsTemp!SUPP_CUST_NAME), "", RsTemp!SUPP_CUST_NAME)

                            .Col = ColQty
                            .Text = VB6.Format(mRunningBal, "0.00")

                            .Col = ColPurchaseCost
                            .Text = VB6.Format(xPurchaseCost, "0.00")

                            .Col = ColLandedCost
                            .Text = VB6.Format(xLandedCost, "0.00")

                            .Col = ColAmount
                            mQtyValue = (mRunningBal * xLandedCost)
                            .Text = VB6.Format(mQtyValue, "0.00")


                            mTotCalcQty = mTotCalcQty + mRunningBal
                            mTotItemAmount = mTotItemAmount + mQtyValue
                        End If
                    Else
                        CntRow = CntRow + 1
                        SprdMain.MaxRows = CntRow
                    End If
                Loop
            End With
        Else

            mTableName = ConInventoryTable

            SqlStr = "SELECT REF_NO, REF_DATE, PURCHASE_COST, LANDED_COST," & vbCrLf & " ITEM_UOM " & vbCrLf & " FROM " & mTableName & "" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ITEM_CODE='" & pItemCode & "' AND STATUS='O'" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            ''ORDER BY

            SqlStr = SqlStr & vbCrLf & " ORDER BY REF_NO, REF_DATE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            '        cntRow = 1
            SprdMain.MaxRows = CntRow
            If RsTemp.EOF = False Then
                With SprdMain

                    .Row = CntRow
                    .Col = ColItemCode
                    .Text = MainClass.AllowSingleQuote(pItemCode)

                    .Col = ColItemDesc
                    .Text = MainClass.AllowSingleQuote(pItemDesc)

                    .Col = ColUom
                    .Text = MainClass.AllowSingleQuote(pItemUOM)

                    .Col = ColCategory
                    .Text = MainClass.AllowSingleQuote(xCatDesc)

                    .Col = colBalStock
                    .Text = VB6.Format(pTotClosing, "0.00")

                    .Col = ColMRRNo
                    .Text = CStr(IIf(IsDbNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value))

                    .Col = ColMRRDate
                    pRefDate = IIf(IsDbNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)
                    .Text = IIf(IsDbNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)

                    .Col = ColVNO
                    .Text = "OP"

                    .Col = ColVDate
                    .Text = IIf(IsDbNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)

                    .Col = colSupplier
                    .Text = "OPENING"

                    .Col = ColQty
                    .Text = VB6.Format(pTotClosing, "0.00")

                    .Col = ColPurchaseCost
                    xPurchaseCost = IIf(IsDbNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value)
                    .Text = VB6.Format(xPurchaseCost, "0.00")

                    .Col = ColLandedCost
                    xLandedCost = IIf(IsDbNull(RsTemp.Fields("LANDED_COST").Value), "", RsTemp.Fields("LANDED_COST").Value)
                    .Text = VB6.Format(xLandedCost, "0.00")

                    .Col = ColAmount
                    mQtyValue = (pTotClosing * CDbl(VB6.Format(xLandedCost, "0.0000")))
                    .Text = VB6.Format(mQtyValue, "0.00")

                    mTotItemAmount = mQtyValue
                End With
            End If
            '        If GetLatestItemCost(pItemCode, xPurchaseCost, xLandedCost, pRefDate, "ST", "", pItemUOM, mFactor) = False Then GoTo ErrPart
            '
            '        If optShow(0).Value = True Then
            '            mQtyValue = (pTotClosing * xPurchaseCost)
            '        ElseIf optShow(1).Value = True Then
            '            mQtyValue = (pTotClosing * VB6.Format(xLandedCost, "0.0000"))
            '        End If
            '
            '        GetBalItemValue = mQtyValue
        End If

        With SprdMain
            CntRow = CntRow + 1
            SprdMain.MaxRows = CntRow

            .Row = CntRow
            .Row2 = CntRow
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF00)
            .BlockMode = False

            .Col = ColItemDesc
            .Text = "TOTAL :"

            .Col = ColQty
            .Text = VB6.Format(mTotCalcQty, "0.00")

            .Col = ColAmount
            .Text = VB6.Format(mTotItemAmount, "0.00")

            .Col = ColStoreStock
            .Text = VB6.Format(mStoreBal, "0.00")

            .Col = ColProductionStock
            .Text = VB6.Format(mPHBalStock, "0.00")

            .Col = ColWIPStock
            .Text = VB6.Format(mWIPBalStock, "0.00")

            .Col = ColFGStock
            .Text = VB6.Format(mFGBalStock, "0.00")

            .Col = ColOthers
            .Text = VB6.Format(mOtherBalStock, "0.00")

        End With

        FillDatainGrid = True
        Exit Function
ErrPart:
        '    Resume
        FillDatainGrid = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
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
        '    GroupByColor

        PrintStatus(True)
        '    SprdMain.SetFocus
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
    End Sub

    Public Sub frmParamStockDetailAll_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
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


    Private Sub frmParamStockDetailAll_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        MainClass.SetControlsColor(Me)

        txtAsOn.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        FormatSprdMain(-1)
        FillSprdMain()

        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked

        cmdsearch.Enabled = False
        cmdSearchCategory.Enabled = False
        cmdSubCatsearch.Enabled = False

        txtItemName.Enabled = False
        txtCategory.Enabled = False
        txtSubCategory.Enabled = False
        optShow(1).Checked = True

        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub frmParamStockDetailAll_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub PrintStatus(ByRef PrintFlag As Boolean)
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub
    Private Sub txtCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.DoubleClick
        SearchCategory()
    End Sub
    Private Sub txtCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCategory()
    End Sub
    Private Sub txtCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCategory.Text = UCase(Trim(txtCategory.Text))
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
    Private Sub SearchCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster((txtCategory.Text), "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtCategory.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        SearchItem()
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub
    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            txtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtSubCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtSubCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.DoubleClick
        SearchSubCategory()
    End Sub


    Private Sub txtSubCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSubCategory()
    End Sub

    Private Sub txtSubCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtSubCategory.Text = "" Then GoTo EventExitSub


        If txtCategory.Text = "" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Else
            If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mCatCode = MasterNo
            End If
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"
        End If

        If MainClass.ValidateWithMasterTable((txtSubCategory.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("No Such Sub Category ")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub chkAllCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCategory.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCategory.Enabled = False
            cmdSearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdSearchCategory.Enabled = True
        End If
    End Sub

    Private Sub chkAllSubCat_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSubCat.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSubCategory.Enabled = False
            cmdSubCatsearch.Enabled = False
        Else
            txtSubCategory.Enabled = True
            cmdSubCatsearch.Enabled = True
        End If
    End Sub
    Private Sub SearchSubCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtCategory.Text = "" Then
            MsgInformation("Please Select category .")
            txtCategory.Focus()
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCatCode = MasterNo
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"


        If MainClass.SearchGridMaster((txtSubCategory.Text), "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtSubCategory.Text = AcName
            End If
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub
End Class
