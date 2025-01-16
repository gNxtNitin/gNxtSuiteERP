Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamStockDetail
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean

    Private Const RowHeight As Short = 12

    Private Const ColMRRNo As Short = 1
    Private Const ColMRRDate As Short = 2
    Private Const ColVNO As Short = 3
    Private Const colSupplier As Short = 4
    Private Const ColVDate As Short = 5
    Private Const ColQty As Short = 6
    Private Const ColPurchaseCost As Short = 7
    Private Const ColED As Short = 8
    Private Const ColCess As Short = 9
    Private Const ColST As Short = 10
    Private Const ColLandedCost As Short = 11
    Private Const ColAmount As Short = 12

    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean
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

            .Col = ColMRRNo
            .Text = "MRR No"

            .Col = ColMRRDate
            .Text = "MRR Date"

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

            .Col = ColVNO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColVNO, 8)

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

            For I = ColQty To ColAmount
                .Col = I
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .set_RowHeight(Arow, RowHeight)
                .set_ColWidth(I, 8)
                If .Col = ColED Or .Col = ColCess Or .Col = ColST Then
                    .ColHidden = True
                End If
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
        mSubTitle = MainClass.AllowSingleQuote(txtItemName.Text)
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

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        For CntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = CntRow

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

            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " FIELD1, FIELD2, " & vbCrLf & " FIELD3, FIELD4, " & vbCrLf & " FIELD5, FIELD6, " & vbCrLf & " FIELD7, FIELD8, " & vbCrLf & " FIELD9, FIELD10, " & vbCrLf & " FIELD11, FIELD12, " & vbCrLf & " FIELD13, FIELD14, FIELD15 " & vbCrLf & " ) VALUES (" & vbCrLf & " '" & PubUserID & "', " & CntRow & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtItemName.Text) & "', '" & txtAsOn.Text & "'," & vbCrLf & " '" & lblItemUom.Text & "', '" & txtClosing.Text & "', " & vbCrLf & " '" & mMRRDATE & "', '" & mVNo & "', " & vbCrLf & " '" & mVDate & "', '" & mQty & "', " & vbCrLf & " '" & mPurchaseCost & "', '" & mED & "', " & vbCrLf & " '" & mCess & "', '" & mST & "', " & vbCrLf & " '" & mLandedCost & "', '" & mAmount & "','" & MainClass.AllowSingleQuote(mPartyName) & "')"

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
        Dim I As Integer
        Dim mCategoryCode As String = ""
        Dim mCond As String

        FieldsVarification = True

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

        SqlStr = MakeSQL
        FormatSprdMain(-1)
        Show1 = True
        Exit Function
