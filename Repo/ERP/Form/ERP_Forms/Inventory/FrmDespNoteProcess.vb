Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmDespNoteProcess
    Inherits System.Windows.Forms.Form
    'Private PvtDBCn As ADODB.Connection


    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdProcess.Click

        On Error GoTo ErrorHandler
        Dim mSuppCustCode As String
        Dim mItemCode As String
        Dim mDivisionCode As Double
        Dim mPONo As Double
        Dim mSqlStr As String
        Dim mTRNType As String
        Dim mFYear As Integer
        Dim RsTemp As ADODB.Recordset = Nothing

        '    MsgInformation "This is Under Process for GST, Please call Administrator"
        '    Exit Sub

        '    If OptAmendType(2).Value = True Then
        '        If PubSuperUser = "S" Or PubSuperUser = "A" Then
        '        Else
        '            MsgInformation "You have No rights to run this process under PO Amend."
        '            Exit Sub
        '        End If
        '    End If

        mFYear = GetCurrentFYNo(PubDBCn, VB6.Format(PubCurrDate, "DD/MM/YYYY"))

        If mFYear <> RsCompany.Fields("FYEAR").Value Then
            MsgInformation("Please login the current FYear.")
            Exit Sub
        End If

        mSuppCustCode = "-1"
        mItemCode = ""

        If chkSuppInvoice.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MsgQuestion("You not want to Generate Auto Supp Invoice, are you sure and want to start Process ...") = CStr(MsgBoxResult.No) Then
                Exit Sub
            End If
        Else
            If Trim(cboInvType.Text) = "" Then
                MsgInformation("Please Select the Invoice Type.")
                Exit Sub
            End If

            If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                mTRNType = MasterNo
            Else
                MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
                Exit Sub
            End If

        End If

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        Else
            mDivisionCode = -1
        End If


        If Trim(TxtAccount.Text) = "" Then
            MsgInformation("Please Enter Supplier Name...")
            Exit Sub
        End If
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            MsgInformation("No Such Supplier in Account Master")
            Exit Sub
        End If


        If OptItem(1).Checked = True Then
            If Trim(txtItem.Text) = "" Then
                MsgInformation("Please Enter Item Name...")
                Exit Sub
            End If
            If MainClass.ValidateWithMasterTable((txtItem.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
            Else
                MsgInformation("No Such Item in Item Master")
                Exit Sub
            End If
        End If


        mPONo = CDbl(Val(txtPONo.Text) & VB6.Format(Val(txtAmendNo.Text), "000"))

        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mSuppCustCode & "' AND SO_APPROVED='Y'"

        mSqlStr = " SELECT CUST_AMEND_NO, AMEND_WEF_FROM FROM DSP_SALEORDER_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & mSuppCustCode & "' AND SO_APPROVED='Y'" & vbCrLf & " AND MKEY=" & mPONo & " "

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtCustAmendDate.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMEND_WEF_FROM").Value), "", RsTemp.Fields("AMEND_WEF_FROM").Value), "DD-MMM-YYYY")
            txtCustAmendNo.Text = IIf(IsDbNull(RsTemp.Fields("CUST_AMEND_NO").Value), 0, RsTemp.Fields("CUST_AMEND_NO").Value)
        Else
            MsgInformation("Either SO is invalid or not Post.")
            Exit Sub
        End If

        '    If MainClass.ValidateWithMasterTable(mPONo, "MKEY", "MKEY", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , mSqlStr) = False Then
        '       MsgInformation "Either SO is invalid or not Post."
        '       Exit Sub
        '    End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If DespatchNoteProcess(mSuppCustCode, mItemCode, mDivisionCode, mTRNType) = True Then
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

    Private Function AutoGenSeqNo(ByRef mDivisionCode As Double) As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsDNMainGen As ADODB.Recordset
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mStartingSNo As Double
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMaxValue As String

        SqlStr = ""
        mStartingSNo = 1


        mSeparateSeries = IIf(IsDbNull(RsCompany.Fields("SEPARATE_DSP_SERIES").Value), "N", RsCompany.Fields("SEPARATE_DSP_SERIES").Value)

        SqlStr = "SELECT DSP_SERIES " & vbCrLf & " FROM INV_DIVISION_MST " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            '        mSeparateSeries = IIf(IsNull(RsTemp!SEPARATE_DSP_SERIES), "N", RsTemp!SEPARATE_DSP_SERIES)
            If mSeparateSeries = "Y" Then
                mStartingSNo = IIf(IsDbNull(RsTemp.Fields("DSP_SERIES").Value), 1, RsTemp.Fields("DSP_SERIES").Value)
                mStartingSNo = IIf(mStartingSNo = 0, 1, mStartingSNo)
            End If
        End If


        SqlStr = "SELECT Max(AUTO_KEY_DESP)  " & vbCrLf & " FROM DSP_DESPATCH_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""



        If mSeparateSeries = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        End If


        SqlStr = SqlStr & vbCrLf & " AND DESPATCHTYPE<>2"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsDNMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = mStartingSNo '' 1
                End If
            End If
        End With
        AutoGenSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function



    Private Function DespatchNoteProcess(ByRef mCustomerCode As String, ByRef pItemCode As String, ByRef mDivisionCode As Double, ByRef mTRNType As String) As Boolean

        On Error GoTo ErrPart

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsPO As ADODB.Recordset
        Dim mProcessKey As Double

        SqlStr = ""

        SqlStr = " SELECT IH.*, " & vbCrLf & " ID.SERIAL_NO, ID.SUPP_CUST_CODE, ID.ITEM_CODE, ID.UOM_CODE, ID.PART_NO,  " & vbCrLf & " ID.ITEM_PRICE, ID.PACK_TYPE, ID.COLOUR_DTL, CMST.SUPP_CUST_NAME as SUPP_CUST_NAME "


        SqlStr = SqlStr & vbCrLf & " FROM DSP_SALEORDER_HDR IH,DSP_SALEORDER_DET ID, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.Company_Code = CMST.Company_Code " & vbCrLf & " AND IH.SUPP_CUST_CODE = CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.AUTO_KEY_SO=" & Val(txtPONo.Text) & " AND IH.SO_APPROVED='Y'"

        SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & mCustomerCode & "' "

        If pItemCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' "
        End If

        If OptAmendType(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ID.IS_SUPP_GEN='N'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE IN ('F00842','F00897','F00438','F00439','F00419','F01207','F00833','F0848','F00481')"

        SqlStr = SqlStr & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AMEND_NO =" & Val(txtAmendNo.Text) & ""

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPO, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsPO.EOF Then
            If MsgQuestion("Are you want to start Process ...") = CStr(MsgBoxResult.No) Then
                DespatchNoteProcess = True
                Exit Function
            End If
            mProcessKey = MainClass.AutoGenRowNo("TEMP_DSP_DESPATCH", "RowNo", PubDBCn)
            If Update1(RsPO, mCustomerCode, mDivisionCode, mProcessKey) = False Then GoTo ErrPart

            If UpdateDespatchTable(mProcessKey, mTRNType) = False Then GoTo ErrPart


        End If
        DespatchNoteProcess = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)


        DespatchNoteProcess = True
        Exit Function
ErrPart:
        DespatchNoteProcess = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateDespatchTable(ByRef mProcessKey As Double, ByRef mTRNType As String) As Boolean

        On Error GoTo ErrPart

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTempDet As ADODB.Recordset
        Dim RsTempBill As ADODB.Recordset

        Dim mSqlStr As String

        Dim pAutoSONO As Double
        Dim pAutoSOAmendNo As Double
        Dim pItemCode As String
        Dim pNewPrice As Double
        Dim RsSuppPO As ADODB.Recordset
        Dim pWEFDate As String
        Dim pOldPrice As Double

        Dim mVNoSeq As Double
        Dim pAutoSODate As String
        Dim pCustPoNo As String
        Dim pCustPODate As String
        Dim mUnit As String
        Dim mPackQty As Double
        Dim mAutoKeyInvoice As String
        Dim mPreviousAutoKeyInvoice As Double
        Dim CntItemCode As Integer
        Dim mDespDate As String
        Dim mDespTime As String
        Dim mKey As Double
        Dim mDivisionCode As Double
        'Dim mDespTime As String

        Dim mDCDate As String
        Dim pSoDate As String
        Dim mSuppCustCode As String
        Dim mDespRef As String

        Dim mCurRowNo As Integer
        Dim nMkey As String
        Dim mBillNoSeq As Double
        Dim mBillNo As String
        Dim mOBillNo As String
        Dim mOBillDate As String

        Dim mAutoKeyNo As String
        Dim mBookSubType As String
        Dim mBookType As String
        Dim mAccountCode As String
        Dim mStockTrf As String

        Dim mItemValue As Double
        Dim mTOTEXPAMT As Double
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mTOTTAXABLEAMOUNT As Double
        Dim mTotCGSTAmount As Double
        Dim mTotSGSTAmount As Double
        Dim mTotIGSTAmount As Double
        Dim mShippedToCode As String

        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mSameGSTNo As String
        Dim mItemDesc As String
        Dim mHSNCode As String
        Dim mPartNo As String
        Dim mAmount As Double
        Dim mTaxableAmount As Double

        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double

        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mSuppRate As Double

        Dim RSExp As ADODB.Recordset
        Dim mExpSeq As Integer
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim mCalcOn As Double
        Dim mRO As String
        Dim mDutyForgone As String

        Dim mInvoiceSeq As Integer
        Dim mOriginalBillDate As String

        '    PubDBCn.Errors.Clear
        '    PubDBCn.BeginTrans

        SqlStr = "SELECT DISTINCT IH.MKEY, ID.REF_NO, ID.REF_DATE, IH.DIV_CODE, IH.AUTO_KEY_SO, " & vbCrLf & " IH.DESP_DATE, IH.SO_DATE, IH.VENDOR_PO, IH.VENDOR_PO_DATE, IH.SUPP_CUST_CODE, IH.DESP_TYPE " & vbCrLf & " FROM TEMP_DSP_DESPATCH_HDR IH, TEMP_DSP_DESPATCH_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY AND IH.MKEY=" & mProcessKey & "" & vbCrLf & " AND IH.USERID = '" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " ORDER BY ID.REF_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                mKey = RsTemp.Fields("mKey").Value
                mAutoKeyInvoice = RsTemp.Fields("REF_NO").Value
                mOriginalBillDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value), "DD/MM/YYYY")
                mDivisionCode = RsTemp.Fields("DIV_CODE").Value
                pAutoSONO = RsTemp.Fields("AUTO_KEY_SO").Value

                mVNoSeq = CDbl(AutoGenSeqNo(mDivisionCode))
                mDespTime = GetServerTime



                mDCDate = RsTemp.Fields("DESP_DATE").Value

                pSoDate = IIf(IsDbNull(RsTemp.Fields("SO_DATE").Value), "", RsTemp.Fields("SO_DATE").Value)
                pCustPoNo = IIf(IsDbNull(RsTemp.Fields("VENDOR_PO").Value), "", RsTemp.Fields("VENDOR_PO").Value)
                pCustPODate = IIf(IsDbNull(RsTemp.Fields("VENDOR_PO_DATE").Value), "", RsTemp.Fields("VENDOR_PO_DATE").Value)
                mSuppCustCode = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)

                mDespRef = IIf(IsDbNull(RsTemp.Fields("DESP_TYPE").Value), "", RsTemp.Fields("DESP_TYPE").Value)

                Dim mLocationID As String = GetDefaultLocation(mSuppCustCode)

                mLocal = "N"
                If Trim(mSuppCustCode) <> "" Then
                    If MainClass.ValidateWithMasterTable(Trim(mSuppCustCode), "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mLocal = Trim(MasterNo)
                    End If
                End If

                mPartyGSTNo = ""
                If MainClass.ValidateWithMasterTable(Trim(mSuppCustCode), "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPartyGSTNo = MasterNo
                End If

                mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")

                SqlStr = "INSERT INTO DSP_DESPATCH_HDR( " & vbCrLf & " COMPANY_CODE, AUTO_KEY_DESP, DESP_DATE," & vbCrLf & " SUPP_CUST_CODE, " & vbCrLf & " TRANSPORTER_NAME, VEHICLE_NO," & vbCrLf & " LOADING_TIME, PRE_EMP_CODE, " & vbCrLf & " DESP_STATUS, DESP_TYPE, " & vbCrLf & " AUTO_KEY_SO, SO_DATE, " & vbCrLf & " VENDOR_PO, VENDOR_PO_DATE, " & vbCrLf & " GRNO,GRDATE," & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE, DIV_CODE, DESPATCHTYPE) "

                SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT COMPANY_CODE, " & mVNoSeq & ", DESP_DATE," & vbCrLf & " SUPP_CUST_CODE, " & vbCrLf & " '', ''," & vbCrLf & " TO_DATE('" & mDespTime & "','HH24:MI'), '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " DESP_STATUS, DESP_TYPE, " & vbCrLf & " AUTO_KEY_SO, SO_DATE, " & vbCrLf & " VENDOR_PO, VENDOR_PO_DATE, " & vbCrLf & " '',''," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','', DIV_CODE, DESPATCHTYPE" & vbCrLf & " FROM TEMP_DSP_DESPATCH_HDR" & vbCrLf & " WHERE MKEY=" & mKey & " AND AUTO_KEY_SO=" & pAutoSONO & "" & vbCrLf & " AND USERID = '" & MainClass.AllowSingleQuote(PubUserID) & "' "


                PubDBCn.Execute(SqlStr)

                ''Generate Invoice Also...


                If chkSuppInvoice.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mInvoiceSeq = IIf(mSameGSTNo = "Y", 5, 9)

                    mCurRowNo = MainClass.AutoGenRowNo("FIN_INVOICE_HDR", "RowNo", PubDBCn)
                    nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo
                    mBookType = "S"
                    If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
                        mBookSubType = MasterNo
                    Else
                        mBookSubType = ""
                    End If

                    mBillNoSeq = CDbl(AutoGenSeqBillNo(mBookType, mBookSubType, 1, mDivisionCode, mInvoiceSeq))
                    mBillNoSeq = CDbl(VB6.Format(Val(CStr(mBillNoSeq)), "00000000"))


                    mBillNo = Trim("S" & VB6.Format(Val(CStr(mBillNoSeq)), "00000000") & Trim(""))
                    mAutoKeyNo = VB6.Format(Val(CStr(mBillNoSeq)), "00000000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")

                    If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "ACCOUNTPOSTCODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
                        mAccountCode = MasterNo
                    Else
                        mAccountCode = "-1"
                    End If

                    If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "ISSTOCKTRF", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
                        mStockTrf = MasterNo
                    Else
                        mStockTrf = "N"
                    End If

                    mItemValue = 0
                    mTOTEXPAMT = 0
                    mNETVALUE = 0
                    mTotQty = 0
                    mTOTTAXABLEAMOUNT = 0
                    mTotCGSTAmount = 0
                    mTotSGSTAmount = 0
                    mTotIGSTAmount = 0
                    mShippedToCode = mSuppCustCode


                    SqlStr = "INSERT INTO FIN_INVOICE_HDR (" & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " ROWNO, TRNTYPE, BILLNOPREFIX, " & vbCrLf & " AUTO_KEY_INVOICE, BILLNOSEQ, BILLNOSUFFIX, BILLNO, " & vbCrLf & " INVOICE_DATE, INV_PREP_DATE, INV_PREP_TIME, " & vbCrLf & " AUTO_KEY_DESP, DCDATE, CUST_PO_NO, CUST_PO_DATE, " & vbCrLf & " AMEND_NO, AMEND_DATE, AMEND_WEF_FROM, REMOVAL_DATE, " & vbCrLf & " REMOVAL_TIME, SUPP_CUST_CODE, ACCOUNTCODE, ST_38_NO, " & vbCrLf & " DUEDAYSFROM, DUEDAYSTO, AUTHSIGN, AUTHDATE, " & vbCrLf & " GRNO, GRDATE, DESPATCHMODE, DOCSTHROUGH, " & vbCrLf & " VEHICLENO, CARRIERS, FREIGHTCHARGES, " & vbCrLf & " TARIFFHEADING, EXEMPT_NOTIF_NO, " & vbCrLf & " BOOKCODE,BOOKTYPE, BOOKSUBTYPE, SALETAXCODE, " & vbCrLf & " REMARKS, ITEMDESC, ITEMVALUE, " & vbCrLf & " TOTSTAMT, TOTCHARGES, TOTEDAMOUNT, " & vbCrLf & " TOTEXPAMT, NETVALUE, TOTQTY, "

                    SqlStr = SqlStr & vbCrLf & " STFORMCODE, STFORMNAME, STFORMNO, STFORMDATE, " & vbCrLf & " STDUEFORMCODE, STDUEFORMNAME, STDUEFORMNO, STDUEFORMDATE,  " & vbCrLf & " STTYPE, IsRegdNo,LSTCST, WITHFORM, FOC, PRINTED," & vbCrLf & " CANCELLED, NARRATION,  " & vbCrLf & " STPERCENT, TOTFREIGHT, EDPERCENT, TOTTAXABLEAMOUNT, " & vbCrLf & " TOTSURCHARGEAMT, TOTDISCAMOUNT, TOTMSCAMOUNT, TotRO,REJECTION,AGTD3, " & vbCrLf & " PACK_MAT_FLAG, CHALLAN_MADE,PRDDate, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE,ISSTOCKTRF,TCSPER, TCSAMOUNT,DNCNNO,DNCNDATE," & vbCrLf & " TOTEDUPERCENT,TOTEDUAMOUNT,TOTSERVICEPERCENT,TOTSERVICEAMOUNT,SERV_PROV," & vbCrLf & " SUPP_FROM_DATE, SUPP_TO_DATE, INTRATE, " & vbCrLf & " AGTCT3, CT_NO, CT3_DATE, ARE_NO, " & vbCrLf & " REF_DESP_TYPE, OUR_AUTO_KEY_SO, OUR_SO_DATE, "

                    SqlStr = SqlStr & vbCrLf & " SHIPPING_NO, SHIPPING_DATE, " & vbCrLf & " ARE1_NO, ARE1_DATE, " & vbCrLf & " PORT_CODE, EXPBILLNO, EXPINV_DATE, TOT_EXPORTEXP,EXCHANGE_RATE, " & vbCrLf & " TOTEXCHANGEVALUE, ADV_LICENSE, DESP_LOCATION, NATURE," & vbCrLf & " TOTMRPVALUE, TAX_ON_MRP, ABATEMENT_PER, " & vbCrLf & " TOT_CUSTOMDUTY, TOT_CD_CESS, CD_PER, CD_CESS_PER, BUYER_CODE, CO_BUYER_CODE," & vbCrLf & " TOTSHECPERCENT, TOTSHECAMOUNT,UPDATE_FROM,ISDUTY_FORGONE, AGT_DUTYFREE_PUR," & vbCrLf & " DUTY_INCLUDED_ITEM, ED_PAYABLE, CESS_PAYABLE, SHEC_PAYABLE,DIV_CODE, " & vbCrLf & " AGTCT1, CT1_NO, CT1_DATE,AGT_Permission,CUST_ITEM_VALUE, " & vbCrLf & " NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT," & vbCrLf & " SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE,E_REFNO,INVOICESEQTYPE,SAC_CODE," & vbCrLf & " ADV_VNO, ADV_VDATE, ADV_ADJUSTED_AMT, " & vbCrLf & " ADV_CGST_AMT, ADV_SGST_AMT, ADV_IGST_AMT,ADV_ITEM_AMT " & vbCrLf & " )"

                    SqlStr = SqlStr & vbCrLf & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mCurRowNo & "," & Val(mTRNType) & ", 'S', " & vbCrLf & " " & mAutoKeyNo & "," & mBillNoSeq & ", '', '" & MainClass.AllowSingleQuote(mBillNo) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & mDespTime & "','HH24:MI')," & vbCrLf & " " & Val(CStr(mVNoSeq)) & ", TO_DATE('" & VB6.Format(mDCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(pCustPoNo) & "', TO_DATE('" & VB6.Format(pCustPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & Val(txtCustAmendNo.Text) & " ,'',TO_DATE('" & VB6.Format(txtCustAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " TO_DATE('" & mDespTime & "','HH24:MI'),'" & mSuppCustCode & "','" & mAccountCode & "','', " & vbCrLf & " 0, 0, '', '', " & vbCrLf & " '', '', '', '', " & vbCrLf & " '', '', '', " & vbCrLf & " '', '', " & vbCrLf & " '" & ConSalesBookCode & "', '" & mBookType & "', '" & mBookSubType & "', -1, " & vbCrLf & " '', '', " & mItemValue & ", " & vbCrLf & " 0, 0, 0, " & vbCrLf & " " & mTOTEXPAMT & ", " & mNETVALUE & ", " & mTotQty & ", " & vbCrLf & " -1, '','', '', " & vbCrLf & " -1, '','', '', " & vbCrLf & " '','N', '', " & vbCrLf & " '', 'N', 'N', " & vbCrLf & " 'N', '',  "

                    SqlStr = SqlStr & vbCrLf & " 0,0,0," & mTOTTAXABLEAMOUNT & "," & vbCrLf & " 0,0,0,0,'N','N', " & vbCrLf & " '','','', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','" & mStockTrf & "'," & vbCrLf & " 0,0," & vbCrLf & " ''," & vbCrLf & " '', " & vbCrLf & " 0, 0," & vbCrLf & " 0,0,''," & vbCrLf & " '', ''," & vbCrLf & " 0, 'N', 0, '',  0," & vbCrLf & " '" & mDespRef & "', " & Val(CStr(pAutoSONO)) & ", TO_DATE('" & VB6.Format(pSoDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), "

                    SqlStr = SqlStr & vbCrLf & " '', '', " & vbCrLf & " '', '', " & vbCrLf & " '', '', ''," & vbCrLf & " 0,0, " & vbCrLf & " 0, '', " & vbCrLf & " '', ''," & vbCrLf & " 0, 'N', 0, " & vbCrLf & " 0 , 0, 0, 0, " & vbCrLf & " '', ''," & vbCrLf & " 0, 0,'N','','', " & vbCrLf & " '', 0, 0, 0," & mDivisionCode & "," & vbCrLf & " '',0, '','N',0," & vbCrLf & " " & Val(CStr(mTotCGSTAmount)) & "," & Val(CStr(mTotSGSTAmount)) & "," & Val(CStr(mTotIGSTAmount)) & "," & vbCrLf & " 'Y','" & mShippedToCode & "',''," & mInvoiceSeq & ",''," & vbCrLf & " '', " & vbCrLf & " '', 0, " & vbCrLf & " 0, 0, 0, 0 " & vbCrLf & " )"

                    PubDBCn.Execute(SqlStr)
                End If


                SqlStr = " SELECT " & vbCrLf & " ITEM_CODE," & vbCrLf & " ITEM_UOM, STOCK_TYPE, PACKED_QTY," & vbCrLf & " NO_OF_PACKETS, PDIR_NO, REF_NO," & vbCrLf & " MRR_REF_NO, COMPANY_CODE, " & vbCrLf & " SONO, SODATE," & vbCrLf & " CUST_PO, CUST_PO_DATE, LOT_NO,JITCALLNO,SUPP_RATE " & vbCrLf & " FROM TEMP_DSP_DESPATCH_DET" & vbCrLf & " WHERE MKEY=" & mKey & " AND REF_NO=" & mAutoKeyInvoice & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTempDet.EOF = False Then
                    CntItemCode = 1
                    Do While RsTempDet.EOF = False

                        pItemCode = IIf(IsDbNull(RsTempDet.Fields("ITEM_CODE").Value), "", RsTempDet.Fields("ITEM_CODE").Value)
                        mUnit = IIf(IsDbNull(RsTempDet.Fields("ITEM_UOM").Value), "", RsTempDet.Fields("ITEM_UOM").Value)
                        mPackQty = IIf(IsDbNull(RsTempDet.Fields("PACKED_QTY").Value), "", RsTempDet.Fields("PACKED_QTY").Value)
                        mSuppRate = IIf(IsDbNull(RsTempDet.Fields("SUPP_RATE").Value), 0, RsTempDet.Fields("SUPP_RATE").Value)


                        '                    mOriginalBillDate = ""
                        '
                        '                    If MainClass.ValidateWithMasterTable(mAutoKeyInvoice, "AUTO_KEY_INVOICE", "INVOICE_DATE", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                        '                        mOriginalBillDate = MasterNo
                        '                    End If

                        SqlStr = " INSERT INTO DSP_DESPATCH_DET ( " & vbCrLf & " AUTO_KEY_DESP, SERIAL_NO, ITEM_CODE," & vbCrLf & " ITEM_UOM, STOCK_TYPE, PACKED_QTY," & vbCrLf & " NO_OF_PACKETS, PDIR_NO, REF_NO, REF_DATE, " & vbCrLf & " MRR_REF_NO, COMPANY_CODE, " & vbCrLf & " SONO, SODATE," & vbCrLf & " CUST_PO, CUST_PO_DATE, LOT_NO,JITCALLNO) "


                        SqlStr = SqlStr & vbCrLf & " VALUES ('" & mVNoSeq & "', " & CntItemCode & " ,'" & pItemCode & "', " & vbCrLf & " '" & mUnit & "','FG'," & mPackQty & ", " & vbCrLf & " 0, '', '" & mAutoKeyInvoice & "', TO_DATE('" & VB6.Format(mOriginalBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " 0, " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " 0, ''," & vbCrLf & " '', '', 0,'') "

                        PubDBCn.Execute(SqlStr)


                        If chkSuppInvoice.CheckState = System.Windows.Forms.CheckState.Checked Then

                            If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                                mItemDesc = MasterNo
                            Else
                                mItemDesc = ""
                            End If

                            mItemDesc = MainClass.AllowSingleQuote(mItemDesc)
                            If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                                mPartNo = MasterNo
                            Else
                                mPartNo = ""
                            End If

                            mHSNCode = GetHSNCode(pItemCode)

                            mAmount = mPackQty * mSuppRate
                            mTaxableAmount = mAmount

                            mCGSTPer = 0
                            mSGSTPer = 0
                            mIGSTPer = 0

                            If GetHSNDetails(mHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, "0", mPartyGSTNo) = False Then GoTo ErrPart

                            mCGSTAmount = CDbl(VB6.Format(mCGSTPer * mTaxableAmount / 100, "0.00"))
                            mSGSTAmount = CDbl(VB6.Format(mSGSTPer * mTaxableAmount / 100, "0.00"))
                            mIGSTAmount = CDbl(VB6.Format(mIGSTPer * mTaxableAmount / 100, "0.00"))

                            mItemValue = mItemValue + mAmount
                            mTOTEXPAMT = 0
                            mNETVALUE = mNETVALUE + (mAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount)
                            mTotQty = mTotQty + mPackQty
                            mTOTTAXABLEAMOUNT = mTOTTAXABLEAMOUNT + mAmount
                            mTotCGSTAmount = mTotCGSTAmount + mCGSTAmount
                            mTotSGSTAmount = mTotSGSTAmount + mSGSTAmount
                            mTotIGSTAmount = mTotIGSTAmount + mIGSTAmount


                            SqlStr = " INSERT INTO FIN_INVOICE_DET ( " & vbCrLf & " MKEY , AUTO_KEY_INVOICE, SUBROWNO, " & vbCrLf & " ITEM_CODE , ITEM_DESC, HSNCODE, CUSTOMER_PART_NO,ITEM_QTY, " & vbCrLf & " ITEM_UOM , ITEM_RATE, ITEM_AMT, GSTABLE_AMT," & vbCrLf & " ITEM_ED, ITEM_ST,ITEM_CESS,ITEM_SERVICE, " & vbCrLf & " COMPANY_CODE,ITEM_MRP,ITEM_SHEC,JIT_CALLNO, " & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT " & vbCrLf & " ) "

                            SqlStr = SqlStr & vbCrLf & " VALUES ('" & nMkey & "'," & mAutoKeyNo & ", " & CntItemCode & ", " & vbCrLf & " '" & pItemCode & "','" & MainClass.AllowSingleQuote(mItemDesc) & "', '" & mHSNCode & "', '" & mPartNo & "'," & mPackQty & ", " & vbCrLf & " '" & mUnit & "'," & mSuppRate & "," & mAmount & ", " & mTaxableAmount & "," & vbCrLf & " 0, 0, 0, " & vbCrLf & " 0, " & RsCompany.Fields("COMPANY_CODE").Value & ",0, " & vbCrLf & " 0 ,''," & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & "," & vbCrLf & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ") "

                            PubDBCn.Execute(SqlStr)

                            SqlStr = "SELECT BILLNO, INVOICE_DATE FROM FIN_INVOICE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_INVOICE=" & mAutoKeyInvoice & ""
                            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempBill, ADODB.LockTypeEnum.adLockReadOnly)


                            If RsTempBill.EOF = False Then
                                mOBillNo = IIf(IsDbNull(RsTempBill.Fields("BILLNO").Value), "", RsTempBill.Fields("BILLNO").Value)
                                mOBillDate = IIf(IsDbNull(RsTempBill.Fields("INVOICE_DATE").Value), "", RsTempBill.Fields("INVOICE_DATE").Value)
                            End If

                            If UpdateGSTTRN(PubDBCn, nMkey, CStr(ConSalesBookCode), mBookType, mBookSubType, mBillNo, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mBillNo, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mOBillNo, mOBillDate, mSuppCustCode, mAccountCode, "Y", mSuppCustCode, CntItemCode, pItemCode, mPackQty, mUnit, mSuppRate, mAmount, mTaxableAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, mDivisionCode, mHSNCode, MainClass.AllowSingleQuote(mItemDesc), "", "N", "U", "G", "N", "D", VB6.Format(PubCurrDate, "DD/MM/YYYY"), "N") = False Then GoTo ErrPart

                        End If
                        RsTempDet.MoveNext()
                        CntItemCode = CntItemCode + 1
                    Loop

                    If chkSuppInvoice.CheckState = System.Windows.Forms.CheckState.Checked Then

                        SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf & " ITEMVALUE=" & mItemValue & ", TOTEXPAMT = " & mTOTEXPAMT & ", " & vbCrLf & " NETVALUE = " & mNETVALUE & ", TOTQTY = " & mTotQty & ", TOTTAXABLEAMOUNT = " & mTOTTAXABLEAMOUNT & "," & vbCrLf & " NETCGST_AMOUNT = " & mTotCGSTAmount & ", NETSGST_AMOUNT = " & mTotSGSTAmount & ", " & vbCrLf & " NETIGST_AMOUNT=" & mTotIGSTAmount & "" & vbCrLf & " WHERE MKEY = '" & nMkey & "'"
                        PubDBCn.Execute(SqlStr)

                        SqlStr = " SELECT * FROM FIN_INTERFACE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND (Type='S' OR Type='B') " & vbCrLf & " AND GST_ENABLED='Y'" & vbCrLf & " ORDER BY PRINTSEQUENCE"

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSExp, ADODB.LockTypeEnum.adLockReadOnly)
                        mExpSeq = 0
                        If RSExp.EOF = False Then
                            Do While RSExp.EOF = False
                                mExpSeq = mExpSeq + 1
                                mExpCode = IIf(IsDbNull(RSExp.Fields("Code").Value), -1, RSExp.Fields("Code").Value)
                                mPercent = 0
                                mCalcOn = 0 ''IIf(IsNull(RSExp!CALCON), -1, RSExp!CALCON)
                                mRO = IIf(IsDbNull(RSExp.Fields("ROUNDOFF").Value), "N", RSExp.Fields("ROUNDOFF").Value)
                                mDutyForgone = "N" ''IIf(IsNull(RSExp!DUTYFORGONE), "N", RSExp!DUTYFORGONE)

                                mExpAmount = 0

                                If RSExp.Fields("Identification").Value = "CGS" Then
                                    mCalcOn = mTOTTAXABLEAMOUNT
                                    mExpAmount = mTotCGSTAmount
                                ElseIf RSExp.Fields("Identification").Value = "SGS" Then
                                    mCalcOn = mTOTTAXABLEAMOUNT
                                    mExpAmount = mTotSGSTAmount
                                ElseIf RSExp.Fields("Identification").Value = "IGS" Then
                                    mCalcOn = mTOTTAXABLEAMOUNT
                                    mExpAmount = mTotIGSTAmount
                                End If

                                SqlStr = "INSERT INTO FIN_INVOICE_EXP (" & vbCrLf & " MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DUTYFORGONE) " & vbCrLf & " VALUES ('" & nMkey & "'," & mExpSeq & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & "," & vbCrLf & " " & mCalcOn & ",'" & mRO & "','" & mDutyForgone & "')"

                                PubDBCn.Execute(SqlStr)
                                RSExp.MoveNext()
                            Loop
                        End If

                        If SalePostTRN_GST(PubDBCn, nMkey, mCurRowNo, CStr(ConSalesBookCode), mBookType, mBookSubType, mBillNo, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mNETVALUE)) - IIf(mSameGSTNo = "Y", Val(CStr(mTotCGSTAmount)) + Val(CStr(mTotSGSTAmount)) + Val(CStr(mTotIGSTAmount)), 0), False, VB6.Format(PubCurrDate, "DD/MM/YYYY"), False, "", False, "", 0, 0, IIf(mSameGSTNo = "Y", 0, Val(CStr(mTotCGSTAmount))), IIf(mSameGSTNo = "Y", 0, Val(CStr(mTotIGSTAmount))), IIf(mSameGSTNo = "Y", 0, Val(CStr(mTotSGSTAmount))), True, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), Val(CStr(mItemValue)), mDivisionCode, "N", 0, 0, 0, mLocationID) = False Then GoTo ErrPart


                    End If
                End If

                PubDBCn.CommitTrans()

                RsTemp.MoveNext()

            Loop
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " DELETE FROM TEMP_DSP_DESPATCH_DET WHERE MKEY=" & mProcessKey & ""
        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM TEMP_DSP_DESPATCH_HDR WHERE MKEY=" & mProcessKey & ""
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        UpdateDespatchTable = True
        Exit Function
