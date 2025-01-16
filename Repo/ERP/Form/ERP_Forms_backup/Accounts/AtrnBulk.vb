Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAtrnBulk
    Inherits System.Windows.Forms.Form
    Private RsTRNMain As ADODB.Recordset '' ADODB.Recordset						
    Private RsTRNDetail As ADODB.Recordset ''ADODB.Recordset						
    Private XRIGHT As String
    Private ADDMode As Boolean
    Private MODIFYMode As Boolean
    Private FormLoaded As Boolean

    Dim PaymentDetailShow As Boolean

    Private Const ColAccountCode As Short = 1
    Private Const ColAccountName As Short = 2
    Private Const ColChqNo As Short = 3
    Private Const ColParticulars As Short = 4
    Private Const ColAmount As Short = 5
    Private Const ColBillDetails As Short = 6
    Private Const ColBalAmount_PayTerms As Short = 7
    Private Const ColBalanceAmount As Short = 8
    Private Const ColVNo As Short = 9
    Private Const ColVDate As Short = 10
    Private Const ColSuppBankName As Short = 11
    Private Const ColProcessKey As Short = 12
    Private Const ColMKEY As Short = 13


    Private Const ConRowHeight As Short = 15
    Dim mAuthorised As String

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        txtPartyName.Text = ""
        lblBookBalAmt.Text = ""
        lblBookBalDC.Text = ""
        TxtVDate.Text = ""
        txtRows.Text = ""
        MainClass.ClearGrid(sprdMain)
        FormatSprdMain(-1)
        ConPaymentDetail = False
        cmdSave.Enabled = False

    End Sub

    Private Sub cmdBillDetails_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBillDetails.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mAccountCode As String
        Dim mAccountName As String
        Dim mAmount As Double
        Dim mBalanceAmount As Double
        Dim mBalanceAmountAdohc As Double
        Dim pProcessKey As Double
        Dim mAccountCount As Integer
        Dim I As Integer
        Dim mSuppBankName As String

        '' Fill Account Code						
        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAccountCode
                mAccountCode = Trim(.Text)
                If mAccountCode = "" Then
                    .Col = ColAccountName
                    mAccountName = UCase(Trim(.Text))
                    If mAccountName = "" Then
                        MsgInformation("Account Name / Code is Blank")
                        Exit Sub
                    Else
                        If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mAccountCode = MasterNo
                            .Col = ColAccountCode
                            .Text = mAccountCode
                        Else
                            MsgInformation("Invaild Account Name")
                            Exit Sub
                        End If
                    End If
                Else
                    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountName = MasterNo
                        .Col = ColAccountName
                        .Text = mAccountName
                    Else
                        MsgInformation("Invaild Account Code")
                        Exit Sub
                    End If
                End If
            Next
        End With


        'Check Duplicate Account						
        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAccountCode
                mAccountCode = Trim(.Text)
                mAccountCount = 0
                For I = 1 To .MaxRows
                    .Row = I
                    .Col = ColAccountCode
                    If mAccountCode = Trim(.Text) Then
                        mAccountCount = mAccountCount + 1
                    End If
                    If mAccountCount > 1 Then
                        MsgInformation("Duplicate Party Code.")
                        Exit Sub
                    End If
                Next
            Next
        End With

        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAccountCode
                mAccountCode = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = MasterNo
                Else
                    mAccountName = ""
                End If

                .Col = ColAccountName
                .Text = Trim(mAccountName)

                .Col = ColSuppBankName
                mSuppBankName = GetSupplierBankName(mAccountCode)
                .Text = Trim(mSuppBankName)

                .Col = ColAmount
                mAmount = Val(.Text)

                mBalanceAmountAdohc = GetOutsAdhoc(mAccountCode, VB6.Format(PubCurrDate, "DD/MM/YYYY"))
                .Col = ColBalAmount_PayTerms
                .Text = VB6.Format(mBalanceAmountAdohc, "0.00")

                mBalanceAmount = GetOpeningBal(mAccountCode, VB6.Format(PubCurrDate, "DD/MM/YYYY"))
                .Col = ColBalanceAmount
                .Text = VB6.Format(mBalanceAmount, "0.00")

                If mBalanceAmount >= 0 Then
                    If MsgQuestion("Balance Amount of party name : (" & mAccountName & " - [Line No : " & cntRow & "]) is already Debit. Are you want to give him Advance?") = CStr(MsgBoxResult.No) Then
                        Exit Sub
                    End If
                Else
                    If mAmount > mBalanceAmountAdohc * -1 Then
                        If MsgQuestion("Balance Overdue Amount of party name : (" & mAccountName & " - [Line No : " & cntRow & "]) is less than pay amount. Are you want to give him excess payment?") = CStr(MsgBoxResult.No) Then
                            Exit Sub
                        End If
                    Else
                        If mAmount > mBalanceAmount * -1 Then
                            If MsgQuestion("Balance Amount of party name : (" & mAccountName & " - [Line No : " & cntRow & "]) is less than pay amount. Are you want to give him Advance?") = CStr(MsgBoxResult.No) Then
                                Exit Sub
                            End If
                        End If
                    End If
                End If

                If Val(CStr(mAmount)) = 0 Then
                    MsgInformation("Amount Cann't be blank.")
                    Exit Sub
                Else
                    .Col = ColProcessKey
                    If Val(.Text) = 0 Then
                        pProcessKey = MainClass.AutoGenRowNo("TEMP_DSP_DESPATCH", "RowNo", PubDBCn)
                        If FillTempBillDetails(mAccountCode, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mAmount, pProcessKey) = False Then GoTo ErrPart
                    Else
                        pProcessKey = Val(.Text)
                    End If

                    .Col = ColProcessKey
                    .Text = CStr(pProcessKey)
                End If

                '            lblAcBalAmt.text = Format(Abs(mOPBal), "0.00")						
                '            lblAcBalDC.text = IIf(mOPBal >= 0, "Dr", "Cr")						

            Next
        End With
        MainClass.ProtectCell(sprdMain, 1, sprdMain.MaxRows, ColAccountCode, ColAmount)
        MainClass.ProtectCell(sprdMain, 1, sprdMain.MaxRows, ColBalanceAmount, ColMKEY)
        ConPaymentDetail = True
        cmdSave.Enabled = True
        Exit Sub
