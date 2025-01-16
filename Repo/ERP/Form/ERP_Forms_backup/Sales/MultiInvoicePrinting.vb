Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Serialization
Imports QRCoder
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Color
Imports System.Drawing
Imports System.Drawing.Printing
Imports AxFPSpreadADO

Friend Class frmMultiInvoicePrinting
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColMKey As Short = 1
    Private Const ColInvoiceSeq As Short = 2
    Private Const ColInvoiceNo As Short = 3
    Private Const CoInvoiceDate As Short = 4
    Private Const ColCustomerCode As Short = 5
    Private Const ColCustomerName As Short = 6
    Private Const ColLocation As Short = 7
    Private Const ColVendorCode As Short = 8
    Private Const ColDistance As Short = 9
    Private Const ColVechile As Short = 10
    Private Const ColBillAmount As Short = 11
    Private Const ColIRNNo As Short = 12
    Private Const ColIRNDate As Short = 13
    Private Const ColIRNAckNo As Short = 14
    Private Const ColIRNAckDate As Short = 15
    Private Const ColEWayNo As Short = 16
    Private Const ColEWayDate As Short = 17
    Private Const ColEWayBillUpToValid As Short = 18
    Private Const ColEWayPath As Short = 19
    Private Const ColConsolidationEWayNo As Short = 20
    Private Const ColFlag As Short = 21
    Private Const ColIRNPrint As Short = 22
    Private Const ColEWayPrint As Short = 23
    Private Const ColConsolidationEWayPrint As Short = 24

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean


    Public Class EWAYBILLBYIRN
        Public Property Irn As String
        Public Property TransMode As String
        Public Property Transid As String
        Public Property Transname As String
        Public Property Distance As Integer
        Public Property Transdocno As String
        Public Property TransdocDt As String

        Public Property VehNo As String
        Public Property VehType As String

        Public Property ShipFrom_Nm As String
        Public Property ShipFrom_Addr1 As String
        Public Property ShipFrom_Addr2 As String
        Public Property ShipFrom_Loc As String
        Public Property ShipFrom_Pin As String
        Public Property ShipFrom_Stcd As String
        Public Property ShipTo_Addr1 As String
        Public Property ShipTo_Addr2 As String
        Public Property ShipTo_Loc As String
        Public Property ShipTo_Pin As String
        Public Property ShipTo_Stcd As String
        Public Property GSTIN As String
        Public Property CDKey As String
        Public Property EinvUserName As String
        Public Property EinvPassword As String
        Public Property EFUserName As String
        Public Property EFPassword As String

    End Class
    Public Class CONSOLIDATIONEWAYBILLBYIRN
        Public Property EWBNumber As String
        Public Property VehicleNumber As String
        Public Property SupPlace As String
        Public Property SupState As String
        Public Property Transdocno As String
        Public Property TransDocDate As Integer
        Public Property TransMode As String


        Public Property GSTIN As String
        Public Property CDKey As String
        Public Property EWBUserName As String
        Public Property EWBPassword As String
        Public Property EFUserName As String
        Public Property EFPassword As String

    End Class
    Public Class EWAYBILLPRN
        Public Property GSTIN As String
        Public Property ewbNo As String     'Long
        Public Property Year As Integer
        Public Property Month As Integer
        Public Property EFUserName As String
        Public Property EFPassword As String
        Public Property CDKey As String

        Public Property EWBUserName As String
        Public Property EWBPassword As String

    End Class

    Public Class EWAYBILLCONSOLIDATIONPRN
        Public Property GSTIN As String
        Public Property tripSheetNo As String     'Long
        Public Property Year As Integer
        Public Property Month As Integer
        Public Property EFUserName As String
        Public Property EFPassword As String
        Public Property CDKey As String

        Public Property EWBUserName As String
        Public Property EWBPassword As String

    End Class

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        cmdShow.Enabled = True

        CmdSave.Enabled = False
        cmdGenerateEWayBill.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
        cmdeMail.Enabled = False
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        cmdShow.Enabled = True
        CmdSave.Enabled = False
        cmdGenerateEWayBill.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
        cmdeMail.Enabled = False
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()

    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String
        Dim mInvoiceNo As String
        Dim mInvoiceDate As String
        Dim mIRNNo As String
        Dim meInvoiceApp As String
        Dim mInvoiceSeq As Long
        Dim mUpdateCount As Integer
        Dim mMKey As String
        Dim mCustomerName As String
        Dim mValue As String

        meInvoiceApp = IIf(IsDBNull(RsCompany.Fields("E_INVOICE_APP").Value), "N", RsCompany.Fields("E_INVOICE_APP").Value)
        If meInvoiceApp = "N" Then Exit Sub

        If chkNonGSTCreditNote.Checked = True Then
            Exit Sub
        End If

        'PubDBCn.Errors.Clear()
        'PubDBCn.BeginTrans()
        mUpdateCount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColFlag
                If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then
                    .Col = ColMKey
                    mMKey = Trim(.Text)

                    .Col = ColInvoiceSeq
                    mInvoiceSeq = Val(.Text)

                    .Col = ColInvoiceNo
                    mInvoiceNo = Trim(.Text)

                    .Col = CoInvoiceDate
                    mInvoiceDate = Trim(.Text)

                    .Col = ColCustomerName
                    mCustomerName = Trim(.Text)

                    If (mInvoiceSeq = 3 Or mInvoiceSeq = 5 Or mInvoiceSeq = 0) Then
                        GoTo NextRowNo
                    End If

                    .Col = ColIRNNo
                    mIRNNo = Trim(.Text)

                    .Col = ColFlag
                    'If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    'If mIRNNo = "" Then
                    If chkCreditNote.Checked = True Or chkDebitNote.Checked = True Then
                        mValue = WebRequestGenerateCRIRN(mMKey, mInvoiceSeq, mCustomerName)
                    Else
                        mValue = WebRequestGenerateIRN(mMKey, mInvoiceSeq, mCustomerName)
                    End If

                    .Col = ColIRNNo
                    .Text = mValue
                    'End If
                    'End If
                End If

NextRowNo:

            Next
        End With

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114 Then
            Dim RsTemp As ADODB.Recordset
            Dim mBillNo As String
            Dim mBillDate As String
            Dim mSuppCustName As String
            Dim mSubRow As Long
            Dim mItemCode As String
            Dim mItemDesc As String
            Dim mPartNo As String
            Dim mItemUOM As String
            Dim mItemQty As Double
            Dim mItemRate As Double
            Dim mItemAmount As Double
            Dim mBodyTextDetail As String
            Dim mBodyText As String
            Dim mSubject As String
            Dim mTo As String
            Dim mCC As String
            Dim mFrom As String
            Dim mVehicleNo As String

            strServerPop3 = GetEMailID("POP_ID")
            strServerSmtp = GetEMailID("SMTP_ID")
            strAccount = GetEMailID("MAIL_ACCOUNT")
            strPassword = GetEMailID("PASSWORD")
            mFrom = GetEMailID("DSP_MAIL_TO")

            mTo = "" '' GetEMailID("ACCT_MAIL_TO")
            mCC = GetEMailID("ACCT_MAIL_TO")

            SqlStr = "SELECT IH.BILLNO, IH.INVOICE_DATE, CMST.SUPP_CUST_NAME, IH.VEHICLENO," & vbCrLf _
                    & " ID.SUBROWNO, ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.CUSTOMER_PART_NO,  " & vbCrLf _
                    & " ID.ITEM_UOM, ID.ITEM_QTY, ID.ITEM_RATE, ID.ITEM_AMT, SUPP_CUST_PUR_MAILID" & vbCrLf _
                    & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                    & " AND IH.MKEY=ID.MKEY" & vbCrLf _
                    & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                    & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                    & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                    & " AND IH.MKEY='" & mMKey & "'" & vbCrLf _
                    & " ORDER BY ID.SUBROWNO"

            'MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


            If RsTemp.EOF = False Then
                mTo = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PUR_MAILID").Value), "", RsTemp.Fields("SUPP_CUST_PUR_MAILID").Value)
                mTo = IIf(mTo = "", mCC, mTo)

                If mTo = "" Then Exit Sub

                mBodyTextDetail = "<table align=center border=1 cellPadding=2 cellSpacing=0>" _
                                & "<tr>" _
                                & "<td width=50><b>SNo</b></td>" _
                                & "<td width=200><b>Vehicle No</b></td>" _
                                & "<td width=200><b>Bill No</b></td>" _
                                & "<td width=200><b>Bill Date</b></td>" _
                                & "<td width=500><b>Customer Name</b></td>" _
                                & "<td width=200><b>Part No</b></td>" _
                                & "<td width=500><b>Product Name</b></td>" _
                                & "<td width=200><b>UOM</b></td>" _
                                & "<td width=200><b>Qty</b></td>" _
                                & "<td width=200><b>Rate (Rs)</b></td>" _
                                & "<td width=200><b>Amount (Rs)</b></td>" _
                                & "</tr>"

                Do While RsTemp.EOF = False
                    mVehicleNo = IIf(IsDBNull(RsTemp.Fields("VEHICLENO").Value), "", RsTemp.Fields("VEHICLENO").Value) ' IIf(IsNull(RsTemp!VEHICLENO), "", RsTemp!VEHICLENO)
                    mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value) ' IIf(IsNull(RsTemp!BILLNO), "", RsTemp!BILLNO)
                    mBillDate = IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", VB6.Format(RsTemp.Fields("INVOICE_DATE").Value, "DD/MM/YYYY")) ' Format(IIf(IsNull(RsTemp!INVOICE_DATE), "", RsTemp!INVOICE_DATE), "DD/MM/YYYY")
                    mSuppCustName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value) 'IIf(IsNull(RsTemp!SUPP_CUST_NAME), "", RsTemp!SUPP_CUST_NAME)
                    mSubRow = IIf(IsDBNull(RsTemp.Fields("SUBROWNO").Value), 0, RsTemp.Fields("SUBROWNO").Value) ' IIf(IsNull(RsTemp!SUBROWNO), 0, RsTemp!SUBROWNO)
                    mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value) 'IIf(IsNull(RsTemp!ITEM_CODE), "", RsTemp!ITEM_CODE)
                    mItemDesc = IIf(IsDBNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value) 'IIf(IsNull(RsTemp!Item_Short_Desc), "", RsTemp!Item_Short_Desc)
                    mPartNo = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value) 'IIf(IsNull(RsTemp!CUSTOMER_PART_NO), "", RsTemp!CUSTOMER_PART_NO)
                    mItemUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value) 'IIf(IsNull(RsTemp!ITEM_UOM), "", RsTemp!ITEM_UOM)
                    mItemQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value) 'IIf(IsNull(RsTemp!ITEM_QTY), 0, RsTemp!ITEM_QTY)
                    mItemRate = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value) 'IIf(IsNull(RsTemp!ITEM_RATE), 0, RsTemp!ITEM_RATE)
                    mItemAmount = IIf(IsDBNull(RsTemp.Fields("ITEM_AMT").Value), 0, RsTemp.Fields("ITEM_AMT").Value) 'IIf(IsNull(RsTemp!ITEM_AMT), 0, RsTemp!ITEM_AMT)

                    mBodyTextDetail = mBodyTextDetail _
                                    & "<tr>" _
                                    & "<td align=Right>" & mSubRow & "</td>" _
                                    & "<td>" & mVehicleNo & "</td>" _
                                    & "<td>" & mBillNo & "</td>" _
                                    & "<td>" & mBillDate & "</td>" _
                                    & "<td>" & mSuppCustName & "</td>" _
                                    & "<td>" & mPartNo & "</td>" _
                                    & "<td>" & mItemDesc & "</td>" _
                                    & "<td>" & mItemUOM & "</td>" _
                                    & "<td align=Right>" & Format(mItemQty, "0.00") & "</td>" _
                                    & "<td align=Right>" & Format(mItemRate, "0.00") & "</td>" _
                                    & "<td align=Right>" & Format(mItemAmount, "0.00") & "</td>" _
                                    & "</tr>"

                    RsTemp.MoveNext()
                Loop

                mBodyTextDetail = mBodyTextDetail & "</table>"

                mBodyText = "<html><body>To,<br />" _
                        & "<b>M/s </b>" & mSuppCustName & "<br />" _
                        & "Dear Sir,<br />" _
                        & "Please find the Invoice Detail :<br />"

                mBodyText = mBodyText _
                        & mBodyTextDetail _
                        & "<br />" _
                        & "<br />" _
                        & "Regards,<br />" _
                        & "<br />" _
                        & "" & RsCompany.Fields("COMPANY_NAME").Value & "<br />" _
                        & "</body></html>"

                If strServerPop3 = "" And strServerSmtp = "" And strAccount = "" And strPassword = "" Then
                    MsgBox("Please Check Email Configuration", vbInformation)
                    '                SendMail = False
                    Exit Sub
                End If

                mSubject = " Invoice Detail Of " & mBillNo & " Date :" & mBillDate

                If Trim(mTo) <> "" Then
                    '' Call SendMailProcess(mFrom, mTo, mCC, "", strAccount, strPassword, "", mSubject, mBodyText)
                    If SendMailProcess(mFrom, mTo, mCC, "", "", mSubject, mBodyText) = False Then GoTo ErrPart
                End If

            End If
        End If

        'PubDBCn.CommitTrans()

        'MsgBox("Total " & mUpdateCount & " Invoice Generated.", MsgBoxStyle.Information)
        Exit Sub
