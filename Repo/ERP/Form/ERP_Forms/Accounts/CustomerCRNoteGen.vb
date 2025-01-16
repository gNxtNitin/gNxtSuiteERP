Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCustomerCRNoteGen
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim mAccountCode As String
    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColUnit As Short = 3
    Private Const ColHSNCode As Short = 4
    Private Const ColBillNo As Short = 5
    Private Const ColAutoInvoiceNo As Short = 6
    Private Const ColBillDate As Short = 7
    Private Const ColQuantity As Short = 8
    Private Const ColAcctQuantity As Short = 9
    Private Const ColRate As Short = 10
    Private Const ColDNCNRate As Short = 11
    Private Const ColSuppRate As Short = 12
    Private Const ColNetRate As Short = 13
    Private Const ColPORate As Short = 14
    Private Const ColDiff As Short = 15
    Private Const ColDiffAmount As Short = 16
    Private Const ColCGSTPer As Short = 17
    Private Const ColCGSTAmount As Short = 18
    Private Const ColSGSTPer As Short = 19
    Private Const ColSGSTAmount As Short = 20
    Private Const ColIGSTPer As Short = 21
    Private Const ColIGSTAmount As Short = 22
    Private Const ColFyear As Short = 23
    Private Const ColDiv As Short = 24
    Private Const ColSONo As Short = 25
    Private Const ColSODate As Short = 26
    Private Const ColCustRefNo As Short = 27
    Private Const ColCustRefDate As Short = 28
    Private Const ColMark As Short = 29
    Private Const ColMKEY As Short = 30
    Private Const ColStatus As Short = 31
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
        cmdSave.Enabled = pPrintEnable
    End Sub
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboInvType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvType.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub chkAllItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItem.CheckStateChanged
        Call PrintStatus(False)
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemName.Enabled = False
            cmdsearchItem.Enabled = False
        Else
            txtItemName.Enabled = True
            cmdsearchItem.Enabled = True
        End If
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub ReportonShow(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Report1.Reset()
        mTitle = "Item Wise - Bill Wise Detail"
        If Trim(cboDivision.Text) <> "ALL" Then
            mTitle = mTitle & "[" & cboDivision.Text & "]"
        End If
        mSubTitle = "VDate From : " & txtDateFrom.Text & " To : " & txtDateTo.Text
        mTitle = mTitle & "-Detailed"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemWiseBillWise.RPT"
        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, ColDiff, PubDBCn) = False Then GoTo ReportErr
        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        '    SqlStr = MakeSQL
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mInvoiceNo As String
        Dim mMark As String
        Dim mItemType As String
        Dim mTRNType As String
        Dim mSuppCustCode As String
        Dim mAccountCode As String
        Dim mBookSubType As String
        Dim mAcceptedQty As Double
        Dim mLockBookCode As Integer
        If MainClass.ChkIsdateF(txtVDate) = False Then
            txtVDate.Focus()
            MsgBox("Invalid date", MsgBoxStyle.Information)
            Exit Sub
        End If
        If FYChk(CStr(CDate(txtVDate.Text))) = False Then
            txtVDate.Focus()
            MsgBox("Date is not is Current FY.", MsgBoxStyle.Information)
            Exit Sub
        End If
        If ValidateBranchLocking((txtVDate.Text)) = True Then
            Exit Sub
        End If
        mLockBookCode = CInt(ConLockPurchase)
        If ValidateBookLocking(PubDBCn, mLockBookCode, txtVDate.Text) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, txtVDate.Text, (TxtAccount.Text)) = True Then
            Exit Sub
        End If
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mTRNType = MasterNo
        Else
            mTRNType = CStr(-1)
            MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
            Exit Sub
        End If
        mSuppCustCode = "-1"
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            Exit Sub
        End If
        '*********
        mAccountCode = "-1"
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "ACCOUNTPOSTCODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        End If
        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Account Code Does Not Exist In Master", MsgBoxStyle.Information)
            Exit Sub
        End If
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
            mBookSubType = MasterNo
        Else
            mBookSubType = "-1"
        End If
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "ITEMTYPE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mItemType = MasterNo
        End If
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColMark
                mMark = Trim(.Text)
                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) And mMark = "" Then
                    .Col = ColBillNo
                    mBillNo = Trim(.Text)
                    .Col = ColBillDate
                    mBillDate = VB6.Format(.Text, "DD/MM/YYYY")
                    .Col = ColAutoInvoiceNo
                    mInvoiceNo = Trim(.Text)
                    .Col = ColAcctQuantity
                    mAcceptedQty = Val(.Text)
                    If mAcceptedQty > 0 Then
                        If Val(mInvoiceNo) <> 0 Then
                            If UpdateMain1(mInvoiceNo, mBillNo, mBillDate, mSuppCustCode, mAccountCode, mBookSubType, mItemType, mTRNType) = False Then GoTo ErrPart
                        End If
                    End If
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        Call PrintStatus(False)
        MsgInformation("Record Saved.")
        Exit Sub
