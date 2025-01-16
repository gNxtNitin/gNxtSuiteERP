Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Module FiFoItemRate
    Public Function GetLatestFIFORate(ByRef pCompanyCode As String, ByRef pItemCode As String, ByRef pItemUOM As String, ByRef pTotClosing As Double,
                                             ByRef pAsOnDate As String, ByRef pCostType As String, Optional ByRef pStockType As String = "",
                                             Optional ByRef pDeptCode As String = "", Optional ByRef mCheckInHouse As String = "",
                                             Optional ByRef pBalanceType As String = "", Optional ByRef pStockID As String = "", Optional ByRef pPartyCode As String = "") As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTemp1 As ADODB.Recordset = Nothing
        Dim mRunningBal As Double
        Dim mApprovedQty As Double
        Dim mQtyValue As Double
        Dim mCalcQty As Double
        Dim pRefDate As String
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim xPurchaseCost As Double
        Dim xLandedCost As Double
        Dim xSaleCost As Double
        Dim mCategory As String
        Dim mStockType As String = ""
        Dim mItemPurchaseCost As Double
        Dim mItemSalesCost As Double
        Dim mMKey As String
        Dim pINHouseRate As Double
        Dim mMainItemCode As String
        Dim mFYOPQty As Double
        Dim mCLQty As Double
        Dim mFYOPAmount As Double
        Dim mCLAmount As Double
        Dim mPurchaseQty As Double
        Dim mPurchaseValue As Double
        Dim mConsumption As Double
        Dim mItemRate As Double
        Dim mNetQty As Double
        Dim mNETVALUE As Double
        GetLatestFIFORate = 0
        If Trim(pItemCode) = "" Then '
            Exit Function
        End If
        If pTotClosing <= 0 Then
            GetLatestFIFORate = 0
            Exit Function
        End If
        mMainItemCode = GetCompanyMainItemCode(pCompanyCode, pItemCode)
        mFactor = 1

        SqlStr = "SELECT A.ISSUE_UOM, A.PURCHASE_UOM, A.UOM_FACTOR,A.PURCHASE_COST,A.ITEM_STD_COST, A.CATEGORY_CODE, B.RATE " & vbCrLf _
            & " FROM INV_ITEM_MST A, INV_ITEM_RATE_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.ITEM_CODE=B.ITEM_CODE" & vbCrLf _
            & " AND A.ITEM_CODE='" & Trim(mMainItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp1.EOF = False Then
            mIssueUOM = IIf(IsDBNull(RsTemp1.Fields("ISSUE_UOM").Value), "", RsTemp1.Fields("ISSUE_UOM").Value)
            mPurchaseUOM = IIf(IsDBNull(RsTemp1.Fields("PURCHASE_UOM").Value), "", RsTemp1.Fields("PURCHASE_UOM").Value)
            mFactor = IIf(IsDBNull(RsTemp1.Fields("UOM_FACTOR").Value) Or RsTemp1.Fields("UOM_FACTOR").Value = 0, 1, RsTemp1.Fields("UOM_FACTOR").Value)
            mCategory = IIf(IsDBNull(RsTemp1.Fields("CATEGORY_CODE").Value), "", RsTemp1.Fields("CATEGORY_CODE").Value)
            mItemPurchaseCost = IIf(IsDBNull(RsTemp1.Fields("RATE").Value), 0, RsTemp1.Fields("RATE").Value)
            mItemSalesCost = IIf(IsDBNull(RsTemp1.Fields("RATE").Value), 0, RsTemp1.Fields("RATE").Value)
            If mPurchaseUOM <> mIssueUOM Then
                If mFactor <> 0 Then
                    mItemPurchaseCost = mItemPurchaseCost / mFactor
                    mItemSalesCost = mItemSalesCost / mFactor
                End If
            End If
        End If

        mStockType = GetStockType(PubDBCn, mMainItemCode, 1)
        If mStockType = "CS" Then
            GetLatestFIFORate = CDbl(VB6.Format(pTotClosing * mItemSalesCost, "0.0000"))
        ElseIf CheckCompanyItemBom(pCompanyCode, mMainItemCode) = True Or pCostType = "S" Then ''IsProductionItem(mMainItemCode) = True Then'If CheckItemBom(mRMCode) = True Then
            If pStockType = "FG" Or pCostType = "S" Then
                SqlStr = "SELECT ITEM_PRICE " & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf _
                    & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                    & " AND IH.COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & mMainItemCode & "' AND SO_APPROVED='Y'" & vbCrLf _
                    & " AND ID.AMEND_WEF= ( " & vbCrLf _
                    & " SELECT MAX(SD.AMEND_WEF) " & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR SH, DSP_SALEORDER_DET SD" & vbCrLf _
                    & " WHERE SH.MKEY=SD.MKEY" & vbCrLf _
                    & " AND SH.COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
                    & " AND SD.ITEM_CODE='" & mMainItemCode & "' AND SO_APPROVED='Y'" & vbCrLf _
                    & " AND SD.AMEND_WEF<=TO_DATE('" & VB6.Format(pAsOnDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))"

                SqlStr = SqlStr & vbCrLf & " ORDER BY SO_STATUS DESC"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    xSaleCost = IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), "", RsTemp.Fields("ITEM_PRICE").Value)
                Else
                    xSaleCost = mItemSalesCost
                End If
            Else
                If pDeptCode = "" And pStockID <> "WH" Then
                    xSaleCost = GetSummCompanyMaterialCost(pCompanyCode, mMainItemCode, pStockType, pAsOnDate, pCostType, pTotClosing, pItemUOM, -1)
                Else
                    xSaleCost = GetCompanyMaterialCost(pCompanyCode, mMainItemCode, pStockType, pDeptCode, pAsOnDate, pCostType, pTotClosing)
                End If
            End If
            If xSaleCost = 0 Then GoTo NextRow
            GetLatestFIFORate = CDbl(VB6.Format(pTotClosing * xSaleCost, "0.0000"))
        Else
