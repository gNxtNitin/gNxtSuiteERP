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
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl

Imports System.Data
Imports System.IO
Imports System.Configuration

Imports System.Data.OleDb
Friend Class frmSaleOrderUpload
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColCustPONO As Short = 1
    Private Const ColCustPODate As Short = 2
    Private Const ColPlant As Short = 3
    Private Const ColCustomerCode As Short = 4
    Private Const ColCustomerName As Short = 5
    Private Const ColPartNo As Short = 6
    Private Const ColItemCode As Short = 7
    Private Const ColItemName As Short = 8
    Private Const ColQty As Short = 9
    Private Const ColRate As Short = 10
    Private Const ColDeliveryDate As Short = 11
    Private Const ColMkey As Short = 12
    Private Const ColSONo As Short = 13
    Private Const ColSODate As Short = 14
    Private Const ColAmend As Short = 15
    Private Const ColERPQty As Short = 16

    'Private Const ColFlag As Short = 13

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()

    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim SqlStr As String
        Dim mUpdateCount As Integer
        Dim mMKey As Double
        Dim mCustomerName As String
        Dim mItemCode As String

        Dim mQty As Double
        Dim mPrice As Double
        Dim mPartNo As String
        Dim mRow As UltraGridRow
        Dim mMaxRow As Long
        mUpdateCount = 0

        Dim mSuppCustCode As String
        Dim mDeliveryDate As String
        Dim mCustomerPONo As String
        Dim mCustomerPODate As String
        Dim mSONo As Double
        Dim mSODate As String
        Dim mAmendNo As Double
        Dim mERPQty As String
        Dim mTotQty As Double



        mMaxRow = UltraGrid1.Rows.Count

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        With UltraGrid1
            For cntRow = 0 To mMaxRow - 1
                mRow = Me.UltraGrid1.Rows(cntRow)


                mMKey = Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMkey - 1)))
                mSuppCustCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerCode - 1))
                mItemCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1))
                mCustomerPONo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustPONO - 1))
                mCustomerPODate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustPODate - 1))

                mSONo = Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColSONo - 1)))
                mSODate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColSODate - 1))
                mPartNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartNo - 1))

                mQty = Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1)))
                mPrice = Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColRate - 1)))

                mDeliveryDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDeliveryDate - 1))
                mAmendNo = Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmend - 1)))
                mERPQty = Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColERPQty - 1)))

                If mSuppCustCode <> "" And mItemCode <> "" Then

                    If UpdateSOMain1(mMKey, mSuppCustCode, mItemCode, mPartNo, mCustomerPONo, mCustomerPODate,
                                mSONo, mSODate, mAmendNo, mQty, mPrice, mDeliveryDate, mERPQty) = False Then GoTo ErrPart

                End If
NextRowNo:

            Next
        End With

        PubDBCn.CommitTrans()
        CmdSave.Enabled = False

        Exit Sub
