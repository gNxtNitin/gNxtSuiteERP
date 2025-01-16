Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSuppPurchaseGen
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim mAccountCode As String
    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColUnit As Short = 3
    Private Const ColHSNCode As Short = 4
    Private Const ColVNo As Short = 5
    Private Const ColVDate As Short = 6
    Private Const ColBillNo As Short = 7
    Private Const ColBillDate As Short = 8
    Private Const ColMRRNo As Short = 9
    Private Const ColMRRDate As Short = 10
    Private Const ColQuantity As Short = 11
    Private Const ColAcctQuantity As Short = 12
    Private Const ColRate As Short = 13
    Private Const ColDNCNRate As Short = 14
    Private Const ColSuppRate As Short = 15
    Private Const ColNetRate As Short = 16
    Private Const ColPORate As Short = 17
    Private Const ColDiff As Short = 18
    Private Const ColDiffAmount As Short = 19
    Private Const ColSuppBillRate As Short = 20
    Private Const ColSuppBillAmount As Short = 21
    Private Const ColCGSTPer As Short = 22
    Private Const ColCGSTAmount As Short = 23
    Private Const ColSGSTPer As Short = 24
    Private Const ColSGSTAmount As Short = 25
    Private Const ColIGSTPer As Short = 26
    Private Const ColIGSTAmount As Short = 27
    Private Const ColItemType As Short = 28
    Private Const ColFyear As Short = 29
    Private Const ColDiv As Short = 30
    Private Const ColPONo As Short = 31
    Private Const ColPODate As Short = 32
    Private Const ColPOWEF As Short = 33
    Private Const ColCustRefNo As Short = 34
    Private Const ColCustRefDate As Short = 35
    Private Const ColRoundOff As Short = 36
    Private Const ColMark As Short = 37
    Private Const ColMKEY As Short = 38
    Private Const ColLocation As Short = 39
    Private Const ColStatus As Short = 40
    ''Private Const ColAutoInvoiceNo = 6
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
        Me.Dispose()
        Me.Close()
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
        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, ColStatus, PubDBCn) = False Then GoTo ReportErr
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
        Dim mInvoiceCheckKey As String
        Dim mMark As String
        Dim mItemType As String
        Dim mTRNType As String
        Dim mSuppCustCode As String
        Dim mAccountCode As String
        Dim mControlAcctCode As String
        Dim mBookSubType As String
        Dim mAcceptedQty As Double
        Dim mCustRefNo As String
        Dim mCustRefDate As String
        Dim mPOWEF As String
        Dim mRO As Double
        'Dim CntRow As Long
        Dim mValue As String
        Dim mLockBookCode As Integer
        Dim mMessage As String
        Dim mPONo As Double
        Dim mPODate As String
        Dim mRate As Double
        Dim pErrorMsg As String
        Dim mLocationID As String

        If MainClass.ChkIsdateF(TxtVDate) = False Then
            TxtVDate.Focus()
            MsgBox("Invalid date", MsgBoxStyle.Information)
            Exit Sub
        End If
        If FYChk(CStr(CDate(TxtVDate.Text))) = False Then
            TxtVDate.Focus()
            MsgBox("Date is not is Current FY.", MsgBoxStyle.Information)
            Exit Sub
        End If
        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            Exit Sub
        End If
        mLockBookCode = CInt(ConLockPurchase)
        If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, TxtVDate.Text, (TxtAccount.Text)) = True Then
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
        If lblGoodsService.Text = "G" Then
            mControlAcctCode = "-1"
            If MainClass.ValidateWithMasterTable((RsCompany.Fields("COMPANY_CODE").Value), "COMPANY_CODE", "RATE_DIFF_ACCOUNTCODE", "FIN_PRINT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mControlAcctCode = MasterNo
            End If
            If MainClass.ValidateWithMasterTable(mControlAcctCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                mControlAcctCode = CStr(-1)
                MsgBox("Control Account Does Not Exist In Master", MsgBoxStyle.Information)
                Exit Sub
            End If
        Else
            mControlAcctCode = mAccountCode
        End If
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
            mBookSubType = MasterNo
        Else
            mBookSubType = "-1"
        End If
        '    If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "ITEMTYPE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mItemType = MasterNo
        '    End If
        If optGSTApp(0).Checked = True Then
            mMessage = "You are select GST Applicable, Are you sure to Continue..."
        Else
            mMessage = "You are select GST is not Applicable, Are you sure to Continue..."
        End If
        If MsgQuestion(mMessage) = CStr(MsgBoxResult.No) Then
            Exit Sub
        End If
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .Col = ColCustRefNo
                    mValue = Trim(.Text)
                    If Trim(mValue) = "" Then
                        MsgInformation("Customer Bill / Ref No cann't be Blank.")
                        Exit Sub
                    End If
                    If ValidateBillNo(mValue, pErrorMsg) = False Then
                        MsgInformation(pErrorMsg)
                        Exit Sub
                    End If
                    .Col = ColCustRefDate
                    mValue = Trim(.Text)
                    If Trim(mValue) = "" Then
                        MsgInformation("Customer Bill / Ref Date cann't be Blank.")
                        Exit Sub
                    ElseIf IsDate(mValue) = False Then
                        MsgInformation("Invalid Customer Bill / Ref Date.")
                        Exit Sub
                    End If
                    If CDate(mValue) < CDate(PubGSTApplicableDate) Then
                        MsgInformation("Invalid Customer Bill / Ref Date.")
                        Exit Sub
                    End If
                End If
            Next
        End With
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColMark
                mMark = Trim(.Text)
                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) And mMark = "" Then
                    .Col = ColItemType
                    mItemType = Trim(.Text)
                    .Col = ColBillNo
                    mBillNo = Trim(.Text)
                    .Col = ColBillDate
                    mBillDate = VB6.Format(.Text, "DD/MM/YYYY")
                    .Col = ColMKEY
                    mInvoiceCheckKey = CStr(Val(.Text))
                    .Col = ColPONo
                    mPONo = Val(.Text)
                    .Col = ColPODate
                    mPODate = VB6.Format(.Text, "DD/MM/YYYY")
                    .Col = ColCustRefNo
                    mCustRefNo = Trim(.Text)
                    mInvoiceCheckKey = mInvoiceCheckKey & "-" & mCustRefNo
                    .Col = ColAcctQuantity
                    mAcceptedQty = Val(.Text)
                    .Col = ColCustRefDate
                    mCustRefDate = VB6.Format(.Text, "DD/MM/YYYY")
                    .Col = ColPOWEF
                    mPOWEF = VB6.Format(.Text, "DD/MM/YYYY")
                    .Col = ColRoundOff
                    mRO = Val(.Text)
                    .Col = ColSuppBillRate
                    mRate = Val(.Text)

                    .Col = ColLocation
                    mLocationID = Trim(.Text)

                    mControlAcctCode = mAccountCode

                    If mAcceptedQty > 0 And mRate > 0 Then
                        If Trim(mInvoiceCheckKey) <> "" Then
                            If UpdateMain1(mInvoiceCheckKey, mBillNo, mBillDate, mSuppCustCode, IIf(CDate(mBillDate) >= CDate("01/10/2018"), mControlAcctCode, mAccountCode), mBookSubType, mItemType, mTRNType, mCustRefNo, mCustRefDate, mPOWEF, mRO, mLocationID) = False Then GoTo ErrPart
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
    Private Function UpdateMain1(ByRef mInvoiceCheckKey As String, ByRef mBillNo As String, ByRef mBillDate As String, ByRef mSuppCustCode As String, ByRef mAccountCode As String, ByRef mBookSubType As String, ByRef mItemType As String, ByRef mTRNType As String, ByRef mCustBillNo As String, ByRef mCustBillDate As String, ByRef pWEFDate As String, ByRef mRO As Double, ByRef mLocationID As String) As Boolean
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
        Dim mTotalGSTValue As Double
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
        'Dim mRO As Double
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
        Dim mCurrCheckKey As String
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
        '    mRO = 0
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
        pJVVnoStr = "-1"
        pJVMKey = "-1"
        mGSTApp = IIf(optGSTApp(0).Checked = True, "G", "W")
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColMark
                mMark = Trim(.Text)
                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) And mMark = "" Then
                    .Col = ColMKEY
                    mCurrCheckKey = CStr(Val(.Text))
                    .Col = ColCustRefNo
                    mCurrCheckKey = mCurrCheckKey & "-" & Trim(.Text)
                    If mInvoiceCheckKey = mCurrCheckKey Then
                        .Col = ColDiv
                        mDivisionCode = Val(.Text)
                        .Col = ColAcctQuantity
                        mTotQty = mTotQty + Val(.Text)
                        .Col = ColSuppBillAmount
                        mItemValue = mItemValue + Val(.Text)
                        '                    .Col = ColRoundOff
                        '                    mRO = Val(.Text)
                        If optGSTApp(0).Checked = True Then
                            .Col = ColCGSTAmount
                            mCGSTAmount = mCGSTAmount + Val(.Text)
                            .Col = ColSGSTAmount
                            mSGSTAmount = mSGSTAmount + Val(.Text)
                            .Col = ColIGSTAmount
                            mIGSTAmount = mIGSTAmount + Val(.Text)
                        End If
                    End If
                End If
            Next
        End With
        mNETVALUE = CDbl(VB6.Format(mItemValue + mCGSTAmount + mSGSTAmount + mIGSTAmount + mRO, "0.00"))
        mTotalGSTValue = CDbl(VB6.Format(mItemValue + mCGSTAmount + mSGSTAmount + mIGSTAmount, "0.00"))
        mTOTTAXABLEAMOUNT = CDbl(VB6.Format(mItemValue, "0.00"))
        If mNETVALUE > 0 Then
            mVNoSeq = CInt(AutoGenSeqBillNo())
            mVNo = VB.Left(ConPurchaseSupp, 1) & VB6.Format(Val(CStr(mVNoSeq)), "00000")
            mVDate = VB6.Format(txtVDate.Text, "DD/MM/YYYY")
            mNarration = "Rates Revised wide PO NO " & txtPONo.Text & "/" & txtPOAmendNo.Text
            mBookType = VB.Left(ConPurchaseSupp, 1)
            SqlStr = ""
            mCurRowNo = MainClass.AutoGenRowNo("FIN_SUPP_PUR_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo
            SqlStr = "INSERT INTO FIN_SUPP_PURCHASE_HDR( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " ROWNO, TRNTYPE, VNOPREFIX, " & vbCrLf & " VNOSEQ, VNO, " & vbCrLf & " VDATE, BILLNO, INVOICE_DATE, " & vbCrLf & " AUTO_KEY_PO, PO_DATE, AMEND_NO, PO_WEFDATE, " & vbCrLf & " SUPP_CUST_CODE, ACCOUNTCODE, TARIFFHEADING, " & vbCrLf & " BOOKTYPE, BOOKSUBTYPE, REMARKS, " & vbCrLf & " ITEMDESC, ITEMVALUE, STPERCENT, " & vbCrLf & " TOTSTAMT, TOTFREIGHT, TOTCHARGES, " & vbCrLf & " EDPERCENT, TOTEDAMOUNT, TOTSURCHARGEAMT, " & vbCrLf & " TOTDISCAMOUNT, TOTMSCAMOUNT, TOTRO, " & vbCrLf & " TOTEXPAMT, TOTTAXABLEAMOUNT, NETVALUE, " & vbCrLf & " TOTQTY, STTYPE, STFORMCODE, "
            SqlStr = SqlStr & vbCrLf & " STFORMNAME, STFORMDATE, " & vbCrLf & " STDUEFORMCODE, STDUEFORMNAME, " & vbCrLf & " STDUEFORMDATE, " & vbCrLf & " ISREGDNO, LSTCST, WITHFORM, " & vbCrLf & " CANCELLED, NARRATION, MODVATNO, " & vbCrLf & " MODVATPER, MODVATAMOUNT, STCLAIMNO, " & vbCrLf & " STCLAIMPER, STCLAIMAMOUNT, SUR_VATCLAIMAMOUNT," & vbCrLf & " JVNO, JVMKEY, " & vbCrLf & " ISCAPITAL, ISMODVAT, " & vbCrLf & " ISSTREFUND, ISFINALPOST, PAYMENTDATE, " & vbCrLf & " MODVATITEMVALUE, " & vbCrLf & " TOTEDUPERCENT, TOTEDUAMOUNT, CESSABLEAMOUNT, " & vbCrLf & " CESSPER, CESSAMOUNT, TO_DATE," & vbCrLf & " SHECPERCENT, SHECAMOUNT, SHECMODVATPER, SHECMODVATAMOUNT, " & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE,DIV_CODE, " & vbCrLf & " ISGSTAPPLICABLE, GST_CLAIM_NO, GST_CLAIM_DATE, " & vbCrLf & " TOTALGSTVALUE, TOTCGST_REFUNDAMT, TOTSGST_REFUNDAMT, " & vbCrLf & " TOTIGST_REFUNDAMT, TOTCGST_AMOUNT, TOTSGST_AMOUNT, TOTIGST_AMOUNT,GOODS_SERVICE,BILL_TO_LOC_ID "
            SqlStr = SqlStr & vbCrLf & " ) VALUES ( " & vbCrLf & " '" & nMkey & "', " & RsCompany.Fields("Company_Code").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mCurRowNo & ", " & Val(mTRNType) & ", '" & VB.Left(ConPurchaseSupp, 1) & "'," & vbCrLf & " " & mVNoSeq & ", '" & MainClass.AllowSingleQuote(mVNo) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mCustBillNo) & "',TO_DATE('" & VB6.Format(mCustBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtPONo.Text) & ", TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtPOAmendNo.Text) & ", TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mSuppCustCode & "', '" & mAccountCode & "', '', " & vbCrLf & " '" & mBookType & "', '" & mBookSubType & "', '', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemType) & "', " & mItemValue & ", " & mSTPERCENT & ", " & vbCrLf & " " & mTOTSTAMT & ", " & mTOTFREIGHT & ", " & mTOTCHARGES & ", " & vbCrLf & " " & mEDPERCENT & ", " & mTotEDAmount & ", " & mSURAmount & ", " & vbCrLf & " " & mTotDiscount & ", " & mMSC & ", " & mRO & ", " & vbCrLf & " " & mTOTEXPAMT & ", " & mTOTTAXABLEAMOUNT & ", " & mNETVALUE & "," & vbCrLf & " " & mTotQty & ", '" & mSTType & "', " & mFormRecdCode & ","
            SqlStr = SqlStr & vbCrLf & " '', '', " & vbCrLf & " " & mFormDueCode & ",'', " & vbCrLf & " '', " & vbCrLf & " '" & mIsRegdNo & "', '" & mLSTCST & "', '" & mWITHFORM & "', " & vbCrLf & " '" & mCancelled & "', '" & MainClass.AllowSingleQuote(mNarration) & "', '" & mModvatNo & "'," & vbCrLf & " 0,0, '" & mSTCLAIMNo & "'," & vbCrLf & " 0,0, 0, " & vbCrLf & " '" & pJVVnoStr & "', '" & pJVMKey & "', " & vbCrLf & " '" & mCapital & "', '" & mISMODVAT & "', " & vbCrLf & " '" & mISSTREFUND & "', '" & mFinalPost & "', TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') ," & vbCrLf & " 0, " & vbCrLf & " 0, 0, 0, " & vbCrLf & " 0 , 0 , " & vbCrLf & " TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " 0, 0, " & vbCrLf & " 0, 0, " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " '', ''," & mDivisionCode & ", " & vbCrLf & " '" & mGSTApp & "', '',''," & vbCrLf & " " & mTotalGSTValue & ", " & Val(CStr(mCGSTAmount)) & ", " & Val(CStr(mSGSTAmount)) & "," & vbCrLf & " " & Val(CStr(mIGSTAmount)) & ", " & Val(CStr(mCGSTAmount)) & ", " & Val(CStr(mSGSTAmount)) & "," & Val(CStr(mIGSTAmount)) & ", '" & lblGoodsService.Text & "','" & mLocationID & "' " & vbCrLf & " ) "
            PubDBCn.Execute(SqlStr)
            If UpdateDetail1(nMkey, mInvoiceCheckKey, mNarration, mSuppCustCode, mAccountCode, mTRNType, mDivisionCode, mBookType, mBookSubType, mTOTTAXABLEAMOUNT, mCGSTAmount, mSGSTAmount, mIGSTAmount, mRO) = False Then GoTo ErrPart
            ''If UpdateDetail1(nMkey, pInvoiceNo, mNarration, mVNo, mVDate, mSuppCustCode, mAccountCode, mDivisionCode, mBookType, mBookSubType, mBillNo, mBillDate, mTOTTAXABLEAMOUNT, mCGSTAmount, mSGSTAmount, mIGSTAmount) = False Then GoTo ErrPart

            If PurchaseSuppPostTRNGST(PubDBCn, nMkey, mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, mVDate, mCustBillNo, mCustBillDate, mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mItemValue)), Val(CStr(mNETVALUE)), False, False, pDueDate, VB.Left(mNarration, 254), "", Val(CStr(mTOTEXPAMT)), IIf(mGSTApp = "G", "Y", "N"), Val(CStr(mCGSTAmount)), Val(CStr(mSGSTAmount)), Val(CStr(mIGSTAmount)), mVDate, True, PubUserID, VB6.Format(PubCurrDate, "DD-MMM-YYYY"), mDivisionCode, Trim(txtPONo.Text), mLocationID) = False Then GoTo ErrPart
        End If
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .Col = ColMKEY
                    mCurrCheckKey = CStr(Val(.Text))
                    .Col = ColCustRefNo
                    mCurrCheckKey = mCurrCheckKey & "-" & Trim(.Text)
                    If mInvoiceCheckKey = mCurrCheckKey Then
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
    Private Function UpdateDetail1(ByRef pMKey As String, ByRef mInvoiceCheckKey As String, ByRef xNarration As String, ByRef pSuppCustCode As String, ByRef mDebitAccountCode As String, ByRef mInvTypeCode As String, ByRef mDivisionCode As Double, ByRef mBookType As String, ByRef mBookSubType As String, ByRef pItemValue As Double, ByRef pCGSTAmount As Double, ByRef pSGSTAmount As Double, ByRef pIGSTAmount As Double, ByRef mRO As Double) As Boolean
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
        Dim mVNo As String
        Dim mVDate As String
        Dim mMRRNO As Double
        Dim mMRRDate As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillQty As Double
        Dim mBillRate As Double
        Dim mPORate As Double
        Dim mQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mInvoiceNo As Double
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mMark As String
        Dim cntRow As Integer
        Dim mCurrCheckKey As String
        Dim mPONo As Double
        Dim mPODate As String
        mTotExicseableAmt = 0
        mTotSTableAmt = 0
        mTotCessableAmt = 0
        PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & pMKey & "' AND BookType='" & mBookType & "' AND BOOKCODE='" & ConPurchaseSuppBookCode & "'")
        PubDBCn.Execute("Delete From FIN_SUPP_PURCHASE_DET Where Mkey='" & pMKey & "'")
        I = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColMark
                mMark = Trim(.Text)
                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) And mMark = "" Then
                    .Col = ColMKEY
                    mCurrCheckKey = CStr(Val(.Text))
                    .Col = ColCustRefNo
                    mCurrCheckKey = mCurrCheckKey & "-" & Trim(.Text)
                    If mInvoiceCheckKey = mCurrCheckKey Then
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
                        .Col = ColPONo
                        mPONo = Val(.Text)
                        .Col = ColPODate
                        mPODate = VB6.Format(.Text, "DD/MM/YYYY")
                        .Col = ColFyear
                        mPurFYear = Val(.Text)
                        .Col = ColMKEY
                        mPurMkey = MainClass.AllowSingleQuote(.Text)
                        .Col = ColBillNo
                        mBillNo = MainClass.AllowSingleQuote(.Text)
                        .Col = ColBillDate
                        mBillDate = VB6.Format(.Text, "DD/MM/YYYY")
                        .Col = ColVNo
                        mVNo = Trim(.Text)
                        .Col = ColVDate
                        mVDate = VB6.Format(.Text, "DD/MM/YYYY")
                        .Col = ColMRRNo
                        mMRRNO = CDbl(Trim(.Text))
                        .Col = ColMRRDate
                        mMRRDate = VB6.Format(.Text, "DD/MM/YYYY")
                        .Col = ColQuantity
                        mBillQty = Val(.Text)
                        .Col = ColRate
                        mBillRate = Val(.Text)
                        .Col = ColPORate
                        mPORate = Val(.Text)
                        .Col = ColAcctQuantity
                        mQty = Val(.Text)
                        .Col = ColSuppBillRate
                        mRate = Val(.Text)
                        .Col = ColSuppBillAmount
                        mAmount = CDbl(VB6.Format(mQty * mRate, "0.00"))
                        If optGSTApp(0).Checked = True Then
                            .Col = ColCGSTPer
                            mCGSTPer = Val(.Text)
                            .Col = ColCGSTAmount
                            mCGSTAmount = System.Math.Abs(Val(.Text)) 'Format(mAmount * mCGSTPer * 0.01, "0.00") '' Abs(Val(.Text))
                            .Col = ColSGSTPer
                            mSGSTPer = Val(.Text)
                            .Col = ColSGSTAmount
                            mSGSTAmount = System.Math.Abs(Val(.Text)) ' Format(mAmount * mSGSTPer * 0.01, "0.00") ''Abs(Val(.Text))
                            .Col = ColIGSTPer
                            mIGSTPer = Val(.Text)
                            .Col = ColIGSTAmount
                            mIGSTAmount = System.Math.Abs(Val(.Text)) ' Format(mAmount * mIGSTPer * 0.01, "0.00") ''Abs(Val(.Text))
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
                        If mItemCode <> "" And mQty > 0 And mAmount > 0 Then
                            SqlStr = " INSERT INTO FIN_SUPP_PURCHASE_DET ( " & vbCrLf & " MKEY , SUBROWNO, " & vbCrLf & " ITEM_CODE , CUSTOMER_PART_NO, " & vbCrLf & " ITEM_DESC, HSNCODE, ITEM_UOM, " & vbCrLf & " PUR_FYEAR, PUR_MKEY, " & vbCrLf & " PURNO, PUR_DATE, " & vbCrLf & " BILL_NO, BILLDATE, " & vbCrLf & " BILL_QTY, BILL_RATE, " & vbCrLf & " PO_RATE, QTY, " & vbCrLf & " RATE, AMOUNT, " & vbCrLf & " ITEM_ED, ITEM_ST, " & vbCrLf & " ITEM_CESS, COMPANY_CODE, " & vbCrLf & " AUTO_KEY_MRR, MRRDATE, " & vbCrLf & " CGST_PER, CGST_AMOUNT, " & vbCrLf & " SGST_PER, SGST_AMOUNT, " & vbCrLf & " IGST_PER, IGST_AMOUNT, PUR_ACCOUNT_CODE, ITEM_TRNTYPE, " & vbCrLf & " PONO, PODATE) "
                            SqlStr = SqlStr & vbCrLf & " VALUES ('" & pMKey & "'," & I & ", " & vbCrLf & " '" & mItemCode & "', '" & mPartNo & "'," & vbCrLf & " '" & mItemDesc & "', '" & mHSNCode & "', '" & mUnit & "'," & vbCrLf & " " & mPurFYear & ", '" & mPurMkey & "', " & vbCrLf & " '" & mVNo & "', TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mBillNo & "', TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mBillQty & ", " & mBillRate & ", " & vbCrLf & " " & mPORate & ", " & mQty & ", " & vbCrLf & " " & mRate & ", " & mAmount & ", " & vbCrLf & " 0, 0," & vbCrLf & " 0," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & Val(CStr(mMRRNO)) & ", TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & mCGSTPer & ", " & mCGSTAmount & "," & vbCrLf & " " & mSGSTPer & ", " & mSGSTAmount & "," & vbCrLf & " " & mIGSTPer & ", " & mIGSTAmount & ", '" & MainClass.AllowSingleQuote(mDebitAccountCode) & "', " & mInvTypeCode & "," & vbCrLf & " " & Val(CStr(mPONo)) & ",TO_DATE('" & VB6.Format(mPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "
                            PubDBCn.Execute(SqlStr)
                            If optGSTApp(0).Checked = True Then
                                If UpdateGSTTRN(PubDBCn, pMKey, CStr(ConPurchaseSuppBookCode), mBookType, mBookSubType, mVNo, VB6.Format(mVDate, "DD-MMM-YYYY"), Trim(mVNo), VB6.Format(mVDate, "DD-MMM-YYYY"), mBillNo, VB6.Format(mBillDate, "DD-MMM-YYYY"), pSuppCustCode, mDebitAccountCode, "Y", pSuppCustCode, I, mItemCode, mQty, mUnit, mRate, mAmount, mAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, mDivisionCode, mHSNCode, mItemDesc, "", "N", "", (lblGoodsService.Text), "N", "C", mVDate, "N") = False Then GoTo UpdateDetail1
                            End If
                        End If
                    End If
                End If
            Next
        End With
        UpdateDetail1 = True
        UpdateDetail1 = UpdateExp1(pMKey, pItemValue, pCGSTAmount, pSGSTAmount, pIGSTAmount, mRO)
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function UpdateExp1(ByRef pMKey As String, ByRef pItemValue As Double, ByRef pCGSTAmount As Double, ByRef pSGSTAmount As Double, ByRef pIGSTAmount As Double, ByRef mROAmount As Double) As Boolean
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
        Dim mRoUpdate As Boolean
        mRoUpdate = False
        PubDBCn.Execute("Delete From FIN_SUPP_PURCHASE_EXP Where Mkey='" & pMKey & "'")
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
                ElseIf mIdentification = "RO" And mRoUpdate = False Then
                    mExpAmount = mROAmount
                    mCalcOn = 0
                    mRoUpdate = True
                Else
                    mExpAmount = 0
                    mCalcOn = 0
                End If
                If mCalcOn <> 0 Or mExpAmount <> 0 Then
                    SqlStr = "Insert Into  FIN_SUPP_PURCHASE_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DebitAmount) " & vbCrLf & " Values ('" & pMKey & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "'," & mDebitAmount & ")"
                    PubDBCn.Execute(SqlStr)
                End If
                RS.MoveNext()
            Loop
        End If
        UpdateExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateExp1 = False
    End Function
    Private Function AutoGenSeqBillNo() As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsSuppPurchMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Double
        Dim SqlStr As String
        Dim mStartingSNo As Double
        Dim xFyear As Integer
        Dim mMaxNo As Double
        SqlStr = ""
        xFyear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))
        mStartingSNo = 1
        If RsCompany.Fields("FYEAR").Value >= 2020 Then
            mStartingSNo = CDbl(xFyear & VB6.Format(IIf(IsDbNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & VB6.Format(mStartingSNo, "00000"))
        End If
        SqlStr = "SELECT Max(VNOSEQ)  FROM FIN_SUPP_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSuppPurchMainGen
            If .EOF = False Then
                mMAxNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mMaxNo <= 0 Then
                    mNewSeqBillNo = mStartingSNo
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
        If MainClass.SearchGridMaster((txtPONo.Text), "PUR_PURCHASE_HDR", "AUTO_KEY_PO", "AMEND_NO", "AMEND_WEF_DATE AS WEF", , SqlStr) = True Then
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
    Private Sub frmSuppPurchaseGen_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmSuppPurchaseGen_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='P' ORDER BY NAME "
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
        optGSTApp(0).Checked = True
        '    optGSTApp(0).Enabled = IIf(PubSuperUser = "S", True, False)
        Call FormatSprdMain(-1)
        Call frmSuppPurchaseGen_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmSuppPurchaseGen_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
    Private Sub frmSuppPurchaseGen_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        Call CalcSprdTotal()
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDate, 8)
            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 8)
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
            .ColsFrozen = ColBillNo
            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMRRNo, 6)
            .ColHidden = False
            .Col = ColMRRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMRRDate, 6)
            .ColHidden = False
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
            .Col = ColItemType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemType, 6)
            .ColHidden = False
            For cntCol = ColFyear To ColLocation
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(cntCol, 8)
                .ColHidden = True
            Next
            .Col = ColCustRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .ColHidden = False
            .Col = ColCustRefDate
            .ColHidden = False
            .Col = ColMark
            .ColHidden = False
            .Col = ColRoundOff
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .ColHidden = False
            .set_ColWidth(ColRoundOff, 7)
            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColStatus, 8)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.UnProtectCell(SprdMain, 1, .MaxRows, 1, ColLocation)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColDiffAmount)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColSuppBillAmount, ColPOWEF)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColMark, ColLocation)
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
        Dim mDivisionCode As Integer
        Dim mWEFDate As String
        Dim mPONo As String
        Dim mPODate As String
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
        mDivisionCode = CInt("-1")
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CInt(Trim(MasterNo))
        End If
        If InsertIntoTemp(mDivisionCode, mCustomerCode) = False Then GoTo LedgError
        SqlStr = MakeSQL_S
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        cntRow = 1
        With SprdMain
            Do While RsTemp.EOF = False
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                .Col = ColUnit
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                .Col = ColItemName
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_NAME").Value), "", RsTemp.Fields("ITEM_NAME").Value)
                .Col = ColItemType
                .Text = IIf(IsDbNull(RsTemp.Fields("itemType").Value), "", RsTemp.Fields("itemType").Value)
                .Col = ColHSNCode
                mHSNCode = IIf(IsDbNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value) ''GetHSNCode(mItemCode)
                If mHSNCode = "" And lblGoodsService.Text = "G" Then
                    mHSNCode = GetHSNCode(mItemCode)
                End If
                .Text = mHSNCode ''IIf(IsNull(RsTemp!HSNCODE), "", RsTemp!HSNCODE)
                .Col = ColQuantity
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("BILLQTY").Value), "0", RsTemp.Fields("BILLQTY").Value), "0.0000")
                .Col = ColAcctQuantity
                mAcceptedQty = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("ACCPETED").Value), "0", RsTemp.Fields("ACCPETED").Value), "0.0000"))
                '            mSaleReturnQty = GetSaleReturnQty(mCustomerCode, mItemCode, IIf(IsNull(RsTemp!VNO), -1, RsTemp!VNO))
                '
                '            mAcceptedQty = mAcceptedQty - mSaleReturnQty
                .Text = VB6.Format(mAcceptedQty, "0.0000")
                .Col = ColRate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("Rate").Value), "0", RsTemp.Fields("Rate").Value), "0.000")
                .Col = ColDNCNRate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("DNCN_RATE").Value), "0", RsTemp.Fields("DNCN_RATE").Value), "0.000")
                .Col = ColSuppRate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SUPP_RATE").Value), "0", RsTemp.Fields("SUPP_RATE").Value), "0.000")
                .Col = ColPORate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("PORATE").Value), "0", RsTemp.Fields("PORATE").Value), "0.000")
                .Col = ColVNo
                .Text = CStr(IIf(IsDbNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value))
                .Col = ColVDate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")
                .Col = ColBillNo
                .Text = IIf(IsDbNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                .Col = ColBillDate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")
                mBillDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")
                .Col = ColMRRNo
                .Text = CStr(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_MRR").Value), "", RsTemp.Fields("AUTO_KEY_MRR").Value))
                .Col = ColMRRDate
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("MRRDATE").Value), "", RsTemp.Fields("MRRDATE").Value), "DD/MM/YYYY")
                .Col = ColFyear
                .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("FYEAR").Value), "0", RsTemp.Fields("FYEAR").Value), "0000")
                If lblGoodsService.Text = "G" Then
                    If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "G", mPartyGSTNo) = False Then GoTo LedgError
                Else
                    If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, "G") = False Then GoTo LedgError
                End If
                .Col = ColCGSTPer
                .Text = VB6.Format(pCGSTPer, "0.00") ' Format(IIf(IsNull(RsTemp!CGST_PER), "0", RsTemp!CGST_PER), "0.00")
                .Col = ColSGSTPer
                .Text = VB6.Format(pSGSTPer, "0.00") 'Format(IIf(IsNull(RsTemp!SGST_PER), "0", RsTemp!SGST_PER), "0.00")
                .Col = ColIGSTPer
                .Text = VB6.Format(pIGSTPer, "0.00") ' Format(IIf(IsNull(RsTemp!IGST_PER), "0", RsTemp!IGST_PER), "0.00")
                .Col = ColDiv
                .Text = CStr(IIf(IsDbNull(RsTemp.Fields("DIV_CODE").Value), "0", RsTemp.Fields("DIV_CODE").Value))
                .Col = ColPONo
                .Text = CStr(IIf(IsDbNull(RsTemp.Fields("CUST_REF_NO").Value), "", RsTemp.Fields("CUST_REF_NO").Value))
                mPONo = CStr(IIf(IsDbNull(RsTemp.Fields("CUST_REF_NO").Value), "", RsTemp.Fields("CUST_REF_NO").Value))
                .Col = ColPODate
                mPODate = ""
                If MainClass.ValidateWithMasterTable(mPONo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPODate = Trim(MasterNo)
                End If
                .Text = VB6.Format(mPODate, "DD/MM/YYYY")
                .Col = ColPOWEF
                mWEFDate = GetPOWEF(mPONo, mItemCode, mBillDate)
                '            mPONo = mPONo & vb6.Format(Val(txtPOAmendNo.Text), "000")
                '            If MainClass.ValidateWithMasterTable(mPONo, "MKEY", "PO_WEF_DATE", "PUR_PURCHASE_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & mItemCode & "'") = True Then
                '                mWEFDate = Trim(MasterNo)
                '            End If
                .Text = VB6.Format(mWEFDate, "DD/MM/YYYY")
                .Col = ColCustRefNo
                .Text = ""
                .Col = ColCustRefDate
                .Text = ""
                .Col = ColRoundOff
                .Text = "0.00"
                .Col = ColMark
                .Text = ""
                .Col = ColMKEY
                .Text = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)

                .Col = ColLocation
                .Text = IIf(IsDBNull(RsTemp.Fields("LOCATION_ID").Value), "", RsTemp.Fields("LOCATION_ID").Value)

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
    Private Function GetPOWEF(ByRef pPONO As String, ByRef pItemCode As String, ByRef pBillDate As String) As String
        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        GetPOWEF = ""
        SqlStr = "SELECT MAX(ID.PO_WEF_DATE) AS PO_WEF_DATE" & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.AUTO_KEY_PO=" & Val(pPONO) & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND ID.PO_WEF_DATE <= TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetPOWEF = VB6.Format(IIf(IsDbNull(RsTemp.Fields("PO_WEF_DATE").Value), "", RsTemp.Fields("PO_WEF_DATE").Value), "DD/MM/YYYY")
        End If
        Exit Function