NextRow:
            If pCostType = "C" Then ''Get Data fom PO Only
                If GetPurchaseOrderRate(pCompanyCode, mMainItemCode, xPurchaseCost, xLandedCost, pAsOnDate, "ST", "", pItemUOM, mFactor) = False Then GoTo ErrPart
                If xPurchaseCost = 0 Then
                    mQtyValue = (pTotClosing * mItemPurchaseCost)
                Else
                    mQtyValue = (pTotClosing * xPurchaseCost)
                End If
                GetLatestFIFORate = mQtyValue
            Else
                SqlStr = "SELECT MKEY, AUTO_KEY_MRR, VNO, MRR_DATE,  " & vbCrLf _
                    & " ITEM_UOM, APPROVED_QTY, " & vbCrLf _
                    & " OTHERS, PORATE " & vbCrLf _
                    & " FROM ( "

                SqlStr = SqlStr & vbCrLf _
                    & " SELECT IH.MKEY, GH.AUTO_KEY_MRR, IH.VNO, GH.MRR_DATE, " & vbCrLf _
                    & " ID.ITEM_UOM, (ID.ITEM_QTY - ID.SHORTAGE_QTY - ID.REJECTED_QTY) AS APPROVED_QTY, " & vbCrLf _
                    & " CASE WHEN IH.ITEMVALUE=0 OR ID.ITEM_QTY=0 THEN 0 ELSE ((IH.TOTEXPAMT)*ID.ITEM_AMT/IH.ITEMVALUE)/ID.ITEM_QTY END AS OTHERS, " & vbCrLf _
                    & " (SELECT (((NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,4))) * EXCHANGERATE)" & vbCrLf _
                    & " FROM PUR_PURCHASE_HDR PH, PUR_PURCHASE_DET PD " & vbCrLf _
                    & " WHERE PH.COMPANY_CODE = PD.COMPANY_CODE AND PH.MKEY = PD.MKEY And PD.ITEM_CODE = ID.ITEM_CODE" & vbCrLf _
                    & " AND PD.MKEY =(SELECT MAX(SPH.MKEY) " & vbCrLf _
                    & " FROM PUR_PURCHASE_HDR SPH, PUR_PURCHASE_DET SPD" & vbCrLf _
                    & " WHERE SPH.MKEY = SPD.MKEY " & vbCrLf _
                    & " AND SPH.AUTO_KEY_PO = ID.CUST_REF_NO" & vbCrLf _
                    & " AND SPD.ITEM_CODE= ID.ITEM_CODE" & vbCrLf _
                    & " AND SPD.PO_WEF_DATE <= GH.MRR_DATE " & vbCrLf _
                    & " AND PO_STATUS='Y' AND PUR_TYPE='P')) AS PORATE" & vbCrLf _
                    & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, INV_GATE_HDR GH" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE = " & pCompanyCode & "" & vbCrLf _
                    & " AND IH.MKEY=ID.MKEY" & vbCrLf _
                    & " AND IH.COMPANY_CODE=GH.COMPANY_CODE" & vbCrLf _
                    & " AND IH.AUTO_KEY_MRR=GH.AUTO_KEY_MRR" & vbCrLf _
                    & " AND GH.REF_TYPE IN ('P')" & vbCrLf _
                    & " AND ITEM_CODE='" & mMainItemCode & "' AND PURCHASE_TYPE='G'" & vbCrLf _
                    & " AND GH.MRR_DATE<=TO_DATE('" & VB6.Format(pAsOnDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')"

                '' AND IH.PURCHASESEQTYPE<>3

                'SqlStr = SqlStr & " UNION " & vbCrLf _
                '    & " SELECT IH.MKEY, GH.AUTO_KEY_MRR, IH.VNO, GH.MRR_DATE, " & vbCrLf _
                '    & " ID.ITEM_UOM, (ID.ITEM_QTY - ID.SHORTAGE_QTY - ID.REJECTED_QTY) AS APPROVED_QTY, " & vbCrLf _
                '    & " CASE WHEN IH.ITEMVALUE=0 OR ID.ITEM_QTY=0 THEN 0 ELSE ((IH.TOTEXPAMT)*ID.ITEM_AMT/IH.ITEMVALUE)/ID.ITEM_QTY END AS OTHERS, " & vbCrLf _
                '    & " (SELECT (((NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,4))) * EXCHANGERATE)" & vbCrLf _
                '    & " FROM PUR_PURCHASE_HDR PH, PUR_PURCHASE_DET PD " & vbCrLf _
                '    & " WHERE PH.COMPANY_CODE = PD.COMPANY_CODE AND PH.MKEY = PD.MKEY And PD.ITEM_CODE = ID.ITEM_CODE" & vbCrLf _
                '    & " AND PD.MKEY =(SELECT MAX(SPH.MKEY) " & vbCrLf _
                '    & " FROM PUR_PURCHASE_HDR SPH, PUR_PURCHASE_DET SPD" & vbCrLf _
                '    & " WHERE SPH.MKEY = SPD.MKEY " & vbCrLf _
                '    & " AND SPH.AUTO_KEY_PO = ID.CUST_REF_NO" & vbCrLf _
                '    & " AND SPD.ITEM_CODE= ID.ITEM_CODE" & vbCrLf _
                '    & " AND SPD.PO_WEF_DATE <= GH.MRR_DATE " & vbCrLf _
                '    & " AND PO_STATUS='Y')) AS PORATE" & vbCrLf _
                '    & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, INV_GATE_HDR GH" & vbCrLf _
                '    & " WHERE IH.COMPANY_CODE = " & pCompanyCode & "" & vbCrLf _
                '    & " AND IH.MKEY=ID.MKEY" & vbCrLf _
                '    & " AND IH.COMPANY_CODE=GH.COMPANY_CODE" & vbCrLf _
                '    & " AND ID.MRRNO=GH.AUTO_KEY_MRR" & vbCrLf _
                '    & " AND GH.REF_TYPE IN ('P') AND IH.PURCHASESEQTYPE=3" & vbCrLf _
                '    & " AND ITEM_CODE='" & mMainItemCode & "'" & vbCrLf _
                '    & " AND GH.MRR_DATE<=TO_DATE('" & VB6.Format(pAsOnDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')"

                SqlStr = SqlStr & vbCrLf _
                    & " )" & vbCrLf _
                    & " ORDER BY MRR_DATE DESC, TO_NUMBER(SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)) DESC,TO_NUMBER(SUBSTR(AUTO_KEY_MRR,1,LENGTH(AUTO_KEY_MRR)-6)) DESC"


                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    mRunningBal = pTotClosing
                    Do While Not RsTemp.EOF
                        mApprovedQty = IIf(IsDBNull(RsTemp.Fields("APPROVED_QTY").Value), 0, RsTemp.Fields("APPROVED_QTY").Value)

                        If mApprovedQty <= 0 Then GoTo NextRec

                        pRefDate = IIf(IsDBNull(RsTemp.Fields("MRR_DATE").Value), "", RsTemp.Fields("MRR_DATE").Value)
                        xPurchaseCost = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("PORATE").Value), 0, RsTemp.Fields("PORATE").Value), "0.0000")) ''GetPurchasePORate(mMkey, pRefDate, mMainItemCode)''GetPurchasePORate(mMkey, pAsOnDate, mMainItemCode)

                        If xPurchaseCost = 0 Then GoTo NextRec ''xPurchaseCost = IIf(IsNull(RsTemp!ITEM_RATE), 0, RsTemp!ITEM_RATE)
                        If pItemUOM <> IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value) Then
                            mApprovedQty = mApprovedQty * mFactor
                            xPurchaseCost = xPurchaseCost / mFactor
                        End If
                        If mRunningBal <= mApprovedQty Then
                            mCalcQty = mRunningBal
                            mRunningBal = 0
                        ElseIf mRunningBal > mApprovedQty Then
                            mCalcQty = mApprovedQty
                            mRunningBal = mRunningBal - mApprovedQty
                        End If
                        If pCostType = "P" Then
                            mQtyValue = (mCalcQty * xPurchaseCost)
                        ElseIf pCostType = "L" Then
                            xLandedCost = xPurchaseCost
                            'xLandedCost = xLandedCost + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("OTHERS").Value), 0, RsTemp.Fields("OTHERS").Value / mFactor), "0.0000"))
                            mQtyValue = (mCalcQty * CDbl(VB6.Format(xLandedCost, "0.0000")))
                        End If
                        GetLatestFIFORate = GetLatestFIFORate + mQtyValue
                        If mRunningBal = 0 Then
                            Exit Do
                        End If