ErrPart:
        ''Resume
        PubDBCn.RollbackTrans() ''
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
        'PubDBCn.RollbackTrans()
    End Sub
    Private Function UpdateSOMain1(ByVal mMKey As Double, ByVal mSuppCustCode As String, ByVal mItemCode As String, ByVal mPartNo As String, ByVal mCustomerPONo As String, ByVal mCustomerPODate As String,
                             ByVal mSONo As Double, ByVal mSODate As String, ByVal mAmendNo As Double, ByVal mQty As Double, ByVal mPrice As Double, ByVal mDeliveryDate As String, ByVal mERPQty As Double) As Boolean

        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""
        Dim nMkey As Double
        Dim nSONo As Double
        Dim nSODate As String
        Dim nAmendNo As Double
        Dim nNewAmendNo As Double
        Dim pBillTo As String
        Dim pCustAmendNo As Double = 0
        Dim pAddMode As Boolean
        Dim RsTemp As ADODB.Recordset = Nothing




        If mMKey = 0 Then
            SqlStr = "SELECT MKEY, AUTO_KEY_SO, SO_DATE, AMEND_NO,CUST_AMEND_NO" & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR IH" & vbCrLf _
                    & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                    & " AND CUST_PO_NO ='" & mCustomerPONo & "' " & vbCrLf _
                    & " AND SUPP_CUST_CODE= '" & mSuppCustCode & "' " & vbCrLf _
                    & " AND MKEY = (" & vbCrLf _
                    & " SELECT MAX(MKEY) " & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR " & vbCrLf _
                    & " WHERE Company_Code= IH.COMPANY_CODE" & vbCrLf _
                    & " AND CUST_PO_NO =IH.CUST_PO_NO" & vbCrLf _
                    & " AND SUPP_CUST_CODE= IH.SUPP_CUST_CODE AND SO_STATUS='O') "
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                nMkey = IIf(IsDBNull(RsTemp.Fields("MKEY").Value), 0, RsTemp.Fields("MKEY").Value)
                nSONo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_SO").Value), 0, RsTemp.Fields("AUTO_KEY_SO").Value)
                nSODate = IIf(IsDBNull(RsTemp.Fields("SO_DATE").Value), "", RsTemp.Fields("SO_DATE").Value)
                nAmendNo = IIf(IsDBNull(RsTemp.Fields("AMEND_NO").Value), 0, RsTemp.Fields("AMEND_NO").Value)
                pCustAmendNo = IIf(IsDBNull(RsTemp.Fields("CUST_AMEND_NO").Value), 0, RsTemp.Fields("CUST_AMEND_NO").Value) + 1
            End If
        Else
            nMkey = mMKey
            nSONo = mSONo
            nSODate = mSODate
            nAmendNo = mAmendNo
        End If

        If nMkey = 0 Then
            pAddMode = True
            nSONo = AutoGenPONoSeq()
            nNewAmendNo = 0
            nSODate = PubCurrDate
            nMkey = nSONo & VB6.Format(nNewAmendNo, "000")
        Else
            SqlStr = "SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf _
               & " FROM DSP_SALEORDER_HDR IH" & vbCrLf _
               & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
               & " AND AUTO_KEY_SO ='" & nSONo & "' " & vbCrLf _
               & " AND SUPP_CUST_CODE= '" & mSuppCustCode & "' " & vbCrLf _
               & " AND SO_STATUS='O' AND SO_APPROVED='N' HAVING COUNT(1)>0"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                If IsDBNull(RsTemp.Fields("AMEND_NO").Value) Then
                    nNewAmendNo = nAmendNo + 1
                    pAddMode = True
                Else
                    nNewAmendNo = IIf(IsDBNull(RsTemp.Fields("AMEND_NO").Value), 0, RsTemp.Fields("AMEND_NO").Value)
                    pAddMode = False
                End If

                'If nNewAmendNo < nAmendNo Then
                '    pAddMode = False
                '    nNewAmendNo = nAmendNo
                'Else
                '    nNewAmendNo = nAmendNo + 1
                '    pAddMode = True
                'End If

            Else
                nNewAmendNo = nAmendNo + 1
                pAddMode = True
            End If
            nMkey = nSONo & VB6.Format(nNewAmendNo, "000")
        End If

        pBillTo = GetDefaultLocation(mSuppCustCode)

        If pAddMode = True Then


            SqlStr = " INSERT INTO DSP_SALEORDER_HDR ( " & vbCrLf & " MKEY, AUTO_KEY_SO,  COMPANY_CODE," & vbCrLf _
                & " SO_DATE, SUPP_CUST_CODE, CUST_PO_NO, " & vbCrLf _
                & " CUST_PO_DATE, CUST_AMEND_NO, AMEND_NO, AMEND_DATE, " & vbCrLf _
                & " AMEND_WEF_FROM, " & vbCrLf & " ROAD_PERMIT, TYPE_OF_SALE," & vbCrLf _
                & " COMM_DTLS, LC_CLAIMS, INSPECTION_DTL, " & vbCrLf _
                & " DESTINATION_DTL, TRANSPORTER_DTL, MODE_OF_DELV, " & vbCrLf _
                & " FREIGHT_CHARGES, OCTROI_DTL, INSURANCE_DTL, " & vbCrLf _
                & " PAYMENT_DTL, BALANCE_PAY_DTL, DESPATCH_DTL, " & vbCrLf _
                & " SALETAX_PER, EXCISE_DUTY_PER, DISCOUNT_PER, " & vbCrLf _
                & " SO_STATUS, REMARKS, ORDER_TYPE, " & vbCrLf _
                & " ADDUSER, ADDDATE," & vbCrLf _
                & " MODUSER, MODDATE,SO_APPROVED,GOODS_SERVICE, SAC_CODE, ISGSTENABLE_PO, EPCG_NO, EPCG_DATE," & vbCrLf _
                & " BILL_TO_LOC_ID, SHIP_TO_LOC_ID, SHIPPED_TO_PARTY_CODE, SHIPPED_TO_SAMEPARTY,DELIVERY_INSTRUCTION_REQ,PO_TYPE," & vbCrLf _
                & " VENDOR_CODE,SCHD_AGREEMENT_NO, SCHD_AGREEMENT_DATE," & vbCrLf _
                & " PROJECT_CODE, SALE_PERSON_CODE, PAYMENT_TYPE, CHEQUE_NO, PO_AMEND_REASON) "

            SqlStr = SqlStr & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & Val(nMkey) & ", " & nSONo & ", " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(nSODate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mCustomerPONo) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mCustomerPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " " & Val(pCustAmendNo) & ", " & vbCrLf _
                & " " & Val(nNewAmendNo) & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(nSODate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), "




            SqlStr = SqlStr & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '', " & vbCrLf _
                & " 0,0,0,'O','','C'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & vbCrLf _
                & " 'N', 'G', '','Y','','', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(pBillTo) & "','" & MainClass.AllowSingleQuote(pBillTo) & "','" & MainClass.AllowSingleQuote(mSuppCustCode) & "'," & vbCrLf _
                & "'Y','N','R',''," & vbCrLf _
                & " '','', " & vbCrLf _
                & " " & "NULL" & ", '', '', '',''" & vbCrLf _
                & ")"
        Else

            SqlStr = " UPDATE DSP_SALEORDER_HDR SET " & vbCrLf _
                & " VENDOR_CODE=''" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND MKEY =" & Val(nMkey) & ""

            'SqlStr = SqlStr & vbCrLf _
            '        & " AUTO_KEY_SO=" & mSONo & ", SO_APPROVED='" & mApproved & "',VENDOR_CODE='" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "'," & vbCrLf _
            '        & " SO_DATE=TO_DATE('" & VB6.Format(txtSODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            '        & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
            '        & " CUST_PO_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "', " & vbCrLf _
            '        & " CUST_PO_DATE=TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            '        & " CUST_AMEND_NO=" & Val(txtCustAmendNo.Text) & ", " & vbCrLf _
            '        & " AMEND_NO=" & Val(txtAmendNo.Text) & ", " & vbCrLf _
            '        & " AMEND_DATE=TO_DATE('" & VB6.Format(txtAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            '        & " AMEND_WEF_FROM=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            '        & " SHIPPED_TO_PARTY_CODE='" & MainClass.AllowSingleQuote(mShipToCustCode) & "',SHIPPED_TO_SAMEPARTY='" & MainClass.AllowSingleQuote(mShipToSameBillTo) & "',  " & vbCrLf _
            '        & " PROJECT_CODE = " & IIf(Val(mProjectCode) = 0, "NULL", Val(mProjectCode)) & ", SALE_PERSON_CODE = '" & MainClass.AllowSingleQuote(mSalePersonCode) & "', PAYMENT_TYPE = '" & MainClass.AllowSingleQuote(mPaymentType) & "', CHEQUE_NO = '" & MainClass.AllowSingleQuote(txtChqNo.Text) & "', "


            'SqlStr = SqlStr & vbCrLf _
            '& " ROAD_PERMIT='" & MainClass.AllowSingleQuote(txtRoadPermit.Text) & "', PO_TYPE='" & mPOType & "'," & vbCrLf _
            '& " TYPE_OF_SALE='" & MainClass.AllowSingleQuote(txtSaleType.Text) & "', " & vbCrLf _
            '& " COMM_DTLS='" & MainClass.AllowSingleQuote(txtCommission.Text) & "', " & vbCrLf _
            '& " LC_CLAIMS='" & MainClass.AllowSingleQuote(txtLCClaim.Text) & "', " & vbCrLf _
            '& " INSPECTION_DTL='" & MainClass.AllowSingleQuote(txtInspection.Text) & "', " & vbCrLf _
            '& " DESTINATION_DTL='" & MainClass.AllowSingleQuote(txtDestination.Text) & "', " & vbCrLf _
            '& " TRANSPORTER_DTL='" & MainClass.AllowSingleQuote(txtTransporter.Text) & "', " & vbCrLf _
            '& " MODE_OF_DELV='" & MainClass.AllowSingleQuote(txtDespMode.Text) & "', " & vbCrLf _
            '& " FREIGHT_CHARGES='" & MainClass.AllowSingleQuote(txtFreight.Text) & "', " & vbCrLf _
            '& " OCTROI_DTL='" & MainClass.AllowSingleQuote(txtOctroi.Text) & "', " & vbCrLf _
            '& " INSURANCE_DTL='" & MainClass.AllowSingleQuote(txtInsurance.Text) & "', " & vbCrLf _
            '& " PAYMENT_DTL='" & MainClass.AllowSingleQuote(txtPayment.Text) & "', " & vbCrLf _
            '& " BALANCE_PAY_DTL='" & MainClass.AllowSingleQuote(txtBalPayment.Text) & "', " & vbCrLf _
            '& " DESPATCH_DTL='" & MainClass.AllowSingleQuote(txtDescDetail.Text) & "', " & vbCrLf _
            '& " SO_STATUS='" & mStatus & "', " & vbCrLf _
            '& " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
            '& " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'," & vbCrLf _
            '& " SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtShipTo.Text) & "'," & vbCrLf _
            '& " ORDER_TYPE='" & mOrderType & "', DELIVERY_INSTRUCTION_REQ='" & mDI & "'," & vbCrLf _
            '& " GOODS_SERVICE='" & VB.Left(cboInvType.Text, 1) & "', SAC_CODE = '" & mSACCode & "', " & vbCrLf _
            '& " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', PO_AMEND_REASON='" & cboReason.Text & "', " & vbCrLf _
            '& " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
            '& " EPCG_NO='" & MainClass.AllowSingleQuote(txtEPCGNo.Text) & "',EPCG_DATE=TO_DATE('" & VB6.Format(txtEPCGDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
            '& " SCHD_AGREEMENT_NO='" & MainClass.AllowSingleQuote(txtScheduleAggNo.Text) & "',SCHD_AGREEMENT_DATE=TO_DATE('" & VB6.Format(txtScheduleAggDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            '& " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            '& " AND MKEY =" & Val(lblMkey.Text) & ""

        End If


        PubDBCn.Execute(SqlStr)