LedgError:
        GetPOWEF = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function InsertIntoTemp(ByRef mDivisionCode As Integer, ByRef mSuppCustCode As String) As Boolean
        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mItemCode As String
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAllTrnType As Boolean
        Dim mFyearFrom As Integer
        Dim mFyearTo As Integer
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mFyearFrom = GetCurrentFYNo(PubDBCn, VB6.Format(txtDateFrom.Text, "DD/MM/YYYY"))
        mFyearTo = GetCurrentFYNo(PubDBCn, VB6.Format(txtDateTo.Text, "DD/MM/YYYY"))
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
            End If
        Else
            mItemCode = ""
        End If
        SqlStr = "DELETE FROM TEMP_DNCN_PROCESS WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = " INSERT INTO TEMP_DNCN_PROCESS ( " & vbCrLf & " USER_ID, COMPANY_CODE, FYEAR, " & vbCrLf & " ITEM_CODE, SUPP_CUST_CODE, SUPP_CUST_NAME, " & vbCrLf & " ITEM_NAME, VNO, VDATE, " & vbCrLf & " BILLNO, BILLDATE, " & vbCrLf & " AUTO_KEY_MRR, MRRDATE, " & vbCrLf & " BILLQTY, RECDQTY, REOFFERQTY, RATE, " & vbCrLf & " DNCNRATE, SUPPRATE, NETRATE, " & vbCrLf & " CUST_REF_NO, CUST_REF_DATE, PORATE, " & vbCrLf & " MKEY,TDSPER,ESIPER,INVOICE_HEAD,SERIAL_NO,ITEM_UOM,DIV_CODE,ITEMTYPE,HSN_CODE, LOCATION_ID) "
        SqlStr = SqlStr & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', IH.COMPANY_CODE, IH.FYEAR, " & vbCrLf & " ID.ITEM_CODE, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " ID.ITEM_DESC, IH.VNO, IH.VDATE, " & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf & " IH.AUTO_KEY_MRR, IH.MRRDATE, " & vbCrLf & " ID.ITEM_QTY, (NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0)), 0, ID.ITEM_RATE, " & vbCrLf & " 0, 0, 0, " & vbCrLf & " ID.CUST_REF_NO, ID.CUST_REF_DATE, 0, " & vbCrLf & " IH.MKEY,TDSPER,ESIPER,ITYPE.NAME,ID.SUBROWNO,ID.ITEM_UOM,IH.DIV_CODE,IH.ITEMDESC, ID.HSNCODE, IH.BILL_TO_LOC_ID"
        'GETREOFFERQTY_NEW (IH.COMPANY_CODE, IH.AUTO_KEY_MRR, IH.MRRDATE, IH.SUPP_CUST_CODE,ID.ITEM_CODE,ID.CUST_REF_NO)
        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, " & vbCrLf & " INV_GATE_HDR GH, FIN_SUPP_CUST_MST CMST, FIN_INVTYPE_MST ITYPE"
        If lblGoodsService.Text = "S" Then
            SqlStr = SqlStr & vbCrLf & " , INV_GATEPASS_DET GD"
        End If
        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.FYEAR>=" & mFyearFrom & " AND IH.FYEAR<=" & mFyearTo & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND TRIM(IH.SUPP_CUST_CODE)=TRIM(CMST.SUPP_CUST_CODE)"
        ''ONLY CHECK PO....15-03-2008
        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=GH.COMPANY_CODE" & vbCrLf & " AND TRIM(IH.SUPP_CUST_CODE)=TRIM(GH.SUPP_CUST_CODE)" & vbCrLf & " AND IH.AUTO_KEY_MRR=GH.AUTO_KEY_MRR "
        If Trim(txtBillNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BILLNO='" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'"
        End If
        SqlStr = SqlStr & vbCrLf & " AND ID.COMPANY_CODE=ITYPE.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_TRNTYPE=ITYPE.CODE "
        '    If cboShowAgt.ListIndex = 0 Then
        If lblGoodsService.Text = "G" Then
            SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE='P'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ID.CUST_REF_NO=GD.AUTO_KEY_PASSNO"
            SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE='R'"
        End If
        '    ElseIf cboShowAgt.ListIndex = 1 Then
        '        Sqlstr = Sqlstr & vbCrLf & " AND GH.REF_TYPE='I'"
        '    ElseIf cboShowAgt.ListIndex = 2 Then
        '        Sqlstr = Sqlstr & vbCrLf & " AND GH.REF_TYPE='R'"
        '    Else
        '        Sqlstr = Sqlstr & vbCrLf & " AND GH.REF_TYPE NOT IN ('P','I','R')"
        '    End If
        If mDivisionCode <> CDbl("-1") Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If
        SqlStr = SqlStr & vbCrLf & "AND CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'"
        If lblGoodsService.Text = "G" Then
            If Val(txtPONo.Text) > 0 Then
                SqlStr = SqlStr & vbCrLf & "AND ID.CUST_REF_NO = '" & Val(txtPONo.Text) & "'"
            End If
        Else
            If Val(txtPONo.Text) > 0 Then
                SqlStr = SqlStr & vbCrLf & "AND GD.AUTO_KEY_WO = '" & Val(txtPONo.Text) & "'"
            End If
        End If
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            '        If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            '            mItemCode = MasterNo
            SqlStr = SqlStr & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            '        End If
        End If
        SqlStr = SqlStr & vbCrLf & "AND IH.AUTO_KEY_MRR<>-1 AND IH.TRNTYPE>0 AND IH.VNO<>'-1' AND IH.ISFINALPOST='Y' AND CANCELLED='N'"
        If optShowType(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MRRDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.MRRDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        '    Else
        '        Sqlstr = Sqlstr & vbCrLf _
        ''                & " AND IH.INVOICE_DATE>=TO_DATE('" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''                & " AND IH.INVOICE_DATE<=TO_DATE('" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"
        '    End If
        PubDBCn.Execute(SqlStr)
        SqlStr = "UPDATE TEMP_DNCN_PROCESS SET " ''& vbCrLf |
        If lblGoodsService.Text = "G" Then
            SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMPRICE_NEW(FYEAR, " & RsCompany.Fields("FYEAR").Value & ", MRRDATE,CUST_REF_NO,ITEM_CODE) "
        Else
            SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMJWRate(" & RsCompany.Fields("COMPANY_CODE").Value & ",1,MRRDATE,CUST_REF_NO,AUTO_KEY_MRR,ITEM_CODE,SERIAL_NO), " & vbCrLf & " CUST_REF_NO=GetITEMJWPONO(" & RsCompany.Fields("COMPANY_CODE").Value & ",1,MRRDATE,CUST_REF_NO,AUTO_KEY_MRR,ITEM_CODE,SERIAL_NO)"
            '        SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMJWRate(" & RsCompany.fields("COMPANY_CODE").value & ",1,MRRDATE,CUST_REF_NO,AUTO_KEY_MRR,ITEM_CODE,SERIAL_NO) "
        End If

        '    If cboShowAgt.ListIndex = 0 Then
        '        If optDate(0).Value = True Then
        '            SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMPRICE_NEW(FYEAR, " & RsCompany.fields("FYEAR").value & ", MRRDATE,CUST_REF_NO,ITEM_CODE) "
        '        Else
        '            Sqlstr = Sqlstr & vbCrLf & " PORATE=GetITEMPRICE_NEW(FYEAR, " & RsCompany.fields("FYEAR").value & ", BILLDATE,CUST_REF_NO,ITEM_CODE) "
        '        End If
        '    ElseIf cboShowAgt.ListIndex = 1 Then
        '        Sqlstr = Sqlstr & vbCrLf & " PORATE=GetSALEITEMPRICE(-1,CUST_REF_NO, SUPP_CUST_CODE,ITEM_CODE) "
        '    ElseIf cboShowAgt.ListIndex = 2 Then
        '        Sqlstr = Sqlstr & vbCrLf & " PORATE=GetITEMJWRate(" & RsCompany.fields("COMPANY_CODE").value & ",1,MRRDATE,CUST_REF_NO,AUTO_KEY_MRR,ITEM_CODE,SERIAL_NO) "
        '        ''GetITEMJWRate(AUTO_KEY_MRR,ITEM_CODE,SERIAL_NO)
        '    Else
        '        Sqlstr = Sqlstr & vbCrLf & " PORATE=GetITEMPRICE_NEW(FYEAR, " & RsCompany.fields("FYEAR").value & ", BILLDATE,-1,ITEM_CODE) "
        '    End If
        SqlStr = SqlStr & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = "UPDATE TEMP_DNCN_PROCESS SET " & vbCrLf & " REOFFERQTY=GETREOFFERQTY(COMPANY_CODE,AUTO_KEY_MRR,MRRDATE,SUPP_CUST_CODE,ITEM_CODE), " & vbCrLf & " RECDQTY=RECDQTY - GETLINEREJECTIONQTY(COMPANY_CODE,AUTO_KEY_MRR,MRRDATE,SUPP_CUST_CODE,ITEM_CODE)" & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)
        ''mCompanyCode NUMBER, mMRRNo Number, mMRRDate Char, mSupplierCode CHAR, mItemCode CHAR
        '    If cboShowAgt.ListIndex <> 3 Then
        SqlStr = "UPDATE TEMP_DNCN_PROCESS SET " & vbCrLf & " DNCNRATE=(NVL(GETDNCNRATE(COMPANY_CODE, FYEAR, SUPP_CUST_CODE, BILLNO, BILLDATE, ITEM_CODE,'R',CUST_REF_NO),0)) " & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = "UPDATE TEMP_DNCN_PROCESS SET " & vbCrLf & " SUPPRATE=(NVL(GETSUPPRATE(COMPANY_CODE, FYEAR, MKEY, SUPP_CUST_CODE, VNO, VDATE, ITEM_CODE,'R'),0)) " & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)
        '    End If
        PubDBCn.CommitTrans()
        InsertIntoTemp = True
        Exit Function