NextRec:
                        RsTemp.MoveNext()
                        If RsTemp.EOF = True Then
                            If mRunningBal > 0 Then
                                If pCostType = "P" Then
                                    mQtyValue = (mRunningBal * xPurchaseCost)
                                Else
                                    mQtyValue = (mRunningBal * xLandedCost)
                                End If
                                GetLatestFIFORate = GetLatestFIFORate + mQtyValue
                            End If
                        End If
                    Loop
                Else
                    If GetPurchaseOrderRate(pCompanyCode, mMainItemCode, xPurchaseCost, xLandedCost, pAsOnDate, "ST", "", pItemUOM, mFactor) = False Then GoTo ErrPart
                    If pCostType = "P" Then
                        If xPurchaseCost = 0 Then
                            mQtyValue = (pTotClosing * mItemPurchaseCost)
                        Else
                            mQtyValue = (pTotClosing * xPurchaseCost)
                        End If
                    ElseIf pCostType = "L" Then
                        If xLandedCost = 0 Then
                            mQtyValue = (pTotClosing * mItemPurchaseCost)
                        Else
                            mQtyValue = (pTotClosing * CDbl(VB6.Format(xLandedCost, "0.0000")))
                        End If
                    End If
                    GetLatestFIFORate = mQtyValue
                End If
            End If
        End If
        GetLatestFIFORate = IIf(GetLatestFIFORate = 0, mItemPurchaseCost * pTotClosing, GetLatestFIFORate)
        Exit Function
