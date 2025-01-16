Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmDrNoteProcess
    Inherits System.Windows.Forms.Form
    '''Private PvtDBCn As ADODB.Connection						

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdProcess.Click
        On Error GoTo ErrorHandler
        Dim mSuppCustCode As String
        Dim mItemCode As String
        Dim mPONo As Double
        Dim mSqlStr As String
        Dim mDivisionCode As Integer
        Dim mLockBookCode As Integer

        '    MsgInformation "This is Under Process for GST, Please call Administrator"						
        '    Exit Sub						

        lblDNCNSeqType.Text = CStr(3)

        If optType(0).Checked = True Then
            mLockBookCode = CInt(ConLockDebitNote)
        Else
            mLockBookCode = CInt(ConLockCreditNote)
        End If

        If ValidateBookLocking(PubDBCn, mLockBookCode, VB6.Format(RunDate, "DD/MM/YYYY")) = True Then
            MsgInformation("Working Book Has Been Locked For The Period " & vbCrLf & "From : " & RunDate & "   To : " & RunDate & vbCrLf & "So Unable to Save or Delete. Contact your system administrator.")
            Exit Sub
        End If


        mSuppCustCode = "-1"
        mItemCode = "-1"

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CInt(Trim(MasterNo))
        End If

        If optCustomer(1).Checked = True Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Please Enter Supplier Name...")
                Exit Sub
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCustCode = MasterNo
            Else
                MsgInformation("No Such Supplier in Account Master")
                Exit Sub
            End If
        End If

        If OptItem(1).Checked = True Then
            If Trim(txtItem.Text) = "" Then
                MsgInformation("Please Enter Item Name...")
                Exit Sub
            End If
            If MainClass.ValidateWithMasterTable((txtItem.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
            Else
                MsgInformation("No Such Item in Item Master")
                Exit Sub
            End If
        End If

        If optRate(1).Checked = True Then
            mPONo = CDbl(Val(txtPONo.Text) & VB6.Format(Val(txtAmendNo.Text), "000"))

            mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PO_STATUS='Y' AND DIV_CODE=" & mDivisionCode & ""

            If mSuppCustCode <> "" Then
                mSqlStr = mSqlStr & " AND SUPP_CUST_CODE='" & mSuppCustCode & "'"
            End If
            If MainClass.ValidateWithMasterTable(mPONo, "MKEY", "MKEY", "PUR_PURCHASE_HDR", PubDBCn, MasterNo,  , mSqlStr) = False Then
                MsgInformation("Either PO is invalid or not Post.")
                Exit Sub
            End If
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If DNCNProcess(mSuppCustCode, mItemCode) = True Then
            MsgInformation("Process Complete...")
            cmdProcess.Enabled = False
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation("Process Not Complete...")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function UpdateMain(ByRef mDebitAccountCode As String, ByRef mCreditAccountCode As String, ByRef mApproved As String, ByRef mDivisionCode As Double, ByRef mBillNo As String, ByRef mBillDate As String, ByRef mPurVNo As String, ByRef mPurVDate As String, ByRef mMRRNO As String, ByRef mMRRDate As String, ByRef mGSTRefund As String, ByRef mBillTo As String) As Boolean
        Dim MainClass_Renamed As Object



        On Error GoTo ErrPart
        Dim SqlStr As String

        Dim mVNoPrefix As String
        Dim mVType As String
        Dim mVNoSuffix As String

        Dim nMkey As String
        Dim mCurRowNo As Integer
        Dim mVNoSeq As Integer


        Dim mVNo As String
        Dim mVDate As String
        Dim mCreditDays1 As Integer
        Dim mCreditDays2 As Integer
        Dim mBookCode As String
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mReason As String
        Dim mItemValue As Double
        Dim mSTPERCENT As Double
        Dim mTOTSTAMT As Double
        Dim mTOTFREIGHT As Double
        Dim mTOTCHARGES As Double
        Dim mEDPERCENT As Double
        Dim mTotEDAmount As Double
        Dim mSURAmount As Double
        Dim mTotDiscount As Double
        Dim mMSC As Double
        Dim mRO As Double
        Dim mTOTEXPAMT As Double
        Dim mTOTTAXABLEAMOUNT As Double
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mCancelled As String
        Dim mNarration As String
        Dim mDnCnType As String
        Dim mModvatNo As String
        Dim mModvatDate As String
        Dim mModvatPer As Double
        Dim mModvatAmount As Double
        Dim mSTRefundNo As String
        Dim mSTRefundDate As String
        Dim mSTRefundPer As Double
        Dim mSTRefundAmount As Double
        Dim mISMODVAT As String
        Dim mISSTREFUND As String
        Dim mISCSTREFUND As String
        Dim mPayDate As String
        Dim mDNFROM As String
        Dim mWithInState As String
        'Dim mPONo As String						
        'Dim mPODate As String						
        Dim mTotCGSTAmount As Double
        Dim mTotSGSTAmount As Double
        Dim mTotIGSTAmount As Double

        mVNoPrefix = ""
        mVNoSuffix = ""

        If optType(0).Checked = True Then
            mVType = "DN"

            mBookCode = CStr(ConDebitNoteBookCode)
            mBookType = VB.Left(ConDebitNote, 1)
            mBookSubType = VB.Right(ConDebitNote, 1)
            mNarration = UCase("Rate Decrease after PO Amend.")
        Else
            mVType = "CN"

            mBookCode = CStr(ConCreditNoteBookCode)
            mBookType = VB.Left(ConCreditNote, 1)
            mBookSubType = VB.Right(ConCreditNote, 1)
            mNarration = UCase("Rate Increase after PO Amend.")
        End If

        mVNoSeq = CInt(AutoGenDNCNNo("VNOSEQ", mBookCode, mVType))
        mCurRowNo = MainClass.AutoGenRowNo("FIN_DNCN_HDR", "RowNo", PubDBCn)
        nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo


        mVNoPrefix = VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00")
        '        mVNo = Trim(mVNoPrefix) & Trim(mVType) & Format(Val(mVNoSeq), "00000") & Trim(mVNoSuffix)						
        mVNo = Trim(mVType) & Trim(Trim(mVNoPrefix) & VB6.Format(Val(CStr(mVNoSeq)), "00000") & Trim(mVNoSuffix))

        mVDate = VB6.Format(RunDate, "DD/MM/YYYY")
        mCreditDays1 = 1
        mCreditDays2 = 1

        mReason = "AMEND. RATE DIFF"
        mCancelled = "N"
        mDnCnType = "A"
        mModvatNo = ""
        mModvatDate = mVDate
        mModvatPer = 100
        mModvatAmount = 0
        mSTRefundNo = ""
        mSTRefundDate = mVDate
        mSTRefundPer = 100
        mSTRefundAmount = 0
        mISMODVAT = "N"
        mISSTREFUND = "N"
        mISCSTREFUND = "N"
        mPayDate = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        mDNFROM = "P"
        mTOTFREIGHT = 0
        mTOTCHARGES = 0
        mEDPERCENT = 0
        mTotEDAmount = 0
        mSURAmount = 0
        mTotDiscount = 0
        mMSC = 0
        mRO = 0

        If MainClass.ValidateWithMasterTable(IIf(optType(0).Checked = True, mDebitAccountCode, mCreditAccountCode), "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mWithInState = MasterNo
        Else
            mWithInState = "N"
        End If

        '        PubDBCn.Errors.Clear						
        '        PubDBCn.BeginTrans						

        If GetDNDetail1(mDebitAccountCode, mCreditAccountCode, mWithInState, mItemValue, mTotQty, mNETVALUE, mSTPERCENT, mTOTSTAMT, mTOTEXPAMT, mTOTTAXABLEAMOUNT, mBillNo, mBillDate) = False Then

            UpdateMain = True
            Exit Function
        End If

        ''						

        '' Sqlstr = Sqlstr & vbCrLf _						
        '& " '" & MainClass.AllowSingleQuote(mPurVNo) & "','" & Format(mPurVDate, "DD-MMM-YYYY") & "'," & vbCrLf _						
        '& " '" & MainClass.AllowSingleQuote(mBillNo) & "','" & Format(mBillDate, "DD-MMM-YYYY") & "'," & vbCrLf _						
        '& " " & Val(mMRRNO) & ",'" & Format(mMRRDATE, "DD-MMM-YYYY") & "'," & vbCrLf _						
        '& " '" & MainClass.AllowSingleQuote(mPONo) & "','" & Format(mPODate, "DD-MMM-YYYY") & "'," & vbCrLf _						
        '						

        SqlStr = "INSERT INTO FIN_DNCN_HDR ( " & vbCrLf _
            & " MKEY, COMPANY_CODE, " & vbCrLf _
            & " FYEAR, ROWNO, " & vbCrLf _
            & " VNOPREFIX, VTYPE, " & vbCrLf _
            & " VNOSEQ, VNOSUFFIX, " & vbCrLf _
            & " VNO, VDATE, " & vbCrLf _
            & " DEBITACCOUNTCODE, CREDITACCOUNTCODE, " & vbCrLf _
            & " DUEDAYSFROM, DUEDAYSTO, " & vbCrLf _
            & " BOOKCODE, BookType, " & vbCrLf _
            & " BOOKSUBTYPE, REMARKS,  " & vbCrLf _
            & " ITEMDESC, REASON, " & vbCrLf _
            & " ITEMVALUE, STPERCENT, " & vbCrLf _
            & " TOTSTAMT, TOTFREIGHT, " & vbCrLf _
            & "  TOTCHARGES, EDPERCENT, "

        SqlStr = SqlStr & vbCrLf _
            & " PURVNO, PURVDATE, " & vbCrLf _
            & " BILLNO, INVOICE_DATE," & vbCrLf _
            & " AUTO_KEY_MRR, MRRDATE, " & vbCrLf _
            & " CUSTREFNO, CUSTREFDATE, "

        SqlStr = SqlStr & vbCrLf _
            & " TOTEDAMOUNT, TOTSURCHARGEAMT, " & vbCrLf _
            & " TOTDISCAMOUNT, TOTMSCAMOUNT, " & vbCrLf _
            & " TOTRO, TOTEXPAMT, " & vbCrLf _
            & " TOTTAXABLEAMOUNT, NETVALUE, " & vbCrLf _
            & " TOTQTY, CANCELLED, " & vbCrLf _
            & " NARRATION, DNCNTYPE, " & vbCrLf _
            & " APPROVED, MODVATNO, " & vbCrLf _
            & " MODVATDATE, MODVATPER, " & vbCrLf _
            & " MODVATAMOUNT, STCLAIMNO, " & vbCrLf _
            & " STCLAIMDATE, STCLAIMPER, " & vbCrLf _
            & " STCLAIMAMOUNT, ISMODVAT, " & vbCrLf _
            & " ISSTREFUND, ISCSTREFUND, PAYDATE,DNCNFROM, " & vbCrLf _
            & " ADDUSER, ADDDATE, " & vbCrLf _
            & " MODUSER, MODDATE,UPDATE_FROM,DIV_CODE, " & vbCrLf _
            & " ISGSTREFUND, DNCNSEQTYPE, ISDNCN_ISSUE,BILL_TO_LOC_ID" & vbCrLf _
            & " ) VALUES ( "

        ''

        SqlStr = SqlStr & vbCrLf _
            & " '" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & ", " & vbCrLf _
            & " " & RsCompany.Fields("FYEAR").Value & ", " & mCurRowNo & ", " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(mVNoPrefix) & "', '" & MainClass.AllowSingleQuote(mVType) & "'," & vbCrLf _
            & " " & mVNoSeq & ", '" & MainClass.AllowSingleQuote(mVNoSuffix) & "', " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(mVNo) & "',TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " '" & mDebitAccountCode & "','" & mCreditAccountCode & "', " & vbCrLf _
            & " " & Val(CStr(mCreditDays1)) & ", " & Val(CStr(mCreditDays2)) & "," & vbCrLf _
            & " '" & mBookCode & "', '" & mBookType & "', " & vbCrLf _
            & " '" & mBookSubType & "', '', " & vbCrLf _
            & " '', '" & MainClass.AllowSingleQuote(mReason) & "', " & vbCrLf _
            & " " & mItemValue & ", " & mSTPERCENT & ", " & vbCrLf _
            & " " & mTOTSTAMT & ", " & mTOTFREIGHT & "," & vbCrLf _
            & " " & mTOTCHARGES & ", " & mEDPERCENT & ", "

        SqlStr = SqlStr & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(mPurVNo) & "',TO_DATE('" & VB6.Format(mPurVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(mBillNo) & "',TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
            & " " & Val(mMRRNO) & ",TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
            & " '','',"

        SqlStr = SqlStr & vbCrLf _
            & " " & mTotEDAmount & ", " & mSURAmount & ", " & vbCrLf _
            & " " & mTotDiscount & "," & mMSC & "," & vbCrLf & " " & mRO & ", " & mTOTEXPAMT & ", " & vbCrLf _
            & " " & mTOTTAXABLEAMOUNT & ", " & mNETVALUE & ", " & vbCrLf _
            & " " & mTotQty & ", '" & mCancelled & "', " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(mNarration) & "', '" & mDnCnType & "'," & vbCrLf _
            & " '" & mApproved & "', " & Val(mModvatNo) & ", " & vbCrLf _
            & " TO_DATE('" & VB6.Format(mModvatDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mModvatPer & ", " & vbCrLf _
            & " " & mModvatAmount & ", " & Val(mSTRefundNo) & ", " & vbCrLf _
            & " TO_DATE('" & VB6.Format(mSTRefundDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mSTRefundPer & ", " & vbCrLf _
            & " " & mSTRefundAmount & ", '" & mISMODVAT & "', " & vbCrLf _
            & " '" & mISSTREFUND & "', '" & mISCSTREFUND & "', TO_DATE('" & VB6.Format(mPayDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mDNFROM & "', " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " '','','Y'," & mDivisionCode & ",'" & mGSTRefund & "'," & Val(lblDNCNSeqType.Text) & ", 'N', '" & MainClass.AllowSingleQuote(mBillTo) & "')"
        ''" & mDNCNIssue & "'," & Val(lblDNCNSeqType.Text) & ",'" & MainClass.AllowSingleQuote(mBillTo) & "'

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(nMkey, mDebitAccountCode, mCreditAccountCode, mItemValue, mTotEDAmount, mTOTSTAMT, mTotCGSTAmount, mTotSGSTAmount, mTotIGSTAmount, mTOTTAXABLEAMOUNT, mBillNo, mBillDate, mGSTRefund) = False Then GoTo ErrPart

        '    If mWithInState = "N" Then						
        If UpdateDNCNExp1(nMkey, mSTPERCENT, mTOTSTAMT, mTotCGSTAmount, mTotSGSTAmount, mTotIGSTAmount, mDebitAccountCode, mCreditAccountCode, mBillNo, mBillDate) = False Then GoTo ErrPart
        '    End If						

        mNETVALUE = mTOTTAXABLEAMOUNT + mTotCGSTAmount + mTotSGSTAmount + mTotIGSTAmount

        SqlStr = "UPDATE FIN_DNCN_HDR SET " & vbCrLf _
            & " NETCGST_AMOUNT=" & mTotCGSTAmount & ", " & vbCrLf _
            & " NETSGST_AMOUNT=" & mTotSGSTAmount & ", " & vbCrLf _
            & " NETIGST_AMOUNT=" & mTotIGSTAmount & ", " & vbCrLf _
            & " CGST_REFUNDAMOUNT=" & mTotCGSTAmount & "," & vbCrLf _
            & " SGST_REFUNDAMOUNT=" & mTotSGSTAmount & "," & vbCrLf _
            & " IGST_REFUNDAMOUNT=" & mTotIGSTAmount & "," & vbCrLf _
            & " TOTTAXABLEAMOUNT=" & mTOTTAXABLEAMOUNT & "," & vbCrLf _
            & " NETVALUE=" & mNETVALUE & "" & vbCrLf _
            & " WHERE MKEY=" & nMkey & ""

        PubDBCn.Execute(SqlStr)

        If mApproved = "Y" Then
            If DNCNPostTRNGST(PubDBCn, nMkey, mCurRowNo, mBookCode, mBookType, mBookSubType, Trim(mVType), mVNo, mVDate, mBillNo, mBillDate, mDebitAccountCode, mCreditAccountCode, Val(CStr(mNETVALUE)), IIf(mCancelled = "Y", True, False), mPayDate, "", mNarration, Val(CStr(mTOTEXPAMT)), True, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivisionCode, IIf(mGSTRefund = "G", "Y", "N"), mTotCGSTAmount, mTotSGSTAmount, mTotIGSTAmount, "A", mBillTo) = False Then GoTo ErrPart
        End If

        '    PubDBCn.CommitTrans						
        UpdateMain = True
        Exit Function
ErrPart:
        '    Resume						
        UpdateMain = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    PubDBCn.RollbackTrans						
        ''Resume						
    End Function
    Private Function AutoGenDNCNNo(ByRef mFieldName As String, ByRef pBookCode As String, ByRef pVType As String) As String
        Dim MainClass_Renamed As Object
        On Error GoTo AutoGenNoErr
        Dim RsGen As ADODB.Recordset
        Dim mNewDNCNNo As Integer
        Dim SqlStr As String
        Dim mStartingNo As Integer
        Dim xFyear As Integer

        SqlStr = ""

        xFyear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))

        mStartingNo = 1
        If RsCompany.Fields("FYEAR").Value >= 2020 Then
            mStartingNo = CInt(xFyear & Val(lblDNCNSeqType.Text) & VB6.Format(mStartingNo, "00000"))
        End If
        '
        SqlStr = "SELECT Max(" & mFieldName & ")  FROM FIN_DNCN_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BookCode='" & Val(pBookCode) & "' AND VType='" & pVType & "'"

        If RsCompany.Fields("FYEAR").Value >= 2020 Then
            SqlStr = SqlStr & vbCrLf & " AND DNCNSEQTYPE=" & Val(lblDNCNSeqType.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsGen
            If .EOF = False Then
                If .Fields(0).Value = -1 Then
                    mNewDNCNNo = mStartingNo
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewDNCNNo = .Fields(0).Value + 1
                Else
                    mNewDNCNNo = mStartingNo
                End If
            Else
                mNewDNCNNo = mStartingNo
            End If
        End With
        AutoGenDNCNNo = CStr(mNewDNCNNo)
        Exit Function
AutoGenNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateDNCNExp1(ByRef xKey As String, ByRef mSTPERCENT As Double, ByRef mTOTSTAMT As Double, ByRef mTotCGSTAmount As Double, ByRef mTotSGSTAmount As Double, ByRef mTotIGSTAmount As Double, ByRef pDebitAccountCode As String, ByRef pCreditAccountCode As String, ByRef mBillNo As String, ByRef mBillDate As String) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim RsMisc As ADODB.Recordset
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim SqlStr As String
        Dim mIDENT As String

        PubDBCn.Execute("Delete From FIN_DNCN_EXP Where Mkey='" & xKey & "'")

        I = 0
        SqlStr = " SELECT DISTINCT EXPCODE,CALCON " & vbCrLf _
            & " FROM FIN_PURCHASE_EXP " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " MKEY IN "

        SqlStr = SqlStr & vbCrLf _
            & "( SELECT MKEY " & vbCrLf _
            & " FROM TEMP_DNCN_TRN " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " AND BILLNO='" & Trim(mBillNo) & "'" & vbCrLf _
            & " AND INVOICE_DATE=TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" ''& vbCrLf |            & " AND PORATE>0"						

        If optType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND ACCOUNTCODE_DR='" & pDebitAccountCode & "'" & vbCrLf _
                & " AND ACCOUNTCODE_CR='" & pCreditAccountCode & "'"
        Else
            SqlStr = SqlStr & vbCrLf _
                & " AND ACCOUNTCODE_DR='" & pCreditAccountCode & "'" & vbCrLf _
                & " AND ACCOUNTCODE_CR='" & pDebitAccountCode & "'"
        End If
        SqlStr = SqlStr & vbCrLf & ")"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)

        If RsMisc.EOF = False Then
            Do While RsMisc.EOF = False

                mExpCode = IIf(IsDBNull(RsMisc.Fields("EXPCODE").Value), -1, RsMisc.Fields("EXPCODE").Value)
                mPercent = mSTPERCENT
                mExpAmount = mTOTSTAMT
                mCalcOn = IIf(IsDBNull(RsMisc.Fields("CALCON").Value), 0, RsMisc.Fields("CALCON").Value)
                mRO = "N"

                SqlStr = ""

                If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "IDENTIFICATION", "FIN_INTERFACE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mIDENT = MasterNo
                Else
                    mIDENT = ""
                End If

                If mIDENT = "ST" Then
                    I = I + 1
                    SqlStr = "Insert Into  FIN_DNCN_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & xKey & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mExpAmount & ",'" & mRO & "')"
                    PubDBCn.Execute(SqlStr)
                ElseIf mIDENT = "CGS" Then
                    mPercent = 0
                    mExpAmount = mTotCGSTAmount
                    I = I + 1
                    SqlStr = "Insert Into  FIN_DNCN_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & xKey & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mExpAmount & ",'" & mRO & "')"
                    PubDBCn.Execute(SqlStr)
                ElseIf mIDENT = "SGS" Then
                    mPercent = 0
                    mExpAmount = mTotSGSTAmount
                    I = I + 1
                    SqlStr = "Insert Into  FIN_DNCN_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & xKey & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mExpAmount & ",'" & mRO & "')"
                    PubDBCn.Execute(SqlStr)
                ElseIf mIDENT = "IGS" Then
                    mPercent = 0
                    mExpAmount = mTotIGSTAmount
                    I = I + 1
                    SqlStr = "Insert Into  FIN_DNCN_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & xKey & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mExpAmount & ",'" & mRO & "')"
                    PubDBCn.Execute(SqlStr)
                End If
                RsMisc.MoveNext()
            Loop
        End If
        UpdateDNCNExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDNCNExp1 = False
    End Function
    Private Function UpdateDetail1(ByRef xKey As String, ByRef pDebitAccountCode As String, ByRef pCreditAccountCode As String, ByRef mItemValue As Double, ByRef mTotEDAmount As Double, ByRef mTOTSTAMT As Double, ByRef mTotCGSTAmount As Double, ByRef mTotSGSTAmount As Double, ByRef mTotIGSTAmount As Double, ByRef mTotTaxableValue As Double, ByRef mBillNo As String, ByRef mBillDate As String, ByRef mGSTRefund As String) As Boolean
        Dim MainClass_Renamed As Object

        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mPORate As Double
        Dim mAmount As Double
        Dim mPONo As String

        Dim mMRRNO As Double
        Dim mMRRDate As String
        'Dim mBillNo As String						
        'Dim mBillDate As String						

        Dim mPurMkey As String
        Dim mPurVNo As String
        Dim mPurVDate As String
        Dim mRefType As String

        Dim RsTemp As ADODB.Recordset
        Dim mSqlStr As String

        Dim pItemEDAmount As Double
        Dim pItemSTAmount As Double

        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double

        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mHSNCode As String
        Dim mNetRate As Double

        mTotCGSTAmount = 0
        mTotSGSTAmount = 0
        mTotIGSTAmount = 0
        mTotTaxableValue = 0

        PubDBCn.Execute("Delete From FIN_DNCN_DET Where Mkey='" & xKey & "'")

        I = 0
        ''AND MKEY='" & pPurMkey & "'						

        SqlStr = " SELECT * " & vbCrLf _
            & " FROM TEMP_DNCN_TRN " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " AND BILLNO='" & Trim(mBillNo) & "'" & vbCrLf _
            & " AND INVOICE_DATE=TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" ''''& vbCrLf |            & " AND PORATE>0"						

        '' MKS ADD PORATE>0 Condition as on date 14-04-2006.....						

        If chkAgtD3.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND PORATE>0"
        Else
            SqlStr = SqlStr & vbCrLf & " AND CASE WHEN SUBSTR(CUST_REF_NO,1,1)='S' THEN 1 ELSE PORATE END>0"
        End If

        If optType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE_DR='" & pDebitAccountCode & "'" & vbCrLf & " AND ACCOUNTCODE_CR='" & pCreditAccountCode & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE_DR='" & pCreditAccountCode & "'" & vbCrLf & " AND ACCOUNTCODE_CR='" & pDebitAccountCode & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY INVOICE_DATE,BILLNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)

        If RsMisc.EOF = False Then
            Do While RsMisc.EOF = False
                I = I + 1
                mItemCode = Trim(IIf(IsDBNull(RsMisc.Fields("ITEM_CODE").Value), "", RsMisc.Fields("ITEM_CODE").Value))

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If


                '            mItemDesc = IIf(IsNull(RsMisc!ITEM_DESC), "", RsMisc!ITEM_DESC)						
                mUnit = IIf(IsDBNull(RsMisc.Fields("ITEM_UOM").Value), "", RsMisc.Fields("ITEM_UOM").Value)

                mQty = IIf(IsDBNull(RsMisc.Fields("ACCPETED").Value), 0, RsMisc.Fields("ACCPETED").Value)

                If optType(0).Checked = True Then ''For Debit Note Only						
                    mRate = System.Math.Abs(IIf(IsDBNull(RsMisc.Fields("ITEM_RATE").Value), 0, RsMisc.Fields("ITEM_RATE").Value))
                    mRate = mRate - IIf(IsDBNull(RsMisc.Fields("DNCN_RATE").Value), 0, RsMisc.Fields("DNCN_RATE").Value)
                    mRate = mRate + IIf(IsDBNull(RsMisc.Fields("SUPP_RATE").Value), 0, RsMisc.Fields("SUPP_RATE").Value)
                    mRate = mRate - IIf(IsDBNull(RsMisc.Fields("PORATE").Value), 0, RsMisc.Fields("PORATE").Value)
                    mRate = System.Math.Abs(mRate)
                Else ''For Credit Note Only Agt Debit Note						
                    mNetRate = mNetRate - IIf(IsDBNull(RsMisc.Fields("DNCN_RATE").Value), 0, RsMisc.Fields("DNCN_RATE").Value)
                    mNetRate = mNetRate + IIf(IsDBNull(RsMisc.Fields("SUPP_RATE").Value), 0, RsMisc.Fields("SUPP_RATE").Value)
                    mNetRate = mNetRate - IIf(IsDBNull(RsMisc.Fields("PORATE").Value), 0, RsMisc.Fields("PORATE").Value)
                    mNetRate = System.Math.Abs(mNetRate)
                    mRate = IIf(IsDBNull(RsMisc.Fields("DNCN_RATE").Value), 0, RsMisc.Fields("DNCN_RATE").Value)

                    If mNetRate < mRate Then
                        mRate = mNetRate
                    End If
                End If
                '            If optType(1).Value = True Then  '"Commited by sandeep 14/06/2016						
                '                If IIf(IsNull(RsMisc!DNCN_RATE), 0, RsMisc!DNCN_RATE) < mRate Then						
                '                    mRate = Abs(IIf(IsNull(RsMisc!DNCN_RATE), 0, RsMisc!DNCN_RATE))						
                '                End If						
                '            End If						

                mPORate = IIf(IsDBNull(RsMisc.Fields("PORATE").Value), 0, RsMisc.Fields("PORATE").Value)


                mPurMkey = IIf(IsDBNull(RsMisc.Fields("mKey").Value), "-1", RsMisc.Fields("mKey").Value)
                mPurVNo = IIf(IsDBNull(RsMisc.Fields("VNO").Value), "-1", RsMisc.Fields("VNO").Value)
                mPurVDate = IIf(IsDBNull(RsMisc.Fields("VDATE").Value), "", RsMisc.Fields("VDATE").Value)

                mMRRNO = IIf(IsDBNull(RsMisc.Fields("AUTO_KEY_MRR").Value), -1, RsMisc.Fields("AUTO_KEY_MRR").Value)
                mMRRDate = IIf(IsDBNull(RsMisc.Fields("MRRDATE").Value), "", RsMisc.Fields("MRRDATE").Value)

                mRefType = GetMrrRefNo(mMRRNO)

                mBillNo = IIf(IsDBNull(RsMisc.Fields("BILLNO").Value), "", RsMisc.Fields("BILLNO").Value)
                mBillDate = IIf(IsDBNull(RsMisc.Fields("INVOICE_DATE").Value), "", RsMisc.Fields("INVOICE_DATE").Value)

                mPONo = IIf(IsDBNull(RsMisc.Fields("CUST_REF_NO").Value), "", RsMisc.Fields("CUST_REF_NO").Value)

                mAmount = mQty * mRate
                mAmount = CDbl(VB6.Format(mAmount, "0.00"))

                If Val(CStr(mItemValue)) = 0 Then
                    pItemEDAmount = 0
                    pItemSTAmount = 0
                Else
                    pItemEDAmount = Val(CStr(mTotEDAmount)) * mAmount / Val(CStr(mItemValue))
                    pItemSTAmount = Val(CStr(mTOTSTAMT)) * mAmount / Val(CStr(mItemValue))
                End If

                '            If mGSTRefund = "G" Or mGSTRefund = "I" Then						
                '                mCGSTPer = Format(IIf(IsNull(RsMisc!CGST_PER), 0, RsMisc!CGST_PER), "0.00")						
                '                mSGSTPer = Format(IIf(IsNull(RsMisc!SGST_PER), 0, RsMisc!SGST_PER), "0.00")						
                '                mIGSTPer = Format(IIf(IsNull(RsMisc!IGST_PER), 0, RsMisc!IGST_PER), "0.00")						
                '            Else						
                mCGSTPer = 0
                mSGSTPer = 0
                mIGSTPer = 0
                '            End If						

                mCGSTAmount = CDbl(VB6.Format(mAmount * mCGSTPer * 0.01, "0.00"))
                mSGSTAmount = CDbl(VB6.Format(mAmount * mSGSTPer * 0.01, "0.00"))
                mIGSTAmount = CDbl(VB6.Format(mAmount * mIGSTPer * 0.01, "0.00"))

                mTotCGSTAmount = mTotCGSTAmount + mCGSTAmount
                mTotSGSTAmount = mTotSGSTAmount + mSGSTAmount
                mTotIGSTAmount = mTotIGSTAmount + mIGSTAmount
                mTotTaxableValue = mTotTaxableValue + mAmount

                mHSNCode = IIf(IsDBNull(RsMisc.Fields("HSNCODE").Value), "", RsMisc.Fields("HSNCODE").Value)

                SqlStr = ""


                If mItemCode <> "" And mAmount <> 0 Then
                    SqlStr = " INSERT INTO FIN_DNCN_DET ( " & vbCrLf _
                        & " MKEY , SUBROWNO, " & vbCrLf _
                        & " ITEM_CODE , ITEM_DESC, ITEM_QTY, " & vbCrLf _
                        & " ITEM_UOM , ITEM_RATE, ITEM_AMT," & vbCrLf _
                        & " MRR_REF_NO,MRR_REF_DATE,SUPP_REF_NO," & vbCrLf _
                        & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf _
                        & " PURMKEY, " & vbCrLf _
                        & " PURVNO, PURVDATE, " & vbCrLf _
                        & " DNCN_REF_NO, DNCN_REF_DATE," & vbCrLf _
                        & " PO_RATE, MRR_REF_TYPE, ITEM_ED, ITEM_ST, " & vbCrLf _
                        & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
                        & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, " & vbCrLf _
                        & " HSNCODE" & vbCrLf _
                        & " ) "


                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ('" & xKey & "'," & I & ", " & vbCrLf _
                        & " '" & mItemCode & "','" & MainClass.AllowSingleQuote(mItemDesc) & "'," & mQty & ", " & vbCrLf _
                        & " '" & mUnit & "'," & mRate & "," & mAmount & "," & vbCrLf _
                        & " " & Val(CStr(mMRRNO)) & ",TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mBillNo) & "', " & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " '" & mPONo & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '" & mPurMkey & "'," & vbCrLf _
                        & " '" & mPurVNo & "',TO_DATE('" & VB6.Format(mPurVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " '" & mPurVNo & "',TO_DATE('" & VB6.Format(mPurVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " " & mPORate & ", '" & mRefType & "', " & pItemEDAmount & ", " & pItemSTAmount & "," & vbCrLf _
                        & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf _
                        & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & "," & vbCrLf _
                        & " '" & mHSNCode & "'" & vbCrLf _
                        & " ) "

                    PubDBCn.Execute(SqlStr)
                End If

                RsMisc.MoveNext()
            Loop
        End If

        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Function

    Private Function GetDNDetail1(ByRef pDebitAccountCode As String, ByRef pCreditAccountCode As String, ByRef mWithInState As String, ByRef mItemValue As Double, ByRef mTotQty As Double, ByRef mNETVALUE As Double, ByRef mSTPERCENT As Double, ByRef mTOTSTAMT As Double, ByRef mTOTEXPAMT As Double, ByRef mTOTTAXABLEAMOUNT As Double, ByRef mBillNo As String, ByRef mBillDate As String) As Boolean
        Dim MainClass_Renamed As Object

        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim mIDENT As String
        Dim mNetRate As Double
        mItemValue = 0
        mTotQty = 0
        mSTPERCENT = 0
        mTOTSTAMT = 0
        mTOTEXPAMT = 0
        mTOTTAXABLEAMOUNT = 0
        mNETVALUE = 0
        ''MKEY='" & pPurMkey & "'						

        SqlStr = " SELECT ITEM_CODE, " & vbCrLf _
            & " ACCPETED, ITEM_RATE, " & vbCrLf _
            & " PORATE, DNCN_RATE, SUPP_RATE " & vbCrLf _
            & " FROM TEMP_DNCN_TRN " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " AND BILLNO='" & Trim(mBillNo) & "'" & vbCrLf _
            & " AND INVOICE_DATE=TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" '''& vbCrLf |            & " AND PORATE>0"						


        If chkAgtD3.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND PORATE>0"
        Else
            SqlStr = SqlStr & vbCrLf & " AND CASE WHEN SUBSTR(CUST_REF_NO,1,1)='S' THEN 1 ELSE PORATE END>0"
        End If

        If optType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE_DR='" & pDebitAccountCode & "'" & vbCrLf & " AND ACCOUNTCODE_CR='" & pCreditAccountCode & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE_DR='" & pCreditAccountCode & "'" & vbCrLf & " AND ACCOUNTCODE_CR='" & pDebitAccountCode & "'"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)

        GetDNDetail1 = False
        If RsMisc.EOF = False Then
            Do While RsMisc.EOF = False
                mItemCode = Trim(IIf(IsDBNull(RsMisc.Fields("ITEM_CODE").Value), "", RsMisc.Fields("ITEM_CODE").Value))
                mQty = IIf(IsDBNull(RsMisc.Fields("ACCPETED").Value), 0, RsMisc.Fields("ACCPETED").Value)
                '            mRate = Abs(IIf(IsNull(RsMisc!ITEM_RATE), 0, RsMisc!ITEM_RATE))						
                '            mRate = mRate - IIf(IsNull(RsMisc!DNCN_RATE), 0, RsMisc!DNCN_RATE)						
                '            mRate = mRate + IIf(IsNull(RsMisc!SUPP_RATE), 0, RsMisc!SUPP_RATE)						
                '						
                '            mRate = Abs(mRate - IIf(IsNull(RsMisc!PORATE), 0, RsMisc!PORATE))						

                If optType(0).Checked = True Then ''For Debit Note Only						
                    mRate = System.Math.Abs(IIf(IsDBNull(RsMisc.Fields("ITEM_RATE").Value), 0, RsMisc.Fields("ITEM_RATE").Value))
                    mRate = mRate - IIf(IsDBNull(RsMisc.Fields("DNCN_RATE").Value), 0, RsMisc.Fields("DNCN_RATE").Value)
                    mRate = mRate + IIf(IsDBNull(RsMisc.Fields("SUPP_RATE").Value), 0, RsMisc.Fields("SUPP_RATE").Value)
                    mRate = mRate - IIf(IsDBNull(RsMisc.Fields("PORATE").Value), 0, RsMisc.Fields("PORATE").Value)
                    mRate = System.Math.Abs(mRate)
                Else ''For Credit Note Only Agt Debit Note						



                    mNetRate = System.Math.Abs(IIf(IsDBNull(RsMisc.Fields("ITEM_RATE").Value), 0, RsMisc.Fields("ITEM_RATE").Value))
                    mNetRate = mNetRate - IIf(IsDBNull(RsMisc.Fields("DNCN_RATE").Value), 0, RsMisc.Fields("DNCN_RATE").Value)
                    mNetRate = mNetRate + IIf(IsDBNull(RsMisc.Fields("SUPP_RATE").Value), 0, RsMisc.Fields("SUPP_RATE").Value)
                    mNetRate = mNetRate - IIf(IsDBNull(RsMisc.Fields("PORATE").Value), 0, RsMisc.Fields("PORATE").Value)
                    mNetRate = System.Math.Abs(mNetRate)
                    mRate = IIf(IsDBNull(RsMisc.Fields("DNCN_RATE").Value), 0, RsMisc.Fields("DNCN_RATE").Value)

                    If mNetRate < mRate Then
                        mRate = mNetRate
                    End If
                End If


                mAmount = mQty * mRate
                mAmount = CDbl(VB6.Format(mAmount, "0.00"))

                If mItemCode <> "" And mAmount <> 0 Then
                    mItemValue = mItemValue + mAmount
                    mTotQty = mTotQty + mQty
                    GetDNDetail1 = True
                End If

                RsMisc.MoveNext()
            Loop
        End If

        If GetDNDetail1 = False Then
            Exit Function
        End If

        mTOTTAXABLEAMOUNT = mItemValue

        If mWithInState = "Y" Then
            mSTPERCENT = 0
            mTOTSTAMT = 0
        Else
            SqlStr = " SELECT DISTINCT EXPCODE " & vbCrLf & " FROM FIN_PURCHASE_EXP " & vbCrLf & " WHERE " & vbCrLf & " MKEY IN "

            SqlStr = SqlStr & vbCrLf & "( SELECT MKEY " & vbCrLf _
                & " FROM TEMP_DNCN_TRN " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
                & " AND BILLNO='" & Trim(mBillNo) & "' AND INVOICE_DATE=TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND PORATE>0"

            If optType(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE_DR='" & pDebitAccountCode & "'" & vbCrLf & " AND ACCOUNTCODE_CR='" & pCreditAccountCode & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE_DR='" & pCreditAccountCode & "'" & vbCrLf & " AND ACCOUNTCODE_CR='" & pDebitAccountCode & "'"
            End If
            SqlStr = SqlStr & vbCrLf & ")"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)

            If RsMisc.EOF = False Then
                Do While RsMisc.EOF = False

                    mExpCode = IIf(IsDBNull(RsMisc.Fields("EXPCODE").Value), -1, RsMisc.Fields("EXPCODE").Value)
                    mPercent = CDbl("4")
                    '                mPercent = IIf(IsNull(RsMisc!EXPPERCENT), 0, RsMisc!EXPPERCENT)						
                    '                mExpAmount = IIf(IsNull(RsMisc!Amount), 0, RsMisc!Amount)						

                    If mExpAmount <> 0 Then
                        If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "IDENTIFICATION", "FIN_INTERFACE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mIDENT = MasterNo
                        Else
                            mIDENT = ""
                        End If

                        If mIDENT = "ST" Then
                            mSTPERCENT = mPercent
                            mTOTSTAMT = mItemValue * mPercent * 0.01
                        End If
                    End If
                    RsMisc.MoveNext()
                Loop
            End If
        End If
        mTOTEXPAMT = mTOTSTAMT
        mNETVALUE = mItemValue + mTOTEXPAMT

        GetDNDetail1 = True
        Exit Function
UpdateDetail1:
        GetDNDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Function
    Private Function DNCNProcess(ByRef pSuppCustCode As String, ByRef pItemCode As String) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset
        Dim mSqlStr As String
        Dim SqlStr As String
        Dim mPurVNo As String
        Dim mPurVDate As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mMRRNO As String
        Dim mMRRDate As String
        Dim mPONo As String
        Dim mPODate As String
        Dim mDebitAccountCode As String
        Dim mCreditAccountCode As String
        Dim mApproved As String
        Dim mVMkey As String
        Dim mPOMkey As Double
        Dim mDivisionCode As Double
        Dim mGSTRefund As String
        Dim xSuppCode As String
        Dim mCompanyGSTNo As String
        Dim mPartyGSTNo As String
        Dim mBillTo As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If InsertIntoTemp(pSuppCustCode, pItemCode, mDivisionCode) = False Then GoTo ErrPart

        SqlStr = MakeSQL(pSuppCustCode, pItemCode, mDivisionCode)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            If MsgQuestion("Are to want to Approved " & IIf(optType(0).Checked = True, "Debit", "Credit") & " Note ...") = CStr(MsgBoxResult.No) Then
                mApproved = "N"
            Else
                mApproved = "Y"
            End If
            If MsgQuestion("Are you want to start Process ...") = CStr(MsgBoxResult.No) Then
                PubDBCn.RollbackTrans()
                DNCNProcess = True
                Exit Function
            End If


            Do While RsTemp.EOF = False


                mPurVNo = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                mPurVDate = IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value)
                mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                mBillDate = IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value)
                mMRRNO = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_MRR").Value), "", RsTemp.Fields("AUTO_KEY_MRR").Value)
                mMRRDate = IIf(IsDBNull(RsTemp.Fields("MRRDATE").Value), "", RsTemp.Fields("MRRDATE").Value)
                '            mPONo = IIf(IsNull(RsTemp!AUTO_KEY_MRR), "", RsTemp!AUTO_KEY_MRR)						
                '            mPODate = IIf(IsNull(RsTemp!MRRDATE), "", RsTemp!MRRDATE)						
                '            mVMkey = IIf(IsNull(RsTemp!mKey), "", RsTemp!mKey)						


                'xSuppCode, "SUPP_CUST_CODE"

                xSuppCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNTCODE_DR").Value), "", RsTemp.Fields("ACCOUNTCODE_DR").Value)

                mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
                mPartyGSTNo = ""
                If MainClass.ValidateWithMasterTable(xSuppCode, "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPartyGSTNo = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mMRRNO, "AUTO_KEY_MRR", "BILL_TO_LOC_ID", "INV_GATE_HDR", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mBillTo = MasterNo
                End If


                If optType(0).Checked = True Then
                    mDebitAccountCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNTCODE_DR").Value), "", RsTemp.Fields("ACCOUNTCODE_DR").Value)
                    mCreditAccountCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNTCODE_CR").Value), "", RsTemp.Fields("ACCOUNTCODE_CR").Value)
                Else
                    mDebitAccountCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNTCODE_CR").Value), "", RsTemp.Fields("ACCOUNTCODE_CR").Value)
                    mCreditAccountCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNTCODE_DR").Value), "", RsTemp.Fields("ACCOUNTCODE_DR").Value)
                End If

                If optType(0).Checked = True Then
                    mGSTRefund = "W"
                Else
                    mGSTRefund = "W"
                    ''Comment on 12/03/2019						
                    '                mGSTRefund = IIf(IsNull(RsTemp!ISGSTREFUND), "W", RsTemp!ISGSTREFUND)						
                    '                mGSTRefund = IIf(mCompanyGSTNo = mPartyGSTNo, "W", IIf(mGSTRefund = "G" Or mGSTRefund = "I", mGSTRefund, "W"))						
                End If


                If UpdateMain(mDebitAccountCode, mCreditAccountCode, mApproved, mDivisionCode, mBillNo, mBillDate, mPurVNo, mPurVDate, mMRRNO, mMRRDate, mGSTRefund, mBillTo) = False Then GoTo ErrPart




                RsTemp.MoveNext()
            Loop
        Else
            MsgInformation("Nothing For Process Debit/Credit Note")
        End If

        PubDBCn.CommitTrans()
        DNCNProcess = True
        Exit Function
ErrPart:
        DNCNProcess = False
        PubDBCn.RollbackTrans()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function InsertIntoTemp(ByRef pSuppCustCode As String, ByRef pItemCode As String, ByRef mDivisionCode As Double) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSqlStr As String

        InsertIntoTemp = False

        '    PubDBCn.Errors.Clear						
        '    PubDBCn.BeginTrans						

        SqlStr = "DELETE FROM TEMP_DNCN_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
                    PubDBCn.Execute(SqlStr)

        mSqlStr = " INSERT INTO TEMP_DNCN_TRN ( " & vbCrLf & " USERID, MKEY, COMPANY_CODE, " & vbCrLf & " FYEAR, ACCOUNTCODE_DR, ACCOUNTCODE_CR, " & vbCrLf & " VNO, VDATE, BILLNO, " & vbCrLf & " INVOICE_DATE, AUTO_KEY_MRR, MRRDATE, " & vbCrLf & " CUST_REF_NO, ITEM_CODE, ITEM_UOM, ITEM_QTY, " & vbCrLf & " ACCPETED, ITEM_RATE, DNCN_RATE, " & vbCrLf & " SUPP_RATE, PORATE,DIV_CODE,SERIAL_NO, " & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, HSNCODE,ISGSTREFUND " & vbCrLf & " ) "


        SqlStr = ""

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " IH.MKEY, IH.COMPANY_CODE, IH.FYEAR, " & vbCrLf & " IH.SUPP_CUST_CODE, IH.ACCOUNTCODE, " & vbCrLf & " IH.VNO, IH.VDATE, IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf & " IH.AUTO_KEY_MRR, IH.MRRDATE," & vbCrLf & " NVL(ID.CUST_REF_NO,'-1'), ID.ITEM_CODE, ID.ITEM_UOM, "

        SqlStr = SqlStr & vbCrLf & " ID.ITEM_QTY, " & vbCrLf & " TO_CHAR(NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0)) AS ACCPETED, " & vbCrLf & " ID.ITEM_RATE, 0,0,0,IH.DIV_CODE,ID.SUBROWNO, " & vbCrLf & " ID.CGST_PER, ID.SGST_PER, ID.IGST_PER, " & vbCrLf & " 0, 0, 0, ID.HSNCODE, IH.ISGSTAPPLICABLE "

        ''''FROM CLAUSE...						
        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, INV_GATE_HDR GH "

        ''''WHERE CLAUSE...''IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "						

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY"

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=GH.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=GH.SUPP_CUST_CODE" & vbCrLf & " AND IH.AUTO_KEY_MRR=GH.AUTO_KEY_MRR"

        If optAgt(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE='P'"
        ElseIf optAgt(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE='I'"
        ElseIf optAgt(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE='R'"
        End If

        '    Sqlstr = Sqlstr & vbCrLf & " AND (CUST_REF_NO IS NOT NULL OR CUST_REF_NO<>'')"						

        SqlStr = SqlStr & vbCrLf & "AND IH.VNO<>'-1' AND IH.ISFINALPOST='Y' AND CANCELLED='N'"

        If pSuppCustCode <> "-1" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSuppCustCode)) & "'"
        End If

        If pItemCode <> "-1" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(pItemCode)) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & mDivisionCode & ""

        If optRate(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ID.CUST_REF_NO='" & Val(txtPONo.Text) & "'"
        End If

        If optBaseOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(TxtDtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(TxtDtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        ElseIf optBaseOn(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(TxtDtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(TxtDtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.MRRDATE>=TO_DATE('" & VB6.Format(TxtDtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.MRRDATE<=TO_DATE('" & VB6.Format(TxtDtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        '    Sqlstr = Sqlstr & vbCrLf & " ORDER BY IH.SUPP_CUST_CODE,IH.AUTO_KEY_MRR, IH.VNO, IH.VDATE "						

        SqlStr = mSqlStr & vbCrLf & SqlStr

        PubDBCn.Execute(SqlStr)
        '    PubDBCn.CommitTrans						

        '    PubDBCn.Errors.Clear						
        '    PubDBCn.BeginTrans						

        SqlStr = " UPDATE TEMP_DNCN_TRN SET " & vbCrLf & " ACCPETED=ACCPETED+GETREOFFERQTY(COMPANY_CODE,AUTO_KEY_MRR,MRRDATE,ACCOUNTCODE_DR,ITEM_CODE)- GETLINEREJECTIONQTY(COMPANY_CODE,AUTO_KEY_MRR,MRRDATE,ACCOUNTCODE_DR,ITEM_CODE), " & vbCrLf & " DNCN_RATE=NVL(GETDNCNRATE(COMPANY_CODE, FYEAR, ACCOUNTCODE_DR, BILLNO, INVOICE_DATE, ITEM_CODE,'R',CUST_REF_NO),0), " & vbCrLf & " SUPP_RATE=NVL(GETSUPPRATE(COMPANY_CODE, FYEAR, MKEY, ACCOUNTCODE_DR, VNO, VDATE, ITEM_CODE,'R'),0), "

        'Less Line rejection...						
        '    SqlStr = SqlStr & vbCrLf & " ACCPETED=ACCPETED - GETLINEREJECTIONQTY(COMPANY_CODE,AUTO_KEY_MRR,MRRDATE,ACCOUNTCODE_DR,ITEM_CODE), "						

        If optAgt(0).Checked = True Then
            If optBaseOn(2).Checked = True Then
                If optRate(0).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMPRICE_NEW(FYEAR, " & RsCompany.Fields("FYEAR").Value & ", MRRDATE,CUST_REF_NO,ITEM_CODE)"
                Else
                    SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMPORATE(CUST_REF_NO," & Val(txtAmendNo.Text) & ",ITEM_CODE)"
                End If
            Else
                If optRate(0).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMPRICE_NEW(FYEAR, " & RsCompany.Fields("FYEAR").Value & ", INVOICE_DATE,CUST_REF_NO,ITEM_CODE)"
                Else
                    SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMPORATE(CUST_REF_NO," & Val(txtAmendNo.Text) & ",ITEM_CODE)"
                End If
            End If
        ElseIf optAgt(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " PORATE=GetSALEITEMPRICE(-1,CUST_REF_NO, SUPP_CUST_CODE,ITEM_CODE) "
        Else
            SqlStr = SqlStr & vbCrLf & " PORATE=GetITEMJWRate(" & RsCompany.Fields("COMPANY_CODE").Value & ",1,MRRDATE,CUST_REF_NO,AUTO_KEY_MRR,ITEM_CODE,SERIAL_NO) "
        End If


        SqlStr = SqlStr & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        PubDBCn.Execute(SqlStr)

        If optType(0).Checked = True Then
            SqlStr = " DELETE FROM TEMP_DNCN_TRN " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"


            SqlStr = SqlStr & vbCrLf & " AND PORATE >= ITEM_RATE - DNCN_RATE + SUPP_RATE"
            SqlStr = SqlStr & vbCrLf & " AND ACCPETED >0"

            PubDBCn.Execute(SqlStr)
        ElseIf optType(1).Checked = True Then
            SqlStr = " DELETE FROM TEMP_DNCN_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
            SqlStr = SqlStr & vbCrLf & " AND PORATE < ITEM_RATE - DNCN_RATE + SUPP_RATE"
            PubDBCn.Execute(SqlStr)

            SqlStr = " DELETE FROM TEMP_DNCN_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
            SqlStr = SqlStr & vbCrLf & " AND (DNCN_RATE=0 OR PORATE=0)"
            PubDBCn.Execute(SqlStr)
        End If

        '    PubDBCn.CommitTrans						

        InsertIntoTemp = True
        Exit Function
ErrPart:
        'Resume						
        MsgInformation(Err.Description)
        '    PubDBCn.RollbackTrans						
        InsertIntoTemp = False
    End Function

    Private Function InsertIntoTempOld(ByRef pSuppCustCode As String, ByRef pItemCode As String) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSqlStr As String

        InsertIntoTempOld = False

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_DNCN_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        mSqlStr = " INSERT INTO TEMP_DNCN_TRN ( " & vbCrLf & " USERID, MKEY, COMPANY_CODE, " & vbCrLf & " FYEAR, ACCOUNTCODE_DR, ACCOUNTCODE_CR, " & vbCrLf & " VNO, VDATE, BILLNO, " & vbCrLf & " INVOICE_DATE, AUTO_KEY_MRR, MRRDATE, " & vbCrLf & " CUST_REF_NO, ITEM_CODE, ITEM_UOM, ITEM_QTY, " & vbCrLf & " ACCPETED, ITEM_RATE, DNCN_RATE, " & vbCrLf & " SUPP_RATE, PORATE) "

        SqlStr = ""

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " IH.MKEY, IH.COMPANY_CODE, IH.FYEAR, " & vbCrLf & " IH.SUPP_CUST_CODE, IH.ACCOUNTCODE, " & vbCrLf & " IH.VNO, IH.VDATE, IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf & " IH.AUTO_KEY_MRR, IH.MRRDATE," & vbCrLf & " NVL(ID.CUST_REF_NO,'-1'), ID.ITEM_CODE, ID.ITEM_UOM, "

        SqlStr = SqlStr & vbCrLf & " ID.ITEM_QTY, " & vbCrLf & " TO_CHAR(NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0) + GETREOFFERQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.AUTO_KEY_MRR,IH.MRRDATE,IH.SUPP_CUST_CODE,ID.ITEM_CODE)) AS ACCPETED, " & vbCrLf & " ID.ITEM_RATE, " & vbCrLf & " NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0) AS DNCN_RATE,  " & vbCrLf & " NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0) AS SUPP_RATE, " & vbCrLf & " CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)<>'S' THEN GetITEMPRICE_NEW(IH.FYEAR, " & RsCompany.Fields("FYEAR").Value & ", IH.INVOICE_DATE,ID.CUST_REF_NO,ID.ITEM_CODE) ELSE 0 END AS PORATE"

        ''+ " & vbCrLf _						
        '& " GETREOFFERQTY(" & RsCompany!COMPANY_CODE & ",IH.AUTO_KEY_MRR,IH.MRRDATE,IH.SUPP_CUST_CODE,ID.ITEM_CODE)						
        ''& " NVL(GETCNFROMDN(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE),0) AS DNCN_RATE," & vbCrLf _						
        '						
        ''''FROM CLAUSE...						
        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID "

        ''''WHERE CLAUSE...''IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "						

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND " & vbCrLf & " IH.MKEY=ID.MKEY"


        '    SqlStr = SqlStr & vbCrLf & " AND SUBSTR(ID.CUST_REF_NO,1,1)<>'S' AND CUST_REF_NO IS NOT NULL"						

        SqlStr = SqlStr & vbCrLf & " AND (CUST_REF_NO IS NOT NULL OR CUST_REF_NO<>'')"

        If optType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)<>'S' THEN GetITEMPRICE_NEW(IH.FYEAR, " & RsCompany.Fields("FYEAR").Value & ", IH.INVOICE_DATE,ID.CUST_REF_NO,ID.ITEM_CODE) ELSE 0 END< " & vbCrLf & " (ID.ITEM_RATE - NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0) " & vbCrLf & " + NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0)) "

            ''& " + NVL(GETCNFROMDN(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE),0)) "						
        Else
            SqlStr = SqlStr & vbCrLf & " AND CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)<>'S' THEN GetITEMPRICE_NEW(IH.FYEAR, " & RsCompany.Fields("FYEAR").Value & ", IH.INVOICE_DATE,ID.CUST_REF_NO,ID.ITEM_CODE) ELSE 0 END >  " & vbCrLf & " (ID.ITEM_RATE -NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0) " & vbCrLf & " + NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0)) "

            ''& " + NVL(GETCNFROMDN(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE),0)) "						
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.VNO<>'-1' AND IH.ISFINALPOST='Y' AND CANCELLED='N'"

        If pSuppCustCode <> "-1" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSuppCustCode)) & "'"
        End If

        If pItemCode <> "-1" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(pItemCode)) & "'"
        End If


        If optBaseOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(TxtDtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(TxtDtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        ElseIf optBaseOn(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(TxtDtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(TxtDtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.MRRDATE>=TO_DATE('" & VB6.Format(TxtDtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.MRRDATE<=TO_DATE('" & VB6.Format(TxtDtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " ORDER BY IH.SUPP_CUST_CODE,IH.AUTO_KEY_MRR, IH.VNO, IH.VDATE "

        SqlStr = mSqlStr & vbCrLf & SqlStr

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        InsertIntoTempOld = True
        Exit Function
ErrPart:
        MsgInformation(Err.Description)
        PubDBCn.RollbackTrans()
        InsertIntoTempOld = False
    End Function
    Private Function MakeSQL(ByRef pSuppCustCode As String, ByRef pItemCode As String, ByRef mDivisionCode As Double) As String
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart

        MakeSQL = "SELECT DISTINCT " & vbCrLf & " COMPANY_CODE, " & vbCrLf & " ACCOUNTCODE_DR, ACCOUNTCODE_CR,ISGSTREFUND, " & vbCrLf & " BILLNO,INVOICE_DATE,VNO,VDATE, AUTO_KEY_MRR, MRRDATE"

        '', FYEAR ''16-01-2008 ''sk duplicate..						
        '            MKEY, , VNO , " & vbCrLf" _						
        ''            & " VDATE, BILLNO, INVOICE_DATE, AUTO_KEY_MRR, MRRDATE "						

        MakeSQL = MakeSQL & vbCrLf & " FROM TEMP_DNCN_TRN "

        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' AND ACCPETED>0 " & vbCrLf
        If chkAgtD3.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND PORATE<>0"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND CASE WHEN SUBSTR(CUST_REF_NO,1,1)='S' THEN 1 ELSE PORATE END<>0"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY ACCOUNTCODE_DR "

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click
        SearchItem()
    End Sub
    Private Sub SearchAccounts()
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub FrmDrNoteProcess_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmDrNoteProcess_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim MainClass_Renamed As Object
        On Error GoTo LErr

        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        MainClass.SetControlsColor(Me)

        ''Set PvtDBCn = New ADODB.Connection						
        ''PvtDBCn.Open StrConn						


        TxtDtFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        TxtDtTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtItem.Enabled = False
        cmdSearchItem.Enabled = False
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False

        'Me.Height = VB6.TwipsToPixelsY(6240)
        'Me.Width = VB6.TwipsToPixelsX(5010)
        Me.Top = 0
        Me.Left = 0


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
        cboDivision.Enabled = True
        Exit Sub
LErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub optBaseOn_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBaseOn.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optBaseOn.GetIndex(eventSender)
            cmdProcess.Enabled = True
        End If
    End Sub

    Private Sub OptCustomer_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCustomer.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optCustomer.GetIndex(eventSender)
            TxtAccount.Enabled = IIf(Index = 0, False, True)
            cmdsearch.Enabled = IIf(Index = 0, False, True)
            cmdProcess.Enabled = True
        End If
    End Sub
    Private Sub OptItem_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptItem.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptItem.GetIndex(eventSender)
            txtItem.Enabled = IIf(Index = 0, False, True)
            cmdSearchItem.Enabled = IIf(Index = 0, False, True)
            cmdProcess.Enabled = True
        End If
    End Sub

    Private Sub optRate_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optRate.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optRate.GetIndex(eventSender)
            If optRate(0).Checked = True Then
                txtPONo.Enabled = False
                txtAmendNo.Enabled = False
            Else
                txtPONo.Enabled = True
                txtAmendNo.Enabled = True
            End If
        End If
    End Sub

    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
            cmdProcess.Enabled = True
        End If
    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        cmdProcess.Enabled = True
    End Sub

    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub

    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim MainClass_Renamed As Object
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
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

    Private Sub TxtDtFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtDtFrom.TextChanged
        cmdProcess.Enabled = True
    End Sub

    Private Sub TxtDtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtDtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If TxtDtFrom.Text = "" Then
            MsgBox("Date From Cannot Be Blank", MsgBoxStyle.Critical)
            TxtDtFrom.Focus()
            Cancel = True
        ElseIf TxtDtFrom.Text <> "" Then
            If Not IsDate(TxtDtFrom.Text) Then
                MsgBox("Invalid Date Pl. Check", MsgBoxStyle.Critical)
                TxtDtFrom.Focus()
                Cancel = True
                '        ElseIf FYChk(CDate(TxtDtFrom.Text)) = False Then						
                '            TxtDtFrom.SetFocus						
                '            Cancel = True						
            End If
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtDtTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtDtTo.TextChanged
        cmdProcess.Enabled = True
    End Sub

    Private Sub TxtDtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtDtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If TxtDtTo.Text = "" Then
            MsgBox("Date To. Cannot Be Blank", MsgBoxStyle.Critical)
            TxtDtTo.Focus()
            Cancel = True
            GoTo EventExitSub
        ElseIf TxtDtTo.Text <> "" Then
            If Not IsDate(TxtDtTo.Text) Then
                MsgBox("Invalid Date Pl. Check", MsgBoxStyle.Critical)
                TxtDtTo.Focus()
                Cancel = True
            ElseIf FYChk(CStr(CDate(TxtDtTo.Text))) = False Then
                TxtDtTo.Focus()
                Cancel = True
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItem.TextChanged
        cmdProcess.Enabled = True
    End Sub

    Private Sub TxtItem_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItem.DoubleClick
        SearchItem()
    End Sub

    Private Sub txtItem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItem.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim MainClass_Renamed As Object
        KeyAscii = MainClass.UpperCase(KeyAscii, txtItem.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtItem_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItem.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub

    Private Sub TxtItem_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItem.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If txtItem.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((txtItem.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            txtItem.Text = UCase(Trim(txtItem.Text))
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
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        MainClass.SearchGridMaster(txtItem.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            txtItem.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class
