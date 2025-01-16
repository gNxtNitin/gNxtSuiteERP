Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmUpdAssetData
    Inherits System.Windows.Forms.Form
    Dim SqlStr As String

    Private Sub chkVNoAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkVNoAll.CheckStateChanged
        txtVNo.Enabled = IIf(chkVNoAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    'Dim RsDDR As ADODB.Recordset						
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub CmdProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdProcess.Click

        If ValidateBranchLocking((TxtDateFrom.Text)) = True Then
            TxtDateFrom.Focus()
            Exit Sub
        End If

        If OptAccount(1).Checked = True Then
            If Trim(txtAccount.Text) = "" Then
                MsgInformation("Please Select Customer Name")
                txtAccount.Focus()
                Exit Sub
            End If
        End If

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            MsgBox("Processed Successfully")
        Else
            MsgBox("Process Failed")
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function Update1() As Boolean
        On Error GoTo ErrPart

        If optType(0).Checked = True Then
            If UpdatePurchase() = False Then GoTo ErrPart
        ElseIf optType(1).Checked = True Then
            If UpdateVoucher() = False Then GoTo ErrPart
        Else
            If UpdateDebit() = False Then GoTo ErrPart
        End If
        Update1 = True
        Exit Function
ErrPart:
        Update1 = False

        MsgBox(Err.Description)
        ''Resume						
    End Function
    Private Function UpdatePurchase() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTempDet As ADODB.Recordset
        Dim RsPur As ADODB.Recordset
        Dim mMkey As String
        Dim mTRNType As String
        Dim mVNO As String
        Dim mVDate As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mMRRNo As String
        Dim mMRRDate As String
        Dim mSupplierCode As String
        Dim mAccountCode As String
        Dim mSupplier As String
        Dim mBookType As String
        Dim mItemValue As Double
        Dim mNetAmount As Double
        Dim mModvatPer As Double
        Dim mModvatAmount As Double
        Dim mSTRefundAmount As Double
        Dim mISFixAssets As String
        Dim mItemDesc As String
        Dim mItemCode As String
        Dim mItemName As String
        Dim mItemQty As String
        Dim xSqlStr As String
        Dim mCount As Integer
        Dim mFYear As Short
        Dim mExpAmount As Double
        Dim mItemType As String
        Dim mAEDPer As Double

        Dim mCGSTClaimAmount As Double
        Dim mSGSTClaimAmount As Double
        Dim mIGSTClaimAmount As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If OptAccount(1).Checked = True Then
            If MainClass.ValidateWithMasterTable(txtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
            End If
            '        SqlStr = SqlStr & vbCrLf & " AND IH.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"

        End If

        SqlStr = "SELECT " & vbCrLf _
            & " IH.MKEY, IH.FYEAR, ID.ITEM_TRNTYPE, IH.VNO, IH.VDATE, IH.BILLNO, " & vbCrLf _
            & " IH.INVOICE_DATE, IH.AUTO_KEY_MRR, IH.MRRDATE, IH.SUPP_CUST_CODE, " & vbCrLf _
            & " IH.BOOKTYPE, IH.ITEMDESC, NETVALUE * SUM(ID.ITEM_AMT)/DECODE(ITEMVALUE,0,1,ITEMVALUE) AS NETVALUE, " & vbCrLf _
            & " IH.MODVATPER, MODVATAMOUNT * SUM(ID.ITEM_AMT)/DECODE(ITEMVALUE,0,1,ITEMVALUE) AS MODVATAMOUNT, " & vbCrLf _
            & " CESSAMOUNT * SUM(ID.ITEM_AMT)/DECODE(ITEMVALUE,0,1,ITEMVALUE) AS CESSAMOUNT, " & vbCrLf _
            & " SHECMODVATAMOUNT * SUM(ID.ITEM_AMT)/DECODE(ITEMVALUE,0,1,ITEMVALUE) AS SHECMODVATAMOUNT, " & vbCrLf _
            & " ADEMODVATAMOUNT * SUM(ID.ITEM_AMT)/DECODE(ITEMVALUE,0,1,ITEMVALUE) AS ADEMODVATAMOUNT, " & vbCrLf _
            & " STCLAIMAMOUNT * SUM(ID.ITEM_AMT)/DECODE(ITEMVALUE,0,1,ITEMVALUE) AS STCLAIMAMOUNT, " & vbCrLf _
            & " SUR_VATCLAIMAMOUNT * SUM(ID.ITEM_AMT)/DECODE(ITEMVALUE,0,1,ITEMVALUE) AS SUR_VATCLAIMAMOUNT, " & vbCrLf _
            & " SERVICECLAIMAMOUNT * SUM(ID.ITEM_AMT)/DECODE(ITEMVALUE,0,1,ITEMVALUE) AS SERVICECLAIMAMOUNT," & vbCrLf _
            & " ISSERVCLAIM, ISSERVTAX_POST, ISSTREFUND, ISCAPITAL, ADEMODVATPER, ISFIXASSETS, " & vbCrLf _
            & " TOTCGST_REFUNDAMT * SUM(ID.ITEM_AMT)/DECODE(ITEMVALUE,0,1,ITEMVALUE) AS TOTCGST_REFUNDAMT," & vbCrLf _
            & " TOTSGST_REFUNDAMT * SUM(ID.ITEM_AMT)/DECODE(ITEMVALUE,0,1,ITEMVALUE) AS TOTSGST_REFUNDAMT," & vbCrLf _
            & " TOTIGST_REFUNDAMT * SUM(ID.ITEM_AMT)/DECODE(ITEMVALUE,0,1,ITEMVALUE) AS TOTIGST_REFUNDAMT" & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_INVTYPE_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_TRNTYPE=INVMST.CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.CANCELLED='N' AND IH.ISFOC='N' AND IH.ISFINALPOST='Y'" & vbCrLf _
            & " AND INVMST.CATEGORY='P' AND ISFIXASSETS='Y'"


        If OptAccount(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND INVMST.ACCOUNTPOSTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        If chkVNoAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VNO ='" & Trim(txtVNo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(TxtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.MKEY, IH.COMPANY_CODE, IH.FYEAR, ID.ITEM_TRNTYPE, IH.VNO, IH.VDATE, IH.BILLNO, " & vbCrLf & " IH.INVOICE_DATE, IH.AUTO_KEY_MRR, IH.MRRDATE, IH.SUPP_CUST_CODE, " & vbCrLf & " IH.BOOKTYPE, IH.ITEMDESC, NETVALUE , ITEMVALUE, " & vbCrLf & " IH.MODVATPER, MODVATAMOUNT, " & vbCrLf & " CESSAMOUNT ,SHECMODVATAMOUNT ,ADEMODVATAMOUNT,STCLAIMAMOUNT,SUR_VATCLAIMAMOUNT,SERVICECLAIMAMOUNT, " & vbCrLf & " ISSERVCLAIM , ISSERVTAX_POST, ISSTREFUND, ISCAPITAL, ADEMODVATPER, ISFIXASSETS,TOTCGST_REFUNDAMT,TOTSGST_REFUNDAMT,TOTIGST_REFUNDAMT"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.COMPANY_CODE, IH.FYEAR, IH.VNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPur, ADODB.LockTypeEnum.adLockReadOnly)
        mCount = 0
        If RsPur.EOF = False Then
            Do While RsPur.EOF = False
                mSTRefundAmount = 0
                mMkey = IIf(IsDBNull(RsPur.Fields("mKey").Value), "", RsPur.Fields("mKey").Value)
                mFYear = IIf(IsDBNull(RsPur.Fields("FYEAR").Value), "", RsPur.Fields("FYEAR").Value)
                mTRNType = IIf(IsDBNull(RsPur.Fields("ITEM_TRNTYPE").Value), "", RsPur.Fields("ITEM_TRNTYPE").Value)
                mVNO = IIf(IsDBNull(RsPur.Fields("VNO").Value), "", RsPur.Fields("VNO").Value)
                mVDate = IIf(IsDBNull(RsPur.Fields("VDATE").Value), "", RsPur.Fields("VDATE").Value)
                mBillNo = IIf(IsDBNull(RsPur.Fields("BILLNO").Value), "", RsPur.Fields("BILLNO").Value)
                mBillDate = IIf(IsDBNull(RsPur.Fields("INVOICE_DATE").Value), "", RsPur.Fields("INVOICE_DATE").Value)
                mMRRNo = IIf(IsDBNull(RsPur.Fields("AUTO_KEY_MRR").Value), "", RsPur.Fields("AUTO_KEY_MRR").Value)
                mMRRDate = IIf(IsDBNull(RsPur.Fields("MRRDATE").Value), "", RsPur.Fields("MRRDATE").Value)
                mSupplierCode = IIf(IsDBNull(RsPur.Fields("SUPP_CUST_CODE").Value), "", RsPur.Fields("SUPP_CUST_CODE").Value)
                If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSupplier = MasterNo
                Else
                    mSupplier = ""
                End If

                mBookType = IIf(IsDBNull(RsPur.Fields("BookType").Value), "", RsPur.Fields("BookType").Value)
                mItemType = IIf(IsDBNull(RsPur.Fields("ITEMDESC").Value), "", RsPur.Fields("ITEMDESC").Value)

                mItemValue = IIf(IsDBNull(RsPur.Fields("NETVALUE").Value), 0, RsPur.Fields("NETVALUE").Value) ''   IIf(IsNull(RsPur!ITEMVALUE), 0, RsPur!ITEMVALUE)						
                mNetAmount = IIf(IsDBNull(RsPur.Fields("NETVALUE").Value), 0, RsPur.Fields("NETVALUE").Value)

                mModvatPer = IIf(IsDBNull(RsPur.Fields("MODVATPER").Value), 0, RsPur.Fields("MODVATPER").Value)
                mModvatAmount = IIf(IsDBNull(RsPur.Fields("MODVATAMOUNT").Value), 0, RsPur.Fields("MODVATAMOUNT").Value)

                mModvatAmount = mModvatAmount + IIf(IsDBNull(RsPur.Fields("CESSAMOUNT").Value), 0, RsPur.Fields("CESSAMOUNT").Value)

                mModvatAmount = mModvatAmount + IIf(IsDBNull(RsPur.Fields("SHECMODVATAMOUNT").Value), 0, RsPur.Fields("SHECMODVATAMOUNT").Value)

                mCGSTClaimAmount = IIf(IsDBNull(RsPur.Fields("TOTCGST_REFUNDAMT").Value), 0, RsPur.Fields("TOTCGST_REFUNDAMT").Value)
                mSGSTClaimAmount = IIf(IsDBNull(RsPur.Fields("TOTSGST_REFUNDAMT").Value), 0, RsPur.Fields("TOTSGST_REFUNDAMT").Value)
                mIGSTClaimAmount = IIf(IsDBNull(RsPur.Fields("TOTIGST_REFUNDAMT").Value), 0, RsPur.Fields("TOTIGST_REFUNDAMT").Value)

                If RsPur.Fields("ISCAPITAL").Value = "Y" Then
                    mModvatAmount = mModvatAmount * 2
                End If

                mAEDPer = IIf(IsDBNull(RsPur.Fields("ADEMODVATPER").Value), 0, RsPur.Fields("ADEMODVATPER").Value)

                If RsPur.Fields("ISCAPITAL").Value = "Y" Then
                    If mAEDPer = 50 Then
                        mModvatAmount = mModvatAmount + (IIf(IsDBNull(RsPur.Fields("ADEMODVATAMOUNT").Value), 0, RsPur.Fields("ADEMODVATAMOUNT").Value) * 2)
                    Else
                        mModvatAmount = mModvatAmount + IIf(IsDBNull(RsPur.Fields("ADEMODVATAMOUNT").Value), 0, RsPur.Fields("ADEMODVATAMOUNT").Value)
                    End If
                Else
                    mModvatAmount = mModvatAmount + IIf(IsDBNull(RsPur.Fields("ADEMODVATAMOUNT").Value), 0, RsPur.Fields("ADEMODVATAMOUNT").Value)
                End If

                If RsPur.Fields("ISSTREFUND").Value = "Y" Then
                    mSTRefundAmount = IIf(IsDBNull(RsPur.Fields("STCLAIMAMOUNT").Value), "", RsPur.Fields("STCLAIMAMOUNT").Value)
                    mSTRefundAmount = mSTRefundAmount + IIf(IsDBNull(RsPur.Fields("SUR_VATCLAIMAMOUNT").Value), "", RsPur.Fields("SUR_VATCLAIMAMOUNT").Value)
                End If

                If RsPur.Fields("ISSERVCLAIM").Value = "Y" And RsPur.Fields("ISSERVTAX_POST").Value = "Y" Then
                    mModvatAmount = mModvatAmount + IIf(IsDBNull(RsPur.Fields("SERVICECLAIMAMOUNT").Value), "", RsPur.Fields("SERVICECLAIMAMOUNT").Value)
                End If
                mItemValue = mItemValue - mModvatAmount

                mISFixAssets = IIf(IsDBNull(RsPur.Fields("ISFIXASSETS").Value), "", RsPur.Fields("ISFIXASSETS").Value)

                mItemDesc = GetItemDesc(mMkey, mTRNType)

                '            xSqlStr = " Select * " & vbCrLf _						
                ''                & " FROM FIN_PURCHASE_DET" & vbCrLf _						
                ''                & " WHERE MKEY='" & mMkey & "'"						
                '						
                '            MainClass.UOpenRecordSet xSqlStr, PubDBCn, adOpenStatic, RsTempDet, adLockReadOnly						

                '            If RsTempDet.EOF = False Then						
                '                mItemDesc = ""						
                '                Do While RsTempDet.EOF = False						
                '                    mItemCode = IIf(IsNull(RsTempDet!ITEM_CODE), "", RsTempDet!ITEM_CODE)						
                '                    mItemQty = IIf(IsNull(RsTempDet!ITEM_QTY), "", RsTempDet!ITEM_QTY) & " " & IIf(IsNull(RsTempDet!ITEM_UOM), "", RsTempDet!ITEM_UOM)						
                '                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then						
                '                        mItemName = MasterNo						
                '                    Else						
                '                        mItemName = ""						
                '                    End If						
                '                    mItemDesc = IIf(mItemDesc = "", "", mItemDesc & ", ") & mItemName & " (" & mItemQty & ")"						
                '                    RsTempDet.MoveNext						
                '                Loop						
                '            End If						
                '            mItemDesc = Left(mItemDesc, 250)						

                If UpdateAssetTRN(mMkey, mFYear, mTRNType, "N", "N", Val(mMRRNo), VB6.Format(mMRRDate, "DD-MMM-YYYY"), Trim(mBillNo), VB6.Format(mBillDate, "DD-MMM-YYYY"), Trim(mVNO), VB6.Format(mVDate, "DD-MMM-YYYY"), mBookType, Trim(mSupplier), Trim(mItemDesc), mItemValue, Val(CStr(mNetAmount)), Val(CStr(mModvatAmount)), Val(CStr(mModvatPer)), Val(CStr(mSTRefundAmount)), mISFixAssets, mItemType, mCGSTClaimAmount, mSGSTClaimAmount, mIGSTClaimAmount) = False Then GoTo ErrPart

                mCount = mCount + 1
                lblCount.Text = CStr(mCount)
                System.Windows.Forms.Application.DoEvents()
                RsPur.MoveNext()
            Loop
        End If

        UpdatePurchase = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdatePurchase = False
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description)
        ''Resume						
    End Function
    Private Function GetItemDesc(ByRef xMKey As String, ByRef xTrnType As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTempDet As ADODB.Recordset
        Dim mItemCode As String
        Dim mItemName As String
        Dim mItemQty As String
        Dim mItemDesc As String

        SqlStr = "SELECT ID.*" & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_INVTYPE_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_TRNTYPE=INVMST.CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.CANCELLED='N' AND IH.ISFOC='N' AND IH.ISFINALPOST='Y'" & vbCrLf & " AND INVMST.CATEGORY='P' AND ISFIXASSETS='Y'" & vbCrLf & " AND ID.MKEY='" & xMKey & "' AND ID.ITEM_TRNTYPE='" & xTrnType & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

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
        GetItemDesc = VB.Left(mItemDesc, 250)

        Exit Function
ErrPart:
        GetItemDesc = ""
        ''Resume						
    End Function
    Private Function UpdateDebit() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTempDet As ADODB.Recordset
        Dim RsPur As ADODB.Recordset
        Dim mMkey As String
        Dim mTRNType As String
        Dim mVNO As String
        Dim mVDate As String

        Dim mDebitAccountCode As String
        Dim mCreditAccountCode As String
        Dim mNETVALUE As Double
        Dim mAccountCode As String
        Dim mBookType As String
        Dim mPurNo As String
        Dim mPurDate As String
        Dim mCount As Integer
        Dim mFYear As Short

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If OptAccount(1).Checked = True Then
            If MainClass.ValidateWithMasterTable(txtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
            End If
            '        SqlStr = SqlStr & vbCrLf & " AND (IH.DEBITACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "' OR IH.CREDITACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "')"						
        End If

        SqlStr = "SELECT * FROM FIN_DNCN_HDR IH" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.CANCELLED='N' AND IH.APPROVED='Y'"

        SqlStr = SqlStr & " AND (IH.DEBITACCOUNTCODE IN (" & vbCrLf & " SELECT ACCOUNTPOSTCODE " & vbCrLf & " FROM FIN_INVTYPE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='P' AND ISFIXASSETS='Y'"

        If OptAccount(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTPOSTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & ")"

        SqlStr = SqlStr & " OR IH.CREDITACCOUNTCODE IN (" & vbCrLf & " SELECT ACCOUNTPOSTCODE " & vbCrLf & " FROM FIN_INVTYPE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='P' AND ISFIXASSETS='Y'"

        If OptAccount(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTPOSTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & "))"

        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(TxtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkVNoAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VNO ='" & Trim(txtVNo.Text) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPur, ADODB.LockTypeEnum.adLockReadOnly)
        mCount = 0
        If RsPur.EOF = False Then
            Do While RsPur.EOF = False
                mMkey = IIf(IsDBNull(RsPur.Fields("mKey").Value), "", RsPur.Fields("mKey").Value)
                mFYear = IIf(IsDBNull(RsPur.Fields("FYEAR").Value), "", RsPur.Fields("FYEAR").Value)
                mVNO = IIf(IsDBNull(RsPur.Fields("VNO").Value), "", RsPur.Fields("VNO").Value)
                mVDate = IIf(IsDBNull(RsPur.Fields("VDATE").Value), "", RsPur.Fields("VDATE").Value)
                mBookType = IIf(IsDBNull(RsPur.Fields("BookType").Value), "", RsPur.Fields("BookType").Value)
                mDebitAccountCode = IIf(IsDBNull(RsPur.Fields("DEBITACCOUNTCODE").Value), "", RsPur.Fields("DEBITACCOUNTCODE").Value)
                mCreditAccountCode = IIf(IsDBNull(RsPur.Fields("CREDITACCOUNTCODE").Value), "", RsPur.Fields("CREDITACCOUNTCODE").Value)
                mNETVALUE = IIf(IsDBNull(RsPur.Fields("NETVALUE").Value), 0, RsPur.Fields("NETVALUE").Value)

                mPurNo = IIf(IsDBNull(RsPur.Fields("PURVNO").Value), "", RsPur.Fields("PURVNO").Value)
                mPurDate = IIf(IsDBNull(RsPur.Fields("PURVDATE").Value), "", RsPur.Fields("PURVDATE").Value)

                If UpdateDNCNFixedAssets(mMkey, mFYear, mVNO, mVDate, mBookType, mDebitAccountCode, mCreditAccountCode, Val(CStr(mNETVALUE)), "N", mPurNo, mPurDate) = False Then GoTo ErrPart

                mCount = mCount + 1
                lblCount.Text = CStr(mCount)
                System.Windows.Forms.Application.DoEvents()
                RsPur.MoveNext()
            Loop
        End If

        UpdateDebit = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateDebit = False
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description)
        ''Resume						
    End Function
    Private Function UpdateVoucher() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTempDet As ADODB.Recordset
        Dim RsPur As ADODB.Recordset
        Dim mMkey As String
        Dim mVNO As String
        Dim mVDate As String
        Dim mAccountCode As String
        Dim mBookType As String
        Dim mISFixAssets As String
        Dim xSqlStr As String
        Dim mCount As Integer
        Dim mFYear As Short

        Dim xAccountCode As String
        Dim mAmount As Double
        Dim mDc As String
        Dim pNarration As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If OptAccount(1).Checked = True Then
            If MainClass.ValidateWithMasterTable(txtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
            End If
            '        SqlStr = SqlStr & vbCrLf & " AND ID.ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"						
        End If

        SqlStr = "SELECT DISTINCT IH.COMPANY_CODE,IH.MKEY, IH.FYEAR, IH.BOOKTYPE, IH.VNO, IH.VDATE " & vbCrLf & " FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.CANCELLED='N' " & vbCrLf & " AND ID.ACCOUNTCODE IN (" & vbCrLf & " SELECT ACCOUNTPOSTCODE " & vbCrLf & " FROM FIN_INVTYPE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='P' AND ISFIXASSETS='Y'" ''& vbCrLf |						
        If OptAccount(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTPOSTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & ")"

        If chkVNoAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VNO ='" & Trim(txtVNo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(TxtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(TxtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.COMPANY_CODE, IH.FYEAR, IH.VNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPur, ADODB.LockTypeEnum.adLockReadOnly)
        mCount = 0
        If RsPur.EOF = False Then
            Do While RsPur.EOF = False
                mMkey = IIf(IsDBNull(RsPur.Fields("mKey").Value), "", RsPur.Fields("mKey").Value)
                mFYear = IIf(IsDBNull(RsPur.Fields("FYEAR").Value), "", RsPur.Fields("FYEAR").Value)
                mVNO = IIf(IsDBNull(RsPur.Fields("VNO").Value), "", RsPur.Fields("VNO").Value)
                mVDate = IIf(IsDBNull(RsPur.Fields("VDATE").Value), "", RsPur.Fields("VDATE").Value)
                mBookType = IIf(IsDBNull(RsPur.Fields("BookType").Value), "", RsPur.Fields("BookType").Value)

                xSqlStr = " Select ACCOUNTCODE, SUM(AMOUNT * DECODE(DC,'D',1,-1)) AMOUNT,MAX(PARTICULARS) PARTICULARS  " & vbCrLf & " FROM FIN_VOUCHER_DET" & vbCrLf & " WHERE MKEY='" & mMkey & "'" & vbCrLf & " "

                If OptAccount(1).Checked = True Then
                    xSqlStr = xSqlStr & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
                End If

                xSqlStr = xSqlStr & vbCrLf & " GROUP BY ACCOUNTCODE"

                MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTempDet.EOF = False Then
                    Do While RsTempDet.EOF = False
                        xAccountCode = IIf(IsDBNull(RsTempDet.Fields("ACCOUNTCODE").Value), "", RsTempDet.Fields("ACCOUNTCODE").Value)
                        mAmount = IIf(IsDBNull(RsTempDet.Fields("AMOUNT").Value), 0, RsTempDet.Fields("AMOUNT").Value)
                        mDc = IIf(mAmount > 0, "D", "C") ''IIf(IsNull(RsTempDet!DC), "D", RsTempDet!DC)						
                        mAmount = System.Math.Abs(mAmount)
                        pNarration = IIf(IsDBNull(RsTempDet.Fields("PARTICULARS").Value), "", RsTempDet.Fields("PARTICULARS").Value)

                        If MainClass.ValidateWithMasterTable(xAccountCode, "ACCOUNTPOSTCODE", "ISFIXASSETS", "FIN_INVTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                            mISFixAssets = MasterNo
                        Else
                            mISFixAssets = "N"
                        End If
                        If mISFixAssets = "Y" Then
                            If UpdateFixedAssetFromVoucher(mMkey, mFYear, mBookType, mVNO, mVDate, xAccountCode, mAmount, mDc, pNarration) = False Then GoTo ErrPart
                        End If
                        RsTempDet.MoveNext()
                    Loop
                End If

                mCount = mCount + 1
                lblCount.Text = CStr(mCount)
                System.Windows.Forms.Application.DoEvents()
                RsPur.MoveNext()
            Loop
        End If

        UpdateVoucher = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateVoucher = False
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description)
        ''Resume						
    End Function

    Private Function UpdateFixedAssetFromVoucher(ByRef mMkey As String, ByRef mFYear As Short, ByRef mBookType As String, ByRef mVNO As String, ByRef mVDate As String, ByRef xAccountCode As String, ByRef mAmount As Double, ByRef mDc As String, ByRef pNarration As String) As Boolean
        On Error GoTo ErrDetail

        Dim i As Integer
        Dim xSqlStr As String
        Dim RsTemp1 As ADODB.Recordset
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mAccountName As String
        Dim mPRRowNo As Integer
        Dim mSuppCode As String
        Dim mBalAmount As Double
        Dim mBillAmount As Double
        Dim mBillDC As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mTRNType As String
        Dim mBillFalse As Boolean
        mBalAmount = mAmount * IIf(mDc = "D", 1, -1)

        If MainClass.ValidateWithMasterTable(xAccountCode, "ACCOUNTPOSTCODE", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
            mTRNType = MasterNo
        Else
            mTRNType = "-1"
        End If

        xSqlStr = " Select * " & vbCrLf _
            & " FROM FIN_VOUCHER_DET" & vbCrLf _
            & " WHERE MKEY='" & mMkey & "'"

        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp1.EOF = False Then
            Do While RsTemp1.EOF = False
                mSuppCode = IIf(IsDBNull(RsTemp1.Fields("ACCOUNTCODE").Value), "-1", RsTemp1.Fields("ACCOUNTCODE").Value)
                mAccountName = IIf(MainClass.ValidateWithMasterTable(mSuppCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, mSuppCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True, mSuppCode, -1)
                mPRRowNo = IIf(IsDBNull(RsTemp1.Fields("PRROWNO").Value), "-1", RsTemp1.Fields("PRROWNO").Value)
                mSuppCode = IIf(IsDBNull(RsTemp1.Fields("DC").Value), "C", RsTemp1.Fields("DC").Value)

                SqlStr = "SELECT BILLNO, BILLDATE,BILLAMOUNT,BILLDC, AMOUNT, DC" & vbCrLf & " FROM FIN_BILLDETAILs_TRN " & vbCrLf & " WHERE " & vbCrLf & " MKEY='" & mMkey & "'" & vbCrLf & " AND ACCOUNTCODE='" & mSuppCode & "'" & vbCrLf & " AND TRNDtlSubRowNo=" & mPRRowNo & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    Do While RsTemp.EOF = False
                        mBillFalse = True
                        mBillDC = IIf(IsDBNull(RsTemp.Fields("DC").Value), "D", RsTemp.Fields("DC").Value)
                        mBillAmount = IIf(IsDBNull(RsTemp.Fields("AMOUNT").Value), 0, RsTemp.Fields("AMOUNT").Value) * IIf(mBillDC = "D", -1, 1)
                        mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), 0, RsTemp.Fields("BILLNO").Value)
                        mBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLDATE").Value), 0, RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")
                        mBalAmount = mBalAmount - mBillAmount

                        If System.Math.Abs(mAmount) < System.Math.Abs(mBillAmount) Then
                            mBillAmount = mAmount
                            mBillFalse = False
                        End If
                        If UpdateAssetTRN(mMkey, mFYear, mTRNType, "N", "N", 0, VB6.Format(mVDate, "DD-MMM-YYYY"), Trim(mBillNo), VB6.Format(mBillDate, "DD-MMM-YYYY"), Trim(mVNO), VB6.Format(mVDate, "DD-MMM-YYYY"), mBookType, Trim(mAccountName), Trim(pNarration), mBillAmount, Val(CStr(mBillAmount)), 0, 0, 0, "Y", "", 0, 0, 0) = False Then GoTo ErrDetail

                        If mBillFalse = False Then UpdateFixedAssetFromVoucher = True : Exit Function
                        RsTemp.MoveNext()
                    Loop
                End If

                RsTemp1.MoveNext()
            Loop
        End If

        If mBalAmount <> 0 Then
            If UpdateAssetTRN(mMkey, mFYear, mTRNType, "N", "N", 0, VB6.Format(mVDate, "DD-MMM-YYYY"), Trim(mVNO), VB6.Format(mVDate, "DD-MMM-YYYY"), Trim(mVNO), VB6.Format(mVDate, "DD-MMM-YYYY"), mBookType, "", Trim(pNarration), mBalAmount, Val(CStr(mBalAmount)), 0, 0, 0, "Y", "", 0, 0, 0) = False Then GoTo ErrDetail
        End If

        UpdateFixedAssetFromVoucher = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateFixedAssetFromVoucher = False
        'Resume						
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE ='F'"
        MainClass.SearchGridMaster(txtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            txtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub frmUpdAssetData_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmUpdAssetData_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo LErr
        MainClass.SetControlsColor(Me)
        txtAccount.Enabled = False
        cmdsearch.Enabled = False

        chkVNoAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtVNo.Enabled = False

        'Me.Height = VB6.TwipsToPixelsY(3800)
        'Me.Width = VB6.TwipsToPixelsX(6615)
        Me.Top = 0
        Me.Left = 0

        TxtDateFrom.Text = RsCompany.Fields("START_DATE").Value
        TxtDateTo.Text = CStr(RunDate)

        Exit Sub
LErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub OptAccount_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAccount.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptAccount.GetIndex(eventSender)
            txtAccount.Enabled = IIf(Index = 0, False, True)
            cmdsearch.Enabled = IIf(Index = 0, False, True)
        End If
    End Sub

    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAccount.DoubleClick
        SearchAccounts()
    End Sub

    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If txtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE ='F'"


        If MainClass.ValidateWithMasterTable(txtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtAccount.Text = UCase(Trim(txtAccount.Text))
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
    Private Function UpdateDNCNFixedAssets(ByRef mMkey As String, ByRef mFYear As Short, ByRef mVNO As String, ByRef mVDate As String, ByRef mBookType As String, ByRef mDebitAccountCode As String, ByRef mCreditAccountCode As String, ByRef mNETVALUE As Double, ByRef mCancelled As String, ByRef xPurNo As String, ByRef xPurDate As String) As Boolean
        On Error GoTo ErrPart
        Dim i As Short
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mISFixAssets As String
        Dim mAccountCode As String
        Dim mSupplierCode As String
        Dim mTRNType As String
        Dim mSupplierName As String

        Dim mMRRNo As Double
        Dim mMRRDate As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mItemValue As Double
        Dim mItemDesc As String
        Dim mDc As Double
        Dim mItemType As String
        Dim mTotItemValue As Double

        SqlStr = " DELETE FROM AST_ASSET_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND VMKEY='" & MainClass.AllowSingleQuote(mMkey) & "' " & vbCrLf _
            & " AND BOOKTYPE='" & MainClass.AllowSingleQuote(mBookType) & "' "


        PubDBCn.Execute(SqlStr)

        If mBookType = "E" Then ''DebitNoteBook						
            mSupplierCode = mDebitAccountCode
            mAccountCode = mCreditAccountCode
            mItemDesc = "Less : Debit Note"
            mDc = -1
        Else
            mAccountCode = mDebitAccountCode
            mSupplierCode = mCreditAccountCode
            mItemDesc = "Add : Credit Note"
            mDc = 1
        End If

        If MainClass.ValidateWithMasterTable(mAccountCode, "ACCOUNTPOSTCODE", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P' AND ISFIXASSETS='Y'") = True Then
            mTRNType = MasterNo
            mISFixAssets = "Y"
        Else
            mTRNType = "-1"
            mISFixAssets = "N"
        End If

        If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierName = MasterNo
        Else
            mSupplierName = ""
        End If

        If mCancelled = "N" And mISFixAssets = "Y" Then

            SqlStr = " SELECT MRR_REF_NO, MRR_REF_DATE," & vbCrLf _
                & " SUPP_REF_NO, SUPP_REF_DATE, " & vbCrLf _
                & " SUM(ITEM_AMT+ITEM_ED+ITEM_ST+CGST_AMOUNT+SGST_AMOUNT+IGST_AMOUNT) AS ITEM_AMT " & vbCrLf _
                & " FROM FIN_DNCN_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND MKEY='" & MainClass.AllowSingleQuote(mMkey) & "' " & vbCrLf _
                & " GROUP BY MRR_REF_NO, MRR_REF_DATE,SUPP_REF_NO, SUPP_REF_DATE" & vbCrLf _
                & " ORDER BY MRR_REF_NO, MRR_REF_DATE,SUPP_REF_NO, SUPP_REF_DATE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            mTotItemValue = 0
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False

                    mMRRNo = IIf(IsDBNull(RsTemp.Fields("MRR_REF_NO").Value), -1, RsTemp.Fields("MRR_REF_NO").Value)
                    mMRRDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("MRR_REF_DATE").Value), "", RsTemp.Fields("MRR_REF_DATE").Value), "DD/MM/YYYY")
                    mBillNo = IIf(IsDBNull(RsTemp.Fields("SUPP_REF_NO").Value), "", RsTemp.Fields("SUPP_REF_NO").Value)
                    mBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SUPP_REF_DATE").Value), "", RsTemp.Fields("SUPP_REF_DATE").Value), "DD/MM/YYYY")
                    mItemValue = IIf(IsDBNull(RsTemp.Fields("ITEM_AMT").Value), 0, RsTemp.Fields("ITEM_AMT").Value)
                    mTotItemValue = mTotItemValue + mItemValue
                    If UpdateAssetTRN(mMkey, mFYear, mTRNType, "N", "N", Val(CStr(mMRRNo)), VB6.Format(mMRRDate, "DD-MMM-YYYY"), Trim(mBillNo), VB6.Format(mBillDate, "DD-MMM-YYYY"), mVNO, VB6.Format(mVDate, "DD-MMM-YYYY"), mBookType, Trim(mSupplierName), Trim(mItemDesc), mItemValue * mDc, Val(CStr(mItemValue * mDc)), 0, 0, 0, "Y", "", 0, 0, 0) = False Then GoTo ErrPart

                    RsTemp.MoveNext()
                Loop
                If mTotItemValue < mNETVALUE Then
                    mItemValue = mNETVALUE - mTotItemValue
                    If UpdateAssetTRN(mMkey, mFYear, mTRNType, "N", "N", 0, VB6.Format(mVDate, "DD-MMM-YYYY"), "", "", mVNO, VB6.Format(mVDate, "DD-MMM-YYYY"), mBookType, Trim(mSupplierName), Trim(mItemDesc), mItemValue * mDc, Val(CStr(mItemValue * mDc)), 0, 0, 0, "Y", mItemType, 0, 0, 0) = False Then GoTo ErrPart
                End If
            Else
                SqlStr = " SELECT AUTO_KEY_MRR, MRRDATE, BILLNO, INVOICE_DATE" & vbCrLf & " FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND VNO='" & Trim(xPurNo) & "' " & vbCrLf _
                    & " AND VDATE=TO_DATE('" & VB6.Format(xPurDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    mMRRNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_MRR").Value), "", RsTemp.Fields("AUTO_KEY_MRR").Value)
                    mMRRDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("MRRDATE").Value), "", RsTemp.Fields("MRRDATE").Value), "DD/MM/YYYY")
                    mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                    mBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                Else
                    mBillNo = ""
                    mBillDate = ""
                End If
                mItemValue = mNETVALUE
                If UpdateAssetTRN(mMkey, mFYear, mTRNType, "N", "N", 0, VB6.Format(mVDate, "DD-MMM-YYYY"), Trim(mBillNo), VB6.Format(mBillDate, "DD-MMM-YYYY"), mVNO, VB6.Format(mVDate, "DD-MMM-YYYY"), mBookType, Trim(mSupplierName), Trim(mItemDesc), mItemValue * mDc, Val(CStr(mItemValue * mDc)), 0, 0, 0, "Y", mItemType, 0, 0, 0) = False Then GoTo ErrPart
            End If
        End If

        UpdateDNCNFixedAssets = True
        Exit Function
ErrPart:
        UpdateDNCNFixedAssets = False
    End Function


    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If TxtDateFrom.Text = "__/__/____" Then GoTo EventExitSub
        If IsDate(TxtDateFrom.Text) = False Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            TxtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If

        '    If FYChk(TxtDateFrom.Text) = False Then						
        '        Cancel = True						
        '        Exit Sub						
        '    End If						

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If TxtDateTo.Text = "__/__/____" Then GoTo EventExitSub
        If IsDate(TxtDateTo.Text) = False Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            TxtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If

        '    If FYChk(TxtDateFrom.Text) = False Then						
        '        Cancel = True						
        '        Exit Sub						
        '    End If						

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
