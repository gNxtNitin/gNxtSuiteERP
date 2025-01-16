Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmInvoiceRCGST
    Inherits System.Windows.Forms.Form
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
    Dim mAuthorised As Boolean
    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String = ""
    Dim mCustomerCode As String
    Dim pRound As Double
    Dim mDNCnNO As String
    Dim mDNCnDate As String
    Dim pShowCalc As Boolean

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
    Private Const ColItemDesc As Short = 3
    Private Const ColHSNCode As Short = 4
    Private Const ColUnit As Short = 5
    Private Const ColQty As Short = 6
    Private Const ColRate As Short = 7
    Private Const ColAmount As Short = 8
    Private Const ColTaxableAmount As Short = 9
    Private Const ColCGSTPer As Short = 10
    Private Const ColCGSTAmount As Short = 11
    Private Const ColSGSTPer As Short = 12
    Private Const ColSGSTAmount As Short = 13
    Private Const ColIGSTPer As Short = 14
    Private Const ColIGSTAmount As Short = 15
    Private Const ColOType As Short = 16
    Private Const ColVNo As Short = 17
    Private Const ColVDate As Short = 18
    Private Const ColBillNo As Short = 19
    Private Const ColBillDate As Short = 20
    Private Const ColPartyName As Short = 21
    Private Const ColOMkey As Short = 22


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


    Dim pMSPCost As Double
    Dim pFreightCost As Double
    Dim pToolAmorCost As Double

    Private Sub cboClaimApp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboClaimApp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboClaimApp_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboClaimApp.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


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
        'Dim cntRow As Long
        'Dim mItemCode As String
        'Dim mMRP As Double
        'Dim mRate As Double
        'Dim mAbtementPer As Double
        Dim mInvCode As Double
        Dim mSuppCode As String
        'Dim mExpCode As Double
        'Dim mIndentificationCode As String
        '
        '    chkStockTrf.Value = vbUnchecked
        '    ChkPaintPrint.Value = vbUnchecked
        '    chkJWDetail.Value = vbUnchecked
        If Trim(cboInvType.Text) = "" Then GoTo EventExitSub

        SqlStr = "SELECT FIN_SUPP_CUST_MST.SUPP_CUST_CODE, FIN_INVTYPE_MST.CODE, " & vbCrLf & " SUPP_CUST_NAME,ISSTOCKTRF,INV_HEADING,FIN_INVTYPE_MST.IDENTIFICATION,IS_OEM,IS_INSTITUTIONAL, IS_AFTER_MKT " & vbCrLf & " FROM FIN_SUPP_CUST_MST,FIN_INVTYPE_MST " & vbCrLf & " WHERE FIN_SUPP_CUST_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=FIN_INVTYPE_MST.COMPANY_CODE " & vbCrLf & " AND FIN_SUPP_CUST_MST.SUPP_CUST_CODE=FIN_INVTYPE_MST.ACCOUNTPOSTCODE " & vbCrLf & " AND FIN_INVTYPE_MST.NAME='" & MainClass.AllowSingleQuote((cboInvType.Text)) & "'"

        If CDbl(lblInvoiceSeq.Text) = 7 Then
            SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION='G'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION='S'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)



        If RsTemp.EOF = False Then
            mInvCode = IIf(IsDbNull(RsTemp.Fields("CODE").Value), "", RsTemp.Fields("CODE").Value)
            mSuppCode = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
            '
            txtCreditAccount.Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
            '        chkStockTrf.Value = IIf(RsTemp.Fields("ISSTOCKTRF").Value = "Y", vbChecked, vbUnchecked)
            '        lblInvHeading.text = IIf(IsNull(RsTemp.Fields("INV_HEADING").Value), "", RsTemp.Fields("INV_HEADING").Value)
            '        If RsTemp!Identification = "J" Or RsTemp!Identification = "M" Then
            '            chkPrintType.Value = vbUnchecked
            ''            ChkPaintPrint.Value = vbChecked
            '        Else
            '            chkPrintType.Value = vbChecked
            ''            ChkPaintPrint.Value = vbUnchecked
            '        End If
            '
            '        If ADDMode = True Then
            '            Call FillExpFromPartyExp
            '        End If
        End If
        '
        '    If ADDMode = True Then
        '        With SprdExp
        '            For cntRow = 1 To .MaxRows
        '                .Row = cntRow
        '                .Col = ColExpSTCode
        '                mExpCode = Val(.Text)
        '                If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "IDENTIFICATION", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                    mIndentificationCode = MasterNo
        '                Else
        '                    mIndentificationCode = ""
        '                End If
        '                If mIndentificationCode = "MSC" Then
        '                    .Col = ColExpAmt
        '                    .Text = Format(pMSPCost, "0.00")
        ''                    Exit For
        '                End If
        '
        ''                If mIndentificationCode = "EMS" Then
        ''                    .Col = ColExpAmt
        ''                    .Text = Format(pExciseableMSCCost, "0.00")
        '                    Exit For
        ''                End If
        '
        '                If mIndentificationCode = "FRO" Then
        '                    .Col = ColExpAmt
        '                    .Text = Format(pFreightCost, "0.00")
        ''                    Exit For
        '                End If
        '                If mIndentificationCode = "TOL" Then
        '                    .Col = ColExpAmt
        '                    .Text = Format(pToolAmorCost, "0.00")
        ''                    Exit For
        '                End If
        '            Next
        '        End With
        '
        '        If MainClass.ValidateWithMasterTable(cboInvType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND (IS_OEM='Y' OR IS_INSTITUTIONAL='Y' OR IS_AFTER_MKT='Y')") = True Then
        '            With SprdMain
        '                For cntRow = 1 To .MaxRows - 1
        '                    .Row = cntRow
        '                    .Col = ColItemCode
        '                    mItemCode = Trim(.Text)
        '
        '
        '                    If MainClass.ValidateWithMasterTable(cboInvType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND (IS_OEM='Y' OR IS_INSTITUTIONAL='Y')") = True Then
        '                        .Col = ColMRP
        '                        .Text = "0.00"
        '
        '                        .Col = ColRate
        '                        .Text = GetMRPRate(txtBillDate.Text, "RATE_OEM", mItemCode, "L")
        '                    Else
        '                        .Col = ColMRP
        '                        mMRP = GetMRPRate(txtBillDate.Text, "RATE", mItemCode, "L")
        '                        .Text = mMRP
        '
        '                        .Col = ColRate
        '                        mRate = GetMRPRate(txtBillDate.Text, "RATE_AFTER_ABATE", mItemCode, "L")
        '                        .Text = mRate
        '
        '
        '
        '    '                    If MainClass.ValidateWithMasterTable(mInvCode, "TRNTYPE", "ABATEMENT_PER", "FIN_PARTY_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'") = True Then
        '    '
        '    '                        mAbtementPer = MasterNo
        '    '                        mRate = mMRP - (mMRP * mAbtementPer * 0.01)
        '    '
        '    '                        .Col = ColRate
        '    '                        .Text = mRate
        '    '                    End If
        '                    End If
        '                Next
        '            End With
        '            FormatSprdMain -1
        '        End If
        '    End If
        '    Call CalcTots
EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        On Error GoTo ERR1
        Dim mDivisionCode As Double
        Dim pSuppCode As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTaxable As Double
        Dim mPer As Double
        Dim mTaxAmount As String
        Dim I As Integer
        'Dim mServCode As String
        Dim mSACCode As String = ""
        Dim mReverseCharge As String = ""
        Dim mInterState As Boolean

        If MsgQuestion("Press YES for InterState & NO for IntraState.") = vbYes Then
            mInterState = True
        Else
            mInterState = False
        End If

        'mLocal = GetPartyBusinessDetail(Trim(xCustCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        'mLocal = IIf(mLocal = "Y", "L", "C")
        'mWithInCountry = GetPartyBusinessDetail(Trim(xCustCode), Trim(txtBillTo.Text), "WITHIN_COUNTRY")

        ''SELECT CLAUSE...
        mDivisionCode = -1
        If cboDivision.SelectedIndex >= 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        End If

        If CDbl(lblInvoiceSeq.Text) = 8 Then
            If Trim(txtSACCode.Text) = "" Then
                MsgInformation("Please Select The SAC Code.")
                Exit Sub
            Else
                If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    '                mServCode = Trim(IIf(IsNull(MasterNo), "", MasterNo))
                    mSACCode = Trim(txtSACCode.Text)
                Else
                    MsgBox("Invalid SAC Code.", MsgBoxStyle.Information)
                    Exit Sub
                End If

            End If
        End If

        If mDivisionCode = -1 Then
            MsgBox("Invalid Division Code.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "REVERSE_CHARGE_APP", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
            mReverseCharge = Trim(IIf(IsDbNull(MasterNo), "N", MasterNo))
        End If

        'If mReverseCharge = "Y" And Trim(txtInwardVNo.Text) = "" Then
        '    MsgInformation("Please Select The Inward Purchase Voucher No.")
        '    Exit Sub
        'End If


        MainClass.ClearGrid(SprdMain)

        '    SqlStr = " SELECT ID.*, 'M' AS O_TYPE "

        SqlStr = " SELECT DISTINCT ID.CUSTOMER_PART_NO, ID.ITEM_CODE, ID.ITEM_DESC, " & vbCrLf _
            & " ID.HSNCODE, ID.ITEM_UOM, ID.ITEM_QTY, ID.ITEM_RATE, ID.ITEM_AMT, ID.GSTABLE_AMT," & vbCrLf _
            & " ID.CGST_PER, ID.SGST_PER, ID.IGST_PER, 'M' AS O_TYPE, IH.BILLNO AS OBILLNO, IH.INVOICE_DATE AS OBILLDATE, IH.VNO AS OVNO, IH.VDATE AS OVDATE, CMST.SUPP_CUST_NAME, IH.MKEY"

        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_BUSINESS_MST CMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID"

        Dim mCompanyStateName As STRING = IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
        If mInterState = True Then
            SqlStr = SqlStr & vbCrLf & " AND CMST.SUPP_CUST_STATE<>'" & mCompanyStateName & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND CMST.SUPP_CUST_STATE='" & mCompanyStateName & "'"
        End If
        '
        SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""

        '    SqlStr = SqlStr & vbCrLf & " AND ID.SALEBILL_NO='S01800030'"

        SqlStr = SqlStr & vbCrLf & "AND (IH.ISGSTAPPLICABLE='R' OR ID.GST_RCAPP='Y')"

        SqlStr = SqlStr & vbCrLf & "AND IH.ISFINALPOST='Y' "

        If CDbl(lblInvoiceSeq.Text) = 7 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.PURCHASE_TYPE='G' OR ID.GOODS_SERVICE='G')"
        Else
            SqlStr = SqlStr & vbCrLf & "AND ID.HSNCODE='" & mSACCode & "'"
            SqlStr = SqlStr & vbCrLf & "AND (IH.PURCHASE_TYPE<>'G' OR ID.GOODS_SERVICE='S')"
        End If

        SqlStr = SqlStr & vbCrLf & "AND (ID.RCSALEBILLMKEY='' OR ID.RCSALEBILLMKEY IS NULL)"

        If mReverseCharge = "Y" And Trim(txtInwardVNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VNO='" & txtInwardVNo.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & "AND IH.VDATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & " SELECT '' AS CUSTOMER_PART_NO, '-1' AS ITEM_CODE, CMST.SUPP_CUST_NAME AS ITEM_DESC, " & vbCrLf _
            & " SAC AS HSNCODE, 'NOS' AS ITEM_UOM, 1 AS ITEM_QTY, AMOUNT AS ITEM_RATE, AMOUNT AS ITEM_AMT, AMOUNT AS GSTABLE_AMT," & vbCrLf _
            & " CGST_PER, SGST_PER, IGST_PER, 'J' AS O_TYPE, ''  AS OBILLNO, IH.VDATE AS OBILLDATE, IH.VNO AS OVNO, IH.VDATE AS OVDATE, CMST.SUPP_CUST_NAME, IH.MKEY" & vbCrLf _
            & " FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID, FIN_SUPP_CUST_MST CMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY AND ID.COMPANYCODE=CMST.COMPANY_CODE AND ID.ACCOUNTCODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND ID.DIV_CODE=" & Val(CStr(mDivisionCode)) & "" & vbCrLf _
            & " AND IH.REVERSE_CHARGE_APP='Y'" & vbCrLf _
            & " AND SAC='" & mSACCode & "'"

        SqlStr = SqlStr & vbCrLf & "AND (ID.SALEBILL_NO='' OR ID.SALEBILL_NO IS NULL)"

        If mReverseCharge = "Y" And Trim(txtInwardVNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VNO='" & txtInwardVNo.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.VDATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY 18, 14"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then

            FormatSprdMain(-1)
            I = 1
            RsTemp.MoveFirst()

            Do While RsTemp.EOF = False
                SprdMain.Row = I
                With SprdMain
                    .Col = ColItemCode
                    .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                    .Col = ColPartNo
                    .Text = IIf(IsDbNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)

                    .Col = ColItemDesc
                    .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_DESC").Value), "", RsTemp.Fields("ITEM_DESC").Value)

                    .Col = ColHSNCode
                    .Text = IIf(IsDbNull(RsTemp.Fields("HSNCODE").Value), "", RsTemp.Fields("HSNCODE").Value)

                    .Col = ColUnit
                    .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)

                    .Col = ColQty
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00")

                    .Col = ColRate
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value), "0.00")

                    .Col = ColAmount
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_AMT").Value), 0, RsTemp.Fields("ITEM_AMT").Value), "0.00")

                    .Col = ColTaxableAmount
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("GSTABLE_AMT").Value), 0, RsTemp.Fields("GSTABLE_AMT").Value), "0.00")
                    mTaxable = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("GSTABLE_AMT").Value), 0, RsTemp.Fields("GSTABLE_AMT").Value), "0.00"))

                    .Col = ColCGSTPer
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value), "0.00")
                    mPer = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value), "0.00"))

                    .Col = ColCGSTAmount
                    mTaxAmount = VB6.Format(mTaxable * mPer * 0.01, "0.00")
                    .Text = VB6.Format(mTaxAmount, "0.00")

                    .Col = ColSGSTPer
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value), "0.00")
                    mPer = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value), "0.00"))

                    .Col = ColSGSTAmount
                    mTaxAmount = VB6.Format(mTaxable * mPer * 0.01, "0.00")
                    .Text = VB6.Format(mTaxAmount, "0.00")

                    .Col = ColIGSTPer
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value), "0.00")
                    mPer = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value), "0.00"))

                    .Col = ColIGSTAmount
                    mTaxAmount = VB6.Format(mTaxable * mPer * 0.01, "0.00")
                    .Text = VB6.Format(mTaxAmount, "0.00")

                    .Col = ColOType
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("O_TYPE").Value), 0, RsTemp.Fields("O_TYPE").Value), "0.00")

                    .Col = ColBillNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("OBILLNO").Value), "", RsTemp.Fields("OBILLNO").Value)

                    .Col = ColBillDate
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OBILLDATE").Value), "", RsTemp.Fields("OBILLDATE").Value), "DD/MM/YYYY")

                    .Col = ColVNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("OVNO").Value), "", RsTemp.Fields("OVNO").Value)

                    .Col = ColVDate
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OVDATE").Value), "", RsTemp.Fields("OVDATE").Value), "DD/MM/YYYY")

                    .Col = ColPartyName
                    .Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)


                    .Col = ColOMkey
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("mKey").Value), 0, RsTemp.Fields("mKey").Value), "0.00")

                End With
                RsTemp.MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End If

        FormatSprdMain(-1)
        Call CalcTots()

        Exit Sub