DetailPart:

        If UpdateDetail1(nMkey, mSuppCustCode, mItemCode, mPartNo, mCustomerPONo, mCustomerPODate,
                                nSONo, nSODate, nNewAmendNo, mQty, mPrice, mDeliveryDate, mERPQty, pBillTo) = False Then GoTo ErrPart

        If UpdateDailyDSDetail(nMkey, mSuppCustCode, mItemCode, mPartNo, mCustomerPONo, mCustomerPODate,
                                nSONo, nSODate, nNewAmendNo, mQty, mPrice, mDeliveryDate, mERPQty) = False Then GoTo ErrPart

        UpdateSOMain1 = True


        Exit Function
ErrPart:
        '    Resume
        UpdateSOMain1 = False

        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function
    Private Function UpdateDailyDSDetail(ByVal mMKey As Double, ByVal mSuppCustCode As String, ByVal mItemCode As String,
                                ByVal mPartNo As String, ByVal mCustomerPONo As String, ByVal mCustomerPODate As String,
                                ByVal mSONo As Double, ByVal mSODate As String, ByVal mAmendNo As Double, ByVal mQty As Double,
                                ByVal mPrice As Double, ByVal mDeliveryDate As String, ByVal mERPQty As Double) As Boolean
        On Error GoTo UpdateErr1
        Dim SqlStr As String = ""
        Dim ii As Long = 0
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTotQty As Double = 0

        mTotQty = GetTotalItemQty(mCustomerPONo, mItemCode, mSuppCustCode, mDeliveryDate)

        SqlStr = "SELECT SERIAL_NO FROM DSP_SALEORDER_DET " & vbCrLf _
            & " Where " & vbCrLf _
            & " MKEY=" & Val(mMKey) & " AND ITEM_CODE='" & mItemCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            ii = IIf(IsDBNull(RsTemp.Fields("SERIAL_NO").Value), 0, RsTemp.Fields("SERIAL_NO").Value)
        End If


        SqlStr = "INSERT INTO DSP_DAILY_SCHLD_DET (" & vbCrLf _
                & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE,REQ_DATE,LOC_CODE,OD_NO,BOOKTYPE ) VALUES (" & vbCrLf _
                & " " & Val(mSONo) & ", " & ii & ", '" & mItemCode & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mDeliveryDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mTotQty & ", 0, " & vbCrLf _
                & " 0, '" & mSuppCustCode & "', TO_DATE('" & VB6.Format(mSODate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','','S') "

        PubDBCn.Execute(SqlStr)


        UpdateDailyDSDetail = True
        Exit Function
UpdateErr1:
        'Resume
        UpdateDailyDSDetail = False
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
    End Function
    Public Function DeleteDSDailyDetail(ByRef pDBCn As ADODB.Connection, ByRef pSONo As Double, ByRef pItemCode As String, ByRef pDelDate As String) As Boolean
        Dim SqlStr As String = ""
        On Error GoTo DeleteDSDailyDetailErr
        SqlStr = ""
        SqlStr = "DELETE FROM DSP_DAILY_SCHLD_DET  " & vbCrLf _
            & " WHERE AUTO_KEY_DELV=" & Val(CStr(pSONo)) & "" & vbCrLf _
            & " And ITEM_CODE='" & pItemCode & "' AND SERIAL_DATE=TO_DATE('" & VB6.Format(pDelDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND BOOKTYPE='S'"

        pDBCn.Execute(SqlStr)

        DeleteDSDailyDetail = True
        Exit Function
DeleteDSDailyDetailErr:
        MsgInformation(Err.Description)
        DeleteDSDailyDetail = False
    End Function
    Private Function UpdateDetail1(ByVal mMKey As Double, ByVal mSuppCustCode As String, ByVal mItemCode As String,
                                ByVal mPartNo As String, ByVal mCustomerPONo As String, ByVal mCustomerPODate As String,
                                ByVal mSONo As Double, ByVal mSODate As String, ByVal mAmendNo As Double, ByVal mQty As Double,
                                ByVal mPrice As Double, ByVal mDeliveryDate As String, ByVal mERPQty As Double, ByVal pBillTo As String) As Boolean


        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer

        Dim mItemUOM As String = ""
        Dim mPackType As String = ""
        Dim mColorDesc As String
        Dim mMRP As Double = 0
        Dim mPOWEF As String
        Dim mMRTCost As Double
        Dim mMSPCostAdd As Double
        Dim mProcessCost As Double
        Dim mMSPCost As Double
        Dim mFreightCost As Double
        Dim mValidQty As Double
        Dim mValidDate As String
        Dim mCGSTPer As String = 0
        Dim mSGSTPer As String = 0
        Dim mIGSTPer As String = 0
        Dim mAcctCode As String = ""
        Dim mAcctName As String
        Dim mHSNCode As String
        Dim mRemarks As String
        Dim mSOStatus As String
        Dim mItemSNo As String
        Dim mAddItemDesc As String
        Dim mCustStoreLoc As String
        Dim mItemQty As Double
        Dim mItemDiscount As Double
        Dim mTODDiscount As Double
        Dim mOtherDiscount As Double
        Dim mPktQty As Double
        Dim mSize As String
        Dim mModelNo As String
        Dim mDrawingNo As String
        Dim mGlassDescription As String

        Dim mActualHeight As Double
        Dim mActualWidth As Double
        Dim mChargeableHeight As Double
        Dim mChargeableWidth As Double
        Dim mArea As Double
        Dim mAreaRate As Double
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pAddMode As Boolean
        Dim mTotQty As String
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String

        If DeleteDSDailyDetail(PubDBCn, mSONo, mItemCode, mDeliveryDate) = False Then GoTo UpdateDetail1


        SqlStr = "SELECT *  FROM DSP_SALEORDER_DET " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " MKEY=" & Val(mMKey) & " AND ITEM_CODE='" & mItemCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pAddMode = False
        Else
            pAddMode = True
        End If
        SqlStr = ""

        mTotQty = GetTotalItemQty(mCustomerPONo, mItemCode, mSuppCustCode, "")

        mItemUOM = ""

        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mItemUOM = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "HSN_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mHSNCode = MasterNo
        End If

        mLocal = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(pBillTo), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(pBillTo), "GST_RGN_NO")


        If GetHSNDetails(mHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, "0", mPartyGSTNo) = False Then GoTo UpdateDetail1

        mAcctCode = GetSaleInvoiceType(mItemCode, mSuppCustCode)

        mSOStatus = "N"


        If mItemCode <> "" And mPrice > 0 Then

            If pAddMode = True Then

                SqlStr = "SELECT MAX(SERIAL_NO) AS SERIAL_NO FROM DSP_SALEORDER_DET " & vbCrLf _
                        & " Where " & vbCrLf _
                        & " MKEY=" & Val(mMKey) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    I = IIf(IsDBNull(RsTemp.Fields("SERIAL_NO").Value), 0, RsTemp.Fields("SERIAL_NO").Value) + 1
                End If

                SqlStr = " INSERT INTO DSP_SALEORDER_DET ( " & vbCrLf _
                        & " COMPANY_CODE, MKEY, SERIAL_NO, " & vbCrLf _
                        & " SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf _
                        & " UOM_CODE, PART_NO,ITEM_PRICE, " & vbCrLf _
                        & " PACK_TYPE, COLOUR_DTL, ITEM_MRP, AMEND_WEF, " & vbCrLf _
                        & " MATERIAL_COST, PROCESS_COST, MSP_COST, " & vbCrLf _
                        & " FREIGHT_COST, VALID_QTY, VALID_DATE, " & vbCrLf _
                        & " CGST_PER, SGST_PER, IGST_PER, ACCOUNT_POSTING_CODE, " & vbCrLf _
                        & " HSN_CODE, REMARKS, SO_ITEM_STATUS, ITEM_SNO, MSP_COST_ADD, ADD_ITEM_DESCRIPTION, CUST_STORE_LOC," & vbCrLf _
                        & " SO_QTY, ITEM_DISC, TOD_DISC, OTH_DISC, PACK_QTY, ITEM_SIZE, ITEM_MODEL, ITEM_DRAWINGNO," & vbCrLf _
                        & " GLASS_DESC, ACTUAL_HEIGHT, ACTUAL_WIDTH," & vbCrLf _
                        & " CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH, GLASS_AREA," & vbCrLf _
                        & " AREA_RATE" & vbCrLf _
                        & " ) "

                SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " " & Val(mMKey) & "," & I & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mSuppCustCode) & "'," & vbCrLf _
                        & " '" & mItemCode & "','" & mItemUOM & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mPartNo) & "', " & mPrice & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mPackType) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mColorDesc) & "', " & mMRP & ",TO_DATE('" & VB6.Format(mPOWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " " & mMRTCost & "," & mProcessCost & "," & mMSPCost & "," & mFreightCost & "," & vbCrLf _
                        & " " & mValidQty & ",TO_DATE('" & VB6.Format(mValidDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ",'" & MainClass.AllowSingleQuote(mAcctCode) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mHSNCode) & "', '" & MainClass.AllowSingleQuote(mRemarks) & "','" & mSOStatus & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mItemSNo) & "'," & mMSPCostAdd & ",'" & MainClass.AllowSingleQuote(mAddItemDesc) & "','" & MainClass.AllowSingleQuote(mCustStoreLoc) & "'," & vbCrLf _
                        & " " & Val(mTotQty) & "," & Val(mItemDiscount) & "," & Val(mTODDiscount) & "," & Val(mOtherDiscount) & "," & Val(mPktQty) & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mSize) & "', '" & MainClass.AllowSingleQuote(mModelNo) & "', '" & MainClass.AllowSingleQuote(mDrawingNo) & "'," & vbCrLf _
                        & " '" & mGlassDescription & "', " & mActualHeight & ", " & mActualWidth & ", " & vbCrLf _
                        & " " & mChargeableHeight & ", " & mChargeableWidth & ", " & mArea & "," & vbCrLf _
                        & " " & mAreaRate & "" & vbCrLf _
                        & " ) "

            Else
                SqlStr = " UPDATE DSP_SALEORDER_DET SET SO_QTY=" & mTotQty & ", ITEM_PRICE=" & mPrice & "" & vbCrLf _
                        & " Where " & vbCrLf _
                        & " MKEY=" & Val(mMKey) & " AND ITEM_CODE='" & mItemCode & "'"

            End If

            PubDBCn.Execute(SqlStr)

            If UpdateSuppCustDet(mSuppCustCode, mPartNo, mItemCode, mPrice, 0, "S") = False Then GoTo UpdateDetail1

        End If
