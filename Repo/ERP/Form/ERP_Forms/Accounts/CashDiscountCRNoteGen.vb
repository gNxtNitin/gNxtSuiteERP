Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO

Friend Class frmCashDiscountCRNoteGen
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim mAccountCode As String
    Private Const ColCustomerCode As Short = 1
    Private Const ColCustomerName As Short = 2
    Private Const ColBillNo As Short = 3
    Private Const ColBillDate As Short = 4
    Private Const ColCNAmount As Short = 5
    Private Const ColInvoiceType As Short = 6
    Private Const ColRemarks As Short = 7

    Private Const ColNarration As Short = 8
    Private Const ColCGSTPer As Short = 9
    Private Const ColCGSTAmount As Short = 10
    Private Const ColSGSTPer As Short = 11
    Private Const ColSGSTAmount As Short = 12
    Private Const ColIGSTPer As Short = 13
    Private Const ColIGSTAmount As Short = 14
    Private Const ColNetAmount As Short = 15
    Private Const ColDivision As Short = 16
    Private Const ColCreditNo As Short = 17
    Private Const ColMkey As Short = 18
    Private Const ColStatus As Short = 19

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
        mSubTitle = ""
        mTitle = mTitle & "-Detailed"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemWiseBillWise.RPT"
        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, ColMkey, PubDBCn) = False Then GoTo ReportErr
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
        Dim mItemType As String
        Dim mTRNType As String
        Dim mSuppCustCode As String = ""
        Dim mAccountCode As String = ""
        Dim mBookSubType As String
        Dim mAcceptedQty As Double
        Dim mLockBookCode As Integer
        Dim mRemarks As String = ""
        Dim mNarration As String
        Dim mCNAmount As Double

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
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mTRNType = MasterNo
        Else
            mTRNType = CStr(-1)
            MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
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
                .Col = ColStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .Col = ColCreditNo
                    If Trim(.Text) = "" Then
                        .Col = ColCustomerCode
                        mSuppCustCode = Trim(.Text)

                        .Col = ColBillNo
                        mBillNo = Trim(.Text)

                        .Col = ColBillDate
                        mBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                        .Col = ColRemarks
                        mRemarks = Trim(.Text)

                        .Col = ColNarration
                        mNarration = Trim(.Text)

                        .Col = ColCNAmount
                        mCNAmount = Val(.Text)
                        If mCNAmount > 0 Then
                            If UpdateMain1(cntRow, mBillNo, mBillDate, mSuppCustCode, mAccountCode, mCNAmount, mRemarks, mNarration, mBookSubType, mItemType, mTRNType) = False Then GoTo ErrPart
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
    Private Function UpdateMain1(ByRef pcntRow As Integer, ByRef mBillNo As String, ByRef mBillDate As String,
                                  ByRef mSuppCustCode As String, ByRef mAccountCode As String,
                                 ByRef mCNAmount As Double, ByRef mRemarks As String, ByRef mNarration As String, ByRef mBookSubType As String, ByRef mItemType As String, ByRef mTRNType As String) As Boolean

        On Error GoTo ErrPart
        'Dim I As Integer
        Dim SqlStr As String
        Dim nMkey As String
        '
        Dim mVNoSeq As Double
        Dim mVNo As String
        Dim mVDate As String
        Dim mFREIGHTCHARGES As String

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
        Dim mLocationID As String
        Dim mCurRowNo As Integer
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mMark As String
        Dim mGSTApp As String


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



        If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = MasterNo
        Else
            mDivisionCode = 1

        End If
        mLocationID = GetDefaultLocation(mSuppCustCode)

        mNETVALUE = mCNAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount
        mTOTTAXABLEAMOUNT = Val(CStr(mCNAmount))
        mBookType = "L"
        If mNETVALUE > 0 Then
            mVNoSeq = Val(AutoGenSeqBillNo())
            mVNo = VB.Left(ConSaleDebit, 1) & VB6.Format(Val(CStr(mVNoSeq)), "0000000")
            mVDate = VB6.Format(txtVDate.Text, "DD/MM/YYYY") ''PubCurrDate
            mBookType = VB.Left(ConSaleDebit, 1)
            SqlStr = ""
            mCurRowNo = MainClass.AutoGenRowNo("FIN_SUPP_PUR_HDR", "RowNo", PubDBCn)

            nMkey = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value) & RsCompany.Fields("FYEAR").Value & mCurRowNo

            SqlStr = "INSERT INTO FIN_SUPP_SALE_HDR( " & vbCrLf _
                & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _
                & " ROWNO, TRNTYPE, VNOSEQ, " & vbCrLf _
                & " VNO, VDATE, BILLNO, " & vbCrLf _
                & " INVOICE_DATE, AUTO_KEY_SO, SO_DATE, " & vbCrLf _
                & " AMEND_NO, SO_WEFDATE, SUPP_CUST_CODE, " & vbCrLf _
                & " ACCOUNTCODE, TARIFFHEADING, BOOKTYPE, " & vbCrLf _
                & " BOOKSUBTYPE, REMARKS, ITEMDESC, " & vbCrLf _
                & " ITEMVALUE, STPERCENT, TOTSTAMT, " & vbCrLf _
                & " TOTFREIGHT, TOTCHARGES, EDPERCENT, " & vbCrLf _
                & " TOTEDAMOUNT, TOTSURCHARGEAMT, TOTDISCAMOUNT, " & vbCrLf _
                & " TOTMSCAMOUNT, TOTRO, TOTEXPAMT," & vbCrLf _
                & " TOTTAXABLEAMOUNT, NETVALUE, TOTQTY," & vbCrLf _
                & " STTYPE, STFORMCODE, STFORMNAME, "

            SqlStr = SqlStr & vbCrLf _
                & " STFORMDATE, STDUEFORMCODE, STDUEFORMNAME, " & vbCrLf _
                & " STDUEFORMDATE, ISREGDNO, LSTCST, " & vbCrLf _
                & " WITHFORM, CANCELLED, NARRATION," & vbCrLf _
                & " JVNO, JVMKEY, ISFINALPOST, " & vbCrLf _
                & " PAYMENTDATE, TOTEDUPERCENT, TOTEDUAMOUNT, " & vbCrLf _
                & " CESSABLEAMOUNT, TO_DATE, SHECPERCENT," & vbCrLf _
                & " SHECAMOUNT, " & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf _
                & " MODUSER, MODDATE,DIV_CODE, " & vbCrLf _
                & " TOTCGST_AMOUNT, TOTSGST_AMOUNT, TOTIGST_AMOUNT,GST_APP,REASON,GOODS_SERVICE,BILL_TO_LOC_ID,IS_ITEMDETAIL "

            SqlStr = SqlStr & vbCrLf _
                & " ) VALUES ( " & vbCrLf _
                & " '" & nMkey & "', " & RsCompany.Fields("Company_Code").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & mCurRowNo & ", " & Val(mTRNType) & ", " & mVNoSeq & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mVNo) & "', TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mBillNo) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), -1, TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & "  0, TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mSuppCustCode & "'," & vbCrLf _
                & "  '" & mAccountCode & "', '', '" & mBookType & "'," & vbCrLf _
                & "  '" & mBookSubType & "', '" & MainClass.AllowSingleQuote(mRemarks) & "', '" & MainClass.AllowSingleQuote(mItemType) & "'," & vbCrLf _
                & "  " & mCNAmount & ", " & mSTPERCENT & ", " & mTOTSTAMT & "," & vbCrLf _
                & "  " & mTOTFREIGHT & ", " & mTOTCHARGES & ", " & mEDPERCENT & ", " & vbCrLf _
                & "  " & mTotEDAmount & ", " & mSURAmount & ", " & mTotDiscount & "," & vbCrLf _
                & "  " & mMSC & ", " & mRO & ", " & mTOTEXPAMT & "," & vbCrLf _
                & "  " & mTOTTAXABLEAMOUNT & ", " & mNETVALUE & ", " & mTotQty & "," & vbCrLf _
                & "  '" & mSTType & "', " & mFormRecdCode & ", '',"

            SqlStr = SqlStr & vbCrLf _
                & "  '', " & mFormDueCode & ",'', " & vbCrLf _
                & " '', '" & mIsRegdNo & "', '" & mLSTCST & "'," & vbCrLf _
                & " '" & mWITHFORM & "', '" & mCancelled & "', '" & MainClass.AllowSingleQuote(mNarration) & "'," & vbCrLf _
                & " '" & pJVVnoStr & "', '" & pJVMKey & "', '" & mFinalPost & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') ,0,0,  " & vbCrLf _
                & " 0, TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 0," & vbCrLf _
                & " 0, " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                & " '', '', " & mDivisionCode & "," & vbCrLf _
                & " " & Val(CStr(mCGSTAmount)) & ", " & Val(CStr(mSGSTAmount)) & "," & Val(CStr(mIGSTAmount)) & ", " & vbCrLf _
                & " '" & mGSTApp & "','3','G','" & mLocationID & "','N') "

            PubDBCn.Execute(SqlStr)

            'If UpdateDetail1(nMkey, pInvoiceNo, mNarration, mVNo, mVDate, mSuppCustCode, mAccountCode, mDivisionCode, mBookType, mBookSubType, mBillNo, mBillDate, mTOTTAXABLEAMOUNT, mCGSTAmount, mSGSTAmount, mIGSTAmount) = False Then GoTo ErrPart

            'If SalePostTRN_GST(PubDBCn, nMkey, mCurRowNo, CStr(ConSaleDebitBookCode), mBookType, mBookSubType, mVNo, 
            'mVDate, mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mNETVALUE)),
            'False, pDueDate, False, "", False, "", 0, Val(CStr(mTOTEXPAMT)),
            'Val(CStr(mCGSTAmount)), Val(CStr(mIGSTAmount)), Val(CStr(mSGSTAmount)), True,
            'PubUserID, VB6.Format(PubCurrDate, "DD-MMM-YYYY"), Val(CStr(mCNAmount)), mDivisionCode, CStr(0), 0, 0, 0, mBillNo, mBillDate) = False
            'Then GoTo ErrPart

            If SalePostTRN_GST(PubDBCn, nMkey, mCurRowNo, CStr(ConSaleDebitBookCode), mBookType, mBookSubType, mVNo, (txtVDate.Text),
                                               mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mNETVALUE)), False,
                                               pDueDate, False, mRemarks, False, "", 0, 0, 0, 0,
                                               0, True, PubUserID, VB6.Format(PubCurrDate, "DD-MMM-YYYY"), Val(CStr(mCNAmount)), mDivisionCode, CStr(0), 0, 0, 0, mLocationID, mBillNo,
                                               mBillDate, True, True, 0, mNarration) = False Then GoTo ErrPart


        End If
        With SprdMain
            .Row = pcntRow
            .Col = ColCreditNo
            .Text = mVNo
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
                mIdentification = IIf(IsDBNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)
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
                    SqlStr = "Insert Into  FIN_SUPP_SALE_EXP (MKEY,SUBROWNO, " & vbCrLf _
                        & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DebitAmount) " & vbCrLf _
                        & " Values ('" & pMKey & "'," & I & ", " & vbCrLf _
                        & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf _
                        & " " & mCalcOn & ",'" & mRO & "'," & mDebitAmount & ")"
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
    Public Function AutoCreditNoteNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String
        Dim mPreFix As String
        Dim mPrefixLen As Long

        'mPreFix = GetDocumentPrefix("P", IIf(LblBookCode.Text = ConSaleCreditBookCode, "M", "R"), cboDivision.Text)     ''GetDocumentPrefix("P", "R")

        mPreFix = GetDocumentPrefix("P", "M", cboDivision.Text)     ''GetDocumentPrefix("P", "R")

        'GetDocumentPrefix("P", IIf(LblBookCode.Text = ConSaleCreditBookCode, "M", "R"), cboDivision.Text)

        mPrefixLen = IIf(Trim(mPreFix) = "", 0, Len(Trim(mPreFix)))
        SqlStr = ""
        ''select BILLNO, NVL(LENGTH(BILLNOPREFIX),0), LENGTH(BILLNO),SUBSTR(REJ_CREDITNOTE,NVL(LENGTH(BILLNOPREFIX),0)+1,LENGTH(REJ_CREDITNOTE)-NVL(LENGTH(BILLNOPREFIX),0)),

        SqlStr = "SELECT MAX(MaxNo)  AS MaxNo FROM ("

        SqlStr = SqlStr & vbCrLf _
            & " Select Max(TO_NUMBER(SUBSTR(REJ_CREDITNOTE," & mPrefixLen + 1 & ",LENGTH(REJ_CREDITNOTE)-" & mPrefixLen & "))) As MaxNo " & vbCrLf _
            & " FROM FIN_PURCHASE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And FYEAR =" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " And PURCHASESEQTYPE=2"

        If mPreFix <> "" Then
            SqlStr = SqlStr & vbCrLf & " And SUBSTR(REJ_CREDITNOTE,1," & mPrefixLen & ")='" & mPreFix & "'"
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 103 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then

            SqlStr = SqlStr & vbCrLf & " UNION ALL"

            SqlStr = SqlStr & vbCrLf _
                & "SELECT Max(TO_NUMBER(SUBSTR(PARTY_DNCN_NO," & mPrefixLen + 1 & ",LENGTH(PARTY_DNCN_NO)-" & mPrefixLen & "))) AS MaxNo " & vbCrLf _
                & " FROM FIN_SUPP_SALE_HDR " & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND FYEAR =" & RsCompany.Fields("FYEAR").Value & ""

            If mPreFix <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND SUBSTR(PARTY_DNCN_NO,1," & mPrefixLen & ")='" & mPreFix & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " )"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = mMaxValue
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            AutoCreditNoteNo = mPreFix & VB6.Format(mNewSeqNo, ConBillFormat)   '' mStartingSNo = CDbl(VB6.Format(pStartingSNo, ConBillFormat))
        Else
            AutoCreditNoteNo = mPreFix & mNewSeqNo
        End If


        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqBillNo() As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsSuppPurchMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Double
        Dim SqlStr As String
        Dim mStartingSNo As Double
        Dim pStartingSNo As Double
        Dim xFYear As Integer
        Dim mMAxNo As Double
        Dim mMonth As String

        SqlStr = ""

        ''
        Dim mStartMonth As String
        Dim mEndMonth As String


        pStartingSNo = 1
        If RsCompany.Fields("FYEAR").Value >= 2024 And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            mMonth = VB6.Format(txtVDate.Text, "YYMM")
            mStartMonth = "01/" & VB6.Format(txtVDate.Text, "MM/YYYY")
            mEndMonth = MainClass.LastDay(Month(txtVDate.Text), Year(txtVDate.Text)) & "/" & VB6.Format(txtVDate.Text, "MM/YYYY")

            xFYear = Val(mMonth)       '' CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))

            mStartingSNo = CDbl(xFYear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & VB6.Format(pStartingSNo, "0000"))

        Else
            xFYear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))

            mStartingSNo = CDbl(xFYear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & VB6.Format(pStartingSNo, "00000"))

        End If



        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then    ''Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104
        '    mStartingSNo = 1
        'Else
        'End If

        SqlStr = "SELECT Max(VNOSEQ)  FROM FIN_SUPP_SALE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND BOOKTYPE='L'"

        ''AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'

        If RsCompany.Fields("FYEAR").Value >= 2024 And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            SqlStr = SqlStr & vbCrLf _
                & " AND VDATE>=TO_DATE('" & VB6.Format(mStartMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND VDATE<=TO_DATE('" & VB6.Format(mEndMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        ''SqlStr = SqlStr & vbCrLf & " AND VDATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSuppPurchMainGen
            If .EOF = False Then
                mMAxNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mMAxNo = -1 Then
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
    Private Sub frmCashDiscountCRNoteGen_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmCashDiscountCRNoteGen_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND CATEGORY='S' ORDER BY NAME "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While Not RS.EOF
                cboInvType.Items.Add(RS.Fields("NAME").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.Items.Clear()
        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY DIV_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboDivision.SelectedIndex = 0

        Call PrintStatus(True)
        'optGSTApp(0).Enabled = True 'IIf(PubSuperUser = "S", True, False)
        Call FormatSprdMain(-1)
        Call frmCashDiscountCRNoteGen_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmCashDiscountCRNoteGen_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
    Private Sub frmCashDiscountCRNoteGen_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColStatus
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)
            .set_RowHeight(-1, RowHeight)

            .Row = -1



            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustomerCode, 7)

            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerName, 35)
            .ColsFrozen = ColCustomerName

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

            .Col = ColCNAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColCNAmount, 7)

            .Col = ColInvoiceType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInvoiceType, 8)
            .ColHidden = True

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 15)

            .Col = ColNarration
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColNarration, 25)

            For cntCol = ColCGSTPer To ColNetAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 7)
                .ColHidden = True
            Next

            .Col = ColDivision
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDivision, 8)
            .ColHidden = True

            .Col = ColCreditNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCreditNo, 12)

            .Col = ColMkey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMkey, 8)
            .ColHidden = True

            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColStatus, 8)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColCreditNo, ColMkey)

            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        'If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        'If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        ''    If chkAll.Value = vbUnchecked Then
        'If Trim(TxtAccount.Text) = "" Then
        '    MsgInformation("Invaild Account Name")
        '    TxtAccount.Focus()
        '    FieldsVerification = False
        '    Exit Function
        'End If
        'If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mAccountCode = MasterNo
        'Else
        '    MsgInformation("Invaild Account Name")
        '    TxtAccount.Focus()
        '    FieldsVerification = False
        '    Exit Function
        'End If
        '    End If
        If MainClass.ChkIsdateF(txtVDate) = False Then Exit Function
        If FYChk(CStr(CDate(txtVDate.Text))) = False Then txtVDate.Focus()

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
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

    Private Sub cmdInsertRow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdInsertRow.Click
        On Error GoTo ErrPart
        Dim mMaxRow As Integer
        Dim ColRow As Integer

        mMaxRow = Val(txtRows.Text)
        If Val(CStr(mMaxRow)) <= 1 Then
            Exit Sub
        End If
        With SprdMain
            For ColRow = 1 To mMaxRow
                .MaxRows = ColRow ''.MaxRows + 1						
                .Row = .MaxRows
                .Action = SS_ACTION_INSERT_ROW
                .set_RowHeight(.MaxRows, 15)
            Next
        End With
        FormatSprdMain(-1)
        cmdSave.Enabled = True
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        cmdSave.Enabled = False
    End Sub

    Private Sub cmdShow_Click(sender As Object, e As EventArgs) Handles cmdShow.Click

        txtRows.Text = ""
        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)
        cmdSave.Enabled = True
    End Sub

    Private Sub SprdMain_Change(sender As Object, e As _DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        cmdSave.Enabled = True
    End Sub
End Class