ErrPart:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Function GetPurchaseOrderRate(ByRef pCompanyCode As String, ByRef pItemCode As String, ByRef pPurchaseCost As Double,
                                         ByRef pLandedCost As Double, ByRef xRefDate As String, ByRef mStockType As String,
                                         ByRef mPartyCode As String, ByRef xItemUOM As String, ByRef pFactor As Double, Optional ByRef mOnlyJWRate As Boolean = False, Optional ByRef mPONo As Double = 0) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RSTempExp As ADODB.Recordset
        Dim mKey As String = ""
        Dim mNetAccessAmt As Double
        Dim mPurchaseUOM As String = ""
        Dim mExpAddDeduct As String = ""
        Dim mExpCode As Double
        Dim xStr As String = ""
        Dim mExpPercent As Double
        Dim mRoType As String = ""
        Dim mExp As Double
        Dim mTableName As String = ""
        Dim mItemIssueUOM As String = ""
        Dim mItemFactor As Double
        Dim mRMCost As Double
        Dim xPurchaseCost As Double
        Dim xLandedCost As Double
        Dim mIsGSTRefund As String = ""
        Dim mFreightCost As Double

        SqlStr = " Select IH.MKEY, (NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2)) PURCHASE_COST, EXCHANGERATE," & vbCrLf _
            & " ISGSTAPPLICABLE,ITEM_UOM,FREIGHT_COST,IH.PUR_TYPE, CGST_PER, SGST_PER, IGST_PER " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " And IH.MKEY=ID.MKEY And IH.PO_STATUS='Y' AND IH.PUR_TYPE IN ('P')"

        If mPartyCode <> "" And mPartyCode <> "-1" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & mPartyCode & "'"
        End If

        If mPONo > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_PO=" & Val(CStr(mPONo)) & ""
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND ID.MKEY = ( " & vbCrLf _
            & " SELECT MAX(SIH.MKEY) " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR SIH, PUR_PURCHASE_DET SID" & vbCrLf _
            & " WHERE SIH.COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND SIH.MKEY=SID.MKEY AND SIH.PO_STATUS='Y' AND SIH.PUR_TYPE IN ('P')"

        If mPartyCode <> "" And mPartyCode <> "-1" Then
            SqlStr = SqlStr & vbCrLf & " AND SIH.SUPP_CUST_CODE='" & mPartyCode & "'"
        End If

        If mPONo > 0 Then
            SqlStr = SqlStr & vbCrLf _
                & " AND SIH.AUTO_KEY_PO=" & Val(CStr(mPONo)) & ""
        Else
            SqlStr = SqlStr & vbCrLf _
                & " AND SIH.AUTO_KEY_PO= ( " & vbCrLf _
                & " SELECT MAX(GD.REF_AUTO_KEY_NO) " & vbCrLf _
                & " FROM INV_GATE_HDR GH, INV_GATE_DET GD" & vbCrLf _
                & " WHERE GH.COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
                & " AND GH.AUTO_KEY_MRR=GD.AUTO_KEY_MRR" & vbCrLf _
                & " AND GD.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                & " AND GH.REF_TYPE='P'"

            If mPartyCode <> "" And mPartyCode <> "-1" Then
                SqlStr = SqlStr & vbCrLf & " AND GH.SUPP_CUST_CODE='" & mPartyCode & "'"
            End If

            SqlStr = SqlStr & vbCrLf _
                & " AND GH.MRR_DATE = (" & vbCrLf _
                & " SELECT MAX(A.MRR_DATE) " & vbCrLf _
                & " FROM INV_GATE_HDR A, INV_GATE_DET B" & vbCrLf _
                & " WHERE A.COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
                & " AND A.AUTO_KEY_MRR=B.AUTO_KEY_MRR" & vbCrLf _
                & " AND B.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                & " AND A.REF_TYPE='P'"

            If mPartyCode <> "" And mPartyCode <> "-1" Then
                SqlStr = SqlStr & vbCrLf & " AND A.SUPP_CUST_CODE='" & mPartyCode & "'"
            End If

            SqlStr = SqlStr & vbCrLf _
                & " AND A.MRR_DATE<=TO_DATE('" & VB6.Format(xRefDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))" & vbCrLf _
                & " )"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND SID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND SID.PO_WEF_DATE <= TO_DATE('" & VB6.Format(xRefDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf _
            & " ORDER BY IH.PO_CLOSED, ID.PO_WEF_DATE DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mKey = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "-1", RsTemp.Fields("mKey").Value)
            xPurchaseCost = IIf(IsDBNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value) * IIf(IsDBNull(RsTemp.Fields("EXCHANGERATE").Value), 1, RsTemp.Fields("EXCHANGERATE").Value)
            mPurchaseUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), 0, RsTemp.Fields("ITEM_UOM").Value)
            mIsGSTRefund = IIf(IsDBNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "N", RsTemp.Fields("ISGSTAPPLICABLE").Value)
            mExpPercent = IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value) + IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value) + IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value)
            mFreightCost = IIf(IsDBNull(RsTemp.Fields("FREIGHT_COST").Value), 0, RsTemp.Fields("FREIGHT_COST").Value)
            xLandedCost = xPurchaseCost
            mExp = 0
            If mIsGSTRefund = "N" Then
                mExp = xLandedCost * mExpPercent * 0.01
            End If
            mExp = mExp + mFreightCost
            xLandedCost = xLandedCost + mExp
            If xItemUOM <> mPurchaseUOM Then
                If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "UOM_FACTOR", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & pCompanyCode & "") = True Then
                    pFactor = MasterNo
                Else
                    pFactor = 1
                End If
                xPurchaseCost = xPurchaseCost / pFactor
                xLandedCost = xLandedCost / pFactor
            End If
        Else
            'mRMCost = GetTotalSFCost(pItemCode, xRefDate, mPurchaseUOM, pFactor)
            'If mRMCost <> 0 Then
            '    xPurchaseCost = xPurchaseCost + mRMCost
            '    xLandedCost = xPurchaseCost
            'Else

            SqlStr = "SELECT A.PURCHASE_COST, A.PURCHASE_UOM, A.ISSUE_UOM, A.UOM_FACTOR, B.RATE " & vbCrLf _
                    & " FROM INV_ITEM_MST A, INV_ITEM_RATE_MST B " & vbCrLf _
                    & " WHERE A.COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
                    & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
                    & " AND A.ITEM_CODE=B.ITEM_CODE" & vbCrLf _
                    & " AND A.ITEM_CODE='" & Trim(pItemCode) & "'"

            'SqlStr = "SELECT PURCHASE_COST, PURCHASE_UOM, ISSUE_UOM,UOM_FACTOR " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND ITEM_CODE='" & Trim(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                xPurchaseCost = IIf(IsDBNull(RsTemp.Fields("RATE").Value), 0, RsTemp.Fields("RATE").Value)
                mItemIssueUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), 0, RsTemp.Fields("ISSUE_UOM").Value)
                mPurchaseUOM = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                xLandedCost = IIf(IsDBNull(RsTemp.Fields("RATE").Value), 0, RsTemp.Fields("RATE").Value)
                mItemFactor = IIf(IsDBNull(RsTemp.Fields("UOM_FACTOR").Value), 0, RsTemp.Fields("UOM_FACTOR").Value)
                If mItemIssueUOM <> mPurchaseUOM Then
                    xPurchaseCost = xPurchaseCost / mItemFactor
                    xLandedCost = xLandedCost / mItemFactor
                End If
            End If
            'End If
        End If
        pPurchaseCost = xPurchaseCost
        pLandedCost = xLandedCost
        GetPurchaseOrderRate = True
        Exit Function