ERR1:
        '    Resume
        MsgInformation(Err.Description)
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


    Private Sub txtInwardVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInwardVNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtInwardVNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInwardVNo.DoubleClick
        SearchInwardVNo()
    End Sub

    Private Sub txtInwardVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInwardVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInwardVNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtInwardVNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInwardVNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchInwardVNo()
    End Sub


    Private Sub SearchInwardVNo()
        On Error GoTo ErrPart
        Dim mDivisionCode As Double
        Dim pSuppCode As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTaxable As Double
        Dim mPer As Double
        Dim mTaxAmount As String
        Dim I As Integer
        'Dim mServCode As String
        Dim mSACCode As String = ""
        Dim mReverseCharge As String = ""

        mDivisionCode = -1
        If cboDivision.SelectedIndex >= 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        End If

        If CDbl(lblInvoiceSeq.Text) = 8 Then
            If Trim(txtSACCode.Text) = "" Then
                MsgInformation("Please Select The SAC Code.")
                Exit Sub
            Else
                If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    '                mServCode = Trim(IIf(IsNull(MasterNo), "", MasterNo))
                    mSACCode = Trim(txtSACCode.Text)
                Else
                    MsgBox("Invalid SAC Code.", MsgBoxStyle.Information)
                    Exit Sub
                End If

            End If
        End If

        If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "REVERSE_CHARGE_APP", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
            mReverseCharge = Trim(IIf(IsDbNull(MasterNo), "N", MasterNo))
        End If

        SqlStr = " SELECT DISTINCT IH.VNO, IH.VDATE, IH.BILLNO, IH.INVOICE_DATE, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.BILL_TO_LOC_ID"

        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""


        SqlStr = SqlStr & vbCrLf & "AND (IH.ISGSTAPPLICABLE='R' OR ID.GST_RCAPP='Y')"

        SqlStr = SqlStr & vbCrLf & "AND IH.ISFINALPOST='Y'  AND IH.CANCELLED='N'"

        If CDbl(lblInvoiceSeq.Text) = 7 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.PURCHASE_TYPE='G' OR ID.GOODS_SERVICE='G')"
        Else
            SqlStr = SqlStr & vbCrLf & "AND ID.HSNCODE='" & mSACCode & "'"
            SqlStr = SqlStr & vbCrLf & "AND (IH.PURCHASE_TYPE<>'G' OR (ID.GOODS_SERVICE='S' OR ID.GOODS_SERVICE=''))"
        End If

        SqlStr = SqlStr & vbCrLf & "AND (ID.RCSALEBILLMKEY='' OR ID.RCSALEBILLMKEY IS NULL)"

        If mReverseCharge = "Y" Then
            If Trim(txtInwardVNo.Text) <> "" Then
                SqlStr = SqlStr & vbCrLf & "AND IH.VNO='" & txtInwardVNo.Text & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.VDATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        'SqlStr = SqlStr & vbCrLf & " UNION "

        'SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT IH.VNO, IH.VDATE " & vbCrLf _
        '    & " FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID" & vbCrLf _
        '    & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
        '    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ID.DIV_CODE=" & Val(CStr(mDivisionCode)) & "" & vbCrLf & " AND IH.REVERSE_CHARGE_APP='Y'" & vbCrLf & " AND SAC='" & mSACCode & "'  AND IH.CANCELLED='N'"

        'SqlStr = SqlStr & vbCrLf & "AND (ID.SALEBILL_NO='' OR ID.SALEBILL_NO IS NULL)"

        'If mReverseCharge = "Y" Then
        '    If Trim(txtInwardVNo.Text) <> "" Then
        '        SqlStr = SqlStr & vbCrLf & "AND IH.VNO='" & txtInwardVNo.Text & "'"
        '    End If
        'End If

        'SqlStr = SqlStr & vbCrLf & "AND IH.VDATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        'Dim SqlStr  As String
        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.SearchGridMasterBySQL2((txtInwardVNo.Text), SqlStr) = True Then
            txtInwardVNo.Text = AcName
            txtInwardVNo_Validating(txtInwardVNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtInwardVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInwardVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mDivisionCode As Double
        Dim pSuppCode As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTaxable As Double
        Dim mPer As Double
        Dim mTaxAmount As String
        Dim I As Integer
        Dim mServCode As String
        Dim mSACCode As String = ""
        Dim mReverseCharge As String

        mDivisionCode = -1
        If cboDivision.SelectedIndex >= 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        End If

        mReverseCharge = "N"
        If CDbl(lblInvoiceSeq.Text) = 8 Then
            If Trim(txtSACCode.Text) = "" Then
                '            MsgInformation ("Please Select The SAC Code.")
                GoTo EventExitSub
            Else
                If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    mServCode = Trim(IIf(IsDbNull(MasterNo), "", MasterNo))
                    mSACCode = Trim(txtSACCode.Text)
                Else
                    MsgBox("Invalid SAC Code.", MsgBoxStyle.Information)
                    GoTo EventExitSub
                End If

            End If

            If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "REVERSE_CHARGE_APP", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                mReverseCharge = Trim(IIf(IsDbNull(MasterNo), "N", MasterNo))
            End If
        End If

        SqlStr = " SELECT DISTINCT IH.VNO, IH.VDATE"

        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""


        SqlStr = SqlStr & vbCrLf & "AND (IH.ISGSTAPPLICABLE='R' OR ID.GST_RCAPP='Y')"

        SqlStr = SqlStr & vbCrLf & "AND IH.ISFINALPOST='Y' "

        If CDbl(lblInvoiceSeq.Text) = 7 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.PURCHASE_TYPE='G' OR ID.GOODS_SERVICE='G')"
        Else
            SqlStr = SqlStr & vbCrLf & "AND ID.HSNCODE='" & mSACCode & "'"
            SqlStr = SqlStr & vbCrLf & "AND (IH.PURCHASE_TYPE<>'G' OR ID.GOODS_SERVICE='S')"
        End If

        SqlStr = SqlStr & vbCrLf & "AND (ID.RCSALEBILLMKEY='' OR ID.RCSALEBILLMKEY IS NULL)"

        If mReverseCharge = "Y" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VNO='" & txtInwardVNo.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.VDATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " UNION "

        SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT IH.VNO, IH.VDATE " & vbCrLf & " FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ID.DIV_CODE=" & Val(CStr(mDivisionCode)) & "" & vbCrLf & " AND IH.REVERSE_CHARGE_APP='Y'" & vbCrLf & " AND SAC='" & mSACCode & "'"

        SqlStr = SqlStr & vbCrLf & "AND (ID.SALEBILL_NO='' OR ID.SALEBILL_NO IS NULL)"

        If mReverseCharge = "Y" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VNO='" & txtInwardVNo.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.VDATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtInwardVDate.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDate").Value), "", RsTemp.Fields("VDate").Value), "DD/MM/YYYY")
        Else
            MsgBox("Invalid Inward Voucher No.", MsgBoxStyle.Information)
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ERR1:
        '    Resume
        MsgInformation(Err.Description)

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtSACCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSACCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            pShowCalc = True
            SprdMain.Enabled = True
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
        Me.Dispose()
    End Sub


    Private Sub ReportonShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mCT3Date As String

        Report1.Reset()
        mTitle = ""
        mSubTitle = ""

        SqlStr = MakeSQL
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\FormARE_3.RPT"

        Report1.SQLQuery = SqlStr
        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, , , "Y")

        MainClass.AssignCRptFormulas(Report1, "Range=""" & IIf(IsDbNull(RsCompany.Fields("EXCISE_RANGE").Value), "", RsCompany.Fields("EXCISE_RANGE").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "Division=""" & IIf(IsDbNull(RsCompany.Fields("EXCISE_DIV").Value), "", RsCompany.Fields("EXCISE_DIV").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "RegnNo=""" & IIf(IsDbNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "Place=""" & IIf(IsDbNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & """")

        '    mCT3Date = GetCT3Date(PubDBCn, Val(TxtCTNo.Text), "", "S", mCustomerCode)

        '    MainClass.AssignCRptFormulas Report1, "CT3Date=""" & mCT3Date & """"

        Report1.WindowShowGroupTree = False
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
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND ID.COMPANY_CODE=ITEMMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=ITEMMST.ITEM_CODE " & vbCrLf & " AND IH.MKEY='" & MainClass.AllowSingleQuote((lblMkey.Text)) & "'"


        MakeSQL = MakeSQL & vbCrLf & "ORDER BY SUBROWNO "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click

        On Error GoTo DelErrPart
        Dim mDeleteRights As String
        Dim xDCNo As String
        Dim mBillNo As String

        'Exit Sub

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

        If MainClass.GetUserCanModify((txtBillDate.Text)) = False Then
            MsgBox("You Have Not Rights to Delete back Voucher", MsgBoxStyle.Information)
            Exit Sub
        End If

        mDeleteRights = GetUserPermission("INVOICE_ADMIN", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)

        If mDeleteRights = "N" Then
            MsgBox("You Have Not Rights to Delete Invoice.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If CheckBillPayment(mCustomerCode, (txtBillNo.Text), "B", (txtBillDate.Text)) = True Then Exit Sub

        If RsSaleMain.Fields("ISTCSPAID").Value = "Y" Then
            MsgInformation("TCS Challan made against this invoice So cann't be Deleted.")
            Exit Sub
        End If

        If Not RsSaleMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_HDR", (LblMKey.Text), RsSaleMain, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_HDR", (LblMKey.Text), RsSaleDetail, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_HDR", (LblMKey.Text), RsSaleExp, "MKEY", "D") = False Then GoTo DelErrPart



                If InsertIntoDeleteTrn(PubDBCn, "FIN_INVOICE_HDR", "MKEY", (lblMkey.Text)) = False Then GoTo DelErrPart

                mBillNo = Trim(Trim(txtBillNoPrefix.Text) & Val(txtBillNo.Text) & Trim(txtBillNoSuffix.Text))

                SqlStr = " UPDATE FIN_PURCHASE_HDR SET " & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND MKEY IN (SELECT MKEY FROM FIN_PURCHASE_DET WHERE RCSALEBILLMKEY='" & LblMKey.Text & "')"

                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE FIN_PURCHASE_DET SET RCSALEBILLMKEY='', SALEBILLNOPREFIX='',SALEBILLNOSEQ=0, SALEBILL_NO='', SALEBILLDATE   =''  " & vbCrLf & " WHERE RCSALEBILLMKEY='" & LblMKey.Text & "' " & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE FIN_VOUCHER_DET SET SALEBILLNOPREFIX='', " & vbCrLf & " SALEBILLNOSEQ=0, SALEBILL_NO='', SALEBILLDATE   ='' " & vbCrLf & " WHERE" & vbCrLf & " SALEBILL_NO='" & mBillNo & "'" & vbCrLf _
                    & " AND SALEBILLDATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND CompanyCode=" & RsCompany.Fields("Company_Code").Value & " " '' & vbCrLf |                    & "  " & vbCrLf |                    & " "

                PubDBCn.Execute(SqlStr)

                PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "' AND BOOKCODE='" & LblBookCode.Text & "'")
                PubDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' AND BookSubType='" & mBookSubType & "'")

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


        SqlStr = "SELECT PRINTED FROM FIN_INVOICE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND Mkey ='" & MainClass.AllowSingleQuote((LblMKey.Text)) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPRINTED = IIf(IsDBNull(RsTemp.Fields("PRINTED").Value), "N", RsTemp.Fields("PRINTED").Value)
            If mPRINTED = "Y" Then
                MsgInformation("Invoice Print Already taken so that you cann't be Modified.")
                Exit Sub
            End If
        End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
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
        Dim mPrintOption As String = ""


        frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(4).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(3).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).Enabled = False

        frmPrintInvCopy.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        For CntCount = 0 To 5
            If frmPrintInvCopy.chkPrintOption(CntCount).CheckState = System.Windows.Forms.CheckState.Checked Then
                mInvoicePrintType = UCase(frmPrintInvCopy.chkPrintOption(CntCount).Text)
                Call ReportOnSales(Crystal.DestinationConstants.crptToWindow, mInvoicePrintType, "N", mPrintOption)
            End If
        Next

        frmPrintInvCopy.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ErrPart:
        frmPrintInvoice.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub ReportOnSales(ByRef Mode As Crystal.DestinationConstants, ByRef mInvoicePrintType As String, ByRef pIsTradingInv As String, ByRef mPrintOption As String)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNO As String
        Dim mWithInState As String

        '    If chkCancelled.Value = vbChecked Then
        '        MsgInformation "Cancelled Invoice Cann't be Print."
        '        Exit Sub
        '    End If

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mWithInState = "N"
        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mWithInState = MasterNo
        End If

        SqlStr = ""
        mTitle = ""
        mSubTitle = ""

        Call SelectQryForPrint(SqlStr)

        If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "INV_HEADING", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
            mTitle = MasterNo
        End If

        mTitle = "Tax Invoice (Reverse Charge)"
        mSubTitle = "[See Section 31 of CGST Act, 2017 read with Rule 46 of CGST Rules]"

        mRptFileName = "Invoice_RC.rpt"

        Call ShowExciseReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, True, mInvoicePrintType)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Function SelectQryForPrint(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, GMST.*, CMST.SUPP_CUST_NAME "



        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST "

        '    If lblInvoiceSeq.text = 7 Or lblInvoiceSeq.text = 8 Then
        '
        '    Else
        '        mSqlStr = mSqlStr & vbCrLf & ", DSP_DESPATCH_DET IDD"
        '    End If



        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY='" & LblMKey.Text & "'"

        '    If lblInvoiceSeq.text = 7 Or lblInvoiceSeq.text = 8 Then
        '
        '    Else
        '        mSqlStr = mSqlStr & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=IDD.COMPANY_CODE" & vbCrLf _
        ''            & " AND IH.AUTO_KEY_DESP=IDD.AUTO_KEY_DESP" & vbCrLf _
        ''            & " AND ID.ITEM_CODE=IDD.ITEM_CODE AND ID.SUBROWNO=IDD.SERIAL_NO"
        '    End If
        '
        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SUBROWNO"

        SelectQryForPrint = mSqlStr
    End Function

    Private Sub ShowExciseReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean, ByRef mInvoicePrintType As String)

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
        Dim mPlaceofSupply As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, , , "Y")

        mStateName = ""
        mStateCode = ""

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mStateName = MasterNo
            mStateCode = GetStateCode(mStateName)
        End If

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mWithInState = MasterNo
        End If

        If mWithInState = "N" Then
            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInCountry = MasterNo
            End If
        End If

        mPlaceofSupply = VB6.Format(mStateCode, "00") & "-" & mStateName '' IIf(mWithInState = "Y", "INTRA STATE", IIf(mWithInCountry = "Y", "INTER STATE", "EXPORT"))


        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.WindowShowPrintBtn = False '' IIf(PubSuperUser = "S", True, False)
        Report1.WindowShowPrintSetupBtn = False ''IIf(PubSuperUser = "S", True, False)
        Report1.WindowShowExportBtn = IIf(PubSuperUser = "S", True, False)

        SqlStr = " SELECT NETVALUE, NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT,INV_PREP_TIME, " & vbCrLf & " SHIPPED_TO_PARTY_CODE, REMOVAL_TIME, OUR_AUTO_KEY_SO, SHIPPED_TO_SAMEPARTY" & vbCrLf & " FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & MainClass.AllowSingleQuote((LblMKey.Text)) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
            mNetDuty = IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)

            mShipToSameParty = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
            mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)

            mPrepTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INV_PREP_TIME").Value), "", RsTemp.Fields("INV_PREP_TIME").Value), "HH:MM")
            mRemovalTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("REMOVAL_TIME").Value), "", RsTemp.Fields("REMOVAL_TIME").Value), "HH:MM")
            mSO = IIf(IsDBNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), "", RsTemp.Fields("OUR_AUTO_KEY_SO").Value)

            '        If mShipToSameParty = "Y" Then
            '            mShipToName = ""
            '            mShipToAddress = ""
            '            mShipToCity = ""
            '            mShipToGSTN = ""
            '            mShipToState = ""
            '            mShipToStateCode = ""
            '        Else
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
            '        End If

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

        '    MainClass.AssignCRptFormulas Report1, "mServiceName=""" & Trim(txtServProvided.Text) & """"

        mPayTerms = ""
        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            SqlStr = " SELECT PAYMENT_DTL, BALANCE_PAY_DTL " & vbCrLf & " FROM DSP_SALEORDER_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_SO=" & Val(CStr(mSO)) & " AND SO_APPROVED='Y'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mPayTerms = IIf(IsDBNull(RsTemp.Fields("PAYMENT_DTL").Value), "", RsTemp.Fields("PAYMENT_DTL").Value)
                mBalPayTerms = Trim(IIf(IsDBNull(RsTemp.Fields("BALANCE_PAY_DTL").Value), "", RsTemp.Fields("BALANCE_PAY_DTL").Value))
                If mBalPayTerms <> "" Then
                    mPayTerms = mPayTerms & " Balance Payment Terms : " & mBalPayTerms
                End If
            End If
            MainClass.AssignCRptFormulas(Report1, "PAYTERMS=""" & mPayTerms & """")
        End If

        If IsSubReport = True Then

            mAmountInword = MainClass.RupeesConversion(mNetAmount)
            mDutyInword = MainClass.RupeesConversion(mNetDuty)

            MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")
            MainClass.AssignCRptFormulas(Report1, "NetAmount=""" & VB6.Format(mNetAmount, "0.00") & """")
            MainClass.AssignCRptFormulas(Report1, "DutyInword=""" & mDutyInword & """")

            SqlStrSub = " SELECT FIN_INVOICE_EXP.MKEY, FIN_INVOICE_EXP.SUBROWNO, FIN_INVOICE_EXP.EXPPERCENT, FIN_INVOICE_EXP.AMOUNT, FIN_INTERFACE_MST.COMPANY_CODE, FIN_INTERFACE_MST.NAME" & vbCrLf & " FROM FIN_INVOICE_EXP, FIN_INVOICE_HDR, FIN_INTERFACE_MST " & vbCrLf & " WHERE FIN_INVOICE_EXP.MKEY = FIN_INVOICE_HDR.MKEY AND FIN_INVOICE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf & " AND FIN_INVOICE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

            SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"

            '    If PubGSTApplicable = True Then
            '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
            '    Else
            '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
            '    End If

            SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub
            MainClass.AssignCRptFormulas(Report1, "JWSTRemarks=""" & mJWSTRemarks & """")
            Report1.SubreportToChange = ""
        End If

        Report1.Action = 1
        Report1.ReportFileName = ""
        Report1.Reset()



        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, , "Y")
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

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
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPRINTED As String = ""
        Dim mExtraRemarks As String
        Dim mPrintOption As String


        mPrintOption = "I"

        SqlStr = "SELECT PRINTED FROM FIN_INVOICE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND Mkey ='" & MainClass.AllowSingleQuote((LblMKey.Text)) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPRINTED = IIf(IsDBNull(RsTemp.Fields("PRINTED").Value), "N", RsTemp.Fields("PRINTED").Value)
            mPRINTED = IIf(PubSuperUser = "S", "N", mPRINTED)
        End If

        frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).Enabled = False

        '        frmPrintInvCopy.chkPrintOption(0).Value = IIf(mPRINTED = "Y", vbUnchecked, vbChecked)
        frmPrintInvCopy.chkPrintOption(0).Enabled = IIf(mPRINTED = "Y", False, True)
        frmPrintInvCopy.ShowDialog()


        If G_PrintLedg = False Then
            Exit Sub
        End If

        For CntCount = 0 To 5
            If frmPrintInvCopy.chkPrintOption(CntCount).CheckState = System.Windows.Forms.CheckState.Checked Then
                mInvoicePrintType = UCase(frmPrintInvCopy.chkPrintOption(CntCount).Text)
                Call ReportOnSales(Crystal.DestinationConstants.crptToPrinter, mInvoicePrintType, "N", mPrintOption)
            End If
        Next

        If frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = "UPDATE FIN_INVOICE_HDR SET  PRINTED= 'Y', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND  Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"

            PubDBCn.Execute(SqlStr)
        End If

        frmPrintInvCopy.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '    Else
        '        mInvoicePrint = True
        '        mSubsidiaryChallanPrint = "N"
        '        mAnnexPrint = "N"
        '        If chkTaxOnMRP.Value = vbChecked Then
        '            frmPrintInvoice.OptInvoiceAnnex.Enabled = True
        '            frmPrintInvoice.OptInvoiceAnnex.Visible = True
        '            frmPrintInvoice.optSubsidiaryChallan.Enabled = False
        '            frmPrintInvoice.optSubsidiaryChallan.Visible = False
        '
        '            frmPrintInvoice.Show 1
        '
        '            If G_PrintLedg = False Then
        '                Exit Sub
        '            End If
        '
        '            If frmPrintInvoice.OptInvoice.Value = True Then
        '                mInvoicePrint = True
        '            Else
        '                mInvoicePrint = False
        '                 mAnnexPrint = "Y"
        '            End If
        '
        '        ElseIf lblDespRef.text = "J" Then
        '            frmPrintInvoice.OptInvoiceAnnex.Enabled = False
        '            frmPrintInvoice.OptInvoiceAnnex.Visible = False
        '            frmPrintInvoice.optSubsidiaryChallan.Enabled = True
        '            frmPrintInvoice.optSubsidiaryChallan.Visible = True
        '
        '            frmPrintInvoice.Show 1
        '
        '            If G_PrintLedg = False Then
        '                Exit Sub
        '            End If
        '
        '            If frmPrintInvoice.OptInvoice.Value = True Then
        '                mInvoicePrint = True
        '            Else
        '                mInvoicePrint = False
        '                mSubsidiaryChallanPrint = "Y"
        '                If frmPrintInvoice.optSCOption(0).Value = True Then
        '                    mSC_All = "Y"
        '                    mSC_F4No = ""
        '                Else
        '                    mSC_All = "N"
        '                    mSC_F4No = Trim(frmPrintInvoice.txtF4no.Text)
        '                End If
        '            End If
        '        End If
        '
        '        mMaxRow = SprdMain.MaxRows - 1
        '
        '        If RsCompany.fields("COMPANY_CODE").value = 5 Or RsCompany.fields("COMPANY_CODE").value = 2 Then
        '            If mMaxRow > 7 Then
        '                frmPrintInvoice.Show 1
        '                If G_PrintLedg = False Then
        '                    Exit Sub
        '                End If
        '                If frmPrintInvoice.OptInvoice.Value = True Then
        '                    mEXPAnnexPrint = "YI"
        '                Else
        '                    mEXPAnnexPrint = "YA"
        '                End If
        '            Else
        '                mEXPAnnexPrint = "N"
        '            End If
        '        Else
        '            mEXPAnnexPrint = "N"
        '        End If
        '
        '        mExtraRemarks = ""
        '
        '        If Val(txtCustMatValue.Text) <> 0 Then
        '            mExtraRemarks = Chr(15) & "Material Supplied by us : Rs. " & vb6.Format(lblTotItemValue.text, "0.00") & Chr(18)
        '            mExtraRemarks = mExtraRemarks & vbCrLf & String(4, " ") & Chr(15) & "Material Supplied by Customer : Rs. " & vb6.Format(Val(txtCustMatValue.Text), "0.00") & Chr(18)
        '            mExtraRemarks = mExtraRemarks & vbCrLf & String(4, " ") & Chr(15) & "Excise Duty Calculated on Assesable Value of : Rs. " & vb6.Format(Val(txtCustMatValue.Text) + Val(lblTotItemValue.text), "0.00") & Chr(18)
        '        End If
        '
        '        Call PrintExcise("P", lblMkey.text, IIf(chkPrintType.Value = vbChecked, "Y", "N"), IIf(ChkPaintPrint.Value = vbChecked, "Y", "N"), lblInvHeading.text, lblDespRef.text, mAnnexPrint, mEXPAnnexPrint, IIf(chkTaxOnMRP.Value = vbChecked, "Y", "N"), IIf(chkJWDetail.Value = vbChecked, "Y", "N"), mSubsidiaryChallanPrint, mJWRemarks, mJWSTRemarks, mSC_All, mSC_F4No, "", "", IIf(chkPrintByGroup.Value = vbUnchecked, "N", "Y"), IIf(chkAgtPermission.Value = vbUnchecked, "N", "Y"), mExtraRemarks)
        '        Unload frmPrintInvoice
        '    End If
        '


        Exit Sub
ErrPart:
        frmPrintInvoice.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Call CalcTots()

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

    Private Sub FrmInvoiceRCGST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

        Dim xIName As String = ""
        Dim SqlStr As String = ""

        Exit Sub

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                '            If mainclass.SearchMaster(.Text, "vwITEM", "ITEMCODE", SqlStr) = True Then
                '                .Row = .ActiveRow
                '                .Col = ColItemCode
                '                .Text = AcName
                '            End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemDesc
                xIName = .Text
                .Text = ""
                '            If mainclass.SearchMaster(.Text, "vwITEM", "Name", SqlStr) = True Then
                '                .Row = .ActiveRow
                '                .Col = ColItemDesc
                '                .Text = AcName
                '            Else
                '                .Row = .ActiveRow
                '                .Col = ColItemDesc
                '                .Text = xIName
                '            End If
                MainClass.ValidateWithMasterTable(.Text, "Name", "ItemCode", "Item", PubDBCn, MasterNo)
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = MasterNo
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

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

        Call CalcTots()
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
                'Call CheckRate()
        End Select
        Call CalcTots()
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

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row

            .Col = 1
            cboInvType.Text = Trim(.Text)

            .Col = 2
            txtBillNoPrefix.Text = .Text

            .Col = 3
            txtBillNo.Text = .Text

            .Col = 4
            txtBillNoSuffix.Text = .Text

            .Col = 6
            txtBillDate.Text = VB6.Format(.Text, "DD/MM/YYYY")

            txtBillNo_Validating(txtBillNo, New System.ComponentModel.CancelEventArgs(False))
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub


    Public Sub txtBillNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mBillNo As String
        If Trim(txtBillNo.Text) = "" Then GoTo EventExitSub

        txtBillNo.Text = Val(txtBillNo.Text)

        If MODIFYMode = True And RsSaleMain.EOF = False Then xMkey = RsSaleMain.Fields("mKey").Value
        mBillNo = Trim(Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & Trim(txtBillNoSuffix.Text))
        '    mBillNo = "S05135"
        SqlStr = " SELECT * FROM FIN_INVOICE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "' " & vbCrLf _
            & " AND BillNo='" & MainClass.AllowSingleQuote(mBillNo) & "' " & vbCrLf _
            & " AND BookCode=" & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' "

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
        Dim mConsingee As String = ""
        Dim mBuyerCode As String
        Dim mCoBuyerCode As String
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
        Dim mPRINTED As String
        Dim mCancelled As String
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
        Dim pDueDate As String = ""
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
        Dim mRemarks As String
        Dim mDutyFreePurchase As String = ""
        Dim mDivisionCode As Double
        Dim mAgtPermission As String
        Dim mShippedToSame As String
        Dim mShippedToCode As String = ""
        Dim mDespatchNo As Double

        Dim pCGSTClaimAmount As Double
        Dim pSGSTClaimAmount As Double
        Dim pIGSTClaimAmount As Double
        Dim cntRow As Integer
        Dim mOType As String
        Dim mOMkey As String
        'Dim mGSTAPP As String
        Dim mHSNCode As String
        Dim pClaimApp As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_HDR", (LblMKey.Text), RsSaleMain, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_HDR", (LblMKey.Text), RsSaleDetail, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_INVOICE_HDR", (LblMKey.Text), RsSaleExp, "MKEY", "M") = False Then GoTo ErrPart
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        pClaimApp = VB.Left(cboClaimApp.Text, 1)

        mFormRecdCode = -1
        mFormDueCode = -1

        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
            mTRNType = MasterNo
        Else
            mTRNType = CStr(-1)
            MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mSuppCustCode = "-1"
        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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
            If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mShippedToCode = MasterNo
            End If
        End If

        If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            mAccountCode = "-1"
            MsgBox("Credit Account Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mAUTHSIGN = ""
        mAUTHDATE = "" '' Format(txtAuthDate.Text, "DD-MMM-YYYY")
        mFREIGHTCHARGES = "Paid"
        mEXEMPT_NOTIF_NO = ""
        mBookCode = CInt(LblBookCode.Text)

        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
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

        mTotServiceAmount = 0
        mTotServicePercent = 0


        mTOTEXPAMT = Val(lblTotExpAmt.Text)
        mNETVALUE = Val(lblNetAmount.Text)

        mSTPERCENT = 0
        mTOTFREIGHT = 0
        mEDPERCENT = 0
        mDutyForgone = CStr(0)

        mTOTTAXABLEAMOUNT = Val(lblTotTaxableAmt.Text)

        mRO = 0 'Val(lblRO.text)
        mTotDiscount = 0
        mSURAmount = 0
        mMSC = 0 ' Val(lblMSC.text)
        mTCSAMOUNT = 0 '  Val(lblTCS.text)
        mTCSPER = 0 ' Val(lblTCSPercentage.text)

        mTotQty = Val(lblTotQty.Text)
        mLSTCST = ""
        mWITHFORM = ""
        mFOC = "N" 'IIf(chkFOC.Value = vbChecked, "Y", "N")
        mPRINTED = "N"
        mCancelled = "N" ' IIf(chkCancelled.Value = vbChecked, "Y", "N")
        mIsRegdNo = "N" '' IIf(chkRegDealer.Value = vbChecked, "Y", "N")
        mREJECTION = "N" '' IIf(chkRejection.Value = vbChecked, "Y", "N")
        mD3 = "N" '' IIf(chkD3.Value = vbChecked, "Y", "N")
        mCT3 = "N" ''IIf(chkCT3.Value = vbChecked, "Y", "N")
        mCT1 = "N" ''IIf(chkCT1.Value = vbChecked, "Y", "N")
        mAgtPermission = "N" ' IIf(chkAgtPermission.Value = vbChecked, "Y", "N")
        mTaxOnMRP = "N" 'IIf(chkTaxOnMRP.Value = vbChecked, "Y", "N")
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

        mStockTrf = "N" ' IIf(chkStockTrf.Value = vbChecked, "Y", "N")

        mPackMat = "N" ' IIf(chkPackmat.Value = vbChecked, "Y", "N")

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
            mBillNoSeq = CInt(AutoGenSeqBillNo(mBookType, mBookSubType, mStartingNo, mDivisionCode))
        Else
            mBillNoSeq = Val(txtBillNo.Text)
        End If

        txtBillNo.Text = Val(CStr(mBillNoSeq))

        If mAuthorised = False Then
            If CheckValidBillDate(mBillNoSeq, mDivisionCode) = False Then GoTo ErrPart
        End If

        mBillNo = Trim(Trim(txtBillNoPrefix.Text) & Val(CStr(mBillNoSeq)) & Trim(txtBillNoSuffix.Text))
        mAutoKeyNo = VB6.Format(Val(CStr(mBillNoSeq)), "00000000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")

        If ADDMode = True Then
            mDespatchNo = MainClass.AutoGenRowNo("FIN_INVOICE_HDR", "DC", PubDBCn)
            mDespatchNo = CDbl(RsCompany.Fields("Company_Code").Value & RsCompany.Fields("FYEAR").Value & VB6.Format(mDespatchNo, "00000"))
        End If

        '    If Left(cboGSTStatus.Text, 1) = "R" Then
        ''        If ADDMode = True Then
        ''            mSaleBillNoPrefix = "S"
        ''            mSaleBillNoSeq = AutoGenSeqSaleBillNo(lblPurchaseType.text)
        ''            mSaleBillNo = mSaleBillNoPrefix & vb6.Format(mSaleBillNoSeq, "00000000")
        ''            mSaleBillDate = Format(TxtVDate.Text, "DD/MM/YYYY")
        ''        Else
        '            mSaleBillNoPrefix = "S"
        '            mSaleBillNoSeq = lblSaleBillNoSeq.text
        '            mSaleBillNo = lblSaleBillNo.text
        '            mSaleBillDate = Format(lblSaleBillDate.text, "DD/MM/YYYY")
        ''        End If
        '    Else
        '        mSaleBillNoPrefix = ""
        '        mSaleBillNoSeq = 0
        '        mSaleBillNo = ""
        '        mSaleBillDate = ""
        '    End If

        '    mAutoKeyNo = Val(IIf(IsNull(RsCompany!INVOICE_PREFIX), 0, RsCompany!INVOICE_PREFIX)) & Val(lblInvoiceSeq.text) & vb6.Format(Val(mBillNoSeq), "00000") & vb6.Format(RsCompany.Fields("FYEAR").Value, "0000") & vb6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")

        SqlStr = ""
        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("FIN_INVOICE_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo
            LblMKey.Text = nMkey
            SqlStr = "INSERT INTO FIN_INVOICE_HDR (" & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " ROWNO, TRNTYPE, BILLNOPREFIX, " & vbCrLf & " AUTO_KEY_INVOICE, BILLNOSEQ, BILLNOSUFFIX, BILLNO, " & vbCrLf & " INVOICE_DATE, INV_PREP_DATE, INV_PREP_TIME, " & vbCrLf & " AUTO_KEY_DESP, DCDATE, CUST_PO_NO, CUST_PO_DATE, " & vbCrLf & " AMEND_NO, AMEND_DATE, AMEND_WEF_FROM, REMOVAL_DATE, " & vbCrLf & " REMOVAL_TIME, SUPP_CUST_CODE, ACCOUNTCODE, ST_38_NO, " & vbCrLf & " DUEDAYSFROM, DUEDAYSTO, AUTHSIGN, AUTHDATE, " & vbCrLf & " GRNO, GRDATE, DESPATCHMODE, DOCSTHROUGH, " & vbCrLf & " VEHICLENO, CARRIERS, FREIGHTCHARGES, " & vbCrLf & " TARIFFHEADING, EXEMPT_NOTIF_NO, " & vbCrLf & " BOOKCODE,BOOKTYPE, BOOKSUBTYPE, SALETAXCODE, " & vbCrLf & " REMARKS, ITEMDESC, ITEMVALUE, " & vbCrLf & " TOTSTAMT, TOTCHARGES, TOTEDAMOUNT, " & vbCrLf & " TOTEXPAMT, NETVALUE, TOTQTY, "

            SqlStr = SqlStr & vbCrLf & " STFORMCODE, STFORMNAME, STFORMNO, STFORMDATE, " & vbCrLf & " STDUEFORMCODE, STDUEFORMNAME, STDUEFORMNO, STDUEFORMDATE,  " & vbCrLf & " STTYPE, IsRegdNo,LSTCST, WITHFORM, FOC, PRINTED," & vbCrLf & " CANCELLED, NARRATION,  " & vbCrLf & " STPERCENT, TOTFREIGHT, EDPERCENT, TOTTAXABLEAMOUNT, " & vbCrLf & " TOTSURCHARGEAMT, TOTDISCAMOUNT, TOTMSCAMOUNT, TotRO,REJECTION,AGTD3, " & vbCrLf & " PACK_MAT_FLAG, CHALLAN_MADE,PRDDate, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE,ISSTOCKTRF,TCSPER, TCSAMOUNT,DNCNNO,DNCNDATE," & vbCrLf & " TOTEDUPERCENT,TOTEDUAMOUNT,TOTSERVICEPERCENT,TOTSERVICEAMOUNT,SERV_PROV," & vbCrLf & " SUPP_FROM_DATE, SUPP_TO_DATE, INTRATE, " & vbCrLf & " AGTCT3, CT_NO, CT3_DATE, ARE_NO, " & vbCrLf & " REF_DESP_TYPE, OUR_AUTO_KEY_SO, OUR_SO_DATE, "

            SqlStr = SqlStr & vbCrLf & " SHIPPING_NO, SHIPPING_DATE, " & vbCrLf & " ARE1_NO, ARE1_DATE, " & vbCrLf & " EXPBILLNO, EXPINV_DATE, TOT_EXPORTEXP,EXCHANGE_RATE, " & vbCrLf & " TOTEXCHANGEVALUE, ADV_LICENSE, DESP_LOCATION, NATURE," & vbCrLf & " TOTMRPVALUE, TAX_ON_MRP, ABATEMENT_PER, " & vbCrLf & " TOT_CUSTOMDUTY, TOT_CD_CESS, CD_PER, CD_CESS_PER, BUYER_CODE, CO_BUYER_CODE," & vbCrLf & " TOTSHECPERCENT, TOTSHECAMOUNT,UPDATE_FROM,ISDUTY_FORGONE, AGT_DUTYFREE_PUR," & vbCrLf & " DUTY_INCLUDED_ITEM, ED_PAYABLE, CESS_PAYABLE, SHEC_PAYABLE,DIV_CODE, " & vbCrLf & " AGTCT1, CT1_NO, CT1_DATE,AGT_Permission,CUST_ITEM_VALUE, " & vbCrLf & " NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT," & vbCrLf & " SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE,E_REFNO,INVOICESEQTYPE,SAC_CODE,GST_CLAIM_APP )"

            SqlStr = SqlStr & vbCrLf & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mCurRowNo & "," & Val(mTRNType) & ", '" & MainClass.AllowSingleQuote(txtBillNoPrefix.Text) & "', " & vbCrLf & " " & mAutoKeyNo & "," & mBillNoSeq & ", '" & MainClass.AllowSingleQuote(txtBillNoSuffix.Text) & "', '" & MainClass.AllowSingleQuote(mBillNo) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & TxtBillTm.Text & "','HH24:MI')," & vbCrLf & " " & mDespatchNo & ", TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '', ''," & vbCrLf & " '','','',TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " TO_DATE('" & TxtBillTm.Text & "','HH24:MI'),'" & mSuppCustCode & "','" & mAccountCode & "','', " & vbCrLf _
                & " 0, 0, '" & mAUTHSIGN & "', TO_DATE('" & VB6.Format(mAUTHDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '', '', '', '', " & vbCrLf & " '', '', '" & mFREIGHTCHARGES & "', " & vbCrLf & " '', '" & mEXEMPT_NOTIF_NO & "', " & vbCrLf & " '" & mBookCode & "', '" & mBookType & "', '" & mBookSubType & "', " & mSALETAXCODE & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '', " & mItemValue & ", " & vbCrLf & " " & mTOTSTAMT & ", " & mTOTCHARGES & ", " & mTotEDAmount & ", " & vbCrLf & " " & mTOTEXPAMT & ", " & mNETVALUE & ", " & mTotQty & ", " & vbCrLf & " " & mFormRecdCode & ", '','', '', " & vbCrLf & " " & mFormDueCode & ", '','', '', " & vbCrLf & " '" & mSTType & "','" & mIsRegdNo & "', '" & mLSTCST & "', " & vbCrLf & " '" & mWITHFORM & "', '" & mFOC & "', '" & mPRINTED & "', " & vbCrLf & " '" & mCancelled & "', '" & MainClass.AllowSingleQuote(txtNarration.Text) & "',  "

            SqlStr = SqlStr & vbCrLf & "" & mSTPERCENT & "," & mTOTFREIGHT & "," & mEDPERCENT & "," & mTOTTAXABLEAMOUNT & "," & vbCrLf & "" & mSURAmount & "," & mTotDiscount & "," & mMSC & "," & mRO & ",'" & mREJECTION & "','" & mD3 & "', " & vbCrLf & "'" & mPackMat & "','" & mChallanMade & "','', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & mStockTrf & "'," & vbCrLf & " " & mTCSPER & "," & mTCSAMOUNT & "," & vbCrLf & " -1," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mTotEDUPercent & ", " & mTotEDUAmount & "," & vbCrLf & " " & mTotServicePercent & "," & mTotServiceAmount & ",'" & MainClass.AllowSingleQuote(txtServProvided.Text) & "'," & vbCrLf _
                & " '', ''," & vbCrLf & " 0, '" & mCT3 & "', 0, TO_DATE('" & VB6.Format(mCT3Date, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  0," & vbCrLf & " 'X', -1, '', "

            SqlStr = SqlStr & vbCrLf & " '', '', " & vbCrLf & " '', '', " & vbCrLf & " '', ''," & vbCrLf & "0,0, " & vbCrLf & " 0, '', " & vbCrLf & " '', '" & MainClass.AllowSingleQuote(txtProcessNature.Text) & "'," & vbCrLf & " 0, '" & mTaxOnMRP & "', 0, " & vbCrLf & " 0 , 0, 0, 0, " & vbCrLf & " '', ''," & vbCrLf & " " & Val(CStr(mSHECPercent)) & ", " & Val(CStr(mSHECAmount)) & ",'N','" & mDutyForgone & "','" & mDutyFreePurchase & "', " & vbCrLf _
                & " '" & mDutyIncluded & "', 0, 0, 0," & mDivisionCode & "," & vbCrLf & " '" & mCT1 & "',0, TO_DATE('" & VB6.Format(mCT1Date, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mAgtPermission & "',0," & vbCrLf & " " & Val(lblTotCGSTAmount.Text) & "," & Val(lblTotSGSTAmount.Text) & "," & Val(lblTotIGSTAmount.Text) & "," & vbCrLf & " '" & mShippedToSame & "','" & mShippedToCode & "','" & Trim(txteRefNo.Text) & "'," & Val(lblInvoiceSeq.Text) & ",'" & txtSACCode.Text & "','" & pClaimApp & "')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE FIN_INVOICE_HDR SET TRNTYPE=" & Val(mTRNType) & ",AGT_Permission ='" & mAgtPermission & "'," & vbCrLf & " BILLNOPREFIX = '" & MainClass.AllowSingleQuote(txtBillNoPrefix.Text) & "', GST_CLAIM_APP='" & pClaimApp & "'," & vbCrLf & " BILLNOSEQ= " & mBillNoSeq & ", " & vbCrLf & " AUTO_KEY_INVOICE= " & mAutoKeyNo & ", " & vbCrLf & " BILLNOSUFFIX= '" & MainClass.AllowSingleQuote(txtBillNoSuffix.Text) & "'," & vbCrLf & " BILLNO= '" & MainClass.AllowSingleQuote(mBillNo) & "'," & vbCrLf & " INVOICE_DATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " PRDDate= ''," & vbCrLf & " INV_PREP_DATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " INV_PREP_TIME= TO_DATE('" & TxtBillTm.Text & "','HH24:MI')," & vbCrLf & " AMEND_NO= ''," & vbCrLf & " AMEND_DATE= ''," & vbCrLf & " AMEND_WEF_FROM= ''," & vbCrLf & " SUPP_CUST_CODE= '" & mSuppCustCode & "'," & vbCrLf & " ACCOUNTCODE= '" & mAccountCode & "'," & vbCrLf & " ST_38_NO= '',"

            SqlStr = SqlStr & vbCrLf & " AUTHSIGN= '" & mAUTHSIGN & "'," & vbCrLf & " AUTHDATE=  TO_DATE('" & VB6.Format(mAUTHDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf

            SqlStr = SqlStr & vbCrLf & " BOOKCODE= " & mBookCode & "," & vbCrLf & " BOOKTYPE= '" & mBookType & "'," & vbCrLf & " BOOKSUBTYPE= '" & mBookSubType & "'," & vbCrLf & " SALETAXCODE= " & mSALETAXCODE & "," & vbCrLf & " REMARKS= '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf & " ITEMVALUE= " & mItemValue & "," & vbCrLf & " TOTSTAMT= " & mTOTSTAMT & "," & vbCrLf & " TOTCHARGES= " & mTOTCHARGES & "," & vbCrLf & " TOTEDAMOUNT= " & mTotEDAmount & "," & vbCrLf & " TOTEXPAMT= " & mTOTEXPAMT & "," & vbCrLf & " NETVALUE= " & mNETVALUE & "," & vbCrLf & " TOTQTY= " & mTotQty & "," & vbCrLf & " STTYPE= '" & mSTType & "'," & vbCrLf & " REJECTION='" & mREJECTION & "'," & vbCrLf & " STFORMCODE= " & mFormRecdCode & "," & vbCrLf & " STFORMNAME= ''," & vbCrLf & " STFORMNO= ''," & vbCrLf & " STFORMDATE= ''," & vbCrLf & " STDUEFORMCODE= " & mFormDueCode & "," & vbCrLf & " STDUEFORMNAME= ''," & vbCrLf & " STDUEFORMNO= ''," & vbCrLf & " STDUEFORMDATE='',"


            SqlStr = SqlStr & vbCrLf & " STPERCENT=" & mSTPERCENT & "," & vbCrLf & " TOTFREIGHT=" & mTOTFREIGHT & "," & vbCrLf & " EDPERCENT=" & mEDPERCENT & ", TOTEDUPERCENT=" & mTotEDUPercent & ", " & vbCrLf & " TOTEDUAMOUNT=" & mTotEDUAmount & ", TOTTAXABLEAMOUNT=" & mTOTTAXABLEAMOUNT & "," & vbCrLf & " TOTSERVICEPERCENT=" & mTotServicePercent & ", TOTSERVICEAMOUNT=" & mTotServiceAmount & ", " & vbCrLf & " ISREGDNO= '" & mIsRegdNo & "', LSTCST= '" & mLSTCST & "', " & vbCrLf & " WITHFORM= '" & mWITHFORM & "'," & vbCrLf & " FOC= '" & mFOC & "'," & vbCrLf & " CANCELLED= '" & mCancelled & "'," & vbCrLf & " NARRATION= '" & MainClass.AllowSingleQuote(txtNarration.Text) & "'," & vbCrLf & " TOTSURCHARGEAMT=" & mSURAmount & ", " & vbCrLf & " TOTDISCAMOUNT=" & mTotDiscount & ", " & vbCrLf & " TOTMSCAMOUNT=" & mMSC & ", " & vbCrLf & " TotRO=" & mRO & ", " & vbCrLf & " AGTD3='" & mD3 & "', " & vbCrLf & " PACK_MAT_FLAG='" & mPackMat & "', " & vbCrLf & " CHALLAN_MADE='" & mChallanMade & "', " & vbCrLf & " ISSTOCKTRF='" & mStockTrf & "', " & vbCrLf & " TCSAMOUNT='" & mTCSAMOUNT & "', " & vbCrLf & " SERV_PROV='" & MainClass.AllowSingleQuote(txtServProvided.Text) & "', SAC_CODE='" & txtSACCode.Text & "',"

            SqlStr = SqlStr & vbCrLf & " TCSPER='" & mTCSPER & "', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),"

            SqlStr = SqlStr & vbCrLf & " NATURE='" & MainClass.AllowSingleQuote(txtProcessNature.Text) & "', " & vbCrLf & " UPDATE_FROM='N',ISDUTY_FORGONE='" & mDutyForgone & "',AGT_DUTYFREE_PUR='" & mDutyFreePurchase & "',"

            SqlStr = SqlStr & vbCrLf & " SHEC_PAYABLE=0, DIV_CODE=" & mDivisionCode & ", " & vbCrLf & " NETCGST_AMOUNT=" & Val(lblTotCGSTAmount.Text) & ", NETSGST_AMOUNT=" & Val(lblTotSGSTAmount.Text) & ", NETIGST_AMOUNT=" & Val(lblTotIGSTAmount.Text) & ", " & vbCrLf & " SHIPPED_TO_SAMEPARTY='" & mShippedToSame & "', SHIPPED_TO_PARTY_CODE='" & mShippedToCode & "', " & vbCrLf & " E_REFNO='" & Trim(txteRefNo.Text) & "', INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & ""

            SqlStr = SqlStr & vbCrLf & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        End If

        ''& " PRINTED= '" & mPRINTED & "'," & vbCrLf

        PubDBCn.Execute(SqlStr)


        If UpdateDetail1(mAutoKeyNo, Trim(txtBillNoPrefix.Text), mBillNoSeq, mBillNo, VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), mTRNType, mSuppCustCode, mAccountCode, mShippedToSame, mShippedToCode, mDivisionCode) = False Then GoTo ErrPart

        With SprdMain
            pCGSTClaimAmount = 0
            pSGSTClaimAmount = 0
            pIGSTClaimAmount = 0

            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColOType
                mOType = Trim(.Text)

                .Col = ColOMkey
                mOMkey = Trim(.Text)

                .Col = ColHSNCode
                mHSNCode = Trim(.Text)

                '            mGSTAPP = "N"
                '
                '            If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "GST_APP", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                '                mGSTAPP = MasterNo
                '            End If

                If pClaimApp = "Y" Then
                    .Col = ColCGSTAmount
                    pCGSTClaimAmount = pCGSTClaimAmount + Val(.Text)

                    .Col = ColSGSTAmount
                    pSGSTClaimAmount = pSGSTClaimAmount + Val(.Text)

                    .Col = ColIGSTAmount
                    pIGSTClaimAmount = pIGSTClaimAmount + Val(.Text)
                End If
            Next

        End With

        Dim mLocationID As String = GetDefaultLocation(mSuppCustCode)


        If mLocationID = "" Then
            If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_CITY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                mLocationID = MasterNo
            End If
        End If

        If SalePostTRN_RC_GST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mBillNo, (txtBillDate.Text), mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mNETVALUE)), False, pDueDate, False, (txtRemarks.Text), False, mConsingee, mTotServiceAmount, 0, Val(lblTotCGSTAmount.Text), Val(lblTotIGSTAmount.Text), Val(lblTotSGSTAmount.Text), pCGSTClaimAmount, pIGSTClaimAmount, pSGSTClaimAmount, ADDMode, mAddUser, mAddDate, Val(lblTotItemValue.Text), mDivisionCode, mLocationID) = False Then GoTo ErrPart

        '    PubDBCn.Execute "DELETE FROM FIN_POSTED_TRN WHERE MKey='" & UCase(LblMKey.text) & "' AND BookType='" & UCase(mBookType) & "' AND BookCode='" & UCase(ConRCSalesBookCode) & "'"

        If lblGSTClaim.Text = "Y" Then
            If RCPurchasePostTRNGST(PubDBCn, (LblMKey.Text), mCurRowNo, CStr(ConRCSalesBookCode), mBookType, mBookSubType, mBillNo, (txtBillDate.Text), mBillNo, (txtBillDate.Text), mSuppCustCode, False, (txtGSTClaimDate.Text), "REVERSE CHARGE CLAIM", "", (lblGSTClaim.Text), Val(CStr(pCGSTClaimAmount)), Val(CStr(pSGSTClaimAmount)), Val(CStr(pIGSTClaimAmount)), (txtBillDate.Text), ADDMode, mAddUser, VB6.Format(mAddDate, "DD/MM/YYYY"), mDivisionCode, mLocationID) = False Then GoTo ErrPart
        End If


        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        '    Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsSaleMain.Requery() ''.Refresh
        RsSaleDetail.Requery() ''.Refresh
        RsSaleTrading.Requery()
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
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

        If txtBillNo.Text = "00001" Then Exit Function

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

        SqlStr = "SELECT MAX(INVOICE_DATE)" & vbCrLf & " FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKCode = " & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' " & vbCrLf & " AND INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & " " & vbCrLf & " AND BillNoSeq<" & Val(CStr(pBillNoSeq)) & ""

        If mSeparateSeries = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDBNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(INVOICE_DATE)" & " FROM FIN_INVOICE_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKCode = " & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' " & vbCrLf & " AND INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & " " & vbCrLf & " AND BillNoSeq>" & Val(CStr(pBillNoSeq)) & ""

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


        SqlStr = ""

        xFYear = CInt(VB6.Format(RsCompany.Fields("Start_Date").Value, "YY"))
        mPrefix = CDbl(xFYear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & Val(lblInvoiceSeq.Text))


        'If RsCompany.Fields("FYEAR").Value >= 2020 Then
        '    mStartingSNo = CDbl(xFYear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & Val(lblInvoiceSeq.Text) & VB6.Format(pStartingSNo, "00000"))
        'Else
        '    mStartingSNo = CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & Val(lblInvoiceSeq.Text) & VB6.Format(pStartingSNo, "00000"))
        'End If
        mStartingSNo = pStartingSNo

        SqlStr = ""


        SqlStr = "SELECT Max(BILLNOSEQ)  FROM FIN_INVOICE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND BookType='" & mBookType & "'" ''& vbCrLf |            & " AND BookSubType  IN ( "

        If Trim(txtBillNoPrefix.Text) = "" Then
            SqlStr = SqlStr & vbCrLf & " AND (BILLNOPREFIX='' OR BILLNOPREFIX IS NULL)"
        Else
            SqlStr = SqlStr & vbCrLf & " AND BILLNOPREFIX='" & Trim(txtBillNoPrefix.Text) & "'"
        End If

        'SqlStr = SqlStr & vbCrLf & " AND INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & ""

        'SqlStr = SqlStr & vbCrLf & " AND INVOICE_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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

        'With RsSaleMainGen
        '    If .EOF = False Then
        '        If Not IsDBNull(.Fields(0).Value) Then
        '            mMaxValue = .Fields(0).Value
        '            mSeqNo = Mid(mMaxValue, 6, Len(mMaxValue) - 5) + 1
        '            'mNewSeqBillNo = .Fields(0).Value + 1
        '        Else
        '            mSeqNo = mStartingSNo
        '            'mNewSeqBillNo = mStartingSNo
        '        End If
        '    Else
        '        mSeqNo = mStartingSNo
        '        'mNewSeqBillNo = mStartingSNo
        '    End If
        'End With

        'mNewSeqBillNo = mPrefix & IIf(RsCompany.Fields("INVOICE_DIGIT").Value = 1, mSeqNo, Format(mSeqNo, "00000"))

        '    mNewSeqBillNo = ""

        ''& vb6.Format(IIf(IsNull(RsCompany!INVOICE_PREFIX), "", RsCompany!INVOICE_PREFIX), "00") & Val(lblInvoiceSeq.text)

        mNewSeqBillNo = mSeqNo      ''VB6.Format(mSeqNo, mFormat)

        AutoGenSeqBillNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateDetail1(ByRef pAutoKey As String, ByRef pBillPrefix As String, ByRef pBillSeqNo As Integer, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pTRNType As String, ByRef pSuppCustCode As String, ByRef pAccountCode As String, ByRef pShipToSameParty As String, ByRef pShipToSuppCustCode As String, ByRef pDivCode As Double) As Boolean

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
        Dim mOMkey As String
        Dim mOType As String
        Dim mAccountCode As String = ""

        mTotExicseableAmt = 0
        mTotSTableAmt = 0
        mTotCessableAmt = 0

        PubDBCn.Execute("Delete From FIN_INVOICE_DET Where Mkey='" & LblMKey.Text & "'")
        PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "' AND BOOKCODE='" & LblBookCode.Text & "'")

        mPOS = ""
        If pShipToSameParty = "N" Then
            If MainClass.ValidateWithMasterTable(pShipToSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                mState = MasterNo
                If MainClass.ValidateWithMasterTable(mState, "NAME", "STATE_CODE", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mPOS = MasterNo
                End If
            End If
        End If

        mIsSaleComp = "N"
        mIsSuppInv = "N"


        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColPartNo
                mPartNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDesc
                If (CDbl(lblInvoiceSeq.Text) = 8 Or CDbl(lblInvoiceSeq.Text) = 9) Then
                    mItemDesc = MainClass.AllowSingleQuote(.Text)
                Else
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemDesc = MasterNo
                        mItemDesc = MainClass.AllowSingleQuote(mItemDesc)
                    Else
                        mItemDesc = MainClass.AllowSingleQuote(.Text)
                    End If
                End If

                .Col = ColHSNCode
                mHSNCode = Trim(.Text)
                mRefNo = ""

                mJITCallNo = ""

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                mMRP = 0
                mTaxableMRP = 0
                mTaxableMRP = 0

                .Col = ColAmount
                mAmount = Val(.Text)

                mCustItemValue = 0

                mExicseableAmt = 0
                mCessableAmt = 0

                mServiceAmt = 0
                mCESSAmt = 0
                mSHECAmt = 0
                mSTableAmt = 0

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

                .Col = ColOType
                mOType = Trim(.Text)

                If mOType = "J" Then
                    mAccountCode = ""
                    If MainClass.ValidateWithMasterTable(mItemDesc, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                        mAccountCode = MasterNo
                    End If
                End If

                .Col = ColOMkey
                mOMkey = CStr(Val(.Text))

                SqlStr = ""

                If mQty > 0 And mAmount > 0 Then 'If (mItemCode <> "" Or mItemDesc <> "") And mQty > 0 Then
                    SqlStr = " INSERT INTO FIN_INVOICE_DET ( " & vbCrLf _
                        & " MKEY , AUTO_KEY_INVOICE, SUBROWNO, " & vbCrLf _
                        & " ITEM_CODE , ITEM_DESC, HSNCODE, CUSTOMER_PART_NO,ITEM_QTY, " & vbCrLf _
                        & " ITEM_UOM , ITEM_RATE, ITEM_AMT, GSTABLE_AMT," & vbCrLf _
                        & " ITEM_ED, ITEM_ST,ITEM_CESS,ITEM_SERVICE, " & vbCrLf _
                        & " COMPANY_CODE,ITEM_MRP,ITEM_SHEC,JIT_CALLNO, " & vbCrLf _
                        & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
                        & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, RCTYPE, O_MKEY " & vbCrLf & " ) "

                    SqlStr = SqlStr & vbCrLf & " VALUES ('" & LblMKey.Text & "'," & pAutoKey & ", " & I & ", " & vbCrLf & " '" & mItemCode & "','" & mItemDesc & "', '" & mHSNCode & "', '" & mPartNo & "'," & mQty & ", " & vbCrLf & " '" & mUnit & "'," & mRate & "," & mAmount & ", " & mTaxableAmount & "," & vbCrLf & " " & mExicseableAmt & "," & mSTableAmt & "," & mCESSAmt & "," & vbCrLf & " " & mServiceAmt & "," & RsCompany.Fields("COMPANY_CODE").Value & "," & mMRP & ", " & vbCrLf & " " & mSHECAmt & ",'" & mJITCallNo & "'," & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & "," & vbCrLf & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ",'" & mOType & "','" & mOMkey & "') "

                    PubDBCn.Execute(SqlStr)

                    If mOType = "M" Then
                        SqlStr = "UPDATE FIN_PURCHASE_HDR SET " & vbCrLf & " UPDATE_FROM='N'," & vbCrLf _
                            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MKEY = '" & mOMkey & "'"

                        PubDBCn.Execute(SqlStr)

                        SqlStr = " UPDATE FIN_PURCHASE_DET SET " & vbCrLf & " RCSALEBILLMKEY = '" & LblMKey.Text & "'," & vbCrLf & " SALEBILLNOPREFIX = '" & pBillPrefix & "'," & vbCrLf & " SALEBILLNOSEQ = " & Val(CStr(pBillSeqNo)) & "," & vbCrLf & " SALEBILL_NO = '" & pBillNo & "'," & vbCrLf _
                            & " SALEBILLDATE = TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MKEY = '" & mOMkey & "' AND HSNCODE='" & mHSNCode & "'"
                    Else
                        SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " AUTHORISED='Y', " & vbCrLf & " AUTHORISED_CODE='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                            & " AUTHORISED_DATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MKEY = '" & mOMkey & "'"

                        PubDBCn.Execute(SqlStr)

                        SqlStr = " UPDATE FIN_VOUCHER_DET SET " & vbCrLf & " SALEBILLNOPREFIX = '" & pBillPrefix & "'," & vbCrLf & " SALEBILLNOSEQ = " & Val(CStr(pBillSeqNo)) & "," & vbCrLf & " SALEBILL_NO = '" & pBillNo & "'," & vbCrLf _
                            & " SALEBILLDATE = TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " WHERE COMPANYCODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MKEY = '" & mOMkey & "' AND SAC='" & mHSNCode & "'"

                        If mAccountCode <> "" Then
                            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
                        End If
                    End If

                    PubDBCn.Execute(SqlStr)


                    If mCGSTAmount + mSGSTAmount + mIGSTAmount > 0 Then
                        mOBillNo = ""
                        mOBillDate = ""

                        mGoodsServices = IIf(CDbl(lblInvoiceSeq.Text) = 7, "G", "S")

                        If UpdateGSTTRN(PubDBCn, (LblMKey.Text), LblBookCode.Text, mBookType, mBookSubType, pBillNo, pBillDate, pBillNo, pBillDate, mOBillNo, mOBillDate, pSuppCustCode, pAccountCode, pShipToSameParty, pShipToSuppCustCode, I, mItemCode, mQty, mUnit, mRate, mAmount, mTaxableAmount, mMRP, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, pDivCode, mHSNCode, Trim(mItemDesc), mPOS, "N", IIf(CDbl(lblInvoiceSeq.Text) = 7, "G", "S"), mGoodsServices, "Y", "D", pBillDate, "N") = False Then GoTo UpdateDetail1

                    End If
                End If
            Next
        End With

        UpdateDetail1 = True
        UpdateDetail1 = UpdateSaleExp1
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
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim mDutyForgone As String

        PubDBCn.Execute("Delete From FIN_INVOICE_EXP Where Mkey='" & lblMkey.Text & "'")
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
                    SqlStr = "Insert Into  FIN_INVOICE_EXP (MKEY,SUBROWNO, " & vbCrLf & "EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DUTYFORGONE) " & vbCrLf & "Values ('" & lblMkey.Text & "'," & I & ", " & vbCrLf & "" & mExpCode & "," & mPercent & "," & mExpAmount & "," & mCalcOn & ",'" & mRO & "','" & mDutyForgone & "')"
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

        FieldsVarification = True


        '     SqlStr = SqlStr & vbCrLf & " INV_GENERATE_24_HOURS,INV_GENERATE_FROM_TM,INV_GENERATE_TO_TM"

        If CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then
            MsgBox("Bill Date Cann't be less than GST Applicable date.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        mInvPrefix = IIf(IsDbNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value)

        If mInvPrefix = "" Then
            MsgBox("Invoice Prefix is not Define, so cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
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

        'If MainClass.GetUserCanModify((txtBillDate.Text)) = False Then
        '    MsgBox("You Have Not Rights to Add or Modify back Voucher", MsgBoxStyle.Information)
        '    FieldsVarification = False
        '    Exit Function
        'End If


        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgInformation("Customer Name is not a Supplier or Customer Category. Cannot Save")
            If txtCustomer.Enabled = True Then txtCustomer.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mIsWithinState = IIf(IsDbNull(MasterNo), "N", MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mGSTRegd = IIf(IsDbNull(MasterNo), "N", MasterNo)
        End If



        mCompanyGSTNo = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerGSTNo = IIf(IsDbNull(MasterNo), "", MasterNo)
        End If


        If Trim(txtCustomer.Text) = "" Then
            MsgBox("Customer Cannot Be Blank", MsgBoxStyle.Information)
            ' txtCustomer.SetFocus
            FieldsVarification = False
            Exit Function
        End If

        If SprdMain.MaxRows <= 2 Then

        Else
            'If Trim(txtInwardVNo.Text) = "" Then
            '    If CDbl(Trim(lblNetAmount.Text)) >= 250000 Then
            '        MsgBox("Invoice Value Cann't be Greater Than 250000", MsgBoxStyle.Information)
            '        ' txtCustomer.SetFocus
            '        FieldsVarification = False
            '        Exit Function
            '    End If
            'End If
        End If


        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = False Then
            MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
            cboInvType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
            mBookSubType = MasterNo
        Else
            mBookSubType = CStr(-1)
        End If


        If CDbl(lblInvoiceSeq.Text) = 7 Then
            If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        End If
        If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function

        If PubUserID <> "G0416" Then
            If Val(txtGSTClaimNo.Text) > 0 Then
                MsgBox("GST Claim or Approval is taken, so cann't be save.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If cboClaimApp.SelectedIndex = -1 Or cboClaimApp.Text = "" Then
            MsgBox("Please Select GST Claim Status, so cann't be save.", MsgBoxStyle.Information)
            If cboClaimApp.Enabled = True Then cboClaimApp.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If CDbl(lblInvoiceSeq.Text) = 8 Then

            If Trim(txtSACCode.Text) = "" Then
                MsgBox("SAC Code is Blank. Please check Service.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If

            mHSNCode = ""
            If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                mHSNCode = Trim(IIf(IsDbNull(MasterNo), "", MasterNo))
            Else
                MsgBox("Invalid SAC Code.", MsgBoxStyle.Information)
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
                        'If MainClass.ValidateWithMasterTable(Trim(mItemCode), "ITEM_CODE", "HSN_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        '    mHSNMstCode = Trim(IIf(IsDbNull(MasterNo), "", MasterNo))
                        '    If mHSNMstCode <> Trim(mHSNCode) Then
                        '        MsgBox("Please Check HSN Code for Item Code : " & Trim(.Text))
                        '        FieldsVarification = False
                        '        Exit Function
                        '    End If
                        'End If
                    End If
                Next
            End With
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemDesc, "S", "Item Description Is Blank.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmInvoiceRCGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    Me.text = IIf(LblBookCode.text = ConSalesBookCode, "Invoice", "Excise Export Invoice")

        Me.Text = IIf(CDbl(lblInvoiceSeq.Text) = 7, "Reverse Charge - Goods", "Reverse Charge - Services")

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
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
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
        SqlStr = ""

        SqlStr = "SELECT FIN_INVTYPE_MST.NAME AS INVOICE_TYPE,BILLNOPREFIX,TO_CHAR(BILLNOSEQ),BILLNOSUFFIX, " & vbCrLf & " BILLNO,INVOICE_DATE  AS BILLDATE, TO_CHAR(INV_PREP_TIME,'HH24:MI') AS BILLTIME, " & vbCrLf & " AUTO_KEY_DESP AS DCNO, DCDATE AS DCDATE, " & vbCrLf & " CUST_PO_NO AS PONO, CUST_PO_DATE AS PODATE, " & vbCrLf & " REMOVAL_DATE AS REMOVAL_DATE, TO_CHAR(REMOVAL_TIME,'HH24:MI') AS REMOVAL_TIME, " & vbCrLf & " A.SUPP_CUST_NAME AS CUSTOMER, B.SUPP_CUST_NAME AS CREDIT_ACCOUNT, " & vbCrLf & " ITEMDESC, NETVALUE FROM " & vbCrLf & " FIN_INVOICE_HDR, FIN_INVTYPE_MST, FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_MST B " & vbCrLf & " WHERE " & vbCrLf & " FIN_INVOICE_HDR.COMPANY_CODE=FIN_INVTYPE_MST.COMPANY_CODE " & vbCrLf & " AND FIN_INVOICE_HDR.TRNTYPE=FIN_INVTYPE_MST.CODE " & vbCrLf & " AND FIN_INVOICE_HDR.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf & " AND FIN_INVOICE_HDR.SUPP_CUST_CODE=A.SUPP_CUST_CODE " & vbCrLf & " AND FIN_INVOICE_HDR.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND FIN_INVOICE_HDR.ACCOUNTCODE=B.SUPP_CUST_CODE "

        SqlStr = SqlStr & vbCrLf & " AND FIN_INVOICE_HDR.BOOKCODE=" & LblBookCode.Text & ""
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

        SqlStr = SqlStr & vbCrLf & " AND FIN_INVOICE_HDR.INVOICE_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND FIN_INVOICE_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " And FIN_INVOICE_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

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
            pShowCalc = False
            .Col = ColExpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColExpName, 18)
            .TypeEditMultiLine = False

            .Col = ColExpPercent
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = 0.0#
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

        mDivCode = -1
        If Trim(cboDivision.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivCode = Val(MasterNo)
            End If
        End If

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColOType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSaleDetail.Fields("RCTYPE").DefinedSize ''
            .set_ColWidth(.Col, 3)
            .ColHidden = False ' IIf(lblInvoiceSeq.text = 8, True, False)

            .Col = ColOMkey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSaleDetail.Fields("O_MKEY").DefinedSize ''
            .set_ColWidth(.Col, 5.5)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 8, True, False)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            '.TypeEditLen = RsSaleDetail.Fields("O_MKEY").DefinedSize ''
            .set_ColWidth(.Col, 8)
            '.ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 8, True, False)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            '.TypeEditLen = RsSaleDetail.Fields("O_MKEY").DefinedSize ''
            .set_ColWidth(.Col, 8)
            '.ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 8, True, False)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSaleDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(.Col, 5.5)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 8, True, False)

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 20 '' RsSaleDetail.Fields("O_MKEY").DefinedSize ''
            .set_ColWidth(.Col, 12)
            '.ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 8, True, False)

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10 ''RsSaleDetail.Fields("O_MKEY").DefinedSize ''
            .set_ColWidth(.Col, 8)
            '.ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 8, True, False)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 100  ''RsSaleDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(.Col, 20)
            '.ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 8, True, False)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("CUSTOMER_PART_NO").DefinedSize
            .ColsFrozen = ColPartNo
            .set_ColWidth(.Col, 10)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 8, True, False)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("Item_Desc").DefinedSize ''
            .set_ColWidth(.Col, 20)


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
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 3, True, False)

            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSaleDetail.Fields("SGST_PER").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 3, True, False)

            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSaleDetail.Fields("IGST_PER").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 3, True, False)

            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 3, True, False)

            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 3, True, False)

            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(CDbl(lblInvoiceSeq.Text) = 3, True, False)

            .ColsFrozen = ColHSNCode

        End With


        If mDivCode = 6 Then
            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColAmount)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemCode)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColQty)
        Else
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColQty)
        End If

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColOType, ColOMkey)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColTaxableAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColIGSTAmount)


        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub

ERR1:
        If Err.Number = -2147418113 Then RsSaleDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsSaleMain

            txtBillNoPrefix.Maxlength = .Fields("BillNoPrefix").DefinedSize ''
            txtBillNo.Maxlength = .Fields("AUTO_KEY_INVOICE").Precision ''
            txtBillNoSuffix.Maxlength = .Fields("BillNoSuffix").DefinedSize ''
            txtBillDate.Maxlength = 10
            TxtBillTm.Maxlength = 5

            txtCustomer.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtCreditAccount.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

            txtRemarks.Maxlength = .Fields("Remarks").DefinedSize ''
            txtNarration.Maxlength = .Fields("NARRATION").DefinedSize ''

            txteRefNo.Maxlength = .Fields("E_REFNO").DefinedSize

            txtServProvided.Maxlength = .Fields("SERV_PROV").DefinedSize
            txtProcessNature.Maxlength = .Fields("NATURE").DefinedSize

            txtShippedTo.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        End With

        Exit Sub
ERR1:
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
        Dim mClaimApp As String

        pShowCalc = False
        With RsSaleMain
            If Not .EOF Then

                lblMkey.Text = .Fields("mKey").Value

                ''***
                If MainClass.ValidateWithMasterTable(.Fields("TRNTYPE").Value, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                    cboInvType.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "INV_HEADING", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                    lblInvHeading.Text = IIf(IsDbNull(MasterNo), "", MasterNo)
                End If

                If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") Then
                    mBookSubType = MasterNo
                Else
                    mBookSubType = CStr(-1)
                End If


                txtBillNoPrefix.Text = IIf(IsDbNull(.Fields("BILLNOPREFIX").Value), "", .Fields("BILLNOPREFIX").Value)
                txtBillNo.Text = IIf(IsDBNull(.Fields("BILLNOSEQ").Value), "", .Fields("BILLNOSEQ").Value)  '', "00000000")
                txtBillNoSuffix.Text = IIf(IsDbNull(.Fields("BILLNOSUFFIX").Value), "", .Fields("BILLNOSUFFIX").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                '            txtProddate = Format(IIf(IsNull(!PRDDate), "", !PRDDate), "DD/MM/YYYY")
                TxtBillTm.Text = VB6.Format(IIf(IsDbNull(.Fields("INV_PREP_TIME").Value), "", .Fields("INV_PREP_TIME").Value), "HH:MM")


                If MainClass.ValidateWithMasterTable(.Fields("SUPP_CUST_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustomer.Text = MasterNo
                End If

                mCustomerCode = .Fields("SUPP_CUST_CODE").Value

                chkShipTo.CheckState = IIf(.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mShippedToCode = IIf(IsDbNull(.Fields("SHIPPED_TO_PARTY_CODE").Value), -1, .Fields("SHIPPED_TO_PARTY_CODE").Value)
                mShippedToName = ""
                If MainClass.ValidateWithMasterTable(mShippedToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mShippedToName = MasterNo
                End If

                txtShippedTo.Text = mShippedToName




                If MainClass.ValidateWithMasterTable(.Fields("ACCOUNTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCreditAccount.Text = MasterNo
                End If


                lblTotQty.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.000")
                lblTotItemValue.Text = VB6.Format(IIf(IsDbNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                lblNetAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")
                txtServProvided.Text = "" 'IIf(IsNull(!SERV_PROV), "", !SERV_PROV)

                txtSACCode.Text = IIf(IsDbNull(.Fields("SAC_CODE").Value), "", .Fields("SAC_CODE").Value)

                If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    txtServProvided.Text = MasterNo
                End If


                txtServProvided.Enabled = False

                txtRemarks.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtNarration.Text = IIf(IsDbNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)

                txteRefNo.Text = IIf(IsDbNull(.Fields("E_REFNO").Value), "", .Fields("E_REFNO").Value)

                txtProcessNature.Text = IIf(IsDbNull(.Fields("NATURE").Value), "", .Fields("NATURE").Value)

                txtGSTClaimNo.Text = IIf(IsDbNull(.Fields("GST_CLAIM_RC_NO").Value), "", .Fields("GST_CLAIM_RC_NO").Value)
                txtGSTClaimDate.Text = VB6.Format(IIf(IsDbNull(.Fields("GST_CLAIM_RC_DATE").Value), "", .Fields("GST_CLAIM_RC_DATE").Value), "DD/MM/YYYY")
                lblGSTClaim.Text = IIf(IsDbNull(.Fields("GST_RC_CLAIM").Value), "N", .Fields("GST_RC_CLAIM").Value)

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False

                mClaimApp = IIf(IsDbNull(.Fields("GST_CLAIM_APP").Value), "N", .Fields("GST_CLAIM_APP").Value)

                cboClaimApp.SelectedIndex = IIf(mClaimApp = "Y", 0, 1)
                cboClaimApp.Enabled = IIf(lblGSTClaim.Text = "N", True, False)


                mAddUser = IIf(IsDbNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                mAddDate = VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                mModUser = IIf(IsDbNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                mModDate = VB6.Format(IIf(IsDbNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")

                Dim mDespatchNo As Double = Val(.Fields("AUTO_KEY_DESP").Value)

                Call ShowSaleDetail1(mDespatchNo)
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
        txtInwardVNo.Enabled = False
        txtSACCode.Enabled = False
        cmdPopulate.Enabled = False '' IIf(PubUserID = "G0416", True, False)
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        pShowCalc = True
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        FormatSprdMain(-1)

        '    cboInvType.Enabled = IIf(XRIGHT = "AMDV", True, False)
        cboInvType.Enabled = MainClass.GetUserCanModify(txtBillDate.Text) 'IIf(PubUserLevel = 1 Or PubUserLevel = 2, True, False)

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
        SqlStr = "Select FIN_INVOICE_EXP.EXPCODE,FIN_INVOICE_EXP.EXPPERCENT, FIN_INVOICE_EXP.DUTYFORGONE," & vbCrLf & " FIN_INVOICE_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO " & vbCrLf & " From FIN_INVOICE_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_INVOICE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_INVOICE_EXP.Mkey='" & lblMkey.Text & "'"

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
                    If RsCompany.Fields("COMPANY_CODE").Value = 16 And RsCompany.Fields("FYEAR").Value = 2015 And RsSaleExp.Fields("Identification").Value = "VOD" And (Trim(txtBillNo.Text) = "00337" Or Trim(txtBillNo.Text) = "00336" Or Trim(txtBillNo.Text) = "00348") Then
                        .Text = "A"
                    Else
                        .Text = IIf(RsSaleExp.Fields("Add_Ded").Value = "A", "A", "D")
                    End If

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
        Dim mOType As String
        Dim OMKey As String
        Dim RsTemp As ADODB.Recordset = Nothing

        mDivCode = -1
        If Trim(cboDivision.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivCode = Val(MasterNo)
            End If
        End If

        '', INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, ID.CUSTOMER_PART_NO AS CUST_PART

        SqlStr = ""
        SqlStr = " SELECT ID.*" & vbCrLf & " FROM FIN_INVOICE_DET ID " & vbCrLf _
            & " Where ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.Mkey='" & LblMKey.Text & "' Order By SubRowNo" ''& vbCrLf |            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf |            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf |            & " Order By SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSaleDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I
                If CDbl(lblInvoiceSeq.Text) = 8 Then
                    SprdMain.Col = ColItemCode
                    mItemCode = "-1"
                    SprdMain.Text = "-1"

                    SprdMain.Col = ColItemDesc
                    mItemDesc = IIf(IsDbNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)
                    SprdMain.Text = mItemDesc

                Else
                    SprdMain.Col = ColItemCode
                    mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                    SprdMain.Text = mItemCode

                    If mItemCode = "-1" Or mItemCode = "" Then
                        SprdMain.Col = ColItemDesc
                        mItemDesc = IIf(IsDbNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)
                    Else
                        SprdMain.Col = ColItemDesc
                        MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                        mItemDesc = MasterNo
                        '                mItemDesc = IIf(IsNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)
                    End If

                    SprdMain.Text = mItemDesc ''IIf(IsNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)

                    SprdMain.Col = ColPartNo
                    MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    mPartNo = MasterNo
                    '            If mDivCode = 6 Then
                    '                mPartNo = IIf(IsNull(.Fields("CUST_PART").Value), "", .Fields("CUST_PART").Value)
                    '            Else
                    '                mPartNo = IIf(IsNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)
                    '            End If
                    '                mPartNo = IIf(IsNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)
                    SprdMain.Text = mPartNo ''IIf(IsNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)
                End If


                SprdMain.Col = ColHSNCode
                mHSNCode = IIf(IsDbNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value) ''GetHSNCode(mItemCode)
                SprdMain.Text = mHSNCode

                '            If lblInvoiceSeq.text = 8 And I = 1 Then
                '                txtSACCode.Text = mHSNCode
                '            End If

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_AMT").Value), 0, .Fields("ITEM_AMT").Value)))

                SprdMain.Col = ColTaxableAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("GSTABLE_AMT").Value), 0, .Fields("GSTABLE_AMT").Value)))

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))

                SprdMain.Col = ColCGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("CGST_AMOUNT").Value), 0, .Fields("CGST_AMOUNT").Value)))

                SprdMain.Col = ColSGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("SGST_AMOUNT").Value), 0, .Fields("SGST_AMOUNT").Value)))

                SprdMain.Col = ColIGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("IGST_AMOUNT").Value), 0, .Fields("IGST_AMOUNT").Value)))

                SprdMain.Col = ColOType
                SprdMain.Text = IIf(IsDbNull(.Fields("RCTYPE").Value), 0, .Fields("RCTYPE").Value)
                mOType = IIf(IsDbNull(.Fields("RCTYPE").Value), 0, .Fields("RCTYPE").Value)

                SprdMain.Col = ColOMkey
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("O_MKEY").Value), 0, .Fields("O_MKEY").Value)))
                OMKey = IIf(IsDbNull(.Fields("O_MKEY").Value), "", .Fields("O_MKEY").Value)

                If CDbl(lblInvoiceSeq.Text) = 8 And I = 1 Then
                    If mOType = "M" Then
                        SqlStr = " SELECT VNO, VDATE FROM FIN_PURCHASE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MKEY='" & OMKey & "'"
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                        If RsTemp.EOF = False Then
                            txtInwardVNo.Text = IIf(IsDbNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                            txtInwardVDate.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDate").Value), "", RsTemp.Fields("VDate").Value), "DD/MM/YYYY")
                        End If
                    Else
                        SqlStr = " SELECT VNO, VDATE FROM FIN_VOUCHER_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MKEY='" & OMKey & "'"
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                        If RsTemp.EOF = False Then
                            txtInwardVNo.Text = IIf(IsDbNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                            txtInwardVDate.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDate").Value), "", RsTemp.Fields("VDate").Value), "DD/MM/YYYY")
                        End If
                    End If
                End If


                If mOType = "M" Then
                    SqlStr = " SELECT BILLNO, INVOICE_DATE, VNO, VDATE, SUPP_CUST_NAME FROM FIN_PURCHASE_HDR IH, FIN_SUPP_CUST_MST CMST" & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " And IH.MKEY='" & OMKey & "'" & vbCrLf _
                        & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        SprdMain.Col = ColBillNo
                        SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)

                        SprdMain.Col = ColBillDate
                        SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")

                        SprdMain.Col = ColVNo
                        SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)

                        SprdMain.Col = ColVDate
                        SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")

                        SprdMain.Col = ColPartyName
                        SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    End If
                Else
                    SqlStr = " SELECT VNO, VDATE, SUPP_CUST_NAME FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID, FIN_SUPP_CUST_MST CMST" & vbCrLf _
                        & " WHERE IH.MKEY=ID.MKEY AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " And IH.MKEY='" & OMKey & "'" & vbCrLf _
                        & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                        & " AND ID.ACCOUNTCODE=CMST.SUPP_CUST_CODE"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        SprdMain.Col = ColBillNo
                        SprdMain.Text = ""

                        SprdMain.Col = ColBillDate
                        SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")

                        SprdMain.Col = ColVNo
                        SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)

                        SprdMain.Col = ColVDate
                        SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")

                        SprdMain.Col = ColPartyName
                        SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    End If
                End If
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
            MainClass.ClearGrid(SprdView)
            AssignGrid((True))
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

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc

                .Col = ColItemDesc
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

                .Col = ColItemDesc
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
                    mTaxableAmount = mItemAmount
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


                mCGSTAmount = CDbl(VB6.Format(mTaxableAmount * mCGSTPer * 0.01, "0.00"))
                mSGSTAmount = CDbl(VB6.Format(mTaxableAmount * mSGSTPer * 0.01, "0.00"))
                mIGSTAmount = CDbl(VB6.Format(mTaxableAmount * mIGSTPer * 0.01, "0.00"))


                .Col = ColCGSTAmount
                .Text = VB6.Format(mCGSTAmount, "0.00")

                .Col = ColSGSTAmount
                .Text = VB6.Format(mSGSTAmount, "0.00")

                .Col = ColIGSTAmount
                .Text = VB6.Format(mIGSTAmount, "0.00")

                mTotCGST = mTotCGST + CDbl(VB6.Format(mCGSTAmount, "0.00"))
                mTotSGST = mTotSGST + CDbl(VB6.Format(mSGSTAmount, "0.00"))
                mTotIGST = mTotIGST + CDbl(VB6.Format(mIGSTAmount, "0.00"))


DontCalc1:
            Next I
        End With



        mNetAccessAmt = mTaxableAmount

        Call BillExpensesCalcTots_GST(SprdExp, (txtBillDate.Text), mNetAccessAmt, mTotItemAmount, mTaxableAmount, 0, 0, 0, mTotIGST, mTotSGST, mTotCGST, pTotExportExp, 0, 0, pTotOthers, pTotCustomDutyExport, pTotCustomDuty, pTotMSC, pTotDiscount, 0, pTotRO, pTotTCS, mTotExp, pTCSPer, "S")


        lblTotItemValue.Text = VB6.Format(mTotItemAmount, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(mTotTaxableItemAmount, "#0.00")
        lblTotCGSTAmount.Text = VB6.Format(mTotCGST, "#0.00")
        lblTotSGSTAmount.Text = VB6.Format(mTotSGST, "#0.00")
        lblTotIGSTAmount.Text = VB6.Format(mTotIGST, "#0.00")
        lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")

        lblTotQty.Text = VB6.Format(mTotQty, "#0.000")

        lblNetAmount.Text = VB6.Format(mTotExp + mTotItemAmount + mTotCGST + mTotSGST + mTotIGST, "#0.00")



        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub Clear1()

        lblMkey.Text = ""


        mCustomerCode = IIf(IsDbNull(RsCompany.Fields("COMPANY_ACCTCODE").Value), -1, RsCompany.Fields("COMPANY_ACCTCODE").Value)

        If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCustomer.Text = MasterNo
        Else
            txtCustomer.Text = ""
        End If


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

        txtInwardVNo.Enabled = True
        txtSACCode.Enabled = True

        mAddUser = ""
        mAddDate = ""
        mModUser = ""
        mModDate = ""

        cboInvType.SelectedIndex = -1

        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        cboClaimApp.SelectedIndex = -1
        cboClaimApp.Enabled = True


        '    txtBillNoPrefix.Text = IIf(LblBookCode.text = ConSalesBookCode, "S", "EXP")
        'txtBillNoPrefix.Text = "S" ''& vb6.Format(IIf(IsNull(RsCompany!INVOICE_PREFIX), "", RsCompany!INVOICE_PREFIX), "00") & Val(lblInvoiceSeq.text)

        txtBillNoPrefix.Text = GetDocumentPrefix("8", lblInvoiceSeq.Text, cboDivision.Text)

        txtBillNo.Text = ""
        txtBillNoSuffix.Text = ""
        txtBillDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        TxtBillTm.Text = GetServerTime
        TxtBillTm.Enabled = False

        txtCreditAccount.Text = ""

        lblInvHeading.Text = ""


        chkShipTo.Enabled = True
        chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        txtShippedTo.Enabled = False
        cmdSearchShippedTo.Enabled = False

        lblTotQty.Text = "0.000"
        lblTotItemValue.Text = "0.00"
        lblTotCGSTAmount.Text = "0.00"
        lblTotSGSTAmount.Text = "0.00"
        lblTotIGSTAmount.Text = "0.00"

        lblNetAmount.Text = "0.00"
        txtRemarks.Text = ""
        txtNarration.Text = ""


        txteRefNo.Text = ""
        txtSACCode.Text = ""
        txtInwardVNo.Text = ""
        txtInwardVDate.Text = ""
        txtServProvided.Text = ""
        txtServProvided.Enabled = IIf(CDbl(lblInvoiceSeq.Text) = 8, True, False)


        lblTotItemValue.Text = VB6.Format(0, "#0.00")
        lblNetAmount.Text = VB6.Format(0, "#0.00")
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")

        txtGSTClaimNo.Text = ""
        txtGSTClaimDate.Text = ""
        lblGSTClaim.Text = "N"

        txtProcessNature.Text = ""


        cmdPopulate.Enabled = True

        TabMain.SelectedIndex = 0


        lblTotTaxableAmt.Text = CStr(0)

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        Call FillSprdExp()
        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

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
            SqlStr = " SELECT ACM.SUPP_CUST_NAME, " & vbCrLf & " ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))) AS AMOUNT, " & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<=0 THEN 'Cr' ELSE 'Dr' END AS DC "

            SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE  " & vbCrLf & " TRN.Company_Code=ACM.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "

            SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE ='" & mBookType & "'" & vbCrLf & " AND BOOKSUBTYPE = '" & mBookSubType & "'" & vbCrLf & " AND TRN.MKEY='" & lblMkey.Text & "'"

            SqlStr = SqlStr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME"

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
        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = IIf(MasterNo = "Y", "L", "C")
            Else
                mLocal = ""
            End If

            If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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
                    SprdExp.Text = Str(IIf(IsDbNull(RS.Fields("DefaultPercent").Value), 0, Str(RS.Fields("DefaultPercent").Value)))
                Else
                    SprdExp.Text = ""
                End If

                SprdExp.Col = ColExpAmt
                SprdExp.Text = "0"

                SprdExp.Col = ColExpSTCode
                SprdExp.Text = CStr(Val(IIf(IsDbNull(RS.Fields("CODE").Value), -1, RS.Fields("CODE").Value)))

                mIdentification = IIf(IsDbNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)

                SprdExp.Col = ColExpAddDeduct

                If RsCompany.Fields("COMPANY_CODE").Value = 16 And RsCompany.Fields("FYEAR").Value = 2015 And mIdentification = "VOD" And (Trim(txtBillNo.Text) = "00337" Or Trim(txtBillNo.Text) = "00336" Or Trim(txtBillNo.Text) = "00348") Then
                    SprdExp.Text = "A"
                Else
                    SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")
                End If

                SprdExp.Col = ColExpIdent
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)


                If mIdentification = "BCD" Then mIsBCD = True

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

        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = IIf(MasterNo = "Y", "L", "C")
            Else
                mLocal = ""
            End If
        Else
            mLocal = ""
        End If


        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            xAcctCode = "-1"
        End If

        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
            xTrnCode = MasterNo
        Else
            xTrnCode = CDbl("-1")
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

            I = 0
            Do While Not RS.EOF
                I = I + 1

                SprdExp.Row = I

                SprdExp.Col = ColExpName
                SprdExp.Text = RS.Fields("Name").Value

                SprdExp.Col = ColExpPercent

                SprdExp.Text = Str(IIf(IsDbNull(RS.Fields("Percent").Value), 0, Str(RS.Fields("Percent").Value)))

                SprdExp.Col = ColExpAmt
                SprdExp.Text = "0"

                SprdExp.Col = ColExpSTCode
                SprdExp.Text = CStr(Val(IIf(IsDbNull(RS.Fields("Code").Value), -1, RS.Fields("Code").Value)))

                mRO = IIf(IsDbNull(RS.Fields("RO").Value), "N", RS.Fields("RO").Value)

                SprdExp.Col = ColRO
                SprdExp.Value = IIf(mRO = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mIdentification = IIf(IsDbNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)

                SprdExp.Col = ColExpAddDeduct
                If RsCompany.Fields("COMPANY_CODE").Value = 16 And RsCompany.Fields("FYEAR").Value = 2015 And mIdentification = "VOD" And (Trim(txtBillNo.Text) = "00337" Or Trim(txtBillNo.Text) = "00336" Or Trim(txtBillNo.Text) = "00348") Then
                    SprdExp.Text = "A"
                Else
                    SprdExp.Text = IIf(RS.Fields("ADD_DED").Value = "A", "A", "D")
                End If



                SprdExp.Col = ColExpIdent
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)
                If SprdExp.Text = "DAM" Then MainClass.ProtectCell(SprdExp, I, I, 1, SprdExp.MaxCols)

                SprdExp.Col = ColTaxable
                SprdExp.Text = IIf(IsDbNull(RS.Fields("TAXABLE").Value), "N", RS.Fields("TAXABLE").Value)

                SprdExp.Col = ColExciseable
                SprdExp.Text = IIf(IsDbNull(RS.Fields("EXCISEABLE").Value), "N", RS.Fields("EXCISEABLE").Value)

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
        Call CalcTots()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume
    End Sub
    Private Sub FrmInvoiceRCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmInvoiceRCGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub

    Public Sub FrmInvoiceRCGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)


        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7755) '8000
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900

        TabMain.SelectedIndex = 0

        cboClaimApp.Items.Clear()
        cboClaimApp.Items.Add("Yes")
        cboClaimApp.Items.Add("No")
        cboClaimApp.SelectedIndex = -1

        mAuthorised = IIf(InStr(1, XRIGHT, "S") > 0, True, False)

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

        AdoDCMain.Visible = False

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
                            Call CalcTots()
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

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))

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

        If MainClass.SearchGridMaster((txtSACCode.Text), "GEN_HSN_MST", "HSN_CODE", "HSN_DESC", , , SqlStr) = True Then
            txtSACCode.Text = AcName
            txtSACCode_Validating(txtSACCode, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtSACCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSACCode.DoubleClick
        SearchProvidedMaster()
    End Sub

    Private Sub txtSACCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSACCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSACCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSACCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSACCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchProvidedMaster()
    End Sub

    Private Sub txtSACCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSACCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtSACCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then
            MsgInformation("Please Select Valid Service Provided")
            Cancel = True
        Else
            txtServProvided.Text = MasterNo
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub FillCboSaleType()

        On Error GoTo ErrPart
        Dim RsSaleType As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        cboInvType.Items.Clear()

        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='S' "

        If CDbl(lblInvoiceSeq.Text) = 7 Then
            SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION='G'"
        ElseIf CDbl(lblInvoiceSeq.Text) = 8 Then
            SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION='S'"
        End If

        '    If LblBookCode = ConExportSalesBookCode Then
        '        SqlStr = SqlStr & vbCrLf & " AND ISEXPORT='Y'"
        '    End If

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
End Class