ErrPart:
        MsgInformation(Err.Number & "-" & Err.Description)
    End Sub

    Private Function GetOutsAdhoc(ByRef pAccountCode As String, ByRef pVDate As String) As Object
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mDateCond As String
        Dim mDueDate As String

        Dim mPaymentTerm As String

        mPaymentTerm = "DECODE(ADHOC_PAY_TERMS,0,DECODE(TO_DAYS,NULL,0,TO_DAYS),ADHOC_PAY_TERMS)"

        SqlStr = " Select SUM(BALANCE * DECODE(DC,'CR',-1,1)) AS CLBal" ''CASE WHEN TRN.BILLDATE <= '" & VB6.Format(pVDate, "DD-MMM-YYYY") & "' THEN						
        SqlStr = SqlStr & vbCrLf & " FROM vw_FIN_PAYMENT_ADV TRN, FIN_SUPP_CUST_MST CH, FIN_PAYTERM_MST PMST "

        SqlStr = SqlStr & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code=CH.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=CH.SUPP_CUST_Code " & vbCrLf & " AND CH.Company_Code=PMST.Company_Code(+) " & vbCrLf & " AND CH.PAYMENT_CODE=PMST.PAY_TERM_CODE(+) "

        ''mDateCond = "CASE WHEN BILLTYPE = 'B' OR BILLTYPE = 'D' OR BILLTYPE = 'C' OR BILLTYPE = 'T' THEN TO_CHAR(ADD_MONTHS(TRN.BILLDATE , + " & mMonth & ") ,'YYYYMMDD') ELSE TO_CHAR(VDATE,'YYYYMMDD') END"						

        SqlStr = SqlStr & vbCrLf & " AND TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(pAccountCode) & "'"
        mDateCond = "TO_CHAR(TRN.BILLDATE  + " & mPaymentTerm & " ,'YYYYMMDD')"
        SqlStr = SqlStr & vbCrLf & " AND " & mDateCond & " < '" & VB6.Format(pVDate, "YYYYMMDD") & "'"

        '    SqlStr = SqlStr & vbCrLf & " AND TRN.BILLDATE <= '" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "'"						
        '						
        '    SqlStr = SqlStr & vbCrLf _						
        ''            & " GROUP BY CH.SUPP_CUST_Code, TRN.PARTYNAME, " & mDueDate & ",CH.PAYMENT_MODE, TO_NUMBER(ACTIVITY)"						
        '						
        '    SqlStr = SqlStr & vbCrLf & " HAVING SUM(TRN.BALANCE * DECODE(DC,'DR',1,-1))<0 "						
        '						
        '						
        '    SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.PARTYNAME"						
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetOutsAdhoc = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CLBal").Value), 0, RsTemp.Fields("CLBal").Value), "0.00")
        Else
            GetOutsAdhoc = 0
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then						
        '        'PvtDBCn.Close						
        '        'Set PvtDBCn = Nothing						
        '    End If						

        If ADDMode = True Or MODIFYMode = True Then
            If MsgQuestion("Are you sure to Exit") = CStr(MsgBoxResult.No) Then
                Exit Sub
            End If
        End If

        RsTRNMain.Close()
        RsTRNMain = Nothing

        RsTRNDetail.Close()
        RsTRNDetail = Nothing
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub


    Private Sub cmdInsertRow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdInsertRow.Click
        On Error GoTo ErrPart
        Dim mMaxRow As Integer
        Dim ColRow As Integer

        mMaxRow = Val(txtRows.Text)
        If Val(CStr(mMaxRow)) <= 1 Then
            Exit Sub
        End If
        With sprdMain
            For ColRow = 1 To mMaxRow
                .MaxRows = ColRow ''.MaxRows + 1						
                .Row = .MaxRows
                .Action = SS_ACTION_INSERT_ROW
                .set_RowHeight(.MaxRows, ConRowHeight)
            Next
        End With
        FormatSprdMain(-1)
        cmdSave.Enabled = False
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        cmdSave.Enabled = False
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    Call ReportOnTrnVoucher(crptToWindow)						

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    Call ReportOnTrnVoucher(crptToPrinter)						
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo SaveErrPart
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mVNo As String
        Dim cntRow As Integer
        Dim mBankName As String
        Dim mBankCode As String
        Dim mVType As String

        Dim mAccountCode As String
        Dim mAccountName As String
        Dim mParticulars As String
        Dim mAmount As Double
        Dim mVDate As String
        Dim mMkey As String
        Dim pProcessKey As Double
        Dim mChqNo As String

        If ConPaymentDetail = False Then
            MsgInformation("Please Calculate the Bill Details")
            Exit Sub
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        If FieldsVerification() = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mBankName = Trim(txtPartyName.Text)
        If MainClass.ValidateWithMasterTable(mBankName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBankCode = MasterNo
        Else
            mBankCode = "-1"
            MsgInformation("Please Select the valid Bank Name.")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(mBankName, "VNAME", "VTYPE", "FIN_VOUCHERTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'") = True Then
            mVType = MasterNo
        Else
            mVType = "-1"
            MsgInformation("Please Select the valid Voucher Type.")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAccountCode
                mAccountCode = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = MasterNo
                Else
                    mAccountCode = "-1"
                    mAccountName = ""
                    MsgInformation("Please Select the valid Party Name.")
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Sub
                End If

                .Col = ColAccountName
                .Text = Trim(mAccountName)

                .Col = ColParticulars
                mParticulars = Trim(UCase(.Text))

                .Col = ColChqNo
                mChqNo = Trim(UCase(.Text))

                .Col = ColAmount
                mAmount = Val(.Text)

                .Col = ColProcessKey
                pProcessKey = Val(.Text)

                If Update1(mBankCode, mAccountCode, mAccountName, mChqNo, mParticulars, mAmount, mVNo, mVDate, mMkey, pProcessKey) = False Then GoTo SaveErrPart

                .Col = ColVNo
                .Text = mVNo

                .Col = ColVDate
                .Text = VB6.Format(mVDate, "DD/MM/YYYY")

                .Col = ColMKEY
                .Text = mMkey


            Next
        End With

        PubDBCn.CommitTrans()
        cmdSave.Enabled = False

        '    If Update1 = True Then						
        '        If chkChqDeposit.Value = vbUnchecked Then						
        '            txtVNo_Validate False						
        '        Else						
        '            Clear1						
        '            SqlStr = " Select VNO From FIN_VOUCHER_HDR WHERE " & vbCrLf _						
        ''                & " MKEY='" & CurMKey & "'"						
        '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly						
        '						
        '            If RsTemp.EOF = False Then						
        '                mVNo = IIf(IsNull(RsTemp!VNO), "", RsTemp!VNO)						
        '                MsgBox "PDC is Normalization New Voucher No Is " & mVNo, vbInformation						
        '            End If						
        '        End If						
        '    Else						
        '        MsgInformation "Record not Saved"						
        '    End If						
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
SaveErrPart:
        PubDBCn.RollbackTrans()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Function Update1(ByRef mBankCode As String, ByRef mAccountCode As String, ByRef mAccountName As String, ByRef mChqNo As String, ByRef mParticulars As String, ByRef mAmount As Double, ByRef mVnoStr As String, ByRef mVDate As String, ByRef mMkey As String, ByRef pProcessKey As Double) As Boolean
        On Error GoTo ErrPart
        Dim mBookCode As String
        Dim SqlStr As String
        Dim mDrCr As String
        Dim mVAmount As Double
        Dim mRowNo As Integer
        Dim CurMKey As String
        Dim mVNo As String
        Dim mVType As String

        Dim mVNoPrefix As String
        Dim mVNoSuffix As String

        Dim mBookType As String
        Dim mBookSubType As String
        Dim mCancelled As String

        Dim mISTDSDEDUCT As String
        Dim mISESIDEDUCT As String
        Dim mISSTDSDEDUCT As String
        Dim mIsSuppBill As String
        Dim mIsCapital As String

        Dim mExpPartyCode As String
        Dim mImpPartyCode As String
        Dim mExpDate As String

        Dim mISMODVAT As String
        Dim mIsPLA As String
        Dim mIsSTClaim As String
        Dim mIsServtaxClaim As String
        Dim mIsServTaxRefund As String
        Dim mNoOfEMI As String
        Dim mTotalNoOfEMI As Integer
        Dim I As Integer

        Dim mPLFlag As String

        Dim cntRow As Integer
        Dim xSqlStr As String
        Dim RSDiv As ADODB.Recordset
        Dim mDC As String
        Dim mDivCode As Double
        Dim mChkDivCode As Double
        Dim mSubRowNo As Integer

        Dim mSuppCustName As String
        Dim mSuppCustAmount As Double
        Dim mPRowNo As Integer
        Dim mServiceCode As Double
        Dim mReverseChargeApp As String
        Dim pNarration As String

        mBookType = VB.Left(lblBookType.Text, 1)
        mBookSubType = VB.Right(lblBookType.Text, 1)
        mVType = MainClass.AllowSingleQuote(Trim(txtVType.Text))
        mExpDate = VB6.Format(TxtVDate.Text, "DD/MM/YYYY")

        mVNo = GenVno()

        mVNoPrefix = GenPrefixVNo(TxtVDate.Text)
        mVNoSuffix = ""
        mVnoStr = mVType & mVNoPrefix & mVNo & mVNoSuffix
        mVDate = VB6.Format(TxtVDate.Text, "DD-MMM-YYYY")
        mCancelled = "N"
        mPLFlag = "N"
        pNarration = mParticulars

        mIsSuppBill = "N"
        mIsCapital = "N"

        mISMODVAT = "N"
        mIsPLA = "N"
        mIsSTClaim = "N"
        mIsServtaxClaim = "N"
        mIsServTaxRefund = "N"

        mBookCode = mBankCode
        mExpPartyCode = ""
        mImpPartyCode = ""


        mISTDSDEDUCT = "N"
        mISESIDEDUCT = "N"
        mISSTDSDEDUCT = "N"
        mReverseChargeApp = "N"
        mServiceCode = -1

        mRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
        CurMKey = RsCompany.Fields("COMPANY_CODE").Value & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(mRowNo)
        mMkey = CurMKey

        SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf & " Mkey, COMPANY_CODE, " & vbCrLf & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf & " Vno, Vdate, BookType,BookSubType, " & vbCrLf & " BookCode, Narration, CANCELLED, " & vbCrLf & " ISTDSDEDUCT,TDSPER,TDSAMOUNT, " & vbCrLf & " ISESIDEDUCT,ESIPER,ESIAMOUNT, ISSTDSDEDUCT,STDSPER,STDSAMOUNT," & vbCrLf & " ISSUPPBILL,MODVATNO ,STREFUNDNO, ISCAPITAL," & vbCrLf & " IMP_SUPP_CUST_CODE, IMP_MRR_NO, " & vbCrLf & " IMP_BILL_NO, IMP_BILL_DATE,  " & vbCrLf & " EXP_SUPP_CUST_CODE, EXP_BILL_NO,  " & vbCrLf & " EXP_BILL_DATE, AUTHORISED, " & vbCrLf & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM,EXPDATE, " & vbCrLf & " ISMODVAT, ISPLA, ISSTCLAIM, ISSERVTAXCLAIM, ISSERVTAXREFUND, SERVNO, PL_FLAG, " & vbCrLf & " SERVICE_CODE, SERVICE_ON_AMT, SERVICE_TAX_PER, " & vbCrLf & " SERVICE_TAX_AMOUNT, SERV_PROVIDER_PER, SERV_RECIPIENT_PER,REVERSE_CHARGE_APP, " & vbCrLf & " IS_REVERSAL_MADE, IS_REVERSAL_VOUCHER, REVERSAL_MKEY) VALUES ( "


        SqlStr = SqlStr & vbCrLf _
            & " '" & CurMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
            & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mRowNo & ", " & vbCrLf _
            & " '" & mVType & "', '" & mVNoPrefix & "', " & vbCrLf & " " & Val(mVNo) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf _
            & " TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf _
            & " '" & mBookCode & "', '" & MainClass.AllowSingleQuote(pNarration) & "', '" & mCancelled & "', " & vbCrLf _
            & " '" & mISTDSDEDUCT & "',0, 0, " & vbCrLf & " '" & mISESIDEDUCT & "',0, 0, " & vbCrLf & " '" & mISSTDSDEDUCT & "',0, 0, " & vbCrLf _
            & " '" & mIsSuppBill & "',0, 0, " & vbCrLf & " '" & mIsCapital & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mImpPartyCode) & "', " & "Null" & "," & vbCrLf _
            & " '', ''," & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(mExpPartyCode) & "', " & "Null" & "," & vbCrLf _
            & " '', 'N', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','H', " & vbCrLf & " TO_DATE('" & VB6.Format(mExpDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mISMODVAT & "','" & mIsPLA & "','" & mIsSTClaim & "','" & mIsServtaxClaim & "'," & vbCrLf & " '" & mIsServTaxRefund & "',0,'" & mPLFlag & "', " & vbCrLf & " " & IIf(mServiceCode = -1, "NULL", mServiceCode) & ", 0, 0, " & vbCrLf & " 0, 0, 0," & vbCrLf & " '" & mReverseChargeApp & "', 'N','N','')"




        PubDBCn.Execute(SqlStr)

        If UpdateDetail(CurMKey, mRowNo, mBookCode, mVType, mVnoStr, (TxtVDate.Text), mChqNo, pNarration, mAccountName, mAccountCode, mAmount, pProcessKey, PubDBCn) = False Then GoTo ErrPart


        xSqlStr = "SELECT DIV_CODE FROM INV_DIVISION_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSDiv, ADODB.LockTypeEnum.adLockReadOnly)

        If RSDiv.EOF = False Then
            mSubRowNo = -1
            Do While RSDiv.EOF = False
                mVAmount = 0
                mDivCode = IIf(IsDBNull(RSDiv.Fields("DIV_CODE").Value), -1, RSDiv.Fields("DIV_CODE").Value)
                mSuppCustName = mAccountName
                mPRowNo = 1

                If GetAccountBalancingMethod(mSuppCustName, False) = "D" Then
                    If GetBillDetailAmount(mPRowNo, mSuppCustName, mDivCode, mDC, mSuppCustAmount, pProcessKey) = True Then
                        mVAmount = mVAmount + (mSuppCustAmount * IIf(UCase(mDC) = "D", 1, -1))
                        mDC = IIf(mDC = "D", "CR", "DR") ''Book Code Update						
                    Else
                        mDC = "DR"
                    End If
                Else
                    mChkDivCode = 1

                    If mDivCode = mChkDivCode Then
                        mDC = "DR"

                        sprdMain.Col = ColAmount
                        mVAmount = mVAmount + (mAmount * IIf(UCase(mDC) = "DR", 1, -1))

                    End If
                End If


                mDrCr = IIf(mVAmount > 0, "C", "D")
                mVAmount = Val(CStr(System.Math.Abs(mVAmount)))

                If mVAmount <> 0 Then
                    If UpdateTRN(PubDBCn, CurMKey, mRowNo, mSubRowNo, mBookCode, mVType, mBookType, mBookSubType, mBookCode, mVnoStr, (TxtVDate.Text), mVnoStr, (TxtVDate.Text), mVAmount, mDrCr, "P", "", "", CStr(-1), CStr(-1), CStr(-1), CStr(-1), "", "", "P", "", "", pNarration, "", mExpDate, ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivCode, "N") = False Then GoTo ErrPart

                    mSubRowNo = mSubRowNo - 1
                End If
                RSDiv.MoveNext()

            Loop
        End If

        Update1 = True

        Exit Function
ErrPart:
        '    Resume						
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Update1 = False

    End Function
    Private Function GetBillDetailAmount(ByRef mPRowNo As Integer, ByRef mSuppCustName As String, ByRef mDivCode As Double, ByRef mDC As String, ByRef mSuppCustAmount As Double, ByRef pProcessKey As Double) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim xAcctCode As String

        If MainClass.ValidateWithMasterTable(mSuppCustName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            xAcctCode = CStr(-1)
        End If

        mDC = "D"
        mSuppCustAmount = 0
        GetBillDetailAmount = False
        SqlStr = "SELECT SUM(AMOUNT * DECODE(DC,'D',1,-1)) AS AMOUNT FROM FIN_TEMPBILL_TRN  Where UserID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & "" & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'" & vbCrLf & " AND TRNDTLSUBROWNO=" & Val(CStr(mPRowNo)) & "" & vbCrLf & " AND BOOKTYPE='" & UCase(Trim(lblBookType.Text)) & "' AND DIV_CODE= " & mDivCode & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            mSuppCustAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
            mDC = IIf(mSuppCustAmount >= 0, "D", "C")
            mSuppCustAmount = System.Math.Abs(mSuppCustAmount)
        End If
        GetBillDetailAmount = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        '    Resume						
    End Function

    Private Function FillTempBillDetails(ByRef mAccountCode As String, ByRef pVDate As String, ByRef mAmount As Double, ByRef pProcessKey As Double) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillAmount As Double
        Dim mBalanceAmount As Double
        Dim cntRow As Integer
        Dim xAmount As Double
        Dim mBillDC As String
        Dim mDC As String

        Dim mOldAmount As Double
        Dim mOldDC As String
        Dim mOldBillNo As String
        Dim mDueDate As String
        Dim mRemarks As String
        Dim mFormRecdCode As Integer
        Dim mFormDueCode As Integer
        Dim mIsRegdNo As String
        Dim mSTType As String
        Dim mTaxableAmount As Double
        Dim mPONo As String
        Dim mDivCode As Double
        Dim mRefNo As String
        Dim xRefNo As String

        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mPayType As String
        Dim mCheckBillNo As String
        Dim mDrCr As String
        Dim mBalAmount As Double
        Dim mBalDC As String
        Dim mTRNType As String
        Dim mBalAmountNum As Double
        Dim mBillToLocation As String

        mBalanceAmount = mAmount

        '    SqlStr = " SELECT BILLNO, BILLDATE, " & vbCrLf _						
        ''            & " SUM(DECODE(TRN.DC,'D',1,-1) * TRN.AMOUNT)  AS AMOUNT " & vbCrLf _						
        ''            & " FROM FIN_POSTED_TRN TRN " & vbCrLf _						
        ''            & " WHERE TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _						
        ''            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _						
        ''            & " AND TRN.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'" & vbCrLf _						
        ''            & " AND TRN.Vdate<='" & VB6.Format(pVDate, "DD-MMM-YYYY") & "'" & vbCrLf _						
        ''            & " GROUP BY BILLDATE, BILLNO" & vbCrLf _						
        ''            & " ORDER BY BILLDATE, BILLNO"						

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""
        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

        SqlStr = " Select CASE WHEN BillNo='ON ACCOUNT' THEN 'O' WHEN BillNo='ADVANCE' THEN 'A' ELSE  'B' END AS TRNTYPE,BillNo,BillDate, " & vbCrLf _
            & " TO_CHAR(ABS(" & mBillAmtStr & ")) AS BillAMT, " & vbCrLf _
            & " CASE WHEN " & mBillAmtStr & " >=0 THEN 'DR' ELSE 'CR' END AS BILLDC , " & vbCrLf _
            & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf _
            & " CASE WHEN " & mBalAmtStr & " >=0 THEN 'DR' ELSE 'CR' END AS BALDC, " & vbCrLf _
            & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS AMOUNT, " & vbCrLf _
            & " CASE WHEN " & mBalAmtStr & " >=0 THEN 'CR' ELSE 'DR' END AS PAYDC, " & vbCrLf _
            & " 0 AS OldAmount,'D' AS OldDC ,'' AS OldBillNo,Min(EXPDATE) AS DUEDATE, LOCATION_ID  " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AccountCode = '" & MainClass.AllowSingleQuote(mAccountCode) & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND BILLDATE <=TO_DATE('" & VB6.Format(pVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY BillNo, BillDate, LOCATION_ID" & vbCrLf _
            & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf & " ORDER BY BillDate, BillNo "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then

            cntRow = 0
            Do While RsTemp.EOF = False
                mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                mBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")
                mBillAmount = Val(IIf(IsDBNull(RsTemp.Fields("BillAMT").Value), 0, RsTemp.Fields("BillAMT").Value))
                mBillDC = IIf(IsDBNull(RsTemp.Fields("BILLDC").Value), "DR", RsTemp.Fields("BILLDC").Value)

                mBalAmount = Val(IIf(IsDBNull(RsTemp.Fields("AMOUNT").Value), 0, RsTemp.Fields("AMOUNT").Value))
                mBalDC = IIf(IsDBNull(RsTemp.Fields("PAYDC").Value), "CR", RsTemp.Fields("PAYDC").Value)
                mBalAmountNum = mBalAmount * IIf(mBalDC = "DR", 1, -1)
                mBillToLocation = IIf(IsDBNull(RsTemp.Fields("LOCATION_ID").Value), "", RsTemp.Fields("LOCATION_ID").Value)

                mOldBillNo = ""
                mOldAmount = 0
                mOldDC = "D"

                cntRow = cntRow + 1
                mTRNType = IIf(IsDBNull(RsTemp.Fields("TRNTYPE").Value), "B", RsTemp.Fields("TRNTYPE").Value)
                mRemarks = mRemarks & IIf(mRemarks = "", "", ", ") & mBillNo
                mSTType = "0"
                mFormRecdCode = -1
                mFormDueCode = -1
                mIsRegdNo = "N"
                mTaxableAmount = 0
                mPONo = ""
                mDivCode = 1
                If mTRNType = "O" Or mTRNType = "A" Then
                    mDivCode = 1
                Else
                    mDivCode = GetDivisionCode(mBillNo, mBillDate, mAccountCode)
                End If

                mRefNo = ""
                mDueDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DUEDATE").Value), "", RsTemp.Fields("DUEDATE").Value), "DD/MM/YYYY")

                If mBalanceAmount <= mBalAmountNum Then
                    xAmount = mBalanceAmount
                    mBalanceAmount = 0
                    mDC = "DR"
                    xAmount = System.Math.Abs(xAmount)
                Else
                    xAmount = mBalAmountNum
                    mBalanceAmount = mBalanceAmount - mBalAmountNum
                    mDC = IIf(xAmount > 0, "DR", "CR")
                    xAmount = System.Math.Abs(xAmount)
                End If

                SqlStr = "INSERT INTO FIN_TEMPBILL_TRN  ( " & vbCrLf _
                    & " USERID, TRNDTLSUBROWNO ,SUBROWNO , BOOKTYPE, " & vbCrLf _
                    & " ACCOUNTCODE, TRNTYPE, BILLNO, BILLDATE, " & vbCrLf _
                    & " BILLAMOUNT, BILLDC, Amount, DC, " & vbCrLf _
                    & " OldBillNo,OldAmount,OldDC," & vbCrLf & " DUEDATE,REMARKS,BillCheck, " & vbCrLf _
                    & " STTYPE, STFORMCODE, STFORMNAME, " & vbCrLf _
                    & " STFORMNO, STFORMDATE, STDUEFORMCODE, " & vbCrLf _
                    & " STDUEFORMNAME, STDUEFORMNO, STDUEFORMDATE, " & vbCrLf _
                    & " ISREGDNO, TAXABLE_AMOUNT,PONO,DIV_CODE, REF_NO, TEMPMKEY,BILL_TO_LOC_ID, BILL_COMPANY_CODE " & vbCrLf _
                    & " ) VALUES ( "

                SqlStr = SqlStr & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " 1," & vbCrLf & " " & cntRow & ", '" & UCase(lblBookType.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mAccountCode) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(UCase(mTRNType)) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mBillNo) & "'," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " " & mBillAmount & ", '" & VB.Left(mBillDC, 1) & "',  " & " " & xAmount & ", '" & VB.Left(mDC, 1) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mOldBillNo) & "', " & vbCrLf & " " & mOldAmount & ", '" & VB.Left(mOldDC, 1) & "'," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','Y', " & vbCrLf _
                    & " '" & mSTType & "', " & mFormRecdCode & ", ''," & vbCrLf & " '', '', " & mFormDueCode & "," & vbCrLf _
                    & " '', '', ''," & vbCrLf _
                    & " '" & mIsRegdNo & "'," & mTaxableAmount & ", '" & mPONo & "'," & mDivCode & ",'" & MainClass.AllowSingleQuote(mRefNo) & "'," & Val(CStr(pProcessKey)) & ",'" & mBillToLocation & "'," & RsCompany.Fields("COMPANY_CODE").Value & " ) "

                PubDBCn.Execute(SqlStr)

                If mBalanceAmount = 0 Then
                    Exit Do
                End If

                RsTemp.MoveNext()
            Loop
        End If

        If mBalanceAmount <> 0 Then
            xAmount = System.Math.Abs(mBalanceAmount)
            mDC = IIf(mBalanceAmount > 0, "DR", "CR")
            mTRNType = "O"
            mBillNo = "ON ACCOUNT"
            mBillDate = pVDate
            mBillDC = "CR"
            mRemarks = mRemarks & IIf(mRemarks = "", "", ", ") & mBillNo
            mDivCode = 1
            mBillToLocation = GetDefaultLocation(mAccountCode)
            SqlStr = "INSERT INTO FIN_TEMPBILL_TRN  ( " & vbCrLf _
                & " USERID, TRNDTLSUBROWNO ,SUBROWNO , BOOKTYPE, " & vbCrLf _
                & " ACCOUNTCODE, TRNTYPE, BILLNO, BILLDATE, " & vbCrLf _
                & " BILLAMOUNT, BILLDC, Amount, DC, " & vbCrLf _
                & " OldBillNo,OldAmount,OldDC," & vbCrLf _
                & " DUEDATE,REMARKS,BillCheck, " & vbCrLf _
                & " STTYPE, STFORMCODE, STFORMNAME, " & vbCrLf _
                & " STFORMNO, STFORMDATE, STDUEFORMCODE, " & vbCrLf _
                & " STDUEFORMNAME, STDUEFORMNO, STDUEFORMDATE, " & vbCrLf _
                & " ISREGDNO, TAXABLE_AMOUNT,PONO,DIV_CODE, REF_NO, TEMPMKEY , BILL_TO_LOC_ID, BILL_COMPANY_CODE" & vbCrLf _
                & " ) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " 1," & vbCrLf & " " & cntRow & ", '" & UCase(lblBookType.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mAccountCode) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(UCase(mTRNType)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mBillNo) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " " & mBillAmount & ", '" & VB.Left(mBillDC, 1) & "',  " & " " & xAmount & ", '" & VB.Left(mDC, 1) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mOldBillNo) & "', " & vbCrLf & " " & mOldAmount & ", '" & VB.Left(mOldDC, 1) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(mDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','Y', " & vbCrLf & " '" & mSTType & "', " & mFormRecdCode & ", ''," & vbCrLf _
                & " '', '', " & mFormDueCode & "," & vbCrLf _
                & " '', '', ''," & vbCrLf _
                & " '" & mIsRegdNo & "'," & mTaxableAmount & ", '" & mPONo & "'," & mDivCode & ",'" & MainClass.AllowSingleQuote(mRefNo) & "'," & Val(CStr(pProcessKey)) & ",'" & mBillToLocation & "'," & RsCompany.Fields("COMPANY_CODE").Value & ") "

            PubDBCn.Execute(SqlStr)
        End If

        mRemarks = IIf(mRemarks = "", "", " agt Bill No(s) ") & mRemarks
        mRemarks = VB.Left(mRemarks, 254)

        SqlStr = " UPDATE FIN_TEMPBILL_TRN SET REMARKS='" & MainClass.AllowSingleQuote(mRemarks) & "'" & vbCrLf & " WHERE USERID = '" & MainClass.AllowSingleQuote(PubUserID) & "' AND TEMPMKEY=" & Val(CStr(pProcessKey)) & ""

        PubDBCn.Execute(SqlStr)

        '    If MainClass.ValidateWithMasterTable(mSuppCustName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then						
        '        xAcctCode = MasterNo						
        '    Else						
        '        xAcctCode = -1						
        '    End If						
        '						
        '    mDC = "D"						
        '    mSuppCustAmount = 0						
        '    GetBillDetailAmount = False						
        '    SqlStr = "SELECT SUM(AMOUNT * DECODE(DC,'D',1,-1)) AS AMOUNT FROM FIN_TEMPBILL_TRN  Where UserID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & "" & vbCrLf _						
        ''            & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'" & vbCrLf _						
        ''            & " AND TRNDTLSUBROWNO=" & Val(mPRowNo) & "" & vbCrLf _						
        ''            & " AND BOOKTYPE='" & UCase(Trim(lblBookType.Caption)) & "' AND DIV_CODE= " & mDivCode & ""						
        '						
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp						
        '						
        '    If RsTemp.EOF = False Then						
        '        mSuppCustAmount = IIf(IsNull(RsTemp!Amount), 0, RsTemp!Amount)						
        '        mDC = IIf(mSuppCustAmount >= 0, "D", "C")						
        '        mSuppCustAmount = Abs(mSuppCustAmount)						
        '    End If						
        FillTempBillDetails = True
        PubDBCn.CommitTrans()
        Exit Function
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
        '    Resume						
    End Function

    Private Function GetDivisionCode(ByRef pBillNo As String, ByRef pBillDate As String, ByRef pAccountCode As String) As Double
        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = " Select DISTINCT DIV_CODE  " & vbCrLf & " FROM FIN_POSTED_TRN  " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AccountCode = '" & pAccountCode & "'" & vbCrLf & " AND BillNo='" & pBillNo & "'" ''& vbCrLf |            & " AND BILLDATE ='" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "'"						

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            GetDivisionCode = 1
        Else
            GetDivisionCode = RsTemp.Fields("DIV_CODE").Value
        End If
        Exit Function