ErrPart:
        MsgInformation("Record Not Save.")
        PubDBCn.RollbackTrans()
    End Sub
    Private Function UpdateMain1(ByRef pInvoiceNo As String, ByRef mBillNo As String, ByRef mBillDate As String, ByRef mSuppCustCode As String, ByRef mAccountCode As String, ByRef mBookSubType As String, ByRef mItemType As String, ByRef mTRNType As String) As Boolean
        On Error GoTo ErrPart
        'Dim I As Integer
        Dim SqlStr As String
        Dim nMkey As String
        '
        Dim mVNoSeq As Integer
        Dim mVNo As String
        Dim mVDate As String
        Dim mFREIGHTCHARGES As String
        Dim mItemValue As Double
        Dim mTOTSTAMT As Double
        Dim mTOTCHARGES As Double
        Dim mTotEDAmount As Double
        Dim mTotEDUAmount As Double
        Dim mTOTEXPAMT As Double
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mLSTCST As String
        Dim mWITHFORM As String
        Dim mPRINTED As String
        Dim mCancelled As String
        Dim mIsRegdNo As String
        Dim mSTType As String
        Dim mSTPERCENT As Double
        Dim mTOTFREIGHT As Double
        Dim mEDPERCENT As Double
        Dim mEDUPERCENT As Double
        Dim mTOTTAXABLEAMOUNT As Double
        Dim mBookType As String
        'Dim mSRBillNo As String
        'Dim mSRBillDate As String
        '
        Dim mRO As Double
        Dim mSURAmount As Double
        Dim mMSC As Double
        Dim mTotDiscount As Double
        Dim mREJECTION As String
        Dim pDueDate As String
        Dim mModvatNo As Integer
        Dim mSERVNo As Integer
        Dim mSTCLAIMNo As Integer
        Dim mCapital As String
        Dim mNarration As String
        '
        Dim mISMODVAT As String
        Dim mIsServClaim As String
        Dim mISSTREFUND As String
        Dim mFinalPost As String
        '
        'Dim mItemType As String
        Dim cntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim xDebitAmt As Double
        Dim xPORate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xRateDiffCN As Double
        Dim mISTDSDEDUCT As String
        Dim mISESIDEDUCT As String
        Dim mISSTDSDEDUCT As String
        Dim mPDIRItem As Integer
        Dim PDIRAmount As Double
        Dim mDNCNCreated As Boolean
        Dim xExpDiffDN As Boolean
        Dim mFormRecdCode As Integer
        Dim mFormDueCode As Integer
        Dim mISFOC As String
        Dim mIsSuppBill As String
        Dim mServTax_Repost As String
        Dim mApproved As String
        '
        Dim pJVVnoStr As String
        Dim pVType As String
        Dim pJVNo As String
        Dim pJVMKey As String
        Dim pRowNo As Integer
        Dim mDivisionCode As Double
        Dim mCurRowNo As Integer
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mMark As String
        Dim mGSTApp As String
        mItemValue = 0
        mTOTSTAMT = 0
        mTOTCHARGES = 0
        mTotEDAmount = 0
        mFormRecdCode = -1
        mFormDueCode = -1
        mFinalPost = "Y"
        mTOTEXPAMT = 0
        mCGSTAmount = 0
        mSGSTAmount = 0
        mIGSTAmount = 0
        mSTPERCENT = 0
        mTOTFREIGHT = 0
        mEDPERCENT = 0
        mEDUPERCENT = 0
        mRO = 0
        mTotDiscount = 0
        mSURAmount = 0
        mMSC = 0
        mTotQty = 0
        mLSTCST = ""
        mWITHFORM = ""
        mPRINTED = "N"
        mCancelled = "N"
        mIsRegdNo = "N"
        mIsSuppBill = "N"
        mSTType = "0"
        pJVVnoStr = ""
        pJVMKey = ""
        mGSTApp = IIf(optGSTApp(0).Checked = True, "Y", "N")
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColMark
                mMark = Trim(.Text)
                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) And mMark = "" Then
                    .Col = ColAutoInvoiceNo
                    If Trim(pInvoiceNo) = Trim(.Text) Then
                        .Col = ColDiv
                        mDivisionCode = Val(.Text)
                        .Col = ColAcctQuantity
                        mTotQty = mTotQty + System.Math.Abs(Val(.Text))
                        .Col = ColDiffAmount
                        mItemValue = mItemValue + System.Math.Abs(Val(.Text))
                        If optGSTApp(0).Checked = True Then
                            .Col = ColCGSTAmount
                            mCGSTAmount = mCGSTAmount + System.Math.Abs(Val(.Text))
                            .Col = ColSGSTAmount
                            mSGSTAmount = mSGSTAmount + System.Math.Abs(Val(.Text))
                            .Col = ColIGSTAmount
                            mIGSTAmount = mIGSTAmount + System.Math.Abs(Val(.Text))
                        End If
                    End If
                End If
            Next
        End With
        mNETVALUE = mItemValue + mCGSTAmount + mSGSTAmount + mIGSTAmount
        mTOTTAXABLEAMOUNT = Val(CStr(mItemValue))
        If mNETVALUE > 0 Then
            mVNoSeq = CInt(AutoGenSeqBillNo())
            mVNo = VB.Left(ConSaleDebit, 1) & VB6.Format(Val(CStr(mVNoSeq)), "0000000")
            mVDate = VB6.Format(txtVDate.Text, "DD/MM/YYYY") ''PubCurrDate
            mNarration = "Rates Revised wide PO NO " & txtPONo.Text & "/" & txtPOAmendNo.Text
            mBookType = VB.Left(ConSaleDebit, 1)
            SqlStr = ""
            mCurRowNo = MainClass.AutoGenRowNo("FIN_SUPP_PUR_HDR", "RowNo", PubDBCn)
            nMkey = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value) & RsCompany.Fields("FYEAR").Value & mCurRowNo
            SqlStr = "INSERT INTO FIN_SUPP_SALE_HDR( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " ROWNO, TRNTYPE, VNOSEQ, " & vbCrLf & " VNO, VDATE, BILLNO, " & vbCrLf & " INVOICE_DATE, AUTO_KEY_SO, SO_DATE, " & vbCrLf & " AMEND_NO, SO_WEFDATE, SUPP_CUST_CODE, " & vbCrLf & " ACCOUNTCODE, TARIFFHEADING, BOOKTYPE, " & vbCrLf & " BOOKSUBTYPE, REMARKS, ITEMDESC, " & vbCrLf & " ITEMVALUE, STPERCENT, TOTSTAMT, " & vbCrLf & " TOTFREIGHT, TOTCHARGES, EDPERCENT, " & vbCrLf & " TOTEDAMOUNT, TOTSURCHARGEAMT, TOTDISCAMOUNT, " & vbCrLf & " TOTMSCAMOUNT, TOTRO, TOTEXPAMT," & vbCrLf & " TOTTAXABLEAMOUNT, NETVALUE, TOTQTY," & vbCrLf & " STTYPE, STFORMCODE, STFORMNAME, "
            SqlStr = SqlStr & vbCrLf & " STFORMDATE, STDUEFORMCODE, STDUEFORMNAME, " & vbCrLf & " STDUEFORMDATE, ISREGDNO, LSTCST, " & vbCrLf & " WITHFORM, CANCELLED, NARRATION," & vbCrLf & " JVNO, JVMKEY, ISFINALPOST, " & vbCrLf & " PAYMENTDATE, TOTEDUPERCENT, TOTEDUAMOUNT, " & vbCrLf & " CESSABLEAMOUNT, TO_DATE, SHECPERCENT," & vbCrLf & " SHECAMOUNT, " & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE,DIV_CODE, " & vbCrLf & " TOTCGST_AMOUNT, TOTSGST_AMOUNT, TOTIGST_AMOUNT,GST_APP,REASON "
            SqlStr = SqlStr & vbCrLf & " ) VALUES ( " & vbCrLf & " '" & nMkey & "', " & RsCompany.Fields("Company_Code").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mCurRowNo & ", " & Val(mTRNType) & ", " & mVNoSeq & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mVNo) & "', TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mBillNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtPONo.Text) & ", TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & "  " & Val(txtPOAmendNo.Text) & ", TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mSuppCustCode & "'," & vbCrLf & "  '" & mAccountCode & "', '', '" & mBookType & "'," & vbCrLf & "  '" & mBookSubType & "', '', '" & MainClass.AllowSingleQuote(mItemType) & "'," & vbCrLf & "  " & mItemValue & ", " & mSTPERCENT & ", " & mTOTSTAMT & "," & vbCrLf & "  " & mTOTFREIGHT & ", " & mTOTCHARGES & ", " & mEDPERCENT & ", " & vbCrLf & "  " & mTotEDAmount & ", " & mSURAmount & ", " & mTotDiscount & "," & vbCrLf & "  " & mMSC & ", " & mRO & ", " & mTOTEXPAMT & "," & vbCrLf & "  " & mTOTTAXABLEAMOUNT & ", " & mNETVALUE & ", " & mTotQty & "," & vbCrLf & "  '" & mSTType & "', " & mFormRecdCode & ", '',"
            SqlStr = SqlStr & vbCrLf & "  '', " & mFormDueCode & ",'', " & vbCrLf & " '', '" & mIsRegdNo & "', '" & mLSTCST & "'," & vbCrLf & " '" & mWITHFORM & "', '" & mCancelled & "', '" & MainClass.AllowSingleQuote(mNarration) & "'," & vbCrLf & " '" & pJVVnoStr & "', '" & pJVMKey & "', '" & mFinalPost & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') ,0,0,  " & vbCrLf & " 0, TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 0," & vbCrLf & " 0, " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " '', '', " & mDivisionCode & "," & vbCrLf & " " & Val(CStr(mCGSTAmount)) & ", " & Val(CStr(mSGSTAmount)) & "," & Val(CStr(mIGSTAmount)) & ", " & vbCrLf & " '" & mGSTApp & "','1') "
            PubDBCn.Execute(SqlStr)
            If UpdateDetail1(nMkey, pInvoiceNo, mNarration, mVNo, mVDate, mSuppCustCode, mAccountCode, mDivisionCode, mBookType, mBookSubType, mBillNo, mBillDate, mTOTTAXABLEAMOUNT, mCGSTAmount, mSGSTAmount, mIGSTAmount) = False Then GoTo ErrPart
            If SalePostTRN_GST(PubDBCn, nMkey, mCurRowNo, CStr(ConSaleDebitBookCode), mBookType, mBookSubType, mVNo, mVDate, mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mNETVALUE)), False, pDueDate, False, "", False, "", 0, Val(CStr(mTOTEXPAMT)), Val(CStr(mCGSTAmount)), Val(CStr(mIGSTAmount)), Val(CStr(mSGSTAmount)), True, PubUserID, VB6.Format(PubCurrDate, "DD-MMM-YYYY"), Val(CStr(mItemValue)), mDivisionCode, CStr(0), 0, 0, 0, mBillNo, mBillDate) = False Then GoTo ErrPart
        End If
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .Col = ColAutoInvoiceNo
                    If Trim(pInvoiceNo) = Trim(.Text) Then
                        .Col = ColMark
                        If Trim(.Text) = "" Then
                            .Text = mVNo
                        End If
                    End If
                End If
            Next
        End With
        UpdateMain1 = True
        Exit Function
