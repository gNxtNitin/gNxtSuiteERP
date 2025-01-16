Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Imports AxFPSpreadADO


Imports System.Data
Imports System.IO
Imports System.Configuration
Imports System.Drawing.Color


Friend Class FrmDespatchNote
    Inherits System.Windows.Forms.Form
    Dim RsDNMain As ADODB.Recordset
    Dim RsDNDetail As ADODB.Recordset

    ''Private PvtDBCn As ADODB.Connection
    Dim mSearchStartRow As Integer
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer

    Dim mCustomerCode As String
    Dim mWithOutOrder As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColSONo As Short = 1
    Private Const ColSODate As Short = 2
    Private Const ColCustomerNo As Short = 3
    Private Const ColCustomerDate As Short = 4
    Private Const ColItemCode As Short = 5
    Private Const ColItemDesc As Short = 6
    Private Const ColPartNo As Short = 7
    Private Const ColUnit As Short = 8
    Private Const ColGlassDescription As Short = 9
    Private Const ColModel As Short = 10
    Private Const ColDrawingNo As Short = 11
    Private Const ColActualWidth As Short = 12
    Private Const ColActualHeight As Short = 13
    Private Const ColArea As Short = 14

    Private Const ColChargeableWidth As Short = 15
    Private Const ColChargeableHeight As Short = 16
    Private Const ColChargeableArea As Short = 17

    Private Const ColMRRNo As Short = 18
    Private Const ColRefNo As Short = 19
    Private Const ColRefDate As Short = 20
    Private Const ColStockType As Short = 21
    Private Const ColLotNo As Short = 22
    Private Const ColStoreLoc As Short = 23
    Private Const ColODNo As Short = 24
    Private Const ColHeatNo As Short = 25
    Private Const ColBatchNo As Short = 26
    Private Const Col57BalQty As Short = 27
    Private Const ColStockQty As Short = 28
    Private Const ColBalScheduleQty As Short = 29
    Private Const ColPackQty As Short = 30
    Private Const ColPktQty As Short = 31
    Private Const ColPackType As Short = 32
    Private Const ColInnerBoxQty As Short = 33
    Private Const ColInnerBoxCode As Short = 34
    Private Const ColOuterBoxQty As Short = 35
    Private Const ColOuterBoxCode As Short = 36
    Private Const ColJITCallNo As Short = 37

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim pMenu As String

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        Dim cntRow As Integer
        Dim xICode As String
        Dim xIUOM As String
        Dim mLotNo As String
        Dim mStockType As String = ""
        Dim mDivisionCode As Double
        Dim xFGBatchNoReq As String
        Dim mHeatNo As String
        Dim mDIRequired As String = "N"
        Dim mODNo As String = ""
        Dim mSOAmendNo As Long
        Dim mSOMKey As String
        Dim mScheduleQty As Double
        Dim mTotMonthPackQty As Double
        Dim mWidth As Double
        Dim mHeight As Double
        Dim mModelNo As String
        Dim mDrawingNo As String
        Dim xStoreLoc As String

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColItemCode
                xICode = Trim(.Text)
                If xICode = "" Then Exit Sub

                .Col = ColUnit
                xIUOM = Trim(.Text)

                .Col = ColHeatNo
                mHeatNo = Trim(.Text)

                .Col = ColBatchNo
                mLotNo = Trim(.Text)


                .Col = ColChargeableWidth
                mWidth = Val(.Text)

                .Col = ColChargeableHeight
                mHeight = Val(.Text)

                If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    xFGBatchNoReq = "Y"
                Else
                    xFGBatchNoReq = "N"
                End If

                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColChargeableWidth
                mWidth = Val(.Text)

                .Col = ColChargeableHeight
                mHeight = Val(.Text)

                .Col = ColModel
                mModelNo = Trim(.Text)

                .Col = ColDrawingNo
                mDrawingNo = Trim(.Text)

                .Col = ColStoreLoc
                xStoreLoc = Trim(SprdMain.Text)

                .Col = ColStockQty
                .Text = CStr(GetBalanceStockQty(xICode, (txtDNDate.Text), xIUOM, "PAD", mStockType, mLotNo, ConWH, mDivisionCode, "DSP", Val(txtDNNo.Text), xFGBatchNoReq,, mHeatNo))

                If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "G" Then
                    mSOAmendNo = GetSOMaxAmendNo(CDbl(VB6.Format(Val(txtSONo.Text))))
                    mSOMKey = CDbl(Val(txtSONo.Text) & VB6.Format(mSOAmendNo, "000"))

                    If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDIRequired = MasterNo
                    End If

                    If mDIRequired = "Y" Then
                        .Col = ColODNo
                        mODNo = .Text
                    End If

                    mScheduleQty = GetSalesDSQty(xICode, mDIRequired, mODNo, Trim(txtStoreLoc.Text), mWidth, mHeight, mModelNo, mDrawingNo)
                    mTotMonthPackQty = GetTotMonthDespatchQty(xICode, mDIRequired, mODNo, mWidth, mHeight, mModelNo, mDrawingNo, xStoreLoc)

                    .Col = ColBalScheduleQty
                    .Text = System.Math.Round(mScheduleQty - mTotMonthPackQty, 2)
                Else
                    .Col = ColBalScheduleQty
                    .Text = "0.00"
                End If
            Next
        End With
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        Dim cntRow As Integer
        Dim xICode As String
        Dim xIUOM As String
        Dim mLotNo As String
        Dim mStockType As String = ""
        Dim mDivisionCode As Double
        Dim xFGBatchNoReq As String
        Dim mHeatNo As String
        Dim mDIRequired As String = "N"
        Dim mODNo As String = ""
        Dim mSOAmendNo As Long
        Dim mSOMKey As String
        Dim mScheduleQty As Double
        Dim mTotMonthPackQty As Double
        Dim mModelNo As String
        Dim mDrawingNo As String
        Dim mHeight As Double
        Dim mWidth As Double
        Dim xStoreLoc As String

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If


        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColItemCode
                xICode = Trim(.Text)
                If xICode = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    xFGBatchNoReq = "Y"
                Else
                    xFGBatchNoReq = "N"
                End If

                .Col = ColUnit
                xIUOM = Trim(.Text)

                .Col = ColHeatNo
                mHeatNo = Trim(.Text)

                .Col = ColBatchNo
                mLotNo = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColChargeableWidth
                mWidth = Val(.Text)

                .Col = ColChargeableHeight
                mHeight = Val(.Text)

                .Col = ColModel
                mModelNo = Trim(.Text)

                .Col = ColDrawingNo
                mDrawingNo = Trim(.Text)

                .Col = ColStoreLoc
                xStoreLoc = Trim(SprdMain.Text)

                .Col = ColStockQty
                .Text = CStr(GetBalanceStockQty(xICode, (txtDNDate.Text), xIUOM, "PAD", mStockType, mLotNo, ConWH, mDivisionCode, "DSP", Val(txtDNNo.Text), xFGBatchNoReq,, mHeatNo))

                If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "G" Then
                    mSOAmendNo = GetSOMaxAmendNo(CDbl(VB6.Format(Val(txtSONo.Text))))
                    mSOMKey = CDbl(Val(txtSONo.Text) & VB6.Format(mSOAmendNo, "000"))

                    If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDIRequired = MasterNo
                    End If

                    If mDIRequired = "Y" Then
                        .Col = ColODNo
                        mODNo = .Text
                    End If

                    mScheduleQty = GetSalesDSQty(xICode, mDIRequired, mODNo, Trim(txtStoreLoc.Text), mWidth, mHeight, mModelNo, mDrawingNo)
                    mTotMonthPackQty = GetTotMonthDespatchQty(xICode, mDIRequired, mODNo, mWidth, mHeight, mModelNo, mDrawingNo, xStoreLoc)

                    .Col = ColBalScheduleQty
                    .Text = System.Math.Round(mScheduleQty - mTotMonthPackQty, 2)
                Else
                    .Col = ColBalScheduleQty
                    .Text = "0.00"
                End If
            Next
        End With
    End Sub


    Private Sub cboRefType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRefType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If VB.Left(cboRefType.Text, 1) = "U" Then
            txtAmendNo.Enabled = True
            txtSuppFromDate.Enabled = True
            txtSuppToDate.Enabled = True
            cmdPopulateSuppBill.Enabled = True
            cmdShow.Enabled = True
        Else
            txtAmendNo.Enabled = False
            txtSuppFromDate.Enabled = False
            txtSuppToDate.Enabled = False
            cmdPopulateSuppBill.Enabled = False
            cmdShow.Enabled = False
        End If
        FormatSprdMain(-1)
    End Sub

    Private Sub cboRefType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboRefType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If VB.Left(cboRefType.Text, 1) = "U" Then
            chkSaleReturn.Enabled = True
        Else
            chkSaleReturn.Enabled = False
        End If
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub chkSaleReturn_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSaleReturn.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkShipTo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShipTo.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
            If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
                txtShipCustomer.Enabled = False
                cmdsearchShipTo.Enabled = False
            Else
                txtShipCustomer.Enabled = True
                cmdsearchShipTo.Enabled = True
            End If
        End If
    End Sub

    Private Sub cmdPopulateSuppBill_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulateSuppBill.Click
        Call CollectPOData(True)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonDespatch(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonDespatch(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonDespatch(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mReportPrint As Boolean

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForDespatch(SqlStr)

        mTitle = IIf(lblDespType.Text = "1", "Despatch Note", IIf(lblDespType.Text = "2", "Gate Pass for Vendor Rejection", "Despatch Note"))

        mSubTitle = "" '' "See Section 34 of CGST Act, 2017 read with Rule 53 of CGST Rules"
        mRptFileName = IIf(lblDespType.Text = "2", "VendorRejection.rpt", "Despatch.rpt") ' "VendorRejection.rpt","Despatch.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)



        Exit Sub
ERR1:
        frmPrintRGP_F4.Close()
        MsgInformation(Err.Description)
    End Sub

    Private Function SelectQryForDespatch(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf _
            & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf _
            & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf _
            & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf _
            & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf _
            & " CMST.LST_NO"

        ''FROM CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BMST, FIN_SUPP_CUST_BUSINESS_MST BMST1, INV_ITEM_MST INVMST "

        If lblDespType.Text = "2" Then
            mSqlStr = mSqlStr & vbCrLf & ", FIN_DNCN_DET CD"
        End If

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=BMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=BMST.LOCATION_ID" & vbCrLf _
            & " AND IH.COMPANY_CODE=BMST1.COMPANY_CODE" & vbCrLf _
            & " AND IH.SHIPPED_TO_PARTY_CODE=BMST1.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.SHIP_TO_LOC_ID=BMST1.LOCATION_ID" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DESP=" & Val(txtDNNo.Text) & ""


        If lblDespType.Text = "2" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_SO=CD.MKEY AND  ID.ITEM_CODE=CD.ITEM_CODE AND CD.MKEY='" & txtSONo.Text & "'" ''ID.SERIAL_NO=CD.SUBROWNO AND
        End If

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForDespatch = mSqlStr
    End Function

    Private Sub cmdReCalculate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReCalculate.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim CntCheckRow As Integer
        Dim mMainItemDesc As String
        Dim mCheckItemDesc As String
        Dim mCheckRate As Double
        Dim mCheckQty As Double

        If VB.Left(cboRefType.Text, 1) <> "U" Then Exit Sub

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mMainItemDesc = Trim(.Text)
                For CntCheckRow = 1 To SprdPostingDetail.MaxRows
                    SprdPostingDetail.Row = CntCheckRow
                    SprdPostingDetail.Col = 2
                    mCheckItemDesc = Trim(SprdPostingDetail.Text)

                    SprdPostingDetail.Col = 4
                    mCheckQty = Val(SprdPostingDetail.Text)

                    If mMainItemDesc = mCheckItemDesc Then
                        SprdPostingDetail.Col = 1
                        If SprdPostingDetail.Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                            .Col = ColPackQty
                            .Text = CStr(0)
                        End If
                        Exit For
                    End If
                Next
            Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdsearchShipTo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchShipTo.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & "" '' AND SUPP_CUST_TYPE IN ('S','C')"

        'If ADDMode = True Then
        '    SqlStr = SqlStr & vbCrLf & "AND STATUS='O'"
        'End If

        If MainClass.SearchGridMaster((txtShipCustomer.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR", SqlStr) = True Then
            txtShipCustomer.Text = AcName
            ''txtCustomerCode.Text = AcName1
            TxtShipTo.Text = AcName2
            txtShipCustomer_Validating(TxtCustomerName, New System.ComponentModel.CancelEventArgs(False))
        End If

        'If MainClass.SearchGridMaster((TxtShipTo.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
        '    TxtShipTo.Text = AcName
        '    TxtCustomerShipTo_Validating(TxtShipTo, New System.ComponentModel.CancelEventArgs(False))
        'End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchSO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSo.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mInterUnit As String = "N"

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtCustomerCode.Text) <> "" Then
            If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
                If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                    SqlStr = SqlStr & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'"
                ElseIf RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y" Then
                    SqlStr = SqlStr & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'"
                Else
                    SqlStr = SqlStr & " AND DEBITACCOUNTCODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'"
                End If

            Else
                If VB.Left(cboRefType.Text, 1) = "E" Then
                    SqlStr = SqlStr & " AND BUYER_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'"
                Else
                    SqlStr = SqlStr & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'"
                End If
            End If
        End If

        If VB.Left(cboRefType.Text, 1) = "E" Then
            'SqlStr = SqlStr & " AND EXP_INV_MADE='Y' AND DC_MADE='N' AND EXCISE_INV_MADE='N' "

            'If MainClass.SearchGridMaster(txtSONo.Text, "DSP_PACKING_HDR", "AUTO_KEY_PACK", "PACK_DATE", "BUYER_PO", "BUYER_PO_DATE", SqlStr) = True Then
            '    txtSONo.Text = AcName
            '    txtSONo_Validating(txtSONo, New System.ComponentModel.CancelEventArgs(False))
            'End If
            SqlStr = " SELECT IH.AUTO_KEY_PACK, EH.BILLNO, IH.PACK_DATE, IH.BUYER_PO, IH.BUYER_PO_DATE, ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.PACKED_QTY" & vbCrLf _
                    & " FROM DSP_PACKING_HDR IH, DSP_PACKING_DET ID, FIN_EXPINV_HDR EH, INV_ITEM_MST INVMST " & vbCrLf _
                    & " WHERE IH.AUTO_KEY_PACK=EH.AUTO_KEY_PACK " & vbCrLf _
                    & " AND IH.AUTO_KEY_PACK=ID.AUTO_KEY_PACK " & vbCrLf _
                    & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND IH.Company_Code=INVMST.Company_Code " & vbCrLf _
                    & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                    & " AND IH.EXP_INV_MADE='Y' AND IH.DC_MADE='N' AND IH.EXCISE_INV_MADE='N' " & vbCrLf _
                    & " AND IH.BUYER_CODE ='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'"

            If MainClass.SearchGridMasterBySQL2(txtSONo.Text, SqlStr) = True Then
                txtSONo.Text = AcName
                txtExportInvoiceNo.Text = AcName1
                txtSONo_Validating(txtSONo, New System.ComponentModel.CancelEventArgs(False))
            End If



        ElseIf VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
            '        SqlStr = SqlStr & " AND (SALEINVOICENO IS NULL OR SALEINVOICENO='') AND CANCELLED='N' AND APPROVED='Y' AND BOOKCODE=" & ConDebitNoteBookCode & " AND VTYPE='DR'"
            '
            '        If MainClass.SearchGridMaster(txtSONo.Text, "FIN_DNCN_HDR", "MKEY", "VNO", "VDATE", "", SqlStr) = True Then
            '            txtSONo.Text = AcName
            '            txtCustPoNo.Text = AcName1
            '            txtSONo_Validate False
            '        End If

            If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then

                SqlStr = "SELECT DISTINCT IH.AUTO_KEY_PO, IH.AMEND_NO, IH.PUR_ORD_DATE" & vbCrLf _
                    & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID,INV_ITEM_MST INVMST " & vbCrLf _
                    & " WHERE IH.MKEY=ID.MKEY AND ID.Company_Code=INVMST.COMPANY_CODE AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                    & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND IH.PUR_TYPE='P' AND PO_STATUS ='Y' AND PO_CLOSED='N'"


                If Trim(txtCustomerCode.Text) <> "" Then
                    SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'"
                End If

                If Trim(txtBillTo.Text) <> "" Then
                    SqlStr = SqlStr & " AND BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
                End If

                If MainClass.SearchGridMasterBySQL2(txtSONo.Text, SqlStr) = True Then
                    txtSONo.Text = AcName
                    txtAmendNo.Text = AcName1
                    txtSODate.Text = AcName2
                    txtSONo_Validating(txtSONo, New System.ComponentModel.CancelEventArgs(False))
                End If

            Else
                SqlStr = "SELECT DISTINCT IH.MKEY, IH.VNO, IH.VDATE" & vbCrLf _
                    & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID,INV_ITEM_MST INVMST " & vbCrLf _
                    & " WHERE IH.MKEY=ID.MKEY AND ID.Company_Code=INVMST.COMPANY_CODE AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                    & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND IH.BOOKCODE=" & ConDebitNoteBookCode & " AND IH.BOOKTYPE='E'" & vbCrLf _
                    & " AND IH.DNCNTYPE='R' AND CANCELLED='N' AND APPROVED='Y' AND VTYPE='DR'" & vbCrLf _
                    & " AND (DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.ITEM_QTY)> " & vbCrLf _
                    & " GETREJDESPATCHQTY (IH.COMPANY_CODE, IH.MKEY,IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE) "

                SqlStr = SqlStr & vbCrLf _
                    & "+ GETREJCREDITQTY (IH.COMPANY_CODE, IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE,DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR)) "

                SqlStr = SqlStr & vbCrLf _
                    & "AND IH.FYEAR>=2010"

                '        CREATE OR REPLACE FUNCTION
                'mCompanyCode NUMBER,pSupplierCode CHAR, pMRRNo NUMBER, mITEMCODE CHAR, mFACTOR NUMBER)

                If CDate(txtDNDate.Text) < CDate(PubGSTApplicableDate) Then

                Else
                    SqlStr = SqlStr & " AND IH.VDATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                If Trim(txtCustomerCode.Text) <> "" Then
                    SqlStr = SqlStr & vbCrLf & "AND IH.DEBITACCOUNTCODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'"
                End If

                If VB.Left(cboRefType.Text, 1) = "Q" Then
                    SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM='M'"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM='S'"

                End If

                If Trim(txtBillTo.Text) <> "" Then
                    SqlStr = SqlStr & " AND BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
                End If

                If MainClass.SearchGridMasterBySQL2(txtSONo.Text, SqlStr) = True Then
                    txtSONo.Text = AcName
                    txtCustPoNo.Text = AcName1
                    txtSONo_Validating(txtSONo, New System.ComponentModel.CancelEventArgs(False))
                End If

            End If




        ElseIf VB.Left(cboRefType.Text, 1) = "U" Then  'Left(cboRefType.Text, 1) = "S" Then  --22-09-2014
            SqlStr = SqlStr & " AND SO_APPROVED='Y'"

            If Trim(txtBillTo.Text) <> "" Then
                SqlStr = SqlStr & " AND BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
            End If

            If MainClass.SearchGridMaster(txtSONo.Text, "DSP_SALEORDER_HDR", "AUTO_KEY_SO", "AMEND_NO", "CUST_PO_NO", "CUST_AMEND_NO", SqlStr) = True Then
                txtSONo.Text = AcName
                txtAmendNo.Text = AcName1
                txtSONo_Validating(txtSONo, New System.ComponentModel.CancelEventArgs(False))
            End If
        Else
            SqlStr = " SELECT IH.AUTO_KEY_SO, IH.AMEND_NO, IH.CUST_PO_NO, IH.CUST_PO_DATE, " & vbCrLf _
                & " ID.ITEM_CODE, ID.PART_NO, IMST.ITEM_SHORT_DESC, ID.UOM_CODE" & vbCrLf _
                & "  FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, INV_ITEM_MST IMST" & vbCrLf _
                & "  WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & "  AND IH.MKEY=ID.MKEY" & vbCrLf _
                & "  AND IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                & "  AND ID.ITEM_CODE=IMST.ITEM_CODE"

            SqlStr = SqlStr & vbCrLf _
                & " AND IH.SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "' AND IH.ISGSTENABLE_PO='Y' AND ID.SO_ITEM_STATUS = 'N' "

            SqlStr = SqlStr & vbCrLf _
                & " AND IH.SO_STATUS='O' AND IH.SO_APPROVED='Y' "

            'SqlStr = SqlStr & vbCrLf _
            '    & " AND IH.SO_STATUS='O'"

            If CDate(txtDNDate.Text) >= CDate(PubGSTApplicableDate) Then
                If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Then
                    SqlStr = SqlStr & " And GOODS_SERVICE='S'"
                Else
                    SqlStr = SqlStr & " AND GOODS_SERVICE='G'"
                End If
            End If

            If Trim(txtBillTo.Text) <> "" Then
                SqlStr = SqlStr & " AND BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
            End If





            If MainClass.SearchGridMasterBySQL2(txtSONo.Text, SqlStr) = True Then        ''If MainClass.SearchGridMaster(txtSONo.Text, "DSP_SALEORDER_HDR", "AUTO_KEY_SO", "SO_DATE", "CUST_PO_NO", "CUST_PO_DATE", SqlStr) = True Then
                txtSONo.Text = AcName
                txtSONo_Validating(txtSONo, New System.ComponentModel.CancelEventArgs(False))
            End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        Dim cntRowMain As Integer
        Dim cntRow As Integer
        Dim cntRowSub As Integer
        Dim mCheckItemCode As String
        'Dim mCheckItemDesc As String
        'Dim mCheckBillRate As Double
        Dim mItemDesc As String
        Dim mShowItemCode As String
        Dim mItemCode As String
        Dim mBillQty As Double


        FraPostingDtl.Visible = Not FraPostingDtl.Visible
        If FraPostingDtl.Visible = True Then
            MainClass.ClearGrid(SprdPostingDetail)
            FraPostingDtl.Enabled = True
            SprdPostingDetail.Enabled = True
            cntRow = 1
            For cntRow = 1 To SprdMain.MaxRows
                SprdMain.Row = cntRow
                SprdMain.Col = ColItemCode
                mCheckItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColItemDesc
                mItemDesc = Trim(SprdMain.Text)

                For cntRowSub = 1 To SprdPostingDetail.MaxRows
                    SprdPostingDetail.Row = cntRowSub
                    SprdPostingDetail.Col = 2
                    mItemCode = Trim(SprdPostingDetail.Text)

                    If (mCheckItemCode = mItemCode) Then
                        GoTo NextRec
                    End If
                Next

                cntRowMain = 1
                For cntRowMain = 1 To SprdMain.MaxRows
                    SprdMain.Row = cntRowMain

                    SprdMain.Col = ColItemCode
                    mShowItemCode = Trim(SprdMain.Text)

                    If mShowItemCode = mCheckItemCode Then
                        SprdMain.Col = ColPackQty
                        mBillQty = mBillQty + Val(SprdMain.Text)
                    End If
                Next
                SprdPostingDetail.Row = SprdPostingDetail.MaxRows
                SprdPostingDetail.Col = 2
                SprdPostingDetail.Value = mCheckItemCode

                SprdPostingDetail.Col = 3
                SprdPostingDetail.Text = mItemDesc

                SprdPostingDetail.Col = 4
                SprdPostingDetail.Text = VB6.Format(mBillQty, "0.00")


                SprdPostingDetail.MaxRows = SprdPostingDetail.MaxRows + 1
NextRec:
                mShowItemCode = ""
                mBillQty = 0
            Next
            Call FormatSprdPostingDetail(-1)
        End If
    End Sub
    Private Sub FormatSprdPostingDetail(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer

        With SprdPostingDetail
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = 1
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(1, 2)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(2, 6)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(3, 25)

            For I = 4 To 4
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 7.5)
            Next

        End With

        MainClass.UnProtectCell(SprdPostingDetail, 1, SprdPostingDetail.MaxRows, 1, 4)
        MainClass.ProtectCell(SprdPostingDetail, 1, SprdPostingDetail.MaxRows, 2, 4)

        MainClass.SetSpreadColor(SprdPostingDetail, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Sub SprdMain_LeaveRow(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveRowEvent) Handles SprdMain.LeaveRow
        '    SprdMain.Row=eventArgs.Row
        '    SprdMain.Row2 = Row
        '    SprdMain.Col = 1
        '    SprdMain.col2 = SprdMain.ActiveCol
        '    SprdMain.BlockMode = True
        '    SprdMain.BackColor = &HFFFF80
        '    SprdMain.BlockMode = False
    End Sub



    Private Sub txtaddress_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAddress.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCustomerCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomerCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomerCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomerCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCustomerCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomerCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAddress As String

        If Trim(txtCustomerCode.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, " & vbCrLf & " SUPP_CUST_STATE, SUPP_CUST_PIN " & vbCrLf & " FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCustomerCode.Text)) & "'"

        If ADDMode = True Then
            SqlStr = SqlStr & vbCrLf & "AND STATUS='O'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            TxtCustomerName.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
            'mAddress = Trim(IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value))
            'mAddress = mAddress & ", " & IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
            'mAddress = mAddress & ", " & IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

            'txtAddress.Text = mAddress
            mCustomerCode = txtCustomerCode.Text
        Else
            mCustomerCode = "-1"
            TxtCustomerName.Text = ""
            'txtAddress.Text = ""
            MsgInformation("Please Check Customer / Supplier Master. Customer is Closed.")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCustomerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCustomerName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomerName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCustomerName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtCustomerName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtCustomerName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtCustomerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub




    Private Sub txtCustPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustPODate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustPoNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustPoNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDNNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDNNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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

    Private Sub txtLoadingTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoadingTime.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPrepared_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrepared.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPrepared_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPrepared.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPrepared.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSODate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSONo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSONo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSONo.DoubleClick
        cmdSearchSO_Click(cmdSearchSo, New System.EventArgs())
    End Sub

    Private Sub txtSONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSONo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSONo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchSO_Click(cmdSearchSo, New System.EventArgs())
    End Sub
    Private Sub txtSONo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel


        If Trim(txtSONo.Text) = "" Then
            txtSODate.Text = ""
            txtCustPoNo.Text = ""
            txtCustPODate.Text = ""
        End If

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            txtSONo.Text = ""
            GoTo EventExitSub
        End If


        If ADDMode = False And MODIFYMode = False Then GoTo EventExitSub
        'If ADDMode = True Then
        '    If VB.Left(cboRefType.Text, 1) = "U" Then
        '        Call CollectPOData(False)
        '    Else
        '        Call CollectPOData(True)
        '    End If
        'End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtDNNo.Enabled = False
            If cboRefType.Enabled = True Then cboRefType.Focus()
        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click

        On Error GoTo DelErrPart

        Dim xDCNo As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBookCode As Integer

        If ValidateBranchLocking((txtDNDate.Text)) = True Then
            Exit Sub
        End If
        If ValidateBookLocking(PubDBCn, CInt(ConLockDespatch), txtDNDate.Text) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, txtDNDate.Text, (TxtCustomerName.Text), mCustomerCode) = True Then
            Exit Sub
        End If

        If cboStatus.SelectedIndex = 1 Or cboStatus.SelectedIndex = 2 Then
            MsgInformation("Transaction Made Against This Despatch Note So Cann't be Deleted")
            Exit Sub
        End If


        If MainClass.ValidateWithMasterTable((txtDNNo.Text), "AUTO_KEY_DESP", "BILLNO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "") = True Then
            MsgBox("Invoice (" & MasterNo & ") had Made Against This Despatch Note So Cann't be Deleted", MsgBoxStyle.Information)
            Exit Sub
        End If


        If Trim(txtDNNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        '    If CheckBillPayment(mCustomerCode, txtBillNo.Text, "B") = True Then Exit Sub

        If Not RsDNMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "DSP_DESPATCH_HDR", (LblMkey.Text), RsDNMain, "AUTO_KEY_DESP", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "DSP_DESPATCH_DET", (LblMkey.Text), RsDNDetail, "AUTO_KEY_DESP", "D") = False Then GoTo DelErrPart
                'If InsertIntoDelAudit(PubDBCn, "DSP_DESPATCH_EXP", (LblMkey.Text), RsDNexp, "MKEY", "D") = False Then GoTo DelErrPart


                If InsertIntoDeleteTrn(PubDBCn, "DSP_DESPATCH_HDR", "AUTO_KEY_DESP", (LblMkey.Text)) = False Then GoTo DelErrPart

                If DeleteCRTRN(PubDBCn, ConStockRefType_DSP, (LblMkey.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_DSP, (txtDNNo.Text)) = False Then GoTo DelErrPart


                If VB.Left(cboRefType.Text, 1) = "E" Then
                    If UpdatePacking(Val(txtSONo.Text), False) = False Then GoTo DelErrPart
                End If

                PubDBCn.Execute("DELETE FROM DSP_PAINT57F4_TRN WHERE MKey='" & LblMkey.Text & "' AND BookType='D' AND BookSubType='O' AND TRNTYPE='D'")

                If CDbl(lblDespType.Text) = 2 And RsCompany.Fields("FYEAR").Value >= 2018 Then

                    SqlStr = " UPDATE FIN_DNCN_HDR SET UPDATE_FROM='N'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " ISDESPATCHED='N',SALEINVOICENO=''," & vbCrLf & " SALEINVOICEDATE=''" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND BOOKCODE=" & ConDebitNoteBookCode & "" & vbCrLf & " AND MKEY ='" & txtSONo.Text & "'"

                    PubDBCn.Execute(SqlStr)

                    PubDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & UCase(LblMkey.Text) & "' AND BookType='S' AND BookSubType='W'")
                End If

                PubDBCn.Execute("Delete from DSP_DESPATCH_DET Where AUTO_KEY_DESP=" & Val(LblMkey.Text) & "")
                PubDBCn.Execute("Delete from DSP_DESPATCH_HDR Where AUTO_KEY_DESP=" & Val(LblMkey.Text) & "")

                PubDBCn.CommitTrans()
                RsDNMain.Requery() ''.Refresh
                RsDNDetail.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsDNMain.Requery() ''.Refresh
        RsDNDetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If PubUserID <> "G0416" Then
            If VB.Left(cboStatus.Text, 1) = "C" Then
                MsgInformation("Invoice Made For this Despatch Note, so Cann't be Modified")
                Exit Sub
            End If
        End If

        If MainClass.ValidateWithMasterTable((txtDNNo.Text), "AUTO_KEY_DESP", "CANCELLED", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND CANCELLED='Y'") = True Then
            MsgInformation("Invoice Cancelled For this Despatch Note, so Cann't be Modified")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtDNNo.Text), "REF_NO", "AUTO_KEY_LOAD", "DSP_LOADING_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_TYPE='D'") = True Then
            MsgInformation("Loading Slip (" & MasterNo & "), made against this despatch note.")
            Exit Sub
        End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsDNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtDNNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If PubUserID = "G0416" Then
            If MsgQuestion("Are you want to Validate?") = vbYes Then
                If FieldsVarification() = False Then
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Sub
                End If
            End If
        Else
            If FieldsVarification() = False Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
        End If

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtDNNo_Validating(txtDNNo, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True And cmdAdd.Visible = True Then cmdAdd.Focus()
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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & ""  '' And SUPP_CUST_TYPE In ('S','C')"

        'If ADDMode = True Then
        '    SqlStr = SqlStr & vbCrLf & "AND STATUS='O'"
        'End If

        If MainClass.SearchGridMaster((TxtCustomerName.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR", SqlStr) = True Then
            TxtCustomerName.Text = AcName
            txtCustomerCode.Text = AcName1
            txtBillTo.Text = AcName2
            txtCustomerName_Validating(TxtCustomerName, New System.ComponentModel.CancelEventArgs(False))
        End If



        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SearchVehicleMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtVehicleNo.Text), "FIN_VEHICLE_MST", "NAME", "CODE", , , SqlStr) = True Then
            txtVehicleNo.Text = AcName
            txtVehicleNo_Validating(txtVehicleNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim xICode As String
        'Dim xPoNo As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pMainItemCode As String
        Dim mStdQty As Double
        Dim mManyItemIn As Boolean
        Dim mLotNo As String
        Dim mHeatNo As String

        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mRow As Integer
        Dim mDivisionCode As Double
        'Dim mShippedCode As String

        Dim mInterUnit As String = "N"

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        If VB.Left(cboRefType.Text, 1) = "E" Then
            GoTo ExportRow
        Else
            If VB.Left(cboRefType.Text, 1) = "P" Or Trim(txtSONo.Text) <> "" Then
                mWithOutOrder = False
            Else
                mWithOutOrder = True
            End If
        End If

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        'If MainClass.ValidateWithMasterTable(Trim(TxtShipTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mShippedCode = Trim(MasterNo)
        'Else
        '    mShippedCode = "-1"
        'End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                If mWithOutOrder = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode


                    SqlStr = "SELECT PODETAIL.ITEM_CODE,INV.ITEM_SHORT_DESC,PODETAIL.CUSTOMER_ITEM_NO " & vbCrLf _
                        & " FROM FIN_SUPP_CUST_DET PODETAIL,INV_ITEM_MST INV " & vbCrLf _
                        & " WHERE " & vbCrLf _
                        & " PODETAIL.COMPANY_CODE=INV.COMPANY_CODE" & vbCrLf _
                        & " AND PODETAIL.ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
                        & " AND PODETAIL.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND PODETAIL.ITEM_CODE LIKE '" & MainClass.AllowSingleQuote(UCase(.Text)) & "%'" & vbCrLf _
                        & " AND SUPP_CUST_CODE='" & Val(txtCustomerCode.Text) & "' "

                    SqlStr = SqlStr & vbCrLf & " ORDER BY ITEM_CODE"
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColItemCode
                        .Text = Trim(AcName)
                    End If
                Else
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then

                        If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                            SqlStr = "SELECT DISTINCT PODETAIL.ITEM_CODE,INV.ITEM_SHORT_DESC, INV.CUSTOMER_PART_NO" & vbCrLf _
                                   & " FROM PUR_PURCHASE_HDR POH, PUR_PURCHASE_DET PODETAIL,INV_ITEM_MST INV " & vbCrLf _
                                   & " WHERE POH.MKEY=PODETAIL.MKEY AND POH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                   & " AND PODETAIL.COMPANY_CODE=INV.COMPANY_CODE" & vbCrLf _
                                   & " AND PODETAIL.ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
                                   & " AND PODETAIL.ITEM_CODE LIKE '" & MainClass.AllowSingleQuote(UCase(.Text)) & "%'" & vbCrLf _
                                   & " AND POH.AUTO_KEY_PO=" & Val(txtSONo.Text) & " AND POH.PUR_TYPE='P' AND PO_STATUS ='Y' AND PO_CLOSED='N'"

                            SqlStr = SqlStr & vbCrLf & " ORDER BY ITEM_CODE"
                        Else
                            SqlStr = "SELECT PODETAIL.ITEM_CODE,INV.ITEM_SHORT_DESC, INV.CUSTOMER_PART_NO" & vbCrLf _
                                   & " FROM FIN_DNCN_DET PODETAIL,INV_ITEM_MST INV " & vbCrLf _
                                   & " WHERE PODETAIL.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                   & " AND PODETAIL.COMPANY_CODE=INV.COMPANY_CODE" & vbCrLf _
                                   & " AND PODETAIL.ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
                                   & " AND PODETAIL.ITEM_CODE LIKE '" & MainClass.AllowSingleQuote(UCase(.Text)) & "%'" & vbCrLf _
                                   & " AND PODETAIL.MKEY=" & Val(txtSONo.Text) & " "

                            SqlStr = SqlStr & vbCrLf & " ORDER BY ITEM_CODE"
                        End If
                    Else
                        SqlStr = "SELECT DISTINCT PODETAIL.ITEM_CODE,INV.ITEM_SHORT_DESC, BILL_TO_LOC_ID, SHIP_TO_LOC_ID"



                        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                            SqlStr = SqlStr & vbCrLf _
                            & ", GLASS_DESC, PODETAIL.ITEM_MODEL, PODETAIL.ITEM_DRAWINGNO, PODETAIL.CHARGEABLE_WIDTH, PODETAIL.CHARGEABLE_HEIGHT,  PODETAIL.ACTUAL_WIDTH, PODETAIL.ACTUAL_HEIGHT, PODETAIL.CHARGEABLEGLASS_AREA, PODETAIL.PART_NO"
                        Else
                            SqlStr = SqlStr & vbCrLf & ", PODETAIL.PART_NO"
                        End If


                        SqlStr = SqlStr & vbCrLf _
                            & " FROM DSP_SALEORDER_HDR POMAIN, DSP_SALEORDER_DET PODETAIL,INV_ITEM_MST INV " & vbCrLf _
                            & " WHERE POMAIN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND POMAIN.MKEY=PODETAIL.MKEY " & vbCrLf _
                            & " AND POMAIN.COMPANY_CODE=INV.COMPANY_CODE" & vbCrLf _
                            & " AND PODETAIL.ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
                            & " AND PODETAIL.ITEM_CODE LIKE '" & MainClass.AllowSingleQuote(UCase(.Text)) & "%'" & vbCrLf _
                            & " AND POMAIN.AUTO_KEY_SO=" & Val(txtSONo.Text) & " AND POMAIN.SO_STATUS='O' AND SO_APPROVED='Y' AND PODETAIL.SO_ITEM_STATUS = 'N'"



                        If Trim(txtStoreLoc.Text) = "" Then
                            SqlStr = SqlStr & vbCrLf & " AND (PODETAIL.CUST_STORE_LOC='' OR PODETAIL.CUST_STORE_LOC IS NULL)"
                        Else
                            SqlStr = SqlStr & vbCrLf & " AND PODETAIL.CUST_STORE_LOC='" & Trim(txtStoreLoc.Text) & "' "
                        End If


                        '                    SqlStr = SqlStr & "AND POMAIN.MKEY = (" & vbCrLf _
                        ''                        & " SELECT MAX(SSH.MKEY) MKEY FROM DSP_SALEORDER_HDR SSH,DSP_SALEORDER_DET SSD " & vbCrLf _
                        ''                        & " WHERE SSH.MKEY=SSD.MKEY AND SSH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                        ''                        & " AND SSH.AUTO_KEY_SO=" & Val(txtSONo.Text) & " " & vbCrLf _
                        ''                        & " AND SSD.ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(.Text)) & "%'" & vbCrLf _
                        ''                        & " AND SSD.AMEND_WEF<='" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "')"

                        SqlStr = SqlStr & vbCrLf & " ORDER BY ITEM_CODE"
                    End If
                    mRow = .ActiveRow
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = mRow
                        .Col = ColItemCode
                        .Text = Trim(AcName)

                        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                            .Col = ColGlassDescription
                            .Text = Trim(AcName4)

                            .Col = ColModel
                            .Text = Trim(AcName5)

                            .Col = ColDrawingNo
                            .Text = Trim(AcName6)

                            .Col = ColChargeableWidth
                            .Text = Trim(AcName7)

                            .Col = ColChargeableHeight
                            .Text = Trim(AcName8)

                            .Col = ColActualWidth
                            .Text = Trim(AcName9)

                            .Col = ColActualHeight
                            .Text = Trim(AcName10)



                        End If

                    End If
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

                If mWithOutOrder = True Then
                    .Row = .ActiveRow
                    SqlStr = "SELECT INV.ITEM_SHORT_DESC,PODETAIL.ITEM_CODE " & vbCrLf _
                        & " FROM FIN_SUPP_CUST_DET PODETAIL,INV_ITEM_MST INV " & vbCrLf _
                        & " WHERE " & vbCrLf _
                        & " PODETAIL.COMPANY_CODE=INV.COMPANY_CODE" & vbCrLf _
                        & " AND PODETAIL.ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
                        & " AND PODETAIL.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND INV.ITEM_SHORT_DESC LIKE '" & MainClass.AllowSingleQuote(UCase(xIName)) & "%'" & vbCrLf _
                        & " AND SUPP_CUST_CODE='" & Val(txtCustomerCode.Text) & "' "


                    SqlStr = SqlStr & vbCrLf & " ORDER BY ITEM_SHORT_DESC"

                    mRow = .ActiveRow

                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = mRow
                        .Col = ColItemDesc
                        .Text = Trim(AcName)

                        .Col = ColItemCode
                        .Text = Trim(AcName1)

                        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                            .Col = ColModel
                            .Text = Trim(AcName5)

                            .Col = ColDrawingNo
                            .Text = Trim(AcName6)

                            .Col = ColChargeableWidth
                            .Text = Trim(AcName7)

                            .Col = ColChargeableHeight
                            .Text = Trim(AcName8)

                            .Col = ColActualWidth
                            .Text = Trim(AcName9)

                            .Col = ColActualHeight
                            .Text = Trim(AcName10)
                        End If


                    End If
                Else

                    SqlStr = "SELECT DISTINCT  INV.ITEM_SHORT_DESC, PODETAIL.ITEM_CODE,BILL_TO_LOC_ID,SHIP_TO_LOC_ID"

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                        SqlStr = SqlStr & vbCrLf & ", PODETAIL.ITEM_MODEL, PODETAIL.ITEM_DRAWINGNO, PODETAIL.CHARGEABLE_WIDTH, PODETAIL.CHARGEABLE_HEIGHT,  PODETAIL.ACTUAL_WIDTH, PODETAIL.ACTUAL_HEIGHT, PODETAIL.CHARGEABLEGLASS_AREA"
                    End If


                    SqlStr = SqlStr & vbCrLf & vbCrLf _
                        & " FROM DSP_SALEORDER_HDR POMAIN, DSP_SALEORDER_DET PODETAIL,INV_ITEM_MST INV " & vbCrLf _
                        & " WHERE POMAIN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND POMAIN.MKEY=PODETAIL.MKEY " & vbCrLf _
                        & " AND POMAIN.COMPANY_CODE=INV.COMPANY_CODE" & vbCrLf _
                        & " AND PODETAIL.ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
                        & " AND INV.ITEM_SHORT_DESC LIKE '" & MainClass.AllowSingleQuote(UCase(xIName)) & "%'" & vbCrLf _
                        & " AND POMAIN.AUTO_KEY_SO=" & Val(txtSONo.Text) & " AND POMAIN.SO_STATUS='O' AND SO_APPROVED='Y' AND PODETAIL.SO_ITEM_STATUS = 'N'"

                    If Trim(txtStoreLoc.Text) = "" Then
                        SqlStr = SqlStr & vbCrLf & " AND (PODETAIL.CUST_STORE_LOC='' OR PODETAIL.CUST_STORE_LOC IS NULL)"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND PODETAIL.CUST_STORE_LOC='" & Trim(txtStoreLoc.Text) & "' "
                    End If

                    SqlStr = SqlStr & vbCrLf & " ORDER BY INV.ITEM_SHORT_DESC"
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColItemDesc
                        .Text = Trim(AcName)

                        .Col = ColItemCode
                        .Text = Trim(AcName1)

                    End If
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If


        If eventArgs.row = 0 And eventArgs.col = ColMRRNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                xICode = Trim(.Text)
                If Trim(.Text) = "" Then Exit Sub

                If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
                    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                    If VB.Left(cboRefType.Text, 1) = "Q" Then
                        SqlStr = SqlStr & vbCrLf & " AND REJECTED_QTY>0 " & vbCrLf & " AND REJ_RTN_STATUS='N' AND ITEM_CODE='" & xICode & "'"
                    End If

                    '' same customer not required..
                    '' If Trim(txtCustomerCode.Text) <> "" Then
                    ''    SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_CODE='" & txtCustomerCode.Text & "'"
                    ''End If


                    .Row = .ActiveRow
                    .Col = ColMRRNo

                    If MainClass.SearchGridMaster(.Text, "INV_GATE_DET", "AUTO_KEY_MRR", "MRR_DATE", "REJECTED_QTY", , SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColMRRNo
                        .Text = AcName
                    End If
                Else
                    '                SqlStr = "SELECT IH.AUTO_KEY_MRR, IH.MRR_DATE, IH.BILL_NO, " & vbCrLf _
                    ''                        & " SUM(ID.RECEIVED_QTY) - GETDESPATCHQTY(IH.COMPANY_CODE,IH.AUTO_KEY_MRR,ID.ITEM_CODE) AS BAL_QTY,CMST.SUPP_CUST_NAME " & vbCrLf _
                    ''                        & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, FIN_SUPP_CUST_MST CMST " & vbCrLf _
                    ''                        & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf _
                    ''                        & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                    ''                        & " AND ID.ITEM_CODE='" & xICode & "'" '' & vbCrLf _
                    ''                        & " AND IH.SUPP_CUST_CODE='" & txtCustomerCode.Text & "'"
                    '
                    '                SqlStr = SqlStr & vbCrLf & " AND IH.REF_TYPE IN ('I','2') AND ID.STOCK_TYPE='CR'"
                    '                SqlStr = SqlStr & vbCrLf & " HAVING SUM(ID.RECEIVED_QTY)-GETDESPATCHQTY(IH.COMPANY_CODE,IH.AUTO_KEY_MRR,ID.ITEM_CODE)<>0"
                    '                SqlStr = SqlStr & vbCrLf & " GROUP BY  IH.COMPANY_CODE,IH.MRR_DATE, IH.AUTO_KEY_MRR, IH.BILL_NO,ID.ITEM_CODE,CMST.SUPP_CUST_NAME"
                    '                SqlStr = SqlStr & vbCrLf & " ORDER BY TO_DATE(IH.MRR_DATE,'DD-MM-YYYY'), IH.AUTO_KEY_MRR"

                    SqlStr = "SELECT AUTO_KEY_MRR, MRR_DATE, SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY) As ITEM_QTY" & vbCrLf & " FROM DSP_CR_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & xICode & "'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & " AND STOCK_TYPE='CR'" & vbCrLf & " GROUP BY AUTO_KEY_MRR, MRR_DATE " & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY)>0"

                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColMRRNo
                        .Text = AcName
                    End If
                End If
            End With
        End If
ExportRow:
        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColStockType
                    .Text = AcName
                End If
            End With
        End If

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


        If eventArgs.row = 0 And eventArgs.col = ColHeatNo Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                xICode = Trim(.Text)

                .Col = ColLotNo
                mLotNo = Trim(.Text)

                .Col = ColHeatNo
                mHeatNo = Trim(.Text)

                .Col = ColUnit
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                SqlStr = GetItemHeatWiseQry(xICode, (txtDNDate.Text), mUOM, "STR", mStockType, mHeatNo, ConWH, "DSP", Val(txtDNNo.Text))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColHeatNo
                    .Text = Trim(AcName1)
                End If

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHeatNo)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColBatchNo Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                xICode = Trim(.Text)

                .Col = ColBatchNo
                mLotNo = Trim(.Text)

                .Col = ColHeatNo
                mHeatNo = Trim(.Text)

                .Col = ColUnit
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                SqlStr = GetItemLotWiseQry(xICode, (txtDNDate.Text), mUOM, "STR", mStockType, mLotNo, ConWH, "DSP", Val(txtDNNo.Text))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColBatchNo
                    .Text = Trim(AcName1)
                End If

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBatchNo)
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColODNo Then
            Dim mODNo As String

            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                xICode = Trim(.Text)

                .Col = ColODNo
                mODNo = Trim(.Text)

                SqlStr = GetItemODWiseQry(xICode, mODNo)
                If SqlStr = "" Then
                Else
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColODNo
                        .Text = Trim(AcName)
                    End If
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColODNo)
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColRefNo Then
            If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Then

                With SprdMain
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    xICode = Trim(.Text)
                    If xICode = "" Then Exit Sub

                    'sk '02-07-2007

                    pMainItemCode = GetInJobworkItem(xICode, Trim(txtDNDate.Text), mStdQty, mManyItemIn)

                    If mManyItemIn = False Then
                        If pMainItemCode = "" Then
                            xICode = "('" & xICode & "')"
                        Else
                            xICode = "('" & xICode & "'," & pMainItemCode & ")"
                        End If
                    Else
                        xICode = "('" & xICode & "')"
                    End If


                    '                If mManyItemIn = False Then
                    SqlStr = " SELECT TO_CHAR(TRN.PARTY_F4NO) AS PARTY_F4NO,  TRN.PARTY_F4DATE, TO_CHAR(SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)) AS BALQTY " & vbCrLf & " FROM DSP_PAINT57F4_TRN TRN, DSP_PAINT57F4_HDR IH " & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf & " AND TRN.PARTY_F4NO=IH.PARTY_F4NO" & vbCrLf & " AND TRN.PARTY_F4DATE=IH.PARTY_F4DATE" & vbCrLf & " AND TRN.SUPP_CUST_CODE=IH.SUPP_CUST_CODE" & vbCrLf & " AND TRN.ITEM_CODE IN " & xICode & "" & vbCrLf & " AND TRN.SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "'" & vbCrLf & " AND TRN.BOOKTYPE<>'P' AND IH.STATUS ='O'" & vbCrLf & " AND TRN.ISSCRAP='N'"

                    If VB.Left(cboRefType.Text, 1) = "J" Then
                        SqlStr = SqlStr & vbCrLf & " AND IH.ISREJECTION='N'"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND IH.ISREJECTION='Y'"
                    End If

                    If Trim(txtDNNo.Text) <> "" Then
                        SqlStr = SqlStr & vbCrLf & " AND TRN.BILL_NO<>'" & MainClass.AllowSingleQuote(txtDNNo.Text) & "'"
                    End If

                    SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.PARTY_F4NO,TRN.PARTY_F4DATE " & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)>0 ORDER BY TO_CHAR(TRN.PARTY_F4NO)"

                    .Col = ColRefNo
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColRefNo
                        .Text = AcName

                        .Col = ColRefDate
                        .Text = AcName1

                    End If
                    '                Else
                    '                    SqlStr = " SELECT TO_CHAR(TRN.PARTY_F4NO) AS PARTY_F4NO,  TRN.PARTY_F4DATE, TO_CHAR(SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)) AS BALQTY " & vbCrLf _
                    ''                            & " FROM DSP_PAINT57F4_TRN TRN, DSP_PAINT57F4_HDR IH " & vbCrLf _
                    ''                            & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    ''                            & " AND TRN.COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf _
                    ''                            & " AND TRN.PARTY_F4NO=IH.PARTY_F4NO" & vbCrLf _
                    ''                            & " AND TRN.PARTY_F4DATE=IH.PARTY_F4DATE" & vbCrLf _
                    ''                            & " AND TRN.SUPP_CUST_CODE=IH.SUPP_CUST_CODE" & vbCrLf _
                    ''                            & " AND TRN.ITEM_CODE IN " & xICode & "" & vbCrLf _
                    ''                            & " AND TRN.SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "'" & vbCrLf _
                    ''                            & " AND TRN.BOOKTYPE<>'P' AND IH.STATUS ='O'" & vbCrLf _
                    ''                            & " AND TRN.ISSCRAP='N'"
                    '
                    '                    If Left(cboRefType.Text, 1) = "J" Then
                    '                        SqlStr = SqlStr & vbCrLf & " AND IH.ISREJECTION='N'"
                    '                    Else
                    '                        SqlStr = SqlStr & vbCrLf & " AND IH.ISREJECTION='Y'"
                    '                    End If
                    '
                    '                    If Trim(txtDNNo) <> "" Then
                    '                        SqlStr = SqlStr & vbCrLf & " AND TRN.BILL_NO<>'" & MainClass.AllowSingleQuote(txtDNNo) & "'"
                    '                    End If
                    '
                    '                    SqlStr = SqlStr & vbCrLf _
                    ''                            & " GROUP BY TRN.PARTY_F4NO,TRN.PARTY_F4DATE " & vbCrLf _
                    ''                            & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)>0 ORDER BY TO_CHAR(TRN.PARTY_F4NO)"
                    '
                    '                    .Col = ColRefNo
                    '                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    '                        .Row = .ActiveRow
                    '                        .Col = ColRefNo
                    '                        .Text = AcName
                    '
                    '                        .Col = ColRefDate
                    '                        .Text = AcName1
                    '
                    '                    End If
                    '                End If
                End With
            End If
        End If

DelRow:
        Dim DelStatus As Boolean
        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode, DelStatus)
                FormatSprdMain(-1)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
            End If
        End If

    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        Dim xICode As String
        Dim xIUOM As String
        Dim xMRRNo As Double
        Dim xRefNo As String
        'Dim mQty As Double
        Dim mStockType As String = ""
        'Dim pMainItemCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        Dim xInItemCode As String
        Dim xOutItemCode As String
        Dim mInConUnit As Double
        Dim mOutConUnit As Double

        'Dim CntCount As Integer
        'Dim xInItemCode1 As String
        'Dim xInItemCode2 As String
        'Dim xInItemCode3 As String
        Dim mSaveQty As Double
        Dim pRefNo As String
        Dim mIsManyIn As Boolean
        Dim mLotNo As String
        Dim xStoreLoc As String
        Dim mDivisionCode As Double
        Dim mStockTable As String
        Dim xLotNo As String
        'Dim mShippedCode As Double
        Dim xFGBatchNoReq As String
        Dim xODNo As String
        Dim mDIRequired As String = ""
        Dim mQtyPerInnerBox As Double
        Dim mQtyPerOuterBox As Double
        Dim mInnerBoxCode As String = ""
        Dim mOuterBoxCode As String = ""
        Dim mPackQty As Double
        Dim mFillPackingQty As Boolean = True
        Dim mHeatNo As String
        'Dim mDIRequired As String = "N"
        Dim mODNo As String = ""
        Dim mSOAmendNo As Long
        Dim mSOMKey As String
        Dim mScheduleQty As Double
        Dim mTotMonthPackQty As Double
        Dim mWidth As Double
        Dim mHeight As Double
        Dim mModelNo As String
        Dim mDrawingNo As String


        If eventArgs.newRow = -1 Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        'If MainClass.ValidateWithMasterTable(Trim(TxtShipTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mShippedCode = CDbl(Trim(MasterNo))
        'Else
        '    mShippedCode = CDbl("-1")
        'End If

        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColItemCode
        If SprdMain.Text = "" Then Exit Sub

        If VB.Left(cboRefType.Text, 1) = "P" Or Trim(txtSONo.Text) <> "" Then
            mWithOutOrder = False
            If MainClass.ValidateWithMasterTable(Val(txtSONo.Text), "AUTO_KEY_SO", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "' AND SO_APPROVED='Y'") = True Then
                mDIRequired = MasterNo
            Else
                mDIRequired = "N"
            End If
        Else
            mWithOutOrder = True
        End If


        Select Case eventArgs.col
            Case ColItemCode, ColODNo
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = Trim(SprdMain.Text)

                SprdMain.Col = ColBatchNo
                xLotNo = Trim(SprdMain.Text)

                SprdMain.Col = ColODNo
                xODNo = Trim(SprdMain.Text)

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                    SprdMain.Col = ColModel
                    xODNo = xODNo & "-" & Trim(SprdMain.Text)

                    SprdMain.Col = ColDrawingNo
                    xODNo = xODNo & "-" & Trim(SprdMain.Text)

                    SprdMain.Col = ColChargeableArea
                    xODNo = xODNo & "-" & Trim(SprdMain.Text)
                End If

                If xICode = "" Then Exit Sub
                If MainClass.ValidateWithMasterTable(xICode, "Item_Code", "Item_Code", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If mDIRequired = "Y" Then
                        If CheckODBalance(xICode, xODNo) = False Then
                            MsgInformation("No Balance Qty of OD No")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, eventArgs.col)
                            Exit Sub
                        End If
                    End If
                    If CheckDuplicateItem(xICode, xLotNo, xODNo, mDIRequired) = False Then
                        If FillGridRow((txtSONo.Text), xICode, mWithOutOrder) = False Then Exit Sub
                        FormatSprdMain(eventArgs.row)
                        If ADDMode = True Then
                            If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "G" Or VB.Left(cboRefType.Text, 1) = "F" Then
                                '                            MainClass.SetFocusToCell SprdMain, Row, ColStockType
                            ElseIf VB.Left(cboRefType.Text, 1) = "J" Then
                                '                            MainClass.SetFocusToCell SprdMain, Row, ColRefNo
                            Else
                                If VB.Left(cboRefType.Text, 1) = "S" Then
                                    '                                MainClass.SetFocusToCell SprdMain, Row, ColStockType
                                ElseIf VB.Left(cboRefType.Text, 1) <> "U" Then
                                    '                                MainClass.SetFocusToCell SprdMain, Row, ColMRRNo
                                End If
                            End If
                        Else
                            If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Then
                                SprdMain.Row = SprdMain.ActiveRow
                                SprdMain.Col = ColRefNo
                                pRefNo = Trim(SprdMain.Text)
                                If CheckDuplicate57F4(xICode, pRefNo) = True Then
                                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, eventArgs.col)
                                    Exit Sub
                                End If

                                '                            MainClass.SetFocusToCell SprdMain, Row, ColRefNo
                            Else
                                '                            MainClass.SetFocusToCell SprdMain, Row, ColStockType
                            End If
                        End If
                    End If

                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If

            Case ColItemDesc
                SprdMain.Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(SprdMain.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If
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
            Case ColPackQty

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                SprdMain.Col = ColPackQty
                If Val(SprdMain.Text) <> 0 Then
                    SprdMain.Row = eventArgs.row
                    SprdMain.Row2 = eventArgs.row
                    SprdMain.Col = 1
                    SprdMain.Col2 = ColJITCallNo ''SprdMain.ActiveCol
                    SprdMain.BlockMode = True
                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                    SprdMain.BlockMode = False
                Else
                    SprdMain.Row = eventArgs.row
                    SprdMain.Row2 = eventArgs.row
                    SprdMain.Col = 1
                    SprdMain.Col2 = ColJITCallNo '' SprdMain.ActiveCol
                    SprdMain.BlockMode = True
                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    SprdMain.BlockMode = False
                End If

                SprdMain.Col = ColPackQty
                If Val(SprdMain.Text) > 0 Then
                    'Dim mQtyPerInnerBox As Double
                    'Dim mQtyPerOuterBox As Double
                    'Dim mInnerBoxCode As String = ""
                    'Dim mOuterBoxCode As String = ""
                    mPackQty = Val(SprdMain.Text)
                    mInnerBoxCode = ""
                    mOuterBoxCode = ""
                    'Dim mInnerBox As Double

                    mQtyPerInnerBox = GetBoxesStd(xICode, txtCustomerCode.Text, "I", mInnerBoxCode, Trim(txtBillTo.Text))
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                        If mQtyPerInnerBox = 0 Then
                            mQtyPerInnerBox = mPackQty
                        Else
                            mQtyPerInnerBox = Int(mPackQty / mQtyPerInnerBox)
                        End If
                    End If

                    SprdMain.Col = ColInnerBoxQty
                    SprdMain.Text = mQtyPerInnerBox
                    'If mQtyPerInnerBox > 0 And (ADDMode = True Or Val(SprdMain.Text) = 0) Then
                    '    mInnerBox = Int(mPackQty / mQtyPerInnerBox)
                    '    SprdMain.Text = Int(mPackQty / mQtyPerInnerBox)
                    'End If

                    SprdMain.Col = ColPackType
                    If mQtyPerInnerBox > 0 And (ADDMode = True And Trim(SprdMain.Text) = "") Then
                        If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "PACK_TYPE", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            SprdMain.Text = MasterNo
                        End If
                    End If

                    SprdMain.Col = ColInnerBoxCode
                    If mQtyPerInnerBox > 0 And (ADDMode = True Or Trim(SprdMain.Text) = "") Then
                        SprdMain.Text = mInnerBoxCode
                    End If

                    mQtyPerOuterBox = GetBoxesStd(xICode, txtCustomerCode.Text, "O", mOuterBoxCode, Trim(txtBillTo.Text))

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                        If mQtyPerOuterBox = 0 Then
                            mQtyPerOuterBox = mPackQty
                        Else
                            mQtyPerOuterBox = Int(mPackQty / mQtyPerOuterBox)
                        End If
                    ElseIf RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                        If mQtyPerOuterBox = 0 Then
                            mQtyPerOuterBox = mPackQty
                        Else
                            mQtyPerOuterBox = mQtyPerOuterBox       ''IIf(Int(mPackQty / mQtyPerOuterBox) = 0, 1, Int(mPackQty / mQtyPerOuterBox))
                        End If
                    End If

                    SprdMain.Col = ColOuterBoxQty
                    SprdMain.Text = mQtyPerOuterBox

                    'If mQtyPerOuterBox > 0 And (ADDMode = True Or Val(SprdMain.Text) = 0) Then
                    '    SprdMain.Text = Int(mInnerBox / mQtyPerOuterBox)
                    'Else
                    '    SprdMain.Text = "0"
                    'End If

                    SprdMain.Col = ColOuterBoxCode
                    If mQtyPerOuterBox > 0 And (ADDMode = True Or Trim(SprdMain.Text) = "") Then
                        SprdMain.Text = mOuterBoxCode
                    End If

                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If

            Case ColMRRNo
                SprdMain.Row = eventArgs.row
                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                SprdMain.Col = ColMRRNo
                xMRRNo = Val(SprdMain.Text)
                If xMRRNo = 0 Then Exit Sub

                If FillMRRDetail(xICode, xMRRNo) = False Then Exit Sub
                '            MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight
                '            FormatSprdMain SprdMain.MaxRows

            Case ColRefNo
                SprdMain.Row = eventArgs.row
                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                xOutItemCode = "'" & xICode & "'"
                If xICode = "" Then Exit Sub

                SprdMain.Col = ColRefNo
                xRefNo = Trim(SprdMain.Text)
                If xRefNo = "" Then Exit Sub

                If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Then
                    xInItemCode = GetInJobworkItem(xICode, Trim(txtDNDate.Text), mInConUnit, mIsManyIn)

                    If VB.Left(cboRefType.Text, 1) = "R" Then
                        xICode = "('" & xICode & "')"
                        mIsManyIn = False
                    Else
                        If xInItemCode = "" Then
                            xICode = "('" & xICode & "')"
                        Else
                            xICode = "('" & xICode & "'," & xInItemCode & ")"
                        End If
                    End If

                    mOutConUnit = 1

                    If mIsManyIn = False Then
                        If FillREFDetail(eventArgs.row, xInItemCode, xOutItemCode, mInConUnit, mOutConUnit, xRefNo) = False Then Exit Sub
                    Else
                        SprdMain.Row = eventArgs.row
                        SprdMain.Col = ColRefNo
                        SprdMain.Text = ""
                    End If
                End If

                '            MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight
                '            FormatSprdMain SprdMain.MaxRows
            Case ColBatchNo, ColHeatNo

                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = Trim(SprdMain.Text)
                If xICode = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    xFGBatchNoReq = "Y"
                Else
                    xFGBatchNoReq = "N"
                End If

                SprdMain.Col = ColUnit
                xIUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColHeatNo
                mHeatNo = Trim(SprdMain.Text)

                SprdMain.Col = ColBatchNo
                mLotNo = Trim(SprdMain.Text)

                If mLotNo <> "" Then
                    mStockTable = ConInventoryTable
                    If MainClass.ValidateWithMasterTable(mLotNo, "BATCH_NO", "BATCH_NO", mStockTable, PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND ITEM_CODE='" & xICode & "'") = False Then
                        MsgInformation("Invalid Lot No")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColBatchNo)
                        Exit Sub
                    End If
                End If

                SprdMain.Col = ColStockType
                mStockType = Trim(SprdMain.Text)
                If mStockType = "" Then Exit Sub


                SprdMain.Col = ColChargeableWidth
                mWidth = Val(SprdMain.Text)

                SprdMain.Col = ColChargeableHeight
                mHeight = Val(SprdMain.Text)

                SprdMain.Col = ColModel
                mModelNo = Trim(SprdMain.Text)

                SprdMain.Col = ColDrawingNo
                mDrawingNo = Trim(SprdMain.Text)

                SprdMain.Col = ColStoreLoc
                xStoreLoc = Trim(SprdMain.Text)

                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(GetBalanceStockQty(xICode, (txtDNDate.Text), xIUOM, "PAD", mStockType, mLotNo, ConWH, mDivisionCode, "DSP", Val(txtDNNo.Text), xFGBatchNoReq,, mHeatNo))

                If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "G" Then
                    mSOAmendNo = GetSOMaxAmendNo(CDbl(VB6.Format(Val(txtSONo.Text))))
                    mSOMKey = CDbl(Val(txtSONo.Text) & VB6.Format(mSOAmendNo, "000"))

                    If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDIRequired = MasterNo
                    End If

                    If mDIRequired = "Y" Then
                        SprdMain.Col = ColODNo
                        mODNo = SprdMain.Text
                    End If

                    mScheduleQty = GetSalesDSQty(xICode, mDIRequired, mODNo, Trim(txtStoreLoc.Text), mWidth, mHeight, mModelNo, mDrawingNo)
                    mTotMonthPackQty = GetTotMonthDespatchQty(xICode, mDIRequired, mODNo, mWidth, mHeight, mModelNo, mDrawingNo, xStoreLoc)

                    SprdMain.Col = ColBalScheduleQty
                    SprdMain.Text = System.Math.Round(mScheduleQty - mTotMonthPackQty, 2)
                Else
                    SprdMain.Col = ColBalScheduleQty
                    SprdMain.Text = "0.00"
                End If
            Case ColStockType
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                xICode = Trim(SprdMain.Text)
                If xICode = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    xFGBatchNoReq = "Y"
                Else
                    xFGBatchNoReq = "N"
                End If

                SprdMain.Col = ColUnit
                xIUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColHeatNo
                mHeatNo = Trim(SprdMain.Text)

                SprdMain.Col = ColBatchNo
                mLotNo = Trim(SprdMain.Text)

                SprdMain.Col = ColStoreLoc
                xStoreLoc = Trim(SprdMain.Text)

                SprdMain.Col = ColStockType
                mStockType = Trim(SprdMain.Text)
                If mStockType = "" Then Exit Sub


                If VB.Left(cboRefType.Text, 1) = "S" And mStockType <> "CR" And RsCompany.Fields("IS_WAREHOUSE").Value = "N" Then
                    MsgInformation("InValid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                    eventArgs.cancel = True
                    Exit Sub
                End If

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("InValid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                Else
                    If ValidateStockType(PubDBCn, xICode, mStockType) = True Then
                        SprdMain.Row = SprdMain.ActiveRow
                        SprdMain.Col = ColPktQty
                        mSaveQty = Val(SprdMain.Text)

                        SprdMain.Col = ColStockQty
                        SprdMain.Text = CStr(GetBalanceStockQty(xICode, (txtDNDate.Text), xIUOM, "PAD", mStockType, mLotNo, ConWH, mDivisionCode, "DSP", Val(txtDNNo.Text), xFGBatchNoReq,, mHeatNo))

                        If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "G" Then
                            mSOAmendNo = GetSOMaxAmendNo(CDbl(VB6.Format(Val(txtSONo.Text))))
                            mSOMKey = CDbl(Val(txtSONo.Text) & VB6.Format(mSOAmendNo, "000"))

                            If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mDIRequired = MasterNo
                            End If

                            If mDIRequired = "Y" Then
                                SprdMain.Col = ColODNo
                                mODNo = SprdMain.Text
                            End If

                            mScheduleQty = GetSalesDSQty(xICode, mDIRequired, mODNo, Trim(txtStoreLoc.Text), mWidth, mHeight, mModelNo, mDrawingNo)
                            mTotMonthPackQty = GetTotMonthDespatchQty(xICode, mDIRequired, mODNo, mWidth, mHeight, mModelNo, mDrawingNo, xStoreLoc)

                            SprdMain.Col = ColBalScheduleQty
                            SprdMain.Text = System.Math.Round(mScheduleQty - mTotMonthPackQty, 2)
                        Else
                            SprdMain.Col = ColBalScheduleQty
                            SprdMain.Text = "0.00"
                        End If
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))

                    Else
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
                    End If
                End If
            Case ColPackType
                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                SprdMain.Col = ColInnerBoxQty
                mQtyPerInnerBox = SprdMain.Text

                SprdMain.Col = ColPackType
                If mQtyPerInnerBox > 0 And (ADDMode = True And Trim(SprdMain.Text) = "") Then
                    If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "PACK_TYPE", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    End If
                End If

        End Select

        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        ''Dim RsMisc As ADODB.Recordset = Nothing
        'Dim mGrossAmt As Double
        'Dim mQty As Double
        'Dim mMRP As Double
        'Dim mPrice As Double
        'Dim mDisc As Double
        'Dim mPackingStandard As Double
        Dim mItemCode As String
        'Dim mPktQty As Double
        Dim I As Integer
        Dim j As Integer
        Dim mHeight As Double
        Dim mWidth As Double
        Dim mArea As Double

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            With SprdMain
                j = .MaxRows
                For I = 1 To j
                    .Row = I

                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    .Col = ColActualHeight
                    mHeight = Val(.Text)

                    .Col = ColActualWidth
                    mWidth = Val(.Text)

                    .Col = ColArea
                    mArea = VB6.Format(mHeight * mWidth, "0.00")
                    .Text = VB6.Format(mArea, "0.00")

                    .Col = ColChargeableHeight
                    mHeight = Val(.Text)

                    .Col = ColChargeableWidth
                    mWidth = Val(.Text)

                    .Col = ColChargeableArea
                    mArea = VB6.Format(mHeight * mWidth, "0.00")
                    .Text = VB6.Format(mArea, "0.00")

                Next I
            End With
        End If


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Function FillMRRDetail(ByRef pItemCode As String, ByRef pMRRNo As Double) As Boolean

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If Val(CStr(pMRRNo)) = 0 Then Exit Function

        SqlStr = " SELECT INV_GATE_HDR.BILL_NO,INV_GATE_DET.REJECTED_QTY FROM INV_GATE_HDR, INV_GATE_DET " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " INV_GATE_HDR.AUTO_KEY_MRR=INV_GATE_DET.AUTO_KEY_MRR" & vbCrLf _
            & " AND INV_GATE_HDR.Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND INV_GATE_DET.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND INV_GATE_HDR.AUTO_KEY_MRR=" & Val(CStr(pMRRNo)) & ""

        If VB.Left(cboRefType.Text, 1) = "Q" Then
            SqlStr = SqlStr & vbCrLf & " AND REJECTED_QTY>0  "
            '    Else
            '        SqlStr = SqlStr & vbCrLf & " AND RECEIVED_QTY>0"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsTemp
                '            SprdMain.C5t = Val(IIf(IsNull(!BILL_NO), "", !BILL_NO))

                ''Not Required ''23-09-2005 SK
                '            SprdMain.Col = ColPackQty
                '            SprdMain.Text = Val(IIf(IsNull(!REJECTED_QTY), "", !REJECTED_QTY))
            End With
            FillMRRDetail = True
        Else
            MsgInformation("Either Invalid MRR or Invalid Item Code for Rejected Item")
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColMRRNo)
            FillMRRDetail = False
        End If

        Exit Function
ERR1:
        FillMRRDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GETCRBalanceQty(ByRef pItemCode As String, ByRef pMRRNo As Double) As Double

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        GETCRBalanceQty = 0
        If Val(CStr(pMRRNo)) = 0 Then Exit Function

        SqlStr = "SELECT " & vbCrLf & " SUM(ID.RECEIVED_QTY) AS CR_QTY " & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf & " AND IH.AUTO_KEY_MRR=" & pMRRNo & "" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "'" ''& vbCrLf |            & " AND IH.SUPP_CUST_CODE='" & txtCustomerCode.Text & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.REF_TYPE IN ('2','I') AND ID.STOCK_TYPE='CR'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GETCRBalanceQty = IIf(IsDBNull(RsTemp.Fields("CR_QTY").Value), 0, RsTemp.Fields("CR_QTY").Value)
        End If


        SqlStr = "SELECT " & vbCrLf & " SUM(ID.PACKED_QTY) AS CR_QTY " & vbCrLf & " FROM DSP_DESPATCH_DET ID " & vbCrLf & " WHERE ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "' AND ID.STOCK_TYPE='CR'"

        If Val(txtDNNo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ID.AUTO_KEY_DESP<>" & Val(txtDNNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GETCRBalanceQty = GETCRBalanceQty - IIf(IsDBNull(RsTemp.Fields("CR_QTY").Value), 0, RsTemp.Fields("CR_QTY").Value)
        End If

        Exit Function
ERR1:
        GETCRBalanceQty = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GETDNBalanceQty(ByRef pItemCode As String, ByRef pMRRNo As Double) As Double

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        GETDNBalanceQty = 0
        '    If Val(pMRRNo) = 0 Then Exit Function

        If Trim(pItemCode) = "" Then Exit Function

        SqlStr = "SELECT " & vbCrLf _
            & " SUM(ID.ITEM_QTY) AS DN_QTY " & vbCrLf _
            & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.MKEY=" & Val(txtSONo.Text) & "" & vbCrLf _
            & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
            & " AND IH.DEBITACCOUNTCODE='" & txtCustomerCode.Text & "'"

        SqlStr = SqlStr & vbCrLf & " AND APPROVED='Y' AND CANCELLED='N'"

        If CDate(txtDNDate.Text) >= CDate("17/03/2019") Then
            SqlStr = SqlStr & vbCrLf & " AND ID.MRR_REF_NO='" & pMRRNo & "'"
            If VB.Left(cboRefType.Text, 1) = "Q" Then
                SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM='M'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM='S'"
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GETDNBalanceQty = IIf(IsDBNull(RsTemp.Fields("DN_QTY").Value), 0, RsTemp.Fields("DN_QTY").Value)
        End If


        SqlStr = "SELECT " & vbCrLf & " SUM(ID.PACKED_QTY) AS RJ_QTY " & vbCrLf & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID " & vbCrLf & " WHERE ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & "" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "' AND DESP_STATUS<>2"

        If Val(txtDNNo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_DESP<>" & Val(txtDNNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GETDNBalanceQty = GETDNBalanceQty - IIf(IsDBNull(RsTemp.Fields("RJ_QTY").Value), 0, RsTemp.Fields("RJ_QTY").Value)
        End If

        Exit Function
ERR1:
        GETDNBalanceQty = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function FillREFDetail(ByRef pRow As Integer, ByRef pInItemCode As String, ByRef pOutItemCode As String, ByRef pInConUnit As Double, ByRef pOutConUnit As Double, ByRef pRefNo As String) As Boolean


        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mCheckF4No As String = ""
        Dim mCheckItemCode As String
        Dim cntRow As Integer
        Dim m57F4Found As Boolean
        Dim mF4Qty As Double
        Dim pItemCode As String
        Dim mF4Date As String
        'Dim mShippedCode As Double

        If Trim(txtCustomerCode.Text) = "" Then
            MsgInformation("Please Select Party Name.")
            FillREFDetail = False
            Exit Function
        End If

        mCustomerCode = Trim(txtCustomerCode.Text)

        'If MainClass.ValidateWithMasterTable(Trim(TxtShipTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mShippedCode = CDbl(Trim(MasterNo))
        'Else
        '    mShippedCode = CDbl("-1")
        'End If

        If Trim(pRefNo) = "" Then MsgInformation("Please Select 57F4 No.") : Exit Function

        If pInItemCode = "" Then
            pItemCode = "(" & pOutItemCode & ")"
        Else
            pItemCode = "(" & pInItemCode & "," & pOutItemCode & ")"
        End If

        If CheckDuplicate57F4(pItemCode, pRefNo) = True Then
            '        MsgInformation "Duplicate F4No for Such Item."
            '        MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColRefNo
            FillREFDetail = False
            Exit Function
        End If

        SqlStr = " SELECT MIN(PARTY_F4DATE) AS PARTY_F4DATE" & vbCrLf & " FROM DSP_PAINT57F4_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & txtCustomerCode.Text & "'" & vbCrLf & " AND TRIM(PARTY_F4NO)='" & MainClass.AllowSingleQuote(Trim(pRefNo)) & "'" & vbCrLf & " AND ITEM_CODE IN " & pItemCode & " " & vbCrLf & " AND ISSCRAP='N' " & vbCrLf & " "

        If Trim(txtDNNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND BILL_NO<>'" & MainClass.AllowSingleQuote(txtDNNo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)>0 GROUP BY PARTY_F4DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mF4Date = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PARTY_F4DATE").Value), "", RsTemp.Fields("PARTY_F4DATE").Value), "DD/MM/YYYY")
        Else
            MsgInformation("Invaild 57F4 No :" & mCheckF4No & " . Please Check.")
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRefNo)
            FillREFDetail = False
            Exit Function
        End If


        SqlStr = " SELECT DISTINCT TRN.PARTY_F4NO AS PARTY_F4NO" & vbCrLf & " FROM DSP_PAINT57F4_TRN TRN, DSP_PAINT57F4_HDR IH" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf & " AND TRN.PARTY_F4NO=IH.PARTY_F4NO" & vbCrLf & " AND TRN.PARTY_F4DATE=IH.PARTY_F4DATE" & vbCrLf & " AND TRN.SUPP_CUST_CODE=IH.SUPP_CUST_CODE" & vbCrLf & " AND TRN.SUPP_CUST_CODE='" & txtCustomerCode.Text & "'" & vbCrLf & " AND TRN.ITEM_CODE IN " & pItemCode & ""

        SqlStr = SqlStr & vbCrLf & " AND TRN.PARTY_F4DATE< TO_DATE('" & VB6.Format(mF4Date, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If Trim(txtDNNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.BILL_NO<>'" & MainClass.AllowSingleQuote(txtDNNo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.ISSCRAP='N'"

        If VB.Left(cboRefType.Text, 1) = "J" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.ISREJECTION='N'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.ISREJECTION='Y'"
        End If

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(TRN.ITEM_IO,'I',1,-1)*TRN.ITEM_QTY)>0 "

        SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.PARTY_F4NO "

        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.PARTY_F4NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mCheckF4No = IIf(IsDBNull(RsTemp.Fields("PARTY_F4NO").Value), "", RsTemp.Fields("PARTY_F4NO").Value)

                m57F4Found = False
                With SprdMain
                    For cntRow = 1 To .MaxRows
                        .Row = cntRow
                        .Col = ColItemCode
                        mCheckItemCode = Trim(.Text)

                        .Col = ColPackQty
                        mF4Qty = Val(.Text)

                        '                If UCase(Trim(mCheckItemCode)) = UCase(Trim(pItemCode)) Then
                        If InStr(1, UCase(Trim(pItemCode)), UCase(Trim(mCheckItemCode))) > 0 Then
                            .Col = ColRefNo
                            If mCheckF4No = Trim(.Text) And mF4Qty > 0 Then
                                m57F4Found = True
                            End If
                        End If
                    Next
                End With
                'If m57F4Found = False Then
                '    MsgInformation("Please Clear First 57F4 No :" & mCheckF4No)
                '    If PubUserID <> "G0416" Then
                '        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRefNo)
                '    End If
                '    FillREFDetail = False
                '    Exit Function
                'End If
                RsTemp.MoveNext()
            Loop
        End If


        SqlStr = " SELECT SUM(DECODE(TRN.ITEM_IO,'I',1,-1)*TRN.ITEM_QTY) AS ITEMQTY " & vbCrLf & " FROM DSP_PAINT57F4_TRN TRN, DSP_PAINT57F4_HDR IH" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf & " AND TRN.PARTY_F4NO=IH.PARTY_F4NO" & vbCrLf & " AND TRN.PARTY_F4DATE=IH.PARTY_F4DATE" & vbCrLf & " AND TRN.SUPP_CUST_CODE=IH.SUPP_CUST_CODE" & vbCrLf & " AND TRN.SUPP_CUST_CODE='" & txtCustomerCode.Text & "'" & vbCrLf & " AND TRIM(TRN.PARTY_F4NO)='" & MainClass.AllowSingleQuote(Trim(pRefNo)) & "'" & vbCrLf & " AND TRN.ITEM_CODE IN " & pItemCode & " AND TRN.ISSCRAP='N'"

        If VB.Left(cboRefType.Text, 1) = "J" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.ISREJECTION='N'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.ISREJECTION='Y'"
        End If

        If Trim(txtDNNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.BILL_NO<>'" & MainClass.AllowSingleQuote(txtDNNo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.PARTY_F4DATE=TO_DATE('" & VB6.Format(mF4Date, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(TRN.ITEM_IO,'I',1,-1)*TRN.ITEM_QTY)>0"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            SprdMain.Row = pRow
            With RsTemp
                mF4Qty = Val(IIf(IsDBNull(.Fields("ITEMQTY").Value), "", .Fields("ITEMQTY").Value))

                '            If pInItemCode <> pOutItemCode Then
                mF4Qty = pOutConUnit * mF4Qty / pInConUnit
                '            End If

                SprdMain.Col = Col57BalQty
                SprdMain.Text = CStr(mF4Qty) ''Val(IIf(IsNull(!ITEMQTY), "", !ITEMQTY))

                SprdMain.Col = ColRefDate
                SprdMain.Text = VB6.Format(mF4Date, "DD/MM/YYYY")
            End With
            FillREFDetail = True
        Else
            MsgInformation("Either Invalid 57F4 No or Invalid Item Code for This Item")
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRefNo)
            FillREFDetail = False
        End If

        Exit Function
ERR1:
        FillREFDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function FillGridRow(ByRef mPONo As String, ByRef mItemCode As String, ByRef pWithOutOrder As Boolean) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mOrdQty As Object
        Dim mRecvQty As Double
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim xSupplierCode As Integer
        Dim mOrderSno As Integer
        Dim SqlStr As String = ""
        Dim mStockType As String = ""
        Dim xItemCode As String = ""
        Dim mSaveQty As String
        Dim mLotNo As String
        Dim mDivisionCode As Double
        Dim xFGBatchNoReq As String
        Dim mHeatNo As String

        Dim mDIRequired As String = "N"
        Dim mODNo As String = ""
        Dim mSOAmendNo As Long
        Dim mSOMKey As String
        Dim mScheduleQty As Double
        Dim mTotMonthPackQty As Double
        Dim mWidth As Double
        Dim mHeight As Double
        Dim mModelNo As String
        Dim mDrawingNo As String
        Dim xStoreLoc As String
        Dim mInterUnit As String = "N"

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        If mItemCode = "" Then Exit Function

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        SqlStr = ""
        If VB.Left(cboRefType.Text, 1) = "E" Then
            SqlStr = " SELECT SD.ITEM_CODE,ITEM_SHORT_DESC AS NAME," & vbCrLf _
                & " ISSUE_UOM,CUSTOMER_PART_NO,  0 ACTUAL_HEIGHT, 0 ACTUAL_WIDTH, 0 GLASS_AREA, 0 CHARGEABLE_HEIGHT, 0 CHARGEABLE_WIDTH, 0 CHARGEABLEGLASS_AREA,'' ITEM_MODEL, '' ITEM_DRAWINGNO, '' GLASS_DESC " & vbCrLf _
                & " FROM DSP_PACKING_HDR SH,DSP_PACKING_DET SD,INV_ITEM_MST INVMST" & vbCrLf _
                & " WHERE SH.AUTO_KEY_PACK=SD.AUTO_KEY_PACK " & vbCrLf _
                & " AND SH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                & " AND SD.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                & " AND SH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SH.AUTO_KEY_PACK=" & Val(mPONo) & "" & vbCrLf _
                & " AND SD.ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "

        ElseIf VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then

            If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                If Val(txtSONo.Text) = 0 And ADDMode = True Then
                    MsgBox("Please Select PO No.")
                    If txtSONo.Enabled = True Then txtSONo.Focus()
                    FillGridRow = False
                    Exit Function
                End If
                SqlStr = " SELECT DISTINCT SD.ITEM_CODE,ITEM_SHORT_DESC AS NAME," & vbCrLf _
                    & " ISSUE_UOM,CUSTOMER_PART_NO,  0 ACTUAL_HEIGHT, 0 ACTUAL_WIDTH, 0 GLASS_AREA, 0 AS CHARGEABLE_HEIGHT, 0 AS  CHARGEABLE_WIDTH, 0 CHARGEABLEGLASS_AREA, '' ITEM_MODEL, '' ITEM_DRAWINGNO, '' GLASS_DESC " & vbCrLf _
                    & " FROM PUR_PURCHASE_HDR SH,PUR_PURCHASE_DET SD,INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE SH.MKEY=SD.MKEY" & vbCrLf _
                    & " AND SH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                    & " AND SD.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                    & " AND SD.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SH.AUTO_KEY_PO=" & Val(txtSONo.Text) & "" & vbCrLf _
                    & " And SD.ITEM_CODE ='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "
            Else
                If Val(txtSONo.Text) = 0 And ADDMode = True Then
                    MsgBox("Please Select Debit Note.")
                    If txtSONo.Enabled = True Then txtSONo.Focus()
                    FillGridRow = False
                    Exit Function
                End If
                SqlStr = " SELECT SD.ITEM_CODE,ITEM_SHORT_DESC AS NAME," & vbCrLf _
                    & " ISSUE_UOM,CUSTOMER_PART_NO,  0 ACTUAL_HEIGHT, 0 ACTUAL_WIDTH, 0 GLASS_AREA, 0 AS CHARGEABLE_HEIGHT, 0 AS  CHARGEABLE_WIDTH, 0 CHARGEABLEGLASS_AREA, '' ITEM_MODEL, '' ITEM_DRAWINGNO, '' GLASS_DESC " & vbCrLf _
                    & " FROM FIN_DNCN_DET SD,INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " SD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                    & " AND SD.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                    & " AND SD.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SD.MKEY='" & txtSONo.Text & "'" & vbCrLf _
                    & " AND SD.ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "
            End If


        Else
            If pWithOutOrder = False Then
                SqlStr = " SELECT SD.ITEM_CODE,ITEM_SHORT_DESC AS NAME," & vbCrLf _
                    & " ISSUE_UOM,SD.PART_NO AS CUSTOMER_PART_NO,SD.ACTUAL_HEIGHT, SD.ACTUAL_WIDTH, SD.GLASS_AREA,SD.CHARGEABLE_HEIGHT, SD.CHARGEABLE_WIDTH, SD.CHARGEABLEGLASS_AREA, SD.ITEM_MODEL, SD.ITEM_DRAWINGNO, SD.GLASS_DESC " & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR SH,DSP_SALEORDER_DET SD,INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE SH.MKEY=SD.MKEY " & vbCrLf _
                    & " AND SH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                    & " AND SD.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                    & " AND SH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SH.AUTO_KEY_SO=" & Val(mPONo) & "" & vbCrLf _
                    & " AND SD.ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' AND SO_APPROVED='Y'"

                '            SqlStr = SqlStr & "  AND SH.MKEY = (SELECT MAX(SSH.MKEY) MKEY FROM DSP_SALEORDER_HDR SSH,DSP_SALEORDER_DET SSD " & vbCrLf _
                ''                    & " WHERE SSH.MKEY=SSD.MKEY AND SSH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                ''                    & " AND SSH.AUTO_KEY_SO=" & Val(mPONo) & " " & vbCrLf _
                ''                    & " AND SSD.ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "'" & vbCrLf _
                ''                    & " AND SSD.AMEND_WEF<='" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "'"

                If Trim(txtStoreLoc.Text) = "" Then
                    'SqlStr = SqlStr & vbCrLf & " AND (SD.CUST_STORE_LOC='' OR SD.CUST_STORE_LOC IS NULL)"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND SD.CUST_STORE_LOC='" & Trim(txtStoreLoc.Text) & "' "
                End If

                If cboStatus.SelectedIndex = 0 Then
                    SqlStr = SqlStr & "AND SH.SO_STATUS='O' AND SD.SO_ITEM_STATUS = 'N'"

                End If
                '            SqlStr = SqlStr & ")"
            Else

                SqlStr = " SELECT SD.ITEM_CODE,ITEM_SHORT_DESC AS NAME," & vbCrLf _
                    & " ISSUE_UOM,CUSTOMER_ITEM_NO AS CUSTOMER_PART_NO,0 ACTUAL_HEIGHT, 0 ACTUAL_WIDTH, 0 GLASS_AREA, 0 CHARGEABLE_HEIGHT, 0 CHARGEABLE_WIDTH, 0 CHARGEABLEGLASS_AREA, '' ITEM_MODEL, '' ITEM_DRAWINGNO, '' GLASS_DESC " & vbCrLf _
                    & " FROM FIN_SUPP_CUST_DET SD,INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE  " & vbCrLf & " SD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                    & " AND SD.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                    & " AND SD.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SD.SUPP_CUST_CODE='" & txtCustomerCode.Text & "'" & vbCrLf & " AND SD.ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "'"
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc
                SprdMain.Col = ColItemCode
                xItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    xFGBatchNoReq = "Y"
                Else
                    xFGBatchNoReq = "N"
                End If

                SprdMain.Col = ColItemDesc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("Name").Value), "", .Fields("Name").Value))

                SprdMain.Col = ColPartNo
                SprdMain.Text = IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)


                'SprdMain.Col = ColModel
                'SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value)


                'SprdMain.Col = ColGlassDescription
                'SprdMain.Text = IIf(IsDBNull(.Fields("GLASS_DESC").Value), "", .Fields("GLASS_DESC").Value)

                'SprdMain.Col = ColDrawingNo
                'SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_DRAWINGNO").Value), "", .Fields("ITEM_DRAWINGNO").Value)


                'SprdMain.Col = ColActualHeight
                'SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_HEIGHT").Value), 0, .Fields("ACTUAL_HEIGHT").Value)))

                'SprdMain.Col = ColActualWidth
                'SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_WIDTH").Value), 0, .Fields("ACTUAL_WIDTH").Value)))

                'SprdMain.Col = ColArea
                'SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GLASS_AREA").Value), 0, .Fields("GLASS_AREA").Value)))

                'SprdMain.Col = ColChargeableHeight
                'SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_HEIGHT").Value), 0, .Fields("CHARGEABLE_HEIGHT").Value)))

                'SprdMain.Col = ColChargeableWidth
                'SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_WIDTH").Value), 0, .Fields("CHARGEABLE_WIDTH").Value)))

                'SprdMain.Col = ColChargeableArea
                'SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLEGLASS_AREA").Value), 0, .Fields("CHARGEABLEGLASS_AREA").Value)))

                SprdMain.Col = ColPackQty
                SprdMain.Text = IIf(Val(SprdMain.Text) = 0, 0, Val(SprdMain.Text))

                SprdMain.Col = ColStockType
                If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
                    'RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And 
                    If RsCompany.Fields("IS_WAREHOUSE").Value = "Y" Then
                        mStockType = "ST"
                    Else
                        mStockType = "RJ"
                    End If

                ElseIf VB.Left(cboRefType.Text, 1) = "S" Then
                    mStockType = IIf(RsCompany.Fields("IS_WAREHOUSE").Value = "N", "CR", "ST")
                Else
                    mStockType = GetStockType(PubDBCn, xItemCode, mDivisionCode) 'IIf(SprdMain.Text = "", "FG", SprdMain.Text)
                End If
                SprdMain.Text = If(Trim(SprdMain.Text) = "", mStockType, SprdMain.Text)


                SprdMain.Col = ColHeatNo
                mHeatNo = Trim(SprdMain.Text)

                SprdMain.Col = ColBatchNo
                mLotNo = Trim(SprdMain.Text)

                ''20-10-2010
                '            SprdMain.Col = ColPktQty
                '            mSaveQty = Val(SprdMain.Text)


                SprdMain.Col = ColChargeableWidth
                mWidth = Val(SprdMain.Text)

                SprdMain.Col = ColChargeableHeight
                mHeight = Val(SprdMain.Text)


                SprdMain.Col = ColModel
                mModelNo = Trim(SprdMain.Text)

                SprdMain.Col = ColDrawingNo
                mDrawingNo = Trim(SprdMain.Text)

                SprdMain.Col = ColStoreLoc
                xStoreLoc = Trim(SprdMain.Text)

                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtDNDate.Text), .Fields("ISSUE_UOM").Value, "PAD", mStockType, mLotNo, ConWH, mDivisionCode, "DSP", Val(txtDNNo.Text), xFGBatchNoReq,, mHeatNo))

                If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "G" Then
                    mSOAmendNo = GetSOMaxAmendNo(CDbl(VB6.Format(Val(txtSONo.Text))))
                    mSOMKey = CDbl(Val(txtSONo.Text) & VB6.Format(mSOAmendNo, "000"))

                    If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDIRequired = MasterNo
                    End If

                    If mDIRequired = "Y" Then
                        SprdMain.Col = ColODNo
                        mODNo = SprdMain.Text
                    End If

                    mScheduleQty = GetSalesDSQty(xItemCode, mDIRequired, mODNo, Trim(txtStoreLoc.Text), mWidth, mHeight, mModelNo, mDrawingNo)
                    mTotMonthPackQty = GetTotMonthDespatchQty(xItemCode, mDIRequired, mODNo, mWidth, mHeight, mModelNo, mDrawingNo, xStoreLoc)

                    SprdMain.Col = ColBalScheduleQty
                    SprdMain.Text = System.Math.Round(mScheduleQty - mTotMonthPackQty, 2)
                Else
                    SprdMain.Col = ColBalScheduleQty
                    SprdMain.Text = "0.00"
                End If

                '            SprdMain.Col = ColPDIRFlag
                '            SprdMain.Value = vbChecked
                '
                '            SprdMain.Col = ColSchdRtnFlag
                '            SprdMain.Value = vbUnchecked

            End With
            FillGridRow = True
        Else
            MsgInformation("Invalid Item Code for that Supplier")
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mDNNo As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        mDNNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(0))

        txtDNNo.Text = CStr(Val(mDNNo))

        txtDNNo_Validating(txtDNNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub

    'Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
    '    With SprdView

    '        If eventArgs.row = 0 Then Exit Sub
    '        .Row = eventArgs.row

    '        .Col = 1
    '        txtDNNo.Text = CStr(Val(.Text))

    '        txtDNNo_Validating(txtDNNo, New System.ComponentModel.CancelEventArgs(False))
    '        CmdView_Click(CmdView, New System.EventArgs())
    '    End With
    'End Sub
    Private Sub txtDNDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDNDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDNDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDNDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Not IsDate(txtDNDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtDNDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDNNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDNNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtDNNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDNNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mMRRNo As String
        Dim SqlStr As String = ""

        If Trim(txtDNNo.Text) = "" Then GoTo EventExitSub

        If Len(txtDNNo.Text) < 6 Then
            txtDNNo.Text = VB6.Format(Val(txtDNNo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsDNMain.EOF = False Then xMkey = RsDNMain.Fields("mKey").Value
        mMRRNo = Trim(txtDNNo.Text)

        SqlStr = " SELECT * FROM DSP_DESPATCH_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_DESP=" & Val(mMRRNo) & " "

        SqlStr = SqlStr & vbCrLf & " AND DESPATCHTYPE =" & lblDespType.Text & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsDNMain.EOF = False Then
            Clear1()
            Show1()
            '        TxtCustomerName.Enabled = True
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Despatch Note, Use Generate Despatch Note Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM DSP_DESPATCH_HDR " & " WHERE AUTO_KEY_DESP=" & Val(xMkey) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mVNoSeq As Double
        Dim mSuppCustCode As String
        Dim mDespStatus As String = ""
        Dim mDespType As String
        Dim mDivisionCode As Double
        Dim mDespatchSeqType As Integer
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String

        Dim xBillNo As String = ""
        Dim xBillDate As String = ""
        Dim xVNo As String
        Dim xVDate As String
        Dim xIsGST As String = ""
        Dim xCancelled As String
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mQty As Double
        Dim mRate As Double
        Dim mAmt As Double

        Dim xCGSTPer As Double
        Dim xSGSTPer As Double
        Dim xIGSTPer As Double
        Dim xCGSTAmount As Double
        Dim xSGSTAmount As Double
        Dim xIGSTAmount As Double
        Dim RsDN As ADODB.Recordset = Nothing
        Dim mShippedToCode As String
        Dim mShippedToSame As String

        Dim mTransMode As String
        Dim mVehicleType As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mSuppCustCode = "-1"
        If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = "-1"
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mShippedToSame = IIf(chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If mShippedToSame = "Y" Then
            mShippedToCode = mSuppCustCode
        Else
            If MainClass.ValidateWithMasterTable((txtShipCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mShippedToCode = MasterNo
            Else
                mShippedToCode = "-1"
                MsgBox("Shipped To Customer Does Not Exist In Master", MsgBoxStyle.Information)
                GoTo ErrPart
            End If
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If


        If cboStatus.SelectedIndex = 0 Then
            mDespStatus = "0"
        ElseIf cboStatus.SelectedIndex = 1 Then
            mDespStatus = "1"
        ElseIf cboStatus.SelectedIndex = 2 Then
            mDespStatus = "2"
        End If

        mDespType = VB.Left(cboRefType.Text, 1)

        If lblDespType.Text = "1" Or lblDespType.Text = "2" Then
            mDespatchSeqType = Val(lblDespType.Text)
        Else
            If CDate(VB6.Format(txtDNDate.Text, "DD/MM/YYYY")) >= CDate(PubGSTApplicableDate) Then
                If mDespType = "Q" Or mDespType = "L" Then
                    mDespatchSeqType = 2
                Else
                    mDespatchSeqType = 1
                End If
            Else
                mDespatchSeqType = 0
            End If
        End If


        If Val(txtDNNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo(mDivisionCode))
        Else
            mVNoSeq = Val(txtDNNo.Text)
        End If

        txtDNNo.Text = CStr(Val(CStr(mVNoSeq)))
        '    mVNoSeq = "12519200601"
        '    txtDNDate.Text = "02/08/2006"
        '    txtDNNo.Text = Val(mVNoSeq)
        ''Temp. Commit.....
        If CheckValidVDate(mVNoSeq, mDivisionCode) = False Then GoTo ErrPart

        mTransMode = VB.Left(cboTransmode.Text, 1)
        mVehicleType = VB.Left(cboVehicleType.Text, 1)

        SqlStr = ""

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "DSP_DESPATCH_HDR", (LblMkey.Text), RsDNMain, "AUTO_KEY_DESP", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "DSP_DESPATCH_DET", (LblMkey.Text), RsDNDetail, "AUTO_KEY_DESP", "M") = False Then GoTo ErrPart
        End If

        If ADDMode = True Then
            LblMkey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO DSP_DESPATCH_HDR( " & vbCrLf _
                & " COMPANY_CODE, AUTO_KEY_DESP, DESP_DATE," & vbCrLf _
                & " SUPP_CUST_CODE, " & vbCrLf _
                & " TRANSPORTER_NAME, VEHICLE_NO," & vbCrLf _
                & " LOADING_TIME, PRE_EMP_CODE, " & vbCrLf _
                & " DESP_STATUS, DESP_TYPE, " & vbCrLf _
                & " AUTO_KEY_SO, SO_DATE, " & vbCrLf _
                & " VENDOR_PO, VENDOR_PO_DATE, " & vbCrLf _
                & " GRNO,GRDATE," & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE, DIV_CODE, DESPATCHTYPE, " & vbCrLf _
                & " SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE,BILL_TO_LOC_ID," & vbCrLf _
                & " SHIP_TO_LOC_ID,LOC_CODE,EXPORT_BILL_NO,  TRANSPORT_MODE, VEHICLE_TYPE, TRANSPORTER_GSTNO) "

            SqlStr = SqlStr & vbCrLf _
                & " VALUES(" & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mVNoSeq)) & ", TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((TxtTransporter.Text)) & "', '" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "', " & vbCrLf _
                & " TO_DATE('" & txtLoadingTime.Text & "','HH24:MI')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtPrepared.Text)) & "', " & vbCrLf _
                & " '" & mDespStatus & "','" & mDespType & "', " & vbCrLf _
                & " " & Val(txtSONo.Text) & ",TO_DATE('" & VB6.Format(txtSODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & txtCustPoNo.Text & "',TO_DATE('" & VB6.Format(txtCustPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((TxtGRNo.Text)) & "', TO_DATE('" & VB6.Format(TxtGRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & mDivisionCode & ", " & mDespatchSeqType & "," & vbCrLf _
                & " '" & mShippedToSame & "','" & MainClass.AllowSingleQuote(mShippedToCode) & "','" & MainClass.AllowSingleQuote(txtBillTo.Text) & "','" & MainClass.AllowSingleQuote(TxtShipTo.Text) & "','" & MainClass.AllowSingleQuote(txtStoreLoc.Text) & "'," & vbCrLf _
                & " '" & txtExportInvoiceNo.Text & "','" & mTransMode & "', '" & mVehicleType & "', '" & MainClass.AllowSingleQuote(txtTransportCode.Text) & "')"


        ElseIf MODIFYMode = True Then

            SqlStr = ""
            SqlStr = "UPDATE DSP_DESPATCH_HDR SET " & vbCrLf _
                & " AUTO_KEY_DESP =" & Val(CStr(mVNoSeq)) & " , EXPORT_BILL_NO='" & txtExportInvoiceNo.Text & "'," & vbCrLf _
                & " DESP_DATE=TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'," & vbCrLf _
                & " SHIPPED_TO_SAMEPARTY='" & mShippedToSame & "',SHIPPED_TO_PARTY_CODE='" & MainClass.AllowSingleQuote(mShippedToCode) & "'," & vbCrLf _
                & " TRANSPORTER_NAME='" & MainClass.AllowSingleQuote((TxtTransporter.Text)) & "', " & vbCrLf _
                & " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote((txtBillTo.Text)) & "', " & vbCrLf _
                & " SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote((TxtShipTo.Text)) & "', " & vbCrLf _
                & " VEHICLE_NO='" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "', " & vbCrLf _
                & " TRANSPORTER_GSTNO ='" & MainClass.AllowSingleQuote(txtTransportCode.Text) & "'," & vbCrLf _
                & " TRANSPORT_MODE='" & mTransMode & "'," & vbCrLf _
                & " VEHICLE_TYPE='" & mVehicleType & "'," & vbCrLf _
                & " LOADING_TIME=TO_DATE('" & txtLoadingTime.Text & "','HH24:MI')," & vbCrLf _
                & " PRE_EMP_CODE='" & MainClass.AllowSingleQuote((txtPrepared.Text)) & "', " & vbCrLf _
                & " DESP_STATUS='" & mDespStatus & "',DESP_TYPE='" & mDespType & "', " & vbCrLf _
                & " GRNO= '" & MainClass.AllowSingleQuote((TxtGRNo.Text)) & "', " & vbCrLf _
                & " GRDATE= TO_DATE('" & VB6.Format(TxtGRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), DIV_CODE=" & mDivisionCode & "," & vbCrLf _
                & " AUTO_KEY_SO=" & Val(txtSONo.Text) & ",SO_DATE=TO_DATE('" & VB6.Format(txtSODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " VENDOR_PO='" & txtCustPoNo.Text & "',VENDOR_PO_DATE=TO_DATE('" & VB6.Format(txtCustPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') ," & vbCrLf _
                & " LOC_CODE='" & MainClass.AllowSingleQuote(txtStoreLoc.Text) & "'" & vbCrLf _
                & " WHERE AUTO_KEY_DESP ='" & MainClass.AllowSingleQuote((LblMkey.Text)) & "'"
        End If




        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(Val(CStr(mVNoSeq)), mDivisionCode, mDespType) = False Then GoTo ErrPart

        If VB.Left(cboRefType.Text, 1) = "E" Then
            If UpdatePacking(Val(txtSONo.Text), True) = False Then GoTo ErrPart
        End If

        If CDbl(lblDespType.Text) = 2 And RsCompany.Fields("FYEAR").Value >= 2018 And RsCompany.Fields("REJECTION_DOCTYPE").Value = "D" Then

            SqlStr = "UPDATE FIN_DNCN_HDR SET UPDATE_FROM='N'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " ISDESPATCHED='Y',SALEINVOICENO='" & MainClass.AllowSingleQuote(txtDNNo.Text) & "'," & vbCrLf & " SALEINVOICEDATE=TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND BOOKCODE=" & ConDebitNoteBookCode & "" & vbCrLf & " AND MKEY ='" & txtSONo.Text & "'"

            PubDBCn.Execute(SqlStr)

            mPartyGSTNo = ""
            If MainClass.ValidateWithMasterTable(Trim(TxtCustomerName.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyGSTNo = MasterNo
            End If

            mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)

            xCGSTAmount = 0
            xSGSTAmount = 0
            xIGSTAmount = 0
            xCancelled = IIf(cboStatus.SelectedIndex = 2, "Y", "N")

            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    .Col = ColPackQty
                    mQty = Val(.Text)

                    SqlStr = " SELECT IH.VNO, IH.VDATE, IH.ISGSTREFUND, ID.ITEM_RATE, ID.SUPP_REF_NO, ID.SUPP_REF_DATE, " & vbCrLf & " ID.CGST_PER, ID.SGST_PER, ID.IGST_PER  " & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.MKEY = '" & txtSONo.Text & "' " & vbCrLf & " AND IH.BOOKCODE=" & ConDebitNoteBookCode & " " & vbCrLf & " AND ID.ITEM_CODE='" & mItemCode & "' "

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDN, ADODB.LockTypeEnum.adLockReadOnly)


                    If RsDN.EOF = False Then
                        xBillNo = IIf(xBillNo = "", IIf(IsDBNull(RsDN.Fields("SUPP_REF_NO").Value), "", RsDN.Fields("SUPP_REF_NO").Value), xBillNo)
                        xBillDate = IIf(xBillDate = "", IIf(IsDBNull(RsDN.Fields("SUPP_REF_DATE").Value), "", RsDN.Fields("SUPP_REF_DATE").Value), xBillDate)
                        xIsGST = IIf(xIsGST = "", IIf(IsDBNull(RsDN.Fields("ISGSTREFUND").Value), "Y", RsDN.Fields("ISGSTREFUND").Value), xIsGST)
                        mRate = IIf(IsDBNull(RsDN.Fields("ITEM_RATE").Value), 0, RsDN.Fields("ITEM_RATE").Value)
                        xCGSTPer = IIf(IsDBNull(RsDN.Fields("CGST_PER").Value), 0, RsDN.Fields("CGST_PER").Value)
                        xSGSTPer = IIf(IsDBNull(RsDN.Fields("SGST_PER").Value), 0, RsDN.Fields("SGST_PER").Value)
                        xIGSTPer = IIf(IsDBNull(RsDN.Fields("IGST_PER").Value), 0, RsDN.Fields("IGST_PER").Value)

                        mAmt = CDbl(VB6.Format(mQty * mRate, "0.00"))

                        xCGSTAmount = xCGSTAmount + CDbl(VB6.Format(mAmt * xCGSTPer * 0.01, "0.00"))
                        xSGSTAmount = xSGSTAmount + CDbl(VB6.Format(mAmt * xSGSTPer * 0.01, "0.00"))
                        xIGSTAmount = xIGSTAmount + CDbl(VB6.Format(mAmt * xIGSTPer * 0.01, "0.00"))
                    End If

                Next
            End With

            If CDate(txtCustPODate.Text) >= CDate("01/04/2018") Then
                If PurRejPostTRNGST(PubDBCn, (LblMkey.Text), 1, CStr(ConSalesBookCode), "S", "W", "S", (txtCustPoNo.Text), (txtCustPODate.Text), xBillNo, xBillDate, "-1", "-1", 0, IIf(xCancelled = "Y", True, False), (txtDNDate.Text), "", "", 0, ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivisionCode, IIf(xIsGST = "G", IIf(Trim(mCompanyGSTNo) = Trim(mPartyGSTNo), "N", "Y"), IIf(xIsGST = "I", "I", "N")), Val(CStr(xCGSTAmount)), Val(CStr(xSGSTAmount)), Val(CStr(xIGSTAmount)), Trim(txtBillTo.Text)) = False Then GoTo ErrPart
            End If
        End If

        If PubUserID <> "G0416" Then
            If MODIFYMode = True Then
                If MainClass.ValidateWithMasterTable(txtDNNo.Text, "AUTO_KEY_DESP", "BILLNO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND CANCELLED='N'") = True Then
                    If PubSuperUser = "S" Then
                        If MsgQuestion("Invoice (" & MasterNo & ") had Made Against This Despatch Note. Are You want to Continue...") = CStr(MsgBoxResult.No) Then
                            GoTo ErrPart
                        End If
                    Else
                        MsgBox("Invoice (" & MasterNo & ") had Made Against This Despatch Note. So Cann't be Changed", MsgBoxStyle.Information)
                        GoTo ErrPart
                    End If
                End If
            End If
        End If

        UpdateMain1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        '    Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsDNMain.Requery() ''.Refresh
        RsDNDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        'If Err.Number = -2147217900 Then
        '    ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        'Else
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'End If
        ''Resume
    End Function

    Private Function CheckValidVDate(ByRef pDNNoSeq As Double, ByRef mDivisionCode As Double) As Object

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mRsCheck1 As ADODB.Recordset = Nothing
        Dim mRsCheck2 As ADODB.Recordset = Nothing
        Dim mBackBillDate As String = ""
        Dim mMaxInvStrfNo As Integer
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckValidVDate = True

        If RsCompany.Fields("STOCKBALCHECK").Value = "N" Or PubSuperUser = "S" Then
            Exit Function
        End If

        If CDate(txtDNDate.Text) <= CDate("30/06/2022") Then
            Exit Function
        End If


        If txtDNNo.Text = 1 & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") Then Exit Function

        '    SqlStr = "SELECT SEPARATE_MRR_SERIES, MRR_SERIES " & vbCrLf _
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



        mSeparateSeries = IIf(IsDBNull(RsCompany.Fields("SEPARATE_DSP_SERIES").Value), "N", RsCompany.Fields("SEPARATE_DSP_SERIES").Value)

        SqlStr = "SELECT MAX(DESP_DATE)" & vbCrLf & " FROM DSP_DESPATCH_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_DESP<" & Val(CStr(pDNNoSeq)) & ""

        '    If mSeparateSeries = "Y" Then
        '        SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        '    End If

        If lblDespType.Text = "2" Then
            SqlStr = SqlStr & vbCrLf & " AND DESPATCHTYPE=2"
        Else
            If mSeparateSeries = "Y" Then
                SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
            End If
            SqlStr = SqlStr & vbCrLf & " AND DESPATCHTYPE<>2"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDBNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(DESP_DATE)" & " FROM DSP_DESPATCH_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_DESP>" & Val(CStr(pDNNoSeq)) & ""

        '    If mSeparateSeries = "Y" Then
        '        SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        '    End If

        If lblDespType.Text = "2" Then
            SqlStr = SqlStr & vbCrLf & " AND DESPATCHTYPE=2"
        Else
            If mSeparateSeries = "Y" Then
                SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
            End If
            SqlStr = SqlStr & vbCrLf & " AND DESPATCHTYPE<>2"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) And mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtDNDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Despatch Note Date Is Greater Than The Despatch Note Date Of Next Despatch Note No.")
                CheckValidVDate = False
            ElseIf CDate(txtDNDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Despatch Note Date Is Less Than The Despatch Note Date Of Previous Despatch Note No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) Then
            If CDate(txtDNDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Despatch Note Date Is Greater Than The Despatch Note Date Of Next Despatch Note No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtDNDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Despatch Note Date Is Less Than The Despatch Note Date Of Previous Despatch Note No.")
                CheckValidVDate = False
            End If
        End If

        Exit Function
CheckERR:
        CheckValidVDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqNo(ByRef mDivisionCode As Double) As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsDNMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mStartingSNo As Double
        Dim mSeparateSeries As String = "N"
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMaxValue As String

        SqlStr = ""
        mStartingSNo = 1

        If lblDespType.Text = "2" Then
            mStartingSNo = 90001
        Else
            mSeparateSeries = IIf(IsDBNull(RsCompany.Fields("SEPARATE_DSP_SERIES").Value), "N", RsCompany.Fields("SEPARATE_DSP_SERIES").Value)

            SqlStr = "SELECT DSP_SERIES " & vbCrLf & " FROM INV_DIVISION_MST " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


            If RsTemp.EOF = False Then
                '        mSeparateSeries = IIf(IsNull(RsTemp!SEPARATE_DSP_SERIES), "N", RsTemp!SEPARATE_DSP_SERIES)
                If mSeparateSeries = "Y" Then
                    mStartingSNo = IIf(IsDBNull(RsTemp.Fields("DSP_SERIES").Value), 1, RsTemp.Fields("DSP_SERIES").Value)
                    mStartingSNo = IIf(mStartingSNo = 0, 1, mStartingSNo)
                End If
            End If
        End If

        SqlStr = "SELECT Max(AUTO_KEY_DESP)  " & vbCrLf & " FROM DSP_DESPATCH_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""



        If lblDespType.Text = "2" Then
            SqlStr = SqlStr & vbCrLf & " AND DESPATCHTYPE=2"
        Else
            If mSeparateSeries = "Y" Then
                SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
            End If
            SqlStr = SqlStr & vbCrLf & " AND DESPATCHTYPE<>2"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsDNMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = Mid(mMaxValue, 1, Len(mMaxValue) - 6)
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

    Private Function UpdateDetail1(ByRef pNewMey As Double, ByRef mDivisionCode As Double, ByRef mDespType As String) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mSubRowNo As Integer
        Dim mItemCode As String
        Dim mUnit As String
        Dim mStockType As String = ""
        Dim mPackQty As Double
        Dim mPktQty As Double
        Dim mPDIRNo As String = ""
        Dim mRefNo As String
        Dim mMRRNo As Double
        Dim mMRRDate As String = ""
        Dim pPartyF4Date As String = ""
        Dim pOurVDate As String = ""
        Dim mHeadType As String = ""
        Dim mSqlStr As String
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim mStdQty As Double
        Dim mRMChildCode As String
        Dim mRMUOM As String
        Dim pErrorDesc As String = ""
        Dim mStockRowNo As Integer
        Dim cntRow As Integer
        Dim mScrapQty As Double

        Dim xSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSoNo As Double
        Dim mSODate As String
        Dim mCustomerNo As String
        Dim mCustomerDate As String
        Dim mLotNo As String
        Dim mHeatNo As String
        Dim mBatchNo As String
        Dim mJITCallNo As String

        Dim mSupplierCode As String = ""
        Dim mOrgBillNO As Double
        Dim mOrdBillDate As String = ""
        Dim mCRItemRate As Double
        Dim mRefDate As String
        Dim mShippedCode As String
        Dim mStockStatus As String
        Dim mODNo As String

        Dim mColInnerBoxQty As Double
        Dim mColInnerBoxCode As String
        Dim mColOuterBoxQty As Double
        Dim mColOuterBoxCode As String
        Dim mPackType As String
        Dim mStoreLoc As String
        Dim mActualHeight As Double
        Dim mActualWidth As Double
        Dim mChargeableHeight As Double
        Dim mChargeableWidth As Double
        Dim mArea As Double
        Dim mModel As String
        Dim mDrawingNo As String
        Dim mGlassDescription As String

        PubDBCn.Execute("Delete From DSP_DESPATCH_DET Where AUTO_KEY_DESP='" & LblMkey.Text & "'")

        If DeleteCRTRN(PubDBCn, ConStockRefType_DSP, (LblMkey.Text)) = False Then GoTo UpdateDetail1Err

        If DeleteStockTRN(PubDBCn, ConStockRefType_DSP, (LblMkey.Text)) = False Then GoTo UpdateDetail1Err


        PubDBCn.Execute("DELETE FROM DSP_PAINT57F4_TRN WHERE MKey='" & LblMkey.Text & "' AND BookType='D' AND BookSubType='O' AND TRNTYPE='D'")

        If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "HEADTYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mHeadType = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtShipCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mShippedCode = Trim(MasterNo)
        Else
            mShippedCode = "-1"
        End If


        '    If lblMkey.text = "29111202001" Or lblMkey.text = "29112202001" Or lblMkey.text = "29113202001" Or lblMkey.text = "29114202001" Or lblMkey.text = "12193202003" Or lblMkey.text = "12194202003" Or lblMkey.text = "12195202003" Then  '29113202001
        '        mStockStatus = "C"
        '    Else
        '        mStockStatus = ""
        '    End If

        mSubRowNo = 0
        cntRow = 1
        mStockRowNo = 1

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I


                .Col = ColSONo
                mSoNo = IIf(VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L", Val(txtSONo.Text), Val(.Text))

                .Col = ColSODate
                mSODate = IIf(VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L", txtSODate.Text, .Text)

                .Col = ColCustomerNo
                mCustomerNo = IIf(VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L", txtCustPoNo.Text, .Text)

                .Col = ColCustomerDate
                mCustomerDate = IIf(VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L", txtCustPODate.Text, .Text)

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColStoreLoc
                mStoreLoc = MainClass.AllowSingleQuote(.Text)

                .Col = ColHeatNo
                mHeatNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColBatchNo
                mBatchNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColODNo
                mODNo = MainClass.AllowSingleQuote(.Text)

                '.Col = ColLotNo
                mLotNo = "" ''Trim(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)

                .Col = ColMRRNo
                mMRRNo = Val(.Text)

                .Col = ColRefNo
                If CDbl(lblDespType.Text) = 2 Then
                    mRefNo = Trim(.Text)
                Else
                    mRefNo = Trim(.Text)
                End If

                .Col = ColRefDate
                mRefDate = Trim(.Text)

                .Col = ColPackQty
                mPackQty = Val(.Text)

                .Col = ColPktQty
                mPktQty = Val(.Text) ''20-10-2010

                .Col = ColJITCallNo
                mJITCallNo = Trim(.Text)



                .Col = ColInnerBoxQty
                mColInnerBoxQty = Val(.Text)

                .Col = ColInnerBoxCode
                mColInnerBoxCode = Trim(.Text)

                .Col = ColOuterBoxQty
                mColOuterBoxQty = Val(.Text)

                .Col = ColOuterBoxCode
                mColOuterBoxCode = Trim(.Text)

                .Col = ColPackType
                mPackType = Trim(.Text)

                .Col = ColActualHeight
                mActualHeight = Val(.Text)

                .Col = ColActualWidth
                mActualWidth = Val(.Text)

                .Col = ColChargeableHeight
                mChargeableHeight = Val(.Text)

                .Col = ColChargeableWidth
                mChargeableWidth = Val(.Text)

                .Col = ColChargeableArea
                mArea = Val(.Text)

                .Col = ColGlassDescription
                mGlassDescription = MainClass.AllowSingleQuote(.Text)

                .Col = ColModel
                mModel = Trim(.Text)

                .Col = ColDrawingNo
                mDrawingNo = Trim(.Text)

                SqlStr = ""
                '            mRefNo = 907
                If mItemCode <> "" And mPackQty > 0 Then
                    mSubRowNo = mSubRowNo + 1
                    SqlStr = " INSERT INTO DSP_DESPATCH_DET (AUTO_KEY_DESP, SERIAL_NO, ITEM_CODE,ITEM_UOM, STOCK_TYPE, " & vbCrLf _
                            & " PACKED_QTY,NO_OF_PACKETS, PDIR_NO, REF_NO, REF_DATE, MRR_REF_NO, COMPANY_CODE, " & vbCrLf _
                            & " SONO, SODATE,CUST_PO, CUST_PO_DATE, LOT_NO,JITCALLNO,HEAT_NO,BATCH_NO, OD_NO," & vbCrLf _
                            & " INNER_PACK_QTY, INNER_PACK_ITEM_CODE, OUTER_PACK_QTY, OUTER_PACK_ITEM_CODE,PACK_TYPE,LOC_CODE," & vbCrLf _
                            & " ACTUAL_HEIGHT, ACTUAL_WIDTH, CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH, GLASS_AREA,ITEM_MODEL, ITEM_DRAWINGNO, GLASS_DESC" & vbCrLf _
                            & " ) " & vbCrLf _
                            & " VALUES ('" & pNewMey & "'," & mSubRowNo & ",'" & mItemCode & "', '" & mUnit & "'," & vbCrLf _
                            & " '" & mStockType & "'," & mPackQty & ", " & mPktQty & ", '" & mPDIRNo & "'," & vbCrLf _
                            & " '" & mRefNo & "', TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " " & mMRRNo & ", " & RsCompany.Fields("COMPANY_CODE").Value & "," & mSoNo & ", " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(mSODate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mCustomerNo) & "'," & vbCrLf _
                            & " TO_DATE('" & VB6.Format(mCustomerDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & mLotNo & "','" & mJITCallNo & "','" & mHeatNo & "','" & mBatchNo & "','" & mODNo & "'," & vbCrLf _
                            & " " & mColInnerBoxQty & ",'" & mColInnerBoxCode & "'," & mColOuterBoxQty & ",'" & mColOuterBoxCode & "','" & mPackType & "','" & mStoreLoc & "'," & vbCrLf _
                            & " " & mActualHeight & ", " & mActualWidth & ", " & mChargeableHeight & ", " & mChargeableWidth & "," & mArea & ",'" & MainClass.AllowSingleQuote(mModel) & "','" & MainClass.AllowSingleQuote(mDrawingNo) & "', '" & MainClass.AllowSingleQuote(mGlassDescription) & "'" & vbCrLf _
                            & " ) "

                    PubDBCn.Execute(SqlStr)

                    If cboStatus.SelectedIndex <> 2 Then
                        If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Then

                            If mHeadType = "J" Then
                                mSqlStr = MakeBOMStockQty(mItemCode)
                                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOM, ADODB.LockTypeEnum.adLockReadOnly)
                                If RsBOM.EOF = False Then
                                    Do While Not RsBOM.EOF

                                        mRMChildCode = Trim(IIf(IsDBNull(RsBOM.Fields("RM_CODE").Value), "", RsBOM.Fields("RM_CODE").Value))
                                        mStdQty = Val(IIf(IsDBNull(RsBOM.Fields("STD_QTY").Value), 0, RsBOM.Fields("STD_QTY").Value))
                                        mScrapQty = Val(IIf(IsDBNull(RsBOM.Fields("GROSS_WT_SCRAP").Value), 0, RsBOM.Fields("GROSS_WT_SCRAP").Value))
                                        mRMUOM = Trim(IIf(IsDBNull(RsBOM.Fields("ISSUE_UOM").Value), "", RsBOM.Fields("ISSUE_UOM").Value))

                                        '                                    If mRMChildCode = "EXP1875" Then MsgBox "OK"
                                        If UpdateFinishedGoodsStock(pErrorDesc, mRMChildCode, mStdQty, mRMUOM, mItemCode, mPackQty, mStockRowNo, cntRow, mStockType, "N", mDivisionCode, mDespType) = False Then GoTo UpdateDetail1Err

                                        If mScrapQty <> 0 Then
                                            If UpdateFinishedGoodsStock(pErrorDesc, mRMChildCode, mScrapQty, mRMUOM, mItemCode, mPackQty, mStockRowNo, cntRow, mStockType, "Y", mDivisionCode, mDespType) = False Then GoTo UpdateDetail1Err
                                        End If
                                        RsBOM.MoveNext()
                                    Loop
                                End If

                            Else

                                Call GetF4detailFromRGP(mRefNo, mShippedCode, pPartyF4Date, pOurVDate)


                                If UpdatePaintDetail(PubDBCn, (txtDNNo.Text), "D", "O", mShippedCode, mRefNo, pPartyF4Date, (txtDNNo.Text), txtDNDate.Text, mItemCode, mPackQty, "O", mSubRowNo, "D", pOurVDate, , , , IIf(VB.Left(cboRefType.Text, 1) = "R", "Y", "N")) = False Then GoTo UpdateDetail1Err
                            End If

                            '                        If UpdateStockTRN(PubDBCn, ConStockRefType_DSP, txtDNNo.Text, mStockRowNo, txtDNDate.Text, txtDNDate.Text, _
                            ''                                mStockType, mItemCode, mUnit, -1, mPackQty, 0, "I", 0, 0, "", "", "", "PAD", "", "N", " To : " & TxtCustomerName.Text, txtCustomerCode.Text, ConJW) = False Then GoTo UpdateDetail1Err
                            '
                            '                        mStockRowNo = mStockRowNo + 1
                        End If


                        If VB.Left(cboRefType.Text, 1) = "S" Then ''If Left(cboRefType.Text, 1) = "S" Then

                            xSqlStr = " SELECT PARTY_F4NO, PARTY_F4DATE,VDATE " & vbCrLf _
                                & " FROM DSP_PAINT57F4_HDR " & vbCrLf _
                                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                & " And AUTO_KEY_MRR=" & mMRRNo & ""

                            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                            If RsTemp.EOF = False Then
                                mRefNo = IIf(IsDBNull(RsTemp.Fields("PARTY_F4NO").Value), "", RsTemp.Fields("PARTY_F4NO").Value)
                                pPartyF4Date = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PARTY_F4DATE").Value), "", RsTemp.Fields("PARTY_F4DATE").Value), "DD/MM/YYYY")
                                pOurVDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDate").Value), "", RsTemp.Fields("VDate").Value), "DD/MM/YYYY")

                                If UpdatePaintDetail(PubDBCn, (txtDNNo.Text), "D", "O", mShippedCode, mRefNo, pPartyF4Date, (txtDNNo.Text), txtDNDate.Text, mItemCode, mPackQty, "O", mSubRowNo, "D", pOurVDate) = False Then GoTo UpdateDetail1Err
                            End If
                        End If

                        If VB.Left(cboRefType.Text, 1) <> "U" Then

                            If UpdateStockTRN(PubDBCn, ConStockRefType_DSP, (txtDNNo.Text), mStockRowNo, (txtDNDate.Text), (txtDNDate.Text), mStockType, mItemCode, mUnit, mLotNo, mPackQty, 0, "O", 0, 0, "", "", "", "PAD", "", "N", " To : " & TxtCustomerName.Text, (txtCustomerCode.Text), ConWH, mDivisionCode, mDespType, "", mStockStatus) = False Then GoTo UpdateDetail1Err

                            mStockRowNo = mStockRowNo + 1

                            If Val(CStr(mPackQty)) > 0 And VB.Left(cboRefType.Text, 1) = "S" And mStockType = "CR" Then

                                If GetCRData(mMRRNo, Trim(mItemCode), mSupplierCode, mOrgBillNO, mOrdBillDate, mCRItemRate, mMRRDate) = False Then GoTo UpdateDetail1Err

                                If UpdateCRTRN(PubDBCn, Val(txtDNNo.Text), (txtDNDate.Text), ConStockRefType_DSP, mSupplierCode, Str(mMRRNo), mMRRDate, CStr(Val(CStr(mOrgBillNO))), mOrdBillDate, Trim(mItemCode), mPackQty, mUnit, mCRItemRate, "CR", "O", (txtDNDate.Text), Val(CStr(mDivisionCode)), "PAD") = False Then GoTo UpdateDetail1Err

                            ElseIf Val(CStr(mPackQty)) > 0 And VB.Left(cboRefType.Text, 1) = "S" And RsCompany.Fields("IS_WAREHOUSE").Value = "Y" Then

                                If GetCRData(mMRRNo, Trim(mItemCode), mSupplierCode, mOrgBillNO, mOrdBillDate, mCRItemRate, mMRRDate) = False Then GoTo UpdateDetail1Err

                                If UpdateCRTRN(PubDBCn, Val(txtDNNo.Text), (txtDNDate.Text), ConStockRefType_DSP, mSupplierCode, Str(mMRRNo), mMRRDate, CStr(Val(CStr(mOrgBillNO))), mOrdBillDate, Trim(mItemCode), mPackQty, mUnit, mCRItemRate, "ST", "O", (txtDNDate.Text), Val(CStr(mDivisionCode)), "PAD") = False Then GoTo UpdateDetail1Err

                            End If

                        End If
                    End If
NextRow:
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function

    Private Function UpdateFinishedGoodsStock(ByRef pErrorDesc As String, ByRef mRMCode As String, ByRef mStdQty As Double, ByRef mRMUOM As String, ByRef mFICode As String, ByRef mFQty As Double, ByRef mStockRowNo As Integer, ByRef cntRow As Integer, ByRef pStockType As String, ByRef IsScrap As String, ByRef mDivisionCode As Double, ByRef mDespType As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim ReqdQty As Double
        Dim mRMQty As Double
        Dim pSupplierCode As String = ""
        'Dim mProductionQty As Double

        Dim mRGPNo As Double
        Dim mRGPDate As String
        Dim pF4No As String
        Dim pF4Date As String
        Dim mBalQty As Double
        Dim mF4Qty As Double
        Dim mExpDate As String
        Dim pVDate As String = ""
        pSupplierCode = Trim(txtCustomerCode.Text)

        '    If mRMCode = "EXP1446" Then MsgBox "ok"

        mRMQty = mStdQty * mFQty
        If mRMUOM = "TON" Then
            mRMQty = mRMQty / 1000
            mRMQty = mRMQty / 1000
        ElseIf mRMUOM = "KGS" Then
            mRMQty = mRMQty / 1000
        End If


        SqlStr = " SELECT SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY) AS ITEM_QTY,ITEM_CODE,PARTY_F4NO,PARTY_F4DATE " & vbCrLf & " FROM DSP_PAINT57F4_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND VDATE<=TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mRMCode) & "'" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'"

        '    If Val(txtDNNo.Text) <> 0 Then
        '        SqlStr = SqlStr & vbCrLf & " AND BILL_NO<>'" & txtDNNo.Text & "'"
        '    End If

        SqlStr = SqlStr & vbCrLf & "AND (PARTY_F4NO IS NOT NULL OR PARTY_F4NO<>0)"

        SqlStr = SqlStr & vbCrLf & "HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)>0"

        SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " PARTY_F4NO,PARTY_F4DATE,ITEM_CODE " '& vbCrLf |                    & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)>0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            ReqdQty = mRMQty
            Do While RsTemp.EOF = False
                '            mRGPNo = IIf(IsNull(RsTemp!RGP_NO), "0", RsTemp!RGP_NO)
                '            mRGPDate = VB6.Format(IIf(IsNull(RsTemp!RGP_DATE), "", RsTemp!RGP_DATE), "DD/MM/YYYY")
                pF4No = IIf(IsDBNull(RsTemp.Fields("PARTY_F4NO").Value), "0", RsTemp.Fields("PARTY_F4NO").Value)
                pF4Date = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PARTY_F4DATE").Value), "", RsTemp.Fields("PARTY_F4DATE").Value), "DD/MM/YYYY")
                mF4Qty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                '            mExpDate = VB6.Format(IIf(IsNull(RsTemp!EXP_RTN_DATE), "", RsTemp!EXP_RTN_DATE), "DD/MM/YYYY")

                If ReqdQty < mF4Qty Then
                    mBalQty = ReqdQty
                    ReqdQty = 0
                Else
                    mBalQty = mF4Qty
                    ReqdQty = ReqdQty - mF4Qty
                End If
                '            mFGQty = mBalQty / mSTDQty

                Call GetF4detailFromRGP(pF4No, pSupplierCode, pF4Date, pVDate)

                If mBalQty > 0 Then
                    If pF4No <> "" Then
                        SqlStr = "INSERT INTO DSP_PAINT57F4_TRN ( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " BOOKTYPE, BOOKSUBTYPE, PARTY_F4NO, " & vbCrLf & " PARTY_F4DATE, SUPP_CUST_CODE, BILL_NO, " & vbCrLf & " BILL_DATE, ITEM_CODE,  " & vbCrLf & " ITEM_QTY, ITEM_IO, SUB_ITEM_CODE, " & vbCrLf & " SUBROWNO,BILL_QTY,TRNTYPE, VDATE,ISSCRAP) VALUES ( " & vbCrLf & " '" & LblMkey.Text & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf & " 'D', 'O', '" & pF4No & "', " & vbCrLf & " TO_DATE('" & VB6.Format(pF4Date, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & pSupplierCode & "', '" & MainClass.AllowSingleQuote(txtDNNo.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mRMCode) & "', " & vbCrLf & " " & mBalQty & ", 'O', '" & MainClass.AllowSingleQuote(mFICode) & "'," & vbCrLf & " " & cntRow & "," & mFQty & ",'D',TO_DATE('" & VB6.Format(pVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & IsScrap & "')" & vbCrLf
                        PubDBCn.Execute(SqlStr)


                        If UpdateStockTRN(PubDBCn, ConStockRefType_DSP, (txtDNNo.Text), mStockRowNo, (txtDNDate.Text), (txtDNDate.Text), "ST", mRMCode, mRMUOM, CStr(-1), mBalQty, 0, "O", 0, 0, "", "", "", "PAD", "", "N", " To : " & TxtCustomerName.Text, (txtCustomerCode.Text), ConWH, mDivisionCode, mDespType, mFICode) = False Then GoTo ErrPart

                        mStockRowNo = mStockRowNo + 1

                    End If

                    cntRow = cntRow + 1
                End If

                If ReqdQty = 0 Then Exit Do
                RsTemp.MoveNext()
            Loop
        End If

        mStockRowNo = mStockRowNo + 1

        UpdateFinishedGoodsStock = True
        Exit Function
ErrPart:
        UpdateFinishedGoodsStock = False
        If Err.Description <> "" Then MsgBox(Err.Description)
        'Resume
    End Function
    Private Function MakeBOMStockQty(ByRef mSFICode As String) As String

        On Error GoTo BOMStockErr
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim I As Integer
        Dim mSrn As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pSqlStr As String = ""
        Dim mLevel As Integer




        SqlStr = " DELETE FROM TEMP_BOM_HDR " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" '' AND PRODUCT_CODE'" & mSFICode & "'"

        PubDBCn.Execute(SqlStr)


        SqlStr = "SELECT  IH.MKEY, IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, ID.STD_QTY, GROSS_WT_SCRAP, INVMST.ISSUE_UOM " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST " & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mSFICode)) & "' " & vbCrLf & " AND IH.WEF=( " & vbCrLf & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mSFICode)) & "' " & vbCrLf & " AND WEF<= TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        I = 0

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                I = I + 1

                mSrn = Str(I)

                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                mLevel = 1
                Call FillBOM(RsShow, mSrn, mLevel, mSFICode)

                RsShow.MoveNext()
            Loop
        End If

        SqlStr = " SELECT * FROM TEMP_BOM_HDR " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" '' AND PRODUCT_CODE'" & mSFICode & "'"

        MakeBOMStockQty = SqlStr
        Exit Function
BOMStockErr:
        If Err.Description <> "" Then MsgBox(Err.Description)
        'Resume
    End Function
    Private Sub FillBOM(ByRef pRs As ADODB.Recordset, ByRef pSRNo As String, ByRef pLevel As Integer, ByRef pFGCode As String)

        On Error GoTo FillGERR
        Dim mRMCode As String
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim NewRow As Boolean
        Dim pCurrentRow As Integer
        Dim cntRow As Integer
        Dim mSearchItemCode As String
        Dim mUOM As String = ""
        Dim mStdQty As Double
        Dim mScrapQty As Double

        NewRow = False


        mRMCode = Trim(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))

        SqlStr = " SELECT IH.PRODUCT_CODE" & vbCrLf & " FROM PRD_NEWBOM_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mRMCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mRMCode) & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsBOM.EOF = True Then

            mUOM = IIf(IsDBNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
            mStdQty = Val(IIf(IsDBNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value))
            mScrapQty = Val(IIf(IsDBNull(pRs.Fields("GROSS_WT_SCRAP").Value), "", pRs.Fields("GROSS_WT_SCRAP").Value))

            SqlStr = " INSERT INTO TEMP_BOM_HDR ( " & vbCrLf & " USERID, PRODUCT_CODE, RM_CODE, " & vbCrLf & " ISSUE_UOM, STD_QTY,GROSS_WT_SCRAP ) VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', '" & MainClass.AllowSingleQuote(pFGCode) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mRMCode) & "', '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf & " " & mStdQty & ", " & mScrapQty & ")"

            PubDBCn.Execute(SqlStr)

            NewRow = True
        End If

NextRecd:
        Call FillSubRecord(mRMCode, pSRNo, pLevel, NewRow, pFGCode)

        Exit Sub
FillGERR:
        '    Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub FillSubRecord(ByRef pProductCode As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef NewRow As Boolean, ByRef pFGCode As String)

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String
        Dim j As Integer

        mSrn = pSrn
        pLevel = pLevel + 1
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, ID.STD_QTY, GROSS_WT_SCRAP, INVMST.ISSUE_UOM "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                '            If NewRow = True Then
                '
                '            End If

                j = j + 1
                xSrn = mSrn & "." & j
                pSrn = pSrn & "." & j

                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                Call FillBOM(RsShow, xSrn, pLevel, pFGCode)
                RsShow.MoveNext()
                NewRow = True
            Loop
        End If
        Exit Sub
FillERR:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Function UpdatePacking(ByRef pPackingNo As Double, ByRef mIsUpdateMode As Boolean) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim mDCMade As String

        mDCMade = IIf(mIsUpdateMode = True, "Y", "N")

        SqlStr = " UPDATE FIN_EXPINV_HDR SET " & vbCrLf & " DC_MADE='" & mDCMade & "'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PACK=" & Val(CStr(pPackingNo)) & ""

        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE DSP_PACKING_HDR SET " & vbCrLf & " DC_MADE='" & mDCMade & "'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PACK=" & Val(CStr(pPackingNo)) & ""

        PubDBCn.Execute(SqlStr)

        UpdatePacking = True
        Exit Function
UpdateDetail1Err:
        UpdatePacking = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function

    Private Sub GetF4detailFromRGP(ByRef pPartyF4No As String, ByRef pPartyCode As String, ByRef pPartyF4Date As String, ByRef pOurVDate As String)

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        pPartyF4Date = ""
        pOurVDate = ""

        mSqlStr = " SELECT PARTY_F4NO,PARTY_F4DATE, VDATE " & vbCrLf & " FROM DSP_PAINT57F4_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pPartyCode & "'" & vbCrLf & " AND PARTY_F4NO='" & Trim(pPartyF4No) & "'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pPartyF4Date = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PARTY_F4DATE").Value), "", RsTemp.Fields("PARTY_F4DATE").Value), "DD/MM/YYYY")
            pOurVDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDate").Value), "", RsTemp.Fields("VDate").Value), "DD/MM/YYYY")
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetSalesDSQty(ByRef pItemCode As String, ByRef mDIRequired As String, ByRef mODNo As String, ByRef mStoreLoc As String, ByRef mWidth As Double, ByRef mHeight As Double, ByRef mModelNo As String, ByRef mDrawingNo As String) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOrderType As String

        GetSalesDSQty = 0
        If MainClass.ValidateWithMasterTable(Val(txtSONo.Text), "AUTO_KEY_SO", "ORDER_TYPE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "' AND SO_APPROVED='Y'") = True Then
            mOrderType = MasterNo
        Else
            mOrderType = "O"
        End If

        ''(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106) And

        If mOrderType = "C" Then

            mSqlStr = " SELECT SUM(SO_QTY) AS ITEM_QTY " & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID " & vbCrLf _
                    & " WHERE IH.MKEY = ID.MKEY" & vbCrLf _
                    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "'" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf _
                    & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & " AND SO_STATUS='O' AND SO_APPROVED='Y' AND ID.SO_ITEM_STATUS = 'N'"


            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                If mWidth > 0 Then
                    mSqlStr = mSqlStr & vbCrLf & " AND CHARGEABLE_WIDTH='" & mWidth & "'"
                End If
                If mHeight > 0 Then
                    mSqlStr = mSqlStr & vbCrLf & " AND CHARGEABLE_HEIGHT='" & mHeight & "'"
                End If
                If mModelNo <> "" Then
                    mSqlStr = mSqlStr & vbCrLf & " AND ITEM_MODEL='" & mModelNo & "'"
                End If
                If mDrawingNo <> "" Then
                    mSqlStr = mSqlStr & vbCrLf & " AND ITEM_DRAWINGNO='" & mDrawingNo & "'"
                End If


            End If

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                GetSalesDSQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))
            End If

            Exit Function
        End If

        If mDIRequired = "N" Then

            mSqlStr = " SELECT SUM(ITEM_QTY) AS ITEM_QTY " & vbCrLf _
                    & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DELV_SCHLD_DET ID " & vbCrLf _
                    & " WHERE IH.AUTO_KEY_DELV = ID.AUTO_KEY_DELV" & vbCrLf _
                    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "'" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf _
                    & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & ""


            If mOrderType = "C" Then
                '        mSqlStr = mSqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')<='" & VB6.Format(txtDNDate, "YYYYMM") & "'"
            Else
                mSqlStr = mSqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')='" & VB6.Format(txtDNDate.Text, "YYYYMM") & "'"
            End If

        Else
            mSqlStr = " SELECT SUM(PLANNED_QTY) AS ITEM_QTY " & vbCrLf _
                    & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID " & vbCrLf _
                    & " WHERE IH.AUTO_KEY_DELV = ID.AUTO_KEY_DELV" & vbCrLf _
                    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "'" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf _
                    & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & ""

            If mODNo = "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND (OD_NO='' OR OD_NO IS NULL)"
            Else
                mSqlStr = mSqlStr & vbCrLf & " AND OD_NO='" & mODNo & "'"
            End If

            'If mOrderType = "C" Then
            '    '        mSqlStr = mSqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')<='" & VB6.Format(txtDNDate, "YYYYMM") & "'"
            'Else
            '    mSqlStr = mSqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')='" & VB6.Format(txtDNDate.Text, "YYYYMM") & "'"
            'End If
        End If

        If mStoreLoc = "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND (LOC_CODE='' OR LOC_CODE IS NULL)"
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND LOC_CODE='" & mStoreLoc & "'"
        End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSalesDSQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetTotMonthDespatchQty(ByRef pItemCode As String, ByRef mDIRequired As String, ByRef mODNo As String, ByRef mWidth As Double, ByRef mHeight As Double, ByRef mModelNo As String, ByRef mDrawingNo As String, ByRef mStoreLoc As String, Optional ByRef pOverAllSOQty As String = "", Optional ByRef mWEF As String = "") As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOrderType As String

        GetTotMonthDespatchQty = 0

        '& " AND IH.AUTO_KEY_SO=" & Val(txtSONo) & "" & vbCrLf _
        '
        If MainClass.ValidateWithMasterTable(Val(txtSONo.Text), "AUTO_KEY_SO", "ORDER_TYPE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "' AND SO_APPROVED='Y'") = True Then
            mOrderType = MasterNo
        Else
            mOrderType = "O"
        End If

        If MainClass.ValidateWithMasterTable(Val(txtSONo.Text), "AUTO_KEY_SO", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "' AND SO_APPROVED='Y'") = True Then
            mDIRequired = MasterNo
        Else
            mDIRequired = "N"
        End If

        mSqlStr = " SELECT SUM(PACKED_QTY) AS ITEM_QTY " & vbCrLf _
            & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID " & vbCrLf _
            & " WHERE IH.AUTO_KEY_DESP = ID.AUTO_KEY_DESP" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "'" & vbCrLf _
            & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'"

        ''
        mSqlStr = mSqlStr & vbCrLf & " AND IH.BILL_TO_LOC_ID='" & txtBillTo.Text & "'"

        mSqlStr = mSqlStr & " AND IH.DESP_TYPE IN ('G','P','S')  AND DESP_STATUS<>2 "   ''ID.STOCK_TYPE='FG'

        If mStoreLoc <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND NVL(ID.LOC_CODE,'')='" & mStoreLoc & "'"
        End If

        If mDIRequired = "Y" Then
            If mODNo = "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND (OD_NO='' OR OD_NO IS NULL)"
            Else
                mSqlStr = mSqlStr & vbCrLf & " AND OD_NO='" & mODNo & "'"
            End If
        End If

        If mHeight > 0 Then
            mSqlStr = mSqlStr & vbCrLf & " AND ACTUAL_HEIGHT=" & mHeight & ""
        End If

        If mWidth > 0 Then
            mSqlStr = mSqlStr & vbCrLf & " AND ACTUAL_WIDTH=" & mWidth & ""
        End If

        If mModelNo <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND ITEM_MODEL='" & mModelNo & "'"
        End If

        If mDrawingNo <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND ITEM_DRAWINGNO='" & mDrawingNo & "'"
        End If

        If mOrderType = "C" Or pOverAllSOQty = "Y" Then
            mSqlStr = mSqlStr & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & " "
            'If mWEF <> "" Then
            '    mSqlStr = mSqlStr & " AND IH.DESP_DATE >=TO_DATE('" & VB6.Format(mWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            'End If
        Else
            mSqlStr = mSqlStr & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & " "
            mSqlStr = mSqlStr & " AND TO_CHAR(IH.DESP_DATE,'YYYYMM')='" & VB6.Format(txtDNDate.Text, "YYYYMM") & "' "
        End If

        If Val(txtDNNo.Text) <> 0 Then
            mSqlStr = mSqlStr & " AND IH.AUTO_KEY_DESP<>" & Val(txtDNNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetTotMonthDespatchQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            mSqlStr = " SELECT SUM(ID.BILL_QTY) AS ITEM_QTY " & vbCrLf _
                    & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, FIN_INVOICE_HDR IIH, FIN_INVOICE_DET IID, DSP_DESPATCH_DET DD " & vbCrLf _
                    & " WHERE IH.AUTO_KEY_MRR = ID.AUTO_KEY_MRR" & vbCrLf _
                    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "'" & vbCrLf _
                    & " AND IH.REF_TYPE='I' " & vbCrLf _
                    & " AND IH.COMPANY_CODE=IIH.COMPANY_CODE " & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE=IIH.SUPP_CUST_CODE " & vbCrLf _
                    & " AND ID.REF_PO_NO=IIH.AUTO_KEY_INVOICE " & vbCrLf _
                    & " AND IIH.MKEY=IID.MKEY " & vbCrLf _
                    & " AND IIH.AUTO_KEY_DESP=DD.AUTO_KEY_DESP " & vbCrLf _
                    & " AND IID.ITEM_CODE=DD.ITEM_CODE " & vbCrLf _
                    & " AND IID.SUBROWNO=DD.SERIAL_NO " & vbCrLf _
                    & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'"

            If mStoreLoc <> "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND NVL(DD.LOC_CODE,'')='" & mStoreLoc & "'"
            End If

            mSqlStr = mSqlStr & " AND IIH.OUR_AUTO_KEY_SO=" & Val(txtSONo.Text) & " "

            mSqlStr = mSqlStr & " AND TO_CHAR(IH.MRR_DATE,'YYYYMM')='" & VB6.Format(txtDNDate.Text, "YYYYMM") & "' "
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                GetTotMonthDespatchQty = GetTotMonthDespatchQty - CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))
            End If

            mSqlStr = " SELECT SUM(IID.ITEM_SHORT_RECD_QTY) AS ITEM_QTY " & vbCrLf _
                   & " FROM FIN_INVOICE_HDR IIH, FIN_INVOICE_DET IID, DSP_DESPATCH_DET DD " & vbCrLf _
                   & " WHERE IIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND IIH.SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "'" & vbCrLf _
                   & " AND CANCELLED='N' " & vbCrLf _
                   & " AND IIH.MKEY=IID.MKEY " & vbCrLf _
                   & " AND IIH.AUTO_KEY_DESP=DD.AUTO_KEY_DESP " & vbCrLf _
                   & " AND IID.ITEM_CODE=DD.ITEM_CODE " & vbCrLf _
                   & " AND IID.SUBROWNO=DD.SERIAL_NO " & vbCrLf _
                   & " AND IID.ITEM_CODE='" & Trim(pItemCode) & "'"

            If mStoreLoc <> "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND NVL(DD.LOC_CODE,'')='" & mStoreLoc & "'"
            End If

            mSqlStr = mSqlStr & " AND IIH.OUR_AUTO_KEY_SO=" & Val(txtSONo.Text) & " "

            mSqlStr = mSqlStr & " AND TO_CHAR(IIH.GRNDATE,'YYYYMM')='" & VB6.Format(txtDNDate.Text, "YYYYMM") & "' "
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                GetTotMonthDespatchQty = GetTotMonthDespatchQty - CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))
            End If

        End If


        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function PreviousDayPendingDN() As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDNDate As String
        Dim mCurrentDate As String
        Dim mTotalHr As Double
        Dim mDNNo As String
        Dim mRejDocType As String
        Dim mApplicableDate As String

        'If RsCompany.Fields("STOCKBALCHECK").Value = "N" Then
        PreviousDayPendingDN = False
        Exit Function
        'End If

        mRejDocType = IIf(IsDBNull(RsCompany.Fields("REJECTION_DOCTYPE").Value), "D", RsCompany.Fields("REJECTION_DOCTYPE").Value)
        mApplicableDate = IIf(IsDBNull(RsCompany.Fields("REJ_APPLICABLEDATE").Value), "", RsCompany.Fields("REJ_APPLICABLEDATE").Value)

        PreviousDayPendingDN = True

        mSqlStr = " SELECT AUTO_KEY_DESP, TO_CHAR(DESP_DATE,'DD-MON-YYYY') || ' ' || TO_CHAR(LOADING_TIME,'HH24:MI') DESP_DATE, TO_CHAR(LOADING_TIME,'DD-MON-YYYY HH24:MI') LOADING_TIME" & vbCrLf & " FROM DSP_DESPATCH_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND DESP_STATUS=0 " & vbCrLf _
            & " AND DESP_DATE <TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If mRejDocType = "D" Or mApplicableDate = "" Then
            If RsCompany.Fields("LOADIND_APP").Value = "N" Then
                mSqlStr = mSqlStr & vbCrLf & " AND DESP_TYPE NOT IN ('Q','L')"
            End If
        End If

        mSqlStr = mSqlStr & vbCrLf & " AND DESP_TYPE NOT IN ('U','E')"

        mSqlStr = mSqlStr & vbCrLf & " ORDER BY DESP_DATE,LOADING_TIME"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            PreviousDayPendingDN = False
        Else
            mDNNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_DESP").Value), "", RsTemp.Fields("AUTO_KEY_DESP").Value)
            mDNDate = VB6.Format(RsTemp.Fields("DESP_DATE").Value, "DD-MM-YYYY HH:MM")
            mCurrentDate = VB6.Format(GetServerDate() & " " & GetServerTime(), "DD-MM-YYYY HH:MM")

            mTotalHr = DateDiff(Microsoft.VisualBasic.DateInterval.Hour, CDate(mDNDate), CDate(mCurrentDate)) ''CDate(mCurrentDate) - CDate(mDNDate)

            If mTotalHr >= 6 And mTotalHr < 24 Then
                MsgInformation("There is a pending Despatch Note (" & mDNNo & ") is more than 6 Hours, Please make it other wise Sale Invoice will be Stop after 24 Hours.")
                PreviousDayPendingDN = False
            ElseIf mTotalHr > 24 Then
                MsgInformation("There is a pending Despatch Note (" & mDNNo & ") is more than 24 Hours, so that Stop despatch note.")
                PreviousDayPendingDN = True
            End If
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function PreviousDayPendingLoading() As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDNDate As String
        Dim mCurrentDate As String
        Dim mTotalHr As Double
        Dim mBillNo As String
        Dim pAppDate As String
        Dim pTimelineFinished As Boolean
        PreviousDayPendingLoading = False

        If RsCompany.Fields("LOADIND_APP").Value = "N" Then
            PreviousDayPendingLoading = False
            Exit Function
        End If

        pAppDate = IIf(IsDBNull(RsCompany.Fields("LOADING_APP_DATE").Value), "", RsCompany.Fields("LOADING_APP_DATE").Value)

        If pAppDate = "" Then
            PreviousDayPendingLoading = False
            Exit Function
        End If

        mSqlStr = " SELECT BILLNo, TO_CHAR(INVOICE_DATE,'DD-MON-YYYY') || ' ' || TO_CHAR(INV_PREP_TIME,'HH24:MI') BILLDATE, FYEAR " & vbCrLf & " FROM FIN_INVOICE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CANCELLED='N' AND BOOKCODE=" & ConSalesBookCode & " AND REF_DESP_TYPE <> 'U' AND INVOICESEQTYPE NOT IN (4,7,8,9)" & vbCrLf & " AND AUTO_KEY_INVOICE NOT IN (" & vbCrLf & " SELECT DISTINCT REF_NO " & vbCrLf & " FROM DSP_LOADING_HDR IH, DSP_LOADING_DET ID" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf & " AND IH.AUTO_KEY_LOAD=ID.AUTO_KEY_LOAD" & vbCrLf & " AND IH.BOOKTYPE='L' AND ID.REF_TYPE='I'" & vbCrLf & " )" & vbCrLf & " AND INVOICE_DATE>=TO_DATE('" & VB6.Format(pAppDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND INVOICE_DATE<TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ORDER BY FYEAR, INVOICE_DATE, INV_PREP_TIME"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            PreviousDayPendingLoading = False
            Exit Function
        Else
            Do While RsTemp.EOF = False
                mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                mDNDate = VB6.Format(RsTemp.Fields("BILLDATE").Value, "DD-MM-YYYY HH:MM")
                mCurrentDate = VB6.Format(GetServerDate() & " " & GetServerTime(), "DD-MM-YYYY HH:MM")

                mTotalHr = DateDiff(Microsoft.VisualBasic.DateInterval.Hour, CDate(mDNDate), CDate(mCurrentDate)) ''CDate(mCurrentDate) - CDate(mDNDate)

                pTimelineFinished = False
                If CheckApproval(mBillNo, mDNDate, mCurrentDate, pTimelineFinished) = True Then

                Else
                    If pTimelineFinished = True Then
                        MsgInformation("There is a already Approved pending Loading Slip (" & mBillNo & "), which time line is finished.")
                        PreviousDayPendingLoading = True
                        Exit Function
                    Else
                        If mTotalHr >= 6 And mTotalHr < 24 Then
                            MsgInformation("There is a pending Loading Slip (" & mBillNo & ") is more than " & mTotalHr & " Hours, Please make Loading Slip other wise Sale Invoice will be Stop after 24 Hours.")
                            PreviousDayPendingLoading = False
                            Exit Function
                        ElseIf mTotalHr > 24 Then
                            MsgInformation("There is a pending Loading Slip (" & mBillNo & ") is more than " & mTotalHr & " Hours, Please make Loading Slip First or take Approval for pending.")
                            PreviousDayPendingLoading = True
                            Exit Function
                        End If
                    End If
                End If
                '            End If
                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function CheckApproval(ByRef mBillNo As String, ByRef mDNDate As String, ByRef mCurrentDate As String, ByRef pTimelineFinished As Boolean) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mClearDate As String
        Dim mTotalHr As Double

        CheckApproval = False

        mSqlStr = " SELECT TO_CHAR(CLEAR_DATE,'DD-MON-YYYY HH24:MI') CLEAR_DATE " & vbCrLf & " FROM FIN_LOADING_SLIP_UNLOCK" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BILL_NO='" & MainClass.AllowSingleQuote(mBillNo) & "'" & vbCrLf & " AND BILL_DATE=TO_DATE('" & VB6.Format(mDNDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            pTimelineFinished = False
            CheckApproval = False
            Exit Function
        Else
            mClearDate = VB6.Format(RsTemp.Fields("CLEAR_DATE").Value, "DD-MM-YYYY HH:MM")
            mCurrentDate = VB6.Format(GetServerDate() & " " & GetServerTime(), "DD-MM-YYYY HH:MM")

            mTotalHr = DateDiff(Microsoft.VisualBasic.DateInterval.Hour, CDate(mClearDate), CDate(mCurrentDate))

            If mTotalHr <= 0 Then
                If mTotalHr >= -6 And mTotalHr <= 0 Then
                    MsgInformation("There is a already approved pending Loading Slip (" & mBillNo & "), which time line is finished after " & mTotalHr & " Hours.")
                End If
                pTimelineFinished = False
                CheckApproval = True
                Exit Function
            Else
                '            MsgInformation "There is a already approved pending Loading Slip (" & mBillNo & "), which time line is finished."
                pTimelineFinished = True
                CheckApproval = False
                Exit Function
            End If
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim cntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim xPORate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xRateDiffCN As Double
        Dim mExciseDutyAmt As Double
        Dim mSalesTaxAmount As Double
        Dim mWithInState As String
        Dim mItemCode As String
        Dim mRefNo As String
        Dim mIsFGItem As Boolean
        Dim mScheduleQty As Double
        Dim mPackQty As Double
        Dim mTotMonthPackQty As Double

        Dim xInItemCode As String
        Dim xOutItemCode As String
        Dim mIsManyIn As Boolean
        Dim mInConUnit As Double
        Dim mOutConUnit As Double
        Dim mInvoiceMade As Boolean
        Dim mLotNoRequied As String

        Dim mBalanceMRRQty As Double
        Dim mMRRNo As Double
        Dim pPackQty As Double
        Dim mIsFixedAssets As String
        Dim mStockBal As Double
        Dim mDespQty As Double
        Dim mDivisionCode As Double
        Dim xIUOM As String
        Dim xStoreLoc As String
        Dim mStockType As String = ""
        Dim mLotNo As String
        Dim xActualWidth As Double
        Dim xActualHeight As Double
        Dim xModel As String
        Dim xDrawingNo As String
        Dim mCRStock As Double
        Dim mSOAmendNo As Double
        Dim mSOMKey As Double
        Dim mDIRequired As String = "N"
        Dim mODNo As String = ""
        Dim mDepatchedQty As Double
        Dim mSOValidQty As Double
        Dim mWEF As String = ""
        Dim xFGBatchNoReq As String
        Dim mSupplierType As String = ""
        Dim mPartyGSTNo As String
        Dim mSameGSTNo As String
        Dim mHeatNo As String
        Dim mWidth As Double
        Dim mHeight As Double
        Dim mModelNo As String
        Dim mDrawingNo As String

        Dim mInterUnit As String = "N"

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        FieldsVarification = True

        If CDate(VB6.Format(txtDNDate.Text, "DD/MM/YYYY")) >= CDate(PubGSTApplicableDate) Then
            If lblDespType.Text = "" Then
                MsgInformation("GST is Applicable, please create the Despatch note in New Format.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If ValidateBranchLocking((txtDNDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateBookLocking(PubDBCn, CInt(ConLockDespatch), txtDNDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, txtDNDate.Text, (TxtCustomerName.Text), mCustomerCode) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsDNMain.EOF = True Then Exit Function

        If MODIFYMode = True And txtDNNo.Text = "" Then
            MsgInformation("Voucher No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtDNDate.Text = "" Then
            MsgBox("VDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDNDate.Focus()
            Exit Function
        ElseIf FYChk((txtDNDate.Text)) = False Then
            FieldsVarification = False
            If txtDNDate.Enabled = True Then txtDNDate.Focus()
            Exit Function
        End If

        If txtLoadingTime.Text = "" Then
            MsgBox("Loading Time is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtLoadingTime.Focus()
            Exit Function
        ElseIf Not IsDate(txtLoadingTime.Text) Then
            MsgBox("Invalid Loading Time", MsgBoxStyle.Information)
            FieldsVarification = False
            txtLoadingTime.Focus()
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        Else
            mDivisionCode = -1
        End If

        If Trim(TxtCustomerName.Text) = "" Then
            MsgBox("Customer Cannot Be Blank", MsgBoxStyle.Information)
            ' TxtCustomerName.SetFocus
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgInformation("Bill To is blank. Cannot Save")
            If txtBillTo.Enabled = True Then txtBillTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'") = False Then
                MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                If txtBillTo.Enabled = True Then txtBillTo.Focus()
                FieldsVarification = False
            End If
        End If
        Dim mShipCustomerCode As String = ""

        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtShipCustomer.Text = Trim(TxtCustomerName.Text)
            mShipCustomerCode = txtCustomerCode.Text
            TxtShipTo.Text = txtBillTo.Text
        Else
            If Trim(txtShipCustomer.Text) = "" Then
                MsgBox("Shipped To Cannot Be Blank", MsgBoxStyle.Information)
                ' TxtCustomerName.SetFocus
                FieldsVarification = False
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable((txtShipCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
                MsgInformation("Shipped To is not a Supplier or Customer Category. Cannot Save")
                If txtShipCustomer.Enabled = True Then txtShipCustomer.Focus()
                FieldsVarification = False
                Exit Function
            Else
                mShipCustomerCode = MasterNo
            End If
        End If

        If Trim(TxtShipTo.Text) = "" Then
            MsgInformation("Ship To is blank. Cannot Save")
            If TxtShipTo.Enabled = True Then TxtShipTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(TxtShipTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipCustomerCode) & "'") = False Then
                MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                If TxtShipTo.Enabled = True Then TxtShipTo.Focus()
                FieldsVarification = False
            End If
        End If

        If VB.Left(cboRefType.Text, 1) = "U" Or VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "G" Or VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "S" Then
            If txtSONo.Text = "" Then
                MsgBox("Sales Order No. is Blank", MsgBoxStyle.Information)
                FieldsVarification = False
                txtSONo.Focus()
                Exit Function
            End If

            If Trim(txtCustPoNo.Text) = "" Then
                MsgBox("Customer Sale Order No. is Blank", MsgBoxStyle.Information)
                FieldsVarification = False
                txtSONo.Focus()
                Exit Function
            End If
        End If

        If VB.Left(cboRefType.Text, 1) = "E" Then
            If txtSONo.Text = "" Then
                MsgBox("Packing List is Blank", MsgBoxStyle.Information)
                FieldsVarification = False
                txtSONo.Focus()
                Exit Function
            End If
        End If

        If ADDMode = True Then
            txtDNNo.Text = ""
            If MainClass.ValidateWithMasterTable((txtCustomerCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='C'") = True Then
                MsgBox("Customer Master is Closed, So cann't be saved", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtCustomerCode.Enabled = True Then txtCustomerCode.Focus()
                Exit Function
            End If

            If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then

            Else
                If MainClass.ValidateWithMasterTable((txtCustomerCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOP_INVOICE='Y'") = True Then
                    MsgBox("Despatch Note Cann't Be Made for Such Customer, So cann't be saved", MsgBoxStyle.Information)
                    FieldsVarification = False
                    If txtCustomerCode.Enabled = True Then txtCustomerCode.Focus()
                    Exit Function
                End If
            End If

            If RsCompany.Fields("LOCK_INVOICE_PAYTERMS").Value = "Y" And mInterUnit = "N" And (VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "G") Then
                If CheckCreditDaysLocking(mCustomerCode, txtDNDate.Text, 0, "") = True Then
                    MsgBox("Credit Limit Days Already Exceeed.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If

        End If

        If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgInformation("Customer Name is not a Supplier or Customer Category. Cannot Save")
            If TxtCustomerName.Enabled = True Then TxtCustomerName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCustomerCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")
        mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")


        mInvoiceMade = False
        If ADDMode = True Then

            'If InStr(1, XRIGHT, "S") = 0 Then
            '    If CDate(txtDNDate.Text) < CDate(PubCurrDate) Then
            '        MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
            '        FieldsVarification = False
            '        Exit Function
            '    End If
            'End If

            If PreviousDayPendingDN() = True Then
                MsgBox("Please First Clear Previous Day Pending Despatch Note, So cann't be Save.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If

            If PreviousDayPendingLoading() = True Then
                '            MsgBox "Please First Clear Previous Day Pending Loading, So cann't be Save.", vbInformation
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MODIFYMode = True Then
            If MainClass.ValidateWithMasterTable((txtDNNo.Text), "AUTO_KEY_DESP", "BILLNO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND CANCELLED='N'") = True Then
                mInvoiceMade = True
                If PubSuperUser = "S" Then
                    If MsgQuestion("Invoice (" & MasterNo & ") had Made Against This Despatch Note. Are You want to Continue...") = CStr(MsgBoxResult.No) Then
                        FieldsVarification = False
                        Exit Function
                    End If
                Else
                    MsgBox("Invoice (" & MasterNo & ") had Made Against This Despatch Note. So Cann't be Changed", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            Else
                '            If lblDespType.text = 2 Then
                '                cboStatus.ListIndex = 1
                '            Else
                '                cboStatus.ListIndex = 0
                '            End If
            End If
        End If

        If CheckLotStockQty() = False Then
            FieldsVarification = False
            Exit Function
        End If

        If VB.Left(cboRefType.Text, 1) = "P" Then
            mSOAmendNo = GetSOMaxAmendNo(CDbl(VB6.Format(Val(txtSONo.Text))))
            mSOMKey = CDbl(Val(txtSONo.Text) & VB6.Format(mSOAmendNo, "000"))

            If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDIRequired = MasterNo
            End If

            With SprdMain
                For mRow = 1 To .MaxRows - 1
                    .Row = mRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                        xFGBatchNoReq = "Y"
                    Else
                        xFGBatchNoReq = "N"
                    End If

                    .Col = ColUnit
                    xIUOM = Trim(SprdMain.Text)

                    .Col = ColStoreLoc
                    xStoreLoc = Trim(SprdMain.Text)

                    .Col = ColPackQty
                    pPackQty = Val(SprdMain.Text)


                    .Col = ColChargeableWidth
                    mWidth = Val(.Text)

                    .Col = ColChargeableHeight
                    mHeight = Val(.Text)

                    .Col = ColDrawingNo
                    mDrawingNo = Trim(.Text)

                    .Col = ColModel
                    mModelNo = Trim(.Text)

                    If mDIRequired = "Y" Then
                        .Col = ColODNo
                        If Trim(SprdMain.Text) = "" Then
                            MsgInformation("Delivery Instruction (OD NO) is must for this Order")
                            MainClass.SetFocusToCell(SprdMain, mRow, ColODNo)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106) Then
                        If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "SO_QTY", "DSP_SALEORDER_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'") = True Then
                            mSOValidQty = Val(MasterNo)
                        End If
                    Else
                        If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "VALID_QTY", "DSP_SALEORDER_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'") = True Then
                            mSOValidQty = Val(MasterNo)
                        End If
                    End If

                    If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "VALID_QTY", "DSP_SALEORDER_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'") = True Then
                        mSOValidQty = Val(MasterNo)
                    End If


                    If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "AMEND_WEF", "DSP_SALEORDER_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'") = True Then
                        mWEF = MasterNo
                    End If

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                    Else
                        If mSOValidQty > 0 Then
                            mDepatchedQty = GetTotMonthDespatchQty(mItemCode, "N", "", mWidth, mHeight, mModelNo, mDrawingNo, xStoreLoc,, mWEF)
                            If mSOValidQty < pPackQty + mDepatchedQty Then
                                MsgInformation("PO is valid for Only " & mSOValidQty & ", you already despatch " & mDepatchedQty & " nos for Item Code :" & mItemCode)
                                MainClass.SetFocusToCell(SprdMain, mRow, ColPackQty)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If

                    If pPackQty > 0 Then
                        mCRStock = GetBalanceStockQty(mItemCode, (txtDNDate.Text), xIUOM, "STR", "CR", "", ConWH, CDbl(mDivisionCode)) ''GetCRStockQty(-1, Trim(mItemCode), "", Int(mDivisionCode), "CR", Val(txtDNNo.Text)) ''
                        If mCRStock >= pPackQty Then
                            If MsgQuestion("CR Stock Qty " & mCRStock & " is available for Item Code - " & mItemCode & ". Are You Want to Continue.") = CStr(MsgBoxResult.No) Then
                                'MsgBox("CR Stock Qty " & mCRStock & " is available for Item Code - " & mItemCode & ". Please Clear CR Stock First.", MsgBoxStyle.Information)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If
                Next
            End With
        End If

        Dim mBalanceDNQty As Double
        Dim mFGItem As Boolean

        With SprdMain
            For mRow = 1 To .MaxRows - 1
                .Row = mRow

                .Col = ColPackQty
                If Val(.Text) <= 0 Then GoTo NextRow

                .Col = ColODNo
                mODNo = Trim(.Text)

                .Col = ColItemCode
                If Trim(.Text) <> "" Then
                    mItemCode = Trim(.Text)

                    If ADDMode = True Then
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='I'") = True Then
                            MsgInformation("Item Status is Closed, So cann't be Saved. [" & Trim(.Text) & "]")
                            MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If

                        mFGItem = IsFGItem(mItemCode)
                        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 And mFGItem = True Then
                            If txtCustomerCode.Text = "11265" Then

                            Else
                                .Col = ColActualWidth
                                If Val(.Text) <= 0 Then
                                    MsgInformation("Please Enter the Width. Cannot Save.")
                                    '' MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                                    FieldsVarification = False
                                    Exit Function
                                End If

                                .Col = ColActualHeight
                                If Val(.Text) <= 0 Then
                                    MsgInformation("Please Enter the Weight. Cannot Save.")
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            End If

                        End If

                    End If

                    If VB.Left(cboRefType.Text, 1) = "G" Then
                        If MainClass.ValidateWithMasterTable(txtCustomerCode.Text, "SUPP_CUST_CODE", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mSupplierType = MasterNo
                        End If

                        If mSupplierType = "CUSTOMER-RM" Then
                            If GetSupplierRMBOM(mItemCode) = False Then
                                MsgBox("Supplier BOM not defined, So cann't be saved", MsgBoxStyle.Information)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If

                    mLotNoRequied = "N"

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mLotNoRequied = MasterNo
                    End If
                    If mLotNoRequied = "Y" Then
                        '                    If Left(cboRefType.Text, 1) = "G" Or Left(cboRefType.Text, 1) = "J" Or Left(cboRefType.Text, 1) = "R" Or Left(cboRefType.Text, 1) = "F" Then
                        If VB.Left(cboRefType.Text, 1) = "S" Or VB.Left(cboRefType.Text, 1) = "U" Or VB.Left(cboRefType.Text, 1) = "R" Then
                        Else
                            .Col = ColBatchNo
                            If Trim(.Text) <= "0" Or Trim(.Text) <= "" Then
                                MsgInformation("Lot No. Must For Such Item.")
                                FieldsVarification = False
                                MainClass.SetFocusToCell(SprdMain, mRow, ColBatchNo)
                                Exit Function
                            End If
                        End If
                        Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColBatchNo, mRow, mRow, ColPackQty, True))
                    End If


                    .Col = ColPackQty
                    If Val(.Text) <> 0 Then
                        mPackQty = Val(.Text)
                        mIsFGItem = IsFGItem(mItemCode)
                        mIsFixedAssets = GetProductionType(mItemCode)
                        mIsFixedAssets = IIf(mIsFixedAssets = "T", "A", mIsFixedAssets)

                        If (mIsFixedAssets = "I" Or mIsFixedAssets = "P") And RsCompany.Fields("IS_WAREHOUSE").Value = "N" Then
                            If VB.Left(cboRefType.Text, 1) = "G" Then
                                If mIsFixedAssets = "I" Then
                                    MsgBox("Item Category is In-House, So Cann't be Make Despatch Note in General.", MsgBoxStyle.Information)
                                Else
                                    MsgBox("Item Category is Finish Goods, So Cann't be Make Despatch Note in General.", MsgBoxStyle.Information)
                                End If
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If


                        If CDbl(lblDespType.Text) = 1 Then
                            If VB.Left(cboRefType.Text, 1) = "F" Then
                                If mIsFixedAssets <> "A" Then
                                    MsgBox("Item is not a Assets. Please select Only Assets Item.", MsgBoxStyle.Information)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            Else
                                If mIsFixedAssets = "A" Then
                                    MsgBox("Item is a Assets. Please select correct Ref Type.", MsgBoxStyle.Information)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            End If
                        End If

                        If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "E" Then
                            .Col = ColStockType
                            If mIsFixedAssets = "I" Or mIsFixedAssets = "B" Then
                                If Trim(.Text) = "ST" Or Trim(.Text) = "FG" Then
                                Else
                                    MsgBox("Please Select (ST/FG) Stock Type for Item Code " & mItemCode & ".", MsgBoxStyle.Information)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            Else
                                'If Trim(.Text) <> "FG" Then
                                '    MsgBox("Please Select (FG) Stock Type for Item Code " & mItemCode & ".", MsgBoxStyle.Information)
                                '    FieldsVarification = False
                                '    Exit Function
                                'End If
                            End If
                        ElseIf VB.Left(cboRefType.Text, 1) = "G" Then
                            .Col = ColStockType
                            If mIsFGItem = True And RsCompany.Fields("IS_WAREHOUSE").Value = "N" Then

                                If Trim(.Text) <> "FG" Then
                                    MsgBox("Please Select (FG) Stock Type for Item Code " & mItemCode & ".", MsgBoxStyle.Information)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                                If Trim(.Text) = "FG" Then
                                    MsgBox("For Finished Goods You cann't be Sale Agt General, Please Select Production Ref Type.", MsgBoxStyle.Information)
                                    FieldsVarification = False
                                    Exit Function
                                End If

                            Else
                                If Trim(.Text) = "FG" Or Trim(.Text) = "CS" Or Trim(.Text) = "RJ" Or Trim(.Text) = "CR" Then
                                    MsgBox("Please Select (ST OR SC) Stock Type for Item Code " & mItemCode & ".", MsgBoxStyle.Information)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            End If
                        ElseIf VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Then
                            'If GetProductionType(mItemCode) <> "J" Then
                            '    MsgBox("Item Category is not Job Work (Third Party) for Item Code : " & mItemCode & ".", MsgBoxStyle.Information)
                            '    FieldsVarification = False
                            '    Exit Function
                            'End If
                            .Col = ColStockType
                            If Trim(.Text) <> "CS" Then
                                MsgBox("Please Select (CS) Stock Type for Item Code " & mItemCode & ".", MsgBoxStyle.Information)
                                FieldsVarification = False
                                Exit Function
                            End If
                        ElseIf VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
                            .Col = ColStockType
                            'RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And 
                            If RsCompany.Fields("IS_WAREHOUSE").Value = "Y" Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then

                                .Row = mRow
                                .Col = ColMRRNo
                                'If Val(.Text) = 0 Then
                                '    MsgBox("Please Select MRR No for Item Code " & mItemCode & ".", MsgBoxStyle.Information)
                                '    FieldsVarification = False
                                '    Exit Function
                                'Else
                                mMRRNo = Val(.Text)
                                'End If

                            Else
                                If Trim(.Text) <> "RJ" Then
                                    MsgBox("Please Select (RJ) Stock Type for Item Code " & mItemCode & ".", MsgBoxStyle.Information)
                                    FieldsVarification = False
                                    Exit Function
                                End If

                                .Row = mRow
                                .Col = ColMRRNo
                                If Val(.Text) = 0 Then
                                    MsgBox("Please Select MRR No for Item Code " & mItemCode & ".", MsgBoxStyle.Information)
                                    FieldsVarification = False
                                    Exit Function
                                Else
                                    mMRRNo = Val(.Text)
                                End If
                            End If


                        ElseIf VB.Left(cboRefType.Text, 1) = "S" Then
                            .Col = ColStockType
                            If Trim(.Text) <> "CR" And RsCompany.Fields("IS_WAREHOUSE").Value = "N" Then
                                MsgBox("Please Select (CR) Stock Type for Item Code " & mItemCode & ".", MsgBoxStyle.Information)
                                FieldsVarification = False
                                Exit Function
                            End If
                            .Row = mRow
                            .Col = ColMRRNo
                            If Val(.Text) = 0 Then
                                MsgBox("Please Select Ref No for Item Code " & mItemCode & ".", MsgBoxStyle.Information)
                                FieldsVarification = False
                                Exit Function
                            Else
                                mMRRNo = Val(.Text)
                            End If

                            .Col = ColPackQty
                            pPackQty = Val(.Text)

                            mBalanceMRRQty = GetCRStockQty(mMRRNo, Trim(mItemCode), "", Int(mDivisionCode), "CR", "DSP" & Val(txtDNNo.Text)) '' GETCRBalanceQty(mItemCode, mMRRNo)

                            If mBalanceMRRQty < pPackQty Then
                                MsgBox("Balance MRR Qty ( " & mBalanceMRRQty & " ) is Less than Pack Qty ( " & pPackQty & " ) for Item Code " & mItemCode & ".", MsgBoxStyle.Information)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If

                        If RsCompany.Fields("PACKETS_COL_SHOW").Value = "Y" Then
                            .Col = ColInnerBoxQty
                            If Val(.Text) > 0 Then
                                .Col = ColPackType
                                If Trim(.Text) = "" Then
                                    MsgBox("You not define Packing Type of Item Code : " & mItemCode)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            End If
                        End If

                        .Col = ColChargeableWidth
                        mWidth = VB6.Format(Val(.Text), "0.00")

                        .Col = ColChargeableHeight
                        mHeight = VB6.Format(Val(.Text), "0.00")

                        .Col = ColModel
                        mModelNo = Trim(.Text)

                        .Col = ColDrawingNo
                        mDrawingNo = Trim(.Text)

                        .Col = ColStoreLoc
                        xStoreLoc = Trim(SprdMain.Text)

                        .Col = ColStockType

                        'If RsCompany.Fields("STOCKBALCHECK").Value = "Y" Then
                        If VB.Left(cboRefType.Text, 1) = "P" And mSameGSTNo = "N" Then       ''Trim(.Text) = "FG" And 
                            mScheduleQty = GetSalesDSQty(mItemCode, mDIRequired, mODNo, Trim(txtStoreLoc.Text), mWidth, mHeight, mModelNo, mDrawingNo)
                            mTotMonthPackQty = GetPackQty(mItemCode, mDIRequired, mODNo, mWidth, mHeight, mModelNo, mDrawingNo) + GetTotMonthDespatchQty(mItemCode, mDIRequired, mODNo, mWidth, mHeight, mModelNo, mDrawingNo, xStoreLoc)
                            If System.Math.Round(mTotMonthPackQty, 0) > System.Math.Round(mScheduleQty, 0) Then
                                MsgBox("Month Schedule for Item Code : " & mItemCode & " is " & mScheduleQty & " And you already Despatched " & mTotMonthPackQty & " Qty. Cann't be Saved", MsgBoxStyle.Information)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                        'End If

                        .Col = ColStockType
                        If Trim(.Text) = "QC" Then
                            MsgBox("You cann't select QC Stock Type. Please Check Stock Type " & mItemCode & ".", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If

                        .Col = ColPackQty

                        If (VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L") And Val(.Text) > 0 Then

                            If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                            Else
                                pPackQty = Val(.Text)

                                mBalanceDNQty = GETDNBalanceQty(mItemCode, mMRRNo)

                                If mBalanceDNQty < pPackQty Then
                                    MsgBox("Balance Debit Note Qty ( " & mBalanceDNQty & " ) is Less than Pack Qty ( " & pPackQty & " ) for Item Code " & mItemCode & ".", MsgBoxStyle.Information)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            End If

                        ElseIf (VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R") And Val(.Text) > 0 Then
                            .Col = ColRefNo
                            mRefNo = Trim(.Text)
                            If CheckDuplicate57F4(mItemCode, mRefNo) = True Then
                                FieldsVarification = False
                                Exit Function
                            End If
                            If mInvoiceMade = False Then
                                SprdMain.Row = mRow
                                xOutItemCode = "'" & mItemCode & "'"
                                xInItemCode = GetInJobworkItem(mItemCode, Trim(txtDNDate.Text), mInConUnit, mIsManyIn)

                                If VB.Left(cboRefType.Text, 1) = "R" Then
                                    mItemCode = "('" & mItemCode & "')"
                                    mIsManyIn = False
                                Else
                                    If xInItemCode = "" Then
                                        mItemCode = "('" & mItemCode & "')"
                                    Else
                                        mItemCode = "('" & mItemCode & "'," & xInItemCode & ")"
                                    End If
                                End If
                                mOutConUnit = 1

                                If mIsManyIn = False Then
                                    If FillREFDetail(mRow, xInItemCode, xOutItemCode, mInConUnit, mOutConUnit, mRefNo) = False Then
                                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, mRow)
                                        FieldsVarification = False
                                        Exit Function
                                    End If
                                Else
                                    .Row = mRow
                                    .Col = ColRefNo
                                    .Text = ""

                                End If
                            End If
                        End If
                    End If
                End If
NextRow:
            Next
        End With

        If VB.Left(cboRefType.Text, 1) = "S" Or VB.Left(cboRefType.Text, 1) = "P" Then
            With SprdMain
                For mRow = 1 To .MaxRows - 1
                    .Row = mRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                        xFGBatchNoReq = "Y"
                    Else
                        xFGBatchNoReq = "N"
                    End If

                    .Col = ColUnit
                    xIUOM = Trim(.Text)

                    .Col = ColStockType
                    mStockType = Trim(.Text)

                    .Col = ColHeatNo
                    mHeatNo = Trim(.Text)

                    .Col = ColBatchNo
                    mLotNo = Trim(.Text)

                    .Col = ColActualWidth
                    xActualWidth = Val(.Text)

                    .Col = ColActualHeight
                    xActualHeight = Val(.Text)

                    .Col = ColModel
                    xModel = Trim(.Text)

                    .Col = ColDrawingNo
                    xDrawingNo = Trim(.Text)


                    .Col = ColStockQty
                    mStockBal = GetBalanceStockQty(mItemCode, (txtDNDate.Text), xIUOM, "PAD", mStockType, mLotNo, ConWH, mDivisionCode, "DSP", Val(txtDNNo.Text), xFGBatchNoReq,, mHeatNo)
                    .Text = CStr(mStockBal)

                    .Col = ColBatchNo
                    If Trim(.Text) = "" Then
                        mDespQty = GetPackQty(mItemCode, "N", "", xActualWidth, xActualHeight, xModel, xDrawingNo)
                    Else
                        .Col = ColPackQty
                        mDespQty = Val(.Text)
                    End If

                    '                For cntRow = 1 To .MaxRows - 1
                    '                    .Row = cntRow
                    '                    .Col = ColItemCode
                    '                    If mItemCode = Trim(.Text) Then
                    '                        .Col = ColPackQty
                    '                        mDespQty = mDespQty + Val(.Text)
                    '                    End If
                    '                Next
                    If RsCompany.Fields("STOCKBALCHECK").Value = "Y" Then
                        If mStockBal < mDespQty Then
                            MsgBox("Stock Qty (" & mStockBal & ") is Less than Pack Qty (" & mDespQty & ") for Item Code : " & mItemCode & ".", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                Next
            End With
        End If

        If VB.Left(cboRefType.Text, 1) <> "U" Then
            If CheckStockQty(SprdMain, ColStockQty, ColPackQty, ColItemCode, ColStockType, True) = False Then
                FieldsVarification = False
                Exit Function
            End If

            If RsCompany.Fields("MINIMUN_QTY_CHECK_DESP").Value = "Y" Then
                If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then

                Else
                    If CheckMinimumStockQty() = False Then
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

            End If
        End If


        If CheckRowCount() = False Then
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColReceivedQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False: Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function

        If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
        Else
            If VB.Left(cboRefType.Text, 1) = "Q" Then
                If MainClass.ValidDataInGrid(SprdMain, ColMRRNo, "N", "Please Check MRR No.") = False Then FieldsVarification = False : Exit Function
            End If
        End If


        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Public Function CheckMinimumStockQty() As Boolean
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mStockQty As Double
        Dim mCheck1Qty As Double
        Dim mStockType As String = ""
        Dim mItemCode As String
        Dim mProdType As String
        Dim mMinimumQty As Double
        Dim mAllowQty As Double

        CheckMinimumStockQty = True


        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)


                .Col = ColPackQty
                mCheck1Qty = Val(.Text)

                If mCheck1Qty = 0 Then GoTo NextRow


                .Col = ColStockQty
                mStockQty = Val(.Text)

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "MINIMUM_QTY", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mMinimumQty = Val(MasterNo)
                    mAllowQty = CheckDespatchAllowQty(txtCustomerCode.Text, mItemCode, VB6.Format(txtDNDate.Text, "DD/MM/YYYY"))

                    If mAllowQty >= mCheck1Qty Then

                    Else
                        If mStockQty - mCheck1Qty < mMinimumQty And mMinimumQty <> 0 Then
                            MsgInformation("You Have Not Enough Stock to manage minimum Qty (" & mMinimumQty & "). For Item Code : " & mItemCode)
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColPackQty)
                            CheckMinimumStockQty = False
                            Exit Function
                        End If
                    End If

                End If

NextRow:
            Next
        End With
        Exit Function
ErrPart:
        CheckMinimumStockQty = False
    End Function
    Private Function CheckLotStockQty() As Boolean

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mLotNo As String
        Dim mAllStockQty As Double
        Dim mStockQty As Double
        Dim mLotQty As Double
        Dim mAutoQCIssue As String
        Dim mStockType As String = ""
        Dim mItemUOM As String = ""
        Dim mDivisionCode As Double
        Dim mCommonDivision As Double
        Dim mHeatNo As String

        Dim I As Integer

        If cboDivision.SelectedIndex >= 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        Else
            CheckLotStockQty = True
            Exit Function
        End If


        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColUnit
                mItemUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)


                .Col = ColBatchNo
                mLotNo = Trim(.Text)

                .Col = ColStockQty
                mStockQty = Val(.Text)

                .Col = ColHeatNo
                mHeatNo = Val(.Text)

                '            .Col = ColPackQty
                '            mLotQty = Trim(.Text)

                If mLotNo <> "" Then
                    mLotQty = 0
                    For I = 1 To .MaxRows - 1
                        .Row = I

                        .Col = ColItemCode
                        If mItemCode = Trim(.Text) Then
                            .Col = ColPackQty
                            mLotQty = mLotQty + Val(.Text)
                        End If
                    Next

                    .Row = cntRow

                    If mLotQty > 0 Then
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "STOCKITEM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOCKITEM='N'") = False Then

                            If MainClass.ValidateWithMasterTable(mItemCode, "AUTO_INDENT", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_INDENT='Y'") = True Then
                                mAutoQCIssue = "Y"
                            Else
                                mAutoQCIssue = "N"
                            End If

                            mCommonDivision = GetCommonDivCode()
                            mAllStockQty = GetBalanceStockQty(mItemCode, (txtDNDate.Text), mItemUOM, "STR", mStockType, "", ConWH, mDivisionCode, ConStockRefType_DSP, Val(txtDNNo.Text),,, mHeatNo)

                            If mDivisionCode <> mCommonDivision Then
                                If mCommonDivision > 0 Then
                                    mAllStockQty = mAllStockQty + GetBalanceStockQty(mItemCode, (txtDNDate.Text), mItemUOM, "STR", mStockType, "", ConWH, mCommonDivision, ConStockRefType_DSP, Val(txtDNNo.Text),,, mHeatNo)
                                End If
                            End If

                            If mAllStockQty < mLotQty And mLotQty <> 0 Then
                                MsgInformation("You Have Not Enough Stock. For Item Code : " & mItemCode)
                                MainClass.SetFocusToCell(SprdMain, cntRow, ColPackQty)
                                CheckLotStockQty = False
                                Exit Function
                            End If
                        End If
                    End If
                End If
NextRow:
            Next
        End With
        CheckLotStockQty = True
        Exit Function
ErrPart:
        CheckLotStockQty = False
    End Function
    Private Function GetSOMaxAmendNo(ByRef pSONo As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        GetSOMaxAmendNo = 0
        SqlStr = "SELECT AMEND_NO " & vbCrLf & " FROM  DSP_SALEORDER_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_SO=" & Val(CStr(pSONo)) & " AND SO_APPROVED='Y'" & vbCrLf & " AND IH.MKEY = ("


        SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf & " WHERE SIH.MKEY=SID.MKEY AND SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SIH.AUTO_KEY_SO=" & Val(CStr(pSONo)) & " AND SO_APPROVED='Y'" & vbCrLf & " AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSOMaxAmendNo = Val(IIf(IsDBNull(RsTemp.Fields("AMEND_NO").Value), 0, RsTemp.Fields("AMEND_NO").Value))
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetSOMaxAmendNo = 0
    End Function
    Private Function GetPackQty(ByRef pItemCode As String, mDIRequired As String, mODNo As String, ByRef mWidth As Double, ByRef mHeight As Double, ByRef mModelNo As String, ByRef mDrawingNo As String) As Double
        On Error GoTo err_Renamed

        Dim mPackQty As Double
        Dim mRow As Integer
        Dim xItemCode As String
        Dim pCheckItemCode As String

        pCheckItemCode = pItemCode
        pCheckItemCode = pCheckItemCode & "-" & VB6.Format(mWidth, "0.00") ''IIf(mWidth > 0, "-" & VB6.Format(mWidth, "0.00"), "")

        pCheckItemCode = pCheckItemCode & "-" & VB6.Format(mHeight, "0.00")

        pCheckItemCode = pCheckItemCode & "-" & Trim(mModelNo)

        pCheckItemCode = pCheckItemCode & "-" & Trim(mDrawingNo)

        GetPackQty = 0
        With SprdMain
            For mRow = 1 To .MaxRows - 1
                .Row = mRow
                .Col = ColItemCode
                xItemCode = Trim(.Text)

                .Col = ColActualWidth
                xItemCode = xItemCode & "-" & VB6.Format(Val(.Text), "0.00")

                .Col = ColActualHeight
                xItemCode = xItemCode & "-" & VB6.Format(Val(.Text), "0.00")

                .Col = ColModel
                xItemCode = xItemCode & "-" & Trim(.Text)

                .Col = ColDrawingNo
                xItemCode = xItemCode & "-" & Trim(.Text)

                If Trim(xItemCode) = Trim(pCheckItemCode) Then
                    If mDIRequired = "N" Then
                        .Col = ColPackQty
                        mPackQty = Val(.Text)
                        GetPackQty = GetPackQty + mPackQty
                    Else
                        .Col = ColODNo
                        If Trim(.Text) = Trim(mODNo) Then
                            .Col = ColPackQty
                            mPackQty = Val(.Text)
                            GetPackQty = GetPackQty + mPackQty
                        End If
                    End If
                End If
            Next
        End With

        'If mODNo = "" Then
        '    mSqlStr = mSqlStr & vbCrLf & " AND (OD_NO='' OR OD_NO IS NULL)"
        'Else
        '    mSqlStr = mSqlStr & vbCrLf & " AND OD_NO='" & mODNo & "'"
        'End If

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmDespatchNote_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = IIf(lblDespType.Text = "1", "Despatch Note", IIf(lblDespType.Text = "2", "Gate Pass for Vendor Rejection", "Despatch Note"))

        SqlStr = ""
        SqlStr = "Select * from DSP_DESPATCH_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from DSP_DESPATCH_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mCustomerCode = CStr(-1)


        cboRefType.Items.Clear()

        If lblDespType.Text = "" Then
            cboRefType.Items.Add("Production")
            cboRefType.Items.Add("General")
            cboRefType.Items.Add("Job Work")
            cboRefType.Items.Add("QC Rejection")
            cboRefType.Items.Add("Line Rejection")
            cboRefType.Items.Add("Sale Rejection")
            cboRefType.Items.Add("U:Supplementry")
            cboRefType.Items.Add("Export")
            cboRefType.Items.Add("Rejection (Job Work)")
            cboRefType.Items.Add("Fixed Assets")
        ElseIf lblDespType.Text = "1" Then
            cboRefType.Items.Add("Production")
            cboRefType.Items.Add("General")
            cboRefType.Items.Add("Job Work")
            cboRefType.Items.Add("Sale Rejection")
            cboRefType.Items.Add("U:Supplementry")
            cboRefType.Items.Add("Export")
            cboRefType.Items.Add("Rejection (Job Work)")
            cboRefType.Items.Add("Fixed Assets")
        ElseIf lblDespType.Text = "2" Then
            cboRefType.Items.Add("QC Rejection")
            cboRefType.Items.Add("Line Rejection")
        End If

        cboStatus.Items.Clear()
        cboStatus.Items.Add("Not Consider")
        cboStatus.Items.Add("Consider")
        cboStatus.Items.Add("Cancelled")

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

        cmdAdd.Visible = True
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())


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
        Dim SqlStr As String = ""
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)
        SqlStr = ""

        'MainClass.ClearGrid(SprdView) 

        SqlStr = "Select DC.AUTO_KEY_DESP as DN_No," & vbCrLf & " TO_CHAR(DC.DESP_DATE,'DD-MM-YYYY') as DN_Date, " & vbCrLf _
            & " AC.SUPP_CUST_NAME AS CustomerName, " & vbCrLf _
            & " DECODE(DESP_STATUS,2,'Cancelled',DECODE(DESP_STATUS,1,'Consider','Not Consider')) AS Status, " & vbCrLf _
            & " CASE WHEN DESP_TYPE='P' THEN 'Production'  " & vbCrLf _
            & " WHEN DESP_TYPE='G' THEN 'General' " & vbCrLf _
            & " WHEN DESP_TYPE='J' THEN 'Job Work' WHEN DESP_TYPE='R' THEN 'Rejection (Job Work)'" & vbCrLf _
            & " WHEN DESP_TYPE='Q' THEN 'QC Rejection' " & vbCrLf & " WHEN DESP_TYPE='L' THEN 'Line Rejection MEMO' " & vbCrLf _
            & " WHEN DESP_TYPE='S' THEN 'Sale' WHEN DESP_TYPE='F' THEN 'Fixed Assets'" & vbCrLf _
            & " WHEN DESP_TYPE='U' THEN 'Supplementry' WHEN DESP_TYPE='E' THEN 'Export' END AS Type, " & vbCrLf _
            & " VENDOR_PO, EXPORT_BILL_NO, TRANSPORTER_NAME As TRANSPORTER,VEHICLE_NO AS VEHICLE ,LOADING_TIME As TIME, DC.ADDUSER, DC.ADDDATE" & vbCrLf _
            & " FROM DSP_DESPATCH_HDR DC,FIN_SUPP_CUST_MST AC " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " DC.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND DC.COMPANY_CODE=AC.COMPANY_CODE " & vbCrLf & " AND DC.SUPP_CUST_CODE=AC.SUPP_CUST_CODE " & vbCrLf & " "

       

        If lblDespType.Text = "1" Then
            SqlStr = SqlStr & vbCrLf & " AND DESP_TYPE IN ('P','G','J','S','U','E','F','R')"
        ElseIf lblDespType.Text = "2" Then
            SqlStr = SqlStr & vbCrLf & " AND DESP_TYPE IN ('Q','L')"
        End If

        SqlStr = SqlStr & " Order by AUTO_KEY_DESP"

        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        'FormatSprdView()
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
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume
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
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "DN No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "DN Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Customer Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Status"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Despatch Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Customer PO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Export Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Transporter Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Vehicle No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Loading Time"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "ADD User"
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "ADD Date"


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
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 100


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
    'Private Sub FormatSprdView()

    '    With SprdView
    '        .Row = -1

    '        .set_RowHeight(0, 600)

    '        .set_ColWidth(0, 600)

    '        .set_ColWidth(1, 1200)
    '        .set_ColWidth(2, 1000)
    '        .set_ColWidth(3, 3500)
    '        .set_ColWidth(4, 1000)
    '        .set_ColWidth(5, 1200)
    '        .set_ColWidth(6, 1500)
    '        .set_ColWidth(7, 1500)
    '        .set_ColWidth(8, 1200)
    '        .set_ColWidth(9, 1200)

    '        MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
    '        MainClass.SetSpreadColor(SprdView, -1)
    '        SprdView.set_RowHeight(-1, 300)
    '        .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
    '        MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
    '    End With
    'End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            SprdMain.Row = 0
            SprdMain.Col = ColPackQty
            SprdMain.Text = "Qty/UOM"

            SprdMain.Col = ColInnerBoxQty
            SprdMain.Text = "Coil Qty"

            SprdMain.Col = ColInnerBoxCode
            SprdMain.Text = "Coil Box Code"
        End If

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            For cntCol = ColSONo To ColCustomerDate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
                .TypeEditMultiLine = False
                If cntCol = ColSONo Then
                    .TypeEditLen = RsDNDetail.Fields("SONO").Precision ''
                ElseIf cntCol = ColCustomerDate Or cntCol = ColSODate Then
                    .TypeEditLen = 10
                Else
                    .TypeEditLen = RsDNDetail.Fields("CUST_PO").DefinedSize
                End If
                .set_ColWidth(cntCol, 6)
                .ColHidden = True
            Next

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsDNDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .ColsFrozen = ColItemDesc
            .set_ColWidth(ColItemDesc, 29)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColPartNo, 12)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsDNDetail.Fields("ITEM_UOM").DefinedSize ''
            .set_ColWidth(ColUnit, 4)

            .Col = ColLotNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsDNDetail.Fields("LOT_NO").DefinedSize ''MainClass.SetMaxLength("LOT_NO", "INV_GATE_DET", PubDBCn)
            .set_ColWidth(ColLotNo, 8)
            .ColHidden = True

            .Col = ColHeatNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsDNDetail.Fields("HEAT_NO").DefinedSize
            .set_ColWidth(ColHeatNo, 8)
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColStoreLoc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsDNDetail.Fields("LOC_CODE").DefinedSize
            .set_ColWidth(ColStoreLoc, 8)
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColODNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsDNDetail.Fields("OD_NO").DefinedSize
            .set_ColWidth(ColStoreLoc, 8)
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColBatchNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsDNDetail.Fields("BATCH_NO").DefinedSize
            .set_ColWidth(ColBatchNo, 8)

            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsDNDetail.Fields("MRR_REF_NO").DefinedSize
            .set_ColWidth(ColMRRNo, 9)
            If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Or VB.Left(cboRefType.Text, 1) = "S" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsDNDetail.Fields("REF_NO").DefinedSize
            .set_ColWidth(ColRefNo, 9)
            If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Or VB.Left(cboRefType.Text, 1) = "U" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefDate, 9)
            .ColHidden = True

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStockQty, 9)

            .Col = ColBalScheduleQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBalScheduleQty, 9)

            .Col = Col57BalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(Col57BalQty, 9)
            If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColPackQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPackQty, 9)

            .Col = ColPktQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeNumberMax = CDbl("99999.99")
            .TypeNumberMin = CDbl("0")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPktQty, 6)
            .ColHidden = True

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsDNDetail.Fields("STOCK_TYPE").DefinedSize ''
            .set_ColWidth(ColStockType, 3.5)

            .Col = ColJITCallNo
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeNumberMax = CDbl("9999999")
            .TypeNumberMin = CDbl("0")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColJITCallNo, 6)
            If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "S" Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Else
                .ColHidden = True
            End If

            .Col = ColPackType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsDNDetail.Fields("PACK_TYPE").DefinedSize
            .set_ColWidth(ColPackType, 8)
            .ColHidden = IIf(RsCompany.Fields("PACKETS_COL_SHOW").Value = "Y", False, True)

            .Col = ColInnerBoxQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104, 2, 0)
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDNDetail.Fields("INNER_PACK_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)
            .ColHidden = IIf(RsCompany.Fields("INNER_PACK_COL_SHOW").Value = "Y" Or RsCompany.Fields("PACKETS_COL_SHOW").Value = "Y", False, True)


            .Col = ColInnerBoxCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsDNDetail.Fields("INNER_PACK_ITEM_CODE").DefinedSize ''						
            .set_ColWidth(.Col, 10)
            .ColHidden = IIf(RsCompany.Fields("INNER_PACK_COL_SHOW").Value = "Y", False, True)


            .Col = ColOuterBoxQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDNDetail.Fields("OUTER_PACK_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)
            .ColHidden = IIf(RsCompany.Fields("OUTER_PACK_COL_SHOW").Value = "Y", False, True)

            .Col = ColOuterBoxCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsDNDetail.Fields("OUTER_PACK_ITEM_CODE").DefinedSize ''						
            .set_ColWidth(.Col, 10)
            .ColHidden = IIf(RsCompany.Fields("OUTER_PACK_COL_SHOW").Value = "Y", False, True)

            For cntCol = ColActualWidth To ColChargeableHeight
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

            .Col = ColActualWidth
            .ColHidden = True

            .Col = ColActualHeight
            .ColHidden = True

            .Col = ColArea
            .ColHidden = True

            .Col = ColGlassDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsDNDetail.Fields("GLASS_DESC").DefinedSize ''				
            .set_ColWidth(.Col, 20)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColModel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsDNDetail.Fields("ITEM_MODEL").DefinedSize ''
            .set_ColWidth(ColModel, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColDrawingNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsDNDetail.Fields("ITEM_DRAWINGNO").DefinedSize ''
            .set_ColWidth(ColDrawingNo, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColChargeableArea
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)


        End With

        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColJITCallNo)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColGlassDescription, ColDrawingNo)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStoreLoc, ColStoreLoc)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColSONo, ColCustomerDate)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUnit)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, Col57BalQty, ColBalScheduleQty)

        '    If ADDMode = True Then
        If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMRRNo, ColMRRNo)
        ElseIf VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Or VB.Left(cboRefType.Text, 1) = "S" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRefNo, ColRefDate)
        Else
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMRRNo, ColRefDate)
        End If
        '    Else
        '        If Left(cboRefType.Text, 1) = "J" Then
        '            MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColMRRNo, ColMRRNo
        '        Else
        '            MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColMRRNo, ColRefDate
        '        End If
        '    End If

        If VB.Left(cboRefType.Text, 1) = "E" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemCode)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPackQty, ColPackQty)
        End If

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColActualWidth, ColChargeableArea)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPktQty, ColPktQty)

        MainClass.SetSpreadColor(SprdMain, Arow)
        '    SprdMain.OperationMode = SS_OP_MODE_ROWMODE
        '
        '    ' Set the spreadsheet to always use edit mode
        '    SprdMain.EditModePermanent = True
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsDNDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsDNMain
            txtDNNo.MaxLength = .Fields("AUTO_KEY_DESP").Precision
            txtDNDate.MaxLength = 10
            TxtCustomerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtBillTo.MaxLength = .Fields("BILL_TO_LOC_ID").DefinedSize
            TxtShipTo.MaxLength = .Fields("SHIP_TO_LOC_ID").DefinedSize
            txtStoreLoc.MaxLength = .Fields("LOC_CODE").DefinedSize
            txtLoadingTime.MaxLength = 5
            txtVehicleNo.MaxLength = .Fields("VEHICLE_NO").DefinedSize
            txtSONo.MaxLength = .Fields("AUTO_KEY_SO").Precision
            txtSODate.MaxLength = 10
            txtCustPoNo.MaxLength = .Fields("VENDOR_PO").DefinedSize
            txtCustPODate.MaxLength = 10
            TxtTransporter.MaxLength = .Fields("TRANSPORTER_NAME").DefinedSize
            txtPrepared.MaxLength = .Fields("PRE_EMP_CODE").DefinedSize
            txtExportInvoiceNo.MaxLength = .Fields("EXPORT_BILL_NO").DefinedSize


            TxtGRNo.MaxLength = .Fields("GRNo").DefinedSize ''
            TxtGRDate.MaxLength = 10
            txtSuppToDate.MaxLength = 10
            txtSuppFromDate.MaxLength = 10
            txtAmendNo.MaxLength = 4
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mDespRefType As String
        Dim Cnt As Integer
        Dim mShippTo As String
        Dim mShippToCode As String

        With RsDNMain
            If Not .EOF Then
                LblMkey.Text = .Fields("AUTO_KEY_DESP").Value
                txtDNNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_DESP").Value), "", .Fields("AUTO_KEY_DESP").Value)
                txtDNDate.Text = VB6.Format(IIf(IsDBNull(.Fields("DESP_DATE").Value), "", .Fields("DESP_DATE").Value), "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtCustomerName.Text = MasterNo
                End If
                mCustomerCode = .Fields("SUPP_CUST_CODE").Value

                txtCustomerCode.Text = Trim(mCustomerCode)
                Call txtCustomerCode_Validating(txtCustomerCode, New System.ComponentModel.CancelEventArgs(True))

                mShippTo = IIf(IsDBNull(.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", .Fields("SHIPPED_TO_SAMEPARTY").Value)
                chkShipTo.CheckState = IIf(mShippTo = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)

                txtStoreLoc.Text = IIf(IsDBNull(.Fields("LOC_CODE").Value), "", .Fields("LOC_CODE").Value)

                If mShippTo = "Y" Then
                    mShippToCode = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                    TxtShipTo.Text = txtBillTo.Text
                Else
                    mShippToCode = IIf(IsDBNull(.Fields("SHIPPED_TO_PARTY_CODE").Value), "", .Fields("SHIPPED_TO_PARTY_CODE").Value)
                    TxtShipTo.Text = IIf(IsDBNull(.Fields("SHIP_TO_LOC_ID").Value), "", .Fields("SHIP_TO_LOC_ID").Value)
                End If


                If MainClass.ValidateWithMasterTable(mShippToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtShipCustomer.Text = MasterNo
                End If

                txtAddress.Text = GetPartyBusinessDetail(Trim(mShippToCode), Trim(TxtShipTo.Text), "SUPP_CUST_ADDR || ', ' || SUPP_CUST_CITY || ', ' || SUPP_CUST_STATE")

                txtLoadingTime.Text = VB6.Format(IIf(IsDBNull(.Fields("LOADING_TIME").Value), "", .Fields("LOADING_TIME").Value), "HH:MM")

                txtVehicleNo.Text = IIf(IsDBNull(.Fields("VEHICLE_NO").Value), "", .Fields("VEHICLE_NO").Value)

                If IsDBNull(.Fields("AUTO_KEY_SO").Value) Then
                    txtSONo.Text = ""
                Else
                    txtSONo.Text = IIf(.Fields("AUTO_KEY_SO").Value = 0, "", .Fields("AUTO_KEY_SO").Value)
                End If

                txtSODate.Text = VB6.Format(IIf(IsDBNull(.Fields("SO_DATE").Value), "", .Fields("SO_DATE").Value), "DD/MM/YYYY")
                txtCustPoNo.Text = IIf(IsDBNull(.Fields("VENDOR_PO").Value), "", .Fields("VENDOR_PO").Value)
                txtCustPODate.Text = VB6.Format(IIf(IsDBNull(.Fields("VENDOR_PO_DATE").Value), "", .Fields("VENDOR_PO_DATE").Value), "DD/MM/YYYY")
                TxtTransporter.Text = IIf(IsDBNull(.Fields("TRANSPORTER_NAME").Value), "", .Fields("TRANSPORTER_NAME").Value)
                txtPrepared.Text = IIf(IsDBNull(.Fields("PRE_EMP_CODE").Value), "", .Fields("PRE_EMP_CODE").Value)
                txtExportInvoiceNo.Text = IIf(IsDBNull(.Fields("EXPORT_BILL_NO").Value), "", .Fields("EXPORT_BILL_NO").Value)


                TxtGRNo.Text = IIf(IsDBNull(.Fields("GRNO").Value), "", .Fields("GRNO").Value)
                TxtGRDate.Text = IIf(IsDBNull(.Fields("GRDATE").Value), "", .Fields("GRDATE").Value)


                If .Fields("DESP_STATUS").Value = "0" Then
                    cboStatus.SelectedIndex = 0
                ElseIf .Fields("DESP_STATUS").Value = "1" Then
                    cboStatus.SelectedIndex = 1
                ElseIf .Fields("DESP_STATUS").Value = "2" Then
                    cboStatus.SelectedIndex = 2
                End If

                If .Fields("DESP_TYPE").Value = "P" Then
                    cboRefType.Text = "Production"
                ElseIf .Fields("DESP_TYPE").Value = "G" Then
                    cboRefType.Text = "General"
                ElseIf .Fields("DESP_TYPE").Value = "J" Then
                    cboRefType.Text = "Job Work"
                ElseIf .Fields("DESP_TYPE").Value = "Q" Then
                    cboRefType.Text = "QC Rejection"
                ElseIf .Fields("DESP_TYPE").Value = "L" Then
                    cboRefType.Text = "Line Rejection"
                ElseIf .Fields("DESP_TYPE").Value = "S" Then
                    cboRefType.Text = "Sale Rejection"
                ElseIf .Fields("DESP_TYPE").Value = "U" Then
                    cboRefType.Text = "U:Supplementry"
                ElseIf .Fields("DESP_TYPE").Value = "E" Then
                    cboRefType.Text = "Export"
                ElseIf .Fields("DESP_TYPE").Value = "R" Then
                    cboRefType.Text = "Rejection (Job Work)"
                ElseIf .Fields("DESP_TYPE").Value = "F" Then
                    cboRefType.Text = "Fixed Assets"
                End If


                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                cboDivision.Enabled = IIf(.Fields("DESP_STATUS").Value = "1" Or .Fields("DESP_STATUS").Value = "2", False, True)

                If txtTransportCode.Text = "" Then
                    If MainClass.ValidateWithMasterTable(TxtTransporter.Text, "TRANSPORTER_NAME", "TRANSPORTER_ID", "FIN_TRANSPORTER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtTransportCode.Text = Trim(MasterNo)
                    End If
                End If

                Dim mTransMode As String = IIf(IsDBNull(.Fields("TRANSPORT_MODE").Value), "0", .Fields("TRANSPORT_MODE").Value)

                mTransMode = Mid(mTransMode, 1, 1)
                cboTransmode.SelectedIndex = Val(mTransMode) - 1

                Dim mVehicleType As String = IIf(IsDBNull(.Fields("VEHICLE_TYPE").Value), "R", .Fields("VEHICLE_TYPE").Value)
                cboVehicleType.SelectedIndex = IIf(mVehicleType = "R", 0, 1)

                Call ShowDetail1((LblMkey.Text), mDivisionCode)
                TxtCustomerName.Enabled = False
                txtCustomerCode.Enabled = False

                txtSONo.Enabled = False
                txtSODate.Enabled = False
                cmdSearchSo.Enabled = False
                cmdsearch.Enabled = False

                chkShipTo.Enabled = IIf(PubUserID = "G0416", True, False)
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
                    chkShipTo.Enabled = True
                End If

                TxtShipTo.Enabled = False
                txtBillTo.Enabled = False
                txtShipCustomer.Enabled = False
                cmdsearchShipTo.Enabled = False
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        cmdGetData.Enabled = False
        txtStoreLoc.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsDNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtDNNo.Enabled = True

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowDetail1(ByRef mMKey As String, ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mStockType As String = ""
        Dim mLotNo As String
        Dim xFGBatchNoReq As String
        Dim mHeatNo As String
        Dim mDIRequired As String = "N"
        Dim mODNo As String = ""
        Dim mSOAmendNo As Long
        Dim xStoreLoc As String
        Dim mSOMKey As String
        Dim mScheduleQty As Double
        Dim mTotMonthPackQty As Double
        Dim mWidth As Double
        Dim mHeight As Double
        Dim mModelNo As String
        Dim mDrawingNo As String
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM DSP_DESPATCH_DET " & vbCrLf _
            & " Where AUTO_KEY_DESP=" & Val(mMKey) & "" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsDNDetail
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColSONo
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("SONo").Value), "", .Fields("SONo").Value))

                SprdMain.Col = ColSODate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("SODATE").Value), "", .Fields("SODATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColCustomerNo
                SprdMain.Text = IIf(IsDBNull(.Fields("CUST_PO").Value), "", .Fields("CUST_PO").Value)

                SprdMain.Col = ColCustomerDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("CUST_PO_DATE").Value), "", .Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = Trim(mItemCode)

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    xFGBatchNoReq = "Y"
                Else
                    xFGBatchNoReq = "N"
                End If

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = Trim(mItemDesc)

                SprdMain.Col = ColPartNo
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mPartNo = MasterNo
                SprdMain.Text = Trim(mPartNo)

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                'SprdMain.Col = ColLotNo
                'mLotNo = IIf(IsDBNull(.Fields("LOT_NO").Value), "", .Fields("LOT_NO").Value)
                'SprdMain.Text = IIf(mLotNo = "0", "", mLotNo)

                SprdMain.Col = ColStoreLoc
                SprdMain.Text = IIf(IsDBNull(.Fields("LOC_CODE").Value), "", .Fields("LOC_CODE").Value)
                xStoreLoc = Trim(SprdMain.Text)

                SprdMain.Col = ColODNo
                SprdMain.Text = IIf(IsDBNull(.Fields("OD_NO").Value), "", .Fields("OD_NO").Value)

                SprdMain.Col = ColHeatNo
                SprdMain.Text = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)
                mHeatNo = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)

                SprdMain.Col = ColBatchNo
                SprdMain.Text = IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)
                mLotNo = IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)

                SprdMain.Col = ColMRRNo
                If IsDBNull(.Fields("MRR_REF_NO").Value) Then
                    SprdMain.Text = ""
                Else
                    SprdMain.Text = IIf(.Fields("MRR_REF_NO").Value = 0, "", Val(.Fields("MRR_REF_NO").Value))
                End If

                SprdMain.Col = ColRefNo
                If IsDBNull(.Fields("REF_NO").Value) Then
                    SprdMain.Text = ""
                    SprdMain.Col = ColRefDate
                    SprdMain.Text = ""
                Else
                    SprdMain.Text = IIf(IsDBNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)  '' IIf(.Fields("REF_NO").Value = 0, "", Trim(.Fields("REF_NO").Value))
                    SprdMain.Col = ColRefDate
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")
                    If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Then
                        SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColRefNo, I, ColItemCode, I, False))
                    End If
                End If

                SprdMain.Row = I
                SprdMain.Col = ColStockType
                mStockType = IIf(IsDBNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)
                SprdMain.Text = mStockType

                SprdMain.Col = ColStockQty ''Val(IIf(IsNull(!PACKED_QTY), 0, !PACKED_QTY)) +
                If VB.Left(cboRefType.Text, 1) = "U" Then
                    SprdMain.Text = "0.00"
                Else
                    SprdMain.Text = GetBalanceStockQty(mItemCode, (txtDNDate.Text), .Fields("ITEM_UOM").Value, "PAD", mStockType, mLotNo, ConWH, mDivisionCode, "DSP", Val(txtDNNo.Text), xFGBatchNoReq,, mHeatNo)
                End If

                SprdMain.Col = Col57BalQty

                '            mPOQty = CalcPOQty(mCustomerCode, mRefPoNo, !ITEM_CODE, !REF_TYPE)
                '            SprdMain.Text = mPOQty

                SprdMain.Col = ColPackQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PACKED_QTY").Value), 0, .Fields("PACKED_QTY").Value)))

                SprdMain.Col = ColPktQty
                '            SprdMain.Text = Val(IIf(IsNull(!PACKED_QTY), 0, !PACKED_QTY))      ''20-10-2010
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("NO_OF_PACKETS").Value), 0, .Fields("NO_OF_PACKETS").Value)))

                SprdMain.Col = ColJITCallNo
                SprdMain.Text = IIf(IsDBNull(.Fields("JITCALLNO").Value), "", .Fields("JITCALLNO").Value)

                SprdMain.Col = ColPackType
                SprdMain.Text = IIf(IsDBNull(.Fields("PACK_TYPE").Value), "", .Fields("PACK_TYPE").Value)

                SprdMain.Col = ColInnerBoxQty
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                    SprdMain.Text = Format(IIf(IsDBNull(.Fields("INNER_PACK_QTY").Value), 0, .Fields("INNER_PACK_QTY").Value), "0.00")
                Else
                    SprdMain.Text = Format(IIf(IsDBNull(.Fields("INNER_PACK_QTY").Value), 0, .Fields("INNER_PACK_QTY").Value), "0")
                End If


                SprdMain.Col = ColInnerBoxCode
                SprdMain.Text = IIf(IsDBNull(.Fields("INNER_PACK_ITEM_CODE").Value), "", .Fields("INNER_PACK_ITEM_CODE").Value)

                SprdMain.Col = ColOuterBoxQty
                SprdMain.Text = Format(IIf(IsDBNull(.Fields("OUTER_PACK_QTY").Value), 0, .Fields("OUTER_PACK_QTY").Value), "0")

                SprdMain.Col = ColOuterBoxCode
                SprdMain.Text = IIf(IsDBNull(.Fields("OUTER_PACK_ITEM_CODE").Value), "", .Fields("OUTER_PACK_ITEM_CODE").Value)

                SprdMain.Col = ColActualHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_HEIGHT").Value), 0, .Fields("ACTUAL_HEIGHT").Value)))
                ''mHeight = Val(SprdMain.Text)

                SprdMain.Col = ColActualWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_WIDTH").Value), 0, .Fields("ACTUAL_WIDTH").Value)))
                ''mWidth = Val(SprdMain.Text)

                SprdMain.Col = ColChargeableHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_HEIGHT").Value), 0, .Fields("CHARGEABLE_HEIGHT").Value)))
                mHeight = Val(SprdMain.Text)

                SprdMain.Col = ColChargeableWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_WIDTH").Value), 0, .Fields("CHARGEABLE_WIDTH").Value)))
                mWidth = Val(SprdMain.Text)

                SprdMain.Col = ColChargeableArea
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GLASS_AREA").Value), 0, .Fields("GLASS_AREA").Value)))

                SprdMain.Col = ColGlassDescription
                SprdMain.Text = IIf(IsDBNull(.Fields("GLASS_DESC").Value), "", .Fields("GLASS_DESC").Value)


                SprdMain.Col = ColModel
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value)
                mModelNo = SprdMain.Text

                SprdMain.Col = ColDrawingNo
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_DRAWINGNO").Value), "", .Fields("ITEM_DRAWINGNO").Value)
                mDrawingNo = SprdMain.Text

                If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "G" Then
                    mSOAmendNo = GetSOMaxAmendNo(CDbl(VB6.Format(Val(txtSONo.Text))))
                    mSOMKey = CDbl(Val(txtSONo.Text) & VB6.Format(mSOAmendNo, "000"))

                    If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDIRequired = MasterNo
                    End If

                    If mDIRequired = "Y" Then
                        SprdMain.Col = ColODNo
                        mODNo = SprdMain.Text
                    End If

                    mScheduleQty = GetSalesDSQty(mItemCode, mDIRequired, mODNo, Trim(txtStoreLoc.Text), mWidth, mHeight, mModelNo, mDrawingNo)
                    mTotMonthPackQty = GetTotMonthDespatchQty(mItemCode, mDIRequired, mODNo, mWidth, mHeight, mModelNo, mDrawingNo, xStoreLoc)

                    SprdMain.Col = ColBalScheduleQty
                    SprdMain.Text = System.Math.Round(mScheduleQty - mTotMonthPackQty, 2)
                Else
                    SprdMain.Col = ColBalScheduleQty
                    SprdMain.Text = "0.00"
                End If

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            '        AdataItem.Refresh
            'FormatSprdView()
            'SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsDNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        LblMkey.Text = ""
        txtSONo.Enabled = True
        cboRefType.Enabled = True

        cboDivision.Text = GetDefaultDivision()  ''cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        mCustomerCode = CStr(-1)
        txtDNNo.Text = ""

        txtDNDate.Text = VB6.Format(GetServerDate, "DD/MM/YYYY")
        TxtGRDate.Text = VB6.Format(GetServerDate, "DD/MM/YYYY")


        txtDNDate.Enabled = True  '' IIf(PubSuperUser = "S", True, False)
        TxtCustomerName.Text = ""
        txtBillTo.Text = ""
        TxtShipTo.Text = ""
        chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked

        txtTransportCode.Text = ""

        cboTransmode.SelectedIndex = 0
        cboVehicleType.SelectedIndex = 0


        txtCustomerCode.Text = ""
        txtShipCustomer.Text = ""
        txtLoadingTime.Text = GetServerTime()
        '    txtVehicleNo.Text = ""

        txtSONo.Text = ""
        txtSODate.Text = ""
        txtCustPoNo.Text = ""
        txtCustPODate.Text = ""
        '    txtTransporter.Text = ""
        txtPrepared.Text = UCase(PubUserID)
        txtExportInvoiceNo.Text = ""
        TxtGRNo.Text = ""

        txtSuppFromDate.Text = ""
        txtSuppToDate.Text = ""
        txtAmendNo.Text = ""

        txtSONo.Enabled = True
        txtSODate.Enabled = True
        cmdSearchSo.Enabled = True


        cboRefType.SelectedIndex = 0 ''cboRefType.Text = "Production"
        cboStatus.SelectedIndex = 0
        txtAddress.Text = ""
        TxtCustomerName.Enabled = True
        txtCustomerCode.Enabled = True
        cmdsearch.Enabled = True

        txtBillTo.Enabled = False
        TxtShipTo.Enabled = False
        chkShipTo.Enabled = False
        txtShipCustomer.Enabled = False
        cmdsearchShipTo.Enabled = False
        txtStoreLoc.Text = ""

        chkSaleReturn.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSaleReturn.Enabled = False

        txtStoreLoc.Enabled = True
        cmdGetData.Enabled = True
        txtAmendNo.Enabled = False
        txtSuppFromDate.Enabled = False
        txtSuppToDate.Enabled = False
        cmdPopulateSuppBill.Enabled = False
        cmdShow.Enabled = False

        'If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
        '    chkShipTo.Enabled = True
        '    'txtShipCustomer.Enabled = True
        '    'TxtShipTo.Enabled = True
        'End If

        If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
            chkShipTo.Enabled = True
        End If

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsDNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FrmDespatchNote_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmDespatchNote_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub

    Private Sub FrmDespatchNote_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        pMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245) '8000
        ''Me.Width = VB6.TwipsToPixelsX(11355) '11900



        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.Text = GetDefaultDivision()        '

        'AdataItem.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

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

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        'With SprdMain
        '    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PO " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY AUTO_KEY_PO,AMEND_NO"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mRemarks As String = ""
        Dim mCompanyStateCode As String
        Dim mStateName As String = ""
        Dim mStateCode As String = "N"
        Dim mWithInState As String = "N"
        Dim mWithInCountry As String
        Dim mPlaceofSupply As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, pMenu)

        If lblDespType.Text = "1" Then
            mRemarks = "Customer Order No & Date : "
        ElseIf lblDespType.Text = "2" Then
            mRemarks = "Debit Note No & Date : "
        End If

        mRemarks = mRemarks & Trim(txtCustPoNo.Text) & " & " & VB6.Format(txtCustPODate.Text, "DD/MM/YYYY")

        If lblDespType.Text = "2" Then
            If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mStateName = MasterNo
                mStateCode = GetStateCode(mStateName)
            End If

            If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInState = MasterNo
            End If

            If mWithInState = "N" Then
                If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mWithInCountry = MasterNo
                End If
            End If
            mPlaceofSupply = VB6.Format(mStateCode, "00") & "-" & mStateName '' IIf(mWithInState = "Y", "INTRA STATE", IIf(mWithInCountry = "Y", "INTER STATE", "EXPORT"))

            MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")
            MainClass.AssignCRptFormulas(Report1, "mPlaceofSupply=""" & mPlaceofSupply & """")

            MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")

            mCompanyStateCode = GetStateCode(IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value))
            MainClass.AssignCRptFormulas(Report1, "CompanyStateCode=""" & mCompanyStateCode & """")

            MainClass.AssignCRptFormulas(Report1, "PhoneNo=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", RsCompany.Fields("COMPANY_PHONE").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "Email=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", RsCompany.Fields("COMPANY_MAILID").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "Website=""" & IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", RsCompany.Fields("WEBSITE").Value) & """")

        Else
            MainClass.AssignCRptFormulas(Report1, "CustomerOrderNo=""" & mRemarks & """")
        End If

        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CollectPOData(ByRef mShowDetail As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsPO As ADODB.Recordset = Nothing
        Dim FirstTime As Boolean
        Dim mSprdRowNo As Integer
        Dim mInterUnit As String = "N"

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        FirstTime = True


        MainClass.ClearGrid(SprdMain, ConRowHeight)
        mSprdRowNo = 0

        FormatSprdMain(-1)

        SqlStr = ""



        If VB.Left(cboRefType.Text, 1) = "E" Then
            SqlStr = " SELECT POM.*, " & vbCrLf _
                & " POD.*,  GLASS_AREA CHARGEABLEGLASS_AREA, " & vbCrLf _
                & " AC.SUPP_CUST_NAME as SuppName " & vbCrLf _
                & " FROM DSP_PACKING_HDR POM,DSP_PACKING_DET POD,FIN_SUPP_CUST_MST AC " & vbCrLf _
                & " WHERE POM.AUTO_KEY_PACK = POD.AUTO_KEY_PACK " & vbCrLf _
                & " AND POM.Company_Code = AC.Company_Code " & vbCrLf _
                & " AND POM.BUYER_CODE = AC.SUPP_CUST_CODE " & vbCrLf _
                & " AND POM.AUTO_KEY_PACK=" & Val(txtSONo.Text) & " "

            If mCustomerCode <> "-1" Then
                SqlStr = SqlStr & vbCrLf & " AND POM.BUYER_CODE='" & mCustomerCode & "' " ''POM.SUPP_CUST_CODE
            End If

            SqlStr = SqlStr & vbCrLf & " AND POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND POM.EXP_INV_MADE='Y' AND POM.DC_MADE='N' AND EXCISE_INV_MADE='N' " & vbCrLf _
                & " ORDER BY POD.SERIAL_NO"
        ElseIf (VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L") Then

            If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                SqlStr = " SELECT POM.*, " & vbCrLf _
                        & " POD.SERIAL_NO SUBROWNO, POD.ITEM_CODE, INVMST.ITEM_SHORT_DESC ITEM_DESC,  " & vbCrLf _
                        & " POD.ITEM_UOM, POD.ITEM_PRICE ITEM_RATE, " & vbCrLf _
                        & " ITEM_QTY, " & vbCrLf _
                        & " AC.SUPP_CUST_NAME as SuppName, 0 ACTUAL_HEIGHT, 0 ACTUAL_WIDTH, 0 GLASS_AREA,0 As CHARGEABLE_HEIGHT, 0 As CHARGEABLE_WIDTH, 0 CHARGEABLEGLASS_AREA , '' GLASS_DESC" & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR POM, PUR_PURCHASE_DET POD, FIN_SUPP_CUST_MST AC,INV_ITEM_MST INVMST " & vbCrLf _
                        & " WHERE POM.MKEY = POD.MKEY " & vbCrLf _
                        & " AND POM.Company_Code = AC.Company_Code " & vbCrLf _
                        & " AND POM.SUPP_CUST_CODE = AC.SUPP_CUST_CODE " & vbCrLf _
                        & " AND POD.Company_Code=INVMST.COMPANY_CODE AND POD.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                        & " AND POM.AUTO_KEY_PO='" & txtSONo.Text & "' AND POM.AMEND_NO='" & txtAmendNo.Text & "' AND PUR_TYPE='P' AND PO_STATUS ='Y' AND PO_CLOSED='N'"

                If mCustomerCode <> "-1" Then
                    SqlStr = SqlStr & vbCrLf & " AND POM.SUPP_CUST_CODE='" & mCustomerCode & "' "
                End If

                SqlStr = SqlStr & vbCrLf & " AND POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

                SqlStr = SqlStr & vbCrLf & " ORDER BY POD.SERIAL_NO"
            Else
                SqlStr = " SELECT POM.*, " & vbCrLf _
                        & " POD.SUBROWNO, POD.ITEM_CODE, POD.ITEM_DESC,  " & vbCrLf _
                        & " POD.ITEM_UOM, POD.ITEM_RATE, POD.ITEM_AMT, POD.ITEM_ED, " & vbCrLf _
                        & " POD.ITEM_ST, POD.MRR_REF_NO, POD.MRR_REF_DATE, POD.SUPP_REF_NO," & vbCrLf _
                        & " POD.SUPP_REF_DATE, POD.REF_PO_NO, POD.PURMKEY, POD.PURVNO," & vbCrLf _
                        & " POD.PURVDATE, POD.DNCN_REF_NO, POD.DNCN_REF_DATE, POD.PO_RATE," & vbCrLf _
                        & " POD.MRR_REF_TYPE, " & vbCrLf _
                        & " (DECODE(INVMST.ISSUE_UOM,POD.ITEM_UOM,1,INVMST.UOM_FACTOR) * POD.ITEM_QTY) " & vbCrLf _
                        & " - GETREJDESPATCHQTY (POM.COMPANY_CODE, POM.MKEY,POM.DEBITACCOUNTCODE,POD.MRR_REF_NO,POD.ITEM_CODE) " & vbCrLf _
                        & " - GETREJCREDITQTY (POM.COMPANY_CODE, POM.DEBITACCOUNTCODE,POD.MRR_REF_NO,POD.ITEM_CODE,DECODE(INVMST.ISSUE_UOM,POD.ITEM_UOM,1,INVMST.UOM_FACTOR)) ITEM_QTY, " & vbCrLf _
                        & " AC.SUPP_CUST_NAME as SuppName, 0 ACTUAL_HEIGHT, 0 ACTUAL_WIDTH, 0 GLASS_AREA,0 As CHARGEABLE_HEIGHT, 0 As CHARGEABLE_WIDTH, 0 CHARGEABLEGLASS_AREA , '' GLASS_DESC" & vbCrLf _
                        & " FROM FIN_DNCN_HDR POM, FIN_DNCN_DET POD, FIN_SUPP_CUST_MST AC,INV_ITEM_MST INVMST " & vbCrLf _
                        & " WHERE POM.MKEY = POD.MKEY " & vbCrLf _
                        & " AND POM.Company_Code = AC.Company_Code " & vbCrLf _
                        & " AND POM.DEBITACCOUNTCODE = AC.SUPP_CUST_CODE " & vbCrLf _
                        & " AND POD.Company_Code=INVMST.COMPANY_CODE AND POD.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                        & " AND POM.MKEY='" & txtSONo.Text & "' "

                If VB.Left(cboRefType.Text, 1) = "Q" Then
                    SqlStr = SqlStr & vbCrLf & " AND POM.DNCNFROM='M'"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND POM.DNCNFROM='S'"
                End If

                If mCustomerCode <> "-1" Then
                    SqlStr = SqlStr & vbCrLf & " AND POM.DEBITACCOUNTCODE='" & mCustomerCode & "' "
                End If

                SqlStr = SqlStr & vbCrLf & " AND POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

                '' & " AND (SALEINVOICENO IS NULL OR SALEINVOICENO='') " & vbCrLf _
                '
                SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N' AND APPROVED='Y' " & vbCrLf & " AND BOOKCODE=" & ConDebitNoteBookCode & " AND VTYPE='DR' AND POM.DNCNTYPE='R'"

                SqlStr = SqlStr & vbCrLf & " AND (DECODE(INVMST.ISSUE_UOM,POD.ITEM_UOM,1,INVMST.UOM_FACTOR) * POD.ITEM_QTY)> " & vbCrLf & " GETREJDESPATCHQTY (POM.COMPANY_CODE, POM.MKEY,POM.DEBITACCOUNTCODE,POD.MRR_REF_NO,POD.ITEM_CODE) " & vbCrLf & " + GETREJCREDITQTY (POM.COMPANY_CODE, POM.DEBITACCOUNTCODE,POD.MRR_REF_NO,POD.ITEM_CODE,DECODE(INVMST.ISSUE_UOM,POD.ITEM_UOM,1,INVMST.UOM_FACTOR)) "

                SqlStr = SqlStr & vbCrLf & " ORDER BY POD.SUBROWNO"
            End If


        ElseIf VB.Left(cboRefType.Text, 1) = "U" Then  'Left(cboRefType.Text, 1) = "S" Then 22-09-2014
            SqlStr = " SELECT POM.*, " & vbCrLf _
                & " POD.SERIAL_NO, POD.SUPP_CUST_CODE, POD.ITEM_CODE, POD.UOM_CODE, POD.PART_NO,  " & vbCrLf _
                & " POD.ITEM_PRICE, POD.PACK_TYPE, POD.COLOUR_DTL, AC.SUPP_CUST_NAME as SuppName, POD.ACTUAL_HEIGHT, POD.ACTUAL_WIDTH, POD.GLASS_AREA,CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH,CHARGEABLEGLASS_AREA , GLASS_DESC" & vbCrLf _
                & " FROM DSP_SALEORDER_HDR POM,DSP_SALEORDER_DET POD,FIN_SUPP_CUST_MST AC " & vbCrLf _
                & " WHERE POM.MKEY = POD.MKEY " & vbCrLf _
                & " AND POM.Company_Code = AC.Company_Code " & vbCrLf _
                & " AND POM.SUPP_CUST_CODE = AC.SUPP_CUST_CODE " & vbCrLf _
                & " AND POM.AUTO_KEY_SO=" & Val(txtSONo.Text) & " AND SO_APPROVED='Y'"

            If mCustomerCode <> "-1" Then
                SqlStr = SqlStr & vbCrLf & " AND POM.SUPP_CUST_CODE='" & mCustomerCode & "' "
            End If

            'If Trim(txtStoreLoc.Text) = "" Then
            '    SqlStr = SqlStr & vbCrLf & " AND (POD.CUST_STORE_LOC='' OR POD.CUST_STORE_LOC IS NULL)"
            'Else
            '    SqlStr = SqlStr & vbCrLf & " AND POD.CUST_STORE_LOC='" & Trim(txtStoreLoc.Text) & "' "
            'End If

            SqlStr = SqlStr & vbCrLf & " AND POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND POM.AMEND_NO =" & Val(txtAmendNo.Text) & ""

            SqlStr = SqlStr & vbCrLf & " ORDER BY POD.SERIAL_NO"

        Else
            SqlStr = " SELECT POM.*, " & vbCrLf _
                & " POD.SERIAL_NO, POD.SUPP_CUST_CODE, POD.ITEM_CODE, POD.UOM_CODE, POD.PART_NO,  " & vbCrLf _
                & " POD.ITEM_PRICE, POD.PACK_TYPE, POD.COLOUR_DTL, AC.SUPP_CUST_NAME as SuppName, POD.ACTUAL_HEIGHT, POD.ACTUAL_WIDTH, POD.GLASS_AREA,CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH,CHARGEABLEGLASS_AREA,POD.ITEM_MODEL, ITEM_DRAWINGNO, GLASS_DESC " & vbCrLf _
                & " FROM DSP_SALEORDER_HDR POM,DSP_SALEORDER_DET POD,FIN_SUPP_CUST_MST AC " & vbCrLf _
                & " WHERE POM.MKEY = POD.MKEY " & vbCrLf _
                & " AND POM.Company_Code = AC.Company_Code " & vbCrLf _
                & " AND POM.SUPP_CUST_CODE = AC.SUPP_CUST_CODE " & vbCrLf _
                & " AND POM.AUTO_KEY_SO=" & Val(txtSONo.Text) & " AND POM.SO_STATUS='O' AND SO_APPROVED='Y' AND POD.SO_ITEM_STATUS = 'N'"




            If mCustomerCode <> "-1" Then
                SqlStr = SqlStr & vbCrLf & " AND POM.SUPP_CUST_CODE='" & mCustomerCode & "' "
            End If

            'If Trim(txtStoreLoc.Text) = "" Then
            '    SqlStr = SqlStr & vbCrLf & " AND (POD.CUST_STORE_LOC='' OR POD.CUST_STORE_LOC IS NULL)"
            'Else
            '    SqlStr = SqlStr & vbCrLf & " AND POD.CUST_STORE_LOC='" & Trim(txtStoreLoc.Text) & "' "
            'End If

            '        SqlStr = SqlStr & vbCrLf & " AND POD.ITEM_CODE='F0008'"

            SqlStr = SqlStr & vbCrLf & " AND POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND POM.SO_STATUS='O' " & vbCrLf & " ORDER BY POD.SERIAL_NO"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPO, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsPO.EOF Then
            If FirstTime = True Then
                If FillPOMainPart(RsPO) = True Then FirstTime = False
            End If
            If mShowDetail = True Then
                If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "S" Then
                    If MsgQuestion("Populate Data From Customer Sales Order ...") = CStr(MsgBoxResult.No) Then
                        Exit Sub
                    End If
                End If
                FillPODetailPart(RsPO, (txtSONo.Text), mSprdRowNo)
            End If
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)

    End Sub
    Private Function GetSOLocation(pPONo As Double) As String

        On Error GoTo ErrPart
        Dim mCustomerCode As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        GetSOLocation = ""

        If MainClass.ValidateWithMasterTable((txtCustomerCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        SqlStr = "SELECT BILL_TO_LOC_ID" & vbCrLf _
            & " FROM  DSP_SALEORDER_HDR IH" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & Val(pPONo) & " AND SO_APPROVED='Y'" & vbCrLf & " AND IH.MKEY = ("

        SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH" & vbCrLf _
            & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SIH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _
            & " AND SIH.AUTO_KEY_SO=" & Val(pPONo) & " AND SO_APPROVED='Y'" & vbCrLf _
            & " AND SIH.AMEND_WEF_FROM <=TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSOLocation = Trim(IIf(IsDBNull(RsTemp.Fields("BILL_TO_LOC_ID").Value), "", RsTemp.Fields("BILL_TO_LOC_ID").Value))
        End If

        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Function
    Private Function FillPOMainPart(ByRef RsPO As ADODB.Recordset) As Boolean
        On Error GoTo ErrPart
        Dim mConsigneeCode As String = ""
        Dim mShippedToCode As String = ""
        Dim mBillToSameShipToCode As String = ""

        TxtCustomerName.Text = IIf(IsDBNull(RsPO.Fields("SuppName").Value), "", RsPO.Fields("SuppName").Value)
        Dim mInterUnit As String = "N"

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
            If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                txtCustomerCode.Text = IIf(IsDBNull(RsPO.Fields("SUPP_CUST_CODE").Value), "", RsPO.Fields("SUPP_CUST_CODE").Value)
            Else
                txtCustomerCode.Text = IIf(IsDBNull(RsPO.Fields("DEBITACCOUNTCODE").Value), "", RsPO.Fields("DEBITACCOUNTCODE").Value)
            End If

        Else
            If VB.Left(cboRefType.Text, 1) = "E" Then
                txtCustomerCode.Text = IIf(IsDBNull(RsPO.Fields("BUYER_CODE").Value), "", RsPO.Fields("BUYER_CODE").Value)
                mConsigneeCode = IIf(IsDBNull(RsPO.Fields("SUPP_CUST_CODE").Value), "", RsPO.Fields("SUPP_CUST_CODE").Value)
            Else
                txtCustomerCode.Text = IIf(IsDBNull(RsPO.Fields("SUPP_CUST_CODE").Value), "", RsPO.Fields("SUPP_CUST_CODE").Value)
            End If
        End If

        If VB.Left(cboRefType.Text, 1) = "E" Then

            txtBillTo.Text = IIf(IsDBNull(RsPO.Fields("BILL_TO_LOC_ID").Value), "", RsPO.Fields("BILL_TO_LOC_ID").Value)

            If MainClass.ValidateWithMasterTable(mConsigneeCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtShipCustomer.Text = MasterNo
                TxtShipTo.Text = IIf(IsDBNull(RsPO.Fields("SHIP_TO_LOC_ID").Value), "", RsPO.Fields("SHIP_TO_LOC_ID").Value)
            Else
                txtShipCustomer.Text = ""
            End If

            If IIf(IsDBNull(RsPO.Fields("BUYER_CODE").Value), "", RsPO.Fields("BUYER_CODE").Value) = IIf(IsDBNull(RsPO.Fields("SUPP_CUST_CODE").Value), "", RsPO.Fields("SUPP_CUST_CODE").Value) Then
                If txtBillTo.Text = TxtShipTo.Text Then
                    chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
                Else
                    chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked
                End If

            Else
                chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If

            txtAddress.Text = GetPartyBusinessDetail(Trim(mConsigneeCode), Trim(TxtShipTo.Text), "SUPP_CUST_ADDR || ', ' || SUPP_CUST_CITY || ', ' || SUPP_CUST_STATE")

            txtSONo.Text = IIf(IsDBNull(RsPO.Fields("AUTO_KEY_PACK").Value), "", RsPO.Fields("AUTO_KEY_PACK").Value)
            txtSODate.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("PACK_DATE").Value), "", RsPO.Fields("PACK_DATE").Value), "DD/MM/YYYY")
            txtCustPoNo.Text = IIf(IsDBNull(RsPO.Fields("BUYER_PO").Value), "", RsPO.Fields("BUYER_PO").Value)
            txtCustPODate.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("BUYER_PO_DATE").Value), "", RsPO.Fields("BUYER_PO_DATE").Value), "DD/MM/YYYY")


        ElseIf VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
            If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                txtSONo.Text = IIf(IsDBNull(RsPO.Fields("AUTO_KEY_PO").Value), "", RsPO.Fields("AUTO_KEY_PO").Value)
                txtSODate.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("PUR_ORD_DATE").Value), "", RsPO.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY")
                txtCustPoNo.Text = IIf(IsDBNull(RsPO.Fields("AUTO_KEY_PO").Value), "", RsPO.Fields("AUTO_KEY_PO").Value) ''IIf(IsDBNull(RsPO.Fields("CUST_PO_NO").Value), "", RsPO.Fields("CUST_PO_NO").Value)
                txtCustPODate.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("PUR_ORD_DATE").Value), "", RsPO.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY") ''VB6.Format(IIf(IsDBNull(RsPO.Fields("CUST_PO_DATE").Value), "", RsPO.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")
            Else
                txtSONo.Text = IIf(IsDBNull(RsPO.Fields("mKey").Value), "", RsPO.Fields("mKey").Value)
                txtSODate.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("VDate").Value), "", RsPO.Fields("VDate").Value), "DD/MM/YYYY")
                txtCustPoNo.Text = IIf(IsDBNull(RsPO.Fields("VNO").Value), "", RsPO.Fields("VNO").Value)
                txtCustPODate.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("VDate").Value), "", RsPO.Fields("VDate").Value), "DD/MM/YYYY")
            End If

        Else
            txtSONo.Text = IIf(IsDBNull(RsPO.Fields("AUTO_KEY_SO").Value), "", RsPO.Fields("AUTO_KEY_SO").Value)
            txtSODate.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("SO_DATE").Value), "", RsPO.Fields("SO_DATE").Value), "DD/MM/YYYY")
            txtCustPoNo.Text = IIf(IsDBNull(RsPO.Fields("CUST_PO_NO").Value), "", RsPO.Fields("CUST_PO_NO").Value)
            txtCustPODate.Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("CUST_PO_DATE").Value), "", RsPO.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")


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
                txtShipCustomer.Text = MasterNo
            Else
                txtShipCustomer.Text = ""
            End If

            txtAddress.Text = GetPartyBusinessDetail(Trim(mShippedToCode), Trim(TxtShipTo.Text), "SUPP_CUST_ADDR || ', ' || SUPP_CUST_CITY || ', ' || SUPP_CUST_STATE")
        End If

        TxtCustomerName.Enabled = False
        txtCustomerCode.Enabled = False
        cmdsearch.Enabled = False
        FillPOMainPart = True
        Exit Function
ErrPart:
        FillPOMainPart = False
        MsgBox(Err.Description)
    End Function

    Private Sub FillPODetailPart(ByRef RsPO As ADODB.Recordset, ByRef mtxtSONo As String, ByRef SprdRowNo As Integer)

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
        Dim mSaleOrderType As String
        Dim mBalQty As Double = 0
        Dim mWidth As Double
        Dim mHeight As Double
        Dim mModelNo As String
        Dim mDrawingNo As String
        Dim mInterUnit As String = "N"
        Dim xStoreLoc As String

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        mFactor = 1
        If RsPO.EOF Then Exit Sub
        RsPO.MoveFirst()

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If VB.Left(cboRefType.Text, 1) = "U" Then
            Do While RsPO.EOF = False
                pAutoSONO = IIf(IsDBNull(RsPO.Fields("AUTO_KEY_SO").Value), -1, RsPO.Fields("AUTO_KEY_SO").Value)
                pAutoSOAmendNo = IIf(IsDBNull(RsPO.Fields("AMEND_NO").Value), -1, RsPO.Fields("AMEND_NO").Value)
                pAutoSOAmendNo = pAutoSOAmendNo - 1
                pItemCode = Trim(IIf(IsDBNull(RsPO.Fields("ITEM_CODE").Value), "", RsPO.Fields("ITEM_CODE").Value))

                pNewPrice = IIf(IsDBNull(RsPO.Fields("ITEM_PRICE").Value), 0, RsPO.Fields("ITEM_PRICE").Value)
                pCustomerCode = IIf(IsDBNull(RsPO.Fields("SUPP_CUST_CODE").Value), "-1", RsPO.Fields("SUPP_CUST_CODE").Value)
                pWEFDate = IIf(IsDBNull(RsPO.Fields("AMEND_WEF_FROM").Value), "", RsPO.Fields("AMEND_WEF_FROM").Value)
                pOldPrice = GetSaleOldPrice(pAutoSONO, pAutoSOAmendNo, pCustomerCode, pItemCode)


                ''& " AND AGTD3='N'" & vbCrLf  ''25-10-2010

                '            If pOldPrice < pNewPrice Then'''   '', '' , '' 
                mSqlStr = "SELECT IH.INVOICE_DATE, IH.AUTO_KEY_INVOICE, ID.ITEM_CODE, ID.ITEM_UOM AS UOM_CODE,ACTUAL_HEIGHT,ACTUAL_WIDTH,CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH, CHARGEABLEGLASS_AREA,GLASS_DESC," & vbCrLf _
                    & " SUM(ID.ITEM_QTY) AS ITEM_QTY" & vbCrLf _
                    & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.MKEY=ID.MKEY " & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'" & vbCrLf _
                    & " AND IH.REF_DESP_TYPE<>'U' AND IH.CANCELLED='N'" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                    & " AND ID.ITEM_RATE -GETSALEDEBITRATE(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.FYEAR,IH.MKEY, '" & Trim(pCustomerCode) & "', ID.ITEM_CODE) + GETSALESUPPBILLPRICE(" & RsCompany.Fields("COMPANY_CODE").Value & ", ID.ITEM_CODE, '" & Trim(pCustomerCode) & "',IH.AUTO_KEY_INVOICE) <" & pNewPrice & ""

                ''- GETSALESUPPBILLQTY (" & RsCompany.fields("COMPANY_CODE").value & ", ID.ITEM_CODE, '" & Trim(pCustomerCode) & "',IH.AUTO_KEY_INVOICE)
                ''AND IH.OUR_AUTO_KEY_SO=" & pAutoSONo & "

                If chkSaleReturn.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    mSqlStr = mSqlStr & vbCrLf & " AND AGTD3='N'"
                End If

                mSqlStr = mSqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtSuppFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                mSqlStr = mSqlStr & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtSuppToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                mSqlStr = mSqlStr & vbCrLf & " GROUP BY IH.INVOICE_DATE, IH.AUTO_KEY_INVOICE, ID.ITEM_CODE, ID.ITEM_UOM,ACTUAL_HEIGHT,ACTUAL_WIDTH,CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH, CHARGEABLEGLASS_AREA,GLASS_DESC"
                '                mSqlStr = mSqlStr & vbCrLf & " HAVING SUM(ID.ITEM_QTY)- GETSALESUPPBILLQTY (" & RsCompany.fields("COMPANY_CODE").value & ", ID.ITEM_CODE, '" & Trim(pCustomerCode) & "',IH.AUTO_KEY_INVOICE)>0 "
                mSqlStr = mSqlStr & vbCrLf & " ORDER BY IH.INVOICE_DATE, IH.AUTO_KEY_INVOICE"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPO, ADODB.LockTypeEnum.adLockReadOnly)
                If RsSuppPO.EOF = False Then
                    With SprdMain
                        Do While RsSuppPO.EOF = False
                            mItemCode = Trim(IIf(IsDBNull(RsSuppPO.Fields("ITEM_CODE").Value), "", RsSuppPO.Fields("ITEM_CODE").Value))
                            mAutoKeyInvoice = IIf(IsDBNull(RsSuppPO.Fields("AUTO_KEY_INVOICE").Value), -1, RsSuppPO.Fields("AUTO_KEY_INVOICE").Value)

                            pQty = CDbl(VB6.Format(IIf(IsDBNull(RsSuppPO.Fields("ITEM_QTY").Value), 0, RsSuppPO.Fields("ITEM_QTY").Value) * mFactor, "0.000"))
                            If chkSaleReturn.CheckState = System.Windows.Forms.CheckState.Checked Then
                                pQty = pQty - GetSaleReturn(Str(RsSuppPO.Fields("AUTO_KEY_INVOICE").Value), pCustomerCode, pItemCode)
                            End If

                            mSqlStr = "SELECT GETSALESHORTAGEQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",IH.FYEAR,IH.MKEY, '" & Trim(pCustomerCode) & "', ID.ITEM_CODE) AS SHORTAGEQTY " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'" & vbCrLf & " AND IH.REF_DESP_TYPE<>'U' AND IH.CANCELLED='N'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND IH.AUTO_KEY_INVOICE = " & mAutoKeyInvoice & ""

                            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRate, ADODB.LockTypeEnum.adLockReadOnly)

                            If RsRate.EOF = False Then
                                pQty = pQty - IIf(IsDBNull(RsRate.Fields("SHORTAGEQTY").Value), 0, RsRate.Fields("SHORTAGEQTY").Value)
                            End If

                            If pQty > 0 Then
                                SprdRowNo = SprdRowNo + 1
                                .MaxRows = SprdRowNo + 1
                                'FormatSprdMain -1
                                .Row = SprdRowNo

                                .Col = ColItemCode
                                .Text = mItemCode

                                .Col = ColItemDesc
                                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                                mItemDesc = MasterNo
                                .Text = mItemDesc
                                mItemDesc = ""

                                .Col = ColPartNo
                                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_ITEM_NO", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'") = True Then
                                    mItemDesc = MasterNo
                                Else
                                    mItemDesc = ""
                                End If

                                .Text = mItemDesc

                                .Col = ColUnit
                                ''15-02-2006  'sk

                                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "ISSUE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                    mUOM = MasterNo
                                Else
                                    mUOM = ""
                                End If

                                .Text = mUOM

                                If Trim(UCase(mUOM)) <> Trim(UCase(IIf(IsDBNull(RsSuppPO.Fields("UOM_CODE").Value), "", RsSuppPO.Fields("UOM_CODE").Value))) Then
                                    If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "UOM_FACTOR", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                        mFactor = MasterNo
                                    Else
                                        mFactor = 1
                                    End If
                                End If

                                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                                    SprdMain.Col = ColActualHeight
                                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsSuppPO.Fields("ACTUAL_HEIGHT").Value), 0, RsSuppPO.Fields("ACTUAL_HEIGHT").Value)))

                                    SprdMain.Col = ColActualWidth
                                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsSuppPO.Fields("ACTUAL_WIDTH").Value), 0, RsSuppPO.Fields("ACTUAL_WIDTH").Value)))

                                    SprdMain.Col = ColChargeableHeight
                                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsSuppPO.Fields("CHARGEABLE_HEIGHT").Value), 0, RsSuppPO.Fields("CHARGEABLE_HEIGHT").Value)))

                                    SprdMain.Col = ColChargeableWidth
                                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsSuppPO.Fields("CHARGEABLE_WIDTH").Value), 0, RsSuppPO.Fields("CHARGEABLE_WIDTH").Value)))

                                    SprdMain.Col = ColChargeableArea
                                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsSuppPO.Fields("CHARGEABLEGLASS_AREA").Value), 0, RsSuppPO.Fields("CHARGEABLEGLASS_AREA").Value)))
                                End If
                                .Col = ColRefNo
                                .Text = Trim(Str(IIf(IsDBNull(RsSuppPO.Fields("AUTO_KEY_INVOICE").Value), "", RsSuppPO.Fields("AUTO_KEY_INVOICE").Value)))


                                .Col = ColStockType
                                .Text = GetStockType(PubDBCn, mItemCode, mDivisionCode)           ''"FG"
                                mStockType = .Text

                                '                        .Col = ColStockQty
                                '                        .Text = GetBalanceStockQty(mItemCode, txtDNDate.Text, mUOM, "PAD", mStockType, "", ConWH, "DSP", Val(txtDNNo.Text))


                                .Col = ColPackQty
                                .Text = VB6.Format(pQty, "0.000")
                            End If

                            RsSuppPO.MoveNext()
                            If RsSuppPO.EOF = False Then
                                If pQty > 0 Then
                                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                                End If
                                '                FormatSprdMain .MaxRows
                            End If
                        Loop
                    End With
                End If
                '            End If
NextRecd:
                RsPO.MoveNext()
            Loop
        Else
            With SprdMain
                Do While RsPO.EOF = False

                    If VB.Left(cboRefType.Text, 1) = "E" Then
                        mSoNo = CDbl(Trim(Str(IIf(IsDBNull(RsPO.Fields("AUTO_KEY_SO").Value), 0, RsPO.Fields("AUTO_KEY_SO").Value))))
                        If Val(CStr(mSoNo)) = 0 Then
                            mSoNo = CDbl("-1") ''Trim(Str(IIf(IsNull(RsPO!AUTO_KEY_PACK), "", RsPO!AUTO_KEY_PACK)))
                        End If

                        If Trim(txtBillTo.Text) = "" Then
                            txtBillTo.Text = GetSOLocation(mSoNo)
                        End If

                        If chkShipTo.Checked = True Then
                            If Trim(txtShipCustomer.Text) = "" Then
                                txtShipCustomer.Text = TxtCustomerName.Text
                            End If
                            If Trim(TxtShipTo.Text) = "" Then
                                TxtShipTo.Text = txtBillTo.Text
                            End If
                        End If
                    ElseIf VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
                        If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                            mSoNo = CDbl(Trim(Str(IIf(IsDBNull(RsPO.Fields("AUTO_KEY_PO").Value), 0, RsPO.Fields("AUTO_KEY_PO").Value))))
                        Else
                            mSoNo = CDbl(Trim(Str(IIf(IsDBNull(RsPO.Fields("mKey").Value), 0, RsPO.Fields("mKey").Value))))
                        End If

                    Else
                        mSoNo = CDbl(Trim(Str(IIf(IsDBNull(RsPO.Fields("AUTO_KEY_SO").Value), 0, RsPO.Fields("AUTO_KEY_SO").Value))))
                        If MainClass.ValidateWithMasterTable(Val(mSoNo), "AUTO_KEY_SO", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "' AND SO_APPROVED='Y'") = True Then
                            mDIRequired = MasterNo
                        Else
                            mDIRequired = "N"
                        End If
                    End If

                    mItemCode = Trim(IIf(IsDBNull(RsPO.Fields("ITEM_CODE").Value), "", RsPO.Fields("ITEM_CODE").Value))




                    If VB.Left(cboRefType.Text, 1) = "E" Then

                    ElseIf VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
                        If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                            xMRRNo = -1
                        Else
                            xMRRNo = Trim(Str(IIf(IsDBNull(RsPO.Fields("MRR_REF_NO").Value), "", RsPO.Fields("MRR_REF_NO").Value)))
                        End If

                        If CheckDuplicateRow(mSoNo, mItemCode & xMRRNo, mDIRequired) = True Then GoTo NexrRec
                    Else
                        If CheckDuplicateRow(mSoNo, mItemCode, mDIRequired) = True Then GoTo NexrRec
                    End If

                    SprdRowNo = SprdRowNo + 1
                    .MaxRows = SprdRowNo + 1
                    '            FormatSprdMain -1
                    .Row = SprdRowNo

                    If VB.Left(cboRefType.Text, 1) = "E" Then
                        If Val(CStr(Val(IIf(IsDBNull(RsPO.Fields("AUTO_KEY_SO").Value), 0, RsPO.Fields("AUTO_KEY_SO").Value)))) = 0 Then
                            .Col = ColSONo
                            .Text = "-1" '' Trim(Str(IIf(IsNull(RsPO!AUTO_KEY_PACK), "", RsPO!AUTO_KEY_PACK)))

                            .Col = ColSODate
                            .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("PACK_DATE").Value), "", RsPO.Fields("PACK_DATE").Value), "DD/MM/YYYY")

                            .Col = ColCustomerNo
                            .Text = IIf(IsDBNull(RsPO.Fields("BUYER_PO").Value), "", RsPO.Fields("BUYER_PO").Value)

                            .Col = ColCustomerDate
                            .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("BUYER_PO_DATE").Value), "", RsPO.Fields("BUYER_PO_DATE").Value), "DD/MM/YYYY")

                            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                                .Col = ColGlassDescription
                                .Text = Trim(IIf(IsDBNull(RsPO.Fields("GLASS_DESC").Value), "", RsPO.Fields("GLASS_DESC").Value))

                                SprdMain.Col = ColModel
                                SprdMain.Text = IIf(IsDBNull(RsPO.Fields("ITEM_MODEL").Value), "", RsPO.Fields("ITEM_MODEL").Value)

                                SprdMain.Col = ColDrawingNo
                                SprdMain.Text = IIf(IsDBNull(RsPO.Fields("ITEM_DRAWINGNO").Value), "", RsPO.Fields("ITEM_DRAWINGNO").Value)

                                SprdMain.Col = ColActualHeight
                                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ACTUAL_HEIGHT").Value), 0, RsPO.Fields("ACTUAL_HEIGHT").Value)))

                                SprdMain.Col = ColActualWidth
                                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ACTUAL_WIDTH").Value), 0, RsPO.Fields("ACTUAL_WIDTH").Value)))


                                SprdMain.Col = ColChargeableHeight
                                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("CHARGEABLE_HEIGHT").Value), 0, RsPO.Fields("CHARGEABLE_HEIGHT").Value)))

                                SprdMain.Col = ColChargeableWidth
                                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("CHARGEABLE_WIDTH").Value), 0, RsPO.Fields("CHARGEABLE_WIDTH").Value)))

                                SprdMain.Col = ColChargeableArea
                                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("CHARGEABLEGLASS_AREA").Value), 0, RsPO.Fields("CHARGEABLEGLASS_AREA").Value)))
                            End If

                        Else

                            .Col = ColSONo
                            .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("AUTO_KEY_SO").Value), 0, RsPO.Fields("AUTO_KEY_SO").Value)))

                            .Col = ColSODate
                            .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("SO_DATE").Value), "", RsPO.Fields("SO_DATE").Value), "DD/MM/YYYY")

                            .Col = ColCustomerNo
                            .Text = IIf(IsDBNull(RsPO.Fields("CUST_PO_NO").Value), "", RsPO.Fields("CUST_PO_NO").Value)

                            .Col = ColCustomerDate
                            .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("CUST_PO_DATE").Value), "", RsPO.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                                .Col = ColGlassDescription
                                .Text = Trim(IIf(IsDBNull(RsPO.Fields("GLASS_DESC").Value), "", RsPO.Fields("GLASS_DESC").Value))

                                SprdMain.Col = ColModel
                                SprdMain.Text = IIf(IsDBNull(RsPO.Fields("ITEM_MODEL").Value), "", RsPO.Fields("ITEM_MODEL").Value)

                                SprdMain.Col = ColDrawingNo
                                SprdMain.Text = IIf(IsDBNull(RsPO.Fields("ITEM_DRAWINGNO").Value), "", RsPO.Fields("ITEM_DRAWINGNO").Value)

                                SprdMain.Col = ColActualHeight
                                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ACTUAL_HEIGHT").Value), 0, RsPO.Fields("ACTUAL_HEIGHT").Value)))

                                SprdMain.Col = ColActualWidth
                                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ACTUAL_WIDTH").Value), 0, RsPO.Fields("ACTUAL_WIDTH").Value)))


                                SprdMain.Col = ColChargeableHeight
                                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("CHARGEABLE_HEIGHT").Value), 0, RsPO.Fields("CHARGEABLE_HEIGHT").Value)))

                                SprdMain.Col = ColChargeableWidth
                                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("CHARGEABLE_WIDTH").Value), 0, RsPO.Fields("CHARGEABLE_WIDTH").Value)))

                                SprdMain.Col = ColChargeableArea
                                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("CHARGEABLEGLASS_AREA").Value), 0, RsPO.Fields("CHARGEABLEGLASS_AREA").Value)))
                            End If
                        End If

                    ElseIf VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
                        If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                            .Col = ColSONo
                            .Text = Trim(Str(IIf(IsDBNull(RsPO.Fields("AUTO_KEY_PO").Value), "", RsPO.Fields("AUTO_KEY_PO").Value)))

                            .Col = ColSODate
                            .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("PUR_ORD_DATE").Value), "", RsPO.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY")

                            .Col = ColCustomerNo
                            .Text = "" 'Trim(IIf(IsDBNull(RsPO.Fields("CUST_PO_NO").Value), "", RsPO.Fields("CUST_PO_NO").Value))

                            .Col = ColCustomerDate
                            .Text = "" ' VB6.Format(IIf(IsDBNull(RsPO.Fields("CUST_PO_DATE").Value), "", RsPO.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")
                        Else
                            .Col = ColSONo
                            .Text = Trim(Str(IIf(IsDBNull(RsPO.Fields("mKey").Value), "", RsPO.Fields("mKey").Value)))

                            .Col = ColSODate
                            .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("VDate").Value), "", RsPO.Fields("VDate").Value), "DD/MM/YYYY")

                            .Col = ColCustomerNo
                            .Text = IIf(IsDBNull(RsPO.Fields("VNO").Value), "", RsPO.Fields("VNO").Value)

                            .Col = ColCustomerDate
                            .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("VDate").Value), "", RsPO.Fields("VDate").Value), "DD/MM/YYYY")
                        End If

                    Else
                        .Col = ColSONo
                        .Text = Trim(Str(IIf(IsDBNull(RsPO.Fields("AUTO_KEY_SO").Value), "", RsPO.Fields("AUTO_KEY_SO").Value)))

                        .Col = ColSODate
                        .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("SO_DATE").Value), "", RsPO.Fields("SO_DATE").Value), "DD/MM/YYYY")

                        .Col = ColCustomerNo
                        .Text = Trim(IIf(IsDBNull(RsPO.Fields("CUST_PO_NO").Value), "", RsPO.Fields("CUST_PO_NO").Value))

                        .Col = ColCustomerDate
                        .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("CUST_PO_DATE").Value), "", RsPO.Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                            .Col = ColGlassDescription
                            .Text = Trim(IIf(IsDBNull(RsPO.Fields("GLASS_DESC").Value), "", RsPO.Fields("GLASS_DESC").Value))

                            SprdMain.Col = ColModel
                            SprdMain.Text = IIf(IsDBNull(RsPO.Fields("ITEM_MODEL").Value), "", RsPO.Fields("ITEM_MODEL").Value)

                            SprdMain.Col = ColDrawingNo
                            SprdMain.Text = IIf(IsDBNull(RsPO.Fields("ITEM_DRAWINGNO").Value), "", RsPO.Fields("ITEM_DRAWINGNO").Value)

                            SprdMain.Col = ColActualHeight
                            SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ACTUAL_HEIGHT").Value), 0, RsPO.Fields("ACTUAL_HEIGHT").Value)))

                            SprdMain.Col = ColActualWidth
                            SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ACTUAL_WIDTH").Value), 0, RsPO.Fields("ACTUAL_WIDTH").Value)))


                            SprdMain.Col = ColChargeableHeight
                            SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("CHARGEABLE_HEIGHT").Value), 0, RsPO.Fields("CHARGEABLE_HEIGHT").Value)))

                            SprdMain.Col = ColChargeableWidth
                            SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("CHARGEABLE_WIDTH").Value), 0, RsPO.Fields("CHARGEABLE_WIDTH").Value)))

                            SprdMain.Col = ColChargeableArea
                            SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("CHARGEABLEGLASS_AREA").Value), 0, RsPO.Fields("CHARGEABLEGLASS_AREA").Value)))



                        End If

                    End If

                    .Col = ColItemCode
                    .Text = mItemCode

                    .Col = ColItemDesc
                    MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    mItemDesc = MasterNo
                    .Text = mItemDesc

                    mItemDesc = ""
                    .Col = ColPartNo
                    If VB.Left(cboRefType.Text, 1) = "P" Then
                        mItemDesc = Trim(IIf(IsDBNull(RsPO.Fields("PART_NO").Value), "", RsPO.Fields("PART_NO").Value))
                    Else
                        MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                        mItemDesc = MasterNo
                    End If
                    .Text = mItemDesc

                    .Col = ColUnit
                    ''15-02-2006  'sk

                    MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "ISSUE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    mUOM = MasterNo
                    .Text = mUOM


                    If VB.Left(cboRefType.Text, 1) = "E" Or VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
                        If Trim(UCase(mUOM)) <> Trim(UCase(IIf(IsDBNull(RsPO.Fields("ITEM_UOM").Value), "", RsPO.Fields("ITEM_UOM").Value))) Then
                            MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "UOM_FACTOR", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                            mFactor = MasterNo
                        End If
                    Else
                        If Trim(UCase(mUOM)) <> Trim(UCase(IIf(IsDBNull(RsPO.Fields("UOM_CODE").Value), "", RsPO.Fields("UOM_CODE").Value))) Then
                            If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "UOM_FACTOR", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mFactor = MasterNo
                            Else
                                mFactor = 1
                            End If

                        End If
                    End If

                    If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
                        If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                            .Col = ColMRRNo
                            .Text = ""

                            .Col = ColRefNo
                            .Text = ""
                        Else
                            .Col = ColMRRNo
                            .Text = Trim(Str(IIf(IsDBNull(RsPO.Fields("MRR_REF_NO").Value), "", RsPO.Fields("MRR_REF_NO").Value)))

                            .Col = ColRefNo
                            .Text = Trim(IIf(IsDBNull(RsPO.Fields("SUPP_REF_NO").Value), "", RsPO.Fields("SUPP_REF_NO").Value))
                        End If

                    ElseIf VB.Left(cboRefType.Text, 1) = "U" Then
                        .Col = ColRefNo
                        .Text = Trim(Str(IIf(IsDBNull(RsPO.Fields("AUTO_KEY_INVOICE").Value), "", RsPO.Fields("AUTO_KEY_INVOICE").Value)))
                    End If

                    .Col = ColStockType
                    If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
                        'RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And
                        If RsCompany.Fields("IS_WAREHOUSE").Value = "Y"  Then
                            .Text = "ST"
                            mStockType = .Text
                        Else
                            .Text = "RJ"
                            mStockType = .Text
                        End If

                    ElseIf VB.Left(cboRefType.Text, 1) = "S" Then
                        .Text = IIf(RsCompany.Fields("IS_WAREHOUSE").Value = "N", "CR", "ST")
                        mStockType = .Text
                    Else
                        .Text = GetStockType(PubDBCn, mItemCode, mDivisionCode)  ''"FG"
                        mStockType = .Text
                    End If

                    .Col = ColStockQty
                    If VB.Left(cboRefType.Text, 1) = "E" Then
                        .Text = CStr(GetBalanceStockQty(mItemCode, (txtDNDate.Text), mUOM, "PAD", mStockType, "", ConWH, mDivisionCode, "DSP", Val(txtDNNo.Text)))
                    ElseIf VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
                        .Text = CStr(GetBalanceStockQty(mItemCode, (txtDNDate.Text), mUOM, "PAD", mStockType, "", ConWH, mDivisionCode, "DSP", Val(txtDNNo.Text)))
                    ElseIf VB.Left(cboRefType.Text, 1) = "J" Then
                        .Text = CStr(GetBalanceStockQty(mItemCode, (txtDNDate.Text), mUOM, "PAD", mStockType, "", ConWH, mDivisionCode, "DSP", Val(txtDNNo.Text)))
                    Else
                        .Text = CStr(GetBalanceStockQty(mItemCode, (txtDNDate.Text), mUOM, "PAD", mStockType, "", ConWH, mDivisionCode, "DSP", Val(txtDNNo.Text)))
                    End If


                    .Col = ColChargeableWidth
                    mWidth = Val(.Text)

                    .Col = ColChargeableHeight
                    mHeight = Val(.Text)



                    .Col = ColModel
                    mModelNo = Trim(.Text)

                    .Col = ColDrawingNo
                    mDrawingNo = Trim(.Text)

                    .Col = ColStoreLoc
                    xStoreLoc = Trim(SprdMain.Text)

                    If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "G" Then
                        mSOAmendNo = GetSOMaxAmendNo(CDbl(VB6.Format(Val(txtSONo.Text))))
                        mSOMKey = CDbl(Val(txtSONo.Text) & VB6.Format(mSOAmendNo, "000"))

                        If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mDIRequired = MasterNo
                        End If

                        If mDIRequired = "Y" Then
                            .Col = ColODNo
                            mODNo = .Text
                        End If

                        mScheduleQty = GetSalesDSQty(mItemCode, mDIRequired, mODNo, Trim(txtStoreLoc.Text), mWidth, mHeight, mModelNo, mDrawingNo)
                        mTotMonthPackQty = GetTotMonthDespatchQty(mItemCode, mDIRequired, mODNo, mWidth, mHeight, mModelNo, mDrawingNo, xStoreLoc)

                        .Col = ColBalScheduleQty
                        .Text = System.Math.Round(mScheduleQty - mTotMonthPackQty, 2)
                        mBalQty = System.Math.Round(mScheduleQty - mTotMonthPackQty, 2)
                    Else
                        .Col = ColBalScheduleQty
                        .Text = "0.00"
                    End If

                    .Col = ColPackQty
                    If VB.Left(cboRefType.Text, 1) = "E" Then
                        .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("PACKED_QTY").Value), 0, RsPO.Fields("PACKED_QTY").Value) * mFactor, "0.000")
                    ElseIf VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
                        If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And RsCompany.Fields("IS_WAREHOUSE").Value = "Y") Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y") Then
                            .Text = "0.00"
                        Else
                            .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("ITEM_QTY").Value), 0, RsPO.Fields("ITEM_QTY").Value) * mFactor, "0.000")
                        End If

                    ElseIf VB.Left(cboRefType.Text, 1) = "U" Then
                        .Text = VB6.Format(IIf(IsDBNull(RsPO.Fields("ITEM_QTY").Value), 0, RsPO.Fields("ITEM_QTY").Value) * mFactor, "0.000")
                    Else
                        mSoNo = CDbl(Trim(Str(IIf(IsDBNull(RsPO.Fields("AUTO_KEY_SO").Value), 0, RsPO.Fields("AUTO_KEY_SO").Value))))
                        If MainClass.ValidateWithMasterTable(Val(mSoNo), "AUTO_KEY_SO", "ORDER_TYPE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "' AND SO_APPROVED='Y'") = True Then
                            mSaleOrderType = MasterNo
                        Else
                            mSaleOrderType = "O"
                        End If
                        If mSaleOrderType = "C" Then
                            .Text = VB6.Format(IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, 0, mBalQty), "0.00")
                        Else
                            .Text = CStr(0)
                        End If

                    End If

NexrRec:
                    RsPO.MoveNext()
                    If RsPO.EOF = False Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        '                FormatSprdMain .MaxRows
                    End If
                Loop
            End With
        End If

        FormatSprdMain(-1)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Function GetSaleReturn(ByRef pSaleInvoiceNo As String, ByRef pCustCode As String, ByRef pItemCode As String) As Double

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSqlStr As String

        GetSaleReturn = 0
        mSqlStr = "SELECT SUM(ID.RECEIVED_QTY) AS RECEIVED_QTY" & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustCode) & "'" & vbCrLf & " AND IH.REF_TYPE IN ('2','I')" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND ID.REF_AUTO_KEY_NO=" & pSaleInvoiceNo & ""

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetSaleReturn = IIf(IsDBNull(RsTemp.Fields("RECEIVED_QTY").Value), 0, RsTemp.Fields("RECEIVED_QTY").Value)
        End If


        Exit Function
ERR1:
        GetSaleReturn = 0
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function GetSupplierRMBOM(ByRef pItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSqlStr As String

        GetSupplierRMBOM = False

        mSqlStr = "SELECT DISTINCT IH.PRODUCT_CODE" & vbCrLf _
            & " FROM PRD_SUPPLIERBOM_HDR IH, PRD_SUPPLIERBOM_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND IH.WEF IN (" & vbCrLf _
            & " SELECT WEF FROM PRD_SUPPLIERBOM_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND RM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND WEF <=TO_DATE('" & VB6.Format(txtDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " AND IH.STATUS='O'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetSupplierRMBOM = True
        End If


        Exit Function
ERR1:
        GetSupplierRMBOM = False
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function CheckDuplicateRow(ByRef mSoNo As Double, ByRef mItemCode As String, ByRef mDIRequired As String) As Boolean
        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        Dim mCheckItemCode As String
        Dim mSize As String
        Dim mModel As String

        If mItemCode = "" Then CheckDuplicateRow = False : Exit Function
        If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Then
            With SprdMain
                For I = 1 To .MaxRows
                    .Row = I
                    .Col = ColSONo
                    If Val(.Text) = Val(CStr(mSoNo)) Then
                        .Col = ColItemCode
                        mCheckItemCode = UCase(Trim(.Text))

                        .Col = ColMRRNo
                        mCheckItemCode = mCheckItemCode & UCase(Trim(.Text))

                        If UCase(Trim(mCheckItemCode)) = UCase(Trim(mItemCode)) Then
                            CheckDuplicateRow = True
                            Exit Function
                        End If
                    End If
                Next
            End With

        Else
            'If mDIRequired = "N" Then
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                CheckDuplicateRow = False
                Exit Function
                'With SprdMain
                '    For I = 1 To .MaxRows
                '        .Row = I
                '        .Col = ColSONo
                '        If Val(.Text) = Val(CStr(mSoNo)) Then
                '            .Col = ColItemCode
                '            mCheckItemCode = Trim(.Text)

                '            .Col = ColItemCode
                '            mCheckItemCode = Trim(.Text)

                '            .Col = ColItemCode
                '            mCheckItemCode = Trim(.Text)

                '            If UCase(Trim(mCheckItemCode)) = UCase(Trim(mItemCode)) Then
                '                CheckDuplicateRow = True
                '                Exit Function
                '            End If
                '        End If
                '    Next
                'End With
            Else
                With SprdMain
                    For I = 1 To .MaxRows
                        .Row = I
                        .Col = ColSONo
                        If Val(.Text) = Val(CStr(mSoNo)) Then
                            .Col = ColItemCode
                            If UCase(Trim(.Text)) = UCase(Trim(mItemCode)) Then
                                CheckDuplicateRow = True
                                Exit Function
                            End If
                        End If
                    Next
                End With
            End If

            'Else
            '    With SprdMain
            '        For I = 1 To .MaxRows
            '            .Row = I
            '            .Col = ColSONo
            '            If Val(.Text) = Val(CStr(mSoNo)) Then
            '                .Col = ColItemCode
            '                mCheckItemCode = UCase(Trim(.Text))

            '                .Col = ColMRRNo
            '                mCheckItemCode = mCheckItemCode & UCase(Trim(.Text))

            '                If UCase(Trim(mCheckItemCode)) = UCase(Trim(mItemCode)) Then
            '                    CheckDuplicateRow = True
            '                    Exit Function
            '                End If
            '            End If
            '        Next
            '    End With
            'End If
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub txtCustomerName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtCustomerName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtCustomerName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtCustomerName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAddress As String

        If Trim(TxtCustomerName.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT SUPP_CUST_NAME,SUPP_CUST_CODE, SUPP_CUST_ADDR, SUPP_CUST_CITY, " & vbCrLf _
            & " SUPP_CUST_STATE, SUPP_CUST_PIN " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_TYPE IN ('S','C')" & vbCrLf _
            & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote((TxtCustomerName.Text)) & "'"

        If ADDMode = True Then
            SqlStr = SqlStr & vbCrLf & "AND STATUS='O'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            txtCustomerCode.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
            'mAddress = Trim(IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value))
            'mAddress = mAddress & ", " & IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
            'mAddress = mAddress & ", " & IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

            'txtAddress.Text = mAddress
            mCustomerCode = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value))
            'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            '    TxtShipTo.Text = Trim(txtCustomerName.Text)
            'End If
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

    Private Function CheckDuplicateItem(ByRef mItemCode As String, ByRef pLotNo As String, ByRef pODNo As String, ByRef mDIRequired As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        Dim mLotNo As String
        Dim mProductionType As String
        Dim mCheckItem As String
        Dim mValidItemCode As String
        Dim mLotCheck As Boolean

        mLotCheck = False

        If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Or VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Or VB.Left(cboRefType.Text, 1) = "S" Or VB.Left(cboRefType.Text, 1) = "U" Or VB.Left(cboRefType.Text, 1) = "E" Then
            CheckDuplicateItem = False : Exit Function
        End If

        mLotCheck = False

        mValidItemCode = mItemCode & "-" & Trim(pLotNo)

        If mDIRequired = "Y" Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            mValidItemCode = mValidItemCode & "-" & Trim(pODNo)
        End If


        If mItemCode = "" Then CheckDuplicateItem = True : Exit Function

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                mCheckItem = UCase(Trim(.Text))

                .Col = ColBatchNo
                mCheckItem = mCheckItem & "-" & Trim(.Text)

                If mDIRequired = "Y" Then
                    .Col = ColODNo
                    mCheckItem = mCheckItem & "-" & Trim(.Text)
                End If

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                    .Col = ColModel
                    mCheckItem = mCheckItem & "-" & Trim(.Text)

                    .Col = ColDrawingNo
                    mCheckItem = mCheckItem & "-" & Trim(.Text)

                    .Col = ColChargeableArea
                    mCheckItem = mCheckItem & "-" & Trim(.Text)
                End If
                If mCheckItem = UCase(Trim(mValidItemCode)) Then
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

    Private Function CheckDuplicate57F4(ByRef mItemCode As String, ByRef m57F4No As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        Dim mCheckItemCode As String


        If mItemCode = "" Then CheckDuplicate57F4 = False : Exit Function
        If m57F4No = "" Then CheckDuplicate57F4 = False : Exit Function

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                mCheckItemCode = UCase(Trim(.Text))
                If InStr(1, UCase(Trim(mItemCode)), UCase(Trim(mCheckItemCode))) > 0 Then
                    .Col = ColRefNo
                    If UCase(Trim(.Text)) = UCase(Trim(m57F4No)) Then
                        mItemRept = mItemRept + 1
                        If mItemRept > 1 Then
                            CheckDuplicate57F4 = True
                            MsgInformation("Duplicate F4 No")
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColRefNo)
                            Exit Function
                        End If
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)

    End Function
    Private Function CheckRowCount() As Boolean
        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemCode As String
        Dim mQty As Double
        Dim mRowCount As Integer
        Dim mTotQty As Double
        Dim mInvoiceLineItem As Integer

        mInvoiceLineItem = 0
        If MainClass.ValidateWithMasterTable((txtCustomerCode.Text), "SUPP_CUST_CODE", "INVOICE_LINEITEM", "FIN_SUPP_CUST_HDR", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInvoiceLineItem = MasterNo
        End If

        mRowCount = 0
        mTotQty = 0
        CheckRowCount = True

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColPackQty
                mQty = Val(.Text)
                mTotQty = mTotQty + mQty

                If mItemCode <> "" And mQty > 0 Then
                    mRowCount = mRowCount + 1
                End If
            Next
        End With

        If mTotQty = 0 Then
            CheckRowCount = False
            MsgInformation("Nothing To Save.")
            Exit Function
        End If

        'If RsCompany.Fields("E_INVOICE_APP").Value = "Y" And mRowCount > 5 Then
        '    If UCase(VB.Left(cboRefType.Text, 1)) = "U" Then

        '    Else
        '        CheckRowCount = False
        '        MsgInformation("Cann't Despatch More Than 5 Item in one Despatch Note")
        '        Exit Function
        '    End If
        'Else

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
        Else
            If mInvoiceLineItem > 0 Then
                If mRowCount > mInvoiceLineItem And UCase(VB.Left(cboRefType.Text, 1)) = "P" Then
                    CheckRowCount = False
                    MsgInformation("Cann't Despatch More Than " & mInvoiceLineItem & " Item in one Despatch Note")
                    Exit Function
                End If
            End If
        End If




        Exit Function
ERR1:
        MsgInformation(Err.Description)
        CheckRowCount = False
    End Function

    Private Sub txtSuppFromDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppFromDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuppFromDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuppFromDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtSuppFromDate.Text) = "" Then Exit Sub

        If Not IsDate(txtSuppFromDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSuppToDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppToDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuppToDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuppToDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSuppToDate.Text) = "" Then Exit Sub

        If Not IsDate(txtSuppToDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        '    If FYChk(txtSuppToDate.Text) = False Then
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtTransporter_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtTransporter.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTransporter_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtTransporter.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtTransporter.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtVehicleNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtVehicleNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNo.DoubleClick
        SearchVehicleMaster()
    End Sub


    Private Sub txtVehicleNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicleNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicleNo.Text, "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtVehicleNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicleNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVehicleMaster()
    End Sub


    Private Sub txtVehicleNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVehicleNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        '    If Trim(txtVehicleNo.Text) = "" Then Exit Sub
        '    Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        '
        '    If MainClass.ValidateWithMasterTable(txtVehicleNo.Text, "NAME", "NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , Sqlstr) = False Then
        '        MsgInformation "Invalid Vehicle No"
        '        Cancel = True
        '    End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function GetSaleOldPrice(ByRef xAutoSONo As Double, ByRef xAutoSOAmendNo As Double, ByRef xCustomerCode As String, ByRef xItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT ID.ITEM_PRICE" & vbCrLf & " FROM DSP_SALEORDER_HDR IH,DSP_SALEORDER_DET ID" & vbCrLf & " WHERE IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.SUPP_CUST_CODE = '" & xCustomerCode & "' " & vbCrLf & " AND IH.AUTO_KEY_SO=" & Val(CStr(xAutoSONo)) & " " & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "' " & vbCrLf & " AND IH.AMEND_NO =" & Val(CStr(xAutoSOAmendNo)) & " AND SO_APPROVED='Y'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSaleOldPrice = IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value)
        End If
        Exit Function
ErrPart:
        GetSaleOldPrice = 0
    End Function
    Private Sub txtStoreLoc_TextChanged(sender As Object, e As EventArgs) Handles txtStoreLoc.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtStoreLoc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStoreLoc.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtStoreLoc.Text)
        e.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtStoreLoc_Validating(sender As Object, e As CancelEventArgs) Handles txtStoreLoc.Validating
        Dim Cancel As Boolean = e.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtStoreLoc.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtStoreLoc.Text), "LOC_CODE", "LOC_CODE", "DSP_CUST_STORE_LOC_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Store LOcation")
            e.Cancel = True
            Exit Sub
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        e.Cancel = Cancel
    End Sub

    Private Sub txtStoreLoc_KeyUp(sender As Object, e As KeyEventArgs) Handles txtStoreLoc.KeyUp
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchStoreLoc()
    End Sub
    Private Sub SearchStoreLoc()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtStoreLoc.Text), "DSP_CUST_STORE_LOC_MST", "LOC_CODE", "LOC_DESCRIPTION", , , SqlStr) = True Then
            txtStoreLoc.Text = AcName
            txtStoreLoc_Validating(txtStoreLoc, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdGetData_Click(sender As Object, e As EventArgs) Handles cmdGetData.Click
        If ADDMode = False And MODIFYMode = False Then GoTo EventExitSub
        If ADDMode = True Then
            If VB.Left(cboRefType.Text, 1) = "U" Then
                Call CollectPOData(False)
            Else
                Call CollectPOData(True)
            End If

            chkShipTo.Enabled = IIf(PubUserID = "G0416", True, False)

            'If VB.Left(cboRefType.Text, 1) = "Q" Or VB.Left(cboRefType.Text, 1) = "L" Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
            '    chkShipTo.Enabled = True
            '    'txtShipCustomer.Enabled = True
            '    'TxtShipTo.Enabled = True
            'End If

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
                chkShipTo.Enabled = True
            End If

            FormatSprdMain(-1)
        End If
EventExitSub:
    End Sub
    Private Function GetItemODWiseQry(ByRef pItemCode As String, ByRef pODNo As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RTemp As ADODB.Recordset = Nothing
        Dim mDIRequired As String = "N"

        If MainClass.ValidateWithMasterTable(Val(txtSONo.Text), "AUTO_KEY_SO", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "' AND SO_APPROVED='Y'") = True Then
            mDIRequired = MasterNo
        End If

        If mDIRequired = "N" Then Exit Function

        SqlStr = "SELECT OD_NO, SUM(PLAN_QTY) AS PLAN_QTY, SUM(PACKED_QTY) As DESP_QTY FROM ("

        SqlStr = SqlStr & vbCrLf _
            & " SELECT ID.OD_NO, ID.PLANNED_QTY AS PLAN_QTY, 0 AS PACKED_QTY" & vbCrLf _
            & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV " & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & ""

        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If pODNo <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.OD_NO='" & MainClass.AllowSingleQuote(pODNo) & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " UNION ALL " & vbCrLf _
            & " SELECT ID.OD_NO, 0 PLAN_QTY, ID.PACKED_QTY AS PACKED_QTY" & vbCrLf _
            & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP " & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & ""

        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If Val(txtDNNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_DESP<>" & Val(txtDNNo.Text) & ""
        End If

        If pODNo <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.OD_NO='" & MainClass.AllowSingleQuote(pODNo) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ) HAVING SUM(PLAN_QTY) > SUM(PACKED_QTY)"
        SqlStr = SqlStr & vbCrLf & " GROUP BY OD_NO "


        GetItemODWiseQry = SqlStr
        Exit Function
ErrPart:
        GetItemODWiseQry = ""
    End Function
    Private Function CheckODBalance(ByRef pItemCode As String, ByRef pODNo As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mQty As Double

        If RsCompany.Fields("stockbalcheck").Value = "N" Then
            CheckODBalance = True
            Exit Function
        End If
        SqlStr = "SELECT SUM(PLAN_QTY)- SUM(PACKED_QTY) As BAL_QTY FROM ("

        SqlStr = SqlStr & vbCrLf _
            & " SELECT ID.OD_NO, ID.PLANNED_QTY AS PLAN_QTY, 0 AS PACKED_QTY" & vbCrLf _
            & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV " & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & ""

        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If pODNo <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.OD_NO='" & MainClass.AllowSingleQuote(pODNo) & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " UNION ALL " & vbCrLf _
            & " SELECT ID.OD_NO, 0 PLAN_QTY, ID.PACKED_QTY AS PACKED_QTY" & vbCrLf _
            & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP " & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & ""

        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If Val(txtDNNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_DESP<>" & Val(txtDNNo.Text) & ""
        End If

        If pODNo <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.OD_NO='" & MainClass.AllowSingleQuote(pODNo) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ) HAVING SUM(PLAN_QTY) > SUM(PACKED_QTY)"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckODBalance = True
            mQty = IIf(IsDBNull(RsTemp.Fields("BAL_QTY").Value), 0, RsTemp.Fields("BAL_QTY").Value)
        Else
            CheckODBalance = False
        End If


        Exit Function
ErrPart:
        CheckODBalance = False
    End Function

    Private Sub TxtTransporter_DoubleClick(sender As Object, e As EventArgs) Handles TxtTransporter.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((TxtTransporter.Text), "FIN_TRANSPORTER_MST", "TRANSPORTER_NAME", "TRANSPORTER_ID", , , SqlStr) = True Then
            TxtTransporter.Text = AcName
            txtTransportCode.Text = AcName1
            If TxtTransporter.Enabled = True Then TxtTransporter.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
    Private Sub cboTransmode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTransmode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboVehicleType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboVehicleType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdSearchItem_Click(sender As Object, e As EventArgs) Handles cmdSearchItem.Click

        On Error GoTo ErrPart
        Dim mSearchItem As String
        Dim mFindItemName As String
        Dim I As Integer

        mSearchItem = Trim(txtSearchItem.Text)
        Dim counter As Short
        With SprdMain
            counter = mSearchStartRow
            For I = counter To .MaxRows
                .Row = I

                .Col = ColItemCode
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then			
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If

                .Col = ColItemDesc
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then			
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If
            Next
            mSearchStartRow = 1
NextRec:
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        Dim mBillNoSeq As String

        If dt.Rows.Count >= 1 Then
            For Each dtRow In dt.Rows
                Dim mItemCode = IIf(IsDBNull(dtRow.item(0)), "", dtRow.item(0))
                mItemDesc = ""
                mPartNo = IIf(IsDBNull(dtRow.item(2)), "", dtRow.item(2))

                mBillNo = IIf(IsDBNull(dtRow.item(4)), "", dtRow.item(4))
                mQty = IIf(IsDBNull(dtRow.item(5)), "", dtRow.item(5))

                With SprdMain


                    .Row = CntRow
                    .Col = ColItemCode
                    .Text = mItemCode

                    .Col = ColItemDesc
                    MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    mItemDesc = MasterNo
                    .Text = mItemDesc
                    mItemDesc = ""

                    .Col = ColPartNo
                    .Text = mPartNo

                    .Col = ColUnit
                    If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "ISSUE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mUOM = MasterNo
                    Else
                        mUOM = ""
                    End If

                    .Text = mUOM

                    .Col = ColRefNo
                    If MainClass.ValidateWithMasterTable(mBillNo, "BILLNO", "AUTO_KEY_INVOICE", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mBillNoSeq = MasterNo
                    Else
                        mBillNo = "-1"
                    End If
                    .Text = mBillNoSeq

                    .Col = ColStockType
                    .Text = IIf(RsCompany.Fields("IS_WAREHOUSE").Value = "Y", "ST", "FG")


                    .Col = ColPackQty
                    .Text = VB6.Format(mQty, "0.000")

                    CntRow = CntRow + 1
                    .MaxRows = CntRow
                End With
            Next
        End If


        Exit Sub
ErrPart:
        PubDBCn.RollbackTrans()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub

    Private Sub FrmDespatchNote_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        Frasprd.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtShipCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShipCustomer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtShipCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShipCustomer.DoubleClick
        cmdsearchShipTo_Click(cmdsearchShipTo, New System.EventArgs())
    End Sub

    Private Sub txtShipCustomer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtShipCustomer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtShipCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtShipCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtShipCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearchShipTo_Click(cmdsearchShipTo, New System.EventArgs())
    End Sub

    Private Sub txtShipCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShipCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAddress As String

        If Trim(txtShipCustomer.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT SUPP_CUST_NAME,SUPP_CUST_CODE, SUPP_CUST_ADDR, SUPP_CUST_CITY, " & vbCrLf _
            & " SUPP_CUST_STATE, SUPP_CUST_PIN " & vbCrLf _
            & " FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " --AND SUPP_CUST_TYPE IN ('S','C')" & vbCrLf _
            & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote((txtShipCustomer.Text)) & "'" & vbCrLf _
            & " AND LOCATION_ID='" & MainClass.AllowSingleQuote((TxtShipTo.Text)) & "'"

        'If ADDMode = True Then
        '    SqlStr = SqlStr & vbCrLf & "AND STATUS='O'"
        'End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            'txtCustomerCode.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
            mAddress = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value))
            mAddress = mAddress & ", " & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
            mAddress = mAddress & ", " & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

            txtAddress.Text = mAddress
            'mCustomerCode = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value))
            'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            '    TxtShipTo.Text = Trim(txtShipCustomer.Text)
            'End If
        Else
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