ErrPart:
        GetDivisionCode = 1
    End Function
    Private Function GetSupplierBankName(ByRef mAccountCode As String) As String
        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mBankName As String
        Dim mBranch As String

        SqlStr = " Select CUST_BANK_BANK, BANK_BRANCH_NAME  " & vbCrLf & " FROM FIN_SUPP_CUST_MST  " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE = '" & mAccountCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mBankName = IIf(IsDBNull(RsTemp.Fields("CUST_BANK_BANK").Value), "", RsTemp.Fields("CUST_BANK_BANK").Value)
            mBranch = IIf(IsDBNull(RsTemp.Fields("BANK_BRANCH_NAME").Value), "", RsTemp.Fields("BANK_BRANCH_NAME").Value)
            GetSupplierBankName = IIf(mBankName = "", "", mBankName & IIf(mBranch = "", "", ", ")) & mBranch
        Else
            GetSupplierBankName = ""
        End If
        Exit Function
ErrPart:
        GetSupplierBankName = ""
    End Function

    Private Function UpdateDetail(ByRef mMkey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef mVType As String, ByRef mVNo As String, ByRef mVDate As String, ByRef mChqNo As String, ByRef pNarration As String, ByRef mAccountName As String, ByRef mAccountCode As String, ByRef mAmount As Double, ByRef pProcessKey As Double, ByRef pDBCn As ADODB.Connection) As Boolean

        On Error GoTo ErrDetail

        Dim I As Integer
        Dim SqlStr As String

        Dim mChequeNo As String
        Dim mChqDate As String

        Dim mCCCode As String
        Dim mExpCode As String
        Dim mDeptCode As String
        Dim mDivisionCode As Double
        Dim mEmpCode As String
        Dim mIBRNo As String
        Dim mDC As String
        Dim mRemarks As String
        Dim mPRRowNo As Integer
        Dim mSubRowNo As Integer
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mClearDate As String
        Dim mParticulars As String
        Dim VMkey As String
        Dim mIsFixedAssets As String
        Dim mSameVNo As Boolean

        Dim pSqlStr As String
        Dim RSDiv As ADODB.Recordset
        Dim pDivCode As Double
        Dim mSAC As String
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mNetAmount As Double

        Dim mSaleBillPrefix As String
        Dim mSaleBillSeq As String
        Dim mSaleBillNo As String
        Dim mSaleBillDate As String


        mBookType = VB.Left(lblBookType.Text, 1)
        mBookSubType = VB.Right(lblBookType.Text, 1)

        SqlStr = "Delete From FIN_BILLDETAILS_TRN Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)

        SqlStr = "Delete From FIN_SERVTAXDETAILS_TRN Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)

        SqlStr = "Delete From PAY_LOAN_MST Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)

        SqlStr = "Delete From FIN_VOUCHER_DET Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE " & vbCrLf & " MKEY ='" & mMkey & "' " & vbCrLf & " AND BookType='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND BooksubType='" & VB.Right(lblBookType.Text, 1) & "'"
        pDBCn.Execute(SqlStr)


        SqlStr = "UPDATE FIN_CHEQUE_MST SET " & vbCrLf & " CHEQUE_STATUS='O'," & vbCrLf & " VMKEY=''," & vbCrLf _
            & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
            & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND VMKEY='" & MainClass.AllowSingleQuote(Trim(mMkey)) & "'"
        PubDBCn.Execute(SqlStr)



        mPRRowNo = 1
        mSubRowNo = mPRRowNo
        mDC = "D"
        mIsFixedAssets = "N"
        mParticulars = pNarration
        mChequeNo = mChqNo
        mChqDate = mVDate
        mCCCode = CStr(-1)
        mExpCode = CStr(-1)
        mDeptCode = CStr(-1)
        mDivisionCode = 1
        mEmpCode = CStr(-1)
        mIBRNo = ""
        mSAC = ""
        mCGSTPer = 0
        mCGSTAmount = 0
        mSGSTPer = 0
        mSGSTAmount = 0
        mIGSTPer = 0
        mIGSTAmount = 0
        mSaleBillPrefix = ""
        mSaleBillSeq = ""
        mSaleBillNo = ""
        mSaleBillDate = ""
        mClearDate = ""


        SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate, " & vbCrLf & " PARTICULARS,DIV_CODE, " & vbCrLf & " SAC, CGST_PER, CGST_AMOUNT, " & vbCrLf & " SGST_PER, SGST_AMOUNT, IGST_PER, IGST_AMOUNT," & vbCrLf & " SALEBILLNOPREFIX, SALEBILLNOSEQ, SALEBILL_NO, SALEBILLDATE" & vbCrLf & " )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & ",  " & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & mSubRowNo & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & "," & vbCrLf & " '" & mSAC & "', " & mCGSTPer & ", " & mCGSTAmount & ", " & vbCrLf & " " & mSGSTPer & ", " & mSGSTAmount & "," & vbCrLf & " " & mIGSTPer & ", " & mIGSTAmount & ", " & vbCrLf & " '" & mSaleBillPrefix & "', '" & mSaleBillSeq & "', '" & mSaleBillNo & "', TO_DATE('" & VB6.Format(mSaleBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " )"

        PubDBCn.Execute(SqlStr)


        mNetAmount = mAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount

        If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mNetAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, pNarration, "N", VB.Left(lblBookType.Text, 1), VB.Right(lblBookType.Text, 1), VB6.Format(mVDate, "DD/MM/YYYY"), ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivisionCode, pProcessKey, "N", "N", mSAC, mCGSTPer, mCGSTAmount, mSGSTPer, mSGSTAmount, mIGSTPer, mIGSTAmount) = False Then GoTo ErrDetail

        '    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "HEADTYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then						
        '        If MasterNo = "L" Then						
        '            If UpdateLoanDetail(pDBCn, mMKey, mEmpCode, IIf(chkCancelled.Value = vbChecked, "Y", "N")) = False Then GoTo ErrDetail						
        '        ElseIf MasterNo = "S" Then						
        '            If UpdateServiceTaxDetail(pDBCn, mMKey, IIf(chkCancelled.Value = vbChecked, "Y", "N")) = False Then GoTo ErrDetail						
        '        End If						
        '    End If						

        '    If (lblBookType.text = ConBankPayment Or lblBookType.text = ConPDCPayment) And mChequeNo <> "" Then						
        '        VMkey = mMKey						
        ''       If chkCancelled.Value = vbUnchecked Then						
        ''            If UpdateChequeDetail(mChequeNo, VMkey, "C", mSameVNo) = False Then GoTo ErrDetail						
        ''       Else						
        ''           If UpdateChequeDetail(mChequeNo, VMkey, "O") = False Then GoTo ErrDetail						
        ''       End If						
        '    End If						

        mSameVNo = True

        UpdateDetail = True
        Exit Function