ErrPart:
        Resume
        MsgInformation(Err.Description)
        UpdateDespatchTable = False
        PubDBCn.RollbackTrans()

    End Function
    Private Function AutoGenSeqBillNo(ByRef mBookType As String, ByRef mBookSubType As String, ByRef pStartingSNo As Double, ByRef mDivisionCode As Double, ByRef mInvoiceSeq As Integer) As String

        On Error GoTo AutoGenSeqBillNoErr
        Dim RsSaleMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Integer
        Dim mStartingSNo As Double
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim xFYear As Integer
        Dim mPrefix As Double
        Dim mMaxValue As String
        Dim mSeqNo As Double

        SqlStr = ""

        xFYear = CInt(VB6.Format(RsCompany.Fields("Start_Date").Value, "YY"))

        mPrefix = CDbl(xFYear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & Val(mInvoiceSeq))

        'If RsCompany.Fields("FYEAR").Value >= 2020 Then
        '    mStartingSNo = CDbl(xFYear & VB6.Format(IIf(IsDbNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & mInvoiceSeq & VB6.Format(pStartingSNo, "00000"))
        'Else
        '    mStartingSNo = CDbl(VB6.Format(IIf(IsDbNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & mInvoiceSeq & VB6.Format(pStartingSNo, "00000"))
        'End If

        mStartingSNo = pStartingSNo

        SqlStr = ""


        SqlStr = "SELECT Max(BILLNOSEQ)  FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BookType='" & mBookType & "'" ''& vbCrLf |            & " AND BookSubType  IN ( "

        SqlStr = SqlStr & vbCrLf & " AND INVOICESEQTYPE=" & mInvoiceSeq & ""

        SqlStr = SqlStr & vbCrLf & " AND INVOICE_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSaleMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mSeqNo = Mid(mMaxValue, 6, Len(mMaxValue) - 5) + 1
                    'mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mSeqNo = mStartingSNo
                    'mNewSeqBillNo = mStartingSNo
                End If
            Else
                mSeqNo = mStartingSNo
                'mNewSeqBillNo = mStartingSNo
            End If
        End With

        mNewSeqBillNo = mPrefix & IIf(RsCompany.Fields("INVOICE_DIGIT").Value = 1, mSeqNo, Format(mSeqNo, "00000"))

        '    mNewSeqBillNo = ""

        ''& vb6.Format(IIf(IsNull(RsCompany!INVOICE_PREFIX), "", RsCompany!INVOICE_PREFIX), "00") & Val(lblInvoiceSeq.text)

        AutoGenSeqBillNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Sub FillCboSaleType()

        On Error GoTo ErrPart
        Dim RsSaleType As ADODB.Recordset
        Dim SqlStr As String = ""

        cboInvType.Items.Clear()

        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='S' AND ISSALERETURN='N' AND IDENTIFICATION<>'P' "


        SqlStr = SqlStr & vbCrLf & " AND ISSUPPBILL='Y'"

        SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION NOT IN ('S','G')"


        SqlStr = SqlStr & vbCrLf & " ORDER BY NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleType, ADODB.LockTypeEnum.adLockReadOnly)

        If RsSaleType.EOF = False Then
            Do While Not RsSaleType.EOF
                cboInvType.Items.Add(RsSaleType.Fields("NAME").Value)
                RsSaleType.MoveNext()
            Loop
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function Update1(ByRef RsPO As ADODB.Recordset, ByRef pCustomerCode As String, ByRef mDivisionCode As Double, ByRef mProcessKey As Double) As Boolean

        On Error GoTo ErrPart

        Dim mSqlStr As String
        Dim SqlStr As String = ""
        Dim pAutoSONO As Double
        Dim pAutoSOAmendNo As Double
        Dim pItemCode As String
        Dim pNewPrice As Double
        Dim RsSuppPO As ADODB.Recordset
        Dim RsRate As ADODB.Recordset
        Dim pWEFDate As String
        Dim pOldPrice As Double

        'Dim mVNoSeq As Double
        Dim pAutoSODate As String
        Dim pCustPoNo As String
        Dim pCustPODate As String
        Dim mUnit As String
        Dim mPackQty As Double
        Dim mAutoKeyInvoice As String
        Dim mSaleReturnQty As Double

        Dim CntItemCode As Integer
        Dim mDespDate As String
        Dim mDespTime As String
        Dim mRate As Double
        Dim mSuppRate As Double
        Dim mDespStatus As String
        Dim mRefDate As String
        Dim mPORate As String
        Dim pSOMKey As Double

        If RsPO.EOF Then Exit Function
        RsPO.MoveFirst()

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mDespDate = CStr(PubCurrDate)
        mDespTime = GetServerTime

        CntItemCode = 1
        Do While RsPO.EOF = False
            pSOMKey = IIf(IsDbNull(RsPO.Fields("mKey").Value), -1, RsPO.Fields("mKey").Value)
            pAutoSONO = IIf(IsDbNull(RsPO.Fields("AUTO_KEY_SO").Value), -1, RsPO.Fields("AUTO_KEY_SO").Value)
            pAutoSOAmendNo = IIf(IsDbNull(RsPO.Fields("AMEND_NO").Value), -1, RsPO.Fields("AMEND_NO").Value)

            pAutoSODate = VB6.Format(IIf(IsDbNull(RsPO.Fields("SO_DATE").Value), "", RsPO.Fields("SO_DATE").Value), "DD/MM/YYYY")
            pCustPoNo = IIf(IsDbNull(RsPO.Fields("CUST_PO_NO").Value), "", RsPO.Fields("CUST_PO_NO").Value)
            pCustPODate = VB6.Format(IIf(IsDbNull(RsPO.Fields("CUST_PO_DATE").Value), "", RsPO.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")



            pAutoSOAmendNo = pAutoSOAmendNo - 1
            pItemCode = Trim(IIf(IsDbNull(RsPO.Fields("ITEM_CODE").Value), "", RsPO.Fields("ITEM_CODE").Value))

            pNewPrice = IIf(IsDbNull(RsPO.Fields("ITEM_PRICE").Value), 0, RsPO.Fields("ITEM_PRICE").Value)
            pWEFDate = IIf(IsDbNull(RsPO.Fields("AMEND_WEF_FROM").Value), "", RsPO.Fields("AMEND_WEF_FROM").Value)
            pOldPrice = GetSaleOldPrice(pAutoSONO, pAutoSOAmendNo, pCustomerCode, pItemCode)

            If OptAmendType(2).Checked = True Then
                If pNewPrice = pOldPrice Then GoTo NextRec
            End If

            mSqlStr = "SELECT IH.INVOICE_DATE, " & vbCrLf & " IH.AUTO_KEY_INVOICE, ID.ITEM_CODE, ID.ITEM_UOM AS UOM_CODE, " & vbCrLf & " SUM(ID.ITEM_QTY) AS ITEM_QTY " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'" & vbCrLf & " AND IH.REF_DESP_TYPE<>'U' AND IH.CANCELLED='N'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

            If OptAmendType(0).Checked = True Then
                mSqlStr = mSqlStr & vbCrLf & " AND ID.ITEM_RATE -GETSALEDEBITRATE(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.FYEAR,IH.MKEY, '" & Trim(pCustomerCode) & "', ID.ITEM_CODE) + GETSALESUPPBILLPRICE(" & RsCompany.Fields("COMPANY_CODE").Value & ", ID.ITEM_CODE, '" & Trim(pCustomerCode) & "',IH.AUTO_KEY_INVOICE) <" & pNewPrice & "" ''09-05-2019
            ElseIf OptAmendType(2).Checked = True Then
                mSqlStr = mSqlStr & vbCrLf & " AND " & pOldPrice & " <" & pNewPrice & "" ''10-04-2020
            Else
                mSqlStr = mSqlStr & vbCrLf & " AND ID.ITEM_RATE -GETSALEDEBITRATE(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.FYEAR,IH.MKEY, '" & Trim(pCustomerCode) & "', ID.ITEM_CODE) + GETSALESUPPBILLPRICE(" & RsCompany.Fields("COMPANY_CODE").Value & ", ID.ITEM_CODE, '" & Trim(pCustomerCode) & "',IH.AUTO_KEY_INVOICE) <GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE) " ''" & pNewPrice & "  09-05-2019
            End If

            If chkSaleReturn.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mSqlStr = mSqlStr & vbCrLf & " AND AGTD3='N'"
            End If

            '        mSqlStr = mSqlStr & vbCrLf & " AND BILLNO IN ('S01103870','S01103896','S01102292','S01102451')"

            '       mSqlStr = mSqlStr & vbCrLf & " AND BILLNO IN ( 'S01105206', 'S01107860', 'S01106345', 'S01110839', 'S01105773', 'S01109210', 'S01107158', 'S01112183', 'S01105530', 'S01108277', 'S01106605', 'S01111532', 'S01105980', 'S01109701', 'S01107546', 'S01112882',  " & vbCrLf _
            ''        & "'S01105233', 'S01107944', 'S01106352', 'S01110887', 'S01105827', 'S01109259', 'S01107241', 'S01112253', 'S01105558', 'S01108289', 'S01106642', 'S01111580', 'S01106038', 'S01109753', 'S01107583', 'S01112898',  " & vbCrLf _
            ''        & "'S01105245', 'S01108065', 'S01106432', 'S01111072', 'S01105828', 'S01109280', 'S01107265', 'S01112254', 'S01105598', 'S01108370', 'S01106667', 'S01111643', 'S01106050', 'S01109907', 'S01107592', 'S01112963',  " & vbCrLf _
            ''        & "'S01105275', 'S01108144', 'S01106444', 'S01111108', 'S01105829', 'S01109297', 'S01107273', 'S01112288', 'S01105646', 'S01108450', 'S01106700', 'S01111715', 'S01106092', 'S01110086', 'S01107600', 'S01113041',  " & vbCrLf _
            ''        & "'S01105291', 'S01108145', 'S01106458', 'S01111290', 'S01105838', 'S01109321', 'S01107283', 'S01112289', 'S01105656', 'S01108705', 'S01106721', 'S01111729', 'S01106128', 'S01110098', 'S01107627', 'S01113084',  " & vbCrLf _
            ''        & "'S01105296', 'S01108167', 'S01106513', 'S01111391', 'S01105846', 'S01109366', 'S01107328', 'S01112412', 'S01105682', 'S01108728', 'S01106753', 'S01111748', 'S01106156', 'S01110262', 'S01107679', 'S01113132',  " & vbCrLf _
            ''        & "'S01105323', 'S01108185', 'S01106517', 'S01111422', 'S01105847', 'S01109368', 'S01107398', 'S01112442', 'S01105695', 'S01108781', 'S01106759', 'S01111808', 'S01106171', 'S01110449', 'S01107703', 'S01113351',  " & vbCrLf _
            ''        & "'S01105328', 'S01108186', 'S01106525', 'S01111423', 'S01105888', 'S01109410', 'S01107404', 'S01112496', 'S01105724', 'S01108875', 'S01106848', 'S01111882', 'S01106249', 'S01110509', 'S01107771', 'S01113383',  " & vbCrLf _
            ''        & "'S01105369', 'S01108187', 'S01106575', 'S01111439', 'S01105905', 'S01109435', 'S01107446', 'S01112520', 'S01105734', 'S01109006', 'S01106906', 'S01111894', 'S01106252', 'S01110533', 'S01107790', 'S01113608',  " & vbCrLf _
            ''        & "'S01105400', 'S01108236', 'S01106590', 'S01111510', 'S01105925', 'S01109537', 'S01107499', 'S01112679', 'S01105744', 'S01109108', 'S01107035', 'S01111971', 'S01106297', 'S01110627', 'S01107811',   " & vbCrLf _
            ''        & "'S01105429', 'S01108263', 'S01106591', 'S01111531', 'S01105965', 'S01109618', 'S01107518', 'S01112753', 'S01105767', 'S01109144', 'S01107068', 'S01112058')"



            mSqlStr = mSqlStr & vbCrLf & " AND OUR_AUTO_KEY_SO=" & pAutoSONO & ""

            mSqlStr = mSqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(TxtDtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            mSqlStr = mSqlStr & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(TxtDtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            mSqlStr = mSqlStr & vbCrLf & " GROUP BY IH.INVOICE_DATE, IH.AUTO_KEY_INVOICE, ID.ITEM_CODE, ID.ITEM_UOM"
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY IH.INVOICE_DATE, IH.AUTO_KEY_INVOICE, ID.ITEM_CODE"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPO, ADODB.LockTypeEnum.adLockReadOnly)

            If RsSuppPO.EOF = False Then
                '            mPreviousAutoKeyInvoice = -1
                Do While RsSuppPO.EOF = False

                    mAutoKeyInvoice = IIf(IsDbNull(RsSuppPO.Fields("AUTO_KEY_INVOICE").Value), 0, RsSuppPO.Fields("AUTO_KEY_INVOICE").Value)
                    mRefDate = VB6.Format(IIf(IsDbNull(RsSuppPO.Fields("INVOICE_DATE").Value), "", RsSuppPO.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")

                    mUnit = IIf(IsDbNull(RsSuppPO.Fields("UOM_CODE").Value), "", RsSuppPO.Fields("UOM_CODE").Value)
                    mPackQty = IIf(IsDbNull(RsSuppPO.Fields("ITEM_QTY").Value), 0, RsSuppPO.Fields("ITEM_QTY").Value)

                    If chkSaleReturn.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mSaleReturnQty = GetSaleReturnQty(pCustomerCode, pItemCode, mAutoKeyInvoice)
                        mPackQty = mPackQty - mSaleReturnQty
                    End If


                    mDespStatus = IIf(chkSuppInvoice.CheckState = System.Windows.Forms.CheckState.Checked, "1", "0")

                    If OptAmendType(0).Checked = True Or OptAmendType(2).Checked = True Then
                        mSqlStr = "SELECT " & pNewPrice & " AS PO_RATE,"
                    Else
                        mSqlStr = "SELECT GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE) AS PO_RATE,"
                    End If

                    mSqlStr = mSqlStr & vbCrLf & " GETSALESHORTAGEQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.FYEAR,IH.MKEY, '" & Trim(pCustomerCode) & "', ID.ITEM_CODE) AS SHORTAGEQTY, " & vbCrLf & " ID.ITEM_RATE -GETSALEDEBITRATE(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.FYEAR,IH.MKEY, '" & Trim(pCustomerCode) & "', ID.ITEM_CODE) + GETSALESUPPBILLPRICE(" & RsCompany.Fields("COMPANY_CODE").Value & ", ID.ITEM_CODE, '" & Trim(pCustomerCode) & "',IH.AUTO_KEY_INVOICE) AS BILL_RATE" & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'" & vbCrLf & " AND IH.REF_DESP_TYPE<>'U' AND IH.CANCELLED='N'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.AUTO_KEY_INVOICE = " & mAutoKeyInvoice & ""

                    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRate, ADODB.LockTypeEnum.adLockReadOnly)

                    mRate = 0
                    mSuppRate = 0
                    mPORate = CStr(0)

                    If RsRate.EOF = False Then
                        mPackQty = mPackQty - IIf(IsDbNull(RsRate.Fields("SHORTAGEQTY").Value), 0, RsRate.Fields("SHORTAGEQTY").Value)
                        mRate = IIf(IsDbNull(RsRate.Fields("BILL_RATE").Value), 0, RsRate.Fields("BILL_RATE").Value)
                        mPORate = VB6.Format(IIf(IsDbNull(RsRate.Fields("PO_RATE").Value), 0, RsRate.Fields("PO_RATE").Value), "0.0000")
                        If OptAmendType(2).Checked = True Then
                            mSuppRate = CDbl(VB6.Format(CDbl(mPORate) - pOldPrice, "0.0000")) ''temp 09/04/2020
                        Else
                            mSuppRate = CDbl(VB6.Format(CDbl(mPORate) - mRate, "0.0000")) ''pNewPrice  09-05-2019
                        End If
                        '
                    End If

                    If mPackQty > 0 And mSuppRate > 0 Then
                        SqlStr = "INSERT INTO TEMP_DSP_DESPATCH_HDR( " & vbCrLf & " USERID, MKEY, COMPANY_CODE,  DESP_DATE," & vbCrLf & " SUPP_CUST_CODE, " & vbCrLf & " LOADING_TIME, " & vbCrLf & " DESP_STATUS, DESP_TYPE, " & vbCrLf & " AUTO_KEY_SO, SO_DATE, " & vbCrLf & " VENDOR_PO, VENDOR_PO_DATE, " & vbCrLf & " DIV_CODE, DESPATCHTYPE) "

                        SqlStr = SqlStr & vbCrLf & " VALUES( '" & MainClass.AllowSingleQuote(PubUserID) & "', " & mProcessKey & ", " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ",  TO_DATE('" & VB6.Format(mDespDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(pCustomerCode) & "', " & vbCrLf & " TO_DATE('" & mDespTime & "','HH24:MI')," & vbCrLf & " '" & mDespStatus & "','U', " & vbCrLf & " " & Val(CStr(pAutoSONO)) & ",TO_DATE('" & VB6.Format(pAutoSODate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & pCustPoNo & "',TO_DATE('" & VB6.Format(pCustPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mDivisionCode & ", 1 )"

                        PubDBCn.Execute(SqlStr)


                        SqlStr = " INSERT INTO TEMP_DSP_DESPATCH_DET ( " & vbCrLf & " MKEY, SERIAL_NO, ITEM_CODE," & vbCrLf & " ITEM_UOM, STOCK_TYPE, PACKED_QTY," & vbCrLf & " NO_OF_PACKETS, PDIR_NO, REF_NO, REF_DATE," & vbCrLf & " MRR_REF_NO, COMPANY_CODE, " & vbCrLf & " SONO, SODATE," & vbCrLf & " CUST_PO, CUST_PO_DATE, LOT_NO,JITCALLNO, SUPP_RATE) "

                        SqlStr = SqlStr & vbCrLf & " VALUES ('" & mProcessKey & "', " & CntItemCode & " ,'" & pItemCode & "', " & vbCrLf & " '" & mUnit & "','FG'," & mPackQty & ", " & vbCrLf & " 0, '', '" & mAutoKeyInvoice & "', TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " 0, " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " 0, ''," & vbCrLf & " '', '', 0,'', " & mSuppRate & ") "

                        PubDBCn.Execute(SqlStr)
                    End If

                    CntItemCode = CntItemCode + 1
                    RsSuppPO.MoveNext()

                Loop
            End If
NextRec:

            SqlStr = "UPDATE DSP_SALEORDER_DET SET IS_SUPP_GEN='Y' WHERE MKEY=" & pSOMKey & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(pItemCode) & "'"
            PubDBCn.Execute(SqlStr)


            RsPO.MoveNext()
        Loop
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
ErrPart:
        'Resume
        MsgInformation(Err.Description)
        Update1 = False
        PubDBCn.RollbackTrans()

    End Function

    Private Function GetSaleOldPrice(ByRef xAutoSONo As Double, ByRef xAutoSOAmendNo As Double, ByRef xCustomerCode As String, ByRef xItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT ID.ITEM_PRICE" & vbCrLf & " FROM DSP_SALEORDER_HDR IH,DSP_SALEORDER_DET ID" & vbCrLf & " WHERE IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.SUPP_CUST_CODE = '" & xCustomerCode & "' " & vbCrLf & " AND IH.AUTO_KEY_SO=" & Val(CStr(xAutoSONo)) & " " & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "' " & vbCrLf & " AND IH.AMEND_NO =" & Val(CStr(xAutoSOAmendNo)) & " AND SO_APPROVED='Y'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSaleOldPrice = IIf(IsDbNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value)
        End If
        Exit Function
ErrPart:
        GetSaleOldPrice = 0
    End Function

    Private Function GetSaleReturnQty(ByRef pCustomerCode As String, ByRef pItemCode As String, ByRef mSaleInvoiceNo As String) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

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

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click
        SearchItem()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub FrmDespNoteProcess_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmDespNoteProcess_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LErr

        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        MainClass.SetControlsColor(Me)

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn


        TxtDtFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        TxtDtTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtItem.Enabled = False
        cmdSearchItem.Enabled = False
        TxtAccount.Enabled = True
        cmdsearch.Enabled = True

        'Me.Height = VB6.TwipsToPixelsY(6855)
        'Me.Width = VB6.TwipsToPixelsX(5010)
        Me.Top = 0
        Me.Left = 0


        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0
        cboDivision.Enabled = True

        Call FillCboSaleType()
        Exit Sub
LErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub OptItem_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptItem.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptItem.GetIndex(eventSender)
            txtItem.Enabled = IIf(Index = 0, False, True)
            cmdSearchItem.Enabled = IIf(Index = 0, False, True)
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
        Dim SqlStr As String = ""

        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

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
                '        ElseIf FYChk(CDate(TxtDtTo.Text)) = False Then
                '            TxtDtTo.SetFocus
                '            Cancel = True
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItem.TextChanged
        cmdProcess.Enabled = True
    End Sub

    Private Sub TxtItem_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItem.DoubleClick
        SearchItem()
    End Sub

    Private Sub TxtItem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItem.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

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
        Dim SqlStr As String = ""

        If txtItem.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((txtItem.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        MainClass.SearchGridMaster(txtItem.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            txtItem.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class