ErrPart:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetPurchaseOrderRate = False
    End Function
    Public Function CheckCompanyItemBom(ByRef pCompanyCode As Long, ByRef pItemCode As String, Optional ByRef pCheckon As String = "") As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        CheckCompanyItemBom = False
        If pItemCode = "" Then
            Exit Function
        End If
        mSqlStr = "SELECT PRODUCT_CODE " & vbCrLf _
            & " FROM PRD_NEWBOM_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND STATUS='O' AND IS_BOP='N' AND IS_APPROVED='Y'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            CheckCompanyItemBom = True
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Function GetCompanyMainItemCode(ByRef pCompanyCode As Long, ByRef pItemCode As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        SqlStr = " Select ITEM_CODE FROM INV_ITEM_RELATIONSHIP_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " And REF_ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetCompanyMainItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), pItemCode, RsTemp.Fields("ITEM_CODE").Value))
        Else
            GetCompanyMainItemCode = Trim(pItemCode)
        End If
        Exit Function
ErrPart:
        GetCompanyMainItemCode = Trim(pItemCode)
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Function GetSummCompanyMaterialCost(ByRef pCompanyCode As Long, ByRef pItemCode As String, ByRef pStockType As String, ByRef pAsOnDate As String, ByRef pCostType As String, ByRef xClosingBal As Double, ByRef pItemUOM As String, ByRef mDivisionCode As Double) As Double
        On Error GoTo ErrPart
        Dim pSqlStr As String = ""
        Dim RsTempSeq As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim mBOMDeptCode As String = ""
        Dim mRMCode As String = ""
        Dim xItemUOM As String = ""
        Dim mDeptSeq As Integer
        Dim mStdQty As Double
        Dim mProdDeptSeq As Integer
        Dim mProdCost As Double
        Dim mOPRCode As String = ""
        Dim mOprSeq As Integer
        Dim mOutJob As String = ""
        Dim mRMType As String
        Dim pCLBal As Double
        Dim pStockDeptCode As String
        Dim xItemClosingBal As Double
        Dim xPendingClosingBal As Double
        GetSummCompanyMaterialCost = 0
        xPendingClosingBal = xClosingBal

        pSqlStr = "Select SERIAL_NO, DEPT_CODE " & vbCrLf _
            & " FROM PRD_PRODSEQUENCE_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " And PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND WEF = (" & vbCrLf _
            & " SELECT MAX(WEF) AS WEF FROM PRD_PRODSEQUENCE_DET" & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND " & vbCrLf _
            & " WEF <=TO_DATE('" & VB6.Format(pAsOnDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))" & vbCrLf _
            & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempSeq, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTempSeq.EOF = False Then
            Do While RsTempSeq.EOF = False
                mProdDeptSeq = IIf(IsDBNull(RsTempSeq.Fields("SERIAL_NO").Value), 0, RsTempSeq.Fields("SERIAL_NO").Value)
                pStockDeptCode = IIf(IsDBNull(RsTempSeq.Fields("DEPT_CODE").Value), "", RsTempSeq.Fields("DEPT_CODE").Value)
                xItemClosingBal = GetBalanceStockQty(pItemCode, pAsOnDate, pItemUOM, pStockDeptCode, pStockType, "", ConPH, mDivisionCode)
                If xItemClosingBal < 0 Then xItemClosingBal = 0
                If xItemClosingBal > xPendingClosingBal Then
                    xItemClosingBal = xPendingClosingBal
                End If