ErrPart:
        ''Resume
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
        'PubDBCn.RollbackTrans()
    End Sub
    Public Function WebRequestEWayBillByIRN(ByRef pMKey As String, ByRef pIRNNo As String, pInvoiceSeqType As Long) As Boolean
        On Error GoTo ErrPart
        Dim url As String

        Dim mUserName As String
        Dim mPassword As String

        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mBody As String
        Dim mResponseId As String
        Dim mResponseIdStr As String
        Dim url1 As String
        Dim WebRequestGen As String
        Dim pStaus As String

        Dim pError As String
        Dim mBMPFileName As String

        Dim pResponseText As String

        Dim mCDKey As String
        Dim mEFUserName As String
        Dim mEFPassword As String
        Dim mEInvUserName As String
        Dim mEInvPassword As String

        Dim xSqlStr As String = ""
        Dim RsTempInv As ADODB.Recordset
        Dim mDespatchFrom As String = ""
        Dim pShipTo_Loc As String = ""
        Dim pShipTo_Addr1 As String = ""
        Dim pShipTo_Addr2 As String = ""
        Dim pShipTo_Pin As String = ""
        Dim pShipTo_Stcd As String = ""
        Dim mShipTo As String = ""
        Dim pTransdocno As String = ""
        Dim mStateName As String = ""

        Dim pShipFrom_Nm As String = ""
        Dim pShipFrom_Addr1 As String = ""
        Dim pShipFrom_Addr2 As String = ""
        Dim pShipFrom_Loc As String = ""
        Dim pShipFrom_Pin As String = ""
        Dim pShipFrom_Stcd As String = ""
        Dim pGSTIN As String = ""
        Dim pTransMode As String = ""
        Dim pTransid As String = ""
        Dim pTransname As String = ""
        Dim pDistance As Integer = 0
        Dim pVehNo As String = ""
        Dim pVehType As String = ""
        Dim mIsTesting As String = "Y"
        Dim mDoc_Dt As String = ""

        If pInvoiceSeqType = 6 Then Exit Function

        If GeteInvoiceSetupContents(url, "G", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword, mIsTesting) = False Then GoTo ErrPart

        If mIsTesting = "Y" Then
            url = "http://einvsandbox.webtel.in/v1.03/GenIRN"
            mCDKey = "1000687"
            mEInvUserName = "03AAACW3775F010"       ''"06AAACW3775F013"		 "29AAACW3775F000" '' 					
            mEInvPassword = "Admin!23"  ''"Admin!23.."    ''
            mEFUserName = "29AAACW3775F000"  '' "29AAACW3775F000"
            mEFPassword = "Admin!23.."

            pGSTIN = "03AAACW3775F010" '' IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        Else
            pGSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        End If


        xSqlStr = " SELECT IH.*, " & vbCrLf _
               & " BMST.LOCATION_ID, BMST.SUPP_CUST_NAME, BMST.SUPP_CUST_ADDR, BMST.SUPP_CUST_CITY, " & vbCrLf _
               & " BMST.SUPP_CUST_STATE, BMST.SUPP_CUST_PIN, BMST.SUPP_CUST_PHONE, BMST.SUPP_CUST_FAXNO, " & vbCrLf _
               & " BMST.SUPP_CUST_MAILID, BMST.SUPP_CUST_MOBILE, BMST.COUNTRY, BMST.GST_RGN_NO " & vbCrLf _
               & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_BUSINESS_MST BMST" & vbCrLf _
               & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
               & " And IH.MKEY='" & pMKey & "'" & vbCrLf _
               & " And IH.COMPANY_CODE=BMST.COMPANY_CODE" & vbCrLf _
               & " And IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
               & " And IH.BILL_TO_LOC_ID=BMST.LOCATION_ID"

        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempInv, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempInv.EOF = False Then

            mDespatchFrom = IIf(IsDBNull(RsTempInv.Fields("IS_DESP_OTHERTHAN_BILL").Value), "N", RsTempInv.Fields("IS_DESP_OTHERTHAN_BILL").Value)
            mShipTo = IIf(IsDBNull(RsTempInv.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsTempInv.Fields("SHIPPED_TO_SAMEPARTY").Value)
            pDistance = IIf(IsDBNull(RsTempInv.Fields("TRANS_DISTANCE").Value), 0, RsTempInv.Fields("TRANS_DISTANCE").Value)
            pVehType = IIf(IsDBNull(RsTempInv.Fields("VEHICLE_TYPE").Value), "R", RsTempInv.Fields("VEHICLE_TYPE").Value)
            pVehNo = IIf(IsDBNull(RsTempInv.Fields("VEHICLENO").Value), "", RsTempInv.Fields("VEHICLENO").Value)
            pTransMode = IIf(IsDBNull(RsTempInv.Fields("TRANSPORT_MODE").Value), "", RsTempInv.Fields("TRANSPORT_MODE").Value) '' "1"  ''1. Road , 2. Rail, 3. Air, 4. Ship ''IIf(IsDBNull(RsTempInv.Fields("TRANSPORT_MODE").Value), "", RsTempInv.Fields("TRANSPORT_MODE").Value)

            pTransdocno = IIf(IsDBNull(RsTempInv.Fields("GRNO").Value), "", RsTempInv.Fields("GRNO").Value)
            mDoc_Dt = VB6.Format(IIf(IsDBNull(RsTempInv.Fields("GRDATE").Value), "", RsTempInv.Fields("GRDATE").Value), "YYYYMMDD")

            pTransid = IIf(IsDBNull(RsTempInv.Fields("TRANSPORTER_GSTNO").Value), "", RsTempInv.Fields("TRANSPORTER_GSTNO").Value)
            pTransname = IIf(IsDBNull(RsTempInv.Fields("CARRIERS").Value), "", RsTempInv.Fields("CARRIERS").Value)


            Dim mShippFrom As String
            mShippFrom = IIf(IsDBNull(RsTempInv.Fields("SHIPPED_FROM_PARTY_CODE").Value), "", RsTempInv.Fields("SHIPPED_FROM_PARTY_CODE").Value)

            Dim mShippTo As String
            mShippTo = IIf(IsDBNull(RsTempInv.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTempInv.Fields("SHIPPED_TO_PARTY_CODE").Value)

            Dim mShippToLoc As String
            mShippToLoc = IIf(IsDBNull(RsTempInv.Fields("BILL_TO_LOC_ID").Value), "", RsTempInv.Fields("BILL_TO_LOC_ID").Value)

            If mDespatchFrom = "Y" Then
                mSqlStr = " Select SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf _
                    & " FROM FIN_SUPP_CUST_MST" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShippFrom) & "'"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    pShipFrom_Nm = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
                    pShipFrom_Addr1 = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                    pShipFrom_Addr2 = ""
                    pShipFrom_Loc = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)


                    pShipFrom_Pin = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
                    mStateName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
                    pShipFrom_Stcd = GetStateCode(mStateName)

                End If
            Else
                pShipFrom_Nm = ""
                pShipFrom_Addr1 = ""
                pShipFrom_Addr2 = ""
                pShipFrom_Loc = ""
                pShipFrom_Pin = ""
                pShipFrom_Stcd = ""
            End If


            If mShipTo = "N" Then
                mSqlStr = " SELECT " & vbCrLf _
                       & " BMST.LOCATION_ID, BMST.SUPP_CUST_NAME, BMST.SUPP_CUST_ADDR, BMST.SUPP_CUST_CITY, " & vbCrLf _
                       & " BMST.SUPP_CUST_STATE, BMST.SUPP_CUST_PIN, BMST.SUPP_CUST_PHONE, BMST.SUPP_CUST_FAXNO, " & vbCrLf _
                       & " BMST.SUPP_CUST_MAILID, BMST.SUPP_CUST_MOBILE, BMST.COUNTRY, BMST.GST_RGN_NO " & vbCrLf _
                       & " FROM FIN_SUPP_CUST_BUSINESS_MST BMST" & vbCrLf _
                       & " WHERE BMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                       & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShippTo) & "'" & vbCrLf _
                       & " And BMST.LOCATION_ID='" & MainClass.AllowSingleQuote(mShippToLoc) & "'"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then

                    pShipTo_Loc = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)

                    pShipTo_Addr1 = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                    pShipTo_Addr2 = ""


                    If pInvoiceSeqType = 6 Then
                        pShipTo_Pin = "999999"
                        pShipTo_Stcd = CStr(96)
                    Else
                        pShipTo_Pin = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
                        mStateName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
                        pShipTo_Stcd = GetStateCode(mStateName)
                    End If
                End If
            Else
                pShipTo_Loc = ""
                pShipTo_Addr1 = ""
                pShipTo_Addr2 = ""
                pShipTo_Pin = ""
                pShipTo_Stcd = ""

            End If
        End If


        Dim http As Object   '' Dim http As MSXML2.XMLHTTP60 '' MSXML.xmlhttp							
        http = CreateObject("MSXML2.ServerXMLHTTP")


        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")

        Dim details As New List(Of EWAYBILLBYIRN)()

        details.Add(New EWAYBILLBYIRN() With {
            .Irn = pIRNNo,
            .TransMode = pTransMode,
            .Transid = pTransid,
            .Transname = pTransname,
            .Distance = pDistance,
            .Transdocno = pTransdocno,
            .TransdocDt = mDoc_Dt,
            .VehNo = pVehNo,
            .VehType = pVehType,
            .ShipFrom_Nm = pShipFrom_Nm,
            .ShipFrom_Addr1 = pShipFrom_Addr1,
            .ShipFrom_Addr2 = pShipFrom_Addr2,
            .ShipFrom_Loc = pShipFrom_Loc,
            .ShipFrom_Pin = pShipFrom_Pin,
            .ShipFrom_Stcd = pShipFrom_Stcd,
            .ShipTo_Addr1 = pShipTo_Addr1,
            .ShipTo_Addr2 = pShipTo_Addr2,
            .ShipTo_Loc = pShipTo_Loc,
            .ShipTo_Pin = pShipTo_Pin,
            .ShipTo_Stcd = pShipTo_Stcd,
            .GSTIN = pGSTIN,
            .CDKey = mCDKey,
            .EinvUserName = mEInvUserName,
            .EinvPassword = mEInvPassword,
            .EFUserName = mEFUserName,
            .EFPassword = mEFPassword
           })

        Dim mBodyDetail As String = JsonConvert.SerializeObject(details)


        mBody = "{""Push_Data_List"":"
        'mBody = mBody & """Data"": "
        mBody = mBody & mBodyDetail
        'mBody = mBody & "]"
        mBody = mBody & "}"

        http.Send(mBody)

        pResponseText = http.responseText
        '    pResponseText = Replace(pResponseText, "\", "")							
        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        '    pResponseText = Mid(pResponseText, 2, Len(pResponseText) - 2)							

        Dim post As Object
        pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Status = ""})).Status

        If pStaus = "1" Then

            Dim meWayResponseID As String
            Dim meWayBillDate As String
            Dim meWayBillUpto As String
            Dim SqlStr As String = ""

            Dim meWayFilePath As String

            meWayResponseID = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .EWayBill = ""})).EWayBill   'JsonTest.Item("EWayBill")
            meWayBillDate = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Date = ""})).Date 'JsonTest.Item("Date")
            meWayBillUpto = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .validUpto = ""})).validUpto ' JsonTest.Item("AckDate") 'JsonTest.Item("elements").Item(mResponseId).Item("ewayBillDate")						

            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
                    & " E_BILLWAYNO ='" & Val(meWayResponseID) & "'," & vbCrLf _
                    & " E_BILLWAYDATE =TO_DATE('" & VB6.Format(meWayBillDate, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                    & " E_BILLWAYVAILDUPTO =TO_DATE('" & VB6.Format(meWayBillUpto, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                    & " E_BILLWAYFILEPATH =''" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND MKEY ='" & pMKey & "'"

            PubDBCn.Execute(SqlStr)

            PubDBCn.CommitTrans()
        End If

        If pStaus = "0" Then
            pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            MsgInformation(pError)
            WebRequestEWayBillByIRN = False
            http = Nothing
            Exit Function
        End If

        WebRequestEWayBillByIRN = True
        http = Nothing

        'Dim JsonTest As Object
        'Dim SB As New cStringBuilder

        'Dim c As Object
        'Dim I As Integer

        'JsonTest = JSON.parse(pResponseText)

        'pStaus = JsonTest.Item("Status")


        'If pStaus = "1" Then
        '    ''pPDFOutFileName						
        'End If

        'If pStaus = "0" Then
        '    pError = JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
        '    MsgInformation(pError)
        '    WebRequestEWayBillByIRN = False
        '    http = Nothing
        '    Exit Function
        'End If

        'WebRequestEWayBillByIRN = True
        'http = Nothing
        ''    Set httpGen = Nothing							
        'Exit Function
ErrPart:
        '    Resume							
        WebRequestEWayBillByIRN = False
        'http = Nothing							
        MsgBox(Err.Description)
        '     PubDBCn.RollbackTrans							
    End Function
    Public Function WebRequestGenerateIRN(ByRef pMKey As String, pInvoiceSeq As Long, pCustomerName As String) As String

        On Error GoTo ErrPart
        Dim url As String = ""

        Dim mGSTIN As String
        Dim mTaxSch As String
        Dim mVersion As String
        Dim mIrn As String
        Dim mTran_Catg As String
        Dim mTran_RegRev As String
        Dim mTran_Typ As String = ""
        Dim mTran_EcmTrn As String
        Dim mTran_EcmGstin As String
        Dim mDoc_Typ As String
        Dim mDOC_NO As String
        Dim mDoc_Dt As String
        Dim mBillFrom_Gstin As String
        Dim mBillFrom_TrdNm As String
        Dim mBillFrom_Bno As String
        Dim mBillFrom_Bnm As String
        Dim mBillFrom_Flno As String
        Dim mBillFrom_Loc As String
        Dim mBillFrom_Dst As String
        Dim mBillFrom_Pin As String
        Dim mBillFrom_Stcd As String
        Dim mBillFrom_Ph As String
        Dim mBillFrom_Em As String
        Dim mBillTo_Gstin As String
        Dim mBillTo_TrdNm As String
        Dim mBillTo_Bno As String
        Dim mBillTo_Bnm As String
        Dim mBillTo_Flno As String
        Dim mBillTo_Loc As String
        Dim mBillTo_Dst As String
        Dim mBillTo_Pin As String
        Dim mBillTo_Stcd As String
        Dim mBillTo_Ph As String
        Dim mBillTo_Em As String
        Dim mToPlace As String
        Dim mItem_PrdNm As String
        Dim mItem_PrdDesc As String
        Dim mItem_HsnCd As String
        Dim mItem_Barcde As String
        Dim mItem_Qty As Double
        Dim mItem_FreeQty As Double
        Dim mItem_Unit As String
        Dim mItem_UnitPrice As Double
        Dim mItem_TotAmt As Double
        Dim mItem_Discount As Double
        Dim mItem_OthChrg As Double
        Dim mItem_AssAmt As Double
        Dim mItem_CgstRt As Double
        Dim mItem_SgstRt As Double
        Dim mItem_IgstRt As Double

        Dim mItem_CgstAmt As Double
        Dim mItem_SgstAmt As Double
        Dim mItem_IgstAmt As Double

        Dim mItem_CesRt As Double
        Dim mItem_CesNonAdval As Double
        Dim mItem_StateCes As Double
        Dim mItem_TotItemVal As Double
        Dim mItem_Bch_Nm As String
        Dim mItem_Bch_ExpDt As String
        Dim mItem_Bch_WrDt As String
        Dim mVal_AssVal As Double
        Dim mVal_CgstVal As Double
        Dim mVal_SgstVal As Double
        Dim mVal_IgstVal As Double
        Dim mVal_CesVal As Double
        Dim mVal_StCesVal As Double
        Dim mVal_CesNonAdVal As Double
        Dim mVal_Disc As Double
        Dim mVal_OthChrg As Double
        Dim mVal_TotInvVal As Double
        Dim mPay_Nam As String
        Dim mPay_Mode As String
        Dim mPay_PayTerm As String
        Dim mPay_PayInstr As String
        Dim mPay_CrDay As String
        Dim mPay_BalAmt As Double
        Dim mPay_PayDueDt As String
        Dim mRef_InvRmk As String
        Dim mRef_InvStDt As String
        Dim mRef_InvEndDt As String
        'Dim mTran_EcmGstin As String							
        Dim mDoc_OrgInvNo As String
        Dim mShipFrom_Gstin As String
        Dim mShipFrom_TrdNm As String
        Dim mShipFrom_Loc As String
        Dim mShipFrom_Pin As String
        Dim mShipFrom_Stcd As String
        Dim mShipFrom_Bno As String
        Dim mShipFrom_Bnm As String
        Dim mShipFrom_Flno As String
        Dim mShipFrom_Dst As String
        Dim mShipFrom_Ph As String
        Dim mShipFrom_Em As String
        Dim mStateName As String
        Dim mShipTo_Gstin As String
        Dim mShipTo_TrdNm As String
        Dim mShipTo_Loc As String
        Dim mShipTo_Pin As String
        Dim mShipTo_Stcd As String
        Dim mShipTo_Bno As String
        Dim mShipTo_Bnm As String
        Dim mShipTo_Flno As String
        Dim mShipTo_Dst As String
        Dim mShipTo_Ph As String
        Dim mShipTo_Em As String
        Dim mPay_FinInsBr As String
        Dim mPay_CrTrn As String
        Dim mPay_DirDr As String
        Dim mPay_AcctDet As String
        Dim mRef_PrecInvNo As String
        Dim mRef_PrecInvDt As String
        Dim mRef_RecAdvRef As String
        Dim mRef_TendRef As String
        Dim mRef_ContrRef As String
        Dim mRef_ExtRef As String
        Dim mRef_ProjRef As String
        Dim mRef_PORef As String
        Dim mExp_ExpCat As String
        Dim mExp_WthPay As String
        Dim mExp_InvForCur As String
        Dim mExp_ForCur As String
        Dim mExp_CntCode As String
        Dim mExp_ShipBNo As String
        Dim mExp_ShipBDt As String
        Dim mExp_Port As String
        Dim mGetQRImg As String
        Dim mGetSignedInvoice As String = ""
        Dim mCDKey As String = ""
        Dim mEInvUserName As String = ""
        Dim mEInvPassword As String = ""
        Dim mEFUserName As String = ""
        Dim mEFPassword As String = ""

        Dim pStateName As String
        Dim pStateCode As String
        Dim cntRow As Integer

        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mBody As String
        Dim mResponseId As String
        Dim mResponseIdStr As String
        Dim url1 As String
        Dim WebRequestGen As String
        Dim pStaus As String

        Dim mIRNNo As String
        Dim mIRNAckNo As String
        Dim mIRNAckDate As String

        Dim pError As String
        Dim mMSC As Double
        Dim pRO As Double


        Dim mSignedQRCode As String
        Dim mSignedInvoice As String
        'Dim pUserId As String							
        Dim mBMPFileName As String
        Dim pIsTesting As String = "Y"

        Dim pResponseText As String
        Dim mEwb_TransId As String
        Dim mEwb_TransName As String
        Dim mEwb_TransMode As String
        Dim mEwb_Distance As String
        Dim mEwb_TransDocNo As String
        Dim mEwb_TransDocDt As String
        Dim mEwb_VehNo As String
        Dim mEwb_VehType As String


        If GeteInvoiceSetupContents(url, "G", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword, pIsTesting) = False Then GoTo ErrPart

        If pIsTesting = "Y" Then
            url = "http://einvsandbox.webtel.in/v1.03/GenIRN"
            mCDKey = "1000687"
            mGSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
            If VB.Left(mGSTIN, 2) = "03" Then
                mEInvUserName = "03AAACW3775F010"       ''"06AAACW3775F013"		 "29AAACW3775F000" '' 					
                mEInvPassword = "Admin!23"  ''"Admin!23.."    ''
                mEFUserName = "29AAACW3775F000"  '' "29AAACW3775F000"
                mEFPassword = "Admin!23.."
                mGSTIN = "03AAACW3775F010" ''IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
            Else
                mEInvUserName = "06AAACW3775F010"       ''"06AAACW3775F013"		 "29AAACW3775F000" '' 					
                mEInvPassword = "Admin!23"  ''"Admin!23.."    ''
                mEFUserName = "29AAACW3775F000"  '' "29AAACW3775F000"
                mEFPassword = "Admin!23.."
                mGSTIN = "06AAACW3775F010" ''IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
            End If

        Else
            mGSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        End If

        Dim HTTP As Object
        HTTP = CreateObject("MSXML2.ServerXMLHTTP")



        mTaxSch = "GST"
        mVersion = "1.0"
        mIrn = ""

        Dim xSqlStr As String = ""
        Dim RsTempInv As ADODB.Recordset = Nothing

        xSqlStr = " Select IH.*, " & vbCrLf _
                & " BMST.LOCATION_ID, BMST.SUPP_CUST_NAME, BMST.SUPP_CUST_ADDR, BMST.SUPP_CUST_CITY, " & vbCrLf _
                & " BMST.SUPP_CUST_STATE, BMST.SUPP_CUST_PIN, BMST.SUPP_CUST_PHONE, BMST.SUPP_CUST_FAXNO, " & vbCrLf _
                & " BMST.SUPP_CUST_MAILID, BMST.SUPP_CUST_MOBILE, BMST.COUNTRY, BMST.GST_RGN_NO " & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_BUSINESS_MST BMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And IH.MKEY='" & pMKey & "'" & vbCrLf _
                & " And IH.COMPANY_CODE=BMST.COMPANY_CODE" & vbCrLf _
                & " And IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
                & " And IH.BILL_TO_LOC_ID=BMST.LOCATION_ID"

        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempInv, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempInv.EOF = False Then
            Dim mISLUT As String = ""
            Dim mDespatchFrom As String = ""
            Dim mShipTo As String = ""
            mISLUT = IIf(IsDBNull(RsTempInv.Fields("IS_LUT").Value), "N", RsTempInv.Fields("IS_LUT").Value)
            If pInvoiceSeq = 6 Then
                mTran_Catg = IIf(mISLUT = "Y", "EXPWOP", "EXPWP") ''						
            Else
                mTran_Catg = "B2B"
            End If

            mTran_RegRev = "N"

            mDespatchFrom = IIf(IsDBNull(RsTempInv.Fields("IS_DESP_OTHERTHAN_BILL").Value), "N", RsTempInv.Fields("IS_DESP_OTHERTHAN_BILL").Value)
            mShipTo = IIf(IsDBNull(RsTempInv.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsTempInv.Fields("SHIPPED_TO_SAMEPARTY").Value)
            If pInvoiceSeq = 4 Then
                mShipTo = "Y"
            End If
            If mDespatchFrom = "N" And mShipTo = "Y" Then
                mTran_Typ = "REG"
            ElseIf mDespatchFrom = "N" And mShipTo = "N" Then
                mTran_Typ = "SHP"
            ElseIf mDespatchFrom = "Y" And mShipTo = "Y" Then
                mTran_Typ = "DIS"
            ElseIf mDespatchFrom = "Y" And mShipTo = "N" Then
                mTran_Typ = "CMB"
            End If

            mTran_EcmTrn = "N"
            mTran_EcmGstin = ""

            If pInvoiceSeq = 9 Then
                mDoc_Typ = "DBN"
            Else
                mDoc_Typ = "INV"
            End If

            mDOC_NO = IIf(IsDBNull(RsTempInv.Fields("BILLNO").Value), "", RsTempInv.Fields("BILLNO").Value)
            mDoc_Dt = VB6.Format(IIf(IsDBNull(RsTempInv.Fields("INVOICE_DATE").Value), "", RsTempInv.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
            mDoc_OrgInvNo = ""

            mBillTo_TrdNm = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_NAME").Value), "", RsTempInv.Fields("SUPP_CUST_NAME").Value) '
            mBillTo_Bno = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_ADDR").Value), "", RsTempInv.Fields("SUPP_CUST_ADDR").Value)
            mBillTo_Bnm = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_CITY").Value), "", RsTempInv.Fields("SUPP_CUST_CITY").Value)
            mBillTo_Flno = ""
            mBillTo_Loc = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_CITY").Value), "", RsTempInv.Fields("SUPP_CUST_CITY").Value)
            mBillTo_Dst = ""
            mBillTo_Ph = ""
            mBillTo_Em = ""
            mToPlace = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_STATE").Value), "", RsTempInv.Fields("SUPP_CUST_STATE").Value)

            mEwb_TransId = IIf(IsDBNull(RsTempInv.Fields("TRANSPORTER_GSTNO").Value), "", RsTempInv.Fields("TRANSPORTER_GSTNO").Value)
            mEwb_TransName = IIf(IsDBNull(RsTempInv.Fields("CARRIERS").Value), "", RsTempInv.Fields("CARRIERS").Value)
            mEwb_TransMode = IIf(IsDBNull(RsTempInv.Fields("TRANSPORT_MODE").Value), 0, VB.Left(RsTempInv.Fields("TRANSPORT_MODE").Value, 1))
            mEwb_Distance = IIf(IsDBNull(RsTempInv.Fields("TRANS_DISTANCE").Value), 0, RsTempInv.Fields("TRANS_DISTANCE").Value)
            mEwb_TransDocNo = IIf(IsDBNull(RsTempInv.Fields("GRNO").Value), "", RsTempInv.Fields("GRNO").Value)
            mEwb_TransDocDt = VB6.Format(IIf(IsDBNull(RsTempInv.Fields("GRDATE").Value), "", RsTempInv.Fields("GRDATE").Value), "DD/MM/YYYY")
            mEwb_VehNo = IIf(IsDBNull(RsTempInv.Fields("VEHICLENO").Value), "", RsTempInv.Fields("VEHICLENO").Value)
            mEwb_VehType = IIf(IsDBNull(RsTempInv.Fields("VEHICLE_TYPE").Value), 0, VB.Left(RsTempInv.Fields("VEHICLE_TYPE").Value, 1))

            If pInvoiceSeq = 6 Then
                mBillTo_Gstin = "URP"
                mBillTo_Pin = "999999"
                mBillTo_Stcd = CStr(96)
            Else
                mBillTo_Gstin = IIf(IsDBNull(RsTempInv.Fields("GST_RGN_NO").Value), "", RsTempInv.Fields("GST_RGN_NO").Value)
                mBillTo_Pin = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_PIN").Value), "", RsTempInv.Fields("SUPP_CUST_PIN").Value)
                mBillTo_Stcd = GetStateCode(mToPlace)
            End If

            Dim mShippFrom As String
            mShippFrom = IIf(IsDBNull(RsTempInv.Fields("SHIPPED_FROM_PARTY_CODE").Value), "", RsTempInv.Fields("SHIPPED_FROM_PARTY_CODE").Value)

            Dim mShippTo As String
            mShippTo = IIf(IsDBNull(RsTempInv.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTempInv.Fields("SHIPPED_TO_PARTY_CODE").Value)

            Dim mShippToLoc As String
            mShippToLoc = IIf(IsDBNull(RsTempInv.Fields("SHIP_TO_LOC_ID").Value), "", RsTempInv.Fields("SHIP_TO_LOC_ID").Value)

            If mDespatchFrom = "Y" Then
                mSqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf _
                    & " FROM FIN_SUPP_CUST_MST" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShippFrom) & "'"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mShipFrom_Gstin = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
                    mShipFrom_TrdNm = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
                    mShipFrom_Loc = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                    mShipFrom_Pin = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
                    mStateName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
                    mShipFrom_Stcd = GetStateCode(mStateName)
                    mShipFrom_Bno = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                    mShipFrom_Bnm = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                    mShipFrom_Flno = ""
                    mShipFrom_Dst = ""
                    mShipFrom_Ph = ""
                    mShipFrom_Em = ""
                End If
            Else
                mShipFrom_Gstin = ""
                mShipFrom_TrdNm = ""
                mShipFrom_Loc = ""
                mShipFrom_Pin = ""
                mShipFrom_Stcd = ""
                mShipFrom_Bno = ""
                mShipFrom_Bnm = ""
                mShipFrom_Flno = ""
                mShipFrom_Dst = ""
                mShipFrom_Ph = ""
                mShipFrom_Em = ""
            End If


            If mShipTo = "N" Then
                'mSqlStr = " SELECT SUPP_CUST_NAME,SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtShippedTo.Text) & "'"

                mSqlStr = " SELECT " & vbCrLf _
                        & " BMST.LOCATION_ID, BMST.SUPP_CUST_NAME, BMST.SUPP_CUST_ADDR, BMST.SUPP_CUST_CITY, " & vbCrLf _
                        & " BMST.SUPP_CUST_STATE, BMST.SUPP_CUST_PIN, BMST.SUPP_CUST_PHONE, BMST.SUPP_CUST_FAXNO, " & vbCrLf _
                        & " BMST.SUPP_CUST_MAILID, BMST.SUPP_CUST_MOBILE, BMST.COUNTRY, BMST.GST_RGN_NO " & vbCrLf _
                        & " FROM FIN_SUPP_CUST_BUSINESS_MST BMST" & vbCrLf _
                        & " WHERE BMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShippTo) & "'" & vbCrLf _
                        & " And BMST.LOCATION_ID='" & MainClass.AllowSingleQuote(mShippToLoc) & "'"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then

                    mShipTo_TrdNm = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
                    mShipTo_Loc = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)

                    mShipTo_Bno = Mid(Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)), 1, 100)
                    mShipTo_Bnm = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                    mShipTo_Flno = Mid(Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)), 101, 100)
                    mShipTo_Dst = ""
                    mShipTo_Ph = ""
                    mShipTo_Em = ""

                    If pInvoiceSeq = 6 Then
                        mShipTo_Gstin = "URP"
                        mShipTo_Pin = "999999"
                        mShipTo_Stcd = CStr(96)
                    Else
                        mShipTo_Gstin = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
                        mShipTo_Pin = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
                        mStateName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
                        mShipTo_Stcd = GetStateCode(mStateName)
                    End If
                End If
            Else
                mShipTo_Gstin = ""
                mShipTo_TrdNm = ""
                mShipTo_Loc = ""
                mShipTo_Pin = ""
                mShipTo_Stcd = ""
                mShipTo_Bno = ""
                mShipTo_Bnm = ""
                mShipTo_Flno = ""
                mShipTo_Dst = ""
                mShipTo_Ph = ""
                mShipTo_Em = ""
            End If

            mVal_AssVal = IIf(IsDBNull(RsTempInv.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTempInv.Fields("TOTTAXABLEAMOUNT").Value)   ''Val(lblTotTaxableAmt.Text)
            mVal_CgstVal = IIf(IsDBNull(RsTempInv.Fields("NETCGST_AMOUNT").Value), 0, RsTempInv.Fields("NETCGST_AMOUNT").Value)   ''Val(lblTotCGSTAmount.Text)
            mVal_SgstVal = IIf(IsDBNull(RsTempInv.Fields("NETSGST_AMOUNT").Value), 0, RsTempInv.Fields("NETSGST_AMOUNT").Value)   '' Val(lblTotSGSTAmount.Text)
            mVal_IgstVal = IIf(IsDBNull(RsTempInv.Fields("NETIGST_AMOUNT").Value), 0, RsTempInv.Fields("NETIGST_AMOUNT").Value)   '' Val(lblTotIGSTAmount.Text)
            mVal_CesVal = 0
            mVal_StCesVal = 0
            mVal_CesNonAdVal = 0

            mVal_TotInvVal = IIf(IsDBNull(RsTempInv.Fields("NETVALUE").Value), 0, RsTempInv.Fields("NETVALUE").Value)   '' Val(lblNetAmount.Text)

            mMSC = IIf(IsDBNull(RsTempInv.Fields("TOTMSCAMOUNT").Value), 0, RsTempInv.Fields("TOTMSCAMOUNT").Value)
            pRO = IIf(IsDBNull(RsTempInv.Fields("TOTRO").Value), 0, RsTempInv.Fields("TOTRO").Value)
            mVal_OthChrg = CDbl(VB6.Format(mVal_TotInvVal - (mVal_AssVal + mVal_CgstVal + mVal_SgstVal + mVal_IgstVal + Val(mMSC)) - Val(pRO), "0.00")) 'Val(lblTotExpAmt.text)  ''							
            mVal_Disc = Val(mMSC) * -1

            mPay_Nam = ""
            mPay_Mode = ""
            mPay_PayTerm = ""
            mPay_PayInstr = ""
            mPay_CrDay = ""
            mPay_BalAmt = 0
            mPay_PayDueDt = ""
            mRef_InvRmk = ""
            mRef_InvStDt = ""
            mRef_InvEndDt = ""
            mTran_EcmGstin = ""



            mPay_FinInsBr = ""
            mPay_CrTrn = ""
            mPay_DirDr = ""
            mPay_AcctDet = ""
            mRef_PrecInvNo = ""
            mRef_PrecInvDt = ""
            mRef_RecAdvRef = ""
            mRef_TendRef = ""
            mRef_ContrRef = ""
            mRef_ExtRef = ""
            mRef_ProjRef = ""
            mRef_PORef = ""
            mExp_ExpCat = ""
            mExp_WthPay = ""
            mExp_InvForCur = ""


            If pInvoiceSeq = 6 Then
                mExp_ShipBNo = IIf(IsDBNull(RsTempInv.Fields("SHIPPING_NO").Value), "", RsTempInv.Fields("SHIPPING_NO").Value)
                mExp_ShipBDt = VB6.Format(IIf(IsDBNull(RsTempInv.Fields("SHIPPING_DATE").Value), "", RsTempInv.Fields("SHIPPING_DATE").Value), "DD/MM/YYYY")
                mExp_Port = IIf(IsDBNull(RsTempInv.Fields("PORT_CODE").Value), "", RsTempInv.Fields("PORT_CODE").Value) ''Trim(txtPortCode.Text)

                If MainClass.ValidateWithMasterTable(pCustomerName, "SUPP_CUST_NAME", "CURRENCY_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mExp_ForCur = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(pCustomerName, "SUPP_CUST_NAME", "COUNTRY_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mExp_CntCode = MasterNo
                End If

            Else
                mExp_ShipBNo = ""
                mExp_ShipBDt = ""
                mExp_Port = ""
                mExp_ForCur = ""
                mExp_CntCode = ""
            End If
        End If

        If pIsTesting = "Y" Then
            mBillFrom_Gstin = "03AAACW3775F010" '' IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        Else
            mBillFrom_Gstin = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        End If

        mBillFrom_TrdNm = IIf(IsDBNull(RsCompany.Fields("Company_Name").Value), "", RsCompany.Fields("Company_Name").Value)
        mBillFrom_Bno = IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)
        mBillFrom_Bnm = ""
        mBillFrom_Flno = ""
        mBillFrom_Loc = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        mBillFrom_Dst = ""
        mBillFrom_Pin = IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)
        pStateName = IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
        pStateCode = GetStateCode(pStateName)
        mBillFrom_Stcd = pStateCode
        mBillFrom_Ph = ""
        mBillFrom_Em = ""


        mGetQRImg = "0" ''0 for text , 1 for Image							
        mGetSignedInvoice = "0" ''1 - Signed Json of Invoice will be return, 0 - will not return signed Invoice.							


        HTTP.Open("POST", url, False)
        HTTP.setRequestHeader("Content-Type", "application/json")

        Dim pSqlStr As String
        Dim RsTempDet As ADODB.Recordset = Nothing

        If pInvoiceSeq = 4 Then
            xSqlStr = " SELECT IH.* " & vbCrLf _
               & " FROM FIN_INVOICE_HDR IH" & vbCrLf _
               & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
               & " And IH.MKEY='" & pMKey & "'"
        ElseIf pInvoiceSeq = 9 Then
            xSqlStr = " SELECT IH.HSNCODE, SUM(IH.ITEM_QTY) AS ITEM_QTY, IH.ITEM_UOM, IH.ITEM_RATE, SUM(IH.GSTABLE_AMT) AS GSTABLE_AMT, IH.SGST_PER, IH.CGST_PER, IH.IGST_PER, SUM(IH.SGST_AMOUNT) AS SGST_AMOUNT, SUM(IH.CGST_AMOUNT) AS CGST_AMOUNT, SUM(IH.IGST_AMOUNT) AS IGST_AMOUNT, " & vbCrLf _
               & " INVMST.ITEM_SHORT_DESC" & vbCrLf _
               & " FROM FIN_INVOICE_DET IH, INV_ITEM_MST INVMST" & vbCrLf _
               & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
               & " And IH.MKEY='" & pMKey & "'" & vbCrLf _
               & " And IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
               & " And IH.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
               & " GROUP BY IH.HSNCODE, IH.ITEM_UOM, IH.ITEM_RATE, IH.SGST_PER, IH.CGST_PER, IH.IGST_PER, INVMST.ITEM_SHORT_DESC"
        Else
            xSqlStr = " SELECT IH.*, " & vbCrLf _
                & " INVMST.ITEM_SHORT_DESC" & vbCrLf _
                & " FROM FIN_INVOICE_DET IH, INV_ITEM_MST INVMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And IH.MKEY='" & pMKey & "'" & vbCrLf _
                & " And IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                & " And IH.ITEM_CODE=INVMST.ITEM_CODE"
        End If


        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

        mBody = "{""Push_Data_List"":{"
        mBody = mBody & """Data"": ["
        If RsTempDet.EOF = False Then
            cntRow = 0
            Do While RsTempDet.EOF = False
                cntRow = cntRow + 1

                mBody = mBody & "{"
                mBody = mBody & """Gstin"":""" & mGSTIN & ""","
                mBody = mBody & """Version"":""" & mVersion & ""","
                mBody = mBody & """Irn"":""" & mIrn & ""","
                mBody = mBody & """Tran_TaxSch"":""" & mTaxSch & ""","

                mBody = mBody & """Tran_SupTyp"":""" & mTran_Catg & ""","
                mBody = mBody & """Tran_RegRev"":""" & mTran_RegRev & ""","
                mBody = mBody & """Tran_Typ"":""" & mTran_Typ & ""","

                mBody = mBody & """Tran_EcmGstin"":""" & mTran_EcmGstin & ""","
                mBody = mBody & """Tran_IgstOnIntra"":""" & "N" & ""","
                mBody = mBody & """Doc_Typ"":""" & mDoc_Typ & ""","
                mBody = mBody & """DOC_NO"":""" & mDOC_NO & ""","
                mBody = mBody & """Doc_Dt"":""" & mDoc_Dt & ""","
                mBody = mBody & """BillFrom_Gstin"":""" & mBillFrom_Gstin & ""","
                mBody = mBody & """BillFrom_LglNm"":""" & mBillFrom_TrdNm & ""","
                mBody = mBody & """BillFrom_TrdNm"":""" & mBillFrom_TrdNm & ""","

                mBody = mBody & """BillFrom_Addr1"":""" & mBillFrom_Bno & ""","
                mBody = mBody & """BillFrom_Addr2"":""" & mBillFrom_Bnm & ""","
                mBody = mBody & """BillFrom_Loc"":""" & mBillFrom_Loc & ""","
                mBody = mBody & """BillFrom_Pin"":""" & mBillFrom_Pin & ""","
                mBody = mBody & """BillFrom_Stcd"":""" & mBillFrom_Stcd & ""","
                mBody = mBody & """BillFrom_Ph"":""" & mBillFrom_Ph & ""","
                mBody = mBody & """BillFrom_Em"":""" & mBillFrom_Em & ""","

                mBody = mBody & """BillTo_Gstin"":""" & mBillTo_Gstin & ""","
                mBody = mBody & """BillTo_LglNm"":""" & mBillTo_TrdNm & ""","
                mBody = mBody & """BillTo_TrdNm"":""" & mBillTo_TrdNm & ""","

                mBody = mBody & """BillTo_Pos"":""" & mBillTo_Stcd & ""","
                mBody = mBody & """BillTo_Addr1"":""" & mBillTo_Bno & ""","
                mBody = mBody & """BillTo_Addr2"":""" & mBillTo_Bnm & ""","
                mBody = mBody & """BillTo_Loc"":""" & mBillTo_Loc & ""","
                mBody = mBody & """BillTo_Pin"":""" & mBillTo_Pin & ""","
                mBody = mBody & """BillTo_Stcd"":""" & mBillTo_Stcd & ""","
                mBody = mBody & """BillTo_Ph"":""" & mBillTo_Ph & ""","
                mBody = mBody & """BillTo_Em"":""" & mBillTo_Em & ""","

                If pInvoiceSeq = 4 Then
                    mItem_PrdNm = IIf(IsDBNull(RsTempDet.Fields("REMARKS").Value), "", RsTempDet.Fields("REMARKS").Value)  'Trim(SprdMain.Text)
                    mItem_PrdDesc = IIf(IsDBNull(RsTempDet.Fields("REMARKS").Value), "", RsTempDet.Fields("REMARKS").Value)  ' Trim(SprdMain.Text)

                    mItem_HsnCd = IIf(IsDBNull(RsTempDet.Fields("SAC_CODE").Value), "", RsTempDet.Fields("SAC_CODE").Value)  'Trim(SprdMain.Text)

                    mItem_Barcde = ""

                    mItem_Qty = 1
                    mItem_FreeQty = 0

                    mItem_Unit = "NOS"
                    mItem_UnitPrice = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("ITEMVALUE").Value), 0, RsTempDet.Fields("ITEMVALUE").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_TotAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTempDet.Fields("TOTTAXABLEAMOUNT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_AssAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTempDet.Fields("TOTTAXABLEAMOUNT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

                    mItem_Discount = 0

                    mItem_OthChrg = mItem_AssAmt - mItem_TotAmt
                    mItem_OthChrg = CDbl(VB6.Format(mItem_OthChrg, "0.00"))
                    mItem_SgstRt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("NET_SGST_PER").Value), 0, RsTempDet.Fields("NET_SGST_PER").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_CgstRt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("NET_CGST_PER").Value), 0, RsTempDet.Fields("NET_CGST_PER").Value), "0.00") ''  CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_IgstRt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("NET_IGST_PER").Value), 0, RsTempDet.Fields("NET_IGST_PER").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_SgstAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("NETSGST_AMOUNT").Value), 0, RsTempDet.Fields("NETSGST_AMOUNT").Value), "0.00") ''CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_CgstAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("NETCGST_AMOUNT").Value), 0, RsTempDet.Fields("NETCGST_AMOUNT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_IgstAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("NETIGST_AMOUNT").Value), 0, RsTempDet.Fields("NETIGST_AMOUNT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

                Else
                    mItem_PrdNm = IIf(IsDBNull(RsTempDet.Fields("ITEM_SHORT_DESC").Value), "", RsTempDet.Fields("ITEM_SHORT_DESC").Value)  'Trim(SprdMain.Text)
                    mItem_PrdNm = MainClass.AllowSingleQuote(mItem_PrdNm)
                    mItem_PrdNm = MainClass.AllowDoubleQuote(mItem_PrdNm)
                    mItem_PrdNm = Replace(mItem_PrdNm, Chr(34), "")
                    mItem_PrdNm = Replace(mItem_PrdNm, "'", "")

                    mItem_PrdDesc = IIf(IsDBNull(RsTempDet.Fields("ITEM_SHORT_DESC").Value), "", RsTempDet.Fields("ITEM_SHORT_DESC").Value)  ' Trim(SprdMain.Text)
                    mItem_PrdDesc = MainClass.AllowSingleQuote(mItem_PrdDesc)
                    mItem_PrdDesc = MainClass.AllowDoubleQuote(mItem_PrdDesc)
                    mItem_PrdDesc = Replace(mItem_PrdDesc, Chr(34), "")
                    mItem_PrdDesc = Replace(mItem_PrdDesc, "'", "")

                    mItem_HsnCd = IIf(IsDBNull(RsTempDet.Fields("HSNCODE").Value), "", RsTempDet.Fields("HSNCODE").Value)  'Trim(SprdMain.Text)

                    mItem_Barcde = ""

                    mItem_Qty = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("ITEM_QTY").Value), 0, RsTempDet.Fields("ITEM_QTY").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_FreeQty = 0

                    mItem_Unit = IIf(IsDBNull(RsTempDet.Fields("ITEM_UOM").Value), "", RsTempDet.Fields("ITEM_UOM").Value)  ' Trim(SprdMain.Text)
                    mItem_UnitPrice = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("ITEM_RATE").Value), 0, RsTempDet.Fields("ITEM_RATE").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_TotAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("GSTABLE_AMT").Value), 0, RsTempDet.Fields("GSTABLE_AMT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_AssAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("GSTABLE_AMT").Value), 0, RsTempDet.Fields("GSTABLE_AMT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

                    mItem_Discount = 0

                    mItem_OthChrg = mItem_AssAmt - mItem_TotAmt
                    mItem_OthChrg = CDbl(VB6.Format(mItem_OthChrg, "0.00"))
                    mItem_SgstRt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("SGST_PER").Value), 0, RsTempDet.Fields("SGST_PER").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_CgstRt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("CGST_PER").Value), 0, RsTempDet.Fields("CGST_PER").Value), "0.00") ''  CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_IgstRt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("IGST_PER").Value), 0, RsTempDet.Fields("IGST_PER").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_SgstAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("SGST_AMOUNT").Value), 0, RsTempDet.Fields("SGST_AMOUNT").Value), "0.00") ''CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_CgstAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("CGST_AMOUNT").Value), 0, RsTempDet.Fields("CGST_AMOUNT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_IgstAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("IGST_AMOUNT").Value), 0, RsTempDet.Fields("IGST_AMOUNT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

                End If

                mItem_CesRt = 0
                mItem_CesNonAdval = 0
                mItem_StateCes = 0
                mItem_TotItemVal = mItem_TotAmt + mItem_SgstAmt + mItem_CgstAmt + mItem_IgstAmt + mItem_CesNonAdval + mItem_StateCes + mItem_OthChrg ''- mItem_Discount '' mItem_AssAmt 30/09' (mItem_AssAmt * ((100 + mItem_SgstRt + mItem_CgstRt + mItem_IgstRt + mItem_CesRt + mItem_StateCes) * 0.01)) + mItem_CesNonAdval		
                mItem_TotItemVal = CDbl(VB6.Format(mItem_TotItemVal, "0.00"))

                mBody = mBody & """Item_SlNo"":""" & cntRow & ""","

                mBody = mBody & """Item_PrdDesc"":""" & mItem_PrdDesc & ""","
                mBody = mBody & """Item_IsServc"":""" & IIf(CDbl(pInvoiceSeq) = 2 Or (pInvoiceSeq) = 4, "Y", "N") & ""","
                mBody = mBody & """Item_HsnCd"":""" & mItem_HsnCd & ""","
                mBody = mBody & """Item_Barcde"":""" & mItem_Barcde & ""","
                mBody = mBody & """Item_Qty"":""" & mItem_Qty & ""","
                mBody = mBody & """Item_FreeQty"":""" & mItem_FreeQty & ""","
                mBody = mBody & """Item_Unit"":""" & mItem_Unit & ""","
                mBody = mBody & """Item_UnitPrice"":""" & mItem_UnitPrice & ""","
                mBody = mBody & """Item_TotAmt"":""" & mItem_TotAmt & ""","
                mBody = mBody & """Item_Discount"":""" & mItem_Discount & ""","
                mBody = mBody & """Item_PreTaxVal"":""" & mItem_TotAmt & ""","
                mBody = mBody & """Item_AssAmt"":""" & mItem_AssAmt & ""","
                mBody = mBody & """Item_GstRt"":""" & mItem_CgstRt + mItem_SgstRt + mItem_IgstRt & ""","

                mBody = mBody & """Item_IgstAmt"":""" & mItem_IgstAmt & ""","
                mBody = mBody & """Item_CgstAmt"":""" & mItem_CgstAmt & ""","
                mBody = mBody & """Item_SgstAmt"":""" & mItem_SgstAmt & ""","
                mBody = mBody & """Item_CesRt"":""" & mItem_CesRt & ""","
                mBody = mBody & """Item_CesAmt"":""" & "" & ""","
                mBody = mBody & """Item_CesNonAdvlAmt"":""" & mItem_CesNonAdval & ""","

                mBody = mBody & """Item_StateCesRt"":""" & "" & ""","
                mBody = mBody & """Item_StateCesAmt"":""" & "" & ""","
                mBody = mBody & """Item_StateCesNonAdvlAmt"":""" & "" & ""","
                mBody = mBody & """Item_OthChrg"":""" & mItem_OthChrg & ""","
                mBody = mBody & """Item_TotItemVal"":""" & mItem_TotItemVal & ""","

                mBody = mBody & """Item_OrdLineRef"":""" & "" & ""","
                mBody = mBody & """Item_OrgCntry"":""" & "" & ""","
                mBody = mBody & """Item_PrdSlNo"":""" & "" & ""","
                mBody = mBody & """Item_Attrib_Nm"":""" & "" & ""","
                mBody = mBody & """Item_Attrib_Val"":""" & "" & ""","

                mBody = mBody & """Item_Bch_Nm"":""" & mItem_Bch_Nm & ""","
                mBody = mBody & """Item_Bch_ExpDt"":""" & mItem_Bch_ExpDt & ""","
                mBody = mBody & """Item_Bch_WrDt"":""" & mItem_Bch_WrDt & ""","
                mBody = mBody & """Val_AssVal"":""" & mVal_AssVal & ""","
                mBody = mBody & """Val_CgstVal"":""" & mVal_CgstVal & ""","
                mBody = mBody & """Val_SgstVal"":""" & mVal_SgstVal & ""","

                mBody = mBody & """Val_IgstVal"":""" & mVal_IgstVal & ""","
                mBody = mBody & """Val_CesVal"":""" & mVal_CesVal & ""","
                mBody = mBody & """Val_StCesVal"":""" & mVal_StCesVal & ""","
                mBody = mBody & """Val_Discount"":""" & mVal_Disc & ""","
                mBody = mBody & """Val_OthChrg"":""" & mVal_OthChrg & ""","
                mBody = mBody & """Val_RndOffAmt"":""" & VB6.Format(Val(pRO), "0.00") & ""","


                mBody = mBody & """Val_TotInvVal"":""" & mVal_TotInvVal & ""","
                mBody = mBody & """Val_TotInvValFc"":""" & "" & ""","

                mBody = mBody & """Pay_Nm"":""" & mPay_Nam & ""","
                mBody = mBody & """Pay_AcctDet"":""" & mPay_AcctDet & ""","
                mBody = mBody & """Pay_Mode"":""" & mPay_Mode & ""","
                mBody = mBody & """Pay_FinInsBr"":""" & mPay_FinInsBr & ""","

                mBody = mBody & """Pay_PayTerm"":""" & mPay_PayTerm & ""","
                mBody = mBody & """Pay_PayInstr"":""" & mPay_PayInstr & ""","
                mBody = mBody & """Pay_CrTrn"":""" & mPay_CrTrn & ""","
                mBody = mBody & """Pay_DirDr"":""" & mPay_DirDr & ""","
                mBody = mBody & """Pay_CrDay"":""" & mPay_CrDay & ""","
                mBody = mBody & """Pay_PaidAmt"":""" & "" & ""","

                mBody = mBody & """Pay_BalAmt"":""" & mPay_BalAmt & ""","
                mBody = mBody & """Pay_PaymtDue"":""" & mPay_PayDueDt & ""","
                mBody = mBody & """Ref_InvRmk"":""" & mRef_InvRmk & ""","
                mBody = mBody & """Ref_InvStDt"":""" & mRef_InvStDt & ""","
                mBody = mBody & """Ref_InvEndDt"":""" & mRef_InvEndDt & ""","
                mBody = mBody & """Doc_OrgInvNo"":""" & mDoc_OrgInvNo & ""","

                mBody = mBody & """ShipFrom_Gstin"":""" & mShipFrom_Gstin & ""","
                mBody = mBody & """ShipFrom_Nm"":""" & mShipFrom_TrdNm & ""","
                mBody = mBody & """ShipFrom_Addr1"":""" & mShipFrom_Bno & ""","
                mBody = mBody & """ShipFrom_Addr2"":""" & mShipFrom_Bnm & ""","
                mBody = mBody & """ShipFrom_Loc"":""" & mShipFrom_Loc & ""","
                mBody = mBody & """ShipFrom_Pin"":""" & mShipFrom_Pin & ""","
                mBody = mBody & """ShipFrom_Stcd"":""" & mShipFrom_Stcd & ""","


                mBody = mBody & """ShipTo_Gstin"":""" & mShipTo_Gstin & ""","
                mBody = mBody & """ShipTo_LglNm"":""" & mShipTo_TrdNm & ""","
                mBody = mBody & """ShipTo_TrdNm"":""" & mShipTo_TrdNm & ""","
                mBody = mBody & """ShipTo_Addr1"":""" & mShipTo_Bno & ""","
                mBody = mBody & """ShipTo_Addr2"":""" & mShipTo_Loc & ""","
                mBody = mBody & """ShipTo_Loc"":""" & mShipTo_Loc & ""","
                mBody = mBody & """ShipTo_Pin"":""" & mShipTo_Pin & ""","
                mBody = mBody & """ShipTo_Stcd"":""" & mShipTo_Stcd & ""","

                mBody = mBody & """Ref_PrecDoc_InvNo"":""" & mRef_PrecInvNo & ""","
                mBody = mBody & """Ref_PrecDoc_InvDt"":""" & mRef_PrecInvDt & ""","
                mBody = mBody & """Ref_PrecDoc_OthRefNo"":""" & "" & ""","


                mBody = mBody & """Ref_Contr_RecAdvRefr"":""" & mRef_RecAdvRef & ""","
                mBody = mBody & """Ref_Contr_RecAdvDt"":""" & "" & ""","

                mBody = mBody & """Ref_Contr_TendRefr"":""" & mRef_TendRef & ""","
                mBody = mBody & """Ref_Contr_ContrRefr"":""" & mRef_ContrRef & ""","
                mBody = mBody & """Ref_Contr_ExtRefr"":""" & mRef_ProjRef & ""","
                mBody = mBody & """Ref_Contr_ProjRefr"":""" & "" & ""","

                mBody = mBody & """Ref_Contr_PORefr"":""" & "" & ""","
                mBody = mBody & """Ref_Contr_PORefDt"":""" & "" & ""","

                mBody = mBody & """AddlDoc_Url"":""" & "" & ""","
                mBody = mBody & """AddlDoc_Docs"":""" & "" & ""","
                mBody = mBody & """AddlDoc_Info"":""" & "" & ""","


                mBody = mBody & """Ewb_TransId"":""" & "" & ""","
                mBody = mBody & """Ewb_TransName"":""" & "" & ""","
                mBody = mBody & """Ewb_TransMode"":""" & "" & ""","
                mBody = mBody & """Ewb_Distance"":""" & "0" & ""","
                mBody = mBody & """Ewb_TransDocNo"":""" & "" & ""","
                mBody = mBody & """Ewb_TransDocDt"":""" & "" & ""","
                mBody = mBody & """Ewb_VehNo"":""" & "" & ""","
                mBody = mBody & """Ewb_VehType"":""" & "" & ""","



                mBody = mBody & """Exp_ForCur"":""" & mExp_ForCur & ""","
                mBody = mBody & """Exp_CntCode"":""" & mExp_CntCode & ""","
                mBody = mBody & """Exp_ShipBNo"":""" & mExp_ShipBNo & ""","
                mBody = mBody & """Exp_ShipBDt"":""" & mExp_ShipBDt & ""","
                mBody = mBody & """Exp_Port"":""" & mExp_Port & ""","

                mBody = mBody & """CDKey"":""" & mCDKey & ""","
                mBody = mBody & """EInvUserName"":""" & mEInvUserName & ""","
                mBody = mBody & """EInvPassword"":""" & mEInvPassword & ""","
                mBody = mBody & """EFUserName"":""" & mEFUserName & ""","
                mBody = mBody & """EFPassword"":""" & mEFPassword & """"

                RsTempDet.MoveNext()
                If RsTempDet.EOF = True Then
                    mBody = mBody & "}"
                Else
                    mBody = mBody & "},"
                End If

            Loop
        End If

        mBody = mBody & "]"
        mBody = mBody & "}"
        mBody = mBody & "}"

        ' shipToGSTIN String GSTIN of  Ship-To shipToTradeName String Trade Name of  Ship-To dispatchFromGSTIN String GSTIN of Dispatch-From dispatchFromTradeName String Trade Name of Dispatch-From IsBillFromShipFromSame String Required, 0 for Different BillFrom and ShipFrom, 1 for Same BillFrom and ShipFrom IsBillToShipToSame String Required, 0 for Different BillTo and ShipTo, 1 for Same BillTo and ShipTo IsGSTINSEZ							

        'Dim feed = JsonSerializer.Deserialize(Of query)(JSON)

        'Dim strserialize As String = JsonConvert.SerializeObject(mBody)

        HTTP.Send(mBody)

        pResponseText = HTTP.responseText

        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        pResponseText = Replace(pResponseText, """", "'")

        Dim post As Object
        pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Status = ""})).Status

        If pStaus = "1" Then
            mIRNNo = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Irn = ""})).Irn   'JsonTest.Item("Irn")
            mIRNAckNo = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .AckNo = ""})).AckNo 'JsonTest.Item("AckNo")
            mIRNAckDate = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .AckDate = ""})).AckDate ' JsonTest.Item("AckDate") 'JsonTest.Item("elements").Item(mResponseId).Item("ewayBillDate")						
            mSignedQRCode = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .SignedQRCode = ""})).SignedQRCode ' JsonTest.Item("SignedQRCode")
            mSignedInvoice = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .SignedInvoice = ""})).SignedInvoice ' JsonTest.Item("SignedInvoice")

            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            Dim SqlStr As String = ""

            SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
                    & " IRN_NO ='" & Trim(mIRNNo) & "'," & vbCrLf _
                    & " IRN_ACK_NO ='" & Trim(mIRNAckNo) & "'," & vbCrLf _
                    & " IRN_ACK_DATE =TO_DATE('" & VB6.Format(mIRNAckDate, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY ='" & pMKey & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = "DELETE FROM FIN_INVOICE_QRCODE WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY ='" & pMKey & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = "INSERT INTO FIN_INVOICE_QRCODE " & vbCrLf _
                    & " ( MKEY, COMPANY_CODE, SIGNQRCODE ) VALUES (" & vbCrLf _
                    & " '" & pMKey & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " '" & mSignedQRCode & "')"

            PubDBCn.Execute(SqlStr)

            PubDBCn.CommitTrans()

            WebRequestGenerateIRN = mIRNNo

            'mBMPFileName = mPubBarCodePath & "\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".bmp"

            'If GererateQRCodeImage(mBMPFileName, mSignedQRCode) = False Then GoTo ErrPart

            'If UpdateQRCODE(CDbl(LblMKey.Text), mBMPFileName) = False Then GoTo ErrPart

        End If

        If pStaus = "0" Then
            pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            'MsgInformation(pError)
            WebRequestGenerateIRN = pError
            HTTP = Nothing
            Exit Function
        End If

        HTTP = Nothing

        Exit Function
ErrPart:
        '    Resume							
        WebRequestGenerateIRN = Err.Description
        'http = Nothing							
        'MsgBox(Err.Description)
        PubDBCn.RollbackTrans()
    End Function
    Public Function WebRequestGenerateCRIRN(ByRef pMKey As String, pInvoiceSeq As Long, pCustomerName As String) As String

        On Error GoTo ErrPart
        Dim url As String = ""

        Dim mGSTIN As String
        Dim mTaxSch As String
        Dim mVersion As String
        Dim mIrn As String
        Dim mTran_Catg As String
        Dim mTran_RegRev As String
        Dim mTran_Typ As String = ""
        Dim mTran_EcmTrn As String
        Dim mTran_EcmGstin As String
        Dim mDoc_Typ As String
        Dim mDOC_NO As String
        Dim mDoc_Dt As String
        Dim mBillFrom_Gstin As String
        Dim mBillFrom_TrdNm As String
        Dim mBillFrom_Bno As String
        Dim mBillFrom_Bnm As String
        Dim mBillFrom_Flno As String
        Dim mBillFrom_Loc As String
        Dim mBillFrom_Dst As String
        Dim mBillFrom_Pin As String
        Dim mBillFrom_Stcd As String
        Dim mBillFrom_Ph As String
        Dim mBillFrom_Em As String
        Dim mBillTo_Gstin As String
        Dim mBillTo_TrdNm As String
        Dim mBillTo_Bno As String
        Dim mBillTo_Bnm As String
        Dim mBillTo_Flno As String
        Dim mBillTo_Loc As String
        Dim mBillTo_Dst As String
        Dim mBillTo_Pin As String
        Dim mBillTo_Stcd As String
        Dim mBillTo_Ph As String
        Dim mBillTo_Em As String
        Dim mToPlace As String
        Dim mItem_PrdNm As String
        Dim mItem_PrdDesc As String
        Dim mItem_HsnCd As String
        Dim mItem_Barcde As String
        Dim mItem_Qty As Double
        Dim mItem_FreeQty As Double
        Dim mItem_Unit As String
        Dim mItem_UnitPrice As Double
        Dim mItem_TotAmt As Double
        Dim mItem_Discount As Double
        Dim mItem_OthChrg As Double
        Dim mItem_AssAmt As Double
        Dim mItem_CgstRt As Double
        Dim mItem_SgstRt As Double
        Dim mItem_IgstRt As Double

        Dim mItem_CgstAmt As Double
        Dim mItem_SgstAmt As Double
        Dim mItem_IgstAmt As Double

        Dim mItem_CesRt As Double
        Dim mItem_CesNonAdval As Double
        Dim mItem_StateCes As Double
        Dim mItem_TotItemVal As Double
        Dim mItem_Bch_Nm As String
        Dim mItem_Bch_ExpDt As String
        Dim mItem_Bch_WrDt As String
        Dim mVal_AssVal As Double
        Dim mVal_CgstVal As Double
        Dim mVal_SgstVal As Double
        Dim mVal_IgstVal As Double
        Dim mVal_CesVal As Double
        Dim mVal_StCesVal As Double
        Dim mVal_CesNonAdVal As Double
        Dim mVal_Disc As Double
        Dim mVal_OthChrg As Double
        Dim mVal_TotInvVal As Double
        Dim mPay_Nam As String
        Dim mPay_Mode As String
        Dim mPay_PayTerm As String
        Dim mPay_PayInstr As String
        Dim mPay_CrDay As String
        Dim mPay_BalAmt As Double
        Dim mPay_PayDueDt As String
        Dim mRef_InvRmk As String
        Dim mRef_InvStDt As String
        Dim mRef_InvEndDt As String
        'Dim mTran_EcmGstin As String							
        Dim mDoc_OrgInvNo As String
        Dim mShipFrom_Gstin As String
        Dim mShipFrom_TrdNm As String
        Dim mShipFrom_Loc As String
        Dim mShipFrom_Pin As String
        Dim mShipFrom_Stcd As String
        Dim mShipFrom_Bno As String
        Dim mShipFrom_Bnm As String
        Dim mShipFrom_Flno As String
        Dim mShipFrom_Dst As String
        Dim mShipFrom_Ph As String
        Dim mShipFrom_Em As String
        Dim mStateName As String
        Dim mShipTo_Gstin As String
        Dim mShipTo_TrdNm As String
        Dim mShipTo_Loc As String
        Dim mShipTo_Pin As String
        Dim mShipTo_Stcd As String
        Dim mShipTo_Bno As String
        Dim mShipTo_Bnm As String
        Dim mShipTo_Flno As String
        Dim mShipTo_Dst As String
        Dim mShipTo_Ph As String
        Dim mShipTo_Em As String
        Dim mPay_FinInsBr As String
        Dim mPay_CrTrn As String
        Dim mPay_DirDr As String
        Dim mPay_AcctDet As String
        Dim mRef_PrecInvNo As String
        Dim mRef_PrecInvDt As String
        Dim mRef_RecAdvRef As String
        Dim mRef_TendRef As String
        Dim mRef_ContrRef As String
        Dim mRef_ExtRef As String
        Dim mRef_ProjRef As String
        Dim mRef_PORef As String
        Dim mExp_ExpCat As String
        Dim mExp_WthPay As String
        Dim mExp_InvForCur As String
        Dim mExp_ForCur As String
        Dim mExp_CntCode As String
        Dim mExp_ShipBNo As String
        Dim mExp_ShipBDt As String
        Dim mExp_Port As String
        Dim mGetQRImg As String
        Dim mGetSignedInvoice As String = ""
        Dim mCDKey As String = ""
        Dim mEInvUserName As String = ""
        Dim mEInvPassword As String = ""
        Dim mEFUserName As String = ""
        Dim mEFPassword As String = ""

        Dim pStateName As String
        Dim pStateCode As String
        Dim cntRow As Integer

        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mBody As String
        Dim mResponseId As String
        Dim mResponseIdStr As String
        Dim url1 As String
        Dim WebRequestGen As String
        Dim pStaus As String

        Dim mIRNNo As String
        Dim mIRNAckNo As String
        Dim mIRNAckDate As String

        Dim pError As String
        Dim mMSC As Double
        Dim pRO As Double
        Dim mIsServices As String

        Dim mSignedQRCode As String
        Dim mSignedInvoice As String
        'Dim pUserId As String							
        Dim mBMPFileName As String
        Dim pIsTesting As String = "Y"

        Dim pResponseText As String
        Dim mEwb_TransId As String
        Dim mEwb_TransName As String
        Dim mEwb_TransMode As String
        Dim mEwb_Distance As String
        Dim mEwb_TransDocNo As String
        Dim mEwb_TransDocDt As String
        Dim mEwb_VehNo As String
        Dim mEwb_VehType As String
        Dim mItem_IsServc As String

        If GeteInvoiceSetupContents(url, "G", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword, pIsTesting) = False Then GoTo ErrPart

        If pIsTesting = "Y" Then
            url = "http://einvsandbox.webtel.in/v1.03/GenIRN"
            mCDKey = "1000687"
            mGSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
            If VB.Left(mGSTIN, 2) = "03" Then
                mEInvUserName = "03AAACW3775F010"       ''"06AAACW3775F013"		 "29AAACW3775F000" '' 					
                mEInvPassword = "Admin!23"  ''"Admin!23.."    ''
                mEFUserName = "29AAACW3775F000"  '' "29AAACW3775F000"
                mEFPassword = "Admin!23.."
                mGSTIN = "03AAACW3775F010" ''IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
            Else
                mEInvUserName = "06AAACW3775F010"       ''"06AAACW3775F013"		 "29AAACW3775F000" '' 					
                mEInvPassword = "Admin!23"  ''"Admin!23.."    ''
                mEFUserName = "29AAACW3775F000"  '' "29AAACW3775F000"
                mEFPassword = "Admin!23.."
                mGSTIN = "06AAACW3775F010" ''IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
            End If

        Else
            mGSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        End If

        Dim HTTP As Object
        HTTP = CreateObject("MSXML2.ServerXMLHTTP")



        mTaxSch = "GST"
        mVersion = "1.0"
        mIrn = ""

        Dim xSqlStr As String = ""
        Dim RsTempInv As ADODB.Recordset = Nothing

        If pInvoiceSeq = "2" Then
            xSqlStr = " Select IH.*, " & vbCrLf _
                & " BMST.LOCATION_ID, BMST.SUPP_CUST_NAME, BMST.SUPP_CUST_ADDR, BMST.SUPP_CUST_CITY, " & vbCrLf _
                & " BMST.SUPP_CUST_STATE, BMST.SUPP_CUST_PIN, BMST.SUPP_CUST_PHONE, BMST.SUPP_CUST_FAXNO, " & vbCrLf _
                & " BMST.SUPP_CUST_MAILID, BMST.SUPP_CUST_MOBILE, BMST.COUNTRY, BMST.GST_RGN_NO " & vbCrLf _
                & " FROM FIN_PURCHASE_HDR IH, FIN_SUPP_CUST_BUSINESS_MST BMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And IH.MKEY='" & pMKey & "'" & vbCrLf _
                & " And IH.COMPANY_CODE=BMST.COMPANY_CODE" & vbCrLf _
                & " And IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
                & " And IH.BILL_TO_LOC_ID=BMST.LOCATION_ID"

        Else

            xSqlStr = "Select IH.*, " & vbCrLf _
                & " BMST.LOCATION_ID, BMST.SUPP_CUST_NAME, BMST.SUPP_CUST_ADDR, BMST.SUPP_CUST_CITY, " & vbCrLf _
                & " BMST.SUPP_CUST_STATE, BMST.SUPP_CUST_PIN, BMST.SUPP_CUST_PHONE, BMST.SUPP_CUST_FAXNO, " & vbCrLf _
                & " BMST.SUPP_CUST_MAILID, BMST.SUPP_CUST_MOBILE, BMST.COUNTRY, BMST.GST_RGN_NO " & vbCrLf _
                & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_CUST_BUSINESS_MST BMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And IH.MKEY='" & pMKey & "'" & vbCrLf _
                & " And IH.COMPANY_CODE=BMST.COMPANY_CODE" & vbCrLf _
                & " And IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
                & " And IH.BILL_TO_LOC_ID=BMST.LOCATION_ID "

            If chkCreditNote.Checked = True Then
                xSqlStr = xSqlStr & " AND IH.BOOKTYPE='L'"
            Else
                xSqlStr = xSqlStr & " AND IH.BOOKTYPE='M'"
            End If
        End If



        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempInv, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempInv.EOF = False Then
            Dim mISLUT As String = ""
            Dim mDespatchFrom As String = ""
            Dim mShipTo As String = ""

            mTran_Catg = "B2B"


            mTran_RegRev = "N"

            mDespatchFrom = "N"
            mShipTo = "Y"

            If mDespatchFrom = "N" And mShipTo = "Y" Then
                mTran_Typ = "REG"
            ElseIf mDespatchFrom = "N" And mShipTo = "N" Then
                mTran_Typ = "SHP"
            ElseIf mDespatchFrom = "Y" And mShipTo = "Y" Then
                mTran_Typ = "DIS"
            ElseIf mDespatchFrom = "Y" And mShipTo = "N" Then
                mTran_Typ = "CMB"
            End If

            mTran_EcmTrn = "N"
            mTran_EcmGstin = ""


            mDoc_Typ = IIf(chkCreditNote.Checked = True, "CRN", "DBN")
            mIsServices = "G"

            If pInvoiceSeq = "2" Then
                mDOC_NO = IIf(IsDBNull(RsTempInv.Fields("REJ_CREDITNOTE").Value), "", RsTempInv.Fields("REJ_CREDITNOTE").Value)
            Else
                mIsServices = IIf(IsDBNull(RsTempInv.Fields("GOODS_SERVICE").Value), "G", RsTempInv.Fields("GOODS_SERVICE").Value)
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 103 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                    mDOC_NO = IIf(IsDBNull(RsTempInv.Fields("PARTY_DNCN_NO").Value), "", RsTempInv.Fields("PARTY_DNCN_NO").Value)
                Else
                    mDOC_NO = IIf(IsDBNull(RsTempInv.Fields("VNO").Value), "", RsTempInv.Fields("VNO").Value)
                End If

            End If

            mItem_IsServc = IIf(mIsServices = "G", "N", "Y")

            mDoc_Dt = VB6.Format(IIf(IsDBNull(RsTempInv.Fields("VDATE").Value), "", RsTempInv.Fields("VDATE").Value), "DD/MM/YYYY")
            mDoc_OrgInvNo = ""

            mBillTo_TrdNm = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_NAME").Value), "", RsTempInv.Fields("SUPP_CUST_NAME").Value) '
            mBillTo_Bno = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_ADDR").Value), "", RsTempInv.Fields("SUPP_CUST_ADDR").Value)
            mBillTo_Bnm = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_CITY").Value), "", RsTempInv.Fields("SUPP_CUST_CITY").Value)
            mBillTo_Flno = ""
            mBillTo_Loc = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_CITY").Value), "", RsTempInv.Fields("SUPP_CUST_CITY").Value)
            mBillTo_Dst = ""
            mBillTo_Ph = ""
            mBillTo_Em = ""
            mToPlace = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_STATE").Value), "", RsTempInv.Fields("SUPP_CUST_STATE").Value)

            mEwb_TransId = ""   ''IIf(IsDBNull(RsTempInv.Fields("TRANSPORTER_GSTNO").Value), "", RsTempInv.Fields("TRANSPORTER_GSTNO").Value)
            mEwb_TransName = ""  '' IIf(IsDBNull(RsTempInv.Fields("CARRIERS").Value), "", RsTempInv.Fields("CARRIERS").Value)
            mEwb_TransMode = ""  '' IIf(IsDBNull(RsTempInv.Fields("TRANSPORT_MODE").Value), 0, VB.Left(RsTempInv.Fields("TRANSPORT_MODE").Value, 1))
            mEwb_Distance = ""   '' IIf(IsDBNull(RsTempInv.Fields("TRANS_DISTANCE").Value), 0, RsTempInv.Fields("TRANS_DISTANCE").Value)
            mEwb_TransDocNo = "" '' IIf(IsDBNull(RsTempInv.Fields("GRNO").Value), "", RsTempInv.Fields("GRNO").Value)
            mEwb_TransDocDt = "" '' VB6.Format(IIf(IsDBNull(RsTempInv.Fields("GRDATE").Value), "", RsTempInv.Fields("GRDATE").Value), "DD/MM/YYYY")
            mEwb_VehNo = ""   ''= IIf(IsDBNull(RsTempInv.Fields("VEHICLENO").Value), "", RsTempInv.Fields("VEHICLENO").Value)
            mEwb_VehType = ""    '' IIf(IsDBNull(RsTempInv.Fields("VEHICLE_TYPE").Value), 0, VB.Left(RsTempInv.Fields("VEHICLE_TYPE").Value, 1))


            mBillTo_Gstin = IIf(IsDBNull(RsTempInv.Fields("GST_RGN_NO").Value), "", RsTempInv.Fields("GST_RGN_NO").Value)
            mBillTo_Pin = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_PIN").Value), "", RsTempInv.Fields("SUPP_CUST_PIN").Value)
            mBillTo_Stcd = GetStateCode(mToPlace)

            Dim mShippFrom As String
            mShippFrom = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_CODE").Value), "", RsTempInv.Fields("SUPP_CUST_CODE").Value) '    ''IIf(IsDBNull(RsTempInv.Fields("SHIPPED_FROM_PARTY_CODE").Value), "", RsTempInv.Fields("SHIPPED_FROM_PARTY_CODE").Value)

            Dim mShippTo As String
            mShippTo = IIf(IsDBNull(RsTempInv.Fields("SUPP_CUST_CODE").Value), "", RsTempInv.Fields("SUPP_CUST_CODE").Value) '    ' IIf(IsDBNull(RsTempInv.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTempInv.Fields("SHIPPED_TO_PARTY_CODE").Value)

            Dim mShippToLoc As String
            mShippToLoc = IIf(IsDBNull(RsTempInv.Fields("BILL_TO_LOC_ID").Value), "", RsTempInv.Fields("BILL_TO_LOC_ID").Value) '    'IIf(IsDBNull(RsTempInv.Fields("SHIP_TO_LOC_ID").Value), "", RsTempInv.Fields("SHIP_TO_LOC_ID").Value)

            If mDespatchFrom = "Y" Then
                mSqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf _
                & " FROM FIN_SUPP_CUST_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShippFrom) & "'"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mShipFrom_Gstin = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
                    mShipFrom_TrdNm = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
                    mShipFrom_Loc = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                    mShipFrom_Pin = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
                    mStateName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
                    mShipFrom_Stcd = GetStateCode(mStateName)
                    mShipFrom_Bno = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                    mShipFrom_Bnm = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                    mShipFrom_Flno = ""
                    mShipFrom_Dst = ""
                    mShipFrom_Ph = ""
                    mShipFrom_Em = ""
                End If
            Else
                mShipFrom_Gstin = ""
                mShipFrom_TrdNm = ""
                mShipFrom_Loc = ""
                mShipFrom_Pin = ""
                mShipFrom_Stcd = ""
                mShipFrom_Bno = ""
                mShipFrom_Bnm = ""
                mShipFrom_Flno = ""
                mShipFrom_Dst = ""
                mShipFrom_Ph = ""
                mShipFrom_Em = ""
            End If


            If mShipTo = "N" Then
                'mSqlStr = " SELECT SUPP_CUST_NAME,SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtShippedTo.Text) & "'"

                mSqlStr = " SELECT " & vbCrLf _
                    & " BMST.LOCATION_ID, BMST.SUPP_CUST_NAME, BMST.SUPP_CUST_ADDR, BMST.SUPP_CUST_CITY, " & vbCrLf _
                    & " BMST.SUPP_CUST_STATE, BMST.SUPP_CUST_PIN, BMST.SUPP_CUST_PHONE, BMST.SUPP_CUST_FAXNO, " & vbCrLf _
                    & " BMST.SUPP_CUST_MAILID, BMST.SUPP_CUST_MOBILE, BMST.COUNTRY, BMST.GST_RGN_NO " & vbCrLf _
                    & " FROM FIN_SUPP_CUST_BUSINESS_MST BMST" & vbCrLf _
                    & " WHERE BMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShippTo) & "'" & vbCrLf _
                    & " And BMST.LOCATION_ID='" & MainClass.AllowSingleQuote(mShippToLoc) & "'"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then

                    mShipTo_TrdNm = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
                    mShipTo_Loc = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)

                    mShipTo_Bno = Mid(Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)), 1, 100)
                    mShipTo_Bnm = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                    mShipTo_Flno = Mid(Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)), 101, 100)
                    mShipTo_Dst = ""
                    mShipTo_Ph = ""
                    mShipTo_Em = ""

                    If pInvoiceSeq = 6 Then
                        mShipTo_Gstin = "URP"
                        mShipTo_Pin = "999999"
                        mShipTo_Stcd = CStr(96)
                    Else
                        mShipTo_Gstin = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
                        mShipTo_Pin = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
                        mStateName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
                        mShipTo_Stcd = GetStateCode(mStateName)
                    End If
                End If
            Else
                mShipTo_Gstin = ""
                mShipTo_TrdNm = ""
                mShipTo_Loc = ""
                mShipTo_Pin = ""
                mShipTo_Stcd = ""
                mShipTo_Bno = ""
                mShipTo_Bnm = ""
                mShipTo_Flno = ""
                mShipTo_Dst = ""
                mShipTo_Ph = ""
                mShipTo_Em = ""
            End If

            mVal_AssVal = IIf(IsDBNull(RsTempInv.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTempInv.Fields("TOTTAXABLEAMOUNT").Value)   ''Val(lblTotTaxableAmt.Text)
            mVal_CgstVal = IIf(IsDBNull(RsTempInv.Fields("TOTCGST_AMOUNT").Value), 0, RsTempInv.Fields("TOTCGST_AMOUNT").Value)   ''Val(lblTotCGSTAmount.Text)
            mVal_SgstVal = IIf(IsDBNull(RsTempInv.Fields("TOTSGST_AMOUNT").Value), 0, RsTempInv.Fields("TOTSGST_AMOUNT").Value)   '' Val(lblTotSGSTAmount.Text)
            mVal_IgstVal = IIf(IsDBNull(RsTempInv.Fields("TOTIGST_AMOUNT").Value), 0, RsTempInv.Fields("TOTIGST_AMOUNT").Value)   '' Val(lblTotIGSTAmount.Text)
            mVal_CesVal = 0
            mVal_StCesVal = 0
            mVal_CesNonAdVal = 0
            ''TOTIGST_AMOUNT
            mVal_TotInvVal = IIf(IsDBNull(RsTempInv.Fields("NETVALUE").Value), 0, RsTempInv.Fields("NETVALUE").Value)   '' Val(lblNetAmount.Text)

            mMSC = 0 '' IIf(IsDBNull(RsTempInv.Fields("TOTMSCAMOUNT").Value), 0, RsTempInv.Fields("TOTMSCAMOUNT").Value)
            pRO = IIf(IsDBNull(RsTempInv.Fields("TOTRO").Value), 0, RsTempInv.Fields("TOTRO").Value)
            ''mVal_OthChrg = CDbl(VB6.Format(mVal_TotInvVal - (mVal_AssVal + mVal_CgstVal + mVal_SgstVal + mVal_IgstVal + Val(mMSC)), "0.00")) 'Val(lblTotExpAmt.text)  ''							
            mVal_OthChrg = CDbl(VB6.Format(mVal_TotInvVal - (mVal_AssVal + mVal_CgstVal + mVal_SgstVal + mVal_IgstVal + Val(mMSC)) - Val(pRO), "0.00"))
            mVal_Disc = Val(mMSC) * -1

            mPay_Nam = ""
            mPay_Mode = ""
            mPay_PayTerm = ""
            mPay_PayInstr = ""
            mPay_CrDay = ""
            mPay_BalAmt = 0
            mPay_PayDueDt = ""
            mRef_InvRmk = ""
            mRef_InvStDt = ""
            mRef_InvEndDt = ""
            mTran_EcmGstin = ""



            mPay_FinInsBr = ""
            mPay_CrTrn = ""
            mPay_DirDr = ""
            mPay_AcctDet = ""
            mRef_PrecInvNo = ""
            mRef_PrecInvDt = ""
            mRef_RecAdvRef = ""
            mRef_TendRef = ""
            mRef_ContrRef = ""
            mRef_ExtRef = ""
            mRef_ProjRef = ""
            mRef_PORef = ""
            mExp_ExpCat = ""
            mExp_WthPay = ""
            mExp_InvForCur = ""



            mExp_ShipBNo = ""
            mExp_ShipBDt = ""
            mExp_Port = ""
            mExp_ForCur = ""
            mExp_CntCode = ""

        End If

        If pIsTesting = "Y" Then
            mBillFrom_Gstin = "03AAACW3775F010" '' IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        Else
            mBillFrom_Gstin = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        End If

        mBillFrom_TrdNm = IIf(IsDBNull(RsCompany.Fields("Company_Name").Value), "", RsCompany.Fields("Company_Name").Value)
        mBillFrom_Bno = IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)
        mBillFrom_Bnm = ""
        mBillFrom_Flno = ""
        mBillFrom_Loc = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        mBillFrom_Dst = ""
        mBillFrom_Pin = IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)
        pStateName = IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
        pStateCode = GetStateCode(pStateName)
        mBillFrom_Stcd = pStateCode
        mBillFrom_Ph = ""
        mBillFrom_Em = ""


        mGetQRImg = "0" ''0 for text , 1 for Image							
        mGetSignedInvoice = "0" ''1 - Signed Json of Invoice will be return, 0 - will not return signed Invoice.							


        HTTP.Open("POST", url, False)
        HTTP.setRequestHeader("Content-Type", "application/json")

        Dim pSqlStr As String
        Dim RsTempDet As ADODB.Recordset = Nothing


        If pInvoiceSeq = "2" Then
            xSqlStr = " SELECT IH.*, " & vbCrLf _
                   & " INVMST.ITEM_SHORT_DESC" & vbCrLf _
                   & " FROM FIN_PURCHASE_DET IH, INV_ITEM_MST INVMST" & vbCrLf _
                   & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " And IH.MKEY='" & pMKey & "'" & vbCrLf _
                   & " And IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                   & " And IH.ITEM_CODE=INVMST.ITEM_CODE"

            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)
        Else

            ''

            xSqlStr = " SELECT IH.*, " & vbCrLf _
                    & " INVMST.ITEM_SHORT_DESC"

            'xSqlStr = " SELECT IH.HSNCODE, SUM(QTY) QTY, RATE, SUM(GSTABLE_AMT) AS GSTABLE_AMT, IH.ITEM_UOM, SGST_PER, CGST_PER, IGST_PER, SUM(SGST_AMOUNT) AS SGST_AMOUNT, SUM(CGST_AMOUNT) AS CGST_AMOUNT, SUM(IGST_AMOUNT) AS IGST_AMOUNT, " & vbCrLf _
            '        & " INVMST.ITEM_SHORT_DESC"

            ''ITEM_QTY, ITEM_RATE, 

            xSqlStr = xSqlStr & vbCrLf _
                    & " FROM FIN_SUPP_SALE_DET IH, INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And IH.MKEY='" & pMKey & "'" & vbCrLf _
                    & " And IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                    & " And IH.ITEM_CODE=INVMST.ITEM_CODE"

            'xSqlStr = xSqlStr & vbCrLf _
            '        & " GROUP BY IH.HSNCODE,  RATE, IH.ITEM_UOM, SGST_PER, CGST_PER, IGST_PER," & vbCrLf _
            '        & " INVMST.ITEM_SHORT_DESC"

            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTempDet.EOF = True Then
                xSqlStr = "Select NARRATION As ITEM_SHORT_DESC, SAC_CODE As HSNCODE, 1 As QTY, ITEMVALUE As RATE, TOTTAXABLEAMOUNT As GSTABLE_AMT, 'NOS' AS ITEM_UOM, TOTCGST_PER CGST_PER, TOTSGST_PER SGST_PER, TOTIGST_PER IGST_PER, TOTCGST_AMOUNT CGST_AMOUNT, TOTSGST_AMOUNT SGST_AMOUNT, TOTIGST_AMOUNT IGST_AMOUNT " & vbCrLf _
                    & " FROM FIN_SUPP_SALE_HDR IH " & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And IH.MKEY='" & pMKey & "'" & vbCrLf _
                    & " AND IH.IS_ITEMDETAIL='N'"
                MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If


        'MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

        mBody = "{""Push_Data_List"":{"
        mBody = mBody & """Data"": ["
        If RsTempDet.EOF = False Then
            cntRow = 0
            Do While RsTempDet.EOF = False
                cntRow = cntRow + 1

                mBody = mBody & "{"
                mBody = mBody & """Gstin"":""" & mGSTIN & ""","
                mBody = mBody & """Version"":""" & mVersion & ""","
                mBody = mBody & """Irn"":""" & mIrn & ""","
                mBody = mBody & """Tran_TaxSch"":""" & mTaxSch & ""","

                mBody = mBody & """Tran_SupTyp"":""" & mTran_Catg & ""","
                mBody = mBody & """Tran_RegRev"":""" & mTran_RegRev & ""","
                mBody = mBody & """Tran_Typ"":""" & mTran_Typ & ""","

                mBody = mBody & """Tran_EcmGstin"":""" & mTran_EcmGstin & ""","
                mBody = mBody & """Tran_IgstOnIntra"":""" & "N" & ""","
                mBody = mBody & """Doc_Typ"":""" & mDoc_Typ & ""","
                mBody = mBody & """DOC_NO"":""" & mDOC_NO & ""","
                mBody = mBody & """Doc_Dt"":""" & mDoc_Dt & ""","
                mBody = mBody & """BillFrom_Gstin"":""" & mBillFrom_Gstin & ""","
                mBody = mBody & """BillFrom_LglNm"":""" & mBillFrom_TrdNm & ""","
                mBody = mBody & """BillFrom_TrdNm"":""" & mBillFrom_TrdNm & ""","

                mBody = mBody & """BillFrom_Addr1"":""" & mBillFrom_Bno & ""","
                mBody = mBody & """BillFrom_Addr2"":""" & mBillFrom_Bnm & ""","
                mBody = mBody & """BillFrom_Loc"":""" & mBillFrom_Loc & ""","
                mBody = mBody & """BillFrom_Pin"":""" & mBillFrom_Pin & ""","
                mBody = mBody & """BillFrom_Stcd"":""" & mBillFrom_Stcd & ""","
                mBody = mBody & """BillFrom_Ph"":""" & mBillFrom_Ph & ""","
                mBody = mBody & """BillFrom_Em"":""" & mBillFrom_Em & ""","

                mBody = mBody & """BillTo_Gstin"":""" & mBillTo_Gstin & ""","
                mBody = mBody & """BillTo_LglNm"":""" & mBillTo_TrdNm & ""","
                mBody = mBody & """BillTo_TrdNm"":""" & mBillTo_TrdNm & ""","

                mBody = mBody & """BillTo_Pos"":""" & mBillTo_Stcd & ""","
                mBody = mBody & """BillTo_Addr1"":""" & mBillTo_Bno & ""","
                mBody = mBody & """BillTo_Addr2"":""" & mBillTo_Bnm & ""","
                mBody = mBody & """BillTo_Loc"":""" & mBillTo_Loc & ""","
                mBody = mBody & """BillTo_Pin"":""" & mBillTo_Pin & ""","
                mBody = mBody & """BillTo_Stcd"":""" & mBillTo_Stcd & ""","
                mBody = mBody & """BillTo_Ph"":""" & mBillTo_Ph & ""","
                mBody = mBody & """BillTo_Em"":""" & mBillTo_Em & ""","


                mItem_PrdNm = IIf(IsDBNull(RsTempDet.Fields("ITEM_SHORT_DESC").Value), "", RsTempDet.Fields("ITEM_SHORT_DESC").Value)  'Trim(SprdMain.Text)
                mItem_PrdNm = MainClass.AllowDoubleQuote(MainClass.AllowSingleQuote(mItem_PrdNm))
                mItem_PrdNm = Replace(mItem_PrdNm, Chr(34), "")
                mItem_PrdNm = Replace(mItem_PrdNm, "'", "")

                mItem_PrdDesc = IIf(IsDBNull(RsTempDet.Fields("ITEM_SHORT_DESC").Value), "", RsTempDet.Fields("ITEM_SHORT_DESC").Value)  ' Trim(SprdMain.Text)
                mItem_PrdDesc = MainClass.AllowDoubleQuote(MainClass.AllowSingleQuote(mItem_PrdDesc))
                mItem_PrdDesc = Replace(mItem_PrdDesc, Chr(34), "")
                mItem_PrdDesc = Replace(mItem_PrdDesc, "'", "")

                mItem_HsnCd = IIf(IsDBNull(RsTempDet.Fields("HSNCODE").Value), "", RsTempDet.Fields("HSNCODE").Value)  'Trim(SprdMain.Text)

                mItem_Barcde = ""
                If pInvoiceSeq = "2" Then
                    mItem_Qty = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("ITEM_QTY").Value), 0, RsTempDet.Fields("ITEM_QTY").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_UnitPrice = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("ITEM_RATE").Value), 0, RsTempDet.Fields("ITEM_RATE").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_TotAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("GSTABLE_AMT").Value), 0, RsTempDet.Fields("GSTABLE_AMT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_AssAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("GSTABLE_AMT").Value), 0, RsTempDet.Fields("GSTABLE_AMT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

                Else
                    mItem_Qty = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("QTY").Value), 0, RsTempDet.Fields("QTY").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_UnitPrice = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("RATE").Value), 0, RsTempDet.Fields("RATE").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_TotAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("GSTABLE_AMT").Value), 0, RsTempDet.Fields("GSTABLE_AMT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    mItem_AssAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("GSTABLE_AMT").Value), 0, RsTempDet.Fields("GSTABLE_AMT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

                End If
                mItem_FreeQty = 0

                mItem_Unit = IIf(IsDBNull(RsTempDet.Fields("ITEM_UOM").Value), "", RsTempDet.Fields("ITEM_UOM").Value)  ' Trim(SprdMain.Text)

                mItem_Discount = 0

                mItem_OthChrg = mItem_AssAmt - mItem_TotAmt
                mItem_OthChrg = CDbl(VB6.Format(mItem_OthChrg, "0.00"))
                mItem_SgstRt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("SGST_PER").Value), 0, RsTempDet.Fields("SGST_PER").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                mItem_CgstRt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("CGST_PER").Value), 0, RsTempDet.Fields("CGST_PER").Value), "0.00") ''  CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                mItem_IgstRt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("IGST_PER").Value), 0, RsTempDet.Fields("IGST_PER").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                mItem_SgstAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("SGST_AMOUNT").Value), 0, RsTempDet.Fields("SGST_AMOUNT").Value), "0.00") ''CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                mItem_CgstAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("CGST_AMOUNT").Value), 0, RsTempDet.Fields("CGST_AMOUNT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                mItem_IgstAmt = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("IGST_AMOUNT").Value), 0, RsTempDet.Fields("IGST_AMOUNT").Value), "0.00") '' CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))



                mItem_CesRt = 0
                mItem_CesNonAdval = 0
                mItem_StateCes = 0
                mItem_TotItemVal = mItem_TotAmt + mItem_SgstAmt + mItem_CgstAmt + mItem_IgstAmt + mItem_CesNonAdval + mItem_StateCes + mItem_OthChrg ''- mItem_Discount '' mItem_AssAmt 30/09' (mItem_AssAmt * ((100 + mItem_SgstRt + mItem_CgstRt + mItem_IgstRt + mItem_CesRt + mItem_StateCes) * 0.01)) + mItem_CesNonAdval		
                mItem_TotItemVal = CDbl(VB6.Format(mItem_TotItemVal, "0.00"))

                mBody = mBody & """Item_SlNo"":""" & cntRow & ""","

                mBody = mBody & """Item_PrdDesc"":""" & mItem_PrdDesc & ""","
                mBody = mBody & """Item_IsServc"":""" & mItem_IsServc & ""","
                mBody = mBody & """Item_HsnCd"":""" & mItem_HsnCd & ""","
                mBody = mBody & """Item_Barcde"":""" & mItem_Barcde & ""","
                mBody = mBody & """Item_Qty"":""" & mItem_Qty & ""","
                mBody = mBody & """Item_FreeQty"":""" & mItem_FreeQty & ""","
                mBody = mBody & """Item_Unit"":""" & mItem_Unit & ""","
                mBody = mBody & """Item_UnitPrice"":""" & mItem_UnitPrice & ""","
                mBody = mBody & """Item_TotAmt"":""" & mItem_TotAmt & ""","
                mBody = mBody & """Item_Discount"":""" & mItem_Discount & ""","
                mBody = mBody & """Item_PreTaxVal"":""" & mItem_TotAmt & ""","
                mBody = mBody & """Item_AssAmt"":""" & mItem_AssAmt & ""","
                mBody = mBody & """Item_GstRt"":""" & mItem_CgstRt + mItem_SgstRt + mItem_IgstRt & ""","

                mBody = mBody & """Item_IgstAmt"":""" & mItem_IgstAmt & ""","
                mBody = mBody & """Item_CgstAmt"":""" & mItem_CgstAmt & ""","
                mBody = mBody & """Item_SgstAmt"":""" & mItem_SgstAmt & ""","
                mBody = mBody & """Item_CesRt"":""" & mItem_CesRt & ""","
                mBody = mBody & """Item_CesAmt"":""" & "" & ""","
                mBody = mBody & """Item_CesNonAdvlAmt"":""" & mItem_CesNonAdval & ""","

                mBody = mBody & """Item_StateCesRt"":""" & "" & ""","
                mBody = mBody & """Item_StateCesAmt"":""" & "" & ""","
                mBody = mBody & """Item_StateCesNonAdvlAmt"":""" & "" & ""","
                mBody = mBody & """Item_OthChrg"":""" & mItem_OthChrg & ""","
                mBody = mBody & """Item_TotItemVal"":""" & mItem_TotItemVal & ""","

                mBody = mBody & """Item_OrdLineRef"":""" & "" & ""","
                mBody = mBody & """Item_OrgCntry"":""" & "" & ""","
                mBody = mBody & """Item_PrdSlNo"":""" & "" & ""","
                mBody = mBody & """Item_Attrib_Nm"":""" & "" & ""","
                mBody = mBody & """Item_Attrib_Val"":""" & "" & ""","

                mBody = mBody & """Item_Bch_Nm"":""" & mItem_Bch_Nm & ""","
                mBody = mBody & """Item_Bch_ExpDt"":""" & mItem_Bch_ExpDt & ""","
                mBody = mBody & """Item_Bch_WrDt"":""" & mItem_Bch_WrDt & ""","
                mBody = mBody & """Val_AssVal"":""" & mVal_AssVal & ""","
                mBody = mBody & """Val_CgstVal"":""" & mVal_CgstVal & ""","
                mBody = mBody & """Val_SgstVal"":""" & mVal_SgstVal & ""","

                mBody = mBody & """Val_IgstVal"":""" & mVal_IgstVal & ""","
                mBody = mBody & """Val_CesVal"":""" & mVal_CesVal & ""","
                mBody = mBody & """Val_StCesVal"":""" & mVal_StCesVal & ""","
                mBody = mBody & """Val_Discount"":""" & mVal_Disc & ""","
                mBody = mBody & """Val_OthChrg"":""" & mVal_OthChrg & ""","
                mBody = mBody & """Val_RndOffAmt"":""" & VB6.Format(Val(pRO), "0.00") & ""","


                mBody = mBody & """Val_TotInvVal"":""" & mVal_TotInvVal & ""","
                mBody = mBody & """Val_TotInvValFc"":""" & "" & ""","

                mBody = mBody & """Pay_Nm"":""" & mPay_Nam & ""","
                mBody = mBody & """Pay_AcctDet"":""" & mPay_AcctDet & ""","
                mBody = mBody & """Pay_Mode"":""" & mPay_Mode & ""","
                mBody = mBody & """Pay_FinInsBr"":""" & mPay_FinInsBr & ""","

                mBody = mBody & """Pay_PayTerm"":""" & mPay_PayTerm & ""","
                mBody = mBody & """Pay_PayInstr"":""" & mPay_PayInstr & ""","
                mBody = mBody & """Pay_CrTrn"":""" & mPay_CrTrn & ""","
                mBody = mBody & """Pay_DirDr"":""" & mPay_DirDr & ""","
                mBody = mBody & """Pay_CrDay"":""" & mPay_CrDay & ""","
                mBody = mBody & """Pay_PaidAmt"":""" & "" & ""","

                mBody = mBody & """Pay_BalAmt"":""" & mPay_BalAmt & ""","
                mBody = mBody & """Pay_PaymtDue"":""" & mPay_PayDueDt & ""","
                mBody = mBody & """Ref_InvRmk"":""" & mRef_InvRmk & ""","
                mBody = mBody & """Ref_InvStDt"":""" & mRef_InvStDt & ""","
                mBody = mBody & """Ref_InvEndDt"":""" & mRef_InvEndDt & ""","
                mBody = mBody & """Doc_OrgInvNo"":""" & mDoc_OrgInvNo & ""","

                mBody = mBody & """ShipFrom_Gstin"":""" & mShipFrom_Gstin & ""","
                mBody = mBody & """ShipFrom_Nm"":""" & mShipFrom_TrdNm & ""","
                mBody = mBody & """ShipFrom_Addr1"":""" & mShipFrom_Bno & ""","
                mBody = mBody & """ShipFrom_Addr2"":""" & mShipFrom_Bnm & ""","
                mBody = mBody & """ShipFrom_Loc"":""" & mShipFrom_Loc & ""","
                mBody = mBody & """ShipFrom_Pin"":""" & mShipFrom_Pin & ""","
                mBody = mBody & """ShipFrom_Stcd"":""" & mShipFrom_Stcd & ""","


                mBody = mBody & """ShipTo_Gstin"":""" & mShipTo_Gstin & ""","
                mBody = mBody & """ShipTo_LglNm"":""" & mShipTo_TrdNm & ""","
                mBody = mBody & """ShipTo_TrdNm"":""" & mShipTo_TrdNm & ""","
                mBody = mBody & """ShipTo_Addr1"":""" & mShipTo_Bno & ""","
                mBody = mBody & """ShipTo_Addr2"":""" & mShipTo_Loc & ""","
                mBody = mBody & """ShipTo_Loc"":""" & mShipTo_Loc & ""","
                mBody = mBody & """ShipTo_Pin"":""" & mShipTo_Pin & ""","
                mBody = mBody & """ShipTo_Stcd"":""" & mShipTo_Stcd & ""","

                mBody = mBody & """Ref_PrecDoc_InvNo"":""" & mRef_PrecInvNo & ""","
                mBody = mBody & """Ref_PrecDoc_InvDt"":""" & mRef_PrecInvDt & ""","
                mBody = mBody & """Ref_PrecDoc_OthRefNo"":""" & "" & ""","


                mBody = mBody & """Ref_Contr_RecAdvRefr"":""" & mRef_RecAdvRef & ""","
                mBody = mBody & """Ref_Contr_RecAdvDt"":""" & "" & ""","

                mBody = mBody & """Ref_Contr_TendRefr"":""" & mRef_TendRef & ""","
                mBody = mBody & """Ref_Contr_ContrRefr"":""" & mRef_ContrRef & ""","
                mBody = mBody & """Ref_Contr_ExtRefr"":""" & mRef_ProjRef & ""","
                mBody = mBody & """Ref_Contr_ProjRefr"":""" & "" & ""","

                mBody = mBody & """Ref_Contr_PORefr"":""" & "" & ""","
                mBody = mBody & """Ref_Contr_PORefDt"":""" & "" & ""","

                mBody = mBody & """AddlDoc_Url"":""" & "" & ""","
                mBody = mBody & """AddlDoc_Docs"":""" & "" & ""","
                mBody = mBody & """AddlDoc_Info"":""" & "" & ""","


                mBody = mBody & """Ewb_TransId"":""" & "" & ""","
                mBody = mBody & """Ewb_TransName"":""" & "" & ""","
                mBody = mBody & """Ewb_TransMode"":""" & "" & ""","
                mBody = mBody & """Ewb_Distance"":""" & "0" & ""","
                mBody = mBody & """Ewb_TransDocNo"":""" & "" & ""","
                mBody = mBody & """Ewb_TransDocDt"":""" & "" & ""","
                mBody = mBody & """Ewb_VehNo"":""" & "" & ""","
                mBody = mBody & """Ewb_VehType"":""" & "" & ""","



                mBody = mBody & """Exp_ForCur"":""" & mExp_ForCur & ""","
                mBody = mBody & """Exp_CntCode"":""" & mExp_CntCode & ""","
                mBody = mBody & """Exp_ShipBNo"":""" & mExp_ShipBNo & ""","
                mBody = mBody & """Exp_ShipBDt"":""" & mExp_ShipBDt & ""","
                mBody = mBody & """Exp_Port"":""" & mExp_Port & ""","

                mBody = mBody & """CDKey"":""" & mCDKey & ""","
                mBody = mBody & """EInvUserName"":""" & mEInvUserName & ""","
                mBody = mBody & """EInvPassword"":""" & mEInvPassword & ""","
                mBody = mBody & """EFUserName"":""" & mEFUserName & ""","
                mBody = mBody & """EFPassword"":""" & mEFPassword & """"

                RsTempDet.MoveNext()
                If RsTempDet.EOF = True Then
                    mBody = mBody & "}"
                Else
                    mBody = mBody & "},"
                End If

            Loop
        End If

        mBody = mBody & "]"
        mBody = mBody & "}"
        mBody = mBody & "}"

        ' shipToGSTIN String GSTIN of  Ship-To shipToTradeName String Trade Name of  Ship-To dispatchFromGSTIN String GSTIN of Dispatch-From dispatchFromTradeName String Trade Name of Dispatch-From IsBillFromShipFromSame String Required, 0 for Different BillFrom and ShipFrom, 1 for Same BillFrom and ShipFrom IsBillToShipToSame String Required, 0 for Different BillTo and ShipTo, 1 for Same BillTo and ShipTo IsGSTINSEZ							

        'Dim feed = JsonSerializer.Deserialize(Of query)(JSON)

        'Dim strserialize As String = JsonConvert.SerializeObject(mBody)

        HTTP.Send(mBody)

        pResponseText = HTTP.responseText

        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        pResponseText = Replace(pResponseText, """", "'")

        Dim post As Object
        pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Status = ""})).Status

        If pStaus = "1" Then
            mIRNNo = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Irn = ""})).Irn   'JsonTest.Item("Irn")
            mIRNAckNo = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .AckNo = ""})).AckNo 'JsonTest.Item("AckNo")
            mIRNAckDate = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .AckDate = ""})).AckDate ' JsonTest.Item("AckDate") 'JsonTest.Item("elements").Item(mResponseId).Item("ewayBillDate")						
            mSignedQRCode = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .SignedQRCode = ""})).SignedQRCode ' JsonTest.Item("SignedQRCode")
            mSignedInvoice = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .SignedInvoice = ""})).SignedInvoice ' JsonTest.Item("SignedInvoice")

            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            Dim SqlStr As String = ""

            If pInvoiceSeq = "2" Then
                SqlStr = "UPDATE FIN_PURCHASE_HDR SET "
            Else
                SqlStr = "UPDATE FIN_SUPP_SALE_HDR SET "
            End If

            SqlStr = SqlStr & vbCrLf _
                    & " IRN_NO ='" & Trim(mIRNNo) & "'," & vbCrLf _
                    & " IRN_ACK_NO ='" & Trim(mIRNAckNo) & "'," & vbCrLf _
                    & " IRN_ACK_DATE =TO_DATE('" & VB6.Format(mIRNAckDate, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY ='" & pMKey & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = "DELETE FROM FIN_INVOICE_QRCODE WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY ='" & pMKey & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = "INSERT INTO FIN_INVOICE_QRCODE " & vbCrLf _
                    & " ( MKEY, COMPANY_CODE, SIGNQRCODE ) VALUES (" & vbCrLf _
                    & " '" & pMKey & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " '" & mSignedQRCode & "')"

            PubDBCn.Execute(SqlStr)

            PubDBCn.CommitTrans()

            WebRequestGenerateCRIRN = mIRNNo

            'mBMPFileName = mPubBarCodePath & "\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".bmp"

            'If GererateQRCodeImage(mBMPFileName, mSignedQRCode) = False Then GoTo ErrPart

            'If UpdateQRCODE(CDbl(LblMKey.Text), mBMPFileName) = False Then GoTo ErrPart

        End If

        If pStaus = "0" Then
            pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            'MsgInformation(pError)
            WebRequestGenerateCRIRN = pError
            HTTP = Nothing
            Exit Function
        End If

        HTTP = Nothing

        Exit Function
ErrPart:
        'Resume
        WebRequestGenerateCRIRN = Err.Description
        'http = Nothing							
        'MsgBox(Err.Description)
        PubDBCn.RollbackTrans()
    End Function
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        'OptSelection(1).Checked = True
        If lblBookType.Text = "VRJ" Then
            ShowVendorRJ()
        ElseIf lblBookType.Text = "REG" Then
            ShowRGP()
        ElseIf chkCreditNote.Checked = True Then
            ShowCreditNote("Y")
        ElseIf chkDebitNote.Checked = True Then
            ShowCreditNote("Y")
        ElseIf chkNonGstCreditNote.Checked = True Then
            ShowCreditNote("N")
        Else
            Show1()
        End If
        FormatSprdMain()
        cmdShow.Enabled = False

        CmdSave.Enabled = True
        cmdGenerateEWayBill.Enabled = True
        cmdConsolidatedEWayBill.Enabled = True
        cmdPrint.Enabled = True
        CmdPreview.Enabled = True
        cmdeMail.Enabled = True
    End Sub
    Private Sub ShowVendorRJ()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivision As Double


        'If lblBookType.Text= "II" Then  ''I Invoice , I - IRN
        ''CHALLAN_PREFIX GATEPASS_NO

        SqlStr = "SELECT IH.AUTO_KEY_DESP ,1 AS INVOICESEQTYPE, IH.AUTO_KEY_DESP BILLNO,"


        SqlStr = SqlStr & vbCrLf _
                & " IH.DESP_DATE, " & vbCrLf _
                & " IH.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, IH.BILL_TO_LOC_ID, ACM.VENDOR_CODE, IH.TRANS_DISTANCE, IH.VEHICLE_NO, ITEMVALUE AS NETVALUE, " & vbCrLf _
                & " '' AS IRN_NO, '' AS IRN_ACK_DATE, '' AS IRN_ACK_NO, '' AS IRN_ACK_DATE, IH.E_BILLWAYNO," & vbCrLf _
                & " IH.E_BILLWAYDATE, IH.E_BILLWAYVAILDUPTO, E_BILLWAYFILEPATH,''," & vbCrLf _
                & " '','IRN Print','EWay Print' "

        SqlStr = SqlStr & vbCrLf _
                & " FROM DSP_DESPATCH_HDR IH, FIN_SUPP_CUST_MST ACM, FIN_DNCN_HDR CH" & vbCrLf _
                & " WHERE IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "


        SqlStr = SqlStr & vbCrLf _
            & " AND IH.Company_Code=CH.Company_Code AND IH.AUTO_KEY_SO=CH.MKEY" '' AND  ID.ITEM_CODE=CD.ITEM_CODE "  ''AND CD.MKEY='" & txtSONo.Text & "'"


        SqlStr = SqlStr & vbCrLf & "AND IH.DESPATCHTYPE=2"

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If


        If cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NULL OR IH.E_BILLWAYNO='')"
        ElseIf cboShow.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NOT NULL OR IH.E_BILLWAYNO<>'')"
        End If

        If lblBookType.Text = "EC" Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NOT NULL OR IH.E_BILLWAYNO<>'')"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VEHICLE_NO='" & MainClass.AllowSingleQuote(txtVehicle.Text) & "'"
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.DESP_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.DESP_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtBillFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.AUTO_KEY_DESP>='" & MainClass.AllowSingleQuote(txtBillFrom.Text) & "'" & vbCrLf _
                & " AND IH.AUTO_KEY_DESP<='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY AUTO_KEY_DESP"


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowCreditNote(ByRef pIsGST As String)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivision As Double


        'If lblBookType.Text= "II" Then  ''I Invoice , I - IRN

        SqlStr = "SELECT IH.MKEY, PURCHASESEQTYPE AS INVOICESEQTYPE, IH.REJ_CREDITNOTE AS BILLNO, IH.VDATE AS INVOICE_DATE,  " & vbCrLf _
                & " IH.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, IH.BILL_TO_LOC_ID, ACM.VENDOR_CODE, 0 AS TRANS_DISTANCE, '' AS VEHICLENO, IH.NETVALUE, " & vbCrLf _
                & " IH.IRN_NO, IH.IRN_ACK_DATE, IH.IRN_ACK_NO, IH.IRN_ACK_DATE, '' AS E_BILLWAYNO," & vbCrLf _
                & " '' AS E_BILLWAYDATE, '' AS E_BILLWAYVAILDUPTO, '' AS E_BILLWAYFILEPATH,''," & vbCrLf _
                & " '','IRN Print','EWay Print' " & vbCrLf _
                & " FROM FIN_PURCHASE_HDR IH, FIN_SUPP_CUST_MST ACM" & vbCrLf _
                & " WHERE IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf & "AND IH.PURCHASESEQTYPE =2 AND CANCELLED='N'"

        If pIsGST = "Y" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.ISGSTAPPLICABLE='G'"
        Else
            SqlStr = SqlStr & vbCrLf & "AND IH.ISGSTAPPLICABLE<>'G'"
        End If

        If chkDebitNote.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND 1=2"
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If lblBookType.Text = "IIG" Then
            If cboShow.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NULL OR IH.IRN_NO='')"
            ElseIf cboShow.SelectedIndex = 2 Then
                SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NOT NULL OR IH.IRN_NO<>'')"
            End If
        End If

        If lblBookType.Text = "EC" Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NOT NULL OR IH.E_BILLWAYNO<>'')"
        End If

        If lblBookType.Text = "IC" Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NOT NULL OR IH.IRN_NO<>'')"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtBillFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.REJ_CREDITNOTE >= '" & MainClass.AllowSingleQuote(txtBillFrom.Text) & "'" & vbCrLf _
                & " AND IH.REJ_CREDITNOTE <= '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " UNION ALL"


        SqlStr = SqlStr & vbCrLf _
                & " SELECT IH.MKEY, 9 AS INVOICESEQTYPE, NVL(IH.PARTY_DNCN_NO,IH.VNO)  AS BILLNO, NVL(PARTY_DNCN_DATE,IH.VDATE) AS INVOICE_DATE,  " & vbCrLf _
                & " IH.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, IH.BILL_TO_LOC_ID, ACM.VENDOR_CODE, 0 AS TRANS_DISTANCE, '' AS VEHICLENO, IH.NETVALUE, " & vbCrLf _
                & " IH.IRN_NO, IH.IRN_ACK_DATE, IH.IRN_ACK_NO, IH.IRN_ACK_DATE, '' AS E_BILLWAYNO," & vbCrLf _
                & " '' AS E_BILLWAYDATE, '' AS E_BILLWAYVAILDUPTO, '' AS E_BILLWAYFILEPATH,''," & vbCrLf _
                & " '','IRN Print','EWay Print'  " & vbCrLf _
                & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_CUST_MST ACM" & vbCrLf _
                & " WHERE IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf & "AND IH.GST_APP ='" & pIsGST & "' AND ISFINALPOST='Y' AND CANCELLED='N'"

        If chkDebitNote.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE='M'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE='L'"
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If lblBookType.Text = "IIG" Then
            If cboShow.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NULL OR IH.IRN_NO='')"
            ElseIf cboShow.SelectedIndex = 2 Then
                SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NOT NULL OR IH.IRN_NO<>'')"
            End If
        End If

        If lblBookType.Text = "IC" Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NOT NULL OR IH.IRN_NO<>'')"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtBillFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.VNO >= '" & MainClass.AllowSingleQuote(txtBillFrom.Text) & "'" & vbCrLf _
                & " AND IH.VNO <= '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY 3"


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowRGP()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivision As Double


        'If lblBookType.Text= "II" Then  ''I Invoice , I - IRN
        ''CHALLAN_PREFIX GATEPASS_NO

        SqlStr = "SELECT IH.AUTO_KEY_PASSNO ,1 AS INVOICESEQTYPE, "

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            SqlStr = SqlStr & vbCrLf & " CHALLAN_PREFIX||TRIM(TO_CHAR(GATEPASS_NO,'000000')) BILLNO," & vbCrLf
        Else
            SqlStr = SqlStr & vbCrLf & " CHALLAN_PREFIX||GATEPASS_NO BILLNO," & vbCrLf
        End If


        SqlStr = SqlStr & vbCrLf _
                & " IH.GATEPASS_DATE, " & vbCrLf _
                & " IH.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, IH.BILL_TO_LOC_ID, ACM.VENDOR_CODE, IH.TRANS_DISTANCE, IH.VEHICLE_NO, 0 AS NETVALUE, " & vbCrLf _
                & " '' AS IRN_NO, '' AS IRN_ACK_DATE, '' AS IRN_ACK_NO, '' AS IRN_ACK_DATE, IH.E_BILLWAYNO," & vbCrLf _
                & " IH.E_BILLWAYDATE, IH.E_BILLWAYVAILDUPTO, E_BILLWAYFILEPATH,''," & vbCrLf _
                & " '' ,'IRN Print','EWay Print'"

        SqlStr = SqlStr & vbCrLf _
                & " FROM INV_GATEPASS_HDR IH, FIN_SUPP_CUST_MST ACM" & vbCrLf _
                & " WHERE IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        'SqlStr = SqlStr & vbCrLf & "AND IH.INVOICESEQTYPE IN (1,2,6,9)"

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If lblBookType.Text = "REG" Then
            If cboShow.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NULL OR IH.E_BILLWAYNO='')"
            ElseIf cboShow.SelectedIndex = 2 Then
                SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NOT NULL OR IH.E_BILLWAYNO<>'')"
            End If
        End If

        If lblBookType.Text = "EC" Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NOT NULL OR IH.E_BILLWAYNO<>'')"
        End If

        If lblBookType.Text = "IC" Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NOT NULL OR IH.IRN_NO<>'')"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VEHICLE_NO='" & MainClass.AllowSingleQuote(txtVehicle.Text) & "'"
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.GATEPASS_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.GATEPASS_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtBillFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.AUTO_KEY_PASSNO>='" & MainClass.AllowSingleQuote(txtBillFrom.Text) & "'" & vbCrLf _
                & " AND IH.AUTO_KEY_PASSNO<='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY CHALLAN_PREFIX||GATEPASS_NO"


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.Focus() : Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
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
        End If

        'If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '    If Trim(TxtAccount.Text) = "" Then
        '        MsgInformation("Invaild Account Name")
        '        TxtAccount.Focus()
        '        FieldsVerification = False
        '        Exit Function
        '    End If
        '    If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mAccountCode = MasterNo
        '    Else
        '        MsgInformation("Invaild Account Name")
        '        TxtAccount.Focus()
        '        FieldsVerification = False
        '        Exit Function
        '    End If
        'End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmMultiInvoicePrinting_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmMultiInvoicePrinting_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

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

        cboShow.Items.Clear()
        cboShow.Items.Add("ALL")
        cboShow.Items.Add("PENDING")
        cboShow.Items.Add("COMPLETE")

        cboShow.SelectedIndex = 0

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False

        chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Checked
        txtVehicle.Enabled = False
        cmdSearchVehicle.Enabled = False

        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtBillFrom.Text = ""
        txtBillTo.Text = ""

        FormatSprdMain()

        If lblBookType.Text = "IIG" Then
            CmdSave.Enabled = True
            cmdGenerateEWayBill.Enabled = True
        ElseIf lblBookType.Text = "IEG" Then
            CmdSave.Enabled = False
            cmdGenerateEWayBill.Enabled = True
        ElseIf lblBookType.Text = "REG" Or lblBookType.Text = "VRJ" Then
            CmdSave.Enabled = False
            cmdGenerateEWayBill.Enabled = True
        End If
        cmdShow.Enabled = True
        CmdSave.Enabled = False
        cmdGenerateEWayBill.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
        cmdeMail.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivision As Double


        'If lblBookType.Text= "II" Then  ''I Invoice , I - IRN

        SqlStr = "SELECT IH.MKEY, INVOICESEQTYPE, IH.BILLNO, IH.INVOICE_DATE,IH.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, " & vbCrLf _
                & " IH.BILL_TO_LOC_ID, IH.VENDOR_CODE, IH.TRANS_DISTANCE, IH.VEHICLENO, IH.NETVALUE, " & vbCrLf _
                & " IH.IRN_NO, IH.IRN_ACK_DATE, IH.IRN_ACK_NO, IH.IRN_ACK_DATE, IH.E_BILLWAYNO," & vbCrLf _
                & " IH.E_BILLWAYDATE, IH.E_BILLWAYVAILDUPTO, E_BILLWAYFILEPATH,CONSOLIDATION_E_BILLWAYNO," & vbCrLf _
                & " '','IRN Print','EWay Print' "

        SqlStr = SqlStr & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST ACM" & vbCrLf _
                & " WHERE IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If lblBookType.Text = "IEG" Then
            If chkServiceInvoiceOnly.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & "AND IH.INVOICESEQTYPE IN (1,2,3,6,9)"
            Else
                SqlStr = SqlStr & vbCrLf & "AND IH.INVOICESEQTYPE IN (4)"
            End If
        Else
            If chkServiceInvoiceOnly.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & "AND IH.INVOICESEQTYPE IN (1,2,6,9)"
            Else
                SqlStr = SqlStr & vbCrLf & "AND IH.INVOICESEQTYPE IN (4)"
            End If

        End If


        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If lblBookType.Text = "IIG" Then
            If cboShow.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NULL OR IH.IRN_NO='')"
            ElseIf cboShow.SelectedIndex = 2 Then
                SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NOT NULL OR IH.IRN_NO<>'')"
            End If
        End If

        If lblBookType.Text = "IEG" Then
            If cboShow.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NULL OR IH.E_BILLWAYNO='')"
            ElseIf cboShow.SelectedIndex = 2 Then
                SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NOT NULL OR IH.E_BILLWAYNO<>'')"
            End If
        End If

        If lblBookType.Text = "EC" Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NOT NULL OR IH.E_BILLWAYNO<>'')"
        End If

        If lblBookType.Text = "IC" Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NOT NULL OR IH.IRN_NO<>'')"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VEHICLENO='" & MainClass.AllowSingleQuote(txtVehicle.Text) & "'"
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtBillFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.BILLNO >= '" & MainClass.AllowSingleQuote(txtBillFrom.Text) & "'" & vbCrLf _
                & " AND IH.BILLNO <= '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.BILLNO"


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()
        With SprdMain

            .MaxCols = ColConsolidationEWayPrint
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)
            .set_RowHeight(-1, RowHeight)

            .Row = -1

            .Col = ColMKey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMKey, 12)
            .ColHidden = True

            .Col = ColInvoiceSeq
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColInvoiceSeq, 6)
            .ColHidden = True

            .Col = ColInvoiceNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColInvoiceNo, 12)

            .Col = CoInvoiceDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(CoInvoiceDate, 10)

            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerCode, 7)

            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerName, 28)

            .ColsFrozen = ColCustomerName

            .Col = ColVendorCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVendorCode, 8)

            .Col = ColLocation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocation, 12)

            .Col = ColVechile
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColVechile, 8)

            .Col = ColDistance
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColDistance, 8)
            .ColHidden = True ''

            .Col = ColBillAmount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColBillAmount, 10)

            .Col = ColIRNNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColIRNNo, 22)
            '.ColHidden = IIf(lblBookType.Text = "IIG" Or lblBookType.Text = "IC", False, True)

            .Col = ColIRNDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColIRNDate, 12)
            .ColHidden = True '' IIf(lblBookType.Text = "IIG" Or lblBookType.Text = "IC", False, True)

            .Col = ColIRNAckNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColIRNAckNo, 12)
            .ColHidden = True

            .Col = ColIRNAckDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColIRNAckDate, 12)
            .ColHidden = True

            .Col = ColEWayNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColEWayNo, 12)
            '.ColHidden = IIf(lblBookType.Text = "IEG" Or lblBookType.Text = "REG" Or lblBookType.Text = "EC", False, True)

            .Col = ColConsolidationEWayNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColConsolidationEWayNo, 12)

            .Col = ColEWayDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColEWayDate, 12)
            .ColHidden = True ''IIf(lblBookType.Text = "IEG" Or lblBookType.Text = "REG" Or lblBookType.Text = "EC", False, True)

            .Col = ColEWayBillUpToValid
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColEWayBillUpToValid, 12)
            .ColHidden = True

            .Col = ColEWayPath
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColEWayPath, 12)
            .ColHidden = True

            .Row = -1
            .Col = ColFlag
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColFlag, 8)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            .Row = -1
            .Col = ColIRNPrint
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "IRN Print"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColIRNPrint, 8)

            .Col = ColEWayPrint
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "EWay Print"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColEWayPrint, 8)

            .Col = ColConsolidationEWayPrint
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "Consolidation EWay Print"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColConsolidationEWayPrint, 8)

            MainClass.SetSpreadColor(SprdMain, -1)

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColConsolidationEWayNo)
            '    SprdMain.OperationMode = OperationModeSingle
            '    SprdMain.DAutoCellTypes = True
            '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '    SprdMain.GridColor = &HC00000
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColMKey
            .Text = "MKey"

            .Col = ColInvoiceSeq
            .Text = "Invoice Seq"

            .Col = ColInvoiceNo
            .Text = "Invoice No"

            .Col = CoInvoiceDate
            .Text = "Invoice Date"

            .Col = ColCustomerCode
            .Text = "Customer Code"

            .Col = ColCustomerName
            .Text = "Customer Name"

            .Col = ColVendorCode
            .Text = "Vendor Code"

            .Col = ColLocation
            .Text = "Customer Location"

            .Col = ColDistance
            .Text = "Distance"

            .Col = ColVechile
            .Text = "Vechile No"

            .Col = ColBillAmount
            .Text = "Bill Amount"

            .Col = ColIRNNo
            .Text = "IRN No"

            .Col = ColIRNDate
            .Text = "IRN Date"

            .Col = ColIRNAckNo
            .Text = "IRN Ack No"

            .Col = ColIRNAckDate
            .Text = "IRN Ack Date"

            .Col = ColEWayNo
            .Text = "EWay No"

            .Col = ColConsolidationEWayNo
            .Text = "Consolidation EWay No"

            .Col = ColEWayDate
            .Text = "EWay Date"

            .Col = ColEWayBillUpToValid
            .Text = "EWay Bill UpTo Valid"

            .Col = ColEWayPath
            .Text = "EWay Bill Path"

            .Col = ColFlag
            .Text = "Generate (Yes/No)"

            .Col = ColIRNPrint
            .Text = "IRN Print"

            .Col = ColEWayPrint
            .Text = "EWay Print"

            .Col = ColConsolidationEWayPrint
            .Text = "Consolidation EWay Print"

        End With
    End Sub
    Private Sub frmMultiInvoicePrinting_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColFlag
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
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        cmdShow.Enabled = True
        CmdSave.Enabled = False
        cmdGenerateEWayBill.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
        cmdeMail.Enabled = False
    End Sub
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(TxtDateFrom.Text)) = False Then
        '        TxtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        cmdShow.Enabled = True
        CmdSave.Enabled = False
        cmdGenerateEWayBill.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
        cmdeMail.Enabled = False
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
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And SUPP_CUST_TYPE In ('S','C')"
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
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


    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
        End If
        cmdShow.Enabled = True

        CmdSave.Enabled = False
        cmdGenerateEWayBill.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
        cmdeMail.Enabled = False
    End Sub

    Private Sub cmdsearch_Click(sender As Object, e As EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub TxtAccount_TextChanged(sender As Object, e As EventArgs) Handles TxtAccount.TextChanged
        cmdShow.Enabled = True
        CmdSave.Enabled = False
        cmdGenerateEWayBill.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
        cmdeMail.Enabled = False
    End Sub

    Private Sub _OptSelection_1_Click(sender As Object, e As EventArgs) Handles _OptSelection_1.Click
        'cmdShow.Enabled = True
        'CmdSave.Enabled = False
        'cmdGenerateEWayBill.Enabled = False
        'cmdConsolidatedEWayBill.Enabled = False
        'cmdPrint.Enabled = False
        'CmdPreview.Enabled = False
        'cmdeMail.Enabled = False
    End Sub

    Private Sub _OptSelection_0_Click(sender As Object, e As EventArgs) Handles _OptSelection_0.Click
        'cmdShow.Enabled = True
        'CmdSave.Enabled = False
        'cmdGenerateEWayBill.Enabled = False
        'cmdConsolidatedEWayBill.Enabled = False
        'cmdPrint.Enabled = False
        'CmdPreview.Enabled = False
        'cmdeMail.Enabled = False
    End Sub
    Public Function WebRequestCreateEWayBill(ByRef pMKey As String, ByRef pInvoiceSeq As Long, ByRef pCustomerName As String, ByRef pIRNNo As String) As String
        On Error GoTo ErrPart
        Dim url As String
        Dim pUserGSTin As String
        Dim pSupplyType As String
        Dim pSubSupplyType As Integer
        Dim pDocType As String
        Dim pDocNo As String
        Dim pDocDate As String
        Dim pFromGSTin As String
        Dim pFromTrdName As String
        Dim pFromAddr1 As String
        Dim pfromAddr2 As String
        Dim pFromPlace As String
        Dim pFromPincode As Double
        Dim pFromStateCode As String
        Dim pToGstin As String
        Dim pToTrdName As String
        Dim pToAddr1 As String
        Dim pToAddr2 As String
        Dim pToPlace As String
        Dim pPortStateCode As String
        Dim pISGSTRegd As String

        Dim pToCity As String
        Dim pToPincode As Double
        Dim pToStateCode As String
        Dim pTransMode As String
        Dim pTransModeStr As String
        Dim pTransDistance As Double
        Dim pTransporterName As String
        Dim pTransporterId As String
        Dim pTransDocNo As String
        Dim pTransDocDate As String
        Dim pVehicleNo As String
        Dim pVehicleType As String
        Dim pItemNo As Double
        Dim pProductName As String
        Dim pProductType As String

        Dim mItemCode As String
        Dim pProductDesc As String
        Dim pHSNCode As Double
        Dim pQuantity As Double
        Dim pQtyUnit As String
        Dim pTaxableAmount As Double
        Dim pSgstRate As Double
        Dim pCgstRate As Double
        Dim pIgstRate As Double
        Dim pCessRate As Double
        Dim pcessAdvol As Double
        Dim pStateName As String
        Dim pStateCode As String
        Dim cntRow As Integer
        Dim pSubSupplyDesc As String = ""

        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pStaus As String
        'Dim meWayResponseID  As String
        Dim mBody As String
        Dim mResponseId As String
        Dim mResponseIdStr As String
        Dim url1 As String
        Dim WebRequestGen As String

        'Dim pCompanyId As String
        'Dim pBranchId As String
        'Dim pTokenId As String
        'Dim pUserId As String



        Dim pResponseText As String
        Dim pError As String
        Dim pInvoiceValue As Double
        Dim pTaxableValue As Double
        Dim pCGSTValue As Double
        Dim pSGSTValue As Double
        Dim pIGSTValue As Double
        Dim pItemCessValue As Double

        Dim pItemCGSTValue As Double
        Dim pItemSGSTValue As Double
        Dim pItemIGSTValue As Double

        Dim mIsBillToShipToSame As String
        Dim mDispatchFromGSTIN As String
        Dim mDispatchFromTradeName As String
        Dim mShipToGSTIN As String
        Dim mShipToTradeName As String
        Dim pShipToStateCode As String
        Dim pOtherValue As Double
        Dim mIsBillFromShipFromSame As String

        Dim meWayResponseID As String
        Dim meWayBillDate As String
        Dim meWayBillUpto As String
        Dim meWayFilePath As String

        Dim pCDKey As String
        Dim pEFUserName As String
        Dim pEFPassword As String
        Dim pEWBUserName As String
        Dim pEWBPassword As String
        'Dim pIRNNo As String
        Dim xSqlStr As String
        Dim RsTempDet As ADODB.Recordset
        Dim mInvoiceSeqType As String
        Dim mLocationID As String
        Dim mShippedToSameParty As String
        Dim mShippedCode As String
        Dim mShipLocationID As String
        Dim mDespatchFrom As String
        Dim mShippedFromCode As String
        Dim RsTempInvDet As ADODB.Recordset
        Dim mSuppCustCode As String
        Dim mDespatchNo As Double
        Dim pIsTesting As String = "Y"
        Dim mPortNo As String

        Dim mTempAddress As String

        WebRequestCreateEWayBill = ""

        If GetWebTeleWaySetupContents(url, "C", pCDKey, pEFUserName, pEFPassword, pEWBUserName, pEWBPassword, pIsTesting) = False Then GoTo ErrPart

        If pIsTesting = "Y" Then
            ''1000687	29AAACW3775F000	Admin!23..	29AAACW3775F000	Admin!23..	29AAACW3775F000	29AAACW3775F000

            url = "http://ip.webtel.in/eWayGSP2/Sandbox/EWayBill/v1.3/GenEWB"
            pCDKey = "1000687"
            pEFUserName = "29AAACW3775F000"
            pEFPassword = "Admin!23.."
            pEWBUserName = "29AAACW3775F000"
            pEWBPassword = "Admin!23.."
            pUserGSTin = "29AAACW3775F000" ' IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) ''"05AAAAU3306Q1ZC" ''
            pFromGSTin = "29AAACW3775F000" ' IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) '"05AAAAU3306Q1ZC" ''
        Else
            pUserGSTin = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) ''"05AAAAU3306Q1ZC" ''
            pFromGSTin = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) '"05AAAAU3306Q1ZC" ''
        End If

        ': http://ip.webtel.in/eWayGSP2/Sandbox/EWayBill/v1.3/GenEWB
        '"EWBUserName": "05AAACD8069K1ZF",
        '"EWBPassword": "abc123@@",
        '
        '"EFUserName": "05AAACD8069K1ZF",
        '"EFPassword": "abc123@@",
        '"CDKey": "1000687

        Dim http As Object  ' MSXML2.XMLHTTP60 '' MSXML.xmlhttp
        http = CreateObject("MSXML2.ServerXMLHTTP")
        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")

        pFromTrdName = IIf(IsDBNull(RsCompany.Fields("Company_Name").Value), "", RsCompany.Fields("Company_Name").Value)
        pFromAddr1 = IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)
        pfromAddr2 = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        pFromPlace = IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
        pFromPincode = IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), 0, RsCompany.Fields("COMPANY_PIN").Value)
        pStateName = IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
        pStateCode = GetStateCode(pStateName)
        pFromStateCode = pStateCode

        If lblBookType.Text = "IEG" Or lblBookType.Text = "IIG" Then
            xSqlStr = " SELECT IH.* " & vbCrLf _
                    & " FROM FIN_INVOICE_HDR IH" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And IH.MKEY='" & pMKey & "'"
        ElseIf lblBookType.Text = "REG" Then
            xSqlStr = " SELECT IH.* " & vbCrLf _
                    & " FROM INV_GATEPASS_HDR IH" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And IH.AUTO_KEY_PASSNO='" & pMKey & "'"
        Else
            xSqlStr = " SELECT IH.*, IH.TRANSPORTER_NAME AS CARRIERS " & vbCrLf _
                   & " FROM DSP_DESPATCH_HDR IH" & vbCrLf _
                   & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " And IH.AUTO_KEY_DESP='" & pMKey & "'"
        End If

        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTempDet.EOF = False Then
            Do While RsTempDet.EOF = False
                mSuppCustCode = IIf(IsDBNull(RsTempDet.Fields("SUPP_CUST_CODE").Value), "", RsTempDet.Fields("SUPP_CUST_CODE").Value)
                If lblBookType.Text = "IEG" Or lblBookType.Text = "IIG" Then
                    mPortNo = IIf(IsDBNull(RsTempDet.Fields("PORT_CODE").Value), "", RsTempDet.Fields("PORT_CODE").Value)
                Else
                    mPortNo = ""
                End If

                If lblBookType.Text = "IEG" Or lblBookType.Text = "IIG" Then
                    mInvoiceSeqType = IIf(IsDBNull(RsTempDet.Fields("INVOICESEQTYPE").Value), -1, RsTempDet.Fields("INVOICESEQTYPE").Value)
                    mLocationID = IIf(IsDBNull(RsTempDet.Fields("BILL_TO_LOC_ID").Value), "", RsTempDet.Fields("BILL_TO_LOC_ID").Value)
                    mShippedToSameParty = IIf(IsDBNull(RsTempDet.Fields("SHIPPED_TO_SAMEPARTY").Value), "", RsTempDet.Fields("SHIPPED_TO_SAMEPARTY").Value)
                    mShippedCode = IIf(IsDBNull(RsTempDet.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTempDet.Fields("SHIPPED_TO_PARTY_CODE").Value)
                    mShipLocationID = IIf(IsDBNull(RsTempDet.Fields("SHIP_TO_LOC_ID").Value), "", RsTempDet.Fields("SHIP_TO_LOC_ID").Value)
                    mDespatchNo = IIf(IsDBNull(RsTempDet.Fields("AUTO_KEY_DESP").Value), "", RsTempDet.Fields("AUTO_KEY_DESP").Value)

                    mDespatchFrom = IIf(IsDBNull(RsTempDet.Fields("IS_DESP_OTHERTHAN_BILL").Value), "", RsTempDet.Fields("IS_DESP_OTHERTHAN_BILL").Value)
                    mShippedFromCode = IIf(IsDBNull(RsTempDet.Fields("SHIPPED_FROM_PARTY_CODE").Value), "", RsTempDet.Fields("SHIPPED_FROM_PARTY_CODE").Value)
                ElseIf lblBookType.Text = "REG" Then
                    mInvoiceSeqType = 11
                    mLocationID = IIf(IsDBNull(RsTempDet.Fields("BILL_TO_LOC_ID").Value), "", RsTempDet.Fields("BILL_TO_LOC_ID").Value)
                    mShippedToSameParty = "Y"
                    mShippedCode = mSuppCustCode
                    mShipLocationID = mLocationID
                    mDespatchNo = -1

                    mDespatchFrom = "N"
                    mShippedFromCode = ""
                Else
                    mInvoiceSeqType = 11
                    mLocationID = IIf(IsDBNull(RsTempDet.Fields("BILL_TO_LOC_ID").Value), "", RsTempDet.Fields("BILL_TO_LOC_ID").Value)
                    'mShippedToSameParty = "Y"
                    'mShippedCode = mSuppCustCode
                    'mShipLocationID = mLocationID
                    'mDespatchNo = -1

                    mShippedToSameParty = IIf(IsDBNull(RsTempDet.Fields("SHIPPED_TO_SAMEPARTY").Value), "", RsTempDet.Fields("SHIPPED_TO_SAMEPARTY").Value)
                    mShippedCode = IIf(IsDBNull(RsTempDet.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTempDet.Fields("SHIPPED_TO_PARTY_CODE").Value)
                    mShipLocationID = IIf(IsDBNull(RsTempDet.Fields("SHIP_TO_LOC_ID").Value), "", RsTempDet.Fields("SHIP_TO_LOC_ID").Value)
                    mDespatchNo = IIf(IsDBNull(RsTempDet.Fields("AUTO_KEY_DESP").Value), "", RsTempDet.Fields("AUTO_KEY_DESP").Value)


                    mDespatchFrom = "N"
                    mShippedFromCode = ""
                End If

                If lblBookType.Text = "IEG" Or lblBookType.Text = "IIG" Then
                    If mInvoiceSeqType = 1 Then
                        pSubSupplyType = "01"
                        pDocType = "INV"
                    ElseIf mInvoiceSeqType = 2 Then
                        pSubSupplyType = "03"
                        pDocType = "CHL"
                    ElseIf mInvoiceSeqType = 3 Then
                        pSubSupplyType = "05"
                        pDocType = "CHL"
                    ElseIf mInvoiceSeqType = 6 Then
                        pSubSupplyType = "03"
                        pDocType = "INV"
                    End If
                ElseIf lblBookType.Text = "REG" Then
                    Dim mSameGSTNo As String
                    Dim mPartyGSTNo As String
                    mPartyGSTNo = ""
                    mPartyGSTNo = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(mLocationID), "GST_RGN_NO")
                    mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")

                    If mSameGSTNo = "Y" Then
                        pSubSupplyType = "05"
                        pDocType = "CHL"
                    Else
                        pSubSupplyType = "04"
                        pDocType = "CHL"
                    End If

                Else
                    pSubSupplyType = "08"
                    pDocType = "OTH"
                    pSubSupplyDesc = "Purchase Return"
                End If

                pSupplyType = "O"

                If lblBookType.Text = "IEG" Or lblBookType.Text = "IIG" Then
                    pDocNo = IIf(IsDBNull(RsTempDet.Fields("BILLNO").Value), "", RsTempDet.Fields("BILLNO").Value) '' Trim(txtPreInvoice.Text) & Trim(txtInvoiceNo.Text)
                    pDocDate = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("INVOICE_DATE").Value), "", RsTempDet.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                ElseIf lblBookType.Text = "REG" Then
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                        pDocNo = IIf(IsDBNull(RsTempDet.Fields("CHALLAN_PREFIX").Value), "", RsTempDet.Fields("CHALLAN_PREFIX").Value) & VB6.Format(IIf(IsDBNull(RsTempDet.Fields("GATEPASS_NO").Value), "", RsTempDet.Fields("GATEPASS_NO").Value), "000000") '' Trim(txtPreInvoice.Text) & Trim(txtInvoiceNo.Text)
                    Else
                        pDocNo = IIf(IsDBNull(RsTempDet.Fields("CHALLAN_PREFIX").Value), "", RsTempDet.Fields("CHALLAN_PREFIX").Value) & IIf(IsDBNull(RsTempDet.Fields("GATEPASS_NO").Value), "", RsTempDet.Fields("GATEPASS_NO").Value) '' Trim(txtPreInvoice.Text) & Trim(txtInvoiceNo.Text)
                    End If
                    pDocDate = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("GATEPASS_DATE").Value), "", RsTempDet.Fields("GATEPASS_DATE").Value), "DD/MM/YYYY")
                Else
                    pDocNo = IIf(IsDBNull(RsTempDet.Fields("AUTO_KEY_DESP").Value), "", RsTempDet.Fields("AUTO_KEY_DESP").Value) '' Trim(txtPreInvoice.Text) & Trim(txtInvoiceNo.Text)
                    pDocDate = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("DESP_DATE").Value), "", RsTempDet.Fields("DESP_DATE").Value), "DD/MM/YYYY")
                End If

                mSqlStr = " Select SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf _
                        & " FROM FIN_SUPP_CUST_BUSINESS_MST" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " And SUPP_CUST_NAME='" & MainClass.AllowSingleQuote((pCustomerName)) & "' AND LOCATION_ID='" & MainClass.AllowSingleQuote(mLocationID) & "'"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    pToTrdName = Trim(pCustomerName)
                    mTempAddress = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                    pToAddr1 = Mid(mTempAddress, 1, 100) '' IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                    pToAddr2 = Mid(mTempAddress, 101, 200) '' IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                    pToCity = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value) ''
                    pToPlace = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

                    If CDbl(mInvoiceSeqType) = 6 Then
                        pToGstin = "URP"
                        pToPincode = CDbl("999999")
                        pToStateCode = 99    '' CStr(99)
                    Else
                        pISGSTRegd = "N"
                        If MainClass.ValidateWithMasterTable(pCustomerName, "SUPP_CUST_NAME", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            pISGSTRegd = MasterNo
                        End If
                        If pISGSTRegd = "N" Then
                            pToGstin = "URP"
                        Else
                            pToGstin = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
                        End If

                        pToPincode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), 0, RsTemp.Fields("SUPP_CUST_PIN").Value)
                        pToStateCode = GetStateCode(pToPlace)
                    End If


                Else
                    'MsgInformation("Invalid Customer Name, Please Select Valid Customer Name.")
                    WebRequestCreateEWayBill = "Invalid Customer Name"
                    http = Nothing
                    Exit Function
                End If

                mIsBillToShipToSame = IIf(mShippedToSameParty = "Y", "1", "0")
                mIsBillFromShipFromSame = "1"
                mDispatchFromGSTIN = ""
                mDispatchFromTradeName = ""
                mShipToGSTIN = ""
                mShipToTradeName = ""


                If mIsBillToShipToSame = "0" Then
                    mSqlStr = " SELECT SUPP_CUST_ADDR,SUPP_CUST_NAME, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf _
                        & " FROM FIN_SUPP_CUST_BUSINESS_MST" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShippedCode) & "'  AND LOCATION_ID='" & MainClass.AllowSingleQuote(mShipLocationID) & "'"

                    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        mShipToTradeName = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)) '' Dated 04/03/2019 Trim(txtSupplierName.Text)

                        ''Ship to  Address
                        mTempAddress = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                        pToAddr1 = Mid(mTempAddress, 1, 100) '' IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                        pToAddr2 = Mid(mTempAddress, 101, 200) '' IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)


                        'pToAddr1 = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                        'pToAddr2 = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                        pToCity = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value) ''
                        pToPlace = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

                        '
                        If CDbl(mInvoiceSeqType) = 6 Then
                            mShipToGSTIN = "" '"URP"

                            pToAddr1 = GetPortData(mPortNo, "PORT_ADDRESS_1")
                            pToAddr2 = GetPortData(mPortNo, "PORT_ADDRESS_2")
                            pToCity = GetPortData(mPortNo, "PORT_CITY")
                            pPortStateCode = GetPortData(mPortNo, "PORT_STATE_CODE")

                            pToPlace = ""
                            If MainClass.ValidateWithMasterTable(pPortStateCode, "CODE", "NAME", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                pToPlace = MasterNo
                            End If


                            pToPincode = CDbl(GetPortData(mPortNo, "PORT_PINCODE"))
                            pShipToStateCode = GetStateCode(pToPlace)

                            ''pToPincode = CDbl("999999")
                            ''pShipToStateCode = CStr(99)
                            'Select Case mPortNo
                            '    Case "INMUN1"
                            '        pToAddr1 = "Kutch District"
                            '        pToAddr2 = ""
                            '        pToCity = "Gujarat"
                            '        pToPlace = "Gujarat"
                            '        pToPincode = CDbl("370421")
                            '        pShipToStateCode = "24"
                            '    Case "INSGF6"
                            '        pToAddr1 = "Rail Linked Logistics Park G.T. Road, Sahnewal"
                            '        pToAddr2 = ""
                            '        pToCity = "Ludhiana"
                            '        pToPlace = "Ludhiana"
                            '        pToPincode = CDbl("141120")
                            '        pShipToStateCode = "03"
                            '    Case "INCPL6"
                            '        pToAddr1 = "Dadri Tilpatta Road ICD"
                            '        pToAddr2 = ""
                            '        pToCity = "Dadri"
                            '        pToPlace = "Dadri"
                            '        pToPincode = CDbl("203207")
                            '        pShipToStateCode = "09"
                            '    Case "INLDH6"
                            '        pToAddr1 = "Dhandari Kalan ICD"
                            '        pToAddr2 = ""
                            '        pToCity = "Ludhiana"
                            '        pToPlace = "Ludhiana"
                            '        pToPincode = CDbl("141010")
                            '        pShipToStateCode = "03"
                            '    Case "INDEL4"
                            '        pToAddr1 = "New CUSTOM HOUSE, IGI AIRPORT"
                            '        pToAddr2 = ""
                            '        pToCity = "New Delhi"
                            '        pToPlace = "New Delhi"
                            '        pToPincode = CDbl("110037")
                            '        pShipToStateCode = "07"
                            '    Case "INTKD6"
                            '        pToAddr1 = "ICD, Tughlakabad"
                            '        pToAddr2 = ""
                            '        pToCity = "New Delhi"
                            '        pToPlace = "New Delhi"
                            '        pToPincode = CDbl("110020")
                            '        pShipToStateCode = "07"
                            '    Case "INNSA1"
                            '        pToAddr1 = "Nhava Sheva Port"
                            '        pToAddr2 = ""
                            '        pToCity = "Navi Mumbai"
                            '        pToPlace = "Navi Mumbai"
                            '        pToPincode = CDbl("400707")
                            '        pShipToStateCode = "27"
                            '    Case "INATQ4"
                            '        pToAddr1 = "Sri Guru Ramdass Jee International Airport,"
                            '        pToAddr2 = "Ajnala Road,"
                            '        pToCity = "Amritsar"
                            '        pToPlace = "Punjab"
                            '        pToPincode = CDbl("143001")
                            '        pShipToStateCode = "03"
                            'End Select
                        Else
                            mShipToGSTIN = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
                            pToPincode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), 0, RsTemp.Fields("SUPP_CUST_PIN").Value)
                            pShipToStateCode = GetStateCode(pToPlace)
                        End If
                        'Else
                        '    MsgInformation("Invalid Shipped to Customer Name, Please Select Valid Shipped To Customer Name.")
                        'WebRequestCreateEWayBill = "Invalid Shipped to Customer Name"
                        'http = Nothing
                        'Exit Function
                    End If
                Else
                    mShipToTradeName = Trim(pCustomerName)
                    If CDbl(mInvoiceSeqType) = 6 Then
                        mShipToGSTIN = "" '"URP"
                        pToAddr1 = ""
                        pToAddr2 = ""
                        pToCity = ""
                        pToPlace = ""
                        pToPincode = 0
                        pShipToStateCode = ""

                        pToAddr1 = GetPortData(mPortNo, "PORT_ADDRESS_1")
                        pToAddr2 = GetPortData(mPortNo, "PORT_ADDRESS_2")
                        pToCity = GetPortData(mPortNo, "PORT_CITY")
                        pPortStateCode = GetPortData(mPortNo, "PORT_STATE_CODE")

                        pToPlace = ""
                        If MainClass.ValidateWithMasterTable(pPortStateCode, "CODE", "NAME", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            pToPlace = MasterNo
                        End If



                        pToPincode = CDbl(GetPortData(mPortNo, "PORT_PINCODE"))
                        pShipToStateCode = GetStateCode(pToPlace)

                        'Select Case mPortNo
                        '    Case "INMUN1"
                        '        pToAddr1 = "Kutch District"
                        '        pToAddr2 = ""
                        '        pToCity = "Gujarat"
                        '        pToPlace = "Gujarat"
                        '        pToPincode = CDbl("370421")
                        '        pShipToStateCode = "24"
                        '    Case "INSGF6"
                        '        pToAddr1 = "Rail Linked Logistics Park G.T. Road, Sahnewal"
                        '        pToAddr2 = ""
                        '        pToCity = "Ludhiana"
                        '        pToPlace = "Ludhiana"
                        '        pToPincode = CDbl("141120")
                        '        pShipToStateCode = "03"
                        '    Case "INCPL6"
                        '        pToAddr1 = "Dadri Tilpatta Road ICD"
                        '        pToAddr2 = ""
                        '        pToCity = "Dadri"
                        '        pToPlace = "Dadri"
                        '        pToPincode = CDbl("203207")
                        '        pShipToStateCode = "09"
                        '    Case "INLDH6"
                        '        pToAddr1 = "Dhandari Kalan ICD"
                        '        pToAddr2 = ""
                        '        pToCity = "Ludhiana"
                        '        pToPlace = "Ludhiana"
                        '        pToPincode = CDbl("141010")
                        '        pShipToStateCode = "03"
                        '    Case "INDEL4"
                        '        pToAddr1 = "New CUSTOM HOUSE, IGI AIRPORT"
                        '        pToAddr2 = ""
                        '        pToCity = "New Delhi"
                        '        pToPlace = "New Delhi"
                        '        pToPincode = CDbl("110037")
                        '        pShipToStateCode = "07"
                        '    Case "INTKD6"
                        '        pToAddr1 = "ICD, Tughlakabad"
                        '        pToAddr2 = ""
                        '        pToCity = "New Delhi"
                        '        pToPlace = "New Delhi"
                        '        pToPincode = CDbl("110020")
                        '        pShipToStateCode = "07"
                        '    Case "INNSA1"
                        '        pToAddr1 = "Nhava Sheva Port"
                        '        pToAddr2 = ""
                        '        pToCity = "Navi Mumbai"
                        '        pToPlace = "Navi Mumbai"
                        '        pToPincode = CDbl("400707")
                        '        pShipToStateCode = "27"
                        '    Case "INATQ4"
                        '        pToAddr1 = "Sri Guru Ramdass Jee International Airport,"
                        '        pToAddr2 = "Ajnala Road,"
                        '        pToCity = "Amritsar"
                        '        pToPlace = "Punjab"
                        '        pToPincode = CDbl("143001")
                        '        pShipToStateCode = "03"
                        'End Select



                        'pToAddr1 = "Nhava Sheva Port"
                        'pToAddr2 = ""
                        'pToCity = "Navi Mumbai"
                        'pToPlace = "MAHARASTHA"
                        'pToPincode = CDbl("400707")
                        'pShipToStateCode = 27 ''       '' CStr(99) '"Addr1":"Nhava Sheva Port","Addr2":null,"Loc":"Navi Mumbai","Pin":400707,"Stcd":"27"
                    Else
                        'mShipToTradeName = Trim(pCustomerName)
                        mShipToGSTIN = pToGstin
                        pShipToStateCode = pToStateCode
                    End If

                End If

                If mDespatchFrom = "Y" Then
                    mSqlStr = " Select SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf _
                        & " FROM FIN_SUPP_CUST_MST" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShippedFromCode) & "'"

                    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        mDispatchFromTradeName = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
                        mDispatchFromGSTIN = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)

                        ''Ship From  Address
                        mTempAddress = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                        pFromAddr1 = Mid(mTempAddress, 1, 100) '' IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                        pfromAddr2 = Mid(mTempAddress, 101, 200) '' IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)


                        'pFromAddr1 = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                        'pfromAddr2 = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                        pFromPlace = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

                        pFromPincode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), 0, RsTemp.Fields("SUPP_CUST_PIN").Value)
                        pStateName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
                        pStateCode = GetStateCode(pStateName)
                        pFromStateCode = pStateCode

                        mIsBillFromShipFromSame = "0"
                        'Else
                        '    MsgInformation("Invalid Shipped From Customer Name, Please Select Valid Shipped From Customer Name.")
                        WebRequestCreateEWayBill = "Invalid Shipped from Customer Name"
                        http = Nothing
                        Exit Function
                    End If
                End If
                ''TRANSPORT_MODE
                pTransModeStr = IIf(IsDBNull(RsTempDet.Fields("TRANSPORT_MODE").Value), "0", RsTempDet.Fields("TRANSPORT_MODE").Value)
                pTransModeStr = IIf(pTransModeStr = "", "1", pTransModeStr)
                pTransMode = VB.Left(pTransModeStr, 1)       'VB.Left(cboTransmode.Text, 1)
                pTransDistance = IIf(IsDBNull(RsTempDet.Fields("TRANS_DISTANCE").Value), 0, RsTempDet.Fields("TRANS_DISTANCE").Value)        ' Val(txtDistance.Text)
                pTransporterName = IIf(IsDBNull(RsTempDet.Fields("CARRIERS").Value), "", RsTempDet.Fields("CARRIERS").Value)        ' Trim(txtTransName.Text)
                pTransporterId = IIf(IsDBNull(RsTempDet.Fields("TRANSPORTER_GSTNO").Value), "", RsTempDet.Fields("TRANSPORTER_GSTNO").Value)        '  Trim(txtTransportCode.Text)
                pTransDocNo = IIf(IsDBNull(RsTempDet.Fields("GRNO").Value), "", RsTempDet.Fields("GRNO").Value)        ' Trim(txtTransportDocNo.Text)
                pTransDocDate = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("GRDATE").Value), "", RsTempDet.Fields("GRDATE").Value), "DD/MM/YYYY") ''IIf(pTransDocNo = "", "", Format(txtTransDocDate.Text, "DD/MM/YYYY"))

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                    If pTransporterId = "" Then
                        If lblBookType.Text = "IEG" Or lblBookType.Text = "IIG" Then
                            pVehicleNo = IIf(IsDBNull(RsTempDet.Fields("VEHICLENO").Value), "", RsTempDet.Fields("VEHICLENO").Value)        ' Trim(txtVehicleNo.Text)
                        Else
                            pVehicleNo = IIf(IsDBNull(RsTempDet.Fields("VEHICLE_NO").Value), "", RsTempDet.Fields("VEHICLE_NO").Value)        ' Trim(txtVehicleNo.Text)
                        End If
                    End If
                Else
                    If lblBookType.Text = "IEG" Or lblBookType.Text = "IIG" Then
                        pVehicleNo = IIf(IsDBNull(RsTempDet.Fields("VEHICLENO").Value), "", RsTempDet.Fields("VEHICLENO").Value)        ' Trim(txtVehicleNo.Text)
                    Else
                        pVehicleNo = IIf(IsDBNull(RsTempDet.Fields("VEHICLE_NO").Value), "", RsTempDet.Fields("VEHICLE_NO").Value)        ' Trim(txtVehicleNo.Text)
                    End If
                End If

                pTransModeStr = IIf(IsDBNull(RsTempDet.Fields("VEHICLE_TYPE").Value), "1", RsTempDet.Fields("VEHICLE_TYPE").Value)
                pTransModeStr = IIf(pTransModeStr = "", "1", pTransModeStr)
                pVehicleType = VB.Left(pTransModeStr, 1) ' VB.Left(cboVehicleType.Text, 1)


                If lblBookType.Text = "IEG" Or lblBookType.Text = "IIG" Then
                    If mInvoiceSeqType = 2 Or mInvoiceSeqType = 3 Then

                        pInvoiceValue = IIf(IsDBNull(RsTempDet.Fields("NETVALUE").Value), 0, RsTempDet.Fields("NETVALUE").Value)
                        pTaxableValue = IIf(IsDBNull(RsTempDet.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTempDet.Fields("TOTTAXABLEAMOUNT").Value)

                        pOtherValue = Format(pInvoiceValue - pTaxableValue, "0.00")
                    Else
                        pInvoiceValue = IIf(IsDBNull(RsTempDet.Fields("NETVALUE").Value), 0, RsTempDet.Fields("NETVALUE").Value)        ' CDbl(VB6.Format(lblNetAmount.Text, "0.00"))
                        pTaxableValue = IIf(IsDBNull(RsTempDet.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTempDet.Fields("TOTTAXABLEAMOUNT").Value)        ' CDbl(VB6.Format(lblTaxableAmount.Text, "0.00"))

                        pCGSTValue = IIf(IsDBNull(RsTempDet.Fields("NETCGST_AMOUNT").Value), 0, RsTempDet.Fields("NETCGST_AMOUNT").Value)
                        pSGSTValue = IIf(IsDBNull(RsTempDet.Fields("NETSGST_AMOUNT").Value), 0, RsTempDet.Fields("NETSGST_AMOUNT").Value)
                        pIGSTValue = IIf(IsDBNull(RsTempDet.Fields("NETIGST_AMOUNT").Value), 0, RsTempDet.Fields("NETIGST_AMOUNT").Value)

                        pOtherValue = Format(pInvoiceValue - (pTaxableValue + pCGSTValue + pSGSTValue + pIGSTValue), "0.00")
                    End If
                Else

                    Dim RsTempRgpDet As ADODB.Recordset

                    If lblBookType.Text = "REG" Then
                        xSqlStr = " SELECT SUM(AMOUNT) AS TOTTAXABLEAMOUNT, SUM(CGST_AMOUNT) AS NETCGST_AMOUNT,  SUM(SGST_AMOUNT) AS NETSGST_AMOUNT, SUM(IGST_AMOUNT) AS NETIGST_AMOUNT " & vbCrLf _
                                & " FROM INV_GATEPASS_DET IH" & vbCrLf _
                                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                & " And IH.AUTO_KEY_PASSNO='" & pMKey & "'"
                    Else

                        xSqlStr = " SELECT SUM(ITEMVALUE) AS TOTTAXABLEAMOUNT, SUM(NETCGST_AMOUNT) AS NETCGST_AMOUNT,  SUM(NETsGST_AMOUNT) AS NETSGST_AMOUNT, SUM(NETiGST_AMOUNT) AS NETIGST_AMOUNT " & vbCrLf _
                                & " FROM FIN_DNCN_HDR IH" & vbCrLf _
                                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                & " And IH.MKEY =  (SELECT AUTO_KEY_SO FROM DSP_DESPATCH_HDR WHERE AUTO_KEY_DESP='" & pMKey & "')"
                    End If
                    MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempRgpDet, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTempRgpDet.EOF = False Then
                        ' CDbl(VB6.Format(lblNetAmount.Text, "0.00"))
                        pTaxableValue = IIf(IsDBNull(RsTempRgpDet.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTempRgpDet.Fields("TOTTAXABLEAMOUNT").Value)        ' CDbl(VB6.Format(lblTaxableAmount.Text, "0.00"))

                        pCGSTValue = IIf(IsDBNull(RsTempRgpDet.Fields("NETCGST_AMOUNT").Value), 0, RsTempRgpDet.Fields("NETCGST_AMOUNT").Value)
                        pSGSTValue = IIf(IsDBNull(RsTempRgpDet.Fields("NETSGST_AMOUNT").Value), 0, RsTempRgpDet.Fields("NETSGST_AMOUNT").Value)
                        pIGSTValue = IIf(IsDBNull(RsTempRgpDet.Fields("NETIGST_AMOUNT").Value), 0, RsTempRgpDet.Fields("NETIGST_AMOUNT").Value)

                        pInvoiceValue = Format(pTaxableValue + pCGSTValue + pSGSTValue + pIGSTValue, "0.00")

                        pOtherValue = Format(pInvoiceValue - (pTaxableValue + pCGSTValue + pSGSTValue + pIGSTValue), "0.00")

                    Else
                        pInvoiceValue = 0       ' CDbl(VB6.Format(lblNetAmount.Text, "0.00"))
                        pTaxableValue = 0       ' CDbl(VB6.Format(lblTaxableAmount.Text, "0.00"))

                        pCGSTValue = 0
                        pSGSTValue = 0
                        pIGSTValue = 0

                        pOtherValue = 0
                    End If

                End If

                If lblBookType.Text = "IEG" Or lblBookType.Text = "IIG" Then
                    If mInvoiceSeqType = 2 Then
                        mSqlStr = "SELECT * FROM FIN_INVOICE_DET WHERE MKEY='" & MainClass.AllowSingleQuote(pMKey) & "'"
                    Else
                        mSqlStr = " SELECT ITEM_CODE, ITEM_DESC, ITEM_UOM, ITEM_RATE, " & vbCrLf _
                                    & " CGST_PER, SGST_PER , IGST_PER, " & vbCrLf _
                                    & " HSNCODE, SUM(CGST_AMOUNT) AS CGST_AMOUNT,SUM(SGST_AMOUNT) AS SGST_AMOUNT,  SUM(IGST_AMOUNT) AS IGST_AMOUNT,  SUM(GSTABLE_AMT) As GSTABLE_AMT, " & vbCrLf _
                                    & " SUM(ITEM_QTY) AS ITEM_QTY, SUM(ITEM_AMT) AS ITEM_AMT " & vbCrLf _
                                    & " FROM FIN_INVOICE_DET WHERE MKEY='" & MainClass.AllowSingleQuote(pMKey) & "'" & vbCrLf _
                                    & " GROUP BY ITEM_CODE, ITEM_DESC, ITEM_UOM, ITEM_RATE, " & vbCrLf _
                                    & " CGST_PER, SGST_PER , IGST_PER, HSNCODE"
                    End If
                ElseIf lblBookType.Text = "REG" Then
                    mSqlStr = " SELECT ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC AS ITEM_DESC, ID.ITEM_UOM, ITEM_RATE, " & vbCrLf _
                                & " CGST_PER, SGST_PER , IGST_PER, " & vbCrLf _
                                & " ID.HSN_CODE AS HSNCODE, SUM(CGST_AMOUNT) AS CGST_AMOUNT,SUM(SGST_AMOUNT) AS SGST_AMOUNT,  SUM(IGST_AMOUNT) AS IGST_AMOUNT,  SUM(ITEM_QTY*ITEM_RATE) As GSTABLE_AMT, " & vbCrLf _
                                & " SUM(ITEM_QTY) AS ITEM_QTY, SUM(AMOUNT) AS ITEM_AMT " & vbCrLf _
                                & " FROM INV_GATEPASS_DET ID, INV_ITEM_MST INVMST " & vbCrLf _
                                & " WHERE AUTO_KEY_PASSNO='" & MainClass.AllowSingleQuote(pMKey) & "'" & vbCrLf _
                                & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                                & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                                & " GROUP BY ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.ITEM_UOM, ID.ITEM_RATE, " & vbCrLf _
                                & " CGST_PER, SGST_PER , IGST_PER, ID.HSN_CODE"
                Else
                    mSqlStr = " SELECT ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC AS ITEM_DESC, ID.ITEM_UOM, DD.ITEM_RATE, " & vbCrLf _
                               & " DD.CGST_PER, DD.SGST_PER , DD.IGST_PER, " & vbCrLf _
                               & " INVMST.HSN_CODE AS HSNCODE, SUM(DD.CGST_AMOUNT) AS CGST_AMOUNT,SUM(DD.SGST_AMOUNT) AS SGST_AMOUNT,  SUM(DD.IGST_AMOUNT) AS IGST_AMOUNT," & vbCrLf _
                               & " SUM(DD.ITEM_AMT) GSTABLE_AMT, " & vbCrLf _
                               & " SUM(PACKED_QTY) AS ITEM_QTY, SUM(DD.ITEM_AMT) AS ITEM_AMT " & vbCrLf _
                               & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, FIN_DNCN_DET DD, INV_ITEM_MST INVMST " & vbCrLf _
                               & " WHERE IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP AND IH.AUTO_KEY_DESP='" & MainClass.AllowSingleQuote(pMKey) & "'" & vbCrLf _
                               & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                               & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                               & " AND IH.AUTO_KEY_SO=DD.MKEY " & vbCrLf _
                               & " AND ID.ITEM_CODE=DD.ITEM_CODE " & vbCrLf _
                               & " AND IH.AUTO_KEY_SO=DD.MKEY" & vbCrLf _
                               & " GROUP BY ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.ITEM_UOM, INVMST.HSN_CODE,DD.ITEM_RATE,DD.CGST_PER, DD.SGST_PER , DD.IGST_PER"

                    'If lblDespType.Text = "2" Then
                    '    mSqlStr = mSqlStr & vbCrLf & " AND  AND '" & txtSONo.Text & "'" ''ID.SERIAL_NO=CD.SUBROWNO AND
                    'End If

                End If


                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempInvDet, ADODB.LockTypeEnum.adLockReadOnly)

                mBody = "{""Push_Data_List"":["
                'mBody = mBody & """Data"": ["

                If RsTempInvDet.EOF = False Then
                    cntRow = 1
                    Do While RsTempInvDet.EOF = False
                        mBody = mBody & "{"
                        mBody = mBody & """GSTIN"":""" & pUserGSTin & ""","
                        mBody = mBody & """Year"":""" & Year(CDate(pDocDate)) & ""","
                        mBody = mBody & """Month"":""" & Month(CDate(pDocDate)) & ""","
                        mBody = mBody & """SupplyType"":""" & pSupplyType & ""","
                        mBody = mBody & """SubType"":""" & pSubSupplyType & ""","

                        mBody = mBody & """DocType"":""" & pDocType & ""","
                        mBody = mBody & """DocNo"":""" & pDocNo & ""","
                        mBody = mBody & """DocDate"":""" & VB6.Format(pDocDate, "YYYYMMDD") & ""","
                        mBody = mBody & """SupGSTIN"":""" & pFromGSTin & ""","
                        mBody = mBody & """SupName"":""" & pFromTrdName & ""","
                        mBody = mBody & """SupAdd1"":""" & pFromAddr1 & ""","
                        mBody = mBody & """SupAdd2"":""" & pfromAddr2 & ""","
                        mBody = mBody & """SupCity"":""" & pfromAddr2 & """," ''pFromPlace 'pStateName
                        mBody = mBody & """SupState"":""" & pStateCode & ""","
                        mBody = mBody & """SupPincode"":""" & pFromPincode & ""","

                        mBody = mBody & """RecGSTIN"":""" & pToGstin & ""","

                        mBody = mBody & """RecName"":""" & pToTrdName & ""","
                        mBody = mBody & """RecAdd1"":""" & pToAddr1 & ""","
                        mBody = mBody & """RecAdd2"":""" & pToAddr2 & ""","
                        mBody = mBody & """Reccity"":""" & pToCity & """," ''& "," & pToPlace
                        mBody = mBody & """RecState"":""" & pToStateCode & """," 'pToPlace
                        mBody = mBody & """Recpincode"":""" & pToPincode & ""","


                        'pTransDistance = 0
                        'If pTransDistance = 0 Then

                        'End If

                        mBody = mBody & """TransMode"":""" & pTransMode & ""","
                        mBody = mBody & """TransporterId"":""" & pTransporterId & ""","
                        mBody = mBody & """TransporterName"":""" & pTransporterName & ""","
                        mBody = mBody & """TransDistance"":""" & pTransDistance & ""","


                        mBody = mBody & """TransDocNo"":""" & pTransDocNo & ""","
                        mBody = mBody & """TransDocDate"":""" & VB6.Format(pTransDocDate, "YYYYMMDD") & ""","
                        mBody = mBody & """VehicleType"":""" & pVehicleType & ""","
                        mBody = mBody & """VehicleNo"":""" & pVehicleNo & ""","

                        pItemNo = cntRow ' Trim(.Text)
                        mItemCode = IIf(IsDBNull(RsTempInvDet.Fields("ITEM_CODE").Value), "", RsTempInvDet.Fields("ITEM_CODE").Value)
                        pProductName = IIf(IsDBNull(RsTempInvDet.Fields("ITEM_DESC").Value), "", RsTempInvDet.Fields("ITEM_DESC").Value)
                        pProductName = MainClass.AllowSingleQuote(pProductName)
                        pProductName = MainClass.AllowDoubleQuote(pProductName)
                        pProductName = Replace(pProductName, "(", "")
                        pProductName = Replace(pProductName, ")", "")
                        pProductName = Replace(pProductName, "/", "")
                        pProductName = Replace(pProductName, "_", "")
                        pProductName = Replace(pProductName, ",", "")
                        pProductName = Replace(pProductName, ";", "")
                        pProductName = Replace(pProductName, "\", "")
                        pProductName = Replace(pProductName, "*", "")
                        pProductName = Replace(pProductName, "#", "")

                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "PRODTYPE_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            pProductType = MasterNo
                        End If
                        pProductType = IIf(Trim(pProductType) = "", pProductName, pProductType)

                        pProductType = VB.Left(pProductType, 80)

                        pProductDesc = pProductName
                        If mInvoiceSeqType = 2 Then
                            Dim mLocal As String
                            Dim mPartyGSTNo As String

                            mLocal = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(mLocationID), "WITHIN_STATE")      '' "N"
                            mPartyGSTNo = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(mLocationID), "GST_RGN_NO")      '' 
                            pHSNCode = GetHSNCode(mItemCode)

                            pQuantity = Format(IIf(IsDBNull(RsTempInvDet.Fields("ITEM_QTY").Value), 0, RsTempInvDet.Fields("ITEM_QTY").Value), "0.00")

                            pQtyUnit = IIf(IsDBNull(RsTempInvDet.Fields("ITEM_UOM").Value), "", RsTempInvDet.Fields("ITEM_UOM").Value) ' IIf(IsNull(RsTemp!ITEM_UOM), "", RsTemp!ITEM_UOM)
                            pQtyUnit = IIf(pQtyUnit = "PCS", "NOS", pQtyUnit)

                            Dim m57F4 As String
                            Dim m57F4date As String
                            Dim mItemRate As String

                            m57F4 = Get57F4(mDespatchNo, Trim(mItemCode))

                            mItemRate = GetChallanRate(mItemCode, m57F4, pCustomerName, mDespatchNo)

                            pSgstRate = 0
                            pCgstRate = 0
                            pIgstRate = 0

                            pItemSGSTValue = 0
                            pItemCGSTValue = 0
                            pItemIGSTValue = 0
                            pCessRate = 0

                            pItemCessValue = 0
                            pcessAdvol = CDbl("0.0")

                            'mCGSTValue = 0
                            'mSGSTValue = 0
                            'mIGSTValue = 0

                            pTaxableAmount = Format(mItemRate * pQuantity, "0.00")
                            'mTotTaxableValue = mTotTaxableValue + mTaxableValue


                        Else
                            pHSNCode = IIf(IsDBNull(RsTempInvDet.Fields("HSNCODE").Value), "", RsTempInvDet.Fields("HSNCODE").Value)   '' CDbl(Trim(SprdMain.Text))
                            pQuantity = Format(IIf(IsDBNull(RsTempInvDet.Fields("ITEM_QTY").Value), 0, RsTempInvDet.Fields("ITEM_QTY").Value), "0.00")   '' Val(SprdMain.Text)
                            pQtyUnit = IIf(IsDBNull(RsTempInvDet.Fields("ITEM_UOM").Value), "", RsTempInvDet.Fields("ITEM_UOM").Value) ' IIf(IsNull(RsTemp!ITEM_UOM), "", RsTemp!ITEM_UOM)
                            pQtyUnit = IIf(pQtyUnit = "PCS", "NOS", pQtyUnit)

                            pTaxableAmount = Format(IIf(IsDBNull(RsTempInvDet.Fields("GSTABLE_AMT").Value), 0, RsTempInvDet.Fields("GSTABLE_AMT").Value), "0.00")   '' Val(SprdMain.Text)

                            If mInvoiceSeqType = 3 Then
                                pSgstRate = 0
                                pCgstRate = 0
                                pIgstRate = 0
                                pItemSGSTValue = 0
                                pItemCGSTValue = 0
                                pItemIGSTValue = 0
                                pCessRate = 0

                                pItemCessValue = 0
                                pcessAdvol = CDbl("0.0")

                            Else
                                pSgstRate = Format(IIf(IsDBNull(RsTempInvDet.Fields("SGST_PER").Value), 0, RsTempInvDet.Fields("SGST_PER").Value), "0.00")   ''Format(IIf(IsNull(RsTemp!SGST_PER), 0, RsTemp!SGST_PER), "0.00")
                                pItemSGSTValue = Format(IIf(IsDBNull(RsTempInvDet.Fields("SGST_AMOUNT").Value), 0, RsTempInvDet.Fields("SGST_AMOUNT").Value), "0.00")   '' RsTemp!SGST_AMOUNT), "0.00")

                                pCgstRate = Format(IIf(IsDBNull(RsTempInvDet.Fields("CGST_PER").Value), 0, RsTempInvDet.Fields("CGST_PER").Value), "0.00")   ''RsTemp!), "0.00")
                                pItemCGSTValue = Format(IIf(IsDBNull(RsTempInvDet.Fields("CGST_AMOUNT").Value), 0, RsTempInvDet.Fields("CGST_AMOUNT").Value), "0.00")   '' RsTemp!), "0.00")

                                pIgstRate = Format(IIf(IsDBNull(RsTempInvDet.Fields("IGST_PER").Value), 0, RsTempInvDet.Fields("IGST_PER").Value), "0.00")   ''RsTemp!), "0.00")

                                pItemIGSTValue = Format(IIf(IsDBNull(RsTempInvDet.Fields("IGST_AMOUNT").Value), 0, RsTempInvDet.Fields("IGST_AMOUNT").Value), "0.00")   '' RsTemp!), "0.00")

                                pCessRate = 0
                                pItemCessValue = 0
                                pcessAdvol = CDbl("0.0")
                                'mCGSTValue = mCGSTValue + Format(IIf(IsNull(RsTemp!CGST_AMOUNT), 0, RsTemp!CGST_AMOUNT), "0.00")
                                'mSGSTValue = mSGSTValue + Format(IIf(IsNull(RsTemp!SGST_AMOUNT), 0, RsTemp!SGST_AMOUNT), "0.00")
                                'mIGSTValue = mIGSTValue + Format(IIf(IsNull(RsTemp!IGST_AMOUNT), 0, RsTemp!IGST_AMOUNT), "0.00")

                            End If
                        End If

                        mBody = mBody & """ProductName"":""" & pProductName & ""","
                        mBody = mBody & """ProductDesc"":""" & pProductDesc & ""","
                        mBody = mBody & """HSNCode"":""" & pHSNCode & ""","
                        mBody = mBody & """Quantity"":""" & pQuantity & ""","
                        mBody = mBody & """QtyUnit"":""" & pQtyUnit & ""","
                        mBody = mBody & """TaxableValue"":""" & pTaxableAmount & ""","
                        mBody = mBody & """TotalValue"":""" & pTaxableAmount & ""","
                        mBody = mBody & """SGSTRate"":""" & pSgstRate & ""","
                        mBody = mBody & """SGSTValue"":""" & pItemSGSTValue & ""","
                        mBody = mBody & """CGSTRate"":""" & pCgstRate & ""","
                        mBody = mBody & """CGSTValue"":""" & pItemCGSTValue & ""","
                        mBody = mBody & """IGSTRate"":""" & pIgstRate & ""","
                        mBody = mBody & """IGSTValue"":""" & pItemIGSTValue & ""","
                        mBody = mBody & """CessRate"":""" & pCessRate & ""","
                        mBody = mBody & """CessValue"":""" & pItemCessValue & ""","

                        mBody = mBody & """EWBUserName"":""" & pEWBUserName & ""","
                        mBody = mBody & """EWBPassword"":""" & pEWBPassword & ""","
                        mBody = mBody & """CessNonAdvol"":""" & pcessAdvol & ""","

                        'If cboSubType.SelectedIndex = 7 Then
                        '    mBody = mBody & """SubSupplyDesc"":""" & "Others" & ""","
                        'Else
                        mBody = mBody & """SubSupplyDesc"":""" & pSubSupplyDesc & ""","
                        'End If

                        mBody = mBody & """ShipFromStateCode"":""" & pFromStateCode & ""","
                        mBody = mBody & """ShipToStateCode"":""" & pShipToStateCode & ""","
                        mBody = mBody & """TotalInvoiceValue"":""" & pInvoiceValue & ""","
                        mBody = mBody & """CessNonAdvolValue"":""" & 0 & ""","
                        mBody = mBody & """OtherValue"":""" & pOtherValue & ""","
                        mBody = mBody & """dispatchFromGSTIN"":""" & mDispatchFromGSTIN & ""","
                        mBody = mBody & """dispatchFromTradeName"":""" & mDispatchFromTradeName & ""","
                        mBody = mBody & """ShipToGSTIN"":""" & mShipToGSTIN & ""","
                        mBody = mBody & """ShipToTradeName"":""" & mShipToTradeName & ""","
                        mBody = mBody & """IsBillFromShipFromSame"":""" & mIsBillFromShipFromSame & ""","
                        mBody = mBody & """IsBillToShipToSame"":""" & mIsBillToShipToSame & ""","

                        If pIRNNo <> "" Then
                            mBody = mBody & """IRN"":""" & pIRNNo & ""","
                        End If

                        mBody = mBody & """IsGSTINSEZ"":""" & "0" & """"


                        '
                        RsTempInvDet.MoveNext()
                        cntRow = cntRow + 1
                        If RsTempInvDet.EOF = True Then
                            mBody = mBody & "}"
                        Else
                            mBody = mBody & "},"
                        End If
                    Loop

                End If

                mBody = mBody & "],"
                mBody = mBody & """Year"":""" & Year(CDate(pDocDate)) & ""","
                mBody = mBody & """Month"":""" & Month(CDate(pDocDate)) & ""","
                mBody = mBody & """EFUserName"":""" & pEFUserName & ""","
                mBody = mBody & """EFPassword"":""" & pEFPassword & ""","
                mBody = mBody & """CDKey"":""" & pCDKey & """"
                '    mBody = .JSON
                RsTempDet.MoveNext()
            Loop
        End If

        'mBody = mBody & "]"
        'mBody = mBody & "}"
        mBody = mBody & "}"

        'Dim strserialize As String = JsonConvert.SerializeObject(mBody)

        ' shipToGSTIN String GSTIN of  Ship-To shipToTradeName String Trade Name of  Ship-To dispatchFromGSTIN String GSTIN of Dispatch-From dispatchFromTradeName String Trade Name of Dispatch-From IsBillFromShipFromSame String Required, 0 for Different BillFrom and ShipFrom, 1 for Same BillFrom and ShipFrom IsBillToShipToSame String Required, 0 for Different BillTo and ShipTo, 1 for Same BillTo and ShipTo IsGSTINSEZ
        http.Send(mBody)

        pResponseText = http.responseText

        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        pResponseText = Replace(pResponseText, "\", "")
        pResponseText = Replace(pResponseText, """", "'")
        pResponseText = Replace(pResponseText, "'{", "{")
        pResponseText = Replace(pResponseText, "}'", "}")
        pResponseText = Replace(pResponseText, ".", "")
        pResponseText = Replace(pResponseText, ";", "")
        pResponseText = Replace(pResponseText, "'(0-9{2}A-Z 0-9{13})'", "")

        ''(0-9{2}A-Z 0-9{13})

        Dim post As Object
        pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .IsSuccess = "false"})).IsSuccess  '\'IsSuccess

        If UCase(pStaus) = "TRUE" Then
            meWayResponseID = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .EWayBill = ""})).EWayBill   'JsonTest.Item("Irn")
            meWayBillDate = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Date = ""})).Date 'JsonTest.Item("AckNo")
            meWayBillUpto = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ValidUpTo = ""})).ValidUpTo ' JsonTest.Item("AckDate") 'JsonTest.Item("elements").Item(mResponseId).Item("ewayBillDate")						

            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            Dim SqlStr As String = ""

            If lblBookType.Text = "IEG" Or lblBookType.Text = "IIG" Then
                SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
                        & " E_BILLWAYNO ='" & Val(meWayResponseID) & "'," & vbCrLf _
                        & " E_BILLWAYDATE =TO_DATE('" & VB6.Format(meWayBillDate, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                        & " E_BILLWAYVAILDUPTO =TO_DATE('" & VB6.Format(meWayBillUpto, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                        & " E_BILLWAYFILEPATH =''" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND MKEY ='" & pMKey & "'"
            ElseIf lblBookType.Text = "REG" Then
                SqlStr = "UPDATE INV_GATEPASS_HDR SET " & vbCrLf _
                        & " E_BILLWAYNO ='" & Val(meWayResponseID) & "'," & vbCrLf _
                        & " E_BILLWAYDATE =TO_DATE('" & VB6.Format(meWayBillDate, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                        & " E_BILLWAYVAILDUPTO =TO_DATE('" & VB6.Format(meWayBillUpto, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                        & " E_BILLWAYFILEPATH =''" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND AUTO_KEY_PASSNO ='" & pMKey & "'"
            Else
                SqlStr = "UPDATE DSP_DESPATCH_HDR SET " & vbCrLf _
                        & " E_BILLWAYNO ='" & Val(meWayResponseID) & "'," & vbCrLf _
                        & " E_BILLWAYDATE =TO_DATE('" & VB6.Format(meWayBillDate, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                        & " E_BILLWAYVAILDUPTO =TO_DATE('" & VB6.Format(meWayBillUpto, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                        & " E_BILLWAYFILEPATH =''" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND AUTO_KEY_DESP ='" & pMKey & "'"
            End If

            PubDBCn.Execute(SqlStr)

            PubDBCn.CommitTrans()
            WebRequestCreateEWayBill = meWayResponseID
        End If

        If UCase(pStaus) = "FALSE" Then
            pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            'MsgInformation(pError)
            WebRequestCreateEWayBill = pError
            http = Nothing
            Exit Function
        End If

        'WebRequestCreateEWayBill = True
        http = Nothing
        '    Set httpGen = Nothing
        Exit Function
ErrPart:
        'Resume
        MsgBox(Err.Description)
        WebRequestCreateEWayBill = ""
        http = Nothing
        'MsgBox(Err.Description)
        PubDBCn.RollbackTrans()
    End Function
    Private Function Get57F4(ByRef pDespatchNote As Double, ByRef pItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        'pRefDate = ""
        Get57F4 = ""
        SqlStr = "SELECT REF_NO,REF_DATE FROM DSP_DESPATCH_DET " & vbCrLf _
            & " WHERE AUTO_KEY_DESP=" & pDespatchNote & "" & vbCrLf _
            & " AND ITEM_CODE='" & pItemCode & "'" '' AND SERIAL_NO=" & xSubRow & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Get57F4 = IIf(IsDBNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value)
            'pRefDate = IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)
        End If
        Exit Function
ErrPart:
        Get57F4 = ""
    End Function
    Public Function GetChallanRate(ByRef mItemCode As String, ByRef m57F4 As String, ByRef pCustomerName As String, ByRef pDespatchNo As Double) As Double

        On Error GoTo ErrPart

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mCustCode As String

        mCustCode = ""
        If MainClass.ValidateWithMasterTable((pCustomerName), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustCode = MasterNo
        End If

        '    SqlStr = " SELECT MAX(ID.ITEM_RATE) AS  ITEM_RATE" & vbCrLf _							
        ''            & " FROM DSP_PAINT57F4_HDR IH, DSP_PAINT57F4_DET ID, DSP_PAINT57F4_TRN TRN" & vbCrLf _							
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _							
        ''            & " AND IH.MKEY=ID.MKEY AND ID.MKEY = TRN.MKEY AND ID.ITEM_CODE=TRN.ITEM_CODE" & vbCrLf _							
        ''            & " AND IH.PARTY_F4NO='" & MainClass.AllowSingleQuote(m57F4) & "' " & vbCrLf _							
        ''            & " AND TRN.SUB_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' " & vbCrLf _							
        ''            & " AND IH.BookType='D' "							

        SqlStr = " SELECT MAX(ID.ITEM_RATE) AS  ITEM_RATE" & vbCrLf _
            & " FROM DSP_PAINT57F4_HDR IH, DSP_PAINT57F4_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.PARTY_F4NO='" & MainClass.AllowSingleQuote(m57F4) & "' " & vbCrLf _
            & " AND IH.BookType='D' " & vbCrLf _
            & " AND ID.ITEM_CODE IN ( " & vbCrLf & " SELECT ITEM_CODE FROM DSP_PAINT57F4_TRN TRN" & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRN.MKEY='" & pDespatchNo & "'" & vbCrLf _
            & " AND TRN.SUB_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
            & " AND TRN.SUPP_CUST_CODE=IH.SUPP_CUST_CODE" & vbCrLf _
            & " AND TRN.PARTY_F4NO=IH.PARTY_F4NO)" & vbCrLf


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetChallanRate = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value), "0.00"))
        End If

        Exit Function
ErrPart:
        GetChallanRate = 0
    End Function

    Private Sub CmdPreview_Click(sender As Object, e As EventArgs) Handles CmdPreview.Click
        On Error GoTo ErrPart
        Dim mInvoicePrint As Boolean
        Dim mAnnexPrint As String
        Dim mSubsidiaryChallanPrint As String
        Dim mEXPAnnexPrint As String
        Dim mMaxRow As Integer
        Dim mSC_All As String
        Dim mSC_F4No As String
        Dim CntCount As Integer
        Dim mInvoicePrintType As String = ""
        Dim mExtraRemarks As String
        Dim mPrintOption As String = ""
        Dim mMKey As String
        Dim mPrintA4 As String
        Dim mPaperStyle As String
        Dim mPrintPaperSize As String
        Dim mMKeyStr As String

        frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(4).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(3).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).Enabled = False
        '_optShow_2
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            frmPrintInvCopy._optShow_0.Text = "Print"
            frmPrintInvCopy._optShow_0.Enabled = False
            frmPrintInvCopy._optShow_2.Checked = True
            frmPrintInvCopy._optShow_1.Checked = False
            frmPrintInvCopy._optShow_1.Enabled = True
            frmPrintInvCopy._optShow_2.Enabled = True
        Else
            frmPrintInvCopy._optShow_0.Text = "Print"
            frmPrintInvCopy._optShow_0.Enabled = False
            frmPrintInvCopy._optShow_1.Checked = True
            frmPrintInvCopy._optShow_1.Enabled = True
            frmPrintInvCopy._optShow_2.Enabled = True
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
            frmPrintInvCopy._optShow_5.Visible = True
            frmPrintInvCopy._optShow_6.Visible = True
        Else
            frmPrintInvCopy._optShow_5.Visible = False
            frmPrintInvCopy._optShow_6.Visible = False
        End If

        frmPrintInvCopy.optPrintPortrait.Checked = IIf(RsCompany.Fields("INVOICE_PRINT_STYLE").Value = "P", True, False)
        frmPrintInvCopy.optPrintLandScape.Checked = IIf(RsCompany.Fields("INVOICE_PRINT_STYLE").Value = "P", False, True)

        mPrintA4 = IIf(IsDBNull(RsCompany.Fields("INVOICE_A4").Value), "Y", RsCompany.Fields("INVOICE_A4").Value)
        frmPrintInvCopy.optA4.Checked = IIf(mPrintA4 = "Y", True, False)
        frmPrintInvCopy.optA3.Checked = IIf(mPrintA4 = "Y", False, True)

        frmPrintInvCopy.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            With SprdMain
                For CntRow = 1 To .MaxRows
                    .Row = CntRow
                    .Col = ColFlag
                    If SprdMain.Value = System.Windows.Forms.CheckState.Checked And frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Checked Then
                        .Col = ColIRNNo
                        If Trim(.Text) = "" Then
                            MsgInformation("IRN Not gererated. so Can not be print Original Invoice.")
                        End If

                    End If
                Next
            End With
        End If

        If frmPrintInvCopy.optShow(3).Checked = True Or frmPrintInvCopy.optShow(4).Checked = True Then
            mMKey = ""
            mMKeyStr = ""
            With SprdMain
                For CntRow = 1 To .MaxRows
                    .Row = CntRow
                    .Col = ColFlag
                    If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then
                        .Col = ColMKey
                        mMKey = Trim(.Text)
                        mMKeyStr = IIf(mMKeyStr = "", "'" & mMKey & "'", mMKeyStr & ",'" & mMKey & "'")
                        'Call ReportOnPackingSlip(Crystal.DestinationConstants.crptToWindow, mMKey, IIf(frmPrintInvCopy.optShow(3).Checked = True, "I", "O"))
                    End If
                Next
            End With

            If mMKeyStr <> "" Then
                Call ReportOnPackingSlip(Crystal.DestinationConstants.crptToWindow, mMKeyStr, IIf(frmPrintInvCopy.optShow(3).Checked = True, "I", "O"))
            End If

            frmPrintInvCopy.Dispose()
            frmPrintInvCopy.Close()
            Exit Sub
        End If

        mPaperStyle = IIf(frmPrintInvCopy.optPrintPortrait.Checked, "P", "L")
        mPrintPaperSize = IIf(frmPrintInvCopy.optA4.Checked, "Y", "N")

        Dim mPrePrint As String = "N"
        If mPrintPaperSize = "N" Then
            mPrePrint = IIf(frmPrintInvCopy.chkPrePrint.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        End If

        If frmPrintInvCopy._optShow_1.Checked = True Then
            mPrintOption = "PDF"
        Else
            mPrintOption = "PDFS"
        End If

        If chkCreditNote.Checked = True Or chkDebitNote.Checked = True Then
            Call ReportOnCreditNote(mPrintOption, mPaperStyle, mPrintPaperSize, mPrePrint, "Y", "N")
        ElseIf chkNonGSTCreditNote.Checked = True Then
            Call ReportOnCreditNote(mPrintOption, mPaperStyle, mPrintPaperSize, mPrePrint, "N", "N")
        Else
            Call ReportOnSales(mPrintOption, mPaperStyle, mPrintPaperSize, mPrePrint, "N")
        End If


        'Call ReportOnSales(mPrintOption, mPaperStyle, mPrintPaperSize, mPrePrint)
        '        End If							
        '    Next							

        frmPrintInvCopy.Close()
        frmPrintInvCopy.Dispose()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ErrPart:
        frmPrintInvCopy.Close()
        frmPrintInvCopy.Dispose()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ReportOnSales(ByRef mPrintOption As String, ByRef mPaperStyle As String, ByRef mPrintPaperSize As String, ByRef mPrePrint As String, ByRef mIseMail As String)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNO As String
        Dim mWithInState As String
        Dim mRMCustomer As Boolean
        Dim CntRow As Long


        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        Dim mMKey As String
        Dim pCustomerName As String
        Dim mCustomerCode As String
        Dim pLocation As String
        Dim mInvoiceSeq As String
        Dim mIRNNo As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillNoStr As String
        Dim mVendorCode As String

        'Dim psInfo As New System.Diagnostics.ProcessStartInfo("C:\Program Files\7-Zip\7z.exe ", Arg1 + ZipFileName + PathToPDFs)
        'psInfo.WindowStyle = ProcessWindowStyle.Hidden
        'Dim zipper As System.Diagnostics.Process = System.Diagnostics.Process.Start(psInfo)
        'Dim timeout As Integer = 60000 '1 minute in milliseconds

        'Dim MyProcess As System.Diagnostics.Process = System.Diagnostics.Process.Start(MyInfo)
        'MyProcess.WaitForExit(90000)

        'Dim mPrintPaperSize As String

        'mPrintPaperSize = IIf(IsDBNull(RsCompany.Fields("INVOICE_A4").Value), "Y", RsCompany.Fields("INVOICE_A4").Value)



        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColFlag
                If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then

                    .Col = ColMKey
                    mMKey = Trim(.Text)

                    .Col = ColInvoiceNo
                    mBillNo = Trim(.Text)
                    mBillNoStr = Replace(mBillNo, "/", "_")
                    mBillNoStr = Replace(mBillNoStr, "\", "_")

                    .Col = CoInvoiceDate
                    mBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                    .Col = ColIRNNo
                    mIRNNo = Trim(.Text)

                    .Col = ColCustomerName
                    pCustomerName = Trim(.Text)
                    mCustomerCode = "-1"

                    If MainClass.ValidateWithMasterTable(pCustomerName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mCustomerCode = MasterNo
                    End If

                    .Col = ColVendorCode
                    mVendorCode = Trim(.Text)

                    .Col = ColLocation
                    pLocation = Trim(.Text)

                    mWithInState = "N"
                    mWithInState = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(pLocation), "WITHIN_STATE")

                    .Col = ColInvoiceSeq
                    mInvoiceSeq = Trim(.Text)

                    SqlStr = SelectQryForPrint(mMKey, mCustomerCode)

                    If Val(mInvoiceSeq) = 3 Or Val(mInvoiceSeq) = 5 Then
                        If Val(mInvoiceSeq) = 5 Then
                            mTitle = IIf(Trim(mTitle) = "", "Internal Memo", mTitle)
                        Else
                            mTitle = IIf(Trim(mTitle) = "", "Delivery Challan for supply", mTitle)
                        End If

                        mSubTitle = "[See Section 143 of CGST Act, 2017 read with Rule 55 of CGST Rules]" ' "[See Rule 1 under Tax Invoice, Credit and Debit Note Rules]"						

                        If Val(mInvoiceSeq) = 5 Then
                            mRptFileName = "BOS_SUPP_GST.rpt"
                        Else
                            mRptFileName = "BOS_GST.rpt"
                        End If
                    Else
                        If Val(mInvoiceSeq) = 9 Then
                            mTitle = IIf(Trim(mTitle) = "", "Debit Note / Supplementary Invoice", mTitle)
                            mSubTitle = "[See Rule 34 of CGST Act, 2017 read with Rule 53 of CGST Rules]"
                        Else
                            mTitle = IIf(Trim(mTitle) = "", "Tax Invoice", mTitle)
                            mSubTitle = "[See Section 31 of CGST Act, 2017 read with Rule 46 of CGST Rules]"
                        End If

                        If mWithInState = "Y" Then
                            If frmPrintInvCopy._optShow_5.Checked = True Then
                                mRptFileName = "DeliverChallan_SGST"
                                mTitle = "Delivery Challan"
                            ElseIf frmPrintInvCopy._optShow_5.Checked = True Then
                                mRptFileName = "Commercial_SGST"
                                mTitle = "Commercial Invoice"
                            Else
                                mRptFileName = IIf(mPaperStyle = "P", "Invoice_SGST", "Invoice_SGST_L")
                            End If

                        Else
                            If CDbl(mInvoiceSeq) = 6 Then
                                mRptFileName = "Invoice_EXP_IGST"
                            Else
                                If frmPrintInvCopy._optShow_5.Checked = True Then
                                    mRptFileName = "DeliverChallan_IGST"
                                    mTitle = "Delivery Challan"
                                ElseIf frmPrintInvCopy._optShow_5.Checked = True Then
                                    mRptFileName = "Commercial_IGST"
                                    mTitle = "Commercial Invoice"
                                Else
                                    mRptFileName = IIf(mPaperStyle = "P", "Invoice_IGST", "Invoice_IGST_L") '' "Invoice_IGST.rpt"
                                End If
                            End If
                        End If
                        If mPrintPaperSize = "Y" Then
                            mRptFileName = mRptFileName & ".rpt"
                        Else
                            mRptFileName = mRptFileName & "_A3.rpt"
                        End If
                    End If
                    Call ShowExcisePDFReport(SqlStr, mTitle, mSubTitle, mRptFileName, True, mPrintOption, mMKey, mCustomerCode, pLocation, mInvoiceSeq, mBillNoStr, mBillDate, mIRNNo, mPrePrint, mIseMail, mVendorCode)

                    'Dim MyProcess As System.Diagnostics.Process = System.Diagnostics.Process.Start(MyInfo)
                    'MyProcess.WaitForExit(90000)

                    'If Not zipper.WaitForExit(timeout) Then
                    '    'Something went wrong with the zipping process; we waited longer than a minute
                    'Else

                    'End If
                End If
            Next
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ReportOnCreditNote(ByRef mPrintOption As String, ByRef mPaperStyle As String, ByRef mPrintPaperSize As String,
                                   mPrePrint As String, mIsGST As String, mIsMail As String)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNO As String
        Dim mWithInState As String
        Dim mRMCustomer As Boolean
        Dim CntRow As Long


        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        Dim mMKey As String
        Dim pCustomerName As String
        Dim mCustomerCode As String
        Dim pLocation As String
        Dim mInvoiceSeq As String
        Dim mIRNNo As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillNoStr As String
        'Dim mPrintPaperSize As String
        Dim mIsItemDetail As String = "Y"

        'mPrintPaperSize = IIf(IsDBNull(RsCompany.Fields("INVOICE_A4").Value), "Y", RsCompany.Fields("INVOICE_A4").Value)

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColFlag
                If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then

                    .Col = ColMKey
                    mMKey = Trim(.Text)

                    .Col = ColInvoiceNo
                    mBillNo = Trim(.Text)
                    mBillNoStr = Replace(mBillNo, "/", "_")
                    mBillNoStr = Replace(mBillNoStr, "\", "_")

                    .Col = CoInvoiceDate
                    mBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                    .Col = ColIRNNo
                    mIRNNo = Trim(.Text)

                    .Col = ColCustomerName
                    pCustomerName = Trim(.Text)
                    mCustomerCode = "-1"

                    If MainClass.ValidateWithMasterTable(pCustomerName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mCustomerCode = MasterNo
                    End If

                    .Col = ColLocation
                    pLocation = Trim(.Text)

                    mWithInState = "N"
                    mWithInState = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(pLocation), "WITHIN_STATE")

                    .Col = ColInvoiceSeq
                    mInvoiceSeq = Trim(.Text)

                    SqlStr = SelectQryForCRVoucher(mMKey, mCustomerCode)

                    mTitle = "Credit Note" ''IIf(LblBookCode.Text = -21, "Credit Note", "Debit Note")  ''"Credit Note"

                    If MainClass.ValidateWithMasterTable(mMKey, "MKEY", "IS_ITEMDETAIL", "FIN_SUPP_SALE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mIsItemDetail = MasterNo
                    End If

                    mRptFileName = IIf(mIsItemDetail = "N", "Cust_Sale_WOItem.rpt", "Cust_Sale.rpt")
                    mSubTitle = "" ''Trim(Mid(cboReason.Text, 3))

                    Call ShowExciseCRPDFReport(mMKey, mIRNNo, mBillNo, mBillNoStr, SqlStr, pCustomerName, Crystal.DestinationConstants.crptToPrinter, mTitle, mSubTitle, mRptFileName, True, "", mPrintOption)


                End If
            Next
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowExciseCRPDFReport(ByRef mMKey As String, ByRef pIRNNo As String, ByRef mBillNo As String, ByRef mBillNoStr As String, ByRef mSqlStr As String, ByRef mCustomerName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean, ByRef mInvoicePrintType As String, ByVal mPDF As String)

        On Error GoTo ErrPart
        Dim RsTemp As New ADODB.Recordset
        Dim RS As New ADODB.Recordset

        Dim fPath As String


        Dim mAmountInword As String
        Dim SqlStrSub As String
        Dim mDutyInword As String
        Dim SqlStr As String = ""

        Dim RsTempShip As ADODB.Recordset = Nothing
        Dim mNetAmount As Double
        Dim mNetDuty As Double
        Dim mPrepTime As String = ""
        Dim mRemovalTime As String = ""
        Dim mManuAVInWord As String
        Dim mManuCessInWord As String
        Dim mManuEDInWord As String
        Dim mManuHCessInWord As String
        Dim mDealerDetail As String
        Dim mDealerAddress As String
        Dim mManuAddress As String
        Dim mSO As Double
        Dim mPayTerms As String
        Dim mBalPayTerms As String
        Dim mJurisdiction As String
        Dim mShipToSameParty As String
        Dim mShipToCode As String

        Dim mShipToName As String = ""
        Dim mShipToAddress As String = ""
        Dim mShipToCity As String = ""
        Dim mShipToGSTN As String = ""
        Dim mCompanyDetail As String = ""
        Dim mCompanyeMail As String = ""
        Dim mCompanyWebSite As String = ""
        Dim mShipToState As String = ""
        Dim mShipToStateCode As String = ""
        Dim mStateName As String = ""
        Dim mStateCode As String = ""
        Dim mWithInState As String = ""
        Dim mWithInCountry As String = ""
        Dim mPlaceofSupply As String = ""
        Dim mExpHeading As String
        Dim mLUT As String
        Dim mCustomerCode As String
        Dim pBarCodeString As String

        Dim mShipFromOtherThan As String
        Dim mShipFromCode As String
        Dim mShipFromName As String
        Dim mShipFromAddress As String
        Dim mShipFromCity As String
        Dim mShipFromState As String
        Dim mShipFromStateCode As String
        Dim mShipFromGSTN As String
        Dim mExWork As String
        Dim path As String
        Dim mCurrency As String
        Dim mRateTitle As String
        Dim mAmountTitle As String
        Dim mShipLocation As String
        Dim mHour As String = ""
        Dim mMin As String = ""
        Dim mShipToPAN As String = ""

        Dim CrReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument  ' Report Name 
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions
        Dim mBillToLocation As String = ""

        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions


        mRptFileName = PubReportFolderPath & mRptFileName      ''"PDF_" &
        'mRptFileName = "G:\VBDotNetERP_Blank\Form\bin\Debug\Reports\PDF_Invoice_SGSTNew.rpt"
        CrReport.Load(mRptFileName)

        SqlStrSub = " SELECT * " & vbCrLf _
            & " FROM FIN_SUPP_SALE_EXP, FIN_SUPP_SALE_HDR, FIN_INTERFACE_MST " & vbCrLf _
            & " WHERE FIN_SUPP_SALE_EXP.MKEY = FIN_SUPP_SALE_HDR.MKEY " & vbCrLf _
            & " AND FIN_SUPP_SALE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _
            & " AND FIN_INVOICE_HDR.COMPANY_CODE = FIN_INTERFACE_MST.COMPANY_CODE" & vbCrLf _
            & " And FIN_SUPP_SALE_EXP.MKEY='" & MainClass.AllowSingleQuote(mMKey) & "'" & vbCrLf _
            & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

        If chkCreditNote.Checked = True Or chkDebitNote.Checked = True Then
            SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='N'"
        End If


        SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

        Call Connect_MainReport_To_Database_11(CrReport)        '', mSqlStr
        Call Connect_SubReport_To_Database_11(CrReport, "PurExp")      '

        CrReport.RecordSelectionFormula = "{IH.COMPANY_CODE} = " & RsCompany.Fields("COMPANY_CODE").Value & " AND {IH.MKEY} = '" & MainClass.AllowSingleQuote(mMKey) & "' AND {IH.FYEAR} = '" & RsCompany.Fields("FYEAR").Value & "' AND {BP.USER_ID} = '" & MainClass.AllowSingleQuote(PubUserID) & "'"

        ClearCRpt11Formulas(CrReport)
        CrReport.ReportOptions.EnableSaveDataWithReport = False
        SetCompanyReport11(CrReport, 1, mTitle, mSubTitle)
        'CrReport.VerifyDatabase()   .VerifyOnEveryPrint = True '' blnVerifyOnEveryPrint		
        CrReport.Refresh()

        mStateName = ""
        mStateCode = ""

        If MainClass.ValidateWithMasterTable(mCustomerName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(mMKey, "MKEY", "BILL_TO_LOC_ID", "FIN_SUPP_SALE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBillToLocation = MasterNo
        End If

        mStateName = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(mBillToLocation), "SUPP_CUST_STATE")
        mStateCode = GetStateCode(mStateName)

        mWithInState = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(mBillToLocation), "WITHIN_STATE")

        'If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mStateName = MasterNo
        '    mStateCode = GetStateCode(mStateName)
        'End If

        'If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mWithInState = MasterNo
        'End If

        If mWithInState = "N" Then
            If MainClass.ValidateWithMasterTable(mCustomerName, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInCountry = MasterNo
            End If
        End If

        mPlaceofSupply = VB6.Format(mStateCode, "00") & "-" & mStateName ''IIf(mWithInState = "Y", "INTRA STATE", IIf(mWithInCountry = "Y", "INTER STATE", "EXPORT"))							


        '    Report1.ReportFileName = PubReportFolderPath & mRptFileName							
        '    Report1.SQLQuery = mSqlStr							
        '    Report1.WindowShowGroupTree = False							
        '							
        '    Report1.WindowShowPrintBtn = False '' IIf(PubSuperUser = "S", True, False)							
        '    Report1.WindowShowPrintSetupBtn = False ''IIf(PubSuperUser = "S", True, False)							
        '    Report1.WindowShowExportBtn = IIf(PubSuperUser = "S", True, False)							

        SqlStr = " SELECT NETVALUE, ITEMVALUE, " & vbCrLf _
            & " (SELECT SUM(CGST_AMOUNT) FROM FIN_SUPP_SALE_DET WHERE COMPANY_CODE=IH.COMPANY_CODE AND MKEY=IH.MKEY) AS NETCGST_AMOUNT, " & vbCrLf _
            & " (SELECT SUM(SGST_AMOUNT) FROM FIN_SUPP_SALE_DET WHERE COMPANY_CODE=IH.COMPANY_CODE AND MKEY=IH.MKEY) AS  NETSGST_AMOUNT, " & vbCrLf _
            & " (SELECT SUM(IGST_AMOUNT) FROM FIN_SUPP_SALE_DET WHERE COMPANY_CODE=IH.COMPANY_CODE AND MKEY=IH.MKEY) AS NETIGST_AMOUNT, '' AS INV_PREP_TIME, " & vbCrLf _
            & " SUPP_CUST_CODE AS SHIPPED_TO_PARTY_CODE, '' AS REMOVAL_TIME, -1 AS OUR_AUTO_KEY_SO, 'Y' AS SHIPPED_TO_SAMEPARTY, " & vbCrLf _
            & " 'N' AS IS_DESP_OTHERTHAN_BILL, '' AS SHIPPED_FROM_PARTY_CODE, 'N' AS IS_SHIPPTO_EX_WORK" & vbCrLf _
            & " FROM FIN_SUPP_SALE_HDR IH" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And MKEY='" & MainClass.AllowSingleQuote(mMKey) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
            mNetDuty = IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)


            mShipToSameParty = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
            mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
            mExWork = "N"       ''IIf(IsDBNull(RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value), "N", RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value)

            mPrepTime = "" '' VB6.Format(IIf(IsDBNull(RsTemp.Fields("INV_PREP_TIME").Value), "", RsTemp.Fields("INV_PREP_TIME").Value), "HH:MM")
            mRemovalTime = "" ''VB6.Format(IIf(IsDBNull(RsTemp.Fields("REMOVAL_TIME").Value), "", RsTemp.Fields("REMOVAL_TIME").Value), "HH:MM")
            mSO = IIf(IsDBNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), "", RsTemp.Fields("OUR_AUTO_KEY_SO").Value)

            'mHour = HoursInText(VB.Left(mRemovalTime, 2))
            'mMin = MinInText(VB.Right(mRemovalTime, 2))

            mHour = mHour & " " & mMin

            mShipToCode = mCustomerCode
            mShipLocation = mBillToLocation



            SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf _
                & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf _
                & " FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipToCode) & "' AND LOCATION_ID='" & MainClass.AllowSingleQuote(mShipLocation) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTempShip.EOF = False Then
                mShipToName = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_NAME").Value), "", RsTempShip.Fields("SUPP_CUST_NAME").Value)
                mShipToAddress = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_ADDR").Value), "", RsTempShip.Fields("SUPP_CUST_ADDR").Value)
                mShipToAddress = Replace(mShipToAddress, vbCrLf, "")
                mShipToCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                mShipToCity = mShipToCity & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
                mShipToState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                mShipToStateCode = GetStateCode(mShipToState)
                mShipToGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)

                mShipToPAN = ""

                If MainClass.ValidateWithMasterTable(mShipToName, "SUPP_CUST_NAME", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mShipToPAN = MasterNo
                End If

            End If



            'mShipFromOtherThan = IIf(IsDBNull(RsTemp.Fields("IS_DESP_OTHERTHAN_BILL").Value), "N", RsTemp.Fields("IS_DESP_OTHERTHAN_BILL").Value)
            'mShipFromCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_FROM_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_FROM_PARTY_CODE").Value)

            mShipFromName = ""
            mShipFromAddress = ""
            mShipFromAddress = ""
            mShipFromCity = ""
            mShipFromCity = ""
            mShipFromState = ""
            mShipFromStateCode = ""
            mShipFromGSTN = ""

        End If

        AssignCRpt11Formulas(CrReport, "InvoicePrintType", "'" & mInvoicePrintType & "'")
        AssignCRpt11Formulas(CrReport, "CompanyCity", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyGSTIN", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & "'")

        ''-------------
        AssignCRpt11Formulas(CrReport, "CompanyAddressNew", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPin", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyState", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPhone", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", RsCompany.Fields("COMPANY_PHONE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyFax", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_FAXNO").Value), "", RsCompany.Fields("COMPANY_FAXNO").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyEmail", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", RsCompany.Fields("COMPANY_MAILID").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyWeb", "'" & IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", RsCompany.Fields("WEBSITE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPAN", "'" & IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value) & "'")
        Dim mCompanyStateCode As String = GetStateCode(IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyStateCode", "'" & mCompanyStateCode & "'")
        ''---------------
        mJurisdiction = "All Disputes Subject to " & IIf(IsDBNull(RsCompany.Fields("JURISDICTION").Value), "", RsCompany.Fields("JURISDICTION").Value) & " Jurisdiction."

        AssignCRpt11Formulas(CrReport, "COMPANYTINNo", "'" & IIf(IsDBNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value) & "'")
        AssignCRpt11Formulas(CrReport, "COMPANYCINNo", "'" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & "'")

        mCompanyeMail = IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", "e-mail : " & RsCompany.Fields("COMPANY_MAILID").Value)
        mCompanyWebSite = IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", "WebSite : " & RsCompany.Fields("WEBSITE").Value)
        mCompanyDetail = mCompanyeMail & ", " & mCompanyWebSite

        AssignCRpt11Formulas(CrReport, "COMPANYDETAIL", "'" & mCompanyDetail & "'")
        AssignCRpt11Formulas(CrReport, "PrepTime", "'" & mPrepTime & "'")
        AssignCRpt11Formulas(CrReport, "RemovalTime", "'" & mRemovalTime & "'")
        AssignCRpt11Formulas(CrReport, "RemovalTimeInWord", "'" & mHour & "'")
        AssignCRpt11Formulas(CrReport, "ShipToPAN", "'" & mShipToPAN & "'")


        'AssignCRpt11Formulas(CrReport, "JWRemarks", "'" & mJWRemarks & "'")
        AssignCRpt11Formulas(CrReport, "Jurisdiction", "'" & mJurisdiction & "'")
        AssignCRpt11Formulas(CrReport, "mShipToName", "'" & mShipToName & "'")
        AssignCRpt11Formulas(CrReport, "mShipToAddress", "'" & mShipToAddress & "'")
        AssignCRpt11Formulas(CrReport, "mShipToCity", "'" & mShipToCity & "'")
        AssignCRpt11Formulas(CrReport, "mShipToGSTN", "'" & mShipToGSTN & "'")
        AssignCRpt11Formulas(CrReport, "mShipToState", "'" & mShipToState & "'")
        AssignCRpt11Formulas(CrReport, "mShipToStateCode", "'" & mShipToStateCode & "'")
        AssignCRpt11Formulas(CrReport, "mStateName", "'" & mStateName & "'")
        AssignCRpt11Formulas(CrReport, "mStateCode", "'" & mStateCode & "'")
        AssignCRpt11Formulas(CrReport, "mPlaceofSupply", "'" & mPlaceofSupply & "'")
        'AssignCRpt11Formulas(CrReport, "mServiceName", "'" & Trim(txtServProvided.Text) & "'")



        'If Val(lblInvoiceSeq.Text) = 6 Or Val(lblInvoiceSeq.Text) = 7 Then
        '    If chkLUT.CheckState = System.Windows.Forms.CheckState.Checked Then
        '        mLUT = GetLUT((txtBillDate.Text))
        '    Else
        '        mLUT = ""
        '    End If

        '    AssignCRpt11Formulas(CrReport, "LUTNo", "'" & mLUT & "'")
        '    mExpHeading = "SUPPLY MEANT FOR EXPORT ON PAYMENT OF IGST OR SUPPLY MEANT FOR EXPORT UNDER BOND OR LETTER OF UNDERTAKING WITHOUT PAYMENT OF IGST"
        '    MainClass.AssignCRptFormulas(Report1, "ExpHeading=""" & mExpHeading & """")

        'End If

        'mPayTerms = ""

        If IsSubReport = True Then

            If mNetAmount = 0 Then
                mAmountInword = " Zero"
            Else
                mAmountInword = MainClass.RupeesConversion(mNetAmount)
            End If

            mDutyInword = MainClass.RupeesConversion(mNetDuty)

            'If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            '    AssignCRpt11Formulas(CrReport, "AmountInWord", "'Rs. Zero'")
            '    AssignCRpt11Formulas(CrReport, "DutyInword", "'Rs. Zero'")
            '    AssignCRpt11Formulas(CrReport, "NetAmount", "'0.00'")
            'Else
            AssignCRpt11Formulas(CrReport, "AmountInWord", "'" & mAmountInword & "'")
            AssignCRpt11Formulas(CrReport, "NetAmount", "'" & VB6.Format(mNetAmount, "0.00") & "'")
            AssignCRpt11Formulas(CrReport, "DutyInword", "'" & mDutyInword & "'")
            'End If

            'SqlStrSub = " SELECT FIN_SUPP_SALE_EXP.MKEY, FIN_SUPP_SALE_EXP.SUBROWNO, FIN_SUPP_SALE_EXP.EXPPERCENT, FIN_SUPP_SALE_EXP.AMOUNT, FIN_INTERFACE_MST.COMPANY_CODE, FIN_INTERFACE_MST.NAME" & vbCrLf _
            '    & " FROM FIN_SUPP_SALE_EXP, FIN_SUPP_SALE_HDR, FIN_INTERFACE_MST " & vbCrLf _
            '    & " WHERE FIN_SUPP_SALE_EXP.MKEY = FIN_SUPP_SALE_HDR.MKEY AND FIN_SUPP_SALE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _
            '    & " AND FIN_SUPP_SALE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf _
            '    & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

            'SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"

            'SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

            'Report1.SubreportToChange = "PurExp" ''Report1.GetNthSubreportName(0)						
            'Report1.Connect = STRRptConn
            'Report1.SQLQuery = SqlStrSub
            'MainClass.AssignCRptFormulas(Report1, "JWSTRemarks=""" & mJWSTRemarks & """")
            '          Report1.SubreportToChange = ""			

        End If


        Dim mBMPFileName As String = ""
        mBillNoStr = Replace(mBillNoStr, "/", "_")
        mBillNoStr = Replace(mBillNoStr, "\", "_")
        mBMPFileName = RefreshQRCode(mMKey, mBillNoStr, pIRNNo)

        If Not FILEExists(mBMPFileName) Then
            mBMPFileName = ""
        End If
        Application.DoEvents()

        AssignCRpt11Formulas(CrReport, "PicLocation", "'" & mBMPFileName & "'")

        Dim mClientLogoPath As String = ""
        If Not FILEExists(PubClientLogoPath) Then
            mClientLogoPath = ""
        Else
            mClientLogoPath = PubClientLogoPath
        End If
        AssignCRpt11Formulas(CrReport, "CompanyLogo", "'" & mClientLogoPath & "'")

        If mPDF = "PDF" Then
            Dim pOutPutFileName As String = ""
            'mBillNoStr = Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text)
            mBillNoStr = Replace(mBillNoStr, "/", "_")
            mBillNoStr = Replace(mBillNoStr, "\", "_")

            fPath = mPubBarCodePath & "\CustCredit_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"
            pOutPutFileName = mPubBarCodePath & "\CustCredit_DS_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"




            'FrmInvoiceViewer.CrystalReportViewer1.ReportSource = Nothing
            'FrmInvoiceViewer.CrystalReportViewer1.ReportSource = CrReport
            'FrmInvoiceViewer.CrystalReportViewer1.Show()

            CrDiskFileDestinationOptions.DiskFileName = fPath
            CrExportOptions = CrReport.ExportOptions

            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .DestinationOptions = CrDiskFileDestinationOptions
                .FormatOptions = CrFormatTypeOptions
            End With
            CrReport.Export()

            If FILEExists(fPath) Then
                If frmPrintInvCopy.optShow(1).Checked = True Then
                    Process.Start("explorer.exe", fPath)
                End If
            End If

            If frmPrintInvCopy.optShow(2).Checked = True Then

                ''My test

                'Dim mSignerName As String
                Dim mPrintDigitalSign As String
                'mPrintDigitalSign = "Authorised Signatory" ''"For " & RsCompany.Fields("PRINT_COMPANY_NAME").Value  ''"Authorised Signatory"
                'mSignerName = GetDigitalSignName(PubUserID)
                'If mSignerName <> "" Then


                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                    pOutPutFileName = mPubDigitalSignPath & "\Signed_CustCredit_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"
                    mPrintDigitalSign = RsCompany.Fields("PRINT_COMPANY_NAME").Value  ''"Authorised Signatory"
                Else
                    pOutPutFileName = mPubBarCodePath & "\TaxInvoice_DigialSign_" & RsCompany.Fields("COMPANY_CODE").Value & "_" & mBillNoStr & ".pdf"
                    mPrintDigitalSign = "Authorised Signatory" ''"For " & RsCompany.Fields("PRINT_COMPANY_NAME").Value  ''"Authorised Signatory"
                End If

                If SignPdf(fPath, pOutPutFileName, mPrintDigitalSign) = False Then Exit Sub

                If FILEExists(pOutPutFileName) Then
                    Process.Start("explorer.exe", pOutPutFileName)
                End If
            End If
            'End If
        Else
            If mMode = Crystal.DestinationConstants.crptToWindow Then
                FrmInvoiceViewer.CrystalReportViewer1.ReportSource = Nothing
                'FrmInvoiceViewer.CrystalReportViewer1.DataBindings()
                FrmInvoiceViewer.CrystalReportViewer1.ReportSource = CrReport
                FrmInvoiceViewer.CrystalReportViewer1.Show()
                FrmInvoiceViewer.MdiParent = Me.MdiParent
                FrmInvoiceViewer.CrystalReportViewer1.ShowGroupTreeButton = False
                FrmInvoiceViewer.CrystalReportViewer1.DisplayGroupTree = False
                FrmInvoiceViewer.Dock = DockStyle.Fill
                FrmInvoiceViewer.Show()
            Else

                'CrReport.PrintToPrinter(1, False, 1, 99)

                'For Each prt In PrinterSettings.InstalledPrinters       ''Printers
                '    If UCase(prt) = UCase("Universal Printer") Then
                '        CrReport.PrintOptions.PrinterName = prt.DeviceName
                '        Exit For
                '    End If
                'Next
                Dim settings As PrinterSettings = New PrinterSettings()
                For Each printer As String In PrinterSettings.InstalledPrinters

                    If settings.IsDefaultPrinter Then
                        settings.PrinterName = printer
                        Exit For
                    End If
                Next

                CrReport.PrintToPrinter(1, False, 1, 99)
                CrReport.Dispose()
            End If
        End If


        Exit Sub
ErrPart:
        'Resume		
        CrReport.Dispose()
        MsgBox(Err.Description)
    End Sub
    Private Function SelectQryForCRVoucher(ByRef mMKey As String, ByRef mSqlStr As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String

        ''SELECT CLAUSE...

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_BARCODE_PRINT WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        For CntCount = 0 To 0
            SqlStr = "INSERT INTO TEMP_BARCODE_PRINT ( " & vbCrLf _
               & " USER_ID, MKEY, BARCODE_VALUE, PRINT_INVOICE_TYPE,PRINT_SEQ  ) VALUES (" & vbCrLf _
               & " '" & MainClass.AllowSingleQuote(PubUserID) & "','" & mMKey & "','',''," & CntCount & ")"

            PubDBCn.Execute(SqlStr)
        Next

        PubDBCn.CommitTrans()

        mSqlStr = " SELECT * "

        ''FROM CLAUSE...							
        mSqlStr = mSqlStr & vbCrLf _
            & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_SALE_DET ID, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST, TEMP_BARCODE_PRINT BP "


        ''WHERE CLAUSE...							
        mSqlStr = mSqlStr & vbCrLf _
            & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.MKEY=BP.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY='" & mMKey & "'" & vbCrLf _
            & " AND BP.USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        ''ORDER CLAUSE...							

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY BP.PRINT_SEQ,BP.PRINT_INVOICE_TYPE,ID.SUBROWNO"

        'mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, CMST.SUPP_CUST_NAME "

        '''FROM CLAUSE...
        'mSqlStr = mSqlStr & vbCrLf & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_SALE_DET ID, FIN_SUPP_CUST_MST CMST "

        '''WHERE CLAUSE...
        'mSqlStr = mSqlStr & vbCrLf _
        '    & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY " & vbCrLf _
        '    & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
        '    & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
        '    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        '    & " AND IH.MKEY='" & LblMKey.Text & "'" & vbCrLf _
        '    & " AND IH.BOOKTYPE='" & mBookType & "'" & vbCrLf _
        '    & " AND IH.BOOKSUBTYPE='" & mBookSubType & "' AND GOODS_SERVICE='" & lblGoodService.Text & "'"


        '''ORDER CLAUSE...
        'mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SUBROWNO"
        SelectQryForCRVoucher = mSqlStr
        Exit Function
ErrPart:

    End Function
    Private Function SelectQryForPrint(ByRef mMKey As String, mCustomerCode As String) As String

        Dim pBarCodeString As String = ""
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mInvoicePrintType As String
        Dim CntCount As Integer
        Dim mUpdateStart As Boolean
        Dim mSqlStr As String
        On Error GoTo ErrPart

        mUpdateStart = True
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_BARCODE_PRINT WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        pBarCodeString = ""

        ''HERO HONDA BARCODE.........							
        If InStr(1, pBARCODEFORMAT1, mCustomerCode, CompareMethod.Text) >= 1 Then
            Call PrintBarcode1(pBarCodeString, mMKey, "N", True)
        End If

        For CntCount = 0 To 5
            If frmPrintInvCopy.chkPrintOption(CntCount).CheckState = System.Windows.Forms.CheckState.Checked Then
                mInvoicePrintType = UCase(frmPrintInvCopy.chkPrintOption(CntCount).Text)
                SqlStr = "INSERT INTO TEMP_BARCODE_PRINT ( " & vbCrLf _
                    & " USER_ID, MKEY, BARCODE_VALUE, PRINT_INVOICE_TYPE,PRINT_SEQ ) VALUES (" & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "','" & mMKey & "','" _
                    & pBarCodeString & "','" & mInvoicePrintType & "'," & CntCount & ")"

                PubDBCn.Execute(SqlStr)
            End If
        Next

        PubDBCn.CommitTrans()

        mUpdateStart = False

        mSqlStr = " SELECT * "

        ''FROM CLAUSE...							
        mSqlStr = mSqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST, DSP_DESPATCH_DET IDD,TEMP_BARCODE_PRINT BP "


        ''WHERE CLAUSE...							
        mSqlStr = mSqlStr & vbCrLf _
            & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.MKEY=BP.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY='" & mMKey & "'" & vbCrLf _
            & " AND IH.COMPANY_CODE=IDD.COMPANY_CODE" & vbCrLf _
            & " AND IH.AUTO_KEY_DESP=IDD.AUTO_KEY_DESP" & vbCrLf _
            & " AND ID.ITEM_CODE=IDD.ITEM_CODE AND ID.SUBROWNO=IDD.SERIAL_NO AND BP.USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        ''ORDER CLAUSE...							

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY BP.PRINT_SEQ,BP.PRINT_INVOICE_TYPE,ID.SUBROWNO"

        SelectQryForPrint = mSqlStr
        Exit Function
ErrPart:
        If mUpdateStart = True Then
            PubDBCn.RollbackTrans()
        End If
        SelectQryForPrint = ""
    End Function
    Private Sub ShowExcisePDFReport(ByRef mSqlStr As String, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String,
                                    ByRef IsSubReport As Boolean, ByVal mPrintOption As String, ByRef mMkey As String, ByRef mCustomerCode As String,
                                    ByRef pBillLocation As String, ByRef mInvoiceSeq As String, pBillNo As String, ByRef pBillDate As String, ByRef mIRNNo As String, ByRef mPrePrint As String, ByRef mIseMail As String, ByRef mVendorCode As String)

        On Error GoTo ErrPart
        Dim RsTemp As New ADODB.Recordset
        Dim RS As New ADODB.Recordset

        Dim fPath As String
        Dim efPath As String

        Dim mAmountInword As String
        Dim SqlStrSub As String
        Dim mDutyInword As String
        Dim SqlStr As String = ""

        Dim RsTempShip As ADODB.Recordset = Nothing
        Dim mNetAmount As Double
        Dim mNetDuty As Double
        Dim mPrepTime As String = ""
        Dim mRemovalTime As String = ""
        Dim mManuAVInWord As String
        Dim mManuCessInWord As String
        Dim mManuEDInWord As String
        Dim mManuHCessInWord As String
        Dim mDealerDetail As String
        Dim mDealerAddress As String
        Dim mManuAddress As String
        Dim mSO As Double
        Dim mPayTerms As String
        Dim mBalPayTerms As String
        Dim mJurisdiction As String
        Dim mShipToSameParty As String
        Dim mShipToCode As String

        Dim mShipToName As String = ""
        Dim mShipToAddress As String = ""
        Dim mShipToCity As String = ""
        Dim mShipToGSTN As String = ""
        Dim mCompanyDetail As String = ""
        Dim mCompanyeMail As String = ""
        Dim mCompanyWebSite As String = ""
        Dim mShipToState As String = ""
        Dim mShipToStateCode As String = ""
        Dim mStateName As String = ""
        Dim mStateCode As String = ""
        Dim mWithInState As String = ""
        Dim mWithInCountry As String = ""
        Dim mPlaceofSupply As String = ""
        Dim mExpHeading As String
        Dim mLUT As String
        Dim pBarCodeString As String

        Dim mShipFromOtherThan As String
        Dim mShipFromCode As String
        Dim mShipFromName As String
        Dim mShipFromAddress As String
        Dim mShipFromCity As String
        Dim mShipFromState As String
        Dim mShipFromStateCode As String
        Dim mShipFromGSTN As String
        Dim mExWork As String
        Dim path As String
        Dim mCurrency As String
        Dim mRateTitle As String
        Dim mAmountTitle As String
        Dim mShipLocation As String

        Dim CrReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument  ' Report Name 
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions

        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions
        Dim mHour As String = ""
        Dim mMin As String = ""
        Dim mShipToPAN As String = ""
        Dim mDespRefType As String
        Dim mEPCGNo As String
        Dim mEPCGDate As String
        Dim xReportFileName As String
        Dim mShipToPIN As String
        Dim mShipToPhoneNo As String
        Dim mShipToMailID As String
        Dim mCreditAccountCode As String
        Dim mInterUnit As String

        xReportFileName = mRptFileName

        mRptFileName = PubReportFolderPath & "PDF_" & mRptFileName
        CrReport.Load(mRptFileName)

        SqlStrSub = " SELECT * " & vbCrLf _
            & " FROM FIN_INVOICE_EXP, FIN_INVOICE_HDR, FIN_INTERFACE_MST " & vbCrLf _
            & " WHERE FIN_INVOICE_EXP.MKEY = FIN_INVOICE_HDR.MKEY " & vbCrLf _
            & " AND FIN_INVOICE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _
            & " AND FIN_INVOICE_HDR.COMPANY_CODE = FIN_INTERFACE_MST.COMPANY_CODE" & vbCrLf _
            & " And FIN_INVOICE_EXP.MKEY='" & MainClass.AllowSingleQuote(mMkey) & "'" & vbCrLf _
            & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

        SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"

        SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

        Call Connect_MainReport_To_Database_11(CrReport)        '', mSqlStr
        Call Connect_SubReport_To_Database_11(CrReport, "PurExp")      '

        CrReport.RecordSelectionFormula = "{IH.COMPANY_CODE} = " & RsCompany.Fields("COMPANY_CODE").Value & " AND {IH.MKEY} = '" & MainClass.AllowSingleQuote(mMkey) & "' AND {IH.FYEAR} = '" & RsCompany.Fields("FYEAR").Value & "' AND {BP.USER_ID} = '" & MainClass.AllowSingleQuote(PubUserID) & "'"

        ClearCRpt11Formulas(CrReport)
        CrReport.ReportOptions.EnableSaveDataWithReport = False
        SetCompanyReport11(CrReport, 1, mTitle, mSubTitle)
        'CrReport.VerifyDatabase()   .VerifyOnEveryPrint = True '' blnVerifyOnEveryPrint		
        CrReport.Refresh()

        mStateName = ""
        mStateCode = ""

        mStateName = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(pBillLocation), "SUPP_CUST_STATE")
        mStateCode = GetStateCode(mStateName)

        mWithInState = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(pBillLocation), "WITHIN_STATE")

        If mWithInState = "N" Then
            If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInCountry = MasterNo
            End If
        End If

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If


        mPlaceofSupply = VB6.Format(mStateCode, "00") & "-" & mStateName ''IIf(mWithInState = "Y", "INTRA STATE", IIf(mWithInCountry = "Y", "INTER STATE", "EXPORT"))							
        Dim pServProvided As String = ""
        Dim mSACCode As String = ""
        Dim mIsLUT As String = "N"
        SqlStr = " SELECT ACCOUNTCODE, NETVALUE, ITEMVALUE, NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT,INV_PREP_TIME, " & vbCrLf _
                & " SHIPPED_TO_PARTY_CODE, REMOVAL_TIME, OUR_AUTO_KEY_SO, SHIPPED_TO_SAMEPARTY, " & vbCrLf _
                & " IS_DESP_OTHERTHAN_BILL, SHIPPED_FROM_PARTY_CODE, IS_SHIPPTO_EX_WORK,SHIP_TO_LOC_ID,SAC_CODE,IS_LUT, REF_DESP_TYPE" & vbCrLf _
                & " FROM FIN_INVOICE_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND MKEY='" & MainClass.AllowSingleQuote(mMkey) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If CDbl(mInvoiceSeq) = 3 Or CDbl(mInvoiceSeq) = 5 Then
                mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                mNetDuty = 0 ''IIf(IsNull(RsTemp!NETCGST_AMOUNT), 0, RsTemp!NETCGST_AMOUNT)					
            Else
                mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                mNetDuty = IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
                mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
                mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)
            End If
            pServProvided = ""
            mIsLUT = IIf(IsDBNull(RsTemp.Fields("IS_LUT").Value), "N", RsTemp.Fields("IS_LUT").Value)
            mSACCode = IIf(IsDBNull(RsTemp.Fields("SAC_CODE").Value), "", RsTemp.Fields("SAC_CODE").Value)
            mCreditAccountCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNTCODE").Value), "", RsTemp.Fields("ACCOUNTCODE").Value)
            If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                pServProvided = Trim(IIf(IsDBNull(MasterNo), "", MasterNo))
            Else
                pServProvided = ""
            End If

            mShipToSameParty = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
            mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
            mExWork = IIf(IsDBNull(RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value), "N", RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value)

            mPrepTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INV_PREP_TIME").Value), "", RsTemp.Fields("INV_PREP_TIME").Value), "HH:MM")
            mRemovalTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("REMOVAL_TIME").Value), "", RsTemp.Fields("REMOVAL_TIME").Value), "HH:MM")
            mSO = IIf(IsDBNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), 0, RsTemp.Fields("OUR_AUTO_KEY_SO").Value)
            mDespRefType = IIf(IsDBNull(RsTemp.Fields("REF_DESP_TYPE").Value), "", RsTemp.Fields("REF_DESP_TYPE").Value)

            mHour = HoursInText(VB.Left(mRemovalTime, 2))
            mMin = MinInText(VB.Right(mRemovalTime, 2))

            mHour = mHour & " " & mMin

            If mExWork = "Y" Then ''mShipToSameParty						
                mShipToName = "Ex Work"
                mShipToAddress = ""
                mShipToCity = ""
                mShipToGSTN = ""
                mShipToState = ""
                mShipToStateCode = ""
            Else
                If mShipToSameParty = "Y" Then
                    mShipToCode = mCustomerCode
                    mShipLocation = Trim(pBillLocation)
                Else
                    mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
                    mShipLocation = IIf(IsDBNull(RsTemp.Fields("SHIP_TO_LOC_ID").Value), "", RsTemp.Fields("SHIP_TO_LOC_ID").Value) ''Trim(TxtShipTo.Text)
                End If
                ''Sandeep 04022023
                'SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf _
                '    & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf _
                '    & " FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
                '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                '    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipToCode) & "' AND LOCATION_ID='" & MainClass.AllowSingleQuote(mShipLocation) & "'"

                SqlStr = "SELECT A.*, B.SUPP_CUST_NAME,PAN_NO FROM FIN_SUPP_CUST_BUSINESS_MST A, FIN_SUPP_CUST_MST B" & vbCrLf _
                    & " Where A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE" & vbCrLf _
                    & " AND B.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipToCode) & "' AND A.LOCATION_ID='" & MainClass.AllowSingleQuote(mShipLocation) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTempShip.EOF = False Then
                    mShipToName = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_NAME").Value), "", RsTempShip.Fields("SUPP_CUST_NAME").Value)
                    mShipToAddress = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_ADDR").Value), "", RsTempShip.Fields("SUPP_CUST_ADDR").Value)
                    mShipToAddress = Replace(mShipToAddress, vbCrLf, "")
                    mShipToCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                    mShipToCity = mShipToCity & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
                    mShipToState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                    mShipToStateCode = GetStateCode(mShipToState)
                    mShipToGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)

                    mShipToPAN = ""

                    If MainClass.ValidateWithMasterTable(mShipToName, "SUPP_CUST_NAME", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mShipToPAN = MasterNo
                    End If

                    mShipToPIN = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
                    mShipToPhoneNo = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PHONE").Value), "", RsTempShip.Fields("SUPP_CUST_PHONE").Value) ' , 
                    mShipToMailID = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_MAILID").Value), "", RsTempShip.Fields("SUPP_CUST_MAILID").Value)


                End If
            End If


            mShipFromOtherThan = IIf(IsDBNull(RsTemp.Fields("IS_DESP_OTHERTHAN_BILL").Value), "N", RsTemp.Fields("IS_DESP_OTHERTHAN_BILL").Value)
            mShipFromCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_FROM_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_FROM_PARTY_CODE").Value)

            mShipFromName = ""
            mShipFromAddress = ""
            mShipFromAddress = ""
            mShipFromCity = ""
            mShipFromCity = ""
            mShipFromState = ""
            mShipFromStateCode = ""
            mShipFromGSTN = ""

            If mShipFromOtherThan = "Y" Then
                SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf _
                    & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipFromCode) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTempShip.EOF = False Then
                    mShipFromName = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_NAME").Value), "", RsTempShip.Fields("SUPP_CUST_NAME").Value)
                    mShipFromAddress = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_ADDR").Value), "", RsTempShip.Fields("SUPP_CUST_ADDR").Value)
                    mShipFromAddress = Replace(mShipFromAddress, vbCrLf, "")
                    mShipFromCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                    mShipFromCity = mShipFromCity & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
                    mShipFromState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                    mShipFromStateCode = GetStateCode(mShipFromState)
                    mShipFromGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)

                    ''Despatch From ...				


                    AssignCRpt11Formulas(CrReport, "ShipFromName", "'" & mShipFromName & "'")
                    AssignCRpt11Formulas(CrReport, "ShipFromAddress", "'" & mShipFromAddress & "'")
                    AssignCRpt11Formulas(CrReport, "ShipFromCity", "'" & mShipFromCity & "'")
                    AssignCRpt11Formulas(CrReport, "ShipFromState", "'" & mShipFromState & "'")

                End If
            End If

        End If


        'AssignCRpt11Formulas(CrReport, "InvoicePrintType", "'" & mInvoicePrintType & "'")
        AssignCRpt11Formulas(CrReport, "CompanyCity", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyGSTIN", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & "'")

        mJurisdiction = "All Disputes Subject to " & IIf(IsDBNull(RsCompany.Fields("JURISDICTION").Value), "", RsCompany.Fields("JURISDICTION").Value) & " Jurisdiction."

        AssignCRpt11Formulas(CrReport, "COMPANYTINNo", "'" & IIf(IsDBNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value) & "'")
        AssignCRpt11Formulas(CrReport, "COMPANYCINNo", "'" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & "'")

        mCompanyeMail = IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", "e-mail : " & RsCompany.Fields("COMPANY_MAILID").Value)
        mCompanyWebSite = IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", "WebSite : " & RsCompany.Fields("WEBSITE").Value)
        mCompanyDetail = mCompanyeMail & ", " & mCompanyWebSite
        Dim mJWRemarks As String = ""

        ''-------------
        AssignCRpt11Formulas(CrReport, "CompanyAddressNew", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPin", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyState", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPhone", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", RsCompany.Fields("COMPANY_PHONE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyFax", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_FAXNO").Value), "", RsCompany.Fields("COMPANY_FAXNO").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyEmail", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", RsCompany.Fields("COMPANY_MAILID").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyWeb", "'" & IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", RsCompany.Fields("WEBSITE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPAN", "'" & IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value) & "'")
        Dim mCompanyStateCode As String = GetStateCode(IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & "")
        AssignCRpt11Formulas(CrReport, "CompanyStateCode", "'" & mCompanyStateCode & "'")
        ''---------------

        AssignCRpt11Formulas(CrReport, "COMPANYDETAIL", "'" & mCompanyDetail & "'")
        AssignCRpt11Formulas(CrReport, "PrepTime", "'" & mPrepTime & "'")
        AssignCRpt11Formulas(CrReport, "RemovalTime", "'" & mRemovalTime & "'")
        AssignCRpt11Formulas(CrReport, "RemovalTimeInWord", "'" & mHour & "'")
        AssignCRpt11Formulas(CrReport, "ShipToPAN", "'" & mShipToPAN & "'")



        AssignCRpt11Formulas(CrReport, "JWRemarks", "'" & mJWRemarks & "'")
        AssignCRpt11Formulas(CrReport, "Jurisdiction", "'" & mJurisdiction & "'")
        AssignCRpt11Formulas(CrReport, "mShipToName", "'" & mShipToName & "'")
        AssignCRpt11Formulas(CrReport, "mShipToAddress", "'" & mShipToAddress & "'")
        AssignCRpt11Formulas(CrReport, "mShipToCity", "'" & mShipToCity & "'")
        AssignCRpt11Formulas(CrReport, "mShipToGSTN", "'" & mShipToGSTN & "'")
        AssignCRpt11Formulas(CrReport, "mShipToState", "'" & mShipToState & "'")
        AssignCRpt11Formulas(CrReport, "mShipToStateCode", "'" & mShipToStateCode & "'")
        AssignCRpt11Formulas(CrReport, "mStateName", "'" & mStateName & "'")
        AssignCRpt11Formulas(CrReport, "mStateCode", "'" & mStateCode & "'")
        AssignCRpt11Formulas(CrReport, "mPlaceofSupply", "'" & mPlaceofSupply & "'")
        AssignCRpt11Formulas(CrReport, "mServiceName", "'" & Trim(pServProvided) & "'")

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            AssignCRpt11Formulas(CrReport, "mShipToPIN", "'" & mShipToPIN & "'")
            AssignCRpt11Formulas(CrReport, "mShipToPhoneNo", "'" & mShipToPhoneNo & "'")
            AssignCRpt11Formulas(CrReport, "mShipToMailID", "'" & mShipToMailID & "'")

            If UCase(Mid(mRptFileName, Len(mRptFileName) - 6)) = "_A3.RPT" Then
                AssignCRpt11Formulas(CrReport, "PrePrint", "'" & mPrePrint & "'")
            End If
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 103 Then
            Dim mBalancePayTerms As String = ""
            If MainClass.ValidateWithMasterTable(mSO, "AUTO_KEY_SO", "BALANCE_PAY_DTL", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O' AND SO_APPROVED='Y'") = True Then
                mBalancePayTerms = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If
            AssignCRpt11Formulas(CrReport, "payterms", "'" & Trim(mBalancePayTerms) & "'")
        End If

        If mDespRefType = "P" And Mid(RsCompany.Fields("COMPANY_NAME").Value, 1, 3) = "KAY" Then
            Dim mSaleAgreementNo As String = ""
            Dim mSaleAgreementDate As String = ""

            If MainClass.ValidateWithMasterTable(mSO, "AUTO_KEY_SO", "SCHD_AGREEMENT_NO", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O' AND SO_APPROVED='Y'") = True Then
                mSaleAgreementNo = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If
            If MainClass.ValidateWithMasterTable(mSO, "AUTO_KEY_SO", "SCHD_AGREEMENT_DATE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O' AND SO_APPROVED='Y'") = True Then
                mSaleAgreementDate = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If
            If mSaleAgreementNo = "" Then
                mSaleAgreementNo = ""
            Else
                mSaleAgreementNo = "Schedule Agreement No : " & mSaleAgreementNo & " Dated : " & VB6.Format(mSaleAgreementDate, "DD/MM/YYYY")
            End If

            AssignCRpt11Formulas(CrReport, "SaleAgreementNo", "'" & Trim(mSaleAgreementNo) & "'")
            'AssignCRpt11Formulas(CrReport, "SaleAgreementDate", "'" & Trim(mSaleAgreementDate) & "'")
        End If

        '' & " AND CUST_PO_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "'" & vbCrLf _
        '' & " AND CUST_PO_DATE=TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SO_STATUS='O'"

        'If UCase(xReportFileName) = "INVOICE_SGST.RPT" Or UCase(xReportFileName) = "INVOICE_IGST.RPT" Or UCase(xReportFileName) = "INVOICE_SGST_L.RPT" Or UCase(xReportFileName) = "INVOICE_IGST_L.RPT" Then
        mEPCGNo = ""
        mEPCGDate = ""
        SqlStr = " SELECT EPCG_NO, EPCG_DATE  " & vbCrLf _
            & " FROM DSP_SALEORDER_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_SO=" & Val(mSO) & "" & vbCrLf _
            & " AND SO_STATUS='O' AND SO_APPROVED='Y'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempShip.EOF = False Then
            mEPCGNo = IIf(IsDBNull(RsTempShip.Fields("EPCG_NO").Value), "", RsTempShip.Fields("EPCG_NO").Value)
            mEPCGDate = VB6.Format(IIf(IsDBNull(RsTempShip.Fields("EPCG_DATE").Value), "", RsTempShip.Fields("EPCG_DATE").Value), "DD/MM/YYYY")
        End If

        If mEPCGNo <> "" Then
            mEPCGNo = "EPCG License No : " & mEPCGNo & " &  Date : " & mEPCGDate
            AssignCRpt11Formulas(CrReport, "EPCGNo", "'" & mEPCGNo & "'")
            'MainClass.AssignCRptFormulas(Report1, "EPCGNo="'" & mEPCGNo & """")
        End If


        'End If

        If Val(mInvoiceSeq) = 6 Then
            If mIsLUT = "Y" Then
                mLUT = GetLUT((pBillDate))
            Else
                mLUT = ""
            End If

            AssignCRpt11Formulas(CrReport, "LUTNo", "'" & mLUT & "'")
            mExpHeading = "SUPPLY MEANT FOR EXPORT ON PAYMENT OF IGST OR SUPPLY MEANT FOR EXPORT UNDER BOND OR LETTER OF UNDERTAKING WITHOUT PAYMENT OF IGST"
            'MainClass.AssignCRptFormulas(Report1, "ExpHeading=""" & mExpHeading & """")

        End If

        Dim mAccountPostingHead As String
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y" And (xReportFileName = "Invoice_IGST.rpt" Or xReportFileName = "Invoice_SGST.rpt") Then

            If MainClass.ValidateWithMasterTable(mCreditAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountPostingHead = MasterNo
            End If

            AssignCRpt11Formulas(CrReport, "AccountPostingHead", "'" & mAccountPostingHead & "'")
        End If

        'mPayTerms = ""

        If IsSubReport = True Then

            If mNetAmount = 0 Then
                mAmountInword = " Zero Only"
            Else
                mAmountInword = MainClass.RupeesConversion(mNetAmount) & " Only"
            End If

            mDutyInword = MainClass.RupeesConversion(mNetDuty) & " Only"

            AssignCRpt11Formulas(CrReport, "AmountInWord", "'" & mAmountInword & "'")
            AssignCRpt11Formulas(CrReport, "NetAmount", "'" & VB6.Format(mNetAmount, "0.00") & "'")
            AssignCRpt11Formulas(CrReport, "DutyInword", "'" & mDutyInword & "'")

        End If


        Dim mBMPFileName As String = ""
        mBMPFileName = RefreshQRCode(mMkey, pBillNo, mIRNNo)
        Application.DoEvents()

        If Not FILEExists(mBMPFileName) Then
            mBMPFileName = ""
        End If
        AssignCRpt11Formulas(CrReport, "PicLocation", "'" & mBMPFileName & "'")

        Dim mClientLogoPath As String = ""
        If Not FILEExists(PubClientLogoPath) Then
            mClientLogoPath = ""
        Else
            mClientLogoPath = PubClientLogoPath
        End If
        AssignCRpt11Formulas(CrReport, "CompanyLogo", "'" & mClientLogoPath & "'")

        If VB.Left(mPrintOption, 3) = "PDF" Then
            Dim pOutPutFileName As String = ""

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                mVendorCode = IIf(mVendorCode = "", "TaxInvoice", mVendorCode)
                fPath = mPubBarCodePath & "\" & mVendorCode & "_" & pBillNo & "_" & VB6.Format(pBillDate, "DDMMYYYY") & ".pdf"
            Else
                fPath = mPubBarCodePath & "\TaxInvoice_" & RsCompany.Fields("COMPANY_CODE").Value & "_" & pBillNo & ".pdf"
            End If

            efPath = fPath

            ''mVendorCode = IIf(mVendorCode = "", "DS", "DS_" & mVendorCode)
            'pOutPutFileName = mPubBarCodePath & "\" & mVendorCode & "_" & pBillNo & "_" & mCustomerCode & ".pdf"

            ''temp check
            'FrmInvoiceViewer.CrystalReportViewer1.ReportSource = Nothing
            'FrmInvoiceViewer.CrystalReportViewer1.ReportSource = CrReport
            'FrmInvoiceViewer.CrystalReportViewer1.Show()

            CrDiskFileDestinationOptions.DiskFileName = fPath
            CrExportOptions = CrReport.ExportOptions

            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .DestinationOptions = CrDiskFileDestinationOptions
                .FormatOptions = CrFormatTypeOptions
            End With
            CrReport.Export()

            'If FILEExists(fPath) Then
            '    Process.Start("explorer.exe", fPath)
            'End If

            'MsgBox("Start 1")
            If Mid(mPrintOption, 1, 4) = "PDFS" Then
                'Dim mCustomerName As String = mCustomerCode.Substring(0, mCustomerCode.IndexOf(" "))

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                    pOutPutFileName = mPubDigitalSignPath & "\Signed_" & pBillNo & "_" & mCustomerCode & ".pdf"
                ElseIf RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                    mVendorCode = IIf(mVendorCode = "", "DS", mVendorCode)
                    pOutPutFileName = mPubBarCodePath & "\" & mVendorCode & "_" & pBillNo & "_" & VB6.Format(pBillDate, "DDMMYYYY") & ".pdf"
                    ''pOutPutFileName = mPubBarCodePath & "\" & mVendorCode & "_" & pBillNo & "_" & mCustomerCode & ".pdf"
                Else
                    pOutPutFileName = mPubBarCodePath & "\TaxInvoice_DigialSign_" & RsCompany.Fields("COMPANY_CODE").Value & "_" & pBillNo & ".pdf"
                End If

                'MsgBox(pOutPutFileName)

                If SignPdf(fPath, pOutPutFileName, "Authorised Signatory") = False Then Exit Sub

                efPath = pOutPutFileName
                ''Authorised Signatory
                'If FILEExists(pOutPutFileName) Then
                '    Process.Start("explorer.exe", pOutPutFileName)
                'End If
            End If


            If mPrintOption = "PDFSP" Then
                Dim defaultPrinterSetting As System.Drawing.Printing.PrinterSettings = Nothing


                'Get de the default printer in the system
                defaultPrinterSetting = DocumentPrinter.GetDefaultPrinterSetting

                'uncomment if you want to change the default printer before print
                'DocumentPrinter.ChangePrinterSettings(defaultPrinterSetting)

                'print your file 
                If DocumentPrinter.PrintFile(pOutPutFileName, defaultPrinterSetting) Then
                    'MsgBox("your print file success message")
                Else
                    MsgBox("your print file failed message")
                End If

                'PrintersDialog.Document = pOutPutFileName
                'AddHandler p_Document.PrintPage, AddressOf HandleOnPrintPage

                'Using document = PdfDocument.Load(pOutPutFileName)
                '    ' Print the document to default printer
                '    document.Print()
                'End Using
            End If

            If mIseMail = "Y" Then
                If SendeMail(efPath, mCustomerCode, pBillNo, pBillDate) = False Then GoTo ErrPart
            End If
        Else
            'Call CrReport.PrintOptions
            'CrReport.PrintToPrinter(1, True, 1, 99)
            Dim settings As PrinterSettings = New PrinterSettings()
            For Each printer As String In PrinterSettings.InstalledPrinters

                If settings.IsDefaultPrinter Then
                    settings.PrinterName = printer
                    Exit For
                End If
            Next
            CrReport.Refresh()
            Application.DoEvents()
            Threading.Thread.Sleep(6000)
            CrReport.PrintToPrinter(1, True, 0, 0)     ''CrReport.PrintToPrinter(1, True, 1, 99)   
            Application.DoEvents()
            Threading.Thread.Sleep(6000)

        End If
        FrmInvoiceViewer.Close()
        FrmInvoiceViewer.Dispose()
        CrReport.Close()
        CrReport.Dispose()
        Exit Sub
ErrPart:
        'Resume							
        MsgBox(Err.Description)
    End Sub
    Private Function SendeMail(ByRef mAttachmentFile As String, ByRef mAccountCode As String, ByRef pBillNo As String, ByRef pBillDate As String) As Boolean
        On Error GoTo ErrPart
        Dim mCC As String
        Dim mFrom As String
        Dim mSubject As String
        Dim mBodyTextHeader As String
        Dim mBodyText As String
        Dim mBodyTextDetail As String
        Dim mBcc As String = ""
        Dim mTo As String = ""
        Dim mAccountName As String = ""

        SendeMail = False
        ' *****************************************************************************
        ' This is where all of the Components Properties are set / Methods called
        ' *****************************************************************************


        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountName = MasterNo
        End If


        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_MAILID", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mTo = MasterNo
        End If


        If Trim(mTo) = "" Then
            MsgInformation("Customer eMail ID is Not defined in Master.")
            SendeMail = True
            Exit Function
        End If

        mFrom = GetEMailID("MAIL_FROM") 'mFrom = GetEMailID("PUR_MAIL_TO")
        'mCC = GetEMailID("PUR_MAIL_TO")
        mCC = GetEMailID("ACCT_MAIL_TO")
        mSubject = "Invoice Copy of Bill No : " & pBillNo & " and Dated : " & pBillDate ''Auto Generated Salary Slip for the month of " & vb6.Format(lblRunDate, "MMMM , YYYY")

        mBodyText = "<html><body><br />" _
            & "<b></b><br />" _
            & "<b></b>To " _
            & mAccountName _
            & ",<br />" _
            & "<b></b><br />" _
            & "<b></b>Dear Sir/Madam,<br />" _
            & "<b></b><br />" _
            & "<b></b>Please find the " _
            & mSubject _
            & ".<br />" _
            & "<br />" _
            & "<br />" _
            & "Your Faithfully<br />" _
            & "for " & RsCompany.Fields("Company_Name").Value _
            & "<br />" _
            & "</body></html>"

        If Trim(mTo) <> "" Then
            If SendMailProcess(mFrom, mTo, mCC, mBcc, mAttachmentFile, mSubject, mBodyText) = False Then GoTo ErrPart
        End If
        SendeMail = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        SendeMail = False
        '    Resume
    End Function
    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        On Error GoTo ErrPart
        Dim mInvoicePrint As Boolean
        Dim mAnnexPrint As String
        Dim mSubsidiaryChallanPrint As String
        Dim mEXPAnnexPrint As String
        Dim mMaxRow As Integer
        Dim mSC_All As String
        Dim mSC_F4No As String
        Dim CntCount As Integer
        Dim mInvoicePrintType As String = ""
        Dim mExtraRemarks As String
        Dim mPrintOption As String = ""
        Dim mMKey As String
        Dim mPrintA4 As String
        Dim mPaperStyle As String
        Dim mPrintPaperSize As String

        frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(4).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(3).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).Enabled = False



        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            frmPrintInvCopy._optShow_0.Text = "Print"
            frmPrintInvCopy._optShow_0.Enabled = True
            frmPrintInvCopy._optShow_2.Checked = True
            frmPrintInvCopy._optShow_1.Enabled = False
            frmPrintInvCopy._optShow_2.Enabled = True
        Else
            frmPrintInvCopy._optShow_0.Text = "Print"
            frmPrintInvCopy._optShow_0.Enabled = True
            frmPrintInvCopy._optShow_0.Checked = True
            frmPrintInvCopy._optShow_1.Enabled = False
            frmPrintInvCopy._optShow_2.Enabled = True
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
            frmPrintInvCopy._optShow_5.Visible = True
            frmPrintInvCopy._optShow_6.Visible = True
        Else
            frmPrintInvCopy._optShow_5.Visible = False
            frmPrintInvCopy._optShow_6.Visible = False
        End If

        frmPrintInvCopy.optPrintPortrait.Checked = IIf(RsCompany.Fields("INVOICE_PRINT_STYLE").Value = "P", True, False)
        frmPrintInvCopy.optPrintLandScape.Checked = IIf(RsCompany.Fields("INVOICE_PRINT_STYLE").Value = "P", False, True)

        mPrintA4 = IIf(IsDBNull(RsCompany.Fields("INVOICE_A4").Value), "Y", RsCompany.Fields("INVOICE_A4").Value)
        frmPrintInvCopy.optA4.Checked = IIf(mPrintA4 = "Y", True, False)
        frmPrintInvCopy.optA3.Checked = IIf(mPrintA4 = "Y", False, True)

        frmPrintInvCopy.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            With SprdMain
                For CntRow = 1 To .MaxRows
                    .Row = CntRow
                    .Col = ColFlag
                    If SprdMain.Value = System.Windows.Forms.CheckState.Checked And frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Checked Then
                        .Col = ColIRNNo
                        If Trim(.Text) = "" Then
                            MsgInformation("IRN Not gererated. so Can not be print Original Invoice.")
                        End If

                    End If
                Next
            End With
        End If

        'If frmPrintInvCopy.optShow(3).Checked = True Or frmPrintInvCopy.optShow(4).Checked = True Then
        '    With SprdMain
        '        For CntRow = 1 To .MaxRows
        '            .Row = CntRow
        '            .Col = ColFlag
        '            If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then
        '                .Col = ColMKey
        '                mMKey = Trim(.Text)
        '                Call ReportOnPackingSlip(Crystal.DestinationConstants.crptToWindow, mMKey, IIf(frmPrintInvCopy.optShow(3).Checked = True, "I", "O"))
        '            End If
        '        Next
        '    End With
        '    frmPrintInvCopy.Dispose()
        '    frmPrintInvCopy.Close()
        '    Exit Sub
        'End If
        Dim mMKeyStr As String = ""
        If frmPrintInvCopy.optShow(3).Checked = True Or frmPrintInvCopy.optShow(4).Checked = True Then
            mMKey = ""
            mMKeyStr = ""
            With SprdMain
                For CntRow = 1 To .MaxRows
                    .Row = CntRow
                    .Col = ColFlag
                    If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then
                        .Col = ColMKey
                        mMKey = Trim(.Text)
                        mMKeyStr = IIf(mMKeyStr = "", "'" & mMKey & "'", mMKeyStr & ",'" & mMKey & "'")
                        'Call ReportOnPackingSlip(Crystal.DestinationConstants.crptToWindow, mMKey, IIf(frmPrintInvCopy.optShow(3).Checked = True, "I", "O"))
                    End If
                Next
            End With

            If mMKeyStr <> "" Then
                Call ReportOnPackingSlip(Crystal.DestinationConstants.crptToPrinter, mMKeyStr, IIf(frmPrintInvCopy.optShow(3).Checked = True, "I", "O"))
            End If

            frmPrintInvCopy.Dispose()
            frmPrintInvCopy.Close()
            Exit Sub
        End If

        mPaperStyle = IIf(frmPrintInvCopy.optPrintPortrait.Checked, "P", "L")
        mPrintPaperSize = IIf(frmPrintInvCopy.optA4.Checked, "Y", "N")

        Dim mPrePrint As String = "N"
        If mPrintPaperSize = "N" Then
            mPrePrint = IIf(frmPrintInvCopy.chkPrePrint.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        End If

        If frmPrintInvCopy._optShow_0.Checked = True Then
            mPrintOption = "PRN"
        Else
            mPrintOption = "PDFSP"
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            If frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Checked And frmPrintInvCopy._optShow_2.Checked = False Then
                If MsgQuestion("Are you Sure want print without Digital Sign?") = CStr(MsgBoxResult.No) Then
                    Exit Sub
                End If
            End If
        End If
        If chkCreditNote.Checked = True Or chkDebitNote.Checked = True Then
            Call ReportOnCreditNote(mPrintOption, mPaperStyle, mPrintPaperSize, mPrePrint, "Y", "N")
        ElseIf chkNonGSTCreditNote.Checked = True Then
            Call ReportOnCreditNote(mPrintOption, mPaperStyle, mPrintPaperSize, mPrePrint, "N", "N")
        Else
            Call ReportOnSales(mPrintOption, mPaperStyle, mPrintPaperSize, mPrePrint, "N")
        End If


        frmPrintInvCopy.Close()
        frmPrintInvCopy.Dispose()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ErrPart:
        frmPrintInvCopy.Close()
        frmPrintInvCopy.Dispose()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ReportOnPackingSlip(ByRef Mode As Crystal.DestinationConstants, ByVal pMKey As String, ByRef pBoxType As String)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNO As String
        Dim mWithInState As String
        Dim mRMCustomer As Boolean
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mPartName As String = ""
        Dim mPartNo As String = ""
        Dim mQtyinBox As Double = 0
        Dim mQtyinBoxA As Double = 0
        Dim mPktQty As Double = 0
        Dim mTotalQty As Double = 0
        Dim mInvoiceNo As String = ""
        Dim mInvoiceDate As String = ""
        Dim mMFGBy As String = ""
        Dim I As Long
        Dim mRowPrinting As Long
        Dim mTotPktQty As Double = 0
        Dim mPKTDesc As String = ""
        Dim mBillNoStr As String
        Dim mBarCode As String = ""

        Dim CrReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument  ' Report Name 
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions

        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions

        'Report1.Reset()
        'MainClass.ClearCRptFormulas(Report1)


        SqlStr = ""
        mTitle = ""
        mSubTitle = ""
        mRptFileName = "PakingSticker.rpt"

        SqlStr = "SELECT BILLNO, INVOICE_DATE, ITEM_DESC, CUSTOMER_PART_NO, INNER_PACK_QTY, INNER_PACK_QTY_A, OUTER_PACK_QTY, OUTER_PACK_QTY_A, ITEM_QTY" & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
                & " And IH.MKEY IN (" & pMKey & ")" & vbCrLf _
                & " And ID.OUTER_PACK_QTY>0 "

        If pBoxType = "I" Then
            SqlStr = SqlStr & vbCrLf & " AND INNER_PACK_QTY=0"
        Else
            SqlStr = SqlStr & vbCrLf & " AND OUTER_PACK_QTY=0"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mBillNoStr = mBillNoStr & IIf(mBillNoStr = "", "", ",") & IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)

                RsTemp.MoveNext()
            Loop
            MsgInformation("Packing Qty not Defined in Bill Nos : " & mBillNoStr)
            Exit Sub
        End If

        SqlStr = "SELECT BILLNO, INVOICE_DATE, ITEM_DESC, CUSTOMER_PART_NO, INNER_PACK_QTY, INNER_PACK_QTY_A, OUTER_PACK_QTY, OUTER_PACK_QTY_A, ITEM_QTY" & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
                & " And IH.MKEY IN (" & pMKey & ")" & vbCrLf _
                & " And ID.OUTER_PACK_QTY>0 ORDER BY BILLNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            PubDBCn.Errors.Clear()

            PubDBCn.BeginTrans()

            SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
            PubDBCn.Execute(SqlStr)

            Do While RsTemp.EOF = False
                mPartName = IIf(IsDBNull(RsTemp.Fields("ITEM_DESC").Value), "", RsTemp.Fields("ITEM_DESC").Value)
                mPartNo = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)
                If pBoxType = "I" Then
                    mQtyinBox = IIf(IsDBNull(RsTemp.Fields("INNER_PACK_QTY").Value), 0, RsTemp.Fields("INNER_PACK_QTY").Value)
                Else
                    mQtyinBox = IIf(IsDBNull(RsTemp.Fields("OUTER_PACK_QTY").Value), 0, RsTemp.Fields("OUTER_PACK_QTY").Value)
                End If

                If pBoxType = "I" Then
                    mQtyinBoxA = IIf(IsDBNull(RsTemp.Fields("INNER_PACK_QTY_A").Value), 0, RsTemp.Fields("INNER_PACK_QTY_A").Value)
                Else
                    mQtyinBoxA = IIf(IsDBNull(RsTemp.Fields("OUTER_PACK_QTY_A").Value), 0, RsTemp.Fields("OUTER_PACK_QTY_A").Value)
                End If

                mTotalQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), "", RsTemp.Fields("ITEM_QTY").Value)

                'mPktQty = Int(mTotalQty / mQtyinBox)

                'mTotPktQty = mPktQty + IIf(mTotalQty > (mPktQty * mQtyinBox), IIf(mQtyinBoxA > 0, 1, 0), 0)

                If pBoxType = "I" Then
                    mPktQty = Int(mTotalQty / mQtyinBox)
                    mQtyinBoxA = IIf(mTotalQty > (mPktQty * mQtyinBox), mTotalQty - (mPktQty * mQtyinBox), 0) ''IIf(mTotalQty > (mPktQty * mQtyinBox), IIf(mQtyinBoxA >= 0, 1, mQtyinBoxA), 0)
                    mTotPktQty = mPktQty + IIf(mTotalQty > (mPktQty * mQtyinBox), 1, 0)
                Else
                    'mPktQty = mQtyinBox ''Int(mTotalQty / mQtyinBox) + IIf(Int(mTotalQty / mQtyinBox) < (mTotalQty / mQtyinBox), 1, 0)
                    'mTotPktQty = mPktQty
                    mPktQty = Int((mTotalQty - mQtyinBoxA) / mQtyinBox)
                    mQtyinBoxA = IIf(mTotalQty > (mPktQty * mQtyinBox), mTotalQty - (mPktQty * mQtyinBox), 0) ''IIf(mTotalQty > (mPktQty * mQtyinBox), IIf(mQtyinBoxA >= 0, 1, mQtyinBoxA), 0)
                    mTotPktQty = mPktQty + IIf(mTotalQty > (mPktQty * mQtyinBox), 1, 0)
                End If

                mInvoiceNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                mInvoiceDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                mMFGBy = IIf(IsDBNull(RsCompany.Fields("PRINT_COMPANY_NAME").Value), "", RsCompany.Fields("PRINT_COMPANY_NAME").Value)
                mRowPrinting = mPktQty       '' Int(mPktQty / 2) + If(mPktQty / 2 > Int(mPktQty / 2), 1, 0)

                For I = 1 To mRowPrinting


                    mPKTDesc = I & "/" & mTotPktQty

                    mBarCode = mInvoiceNo & "#" & mPartNo & "#" & mQtyinBox & "#" & I

                    SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf _
                        & " FIELD1, FIELD2, FIELD3," & vbCrLf _
                        & " FIELD4, FIELD5, FIELD6, FIELD7,FIELD8,FIELD9 ) " & vbCrLf _
                        & " VALUES (" & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & I & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mPartName) & "', '" & MainClass.AllowSingleQuote(mPartNo) & "'," & vbCrLf _
                        & " '" & mQtyinBox & "', '" & mTotalQty & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mInvoiceNo) & "', '" & mInvoiceDate & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mMFGBy) & "','" & mPKTDesc & "','" & mBarCode & "') "


                    PubDBCn.Execute(SqlStr)
                Next

                'mPktQty = 1

                'mRowPrinting = 1
                If mQtyinBoxA > 0 And mTotalQty > (mPktQty * mQtyinBox) Then

                    'mQtyDesc = mQtyinBoxA
                    mPKTDesc = mRowPrinting + 1 & "/" & mTotPktQty

                    For I = 1 To 1

                        mBarCode = mInvoiceNo & "#" & mPartNo & "#" & mQtyinBoxA & "#" & I

                        SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf _
                            & " FIELD1, FIELD2, FIELD3," & vbCrLf _
                            & " FIELD4, FIELD5, FIELD6, FIELD7 ,FIELD8,FIELD9) " & vbCrLf _
                            & " VALUES (" & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & I & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mPartName) & "', '" & MainClass.AllowSingleQuote(mPartNo) & "'," & vbCrLf _
                            & " '" & mQtyinBoxA & "', '" & mTotalQty & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mInvoiceNo) & "', '" & mInvoiceDate & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mMFGBy) & "','" & mPKTDesc & "','" & mBarCode & "') "


                        PubDBCn.Execute(SqlStr)
                    Next
                End If

                RsTemp.MoveNext()
            Loop
            PubDBCn.CommitTrans()
        Else
            'MsgInformation("Nothing to Print.")
            Exit Sub
        End If

        mRptFileName = PubReportFolderPath & mRptFileName
        CrReport.Load(mRptFileName)
        Call Connect_MainReport_To_Database_11(CrReport)

        CrReport.RecordSelectionFormula = "{TEMP_PrintDummyData.UserID} = '" & MainClass.AllowSingleQuote(PubUserID) & "'"

        ClearCRpt11Formulas(CrReport)
        CrReport.ReportOptions.EnableSaveDataWithReport = False
        SetCompanyReport11(CrReport, 1, mTitle, mSubTitle)
        'CrReport.VerifyDatabase()   .VerifyOnEveryPrint = True '' blnVerifyOnEveryPrint		
        CrReport.Refresh()


        If Mode = Crystal.DestinationConstants.crptToWindow Then
            FrmInvoiceViewer.CrystalReportViewer1.ReportSource = Nothing
            'FrmInvoiceViewer.CrystalReportViewer1.DataBindings()
            FrmInvoiceViewer.CrystalReportViewer1.ReportSource = CrReport
            FrmInvoiceViewer.CrystalReportViewer1.Show()
            FrmInvoiceViewer.MdiParent = Me.MdiParent
            FrmInvoiceViewer.CrystalReportViewer1.ShowGroupTreeButton = False
            FrmInvoiceViewer.CrystalReportViewer1.DisplayGroupTree = False
            FrmInvoiceViewer.Dock = DockStyle.Fill
            FrmInvoiceViewer.Show()
        Else
            Dim settings As PrinterSettings = New PrinterSettings()
            For Each printer As String In PrinterSettings.InstalledPrinters

                If settings.IsDefaultPrinter Then
                    settings.PrinterName = printer
                    Exit For
                End If
            Next

            CrReport.PrintToPrinter(1, False, 1, 99)
            CrReport.Dispose()
        End If


        'SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, True, "")

        'Report1.Destination = Mode
        'Report1.DiscardSavedData = True
        'MainClass.ReportWindow(Report1, mTitle)
        'Report1.Connect = STRRptConn


        'Report1.ReportFileName = PubReportFolderPath & mRptFileName

        'Report1.SQLQuery = "SELECT * FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' ORDER BY FIELD1,FIELD2,FIELD5,SUBROW"
        'Report1.WindowShowGroupTree = False

        'Report1.Action = 1
        'Report1.Reset()

        'Call ShowPackingReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        CrReport.Dispose()
        PubDBCn.RollbackTrans()
    End Sub
    Private Sub cmdGenerateEWayBill_Click(sender As Object, e As EventArgs) Handles cmdGenerateEWayBill.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String
        Dim mInvoiceNo As String
        Dim mInvoiceDate As String
        Dim mIRNNo As String
        Dim meInvoiceApp As String
        Dim mInvoiceSeq As Long
        Dim mUpdateCount As Integer
        Dim mMKey As String
        Dim mCustomerName As String
        Dim mValue As String
        Dim mLocation As String
        Dim pPINNo As String

        If chkDebitNote.Checked = True Or chkCreditNote.Checked = True Or chkNonGSTCreditNote.Checked = True Then
            Exit Sub
        End If

        meInvoiceApp = IIf(IsDBNull(RsCompany.Fields("EWAYBILLAPP").Value), "N", RsCompany.Fields("EWAYBILLAPP").Value)
        If meInvoiceApp = "N" Then Exit Sub


        'PubDBCn.Errors.Clear()
        'PubDBCn.BeginTrans()
        mUpdateCount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColFlag
                If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then
                    .Col = ColMKey
                    mMKey = Trim(.Text)

                    .Col = ColInvoiceSeq
                    mInvoiceSeq = Val(.Text)

                    .Col = ColInvoiceNo
                    mInvoiceNo = Trim(.Text)

                    .Col = CoInvoiceDate
                    mInvoiceDate = Trim(.Text)

                    .Col = ColCustomerName
                    mCustomerName = Trim(.Text)

                    .Col = ColLocation
                    mLocation = Trim(.Text)

                    If MainClass.ValidateWithMasterTable(mCustomerName, "SUPP_CUST_NAME", "SUPP_CUST_PIN", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LOCATION_ID='" & mLocation & "'") = True Then
                        pPINNo = MasterNo
                        If Len(pPINNo) = 6 And Val(pPINNo) > 0 Then
                            .Col = ColIRNNo
                            mIRNNo = Trim(.Text)
                            'If mIRNNo = "" Then
                            mValue = WebRequestCreateEWayBill(mMKey, mInvoiceSeq, mCustomerName, mIRNNo)
                            .Col = ColEWayNo
                            .Text = mValue
                            'Else
                            'If WebRequestEWayBillByIRN(mMKey, mIRNNo, mInvoiceSeq) = False Then Exit Sub
                            'End If
                        Else
                            MsgInformation("Invalid PIN Code (" & pPINNo & ") for Customer Name : " & mCustomerName)
                        End If
                    End If
                End If
NextRowNo:

            Next
        End With
        'PubDBCn.CommitTrans()

        'MsgBox("Total " & mUpdateCount & " Invoice Generated.", MsgBoxStyle.Information)
        Exit Sub
ErrPart:
        ''Resume
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
        'PubDBCn.RollbackTrans()
    End Sub
    Private Sub txtVehicle_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.DoubleClick
        SearchVehicle()
    End Sub
    Private Sub SearchVehicle()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtVehicle.Text, "FIN_VEHICLE_MST", "NAME", , , , SqlStr)
        If AcName <> "" Then
            txtVehicle.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtVehicle_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicle.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicle.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVehicle_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicle.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVehicle()
    End Sub
    Private Sub txtVehicle_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVehicle.Validating
        '        Dim Cancel As Boolean = eventArgs.Cancel
        '        On Error GoTo ERR1
        '        Dim SqlStr As String

        '        If txtVehicle.Text = "" Then GoTo EventExitSub

        '        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '        If MainClass.ValidateWithMasterTable((txtVehicle.Text), "NAME", "NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
        '            txtVehicle.Text = UCase(Trim(txtVehicle.Text))
        '        Else
        '            MsgInformation("No Such Vechicle in Vechicle Master")
        '            Cancel = True
        '        End If
        '        GoTo EventExitSub
        'ERR1:
        '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'EventExitSub:
        '        eventArgs.Cancel = Cancel
    End Sub
    Private Sub chkAllVehicle_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllVehicle.CheckStateChanged
        If chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtVehicle.Enabled = False
            cmdSearchVehicle.Enabled = False
        Else
            txtVehicle.Enabled = True
            cmdSearchVehicle.Enabled = True
        End If
        cmdShow.Enabled = True

        CmdSave.Enabled = False
        cmdGenerateEWayBill.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
        cmdeMail.Enabled = False
    End Sub

    Private Sub cmdsearchVehicle_Click(sender As Object, e As EventArgs) Handles cmdSearchVehicle.Click
        SearchVehicle()
    End Sub

    Private Sub txtVehicle_TextChanged(sender As Object, e As EventArgs) Handles txtVehicle.TextChanged
        cmdShow.Enabled = True
        CmdSave.Enabled = False
        cmdGenerateEWayBill.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
        cmdeMail.Enabled = False
    End Sub

    Private Sub txtBillFrom_TextChanged(sender As Object, e As EventArgs) Handles txtBillFrom.TextChanged
        cmdShow.Enabled = True
        CmdSave.Enabled = False
        cmdGenerateEWayBill.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
        cmdeMail.Enabled = False
    End Sub

    Private Sub txtBillTo_TextChanged(sender As Object, e As EventArgs) Handles txtBillTo.TextChanged
        cmdShow.Enabled = True
        CmdSave.Enabled = False
        cmdGenerateEWayBill.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
        cmdeMail.Enabled = False
    End Sub

    Private Sub cmdConsolidatedEWayBill_Click(sender As Object, e As EventArgs) Handles cmdConsolidatedEWayBill.Click
        ''Public Function WebRequestEWayBillByIRN(ByRef pMKey As String, ByRef pIRNNo As String, pInvoiceSeqType As Long) As Boolean
        On Error GoTo ErrPart
        Dim url As String

        Dim mUserName As String
        Dim mPassword As String

        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mBody As String
        Dim mResponseId As String
        Dim mResponseIdStr As String
        Dim url1 As String
        Dim WebRequestGen As String
        Dim pStaus As String

        Dim pError As String
        Dim mBMPFileName As String

        Dim pResponseText As String

        Dim mCDKey As String
        Dim mEFUserName As String
        Dim mEFPassword As String
        Dim mEWBUserName As String
        Dim mEWBPassword As String
        Dim mIsTesting As String
        Dim pGSTIN As String
        Dim mStateCode As String

        Dim mUpdateStart As Boolean = False

        If chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Checked Or Trim(txtVehicle.Text) = "" Then
            MsgInformation("Please Select The Vehicle No.")
            Exit Sub
        End If

        'pCDKey = "1000687"
        'pEFUserName = "29AAACW3775F000"
        'pEFPassword = "Admin!23.."
        'pEWBUserName = "29AAACW3775F000"
        'pEWBPassword = "Admin!23.."

        If GetWebTeleWaySetupContents(url, "CON", mCDKey, mEFUserName, mEFPassword, mEWBUserName, mEWBPassword, mIsTesting) = False Then GoTo ErrPart

        If mIsTesting = "Y" Then
            url = "http://ip.webtel.in/eWayGSP2/Sandbox/EWayBill/v1.3/GenEWB"
            mCDKey = "1000687"
            mEFUserName = "29AAACW3775F000"
            mEFPassword = "Admin!23.."
            mEWBUserName = "29AAACW3775F000"
            mEWBPassword = "Admin!23.."
            pGSTIN = "29AAACW3775F000" ' IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) ''"05AAAAU3306Q1ZC" ''
        Else
            pGSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) ''"05AAAAU3306Q1ZC" ''
        End If

        Dim http As Object   '' Dim http As MSXML2.XMLHTTP60 '' MSXML.xmlhttp							
        http = CreateObject("MSXML2.ServerXMLHTTP")


        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")

        'Dim details As New List(Of CONSOLIDATIONEWAYBILLBYIRN)()

        Dim pEWBNo As Long
        Dim pSupPlace As String
        Dim pSupState As String
        Dim pTransdocno As String
        Dim pTransDocDate As String
        Dim pTransMode As String
        Dim pConsolidationEWayNo As Long
        Dim mInvoiceSeq As String
        Dim pMKey As String
        Dim SqlStr As String
        Dim RsTempDet As ADODB.Recordset = Nothing
        Dim pTransModeStr As String

        mBody = "{""Push_Data_List"":["

        For CntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = CntRow
            SprdMain.Col = ColFlag
            If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then

                SprdMain.Col = ColMKey
                pMKey = Trim(SprdMain.Text)

                SprdMain.Col = ColEWayNo

                pEWBNo = Val(SprdMain.Text)

                SprdMain.Col = ColConsolidationEWayNo
                pConsolidationEWayNo = Val(SprdMain.Text)

                SprdMain.Col = ColInvoiceSeq
                mInvoiceSeq = Val(SprdMain.Text)

                SprdMain.Col = ColInvoiceNo
                pTransdocno = Trim(SprdMain.Text)

                SprdMain.Col = CoInvoiceDate
                pTransDocDate = Trim(SprdMain.Text)

                pSupPlace = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
                pSupState = IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
                mStateCode = GetStateCode(pSupState)

                If lblBookType.Text = "IEG" Or lblBookType.Text = "IIG" Then
                    SqlStr = " SELECT IH.* " & vbCrLf _
                    & " FROM FIN_INVOICE_HDR IH" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And IH.MKEY='" & pMKey & "'"
                ElseIf lblBookType.Text = "REG" Then
                    SqlStr = " SELECT IH.* " & vbCrLf _
                    & " FROM INV_GATEPASS_HDR IH" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And IH.AUTO_KEY_PASSNO='" & pMKey & "'"
                Else
                    SqlStr = " SELECT IH.* " & vbCrLf _
                    & " FROM DSP_DESPATCH_HDR IH" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And IH.AUTO_KEY_DESP='" & pMKey & "'"
                End If

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

                pTransMode = 1
                If RsTempDet.EOF = False Then
                    pTransModeStr = IIf(IsDBNull(RsTempDet.Fields("TRANSPORT_MODE").Value), "1", RsTempDet.Fields("TRANSPORT_MODE").Value)
                    pTransModeStr = IIf(pTransModeStr = "", "1", pTransModeStr)
                    pTransMode = VB.Left(pTransModeStr, 1)       'VB.Left(cboTransmode.Text, 1)
                End If

                If pEWBNo > 0 And pConsolidationEWayNo = 0 Then
                    mBody = mBody & "{"
                    mBody = mBody & """GSTIN"":""" & pGSTIN & ""","
                    mBody = mBody & """EWBNumber"":""" & pEWBNo & ""","
                    mBody = mBody & """VehicleNumber"":""" & txtVehicle.Text & ""","
                    mBody = mBody & """SupPlace"":""" & pSupPlace & ""","
                    mBody = mBody & """SupState"":""" & mStateCode & ""","
                    mBody = mBody & """Transdocno"":""" & pTransdocno & ""","
                    mBody = mBody & """TransDocDate"":""" & VB6.Format(pTransDocDate, "YYYYMMDD") & ""","
                    mBody = mBody & """TransMode"":""" & pTransMode & ""","
                    mBody = mBody & """EWBUserName"":""" & mEWBUserName & ""","
                    mBody = mBody & """EWBPassword"":""" & mEWBPassword & """"
                End If

                If CntRow = SprdMain.MaxRows Then
                    mBody = mBody & "}"
                Else
                    mBody = mBody & "},"
                End If
            End If

        Next


        mBody = mBody & "],"
        mBody = mBody & """Year"":""" & Year(CDate(pTransDocDate)) & ""","
        mBody = mBody & """Month"":""" & Month(CDate(pTransDocDate)) & ""","
        mBody = mBody & """EFUserName"":""" & mEFUserName & ""","
        mBody = mBody & """EFPassword"":""" & mEFPassword & ""","
        mBody = mBody & """CDKey"":""" & mCDKey & """"

        mBody = mBody & "}"


        http.Send(mBody)

        pResponseText = http.responseText

        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        pResponseText = Replace(pResponseText, "\", "")
        pResponseText = Replace(pResponseText, """", "'")
        pResponseText = Replace(pResponseText, "'{", "{")
        pResponseText = Replace(pResponseText, "}'", "}")


        Dim meWayResponseID As String
        Dim meWayResponseDate As String

        Dim post As Object
        pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .IsSuccess = ""})).IsSuccess  '\'IsSuccess

        If UCase(pStaus) = "TRUE" Then
            meWayResponseID = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .EWayBill = ""})).EWayBill   'JsonTest.Item("Irn")
            meWayResponseDate = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Date = ""})).Date   'JsonTest.Item("Irn")
            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()
            mUpdateStart = True

            For CntRow = 1 To SprdMain.MaxRows
                SprdMain.Row = CntRow
                SprdMain.Col = ColFlag
                If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then
                    SprdMain.Col = ColEWayNo
                    SprdMain.Row = CntRow

                    SprdMain.Col = ColMKey
                    pMKey = Trim(SprdMain.Text)

                    SprdMain.Col = ColConsolidationEWayNo
                    SprdMain.Text = meWayResponseID

                    If lblBookType.Text = "IEG" Or lblBookType.Text = "IIG" Then
                        SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
                                    & " CONSOLIDATION_E_BILLWAYNO ='" & Val(meWayResponseID) & "'," & vbCrLf _
                                    & " CONSOLIDATION_E_BILLWAYDATE =TO_DATE('" & VB6.Format(meWayResponseDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                    & " AND MKEY ='" & pMKey & "'"
                    ElseIf lblBookType.Text = "REG" Then
                        SqlStr = "UPDATE INV_GATEPASS_HDR SET " & vbCrLf _
                                    & " CONSOLIDATION_E_BILLWAYNO ='" & Val(meWayResponseID) & "'" & vbCrLf _
                                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                    & " AND AUTO_KEY_PASSNO ='" & pMKey & "'"
                    Else
                        SqlStr = "UPDATE DSP_DESPATCH_HDR SET " & vbCrLf _
                                    & " CONSOLIDATION_E_BILLWAYNO ='" & Val(meWayResponseID) & "'" & vbCrLf _
                                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                    & " AND AUTO_KEY_DESP ='" & pMKey & "'"
                    End If
                    PubDBCn.Execute(SqlStr)
                End If
            Next


            PubDBCn.CommitTrans()
            'WebRequestCreateEWayBill = meWayResponseID
        End If

        mUpdateStart = False
        If UCase(pStaus) = "FALSE" Then
            pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            MsgInformation(pError)
            'WebRequestCreateEWayBill = pError
            http = Nothing
            Exit Sub
        End If
ErrPart:
        '    Resume							
        'http = Nothing							
        MsgBox(Err.Description)
        If mUpdateStart = True Then
            PubDBCn.RollbackTrans()
        End If
    End Sub

    Private Sub frmMultiInvoicePrinting_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        'UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_ButtonClicked(sender As Object, e As _DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        Try
            Dim pEWayBillNo As String
            Dim pIRNNo As String
            Dim pBillDate As String
            Dim mMKey As String = ""

            SprdMain.Row = e.row

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Select Case e.col
                Case ColIRNPrint
                    SprdMain.Col = ColIRNNo
                    pIRNNo = Trim(SprdMain.Text)
                    Call PrinteInvoice(pIRNNo)

                Case ColEWayPrint
                    SprdMain.Col = ColEWayNo
                    pEWayBillNo = Trim(SprdMain.Text)

                    SprdMain.Col = ColEWayDate
                    pBillDate = VB6.Format(SprdMain.Text, "DD/MM/YYYY")

                    Call eWayBillPrint(pEWayBillNo, pBillDate)

                Case ColConsolidationEWayPrint
                    SprdMain.Col = ColConsolidationEWayNo
                    pEWayBillNo = Trim(SprdMain.Text)

                    SprdMain.Col = ColEWayDate
                    pBillDate = VB6.Format(SprdMain.Text, "DD/MM/YYYY")

                    If pEWayBillNo <> "" Then
                        Call ConsolidationEWayPrintReport(pEWayBillNo)
                    End If
                    'Call ConsolidationEWayPrint(pEWayBillNo, pBillDate)
            End Select



            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ConsolidationEWayPrint(ByRef pEWayBillNo As String, ByRef pBillDate As String)
        On Error GoTo ErrPart
        Dim mFilePath As String
        Dim pCDKey As String
        Dim pEFUserName As String
        Dim pEFPassword As String
        Dim pEWBUserName As String
        Dim pEWBPassword As String
        Dim url As String
        Dim pResponseIdText As String
        Dim mBody As String
        Dim pStatus As String

        If Trim(pEWayBillNo) = "" Then
            MsgInformation("Nothing to print.")
            Exit Sub
        End If


        If GetWebTeleWaySetupContents(url, "CONP", pCDKey, pEFUserName, pEFPassword, pEWBUserName, pEWBPassword, "N") = False Then GoTo ErrPart

        Dim http As Object  ' MSXML2.XMLHTTP60 '' MSXML.xmlhttp
        http = CreateObject("MSXML2.ServerXMLHTTP")
        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")

        Dim details As New List(Of EWAYBILLCONSOLIDATIONPRN)()

        details.Add(New EWAYBILLCONSOLIDATIONPRN() With {
            .GSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value),
            .tripSheetNo = Trim(pEWayBillNo),
            .EWBUserName = pEWBUserName,
            .EWBPassword = pEWBPassword,
            .Year = Year(pBillDate),
            .Month = Month(pBillDate),
            .EFUserName = pEFUserName,
            .EFPassword = pEFPassword,
            .CDKey = pCDKey
         })

        Dim mBodyDetail As String = JsonConvert.SerializeObject(details)

        mBody = mBody & mBodyDetail
        mBody = Replace(mBody, "[", "")
        mBody = Replace(mBody, "]", "")

        http.Send(mBody)

        Dim pResponseText As String = http.responseText
        If pResponseText <> "" Then
            Process.Start("explorer.exe", pResponseText)
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
    End Sub
    Private Sub PrinteInvoice(ByRef pIRNNo As String)
        On Error GoTo ErrPart
        Dim url As String

        Dim mGSTIN As String
        Dim mIrn As String

        Dim mGetQRImg As String
        Dim mGetSignedInvoice As String
        Dim mCDKey As String
        Dim mEInvUserName As String
        Dim mEInvPassword As String
        Dim mEFUserName As String
        Dim mEFPassword As String

        Dim mBody As String
        Dim mResponseId As String
        Dim mResponseIdStr As String
        Dim url1 As String
        Dim WebRequestGen As String
        Dim pStaus As String

        Dim mIRNNo As String
        Dim mSignedInvoice As String
        Dim mSignedQRCode As String

        Dim pError As String

        Dim mBMPFileName As String
        Dim mFilePath As String
        Dim pIsTesting As String = "Y"
        Dim pResponseText As String

        If Trim(pIRNNo) = "" Then Exit Sub

        If GeteInvoiceSetupContents(url, "P", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword, pIsTesting) = False Then GoTo ErrPart

        If pIsTesting = "Y" Then
            url = "http://einvsandbox.webtel.in/v1.03/GenIRN"
            mCDKey = "1000687"
            mEInvUserName = "03AAACW3775F010"       ''"06AAACW3775F013"		 "29AAACW3775F000" '' 					
            mEInvPassword = "Admin!23"  ''"Admin!23.."    ''
            mEFUserName = "29AAACW3775F000"  '' "29AAACW3775F000"
            mEFPassword = "Admin!23.."
            mGSTIN = "03AAACW3775F010" ''IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        Else
            mGSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        End If

        Dim http As Object 'MSXML2.XMLHTTP60 '' MSXML.xmlhttp							
        http = CreateObject("MSXML2.ServerXMLHTTP")


        mIRNNo = Trim(pIRNNo)

        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")
        mBody = ""

        mBody = mBody & "{"

        mBody = mBody & """Irn"":""" & mIRNNo & ""","
        mBody = mBody & """GSTIN"":""" & mGSTIN & ""","
        mBody = mBody & """CDKey"":""" & mCDKey & ""","
        mBody = mBody & """EInvUserName"":""" & mEInvUserName & ""","
        mBody = mBody & """EInvPassword"":""" & mEInvPassword & ""","
        mBody = mBody & """EFUserName"":""" & mEFUserName & ""","
        mBody = mBody & """EFPassword"":""" & mEFPassword & """"

        mBody = mBody & "}"

        http.Send(mBody)

        pResponseText = http.responseText

        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        pResponseText = Replace(pResponseText, """", "'")

        Dim post As Object
        pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Status = ""})).Status

        If pStaus = "1" Then

            mFilePath = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .File = ""})).File ' JsonTest.Item("File") ''http.responseText						

            If mFilePath <> "" Then
                Process.Start("explorer.exe", mFilePath)
            End If

        End If

        If pStaus = "0" Then
            pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("File") '' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            MsgInformation(pError)
            http = Nothing
            Exit Sub
        End If

        http = Nothing

        Exit Sub
ErrPart:

        http = Nothing
        MsgBox(Err.Description)

    End Sub



    Private Sub eWayBillPrint(ByRef pEWayBillNo As String, ByRef pBillDate As String)
        On Error GoTo ErrPart
        Dim mFilePath As String
        Dim pCDKey As String
        Dim pEFUserName As String
        Dim pEFPassword As String
        Dim pEWBUserName As String
        Dim pEWBPassword As String
        Dim url As String
        Dim pResponseIdText As String
        Dim mBody As String
        Dim pStatus As String

        If Trim(pEWayBillNo) = "" Then
            MsgInformation("Nothing to print.")
            Exit Sub
        End If


        If GetWebTeleWaySetupContents(url, "P", pCDKey, pEFUserName, pEFPassword, pEWBUserName, pEWBPassword, "N") = False Then GoTo ErrPart

        Dim http As Object  ' MSXML2.XMLHTTP60 '' MSXML.xmlhttp
        http = CreateObject("MSXML2.ServerXMLHTTP")
        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")

        Dim details As New List(Of EWAYBILLPRN)()

        details.Add(New EWAYBILLPRN() With {
            .GSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value),
            .ewbNo = Trim(pEWayBillNo),
            .Year = Year(pBillDate),
            .Month = Month(pBillDate),
            .EFUserName = pEFUserName,
            .EFPassword = pEFPassword,
            .CDKey = pCDKey,
            .EWBUserName = pEWBUserName,
            .EWBPassword = pEWBPassword
         })

        Dim mBodyDetail As String = JsonConvert.SerializeObject(details)

        mBody = mBody & mBodyDetail
        mBody = Replace(mBody, "[", "")
        mBody = Replace(mBody, "]", "")

        http.Send(mBody)

        Dim pResponseText As String = http.responseText
        If pResponseText <> "" Then
            Process.Start("explorer.exe", pResponseText)
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdeMail_Click(sender As Object, e As EventArgs) Handles cmdeMail.Click
        On Error GoTo ErrPart
        Dim mInvoicePrint As Boolean
        Dim mAnnexPrint As String
        Dim mSubsidiaryChallanPrint As String
        Dim mEXPAnnexPrint As String
        Dim mMaxRow As Integer
        Dim mSC_All As String
        Dim mSC_F4No As String
        Dim CntCount As Integer
        Dim mInvoicePrintType As String = ""
        Dim mExtraRemarks As String
        Dim mPrintOption As String = ""
        Dim mMKey As String
        Dim mPrintA4 As String
        Dim mPaperStyle As String
        Dim mPrintPaperSize As String

        frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(4).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(3).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).Enabled = False
        frmPrintInvCopy._optShow_0.Text = "Print"
        frmPrintInvCopy._optShow_0.Enabled = True
        frmPrintInvCopy._optShow_0.Checked = True
        frmPrintInvCopy._optShow_1.Enabled = False
        frmPrintInvCopy._optShow_2.Enabled = True

        frmPrintInvCopy.optPrintPortrait.Checked = IIf(RsCompany.Fields("INVOICE_PRINT_STYLE").Value = "P", True, False)
        frmPrintInvCopy.optPrintLandScape.Checked = IIf(RsCompany.Fields("INVOICE_PRINT_STYLE").Value = "P", False, True)

        mPrintA4 = IIf(IsDBNull(RsCompany.Fields("INVOICE_A4").Value), "Y", RsCompany.Fields("INVOICE_A4").Value)
        frmPrintInvCopy.optA4.Checked = IIf(mPrintA4 = "Y", True, False)
        frmPrintInvCopy.optA3.Checked = IIf(mPrintA4 = "Y", False, True)

        frmPrintInvCopy.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        If frmPrintInvCopy.optShow(3).Checked = True Or frmPrintInvCopy.optShow(4).Checked = True Then
            With SprdMain
                For CntRow = 1 To .MaxRows
                    .Row = CntRow
                    .Col = ColFlag
                    If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then
                        .Col = ColMKey
                        mMKey = Trim(.Text)
                        Call ReportOnPackingSlip(Crystal.DestinationConstants.crptToWindow, mMKey, IIf(frmPrintInvCopy.optShow(3).Checked = True, "I", "O"))
                    End If
                Next
            End With
            frmPrintInvCopy.Dispose()
            frmPrintInvCopy.Close()
            Exit Sub
        End If

        mPaperStyle = IIf(frmPrintInvCopy.optPrintPortrait.Checked, "P", "L")
        mPrintPaperSize = IIf(frmPrintInvCopy.optA4.Checked, "Y", "N")

        Dim mPrePrint As String = "N"
        If mPrintPaperSize = "N" Then
            mPrePrint = IIf(frmPrintInvCopy.chkPrePrint.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        End If

        If frmPrintInvCopy._optShow_0.Checked = True Then
            mPrintOption = "PDF"
        Else
            mPrintOption = "PDFSP"
        End If

        If chkCreditNote.Checked = True Or chkDebitNote.Checked = True Or chkNonGSTCreditNote.Checked = True Then
            Call ReportOnCreditNote(mPrintOption, mPaperStyle, mPrintPaperSize, mPrePrint, IIf(chkCreditNote.Checked = True, "Y", "N"), "Y")
        Else
            Call ReportOnSales(mPrintOption, mPaperStyle, mPrintPaperSize, mPrePrint, "Y")
        End If


        frmPrintInvCopy.Close()
        frmPrintInvCopy.Dispose()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ErrPart:
        frmPrintInvCopy.Close()
        frmPrintInvCopy.Dispose()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ConsolidationEWayPrintReport(ByRef pConsolidationEWayBillNo As String)

        On Error GoTo ErrPart
        Dim RsTemp As New ADODB.Recordset
        Dim RS As New ADODB.Recordset
        Dim mTitle, mSubTitle As String

        Dim fPath As String
        Dim efPath As String

        Dim SqlStr As String = ""
        Dim mRptFileName As String = ""
        Dim path As String

        Dim CrReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument  ' Report Name 
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions

        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions


        mRptFileName = PubReportFolderPath & "ConsolidationEWayBill.rpt"
        CrReport.Load(mRptFileName)

        Call Connect_MainReport_To_Database_11(CrReport)        '', mSqlStr


        CrReport.RecordSelectionFormula = "{IH.COMPANY_CODE} = " & RsCompany.Fields("COMPANY_CODE").Value & " AND {IH.CONSOLIDATION_E_BILLWAYNO} = '" & MainClass.AllowSingleQuote(pConsolidationEWayBillNo) & "' AND {IH.FYEAR} = '" & RsCompany.Fields("FYEAR").Value & "'"

        ClearCRpt11Formulas(CrReport)
        CrReport.ReportOptions.EnableSaveDataWithReport = False
        SetCompanyReport11(CrReport, 1, mTitle, mSubTitle)
        'CrReport.VerifyDatabase()   .VerifyOnEveryPrint = True '' blnVerifyOnEveryPrint		
        CrReport.Refresh()


        'AssignCRpt11Formulas(CrReport, "InvoicePrintType", "'" & mInvoicePrintType & "'")
        'AssignCRpt11Formulas(CrReport, "CompanyCity", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & "'")
        Dim mFromEWayBill As String
        ' Dim mFromEWayBill As String

        mFromEWayBill = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        mFromEWayBill = mFromEWayBill & "-" & IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)

        AssignCRpt11Formulas(CrReport, "FromEWayBill", "'" & mFromEWayBill & "'")

        AssignCRpt11Formulas(CrReport, "CompanyGSTIN", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & "'")

        Dim pOutPutFileName As String = ""
        fPath = mPubBarCodePath & "\ConsolidationEway_" & pConsolidationEWayBillNo & ".pdf"
        efPath = fPath

        ''temp check
        'FrmInvoiceViewer.CrystalReportViewer1.ReportSource = Nothing
        'FrmInvoiceViewer.CrystalReportViewer1.ReportSource = CrReport
        'FrmInvoiceViewer.CrystalReportViewer1.Show()

        CrDiskFileDestinationOptions.DiskFileName = fPath
        CrExportOptions = CrReport.ExportOptions

        With CrExportOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
            .DestinationOptions = CrDiskFileDestinationOptions
            .FormatOptions = CrFormatTypeOptions
        End With
        CrReport.Export()

        'If FILEExists(fPath) Then
        '    Process.Start("explorer.exe", fPath)
        'End If



        'FrmInvoiceViewer.Close()
        'FrmInvoiceViewer.Dispose()
        CrReport.Close()
        CrReport.Dispose()
        Exit Sub
ErrPart:
        'Resume							
        MsgBox(Err.Description)
    End Sub

    Private Sub chkCreditNote_CheckStateChanged(sender As Object, e As EventArgs) Handles chkCreditNote.CheckStateChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub chkDebitNote_CheckStateChanged(sender As Object, e As EventArgs) Handles chkDebitNote.CheckStateChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub chkServiceInvoiceOnly_CheckStateChanged(sender As Object, e As EventArgs) Handles chkServiceInvoiceOnly.CheckStateChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub chkNonGSTCreditNote_CheckStateChanged(sender As Object, e As EventArgs) Handles chkNonGSTCreditNote.CheckStateChanged
        cmdShow.Enabled = True
    End Sub
End Class