NextRow:

        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function UpdateSuppCustDet(ByRef xSuppCustCode As String, ByRef mPartNo As String, ByRef xItemCode As String, ByRef xRate As Double, ByRef xDisc As Double, ByRef xType As String) As Boolean

        On Error GoTo UpdateErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""
        SqlStr = " SELECT ITEM_CODE FROM FIN_SUPP_CUST_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  " & vbCrLf _
            & " AND SUPP_CUST_CODE='" & xSuppCustCode & "'  " & vbCrLf _
            & " AND ITEM_CODE='" & Trim(xItemCode) & "'  "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenKeyset, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            If xItemCode <> "" And xRate > 0 Then
                SqlStr = " INSERT INTO FIN_SUPP_CUST_DET ( " & vbCrLf & " COMPANY_CODE , SUPP_CUST_CODE, " & vbCrLf & " ITEM_CODE, ITEM_RATE, " & vbCrLf & " DISC_PER, TRN_TYPE,CUSTOMER_ITEM_NO) "
                SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " '" & RsCompany.Fields("COMPANY_CODE").Value & "','" & MainClass.AllowSingleQuote(xSuppCustCode) & "', " & vbCrLf & " '" & xItemCode & "'," & xRate & ", " & vbCrLf & " " & xDisc & ",'" & xType & "','" & mPartNo & "') "

                PubDBCn.Execute(SqlStr)
            End If
        End If

        UpdateSuppCustDet = True

        Exit Function