ErrDetail:
        'Resume						
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDetail = False
        'Resume						
    End Function


    Private Function GenVno() As String
        On Error GoTo ERR1
        Dim mVNo1 As String
        Dim SqlStr2 As String
        Dim SqlStr As String
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mVType As String


        mBookType = VB.Left(lblBookType.Text, 1)
        mBookSubType = VB.Right(lblBookType.Text, 1)
        mVType = Trim(txtVType.Text)


        SqlStr = "SELECT MAX(VNOSeq) From FIN_VOUCHER_HDR WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BookType='" & mBookType & "'" & vbCrLf _
            & " AND BookSubType='" & mBookSubType & "'" & vbCrLf _
            & " AND VTYPE='" & MainClass.AllowSingleQuote(mVType) & "'"

        If RsCompany.Fields("CBJVoucherSeq").Value = "D" Then
            SqlStr = SqlStr & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "M" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(VDATE,'MMMYYYY')=TO_CHAR('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','MMMYYYY')"
        ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "Y" Then

        End If

        GenVno = VB6.Format(MainClass.AutoGenVNo(SqlStr, PubDBCn), "00000")

        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume						
    End Function
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ErrPart


        With sprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColAccountCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn)
            .set_ColWidth(ColAccountCode, 10)

            .Col = ColAccountName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .set_ColWidth(ColAccountName, 25)
            .ColsFrozen = ColAccountName

            .Col = ColChqNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsTRNDetail.Fields("CHEQUENO").DefinedSize ''						
            .set_ColWidth(ColChqNo, 10)

            .Col = ColParticulars
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsTRNDetail.Fields("PARTICULARS").DefinedSize ''						
            .set_ColWidth(ColParticulars, 21)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")

            .Col = ColBalAmount_PayTerms
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")


            .Col = ColBalanceAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")

            .Col = ColVNo
            .TypeEditLen = RsTRNMain.Fields("VNO").DefinedSize ''						
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColVNo, 8)

            .Col = ColSuppBankName
            .TypeEditLen = MainClass.SetMaxLength("CUST_BANK_BANK", "FIN_SUPP_CUST_MST", PubDBCn) + MainClass.SetMaxLength("BANK_BRANCH_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColVNo, 8)

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColVDate, 8)

            .Col = ColMKEY
            .TypeEditLen = RsTRNMain.Fields("MKEY").DefinedSize ''						
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            .Col = ColProcessKey
            .TypeEditLen = 50 ''						
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColProcessKey, 8)
            .ColHidden = True

            .Col = ColBillDetails
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False						
            .TypeButtonText = "Bill Details"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColBillDetails, 8)

        End With

        MainClass.SetSpreadColor(sprdMain, Arow)
        '    MainClass.ProtectCell sprdMain, 1, sprdMain.MaxRows, ColAccountName, ColAccountName						
        MainClass.ProtectCell(sprdMain, 1, sprdMain.MaxRows, ColBalAmount_PayTerms, ColMKEY)
        Exit Sub
