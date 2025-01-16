Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAssetsEntry
    Inherits System.Windows.Forms.Form
    Dim RsAssetTRN As ADODB.Recordset
    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim xMyMenu As String
    Private Const ConRowHeight As Short = 12
    Private Const ColFYear As Short = 1

    Private Const ColBillNo As Short = 1
    Private Const ColBillDate As Short = 2
    Private Const ColOriginalCost As Short = 3
    Private Const ColSupplier As Short = 4
    Private Const ColSaleAmount As Short = 5
    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtAssetSNo.Enabled = False
            cmdSearchAssetCode.Enabled = False
            txtAssetType.Enabled = True
            cmdSearchAssetType.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsAssetTRN.EOF = False Then RsAssetTRN.MoveFirst()
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume	
    End Sub
    Private Sub FillHeading()
        Dim RsTemp As ADODB.Recordset
        Dim cntCol As Integer
        Dim mAddDeduct As Integer
        Dim SqlStr As String

        MainClass.ClearGrid(SprdSale)
        With SprdSale
            .MaxCols = 5
            .MaxRows = 6
        End With

        MainClass.ClearGrid(SprdMain)

        With SprdMain
            .MaxCols = ColFYear

            .Row = 0
            .set_RowHeight(0, ConRowHeight)

            .Col = ColFYear
            .Text = "FYEAR"

            .ColsFrozen = ColFYear

            SqlStr = " SELECT MODE_CODE " & vbCrLf & " FROM AST_DEPRECIATION_MODE_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            SqlStr = SqlStr & vbCrLf & " ORDER BY MODE_CODE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                .MaxCols = .MaxCols + 1
                cntCol = 1
                Do While Not RsTemp.EOF
                    .Col = ColFYear + cntCol
                    .Text = RsTemp.Fields("MODE_CODE").Value
                    cntCol = cntCol + 1
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        .MaxCols = .MaxCols + 1
                    End If
                Loop
            End If
            MainClass.ProtectCell(SprdMain, 0, SprdMain.MaxRows, 0, SprdMain.MaxCols)
        End With
    End Sub
    Private Sub FormatSprd(ByRef mRow As Integer)
        Dim cntCol As Integer

        On Error GoTo ERR1

        With SprdSale
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsAssetTRN.Fields("SALE_BILL_NO").DefinedSize
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 13)

            .Col = 2
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(.Col, 9)

            .Col = 3
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(.Col, 15)

            .Col = 4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsAssetTRN.Fields("SALE_PARTY_NAME").DefinedSize
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 35)

            .Col = 5
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(.Col, 15)


        End With

        MainClass.SetSpreadColor(SprdSale, mRow)

        With SprdMain
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)


            .Col = ColFYear
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColFYear, 10)

            For cntCol = ColFYear + 1 To .MaxCols
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 10)
            Next

        End With
        MainClass.ProtectCell(SprdMain, 0, SprdMain.MaxRows, 0, SprdMain.MaxCols)
        MainClass.SetSpreadColor(SprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtAssetSNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If RsAssetTRN.Fields("CANCELLED").Value = "Y" Then
            MsgInformation("Cancelled Entry Cann't be Deleted.")
            Exit Sub
        End If

        If CDate(txtPVDate.Text) < CDate("01/04/2007") Then
            MsgInformation("Upto Last Year Locked")
            Exit Sub
        End If

        If txtPutDate.Text <> "" Then
            If CDate(txtPutDate.Text) < CDate("01/04/2007") Then
                MsgInformation("Upto Last Year Locked")
                Exit Sub
            End If
        End If

        If Not RsAssetTRN.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "AST_ASSET_TRN", (txtAssetSNo.Text), RsAssetTRN) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "AST_ASSET_TRN", "AUTO_KEY_ASSET", (lblAssetCode.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM AST_ASSET_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & Val(lblAssetCode.Text) & "")
                PubDBCn.CommitTrans()
                RsAssetTRN.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsAssetTRN.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            If RsAssetTRN.Fields("CANCELLED").Value = "Y" Then
                MsgInformation("Cancelled Entry cann't be Modified")
                Exit Sub
            End If

            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsAssetTRN, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtAssetSNo.Enabled = False
            cmdSearchAssetCode.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtAssetSNo_Validating(txtAssetSNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mStatus As String
        Dim mSeqNo As Double
        Dim mGroupCode As Integer
        Dim mFYear As Integer
        Dim mCancelled As String



        ReDim mSaleData(SprdSale.MaxRows)
        Dim cntRow As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""

        If MainClass.ValidateWithMasterTable(txtAssetType.Text, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
            mGroupCode = MasterNo
        Else
            mGroupCode = -1
        End If

        mFYear = GetMRRFYNO((txtPVDate.Text))

        mStatus = VB.Left(cboStatus.Text, 1)
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        With SprdSale
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColBillNo
                mSaleData(cntRow).mBillNo = Trim(.Text)

                .Col = ColBillDate
                mSaleData(cntRow).mBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColOriginalCost
                mSaleData(cntRow).mOriginalCost = Val(.Text)

                .Col = ColSupplier
                mSaleData(cntRow).mSupplier = Trim(.Text)

                .Col = ColSaleAmount
                mSaleData(cntRow).mSaleAmount = Val(.Text)
            Next
        End With

        If lblBookType.Text = "" Then lblBookType.Text = "P"

        If ADDMode = True Then
            mSeqNo = CDbl(AutoGenAssetNo())
            txtAssetSNo.Text = VB6.Format(mSeqNo, "00000")
            lblAssetCode.Text = CStr(mSeqNo)
            SqlStr = " INSERT INTO AST_ASSET_TRN (" & vbCrLf _
                & " AUTO_KEY_ASSET, COMPANY_CODE, FYEAR, " & vbCrLf _
                & " GROUP_CODE, MRR_NO, MRR_DATE, " & vbCrLf _
                & " PV_NO, PV_DATE, BILL_NO, " & vbCrLf _
                & " BILL_DATE, SUPP_CUST_NAME, ITEM_DESC, " & vbCrLf _
                & " INSTALL_DATE, PUT_DATE, ITEM_VALUE, " & vbCrLf _
                & " CD_AMOUNT, OTH_AMOUNT, TOTAL_COST, " & vbCrLf _
                & " MODVAT_AMOUNT, CESS_AMOUNT, SHEC_AMOUNT, " & vbCrLf _
                & " AED_AMOUNT, MODVAT_DUR_YEAR_PER, STATUS, " & vbCrLf _
                & " SALE_BILL_NO, SALE_BILL_DATE, SALE_PARTY_NAME, SALE_AMOUNT," & vbCrLf _
                & " SALE_BILL_NO1, SALE_BILL_DATE1, SALE_PARTY_NAME1, SALE_AMOUNT1," & vbCrLf _
                & " SALE_BILL_NO2, SALE_BILL_DATE2, SALE_PARTY_NAME2, SALE_AMOUNT2," & vbCrLf _
                & " SALE_BILL_NO3, SALE_BILL_DATE3, SALE_PARTY_NAME3, SALE_AMOUNT3," & vbCrLf _
                & " ORIGINAL_COST, ORIGINAL_COST1, ORIGINAL_COST2, ORIGINAL_COST3," & vbCrLf _
                & " PHY_VARIFICATION, LOCATION, " & vbCrLf _
                & " REMARKS, SALVAGE_AMT, VMKEY, BOOKTYPE," & vbCrLf _
                & " ADDUSER, ADDDATE, " & vbCrLf _
                & " MODUSER, MODDATE,CANCELLED,ITEM_TYPE,SALETAX_REFUND,DN_CR_AMOUNT," & vbCrLf _
                & " CGST_CLAIMAMOUNT, SGST_CLAIMAMOUNT, IGST_CLAIMAMOUNT)" & vbCrLf _
                & " VALUES ( "

            SqlStr = SqlStr & vbCrLf _
                & " " & mSeqNo & ", " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mFYear & ", " & vbCrLf _
                & " " & mGroupCode & ", " & Val(txtMRRNo.Text) & ",TO_DATE('" & vb6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & txtPVNo.Text & "',TO_DATE('" & vb6.Format(txtPVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & txtBillNo.Text & "', " & vbCrLf _
                & " TO_DATE('" & vb6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtSupplierName.Text) & "', '" & MainClass.AllowSingleQuote(txtItemDesc.Text) & "'," & vbCrLf _
                & " TO_DATE('" & vb6.Format(txtInstallDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & vb6.Format(txtPutDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtItemValue.Text) & "," & vbCrLf _
                & " " & Val(txtCDAmount.Text) & ", " & Val(txtInstallAmount.Text) & ", " & Val(txtTotalCost.Text) & "," & vbCrLf _
                & " " & Val(txtModvatAmount.Text) & ",0,0," & vbCrLf _
                & " 0," & Val(txtModvatPer.Text) & ", '" & mStatus & "', " & vbCrLf _
                & " '" & mSaleData(1).mBillNo & "',TO_DATE('" & vb6.Format(mSaleData(1).mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSaleData(1).mSupplier) & "', " & Val(mSaleData(1).mSaleAmount) & "," & vbCrLf _
                & " '" & mSaleData(2).mBillNo & "',TO_DATE('" & vb6.Format(mSaleData(2).mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSaleData(2).mSupplier) & "', " & Val(mSaleData(2).mSaleAmount) & "," & vbCrLf _
                & " '" & mSaleData(3).mBillNo & "',TO_DATE('" & vb6.Format(mSaleData(3).mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSaleData(3).mSupplier) & "', " & Val(mSaleData(3).mSaleAmount) & "," & vbCrLf _
                & " '" & mSaleData(4).mBillNo & "',TO_DATE('" & vb6.Format(mSaleData(4).mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSaleData(4).mSupplier) & "', " & Val(mSaleData(4).mSaleAmount) & "," & vbCrLf _
                & " " & Val(mSaleData(1).mOriginalCost) & ", " & Val(mSaleData(2).mOriginalCost) & ", " & Val(mSaleData(3).mOriginalCost) & ", " & Val(mSaleData(4).mOriginalCost) & "," & vbCrLf _
                & " '','" & MainClass.AllowSingleQuote(txtLocation.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & Val(txtSalvageAmount) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(lblVMKey.Text) & "', '" & MainClass.AllowSingleQuote(lblBookType.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','N'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtItemType.Text) & "'," & Val(txtSTRefund.Text) & "," & Val(txtDrCrAmount.Text) & "," & vbCrLf _
                & " " & Val(txtCGSTAmount.Text) & "," & Val(txtSGSTAmount.Text) & "," & Val(txtIGSTAmount.Text) & ")"




        ElseIf MODIFYMode = True Then

            SqlStr = " UPDATE AST_ASSET_TRN SET " & vbCrLf _
                & " AUTO_KEY_ASSET=" & Val(txtAssetSNo.Text) & ", " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                & " FYEAR=" & mFYear & ", " & vbCrLf _
                & " GROUP_CODE=" & mGroupCode & ", " & vbCrLf _
                & " MRR_NO=" & Val(txtMRRNo.Text) & ", " & vbCrLf _
                & " MRR_DATE=TO_DATE('" & vb6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PV_NO='" & txtPVNo.Text & "', " & vbCrLf _
                & " PV_DATE=TO_DATE('" & vb6.Format(txtPVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " BILL_NO='" & txtBillNo.Text & "', " & vbCrLf _
                & " BILL_DATE=TO_DATE('" & vb6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplierName.Text) & "', " & vbCrLf _
                & " ITEM_DESC='" & MainClass.AllowSingleQuote(txtItemDesc.Text) & "', " & vbCrLf _
                & " INSTALL_DATE=TO_DATE('" & vb6.Format(txtInstallDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PUT_DATE=TO_DATE('" & vb6.Format(txtPutDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ITEM_VALUE=" & Val(txtItemValue.Text) & ", " & vbCrLf _
                & " CD_AMOUNT=" & Val(txtCDAmount.Text) & ", " & vbCrLf _
                & " OTH_AMOUNT=" & Val(txtInstallAmount.Text) & ", DN_CR_AMOUNT=" & Val(txtDrCrAmount.Text) & ","


            SqlStr = SqlStr & vbCrLf & " TOTAL_COST=" & Val(txtTotalCost.Text) & " , " & vbCrLf _
                & " MODVAT_AMOUNT=" & Val(txtModvatAmount.Text) & ", " & vbCrLf _
                & " CESS_AMOUNT=0, " & vbCrLf & " SHEC_AMOUNT=0, " & vbCrLf _
                & " AED_AMOUNT=0, SALETAX_REFUND=" & Val(txtSTRefund.Text) & "," & vbCrLf _
                & " MODVAT_DUR_YEAR_PER=" & Val(txtModvatPer.Text) & ", " & vbCrLf _
                & " CGST_CLAIMAMOUNT=" & Val(txtCGSTAmount.Text) & ", " & vbCrLf _
                & " SGST_CLAIMAMOUNT=" & Val(txtSGSTAmount.Text) & ", " & vbCrLf _
                & " IGST_CLAIMAMOUNT=" & Val(txtIGSTAmount.Text) & "," & vbCrLf

            SqlStr = SqlStr & vbCrLf _
                & " SALE_BILL_NO='" & mSaleData(1).mBillNo & "', " & vbCrLf _
                & " SALE_BILL_DATE=TO_DATE('" & vb6.Format(mSaleData(1).mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ORIGINAL_COST=" & Val(mSaleData(1).mOriginalCost) & ", " & vbCrLf _
                & " SALE_PARTY_NAME='" & MainClass.AllowSingleQuote(mSaleData(1).mSupplier) & "', " & vbCrLf _
                & " SALE_AMOUNT=" & Val(mSaleData(1).mSaleAmount) & ", "

            SqlStr = SqlStr & vbCrLf _
                & " SALE_BILL_NO1='" & mSaleData(2).mBillNo & "', " & vbCrLf _
                & " SALE_BILL_DATE1=TO_DATE('" & vb6.Format(mSaleData(2).mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ORIGINAL_COST1=" & Val(mSaleData(2).mOriginalCost) & ", " & vbCrLf _
                & " SALE_PARTY_NAME1='" & MainClass.AllowSingleQuote(mSaleData(2).mSupplier) & "', " & vbCrLf _
                & " SALE_AMOUNT1=" & Val(mSaleData(2).mSaleAmount) & ", "


            SqlStr = SqlStr & vbCrLf _
                & " SALE_BILL_NO2='" & mSaleData(3).mBillNo & "', " & vbCrLf _
                & " SALE_BILL_DATE2=TO_DATE('" & vb6.Format(mSaleData(3).mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ORIGINAL_COST2=" & Val(mSaleData(3).mOriginalCost) & ", " & vbCrLf _
                & " SALE_PARTY_NAME2='" & MainClass.AllowSingleQuote(mSaleData(3).mSupplier) & "', " & vbCrLf _
                & " SALE_AMOUNT2=" & Val(mSaleData(3).mSaleAmount) & ", "

            SqlStr = SqlStr & vbCrLf _
                & " SALE_BILL_NO3='" & mSaleData(4).mBillNo & "', " & vbCrLf _
                & " SALE_BILL_DATE3=TO_DATE('" & vb6.Format(mSaleData(4).mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ORIGINAL_COST3=" & Val(mSaleData(4).mOriginalCost) & ", " & vbCrLf _
                & " SALE_PARTY_NAME3='" & MainClass.AllowSingleQuote(mSaleData(4).mSupplier) & "', " & vbCrLf _
                & " SALE_AMOUNT3=" & Val(mSaleData(4).mSaleAmount) & ", "

            SqlStr = SqlStr & vbCrLf _
                & " SALE_BILL_NO4='" & mSaleData(5).mBillNo & "', " & vbCrLf _
                & " SALE_BILL_DATE4=TO_DATE('" & vb6.Format(mSaleData(5).mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ORIGINAL_COST4=" & Val(mSaleData(5).mOriginalCost) & ", " & vbCrLf _
                & " SALE_PARTY_NAME4='" & MainClass.AllowSingleQuote(mSaleData(5).mSupplier) & "', " & vbCrLf _
                & " SALE_AMOUNT4=" & Val(mSaleData(5).mSaleAmount) & ", "

            SqlStr = SqlStr & vbCrLf _
                & " SALE_BILL_NO5='" & mSaleData(6).mBillNo & "', " & vbCrLf _
                & " SALE_BILL_DATE5=TO_DATE('" & vb6.Format(mSaleData(6).mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ORIGINAL_COST5=" & Val(mSaleData(6).mOriginalCost) & ", " & vbCrLf _
                & " SALE_PARTY_NAME5='" & MainClass.AllowSingleQuote(mSaleData(6).mSupplier) & "', " & vbCrLf _
                & " SALE_AMOUNT5=" & Val(mSaleData(6).mSaleAmount) & ", "

            SqlStr = SqlStr & vbCrLf _
                & " PHY_VARIFICATION='', " & vbCrLf _
                & " SALVAGE_AMT=" & Val(txtSalvageAmount.Text) & "," & vbCrLf _
                & " LOCATION='" & MainClass.AllowSingleQuote(txtLocation.Text) & "', " & vbCrLf _
                & " CANCELLED='" & mCancelled & "',"

            SqlStr = SqlStr & vbCrLf _
                & " VMKEY='" & MainClass.AllowSingleQuote(lblVMKey.Text) & "', " & vbCrLf _
                & " BOOKTYPE='" & MainClass.AllowSingleQuote(lblBookType.Text) & "', " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " STATUS='" & mStatus & "', ITEM_TYPE='" & MainClass.AllowSingleQuote(txtItemType.Text) & "'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND AUTO_KEY_ASSET ='" & MainClass.AllowSingleQuote(lblAssetCode.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsAssetTRN.Requery()
        MsgBox(Err.Description)
        '    Resume	
    End Function
    Private Function GetMRRFYNO(ByRef pDate As String) As Integer
        On Error GoTo FillFYErr
        Dim SqlStr As String
        Dim RsCFYNo As ADODB.Recordset

        If pDate = "" Then
            Exit Function
        End If
        If Not IsDate(pDate) Then
            Exit Function
        End If

        SqlStr = "SELECT FYEAR,START_DATE,END_DATE FROM GEN_CMPYRDTL_TRN" & " WHERE COMPANY_CODE=" & Val(RsCompany.Fields("COMPANY_CODE").Value) & " " & " AND START_DATE<=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & " AND END_DATE>=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCFYNo)
        If Not RsCFYNo.EOF Then
            GetMRRFYNO = CInt(VB6.Format(CStr(RsCFYNo.Fields("FYEAR").Value), "0000"))
        Else
            Select Case Month(CDate(pDate))
                Case 1, 2, 3
                    GetMRRFYNO = Year(CDate(pDate)) - 1
                Case 4, 5, 6, 7, 8, 9, 10, 11, 12
                    GetMRRFYNO = Year(CDate(pDate))
            End Select
        End If
        Exit Function
FillFYErr:
        GetMRRFYNO = -1
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub ViewGrid()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh	
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsAssetTRN, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub frmAssetsEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Asset Entry"

        SqlStr = "Select * From AST_ASSET_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAssetTRN, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf _
            & " TO_CHAR(IH.AUTO_KEY_ASSET,'00000') AS REF_NO, IMST.NAME AS ASSETS_TYPE,IH.ITEM_TYPE, MRR_NO, MRR_DATE, " & vbCrLf _
            & " PV_NO, PV_DATE, BILL_NO, BILL_DATE, SUPP_CUST_NAME, " & vbCrLf _
            & " IH.REMARKS,IH.STATUS " & vbCrLf _
            & " FROM AST_ASSET_TRN IH, FIN_INVTYPE_MST IMST " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=IMST.COMPANY_CODE AND IH.GROUP_CODE=IMST.CODE AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " ORDER BY AUTO_KEY_ASSET"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmAssetsEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmAssetsEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        xMyMenu = myMenu
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(11595)

        cboStatus.Items.Clear()
        cboStatus.Items.Add("OPEN/ACTIVE")
        cboStatus.Items.Add("TRANSFER/SALE")
        cboStatus.Items.Add("SCRAP")
        cboStatus.Items.Add("CLOSE/INACTIVE")
        cboStatus.SelectedIndex = 0

        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()
        On Error GoTo ClearErr

        txtAssetSNo.Text = ""
        txtAssetSNo.Enabled = True
        cmdSearchAssetCode.Enabled = True
        txtAssetType.Text = ""
        txtItemType.Text = ""
        txtMRRNo.Text = ""
        txtMRRDate.Text = ""
        txtPVNo.Text = ""
        txtPVDate.Text = ""
        txtBillNo.Text = ""
        txtBillDate.Text = ""
        txtSupplierName.Text = ""
        txtItemDesc.Text = ""
        txtInstallDate.Text = ""
        txtPutDate.Text = ""
        txtItemValue.Text = ""
        txtCDAmount.Text = ""
        txtInstallAmount.Text = ""
        txtModvatAmount.Text = ""
        txtCGSTAmount.Text = ""
        txtSGSTAmount.Text = ""
        txtIGSTAmount.Text = ""

        txtSalvageAmount.Text = ""
        txtSTRefund.Text = ""
        txtTotalCost.Text = ""
        txtModvatPer.Text = ""
        txtLocation.Text = ""
        cboStatus.SelectedIndex = 0

        txtRemarks.Text = ""
        lblAssetCode.Text = ""

        lblBookType.Text = ""
        lblVMKey.Text = ""

        txtMRRNo.Enabled = True
        txtMRRDate.Enabled = True
        txtPVNo.Enabled = True
        txtPVDate.Enabled = True
        txtBillNo.Enabled = True
        txtBillDate.Enabled = True
        txtSupplierName.Enabled = True

        txtDrCrAmount.Text = ""

        fraSale.Enabled = True
        SSTab1.SelectedIndex = 0

        FillHeading()
        FormatSprd(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsAssetTRN, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub
    Private Sub FormatSprdView()
        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 1)
            .set_ColWidth(1, 500 * 1.5)
            .set_ColWidth(2, 500 * 8)
            .set_ColWidth(3, 500 * 5)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 3)
            .set_ColWidth(8, 500 * 3)
            .set_ColWidth(9, 500 * 3)
            .set_ColWidth(10, 500 * 8)
            .set_ColWidth(11, 500 * 3)
            .set_ColWidth(12, 500 * 3)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtAssetSNo.MaxLength = RsAssetTRN.Fields("AUTO_KEY_ASSET").Precision
        txtAssetType.MaxLength = MainClass.SetMaxLength("NAME", "FIN_INVTYPE_MST", PubDBCn)
        txtItemType.MaxLength = MainClass.SetMaxLength("NAME", "FIN_ITEMTYPE_MST", PubDBCn)

        txtMRRNo.MaxLength = RsAssetTRN.Fields("MRR_NO").Precision
        txtMRRDate.MaxLength = 10
        txtPVNo.MaxLength = RsAssetTRN.Fields("PV_NO").DefinedSize
        txtPVDate.MaxLength = 10
        txtBillNo.MaxLength = RsAssetTRN.Fields("BILL_NO").DefinedSize
        txtBillDate.MaxLength = 10
        txtSupplierName.MaxLength = RsAssetTRN.Fields("SUPP_CUST_NAME").DefinedSize
        txtItemDesc.MaxLength = RsAssetTRN.Fields("ITEM_DESC").DefinedSize
        txtInstallDate.MaxLength = 10
        txtPutDate.MaxLength = 10
        txtItemValue.MaxLength = RsAssetTRN.Fields("ITEM_VALUE").Precision
        txtCDAmount.MaxLength = RsAssetTRN.Fields("CD_AMOUNT").Precision
        txtInstallAmount.MaxLength = RsAssetTRN.Fields("OTH_AMOUNT").Precision
        txtModvatAmount.MaxLength = RsAssetTRN.Fields("MODVAT_AMOUNT").Precision

        txtCGSTAmount.MaxLength = RsAssetTRN.Fields("CGST_CLAIMAMOUNT").Precision
        txtSGSTAmount.MaxLength = RsAssetTRN.Fields("SGST_CLAIMAMOUNT").Precision
        txtIGSTAmount.MaxLength = RsAssetTRN.Fields("IGST_CLAIMAMOUNT").Precision

        txtSalvageAmount.MaxLength = RsAssetTRN.Fields("SALVAGE_AMT").Precision
        txtSTRefund.MaxLength = RsAssetTRN.Fields("SALETAX_REFUND").Precision
        txtTotalCost.MaxLength = RsAssetTRN.Fields("TOTAL_COST").Precision
        txtModvatPer.MaxLength = RsAssetTRN.Fields("MODVAT_DUR_YEAR_PER").Precision
        txtLocation.MaxLength = RsAssetTRN.Fields("LOCATION").Precision

        txtDrCrAmount.MaxLength = RsAssetTRN.Fields("DN_CR_AMOUNT").Precision
        txtRemarks.MaxLength = RsAssetTRN.Fields("REMARKS").Precision


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        Dim cntRow As Integer

        FieldsVarification = True

        '    If ValidateBranchLocking(txtPVDate.Text) = True Then	
        '        FieldsVarification = False	
        '        Exit Function	
        '    End If	


        '    If ValidateBookLocking(PubDBCn, ConLockAssetEntry, TxtVDate) = True Then	
        '        FieldsVerification = False	
        '        Exit Function	
        '    End If	

        '    If CVDate(txtPVDate.Text) < CVDate("01/04/2007") Then	
        '        MsgInformation "Upto Last Year Locked"	
        '        FieldsVarification = False	
        '        Exit Function	
        '    End If	
        '	
        '    If CVDate(txtPutDate.Text) < CVDate("01/04/2007") Then	
        '        MsgInformation "Upto Last Year Locked"	
        '        FieldsVarification = False	
        '        Exit Function	
        '    End If	



        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If

        If MODIFYMode = True And RsAssetTRN.EOF = True Then Exit Function

        If MODIFYMode = True Then
            If Trim(txtAssetSNo.Text) = "" Then
                MsgInformation("Asset S.No. is empty, So unable to save.")
                txtAssetType.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtAssetType.Text) = "" Then
            MsgInformation("Asset Type is empty, So unable to save.")
            txtAssetType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtAssetType.Text, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P' AND ISFIXASSETS='Y'") = False Then
            MsgInformation("Invalid Asset Type, So unable to save.")
            txtAssetType.Focus()
            FieldsVarification = False
            Exit Function
        End If


        '    If Trim(txtItemType.Text) = "" Then	
        '        MsgInformation "Category is empty, So unable to save."	
        '        txtItemType.SetFocus	
        '        FieldsVarification = False	
        '        Exit Function	
        '    End If	

        If Trim(txtLocation.Text) = "" Then
            MsgInformation("Location is empty, So unable to save.")
            txtLocation.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtMRRNo.Text) = "" Then
            MsgInformation("MRR No is empty, So unable to save.")
            txtMRRNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtMRRDate.Text) = "" Then
            MsgInformation("MRR Date is empty, So unable to save.")
            txtMRRDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPutDate.Text) = "" Then
            With SprdSale
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColBillDate
                    If Trim(.Text) <> "" Then
                        MsgInformation("Put to Use Date Cann't be Blank Before Sale, So unable to save.")
                        FieldsVarification = False
                        Exit Function
                    End If
                Next
            End With
        Else
            With SprdSale
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColBillDate
                    If Trim(.Text) <> "" Then
                        If CDate(txtPutDate.Text) > CDate(.Text) Then
                            MsgInformation("Sale Date Cann't be Less Than Put to Use Date, So unable to save.")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                Next
            End With
        End If


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmAssetsEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        'RsAssetTRN.Close()
        'RsAssetTRN = Nothing
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub SprdSale_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdSale.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Sub SprdSale_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdSale.LeaveCell
        '    MainClass.SaveStatus Me, ADDMode, MODIFYMode	
    End Sub


    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtAssetSNo.Text = SprdView.Text

        txtAssetSNo_Validating(txtAssetSNo, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim mGroupName As String
        Dim mGroupCode As String
        Dim mCancelled As String

        If Not RsAssetTRN.EOF Then
            lblAssetCode.Text = IIf(IsDBNull(RsAssetTRN.Fields("AUTO_KEY_ASSET").Value), "", RsAssetTRN.Fields("AUTO_KEY_ASSET").Value)
            txtAssetSNo.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("AUTO_KEY_ASSET").Value), "", RsAssetTRN.Fields("AUTO_KEY_ASSET").Value), "00000")

            mGroupCode = IIf(IsDBNull(RsAssetTRN.Fields("GROUP_CODE").Value), "", RsAssetTRN.Fields("GROUP_CODE").Value)
            If MainClass.ValidateWithMasterTable(mGroupCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                mGroupName = MasterNo
            Else
                mGroupName = ""
            End If


            txtAssetType.Text = mGroupName
            txtItemType.Text = IIf(IsDBNull(RsAssetTRN.Fields("ITEM_TYPE").Value), "", RsAssetTRN.Fields("ITEM_TYPE").Value)

            txtMRRNo.Text = IIf(IsDBNull(RsAssetTRN.Fields("MRR_NO").Value), "", RsAssetTRN.Fields("MRR_NO").Value)
            txtMRRDate.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("MRR_DATE").Value), "", RsAssetTRN.Fields("MRR_DATE").Value), "DD/MM/YYYY")
            txtPVNo.Text = IIf(IsDBNull(RsAssetTRN.Fields("PV_NO").Value), "", RsAssetTRN.Fields("PV_NO").Value)
            txtPVDate.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("PV_DATE").Value), "", RsAssetTRN.Fields("PV_DATE").Value), "DD/MM/YYYY")
            txtBillNo.Text = IIf(IsDBNull(RsAssetTRN.Fields("BILL_NO").Value), "", RsAssetTRN.Fields("BILL_NO").Value)
            txtBillDate.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("BILL_DATE").Value), "", RsAssetTRN.Fields("BILL_DATE").Value), "DD/MM/YYYY")
            txtSupplierName.Text = IIf(IsDBNull(RsAssetTRN.Fields("SUPP_CUST_NAME").Value), "", RsAssetTRN.Fields("SUPP_CUST_NAME").Value)
            txtItemDesc.Text = IIf(IsDBNull(RsAssetTRN.Fields("ITEM_DESC").Value), "", RsAssetTRN.Fields("ITEM_DESC").Value)
            txtInstallDate.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("INSTALL_DATE").Value), "", RsAssetTRN.Fields("INSTALL_DATE").Value), "DD/MM/YYYY")
            txtPutDate.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("PUT_DATE").Value), "", RsAssetTRN.Fields("PUT_DATE").Value), "DD/MM/YYYY")
            txtItemValue.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("ITEM_VALUE").Value), 0, RsAssetTRN.Fields("ITEM_VALUE").Value), "0.00")
            txtCDAmount.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("CD_AMOUNT").Value), 0, RsAssetTRN.Fields("CD_AMOUNT").Value), "0.00")
            txtInstallAmount.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("OTH_AMOUNT").Value), 0, RsAssetTRN.Fields("OTH_AMOUNT").Value), "0.00")
            txtModvatAmount.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("MODVAT_AMOUNT").Value), 0, RsAssetTRN.Fields("MODVAT_AMOUNT").Value), "0.00")

            txtCGSTAmount.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("CGST_CLAIMAMOUNT").Value), 0, RsAssetTRN.Fields("CGST_CLAIMAMOUNT").Value), "0.00")
            txtSGSTAmount.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SGST_CLAIMAMOUNT").Value), 0, RsAssetTRN.Fields("SGST_CLAIMAMOUNT").Value), "0.00")
            txtIGSTAmount.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("IGST_CLAIMAMOUNT").Value), 0, RsAssetTRN.Fields("IGST_CLAIMAMOUNT").Value), "0.00")


            txtSalvageAmount.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALVAGE_AMT").Value), 0, RsAssetTRN.Fields("SALVAGE_AMT").Value), "0.00")
            txtSTRefund.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALETAX_REFUND").Value), 0, RsAssetTRN.Fields("SALETAX_REFUND").Value), "0.00")
            txtTotalCost.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("TOTAL_COST").Value), 0, RsAssetTRN.Fields("TOTAL_COST").Value), "0.00")
            txtModvatPer.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("MODVAT_DUR_YEAR_PER").Value), 0, RsAssetTRN.Fields("MODVAT_DUR_YEAR_PER").Value), "0.00")
            txtLocation.Text = IIf(IsDBNull(RsAssetTRN.Fields("LOCATION").Value), "", RsAssetTRN.Fields("LOCATION").Value)

            txtRemarks.Text = IIf(IsDBNull(RsAssetTRN.Fields("REMARKS").Value), "", RsAssetTRN.Fields("REMARKS").Value)
            mCancelled = IIf(IsDBNull(RsAssetTRN.Fields("CANCELLED").Value), "N", RsAssetTRN.Fields("CANCELLED").Value)
            chkCancelled.CheckState = IIf(mCancelled = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

            If RsAssetTRN.Fields("Status").Value = "O" Then
                cboStatus.Text = "OPEN/ACTIVE"
            ElseIf RsAssetTRN.Fields("Status").Value = "T" Then
                cboStatus.Text = "TRANSFER/SALE"
                fraSale.Enabled = False
            ElseIf RsAssetTRN.Fields("Status").Value = "S" Then
                cboStatus.Text = "SCRAP"
            ElseIf RsAssetTRN.Fields("Status").Value = "C" Then
                cboStatus.Text = "CLOSE/INACTIVE"
            End If

            txtDrCrAmount.Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("DN_CR_AMOUNT").Value), 0, RsAssetTRN.Fields("DN_CR_AMOUNT").Value), "0.00") ''GetDnCnAmount(txtSupplierName.Text, txtBillNo.Text, txtBillDate.Text)	
            lblBookType.Text = IIf(IsDBNull(RsAssetTRN.Fields("BookType").Value), "", RsAssetTRN.Fields("BookType").Value)
            lblVMKey.Text = IIf(IsDBNull(RsAssetTRN.Fields("VMkey").Value), "", RsAssetTRN.Fields("VMkey").Value)

            With SprdSale
                .Row = 1

                .Col = ColBillNo
                .Text = IIf(IsDBNull(RsAssetTRN.Fields("SALE_BILL_NO").Value), "", RsAssetTRN.Fields("SALE_BILL_NO").Value)

                .Col = ColBillDate
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALE_BILL_DATE").Value), "", RsAssetTRN.Fields("SALE_BILL_DATE").Value), "DD/MM/YYYY")

                .Col = ColOriginalCost
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("ORIGINAL_COST").Value), 0, RsAssetTRN.Fields("ORIGINAL_COST").Value), "0.00")

                .Col = ColSupplier
                .Text = IIf(IsDBNull(RsAssetTRN.Fields("SALE_PARTY_NAME").Value), "", RsAssetTRN.Fields("SALE_PARTY_NAME").Value)

                .Col = ColSaleAmount
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALE_AMOUNT").Value), 0, RsAssetTRN.Fields("SALE_AMOUNT").Value), "0.00")

                .Row = 2

                .Col = ColBillNo
                .Text = IIf(IsDBNull(RsAssetTRN.Fields("SALE_BILL_NO1").Value), "", RsAssetTRN.Fields("SALE_BILL_NO1").Value)

                .Col = ColBillDate
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALE_BILL_DATE1").Value), "", RsAssetTRN.Fields("SALE_BILL_DATE1").Value), "DD/MM/YYYY")

                .Col = ColOriginalCost
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("ORIGINAL_COST1").Value), 0, RsAssetTRN.Fields("ORIGINAL_COST1").Value), "0.00")

                .Col = ColSupplier
                .Text = IIf(IsDBNull(RsAssetTRN.Fields("SALE_PARTY_NAME1").Value), "", RsAssetTRN.Fields("SALE_PARTY_NAME1").Value)

                .Col = ColSaleAmount
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALE_AMOUNT1").Value), 0, RsAssetTRN.Fields("SALE_AMOUNT1").Value), "0.00")

                .Row = 3

                .Col = ColBillNo
                .Text = IIf(IsDBNull(RsAssetTRN.Fields("SALE_BILL_NO2").Value), "", RsAssetTRN.Fields("SALE_BILL_NO2").Value)

                .Col = ColBillDate
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALE_BILL_DATE2").Value), "", RsAssetTRN.Fields("SALE_BILL_DATE2").Value), "DD/MM/YYYY")

                .Col = ColOriginalCost
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("ORIGINAL_COST2").Value), 0, RsAssetTRN.Fields("ORIGINAL_COST2").Value), "0.00")

                .Col = ColSupplier
                .Text = IIf(IsDBNull(RsAssetTRN.Fields("SALE_PARTY_NAME2").Value), "", RsAssetTRN.Fields("SALE_PARTY_NAME2").Value)

                .Col = ColSaleAmount
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALE_AMOUNT2").Value), 0, RsAssetTRN.Fields("SALE_AMOUNT2").Value), "0.00")

                .Row = 4

                .Col = ColBillNo
                .Text = IIf(IsDBNull(RsAssetTRN.Fields("SALE_BILL_NO3").Value), "", RsAssetTRN.Fields("SALE_BILL_NO3").Value)

                .Col = ColBillDate
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALE_BILL_DATE3").Value), "", RsAssetTRN.Fields("SALE_BILL_DATE3").Value), "DD/MM/YYYY")

                .Col = ColOriginalCost
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("ORIGINAL_COST3").Value), 0, RsAssetTRN.Fields("ORIGINAL_COST3").Value), "0.00")

                .Col = ColSupplier
                .Text = IIf(IsDBNull(RsAssetTRN.Fields("SALE_PARTY_NAME3").Value), "", RsAssetTRN.Fields("SALE_PARTY_NAME3").Value)

                .Col = ColSaleAmount
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALE_AMOUNT3").Value), 0, RsAssetTRN.Fields("SALE_AMOUNT3").Value), "0.00")

                .Row = 5

                .Col = ColBillNo
                .Text = IIf(IsDBNull(RsAssetTRN.Fields("SALE_BILL_NO4").Value), "", RsAssetTRN.Fields("SALE_BILL_NO4").Value)

                .Col = ColBillDate
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALE_BILL_DATE4").Value), "", RsAssetTRN.Fields("SALE_BILL_DATE4").Value), "DD/MM/YYYY")

                .Col = ColOriginalCost
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("ORIGINAL_COST4").Value), 0, RsAssetTRN.Fields("ORIGINAL_COST4").Value), "0.00")

                .Col = ColSupplier
                .Text = IIf(IsDBNull(RsAssetTRN.Fields("SALE_PARTY_NAME4").Value), "", RsAssetTRN.Fields("SALE_PARTY_NAME4").Value)

                .Col = ColSaleAmount
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALE_AMOUNT4").Value), 0, RsAssetTRN.Fields("SALE_AMOUNT4").Value), "0.00")

                .Row = 6

                .Col = ColBillNo
                .Text = IIf(IsDBNull(RsAssetTRN.Fields("SALE_BILL_NO5").Value), "", RsAssetTRN.Fields("SALE_BILL_NO5").Value)

                .Col = ColBillDate
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALE_BILL_DATE5").Value), "", RsAssetTRN.Fields("SALE_BILL_DATE5").Value), "DD/MM/YYYY")

                .Col = ColOriginalCost
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("ORIGINAL_COST5").Value), 0, RsAssetTRN.Fields("ORIGINAL_COST5").Value), "0.00")

                .Col = ColSupplier
                .Text = IIf(IsDBNull(RsAssetTRN.Fields("SALE_PARTY_NAME5").Value), "", RsAssetTRN.Fields("SALE_PARTY_NAME5").Value)

                .Col = ColSaleAmount
                .Text = VB6.Format(IIf(IsDBNull(RsAssetTRN.Fields("SALE_AMOUNT5").Value), 0, RsAssetTRN.Fields("SALE_AMOUNT5").Value), "0.00")

            End With


            'mSqlStr = "SELECT ID.ITEM_CODE,ID.ITEM_QTY,ID.ITEM_UOM,ID.ITEM_RATE,ID.ITEM_AMT,IMST.ITEM_SHORT_DESC  " & vbCrLf _
            '    & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, INV_ITEM_MST IMST " & vbCrLf _
            '    & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            '    & " AND IH.COMPANY_CODE= IMST.COMPANY_CODE" & vbCrLf _
            '    & " AND ID.ITEM_CODE= IMST.ITEM_CODE" & vbCrLf _
            '    & " AND VNO = '" & Trim(txtPVNo.Text) & "' AND VDATE = '" & Format(txtPVDate.Text, "DD/MMM/YYYY") & "'"

            'MainClass.UOpenRecordSet mSqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly

            'If RsTemp.EOF = False Then
            '        cntRow = 1
            '        Do While RsTemp.EOF = False
            '            With sprdPurchase
            '                .Row = cntRow
            '                .Col = 1
            '                .Text = IIf(IsNull(RsTemp!ITEM_CODE), "", RsTemp!ITEM_CODE)

            '                .Col = 2
            '                .Text = Trim(IIf(IsNull(RsTemp!ITEM_SHORT_DESC), "", RsTemp!ITEM_SHORT_DESC))

            '                .Col = 3
            '                .Text = IIf(IsNull(RsTemp!ITEM_UOM), "", RsTemp!ITEM_UOM)

            '                .Col = 4
            '                .Text = Format(IIf(IsNull(RsTemp!ITEM_QTY), 0, RsTemp!ITEM_QTY), "0.00")

            '                .Col = 5
            '                .Text = Format(IIf(IsNull(RsTemp!ITEM_RATE), 0, RsTemp!ITEM_RATE), "0.00")

            '                .Col = 6
            '                .Text = Format(IIf(IsNull(RsTemp!ITEM_AMT), 0, RsTemp!ITEM_AMT), "0.00")
            '            End With
            '            RsTemp.MoveNext
            '            If RsTemp.EOF = False Then
            '                cntRow = cntRow + 1
            '                sprdPurchase.MaxRows = cntRow
            '            End If
            '        Loop
            '    End If

            If Trim(txtPVDate.Text) = "" Then
                txtMRRNo.Enabled = False
                txtMRRDate.Enabled = False
                txtPVNo.Enabled = False
                txtPVDate.Enabled = False
                txtBillNo.Enabled = False
                txtBillDate.Enabled = False
                txtSupplierName.Enabled = False
            ElseIf CDate(txtPVDate.Text) < CDate("31/03/2003") Then
                txtMRRNo.Enabled = True
                txtMRRDate.Enabled = True
                txtPVNo.Enabled = True
                txtPVDate.Enabled = True
                txtBillNo.Enabled = True
                txtBillDate.Enabled = True
                txtSupplierName.Enabled = True
            Else
                txtMRRNo.Enabled = False
                txtMRRDate.Enabled = False
                txtPVNo.Enabled = False
                txtPVDate.Enabled = False
                txtBillNo.Enabled = False
                txtBillDate.Enabled = False
                txtSupplierName.Enabled = False
            End If

            If Trim(txtPVDate.Text) <> "" Then
                ShowCADepreciation()
            End If

        End If

        ADDMode = False
        MODIFYMode = False
        txtAssetSNo.Enabled = True
        cmdSearchAssetCode.Enabled = True
        '    txtAssetType.Enabled = False	
        '    cmdSearchAssetType.Enabled = False	
        MainClass.ButtonStatus(Me, XRIGHT, RsAssetTRN, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub
    Private Function GetDnCnAmount(ByRef pSuppName As String, ByRef pBillNo As String, ByRef pBillDate As String) As Double
        On Error GoTo ShowErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset


        SqlStr = "SELECT SUM(TOTAL_COST) As AMOUNT FROM AST_ASSET_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(pSuppName) & "'" & vbCrLf _
            & " AND BILL_NO='" & MainClass.AllowSingleQuote(pBillNo) & "'" & vbCrLf _
            & " AND BILL_DATE=TO_DATE('" & vb6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND BOOKTYPE IN ('E','R') AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetDnCnAmount = IIf(IsDBNull(RsTemp.Fields("AMOUNT").Value), 0, RsTemp.Fields("AMOUNT").Value)
        End If
        Exit Function
ShowErrPart:
        GetDnCnAmount = 0
        '    Resume	
    End Function
    Private Sub ShowCADepreciation()
        On Error GoTo ShowErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mMRRDate As String
        Dim mFYear As Integer
        Dim cntRow As Integer
        Dim mRow As Integer
        Dim mDeprMode As String
        Dim cntCol As Integer
        Dim pGroupCode As String
        Dim mDepreRate As Double
        Dim mModeType As String
        Dim mLastValue As Double
        Dim mInvoiceValue As Double
        Dim mCurrFYear As Integer
        Dim mDays As Integer

        mInvoiceValue = Val(txtTotalCost.Text)

        If MainClass.ValidateWithMasterTable(Trim(txtAssetType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
            pGroupCode = MasterNo
        Else
            pGroupCode = "-1"
        End If

        mMRRDate = VB6.Format(txtMRRDate.Text, "DD/MM/YYYY")

        If Trim(mMRRDate) = "" Then
            mMRRDate = VB6.Format(txtPVDate.Text, "DD/MM/YYYY")
        End If
        If mMRRDate = "" Then Exit Sub
        mFYear = GetMRRFYNO(mMRRDate)


        With SprdMain
            .MaxRows = Val(RsCompany.Fields("FYEAR").Value) - mFYear + 2
            For cntCol = ColFYear + 1 To .MaxCols
                mRow = 0
                .Row = 0
                .Col = cntCol
                mDeprMode = Trim(.Text)
                For cntRow = mFYear To RsCompany.Fields("FYEAR").Value + 1
                    mRow = mRow + 1

                    .Row = mRow
                    .Col = ColFYear
                    .Text = VB6.Format(cntRow, "0000")
                    mCurrFYear = CInt(VB6.Format(cntRow, "0000"))


                    If MainClass.ValidateWithMasterTable(Trim(mDeprMode), "MODE_CODE", "MODE_TYPE", "AST_DEPRECIATION_MODE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mModeType = MasterNo
                    Else
                        mModeType = "W"
                    End If


                    SqlStr = " SELECT * FROM AST_DEPRECIATION_MST " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND FYEAR=" & mCurrFYear & "" & vbCrLf _
                        & " AND GROUP_CODE='" & pGroupCode & "'" & vbCrLf _
                        & " AND MODE_CODE='" & mDeprMode & "'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mDepreRate = IIf(IsDBNull(RsTemp.Fields("DEPR_RATE").Value), 0, RsTemp.Fields("DEPR_RATE").Value)
                        If mModeType = "W" Then
                            If mRow = 1 Then
                                mInvoiceValue = Val(txtTotalCost.Text)
                            Else
                                .Row = mRow - 1
                                .Col = cntCol
                                mInvoiceValue = Val(.Text)
                            End If
                            If mRow = 1 And (MonthValue(mMRRDate) = 10 Or MonthValue(mMRRDate) = 11 Or MonthValue(mMRRDate) = 12 Or MonthValue(mMRRDate) = 1 Or MonthValue(mMRRDate) = 2 Or MonthValue(mMRRDate) = 3) Then
                                mLastValue = mInvoiceValue
                            Else
                                mLastValue = CDbl(VB6.Format(mInvoiceValue - (mInvoiceValue * mDepreRate * 1 / 100), "0.00"))
                            End If
                        Else
                            mInvoiceValue = Val(txtTotalCost.Text)
                            If mRow = 1 Then
                                mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mMRRDate), CDate(VB6.Format("31/03/" & mCurrFYear + 1, "DD/MM/YYYY")))
                                mLastValue = CDbl(VB6.Format(mInvoiceValue - ((mInvoiceValue * mDepreRate * mDays * 0.01) / 365), "0.00"))
                            Else
                                mLastValue = CDbl(VB6.Format(mInvoiceValue - (mInvoiceValue * mDepreRate * mRow / 100), "0.00"))
                            End If
                        End If
                    End If
                    .Row = mRow
                    .Col = cntCol
                    .Text = CStr(mLastValue)
                    mLastValue = 0
                Next

            Next
        End With


        FormatSprd(-1)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnAsset(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnAsset(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnAsset(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String


        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtAssetSNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAssetSNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAssetSNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAssetSNo.DoubleClick
        Call cmdSearchAssetCode_Click(cmdSearchAssetCode, New System.EventArgs())
    End Sub

    Private Sub txtAssetSNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAssetSNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAssetSNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAssetSNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAssetCode_Click(cmdSearchAssetCode, New System.EventArgs())
    End Sub

    Public Sub txtAssetSNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAssetSNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim xAssetCode As Double
        Dim SqlStr As String

        If Trim(txtAssetSNo.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsAssetTRN.BOF = False Then xAssetCode = RsAssetTRN.Fields("AUTO_KEY_ASSET").Value

        SqlStr = "SELECT * FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_ASSET=" & Val(txtAssetSNo.Text) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAssetTRN, ADODB.LockTypeEnum.adLockReadOnly)
        If RsAssetTRN.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Code. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then

                SqlStr = "SELECT * FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_ASSET=" & Val(CStr(xAssetCode)) & " "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAssetTRN, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchAssetCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAssetCode.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster(txtAssetSNo.Text, "AST_ASSET_TRN", "AUTO_KEY_ASSET", "GROUP_CODE", "MRR_NO", "TOTAL_COST", SqlStr) = True Then
            txtAssetSNo.Text = AcName
            If txtAssetSNo.Enabled = True Then txtAssetSNo.Focus()
        End If
    End Sub

    Private Sub txtAssetType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAssetType.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAssetType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAssetType.DoubleClick
        Call cmdSearchAssetType_Click(cmdSearchAssetType, New System.EventArgs())
    End Sub

    Private Sub txtAssetType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAssetType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtAssetType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAssetType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAssetType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAssetType_Click(cmdSearchAssetType, New System.EventArgs())
    End Sub

    Public Sub txtAssetType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAssetType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String

        If Trim(txtAssetType.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND CATEGORY='P' AND ISFIXASSETS='Y'"
        If MainClass.ValidateWithMasterTable(txtAssetType.Text, "NAME", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtAssetType.Text = MasterNo
        Else
            MsgBox("Not a valid Asset Type")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSearchAssetType_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAssetType.Click
        Dim SqlStr As String
        SqlStr = " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P' AND ISFIXASSETS='Y'"
        If MainClass.SearchGridMaster(txtAssetType.Text, "FIN_INVTYPE_MST", "NAME", "", , , SqlStr) = True Then
            txtAssetType.Text = AcName
            If txtAssetType.Enabled = True Then txtAssetType.Focus()
        End If
    End Sub
    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBillDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtBillDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtBillDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCGSTAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCGSTAmount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCGSTAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCGSTAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSGSTAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSGSTAmount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSGSTAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSGSTAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtIGSTAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIGSTAmount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIGSTAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIGSTAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDrCrAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDrCrAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemType.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemType.DoubleClick
        Call cmdSearchCategory_Click(cmdSearchCategory, New System.EventArgs())
    End Sub

    Private Sub txtItemType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCategory_Click(cmdSearchCategory, New System.EventArgs())
    End Sub

    Public Sub txtItemType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String

        If Trim(txtItemType.Text) = "" Then GoTo EventExitSub

        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""	
        '    If MainClass.ValidateWithMasterTable(txtItemType.Text, "NAME", "NAME", "FIN_ITEMTYPE_MST", PubDBCn, MasterNo, , SqlStr) = True Then	
        '        txtItemType.Text = MasterNo	
        '    Else	
        '        MsgBox "Not a valid Category"	
        '        Cancel = True	
        '    End If	
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCategory.Click
        Dim SqlStr As String
        SqlStr = " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtItemType.Text, "FIN_ITEMTYPE_MST", "NAME", "", , , SqlStr) = True Then
            txtItemType.Text = AcName
            If txtItemType.Enabled = True Then txtItemType.Focus()
        End If
    End Sub

    Private Sub txtCDAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCDAmount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCDAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCDAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInstallamount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInstallAmount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInstallAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInstallAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInstallDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInstallDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInstallDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInstallDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtInstallDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtInstallDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtItemDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemDesc.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemValue_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemValue.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemValue_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemValue.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLocation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtLocation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtModvatAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModvatAmount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtModvatAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtModvatAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtModvatPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModvatPer.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtModvatPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtModvatPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtMRRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtMRRDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtMRRDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mGroupName As String
        Dim mGroupCode As String
        Dim mSuppCode As String

        Dim xSqlStr As String
        Dim RsTempDet As ADODB.Recordset
        Dim mKey As String
        Dim mItemDesc As String
        Dim mItemCode As String
        Dim mItemName As String
        Dim mItemQty As String

        If ADDMode = False Then GoTo EventExitSub
        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub

        SqlStr = "SELECT * FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MRR_NO=" & Val(txtMRRNo.Text) & " AND BOOKTYPE='P' ANd CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAssetTRN, ADODB.LockTypeEnum.adLockReadOnly)
        If RsAssetTRN.EOF = False Then
            Clear1()
            Show1()
            GoTo EventExitSub
        End If

        SqlStr = " Select * " & vbCrLf & " FROM FIN_PURCHASE_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" & vbCrLf & " ANd ISFINALPOST='Y'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mKey = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
            mGroupCode = IIf(IsDBNull(RsTemp.Fields("TRNTYPE").Value), "", RsTemp.Fields("TRNTYPE").Value)
            If MainClass.ValidateWithMasterTable(mGroupCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                mGroupName = MasterNo
            Else
                mGroupName = ""
            End If

            txtAssetType.Text = mGroupName
            txtItemType.Text = IIf(IsDBNull(RsTemp.Fields("ITEMDESC").Value), "", RsTemp.Fields("ITEMDESC").Value)

            txtMRRNo.Text = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_MRR").Value), "", RsTemp.Fields("AUTO_KEY_MRR").Value)
            txtMRRDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("MRRDATE").Value), "", RsTemp.Fields("MRRDATE").Value), "DD/MM/YYYY")
            txtPVNo.Text = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
            txtPVDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")
            txtBillNo.Text = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
            txtBillDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")

            mSuppCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mSuppCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSupplierName.Text = MasterNo
            Else
                txtSupplierName.Text = ""
            End If



            txtItemValue.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEMVALUE").Value), 0, RsTemp.Fields("ITEMVALUE").Value), "0.00")
            txtCDAmount.Text = "0.00"
            txtInstallAmount.Text = "0.00"
            txtModvatAmount.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("MODVATAMOUNT").Value), 0, RsTemp.Fields("MODVATAMOUNT").Value), "0.00")
            txtSTRefund.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("STCLAIMAMOUNT").Value), 0, RsTemp.Fields("STCLAIMAMOUNT").Value), "0.00") & VB6.Format(IIf(IsDBNull(RsTemp.Fields("SUR_VATCLAIMAMOUNT").Value), 0, RsTemp.Fields("SUR_VATCLAIMAMOUNT").Value), "0.00")
            txtTotalCost.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value), "0.00")

            txtCGSTAmount.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TOTCGST_REFUNDAMT").Value), 0, RsTemp.Fields("TOTCGST_REFUNDAMT").Value), "0.00")
            txtSGSTAmount.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TOTSGST_REFUNDAMT").Value), 0, RsTemp.Fields("TOTSGST_REFUNDAMT").Value), "0.00")
            txtIGSTAmount.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TOTIGST_REFUNDAMT").Value), 0, RsTemp.Fields("TOTIGST_REFUNDAMT").Value), "0.00")

            txtModvatPer.Text = CStr(50)
            txtLocation.Text = "FACTORY"
            lblVMKey.Text = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
            lblBookType.Text = IIf(IsDBNull(RsTemp.Fields("BookType").Value), "", RsTemp.Fields("BookType").Value)

            xSqlStr = " Select * " & vbCrLf & " FROM FIN_PURCHASE_DET" & vbCrLf & " WHERE MKEY='" & mKey & "'"

            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTempDet.EOF = False Then
                mItemDesc = ""
                Do While RsTempDet.EOF = False
                    mItemCode = IIf(IsDBNull(RsTempDet.Fields("ITEM_CODE").Value), "", RsTempDet.Fields("ITEM_CODE").Value)
                    mItemQty = IIf(IsDBNull(RsTempDet.Fields("ITEM_QTY").Value), "", RsTempDet.Fields("ITEM_QTY").Value) & " " & IIf(IsDBNull(RsTempDet.Fields("ITEM_UOM").Value), "", RsTempDet.Fields("ITEM_UOM").Value)
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemName = MasterNo
                    Else
                        mItemName = ""
                    End If
                    mItemDesc = IIf(mItemDesc = "", "", mItemDesc & ", ") & mItemName & " (" & mItemQty & ")"
                    RsTempDet.MoveNext()
                Loop
            End If
            txtItemDesc.Text = VB.Left(mItemDesc, 250)
            MainClass.ClearGrid(SprdSale)
        Else
            '        Clear1	
            '        MsgBox "Invalid MRR No."	
            '        Cancel = True	
        End If
        GoTo EventExitSub
ERR1:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPutDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPutDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPutDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPutDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPutDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtPutDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtPVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPVDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPVDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtPVDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtPVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPVNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPVNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub




    Private Sub txtSalvageAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSalvageAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSTRefund_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTRefund.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSTRefund_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTRefund.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSupplierName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSupplierName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplierName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplierName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTotalCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotalCost.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotalCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