InsertErr:
        Show1 = False
        MsgBox(Err.Description)
        ''Resume
    End Function



    Private Function MakeSQL() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTemp1 As ADODB.Recordset = Nothing

        Dim mRunningBal As Double
        Dim mApprovedQty As Double
        Dim mQtyValue As Double
        Dim mCalcQty As Double
        Dim pRefDate As String
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim xPurchaseCost As Double
        Dim xLandedCost As Double
        Dim pTotClosing As String
        Dim pItemUOM As String = ""
        Dim mTotItemAmount As Double
        Dim CntRow As Integer
        Dim mSupplierName As String = ""
        Dim mTableName As String
        Dim mMKEY As String
        MakeSQL = ""
        pTotClosing = CStr(Val(txtClosing.Text))
        pItemUOM = Trim(lblItemUOM.Text)
        If CDbl(pTotClosing) <= 0 Then
            MakeSQL = ""
            Exit Function
        End If

        mFactor = 1

        SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR,PURCHASE_COST FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote((lblItemCode.Text)) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp1.EOF = False Then
            mIssueUOM = IIf(IsDBNull(RsTemp1.Fields("ISSUE_UOM").Value), "", RsTemp1.Fields("ISSUE_UOM").Value)
            mPurchaseUOM = IIf(IsDBNull(RsTemp1.Fields("PURCHASE_UOM").Value), "", RsTemp1.Fields("PURCHASE_UOM").Value)
            mFactor = IIf(IsDBNull(RsTemp1.Fields("UOM_FACTOR").Value) Or RsTemp1.Fields("UOM_FACTOR").Value = 0, 1, RsTemp1.Fields("UOM_FACTOR").Value)
        End If

        SqlStr = "SELECT IH.MKEY, IH.AUTO_KEY_MRR, CMST.SUPP_CUST_NAME, IH.VNO,IH.VDATE, IH.MRRDATE,IH.ISMODVAT, IH.ISSTREFUND, IH.CESSAMOUNT, " & vbCrLf _
            & " ID.ITEM_UOM, " & vbCrLf _
            & " (ID.ITEM_QTY-ID.SHORTAGE_QTY-REJECTED_QTY) as APPROVED_QTY, " & vbCrLf _
            & " ID.ITEM_RATE, ID.ITEM_ED, ID.ITEM_ST, ID.ITEM_CESS " & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST, FIN_INVTYPE_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.TRNTYPE=INVMST.CODE"

        'SqlStr = SqlStr & vbCrLf _
        '    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND ID.COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " 

        If lblCompanyCode.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE IN " & lblCompanyCode.Text & ""
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR > 2022"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " And IH.COMPANY_CODE = CMST.COMPANY_CODE " & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " And ID.ITEM_CODE='" & lblItemCode.Text & "'" & vbCrLf _
            & " AND ISFOC='N' AND IH.CANCELLED='N' AND VNO<>'-1' " & vbCrLf _
            & " AND ISFINALPOST='Y' AND INVMST.CATEGORY='P' AND ISSALEJW<>'Y'" & vbCrLf _
            & " AND IH.MRRDATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        ''ORDER BY

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.MRRDATE DESC, TO_NUMBER(SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)) DESC,TO_NUMBER(SUBSTR(AUTO_KEY_MRR,1,LENGTH(AUTO_KEY_MRR)-6)) DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        CntRow = 1
        SprdMain.MaxRows = CntRow
        If RsTemp.EOF = False Then
            With SprdMain
                mRunningBal = CDbl(pTotClosing)
                Do While Not RsTemp.EOF

                    mMKEY = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "-1", RsTemp.Fields("mKey").Value)
                    mApprovedQty = IIf(IsDBNull(RsTemp.Fields("APPROVED_QTY").Value), 0, RsTemp.Fields("APPROVED_QTY").Value)
                    pRefDate = IIf(IsDBNull(RsTemp.Fields("MRRDATE").Value), "", RsTemp.Fields("MRRDATE").Value)

                    If mApprovedQty <= 0 Then GoTo NextRec

                    xPurchaseCost = GetPurchasePORate(mMKEY, pRefDate, Trim(lblItemCode.Text)) '' IIf(IsNull(RsTemp!ITEM_RATE), 0, RsTemp!ITEM_RATE)

                    'ID.ITEM_RATE

                    If pItemUOM <> IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value) Then
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
                    .Col = ColMRRNo
                    .Text = CStr(IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_MRR").Value), "", RsTemp.Fields("AUTO_KEY_MRR").Value))

                    .Col = ColMRRDate
                    .Text = IIf(IsDBNull(RsTemp.Fields("MRRDATE").Value), "", RsTemp.Fields("MRRDATE").Value)

                    .Col = ColVNO
                    .Text = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)

                    .Col = ColVDate
                    .Text = IIf(IsDBNull(RsTemp.Fields("VDate").Value), "", RsTemp.Fields("VDate").Value)

                    .Col = colSupplier
                    .Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    mSupplierName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                    .Col = ColQty
                    .Text = VB6.Format(mCalcQty, "0.00")

                    .Col = ColPurchaseCost
                    .Text = VB6.Format(xPurchaseCost, "0.00")

                    xLandedCost = xPurchaseCost

                    .Col = ColED
                    If RsTemp.Fields("ISMODVAT").Value = "N" Then
                        .Text = CStr(IIf(IsDBNull(RsTemp.Fields("ITEM_ED").Value), 0, RsTemp.Fields("ITEM_ED").Value) / mApprovedQty)
                    Else
                        .Text = CStr(0)
                    End If
                    xLandedCost = xLandedCost + Val(.Text)

                    .Col = ColCess
                    If RsTemp.Fields("ISMODVAT").Value = "N" Then
                        .Text = CStr(IIf(IsDBNull(RsTemp.Fields("ITEM_CESS").Value), 0, RsTemp.Fields("ITEM_CESS").Value) / mApprovedQty)
                    Else
                        .Text = CStr(0)
                    End If
                    xLandedCost = xLandedCost + Val(.Text)

                    .Col = ColST
                    If RsTemp.Fields("ISSTREFUND").Value = "N" Then
                        .Text = CStr(IIf(IsDBNull(RsTemp.Fields("ITEM_ST").Value), 0, RsTemp.Fields("ITEM_ST").Value) / mApprovedQty)
                    Else
                        .Text = CStr(0)
                    End If
                    xLandedCost = xLandedCost + Val(.Text)

                    .Col = ColLandedCost
                    .Text = VB6.Format(xLandedCost, "0.00")

                    .Col = ColAmount
                    mQtyValue = (mCalcQty * CDbl(VB6.Format(xLandedCost, "0.0000")))
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
                            .Col = ColMRRDate
                            .Text = "-1"

                            .Col = colSupplier
                            .Text = mSupplierName 'IIf(IsNull(RsTemp!SUPP_CUST_NAME), "", RsTemp!SUPP_CUST_NAME)

                            .Col = ColQty
                            .Text = VB6.Format(mRunningBal, "0.00")

                            .Col = ColPurchaseCost
                            .Text = VB6.Format(xPurchaseCost, "0.00")

                            .Col = ColLandedCost
                            .Text = VB6.Format(xLandedCost, "0.00")

                            .Col = ColAmount
                            mQtyValue = (mRunningBal * xLandedCost)
                            .Text = VB6.Format(mQtyValue, "0.00")



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

            SqlStr = "SELECT '' AS REF_NO, '' AS REF_DATE, RATE AS PURCHASE_COST, RATE AS LANDED_COST," & vbCrLf _
                & " '" & mIssueUOM & "' AS ITEM_UOM " & vbCrLf _
                & " FROM INV_ITEM_RATE_MST  " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ITEM_CODE='" & lblItemCode.Text & "' "

            '& vbCrLf _
            '    & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            ''SELECT B.RATE FROM INV_ITEM_RATE_MST B WHERE B.COMPANY_CODE=mCompanyCode AND ITEM_CODE=nItem_code;


            ''ORDER BY

            'SqlStr = SqlStr & vbCrLf & " ORDER BY REF_NO, REF_DATE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            CntRow = 1
            SprdMain.MaxRows = CntRow
            If RsTemp.EOF = False Then
                With SprdMain

                    .Row = CntRow
                    .Col = ColMRRNo
                    .Text = CStr(IIf(IsDBNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value))

                    .Col = ColMRRDate
                    pRefDate = IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)
                    .Text = IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)

                    .Col = ColVNO
                    .Text = "OP"

                    .Col = ColVDate
                    .Text = IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)

                    .Col = colSupplier
                    .Text = "OPENING"

                    .Col = ColQty
                    .Text = VB6.Format(pTotClosing, "0.00")

                    .Col = ColPurchaseCost
                    xPurchaseCost = IIf(IsDBNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value)
                    .Text = VB6.Format(xPurchaseCost, "0.00")

                    .Col = ColLandedCost
                    xLandedCost = IIf(IsDBNull(RsTemp.Fields("LANDED_COST").Value), "", RsTemp.Fields("LANDED_COST").Value)
                    .Text = VB6.Format(xLandedCost, "0.00")

                    .Col = ColAmount
                    mQtyValue = (CDbl(pTotClosing) * CDbl(VB6.Format(xLandedCost, "0.0000")))
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

        lblTotal.Text = VB6.Format(mTotItemAmount, "0.00")
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Public Sub frmParamStockDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        Dim SqlStr As String = ""
        If FormLoaded = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Call Show1()
        FormLoaded = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub


    Private Sub frmParamStockDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(9450)

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)

        MainClass.SetControlsColor(Me)

        FormatSprdMain(-1)
        FillSprdMain()
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub frmParamStockDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub PrintStatus(ByRef PrintFlag As Boolean)
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub
End Class