ErrPart:
        'Resume						
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmAtrnBulk_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode						
    End Sub

    Private Sub frmAtrnBulk_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")

        If ADDMode = True Or MODIFYMode = True Then
            If KeyAscii = System.Windows.Forms.Keys.Escape Then cmdClose_Click(cmdClose, New System.EventArgs())
        End If
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub frmAtrnBulk_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '''Set PvtDBCn = New ADODB.Connection						
        '''PvtDBCn.Open StrConn						
        '    Call SetMainFormCordinate(Me)						

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7310)
        'Me.Width = VB6.TwipsToPixelsX(11340)


        CurrFormHeight = 7310
        CurrFormWidth = 11340

        ADDMode = False
        MODIFYMode = False
        FormLoaded = False

        mAuthorised = "N"
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        lblBookBalAmt.Text = "0.00"
        lblBookBalDC.Text = ""
        TxtVDate.Text = IIf(TxtVDate.Text = "", RunDate, TxtVDate.Text)
        ConPaymentDetail = False
        cmdSave.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Call frmAtrnBulk_Activated(eventSender, eventArgs)
        MainClass.SetControlsColor(Me)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Sub

    Public Sub frmAtrnBulk_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ErrPart
        Dim SqlStr As String

        sprdMain.Refresh()
        If FormLoaded = True Then Exit Sub
        FormLoaded = True

        SqlStr = "Select * From FIN_VOUCHER_HDR Where 1=2 "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From FIN_VOUCHER_DET Where 1=2 "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNDetail, ADODB.LockTypeEnum.adLockReadOnly)


        FormatSprdMain(-1)



        SetTextLengths()
        '    CalcAccountBal						
        '    If cmdAdd.Enabled = True Then cmdAdd_Click						
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        Dim SqlStr As String



        Exit Sub
