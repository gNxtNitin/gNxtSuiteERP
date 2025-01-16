Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmReversalVoucher
    Inherits System.Windows.Forms.Form

    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ColPRRowNo As Short = 1
    Private Const ColDC As Short = 2
    Private Const ColAccountName As Short = 3
    Private Const ColParticulars As Short = 4
    Private Const ColChequeNo As Short = 5
    Private Const ColChequeDate As Short = 6
    Private Const ColEmp As Short = 7
    Private Const ColDept As Short = 8
    Private Const ColCC As Short = 9
    Private Const ColExp As Short = 10
    Private Const ColDivisionCode As Short = 11
    Private Const ColIBRNo As Short = 12
    Private Const ColAmount As Short = 13

    Private Const ColSAC As Short = 14
    Private Const ColCGSTPer As Short = 15
    Private Const ColCGSTAmount As Short = 16
    Private Const ColSGSTPer As Short = 17
    Private Const ColSGSTAmount As Short = 18
    Private Const ColIGSTPer As Short = 19
    Private Const ColIGSTAmount As Short = 20

    Private Const ColSaleBillPrefix As Short = 21
    Private Const ColSaleBillSeq As Short = 22
    Private Const ColSaleBillNo As Short = 23
    Private Const ColSaleBillDate As Short = 24

    Private Const ColClearDate As Short = 25

    Private Const ConRowHeight As Short = 15

    Private Const mBookType As String = "F"



    Dim pProcessKey As Double
    Private Sub CmdClear_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClear.Click
        On Error GoTo ClearErr
        txtPartyName.Text = ""
        txtVno.Text = ""
        txtNarration.Text = ""
        txtFYear.Text = ""
        lblMKey.Text = ""
        txtVDate.Text = ""
        txtNarration.Text = ""
        LblDrAmt.Text = ""
        LblCrAmt.Text = ""
        LblNetAmt.Text = ""
        txtVno.Enabled = True
        txtVDate.Enabled = False
        txtFYear.Enabled = True
        cboVoucher.Enabled = True
        CmdSave.Enabled = False
        cboVoucher.SelectedIndex = 0
        cmdShow.Enabled = True
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        If FieldVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
            CmdSave.Enabled = False
            MsgInformation("Record saved")
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim mBookCode As String
        Dim SqlStr As String
        Dim mAccountCode As String
        Dim mDrCr As String
        Dim mVAmount As Double

        Dim mVnoStr As String
        Dim mVType As String

        Dim mVNoPrefix As String
        Dim mVNoSuffix As String

        Dim mBookType As String
        Dim mBookSubType As String
        Dim mVNO As String
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
        Dim mVDate As String
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
        Dim mRowNo As Integer
        Dim CurMKey As String
        Dim mNarration As String
        Dim mMsg As String
        Dim mLocCode As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        txtNarration.Text = Trim(Replace(txtNarration.Text, vbCrLf, ""))


        mBookType = VB.Left(lblNewBookType.Text, 1)
        mBookSubType = VB.Right(lblNewBookType.Text, 1)
        mVType = lblVType.Text '' "JV"								

        mVNoPrefix = GenPrefixVNo(txtVDate.Text)
        mVNoSuffix = ""
        mVNO = GenVno(mVType)
        mVnoStr = mVType & mVNoPrefix & mVNO & mVNoSuffix


        mCancelled = "N"

        Select Case lblNewBookType.Text
            Case ConJournal
                mBookCode = CStr(ConJournalBookCode)
            Case ConContra
                mBookCode = CStr(ConContraBookCode)
            Case Else
                If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mBookCode = MasterNo
                End If
        End Select

        mVDate = GetVDate(PubCurrDate)
        mExpDate = VB6.Format(mVDate, "DD/MM/YYYY")

        mPLFlag = "N"

        mIsSuppBill = "N"
        mIsCapital = "N"

        mISMODVAT = "N"
        mIsPLA = "N"
        mIsSTClaim = "N"
        mIsServtaxClaim = "N"
        mIsServTaxRefund = "N"

        mExpPartyCode = ""
        mImpPartyCode = ""


        mISTDSDEDUCT = "N"
        mISESIDEDUCT = "N"
        mISSTDSDEDUCT = "N"
        mReverseChargeApp = "N"
        mServiceCode = -1

        mNarration = Trim(txtNarration.Text) & " (Reversal of Voucher No : " & txtVno.Text & " Dated : " & VB6.Format(txtVDate.Text) & ")"

        mRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
        CurMKey = RsCompany.Fields("COMPANY_CODE").Value & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(mRowNo)

        SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf & " Mkey, COMPANY_CODE, " & vbCrLf & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf & " Vno, Vdate, BookType,BookSubType, " & vbCrLf & " BookCode, Narration, CANCELLED, " & vbCrLf & " ISTDSDEDUCT,TDSPER,TDSAMOUNT, " & vbCrLf & " ISESIDEDUCT,ESIPER,ESIAMOUNT, ISSTDSDEDUCT,STDSPER,STDSAMOUNT," & vbCrLf & " ISSUPPBILL,MODVATNO ,STREFUNDNO, ISCAPITAL," & vbCrLf & " IMP_SUPP_CUST_CODE, IMP_MRR_NO, " & vbCrLf & " IMP_BILL_NO, IMP_BILL_DATE,  " & vbCrLf & " EXP_SUPP_CUST_CODE, EXP_BILL_NO,  " & vbCrLf & " EXP_BILL_DATE, AUTHORISED, " & vbCrLf & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM,EXPDATE, " & vbCrLf & " ISMODVAT, ISPLA, ISSTCLAIM, ISSERVTAXCLAIM, ISSERVTAXREFUND, SERVNO, PL_FLAG, " & vbCrLf & " SERVICE_CODE, SERVICE_ON_AMT, SERVICE_TAX_PER, " & vbCrLf & " SERVICE_TAX_AMOUNT, SERV_PROVIDER_PER, SERV_RECIPIENT_PER,REVERSE_CHARGE_APP, " & vbCrLf & " IS_REVERSAL_MADE, IS_REVERSAL_VOUCHER, REVERSAL_MKEY) VALUES ( "

        SqlStr = SqlStr & vbCrLf & " '" & CurMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mRowNo & ", " & vbCrLf & " '" & mVType & "', '" & mVNoPrefix & "', " & vbCrLf & " " & Val(mVNO) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf _
            & " TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf & " '" & mBookCode & "', '" & MainClass.AllowSingleQuote(txtNarration.Text) & "', '" & mCancelled & "', " & vbCrLf & " '" & mISTDSDEDUCT & "',0, 0, " & vbCrLf & " '" & mISESIDEDUCT & "',0, 0, " & vbCrLf & " '" & mISSTDSDEDUCT & "',0, 0, " & vbCrLf & " '" & mIsSuppBill & "',0,0, " & vbCrLf & " '" & mIsCapital & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mImpPartyCode) & "', " & "Null" & "," & vbCrLf & " '', ''," & vbCrLf & " '', " & "Null" & "," & vbCrLf & " '', 'N', " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','H', " & vbCrLf _
            & " TO_DATE('" & VB6.Format(mExpDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mISMODVAT & "','" & mIsPLA & "','" & mIsSTClaim & "','" & mIsServtaxClaim & "'," & vbCrLf & "'" & mIsServTaxRefund & "',0,'" & mPLFlag & "', " & vbCrLf & " " & "Null" & ", 0, 0, " & vbCrLf & " 0, 0, 0," & vbCrLf & " '" & mReverseChargeApp & "','N','Y','" & lblMKey.Text & "')"


        PubDBCn.Execute(SqlStr)

        If UpdateDetail(CurMKey, mRowNo, mBookType, mBookSubType, mBookCode, mVType, mVnoStr, mVDate, mNarration, PubDBCn) = False Then GoTo ErrPart

        If lblNewBookType.Text = ConJournal Or lblNewBookType.Text = ConContra Then

        Else
            xSqlStr = "SELECT DIV_CODE FROM INV_DIVISION_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSDiv, ADODB.LockTypeEnum.adLockReadOnly)

            If RSDiv.EOF = False Then
                mSubRowNo = -1
                Do While RSDiv.EOF = False
                    mVAmount = 0
                    mDivCode = IIf(IsDBNull(RSDiv.Fields("DIV_CODE").Value), -1, RSDiv.Fields("DIV_CODE").Value)

                    For cntRow = 1 To SprdMain.MaxRows - 1
                        SprdMain.Row = cntRow

                        SprdMain.Col = ColAccountName
                        mSuppCustName = MainClass.AllowSingleQuote(SprdMain.Text)

                        SprdMain.Col = ColPRRowNo
                        mPRowNo = Val(SprdMain.Text)

                        If GetAccountBalancingMethod(mSuppCustName, False) = "D" Then
                            If GetBillDetailAmount(mPRowNo, mSuppCustName, mDivCode, mDC, mSuppCustAmount) = True Then
                                mVAmount = mVAmount + (mSuppCustAmount * IIf(UCase(mDC) = "D", 1, -1))
                                mDC = IIf(mDC = "D", "CR", "DR") ''Book Code Update								
                            Else
                                mDC = "DR"
                            End If
                        Else
                            SprdMain.Col = ColDivisionCode
                            mChkDivCode = Val(SprdMain.Text)

                            If mDivCode = mChkDivCode Then
                                SprdMain.Col = ColDC
                                mDC = Trim(SprdMain.Text)

                                SprdMain.Col = ColAmount
                                mVAmount = mVAmount + (Val(SprdMain.Text) * IIf(UCase(mDC) = "CR", 1, -1))

                                '                            If chkReverseCharge.Value = Unchecked Then								
                                SprdMain.Col = ColCGSTAmount
                                mVAmount = mVAmount + (Val(SprdMain.Text) * IIf(UCase(mDC) = "CR", 1, -1))

                                SprdMain.Col = ColSGSTAmount
                                mVAmount = mVAmount + (Val(SprdMain.Text) * IIf(UCase(mDC) = "CR", 1, -1))

                                SprdMain.Col = ColIGSTAmount
                                mVAmount = mVAmount + (Val(SprdMain.Text) * IIf(UCase(mDC) = "CR", 1, -1))
                                '                            End If								
                            End If
                        End If
                    Next

                    mDrCr = IIf(mVAmount > 0, "C", "D")
                    mVAmount = Val(CStr(System.Math.Abs(mVAmount)))
                    If mCancelled = "Y" Then
                        mVAmount = 0
                    End If

                    If mVAmount <> 0 Then
                        mLocCode = GetDefaultLocation(mBookCode)
                        If UpdateTRN(PubDBCn, CurMKey, mRowNo, mSubRowNo, mBookCode, mVType, mBookType, mBookSubType, mBookCode, mVnoStr, mVDate, mVnoStr, mVDate, mVAmount, mDrCr, "P", "", "", CStr(-1), CStr(-1), CStr(-1), CStr(-1), "", "", "P", "", "", mNarration, "", mExpDate, True, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivCode, mLocCode, "N") = False Then GoTo ErrPart

                        mSubRowNo = mSubRowNo - 1
                    End If
                    RSDiv.MoveNext()

                Loop
            Else
                mVAmount = Val(CStr(CDbl(LblNetAmt.Text)))
                mDrCr = IIf(Val(CStr(CDbl(LblDrAmt.Text))) >= Val(CStr(CDbl(LblCrAmt.Text))), "D", "C")
                If mCancelled = "Y" Then
                    mVAmount = 0
                End If
                mLocCode = GetDefaultLocation(mBookCode)
                If UpdateTRN(PubDBCn, CurMKey, mRowNo, -1, mBookCode, mVType, mBookType, mBookSubType, mBookCode, mVnoStr, mVDate, mVnoStr, mVDate, mVAmount, mDrCr, "P", "", "", CStr(-1), CStr(-1), CStr(-1), CStr(-1), "", "", "P", "", "", mNarration, "", mExpDate, True, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivCode, mLocCode, "N") = False Then GoTo ErrPart
            End If
        End If

        SqlStr = "UPDATE FIN_VOUCHER_HDR SET IS_REVERSAL_MADE='Y' WHERE MKEY='" & lblMKey.Text & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()



        '								
        Select Case lblNewBookType.Text
            Case ConCashReceipt
                mMsg = "Cash Receipt Voucher"
            Case ConCashPayment
                mMsg = "Cash Payment Voucher"
            Case ConBankReceipt
                mMsg = "Bank Receipt Voucher"
            Case ConBankPayment
                mMsg = "Bank Payment Voucher"
            Case ConContra
                mMsg = "Contra Voucher"
'        Case ConPDCReceipt								
'								
'        Case ConPDCPayment								

            Case ConJournal
                mMsg = "Journal Voucher"
                '        Case "Purchase"								
                '								
                '        Case "General Purchase"								
                '								
                '        Case "Debit Note"								
                '								
                '        Case "Credit Note"								
                '								
                '        Case "Sale"								
                '								
                '        Case "Customer Debit Note"								
                '								
                '        Case "Sale Return"								

        End Select

        MsgBox(mMsg & "Voucher No. " & mVnoStr & " Created. ", MsgBoxStyle.Information)


        Update1 = True

        Exit Function
ErrPart:
        '    Resume								
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''								
        Update1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function
    Private Function GetBillDetailAmount(ByRef mPRowNo As Integer, ByRef mSuppCustName As String, ByRef mDivCode As Double, ByRef mDC As String, ByRef mSuppCustAmount As Double) As Boolean
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
        SqlStr = "SELECT SUM(AMOUNT * DECODE(DC,'D',1,-1)) AS AMOUNT FROM FIN_TEMPBILL_TRN  Where UserID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & "" & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'" & vbCrLf & " AND TRNDTLSUBROWNO=" & Val(CStr(mPRowNo)) & "" & vbCrLf & " AND BOOKTYPE='" & UCase(Trim(lblNewBookType.Text)) & "' AND DIV_CODE= " & mDivCode & ""

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
    Private Function UpdateDetail(ByRef mMKey As String, ByRef mRowNo As Integer, ByRef mBookType As String, ByRef mBookSubType As String, ByRef mBookCode As String, ByRef mVType As String, ByRef mVNO As String, ByRef mVDate As String, ByRef pNarration As String, ByRef pDBCn As ADODB.Connection) As Boolean

        On Error GoTo ErrDetail

        Dim I As Integer
        Dim SqlStr As String
        Dim mAccountName As String
        Dim mAccountCode As String
        Dim mChequeNo As String
        Dim mChqDate As String
        Dim mAmount As Double
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


        SqlStr = "Delete From FIN_BILLDETAILS_TRN Where Mkey='" & mMKey & "'"
        pDBCn.Execute(SqlStr)

        SqlStr = "Delete From FIN_SERVTAXDETAILS_TRN Where Mkey='" & mMKey & "'"
        pDBCn.Execute(SqlStr)

        SqlStr = "Delete From PAY_LOAN_MST Where Mkey='" & mMKey & "'"
        pDBCn.Execute(SqlStr)

        SqlStr = "Delete From FIN_VOUCHER_DET Where Mkey='" & mMKey & "'"
        pDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE " & vbCrLf & " MKEY ='" & mMKey & "' " & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BooksubType='" & mBookSubType & "'"
        pDBCn.Execute(SqlStr)


        mSameVNo = False
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColAccountName
                mAccountName = Trim(.Text)
                .Col = 0
                If mAccountName <> "" Then
                    .Col = ColPRRowNo
                    mPRRowNo = Val(.Text)

                    mSubRowNo = mPRRowNo

                    .Col = ColDC
                    mDC = UCase(VB.Left(.Text, 1))
                    mDC = IIf(mDC = "D", "C", "D")

                    .Col = ColAccountName
                    mAccountCode = IIf(MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, -1)

                    If MainClass.ValidateWithMasterTable(mAccountCode, "ACCOUNTPOSTCODE", "ISFIXASSETS", "FIN_INVTYPE_MST", pDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                        mIsFixedAssets = MasterNo
                    Else
                        mIsFixedAssets = "N"
                    End If

                    .Col = ColParticulars
                    mParticulars = pNarration '' trim(txtNarration.Text)								

                    .Col = ColAmount
                    mAmount = Val(.Text)

                    .Col = ColChequeNo
                    mChequeNo = ""

                    .Col = ColChequeDate
                    mChqDate = mVDate

                    .Col = ColCC
                    mCCCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "CC_CODE", "CC_CODE", "FIN_CCENTER_HDR", pDBCn, mCCCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mCCCode, -1)

                    .Col = ColExp
                    mExpCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "COST_CENTER_CODE", "COST_CENTER_CODE", "CST_CENTER_MST", pDBCn, mExpCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mExpCode, -1)

                    .Col = ColDept
                    mDeptCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "DEPT_CODE", "DEPT_CODE", "PAY_DEPT_MST", pDBCn, mDeptCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mDeptCode, -1)

                    .Col = ColDivisionCode
                    mDivisionCode = IIf(MainClass.ValidateWithMasterTable(Val(SprdMain.Text), "DIV_CODE", "DIV_CODE", "INV_DIVISION_MST", pDBCn, mDivisionCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mDivisionCode, -1)

                    .Col = ColEmp
                    mEmpCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", pDBCn, mEmpCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mEmpCode, -1)

                    .Col = ColIBRNo
                    mIBRNo = .Text

                    .Col = ColSAC
                    If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", pDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                        mSAC = .Text
                    Else
                        mSAC = ""
                    End If

                    .Col = ColCGSTPer
                    mCGSTPer = Val(.Text)

                    .Col = ColCGSTAmount
                    mCGSTAmount = Val(.Text)

                    .Col = ColSGSTPer
                    mSGSTPer = Val(.Text)

                    .Col = ColSGSTAmount
                    mSGSTAmount = Val(.Text)

                    .Col = ColIGSTPer
                    mIGSTPer = Val(.Text)

                    .Col = ColIGSTAmount
                    mIGSTAmount = Val(.Text)

                    SprdMain.Col = ColSaleBillPrefix
                    mSaleBillPrefix = .Text

                    SprdMain.Col = ColSaleBillSeq
                    mSaleBillSeq = .Text

                    SprdMain.Col = ColSaleBillNo
                    mSaleBillNo = .Text

                    SprdMain.Col = ColSaleBillDate
                    mSaleBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                    .Col = ColClearDate
                    mClearDate = .Text


                    SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate, " & vbCrLf & " PARTICULARS,DIV_CODE, " & vbCrLf & " SAC, CGST_PER, CGST_AMOUNT, " & vbCrLf & " SGST_PER, SGST_AMOUNT, IGST_PER, IGST_AMOUNT," & vbCrLf & " SALEBILLNOPREFIX, SALEBILLNOSEQ, SALEBILL_NO, SALEBILLDATE" & vbCrLf & " )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & ",  " & vbCrLf & " '" & mMKey & "', " & mPRRowNo & ", " & vbCrLf & " " & mSubRowNo & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & "," & vbCrLf _
                        & " '" & mSAC & "', " & mCGSTPer & ", " & mCGSTAmount & ", " & vbCrLf _
                        & " " & mSGSTPer & ", " & mSGSTAmount & "," & vbCrLf & " " & mIGSTPer & ", " & mIGSTAmount & ", " & vbCrLf _
                        & " '" & mSaleBillPrefix & "', '" & mSaleBillSeq & "', '" & mSaleBillNo & "', TO_DATE('" & VB6.Format(mSaleBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                        & " )"

                    PubDBCn.Execute(SqlStr)

                    '                If chkReverseCharge.Value = vbChecked Then								
                    '                    mNetAmount = mAmount								
                    '                Else								
                    mNetAmount = mAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount
                    '                End If								

                    If UpdatePRDetail(pDBCn, mMKey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNO, mVDate, mDC, mNetAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, pNarration, "N", mBookType, mBookSubType, VB6.Format(mVDate, "DD/MM/YYYY"), True, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivisionCode, pProcessKey, "N", "N", mSAC, mCGSTPer, mCGSTAmount, mSGSTPer, mSGSTAmount, mIGSTPer, mIGSTAmount) = False Then GoTo ErrDetail

                    '                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "HEADTYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then								
                    '                    If MasterNo = "L" Then								
                    '                        If UpdateLoanDetail(pDBCn, mMkey, mEmpCode, "N") = False Then GoTo ErrDetail								
                    '                    End If								
                    '                End If								

                End If
                mSameVNo = True
            Next I

        End With
        UpdateDetail = True
        Exit Function
ErrDetail:
        'Resume								
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDetail = False
        'Resume								
    End Function
    Private Function GetVDate(ByRef mCurrDate As Date) As String
        On Error GoTo CheckLastestVDateErr
        Dim SqlStr As String
        Dim RsCheck As ADODB.Recordset '' ADODB.Recordset								
        Dim mStartCheckDate As String
        Dim mEndCheckDate As String

        mStartCheckDate = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
        mEndCheckDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")

        If CDate(mCurrDate) >= CDate(mStartCheckDate) And CDate(mCurrDate) <= CDate(mEndCheckDate) Then
            GetVDate = VB6.Format(mCurrDate, "DD/MM/YYYY")
        ElseIf CDate(mCurrDate) > CDate(mEndCheckDate) Then
            GetVDate = VB6.Format(mEndCheckDate, "DD/MM/YYYY")
        ElseIf CDate(mCurrDate) < CDate(mStartCheckDate) Then
            GetVDate = VB6.Format(mStartCheckDate, "DD/MM/YYYY")
        End If

        Exit Function

CheckLastestVDateErr:
        GetVDate = ""
    End Function
    Private Function GenVno(ByRef xVTYPE As String) As String
        On Error GoTo ERR1
        Dim mVNo1 As String
        Dim SqlStr2 As String
        Dim SqlStr As String
        Dim mBookType As String
        Dim mBookSubType As String


        mBookType = VB.Left(lblNewBookType.Text, 1)
        mBookSubType = VB.Right(lblNewBookType.Text, 1)

        SqlStr = "SELECT MAX(VNOSeq) From FIN_VOUCHER_HDR WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BookType='" & mBookType & "'" & vbCrLf _
            & " AND BookSubType='" & mBookSubType & "'" & vbCrLf _
            & " AND VTYPE='" & MainClass.AllowSingleQuote(xVTYPE) & "'"

        If RsCompany.Fields("CBJVoucherSeq").Value = "D" Then
            SqlStr = SqlStr & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "M" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(VDATE,'MMMYYYY')=TO_CHAR('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','MMMYYYY')"
        ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "Y" Then

        End If

        GenVno = VB6.Format(MainClass.AutoGenVNo(SqlStr, PubDBCn), "00000")

        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume								
    End Function
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        Dim SqlStr As String
        Dim RsVoucher As ADODB.Recordset

        If Trim(txtVno.Text) = "" Then
            MsgInformation("Please Enter the Voucher No.")
            Exit Sub
        End If

        If Val(txtFYear.Text) = 0 Then
            MsgInformation("Please Enter the Finacial Year.")
            Exit Sub
        End If

        If Val(txtFYear.Text) > RsCompany.Fields("FYEAR").Value Then
            MsgInformation("Please Enter the Vaild Finacial Year.")
            Exit Sub
        End If


        Select Case cboVoucher.Text
            Case "Cash Receipt"
                lblBookType.Text = ConCashReceipt
                lblNewBookType.Text = ConCashPayment
                lblBookCode.Text = "-1"
            Case "Cash Payment"
                lblBookType.Text = ConCashPayment
                lblNewBookType.Text = ConCashReceipt
                lblBookCode.Text = "-1"
            Case "Bank Receipt"
                lblBookType.Text = ConBankReceipt
                lblNewBookType.Text = ConBankPayment
                lblBookCode.Text = "-1"
            Case "Bank Payment"
                lblBookType.Text = ConBankPayment
                lblNewBookType.Text = ConBankReceipt
                lblBookCode.Text = "-1"
            Case "Contra Entry"
                lblBookType.Text = ConContra
            Case "PDC Receipt"
                lblBookType.Text = ConPDCReceipt
            Case "PDC Payment"
                lblBookType.Text = ConPDCPayment
            Case "Journal"
                lblBookType.Text = ConJournal
                lblNewBookType.Text = ConJournal
                lblBookCode.Text = CStr(ConJournalBookCode)
            Case "Purchase"
                lblBookType.Text = ConPurchase
            Case "General Purchase"
                lblBookType.Text = ConPurchaseGen
            Case "Debit Note"
                lblBookType.Text = ConDebitNote
            Case "Credit Note"
                lblBookType.Text = ConCreditNote
            Case "Sale"
                lblBookType.Text = ConSale
            Case "Customer Debit Note"
                lblBookType.Text = ConSaleDebit
            Case "Sale Return"
                lblBookType.Text = ConPurchase
        End Select

        SqlStr = " SELECT * FROM FIN_VOUCHER_HDR WHERE " & vbCrLf & " Vno='" & Trim(txtVno.Text) & "'" & vbCrLf & " AND Booktype='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND BookSubType='" & VB.Right(lblBookType.Text, 1) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & Val(txtFYear.Text) & "" & vbCrLf & " AND CANCELLED='N' AND IS_REVERSAL_MADE='N' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVoucher, ADODB.LockTypeEnum.adLockReadOnly)

        If RsVoucher.EOF = False Then
            Clear1()
            Show1(RsVoucher)
            cmdShow.Enabled = False
        Else
            MsgInformation("Invaild Voucher No")
            Exit Sub
        End If
    End Sub
    Private Sub Show1(ByRef pRsVoucher As ADODB.Recordset)
        On Error GoTo ShowErrPart
        'Dim PKey As String								
        'Dim SqlStr As String								
        'Dim RS As ADODB.Recordset            '' ADODB.Recordset								
        'Dim mOPBal As Double								
        '								
        'Dim mPartyCode As String								
        'Dim mServiceCode As Double								

        Dim CurMKey As String
        Dim mBookCode As String

        If pRsVoucher.EOF = True Then Exit Sub


        lblMKey.Text = pRsVoucher.Fields("mKey").Value
        '    mRowNo = pRsVoucher.Fields("RowNo").Value								
        ''CANCELLED								

        txtVno.Text = IIf(IsDBNull(pRsVoucher.Fields("VNO").Value), "", pRsVoucher.Fields("VNO").Value)
        txtVDate.Text = IIf(IsDBNull(pRsVoucher.Fields("VDate").Value), "", pRsVoucher.Fields("VDate").Value)
        lblVType.Text = IIf(IsDBNull(pRsVoucher.Fields("VTYPE").Value), "", pRsVoucher.Fields("VTYPE").Value)
        mBookCode = IIf(IsDBNull(pRsVoucher.Fields("BOOKCODE").Value), "", pRsVoucher.Fields("BOOKCODE").Value)

        Select Case lblNewBookType.Text
            Case ConJournal
                txtPartyName.Text = ""
            Case ConContra
                txtPartyName.Text = ""
            Case Else
                If MainClass.ValidateWithMasterTable(mBookCode, "SUPP_CUST_Code", "SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtPartyName.Text = MasterNo
                End If
        End Select

        ShowDetail()

        CopyToTempPRDetail((lblMKey.Text))
        CalcTots()

        SprdMain.Enabled = True

        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume								
    End Sub
    Private Sub CopyToTempPRDetail(ByRef pMKey As String)
        On Error GoTo ERR1
        Dim SqlStr As String


        SqlStr = "Insert Into FIN_TEMPBILL_TRN  ( " & vbCrLf & " UserId, TRNDTLSUBROWNO, SUBROWNO, " & vbCrLf & " ACCOUNTCODE, BILLNO, BILLDATE, " & vbCrLf & " BILLAMOUNT, BILLDC, TRNTYPE, " & vbCrLf & " Amount, DC, BOOKTYPE, REMARKS,  " & vbCrLf & " OldAmount, OldDC, OldBillNo, " & vbCrLf & " OldPayType,DUEDATE, " & vbCrLf & " STTYPE, STFORMNAME, STFORMNO, " & vbCrLf & " STFORMDATE, STFORMAMT, STDUEFORMNAME, " & vbCrLf & " STDUEFORMNO, STDUEFORMDATE, STDUEFORMAMT, " & vbCrLf & " ISREGDNO, STFORMCODE, STDUEFORMCODE,TAXABLE_AMOUNT,PONO,DIV_CODE,REF_NO,TEMPMKEY " & vbCrLf & " )"


        SqlStr = SqlStr & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "' , " & vbCrLf & " TRNDTLSUBROWNO, SUBROWNO, " & vbCrLf & " ACCOUNTCODE, BILLNO, BILLDATE, " & vbCrLf & " BILLAMOUNT, BILLDC,TRNTYPE,Amount,DECODE(DC,'D','C','D'), " & vbCrLf & " '" & Trim(UCase(lblNewBookType.Text)) & "', " & vbCrLf & " REMARKS, AMOUNT, DC, BILLNO, TRNTYPE,DUEDATE, " & vbCrLf & " STTYPE, STFORMNAME, STFORMNO, " & vbCrLf & " STFORMDATE, STFORMAMT, STDUEFORMNAME, " & vbCrLf & " STDUEFORMNO, STDUEFORMDATE, STDUEFORMAMT, " & vbCrLf & " ISREGDNO, STFORMCODE, STDUEFORMCODE,TAXABLE_AMOUNT,PONO,DIV_CODE,REF_NO," & pProcessKey & "" & vbCrLf & " FROM FIN_BILLDETAILS_TRN Where MKey='" & pMKey & "'"

        PubDBCn.Execute(SqlStr)


        Exit Sub
ERR1:
        '    Resume								
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub CalcTots()
        On Error GoTo ErrSprdTotal
        Dim mDAmt As Double
        Dim mCAmt As Double
        Dim mNetAmt As Double
        Dim MTotalAmt As Double
        Dim cntRow As Integer
        Dim mPartyAmt As Double

        mPartyAmt = 0
        For cntRow = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = cntRow

            SprdMain.Col = ColDC
            If VB.Left(SprdMain.Text, 1) = "D" Then
                SprdMain.Col = ColAmount
                mDAmt = mDAmt + Val(SprdMain.Value)

                '            If chkReverseCharge.Value = vbUnchecked Then								
                '                SprdMain.Col = ColCGSTAmount								
                '                mDAmt = mDAmt + Val(SprdMain.Value)								
                '								
                '                SprdMain.Col = ColSGSTAmount								
                '                mDAmt = mDAmt + Val(SprdMain.Value)								
                '								
                '                SprdMain.Col = ColIGSTAmount								
                '                mDAmt = mDAmt + Val(SprdMain.Value)								
                '								
                '            End If								
            Else
                SprdMain.Col = ColAmount
                mCAmt = mCAmt + Val(SprdMain.Value)
                If mPartyAmt = 0 Then
                    mPartyAmt = Val(SprdMain.Value)
                End If
            End If
            mNetAmt = System.Math.Abs(mCAmt - mDAmt)
NextRow:
        Next cntRow

        LblDrAmt.Text = VB6.Format(mDAmt, "##,##,##,##0.00")
        LblCrAmt.Text = VB6.Format(mCAmt, "##,##,##,##0.00")
        LblNetAmt.Text = VB6.Format(mNetAmt, "##,##,##,##0.00")

        Exit Sub

ErrSprdTotal:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume								
    End Sub

    Private Sub ShowDetail()
        On Error GoTo ShowErr
        Dim RsVoucherDet As ADODB.Recordset
        Dim SqlStr As String
        Dim mAccountCode As String
        Dim mNewAccountCode As String

        ', FIN_SUPP_CUST_MST.SUPP_CUST_NAME AS ACCOUNTNAME								
        '',FIN_SUPP_CUST_MST								

        SqlStr = "SELECT FIN_VOUCHER_DET.*" & vbCrLf & " FROM FIN_VOUCHER_DET WHERE MKEY= '" & lblMKey.Text & "' Order By SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVoucherDet, ADODB.LockTypeEnum.adLockReadOnly)

        If RsVoucherDet.EOF = True Then Exit Sub

        Do While RsVoucherDet.EOF = False

            SprdMain.Row = SprdMain.MaxRows

            SprdMain.Col = ColPRRowNo
            SprdMain.Text = Str(IIf(IsDBNull(RsVoucherDet.Fields("PRRowNo").Value), 0, RsVoucherDet.Fields("PRRowNo").Value))

            SprdMain.Col = ColDC
            SprdMain.Text = RsVoucherDet.Fields("DC").Value + "r"

            mNewAccountCode = RsVoucherDet.Fields("ACCOUNTCODE").Value
            SprdMain.Col = ColAccountName
            If MainClass.ValidateWithMasterTable(mNewAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SprdMain.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If

            '        SprdMain.Text = IIf(IsNull(RsVoucherDet.Fields("AccountName").Value), "", RsVoucherDet.Fields("AccountName").Value)								

            SprdMain.Col = ColParticulars
            SprdMain.Text = IIf(IsDBNull(RsVoucherDet.Fields("PARTICULARS").Value), "", RsVoucherDet.Fields("PARTICULARS").Value)

            SprdMain.Col = ColChequeNo
            SprdMain.Text = IIf(Not IsDBNull(RsVoucherDet.Fields("ChequeNo").Value), RsVoucherDet.Fields("ChequeNo").Value, "")

            SprdMain.Col = ColChequeDate
            SprdMain.Text = VB6.Format(IIf(Not IsDBNull(RsVoucherDet.Fields("CHQDATE").Value), RsVoucherDet.Fields("CHQDATE").Value, ""), "DD/MM/YYYY")

            SprdMain.Col = ColCC
            If RsVoucherDet.Fields("COSTCCODE").Value <> -1 Then
                '            If MainClass.ValidateWithMasterTable(RsVoucherDet.Fields("CostCCode").Value, "COST_CENTER_CODE", "Alias", "CST_CENTER_MST", PubDBCn, MasterNo) = True Then								
                '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)								
                '            End If								
                SprdMain.Text = IIf(IsDBNull(RsVoucherDet.Fields("COSTCCODE").Value), "", RsVoucherDet.Fields("COSTCCODE").Value)
            Else
                SprdMain.Text = ""
            End If

            SprdMain.Col = ColExp
            If RsVoucherDet.Fields("EXP_CODE").Value <> -1 Then
                SprdMain.Text = IIf(IsDBNull(RsVoucherDet.Fields("EXP_CODE").Value), "", RsVoucherDet.Fields("EXP_CODE").Value)
            Else
                SprdMain.Text = ""
            End If

            SprdMain.Col = ColDept
            If RsVoucherDet.Fields("DeptCode").Value <> -1 Then
                '            If MainClass.ValidateWithMasterTable(RsVoucherDet.Fields("DeptCode").Value, "Code", "Alias", "Dept", PubDBCn, MasterNo) = True Then								
                '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)								
                '            End If								
                SprdMain.Text = IIf(IsDBNull(RsVoucherDet.Fields("DeptCode").Value), "", RsVoucherDet.Fields("DeptCode").Value)
            Else
                SprdMain.Text = ""
            End If

            SprdMain.Col = ColDivisionCode
            If RsVoucherDet.Fields("DIV_CODE").Value <> -1 Then
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsVoucherDet.Fields("DIV_CODE").Value), "", RsVoucherDet.Fields("DIV_CODE").Value)))
            Else
                SprdMain.Text = ""
            End If

            SprdMain.Col = ColEmp
            If RsVoucherDet.Fields("EMPCODE").Value <> -1 Then
                '            If MainClass.ValidateWithMasterTable(RsVoucherDet.Fields("EMPCODE").Value, "Code", "Alias", "Emp", PubDBCn, MasterNo) = True Then								
                '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)								
                '            End If								
                SprdMain.Text = IIf(IsDBNull(RsVoucherDet.Fields("EMPCODE").Value), "", RsVoucherDet.Fields("EMPCODE").Value)
            Else
                SprdMain.Text = ""
            End If

            SprdMain.Col = ColIBRNo
            SprdMain.Text = IIf(Not IsDBNull(RsVoucherDet.Fields("IBRNo").Value), RsVoucherDet.Fields("IBRNo").Value, "")

            SprdMain.Col = ColAmount
            SprdMain.Text = Str(RsVoucherDet.Fields("Amount").Value)

            SprdMain.Col = ColSAC
            SprdMain.Text = IIf(Not IsDBNull(RsVoucherDet.Fields("SAC").Value), RsVoucherDet.Fields("SAC").Value, "")

            SprdMain.Col = ColCGSTPer
            SprdMain.Text = Str(IIf(IsDBNull(RsVoucherDet.Fields("CGST_PER").Value), 0, RsVoucherDet.Fields("CGST_PER").Value))

            SprdMain.Col = ColCGSTAmount
            SprdMain.Text = Str(IIf(IsDBNull(RsVoucherDet.Fields("CGST_AMOUNT").Value), 0, RsVoucherDet.Fields("CGST_AMOUNT").Value))

            SprdMain.Col = ColSGSTPer
            SprdMain.Text = Str(IIf(IsDBNull(RsVoucherDet.Fields("SGST_PER").Value), 0, RsVoucherDet.Fields("SGST_PER").Value))

            SprdMain.Col = ColSGSTAmount
            SprdMain.Text = Str(IIf(IsDBNull(RsVoucherDet.Fields("SGST_AMOUNT").Value), 0, RsVoucherDet.Fields("SGST_AMOUNT").Value))

            SprdMain.Col = ColIGSTPer
            SprdMain.Text = Str(IIf(IsDBNull(RsVoucherDet.Fields("IGST_PER").Value), 0, RsVoucherDet.Fields("IGST_PER").Value))

            SprdMain.Col = ColIGSTAmount
            SprdMain.Text = Str(IIf(IsDBNull(RsVoucherDet.Fields("IGST_AMOUNT").Value), 0, RsVoucherDet.Fields("IGST_AMOUNT").Value))

            SprdMain.Col = ColSaleBillPrefix
            SprdMain.Text = IIf(IsDBNull(RsVoucherDet.Fields("SALEBILLNOPREFIX").Value), "", RsVoucherDet.Fields("SALEBILLNOPREFIX").Value)

            SprdMain.Col = ColSaleBillSeq
            SprdMain.Text = Str(IIf(IsDBNull(RsVoucherDet.Fields("SALEBILLNOSEQ").Value), 0, RsVoucherDet.Fields("SALEBILLNOSEQ").Value))

            SprdMain.Col = ColSaleBillNo
            SprdMain.Text = IIf(IsDBNull(RsVoucherDet.Fields("SALEBILL_NO").Value), "", RsVoucherDet.Fields("SALEBILL_NO").Value)

            SprdMain.Col = ColSaleBillDate
            SprdMain.Text = IIf(IsDBNull(RsVoucherDet.Fields("SALEBILLDATE").Value), "", RsVoucherDet.Fields("SALEBILLDATE").Value)

            SprdMain.Col = ColClearDate
            SprdMain.Text = IIf(IsDBNull(RsVoucherDet.Fields("ClearDate").Value), "", RsVoucherDet.Fields("ClearDate").Value)

            '        SprdMain.Col = ColSaleBillNo								
            '        If lblSaleBillNo.Caption = "" Then								
            '            lblSaleBillNo.Caption = IIf(SprdMain.Text = "", "", SprdMain.Text)								
            '        Else								
            '            lblSaleBillNo.Caption = IIf(SprdMain.Text = "", lblSaleBillNo.Caption, lblSaleBillNo.Caption & "," & SprdMain.Text)								
            '        End If								


            SprdMain.MaxRows = SprdMain.MaxRows + 1
            RsVoucherDet.MoveNext()
        Loop
        '    FormatSprdMain -1								
        '    FormatSprdMainGST -1								
        Exit Sub
ShowErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume								
    End Sub
    Private Sub frmReversalVoucher_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SetTextLengths()
        Clear1()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmReversalVoucher_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '''''Set PvtDBCn = New ADODB.Connection								
        '''''PvtDBCn.Open StrConn								
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        cboVoucher.Items.Clear()

        cboVoucher.Items.Add("Cash Receipt")
        cboVoucher.Items.Add("Cash Payment")
        cboVoucher.Items.Add("Bank Receipt")
        cboVoucher.Items.Add("Bank Payment")
        cboVoucher.Items.Add("Journal")
        'cboVoucher.AddItem "Contra Entry"								
        '    cboVoucher.AddItem "PDC Receipt"								
        '    cboVoucher.AddItem "PDC Payment"								

        'cboVoucher.AddItem "Purchase"								
        'cboVoucher.AddItem "General Purchase"								
        '    cboVoucher.AddItem "Debit Note"								
        '    cboVoucher.AddItem "Credit Note"								
        'cboVoucher.AddItem "Sale"								
        '    cboVoucher.AddItem "Customer Debit Note"								
        '    cboVoucher.AddItem "Sale Return"								

        cboVoucher.SelectedIndex = 0



        FormActive = False
        Call frmReversalVoucher_Activated(eventSender, eventArgs)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()
        On Error GoTo ClearErr
        Dim SqlStr As String

        txtPartyName.Text = ""
        lblMKey.Text = ""
        txtVDate.Text = ""
        txtNarration.Text = ""
        LblDrAmt.Text = ""
        LblCrAmt.Text = ""
        LblNetAmt.Text = ""
        txtNarration.Text = ""
        txtVno.Enabled = False
        txtVDate.Enabled = False
        txtFYear.Enabled = False
        cboVoucher.Enabled = False
        CmdSave.Enabled = True

        pProcessKey = MainClass.AutoGenRowNo("TEMP_DSP_DESPATCH", "RowNo", PubDBCn)
        SqlStr = "Delete from FIN_TEMPBILL_TRN  Where UserID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & "" ''BookType='" & Trim(lblBookType.Caption) & "'"								
        PubDBCn.Execute(SqlStr)

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '''Resume								
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ERR1
        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColPRRowNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColDC
            .CellType = SS_CELL_TYPE_EDIT
            '        If FormLoaded = False Then								
            If lblBookType.Text = ConCashReceipt Or lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConPDCReceipt Then
                .Text = "Cr"
            ElseIf lblBookType.Text = ConCashPayment Or lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment Then
                .Text = "Dr"
            End If
            '        End If								
            .set_ColWidth(ColDC, 2.4)

            .Col = ColAccountName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConPDCPayment Or lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConContra Then
                .set_ColWidth(ColAccountName, 21.5)
            Else
                .set_ColWidth(ColAccountName, 25.5)
            End If
            .ColsFrozen = ColAccountName

            .Col = ColParticulars
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .TypeEditMultiLine = True
            '        .TypeEditLen = RsTRNDetail.Fields("PARTICULARS").DefinedSize           ''								
            If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConPDCPayment Or lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConContra Then
                .set_ColWidth(ColParticulars, 15.5)
            Else
                .set_ColWidth(ColParticulars, 21.5)
            End If

            .Col = ColChequeNo
            '        .TypeEditLen = RsTRNDetail.Fields("ChequeNo").DefinedSize           ''								
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .TypeEditMultiLine = False
            .set_ColWidth(ColChequeNo, 7.5)
            If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConPDCPayment Or lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConContra Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If


            .Col = ColChequeDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(ColChequeDate, 7.5)
            If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConPDCPayment Or lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConContra Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColExp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False

            .set_ColWidth(ColExp, 5)

            .Col = ColCC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColCC, 5)

            .Col = ColDept

            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColDept, 4)

            .Col = ColDivisionCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .TypeEditCharSet = FPSpreadADO.TypeEditCharSetConstants.TypeEditCharSetNumeric
            .set_ColWidth(ColDivisionCode, 4)

            .Col = ColEmp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColEmp, 5)

            .Col = ColIBRNo
            '        .TypeEditLen = RsTRNDetail.Fields("IBRNo").DefinedSize           ''								
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")

            .Col = ColSAC
            '        .TypeEditLen = RsTRNDetail.Fields("SAC").DefinedSize           ''								
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False

            .Col = ColCGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")

            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")

            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")

            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")

            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")

            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")

            .Col = ColSaleBillPrefix
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .ColHidden = True

            .Col = ColSaleBillSeq
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .ColHidden = True

            .Col = ColSaleBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .ColHidden = True


            .Col = ColSaleBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .ColHidden = True


            .Col = ColClearDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = 10
            .ColHidden = True

        End With

        MainClass.SetSpreadColor(SprdMain, Arow)
        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColIGSTAmount								
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPRRowNo, ColSaleBillDate)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume								
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        '    txtBankName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)								
        '    txtVType.MaxLength = RsLoanMain.Fields("VTYPE").DefinedSize           ''								
        '								
        '    txtDisbDate.MaxLength = 10           ''								
        '    txtInsRate.MaxLength = RsLoanMain.Fields("INTEREST_RATE").Precision           ''								
        '    txtLoanAmount.MaxLength = RsLoanMain.Fields("LOAN_AMOUNT").Precision           ''								
        '    txtStartDate.MaxLength = 10           ''								
        '    txtChequeFrom.MaxLength = RsLoanMain.Fields("CHEQUE_FROM").DefinedSize           ''								
        '    txtChequeTo.MaxLength = RsLoanMain.Fields("CHEQUE_TO").DefinedSize           ''								
        '    txtLoanPeriod.MaxLength = RsLoanMain.Fields("LOAN_PERIOD").Precision           ''								
        '								
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Function FieldVarification() As Boolean
        On Error GoTo err_Renamed
        Dim cntRow As Integer

        FieldVarification = True


        '    If Trim(cboDivision.Text) = "" Then								
        '        MsgBox "Division Name is Blank", vbInformation								
        '        FieldVarification = False								
        '        If cboDivision.Enabled = True Then cboDivision.SetFocus								
        '        Exit Function								
        '    End If								
        '								
        If Trim(txtNarration.Text) = "" Then
            MsgInformation("Please Enter The Narration")
            txtNarration.Focus()
            FieldVarification = False
            Exit Function
        End If
        '								
        '    If MainClass.ValidateWithMasterTable(txtBankName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = False Then								
        '        txtBankName.SetFocus								
        '        FieldVarification = False								
        '        Exit Function								
        '    End If								
        '								
        '								
        '    If Trim(txtPrincipalName.Text) = "" Then								
        '        MsgInformation "Principal debit Name is empty. Cannot Save"								
        '        txtPrincipalName.SetFocus								
        '        FieldVarification = False								
        '        Exit Function								
        '    End If								
        '								
        '    If MainClass.ValidateWithMasterTable(txtPrincipalName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then								
        '        txtPrincipalName.SetFocus								
        '        FieldVarification = False								
        '        Exit Function								
        '    End If								
        '								
        '    If Trim(txtInterestName.Text) = "" Then								
        '        MsgInformation "Interest Debit Name is empty. Cannot Save"								
        '        txtPrincipalName.SetFocus								
        '        FieldVarification = False								
        '        Exit Function								
        '    End If								
        '								
        '    If MainClass.ValidateWithMasterTable(txtInterestName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then								
        '        txtPrincipalName.SetFocus								
        '        FieldVarification = False								
        '        Exit Function								
        '    End If								
        '								
        '    If txtVType.Text = "" Then								
        '        MsgInformation "Voucher Type is empty. Cannot Save"								
        '        txtVType.SetFocus								
        '        FieldVarification = False								
        '        Exit Function								
        '    End If								
        '								
        '								
        '    With SprdMain								
        '        For cntRow = 1 To .MaxRows								
        '            .Row = cntRow								
        '            .Col = ColChequeNo								
        '            If Trim(.Text) <> "" Then								
        '                If GetChequeStatus(Trim(.Text)) = False Then FieldVarification = False: Exit Function								
        '            End If								
        '        Next								
        '    End With								


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmReversalVoucher_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()

        'RsOpOuts.Close								
    End Sub

    Private Sub txtFYear_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFYear.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVno.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVno.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