UpdateErrPart:
        MsgBox(Err.Description)
        UpdateSuppCustDet = False

        ''Resume			
    End Function
    Private Function AutoGenPONoSeq() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        mAutoGen = 1

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_SO)  " & vbCrLf _
            & " FROM DSP_SALEORDER_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_SO,LENGTH(AUTO_KEY_SO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = Mid(mMaxValue, 1, Len(mMaxValue) - 6)
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With

        AutoGenPONoSeq = mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        Dim strFilePath As String


        strFilePath = My.Application.Info.DirectoryPath
        If Not fOpenFile(strFilePath, "*.xls||*.xlsx", "Excel Data", CommonDialogOpen) Then
            GoTo NormalExit
        End If

        If Trim(strFilePath) = "" Then
            GoTo NormalExit
        End If

        Call PopulateFromXLSFile(strFilePath)
        CmdSave.Enabled = True

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""

        Dim mItemCode As String
        Dim mCustPONo As String
        Dim mCustPODate As String
        Dim mPlant As String
        Dim mPartNo As String
        Dim mItemName As String
        Dim mQty As Double
        Dim mDelDate As String
        Dim mRate As Double
        Dim mCustomerCode As String
        Dim mCustomerName As String
        Dim mMkey As String
        Dim mSONo As String
        Dim mSODate As String
        Dim mAmend As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTempItem As ADODB.Recordset = Nothing
        Dim mERPQty As Double

        'Dim FPath As String

        'Dim ErrorFile As System.IO.StreamWriter

        'FPath = mPubBarCodePath & "\POImportError2.txt"

        'If FILEExists(FPath) Then
        '    Kill(FPath)
        'End If

        'ErrorFile = My.Computer.FileSystem.OpenTextFileWriter(FPath, True)

        'PubDBCn.Errors.Clear()
        'PubDBCn.BeginTrans()


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
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        cntRow = 1

        Dim ultrow As UltraDataRow

        UltraDataSource1.Rows.Clear()

        'Me.UltraGrid1.DataSource = Me.UltraDataSource1

        For Each dtRow In dt.Rows
            mCustPONo = Trim(IIf(IsDBNull(dtRow.Item(0)), "", dtRow.Item(0)))      ''Trim(IIf(IsDBNull(RsFile.Fields(2).Value), "", RsFile.Fields(2).Value))
            mCustPODate = Trim(IIf(IsDBNull(dtRow.Item(2)), "", dtRow.Item(2)))
            mPlant = Trim(IIf(IsDBNull(dtRow.Item(4)), "", dtRow.Item(4)))

            mPartNo = Trim(IIf(IsDBNull(dtRow.Item(5)), "", dtRow.Item(5)))
            mItemName = Trim(IIf(IsDBNull(dtRow.Item(6)), "", dtRow.Item(6)))
            mQty = Trim(IIf(IsDBNull(dtRow.Item(7)), 0, dtRow.Item(7)))
            mRate = Trim(IIf(IsDBNull(dtRow.Item(8)), 0, dtRow.Item(8)))
            mDelDate = Trim(IIf(IsDBNull(dtRow.Item(9)), "", dtRow.Item(9)))

            mCustomerCode = ""
            mCustomerName = ""
            mItemCode = ""
            mItemName = ""

            If MainClass.ValidateWithMasterTable(mPlant, "ALIAS_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustomerCode = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustomerName = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(mPartNo, "CUSTOMER_PART_NO", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemName = MasterNo
            End If


            SqlStr = "SELECT MKEY, AUTO_KEY_SO, SO_DATE, AMEND_NO" & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR IH" & vbCrLf _
                    & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                    & " AND CUST_PO_NO ='" & mCustPONo & "' " & vbCrLf _
                    & " AND SUPP_CUST_CODE= '" & mCustomerCode & "' " & vbCrLf _
                    & " AND MKEY = (" & vbCrLf _
                    & " SELECT MAX(MKEY) " & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR " & vbCrLf _
                    & " WHERE Company_Code= IH.COMPANY_CODE" & vbCrLf _
                    & " AND CUST_PO_NO =IH.CUST_PO_NO" & vbCrLf _
                    & " AND SUPP_CUST_CODE= IH.SUPP_CUST_CODE AND SO_STATUS='O') "
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            mMkey = ""
            mSONo = ""
            mSODate = ""
            mAmend = ""
            mERPQty = 0

            If RsTemp.EOF = False Then
                mMkey = IIf(IsDBNull(RsTemp.Fields("MKEY").Value), "", RsTemp.Fields("MKEY").Value)
                mSONo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_SO").Value), "", RsTemp.Fields("AUTO_KEY_SO").Value)
                mSODate = IIf(IsDBNull(RsTemp.Fields("SO_DATE").Value), "", RsTemp.Fields("SO_DATE").Value)
                mAmend = IIf(IsDBNull(RsTemp.Fields("AMEND_NO").Value), "", RsTemp.Fields("AMEND_NO").Value)

                SqlStr = " SELECT PLANNED_QTY" & vbCrLf _
                       & " FROM DSP_DAILY_SCHLD_DET" & vbCrLf _
                       & " WHERE AUTO_KEY_DELV=" & mSONo & " " & vbCrLf _
                       & " AND ITEM_CODE ='" & mItemCode & "'" & vbCrLf _
                       & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(mDelDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                       & " AND BOOKTYPE='S'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempItem, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTempItem.EOF = False Then
                    mERPQty = IIf(IsDBNull(RsTempItem.Fields("PLANNED_QTY").Value), 0, RsTempItem.Fields("PLANNED_QTY").Value)
                End If

            End If

            ultrow = UltraDataSource1.Rows.Add()

            ultrow.SetCellValue(ColCustPONO - 1, mCustPONo)
            ultrow.SetCellValue(ColCustPODate - 1, mCustPODate)
            ultrow.SetCellValue(ColPlant - 1, mPlant)
            ultrow.SetCellValue(ColCustomerCode - 1, mCustomerCode)
            ultrow.SetCellValue(ColCustomerName - 1, mCustomerName)
            ultrow.SetCellValue(ColPartNo - 1, mPartNo)
            ultrow.SetCellValue(ColItemCode - 1, mItemCode)
            ultrow.SetCellValue(ColItemName - 1, mItemName)
            ultrow.SetCellValue(ColQty - 1, mQty)
            ultrow.SetCellValue(ColRate - 1, mRate)
            ultrow.SetCellValue(ColDeliveryDate - 1, mDelDate)
            ultrow.SetCellValue(ColMkey - 1, mMkey)
            ultrow.SetCellValue(ColSONo - 1, mSONo)
            ultrow.SetCellValue(ColSODate - 1, mSODate)
            ultrow.SetCellValue(ColAmend - 1, mAmend)
            ultrow.SetCellValue(ColERPQty - 1, mERPQty)

NextRecord:

        Next

        ''ErrorFile.WriteLine(mFileLineNo & " Part No blank :" & mCheckItemCode)
        'ErrorFile.Close()

        'If FILEExists(FPath) Then
        '    Process.Start("notepad.exe", FPath)            ''Process.Start("explorer.exe", FPath)
        'End If

        Exit Sub
ErrPart:
        'ErrorFile.Close()
        PubDBCn.RollbackTrans()

        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1


        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Public Sub frmSaleOrderUpload_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmSaleOrderUpload_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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

        CurrFormHeight = 7440
        CurrFormWidth = 11625


        CreateGridHeader()        ''Show1("L")
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1(pShowType As String)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivision As Double

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)

        SqlStr = "SELECT IH.CUST_PO_NO, IH.CUST_PO_DATE, ACM.ALIAS_NAME, IH.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME,  IMST.CUSTOMER_PART_NO, " & vbCrLf _
                & " ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, ID.SO_QTY, ID.ITEM_PRICE,  " & vbCrLf _
                & " IH.SO_DATE, IH.MKEY, IH.AUTO_KEY_SO, IH.SO_DATE, IH.AMEND_NO "

        SqlStr = SqlStr & vbCrLf _
                & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, FIN_SUPP_CUST_MST ACM, INV_ITEM_MST IMST" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " And IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE " & vbCrLf _
                & " And IH.Company_Code=IMST.Company_Code " & vbCrLf _
                & " And IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If pShowType = "L" Then
            SqlStr = SqlStr & vbCrLf & "And 1=2"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.MKEY"

        ClearGroupFromUltraGrid(UltraGrid1)
        ClearFilterFromUltraGrid(UltraGrid1)

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader()


        oledbAdapter.Dispose()
        oledbCnn.Close()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub CreateGridHeader()
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header
            Me.UltraGrid1.DataSource = Me.UltraDataSource1
            Me.UltraDataSource1.Band.Columns.Add("Customer PO No", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("Customer PO Date", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("Plant", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("Customer Code", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("Customer Name", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("Customer Part No", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("Item Code", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("Item Desc", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("Qty", GetType(Decimal))
            Me.UltraDataSource1.Band.Columns.Add("Rate", GetType(Decimal))
            Me.UltraDataSource1.Band.Columns.Add("Delivery Date", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("MKey", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("SO NO", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("SO Date", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("Amend No", GetType(String))
            Me.UltraDataSource1.Band.Columns.Add("ERP Qty", GetType(Decimal))

            'UltraGrid1.DisplayLayout.Bands(0).Columns(0).RowLayoutColumnInfo.PreferredLabelSize = New System.Drawing.Size(0, 40)
            'UltraGrid1.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True

            'UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Customer PO No"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Customer PO Date"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Plant"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Customer Code"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Customer Name"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Customer Part No"

            'UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Item Code"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Item Desc"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Qty"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Rate"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "Delivery Date"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "MKey"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(12).Header.Caption = "SO NO"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(13).Header.Caption = "SO Date"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(14).Header.Caption = "Amend No"

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColFlag - 1).Style = UltraWinGrid.ColumnStyle.CheckBox
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRate - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColERPQty - 1).Style = UltraWinGrid.ColumnStyle.Double

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRate - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColERPQty - 1).CellAppearance.TextHAlign = HAlign.Right


            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMkey - 1).Hidden = True


            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 50
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 150
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 160
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 150
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Width = 40
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Width = 80



            'MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "", "")
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub frmSaleOrderUpload_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub frmSaleOrderUpload_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)



        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))


        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1	
        '    MainClass.SetSpreadColor SprdOption, -1	
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetTotalItemQty(ByVal mCheckCustomerPONo As String, ByVal mCheckItemCode As String,
                                ByVal mCheckCustomerCode As String, ByVal pCheckDelDate As String) As Double
        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mQty As Double
        Dim mCustomerPONo As String
        Dim mRow As UltraGridRow
        Dim mMaxRow As Long
        Dim mSuppCustCode As String
        Dim mItemCode As String
        Dim mDelDate As String

        mMaxRow = UltraGrid1.Rows.Count
        GetTotalItemQty = 0

        With UltraGrid1
            For cntRow = 0 To mMaxRow - 1
                mRow = Me.UltraGrid1.Rows(cntRow)
                mSuppCustCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerCode - 1))
                mItemCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1))
                mCustomerPONo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustPONO - 1))

                mQty = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1))
                mDelDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColDeliveryDate - 1))

                If pCheckDelDate = "" Then
                    If mCheckCustomerPONo = mCustomerPONo And mSuppCustCode = mCheckCustomerCode And mItemCode = mCheckItemCode Then
                        GetTotalItemQty = GetTotalItemQty + mQty
                    End If
                Else
                    If mCheckCustomerPONo = mCustomerPONo And mSuppCustCode = mCheckCustomerCode And mItemCode = mCheckItemCode And mDelDate = pCheckDelDate Then
                        GetTotalItemQty = GetTotalItemQty + mQty
                    End If
                End If