ErrPart:
        UpdateMain1 = False
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '    Resume
    End Function
    Private Function UpdateDetail1(ByRef pMKey As String, ByRef pInvoiceNo As String, ByRef xNarration As String, ByRef pVNo As String, ByRef pVDate As String, ByRef pSuppCustCode As String, ByRef mDebitAccountCode As String, ByRef mDivisionCode As Double, ByRef mBookType As String, ByRef mBookSubType As String, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pItemValue As Double, ByRef pCGSTAmount As Double, ByRef pSGSTAmount As Double, ByRef pIGSTAmount As Double) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mPartNo As String
        Dim mItemDesc As String
        Dim mUnit As String
        Dim mExicseableAmt As Double
        Dim mCessableAmt As Double
        Dim mSTableAmt As Double
        Dim mCESSAmt As Double
        Dim mTotExicseableAmt As Double
        Dim mTotSTableAmt As Double
        Dim mTotCessableAmt As Double
        Dim mServiceAmt As Double
        Dim mHSNCode As String
        Dim mPurFYear As Integer
        Dim mPurMkey As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillQty As Double
        Dim mBillRate As Double
        Dim mPORate As Double
        Dim mQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mInvoiceNo As String
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mMark As String
        Dim cntRow As Integer
        mTotExicseableAmt = 0
        mTotSTableAmt = 0
        mTotCessableAmt = 0
        PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & pMKey & "' AND BookType='" & mBookType & "' AND BOOKCODE='" & ConSaleDebitBookCode & "'")
        PubDBCn.Execute("Delete From FIN_SUPP_SALE_DET Where Mkey='" & pMKey & "'")
        I = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColMark
                mMark = Trim(.Text)
                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) And mMark = "" Then
                    .Col = ColAutoInvoiceNo
                    If Trim(pInvoiceNo) = Trim(.Text) Then
                        I = I + 1
                        .Col = ColItemCode
                        mItemCode = MainClass.AllowSingleQuote(.Text)
                        mPartNo = ""
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mPartNo = MasterNo
                        End If
                        mPartNo = MainClass.AllowSingleQuote(mPartNo)
                        .Col = ColItemName
                        mItemDesc = MainClass.AllowSingleQuote(.Text)
                        xNarration = xNarration & IIf(xNarration = "", "", ", ") & mItemDesc
                        .Col = ColUnit
                        mUnit = MainClass.AllowSingleQuote(.Text)
                        SprdMain.Col = ColHSNCode
                        mHSNCode = MainClass.AllowSingleQuote(.Text)
                        .Col = ColFyear
                        mPurFYear = Val(.Text)
                        .Col = ColMKEY
                        mPurMkey = MainClass.AllowSingleQuote(.Text)
                        .Col = ColBillNo
                        mBillNo = MainClass.AllowSingleQuote(.Text)
                        .Col = ColBillDate
                        mBillDate = VB6.Format(.Text, "DD/MM/YYYY")
                        .Col = ColAutoInvoiceNo
                        mInvoiceNo = Trim(.Text)
                        .Col = ColQuantity
                        mBillQty = Val(.Text)
                        .Col = ColRate
                        mBillRate = Val(.Text)
                        .Col = ColPORate
                        mPORate = Val(.Text)
                        .Col = ColAcctQuantity
                        mQty = Val(.Text)
                        .Col = ColDiff
                        mRate = System.Math.Abs(Val(.Text))
                        .Col = ColDiffAmount
                        mAmount = System.Math.Abs(Val(.Text))
                        If optGSTApp(0).Checked = True Then
                            .Col = ColCGSTPer
                            mCGSTPer = Val(.Text)
                            .Col = ColCGSTAmount
                            mCGSTAmount = System.Math.Abs(Val(.Text))
                            .Col = ColSGSTPer
                            mSGSTPer = Val(.Text)
                            .Col = ColSGSTAmount
                            mSGSTAmount = System.Math.Abs(Val(.Text))
                            .Col = ColIGSTPer
                            mIGSTPer = Val(.Text)
                            .Col = ColIGSTAmount
                            mIGSTAmount = System.Math.Abs(Val(.Text))
                        Else
                            mCGSTPer = 0
                            mCGSTAmount = 0
                            mSGSTPer = 0
                            mSGSTAmount = 0
                            mIGSTPer = 0
                            mIGSTAmount = 0
                        End If
                        mExicseableAmt = 0
                        mCessableAmt = 0
                        mServiceAmt = 0
                        mCESSAmt = 0
                        mSTableAmt = 0
                        SqlStr = ""
                        If mItemCode <> "" And mQty > 0 Then
                            SqlStr = " INSERT INTO FIN_SUPP_SALE_DET ( " & vbCrLf & " MKEY , SUBROWNO, " & vbCrLf & " ITEM_CODE , CUSTOMER_PART_NO, HSNCODE, " & vbCrLf & " ITEM_DESC, ITEM_UOM, " & vbCrLf & " SALE_FYEAR, SALE_MKEY, " & vbCrLf & " BILL_NO, INVOICE_DATE, " & vbCrLf & " BILL_QTY, BILL_RATE, " & vbCrLf & " SO_RATE, QTY, " & vbCrLf & " RATE, AMOUNT, " & vbCrLf & " ITEM_ED, ITEM_ST, " & vbCrLf & " ITEM_CESS, COMPANY_CODE, AUTO_KEY_INVOICE, " & vbCrLf & " CGST_PER, CGST_AMOUNT, " & vbCrLf & " SGST_PER, SGST_AMOUNT, " & vbCrLf & " IGST_PER, IGST_AMOUNT " & vbCrLf & " ) "
                            SqlStr = SqlStr & vbCrLf & " VALUES ('" & pMKey & "'," & I & ", " & vbCrLf & " '" & mItemCode & "', '" & mPartNo & "', '" & mHSNCode & "', " & vbCrLf & " '" & mItemDesc & "', '" & mUnit & "'," & vbCrLf & " " & mPurFYear & ", '" & mPurMkey & "', " & vbCrLf & " '" & mBillNo & "', TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mBillQty & ", " & mBillRate & ", " & vbCrLf & " " & mPORate & ", " & mQty & ", " & vbCrLf & " " & mRate & ", " & mAmount & ", " & vbCrLf & " " & mExicseableAmt & ", " & mSTableAmt & "," & vbCrLf & " " & mCESSAmt & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & mInvoiceNo & "," & vbCrLf & " " & mCGSTPer & ", " & mCGSTAmount & "," & vbCrLf & " " & mSGSTPer & ", " & mSGSTAmount & "," & vbCrLf & " " & mIGSTPer & ", " & mIGSTAmount & " " & vbCrLf & " ) "
                            PubDBCn.Execute(SqlStr)
                            If optGSTApp(0).Checked = True Then
                                If UpdateGSTTRN(PubDBCn, pMKey, CStr(ConSaleDebitBookCode), mBookType, mBookSubType, pVNo, VB6.Format(pVDate, "DD-MMM-YYYY"), Trim(pBillNo), VB6.Format(pBillDate, "DD-MMM-YYYY"), mBillNo, VB6.Format(mBillDate, "DD-MMM-YYYY"), pSuppCustCode, mDebitAccountCode, "Y", pSuppCustCode, I, mItemCode, mQty, mUnit, mRate, mAmount, mAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, mDivisionCode, mHSNCode, mItemDesc, "", "N", mBookType, "G", "N", "D", VB6.Format(pBillDate, "DD-MMM-YYYY"), "N") = False Then GoTo UpdateDetail1
                            End If
                        End If
                    End If
                End If
            Next
        End With
        UpdateDetail1 = True
        UpdateDetail1 = UpdateExp1(pMKey, pItemValue, pCGSTAmount, pSGSTAmount, pIGSTAmount)
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function UpdateExp1(ByRef pMKey As String, ByRef pItemValue As Double, ByRef pCGSTAmount As Double, ByRef pSGSTAmount As Double, ByRef pIGSTAmount As Double) As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim mDebitAmount As String
        Dim mIdentification As String
        PubDBCn.Execute("Delete From FIN_SUPP_SALE_EXP Where Mkey='" & pMKey & "'")
        SqlStr = "Select * From FIN_INTERFACE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (Type='S' OR Type='B') AND GST_ENABLED='Y' Order By PrintSequence"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            I = 0
            Do While Not RS.EOF
                I = I + 1
                mExpCode = RS.Fields("CODE").Value
                mPercent = 0
                mRO = "N"
                mDebitAmount = CStr(0)
                mCalcOn = 0
                mExpAmount = 0
                mIdentification = IIf(IsDbNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)
                If mIdentification = "CGS" Then
                    mExpAmount = pCGSTAmount
                    mCalcOn = pItemValue
                ElseIf mIdentification = "SGS" Then
                    mExpAmount = pSGSTAmount
                    mCalcOn = pItemValue
                ElseIf mIdentification = "IGS" Then
                    mExpAmount = pIGSTAmount
                    mCalcOn = pItemValue
                Else
                    mExpAmount = 0
                    mCalcOn = 0
                End If
                If mCalcOn <> 0 Or mExpAmount <> 0 Then
                    SqlStr = "Insert Into  FIN_SUPP_SALE_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DebitAmount) " & vbCrLf & " Values ('" & pMKey & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "'," & mDebitAmount & ")"
                    PubDBCn.Execute(SqlStr)
                End If
                RS.MoveNext()
                '            SprdExp.Row = I
                '
                '            SprdExp.Col = ColRO
                '            SprdExp.Value = IIf(RS.Fields("ROUNDOFF").Value = "Y", vbChecked, vbUnchecked)
                '
                '            SprdExp.Col = ColExpName
                '            SprdExp.Text = RS.Fields("Name").Value
                '
                '            SprdExp.Col = ColExpPercent
                '            If ADDMode = True Then
                '                SprdExp.Text = Str(IIf(IsNull(RS.Fields("DefaultPercent").Value), 0, Str(RS.Fields("DefaultPercent").Value)))
                '            Else
                '                SprdExp.Text = ""
                '            End If
                '
                '            SprdExp.Col = ColExpAmt
                '            SprdExp.Text = "0"
                '
                '            SprdExp.Col = ColExpSTCode
                '            SprdExp.Text = Val(IIf(IsNull(RS.Fields("CODE").Value), -1, RS.Fields("CODE").Value))
                '
                '            SprdExp.Col = ColExpAddDeduct
                '            SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")
                '
                '            SprdExp.Col = ColExpIdent
                '            SprdExp.Text = IIf(IsNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)
                '            If SprdExp.Text = "DAM" Then MainClass.ProtectCell SprdExp, I, I, 1, SprdExp.MaxCols
                '
                '            SprdExp.Col = ColTaxable
                '            SprdExp.Text = IIf(IsNull(RS.Fields("Taxable").Value), "N", RS.Fields("Taxable").Value)
                '
                '            SprdExp.Col = ColExciseable
                '            SprdExp.Text = IIf(IsNull(RS.Fields("Exciseable").Value), "N", RS.Fields("Exciseable").Value)
                '
                '            If RS.Fields("Identification").Value = "ST" Then
                '                If RS.Fields("STTYPE").Value = mLocal Then
                '                    SprdExp.RowHidden = False
                '                Else
                '                    SprdExp.RowHidden = True
                '                End If
                '            End If
                '
                '
                '            If RS.EOF = False Then
                '                SprdExp.MaxRows = SprdExp.MaxRows + 1
                '            End If
            Loop
        End If
        '    With SprdExp
        '        For I = 1 To .MaxRows
        '            .Row = I
        '
        '            .Col = ColExpName
        '            If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND GST_ENABLED='Y' ") = True Then
        '                mExpCode = MasterNo
        '            Else
        '                mExpCode = -1
        '            End If
        '
        '            .Col = ColExpPercent
        '            mPercent = Val(.Text)
        '
        '            .Col = ColExpAmt
        '            mExpAmount = Val(.Text)
        '
        '            SprdExp.Col = ColExpAddDeduct
        '            m_AD = SprdExp.Text
        '            If m_AD = "D" Then
        '                mExpAmount = mExpAmount * -1
        '            End If
        '
        '            SprdExp.Col = ColExpCalcOn
        '            mCalcOn = Val(.Text)
        '
        '            .Col = ColExpDebitAmt
        '            mDebitAmount = Val(.Text)
        '
        '            .Col = ColRO
        '            mRO = IIf(.Value = vbChecked, "Y", "N")
        '
        '            SqlStr = ""
        '            If mCalcOn <> 0 Or mExpAmount <> 0 Then
        '                SqlStr = "Insert Into  FIN_SUPP_SALE_EXP (MKEY,SUBROWNO, " & vbCrLf _
        ''                        & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DebitAmount) " & vbCrLf _
        ''                        & " Values ('" & LblMKey.text & "'," & I & ", " & vbCrLf _
        ''                        & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf _
        ''                        & " " & mCalcOn & ",'" & mRO & "'," & mDebitAmount & ")"
        '                PubDBCn.Execute SqlStr
        '            End If
        '        Next I
        '    End With
        UpdateExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateExp1 = False
    End Function
    Private Function AutoGenSeqBillNo() As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsSuppPurchMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Integer
        Dim SqlStr As String
        Dim mStartingSNo As Double
        Dim pStartingSNo As Double
        Dim xFYear As Integer
        Dim mMAxNo As Double

        SqlStr = ""
        pStartingSNo = 1
        xFYear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))
        mStartingSNo = CDbl(xFYear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & VB6.Format(pStartingSNo, "00000"))

        SqlStr = "SELECT Max(VNOSEQ)  FROM FIN_SUPP_SALE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND BOOKTYPE='" & VB.Left(ConSaleDebit, 1) & "'"
        SqlStr = SqlStr & vbCrLf & " AND VDATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSuppPurchMainGen
            If .EOF = False Then
                mMAxNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mMAxNo = -1 Then
                    mNewSeqBillNo = 1
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = mStartingSNo
                End If
            Else
                mNewSeqBillNo = mStartingSNo
            End If
        End With
        AutoGenSeqBillNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub SearchSO()
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mCustCode As String
        If Trim(TxtAccount.Text) = "" Then
            MsgInformation("Please Select Customer First")
            Exit Sub
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCustCode = MasterNo
        Else
            MsgInformation("No Such Account in Account Master")
            Exit Sub
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "'" ''& vbCrLf |            & " AND ORDER_TYPE='O'"
        If MainClass.SearchGridMaster((txtPONo.Text), "DSP_SALEORDER_HDR", "AUTO_KEY_SO", "AMEND_NO", "AMEND_WEF_FROM AS WEF", "CUST_PO_NO || '-' || CUST_AMEND_NO AS CUSTOMER_PONO", SqlStr) = True Then
            txtPONo.Text = AcName
            txtPOAmendNo.Text = AcName1
            txtPONO_Validating(txtPONO, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchItem.Click
        SearchItem()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        FormatSprdMain(-1)
        Call CalcSprdTotal()
        Call PrintStatus(True)
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = "DELETE FROM TEMP_DNCN_PROCESS WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmCustomerCRNoteGen_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmCustomerCRNoteGen_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        cboInvType.Items.Clear()
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='S' ORDER BY NAME "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While Not RS.EOF
                cboInvType.Items.Add(RS.Fields("NAME").Value)
                RS.MoveNext()
            Loop
        End If
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
        TxtAccount.Enabled = True
        cmdsearch.Enabled = True
        txtItemName.Enabled = False
        cmdsearchItem.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        optGSTApp(0).Enabled = True 'IIf(PubSuperUser = "S", True, False)
        Call FormatSprdMain(-1)
        Call frmCustomerCRNoteGen_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmCustomerCRNoteGen_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth
        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmCustomerCRNoteGen_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColStatus
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With
        End If
    End Sub
    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchItem()
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            txtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        If TxtAccount.Text = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
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
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColStatus
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)
            .set_RowHeight(-1, RowHeight)
            .Row = -1
            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 7)
            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 15)
            .ColsFrozen = ColItemName
            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 7)
            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColHSNCode, 7)
            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 8)
            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 8)
            .Col = ColAutoInvoiceNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAutoInvoiceNo, 6)
            .ColHidden = True
            For cntCol = ColQuantity To ColIGSTAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 7)
            Next
            For cntCol = ColFyear To ColMKEY
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(cntCol, 8)
                .ColHidden = True
            Next
            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColStatus, 8)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColMKEY)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean
        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset
        Dim mData As Double
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim mMRRDate As String
        Dim mBillDate As String
        Dim mCustRefNo As String
        Dim mItemCode As String
        Dim mSuppCode As String
        Dim mFYear As Integer
        Dim mMRRNO As String
        Dim mBillNo As String
        Dim mMkey As String
        Dim mVNo As String
        Dim mVDate As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mHSNCode As String
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mAcceptedQty As Double
        Dim mSaleReturnQty As Double
        Dim mCustomerCode As String
        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mLocal = "N"
        If Trim(TxtAccount.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(Trim(TxtAccount.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = Trim(MasterNo)
            End If
        End If
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        If MainClass.ValidateWithMasterTable(Trim(TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = Trim(MasterNo)
        End If
        SqlStr = MakeSQL_C
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        cntRow = 1
        With SprdMain
            Do While RsTemp.EOF = False
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                .Col = ColUnit
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                .Col = ColItemName
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_NAME").Value), "", RsTemp.Fields("ITEM_NAME").Value)
                .Col = ColHSNCode
                mHSNCode = GetHSNCode(mItemCode)
                .Text = mHSNCode ''IIf(IsNull(RsTemp!HSNCODE), "", RsTemp!HSNCODE)
                .Col = ColQuantity
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("BILLQTY").Value), "0", RsTemp.Fields("BILLQTY").Value), "0.00")
                .Col = ColAcctQuantity
                mAcceptedQty = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("ACCPETED").Value), "0", RsTemp.Fields("ACCPETED").Value), "0.00"))
                mSaleReturnQty = GetSaleReturnQty(mCustomerCode, mItemCode, IIf(IsDbNull(RsTemp.Fields("VNO").Value), -1, RsTemp.Fields("VNO").Value))
                mAcceptedQty = mAcceptedQty - mSaleReturnQty
                .Text = VB6.Format(mAcceptedQty, "0.00")
                .Col = ColRate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("Rate").Value), "0", RsTemp.Fields("Rate").Value), "0.00")
                .Col = ColDNCNRate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("DNCN_RATE").Value), "0", RsTemp.Fields("DNCN_RATE").Value), "0.00")
                .Col = ColSuppRate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SUPP_RATE").Value), "0", RsTemp.Fields("SUPP_RATE").Value), "0.00")
                .Col = ColPORate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("PORATE").Value), "0", RsTemp.Fields("PORATE").Value), "0.00")
                .Col = ColAutoInvoiceNo
                .Text = CStr(IIf(IsDbNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value))
                .Col = ColBillNo
                .Text = IIf(IsDbNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                .Col = ColBillDate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")
                .Col = ColFyear
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("FYEAR").Value), "0", RsTemp.Fields("FYEAR").Value), "0000")
                If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "G", mPartyGSTNo) = False Then GoTo LedgError
                .Col = ColCGSTPer
                .Text = VB6.Format(pCGSTPer, "0.00") ' Format(IIf(IsNull(RsTemp!CGST_PER), "0", RsTemp!CGST_PER), "0.00")
                .Col = ColSGSTPer
                .Text = VB6.Format(pSGSTPer, "0.00") 'Format(IIf(IsNull(RsTemp!SGST_PER), "0", RsTemp!SGST_PER), "0.00")
                .Col = ColIGSTPer
                .Text = VB6.Format(pIGSTPer, "0.00") ' Format(IIf(IsNull(RsTemp!IGST_PER), "0", RsTemp!IGST_PER), "0.00")
                .Col = ColDiv
                .Text = CStr(IIf(IsDbNull(RsTemp.Fields("DIV_CODE").Value), "0", RsTemp.Fields("DIV_CODE").Value))
                .Col = ColSONo
                .Text = CStr(IIf(IsDbNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), "", RsTemp.Fields("OUR_AUTO_KEY_SO").Value))
                .Col = ColSODate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("OUR_SO_DATE").Value), "", RsTemp.Fields("OUR_SO_DATE").Value), "DD/MM/YYYY")
                .Col = ColCustRefNo
                .Text = IIf(IsDbNull(RsTemp.Fields("CUST_PO_NO").Value), "", RsTemp.Fields("CUST_PO_NO").Value)
                .Col = ColCustRefDate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CUST_PO_DATE").Value), "", RsTemp.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")
                .Col = ColMark
                .Text = ""
                .Col = ColMKEY
                .Text = IIf(IsDbNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
                .Col = ColStatus
                .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End If
            Loop
        End With
        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
LedgError:
        '    Resume
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetSaleReturnQty(ByRef pCustomerCode As String, ByRef pItemCode As String, ByRef mSaleInvoiceNo As Double) As Double
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        GetSaleReturnQty = 0
        mSqlStr = "SELECT " & vbCrLf & " SUM(ID.BILL_QTY) AS BILL_QTY" & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'" & vbCrLf & " AND IH.REF_TYPE IN ('I','1','2')" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        mSqlStr = mSqlStr & vbCrLf & " AND ID.REF_AUTO_KEY_NO=" & mSaleInvoiceNo & ""
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetSaleReturnQty = IIf(IsDbNull(RsTemp.Fields("BILL_QTY").Value), 0, RsTemp.Fields("BILL_QTY").Value)
        End If
        Exit Function
ErrPart:
        'Resume
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQL_C() As String
        On Error GoTo ERR1
        Dim mDivisionCode As Double
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAllTrnType As Boolean
        Dim mItemCode As String
        ''SELECT CLAUSE...
        MakeSQL_C = " SELECT ID.ITEM_CODE, ID.ITEM_UOM, CMST.SUPP_CUST_NAME, ID.ITEM_DESC || ' ' || ID.CUSTOMER_PART_NO AS ITEM_NAME, " & vbCrLf & " IH.AUTO_KEY_INVOICE AS VNO,IH.INVOICE_DATE AS VDATE, IH.BILLNO, IH.INVOICE_DATE AS BILLDATE," & vbCrLf & " SUM(ID.ITEM_QTY) As BILLQTY, SUM(ID.ITEM_QTY - GETSALESHORTAGEQTY(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE)) AS ACCPETED, ID.ITEM_RATE As RATE, " & vbCrLf & " TO_CHAR(GETSALEDEBITRATE(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE)) AS DNCN_RATE," & vbCrLf & " TO_CHAR(GETSALESUPPBILLPRICE(IH.COMPANY_CODE, ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE)) AS SUPP_RATE, " & vbCrLf & " '0.000', TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE)) AS PORATE, " & vbCrLf & " IH.FYEAR, IH.CUST_PO_NO, IH.CUST_PO_DATE, IH.OUR_AUTO_KEY_SO, IH.OUR_SO_DATE ,ID.CGST_PER, ID.SGST_PER, ID.IGST_PER, ID.HSNCODE, IH.DIV_CODE, IH.MKEY "
        ''GETSALEDEBITRATE (mCompanyCode NUMBER,mFYEAR NUMBER, mMKEY CHAR, mSUPPLIERCODE char, mItemCode CHAR)
        ''FROM CLAUSE...
        MakeSQL_C = MakeSQL_C & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST"
        ''WHERE CLAUSE...
        MakeSQL_C = MakeSQL_C & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND" & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"
        If Val(txtPONo.Text) <> 0 Then
            MakeSQL_C = MakeSQL_C & vbCrLf & " AND IH.OUR_AUTO_KEY_SO=" & Val(txtPONo.Text) & ""
        End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            MakeSQL_C = MakeSQL_C & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        MakeSQL_C = MakeSQL_C & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mItemCode = "-1"
            If MainClass.ValidateWithMasterTable(Trim(txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = Trim(MasterNo)
            End If
            MakeSQL_C = MakeSQL_C & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
        End If
        MakeSQL_C = MakeSQL_C & vbCrLf & "AND CANCELLED='N' AND IH.REF_DESP_TYPE<>'U'" ' AND AGTD3='N'"
        MakeSQL_C = MakeSQL_C & vbCrLf & " AND GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE)< " & vbCrLf & " ID.ITEM_RATE + GETSALESUPPBILLPRICE(IH.COMPANY_CODE, ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE)-GETSALEDEBITRATE(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE)"
        MakeSQL_C = MakeSQL_C & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MakeSQL_C = MakeSQL_C & vbCrLf & " GROUP BY ID.ITEM_CODE, ID.ITEM_UOM,CMST.SUPP_CUST_NAME, ID.ITEM_DESC || ' ' || ID.CUSTOMER_PART_NO, " & vbCrLf & " IH.AUTO_KEY_INVOICE,IH.INVOICE_DATE, IH.BILLNO, IH.INVOICE_DATE," & vbCrLf & " ID.ITEM_RATE, " & vbCrLf & " TO_CHAR(GETSALEDEBITRATE(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE))," & vbCrLf & " TO_CHAR(GETSALESUPPBILLPRICE(IH.COMPANY_CODE, ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE)), " & vbCrLf & " TO_CHAR(GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE)), " & vbCrLf & " FYEAR,IH.CUST_PO_NO, IH.CUST_PO_DATE, IH.OUR_AUTO_KEY_SO, IH.OUR_SO_DATE ,ID.CGST_PER, ID.SGST_PER, ID.IGST_PER, ID.HSNCODE, IH.DIV_CODE,IH.MKEY "
        ''ORDER CLAUSE...
        MakeSQL_C = MakeSQL_C & vbCrLf & "ORDER BY IH.INVOICE_DATE,IH.BILLNO,ID.ITEM_DESC || ' ' || ID.CUSTOMER_PART_NO, CMST.SUPP_CUST_NAME"
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub CalcSprdTotal()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mPurRate As Double
        Dim mDNCNRate As Double
        Dim mSuppRate As Double
        Dim mNetRate As Double
        Dim mPORate As Double
        Dim mDiffRate As Double
        Dim mDelRow As Double
        Dim mQty As Double
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mDiffAmount As Double
        mDelRow = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAcctQuantity
                mQty = Val(.Text)
                .Col = ColRate
                mPurRate = Val(.Text)
                .Col = ColDNCNRate
                mDNCNRate = Val(.Text)
                .Col = ColSuppRate
                mSuppRate = Val(.Text)
                .Col = ColNetRate
                mNetRate = CDbl(VB6.Format(mPurRate - mDNCNRate + mSuppRate, "0.000"))
                .Text = VB6.Format(mPurRate - mDNCNRate + mSuppRate, "0.000")
                .Col = ColPORate
                mPORate = Val(.Text)
                .Col = ColDiff
                mDiffRate = mPORate - mNetRate
                .Text = VB6.Format(mDiffRate, "0.000")
                .Col = ColDiffAmount
                mDiffAmount = CDbl(VB6.Format(mQty * mDiffRate, "0.00"))
                .Text = VB6.Format(mDiffAmount, "0.00")
                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)
                .Col = ColCGSTAmount
                mCGSTAmount = CDbl(VB6.Format(System.Math.Round(mDiffAmount * mCGSTPer * 0.01, 2), "0.00"))
                .Text = VB6.Format(mCGSTAmount, "0.00")
                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)
                .Col = ColSGSTAmount
                mSGSTAmount = CDbl(VB6.Format(System.Math.Round(mDiffAmount * mSGSTPer * 0.01, 2), "0.00"))
                .Text = VB6.Format(mSGSTAmount, "0.00")
                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)
                .Col = ColIGSTAmount
                mIGSTAmount = CDbl(VB6.Format(System.Math.Round(mDiffAmount * mIGSTPer * 0.01, 2), "0.00"))
                .Text = VB6.Format(mIGSTAmount, "0.00")
                '            If cboShow.ListIndex = 1 Then
                '                If mDiffRate >= 0 Then
                '                    .Row = cntRow
                '                    .Action = SS_ACTION_DELETE_ROW
                ''                    If .MaxRows > 1 Then .MaxRows = .MaxRows - 1
                '                    mDelRow = mDelRow + 1
                '                End If
                '            ElseIf cboShow.ListIndex = 2 Then
                '                 If mDiffRate <= 0 Then
                '                    .Row = cntRow
                '                    .Action = SS_ACTION_DELETE_ROW
                ''                    If .MaxRows > 1 Then .MaxRows = .MaxRows - 1
                '                    mDelRow = mDelRow + 1
                '                End If
                '            End If
            Next
            '        If .MaxRows > mDelRow Then .MaxRows = .MaxRows - mDelRow
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        '    If chkAll.Value = vbUnchecked Then
        If Trim(TxtAccount.Text) = "" Then
            MsgInformation("Invaild Account Name")
            TxtAccount.Focus()
            FieldsVerification = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            MsgInformation("Invaild Account Name")
            TxtAccount.Focus()
            FieldsVerification = False
            Exit Function
        End If
        '    End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String
        '    SprdMain.Row = SprdMain.ActiveRow
        '
        '    SprdMain.Col = ColVDate
        '    xVDate = Me.SprdMain.Text
        '
        '    SprdMain.Col = ColMKEY
        '    xMKey = Me.SprdMain.Text
        '
        '    SprdMain.Col = IIf(lblBookType.text = "S", ColVNo, ColBillNo)
        '    xVNo = Me.SprdMain.Text
        '
        '    Call ShowTrn(xMKey, xVDate, "", xVNo, IIf(lblBookType.text = "S", "P", "S"), "")
    End Sub
    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then
            SprdMain_DblClick(SprdMain, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        '    If KeyCode = vbKeyEscape Then cmdClose = True
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
        Dim SqlStr As String
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
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then
        '        txtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPOAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPOAmendNo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPODate.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtPONO_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.DoubleClick
        SearchSO()
    End Sub
    Private Sub txtPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPONO_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPONo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSO()
    End Sub
    Public Sub txtPONO_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim xMKey As String
        Dim mPONo As Double
        Dim SqlStr As String
        Dim RsPOMain As ADODB.Recordset
        Dim mAccountName As String
        Dim mSupplierCode As String
        If Val(txtPONo.Text) = 0 Then GoTo EventExitSub
        If Len(txtPONo.Text) < 6 Then
            txtPONo.Text = Val(txtPONo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        mPONo = Val(txtPONo.Text)
        '    If Val(txtPOAmendNo.Text) = 0 Then Exit Sub
        SqlStr = "SELECT * FROM DSP_SALEORDER_HDR " & vbCrLf & " WHERE AUTO_KEY_SO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " ''& vbCrLf |            & " AND ORDER_TYPE='O'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPOMain.EOF = False Then
            '        Clear1
            txtPONo.Text = IIf(IsDbNull(RsPOMain.Fields("AUTO_KEY_SO").Value), "", RsPOMain.Fields("AUTO_KEY_SO").Value)
            txtPODate.Text = VB6.Format(IIf(IsDbNull(RsPOMain.Fields("SO_DATE").Value), "", RsPOMain.Fields("SO_DATE").Value), "DD/MM/YYYY")
            '        txtWEFDate.Text = Format(IIf(IsNull(RsPOMain.Fields("AMEND_WEF_FROM").Value), "", RsPOMain.Fields("AMEND_WEF_FROM").Value), "DD/MM/YYYY")
            mSupplierCode = IIf(IsDbNull(RsPOMain.Fields("SUPP_CUST_CODE").Value), -1, RsPOMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If
            TxtAccount.Text = mAccountName
        Else
            MsgBox("Invalid PO NO.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtVDate) = False Then
            txtVDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtVDate.Text))) = False Then
            txtVDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