ERR1:
        '    Resume						
        ErrorMsg(Err.Description)
    End Sub

    Private Sub frmAtrnBulk_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        FraTrans.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth - 120, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(sprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmAtrnBulk_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then						
        '        'PvtDBCn.Close						
        '        'Set PvtDBCn = Nothing						
        '    End If						
        '.Cancel = 0
        'If ADDMode = True Or MODIFYMode = True Then
        '    If MsgQuestion("Are you sure to Exit") = CStr(MsgBoxResult.No) Then
        '        Cancel = 1
        '        Exit Sub
        '    End If
        'End If
        'RsTRNMain.Close()
        'RsTRNMain = Nothing

        'RsTRNDetail.Close()
        'RsTRNDetail = Nothing


        '    Unload frmPaymentDetail						
        '    Unload frmViewOuts	
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles sprdMain.ButtonClicked
        On Error GoTo ERR1

        Call PayDetailForm((sprdMain.ActiveRow))

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdMain.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles sprdMain.ClickEvent
        On Error GoTo ERR1
        Dim Response As String

        Select Case eventArgs.col
            Case 0
                If eventArgs.row > 0 And sprdMain.Enabled = True Then
                    If sprdMain.MaxRows > 1 Then
                        Response = MsgQuestion("Are you sure to Delete this Row ? ")
                        If Response = CStr(MsgBoxResult.Yes) Then
                            sprdMain.Row = eventArgs.row
                            sprdMain.Action = SS_ACTION_DELETE_ROW
                            sprdMain.MaxRows = sprdMain.MaxRows - 1
                        End If
                    End If
                    '               MainClass.DeleteSprdRow SprdMain, Row, ColAccountName						
                End If
        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub PayDetailForm(ByRef mActiveRow As Integer)
        Dim pVDate As String
        Dim mAccountName As String
        Dim mDC As String
        Dim mCostCode As String
        Dim mDivisionCode As Integer
        Dim mAmount As Double
        Dim pProcessKey As Double
        Dim mNarration As String
        Dim mCostCName As String
        Dim CurMKey As String

        ConPaymentDetail = False

        sprdMain.Row = mActiveRow
        sprdMain.Col = ColAccountName
        mAccountName = Trim(sprdMain.Text)

        sprdMain.Col = ColAmount
        mAmount = Val(sprdMain.Text)

        sprdMain.Col = ColProcessKey
        pProcessKey = Val(sprdMain.Text)

        sprdMain.Col = ColMKEY
        CurMKey = Trim(sprdMain.Text)

        If ShowDetailForm(VB6.Format(TxtVDate.Text, "DD/MM/YYYY"), mAccountName, "DR", "-1", 1, mAmount, pProcessKey, "", "", CurMKey) = "S" Then 'When Account is bill by bill						
            '            If sprdMain.MaxRows = mActiveRow Then						
            '                MainClass.AddBlankSprdRow sprdMain, ColAccountName, ConRowHeight						
            ''                FormatSprdMain -1						
            '                FormatSprdMainGST -1						
            '            End If						
        Else
            If ConPaymentDetail = True Then
                '                sprdMain.Row = mActiveRow						
                '                sprdMain.Col = ColAmount						
                '                sprdMain.Text = Val(frmPaymentDetail.LblNetAmt)						
                '                If sprdMain.MaxRows = mActiveRow Then						
                '                    MainClass.AddBlankSprdRow sprdMain, ColAccountName, ConRowHeight						
                ''                    FormatSprdMain -1						
                '                    FormatSprdMainGST -1						
                '                End If						
            End If
            frmPaymentDetail.Close()
        End If
    End Sub

    Private Function ShowDetailForm(ByRef pVDate As String, ByRef mAccountName As String, ByRef mDC As String, ByRef mCostCode As String, ByRef mDivisionCode As Integer, ByRef mAmount As Double, ByRef pProcessKey As Double, ByRef mNarration As String, ByRef mCostCName As String, ByRef CurMKey As String) As String
        'Dim mAccountName As String						
        'Dim mAmount As Double						
        'Dim mDC As String						
        'Dim mNarration As String						
        'Dim mEmpCode As String						
        'Dim mCostCName As String						
        'Dim mPRRowNo As Long						
        'Dim mCostCode As String						
        'Dim mAccountCode As String						
        'Dim mHeadType As String						
        'Dim mPartyName As String						
        'Dim mCurrRow As Long						
        'Dim cntRow As Long						
        'Dim mSectionCode As Double						
        'Dim mBillAmount As Double						
        'Dim mDivisionCode As Double						

        ShowDetailForm = "S"

        If GetAccountBalancingMethod(mAccountName, False) = "D" Then
            ShowDetailForm = "D"
            With frmPaymentDetail
                .lblAccountName.Text = mAccountName
                .lblAmount.Text = CStr(mAmount)
                .lblADDMode.Text = CStr(ADDMode)
                .lblTempProcessKey.Text = CStr(pProcessKey)
                .lblModifyMode.Text = CStr(MODIFYMode)
                .lblDC.Text = mDC
                .lblVDate.Text = pVDate
                .lblNarration.Text = mNarration
                .lblBookType.Text = lblBookType.Text
                .lblCostCName.Text = mCostCName
                .lblCostCCode.Text = mCostCode
                .lblTrnRowNo.Text = CStr(1)
                .lblDivisionCode.Text = CStr(mDivisionCode)
                .lblMkey.Text = CurMKey
                .cmdPopulate.Enabled = True
                '            If ADDMode = True Then						
                .cmdAppendDetail.Enabled = False
                '            Else						
                '                .cmdAppendDetail.Enabled = True						
                '            End If						
                .ShowDialog()
                cmdSave.Enabled = True ''If ADDMode = True Or MODIFYMode = True Then cmdSave.Enabled = True						
            End With
        End If


    End Function

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles sprdMain.DblClick
        On Error GoTo ERR1
        '    Select Case Col						
        '        Case ColAccountName						
        '            NameSearch Col, sprdMain.ActiveRow						
        '    End Select						
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub NameSearch(ByRef Col As Integer, ByRef Row As Integer)
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mString As String

        sprdMain.Row = Row
        sprdMain.Col = ColAccountName
        mString = sprdMain.Text
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"

        If Col = ColAccountName Then
            If MainClass.SearchGridMaster(mString, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , SqlStr) = True Then
                If AcName <> "" Then
                    sprdMain.Row = sprdMain.ActiveRow
                    sprdMain.Col = ColAccountName
                    sprdMain.Text = AcName
                    sprdMain.Col = ColAccountCode
                    sprdMain.Text = AcName1
                End If
            End If
        End If

        SprdMain_LeaveCell(sprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(Col, sprdMain.ActiveRow, Col, sprdMain.ActiveRow, False))

        sprdMain.Refresh()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles sprdMain.KeyUpEvent
        On Error GoTo ERR1

        If sprdMain.ActiveRow <= 0 Then Exit Sub

        Select Case sprdMain.ActiveCol
            Case ColAccountName

                If eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then NameSearch((sprdMain.ActiveCol), (sprdMain.ActiveRow))

                '        Case ColAmount						
                '            If KeyCode = vbKeyReturn Or KeyCode = vbKeyTab Then						
                '                If sprdMain.MaxRows = sprdMain.ActiveRow Then						
                '                    MainClass.AddBlankSprdRow sprdMain, ColAccountName, ConRowHeight						
                ''                    FormatSprdMain -1						
                '                    FormatSprdMainGST -1						
                '                End If						
                '            End If						
        End Select
        eventArgs.keyCode = 9999
        Exit Sub
ERR1:
        ErrorMsg(Err.Description)
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdMain.LeaveCell
        On Error GoTo ERR1
        Dim mAmount As Double
        Dim pAccountName As String
        Dim mAccountCode As String


        If eventArgs.newRow = -1 Then Exit Sub
        Select Case eventArgs.col

            Case ColAccountName

                sprdMain.Row = eventArgs.row
                sprdMain.Col = ColAccountName
                pAccountName = Trim(sprdMain.Text)
                If pAccountName = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(pAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode = MasterNo
                    sprdMain.Col = ColAccountCode
                    sprdMain.Text = mAccountCode
                Else
                    MsgInformation("Invaild Account Name")
                    eventArgs.cancel = True
                    Exit Sub
                End If



            Case ColAmount
                '            Call PayDetailForm(sprdMain.ActiveRow)						

        End Select

        '    FormatSprdMain -1						


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume						
    End Sub
    Private Sub txtPartyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.DoubleClick
        Call SearchName()
    End Sub

    Private Sub SearchName()
        On Error GoTo SearchErr
        Dim SqlStr As String

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        Select Case lblBookType.Text
            Case ConCashPayment, ConCashReceipt
                SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '1'"
            Case ConBankPayment, ConBankReceipt, ConPDCPayment, ConPDCReceipt
                SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '2'"
            Case Else
                SqlStr = SqlStr & " AND 1=2"
        End Select

        SqlStr = SqlStr & " AND STATUS='O'"
        MainClass.SearchGridMaster(txtPartyName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", "", "", SqlStr)
        If AcName <> "" Then
            txtPartyName.Text = AcName
            txtPartyName_Validating(txtPartyName, New System.ComponentModel.CancelEventArgs(True))
            If ADDMode = True Then txtVType.Focus() Else sprdMain.Focus()
        End If
        Exit Sub

SearchErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchName()
    End Sub

    Private Sub txtPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsCheckName As ADODB.Recordset
        Dim SqlStr As String
        Dim mOPBal As Double

        If Trim(txtPartyName.Text) = "" Then GoTo EventExitSub

        SqlStr = " Select SUPP_CUST_NAME,SUPP_CUST_CODE,STATUS FROM FIN_SUPP_CUST_MST Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(Trim(txtPartyName.Text)) & "'"

        SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_TYPE='2'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheckName, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCheckName.EOF = True Then
            MsgBox("Invaild Account Name. ", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConBankPayment Then
            If PubUserID = "G0416" Then
            Else
                If CheckPendingPDC() = True Then
                    Cancel = True
                    GoTo EventExitSub
                End If
            End If
        End If

        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "VNAME", "VTYPE", "FIN_VOUCHERTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'") = True Then
            txtVType.Text = MasterNo
        End If

        If IsDate(TxtVDate.Text) Then
            mOPBal = GetOpeningBal((RsCheckName.Fields("SUPP_CUST_CODE").Value), (TxtVDate.Text))
        End If
        lblBookBalAmt.Text = VB6.Format(System.Math.Abs(mOPBal), "0.00")
        lblBookBalDC.Text = IIf(mOPBal >= 0, "Dr", "Cr")
        RsCheckName.Close()
        RsCheckName = Nothing

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function CheckPendingPDC() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsPDC As ADODB.Recordset
        Dim xBookType As String
        Dim xBookSubType As String
        Dim xAccountCode As String
        Dim mChq As String

        If lblBookType.Text = ConBankReceipt Then
            xBookType = VB.Left(ConPDCReceipt, 1)
            xBookSubType = VB.Right(ConPDCReceipt, 1)
        ElseIf lblBookType.Text = ConBankPayment Then
            xBookType = VB.Left(ConPDCPayment, 1)
            xBookSubType = VB.Right(ConPDCPayment, 1)
        End If

        If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            xAccountCode = IIf(IsDBNull(MasterNo), -1, MasterNo)
        Else
            xAccountCode = CStr(-1)
        End If

        SqlStr = "SELECT 'VNO : ' || FIN_VOUCHER_HDR.VNO || ':' || 'CHQ NO : ' || CHEQUENO AS VNO FROM FIN_VOUCHER_HDR,FIN_VOUCHER_DET" & vbCrLf & " WHERE FIN_VOUCHER_HDR.MKEY=FIN_VOUCHER_DET.MKEY " & vbCrLf & " AND COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & " AND CHQDATE<=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND BOOKTYPE='" & xBookType & "'" & vbCrLf & " AND BOOKSUBTYPE='" & xBookSubType & "' AND BOOKCODE='" & xAccountCode & "' AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPDC, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPDC.EOF = False Then
            Do While Not RsPDC.EOF
                mChq = IIf(mChq = "", "", mChq & vbNewLine) & IIf(IsDBNull(RsPDC.Fields("VNO").Value), "", RsPDC.Fields("VNO").Value)
                RsPDC.MoveNext()
            Loop
            MsgBox("Following PDC are pending for Normalization " & vbNewLine & mChq, MsgBoxStyle.Information)
            CheckPendingPDC = True
        Else
            CheckPendingPDC = False
        End If
        RsPDC.Close()
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        RsPDC.Close()
    End Function

    Private Sub txtVDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtVDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtVDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(TxtVDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConPDCPayment Then GoTo EventExitSub

        If FYChk((TxtVDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mAcctCode As String
        'Dim mPRRowNo As Long						
        Dim mAmount As Double
        Dim mVType As String
        Dim ii As Integer
        Dim mAccountName As String
        Dim mLockBookCode As Integer
        'Dim mIsTDSAccount As Boolean						
        'Dim pTDSChallanNo As String						
        'Dim pVNo As String						
        'Dim mServiceClaimCode As String						
        'Dim mISServiceClaim As Boolean						
        'Dim pClaimNo As String						
        'Dim mServiceTaxHeadCount As Long						
        Dim mPartyName As String
        'Dim mChequeNo As String						
        Dim mIsAuthorisedUser As String
        Dim mPANNo As String
        'Dim mHeadType As String						
        'Dim mDRCRBal As Double						
        'Dim xDivName As String						
        'Dim mServiceGL As String						
        'Dim xUnlockVType As String						
        'Dim RsTemp As ADODB.Recordset						
        'Dim SqlStr As String						
        'Dim mRefNo As String						
        'Dim mSACCode As String						
        'Dim mCGSTPer As Double						
        'Dim mSGSTPer As Double						
        'Dim mIGSTPer As Double						
        ''Dim mAmount As Double						
        'Dim mCGSTAmount As Double						
        'Dim mSGSTAmount As Double						
        'Dim mIGSTAmount As Double						
        'Dim mCheckSAC As Long						
        'Dim mReversalVoucher As String						
        'Dim mReversalMkey As String						
        Dim mBankCode As String
        Dim mLenderBankCode As String
        Dim mLenderBankName As String
        Dim pProcessKey As Double

        FieldsVerification = False

        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            FieldsVerification = False
            Exit Function
        End If

        If ValidateAccountLocking(PubDBCn, TxtVDate.Text, (txtPartyName.Text)) = True Then
            FieldsVerification = False
            Exit Function
        End If

        '    xUnlockVType = ""						
        '						
        '    mLockBookCode = ConLockBankPayment						
        '    xUnlockVType = "P"						

        If ValidateBookLocking(PubDBCn, CInt(ConLockBankPayment), (TxtVDate.Text)) = True Then
            FieldsVerification = False
            Exit Function
        End If

        If FYChk(TxtVDate.Text) = False Then
            '        MsgInformation "Date is not in the Current Financial Year"						
            If TxtVDate.Enabled = True Then TxtVDate.Focus()
            Exit Function
        End If


        mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)


        If Trim(txtPartyName.Text) = "" Then
            MsgInformation("Bank Name missing")
            txtPartyName.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_Name", "STATUS", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            If MasterNo = "C" Then
                MsgInformation("Account is closed. So that you Cann't Save. ")
                FieldsVerification = False
                Exit Function
            End If
        End If

        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBankCode = MasterNo
        Else
            MsgInformation("Invalid Bank Name. So that you Cann't Save. ")
            FieldsVerification = False
            Exit Function
        End If

        mVType = ""
        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "VNAME", "VTYPE", "FIN_VOUCHERTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'") = True Then
            mVType = MasterNo
        Else
            MsgInformation("Invalid Bank Voucher Type. So that you Cann't Save. ")
            FieldsVerification = False
            Exit Function
        End If

        If Trim(mVType) = "" Then
            MsgInformation("Voucher Type is Blank")
            FieldsVerification = False
            Exit Function
        End If

        If InStr(1, mIsAuthorisedUser, "S") = 0 Then
            If CheckLastestVDate(CDate(TxtVDate.Text), mVType) = False Then ''If CheckBackDateEntry(TxtVDate.Text) = True Then						
                MsgBox("You Cann't Add/Modify back date Voucher", MsgBoxStyle.Information)
                FieldsVerification = False
                Exit Function
            End If
        End If

        With sprdMain
            For ii = 1 To .MaxRows
                .Row = ii
                .Col = ColProcessKey
                pProcessKey = Val(.Text)

                .Col = ColAccountCode
                If Trim(.Text) <> "" Then
                    mAcctCode = Trim(.Text)
                    If MainClass.ValidateWithMasterTable(mAcctCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountName = MasterNo

                        .Col = ColAccountName
                        .Text = Trim(mAccountName)

                        If MainClass.ValidateWithMasterTable(mAcctCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOP_BANK='Y'") = True Then
                            MsgBox("Bank Payment Cann't Be Made for Such Customer/Supplier, So cann't be saved", MsgBoxStyle.Information)
                            FieldsVerification = False
                            Exit Function
                        End If

                        mLenderBankCode = ""
                        If MainClass.ValidateWithMasterTable(mAcctCode, "SUPP_CUST_CODE", "LENDER_BANK_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mLenderBankCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                        End If

                        If Trim(mLenderBankCode) <> "" Then
                            If mLenderBankCode <> mBankCode Then
                                If MainClass.ValidateWithMasterTable(mLenderBankCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                    mLenderBankName = IIf(IsDBNull(MasterNo), "", MasterNo)
                                End If
                                If MsgQuestion("Lender Bank (" & mLenderBankName & ") is not match with payment bank. Are you want to continue...") = CStr(MsgBoxResult.No) Then
                                    FieldsVerification = False
                                    Exit Function
                                End If
                            End If
                        End If

                    Else
                        MsgInformation("Invaild Account Name.")
                        MainClass.SetFocusToCell(sprdMain, cntRow, ColAccountName)
                        Exit Function
                    End If

                    If ValidateAccountLocking(PubDBCn, TxtVDate.Text, mAccountName) = True Then
                        FieldsVerification = False
                        Exit Function
                    End If

                    If CheckValidPartyPanNo(UCase(mAccountName), pProcessKey) = False Then
                        MsgInformation("Invalid Party PANNo, so Cann't be Saved. Row No : " & ii)
                        FieldsVerification = False
                        Exit Function
                    End If
                End If
            Next
        End With


        '    If MainClass.ValidDataInGrid(sprdMain, ColAmount, "N", "Please check. Either Amount is Missing all the rows are marked for deletion") = False Then Exit Function						



        '    If MainClass.ValidDataInGrid(SprdMain, ColDivisionCode, "S", "Division Is Blank.") = False Then FieldsVerification = False: Exit Function						

        FieldsVerification = True
        Exit Function
ERR1:
        '    Resume						
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FieldsVerification = False
    End Function
    Private Function CheckValidPartyPanNo(ByRef pPartyName As String, ByRef pProcessKey As Double) As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mPANNo As String
        Dim xAccountCode As String
        Dim xSuppCustType As String

        If MainClass.ValidateWithMasterTable(pPartyName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAccountCode = MasterNo
        Else
            CheckValidPartyPanNo = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(xAccountCode, "SUPP_CUST_CODE", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            If Trim(MasterNo) = "N" Then
                CheckValidPartyPanNo = True
                Exit Function
            End If
        End If

        If MainClass.ValidateWithMasterTable(xAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_TYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSuppCustType = MasterNo
            If xSuppCustType = "C" Or xSuppCustType = "S" Then

            Else
                CheckValidPartyPanNo = True
                Exit Function
            End If
        Else
            CheckValidPartyPanNo = False
            Exit Function
        End If



        If MainClass.ValidateWithMasterTable(xAccountCode, "SUPP_CUST_CODE", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPANNo = MasterNo
            If Trim(mPANNo) <> "" Then
                If CheckPANValidation(mPANNo) = True Then
                    CheckValidPartyPanNo = True
                    Exit Function
                End If
            End If
        Else
            CheckValidPartyPanNo = False
            Exit Function
        End If


        mSqlStr = " Select A.BILLNO, A.BILLDATE " & vbCrLf & " FROM FIN_TEMPBILL_TRN A, FIN_POSTED_TRN B, TDS_TRN C  " & vbCrLf & " WHERE B.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND B.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND A.UserID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & "" & vbCrLf & " AND A.AccountCode='" & xAccountCode & "'" & vbCrLf & " AND A.BookType='" & lblBookType.Text & "'" & vbCrLf & " AND A.BILLNO=B.BILLNO" & vbCrLf & " AND A.BILLDATE=B.BILLDATE" & vbCrLf & " AND A.ACCOUNTCODE=B.ACCOUNTCODE" & vbCrLf & " AND B.COMPANY_CODE=C.COMPANY_CODE AND B.MKEY=C.MKEY" & vbCrLf & " AND B.BOOKTYPE=C.BOOKTYPE AND B.BOOKSUBTYPE=C.BOOKSUBTYPE"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = True Then
            CheckValidPartyPanNo = True
            Exit Function
        Else
            CheckValidPartyPanNo = False
            Exit Function
        End If
        Exit Function
ErrPart:
        CheckValidPartyPanNo = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function CheckLastestVDate(ByRef mVDate As Date, ByRef mVType As String) As Boolean
        On Error GoTo CheckLastestVDateErr
        Dim SqlStr As String
        Dim RsCheck As ADODB.Recordset '' ADODB.Recordset						
        Dim mBookSubType As String
        Dim mBookType As String

        '    If chkChqDeposit.Value = vbChecked Then						
        '        Call GetNewBook(mBookType, mBookSubType, mVType)						
        '    Else						
        mBookType = VB.Left(lblBookType.Text, 1)
        mBookSubType = VB.Right(lblBookType.Text, 1)
        '    End If						

        CheckLastestVDate = True
        SqlStr = "SELECT VDATE FROM FIN_VOUCHER_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " BookType='" & mBookType & "' AND " & vbCrLf & " BookSubType='" & mBookSubType & "' AND " & vbCrLf & " VTYPE='" & MainClass.AllowSingleQuote(Trim(mVType)) & "' AND " & vbCrLf & " VDATE>TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheck, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCheck.EOF = False Then
            CheckLastestVDate = False
        End If
        Exit Function

CheckLastestVDateErr:
        CheckLastestVDate = False
    End Function
End Class