LedgError:
        'Resume
        InsertIntoTemp = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
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
    Private Function MakeSQL_S() As String
        On Error GoTo ERR1
        ''SELECT CLAUSE...
        '    If optType(0).Value = True Then
        MakeSQL_S = " SELECT ITEM_CODE,SUPP_CUST_NAME, ITEM_NAME, HSN_CODE," & vbCrLf & " VNO,VDATE, BILLNO, BILLDATE," & vbCrLf & " BILLQTY, " & vbCrLf & " TO_CHAR(RECDQTY+REOFFERQTY) AS ACCPETED, " & vbCrLf & " RATE, " & vbCrLf & " TO_CHAR(DNCNRATE) AS DNCN_RATE,  " & vbCrLf & " TO_CHAR(SUPPRATE) AS SUPP_RATE, " & vbCrLf & " '0.000', " & vbCrLf & " TO_CHAR(PORATE) AS PORATE," & vbCrLf & " '0.000', FYEAR, MRRDATE, CUST_REF_NO, AUTO_KEY_MRR,TDSPER,ESIPER,INVOICE_HEAD,MKEY,ITEM_UOM,DIV_CODE,ITEMTYPE,LOCATION_ID "
        '    Else
        '        MakeSQL_S = " SELECT ITEM_CODE,SUPP_CUST_NAME, ITEM_NAME, " & vbCrLf _
        ''                  & " '','', '', ''," & vbCrLf _
        ''                  & " SUM(BILLQTY) AS BILLQTY, " & vbCrLf _
        ''                  & " TO_CHAR(SUM(RECDQTY+REOFFERQTY)) AS ACCPETED, " & vbCrLf _
        ''                  & " SUM(RATE*BILLQTY) As RATE, " & vbCrLf _
        ''                  & " TO_CHAR(SUM(DNCNRATE)) AS DNCN_RATE,  " & vbCrLf _
        ''                  & " TO_CHAR(SUM(SUPPRATE)) AS SUPP_RATE, " & vbCrLf _
        ''                  & " '0.000', " & vbCrLf _
        ''                  & " SUM(PORATE * BILLQTY) AS PORATE," & vbCrLf _
        ''                  & " '0.000', '','','','0','0','','' "
        '    End If
        ''FROM CLAUSE...
        MakeSQL_S = MakeSQL_S & vbCrLf & " FROM TEMP_DNCN_PROCESS"
        ''WHERE CLAUSE...
        MakeSQL_S = MakeSQL_S & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        '    If cboShow.ListIndex = 1 Then
        '        MakeSQL_S = MakeSQL_S & vbCrLf _
        ''            & " AND PORATE< (RATE - DNCNRATE + SUPPRATE) "
        '    ElseIf cboShow.ListIndex = 2 Then
        MakeSQL_S = MakeSQL_S & vbCrLf & " AND PORATE > (RATE - DNCNRATE + SUPPRATE) "
        '    End If
        MakeSQL_S = MakeSQL_S & vbCrLf & "AND PORATE>0"
        MakeSQL_S = MakeSQL_S & vbCrLf & "AND RECDQTY+REOFFERQTY<>0"
        ''GROUP BY CLAUSE
        '    If optType(1).Value = True Then
        '            MakeSQL_S = MakeSQL_S & vbCrLf & " GROUP BY ITEM_CODE , SUPP_CUST_NAME, ITEM_NAME"
        '    End If
        ''ORDER CLAUSE...
        MakeSQL_S = MakeSQL_S & vbCrLf & "ORDER BY SUPP_CUST_NAME,BILLNO, BILLDATE,ITEM_NAME"
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
        Dim mSuppBillRate As Double
        Dim mSuppBillAmount As Double
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
                .Col = ColSuppBillRate
                mSuppBillRate = CDbl(VB6.Format(Val(.Text), "0.000"))
                .Col = ColSuppBillAmount
                mSuppBillAmount = CDbl(VB6.Format(mQty * mSuppBillRate, "0.00"))
                .Text = VB6.Format(mSuppBillAmount, "0.00")
                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)
                .Col = ColCGSTAmount
                mCGSTAmount = CDbl(VB6.Format(System.Math.Round(mSuppBillAmount * mCGSTPer * 0.01, 2), "0.00"))
                .Text = VB6.Format(mCGSTAmount, "0.00")
                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)
                .Col = ColSGSTAmount
                mSGSTAmount = CDbl(VB6.Format(System.Math.Round(mSuppBillAmount * mSGSTPer * 0.01, 2), "0.00"))
                .Text = VB6.Format(mSGSTAmount, "0.00")
                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)
                .Col = ColIGSTAmount
                mIGSTAmount = CDbl(VB6.Format(System.Math.Round(mSuppBillAmount * mIGSTPer * 0.01, 2), "0.00"))
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
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus
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
        '    If FYChk(CDate(txtDateTo.Text)) = False Then
        '        txtDateTo.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
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
        SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & vbCrLf & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " ''& vbCrLf |            & " AND ORDER_TYPE='O'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPOMain.EOF = False Then
            '        Clear1
            txtPONo.Text = IIf(IsDbNull(RsPOMain.Fields("AUTO_KEY_PO").Value), "", RsPOMain.Fields("AUTO_KEY_PO").Value)
            txtPODate.Text = VB6.Format(IIf(IsDbNull(RsPOMain.Fields("PUR_ORD_DATE").Value), "", RsPOMain.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY")
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
        If MainClass.ChkIsdateF(TxtVDate) = False Then
            TxtVDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(TxtVDate.Text))) = False Then
            TxtVDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdMain_Advance(sender As Object, e As AxFPSpreadADO._DSpreadEvents_AdvanceEvent) Handles SprdMain.Advance

    End Sub
End Class
