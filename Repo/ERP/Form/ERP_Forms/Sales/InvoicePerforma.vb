Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Color
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.ComponentModel
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
Imports System.Data.OleDb
Imports AxFPSpreadADO

Friend Class FrmInvoicePerforma
    Inherits System.Windows.Forms.Form


    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Dim RsSaleMain As ADODB.Recordset ''Recordset				
    Dim RsSaleDetail As ADODB.Recordset ''Recordset				
    Dim RsSaleExp As ADODB.Recordset ''Recordset				


    ''''Private PvtDBCn As ADODB.Connection				

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String
    Dim mCustomerCode As String
    Dim pRound As Double

    Dim pShowCalc As Boolean

    Dim mAddUser As String
    Dim mAddDate As String
    Dim mModUser As String
    Dim mModDate As String

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColPartNo As Short = 2
    Private Const ColItemDesc As Short = 3
    Private Const ColHSNCode As Short = 4
    Private Const ColUnit As Short = 5
    Private Const ColGlassDescription As Short = 6
    Private Const ColActualWidthInch As Short = 7
    Private Const ColActualHeightInch As Short = 8
    Private Const ColActualWidth As Short = 9
    Private Const ColActualHeight As Short = 10
    Private Const ColChargeableWidth As Short = 11
    Private Const ColChargeableHeight As Short = 12


    Private Const ColArea As Short = 13
    Private Const ColPacketQty As Short = 14
    Private Const ColQty As Short = 15
    Private Const ColAreaRate As Short = 16
    Private Const ColMRP As Short = 17
    Private Const ColDiscRate As Short = 18
    Private Const ColRate As Short = 19

    Private Const ColAmount As Short = 20
    Private Const ColTaxableAmount As Short = 21
    Private Const ColCGSTPer As Short = 22
    Private Const ColCGSTAmount As Short = 23
    Private Const ColSGSTPer As Short = 24
    Private Const ColSGSTAmount As Short = 25
    Private Const ColIGSTPer As Short = 26
    Private Const ColIGSTAmount As Short = 27
    Private Const ColGlassDevelopmentRate As Short = 28
    Private Const ColDieDevelopmentRate As Short = 29

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


    Dim FileDBCn As ADODB.Connection

    Dim mIndentificationCode As String
    Dim mExpCode As String
    Dim pMSRCost As Double
    Dim pMSPCost As Double
    Dim pFreightCost As Double
    Dim pToolAmorCost As Double

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Function GetSORate(ByRef pItemCode As String, ByRef xCustomerCode As String, ByRef pDespType As String, ByRef IsMRP As String, ByRef mOldBillDate As String, ByRef pUOM As String, ByRef pCGSTPer As Double, ByRef pSGSTPer As Double, ByRef pIGSTPer As Double, ByRef mInvoiceType As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFieldName As String
        Dim mWOPO As Boolean
        Dim mPurchaseUOM As String
        Dim mFactor As Double
        Dim mHSNCode As String
        Dim mPartyGSTNo As String
        Dim mMerchantExporter As String

        mWOPO = False
        pCGSTPer = 0
        pSGSTPer = 0
        pIGSTPer = 0

        mMerchantExporter = "N"
        If MainClass.ValidateWithMasterTable(xCustomerCode, "SUPP_CUST_CODE", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER= 'EXPORTER-MERCHANT'") = True Then
            mMerchantExporter = "Y"
        End If


        If Val(lblPoNo.Text) = CDbl("-1") Or Val(lblPoNo.Text) = CDbl("0") Then
            If IsMRP = "MSP" Or IsMRP = "MSR" Or IsMRP = "FR" Or IsMRP = "TOL" Or IsMRP = "J" Then GetSORate = 0 : Exit Function
        End If

        If pDespType = "E" Then
            SqlStr = "SELECT RATE_INR AS ITEM_PRICE, 0 AS CGST_PER, 0 AS SGST_PER, 0 As IGST_PER, -1 AS ACCOUNT_POSTING_CODE " & vbCrLf _
                & " FROM FIN_EXPINV_HDR IH, FIN_EXPINV_DET ID" & vbCrLf _
                & " WHERE IH.AUTO_KEY_EXPINV=ID.AUTO_KEY_EXPINV " & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                & " AND IH.AUTO_KEY_PACK=" & Val(lblPoNo.Text) & ""
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
                SqlStr = "SELECT " & mFieldName & " AS ITEM_PRICE, CGST_PER, SGST_PER, IGST_PER, ACCOUNT_POSTING_CODE" & vbCrLf _
                    & " FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                    & " AND IH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf _
                    & " AND IH.MKEY = ("

                If pDespType = "U" Then
                    SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _
                        & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                        & " AND SID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                        & " AND SIH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf _
                        & " AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(mOldBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                Else
                    SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _
                        & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                        & " AND SID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                        & " AND SIH.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'" & vbCrLf _
                        & " AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
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
                SqlStr = "SELECT " & mFieldName & " AS ITEM_PRICE, 0 AS CGST_PER, 0 AS SGST_PER, 0 As IGST_PER, '-1' AS ACCOUNT_POSTING_CODE " & vbCrLf _
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
                mPartyGSTNo = GetPartyBusinessDetail(xCustomerCode, Trim(txtBillTo.Text), "GST_RGN_NO")
                'If MainClass.ValidateWithMasterTable(xCustomerCode, "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    mPartyGSTNo = MasterNo
                'End If

                If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, "N", "0", mPartyGSTNo,,,, mMerchantExporter) = False Then GoTo ErrPart
            Else
                pCGSTPer = Val(IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value))
                pSGSTPer = Val(IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value))
                pIGSTPer = Val(IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value))
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

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" '' & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        'If ADDMode = True Then
        '    SqlStr = SqlStr & vbCrLf & "AND STATUS='O'"
        'End If

        'If MainClass.SearchGridMaster(txtCustomer.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
        '    txtCustomer.Text = AcName
        '    mCustomerCode = AcName1
        '    txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False))
        'End If

        If MainClass.SearchGridMaster((txtCustomer.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR", SqlStr) = True Then
            txtCustomer.Text = AcName
            'txtCustomerCode.Text = AcName1
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mAddress As String

        If Trim(txtCustomer.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT SUPP_CUST_NAME,SUPP_CUST_CODE, SUPP_CUST_ADDR, SUPP_CUST_CITY, " & vbCrLf _
            & " SUPP_CUST_STATE, SUPP_CUST_PIN " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_TYPE IN ('S','C')" & vbCrLf _
            & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"

        If ADDMode = True Then
            SqlStr = SqlStr & vbCrLf & "AND STATUS='O'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mCustomerCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
        Else
            mCustomerCode = "-1"
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
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
        Dim SqlStr As String
        Dim xAcctCode As String


        If Trim(txtShippedFrom.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtShippedFrom.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgBox("Invalid Shipped From Supplier Name.", vbInformation)
            Cancel = True
            Exit Sub
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchDespatchFrom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDespatchFrom.Click
        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        If ADDMode = True Then
            SqlStr = SqlStr & " AND STATUS='O'"
        End If


        If MainClass.SearchGridMaster(txtShippedFrom.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtShippedFrom.Text = AcName
            txtShippedFrom_Validating(txtShippedFrom, New System.ComponentModel.CancelEventArgs(False))
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

    Private Sub cboTransmode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTransmode.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkShipTo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShipTo.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtShippedTo.Enabled = False
            cmdSearchShippedTo.Enabled = False
        Else
            txtShippedTo.Enabled = True
            cmdSearchShippedTo.Enabled = True
        End If
    End Sub

    Private Sub cmdSavePrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSavePrint.Click
        On Error GoTo ErrPart

        Exit Sub
ErrPart:

    End Sub

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
        Dim SqlStr As String
        Dim mDivisionCode As Double
        Dim xSupplierCode As Double
        Dim mVNo As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSupplierCode = MasterNo
        End If

        mVNo = ""

        If Val(CStr(Val(txtBillNo.Text))) > 0 Then
            mVNo = Trim(Trim(txtBillNoPrefix.Text) & VB6.Format(Val(txtBillNo.Text), "00000000") & Trim(txtBillNoSuffix.Text))
        End If

        ''            & " AND DIV_CODE = " & mDivisionCode & "" & vbCrLf _				
        '				
        SqlStr = " SELECT VNO, VDATE, SUM(NETVALUE) AS NETVALUE FROM ("

        SqlStr = SqlStr & vbCrLf & " SELECT VNO, VDATE, SUM(NETVALUE) AS NETVALUE " & vbCrLf & " FROM FIN_ADVANCE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xSupplierCode & "' AND BOOKTYPE='AR'" & vbCrLf _
            & " AND VDATE <= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY VNO, VDATE"

        SqlStr = SqlStr & vbCrLf & " UNION "

        SqlStr = SqlStr & vbCrLf & " SELECT ADV_VNO AS VNO, ADV_VDATE AS VDATE, SUM(ADV_ADJUSTED_AMT*-1) AS ADV_ADJUSTED_AMT " & vbCrLf & " FROM FIN_PRO_INVOICE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xSupplierCode & "'" & vbCrLf _
            & " AND INVOICE_DATE <= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If mVNo <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND FYEAR || BILLNO <> " & RsCompany.Fields("FYEAR").Value & " || '" & mVNo & "'"
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
        Dim SqlStr As String
        Dim mDivisionCode As Double

        If txtAdvVNo.Text = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        ''AND DIV_CODE = " & mDivisionCode & "				

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND BOOKTYPE='AR'"

        If MainClass.ValidateWithMasterTable(txtAdvVNo.Text, "VNO", "VDATE", "FIN_ADVANCE_HDR", PubDBCn, MasterNo, , SqlStr) = True Then
            txtAdvDate.Text = Format(MasterNo, "DD/MM/YYYY")
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
        Dim SqlStr As String
        Dim xAcctCode As String


        If Trim(txtShippedTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtShippedTo.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgBox("Invalid Shipped to Supplier Name.", vbInformation)
            Cancel = True
            Exit Sub
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchShippedTo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchShippedTo.Click
        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""  '' AND SUPP_CUST_TYPE IN ('S','C')"
        'If ADDMode = True Then
        '    SqlStr = SqlStr & " AND STATUS='O'"
        'End If

        'If MainClass.SearchGridMaster(txtShippedTo.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
        '    txtShippedTo.Text = AcName
        '    txtShippedTo_Validating(txtShippedTo, New System.ComponentModel.CancelEventArgs(True))
        '    '        If TxtRemarks.Enabled = True Then TxtRemarks.SetFocus
        'End If

        If MainClass.SearchGridMaster((txtShippedTo.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR", SqlStr) = True Then
            txtShippedTo.Text = AcName
            'txtCustomerCode.Text = AcName1
            TxtShipTo.Text = AcName2
            txtShippedTo_Validating(txtShippedTo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub


    Private Sub ReportonShow(ByRef Mode As Crystal.DestinationConstants)
        'Dim Printer As New Printer				
        On Error GoTo ReportErr
        Dim SqlStr As String
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
        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle,  ,  , "Y")

        MainClass.AssignCRptFormulas(Report1, "Range=""" & IIf(IsDBNull(RsCompany.Fields("EXCISE_RANGE").Value), "", RsCompany.Fields("EXCISE_RANGE").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "Division=""" & IIf(IsDBNull(RsCompany.Fields("EXCISE_DIV").Value), "", RsCompany.Fields("EXCISE_DIV").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "RegnNo=""" & IIf(IsDBNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "Place=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & """")

        '    mCT3Date = GetCT3Date(PubDBCn, Val(TxtCTNo.Text), "", "S", mCustomerCode)				

        '    MainClass.AssignCRptFormulas Report1, "CT3Date=""" & mCT3Date & """"				

        Report1.WindowShowGroupTree = False

        'If PubUniversalPrinter = "Y" And Mode = Crystal.DestinationConstants.crptToPrinter Then				

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
        Dim mCatCode As String
        Dim mSubCatCode As String

        ''''SELECT CLAUSE...				


        MakeSQL = " SELECT IH.*,ID.*, CMST.*, ITEMMST.* "


        ''''FROM CLAUSE...				
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_PRO_INVOICE_HDR IH, FIN_PRO_INVOICE_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST ITEMMST"

        ''''WHERE CLAUSE...				
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE=ITEMMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=ITEMMST.ITEM_CODE " & vbCrLf _
            & " AND IH.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"


        MakeSQL = MakeSQL & vbCrLf & "ORDER BY SUBROWNO "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim mDeleteRights As String
        Dim xDCNo As String


        If Trim(txtBillNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If MainClass.GetUserCanModify(txtBillDate.Text) = False Then
            MsgBox("You Have Not Rights to Delete back Voucher", vbInformation)
            Exit Sub
        End If

        mDeleteRights = GetUserPermission("INVOICE_ADMIN", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)

        If mDeleteRights = "N" Then
            MsgBox("You Have Not Rights to Delete Invoice.", MsgBoxStyle.Information)
            Exit Sub
        End If


        If Not RsSaleMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.				
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "FIN_PRO_INVOICE_HDR", (LblMKey.Text), RsSaleMain, "Mkey", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_PRO_INVOICE_DET", (LblMKey.Text), RsSaleDetail, "Mkey", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_PRO_INVOICE_EXP", (LblMKey.Text), RsSaleExp, "Mkey", "D") = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "FIN_PRO_INVOICE_HDR", "MKEY", (LblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from FIN_PRO_INVOICE_EXP Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("Delete from FIN_PRO_INVOICE_DET Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("Delete from FIN_PRO_INVOICE_HDR Where Mkey='" & LblMKey.Text & "'")


                PubDBCn.CommitTrans()
                RsSaleMain.Requery() ''.Refresh				
                RsSaleDetail.Requery() ''.Refresh				
                RsSaleExp.Requery() ''.Refresh				
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''				
        RsSaleMain.Requery() ''.Refresh				
        RsSaleDetail.Requery() ''.Refresh				
        RsSaleExp.Requery() ''.Refresh				
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click
        On Error GoTo ModifyErr
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mPRINTED As String

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Modified.")
            Exit Sub
        End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtBillNo.Enabled = IIf(PubSuperUser = "S", True, False)
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
        Dim mInvoicePrintType As String
        Dim mExtraRemarks As String
        Dim mPrintOption As String


        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Invoice Cann't be Print.")
            Exit Sub
        End If

        mPrintOption = "I"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            frmPrintInvoice.OptInvoice.Enabled = True
            frmPrintInvoice.OptInvoiceAnnex.Enabled = True
            frmPrintInvoice.optSubsidiaryChallan.Enabled = True
            frmPrintInvoice.Opt4.Enabled = True

            frmPrintInvoice.OptInvoice.Visible = True
            frmPrintInvoice.OptInvoiceAnnex.Visible = True
            frmPrintInvoice.optSubsidiaryChallan.Visible = True
            frmPrintInvoice.Opt4.Visible = True

            frmPrintInvoice.OptInvoice.Text = "BP"
            frmPrintInvoice.OptInvoiceAnnex.Text = "Automotive"
            frmPrintInvoice.optSubsidiaryChallan.Text = "Development"
            frmPrintInvoice.Opt4.Text = "Architecture"

            frmPrintInvoice.FraF4.Enabled = True
            frmPrintInvoice.FraF4.Visible = True
            frmPrintInvoice.ShowDialog()

            If G_PrintLedg = False Then
                frmPrintInvoice.Close()
                Exit Sub
            Else
                If frmPrintInvoice.OptInvoice.Checked = True Then
                    mPrintOption = "I"
                ElseIf frmPrintInvoice.OptInvoiceAnnex.Checked = True Then
                    mPrintOption = "A"
                ElseIf frmPrintInvoice.optSubsidiaryChallan.Checked = True Then
                    mPrintOption = "D"
                Else
                    mPrintOption = "R"
                End If
            End If
        End If

        Call ReportOnSales(Crystal.DestinationConstants.crptToWindow, mInvoicePrintType, "N", mPrintOption)

        '    Unload frmPrintInvCopy				
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ErrPart:
        '    Unload frmPrintInvoice				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub ReportOnSales(ByRef Mode As Crystal.DestinationConstants, ByRef mInvoicePrintType As String, ByRef pIsTradingInv As String, ByRef mPrintOption As String)
        Dim frmPrintInvCopy As Object
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mRptFileName As String
        Dim mVNo As String
        Dim mWithInState As String
        Dim mRMCustomer As Boolean

        '    If chkCancelled.Value = vbChecked Then				
        '        MsgInformation "Cancelled Invoice Cann't be Print."				
        '        Exit Sub				
        '    End If				

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mWithInState = "N"
        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mWithInState = MasterNo
        End If

        mRMCustomer = False
        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER='CUSTOMER-RM'") = True Then
            mRMCustomer = True
        End If

        SqlStr = ""
        mTitle = ""
        mSubTitle = ""

        Call SelectQryForPrint(SqlStr)

        mTitle = "PROFORMA INVOICE"
        mSubTitle = ""
        If mPrintOption = "I" Then
            mRptFileName = "Invoice_PI.rpt"
        ElseIf mPrintOption = "A" Then
            mRptFileName = "Invoice_PI_Auto.rpt"
        ElseIf mPrintOption = "D" Then
            mRptFileName = "Invoice_PI_Develop.rpt"
        Else
            mRptFileName = "Invoice_PI_Architecture.rpt"
        End If


        'If frmPrintInvCopy.optShow(0).Value = True Then

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            Call ShowExciseReport11(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, True, mInvoicePrintType, "N")
        Else
            Call ShowExciseReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, True, mInvoicePrintType, "N")
        End If

        'Else
        '    Call ShowExciseReport(SqlStr, Crystal.DestinationConstants.crptToPrinter, mTitle, mSubTitle, mRptFileName, True, mInvoicePrintType, "Y")
        'End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub


    Private Function SelectQryForPrint(ByRef mSqlStr As String) As String
        Dim mCustomerCode As String
        Dim pBarCodeString As String
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mInvoicePrintType As String
        Dim CntCount As Integer
        Dim mUpdateStart As Boolean

        On Error GoTo ErrPart

        mUpdateStart = True
        '    PubDBCn.Errors.Clear				
        '    PubDBCn.BeginTrans				
        '				
        '    SqlStr = "DELETE FROM TEMP_BARCODE_PRINT WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"				
        '    PubDBCn.Execute SqlStr				
        '				
        '    If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then				
        '        mCustomerCode = MasterNo				
        '    End If				
        '				
        '    pBarCodeString = ""				
        '				
        ''    For CntCount = 1 To 1       ''For CntCount = 0 To 5				
        ''        If frmPrintInvCopy.chkPrintOption(CntCount).Value = vbChecked Then				
        ''            mInvoicePrintType = UCase(frmPrintInvCopy.chkPrintOption(CntCount).Caption)				
        '            SqlStr = "INSERT INTO TEMP_BARCODE_PRINT ( " & vbCrLf _				
        ''                    & " USER_ID, MKEY, BARCODE_VALUE, PRINT_INVOICE_TYPE ) VALUES (" & vbCrLf _				
        ''                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "','" & LblMKey.Caption & "','" & pBarCodeString & "','')"  ''" & mInvoicePrintType & "				
        '				
        '            PubDBCn.Execute SqlStr				
        ''        End If				
        ''    Next				
        '				
        '    PubDBCn.CommitTrans				

        mUpdateStart = False

        mSqlStr = " SELECT " & vbCrLf _
            & " IH.*, ID.*, GMST.*, CMST.SUPP_CUST_NAME "

        ''''FROM CLAUSE...				
        mSqlStr = mSqlStr & vbCrLf _
            & " FROM FIN_PRO_INVOICE_HDR IH, FIN_PRO_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST"


        ''''WHERE CLAUSE...				
        mSqlStr = mSqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND  " & vbCrLf _
            & " IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY='" & LblMKey.Text & "'" ''& vbCrLf |            & " --AND IH.COMPANY_CODE=IDD.COMPANY_CODE" & vbCrLf |            & " --AND IH.AUTO_KEY_DESP=IDD.AUTO_KEY_DESP" & vbCrLf |            & " --AND BP.USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"				

        ''''ORDER CLAUSE...				

        mSqlStr = mSqlStr & vbCrLf _
            & "ORDER BY ID.SUBROWNO"

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
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim RsTempShip As ADODB.Recordset
        Dim mNetAmount As Double
        Dim mNetDuty As Double
        Dim mPrepTime As String
        Dim mRemovalTime As String
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

        Dim mShipToName As String
        Dim mShipToAddress As String
        Dim mShipToCity As String
        Dim mShipToGSTN As String
        Dim mCompanyDetail As String
        Dim mCompanyeMail As String
        Dim mCompanyWebSite As String
        Dim mShipToState As String
        Dim mShipToStateCode As String
        Dim mStateName As String
        Dim mStateCode As String
        Dim mWithInState As String
        Dim mWithInCountry As String
        Dim mPlaceofSupply As String
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
        Dim mPrepareBy As String
        Dim mPrepareById As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle,  ,  , "Y")

        '    If PubUserID = "G0416" Then				
        '        mRptFileName = Left(mRptFileName, Len(mRptFileName) - 4) & "_E.rpt"				
        '    End If				

        mStateName = ""
        mStateCode = ""

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mStateName = MasterNo
            mStateCode = GetStateCode(mStateName)
        End If

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mWithInState = MasterNo
        End If

        If mWithInState = "N" Then
            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInCountry = MasterNo
            End If
        End If

        mPlaceofSupply = VB6.Format(mStateCode, "00") & "-" & mStateName ''IIf(mWithInState = "Y", "INTRA STATE", IIf(mWithInCountry = "Y", "INTER STATE", "EXPORT"))				



        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.WindowShowPrintBtn = True '' IIf(PubSuperUser = "S", True, False)				
        Report1.WindowShowPrintSetupBtn = True ''IIf(PubSuperUser = "S", True, False)				
        '    Report1.PrinterName = "Microsoft Print to PDF"				
        Report1.WindowShowExportBtn = True


        SqlStr = " SELECT NETVALUE, ITEMVALUE, NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT,INV_PREP_TIME, " & vbCrLf _
            & " SHIPPED_TO_PARTY_CODE, OUR_AUTO_KEY_SO, SHIPPED_TO_SAMEPARTY, " & vbCrLf _
            & " IS_DESP_OTHERTHAN_BILL, SHIPPED_FROM_PARTY_CODE, IS_SHIPPTO_EX_WORK,ADDUSER" & vbCrLf _
            & " FROM FIN_PRO_INVOICE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
            mNetDuty = IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)


            mShipToSameParty = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
            If mShipToSameParty = "Y" Then ''mCustomerCode
                mShipToCode = mCustomerCode
            Else
                mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
            End If
            mExWork = IIf(IsDBNull(RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value), "N", RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value)

            mPrepareById = IIf(IsDBNull(RsTemp.Fields("ADDUSER").Value), "", RsTemp.Fields("ADDUSER").Value)
            mPrepareBy = ""
            If MainClass.ValidateWithMasterTable(mPrepareById, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPrepareBy = MasterNo
            End If

            MainClass.AssignCRptFormulas(Report1, "PrepareBy=""" & mPrepareBy & """")

            mPrepTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INV_PREP_TIME").Value), "", RsTemp.Fields("INV_PREP_TIME").Value), "HH:MM")
            mRemovalTime = "" '' Format(IIf(IsdbNull(RsTemp!REMOVAL_TIME), "", RsTemp!REMOVAL_TIME), "HH:MM")				
            mSO = IIf(IsDBNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), "", RsTemp.Fields("OUR_AUTO_KEY_SO").Value)

            If mExWork = "Y" Then ''mShipToSameParty				
                mShipToName = "Ex Work"
                mShipToAddress = ""
                mShipToCity = ""
                mShipToGSTN = ""
                mShipToState = ""
                mShipToStateCode = ""
            Else
                SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf _
                    & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf _
                    & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipToCode) & "'"
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
                SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf _
                    & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf _
                    & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
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

                    MainClass.AssignCRptFormulas(Report1, "ShipFromName=""" & mShipFromName & """")
                    MainClass.AssignCRptFormulas(Report1, "ShipFromAddress=""" & mShipFromAddress & """")
                    MainClass.AssignCRptFormulas(Report1, "ShipFromCity=""" & mShipFromCity & """")
                    ''                MainClass.AssignCRptFormulas Report1, "ShipFromGSTN=""" & mShipFromGSTN & """"				

                    MainClass.AssignCRptFormulas(Report1, "ShipFromState=""" & mShipFromState & """")
                    ''                MainClass.AssignCRptFormulas Report1, "ShipFromStateCode=""" & mShipFromStateCode & """"				

                End If
            End If

        End If

        If UCase(mRptFileName) = "INVOICE_SGST.RPT" Or UCase(mRptFileName) = "INVOICE_IGST.RPT" Then
            mEPCGNo = ""
            mEPCGDate = ""
            SqlStr = " SELECT EPCG_NO, EPCG_DATE  " & vbCrLf & " FROM DSP_SALEORDER_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_SO='" & MainClass.AllowSingleQuote(lblPoNo.Text) & "'" & vbCrLf _
                & " AND CUST_PO_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "'" & vbCrLf _
                & " AND CUST_PO_DATE=TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SO_STATUS='O'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTempShip.EOF = False Then
                mEPCGNo = IIf(IsDBNull(RsTempShip.Fields("EPCG_NO").Value), "", RsTempShip.Fields("EPCG_NO").Value)
                mEPCGDate = VB6.Format(IIf(IsDBNull(RsTempShip.Fields("EPCG_DATE").Value), "", RsTempShip.Fields("EPCG_DATE").Value), "DD/MM/YYYY")
            End If

            If mEPCGNo <> "" Then
                mEPCGNo = "EPCG License No : " & mEPCGNo & " &  Date : " & mEPCGDate
            End If
            MainClass.AssignCRptFormulas(Report1, "EPCGNo=""" & mEPCGNo & """")
            '        MainClass.AssignCRptFormulas Report1, "EPCGDate=""" & mEPCGDate & """"				
        End If




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

        '    MainClass.AssignCRptFormulas Report1, "mServiceName=""" & Trim(txtServProvided.Text) & """"				



        mPayTerms = ""
        'If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
        '    SqlStr = " SELECT PAYMENT_DESC " & vbCrLf & " FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & txtCustomer.Text & "'"
        '    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        '    If RsTemp.EOF = False Then
        '        mPayTerms = IIf(IsDBNull(RsTemp.Fields("PAYMENT_DESC").Value), "", RsTemp.Fields("PAYMENT_DESC").Value)
        '    End If
        '    MainClass.AssignCRptFormulas(Report1, "PAYTERMS=""" & mPayTerms & """")
        'End If

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

            SqlStrSub = " SELECT FIN_PRO_INVOICE_EXP.MKEY, FIN_PRO_INVOICE_EXP.SUBROWNO, FIN_PRO_INVOICE_EXP.EXPPERCENT, FIN_PRO_INVOICE_EXP.AMOUNT, FIN_INTERFACE_MST.COMPANY_CODE, FIN_INTERFACE_MST.NAME" & vbCrLf _
                & " FROM FIN_PRO_INVOICE_EXP, FIN_PRO_INVOICE_HDR, FIN_INTERFACE_MST " & vbCrLf _
                & " WHERE FIN_PRO_INVOICE_EXP.MKEY = FIN_PRO_INVOICE_HDR.MKEY AND FIN_PRO_INVOICE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _
                & " AND FIN_PRO_INVOICE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf _
                & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

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

            '          Report1.SubreportToChange = ""				
        End If


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
        '    '        If PubUserID = "G0416" Then				
        '    '            Dim pOutPutFileName As String				
        '    '            If WebRequestGenerateDigitalSign("D:\test_DS.pdf", pOutPutFileName) = False Then Exit Sub				
        '    '        End If				
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
    Private Sub ShowExciseReport11(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean, ByRef mInvoicePrintType As String, ByRef pIsPDF As String)

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

        mRptFileName = PubReportFolderPath & mRptFileName
        'mRptFileName = "G:\VBDotNetERP_Blank\Form\bin\Debug\Reports\PDF_Invoice_SGSTNew.rpt"
        CrReport.Load(mRptFileName)

        SqlStrSub = " SELECT * " & vbCrLf _
            & " FROM FIN_PRO_INVOICE_EXP, FIN_PRO_INVOICE_HDR, FIN_INTERFACE_MST " & vbCrLf _
            & " WHERE FIN_PRO_INVOICE_EXP.MKEY = FIN_PRO_INVOICE_HDR.MKEY " & vbCrLf _
            & " AND FIN_PRO_INVOICE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _
            & " AND FIN_PRO_INVOICE_HDR.COMPANY_CODE = FIN_INTERFACE_MST.COMPANY_CODE" & vbCrLf _
            & " And FIN_PRO_INVOICE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf _
            & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

        SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"

        SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

        Call Connect_MainReport_To_Database_11(CrReport)        '', mSqlStr
        Call Connect_SubReport_To_Database_11(CrReport, "PurExp")      '

        CrReport.RecordSelectionFormula = "{IH.COMPANY_CODE} = " & RsCompany.Fields("COMPANY_CODE").Value & " AND {IH.MKEY} = '" & MainClass.AllowSingleQuote(LblMKey.Text) & "' AND {IH.FYEAR} = '" & RsCompany.Fields("FYEAR").Value & "'"


        '& " IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
        '& " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
        '& " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
        '& " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '& " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        '& " AND IH.MKEY='" & LblMKey.Text & "'" ''& vbCrLf |            & " --AND IH.COMPANY_CODE=IDD.COMPANY_CODE" & vbCrLf |            & " --AND IH.AUTO_KEY_DESP=IDD.AUTO_KEY_DESP" & vbCrLf |            & " --AND BP.USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"				


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

        SqlStr = " SELECT NETVALUE, ITEMVALUE, NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT,INV_PREP_TIME, " & vbCrLf _
            & " SHIPPED_TO_PARTY_CODE, OUR_AUTO_KEY_SO, SHIPPED_TO_SAMEPARTY, " & vbCrLf _
            & " IS_DESP_OTHERTHAN_BILL, SHIPPED_FROM_PARTY_CODE, IS_SHIPPTO_EX_WORK" & vbCrLf _
            & " FROM FIN_PRO_INVOICE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
            mNetDuty = IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)


            mShipToSameParty = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
            If mShipToSameParty = "Y" Then ''mCustomerCode
                mShipToCode = mCustomerCode
            Else
                mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
            End If

            mExWork = IIf(IsDBNull(RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value), "N", RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value)

            'mPrepTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INV_PREP_TIME").Value), "", RsTemp.Fields("INV_PREP_TIME").Value), "HH:MM")
            'mRemovalTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("REMOVAL_TIME").Value), "", RsTemp.Fields("REMOVAL_TIME").Value), "HH:MM")
            mSO = IIf(IsDBNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), "", RsTemp.Fields("OUR_AUTO_KEY_SO").Value)

            'mHour = HoursInText(VB.Left(mRemovalTime, 2))
            'mMin = MinInText(VB.Right(mRemovalTime, 2))

            'mHour = mHour & " " & mMin

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
                SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf _
                    & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf _
                    & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
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

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            AssignCRpt11Formulas(CrReport, "mShipToPIN", "'" & mShipToPIN & "'")
            AssignCRpt11Formulas(CrReport, "mShipToPhoneNo", "'" & mShipToPhoneNo & "'")
            AssignCRpt11Formulas(CrReport, "mShipToMailID", "'" & mShipToMailID & "'")
        End If

        'If lblDespRef.Text = "P" And Mid(RsCompany.Fields("COMPANY_NAME").Value, 1, 3) = "KAY" Then
        '    Dim mSaleAgreementNo As String = ""
        '    Dim mSaleAgreementDate As String = ""

        '    If MainClass.ValidateWithMasterTable((lblPoNo.Text), "AUTO_KEY_SO", "SCHD_AGREEMENT_NO", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O' AND SO_APPROVED='Y'") = True Then
        '        mSaleAgreementNo = IIf(IsDBNull(MasterNo), "", MasterNo)
        '    End If
        '    If MainClass.ValidateWithMasterTable((lblPoNo.Text), "AUTO_KEY_SO", "SCHD_AGREEMENT_DATE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O' AND SO_APPROVED='Y'") = True Then
        '        mSaleAgreementDate = IIf(IsDBNull(MasterNo), "", MasterNo)
        '    End If
        '    If mSaleAgreementNo = "" Then
        '        mSaleAgreementNo = ""
        '    Else
        '        mSaleAgreementNo = "Schedule Agreement No : " & mSaleAgreementNo & " Dated : " & VB6.Format(mSaleAgreementDate, "DD/MM/YYYY")
        '    End If

        '    AssignCRpt11Formulas(CrReport, "SaleAgreementNo", "'" & Trim(mSaleAgreementNo) & "'")
        '    'AssignCRpt11Formulas(CrReport, "SaleAgreementDate", "'" & Trim(mSaleAgreementDate) & "'")
        'End If

        'If UCase(mRptFileName) = "PDF_INVOICE_SGST.RPT" Or UCase(mRptFileName) = "PDF_INVOICE_IGST.RPT" Or UCase(mRptFileName) = "PDF_INVOICE_SGST_L.RPT" Or UCase(mRptFileName) = "PDF_INVOICE_IGST_L.RPT" Then
        'mEPCGNo = ""
        'mEPCGDate = ""
        'SqlStr = " SELECT EPCG_NO, EPCG_DATE  " & vbCrLf _
        '    & " FROM DSP_SALEORDER_HDR " & vbCrLf _
        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND AUTO_KEY_SO='" & MainClass.AllowSingleQuote(lblPoNo.Text) & "'" & vbCrLf _
        '    & " AND CUST_PO_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "'" & vbCrLf _
        '    & " AND CUST_PO_DATE=TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SO_STATUS='O'"
        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)

        'If RsTempShip.EOF = False Then
        '    mEPCGNo = IIf(IsDBNull(RsTempShip.Fields("EPCG_NO").Value), "", RsTempShip.Fields("EPCG_NO").Value)
        '    mEPCGDate = VB6.Format(IIf(IsDBNull(RsTempShip.Fields("EPCG_DATE").Value), "", RsTempShip.Fields("EPCG_DATE").Value), "DD/MM/YYYY")
        'End If

        'If mEPCGNo <> "" Then
        '    mEPCGNo = "EPCG License No : " & mEPCGNo & " &  Date : " & mEPCGDate
        '    AssignCRpt11Formulas(CrReport, "EPCGNo", "'" & mEPCGNo & "'")
        'End If


        'End If

        'If Val(lblInvoiceSeq.Text) = 6 Or Val(lblInvoiceSeq.Text) = 7 Then
        '    If chkLUT.CheckState = System.Windows.Forms.CheckState.Checked Then
        '        mLUT = GetLUT((txtBillDate.Text))
        '    Else
        '        mLUT = ""
        '    End If

        '    AssignCRpt11Formulas(CrReport, "LUTNo", "'" & mLUT & "'")
        '    mExpHeading = "SUPPLY MEANT FOR EXPORT ON PAYMENT OF IGST OR SUPPLY MEANT FOR EXPORT UNDER BOND OR LETTER OF UNDERTAKING WITHOUT PAYMENT OF IGST"
        '    'MainClass.AssignCRptFormulas(Report1, "ExpHeading=""" & mExpHeading & """")
        'End If

        'mPayTerms = ""

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


        'Dim mBMPFileName As String = ""
        'mBillNoStr = Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text)
        'mBillNoStr = Replace(mBillNoStr, "/", "_")
        'mBillNoStr = Replace(mBillNoStr, "\", "_")
        'mBMPFileName = RefreshQRCode(LblMKey.Text, mBillNoStr, txtIRNNo.Text)

        'If Not FILEExists(mBMPFileName) Then
        '    mBMPFileName = ""
        'End If

        'AssignCRpt11Formulas(CrReport, "PicLocation", "'" & mBMPFileName & "'")

        Dim mClientLogoPath As String = ""
        If Not FILEExists(PubClientLogoPath) Then
            mClientLogoPath = ""
        Else
            mClientLogoPath = PubClientLogoPath
        End If
        AssignCRpt11Formulas(CrReport, "CompanyLogo", "'" & mClientLogoPath & "'")
        Dim mPDF As Boolean = False

        If mPDF = True Then
            Dim pOutPutFileName As String = ""
            mBillNoStr = Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text)
            mBillNoStr = Replace(mBillNoStr, "/", "_")
            mBillNoStr = Replace(mBillNoStr, "\", "_")

            fPath = mPubBarCodePath & "\ProformaInvoice_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"
            pOutPutFileName = mPubBarCodePath & "\ProformaInvoice_DS_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"

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
    Private Sub ShowExcisePDFReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean, ByRef mInvoicePrintType As String)
        On Error GoTo ErrPart
        Dim crapp As New CRAXDRT.Application
        Dim RsTemp As New ADODB.Recordset
        Dim RS As New ADODB.Recordset

        Dim objRpt As CRAXDRT.Report
        Dim fPath As String


        Dim mAmountInword As String
        Dim SqlStrSub As String
        Dim mDutyInword As String
        Dim SqlStr As String

        Dim RsTempShip As ADODB.Recordset
        Dim mNetAmount As Double
        Dim mNetDuty As Double
        Dim mPrepTime As String
        Dim mRemovalTime As String
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

        Dim mShipToName As String
        Dim mShipToAddress As String
        Dim mShipToCity As String
        Dim mShipToGSTN As String
        Dim mCompanyDetail As String
        Dim mCompanyeMail As String
        Dim mCompanyWebSite As String
        Dim mShipToState As String
        Dim mShipToStateCode As String
        Dim mStateName As String
        Dim mStateCode As String
        Dim mWithInState As String
        Dim mWithInCountry As String
        Dim mPlaceofSupply As String
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

        '    SetCrpt Report1, mMode, 1, mTitle, mSubTitle, , , "Y"				

        objRpt = crapp.OpenReport(PubReportFolderPath & mRptFileName)

        SqlStrSub = " SELECT FIN_PRO_INVOICE_EXP.MKEY, FIN_PRO_INVOICE_EXP.SUBROWNO, FIN_PRO_INVOICE_EXP.EXPPERCENT, FIN_PRO_INVOICE_EXP.AMOUNT, FIN_INTERFACE_MST.COMPANY_CODE, FIN_INTERFACE_MST.NAME" & vbCrLf & " FROM FIN_PRO_INVOICE_EXP, FIN_PRO_INVOICE_HDR, FIN_INTERFACE_MST " & vbCrLf & " WHERE FIN_PRO_INVOICE_EXP.MKEY = FIN_PRO_INVOICE_HDR.MKEY AND FIN_PRO_INVOICE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf & " AND FIN_PRO_INVOICE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

        SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"

        SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

        Call Connect_Report_To_Database(objRpt, RS, mSqlStr, SqlStrSub)
        With objRpt
            Call ClearCRpt8Formulas(objRpt)
            .DiscardSavedData()
            .Database.SetDataSource(RS)
            SetCrpteMail(objRpt, 1, mTitle, mSubTitle)
            .VerifyOnEveryPrint = True '' blnVerifyOnEveryPrint				
        End With

        mStateName = ""
        mStateCode = ""

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mStateName = MasterNo
            mStateCode = GetStateCode(mStateName)
        End If

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mWithInState = MasterNo
        End If

        If mWithInState = "N" Then
            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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

        SqlStr = " SELECT NETVALUE, ITEMVALUE, NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT,INV_PREP_TIME, " & vbCrLf & " SHIPPED_TO_PARTY_CODE, REMOVAL_TIME, OUR_AUTO_KEY_SO, SHIPPED_TO_SAMEPARTY, " & vbCrLf & " IS_DESP_OTHERTHAN_BILL, SHIPPED_FROM_PARTY_CODE, IS_SHIPPTO_EX_WORK" & vbCrLf & " FROM FIN_PRO_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
            mNetDuty = IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)


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


                    AssignCRpt8Formulas(objRpt, "ShipFromName", "'" & mShipFromName & "'")
                    AssignCRpt8Formulas(objRpt, "ShipFromAddress", "'" & mShipFromAddress & "'")
                    AssignCRpt8Formulas(objRpt, "ShipFromCity", "'" & mShipFromCity & "'")
                    AssignCRpt8Formulas(objRpt, "ShipFromState", "'" & mShipFromState & "'")

                End If
            End If

        End If

        AssignCRpt8Formulas(objRpt, "InvoicePrintType", "'" & mInvoicePrintType & "'")
        AssignCRpt8Formulas(objRpt, "CompanyCity", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & "'")
        AssignCRpt8Formulas(objRpt, "CompanyGSTIN", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & "'")

        mJurisdiction = "All Disputes Subject to " & IIf(IsDBNull(RsCompany.Fields("JURISDICTION").Value), "", RsCompany.Fields("JURISDICTION").Value) & " Jurisdiction."

        AssignCRpt8Formulas(objRpt, "COMPANYTINNo", "'" & IIf(IsDBNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value) & "'")
        AssignCRpt8Formulas(objRpt, "COMPANYCINNo", "'" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & "'")

        mCompanyeMail = IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", "e-mail : " & RsCompany.Fields("COMPANY_MAILID").Value)
        mCompanyWebSite = IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", "WebSite : " & RsCompany.Fields("WEBSITE").Value)
        mCompanyDetail = mCompanyeMail & ", " & mCompanyWebSite

        AssignCRpt8Formulas(objRpt, "COMPANYDETAIL", "'" & mCompanyDetail & "'")
        AssignCRpt8Formulas(objRpt, "Jurisdiction", "'" & mJurisdiction & "'")
        AssignCRpt8Formulas(objRpt, "mShipToName", "'" & mShipToName & "'")
        AssignCRpt8Formulas(objRpt, "mShipToAddress", "'" & mShipToAddress & "'")
        AssignCRpt8Formulas(objRpt, "mShipToCity", "'" & mShipToCity & "'")
        AssignCRpt8Formulas(objRpt, "mShipToGSTN", "'" & mShipToGSTN & "'")
        AssignCRpt8Formulas(objRpt, "mShipToState", "'" & mShipToState & "'")
        AssignCRpt8Formulas(objRpt, "mShipToStateCode", "'" & mShipToStateCode & "'")
        AssignCRpt8Formulas(objRpt, "mStateName", "'" & mStateName & "'")
        AssignCRpt8Formulas(objRpt, "mStateCode", "'" & mStateCode & "'")
        AssignCRpt8Formulas(objRpt, "mPlaceofSupply", "'" & mPlaceofSupply & "'")
        '    AssignCRpt8Formulas objRpt, "mServiceName", "'" & Trim(txtServProvided.Text) & "'"				


        '    mPayTerms = ""				
        '    If RsCompany.Fields("COMPANY_CODE").Value = 16 Then				
        '        SqlStr = " SELECT PAYMENT_DESC " & vbCrLf _				
        ''            & " FROM FIN_SUPP_CUST_MST " & vbCrLf _				
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _				
        ''            & " AND SUPP_CUST_NAME='" & txtCustomer.Text & "'"				
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly				
        '				
        '        If RsTemp.EOF = False Then				
        '            mPayTerms = IIf(IsdbNull(RsTemp!PAYMENT_DESC), "", RsTemp!PAYMENT_DESC)				
        '        End If				
        '        AssignCRpt8Formulas objRpt, "PAYTERMS", "'" & mPayTerms & "'"				
        '    End If				

        If IsSubReport = True Then

            If mNetAmount = 0 Then
                mAmountInword = " Zero"
            Else
                mAmountInword = MainClass.RupeesConversion(mNetAmount)
            End If

            mDutyInword = MainClass.RupeesConversion(mNetDuty)

            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                AssignCRpt8Formulas(objRpt, "AmountInWord", "'Rs. Zero'")
                AssignCRpt8Formulas(objRpt, "DutyInword", "'Rs. Zero'")
                AssignCRpt8Formulas(objRpt, "NetAmount", "'0.00'")
            Else
                AssignCRpt8Formulas(objRpt, "AmountInWord", "'" & mAmountInword & "'")
                AssignCRpt8Formulas(objRpt, "NetAmount", "'" & VB6.Format(mNetAmount, "0.00") & "'")
                AssignCRpt8Formulas(objRpt, "DutyInword", "'" & mDutyInword & "'")
            End If
        End If


        '    fPath = mLocalPath & "\TaxInvoice_" & RsCompany.Fields("COMPANY_CODE").Value & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".pdf"				

        With objRpt
            .ExportOptions.FormatType = CRAXDDRT.CRExportFormatType.crEFTPortableDocFormat
            .ExportOptions.DestinationType = CRAXDDRT.CRExportDestinationType.crEDTApplication '' crEDTDiskFile				
            '        .ExportOptions.DiskFileName = fPath				
            .ExportOptions.PDFExportAllPages = True
            .Export(True)
        End With



        Exit Sub
ErrPart:
        'Resume				
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        'Dim Printer As New Printer				
        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim RsTemp As ADODB.Recordset
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

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True,  , "N")


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
        Dim mInvoicePrintType As String
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mPRINTED As String
        Dim mExtraRemarks As String
        Dim mPrintOption As String

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Invoice Cann't be Print.")
            Exit Sub
        End If
        mPrintOption = "I"


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            frmPrintInvoice.OptInvoice.Enabled = True
            frmPrintInvoice.OptInvoiceAnnex.Enabled = True
            frmPrintInvoice.optSubsidiaryChallan.Enabled = True

            frmPrintInvoice.OptInvoice.Visible = True
            frmPrintInvoice.OptInvoiceAnnex.Visible = True
            frmPrintInvoice.optSubsidiaryChallan.Visible = True

            frmPrintInvoice.OptInvoice.Text = "BP"
            frmPrintInvoice.OptInvoiceAnnex.Text = "Automotive"
            frmPrintInvoice.optSubsidiaryChallan.Text = "Development"

            frmPrintInvoice.FraF4.Enabled = False
            frmPrintInvoice.FraF4.Visible = False
            frmPrintInvoice.ShowDialog()

            If G_PrintLedg = False Then
                frmPrintInvoice.Close()
                Exit Sub
            Else
                If frmPrintInvoice.OptInvoice.Checked = True Then
                    mPrintOption = "I"
                ElseIf frmPrintInvoice.OptInvoiceAnnex.Checked = True Then
                    mPrintOption = "A"
                Else
                    mPrintOption = "D"
                End If
            End If
        End If

        Call ReportOnSales(Crystal.DestinationConstants.crptToPrinter, mInvoicePrintType, "N", mPrintOption)


        '    Unload frmPrintInvCopy	
        frmPrintInvoice.Hide()
        frmPrintInvoice.Close()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default


        Exit Sub
ErrPart:
        '    Unload frmPrintInvoice	
        frmPrintInvoice.Hide()
        frmPrintInvoice.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call CalcTots()
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
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

    Private Sub FrmInvoicePerforma_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub SprdExp_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdExp.ButtonClicked
        If pShowCalc = True Then ''FormActive = True Or      If FormActive = True Then				
            SprdExp.Col = ColExpAmt
            SprdExp.Row = eventArgs.row
            If Val(SprdExp.Text) <> 0 Then
                Call CalcTots()
            End If
        End If
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim xIName As String
        Dim SqlStr As String


        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "CUSTOMER_PART_NO", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemDesc
                xIName = .Text
                .Text = ""
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "CUSTOMER_PART_NO",  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = AcName
                Else
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = xIName
                End If
                If MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = MasterNo
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColPartNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColPartNo
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "CUSTOMER_PART_NO", "ITEM_CODE", "ITEM_SHORT_DESC",  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = AcName1

                    .Col = ColItemDesc
                    .Text = AcName2

                    .Col = ColPartNo
                    .Text = AcName

                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then '***ROW DEL. OPTION NOT REQ IN INVOICE				
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                FormatSprdMain(eventArgs.row)
                '            Call DistributeExpInMainGrid				
                '            Call CalcTots				
            End If
        End If

        Call CalcTots()
    End Sub

    'Private Sub SprdMain_LeaveCell(ByVal Col As Long, ByVal Row As Long, ByVal NewCol As Long, ByVal NewRow As Long, Cancel As Boolean)				
    'On Error GoTo ErrPart				
    'Dim RIBBONSGroup As Boolean				
    'Dim xSoNo As String				
    'Dim xICode As String				
    '				
    '    If NewRow = -1 Then Exit Sub				
    '				
    '    SprdMain.Row = SprdMain.ActiveRow				
    '				
    '    Select Case Col				
    '        Case ColQty				
    '            If CheckQty() = True Then				
    '                MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight				
    '                FormatSprdMain SprdMain.MaxRows				
    '            End If				
    '        Case ColRate				
    '            Call CheckRate				
    '    End Select				
    '    Call CalcTots				
    '    Exit Sub				
    'ErrPart:				
    '    MsgBox err.Description				
    'End Sub				

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim xICode As String
        Dim xAcctPostName As String
        If eventArgs.newRow = -1 Then Exit Sub
        Dim mPreviousItemRate As Double
        Dim mItemRate As Double
        Dim mGlassDesc As String

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                SprdMain.Col = ColGlassDescription
                mGlassDesc = xICode & UCase(SprdMain.Text)

                '            If GetValidItem(xICode) = True Then				
                If CheckDuplicateItem(xICode) = False Then
                    If FillGridRow(xICode) = False Then Exit Sub
                    '                    FormatSprdMain Row				
                    '                MainClass.SetFocusToCell SprdMain, Row, ColItemRate				
                End If
'            Else				
'                MainClass.SetFocusToCell SprdMain, Row, ColItemCode				
'            End If				

            Case ColRate
                If CheckItemRate() = True Then
                    SprdMain.Row = SprdMain.ActiveRow

                    SprdMain.Col = ColRate
                    mItemRate = Val(SprdMain.Text)
                    ''Not Change				
                    SprdMain.Row = SprdMain.Row
                    SprdMain.Row2 = SprdMain.Row
                    SprdMain.Col = 1
                    SprdMain.Col2 = SprdMain.MaxCols
                    SprdMain.BlockMode = True
                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    SprdMain.BlockMode = False

                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain(-1)
                End If


        End Select
CalcPart:

        Call CalcTots()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckItemRate() As Boolean
        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColRate
            If Val(.Text) > 0 Then
                CheckItemRate = True
            Else
                MsgInformation("Please Check the Item Price.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColRate)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FillGridRow(ByRef mItemCode As String) As Boolean
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim mHSNCode As String
        Dim mSaleInvTypeCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mInvTypeDesc As String
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mCustomerCode As String = ""
        Dim mMerchantExporter As String = "N"
        Dim mWithInCountry As String = "Y"
        Dim mMRP As String
        Dim pCategoryType As String = ""


        If mItemCode = "" Then Exit Function

        mLocal = "N"
        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustomerCode = Trim(MasterNo)
            End If

            If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER= 'EXPORTER-MERCHANT'") = True Then
                mMerchantExporter = "Y"
            End If

            If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInCountry = MasterNo
            End If

        End If

        mLocal = GetPartyBusinessDetail(mCustomerCode, Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(mCustomerCode, Trim(txtBillTo.Text), "GST_RGN_NO")
        'If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        SqlStr = ""

        SqlStr = " Select INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC,INVMST.ISSUE_UOM, " & vbCrLf _
                & " 0 AS ITEM_RATE,  0 As DISC_PER,CUSTOMER_PART_NO,ITEM_COLOR," & vbCrLf _
                & " INVMST.CATEGORY_CODE, CMST.SALEINVTYPECODE, CMST.PURCHASEINVTYPECODE, INVMST.HSN_CODE, INVMST.MAT_THICHNESS, INVMST.MAT_LEN, INVMST.MAT_WIDTH" & vbCrLf _
                & " FROM INV_ITEM_MST INVMST , INV_GENERAL_MST CMST" & vbCrLf _
                & " WHERE INVMST.COMPANY_CODE=CMST.COMPANY_CODE AND INVMST.CATEGORY_CODE=CMST.GEN_CODE" & vbCrLf _
                & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND INVMST.ITEM_CODE='" & Trim(mItemCode) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)

        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                pCategoryType = GetProductionType(mItemCode)

                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                SprdMain.Col = ColHSNCode

                '            If Left(cboInvType.Text, 1) = "G" Then				
                mHSNCode = GetHSNCode(mItemCode) 'IIf(IsdbNull(!HSN_CODE), "", !HSN_CODE)				
                '            Else				
                '                mHSNCode = GetSACCode(txtServProvided.Text)				
                '            End If				

                SprdMain.Text = mHSNCode

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)

                SprdMain.Col = ColPartNo
                SprdMain.Text = IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)



                'SprdMain.Col = ColActualt
                'SprdMain.Text = IIf(IsDBNull(.Fields("MAT_THICHNESS").Value), "", .Fields("MAT_THICHNESS").Value)

                SprdMain.Col = ColActualHeight
                SprdMain.Text = IIf(IsDBNull(.Fields("MAT_LEN").Value), "", .Fields("MAT_LEN").Value)

                SprdMain.Col = ColActualWidth
                SprdMain.Text = IIf(IsDBNull(.Fields("MAT_WIDTH").Value), "", .Fields("MAT_WIDTH").Value)

                SprdMain.Col = ColMRP
                If Val(SprdMain.Text) = 0 Then
                    mMRP = GetMRPRate((txtBillDate.Text), "RATE", mItemCode, "L")
                Else
                    mMRP = Val(SprdMain.Text)
                End If
                SprdMain.Text = CStr(mMRP)

                SprdMain.Col = ColRate
                If Val(SprdMain.Text) = 0 Then
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), "", .Fields("ITEM_RATE").Value)))
                End If

                If pCategoryType = "S" Then
                    If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, "0") = False Then GoTo ERR1
                Else
                    If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "0", mPartyGSTNo,,,, mMerchantExporter) = False Then GoTo ERR1
                End If


                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")


                MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                FormatSprdMain(-1)

            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Sub CheckRate()
        On Error GoTo ERR1
        With SprdMain

            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Sub

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

    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mSONO As String
        Dim mBillNo As String
        Dim mBillNoPrefix As String
        Dim mBillNoSuffix As String
        Dim mBillDate As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        mBillNoPrefix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(0))
        mBillNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1))

        mBillNoSuffix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(2))
        mBillDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(4))

        txtBillNoPrefix.Text = mBillNoPrefix
        txtBillNo.Text = VB6.Format(mBillNo, "00000000")
        txtBillNoSuffix.Text = mBillNoSuffix
        txtBillDate.Text = VB6.Format(mBillDate, "DD/MM/YYYY")

        txtBillNo_Validating(txtBillNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())


    End Sub

    Public Sub txtBillNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim xMkey As String
        Dim mBillNo As String
        If Trim(txtBillNo.Text) = "" Then GoTo EventExitSub

        txtBillNo.Text = VB6.Format(Val(txtBillNo.Text), "00000000")

        If MODIFYMode = True And RsSaleMain.EOF = False Then xMkey = RsSaleMain.Fields("mKey").Value
        mBillNo = Trim(Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & Trim(txtBillNoSuffix.Text))
        '    mBillNo = "S05135"				
        SqlStr = " SELECT * FROM FIN_PRO_INVOICE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "' " & vbCrLf _
            & " AND BillNo='" & MainClass.AllowSingleQuote(mBillNo) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsSaleMain.EOF = False Then
            Clear1()

            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Proforma Invoice, Use Generate Proforma Invoice Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_PRO_INVOICE_HDR " & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMkey) & "' "
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
        Dim mAutoKeyNo As String '' Double				
        Dim mBillNoSeq As Integer
        Dim mBillNo As String
        Dim mSuppCustCode As String
        Dim mConsingee As String
        Dim mAccountCode As String

        Dim mFREIGHTCHARGES As String

        Dim mItemValue As Double
        Dim mTOTSTAMT As Double
        Dim mTOTCHARGES As Double

        Dim mTOTEXPAMT As Double
        Dim mNETVALUE As Double
        Dim mTotQty As Double

        Dim mFOC As String
        Dim mCancelled As String
        Dim mIsRegdNo As String
        Dim mStockTrf As String


        Dim mStartingNo As Double

        Dim mTOTFREIGHT As Double
        Dim mTOTTAXABLEAMOUNT As Double

        Dim mTCSAMOUNT As Double
        Dim mTCSPER As Double

        Dim mRO As Double
        Dim mSURAmount As Double
        Dim mTotDiscount As Double
        Dim RsTemp As ADODB.Recordset
        Dim mRemarks As String
        Dim mDivisionCode As Double
        Dim mShippedToSame As String
        Dim mShippedToCode As String
        Dim mShippedFromCode As String

        Dim mPartyGSTNo As String
        Dim mSameGSTNo As String

        Dim mTransMode As String
        Dim mDespatchFrom As String
        Dim mShippToExWork As String

        Dim xBillNo As String
        Dim xBillDate As String
        Dim xIsGST As String
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim RsDN As ADODB.Recordset
        Dim mSalePersonCode As String = ""

        Dim mRejDocType As String
        Dim mApplicableDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        'If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        mSuppCustCode = "-1"
        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = "-1"
            MsgBox("Customer Does Not Exist In Master", vbInformation)
            GoTo ErrPart
        End If

        mPartyGSTNo = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        mShippedToSame = IIf(chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")    ''IIf(System.Windows.Forms.CheckState.Checked = CheckState.Checked, "Y", "N")

        If mShippedToSame = "Y" Then
            mShippedToCode = mSuppCustCode
        Else
            If MainClass.ValidateWithMasterTable(txtShippedTo.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mShippedToCode = MasterNo
            End If
        End If

        mSalePersonCode = IIf(cboSalePersonName.Text = "", "", cboSalePersonName.Value)

        mFREIGHTCHARGES = IIf(OptFreight(0).Checked = True, "To Pay", "Paid")

        mItemValue = Val(lblTotItemValue.Text)
        mTOTCHARGES = 0

        mTOTEXPAMT = Val(lblTotExpAmt.Text)
        mNETVALUE = Val(lblNetAmount.Text)


        mTOTFREIGHT = 0


        mTOTTAXABLEAMOUNT = Val(lblTotTaxableAmt.Text)

        mRO = Val(lblRO.Text)
        mTotDiscount = 0
        mSURAmount = 0

        mTCSAMOUNT = Val(lblTCS.Text)
        mTCSPER = Val(lblTCSPercentage.Text)

        mTotQty = Val(lblTotQty.Text)

        '    mFOC = IIf(chkFOC.Value = vbChecked, "Y", "N")				

        mDespatchFrom = IIf(chkDespatchFrom.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mShippToExWork = IIf(chkExWork.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        If mDespatchFrom = "N" Then
            mShippedFromCode = "-1"
        Else
            If MainClass.ValidateWithMasterTable(txtShippedFrom.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mShippedFromCode = MasterNo
            End If
        End If

        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsRegdNo = "N" '' IIf(chkRegDealer.Value = vbChecked, "Y", "N")				


        If Trim(txtBillNo.Text) = "" Then
            mStartingNo = 1
            mBillNoSeq = CInt(AutoGenSeqBillNo(mStartingNo))
        Else
            mBillNoSeq = Val(txtBillNo.Text)
        End If


        txtBillNo.Text = VB6.Format(Val(CStr(mBillNoSeq)), "00000000")
        mBillNo = Trim(Trim(txtBillNoPrefix.Text) & VB6.Format(Val(CStr(mBillNoSeq)), "00000000") & Trim(txtBillNoSuffix.Text))
        mAutoKeyNo = VB6.Format(VB6.Format(Val(CStr(mBillNoSeq)), "00000000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))

        If CheckValidBillDate(mBillNoSeq, mDivisionCode) = False Then GoTo ErrPart

        '    mSACCode = ""				
        '    If Trim(txtServProvided.Text) <> "" Then				
        '        If MainClass.ValidateWithMasterTable(txtServProvided.Text, "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then				
        '            mSACCode = Trim(IIf(IsdbNull(MasterNo), "", MasterNo))				
        '        End If				
        '    End If				


        mTransMode = VB.Left(cboTransmode.Text, 1)

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "FIN_PRO_INVOICE_HDR", (LblMKey.Text), RsSaleMain, "Mkey", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_PRO_INVOICE_DET", (LblMKey.Text), RsSaleDetail, "Mkey", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_PRO_INVOICE_EXP", (LblMKey.Text), RsSaleExp, "Mkey", "M") = False Then GoTo ErrPart
        End If
        SqlStr = ""
        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("FIN_PRO_INVOICE_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo
            LblMKey.Text = nMkey
            SqlStr = "INSERT INTO FIN_PRO_INVOICE_HDR (" & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _
                & " ROWNO, BILLNOPREFIX, " & vbCrLf _
                & " AUTO_KEY_INVOICE, BILLNOSEQ, BILLNOSUFFIX, BILLNO, " & vbCrLf _
                & " INVOICE_DATE, INV_PREP_DATE, INV_PREP_TIME, " & vbCrLf _
                & " CUST_PO_NO, CUST_PO_DATE, " & vbCrLf _
                & " SUPP_CUST_CODE, " & vbCrLf _
                & " DUEDAYSFROM, DUEDAYSTO, " & vbCrLf _
                & " DESPATCHMODE, DOCSTHROUGH, " & vbCrLf _
                & " VEHICLENO, CARRIERS, FREIGHTCHARGES, " & vbCrLf _
                & " REMARKS, ITEMVALUE, " & vbCrLf _
                & " TOTCHARGES, " & vbCrLf _
                & " TOTEXPAMT, NETVALUE, TOTQTY, "

            SqlStr = SqlStr & vbCrLf & " CANCELLED, NARRATION,  " & vbCrLf & " TOTFREIGHT, TOTTAXABLEAMOUNT, " & vbCrLf & " TOTDISCAMOUNT, TotRO, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE,TCSPER, TCSAMOUNT," & vbCrLf & " OUR_AUTO_KEY_SO, OUR_SO_DATE, "

            SqlStr = SqlStr & vbCrLf & " --SHIPPING_NO, SHIPPING_DATE, " & vbCrLf _
                & " --ARE1_NO, ARE1_DATE, " & vbCrLf _
                & " --PORT_CODE, EXPBILLNO, EXPINV_DATE, TOT_EXPORTEXP,EXCHANGE_RATE, " & vbCrLf _
                & " --TOTEXCHANGEVALUE, ADV_LICENSE, DESP_LOCATION, NATURE," & vbCrLf _
                & " --TOTMRPVALUE, TAX_ON_MRP, ABATEMENT_PER, " & vbCrLf _
                & " --TOT_CUSTOMDUTY, TOT_CD_CESS, CD_PER, CD_CESS_PER, BUYER_CODE, CO_BUYER_CODE," & vbCrLf _
                & " --TOTSHECPERCENT, TOTSHECAMOUNT,UPDATE_FROM,ISDUTY_FORGONE, AGT_DUTYFREE_PUR," & vbCrLf _
                & " DIV_CODE, " & vbCrLf & " NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT," & vbCrLf _
                & " SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE," & vbCrLf _
                & " ADV_VNO, ADV_VDATE, ADV_ADJUSTED_AMT, " & vbCrLf & " ADV_CGST_AMT, ADV_SGST_AMT, ADV_IGST_AMT,ADV_ITEM_AMT, " & vbCrLf _
                & " TRANSPORT_MODE,  " & vbCrLf & " --VEHICLE_TYPE, " & vbCrLf & " IS_DESP_OTHERTHAN_BILL, SHIPPED_FROM_PARTY_CODE, " & vbCrLf _
                & " IS_SHIPPTO_EX_WORK,BILL_TO_LOC_ID,SHIP_TO_LOC_ID,SALE_PERSON_CODE" & vbCrLf _
                & " )"

            SqlStr = SqlStr & vbCrLf & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mCurRowNo & ",'" & MainClass.AllowSingleQuote(txtBillNoPrefix.Text) & "', " & vbCrLf & " " & mAutoKeyNo & "," & mBillNoSeq & ", '" & MainClass.AllowSingleQuote(txtBillNoSuffix.Text) & "', '" & MainClass.AllowSingleQuote(mBillNo) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & TxtBillTm.Text & "','HH24:MI')," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtPONo.Text) & "', TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & mSuppCustCode & "'," & vbCrLf & " " & Val(txtCreditDays(0).Text) & ", " & Val(txtCreditDays(1).Text) & ",  " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtMode.Text) & "', '" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "', '" & mFREIGHTCHARGES & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & mItemValue & ", " & vbCrLf & " " & mTOTCHARGES & ",  " & vbCrLf & " " & mTOTEXPAMT & ", " & mNETVALUE & ", " & mTotQty & ", " & vbCrLf & " '" & mCancelled & "', '" & MainClass.AllowSingleQuote(txtNarration.Text) & "', 0,0,0,0, "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & vbCrLf & " " & mTCSPER & "," & mTCSAMOUNT & "," & vbCrLf & " " & Val(lblPoNo.Text) & ", To_DATE('" & VB6.Format(lblSoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), "

            SqlStr = SqlStr & vbCrLf & " " & mDivisionCode & "," & vbCrLf & " " & Val(lblTotCGSTAmount.Text) & "," & Val(lblTotSGSTAmount.Text) & "," & Val(lblTotIGSTAmount.Text) & "," & vbCrLf & " '" & mShippedToSame & "','" & mShippedToCode & "'," & vbCrLf & " '" & Trim(txtAdvVNo.Text) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtAdvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtAdvAdjust.Text) & ", " & vbCrLf _
                & " " & Val(txtAdvCGST.Text) & ", " & Val(txtAdvSGST.Text) & ", " & Val(txtAdvIGST.Text) & ", " & Val(txtItemAdvAdjust.Text) & ", " & vbCrLf _
                & " '" & mTransMode & "', " & vbCrLf & " '" & mDespatchFrom & "', '" & MainClass.AllowSingleQuote(mShippedFromCode) & "'," & vbCrLf _
                & " '" & mShippToExWork & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "', '" & MainClass.AllowSingleQuote(TxtShipTo.Text) & "','" & MainClass.AllowSingleQuote(mSalePersonCode) & "')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE FIN_PRO_INVOICE_HDR SET " & vbCrLf _
                & " BILLNOPREFIX = '" & MainClass.AllowSingleQuote(txtBillNoPrefix.Text) & "'," & vbCrLf _
                & " BILLNOSEQ= " & mBillNoSeq & ", " & vbCrLf & " AUTO_KEY_INVOICE= " & mAutoKeyNo & ", " & vbCrLf _
                & " BILLNOSUFFIX= '" & MainClass.AllowSingleQuote(txtBillNoSuffix.Text) & "'," & vbCrLf _
                & " BILLNO= '" & MainClass.AllowSingleQuote(mBillNo) & "'," & vbCrLf _
                & " INVOICE_DATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " --PRDDate= ''," & vbCrLf & " INV_PREP_DATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " INV_PREP_TIME= TO_DATE('" & TxtBillTm.Text & "','HH24:MI')," & vbCrLf _
                & " CUST_PO_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "', " & vbCrLf _
                & " CUST_PO_DATE= TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " --AMEND_DATE= ''," & vbCrLf & " SUPP_CUST_CODE= '" & mSuppCustCode & "'," & vbCrLf & " --ACCOUNTCODE= '" & mAccountCode & "'"

            SqlStr = SqlStr & vbCrLf & " DUEDAYSFROM= " & Val(txtCreditDays(0).Text) & "," & vbCrLf _
                & " DUEDAYSTO= " & Val(txtCreditDays(1).Text) & ", " & vbCrLf _
                & " DESPATCHMODE= '" & MainClass.AllowSingleQuote(txtMode.Text) & "', " & vbCrLf _
                & " DOCSTHROUGH= '" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "'," & vbCrLf _
                & " VEHICLENO= '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', " & vbCrLf _
                & " CARRIERS=  '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "'," & vbCrLf _
                & " FREIGHTCHARGES= '" & mFREIGHTCHARGES & "',BILL_TO_LOC_ID= '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "', SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote(TxtShipTo.Text) & "',"


            SqlStr = SqlStr & vbCrLf & " REMARKS= '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf & " ITEMVALUE= " & mItemValue & "," & vbCrLf & " TOTCHARGES= " & mTOTCHARGES & "," & vbCrLf & " TOTEXPAMT= " & mTOTEXPAMT & "," & vbCrLf & " NETVALUE= " & mNETVALUE & "," & vbCrLf & " TOTQTY= " & mTotQty & ","


            SqlStr = SqlStr & vbCrLf & " TOTFREIGHT=" & mTOTFREIGHT & "," & vbCrLf & " TOTTAXABLEAMOUNT=" & mTOTTAXABLEAMOUNT & "," & vbCrLf & " CANCELLED= '" & mCancelled & "'," & vbCrLf & " NARRATION= '" & MainClass.AllowSingleQuote(txtNarration.Text) & "'," & vbCrLf & " TOTDISCAMOUNT=" & mTotDiscount & ", " & vbCrLf & " TotRO=" & mRO & ", " & vbCrLf & " TCSAMOUNT='" & mTCSAMOUNT & "', "

            SqlStr = SqlStr & vbCrLf & " TCSPER='" & mTCSPER & "', " & vbCrLf & " OUR_AUTO_KEY_SO=" & Val(lblPoNo.Text) & ", " & vbCrLf _
                & " OUR_SO_DATE=TO_DATE('" & VB6.Format(lblSoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),"

            SqlStr = SqlStr & vbCrLf & " UPDATE_FROM='H',"

            SqlStr = SqlStr & vbCrLf & " ADV_VNO = '" & Trim(txtAdvVNo.Text) & "'," & vbCrLf _
                & " ADV_VDATE = TO_DATE('" & VB6.Format(txtAdvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " ADV_ADJUSTED_AMT = " & Val(txtAdvAdjust.Text) & ", " & vbCrLf _
                & " ADV_CGST_AMT = " & Val(txtAdvCGST.Text) & ", " & vbCrLf & " ADV_SGST_AMT = " & Val(txtAdvSGST.Text) & ", " & vbCrLf & " ADV_IGST_AMT = " & Val(txtAdvIGST.Text) & ", " & vbCrLf & " ADV_ITEM_AMT = " & Val(txtItemAdvAdjust.Text) & ", "

            SqlStr = SqlStr & vbCrLf & " DIV_CODE=" & mDivisionCode & ", " & vbCrLf & " NETCGST_AMOUNT=" & Val(lblTotCGSTAmount.Text) & ", NETSGST_AMOUNT=" & Val(lblTotSGSTAmount.Text) & ", NETIGST_AMOUNT=" & Val(lblTotIGSTAmount.Text) & ", " & vbCrLf & " SHIPPED_TO_SAMEPARTY='" & mShippedToSame & "', SHIPPED_TO_PARTY_CODE='" & mShippedToCode & "', "

            SqlStr = SqlStr & vbCrLf & " TRANSPORT_MODE='" & mTransMode & "'," & vbCrLf _
                & " IS_DESP_OTHERTHAN_BILL='" & mDespatchFrom & "'," & vbCrLf _
                & " SHIPPED_FROM_PARTY_CODE='" & MainClass.AllowSingleQuote(mShippedFromCode) & "'," & vbCrLf _
                & " IS_SHIPPTO_EX_WORK='" & mShippToExWork & "',SALE_PERSON_CODE = '" & MainClass.AllowSingleQuote(mSalePersonCode) & "'"

            SqlStr = SqlStr & vbCrLf & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")

        If UpdateDetail1(mAutoKeyNo, mBillNo, VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), mSuppCustCode, mAccountCode, mShippedToSame, mShippedToCode, mDivisionCode, mSameGSTNo) = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        '    Resume				
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''				
        RsSaleMain.Requery() ''.Refresh				
        RsSaleDetail.Requery() ''.Refresh				
        If Err.Description = "" Then Exit Function
        'If Err.Number = -2147217900 Then
        '    ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        'Else
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'End If
        ''    Resume				
    End Function
    Private Function CheckValidBillDate(ByRef pBillNoSeq As Integer, ByRef mDivisionCode As Double) As Object
        On Error GoTo CheckERR
        Dim SqlStr As String
        Dim mRsCheck1 As ADODB.Recordset
        Dim mRsCheck2 As ADODB.Recordset
        Dim mBackBillDate As String
        Dim mMaxInvStrfNo As Integer

        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset

        CheckValidBillDate = True

        If txtBillNo.Text = "00001" Then Exit Function

        mSeparateSeries = IIf(IsDBNull(RsCompany.Fields("SEPARATE_INV_SERIES").Value), "N", RsCompany.Fields("SEPARATE_INV_SERIES").Value)

        SqlStr = "SELECT MAX(INVOICE_DATE)" & vbCrLf & " FROM FIN_PRO_INVOICE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BillNoSeq<" & Val(CStr(pBillNoSeq)) & ""

        If mSeparateSeries = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDBNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(INVOICE_DATE)" & " FROM FIN_PRO_INVOICE_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BillNoSeq>" & Val(CStr(pBillNoSeq)) & ""

        If mSeparateSeries = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        End If

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
    Private Function AutoGenSeqBillNo(ByRef pStartingSNo As Double) As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsSaleMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Integer
        Dim mStartingSNo As Double
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset
        Dim xFYear As Integer

        SqlStr = ""

        xFYear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))

        mStartingSNo = CDbl(xFYear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "0") & VB6.Format(pStartingSNo, "00000"))


        SqlStr = ""


        SqlStr = "SELECT Max(BILLNOSEQ)  FROM FIN_PRO_INVOICE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND INVOICE_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSaleMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
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
        '    Resume				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateDetail1(ByRef pAutoKey As String, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pSuppCustCode As String, ByRef pAccountCode As String, ByRef pShipToSameParty As String, ByRef pShipToSuppCustCode As String, ByRef pDivCode As Double, ByRef mSameGSTNo As String) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset
        Dim I As Integer
        Dim mItemCode As String
        Dim mPartNo As String
        Dim mItemDesc As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mAmount As Double

        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim RsTemp As ADODB.Recordset
        Dim mHSNCode As String
        Dim mPOS As String
        Dim mState As String
        Dim mGoodsServices As String
        Dim mTaxableAmount As Double

        Dim mGlassDescription As String
        Dim mActualHeightInch As Double
        Dim mActualWidthInch As Double
        Dim mPackQty As Double

        Dim mActualHeight As Double
        Dim mActualWidth As Double
        Dim mChargeableHeight As Double
        Dim mChargeableWidth As Double
        Dim mArea As Double
        Dim mAreaRate As Double
        Dim mGlassDevelopmentRate As Double
        Dim mDieDevelopmentRate As Double
        Dim mDiscountRate As Double
        Dim mMRP As Double


        PubDBCn.Execute("Delete From FIN_PRO_INVOICE_DET Where Mkey='" & LblMKey.Text & "'")

        mPOS = ""
        If pShipToSameParty = "N" Then
            If MainClass.ValidateWithMasterTable(pShipToSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                mState = MasterNo
                If MainClass.ValidateWithMasterTable(mState, "NAME", "STATE_CODE", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mPOS = MasterNo
                End If
            End If
        End If

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColPartNo
                mPartNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                    mItemDesc = MainClass.AllowSingleQuote(mItemDesc)
                Else
                    mItemDesc = MainClass.AllowSingleQuote(.Text)
                End If

                .Col = ColHSNCode
                mHSNCode = Trim(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColMRP
                mMRP = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColDiscRate
                mDiscountRate = Val(.Text)

                .Col = ColAmount
                mAmount = Val(.Text)

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

                .Col = ColGlassDescription
                mGlassDescription = MainClass.AllowSingleQuote(.Text)

                .Col = ColActualHeightInch
                mActualHeightInch = Val(.Text)

                .Col = ColActualWidthInch
                mActualWidthInch = Val(.Text)

                .Col = ColActualWidthInch

                .Col = ColActualHeight
                mActualHeight = Val(.Text)

                .Col = ColActualWidth
                mActualWidth = Val(.Text)

                .Col = ColChargeableHeight
                mChargeableHeight = Val(.Text)

                .Col = ColChargeableWidth
                mChargeableWidth = Val(.Text)

                .Col = ColArea
                mArea = Val(.Text)

                .Col = ColAreaRate
                mAreaRate = Val(.Text)

                .Col = ColGlassDevelopmentRate
                mGlassDevelopmentRate = Val(.Text)

                .Col = ColDieDevelopmentRate
                mDieDevelopmentRate = Val(.Text)

                .Col = ColPacketQty
                mPackQty = Val(.Text)

                SqlStr = ""

                If mItemCode <> "" And mQty > 0 Then
                    SqlStr = " INSERT INTO FIN_PRO_INVOICE_DET ( " & vbCrLf _
                        & " MKEY , AUTO_KEY_INVOICE, SUBROWNO, " & vbCrLf _
                        & " ITEM_CODE , ITEM_DESC, HSNCODE, CUSTOMER_PART_NO,ITEM_QTY, " & vbCrLf _
                        & " ITEM_UOM , ITEM_RATE, ITEM_AMT, GSTABLE_AMT," & vbCrLf _
                        & " COMPANY_CODE," & vbCrLf _
                        & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
                        & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, " & vbCrLf _
                        & " GLASS_DESC, ACTUAL_HEIGHT, ACTUAL_WIDTH," & vbCrLf _
                        & " CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH, GLASS_AREA," & vbCrLf _
                        & " AREA_RATE, GLASS_DEVELOPMENT_RATE, DIE_DEVELOPMENT_RATE, ACTUAL_HEIGHT_INCH, " & vbCrLf _
                        & " ACTUAL_WIDTH_INCH, PACK_QTY,DISC_RATE,ITEM_MRP" & vbCrLf _
                        & " ) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ('" & LblMKey.Text & "'," & pAutoKey & ", " & I & ", " & vbCrLf _
                        & " '" & mItemCode & "','" & mItemDesc & "', '" & mHSNCode & "', '" & mPartNo & "', " & mQty & ", " & vbCrLf _
                        & " '" & mUnit & "'," & mRate & "," & mAmount & ", " & mTaxableAmount & "," & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & "," & vbCrLf _
                        & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ", " & vbCrLf _
                        & " '" & mGlassDescription & "', " & mActualHeight & ", " & mActualWidth & ", " & vbCrLf _
                        & " " & mChargeableHeight & ", " & mChargeableWidth & ", " & mArea & "," & vbCrLf _
                        & " " & mAreaRate & ", " & mGlassDevelopmentRate & ", " & mDieDevelopmentRate & ", " & mActualHeightInch & ", " & vbCrLf _
                        & " " & mActualWidthInch & ", " & mPackQty & "," & mDiscountRate & "," & mMRP & "" & vbCrLf _
                        & " ) "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        UpdateDetail1 = True
        UpdateDetail1 = UpdateSaleExp1()
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Function

    Private Function UpdateSaleExp1() As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim mExpCode As Integer
        Dim mPerCent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim mDutyForgone As String

        PubDBCn.Execute("Delete From FIN_PRO_INVOICE_EXP Where Mkey='" & LblMKey.Text & "'")
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
                mPerCent = Val(.Text)

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
                    SqlStr = "Insert Into  FIN_PRO_INVOICE_EXP (MKEY,SUBROWNO, " & vbCrLf & "EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DUTYFORGONE) " & vbCrLf & "Values ('" & LblMKey.Text & "'," & I & ", " & vbCrLf & "" & mExpCode & "," & mPerCent & "," & mExpAmount & "," & mCalcOn & ",'" & mRO & "','" & mDutyForgone & "')"
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
        Dim mIsWithinState As String
        Dim mIsWithinCountry As String
        Dim mDespType As String
        Dim mItemCode As String
        Dim mUOM As String
        Dim mItemRate As Double
        Dim mCurrentTime As String
        Dim mInvGenTimeFrom As String
        Dim mInvGenTimeTo As String
        Dim mInvoiceType As String
        Dim mInvoiceTypeName As String
        Dim SORate As Double
        Dim mHSNCode As String
        Dim mInterUnit As String
        Dim mGSTRegd As String
        'Dim mItemCode As String				
        Dim mHSNMstCode As String
        Dim mInvPrefix As String
        Dim mCompanyGSTNo As String
        Dim mCustomerGSTNo As String
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

        mRejDocType = IIf(IsDBNull(RsCompany.Fields("REJECTION_DOCTYPE").Value), "D", RsCompany.Fields("REJECTION_DOCTYPE").Value)
        mApplicableDate = IIf(IsDBNull(RsCompany.Fields("REJ_APPLICABLEDATE").Value), "", RsCompany.Fields("REJ_APPLICABLEDATE").Value)

        FieldsVarification = True


        '     SqlStr = SqlStr & vbCrLf & " INV_GENERATE_24_HOURS,INV_GENERATE_FROM_TM,INV_GENERATE_TO_TM"				

        If CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then
            MsgBox("Bill Date Cann't be less than GST Applicable date.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        mInvPrefix = IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value)

        If mInvPrefix = "" Then
            MsgBox("Invoice Prefix is not Define, so cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

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

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Customer Does Not Exist In Master", vbInformation)
            'txtCustomer.SetFocus
            FieldsVarification = False
            Exit Function
        Else
            mCustomerCode = Trim(MasterNo)
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

        '    If MODIFYMode = True Then				
        '        If RsSaleMain!ISTCSPAID = "Y" And PubSuperUser <> "S" Then				
        '            MsgInformation ("TCS Challan made against this invoice So cann't be modified")				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '    End If				

        If MODIFYMode = True And txtBillNo.Text = "" Then
            MsgInformation("Bill No. is Blank")
            FieldsVarification = False
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

        If MainClass.GetUserCanModify(txtBillDate.Text) = False Then
            MsgBox("You Have Not Rights to Add or Modify back Voucher", vbInformation)
            FieldsVarification = False
            Exit Function
        End If


        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgInformation("Customer Name is not a Supplier or Customer Category. Cannot Save")
            If txtCustomer.Enabled = True Then txtCustomer.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mIsWithinState = IIf(IsDBNull(MasterNo), "N", MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = IIf(IsDBNull(MasterNo), "N", MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mIsWithinCountry = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mGSTRegd = IIf(IsDBNull(MasterNo), "N", MasterNo)
        End If
        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)

        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerGSTNo = IIf(IsDBNull(MasterNo), "", MasterNo)
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Cannot Be Blank", MsgBoxStyle.Information)
            ' txtCustomer.SetFocus				
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCustomer.Text) = "" Then
            MsgBox("Customer Cannot Be Blank", MsgBoxStyle.Information)
            ' txtCustomer.SetFocus				
            FieldsVarification = False
            Exit Function
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

        '    If ADDMode = True Then				
        '        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='C'") = True Then				
        '            MsgBox "Customer Master is Closed, So cann't be saved", vbInformation				
        '            FieldsVarification = False				
        '            If txtBillNo.Enabled = True Then txtBillNo.SetFocus				
        '            Exit Function				
        '        End If				
        '				
        '        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOP_INVOICE='Y'") = True Then				
        '            MsgBox "Cann't Make Invoice For Such Customer, So cann't be saved", vbInformation				
        '            FieldsVarification = False				
        '            If txtBillNo.Enabled = True Then txtBillNo.SetFocus				
        '            Exit Function				
        '        End If				
        '    End If				

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
        '    If Trim(cboSalePersonName.Text) = "" Then
        '        MsgInformation("Sale Person Name is Blank")
        '        TabMain.SelectedIndex = 1
        '        cboSalePersonName.Focus()
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If

        Dim mShippedCustomerCode As String = ""
        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtShippedTo.Text) = "" Then
                MsgInformation("Please Select Shipped To Supplier Name. Cannot Save")
                FieldsVarification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(txtShippedTo.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Shipped To Supplier Name. Cannot Save")
                If txtShippedTo.Enabled = True Then txtShippedTo.Focus()
                FieldsVarification = False
                Exit Function
            Else
                mShippedCustomerCode = MasterNo
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
        Else
            mShippedCustomerCode = mCustomerCode
        End If



        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function

        'If MainClass.ValidDataInGrid(SprdMain, ColMRP, "N", "Please Check MRP.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColRate, "N", "Please Check Rate.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColAmount, "N", "Please Check Amount.") = False Then FieldsVarification = False : Exit Function


        If chkDespatchFrom.CheckState = System.Windows.Forms.CheckState.Checked Then
            If Trim(txtShippedFrom.Text) = "" Then
                MsgBox("Despatch From Address Cann't be blank.", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtShippedFrom.Enabled = True Then txtShippedFrom.Focus()
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(txtShippedFrom.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Shipped From Supplier Name. Cannot Save")
                If txtShippedFrom.Enabled = True Then txtShippedFrom.Focus()
                FieldsVarification = False
                Exit Function
            End If

        End If


        With SprdMain
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
                    If MainClass.ValidateWithMasterTable(Trim(mItemCode), "ITEM_CODE", "HSN_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mHSNMstCode = Trim(IIf(IsDBNull(MasterNo), "", MasterNo))
                        If mHSNMstCode <> Trim(mHSNCode) Then
                            MsgBox("Please Check HSN Code for Item Code : " & Trim(.Text))
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    .Col = ColDiscRate
                    If Val(.Text) > 100 Then
                        MsgBox("Discount % Cann't be Greater Than 100 for Item Code : " & Trim(.Text))
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            Next
        End With


GotoNextRowSupp:


        '    If mIsWithinCountry = "N" Then				
        '				
        '        If Trim(txtBuyerName.Text) = "" Then				
        '            If MsgQuestion("You not Defined Buyer. Do You Want to Continue ...") = vbNo Then				
        '                FieldsVarification = False				
        '                Exit Function				
        '            End If				
        '        End If				
        '				
        '        If Trim(txtShippingNo.Text) = "" Then				
        '            MsgInformation ("Shipping No cann't be blank. Cann't be Saved.")				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '				
        '        If Trim(txtShippingDate.Text) = "" Then				
        '            MsgInformation ("Shipping Date cann't be blank. Cann't be Saved.")				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '				
        '        If Trim(txtARE1No.Text) = "" Then				
        '            MsgInformation ("ARE1 No cann't be blank. Cann't be Saved.")				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '				
        '        If Trim(txtARE1Date.Text) = "" Then				
        '            MsgInformation ("ARE1 Date cann't be blank. Cann't be Saved.")				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '				
        '        If Trim(txtPortCode.Text) = "" Then				
        '            MsgInformation ("Port Code cann't be blank. Cann't be Saved.")				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '				
        '        If Trim(txtExportBillNo.Text) = "" Then				
        '            MsgInformation ("Export Invoice No cann't be blank. Cann't be Saved.")				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '				
        '        If Trim(txtExportBillDate.Text) = "" Then				
        '            MsgInformation ("Export Invoice Date cann't be blank. Cann't be Saved.")				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '				
        '        If Val(txtExchangeRate.Text) = 0 Then				
        '            MsgInformation ("Exchange Rate cann't be blank. Cann't be Saved.")				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '				
        '    End If				

        '    If Val(txtAdvBal.Text) > 0 And Val(txtAdvAdjust.Text) = 0 Then				
        '        If MsgQuestion("Customer has advance Payment, Want to adjust with this voucher.") = vbYes Then				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '    End If				
        '				
        '    If Val(txtAdvBal.Text) > 0 Then				
        '        If Val(txtAdvBal.Text) < Val(txtAdvAdjust.Text) Then				
        '            MsgBox "Advance Balance is Less than Advance Adjusted, So cann't be Saved.", vbInformation				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '    End If				
        '				
        '    If Val(txtAdvCGST.Text) > 0 Then				
        '        If Val(txtAdvCGST.Text) > Val(txtAdvCGSTBal.Text) Then				
        '            MsgBox "CGST Advance is Greater Than Balance CGST Advance Value, So cann't be Saved.", vbInformation				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '        If Val(txtAdvCGST.Text) <> Val(lblTotCGSTAmount.Caption) Then				
        '            MsgBox "CGST Advance is not Match with CGST Advance Value, So cann't be Saved.", vbInformation				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '    End If				

        '    If Val(txtAdvSGST.Text) > 0 Then				
        '        If Val(txtAdvSGST.Text) > Val(txtAdvSGSTBal.Text) Then				
        '            MsgBox "SGST Advance is Greater Than Balance SGST Advance Value, So cann't be Saved.", vbInformation				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '        If Val(txtAdvSGST.Text) <> Val(lblTotSGSTAmount.Caption) Then				
        '            MsgBox "SGST Advance is not Match with SGST Advance Value, So cann't be Saved.", vbInformation				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '    End If				
        '				
        '    If Val(txtAdvIGST.Text) > 0 Then				
        '        If Val(txtAdvIGST.Text) > Val(txtAdvIGSTBal.Text) Then				
        '            MsgBox "IGST Advance is Greater Than Balance IGST Advance Value, So cann't be Saved.", vbInformation				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '        If Val(txtAdvIGST.Text) <> Val(lblTotIGSTAmount.Caption) Then				
        '            MsgBox "IGST Advance is not Match with IGST Advance Value, So cann't be Saved.", vbInformation				
        '            FieldsVarification = False				
        '            Exit Function				
        '        End If				
        '    End If				


        mPinCode = ""
        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_PIN", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPinCode = MasterNo
        End If

        If Val(mPinCode) = 0 Then
            MsgBox("Party's PinCode is not defined Correct in Master, So cann't be Saved.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmInvoicePerforma_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Proforma Invoice"

        SqlStr = ""
        SqlStr = "Select * from FIN_PRO_INVOICE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from FIN_PRO_INVOICE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from FIN_PRO_INVOICE_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleExp, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mCustomerCode = CStr(-1)


        cboTransmode.Items.Clear()
        cboTransmode.Items.Add("1. Road")
        cboTransmode.Items.Add("2. Rail")
        cboTransmode.Items.Add("3. Air")
        cboTransmode.Items.Add("4. Ship")
        cboTransmode.SelectedIndex = 0

        cboCalcOn.Items.Clear()
        cboCalcOn.Items.Add("1. Same As Actual")
        cboCalcOn.Items.Add("2. Next 20 Even")
        cboCalcOn.Items.Add("3. Plus 20 mm")
        cboCalcOn.Items.Add("4. Plus 30 mm")
        cboCalcOn.Items.Add("5. Plus 40 mm")
        cboCalcOn.Items.Add("6. Plus Next 20 Even")
        cboCalcOn.SelectedIndex = -1

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
        Dim SqlStr As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)


        SqlStr = ""

        SqlStr = "SELECT BILLNOPREFIX,TO_CHAR(BILLNOSEQ),BILLNOSUFFIX, " & vbCrLf _
            & " BILLNO,INVOICE_DATE  AS BILLDATE, TO_CHAR(INV_PREP_TIME,'HH24:MI') AS BILLTIME, " & vbCrLf _
            & " CUST_PO_NO AS PONO, " & vbCrLf _
            & " CUST_PO_DATE AS PODATE, " & vbCrLf _
            & " A.SUPP_CUST_NAME AS CUSTOMER, " & vbCrLf _
            & " ITEMVALUE, TOTQTY, NETVALUE , BILL_TO_LOC_ID, SHIP_TO_LOC_ID, REMARKS, SALE_PERSON_CODE, "

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            SqlStr = SqlStr & vbCrLf & "EMP.NAME"
        Else
            SqlStr = SqlStr & vbCrLf & "EMP.EMP_NAME NAME"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_PRO_INVOICE_HDR, FIN_SUPP_CUST_MST A, "

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            SqlStr = SqlStr & vbCrLf & "FIN_SALESPERSON_MST EMP"
        Else
            SqlStr = SqlStr & vbCrLf & "PAY_EMPLOYEE_MST EMP"         '' WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " FIN_PRO_INVOICE_HDR.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
            & " AND FIN_PRO_INVOICE_HDR.SUPP_CUST_CODE=A.SUPP_CUST_CODE "

        '    SqlStr = SqlStr & vbCrLf & " AND FIN_PRO_INVOICE_HDR.INVOICE_DATE>='" & vb6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "'"				

        SqlStr = SqlStr & vbCrLf _
            & " AND FIN_PRO_INVOICE_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And FIN_PRO_INVOICE_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            SqlStr = SqlStr & vbCrLf & " AND  SALE_PERSON_CODE = EMP.CODE(+)  "
        Else
            SqlStr = SqlStr & vbCrLf & " AND  SALE_PERSON_CODE = EMP.EMP_CODE(+)  "     '' WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " Order by BILLDATE,BillNo"

        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))

        ClearGroupFromUltraGrid(UltraGrid1)
        ClearFilterFromUltraGrid(UltraGrid1)

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader("S")


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
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Bill No Prefix"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Bill No Suffix"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Bill Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Bill Time"


            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Customer PO NO"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Customer PO Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Customer Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Item Value"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "Total Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "Net Value"
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Header.Caption = "Bill To Location"
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Header.Caption = "Ship To Location"
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Header.Caption = "Remarks"

            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Header.Caption = "Sales Person Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Header.Caption = "Sales Person Name"


            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 400
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Width = 200

            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Width = 200

            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Hidden = True


            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
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
        '    .set_ColWidth(1, 0)
        '    .set_ColWidth(2, 0)
        '    .set_ColWidth(3, 0)
        '    .set_ColWidth(4, 1200)

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
            .Col = ColExpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColExpName, 28)
            .TypeEditMultiLine = False

            .Col = ColExpPercent
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = 0#
            .TypeFloatMax = 99.999
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpPercent, 6)
            .TypeEditMultiLine = False

            .Col = ColExpAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpAmt, 8)
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
            .ColHidden = IIf(RsCompany.Fields("COMPANY_CODE").Value = 5 Or RsCompany.Fields("COMPANY_CODE").Value = 1, False, True)

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
        Dim cntCol As Long

        mDivCode = -1
        If Trim(cboDivision.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivCode = Val(MasterNo)
            End If
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

            .Col = ColGlassDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("GLASS_DESC").DefinedSize ''				
            .set_ColWidth(.Col, 20)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            For cntCol = ColActualWidthInch To ColChargeableHeight
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 9)
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Next

            .Col = ColArea
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColAreaRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            For cntCol = ColGlassDevelopmentRate To ColDieDevelopmentRate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 9)
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Next


            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsSaleDetail.Fields("ITEM_UOM").DefinedSize ''				
            .set_ColWidth(.Col, 5)

            .Col = ColPacketQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104, False, True)


            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
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


            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            .Col = ColDiscRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(.Col, 5)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
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

            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSaleDetail.Fields("SGST_PER").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)

            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSaleDetail.Fields("IGST_PER").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)

            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(.Col, 9)

            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(.Col, 9)

            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(.Col, 9)

            .ColsFrozen = ColItemDesc

        End With


        '    If mDivCode = 6 Then				
        '        MainClass.UnProtectCell SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColAmount				
        '        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemCode				
        '        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColQty				
        '    Else				
        '        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColItemCode, IIf(PubUserID = "G0416", ColUnit, ColQty) ''ColQty '				
        '    End If				


        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartNo, ColUnit)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColArea, ColArea)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRate, ColRate)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColIGSTAmount)


        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub

ERR1:
        If Err.Number = -2147418113 Then RsSaleDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckDuplicateItem(ByRef mItemCode As String) As Boolean
        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        Dim pItemCode As String

        If mItemCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                pItemCode = UCase(.Text)

                '.Col = ColItemCode
                'pGlassDesc = pGlassDesc & UCase(.Text)

                If pItemCode = UCase(mItemCode) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        SprdMain.Refresh()
    End Sub


    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsSaleMain

            txtBillNoPrefix.MaxLength = .Fields("BillNoPrefix").DefinedSize ''				
            txtBillNo.MaxLength = .Fields("AUTO_KEY_INVOICE").Precision ''				
            txtBillNoSuffix.MaxLength = .Fields("BillNoSuffix").DefinedSize ''				
            txtBillDate.MaxLength = 10
            TxtBillTm.MaxLength = 5

            txtCustomer.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

            txtCreditDays(0).MaxLength = .Fields("DUEDAYSFROM").Precision ''				
            txtCreditDays(1).MaxLength = .Fields("DUEDAYSTO").Precision ''				



            txtRemarks.MaxLength = .Fields("Remarks").DefinedSize ''				
            txtNarration.MaxLength = .Fields("NARRATION").DefinedSize ''				
            txtCarriers.MaxLength = .Fields("CARRIERS").DefinedSize ''				
            txtVehicle.MaxLength = .Fields("VehicleNo").DefinedSize ''				

            txtDocsThru.MaxLength = .Fields("DocsThrough").DefinedSize ''				
            txtMode.MaxLength = .Fields("DespatchMode").DefinedSize ''				

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
        Dim RsMisc As ADODB.Recordset
        Dim mTaxOnMRP As String

        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mShippedToCode As String
        Dim mShippedToName As String

        Dim mShippedFromCode As String
        Dim mShippedFromName As String

        Dim mBillNo As String
        Dim mBalCGST As Double
        Dim mBalSGST As Double
        Dim mBalIGST As Double
        Dim mTransMode As Integer
        Dim mVehicleType As String


        pShowCalc = False
        With RsSaleMain
            If Not .EOF Then

                LblMKey.Text = .Fields("mKey").Value



                '''***				
                lblPoNo.Text = IIf(IsDBNull(.Fields("OUR_AUTO_KEY_SO").Value), "", .Fields("OUR_AUTO_KEY_SO").Value)
                lblSoDate.Text = IIf(IsDBNull(.Fields("OUR_SO_DATE").Value), "", .Fields("OUR_SO_DATE").Value)

                mBillNo = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)

                txtBillNoPrefix.Text = IIf(IsDBNull(.Fields("BILLNOPREFIX").Value), "", .Fields("BILLNOPREFIX").Value)
                txtBillNo.Text = VB6.Format(IIf(IsDBNull(.Fields("BILLNOSEQ").Value), "", .Fields("BILLNOSEQ").Value), "00000000")
                txtBillNoSuffix.Text = IIf(IsDBNull(.Fields("BILLNOSUFFIX").Value), "", .Fields("BILLNOSUFFIX").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                '            txtProddate = Format(IIf(IsdbNull(!PRDDate), "", !PRDDate), "DD/MM/YYYY")				
                TxtBillTm.Text = VB6.Format(IIf(IsDBNull(.Fields("INV_PREP_TIME").Value), "", .Fields("INV_PREP_TIME").Value), "HH:MM")



                If MainClass.ValidateWithMasterTable(.Fields("SUPP_CUST_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustomer.Text = MasterNo
                End If


                mCustomerCode = .Fields("SUPP_CUST_CODE").Value

                chkShipTo.CheckState = IIf(.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mShippedToCode = IIf(IsDBNull(.Fields("SHIPPED_TO_PARTY_CODE").Value), -1, .Fields("SHIPPED_TO_PARTY_CODE").Value)
                mShippedToName = ""
                If MainClass.ValidateWithMasterTable(mShippedToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mShippedToName = MasterNo
                End If

                txtShippedTo.Text = mShippedToName


                txtCreditDays(0).Text = IIf(IsDBNull(.Fields("DUEDAYSFROM").Value), "", .Fields("DUEDAYSFROM").Value)
                txtCreditDays(1).Text = IIf(IsDBNull(.Fields("DUEDAYSTO").Value), "", .Fields("DUEDAYSTO").Value)

                chkCancelled.CheckState = IIf(.Fields("Cancelled").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                If PubUserID = "G0416" Then
                    chkCancelled.Enabled = IIf(.Fields("Cancelled").Value = "Y", False, True)
                Else
                    chkCancelled.Enabled = False ''IIf(!Cancelled = "Y", False, True)				
                End If

                lblTotQty.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.000")
                lblTotItemValue.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                '            lblTotED.Caption = Format(IIf(IsdbNull(!TOTEDAMOUNT), 0, !TOTEDAMOUNT), "0.00")				
                '            lblTotST.Caption = Format(IIf(IsdbNull(!TOTSTAMT), 0, !TOTSTAMT), "0.00")				
                lblNetAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")

                '            lblEDUAmount.Caption = Format(IIf(IsdbNull(!TOTEDUAMOUNT), 0, !TOTEDUAMOUNT), "0.00")				
                '            lblEDUPercent.Caption = Format(IIf(IsdbNull(!TOTEDUPERCENT), 0, !TOTEDUPERCENT), "0.00")				

                '            lblSHECAmount.Caption = Format(IIf(IsdbNull(!TOTSHECAMOUNT), 0, !TOTSHECAMOUNT), "0.00")				
                '            lblSHECPercent.Caption = Format(IIf(IsdbNull(!TOTSHECPERCENT), 0, !TOTSHECPERCENT), "0.00")				

                '            lblServiceAmount.Caption = Format(IIf(IsdbNull(!TOTSERVICEAMOUNT), 0, !TOTSERVICEAMOUNT), "0.00")				
                '            lblServicePercentage.Caption = Format(IIf(IsdbNull(!TOTSERVICEPERCENT), 0, !TOTSERVICEPERCENT), "0.00")				



                txtRemarks.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtNarration.Text = IIf(IsDBNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)
                txtCarriers.Text = IIf(IsDBNull(.Fields("CARRIERS").Value), "", .Fields("CARRIERS").Value)

                txtVehicle.Text = IIf(IsDBNull(.Fields("VEHICLENO").Value), "", .Fields("VEHICLENO").Value)

                txtDocsThru.Text = IIf(IsDBNull(.Fields("DOCSTHROUGH").Value), "", .Fields("DOCSTHROUGH").Value)
                txtMode.Text = IIf(IsDBNull(.Fields("DESPATCHMODE").Value), "", .Fields("DESPATCHMODE").Value)

                mTransMode = IIf(IsDBNull(.Fields("TRANSPORT_MODE").Value), 0, VB.Left(.Fields("TRANSPORT_MODE").Value, 1))
                cboTransmode.SelectedIndex = mTransMode - 1


                mVehicleType = IIf(IsDBNull(.Fields("VEHICLE_TYPE").Value), "", .Fields("VEHICLE_TYPE").Value)



                If .Fields("FREIGHTCHARGES").Value = "To Pay" Then
                    OptFreight(0).Checked = True
                Else
                    OptFreight(1).Checked = True
                End If

                txtPONo.Text = IIf(IsDBNull(.Fields("CUST_PO_NO").Value), "", .Fields("CUST_PO_NO").Value)
                txtPODate.Text = VB6.Format(IIf(IsDBNull(.Fields("CUST_PO_DATE").Value), "", .Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")


                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                cboDivision.Enabled = False

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

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                TxtShipTo.Text = IIf(IsDBNull(.Fields("SHIP_TO_LOC_ID").Value), "", .Fields("SHIP_TO_LOC_ID").Value)

                mAddUser = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                mAddDate = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                mModUser = IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                mModDate = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")

                cboSalePersonName.Value = IIf(IsDBNull(.Fields("SALE_PERSON_CODE").Value), "", .Fields("SALE_PERSON_CODE").Value)

                Call ShowSaleDetail1(CDbl(LblMKey.Text))
                Call ShowSaleExp1()
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))

                '            If lblCT3Date.Caption = "" And chkCT3.Value = vbChecked Then				
                '                TxtCTNo_Validate False				
                '            End If				
                '				
                '            If lblCT1Date.Caption = "" And chkCT1.Value = vbChecked Then				
                '                TxtCT1No_Validate False				
                '            End If				

                ''Call CalcTots				
            End If
        End With
        txtBillNo.Enabled = True

        SprdMain.Enabled = True
        SprdExp.Enabled = True
        pShowCalc = True
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        FormatSprdMain(-1)

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
        SqlStr = "Select FIN_PRO_INVOICE_EXP.EXPCODE,FIN_PRO_INVOICE_EXP.EXPPERCENT, FIN_PRO_INVOICE_EXP.DUTYFORGONE," & vbCrLf & " FIN_PRO_INVOICE_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO " & vbCrLf & " From FIN_PRO_INVOICE_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_PRO_INVOICE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_PRO_INVOICE_EXP.Mkey='" & LblMKey.Text & "'"

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
                    If RsSaleExp.Fields("Identification").Value = "RO" Then '''30.10.2001   ''Allow '-' if exp. is ropund off				
                        .Text = CStr(Val(IIf(IsDBNull(RsSaleExp.Fields("Amount").Value), "", RsSaleExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDBNull(RsSaleExp.Fields("Amount").Value), "", RsSaleExp.Fields("Amount").Value))))
                    End If

                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDBNull(RsSaleExp.Fields("CODE").Value), 0, RsSaleExp.Fields("CODE").Value)))

                    .Col = ColExpAddDeduct 'ExpFlag				
                    If RsCompany.Fields("COMPANY_CODE").Value = 16 And RsCompany.Fields("FYEAR").Value = 2015 And RsSaleExp.Fields("Identification").Value = "VOD" And (Trim(txtBillNo.Text) = "00337" Or Trim(txtBillNo.Text) = "00336" Or Trim(txtBillNo.Text) = "00348") Then
                        .Text = "A"
                    Else
                        .Text = IIf(RsSaleExp.Fields("Add_Ded").Value = "A", "A", "D")
                    End If

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
    Private Sub ShowSaleDetail1(ByRef mDespatchNo As Double)
        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mDivCode As Double
        Dim mHSNCode As String
        Dim pRefDate As String
        Dim mItemSNo As String

        mDivCode = -1
        If Trim(cboDivision.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivCode = Val(MasterNo)
            End If
        End If


        SqlStr = ""
        SqlStr = " SELECT ID.*, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, ID.CUSTOMER_PART_NO AS CUST_PART" & vbCrLf & " FROM FIN_PRO_INVOICE_DET ID, INV_ITEM_MST INVMST " & vbCrLf & " Where ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.Mkey='" & LblMKey.Text & "'" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " Order By SubRowNo"

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
                mItemDesc = IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)
                SprdMain.Text = mItemDesc ''IIf(IsdbNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)				

                SprdMain.Col = ColPartNo
                mPartNo = IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)
                SprdMain.Text = mPartNo

                SprdMain.Col = ColHSNCode
                mHSNCode = IIf(IsDBNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value) ''GetHSNCode(mItemCode)				
                SprdMain.Text = mHSNCode

                SprdMain.Col = ColGlassDescription
                SprdMain.Text = IIf(IsDBNull(.Fields("GLASS_DESC").Value), "", .Fields("GLASS_DESC").Value)

                SprdMain.Col = ColActualHeightInch
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_HEIGHT_INCH").Value), 0, .Fields("ACTUAL_HEIGHT_INCH").Value)))

                SprdMain.Col = ColActualWidthInch
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_WIDTH_INCH").Value), 0, .Fields("ACTUAL_WIDTH_INCH").Value)))

                SprdMain.Col = ColPacketQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PACK_QTY").Value), 0, .Fields("PACK_QTY").Value)))

                SprdMain.Col = ColActualHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_HEIGHT").Value), 0, .Fields("ACTUAL_HEIGHT").Value)))

                SprdMain.Col = ColActualWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_WIDTH").Value), 0, .Fields("ACTUAL_WIDTH").Value)))

                SprdMain.Col = ColChargeableHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_HEIGHT").Value), 0, .Fields("CHARGEABLE_HEIGHT").Value)))

                SprdMain.Col = ColChargeableWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_WIDTH").Value), 0, .Fields("CHARGEABLE_WIDTH").Value)))

                SprdMain.Col = ColArea
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GLASS_AREA").Value), 0, .Fields("GLASS_AREA").Value)))

                SprdMain.Col = ColAreaRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("AREA_RATE").Value), 0, .Fields("AREA_RATE").Value)))

                SprdMain.Col = ColGlassDevelopmentRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GLASS_DEVELOPMENT_RATE").Value), 0, .Fields("GLASS_DEVELOPMENT_RATE").Value)))

                SprdMain.Col = ColDieDevelopmentRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("DIE_DEVELOPMENT_RATE").Value), 0, .Fields("DIE_DEVELOPMENT_RATE").Value)))

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColMRP
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_MRP").Value), 0, .Fields("ITEM_MRP").Value)))

                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))

                SprdMain.Col = ColDiscRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("DISC_RATE").Value), 0, .Fields("DISC_RATE").Value)))

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
            UltraGrid1.Refresh()
            UltraGrid1.Focus()
            UltraGrid1.BringToFront()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
            UltraGrid1.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        Dim mNetAccessAmt As Double
        Dim mExciseableAmount As Double

        Dim mTaxableAmount As Double

        Dim mISJobWork As String
        Dim mMaterialCost As Double
        Dim mTotTaxableItemAmount As Double
        Dim mTaxableItemAmount As Double
        Dim pCustomerCode As String
        Dim pCST_ON_MRTL As Boolean

        Dim mCEDCessAble As Double
        Dim mADDCessAble As Double
        Dim mCessableAmount As Double
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
        Dim mLocal As String
        Dim mSuppCustCode As String = ""
        Dim mHeight As Double
        Dim mWidth As Double
        Dim mArea As Double

        Dim mWidthInch As Double
        Dim mHeightInch As Double
        Dim mHeightMM As Double
        Dim mWidthMM As Double
        Dim mAreaRate As Double
        Dim mDiscountRate As Double
        Dim mMRP As Double

        mLocal = "N"
        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCustCode = Trim(MasterNo)
            End If
        End If

        mLocal = GetPartyBusinessDetail(mSuppCustCode, Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(txtBillTo.Text), "GST_RGN_NO")
        'If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")


        pRound = 0
        mQty = 0

        mRate = 0
        mItemAmount = 0
        mTotItemAmount = 0
        mItemValue = 0
        mTotExp = 0
        mOtherTaxableAmount = 0
        mTotTaxableItemAmount = 0


        pCST_ON_MRTL = False

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

                .Col = ColActualHeightInch
                mHeightInch = Val(.Text)

                .Col = ColActualWidthInch
                mWidthInch = Val(.Text)

                .Col = ColActualHeight
                mHeightMM = Val(.Text)

                .Col = ColActualWidth
                mWidthMM = Val(.Text)

                If mHeightInch <> 0 And mHeightMM = 0 Then
                    mHeightMM = VB6.Format(mHeightInch * 25.4, "0.00")
                End If

                If mHeightMM <> 0 And mHeightInch = 0 Then
                    mHeightInch = VB6.Format(mHeightMM / 25.4, "0.00")
                End If

                If mWidthInch <> 0 And mWidthMM = 0 Then
                    mWidthMM = VB6.Format(mWidthInch * 25.4, "0.00")
                End If

                If mWidthMM <> 0 And mWidthInch = 0 Then
                    mWidthInch = VB6.Format(mWidthMM / 25.4, "0.00")
                End If

                .Col = ColActualHeightInch
                .Text = mHeightInch

                .Col = ColActualWidthInch
                .Text = mWidthInch

                .Col = ColActualHeight
                .Text = mHeightMM

                .Col = ColActualWidth
                .Text = mWidthMM

                .Col = ColChargeableHeight
                If mHeightMM > 0 Then  ''Val(.Text) = 0 And And ADDMode = True

                    mHeight = Val(.Text)
                    If cboCalcOn.SelectedIndex = 0 Then
                        mHeight = mHeightMM
                    ElseIf cboCalcOn.SelectedIndex = 1 Then
                        mHeight = (mHeightMM + IIf((mHeightMM Mod 20) > 0, (20 - (mHeightMM Mod 20)), 0))
                    ElseIf cboCalcOn.SelectedIndex = 2 Then
                        mHeight = mHeightMM + 20
                    ElseIf cboCalcOn.SelectedIndex = 3 Then
                        mHeight = mHeightMM + 30
                    ElseIf cboCalcOn.SelectedIndex = 4 Then
                        mHeight = mHeightMM + 40
                    ElseIf cboCalcOn.SelectedIndex = 5 Then
                        If mHeightMM Mod 20 = 0 Then
                            mHeight = mHeightMM + 20
                        Else
                            mHeight = (mHeightMM - (mHeightMM Mod 20)) + 20
                        End If
                    End If

                    .Text = mHeight
                    mHeight = Val(.Text) / 1000
                Else
                    mHeight = Val(.Text) / 1000
                End If


                .Col = ColChargeableWidth
                If mWidthMM > 0 Then ''Val(.Text) = 0 And ADDMode = True Then
                    mWidth = Val(.Text)
                    If cboCalcOn.SelectedIndex = 0 Then
                        mWidth = mWidthMM
                    ElseIf cboCalcOn.SelectedIndex = 1 Then
                        mWidth = (mWidthMM + IIf((mWidthMM Mod 20) > 0, (20 - (mWidthMM Mod 20)), 0))
                    ElseIf cboCalcOn.SelectedIndex = 2 Then
                        mWidth = mWidthMM + 20
                    ElseIf cboCalcOn.SelectedIndex = 3 Then
                        mWidth = mWidthMM + 30
                    ElseIf cboCalcOn.SelectedIndex = 4 Then
                        mWidth = mWidthMM + 40
                    ElseIf cboCalcOn.SelectedIndex = 5 Then
                        If mWidthMM Mod 20 = 0 Then
                            mWidth = mWidthMM + 20
                        Else
                            mWidth = (mWidthMM - (mWidthMM Mod 20)) + 20
                        End If
                    End If

                    .Text = mWidth
                    mWidth = Val(.Text) / 1000
                Else
                    mWidth = Val(.Text) / 1000
                End If

                '.Col = ColChargeableHeight
                'mHeight = Val(.Text) / 1000

                '.Col = ColChargeableWidth
                'mWidth = Val(.Text) / 1000

                .Col = ColArea
                mArea = VB6.Format(mHeight * mWidth, "0.0000")
                .Text = VB6.Format(mArea, "0.0000")

                mTotQty = mTotQty + mQty

                .Col = ColAreaRate
                mAreaRate = Val(.Text)


                If mArea > 0 And mAreaRate > 0 Then
                    .Col = ColRate
                    .Text = VB6.Format(mArea * mAreaRate, "0.00")
                    mRate = VB6.Format(mArea * mAreaRate, "0.00")           ''Val(.Text)
                Else

                    .Col = ColMRP
                    mMRP = Val(.Text)

                    .Col = ColDiscRate
                    mDiscountRate = Val(.Text)

                    mRate = mMRP - (mMRP * mDiscountRate / 100)

                    .Col = ColRate
                    .Text = VB6.Format(mRate, "0.00")
                End If



                .Col = ColAmount
                .Text = VB6.Format(mQty * mRate, "0.00")

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

                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)

                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)

                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)


                mCGSTAmount = System.Math.Round(mTaxableAmount * mCGSTPer * 0.01, 2)
                mSGSTAmount = System.Math.Round(mTaxableAmount * mSGSTPer * 0.01, 2)
                mIGSTAmount = System.Math.Round(mTaxableAmount * mIGSTPer * 0.01, 2)


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
                '                If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then				
                '                    pCustomerCode = MasterNo				
                '                Else				
                '                    pCustomerCode = "-1"				
                '                End If				
                '                If Val(lblPoNo.Caption) = "-1" Or Val(lblPoNo.Caption) = "0" Then				
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


        mNetAccessAmt = mTaxableAmount


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

        lblTotQty.Text = VB6.Format(mTotQty, "#0.000")
        lblTCS.Text = VB6.Format(pTotTCS, "#0.00")


        If mSameGSTNo = "Y" Then
            lblNetAmount.Text = VB6.Format(mTotExp + mTotItemAmount, "#0.00")
        Else
            lblNetAmount.Text = VB6.Format(mTotExp + mTotItemAmount + mTotCGST + mTotSGST + mTotIGST, "#0.00")
        End If

        '    lblServicePercentage.Caption = Val(pServPer)				
        lblTCSPercentage.Text = CStr(Val(CStr(pTCSPer)))

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Sub
    Private Sub Clear1()

        Dim RS As ADODB.Recordset
        Dim CntLst As Long

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim ds1 As New DataSet
        Dim ds2 As New DataSet

        oledbCnn = New OleDbConnection(StrConn)

        oledbCnn.Open()


        LblMKey.Text = ""
        mCustomerCode = CStr(-1)

        cmdSavePrint.Enabled = True
        mAddUser = ""
        mAddDate = ""
        mModUser = ""
        mModDate = ""

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            cboCalcOn.Visible = True
            cboCalcOn.SelectedIndex = -1
        Else
            cboCalcOn.Visible = False
            cboCalcOn.SelectedIndex = -1
        End If

        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        txtBillNoPrefix.Text = "PI" ''& vb6.Format(IIf(IsdbNull(RsCompany!INVOICE_PREFIX), "", RsCompany!INVOICE_PREFIX), "00") & Val(lblInvoiceSeq.Caption)				
        txtBillNo.Text = ""
        txtBillNoSuffix.Text = ""
        txtBillDate.Text = VB6.Format(GetServerDate, "DD/MM/YYYY")
        txtBillDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)

        TxtBillTm.Text = GetServerTime()

        txtCustomer.Text = ""
        txtCustomer.Enabled = True
        txtCreditDays(0).Text = ""
        txtCreditDays(1).Text = ""
        txtPONo.Text = ""
        txtPODate.Text = ""

        txtPONo.Enabled = True
        txtPODate.Enabled = True

        lblPoNo.Text = ""

        lblSoDate.Text = ""

        chkShipTo.Enabled = True
        chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        txtShippedTo.Enabled = False
        cmdSearchShippedTo.Enabled = False

        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked



        lblTotQty.Text = "0.000"
        lblTotItemValue.Text = "0.00"
        lblTotCGSTAmount.Text = "0.00"
        lblTotSGSTAmount.Text = "0.00"
        lblTotIGSTAmount.Text = "0.00"

        lblNetAmount.Text = "0.00"

        txtRemarks.Text = ""
        txtNarration.Text = ""
        txtCarriers.Text = ""
        txtVehicle.Text = ""

        txtDocsThru.Text = ""
        txtMode.Text = "BY ROAD"

        cboTransmode.SelectedIndex = 0
        '    txtTransportCode.Text = ""				
        '    txtDistance.Text = ""				
        '    cboVehicleType.ListIndex = 0				



        OptFreight(0).Checked = True
        OptFreight(1).Checked = False


        '    txtServProvided.Text = ""				
        '    txtServProvided.Enabled = False				


        lblTotItemValue.Text = VB6.Format(0, "#0.00")
        lblNetAmount.Text = VB6.Format(0, "#0.00")
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")
        lblRO.Text = VB6.Format(0, "#0.00")

        lblTCS.Text = VB6.Format(0, "#0.00")
        lblTCSPercentage.Text = VB6.Format(0, "#0.00")

        cboSalePersonName.Text = ""

        '    lblServicePercentage.Caption = Format(0, "#0.00")				
        '    lblTotExportExp.Caption = Format(0, "#0.00")				
        '    lblMRPValue.Caption = Format(0, "#0.00")				
        '    chkStockTrf.Value = vbUnchecked				
        '    chkPrintType.Value = vbChecked				
        '    ChkPaintPrint.Value = vbUnchecked				
        '    chkJWDetail.Value = vbUnchecked				

        '    chkDutyFreePurchase.Enabled = True				
        '    chkDutyFreePurchase.Visible = True				
        '    chkDutyFreePurchase.Value = vbUnchecked				


        '    txtShippingNo.Text = ""				
        '    txtShippingDate.Text = ""				
        '    txtARE1No.Text = ""				
        '    txtARE1Date.Text = ""				
        '    txtPortCode.Text = ""				
        '    txtExportBillNo.Text = ""				
        '    txtExportBillDate.Text = ""				
        '    txtBuyerName.Text = ""				
        '    txtCoBuyerName.Text = ""				
        '				
        '				
        '    txtTotalEuro.Text = ""				
        '    txtAdvLicense.Text = ""				
        '    txtLocation.Text = IIf(IsdbNull(RsCompany!COMPANY_CITY), "", RsCompany!COMPANY_CITY)				
        '    txtProcessNature.Text = ""				



        '    lblTotCD.Caption = Format(0, "#0.00")				
        '    lblEDUOnCDAmount.Caption = Format(0, "#0.00")				
        '    lblCDPer.Caption = Format(0, "#0.00")				
        '    lblCessOnCDPer.Caption = Format(0, "#0.00")				

        '    lblCDLabel.Visible = False				
        '    lblCessCDLabel.Visible = False				
        '    lblTotCD.Visible = False				
        '    lblEDUOnCDAmount.Visible = False				

        '    chkPrintTextDesc.Value = vbUnchecked				
        '    txtTextDesc.Text = ""				

        chkCancelled.Enabled = False



        '    chkPrintByGroup.Value = vbUnchecked				

        TabMain.SelectedIndex = 0

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

        txtBillTo.Text = ""
        TxtShipTo.Text = ""


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            SqlStr = "Select DISTINCT NAME, CODE  " & vbCrLf _
                 & " FROM FIN_SALESPERSON_MST ORDER BY NAME"

        Else

            SqlStr = "Select DISTINCT EMP_NAME NAME, EMP_CODE CODE " & vbCrLf _
                     & " FROM PAY_EMPLOYEE_MST"         '' WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        End If

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(ds1)
        ' Set the data source and data member to bind the grid.
        cboSalePersonName.DataSource = ds1
        cboSalePersonName.DataMember = ""
        cboSalePersonName.DisplayMember = "NAME"
        cboSalePersonName.ValueMember = "CODE"

        cboSalePersonName.Appearance.FontData.SizeInPoints = 8.5
        cboSalePersonName.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Sale Person Name"
        cboSalePersonName.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Sale Person Code"
        cboSalePersonName.DisplayLayout.Bands(0).Columns(0).Width = 350
        cboSalePersonName.DisplayLayout.Bands(0).Columns(1).Width = 100

        'cboSalePersonName.DisplayLayout.Bands(0).Columns(1).Hidden = True

        cboSalePersonName.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5
        cboSalePersonName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        Call FillSprdExp()
        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)


    End Sub

    Private Sub FillSprdExp()
        On Error GoTo ERR1
        Dim RS As ADODB.Recordset
        Dim I As Integer
        Dim mLocal As String
        Dim mWithInCountry As String
        Dim mIdentification As String
        Dim mIsBCD As Boolean

        MainClass.ClearGrid(SprdExp)
        mIsBCD = False
        pShowCalc = False
        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = IIf(MasterNo = "Y", "L", "C")
            Else
                mLocal = ""
            End If

            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInCountry = MasterNo
            Else
                mWithInCountry = "Y"
            End If

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

        '    If LblBookCode.Caption = ConSalesBookCode Then				
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

                If RsCompany.Fields("COMPANY_CODE").Value = 16 And RsCompany.Fields("FYEAR").Value = 2015 And mIdentification = "VOD" And (Trim(txtBillNo.Text) = "00337" Or Trim(txtBillNo.Text) = "00336" Or Trim(txtBillNo.Text) = "00348") Then
                    SprdExp.Text = "A"
                Else
                    SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")
                End If

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

        '    lblCDLabel.Visible = mIsBCD				
        '    lblCessCDLabel.Visible = mIsBCD				
        '    lblTotCD.Visible = mIsBCD				
        '    lblEDUOnCDAmount.Visible = mIsBCD				
        pShowCalc = True
        FormatSprdExp(-1)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume				
    End Sub
    Private Sub FrmInvoicePerforma_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If

        If KeyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemCode)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If

    End Sub

    Private Sub FrmInvoicePerforma_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode				
    End Sub

    Public Sub FrmInvoicePerforma_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        '''Set PvtDBCn = New ADODB.Connection				
        '''PvtDBCn.Open StrConn				

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        '    If InStr(1, XRIGHT, "D", vbTextCompare) > 1 Then				
        '        chkCancelled.Enabled = True				
        '    Else				
        '        chkCancelled.Enabled = False				
        '    End If				

        If PubSuperUser = "S" Then
            chkCancelled.Enabled = False ' True				
        Else
            chkCancelled.Enabled = False
        End If

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7755) '''8000				
        Me.Width = VB6.TwipsToPixelsX(11355) '''11900				

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



        txtCustomer.Enabled = False
        txtBillNoPrefix.Enabled = False
        txtBillNoSuffix.Enabled = False
        txtBillDate.Enabled = IIf(CDbl(PubUserLevel) = 1 Or CDbl(PubUserLevel) = 2 Or XRIGHT = "AMDV", True, False) ''IIf(XRIGHT = "AMDV", True, False)				

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
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

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
                            Call CalcTots()
                            Exit Sub
                        End If
                        SprdExp.Row = ESRow
                        SprdExp.Col = 3
                        If MainClass.ValidateWithMasterTable(m_xpn, "Name", "RoundOff", "FIN_INTERFACE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
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
        Call CalcTots()
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



    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        'With SprdMain
        '    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
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

    'Private Sub SearchDealer()				
    'On Error GoTo ErrPart				
    'Dim SqlStr  As String				
    '				
    '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"				
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
    '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"				
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
        Dim SqlStr As String
        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""				
        '				
        '    If MainClass.SearchGridMaster(txtCarriers.Text, "FIN_TRANSPORTER_MST", "TRANSPORTER_NAME", "TRANSPORTER_ID", , , SqlStr) = True Then				
        '        txtCarriers.Text = AcName				
        '        txtTransportCode.Text = AcName1				
        '        If txtCarriers.Enabled = True Then txtCarriers.SetFocus				
        '    End If				
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

    Private Sub txtVehicle_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtVehicle.Text, "FIN_Vehicle_MST", "NAME", , , , SqlStr) = True Then
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

    Private Sub txtVehicle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Function GetTCSApplication(ByRef mCustomerCode As String, ByRef mDespType As String, ByRef pDate As String) As Double
        On Error GoTo ERR1
        Dim mTCSApp As String
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
            Else
                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "TCS_APP", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mTCSApp = MasterNo
                End If
                mTCSApp = IIf(mTCSApp = "", "N", mTCSApp)
                If mTCSApp = "N" Then
                    mTurnOver = GetCurrentTurnOver(mCustomerCode, "", VB6.Format(txtBillDate.Text, "DD/MM/YYYY"), mCompanyPANNo, mCustomerPANNo)
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

    Private Sub FillCreditDays(ByRef mCustomerCode As String)
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
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
    Private Sub txtVehicle_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicle.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicle.Text, "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmInvoicePerforma_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 250, mReFormWidth - 250, mReFormWidth))
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        TabMain.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        Frame6.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        'SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdGetData_Click(sender As Object, e As EventArgs) Handles cmdGetData.Click
        If ADDMode = False And MODIFYMode = False Then GoTo EventExitSub
        If ADDMode = True Then
            Call CollectPOData()
            FormatSprdMain(-1)
        End If
EventExitSub:
    End Sub

    Private Sub txtPONo_DoubleClick(sender As Object, e As EventArgs) Handles txtPONo.DoubleClick
        Call SearchSO()
    End Sub
    Private Sub txtPONo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPONo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSO()
    End Sub
    Private Sub SearchSO()
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset
        Dim pCustomerCode As String



        If Trim(txtCustomer.Text) = "" Then Exit Sub

        SqlStr = " SELECT SUPP_CUST_NAME,SUPP_CUST_CODE, SUPP_CUST_ADDR, SUPP_CUST_CITY, " & vbCrLf _
            & " SUPP_CUST_STATE, SUPP_CUST_PIN " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_TYPE IN ('S','C')" & vbCrLf _
            & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"

        If ADDMode = True Then
            SqlStr = SqlStr & vbCrLf & "AND STATUS='O'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            pCustomerCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
        Else
            pCustomerCode = "-1"
            Exit Sub
        End If


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'"


        SqlStr = " SELECT IH.CUST_PO_NO, IH.CUST_PO_DATE, IH.AUTO_KEY_SO, IH.AMEND_NO,  " & vbCrLf _
            & " ID.ITEM_CODE, ID.PART_NO, IMST.ITEM_SHORT_DESC, ID.UOM_CODE" & vbCrLf _
            & "  FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, INV_ITEM_MST IMST" & vbCrLf _
            & "  WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & "  AND IH.MKEY=ID.MKEY" & vbCrLf _
            & "  AND IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
            & "  AND ID.ITEM_CODE=IMST.ITEM_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & Trim(pCustomerCode) & "' AND IH.SO_STATUS='O' AND IH.SO_APPROVED='Y' AND IH.ISGSTENABLE_PO='Y'"

        SqlStr = SqlStr & " AND GOODS_SERVICE='G'"

        If Trim(txtBillTo.Text) <> "" Then
            SqlStr = SqlStr & " AND BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2(txtPONo.Text, SqlStr) = True Then        ''If MainClass.SearchGridMaster(txtSONo.Text, "DSP_SALEORDER_HDR", "AUTO_KEY_SO", "SO_DATE", "CUST_PO_NO", "CUST_PO_DATE", SqlStr) = True Then
            txtPONo.Text = AcName
            txtPODate.Text = AcName1
            lblPoNo.Text = AcName2
            'txtPONo_Validating(txtPONo, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CollectPOData()

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsPO As ADODB.Recordset = Nothing
        Dim FirstTime As Boolean
        Dim mSprdRowNo As Integer


        FirstTime = True


        MainClass.ClearGrid(SprdMain, ConRowHeight)
        mSprdRowNo = 0

        FormatSprdMain(-1)

        SqlStr = ""


        SqlStr = " SELECT POM.*, " & vbCrLf _
                & " POD.SERIAL_NO, POD.SUPP_CUST_CODE, POD.ITEM_CODE, POD.UOM_CODE, POD.PART_NO, SO_QTY, ITEM_SIZE, POD.HSN_CODE, POD.PACK_QTY," & vbCrLf _
                & " POD.ITEM_PRICE, POD.PACK_TYPE, POD.COLOUR_DTL, AC.SUPP_CUST_NAME as SuppName, " & vbCrLf _
                & " GLASS_DESC, ACTUAL_HEIGHT, ACTUAL_WIDTH, CHARGEABLE_HEIGHT,CHARGEABLE_WIDTH, GLASS_AREA,CHARGEABLEGLASS_AREA,AREA_RATE" & vbCrLf _
                & " FROM DSP_SALEORDER_HDR POM,DSP_SALEORDER_DET POD,FIN_SUPP_CUST_MST AC " & vbCrLf _
                & " WHERE POM.MKEY = POD.MKEY " & vbCrLf _
                & " AND POM.Company_Code = AC.Company_Code " & vbCrLf _
                & " AND POM.SUPP_CUST_CODE = AC.SUPP_CUST_CODE " & vbCrLf _
                & " AND POM.AUTO_KEY_SO=" & Val(lblPoNo.Text) & " AND SO_APPROVED='Y'"

        If mCustomerCode <> "-1" Then
            SqlStr = SqlStr & vbCrLf & " AND POM.SUPP_CUST_CODE='" & mCustomerCode & "' "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND POM.SO_STATUS='O' " & vbCrLf _
            & " ORDER BY POD.SERIAL_NO"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPO, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsPO.EOF Then
            If FirstTime = True Then
                If FillPOMainPart(RsPO) = True Then FirstTime = False
            End If
            If MsgQuestion("Populate Data From Customer Sales Order ...") = CStr(MsgBoxResult.No) Then
                Exit Sub
            End If
            FillPODetailPart(RsPO, (lblPoNo.Text), mSprdRowNo, mCustomerCode)
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)

    End Sub
    Private Function FillPOMainPart(ByRef RsPO As ADODB.Recordset) As Boolean
        On Error GoTo ErrPart
        Dim mConsigneeCode As String = ""
        Dim mShippedToCode As String = ""
        Dim mBillToSameShipToCode As String = ""
        Dim mCustomerCode As String

        txtCustomer.Text = IIf(IsDBNull(RsPO.Fields("SuppName").Value), "", RsPO.Fields("SuppName").Value)


        mCustomerCode = IIf(IsDBNull(RsPO.Fields("SUPP_CUST_CODE").Value), "", RsPO.Fields("SUPP_CUST_CODE").Value)

        'txtSONo.Text = IIf(IsDBNull(RsPO.Fields("AUTO_KEY_SO").Value), "", RsPO.Fields("AUTO_KEY_SO").Value)
        'txtSODate.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("SO_DATE").Value), "", RsPO.Fields("SO_DATE").Value), "DD/MM/YYYY")
        'txtCustPoNo.Text = IIf(IsDBNull(RsPO.Fields("CUST_PO_NO").Value), "", RsPO.Fields("CUST_PO_NO").Value)
        'txtCustPODate.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("CUST_PO_DATE").Value), "", RsPO.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")


        mBillToSameShipToCode = IIf(IsDBNull(RsPO.Fields("SHIPPED_TO_SAMEPARTY").Value), "", RsPO.Fields("SHIPPED_TO_SAMEPARTY").Value)
        txtBillTo.Text = IIf(IsDBNull(RsPO.Fields("BILL_TO_LOC_ID").Value), "", RsPO.Fields("BILL_TO_LOC_ID").Value)

        If mBillToSameShipToCode = "Y" Then
            mShippedToCode = IIf(IsDBNull(RsPO.Fields("SUPP_CUST_CODE").Value), "", RsPO.Fields("SUPP_CUST_CODE").Value)
            TxtShipTo.Text = txtBillTo.Text
            chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        Else
            mShippedToCode = IIf(IsDBNull(RsPO.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsPO.Fields("SHIPPED_TO_PARTY_CODE").Value)
            TxtShipTo.Text = IIf(IsDBNull(RsPO.Fields("SHIP_TO_LOC_ID").Value), "", RsPO.Fields("SHIP_TO_LOC_ID").Value)
            chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked
        End If

        If MainClass.ValidateWithMasterTable(mShippedToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtShippedTo.Text = MasterNo
        Else
            txtShippedTo.Text = ""
        End If

        'txtAddress.Text = GetPartyBusinessDetail(Trim(mShippedToCode), Trim(TxtShipTo.Text), "SUPP_CUST_ADDR || ', ' || SUPP_CUST_CITY || ', ' || SUPP_CUST_STATE")

        'TxtCustomerName.Enabled = False
        'txtCustomerCode.Enabled = False
        cmdsearch.Enabled = False
        FillPOMainPart = True
        Exit Function
ErrPart:
        FillPOMainPart = False
        MsgBox(Err.Description)
    End Function
    Private Sub FillPODetailPart(ByRef RsPO As ADODB.Recordset, ByRef mtxtSONo As String, ByRef SprdRowNo As Integer, ByRef mCustomerCode As String)

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""
        Dim mFactor As Double
        Dim mStockType As String = ""
        Dim mSqlStr As String
        Dim pAutoSONO As Double
        Dim pAutoSOAmendNo As Double
        Dim pItemCode As String
        Dim pNewPrice As Double
        Dim pCustomerCode As String
        Dim RsSuppPO As ADODB.Recordset = Nothing
        Dim pWEFDate As String
        Dim pOldPrice As Double
        Dim mSoNo As Double
        Dim mDIRequired As String = "N"
        Dim pQty As Double
        Dim xMRRNo As String
        Dim mDivisionCode As Double

        Dim mAutoKeyInvoice As String
        'Dim mSqlStr As String
        Dim RsRate As ADODB.Recordset = Nothing

        Dim mODNo As String = ""
        Dim mSOAmendNo As Long
        Dim mSOMKey As String
        Dim mScheduleQty As Double
        Dim mTotMonthPackQty As Double
        Dim mGlassDesc As String

        Dim mHSNCode As String
        Dim mSaleInvTypeCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mInvTypeDesc As String
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mGoodsServices As String = "G"

        mFactor = 1
        If RsPO.EOF Then Exit Sub
        RsPO.MoveFirst()

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustomerCode = Trim(MasterNo)
            End If
        End If

        mLocal = GetPartyBusinessDetail(mCustomerCode, Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(mCustomerCode, Trim(txtBillTo.Text), "GST_RGN_NO")
        'If MainClass.ValidateWithMasterTable(Trim(txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If


        With SprdMain
            Do While RsPO.EOF = False


                mGoodsServices = IIf(IsDBNull(RsPO.Fields("GOODS_SERVICE").Value), "G", RsPO.Fields("GOODS_SERVICE").Value)

                mSoNo = CDbl(Trim(Str(IIf(IsDBNull(RsPO.Fields("AUTO_KEY_SO").Value), 0, RsPO.Fields("AUTO_KEY_SO").Value))))
                If MainClass.ValidateWithMasterTable(Val(mSoNo), "AUTO_KEY_SO", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & Trim(mCustomerCode) & "' AND SO_APPROVED='Y'") = True Then
                    mDIRequired = MasterNo
                Else
                    mDIRequired = "N"
                End If

                mItemCode = Trim(IIf(IsDBNull(RsPO.Fields("ITEM_CODE").Value), "", RsPO.Fields("ITEM_CODE").Value))

                mGlassDesc = mItemCode & IIf(IsDBNull(RsPO.Fields("ITEM_SIZE").Value), "", RsPO.Fields("ITEM_SIZE").Value)

                If CheckDuplicateItem(mGlassDesc) = True Then GoTo NexrRec


                SprdRowNo = SprdRowNo + 1
                .MaxRows = SprdRowNo + 1
                '            FormatSprdMain -1
                .Row = SprdRowNo

                .Col = ColItemCode
                .Text = mItemCode

                .Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                .Text = mItemDesc

                mItemDesc = ""
                .Col = ColPartNo

                mItemDesc = Trim(IIf(IsDBNull(RsPO.Fields("PART_NO").Value), "", RsPO.Fields("PART_NO").Value))

                .Text = mItemDesc

                .Col = ColUnit
                ''15-02-2006  'sk

                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "ISSUE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mUOM = MasterNo
                .Text = mUOM

                .Col = ColPacketQty
                .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("PACK_QTY").Value), 0, RsPO.Fields("PACK_QTY").Value), "0.000")
                '
                .Col = ColQty
                .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("SO_QTY").Value), 0, RsPO.Fields("SO_QTY").Value), "0.000")

                .Col = ColMRP
                .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("ITEM_PRICE").Value), 0, RsPO.Fields("ITEM_PRICE").Value), "0.000")

                .Col = ColRate
                .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("ITEM_PRICE").Value), 0, RsPO.Fields("ITEM_PRICE").Value), "0.000")

                .Col = ColGlassDescription
                .Text = IIf(IsDBNull(RsPO.Fields("ITEM_SIZE").Value), "", RsPO.Fields("ITEM_SIZE").Value)

                .Col = ColActualHeight
                .Text = IIf(IsDBNull(RsPO.Fields("ACTUAL_HEIGHT").Value), 0, RsPO.Fields("ACTUAL_HEIGHT").Value)

                .Col = ColActualWidth
                .Text = IIf(IsDBNull(RsPO.Fields("ACTUAL_WIDTH").Value), 0, RsPO.Fields("ACTUAL_WIDTH").Value)

                .Col = ColChargeableHeight
                .Text = IIf(IsDBNull(RsPO.Fields("CHARGEABLE_HEIGHT").Value), 0, RsPO.Fields("CHARGEABLE_HEIGHT").Value)

                .Col = ColChargeableWidth
                .Text = IIf(IsDBNull(RsPO.Fields("CHARGEABLE_WIDTH").Value), 0, RsPO.Fields("CHARGEABLE_WIDTH").Value)

                .Col = ColArea
                .Text = IIf(IsDBNull(RsPO.Fields("CHARGEABLEGLASS_AREA").Value), 0, RsPO.Fields("CHARGEABLEGLASS_AREA").Value)

                .Col = ColAreaRate
                .Text = IIf(IsDBNull(RsPO.Fields("AREA_RATE").Value), 0, RsPO.Fields("AREA_RATE").Value)

                .Col = ColHSNCode
                .Text = IIf(IsDBNull(RsPO.Fields("HSN_CODE").Value), "", RsPO.Fields("HSN_CODE").Value)
                mHSNCode = IIf(IsDBNull(RsPO.Fields("HSN_CODE").Value), "", RsPO.Fields("HSN_CODE").Value)

                If mGoodsServices = "G" Then
                    If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "0", mPartyGSTNo) = False Then GoTo ERR1
                Else
                    If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, "0") = False Then GoTo ERR1
                End If


                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")

NexrRec:
                RsPO.MoveNext()
                If RsPO.EOF = False Then
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    '                FormatSprdMain .MaxRows
                End If
            Loop
        End With


        FormatSprdMain(-1)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub cboCalcOn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCalcOn.SelectedIndexChanged
        On Error GoTo ERR1
        Dim mQty As Double
        Dim mRate As Double
        Dim mItemAmount As Double
        Dim mItemValue As Double
        Dim mTotQty As Double
        Dim j As Integer
        Dim I As Integer
        Dim mItemCode As String
        Dim mTotItemAmount As Double
        Dim mUOM As String
        Dim mHeight As Double
        Dim mWidth As Double
        Dim mArea As Double

        Dim mWidthInch As Double
        Dim mHeightInch As Double
        Dim mHeightMM As Double
        Dim mWidthMM As Double
        Dim mAreaRate As Double
        Dim mDiscRate As Double
        Dim mMRP As Double
        If FormActive = False Then Exit Sub
        If cboCalcOn.SelectedIndex = -1 Then Exit Sub

        If MsgQuestion("Want to Reset Chargeable Area ? ") = CStr(MsgBoxResult.No) Then Exit Sub

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                '.Col = 0
                'If .Text = "Del" Then GoTo DontCalc

                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc
                mItemCode = .Text

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColUnit
                mUOM = Trim(.Text)

                .Col = ColActualHeightInch
                mHeightInch = Val(.Text)

                .Col = ColActualWidthInch
                mWidthInch = Val(.Text)

                .Col = ColActualHeight
                mHeightMM = Val(.Text)

                .Col = ColActualWidth
                mWidthMM = Val(.Text)

                If mHeightInch <> 0 And mHeightMM = 0 Then
                    mHeightMM = VB6.Format(mHeightInch * 25.4, "0.00")
                End If

                If mHeightMM <> 0 And mHeightInch = 0 Then
                    mHeightInch = VB6.Format(mHeightMM / 25.4, "0.00")
                End If

                If mWidthInch <> 0 And mWidthMM = 0 Then
                    mWidthMM = VB6.Format(mWidthInch * 25.4, "0.00")
                End If

                If mWidthMM <> 0 And mWidthInch = 0 Then
                    mWidthInch = VB6.Format(mWidthMM / 25.4, "0.00")
                End If

                .Col = ColActualHeightInch
                .Text = mHeightInch

                .Col = ColActualWidthInch
                .Text = mWidthInch

                .Col = ColActualHeight
                .Text = mHeightMM

                .Col = ColActualWidth
                .Text = mWidthMM

                .Col = ColChargeableHeight
                'If Val(.Text) = 0 And ADDMode = True Then
                If cboCalcOn.SelectedIndex = 0 Then
                    mHeight = mHeightMM
                ElseIf cboCalcOn.SelectedIndex = 1 Then
                    mHeight = (mHeightMM + IIf((mHeightMM Mod 20) > 0, (20 - (mHeightMM Mod 20)), 0))
                ElseIf cboCalcOn.SelectedIndex = 2 Then
                    mHeight = mHeightMM + 20
                ElseIf cboCalcOn.SelectedIndex = 3 Then
                    mHeight = mHeightMM + 30
                ElseIf cboCalcOn.SelectedIndex = 4 Then
                    mHeight = mHeightMM + 40
                ElseIf cboCalcOn.SelectedIndex = 5 Then
                    If mHeightMM Mod 20 = 0 Then
                        mHeight = mHeightMM + 20
                    Else
                        mHeight = (mHeightMM - (mHeightMM Mod 20)) + 20
                    End If
                End If

                .Text = mHeight
                mHeight = Val(.Text) / 1000
                'Else
                '    mHeight = Val(.Text) / 1000
                'End If


                .Col = ColChargeableWidth
                'If Val(.Text) = 0 And ADDMode = True Then
                If cboCalcOn.SelectedIndex = 0 Then
                    mWidth = mWidthMM
                ElseIf cboCalcOn.SelectedIndex = 1 Then
                    mWidth = (mWidthMM + IIf((mWidthMM Mod 20) > 0, (20 - (mWidthMM Mod 20)), 0))
                ElseIf cboCalcOn.SelectedIndex = 2 Then
                    mWidth = mWidthMM + 20
                ElseIf cboCalcOn.SelectedIndex = 3 Then
                    mWidth = mWidthMM + 30
                ElseIf cboCalcOn.SelectedIndex = 4 Then
                    mWidth = mWidthMM + 40
                ElseIf cboCalcOn.SelectedIndex = 5 Then
                    If mWidthMM Mod 20 = 0 Then
                        mWidth = mWidthMM + 20
                    Else
                        mWidth = (mWidthMM - (mWidthMM Mod 20)) + 20
                    End If
                End If

                .Text = mWidth
                mWidth = Val(.Text) / 1000
                'Else
                '    mWidth = Val(.Text) / 1000
                'End If

                '.Col = ColChargeableHeight
                'mHeight = Val(.Text) / 1000

                '.Col = ColChargeableWidth
                'mWidth = Val(.Text) / 1000

                .Col = ColArea
                mArea = VB6.Format(mHeight * mWidth, "0.0000")
                .Text = VB6.Format(mArea, "0.0000")

                mTotQty = mTotQty + mQty

                .Col = ColAreaRate
                mAreaRate = Val(.Text)


                If mArea > 0 And mAreaRate > 0 Then
                    .Col = ColRate
                    .Text = VB6.Format(mArea * mAreaRate, "0.00")
                    mRate = VB6.Format(mArea * mAreaRate, "0.00")           ''Val(.Text)
                Else

                    .Col = ColMRP
                    mMRP = Val(.Text)

                    .Col = ColDiscRate
                    mDiscRate = Val(.Text)

                    mRate = mMRP - (mMRP * mDiscRate / 100)

                    .Col = ColRate
                    .Text = VB6.Format(mRate, "0.00")

                End If

                '.Col = ColDiscRate
                'mDiscRate = Val(.Text)

                'mRate = mRate - (mRate * mDiscRate / 100)

                .Col = ColAmount
                .Text = VB6.Format(mQty * mRate, "0.00")

                mTotItemAmount = mTotItemAmount + CDbl(VB6.Format(mQty * mRate, "0.00"))
DontCalc:
            Next I
        End With


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume		
    End Sub

    Private Sub lblPoNo_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles lblPoNo.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub lblPoNo_Validating(sender As Object, EventArgs As CancelEventArgs) Handles lblPoNo.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mPONo As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset

        If Trim(lblPoNo.Text) = "" Then GoTo EventExitSub
        If Len(lblPoNo.Text) < 6 Then
            lblPoNo.Text = VB6.Format(Val(lblPoNo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mPONo = Val(lblPoNo.Text)


        SqlStr = "SELECT * FROM DSP_SALEORDER_HDR " & " WHERE AUTO_KEY_SO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'" ''& vbCrLf |            & " AND SUBSTR(AUTO_KEY_SO,LENGTH(AUTO_KEY_SO)-5,4)=" & RsCompany.fields("FYEAR").value & ""


        SqlStr = SqlStr & " AND AMEND_NO = (" & vbCrLf _
                & " SELECT MAX(AMEND_NO) FROM DSP_SALEORDER_HDR " & " WHERE AUTO_KEY_SO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then

            txtPONo.Text = IIf(IsDBNull(RsTemp.Fields("CUST_PO_NO").Value), "", RsTemp.Fields("CUST_PO_NO").Value)
            txtPODate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CUST_PO_DATE").Value), "", RsTemp.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")
            lblSoDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SO_DATE").Value), "", RsTemp.Fields("SO_DATE").Value), "DD/MM/YYYY")
        Else
            MsgBox("No Such SO No. Click", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdPopulateExcel_Click(sender As Object, e As EventArgs) Handles cmdPopulateExcel.Click
        On Error GoTo ErrPart
        Dim strFilePath As String

        strFilePath = My.Application.Info.DirectoryPath
        If Not fOpenFile(strFilePath, "*.xls", "Excel Data", CommonDialogOpen) Then
            GoTo NormalExit
        End If

        Call PopulateFromXLSFile(strFilePath)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mSqlStr As String
        Dim mItemCode As String
        Dim mPartNo As String
        Dim mItemDesc As String
        Dim mHSNCode As String
        Dim mUnit As String
        Dim mGlassDescription As String

        Dim mActualWidthInch As Double
        Dim mActualHeightInch As Double
        Dim mActualWidth As Double
        Dim mActualHeight As Double
        Dim mChargeableWidth As Double
        Dim mChargeableHeight As Double


        Dim mArea As Double
        Dim mPacketQty As Double
        Dim mQty As Double
        Dim mAreaRate As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mTaxableAmount As Double
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mGlassDevelopmentRate As Double
        Dim mDieDevelopmentRate As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset

        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""

        Dim mTempFile As String

        mTempFile = Mid(strXLSFile, 1, Len(strXLSFile) - 4) & "_Temp" & ".xlsx"   ''(.xlsx)

        CopyFile(strXLSFile, mTempFile, 0)

        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", mTempFile)
        strTemp = Mid(mTempFile, 1, InStrRev(mTempFile, "\") - 1)
        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
            GoTo ErrPart
        End If

        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        strWkShName = RsFile.Fields("Table_Name").Value

        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"
        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

        Dim ultRow As UltraDataRow
        Dim lngRow As Long


        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then



            If RsFile.EOF = False Then
                cntRow = 1
                Do While Not RsFile.EOF
                    mItemCode = Trim(IIf(IsDBNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))

                    mPartNo = ""
                    mItemDesc = ""
                    mHSNCode = ""
                    mUnit = ""

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        mItemDesc = MasterNo
                    End If

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        mPartNo = MasterNo
                    End If

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "HSN_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        mHSNCode = MasterNo
                    End If

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        mUnit = MasterNo
                    End If

                    mGlassDescription = Trim(IIf(IsDBNull(RsFile.Fields(5).Value), "", RsFile.Fields(5).Value))
                    mActualWidthInch = Trim(IIf(IsDBNull(RsFile.Fields(6).Value), 0, RsFile.Fields(6).Value))
                    mActualHeightInch = Trim(IIf(IsDBNull(RsFile.Fields(7).Value), 0, RsFile.Fields(7).Value))
                    mActualWidth = Trim(IIf(IsDBNull(RsFile.Fields(8).Value), 0, RsFile.Fields(8).Value))
                    mActualHeight = Trim(IIf(IsDBNull(RsFile.Fields(9).Value), 0, RsFile.Fields(9).Value))
                    mChargeableWidth = Trim(IIf(IsDBNull(RsFile.Fields(10).Value), 0, RsFile.Fields(10).Value))
                    mChargeableHeight = Trim(IIf(IsDBNull(RsFile.Fields(11).Value), 0, RsFile.Fields(11).Value))
                    mArea = Trim(IIf(IsDBNull(RsFile.Fields(12).Value), 0, RsFile.Fields(12).Value))
                    mPacketQty = Trim(IIf(IsDBNull(RsFile.Fields(13).Value), 0, RsFile.Fields(13).Value))
                    mQty = Trim(IIf(IsDBNull(RsFile.Fields(14).Value), 0, RsFile.Fields(14).Value))
                    mAreaRate = Trim(IIf(IsDBNull(RsFile.Fields(15).Value), 0, RsFile.Fields(15).Value))
                    mRate = Trim(IIf(IsDBNull(RsFile.Fields(16).Value), 0, RsFile.Fields(16).Value))
                    mAmount = Trim(IIf(IsDBNull(RsFile.Fields(17).Value), 0, RsFile.Fields(17).Value))
                    mTaxableAmount = Trim(IIf(IsDBNull(RsFile.Fields(18).Value), 0, RsFile.Fields(18).Value))
                    mCGSTPer = Trim(IIf(IsDBNull(RsFile.Fields(19).Value), 0, RsFile.Fields(19).Value))
                    mCGSTAmount = Trim(IIf(IsDBNull(RsFile.Fields(20).Value), 0, RsFile.Fields(20).Value))
                    mSGSTPer = Trim(IIf(IsDBNull(RsFile.Fields(21).Value), 0, RsFile.Fields(21).Value))
                    mSGSTAmount = Trim(IIf(IsDBNull(RsFile.Fields(22).Value), 0, RsFile.Fields(22).Value))
                    mIGSTPer = Trim(IIf(IsDBNull(RsFile.Fields(23).Value), 0, RsFile.Fields(23).Value))
                    mIGSTAmount = Trim(IIf(IsDBNull(RsFile.Fields(24).Value), 0, RsFile.Fields(24).Value))
                    mGlassDevelopmentRate = Trim(IIf(IsDBNull(RsFile.Fields(25).Value), 0, RsFile.Fields(25).Value))
                    mDieDevelopmentRate = Trim(IIf(IsDBNull(RsFile.Fields(26).Value), 0, RsFile.Fields(26).Value))

                    SprdMain.Row = cntRow
                    SprdMain.Col = ColItemCode
                    SprdMain.Text = mItemCode

                    SprdMain.Col = ColPartNo
                    SprdMain.Text = mPartNo

                    SprdMain.Col = ColItemDesc
                    SprdMain.Text = mItemDesc

                    SprdMain.Col = ColHSNCode
                    SprdMain.Text = mHSNCode

                    SprdMain.Col = ColUnit
                    SprdMain.Text = mUnit

                    SprdMain.Col = ColGlassDescription
                    SprdMain.Text = mGlassDescription

                    SprdMain.Col = ColActualWidthInch
                    SprdMain.Text = VB6.Format(mActualWidthInch, "0.00")

                    SprdMain.Col = ColActualHeightInch
                    SprdMain.Text = VB6.Format(mActualHeightInch, "0.00")

                    SprdMain.Col = ColActualWidth
                    SprdMain.Text = VB6.Format(mActualWidth, "0.00")

                    SprdMain.Col = ColActualHeight
                    SprdMain.Text = VB6.Format(mActualHeight, "0.00")

                    SprdMain.Col = ColChargeableWidth
                    SprdMain.Text = VB6.Format(mChargeableWidth, "0.00")

                    SprdMain.Col = ColChargeableHeight
                    SprdMain.Text = VB6.Format(mChargeableHeight, "0.00")

                    SprdMain.Col = ColArea
                    SprdMain.Text = VB6.Format(mArea, "0.00")

                    SprdMain.Col = ColPacketQty
                    SprdMain.Text = VB6.Format(mPacketQty, "0.00")

                    SprdMain.Col = ColQty
                    SprdMain.Text = VB6.Format(mQty, "0.00")

                    SprdMain.Col = ColAreaRate
                    SprdMain.Text = VB6.Format(mAreaRate, "0.00")

                    SprdMain.Col = ColRate
                    SprdMain.Text = VB6.Format(mRate, "0.00")

                    SprdMain.Col = ColAmount
                    SprdMain.Text = VB6.Format(mAmount, "0.00")

                    SprdMain.Col = ColTaxableAmount
                    SprdMain.Text = VB6.Format(mTaxableAmount, "0.00")

                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(mCGSTPer, "0.00")

                    SprdMain.Col = ColCGSTAmount
                    SprdMain.Text = VB6.Format(mCGSTAmount, "0.00")

                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(mSGSTPer, "0.00")

                    SprdMain.Col = ColSGSTAmount
                    SprdMain.Text = VB6.Format(mSGSTAmount, "0.00")

                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(mIGSTPer, "0.00")

                    SprdMain.Col = ColIGSTAmount
                    SprdMain.Text = VB6.Format(mIGSTAmount, "0.00")

                    SprdMain.Col = ColGlassDevelopmentRate
                    SprdMain.Text = VB6.Format(mGlassDevelopmentRate, "0.00")

                    SprdMain.Col = ColDieDevelopmentRate
                    SprdMain.Text = VB6.Format(mDieDevelopmentRate, "0.00")

                    RsFile.MoveNext()
                    cntRow = cntRow + 1
                    SprdMain.MaxRows = cntRow
                Loop
            End If
        End If

        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
        End If

        'red=&H00C0C0FF&
        'g=&H00C0FFC0&
        '    CmdPopFromFile.Enabled = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
    Private Sub cboSalePersonName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSalePersonName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboSalePersonName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles cboSalePersonName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, cboSalePersonName.Text)  '' MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdMain_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        If e.keyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemCode)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub
    Private Function CheckDuplicateItem(ByVal pRow As Integer) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        Dim mItemCode As String
        Dim mCheckItemCode As String

        If pRow < 1 Then CheckDuplicateItem = True : Exit Function

        With SprdMain
            .Row = pRow
            .Col = ColItemCode
            mItemCode = UCase(.Text)

            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                mCheckItemCode = UCase(.Text)

                If UCase(mCheckItemCode) = UCase(mItemCode) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code : " & mCheckItemCode & " of Line No : " & I)
                        MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

End Class
