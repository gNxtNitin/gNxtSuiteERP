Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility

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

Friend Class FrmInvoice_MiscGST
    Inherits System.Windows.Forms.Form
    Private Enum TerrorCorretion
        QualityLow
        QualityMedium
        QualityStandard
        QualityHigh
    End Enum

    Dim AccessCnn As New ADODB.Connection

    Dim RsSaleMain As ADODB.Recordset ''Recordset	
    Dim RsSaleExp As ADODB.Recordset ''Recordset	
    Dim RSSalesPrn As ADODB.Recordset ''Recordset	
    ''Private PvtDBCn As ADODB.Connection	

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String = ""
    Dim mCustomerCode As String
    Dim pRound As Double

    Private Const mBookType As String = "S"
    ''Private Const mBookSubType = "C"	

    Dim mBookSubType As String
    Private Const ConRowHeight As Short = 12

    Dim mAddUser As String
    Dim mAddDate As String
    Dim mModUser As String
    Dim mModDate As String

    'Private JB As JsonBag

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

        chkStockTrf.CheckState = System.Windows.Forms.CheckState.Unchecked
        If Trim(cboInvType.Text) = "" Then GoTo EventExitSub
        SqlStr = "SELECT SUPP_CUST_NAME,ISSTOCKTRF,INV_HEADING,FIN_INVTYPE_MST.IDENTIFICATION " & vbCrLf & " FROM FIN_SUPP_CUST_MST,FIN_INVTYPE_MST " & vbCrLf & " WHERE FIN_SUPP_CUST_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=FIN_INVTYPE_MST.COMPANY_CODE " & vbCrLf & " AND FIN_SUPP_CUST_MST.SUPP_CUST_CODE=FIN_INVTYPE_MST.ACCOUNTPOSTCODE " & vbCrLf & " AND FIN_INVTYPE_MST.NAME='" & MainClass.AllowSingleQuote((cboInvType.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtCreditAccount.Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
            chkStockTrf.CheckState = IIf(RsTemp.Fields("ISSTOCKTRF").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            lblInvHeading.Text = IIf(IsDbNull(RsTemp.Fields("INV_HEADING").Value), "", RsTemp.Fields("INV_HEADING").Value)

            'If ADDMode = True Then
            '    Call FillExpFromPartyExp()
            'End If
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkFOC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFOC.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdExp.Enabled = True
            txtBillNo.Enabled = False
            cboInvType.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim mDeleteRights As String
        Dim xDCNo As String

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

        If Trim(txtIRNNo.Text) <> "" Then
            MsgInformation("IRN No Made against this invoice So cann't be Deleted.")
            Exit Sub
        End If

        mDeleteRights = GetUserPermission("INVOICE_ADMIN", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)

        If mDeleteRights = "N" Then
            MsgBox("You Have Not Rights to Delete Invoice.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If CheckBillPayment(mCustomerCode, (txtBillNo.Text), "B", (txtBillDate.Text)) = True Then Exit Sub

        If Not RsSaleMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User choose Yes.	
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_HDR", (LblMKey.Text), RsSaleMain, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_EXP", (LblMKey.Text), RsSaleExp, "MKEY", "D") = False Then GoTo DelErrPart


                If InsertIntoDeleteTrn(PubDBCn, "FIN_INVOICE_HDR", "MKEY", (LblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & lblMkey.Text & "' AND BookType='" & mBookType & "' AND BookSubType='" & mBookSubType & "'")

                PubDBCn.Execute("Delete from FIN_INVOICE_EXP Where Mkey='" & lblMkey.Text & "'")
                PubDBCn.Execute("Delete from FIN_INVOICE_DET Where Mkey='" & lblMkey.Text & "'")
                PubDBCn.Execute("Delete from FIN_INVOICE_HDR Where Mkey='" & lblMkey.Text & "'")


                PubDBCn.CommitTrans()
                RsSaleMain.Requery() ''.Refresh	
                RsSaleExp.Requery() ''.Refresh	
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''	
        RsSaleMain.Requery() ''.Refresh	
        RsSaleExp.Requery() ''.Refresh	
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub cmdeInvoice_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdeInvoice.Click
        On Error GoTo ErrPart
        Dim mMKey As String
        Dim meInvoiceApp As String

        If ADDMode = True Or MODIFYMode = True Then
            Exit Sub
        End If

        meInvoiceApp = IIf(IsDbNull(RsCompany.Fields("E_INVOICE_APP").Value), "N", RsCompany.Fields("E_INVOICE_APP").Value)

        If lblInvoiceSeq.Text <> "4" Then Exit Sub
        If meInvoiceApp = "N" Then Exit Sub


        mMKey = Trim(lblMkey.Text)

        If Trim(txtIRNNo.Text) = "" Then
            If WebRequestGenerateIRN(mMKey) = False Then Exit Sub
        Else
            MsgInformation("IRN Already generated.")
            Exit Sub
        End If


        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub
    Public Function WebRequestGenerateIRN(ByRef pMKey As String) As Boolean

        'On Error GoTo ErrPart
        'Dim url As String

        'Dim mGSTIN As String
        'Dim mTaxSch As String
        'Dim mVersion As String
        'Dim mIrn As String
        'Dim mTran_Catg As String
        'Dim mTran_RegRev As String
        'Dim mTran_Typ As String
        'Dim mTran_EcmTrn As String
        'Dim mTran_EcmGstin As String
        'Dim mDoc_Typ As String
        'Dim mDOC_NO As String
        'Dim mDoc_Dt As String
        'Dim mBillFrom_Gstin As String
        'Dim mBillFrom_TrdNm As String
        'Dim mBillFrom_Bno As String
        'Dim mBillFrom_Bnm As String
        'Dim mBillFrom_Flno As String
        'Dim mBillFrom_Loc As String
        'Dim mBillFrom_Dst As String
        'Dim mBillFrom_Pin As String
        'Dim mBillFrom_Stcd As String
        'Dim mBillFrom_Ph As String
        'Dim mBillFrom_Em As String
        'Dim mBillTo_Gstin As String
        'Dim mBillTo_TrdNm As String
        'Dim mBillTo_Bno As String
        'Dim mBillTo_Bnm As String
        'Dim mBillTo_Flno As String
        'Dim mBillTo_Loc As String
        'Dim mBillTo_Dst As String
        'Dim mBillTo_Pin As String
        'Dim mBillTo_Stcd As String
        'Dim mBillTo_Ph As String
        'Dim mBillTo_Em As String
        'Dim mToPlace As String
        'Dim mItem_PrdNm As String
        'Dim mItem_PrdDesc As String
        'Dim mItem_HsnCd As String
        'Dim mItem_Barcde As String
        'Dim mItem_Qty As Double
        'Dim mItem_FreeQty As Double
        'Dim mItem_Unit As String
        'Dim mItem_UnitPrice As Double
        'Dim mItem_TotAmt As Double
        'Dim mItem_Discount As Double
        'Dim mItem_OthChrg As Double
        'Dim mItem_AssAmt As Double
        'Dim mItem_CgstRt As Double
        'Dim mItem_SgstRt As Double
        'Dim mItem_IgstRt As Double
        'Dim mItem_CesRt As Double
        'Dim mItem_CesNonAdval As Double
        'Dim mItem_StateCes As Double
        'Dim mItem_TotItemVal As Double
        'Dim mItem_Bch_Nm As String
        'Dim mItem_Bch_ExpDt As String
        'Dim mItem_Bch_WrDt As String
        'Dim mVal_AssVal As Double
        'Dim mVal_CgstVal As Double
        'Dim mVal_SgstVal As Double
        'Dim mVal_IgstVal As Double
        'Dim mVal_CesVal As Double
        'Dim mVal_StCesVal As Double
        'Dim mVal_CesNonAdVal As Double
        'Dim mVal_Disc As Double
        'Dim mVal_OthChrg As Double
        'Dim mVal_TotInvVal As Double
        'Dim mPay_Nam As String
        'Dim mPay_Mode As String
        'Dim mPay_PayTerm As String
        'Dim mPay_PayInstr As String
        'Dim mPay_CrDay As String
        'Dim mPay_BalAmt As Double
        'Dim mPay_PayDueDt As String
        'Dim mRef_InvRmk As String
        'Dim mRef_InvStDt As String
        'Dim mRef_InvEndDt As String
        ''Dim mTran_EcmGstin As String	
        'Dim mDoc_OrgInvNo As String
        'Dim mShipFrom_Gstin As String
        'Dim mShipFrom_TrdNm As String
        'Dim mShipFrom_Loc As String
        'Dim mShipFrom_Pin As String
        'Dim mShipFrom_Stcd As String
        'Dim mShipFrom_Bno As String
        'Dim mShipFrom_Bnm As String
        'Dim mShipFrom_Flno As String
        'Dim mShipFrom_Dst As String
        'Dim mShipFrom_Ph As String
        'Dim mShipFrom_Em As String
        'Dim mStateName As String
        'Dim mShipTo_Gstin As String
        'Dim mShipTo_TrdNm As String
        'Dim mShipTo_Loc As String
        'Dim mShipTo_Pin As String
        'Dim mShipTo_Stcd As String
        'Dim mShipTo_Bno As String
        'Dim mShipTo_Bnm As String
        'Dim mShipTo_Flno As String
        'Dim mShipTo_Dst As String
        'Dim mShipTo_Ph As String
        'Dim mShipTo_Em As String
        'Dim mPay_FinInsBr As String
        'Dim mPay_CrTrn As String
        'Dim mPay_DirDr As String
        'Dim mPay_AcctDet As String
        'Dim mRef_PrecInvNo As String
        'Dim mRef_PrecInvDt As String
        'Dim mRef_RecAdvRef As String
        'Dim mRef_TendRef As String
        'Dim mRef_ContrRef As String
        'Dim mRef_ExtRef As String
        'Dim mRef_ProjRef As String
        'Dim mRef_PORef As String
        'Dim mExp_ExpCat As String
        'Dim mExp_WthPay As String
        'Dim mExp_InvForCur As String
        'Dim mExp_ForCur As String
        'Dim mExp_CntCode As String
        'Dim mExp_ShipBNo As String
        'Dim mExp_ShipBDt As String
        'Dim mExp_Port As String
        'Dim mGetQRImg As String
        'Dim mGetSignedInvoice As String
        'Dim mCDKey As String
        'Dim mEInvUserName As String
        'Dim mEInvPassword As String
        'Dim mEFUserName As String
        'Dim mEFPassword As String

        'Dim pStateName As String
        'Dim pStateCode As String
        'Dim cntRow As Integer

        'Dim mSqlStr As String
        'Dim RsTemp As ADODB.Recordset = Nothing

        'Dim mBody As String
        'Dim mResponseId As String
        'Dim mResponseIdStr As String
        'Dim url1 As String
        'Dim WebRequestGen As String
        'Dim pStaus As String

        'Dim mIRNNo As String
        'Dim mIRNAckNo As String
        'Dim mIRNAckDate As String

        'Dim pError As String



        'Dim mSignedQRCode As String
        'Dim mSignedInvoice As String
        ''Dim pUserId As String	
        'Dim mBMPFileName As String


        'Dim pResponseText As String


        'If GeteInvoiceSetupContents(url, "G", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword) = False Then GoTo ErrPart

        ''    url = "http://EinvSandbox.webtel.in/v1.0/GenIRN"	
        ''    mCDKey = "1000687"	
        ''    mEInvUserName = "06AAACH0118F2Z9"       ''"06AAACW3775F013"	
        ''    mEInvPassword = "Admin!23"	
        ''    mEFUserName = "29AAACW3775F000"	
        ''    mEFPassword = "Admin!23.."	


        ''22/10/2021 Dim http As MSXML2.XMLHTTP60 '' MSXML.xmlhttp	
        ''22/10/2021 http = CreateObject("MSXML2.ServerXMLHTTP")

        'mGSTIN = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        'mTaxSch = "GST"
        'mVersion = "1.0"
        'mIrn = ""
        'If CDbl(lblInvoiceSeq.Text) = 6 Then
        '    mTran_Catg = "EXP"
        'Else
        '    mTran_Catg = "B2B"
        'End If

        'mTran_RegRev = "N"
        ''    If chkDespatchFrom.Value = vbUnchecked And chkShipTo.Value = vbChecked Then	
        'mTran_Typ = "REG"
        ''    ElseIf chkDespatchFrom.Value = vbUnchecked And chkShipTo.Value = vbUnchecked Then	
        ''         mTran_Typ = "SHP"	
        ''    ElseIf chkDespatchFrom.Value = vbChecked And chkShipTo.Value = vbChecked Then	
        ''         mTran_Typ = "DIS"	
        ''    ElseIf chkDespatchFrom.Value = vbChecked And chkShipTo.Value = vbUnchecked Then	
        ''         mTran_Typ = "CMB"	
        ''    End If	

        'mTran_EcmTrn = "N"
        'mTran_EcmGstin = ""

        'If CDbl(lblInvoiceSeq.Text) = 9 Then
        '    mTran_Catg = "DBN"
        'Else
        '    mDoc_Typ = "INV"
        'End If

        'mDOC_NO = Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text)
        'mDoc_Dt = VB6.Format(txtBillDate.Text, "DD/MM/YYYY") 'Format(txtBillDate.Text, "YYYY-MM-DD")	
        'mDoc_OrgInvNo = ""




        'mBillFrom_Gstin = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        'mBillFrom_TrdNm = IIf(IsDbNull(RsCompany.Fields("Company_Name").Value), "", RsCompany.Fields("Company_Name").Value)
        'mBillFrom_Bno = IIf(IsDbNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)
        'mBillFrom_Bnm = ""
        'mBillFrom_Flno = ""
        'mBillFrom_Loc = IIf(IsDbNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        'mBillFrom_Dst = ""
        'mBillFrom_Pin = IIf(IsDbNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)
        'pStateName = IIf(IsDbNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
        'pStateCode = GetStateCode(pStateName)
        'mBillFrom_Stcd = pStateCode
        'mBillFrom_Ph = ""
        'mBillFrom_Em = ""

        'mSqlStr = " SELECT SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote((txtCustomer.Text)) & "'"

        'MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        'If RsTemp.EOF = False Then
        '    mBillTo_TrdNm = Trim(txtCustomer.Text)
        '    mBillTo_Bno = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
        '    mBillTo_Bnm = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
        '    mBillTo_Flno = ""
        '    mBillTo_Loc = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
        '    mBillTo_Dst = ""
        '    mBillTo_Ph = ""
        '    mBillTo_Em = ""
        '    mToPlace = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

        '    If CDbl(lblInvoiceSeq.Text) = 6 Then
        '        mBillTo_Gstin = "URP"
        '        mBillTo_Pin = "999999"
        '        mBillTo_Stcd = CStr(99)
        '    Else
        '        mBillTo_Gstin = IIf(IsDbNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
        '        mBillTo_Pin = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
        '        mBillTo_Stcd = GetStateCode(mToPlace)
        '    End If


        'Else
        '    MsgInformation("Invalid Customer Name, Please Select Valid Customer Name.")
        '    WebRequestGenerateIRN = False
        '    http = Nothing
        '    Exit Function
        'End If


        'mShipFrom_Gstin = ""
        'mShipFrom_TrdNm = ""
        'mShipFrom_Loc = ""
        'mShipFrom_Pin = ""
        'mShipFrom_Stcd = ""
        'mShipFrom_Bno = ""
        'mShipFrom_Bnm = ""
        'mShipFrom_Flno = ""
        'mShipFrom_Dst = ""
        'mShipFrom_Ph = ""
        'mShipFrom_Em = ""


        'mShipTo_Gstin = ""
        'mShipTo_TrdNm = ""
        'mShipTo_Loc = ""
        'mShipTo_Pin = ""
        'mShipTo_Stcd = ""
        'mShipTo_Bno = ""
        'mShipTo_Bnm = ""
        'mShipTo_Flno = ""
        'mShipTo_Dst = ""
        'mShipTo_Ph = ""
        'mShipTo_Em = ""


        'mVal_AssVal = Val(lblTotTaxableAmt.Text)
        'mVal_CgstVal = Val(lblCGSTAmount.Text)
        'mVal_SgstVal = Val(lblSGSTAmount.Text)
        'mVal_IgstVal = Val(lblIGSTAmount.Text)
        'mVal_CesVal = 0
        'mVal_StCesVal = 0
        'mVal_CesNonAdVal = 0
        'mVal_Disc = 0
        'mVal_TotInvVal = Val(lblNetAmount.Text)
        'mVal_OthChrg = Val(lblTotExpAmt.Text) ''Format(mVal_TotInvVal - (mVal_AssVal + mVal_CgstVal + mVal_SgstVal + mVal_IgstVal), "0.00") '	


        'mPay_Nam = ""
        'mPay_Mode = ""
        'mPay_PayTerm = ""
        'mPay_PayInstr = ""
        'mPay_CrDay = ""
        'mPay_BalAmt = 0
        'mPay_PayDueDt = ""
        'mRef_InvRmk = ""
        'mRef_InvStDt = ""
        'mRef_InvEndDt = ""
        'mTran_EcmGstin = ""



        'mPay_FinInsBr = ""
        'mPay_CrTrn = ""
        'mPay_DirDr = ""
        'mPay_AcctDet = ""
        'mRef_PrecInvNo = ""
        'mRef_PrecInvDt = ""
        'mRef_RecAdvRef = ""
        'mRef_TendRef = ""
        'mRef_ContrRef = ""
        'mRef_ExtRef = ""
        'mRef_ProjRef = ""
        'mRef_PORef = ""
        'mExp_ExpCat = ""
        'mExp_WthPay = ""
        'mExp_InvForCur = ""
        'mExp_ForCur = ""
        'mExp_CntCode = ""
        'mExp_ShipBNo = ""
        'mExp_ShipBDt = ""
        'mExp_Port = ""
        'mGetQRImg = "0" ''0 for text , 1 for Image	
        'mGetSignedInvoice = "0" ''1 - Signed Json of Invoice will be return, 0 - will not return signed Invoice.	


        'http.Open("POST", url, False)

        'http.setRequestHeader("Content-Type", "application/json")



        'With JB
        '    .Clear()
        '    .IsArray_Renamed = False 'Actually the default after Clear.	

        '    With .AddNewObject("Push_Data_List")
        '        With .AddNewArray("Data") ''With .AddNewArray("Push_Data_List")	
        '            For cntRow = 1 To 1
        '                With .AddNewObject()
        '                    .Item("Gstin") = mGSTIN
        '                    .Item("Version") = mVersion
        '                    .Item("Irn") = mIrn
        '                    .Item("Tran_TaxSch") = mTaxSch 'check	


        '                    .Item("Tran_SupTyp") = mTran_Catg ''  .Item("Tran_Catg")	
        '                    .Item("Tran_RegRev") = mTran_RegRev
        '                    .Item("Tran_Typ") = mTran_Typ

        '                    .Item("Tran_EcmTrn") = mTran_EcmTrn 'check	
        '                    .Item("Tran_EcmGstin") = mTran_EcmGstin
        '                    .Item("Tran_IgstOnIntra") = "N" ''Y- indicates the supply is intra state but chargeable to IGST	
        '                    .Item("Doc_Typ") = mDoc_Typ
        '                    .Item("DOC_NO") = mDOC_NO
        '                    .Item("Doc_Dt") = mDoc_Dt
        '                    .Item("BillFrom_Gstin") = mBillFrom_Gstin
        '                    .Item("BillFrom_LglNm") = mBillFrom_TrdNm
        '                    .Item("BillFrom_TrdNm") = mBillFrom_TrdNm

        '                    .Item("BillFrom_Addr1") = mBillFrom_Bno
        '                    .Item("BillFrom_Addr2") = mBillFrom_Bnm
        '                    '                        .Item("BillFrom_Flno") = mBillFrom_Flno	
        '                    .Item("BillFrom_Loc") = mBillFrom_Loc
        '                    '                        .Item("BillFrom_Dst") = mBillFrom_Dst	
        '                    .Item("BillFrom_Pin") = mBillFrom_Pin
        '                    .Item("BillFrom_Stcd") = mBillFrom_Stcd
        '                    .Item("BillFrom_Ph") = mBillFrom_Ph
        '                    .Item("BillFrom_Em") = mBillFrom_Em

        '                    .Item("BillTo_Gstin") = mBillTo_Gstin
        '                    .Item("BillTo_LglNm") = mBillTo_TrdNm
        '                    .Item("BillTo_TrdNm") = mBillTo_TrdNm

        '                    .Item("BillTo_Pos") = mBillTo_Stcd

        '                    .Item("BillTo_Addr1") = mBillTo_Bno
        '                    .Item("BillTo_Addr2") = mBillTo_Bnm
        '                    '                        .Item("BillTo_Flno") = mBillTo_Flno	
        '                    .Item("BillTo_Loc") = mBillTo_Loc
        '                    '                        .Item("BillTo_Dst") = mBillTo_Dst	
        '                    .Item("BillTo_Pin") = mBillTo_Pin
        '                    .Item("BillTo_Stcd") = mBillTo_Stcd
        '                    .Item("BillTo_Ph") = mBillTo_Ph
        '                    .Item("BillTo_Em") = mBillTo_Em



        '                    mItem_PrdNm = Trim(txtRemarks.Text)
        '                    mItem_PrdDesc = Trim(txtRemarks.Text)

        '                    mItem_HsnCd = Trim(txtHSNCode.Text)

        '                    mItem_Barcde = ""

        '                    mItem_Qty = 1
        '                    mItem_FreeQty = 0

        '                    mItem_Unit = "NOS"

        '                    mItem_UnitPrice = Val(txtTotItemValue.Text)

        '                    mItem_TotAmt = Val(txtTotItemValue.Text)

        '                    mItem_AssAmt = Val(lblTotTaxableAmt.Text)

        '                    mItem_Discount = 0
        '                    mItem_OthChrg = mItem_AssAmt - mItem_TotAmt


        '                    '                     = ""	
        '                    '                    mItem_TotItemVal = ""	
        '                    '                    mItem_Bch_Nm = ""	
        '                    '                    mItem_Bch_ExpDt = ""	
        '                    '                    mItem_Bch_WrDt = ""	


        '                    mItem_SgstRt = Val(lblSGSTPer.Text)

        '                    mItem_CgstRt = Val(lblCGSTPer.Text)


        '                    mItem_IgstRt = Val(lblIGSTPer.Text)

        '                    mItem_CesRt = 0
        '                    mItem_CesNonAdval = 0
        '                    mItem_StateCes = 0
        '                    mItem_TotItemVal = (mItem_AssAmt * ((100 + mItem_SgstRt + mItem_CgstRt + mItem_IgstRt + mItem_CesRt + mItem_StateCes) * 0.01)) + mItem_CesNonAdval

        '                    mItem_TotItemVal = CDbl(VB6.Format(mItem_TotItemVal, "0.00"))


        '                    '                        .Item("Item_PrdNm") = mItem_PrdNm	
        '                    '                        .Item("Item_PrdDesc") = mItem_PrdDesc	
        '                    '                        .Item("Item_HsnCd") = mItem_HsnCd	
        '                    '                        .Item("Item_Barcde") = mItem_Barcde	
        '                    '                        .Item("Item_Qty") = mItem_Qty	
        '                    '                        .Item("Item_FreeQty") = mItem_FreeQty	
        '                    '                        .Item("Item_Unit") = mItem_Unit	
        '                    '                        .Item("Item_UnitPrice") = mItem_UnitPrice	
        '                    '                        .Item("Item_TotAmt") = mItem_TotAmt	
        '                    '                        .Item("Item_Discount") = mItem_Discount	
        '                    '                        .Item("Item_OthChrg") = mItem_OthChrg	
        '                    '                        .Item("Item_AssAmt") = mItem_AssAmt	
        '                    '                        .Item("Item_CgstRt") = mItem_CgstRt	
        '                    '                        .Item("Item_SgstRt") = mItem_SgstRt	
        '                    '                        .Item("Item_IgstRt") = mItem_IgstRt	
        '                    '                        .Item("Item_CesRt") = mItem_CesRt	
        '                    '                        .Item("Item_CesNonAdval") = mItem_CesNonAdval	
        '                    '                        .Item("Item_StateCes") = mItem_StateCes	
        '                    '                        .Item("Item_TotItemVal") = mItem_TotItemVal	
        '                    '	
        '                    '                        .Item("Item_Bch_Nm") = mItem_Bch_Nm	
        '                    '                        .Item("Item_Bch_ExpDt") = mItem_Bch_ExpDt	
        '                    '                        .Item("Item_Bch_WrDt") = mItem_Bch_WrDt	
        '                    '                        .Item("Val_AssVal") = mVal_AssVal	
        '                    '                        .Item("Val_CgstVal") = mVal_CgstVal	
        '                    '                        .Item("Val_SgstVal") = mVal_SgstVal	
        '                    '                        .Item("Val_IgstVal") = mVal_IgstVal	
        '                    '                        .Item("Val_CesVal") = mVal_CesVal	
        '                    '                        .Item("Val_StCesVal") = mVal_StCesVal	
        '                    '                        .Item("Val_CesNonAdVal") = mVal_CesNonAdVal	
        '                    '                        .Item("Val_Disc") = mVal_Disc	
        '                    '                        .Item("Val_OthChrg") = mVal_OthChrg	
        '                    '                        .Item("Val_TotInvVal") = mVal_TotInvVal	
        '                    '                        .Item("Pay_Nam") = mPay_Nam	
        '                    '                        .Item("Pay_Mode") = mPay_Mode	
        '                    '                        .Item("Pay_PayTerm") = mPay_PayTerm	
        '                    '                        .Item("Pay_PayInstr") = mPay_PayInstr	
        '                    '                        .Item("Pay_CrDay") = mPay_CrDay	
        '                    '                        .Item("Pay_BalAmt") = mPay_BalAmt	
        '                    '                        .Item("Pay_PayDueDt") = mPay_PayDueDt	
        '                    '                        .Item("Ref_InvRmk") = mRef_InvRmk	
        '                    '                        .Item("Ref_InvStDt") = mRef_InvStDt	
        '                    '                        .Item("Ref_InvEndDt") = mRef_InvEndDt	
        '                    '                        .Item("Doc_OrgInvNo") = mDoc_OrgInvNo	
        '                    '                        .Item("ShipFrom_Gstin") = mShipFrom_Gstin	
        '                    '                        .Item("ShipFrom_TrdNm") = mShipFrom_TrdNm	
        '                    '                        .Item("ShipFrom_Loc") = mShipFrom_Loc	
        '                    '                        .Item("ShipFrom_Pin") = mShipFrom_Pin	
        '                    '                        .Item("ShipFrom_Stcd") = mShipFrom_Stcd	
        '                    '                        .Item("ShipFrom_Bno") = mShipFrom_Bno	
        '                    '                        .Item("ShipFrom_Bnm") = mShipFrom_Bnm	
        '                    '                        .Item("ShipFrom_Flno") = mShipFrom_Flno	
        '                    '                        .Item("ShipFrom_Dst") = mShipFrom_Dst	
        '                    '                        .Item("ShipFrom_Ph") = mShipFrom_Ph	
        '                    '                        .Item("ShipFrom_Em") = mShipFrom_Em	
        '                    '                        .Item("ShipTo_Gstin") = mShipTo_Gstin	
        '                    '                        .Item("ShipTo_TrdNm") = mShipTo_TrdNm	
        '                    '                        .Item("ShipTo_Loc") = mShipTo_Loc	
        '                    '                        .Item("ShipTo_Pin") = mShipTo_Pin	
        '                    '                        .Item("ShipTo_Stcd") = mShipTo_Stcd	
        '                    '                        .Item("ShipTo_Bno") = mShipTo_Bno	
        '                    '                        .Item("ShipTo_Bnm") = mShipTo_Bnm	
        '                    '                        .Item("ShipTo_Flno") = mShipTo_Flno	
        '                    '                        .Item("ShipTo_Dst") = mShipTo_Dst	
        '                    '                        .Item("ShipTo_Ph") = mShipTo_Ph	
        '                    '                        .Item("ShipTo_Em") = mShipTo_Em	
        '                    '                        .Item("Pay_FinInsBr") = mPay_FinInsBr	
        '                    '                        .Item("Pay_CrTrn") = mPay_CrTrn	
        '                    '                        .Item("Pay_DirDr") = mPay_DirDr	
        '                    '                        .Item("Pay_AcctDet") = mPay_AcctDet	
        '                    '                        .Item("Ref_PrecInvNo") = mRef_PrecInvNo	
        '                    '                        .Item("Ref_PrecInvDt") = mRef_PrecInvDt	
        '                    '                        .Item("Ref_RecAdvRef") = mRef_RecAdvRef	
        '                    '                        .Item("Ref_TendRef") = mRef_TendRef	
        '                    '                        .Item("Ref_ContrRef") = mRef_ContrRef	
        '                    '                        .Item("Ref_ExtRef") = mRef_ExtRef	
        '                    '                        .Item("Ref_ProjRef") = mRef_ProjRef	
        '                    '                        .Item("Ref_PORef") = mRef_PORef	
        '                    '                        .Item("Exp_ExpCat") = mExp_ExpCat	
        '                    '                        .Item("Exp_WthPay") = mExp_WthPay	
        '                    '                        .Item("Exp_InvForCur") = mExp_InvForCur	
        '                    '                        .Item("Exp_ForCur") = mExp_ForCur	
        '                    '                        .Item("Exp_CntCode") = mExp_CntCode	
        '                    '                        .Item("Exp_ShipBNo") = mExp_ShipBNo	
        '                    '                        .Item("Exp_ShipBDt") = mExp_ShipBDt	
        '                    '                        .Item("Exp_Port") = mExp_Port	
        '                    '                        .Item("GetQRImg") = mGetQRImg	
        '                    '                        .Item("GetSignedInvoice") = mGetSignedInvoice	


        '                    '                        .Item("Item_PrdNm") = mItem_PrdNm  '' Not required	

        '                    .Item("Item_SlNo") = 1
        '                    .Item("Item_PrdDesc") = mItem_PrdDesc
        '                    .Item("Item_IsServc") = IIf(CDbl(lblInvoiceSeq.Text) = 4, "Y", "N")
        '                    .Item("Item_HsnCd") = mItem_HsnCd
        '                    .Item("Item_Barcde") = mItem_Barcde
        '                    .Item("Item_Qty") = mItem_Qty
        '                    .Item("Item_FreeQty") = mItem_FreeQty
        '                    .Item("Item_Unit") = mItem_Unit
        '                    .Item("Item_UnitPrice") = mItem_UnitPrice
        '                    .Item("Item_TotAmt") = mItem_TotAmt
        '                    .Item("Item_Discount") = mItem_Discount
        '                    .Item("Item_PreTaxVal") = mItem_TotAmt
        '                    .Item("Item_AssAmt") = mItem_AssAmt
        '                    .Item("Item_GstRt") = mItem_CgstRt + mItem_SgstRt + mItem_IgstRt

        '                    .Item("Item_IgstAmt") = Val(lblIGSTAmount.Text)
        '                    .Item("Item_CgstAmt") = Val(lblCGSTAmount.Text)
        '                    .Item("Item_SgstAmt") = Val(lblSGSTAmount.Text)
        '                    .Item("Item_CesRt") = 0 ''mItem_CesRt	
        '                    .Item("Item_CesAmt") = ""
        '                    .Item("Item_CesNonAdvlAmt") = mItem_CesNonAdval

        '                    .Item("Item_StateCesRt") = ""
        '                    .Item("Item_StateCesAmt") = ""
        '                    .Item("Item_StateCesNonAdvlAmt") = ""

        '                    .Item("Item_OthChrg") = mItem_OthChrg


        '                    .Item("Item_TotItemVal") = mItem_TotItemVal

        '                    .Item("Item_OrdLineRef") = ""
        '                    .Item("Item_OrgCntry") = ""
        '                    .Item("Item_PrdSlNo") = ""
        '                    .Item("Item_Attrib_Nm") = ""
        '                    .Item("Item_Attrib_Val") = ""


        '                    .Item("Item_Bch_Nm") = mItem_Bch_Nm
        '                    .Item("Item_Bch_ExpDt") = mItem_Bch_ExpDt
        '                    .Item("Item_Bch_WrDt") = mItem_Bch_WrDt
        '                    .Item("Val_AssVal") = mVal_AssVal
        '                    .Item("Val_CgstVal") = mVal_CgstVal
        '                    .Item("Val_SgstVal") = mVal_SgstVal
        '                    .Item("Val_IgstVal") = mVal_IgstVal
        '                    .Item("Val_CesVal") = mVal_CesVal
        '                    .Item("Val_StCesVal") = mVal_StCesVal
        '                    '                        .Item("Val_CesNonAdVal") = mVal_CesNonAdVal	
        '                    .Item("Val_Discount") = mVal_Disc
        '                    .Item("Val_OthChrg") = mVal_OthChrg
        '                    .Item("Val_RndOffAmt") = VB6.Format(Val(lblRO.Text), "0.00")

        '                    .Item("Val_TotInvVal") = mVal_TotInvVal
        '                    .Item("Val_TotInvValFc") = ""

        '                    .Item("Pay_Nm") = mPay_Nam
        '                    .Item("Pay_AcctDet") = mPay_AcctDet
        '                    .Item("Pay_Mode") = mPay_Mode
        '                    .Item("Pay_FinInsBr") = mPay_FinInsBr

        '                    .Item("Pay_PayTerm") = mPay_PayTerm
        '                    .Item("Pay_PayInstr") = mPay_PayInstr
        '                    .Item("Pay_CrTrn") = mPay_CrTrn
        '                    .Item("Pay_DirDr") = mPay_DirDr
        '                    .Item("Pay_CrDay") = mPay_CrDay
        '                    .Item("Pay_PaidAmt") = ""
        '                    .Item("Pay_BalAmt") = mPay_BalAmt
        '                    .Item("Pay_PaymtDue") = mPay_PayDueDt
        '                    .Item("Ref_InvRmk") = mRef_InvRmk
        '                    .Item("Ref_InvStDt") = mRef_InvStDt
        '                    .Item("Ref_InvEndDt") = mRef_InvEndDt
        '                    .Item("Doc_OrgInvNo") = mDoc_OrgInvNo


        '                    .Item("ShipFrom_Gstin") = mShipFrom_Gstin
        '                    '                        .Item("ShipFrom_TrdNm") = mShipFrom_TrdNm	
        '                    .Item("ShipFrom_Nm") = mShipFrom_TrdNm
        '                    ''	
        '                    .Item("ShipFrom_Addr1") = mShipFrom_Bno
        '                    .Item("ShipFrom_Addr2") = mShipFrom_Bnm
        '                    .Item("ShipFrom_Loc") = mShipFrom_Loc
        '                    .Item("ShipFrom_Pin") = mShipFrom_Pin
        '                    .Item("ShipFrom_Stcd") = mShipFrom_Stcd
        '                    '                        .Item("ShipFrom_Bno") = mShipFrom_Bno	
        '                    '                        .Item("ShipFrom_Bnm") = mShipFrom_Bnm	
        '                    '                        .Item("ShipFrom_Flno") = mShipFrom_Flno	
        '                    '                        .Item("ShipFrom_Dst") = mShipFrom_Dst	
        '                    '                        .Item("ShipFrom_Ph") = mShipFrom_Ph	
        '                    '                        .Item("ShipFrom_Em") = mShipFrom_Em	
        '                    .Item("ShipTo_Gstin") = mShipTo_Gstin
        '                    .Item("ShipTo_LglNm") = mShipTo_TrdNm
        '                    .Item("ShipTo_TrdNm") = mShipTo_TrdNm
        '                    .Item("ShipTo_Addr1") = mShipTo_Bno
        '                    .Item("ShipTo_Addr2") = mShipTo_Loc
        '                    .Item("ShipTo_Loc") = mShipTo_Loc
        '                    .Item("ShipTo_Pin") = mShipTo_Pin
        '                    .Item("ShipTo_Stcd") = mShipTo_Stcd
        '                    '                        .Item("ShipTo_Bno") = mShipTo_Bno	
        '                    '                        .Item("ShipTo_Bnm") = mShipTo_Bnm	
        '                    '                        .Item("ShipTo_Flno") = mShipTo_Flno	
        '                    '                        .Item("ShipTo_Dst") = mShipTo_Dst	
        '                    '                        .Item("ShipTo_Ph") = mShipTo_Ph	
        '                    '                        .Item("ShipTo_Em") = mShipTo_Em	

        '                    .Item("Ref_PrecDoc_InvNo") = mRef_PrecInvNo
        '                    .Item("Ref_PrecDoc_InvDt") = mRef_PrecInvDt
        '                    .Item("Ref_PrecDoc_OthRefNo") = ""

        '                    .Item("Ref_Contr_RecAdvRefr") = mRef_RecAdvRef
        '                    .Item("Ref_Contr_RecAdvDt") = ""

        '                    .Item("Ref_Contr_TendRefr") = mRef_TendRef
        '                    .Item("Ref_Contr_ContrRefr") = mRef_ContrRef
        '                    '                        .Item("Ref_ExtRef") = mRef_ExtRef	
        '                    .Item("Ref_Contr_ExtRefr") = mRef_ProjRef
        '                    .Item("Ref_Contr_ProjRefr") = ""

        '                    .Item("Ref_Contr_PORefr") = mRef_PORef
        '                    .Item("Ref_Contr_PORefDt") = ""

        '                    .Item("AddlDoc_Url") = ""
        '                    .Item("AddlDoc_Docs") = ""
        '                    .Item("AddlDoc_Info") = ""


        '                    '                        .Item("Exp_ExpCat") = mExp_ExpCat	
        '                    '                        .Item("Exp_WthPay") = mExp_WthPay	
        '                    '                        .Item("Exp_InvForCur") = mExp_InvForCur	


        '                    .Item("Exp_ForCur") = mExp_ForCur
        '                    .Item("Exp_CntCode") = mExp_CntCode
        '                    .Item("Exp_ShipBNo") = mExp_ShipBNo
        '                    .Item("Exp_ShipBDt") = mExp_ShipBDt
        '                    .Item("Exp_Port") = mExp_Port
        '                    '                        .Item("GetQRImg") = mGetQRImg       ''29/09/2020	
        '                    '                        .Item("GetSignedInvoice") = mGetSignedInvoice ''29/09/2020	


        '                    .Item("CDKey") = mCDKey
        '                    .Item("EInvUserName") = mEInvUserName
        '                    .Item("EInvPassword") = mEInvPassword
        '                    .Item("EFUserName") = mEFUserName
        '                    .Item("EFPassword") = mEFPassword

        '                End With
        '            Next
        '        End With
        '    End With
        '    mBody = .JSON
        'End With

        '' shipToGSTIN String GSTIN of  Ship-To shipToTradeName String Trade Name of  Ship-To dispatchFromGSTIN String GSTIN of Dispatch-From dispatchFromTradeName String Trade Name of Dispatch-From IsBillFromShipFromSame String Required, 0 for Different BillFrom and ShipFrom, 1 for Same BillFrom and ShipFrom IsBillToShipToSame String Required, 0 for Different BillTo and ShipTo, 1 for Same BillTo and ShipTo IsGSTINSEZ	

        'http.Send(mBody)

        'pResponseText = http.responseText
        ''    pResponseText = Replace(pResponseText, "\", "")	
        'pResponseText = Replace(pResponseText, "[", "")
        'pResponseText = Replace(pResponseText, "]", "")
        ''    pResponseText = Mid(pResponseText, 2, Len(pResponseText) - 2)	

        'Dim JsonTest As Object
        'Dim SB As New cStringBuilder

        'Dim c As Object
        'Dim I As Integer

        'JsonTest = JSON.parse(pResponseText)

        'pStaus = JsonTest.Item("Status")


        'If pStaus = "1" Then
        '    mIRNNo = JsonTest.Item("Irn")
        '    mIRNAckNo = JsonTest.Item("AckNo")
        '    mIRNAckDate = JsonTest.Item("AckDate") 'JsonTest.Item("elements").Item(mResponseId).Item("ewayBillDate")	
        '    mSignedQRCode = JsonTest.Item("SignedQRCode")
        '    mSignedInvoice = JsonTest.Item("SignedInvoice")

        '    txtIRNNo.Text = Trim(mIRNNo)
        '    txteInvAckNo.Text = Trim(mIRNAckNo)
        '    txteInvAckDate.Text = VB6.Format(mIRNAckDate, "DD/MM/YYYY HH:MM")


        '    PubDBCn.Errors.Clear()
        '    PubDBCn.BeginTrans()

        '    SqlStr = ""

        '    SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf & " IRN_NO ='" & Trim(txtIRNNo.Text) & "'," & vbCrLf & " IRN_ACK_NO ='" & Trim(txteInvAckNo.Text) & "'," & vbCrLf & " IRN_ACK_DATE =TO_DATE('" & VB6.Format(txteInvAckDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY ='" & pMKey & "'"

        '    PubDBCn.Execute(SqlStr)
        '    PubDBCn.CommitTrans()

        '    mBMPFileName = mPubBarCodePath & "\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".bmp"
        ' If GererateQRCodeImage(mBMPFileName, mSignedQRCode) = False Then GoTo ErrPart

        '    If UpdateQRCODE(CDbl(lblMkey.Text), mBMPFileName) = False Then GoTo ErrPart

        'End If

        'If pStaus = "0" Then
        '    pError = JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")	
        '    MsgInformation(pError)
        '    WebRequestGenerateIRN = False
        '    http = Nothing
        '    Exit Function
        'End If

        '        WebRequestGenerateIRN = True
        '        http = Nothing
        '        Exit Function
        'ErrPart:
        '        '    Resume	
        '        WebRequestGenerateIRN = False
        '        http = Nothing
        '        MsgBox(Err.Description)
        '        PubDBCn.RollbackTrans()
    End Function

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Modified")
            Exit Sub
        End If

        If PubUserID = "G0416" Then
        Else

            If Trim(txtIRNNo.Text) <> "" Then
                MsgInformation("IRN No Made against this invoice So cann't be modified.")
                Exit Sub
            End If
        End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdExp.Enabled = True

            '        txtBillNo.Enabled = False	
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function MakeSQL() As String
        On Error GoTo ERR1

        ''SELECT CLAUSE...	

        MakeSQL = " SELECT " & vbCrLf & " IH.*, GMST.*, CMST.SUPP_CUST_NAME "

        ''FROM CLAUSE...	
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BMST, GEN_COMPANY_MST GMST"


        ''WHERE CLAUSE...	
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=BMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=BMST.LOCATION_ID" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY='" & LblMKey.Text & "' "

        ''ORDER CLAUSE...	

        '    MakeSQL = mSqlStr & vbCrLf & "ORDER BY ID.SUBROWNO"	

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        On Error GoTo ErrPart
        Dim mInvoicePrint As Boolean
        Dim CntCount As Integer
        Dim mInvoicePrintType As String
        Dim mExtraRemarks As String
        Dim mPrintOption As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Invoice Cann't be Print.")
            Exit Sub
        End If

        frmPrintInvCopy.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).Enabled = False

        frmPrintInvCopy.chkPrintOption(1).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(1).Enabled = False

        frmPrintInvCopy.chkPrintOption(4).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(3).CheckState = System.Windows.Forms.CheckState.Unchecked

        frmPrintInvCopy.chkPrintOption(5).Text = "Duplicate for Supplier"
        '    frmPrintInvCopy.chkPrintOption(5).Enabled = False	

        frmPrintInvCopy.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If

        For CntCount = 0 To 5
            If frmPrintInvCopy.chkPrintOption(CntCount).CheckState = System.Windows.Forms.CheckState.Checked Then
                If CntCount = 0 And lblInvoiceSeq.Text = "4" Then
                    If Trim(txtIRNNo.Text) = "" Then
                        If MsgQuestion("You have not generated IRN. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                            Exit Sub
                        End If
                        '                    MsgInformation "Please generate the IRN first."	
                        '                    Exit Sub	
                    End If
                End If
                mInvoicePrintType = UCase(frmPrintInvCopy.chkPrintOption(CntCount).Text)
                Call ReportOnInvoice(Crystal.DestinationConstants.crptToWindow, mInvoicePrintType)
            End If
        Next

        frmPrintInvCopy.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ErrPart:
        frmPrintInvoice.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click

        On Error GoTo ErrPart
        Dim mInvoicePrint As Boolean
        Dim mAnnexPrint As String
        'Dim mEXPAnnexPrint As String	
        'Dim mMaxRow As Long	
        'Dim mSubsidiaryChallanPrint As String	
        'Dim mSC_All As String	
        'Dim mSC_F4No As String	
        Dim CntCount As Integer
        Dim mInvoicePrintType As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPRINTED As String = ""
        'Dim mExtraRemarks As String	
        Dim mPrintOption As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mPrintOption = "I"
        SqlStr = "SELECT PRINTED FROM FIN_INVOICE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND Mkey ='" & MainClass.AllowSingleQuote((lblMkey.Text)) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPRINTED = IIf(IsDbNull(RsTemp.Fields("PRINTED").Value), "N", RsTemp.Fields("PRINTED").Value)
            mPRINTED = IIf(PubSuperUser = "S", "N", mPRINTED)
        End If

        frmPrintInvCopy.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).Enabled = False

        frmPrintInvCopy.chkPrintOption(1).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(1).Enabled = False

        frmPrintInvCopy.chkPrintOption(5).Text = "Duplicate for Supplier"

        frmPrintInvCopy.chkPrintOption(0).CheckState = IIf(mPRINTED = "Y", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
        frmPrintInvCopy.chkPrintOption(0).Enabled = IIf(mPRINTED = "Y", False, True)
        frmPrintInvCopy.ShowDialog()

        If G_PrintLedg = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        For CntCount = 0 To 5
            If frmPrintInvCopy.chkPrintOption(CntCount).CheckState = System.Windows.Forms.CheckState.Checked Then
                If CntCount = 0 And lblInvoiceSeq.Text = "4" Then
                    If Trim(txtIRNNo.Text) = "" Then
                        If MsgQuestion("You have not generated IRN. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                            Exit Sub
                        End If
                        '                    MsgInformation "Please generate the IRN first."	
                        '                    Exit Sub	
                    End If
                End If
                mInvoicePrintType = UCase(frmPrintInvCopy.chkPrintOption(CntCount).Text)
                Call ReportOnInvoice(Crystal.DestinationConstants.crptToPrinter, mInvoicePrintType)
                '            Call ReportOnSales(crptToPrinter, mInvoicePrintType, "N", mPrintOption)	
            End If
        Next

        If frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = "UPDATE FIN_INVOICE_HDR SET  PRINTED= 'Y', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND  Mkey ='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"

            PubDBCn.Execute(SqlStr)
        End If

        frmPrintInvCopy.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ErrPart:
        frmPrintInvoice.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        If InStr(1, pBARCODEFORMAT1, Trim(mCustomerCode), CompareMethod.Text) >= 1 Then
            Call PrintBarcode1(pBarCodeString, LblMKey.Text, "N", True)
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
            & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST CBMST, GEN_COMPANY_MST GMST, TEMP_BARCODE_PRINT BP "


        ''WHERE CLAUSE...							
        mSqlStr = mSqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.MKEY=BP.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=CBMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CBMST.SUPP_CUST_CODE AND IH.BILL_TO_LOC_ID=CBMST.LOCATION_ID" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY='" & LblMKey.Text & "'" & vbCrLf _
            & " AND BP.USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        ''ORDER CLAUSE...							

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY BP.PRINT_SEQ,BP.PRINT_INVOICE_TYPE"

        SelectQryForPrint = mSqlStr
        Exit Function
ErrPart:
        If mUpdateStart = True Then
            PubDBCn.RollbackTrans()
        End If
        SelectQryForPrint = ""
    End Function
    Private Sub ReportOnInvoice(ByRef Mode As Crystal.DestinationConstants, ByRef mInvoicePrintType As String)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNO As String
        'Dim mPDF As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""
        mTitle = ""
        mSubTitle = ""


        'SqlStr = MakeSQL
        Call SelectQryForPrint(SqlStr)
        If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "INV_HEADING", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
            mTitle = MasterNo
        End If

        ''cboInvType.Text	

        If lblInvoiceSeq.Text = "4" Then
            mTitle = IIf(Trim(mTitle) = "", "Tax Invoice", mTitle)
            mSubTitle = "[See Rule 1 under Tax Invoice, Credit and Debit Note Rules]"
            mRptFileName = "INVOICE_MISC_GST.rpt"
        ElseIf lblInvoiceSeq.Text = "0" Then
            mTitle = IIf(Trim(mTitle) = "", "Bill of Supply", mTitle)
            mSubTitle = "[Under section 31(3)(c) of CGST Act, 2017 read with Rule 49 of CGST Rules, 2017]"
            mRptFileName = "INVOICE_MISC_GST.rpt"   ''"INVOICE_BOS.rpt"
        Else
            mTitle = "Input Service Distributor Invoice"
            mSubTitle = "[See Rule 9 under Tax Invoice, Credit and Debit Note Rules]"
            mRptFileName = "INVOICE_ISD_GST.rpt"
        End If

        Dim mPDFPrint As Boolean = False
        If frmPrintInvCopy.optShow(0).Checked = True Then     ''mPDF
            mPDFPrint = False
        Else
            mPDFPrint = True
        End If

        Call ShowExcisePDFReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, True, mInvoicePrintType, mPDFPrint)

        'Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, mInvoicePrintType)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowExcisePDFReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean, ByRef mInvoicePrintType As String, ByVal mPDF As Boolean)

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

        Dim CrReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument  ' Report Name 
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions

        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions


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
            mSO = IIf(IsDBNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), -1, RsTemp.Fields("OUR_AUTO_KEY_SO").Value)

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
                'If mShipToSameParty = "Y" Then
                mShipToCode = mCustomerCode
                mShipLocation = Trim(txtBillTo.Text)
                'Else
                '    mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
                '    mShipLocation = Trim(TxtShipTo.Text)
                'End If
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
        AssignCRpt11Formulas(CrReport, "mServiceName", "'" & Trim(txtServProvided.Text) & "'")



        'If Val(lblInvoiceSeq.Text) = 6 Then
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

        If mPDF = True Then
            Dim pOutPutFileName As String = ""
            mBillNoStr = Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text)
            mBillNoStr = Replace(mBillNoStr, "/", "_")
            mBillNoStr = Replace(mBillNoStr, "\", "_")

            fPath = mPubBarCodePath & "\TaxInvoice_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"
            pOutPutFileName = mPubBarCodePath & "\TaxInvoice_DS_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"

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
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef mInvoicePrintType As String)

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim SqlStrSub As String
        Dim mDutyInword As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
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

        Dim mCompanyDetail As String
        Dim mCompanyeMail As String
        Dim mCompanyWebSite As String
        Dim mShipToState As String
        Dim mShipToStateCode As String
        Dim mStateName As String
        Dim mStateCode As String
        Dim mWithInState As String = ""
        Dim mWithInCountry As String
        Dim mPlaceofSupply As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        '    mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(lblNetAmount.text) = 0, 0, lblNetAmount.text)))	
        '	
        '    If chkCancelled.Value = vbChecked Then	
        '        MainClass.AssignCRptFormulas Report1, "AmountInWord=""Rs. Zero"""	
        '        MainClass.AssignCRptFormulas Report1, "NetAmount=""0.00"""	
        '    Else	
        '        MainClass.AssignCRptFormulas Report1, "AmountInWord=""" & mAmountInword & """"	
        '        MainClass.AssignCRptFormulas Report1, "NetAmount=""" & lblNetAmount.text & """"	
        '    End If	

        MainClass.AssignCRptFormulas(Report1, "InvoicePrintType=""" & mInvoicePrintType & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyCity=""" & IIf(IsDbNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & """")


        mStateName = ""
        mStateCode = ""

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LOCATION_ID='" & txtBillTo.Text & "'") = True Then
            mStateName = MasterNo
            mStateCode = GetStateCode(mStateName)
        End If

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LOCATION_ID='" & txtBillTo.Text & "'") = True Then
            mWithInState = MasterNo
        End If

        If mWithInState = "N" Then
            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LOCATION_ID='" & txtBillTo.Text & "'") = True Then
                mWithInCountry = MasterNo
            End If
        End If

        mPlaceofSupply = VB6.Format(mStateCode, "00") & "-" & mStateName ''IIf(mWithInState = "Y", "INTRA STATE", IIf(mWithInCountry = "Y", "INTER STATE", "EXPORT"))	

        SqlStr = " SELECT NETVALUE, NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT,INV_PREP_TIME, " & vbCrLf & " SHIPPED_TO_PARTY_CODE, REMOVAL_TIME, OUR_AUTO_KEY_SO, SHIPPED_TO_SAMEPARTY" & vbCrLf & " FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mNetAmount = IIf(IsDbNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
            mNetDuty = IIf(IsDbNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDbNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDbNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)

            '        mSO = IIf(IsNull(RsTemp!OUR_AUTO_KEY_SO), "", RsTemp!OUR_AUTO_KEY_SO)	
        End If

        mJurisdiction = "All Disputes Subject to " & IIf(IsDbNull(RsCompany.Fields("JURISDICTION").Value), "", RsCompany.Fields("JURISDICTION").Value) & " Jurisdiction."

        MainClass.AssignCRptFormulas(Report1, "COMPANYTINNo=""" & IIf(IsDbNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDbNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")

        mCompanyeMail = IIf(IsDbNull(RsCompany.Fields("COMPANY_MAILID").Value), "", "e-mail : " & RsCompany.Fields("COMPANY_MAILID").Value)
        mCompanyWebSite = IIf(IsDbNull(RsCompany.Fields("WEBSITE").Value), "", "WebSite : " & RsCompany.Fields("WEBSITE").Value)
        mCompanyDetail = mCompanyeMail & ", " & mCompanyWebSite
        MainClass.AssignCRptFormulas(Report1, "COMPANYDETAIL=""" & mCompanyDetail & """")

        MainClass.AssignCRptFormulas(Report1, "PrepTime=""" & mPrepTime & """")
        MainClass.AssignCRptFormulas(Report1, "RemovalTime=""" & mRemovalTime & """")
        '    MainClass.AssignCRptFormulas Report1, "JWRemarks=""" & mJWRemarks & """"	
        MainClass.AssignCRptFormulas(Report1, "Jurisdiction=""" & mJurisdiction & """")

        MainClass.AssignCRptFormulas(Report1, "mStateName=""" & mStateName & """")
        MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")
        MainClass.AssignCRptFormulas(Report1, "mPlaceofSupply=""" & mPlaceofSupply & """")

        MainClass.AssignCRptFormulas(Report1, "CGSTPer=""" & VB6.Format(lblCGSTPer.Text, "0.00") & """")
        MainClass.AssignCRptFormulas(Report1, "SGSTPer=""" & VB6.Format(lblSGSTPer.Text, "0.00") & """")
        MainClass.AssignCRptFormulas(Report1, "IGSTPer=""" & VB6.Format(lblIGSTPer.Text, "0.00") & """")
        MainClass.AssignCRptFormulas(Report1, "HSNCode=""" & Trim(txtHSNCode.Text) & """")

        mPayTerms = ""

        MainClass.AssignCRptFormulas(Report1, "ServiceTaxNo=""" & IIf(IsDbNull(RsCompany.Fields("SERV_REGN_NO").Value), "-", RsCompany.Fields("SERV_REGN_NO").Value) & """")
        Report1.ReportFileName = PubReportFolderPath & mRptFileName

        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False


        mAmountInword = MainClass.RupeesConversion(mNetAmount)
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

        SqlStrSub = " SELECT FIN_INVOICE_EXP.MKEY, FIN_INVOICE_EXP.SUBROWNO, FIN_INVOICE_EXP.EXPPERCENT, FIN_INVOICE_EXP.AMOUNT, FIN_INTERFACE_MST.COMPANY_CODE, FIN_INTERFACE_MST.NAME" & vbCrLf & " FROM FIN_INVOICE_EXP, FIN_INVOICE_HDR, FIN_INTERFACE_MST " & vbCrLf & " WHERE FIN_INVOICE_EXP.MKEY = FIN_INVOICE_HDR.MKEY AND FIN_INVOICE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf & " AND FIN_INVOICE_EXP.MKEY='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'" & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

        If PubGSTApplicable = True Then
            SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

        Report1.SubreportToChange = "PurExp" ''Report1.GetNthSubreportName(0)	
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStrSub
        '    MainClass.AssignCRptFormulas Report1, "JWSTRemarks=""" & mJWSTRemarks & """"	
        '    Report1.SubreportToChange = ""	

        '    If Trim(txtIRNNo.Text) <> "" Then	
        'SqlStrSub = "SELECT * FROM INVOICE_QRCODE WHERE MKEY='" & lblMkey.Text & "'"
        'Report1.SubreportToChange = "QRcode" ''Report1.GetNthSubreportName(1)	
        ''Report1.Connect = AccessRptConn
        'Report1.SQLQuery = SqlStrSub
        'Report1.SubreportToChange = ""
        '    End If	

        ''"InvMiscExp"	

        '    SqlStrSub = " SELECT MKEY, SUBROWNO, EXPPERCENT, AMOUNT, COMPANY_CODE, NAME" & vbCrLf _	
        ''                & " FROM FIN_INVOICE_EXP, FIN_INTERFACE_MST " & vbCrLf _	
        ''                & " WHERE FIN_INVOICE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _	
        ''                & " AND FIN_INVOICE_EXP.MKEY='" & MainClass.AllowSingleQuote(lblMkey.text) & "'" & vbCrLf _	
        ''                & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""	
        '	
        '    If PubGSTApplicable = True Then	
        '        SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"	
        '    Else	
        '        SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='N'"	
        '    End If	
        '	
        '    SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"	
        '	
        '    Report1.SubreportToChange = Report1.GetNthSubreportName(0)	
        '    Report1.Connect = STRRptConn	
        '    Report1.SQLQuery = SqlStrSub	
        '	
        '    Report1.SubreportToChange = ""	

        Report1.Action = 1
        Report1.Reset()
        Report1.ReportFileName = ""

        Exit Sub
ErrPart:
        Resume
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Sub

    Private Sub cmdQRCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdQRCode.Click
        On Error GoTo ErrPart
        '        Dim url As String

        '        Dim mGSTIN As String
        '        Dim mIrn As String

        '        Dim mGetQRImg As String
        '        Dim mGetSignedInvoice As String
        '        Dim mCDKey As String
        '        Dim mEInvUserName As String
        '        Dim mEInvPassword As String
        '        Dim mEFUserName As String
        '        Dim mEFPassword As String

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
        '        Dim mBMPFileName As String


        '        Dim pResponseText As String

        '        If Trim(txtIRNNo.Text) = "" Then Exit Sub

        '        If GeteInvoiceSetupContents(url, "I", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword) = False Then GoTo ErrPart

        '        '    url = "http://EinvSandbox.webtel.in/v1.0/GetEInvoiceByIRN"	
        '        '    mCDKey = "1000687"	
        '        '    mEInvUserName = "06AAACW3775F013"	
        '        '    mEInvPassword = "Admin!23"	
        '        '    mEFUserName = "29AAACW3775F000"	
        '        '    mEFPassword = "Admin!23.."	


        '        '22/10/2021 Dim http As MSXML2.XMLHTTP60 '' MSXML.xmlhttp	
        '        '22/10/2021 http = CreateObject("MSXML2.ServerXMLHTTP")

        '        mGSTIN = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        '        mIRNNo = Trim(txtIRNNo.Text)

        '        mGetQRImg = "0" ''0 for text , 1 for Image	
        '        mGetSignedInvoice = "0" ''1 - Signed Json of Invoice will be return, 0 - will not return signed Invoice.	

        '        http.Open("POST", url, False)

        '        http.setRequestHeader("Content-Type", "application/json")

        '        With JB
        '            .Clear()
        '            .IsArray_Renamed = False 'Actually the default after Clear.	

        '            With .AddNewObject("Push_Data_List")
        '                With .AddNewArray("Data") ''With .AddNewArray("Push_Data_List")	
        '                    With .AddNewObject()
        '                        .Item("Irn") = mIRNNo
        '                        .Item("GSTIN") = mGSTIN
        '                        .Item("CDKey") = mCDKey
        '                        .Item("EInvUserName") = mEInvUserName
        '                        .Item("EInvPassword") = mEInvPassword
        '                        .Item("EFUserName") = mEFUserName
        '                        .Item("EFPassword") = mEFPassword

        '                    End With
        '                End With
        '            End With

        '            mBody = .JSON
        '        End With

        '        http.Send(mBody)

        '        pResponseText = http.responseText
        '        '    pResponseText = Replace(pResponseText, "\", "")	
        '        pResponseText = Replace(pResponseText, "[", "")
        '        pResponseText = Replace(pResponseText, "]", "")
        '        '    pResponseText = Mid(pResponseText, 2, Len(pResponseText) - 2)	

        '        Dim JsonTest As Object

        '        JsonTest = JSON.parse(pResponseText)

        '        pStaus = JsonTest.Item("Status")


        '        If pStaus = "1" Then
        '            mSignedQRCode = JsonTest.Item("SignedQRCode")
        '            mSignedInvoice = JsonTest.Item("SignedInvoice")

        '            mBMPFileName = mPubBarCodePath & "\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".bmp"
        'If GererateQRCodeImage(mBMPFileName, mSignedQRCode) = False Then GoTo ErrPart

        '            If UpdateQRCODE(CDbl(lblMkey.Text), mBMPFileName) = False Then GoTo ErrPart
        '        End If

        '        If pStaus = "0" Then
        '            pError = JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")	
        '            MsgInformation(pError)
        '            http = Nothing
        '            Exit Sub
        '        End If

        '        http = Nothing
        '        '    Set httpGen = Nothing	
        '        Exit Sub
        'ErrPart:
        '        '    Resume	
        '        http = Nothing
        '        MsgBox(Err.Description)
        '    End Sub
        '    Private Function UpdateQRCODE(ByRef nMkey As Double, ByRef pFilePath As String) As Boolean

        '        On Error GoTo ErrPart
        '        Dim SqlStr As String=""
        '        Dim RS As New ADODB.Recordset
        '        Dim mInventoryGroupCode As Integer
        '        Dim mstream As ADODB.Stream

        '        If pFilePath = "" Or Trim(txtIRNNo.Text) = "" Then UpdateQRCODE = True : Exit Function

        '        If AccessCnn.State <> ADODB.ObjectStateEnum.adStateOpen Then
        '            AccessCnn.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBConDataPath & "ERPIMAGE.mdb;Persist Security Info=False")
        '            '        AccessCnn.Open "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & App.path & "\DATA\ERPIMAGE.mdb;Persist Security Info=False"	
        '        End If
        '        AccessCnn.BeginTrans()

        '        SqlStr = "Select * From INVOICE_QRCODE " 'WHERE ITEMCODE='" & pcls6.AllowSingleQuote(txtItemCode.Text) & "'"	
        '        MainClass.UOpenRecordSet(SqlStr, AccessCnn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)
        '        RS.Find("MKEY='" & nMkey & "'")
        '        Dim ss As String '' PropertyBag
        '        If RS.EOF Then
        '            RS.AddNew()
        '            RS.Fields("mKey").Value = nMkey
        '            RS.Fields("COMPANY_CODE").Value = RsCompany.Fields("COMPANY_CODE").Value
        '            RS.Fields("IRN_NO").Value = txtIRNNo.Text
        '            RS.Fields("BFILE_TYPE").Value = "JPG"

        '            '                GetPhoto IIf(CDlg1.FileName = "", "Photo", App.path & "\Picture\MIPLITEM.BMP"), Rs, "ItemPicture", "ItemPicSize"	
        '            GetPhoto(IIf(pFilePath = "", "Photo", pFilePath), RS, "INV_QRCODE", "ItemPicSize")
        '            RS.Update()
        '        Else
        '            'GetPhoto IIf(CDlg1.FileName = "", "Photo", App.Path & "\Picture\MIPLITEM.BMP"), Rs, "ItemPicture", "ItemPicSize"	
        '            'SaveImageToDB Me.Picture1.Picture, Rs, "pic"	

        '            'Set ss = New PropertyBag	
        '            'ss.WriteProperty "MyImage", pPic	
        '            'Rs.Fields("ItemPicture").AppendChunk ss.Contents	
        '            ''Rs.Update	
        '            'Set ss = Nothing	


        '            mstream = New ADODB.Stream
        '            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
        '            mstream.Open()

        '            mstream.LoadFromFile(pFilePath) ''App.path & "\Picture\MIPLITEM.BMP"	
        '            RS.Fields("INV_QRCODE").Value = mstream.Read

        '            RS.Update()
        '        End If
        '        '       AccessCnn.Execute SqlStr	
        '        AccessCnn.CommitTrans()
        'UpdateQRCODE = True
        Exit Sub
ErrPart:
        'Resume	
        'UpdateQRCODE = False
        MsgInformation(Err.Description)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Call CalcTots()

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtBillNo_Validating(txtBillNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
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

    Private Sub cmpPrinteInvoice_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmpPrinteInvoice.Click
        'On Error GoTo ErrPart
        '        Dim url As String

        '        Dim mGSTIN As String
        '        Dim mIrn As String

        '        Dim mGetQRImg As String
        '        Dim mGetSignedInvoice As String
        '        Dim mCDKey As String
        '        Dim mEInvUserName As String
        '        Dim mEInvPassword As String
        '        Dim mEFUserName As String
        '        Dim mEFPassword As String

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
        '        Dim mBMPFileName As String
        '        Dim mFilePath As String

        '        Dim pResponseText As String

        '        If Trim(txtIRNNo.Text) = "" Then Exit Sub

        '        If GeteInvoiceSetupContents(url, "P", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword) = False Then GoTo ErrPart

        '        '    url = "http://EinvSandbox.webtel.in/v1.0/GetEInvoiceByIRN"	
        '        '    mCDKey = "1000687"	
        '        '    mEInvUserName = "06AAACW3775F013"	
        '        '    mEInvPassword = "Admin!23"	
        '        '    mEFUserName = "29AAACW3775F000"	
        '        '    mEFPassword = "Admin!23.."	


        '        '22/10/2021  Dim http As MSXML2.XMLHTTP60 '' MSXML.xmlhttp	
        '        '22/10/2021  http = CreateObject("MSXML2.ServerXMLHTTP")

        '        mGSTIN = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        '        mIRNNo = Trim(txtIRNNo.Text)

        '        '    mGetQRImg = "0"      ''0 for text , 1 for Image	
        '        '    mGetSignedInvoice = "0"  ''1 - Signed Json of Invoice will be return, 0 - will not return signed Invoice.	

        '        http.Open("POST", url, False)

        '        http.setRequestHeader("Content-Type", "application/json")

        '        With JB
        '            .Clear()
        '            .IsArray_Renamed = False 'Actually the default after Clear.	

        '            .Item("Irn") = mIRNNo
        '            .Item("GSTIN") = mGSTIN
        '            .Item("CDKey") = mCDKey
        '            .Item("EInvUserName") = mEInvUserName
        '            .Item("EInvPassword") = mEInvPassword
        '            .Item("EFUserName") = mEFUserName
        '            .Item("EFPassword") = mEFPassword
        '            mBody = .JSON
        '        End With

        '        http.Send(mBody)

        '        pResponseText = http.responseText
        '        '    pResponseText = Replace(pResponseText, "\", "")	
        '        pResponseText = Replace(pResponseText, "[", "")
        '        pResponseText = Replace(pResponseText, "]", "")
        '        '    pResponseText = Mid(pResponseText, 2, Len(pResponseText) - 2)	

        '        Dim JsonTest As Object

        '        JsonTest = JSON.parse(pResponseText)

        '        pStaus = JsonTest.Item("Status")


        '        If pStaus = "1" Then

        '            mFilePath = JsonTest.Item("File") ''http.responseText	

        '            If mFilePath <> "" Then
        '                'ShellExecute(Me.Handle.ToInt32, "open", mFilePath, vbNullString, vbNullString, SW_SHOWNORMAL)
        '            End If

        '        End If

        '        If pStaus = "0" Then
        '            pError = JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")	
        '            MsgInformation(pError)
        '            http = Nothing
        '            Exit Sub
        '        End If

        '        http = Nothing
        '        '    Set httpGen = Nothing	
        '        Exit Sub
        'ErrPart:
        '        '    Resume	
        '        http = Nothing
        '        MsgBox(Err.Description)

    End Sub

    Private Sub FrmInvoice_MiscGST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub SprdExp_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdExp.ButtonClicked
        If FormActive = True Then
            SprdExp.Col = ColExpAmt
            SprdExp.Row = eventArgs.row
            If Val(SprdExp.Text) <> 0 Then
                Call CalcTots()
            End If
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.Row

            .Col = 1
            If Trim(.Text) = "" Then Exit Sub
            cboInvType.Text = Trim(.Text)

            .Col = 2
            txtBillNoPrefix.Text = .Text

            .Col = 3
            If Val(.Text) = 1509 And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 And RsCompany.Fields("FYEAR").Value = 2023 Then
                txtBillNo.Text = VB6.Format(.Text, "0")     ''VB6.Format(.Text)
            Else
                txtBillNo.Text = VB6.Format(.Text, ConBillFormat)     ''VB6.Format(.Text)
            End If


            .Col = 4
            txtBillNoSuffix.Text = .Text

            .Col = 6
            txtBillDate.Text = VB6.Format(.Text, "DD/MM/YYYY")

            txtBillNo_Validating(txtBillNo, New System.ComponentModel.CancelEventArgs(False))
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Sub txtAuth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuth.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAuth_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAuth.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAuth.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAuthDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Public Sub txtBillNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mBillNo As String
        If Trim(txtBillNo.Text) = "" Then GoTo EventExitSub

        'txtBillNo.Text = Val(txtBillNo.Text)
        If Val(txtBillNo.Text) = 1509 And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 And RsCompany.Fields("FYEAR").Value = 2023 Then
            txtBillNo.Text = VB6.Format(Val(txtBillNo.Text), "0")
        Else
            txtBillNo.Text = VB6.Format(Val(txtBillNo.Text), ConBillFormat)
        End If


        If MODIFYMode = True And RsSaleMain.EOF = False Then xMkey = RsSaleMain.Fields("mKey").Value
        mBillNo = Trim(Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & Trim(txtBillNoSuffix.Text))

        SqlStr = " SELECT * FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "' " & vbCrLf & " AND BillNo='" & MainClass.AllowSingleQuote(mBillNo) & "' " & vbCrLf & " AND BookCode=" & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' "

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
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim I As Short
        Dim nMkey As String
        Dim mTRNType As String
        Dim mAutoKeyNo As String
        Dim mBillNoSeq As Integer
        Dim mBillNo As String
        Dim mSuppCustCode As String
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


        Dim mTOTEXPAMT As Double
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mLSTCST As String
        Dim mWITHFORM As String
        Dim mFOC As String
        Dim mPRINTED As String
        Dim mCancelled As String
        Dim mIsRegdNo As String
        Dim mSTType As String
        Dim mStockTrf As String

        Dim mBookCode As Integer
        Dim mStartingNo As Integer
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
        Dim mDivisionCode As Double
        Dim pGSTableAmount As Double
        Dim mWOGST As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        mFormRecdCode = -1
        mFormDueCode = -1


        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mTRNType = MasterNo
        Else
            mTRNType = CStr(-1)
            MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            mAccountCode = CStr(-1)
            MsgBox("Credit Account Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mAUTHSIGN = MainClass.AllowSingleQuote(txtAuth.Text)
        mAUTHDATE = VB6.Format(txtAuthDate.Text, "DD-MMM-YYYY")
        mFREIGHTCHARGES = IIf(OptFreight(0).Checked = True, "To Pay", "Paid")
        mEXEMPT_NOTIF_NO = ""
        mBookCode = CInt(LblBookCode.Text)

        If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
            mBookSubType = MasterNo
        Else
            mBookSubType = CStr(-1)
        End If

        mSALETAXCODE = -1
        mItemValue = Val(txtTotItemValue.Text)
        mTOTSTAMT = 0
        mTOTCHARGES = Val(lblTotCharges.Text)
        mTotEDAmount = 0
        mTotEDUAmount = 0
        mTotEDUPercent = 0

        mTotServiceAmount = 0
        mTotServicePercent = 0

        mTOTEXPAMT = Val(lblTotExpAmt.Text)
        mNETVALUE = Val(lblNetAmount.Text)

        mSTPERCENT = Val(lblSTPercentage.Text)
        mTOTFREIGHT = Val(lblTotFreight.Text)
        mEDPERCENT = 0
        mTOTTAXABLEAMOUNT = Val(lblTotTaxableAmt.Text)

        mRO = Val(lblRO.Text)
        mTotDiscount = Val(lblDiscount.Text)
        mSURAmount = Val(lblSurcharge.Text)
        mMSC = Val(lblMSC.Text)
        mTCSAMOUNT = Val(lblTCS.Text)
        mTCSPER = Val(lblTCSPercentage.Text)

        mTotQty = Val(lblTotQty.Text)
        mLSTCST = ""
        mWITHFORM = ""
        mFOC = IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPRINTED = "N"
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsRegdNo = "N"
        mREJECTION = "N"
        mD3 = "N"
        mStockTrf = IIf(chkStockTrf.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mPackMat = IIf(chkPackmat.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        ''chkChallanMade.CheckState = IIf(.Fields("CHALLAN_MADE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        mWOGST = IIf(chkWoGST.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If mPackMat = "Y" Then
            mChallanMade = "N"
        Else
            mChallanMade = "Y"
        End If

        mSTType = "0"

        '    If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "INVOICENOSTART", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then	
        '        mStartingNo = MasterNo	
        '    Else	
        '        mStartingNo = 1	
        '    End If	

        mStartingNo = 1
        If Trim(txtBillNo.Text) = "" Then
            mBillNoSeq = CInt(AutoGenSeqBillNo(mBookType, mBookSubType, mStartingNo, mDivisionCode))
        Else
            mBillNoSeq = Val(txtBillNo.Text)
        End If

        If Val(mBillNoSeq) = 1509 And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 And RsCompany.Fields("FYEAR").Value = 2023 Then
            txtBillNo.Text = Val(CStr(mBillNoSeq))
        Else
            txtBillNo.Text = VB6.Format(Val(CStr(mBillNoSeq)), ConBillFormat)       ''Val(CStr(mBillNoSeq))
        End If

        If CheckValidBillDate(mBillNoSeq) = False Then GoTo ErrPart

        If Val(mBillNoSeq) = 1509 And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 And RsCompany.Fields("FYEAR").Value = 2023 Then
            mBillNo = Trim(Trim(txtBillNoPrefix.Text) & VB6.Format(Val(CStr(mBillNoSeq)), "0") & Trim(txtBillNoSuffix.Text)) ''Trim(Trim(txtBillNoPrefix.Text) & Val(CStr(mBillNoSeq)) & Trim(txtBillNoSuffix.Text))
        Else
            mBillNo = Trim(Trim(txtBillNoPrefix.Text) & VB6.Format(Val(CStr(mBillNoSeq)), ConBillFormat) & Trim(txtBillNoSuffix.Text)) ''Trim(Trim(txtBillNoPrefix.Text) & Val(CStr(mBillNoSeq)) & Trim(txtBillNoSuffix.Text))
        End If


        mAutoKeyNo = mBillNoSeq & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_HDR", (LblMKey.Text), RsSaleMain, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_EXP", (LblMKey.Text), RsSaleExp, "MKEY", "M") = False Then GoTo ErrPart
        End If

        SqlStr = ""
        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("FIN_INVOICE_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & VB.Right(RsCompany.Fields("FYEAR").Value, 2) & mCurRowNo
            lblMkey.Text = nMkey
            SqlStr = "INSERT INTO FIN_INVOICE_HDR (" & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _
                & " ROWNO, TRNTYPE, BILLNOPREFIX, " & vbCrLf _
                & " AUTO_KEY_INVOICE, BILLNOSEQ, BILLNOSUFFIX, BILLNO, " & vbCrLf _
                & " INVOICE_DATE, INV_PREP_DATE, INV_PREP_TIME, " & vbCrLf _
                & " AUTO_KEY_DESP, DCDATE, CUST_PO_NO, CUST_PO_DATE, " & vbCrLf _
                & " AMEND_NO, AMEND_DATE, AMEND_WEF_FROM, REMOVAL_DATE, " & vbCrLf _
                & " REMOVAL_TIME, SUPP_CUST_CODE, ACCOUNTCODE, ST_38_NO, " & vbCrLf _
                & " DUEDAYSFROM, DUEDAYSTO, AUTHSIGN, AUTHDATE, " & vbCrLf _
                & " GRNO, GRDATE, DESPATCHMODE, DOCSTHROUGH, " & vbCrLf _
                & " VEHICLENO, CARRIERS, FREIGHTCHARGES, " & vbCrLf _
                & " TARIFFHEADING, EXEMPT_NOTIF_NO, " & vbCrLf _
                & " BOOKCODE,BOOKTYPE, BOOKSUBTYPE, SALETAXCODE, " & vbCrLf _
                & " REMARKS, ITEMDESC, ITEMVALUE, SUPPITEMTOT," & vbCrLf _
                & " TOTSTAMT, TOTCHARGES, TOTEDAMOUNT, " & vbCrLf _
                & " TOTEXPAMT, NETVALUE, TOTQTY, " & vbCrLf _
                & " STFORMCODE, STFORMNAME, STFORMNO, STFORMDATE, " & vbCrLf _
                & " STDUEFORMCODE, STDUEFORMNAME, STDUEFORMNO, STDUEFORMDATE,  " & vbCrLf _
                & " STTYPE, IsRegdNo,LSTCST, WITHFORM, FOC, PRINTED," & vbCrLf _
                & " CANCELLED, NARRATION,  " & vbCrLf _
                & " STPERCENT, TOTFREIGHT, EDPERCENT, TOTTAXABLEAMOUNT, "

            SqlStr = SqlStr & vbCrLf & " TOTSURCHARGEAMT, TOTDISCAMOUNT, TOTMSCAMOUNT, TotRO,REJECTION,AGTD3, " & vbCrLf _
                & " PACK_MAT_FLAG, CHALLAN_MADE,PRDDate, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE,ISSTOCKTRF,TCSPER, TCSAMOUNT,DNCNNO,DNCNDATE," & vbCrLf _
                & " TOTEDUPERCENT,TOTEDUAMOUNT,TOTSERVICEPERCENT,TOTSERVICEAMOUNT,SERV_PROV, " & vbCrLf _
                & " UPDATE_FROM,TOTSHECPERCENT, TOTSHECAMOUNT,DIV_CODE," & vbCrLf _
                & " NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT, SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE, SAC_CODE,INVOICESEQTYPE," & vbCrLf _
                & " NET_CGST_PER, NET_SGST_PER, NET_IGST_PER,BILL_TO_LOC_ID,WITHOUT_GST,VENDOR_CODE)"


            SqlStr = SqlStr & vbCrLf _
                & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & mCurRowNo & "," & Val(mTRNType) & ", '" & MainClass.AllowSingleQuote(txtBillNoPrefix.Text) & "', " & vbCrLf _
                & " " & mAutoKeyNo & "," & mBillNoSeq & ", '" & MainClass.AllowSingleQuote(txtBillNoSuffix.Text) & "', '" & MainClass.AllowSingleQuote(mBillNo) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & TxtBillTm.Text & "','HH24:MI')," & vbCrLf _
                & " " & Val(nMkey) & ", TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtPONo.Text) & "', TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '','','',TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " TO_DATE('" & TxtBillTm.Text & "','HH24:MI'),'" & mSuppCustCode & "','" & mAccountCode & "','', " & vbCrLf _
                & " " & Val(txtCreditDays(0).Text) & ", " & Val(txtCreditDays(1).Text) & ", '" & mAUTHSIGN & "', TO_DATE('" & VB6.Format(mAUTHDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & txtDCNo.Text & "', TO_DATE('" & VB6.Format(txtDCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtMode.Text) & "', '" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "', '" & mFREIGHTCHARGES & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtTariff.Text) & "', '" & mEXEMPT_NOTIF_NO & "', " & vbCrLf _
                & " '" & mBookCode & "', '" & mBookType & "', '" & mBookSubType & "', " & mSALETAXCODE & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & MainClass.AllowSingleQuote(txtItemType.Text) & "',  " & mItemValue & ", 0," & vbCrLf _
                & " " & mTOTSTAMT & ", " & mTOTCHARGES & ", " & mTotEDAmount & ", " & vbCrLf & " " & mTOTEXPAMT & ", " & mNETVALUE & ", " & mTotQty & ", " & vbCrLf _
                & " " & mFormRecdCode & ", '','', '', " & vbCrLf & " " & mFormDueCode & ", '','','', " & vbCrLf _
                & " '" & mSTType & "','" & mIsRegdNo & "', '" & mLSTCST & "', " & vbCrLf _
                & " '" & mWITHFORM & "', '" & mFOC & "', '" & mPRINTED & "', " & vbCrLf _
                & " '" & mCancelled & "', '" & MainClass.AllowSingleQuote(txtNarration.Text) & "',  "

            SqlStr = SqlStr & vbCrLf _
                & "" & mSTPERCENT & "," & mTOTFREIGHT & "," & mEDPERCENT & "," & mTOTTAXABLEAMOUNT & "," & vbCrLf _
                & "" & mSURAmount & "," & mTotDiscount & "," & mMSC & "," & mRO & ",'" & mREJECTION & "','" & mD3 & "', " & vbCrLf _
                & "'" & mPackMat & "','" & mChallanMade & "',TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & mStockTrf & "'," & vbCrLf _
                & " " & mTCSPER & "," & mTCSAMOUNT & "," & vbCrLf _
                & " ''," & vbCrLf & " '', " & vbCrLf _
                & " " & mTotEDUPercent & ", " & mTotEDUAmount & "," & vbCrLf _
                & " " & mTotServicePercent & "," & mTotServiceAmount & ",'" & MainClass.AllowSingleQuote(txtServProvided.Text) & "','N'," & vbCrLf _
                & " 0, 0, " & mDivisionCode & "," & vbCrLf & " " & Val(lblCGSTAmount.Text) & ", " & Val(lblSGSTAmount.Text) & ", " & Val(lblIGSTAmount.Text) & ", " & vbCrLf _
                & " 'N',''," & vbCrLf & " '" & Trim(txtHSNCode.Text) & "', " & Val(lblInvoiceSeq.Text) & ", " & vbCrLf _
                & " " & Val(lblCGSTPer.Text) & ", " & Val(lblSGSTPer.Text) & ", " & Val(lblIGSTPer.Text) & ",'" & txtBillTo.Text & "','" & mWOGST & "', '" & txtVendorCode.Text & "')"


        ElseIf MODIFYMode = True Then
            PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & lblMkey.Text & "' AND BookType='" & UCase(mBookType) & "' AND BOOKCODE='" & LblBookCode.Text & "'")

            SqlStr = ""
            SqlStr = "UPDATE FIN_INVOICE_HDR SET TRNTYPE=" & Val(mTRNType) & "," & vbCrLf & " BILLNOPREFIX = '" & MainClass.AllowSingleQuote(txtBillNoPrefix.Text) & "'," & vbCrLf & " BILLNOSEQ= " & mBillNoSeq & ", " & vbCrLf & " AUTO_KEY_INVOICE= " & mAutoKeyNo & ", " & vbCrLf & " BILLNOSUFFIX= '" & MainClass.AllowSingleQuote(txtBillNoSuffix.Text) & "'," & vbCrLf & " BILLNO= '" & MainClass.AllowSingleQuote(mBillNo) & "'," & vbCrLf & " INVOICE_DATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " PRDDate= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " INV_PREP_DATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " INV_PREP_TIME= TO_DATE('" & TxtBillTm.Text & "','HH24:MI')," & vbCrLf & " AUTO_KEY_DESP= " & Val(LblMKey.Text) & " ," & vbCrLf & " DCDATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " CUST_PO_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "', " & vbCrLf & " CUST_PO_DATE= TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " AMEND_NO= ''," & vbCrLf & " AMEND_DATE= ''," & vbCrLf & " AMEND_WEF_FROM= ''," & vbCrLf & " REMOVAL_DATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " REMOVAL_TIME=TO_DATE('" & TxtBillTm.Text & "','HH24:MI')," & vbCrLf & " SUPP_CUST_CODE= '" & mSuppCustCode & "'," & vbCrLf & " ACCOUNTCODE= '" & mAccountCode & "'," & vbCrLf _
                & " ST_38_NO= '', "

            SqlStr = SqlStr & vbCrLf _
                & " DUEDAYSFROM= " & Val(txtCreditDays(0).Text) & "," & vbCrLf _
                & " DUEDAYSTO= " & Val(txtCreditDays(1).Text) & ", " & vbCrLf _
                & " AUTHSIGN= '" & mAUTHSIGN & "'," & vbCrLf & " AUTHDATE=  TO_DATE('" & VB6.Format(mAUTHDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " GRNO= '" & txtDCNo.Text & "', " & vbCrLf _
                & " GRDATE= TO_DATE('" & VB6.Format(txtDCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " DESPATCHMODE= '" & MainClass.AllowSingleQuote(txtMode.Text) & "', " & vbCrLf & " DOCSTHROUGH= '" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "'," & vbCrLf & " VEHICLENO= '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', " & vbCrLf & " CARRIERS=  '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "'," & vbCrLf & " FREIGHTCHARGES= '" & mFREIGHTCHARGES & "', " & vbCrLf & " TARIFFHEADING= '" & MainClass.AllowSingleQuote(txtTariff.Text) & "'," & vbCrLf & " EXEMPT_NOTIF_NO= '" & MainClass.AllowSingleQuote(mEXEMPT_NOTIF_NO) & "',"


            SqlStr = SqlStr & vbCrLf & " BOOKCODE= " & mBookCode & "," & vbCrLf & " BOOKTYPE= '" & mBookType & "'," & vbCrLf & " BOOKSUBTYPE= '" & mBookSubType & "'," & vbCrLf & " SALETAXCODE= " & mSALETAXCODE & "," & vbCrLf & " REMARKS= '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf & " ITEMDESC= '" & MainClass.AllowSingleQuote(txtItemType.Text) & "'," & vbCrLf & " ITEMVALUE= " & mItemValue & ", SUPPITEMTOT=0, " & vbCrLf & " TOTSTAMT= " & mTOTSTAMT & "," & vbCrLf & " TOTCHARGES= " & mTOTCHARGES & "," & vbCrLf & " TOTEDAMOUNT= " & mTotEDAmount & "," & vbCrLf & " TOTEXPAMT= " & mTOTEXPAMT & "," & vbCrLf & " NETVALUE= " & mNETVALUE & "," & vbCrLf & " TOTQTY= " & mTotQty & "," & vbCrLf & " STTYPE= '" & mSTType & "'," & vbCrLf & " REJECTION='" & mREJECTION & "'," & vbCrLf & " STFORMCODE= " & mFormRecdCode & "," & vbCrLf & " STFORMNAME= ''," & vbCrLf & " STFORMNO= ''," & vbCrLf & " STFORMDATE= ''," & vbCrLf & " STDUEFORMCODE= " & mFormDueCode & "," & vbCrLf & " STDUEFORMNAME= ''," & vbCrLf & " STDUEFORMNO= ''," & vbCrLf & " STDUEFORMDATE= '',"


            SqlStr = SqlStr & vbCrLf & " STPERCENT=" & mSTPERCENT & "," & vbCrLf & " TOTFREIGHT=" & mTOTFREIGHT & "," & vbCrLf & " EDPERCENT=" & mEDPERCENT & ", TOTEDUPERCENT=" & mTotEDUPercent & ", " & vbCrLf & " TOTEDUAMOUNT=" & mTotEDUAmount & ", TOTTAXABLEAMOUNT=" & mTOTTAXABLEAMOUNT & "," & vbCrLf & " TOTSERVICEPERCENT=" & mTotServicePercent & ", TOTSERVICEAMOUNT=" & mTotServiceAmount & ", " & vbCrLf & " ISREGDNO= '" & mIsRegdNo & "', LSTCST= '" & mLSTCST & "', " & vbCrLf & " WITHFORM= '" & mWITHFORM & "'," & vbCrLf & " FOC= '" & mFOC & "'," & vbCrLf & " PRINTED= '" & mPRINTED & "'," & vbCrLf & " CANCELLED= '" & mCancelled & "'," & vbCrLf & " NARRATION= '" & MainClass.AllowSingleQuote(txtNarration.Text) & "'," & vbCrLf & " TOTSURCHARGEAMT=" & mSURAmount & ", " & vbCrLf & " TOTDISCAMOUNT=" & mTotDiscount & ", " & vbCrLf & " TOTMSCAMOUNT=" & mMSC & ", " & vbCrLf & " TotRO=" & mRO & ", " & vbCrLf & " AGTD3='" & mD3 & "', " & vbCrLf & " PACK_MAT_FLAG='" & mPackMat & "', " & vbCrLf & " CHALLAN_MADE='" & mChallanMade & "', " & vbCrLf & " ISSTOCKTRF='" & mStockTrf & "', " & vbCrLf & " TCSAMOUNT='" & mTCSAMOUNT & "', TOTSHECPERCENT=0, TOTSHECAMOUNT=0," & vbCrLf & " NET_CGST_PER=" & Val(lblCGSTPer.Text) & ", NET_SGST_PER=" & Val(lblSGSTPer.Text) & ", NET_IGST_PER=" & Val(lblIGSTPer.Text) & ", "


            SqlStr = SqlStr & vbCrLf & " NETCGST_AMOUNT=" & Val(lblCGSTAmount.Text) & ", NETSGST_AMOUNT=" & Val(lblSGSTAmount.Text) & " , NETIGST_AMOUNT=" & Val(lblIGSTAmount.Text) & " , " & vbCrLf & " SHIPPED_TO_SAMEPARTY='N', SHIPPED_TO_PARTY_CODE='', " & vbCrLf & " SAC_CODE='" & Trim(txtHSNCode.Text) & "'," & vbCrLf & " SERV_PROV='" & MainClass.AllowSingleQuote(txtServProvided.Text) & "'," & vbCrLf & " TCSPER='" & mTCSPER & "', DNCNNO='',DNCNDATE='', " & vbCrLf & " UPDATE_FROM='N', INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & ", " & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),DIV_CODE=" & mDivisionCode & ",BILL_TO_LOC_ID='" & txtBillTo.Text & "',WITHOUT_GST='" & mWOGST & "', VENDOR_CODE='" & txtVendorCode.Text & "' " & vbCrLf _
                & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        End If
        PubDBCn.Execute(SqlStr)

        If UpdateSaleExp1(pGSTableAmount) = False Then GoTo ErrPart

        pDueDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, Val(txtCreditDays(1).Text), CDate(txtBillDate.Text)))

        '    If SalePostTRN_GST(PubDBCn, LblMKey.text, mCurRowNo, _	
        ''        LblBookCode.text, mBookType, mBookSubType, mBillNo, txtBillDate.Text, _	
        ''        mTRNType, mSuppCustCode, mAccountCode, Val(mNETVALUE), IIf(chkCancelled.Value = vbChecked, True, False), _	
        ''        pDueDate, False, txtRemarks.Text, IIf(chkFOC.Value = vbChecked, True, False), mConsingee, mTotServiceAmount, Val(lblTotExportExp.text), _	
        ''        Val(lblTotCGSTAmount.text), Val(lblTotIGSTAmount.text), Val(lblTotSGSTAmount.text), _	
        ''        ADDMode, mAddUser, mAddDate, 0, mDivisionCode) = False Then GoTo ErrPart	



        If Val(lblCGSTAmount.Text) + Val(lblIGSTAmount.Text) + Val(lblSGSTAmount.Text) > 0 Then
            If UpdateGSTTRN(PubDBCn, (lblMkey.Text), LblBookCode.Text, mBookType, mBookSubType, mBillNo, (txtBillDate.Text), mBillNo, (txtBillDate.Text), "", "", mSuppCustCode, mAccountCode, "Y", mSuppCustCode, 1, "-1", 1, "NOS", Val(txtTotItemValue.Text), Val(txtTotItemValue.Text), Val(txtTotItemValue.Text) + pGSTableAmount, 0, Val(lblCGSTPer.Text), Val(lblSGSTPer.Text), Val(lblIGSTPer.Text), Val(lblCGSTAmount.Text), Val(lblSGSTAmount.Text), Val(lblIGSTAmount.Text), Val(lblCGSTAmount.Text), Val(lblSGSTAmount.Text), Val(lblIGSTAmount.Text), mDivisionCode, Trim(txtHSNCode.Text), Trim(txtServProvided.Text), "", "N", "", "S", "N", "D", (txtBillDate.Text), "N") = False Then GoTo ErrPart

        End If

        If SalePostTRN_GST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mBillNo, (txtBillDate.Text), mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mNETVALUE)), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate, False, (txtRemarks.Text), False, "", mTotServiceAmount, 0, Val(lblCGSTAmount.Text), Val(lblIGSTAmount.Text), Val(lblSGSTAmount.Text), ADDMode, mAddUser, mAddDate, Val(txtTotItemValue.Text), mDivisionCode, "N", 0, 0, 0, txtBillTo.Text) = False Then GoTo ErrPart



        '    If SalePostTRN(PubDBCn, LblMKey.text, mCurRowNo, _	
        ''        LblBookCode.text, mBookType, mBookSubType, mBillNo, txtBillDate.Text, _	
        ''        mTRNType, mSuppCustCode, mAccountCode, Val(mNETVALUE), IIf(chkCancelled.Value = vbChecked, True, False), _	
        ''        pDueDate, False, txtRemarks.Text, IIf(chkFOC.Value = vbChecked, True, False), "", mTotServiceAmount, 0, ADDMode, mAddUser, mAddDate, mItemValue, mDivisionCode) = False Then GoTo ErrPart	


        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        '    Resume	
        txtBillNo.Text = ""
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''	
        RsSaleMain.Requery() ''.Refresh	
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''    Resume	
    End Function
    Private Function CheckValidBillDate(ByRef pBillNoSeq As Integer) As Object

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mRsCheck1 As ADODB.Recordset = Nothing
        Dim mRsCheck2 As ADODB.Recordset = Nothing
        Dim mBackBillDate As String = ""
        Dim mMaxInvStrfNo As Integer
        CheckValidBillDate = True

        If Val(txtBillNo.Text) = 1 Then Exit Function

        SqlStr = "SELECT MAX(INVOICE_DATE)" & vbCrLf & " FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND BOOKCode = " & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' " & vbCrLf & " AND INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & " " & vbCrLf & " AND BillNoSeq<" & Val(CStr(pBillNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDbNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(INVOICE_DATE)" & " FROM FIN_INVOICE_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND BOOKCode = " & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' " & vbCrLf & " AND INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & "" & vbCrLf & " AND BillNoSeq>" & Val(CStr(pBillNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck1.EOF = False And Not IsDbNull(mRsCheck1.Fields(0).Value) And mRsCheck2.EOF = False And Not IsDbNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtBillDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Bill Date Is Greater Than The BillDate Of Next InvoiceNo.")
                CheckValidBillDate = False
            ElseIf CDate(txtBillDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Bill Date Is Less Than The BillDate Of Previous InvoiceNo.")
                CheckValidBillDate = False
            End If
        ElseIf mRsCheck1.EOF = False And Not IsDbNull(mRsCheck1.Fields(0).Value) Then
            If CDate(txtBillDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Bill Date Is Greater Than The BillDate Of Next InvoiceNo.")
                CheckValidBillDate = False
            End If
        ElseIf mRsCheck2.EOF = False And Not IsDbNull(mRsCheck2.Fields(0).Value) Then
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

        If Trim(txtBillNoPrefix.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND BILLNOPREFIX='" & Trim(txtBillNoPrefix.Text) & "'"
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
    Private Function AutoGenSeqBillNoOld(ByRef mBookType As String, ByRef mBookSubType As String, ByRef pStartingNo As Integer, ByRef mDivisionCode As Double) As String

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

        SqlStr = ""

        xFYear = CInt(VB6.Format(RsCompany.Fields("Start_Date").Value, "YY"))
        mPrefix = CDbl(xFYear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & Val(lblInvoiceSeq.Text))


        'mStartingSNo = CDbl(xFYear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & Val(lblInvoiceSeq.Text) & VB6.Format(pStartingNo, "00000"))
        mStartingSNo = pStartingNo

        SqlStr = ""


        SqlStr = "SELECT Max(BILLNOSEQ)  FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BookType='" & mBookType & "'"

        SqlStr = SqlStr & vbCrLf & " AND INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & ""

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


        AutoGenSeqBillNoOld = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateSaleExp1(ByRef pGSTableAmount As Double) As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim mIsTaxable As String

        pGSTableAmount = 0
        PubDBCn.Execute("Delete From FIN_INVOICE_EXP Where Mkey='" & lblMkey.Text & "'")
        With SprdExp
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColExpName
                If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND GST_ENABLED='Y'") = True Then
                    mExpCode = MasterNo
                Else
                    mExpCode = -1
                End If

                .Col = ColExpPercent
                mPercent = Val(.Text)

                .Col = ColExpAmt
                mExpAmount = Val(.Text)

                If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y' AND TAXABLE='Y'") = True Then
                    mIsTaxable = MasterNo
                Else
                    mIsTaxable = "N"
                End If

                If mIsTaxable = "Y" Then
                    pGSTableAmount = pGSTableAmount + CDbl(VB6.Format(mExpAmount, "0.00"))
                End If

                SprdExp.Col = ColExpAddDeduct
                m_AD = SprdExp.Text
                If m_AD = "D" Then
                    mExpAmount = mExpAmount * -1
                End If

                SprdExp.Col = ColExpCalcOn
                mCalcOn = Val(.Text)

                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                SqlStr = ""
                If mCalcOn <> 0 Or mExpAmount <> 0 Then
                    SqlStr = "Insert Into  FIN_INVOICE_EXP (MKEY,SUBROWNO, " & vbCrLf & "EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & "Values ('" & lblMkey.Text & "'," & I & ", " & vbCrLf & "" & mExpCode & "," & mPercent & "," & mExpAmount & "," & mCalcOn & ",'" & mRO & "')"
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
        Dim mIsSPD As String
        Dim mTariffCode As String
        Dim mIsSaleComp As String
        Dim mWithInDistt As String
        Dim mInvPrefix As String
        Dim mInterUnit As String = ""

        FieldsVarification = True

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

        mInvPrefix = IIf(IsDbNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value)

        If mInvPrefix = "" Then
            MsgBox("Invoice Prefix is not Define, so cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
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

        If MODIFYMode = True And txtBillNo.Text = "" Then
            MsgInformation("Bill No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
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

        If txtDCDate.Text = "" Then
        ElseIf Not IsDate(txtDCDate.Text) Then
            MsgBox("DC Date is not Vaild", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDCDate.Focus()
            Exit Function
        End If

        If Trim(txtCustomer.Text) = "" Then
            MsgBox("Customer Cannot Be Blank", MsgBoxStyle.Information)
            ' txtCustomer.SetFocus	
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgBox("Customer Cannot Be Blank", MsgBoxStyle.Information)
            ' txtCustomer.SetFocus	
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
            cboInvType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        'If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '    MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
        '    'txtCustomer.SetFocus	
        '    FieldsVarification = False
        '    Exit Function
        'End If

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgInformation("Customer Name is not a Supplier or Customer Category. Cannot Save")
            If txtCustomer.Enabled = True Then txtCustomer.Focus()
            FieldsVarification = False
            Exit Function
        End If



        If lblInvoiceSeq.Text = "5" Then
            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C') AND INTER_UNIT='Y'") = False Then
                MsgInformation("Please Select Inter Unit. Cannot Save")
                If txtCustomer.Enabled = True Then txtCustomer.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LOCATION_ID='" & txtBillTo.Text & "'") = False Then
            MsgInformation("Location is not a valid for such Customer. Cannot Save")
            If txtCustomer.Enabled = True Then txtCustomer.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_DISTT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mWithInDistt = MasterNo
        Else
            mWithInDistt = "N"
        End If

        If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Credit Account Does Not Exist In Master", MsgBoxStyle.Information)
            'txtCreditAccount.SetFocus	
            FieldsVarification = False
            Exit Function
        End If


        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
            mBookSubType = MasterNo
        Else
            mBookSubType = CStr(-1)
        End If


        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "ISSTOCKTRF", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
            mIsStockTransfer = MasterNo
        Else
            mIsStockTransfer = "N"
        End If

        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "ISSPD", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
            mIsSPD = MasterNo
        Else
            mIsSPD = "N"
        End If

        If mBookSubType = "J" Or mBookSubType = "M" Then

        ElseIf Trim(txtItemType.Text) = "" Then
            txtItemType.Text = "-"
            'MsgBox("Item Type Cann't be blank.", MsgBoxStyle.Information)
            'FieldsVarification = False
            'txtItemType.Focus()
            'Exit Function
        End If

        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = IIf(IsDbNull(MasterNo), "N", MasterNo)
        End If

        If lblInvoiceSeq.Text = "4" Or lblInvoiceSeq.Text = "0" Then
            '        If mInterUnit = "Y" Then	
            '            MsgBox "Cann't be select Inter Unit.", vbInformation	
            '            FieldsVarification = False	
            '            Exit Function	
            '        End If	
        Else
            If mInterUnit = "N" Then
                MsgBox("Please select Inter Unit.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If


        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmInvoice_MiscGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If CDbl(lblInvoiceSeq.Text) = 5 Then
            Me.Text = "Input Service Distributor Invoice"
        ElseIf CDbl(lblInvoiceSeq.Text) = 0 Then
            Me.Text = "Bill of Supply"
        Else
            Me.Text = "Service / Rental Invoice (GST)"
        End If
        SqlStr = ""
        SqlStr = "Select * from FIN_INVOICE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from FIN_INVOICE_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleExp, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mCustomerCode = CStr(-1)

        'JB = New JsonBag
        'JB.Whitespace = System.Windows.Forms.CheckState.Checked

        FillCboSaleType()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume	
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        SqlStr = ""

        SqlStr = "SELECT FIN_INVTYPE_MST.NAME AS INVOICE_TYPE,BILLNOPREFIX,TO_CHAR(BILLNOSEQ),BILLNOSUFFIX, " & vbCrLf & " BILLNO,INVOICE_DATE  AS BILLDATE, TO_CHAR(INV_PREP_TIME,'HH24:MI') AS BILLTIME, " & vbCrLf & " AUTO_KEY_DESP AS DCNO, DCDATE AS DCDATE, " & vbCrLf & " CUST_PO_NO AS PONO, CUST_PO_DATE AS PODATE, " & vbCrLf & " REMOVAL_DATE AS REMOVAL_DATE, TO_CHAR(REMOVAL_TIME,'HH24:MI') AS REMOVAL_TIME, " & vbCrLf & " A.SUPP_CUST_NAME AS CUSTOMER, B.SUPP_CUST_NAME AS CREDIT_ACCOUNT, " & vbCrLf & " ITEMDESC, NETVALUE FROM " & vbCrLf & " FIN_INVOICE_HDR, FIN_INVTYPE_MST, FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_MST B " & vbCrLf & " WHERE FIN_INVOICE_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " And FIN_INVOICE_HDR.FYEAR='" & RsCompany.Fields("FYEAR").Value & "' " & vbCrLf & " And FIN_INVOICE_HDR.BOOKCODE=" & LblBookCode.Text & " " & vbCrLf & " AND FIN_INVOICE_HDR.COMPANY_CODE=FIN_INVTYPE_MST.COMPANY_CODE " & vbCrLf & " AND FIN_INVOICE_HDR.TRNTYPE=FIN_INVTYPE_MST.CODE " & vbCrLf & " AND FIN_INVOICE_HDR.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf & " AND FIN_INVOICE_HDR.SUPP_CUST_CODE=A.SUPP_CUST_CODE " & vbCrLf & " AND FIN_INVOICE_HDR.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND FIN_INVOICE_HDR.ACCOUNTCODE=B.SUPP_CUST_CODE "

        SqlStr = SqlStr & vbCrLf & " AND FIN_INVOICE_HDR.INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & ""


        SqlStr = SqlStr & vbCrLf & " Order by BILLDATE,BillNo"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 600)
            .set_ColWidth(1, 2000)
            .set_ColWidth(2, 0)
            .set_ColWidth(3, 0)
            .set_ColWidth(4, 0)

            .set_ColWidth(5, 1200)
            .set_ColWidth(6, 1200)
            .set_ColWidth(7, 1200)
            .set_ColWidth(8, 1200)
            .set_ColWidth(9, 1000)
            .set_ColWidth(10, 1000)
            .set_ColWidth(11, 1000)
            .set_ColWidth(12, 1000)
            .set_ColWidth(13, 1000)
            .set_ColWidth(14, 3000)
            .set_ColWidth(15, 500 * 6)
            .set_ColWidth(16, 500 * 2)
            .set_ColWidth(17, 500 * 2)
            .set_ColWidth(18, 500 * 2)
            .Col = 18
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdExp(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdExp
            .Row = Arow
            .set_RowHeight(Arow, 10)

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
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpPercent, 5)
            .TypeEditMultiLine = False

            .Col = ColExpAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpAmt, 9)
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

            MainClass.ProtectCell(SprdExp, 1, .MaxRows, ColExpName, ColExpName)
        End With
        MainClass.SetSpreadColor(SprdExp, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub

    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsSaleMain
            ' TxtDCNoPrefix.MaxLength = 0	

            'txtDCNoSuffix.MaxLength = 0	

            txtBillNoPrefix.Maxlength = .Fields("BillNoPrefix").DefinedSize ''	
            txtBillNo.Maxlength = .Fields("AUTO_KEY_INVOICE").Precision ''	
            txtBillNoSuffix.Maxlength = .Fields("BillNoSuffix").DefinedSize ''	
            txtBillDate.Maxlength = 10
            TxtBillTm.Maxlength = 5

            txtCustomer.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtBillTo.MaxLength = MainClass.SetMaxLength("LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn)

            txtCreditAccount.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

            txtCreditDays(0).Maxlength = .Fields("DUEDAYSFROM").Precision ''	
            txtCreditDays(1).Maxlength = .Fields("DUEDAYSTO").Precision ''	

            txtHSNCode.Maxlength = MainClass.SetMaxLength("HSN_CODE", "GEN_HSN_MST", PubDBCn) ''	
            txtTariff.Maxlength = .Fields("TARIFFHEADING").DefinedSize ''	

            txtItemType.Maxlength = .Fields("ItemDesc").DefinedSize ''	
            txtRemarks.Maxlength = .Fields("Remarks").DefinedSize ''	
            txtNarration.Maxlength = .Fields("NARRATION").DefinedSize ''	
            txtCarriers.Maxlength = .Fields("CARRIERS").DefinedSize ''	
            txtVehicle.Maxlength = .Fields("VehicleNo").DefinedSize ''	
            txtDocsThru.Maxlength = .Fields("DocsThrough").DefinedSize ''	
            txtMode.Maxlength = .Fields("DespatchMode").DefinedSize ''	

            txtDCNo.MaxLength = .Fields("GRNO").DefinedSize ''	
            txtDCDate.MaxLength = 10

            txtServProvided.Maxlength = MainClass.SetMaxLength("HSN_DESC", "GEN_HSN_MST", PubDBCn)
        End With
        Exit Sub
ERR1:
        '    Resume	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        With RsSaleMain
            If Not .EOF Then

                lblMkey.Text = .Fields("MKey").Value

                If MainClass.ValidateWithMasterTable((.Fields("TRNTYPE").Value), "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    cboInvType.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "INV_HEADING", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblInvHeading.Text = IIf(IsDbNull(MasterNo), "", MasterNo)
                End If

                If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mBookSubType = MasterNo
                Else
                    mBookSubType = CStr(-1)
                End If

                txtBillNoPrefix.Text = IIf(IsDbNull(.Fields("BILLNOPREFIX").Value), "", .Fields("BILLNOPREFIX").Value)
                ''txtBillNo.Text = IIf(IsDBNull(.Fields("BILLNOSEQ").Value), "", .Fields("BILLNOSEQ").Value)       '', "00000000")

                If Val(.Fields("BILLNOSEQ").Value) = 1509 And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 And RsCompany.Fields("FYEAR").Value = 2023 Then
                    txtBillNo.Text = VB6.Format(IIf(IsDBNull(.Fields("BILLNOSEQ").Value), "", .Fields("BILLNOSEQ").Value), "0")

                Else
                    txtBillNo.Text = VB6.Format(IIf(IsDBNull(.Fields("BILLNOSEQ").Value), "", .Fields("BILLNOSEQ").Value), ConBillFormat)

                End If
                txtBillNoSuffix.Text = IIf(IsDBNull(.Fields("BILLNOSUFFIX").Value), "", .Fields("BILLNOSUFFIX").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")


                TxtBillTm.Text = VB6.Format(IIf(IsDbNull(.Fields("INV_PREP_TIME").Value), "", .Fields("INV_PREP_TIME").Value), "HH:MM")

                txtHSNCode.Text = IIf(IsDbNull(.Fields("SAC_CODE").Value), "", .Fields("SAC_CODE").Value)
                txtServProvided.Text = ""

                If CDbl(lblInvoiceSeq.Text) = 0 Then
                    If MainClass.ValidateWithMasterTable((txtHSNCode.Text), "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'") = True Then
                        txtServProvided.Text = MasterNo
                    End If
                Else
                    If MainClass.ValidateWithMasterTable((txtHSNCode.Text), "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                        txtServProvided.Text = MasterNo
                    End If
                End If

                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustomer.Text = MasterNo
                End If

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)

                If MainClass.ValidateWithMasterTable((.Fields("ACCOUNTCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCreditAccount.Text = MasterNo
                End If


                txtCreditDays(0).Text = IIf(IsDbNull(.Fields("DUEDAYSFROM").Value), "", .Fields("DUEDAYSFROM").Value)
                txtCreditDays(1).Text = IIf(IsDbNull(.Fields("DUEDAYSTO").Value), "", .Fields("DUEDAYSTO").Value)

                chkCancelled.CheckState = IIf(.Fields("CANCELLED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCancelled.Enabled = IIf(.Fields("CANCELLED").Value = "Y", False, True)

                chkFOC.CheckState = IIf(.Fields("FOC").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                chkPackmat.CheckState = IIf(.Fields("PACK_MAT_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkChallanMade.CheckState = IIf(.Fields("CHALLAN_MADE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkWoGST.CheckState = IIf(.Fields("WITHOUT_GST").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkStockTrf.CheckState = IIf(.Fields("ISSTOCKTRF").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                lblTotQty.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.00")
                txtTotItemValue.Text = VB6.Format(IIf(IsDbNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")

                lblNetAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")



                '            cboExciseEntry.Text = IIf(IsNull(.Fields("EXCISEDEBITTYPE").Value), "", .Fields("EXCISEDEBITTYPE").Value)	
                '            txtExciseNo.Text = IIf(IsNull(.Fields("EXCISEDEBITNO").Value), "", .Fields("EXCISEDEBITNO").Value)	
                '            txtExciseDate.Text = IIf(IsNull(.Fields("EXCISEDEBITDATE").Value), "", .Fields("EXCISEDEBITDATE").Value)	
                txtTariff.Text = IIf(IsDbNull(.Fields("TARIFFHEADING").Value), "", .Fields("TARIFFHEADING").Value)

                txtItemType.Text = IIf(IsDbNull(.Fields("ITEMDESC").Value), "", .Fields("ITEMDESC").Value)
                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtNarration.Text = IIf(IsDbNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)
                txtCarriers.Text = IIf(IsDbNull(.Fields("CARRIERS").Value), "", .Fields("CARRIERS").Value)

                txtVehicle.Text = IIf(IsDbNull(.Fields("VEHICLENO").Value), "", .Fields("VEHICLENO").Value)
                txtDocsThru.Text = IIf(IsDbNull(.Fields("DOCSTHROUGH").Value), "", .Fields("DOCSTHROUGH").Value)
                txtMode.Text = IIf(IsDbNull(.Fields("DESPATCHMODE").Value), "", .Fields("DESPATCHMODE").Value)


                If .Fields("FREIGHTCHARGES").Value = "To Pay" Then
                    OptFreight(0).Checked = True
                Else
                    OptFreight(1).Checked = True
                End If

                txtVendorCode.Text = IIf(IsDBNull(.Fields("VENDOR_CODE").Value), "", .Fields("VENDOR_CODE").Value)
                txtDCNo.Text = IIf(IsDBNull(.Fields("GRNO").Value), "", .Fields("GRNO").Value)
                txtDCDate.Text = VB6.Format(IIf(IsDBNull(.Fields("GRDATE").Value), "", .Fields("GRDATE").Value), "DD/MM/YYYY")

                txtPONo.Text = IIf(IsDbNull(.Fields("CUST_PO_NO").Value), "", .Fields("CUST_PO_NO").Value)
                txtPODate.Text = VB6.Format(IIf(IsDbNull(.Fields("CUST_PO_DATE").Value), "", .Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                lblCGSTPer.Text = VB6.Format(IIf(IsDbNull(.Fields("NET_CGST_PER").Value), 0, .Fields("NET_CGST_PER").Value), "0.00")
                lblSGSTPer.Text = VB6.Format(IIf(IsDbNull(.Fields("NET_SGST_PER").Value), 0, .Fields("NET_SGST_PER").Value), "0.00")
                lblIGSTPer.Text = VB6.Format(IIf(IsDbNull(.Fields("NET_IGST_PER").Value), 0, .Fields("NET_IGST_PER").Value), "0.00")

                txtIRNNo.Text = IIf(IsDbNull(.Fields("IRN_NO").Value), "", .Fields("IRN_NO").Value)
                txteInvAckNo.Text = IIf(IsDbNull(.Fields("IRN_ACK_NO").Value), "", .Fields("IRN_ACK_NO").Value)
                txteInvAckDate.Text = VB6.Format(IIf(IsDbNull(.Fields("IRN_ACK_DATE").Value), "", .Fields("IRN_ACK_DATE").Value), "DD/MM/YYYY HH:MM")

                If Trim(txtIRNNo.Text) = "" Then
                    cmdeInvoice.Enabled = IIf(RsCompany.Fields("E_INVOICE_APP").Value = "Y", True, False)
                Else
                    cmdeInvoice.Enabled = False
                End If

                mAddUser = IIf(IsDbNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                mAddDate = VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                mModUser = IIf(IsDbNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                mModDate = VB6.Format(IIf(IsDbNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False

                Call ShowSaleExp1()
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                ''Call CalcTots	
            End If
        End With
        Call txtServProvided_Validating(txtServProvided, New System.ComponentModel.CancelEventArgs(False))
        txtBillNo.Enabled = True
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        SprdExp.Enabled = True

        '    cboInvType.Enabled = IIf(XRIGHT = "AMDV", True, False)	
        cboInvType.Enabled = IIf(CDbl(PubUserLevel) = 1 Or CDbl(PubUserLevel) = 2 Or XRIGHT = "AMDV", True, False) ''	
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

        SqlStr = ""
        SqlStr = "Select FIN_INVOICE_EXP.EXPCODE,FIN_INVOICE_EXP.EXPPERCENT, FIN_INVOICE_EXP.RO," & vbCrLf & " FIN_INVOICE_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn " & vbCrLf & " From FIN_INVOICE_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_INVOICE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_INVOICE_EXP.Mkey='" & lblMkey.Text & "'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

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
                    .Text = CStr(Val(IIf(IsDbNull(RsSaleExp.Fields("ExpPercent").Value), "", RsSaleExp.Fields("ExpPercent").Value)))

                    .Col = ColExpAmt
                    If RsSaleExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off	
                        .Text = CStr(Val(IIf(IsDbNull(RsSaleExp.Fields("Amount").Value), "", RsSaleExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDbNull(RsSaleExp.Fields("Amount").Value), "", RsSaleExp.Fields("Amount").Value))))
                    End If

                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDbNull(RsSaleExp.Fields("CODE").Value), 0, RsSaleExp.Fields("CODE").Value)))

                    .Col = ColExpAddDeduct 'ExpFlag	
                    .Text = IIf(RsSaleExp.Fields("Add_Ded").Value = "A", "A", "D")

                    .Col = ColExpIdent
                    .Text = IIf(IsDbNull(RsSaleExp.Fields("Identification").Value), "", RsSaleExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off	
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If

                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDbNull(RsSaleExp.Fields("Taxable").Value), "N", RsSaleExp.Fields("Taxable").Value)

                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDbNull(RsSaleExp.Fields("Exciseable").Value), "N", RsSaleExp.Fields("Exciseable").Value)

                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDbNull(RsSaleExp.Fields("CalcOn").Value), "", RsSaleExp.Fields("CalcOn").Value)))

                    SprdExp.Col = ColRO
                    SprdExp.Value = IIf(RsSaleExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    RsSaleExp.MoveNext()
                Loop
            End With
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub

    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh	
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim mQty As Double
        Dim mRate As Double
        Dim mItemAmount As Double
        Dim mItemValue As Double
        Dim mTotQty As Double
        Dim j As Integer
        Dim I As Integer
        Dim mItemCode As String
        Dim xStr As String
        Dim mExpPercent As Double
        Dim mNetAccessAmt As Double
        Dim pTotKKCAmount As Double
        Dim mExciseableAmount As Double
        Dim mTaxableAmount As Double
        Dim mModvatableAmount As Double
        Dim mTotModvatableAmount As Double
        Dim mTotServiceableAmount As Double
        Dim mTotSTRefundableAmt As Double
        Dim mShortage As Double
        Dim mCEDCessAble As Double
        Dim mADDCessAble As Double
        Dim mCESSableAmount As Double
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

        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double

        Dim mTotCGST As Double
        Dim mTotSGST As Double
        Dim mTotIGST As Double

        Dim mExpName As String
        Dim mIsTaxable As String
        Dim mOtherTaxableAmount As Double

        Dim mLocal As String
        Dim mPartyGSTNo As String

        Dim mServCode As String
        Dim mSACCode As String = ""
        'Dim mCGSTPer As Double	
        'Dim mSGSTPer As Double	
        'Dim mIGSTPer As Double	
        Dim xAcctCode As String


        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            xAcctCode = "-1"
        End If

        mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        'mLocal = "N"
        'If Trim(txtCustomer.Text) <> "" Then
        '    If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mLocal = Trim(MasterNo)
        '    End If
        'End If

        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        If CDbl(lblInvoiceSeq.Text) = 0 Then
            If MainClass.ValidateWithMasterTable((txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'") = True Then
                mSACCode = MasterNo
            End If
        Else
            If MainClass.ValidateWithMasterTable((txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                mSACCode = MasterNo
            End If
        End If

        mCGSTPer = 0
        mSGSTPer = 0
        mIGSTPer = 0

        If chkWoGST.CheckState = System.Windows.Forms.CheckState.Checked Then
            If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, "N") = False Then GoTo ERR1
        Else
            If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, "G") = False Then GoTo ERR1
        End If

        lblCGSTPer.Text = VB6.Format(mCGSTPer, "0.00")
        lblSGSTPer.Text = VB6.Format(mSGSTPer, "0.00")
        lblIGSTPer.Text = VB6.Format(mIGSTPer, "0.00")

        pRound = 0
        mQty = 0
        mRate = 0
        '    mST = 0	
        mItemAmount = 0
        mTotItemAmount = 0
        mItemValue = 0
        mTotExp = 0
        mOtherTaxableAmount = 0
        '    mADEAmount = 0	

        With SprdExp
            For I = 1 To SprdExp.MaxRows
                .Row = I
                .Col = ColExpName
                mExpName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y' AND TAXABLE='Y'") = True Then
                    mIsTaxable = MasterNo
                Else
                    mIsTaxable = "N"
                End If

                If mIsTaxable = "Y" Then
                    .Col = ColExpAmt
                    mOtherTaxableAmount = mOtherTaxableAmount + CDbl(VB6.Format(.Text, "0.00"))
                End If
            Next
        End With


        mTotItemAmount = Val(txtTotItemValue.Text)
        mNetAccessAmt = Val(CStr(mOtherTaxableAmount + mTotItemAmount))
        mExciseableAmount = Val(CStr(mTotItemAmount))
        mTaxableAmount = Val(CStr(mOtherTaxableAmount + mTotItemAmount)) ''0 dt.26-11-2010	

        '    mCGSTPer = Val(lblCGSTPer.text)	
        '    mSGSTPer = Val(lblSGSTPer.text)	
        '    mIGSTPer = Val(lblIGSTPer.text)	
        '	
        mTotCGST = System.Math.Round(mNetAccessAmt * mCGSTPer * 0.01, 2)
        mTotSGST = System.Math.Round(mNetAccessAmt * mSGSTPer * 0.01, 2)
        mTotIGST = System.Math.Round(mNetAccessAmt * mIGSTPer * 0.01, 2)


        Call BillExpensesCalcTots_GST(SprdExp, (txtBillDate.Text), mNetAccessAmt, mTotItemAmount, mTaxableAmount, 0, 0, 0, mTotIGST, mTotSGST, mTotCGST, pTotExportExp, 0, 0, pTotOthers, pTotCustomDutyExport, pTotCustomDuty, pTotMSC, pTotDiscount, 0, pTotRO, pTotTCS, mTotExp, pTCSPer, "S")



        txtTotItemValue.Text = VB6.Format(mTotItemAmount, "#0.00")
        lblCGSTAmount.Text = VB6.Format(mTotCGST, "#0.00")
        lblSGSTAmount.Text = VB6.Format(mTotSGST, "#0.00")
        lblIGSTAmount.Text = VB6.Format(mTotIGST, "#0.00")
        lblOtherExp.Text = VB6.Format(mTotExp, "#0.00")


        lblNetAmount.Text = VB6.Format(mTotExp + mTotItemAmount + mTotCGST + mTotSGST + mTotIGST, "#0.00") '' Format(mTotItemAmount + mTotExp, "#0.00")    ''+ mTotItemAmount	
        lblTotFreight.Text = VB6.Format(pTotOthers, "#0.00")
        lblTotCharges.Text = CStr(0) ''Format(mRO, "#0.00")	
        lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(mTaxableAmount, "#0.00")

        lblRO.Text = VB6.Format(pTotRO, "#0.00")
        lblDiscount.Text = VB6.Format(pTotDiscount, "#0.00")
        lblSurcharge.Text = VB6.Format(pTotSurcharge, "#0.00")
        lblMSC.Text = VB6.Format(pTotMSC, "#0.00")
        lblTotQty.Text = VB6.Format(mTotQty, "#0.00")
        lblTCS.Text = VB6.Format(pTotTCS, "#0.00")




        lblSTPercentage.Text = CStr(Val(CStr(pSTPer)))
        lblTCSPercentage.Text = CStr(Val(CStr(pTCSPer)))



        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Sub


    Private Sub Clear1()

        lblMkey.Text = ""
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

        mAddUser = ""
        mAddDate = ""
        mModUser = ""
        mModDate = ""

        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        cboInvType.SelectedIndex = -1
        txtBillNoPrefix.Text = GetDocumentPrefix("S", lblInvoiceSeq.Text, cboDivision.Text)  ' "S" ''& IIf(IsNull(RsCompany!INVOICE_PREFIX), "", RsCompany!INVOICE_PREFIX)  ''txtBillNoPrefix.Text = "S"	
        txtBillNo.Text = ""
        txtBillNoSuffix.Text = ""
        txtBillDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        TxtBillTm.Text = GetServerTime()

        txtCustomer.Text = ""
        txtBillTo.Text = ""
        txtCreditAccount.Text = ""

        txtCreditDays(0).Text = ""
        txtCreditDays(1).Text = ""
        txtPONo.Text = ""
        txtPODate.Text = ""

        txtVendorCode.Text = ""
        txtDCNo.Text = ""
        txtDCDate.Text = ""
        txtServProvided.Text = ""
        lblInvHeading.Text = ""

        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked

        lblTotQty.Text = "0.00"
        txtTotItemValue.Text = "0.00"

        lblNetAmount.Text = "0.00"

        txtTariff.Text = ""
        txtHSNCode.Text = ""
        '    txtHSNDesc.Text = ""	

        '    txtST38No.Text = ""	
        txtItemType.Text = ""
        txtRemarks.Text = ""
        txtNarration.Text = ""
        txtCarriers.Text = ""
        txtVehicle.Text = ""
        txtDocsThru.Text = ""
        txtMode.Text = ""
        OptFreight(0).Checked = True
        OptFreight(1).Checked = False

        txtHSNCode.Enabled = IIf(lblInvoiceSeq.Text = 0, True, False)

        chkPackmat.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkChallanMade.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkWoGST.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtTotItemValue.Text = VB6.Format(0, "#0.00")

        lblNetAmount.Text = VB6.Format(0, "#0.00")
        lblTotFreight.Text = VB6.Format(0, "#0.00")
        lblTotCharges.Text = VB6.Format(0, "#0.00")
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(0, "#0.00")

        lblRO.Text = VB6.Format(0, "#0.00")
        lblDiscount.Text = VB6.Format(0, "#0.00")
        lblSurcharge.Text = VB6.Format(0, "#0.00")
        lblMSC.Text = VB6.Format(0, "#0.00")
        lblTCS.Text = VB6.Format(0, "#0.00")
        lblTCSPercentage.Text = VB6.Format(0, "#0.00")

        lblCGSTAmount.Text = VB6.Format(0, "#0.00")
        lblSGSTAmount.Text = VB6.Format(0, "#0.00")
        lblIGSTAmount.Text = VB6.Format(0, "#0.00")
        lblOtherExp.Text = VB6.Format(0, "#0.00")

        lblCGSTPer.Text = VB6.Format(0, "0.00")
        lblSGSTPer.Text = VB6.Format(0, "0.00")
        lblIGSTPer.Text = VB6.Format(0, "0.00")

        txtIRNNo.Text = ""
        txteInvAckNo.Text = ""
        txteInvAckDate.Text = ""
        cmdeInvoice.Enabled = False
        cmdeInvoice.Enabled = IIf(RsCompany.Fields("E_INVOICE_APP").Value = "Y", True, False)
        cmdQRCode.Enabled = IIf(RsCompany.Fields("E_INVOICE_APP").Value = "Y", True, False)

        chkStockTrf.CheckState = System.Windows.Forms.CheckState.Unchecked


        TabMain.SelectedIndex = 0

        Dim SqlStr As String = ""
        Dim RsAuth As ADODB.Recordset = Nothing

        SqlStr = " SELECT NAME,WEF FROM FIN_AUTH_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND STATUS='O'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAuth, ADODB.LockTypeEnum.adLockReadOnly)

        If RsAuth.EOF = False Then
            txtAuth.Text = IIf(IsDbNull(RsAuth.Fields("NAME").Value), "", RsAuth.Fields("NAME").Value)
            txtAuthDate.Text = VB6.Format(IIf(IsDbNull(RsAuth.Fields("WEF").Value), "", RsAuth.Fields("WEF").Value), "DD/MM/YYYY")
        End If

        MainClass.ClearGrid(SprdExp)
        Call FillSprdExp()

        FraPostingDtl.Visible = False
        MainClass.ClearGrid(SprdPostingDetail)
        Call FormatSprdPostingDetail(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FillSprdExp()

        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim mLocal As String
        Dim xAcctCode As String
        MainClass.ClearGrid(SprdExp)



        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            xAcctCode = "-1"
        End If

        mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")

        'If Trim(txtCustomer.Text) <> "" Then
        '    If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mLocal = IIf(MasterNo = "Y", "L", "C")
        '    Else
        '        mLocal = ""
        '    End If
        'Else
        '    mLocal = ""
        'End If

        SqlStr = "Select * From FIN_INTERFACE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (Type='S' OR Type='B') "

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " Order By PrintSequence"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            I = 0
            Do While Not RS.EOF
                I = I + 1

                SprdExp.Row = I

                SprdExp.Col = ColExpName
                SprdExp.Text = RS.Fields("Name").Value

                SprdExp.Col = ColExpPercent
                If ADDMode = True Then
                    SprdExp.Text = Str(IIf(IsDbNull(RS.Fields("DefaultPercent").Value), 0, Str(RS.Fields("DefaultPercent").Value)))
                Else
                    SprdExp.Text = ""
                End If

                SprdExp.Col = ColExpAmt
                SprdExp.Text = "0"

                SprdExp.Col = ColExpSTCode
                SprdExp.Text = CStr(Val(IIf(IsDbNull(RS.Fields("CODE").Value), -1, RS.Fields("CODE").Value)))

                SprdExp.Col = ColExpAddDeduct
                SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")

                SprdExp.Col = ColExpIdent
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)
                If SprdExp.Text = "DAM" Then MainClass.ProtectCell(SprdExp, I, I, 1, SprdExp.MaxCols)

                SprdExp.Col = ColTaxable
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Taxable").Value), "N", RS.Fields("Taxable").Value)

                SprdExp.Col = ColExciseable
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Exciseable").Value), "N", RS.Fields("Exciseable").Value)

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
        End If
        FormatSprdExp(-1)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume	
    End Sub
    '    Private Sub FillExpFromPartyExp()

    '        On Error GoTo ERR1
    '        Dim RS As ADODB.Recordset = Nothing
    '        Dim xAcctCode As String
    '        Dim xTrnCode As Double
    '        Dim I As Integer
    '        Dim mLocal As String
    '        Dim mRO As String


    '        If Trim(txtCustomer.Text) = "" Then Exit Sub
    '        If Trim(cboInvType.Text) = "" Then Exit Sub

    '        Call FillSprdExp()

    '        If Trim(txtCustomer.Text) <> "" Then
    '            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                mLocal = IIf(MasterNo = "Y", "L", "C")
    '            Else
    '                mLocal = ""
    '            End If
    '        Else
    '            mLocal = ""
    '        End If


    '        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '            xAcctCode = MasterNo
    '        Else
    '            xAcctCode = "-1"
    '        End If

    '        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
    '            xTrnCode = MasterNo
    '        Else
    '            xTrnCode = CDbl("-1")
    '        End If

    '        SqlStr = "Select IH.*, ID.PERCENT,ID.RO FROM " & vbCrLf & " FIN_INTERFACE_MST IH, FIN_PARTY_INTERFACE_MST ID  " & vbCrLf & " Where IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE(+) " & vbCrLf & " AND IH.CODE=ID.EXPCODE(+) " & vbCrLf & " AND ID.SUPP_CUST_CODE='" & xAcctCode & "'" & vbCrLf & " AND ID.TRNTYPE='" & xTrnCode & "'" & vbCrLf & " AND (IH.Type='S' OR IH.Type='B')  " & vbCrLf & " AND ID.CATEGORY='S' "

    '        If PubGSTApplicable = True Then
    '            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
    '        Else
    '            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
    '        End If

    '        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PrintSequence"

    '        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

    '        If RS.EOF = False Then
    '            I = 0
    '            Do While Not RS.EOF
    '                I = I + 1

    '                SprdExp.Row = I

    '                SprdExp.Col = ColExpName
    '                SprdExp.Text = RS.Fields("Name").Value

    '                mRO = IIf(IsDbNull(RS.Fields("RO").Value), "N", RS.Fields("RO").Value)

    '                SprdExp.Col = ColRO
    '                SprdExp.Value = IIf(mRO = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

    '                SprdExp.Col = ColExpPercent

    '                SprdExp.Text = Str(IIf(IsDbNull(RS.Fields("PERCENT").Value), 0, Str(RS.Fields("PERCENT").Value)))

    '                SprdExp.Col = ColExpAmt
    '                SprdExp.Text = "0"

    '                SprdExp.Col = ColExpSTCode
    '                SprdExp.Text = CStr(Val(IIf(IsDbNull(RS.Fields("CODE").Value), -1, RS.Fields("CODE").Value)))

    '                SprdExp.Col = ColExpAddDeduct
    '                SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")

    '                SprdExp.Col = ColExpIdent
    '                SprdExp.Text = IIf(IsDbNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)
    '                If SprdExp.Text = "DAM" Then MainClass.ProtectCell(SprdExp, I, I, 1, SprdExp.MaxCols)

    '                SprdExp.Col = ColTaxable
    '                SprdExp.Text = IIf(IsDbNull(RS.Fields("Taxable").Value), "N", RS.Fields("Taxable").Value)

    '                SprdExp.Col = ColExciseable
    '                SprdExp.Text = IIf(IsDbNull(RS.Fields("Exciseable").Value), "N", RS.Fields("Exciseable").Value)

    '                If RS.Fields("Identification").Value = "ST" Then
    '                    If RS.Fields("STTYPE").Value = mLocal Then
    '                        SprdExp.RowHidden = False
    '                    Else
    '                        SprdExp.RowHidden = True
    '                    End If
    '                End If

    '                RS.MoveNext()

    '                If RS.EOF = False Then
    '                    SprdExp.MaxRows = SprdExp.MaxRows + 1
    '                End If
    '            Loop
    '        End If
    '        FormatSprdExp(-1)
    '        Exit Sub
    'ERR1:
    '        MsgInformation(Err.Description)
    '        ''Resume	
    '    End Sub
    Private Sub FrmInvoice_MiscGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmInvoice_MiscGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Public Sub FrmInvoice_MiscGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        If InStr(1, XRIGHT, "D", CompareMethod.Text) > 1 Then
            chkCancelled.Enabled = True
        Else
            chkCancelled.Enabled = False
        End If

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7755) '8000	
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900	

        TabMain.SelectedIndex = 0

        AdoDCMain.Visible = False

        txtCustomer.Enabled = True
        txtBillNoPrefix.Enabled = False
        txtBillNoSuffix.Enabled = False
        txtBillDate.Enabled = IIf(CDbl(PubUserLevel) = 1 Or CDbl(PubUserLevel) = 2 Or XRIGHT = "AMDV", True, False) ''IIf(XRIGHT = "AMDV", True, False)	


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
                    If PubGSTApplicable = True Then
                        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
                    End If

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
                            Call CalcTots()
                            Exit Sub
                        End If
                        SprdExp.Row = ESRow
                        SprdExp.Col = 3
                        If MainClass.ValidateWithMasterTable(m_xpn, "Name", "RoundOff", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                            If MasterNo = True Then
                                SprdExp.Text = VB6.Format((CDbl(m_ExpPercent) * CDbl(txtTotItemValue.Text)) / 100, "0")
                            Else
                                SprdExp.Text = VB6.Format((CDbl(m_ExpPercent) * CDbl(txtTotItemValue.Text)) / 100, "0")
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
        Call CalcTots()
        Exit Sub
ErrPart:
        SprdExp.Col = ESCol
        SprdExp.col2 = ESCol
        SprdExp.Row = ESRow
        SprdExp.Row2 = ESRow
        SprdExp.BlockMode = True
        SprdExp.Action = 0
        SprdExp.BlockMode = False
        SprdExp.Focus()
    End Sub

    Private Sub SprdExp_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdExp.ClickEvent

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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

    Private Sub txtCarriers_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCarriers.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCarriers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCarriers.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCarriers.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        Call SearchCustomer()
    End Sub
    Private Sub SearchCustomer()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE IN ( SELECT SUPP_CUST_CODE FROM FIN_SUPP_CUST_MST WHERE SUPP_CUST_TYPE IN ('S','C')"
        If lblInvoiceSeq.Text = "5" Then
            SqlStr = SqlStr & vbCrLf & " AND INTER_UNIT='Y'"
        End If

        SqlStr = SqlStr & vbCrLf & " )"
        ''
        If MainClass.SearchGridMaster((txtCustomer.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR||SUPP_CUST_CITY", SqlStr) = True Then

            ' If MainClass.SearchGridMaster((txtCustomer.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtCustomer.Text = AcName
            txtBillTo.Text = AcName2
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCustomer()
    End Sub
    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtCustomer.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If lblInvoiceSeq.Text = "5" Then
            SqlStr = SqlStr & vbCrLf & " AND INTER_UNIT='Y'"
        End If

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCustomerCode = MasterNo
        Else
            mCustomerCode = "-1"
            Cancel = True
        End If

        Call FillSprdExp()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
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
        Dim mServCode As String
        Dim mSACCode As String = ""
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mLocal As String
        'Dim mHSNDesc As String	
        Dim mPartyGSTNo As String
        Dim xAcctCode As String


        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            xAcctCode = "-1"
        End If

        mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "GST_RGN_NO")


        If Trim(txtServProvided.Text) = "" Then GoTo EventExitSub

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

        '	
        '    If MainClass.ValidateWithMasterTable(txtServProvided.Text, "NAME", "CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = False Then	
        '        MsgInformation "Please Select Valid Service Provided"	
        '        Cancel = True	
        '        Exit Sub	
        '    Else	
        '        mServCode = MasterNo	
        '    End If	

        If CDbl(lblInvoiceSeq.Text) = 0 Then
            If MainClass.ValidateWithMasterTable((txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'") = True Then
                mSACCode = MasterNo
            End If
        Else
            If MainClass.ValidateWithMasterTable((txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                mSACCode = MasterNo
            End If
        End If

        '    If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then	
        '        mHSNDesc = MasterNo	
        '    End If	

        mCGSTPer = 0
        mSGSTPer = 0
        mIGSTPer = 0

        If chkWoGST.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, "G") = False Then GoTo ERR1
        End If

        txtHSNCode.Text = Trim(mSACCode)
        '    txtHSNDesc.Text = Trim(mHSNDesc)	

        lblCGSTPer.Text = VB6.Format(mCGSTPer, "0.00")
        lblSGSTPer.Text = VB6.Format(mSGSTPer, "0.00")
        lblIGSTPer.Text = VB6.Format(mIGSTPer, "0.00")

        CalcTots()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchProvidedMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If CDbl(lblInvoiceSeq.Text) = 0 Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G' "
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S' "
        End If

        If MainClass.SearchGridMaster((txtServProvided.Text), "GEN_HSN_MST", "HSN_DESC", "HSN_CODE", , , SqlStr) = True Then
            txtServProvided.Text = AcName
            txtServProvided_Validating(txtServProvided, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

    Private Sub txtTotItemValue_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotItemValue.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotItemValue_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotItemValue.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotItemValue_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotItemValue.Leave
        Call CalcTots()
    End Sub

    Private Sub txtTotItemValue_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotItemValue.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
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
    Private Sub txtVendorCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVendorCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDCNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDCNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDCDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDCDate.TextChanged
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
    Private Sub txtVendorCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVendorCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVendorCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDCNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDCNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDCNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
    Private Sub FillCboSaleType()

        On Error GoTo ErrPart
        Dim RsSaleType As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        cboInvType.Items.Clear()

        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='S' AND IDENTIFICATION='P' ORDER BY NAME "

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

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicle.Text)
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
        GetExicseAbleAmt = pExicseAbleAmt + Val(txtTotItemValue.Text)
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetExicseAbleAmt = 0
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
    Private Sub cmdPostingHead_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPostingHead.Click

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer

        FraPostingDtl.Visible = Not FraPostingDtl.Visible
        If FraPostingDtl.Visible = True Then
            FraPostingDtl.BringToFront()
            MainClass.ClearGrid(SprdPostingDetail)
            SqlStr = " SELECT ACM.SUPP_CUST_NAME, " & vbCrLf & " ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))) AS AMOUNT, " & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<=0 THEN 'Cr' ELSE 'Dr' END AS DC "

            SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE  " & vbCrLf & " TRN.Company_Code=ACM.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "

            SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE ='" & mBookType & "'" & vbCrLf & " AND BOOKSUBTYPE = '" & mBookSubType & "'" & vbCrLf & " AND TRN.MKEY='" & lblMkey.Text & "'"

            SqlStr = SqlStr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            cntRow = 1
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    SprdPostingDetail.Row = cntRow
                    SprdPostingDetail.Col = 1
                    SprdPostingDetail.Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                    SprdPostingDetail.Col = 2
                    SprdPostingDetail.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("Amount").Value), "0.00", RsTemp.Fields("Amount").Value), "0.00")

                    SprdPostingDetail.Col = 3
                    SprdPostingDetail.Text = IIf(IsDbNull(RsTemp.Fields("DC").Value), "", RsTemp.Fields("DC").Value)

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

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(1, 30)

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
            .set_ColWidth(3, 5)
        End With


        MainClass.ProtectCell(SprdPostingDetail, 1, SprdPostingDetail.MaxRows, 1, 3)

        MainClass.SetSpreadColor(SprdPostingDetail, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub chkWoGST_CheckedChanged(sender As Object, e As EventArgs) Handles chkWoGST.CheckedChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub
End Class