PendingClBal:
                xPendingClosingBal = xPendingClosingBal - xItemClosingBal
                If xItemClosingBal <> 0 Then
                    'mProdDeptSeq = GetProductSeqNo(pItemCode, pStockDeptCode, pAsOnDate)
                    If pStockType = "WP" Then
                        mProdDeptSeq = mProdDeptSeq - 1
                        pStockType = "ST"
                    End If
                    SqlStr = "SELECT ID.RM_CODE, ID.DEPT_CODE, OPR_CODE,(STD_QTY + GROSS_WT_SCRAP)/OUTPUT_QTY STD_QTY , 'B' BOM_TYPE" & vbCrLf _
                        & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf _
                        & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                        & " AND IH.COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
                        & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                        & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) " & vbCrLf _
                        & " FROM PRD_NEWBOM_HDR" & vbCrLf & " WHERE " & vbCrLf _
                        & " COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                        & " AND WEF<=TO_DATE('" & VB6.Format(pAsOnDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))"


                    SqlStr = SqlStr & vbCrLf _
                        & " AND ID.RM_CODE IN (" & vbCrLf _
                        & " SELECT ITEM_CODE FROM INV_ITEM_MST A, INV_GENERAL_MST B" & vbCrLf _
                        & " WHERE A.COMPANY_CODE='" & pCompanyCode & "'" & vbCrLf _
                        & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
                        & " AND A.CATEGORY_CODE=B.GEN_CODE" & vbCrLf _
                        & " AND B.PRD_TYPE IN ('R','B','I','D','P','2','3')" & vbCrLf _
                        & " AND A.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf _
                        & " AND A.ITEM_CODE=ID.RM_CODE )"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOM, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsBOM.EOF = False Then
                        Do While RsBOM.EOF = False
                            mBOMDeptCode = IIf(IsDBNull(RsBOM.Fields("DEPT_CODE").Value), "", RsBOM.Fields("DEPT_CODE").Value)
                            mStdQty = IIf(IsDBNull(RsBOM.Fields("STD_QTY").Value), 0, RsBOM.Fields("STD_QTY").Value)
                            mDeptSeq = GetProductSeqNo(pItemCode, mBOMDeptCode, pAsOnDate, pCompanyCode)
                            mRMCode = Trim(IIf(IsDBNull(RsBOM.Fields("RM_CODE").Value), "", RsBOM.Fields("RM_CODE").Value))
                            mRMType = GetProductionType(mRMCode)
                            If mRMType = "R" Or mRMType = "I" Or mRMType = "B" Or mRMType = "P" Or mRMType = "3" Or mRMType = "D" Then
                                mOPRCode = Trim(IIf(IsDBNull(RsBOM.Fields("OPR_CODE").Value), "", RsBOM.Fields("OPR_CODE").Value))
                            Else
                                GoTo NextRec ''For Paint..
                            End If
                            mOprSeq = GetOperationSeq(pItemCode, mBOMDeptCode, mOPRCode, pAsOnDate, pCompanyCode)
                            If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & pCompanyCode & "") = True Then
                                xItemUOM = MasterNo
                            End If
                            mOutJob = Trim(IIf(IsDBNull(RsBOM.Fields("BOM_TYPE").Value), "B", RsBOM.Fields("BOM_TYPE").Value))
                            If mOutJob = "B" Then
                                If xItemUOM = "KGS" Then
                                    mStdQty = mStdQty / 1000
                                ElseIf xItemUOM = "TON" Then
                                    mStdQty = mStdQty / 1000
                                    mStdQty = mStdQty / 1000
                                ElseIf xItemUOM = "MT" Then
                                    mStdQty = mStdQty / 1000
                                    mStdQty = mStdQty / 1000
                                End If
                            End If
                            pCLBal = xItemClosingBal * mStdQty
                            pCLBal = IIf(pCLBal <= 0, 1, pCLBal)
                            mProdCost = 0
                            If mDeptSeq = mProdDeptSeq Then
                                If pStockType <> "WP" Then
                                    If Val(pStockType) > 0 Then
                                        If Val(CStr(mOprSeq)) <= Val(pStockType) Then
                                            mProdCost = GetLatestFIFORate(pCompanyCode, mRMCode, xItemUOM, pCLBal, pAsOnDate, pCostType, "", mBOMDeptCode) ''mBOMDeptCode
                                        End If
                                    Else
                                        mProdCost = GetLatestFIFORate(pCompanyCode, mRMCode, xItemUOM, pCLBal, pAsOnDate, pCostType, "", mBOMDeptCode) ''mBOMDeptCode
                                    End If
                                End If
                            ElseIf mDeptSeq < mProdDeptSeq Then
                                mProdCost = GetLatestFIFORate(pCompanyCode, mRMCode, xItemUOM, pCLBal, pAsOnDate, pCostType, "", mBOMDeptCode) ''mBOMDeptCode
                            ElseIf mProdDeptSeq = 0 Then
                                mProdCost = GetLatestFIFORate(pCompanyCode, mRMCode, xItemUOM, pCLBal, pAsOnDate, pCostType, "", mBOMDeptCode) ''mBOMDeptCode
                            End If
                            GetSummCompanyMaterialCost = GetSummCompanyMaterialCost + mProdCost