NextRowNo:

            Next
        End With

        Exit Function
ErrPart:
        ''Resume
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
        'PubDBCn.RollbackTrans()
    End Function
    Private Function GetSaleInvoiceType(ByRef mItemCode As String, pSupplierCode As String) As String

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If mItemCode = "" Then Exit Function
        GetSaleInvoiceType = ""

        SqlStr = ""
        'SqlStr = " Select INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC,INVMST.ISSUE_UOM, " & vbCrLf _
        '    & " ID.ITEM_RATE,  ID.DISC_PER,ID.CUSTOMER_ITEM_NO , CUSTOMER_PART_NO,ITEM_COLOR," & vbCrLf _
        '    & " INVMST.CATEGORY_CODE, CMST.SALEINVTYPECODE, CMST.PURCHASEINVTYPECODE, INVMST.HSN_CODE, MAT_WIDTH , MAT_LEN" & vbCrLf _
        '    & " FROM FIN_SUPP_CUST_DET ID, INV_ITEM_MST INVMST , INV_GENERAL_MST CMST" & vbCrLf _
        '    & " WHERE ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
        '    & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
        '    & " AND INVMST.COMPANY_CODE=CMST.COMPANY_CODE AND INVMST.CATEGORY_CODE=CMST.GEN_CODE" & vbCrLf _
        '    & " AND ID.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf _
        '    & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND INVMST.ITEM_CODE='" & Trim(mItemCode) & "'"

        SqlStr = " Select CMST.SALEINVTYPECODE, CMST.PURCHASEINVTYPECODE" & vbCrLf _
            & " FROM INV_ITEM_MST INVMST , INV_GENERAL_MST CMST" & vbCrLf _
            & " WHERE INVMST.COMPANY_CODE=CMST.COMPANY_CODE AND INVMST.CATEGORY_CODE=CMST.GEN_CODE" & vbCrLf _
            & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND INVMST.ITEM_CODE='" & Trim(mItemCode) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            GetSaleInvoiceType = IIf(IsDBNull(RsMisc.Fields("SALEINVTYPECODE").Value), "", RsMisc.Fields("SALEINVTYPECODE").Value)
        End If


        Exit Function
ERR1:

        MsgBox(Err.Description)
    End Function
End Class
