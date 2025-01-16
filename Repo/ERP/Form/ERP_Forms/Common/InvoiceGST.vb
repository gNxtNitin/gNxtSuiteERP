Option Strict Off									
Option Explicit On									
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
''Imports Newtonsoft.Json
Imports System.Xml
'Imports System.Web.Script.Serialization
Imports System.Xml.Linq

Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports QRCoder
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Color
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Drawing.Printing

Imports System.Data
Imports System.IO
Imports System.Configuration





'Imports System.IO
'Imports System.Drawing
'Imports MessagingToolkit.QRCode.Codec
'Imports System.IO
'Imports System.IO.FileInfo


Friend Class FrmInvoiceGST
    Inherits System.Windows.Forms.Form
    'Private Enum TerrorCorretion
    '    QualityLow
    '    QualityMedium
    '    QualityStandard
    '    QualityHigh
    'End Enum

    Public Class IRNQRData
        Public Property Irn As String
        Public Property GSTIN As String
        Public Property CDKey As String
        Public Property EInvUserName As String
        Public Property EInvPassword As String
        Public Property EFUserName As String
        Public Property EFPassword As String
    End Class

    Public Class WebSignData
        Public Property PDFByte As Byte
        Public Property AuthorizeSignatory As String
        Public Property SignerName As String
        Public Property TopLeft As Integer
        Public Property BottemLeft As Integer
        Public Property TopRight As Integer
        Public Property BottomRight As Integer
        Public Property ExcludePageNumber As String

        Public Property InvoiceNumber As String
        Public Property PageNo As Integer
        Public Property PrintDate As String
        Public Property FindAuth As String
        Public Property FindAuthLocation As Integer
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
    Dim AccessCnn As New ADODB.Connection

    'Private WithEvents rptSection As CRAXDRT.Section
    Dim mPicSectionName As String

    Dim RsSaleMain As ADODB.Recordset ''Recordset								
    Dim RsSaleDetail As ADODB.Recordset ''Recordset								
    Dim RsSaleExp As ADODB.Recordset ''Recordset								
    Dim RSSalesPrn As ADODB.Recordset ''Recordset								
    Dim RsSaleTrading As ADODB.Recordset

    ''Private PvtDBCn As ADODB.Connection								

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim PrintLine_Renamed As Integer

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String = ""
    Dim mCustomerCode As String
    Dim pRound As Double
    Dim mDNCnNO As String
    Dim mDNCnDate As String
    Dim pShowCalc As Boolean
    Dim mRMCustomer As Boolean
    Dim mUpdate As Boolean
    Private Const mBookType As String = "S"
    ''Private Const mBookSubType = "C"								




    Dim mAddUser As String
    Dim mAddDate As String
    Dim mModUser As String
    Dim mModDate As String

    Dim mBookSubType As String
    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColPartNo As Short = 2
    Private Const ColItemSNo As Short = 3
    Private Const ColItemDesc As Short = 4
    Private Const ColHSNCode As Short = 5

    Private Const ColUnit As Short = 6

    Private Const ColGlassDescription As Short = 7
    Private Const ColModel As Short = 8

    Private Const ColActualWidth As Short = 9
    Private Const ColActualHeight As Short = 10

    Private Const ColActualArea As Short = 11
    Private Const ColChargeableWidth As Short = 12
    Private Const ColChargeableHeight As Short = 13

    Private Const ColChargeableArea As Short = 14
    Private Const ColAreaRate As Short = 15
    Private Const ColQty As Short = 16
    Private Const ColMRP As Short = 17
    Private Const ColRate As Short = 18
    Private Const ColAmount As Short = 19
    Private Const ColTaxableAmount As Short = 20
    Private Const ColCGSTPer As Short = 21
    Private Const ColCGSTAmount As Short = 22
    Private Const ColSGSTPer As Short = 23
    Private Const ColSGSTAmount As Short = 24
    Private Const ColIGSTPer As Short = 25
    Private Const ColIGSTAmount As Short = 26
    Private Const ColNoOfStrip As Short = 27
    Private Const ColStripRate As Short = 28
    Private Const ColPackType As Short = 29
    Private Const ColInnerBoxQty As Short = 30
    Private Const ColInnerBoxQtyA As Short = 31
    Private Const ColInnerBoxCode As Short = 32
    Private Const ColOuterBoxQty As Short = 33
    Private Const ColOuterBoxQtyA As Short = 34
    Private Const ColOuterBoxCode As Short = 35
    Private Const ColJITCallNo As Short = 36
    Private Const ColAddItemDesc As Short = 37
    Private Const ColMRRNo As Short = 38
    Private Const ColODNo As Short = 39
    Private Const ColHeatNo As Short = 40
    Private Const ColBatchNo As Short = 41

    Private Const Col57F4 As Short = 42
    Private Const Col57F4Date As Short = 43
    Private Const ColInvoiceType As Short = 44
    Private Const ColAccountName As Short = 45

    Private Const ColRO As Short = 1
    Private Const ColExpName As Short = 2
    Private Const ColExpPercent As Short = 3
    Private Const ColExpAmt As Short = 4
    Private Const ColExpSTCode As Short = 5
    Private Const ColExpAddDeduct As Short = 6
    Private Const ColExpIdent As Short = 7
    Private Const ColTaxable As Short = 8
    Private Const ColExciseable As Short = 9
    Private Const ColExpCalcOn As Short = 10
    Private Const ColDutyForgone As Short = 11
    Dim mJWRemarks As String
    Dim mJWSTRemarks As String
    Const TabLastCol As Short = 135

    Dim mIndentificationCode As String
    Dim mExpCode As String
    Dim pMSRCost As Double
    Dim pMSPCost As Double
    Dim pFreightCost As Double
    Dim pToolAmorCost As Double
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Function GetSORate(ByRef pItemCode As String, ByRef xCustomerCode As String, ByRef pDespType As String, ByRef IsMRP As String,
                               ByRef mOldBillDate As String, ByRef pUOM As String, ByRef pCGSTPer As Double, ByRef pSGSTPer As Double, ByRef pIGSTPer As Double,
                               ByRef mInvoiceType As String, ByRef mItemPartNo As String, ByRef mHeight As Double, ByRef mWidth As Double, ByRef mModelNo As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFieldName As String
        Dim mWOPO As Boolean
        Dim mPurchaseUOM As String
        Dim mFactor As Double
        Dim mHSNCode As String
        Dim mPartyGSTNo As String
        Dim mMerchantExporter As String = "N"
        mWOPO = False
        pCGSTPer = 0
        pSGSTPer = 0
        pIGSTPer = 0
        mItemPartNo = ""

        If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER= 'EXPORTER-MERCHANT'") = True Then
            mMerchantExporter = "Y"
        End If

        If Val(lblPoNo.Text) = CDbl("-1") Or Val(lblPoNo.Text) = CDbl("0") Then
            If IsMRP = "MSP" Or IsMRP = "MSR" Or IsMRP = "FR" Or IsMRP = "TOL" Or IsMRP = "J" Then GetSORate = 0 : Exit Function
        End If

        If pDespType = "E" Then
            SqlStr = "SELECT RATE_INR AS ITEM_PRICE, 0 AS CGST_PER, 0 AS SGST_PER, 0 As IGST_PER, -1 AS ACCOUNT_POSTING_CODE,'' AS PART_NO " & vbCrLf _
                & " FROM FIN_EXPINV_HDR IH, FIN_EXPINV_DET ID" & vbCrLf _
                & " WHERE IH.AUTO_KEY_EXPINV=ID.AUTO_KEY_EXPINV " & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " --AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND IH.AUTO_KEY_PACK=" & Val(lblPoNo.Text) & ""
        Else
            If Val(lblPoNo.Text) <> CDbl("-1") And Val(lblPoNo.Text) <> CDbl("0") Then
                If IsMRP = "Y" Then
                    mFieldName = "ITEM_MRP"
                ElseIf IsMRP = "J" Then
                    mFieldName = "MATERIAL_COST"
                ElseIf IsMRP = "MSP" Then
                    mFieldName = "MSP_COST"
                ElseIf IsMRP = "MSR" Then
                    mFieldName = "MSP_COST_ADD"
                ElseIf IsMRP = "FR" Then
                    mFieldName = "FREIGHT_COST"
                ElseIf IsMRP = "TOL" Then
                    mFieldName = "TOL_AMOR_COST"
                Else
                    mFieldName = "ITEM_PRICE"
                End If
                SqlStr = "SELECT " & mFieldName & " AS ITEM_PRICE, CGST_PER, SGST_PER, IGST_PER, ACCOUNT_POSTING_CODE, PART_NO" & vbCrLf _
                    & " FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                    & " AND IH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf _
                    & " AND IH.MKEY = ("

                If pDespType = "U" Then
                    SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf & " AND SID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND SIH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf & " AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(mOldBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                Else
                    SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _
                        & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                        & " AND SID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                        & " AND SIH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf _
                        & " AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                        If xCustomerCode = "11265" Then

                        Else
                            If mModelNo = "" Then
                                SqlStr = SqlStr & " And CHARGEABLE_HEIGHT=" & mHeight & " And CHARGEABLE_WIDTH=" & mWidth & ""
                            Else
                                SqlStr = SqlStr & " And ITEM_MODEL='" & MainClass.AllowSingleQuote(mModelNo) & "'"
                            End If
                        End If

                    End If

                    SqlStr = SqlStr & ")"
                End If

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then

                    If xCustomerCode = "11265" Then

                    Else
                        If mModelNo <> "" Then
                            SqlStr = SqlStr & " And ITEM_MODEL='" & MainClass.AllowSingleQuote(mModelNo) & "'"
                        End If

                        If Val(mHeight) > 0 Then
                            SqlStr = SqlStr & " And CHARGEABLE_HEIGHT=" & mHeight & ""
                        End If

                        If Val(mWidth) > 0 Then
                            SqlStr = SqlStr & " And CHARGEABLE_WIDTH=" & mWidth & ""
                        End If
                    End If
                End If

                ''AND IH.SO_STATUS='O'					
            Else
                If IsMRP = "Y" Then
                    mFieldName = "ITEM_MRP"
                    '                ElseIf IsMRP = "J" Then				
                    '                        mFieldName = "MATERIAL_COST"				
                Else
                    mFieldName = "ITEM_RATE"
                End If
                SqlStr = "SELECT " & mFieldName & " AS ITEM_PRICE, 0 AS CGST_PER, 0 AS SGST_PER, 0 As IGST_PER, '-1' AS ACCOUNT_POSTING_CODE, CUSTOMER_ITEM_NO AS PART_NO " & vbCrLf _
                    & " FROM FIN_SUPP_CUST_DET " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                    & " AND ITEM_CODE='" & pItemCode & "'"

                mWOPO = True
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSORate = Val(IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value))
            If pDespType = "E" Then
                mHSNCode = GetHSNCode(pItemCode)
                mPartyGSTNo = ""

                mPartyGSTNo = GetPartyBusinessDetail(xCustomerCode, Trim(txtBillTo.Text), "GST_RGN_NO")

                'If MainClass.ValidateWithMasterTable(xCustomerCode, "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    mPartyGSTNo = MasterNo
                'End If

                If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, "N", "0", mPartyGSTNo,,,, mMerchantExporter) = False Then GoTo ErrPart
            Else
                pCGSTPer = Val(IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value))
                pSGSTPer = Val(IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value))
                pIGSTPer = Val(IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value))
            End If

            mItemPartNo = IIf(IsDBNull(RsTemp.Fields("PART_NO").Value), "", RsTemp.Fields("PART_NO").Value)

            If pDespType = "E" And mItemPartNo = "" Then
                'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And mItemPartNo = "" Then

                'Else
                If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "CUSTOMER_ITEM_NO", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & xCustomerCode & "'") = True Then
                    mItemPartNo = MasterNo
                End If

                If mItemPartNo = "" Then
                    If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemPartNo = MasterNo
                    End If
                End If
                'End If
            End If

            If mInvoiceType = "" Then
                mInvoiceType = IIf(IsDBNull(RsTemp.Fields("ACCOUNT_POSTING_CODE").Value), "", RsTemp.Fields("ACCOUNT_POSTING_CODE").Value)
            End If

            If mWOPO = True Then
                SqlStr = "SELECT PURCHASE_UOM,UOM_FACTOR " & vbCrLf & " FROM INV_ITEM_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & pItemCode & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    mPurchaseUOM = CStr(Val(IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)))
                    mFactor = Val(IIf(IsDBNull(RsTemp.Fields("UOM_FACTOR").Value), 0, RsTemp.Fields("UOM_FACTOR").Value))
                    If Trim(mPurchaseUOM) <> Trim(pUOM) Then
                        GetSORate = CDbl(VB6.Format(GetSORate / IIf(mFactor = 0, 1, mFactor), "0.0000"))
                    End If
                End If
            End If
        Else
            GetSORate = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetSORate = 0
    End Function
    Private Function GetBillRateDiff(ByRef pItemCode As String, ByRef xCustomerCode As String, ByRef pOldBillNo As String, ByRef mOldBillDate As String, ByRef pOldBillRate As Double, ByRef pNewSORate As Double, ByRef pDNRate As Double, ByRef pSuppBillRate As Double, ByRef pType As String, ByRef pCGSTPer As Double, ByRef pSGSTPer As Double, ByRef pIGSTPer As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        pOldBillRate = 0
        pNewSORate = 0
        pDNRate = 0
        pSuppBillRate = 0
        pCGSTPer = 0
        pSGSTPer = 0
        pIGSTPer = 0

        GetBillRateDiff = 0

        mOldBillDate = ""

        If pType = "P" Then
            SqlStr = "SELECT INVOICE_DATE, ID.ITEM_RATE  AS OLDRATE," & vbCrLf _
                & " 0 AS SORATE, " & vbCrLf _
                & " 0 AS DNRATE, " & vbCrLf _
                & " 0 AS SUPPRATE, CGST_PER, SGST_PER, IGST_PER "
        Else
            SqlStr = "SELECT INVOICE_DATE, ID.ITEM_RATE  AS OLDRATE," & vbCrLf _
                & " GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE) AS SORATE, " & vbCrLf _
                & " GETSALEDEBITRATE(IH.COMPANY_CODE,IH.FYEAR,IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE) AS DNRATE, " & vbCrLf _
                & " GETSALESUPPBILLPRICE(IH.COMPANY_CODE, ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE) AS SUPPRATE, CGST_PER, SGST_PER, IGST_PER "

        End If

        SqlStr = SqlStr & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID " & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.AUTO_KEY_INVOICE='" & pOldBillNo & "'"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N' AND IH.REF_DESP_TYPE<>'U'" '' Dated 30/11/2017 AND AGTD3='N'"							

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pOldBillRate = Val(IIf(IsDBNull(RsTemp.Fields("OLDRATE").Value), 0, RsTemp.Fields("OLDRATE").Value))
            pNewSORate = Val(IIf(IsDBNull(RsTemp.Fields("SORATE").Value), 0, RsTemp.Fields("SORATE").Value))
            pDNRate = Val(IIf(IsDBNull(RsTemp.Fields("DNRATE").Value), 0, RsTemp.Fields("DNRATE").Value))
            pSuppBillRate = Val(IIf(IsDBNull(RsTemp.Fields("SUPPRATE").Value), 0, RsTemp.Fields("SUPPRATE").Value))
            pCGSTPer = Val(IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value))
            pSGSTPer = Val(IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value))
            pIGSTPer = Val(IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value))

            GetBillRateDiff = CDbl(VB6.Format(pNewSORate + pDNRate - pOldBillRate - pSuppBillRate, "0.00"))

            mOldBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
        End If


        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetBillRateDiff = 0
    End Function


    Private Function GetDRRate(ByRef pItemCode As String, ByRef xCustomerCode As String, ByRef pCGSTPer As Double, ByRef pSGSTPer As Double, ByRef pIGSTPer As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mInterUnit As String = "N"

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        GetDRRate = 0
        pCGSTPer = 0
        pSGSTPer = 0
        pIGSTPer = 0


        'SqlStr = "SELECT DISTINCT IH.AUTO_KEY_PO, IH.MKEY, IH.AMEND_NO, IH.PUR_ORD_DATE" & vbCrLf _
        '            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID,INV_ITEM_MST INVMST " & vbCrLf _
        '            & " WHERE IH.MKEY=ID.MKEY AND ID.Company_Code=INVMST.COMPANY_CODE AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
        '            & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        '            & " AND IH.PUR_TYPE='P' AND PO_STATUS ='Y' AND PO_CLOSED='N'"



        If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
            SqlStr = "SELECT ITEM_PRICE AS ITEM_PRICE, CGST_PER, SGST_PER, IGST_PER " & vbCrLf _
                   & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID " & vbCrLf _
                   & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                   & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                   & " AND IH.MKEY = ("

            SqlStr = SqlStr & vbCrLf & "SELECT MAX(A.MKEY) " & vbCrLf _
                   & " FROM PUR_PURCHASE_HDR A, PUR_PURCHASE_DET B " & vbCrLf _
                   & " WHERE A.MKEY=B.MKEY" & vbCrLf _
                   & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                   & " AND A.AUTO_KEY_PO='" & lblPoNo.Text & "' " & vbCrLf _
                   & " AND PUR_ORD_DATE<=TO_DATE('" & VB6.Format(txtDCDate.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'))"


        Else
            SqlStr = "SELECT ITEM_RATE AS ITEM_PRICE, CGST_PER, SGST_PER, IGST_PER " & vbCrLf _
                    & " FROM FIN_DNCN_DET" & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                    & " AND MKEY='" & lblPoNo.Text & "'"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetDRRate = Val(IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value))
            pCGSTPer = Val(IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value))
            pSGSTPer = Val(IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value))
            pIGSTPer = Val(IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value))
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetDRRate = 0
    End Function
    Private Function GetPORate(ByRef pItemCode As String, ByRef xCustomerCode As String, ByRef xMRRNo As Double, ByRef xMRRDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = "SELECT ITEM_RATE " & vbCrLf & " FROM INV_GATE_HDR,INV_GATE_DET " & vbCrLf & " WHERE " & vbCrLf & " INV_GATE_HDR.AUTO_KEY_MRR=INV_GATE_DET.AUTO_KEY_MRR " & vbCrLf & " AND INV_GATE_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INV_GATE_HDR.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf & " AND INV_GATE_DET.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND INV_GATE_HDR.AUTO_KEY_MRR=" & Val(CStr(xMRRNo)) & "" & vbCrLf & " AND INV_GATE_HDR.MRR_DATE=TO_DATE(" & VB6.Format(xMRRDate, "DD/MMM/YYYY") & ",'DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPORate = Val(IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value))
        Else
            GetPORate = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetPORate = 0
    End Function

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

    End Sub

    Private Sub cboInvType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboInvType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboInvType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboInvType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mMRP As Double
        Dim mRate As Double
        'Dim mAbtementPer As Double							
        Dim mInvCode As Double
        Dim mSuppCode As String
        Dim mExpCode As Double
        Dim mIndentificationCode As String
        Dim pTCSRate As Double
        Dim mCustomerCode1 As String = ""

        chkStockTrf.CheckState = System.Windows.Forms.CheckState.Unchecked
        ChkPaintPrint.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkJWDetail.CheckState = System.Windows.Forms.CheckState.Unchecked
        If Trim(cboInvType.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode1 = Trim(MasterNo)
        End If

        SqlStr = "SELECT FIN_SUPP_CUST_MST.SUPP_CUST_CODE, FIN_INVTYPE_MST.CODE, " & vbCrLf _
            & " SUPP_CUST_NAME,ISSTOCKTRF,INV_HEADING,FIN_INVTYPE_MST.IDENTIFICATION,IS_OEM,IS_INSTITUTIONAL, IS_AFTER_MKT " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST,FIN_INVTYPE_MST " & vbCrLf _
            & " WHERE FIN_SUPP_CUST_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=FIN_INVTYPE_MST.COMPANY_CODE " & vbCrLf _
            & " AND FIN_SUPP_CUST_MST.SUPP_CUST_CODE=FIN_INVTYPE_MST.ACCOUNTPOSTCODE " & vbCrLf _
            & " AND FIN_INVTYPE_MST.NAME='" & MainClass.AllowSingleQuote((cboInvType.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)



        If RsTemp.EOF = False Then
            mInvCode = IIf(IsDBNull(RsTemp.Fields("CODE").Value), "", RsTemp.Fields("CODE").Value)
            mSuppCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)

            txtCreditAccount.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
            chkStockTrf.CheckState = IIf(RsTemp.Fields("ISSTOCKTRF").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            lblInvHeading.Text = IIf(IsDBNull(RsTemp.Fields("INV_HEADING").Value), "", RsTemp.Fields("INV_HEADING").Value)
            If RsTemp.Fields("Identification").Value = "J" Or RsTemp.Fields("Identification").Value = "M" Then
                chkPrintType.CheckState = System.Windows.Forms.CheckState.Unchecked
                '            ChkPaintPrint.Value = vbChecked					
            Else
                chkPrintType.CheckState = System.Windows.Forms.CheckState.Checked
                '            ChkPaintPrint.Value = vbUnchecked					
            End If

            If ADDMode = True Then
                Call FillExpFromPartyExp()
            End If
        End If

        If ADDMode = True Then
            With SprdExp
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColExpSTCode
                    mExpCode = Val(.Text)
                    If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "IDENTIFICATION", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mIndentificationCode = MasterNo
                    Else
                        mIndentificationCode = ""
                    End If
                    If mIndentificationCode = "MSR" Then
                        .Col = ColExpAmt
                        .Text = VB6.Format(pMSRCost, "0.00")
                        '                    Exit For			
                    End If

                    If mIndentificationCode = "MSC" Then
                        .Col = ColExpAmt
                        .Text = VB6.Format(pMSPCost, "0.00")
                        '                    Exit For			
                    End If

                    '                If mIndentificationCode = "EMS" Then				
                    '                    .Col = ColExpAmt				
                    '                    .Text = Format(pExciseableMSCCost, "0.00")				
                    ''                    Exit For				
                    '                End If				

                    If mIndentificationCode = "FRO" Then
                        .Col = ColExpAmt
                        .Text = VB6.Format(pFreightCost, "0.00")
                        '                    Exit For			
                    End If
                    If mIndentificationCode = "TOL" Then
                        .Col = ColExpAmt
                        .Text = VB6.Format(pToolAmorCost, "0.00")
                        '                    Exit For			
                    End If

                    If mIndentificationCode = "TCS" Then
                        .Col = ColExpPercent
                        pTCSRate = GetTCSApplication(mCustomerCode1, (lblDespRef.Text), VB6.Format(txtBillDate.Text, "DD/MM/YYYY"))
                        .Text = VB6.Format(pTCSRate, "0.0000")
                        '                Exit For			
                    End If

                Next
            End With

            If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (IS_OEM='Y' OR IS_INSTITUTIONAL='Y' OR IS_AFTER_MKT='Y')") = True Then
                With SprdMain
                    For cntRow = 1 To .MaxRows - 1
                        .Row = cntRow
                        .Col = ColItemCode
                        mItemCode = Trim(.Text)


                        If MainClass.ValidateWithMasterTable(cboInvType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (IS_OEM='Y' OR IS_INSTITUTIONAL='Y')") = True Then
                            .Col = ColMRP
                            .Text = "0.00"

                            .Col = ColRate
                            .Text = CStr(GetMRPRate((txtBillDate.Text), "RATE_OEM", mItemCode, "L"))
                        Else
                            .Col = ColMRP
                            mMRP = GetMRPRate((txtBillDate.Text), "RATE", mItemCode, "L")
                            .Text = CStr(mMRP)

                            .Col = ColRate
                            mRate = GetMRPRate((txtBillDate.Text), "RATE_AFTER_ABATE", mItemCode, "L")
                            .Text = CStr(mRate)



                            '                    If MainClass.ValidateWithMasterTable(mInvCode, "TRNTYPE", "ABATEMENT_PER", "FIN_PARTY_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'") = True Then		
                            '		
                            '                        mAbtementPer = MasterNo		
                            '                        mRate = mMRP - (mMRP * mAbtementPer * 0.01)		
                            '		
                            '                        .Col = ColRate		
                            '                        .Text = mRate		
                            '                    End If		
                        End If
                    Next
                End With
                FormatSprdMain(-1)
            End If
        End If
        Call CalcTots("N")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub chkDespatchFrom_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDespatchFrom.CheckStateChanged


        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkDespatchFrom.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            txtShippedFrom.Enabled = False
            cmdSearchDespatchFrom.Enabled = False
        Else
            txtShippedFrom.Enabled = True
            cmdSearchDespatchFrom.Enabled = True
        End If

    End Sub

    Private Sub chkExWork_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkExWork.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRejection_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRejection.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdeInvoice_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdeInvoice.Click
        On Error GoTo ErrPart
        Dim mMKey As String
        Dim meInvoiceApp As String

        If ADDMode = True Or MODIFYMode = True Then
            Exit Sub
        End If

        meInvoiceApp = "Y" ''IIf(PubUserID = "EINV", "Y", IIf(IsDBNull(RsCompany.Fields("E_INVOICE_APP").Value), "N", RsCompany.Fields("E_INVOICE_APP").Value))
        If meInvoiceApp = "N" Then Exit Sub

        If (CDbl(lblInvoiceSeq.Text) = 3 Or CDbl(lblInvoiceSeq.Text) = 4 Or CDbl(lblInvoiceSeq.Text) = 5 Or CDbl(lblInvoiceSeq.Text) = 0) Then
            Exit Sub
        End If


        mMKey = Trim(LblMKey.Text)

        If Trim(txtIRNNo.Text) = "" Then
            If WebRequestGenerateIRN_New(mMKey) = False Then Exit Sub
        Else
            MsgInformation("IRN Already generated.")
            Exit Sub
        End If


        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub


    Public Function WebRequestGenerateIRN_New(ByRef pMKey As String) As Boolean

        On Error GoTo ErrPart
        Dim url As String

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
        Dim mGetSignedInvoice As String
        Dim mCDKey As String
        Dim mEInvUserName As String
        Dim mEInvPassword As String
        Dim mEFUserName As String
        Dim mEFPassword As String

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



        Dim mSignedQRCode As String
        Dim mSignedInvoice As String
        'Dim pUserId As String							
        Dim mBMPFileName As String
        Dim pIsTesting As String = "Y"

        Dim pResponseText As String


        If GeteInvoiceSetupContents(url, "G", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword, pIsTesting) = False Then GoTo ErrPart

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

        Dim HTTP As Object
        HTTP = CreateObject("MSXML2.ServerXMLHTTP")


        mGSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mTaxSch = "GST"
        mVersion = "1.0"
        mIrn = ""
        If CDbl(lblInvoiceSeq.Text) = 6 Then
            mTran_Catg = IIf(chkLUT.CheckState = System.Windows.Forms.CheckState.Checked, "EXPWOP", "EXPWP") ''						
        Else
            mTran_Catg = "B2B"
        End If

        mTran_RegRev = "N"
        If chkDespatchFrom.CheckState = System.Windows.Forms.CheckState.Unchecked And chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            mTran_Typ = "REG"
        ElseIf chkDespatchFrom.CheckState = System.Windows.Forms.CheckState.Unchecked And chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTran_Typ = "SHP"
        ElseIf chkDespatchFrom.CheckState = System.Windows.Forms.CheckState.Checked And chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            mTran_Typ = "DIS"
        ElseIf chkDespatchFrom.CheckState = System.Windows.Forms.CheckState.Checked And chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTran_Typ = "CMB"
        End If

        mTran_EcmTrn = "N"
        mTran_EcmGstin = ""

        If CDbl(lblInvoiceSeq.Text) = 9 Then
            mDoc_Typ = "DBN"
        Else
            mDoc_Typ = "INV"
        End If

        mDOC_NO = Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & Trim(txtBillNoSuffix.Text)
        mDoc_Dt = VB6.Format(txtBillDate.Text, "DD/MM/YYYY")
        mDoc_OrgInvNo = ""



        If pIsTesting = "Y" Then
            mBillFrom_Gstin = "03AAACW3775F010"
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

        mSqlStr = " SELECT SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote((txtCustomer.Text)) & "'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mBillTo_TrdNm = Trim(txtCustomer.Text)
            mBillTo_Bno = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
            mBillTo_Bnm = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
            mBillTo_Flno = ""
            mBillTo_Loc = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
            mBillTo_Dst = ""
            mBillTo_Ph = ""
            mBillTo_Em = ""
            mToPlace = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

            If CDbl(lblInvoiceSeq.Text) = 6 Then
                mBillTo_Gstin = "URP"
                mBillTo_Pin = "999999"
                mBillTo_Stcd = CStr(96)
            Else
                mBillTo_Gstin = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
                mBillTo_Pin = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
                mBillTo_Stcd = GetStateCode(mToPlace)
            End If


        Else
            MsgInformation("Invalid Customer Name, Please Select Valid Customer Name.")
            WebRequestGenerateIRN_New = False
            HTTP = Nothing
            Exit Function
        End If

        If chkDespatchFrom.CheckState = System.Windows.Forms.CheckState.Checked Then
            mSqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtShippedFrom.Text) & "'"

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
            Else
                MsgInformation("Invalid Shipped From Customer Name, Please Select Valid Shipped From Customer Name.")
                WebRequestGenerateIRN_New = False
                HTTP = Nothing
                Exit Function
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


        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            'mSqlStr = " SELECT SUPP_CUST_NAME,SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf _
            '    & " FROM FIN_SUPP_CUST_MST" & vbCrLf _
            '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            '    & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtShippedTo.Text) & "'"

            mSqlStr = " SELECT " & vbCrLf _
                    & " BMST.LOCATION_ID, BMST.SUPP_CUST_NAME, BMST.SUPP_CUST_ADDR, BMST.SUPP_CUST_CITY, " & vbCrLf _
                    & " BMST.SUPP_CUST_STATE, BMST.SUPP_CUST_PIN, BMST.SUPP_CUST_PHONE, BMST.SUPP_CUST_FAXNO, " & vbCrLf _
                    & " BMST.SUPP_CUST_MAILID, BMST.SUPP_CUST_MOBILE, BMST.COUNTRY, BMST.GST_RGN_NO " & vbCrLf _
                    & " FROM FIN_SUPP_CUST_BUSINESS_MST BMST" & vbCrLf _
                    & " WHERE BMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'" & vbCrLf _
                    & " And BMST.LOCATION_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then

                mShipTo_TrdNm = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
                mShipTo_Loc = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)

                mShipTo_Bno = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                mShipTo_Bnm = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                mShipTo_Flno = ""
                mShipTo_Dst = ""
                mShipTo_Ph = ""
                mShipTo_Em = ""

                If CDbl(lblInvoiceSeq.Text) = 6 Then
                    mShipTo_Gstin = "URP"
                    mShipTo_Pin = "999999"
                    mShipTo_Stcd = CStr(96)
                Else
                    mShipTo_Gstin = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
                    mShipTo_Pin = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
                    mStateName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
                    mShipTo_Stcd = GetStateCode(mStateName)
                End If
            Else
                MsgInformation("Invalid Shipped to Customer Name, Please Select Valid Shipped To Customer Name.")
                WebRequestGenerateIRN_New = False
                HTTP = Nothing
                Exit Function
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


        mVal_AssVal = Val(lblTotTaxableAmt.Text)
        mVal_CgstVal = Val(lblTotCGSTAmount.Text)
        mVal_SgstVal = Val(lblTotSGSTAmount.Text)
        mVal_IgstVal = Val(lblTotIGSTAmount.Text)
        mVal_CesVal = 0
        mVal_StCesVal = 0
        mVal_CesNonAdVal = 0

        mVal_TotInvVal = Val(lblNetAmount.Text)
        mVal_OthChrg = CDbl(VB6.Format(mVal_TotInvVal - (mVal_AssVal + mVal_CgstVal + mVal_SgstVal + mVal_IgstVal + Val(lblMSC.Text)) - Val(lblRO.Text), "0.00")) 'Val(lblTotExpAmt.text)  ''							
        '    mVal_OthChrg = Format(mVal_TotInvVal - (Val(lblTotItemValue.text) + mVal_CgstVal + mVal_SgstVal + mVal_IgstVal + Val(lblRO.text)), "0.00")							

        '    mVal_OthChrg = Val(lblMSC.text)							
        mVal_Disc = Val(lblMSC.Text) * -1

        '    If mVal_OthChrg < 0 Then							
        '        mVal_Disc = Format(mVal_OthChrg * -1, "0.00")							
        '        mVal_OthChrg = Format(mVal_AssVal - Val(lblTotItemValue.text), "0.00")							
        '    Else							
        '        mVal_Disc = 0							
        '        mVal_OthChrg = Format(mVal_OthChrg + mVal_AssVal - Val(lblTotItemValue.text), "0.00")							
        '    End If							

        'pInvoiceValue = Format(lblNetAmount.text, "0.00")							
        '    pTaxableValue = Format(lblTaxableAmount.text, "0.00")							
        '							
        '    pCGSTValue = Format(lblCGSTAmt.text, "0.00")							
        '    pSGSTValue = Format(lblSGSTAmt.text, "0.00")							
        '    pIGSTValue = Format(lblIGSTAmt.text, "0.00")							
        '							
        '    pOtherValue = Format(pInvoiceValue - (pTaxableValue + pCGSTValue + pSGSTValue + pIGSTValue), "0.00")							
        '							

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


        If CDbl(lblInvoiceSeq.Text) = 6 Then
            mExp_ShipBNo = Trim(txtShippingNo.Text)
            mExp_ShipBDt = VB6.Format(txtShippingDate.Text, "DD/MM/YYYY")
            mExp_Port = Trim(txtPortCode.Text)

            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "CURRENCY_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mExp_ForCur = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "COUNTRY_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mExp_CntCode = MasterNo
            End If

        Else
            mExp_ShipBNo = ""
            mExp_ShipBDt = ""
            mExp_Port = ""
            mExp_ForCur = ""
            mExp_CntCode = ""
        End If

        mGetQRImg = "0" ''0 for text , 1 for Image							
        mGetSignedInvoice = "0" ''1 - Signed Json of Invoice will be return, 0 - will not return signed Invoice.							


        HTTP.Open("POST", url, False)

        HTTP.setRequestHeader("Content-Type", "application/json")


        mBody = "{""Push_Data_List"":{"
        mBody = mBody & """Data"": ["
        For cntRow = 1 To SprdMain.MaxRows - 1
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


            SprdMain.Row = cntRow
            SprdMain.Col = ColItemCode

            SprdMain.Col = ColItemDesc
            mItem_PrdNm = MainClass.AllowSingleQuote(SprdMain.Text)
            mItem_PrdNm = MainClass.AllowDoubleQuote(mItem_PrdNm)
            mItem_PrdDesc = MainClass.AllowSingleQuote(SprdMain.Text)
            mItem_PrdDesc = MainClass.AllowDoubleQuote(mItem_PrdDesc)

            SprdMain.Col = ColHSNCode
            mItem_HsnCd = Trim(SprdMain.Text)

            mItem_Barcde = ""

            SprdMain.Col = ColQty
            mItem_Qty = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
            mItem_FreeQty = 0

            SprdMain.Col = ColUnit
            mItem_Unit = Trim(SprdMain.Text)

            SprdMain.Col = ColRate
            mItem_UnitPrice = CDbl(VB6.Format(Val(SprdMain.Text), "0.0000"))

            SprdMain.Col = ColTaxableAmount ''ColAmount		
            mItem_TotAmt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

            SprdMain.Col = ColTaxableAmount
            mItem_AssAmt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

            mItem_Discount = 0
            '                        If Val(lblTotItemValue.text) <> 0 Then		
            '                            mItem_Discount = Format(mVal_Disc * mItem_TotAmt / Val(lblTotItemValue.text), "0.00")		
            '                        End If		

            mItem_OthChrg = mItem_AssAmt - mItem_TotAmt
            mItem_OthChrg = CDbl(VB6.Format(mItem_OthChrg, "0.00"))


            '                        mItem_OthChrg = mItem_OthChrg - mItem_Discount		

            '                        If mItem_OthChrg < 0 Then		
            '                            mItem_Discount = mItem_OthChrg		
            '                            mItem_OthChrg = 0		
            '                        Else		
            '                            mItem_Discount = 0		
            '                            mItem_OthChrg = mItem_OthChrg		
            '                        End If		

            '                     = ""		
            '                    mItem_TotItemVal = ""		
            '                    mItem_Bch_Nm = ""		
            '                    mItem_Bch_ExpDt = ""		
            '                    mItem_Bch_WrDt = ""		


            SprdMain.Col = ColSGSTPer
            mItem_SgstRt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

            SprdMain.Col = ColCGSTPer
            mItem_CgstRt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

            SprdMain.Col = ColIGSTPer
            mItem_IgstRt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

            SprdMain.Col = ColSGSTAmount
            mItem_SgstAmt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

            SprdMain.Col = ColCGSTAmount
            mItem_CgstAmt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

            SprdMain.Col = ColIGSTAmount
            mItem_IgstAmt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

            mItem_CesRt = 0
            mItem_CesNonAdval = 0
            mItem_StateCes = 0
            mItem_TotItemVal = mItem_TotAmt + mItem_SgstAmt + mItem_CgstAmt + mItem_IgstAmt + mItem_CesNonAdval + mItem_StateCes + mItem_OthChrg ''- mItem_Discount '' mItem_AssAmt 30/09' (mItem_AssAmt * ((100 + mItem_SgstRt + mItem_CgstRt + mItem_IgstRt + mItem_CesRt + mItem_StateCes) * 0.01)) + mItem_CesNonAdval		
            mItem_TotItemVal = CDbl(VB6.Format(mItem_TotItemVal, "0.00"))

            mBody = mBody & """Item_SlNo"": """ & cntRow & ""","

            mBody = mBody & """Item_PrdDesc"":""" & mItem_PrdDesc & ""","
            mBody = mBody & """Item_IsServc"":""" & IIf(CDbl(lblInvoiceSeq.Text) = 2, "Y", "N") & ""","
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
            mBody = mBody & """Val_RndOffAmt"":""" & VB6.Format(Val(lblRO.Text), "0.00") & ""","


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

            If cntRow = SprdMain.MaxRows - 1 Then
                mBody = mBody & "}"
            Else
                mBody = mBody & "},"
            End If
        Next

        mBody = mBody & "]"
        mBody = mBody & "}"
        mBody = mBody & "}"

        ' shipToGSTIN String GSTIN of  Ship-To shipToTradeName String Trade Name of  Ship-To dispatchFromGSTIN String GSTIN of Dispatch-From dispatchFromTradeName String Trade Name of Dispatch-From IsBillFromShipFromSame String Required, 0 for Different BillFrom and ShipFrom, 1 for Same BillFrom and ShipFrom IsBillToShipToSame String Required, 0 for Different BillTo and ShipTo, 1 for Same BillTo and ShipTo IsGSTINSEZ							

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

            txtIRNNo.Text = Trim(mIRNNo)
            txteInvAckNo.Text = Trim(mIRNAckNo)
            txteInvAckDate.Text = VB6.Format(mIRNAckDate, "DD/MM/YYYY HH:MM")


            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            SqlStr = ""

            SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
                    & " IRN_NO ='" & Trim(txtIRNNo.Text) & "'," & vbCrLf _
                    & " IRN_ACK_NO ='" & Trim(txteInvAckNo.Text) & "'," & vbCrLf _
                    & " IRN_ACK_DATE =TO_DATE('" & VB6.Format(txteInvAckDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY ='" & pMKey & "'"

            PubDBCn.Execute(SqlStr)


            SqlStr = "INSERT INTO FIN_INVOICE_QRCODE " & vbCrLf _
                    & " ( MKEY, COMPANY_CODE, SIGNQRCODE ) VALUES (" & vbCrLf _
                    & " '" & pMKey & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " '" & mSignedQRCode & "')"

            PubDBCn.Execute(SqlStr)

            PubDBCn.CommitTrans()

            mBMPFileName = mPubBarCodePath & "\" & Replace(Trim(txtBillNoPrefix.Text), "/", "") & Trim(txtBillNo.Text) & ".bmp"

            If GererateQRCodeImage(mBMPFileName, mSignedQRCode) = False Then GoTo ErrPart

            If UpdateQRCODE(CDbl(LblMKey.Text), mBMPFileName) = False Then GoTo ErrPart

        End If

        If pStaus = "0" Then
            pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            MsgInformation(pError)
            WebRequestGenerateIRN_New = False
            HTTP = Nothing
            Exit Function
        End If

        WebRequestGenerateIRN_New = True
        HTTP = Nothing
        '    Set httpGen = Nothing							
        Exit Function
ErrPart:
        '    Resume							
        WebRequestGenerateIRN_New = False
        'http = Nothing							
        MsgBox(Err.Description)
        PubDBCn.RollbackTrans()
    End Function

    Public Function WebRequestGenerateDigitalSign(ByRef pPDFFileName As String, ByRef pPDFOutFileName As String) As Boolean
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

        ''    If GeteInvoiceSetupContents(url, "G", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword) = False Then GoTo ErrPart							

        url = "http://192.168.0.191:82/service.asmx"


        '' "http://ip.webtel.in/webesignapi/service.asmx"
        mUserName = "admin" '' "rR482Xeoilw" ''"06AAACW3775F013"							
        mPassword = "admin@123"  '' "Rqsie103pd"

        '        App url: http : //192.168.0.191 ''http://103.178.248.99:80
        'user Name: admin
        'password: admin@123


        'Api url: http : //192.168.0.191:82/service.asmx


        Dim http As Object   '' Dim http As MSXML2.XMLHTTP60 '' MSXML.xmlhttp							
        http = CreateObject("MSXML2.ServerXMLHTTP")

        '      '22/10/2021 http.Open("POST", url, False)							

        ''    http.setRequestHeader "Content-Type", "application/json"							

        ''    http.Open "POST", url, False							

        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")


        'http.setRequestHeader("Host", "ip.webtel.in")
        'http.setRequestHeader("Content-Type", "text/xml; charset=utf-8")
        'http.setRequestHeader("Content-Length", "length")
        'http.setRequestHeader("SOAPAction", "http://tempuri.org/SignPDF")
        http.setRequestHeader("UserName", mUserName)
        http.setRequestHeader("Password", mPassword)

        Dim details As New List(Of WebSignData)()

        details.Add(New WebSignData() With {
        .PDFByte = pPDFFileName,
        .AuthorizeSignatory = "SANDEEP KANDWAL",
        .SignerName = "SANDEEP KANDWAL",
        .TopLeft = 100,
        .BottemLeft = 290,
        .TopRight = 190,
        .BottomRight = 340,
        .ExcludePageNumber = "",
        .InvoiceNumber = "",
        .PageNo = -1,
        .PrintDate = "",
        .FindAuth = "",
        .FindAuthLocation = ""
           })

        Dim mBodyDetail As String = JsonConvert.SerializeObject(details)


        mBody = "{""Push_Data_List"":{"
        mBody = mBody & """Data"": "
        mBody = mBody & mBodyDetail
        mBody = mBody & "}"
        mBody = mBody & "}"

        ' <?xml version="1.0" encoding="utf-8"?>							
        '<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">							
        '  <soap:Header>							
        '    <AuthHeader xmlns="http://tempuri.org/">							
        '      <Username>string</Username>							
        '      <Password>string</Password>							
        '    </AuthHeader>							
        '  </soap:Header>							
        '  <soap:Body>							
        '    <SignPDF xmlns="http://tempuri.org/">							
        '      <pdfByte>base64Binary</pdfByte>							
        '      <AuthorizedSignatory>string</AuthorizedSignatory>							
        '      <SignerName>string</SignerName>							
        '      <TopLeft>int</TopLeft>							
        '      <BottomLeft>int</BottomLeft>							
        '      <TopRight>int</TopRight>							
        '      <BottomRight>int</BottomRight>							
        '      <ExcludePageNo>string</ExcludePageNo>							
        '      <InvoiceNumber>string</InvoiceNumber>							
        '      <pageNo>int</pageNo>							
        '      <PrintDateTime>string</PrintDateTime>							
        '      <FindAuth>string</FindAuth>							
        '      <FindAuthLocation>int</FindAuthLocation>							
        '    </SignPDF>							
        '  </soap:Body>							
        '</soap:Envelope>							

        http.Send(mBody)

        pResponseText = http.responseText
        '    pResponseText = Replace(pResponseText, "\", "")							
        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        '    pResponseText = Mid(pResponseText, 2, Len(pResponseText) - 2)							

        Dim JsonTest As Object
        Dim SB As New cStringBuilder

        Dim c As Object
        Dim I As Integer

        JsonTest = JSON.parse(pResponseText)

        pStaus = JsonTest.Item("Status")


        If pStaus = "1" Then
            ''pPDFOutFileName						
        End If

        If pStaus = "0" Then
            pError = JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            MsgInformation(pError)
            WebRequestGenerateDigitalSign = False
            http = Nothing
            Exit Function
        End If

        WebRequestGenerateDigitalSign = True
        http = Nothing
        '    Set httpGen = Nothing							
        Exit Function
ErrPart:
        '    Resume							
        WebRequestGenerateDigitalSign = False
        'http = Nothing							
        MsgBox(Err.Description)
        '     PubDBCn.RollbackTrans							
    End Function
    Private Sub cmdQRCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdQRCode.Click
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
        'Dim pBranchId As String							
        'Dim pTokenId As String							
        'Dim pUserId As String							
        Dim mBMPFileName As String


        Dim pResponseText As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pIsTesting As String = "Y"

        If Trim(txtIRNNo.Text) = "" Then Exit Sub
        SqlStr = "SELECT SIGNQRCODE FROM FIN_INVOICE_QRCODE " & vbCrLf _
            & " WHERE MKEY = '" & LblMKey.Text & "'" & vbCrLf _
            & " AND COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


        If RsTemp.EOF = False Then
            mSignedQRCode = IIf(IsDBNull(RsTemp.Fields("SIGNQRCODE").Value), "", RsTemp.Fields("SIGNQRCODE").Value)

            mBMPFileName = mPubBarCodePath & "\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".bmp"

            If GererateQRCodeImage(mBMPFileName, mSignedQRCode) = False Then GoTo ErrPart

            If UpdateQRCODE(LblMKey.Text, mBMPFileName) = False Then GoTo ErrPart
        Else

            If GeteInvoiceSetupContents(url, "I", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword, pIsTesting) = False Then GoTo ErrPart

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


            Dim http As Object '' MSXML2.XMLHTTP60 '' MSXML.xmlhttp							
            http = CreateObject("MSXML2.ServerXMLHTTP")

            mIRNNo = Trim(txtIRNNo.Text)

            mGetQRImg = "0" ''0 for text , 1 for Image							
            mGetSignedInvoice = "0" ''1 - Signed Json of Invoice will be return, 0 - will not return signed Invoice.							

            http.Open("POST", url, False)

            http.setRequestHeader("Content-Type", "application/json")


            Dim details As New List(Of IRNQRData)()

            details.Add(New IRNQRData() With {
            .Irn = mIRNNo,
            .GSTIN = mGSTIN,
            .CDKey = mCDKey,
            .EInvUserName = mEInvUserName,
            .EInvPassword = mEInvPassword,
            .EFUserName = mEFUserName,
            .EFPassword = mEFPassword
               })


            Dim mBodyDetail As String = JsonConvert.SerializeObject(details)


            mBody = "{""Push_Data_List"":{"
            mBody = mBody & """Data"": "
            mBody = mBody & mBodyDetail
            mBody = mBody & "}"
            mBody = mBody & "}"

            'Dim IRNQRData = New IRNQRData()

            'IRNQRData.Irn = mIRNNo
            'IRNQRData.GSTIN = mGSTIN
            'IRNQRData.CDKey = mCDKey
            'IRNQRData.EInvUserName = mEInvUserName
            'IRNQRData.EInvPassword = mEInvPassword
            'IRNQRData.EFUserName = mEFUserName
            'IRNQRData.EFPassword = mEFPassword

            'mBody = JsonConvert.SerializeObject(IRNQRData)

            'Dim customers As List(Of IRNQRData) = JsonConvert.SerializeObject(List(Of IRNQRData))


            'mBody = "{""Push_Data_List"":{"
            'mBody = mBody & """Data"": ["
            'mBody = mBody & "{"

            'mBody = mBody & """Irn"":""" & mIRNNo & ""","
            'mBody = mBody & """GSTIN"":""" & mGSTIN & ""","
            'mBody = mBody & """CDKey"":""" & mCDKey & ""","
            'mBody = mBody & """EInvUserName"":""" & mEInvUserName & ""","
            'mBody = mBody & """EInvPassword"":""" & mEInvPassword & ""","
            'mBody = mBody & """EFUserName"":""" & mEFUserName & ""","
            'mBody = mBody & """EFPassword"":""" & mEFPassword & """"

            'mBody = mBody & "}"


            'mBody = mBody & "]"
            'mBody = mBody & "}"
            'mBody = mBody & "}"

            http.Send(mBody)

            pResponseText = http.responseText
            pResponseText = Replace(pResponseText, "[", "")
            pResponseText = Replace(pResponseText, "]", "")
            pResponseText = Replace(pResponseText, """", "'")

            Dim post As Object
            pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Status = ""})).Status

            If pStaus = "1" Then
                mSignedQRCode = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .SignedQRCode = ""})).SignedQRCode 'JsonTest.Item("SignedQRCode")
                mSignedInvoice = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .SignedInvoice = ""})).SignedInvoice ' JsonTest.Item("SignedInvoice")

                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                SqlStr = "INSERT INTO FIN_INVOICE_QRCODE " & vbCrLf _
                    & " ( MKEY, COMPANY_CODE, SIGNQRCODE ) VALUES (" & vbCrLf _
                    & " '" & LblMKey.Text & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " '" & mSignedQRCode & "')"

                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()

                mBMPFileName = mPubBarCodePath & "\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".bmp"
                If GererateQRCodeImage(mBMPFileName, mSignedQRCode) = False Then GoTo ErrPart

                If UpdateQRCODE(CDbl(LblMKey.Text), mBMPFileName) = False Then GoTo ErrPart
            End If

            If pStaus = "0" Then
                pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
                MsgInformation(pError)
                http = Nothing
                Exit Sub
            End If

            http = Nothing
            '    Set httpGen = Nothing		
        End If
        Exit Sub
ErrPart:
        '    Resume							
        'http = Nothing							
        MsgBox(Err.Description)

    End Sub

    '    Private Function GererateQRCodeImage(ByVal mBMPFileName As String, ByRef pSignedQRCode As String) As Boolean

    '        On Error GoTo ErrPart

    '        Dim qrGenerator As New QRCodeGenerator()

    '        Dim QRCodeData As QRCodeData = qrGenerator.CreateQrCode(pSignedQRCode, QRCodeGenerator.ECCLevel.Q)

    '        Dim qrCode As New QRCode(QRCodeData)

    '        Dim imgBarCode As New System.Web.UI.WebControls.Image()
    '        imgBarCode.Height = 150
    '        imgBarCode.Width = 150
    '        'Dim mBMPFileName As String = Application.ExecutablePath

    '        'mBMPFileName = mPubBarCodePath & "\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".Bmp"   ''file_name.Substring(0, file_name.LastIndexOf("\bin")) & "\test."

    '        Using bitMap As Bitmap = qrCode.GetGraphic(20)
    '            bitMap.Save(mBMPFileName, System.Drawing.Imaging.ImageFormat.Bmp)
    '        End Using

    '        GererateQRCodeImage = True
    '        Exit Function
    'ErrPart:
    '        'Resume							
    '        GererateQRCodeImage = False
    '        MsgInformation(Err.Description)
    '    End Function
    Private Function UpdateQRCODE(ByRef nMkey As Double, ByRef pFilePath As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As New ADODB.Recordset
        Dim mInventoryGroupCode As Integer
        Dim mstream As ADODB.Stream

        UpdateQRCODE = True
        Exit Function

        If pFilePath = "" Or Trim(txtIRNNo.Text) = "" Then UpdateQRCODE = True : Exit Function

        If AccessCnn.State <> ADODB.ObjectStateEnum.adStateOpen Then
            AccessCnn.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBConDataPath & "ERPIMAGE.mdb;Persist Security Info=False")
            ''AccessCnn.Open "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & App.path & "\DATA\ERPIMAGE.mdb;Persist Security Info=False"						
        End If
        AccessCnn.BeginTrans()

        SqlStr = "Delete From INVOICE_QRCODE WHERE MKEY='" & nMkey & "'"
        AccessCnn.Execute(SqlStr)

        SqlStr = "Select * From INVOICE_QRCODE " 'WHERE ITEMCODE='" & pcls6.AllowSingleQuote(txtItemCode.Text) & "'"							
        MainClass.UOpenRecordSet(SqlStr, AccessCnn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)
        RS.Find("MKEY='" & nMkey & "'")
        Dim ss As String    ''PropertyBag							
        If RS.EOF Then
            RS.AddNew()
            RS.Fields("mKey").Value = nMkey
            RS.Fields("COMPANY_CODE").Value = RsCompany.Fields("COMPANY_CODE").Value
            RS.Fields("IRN_NO").Value = txtIRNNo.Text
            RS.Fields("BFILE_TYPE").Value = "BMP"

            '                GetPhoto IIf(CDlg1.FileName = "", "Photo", App.path & "\Picture\MIPLITEM.BMP"), Rs, "ItemPicture", "ItemPicSize"						
            GetPhoto(IIf(pFilePath = "", "Photo", pFilePath), RS, "INV_QRCODE", "ItemPicSize")
            RS.Update()
        Else
            'GetPhoto IIf(CDlg1.FileName = "", "Photo", App.Path & "\Picture\MIPLITEM.BMP"), Rs, "ItemPicture", "ItemPicSize"						
            'SaveImageToDB Me.Picture1.Picture, Rs, "pic"						

            'Set ss = New PropertyBag						
            'ss.WriteProperty "MyImage", pPic						
            'Rs.Fields("ItemPicture").AppendChunk ss.Contents						
            ''Rs.Update						
            'Set ss = Nothing						


            mstream = New ADODB.Stream
            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            mstream.Open()

            mstream.LoadFromFile(pFilePath) ''App.path & "\Picture\ITEM.BMP"						
            RS.Fields("INV_QRCODE").Value = mstream.Read

            RS.Update()
        End If
        '       AccessCnn.Execute SqlStr							
        AccessCnn.CommitTrans()
        UpdateQRCODE = True
        Exit Function
ErrPart:
        'Resume							
        UpdateQRCODE = False
        MsgInformation(Err.Description)
    End Function




    Private Sub cmpPrinteInvoice_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmpPrinteInvoice.Click
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
        'Dim pBranchId As String							
        'Dim pTokenId As String							
        'Dim pUserId As String							
        Dim mBMPFileName As String
        Dim mFilePath As String
        Dim pIsTesting As String = "Y"
        Dim pResponseText As String

        If Trim(txtIRNNo.Text) = "" Then Exit Sub

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


        mIRNNo = Trim(txtIRNNo.Text)

        '    mGetQRImg = "0"      ''0 for text , 1 for Image							
        '    mGetSignedInvoice = "0"  ''1 - Signed Json of Invoice will be return, 0 - will not return signed Invoice.							

        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")
        mBody = ""
        'mBody = "{""Push_Data_List"":{"
        'mBody = mBody & """Data"": ["
        mBody = mBody & "{"

        mBody = mBody & """Irn"":""" & mIRNNo & ""","
        mBody = mBody & """GSTIN"":""" & mGSTIN & ""","
        mBody = mBody & """CDKey"":""" & mCDKey & ""","
        mBody = mBody & """EInvUserName"":""" & mEInvUserName & ""","
        mBody = mBody & """EInvPassword"":""" & mEInvPassword & ""","
        mBody = mBody & """EFUserName"":""" & mEFUserName & ""","
        mBody = mBody & """EFPassword"":""" & mEFPassword & """"

        mBody = mBody & "}"


        'mBody = mBody & "]"
        'mBody = mBody & "}"
        'mBody = mBody & "}"

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
                'Dim success As Long = ShellEx(0&, "open", mFilePath, vbNullString, vbNullString, vbNormalFocus)
                'Shell(mFilePath, AppWinStyle.NormalFocus)
                'ShellExecute(Me.Handle.ToInt32, "open", mFilePath, vbNullString, vbNullString, vbNormalFocus)
                'WebBrowser.Navigate(mFilePath)
            End If

        End If

        If pStaus = "0" Then
            pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("File") '' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            MsgInformation(pError)
            http = Nothing
            Exit Sub
        End If

        http = Nothing
        '    Set httpGen = Nothing							
        Exit Sub
ErrPart:
        '    Resume							
        http = Nothing
        MsgBox(Err.Description)

    End Sub

    Private Sub txtShippedFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShippedFrom.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtShippedFrom_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtShippedFrom.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtShippedFrom.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtShippedFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShippedFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String


        If Trim(txtShippedFrom.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtShippedFrom.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgBox("Invalid Shipped From Supplier Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchDespatchFrom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDespatchFrom.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        If ADDMode = True Then
            SqlStr = SqlStr & " AND STATUS='O'"
        End If
        If MainClass.SearchGridMaster((txtShippedFrom.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtShippedFrom.Text = AcName
            txtShippedFrom_Validating(txtShippedFrom, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Sub txtShippedFrom_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShippedFrom.DoubleClick
        cmdSearchDespatchFrom_Click(cmdSearchDespatchFrom, New System.EventArgs())
    End Sub

    Private Sub txtShippedFrom_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtShippedFrom.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchDespatchFrom_Click(cmdSearchDespatchFrom, New System.EventArgs())
    End Sub

    Private Sub txtDistance_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDistance.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPOWEFDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPOWEFDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPOWEFDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPOWEFDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtPOWEFDate.Text = "" Then GoTo EventExitSub
        If IsDate(txtPOWEFDate.Text) = False Then
            ErrorMsg("Invalid Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPOAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPOAmendNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPOAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPOAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTransportCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTransportCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTransportCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTransportCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTransportCode.Text, "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtResponseId_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtResponseId.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtResponseId_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtResponseId.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtResponseId.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEwayBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEWayBillNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtEwayBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEWayBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEWayBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub cboTransmode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTransmode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboVehicleType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboVehicleType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDistance_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDistance.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkAgtPermission_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAgtPermission.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged, chkByHand.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkDutyFreePurchase_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDutyFreePurchase.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkFOC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFOC.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkLUT_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkLUT.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If CDbl(lblInvoiceSeq.Text) = 6 And ADDMode = True Then
            If chkLUT.CheckState = System.Windows.Forms.CheckState.Checked Then
                If MsgQuestion("Are you sure to Click LUT, Once it click all taxes have Zero.") = CStr(MsgBoxResult.No) Then
                    chkLUT.CheckState = System.Windows.Forms.CheckState.Unchecked
                    Exit Sub
                Else
                    CalcTots("N")
                End If
            End If
        End If
    End Sub


    Private Sub chkPrintTextDesc_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPrintTextDesc.CheckStateChanged
        '    MainClass.SaveStatus Me, ADDMode, MODIFYMode							
    End Sub

    Private Sub chkShipTo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShipTo.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    txtShippedTo.Enabled = False
        '    cmdSearchShippedTo.Enabled = False
        'Else
        '    txtShippedTo.Enabled = True
        '    cmdSearchShippedTo.Enabled = True
        'End If
    End Sub

    Private Sub cmdSavePrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSavePrint.Click
        On Error GoTo ErrPart
        If CDbl(lblInvoiceSeq.Text) = 2 Or CDbl(lblInvoiceSeq.Text) = 3 Then
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            ReportForF4Show(Crystal.DestinationConstants.crptToWindow)
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End If

        Exit Sub
ErrPart:

    End Sub

    Private Sub ReportForF4Show(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String = ""
        Dim PrintStatus As Boolean
        Dim mReportFileName As String


        'Select Record for print...							

        SqlStr = ""

        '    SqlStr = MainClass.FillPrintDummyDataFromSprd() 'MakeF4SQL(LblMKey.text)							

        If FillPrintDummyDataFromDS(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1
        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mTitle = "Delivery Challan After Jobwork"
        '    mSubTitle = "Bill No : " & txtBillNoPrefix.Text & vb6.Format(txtBillNo.Text, ConBillFormat) & " & Bill Date : " & vb6.Format(txtBillDate.Text, "DD/MM/YYYY")							

        mReportFileName = "JobWorkChallan.Rpt" '' "BillF4Detail.Rpt"							

        Call ShowReportDC(SqlStr, Mode, mTitle, mSubTitle, mReportFileName)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        '    Resume							
    End Sub
    Public Function FillPrintDummyDataFromDS(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer, ByRef mPvtDBCn As ADODB.Connection) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...							
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        'Dim GetData As String							
        'Dim SetData As String							
        Dim SqlStr As String = ""

        Dim mItemCode As String
        Dim mItemName As String
        Dim mHSNCode As String
        Dim mQty As Double
        Dim mItemUOM As String
        Dim mItemRate As Double
        Dim mTaxableValue As Double
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim m57F4 As String
        Dim m57F4date As String
        Dim mRefNo As String
        Dim xAcctCode As String
        Dim mMerchantExporter As String = "N"

        mPvtDBCn.Errors.Clear()

        mPvtDBCn.BeginTrans()

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            xAcctCode = "-1"
        End If

        mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER= 'EXPORTER-MERCHANT'") = True Then
            mMerchantExporter = "Y"
        End If

        'mLocal = "N"
        'If Trim(txtCustomer.Text) <> "" Then
        '    If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mLocal = Trim(MasterNo)
        '    End If
        'End If

        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If


        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        mPvtDBCn.Execute(SqlStr)

        With SprdMain
            For RowNum = 1 To .MaxRows - 1
                .Row = RowNum
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColItemDesc
                mItemName = Trim(.Text)

                mHSNCode = GetHSNCode(mItemCode)

                .Col = ColUnit
                mItemUOM = Trim(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = Col57F4
                m57F4 = Trim(.Text)

                .Col = Col57F4Date
                m57F4date = Trim(.Text)

                mRefNo = m57F4 & " " & m57F4date

                .Col = ColRate
                mItemRate = Trim(.Text)     '' GetChallanRate(mItemCode, m57F4)

                If GetHSNDetails(mHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, "G", mPartyGSTNo,,,, mMerchantExporter) = False Then GoTo PrintDummyErr

                mTaxableValue = CDbl(VB6.Format(mItemRate * mQty, "0.00"))

                mCGSTAmount = CDbl(VB6.Format(mItemRate * mQty * mCGSTPer * 0.01, "0.00"))
                mSGSTAmount = CDbl(VB6.Format(mItemRate * mQty * mSGSTPer * 0.01, "0.00"))
                mIGSTAmount = CDbl(VB6.Format(mItemRate * mQty * mIGSTPer * 0.01, "0.00"))

                SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf _
                    & " FIELD1, FIELD2, FIELD3," & vbCrLf & " FIELD4, FIELD5, FIELD6," & vbCrLf _
                    & " FIELD7, FIELD8, FIELD9," & vbCrLf _
                    & " FIELD10, FIELD11, FIELD12, FIELD13, FIELD14  " & vbCrLf _
                    & " ) " & vbCrLf _
                    & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mItemCode) & "', '" & MainClass.AllowSingleQuote(mItemName) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mHSNCode) & "', '" & (mQty) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mItemUOM) & "', '" & (mItemRate) & "'," & vbCrLf _
                    & " '" & (mTaxableValue) & "', '" & (mCGSTPer) & "'," & vbCrLf _
                    & " '" & (mCGSTAmount) & "', '" & (mSGSTPer) & "'," & vbCrLf _
                    & " '" & (mSGSTAmount) & "', '" & (mIGSTPer) & "'," & vbCrLf & " '" & (mIGSTAmount) & "', '" & (mRefNo) & "'" & vbCrLf & " ) "


                mPvtDBCn.Execute(SqlStr)
NextRec:
            Next
        End With

        mPvtDBCn.CommitTrans()
        FillPrintDummyDataFromDS = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillPrintDummyDataFromDS = False
        mPvtDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function GetChallanRate(ByRef mItemCode As String, ByRef m57F4 As String) As Double

        On Error GoTo ErrPart

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mCustCode As String

        mCustCode = ""
        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustCode = MasterNo
        End If

        '    SqlStr = " SELECT MAX(ID.ITEM_RATE) AS  ITEM_RATE" & vbCrLf _							
        ''            & " FROM DSP_PAINT57F4_HDR IH, DSP_PAINT57F4_DET ID, DSP_PAINT57F4_TRN TRN" & vbCrLf _							
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _							
        ''            & " AND IH.MKEY=ID.MKEY AND ID.MKEY = TRN.MKEY AND ID.ITEM_CODE=TRN.ITEM_CODE" & vbCrLf _							
        ''            & " AND IH.PARTY_F4NO='" & MainClass.AllowSingleQuote(m57F4) & "' " & vbCrLf _							
        ''            & " AND TRN.SUB_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' " & vbCrLf _							
        ''            & " AND IH.BookType='D' "							

        SqlStr = " SELECT MAX(ID.ITEM_RATE) AS  ITEM_RATE" & vbCrLf & " FROM DSP_PAINT57F4_HDR IH, DSP_PAINT57F4_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.PARTY_F4NO='" & MainClass.AllowSingleQuote(m57F4) & "' " & vbCrLf & " AND IH.BookType='D' " & vbCrLf & " AND ID.ITEM_CODE IN ( " & vbCrLf & " SELECT ITEM_CODE FROM DSP_PAINT57F4_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.MKEY='" & txtDCNo.Text & "'" & vbCrLf & " AND TRN.SUB_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND TRN.SUPP_CUST_CODE=IH.SUPP_CUST_CODE" & vbCrLf & " AND TRN.PARTY_F4NO=IH.PARTY_F4NO)" & vbCrLf


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetChallanRate = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value), "0.00"))
        End If

        Exit Function
ErrPart:
        GetChallanRate = 0
    End Function
    Private Function MakeF4SQL(ByRef pMKey As String) As String
        On Error GoTo ERR1

        ''SELECT CLAUSE...							

        MakeF4SQL = " SELECT ID2.CUSTOMER_PART_NO, ID2.ITEM_SHORT_DESC, " & vbCrLf & " SUM(IH.ITEM_QTY) AS ITEM_QTY, IH.PARTY_F4NO "

        ''FROM CLAUSE...							
        MakeF4SQL = MakeF4SQL & vbCrLf & " FROM DSP_PAINT57F4_TRN IH, INV_ITEM_MST ID1, INV_ITEM_MST ID2"


        ''WHERE CLAUSE...							
        MakeF4SQL = MakeF4SQL & vbCrLf & " WHERE " & vbCrLf & " IH.SUB_ITEM_CODE=ID1.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=ID1.COMPANY_CODE" & vbCrLf & " AND IH.ITEM_CODE=ID2.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=ID2.COMPANY_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY='" & LblMKey.Text & "'" & vbCrLf & " AND IH.BOOKTYPE='D' AND IH.ISSCRAP='N'"

        ''GROUP BY CLAUSE...							

        MakeF4SQL = MakeF4SQL & vbCrLf & "GROUP BY ID2.CUSTOMER_PART_NO, ID2.ITEM_SHORT_DESC, IH.PARTY_F4NO "

        ''ORDER CLAUSE...							

        MakeF4SQL = MakeF4SQL & vbCrLf & "ORDER BY ID2.CUSTOMER_PART_NO" ''IH.SUBROWNO"							

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub txtAdvAdjust_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvAdjust.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub CalcAdvTots()
        On Error GoTo ERR1
        Dim mNetAdvanceAmount As Double


        txtItemAdvAdjust.Text = VB6.Format(txtItemAdvAdjust.Text, "0.00")
        mNetAdvanceAmount = Val(txtItemAdvAdjust.Text)

        txtAdvCGST.Text = VB6.Format(txtAdvCGST.Text, "0.00")
        mNetAdvanceAmount = mNetAdvanceAmount + Val(txtAdvCGST.Text)

        txtAdvSGST.Text = VB6.Format(txtAdvSGST.Text, "0.00")
        mNetAdvanceAmount = mNetAdvanceAmount + Val(txtAdvSGST.Text)

        txtAdvIGST.Text = VB6.Format(txtAdvIGST.Text, "0.00")
        mNetAdvanceAmount = mNetAdvanceAmount + Val(txtAdvIGST.Text)

        txtAdvAdjust.Text = VB6.Format(mNetAdvanceAmount, "0.00")


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Sub
    Private Sub txtAdvBal_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvBal.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvBal_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvBal.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtAdvAdjust_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvAdjust.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvAdjust_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvAdjust.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAdvCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvCGST.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvCGST.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAdvCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvCGST.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAdvCGSTBal_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvCGSTBal.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvCGSTBal_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvCGSTBal.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvSGSTBal_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvSGSTBal.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvSGSTBal_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvSGSTBal.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvIGSTBal_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvIGSTBal.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvIGSTBal_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvIGSTBal.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAdvIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvIGST.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtAdvSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvSGST.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvSGST.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAdvIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvIGST.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvIGST.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtAdvDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAdvSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvSGST.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtAdvVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvVNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SearchAdvanceVNo()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mDivisionCode As Double
        Dim xSupplierCode As Double
        Dim mVNO As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSupplierCode = MasterNo
        End If

        mVNO = ""

        If Val(CStr(Val(txtBillNo.Text))) > 0 Then
            mVNO = Trim(Trim(txtBillNoPrefix.Text) & VB6.Format(Val(txtBillNo.Text), ConBillFormat) & Trim(txtBillNoSuffix.Text))
        End If

        ''            & " AND DIV_CODE = " & mDivisionCode & "" & vbCrLf _							
        '							
        SqlStr = " SELECT VNO, VDATE, SUM(NETVALUE) AS NETVALUE FROM ("

        SqlStr = SqlStr & vbCrLf & " SELECT VNO, VDATE, SUM(NETVALUE) AS NETVALUE " & vbCrLf & " FROM FIN_ADVANCE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xSupplierCode & "' AND BOOKTYPE='AR'" & vbCrLf & " AND VDATE <= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY VNO, VDATE"

        SqlStr = SqlStr & vbCrLf & " UNION "

        SqlStr = SqlStr & vbCrLf & " SELECT ADV_VNO AS VNO, ADV_VDATE AS VDATE, SUM(ADV_ADJUSTED_AMT*-1) AS ADV_ADJUSTED_AMT " & vbCrLf & " FROM FIN_INVOICE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xSupplierCode & "'" & vbCrLf & " AND INVOICE_DATE <= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If mVNO <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND FYEAR || BILLNO <> " & RsCompany.Fields("FYEAR").Value & " || '" & mVNO & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY ADV_VNO, ADV_VDATE HAVING SUM(ADV_ADJUSTED_AMT)<>0"

        SqlStr = SqlStr & vbCrLf & ") GROUP BY VNO, VDATE HAVING SUM(NETVALUE)>0"

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtAdvVNo.Text = AcName
            txtAdvVNo_Validating(txtAdvVNo, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtAdvVNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvVNo.DoubleClick
        Call SearchAdvanceVNo()
    End Sub

    Private Sub txtAdvVNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAdvVNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAdvanceVNo()
    End Sub


    Private Sub txtAdvVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mDivisionCode As Double

        If txtAdvVNo.Text = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        ''AND DIV_CODE = " & mDivisionCode & "							

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND BOOKTYPE='AR'"

        If MainClass.ValidateWithMasterTable((txtAdvVNo.Text), "VNO", "VDATE", "FIN_ADVANCE_HDR", PubDBCn, MasterNo, , SqlStr) = True Then
            txtAdvDate.Text = VB6.Format(MasterNo, "DD/MM/YYYY")
        Else
            MsgInformation("No Such Advance Voucher")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txteRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteRefNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txteRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txteRefNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txteRefNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPortCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPortCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPortCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPortCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPortCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtShippedTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShippedTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtShippedTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShippedTo.DoubleClick
        cmdSearchShippedTo_Click(cmdSearchShippedTo, New System.EventArgs())
    End Sub


    Private Sub txtShippedTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtShippedTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtShippedTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtShippedTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtShippedTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchShippedTo_Click(cmdSearchShippedTo, New System.EventArgs())
    End Sub

    Private Sub txtShippedTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShippedTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String


        If Trim(txtShippedTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgBox("Invalid Shipped to Supplier Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchShippedTo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchShippedTo.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        If ADDMode = True Then
            SqlStr = SqlStr & " AND STATUS='O'"
        End If
        If MainClass.SearchGridMaster((txtShippedTo.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtShippedTo.Text = AcName
            txtShippedTo_Validating(txtShippedTo, New System.ComponentModel.CancelEventArgs(True))
            '        If TxtRemarks.Enabled = True Then TxtRemarks.SetFocus						
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub chkStockTrf_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStockTrf.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkTaxOnMRP_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTaxOnMRP.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots("N")
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            pShowCalc = True
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtBillNo.Enabled = False
            txtDCNo.Enabled = True
            CmdSearchDC.Enabled = True
            cboInvType.Enabled = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101, False, True)
        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdBarCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBarCode.Click
        On Error GoTo ErrPart

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        'If InStr(1, pBARCODEFORMAT1, mCustomerCode, CompareMethod.Text) >= 1 Then
        '    ''HERO HONDA BARCODE.........						
        '    Call PrintBarcode1("", False)
        '    Exit Sub
        'End If

        'If InStr(1, pBARCODEFORMAT2, mCustomerCode, CompareMethod.Text) >= 1 Then
        '    ''TVS BARCODE.........						
        '    Call PrintBarcode2("", False)
        '    Exit Sub
        'End If

        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub PrintMRPBarcode()

        On Error GoTo ErrPart
        Dim cntRow As Integer


        Dim mBillNo As String
        Dim mBillDate As String

        Dim mPartNo As String
        Dim mDescription As String
        Dim mQty As Double
        Dim mPQtyStr As String
        Dim mPQty As Integer
        Dim mMRP As Double
        Dim mPktDate As String
        Dim mString As String
        Dim mSeparator As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pMRPPrint As String

        ''HERO HONDA BARCODE.........							

        pMRPPrint = InputBox("Press 'Y' for With MRP and 'N' Without MRP Print :", "MRP Print", "Y")

        mSeparator = vbTab
        mString = ""
        If Trim(txtBillNo.Text) <> "" Then
            mBillNo = VB6.Format(Trim(txtBillNo.Text), ConBillFormat)
        Else
            mBillNo = " "
        End If

        If IsDate(txtBillDate.Text) Then
            mBillDate = VB6.Format(txtBillDate.Text, "dd.mm.yyyy")
        Else
            mBillDate = " "
        End If

        mString = mBillNo & mSeparator & mBillDate


        ''If chkMRP.Value = vbUnchecked Then							

        SqlStr = "SELECT ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, " & vbCrLf & " ID.CUSTOMER_PART_NO, ID.ITEM_QTY, IMST.ITEM_STD_COST " & vbCrLf & " FROM FIN_INVOICE_DET ID, INV_ITEM_MST IMST " & vbCrLf & " WHERE ID.MKEY='" & LblMKey.Text & "'" & vbCrLf & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf & " ORDER BY SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            With SprdMain
                Do While Not RsTemp.EOF
                    mPartNo = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)
                    mDescription = IIf(IsDBNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)
                    mQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0"))
                    mMRP = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_STD_COST").Value), 0, RsTemp.Fields("ITEM_STD_COST").Value), "0.00"))
                    mPktDate = VB6.Format(txtBillDate.Text, "dd/mm/yyyy")

                    mPQtyStr = InputBox("Please Enter No of Stricker :", "Stricker", CStr(mQty))
                    mPQty = Val(mPQtyStr)

                    '                For cntRow = 1 To mPQty				
                    'Call Print2DMRPBarcode(mString, mPartNo, mDescription, mPktDate, VB6.Format(mMRP, "0.00"), pMRPPrint, mPQty, MSComm1) 'IIf(chkMRP.Value = vbUnchecked, "N", "Y")				
                    '                Next				
                    RsTemp.MoveNext()
                Loop
            End With
        End If


        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Function GetItemSNo(ByRef pItemCode As String) As Object

        On Error GoTo ErrPart
        Dim mCustomerCode As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetItemSNo = ""

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        SqlStr = "SELECT ITEM_SNO" & vbCrLf & " FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND IH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf & " AND IH.MKEY = ("

        SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf & " AND SID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND SIH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf & " AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetItemSNo = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SNO").Value), "", RsTemp.Fields("ITEM_SNO").Value))
        End If

        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Function

    Private Function GetVendorCode() As String

        On Error GoTo ErrPart
        Dim mCustomerCode As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetVendorCode = ""

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        SqlStr = "SELECT VENDOR_CODE" & vbCrLf _
            & " FROM  DSP_SALEORDER_HDR IH" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf & " AND IH.MKEY = ("

        SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH" & vbCrLf _
            & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SIH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _
            & " AND SIH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf _
            & " AND SIH.AMEND_WEF_FROM <=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetVendorCode = Trim(IIf(IsDBNull(RsTemp.Fields("VENDOR_CODE").Value), "", RsTemp.Fields("VENDOR_CODE").Value))
        End If

        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Function

    Private Sub PrintBarcode2(ByRef pString As String, ByRef GetStrOnly As Boolean)
        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mPONo As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mEDAmount As String = ""
        Dim mCWBNo As String
        Dim mTransportCode As String
        Dim mItemNo As String
        Dim mPartNo As String
        Dim mQty As String
        Dim mConCode As String
        Dim mNoofCon As String
        Dim mJITCall As String
        Dim mLocCode As String
        Dim mItemCode As String

        Dim mString As String
        Dim mSeparator As Object

        mSeparator = vbTab 'Chr(vbKeyReturn)							

        If Trim(txtPONo.Text) <> "" Then
            mPONo = Trim(txtPONo.Text)
        Else
            mPONo = " "
        End If

        If Trim(txtBillNo.Text) <> "" Then
            mBillNo = VB6.Format(Trim(txtBillNo.Text), ConBillFormat)
        Else
            mBillNo = " "
        End If

        If IsDate(txtBillDate.Text) Then
            mBillDate = VB6.Format(txtBillDate.Text, "dd.mm.yyyy")
        Else
            mBillDate = " "
        End If

        '    If Val(lblTotED.text) <> 0 Then							
        '        mEDAmount = Val(lblTotED.text)							
        '    Else							
        '        mEDAmount = " "							
        '    End If							

        mCWBNo = New String(" ", 16)
        mTransportCode = New String(" ", 10)

        mString = mPONo & mSeparator & mBillNo & mSeparator & mBillDate & mSeparator & mEDAmount & mSeparator & mCWBNo & mSeparator & mTransportCode & mSeparator

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                '            mItemNo = cntRow					
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "IDENT_MARK", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemNo = MasterNo
                Else
                    mItemNo = " "
                End If

                mString = mString & mSeparator & mItemNo

                .Col = ColPartNo
                mPartNo = Trim(.Text)
                If Trim(mPartNo) <> "" Then
                    mString = mString & mSeparator & mPartNo
                Else
                    mString = mString & mSeparator & " "
                End If

                .Col = ColQty
                mQty = Trim(.Text)
                If Trim(mQty) <> "" Then
                    mString = mString & mSeparator & mQty
                Else
                    mString = mString & mSeparator & "0"
                End If

                mConCode = " "
                mNoofCon = " "
                mLocCode = " "
                mString = mString & mSeparator & mConCode
                mString = mString & mSeparator & mNoofCon

                .Col = ColJITCallNo
                mJITCall = CStr(Val(.Text))
                If Val(mJITCall) > 0 Then
                    mString = mString & mSeparator & mJITCall
                Else
                    mString = mString & mSeparator & "0"
                End If

                '            mString = mString & mSeparator & mJITCall					
                mString = mString & mSeparator & mLocCode
            Next
        End With

        mString = mString & mSeparator

        Dim mFP As Boolean
        'If pBARCODEPRINTER = "Y" Then							
        '	Call Print2DBarcode(mString, "Bill No : " & Trim(mBillNo), MSComm1)						
        'Else							
        '	If CreateOutPutFile(mString, "PDF.DAT") = False Then GoTo ErrPart						

        '	mString = vbTab & vbTab & vbTab & "Bill No : " & Trim(mBillNo) & vbNewLine & vbNewLine & " "						
        '	If CreateOutPutFile(mString, "Inv.Prn") = False Then GoTo ErrPart						
        '	'    Shell mLocalPath & "\PDF.bat",vbNormalFocus						
        '	mFP = Shell(mLocalPath & "\PDF.bat", AppWinStyle.NormalFocus)						
        '	'    mFP = Shell(App.path & "\PDF.bat", vbNormalFocus)						
        'End If							
        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        '    Resume							
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        FrmInvoiceViewer.Hide()
        FrmInvoiceViewer.Dispose()
        FrmInvoiceViewer.Close()
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub


    Private Sub ReportonShow(ByRef Mode As Crystal.DestinationConstants)
        'Dim Printer As New Printer							

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mCT3Date As String
        'Dim prt As Printer							

        Report1.Reset()
        mTitle = ""
        mSubTitle = ""

        SqlStr = MakeSQL()
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\FormARE_3.RPT"

        Report1.SQLQuery = SqlStr
        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, , , "Y")

        MainClass.AssignCRptFormulas(Report1, "Range=""" & IIf(IsDBNull(RsCompany.Fields("EXCISE_RANGE").Value), "", RsCompany.Fields("EXCISE_RANGE").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "Division=""" & IIf(IsDBNull(RsCompany.Fields("EXCISE_DIV").Value), "", RsCompany.Fields("EXCISE_DIV").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "RegnNo=""" & IIf(IsDBNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "Place=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & """")

        '    mCT3Date = GetCT3Date(PubDBCn, Val(TxtCTNo.Text), "", "S", mCustomerCode)							

        '    MainClass.AssignCRptFormulas Report1, "CT3Date=""" & mCT3Date & """"							

        Report1.WindowShowGroupTree = False

        'If PubUniversalPrinter = "Y" And Mode = Crystal.DestinationConstants.crptToPrinter Then							

        '	For	Each prt In Printers					
        '		If UCase(prt.DeviceName) = UCase("Universal Printer") Then					
        '			Printer = prt				

        '			Report1.PrinterName = prt.DeviceName				
        '			Report1.PrinterDriver = prt.DriverName				
        '			Report1.PrinterPort = prt.Port				
        '			'Report1.PrintFileName = "D:\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".pdf"				
        '			Exit For				
        '		End If					
        '	Next prt						
        '	''						
        'End If							

        Report1.Action = 1

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume							
    End Sub
    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String

        ''SELECT CLAUSE...							


        MakeSQL = " SELECT IH.*,ID.*, CMST.*, ITEMMST.* "


        ''FROM CLAUSE...							
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST ITEMMST"

        ''WHERE CLAUSE...							
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND ID.COMPANY_CODE=ITEMMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=ITEMMST.ITEM_CODE " & vbCrLf & " AND IH.MKEY='" & MainClass.AllowSingleQuote((LblMKey.Text)) & "'"


        MakeSQL = MakeSQL & vbCrLf & "ORDER BY SUBROWNO "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click

        On Error GoTo DelErrPart
        Dim mDeleteRights As String
        Dim xDCNo As String

        If Val(lblCompanyCode.Text) > 0 Then
            If Val(lblCompanyCode.Text) <> RsCompany.Fields("Company_Code").Value Then
                MsgInformation("Cann't be Delete Another Unit Voucher.")
                Exit Sub
            End If
        End If
        If ValidateBranchLocking((txtBillDate.Text)) = True Then
            Exit Sub
        End If
        If ValidateBookLocking(PubDBCn, CInt(ConLockSale), txtBillDate.Text) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, txtBillDate.Text, (txtCustomer.Text), mCustomerCode) = True Then
            Exit Sub
        End If

        If Trim(txtBillNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        'If MainClass.GetUserCanModify((txtBillDate.Text)) = False Then
        '    MsgBox("You Have Not Rights to Delete back Voucher", MsgBoxStyle.Information)
        '    Exit Sub
        'End If

        'mDeleteRights = GetUserPermission("INVOICE_ADMIN", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)

        'If mDeleteRights = "N" Then
        '    MsgBox("You Have Not Rights to Delete Invoice.", MsgBoxStyle.Information)
        '    Exit Sub
        'End If

        If CheckBillPayment(mCustomerCode, (txtBillNo.Text), "B", (txtBillDate.Text)) = True Then Exit Sub

        If RsSaleMain.Fields("ISTCSPAID").Value = "Y" Then
            MsgInformation("TCS Challan made against this invoice So cann't be Deleted.")
            Exit Sub
        End If

        If Trim(txtIRNNo.Text) <> "" Then
            MsgInformation("IRN No Made against this invoice So cann't be Deleted.")
            Exit Sub
        End If

        If Not RsSaleMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.						
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_HDR", (LblMKey.Text), RsSaleMain, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_DET", (LblMKey.Text), RsSaleDetail, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_EXP", (LblMKey.Text), RsSaleExp, "MKEY", "D") = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "FIN_INVOICE_HDR", "MKEY", (LblMKey.Text)) = False Then GoTo DelErrPart

                '' & " AND DESP_DATE=TO_DATE('" & VB6.Format(txtDCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _

                SqlStr = "UPDATE DSP_DESPATCH_HDR SET DESP_STATUS=0 " & vbCrLf _
                    & " WHERE AUTO_KEY_DESP=" & Val(txtDCNo.Text) & " " & vbCrLf _
                    & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

                PubDBCn.Execute(SqlStr)

                If Val(txtDCNo.Text) > 0 And chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    SqlStr = "UPDATE FIN_DNCN_HDR SET ISDESPATCHED='N',SALEINVOICENO='',SALEINVOICEDATE=''," & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BOOKCODE='-4'" & vbCrLf & " AND MKEY IN (SELECT DISTINCT SONO " & vbCrLf & " FROM DSP_DESPATCH_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_DESP = " & Val(txtDCNo.Text) & " )" ''& vbCrLf |                        & " AND VDATE = '" & vb6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "'"				

                    PubDBCn.Execute(SqlStr)
                End If

                PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "' AND BOOKCODE='" & LblBookCode.Text & "'")
                PubDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' ")  ''AND BookSubType='" & mBookSubType & "'
                PubDBCn.Execute("Delete From FIN_CT_TRN Where Mkey='" & LblMKey.Text & "' AND BOOKTYPE='S' AND BOOKSUBTYPE='O'")

                PubDBCn.Execute("DELETE FROM DSP_PAINT57F4_TRN WHERE MKey='" & LblMKey.Text & "' AND BookType='P' AND BookSubType='O' AND TRNTYPE='S'")
                PubDBCn.Execute("Delete From FIN_RGDAILYMANU_HDR Where Mkey='" & LblMKey.Text & "' AND BOOKTYPE='S'")
                PubDBCn.Execute("Delete From TCS_TRN Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND Mkey='" & LblMKey.Text & "'")

                If UpdatePacking(VB6.Format(Val(txtBillNo.Text), ConBillFormat), (txtBillDate.Text), mCustomerCode, False) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete From FIN_TRADING_TRN Where Mkey='" & LblMKey.Text & "'")

                PubDBCn.Execute("Delete from FIN_INVOICE_EXP Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("Delete from FIN_INVOICE_DET Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("Delete from FIN_INVOICE_HDR Where Mkey='" & LblMKey.Text & "'")


                PubDBCn.CommitTrans()
                RsSaleMain.Requery() ''.Refresh					
                RsSaleDetail.Requery() ''.Refresh					
                RsSaleExp.Requery() ''.Refresh					
                RsSaleTrading.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''							
        RsSaleMain.Requery() ''.Refresh							
        RsSaleDetail.Requery() ''.Refresh							
        RsSaleTrading.Requery()
        RsSaleExp.Requery() ''.Refresh							
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPRINTED As String

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Modified.")
            Exit Sub
        End If

        'If PubUserID <> "G0416" Then
        'If Trim(txtIRNNo.Text) <> "" Then
        '    MsgInformation("IRN No Made against this invoice So cann't be Modified.")
        '    Exit Sub
        'End If


        SqlStr = "SELECT PRINTED FROM FIN_INVOICE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND Mkey ='" & MainClass.AllowSingleQuote((LblMKey.Text)) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPRINTED = IIf(IsDBNull(RsTemp.Fields("PRINTED").Value), "N", RsTemp.Fields("PRINTED").Value)
            If mPRINTED = "Y" Then
                MsgInformation("Invoice Print Already taken so that you cann't be Modified.")
                Exit Sub
            End If
        End If
        'End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtDCNo.Enabled = False
            CmdSearchDC.Enabled = False
            txtDCDate.Enabled = False
            chkTaxOnMRP.Enabled = True
            txtAbatementPer.Enabled = True
            txtBillNo.Enabled = True '' IIf(PubSuperUser = "S", True, False)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
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
        Dim mPrintA4 As String
        Dim mPaperStyle As String
        Dim mPrintPaperSize As String

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Invoice Cann't be Print.")
            Exit Sub
        End If

        If CheckDespatchQty() = False Then
            MsgInformation("Despatch Qty Not Match with Invoice Qty. Cann't be Saved.")
            Exit Sub
        End If

        If lblDespRef.Text = "U" Then
            frmPrintInvoice.OptInvoiceAnnex.Enabled = True
            frmPrintInvoice.OptInvoiceAnnex.Visible = True
            frmPrintInvoice.optSubsidiaryChallan.Enabled = False
            frmPrintInvoice.optSubsidiaryChallan.Visible = False
            frmPrintInvoice.FraF4.Enabled = False
            frmPrintInvoice.FraF4.Visible = False
            frmPrintInvoice.ShowDialog()

            If G_PrintLedg = False Then
                frmPrintInvoice.Close()
                Exit Sub
            Else
                mPrintOption = IIf(frmPrintInvoice.OptInvoiceAnnex.Checked = True, "YA", "YI") 'A-Annex & I-Invoice					
                If mPrintOption = "YA" Then
                    Call ReportonInvoiceAnnex(Crystal.DestinationConstants.crptToWindow)
                    Exit Sub
                End If
                frmPrintInvoice.Close()
            End If
        End If


        frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(4).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(3).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).Enabled = False

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



        If frmPrintInvCopy.optShow(3).Checked = True Or frmPrintInvCopy.optShow(4).Checked = True Then



            Call ReportOnPackingSlip(Crystal.DestinationConstants.crptToWindow, IIf(frmPrintInvCopy.optShow(3).Checked = True, "I", "O"))
            frmPrintInvCopy.Dispose()
            frmPrintInvCopy.Close()
            Exit Sub
        End If

        mPaperStyle = IIf(frmPrintInvCopy.optPrintPortrait.Checked, "P", "L")
        mPrintPaperSize = IIf(frmPrintInvCopy.optA4.Checked, "Y", "N")

        Dim mPrePrint As String = "N"
        If mPrintPaperSize = "N" Then
            mPrePrint = IIf(frmPrintInvCopy.chkPrePrint.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        Else
            mPrePrint = "N"
        End If

        '    For CntCount = 0 To 5							
        If frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Checked Then
            If RsCompany.Fields("E_INVOICE_APP").Value = "Y" And (CDbl(lblInvoiceSeq.Text) = 1 Or CDbl(lblInvoiceSeq.Text) = 2 Or CDbl(lblInvoiceSeq.Text) = 6 Or CDbl(lblInvoiceSeq.Text) = 9) Then
                If Trim(txtIRNNo.Text) = "" Then

                    Dim mGSTRegd As String = "N"
                    If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mGSTRegd = MasterNo
                    End If

                    If mGSTRegd <> "N" Then
                        MsgInformation("You have not generated IRN. Please generate the IRN.")
                        Exit Sub
                    End If


                End If
            End If
        End If
        '            mInvoicePrintType = UCase(frmPrintInvCopy.chkPrintOption(CntCount).text)							
        Call ReportOnSales(Crystal.DestinationConstants.crptToWindow, mInvoicePrintType, "N", mPrintOption, mPaperStyle, mPrintPaperSize, mPrePrint)
        '        End If							
        '    Next							

        frmPrintInvCopy.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ErrPart:
        frmPrintInvCopy.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub ReportOnSales(ByRef Mode As Crystal.DestinationConstants, ByRef mInvoicePrintType As String, ByRef pIsTradingInv As String, ByRef mPrintOption As String, ByRef mPaperStyle As String, ByRef mPrintPaperSize As String, mPrePrint As String)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNO As String
        Dim mWithInState As String
        Dim mRMCustomer As Boolean


        '    If chkCancelled.Value = vbChecked Then							
        '        MsgInformation "Cancelled Invoice Cann't be Print."							
        '        Exit Sub							
        '    End If							

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mWithInState = "N"
        mWithInState = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        'If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mWithInState = MasterNo
        'End If

        mRMCustomer = False
        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER='CUSTOMER-RM'") = True Then
            mRMCustomer = True
        End If

        SqlStr = ""
        mTitle = ""
        mSubTitle = ""

        Call SelectQryForPrint(SqlStr)
        'mPrintPaperSize = IIf(IsDBNull(RsCompany.Fields("INVOICE_A4").Value), "Y", RsCompany.Fields("INVOICE_A4").Value)


        If Val(lblInvoiceSeq.Text) = 3 Or Val(lblInvoiceSeq.Text) = 5 Then
            If Val(lblInvoiceSeq.Text) = 5 Then
                mTitle = IIf(Trim(mTitle) = "", "Internal Memo", mTitle)
            Else
                mTitle = IIf(Trim(mTitle) = "", "Delivery Challan for supply", mTitle)
            End If

            mSubTitle = "[See Section 143 of CGST Act, 2017 read with Rule 55 of CGST Rules]" ' "[See Rule 1 under Tax Invoice, Credit and Debit Note Rules]"						

            If mPrintOption = "YA" Then
                mRptFileName = "BOS_SUPP_ANNEX_GST.rpt"
            Else
                If Val(lblInvoiceSeq.Text) = 5 Then
                    mRptFileName = "BOS_SUPP_GST.rpt"
                Else
                    mRptFileName = "BOS_GST.rpt"
                End If
            End If

        Else
            If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "INV_HEADING", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                mTitle = MasterNo
            End If

            If Val(lblInvoiceSeq.Text) = 9 Or Val(lblInvoiceSeq.Text) = 7 Then
                mTitle = IIf(Trim(mTitle) = "", "Debit Note / Supplementary Invoice", mTitle)
                mSubTitle = "[See Rule 34 of CGST Act, 2017 read with Rule 53 of CGST Rules]"
            Else
                mTitle = IIf(Trim(mTitle) = "", "Tax Invoice", mTitle)
                mSubTitle = "[See Section 31 of CGST Act, 2017 read with Rule 46 of CGST Rules]"
            End If


            If mPrintOption = "YA" Then
                If mPrintPaperSize = "Y" Then
                    If mWithInState = "Y" Then
                        If frmPrintInvCopy._optShow_5.Checked = True Then
                            mRptFileName = "DeliverChallan_SGST.rpt"
                            mTitle = "Delivery Challan"
                        ElseIf frmPrintInvCopy._optShow_5.Checked = True Then
                            mRptFileName = "Commercial_SGST.rpt"
                            mTitle = "Commercial Invoice"
                        Else
                            mRptFileName = IIf(mPaperStyle = "P", "Invoice_SGST.rpt", "Invoice_SGST_L.rpt")
                        End If
                        'mRptFileName = IIf(mPaperStyle = "P", "Invoice_SGST.rpt", "Invoice_SGST_L.rpt")  '"Invoice_SGST.rpt" ''"Invoice_Supp_SGST.rpt"				
                    Else
                        If frmPrintInvCopy._optShow_5.Checked = True Then
                            mRptFileName = "DeliverChallan_IGST.rpt"
                            mTitle = "Delivery Challan"
                        ElseIf frmPrintInvCopy._optShow_5.Checked = True Then
                            mRptFileName = "Commercial_IGST.rpt"
                            mTitle = "Commercial Invoice"
                        Else
                            mRptFileName = IIf(mPaperStyle = "P", "Invoice_IGST.rpt", "Invoice_IGST_L.rpt")
                        End If
                        'mRptFileName = IIf(mPaperStyle = "P", "Invoice_IGST.rpt", "Invoice_IGST_L.rpt") ''"Invoice_IGST.rpt" ''"Invoice_Supp_IGST.rpt"				
                    End If
                Else
                    If mWithInState = "Y" Then
                        mRptFileName = IIf(mPaperStyle = "P", "Invoice_SGST_A3.rpt", "Invoice_SGST_L_A3.rpt")  '"Invoice_SGST.rpt" ''"Invoice_Supp_SGST.rpt"				
                    Else
                        mRptFileName = IIf(mPaperStyle = "P", "Invoice_IGST_A3.rpt", "Invoice_IGST_L_A3.rpt") ''"Invoice_IGST.rpt" ''"Invoice_Supp_IGST.rpt"				
                    End If
                End If

            Else

                If mWithInState = "Y" Then
                    If chkPrintByGroup.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mRptFileName = "Invoice_SGST_Grp"
                    Else
                        mRptFileName = IIf(mPaperStyle = "P", "Invoice_SGST", "Invoice_SGST_L") ''"Invoice_SGST.rpt"
                    End If
                Else
                    If CDbl(lblInvoiceSeq.Text) = 6 Or CDbl(lblInvoiceSeq.Text) = 7 Then
                        mRptFileName = "Invoice_EXP_IGST"
                    Else
                        If chkPrintByGroup.CheckState = System.Windows.Forms.CheckState.Checked Then
                            mRptFileName = "Invoice_IGST_GRP"
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
        End If

        'If mPrintPaperSize = "N" Then
        '    mRptFileName = mRptFileName & "_A3"
        'End If

        'mRptFileName = mRptFileName & ".rpt"

        Dim mPDFPrint As Boolean = False
        If frmPrintInvCopy.optShow(0).Checked = True Then     ''mPDF
            mPDFPrint = False
        Else
            mPDFPrint = True
        End If

        Call ShowExcisePDFReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, True, mInvoicePrintType, mPDFPrint, mPrePrint)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub


    Private Function SelectQryForPrint(ByRef mSqlStr As String) As String

        Dim mCustomerCode As String = ""
        Dim pBarCodeString As String = ""
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mInvoicePrintType As String
        Dim CntCount As Integer
        Dim mUpdateStart As Boolean

        On Error GoTo ErrPart

        mUpdateStart = True
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_BARCODE_PRINT WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        pBarCodeString = ""

        ''HERO HONDA BARCODE.........		
        If lblDespRef.Text = "U" Then
        Else
            If InStr(1, pBARCODEFORMAT1, Trim(mCustomerCode), CompareMethod.Text) >= 1 Then
                Call PrintBarcode1(pBarCodeString, LblMKey.Text, "N", True)
            End If
        End If


        '    ''TVS BARCODE.........							
        '    If InStr(1, pBARCODEFORMAT2, mCustomerCode, vbTextCompare) >= 1 Then							
        '        Call PrintBarcode2(pBarCodeString, True)							
        '        Exit Sub							
        '    End If							

        For CntCount = 0 To 5
            If frmPrintInvCopy.chkPrintOption(CntCount).CheckState = System.Windows.Forms.CheckState.Checked Then
                mInvoicePrintType = UCase(frmPrintInvCopy.chkPrintOption(CntCount).Text)
                SqlStr = "INSERT INTO TEMP_BARCODE_PRINT ( " & vbCrLf _
                    & " USER_ID, MKEY, BARCODE_VALUE, PRINT_INVOICE_TYPE,PRINT_SEQ  ) VALUES (" & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "','" & LblMKey.Text & "','" & pBarCodeString & "','" & mInvoicePrintType & "'," & CntCount & ")"

                PubDBCn.Execute(SqlStr)
            End If
        Next

        PubDBCn.CommitTrans()

        mUpdateStart = False

        mSqlStr = " SELECT * "

        ''FROM CLAUSE...							
        mSqlStr = mSqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST CBMST, GEN_COMPANY_MST GMST, DSP_DESPATCH_DET IDD,TEMP_BARCODE_PRINT BP "


        ''WHERE CLAUSE...							
        mSqlStr = mSqlStr & vbCrLf _
            & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.MKEY=BP.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=CBMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CBMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=CBMST.LOCATION_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY='" & LblMKey.Text & "'" & vbCrLf _
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
    Private Sub ShowExciseReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean, ByRef mInvoicePrintType As String, ByRef pIsPDF As String)
        'Dim Printer As New Printer

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim SqlStrSub As String
        Dim mDutyInword As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
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
        Dim mPayTerms As String = ""
        Dim mBalPayTerms As String = ""
        Dim mJurisdiction As String = ""
        Dim mShipToSameParty As String = ""
        Dim mShipToCode As String = ""

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
        Dim mBMPFileName As String
        Dim mEPCGNo As String
        Dim mEPCGDate As String
        Dim mFilePath As String
        Dim mCurrency As String
        Dim mRateTitle As String
        Dim mAmountTitle As String


        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, , , "Y")

        '    If PubUserID = "G0416" Then							
        '        mRptFileName = Left(mRptFileName, Len(mRptFileName) - 4) & "_E.rpt"							
        '    End If							

        mStateName = ""
        mStateCode = ""

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mStateName = MasterNo
            mStateCode = GetStateCode(mStateName)
        End If

        'If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mWithInState = MasterNo
        'End If

        mWithInState = GetPartyBusinessDetail(mCustomerCode, Trim(txtBillTo.Text), "WITHIN_STATE")

        If mWithInState = "N" Then
            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInCountry = MasterNo
            End If
        End If

        mPlaceofSupply = VB6.Format(mStateCode, "00") & "-" & mStateName ''IIf(mWithInState = "Y", "INTRA STATE", IIf(mWithInCountry = "Y", "INTER STATE", "EXPORT"))							


        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        ''Temp Lock							
        '    Report1.WindowShowPrintBtn = False '' IIf(PubSuperUser = "S", True, False)							
        '    Report1.WindowShowPrintSetupBtn = False ''IIf(PubSuperUser = "S", True, False)							
        ''    Report1.PrinterName = "Microsoft Print to PDF"							
        '    Report1.WindowShowExportBtn = IIf(PubSuperUser = "S", True, False)							

        Report1.WindowShowPrintBtn = True '' IIf(PubSuperUser = "S", True, False)							
        Report1.WindowShowPrintSetupBtn = True ''IIf(PubSuperUser = "S", True, False)							
        '    Report1.PrinterName = "Microsoft Print to PDF"							
        Report1.WindowShowExportBtn = True


        SqlStr = " SELECT NETVALUE, ITEMVALUE, NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT,INV_PREP_TIME, " & vbCrLf & " SHIPPED_TO_PARTY_CODE, REMOVAL_TIME, OUR_AUTO_KEY_SO, SHIPPED_TO_SAMEPARTY, " & vbCrLf & " IS_DESP_OTHERTHAN_BILL, SHIPPED_FROM_PARTY_CODE, IS_SHIPPTO_EX_WORK" & vbCrLf & " FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & MainClass.AllowSingleQuote((LblMKey.Text)) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If CDbl(lblInvoiceSeq.Text) = 3 Or CDbl(lblInvoiceSeq.Text) = 5 Then
                mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                mNetDuty = 0 ''IIf(IsNull(RsTemp!NETCGST_AMOUNT), 0, RsTemp!NETCGST_AMOUNT)					
            Else
                mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                mNetDuty = IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
                mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
                mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)
            End If

            mShipToSameParty = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
            mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
            mExWork = IIf(IsDBNull(RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value), "N", RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value)

            mPrepTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INV_PREP_TIME").Value), "", RsTemp.Fields("INV_PREP_TIME").Value), "HH:MM")
            mRemovalTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("REMOVAL_TIME").Value), "", RsTemp.Fields("REMOVAL_TIME").Value), "HH:MM")
            mSO = IIf(IsDBNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), "", RsTemp.Fields("OUR_AUTO_KEY_SO").Value)

            If mExWork = "Y" Then ''mShipToSameParty						
                mShipToName = "Ex Work"
                mShipToAddress = ""
                mShipToCity = ""
                mShipToGSTN = ""
                mShipToState = ""
                mShipToStateCode = ""
            Else
                SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipToCode) & "'"
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
                SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipFromCode) & "'"
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

                    MainClass.AssignCRptFormulas(Report1, "ShipFromName=""" & mShipFromName & """")
                    MainClass.AssignCRptFormulas(Report1, "ShipFromAddress=""" & mShipFromAddress & """")
                    MainClass.AssignCRptFormulas(Report1, "ShipFromCity=""" & mShipFromCity & """")
                    ''                MainClass.AssignCRptFormulas Report1, "ShipFromGSTN=""" & mShipFromGSTN & """"				

                    MainClass.AssignCRptFormulas(Report1, "ShipFromState=""" & mShipFromState & """")
                    ''                MainClass.AssignCRptFormulas Report1, "ShipFromStateCode=""" & mShipFromStateCode & """"				

                End If
            End If

        End If

        'If UCase(mRptFileName) = "INVOICE_SGST.RPT" Or UCase(mRptFileName) = "INVOICE_IGST.RPT" Or UCase(mRptFileName) = "INVOICE_SGST_L.RPT" Or UCase(mRptFileName) = "INVOICE_IGST_L.RPT" Then
        mEPCGNo = ""
        mEPCGDate = ""
        SqlStr = " SELECT EPCG_NO, EPCG_DATE  " & vbCrLf & " FROM DSP_SALEORDER_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_SO='" & MainClass.AllowSingleQuote(lblPoNo.Text) & "'" & vbCrLf & " AND CUST_PO_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "'" & vbCrLf & " AND CUST_PO_DATE=TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SO_STATUS='O'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempShip.EOF = False Then
            mEPCGNo = IIf(IsDBNull(RsTempShip.Fields("EPCG_NO").Value), "", RsTempShip.Fields("EPCG_NO").Value)
            mEPCGDate = VB6.Format(IIf(IsDBNull(RsTempShip.Fields("EPCG_DATE").Value), "", RsTempShip.Fields("EPCG_DATE").Value), "DD/MM/YYYY")
        End If

        If mEPCGNo <> "" Then
            mEPCGNo = "EPCG License No : " & mEPCGNo & " &  Date : " & mEPCGDate
        End If
        'AssignCRpt11Formulas(CrReport, "EPCGNo", "'" & mEPCGNo & "'")
        'MainClass.AssignCRptFormulas(Report1, "EPCGNo=""" & mEPCGNo & """")
        '        MainClass.AssignCRptFormulas Report1, "EPCGDate=""" & mEPCGDate & """"						
        'End If

        MainClass.AssignCRptFormulas(Report1, "InvoicePrintType=""" & mInvoicePrintType & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyCity=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")

        mJurisdiction = "All Disputes Subject to " & IIf(IsDBNull(RsCompany.Fields("JURISDICTION").Value), "", RsCompany.Fields("JURISDICTION").Value) & " Jurisdiction."

        MainClass.AssignCRptFormulas(Report1, "COMPANYTINNo=""" & IIf(IsDBNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")

        mCompanyeMail = IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", "e-mail : " & RsCompany.Fields("COMPANY_MAILID").Value)
        mCompanyWebSite = IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", "WebSite : " & RsCompany.Fields("WEBSITE").Value)
        mCompanyDetail = mCompanyeMail & ", " & mCompanyWebSite
        MainClass.AssignCRptFormulas(Report1, "COMPANYDETAIL=""" & mCompanyDetail & """")

        MainClass.AssignCRptFormulas(Report1, "PrepTime=""" & mPrepTime & """")
        MainClass.AssignCRptFormulas(Report1, "RemovalTime=""" & mRemovalTime & """")
        MainClass.AssignCRptFormulas(Report1, "JWRemarks=""" & mJWRemarks & """")
        MainClass.AssignCRptFormulas(Report1, "Jurisdiction=""" & mJurisdiction & """")
        MainClass.AssignCRptFormulas(Report1, "mShipToName=""" & mShipToName & """")
        MainClass.AssignCRptFormulas(Report1, "mShipToAddress=""" & mShipToAddress & """")
        MainClass.AssignCRptFormulas(Report1, "mShipToCity=""" & mShipToCity & """")
        MainClass.AssignCRptFormulas(Report1, "mShipToGSTN=""" & mShipToGSTN & """")

        MainClass.AssignCRptFormulas(Report1, "mShipToState=""" & mShipToState & """")
        MainClass.AssignCRptFormulas(Report1, "mShipToStateCode=""" & mShipToStateCode & """")

        MainClass.AssignCRptFormulas(Report1, "mStateName=""" & mStateName & """")
        MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")
        MainClass.AssignCRptFormulas(Report1, "mPlaceofSupply=""" & mPlaceofSupply & """")

        MainClass.AssignCRptFormulas(Report1, "mServiceName=""" & Trim(txtServProvided.Text) & """")

        '    mBMPFileName = "C:\Windows\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".bmp"							
        '    Set Report1.SelectionFormula         ''.picture1.FormattedPicture = LoadPicture(mBMPFileName)							


        If Val(lblInvoiceSeq.Text) = 6 Or Val(lblInvoiceSeq.Text) = 7 Then
            If chkLUT.CheckState = System.Windows.Forms.CheckState.Checked Then
                mLUT = GetLUT((txtBillDate.Text))
                mExpHeading = "SUPPLY MEANT FOR EXPORT ON PAYMENT OF IGST OR SUPPLY MEANT FOR EXPORT UNDER BOND OR LETTER OF UNDERTAKING WITHOUT PAYMENT OF IGST"
            Else
                mLUT = ""
                mExpHeading = ""
            End If

            mCurrency = ""
            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "CURRENCYNAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCurrency = MasterNo
            End If

            mRateTitle = "Rate (" & mCurrency & ")"
            mAmountTitle = "Amount (" & mCurrency & ")"

            MainClass.AssignCRptFormulas(Report1, "LUTNo=""" & mLUT & """")
            MainClass.AssignCRptFormulas(Report1, "ExpHeading=""" & mExpHeading & """")

            MainClass.AssignCRptFormulas(Report1, "RateTitle=""" & mRateTitle & """")
            MainClass.AssignCRptFormulas(Report1, "AmountTitle=""" & mAmountTitle & """")
        Else
            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER='100% EOU'") = True Then
                mLUT = GetLUT((txtBillDate.Text))
                mExpHeading = "SUPPLY MEANT FOR EXPORT ON PAYMENT OF IGST OR SUPPLY MEANT FOR EXPORT UNDER BOND OR LETTER OF UNDERTAKING WITHOUT PAYMENT OF IGST"
            Else
                mLUT = ""
                mExpHeading = ""
            End If
            MainClass.AssignCRptFormulas(Report1, "LUTNo=""" & mLUT & """")
            MainClass.AssignCRptFormulas(Report1, "ExpHeading=""" & mExpHeading & """")
        End If

        mPayTerms = ""


        If IsSubReport = True Then

            If mNetAmount = 0 Then
                mAmountInword = " Zero"
            Else
                mAmountInword = MainClass.RupeesConversion(mNetAmount)
            End If

            mDutyInword = MainClass.RupeesConversion(mNetDuty)

            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                MainClass.AssignCRptFormulas(Report1, "AmountInWord=""Rs. Zero""")
                MainClass.AssignCRptFormulas(Report1, "DutyInword=""Rs. Zero""")
                MainClass.AssignCRptFormulas(Report1, "NetAmount=""0.00""")
            Else
                MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")
                MainClass.AssignCRptFormulas(Report1, "NetAmount=""" & VB6.Format(mNetAmount, "0.00") & """")
                MainClass.AssignCRptFormulas(Report1, "DutyInword=""" & mDutyInword & """")
            End If

            SqlStrSub = " SELECT FIN_INVOICE_EXP.MKEY, FIN_INVOICE_EXP.SUBROWNO, FIN_INVOICE_EXP.EXPPERCENT, FIN_INVOICE_EXP.AMOUNT, FIN_INTERFACE_MST.COMPANY_CODE, FIN_INTERFACE_MST.NAME" & vbCrLf & " FROM FIN_INVOICE_EXP, FIN_INVOICE_HDR, FIN_INTERFACE_MST " & vbCrLf & " WHERE FIN_INVOICE_EXP.MKEY = FIN_INVOICE_HDR.MKEY AND FIN_INVOICE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf & " AND FIN_INVOICE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

            SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"

            SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

            '        Dim NumSubReports As Integer						
            '        Dim i As Integer						
            '        NumSubReports = Report1.GetNSubreports						
            '        For i = 0 To NumSubReports - 1						
            '          MsgBox Report1.GetNthSubreportName(i)						
            '        Next i						

            Report1.SubreportToChange = "PurExp" ''Report1.GetNthSubreportName(0)						
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub
            MainClass.AssignCRptFormulas(Report1, "JWSTRemarks=""" & mJWSTRemarks & """")
            '          Report1.SubreportToChange = ""						
        End If

        '    If CDate(Format(txtBillDate.Text, "DD/MM/YYYY")) >= CDate("01/10/2020") Then							
        '        If RsCompany!E_INVOICE_APP = "Y" Or PubUserID = "EINV" Then							
        '            If Trim(txtIRNNo.Text) <> "" Then							
        'SqlStrSub = "SELECT * FROM INVOICE_QRCODE WHERE MKEY='" & LblMKey.Text & "'"
        'Report1.SubreportToChange = "QRCode" ''Report1.GetNthSubreportName(1)							
        ''Report1.Connect = AccessRptConn
        'Report1.SQLQuery = SqlStrSub
        'Report1.SubreportToChange = ""
        '            End If							
        '        End If							
        '    End If							

        'Dim prt As Printer
        'If pIsPDF = "Y" Then
        '    mFilePath = mPubBarCodePath & "\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".pdf"
        '    For Each prt In Printers
        '        If UCase(prt.DeviceName) = UCase("Microsoft Print to PDF") Or UCase(prt.DeviceName) = UCase("PDFPrinter") Then
        '            Printer = prt

        '            Report1.PrinterName = prt.DeviceName
        '            Report1.PrinterDriver = prt.DriverName
        '            Report1.PrinterPort = prt.Port
        '            '            Report1.PrintFileName = mFilePath           ''"D:\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".pdf"				
        '            Exit For
        '        End If
        '    Next prt

        '    '        Report1.PrintFileName = mFilePath           ''"D:\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".pdf"						

        '    ''For Digital Sign						
        'If PubUserID = "G0416" Then
        '    Dim pOutPutFileName As String = ""
        '    If WebRequestGenerateDigitalSignTest("D:\test_DS.pdf", pOutPutFileName) = False Then Exit Sub
        'End If
        'ElseIf PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then

        '    For Each prt In Printers
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then
        '            Printer = prt

        '            Report1.PrinterName = prt.DeviceName
        '            Report1.PrinterDriver = prt.DriverName
        '            Report1.PrinterPort = prt.Port
        '            'Report1.PrintFileName = "D:\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".pdf"				
        '            Exit For
        '        End If
        '    Next prt
        '    ''						
        'End If

        Report1.Action = 1
        Report1.ReportFileName = ""
        Report1.Reset()

        '    If mFilePath <> "" Then							
        '        ShellExecute Me.hwnd, "open", mFilePath, vbNullString, vbNullString, SW_SHOWNORMAL							
        '    End If							

        Exit Sub
ErrPart:
        'Resume							
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowExcisePDFReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean, ByRef mInvoicePrintType As String, ByVal mPDF As Boolean, mPrePrint As String)

        On Error GoTo ErrPart
        Dim RsTemp As New ADODB.Recordset
        Dim RS As New ADODB.Recordset

        Dim fPath As String
        Dim mBillNoStr As String

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
        Dim mShipToPhoneNo As String
        Dim mShipToMailID As String

        Dim CrReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument  ' Report Name 
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions

        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions

        Dim mEPCGNo As String
        Dim mEPCGDate As String
        Dim mShipToPIN As String
        Dim xRPTFileName As String
        Dim mInterUnit As String

        Dim mStoreName As String = ""
        Dim mStoreAddress As String = ""
        Dim mStoreCity As String = ""
        Dim mStoreState As String = ""
        Dim mStoreGSTN As String = ""

        Dim mApplicantName As String = ""
        Dim mApplicantAddress As String = ""
        Dim mApplicantCity As String = ""
        Dim mApplicantState As String = ""
        Dim mApplicantGSTN As String = ""
        Dim mPaymentTerms As String

        xRPTFileName = "PDF_" & mRptFileName
        mRptFileName = PubReportFolderPath & "PDF_" & mRptFileName
        'mRptFileName = "G:\VBDotNetERP_Blank\Form\bin\Debug\Reports\PDF_Invoice_SGSTNew.rpt"



        CrReport.Load(mRptFileName)

        SqlStrSub = " SELECT * " & vbCrLf _
            & " FROM FIN_INVOICE_EXP, FIN_INVOICE_HDR, FIN_INTERFACE_MST " & vbCrLf _
            & " WHERE FIN_INVOICE_EXP.MKEY = FIN_INVOICE_HDR.MKEY " & vbCrLf _
            & " AND FIN_INVOICE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _
            & " AND FIN_INVOICE_HDR.COMPANY_CODE = FIN_INTERFACE_MST.COMPANY_CODE" & vbCrLf _
            & " And FIN_INVOICE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf _
            & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

        SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"

        SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

        Call Connect_MainReport_To_Database_11(CrReport)        '', mSqlStr
        Call Connect_SubReport_To_Database_11(CrReport, "PurExp")      '

        CrReport.RecordSelectionFormula = "{IH.COMPANY_CODE} = " & RsCompany.Fields("COMPANY_CODE").Value & " AND {IH.MKEY} = '" & MainClass.AllowSingleQuote(LblMKey.Text) & "' AND {IH.FYEAR} = '" & RsCompany.Fields("FYEAR").Value & "' AND {BP.USER_ID} = '" & MainClass.AllowSingleQuote(PubUserID) & "'"

        ClearCRpt11Formulas(CrReport)
        CrReport.ReportOptions.EnableSaveDataWithReport = False
        SetCompanyReport11(CrReport, 1, mTitle, mSubTitle)
        'CrReport.VerifyDatabase()   .VerifyOnEveryPrint = True '' blnVerifyOnEveryPrint		
        CrReport.Refresh()

        mStateName = ""
        mStateCode = ""

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        mStateName = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(txtBillTo.Text), "SUPP_CUST_STATE")
        mStateCode = GetStateCode(mStateName)

        mWithInState = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(txtBillTo.Text), "WITHIN_STATE")

        'If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mStateName = MasterNo
        '    mStateCode = GetStateCode(mStateName)
        'End If

        'If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mWithInState = MasterNo
        'End If

        If mWithInState = "N" Then
            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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

        SqlStr = " SELECT NETVALUE, ITEMVALUE, NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT,INV_PREP_TIME, " & vbCrLf & " SHIPPED_TO_PARTY_CODE, REMOVAL_TIME, OUR_AUTO_KEY_SO, SHIPPED_TO_SAMEPARTY, " & vbCrLf & " IS_DESP_OTHERTHAN_BILL, SHIPPED_FROM_PARTY_CODE, IS_SHIPPTO_EX_WORK" & vbCrLf & " FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If CDbl(lblInvoiceSeq.Text) = 3 Or CDbl(lblInvoiceSeq.Text) = 5 Then
                mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                mNetDuty = 0 ''IIf(IsNull(RsTemp!NETCGST_AMOUNT), 0, RsTemp!NETCGST_AMOUNT)					
            Else
                mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                mNetDuty = IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
                mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
                mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)
            End If

            mShipToSameParty = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
            mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
            mExWork = IIf(IsDBNull(RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value), "N", RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value)

            mPrepTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INV_PREP_TIME").Value), "", RsTemp.Fields("INV_PREP_TIME").Value), "HH:MM")
            mRemovalTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("REMOVAL_TIME").Value), "", RsTemp.Fields("REMOVAL_TIME").Value), "HH:MM")
            mSO = IIf(IsDBNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), "", RsTemp.Fields("OUR_AUTO_KEY_SO").Value)

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
                    mShipLocation = Trim(txtBillTo.Text)
                Else
                    mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
                    mShipLocation = Trim(TxtShipTo.Text)
                End If
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

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                        mShipToCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                    Else
                        mShipToCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                        mShipToCity = mShipToCity & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
                    End If

                    mShipToPIN = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
                    mShipToState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                    mShipToStateCode = GetStateCode(mShipToState)
                    mShipToGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)

                    mShipToPAN = ""

                    If MainClass.ValidateWithMasterTable(mShipToName, "SUPP_CUST_NAME", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mShipToPAN = MasterNo
                    End If

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
                SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipFromCode) & "'"
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
        Dim mCompanyStateCode As String = GetStateCode(IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & "")
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
        AssignCRpt11Formulas(CrReport, "mServiceName", "'" & Trim(txtServProvided.Text) & "'")

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            AssignCRpt11Formulas(CrReport, "mShipToPIN", "'" & mShipToPIN & "'")
            AssignCRpt11Formulas(CrReport, "mShipToPhoneNo", "'" & mShipToPhoneNo & "'")
            AssignCRpt11Formulas(CrReport, "mShipToMailID", "'" & mShipToMailID & "'")

            If UCase(Mid(mRptFileName, Len(mRptFileName) - 6)) = "_A3.RPT" Then
                AssignCRpt11Formulas(CrReport, "PrePrint", "'" & mPrePrint & "'")
            End If
        End If

        ''

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
            If txtStoreDetail.Text <> "" Then
                SqlStr = "SELECT * FROM FIN_SUPP_CUST_MST " & vbCrLf _
                   & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtStoreDetail.Text) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTempShip.EOF = False Then
                    mStoreName = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_NAME").Value), "", RsTempShip.Fields("SUPP_CUST_NAME").Value)
                    mStoreAddress = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_ADDR").Value), "", RsTempShip.Fields("SUPP_CUST_ADDR").Value)
                    mStoreAddress = Replace(mStoreAddress, vbCrLf, "")

                    mStoreCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                    mStoreCity = mStoreCity & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)

                    mStoreState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                    mStoreGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)
                    'mStorePANno = IIf(IsDBNull(RsTempShip.Fields("PAN_NO").Value), "", RsTempShip.Fields("PAN_NO").Value)
                    'mStorePhoneNo = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PHONE").Value), "", RsTempShip.Fields("SUPP_CUST_PHONE").Value) ' , 
                    'mStoreMailID = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_MAILID").Value), "", RsTempShip.Fields("SUPP_CUST_MAILID").Value)
                End If
            End If

            MainClass.AssignCRptFormulas(Report1, "mStoreName=""" & mStoreName & """")
            MainClass.AssignCRptFormulas(Report1, "mStoreAddress=""" & mStoreAddress & """")
            MainClass.AssignCRptFormulas(Report1, "mStoreCity=""" & mStoreCity & """")
            MainClass.AssignCRptFormulas(Report1, "mStoreState=""" & mStoreState & """")
            MainClass.AssignCRptFormulas(Report1, "mStoreGSTN=""" & mStoreGSTN & """")



            If txtApplicant.Text <> "" Then
                SqlStr = "SELECT * FROM FIN_SUPP_CUST_MST " & vbCrLf _
                   & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtApplicant.Text) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTempShip.EOF = False Then
                    mApplicantName = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_NAME").Value), "", RsTempShip.Fields("SUPP_CUST_NAME").Value)
                    mApplicantAddress = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_ADDR").Value), "", RsTempShip.Fields("SUPP_CUST_ADDR").Value)
                    mApplicantAddress = Replace(mApplicantAddress, vbCrLf, "")

                    mApplicantCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                    mApplicantCity = mApplicantCity & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)

                    mApplicantState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                    mApplicantGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)
                    'mApplicantPANno = IIf(IsDBNull(RsTempShip.Fields("PAN_NO").Value), "", RsTempShip.Fields("PAN_NO").Value)
                    'mApplicantPhoneNo = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PHONE").Value), "", RsTempShip.Fields("SUPP_CUST_PHONE").Value) ' , 
                    'mApplicantMailID = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_MAILID").Value), "", RsTempShip.Fields("SUPP_CUST_MAILID").Value)
                End If
            End If


            MainClass.AssignCRptFormulas(Report1, "mApplicantName=""" & mApplicantName & """")
            MainClass.AssignCRptFormulas(Report1, "mApplicantAddress=""" & mApplicantAddress & """")
            MainClass.AssignCRptFormulas(Report1, "mApplicantCity=""" & mApplicantCity & """")
            MainClass.AssignCRptFormulas(Report1, "mApplicantState=""" & mApplicantState & """")
            MainClass.AssignCRptFormulas(Report1, "mApplicantGSTN=""" & mApplicantGSTN & """")


        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 103 Then
            Dim mBalancePayTerms As String = ""
            If MainClass.ValidateWithMasterTable((lblPoNo.Text), "AUTO_KEY_SO", "BALANCE_PAY_DTL", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O' AND SO_APPROVED='Y'") = True Then
                mBalancePayTerms = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If
            AssignCRpt11Formulas(CrReport, "payterms", "'" & Trim(mBalancePayTerms) & "'")
        End If


        If lblDespRef.Text = "P" And Mid(RsCompany.Fields("COMPANY_NAME").Value, 1, 3) = "KAY" Then
            Dim mSaleAgreementNo As String = ""
            Dim mSaleAgreementDate As String = ""

            If MainClass.ValidateWithMasterTable((lblPoNo.Text), "AUTO_KEY_SO", "SCHD_AGREEMENT_NO", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O' AND SO_APPROVED='Y'") = True Then
                mSaleAgreementNo = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If
            If MainClass.ValidateWithMasterTable((lblPoNo.Text), "AUTO_KEY_SO", "SCHD_AGREEMENT_DATE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O' AND SO_APPROVED='Y'") = True Then
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

        'If UCase(mRptFileName) = "PDF_INVOICE_SGST.RPT" Or UCase(mRptFileName) = "PDF_INVOICE_IGST.RPT" Or UCase(mRptFileName) = "PDF_INVOICE_SGST_L.RPT" Or UCase(mRptFileName) = "PDF_INVOICE_IGST_L.RPT" Then
        mEPCGNo = ""
        mEPCGDate = ""
        SqlStr = " SELECT EPCG_NO, EPCG_DATE  " & vbCrLf _
            & " FROM DSP_SALEORDER_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_SO='" & MainClass.AllowSingleQuote(lblPoNo.Text) & "'" & vbCrLf _
            & " AND CUST_PO_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "'" & vbCrLf _
            & " AND CUST_PO_DATE=TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SO_STATUS='O'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempShip.EOF = False Then
            mEPCGNo = IIf(IsDBNull(RsTempShip.Fields("EPCG_NO").Value), "", RsTempShip.Fields("EPCG_NO").Value)
            mEPCGDate = VB6.Format(IIf(IsDBNull(RsTempShip.Fields("EPCG_DATE").Value), "", RsTempShip.Fields("EPCG_DATE").Value), "DD/MM/YYYY")
        End If

        If mEPCGNo <> "" Then
            mEPCGNo = "EPCG License No : " & mEPCGNo & " &  Date : " & mEPCGDate
            AssignCRpt11Formulas(CrReport, "EPCGNo", "'" & mEPCGNo & "'")
        End If


        'End If

        If Val(lblInvoiceSeq.Text) = 6 Or Val(lblInvoiceSeq.Text) = 7 Then
            If chkLUT.CheckState = System.Windows.Forms.CheckState.Checked Then
                mLUT = GetLUT((txtBillDate.Text))
            Else
                mLUT = ""
            End If

            AssignCRpt11Formulas(CrReport, "LUTNo", "'" & mLUT & "'")
            mExpHeading = "SUPPLY MEANT FOR EXPORT ON PAYMENT OF IGST OR SUPPLY MEANT FOR EXPORT UNDER BOND OR LETTER OF UNDERTAKING WITHOUT PAYMENT OF IGST"
            'MainClass.AssignCRptFormulas(Report1, "ExpHeading=""" & mExpHeading & """")
        Else
            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER='100% EOU'") = True Then
                mLUT = GetLUT((txtBillDate.Text))
                mExpHeading = "SUPPLY MEANT FOR EXPORT ON PAYMENT OF IGST OR SUPPLY MEANT FOR EXPORT UNDER BOND OR LETTER OF UNDERTAKING WITHOUT PAYMENT OF IGST"
            Else
                mLUT = ""
                mExpHeading = ""
            End If
            MainClass.AssignCRptFormulas(Report1, "LUTNo=""" & mLUT & """")
        End If

        'mPayTerms = ""

        Dim mAccountPostingHead As String
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y" And (xRPTFileName = "PDF_Invoice_IGST.rpt" Or xRPTFileName = "PDF_Invoice_SGST.rpt") Then
            mAccountPostingHead = Trim(txtCreditAccount.Text)
            AssignCRpt11Formulas(CrReport, "AccountPostingHead", "'" & mAccountPostingHead & "'")
        End If

        If IsSubReport = True Then

            If mNetAmount = 0 Then
                mAmountInword = " Zero Only"
            Else
                mAmountInword = MainClass.RupeesConversion(mNetAmount) & " Only"
            End If

            mDutyInword = MainClass.RupeesConversion(mNetDuty) & " Only"

            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                AssignCRpt11Formulas(CrReport, "AmountInWord", "'Rs. Zero'")
                AssignCRpt11Formulas(CrReport, "DutyInword", "'Rs. Zero'")
                AssignCRpt11Formulas(CrReport, "NetAmount", "'0.00'")
            Else
                AssignCRpt11Formulas(CrReport, "AmountInWord", "'" & mAmountInword & "'")
                AssignCRpt11Formulas(CrReport, "NetAmount", "'" & VB6.Format(mNetAmount, "0.00") & "'")
                AssignCRpt11Formulas(CrReport, "DutyInword", "'" & mDutyInword & "'")
            End If
        End If


        Dim mBMPFileName As String = ""
        mBillNoStr = Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text)
        mBillNoStr = Replace(mBillNoStr, "/", "_")
        mBillNoStr = Replace(mBillNoStr, "\", "_")
        mBMPFileName = RefreshQRCode(LblMKey.Text, mBillNoStr, txtIRNNo.Text)

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

        Dim mVendorCode As String = ""
        If mPDF = True Then
            Dim pOutPutFileName As String = ""
            mBillNoStr = Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & Trim(txtBillNoSuffix.Text)
            mBillNoStr = Replace(mBillNoStr, "/", "_")
            mBillNoStr = Replace(mBillNoStr, "\", "_")

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                mVendorCode = IIf(txtVendorCode.Text = "", "TaxInvoice", "TaxInvoice" & "_" & mVendorCode)
                fPath = mPubBarCodePath & "\" & mVendorCode & "_" & mBillNoStr & "_" & VB6.Format(txtBillDate.Text, "DDMMYYYY") & ".pdf"

                mVendorCode = IIf(txtVendorCode.Text = "", "DS", mVendorCode)
                pOutPutFileName = mPubBarCodePath & "\" & mVendorCode & "_" & mBillNoStr & "_" & VB6.Format(txtBillDate.Text, "DDMMYYYY") & ".pdf"

                'fPath = mPubBarCodePath & "\TaxInvoice_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"
                'pOutPutFileName = mPubBarCodePath & "\TaxInvoice_DS_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"
            Else
                fPath = mPubBarCodePath & "\TaxInvoice_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"
                pOutPutFileName = mPubBarCodePath & "\TaxInvoice_DS_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"
            End If



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

            CrReport.Refresh()
            Application.DoEvents()
            Threading.Thread.Sleep(6000)

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
                mPrintDigitalSign = "Authorised Signatory" ''"For " & RsCompany.Fields("PRINT_COMPANY_NAME").Value  ''"Authorised Signatory"
                'mSignerName = GetDigitalSignName(PubUserID)
                'If mSignerName <> "" Then
                If SignPdf(fPath, pOutPutFileName, mPrintDigitalSign) = False Then Exit Sub

                If FILEExists(pOutPutFileName) Then
                    Process.Start("explorer.exe", pOutPutFileName)
                End If
                'End If
            End If
        Else
            If mMode = Crystal.DestinationConstants.crptToWindow Then



                FrmInvoiceViewer.CrystalReportViewer1.ReportSource = Nothing
                'FrmInvoiceViewer.CrystalReportViewer1.DataBindings()
                FrmInvoiceViewer.CrystalReportViewer1.ReportSource = CrReport

                Application.DoEvents()
                Threading.Thread.Sleep(6000)

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


                CrReport.Refresh()
                Application.DoEvents()
                Threading.Thread.Sleep(6000)
                CrReport.PrintToPrinter(1, False, 1, 99)
                Application.DoEvents()
                Threading.Thread.Sleep(6000)



                CrReport.Dispose()
            End If
        End If


        Exit Sub
ErrPart:
        'Resume		
        CrReport.Dispose()
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowBarCodeReport(ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        'Dim crx As CRAXDRT.Application							
        'Dim rpt As CRAXDRT.Report							
        Dim Sect As CRAXDRT.Section
        Dim bmp As System.Drawing.Image

        Dim crapp As New CRAXDRT.Application
        Dim RsTemp As New ADODB.Recordset
        Dim RS As New ADODB.Recordset
        Dim objRpt As CRAXDRT.Report
        Dim fPath As String
        Dim pLocalPath As String

        mRptFileName = PubReportFolderPath & mRptFileName

        objRpt = crapp.OpenReport(mRptFileName)

        '    Call Connect_Report_To_Database(objRpt, RS, SqlStr)							
        With objRpt
            Call ClearCRpt8Formulas(objRpt)
            .DiscardSavedData()
            '        .Database.SetDataSource RS						
            SetCrpteMail(objRpt, 1, mTitle, mSubTitle)
            .VerifyOnEveryPrint = True '' blnVerifyOnEveryPrint						
        End With

        Sect = objRpt.Sections(1)
        With Sect.ReportObjects

            pLocalPath = "D:\Barcode"

            '        Set .Item("BarCodeImage").FormattedPicture = LoadPicture(pLocalPath & "\OUTPUT.bmp")						
            .Item(1).FormattedPicture = System.Drawing.Image.FromFile(pLocalPath & "\OUTPUT.bmp")
            If .Item("adoFileName").Value <> "" Then
                bmp = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & .Item("adoFileName").Value)
                .Item("BarCodeImage").FormattedPicture = bmp
            End If
        End With

        fPath = mLocalPath & "\eBarCode" & ".pdf"

        With objRpt
            .ExportOptions.FormatType = CRAXDDRT.CRExportFormatType.crEFTPortableDocFormat
            .ExportOptions.DestinationType = CRAXDDRT.CRExportDestinationType.crEDTDiskFile
            .ExportOptions.DiskFileName = fPath
            .ExportOptions.PDFExportAllPages = True
            .Export(False)
        End With

        objRpt = Nothing


        '    ''mLocalPath & "\" & pFileName							
        '    mRptFileName = PubReportFolderPath & mRptFileName							
        '    Set rpt = crx.OpenReport(mRptFileName)							
        '    Set Sect = rpt.Sections("Page Header b")							
        '							
        '    With rpt							
        '        Call ClearCRpt8Formulas(rpt)							
        '        .DiscardSavedData							
        ''        .Database.SetDataSource RS							
        '        SetCrpteMail rpt, 1, mTitle, mSubTitle							
        '        .VerifyOnEveryPrint = True  '' blnVerifyOnEveryPrint							
        '    End With							
        '							
        '							
        '    With Sect.ReportObjects							
        '        Set .Item("BarCodeImage").FormattedPicture = LoadPicture(mLocalPath & "\OUTPUT.bmp")							
        '        If .Item("adoFileName").Value <> "" Then							
        '            Set bmp = LoadPicture(App.path & .Item("adoFileName").Value)							
        '            Set .Item("BarCodeImage").FormattedPicture = bmp							
        '        End If							
        '    End With							
        '							
        '    With rpt							
        '        .ExportOptions.FormatType = crEFTCrystalReport      '' = crEFTPortableDocFormat							
        '        .ExportOptions.DestinationType = crEDTApplication '' = crEDTDiskFile							
        ''        .ExportOptions.DiskFileName = fPath							
        '    '    .ExportOptions.PDFExportAllPages = True							
        '        .Export False							
        '    End With							
        '							
        '    Set rpt = Nothing							



        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        '    Resume							

        '							

    End Sub

    Private Sub ReportonInvoiceAnnex(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mReportPrint As Boolean
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mPartNo As String
        Dim mItemDesc As String
        Dim mRefNo As String
        Dim mUnit As String
        Dim mQty As Double
        Dim mRate As Double
        Dim mCustomerCode As String = ""
        Dim mOldBillNo As String
        Dim mOldBillDate As String = ""
        Dim pOldBillRate As Double
        Dim pNewSORate As Double
        Dim pDNRate As Double
        Dim pSuppBillRate As Double
        Dim mOldBillNoStr As String
        Dim mBillRateDiff As Double
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mItemSNo As String
        Dim mHSNCode As String
        Dim mBillNo As String = ""

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mBillNo = txtBillNoPrefix.Text & txtBillNo.Text
        SqlStr = ""

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        With SprdMain

            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColItemSNo
                mItemSNo = Trim(.Text)

                .Col = ColHSNCode
                mHSNCode = Trim(.Text)

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColPartNo
                mPartNo = Trim(.Text)

                .Col = ColItemDesc
                mItemDesc = Trim(.Text)

                .Col = Col57F4
                mOldBillNo = CStr(Val(.Text))

                .Col = ColUnit
                mUnit = Trim(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)

                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)

                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)

                .Col = ColCGSTAmount
                mCGSTAmount = Val(.Text)

                .Col = ColSGSTAmount
                mSGSTAmount = Val(.Text)

                .Col = ColIGSTAmount
                mIGSTAmount = Val(.Text)


                mBillRateDiff = GetBillRateDiff(mItemCode, mCustomerCode, mOldBillNo, mOldBillDate, pOldBillRate, pNewSORate, pDNRate, pSuppBillRate, "P", 0, 0, 0)
                mOldBillNoStr = ""

                If MainClass.ValidateWithMasterTable(mOldBillNo, "AUTO_KEY_INVOICE", "BILLNO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mCustomerCode & "'") = True Then
                    mOldBillNoStr = MasterNo
                End If

                SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf _
                    & " FIELD1,FIELD2,FIELD3, " & vbCrLf _
                    & " FIELD4,FIELD5,FIELD6,FIELD7,FIELD8, " & vbCrLf _
                    & " FIELD9,FIELD10,FIELD11,FIELD12,FIELD13,FIELD14,FIELD15,FIELD16,FIELD17,FIELD18,FIELD19,FIELD20,FIELD21,FIELD22,FIELD23,FIELD24,FIELD25) " & vbCrLf _
                    & " VALUES (" & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & cntRow & ",'','', '" & txtVendorCode.Text & "','" & txtPONo.Text & "', '" & mItemSNo & "'," & vbCrLf _
                    & " '" & mPartNo & "', '" & MainClass.AllowSingleQuote(mItemDesc) & "', '" & mHSNCode & "', '" & mOldBillNoStr & "', " & vbCrLf _
                    & " '" & mOldBillDate & "', '',  '" & mQty & "',   '" & pOldBillRate & "', '" & pNewSORate & "',  '" & mRate & "', " & vbCrLf _
                    & " '" & mQty * mRate & "', '" & mCGSTAmount & "', '" & mSGSTAmount & "', '" & mIGSTAmount & "',  " & vbCrLf _
                    & " '" & (mQty * mRate) + mCGSTAmount + mSGSTAmount + mIGSTAmount & "',  '" & mBillNo & "', '" & txtBillDate.Text & "'," & vbCrLf _
                    & " '" & mCGSTPer & "', '" & mSGSTPer & "', '" & mIGSTPer & "' " & vbCrLf _
                    & " ) "

                PubDBCn.Execute(SqlStr)
            Next
        End With
        ''VENDOR	Schedule Agreement No	Line Item No	Part No	Part Description	HSN No	Orginal Inv No	Orginal Inv Date	GRN NO	Quantity Invoiced	Old Settlement Value	New Settlement Value	Difference Amount	Diff Basic	Cgst @ 9% Or 14%	Sgst @ 9% Or 14%	Igst @ 18% Or 28%	Total Supp Inv Value	Debit Note/Supp Inv No	Supp Inv Date

        PubDBCn.CommitTrans()


        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mTitle = "Invoice Annex."
        mSubTitle = txtCustomer.Text & " Bill No : " & txtBillNoPrefix.Text & txtBillNo.Text & " Bill Date : " & VB6.Format(txtBillDate.Text, "DD/MM/YYYY")
        mRptFileName = "InvoiceSuppAnnex.rpt"

        Call MainClass.ClearCRptFormulas(Report1)
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)



        Exit Sub
ERR1:
        'Resume							
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        'Dim Printer As New Printer

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xSqlStr As String

        Dim mCustomerName As String
        Dim mCustomerAddress As String
        Dim mCustomerCity As String
        Dim mCustomerState As String
        Dim mCustomerStateCode As String
        Dim mCustomerGSTN As String
        Dim mJurisdiction As String

        Dim mCompanyeMail As String
        Dim mCompanyWebSite As String
        Dim mCompanyDetail As String


        Dim mStateName As String
        Dim mStateCode As String
        Dim mWithInState As String
        Dim mPlaceofSupply As String
        Dim mWithInCountry As String

        Dim mChallanNo As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mCompanyStateCode As String
        'Dim prt As Printer

        MainClass.ClearCRptFormulas(Report1)

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, , "N")


        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        'If PubUniversalPrinter = "Y" Then ''And mMode = crptToPrinter							

        '    For Each prt In Printers
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then
        '            Printer = prt

        '            Report1.PrinterName = prt.DeviceName
        '            Report1.PrinterDriver = prt.DriverName
        '            Report1.PrinterPort = prt.Port
        '            'Report1.PrintFileName = "D:\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".pdf"				
        '            Exit For
        '        End If
        '    Next prt
        '    ''						
        'End If

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowReportDC(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        'Dim Printer As New Printer

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xSqlStr As String

        Dim mCustomerName As String = ""
        Dim mCustomerAddress As String = ""
        Dim mCustomerCity As String = ""
        Dim mCustomerState As String = ""
        Dim mCustomerStateCode As String = ""
        Dim mCustomerGSTN As String = ""
        Dim mJurisdiction As String = ""

        Dim mCompanyeMail As String = ""
        Dim mCompanyWebSite As String = ""
        Dim mCompanyDetail As String = ""


        Dim mStateName As String
        Dim mStateCode As String
        Dim mWithInState As String = ""
        Dim mPlaceofSupply As String
        Dim mWithInCountry As String

        Dim mChallanNo As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mCompanyStateCode As String
        'Dim prt As Printer
        Dim xCustCode As String

        mStateName = ""
        mStateCode = ""

        MainClass.ClearCRptFormulas(Report1)

        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xCustCode = Trim(MasterNo)
            End If
        End If

        mStateName = GetPartyBusinessDetail(xCustCode, Trim(txtBillTo.Text), "SUPP_CUST_STATE")
        mStateCode = GetStateCode(mStateName)

        mWithInState = GetPartyBusinessDetail(xCustCode, Trim(txtBillTo.Text), "WITHIN_STATE")


        'If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mStateName = MasterNo
        '    mStateCode = GetStateCode(mStateName)
        'End If

        'If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mWithInState = MasterNo
        'End If

        If mWithInState = "N" Then
            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInCountry = MasterNo
            End If
        End If

        mPlaceofSupply = VB6.Format(mStateCode, "00") & "-" & mStateName ''IIf(mWithInState = "Y", "INTRA STATE", IIf(mWithInCountry = "Y", "INTER STATE", "EXPORT"))							


        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, , "N")


        xSqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf _
            & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"

        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mCustomerName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
            mCustomerAddress = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
            mCustomerAddress = Replace(mCustomerAddress, vbCrLf, "")
            mCustomerCity = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
            mCustomerCity = mCustomerCity & " " & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
            mCustomerState = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
            mCustomerStateCode = GetStateCode(mCustomerState)
            mCustomerGSTN = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
        End If

        MainClass.AssignCRptFormulas(Report1, "CompanyCity=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")

        mJurisdiction = "All Disputes Subject to " & IIf(IsDBNull(RsCompany.Fields("JURISDICTION").Value), "", RsCompany.Fields("JURISDICTION").Value) & " Jurisdiction."

        '    MainClass.AssignCRptFormulas Report1, "COMPANYTINNo=""" & IIf(IsNull(RsCompany!TINNO), "", RsCompany!TINNO) & """"							
        MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")

        mCompanyeMail = IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", "e-mail : " & RsCompany.Fields("COMPANY_MAILID").Value)
        mCompanyWebSite = IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", "WebSite : " & RsCompany.Fields("WEBSITE").Value)
        mCompanyDetail = mCompanyeMail & ", " & mCompanyWebSite
        MainClass.AssignCRptFormulas(Report1, "COMPANYDETAIL=""" & mCompanyDetail & """")

        MainClass.AssignCRptFormulas(Report1, "RemovalTime=""" & txtRemovalTime.Text & """")

        MainClass.AssignCRptFormulas(Report1, "Jurisdiction=""" & mJurisdiction & """")
        MainClass.AssignCRptFormulas(Report1, "mCustomerName=""" & mCustomerName & """")
        MainClass.AssignCRptFormulas(Report1, "mCustomerAddress=""" & mCustomerAddress & """")
        MainClass.AssignCRptFormulas(Report1, "mCustomerCity=""" & mCustomerCity & """")
        MainClass.AssignCRptFormulas(Report1, "mCustomerGSTN=""" & mCustomerGSTN & """")

        MainClass.AssignCRptFormulas(Report1, "mCustomerState=""" & mCustomerState & """")
        MainClass.AssignCRptFormulas(Report1, "mCustomerStateCode=""" & mCustomerStateCode & """")

        '    MainClass.AssignCRptFormulas Report1, "CompanyStateName=""" & IIf(IsNull(RsCompany!COMPANY_STATE), "", RsCompany!COMPANY_STATE) & """"							

        mCompanyStateCode = GetStateCode(IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value))

        MainClass.AssignCRptFormulas(Report1, "CompanyStateCode=""" & mCompanyStateCode & """")

        MainClass.AssignCRptFormulas(Report1, "Service=""" & Trim(txtServProvided.Text) & """")

        mChallanNo = VB6.Format(txtBillNo.Text, ConBillFormat)
        mBillNo = txtBillNoPrefix.Text & VB6.Format(txtBillNo.Text, ConBillFormat)
        mBillDate = VB6.Format(txtBillDate.Text, "DD/MM/YYYY")

        MainClass.AssignCRptFormulas(Report1, "mChallanNo=""" & Trim(mChallanNo) & """")
        MainClass.AssignCRptFormulas(Report1, "mBillNo=""" & Trim(mBillNo) & """")
        MainClass.AssignCRptFormulas(Report1, "mBillDate=""" & Trim(mBillDate) & """")

        MainClass.AssignCRptFormulas(Report1, "Vehicle=""" & Trim(txtVehicle.Text) & """")

        MainClass.AssignCRptFormulas(Report1, "mStateName=""" & mStateName & """")
        MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")
        MainClass.AssignCRptFormulas(Report1, "mPlaceofSupply=""" & mPlaceofSupply & """")

        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        'If PubUniversalPrinter = "Y" Then ''And mMode = crptToPrinter							

        '    For Each prt In Printers
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then
        '            Printer = prt

        '            Report1.PrinterName = prt.DeviceName
        '            Report1.PrinterDriver = prt.DriverName
        '            Report1.PrinterPort = prt.Port
        '            'Report1.PrintFileName = "D:\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".pdf"				
        '            Exit For
        '        End If
        '    Next prt
        '    ''						
        'End If

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click

        On Error GoTo ErrPart
        Dim mInvoicePrint As Boolean
        Dim mAnnexPrint As String
        Dim mEXPAnnexPrint As String
        Dim mMaxRow As Integer
        Dim mSubsidiaryChallanPrint As String
        Dim mSC_All As String
        Dim mSC_F4No As String
        Dim CntCount As Integer
        Dim mInvoicePrintType As String = ""
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPRINTED As String = ""
        Dim mExtraRemarks As String
        Dim mPrintOption As String
        Dim mPrintA4 As String
        Dim mPaperStyle As String
        Dim mPrintPaperSize As String

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Invoice Cann't be Print.")
            Exit Sub
        End If

        If CheckDespatchQty() = False Then
            MsgInformation("Despatch Qty Not Match with Invoice Qty. Cann't be Saved.")
            Exit Sub
        End If

        mPrintOption = "I"

        If lblDespRef.Text = "U" Then
            frmPrintInvoice.OptInvoiceAnnex.Enabled = True
            frmPrintInvoice.OptInvoiceAnnex.Visible = True
            frmPrintInvoice.optSubsidiaryChallan.Enabled = False
            frmPrintInvoice.optSubsidiaryChallan.Visible = False
            frmPrintInvoice.FraF4.Enabled = False
            frmPrintInvoice.FraF4.Visible = False
            frmPrintInvoice.ShowDialog()

            If G_PrintLedg = False Then
                frmPrintInvoice.Close()
                Exit Sub
            Else
                mPrintOption = IIf(frmPrintInvoice.OptInvoiceAnnex.Checked = True, "YA", "YI") 'A-Annex & I-Invoice					
                If mPrintOption = "YA" Then
                    Call ReportonInvoiceAnnex(Crystal.DestinationConstants.crptToWindow)
                    Exit Sub
                End If
                frmPrintInvoice.Close()
            End If
        End If

        SqlStr = "SELECT PRINTED FROM FIN_INVOICE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND Mkey ='" & MainClass.AllowSingleQuote((LblMKey.Text)) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPRINTED = IIf(IsDBNull(RsTemp.Fields("PRINTED").Value), "N", RsTemp.Fields("PRINTED").Value)
            mPRINTED = IIf(PubSuperUser = "S", "N", mPRINTED)
        End If

        If lblDespRef.Text = "U" Then
            frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Checked
            frmPrintInvCopy.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Unchecked
            frmPrintInvCopy.chkPrintOption(4).CheckState = System.Windows.Forms.CheckState.Unchecked
            frmPrintInvCopy.chkPrintOption(3).CheckState = System.Windows.Forms.CheckState.Unchecked
            frmPrintInvCopy.chkPrintOption(5).CheckState = System.Windows.Forms.CheckState.Unchecked
            frmPrintInvCopy.chkPrintOption(2).Enabled = False


            frmPrintInvCopy.chkPrintOption(0).Enabled = True     '' IIf(mPRINTED = "Y", False, True)
        Else

            frmPrintInvCopy.optPrintPortrait.Checked = IIf(RsCompany.Fields("INVOICE_PRINT_STYLE").Value = "P", True, False)
            frmPrintInvCopy.optPrintLandScape.Checked = IIf(RsCompany.Fields("INVOICE_PRINT_STYLE").Value = "P", False, True)

            mPrintA4 = IIf(IsDBNull(RsCompany.Fields("INVOICE_A4").Value), "Y", RsCompany.Fields("INVOICE_A4").Value)
            frmPrintInvCopy.optA4.Checked = IIf(mPrintA4 = "Y", True, False)
            frmPrintInvCopy.optA3.Checked = IIf(mPrintA4 = "Y", False, True)


            'RsCompany.Fields("INVOICE_PRINT_STYLE").Value = "P"
            'mPrintPaperSize = IIf(IsDBNull(RsCompany.Fields("INVOICE_A4").Value), "Y", RsCompany.Fields("INVOICE_A4").Value)

            frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Unchecked
            frmPrintInvCopy.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Unchecked
            frmPrintInvCopy.chkPrintOption(2).Enabled = False

            '        frmPrintInvCopy.chkPrintOption(0).Value = IIf(mPRINTED = "Y", vbUnchecked, vbChecked)						
            frmPrintInvCopy.chkPrintOption(0).Enabled = True        ''IIf(mPRINTED = "Y", False, True)
        End If

        frmPrintInvCopy.ShowDialog()


        If G_PrintLedg = False Then
            Exit Sub
        End If

        If frmPrintInvCopy.optShow(3).Checked = True Or frmPrintInvCopy.optShow(4).Checked = True Then

            Dim RsTemp1 As ADODB.Recordset = Nothing
            Dim mPackingPRINTED As String = "N"
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                SqlStr = "SELECT PRINT_PACKING FROM FIN_INVOICE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND Mkey ='" & MainClass.AllowSingleQuote((LblMKey.Text)) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp1.EOF = False Then
                    mPackingPRINTED = IIf(IsDBNull(RsTemp1.Fields("PRINT_PACKING").Value), "N", RsTemp1.Fields("PRINT_PACKING").Value)
                    If mPackingPRINTED = "Y" Then
                        MsgInformation("Packing Stricker Print Already taken so that you cann't be take again.")
                        Exit Sub
                    End If
                End If
            End If

            Call ReportOnPackingSlip(Crystal.DestinationConstants.crptToPrinter, IIf(frmPrintInvCopy.optShow(3).Checked = True, "I", "O"))
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
        ''17-05-2020							
        '    For CntCount = 0 To 5			

        If frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Checked Then ''If frmPrintInvCopy.chkPrintOption(CntCount).Value = vbChecked Then							
            If RsCompany.Fields("E_INVOICE_APP").Value = "Y" And (CDbl(lblInvoiceSeq.Text) = 1 Or CDbl(lblInvoiceSeq.Text) = 2 Or CDbl(lblInvoiceSeq.Text) = 6 Or CDbl(lblInvoiceSeq.Text) = 9) Then ''CntCount = 0 And						
                If Trim(txtIRNNo.Text) = "" Then

                    Dim mGSTRegd As String = "N"
                    If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mGSTRegd = MasterNo
                    End If

                    If mGSTRegd <> "N" Then
                        MsgInformation("You have not generated IRN. Please generate the IRN.")
                        Exit Sub
                    End If
                End If
                End If
        End If
        '            mInvoicePrintType = UCase(frmPrintInvCopy.chkPrintOption(CntCount).text)							
        Call ReportOnSales(Crystal.DestinationConstants.crptToPrinter, mInvoicePrintType, "N", mPrintOption, mPaperStyle, mPrintPaperSize, mPrePrint)
        '        End If							
        '    Next							

        If frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = "UPDATE FIN_INVOICE_HDR SET  PRINTED= 'Y', " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND  Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"

            PubDBCn.Execute(SqlStr)
        End If

        frmPrintInvCopy.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default


        Exit Sub
ErrPart:
        frmPrintInvoice.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        If Val(lblCompanyCode.Text) > 0 Then
            If Val(lblCompanyCode.Text) <> RsCompany.Fields("Company_Code").Value Then
                MsgInformation("Cann't be Add OR Modify Another Unit Voucher.")
                Exit Sub
            End If
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call CalcTots("N")
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Trim(txtIRNNo.Text) <> "" Then

            If PubSuperUser = "S" Then
                If MsgQuestion("IRN No Made against this invoice , Are you want to continue..") = vbNo Then
                    Exit Sub
                End If
            Else
                If MsgQuestion("IRN No Made against this invoice So cann't be Modified. Only Packing Standard will be Save, want to continue..") = vbNo Then
                    Exit Sub
                End If
                If UpdatePackingDetails() = True Then
                    txtBillNo_Validating(txtBillNo, New System.ComponentModel.CancelEventArgs(False))
                    If cmdAdd.Enabled = True Then cmdAdd.Focus()
                Else
                    MsgInformation("Record not saved")
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                End If
                Exit Sub
            End If


        End If

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtBillNo_Validating(txtBillNo, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume							
    End Sub
    Private Function UpdatePackingDetails() As Boolean

        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim I As Integer

        Dim mItemCode As String
        Dim mColInnerBoxQty As Double
        Dim mColInnerBoxQtyA As Double
        Dim mColInnerBoxCode As String
        Dim mColOuterBoxQty As Double
        Dim mColOuterBoxQtyA As Double
        Dim mColOuterBoxCode As String
        Dim mColPackType As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColInnerBoxQty
                mColInnerBoxQty = Val(.Text)

                .Col = ColInnerBoxQtyA
                mColInnerBoxQtyA = Val(.Text)

                .Col = ColInnerBoxCode
                mColInnerBoxCode = Trim(.Text)

                .Col = ColOuterBoxQty
                mColOuterBoxQty = Val(.Text)

                .Col = ColOuterBoxQtyA
                mColOuterBoxQtyA = Val(.Text)

                .Col = ColOuterBoxCode
                mColOuterBoxCode = Trim(.Text)

                .Col = ColPackType
                mColPackType = Trim(.Text)

                SqlStr = ""

                SqlStr = " UPDATE FIN_INVOICE_DET SET " & vbCrLf _
                        & " INNER_PACK_QTY=" & Val(mColInnerBoxQty) & ", " & vbCrLf _
                        & " INNER_PACK_QTY_A=" & Val(mColInnerBoxQtyA) & ", " & vbCrLf _
                        & " INNER_PACK_ITEM_CODE='" & mColInnerBoxCode & "', " & vbCrLf _
                        & " OUTER_PACK_QTY=" & Val(mColOuterBoxQty) & ", " & vbCrLf _
                        & " OUTER_PACK_QTY_A=" & Val(mColOuterBoxQtyA) & "," & vbCrLf _
                        & " OUTER_PACK_ITEM_CODE='" & mColOuterBoxCode & "', PACK_TYPE='" & mColPackType & "'" & vbCrLf _
                        & " WHERE MKEY='" & LblMKey.Text & "' AND ITEM_CODE='" & mItemCode & "' AND SUBROWNO=" & I & ""

                PubDBCn.Execute(SqlStr)
            Next
        End With

        UpdatePackingDetails = True
        PubDBCn.CommitTrans()

        Exit Function
UpdateDetail1:
        UpdatePackingDetails = False
        PubDBCn.RollbackTrans()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Function
    Private Sub CmdSearchDC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchDC.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mRejDocType As String
        Dim mApplicableDate As String
        Dim mInterUnit As String = "N"

        mRejDocType = IIf(IsDBNull(RsCompany.Fields("REJECTION_DOCTYPE").Value), "D", RsCompany.Fields("REJECTION_DOCTYPE").Value)
        mApplicableDate = IIf(IsDBNull(RsCompany.Fields("REJ_APPLICABLEDATE").Value), "", RsCompany.Fields("REJ_APPLICABLEDATE").Value)


        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
        '    mInterUnit = "N"
        '    If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mInterUnit = MasterNo
        '    End If
        '    If mInterUnit = "Y" And CDate(txtDNDate.Text) >= CDate("13/12/2023") Then
        '        mRejDocType = "I"
        '    End If
        'End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            SqlStr = " SELECT DISTINCT IH.AUTO_KEY_DESP, IH.DESP_DATE, EXPORT_BILL_NO," & vbCrLf _
            & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_CITY, IH.TRANSPORTER_NAME, IH.VEHICLE_NO, IH.VENDOR_PO" & vbCrLf _
            & " FROM DSP_DESPATCH_HDR IH,  FIN_SUPP_CUST_BUSINESS_MST CMST, GEN_COMPANY_MST GMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE "
        Else
            SqlStr = " SELECT DISTINCT IH.AUTO_KEY_DESP, IH.DESP_DATE, EXPORT_BILL_NO," & vbCrLf _
            & " CMST.SUPP_CUST_NAME, ID.ITEM_CODE, INVMST.CUSTOMER_PART_NO, INVMST.ITEM_SHORT_DESC, CMST.SUPP_CUST_CITY, IH.TRANSPORTER_NAME, IH.VEHICLE_NO, IH.VENDOR_PO" & vbCrLf _
            & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, FIN_SUPP_CUST_BUSINESS_MST CMST,  FIN_SUPP_CUST_MST ACM, INV_ITEM_MST INVMST, GEN_COMPANY_MST GMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACM.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ITEM_CODE=INVMST.ITEM_CODE "
        End If


        SqlStr = SqlStr & vbCrLf _
            & " And IH.BILL_TO_LOC_ID=CMST.LOCATION_ID AND IH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " And SUBSTR(IH.AUTO_KEY_DESP,LENGTH(IH.AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & " And IH.DESP_STATUS=0"

        If CDbl(lblInvoiceSeq.Text) = 3 Or CDbl(lblInvoiceSeq.Text) = 5 Then
            SqlStr = SqlStr & vbCrLf & "  AND NVL(CMST.GST_RGN_NO,' ') = GMST.COMPANY_GST_RGN_NO"
        Else
            SqlStr = SqlStr & vbCrLf & " AND NVL(CMST.GST_RGN_NO,' ') <> GMST.COMPANY_GST_RGN_NO"
        End If

        If CDbl(lblInvoiceSeq.Text) = 9 Or CDbl(lblInvoiceSeq.Text) = 5 Or CDbl(lblInvoiceSeq.Text) = 7 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DESP_TYPE='U'"
        ElseIf CDbl(lblInvoiceSeq.Text) = 1 Then
            If mRejDocType = "D" Or mApplicableDate = "" Then
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                    SqlStr = SqlStr & vbCrLf & " AND ( IH.DESP_TYPE IN ('P','F','S','G')"

                    SqlStr = SqlStr & vbCrLf & " OR IH.DESP_TYPE = CASE WHEN ACM.INTER_UNIT='Y' AND IH.DESP_DATE>=TO_DATE('13-DEC-2023') THEN 'Q' ELSE '' END "

                    SqlStr = SqlStr & vbCrLf & " OR IH.DESP_TYPE = CASE WHEN ACM.INTER_UNIT='Y' AND IH.DESP_DATE>=TO_DATE('13-DEC-2023') THEN 'L' ELSE '' END )"

                Else
                    SqlStr = SqlStr & vbCrLf & " AND IH.DESP_TYPE IN ('P','F','S','G')"
                End If

            Else
                SqlStr = SqlStr & vbCrLf & " AND IH.DESP_TYPE IN ('P','F','S','G','Q','L')"
            End If
        ElseIf CDbl(lblInvoiceSeq.Text) = 6 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DESP_TYPE IN ('E')"
        ElseIf CDbl(lblInvoiceSeq.Text) = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DESP_TYPE IN ('J','R')"
        ElseIf CDbl(lblInvoiceSeq.Text) = 3 Then
            If mRejDocType = "D" Or mApplicableDate = "" Then

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                    SqlStr = SqlStr & vbCrLf & " AND IH.DESP_TYPE NOT IN ('Q','L')"
                    'SqlStr = SqlStr & vbCrLf & " AND IH.DESP_TYPE NOT IN (CASE WHEN ACM.INTER_UNIT='Y' AND IH.DESP_DATE<TO_DATE('13-DEC-2023') THEN ('Q','L') ELSE ('XX') END) "

                Else
                    SqlStr = SqlStr & vbCrLf & " AND IH.DESP_TYPE NOT IN ('Q','L')"
                End If

            Else
            End If

        End If

        If MainClass.SearchGridMasterBySQL2((txtDCNo.Text), SqlStr) = True Then    ''If MainClass.SearchGridMaster((txtDCNo.Text), "DSP_DESPATCH_HDR", "AUTO_KEY_DESP", "DESP_DATE", "TO_CHAR(LOADING_TIME,'HH24:MI') AS DESPTIME", , SqlStr) = True Then
            txtDCNo.Text = AcName
            txtDCNo_Validating(txtDCNo, New System.ComponentModel.CancelEventArgs(False))
            If txtDCNo.Enabled = True Then txtDCNo.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub SearchDNCN()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mAccountCode As String = ""

        ''AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "							

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND BOOKCODE='-4' AND APPROVED='Y' AND CANCELLED='N' AND ISDespatched='N'"

        If RsCompany.Fields("COMPANY_CODE").Value <> 4 Then
            SqlStr = SqlStr & vbCrLf & " AND DNCNTYPE='R'"
        End If

        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            End If
        End If


        If mAccountCode <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND DEBITACCOUNTCODE='" & mAccountCode & "'"
        End If

        If MainClass.SearchGridMaster((txtDNNo.Text), "FIN_DNCN_HDR", "VNO", "VDATE", "NETVALUE", , SqlStr) = True Then
            txtDNNo.Text = AcName
            txtDNDate.Text = AcName1
            txtDNNo_Validating(txtDNNo, New System.ComponentModel.CancelEventArgs(False))
            If txtDNNo.Enabled = True Then txtDNNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FrmInvoiceGST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FrmInvoiceViewer.Hide()
        FrmInvoiceViewer.Dispose()
        FrmInvoiceViewer.Close()
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub SprdExp_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdExp.ButtonClicked
        If pShowCalc = True Then ''FormActive = True Or      If FormActive = True Then							
            SprdExp.Col = ColExpAmt
            SprdExp.Row = eventArgs.row
            If Val(SprdExp.Text) <> 0 Then
                Call CalcTots("N")
            End If
        End If
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String = ""
        Dim SqlStr As String = ""

        'Exit Sub

        'If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
        '    With SprdMain
        '        .Row = .ActiveRow
        '        .Col = ColItemCode
        '        '            If mainclass.SearchMaster(.Text, "vwITEM", "ITEMCODE", SqlStr) = True Then									
        '        '                .Row = .ActiveRow									
        '        '                .Col = ColItemCode									
        '        '                .Text = AcName									
        '        '            End If									
        '        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
        '    End With
        'End If

        If eventArgs.row = 0 And eventArgs.col = ColPackType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColPackType
                If MainClass.SearchGridMaster(.Text, "DSP_PACKINGTYPE_MST", "NAME", "", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColPackType
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColInnerBoxCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColInnerBoxCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColInnerBoxCode
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColOuterBoxCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColOuterBoxCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColOuterBoxCode
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColInvoiceType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColInvoiceType

                If lblDespRef.Text = "Q" Or lblDespRef.Text = "L" Then
                    SqlStr = "SELECT A.NAME, B.SUPP_CUST_NAME FROM FIN_INVTYPE_MST A, FIN_SUPP_CUST_MST B WHERE A.COMPANY_CODE=B.COMPANY_CODE AND A.ACCOUNTPOSTCODE=B.SUPP_CUST_CODE AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.CATEGORY='P'"
                Else
                    SqlStr = "SELECT A.NAME, B.SUPP_CUST_NAME FROM FIN_INVTYPE_MST A, FIN_SUPP_CUST_MST B WHERE A.COMPANY_CODE=B.COMPANY_CODE AND A.ACCOUNTPOSTCODE=B.SUPP_CUST_CODE AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.CATEGORY='S'"
                End If
                'If MainClass.SearchGridMasterBySQL2(.Text, "FIN_INVTYPE_MST", "NAME", "GetAccountName(COMPANY_CODE,ACCOUNTPOSTCODE) AS SUPP_CUST_NAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColInvoiceType
                    .Text = AcName
                    '                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColInvType
                End If
            End With
        End If

        'If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
        '    With SprdMain
        '        .Row = .ActiveRow
        '        .Col = ColItemDesc
        '        xIName = .Text
        '        .Text = ""
        '        '            If mainclass.SearchMaster(.Text, "vwITEM", "Name", SqlStr) = True Then									
        '        '                .Row = .ActiveRow									
        '        '                .Col = ColItemDesc									
        '        '                .Text = AcName									
        '        '            Else									
        '        '                .Row = .ActiveRow									
        '        '                .Col = ColItemDesc									
        '        '                .Text = xIName									
        '        '            End If									
        '        MainClass.ValidateWithMasterTable(.Text, "Name", "ItemCode", "Item", PubDBCn, MasterNo)
        '        .Row = .ActiveRow
        '        .Col = ColItemCode
        '        .Text = MasterNo
        '        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
        '    End With
        'End If

        '    If eventArgs.Col = 0 And eventArgs.Row > 0 Then    '***ROW DEL. OPTION NOT REQ IN INVOICE									
        '        SprdMain.Row = Row									
        '        SprdMain.Col = ColSONo									
        '        If Row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then									
        '            mainclass.DeleteSprdRow SprdMain, Row, ColSONo									
        '            mainclass.SaveStatus Me, ADDMode, MODIFYMode									
        '            FormatSprdMain Row									
        ''            Call DistributeExpInMainGrid									
        ''            Call CalcTots									
        '        End If									
        '    End If									

        Call CalcTots("N")
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim RIBBONSGroup As Boolean
        Dim xSoNo As String
        Dim xICode As String

        If eventArgs.newRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColQty
                If CheckQty() = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColRate
                Call CheckRate()
            Case ColPackType
                SprdMain.Col = ColPackType
                If Trim(SprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(SprdMain.Text, "NAME", "NAME", "DSP_PACKINGTYPE_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPackType)
                    End If
                End If
            Case ColInnerBoxCode
                SprdMain.Col = ColInnerBoxCode
                If Trim(SprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(SprdMain.Text, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColInnerBoxCode)
                    End If
                End If
            Case ColOuterBoxCode
                SprdMain.Col = ColOuterBoxCode
                If Trim(SprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(SprdMain.Text, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColOuterBoxCode)
                    End If
                End If
            Case ColInvoiceType
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColInvoiceType
                Dim mAccountCode As String
                If Trim(SprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "NAME", "ACCOUNTPOSTCODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgBox("Invoice Name Does Not Exist In Master", MsgBoxStyle.Information)
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColInvoiceType)
                        eventArgs.cancel = True
                        Exit Sub
                    Else
                        mAccountCode = MasterNo
                        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            SprdMain.Row = SprdMain.ActiveRow
                            SprdMain.Col = ColAccountName
                            SprdMain.Text = MasterNo
                        End If
                    End If
                End If
        End Select
        Call CalcTots("N")
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub CheckRate()
        On Error GoTo ERR1
        With SprdMain

            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Sub
            If chkFOC.CheckState = System.Windows.Forms.CheckState.Checked Then Exit Sub

            .Col = ColRate
            If Val(.Text) <= 0 Then
                MsgInformation("Please Enter the Rate.")
                '            MainClass.SetFocusToCell SprdMain, .ActiveRow, ColRate									
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function CheckQty() As Boolean

        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
        'With SprdView
        '    If eventArgs.row < 1 Then Exit Sub

        '    .Row = eventArgs.row

        '    .Col = 1
        '    cboInvType.Text = Trim(.Text)

        '    .Col = 2
        '    txtBillNoPrefix.Text = .Text

        '    .Col = 3
        '    txtBillNo.Text = VB6.Format(.Text, ConBillFormat)

        '    .Col = 4
        '    txtBillNoSuffix.Text = .Text

        '    .Col = 6
        '    txtBillDate.Text = VB6.Format(.Text, "DD/MM/YYYY")

        '    txtBillNo_Validating(txtBillNo, New System.ComponentModel.CancelEventArgs(False))
        '    CmdView_Click(CmdView, New System.EventArgs())
        'End With
    End Sub


    Private Sub txtAbatementPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAbatementPer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAbatementPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAbatementPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtARE1Date_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtARE1Date.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtARE1Date_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtARE1Date.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtARE1Date.Text = "" Then GoTo EventExitSub
        If IsDate(txtARE1Date.Text) = False Then
            ErrorMsg("Invalid ARE 1 Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtARE1No_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtARE1No.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Public Sub txtBillNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mBillNo As String
        If Trim(txtBillNo.Text) = "" Then GoTo EventExitSub

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 And RsCompany.Fields("FYEAR").Value = 2023 And RsCompany.Fields("COMPANY_CODE").Value = 1 And Val(txtBillNo.Text) < 100 Then
            txtBillNo.Text = VB6.Format(Val(txtBillNo.Text), "0")
        Else
            txtBillNo.Text = VB6.Format(Val(txtBillNo.Text), ConBillFormat)
        End If


        If MODIFYMode = True And RsSaleMain.EOF = False Then xMkey = RsSaleMain.Fields("mKey").Value
        mBillNo = Trim(Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & Trim(txtBillNoSuffix.Text))
        '    mBillNo = "S05135"							
        SqlStr = " SELECT * FROM FIN_INVOICE_HDR "

        If Val(lblCompanyCode.Text) <= 0 Then
            SqlStr = SqlStr & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " "
        Else
            SqlStr = SqlStr & vbCrLf & " WHERE Company_Code=" & Val(lblCompanyCode.Text) & " "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "' " & vbCrLf _
            & " AND BillNo='" & MainClass.AllowSingleQuote(mBillNo) & "' " & vbCrLf _
            & " AND BookCode=" & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' "

        SqlStr = SqlStr & vbCrLf & " AND INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & ""

        SqlStr = SqlStr & vbCrLf & " AND INVOICE_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsSaleMain.EOF = False Then
            Clear1()

            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Invoice, Use Generate Invoice Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_INVOICE_HDR " & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMkey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function GetPackingDetails() As String

        On Error GoTo ErrPart
        Dim cntRow As Long
        Dim mTotalPacket As Double
        Dim mPackQty As Double
        Dim mPackType As String

        GetPackingDetails = ""
        mPackType = ""

        Dim myarray() As String
        Dim mPackTypeList As String = ""
        Dim x As Integer
        Dim mCheckPackType As String

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColPackType
                mPackType = Trim(.Text)
                If cntRow = 1 Then
                    mPackTypeList = mPackType
                Else
                    If InStrRev(mPackTypeList, mPackType) = 0 Then
                        mPackTypeList = mPackTypeList & ", " & mPackType
                    End If
                End If
            Next
        End With

        myarray = Split(mPackTypeList, ", ")

        For x = LBound(myarray) To UBound(myarray)
            mCheckPackType = myarray(x)
            mTotalPacket = 0
            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = ColPackType
                    mPackType = Trim(.Text)
                    If mCheckPackType = mPackType Then
                        .Col = ColInnerBoxQty
                        mPackQty = Val(.Text)
                        mTotalPacket = mTotalPacket + mPackQty
                    End If
                Next
            End With
            GetPackingDetails = IIf(GetPackingDetails = "", "", GetPackingDetails & " / ") & mTotalPacket & " " & mCheckPackType
        Next


        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Function
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim I As Short
        Dim nMkey As String
        Dim mTRNType As String
        Dim mAutoKeyNo As String '' Double							
        Dim mBillNoSeq As Double
        Dim mBillNo As String
        Dim mSuppCustCode As String
        Dim mConsingee As String = ""
        Dim mBuyerCode As String = ""
        Dim mCoBuyerCode As String = ""
        Dim mAccountCode As String
        Dim mAUTHSIGN As String
        Dim mAUTHDATE As String
        Dim mFREIGHTCHARGES As String
        Dim mEXEMPT_NOTIF_NO As String
        Dim mSALETAXCODE As Integer
        Dim mItemValue As Double
        Dim mTOTSTAMT As Double
        Dim mTOTCHARGES As Double
        Dim mTotEDAmount As Double
        Dim mTotEDUAmount As Double
        Dim mTotEDUPercent As Double

        Dim mTotServicePercent As Double
        Dim mTotServiceAmount As Double
        Dim mDutyForgone As String

        Dim mTOTEXPAMT As Double
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mLSTCST As String
        Dim mWITHFORM As String
        Dim mFOC As String
        Dim mLUT As String
        Dim mPRINTED As String
        Dim mCancelled As String
        Dim mByHand As String

        Dim mIsRegdNo As String
        Dim mSTType As String
        Dim mStockTrf As String

        Dim mBookCode As Integer
        Dim mStartingNo As Double
        Dim mSTPERCENT As Double
        Dim mTOTFREIGHT As Double
        Dim mEDPERCENT As Double
        Dim mTOTTAXABLEAMOUNT As Double

        Dim mTCSAMOUNT As Double
        Dim mTCSPER As Double

        Dim mRO As Double
        Dim mSURAmount As Double
        Dim mMSC As Double
        Dim mTotDiscount As Double
        Dim mREJECTION As String
        Dim pDueDate As String
        Dim mD3 As String
        Dim mPackMat As String
        Dim mChallanMade As String

        Dim mFormRecdCode As Integer
        Dim mFormDueCode As Integer
        Dim mCT3 As String
        Dim mCT1 As String
        Dim mCT3Date As String = ""
        Dim mCT1Date As String = ""
        Dim mTaxOnMRP As String
        Dim mDutyIncluded As String

        Dim mSHECPercent As Double
        Dim mSHECAmount As Double
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mRemarks As String = ""
        Dim mDutyFreePurchase As String
        Dim mDivisionCode As Double
        Dim mAgtPermission As String
        Dim mShippedToSame As String
        Dim mShippedToCode As String = ""
        Dim mShippedFromCode As String = ""

        Dim mPartyGSTNo As String
        Dim mSameGSTNo As String
        Dim mSACCode As String
        Dim mTransMode As String
        Dim mVehicleType As String
        Dim mDespatchFrom As String
        Dim mShippToExWork As String

        Dim xBillNo As String
        Dim xBillDate As String
        Dim xIsGST As String
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim RsDN As ADODB.Recordset
        Dim mSalePersonCode As String

        Dim mRejDocType As String
        Dim mApplicableDate As String
        Dim mTRNTypeName As String

        Dim mStoreCode As String = ""
        Dim mApplicantCode As String = ""
        Dim mPackingPrinted As String = "N"
        Dim mInterUnit As String = "N"

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        mPackingPrinted = IIf(chkByHand.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mSuppCustCode = "-1"
        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = "-1"
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mPartyGSTNo = ""
        mPartyGSTNo = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(txtBillTo.Text), "GST_RGN_NO")
        'If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        mDutyFreePurchase = IIf(chkDutyFreePurchase.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If RsCompany.Fields("PACKETS_COL_SHOW").Value = "Y" Then
            txtPacking.Text = GetPackingDetails()
        End If

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_HDR", (LblMKey.Text), RsSaleMain, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_DET", (LblMKey.Text), RsSaleDetail, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_EXP", (LblMKey.Text), RsSaleExp, "MKEY", "M") = False Then GoTo ErrPart
        End If

        If ADDMode = True Then
            SqlStr = " SELECT VNO FROM FIN_DNCN_HDR " & vbCrLf _
                & " WHERE  COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND BOOKCODE='-4'" & vbCrLf & " AND MKEY IN (SELECT DISTINCT SONO " & vbCrLf _
                & " FROM DSP_DESPATCH_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_DESP = " & Val(txtDCNo.Text) & ")"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    If mRemarks = "" Then
                        mRemarks = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                    Else
                        mRemarks = mRemarks & ", " & IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                    End If
                    RsTemp.MoveNext()
                Loop
                mRemarks = "Our Debit Note No. " & mRemarks
                txtRemarks.Text = IIf(Trim(txtRemarks.Text) = "", mRemarks, txtRemarks.Text & " " & mRemarks)
            End If
        End If

        mFormRecdCode = -1
        mFormDueCode = -1

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            Dim mInvType As String

            SprdMain.Row = 1
            SprdMain.Col = ColInvoiceType
            mInvType = SprdMain.Text
            If MainClass.ValidateWithMasterTable(mInvType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then   '' AND CATEGORY='S'
                mTRNType = MasterNo
            Else
                mTRNType = CStr(-1)
                MsgBox("Please Check Invoice Type.", MsgBoxStyle.Information)
                GoTo ErrPart
            End If
        Else

            If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then    '' AND CATEGORY='S'
                mTRNType = MasterNo
            Else
                mTRNType = CStr(-1)
                MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
                GoTo ErrPart
            End If
        End If

        mShippedToSame = IIf(chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If mShippedToSame = "Y" Then
            mShippedToCode = mSuppCustCode
        Else
            If MainClass.ValidateWithMasterTable(txtShippedTo.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mShippedToCode = MasterNo
            End If
        End If

        If Trim(txtBuyerName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtBuyerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mBuyerCode = MasterNo
            End If
        End If


        mStoreCode = ""
        If Trim(txtStoreDetail.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtStoreDetail.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mStoreCode = MasterNo
            End If
        End If

        mApplicantCode = ""
        If Trim(txtApplicant.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtApplicant.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mApplicantCode = MasterNo
            End If
        End If

        If Trim(txtCoBuyerName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtCoBuyerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCoBuyerCode = MasterNo
            End If
        End If

        If MainClass.ValidateWithMasterTable(txtCreditAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            mAccountCode = "-1"
            MsgBox("Credit Account Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mAUTHSIGN = ""
        mAUTHDATE = "" '' Format(txtAuthDate.Text, "DD-MMM-YYYY")							
        mFREIGHTCHARGES = IIf(OptFreight(0).Checked = True, "To Pay", "Paid")
        mEXEMPT_NOTIF_NO = ""
        mBookCode = CInt(LblBookCode.Text)



        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            SprdMain.Row = 1
            SprdMain.Col = ColInvoiceType
            mTRNTypeName = SprdMain.Text
        Else
            mTRNTypeName = cboInvType.Text
        End If

        If MainClass.ValidateWithMasterTable(mTRNTypeName, "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then    '' AND CATEGORY='S'
            mBookSubType = MasterNo
        Else
            mBookSubType = CStr(-1)
        End If

        mSALETAXCODE = -1
        mItemValue = Val(lblTotItemValue.Text)
        mTOTSTAMT = 0
        mTOTCHARGES = 0
        mTotEDAmount = 0

        mTotEDUAmount = 0
        mTotEDUPercent = 0

        mSHECPercent = 0
        mSHECAmount = 0

        '    mTotServiceAmount = Val(lblServiceAmount.text)							
        '    mTotServicePercent = Val(lblServicePercentage.text)							


        mTOTEXPAMT = Val(lblTotExpAmt.Text)
        mNETVALUE = Val(lblNetAmount.Text)

        mSTPERCENT = 0
        mTOTFREIGHT = 0
        mEDPERCENT = 0
        mDutyForgone = CStr(0)

        mTOTTAXABLEAMOUNT = Val(lblTotTaxableAmt.Text)

        mRO = Val(lblRO.Text)
        mTotDiscount = 0
        mSURAmount = 0
        mMSC = Val(lblMSC.Text)
        mTCSAMOUNT = Val(lblTCS.Text)
        mTCSPER = Val(lblTCSPercentage.Text)

        mTotQty = Val(lblTotQty.Text)
        mLSTCST = ""
        mWITHFORM = ""
        mFOC = IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mLUT = IIf(chkLUT.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mDespatchFrom = IIf(chkDespatchFrom.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mShippToExWork = IIf(chkExWork.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        If mDespatchFrom = "N" Then
            mShippedFromCode = "-1"
        Else
            If MainClass.ValidateWithMasterTable(txtShippedFrom.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mShippedFromCode = MasterNo
            End If
        End If

        mPRINTED = "N"
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mByHand = IIf(chkByHand.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsRegdNo = "N" '' IIf(chkRegDealer.Value = vbChecked, "Y", "N")							
        mREJECTION = IIf(chkRejection.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mD3 = "N" '' IIf(chkD3.Value = vbChecked, "Y", "N")							
        mCT3 = "N" ''IIf(chkCT3.Value = vbChecked, "Y", "N")							
        mCT1 = "N" ''IIf(chkCT1.Value = vbChecked, "Y", "N")							
        mAgtPermission = IIf(chkAgtPermission.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mTaxOnMRP = IIf(chkTaxOnMRP.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mDutyIncluded = "N" '' IIf(chkDutyIncluded.Value = vbChecked, "Y", "N")							

        '    If mCT3 = "Y" Then							
        '        If Val(txtARENo.Text) = 0 Then							
        '            txtARENo.Text = GETMAX_ARENO()      '' IIf(IsNull(.Fields("ARE_NO").Value), "0", .Fields("ARE_NO").Value)							
        '        End If							
        '        lblCT3Date.text = GetCT3Date(PubDBCn, Val(TxtCTNo.Text), "", "S", mCustomerCode)							
        '        mCT3Date = Format(lblCT3Date.text, "DD/MM/YYYY")							
        '    Else							
        '        TxtCTNo.Text = ""							
        '        txtARENo.Text = ""							
        '        lblCT3Date.text = ""							
        '        mCT3Date = ""							
        '    End If							

        '    If mCT1 = "Y" Then							
        '        lblCT1Date.text = GetCT1Date(PubDBCn, Val(txtCT1No.Text), "", "S", mCustomerCode)							
        '        mCT1Date = Format(lblCT1Date.text, "DD/MM/YYYY")							
        '    Else							
        '        txtCT1No.Text = ""							
        '        lblCT1Date.text = ""							
        '        mCT1Date = ""							
        '    End If							

        mStockTrf = IIf(chkStockTrf.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mPackMat = IIf(chkPackmat.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If mPackMat = "Y" Then
            mChallanMade = "N"
        Else
            mChallanMade = "Y"
        End If

        '    If optSTType(0).Value = True Then							
        mSTType = "0"
        '    ElseIf optSTType(1).Value = True Then							
        '        mSTType = "1"							
        '    Else							
        '        mSTType = "2"							
        '    End If							

        '    If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "INVOICENOSTART", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND CATEGORY='S'") Then							
        '        mStartingNo = MasterNo							
        '    Else							
        '        mStartingNo = 1							
        '    End If							

        If Trim(txtBillNo.Text) = "" Then
            mStartingNo = 1
            mBillNoSeq = CDbl(AutoGenSeqBillNo(mBookType, mBookSubType, mStartingNo, mDivisionCode))
        Else
            mBillNoSeq = Val(txtBillNo.Text)
        End If

        '    If RsCompany.fields("FYEAR").value >= 2020 Then							
        '        txtBillNo.Text = Format(Val(mBillNoSeq), "0000000000")							
        '        mBillNo = Trim(Trim(txtBillNoPrefix.Text) & vb6.Format(Val(mBillNoSeq), "0000000000") & Trim(txtBillNoSuffix.Text))							
        '        mAutoKeyNo = Format(Val(mBillNoSeq), "0000000000") & vb6.Format(RsCompany.Fields("FYEAR").Value, "0000") & vb6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")							
        '    Else	

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 And RsCompany.Fields("FYEAR").Value = 2023 And RsCompany.Fields("COMPANY_CODE").Value = 1 And mBillNoSeq < 100 Then
            txtBillNo.Text = VB6.Format(Val(CStr(mBillNoSeq)), "0")
            mBillNo = Trim(Trim(txtBillNoPrefix.Text) & VB6.Format(Val(CStr(mBillNoSeq)), "0") & Trim(txtBillNoSuffix.Text))
            mAutoKeyNo = VB6.Format(VB6.Format(Val(CStr(mBillNoSeq)), 1) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        Else
            txtBillNo.Text = VB6.Format(Val(CStr(mBillNoSeq)), ConBillFormat)
            mBillNo = Trim(Trim(txtBillNoPrefix.Text) & VB6.Format(Val(CStr(mBillNoSeq)), ConBillFormat) & Trim(txtBillNoSuffix.Text))
            mAutoKeyNo = VB6.Format(VB6.Format(Val(CStr(mBillNoSeq)), ConBillFormat) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))

        End If
        '    End If							

        If CheckValidBillDate(mBillNoSeq, mDivisionCode) = False Then GoTo ErrPart


        '    mAutoKeyNo = Val(IIf(IsNull(RsCompany!INVOICE_PREFIX), 0, RsCompany!INVOICE_PREFIX)) & Val(lblInvoiceSeq.text) & vb6.Format(Val(mBillNoSeq), "00000") & vb6.Format(RsCompany.Fields("FYEAR").Value, "0000") & vb6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")							

        mSACCode = ""
        If Trim(txtServProvided.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtServProvided.Text, "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                mSACCode = Trim(IIf(IsDBNull(MasterNo), "", MasterNo))
            End If
        End If


        mTransMode = VB.Left(cboTransmode.Text, 1)
        mVehicleType = VB.Left(cboVehicleType.Text, 1)

        mSalePersonCode = ""
        If Trim(lblPoNo.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(lblPoNo.Text, "AUTO_KEY_SO", "SALE_PERSON_CODE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O'") = True Then
                mSalePersonCode = Trim(IIf(IsDBNull(MasterNo), "", MasterNo))
            End If
        End If

        SqlStr = ""
        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("FIN_INVOICE_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo

            LblMKey.Text = nMkey
            SqlStr = "INSERT INTO FIN_INVOICE_HDR (" & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " ROWNO, TRNTYPE, BILLNOPREFIX, " & vbCrLf & " AUTO_KEY_INVOICE, BILLNOSEQ, BILLNOSUFFIX, BILLNO, " & vbCrLf & " INVOICE_DATE, INV_PREP_DATE, INV_PREP_TIME, " & vbCrLf & " AUTO_KEY_DESP, DCDATE, CUST_PO_NO, CUST_PO_DATE, " & vbCrLf & " AMEND_NO, AMEND_DATE, AMEND_WEF_FROM, REMOVAL_DATE, " & vbCrLf & " REMOVAL_TIME, SUPP_CUST_CODE, ACCOUNTCODE, ST_38_NO, " & vbCrLf & " DUEDAYSFROM, DUEDAYSTO, AUTHSIGN, AUTHDATE, " & vbCrLf & " GRNO, GRDATE, DESPATCHMODE, DOCSTHROUGH, " & vbCrLf & " VEHICLENO, CARRIERS, FREIGHTCHARGES, " & vbCrLf & " TARIFFHEADING, EXEMPT_NOTIF_NO, " & vbCrLf & " BOOKCODE,BOOKTYPE, BOOKSUBTYPE, SALETAXCODE, " & vbCrLf & " REMARKS, ITEMDESC, ITEMVALUE, " & vbCrLf & " TOTSTAMT, TOTCHARGES, TOTEDAMOUNT, " & vbCrLf & " TOTEXPAMT, NETVALUE, TOTQTY, "

            SqlStr = SqlStr & vbCrLf & " STFORMCODE, STFORMNAME, STFORMNO, STFORMDATE, " & vbCrLf & " STDUEFORMCODE, STDUEFORMNAME, STDUEFORMNO, STDUEFORMDATE,  " & vbCrLf & " STTYPE, IsRegdNo,LSTCST, WITHFORM, FOC, PRINTED," & vbCrLf _
                & " CANCELLED, BY_HAND, NARRATION,  " & vbCrLf & " STPERCENT, TOTFREIGHT, EDPERCENT, TOTTAXABLEAMOUNT, " & vbCrLf & " TOTSURCHARGEAMT, TOTDISCAMOUNT, TOTMSCAMOUNT, TotRO,REJECTION,AGTD3, " & vbCrLf & " PACK_MAT_FLAG, CHALLAN_MADE,PRDDate, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE,ISSTOCKTRF,TCSPER, TCSAMOUNT,DNCNNO,DNCNDATE," & vbCrLf & " TOTEDUPERCENT,TOTEDUAMOUNT,TOTSERVICEPERCENT,TOTSERVICEAMOUNT,SERV_PROV," & vbCrLf & " SUPP_FROM_DATE, SUPP_TO_DATE, INTRATE, " & vbCrLf & " AGTCT3, CT_NO, CT3_DATE, ARE_NO, " & vbCrLf & " REF_DESP_TYPE, OUR_AUTO_KEY_SO, OUR_SO_DATE, "

            SqlStr = SqlStr & vbCrLf & " SHIPPING_NO, SHIPPING_DATE, " & vbCrLf & " ARE1_NO, ARE1_DATE, " & vbCrLf & " PORT_CODE, EXPBILLNO, EXPINV_DATE, TOT_EXPORTEXP,EXCHANGE_RATE, " & vbCrLf & " TOTEXCHANGEVALUE, ADV_LICENSE, DESP_LOCATION, NATURE," & vbCrLf & " TOTMRPVALUE, TAX_ON_MRP, ABATEMENT_PER, " & vbCrLf & " TOT_CUSTOMDUTY, TOT_CD_CESS, CD_PER, CD_CESS_PER, BUYER_CODE, CO_BUYER_CODE," & vbCrLf & " TOTSHECPERCENT, TOTSHECAMOUNT,UPDATE_FROM,ISDUTY_FORGONE, AGT_DUTYFREE_PUR," & vbCrLf & " DUTY_INCLUDED_ITEM, ED_PAYABLE, CESS_PAYABLE, SHEC_PAYABLE,DIV_CODE, " & vbCrLf & " AGTCT1, CT1_NO, CT1_DATE,AGT_Permission,CUST_ITEM_VALUE, " & vbCrLf & " NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT," & vbCrLf & " SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE,E_REFNO,INVOICESEQTYPE,SAC_CODE," & vbCrLf & " ADV_VNO, ADV_VDATE, ADV_ADJUSTED_AMT, " & vbCrLf & " ADV_CGST_AMT, ADV_SGST_AMT, ADV_IGST_AMT,ADV_ITEM_AMT,IS_LUT, " & vbCrLf & " TRANSPORT_MODE, TRANSPORTER_GSTNO, TRANS_DISTANCE, " & vbCrLf & " VEHICLE_TYPE, EWAYRESPONSEID, E_BILLWAYNO," & vbCrLf _
                & " IS_DESP_OTHERTHAN_BILL, SHIPPED_FROM_PARTY_CODE, " & vbCrLf _
                & " IS_SHIPPTO_EX_WORK, BILL_TO_LOC_ID, SHIP_TO_LOC_ID, VENDOR_CODE,PACKING_DETAILS,TDS_ON_SALE,SALE_PERSON_CODE," & vbCrLf _
                & " SUPP_CUST_STORE_CODE, SUPP_CUST_APPLICANT_CODE,PRINT_PACKING)"

            '
            '

            SqlStr = SqlStr & vbCrLf & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mCurRowNo & "," & Val(mTRNType) & ", '" & MainClass.AllowSingleQuote(txtBillNoPrefix.Text) & "', " & vbCrLf & " " & mAutoKeyNo & "," & mBillNoSeq & ", '" & MainClass.AllowSingleQuote(txtBillNoSuffix.Text) & "', '" & MainClass.AllowSingleQuote(mBillNo) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & TxtBillTm.Text & "','HH24:MI')," & vbCrLf & " " & Val(txtDCNo.Text) & ", TO_DATE('" & VB6.Format(txtDCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtPONo.Text) & "', TO_DATE(TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))," & vbCrLf & " " & Val(txtPOAmendNo.Text) & ",'',TO_DATE('" & VB6.Format(txtPOWEFDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtRemovalDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " TO_DATE('" & txtRemovalTime.Text & "','HH24:MI'),'" & mSuppCustCode & "','" & mAccountCode & "','', " & vbCrLf & " " & Val(txtCreditDays(0).Text) & ", " & Val(txtCreditDays(1).Text) & ", '" & mAUTHSIGN & "', TO_DATE('" & VB6.Format(mAUTHDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(TxtGRNo.Text) & "', TO_DATE('" & VB6.Format(TxtGRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtMode.Text) & "', '" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "', '" & mFREIGHTCHARGES & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtTariff.Text) & "', '" & mEXEMPT_NOTIF_NO & "', " & vbCrLf & " '" & mBookCode & "', '" & mBookType & "', '" & mBookSubType & "', " & mSALETAXCODE & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & MainClass.AllowSingleQuote(txtItemType.Text) & "', " & mItemValue & ", " & vbCrLf & " " & mTOTSTAMT & ", " & mTOTCHARGES & ", " & mTotEDAmount & ", " & vbCrLf & " " & mTOTEXPAMT & ", " & mNETVALUE & ", " & mTotQty & ", " & vbCrLf & " " & mFormRecdCode & ", '','', '', " & vbCrLf & " " & mFormDueCode & ", '','', '', " & vbCrLf & " '" & mSTType & "','" & mIsRegdNo & "', '" & mLSTCST & "', " & vbCrLf & " '" & mWITHFORM & "', '" & mFOC & "', '" & mPRINTED & "', " & vbCrLf _
                & " '" & mCancelled & "', '" & mByHand & "','" & MainClass.AllowSingleQuote(txtNarration.Text) & "',  "

            SqlStr = SqlStr & vbCrLf & "" & mSTPERCENT & "," & mTOTFREIGHT & "," & mEDPERCENT & "," & mTOTTAXABLEAMOUNT & "," & vbCrLf & "" & mSURAmount & "," & mTotDiscount & "," & mMSC & "," & mRO & ",'" & mREJECTION & "','" & mD3 & "', " & vbCrLf & "'" & mPackMat & "','" & mChallanMade & "','', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & mStockTrf & "'," & vbCrLf & " " & mTCSPER & "," & mTCSAMOUNT & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtDNNo.Text) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mTotEDUPercent & ", " & mTotEDUAmount & "," & vbCrLf & " " & mTotServicePercent & "," & mTotServiceAmount & ",'" & MainClass.AllowSingleQuote(txtServProvided.Text) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtSuppFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(txtSuppToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & Val(txtIntRate.Text) & ", '" & mCT3 & "', 0, TO_DATE('" & VB6.Format(mCT3Date, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  0," & vbCrLf & " '" & lblDespRef.Text & "', " & Val(lblPoNo.Text) & ", TO_DATE('" & VB6.Format(lblSoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(txtShippingNo.Text) & "', TO_DATE('" & VB6.Format(txtShippingDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtARE1No.Text) & "', TO_DATE('" & VB6.Format(txtARE1Date.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtPortCode.Text) & "', '" & MainClass.AllowSingleQuote(txtExportBillNo.Text) & "', TO_DATE('" & VB6.Format(txtExportBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & Val(lblTotExportExp.Text) & "," & Val(txtExchangeRate.Text) & ", " & vbCrLf & " " & Val(txtTotalEuro.Text) & ", '" & MainClass.AllowSingleQuote(txtAdvLicense.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtLocation.Text) & "', '" & MainClass.AllowSingleQuote(txtProcessNature.Text) & "'," & vbCrLf & " " & Val(lblMRPValue.Text) & ", '" & mTaxOnMRP & "', " & Val(txtAbatementPer.Text) & ", " & vbCrLf & " " & Val(lblTotCD.Text) & " , " & Val(lblEDUOnCDAmount.Text) & ", 0, 0, " & vbCrLf & " '" & MainClass.AllowSingleQuote(mBuyerCode) & "', '" & MainClass.AllowSingleQuote(mCoBuyerCode) & "'," & vbCrLf & " " & Val(CStr(mSHECPercent)) & ", " & Val(CStr(mSHECAmount)) & ",'N','" & mDutyForgone & "','" & mDutyFreePurchase & "', " & vbCrLf & " '" & mDutyIncluded & "', 0, 0, 0," & mDivisionCode & "," & vbCrLf & " '" & mCT1 & "',0, TO_DATE('" & VB6.Format(mCT1Date, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mAgtPermission & "'," & Val(txtCustMatValue.Text) & "," & vbCrLf & " " & Val(lblTotCGSTAmount.Text) & "," & Val(lblTotSGSTAmount.Text) & "," & Val(lblTotIGSTAmount.Text) & "," & vbCrLf & " '" & mShippedToSame & "','" & mShippedToCode & "','" & Trim(txteRefNo.Text) & "'," & Val(lblInvoiceSeq.Text) & ",'" & mSACCode & "'," & vbCrLf & " '" & Trim(txtAdvVNo.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtAdvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtAdvAdjust.Text) & ", " & vbCrLf & " " & Val(txtAdvCGST.Text) & ", " & Val(txtAdvSGST.Text) & ", " & Val(txtAdvIGST.Text) & ", " & Val(txtItemAdvAdjust.Text) & ",'" & mLUT & "', " & vbCrLf & " '" & mTransMode & "', '" & MainClass.AllowSingleQuote(txtTransportCode.Text) & "', " & Val(txtDistance.Text) & ", " & vbCrLf _
                & " '" & mVehicleType & "', '" & MainClass.AllowSingleQuote(txtResponseId.Text) & "','" & MainClass.AllowSingleQuote(txtEWayBillNo.Text) & "'," & vbCrLf _
                & " '" & mDespatchFrom & "', '" & MainClass.AllowSingleQuote(mShippedFromCode) & "'," & vbCrLf _
                & " '" & mShippToExWork & "', '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "', '" & MainClass.AllowSingleQuote(TxtShipTo.Text) & "' , '" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "', '" & MainClass.AllowSingleQuote(txtPacking.Text) & "'," & Val(txtTDSOnSale.Text) & ",'" & mSalePersonCode & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mStoreCode) & "','" & MainClass.AllowSingleQuote(mApplicantCode) & "','" & mPackingPrinted & "')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE FIN_INVOICE_HDR SET TRNTYPE=" & Val(mTRNType) & ",AGT_Permission ='" & mAgtPermission & "'," & vbCrLf & " BILLNOPREFIX = '" & MainClass.AllowSingleQuote(txtBillNoPrefix.Text) & "'," & vbCrLf & " BILLNOSEQ= " & mBillNoSeq & ", " & vbCrLf & " AUTO_KEY_INVOICE= " & mAutoKeyNo & ", " & vbCrLf & " BILLNOSUFFIX= '" & MainClass.AllowSingleQuote(txtBillNoSuffix.Text) & "'," & vbCrLf & " BILLNO= '" & MainClass.AllowSingleQuote(mBillNo) & "'," & vbCrLf & " INVOICE_DATE= TO_DATE(TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))," & vbCrLf & " PRDDate= ''," & vbCrLf & " INV_PREP_DATE= TO_DATE(TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))," & vbCrLf & " INV_PREP_TIME= TO_DATE('" & TxtBillTm.Text & "','HH24:MI')," & vbCrLf & " AUTO_KEY_DESP= " & Val(txtDCNo.Text) & "," & vbCrLf & " DCDATE= TO_DATE('" & VB6.Format(txtDCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " CUST_PO_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "', " & vbCrLf & " CUST_PO_DATE= TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " AMEND_NO= " & Val(txtPOAmendNo.Text) & "," & vbCrLf & " AMEND_DATE= ''," & vbCrLf & " AMEND_WEF_FROM= TO_DATE('" & VB6.Format(txtPOWEFDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " REMOVAL_DATE= TO_DATE('" & VB6.Format(txtRemovalDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " REMOVAL_TIME=TO_DATE('" & txtRemovalTime.Text & "','HH24:MI')," & vbCrLf & " SUPP_CUST_CODE= '" & mSuppCustCode & "'," & vbCrLf & " ACCOUNTCODE= '" & mAccountCode & "'," & vbCrLf & " ST_38_NO= '',CUST_ITEM_VALUE= " & Val(txtCustMatValue.Text) & ","

            SqlStr = SqlStr & vbCrLf & " DUEDAYSFROM= " & Val(txtCreditDays(0).Text) & "," & vbCrLf & " DUEDAYSTO= " & Val(txtCreditDays(1).Text) & ", " & vbCrLf & " AUTHSIGN= '" & mAUTHSIGN & "'," & vbCrLf & " AUTHDATE=  TO_DATE('" & VB6.Format(mAUTHDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " GRNO= '" & MainClass.AllowSingleQuote(TxtGRNo.Text) & "', " & vbCrLf & " GRDATE= TO_DATE('" & VB6.Format(TxtGRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " DESPATCHMODE= '" & MainClass.AllowSingleQuote(txtMode.Text) & "', " & vbCrLf & " DOCSTHROUGH= '" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "'," & vbCrLf & " VEHICLENO= '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', " & vbCrLf & " CARRIERS=  '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "'," & vbCrLf & " FREIGHTCHARGES= '" & mFREIGHTCHARGES & "', " & vbCrLf & " TARIFFHEADING= '" & MainClass.AllowSingleQuote(txtTariff.Text) & "'," & vbCrLf & " EXEMPT_NOTIF_NO= '" & MainClass.AllowSingleQuote(mEXEMPT_NOTIF_NO) & "',"


            SqlStr = SqlStr & vbCrLf & " BOOKCODE= " & mBookCode & "," & vbCrLf & " BOOKTYPE= '" & mBookType & "'," & vbCrLf & " BOOKSUBTYPE= '" & mBookSubType & "'," & vbCrLf & " SALETAXCODE= " & mSALETAXCODE & "," & vbCrLf & " REMARKS= '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf & " ITEMDESC= '" & MainClass.AllowSingleQuote(txtItemType.Text) & "'," & vbCrLf & " ITEMVALUE= " & mItemValue & "," & vbCrLf & " TOTSTAMT= " & mTOTSTAMT & "," & vbCrLf & " TOTCHARGES= " & mTOTCHARGES & "," & vbCrLf & " TOTEDAMOUNT= " & mTotEDAmount & "," & vbCrLf & " TOTEXPAMT= " & mTOTEXPAMT & "," & vbCrLf & " NETVALUE= " & mNETVALUE & "," & vbCrLf & " TOTQTY= " & mTotQty & "," & vbCrLf & " STTYPE= '" & mSTType & "'," & vbCrLf & " REJECTION='" & mREJECTION & "'," & vbCrLf & " STFORMCODE= " & mFormRecdCode & "," & vbCrLf & " STFORMNAME= ''," & vbCrLf & " STFORMNO= ''," & vbCrLf & " STFORMDATE= ''," & vbCrLf & " STDUEFORMCODE= " & mFormDueCode & "," & vbCrLf & " STDUEFORMNAME= ''," & vbCrLf & " STDUEFORMNO= ''," & vbCrLf & " STDUEFORMDATE='',"


            SqlStr = SqlStr & vbCrLf & " STPERCENT=" & mSTPERCENT & "," & vbCrLf & " TOTFREIGHT=" & mTOTFREIGHT & "," & vbCrLf & " EDPERCENT=" & mEDPERCENT & ", TOTEDUPERCENT=" & mTotEDUPercent & ", " & vbCrLf & " TOTEDUAMOUNT=" & mTotEDUAmount & ", TOTTAXABLEAMOUNT=" & mTOTTAXABLEAMOUNT & "," & vbCrLf & " TOTSERVICEPERCENT=" & mTotServicePercent & ", TOTSERVICEAMOUNT=" & mTotServiceAmount & ", " & vbCrLf & " ISREGDNO= '" & mIsRegdNo & "', LSTCST= '" & mLSTCST & "', " & vbCrLf & " WITHFORM= '" & mWITHFORM & "'," & vbCrLf & " FOC= '" & mFOC & "'," & vbCrLf & " IS_LUT= '" & mLUT & "'," & vbCrLf _
                & " CANCELLED= '" & mCancelled & "', BY_HAND= '" & mByHand & "', " & vbCrLf _
                & " NARRATION= '" & MainClass.AllowSingleQuote(txtNarration.Text) & "'," & vbCrLf & " TOTSURCHARGEAMT=" & mSURAmount & ", " & vbCrLf & " TOTDISCAMOUNT=" & mTotDiscount & ", " & vbCrLf & " TOTMSCAMOUNT=" & mMSC & ", " & vbCrLf & " TotRO=" & mRO & ", " & vbCrLf & " AGTD3='" & mD3 & "', " & vbCrLf & " PACK_MAT_FLAG='" & mPackMat & "', " & vbCrLf & " CHALLAN_MADE='" & mChallanMade & "', " & vbCrLf & " ISSTOCKTRF='" & mStockTrf & "', " & vbCrLf & " TCSAMOUNT='" & mTCSAMOUNT & "', " & vbCrLf & " SERV_PROV='" & MainClass.AllowSingleQuote(txtServProvided.Text) & "', SAC_CODE='" & mSACCode & "',"

            If mByHand = "Y" Then
                SqlStr = SqlStr & vbCrLf & " PRINT_PACKING='" & mPackingPrinted & "',"
            End If

            SqlStr = SqlStr & vbCrLf & " TCSPER='" & mTCSPER & "', " & vbCrLf & " DNCNNO='" & MainClass.AllowSingleQuote(txtDNNo.Text) & "'," & vbCrLf & " DNCNDATE=TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " SUPP_FROM_DATE=TO_DATE('" & VB6.Format(txtSuppFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " SUPP_TO_DATE=TO_DATE('" & VB6.Format(txtSuppToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " INTRATE=" & Val(txtIntRate.Text) & "," & vbCrLf & " AGTCT3='" & mCT3 & "', CT_NO=0, CT3_DATE=''," & vbCrLf & " AGTCT1='" & mCT1 & "', CT1_NO=0, CT1_DATE=''," & vbCrLf & " ARE_NO=0," & vbCrLf & " REF_DESP_TYPE='" & lblDespRef.Text & "', " & vbCrLf & " OUR_AUTO_KEY_SO=" & Val(lblPoNo.Text) & ", " & vbCrLf & " OUR_SO_DATE=TO_DATE('" & VB6.Format(lblSoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),"

            SqlStr = SqlStr & vbCrLf & " SHIPPING_NO='" & MainClass.AllowSingleQuote(txtShippingNo.Text) & "', " & vbCrLf & " SHIPPING_DATE=TO_DATE('" & VB6.Format(txtShippingDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " ARE1_NO='" & MainClass.AllowSingleQuote(txtARE1No.Text) & "', " & vbCrLf & " ARE1_DATE=TO_DATE('" & VB6.Format(txtARE1Date.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), PORT_CODE='" & MainClass.AllowSingleQuote(txtPortCode.Text) & "'," & vbCrLf & " EXPBILLNO='" & MainClass.AllowSingleQuote(txtExportBillNo.Text) & "'," & vbCrLf & " EXPINV_DATE=TO_DATE('" & VB6.Format(txtExportBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TOT_EXPORTEXP=" & lblTotExportExp.Text & "," & vbCrLf & " EXCHANGE_RATE=" & Val(txtExchangeRate.Text) & ", " & vbCrLf & " TOTEXCHANGEVALUE=" & Val(txtTotalEuro.Text) & ", " & vbCrLf & " ADV_LICENSE='" & MainClass.AllowSingleQuote(txtAdvLicense.Text) & "', " & vbCrLf & " DESP_LOCATION='" & MainClass.AllowSingleQuote(txtLocation.Text) & "', " & vbCrLf & " NATURE='" & MainClass.AllowSingleQuote(txtProcessNature.Text) & "', " & vbCrLf & " TOTMRPVALUE=" & Val(lblMRPValue.Text) & "," & vbCrLf & " TAX_ON_MRP='" & mTaxOnMRP & "'," & vbCrLf & " ABATEMENT_PER=" & Val(txtAbatementPer.Text) & ", " & vbCrLf & " TOT_CUSTOMDUTY=" & Val(lblTotCD.Text) & " , " & vbCrLf & " TOT_CD_CESS=" & Val(lblEDUOnCDAmount.Text) & ", " & vbCrLf & " CD_PER=0, " & vbCrLf & " CD_CESS_PER =0, " & vbCrLf & " BUYER_CODE='" & MainClass.AllowSingleQuote(mBuyerCode) & "'," & vbCrLf & " CO_BUYER_CODE='" & MainClass.AllowSingleQuote(mCoBuyerCode) & "'," & vbCrLf & " TOTSHECPERCENT = " & mSHECPercent & ", " & vbCrLf & " TOTSHECAMOUNT = " & mSHECAmount & ", " & vbCrLf & " UPDATE_FROM='N',ISDUTY_FORGONE='" & mDutyForgone & "',AGT_DUTYFREE_PUR='" & mDutyFreePurchase & "',"

            SqlStr = SqlStr & vbCrLf & " ADV_VNO = '" & Trim(txtAdvVNo.Text) & "'," & vbCrLf & " ADV_VDATE = TO_DATE('" & VB6.Format(txtAdvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " ADV_ADJUSTED_AMT = " & Val(txtAdvAdjust.Text) & ", " & vbCrLf & " ADV_CGST_AMT = " & Val(txtAdvCGST.Text) & ", " & vbCrLf & " ADV_SGST_AMT = " & Val(txtAdvSGST.Text) & ", " & vbCrLf & " ADV_IGST_AMT = " & Val(txtAdvIGST.Text) & ", " & vbCrLf & " ADV_ITEM_AMT = " & Val(txtItemAdvAdjust.Text) & ", "

            SqlStr = SqlStr & vbCrLf & " DUTY_INCLUDED_ITEM='" & mDutyIncluded & "'," & vbCrLf & " ED_PAYABLE=0," & vbCrLf & " CESS_PAYABLE=0," & vbCrLf & " SHEC_PAYABLE=0, DIV_CODE=" & mDivisionCode & ", " & vbCrLf & " NETCGST_AMOUNT=" & Val(lblTotCGSTAmount.Text) & ", NETSGST_AMOUNT=" & Val(lblTotSGSTAmount.Text) & ", NETIGST_AMOUNT=" & Val(lblTotIGSTAmount.Text) & ", " & vbCrLf & " SHIPPED_TO_SAMEPARTY='" & mShippedToSame & "', SHIPPED_TO_PARTY_CODE='" & mShippedToCode & "', " & vbCrLf & " E_REFNO='" & Trim(txteRefNo.Text) & "', INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & ","

            SqlStr = SqlStr & vbCrLf & " SALE_PERSON_CODE='" & mSalePersonCode & "', TRANSPORT_MODE='" & mTransMode & "'," & vbCrLf _
                & " TRANSPORTER_GSTNO='" & MainClass.AllowSingleQuote(txtTransportCode.Text) & "'," & vbCrLf _
                & " TRANS_DISTANCE=" & Val(txtDistance.Text) & "," & vbCrLf & " VEHICLE_TYPE='" & mVehicleType & "'," & vbCrLf _
                & " EWAYRESPONSEID= '" & MainClass.AllowSingleQuote(txtResponseId.Text) & "'," & vbCrLf & " E_BILLWAYNO='" & MainClass.AllowSingleQuote(txtEWayBillNo.Text) & "'," & vbCrLf & " IS_DESP_OTHERTHAN_BILL='" & mDespatchFrom & "'," & vbCrLf & " SHIPPED_FROM_PARTY_CODE='" & MainClass.AllowSingleQuote(mShippedFromCode) & "'," & vbCrLf _
                & " IS_SHIPPTO_EX_WORK='" & mShippToExWork & "', TDS_ON_SALE=" & Val(txtTDSOnSale.Text) & "," & vbCrLf _
                & " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "', SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote(TxtShipTo.Text) & "', VENDOR_CODE='" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "', PACKING_DETAILS='" & MainClass.AllowSingleQuote(txtPacking.Text) & "', " & vbCrLf _
                & " SUPP_CUST_STORE_CODE='" & MainClass.AllowSingleQuote(mStoreCode) & "', SUPP_CUST_APPLICANT_CODE='" & MainClass.AllowSingleQuote(mApplicantCode) & "'"
            '
            SqlStr = SqlStr & vbCrLf & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        End If

        ''& " PRINTED= '" & mPRINTED & "'," & vbCrLf							

        PubDBCn.Execute(SqlStr)

        mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")

        If UpdateDetail1(mAutoKeyNo, mBillNo, VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), mTRNType, mSuppCustCode, mAccountCode, mShippedToSame, mShippedToCode, mDivisionCode, mSameGSTNo) = False Then GoTo ErrPart
        If UpdateDCMain(mBillNo) = False Then GoTo ErrPart

        '    If RsCompany.fields("FYEAR").value >= 2020 Then							
        '        If UpdatePacking(Format(Val(mBillNoSeq), "0000000000"), txtBillDate.Text, mSuppCustCode, True) = False Then GoTo ErrPart							
        '    Else							
        If UpdatePacking(VB6.Format(Val(CStr(mBillNoSeq)), ConBillFormat), (txtBillDate.Text), mSuppCustCode, True) = False Then GoTo ErrPart
        '    End If							
        If UpdateTCSDetail1(mBillNo, mTRNType, mSuppCustCode, mBookType, mBookSubType, mCancelled) = False Then GoTo ErrPart

        pDueDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, Val(txtCreditDays(1).Text), CDate(txtBillDate.Text)))

        '    mConsingee = ""							
        '    If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "BUYERCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then							
        '        mBuyerCode = MasterNo							
        '        If Trim(mBuyerCode) <> "" Then							
        '            If MainClass.ValidateWithMasterTable(mBuyerCode, "SUPP_CUST_CODE", "BUYERCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then							
        '                mSuppCustCode = mBuyerCode							
        '                mConsingee = txtCustomer.Text							
        '            Else							
        '                MsgInformation "Invalid Buyer Code"							
        '                GoTo ErrPart							
        '            End If							
        '        End If							
        '    End If							

        If Trim(mBuyerCode) <> "" Then
            mConsingee = txtCustomer.Text
            mSuppCustCode = mBuyerCode
        End If
        '							
        '    If SalePostTRN(PubDBCn, LblMKey.text, mCurRowNo, _							
        ''        LblBookCode.text, mBookType, mBookSubType, mBillNo, txtBillDate.Text, _							
        ''        mTRNType, mSuppCustCode, mAccountCode, Val(mNETVALUE), IIf(chkCancelled.Value = vbChecked, True, False), _							
        ''        pDueDate, IIf(Trim(txtDNNo) <> "", True, False), txtRemarks.Text, IIf(chkFOC.Value = vbChecked, True, False), mConsingee, mTotServiceAmount, Val(lblTotExportExp.text), ADDMode, mAddUser, mAddDate, 0, mDivisionCode) = False Then GoTo ErrPart							

        '     ,							


        mRejDocType = IIf(IsDBNull(RsCompany.Fields("REJECTION_DOCTYPE").Value), "D", RsCompany.Fields("REJECTION_DOCTYPE").Value)
        mApplicableDate = IIf(IsDBNull(RsCompany.Fields("REJ_APPLICABLEDATE").Value), "", RsCompany.Fields("REJ_APPLICABLEDATE").Value)

        If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked And mRejDocType = "I" Then
            If mApplicableDate <> "" Then
                If CDate(mApplicableDate) <= CDate(txtBillDate.Text) Then
                    '                With SprdMain				
                    '                    For cntRow = 1 To .MaxRows - 1				
                    '                        .Row = cntRow				
                    '                        .Col = ColItemCode				
                    '                        mItemCode = Trim(.Text)				
                    '				
                    '				
                    '                        SqlStr = " SELECT IH.VNO, IH.VDATE, IH.ISGSTREFUND, ID.ITEM_RATE, ID.SUPP_REF_NO, ID.SUPP_REF_DATE, " & vbCrLf _				
                    ''                                & " ID.CGST_PER, ID.SGST_PER, ID.IGST_PER  " & vbCrLf _				
                    ''                                & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID" & vbCrLf _				
                    ''                                & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _				
                    ''                                & " AND IH.MKEY=ID.MKEY " & vbCrLf _				
                    ''                                & " AND IH.VNO = '" & txtPONo.Text & "' AND IH.VDATE = '" & vb6.Format(txtPODate.Text, "DD-MMM-YYYY") & "' " & vbCrLf _				
                    ''                                & " AND IH.BOOKCODE=" & ConDebitNoteBookCode & " " & vbCrLf _				
                    ''                                & " AND ID.ITEM_CODE='" & mItemCode & "' "				
                    '				
                    '                        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsDN, adLockReadOnly				
                    '				
                    '				
                    '                        If RsDN.EOF = False Then				
                    '                            xBillNo = IIf(xBillNo = "", IIf(IsNull(RsDN!SUPP_REF_NO), "", RsDN!SUPP_REF_NO), xBillNo)				
                    '                            xBillDate = IIf(xBillDate = "", IIf(IsNull(RsDN!SUPP_REF_DATE), "", RsDN!SUPP_REF_DATE), xBillDate)				
                    '                            xIsGST = IIf(xIsGST = "", IIf(IsNull(RsDN!ISGSTREFUND), "Y", RsDN!ISGSTREFUND), xIsGST)				
                    '                        End If				
                    '				
                    '                    Next				
                    '                End With				

                    If PurRejPostTRNGST(PubDBCn, (LblMKey.Text), 1, (LblBookCode.Text), mBookType, mBookSubType, "S", mBillNo, (txtBillDate.Text), (txtPONo.Text), (txtPODate.Text), "-1", "-1", 0, IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate, "AGT REJECTION NO & DATE " & txtPONo.Text & txtPODate.Text, "", 0, ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivisionCode, IIf(mSameGSTNo = "Y", "N", "Y"), Val(lblTotCGSTAmount.Text), Val(lblTotSGSTAmount.Text), Val(lblTotIGSTAmount.Text), txtBillTo.Text) = False Then GoTo ErrPart

                    If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                        ''IIf(xIsGST = "G", IIf(mSameGSTNo = "Y", "N", "Y"), IIf(xIsGST = "I", "I", "N"))				
                        If mDNCnNO <> Trim(txtDNNo.Text) Then
                            SqlStr = " UPDATE FIN_DNCN_HDR SET UPDATE_FROM='N'," & vbCrLf _
                                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                                    & " ISDESPATCHED='N',SALEINVOICENO='',SALEINVOICEDATE='' " & vbCrLf _
                                    & " WHERE  COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                                    & " AND BOOKCODE='-4'" & vbCrLf & " AND MKEY IN (SELECT DISTINCT SONO " & vbCrLf _
                                    & " FROM DSP_DESPATCH_DET " & vbCrLf _
                                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                    & " AND AUTO_KEY_DESP = " & Val(txtDCNo.Text) & ")"

                            '                & " AND VNO = '" & MainClass.AllowSingleQuote(mDNCnNO) & "'" & vbCrLf _			
                            ''                & " AND VDATE='" & vb6.Format(mDNCnDate, "DD-MMM-YYYY") & "' AND BOOKCODE='-4'" ''			
                            PubDBCn.Execute(SqlStr)
                        End If

                        If mDNCnNO <> Trim(txtDNNo.Text) And chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                            SqlStr = " UPDATE FIN_DNCN_HDR SET UPDATE_FROM='N'," & vbCrLf _
                                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                                    & " ISDESPATCHED='Y',SALEINVOICENO='" & MainClass.AllowSingleQuote(mBillNo) & "'," & vbCrLf _
                                    & " SALEINVOICEDATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                                    & " AND BOOKCODE='-4'" & vbCrLf & " AND MKEY IN (SELECT DISTINCT SONO " & vbCrLf _
                                    & " FROM DSP_DESPATCH_DET " & vbCrLf _
                                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                    & " AND AUTO_KEY_DESP = " & Val(txtDCNo.Text) & ")"

                            '                & " AND VNO = '" & MainClass.AllowSingleQuote(txtDNNo.Text) & "'" & vbCrLf _			
                            ''                & " AND VDATE='" & vb6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "'"			
                        Else
                            SqlStr = " UPDATE FIN_DNCN_HDR SET UPDATE_FROM='N'," & vbCrLf _
                                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                                & " ISDESPATCHED='N',SALEINVOICENO='',SALEINVOICEDATE=''" & vbCrLf _
                                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                & " AND BOOKCODE='-4'" & vbCrLf _
                                & " AND MKEY IN (SELECT DISTINCT SONO " & vbCrLf _
                                & " FROM DSP_DESPATCH_DET " & vbCrLf _
                                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                & " AND AUTO_KEY_DESP = " & Val(txtDCNo.Text) & ")"

                            '                & " AND VNO = '" & MainClass.AllowSingleQuote(txtDNNo.Text) & "'" & vbCrLf _			
                            ''                & " AND VDATE='" & vb6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "'"			
                        End If
                        PubDBCn.Execute(SqlStr)
                    End If
                End If
                End If
        Else
            If RsCompany.Fields("IS_POST_DC_IN_LEDGER").Value = "N" And (Val(lblInvoiceSeq.Text) = 3 Or Val(lblInvoiceSeq.Text) = 5) Then

            Else
                If SalePostTRN_GST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mBillNo, (txtBillDate.Text),
                            mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mNETVALUE)), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked,
                            True, False), pDueDate, IIf(Trim(txtDNNo.Text) <> "", True, False), (txtRemarks.Text), IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False),
                               mConsingee, mTotServiceAmount, Val(lblTotExportExp.Text), IIf(mSameGSTNo = "Y", 0, Val(lblTotCGSTAmount.Text)),
                               IIf(mSameGSTNo = "Y", 0, Val(lblTotIGSTAmount.Text)), IIf(mSameGSTNo = "Y", 0, Val(lblTotSGSTAmount.Text)), ADDMode, mAddUser, mAddDate, Val(lblTotItemValue.Text), mDivisionCode,
                               IIf(CDbl(lblInvoiceSeq.Text) = 6 Or Val(lblInvoiceSeq.Text) = 7, "Y", "N"), Val(txtAdvCGST.Text), Val(txtAdvSGST.Text), Val(txtAdvIGST.Text), Trim(txtBillTo.Text)) = False Then GoTo ErrPart
            End If
        End If

        ''- IIf(mSameGSTNo = "Y", Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text), 0) ''14/11/2018							


        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        '    Resume							
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''	

        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        RsSaleMain.Requery() ''.Refresh							
        RsSaleDetail.Requery() ''.Refresh							
        RsSaleTrading.Requery()
        'If Err.Description = "" Then Exit Function
        'If Err.Number = -2147217900 Then
        '    ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        'Else

        'End If
        ''    Resume							
    End Function
    Private Function CheckValidBillDate(ByRef pBillNoSeq As Integer, ByRef mDivisionCode As Double) As Object

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mRsCheck1 As ADODB.Recordset = Nothing
        Dim mRsCheck2 As ADODB.Recordset = Nothing
        Dim mBackBillDate As String = ""
        Dim mMaxInvStrfNo As Integer

        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckValidBillDate = True

        If RsCompany.Fields("STOCKBALCHECK").Value = "N" Then
            Exit Function
        End If

        If Val(txtBillNo.Text) = 1 Then Exit Function

        mSeparateSeries = IIf(IsDBNull(RsCompany.Fields("SEPARATE_INV_SERIES").Value), "N", RsCompany.Fields("SEPARATE_INV_SERIES").Value)

        '    SqlStr = "SELECT INV_SERIES " & vbCrLf _							
        ''            & " FROM INV_DIVISION_MST " & vbCrLf _							
        ''            & " WHERE Company_Code=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _							
        ''            & " AND DIV_CODE=" & mDivisionCode & ""							
        '							
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly							
        '							
        '							
        '    If RsTemp.EOF = False Then							
        '        mSeparateSeries = IIf(IsNull(RsTemp!SEPARATE_MRR_SERIES), "N", RsTemp!SEPARATE_MRR_SERIES)							
        '    End If							

        SqlStr = "SELECT MAX(INVOICE_DATE)" & vbCrLf _
            & " FROM FIN_INVOICE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKCode = " & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' " & vbCrLf & " AND INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & " " & vbCrLf & " AND BillNoSeq<" & Val(CStr(pBillNoSeq)) & ""

        'If mSeparateSeries = "Y" Then
        SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        'End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDBNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(INVOICE_DATE)" & " FROM FIN_INVOICE_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKCode = " & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' " & vbCrLf & " AND INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & " " & vbCrLf & " AND BillNoSeq>" & Val(CStr(pBillNoSeq)) & ""

        'If mSeparateSeries = "Y" Then
        SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        'End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) And mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtBillDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Bill Date Is Greater Than The BillDate Of Next InvoiceNo.")
                CheckValidBillDate = False
            ElseIf CDate(txtBillDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Bill Date Is Less Than The BillDate Of Previous InvoiceNo.")
                CheckValidBillDate = False
            End If
        ElseIf mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) Then
            If CDate(txtBillDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Bill Date Is Greater Than The BillDate Of Next InvoiceNo.")
                CheckValidBillDate = False
            End If
        ElseIf mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtBillDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Bill Date Is Less Than The BillDate Of Previous InvoiceNo.")
                CheckValidBillDate = False
            End If
        End If

        Exit Function
CheckERR:
        CheckValidBillDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqBillNo(ByRef mBookType As String, ByRef mBookSubType As String, ByRef pStartingSNo As Double, ByRef mDivisionCode As Double) As String

        On Error GoTo AutoGenSeqBillNoErr
        Dim RsSaleMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqBillNo As Integer
        Dim mStartingSNo As Double
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xFYear As Integer
        Dim mPrefix As Double
        Dim mMaxValue As String
        Dim mSeqNo As Double
        Dim mFormat As String
        Dim mBillPrefix As String

        SqlStr = ""

        xFYear = CInt(VB6.Format(RsCompany.Fields("Start_Date").Value, "YY"))

        mBillPrefix = IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value)

        mStartingSNo = CDbl(VB6.Format(pStartingSNo, ConBillFormat))


        SqlStr = ""


        SqlStr = "SELECT Max(BILLNOSEQ)  FROM FIN_INVOICE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND BookType='" & mBookType & "'" ''& vbCrLf |            & " AND BookSubType  IN ( "							

        ''31/03/2022
        'SqlStr = SqlStr & vbCrLf & " AND INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & ""

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 112 Then
            SqlStr = SqlStr & vbCrLf & " AND INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & ""
        Else
            If Trim(txtBillNoPrefix.Text) = "" Then
                SqlStr = SqlStr & vbCrLf & " AND (BILLNOPREFIX='' OR BILLNOPREFIX IS NULL)"
            Else
                SqlStr = SqlStr & vbCrLf & " AND BILLNOPREFIX='" & Trim(txtBillNoPrefix.Text) & "'"
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSaleMainGen
            If .EOF = False Then

                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mSeqNo = mMaxValue + 1     '' Mid(mMaxValue, 6, Len(mMaxValue) - 5) + 1

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



        mNewSeqBillNo = mSeqNo      ''VB6.Format(mSeqNo, mFormat)

        ''& vb6.Format(IIf(IsNull(RsCompany!INVOICE_PREFIX), "", RsCompany!INVOICE_PREFIX), "00") & Val(lblInvoiceSeq.text)							

        AutoGenSeqBillNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        '    Resume							
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GETMAX_ARENO() As Double

        On Error GoTo AutoGenNoErr
        Dim RsGen As ADODB.Recordset = Nothing
        Dim mNewNo As Integer
        SqlStr = ""


        SqlStr = "SELECT Max(ARE_NO)  FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND AGTCT3='Y'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mNewNo = .Fields(0).Value + 1
                Else
                    mNewNo = 1
                End If
            Else
                mNewNo = 1
            End If
        End With
        GETMAX_ARENO = mNewNo
        Exit Function
AutoGenNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateDCMain(ByRef pBillNoStr As String) As Boolean

        On Error GoTo UpdateDCErr
        Dim xDCNo As Double
        Dim mDescStatus As Integer
        Dim mCancelled As String

        xDCNo = Val(txtDCNo.Text)



        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            mDescStatus = 2
        Else
            mDescStatus = 1
        End If

        SqlStr = ""

        ''            & " AND DESP_DATE=TO_DATE('" & VB6.Format(txtDCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _

        SqlStr = "UPDATE DSP_DESPATCH_HDR SET DESP_STATUS=" & mDescStatus & ", " & vbCrLf _
            & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " WHERE AUTO_KEY_DESP=" & Val(CStr(xDCNo)) & " " & vbCrLf _
            & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

        PubDBCn.Execute(SqlStr)

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            If DeleteStockTRN(PubDBCn, ConStockRefType_DSP, (txtDCNo.Text)) = False Then GoTo UpdateDCErr
            PubDBCn.Execute("DELETE FROM DSP_PAINT57F4_TRN WHERE MKey='" & txtDCNo.Text & "' AND BookType='D' AND BookSubType='O' AND TRNTYPE='D'")
        End If

        UpdateDCMain = True
        Exit Function
UpdateDCErr:
        UpdateDCMain = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume							
    End Function
    Private Function UpdatePacking(ByRef pBillNo As String, ByRef pBillDate As String, ByRef pCustCode As String, ByRef mIsUpdateMode As Boolean) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mExsiceInvMade As String
        Dim mDCNo As Double
        Dim mPackingNo As Double
        Dim mDespType As String = ""
        Dim mExciseInvNo As String
        Dim mExciseInvDate As String

        mDCNo = Val(txtDCNo.Text)
        mExsiceInvMade = IIf(mIsUpdateMode = True, "Y", "N")
        mExciseInvNo = IIf(mIsUpdateMode = True, pBillNo, "")
        mExciseInvDate = IIf(mIsUpdateMode = True, pBillDate, "")

        SqlStr = " SELECT DESP_TYPE, AUTO_KEY_SO" & vbCrLf _
            & " FROM  DSP_DESPATCH_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustCode) & "'" & vbCrLf _
            & " AND AUTO_KEY_DESP=" & Val(CStr(mDCNo)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mDespType = IIf(IsDBNull(RsTemp.Fields("DESP_TYPE").Value), "X", RsTemp.Fields("DESP_TYPE").Value)
            mPackingNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_SO").Value), 0, RsTemp.Fields("AUTO_KEY_SO").Value)
        Else
            MsgInformation("No. Such Despatch Note Found.")
            UpdatePacking = False
        End If


        If mDespType <> "E" Then
            UpdatePacking = True
            Exit Function
        End If
        SqlStr = " UPDATE FIN_EXPINV_HDR SET " & vbCrLf & " EXCISE_INV_MADE='" & mExsiceInvMade & "'," & vbCrLf & " EXCISE_INV_NO='" & mExciseInvNo & "'," & vbCrLf & " EXCISE_INV_DATE=TO_DATE('" & VB6.Format(mExciseInvDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PACK=" & Val(CStr(mPackingNo)) & ""

        PubDBCn.Execute(SqlStr)


        SqlStr = " UPDATE DSP_PACKING_HDR SET " & vbCrLf & " EXCISE_INV_MADE='" & mExsiceInvMade & "'," & vbCrLf & " EXCISE_INV_NO='" & mExciseInvNo & "'," & vbCrLf & " EXCISE_INV_DATE=TO_DATE('" & VB6.Format(mExciseInvDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PACK=" & Val(CStr(mPackingNo)) & ""

        PubDBCn.Execute(SqlStr)

        UpdatePacking = True
        Exit Function
UpdateDetail1Err:
        UpdatePacking = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Function

    Private Function UpdateDetail1(ByRef pAutoKey As String, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pTRNType As String, ByRef pSuppCustCode As String, ByRef pAccountCode As String, ByRef pShipToSameParty As String, ByRef pShipToSuppCustCode As String, ByRef pDivCode As Double, ByRef mSameGSTNo As String) As Boolean

        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim mItemCode As String
        Dim mPartNo As String
        Dim mItemDesc As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mMRP As Double
        Dim mAmount As Double
        Dim mExicseableAmt As Double
        Dim mSTableAmt As Double
        Dim mCessableAmt As Double
        Dim mCESSAmt As Double
        Dim mSHECAmt As Double
        Dim mRefNo As String
        Dim mRefDate As String
        Dim UpdateRec As String

        Dim mTotExicseableAmt As Double
        Dim mTotSTableAmt As Double
        Dim mTotCessableAmt As Double
        Dim mIsSaleComp As String
        Dim mIsSuppInv As String
        Dim mServiceAmt As Double
        Dim mTaxableMRP As Double
        Dim mJITCallNo As String
        Dim mCustItemValue As Integer
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mOBillNo As String
        Dim mOBillDate As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mHSNCode As String
        Dim mPOS As String
        Dim mState As String
        Dim mGoodsServices As String
        Dim mTaxableAmount As Double

        Dim mNoofStrip As Double
        Dim mStripRate As Double
        Dim mItemSNo As String

        Dim mAddItemDesc As String = ""
        Dim mMRRNo As Double
        Dim mODNo As String = ""
        Dim mHeatNo As String = ""
        Dim mBatchNo As String = ""
        Dim mColPackType As String
        Dim mColInnerBoxQty As Double
        Dim mColInnerBoxQtyA As Double
        Dim mColInnerBoxCode As String
        Dim mColOuterBoxQty As Double
        Dim mColOuterBoxQtyA As Double
        Dim mColOuterBoxCode As String
        Dim mInvoiceTypeCode As String = ""
        Dim mInvoiceTypeName As String

        Dim mAccountHeadCode As String = ""

        Dim mGlassDescription As String

        Dim mActualHeight As Double
        Dim mActualWidth As Double
        Dim mChargeableHeight As Double
        Dim mChargeableWidth As Double
        Dim mArea As Double
        Dim mChargeableArea As Double
        Dim mAreaRate As Double
        Dim mModelNo As String

        mTotExicseableAmt = GetExicseAbleAmt()
        mTotSTableAmt = GetSTAbleAmt()
        mTotCessableAmt = GetCessAbleAmt()

        PubDBCn.Execute("Delete From FIN_INVOICE_DET Where Mkey='" & LblMKey.Text & "'")
        PubDBCn.Execute("Delete From TEMP_FIN_RGDAILYMANU_HDR Where Mkey='" & LblMKey.Text & "' AND BOOKTYPE='S'")
        PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "' AND BOOKCODE='" & LblBookCode.Text & "'")


        ''UpdateGSTTRN							
        SqlStr = "INSERT INTO TEMP_FIN_RGDAILYMANU_HDR (SELECT * From FIN_RGDAILYMANU_HDR Where Mkey='" & LblMKey.Text & "'  AND BOOKTYPE='S')"
        PubDBCn.Execute(SqlStr)

        PubDBCn.Execute("Delete From FIN_RGDAILYMANU_HDR Where Mkey='" & LblMKey.Text & "' AND BOOKTYPE='S'")
        PubDBCn.Execute("DELETE FROM DSP_PAINT57F4_TRN WHERE MKey='" & LblMKey.Text & "' AND BookType='D' AND BookSubType='O' AND TRNTYPE='S'")

        PubDBCn.Execute("Delete From FIN_CT_TRN Where Mkey='" & LblMKey.Text & "'" & vbCrLf & " AND BOOKTYPE='S' AND BOOKSUBTYPE='O'")

        PubDBCn.Execute("Delete From FIN_CT1_TRN Where Mkey='" & LblMKey.Text & "' AND BOOKTYPE='S' AND BOOKSUBTYPE='O'")

        mPOS = ""
        If pShipToSameParty = "N" Then
            If MainClass.ValidateWithMasterTable(pShipToSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                mState = MasterNo
                If MainClass.ValidateWithMasterTable(mState, "NAME", "STATE_CODE", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mPOS = MasterNo
                End If
            End If
        End If


        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "ISSALECOMP", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
            mIsSaleComp = MasterNo
        Else
            mIsSaleComp = "N"
        End If

        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "ISSUPPBILL", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
            mIsSuppInv = MasterNo
        Else
            mIsSuppInv = "N"
        End If

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemSNo
                mItemSNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColPartNo
                mPartNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                    mItemDesc = MainClass.AllowSingleQuote(mItemDesc)
                Else
                    mItemDesc = MainClass.AllowSingleQuote(.Text)
                End If

                .Col = ColHSNCode
                mHSNCode = Trim(.Text)

                .Col = Col57F4
                mRefNo = Trim(.Text)

                .Col = Col57F4Date
                mRefDate = Val(.Text)

                .Col = ColJITCallNo
                mJITCallNo = Trim(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColMRP
                mMRP = Val(.Text)
                mTaxableMRP = mMRP - (mMRP * 0.01 * Val(txtAbatementPer.Text))
                mTaxableMRP = mTaxableMRP * mQty

                .Col = ColAmount
                mAmount = Val(.Text)
                If Val(lblTotItemValue.Text) = 0 Then
                    mCustItemValue = 0
                Else
                    mCustItemValue = Val(txtCustMatValue.Text) * mAmount / Val(lblTotItemValue.Text)
                End If
                If mTotExicseableAmt = 0 Then
                    mExicseableAmt = 0
                    mCessableAmt = 0
                Else
                    If chkTaxOnMRP.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        mExicseableAmt = 0 ' Format((Val(lblTotED.text) * (mAmount + mCustItemValue)) / mTotExicseableAmt, "0.00")			
                    Else
                        mExicseableAmt = 0 'Format((Val(lblTotED.text) * (mTaxableMRP + mCustItemValue)) / mTotExicseableAmt, "0.00")			
                    End If
                    mCessableAmt = mExicseableAmt
                    ''mExicseableAmt = Format((Val(lblTotED.text) * mAmount) / (mTotExicseableAmt - Val(lblEDUAmount.text)), "0.00")				
                End If

                If Val(lblTotItemValue.Text) = 0 Then
                    mServiceAmt = 0
                    '                mCessableAmt = 0				
                Else
                    If chkTaxOnMRP.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        mServiceAmt = 0 ' Format((Val(lblServiceAmount.text) * mAmount) / Val(lblTotItemValue.text), "0.00")			
                    Else
                        mServiceAmt = 0 ' Format((Val(lblServiceAmount.text) * mTaxableMRP) / Val(lblTotItemValue.text), "0.00")			
                    End If
                    mCessableAmt = mCessableAmt + mServiceAmt
                End If

                If mTotCessableAmt = 0 Then
                    mCESSAmt = 0
                Else
                    mCESSAmt = 0 ' Format((Val(lblEDUAmount.text) * mCessableAmt) / mTotCessableAmt, "0.00")				
                End If

                If mTotCessableAmt = 0 Then
                    mSHECAmt = 0
                Else
                    mSHECAmt = 0 ' Format((Val(lblSHECAmount.text) * mCessableAmt) / mTotCessableAmt, "0.00")				
                End If

                '            If Val(lblEDUAmount.text) = 0 Then					
                '                mCESSAmt = 0					
                '            Else					
                '                mCESSAmt = Format((Val(lblEDUAmount.text) * mExicseableAmt) / Val(lblTotED.text), "0.00")					
                '            End If					
                '					

                If mTotSTableAmt = 0 Then
                    mSTableAmt = 0
                Else
                    If chkTaxOnMRP.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        mSTableAmt = 0 ' Format((Val(lblTotST.text) * (mAmount + mCustItemValue + mExicseableAmt + mCESSAmt)) / mTotSTableAmt, "0.00")			
                    Else
                        mSTableAmt = 0 ' Format((Val(lblTotST.text) * (mTaxableMRP + mCustItemValue + mExicseableAmt + mCESSAmt)) / mTotSTableAmt, "0.00")			
                    End If
                    '                mSTableAmt = Format((Val(lblTotST.text) * (mAmount + mExicseableAmt + (mExicseableAmt * 0.02))) / mTotSTableAmt, "0.00")				
                End If

                .Col = ColTaxableAmount
                mTaxableAmount = Val(.Text)

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

                .Col = ColNoOfStrip
                mNoofStrip = Val(.Text)

                .Col = ColStripRate
                mStripRate = Val(.Text)

                .Col = ColAddItemDesc
                mAddItemDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColMRRNo
                mMRRNo = Val(.Text)

                .Col = ColODNo
                mODNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColHeatNo
                mHeatNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColBatchNo
                mBatchNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColInnerBoxQty
                mColInnerBoxQty = Val(.Text)

                .Col = ColInnerBoxQtyA
                mColInnerBoxQtyA = Val(.Text)

                .Col = ColInnerBoxCode
                mColInnerBoxCode = Trim(.Text)

                .Col = ColOuterBoxQty
                mColOuterBoxQty = Val(.Text)

                .Col = ColOuterBoxQtyA
                mColOuterBoxQtyA = Val(.Text)

                .Col = ColOuterBoxCode
                mColOuterBoxCode = Trim(.Text)

                .Col = ColInvoiceType
                mInvoiceTypeName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mInvoiceTypeName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mInvoiceTypeCode = MasterNo
                End If

                mAccountHeadCode = GetDebitNameOfInvType(mInvoiceTypeName, "N")


                .Col = ColPackType
                mColPackType = Trim(.Text)

                .Col = ColGlassDescription
                mGlassDescription = MainClass.AllowSingleQuote(.Text)

                .Col = ColModel
                mModelNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColActualHeight
                mActualHeight = Val(.Text)

                .Col = ColActualWidth
                mActualWidth = Val(.Text)

                .Col = ColChargeableHeight
                mChargeableHeight = Val(.Text)

                .Col = ColChargeableWidth
                mChargeableWidth = Val(.Text)

                .Col = ColActualArea
                mArea = Val(.Text)

                .Col = ColChargeableArea
                mChargeableArea = Val(.Text)

                .Col = ColAreaRate
                mAreaRate = Val(.Text)

                SqlStr = ""

                mUpdate = False
                If lblDespRef.Text = "U" And mQty <> 0 Then
                    mUpdate = True
                ElseIf mQty > 0 Then
                    mUpdate = True
                End If

                If mItemCode <> "" And mUpdate = True Then
                    SqlStr = " INSERT INTO FIN_INVOICE_DET ( " & vbCrLf _
                        & " MKEY , AUTO_KEY_INVOICE, SUBROWNO, " & vbCrLf _
                        & " ITEM_CODE , ITEM_DESC, HSNCODE, CUSTOMER_PART_NO,ITEM_SNO,ITEM_QTY, " & vbCrLf _
                        & " ITEM_UOM , ITEM_RATE, ITEM_AMT, GSTABLE_AMT," & vbCrLf _
                        & " ITEM_ED, ITEM_ST,ITEM_CESS,ITEM_SERVICE, " & vbCrLf _
                        & " COMPANY_CODE,ITEM_MRP,ITEM_SHEC,JIT_CALLNO, " & vbCrLf _
                        & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
                        & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, NO_OF_STRIP, STRIP_RATE, " & vbCrLf _
                        & " OD_NO, MRR_REF_NO, MRR_REF_DATE, " & vbCrLf _
                        & " OUR_REF_NO, OUR_REF_DATE, " & vbCrLf _
                        & " BATCH_NO, HEAT_NO, ADD_ITEM_DESCRIPTION,INNER_PACK_QTY, INNER_PACK_QTY_A, INNER_PACK_ITEM_CODE, OUTER_PACK_QTY, OUTER_PACK_QTY_A, " & vbCrLf _
                        & " OUTER_PACK_ITEM_CODE,ACCOUNT_POSTING_CODE, PACK_TYPE," & vbCrLf _
                        & " GLASS_DESC, ACTUAL_HEIGHT, ACTUAL_WIDTH," & vbCrLf _
                        & " CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH, GLASS_AREA, CHARGEABLEGLASS_AREA," & vbCrLf _
                        & " AREA_RATE,ITEM_MODEL,INV_ACCOUNT_CODE" & vbCrLf _
                        & " ) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ('" & LblMKey.Text & "'," & pAutoKey & ", " & I & ", " & vbCrLf _
                        & " '" & mItemCode & "','" & mItemDesc & "', '" & mHSNCode & "', '" & mPartNo & "', '" & mItemSNo & "', " & mQty & ", " & vbCrLf _
                        & " '" & mUnit & "'," & mRate & "," & mAmount & ", " & mTaxableAmount & "," & vbCrLf _
                        & " " & mExicseableAmt & "," & mSTableAmt & "," & mCESSAmt & "," & vbCrLf _
                        & " " & mServiceAmt & "," & RsCompany.Fields("COMPANY_CODE").Value & "," & mMRP & ", " & vbCrLf _
                        & " " & mSHECAmt & ",'" & mJITCallNo & "'," & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & "," & vbCrLf _
                        & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ", " & vbCrLf _
                        & " " & mNoofStrip & ", " & mStripRate & ", " & vbCrLf _
                        & " '" & mODNo & "', " & mMRRNo & ", '', " & vbCrLf _
                        & " '" & mRefNo & "',TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & mBatchNo & "', '" & mHeatNo & "', '" & mAddItemDesc & "'," & mColInnerBoxQty & ", " & mColInnerBoxQtyA & ", '" & mColInnerBoxCode & "'," & vbCrLf _
                        & " " & mColOuterBoxQty & "," & mColOuterBoxQtyA & ",'" & mColOuterBoxCode & "','" & MainClass.AllowSingleQuote(mInvoiceTypeCode) & "','" & mColPackType & "'," & vbCrLf _
                        & " '" & mGlassDescription & "', " & mActualHeight & ", " & mActualWidth & ", " & vbCrLf _
                        & " " & mChargeableHeight & ", " & mChargeableWidth & ", " & mArea & "," & mChargeableArea & "," & vbCrLf _
                        & " " & mAreaRate & ",'" & mModelNo & "','" & MainClass.AllowSingleQuote(mAccountHeadCode) & "'" & vbCrLf _
                        & " ) "

                    PubDBCn.Execute(SqlStr)

                    If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then

                        UpdateRec = "N"

                        SqlStr = "Select * From TEMP_FIN_RGDAILYMANU_HDR Where Mkey='" & LblMKey.Text & "' AND BOOKTYPE='S'  AND ITEM_CODE='" & mItemCode & "' AND UpdateFlag='Y'"
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsMisc.EOF = False Then
                            UpdateRec = "Y"
                        Else
                            UpdateRec = "N"
                        End If

                        If mIsSaleComp = "Y" Then
                            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then 'chkD3.Value = vbUnchecked And		
                                SqlStr = " INSERT INTO FIN_RGDAILYMANU_HDR ( " & vbCrLf & " MKEY , COMPANY_CODE, FYEAR, BOOKTYPE, " & vbCrLf & " BILLNO , INV_PREP_TM, MDATE, " & vbCrLf & " ITEM_CODE,ITEM_QTY, TARIFF_CODE, UPDATEFLAG) "

                                SqlStr = SqlStr & vbCrLf & " VALUES ('" & LblMKey.Text & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", 'S'," & vbCrLf & " '" & pBillNo & "', TO_DATE('" & TxtBillTm.Text & "','HH24:MI'), " & vbCrLf & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mItemCode & "'," & mQty & ",'" & txtTariff.Text & "','" & UpdateRec & "' ) "
                                PubDBCn.Execute(SqlStr)
                                '                    Else	
                                '                        PubDBCn.Execute "Delete From FIN_RGDAILYMANU_HDR Where Mkey='" & lblMkey.text & "' AND ITEM_CODE='" & mItemCode & "'"	
                            End If
                        End If
                    End If

                    If mSameGSTNo = "Y" Then

                    Else
                        If mCGSTAmount + mSGSTAmount + mIGSTAmount > 0 Then
                            mOBillNo = ""
                            mOBillDate = ""
                            If lblDespRef.Text = "U" And mRefNo <> "" Then
                                SqlStr = "SELECT BILLNO, INVOICE_DATE FROM FIN_INVOICE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_INVOICE=" & mRefNo & ""
                                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


                                If RsTemp.EOF = False Then
                                    mOBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                                    mOBillDate = IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value)
                                End If
                            End If

                            mGoodsServices = IIf(lblDespRef.Text = "J" Or lblDespRef.Text = "R", "S", "G")

                            If UpdateGSTTRN(PubDBCn, (LblMKey.Text), LblBookCode.Text, mBookType, mBookSubType, pBillNo, pBillDate, pBillNo, pBillDate, mOBillNo, mOBillDate, pSuppCustCode, pAccountCode, pShipToSameParty, pShipToSuppCustCode, I, mItemCode, mQty, mUnit, mRate, mAmount, mTaxableAmount, mMRP, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, pDivCode, mHSNCode, Trim(mItemDesc), mPOS, "N", (lblDespRef.Text), mGoodsServices, "N", "D", pBillDate, "N") = False Then GoTo UpdateDetail1

                        End If
                    End If
                End If
            Next
        End With

        PubDBCn.Execute("Delete From TEMP_FIN_RGDAILYMANU_HDR Where Mkey='" & LblMKey.Text & "' AND BOOKTYPE='S'")

        UpdateDetail1 = True
        UpdateDetail1 = UpdateSaleExp1()
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Function

    Public Function UpdateF4Detail(ByRef pDBCn As ADODB.Connection, ByRef pMKey As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pAccountCode As String, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pItemCode As String, ByRef xItemQty As Double, ByRef pIO As String, ByRef pSubRowNo As Integer, ByRef pTRNType As String, ByRef pVDate As String, Optional ByRef IsPaintF4 As String = "", Optional ByRef pIsScrap As String = "", Optional ByRef pIsScrapMaterial As String = "", Optional ByRef pIsRejection As String = "") As Boolean


        On Error GoTo ErrDetail

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTempF4 As ADODB.Recordset = Nothing
        Dim pF4No As String
        Dim pF4Date As String
        Dim mItemF4ConsQty As Double

        'Dim mSqlStr As String							
        'Dim RsTempUOM As ADODB.Recordset							
        '							
        'Dim pSubItemCode As String							
        'Dim mItemConsQty As Double							

        'Dim RsTemp57F4 As ADODB.Recordset							
        'Dim xBookType As String=""							
        '							
        'Dim mIssueUOM As String							
        'Dim mPurchaseUOM As String							
        'Dim mFactor As Long							
        'Dim mINCodeConQty As Double							
        'Dim pItemQty As Double							
        'Dim mIsManyIn As Boolean							

        Dim mF4Qty As Double
        Dim mF4ItemCode As String
        Dim mBalF4Qty As Double
        Dim mUpdateF4Qty As Double
        Dim mHeight As Double
        Dim mWidth As Double

        '    mIsManyIn = False							

        '    xBookType = "G"							

        '    pSubItemCode = ""							

        SqlStr = "SELECT IH.MKEY, IH.PRODUCT_CODE, ID.RM_CODE,STD_QTY/OUTPUT_QTY AS  STD_QTY" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & pItemCode & "'" & vbCrLf & " AND ID.STOCK_TYPE='CS'"

        SqlStr = SqlStr & vbCrLf & " AND IH.WEF = (" & vbCrLf & " SELECT MAX(SH.WEF) " & vbCrLf & " FROM PRD_NEWBOM_HDR SH, PRD_NEWBOM_DET SD" & vbCrLf & " WHERE SH.MKEY=SD.MKEY" & vbCrLf & " AND SH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SH.PRODUCT_CODE='" & pItemCode & "'" & vbCrLf & " AND SD.STOCK_TYPE='CS'" & vbCrLf & " AND SH.WEF<=TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mF4Qty = xItemQty * IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)
                mBalF4Qty = mF4Qty
                mF4ItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("RM_CODE").Value), "", RsTemp.Fields("RM_CODE").Value))

                SqlStr = " SELECT SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY) AS ITEM_QTY,ITEM_CODE,PARTY_F4NO,PARTY_F4DATE " & vbCrLf & " FROM DSP_PAINT57F4_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE = '" & mF4ItemCode & "'" & vbCrLf & " AND ISSCRAP ='N'" & vbCrLf & " GROUP BY " & vbCrLf & " PARTY_F4NO,PARTY_F4DATE,ITEM_CODE " & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)>0"

                SqlStr = SqlStr & vbCrLf & " ORDER BY PARTY_F4DATE, PARTY_F4NO"

                MainClass.UOpenRecordSet(SqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempF4, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTempF4.EOF = False Then
                    Do While RsTempF4.EOF = False
                        pF4No = IIf(IsDBNull(RsTempF4.Fields("PARTY_F4NO").Value), "", RsTempF4.Fields("PARTY_F4NO").Value)
                        pF4Date = IIf(IsDBNull(RsTempF4.Fields("PARTY_F4DATE").Value), "", RsTempF4.Fields("PARTY_F4DATE").Value)
                        mItemF4ConsQty = IIf(IsDBNull(RsTempF4.Fields("ITEM_QTY").Value), 0, RsTempF4.Fields("ITEM_QTY").Value) ''/ mINCodeConQty			

                        If mItemF4ConsQty >= mBalF4Qty Then
                            mUpdateF4Qty = mBalF4Qty
                            mBalF4Qty = 0
                        Else
                            mUpdateF4Qty = mItemF4ConsQty
                            mBalF4Qty = mBalF4Qty - mItemF4ConsQty
                        End If

                        SqlStr = " INSERT INTO DSP_PAINT57F4_TRN ( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " BOOKTYPE, BOOKSUBTYPE, PARTY_F4NO, " & vbCrLf & " PARTY_F4DATE, SUPP_CUST_CODE, BILL_NO, " & vbCrLf & " BILL_DATE, ITEM_CODE,  " & vbCrLf & " ITEM_QTY, ITEM_IO, SUB_ITEM_CODE, " & vbCrLf & " SUBROWNO,BILL_QTY,TRNTYPE, VDATE, ISSCRAP,IS_SCRAP_MAT) VALUES ( " & vbCrLf & " '" & pMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf & " '" & pBookType & "', '" & pBookSubType & "', '" & pF4No & "', " & vbCrLf & " TO_DATE('" & VB6.Format(pF4Date, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & pAccountCode & "', '" & MainClass.AllowSingleQuote(pBillNo) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mF4ItemCode) & "', " & vbCrLf & " " & mUpdateF4Qty & ", '" & pIO & "', '" & MainClass.AllowSingleQuote(pItemCode) & "'," & vbCrLf & " " & pSubRowNo & "," & mUpdateF4Qty & ",'" & pTRNType & "', " & vbCrLf & " TO_DATE('" & VB6.Format(pVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & pIsScrap & "', '" & pIsScrapMaterial & "' )"

                        pDBCn.Execute(SqlStr)

                        If mBalF4Qty = 0 Then
                            GoTo NextItemCode
                        End If

                        '                    If pItemQty > mItemF4ConsQty Then			
                        '                        MsgBox "No Enough 57F4 Stock at Item Code " & pItemCode, vbCritical			
                        '                        UpdateF4Detail = False			
                        '                        Exit Function			
                        '                    End If			
                        '                    pItemQty = pItemQty * mINCodeConQty			

                        RsTempF4.MoveNext()
                        If RsTempF4.EOF = True Then
                            If mBalF4Qty > 0 Then
                                MsgBox("No Enough 57F4 Stock at Item Code " & mF4ItemCode, MsgBoxStyle.Critical)
                                UpdateF4Detail = False
                                Exit Function
                            End If
                        End If
                    Loop
                Else
                    MsgBox("No Enough 57F4 Stock at Item Code " & mF4ItemCode, MsgBoxStyle.Critical)
                    '                UpdateF4Detail = False				
                    '                Exit Function				
                End If

NextItemCode:
                RsTemp.MoveNext()
            Loop
        End If


        '    pSubItemCode = GetInJobworkItem(pItemCode, Trim(pBillDate), mINCodeConQty, mIsManyIn)							
        '    If pSubItemCode = "" Then							
        '        pSubItemCode = "('" & pItemCode & "')"							
        '    Else							
        '        pSubItemCode = "(" & Trim(pSubItemCode) & ",'" & pItemCode & "')"							
        '    End If							
        '    mIsManyIn = IIf(pTRNType = "N" Or pIsRejection = "Y", False, mIsManyIn)							
        '							
        '							
        '    If mIsManyIn = True Then							
        '        If UpdateManyF4TRN(pItemCode, pVDate, pMKey, pBookType, pBookSubType, _							
        ''            pAccountCode, pBillNo, pBillDate, xItemQty, pIO, pSubRowNo, _							
        ''            pTRNType, pVDate, pIsScrap, pIsScrapMaterial) = False Then GoTo ErrDetail							
        '        UpdateF4Detail = True							
        '        Exit Function							
        '    Else							
        '        SqlStr = " SELECT SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY) AS ITEM_QTY,ITEM_CODE,PARTY_F4NO,PARTY_F4DATE " & vbCrLf _							
        ''                & " FROM DSP_PAINT57F4_TRN " & vbCrLf _							
        ''                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _							
        ''                & " AND TRIM(PARTY_F4NO)='" & MainClass.AllowSingleQuote(pF4No) & "' " & vbCrLf _							
        ''                & " AND ITEM_CODE IN " & pSubItemCode & "" & vbCrLf _							
        ''                & " AND ISSCRAP ='" & pIsScrap & "' AND SUPP_CUST_CODE='" & pAccountCode & "'" & vbCrLf _							
        ''                & " GROUP BY " & vbCrLf _							
        ''                & " PARTY_F4NO,PARTY_F4DATE,ITEM_CODE " & vbCrLf _							
        ''                & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)>0"							
        '							
        '        MainClass.UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RsTemp, adLockReadOnly							
        '        ''AND BILL_NO<>'" & pBillNo & "'							
        '							
        '        If RsTemp.EOF = False Then							
        '            pSubItemCode = IIf(IsNull(RsTemp!ITEM_CODE), "", RsTemp!ITEM_CODE)							
        '            pF4No = IIf(IsNull(RsTemp!PARTY_F4NO), "", RsTemp!PARTY_F4NO)							
        '            pF4Date = IIf(IsNull(RsTemp!PARTY_F4DATE), "", RsTemp!PARTY_F4DATE)							
        '            mItemF4ConsQty = IIf(IsNull(RsTemp!ITEM_QTY), 0, RsTemp!ITEM_QTY) / mINCodeConQty							
        '							
        '            If pItemQty > mItemF4ConsQty Then							
        '                MsgBox "No Enough 57F4 Stock at Item Code " & pItemCode, vbCritical							
        '                UpdateF4Detail = False							
        '                Exit Function							
        '            End If							
        '            pItemQty = pItemQty * mINCodeConQty							
        '        Else							
        '            MsgBox "No Enough 57F4 Stock at Item Code " & pItemCode, vbCritical							
        '            UpdateF4Detail = False							
        '            Exit Function							
        '        End If							
        '							
        '							
        '        If pF4No <> "" And pItemQty > 0 Then							
        '            SqlStr = "INSERT INTO DSP_PAINT57F4_TRN ( " & vbCrLf _							
        ''                & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _							
        ''                & " BOOKTYPE, BOOKSUBTYPE, PARTY_F4NO, " & vbCrLf _							
        ''                & " PARTY_F4DATE, SUPP_CUST_CODE, BILL_NO, " & vbCrLf _							
        ''                & " BILL_DATE, ITEM_CODE,  " & vbCrLf _							
        ''                & " ITEM_QTY, ITEM_IO, SUB_ITEM_CODE, " & vbCrLf _							
        ''                & " SUBROWNO,BILL_QTY,TRNTYPE, VDATE, ISSCRAP,IS_SCRAP_MAT) VALUES ( " & vbCrLf _							
        ''                & " '" & pMKey & "'," & RsCompany.fields("COMPANY_CODE").value & ", " & RsCompany.fields("FYEAR").value & "," & vbCrLf _							
        ''                & " '" & pBookType & "', '" & pBookSubType & "', '" & pF4No & "', " & vbCrLf _							
        ''                & " '" & vb6.Format(pF4Date, "DD-MMM-YYYY") & "', '" & pAccountCode & "', '" & MainClass.AllowSingleQuote(pBillNo) & "', " & vbCrLf _							
        ''                & " '" & vb6.Format(pBillDate, "DD-MMM-YYYY") & "', '" & MainClass.AllowSingleQuote(pSubItemCode) & "', " & vbCrLf _							
        ''                & " " & pItemQty & ", '" & pIO & "', '" & MainClass.AllowSingleQuote(pItemCode) & "'," & vbCrLf _							
        ''                & " " & pSubRowNo & "," & pItemQty & ",'" & pTRNType & "', " & vbCrLf _							
        ''                & " '" & vb6.Format(pVDate, "DD-MMM-YYYY") & "','" & pIsScrap & "', '" & pIsScrapMaterial & "' )"							
        '							
        '            pDBCn.Execute SqlStr							
        '        End If							
        '    End If							
        'Else							
        '    If Val(RsCompany.fields("COMPANY_CODE").value) = 1 Then							
        '        If CDate(Format(pBillDate, "DD/MM/YYYY")) <= CDate("15/12/2004") Then							
        '            UpdateF4Detail = True							
        '            Exit Function							
        '        End If							
        '    End If							
        '							
        '    SqlStr = "SELECT CONSUMPTION_UNIT,CON_ITEM_CODE,CONSUMPTION_QTY " & vbCrLf _							
        ''        & " FROM DSP_CONSUMPTION_HDR IH,DSP_CONSUMPTION_DET ID " & vbCrLf _							
        ''        & " WHERE " & vbCrLf _							
        ''        & " IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf _							
        ''        & " AND IH.BOOKTYPE='P' AND IH.ITEM_CODE=ID.ITEM_CODE " & vbCrLf _							
        ''        & " AND IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _							
        ''        & " AND IH.ITEM_CODE='" & pItemCode & "'"							
        '    MainClass.UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RsTemp, adLockReadOnly							
        '							
        '    If RsTemp.EOF = False Then							
        '        Do While Not RsTemp.EOF							
        '            pSubItemCode = IIf(IsNull(RsTemp!CON_ITEM_CODE), "", RsTemp!CON_ITEM_CODE)							
        '            mItemConsQty = pItemQty * IIf(IsNull(RsTemp!CONSUMPTION_QTY), 0, RsTemp!CONSUMPTION_QTY) / IIf(IsNull(RsTemp!CONSUMPTION_UNIT), 0, RsTemp!CONSUMPTION_UNIT)							
        '							
        '            SqlStr = " SELECT SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY*CASE WHEN ITEM_IO='O' AND MKEY='" & pMKey & "' THEN 0 ELSE 1 END) AS ITEMQTY,PARTY_F4NO,PARTY_F4DATE " & vbCrLf _							
        ''                    & " FROM DSP_PAINT57F4_TRN " & vbCrLf _							
        ''                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _							
        ''                    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pSubItemCode) & "'" & vbCrLf _							
        ''                    & " AND SUPP_CUST_CODE= '" & MainClass.AllowSingleQuote(pAccountCode) & "'" & vbCrLf _							
        ''                    & " AND ISSCRAP='" & pIsScrap & "'" & vbCrLf _							
        ''                    & " AND PARTY_F4NO NOT LIKE 'DIFF%' " & vbCrLf _							
        ''                    & " GROUP BY PARTY_F4NO,PARTY_F4DATE " & vbCrLf _							
        ''                    & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY*CASE WHEN ITEM_IO='O' AND MKEY='" & pMKey & "' THEN 0 ELSE 1 END)>0"							
        '							
        '            MainClass.UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RsTemp57F4, adLockReadOnly							
        '            If RsTemp57F4.EOF = False Then							
        '                Do While Not RsTemp57F4.EOF							
        ''                            Do While mItemConsQty <> 0							
        '                    If mItemConsQty <= 0 Then GoTo GotoNext							
        '                        pF4No = IIf(IsNull(RsTemp57F4!PARTY_F4NO), "", RsTemp57F4!PARTY_F4NO)							
        '                        pF4Date = IIf(IsNull(RsTemp57F4!PARTY_F4DATE), "", RsTemp57F4!PARTY_F4DATE)							
        '                        mItemF4ConsQty = Format(IIf(IsNull(RsTemp57F4!ITEMQTY), 0, RsTemp57F4!ITEMQTY), "0.000")							
        '							
        ''                                Call Get57F4Detail(pDBCn, pSubItemCode, pAccountCode, pF4No, pF4Date, mItemF4ConsQty)							
        '                        If mItemF4ConsQty = 0 Then							
        ''                                    UpdateF4Detail = True							
        '                            GoTo GotoNext57F4							
        '                        End If							
        '							
        '                        If mItemConsQty <= mItemF4ConsQty Then							
        '                            mItemF4ConsQty = mItemConsQty							
        '                        End If							
        '							
        '                        If pF4No <> "" And mItemF4ConsQty > 0 Then							
        '                            SqlStr = "INSERT INTO DSP_PAINT57F4_TRN ( " & vbCrLf _							
        ''                                & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _							
        ''                                & " BOOKTYPE, BOOKSUBTYPE, PARTY_F4NO, " & vbCrLf _							
        ''                                & " PARTY_F4DATE, SUPP_CUST_CODE, BILL_NO, " & vbCrLf _							
        ''                                & " BILL_DATE, ITEM_CODE,  " & vbCrLf _							
        ''                                & " ITEM_QTY, ITEM_IO, " & vbCrLf _							
        ''                                & " SUB_ITEM_CODE,SUBROWNO,BILL_QTY,TRNTYPE,VDATE,ISSCRAP,IS_SCRAP_MAT) VALUES ( " & vbCrLf _							
        ''                                & " '" & pMKey & "'," & RsCompany.fields("COMPANY_CODE").value & ", " & RsCompany.fields("FYEAR").value & "," & vbCrLf _							
        ''                                & " '" & pBookType & "', '" & pBookSubType & "', '" & pF4No & "', " & vbCrLf _							
        ''                                & " '" & vb6.Format(pF4Date, "DD-MMM-YYYY") & "', '" & pAccountCode & "', '" & MainClass.AllowSingleQuote(pBillNo) & "', " & vbCrLf _							
        ''                                & " '" & vb6.Format(pBillDate, "DD-MMM-YYYY") & "', '" & MainClass.AllowSingleQuote(pSubItemCode) & "', " & vbCrLf _							
        ''                                & " " & vb6.Format(mItemF4ConsQty, "0.000") & ", '" & pIO & "', '" & MainClass.AllowSingleQuote(pItemCode) & "'," & vbCrLf _							
        ''                                & " " & pSubRowNo & "," & pItemQty & ",'" & pTRNType & "', '" & vb6.Format(pVDate, "DD-MMM-YYYY") & "','" & pIsScrap & "', '" & pIsScrapMaterial & "' )"							
        '							
        '                            pDBCn.Execute SqlStr							
        '                        End If							
        '                        mItemConsQty = mItemConsQty - mItemF4ConsQty							
        '                        pF4No = ""							
        '                        pF4Date = ""							
        '                        mItemF4ConsQty = 0							
        '							
        ''                            Loop							
        'GotoNext57F4:							
        '                If mItemConsQty = 0 Then Exit Do							
        '							
        '                RsTemp57F4.MoveNext							
        '                Loop							
        '            End If							
        'GotoNext:							
        '            RsTemp.MoveNext							
        '        Loop							
        '    End If							

        UpdateF4Detail = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateF4Detail = False
        '    Resume							
    End Function

    Private Function UpdateTCSDetail1(ByRef pBillNo As String, ByRef pTRNType As String, ByRef pCustomerCode As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pCancelled As String) As Boolean

        On Error GoTo UpdateDetail1
        Dim mTCSAMOUNT As Double

        PubDBCn.Execute("Delete From TCS_TRN Where Mkey='" & LblMKey.Text & "'")

        '    If MainClass.ValidateWithMasterTable(pTRNType, "CODE", "ISSCRAPSALE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND CATEGORY='S'") = True Then							
        '        If MasterNo = "N" Then							
        '            UpdateTCSDetail1 = True							
        '            Exit Function							
        '        End If							
        '    Else							
        '        UpdateTCSDetail1 = True							
        '        Exit Function							
        '    End If							

        If Val(lblTCS.Text) = 0 Then
            UpdateTCSDetail1 = True
            Exit Function
        End If

        mTCSAMOUNT = Val(lblTCS.Text)
        'mTCSAMOUNT = System.Math.Round(mTCSAMOUNT, 0)

        SqlStr = "INSERT INTO TCS_TRN ( " & vbCrLf _
            & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _
            & " BILLNO, SUBROWNO, INVOICE_DATE, " & vbCrLf _
            & " SUPP_CUST_CODE, BOOKCODE, BOOKTYPE, " & vbCrLf _
            & " BOOKSUBTYPE, NETVALUE, TCSPER, " & vbCrLf _
            & " TCSAMOUNT, REMARKS, CANCELLED, " & vbCrLf _
            & " ADDITIONAL_TAX,  " & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf _
            & " MODUSER, MODDATE,UPDATE_FROM " & vbCrLf _
            & " ) VALUES ( "

        SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(LblMKey.Text)) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & pBillNo & "', 1, TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & pCustomerCode & "', " & Val(LblBookCode.Text) & ",'" & pBookType & "', " & vbCrLf & " '" & pBookSubType & "', " & Val(lblNetAmount.Text) & ", " & Val(lblTCSPercentage.Text) & ", " & vbCrLf & " " & Val(CStr(mTCSAMOUNT)) & ", '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & pCancelled & "', " & vbCrLf & " 'N'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N')"

        PubDBCn.Execute(SqlStr)

        UpdateTCSDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateTCSDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Function
    Private Function UpdateSaleExp1() As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim mDutyForgone As String

        PubDBCn.Execute("Delete From FIN_INVOICE_EXP Where Mkey='" & LblMKey.Text & "'")
        With SprdExp
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColExpName
                If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mExpCode = MasterNo
                Else
                    mExpCode = -1
                End If

                .Col = ColExpPercent
                mPercent = Val(.Text)

                .Col = ColExpAmt
                mExpAmount = Val(.Text)

                SprdExp.Col = ColExpAddDeduct
                m_AD = SprdExp.Text
                If m_AD = "D" Then
                    mExpAmount = mExpAmount * -1
                End If

                SprdExp.Col = ColExpCalcOn
                mCalcOn = Val(.Text)

                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColDutyForgone
                mDutyForgone = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                SqlStr = ""
                If mCalcOn <> 0 Or mExpAmount <> 0 Then
                    SqlStr = "Insert Into  FIN_INVOICE_EXP (MKEY,SUBROWNO, " & vbCrLf & "EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DUTYFORGONE) " & vbCrLf & "Values ('" & LblMKey.Text & "'," & I & ", " & vbCrLf & "" & mExpCode & "," & mPercent & "," & mExpAmount & "," & mCalcOn & ",'" & mRO & "','" & mDutyForgone & "')"
                    PubDBCn.Execute(SqlStr)
                End If
            Next I
        End With
        UpdateSaleExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateSaleExp1 = False
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim mIsStockTransfer As String
        Dim mIsJW As String
        Dim mIsSPD As String
        Dim mIsScrapSale As String
        Dim mTariffCode As String
        Dim mIsSaleComp As String
        Dim mIsWithinState As String = ""
        Dim mIsWithinCountry As String = ""
        Dim mDespType As String = ""
        Dim mItemCode As String
        Dim mUOM As String
        Dim mItemRate As Double
        Dim mCurrentTime As String
        Dim mInvGenTimeFrom As String
        Dim mInvGenTimeTo As String
        Dim mInvoiceType As String = ""
        Dim mInvoiceTypeName As String
        Dim SORate As Double
        Dim mHSNCode As String
        Dim mInterUnit As String = ""
        Dim mGSTRegd As String = ""
        'Dim mItemCode As String							
        Dim mHSNMstCode As String
        Dim mInvPrefix As String
        Dim mCompanyGSTNo As String
        Dim mCustomerGSTNo As String = ""
        Dim cntRow As Integer
        Dim mRefNo As String
        Dim mRefDate As String
        Dim mPinCode As String
        Dim mRejDocType As String
        Dim mApplicableDate As String
        Dim mQty As Double
        Dim xMSRCost As Double
        Dim xMSPCost As Double
        Dim pChkMSRCost As Double
        Dim pChkMSPCost As Double
        Dim mMaxBillLimit As Double
        Dim mHeight As Double
        Dim mWidth As Double

        mRejDocType = IIf(IsDBNull(RsCompany.Fields("REJECTION_DOCTYPE").Value), "D", RsCompany.Fields("REJECTION_DOCTYPE").Value)
        mApplicableDate = IIf(IsDBNull(RsCompany.Fields("REJ_APPLICABLEDATE").Value), "", RsCompany.Fields("REJ_APPLICABLEDATE").Value)

        FieldsVarification = True


        '     SqlStr = SqlStr & vbCrLf & " INV_GENERATE_24_HOURS,INV_GENERATE_FROM_TM,INV_GENERATE_TO_TM"							

        If CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then
            MsgBox("Bill Date Cann't be less than GST Applicable date.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If


        If CDate(VB6.Format(txtBillDate.Text, "DD/MM/YYYY")) > CDate(VB6.Format(PubCurrDate, "DD/MM/YYYY")) Then
            MsgInformation("Bill Date is Greater Than Current Date. Cannot Save")
            txtBillDate.Focus()
            Exit Function
        End If

        If mRejDocType = "I" And (lblDespRef.Text = "Q" Or lblDespRef.Text = "L") Then 'Or lblDespRef.text = "F"							
            If CDate(txtDCDate.Text) < CDate(mApplicableDate) Then
                MsgBox("Despatch Note Date must be greater than Rejection Invoice Applicable Date.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        'mInvPrefix = IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value)

        'If mInvPrefix = "" Then
        '    MsgBox("Invoice Prefix is not Define, so cann't be Save.", MsgBoxStyle.Information)
        '    FieldsVarification = False
        '    Exit Function
        'End If

        If RsCompany.Fields("INV_GENERATE_24_HOURS").Value = "N" And ADDMode = True Then
            mCurrentTime = GetServerTime()
            mInvGenTimeFrom = VB6.Format(IIf(IsDBNull(RsCompany.Fields("INV_GENERATE_FROM_TM").Value), "", RsCompany.Fields("INV_GENERATE_FROM_TM").Value), "HH:MM")
            mInvGenTimeTo = VB6.Format(IIf(IsDBNull(RsCompany.Fields("INV_GENERATE_TO_TM").Value), "", RsCompany.Fields("INV_GENERATE_TO_TM").Value), "HH:MM")

            If CDate(mCurrentTime) < CDate(mInvGenTimeFrom) Or CDate(mCurrentTime) > CDate(mInvGenTimeTo) Then
                MsgBox("You Cann't be Generate Invoice for this Time.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        mMaxBillLimit = IIf(IsDBNull(RsCompany.Fields("BILLAMOUNT_LIMIT").Value), 0, RsCompany.Fields("BILLAMOUNT_LIMIT").Value)
        If mMaxBillLimit > 0 Then
            If Val(lblNetAmount.Text) > mMaxBillLimit Then
                MsgBox("Bill Amount is Greater than Max Bill Limit set.", MsgBoxStyle.Information)
                'txtCustomer.SetFocus						
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            'txtCustomer.SetFocus						
            FieldsVarification = False
            Exit Function
        Else
            mCustomerCode = Trim(MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = IIf(IsDBNull(MasterNo), "N", MasterNo)
        End If

        If ValidateBranchLocking((txtBillDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateBookLocking(PubDBCn, CInt(ConLockSale), txtBillDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, txtBillDate.Text, (txtCustomer.Text), mCustomerCode) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsSaleMain.EOF = True Then Exit Function

        If CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then
            MsgInformation("GST Applicable So that cann't be Save in Previous Date.")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(TxtGRDate.Text) <> "" Then
            If CDate(txtBillDate.Text) > CDate(TxtGRDate.Text) Then
                MsgInformation("Bill Date Cann't be Greater than GR Date.")
                TxtGRDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MODIFYMode = True Then
            If RsSaleMain.Fields("ISTCSPAID").Value = "Y" And PubSuperUser <> "S" Then
                MsgInformation("TCS Challan made against this invoice So cann't be modified")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MODIFYMode = True And txtBillNo.Text = "" Then
            MsgInformation("Bill No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If txtDCNo.Text = "" Then
            MsgBox("DCNo is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDCNo.Focus()
            Exit Function
        End If
        If txtDCDate.Text = "" Then
            MsgBox("DCDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDCDate.Focus()
            Exit Function
        ElseIf FYChk((txtDCDate.Text)) = False Then
            FieldsVarification = False
            txtDCDate.Focus()
            Exit Function
        End If
        If txtBillDate.Text = "" Then
            MsgBox("BillDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtBillDate.Focus()
            Exit Function
        ElseIf FYChk((txtBillDate.Text)) = False Then
            FieldsVarification = False
            txtBillDate.Focus()
            Exit Function
        End If

        If MainClass.GetUserCanModify((txtBillDate.Text)) = False Then
            MsgBox("You Have Not Rights to Add or Modify back Voucher", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If CDate(txtBillDate.Text) < CDate(txtDCDate.Text) Then
            MsgBox("Bill Date Can Not be Less Than DCDate.")
            FieldsVarification = False
            txtBillDate.Focus()
            Exit Function
        End If

        If CDate(txtBillDate.Text) > CDate(txtDCDate.Text) Then
            If MsgQuestion("Bill Date is Greater Than DC Date. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        If TxtGRDate.Text <> "" Then
            If FYChk((TxtGRDate.Text)) = False Then
                FieldsVarification = False
                TxtGRDate.Focus()
                Exit Function
            End If
        End If

        If RsCompany.Fields("LOCK_INVOICE_PAYTERMS").Value = "Y" And mInterUnit = "N" And (lblDespRef.Text = "P" Or lblDespRef.Text = "G") Then
            If CheckCreditDaysLocking(mCustomerCode, txtBillDate.Text, Val(lblNetAmount.Text), txtBillNoPrefix.Text & txtBillNo.Text) = True Then
                MsgBox("Credit Limit Days Already Exceeed.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If RsCompany.Fields("CREDIT_LIMIT_APP").Value = "Y" And mInterUnit = "N" And (lblDespRef.Text = "P" Or lblDespRef.Text = "G") Then
            Dim mCreditLimit As Double = 0
            Dim mLedgerBalance As Double = 0
            Dim mTempCreditLimit As Double = 0
            Dim mIsGroupLimit As String = "N"
            Dim xSqlStr As String
            Dim RsTempLtd As ADODB.Recordset = Nothing

            If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "GROUP_LIMIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "") = True Then
                mIsGroupLimit = MasterNo
            End If

            If mIsGroupLimit = "N" Then
                If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "CREDIT_LIMIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCreditLimit = Val(MasterNo)
                End If
            Else
                SqlStr = " SELECT MAX(CREDIT_LIMIT) AS CREDIT_LIMIT FROM FIN_SUPP_CUST_MST" & vbCrLf _
                    & " WHERE GROUPCODE = (SELECT DISTINCT GROUPCODE FROM FIN_SUPP_CUST_MST" & vbCrLf _
                    & " WHERE SUPP_CUST_CODE='" & mCustomerCode & "')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempLtd, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTempLtd.EOF = False Then
                    mCreditLimit = Val(IIf(IsDBNull(RsTempLtd.Fields("CREDIT_LIMIT").Value), 0, RsTempLtd.Fields("CREDIT_LIMIT").Value))
                End If
            End If

            mTempCreditLimit = GetCreditLimit(mCustomerCode, txtBillDate.Text)

                'If Val(lblNetAmount.Text) > mCreditLimit Then
                '    MsgBox("Tax Invoice Cann't be More than Credit Limit : " & mCreditLimit, MsgBoxStyle.Information)
                '    FieldsVarification = False
                '    Exit Function
                'End If

                If mTempCreditLimit > mCreditLimit Then
                    mCreditLimit = mTempCreditLimit
                End If

                mLedgerBalance = GetOpeningBal(mCustomerCode, "",,, "", "Y", "")

                mLedgerBalance = mLedgerBalance + IIf(ADDMode = True, Val(lblNetAmount.Text), 0)

                If Val(mLedgerBalance) > mCreditLimit Then
                    MsgBox("Ledger Balance Already Exceeed from Credit Limit : " & mCreditLimit, MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If

            End If
            '    If ADDMode = True Then							
            '        If lblDespRef.text = "Q" Or lblDespRef.text = "L" Then							
            '            If CheckPartyWiseBillExp(Trim(cboInvType.Text), Trim(txtCustomer.Text), "S") = False Then							
            '                MsgInformation "Party Wise Bill Expenses Not Defined For This Invoice Type. Please Enter this First."							
            '                FieldsVarification = False							
            '                Exit Function							
            '            End If							
            '        End If							
            '    End If							

            If (lblDespRef.Text = "Q" Or lblDespRef.Text = "L") Then

            If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then

            Else
                If Trim(txtDNNo.Text) = "" Then
                    MsgBox("Please enter the Valid Debit Note No", MsgBoxStyle.Information)
                    FieldsVarification = False
                    txtDNNo.Focus()
                    Exit Function
                End If
            End If

            '        End If						
            '        If Val(lblNetAmount.text) <> Val(lblDNAmount.text) Then						
            '            If ADDMode = True Then						
            '                MsgInformation "Debit Note Net Amount Not equal To Bill Amount. Cann't be Save.."						
            '                FieldsVarification = False						
            '                Exit Function						
            '            Else						
            '                If MsgQuestion("Debit Note Net Amount not equal to Bill Amount. You Want to Continue ...") = vbNo Then						
            '                    FieldsVarification = False						
            '                    Exit Function						
            '                End If						
            '            End If						
            '        End If						
        End If

        Dim mInvtype As String
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = ColInvoiceType
                    mInvtype = .Text

                    If MainClass.ValidateWithMasterTable((txtDCNo.Text), "AUTO_KEY_DESP", "DESP_TYPE", "DSP_DESPATCH_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDespType = MasterNo
                        If mDespType = "U" Then
                            If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISSUPPBILL='N'") = True Then
                                MsgBox("Invalid Supplementary INVOICE TYPE.", MsgBoxStyle.Information)
                                FieldsVarification = False
                                Exit Function
                            End If
                        Else
                            If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISSUPPBILL='Y'") = True Then
                                MsgBox("Please Unselect Supplementary INVOICE TYPE.", MsgBoxStyle.Information)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If

                        If mDespType = "F" Then
                            If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISFIXASSETS='N'") = True Then
                                MsgBox("Invalid Fixed Assets INVOICE TYPE.", MsgBoxStyle.Information)
                                FieldsVarification = False
                                Exit Function
                            End If
                        Else
                            '            If lblDespRef.text = "Q" Or lblDespRef.text = "L" Then					
                            '            Else					
                            If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISFIXASSETS='Y'") = True Then
                                MsgBox("Please Unselect Fixed Assets INVOICE TYPE.", MsgBoxStyle.Information)
                                FieldsVarification = False
                                Exit Function
                            End If
                            '            End If					
                        End If
                    End If
                Next
            End With
        Else
            If MainClass.ValidateWithMasterTable((txtDCNo.Text), "AUTO_KEY_DESP", "DESP_TYPE", "DSP_DESPATCH_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDespType = MasterNo
                If mDespType = "U" Then
                    If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISSUPPBILL='N'") = True Then
                        MsgBox("Invalid Supplementary INVOICE TYPE.", MsgBoxStyle.Information)
                        cboInvType.Focus()
                        FieldsVarification = False
                        Exit Function
                    End If
                Else
                    If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISSUPPBILL='Y'") = True Then
                        MsgBox("Please Unselect Supplementary INVOICE TYPE.", MsgBoxStyle.Information)
                        cboInvType.Focus()
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

                If mDespType = "F" Then
                    If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISFIXASSETS='N'") = True Then
                        MsgBox("Invalid Fixed Assets INVOICE TYPE.", MsgBoxStyle.Information)
                        cboInvType.Focus()
                        FieldsVarification = False
                        Exit Function
                    End If
                Else
                    '            If lblDespRef.text = "Q" Or lblDespRef.text = "L" Then					
                    '            Else					
                    If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISFIXASSETS='Y'") = True Then
                        MsgBox("Please Unselect Fixed Assets INVOICE TYPE.", MsgBoxStyle.Information)
                        cboInvType.Focus()
                        FieldsVarification = False
                        Exit Function
                    End If
                    '            End If					
                End If
            End If
        End If


        'If CDbl(lblInvoiceSeq.Text) <> 9 Then
        '    If mDespType = "F" Or mDespType = "P" Or mDespType = "S" Or mDespType = "U" Or mDespType = "G" Or mDespType = "J" Then
        '        If MainClass.ValidateWithMasterTable((lblPoNo.Text), "AUTO_KEY_SO", "AUTO_KEY_SO", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'") = False Then
        '            MsgBox("Sale Order in Not in GST Regime.", MsgBoxStyle.Information)
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        'End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            SprdMain.Row = 1
            SprdMain.Col = ColInvoiceType
            mInvtype = SprdMain.Text
            txtCreditAccount.Text = GetDebitNameOfInvType(Trim(SprdMain.Text), "Y")
        Else
            mInvtype = cboInvType.Text
        End If

        If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "ISSALECOMP", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISSCRAPSALE='N'") Then
            mIsSaleComp = MasterNo
        Else
            mIsSaleComp = "N"
        End If


        If mIsSaleComp = "Y" Then
            If mDespType = "P" Or mDespType = "S" Or mDespType = "E" Or mDespType = "Q" Or mDespType = "L" Then

            Else
                MsgBox("You make a Despatch Note in General, So cann't be save in Sale Components.", MsgBoxStyle.Information)
                FieldsVarification = False
                If cboInvType.Enabled = True Then cboInvType.Focus()
                Exit Function
            End If
        Else
            If mDespType = "P" Then
                MsgBox("You make a Despatch Note in Production, So please select Sale Components.", MsgBoxStyle.Information)
                FieldsVarification = False
                If cboInvType.Enabled = True Then cboInvType.Focus()
                Exit Function
            End If
        End If


        If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "ISSALEJW", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
            mIsJW = MasterNo
        Else
            mIsJW = "N"
        End If

        If mDespType = "Q" Or mDespType = "L" Then
            If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "ISSTOCKTRF", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") Then
                mIsStockTransfer = MasterNo
            Else
                mIsStockTransfer = "N"
            End If
        Else
            If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "ISSTOCKTRF", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
                mIsStockTransfer = MasterNo
            Else
                mIsStockTransfer = "N"
            End If
        End If




        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgInformation("Customer Name is not a Supplier or Customer Category. Cannot Save")
            If txtCustomer.Enabled = True Then txtCustomer.Focus()
            FieldsVarification = False
            Exit Function
        End If

        'If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mIsWithinState = IIf(IsDBNull(MasterNo), "N", MasterNo)
        'End If

        mIsWithinState = GetPartyBusinessDetail(mCustomerCode, Trim(txtBillTo.Text), "WITHIN_STATE")



        'If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mIsWithinCountry = MasterNo
        'End If

        mIsWithinCountry = GetPartyBusinessDetail(mCustomerCode, Trim(txtBillTo.Text), "WITHIN_COUNTRY")

        mGSTRegd = GetPartyBusinessDetail(mCustomerCode, Trim(txtBillTo.Text), "GST_RGN_NO")

        'If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mGSTRegd = IIf(IsDBNull(MasterNo), "N", MasterNo)
        'End If

        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INTER_UNIT='" & mIsStockTransfer & "'") = False Then
            If mIsStockTransfer = "Y" Then
                MsgBox("Customer is not a Inter Unit. Please select Correct Invoice Type.", MsgBoxStyle.Information)
            Else
                MsgBox("Customer is a Inter Unit. Please select Correct Invoice Type.", MsgBoxStyle.Information)
            End If

            FieldsVarification = False
            If cboInvType.Enabled = True Then cboInvType.Focus()
            Exit Function
        End If


        If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "ISSPD", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
            mIsSPD = MasterNo
        Else
            mIsSPD = "N"
        End If

        If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "ISSCRAPSALE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
            mIsScrapSale = MasterNo
        Else
            mIsScrapSale = "N"
        End If

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)

        mCustomerGSTNo = GetPartyBusinessDetail(mCustomerCode, Trim(txtBillTo.Text), "GST_RGN_NO")

        'If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mCustomerGSTNo = IIf(IsDBNull(MasterNo), "", MasterNo)
        'End If

        If lblInvoiceSeq.Text = "3" Or lblInvoiceSeq.Text = "5" Then

            'mIsWithinState,mInterUnit,mGSTRegd						
            If mInterUnit = "N" Then
                MsgBox("Please check. Supplier is not a Inter Unit.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            If mIsWithinState = "Y" And mInterUnit = "Y" And (Trim(mCompanyGSTNo) = Trim(mCustomerGSTNo)) Then
                Dim mRGPNo As String
                Dim mTillDate As String
                mTillDate = DateAdd("d", -2, txtBillDate.Text)
                Dim RsTemp As ADODB.Recordset = Nothing

                SqlStr = "SELECT BILLNO " & vbCrLf _
                   & " FROM FIN_INVOICE_HDR" & vbCrLf _
                   & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf _
                   & " AND IS_GATENTRY_MADE='N' AND CANCELLED='N'" & vbCrLf _
                   & " AND INVOICE_DATE >= TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                   & " AND INVOICE_DATE <= TO_DATE('" & VB6.Format(mTillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    Do While RsTemp.EOF = False
                        mRGPNo = IIf(mRGPNo = "", "", mRGPNo & ", ") & IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                        RsTemp.MoveNext()
                    Loop
                    MsgInformation("Following Invoices (" & mRGPNo & ") are pending for more than 24 Hours, so Cann't be save.")
                    FieldsVarification = False
                    Exit Function
                End If
            Else
                If mIsWithinState = "Y" And mInterUnit = "Y" And (Trim(mCompanyGSTNo) <> Trim(mCustomerGSTNo)) Then
                    MsgBox("Delivery Challan (Stock Transfer) made only for same GST No.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                ElseIf mIsWithinState = "N" And mInterUnit = "Y" Then
                    MsgBox("Delivery Challan (Stock Transfer) made only for Within State.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                ElseIf mGSTRegd = "Y" Then
                    MsgBox("Delivery Challan Cann't be made for GST Regd Customer/ Vendor.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                ElseIf mGSTRegd = "N" And mIsWithinState = "N" Then
                    MsgBox("Delivery Challan Cann't be made for Inter State Customer/ Vendor.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        ElseIf lblInvoiceSeq.Text = "6" Or Val(lblInvoiceSeq.Text) = 7 Then
            If mIsWithinCountry = "Y" Then
                MsgBox("Please Select The Valid Customer. It is not Overseas Customer.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        Else
            If mIsWithinCountry = "N" Then
                MsgBox("Please Select The Valid Customer. It is Overseas Customer.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            If mIsWithinState = "Y" And mInterUnit = "Y" And (Trim(mCompanyGSTNo) = Trim(mCustomerGSTNo)) Then
                MsgBox("Please made Bill of Supply (Stock Transfer) For Such Customer / Vendor.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
                '        ElseIf mGSTRegd = "N" Then					
                '            MsgBox "Tax Invoice Cann't be made for GST unRegd Customer/ Vendor.", vbInformation					
                '            FieldsVarification = False					
                '            Exit Function					
            End If
            If (Trim(mCompanyGSTNo) = Trim(mCustomerGSTNo)) Then
                MsgBox("Please Check GST No. Customer's GST No and Out GST No. is Same.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If mDespType = "J" Or mDespType = "R" Then
            If mIsJW = "N" Then
                MsgBox("You make a Despatch Note in Jobwork, So please select Job Work Invoice Type.", MsgBoxStyle.Information)
                FieldsVarification = False
                If cboInvType.Enabled = True Then cboInvType.Focus()
                Exit Function
            End If
        End If

        If mIsJW = "Y" Then
            If Trim(txtServProvided.Text) = "" Then
                MsgBox("Please Select The Valid Service.", MsgBoxStyle.Information)
                FieldsVarification = False
                If cboInvType.Enabled = True Then cboInvType.Focus()
                Exit Function
            End If
        End If


        If Trim(txtCustomer.Text) = "" Then
            MsgBox("Customer Cannot Be Blank", MsgBoxStyle.Information)
            ' txtCustomer.SetFocus						
            FieldsVarification = False
            Exit Function
        End If

        If lblDespRef.Text = "Q" Or lblDespRef.Text = "L" Then 'Or lblDespRef.text = "F"							
            'If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISSALERETURN='Y'") = False Then
            '    MsgBox("Invalid Rejection INVOICE TYPE.", MsgBoxStyle.Information)
            '    cboInvType.Focus()
            '    FieldsVarification = False
            '    Exit Function
            'End If
        Else
            If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = False Then
                MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
                cboInvType.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISSALERETURN='N'") = False Then
                MsgBox("You Selected Rejection INVOICE TYPE. Either You click on Rejection or Select another Invoice Type.", MsgBoxStyle.Information)
                cboInvType.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If


        If ADDMode = True Then
            If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='C'") = True Then
                MsgBox("Customer Master is Closed, So cann't be saved", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtBillNo.Enabled = True Then txtBillNo.Focus()
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOP_INVOICE='Y'") = True Then
                MsgBox("Cann't Make Invoice For Such Customer, So cann't be saved", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtBillNo.Enabled = True Then txtBillNo.Focus()
                Exit Function
            End If
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgInformation("Bill To is blank. Cannot Save")
            If txtBillTo.Enabled = True Then txtBillTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustomerCode) & "'") = False Then
                MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                If txtBillTo.Enabled = True Then txtBillTo.Focus()
                FieldsVarification = False
            End If
        End If
        Dim mShippedCustomerCode As String = ""

        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtShippedTo.Text) = "" Then
                MsgInformation("Please Select Shipped To Supplier Name. Cannot Save")
                FieldsVarification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Shipped To Supplier Name. Cannot Save")
                If txtShippedTo.Enabled = True Then txtShippedTo.Focus()
                FieldsVarification = False
                Exit Function
            Else
                mShippedCustomerCode = MasterNo
            End If
        Else
            mShippedCustomerCode = mCustomerCode
        End If

        If Trim(TxtShipTo.Text) = "" Then
            MsgInformation("Ship To is blank. Cannot Save")
            If TxtShipTo.Enabled = True Then TxtShipTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(TxtShipTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShippedCustomerCode) & "'") = False Then
                MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                If TxtShipTo.Enabled = True Then TxtShipTo.Focus()
                FieldsVarification = False
            End If
        End If

        If txtStoreDetail.Text <> "" Then
            If MainClass.ValidateWithMasterTable((txtStoreDetail.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Store Detail. Cannot Save")
                If txtStoreDetail.Enabled = True Then txtStoreDetail.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If txtApplicant.Text <> "" Then
            If MainClass.ValidateWithMasterTable((txtApplicant.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Applicant. Cannot Save")
                If txtApplicant.Enabled = True Then txtApplicant.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
        '    SprdMain.Row = 1
        '    SprdMain.Col = ColInvoiceType
        '    txtCreditAccount.Text = GetDebitNameOfInvType(Trim(SprdMain.Text), "Y")

        'Else
        If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Credit Account Does Not Exist In Master", MsgBoxStyle.Information)
            'txtCreditAccount.SetFocus						
            FieldsVarification = False
            Exit Function
        End If
        'End If


        '    If LblBookCode.text = ConExportSalesBookCode Then							

        If mDespType = "Q" Or mDespType = "L" Then
            If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") Then
                mBookSubType = MasterNo
            Else
                mBookSubType = CStr(-1)
            End If
        Else
            If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
                mBookSubType = MasterNo
            Else
                mBookSubType = CStr(-1)
            End If
        End If


        Dim mTCSApp As String = ""
        Dim mCompanyPANNo As String
        Dim mCustomerPANNo As String
        Dim mPANAvilable As String
        Dim mTurnOver As Double
        Dim mTCSRate As Double
        Dim mBillTCSRate As Double
        Dim mTurnoverExceed As Boolean
        Dim mModel As String

        If RsCompany.Fields("TCS_APPLICABLE").Value = "Y" Then
            mTurnoverExceed = False
            mCompanyPANNo = IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)

            If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                mCustomerPANNo = MasterNo
            Else
                mCustomerPANNo = "N"
            End If

            mPANAvilable = IIf(Trim(mCustomerPANNo) = "", "N", "Y")

            If mCompanyPANNo = mCustomerPANNo Or mDespType = "J" Or mDespType = "R" Or lblDespRef.Text = "Q" Or lblDespRef.Text = "L" Or mIsScrapSale = "Y" Then
                mTCSApp = "N"
            ElseIf MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "TCS_NOT_APP", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TCS_NOT_APP='Y'") Then
                mTCSApp = "N"
            Else
                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "TCS_APP", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mTCSApp = MasterNo
                End If
                mTCSApp = IIf(mTCSApp = "", "N", mTCSApp)
                If mTCSApp = "N" Then
                    mTurnOver = GetCurrentTurnOver(mCustomerCode, txtBillNoPrefix.Text & txtBillNo.Text, VB6.Format(txtBillDate.Text, "DD/MM/YYYY"), mCompanyPANNo, mCustomerPANNo)
                    mTurnOver = mTurnOver + Val(lblNetAmount.Text)
                    If mTurnOver > 5000000 Then
                        mTurnoverExceed = True
                        mTCSApp = "Y"
                    Else
                        mTCSApp = "N"
                    End If
                End If
            End If

            If mTCSApp = "Y" Then
                mTCSRate = GetTCSRate(mPANAvilable, VB6.Format(txtBillDate.Text, "DD/MM/YYYY"))
                If Val(lblTCSPercentage.Text) <> mTCSRate Then
                    MsgInformation("Invalid TCS Rate. Cannot Save")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If


        '    If mBookSubType = "J" Or mBookSubType = "M" Then							
        '							
        '    ElseIf Trim(txtItemType.Text) = "" Then							
        '        MsgBox "Item Type Cann't be blank.", vbInformation							
        '        FieldsVarification = False							
        '        txtItemType.SetFocus							
        '        Exit Function							
        '    End If							

        '    If mBookSubType = "J" And Trim(txtServProvided.Text) = "" Then							
        '        MsgBox "Service Provided Cann't be blank.", vbInformation							
        '        FieldsVarification = False							
        '        txtServProvided.SetFocus							
        '        Exit Function							
        '    End If							


        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If mDespType = "U" Then
            If MainClass.ValidDataInGrid(SprdMain, ColRate, "N", "Please Check Rate.") = False Then FieldsVarification = False : Exit Function
        Else
            If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function
            If chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidDataInGrid(SprdMain, ColRate, "N", "Please Check Rate.") = False Then FieldsVarification = False : Exit Function
                If MainClass.ValidDataInGrid(SprdMain, ColAmount, "N", "Please Check Amount.") = False Then FieldsVarification = False : Exit Function
            End If
        End If

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
        '    If MainClass.ValidDataInGrid(SprdMain, ColActualWidth, "N", "Please Check Actual Width.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColActualHeight, "N", "Please Check Actual Height.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColChargeableWidth, "N", "Please Chargeable Width.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColChargeableHeight, "N", "Please Chargeable Height.") = False Then FieldsVarification = False : Exit Function
        'End If






        If chkTaxOnMRP.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MainClass.ValidDataInGrid(SprdMain, ColMRP, "N", "Please Check MRP.") = False Then FieldsVarification = False : Exit Function
        End If

        mRMCustomer = False
        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER='CUSTOMER-RM'") = True Then
            mRMCustomer = True
        End If


        If mRMCustomer = True Then
            With SprdMain
                For cntRow = 1 To .MaxCols - 1
                    .Col = ColUnit
                    mUOM = Trim(.Text)
                    If mUOM = "KGS" Or mUOM = "TON" Or mUOM = "MT" Then
                        .Col = ColNoOfStrip
                        If Val(.Text) = 0 Then
                            MsgInformation("Please Enter the total No of Strip")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                Next
            End With
        End If


        If MainClass.ValidateWithMasterTable(mInvtype, "NAME", "ISSALECOMP", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
            mIsSaleComp = MasterNo
        Else
            mIsSaleComp = "N"
        End If

        If chkDespatchFrom.CheckState = System.Windows.Forms.CheckState.Checked Then
            If Trim(txtShippedFrom.Text) = "" Then
                MsgBox("Despatch From Address Cann't be blank.", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtShippedFrom.Enabled = True Then txtShippedFrom.Focus()
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable((txtShippedFrom.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Shipped From Supplier Name. Cannot Save")
                If txtShippedFrom.Enabled = True Then txtShippedFrom.Focus()
                FieldsVarification = False
                Exit Function
            End If

        End If

        If mIsJW = "Y" Then
            mHSNCode = ""

            If MainClass.ValidateWithMasterTable((txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                mHSNCode = Trim(IIf(IsDBNull(MasterNo), "", MasterNo))
            Else
                MsgBox("Invalid SAC Code.", MsgBoxStyle.Information)
                If cboInvType.Enabled = True Then cboInvType.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If mHSNCode = "" Then
                MsgBox("SAC Code is Blank. Please check Service.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If

            With SprdMain
                For mRow = 1 To .MaxRows
                    .Row = mRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    If mItemCode <> "" Then
                        SprdMain.Col = ColHSNCode
                        SprdMain.Text = mHSNCode
                    End If
                Next
            End With
        Else
            Dim mHSNPrevious As String = ""
            Dim xInvoiceType As String
            With SprdMain
                SprdMain.Row = 1
                SprdMain.Col = ColInvoiceType
                xInvoiceType = SprdMain.Text

                For mRow = 1 To .MaxRows
                    .Row = mRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    If mItemCode <> "" Then
                        SprdMain.Col = ColHSNCode
                        mHSNCode = Trim(UCase(SprdMain.Text))
                        If mHSNCode = "" Then
                            MsgInformation("HSN Cann't be Blank.")
                            FieldsVarification = False
                            Exit Function
                        End If
                        If MainClass.ValidateWithMasterTable(Trim(mHSNCode), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                            'mHSNMstCode = Trim(IIf(IsDBNull(MasterNo), "", MasterNo))
                            'If mHSNMstCode <> Trim(mHSNCode) Then
                            MsgBox("Please Check HSN Code for Item Code : " & Trim(.Text))
                            FieldsVarification = False
                            Exit Function
                            'End If
                        End If

                        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And mHSNPrevious <> "" Then
                            If mHSNCode <> mHSNPrevious Then
                                MsgBox("Different HSN Code Found, Cann't be Save." & Trim(.Text))
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If

                        mHSNPrevious = mHSNCode

                        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                            If Len(mHSNCode) <> 8 Then
                                MsgBox("HSN Code must be Eight Digit, Cann't be Save." & Trim(.Text))
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If

                        SprdMain.Col = ColInvoiceType
                        If Trim(SprdMain.Text) = "" Then
                            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then

                                If (lblInvoiceSeq.Text = 9 Or lblInvoiceSeq.Text = 6 Or lblInvoiceSeq.Text = 5 Or lblInvoiceSeq.Text = 7) And xInvoiceType <> "" Then
                                    SprdMain.Text = xInvoiceType
                                Else
                                    MsgBox("Please Select The Invoice Type.")
                                    MainClass.SetFocusToCell(SprdMain, mRow, ColInvoiceType)
                                    FieldsVarification = False
                                End If

                            Else
                                SprdMain.Text = Trim(cboInvType.Text)
                            End If
                        End If
                    End If
                Next
            End With
        End If

        ''Check Not Required 11/03/2019							

        '    If lblInvoiceSeq.text = "9" Then							
        '        With SprdMain							
        '            .Row = 1							
        '            .Col = Col57F4							
        '            mRefNo = Trim(UCase(.Text))							
        '							
        '            If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_INVOICE", "INVOICE_DATE", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then							
        '                mRefDate = MasterNo							
        '            End If							
        '							
        '            If CDate(mRefDate) < CDate(PubGSTApplicableDate) Then							
        '                GoTo GotoNextRowSupp							
        '            End If							
        '            For mRow = 1 To .MaxRows - 1							
        '                .Row = mRow							
        '                .Col = ColItemCode							
        '                mItemCode = Trim(.Text)							
        '                If mItemCode <> "" Then							
        '                    .Col = Col57F4							
        '                    If mRefNo <> Trim(UCase(.Text)) Then							
        '                        MsgInformation "Please Select Single Original Invoice."							
        '                        FieldsVarification = False							
        '                        Exit Function							
        '                    End If							
        '                 End If							
        '            Next							
        '        End With							
        '    End If							
GotoNextRowSupp:

        'SprdMain.Col = ColHSN							
        '            mHSNCode = Trim(UCase(SprdMain.Text))							
        '            If mGSTClass = "0" Then							
        '                If mHSNCode = "" Then							
        '                    MsgInformation "HSN Cann't be Blank."							
        '                    FieldsVarification = False							
        '                    Exit Function							
        '                Else							
        '                    If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, IIf(chkReverseCharge.Value = vbChecked, "Y", "N")) = False Then GoTo err							
        '                End If							
        '							
        '							
        '                If chkGSTApplicable.Value = vbChecked And (pCGSTPer + pSGSTPer + pIGSTPer) = 0 Then							
        '                    MsgInformation "GST % is not Defined for Item Code : " & mItemCode							
        '                    FieldsVarification = False							
        '                    MainClass.SetFocusToCell SprdMain, I, ColItemCode							
        '                    Exit Function							
        '                End If							
        '            End If							

        '    If mIsSaleComp = "Y" Then							
        '        If Trim(txtTariff.Text) = "" Then							
        '            MsgBox "Please Check Tariff Heading"							
        '            FieldsVarification = False							
        '            Exit Function							
        '        Else							
        '            If RsCompany.fields("FYEAR").value > 2004 Then							
        '                With SprdMain							
        '                    For mRow = 1 To .MaxRows							
        '                        .Row = mRow							
        '                        .Col = ColItemCode							
        '                        If MainClass.ValidateWithMasterTable(Trim(.Text), "ITEM_CODE", "TARIFF_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then							
        '                            mTariffCode = Trim(IIf(IsNull(MasterNo), "", MasterNo))							
        '                            If mTariffCode <> Trim(txtTariff.Text) Then							
        '                                MsgBox "Please Check Tariff Heading for Item Code : " & Trim(.Text)							
        '                                FieldsVarification = False							
        '                                Exit Function							
        '                            End If							
        '                        End If							
        '                    Next							
        '                End With							
        '            End If							
        '        End If							
        '    End If							

        If mDespType = "L" Or mDespType = "Q" Then
            If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then

            Else
                With SprdMain
                    For mRow = 1 To .MaxRows
                        .Row = mRow
                        .Col = ColItemCode
                        mItemCode = Trim(.Text)

                        .Col = ColRate
                        mItemRate = Val(.Text)

                        If CheckItemRateWithDN(mItemCode, mItemRate) = False Then
                            MsgInformation("Invoice Rate is not Match with Debit Note Rate for Item Code - " & mItemCode & ". Please Check.")
                            FieldsVarification = False
                            Exit Function
                        End If
                    Next
                End With
            End If

        End If

        pChkMSPCost = 0
        pChkMSRCost = 0
        Dim mStoreLoc As String = ""
        Dim mSNo As Double = 0
        If mDespType = "Q" Or mDespType = "L" Or mDespType = "S" Or mDespType = "U" Or mDespType = "R" Then
        Else
            With SprdMain
                For mRow = 1 To 1
                    .Row = mRow
                    mSNo = mRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    .Col = ColUnit
                    mUOM = Trim(.Text)

                    .Col = ColQty
                    mQty = Val(.Text)

                    .Col = ColModel
                    mModel = Trim(.Text)

                    .Col = ColChargeableHeight
                    mHeight = Val(.Text)

                    .Col = ColChargeableWidth
                    mWidth = Val(.Text)


                    Dim pChargeableArea As Double
                    Dim pAreaRate As Double
                    Dim pQty As Double
                    Dim pRate As Double

                    .Col = ColChargeableArea
                    pChargeableArea = Val(.Text)

                    .Col = ColAreaRate
                    pAreaRate = Val(.Text)

                    .Col = ColQty
                    pQty = Val(.Text)

                    .Col = ColRate
                    pRate = Val(.Text)

                    mStoreLoc = GetCustomerStoreLoc(mSNo, Val(txtDCNo.Text), mItemCode)

                    If pChargeableArea * pAreaRate * pQty > 0 Then
                        If Math.Abs(Val(pChargeableArea * pAreaRate * pQty) - Val(pRate * pQty)) > 1 Then
                            MsgInformation("SQM Rate is not Match with Pcs Rate for Item Code : " & mItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    If CheckInvoiceReceiptPending(mCustomerCode, mItemCode, mStoreLoc) = True Then
                        MsgInformation("Customer : " & txtCustomer.Text & " Receipt is pending for Item Code : " & mItemCode)
                        FieldsVarification = False
                        Exit Function
                    End If

                    If CheckRTVPending(mCustomerCode, mItemCode, txtBillDate.Text, mStoreLoc) = True Then
                        MsgInformation("Customer : " & txtCustomer.Text & " RTV is pending for Item Code : " & mItemCode)
                        FieldsVarification = False
                        Exit Function
                    End If

                    If CheckDDR(mCustomerCode, mItemCode, txtBillDate.Text, mStoreLoc) = True Then
                        MsgInformation("Customer : " & txtCustomer.Text & " DDR for Item Code : " & mItemCode)
                        FieldsVarification = False
                        Exit Function
                    End If


                    If mDespType = "P" Or mDespType = "S" Then
                        pChkMSPCost = pChkMSPCost + (GetSORate(mItemCode, mCustomerCode, mDespType, "MSP", "", mUOM, 0, 0, 0, "", "", mHeight, mWidth, mModel) * mQty)
                        pChkMSRCost = pChkMSRCost + (GetSORate(mItemCode, mCustomerCode, mDespType, "MSR", "", mUOM, 0, 0, 0, "", "", mHeight, mWidth, mModel) * mQty)
                    End If

                    SORate = GetSORate(mItemCode, mCustomerCode, mDespType, "N", "", mUOM, 0, 0, 0, mInvoiceType, "", mHeight, mWidth, mModel)
                    mInvoiceTypeName = ""
                    If mInvoiceType <> "" Then
                        If MainClass.ValidateWithMasterTable(mInvoiceType, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
                            mInvoiceTypeName = MasterNo
                        End If
                        If Trim(mInvoiceTypeName) <> Trim(cboInvType.Text) Then
                            If MsgQuestion("Invoice Type not Match with Defined Invoice Type. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If
                Next
            End With
        End If


        xMSRCost = 0
        xMSPCost = 0
        If mDespType = "P" Then
            With SprdExp
                For cntRow = 1 To .MaxRows
                    .Row = cntRow

                    .Col = ColExpSTCode
                    mExpCode = CStr(Val(.Text))
                    If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "IDENTIFICATION", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mIndentificationCode = MasterNo
                    Else
                        mIndentificationCode = ""
                    End If

                    If mIndentificationCode = "MSC" Then
                        .Col = ColExpAmt
                        xMSPCost = xMSPCost + CDbl(VB6.Format(.Text, "0.00"))
                    End If
                    If mIndentificationCode = "MSR" Then
                        .Col = ColExpAmt
                        xMSRCost = xMSRCost + CDbl(VB6.Format(.Text, "0.00"))
                    End If
                Next
            End With
        End If

        If lblInvoiceSeq.Text = 9 Then

        Else
            If RsCompany.Fields("PACKETS_COL_SHOW").Value = "Y" Then
                With SprdMain
                    For cntRow = 1 To .MaxRows
                        .Row = cntRow

                        .Col = ColInnerBoxQty
                        If Val(.Text) > 0 Then
                            .Col = ColPackType
                            If Trim(.Text) = "" Then
                                MsgBox("You not define Packing Type of Line No : " & cntRow)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    Next
                End With
            End If
        End If




        '    If xMSPCost <> xMSRCost Then							
        '        If MsgQuestion("Add & Less Material Supply by Party is not Match. You Want to Continue ...") = vbNo Then							
        '            FieldsVarification = False							
        '            Exit Function							
        '        End If							
        '    End If							


        If VB6.Format(xMSPCost, "0.00") <> VB6.Format(pChkMSPCost, "0.00") Then
            MsgInformation("Material Supply Cost is not match with PO Cost")
            FieldsVarification = False
            Exit Function
        End If

        If VB6.Format(xMSRCost, "0.00") <> VB6.Format(pChkMSRCost, "0.00") Then
            MsgInformation("Material Supply Cost is not match with PO Cost")
            FieldsVarification = False
            Exit Function
        End If


        If mIsScrapSale = "Y" Then
            If Val(lblTCS.Text) = 0 Then
                If MsgQuestion("TCS Amount is Zero. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If


        If mIsWithinCountry = "N" Then

            'If Trim(txtBuyerName.Text) = "" Then
            '    If MsgQuestion("You not Defined Buyer. Do You Want to Continue ...") = CStr(MsgBoxResult.No) Then
            '        FieldsVarification = False
            '        Exit Function
            '    End If
            'End If

            'If Trim(txtShippingNo.Text) = "" Then
            '    MsgInformation("Shipping No cann't be blank. Cann't be Saved.")
            '    FieldsVarification = False
            '    Exit Function
            'End If

            'If Trim(txtShippingDate.Text) = "" Then
            '    MsgInformation("Shipping Date cann't be blank. Cann't be Saved.")
            '    FieldsVarification = False
            '    Exit Function
            'End If

            'If Trim(txtARE1No.Text) = "" Then
            '    MsgInformation("ARE1 No cann't be blank. Cann't be Saved.")
            '    FieldsVarification = False
            '    Exit Function
            'End If

            'If Trim(txtARE1Date.Text) = "" Then
            '    MsgInformation("ARE1 Date cann't be blank. Cann't be Saved.")
            '    FieldsVarification = False
            '    Exit Function
            'End If
            If lblInvoiceSeq.Text = "7" Then

            Else
                If Trim(txtPortCode.Text) = "" And CDate(txtBillDate.Text) >= CDate("01/07/2022") Then
                    MsgInformation("Port Code cann't be blank. Cann't be Saved.")
                    FieldsVarification = False
                    Exit Function
                End If

                If Trim(txtExportBillNo.Text) = "" Then
                    MsgInformation("Export Invoice No cann't be blank. Cann't be Saved.")
                    FieldsVarification = False
                    Exit Function
                End If

                If Trim(txtExportBillDate.Text) = "" Then
                    MsgInformation("Export Invoice Date cann't be blank. Cann't be Saved.")
                    FieldsVarification = False
                    Exit Function
                End If

                If Val(txtExchangeRate.Text) = 0 Then
                    MsgInformation("Exchange Rate cann't be blank. Cann't be Saved.")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If Val(txtAdvBal.Text) > 0 And Val(txtAdvAdjust.Text) = 0 Then
            If MsgQuestion("Customer has advance Payment, Want to adjust with this voucher.") = CStr(MsgBoxResult.Yes) Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Val(txtAdvBal.Text) > 0 Then
            If Val(txtAdvBal.Text) < Val(txtAdvAdjust.Text) Then
                MsgBox("Advance Balance is Less than Advance Adjusted, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Val(txtAdvCGST.Text) > 0 Then
            If Val(txtAdvCGST.Text) > Val(txtAdvCGSTBal.Text) Then
                MsgBox("CGST Advance is Greater Than Balance CGST Advance Value, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            If Val(txtAdvCGST.Text) <> Val(lblTotCGSTAmount.Text) Then
                MsgBox("CGST Advance is not Match with CGST Advance Value, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Val(txtAdvSGST.Text) > 0 Then
            If Val(txtAdvSGST.Text) > Val(txtAdvSGSTBal.Text) Then
                MsgBox("SGST Advance is Greater Than Balance SGST Advance Value, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            If Val(txtAdvSGST.Text) <> Val(lblTotSGSTAmount.Text) Then
                MsgBox("SGST Advance is not Match with SGST Advance Value, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Val(txtAdvIGST.Text) > 0 Then
            If Val(txtAdvIGST.Text) > Val(txtAdvIGSTBal.Text) Then
                MsgBox("IGST Advance is Greater Than Balance IGST Advance Value, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            If Val(txtAdvIGST.Text) <> Val(lblTotIGSTAmount.Text) Then
                MsgBox("IGST Advance is not Match with IGST Advance Value, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        'If Val(txtDistance.Text) = 0 And ADDMode = True Then
        '    MsgBox("Please enter the party distance from our Premises, So cann't be Saved.", MsgBoxStyle.Information)
        '    FieldsVarification = False
        '    Exit Function
        'End If

        mPinCode = ""
        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_PIN", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPinCode = MasterNo
        End If

        If Val(mPinCode) = 0 Then
            MsgBox("Party's PinCode is not defined Correct in Master, So cann't be Saved.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If CheckTCSApplication(mCustomerCode, (lblDespRef.Text), VB6.Format(txtBillDate.Text, "DD/MM/YYYY")) = True Then
            If ADDMode = True Then
                With SprdExp
                    For cntRow = 1 To .MaxRows
                        .Row = cntRow
                        .Col = ColExpSTCode
                        mExpCode = Val(.Text)
                        If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "IDENTIFICATION", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mIndentificationCode = MasterNo
                        Else
                            mIndentificationCode = ""
                        End If
                        If mIndentificationCode = "TCS" Then
                            .Col = ColExpAmt
                            If Val(.Text) <= 0 Then
                                MsgBox("Party's sale is more than 50 Lakh, Please select the TCS % or Tick TCS Not applicable in Master, So cann't be Saved.", MsgBoxStyle.Information)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If

                    Next
                End With
            End If
        End If



        If CheckDespatchQty() = False Then
            MsgInformation("Despatch Qty Not Match with Invoice Qty. Cann't be Saved.")
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume							
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmInvoiceGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    Me.text = IIf(LblBookCode.text = ConSalesBookCode, "Invoice", "Excise Export Invoice")							

        '    Me.text = IIf(lblInvoiceSeq.text = 1, "Tax Invoice", IIf(lblInvoiceSeq.text = 2, "Jobwork Invoice", "Bill of Supply"))							

        If Val(lblInvoiceSeq.Text) = 9 Then
            Me.Text = "Supplementary Invoice (Credit Note)"
        ElseIf Val(lblInvoiceSeq.Text) = 7 Then
            Me.Text = "Export Supplementary Invoice (Credit Note)"
        ElseIf Val(lblInvoiceSeq.Text) = 5 Then
            Me.Text = "Delivery Challan Supplementary Invoice (Internal Memo)"
        Else
            Me.Text = IIf(Val(lblInvoiceSeq.Text) = 1, "Tax Invoice", IIf(Val(lblInvoiceSeq.Text) = 2, "Jobwork Invoice", IIf(Val(lblInvoiceSeq.Text) = 3, "Delivery Challan for Supply", "Tax Invoice Export")))
        End If

        SqlStr = ""
        SqlStr = "Select * from FIN_INVOICE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from FIN_INVOICE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from FIN_INVOICE_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleExp, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from FIN_TRADING_TRN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleTrading, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mCustomerCode = CStr(-1)

        FillCboSaleType()

        cboTransmode.Items.Clear()
        cboTransmode.Items.Add("1. Road")
        cboTransmode.Items.Add("2. Rail")
        cboTransmode.Items.Add("3. Air")
        cboTransmode.Items.Add("4. Ship")
        cboTransmode.SelectedIndex = 0

        cboVehicleType.Items.Clear()
        cboVehicleType.Items.Add("Regular")
        cboVehicleType.Items.Add("Over Dimensional Cargo")
        cboVehicleType.SelectedIndex = 0

        'JB = New JsonBag
        'JB.Whitespace = System.Windows.Forms.CheckState.Checked

        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:

        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '   Resume							
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo AssignGridErr
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)

        SqlStr = ""

        SqlStr = "SELECT FIN_INVTYPE_MST.NAME AS INVOICE_TYPE,BILLNOPREFIX,TO_CHAR(BILLNOSEQ),BILLNOSUFFIX, " & vbCrLf _
            & " BILLNO,INVOICE_DATE  AS BILLDATE, TO_CHAR(INV_PREP_TIME,'HH24:MI') AS BILLTIME, " & vbCrLf _
            & " AUTO_KEY_DESP AS DCNO, DCDATE AS DCDATE, " & vbCrLf _
            & " CUST_PO_NO AS PONO, CUST_PO_DATE AS PODATE, " & vbCrLf _
            & " REMOVAL_DATE AS REMOVAL_DATE, TO_CHAR(REMOVAL_TIME,'HH24:MI') AS REMOVAL_TIME, " & vbCrLf _
            & " A.SUPP_CUST_CODE, A.SUPP_CUST_NAME AS CUSTOMER, B.SUPP_CUST_NAME AS CREDIT_ACCOUNT, " & vbCrLf _
            & " ITEMDESC, NETVALUE FROM " & vbCrLf _
            & " FIN_INVOICE_HDR, FIN_INVTYPE_MST, FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_MST B " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " FIN_INVOICE_HDR.COMPANY_CODE=FIN_INVTYPE_MST.COMPANY_CODE " & vbCrLf _
            & " AND FIN_INVOICE_HDR.TRNTYPE=FIN_INVTYPE_MST.CODE " & vbCrLf _
            & " AND FIN_INVOICE_HDR.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
            & " AND FIN_INVOICE_HDR.SUPP_CUST_CODE=A.SUPP_CUST_CODE " & vbCrLf _
            & " AND FIN_INVOICE_HDR.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
            & " AND FIN_INVOICE_HDR.ACCOUNTCODE=B.SUPP_CUST_CODE "

        SqlStr = SqlStr & vbCrLf & " AND FIN_INVOICE_HDR.BOOKCODE='" & LblBookCode.Text & "'"

        SqlStr = SqlStr & vbCrLf & " AND FIN_INVOICE_HDR.INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & ""

        '    SqlStr = SqlStr & vbCrLf & " AND FIN_INVOICE_HDR.BookSubType  IN ( "							
        '							
        '    SqlStr = SqlStr & vbCrLf _							
        ''            & " SELECT IDENTIFICATION FROM FIN_INVTYPE_MST " & vbCrLf _							
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _							
        ''            & " AND CATEGORY='S' AND ISSALERETURN='N' AND IDENTIFICATION<>'P' "							
        '							
        '    If lblInvoiceSeq.text = 1 Then							
        '        SqlStr = SqlStr & vbCrLf & " AND ISSALEJW='N'"							
        '    ElseIf lblInvoiceSeq.text = 2 Then							
        '        SqlStr = SqlStr & vbCrLf & " AND ISSALEJW='Y'"							
        '    ElseIf lblInvoiceSeq.text = 4 Then							
        '							
        '    End If							
        '							
        '    SqlStr = SqlStr & vbCrLf & ")"							

        SqlStr = SqlStr & vbCrLf _
            & " AND FIN_INVOICE_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And FIN_INVOICE_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & vbCrLf & " Order by BILLDATE DESC,BillNo DESC"

        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader("S")

        MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
        oledbAdapter.Dispose()
        oledbCnn.Close()

        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub CreateGridHeader(pShowType As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header


            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Invoice Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Bill No Prefix"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Bill Seq No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Bill No Suffix"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Bill Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Bill Time"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "DC No"

            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "DC Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Customer PO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "Customer PO Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "Removal Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Header.Caption = "Removal Time"
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Header.Caption = "Customer Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Header.Caption = "Customer Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Header.Caption = "Credit Account Name"

            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Header.Caption = "Item Description"
            UltraGrid1.DisplayLayout.Bands(0).Columns(17).Header.Caption = "Net Amount"


            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).CellAppearance.TextHAlign = HAlign.Right

            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Hidden = True

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 125
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(17).Width = 90

            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            'fill labels 
            'FillLabelsFromResFile(Me)
            'Catch sqlex As SqlException
            '    ErrorTrap(sqlex.Message, "frmRMReturn.vb", "CreateHeader", "", "", "Sql Exception")
            '    Me.Cursor = Windows.Forms.Cursors.Default
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Sub FormatSprdView()

        'With SprdView
        '    .Row = -1

        '    .set_RowHeight(0, 600)

        '    .set_ColWidth(0, 600)
        '    .set_ColWidth(1, 2000)
        '    .set_ColWidth(2, 0)
        '    .set_ColWidth(3, 0)
        '    .set_ColWidth(4, 0)

        '    .set_ColWidth(5, 1200)
        '    .set_ColWidth(6, 1200)
        '    .set_ColWidth(7, 1200)
        '    .set_ColWidth(8, 1200)
        '    .set_ColWidth(9, 1000)
        '    .set_ColWidth(10, 1000)
        '    .set_ColWidth(11, 1000)
        '    .set_ColWidth(12, 1000)
        '    .set_ColWidth(13, 1000)
        '    .set_ColWidth(14, 3000)
        '    .set_ColWidth(15, 500 * 6)
        '    .set_ColWidth(16, 500 * 2)
        '    .set_ColWidth(17, 500 * 2)
        '    .set_ColWidth(18, 500 * 2)
        '    .Col = 18
        '    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

        '    MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
        '    MainClass.SetSpreadColor(SprdView, -1)
        '    SprdView.set_RowHeight(-1, 300)
        '    .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        '    MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        'End With
    End Sub
    Private Sub FormatSprdExp(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdExp
            .Row = Arow
            .set_RowHeight(Arow, 10)
            pShowCalc = False
            .Col = 0
            .ColHidden = True

            .Col = ColExpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColExpName, 25)
            .TypeEditMultiLine = False

            .Col = ColExpPercent
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = 0.0#
            .TypeFloatMax = 99.999
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpPercent, 8)
            .TypeEditMultiLine = False

            .Col = ColExpAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpAmt, 11)
            .TypeEditMultiLine = False

            .Col = ColExpSTCode
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMin = CDbl("-9999999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True

            .Col = ColExpAddDeduct 'ExpFlag (For Add or Deduct) Hidden Column						
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColExpIdent
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColTaxable
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColExciseable
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True

            SprdExp.Col = ColExpCalcOn
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColRO
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColRO, 2)

            .Col = ColDutyForgone
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColDutyForgone, 2)
            .ColHidden = True

            pShowCalc = True
            MainClass.ProtectCell(SprdExp, 1, .MaxRows, ColExpName, ColExpName)
        End With
        MainClass.SetSpreadColor(SprdExp, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume							
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim xCustCode As String
        Dim mDivCode As Double
        Dim mCustType As String

        mDivCode = -1
        If Trim(cboDivision.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivCode = Val(MasterNo)
            End If
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            SprdMain.Row = 0
            SprdMain.Col = ColQty
            SprdMain.Text = "Qty/UOM"

            SprdMain.Col = ColInnerBoxQty
            SprdMain.Text = "Coil Qty"

            SprdMain.Col = ColInnerBoxCode
            SprdMain.Text = "Coil Box Code"
        End If

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSaleDetail.Fields("ITEM_CODE").DefinedSize ''						
            .set_ColWidth(.Col, 8)

            .Col = ColItemSNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("ITEM_SNO").DefinedSize
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, True, False)
            '.ColsFrozen = ColItemSNo
            .set_ColWidth(.Col, 6)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("CUSTOMER_PART_NO").DefinedSize
            .ColsFrozen = ColPartNo
            .set_ColWidth(.Col, 10)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("Item_Desc").DefinedSize ''						
            .set_ColWidth(.Col, 20)

            .Col = ColJITCallNo
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeNumberMax = CDbl("9999999")
            .TypeNumberMin = CDbl("0")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColJITCallNo, 6)
            .ColHidden = True

            .Col = ColAddItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '.TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("ADD_ITEM_DESCRIPTION").DefinedSize
            .ColHidden = True
            .set_ColWidth(.Col, 10)

            .Col = ColODNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '.TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("OD_NO").DefinedSize
            .ColHidden = True
            .set_ColWidth(.Col, 10)

            .Col = ColHeatNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '.TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("HEAT_NO").DefinedSize
            .ColHidden = True
            .set_ColWidth(.Col, 10)

            .Col = ColBatchNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '.TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("BATCH_NO").DefinedSize
            .ColHidden = True
            .set_ColWidth(.Col, 10)

            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            '.TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '.TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("MRR_REF_NO").Precision
            .set_ColWidth(.Col, 8)

            .Col = Col57F4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("REF_NO", "DSP_DESPATCH_DET", PubDBCn)
            .set_ColWidth(.Col, 8)



            .Col = Col57F4Date
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = 10
            .set_ColWidth(.Col, 8)
            .ColHidden = False

            '        .CellType = SS_CELL_TYPE_FLOAT						
            '        .TypeFloatDecimalPlaces = 2						
            '        .TypeFloatDecimalChar = Asc(".")						
            '        .TypeFloatMax = "99999999999.99"						
            '        .TypeFloatMin = "-99999999999.99"						
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC						

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsSaleDetail.Fields("ITEM_UOM").DefinedSize ''						
            .set_ColWidth(.Col, 5)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            .Col = ColMRP
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(.Col, 9)

            .Col = ColNoOfStrip
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .ColHidden = True
            .set_ColWidth(.Col, 9)

            .Col = ColStripRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .ColHidden = True
            .set_ColWidth(.Col, 9)

            .Col = ColTaxableAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(.Col, 9)

            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsSaleDetail.Fields("HSNCODE").Precision 'MainClass.SetMaxLength("HSN_CODE", "GEN_HSN_MST", PubDBCn)						
            '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE						
            '        .TypeHAlign = SS_CELL_H_ALIGN_CENTER						
            .set_ColWidth(.Col, 8)

            .Col = ColCGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSaleDetail.Fields("CGST_PER").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 3 Or CDbl(lblInvoiceSeq.Text) = 5, True, False)

            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSaleDetail.Fields("SGST_PER").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 3 Or CDbl(lblInvoiceSeq.Text) = 5, True, False)

            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSaleDetail.Fields("IGST_PER").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 3 Or CDbl(lblInvoiceSeq.Text) = 5, True, False)

            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 3 Or CDbl(lblInvoiceSeq.Text) = 5, True, False)

            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 3 Or CDbl(lblInvoiceSeq.Text) = 5, True, False)

            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 3 Or CDbl(lblInvoiceSeq.Text) = 5, True, False)

            .Col = ColPackType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '.TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("PACK_TYPE").DefinedSize
            .ColHidden = IIf(RsCompany.Fields("PACKETS_COL_SHOW").Value = "Y", False, True)
            .set_ColWidth(.Col, 10)


            .Col = ColInnerBoxQty
            .CellType = SS_CELL_TYPE_FLOAT
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                .TypeFloatDecimalPlaces = 2
            Else
                .TypeFloatDecimalPlaces = 0
            End If

            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSaleDetail.Fields("INNER_PACK_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = IIf(RsCompany.Fields("INNER_PACK_COL_SHOW").Value = "Y" Or RsCompany.Fields("PACKETS_COL_SHOW").Value = "Y", False, True)

            .Col = ColInnerBoxQtyA
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSaleDetail.Fields("INNER_PACK_QTY_A").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = IIf(RsCompany.Fields("INNER_PACK_COL_SHOW").Value = "Y", IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104, True, False), True)

            .Col = ColInnerBoxCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSaleDetail.Fields("INNER_PACK_ITEM_CODE").DefinedSize ''						
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("INNER_PACK_COL_SHOW").Value = "Y", False, True)

            .Col = ColOuterBoxQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSaleDetail.Fields("OUTER_PACK_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = IIf(RsCompany.Fields("OUTER_PACK_COL_SHOW").Value = "Y", False, True)

            .Col = ColOuterBoxQtyA
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSaleDetail.Fields("OUTER_PACK_QTY_A").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = IIf(RsCompany.Fields("OUTER_PACK_COL_SHOW").Value = "Y", False, True)

            .Col = ColOuterBoxCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSaleDetail.Fields("OUTER_PACK_ITEM_CODE").DefinedSize ''						
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("OUTER_PACK_COL_SHOW").Value = "Y", False, True)

            .Col = ColInvoiceType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("NAME", "FIN_INVTYPE_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 24)

            .Col = ColAccountName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 24)

            .ColsFrozen = ColItemDesc

            .Col = ColGlassDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("GLASS_DESC").DefinedSize ''				
            .set_ColWidth(.Col, 20)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColModel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("ITEM_MODEL").DefinedSize ''				
            .set_ColWidth(.Col, 10)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            For cntCol = ColActualWidth To ColChargeableHeight
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 7)
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Next

            For cntCol = ColChargeableArea To ColAreaRate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 7)
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Next

        End With

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColAmount)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemCode)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColActualArea)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColChargeableArea, ColChargeableArea)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQty, ColQty)
            'ColAreaRate
        Else
            If mDivCode = 6 Then
                MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColAmount)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemCode)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColQty)
            Else
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColQty)
            End If
        End If


        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColTaxableAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMRP, ColMRP)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColIGSTAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStripRate, ColStripRate)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAccountName, ColAccountName)


        If MainClass.ValidateWithMasterTable(Val(txtDCNo.Text), "AUTO_KEY_DESP", "SUPP_CUST_CODE", "DSP_DESPATCH_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DESP_TYPE IN ('P','J','F','G','S')") = True Then
            xCustCode = Trim(MasterNo)
            If MainClass.ValidateWithMasterTable(xCustCode, "SUPP_CUST_CODE", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND WITHIN_COUNTRY='Y'") = True Then
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRate, ColRate)
            End If

            If MainClass.ValidateWithMasterTable(xCustCode, "SUPP_CUST_CODE", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER='CUSTOMER-RM'") = True Then
                SprdMain.Col = ColNoOfStrip
                SprdMain.ColHidden = False

                SprdMain.Col = ColStripRate
                SprdMain.ColHidden = False
            Else
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColNoOfStrip, ColNoOfStrip)
            End If

        End If

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub

ERR1:
        If Err.Number = -2147418113 Then RsSaleDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsSaleMain
            ' TxtDCNoPrefix.MaxLength = 0						
            txtDCNo.MaxLength = .Fields("AUTO_KEY_DESP").Precision ''						
            'txtDCNoSuffix.MaxLength = 0						
            txtDCDate.MaxLength = 10
            txtBillNoPrefix.MaxLength = .Fields("BillNoPrefix").DefinedSize ''						
            txtBillNo.MaxLength = .Fields("AUTO_KEY_INVOICE").Precision ''						
            txtBillNoSuffix.MaxLength = .Fields("BillNoSuffix").DefinedSize ''						
            txtBillDate.MaxLength = 10
            TxtBillTm.MaxLength = 5
            txtRemovalDate.MaxLength = 10
            txtRemovalTime.MaxLength = 5
            txtCustomer.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtCreditAccount.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            TxtGRNo.MaxLength = .Fields("GRNo").DefinedSize ''						
            TxtGRDate.MaxLength = 10
            txtCreditDays(0).MaxLength = .Fields("DUEDAYSFROM").Precision ''						
            txtCreditDays(1).MaxLength = .Fields("DUEDAYSTO").Precision ''						
            '        txtExciseNo.MaxLength = .Fields("EXCISEDEBITNO").Precision     ''						
            txtExchangeRate.MaxLength = .Fields("EXCHANGE_RATE").Precision
            '        txtExciseDate.MaxLength = 10						
            txtTariff.MaxLength = .Fields("TARIFFHEADING").DefinedSize ''						
            '        txtST38No.MaxLength = .Fields("ST_38_NO").DefinedSize						
            '        TxtCTNo.MaxLength = .Fields("CT_NO").Precision   ''						
            '        txtCT1No.MaxLength = .Fields("CT1_NO").Precision						
            txtItemType.MaxLength = .Fields("ItemDesc").DefinedSize ''						
            txtRemarks.MaxLength = .Fields("Remarks").DefinedSize ''						
            txtNarration.MaxLength = .Fields("NARRATION").DefinedSize ''						
            txtCarriers.MaxLength = .Fields("CARRIERS").DefinedSize ''						
            txtVehicle.MaxLength = .Fields("VehicleNo").DefinedSize ''						
            txteRefNo.MaxLength = .Fields("E_REFNO").DefinedSize
            txtDocsThru.MaxLength = .Fields("DocsThrough").DefinedSize ''						
            txtMode.MaxLength = .Fields("DespatchMode").DefinedSize ''						


            txtTransportCode.MaxLength = .Fields("TRANSPORTER_GSTNO").DefinedSize
            txtDistance.MaxLength = .Fields("TRANS_DISTANCE").Precision
            txtResponseId.MaxLength = .Fields("EWAYRESPONSEID").DefinedSize
            txtEWayBillNo.MaxLength = .Fields("E_BILLWAYNO").DefinedSize



            '						
            '						
            '        txtFormRecvName.MaxLength = .Fields("STFORMNAME").DefinedSize           ''						
            '        txtFormRecvNo.MaxLength = .Fields("STFORMNO").DefinedSize           ''						
            '        txtFormRecvDate.MaxLength = 10						
            '        txtFormDueName.MaxLength = .Fields("STDUEFORMNAME").DefinedSize           ''						
            '        txtFormDueNo.MaxLength = .Fields("STDUEFORMNO").DefinedSize           ''						
            '        txtFormDueDate.MaxLength = 10						
            txtServProvided.MaxLength = .Fields("SERV_PROV").DefinedSize

            txtShippingNo.MaxLength = .Fields("SHIPPING_NO").DefinedSize
            txtVendorCode.MaxLength = .Fields("VENDOR_CODE").DefinedSize
            txtPacking.MaxLength = .Fields("PACKING_DETAILS").DefinedSize

            txtBuyerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtCoBuyerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

            txtShippingDate.MaxLength = 10
            txtARE1No.MaxLength = .Fields("ARE1_NO").DefinedSize
            txtARE1Date.MaxLength = 10
            txtExportBillNo.MaxLength = .Fields("EXPBILLNO").DefinedSize
            txtPortCode.MaxLength = .Fields("PORT_CODE").DefinedSize
            txtExportBillDate.MaxLength = 10

            txtTotalEuro.MaxLength = .Fields("TOTEXCHANGEVALUE").DefinedSize
            txtAdvLicense.MaxLength = .Fields("ADV_LICENSE").DefinedSize
            txtLocation.MaxLength = .Fields("DESP_LOCATION").DefinedSize
            txtProcessNature.MaxLength = .Fields("NATURE").DefinedSize

            txtPOAmendNo.MaxLength = .Fields("AMEND_NO").DefinedSize
            txtPOWEFDate.MaxLength = 10
            txtSuppFromDate.MaxLength = 10
            txtSuppToDate.MaxLength = 10

            txtShippedTo.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

            txtStoreDetail.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtApplicant.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

            txtAdvVNo.MaxLength = .Fields("ADV_VNO").DefinedSize
            txtAdvDate.MaxLength = .Fields("ADV_VDATE").DefinedSize
            txtItemAdvAdjust.MaxLength = .Fields("ADV_ITEM_AMT").Precision
            txtAdvAdjust.MaxLength = .Fields("ADV_ADJUSTED_AMT").Precision
            txtAdvCGST.MaxLength = .Fields("ADV_CGST_AMT").Precision
            txtAdvSGST.MaxLength = .Fields("ADV_SGST_AMT").Precision
            txtAdvIGST.MaxLength = .Fields("ADV_IGST_AMT").Precision

            txtShippedFrom.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)


        End With

        Exit Sub
ERR1:
        '    Resume							
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mTaxOnMRP As String
        Dim mBuyerCode As String
        Dim mCoBuyerCode As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mShippedToCode As String
        Dim mShippedToName As String

        Dim mShippedFromCode As String
        Dim mShippedFromName As String

        Dim mSACCode As String
        Dim mBillNo As String
        Dim mBalCGST As Double
        Dim mBalSGST As Double
        Dim mBalIGST As Double
        Dim mTransMode As Integer
        Dim mVehicleType As String
        Dim mTRNType As String
        Dim mTRNTypeName As String
        Dim mAccountCode As String

        Dim mStoreDetailCode As String = ""
        Dim mStoreDetailName As String = ""
        Dim mApplicantCode As String = ""
        Dim mApplicantName As String = ""

        pShowCalc = False
        With RsSaleMain
            If Not .EOF Then
                txtDCNo.Enabled = False
                LblMKey.Text = .Fields("mKey").Value

                TxtDCNoPrefix.Text = ""
                txtDCNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_DESP").Value), "", .Fields("AUTO_KEY_DESP").Value)
                txtDCNoSuffix.Text = ""
                txtDCDate.Text = IIf(IsDBNull(.Fields("DCDATE").Value), "", .Fields("DCDATE").Value)

                '***					
                lblPoNo.Text = IIf(IsDBNull(.Fields("OUR_AUTO_KEY_SO").Value), "", .Fields("OUR_AUTO_KEY_SO").Value)
                lblSoDate.Text = IIf(IsDBNull(.Fields("OUR_SO_DATE").Value), "", .Fields("OUR_SO_DATE").Value)
                lblDespRef.Text = IIf(IsDBNull(.Fields("REF_DESP_TYPE").Value), "", .Fields("REF_DESP_TYPE").Value)

                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False

                '
                'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 And RsCompany.Fields("FYEAR").Value = 2023 And mBillNoSeq < 100 Then
                '    txtBillNoPrefix.Text = IIf(IsDBNull(.Fields("BILLNOPREFIX").Value), "", .Fields("BILLNOPREFIX").Value)
                'Else
                '    txtBillNoPrefix.Text = GetDocumentPrefix("S", lblInvoiceSeq.Text, cboDivision.Text)
                'End If


                ''***			
                mTRNType = IIf(IsDBNull(.Fields("TRNTYPE").Value), "", .Fields("TRNTYPE").Value)
                mTRNTypeName = ""
                If MainClass.ValidateWithMasterTable(mTRNType, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                    cboInvType.Text = MasterNo
                    mTRNTypeName = MasterNo
                End If

                If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "INV_HEADING", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                    lblInvHeading.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If

                If lblDespRef.Text = "Q" Or lblDespRef.Text = "L" Then
                    If MainClass.ValidateWithMasterTable(mTRNTypeName, "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") Then
                        mBookSubType = MasterNo
                    Else
                        mBookSubType = CStr(-1)
                    End If
                Else
                    If MainClass.ValidateWithMasterTable(mTRNTypeName, "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
                        mBookSubType = MasterNo
                    Else
                        mBookSubType = CStr(-1)
                    End If
                End If


                If mBookSubType = "J" Or mBookSubType = "M" Then
                    chkPrintType.CheckState = System.Windows.Forms.CheckState.Unchecked
                    '                ChkPaintPrint.Value = vbChecked				
                Else
                    chkPrintType.CheckState = System.Windows.Forms.CheckState.Checked
                    '            ChkPaintPrint.Value = vbUnchecked				
                End If
                ChkPaintPrint.CheckState = System.Windows.Forms.CheckState.Unchecked
                chkJWDetail.CheckState = System.Windows.Forms.CheckState.Unchecked

                mBillNo = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)

                txtBillNoPrefix.Text = IIf(IsDBNull(.Fields("BILLNOPREFIX").Value), "", .Fields("BILLNOPREFIX").Value)

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 And RsCompany.Fields("FYEAR").Value = 2023 And RsCompany.Fields("COMPANY_CODE").Value = 1 And IIf(IsDBNull(.Fields("BILLNOSEQ").Value), 0, .Fields("BILLNOSEQ").Value) < 100 Then
                    txtBillNo.Text = VB6.Format(IIf(IsDBNull(.Fields("BILLNOSEQ").Value), 0, .Fields("BILLNOSEQ").Value), "0")
                Else
                    txtBillNo.Text = VB6.Format(IIf(IsDBNull(.Fields("BILLNOSEQ").Value), "", .Fields("BILLNOSEQ").Value), ConBillFormat)
                End If

                txtBillNoSuffix.Text = IIf(IsDBNull(.Fields("BILLNOSUFFIX").Value), "", .Fields("BILLNOSUFFIX").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                '            txtProddate = Format(IIf(IsNull(!PRDDate), "", !PRDDate), "DD/MM/YYYY")					
                TxtBillTm.Text = VB6.Format(IIf(IsDBNull(.Fields("INV_PREP_TIME").Value), "", .Fields("INV_PREP_TIME").Value), "HH:MM")
                txtRemovalDate.Text = VB6.Format(IIf(IsDBNull(.Fields("REMOVAL_DATE").Value), "", .Fields("REMOVAL_DATE").Value), "DD/MM/YYYY")
                txtRemovalTime.Text = VB6.Format(IIf(IsDBNull(.Fields("REMOVAL_TIME").Value), "", .Fields("REMOVAL_TIME").Value), "HH:MM")

                txtIRNNo.Text = IIf(IsDBNull(.Fields("IRN_NO").Value), "", .Fields("IRN_NO").Value)
                txteInvAckNo.Text = IIf(IsDBNull(.Fields("IRN_ACK_NO").Value), "", .Fields("IRN_ACK_NO").Value)
                txteInvAckDate.Text = VB6.Format(IIf(IsDBNull(.Fields("IRN_ACK_DATE").Value), "", .Fields("IRN_ACK_DATE").Value), "DD/MM/YYYY HH:MM")

                If Trim(txtIRNNo.Text) = "" Then
                    cmdeInvoice.Enabled = True ' IIf(PubUserID = "EINV", True, IIf(RsCompany.Fields("E_INVOICE_APP").Value = "Y", True, False))
                Else
                    cmdeInvoice.Enabled = False
                End If

                mCustomerCode = .Fields("SUPP_CUST_CODE").Value

                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustomer.Text = MasterNo
                End If

                chkShipTo.CheckState = IIf(.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mShippedToCode = IIf(IsDBNull(.Fields("SHIPPED_TO_PARTY_CODE").Value), -1, .Fields("SHIPPED_TO_PARTY_CODE").Value)
                mShippedToName = ""
                If MainClass.ValidateWithMasterTable(mShippedToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mShippedToName = MasterNo
                End If

                txtShippedTo.Text = mShippedToName



                mStoreDetailCode = IIf(IsDBNull(.Fields("SUPP_CUST_STORE_CODE").Value), "", .Fields("SUPP_CUST_STORE_CODE").Value)
                mStoreDetailName = ""
                If mStoreDetailCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mStoreDetailCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mStoreDetailName = MasterNo
                    End If
                End If

                txtStoreDetail.Text = mStoreDetailName


                mApplicantCode = IIf(IsDBNull(.Fields("SUPP_CUST_APPLICANT_CODE").Value), "", .Fields("SUPP_CUST_APPLICANT_CODE").Value)
                mApplicantName = ""
                If mApplicantCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mApplicantCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mApplicantName = MasterNo
                    End If
                End If

                txtApplicant.Text = mApplicantName

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                    TxtShipTo.Text = IIf(IsDBNull(.Fields("SHIP_TO_LOC_ID").Value), "", .Fields("SHIP_TO_LOC_ID").Value)
                    txtVendorCode.Text = IIf(IsDBNull(.Fields("VENDOR_CODE").Value), "", .Fields("VENDOR_CODE").Value)
                    txtPacking.Text = IIf(IsDBNull(.Fields("PACKING_DETAILS").Value), "", .Fields("PACKING_DETAILS").Value)

                    If txtBillTo.Text <> "" Then
                        If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "SUPP_CUST_ADDR || ',' || SUPP_CUST_CITY || ',' || SUPP_CUST_STATE || ',' || ' GST NO :' || GST_RGN_NO", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustomerCode) & "'") = True Then
                            txtAddress.Text = MasterNo
                        End If
                    Else
                        txtAddress.Text = ""
                    End If

                    mBuyerCode = IIf(IsDBNull(.Fields("BUYER_CODE").Value), "", .Fields("BUYER_CODE").Value)
                    If mBuyerCode <> "" Then
                        If MainClass.ValidateWithMasterTable(mBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            txtBuyerName.Text = MasterNo
                        End If
                    End If

                    mCoBuyerCode = IIf(IsDBNull(.Fields("CO_BUYER_CODE").Value), "", .Fields("CO_BUYER_CODE").Value)
                    If mCoBuyerCode <> "" Then
                        If MainClass.ValidateWithMasterTable(mCoBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            txtCoBuyerName.Text = MasterNo
                        End If
                    End If

                    mAccountCode = IIf(IsDBNull(.Fields("ACCOUNTCODE").Value), "", .Fields("ACCOUNTCODE").Value)

                    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtCreditAccount.Text = MasterNo
                    End If

                    TxtGRNo.Text = IIf(IsDBNull(.Fields("GRNO").Value), "", .Fields("GRNO").Value)
                    TxtGRDate.Text = IIf(IsDBNull(.Fields("GRDATE").Value), "", .Fields("GRDATE").Value)
                    txtCreditDays(0).Text = IIf(IsDBNull(.Fields("DUEDAYSFROM").Value), "", .Fields("DUEDAYSFROM").Value)
                    txtCreditDays(1).Text = IIf(IsDBNull(.Fields("DUEDAYSTO").Value), "", .Fields("DUEDAYSTO").Value)

                    chkCancelled.CheckState = IIf(.Fields("Cancelled").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    If PubUserID = "G0416" Then
                        chkCancelled.Enabled = IIf(.Fields("Cancelled").Value = "Y", False, True)
                    Else
                        chkCancelled.Enabled = False ''IIf(!Cancelled = "Y", False, True)				
                    End If

                chkByHand.CheckState = IIf(.Fields("BY_HAND").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                chkFOC.CheckState = IIf(.Fields("FOC").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkLUT.CheckState = IIf(.Fields("IS_LUT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    chkLUT.Enabled = False
                    chkRejection.CheckState = IIf(.Fields("REJECTION").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                    '            chkRegDealer.Value = IIf(!ISREGDNO = "Y", vbChecked, vbUnchecked)					
                    '            chkD3.Value = IIf(!AGTD3 = "Y", vbChecked, vbUnchecked)					
                    '            chkCT3.Value = IIf(!AGTCT3 = "Y", vbChecked, vbUnchecked)					
                    '            chkCT1.Value = IIf(!AGTCT1 = "Y", vbChecked, vbUnchecked)					
                    chkAgtPermission.CheckState = IIf(.Fields("AGT_Permission").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    '            cmdCTCertificate.Enabled = IIf(chkCT3.Value = vbChecked, True, False)					

                    '            txtARENo.Text = IIf(IsNull(!ARE_NO), "0", !ARE_NO)					
                    chkPackmat.CheckState = IIf(.Fields("PACK_MAT_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    chkChallanMade.CheckState = IIf(.Fields("CHALLAN_MADE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    chkStockTrf.CheckState = IIf(.Fields("ISSTOCKTRF").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    chkDutyFreePurchase.CheckState = IIf(.Fields("AGT_DUTYFREE_PUR").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    lblTotQty.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.000")
                    lblTotItemValue.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                    '            lblTotED.text = Format(IIf(IsNull(!TOTEDAMOUNT), 0, !TOTEDAMOUNT), "0.00")					
                    '            lblTotST.text = Format(IIf(IsNull(!TOTSTAMT), 0, !TOTSTAMT), "0.00")					
                    lblNetAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")

                    '            lblEDUAmount.text = Format(IIf(IsNull(!TOTEDUAMOUNT), 0, !TOTEDUAMOUNT), "0.00")					
                    '            lblEDUPercent.text = Format(IIf(IsNull(!TOTEDUPERCENT), 0, !TOTEDUPERCENT), "0.00")					

                    '            lblSHECAmount.text = Format(IIf(IsNull(!TOTSHECAMOUNT), 0, !TOTSHECAMOUNT), "0.00")					
                    '            lblSHECPercent.text = Format(IIf(IsNull(!TOTSHECPERCENT), 0, !TOTSHECPERCENT), "0.00")					

                    '            lblServiceAmount.text = Format(IIf(IsNull(!TOTSERVICEAMOUNT), 0, !TOTSERVICEAMOUNT), "0.00")					
                    '            lblServicePercentage.text = Format(IIf(IsNull(!TOTSERVICEPERCENT), 0, !TOTSERVICEPERCENT), "0.00")					


                    mSACCode = IIf(IsDBNull(.Fields("SAC_CODE").Value), "", .Fields("SAC_CODE").Value)

                    If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                        txtServProvided.Text = Trim(IIf(IsDBNull(MasterNo), "", MasterNo))
                    Else
                        txtServProvided.Text = ""
                    End If

                    lblTotExportExp.Text = VB6.Format(IIf(IsDBNull(.Fields("TOT_EXPORTEXP").Value), 0, .Fields("TOT_EXPORTEXP").Value), "0.00")


                    lblTotCD.Text = VB6.Format(IIf(IsDBNull(.Fields("TOT_CUSTOMDUTY").Value), 0, .Fields("TOT_CUSTOMDUTY").Value), "0.00")
                    lblEDUOnCDAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("TOT_CD_CESS").Value), 0, .Fields("TOT_CD_CESS").Value), "0.00")
                    '            lblCDPer.text = Format(IIf(IsNull(!CD_PER), 0, !CD_PER), "0.00")					
                    '            lblCessOnCDPer.text = Format(IIf(IsNull(!CD_CESS_PER), 0, !CD_CESS_PER), "0.00")					

                    '            cboExciseEntry.Text = IIf(IsNull(!EXCISEDEBITTYPE), "", !EXCISEDEBITTYPE)					
                    '            txtExciseNo.Text = IIf(IsNull(!EXCISEDEBITNO), "", !EXCISEDEBITNO)					
                    '            txtExciseDate.Text = IIf(IsNull(!EXCISEDEBITDATE), "", !EXCISEDEBITDATE)					
                    txtTariff.Text = IIf(IsDBNull(.Fields("TARIFFHEADING").Value), "", .Fields("TARIFFHEADING").Value)
                    '            txtST38No.Text = IIf(IsNull(!ST_38_NO), "", !ST_38_NO)					
                    '            TxtCTNo.Text = IIf(IsNull(!CT_NO), "", !CT_NO)					
                    '            lblCT3Date.text = Format(IIf(IsNull(!CT3_DATE), "", !CT3_DATE), "DD/MM/YYYY")					
                    '					
                    '            txtCT1No.Text = IIf(IsNull(!CT1_NO), "", !CT1_NO)					
                    '            lblCT1Date.text = Format(IIf(IsNull(!CT1_DATE), "", !CT1_DATE), "DD/MM/YYYY")					

                    txtExchangeRate.Text = VB6.Format(IIf(IsDBNull(.Fields("EXCHANGE_RATE").Value), "", .Fields("EXCHANGE_RATE").Value), "0.00")

                    '            chkDutyIncluded.Value = IIf(!DUTY_INCLUDED_ITEM = "Y", vbChecked, vbUnchecked)					
                    '            txtEDPayable.Text = Format(IIf(IsNull(!ED_PAYABLE), "", !ED_PAYABLE), "0.00")					
                    '            txtCessPayable.Text = Format(IIf(IsNull(!CESS_PAYABLE), "", !CESS_PAYABLE), "0.00")					
                    '            txtSHECPayable.Text = Format(IIf(IsNull(!SHEC_PAYABLE), "", !SHEC_PAYABLE), "0.00")					

                    txtItemType.Text = IIf(IsDBNull(.Fields("ITEMDESC").Value), "", .Fields("ITEMDESC").Value)
                    txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                    txtNarration.Text = IIf(IsDBNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)
                    txtCarriers.Text = IIf(IsDBNull(.Fields("CARRIERS").Value), "", .Fields("CARRIERS").Value)

                    txtVehicle.Text = IIf(IsDBNull(.Fields("VEHICLENO").Value), "", .Fields("VEHICLENO").Value)
                    txteRefNo.Text = IIf(IsDBNull(.Fields("E_REFNO").Value), "", .Fields("E_REFNO").Value)

                    txtDocsThru.Text = IIf(IsDBNull(.Fields("DOCSTHROUGH").Value), "", .Fields("DOCSTHROUGH").Value)
                    txtMode.Text = IIf(IsDBNull(.Fields("DESPATCHMODE").Value), "", .Fields("DESPATCHMODE").Value)

                    mTransMode = IIf(IsDBNull(.Fields("TRANSPORT_MODE").Value), 0, VB.Left(.Fields("TRANSPORT_MODE").Value, 1))
                    cboTransmode.SelectedIndex = mTransMode - 1
                    txtTransportCode.Text = IIf(IsDBNull(.Fields("TRANSPORTER_GSTNO").Value), "", .Fields("TRANSPORTER_GSTNO").Value)
                    txtDistance.Text = IIf(IsDBNull(.Fields("TRANS_DISTANCE").Value), 0, .Fields("TRANS_DISTANCE").Value)
                    txtResponseId.Text = IIf(IsDBNull(.Fields("EWAYRESPONSEID").Value), "", .Fields("EWAYRESPONSEID").Value)
                    txtEWayBillNo.Text = IIf(IsDBNull(.Fields("E_BILLWAYNO").Value), "", .Fields("E_BILLWAYNO").Value)

                    mVehicleType = IIf(IsDBNull(.Fields("VEHICLE_TYPE").Value), "", .Fields("VEHICLE_TYPE").Value)
                    cboVehicleType.SelectedIndex = IIf(mVehicleType = "R", 0, 1)


                    txtDNNo.Text = IIf(IsDBNull(.Fields("DNCNNO").Value), "", .Fields("DNCNNO").Value)
                    mDNCnNO = IIf(IsDBNull(.Fields("DNCNNO").Value), "", .Fields("DNCNNO").Value)
                    txtDNDate.Text = VB6.Format(IIf(IsDBNull(.Fields("DNCNDATE").Value), "", .Fields("DNCNDATE").Value), "DD/MM/YYYY")
                    mDNCnDate = VB6.Format(IIf(IsDBNull(.Fields("DNCNDATE").Value), "", .Fields("DNCNDATE").Value), "DD/MM/YYYY")

                    txtDNNo_Validating(txtDNNo, New System.ComponentModel.CancelEventArgs(True))

                    If .Fields("FREIGHTCHARGES").Value = "To Pay" Then
                        OptFreight(0).Checked = True
                    Else
                        OptFreight(1).Checked = True
                    End If

                    '            If !STTYPE = "0" Then					
                    '                optSTType(0).Value = True					
                    '            ElseIf !STTYPE = "1" Then					
                    '                optSTType(1).Value = True					
                    '            Else					
                    '                optSTType(2).Value = True					
                    '            End If					

                    '            txtFormRecvName.Text = IIf(IsNull(!STFORMNAME), "", !STFORMNAME)					
                    '            If MainClass.ValidateWithMasterTable(!STFORMCODE, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then					
                    '                txtFormRecvName.Text = MasterNo					
                    '            Else					
                    '                txtFormRecvName.Text = ""					
                    '            End If					
                    '					
                    '            txtFormRecvNo.Text = IIf(IsNull(!STFORMNO), "", !STFORMNO)					
                    '            txtFormRecvDate = Format(IIf(IsNull(!STFORMDATE), "", !STFORMDATE), "DD/MM/YYYY")					
                    ''            txtFormDueName.Text = IIf(IsNull(!STDUEFORMNAME), "", !STDUEFORMNAME)					
                    '            If MainClass.ValidateWithMasterTable(!STDUEFORMCODE, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then					
                    '                txtFormDueName.Text = MasterNo					
                    '            Else					
                    '                txtFormDueName.Text = ""					
                    '            End If					
                    '					
                    '            txtFormDueNo.Text = IIf(IsNull(!STDUEFORMNO), "", !STDUEFORMNO)					
                    '            txtFormDueDate = Format(IIf(IsNull(!STDUEFORMDATE), "", !STDUEFORMDATE), "DD/MM/YYYY")					

                    txtPONo.Text = IIf(IsDBNull(.Fields("CUST_PO_NO").Value), "", .Fields("CUST_PO_NO").Value)
                    txtPODate.Text = VB6.Format(IIf(IsDBNull(.Fields("CUST_PO_DATE").Value), "", .Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                    txtPOAmendNo.Text = IIf(IsDBNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)
                    txtPOWEFDate.Text = VB6.Format(IIf(IsDBNull(.Fields("AMEND_WEF_FROM").Value), "", .Fields("AMEND_WEF_FROM").Value), "DD/MM/YYYY")


                    txtSuppFromDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SUPP_FROM_DATE").Value), "", .Fields("SUPP_FROM_DATE").Value), "DD/MM/YYYY")
                    txtSuppToDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SUPP_TO_DATE").Value), "", .Fields("SUPP_TO_DATE").Value), "DD/MM/YYYY")
                    txtIntRate.Text = VB6.Format(IIf(IsDBNull(.Fields("INTRATE").Value), "0", .Fields("INTRATE").Value), "0.00")


                    txtShippingNo.Text = IIf(IsDBNull(.Fields("SHIPPING_NO").Value), "", .Fields("SHIPPING_NO").Value)
                    txtShippingDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SHIPPING_DATE").Value), "", .Fields("SHIPPING_DATE").Value), "DD/MM/YYYY")
                    txtARE1No.Text = IIf(IsDBNull(.Fields("ARE1_NO").Value), "", .Fields("ARE1_NO").Value)
                    txtARE1Date.Text = VB6.Format(IIf(IsDBNull(.Fields("ARE1_DATE").Value), "", .Fields("ARE1_DATE").Value), "DD/MM/YYYY")
                    txtPortCode.Text = IIf(IsDBNull(.Fields("PORT_CODE").Value), "", .Fields("PORT_CODE").Value)
                    txtExportBillNo.Text = IIf(IsDBNull(.Fields("EXPBILLNO").Value), "", .Fields("EXPBILLNO").Value)
                    txtExportBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("EXPINV_DATE").Value), "", .Fields("EXPINV_DATE").Value), "DD/MM/YYYY")

                    txtTotalEuro.Text = IIf(IsDBNull(.Fields("TOTEXCHANGEVALUE").Value), "", .Fields("TOTEXCHANGEVALUE").Value)
                    txtAdvLicense.Text = IIf(IsDBNull(.Fields("ADV_LICENSE").Value), "", .Fields("ADV_LICENSE").Value)
                    txtLocation.Text = IIf(IsDBNull(.Fields("DESP_LOCATION").Value), "", .Fields("DESP_LOCATION").Value)
                    txtProcessNature.Text = IIf(IsDBNull(.Fields("NATURE").Value), "", .Fields("NATURE").Value)

                    lblMRPValue.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTMRPVALUE").Value), "0", .Fields("TOTMRPVALUE").Value), "0.00")
                    txtAbatementPer.Text = VB6.Format(IIf(IsDBNull(.Fields("ABATEMENT_PER").Value), "0", .Fields("ABATEMENT_PER").Value), "0.00")
                    txtCustMatValue.Text = VB6.Format(IIf(IsDBNull(.Fields("CUST_ITEM_VALUE").Value), "0", .Fields("CUST_ITEM_VALUE").Value), "0.00")

                    txtTDSOnSale.Text = VB6.Format(IIf(IsDBNull(.Fields("TDS_ON_SALE").Value), "0", .Fields("TDS_ON_SALE").Value), "0.00")

                    mTaxOnMRP = IIf(IsDBNull(.Fields("TAX_ON_MRP").Value), "N", .Fields("TAX_ON_MRP").Value)
                    chkTaxOnMRP.CheckState = IIf(mTaxOnMRP = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    txtModvatNo.Text = IIf(IsDBNull(.Fields("MODVATNO").Value), "", .Fields("MODVATNO").Value)
                    txtModvatDate.Text = IIf(IsDBNull(.Fields("MODVATDATE").Value), "", .Fields("MODVATDATE").Value)



                    txtAdvVNo.Text = IIf(IsDBNull(.Fields("ADV_VNO").Value), "", .Fields("ADV_VNO").Value)
                    txtAdvDate.Text = IIf(IsDBNull(.Fields("ADV_VDATE").Value), "", .Fields("ADV_VDATE").Value)

                    txtAdvBal.Text = CStr(GetBalancePaymentAmount((.Fields("SUPP_CUST_CODE").Value), txtBillDate.Text, mBillNo, (txtBillDate.Text), mDivisionCode, "AR", mBalCGST, mBalSGST, mBalIGST))
                    txtAdvBal.Text = VB6.Format(txtAdvBal.Text, "0.00")

                    txtAdvCGSTBal.Text = VB6.Format(mBalCGST, "0.00")
                    txtAdvSGSTBal.Text = VB6.Format(mBalSGST, "0.00")
                    txtAdvIGSTBal.Text = VB6.Format(mBalIGST, "0.00")

                    txtItemAdvAdjust.Text = VB6.Format(IIf(IsDBNull(.Fields("ADV_ITEM_AMT").Value), 0, .Fields("ADV_ITEM_AMT").Value), "0.00")
                    txtAdvAdjust.Text = VB6.Format(IIf(IsDBNull(.Fields("ADV_ADJUSTED_AMT").Value), 0, .Fields("ADV_ADJUSTED_AMT").Value), "0.00")
                    txtAdvCGST.Text = VB6.Format(IIf(IsDBNull(.Fields("ADV_CGST_AMT").Value), 0, .Fields("ADV_CGST_AMT").Value), "0.00")
                    txtAdvSGST.Text = VB6.Format(IIf(IsDBNull(.Fields("ADV_SGST_AMT").Value), 0, .Fields("ADV_SGST_AMT").Value), "0.00")
                    txtAdvIGST.Text = VB6.Format(IIf(IsDBNull(.Fields("ADV_IGST_AMT").Value), 0, .Fields("ADV_IGST_AMT").Value), "0.00")

                    chkDespatchFrom.CheckState = IIf(.Fields("IS_DESP_OTHERTHAN_BILL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    chkExWork.CheckState = IIf(.Fields("IS_SHIPPTO_EX_WORK").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    chkDespatchFrom.Enabled = IIf(chkDespatchFrom.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                    chkExWork.Enabled = IIf(chkExWork.CheckState = System.Windows.Forms.CheckState.Checked, False, True)


                    mShippedFromCode = IIf(IsDBNull(.Fields("SHIPPED_FROM_PARTY_CODE").Value), "-1", .Fields("SHIPPED_FROM_PARTY_CODE").Value)
                    mShippedFromName = ""
                    If MainClass.ValidateWithMasterTable(mShippedFromCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mShippedFromName = MasterNo
                    End If

                    txtShippedFrom.Text = mShippedFromName
                    txtShippedFrom.Enabled = False

                    mAddUser = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                    mAddDate = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                    mModUser = IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                    mModDate = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")

                    lblAddUser.Text = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                    lblAddDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                    lblModUser.Text = IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                    lblModDate.Text = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")

                    Call ShowSaleDetail1(.Fields("AUTO_KEY_DESP").Value)
                    Call ShowSaleExp1()
                    Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))

                    '            If lblCT3Date.text = "" And chkCT3.Value = vbChecked Then					
                    '                TxtCTNo_Validate False					
                    '            End If					
                    '					
                    '            If lblCT1Date.text = "" And chkCT1.Value = vbChecked Then					
                    '                TxtCT1No_Validate False					
                    '            End If					

                    ''Call CalcTots					
                End If
        End With
        txtBillNo.Enabled = True
        chkTaxOnMRP.Enabled = False
        txtAbatementPer.Enabled = False
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        pShowCalc = True
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        FormatSprdMain(-1)

        CmdSearchDC.Enabled = False
        txtDCNoSuffix.Enabled = False
        txtDCDate.Enabled = False

        '    cboInvType.Enabled = IIf(XRIGHT = "AMDV", True, False)							
        cboInvType.Enabled = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101, False, MainClass.GetUserCanModify(txtBillDate.Text)) 'IIf(PubUserLevel = 1 Or PubUserLevel = 2, True, False)							

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Sub
    Private Sub ShowSaleExp1()

        On Error GoTo ERR1
        Dim I As Integer


        Call FillSprdExp()
        pShowCalc = False
        SqlStr = ""
        SqlStr = "Select FIN_INVOICE_EXP.EXPCODE,FIN_INVOICE_EXP.EXPPERCENT, FIN_INVOICE_EXP.DUTYFORGONE," & vbCrLf & " FIN_INVOICE_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO " & vbCrLf & " From FIN_INVOICE_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_INVOICE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_INVOICE_EXP.Mkey='" & LblMKey.Text & "'"

        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"

        '    If PubGSTApplicable = True Then							
        '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"							
        '    Else							
        '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"							
        '    End If							
        '							
        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsSaleExp.EOF = False Then
            RsSaleExp.MoveFirst()
            With SprdExp
                Do While Not RsSaleExp.EOF
                    For I = 1 To .MaxRows
                        .Row = I
                        .Col = ColExpName
                        If .Text = RsSaleExp.Fields("Name").Value Then Exit For
                    Next I

                    .Col = ColExpPercent 'Exp. %				
                    .Text = CStr(Val(IIf(IsDBNull(RsSaleExp.Fields("ExpPercent").Value), "", RsSaleExp.Fields("ExpPercent").Value)))

                    .Col = ColExpAmt
                    If RsSaleExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off				
                        .Text = CStr(Val(IIf(IsDBNull(RsSaleExp.Fields("Amount").Value), "", RsSaleExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDBNull(RsSaleExp.Fields("Amount").Value), "", RsSaleExp.Fields("Amount").Value))))
                    End If

                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDBNull(RsSaleExp.Fields("CODE").Value), 0, RsSaleExp.Fields("CODE").Value)))

                    .Col = ColExpAddDeduct 'ExpFlag				
                    .Text = IIf(RsSaleExp.Fields("Add_Ded").Value = "A", "A", "D")


                    .Col = ColExpIdent
                    .Text = IIf(IsDBNull(RsSaleExp.Fields("Identification").Value), "", RsSaleExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off				
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If

                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDBNull(RsSaleExp.Fields("Taxable").Value), "N", RsSaleExp.Fields("Taxable").Value)

                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDBNull(RsSaleExp.Fields("Exciseable").Value), "N", RsSaleExp.Fields("Exciseable").Value)

                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDBNull(RsSaleExp.Fields("CalcOn").Value), "", RsSaleExp.Fields("CalcOn").Value)))

                    SprdExp.Col = ColRO
                    SprdExp.Value = IIf(RsSaleExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    .Row = I
                    SprdExp.Col = ColDutyForgone
                    SprdExp.Value = IIf(RsSaleExp.Fields("DUTYFORGONE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    RsSaleExp.MoveNext()
                Loop
            End With
        End If
        pShowCalc = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub
    Private Sub ShowDRExp1()

        On Error GoTo ERR1
        Dim I As Integer

        Call FillSprdExp()

        SqlStr = ""
        SqlStr = "Select FIN_DNCN_EXP.EXPCODE,FIN_DNCN_EXP.EXPPERCENT, " & vbCrLf & " FIN_DNCN_EXP.AMOUNT, FIN_DNCN_EXP.RO," & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn " & vbCrLf & " From FIN_DNCN_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_DNCN_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_DNCN_EXP.Mkey='" & lblPoNo.Text & "'"

        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"

        '    If PubGSTApplicable = True Then							
        '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"							
        '    Else							
        '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"							
        '    End If							

        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsSaleExp.EOF = False Then
            RsSaleExp.MoveFirst()
            With SprdExp
                Do While Not RsSaleExp.EOF
                    For I = 1 To .MaxRows
                        .Row = I
                        .Col = ColExpName
                        If .Text = RsSaleExp.Fields("Name").Value Then Exit For
                    Next I

                    .Col = ColRO
                    .Value = IIf(RsSaleExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    .Col = ColExpPercent 'Exp. %				
                    .Text = CStr(Val(IIf(IsDBNull(RsSaleExp.Fields("ExpPercent").Value), "", RsSaleExp.Fields("ExpPercent").Value)))

                    .Col = ColExpAmt
                    If RsSaleExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off				
                        .Text = CStr(Val(IIf(IsDBNull(RsSaleExp.Fields("Amount").Value), "", RsSaleExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDBNull(RsSaleExp.Fields("Amount").Value), "", RsSaleExp.Fields("Amount").Value))))
                    End If

                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDBNull(RsSaleExp.Fields("CODE").Value), 0, RsSaleExp.Fields("CODE").Value)))

                    .Col = ColExpAddDeduct 'ExpFlag				
                    .Text = IIf(RsSaleExp.Fields("Add_Ded").Value = "A", "A", "D")

                    .Col = ColExpIdent
                    .Text = IIf(IsDBNull(RsSaleExp.Fields("Identification").Value), "", RsSaleExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off				
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If

                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDBNull(RsSaleExp.Fields("Taxable").Value), "N", RsSaleExp.Fields("Taxable").Value)

                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDBNull(RsSaleExp.Fields("Exciseable").Value), "N", RsSaleExp.Fields("Exciseable").Value)

                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDBNull(RsSaleExp.Fields("CalcOn").Value), "", RsSaleExp.Fields("CalcOn").Value)))
                    RsSaleExp.MoveNext()
                Loop
            End With
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub
    Private Sub ShowSaleDetail1(ByVal mDespatchNo As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mDivCode As Double
        Dim mHSNCode As String
        Dim pRefDate As String = ""
        Dim mItemSNo As String
        Dim mInvTypeCode As String
        Dim mInvTypeDesc As String
        Dim mCompanyCode As Long



        If Val(lblCompanyCode.Text) <= 0 Then
            mCompanyCode = RsCompany.Fields("Company_Code").Value
        Else
            mCompanyCode = Val(lblCompanyCode.Text)
        End If

        mDivCode = -1
        If Trim(cboDivision.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                mDivCode = Val(MasterNo)
            End If
        End If


        SqlStr = ""
        SqlStr = " SELECT ID.*, INVMST.ITEM_SHORT_DESC, ID.CUSTOMER_PART_NO, ID.CUSTOMER_PART_NO AS CUST_PART" & vbCrLf _
                & " FROM FIN_INVOICE_DET ID, INV_ITEM_MST INVMST " & vbCrLf _
                & " Where ID.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
                & " AND ID.Mkey='" & LblMKey.Text & "'" & vbCrLf _
                & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                & " Order By SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSaleDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemDesc
                '            MainClass.ValidateWithMasterTable mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & ""					
                '            mItemDesc = MasterNo					
                mItemDesc = IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)
                SprdMain.Text = mItemDesc ''IIf(IsNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)					

                SprdMain.Col = ColPartNo
                '            MainClass.ValidateWithMasterTable mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & ""					
                '            mPartNo = MasterNo					
                '            If mDivCode = 6 Then					
                '                mPartNo = IIf(IsNull(.Fields("CUST_PART").Value), "", .Fields("CUST_PART").Value)					
                '            Else					
                '                mPartNo = IIf(IsNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)					
                '            End If					
                mPartNo = IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)
                SprdMain.Text = mPartNo


                SprdMain.Col = ColItemSNo
                mItemSNo = IIf(IsDBNull(.Fields("ITEM_SNO").Value), "", .Fields("ITEM_SNO").Value)
                SprdMain.Text = mItemSNo

                SprdMain.Col = ColGlassDescription
                SprdMain.Text = IIf(IsDBNull(.Fields("GLASS_DESC").Value), "", .Fields("GLASS_DESC").Value)

                SprdMain.Col = ColModel
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value)

                SprdMain.Col = ColActualHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_HEIGHT").Value), 0, .Fields("ACTUAL_HEIGHT").Value)))

                SprdMain.Col = ColActualWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_WIDTH").Value), 0, .Fields("ACTUAL_WIDTH").Value)))

                SprdMain.Col = ColChargeableHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_HEIGHT").Value), 0, .Fields("CHARGEABLE_HEIGHT").Value)))

                SprdMain.Col = ColChargeableWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_WIDTH").Value), 0, .Fields("CHARGEABLE_WIDTH").Value)))

                SprdMain.Col = ColActualArea
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GLASS_AREA").Value), 0, .Fields("GLASS_AREA").Value)))

                SprdMain.Col = ColChargeableArea
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLEGLASS_AREA").Value), 0, .Fields("CHARGEABLEGLASS_AREA").Value)))

                SprdMain.Col = ColAreaRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("AREA_RATE").Value), 0, .Fields("AREA_RATE").Value)))


                SprdMain.Col = ColHSNCode
                mHSNCode = IIf(IsDBNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value) ''GetHSNCode(mItemCode)					
                SprdMain.Text = mHSNCode

                SprdMain.Col = ColJITCallNo
                SprdMain.Text = IIf(IsDBNull(.Fields("JIT_CALLNO").Value), "", .Fields("JIT_CALLNO").Value)

                SprdMain.Col = ColAddItemDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("ADD_ITEM_DESCRIPTION").Value), "", .Fields("ADD_ITEM_DESCRIPTION").Value)

                SprdMain.Col = ColMRRNo
                SprdMain.Text = IIf(IsDBNull(.Fields("MRR_REF_NO").Value), "", .Fields("MRR_REF_NO").Value)

                SprdMain.Col = ColODNo
                SprdMain.Text = IIf(IsDBNull(.Fields("OD_NO").Value), "", .Fields("OD_NO").Value)

                SprdMain.Col = ColHeatNo
                SprdMain.Text = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)

                SprdMain.Col = ColBatchNo
                SprdMain.Text = IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)


                SprdMain.Col = Col57F4
                SprdMain.Text = Get57F4(mDespatchNo, Trim(.Fields("ITEM_CODE").Value), I, pRefDate)

                SprdMain.Col = Col57F4Date
                SprdMain.Text = VB6.Format(pRefDate, "DD/MM/YYYY")

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))

                SprdMain.Col = ColMRP
                '            If pDespType = "E" Then					
                '                SprdMain.Text = 0					
                '            Else					
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_MRP").Value), 0, .Fields("ITEM_MRP").Value)))
                '            End If					

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_AMT").Value), 0, .Fields("ITEM_AMT").Value)))

                SprdMain.Col = ColTaxableAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GSTABLE_AMT").Value), 0, .Fields("GSTABLE_AMT").Value)))

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))

                SprdMain.Col = ColCGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CGST_AMOUNT").Value), 0, .Fields("CGST_AMOUNT").Value)))

                SprdMain.Col = ColSGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SGST_AMOUNT").Value), 0, .Fields("SGST_AMOUNT").Value)))

                SprdMain.Col = ColIGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_AMOUNT").Value), 0, .Fields("IGST_AMOUNT").Value)))

                SprdMain.Col = ColNoOfStrip
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("NO_OF_STRIP").Value), 0, .Fields("NO_OF_STRIP").Value)))

                SprdMain.Col = ColStripRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("STRIP_RATE").Value), 0, .Fields("STRIP_RATE").Value)))

                SprdMain.Col = ColPackType
                SprdMain.Text = IIf(IsDBNull(.Fields("PACK_TYPE").Value), "", .Fields("PACK_TYPE").Value)

                SprdMain.Col = ColInnerBoxQty
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                    SprdMain.Text = Format(IIf(IsDBNull(.Fields("INNER_PACK_QTY").Value), 0, .Fields("INNER_PACK_QTY").Value), "0.00")
                Else
                    SprdMain.Text = Format(IIf(IsDBNull(.Fields("INNER_PACK_QTY").Value), 0, .Fields("INNER_PACK_QTY").Value), "0")
                End If


                SprdMain.Col = ColInnerBoxQtyA
                SprdMain.Text = Format(IIf(IsDBNull(.Fields("INNER_PACK_QTY_A").Value), 0, .Fields("INNER_PACK_QTY_A").Value), "0")

                SprdMain.Col = ColInnerBoxCode
                SprdMain.Text = IIf(IsDBNull(.Fields("INNER_PACK_ITEM_CODE").Value), "", .Fields("INNER_PACK_ITEM_CODE").Value)

                SprdMain.Col = ColOuterBoxQty
                SprdMain.Text = Format(IIf(IsDBNull(.Fields("OUTER_PACK_QTY").Value), 0, .Fields("OUTER_PACK_QTY").Value), "0")

                SprdMain.Col = ColOuterBoxQtyA
                SprdMain.Text = Format(IIf(IsDBNull(.Fields("OUTER_PACK_QTY_A").Value), 0, .Fields("OUTER_PACK_QTY_A").Value), "0")

                SprdMain.Col = ColOuterBoxCode
                SprdMain.Text = IIf(IsDBNull(.Fields("OUTER_PACK_ITEM_CODE").Value), "", .Fields("OUTER_PACK_ITEM_CODE").Value)

                mInvTypeCode = Trim(IIf(IsDBNull(.Fields("ACCOUNT_POSTING_CODE").Value), "", .Fields("ACCOUNT_POSTING_CODE").Value))
                mInvTypeDesc = ""


                If lblDespRef.Text = "Q" Or lblDespRef.Text = "L" Then
                    If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                        mInvTypeDesc = MasterNo
                    End If
                Else
                    If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & " AND CATEGORY='S'") = True Then
                        mInvTypeDesc = MasterNo
                    End If
                End If



                SprdMain.Col = ColInvoiceType
                SprdMain.Text = mInvTypeDesc

                '---
                Dim mAccountHeadCode As String
                Dim mAccountHeadDesc As String
                mAccountHeadCode = Trim(IIf(IsDBNull(.Fields("INV_ACCOUNT_CODE").Value), "", .Fields("INV_ACCOUNT_CODE").Value))
                mAccountHeadDesc = ""


                If mAccountHeadCode = "" Then
                    mAccountHeadDesc = GetDebitNameOfInvType(mInvTypeDesc, "Y")
                Else
                    If MainClass.ValidateWithMasterTable(mAccountHeadCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                        mAccountHeadDesc = MasterNo
                    End If
                End If

                SprdMain.Col = ColAccountName
                SprdMain.Text = mAccountHeadDesc



                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume							
    End Sub
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            'MainClass.ClearGrid(SprdView)
            AssignGrid((True))
            '        AdoDCMain.Refresh						
            FormatSprdView()
            'SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CalcTots(ByRef pDespatchLoad As String)
        On Error GoTo ERR1
        Dim mQty As Double
        Dim mRate As Double
        Dim mItemAmount As Double
        Dim mItemValue As Double
        Dim mTotQty As Double
        Dim j As Integer
        Dim I As Integer
        Dim mItemCode As String
        Dim mHSNCode As String

        Dim mNetAccessAmt As Double
        Dim mExciseableAmount As Double

        Dim mTaxableAmount As Double
        Dim mTotalMRP As Double
        Dim mMRP As Double

        Dim mIsJobWork As String
        Dim mMaterialCost As Double
        Dim mTotTaxableItemAmount As Double
        Dim mTaxableItemAmount As Double
        Dim pCustomerCode As String
        Dim pCST_ON_MRTL As Boolean

        Dim mCEDCessAble As Double
        Dim mADDCessAble As Double
        Dim mCESSableAmount As Double
        Dim pTotKKCAmount As Double
        Dim mTotItemAmount As Double
        Dim pTotExciseDuty As Double
        Dim pTotEduCess As Double
        Dim pTotSHECess As Double
        Dim pTotADE As Double
        Dim pTotExportExp As Double
        Dim pTotOthers As Double
        Dim pTotSalesTax As Double
        Dim pTotSurcharge As Double
        Dim pTotCustomDuty As Double
        Dim pTotAddCess As Double
        Dim pTotCustomDutyExport As Double
        Dim pTotCustomDutyCess As Double
        Dim pTotMSC As Double
        Dim pTotDiscount As Double
        Dim pTotServiceTax As Double
        Dim pTotRO As Double
        Dim pTotTCS As Double
        Dim mTotExp As Double
        Dim pEDPer As Double
        Dim pSTPer As Double
        Dim pServPer As Double
        Dim pCessPer As Double
        Dim pSHECPer As Double
        Dim pTCSPer As Double
        Dim mUOM As String

        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double


        Dim mTotCGST As Double
        Dim mTotSGST As Double
        Dim mTotIGST As Double

        Dim mExpName As String
        Dim mOtherTaxableAmount As Double
        Dim mIsTaxable As String
        'Dim mTaxableAmount As Double							
        Dim mExpAddDeduct As String
        Dim mPartyGSTNo As String
        Dim mSameGSTNo As String
        Dim mNoofStrip As Double
        Dim mStripRate As Double
        Dim mLocal As String
        Dim xCustCode As String = "-1"
        Dim mHeight As Double
        Dim mWidth As Double
        Dim mArea As Double
        Dim mMerchantExporter As String = ""

        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xCustCode = Trim(MasterNo)
            End If
        End If

        mLocal = GetPartyBusinessDetail(Trim(xCustCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(xCustCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER= 'EXPORTER-MERCHANT'") = True Then
            mMerchantExporter = "Y"
        End If

        'mLocal = "N"
        'If Trim(txtCustomer.Text) <> "" Then
        '    If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mLocal = Trim(MasterNo)
        '    End If
        'End If

        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")

        pRound = 0
        mQty = 0
        mMRP = 0
        mRate = 0
        mItemAmount = 0
        mTotItemAmount = 0
        mItemValue = 0
        mTotExp = 0
        mTotalMRP = 0
        mOtherTaxableAmount = 0
        mTotTaxableItemAmount = 0

        lblMRPValue.Text = CStr(0)
        pCST_ON_MRTL = False

        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "ISSALEJW", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
            mIsJobWork = MasterNo
        Else
            mIsJobWork = "N"
        End If


        With SprdExp
            For I = 1 To SprdExp.MaxRows
                .Row = I
                .Col = ColExpName
                mExpName = Trim(.Text)

                .Col = ColExpAddDeduct
                mExpAddDeduct = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y' AND TAXABLE='Y'") = True Then
                    mIsTaxable = MasterNo
                Else
                    mIsTaxable = "N"
                End If

                If mIsTaxable = "Y" Then
                    .Col = ColExpAmt
                    mOtherTaxableAmount = mOtherTaxableAmount + (CDbl(VB6.Format(.Text, "0.00")) * IIf(mExpAddDeduct = "D", -1, 1))
                End If
            Next
        End With

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            With SprdMain
                j = .MaxRows
                For I = 1 To j
                    .Row = I
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    .Col = ColChargeableHeight
                    mHeight = Val(.Text) / 1000

                    .Col = ColChargeableWidth
                    mWidth = Val(.Text) / 1000

                    .Col = ColChargeableArea
                    mArea = VB6.Format(mHeight * mWidth, "0.0000")
                    .Text = VB6.Format(mArea, "0.0000")

                    .Col = ColActualHeight
                    mHeight = Val(.Text) / 1000

                    .Col = ColActualWidth
                    mWidth = Val(.Text) / 1000

                    .Col = ColActualArea
                    mArea = VB6.Format(mHeight * mWidth, "0.00")
                    .Text = VB6.Format(mArea, "0.00")

                Next I
            End With
        End If

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc

                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc
                mItemCode = .Text

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColUnit
                mUOM = Trim(.Text)

                mTotQty = mTotQty + mQty

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColAmount
                .Text = VB6.Format(mQty * mRate, "0.00")

                .Col = ColNoOfStrip
                mNoofStrip = Val(.Text)

                If mNoofStrip = 0 Then
                    mStripRate = 0
                Else
                    mStripRate = CDbl(VB6.Format(mQty * mRate / mNoofStrip, "0.000"))
                End If

                .Col = ColStripRate
                .Text = VB6.Format(mStripRate, "0.000")

                .Col = ColMRP
                mMRP = CDbl(VB6.Format(Val(.Text) * mQty, "0.00"))

                mTotalMRP = mTotalMRP + mMRP

                mTotItemAmount = mTotItemAmount + CDbl(VB6.Format(mQty * mRate, "0.00"))
DontCalc:
            Next I
        End With

        mTotTaxableItemAmount = mTotItemAmount + mOtherTaxableAmount

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc1

                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc1
                mItemCode = .Text

                .Col = ColHSNCode
                mHSNCode = Trim(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColUnit
                mUOM = Trim(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColAmount
                mItemAmount = CDbl(VB6.Format(Val(.Text), "0.00"))

                .Col = ColTaxableAmount
                If mTotItemAmount = 0 Then
                    mTaxableAmount = 0
                Else
                    mTaxableAmount = mItemAmount + CDbl(VB6.Format(mOtherTaxableAmount * mItemAmount / mTotItemAmount, "0.00")) '' Format(Val(.Text), "0.00")				
                End If
                .Text = VB6.Format(Val(CStr(mTaxableAmount)), "0.00")

                If chkLUT.CheckState = System.Windows.Forms.CheckState.Checked Then
                    .Col = ColCGSTPer
                    .Text = "0.00"

                    .Col = ColSGSTPer
                    .Text = "0.00"

                    .Col = ColIGSTPer
                    .Text = "0.00"
                Else
                    If CDbl(lblInvoiceSeq.Text) = 6 Or Val(lblInvoiceSeq.Text) = 7 Then
                        If GetHSNDetails(mHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, "0", mPartyGSTNo,,,, mMerchantExporter) = False Then GoTo ERR1
                        .Col = ColCGSTPer
                        .Text = CStr(mCGSTPer)

                        .Col = ColSGSTPer
                        .Text = CStr(mSGSTPer)

                        .Col = ColIGSTPer
                        .Text = CStr(mIGSTPer)
                    End If
                End If

                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)

                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)

                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)

                If chkTaxOnMRP.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mCGSTAmount = System.Math.Round(Val(CStr(mMRP - ((mMRP * Val(txtAbatementPer.Text)) / 100))) * mCGSTPer * 0.01, 2)
                    mSGSTAmount = System.Math.Round(Val(CStr(mMRP - ((mMRP * Val(txtAbatementPer.Text)) / 100))) * mSGSTPer * 0.01, 2)
                    mIGSTAmount = System.Math.Round(Val(CStr(mMRP - ((mMRP * Val(txtAbatementPer.Text)) / 100))) * mIGSTPer * 0.01, 2)
                Else
                    mCGSTAmount = System.Math.Round(mTaxableAmount * mCGSTPer * 0.01, 2)
                    mSGSTAmount = System.Math.Round(mTaxableAmount * mSGSTPer * 0.01, 2)
                    mIGSTAmount = System.Math.Round(mTaxableAmount * mIGSTPer * 0.01, 2)
                End If

                .Col = ColCGSTAmount
                .Text = VB6.Format(mCGSTAmount, "0.00")

                .Col = ColSGSTAmount
                .Text = VB6.Format(mSGSTAmount, "0.00")

                .Col = ColIGSTAmount
                .Text = VB6.Format(mIGSTAmount, "0.00")

                mTotCGST = mTotCGST + CDbl(VB6.Format(mCGSTAmount, "0.00"))
                mTotSGST = mTotSGST + CDbl(VB6.Format(mSGSTAmount, "0.00"))
                mTotIGST = mTotIGST + CDbl(VB6.Format(mIGSTAmount, "0.00"))

                '            If mIsJobWork = "Y" Then					
                '                If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then					
                '                    pCustomerCode = MasterNo					
                '                Else					
                '                    pCustomerCode = "-1"					
                '                End If					
                '                If Val(lblPoNo.text) = "-1" Or Val(lblPoNo.text) = "0" Then					
                '                    mMaterialCost = 0					
                '                Else					
                '                    mMaterialCost = GetSORate(mItemCode, pCustomerCode, "J", "J", "", mUOM, 0, 0, 0)					
                '                End If					
                '                If mMaterialCost > 0 Then					
                '                    mTaxableItemAmount = Format((mQty * mMaterialCost), "0.00")					
                '                    pCST_ON_MRTL = True					
                '                    mJWRemarks = "Material Cost used in Plating Process @ Rs " & mMaterialCost & " per/Pc."					
                '                    mJWSTRemarks = "(On Material Cost)"					
                '                Else					
                '                    mTaxableItemAmount = Format((mQty * mRate), "0.00")					
                '                End If					
                '            Else					
                '                mTaxableItemAmount = Format((mQty * mRate), "0.00") '- mDiscount					
                '            End If					


DontCalc1:
            Next I
        End With

        lblMRPValue.Text = VB6.Format(mTotalMRP, "0.00")


        '    If chkTaxOnMRP.Value = vbChecked Then							
        '        mNetAccessAmt = Val(mTotalMRP - ((mTotalMRP * Val(txtAbatementPer.Text)) / 100))							
        '        mExciseableAmount = Val(mTotalMRP - ((mTotalMRP * Val(txtAbatementPer.Text)) / 100))							
        '        mTaxableAmount = Val(mTotTaxableItemAmount)     '' Val(mTotItemAmount)        ''Val(mTotalMRP - ((mTotalMRP * Val(txtAbatementPer.Text)) / 100))							
        '    Else							
        '        mNetAccessAmt = Val(mTotItemAmount)							
        '        mExciseableAmount = Val(mTotItemAmount) + Val(txtCustMatValue.Text)							
        '        mTaxableAmount = Val(mTotTaxableItemAmount) + Val(txtCustMatValue.Text)     ''Val(mTotItemAmount)							
        '    End If							

        mNetAccessAmt = mTaxableAmount
        '    mTaxableAmount = mTotItemAmount							

        If ADDMode = True And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 And pDespatchLoad = "Y" Then
            Dim mRO As Double
            Dim xNetTotal As Double
            With SprdExp
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColExpSTCode
                    mExpCode = Val(.Text)
                    If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "IDENTIFICATION", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mIndentificationCode = MasterNo
                    Else
                        mIndentificationCode = ""
                    End If

                    If mIndentificationCode = "RO" Then
                        xNetTotal = mTotExp + mTotItemAmount + mTotCGST + mTotSGST + mTotIGST

                        mRO = 0
                        If System.Math.Round(xNetTotal, 0) > xNetTotal Then
                            mRO = System.Math.Round(xNetTotal, 0) - xNetTotal
                        ElseIf System.Math.Round(xNetTotal, 0) < xNetTotal Then
                            mRO = -1 * (xNetTotal - System.Math.Round(xNetTotal, 0))
                        End If
                        'mRO = If((mTotExp + mTotItemAmount + mTotCGST + mTotSGST + mTotIGST) > Int(mTotExp + mTotItemAmount + mTotCGST + mTotSGST + mTotIGST), Int(mTotExp + mTotItemAmount + mTotCGST + mTotSGST + mTotIGST) + 1, (mTotExp + mTotItemAmount + mTotCGST + mTotSGST + mTotIGST))
                        mRO = VB6.Format(mRO, "#0.00") ''- VB6.Format((mTotExp + mTotItemAmount + mTotCGST + mTotSGST + mTotIGST), "#0.00")
                        .Col = ColExpAmt
                        .Text = VB6.Format(mRO, "0.00")
                        Exit For
                    End If
                Next
            End With
        End If

        Call BillExpensesCalcTots_GST(SprdExp, (txtBillDate.Text), mNetAccessAmt, mTotItemAmount, mTaxableAmount, 0, 0, 0, mTotIGST, mTotSGST, mTotCGST, pTotExportExp, 0, 0, pTotOthers, pTotCustomDutyExport, pTotCustomDuty, pTotMSC, pTotDiscount, 0, pTotRO, pTotTCS, mTotExp, pTCSPer, "S")


        '    Call BillExpensesCalcTots(SprdExp, txtBillDate.Text, pCST_ON_MRTL, mNetAccessAmt, mExciseableAmount, mTaxableAmount, _							
        ''                                mCEDCessAble, mADDCessAble, mCESSableAmount, mTotItemAmount, _							
        ''                                pTotExciseDuty, pTotEduCess, pTotSHECess, pTotADE, pTotExportExp, pTotOthers, _							
        ''                                pTotSalesTax, pTotSurcharge, pTotCustomDuty, pTotAddCess, pTotCustomDutyExport, pTotCustomDutyCess, _							
        ''                                pTotMSC, pTotDiscount, pTotServiceTax, pTotRO, pTotTCS, mTotExp, pEDPer, pSTPer, pServPer, pCessPer, pSHECPer, pTCSPer, "S", mNetAccessAmt, pTotKKCAmount)							


        lblTotItemValue.Text = VB6.Format(mTotItemAmount, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(mTotTaxableItemAmount, "#0.00")
        lblTotCGSTAmount.Text = VB6.Format(mTotCGST, "#0.00")
        lblTotSGSTAmount.Text = VB6.Format(mTotSGST, "#0.00")
        lblTotIGSTAmount.Text = VB6.Format(mTotIGST, "#0.00")
        lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
        lblRO.Text = VB6.Format(pTotRO, "#0.00")
        lblMSC.Text = VB6.Format(pTotMSC, "#0.00")
        lblTotQty.Text = VB6.Format(mTotQty, "#0.000")
        lblTCS.Text = VB6.Format(pTotTCS, "#0.00")
        lblTotExportExp.Text = VB6.Format(pTotExportExp, "#0.00")

        lblTotCD.Text = VB6.Format(pTotCustomDutyExport + pTotCustomDuty, "#0.00")
        lblEDUOnCDAmount.Text = VB6.Format(pTotCustomDutyCess, "#0.00")

        If mSameGSTNo = "Y" Then
            lblNetAmount.Text = VB6.Format(mTotExp + mTotItemAmount, "#0.00")
        Else
            lblNetAmount.Text = VB6.Format(mTotExp + mTotItemAmount + mTotCGST + mTotSGST + mTotIGST, "#0.00")
        End If

        lblServicePercentage.Text = CStr(Val(CStr(pServPer)))
        lblTCSPercentage.Text = CStr(Val(CStr(pTCSPer)))

        If Val(txtExchangeRate.Text) = 0 Then
            txtTotalEuro.Text = CStr(0)
        Else
            txtTotalEuro.Text = VB6.Format(Val(lblTotItemValue.Text & lblTotExportExp.Text) / Val(txtExchangeRate.Text), "0.00")
        End If

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTDS As Double = 0
        Dim mTDSRequired As String = "N"

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "TDS_UNDER_194Q", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mTDSRequired = IIf(IsDBNull(MasterNo), "N", MasterNo)
        End If

        If mTDSRequired = "Y" Then
            SqlStr = "SELECT NAME, TDS_DEFAULT_PER FROM TDS_SECTION_MST " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND TDS_ON='P'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mTDS = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDS_DEFAULT_PER").Value), 0, RsTemp.Fields("TDS_DEFAULT_PER").Value), "0.000")
            Else
                mTDS = 0.1
            End If

            txtTDSOnSale.Text = VB6.Format(Val(lblTotItemValue.Text) * mTDS / 100, "0.00")
        Else
            txtTDSOnSale.Text = "0.00"
        End If

        '    lblTotST.text = Format(pTotSalesTax, "#0.00")							
        '    lblTotED.text = Format(pTotExciseDuty, "#0.00")							
        '    lblEDUAmount.text = Format(pTotEduCess, "#0.00")							
        '    lblSHECAmount.text = Format(pTotSHECess, "#0.00")							
        '    lblServiceAmount.text = Format(pTotServiceTax, "#0.00")							

        '    lblKKCessAmount.text = Format(pTotKKCAmount, "#0.00")							
        '    lblTotFreight.text = Format(pTotOthers, "#0.00")							
        '    lblTotCharges.text = 0       ''Format(mRO, "#0.00")							

        '    lblTotTaxableAmt.text = Format(mTaxableAmount - Val(txtCustMatValue.Text), "#0.00")							


        '    lblDiscount.text = Format(pTotDiscount, "#0.00")							
        '    lblSurcharge.text = Format(pTotSurcharge, "#0.00")							


        '    lblEDPercentage.text = Format(pEDPer, "#0.00")							
        '    lblSTPercentage.text = Format(pSTPer, "#0.00")							
        '    lblEDPercentage.text = Val(pEDPer)							

        '    lblEDUPercent.text = Val(pCessPer)							
        '    lblSHECPercent.text = Val(pSHECPer)							
        '    lblSTPercentage.text = Val(pSTPer)							


        '							
        '    If chkDutyIncluded.Value = vbUnchecked Then							
        '        txtEDPayable.Text = Format(lblTotED.text, "0.00")							
        '        txtCessPayable.Text = Format(lblEDUAmount.text, "0.00")							
        '        txtSHECPayable.Text = Format(lblSHECAmount.text, "0.00")							
        '    End If							

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Sub
    Private Sub Clear1()

        LblMKey.Text = ""
        mCustomerCode = CStr(-1)
        '    mAuthSign = ""							
        '    TxtDCNoPrefix.Text = RsCompany!Alias & vb6.Format(RsCompany!FYNO, "00")							
        '    txtDCNo.Text = ""							
        '    txtDCDate.Text = ""							
        '    txtBillNoPrefix.Text = RsCompany!Alias & vb6.Format(RsCompany!FYNO, "00")							
        '    txtBillNo.Text = ""							
        '    txtBillNoSuffix.Text = IIf(LblBookCode.text = "-7", "E", "")							
        '    txtBillDate.Text = Format(RunDate, "DD/MM/YYYY")							
        '    TxtBillTm.Text = GetServerTime							
        '    txtCustomer.Text = ""							
        cmdSavePrint.Enabled = True
        mAddUser = ""
        mAddDate = ""
        mModUser = ""
        mModDate = ""
        txtAddress.Text = ""
        TxtDCNoPrefix.Text = ""
        txtDCNo.Text = ""
        txtDCNoSuffix.Text = ""
        txtDCDate.Text = ""
        txtDCDate.Enabled = False
        cboInvType.SelectedIndex = -1

        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = False

        '    txtBillNoPrefix.Text = IIf(LblBookCode.text = ConSalesBookCode, "S", "EXP")							
        'txtBillNoPrefix.Text = GetDocumentPrefix("S", lblInvoiceSeq.Text)  ''"S" ''& vb6.Format(IIf(IsNull(RsCompany!INVOICE_PREFIX), "", RsCompany!INVOICE_PREFIX), "00") & Val(lblInvoiceSeq.text)							
        txtBillNoPrefix.Text = GetDocumentPrefix("S", lblInvoiceSeq.Text, cboDivision.Text)  ''lblInvoiceSeq.Text  ''Change with 8 not know 28082022
        txtBillNo.Text = ""
        txtBillNoSuffix.Text = GetDocumentPrefix("S", lblInvoiceSeq.Text, cboDivision.Text, "Y")

        txtBillDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        '        txtProddate.Text = Format(RunDate, "DD/MM/YYYY")						
        TxtGRDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtRemovalDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        txtDNNo.Text = ""
        txtDNDate.Text = ""
        TxtBillTm.Text = GetServerTime()

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            txtRemovalTime.Text = ""
            CmdPopFromFile.Enabled = IIf(CDbl(lblInvoiceSeq.Text) = 9, True, False)
            CmdPopFromFile.Visible = IIf(CDbl(lblInvoiceSeq.Text) = 9, True, False)
        Else
            txtRemovalTime.Text = GetServerTime()
            CmdPopFromFile.Enabled = False
            CmdPopFromFile.Visible = False
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            cboInvType.Enabled = False
        Else
            cboInvType.Enabled = True
        End If

        txtCustomer.Text = ""
        txtCreditAccount.Text = ""
        TxtGRNo.Text = ""

        txtCreditDays(0).Text = ""
        txtCreditDays(1).Text = ""
        txtPONo.Text = ""
        txtPODate.Text = ""
        lblInvHeading.Text = ""
        lblPoNo.Text = ""
        lblDespRef.Text = ""
        lblSoDate.Text = ""
        txtStoreDetail.Text = ""
        txtApplicant.Text = ""

        chkShipTo.Enabled = False
        chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        txtShippedTo.Enabled = False
        cmdSearchShippedTo.Enabled = False

        txtStoreDetail.Enabled = True
        txtApplicant.Enabled = True

        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkByHand.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkByHand.Enabled = True
        chkRejection.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkLUT.CheckState = IIf(CDbl(lblInvoiceSeq.Text) = 6 Or Val(lblInvoiceSeq.Text) = 7, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkLUT.Enabled = IIf(CDbl(lblInvoiceSeq.Text) = 6 Or Val(lblInvoiceSeq.Text) = 7, True, False)

        '    chkRegDealer.Value = vbUnchecked							

        lblTotQty.Text = "0.000"
        lblTotItemValue.Text = "0.00"
        lblTotCGSTAmount.Text = "0.00"
        lblTotSGSTAmount.Text = "0.00"
        lblTotIGSTAmount.Text = "0.00"

        txtIRNNo.Text = ""
        txteInvAckNo.Text = ""
        txteInvAckDate.Text = ""
        cmdeInvoice.Enabled = False

        '    lblTotED.text = "0.00"							
        '    lblTotST.text = "0.00"							
        lblNetAmount.Text = "0.00"
        '    cboExciseEntry.ListIndex = -1							
        '    txtExciseNo.Text = ""							
        txtExchangeRate.Text = "0.00"
        '    txtExciseDate.Text = ""							
        txtTariff.Text = ""
        '    txtST38No.Text = ""							
        '    TxtCTNo.Text = ""							
        '    lblCT3Date.text = ""							
        '							
        '    txtCT1No.Text = ""							
        '    lblCT1Date.text = ""							

        '    txtARENo.Text = ""							
        txtItemType.Text = ""
        txtRemarks.Text = ""
        txtNarration.Text = ""
        txtCarriers.Text = ""
        txtVehicle.Text = ""
        txteRefNo.Text = ""
        txtDocsThru.Text = ""
        txtMode.Text = "BY ROAD"

        cboTransmode.SelectedIndex = 0
        txtTransportCode.Text = ""
        txtDistance.Text = ""
        cboVehicleType.SelectedIndex = 0
        txtResponseId.Text = ""
        txtEWayBillNo.Text = ""


        OptFreight(0).Checked = True
        OptFreight(1).Checked = False
        '    optSTType(0).Value = True							
        '    optSTType(1).Value = False							
        '    optSTType(2).Value = False							
        '    txtFormRecvName.Text = ""							
        '    txtFormRecvNo.Text = ""							
        '    txtFormRecvDate = ""							
        '    txtFormDueName.Text = ""							
        '    txtFormDueNo.Text = ""							
        '    txtFormDueDate = ""							

        txtPOAmendNo.Text = ""
        txtPOWEFDate.Text = ""
        txtSuppFromDate.Text = ""
        txtSuppToDate.Text = ""
        txtIntRate.Text = "0.00"
        txtAbatementPer.Text = "0.00"
        txtCustMatValue.Text = "0.00"
        txtTDSOnSale.Text = "0.00"

        txtServProvided.Text = ""
        txtServProvided.Enabled = False
        '    chkD3.Value = vbUnchecked							
        '    chkCT3.Value = vbUnchecked							

        '    chkCT1.Value = vbUnchecked							
        chkAgtPermission.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkPackmat.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkChallanMade.CheckState = System.Windows.Forms.CheckState.Unchecked

        lblTotItemValue.Text = VB6.Format(0, "#0.00")
        '    lblTotST.text = Format(0, "#0.00")							
        '    lblTotED.text = Format(0, "#0.00")							
        lblNetAmount.Text = VB6.Format(0, "#0.00")
        '    lblTotFreight.text = Format(0, "#0.00")							
        '    lblTotCharges.text = Format(0, "#0.00")							
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")
        '    lblTotTaxableAmt.text = Format(0, "#0.00")							
        '    lblEDPercentage.text = Format(0, "#0.00")							
        '    lblDutyForgone.text = "N"							

        lblRO.Text = VB6.Format(0, "#0.00")
        '    lblDiscount.text = Format(0, "#0.00")							
        '    lblSurcharge.text = Format(0, "#0.00")							
        lblMSC.Text = VB6.Format(0, "#0.00")
        lblTCS.Text = VB6.Format(0, "#0.00")
        lblTCSPercentage.Text = VB6.Format(0, "#0.00")

        '    lblEDUPercent.text = Format(0, "#0.00")							
        '    lblEDUAmount.text = Format(0, "#0.00")							

        '    lblSHECAmount.text = Format(0, "#0.00")							
        '    lblSHECPercent.text = Format(0, "#0.00")							

        lblServicePercentage.Text = VB6.Format(0, "#0.00")
        '    lblServiceAmount.text = Format(0, "#0.00")							
        lblTotExportExp.Text = VB6.Format(0, "#0.00")
        lblMRPValue.Text = VB6.Format(0, "#0.00")
        chkStockTrf.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkPrintType.CheckState = System.Windows.Forms.CheckState.Checked
        ChkPaintPrint.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkJWDetail.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTaxOnMRP.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkDutyFreePurchase.Enabled = True
        chkDutyFreePurchase.Visible = True
        chkDutyFreePurchase.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkTaxOnMRP.Enabled = True
        txtAbatementPer.Enabled = True

        txtShippingNo.Text = ""
        txtShippingDate.Text = ""
        txtARE1No.Text = ""
        txtARE1Date.Text = ""
        txtPortCode.Text = ""
        txtExportBillNo.Text = ""
        txtExportBillDate.Text = ""
        txtBuyerName.Text = ""
        txtCoBuyerName.Text = ""
        txtVendorCode.Text = ""
        txtPacking.Text = ""

        txtTotalEuro.Text = ""
        txtAdvLicense.Text = ""
        txtLocation.Text = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        txtProcessNature.Text = ""

        txtModvatNo.Text = ""
        txtModvatDate.Text = ""

        lblTotCD.Text = VB6.Format(0, "#0.00")
        lblEDUOnCDAmount.Text = VB6.Format(0, "#0.00")
        '    lblCDPer.text = Format(0, "#0.00")							
        '    lblCessOnCDPer.text = Format(0, "#0.00")							

        lblCDLabel.Visible = False
        lblCessCDLabel.Visible = False
        lblTotCD.Visible = False
        lblEDUOnCDAmount.Visible = False


        '    chkDutyIncluded.Value = vbUnchecked							
        '    txtEDPayable.Text = "0.00"							
        '    txtCessPayable.Text = "0.00"							
        '    txtSHECPayable.Text = "0.00"							

        '    txtEDPayable.Enabled = False							
        '    txtCessPayable.Enabled = False							
        '    txtSHECPayable.Enabled = False							

        chkPrintTextDesc.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtTextDesc.Text = ""

        chkCancelled.Enabled = False
        chkFOC.Enabled = False
        chkRejection.Enabled = False

        chkPrintByGroup.CheckState = System.Windows.Forms.CheckState.Unchecked

        '    If PubUserLevel = 1 Or PubUserLevel = 2 Then							
        '        txtProddate.Enabled = True							
        '    End If							

        TabMain.SelectedIndex = 0

        '    Dim SqlStr As String=""							
        '    Dim RsAuth As ADODB.Recordset							
        '							
        '    SqlStr = " SELECT NAME,WEF FROM FIN_AUTH_MST " & vbCrLf _							
        ''        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _							
        ''        & " AND STATUS='O'"							
        '							
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsAuth, adLockReadOnly							
        '							
        '    If RsAuth.EOF = False Then							
        '        txtAuth.Text = IIf(IsNull(RsAuth.Fields("NAME").Value), "", RsAuth.Fields("NAME").Value)							
        '        txtAuthDate = Format(IIf(IsNull(RsAuth.Fields("WEF").Value), "", RsAuth.Fields("WEF").Value), "DD/MM/YYYY")							
        '    End If							
        lblDNAmount.Text = "0.00"

        mDNCnNO = ""
        mDNCnDate = ""
        mJWRemarks = ""
        mJWSTRemarks = ""
        lblTotTaxableAmt.Text = CStr(0)

        txtAdvVNo.Text = ""
        txtAdvDate.Text = ""
        txtAdvBal.Text = ""
        txtAdvCGSTBal.Text = ""
        txtAdvSGSTBal.Text = ""
        txtAdvIGSTBal.Text = ""
        txtItemAdvAdjust.Text = ""
        txtAdvAdjust.Text = ""
        txtAdvCGST.Text = ""
        txtAdvSGST.Text = ""
        txtAdvIGST.Text = ""

        chkDespatchFrom.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtShippedFrom.Text = ""
        chkExWork.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtShippedFrom.Enabled = False
        cmdSearchDespatchFrom.Enabled = False
        chkDespatchFrom.Enabled = True
        chkExWork.Enabled = True

        pMSPCost = 0
        pMSRCost = 0
        pFreightCost = 0
        pToolAmorCost = 0

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        cmdeInvoice.Enabled = True 'IIf(PubUserID = "EINV", True, IIf(RsCompany.Fields("E_INVOICE_APP").Value = "Y", True, False))
        cmdQRCode.Enabled = IIf(PubUserID = "EINV", True, IIf(RsCompany.Fields("E_INVOICE_APP").Value = "Y", True, False))

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        Call FillSprdExp()
        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        FraPostingDtl.Visible = False
        FraPostingDtl.Enabled = False
        SprdPostingDetail.Enabled = False
        MainClass.ClearGrid(SprdPostingDetail)
        Call FormatSprdPostingDetail(-1)

    End Sub
    Private Sub cmdPostingHead_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPostingHead.Click

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer

        FraPostingDtl.Visible = Not FraPostingDtl.Visible
        FraPostingDtl.Enabled = FraPostingDtl.Visible
        SprdPostingDetail.Enabled = FraPostingDtl.Visible

        If FraPostingDtl.Visible = True Then
            FraPostingDtl.BringToFront()
            MainClass.ClearGrid(SprdPostingDetail)
            SqlStr = " SELECT ACM.SUPP_CUST_NAME, " & vbCrLf _
                & " ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))) AS AMOUNT, " & vbCrLf _
                & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<=0 THEN 'Cr' ELSE 'Dr' END AS DC "

            SqlStr = SqlStr & vbCrLf _
                & " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM " & vbCrLf _
                & " WHERE  " & vbCrLf & " TRN.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "

            SqlStr = SqlStr & vbCrLf _
                & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND BOOKTYPE ='" & mBookType & "'" & vbCrLf _
                & " --AND BOOKSUBTYPE = '" & mBookSubType & "'" & vbCrLf _
                & " AND TRN.MKEY='" & LblMKey.Text & "'"

            SqlStr = SqlStr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            cntRow = 1
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    SprdPostingDetail.Row = cntRow
                    SprdPostingDetail.Col = 1
                    SprdPostingDetail.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                    SprdPostingDetail.Col = 2
                    SprdPostingDetail.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), "0.00", RsTemp.Fields("Amount").Value), "0.00")

                    SprdPostingDetail.Col = 3
                    SprdPostingDetail.Text = IIf(IsDBNull(RsTemp.Fields("DC").Value), "", RsTemp.Fields("DC").Value)

                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        cntRow = cntRow + 1
                        SprdPostingDetail.MaxRows = cntRow
                    End If
                Loop
            End If
            Call FormatSprdPostingDetail(-1)
        End If
    End Sub
    Private Sub FormatSprdPostingDetail(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdPostingDetail
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = 0
            .ColHidden = True

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(1, 28)

            .Col = 2
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(2, 12)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .set_ColWidth(3, 4)
        End With


        MainClass.ProtectCell(SprdPostingDetail, 1, SprdPostingDetail.MaxRows, 1, 3)

        MainClass.SetSpreadColor(SprdPostingDetail, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillSprdExp()

        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim mLocal As String
        Dim mWithInCountry As String
        Dim mIdentification As String
        Dim mIsBCD As Boolean

        MainClass.ClearGrid(SprdExp)
        mIsBCD = False
        pShowCalc = False
        Dim xCustCode As String = "-1"

        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xCustCode = Trim(MasterNo)
            End If
        End If

        If Trim(txtCustomer.Text) <> "" Then
            mLocal = GetPartyBusinessDetail(Trim(xCustCode), Trim(txtBillTo.Text), "WITHIN_STATE")
            mLocal = IIf(mLocal = "Y", "L", "C")
            mWithInCountry = GetPartyBusinessDetail(Trim(xCustCode), Trim(txtBillTo.Text), "WITHIN_COUNTRY")

            'If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mLocal = IIf(MasterNo = "Y", "L", "C")
            'Else
            '    mLocal = ""
            'End If

            'If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mWithInCountry = MasterNo
            'Else
            '    mWithInCountry = "Y"
            'End If

        Else
            mLocal = ""
            mWithInCountry = "Y"
        End If


        SqlStr = "Select * From FIN_INTERFACE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (Type='S' OR Type='B') "

        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"

        '    If PubGSTApplicable = True Then							
        '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"							
        '    Else							
        '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"							
        '    End If							

        '    If LblBookCode.text = ConSalesBookCode Then							
        '        SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION<>'EE'"							
        '    End If							

        SqlStr = SqlStr & vbCrLf & "Order By PrintSequence"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            I = 0
            Do While Not RS.EOF
                I = I + 1

                SprdExp.Row = I

                SprdExp.Col = ColRO
                SprdExp.Value = IIf(RS.Fields("ROUNDOFF").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdExp.Col = ColExpName
                SprdExp.Text = RS.Fields("Name").Value

                SprdExp.Col = ColExpPercent
                If ADDMode = True Then
                    SprdExp.Text = Str(IIf(IsDBNull(RS.Fields("DefaultPercent").Value), 0, Str(RS.Fields("DefaultPercent").Value)))
                Else
                    SprdExp.Text = ""
                End If

                SprdExp.Col = ColExpAmt
                SprdExp.Text = "0"

                SprdExp.Col = ColExpSTCode
                SprdExp.Text = CStr(Val(IIf(IsDBNull(RS.Fields("CODE").Value), -1, RS.Fields("CODE").Value)))

                mIdentification = IIf(IsDBNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)

                SprdExp.Col = ColExpAddDeduct

                SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")


                SprdExp.Col = ColExpIdent
                SprdExp.Text = IIf(IsDBNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)


                If mIdentification = "BCD" Then mIsBCD = True

                SprdExp.Col = ColTaxable
                SprdExp.Text = IIf(IsDBNull(RS.Fields("Taxable").Value), "N", RS.Fields("Taxable").Value)

                SprdExp.Col = ColExciseable
                SprdExp.Text = IIf(IsDBNull(RS.Fields("Exciseable").Value), "N", RS.Fields("Exciseable").Value)

                If RS.Fields("Identification").Value = "ST" Then
                    If RS.Fields("STTYPE").Value = mLocal Then
                        SprdExp.RowHidden = False
                    Else
                        SprdExp.RowHidden = True
                    End If
                End If

                If RS.Fields("Identification").Value = "EE" Then
                    If mWithInCountry = "N" Then
                        SprdExp.RowHidden = False
                    Else
                        SprdExp.RowHidden = True
                    End If
                End If

                RS.MoveNext()

                If RS.EOF = False Then
                    SprdExp.MaxRows = SprdExp.MaxRows + 1
                End If
            Loop
        End If

        lblCDLabel.Visible = mIsBCD
        lblCessCDLabel.Visible = mIsBCD
        lblTotCD.Visible = mIsBCD
        lblEDUOnCDAmount.Visible = mIsBCD
        pShowCalc = True
        FormatSprdExp(-1)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume							
    End Sub
    Private Sub FillExpFromPartyExp()

        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        Dim xAcctCode As String
        Dim xTrnCode As Double
        Dim I As Integer
        Dim mLocal As String
        Dim mTaxOnMRP As String
        Dim mRO As String
        Dim mIdentification As String

        If Trim(txtCustomer.Text) = "" Then Exit Sub
        If Trim(cboInvType.Text) = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            xAcctCode = "-1"
        End If

        If Trim(txtCustomer.Text) <> "" Then
            mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
            mLocal = IIf(mLocal = "Y", "L", "C")

            'If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mLocal = IIf(MasterNo = "Y", "L", "C")
            'Else
            '    mLocal = ""
            'End If
        Else
            mLocal = ""
        End If


        If lblDespRef.Text = "Q" Or lblDespRef.Text = "L" Then
            If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                xTrnCode = MasterNo
            Else
                xTrnCode = CDbl("-1")
            End If
        Else
            If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                xTrnCode = MasterNo
            Else
                xTrnCode = CDbl("-1")
            End If
        End If



        SqlStr = "Select IH.*, ID.PERCENT, ID.TAX_ON_MRP, ID.ABATEMENT_PER,ID.RO FROM " & vbCrLf & " FIN_INTERFACE_MST IH, FIN_PARTY_INTERFACE_MST ID  " & vbCrLf & " Where IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE(+) " & vbCrLf & " AND IH.CODE=ID.EXPCODE(+) " & vbCrLf & " AND ID.SUPP_CUST_CODE='" & xAcctCode & "'" & vbCrLf & " AND ID.TRNTYPE='" & xTrnCode & "'" & vbCrLf & " AND (IH.Type='S' OR IH.Type='B')  " & vbCrLf & " AND ID.CATEGORY='S' "

        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"

        '    If PubGSTApplicable = True Then							
        '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"							
        '    Else							
        '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"							
        '    End If							

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PrintSequence"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            MainClass.ClearGrid(SprdExp)
            If ADDMode = True Then
                mTaxOnMRP = IIf(IsDBNull(RS.Fields("TAX_ON_MRP").Value), "N", RS.Fields("TAX_ON_MRP").Value)
                chkTaxOnMRP.CheckState = IIf(mTaxOnMRP = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                txtAbatementPer.Text = VB6.Format(IIf(IsDBNull(RS.Fields("ABATEMENT_PER").Value), 0, RS.Fields("ABATEMENT_PER").Value), "0.00")
            End If

            I = 0
            Do While Not RS.EOF
                I = I + 1

                SprdExp.Row = I

                SprdExp.Col = ColExpName
                SprdExp.Text = RS.Fields("Name").Value

                SprdExp.Col = ColExpPercent

                SprdExp.Text = Str(IIf(IsDBNull(RS.Fields("Percent").Value), 0, Str(RS.Fields("Percent").Value)))

                SprdExp.Col = ColExpAmt
                SprdExp.Text = "0"

                SprdExp.Col = ColExpSTCode
                SprdExp.Text = CStr(Val(IIf(IsDBNull(RS.Fields("Code").Value), -1, RS.Fields("Code").Value)))

                mRO = IIf(IsDBNull(RS.Fields("RO").Value), "N", RS.Fields("RO").Value)

                SprdExp.Col = ColRO
                SprdExp.Value = IIf(mRO = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mIdentification = IIf(IsDBNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)

                SprdExp.Col = ColExpAddDeduct
                SprdExp.Text = IIf(RS.Fields("ADD_DED").Value = "A", "A", "D")

                SprdExp.Col = ColExpIdent
                SprdExp.Text = IIf(IsDBNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)
                If SprdExp.Text = "DAM" Then MainClass.ProtectCell(SprdExp, I, I, 1, SprdExp.MaxCols)

                SprdExp.Col = ColTaxable
                SprdExp.Text = IIf(IsDBNull(RS.Fields("TAXABLE").Value), "N", RS.Fields("TAXABLE").Value)

                SprdExp.Col = ColExciseable
                SprdExp.Text = IIf(IsDBNull(RS.Fields("EXCISEABLE").Value), "N", RS.Fields("EXCISEABLE").Value)

                If RS.Fields("Identification").Value = "ST" Then
                    If RS.Fields("STTYPE").Value = mLocal Then
                        SprdExp.RowHidden = False
                    Else
                        SprdExp.RowHidden = True
                    End If
                End If

                RS.MoveNext()

                If RS.EOF = False Then
                    SprdExp.MaxRows = SprdExp.MaxRows + 1
                End If
            Loop
        Else
            Call FillSprdExp()
        End If
        FormatSprdExp(-1)
        Call CalcTots("N")
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume							
    End Sub
    Private Sub FrmInvoiceGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmInvoiceGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode							
    End Sub

    Public Sub FrmInvoiceGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection							
        'PvtDBCn.Open StrConn							
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        '    If InStr(1, XRIGHT, "D", vbTextCompare) > 1 Then							
        '        chkCancelled.Enabled = True							
        '    Else							
        '        chkCancelled.Enabled = False							
        '    End If							

        If PubSuperUser = "S" Then
            chkCancelled.Enabled = False ' True						
            chkFOC.Enabled = False 'True						
        Else
            chkCancelled.Enabled = False
            chkFOC.Enabled = False
        End If

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7755) '8000							
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900							

        TabMain.SelectedIndex = 0

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

        'AdoDCMain.Visible = False

        txtCustomer.Enabled = False
        txtBillNoPrefix.Enabled = False
        txtBillNoSuffix.Enabled = False
        txtBillDate.Enabled = IIf(CDbl(PubUserLevel) = 1 Or CDbl(PubUserLevel) = 2 Or XRIGHT = "AMDV", True, False) ''IIf(XRIGHT = "AMDV", True, False)							
        TxtDCNoPrefix.Enabled = False
        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub OptFreight_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptFreight.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptFreight.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdExp_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdExp.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdExp_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdExp.LeaveCell

        On Error GoTo ErrPart
        Static ESCol As Object
        Static ESRow As Integer
        Static m_Exp As Object
        Static mIDENT As String
        Static m_Amt As Object
        Static m_ExpPercent As Double
        Static m_xp As Object
        Static m_xpn As String
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        ESCol = eventArgs.col
        ESRow = eventArgs.row
        Select Case eventArgs.col
            Case 1 'Exp.Name						
                If eventArgs.newRow >= ESRow Or eventArgs.newRow = -1 Then
                    SprdExp.Row = ESRow

                    SprdExp.Col = 1
                    m_Exp = MainClass.AllowSingleQuote(SprdExp.Text)

                    If SprdExp.Text = "" Then Exit Sub
                    If m_Exp <> "" Then Exit Sub

                    SprdExp.Col = ColExpIdent
                    mIDENT = SprdExp.Text

                    SqlStr = "Select * From FIN_INTERFACE_MST Where COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND Name= '" & m_Exp & "'"
                    SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"

                    '    If PubGSTApplicable = True Then				
                    '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"				
                    '    Else				
                    '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"				
                    '    End If				

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
                    If RS.EOF = True Then
                        ESCol = 1
                        GoTo ErrPart
                    Else
                        If mIDENT = "ST" Then
                            SprdExp.Col = 2
                            SprdExp.Text = CStr(0)
                        End If
                        If RS.EOF = False Then
                            SprdExp.Row = ESRow
                            SprdExp.Col = 4
                            SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")
                        End If
                        SprdExp.Col = 1
                        If SprdExp.Text <> "" Then
                            If SprdExp.MaxRows = ESRow Then
                                MainClass.AddBlankSprdRow(SprdExp, ColExpName)
                                FormatSprdExp((SprdExp.MaxRows))
                            End If
                        End If
                    End If
                End If

            Case 2 'Exp. %						
                If eventArgs.newRow >= ESRow Or eventArgs.newRow = -1 Then
                    SprdExp.Row = ESRow
                    SprdExp.Col = 1
                    If SprdExp.Text = "" Then Exit Sub
                    '               mExp = SprdExp.Text				
                    m_xpn = SprdExp.Text
                    SprdExp.Col = 2
                    SprdExp.Row = ESRow
                    m_ExpPercent = Val(SprdExp.Value)
                    If m_ExpPercent = 0 Then
                        Exit Sub
                    Else
                        SprdExp.Col = ColExpIdent
                        mIDENT = SprdExp.Text

                        If mIDENT = "ST" Or mIDENT = "ED" Or mIDENT = "RO" Then
                            Call CalcTots("N")
                            Exit Sub
                        End If
                        SprdExp.Row = ESRow
                        SprdExp.Col = 3
                        If MainClass.ValidateWithMasterTable(m_xpn, "Name", "RoundOff", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                            If MasterNo = True Then
                                SprdExp.Text = VB6.Format((CDbl(m_ExpPercent) * CDbl(lblTotItemValue.Text)) / 100, "0")
                            Else
                                SprdExp.Text = VB6.Format((CDbl(m_ExpPercent) * CDbl(lblTotItemValue.Text)) / 100, "0")
                            End If
                        End If
                    End If
                Else
                    ESCol = 2
                    ESRow = eventArgs.newRow
                    GoTo ErrPart
                End If

        End Select
        'Call DistributeExpInMainGrid							
        Call CalcTots("N")
        Exit Sub
ErrPart:
        SprdExp.Col = ESCol
        SprdExp.Col2 = ESCol
        SprdExp.Row = ESRow
        SprdExp.Row2 = ESRow
        SprdExp.BlockMode = True
        SprdExp.Action = 0
        SprdExp.BlockMode = False
        '    SprdExp.SetFocus							
    End Sub

    Private Sub SprdExp_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdExp.ClickEvent

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColInvoiceType Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColInvoiceType, 0))

        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub


    Private Sub txtBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBillNoPrefix_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNoPrefix.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBillNoSuffix_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNoSuffix.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtBillTm_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtBillTm.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVendorCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVendorCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPacking_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPacking.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBuyerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBuyerName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBuyerName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBuyerName.DoubleClick
        cmdBuyerSearch_Click(cmdBuyerSearch, New System.EventArgs())
    End Sub


    Private Sub txtBuyerName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBuyerName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBuyerName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBuyerName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdBuyerSearch_Click(cmdBuyerSearch, New System.EventArgs())
    End Sub

    Private Sub txtBuyerName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBuyerName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtBuyerName.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((txtBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Invalid Buyer.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdBuyerSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBuyerSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtBuyerName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtBuyerName.Text = AcName
            txtBuyerName_Validating(txtBuyerName, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    'Private Sub SearchDealer()								
    'On Error GoTo ErrPart								
    'Dim SqlStr  As String								
    '								
    '    SqlStr = "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_TYPE IN ('S','C')"								
    '								
    '    If MainClass.SearchGridMaster(txtStageDealerName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then								
    '        txtStageDealerName.Text = AcName								
    '        txtStageDealerName_Validate False								
    '    End If								
    'Exit Sub								
    'ErrPart:								
    '    ErrorMsg err.Description, err.Number, vbCritical								
    'End Sub								
    'Private Sub SearchManufacturer()								
    'On Error GoTo ErrPart								
    'Dim SqlStr  As String								
    '								
    '    SqlStr = "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_TYPE IN ('S','C')"								
    '								
    '    If MainClass.SearchGridMaster(txtManuName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then								
    '        txtManuName.Text = AcName								
    '        txtManuName_Validate False								
    '    End If								
    'Exit Sub								
    'ErrPart:								
    '    ErrorMsg err.Description, err.Number, vbCritical								
    'End Sub								
    Private Sub txtCarriers_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCarriers.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCarriers_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCarriers.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtCarriers.Text), "FIN_TRANSPORTER_MST", "TRANSPORTER_NAME", "TRANSPORTER_ID", , , SqlStr) = True Then
            txtCarriers.Text = AcName
            txtTransportCode.Text = AcName1
            If txtCarriers.Enabled = True Then txtCarriers.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub txtCarriers_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCarriers.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtCarriers_DoubleClick(txtCarriers, New System.EventArgs())
    End Sub
    Private Sub txtCarriers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCarriers.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCarriers.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPacking_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPacking.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPacking.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCoBuyerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCoBuyerName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCoBuyerName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCoBuyerName.DoubleClick
        cmdCoBuyerSearch_Click(cmdCoBuyerSearch, New System.EventArgs())
    End Sub


    Private Sub txtCoBuyerName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCoBuyerName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCoBuyerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCoBuyerName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCoBuyerName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdCoBuyerSearch_Click(cmdCoBuyerSearch, New System.EventArgs())
    End Sub

    Private Sub txtCoBuyerName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCoBuyerName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtCoBuyerName.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((txtCoBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Invalid Buyer.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdCoBuyerSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCoBuyerSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtCoBuyerName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtCoBuyerName.Text = AcName
            txtCoBuyerName_Validating(txtCoBuyerName, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtCreditAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreditAccount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCreditAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreditAccount.DoubleClick
        On Error GoTo ErrPart

        If MainClass.SearchGridMaster((txtCreditAccount.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCreditAccount.Text = AcName
            'txtDCNo_Validate False						
            txtCreditAccount.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtCreditAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCreditAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCreditAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCreditAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtCreditAccount_DoubleClick(txtCreditAccount, New System.EventArgs())
    End Sub

    Private Sub txtCreditAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCreditAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtCreditAccount.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            Cancel = True
            ErrorMsg("Please Enter the Valid Credit Account.", "", MsgBoxStyle.Critical)
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCreditDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreditDays.TextChanged
        Dim Index As Short = txtCreditDays.GetIndex(eventSender)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCreditDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim Index As Short = txtCreditDays.GetIndex(eventSender)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub




    Private Sub txtCustMatValue_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustMatValue.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTDSOnSale_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSOnSale.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTDSOnSale_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSOnSale.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTDSOnSale_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSOnSale.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots("N")
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtCustMatValue_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustMatValue.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCustMatValue_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustMatValue.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots("N")
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDCDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDCDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDCNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDCNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDCNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDCNo.DoubleClick
        CmdSearchDC_Click(CmdSearchDC, New System.EventArgs())
    End Sub

    Private Sub txtDCNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDCNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDCNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDCNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then CmdSearchDC_Click(CmdSearchDC, New System.EventArgs())
    End Sub

    Private Sub txtDCNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDCNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mRejDocType As String
        Dim mApplicableDate As String

        mRejDocType = IIf(IsDBNull(RsCompany.Fields("REJECTION_DOCTYPE").Value), "D", RsCompany.Fields("REJECTION_DOCTYPE").Value)
        mApplicableDate = IIf(IsDBNull(RsCompany.Fields("REJ_APPLICABLEDATE").Value), "", RsCompany.Fields("REJ_APPLICABLEDATE").Value)

        If Trim(txtDCNo.Text) = "" Then GoTo EventExitSub

        If Len(txtDCNo.Text) < 6 Then
            txtDCNo.Text = VB6.Format(Val(txtDCNo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        SqlStr = " SELECT * FROM DSP_DESPATCH_HDR IH, FIN_SUPP_CUST_MST ACM " _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.AUTO_KEY_DESP=" & Val(txtDCNo.Text) & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ACM.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf



        If CDbl(lblInvoiceSeq.Text) = 9 Or CDbl(lblInvoiceSeq.Text) = 5 Or CDbl(lblInvoiceSeq.Text) = 7 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DESP_TYPE='U'"
        ElseIf CDbl(lblInvoiceSeq.Text) = 1 Then
            If mRejDocType = "D" Or mApplicableDate = "" Then
                'SqlStr = SqlStr & vbCrLf & " AND DESP_TYPE IN ('P','E','F','S','G')"

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                    SqlStr = SqlStr & vbCrLf & " AND (DESP_TYPE IN ('P','E','F','S','G')"

                    SqlStr = SqlStr & vbCrLf & " OR IH.DESP_TYPE = CASE WHEN ACM.INTER_UNIT='Y' AND DESP_DATE>=TO_DATE('13-DEC-2023') THEN 'Q' ELSE '' END "

                    SqlStr = SqlStr & vbCrLf & " OR IH.DESP_TYPE = CASE WHEN ACM.INTER_UNIT='Y' AND DESP_DATE>=TO_DATE('13-DEC-2023') THEN 'L' ELSE '' END )"

                Else
                    SqlStr = SqlStr & vbCrLf & " AND DESP_TYPE IN ('P','E','F','S','G')"
                End If

            Else
                SqlStr = SqlStr & vbCrLf & " AND DESP_TYPE IN ('P','E','F','S','G','Q','L')"
            End If
        ElseIf CDbl(lblInvoiceSeq.Text) = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND DESP_TYPE IN ('J','R')"
        ElseIf CDbl(lblInvoiceSeq.Text) = 3 Then
            If mRejDocType = "D" Or mApplicableDate = "" Then
                SqlStr = SqlStr & vbCrLf & " AND DESP_TYPE NOT IN ('Q','L')"
            End If
        End If

        '    If lblInvoiceSeq.text = 9 Then							
        '        SqlStr = SqlStr & vbCrLf & " AND DESP_TYPE='U'"							
        '    Else							
        '        If (lblInvoiceSeq.text = 1 Or lblInvoiceSeq.text = 2) Then							
        '            SqlStr = SqlStr & vbCrLf & " AND DESP_TYPE<>'U'"							
        '        ElseIf lblInvoiceSeq.text = 3 Then							
        '							
        '        End If							
        '							
        '    End If							

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Clear1()
            If ShowFromDCMain(RsTemp) = False Then
                Cancel = True
                GoTo EventExitSub
            End If

        Else
            ErrorMsg("Please Enter Vaild Despatch Note.", "", MsgBoxStyle.Critical)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtDCNoPrefix_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtDCNoPrefix.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDCNoSuffix_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDCNoSuffix.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDNDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDNDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDNDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDNDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtDNDate.Text = "" Then GoTo EventExitSub
        If IsDate(txtDNDate.Text) = False Then
            ErrorMsg("Invalid Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtDNNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDNNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDNNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDNNo.DoubleClick
        Call SearchDNCN()
    End Sub


    Private Sub txtDNNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDNNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDNNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDNNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDNNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchDNCN()
    End Sub

    Private Sub txtDNNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDNNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mInterUnit As String = "N"

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        If Trim(txtDNNo.Text) = "" Then GoTo EventExitSub

        If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
            GoTo EventExitSub
        End If
        ''AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "							
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  " & vbCrLf & " AND BOOKCODE='-4' AND APPROVED='Y' AND CANCELLED='N'"


        If Trim(txtDNDate.Text) = "" Then
            SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
        Else
            SqlStr = SqlStr & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If



        SqlStr = SqlStr & vbCrLf & " AND DNCNTYPE='R'"


        If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then

        Else
            mSqlStr = " SELECT SUM(NETVALUE) AS NETVALUE" & vbCrLf _
                & " FROM FIN_DNCN_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND MKEY IN ( " & vbCrLf _
                & " SELECT DISTINCT SONO FROM DSP_DESPATCH_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_DESP=" & Val(txtDCNo.Text) & ")"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                lblDNAmount.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value), "0.00")
                If Val(lblNetAmount.Text) <> Val(lblDNAmount.Text) Then
                    If MsgQuestion("Debit Note Net Amount not equal to Bill Amount. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                        Cancel = True
                    Else
                        '                If txtProddate.Enabled = True Then txtProddate.SetFocus				
                    End If
                End If
            Else
                ErrorMsg("Please Enter Vaild debit Note No.", "", MsgBoxStyle.Critical)
                Cancel = True
            End If
        End If
        '    If MainClass.ValidateWithMasterTable(txtDNNo.Text, "VNO", "NETVALUE", "FIN_DNCN_HDR", PubDBCn, MasterNo, , SqlStr) = False Then							
        '        ErrorMsg "Please Enter Vaild debit Note No.", "", vbCritical							
        '        Cancel = True							
        '    Else							
        '        lblDNAmount.text = Format(Val(MasterNo), "0.00")							
        '        If Val(lblNetAmount.text) <> Val(lblDNAmount.text) Then							
        '            If MsgQuestion("Debit Note Net Amount not equal to Bill Amount. You Want to Continue ...") = vbNo Then							
        '                Cancel = True							
        '            Else							
        '                If txtProddate.Enabled = True Then txtProddate.SetFocus							
        '            End If							
        '        End If							
        '    End If							
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDocsThru_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocsThru.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDocsThru_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDocsThru.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDocsThru.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtExchangeRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExchangeRate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExchangeRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExchangeRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtExchangeRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExchangeRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots("N")
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtExportBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExportBillDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExportBillDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExportBillDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtExportBillDate.Text = "" Then GoTo EventExitSub
        If IsDate(txtExportBillDate.Text) = False Then
            ErrorMsg("Invalid Export Bill Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtExportBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExportBillNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtGRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtGRDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtGRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtGRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If TxtGRDate.Text = "" Then GoTo EventExitSub
        If IsDate(TxtGRDate.Text) = False Then
            ErrorMsg("Invalid Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtGRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtGRNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtGRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtGRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtIntRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIntRate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIntRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIntRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemType.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtItemType.Text), "FIN_ITEMTYPE_MST", "NAME", , , , SqlStr) = True Then
            txtItemType.Text = AcName
            If txtItemType.Enabled = True Then txtItemType.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtItemType_DoubleClick(txtItemType, New System.EventArgs())
    End Sub

    Private Sub txtServProvided_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServProvided.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtServProvided_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServProvided.DoubleClick
        SearchProvidedMaster()
    End Sub

    Private Sub txtServProvided_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServProvided.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtServProvided.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtServProvided_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtServProvided.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchProvidedMaster()
    End Sub

    Private Sub txtServProvided_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServProvided.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtServProvided.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then
            MsgInformation("Please Select Valid Service Provided")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchProvidedMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'"

        If MainClass.SearchGridMaster((txtServProvided.Text), "GEN_HSN_MST", "HSN_DESC", "HSN_CODE", , , SqlStr) = True Then
            txtServProvided.Text = AcName
            txtServProvided_Validating(txtServProvided, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtShippingDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShippingDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtShippingDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShippingDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtShippingDate.Text = "" Then GoTo EventExitSub
        If IsDate(txtShippingDate.Text) = False Then
            ErrorMsg("Invalid Shipping Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtShippingNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShippingNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtSuppFromDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppFromDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuppFromDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSuppFromDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.CheckDateKey(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSuppFromDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuppFromDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtSuppFromDate.Text = "" Then GoTo EventExitSub
        If IsDate(txtSuppFromDate.Text) = False Then
            ErrorMsg("Invalid Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSuppToDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppToDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuppToDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSuppToDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.CheckDateKey(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSuppToDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuppToDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtSuppToDate.Text = "" Then GoTo EventExitSub
        If IsDate(txtSuppToDate.Text) = False Then
            ErrorMsg("Invalid Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTariff_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariff.DoubleClick
        SearchTariff()
    End Sub

    Private Sub txtTariff_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTariff.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTariff()
    End Sub

    Private Sub txtTariff_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTariff.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtTariff.Text) = "" Then GoTo EventExitSub

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtTariff.Text), "TARRIF_CODE", "TARRIF_DESC", "FIN_TARRIF_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Please Enter Vaild Tariff.", "", MsgBoxStyle.Critical)
            Cancel = True
        Else
            txtItemType.Text = MasterNo
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTextDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTextDesc.TextChanged
        '    MainClass.SaveStatus Me, ADDMode, MODIFYMode							
    End Sub

    Private Sub txtVehicle_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtVehicle.Text), "FIN_Vehicle_MST", "NAME", , , , SqlStr) = True Then
            txtVehicle.Text = AcName
            If txtVehicle.Enabled = True Then txtVehicle.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtVehicle_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicle.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtVehicle_DoubleClick(txtVehicle, New System.EventArgs())
    End Sub
    Private Sub txtMode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNarration_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNarration.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNarration_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNarration.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNarration.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPODate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRemovalDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemovalDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemovalTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemovalTime.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTariff_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariff.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTariff_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTariff.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVehicle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Function ShowFromDCMain(ByRef mRsDC As ADODB.Recordset) As Boolean

        On Error GoTo ErrPart
        Dim mFormCode As Integer
        Dim mBuyerCode As String
        Dim mInvoiceNo As String

        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim cntRow As Integer
        Dim mExpCode As Integer
        Dim mIndentificationCode As String
        Dim mDespType As String
        Dim mInvoiceType As String
        Dim pInvTypeName As String
        Dim mSACCode As String
        Dim mServDesc As String
        Dim mBalCGST As Double
        Dim mBalSGST As Double
        Dim mBalIGST As Double
        Dim mShippedToCode As String
        Dim mShippedToName As String
        Dim mEOU As String
        Dim pTCSRate As String
        Dim RsTemp As ADODB.Recordset = Nothing

        txtDCNo.Text = IIf(IsDBNull(mRsDC.Fields("AUTO_KEY_DESP").Value), 0, mRsDC.Fields("AUTO_KEY_DESP").Value)

        '    If mRsDC.Fields("DESP_STATUS").Value = 1 Or mRsDC.Fields("DESP_STATUS").Value = 2 Then							
        If MainClass.ValidateWithMasterTable((mRsDC.Fields("AUTO_KEY_DESP").Value), "AUTO_KEY_DESP", "BILLNO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInvoiceNo = MasterNo
            MsgInformation("Invoice : " & mInvoiceNo & " Already made Against This Despatch Note")
            ShowFromDCMain = False
            Exit Function
        End If
        '    End If							


        txtDCDate.Text = IIf(IsDBNull(mRsDC.Fields("DESP_DATE").Value), "", mRsDC.Fields("DESP_DATE").Value)
        If MainClass.ValidateWithMasterTable((mRsDC.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCustomer.Text = MasterNo
            mCustomerCode = Trim(mRsDC.Fields("SUPP_CUST_CODE").Value)
        End If

        If Val(lblInvoiceSeq.Text) = 6 Or Val(lblInvoiceSeq.Text) = 7 Then

        Else
            If MainClass.ValidateWithMasterTable((mRsDC.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER='100% EOU'") = True Then
                mEOU = "Y"
            Else
                mEOU = "N"
            End If

            chkLUT.CheckState = IIf(mEOU = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        End If



        chkShipTo.CheckState = IIf(mRsDC.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked) ''							
        mShippedToCode = IIf(IsDBNull(mRsDC.Fields("SHIPPED_TO_PARTY_CODE").Value), "-1", mRsDC.Fields("SHIPPED_TO_PARTY_CODE").Value)
        mShippedToName = ""
        If MainClass.ValidateWithMasterTable(mShippedToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mShippedToName = MasterNo
        End If



        txtShippedTo.Text = mShippedToName

        txtBillTo.Text = IIf(IsDBNull(mRsDC.Fields("BILL_TO_LOC_ID").Value), "", mRsDC.Fields("BILL_TO_LOC_ID").Value)
        TxtShipTo.Text = IIf(IsDBNull(mRsDC.Fields("SHIP_TO_LOC_ID").Value), "", mRsDC.Fields("SHIP_TO_LOC_ID").Value)

        If txtBillTo.Text <> "" Then
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "SUPP_CUST_ADDR || ',' || SUPP_CUST_CITY || ',' || SUPP_CUST_STATE || ',' || ' GST NO :' || GST_RGN_NO", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'") = True Then
                txtAddress.Text = MasterNo
            End If
        Else
            txtAddress.Text = ""
        End If

        txtCarriers.Text = IIf(IsDBNull(mRsDC.Fields("TRANSPORTER_NAME").Value), "", mRsDC.Fields("TRANSPORTER_NAME").Value)

        If MainClass.ValidateWithMasterTable(txtCarriers.Text, "TRANSPORTER_NAME", "TRANSPORTER_ID", "FIN_TRANSPORTER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtTransportCode.Text = Trim(MasterNo)
        End If


        txtVehicle.Text = IIf(IsDBNull(mRsDC.Fields("VEHICLE_NO").Value), "", mRsDC.Fields("VEHICLE_NO").Value)

        lblPoNo.Text = IIf(IsDBNull(mRsDC.Fields("AUTO_KEY_SO").Value), "", mRsDC.Fields("AUTO_KEY_SO").Value)
        lblDespRef.Text = IIf(IsDBNull(mRsDC.Fields("DESP_TYPE").Value), "", mRsDC.Fields("DESP_TYPE").Value)

        If MainClass.ValidateWithMasterTable((mRsDC.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "LOC_DISTANCE", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LOCATION_ID='" & txtBillTo.Text & "'") = True Then
            txtDistance.Text = MasterNo
        End If

        If lblDespRef.Text = "J" Or lblDespRef.Text = "R" Then
            mServDesc = ""
            If MainClass.ValidateWithMasterTable((lblPoNo.Text), "AUTO_KEY_SO", "SAC_CODE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O' AND SO_APPROVED='Y'") = True Then
                mSACCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    mServDesc = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
            End If
            txtServProvided.Text = mServDesc
        End If

        lblSoDate.Text = IIf(IsDBNull(mRsDC.Fields("SO_DATE").Value), "", mRsDC.Fields("SO_DATE").Value)

        txtPONo.Text = IIf(IsDBNull(mRsDC.Fields("VENDOR_PO").Value), "", mRsDC.Fields("VENDOR_PO").Value)
        txtPODate.Text = IIf(IsDBNull(mRsDC.Fields("VENDOR_PO_DATE").Value), "", mRsDC.Fields("VENDOR_PO_DATE").Value)

        TxtGRNo.Text = IIf(IsDBNull(mRsDC.Fields("GRNo").Value), "", mRsDC.Fields("GRNo").Value)
        TxtGRDate.Text = IIf(IsDBNull(mRsDC.Fields("GRDATE").Value), "", mRsDC.Fields("GRDATE").Value)

        txtVendorCode.Text = GetVendorCode()

        If mRsDC.Fields("DESP_TYPE").Value = "Q" Or mRsDC.Fields("DESP_TYPE").Value = "L" Then
            chkRejection.CheckState = System.Windows.Forms.CheckState.Checked
            txtDNNo.Text = IIf(IsDBNull(mRsDC.Fields("VENDOR_PO").Value), "", mRsDC.Fields("VENDOR_PO").Value)
            txtDNDate.Text = IIf(IsDBNull(mRsDC.Fields("VENDOR_PO_DATE").Value), "", mRsDC.Fields("VENDOR_PO_DATE").Value)
        Else
            chkRejection.CheckState = System.Windows.Forms.CheckState.Unchecked
        End If

        '    If mRsDC.Fields("DESP_TYPE").Value = "S" Or mRsDC.Fields("DESP_TYPE").Value = "R" Then							
        '        chkD3.Value = vbChecked							
        '    Else							
        '        chkD3.Value = vbUnchecked							
        '    End If							

        '    If Trim(txtFormRecvName.Text) = "" Then							
        '        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SALE_STRECD_FORMCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then							
        '            mFormCode = Val(MasterNo)							
        '            If Trim(mFormCode) <> 0 Then							
        '                If MainClass.ValidateWithMasterTable(mFormCode, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then							
        '                    txtFormRecvName.Text = MasterNo							
        '                End If							
        '            End If							
        '        End If							
        '    End If							
        '							
        '    If Trim(txtFormDueName.Text) = "" Then							
        '        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SALE_STDUE_FORMCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then							
        '            mFormCode = Val(MasterNo)							
        '            If Trim(mFormCode) <> 0 Then							
        '                If MainClass.ValidateWithMasterTable(mFormCode, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then							
        '                    txtFormDueName.Text = MasterNo							
        '                End If							
        '            End If							
        '        End If							
        '    End If							

        If MainClass.ValidateWithMasterTable((mRsDC.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "BUYERCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBuyerCode = Trim(MasterNo)
            If mBuyerCode <> "" Then
                If MainClass.ValidateWithMasterTable(mBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtBuyerName.Text = MasterNo
                End If
            End If
        End If


        mDivisionCode = IIf(IsDBNull(mRsDC.Fields("DIV_CODE").Value), -1, mRsDC.Fields("DIV_CODE").Value)

        If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionDesc = Trim(MasterNo)
            cboDivision.Text = mDivisionDesc
        End If
        cboDivision.Enabled = False

        txtBillNoPrefix.Text = GetDocumentPrefix("S", lblInvoiceSeq.Text, cboDivision.Text)


        If lblDespRef.Text = "P" Then
            Dim mStoreCode As String = ""
            Dim mStoreName As String = ""

            Dim mApplicantCode As String = ""
            Dim mApplicantName As String = ""

            '
            If MainClass.ValidateWithMasterTable((lblPoNo.Text), "AUTO_KEY_SO", "SUPP_CUST_STORE_CODE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O' AND SO_APPROVED='Y'") = True Then
                mStoreCode = IIf(IsDBNull(MasterNo), "", MasterNo)

                If mStoreCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mStoreCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mStoreName = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                End If
            End If
            txtStoreDetail.Text = mStoreName

            If MainClass.ValidateWithMasterTable((lblPoNo.Text), "AUTO_KEY_SO", "SUPP_CUST_APPLICANT_CODE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O' AND SO_APPROVED='Y'") = True Then
                mApplicantCode = IIf(IsDBNull(MasterNo), "", MasterNo)

                If mApplicantCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mApplicantCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mApplicantName = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                End If
            End If
            txtApplicant.Text = mApplicantName

        End If


        Call FillCreditDays(mCustomerCode)
        mInvoiceType = ""

        If ShowFromDCDetail((mRsDC.Fields("AUTO_KEY_DESP").Value), mCustomerCode, (mRsDC.Fields("DESP_TYPE").Value), mInvoiceType) = False Then GoTo ErrPart

        mDespType = mRsDC.Fields("DESP_TYPE").Value

        txtAdvBal.Text = CStr(GetBalancePaymentAmount(mCustomerCode, txtBillDate.Text, "", "", mDivisionCode, "AR", mBalCGST, mBalSGST, mBalIGST))
        txtAdvCGSTBal.Text = VB6.Format(mBalCGST, "0.00")
        txtAdvSGSTBal.Text = VB6.Format(mBalSGST, "0.00")
        txtAdvIGSTBal.Text = VB6.Format(mBalIGST, "0.00")

        If mDespType = "S" Or mDespType = "U" Or mDespType = "R" Then

        ElseIf mDespType = "Q" Or mDespType = "L" Then
            If mInvoiceType <> "" Then
                If MainClass.ValidateWithMasterTable(mInvoiceType, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") Then
                    pInvTypeName = MasterNo
                    cboInvType.Text = Trim(pInvTypeName)
                    Call cboInvType_Validating(Trim(pInvTypeName), New System.ComponentModel.CancelEventArgs(False))
                End If
            End If
        Else
            If mInvoiceType <> "" Then
                If MainClass.ValidateWithMasterTable(mInvoiceType, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
                    pInvTypeName = MasterNo
                    cboInvType.Text = Trim(pInvTypeName)
                    Call cboInvType_Validating(Trim(pInvTypeName), New System.ComponentModel.CancelEventArgs(False))
                End If
            End If
        End If
        If lblInvoiceSeq.Text = 6 Then
            SqlStr = "SELECT BILLNO, EXPINV_DATE" & vbCrLf _
                & " FROM FIN_EXPINV_HDR IH" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_PACK=" & Val(lblPoNo.Text) & ""

            ''& " AND IH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _
            ''& " AND IH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

            If RsTemp.EOF = False Then
                txtExportBillNo.Text = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                txtExportBillDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("EXPINV_DATE").Value), "", RsTemp.Fields("EXPINV_DATE").Value), "DD/MM/YYYY")
            End If


            SqlStr = "SELECT CASE WHEN FIELD_NAME='SHIPNO' THEN FIELD_VALUE ELSE '' END AS SHIPNO," & vbCrLf _
                & " CASE WHEN FIELD_NAME='SHIPDATE' THEN FIELD_VALUE ELSE '' END AS SHIPDATE," & vbCrLf _
                & " CASE WHEN FIELD_NAME='PORTNO' THEN FIELD_VALUE ELSE '' END AS PORTNO" & vbCrLf _
                & " FROM FIN_EXPINV_HDR IH, FIN_EXPORT_PARA_EXP EXP" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_PACK=" & Val(lblPoNo.Text) & "" & vbCrLf _
                & " AND IH.AUTO_KEY_EXPINV=EXP.AUTO_KEY_EXPINV AND FIELD_NAME IN ('SHIPNO','SHIPDATE','PORTNO')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

            If RsTemp.EOF = False Then
                txtShippingNo.Text = IIf(IsDBNull(RsTemp.Fields("SHIPNO").Value), "", RsTemp.Fields("SHIPNO").Value)
                txtShippingDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SHIPDATE").Value), "", RsTemp.Fields("SHIPDATE").Value), "DD/MM/YYYY")
                txtPortCode.Text = IIf(IsDBNull(RsTemp.Fields("PORTNO").Value), "", RsTemp.Fields("PORTNO").Value)
            End If
        End If


        If mRsDC.Fields("DESP_TYPE").Value = "Q" Or mRsDC.Fields("DESP_TYPE").Value = "L" Then
            Call ShowDRExp1()
            Call CalcTots("Y")
            Call txtDNNo_Validating(txtDNNo, New System.ComponentModel.CancelEventArgs(True))
        Else
            Call FillSprdExp()
        End If

        With SprdExp
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                '            .Col = ColExpName					
                '            MsgBox .Text					

                .Col = ColExpSTCode
                mExpCode = Val(.Text)
                If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "IDENTIFICATION", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mIndentificationCode = MasterNo
                Else
                    mIndentificationCode = ""
                End If

                If mIndentificationCode = "MSR" Then
                    .Col = ColExpAmt
                    .Text = VB6.Format(pMSRCost, "0.00")
                    '                Exit For				
                End If

                If mIndentificationCode = "MSC" Then
                    .Col = ColExpAmt
                    .Text = VB6.Format(pMSPCost, "0.00")
                    '                Exit For				
                End If
                '            If mIndentificationCode = "EMS" Then					
                '                .Col = ColExpAmt					
                '                .Text = Format(pExciseableMSCCost, "0.00")					
                ''                Exit For					
                '            End If					
                If mIndentificationCode = "FRO" Then
                    .Col = ColExpAmt
                    .Text = VB6.Format(pFreightCost, "0.00")
                    '                Exit For				
                End If
                If mIndentificationCode = "TOL" Then
                    .Col = ColExpAmt
                    .Text = VB6.Format(pToolAmorCost, "0.00")
                    '                Exit For				
                End If

                If mIndentificationCode = "TCS" Then
                    .Col = ColExpPercent
                    pTCSRate = CStr(GetTCSApplication(mCustomerCode, (lblDespRef.Text), VB6.Format(txtBillDate.Text, "DD/MM/YYYY")))
                    .Text = VB6.Format(pTCSRate, "0.0000")
                    '                Exit For				
                End If

            Next
        End With

        Call CalcTots("Y")
        ShowFromDCMain = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowFromDCMain = False
        '    Resume							
    End Function

    Private Function GetTCSApplication(ByRef mCustomerCode As String, ByRef mDespType As String, ByRef pDate As String) As Double
        On Error GoTo ERR1
        Dim mTCSApp As String = ""
        Dim mCompanyPANNo As String
        Dim mCustomerPANNo As String
        Dim mPANAvilable As String
        Dim mTurnOver As Double
        Dim mTCSRate As Double
        Dim mBillTCSRate As Double
        Dim mTurnoverExceed As Boolean

        GetTCSApplication = 0
        If CDate(pDate) < CDate("01/10/2020") Then Exit Function

        If RsCompany.Fields("TCS_APPLICABLE").Value = "Y" Then
            mTurnoverExceed = False
            mCompanyPANNo = IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)

            If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                mCustomerPANNo = MasterNo
            Else
                mCustomerPANNo = ""
            End If

            mPANAvilable = IIf(Trim(mCustomerPANNo) = "", "N", "Y")

            If mCompanyPANNo = mCustomerPANNo Or mDespType = "J" Or mDespType = "R" Or mDespType = "Q" Or mDespType = "L" Then ''Or mIsScrapSale = "Y"						
                mTCSApp = "N"
            ElseIf MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "TCS_NOT_APP", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TCS_NOT_APP='Y'") Then
                mTCSApp = "N"
            Else
                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "TCS_APP", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mTCSApp = MasterNo
                End If
                mTCSApp = IIf(mTCSApp = "", "N", mTCSApp)
                If mTCSApp = "N" Then
                    mTurnOver = GetCurrentTurnOver(mCustomerCode, txtBillNoPrefix.Text & txtBillNo.Text, VB6.Format(txtBillDate.Text, "DD/MM/YYYY"), mCompanyPANNo, mCustomerPANNo)
                    mTurnOver = mTurnOver + Val(lblNetAmount.Text)
                    If mTurnOver > 5000000 Then
                        mTurnoverExceed = True
                        mTCSApp = "Y"
                    Else
                        mTCSApp = "N"
                    End If
                End If
            End If

            If mTCSApp = "Y" Then
                GetTCSApplication = GetTCSRate(mPANAvilable, VB6.Format(pDate, "DD/MM/YYYY"))
            Else
                GetTCSApplication = 0
            End If
        End If
        Exit Function
ERR1:
        GetTCSApplication = 0
        MsgInformation(Err.Description)
    End Function

    Private Function CheckTCSApplication(ByRef mCustomerCode As String, ByRef mDespType As String, ByRef pDate As String) As Boolean
        On Error GoTo ERR1
        Dim mTCSApp As String = ""
        Dim mCompanyPANNo As String
        Dim mCustomerPANNo As String
        Dim mPANAvilable As String
        Dim mTurnOver As Double
        Dim mTCSRate As Double
        Dim mBillTCSRate As Double
        Dim mTurnoverExceed As Boolean

        CheckTCSApplication = False
        If CDate(pDate) < CDate("01/10/2020") Then Exit Function

        If RsCompany.Fields("TCS_APPLICABLE").Value = "Y" Then
            mTurnoverExceed = False
            mCompanyPANNo = IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)

            If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                mCustomerPANNo = MasterNo
            Else
                mCustomerPANNo = ""
            End If

            mPANAvilable = IIf(Trim(mCustomerPANNo) = "", "N", "Y")

            If mCompanyPANNo = mCustomerPANNo Or mDespType = "J" Or mDespType = "R" Or mDespType = "Q" Or mDespType = "L" Then ''Or mIsScrapSale = "Y"						
                mTCSApp = "N"
            ElseIf MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "TCS_NOT_APP", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TCS_NOT_APP='Y'") Then
                mTCSApp = "N"
            Else
                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "TCS_APP", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mTCSApp = MasterNo
                End If
                mTCSApp = IIf(mTCSApp = "", "N", mTCSApp)
                If mTCSApp = "N" Then
                    mTurnOver = GetCurrentTurnOver(mCustomerCode, txtBillNoPrefix.Text & txtBillNo.Text, VB6.Format(txtBillDate.Text, "DD/MM/YYYY"), mCompanyPANNo, mCustomerPANNo)
                    mTurnOver = mTurnOver + Val(lblNetAmount.Text)
                    If mTurnOver > 5000000 Then
                        mTurnoverExceed = True
                        mTCSApp = "Y"
                    Else
                        mTCSApp = "N"
                    End If
                End If
            End If

            If mTCSApp = "Y" Then
                CheckTCSApplication = True
            Else
                CheckTCSApplication = False
            End If
        End If
        Exit Function
ERR1:
        CheckTCSApplication = False
        MsgInformation(Err.Description)
    End Function

    Private Sub FillCreditDays(ByRef mCustomerCode As String)

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPayDate As String
        Dim mPayDay As Integer
        Dim mPayDay2 As Integer


        SqlStr = "SELECT FROM_DAYS, TO_DAYS FROM " & vbCrLf & " FIN_SUPP_CUST_HDR A, FIN_PAYTERM_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.PAYMENT_CODE=B.PAY_TERM_CODE AND SUPP_CUST_CODE='" & mCustomerCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            txtCreditDays(0).Text = IIf(IsDBNull(RsTemp.Fields("FROM_DAYS").Value), 0, RsTemp.Fields("FROM_DAYS").Value)
            txtCreditDays(1).Text = IIf(IsDBNull(RsTemp.Fields("TO_DAYS").Value), 0, RsTemp.Fields("TO_DAYS").Value)
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckDespatchQty() As Boolean

        On Error GoTo ErrPart
        Dim mDespQty As Double
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mDCQty As Double

        Dim I As Integer
        Dim j As Integer
        Dim mItemCode As String
        Dim mInvQty As Double

        Dim mTotalInvQty As Double
        Dim mTotalDCQty As Double

        CheckDespatchQty = False

        mTotalInvQty = Val(lblTotQty.Text)
        mTotalDCQty = 0

        SqlStr = "SELECT SUM(PACKED_QTY) AS QTY, ITEM_CODE " & vbCrLf & " FROM DSP_DESPATCH_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_DESP=" & Val(txtDCNo.Text) & "" & vbCrLf & " GROUP BY ITEM_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)


        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mDCQty = IIf(IsDBNull(RsTemp.Fields("QTY").Value), 0, RsTemp.Fields("QTY").Value)
                mTotalDCQty = mTotalDCQty + mDCQty
                mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                mInvQty = 0
                With SprdMain
                    For I = 1 To .MaxRows - 1
                        .Row = I
                        .Col = ColItemCode
                        If Trim(mItemCode) = Trim(.Text) Then
                            .Col = ColQty
                            mInvQty = mInvQty + Val(.Text)
                        End If
                    Next
                End With

                If VB6.Format(mInvQty, "0.000") <> VB6.Format(mDCQty, "0.000") Then
                    CheckDespatchQty = False
                    Exit Function
                End If
                RsTemp.MoveNext()
            Loop
        End If

        If VB6.Format(mTotalInvQty, "0.000") <> VB6.Format(mTotalDCQty, "0.000") Then
            CheckDespatchQty = False
            Exit Function
        End If

        CheckDespatchQty = True

        Exit Function
ErrPart:
        CheckDespatchQty = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function CheckItemRateWithDN(ByRef mItemCode As String, ByRef mInvoiceRate As Double) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mDNCNRate As Double

        CheckItemRateWithDN = False

        SqlStr = "SELECT ITEM_RATE " & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.MKEY ='" & MainClass.AllowSingleQuote((lblPoNo.Text)) & "'" & vbCrLf & " AND ID.ITEM_CODE ='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            mDNCNRate = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
        End If

        If mDNCNRate = mInvoiceRate Then
            CheckItemRateWithDN = True
        Else
            CheckItemRateWithDN = False
        End If

        Exit Function
ErrPart:
        CheckItemRateWithDN = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function ShowFromDCDetail(ByRef mDCNo As Double, ByRef pCustomerCode As String, ByRef pDespType As String, ByRef mInvoiceType As String) As Boolean

        On Error GoTo ErrPart
        Dim RsDc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mTariff As String = ""
        Dim mTariffDesc As String = ""
        Dim mSORate As Double
        Dim mBillRateDiff As Double
        Dim mOldBillNo As String = ""
        Dim mOldBillDate As String
        Dim mMRPRate As Double
        Dim mAmount As Double
        Dim mRefNo As String
        Dim mUOM As String
        Dim pOldBillRate As Double
        Dim pNewSORate As Double
        Dim pDNRate As Double
        Dim pSuppBillRate As Double
        Dim mQty As Double
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mHSNCode As String

        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mCustomerPartNo As String = ""
        Dim mItemSNo As String
        Dim mRMCustomer As Boolean
        Dim mStripWt As Double
        Dim mNoofStrip As Double
        Dim mStripRate As Double
        Dim xAcctCode As String = ""

        Dim mInvTypeCode As String
        Dim mInvTypeDesc As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mHeight As Double
        Dim mWidth As Double
        Dim mModelNo As String
        Dim mMerchantExporter As String = ""
        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            xAcctCode = "-1"
        End If

        mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER= 'EXPORTER-MERCHANT'") = True Then
            mMerchantExporter = "Y"
        End If

        'mLocal = "N"
        'If Trim(txtCustomer.Text) <> "" Then
        '    If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mLocal = Trim(MasterNo)
        '    End If
        'End If

        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        mRMCustomer = False
        If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER='CUSTOMER-RM'") = True Then
            mRMCustomer = True
        End If


        pMSPCost = 0
        pMSRCost = 0
        pFreightCost = 0
        pToolAmorCost = 0

        SqlStr = "SELECT * FROM DSP_DESPATCH_DET " & vbCrLf _
            & " WHERE AUTO_KEY_DESP=" & mDCNo & "" & vbCrLf _
            & " ORDER BY SERIAL_NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDc, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            cntRow = 1
            If RsDc.EOF = False Then
                Do While Not RsDc.EOF


                    .Row = cntRow
                    .Col = ColItemCode
                    .Text = IIf(IsDBNull(RsDc.Fields("ITEM_CODE").Value), "", RsDc.Fields("ITEM_CODE").Value)
                    mItemCode = IIf(IsDBNull(RsDc.Fields("ITEM_CODE").Value), "", RsDc.Fields("ITEM_CODE").Value)

                    .Col = ColItemDesc
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = MasterNo
                    End If


                    .Col = ColItemSNo
                    mItemSNo = GetItemSNo(mItemCode)
                    .Text = Trim(mItemSNo)

                    If lblDespRef.Text = "J" Or lblDespRef.Text = "R" Then
                        mHSNCode = GetSACCode((txtServProvided.Text))
                    Else
                        mHSNCode = ""
                        mHSNCode = GetHSNFromSO(mItemCode)

                        If mHSNCode = "" And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110 Then
                            mHSNCode = GetHSNCode(mItemCode)
                        End If
                    End If

                    SprdMain.Col = ColGlassDescription
                    SprdMain.Text = IIf(IsDBNull(RsDc.Fields("GLASS_DESC").Value), "", RsDc.Fields("GLASS_DESC").Value)

                    SprdMain.Col = ColModel
                    SprdMain.Text = IIf(IsDBNull(RsDc.Fields("ITEM_MODEL").Value), "", RsDc.Fields("ITEM_MODEL").Value)
                    mModelNo = IIf(IsDBNull(RsDc.Fields("ITEM_MODEL").Value), "", RsDc.Fields("ITEM_MODEL").Value)

                    SprdMain.Col = ColActualHeight
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsDc.Fields("ACTUAL_HEIGHT").Value), 0, RsDc.Fields("ACTUAL_HEIGHT").Value)))

                    SprdMain.Col = ColActualWidth
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsDc.Fields("ACTUAL_WIDTH").Value), 0, RsDc.Fields("ACTUAL_WIDTH").Value)))

                    SprdMain.Col = ColChargeableHeight
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsDc.Fields("CHARGEABLE_HEIGHT").Value), 0, RsDc.Fields("CHARGEABLE_HEIGHT").Value)))
                    mHeight = Val(IIf(IsDBNull(RsDc.Fields("CHARGEABLE_HEIGHT").Value), 0, RsDc.Fields("CHARGEABLE_HEIGHT").Value))

                    SprdMain.Col = ColChargeableWidth
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsDc.Fields("CHARGEABLE_WIDTH").Value), 0, RsDc.Fields("CHARGEABLE_WIDTH").Value)))
                    mWidth = Val(IIf(IsDBNull(RsDc.Fields("CHARGEABLE_WIDTH").Value), 0, RsDc.Fields("CHARGEABLE_WIDTH").Value))



                    .Col = ColHSNCode
                    .Text = CStr(Val(mHSNCode))

                    .Col = ColJITCallNo
                    .Text = IIf(IsDBNull(RsDc.Fields("JITCALLNO").Value), "", RsDc.Fields("JITCALLNO").Value)

                    SprdMain.Col = ColAddItemDesc
                    SprdMain.Text = GetAddDescription(mItemCode, lblPoNo.Text, txtDCDate.Text)

                    SprdMain.Col = ColMRRNo
                    SprdMain.Text = IIf(IsDBNull(RsDc.Fields("MRR_REF_NO").Value), "", RsDc.Fields("MRR_REF_NO").Value)

                    SprdMain.Col = ColODNo
                    SprdMain.Text = IIf(IsDBNull(RsDc.Fields("OD_NO").Value), "", RsDc.Fields("OD_NO").Value)

                    SprdMain.Col = ColHeatNo
                    SprdMain.Text = IIf(IsDBNull(RsDc.Fields("HEAT_NO").Value), "", RsDc.Fields("HEAT_NO").Value)

                    SprdMain.Col = ColBatchNo
                    SprdMain.Text = IIf(IsDBNull(RsDc.Fields("BATCH_NO").Value), "", RsDc.Fields("BATCH_NO").Value)

                    .Col = Col57F4
                    mRefNo = IIf(IsDBNull(RsDc.Fields("REF_NO").Value), "", RsDc.Fields("REF_NO").Value)
                    If mRefNo <> "" Then
                        .Text = Trim(IIf(IsDBNull(RsDc.Fields("REF_NO").Value), "", RsDc.Fields("REF_NO").Value)) ''& " " & vb6.Format(IIf(IsNull(RsDc.Fields("REF_DATE").Value), "", RsDc.Fields("REF_DATE").Value), "DD/MM/YYYY")			

                        .Col = Col57F4Date
                        .Text = VB6.Format(IIf(IsDBNull(RsDc.Fields("REF_DATE").Value), "", RsDc.Fields("REF_DATE").Value), "DD/MM/YYYY")
                    End If

                    If pDespType = "U" Then
                        mOldBillNo = Trim(IIf(IsDBNull(RsDc.Fields("REF_NO").Value), -1, RsDc.Fields("REF_NO").Value))
                    End If

                    .Col = ColUnit
                    .Text = IIf(IsDBNull(RsDc.Fields("ITEM_UOM").Value), "", RsDc.Fields("ITEM_UOM").Value)
                    mUOM = IIf(IsDBNull(RsDc.Fields("ITEM_UOM").Value), "", RsDc.Fields("ITEM_UOM").Value)

                    .Col = ColQty
                    .Text = CStr(Val(IIf(IsDBNull(RsDc.Fields("PACKED_QTY").Value), "", RsDc.Fields("PACKED_QTY").Value)))
                    mQty = Val(IIf(IsDBNull(RsDc.Fields("PACKED_QTY").Value), "", RsDc.Fields("PACKED_QTY").Value))

                    SprdMain.Col = ColPackType
                    SprdMain.Text = IIf(IsDBNull(RsDc.Fields("PACK_TYPE").Value), "", RsDc.Fields("PACK_TYPE").Value)

                    SprdMain.Col = ColInnerBoxQty
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                        SprdMain.Text = Format(IIf(IsDBNull(RsDc.Fields("INNER_PACK_QTY").Value), 0, RsDc.Fields("INNER_PACK_QTY").Value), "0.00")
                    Else
                        SprdMain.Text = Format(IIf(IsDBNull(RsDc.Fields("INNER_PACK_QTY").Value), 0, RsDc.Fields("INNER_PACK_QTY").Value), "0")
                    End If

                    SprdMain.Col = ColInnerBoxCode
                    SprdMain.Text = IIf(IsDBNull(RsDc.Fields("INNER_PACK_ITEM_CODE").Value), "", RsDc.Fields("INNER_PACK_ITEM_CODE").Value)

                    SprdMain.Col = ColOuterBoxQty
                    SprdMain.Text = Format(IIf(IsDBNull(RsDc.Fields("OUTER_PACK_QTY").Value), 0, RsDc.Fields("OUTER_PACK_QTY").Value), "0")

                    SprdMain.Col = ColOuterBoxCode
                    SprdMain.Text = IIf(IsDBNull(RsDc.Fields("OUTER_PACK_ITEM_CODE").Value), "", RsDc.Fields("OUTER_PACK_ITEM_CODE").Value)

                    .Col = ColRate
                    If pDespType = "Q" Or pDespType = "L" Then
                        mCustomerPartNo = ""

                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_ITEM_NO", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pCustomerCode & "'") = True Then
                            mCustomerPartNo = MasterNo
                        End If

                        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then

                        Else
                            If Trim(mCustomerPartNo) = "" Then
                                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                    mCustomerPartNo = MasterNo
                                End If
                            End If
                        End If


                        .Col = ColPartNo
                        .Text = Trim(mCustomerPartNo)

                        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y" Then

                        'Else

                        .Col = ColRate
                        .Text = CStr(GetDRRate(mItemCode, pCustomerCode, pCGSTPer, pSGSTPer, pIGSTPer))

                        .Col = ColCGSTPer
                        .Text = VB6.Format(pCGSTPer, "0.00")

                        .Col = ColSGSTPer
                        .Text = VB6.Format(pSGSTPer, "0.00")

                        .Col = ColIGSTPer
                        .Text = VB6.Format(pIGSTPer, "0.00")
                        'End If


                    Else
                        mOldBillDate = ""
                        If pDespType = "U" Then
                            mBillRateDiff = GetBillRateDiff(mItemCode, pCustomerCode, mOldBillNo, mOldBillDate, pOldBillRate, pNewSORate, pDNRate, pSuppBillRate, "S", pCGSTPer, pSGSTPer, pIGSTPer)

                            mSORate = System.Math.Round(mBillRateDiff, 2)

                            '                        If mItemCode = "F00005" Then		
                            '                            mSORate = 0.29		
                            '                        ElseIf mItemCode = "F00007" Then		
                            '                            mSORate = 7.11		
                            '                        ElseIf mItemCode = "F00009" Then		
                            '                            mSORate = 10.81		
                            '                        ElseIf mItemCode = "F00010" Then		
                            '                            mSORate = 1.76		
                            '                        ElseIf mItemCode = "F00013" Then		
                            '                            mSORate = 9.69		
                            '                        End If		

                            If Trim(mOldBillDate) = "" Or (pCGSTPer + pSGSTPer + pIGSTPer) = 0 Then
                                If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "0", mPartyGSTNo,,,, mMerchantExporter) = False Then GoTo ErrPart
                            ElseIf CDate(mOldBillDate) < CDate(PubGSTApplicableDate) Then  ''pCGSTPer + pSGSTPer + pIGSTPer = 0 Then		
                                If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "0", mPartyGSTNo,,,, mMerchantExporter) = False Then GoTo ErrPart
                            End If

                            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_ITEM_NO", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pCustomerCode & "'") = True Then
                                mCustomerPartNo = MasterNo
                            End If


                            .Col = ColPartNo
                            .Text = Trim(mCustomerPartNo)
                        Else
                            mSORate = GetSORate(mItemCode, pCustomerCode, pDespType, "N", "", mUOM, pCGSTPer, pSGSTPer, pIGSTPer, mInvoiceType, mCustomerPartNo, mHeight, mWidth, mModelNo)

                            .Col = ColPartNo
                            .Text = Trim(mCustomerPartNo)
                        End If

                        If pDespType = "Q" Or pDespType = "L" Or pDespType = "S" Or pDespType = "U" Or pDespType = "R" Then
                            mInvoiceType = ""
                        End If

                        If chkLUT.CheckState = System.Windows.Forms.CheckState.Checked Then
                            pCGSTPer = CDbl("0.00")
                            pSGSTPer = CDbl("0.00")
                            pIGSTPer = CDbl("0.00")
                        End If

                        .Col = ColRate
                        .Text = CStr(mSORate)

                        .Col = ColCGSTPer
                        .Text = VB6.Format(pCGSTPer, "0.00")

                        .Col = ColSGSTPer
                        .Text = VB6.Format(pSGSTPer, "0.00")

                        .Col = ColIGSTPer
                        .Text = VB6.Format(pIGSTPer, "0.00")

                    End If

                    .Col = ColRate
                    mAmount = CDbl(VB6.Format(mQty * Val(.Text), "0.00"))

                    SprdMain.Col = ColModel
                    mModelNo = Trim(.Text)

                    SprdMain.Col = ColChargeableHeight
                    mHeight = Val(.Text)

                    SprdMain.Col = ColChargeableWidth
                    mWidth = Val(.Text)



                    .Col = ColMRP
                    If pDespType = "E" Then
                        mMRPRate = 0
                    Else
                        mMRPRate = GetSORate(mItemCode, pCustomerCode, pDespType, "Y", "", mUOM, 0, 0, 0, "", "", mHeight, mWidth, mModelNo)
                    End If
                    .Text = CStr(mMRPRate)

                    If pDespType = "P" Or pDespType = "S" Then
                        pMSPCost = pMSPCost + (GetSORate(mItemCode, pCustomerCode, pDespType, "MSP", "", mUOM, 0, 0, 0, "", "", mHeight, mWidth, mModelNo) * mQty)
                        pMSRCost = pMSRCost + (GetSORate(mItemCode, pCustomerCode, pDespType, "MSR", "", mUOM, 0, 0, 0, "", "", mHeight, mWidth, mModelNo) * mQty)
                        pFreightCost = pFreightCost + (GetSORate(mItemCode, pCustomerCode, pDespType, "FR", "", mUOM, 0, 0, 0, "", "", mHeight, mWidth, mModelNo) * mQty)
                        pToolAmorCost = pToolAmorCost + (GetSORate(mItemCode, pCustomerCode, pDespType, "TOL", "", mUOM, 0, 0, 0, "", "", mHeight, mWidth, mModelNo) * mQty)
                    End If

                    If Trim(txtTariff.Text) = "" Then
                        If GetTariffHeading(mItemCode, mTariff, mTariffDesc) = True Then
                            txtTariff.Text = mTariff
                            txtItemType.Text = mTariffDesc
                        End If
                    End If

                    If lblDespRef.Text = "P" Or lblDespRef.Text = "G" Or lblDespRef.Text = "J" Then

                        SqlStr = "SELECT ACCOUNT_POSTING_CODE" & vbCrLf _
                            & " FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf _
                            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & pCustomerCode & "'" & vbCrLf _
                            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                            & " AND IH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf _
                            & " AND IH.MKEY = ("

                        SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _
                            & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & pCustomerCode & "'" & vbCrLf _
                            & " AND SID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                            & " AND SIH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf _
                            & " AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            mInvTypeCode = Trim(IIf(IsDBNull(RsTemp.Fields("ACCOUNT_POSTING_CODE").Value), "", RsTemp.Fields("ACCOUNT_POSTING_CODE").Value))
                        Else
                            mInvTypeCode = -1
                        End If
                        mInvTypeDesc = ""

                        If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                            mInvTypeDesc = MasterNo
                        End If

                        SprdMain.Col = ColInvoiceType
                        SprdMain.Text = mInvTypeDesc

                        SprdMain.Col = ColAccountName
                        SprdMain.Text = GetDebitNameOfInvType(mInvTypeDesc, "Y")
                    ElseIf lblDespRef.Text = "Q" Or lblDespRef.Text = "L" Then

                        SqlStr = "SELECT ACCOUNT_POSTING_CODE" & vbCrLf _
                            & " FROM  PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf _
                            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & pCustomerCode & "'" & vbCrLf _
                            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                            & " AND IH.AUTO_KEY_PO=" & Val(lblPoNo.Text) & " AND PO_STATUS='Y'" & vbCrLf _
                            & " AND IH.MKEY = ("

                        SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  PUR_PURCHASE_HDR SIH, PUR_PURCHASE_DET SID" & vbCrLf _
                            & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & pCustomerCode & "'" & vbCrLf _
                            & " AND SID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                            & " AND SIH.AUTO_KEY_PO=" & Val(lblPoNo.Text) & " AND PO_STATUS='Y'" & vbCrLf _
                            & " AND SID.PO_WEF_DATE <=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            mInvTypeCode = Trim(IIf(IsDBNull(RsTemp.Fields("ACCOUNT_POSTING_CODE").Value), "", RsTemp.Fields("ACCOUNT_POSTING_CODE").Value))
                        Else
                            mInvTypeCode = -1
                        End If
                        mInvTypeDesc = ""

                        If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                            mInvTypeDesc = MasterNo
                        End If

                        SprdMain.Col = ColInvoiceType
                        SprdMain.Text = mInvTypeDesc

                        SprdMain.Col = ColAccountName
                        SprdMain.Text = GetDebitNameOfInvType(mInvTypeDesc, "Y")

                    End If

                    If mRMCustomer = True Then
                        mStripWt = 0
                        mStripRate = 0
                        mNoofStrip = 0

                        .Col = ColNoOfStrip
                        If mUOM = "KGS" Or mUOM = "TON" Or mUOM = "MT" Then
                            If MainClass.ValidateWithMasterTable(Trim(mItemCode), "ITEM_CODE", "WT_PER_STRIP", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mStripWt = Val(MasterNo)
                                If mUOM = "TON" Or mUOM = "MT" Then
                                    mStripWt = mStripWt * 0.001
                                End If
                            End If
                            If mStripWt > 0 Then
                                mNoofStrip = System.Math.Round(mQty / mStripWt, 0)
                                .Text = CStr(System.Math.Round(mNoofStrip, 0))
                            Else
                                .Text = CStr(0)
                            End If
                        Else
                            mNoofStrip = System.Math.Round(mQty, 0)
                            .Text = CStr(System.Math.Round(mNoofStrip, 0))
                        End If

                        .Col = ColStripRate
                        If mNoofStrip > 0 Then
                            mStripRate = CDbl(VB6.Format(mAmount / mNoofStrip, "0.000"))
                            .Text = CStr(System.Math.Round(mStripRate, 0))
                        Else
                            .Text = CStr(0)
                        End If

                    End If

                    RsDc.MoveNext()
                    cntRow = cntRow + 1
                    .MaxRows = .MaxRows + 1
                Loop
            End If
        End With
        FormatSprdMain(-1)
        Call CalcTots("Y")
        ShowFromDCDetail = True
        Exit Function
ErrPart:
        '    Resume							
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowFromDCDetail = False
    End Function
    Private Function GetHSNFromSO(ByRef pItemCode As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String
        Dim mPONO As Double
        Dim mPackingListNo As Double

        GetHSNFromSO = ""

        If lblInvoiceSeq.Text = 6 Then     ''Or Val(lblInvoiceSeq.Text) = 7
            mPackingListNo = Val(lblPoNo.Text)

            SqlStr = "SELECT AUTO_KEY_SO " & vbCrLf _
                & " FROM FIN_EXPINV_HDR IH, FIN_EXPINV_DET ID" & vbCrLf _
                & " WHERE IH.AUTO_KEY_EXPINV=ID.AUTO_KEY_EXPINV " & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_PACK=" & Val(lblPoNo.Text) & ""
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mPONO = Val(IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_SO").Value), -1, RsTemp.Fields("AUTO_KEY_SO").Value))
            End If
            RsTemp.Close()

        Else
            mPONO = Val(lblPoNo.Text)
        End If

        ''SqlStr = " SELECT HSN_CODE " & vbCrLf _
        ''    & " FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf _
        ''    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''    & " AND IH.MKEY=ID.MKEY" & vbCrLf _
        ''    & " AND IH.SO_APPROVED='Y' AND IH.SO_STATUS='O'" & vbCrLf & " AND AUTO_KEY_SO=" & mPONO & "" & vbCrLf _
        ''    & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If lblDespRef.Text = "Q" Or lblDespRef.Text = "L" Then
            SqlStr = "SELECT HSN_CODE " & vbCrLf _
                    & " FROM  PUR_PURCHASE_HDR IH,  PUR_PURCHASE_DET ID" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.MKEY=ID.MKEY " & vbCrLf _
                    & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                    & " AND IH.AUTO_KEY_PO=" & mPONO & " AND PO_STATUS='Y'" & vbCrLf _
                    & " AND IH.MKEY = ("

            SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM   PUR_PURCHASE_HDR SIH,  PUR_PURCHASE_DET SID" & vbCrLf _
                            & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND SIH.MKEY=SID.MKEY " & vbCrLf _
                            & " AND SID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                            & " AND SIH.AUTO_KEY_PO=" & mPONO & " AND PO_STATUS='Y'" & vbCrLf _
                            & " AND SID.PO_WEF_DATE <=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        Else
            SqlStr = "SELECT HSN_CODE " & vbCrLf _
                    & " FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.MKEY=ID.MKEY " & vbCrLf _
                    & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                    & " AND IH.AUTO_KEY_SO=" & mPONO & " AND SO_APPROVED='Y'" & vbCrLf _
                    & " AND IH.MKEY = ("

            SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _
                            & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND SIH.MKEY=SID.MKEY " & vbCrLf _
                            & " AND SID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                            & " AND SIH.AUTO_KEY_SO=" & mPONO & " AND SO_APPROVED='Y'" & vbCrLf _
                            & " AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        End If

        'End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetHSNFromSO = Trim(IIf(IsDBNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value))
        End If
        Exit Function
ErrPart:
        GetHSNFromSO = -1
    End Function
    Private Sub FillCboSaleType()

        On Error GoTo ErrPart
        Dim RsSaleType As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mRejDocType As String
        Dim mApplicableDate As String

        mRejDocType = IIf(IsDBNull(RsCompany.Fields("REJECTION_DOCTYPE").Value), "D", RsCompany.Fields("REJECTION_DOCTYPE").Value)
        mApplicableDate = IIf(IsDBNull(RsCompany.Fields("REJ_APPLICABLEDATE").Value), "", RsCompany.Fields("REJ_APPLICABLEDATE").Value)

        cboInvType.Items.Clear()

        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND CATEGORY='S' AND IDENTIFICATION<>'P' "

        If CDbl(lblInvoiceSeq.Text) = 9 Then
            SqlStr = SqlStr & vbCrLf & " AND ISSUPPBILL='Y' AND SAME_GSTN='N'"
        ElseIf CDbl(lblInvoiceSeq.Text) = 5 Then
            SqlStr = SqlStr & vbCrLf & " AND ISSUPPBILL='Y' AND (ISSTOCKTRANFER_FG='Y' OR ISSTOCKTRF='Y') AND SAME_GSTN='Y'"
        ElseIf CDbl(lblInvoiceSeq.Text) = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND ISSALEJW='N' AND ISEXPORT='N' AND SAME_GSTN='N'"
            SqlStr = SqlStr & vbCrLf & " AND ISSUPPBILL='N'"
        ElseIf CDbl(lblInvoiceSeq.Text) = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND ISSALEJW='Y' AND SAME_GSTN='N'"
            SqlStr = SqlStr & vbCrLf & " AND ISSUPPBILL='N'"
        ElseIf CDbl(lblInvoiceSeq.Text) = 3 Then
            SqlStr = SqlStr & vbCrLf & " AND ISEXPORT='N'"
            SqlStr = SqlStr & vbCrLf & " AND ISSUPPBILL='N' AND (ISSTOCKTRANFER_FG='Y' OR ISSTOCKTRF='Y') AND SAME_GSTN='Y'"
        ElseIf CDbl(lblInvoiceSeq.Text) = 6 Then
            SqlStr = SqlStr & vbCrLf & " AND ISEXPORT='Y' AND SAME_GSTN='N'"
            SqlStr = SqlStr & vbCrLf & " AND ISSUPPBILL='N'"
        ElseIf CDbl(lblInvoiceSeq.Text) = 7 Then
            SqlStr = SqlStr & vbCrLf & " AND ISEXPORT='Y' AND SAME_GSTN='N'"
            SqlStr = SqlStr & vbCrLf & " AND ISSUPPBILL='Y'"
        End If
        '							
        '    If lblInvoiceSeq.text = 1 Then							
        ''        SqlStr = SqlStr & vbCrLf & " AND ISSUPPBILL='N'"							
        '    Else							
        '        SqlStr = SqlStr & vbCrLf & " AND ISSUPPBILL='N'"							
        '    End If							

        SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION NOT IN ('S','G')"

        If mRejDocType = "D" Or mApplicableDate = "" Then
            SqlStr = SqlStr & vbCrLf & " AND ISSALERETURN='N'"
        End If


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

    Private Sub txtVehicle_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicle.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicle.Text, "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Function GetExicseAbleAmt() As Double
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mExpName As String
        Dim pExicseAbleAmt As Double
        Dim mExpAddDeduct As String
        Dim mAssesableValue As Double

        With SprdExp
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColExpName
                mExpName = Trim(.Text)

                .Col = ColExpAddDeduct
                mExpAddDeduct = Trim(.Text)

                .Col = ColExpAmt
                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "EXCISEABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        pExicseAbleAmt = pExicseAbleAmt + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                    End If
                End If
            Next
        End With

        If chkTaxOnMRP.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mAssesableValue = Val(lblTotItemValue.Text)
        Else
            mAssesableValue = Val(CStr(Val(lblMRPValue.Text) - ((Val(lblMRPValue.Text) * Val(txtAbatementPer.Text)) / 100)))
        End If

        GetExicseAbleAmt = CDbl(VB6.Format(pExicseAbleAmt + mAssesableValue + Val(txtCustMatValue.Text), "0.00"))
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetExicseAbleAmt = 0
    End Function

    Private Function GetCessAbleAmt() As Double
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mExpName As String
        Dim pCessAbleAmt As Double
        Dim mExpAddDeduct As String
        With SprdExp
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColExpName
                mExpName = Trim(.Text)

                .Col = ColExpAddDeduct
                mExpAddDeduct = Trim(.Text)

                .Col = ColExpAmt
                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "CESSABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        pCessAbleAmt = pCessAbleAmt + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                    End If
                End If
            Next
        End With
        GetCessAbleAmt = pCessAbleAmt
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetCessAbleAmt = 0
    End Function
    Private Function GetSTAbleAmt() As Double
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mExpName As String
        Dim pSTAbleAmt As Double
        Dim mExpAddDeduct As String
        Dim mAssesableValue As Double

        With SprdExp
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColExpName
                mExpName = Trim(.Text)

                .Col = ColExpAddDeduct
                mExpAddDeduct = Trim(.Text)

                .Col = ColExpAmt
                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        pSTAbleAmt = pSTAbleAmt + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                    End If
                End If
            Next
        End With
        '    If chkTaxOnMRP.Value = vbUnchecked Then							
        mAssesableValue = Val(lblTotItemValue.Text)
        '    Else							
        '        mAssesableValue = Val(Val(lblMRPValue.text) - ((Val(lblMRPValue.text) * Val(txtAbatementPer.Text)) / 100))							
        '    End If							

        GetSTAbleAmt = CDbl(VB6.Format(pSTAbleAmt + mAssesableValue + Val(txtCustMatValue.Text), "0.00"))
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetSTAbleAmt = 0
    End Function

    Private Function Get57F4(ByRef pDespatchNote As Double, ByRef pItemCode As String, ByRef xSubRow As Integer, ByRef pRefDate As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        pRefDate = ""
        Get57F4 = ""
        SqlStr = "SELECT REF_NO,REF_DATE FROM DSP_DESPATCH_DET " & vbCrLf _
            & " WHERE AUTO_KEY_DESP=" & pDespatchNote & "" & vbCrLf _
            & " AND ITEM_CODE='" & pItemCode & "' AND SERIAL_NO=" & xSubRow & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Get57F4 = IIf(IsDBNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value)
            pRefDate = IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)
        End If
        Exit Function
ErrPart:
        Get57F4 = ""
    End Function

    Private Sub SearchTariff()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtTariff.Text), "FIN_TARRIF_MST", "TARRIF_CODE", "TARRIF_DESC", , , SqlStr) = True Then
            txtTariff.Text = AcName
            txtItemType.Text = AcName1
            '        txtTariff_Validate False						
            If txtTariff.Enabled = True Then txtTariff.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    'Private Sub SearchCT3()								
    'On Error GoTo ErrPart								
    'Dim SqlStr  As String								
    'Dim mItemCode As String								
    'Dim mItemCodeStr As String								
    'Dim cntRow As Long								
    '								
    '    mItemCodeStr = ""								
    '    With SprdMain								
    '        For cntRow = 1 To .MaxRows								
    '            .Row = cntRow								
    '            .Col = ColItemCode								
    '            mItemCode = Trim(.Text)								
    '            If mItemCode <> "" Then								
    '                If mItemCodeStr = "" Then								
    '                    mItemCodeStr = "'" & mItemCode & "'"								
    '                Else								
    '                    mItemCodeStr = mItemCodeStr & "," & "'" & mItemCode & "'"								
    '                End If								
    '            End If								
    '        Next								
    '    End With								
    '								
    '    mItemCodeStr = "(" & mItemCodeStr & ")"								
    '								
    '    If chkCT3.Value = vbUnchecked Then Exit Sub								
    '    SqlStr = " SELECT CT_NO, CT_DATE," & vbCrLf _								
    ''            & " TRN.ITEM_CODE, " & vbCrLf _								
    ''            & " TO_CHAR(SUM(DECODE(BOOKSUBTYPE,'I',1,-1)*ITEM_QTY)) AS BalQty" & vbCrLf _								
    ''            & " FROM FIN_CT_TRN TRN " & vbCrLf _								
    ''            & " WHERE " & vbCrLf _								
    ''            & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _								
    ''            & " AND BOOKTYPE='S'"								
    '								
    '    If LblMKey.text <> "" Then								
    '        SqlStr = SqlStr & vbCrLf & "AND TRN.MKEY <> '" & MainClass.AllowSingleQuote(LblMKey.text) & "'"								
    '    End If								
    '								
    '    If Trim(TxtCTNo.Text) <> "" Then								
    '        SqlStr = SqlStr & vbCrLf & " AND CT_NO=" & Val(TxtCTNo.Text) & " "								
    '    End If								
    '								
    '    SqlStr = SqlStr & vbCrLf & "AND TRN.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustomerCode) & "'"								
    '    SqlStr = SqlStr & vbCrLf & "AND TRN.ITEM_CODE IN " & mItemCodeStr & ""								
    '								
    '    SqlStr = SqlStr & vbCrLf & " GROUP BY CT_NO, CT_DATE, TRN.ITEM_CODE"								
    '    SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(BOOKSUBTYPE,'O',-1,1)*ITEM_QTY)<>0"								
    '    SqlStr = SqlStr & vbCrLf & " ORDER BY CT_NO, CT_DATE, ITEM_CODE "								
    '								
    '								
    '    If MainClass.SearchGridMasterBySQL2(TxtCTNo.Text, SqlStr) = True Then								
    '        TxtCTNo.Text = AcName								
    '        lblCT3Date.text = AcName1								
    '        If TxtCTNo.Enabled = True Then TxtCTNo.SetFocus								
    '    End If								
    '								
    '								
    'Exit Sub								
    'ErrPart:								
    '    ErrorMsg err.Description, err.Number, vbCritical								
    'End Sub								

    'Private Sub SearchCT1()								
    'On Error GoTo ErrPart								
    'Dim SqlStr  As String								
    'Dim mItemCode As String								
    'Dim mItemCodeStr As String								
    'Dim cntRow As Long								
    '								
    '    mItemCodeStr = ""								
    '    With SprdMain								
    '        For cntRow = 1 To .MaxRows								
    '            .Row = cntRow								
    '            .Col = ColItemCode								
    '            mItemCode = Trim(.Text)								
    '            If mItemCode <> "" Then								
    '                If mItemCodeStr = "" Then								
    '                    mItemCodeStr = "'" & mItemCode & "'"								
    '                Else								
    '                    mItemCodeStr = mItemCodeStr & "," & "'" & mItemCode & "'"								
    '                End If								
    '            End If								
    '        Next								
    '    End With								
    '								
    '    mItemCodeStr = "(" & mItemCodeStr & ")"								
    '								
    '    If chkCT1.Value = vbUnchecked Then Exit Sub								
    '    SqlStr = " SELECT TO_CHAR(CT_NO) AS CT_NO, CT_DATE," & vbCrLf _								
    ''            & " TRN.ITEM_CODE, " & vbCrLf _								
    ''            & " TO_CHAR(SUM(DECODE(BOOKSUBTYPE,'I',1,-1)*ITEM_QTY)) AS BalQty" & vbCrLf _								
    ''            & " FROM FIN_CT1_TRN TRN " & vbCrLf _								
    ''            & " WHERE " & vbCrLf _								
    ''            & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _								
    ''            & " AND BOOKTYPE='S'"								
    '								
    '    If LblMKey.text <> "" Then								
    '        SqlStr = SqlStr & vbCrLf & "AND TRN.MKEY <> '" & MainClass.AllowSingleQuote(LblMKey.text) & "'"								
    '    End If								
    '								
    '    If Trim(txtCT1No.Text) <> "" Then								
    '        SqlStr = SqlStr & vbCrLf & " AND CT_NO=" & Val(txtCT1No.Text) & " "								
    '    End If								
    '								
    '    SqlStr = SqlStr & vbCrLf & "AND TRN.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustomerCode) & "'"								
    '    SqlStr = SqlStr & vbCrLf & "AND TRN.ITEM_CODE IN " & mItemCodeStr & ""								
    '								
    '    SqlStr = SqlStr & vbCrLf & " GROUP BY CT_NO, CT_DATE, TRN.ITEM_CODE"								
    '    SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(BOOKSUBTYPE,'O',-1,1)*ITEM_QTY)<>0"								
    '    SqlStr = SqlStr & vbCrLf & " ORDER BY CT_NO, CT_DATE, ITEM_CODE "								
    '								
    '								
    '    If MainClass.SearchGridMasterBySQL2(txtCT1No.Text, SqlStr) = True Then								
    '        txtCT1No.Text = AcName								
    '        lblCT1Date.text = AcName1								
    '        If txtCT1No.Enabled = True Then txtCT1No.SetFocus								
    '    End If								
    '								
    '								
    'Exit Sub								
    'ErrPart:								
    '    ErrorMsg err.Description, err.Number, vbCritical								
    'End Sub								
    Private Function ReportCT3(ByRef pPrintMode As String) As Boolean

        On Error GoTo ErrPart
        Dim pFileName As String
        Dim RsTemp As ADODB.Recordset = Nothing

        pFileName = mLocalPath & "\Report.Prn"
        Call ShellAndContinue("ATTRIB +A -R " & pFileName)
        FileOpen(1, pFileName, OpenMode.Output)

        SqlStr = MakeSQL()
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MsgInformation("Nothing to print")
            ReportCT3 = False
            FileClose(1)
            Exit Function
        End If


        Call PrintCT3Header(RsTemp)
        Call PrintCT3Detail(RsTemp)

        FileClose(1)


        Dim mFP As Boolean
        If pPrintMode = "P" Then
            mFP = Shell(My.Application.Info.DirectoryPath & "\PrintReport.bat", AppWinStyle.NormalFocus)
            If mFP = False Then GoTo ErrPart
            '        Shell App.path & "\PrintReport.bat",vbNormalFocus						
        Else
            Shell("ATTRIB +R -A " & pFileName)
            Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
            'App.Path & "\RVIEW.EXE "						
        End If

        ReportCT3 = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        ReportCT3 = False
        ''Resume							
        FileClose(1)
    End Function
    Private Sub PrintCT3Detail(ByRef xRsTemp As ADODB.Recordset)
        On Error GoTo ERR1
        Dim mSno As Integer
        Dim pItemDesc As String


        Dim pQty As String
        Dim pValue As String
        Dim pEDRate As String
        Dim pCessRate As String
        Dim pSHECessRate As String
        Dim pEDAmount As String
        Dim pCESSAmount As String
        Dim pSHECESSAmount As String
        Dim pBillDate As String = ""

        Dim TabSNo As Integer
        Dim TabIDesc As Integer
        Dim TabQty As Integer
        Dim TabValue As Integer
        Dim TabRate As Integer
        Dim TabAmount As Integer
        Dim mRemarks As String = ""
        Dim xEDRate As Double

        mSno = 1
        TabSNo = 3
        TabIDesc = 13
        TabQty = 73 ''65							
        TabValue = 90 ''75							
        TabRate = 110
        TabAmount = 120

        If xRsTemp.EOF = False Then
            mRemarks = IIf(IsDBNull(xRsTemp.Fields("Remarks").Value), "", xRsTemp.Fields("Remarks").Value)
        End If

        Do While Not xRsTemp.EOF
            pBillDate = IIf(IsDBNull(xRsTemp.Fields("INVOICE_DATE").Value), "", xRsTemp.Fields("INVOICE_DATE").Value)

            pItemDesc = IIf(IsDBNull(xRsTemp.Fields("Item_Short_Desc").Value), "", xRsTemp.Fields("Item_Short_Desc").Value)
            pQty = VB6.Format(IIf(IsDBNull(xRsTemp.Fields("ITEM_QTY").Value), 0, xRsTemp.Fields("ITEM_QTY").Value), "0.00")
            pValue = VB6.Format(IIf(IsDBNull(xRsTemp.Fields("ITEM_AMT").Value), 0, xRsTemp.Fields("ITEM_AMT").Value), "0.00")

            '        If CDate(pBillDate) < CDate("27/03/2008") Then						
            '            xEDRate = 16						
            '        ElseIf CDate(pBillDate) > CDate("27/03/2008") And CDate(pBillDate) < CDate("08/12/2008") Then						
            '            xEDRate = 14						
            '        ElseIf CDate(pBillDate) > CDate("08/12/2008") And CDate(pBillDate) < CDate("25/02/2009") Then						
            '            xEDRate = 10						
            '        Else						
            '            xEDRate = 8						
            '        End If						

            '    If CVDate(pStartDate) < CVDate("01/03/2008") Then						
            '        mBEDRate = 16						
            '    ElseIf CVDate(pStartDate) < CVDate("01/01/2009") Then						
            '        mBEDRate = 14						
            '    ElseIf CVDate(pStartDate) < CVDate("25/02/2009") Then						
            '        mBEDRate = 10						
            '    Else						
            '        mBEDRate = 8						
            '    End If						

            xEDRate = GetBEDRate(pBillDate)


            '        xEDRate = Val(lblEDPercentage.text)						

            pEDRate = xEDRate & "%"
            pCessRate = "2%"
            pSHECessRate = "1%"
            pEDAmount = VB6.Format(IIf(IsDBNull(xRsTemp.Fields("ITEM_AMT").Value), 0, xRsTemp.Fields("ITEM_AMT").Value) * xEDRate / 100, "0.00")
            pCESSAmount = VB6.Format(CDbl(pEDAmount) * 0.02, "0.00")
            pSHECESSAmount = VB6.Format(CDbl(pEDAmount) * 0.01, "0.00")

            Print(1, TAB(TabSNo), mSno) ''Chr(15) &						
            pItemDesc = GetMultiLine(pItemDesc, PrintLine_Renamed, TabQty - TabIDesc, TabIDesc)
            Print(1, TAB(TabIDesc), pItemDesc)
            Print(1, TAB(TabQty), New String(" ", TabValue - TabQty - Len(pQty)) & pQty)
            Print(1, TAB(TabValue), New String(" ", TabRate - TabValue - Len(pValue)) & pValue)
            Print(1, TAB(TabRate), New String(" ", TabAmount - TabRate - Len(pEDRate)) & pEDRate)
            PrintLine(1, TAB(TabAmount), New String(" ", TabLastCol - TabAmount - Len(pEDAmount)) & pEDAmount)
            PrintLine_Renamed = PrintLine_Renamed + 1

            Print(1, TAB(TabRate), New String(" ", TabAmount - TabRate - Len(pCessRate)) & pCessRate)
            PrintLine(1, TAB(TabAmount), New String(" ", TabLastCol - TabAmount - Len(pCESSAmount)) & pCESSAmount)
            PrintLine_Renamed = PrintLine_Renamed + 1

            Print(1, TAB(TabRate), New String(" ", TabAmount - TabRate - Len(pSHECessRate)) & pSHECessRate)
            PrintLine(1, TAB(TabAmount), New String(" ", TabLastCol - TabAmount - Len(pSHECESSAmount)) & pSHECESSAmount)
            PrintLine_Renamed = PrintLine_Renamed + 1

            xRsTemp.MoveNext()
            mSno = mSno + 1
        Loop

        mRemarks = GetMultiLine(mRemarks, PrintLine_Renamed, TabQty - TabIDesc, TabIDesc)
        PrintLine(1, TAB(TabIDesc), mRemarks)
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Chr(18)							

        If PrintLine_Renamed < 40 Then
            Do While PrintLine_Renamed <> 40
                If PrintLine_Renamed >= 40 Then Exit Do
                PrintLine(1, TAB(0), " ")
                PrintLine_Renamed = PrintLine_Renamed + 1
            Loop
        End If

        PrintLine(1, TAB(TabIDesc), pBillDate)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabIDesc), IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value))
        PrintLine(1, TAB(0), "" & Chr(18) & Chr(12))

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume							
    End Sub
    Private Sub PrintCT3Header(ByRef pRsTemp As ADODB.Recordset)
        On Error GoTo ERR1
        Dim mString As String
        Dim Tab1 As Integer
        Dim Tab2 As Integer
        Dim Tab3 As Integer

        PageNo = PageNo + 1
        Tab1 = 15
        Tab2 = 93
        Tab3 = 120

        PrintLine_Renamed = 1
        Do While PrintLine_Renamed <> 6
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop
        PrintLine(1, TAB(0), "" & Chr(15))

        Print(1, TAB(Tab1), IIf(IsDBNull(RsCompany.Fields("EXCISE_RANGE").Value), "", RsCompany.Fields("EXCISE_RANGE").Value))
        mString = IIf(IsDBNull(pRsTemp.Fields("ARE_NO").Value), "", pRsTemp.Fields("ARE_NO").Value) & "/" & RsCompany.Fields("FYEAR").Value & "-" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")
        mString = mString & New String(" ", 12 - Len(mString))
        Print(1, TAB(Tab2), Chr(18) & mString & Chr(15))
        mString = IIf(IsDBNull(pRsTemp.Fields("INVOICE_DATE").Value), "", pRsTemp.Fields("INVOICE_DATE").Value)
        mString = New String(" ", TabLastCol - Tab3 - Len(mString)) & mString
        PrintLine(1, TAB(Tab3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(Tab1), IIf(IsDBNull(RsCompany.Fields("EXCISE_DIV").Value), "", RsCompany.Fields("EXCISE_DIV").Value))
        mString = IIf(IsDBNull(pRsTemp.Fields("BILLNO").Value), "", pRsTemp.Fields("BILLNO").Value)
        mString = mString & New String(" ", 12 - Len(mString))
        Print(1, TAB(Tab2), Chr(18) & mString & Chr(15))
        mString = IIf(IsDBNull(pRsTemp.Fields("INVOICE_DATE").Value), "", pRsTemp.Fields("INVOICE_DATE").Value)
        mString = New String(" ", TabLastCol - Tab3 - Len(mString)) & mString
        PrintLine(1, TAB(Tab3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(Tab1), IIf(IsDBNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value))
        mString = IIf(IsDBNull(pRsTemp.Fields("CT_NO").Value), "", pRsTemp.Fields("CT_NO").Value)
        mString = mString & New String(" ", 12 - Len(mString))
        Print(1, TAB(Tab2), Chr(18) & mString & Chr(15))
        mString = IIf(IsDBNull(pRsTemp.Fields("CT3_DATE").Value), "", pRsTemp.Fields("CT3_DATE").Value)
        mString = New String(" ", TabLastCol - Tab3 - Len(mString)) & mString
        PrintLine(1, TAB(Tab3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = IIf(IsDBNull(pRsTemp.Fields("VEHICLENO").Value), "", pRsTemp.Fields("VEHICLENO").Value)
        PrintLine(1, TAB(Tab2), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " " & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "I/WE " & RsCompany.Fields("Company_Name").Value
        mString = mString & " holder(s) of Central Excise Regn. No. " & IIf(IsDBNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value)
        mString = mString & " undertaken to remove the undermentioned goods from the factory / warehouse at " & IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        mString = mString & " to the warehouse at " & IIf(IsDBNull(pRsTemp.Fields("SUPP_CUST_CITY").Value), "", pRsTemp.Fields("SUPP_CUST_CITY").Value)
        mString = mString & " in Range " & IIf(IsDBNull(pRsTemp.Fields("EXCISE_RANGE").Value), "", pRsTemp.Fields("EXCISE_RANGE").Value)
        mString = mString & " Division " & IIf(IsDBNull(pRsTemp.Fields("EXCISE_DIV").Value), "", pRsTemp.Fields("EXCISE_DIV").Value)
        mString = mString & " Mr./Messrs " & IIf(IsDBNull(pRsTemp.Fields("SUPP_CUST_NAME").Value), "", pRsTemp.Fields("SUPP_CUST_NAME").Value)
        mString = mString & " holder of Central Excise Registration No. " & IIf(IsDBNull(pRsTemp.Fields("CENT_EXC_RGN_NO").Value), "", pRsTemp.Fields("CENT_EXC_RGN_NO").Value)
        mString = GetMultiLine(mString, PrintLine_Renamed, 80, 3)

        PrintLine(1, TAB(3), mString & Chr(15))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Do While PrintLine_Renamed <> 26
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume							
    End Sub
    Private Sub txtTotalEuro_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotalEuro.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotalEuro_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalEuro.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTotalEuro.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtAdvLicense_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvLicense.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAdvLicense_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvLicense.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAdvLicense.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLocation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLocation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProcessNature_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcessNature.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProcessNature_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProcessNature.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProcessNature.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Function WebRequestGenerateDigitalSignTest(ByRef pPDFFileName As String, ByRef pPDFOutFileName As String) As Boolean
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

        url = "http://ip.webtel.in/webesignapi/service.asmx"
        mUserName = "rR482Xeoilw" ''"06AAACW3775F013"							
        mPassword = "Rqsie103pd"

        Dim http As Object   '' Dim http As MSXML2.XMLHTTP60 '' MSXML.xmlhttp							
        http = CreateObject("MSXML2.ServerXMLHTTP")




        Dim pHeader As XElement = New XElement("Header",
                    New XElement("AuthHeader", New Object() {
                                 New XElement("Username", mUserName),
                                 New XElement("Password", mPassword)
                    })
            )

        Dim pBody As XElement = New XElement("Body",
            New XElement("SignPDF", New Object() {
                New XElement("pdfByte", pPDFFileName),
                New XElement("AuthorizedSignatory", "SANDEEP KANDWAL"),
                New XElement("SignerName", "SANDEEP KANDWAL"),
                New XElement("TopLeft", 100),
                New XElement("BottomLeft", 290),
                New XElement("TopRight", 190),
                New XElement("BottomRight", 340),
                New XElement("ExcludePageNo", ""),
                New XElement("InvoiceNumber", ""),
                New XElement("pageNo", -1),
                New XElement("PrintDateTime", ""),
                New XElement("FindAuth", ""),
                New XElement("FindAuthLocation", "")
            })
        )

        Dim pXMLString As XElement = New XElement("Envelope",
                pHeader,
                pBody
        )

        http.Send(pXMLString)

        pResponseText = http.responseText
        '    pResponseText = Replace(pResponseText, "\", "")							
        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        '    pResponseText = Mid(pResponseText, 2, Len(pResponseText) - 2)			

        'Dim writer As New XmlTextWriter("DigitalSign.xml", System.Text.Encoding.UTF8)

        'writer.WriteStartDocument(True)
        'writer.Formatting = Formatting.Indented
        'writer.Indentation = 2
        'writer.WriteStartElement("Envelope")

        'writer.WriteStartElement("Header")
        'writer.WriteStartElement("AuthHeader")
        'createNode("Username", mUserName, writer)
        'createNode("Password", mPassword, writer)
        'writer.WriteEndElement()
        'writer.WriteEndElement()

        'writer.WriteStartElement("Body")
        'writer.WriteStartElement("SignPDF")
        'createNode("pdfByte", pPDFFileName, writer)
        'createNode("AuthorizedSignatory", "SANDEEP KANDWAL", writer)
        'createNode("SignerName", "SANDEEP KANDWAL", writer)
        'createNode("TopLeft", 100, writer)
        'createNode("BottomLeft", 290, writer)
        'createNode("TopRight", 190, writer)
        'createNode("BottomRight", 340, writer)
        'createNode("ExcludePageNo", "", writer)
        'createNode("InvoiceNumber", "", writer)
        'createNode("pageNo", -1, writer)
        'createNode("PrintDateTime", "", writer)
        'createNode("FindAuth", "", writer)
        'createNode("FindAuthLocation", "", writer)


        'writer.WriteEndElement()
        'writer.WriteEndElement()
        'writer.WriteEndElement()

        'writer.WriteEndDocument()

        'writer.Close()

        'Dim companyFile As XElement = New XElement("CompanyFile",
        '    New XElement("Companybranch", New Object() {
        '                 New XAttribute("name", companyName),
        '                 New XElement("Customer", New Object() {
        '                              New XElement("name", customerName),
        '                              New XElement("age", age),
        '                              New XElement("address",
        '                                           New XElement("addressLine", address))
        '                          })
        '             })
        '     )



        WebRequestGenerateDigitalSignTest = True
        Exit Function
ErrPart:
        '    Resume							
        WebRequestGenerateDigitalSignTest = False
        'http = Nothing							
        MsgBox(Err.Description)
        '     PubDBCn.RollbackTrans							
    End Function
    Private Sub createNode(ByVal pProductName As String, ByVal pProductValue As String, ByVal writer As XmlTextWriter)
        writer.WriteStartElement(pProductName)
        writer.WriteString(pProductValue)
        writer.WriteEndElement()
    End Sub

    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mBillNoPrefix As String
        Dim mBillNo As String
        Dim mBillNoSuffix As String
        Dim mRow As UltraGridRow
        Dim mCol As UltraGridColumn

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)
        mCol = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1)


        mBillNoPrefix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1))
        mBillNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(2))       ''ultrow.SetCellValue(m_udtColumns.EntryNo, dtRow.Item("EntryNo"))
        mBillNoSuffix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(3))

        txtBillNoPrefix.Text = mBillNoPrefix
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 And RsCompany.Fields("FYEAR").Value = 2023 And RsCompany.Fields("COMPANY_CODE").Value = 1 And Val(mBillNo) < 100 Then
            txtBillNo.Text = VB6.Format(mBillNo, "0")
        Else
            txtBillNo.Text = VB6.Format(mBillNo, ConBillFormat)
        End If



        txtBillNoSuffix.Text = mBillNoSuffix

        txtBillNo_Validating(txtBillNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub

    '    Private Function RefreshQRCode() As String
    '        On Error GoTo ErrPart
    '        Dim url As String

    '        Dim mGSTIN As String
    '        Dim mIrn As String

    '        Dim mGetQRImg As String
    '        Dim mGetSignedInvoice As String
    '        Dim mCDKey As String = ""
    '        Dim mEInvUserName As String = ""
    '        Dim mEInvPassword As String = ""
    '        Dim mEFUserName As String = ""
    '        Dim mEFPassword As String = ""

    '        Dim mBody As String
    '        Dim mResponseId As String
    '        Dim mResponseIdStr As String
    '        Dim url1 As String
    '        Dim WebRequestGen As String
    '        Dim pStaus As String

    '        Dim mIRNNo As String
    '        Dim mSignedInvoice As String
    '        Dim mSignedQRCode As String

    '        Dim pError As String
    '        'Dim pBranchId As String
    '        'Dim pTokenId As String
    '        'Dim pUserId As String
    '        'Dim mBMPFileName As String = " "

    '        Dim pResponseText As String
    '        Dim RsTemp As ADODB.Recordset
    '        Dim pIsTesting As String = "Y"
    '        If Trim(txtIRNNo.Text) = "" Then Exit Function

    '        'RefreshQRCode = " "
    '        RefreshQRCode = mPubBarCodePath & "\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".bmp"

    '        SqlStr = "SELECT SIGNQRCODE FROM FIN_INVOICE_QRCODE " & vbCrLf _
    '                & " WHERE MKEY = '" & LblMKey.Text & "'" & vbCrLf _
    '                & " AND COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " "

    '        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
    '        'MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly

    '        If RsTemp.EOF = False Then
    '            mSignedQRCode = IIf(IsDBNull(RsTemp.Fields("SIGNQRCODE").Value), "", RsTemp.Fields("SIGNQRCODE").Value)
    '            If GererateQRCodeImage(RefreshQRCode, mSignedQRCode) = False Then GoTo ErrPart
    '        Else
    '            If GeteInvoiceSetupContents(url, "I", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword, pIsTesting) = False Then GoTo ErrPart

    '            If pIsTesting = "Y" Then
    '                url = "http://einvsandbox.webtel.in/v1.03/GenIRN"
    '                mCDKey = "1000687"
    '                mEInvUserName = "03AAACW3775F010"       ''"06AAACW3775F013"		 "29AAACW3775F000" '' 					
    '                mEInvPassword = "Admin!23"  ''"Admin!23.."    ''
    '                mEFUserName = "29AAACW3775F000"  '' "29AAACW3775F000"
    '                mEFPassword = "Admin!23.."
    '                mGSTIN = "03AAACW3775F010" ''IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
    '            Else
    '                mGSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
    '            End If


    '            Dim http As Object ''MSXML2.XMLHTTP60   '' MSXML.xmlhttp
    '            http = CreateObject("MSXML2.ServerXMLHTTP")


    '            mIRNNo = Trim(txtIRNNo.Text)

    '            mGetQRImg = "0"      ''0 for text , 1 for Image
    '            mGetSignedInvoice = "0"  ''1 - Signed Json of Invoice will be return, 0 - will not return signed Invoice.

    '            http.Open("POST", url, False)

    '            http.setRequestHeader("Content-Type", "application/json")

    '            mBody = "{""Push_Data_List"":{"
    '            mBody = mBody & """Data"": ["
    '            mBody = mBody & "{"

    '            mBody = mBody & """Irn"":""" & mIRNNo & ""","
    '            mBody = mBody & """GSTIN"":""" & mGSTIN & ""","
    '            mBody = mBody & """CDKey"":""" & mCDKey & ""","
    '            mBody = mBody & """EInvUserName"":""" & mEInvUserName & ""","
    '            mBody = mBody & """EInvPassword"":""" & mEInvPassword & ""","
    '            mBody = mBody & """EFUserName"":""" & mEFUserName & ""","
    '            mBody = mBody & """EFPassword"":""" & mEFPassword & """"

    '            mBody = mBody & "}"


    '            mBody = mBody & "]"
    '            mBody = mBody & "}"
    '            mBody = mBody & "}"

    '            http.Send(mBody)

    '            pResponseText = http.responseText
    '            pResponseText = Replace(pResponseText, "[", "")
    '            pResponseText = Replace(pResponseText, "]", "")
    '            pResponseText = Replace(pResponseText, """", "'")

    '            Dim post As Object
    '            pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Status = ""})).Status


    '            If pStaus = "1" Then
    '                mSignedQRCode = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .SignedQRCode = ""})).SignedQRCode ' JsonTest.item("SignedQRCode")
    '                mSignedInvoice = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .SignedInvoice = ""})).SignedInvoice ' JsonTest.item("SignedInvoice")

    '                PubDBCn.Errors.Clear()
    '                PubDBCn.BeginTrans()

    '                SqlStr = "INSERT INTO FIN_INVOICE_QRCODE " & vbCrLf _
    '                    & " ( MKEY, COMPANY_CODE, SIGNQRCODE ) VALUES (" & vbCrLf _
    '                    & " '" & LblMKey.Text & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
    '                    & " '" & mSignedQRCode & "')"

    '                PubDBCn.Execute(SqlStr)

    '                PubDBCn.CommitTrans()
    '                If GererateQRCodeImage(RefreshQRCode, mSignedQRCode) = False Then GoTo ErrPart
    '            End If

    '            If pStaus = "0" Then
    '                pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage 'JsonTest.item("ErrorMessage")  ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")
    '                MsgInformation(pError)
    '                http = Nothing
    '                RefreshQRCode = " "
    '                Exit Function
    '            End If

    '            http = Nothing
    '        End If
    '        '    Set httpGen = Nothing
    '        Exit Function
    'ErrPart:
    '        '    Resume
    '        'http = Nothing
    '        RefreshQRCode = " "
    '        MsgBox(Err.Description)

    '    End Function
    Private Sub ReportOnPackingSlip(ByRef Mode As Crystal.DestinationConstants, ByRef pBoxType As String)

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
        Dim mTotPktQty As Double = 0

        Dim mTotalQty As Double = 0
        Dim mInvoiceNo As String = ""
        Dim mInvoiceDate As String = ""
        Dim mMFGBy As String = ""
        Dim I As Long
        Dim mRowPrinting As Long
        Dim mQtyDesc As String
        Dim mPKTDesc As String
        Dim mBarCode As String

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
                & " And IH.MKEY='" & LblMKey.Text & "'"

        If pBoxType = "I" Then
            SqlStr = SqlStr & " AND ID.INNER_PACK_QTY>0"
        Else
            SqlStr = SqlStr & " AND ID.OUTER_PACK_QTY>0"
        End If

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
                    mQtyinBox = IIf(IsDBNull(RsTemp.Fields("INNER_PACK_QTY").Value), "", RsTemp.Fields("INNER_PACK_QTY").Value)
                Else
                    mQtyinBox = IIf(IsDBNull(RsTemp.Fields("OUTER_PACK_QTY").Value), "", RsTemp.Fields("OUTER_PACK_QTY").Value)
                End If

                If pBoxType = "I" Then
                    mQtyinBoxA = IIf(IsDBNull(RsTemp.Fields("INNER_PACK_QTY_A").Value), "", RsTemp.Fields("INNER_PACK_QTY_A").Value)
                Else
                    mQtyinBoxA = IIf(IsDBNull(RsTemp.Fields("OUTER_PACK_QTY_A").Value), "", RsTemp.Fields("OUTER_PACK_QTY_A").Value)
                End If


                mTotalQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), "", RsTemp.Fields("ITEM_QTY").Value)

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

                    mQtyDesc = mQtyinBox ''& " (" & I & "/" & mTotPktQty & ")"
                    mPKTDesc = I & "/" & mTotPktQty

                    mBarCode = mInvoiceNo & "#" & mPartNo & "#" & mQtyinBox & "#" & I

                    SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf _
                        & " FIELD1, FIELD2, FIELD3," & vbCrLf _
                        & " FIELD4, FIELD5, FIELD6, FIELD7,FIELD8, FIELD9 ) " & vbCrLf _
                        & " VALUES (" & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & I & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mPartName) & "', '" & MainClass.AllowSingleQuote(mPartNo) & "'," & vbCrLf _
                        & " '" & mQtyDesc & "', '" & mTotalQty & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mInvoiceNo) & "', '" & mInvoiceDate & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mMFGBy) & "','" & mPKTDesc & "','" & mBarCode & "') "


                    PubDBCn.Execute(SqlStr)
                Next

                'mRowPrinting = 1
                If mQtyinBoxA > 0 And mTotalQty > (mPktQty * mQtyinBox) Then    ''And pBoxType = "I" 
                    For I = 1 To 1

                        mQtyDesc = mQtyinBoxA ''& " (" & mRowPrinting + 1 & "/" & mTotPktQty & ")"

                        mPKTDesc = mRowPrinting + 1 & "/" & mTotPktQty

                        mBarCode = mInvoiceNo & "#" & mPartNo & "#" & mQtyinBoxA & "#" & I

                        SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf _
                            & " FIELD1, FIELD2, FIELD3," & vbCrLf _
                            & " FIELD4, FIELD5, FIELD6, FIELD7, FIELD8,FIELD9 ) " & vbCrLf _
                            & " VALUES (" & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & I & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mPartName) & "', '" & MainClass.AllowSingleQuote(mPartNo) & "'," & vbCrLf _
                            & " '" & mQtyDesc & "', '" & mTotalQty & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mInvoiceNo) & "', '" & mInvoiceDate & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mMFGBy) & "','" & mPKTDesc & "','" & mBarCode & "') "


                        PubDBCn.Execute(SqlStr)
                    Next
                End If

                RsTemp.MoveNext()
            Loop
            PubDBCn.CommitTrans()
        Else
            MsgInformation("Nothing to Print.")
            Exit Sub
        End If

        'SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, True, "")

        'Report1.MarginTop = 0
        'Report1.MarginBottom = 0
        'Report1.MarginLeft = 0
        'Report1.MarginRight = 0

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

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                FrmInvoiceViewer.CrystalReportViewer1.ShowPrintButton = False
            Else
                FrmInvoiceViewer.CrystalReportViewer1.ShowPrintButton = True
            End If


            'Report1.WindowShowPrintBtn = True '' IIf(PubSuperUser = "S", True, False)
            'Report1.WindowShowPrintSetupBtn = True ''IIf(PubSuperUser = "S", True, False)

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

            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            SqlStr = "UPDATE FIN_INVOICE_HDR SET  PRINT_PACKING= 'Y', " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " MODDATE='" & Format(PubCurrDate, "DD-MMM-YYYY") & "'" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND  Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"

            PubDBCn.Execute(SqlStr)

            PubDBCn.CommitTrans()
        End If

        ' 
        ' 

        'Report1.Destination = Mode
        'Report1.DiscardSavedData = True
        'MainClass.ReportWindow(Report1, mTitle)
        'Report1.Connect = STRRptConn

        'Report1.ReportFileName = PubReportFolderPath & mRptFileName

        'Report1.SQLQuery = "SELECT * FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        'Report1.WindowShowGroupTree = False

        'Report1.Action = 1
        'Report1.Reset()

        'Call ShowPackingReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        CrReport.Dispose()
    End Sub

    Private Sub txtDistanceUpdate_Click(sender As Object, e As EventArgs) Handles txtDistanceUpdate.Click
        On Error GoTo ErrPart
        If Trim(txtEWayBillNo.Text) = "" And txtBillNo.Text <> "" Then

            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            SqlStr = "UPDATE FIN_INVOICE_HDR SET TRANS_DISTANCE =" & Val(txtDistance.Text) & ", " & vbCrLf _
                    & " TRANSPORTER_GSTNO='" & txtTransportCode.Text & "', CARRIERS='" & txtCarriers.Text & "', VEHICLENO='" & txtVehicle.Text & "'" & vbCrLf _
                    & " WHERE MKEY='" & LblMKey.Text & "' " & vbCrLf _
                    & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

            PubDBCn.Execute(SqlStr)

            SqlStr = "UPDATE DSP_DESPATCH_HDR SET " & vbCrLf _
                    & " TRANSPORTER_NAME='" & txtCarriers.Text & "', VEHICLE_NO='" & txtVehicle.Text & "'" & vbCrLf _
                    & " WHERE AUTO_KEY_DESP=" & Val(txtDCNo.Text) & " " & vbCrLf _
                    & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

            PubDBCn.Execute(SqlStr)

            PubDBCn.CommitTrans()

        End If
        Exit Sub
ErrPart:
        PubDBCn.RollbackTrans()
    End Sub

    Private Sub cmdeWayBill_Click(sender As Object, e As EventArgs) Handles cmdeWayBill.Click
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

        If Trim(txtEWayBillNo.Text) = "" Then
            MsgInformation("Nothing to print.")
            Exit Sub
        End If


        If GetWebTeleWaySetupContents(url, "P", pCDKey, pEFUserName, pEFPassword, pEWBUserName, pEWBPassword, "N") = False Then GoTo ErrPart

        Dim http As Object  ' MSXML2.XMLHTTP60 '' MSXML.xmlhttp
        http = CreateObject("MSXML2.ServerXMLHTTP")
        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")

        ''        .Clear
        ''        .IsArray = False 'Actually the default after Clear.

        ''        .item("GSTIN") = IIf(IsNull(RsCompany!COMPANY_GST_RGN_NO), "", RsCompany!COMPANY_GST_RGN_NO)
        ''        .item("ewbNo") = Trim(txtEWayBillNo.Text)
        ''        .item("Year") = Year(txtBillDate.Text)
        ''        .item("Month") = Month(txtBillDate.Text)
        ''        .item("EFUserName") = pEFUserName
        ''        .item("EFPassword") = pEFPassword
        ''        .item("CDKey") = pCDKey
        ''        .item("EWBUserName") = pEWBUserName
        ''        .item("EWBPassword") = pEWBPassword
        ''        mBody = .JSON

        Dim details As New List(Of EWAYBILLPRN)()

        details.Add(New EWAYBILLPRN() With {
            .GSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value),
            .ewbNo = Trim(txtEWayBillNo.Text),
            .Year = Year(txtBillDate.Text),
            .Month = Month(txtBillDate.Text),
            .EFUserName = pEFUserName,
            .EFPassword = pEFPassword,
            .CDKey = pCDKey,
            .EWBUserName = pEWBUserName,
            .EWBPassword = pEWBPassword
         })

        Dim mBodyDetail As String = JsonConvert.SerializeObject(details)


        'mBody = "{""Push_Data_List"":"
        'mBody = mBody & """Data"": "
        mBody = mBody & mBodyDetail
        mBody = Replace(mBody, "[", "")
        mBody = Replace(mBody, "]", "")
        'mBody = mBody & "]"
        'mBody = mBody & "}"

        http.Send(mBody)

        Dim pResponseText As String = http.responseText
        '    pResponseText = Replace(pResponseText, "\", "")							
        'pResponseText = Replace(pResponseText, "[", "")
        'pResponseText = Replace(pResponseText, "]", "")
        '    pResponseText = Mid(pResponseText, 2, Len(pResponseText) - 2)							

        If pResponseText <> "" Then
            Process.Start("explorer.exe", pResponseText)
        End If

        'Dim pStaus As String
        'pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .IsSuccess = ""})).IsSuccess

        'If pStaus = "false" Then

        'Else

        'End If

        '    Dim meWayResponseID As String
        '    Dim meWayBillDate As String
        '    Dim meWayBillUpto As String
        '    Dim SqlStr As String = ""

        '    Dim meWayFilePath As String

        '    meWayResponseID = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .EWayBill = ""})).EWayBill   'JsonTest.Item("EWayBill")
        '    meWayBillDate = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Date = ""})).Date 'JsonTest.Item("Date")
        '    meWayBillUpto = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .validUpto = ""})).validUpto ' JsonTest.Item("AckDate") 'JsonTest.Item("elements").Item(mResponseId).Item("ewayBillDate")						

        '    PubDBCn.Errors.Clear()
        '    PubDBCn.BeginTrans()

        '    SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
        '            & " E_BILLWAYNO ='" & Val(meWayResponseID) & "'," & vbCrLf _
        '            & " E_BILLWAYDATE =TO_DATE('" & VB6.Format(meWayBillDate, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
        '            & " E_BILLWAYVAILDUPTO =TO_DATE('" & VB6.Format(meWayBillUpto, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
        '            & " E_BILLWAYFILEPATH =''" & vbCrLf _
        '            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '            & " AND MKEY ='" & pMKey & "'"

        '    PubDBCn.Execute(SqlStr)

        '    PubDBCn.CommitTrans()
        'End If

        'If pStaus = "0" Then
        '    pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
        '    MsgInformation(pError)
        '    WebRequestEWayBillByIRN = False
        '    http = Nothing
        '    Exit Functio

        'mFilePath = http.responseText
        '    If mFilePath <> "" Then
        '        ShellExecute Me.hWnd, "open", mFilePath, vbNullString, vbNullString, SW_SHOWNORMAL
        'End If

        Exit Sub
ErrPart:
        ''Resume
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
        'PubDBCn.RollbackTrans()
    End Sub

    Private Sub FrmInvoiceGST_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 350, mReFormWidth - 350, mReFormWidth))
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        TabMain.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        Frame6.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdResetPO_Click(sender As Object, e As EventArgs) Handles cmdResetPO.Click
        Try
            Dim mPONO As String = ""
            Dim mPODate As String = ""
            Dim SqlStr As String
            Dim RsTemp As ADODB.Recordset
            Dim xCustomerCode As String
            Dim mShippedTo As String
            Dim mShipTo As String
            Dim mShippSameasBillTo As String
            Dim mIRNNo As String
            Dim meWayNo As String

            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xCustomerCode = MasterNo
            End If

            SqlStr = "SELECT CUST_PO_NO, CUST_PO_DATE, SHIPPED_TO_PARTY_CODE, SHIPPED_TO_SAMEPARTY, SHIP_TO_LOC_ID" & vbCrLf _
                    & " FROM  DSP_SALEORDER_HDR IH" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                    & " AND IH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf _
                    & " AND IH.MKEY = ("

            SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH" & vbCrLf _
                    & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SIH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                    & " AND SIH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf _
                    & " )"

            ''AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mPONO = IIf(IsDBNull(RsTemp.Fields("CUST_PO_NO").Value), "", RsTemp.Fields("CUST_PO_NO").Value)
                mPODate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CUST_PO_DATE").Value), "", RsTemp.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")
                mShippedTo = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
                mShipTo = IIf(IsDBNull(RsTemp.Fields("SHIP_TO_LOC_ID").Value), "", RsTemp.Fields("SHIP_TO_LOC_ID").Value)
                mShippSameasBillTo = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
            End If

            SqlStr = "UPDATE FIN_INVOICE_HDR SET CUST_PO_NO ='" & mPONO & "', " & vbCrLf _
                    & " CUST_PO_DATE=TO_DATE('" & VB6.Format(mPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE MKEY='" & LblMKey.Text & "' " & vbCrLf _
                    & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

            PubDBCn.Execute(SqlStr)

            ''CUST_PO_NO, CUST_PO_DATE, OUR_AUTO_KEY_SO, OUR_SO_DATE

            SqlStr = "UPDATE DSP_DESPATCH_HDR SET " & vbCrLf _
                    & " VENDOR_PO='" & mPONO & "', VENDOR_PO_DATE=TO_DATE('" & VB6.Format(mPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE AUTO_KEY_DESP=" & Val(txtDCNo.Text) & " " & vbCrLf _
                    & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

            PubDBCn.Execute(SqlStr)

            SqlStr = "UPDATE DSP_DESPATCH_DET SET " & vbCrLf _
                    & " CUST_PO='" & mPONO & "', CUST_PO_DATE=TO_DATE('" & VB6.Format(mPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE AUTO_KEY_DESP=" & Val(txtDCNo.Text) & " AND SONO=" & Val(lblPoNo.Text) & "" & vbCrLf _
                    & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

            PubDBCn.Execute(SqlStr)

            SqlStr = "SELECT E_BILLWAYNO, IRN_NO" & vbCrLf _
                    & " FROM  FIN_INVOICE_HDR IH" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                    & " AND MKEY='" & LblMKey.Text & "' "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mIRNNo = IIf(IsDBNull(RsTemp.Fields("IRN_NO").Value), "", RsTemp.Fields("IRN_NO").Value)
                meWayNo = IIf(IsDBNull(RsTemp.Fields("E_BILLWAYNO").Value), "", RsTemp.Fields("E_BILLWAYNO").Value)

                If mIRNNo = "" And meWayNo = "" Then
                    SqlStr = "UPDATE FIN_INVOICE_HDR SET SHIPPED_TO_SAMEPARTY ='" & MainClass.AllowDoubleQuote(mShippSameasBillTo) & "' , " & vbCrLf _
                        & " SHIPPED_TO_PARTY_CODE = '" & MainClass.AllowDoubleQuote(mShippedTo) & "', SHIP_TO_LOC_ID = '" & MainClass.AllowDoubleQuote(mShipTo) & "'" & vbCrLf _
                        & " WHERE MKEY='" & LblMKey.Text & "' " & vbCrLf _
                        & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

                    PubDBCn.Execute(SqlStr)

                    SqlStr = "UPDATE DSP_DESPATCH_HDR SET SHIPPED_TO_SAMEPARTY ='" & MainClass.AllowDoubleQuote(mShippSameasBillTo) & "' , " & vbCrLf _
                            & " SHIPPED_TO_PARTY_CODE = '" & MainClass.AllowDoubleQuote(mShippedTo) & "', SHIP_TO_LOC_ID ='" & MainClass.AllowDoubleQuote(mShipTo) & "'" & vbCrLf _
                            & " WHERE AUTO_KEY_DESP=" & Val(txtDCNo.Text) & " " & vbCrLf _
                            & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

                    PubDBCn.Execute(SqlStr)
                End If
            End If

            PubDBCn.CommitTrans()
        Catch ex As Exception
            PubDBCn.RollbackTrans()
        End Try
    End Sub

    Private Sub CmdPopFromFile_Click(sender As Object, e As EventArgs) Handles CmdPopFromFile.Click
        Try
            Dim strFilePath As String = ""
            Dim intflag As Integer
            CommonDialogOpen.FileName = ""

            intflag = CommonDialogOpen.ShowDialog()

            If intflag = 1 Then
                If CommonDialogOpen.FileName <> "" Then
                    strFilePath = CommonDialogOpen.FileName
                    'strfilename = CommonDialogOpen.SafeFileName
                    Call PopulateFromXLSFile(strFilePath)
                End If
            End If

        Catch ex As Exception

        End Try
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""

        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""

        Dim mCode As Long
        Dim mDeptCode As String
        Dim mLevelCode As Long
        Dim mProcessCode As Long
        Dim mDeptName As String
        Dim mLevelName As String
        Dim mProcessName As String
        Dim mSkillName As String
        Dim mSkillCode As String
        Dim mRate As Double
        Dim mSNO As Double

        'MainClass.ClearGrid(SprdMain)
        'FormatSprdMain(-1)


        Dim ErrorFile As System.IO.StreamWriter


        Dim FileName As String = Path.GetFileName(strXLSFile)
        Dim Extension As String = Path.GetExtension(strXLSFile)


        Dim conStr As String = ""
        Select Case UCase(Extension)
            Case ".XLS"
                'Excel 97-03 
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'"
                Exit Select
            Case ".XLSX"
                'Excel 07 
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"
                Exit Select
        End Select

        conStr = String.Format(conStr, strXLSFile)    ''isHDR='Yes'

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet 
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()

        connExcel.Close()

        'Read Data from First Sheet 
        connExcel.Open()

        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"      '' ORDER BY 4 DESC
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Dim mItemDesc As String
        Dim CntRow As Long = 1
        Dim mPartNo As String
        Dim mUOM As String
        Dim mQty As Double = 0
        Dim mBillNo As String
        Dim mBillNoSeq As String = 0

        If dt.Rows.Count >= 1 Then
            For Each dtRow In dt.Rows
                Dim mItemCode = Trim(IIf(IsDBNull(dtRow.item(0)), "", dtRow.item(0)))
                mItemDesc = ""
                mPartNo = IIf(IsDBNull(dtRow.item(2)), "", dtRow.item(2))

                mBillNo = IIf(IsDBNull(dtRow.item(4)), "", dtRow.item(4))
                mQty = IIf(IsDBNull(dtRow.item(5)), "", dtRow.item(5))
                mSNO = IIf(IsDBNull(dtRow.item(6)), 0, dtRow.item(6))
                mRate = IIf(IsDBNull(dtRow.item(7)), 0, dtRow.item(7))

                With SprdMain
                    .Row = mSNO
                    .Col = ColItemCode
                    If mItemCode = Trim(.Text) Then
                        .Col = ColRate
                        .Text = Val(mRate)
                    End If
                End With

            Next
        End If


        Exit Sub
ErrPart:
        PubDBCn.RollbackTrans()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
    Private Sub txtStoreDetail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStoreDetail.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtStoreDetail_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStoreDetail.DoubleClick
        Call SearchStoreDetail()
    End Sub
    Private Sub txtStoreDetail_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStoreDetail.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtStoreDetail.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtStoreDetail_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtStoreDetail.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchStoreDetail()
    End Sub
    Private Sub txtStoreDetail_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtStoreDetail.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String


        If Trim(txtStoreDetail.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtStoreDetail.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgBox("Invalid Store Details.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SearchStoreDetail()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        If ADDMode = True Then
            SqlStr = SqlStr & " AND STATUS='O'"
        End If
        If MainClass.SearchGridMaster((txtStoreDetail.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtStoreDetail.Text = AcName
            txtStoreDetail_Validating(txtStoreDetail, New System.ComponentModel.CancelEventArgs(True))
            '        If TxtRemarks.Enabled = True Then TxtRemarks.SetFocus						
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtApplicant_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApplicant.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtApplicant_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApplicant.DoubleClick
        Call SearchApplicant()
    End Sub
    Private Sub txtApplicant_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtApplicant.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtApplicant.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtApplicant_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtApplicant.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchApplicant()
    End Sub
    Private Sub txtApplicant_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtApplicant.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String


        If Trim(txtApplicant.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtApplicant.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgBox("Invalid Shipped to Supplier Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchApplicant()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        If ADDMode = True Then
            SqlStr = SqlStr & " AND STATUS='O'"
        End If
        If MainClass.SearchGridMaster((txtApplicant.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtApplicant.Text = AcName
            txtApplicant_Validating(txtApplicant, New System.ComponentModel.CancelEventArgs(True))
            '        If TxtRemarks.Enabled = True Then TxtRemarks.SetFocus						
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

End Class