NextRec:
                            RsBOM.MoveNext()
                        Loop
                    End If
                End If
                If xPendingClosingBal = 0 Then
                    Exit Do
                End If
                RsTempSeq.MoveNext()
            Loop
        End If
        If xPendingClosingBal > 0 Then
            xItemClosingBal = xPendingClosingBal
            mProdDeptSeq = 0
            pStockDeptCode = "STR"
            GoTo PendingClBal
        End If
        If xClosingBal = 0 Then
            'GetSummCompanyMaterialCost = GetSummCompanyMaterialCost / xClosingBal
        Else
            GetSummCompanyMaterialCost = GetSummCompanyMaterialCost / xClosingBal
        End If
        Exit Function
ErrPart:
        'Resume
        GetSummCompanyMaterialCost = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Function GetCompanyMaterialCost(ByRef pCompanyCode As Long, ByRef pItemCode As String, ByRef pStockType As String, ByRef pStockDeptCode As String, ByRef pAsOnDate As String, ByRef pCostType As String, ByRef xClosingBal As Double) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim mBOMDeptCode As String
        Dim mRMCode As String
        Dim xItemUOM As String = ""
        Dim mDeptSeq As Integer
        Dim mStdQty As Double
        Dim mProdDeptSeq As Integer
        Dim mProdCost As Double
        Dim mOPRCode As String
        Dim mOprSeq As Integer
        Dim mOutJob As String
        Dim mRMType As String
        Dim pCLBal As Double
        GetCompanyMaterialCost = 0
        mProdDeptSeq = GetProductSeqNo(pItemCode, pStockDeptCode, pAsOnDate, pCompanyCode)
        If pStockType = "WP" Then
            mProdDeptSeq = mProdDeptSeq - 1
            pStockType = "ST"
        End If
        SqlStr = "Select ID.RM_CODE, ID.DEPT_CODE, OPR_CODE,(STD_QTY + GROSS_WT_SCRAP)/OUTPUT_QTY STD_QTY , 'B' BOM_TYPE" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) " & vbCrLf & " FROM PRD_NEWBOM_HDR" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(pAsOnDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))"
        ''SqlStr = SqlStr & vbCrLf & " AND ID.RM_CODE NOT LIKE 'P%'"
        SqlStr = SqlStr & vbCrLf & " AND ID.RM_CODE IN (" & vbCrLf & " SELECT ITEM_CODE FROM INV_ITEM_MST A, INV_GENERAL_MST B" & vbCrLf & " WHERE A.COMPANY_CODE='" & pCompanyCode & "'" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf & " AND A.CATEGORY_CODE=B.GEN_CODE" & vbCrLf & " AND B.PRD_TYPE IN ('R','B','I','D','P','2','3')" & vbCrLf & " AND A.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=ID.RM_CODE )"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsBOM.EOF = False Then
            Do While RsBOM.EOF = False
                mBOMDeptCode = IIf(IsDBNull(RsBOM.Fields("DEPT_CODE").Value), "", RsBOM.Fields("DEPT_CODE").Value)
                mStdQty = IIf(IsDBNull(RsBOM.Fields("STD_QTY").Value), 0, RsBOM.Fields("STD_QTY").Value)
                mDeptSeq = GetProductSeqNo(pItemCode, mBOMDeptCode, pAsOnDate)
                mRMCode = Trim(IIf(IsDBNull(RsBOM.Fields("RM_CODE").Value), "", RsBOM.Fields("RM_CODE").Value))
                mRMType = GetProductionType(mRMCode)
                If mRMType = "R" Or mRMType = "I" Or mRMType = "B" Or mRMType = "P" Or mRMType = "3" Or mRMType = "D" Then
                    mOPRCode = Trim(IIf(IsDBNull(RsBOM.Fields("OPR_CODE").Value), "", RsBOM.Fields("OPR_CODE").Value))
                Else
                    GoTo NextRec ''For Paint..
                End If
                mOprSeq = GetOperationSeq(pItemCode, mBOMDeptCode, mOPRCode, pAsOnDate, pCompanyCode)
                If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & pCompanyCode & "") = True Then
                    xItemUOM = MasterNo
                End If
                mOutJob = Trim(IIf(IsDBNull(RsBOM.Fields("BOM_TYPE").Value), "B", RsBOM.Fields("BOM_TYPE").Value))
                If mOutJob = "B" Then
                    If xItemUOM = "KGS" Then
                        mStdQty = mStdQty / 1000
                    ElseIf xItemUOM = "TON" Then
                        mStdQty = mStdQty / 1000
                        mStdQty = mStdQty / 1000
                    ElseIf xItemUOM = "MT" Then
                        mStdQty = mStdQty / 1000
                        mStdQty = mStdQty / 1000
                    End If
                End If
                pCLBal = xClosingBal * mStdQty
                pCLBal = IIf(pCLBal <= 0, 1, pCLBal)
                mProdCost = 0
                If mDeptSeq = mProdDeptSeq Then
                    If pStockType <> "WP" Then
                        If Val(pStockType) > 0 Then
                            If Val(CStr(mOprSeq)) <= Val(pStockType) Then
                                mProdCost = GetLatestFIFORate(pCompanyCode, mRMCode, xItemUOM, pCLBal, pAsOnDate, pCostType, "", pStockDeptCode) ''mBOMDeptCode
                            End If
                        Else
                            mProdCost = GetLatestFIFORate(pCompanyCode, mRMCode, xItemUOM, pCLBal, pAsOnDate, pCostType, "", pStockDeptCode) ''mBOMDeptCode
                        End If
                    End If
                ElseIf mDeptSeq < mProdDeptSeq Then
                    mProdCost = GetLatestFIFORate(pCompanyCode, mRMCode, xItemUOM, pCLBal, pAsOnDate, pCostType, "", pStockDeptCode) ''mBOMDeptCode
                ElseIf mProdDeptSeq = 0 Then
                    mProdCost = GetLatestFIFORate(pCompanyCode, mRMCode, xItemUOM, pCLBal, pAsOnDate, pCostType, "", pStockDeptCode) ''mBOMDeptCode
                End If
                mProdCost = mProdCost / pCLBal
                GetCompanyMaterialCost = GetCompanyMaterialCost + (mProdCost * mStdQty)
NextRec:
                RsBOM.MoveNext()
            Loop
        End If
        Exit Function
ErrPart:
        'Resume
        GetCompanyMaterialCost = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Module
