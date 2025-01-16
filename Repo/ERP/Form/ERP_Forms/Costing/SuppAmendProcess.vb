Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmSuppAmendProcess
    Inherits System.Windows.Forms.Form
    Dim SqlStr As String
    Dim pTempSeq As String


    'Dim RsDDR As ADODB.Recordset			
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdProcess.Click
        On Error GoTo ErrPart

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If Trim(TxtDtFrom.Text) = "" Then
            MsgBox("WEF Date Is Empty ")
            TxtDtFrom.Focus()
            Exit Sub
        End If

        If optCustomer(1).Checked = True Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Please Select Customer Name")
                TxtAccount.Focus()
                Exit Sub
            End If
        End If

        If OptItem(1).Checked = True Then
            If Trim(txtItem.Text) = "" Then
                MsgInformation("Please Select txtItem Name")
                txtItem.Focus()
                Exit Sub
            End If
        End If

        '    If IsProcessed = True Then			
        '        MsgInformation "Amendmend Already Processed"			
        '        Exit Sub			
        '    End If			

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            PubDBCn.CommitTrans()
            MsgBox("Processed Successfully")
        Else
            PubDBCn.RollbackTrans()
            MsgBox("Process Failed")
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        PubDBCn.RollbackTrans()
        MsgBox("Process Failed")
    End Sub

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mSupplierCode As String
        Dim mItemCode As String
        Dim pPrevWEFDate As String
        Dim pWEFDate As String
        Dim pPrevMKey As String
        Dim pAmendNo As Integer
        Dim pGrossCost As Double
        Dim pScrapCost As Double
        Dim pNetCost As Double
        Dim pStdPartCost As Double
        Dim pProcessCost_A As Double
        Dim pProcessCost_B As Double
        Dim pRemarks As String
        Dim pPreparedBy As String
        Dim pNetBOPCost As Double
        Dim pToolCost As Double
        Dim pToolQty As Double
        Dim pToolCostPerPc As Double
        Dim pApprovedBy As String
        Dim mStatus As String
        Dim nMkey As String
        Dim mRowNo As Integer
        Dim mTotExpAmount As Double
        Dim mTotalOprCost As Double
        Dim mGrossWt As Double
        Dim mScrapWt As Double
        Dim mNetWt As Double
        Dim mRMGrossWt As Double
        Dim mRMNetWt As Double
        Dim mV2VSupplier As String

        SqlStr = ""

        pWEFDate = VB6.Format(TxtDtFrom.Text, "DD/MM/YYYY")

        mSqlStr = " SELECT * " & vbCrLf & " FROM PRD_BOP_COST_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If optCustomer(1).Checked = True Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplierCode = MasterNo
            End If

            mSqlStr = mSqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'"
        End If

        If OptItem(1).Checked = True Then
            If MainClass.ValidateWithMasterTable(txtItem.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
            End If

            mSqlStr = mSqlStr & vbCrLf & " AND IH.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
        End If

        mSqlStr = mSqlStr & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) FROM PRD_BOP_COST_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE=IH.SUPP_CUST_CODE" & vbCrLf & " AND ITEM_CODE=IH.ITEM_CODE" & vbCrLf & " AND WEF<TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " )"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.ITEM_CODE NOT IN ( " & vbCrLf & " SELECT DISTINCT ITEM_CODE FROM PRD_BOP_COST_HDR " & vbCrLf & " WHERE COMPANY_CODE=IH.COMPANY_CODE " & vbCrLf & " AND SUPP_CUST_CODE=IH.SUPP_CUST_CODE" & vbCrLf & " AND ITEM_CODE=IH.ITEM_CODE" & vbCrLf & " AND WEF>=TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " )"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                pPrevMKey = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
                mSupplierCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                mV2VSupplier = IIf(IsDBNull(RsTemp.Fields("V2V_SUPPLIER").Value), "N", RsTemp.Fields("V2V_SUPPLIER").Value)
                pAmendNo = IIf(IsDBNull(RsTemp.Fields("AMEND_NO").Value), 0, RsTemp.Fields("AMEND_NO").Value) + 1
                pGrossCost = 0
                pScrapCost = 0
                pNetCost = 0
                pNetBOPCost = 0

                mRowNo = MainClass.AutoGenRowNo("PRD_BOP_COST_HDR", "RowNo", PubDBCn)
                nMkey = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & mRowNo & UCase(Trim(mSupplierCode)) & UCase(Trim(mItemCode)) & VB6.Format(pWEFDate, "YYYYMMDD")


                If GetBOPCost(pPrevMKey, nMkey, mSupplierCode, mItemCode, pGrossCost, pScrapCost, pNetCost, "N") = False Then GoTo ErrPart

                pStdPartCost = IIf(IsDBNull(RsTemp.Fields("PART_COST").Value), 0, RsTemp.Fields("PART_COST").Value)
                pProcessCost_A = IIf(IsDBNull(RsTemp.Fields("PROCESS_A_COST").Value), 0, RsTemp.Fields("PROCESS_A_COST").Value)
                pProcessCost_B = IIf(IsDBNull(RsTemp.Fields("PROCESS_B_COST").Value), 0, RsTemp.Fields("PROCESS_B_COST").Value)

                mTotExpAmount = IIf(IsDBNull(RsTemp.Fields("OTHERCHARGES").Value), 0, RsTemp.Fields("OTHERCHARGES").Value)
                mTotalOprCost = IIf(IsDBNull(RsTemp.Fields("OPR_COST").Value), 0, RsTemp.Fields("OPR_COST").Value)

                pRemarks = IIf(IsDBNull(RsTemp.Fields("REMARKS").Value), "", RsTemp.Fields("REMARKS").Value)
                pPreparedBy = IIf(IsDBNull(RsTemp.Fields("PREPARED_BY").Value), "", RsTemp.Fields("PREPARED_BY").Value)

                pToolCost = IIf(IsDBNull(RsTemp.Fields("TOOL_COST").Value), 0, RsTemp.Fields("TOOL_COST").Value)
                pToolQty = IIf(IsDBNull(RsTemp.Fields("TOOL_QTY").Value), 0, RsTemp.Fields("TOOL_QTY").Value)
                pToolCostPerPc = IIf(IsDBNull(RsTemp.Fields("TOOL_COST_PER_PC").Value), 0, RsTemp.Fields("TOOL_COST_PER_PC").Value)


                mRMGrossWt = IIf(IsDBNull(RsTemp.Fields("RM_GROSSWT").Value), 0, RsTemp.Fields("RM_GROSSWT").Value)
                mRMNetWt = IIf(IsDBNull(RsTemp.Fields("RM_NETWT").Value), 0, RsTemp.Fields("RM_NETWT").Value)
                mGrossWt = IIf(IsDBNull(RsTemp.Fields("ITEM_GROSS_WT").Value), 0, RsTemp.Fields("ITEM_GROSS_WT").Value)
                mScrapWt = IIf(IsDBNull(RsTemp.Fields("ITEM_SCRAP_WT").Value), 0, RsTemp.Fields("ITEM_SCRAP_WT").Value)
                mNetWt = IIf(IsDBNull(RsTemp.Fields("ITEM_NET_WT").Value), 0, RsTemp.Fields("ITEM_NET_WT").Value)

                pNetBOPCost = pNetCost + pStdPartCost + pProcessCost_A + pProcessCost_B + mTotalOprCost + mTotExpAmount + pToolCostPerPc

                pApprovedBy = IIf(IsDBNull(RsTemp.Fields("APP_EMP_CODE").Value), "", RsTemp.Fields("APP_EMP_CODE").Value)
                mStatus = "O"


                SqlStr = " INSERT INTO PRD_BOP_COST_HDR ( " & vbCrLf & " MKEY, COMPANY_CODE, ROWNO, " & vbCrLf & " SUPP_CUST_CODE, ITEM_CODE, WEF, AMEND_NO, " & vbCrLf & " RM_GROSSCOST, SCRAP_COST, RM_NETCOST, " & vbCrLf & " PART_COST, PROCESS_A_COST, PROCESS_B_COST, " & vbCrLf & " NET_COST, REMARKS, PREPARED_BY, " & vbCrLf & " TOOL_COST, TOOL_QTY, TOOL_COST_PER_PC, " & vbCrLf & " APP_EMP_CODE, STATUS, ADDUSER, " & vbCrLf & " ADDDATE, MODUSER, MODDATE, " & vbCrLf & " RM_GROSSWT, RM_NETWT, OTHERCHARGES, OPR_COST, " & vbCrLf & " ITEM_GROSS_WT, ITEM_SCRAP_WT, ITEM_NET_WT, V2V_SUPPLIER" & vbCrLf & " ) VALUES( "

                SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(nMkey) & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & mRowNo & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mSupplierCode) & "', '" & MainClass.AllowSingleQuote(mItemCode) & "', TO_DATE('" & VB6.Format(pWEFDate, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & Val(CStr(pAmendNo)) & ", " & vbCrLf & " " & Val(CStr(pGrossCost)) & ", " & Val(CStr(pScrapCost)) & ", " & Val(CStr(pNetCost)) & "," & vbCrLf & " " & Val(CStr(pStdPartCost)) & ", " & Val(CStr(pProcessCost_A)) & ", " & Val(CStr(pProcessCost_B)) & "," & vbCrLf & " " & Val(CStr(pNetBOPCost)) & ", '" & MainClass.AllowSingleQuote(pRemarks) & "', '" & MainClass.AllowSingleQuote(pPreparedBy) & "', " & vbCrLf & " " & Val(CStr(pToolCost)) & ", " & Val(CStr(pToolQty)) & ", " & Val(CStr(pToolCostPerPc)) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(pApprovedBy) & "', '" & mStatus & "', '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','', " & vbCrLf & " " & Val(CStr(mRMGrossWt)) & ", " & Val(CStr(mRMNetWt)) & ", " & Val(CStr(mTotExpAmount)) & ", " & Val(CStr(mTotalOprCost)) & ", " & vbCrLf & " " & Val(CStr(mGrossWt)) & ", " & Val(CStr(mScrapWt)) & ", " & Val(CStr(mNetWt)) & ", '" & mV2VSupplier & "'" & vbCrLf & " )"



                PubDBCn.Execute(SqlStr)

                If GetBOPCost(pPrevMKey, nMkey, mSupplierCode, mItemCode, pGrossCost, pScrapCost, pNetCost, "Y") = False Then GoTo ErrPart

                If UpdateOtherDetails(pPrevMKey, nMkey, mSupplierCode, mItemCode) = False Then GoTo ErrPart

                If Val(CStr(pAmendNo)) > 0 Then
                    If UpdatePreviousCost(mSupplierCode, mItemCode, Val(CStr(pAmendNo)), "C") = False Then GoTo ErrPart
                End If


                RsTemp.MoveNext()
            Loop

        End If


        Update1 = True
        Exit Function
ErrPart:
        Update1 = False
        MsgBox(Err.Description)
        'Resume			
    End Function
    Private Function UpdateOtherDetails(ByRef pPrevMKey As String, ByRef nMkey As String, ByRef pSupplierCode As String, ByRef pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String


        SqlStr = " INSERT INTO  PRD_BOP_PART_DET (" & vbCrLf & " MKEY, COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, PART_DESC, PART_NO, " & vbCrLf & " PART_UOM, PART_QTY, PART_RATE, " & vbCrLf & " PART_COST ) "

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " '" & MainClass.AllowSingleQuote(nMkey) & "', COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, PART_DESC, PART_NO, " & vbCrLf & " PART_UOM, PART_QTY, PART_RATE, " & vbCrLf & " PART_COST " & vbCrLf & " FROM PRD_BOP_PART_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(pPrevMKey) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO  PRD_BOP_PROCESS1_DET (" & vbCrLf & " MKEY, COMPANY_CODE,SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, OPR_CODE, MACHINE_ITEM_CODE, " & vbCrLf & " STORKE, RATE, COST ) "

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " '" & MainClass.AllowSingleQuote(nMkey) & "', COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, OPR_CODE, MACHINE_ITEM_CODE, " & vbCrLf & " STORKE, RATE, COST " & vbCrLf & " FROM PRD_BOP_PROCESS1_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(pPrevMKey) & "'"

        PubDBCn.Execute(SqlStr)


        SqlStr = " INSERT INTO  PRD_BOP_PROCESS2_DET (" & vbCrLf & " MKEY, COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, PROCESS_DESC, PLANT_NO, " & vbCrLf & " SURFACE, RATE, COST )"


        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " '" & MainClass.AllowSingleQuote(nMkey) & "',  COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, PROCESS_DESC, PLANT_NO, " & vbCrLf & " SURFACE, RATE, COST " & vbCrLf & " FROM PRD_BOP_PROCESS2_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(pPrevMKey) & "'"

        PubDBCn.Execute(SqlStr)



        SqlStr = " INSERT INTO  PRD_BOP_OPERATION_DET (" & vbCrLf & " MKEY, COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, OPR_CODE, OPR_QTY, " & vbCrLf & " OPR_RATE, OPR_COST )"

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " '" & MainClass.AllowSingleQuote(nMkey) & "',  COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, OPR_CODE, OPR_QTY, " & vbCrLf & " OPR_RATE, OPR_COST " & vbCrLf & " FROM PRD_BOP_OPERATION_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(pPrevMKey) & "'"

        PubDBCn.Execute(SqlStr)


        SqlStr = " INSERT INTO  PRD_BOP_EXP_COST_DET (" & vbCrLf & " MKEY, COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, EXP_CODE, EXP_PERCENT, EXP_AMOUNT, " & vbCrLf & " EXP_REMARKS)"

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " '" & MainClass.AllowSingleQuote(nMkey) & "',COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, EXP_CODE, 0, EXP_AMOUNT, " & vbCrLf & " EXP_REMARKS " & vbCrLf & " FROM PRD_BOP_EXP_COST_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(pPrevMKey) & "'"

        ''EXP_PERCENT			

        PubDBCn.Execute(SqlStr)

        UpdateOtherDetails = True

        Exit Function
ErrPart:
        UpdateOtherDetails = False
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume			
    End Function

    Private Function GetBOPCost(ByRef pPrevMKey As String, ByRef pNewMKey As String, ByRef mSupplierCode As String, ByRef mItemCode As String, ByRef pGrossCost As Double, ByRef pScrapCost As Double, ByRef pNetCost As Double, ByRef pIsUpdate As String) As Boolean

        ''GetBOPCost(pPrevMKey, mSupplierCode, mItemCode, pGrossCost, pScrapCost, pNetCost, pNetBOPCost)			
        On Error GoTo AuERR
        Dim i As Integer
        Dim mRMCode As String
        Dim mRMDesc As String
        Dim mRMRate As Double
        Dim mRMUOM As String
        Dim mRMThick As Double
        Dim mRMLenth As Double
        Dim mRMWidth As Double
        Dim mRMDiaMeter As Double
        Dim mWtPerStrip As Double
        Dim mQtyPerStrip As Double
        Dim mWtPerPc As Double
        Dim mRMCost As Double
        Dim mNetWt As Double
        Dim mScrapWt As Double
        Dim mScrapRate As Double
        Dim mScrapCost As Double
        Dim mNetRMCost As Double

        Dim mTotalGrossCost As Double
        Dim mTotalScrapCost As Double
        Dim mTotalNetCost As Double
        Dim mTotalPartCost As Double
        Dim mTotalProcessACost As Double
        Dim mTotalProcessBCost As Double
        Dim mTotalNetBOPCost As Double

        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mMannualCalc As String

        GetBOPCost = False
        mSqlStr = "SELECT * FROM PRD_BOP_COST_DET " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND MKEY = '" & MainClass.AllowSingleQuote(pPrevMKey) & "'" & vbCrLf _
        & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf _
        & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "'"


        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        i = 0

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                i = i + 1
                mRMCode = Trim(IIf(IsDBNull(RsTemp.Fields("RM_CODE").Value), "", RsTemp.Fields("RM_CODE").Value))
                mRMUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                If Trim(mRMCode) = "" Then GoTo NextLoop

                mMannualCalc = Trim(IIf(IsDBNull(RsTemp.Fields("MANNUAL_CALC").Value), "N", RsTemp.Fields("MANNUAL_CALC").Value))
                mRMRate = GetLastestRate(mRMCode, mSupplierCode, Trim(TxtDtFrom.Text), "RM")
                mScrapRate = GetLastestRate(mRMCode, mSupplierCode, Trim(TxtDtFrom.Text), "SC")

                mRMThick = Val(IIf(IsDBNull(RsTemp.Fields("THICKNESS_RM").Value), 0, RsTemp.Fields("THICKNESS_RM").Value))
                mRMLenth = Val(IIf(IsDBNull(RsTemp.Fields("LENGTH_RM").Value), 0, RsTemp.Fields("LENGTH_RM").Value))
                mRMWidth = Val(IIf(IsDBNull(RsTemp.Fields("WIDTH_RM").Value), 0, RsTemp.Fields("WIDTH_RM").Value))
                mRMDiaMeter = Val(IIf(IsDBNull(RsTemp.Fields("DIAMETER_RM").Value), 0, RsTemp.Fields("DIAMETER_RM").Value))
                mWtPerStrip = Val(IIf(IsDBNull(RsTemp.Fields("WT_PER_STRIP").Value), 0, RsTemp.Fields("WT_PER_STRIP").Value))
                mQtyPerStrip = Val(IIf(IsDBNull(RsTemp.Fields("QTY_PER_STRIP").Value), 0, RsTemp.Fields("QTY_PER_STRIP").Value))
                mWtPerPc = Val(IIf(IsDBNull(RsTemp.Fields("GROSS_WT_PCS").Value), 0, RsTemp.Fields("GROSS_WT_PCS").Value))
                mRMCost = CDbl(VB6.Format(mWtPerPc * mRMRate, "0.00"))
                mRMCost = mRMCost / 1000 ''In KGS			
                mNetWt = Val(IIf(IsDBNull(RsTemp.Fields("NET_WT_PCS").Value), 0, RsTemp.Fields("NET_WT_PCS").Value))
                mScrapWt = CDbl(VB6.Format(Val(IIf(IsDBNull(RsTemp.Fields("GROSS_WT_SCRAP").Value), 0, RsTemp.Fields("GROSS_WT_SCRAP").Value)), "0.000"))

                mScrapCost = CDbl(VB6.Format(mScrapWt * mScrapRate, "0.00"))
                mScrapCost = mScrapCost / 1000 ''In KGS			
                mNetRMCost = CDbl(VB6.Format(mRMCost - mScrapCost, "0.00"))


                If pIsUpdate = "Y" Then
                    SqlStr = " INSERT INTO  PRD_BOP_COST_DET (" & vbCrLf & " MKEY, COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, RM_CODE, ISSUE_UOM, " & vbCrLf & " RATE_PCS, THICKNESS_RM, LENGTH_RM, " & vbCrLf & " WIDTH_RM, DIAMETER_RM, WT_PER_STRIP, " & vbCrLf & " QTY_PER_STRIP, GROSS_WT_PCS, COST_PCS, " & vbCrLf & " NET_WT_PCS, GROSS_WT_SCRAP, RATE_SCRAP, " & vbCrLf & " COST_SCRAP, NET_COST_PCS,MANNUAL_CALC ) VALUES ( "


                    SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(pNewMKey) & "'," & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(mSupplierCode) & "', '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf & " " & i & ", '" & mRMCode & "', '" & mRMUOM & "', " & vbCrLf & " " & mRMRate & ", " & mRMThick & ", " & mRMLenth & ", " & vbCrLf & " " & mRMWidth & ", " & mRMDiaMeter & ", " & mWtPerStrip & ", " & vbCrLf & " " & mQtyPerStrip & ", " & mWtPerPc & ", " & mRMCost & ", " & vbCrLf & " " & mNetWt & ", " & mScrapWt & ", " & mScrapRate & ", " & vbCrLf & " " & mScrapCost & ", " & mNetRMCost & ", '" & mMannualCalc & "')"

                    PubDBCn.Execute(SqlStr)
                End If

                pGrossCost = pGrossCost + mRMCost
                pScrapCost = pScrapCost + mScrapCost
                pNetCost = pNetCost + mNetRMCost

NextLoop:
                RsTemp.MoveNext()
            Loop
        End If

        GetBOPCost = True
        '    If mRMThick <> 0 And mRMLenth <> 0 And mRMWidth <> 0 Then			
        '        mWtPerStrip = Format(mRMThick * mRMLenth * mRMWidth * 7.85 / (1000000), "0.000")			
        '    ElseIf mRMThick <> 0 And mRMLenth <> 0 And mRMDiaMeter <> 0 Then			
        '        mWtPerStrip = Format(3.14 * (mRMDiaMeter - mRMThick) * mRMLenth * 7.85 / (1000000), "0.000")			
        '    ElseIf mRMLenth <> 0 And mRMDiaMeter <> 0 Then			
        '        mWtPerStrip = Format((3.14 / 4) * (mRMDiaMeter * mRMDiaMeter) * mRMLenth * 7.85 / (1000000), "0.000")			
        '    End If			
        '    mWtPerStrip = mWtPerStrip * 1000 ''IN Grams			
        'mWtPerPc = Format(mWtPerStrip / mQtyPerStrip, "0.000")			



        '    txtGrossCost.Text = Format(mTotalGrossCost, "0.00")			
        '    txtScrapCost.Text = Format(mTotalScrapCost, "0.00")			
        '    txtNetCost.Text = Format(mTotalNetCost, "0.00")			
        '    txtStdPartCost.Text = Format(mTotalPartCost, "0.00")			
        '    txtProcessCost_A.Text = Format(mTotalProcessACost, "0.00")			
        '    txtProcessCost_B.Text = Format(mTotalProcessBCost, "0.00")			
        '			
        '    txtOpeartionCost.Text = Format(mTotalOprCost, "0.00")			
        '			
        '    mTotExpAmount = AutoCostExpCalc			
        '    txtOtherCost.Text = Format(mTotExpAmount, "0.00")			
        '			
        '    If Val(txtToolQty.Text) <> 0 Then			
        '        txtToolCostPerPc.Text = Format(Val(txtToolCost.Text) / Val(txtToolQty.Text), "0.00")			
        '    End If			
        '			
        '    mTotalNetBOPCost = mTotalNetCost + mTotalPartCost + mTotalProcessACost + mTotalProcessBCost + mTotalOprCost + mTotExpAmount + Val(txtToolCostPerPc.Text)			
        '    txtNetBOPCost.Text = Format(mTotalNetBOPCost, "0.00")			
        '			
        '    txtGrossWt.Text = Format(mTotalGrossWt, "0.00")			
        '    txtScrapWt.Text = Format(mTotalScrapWt, "0.00")			
        '    txtNetWt.Text = Format(mTotalNetWt, "0.00")			


        Exit Function
AuERR:
        '    Resume			
        GetBOPCost = False
        MsgBox(Err.Description)
    End Function
    Private Function GetLastestRate(ByRef mRMCode As String, ByRef pSupplierCode As String, ByRef pDate As String, ByRef pType As String) As Double


        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetLastestRate = 0
        SqlStr = ""

        If pType = "RM" Then
            SqlStr = " SELECT  RATE AS RATE "
        Else
            SqlStr = " SELECT  SCRAP_RATE AS RATE "
        End If

        SqlStr = SqlStr & vbCrLf _
        & " FROM PRD_RM_GRADE_RATE_DET" & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND GRADE_CODE ='" & MainClass.AllowSingleQuote(mRMCode) & "'" & vbCrLf _
        & " AND SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf _
        & " AND WEF_DATE =TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetLastestRate = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("Rate").Value), 0, RsTemp.Fields("Rate").Value), "0.00"))
        End If
        Exit Function
ErrPart:
        GetLastestRate = 0
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume			
    End Function


    Private Function UpdatePreviousCost(ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef pAmendNo As Integer, ByRef pPreviousStatus As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " UPDATE PRD_BOP_COST_HDR SET " & vbCrLf & " STATUS = '" & pPreviousStatus & "', " & vbCrLf & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE = TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AMEND_NO = " & pAmendNo - 1 & "" & vbCrLf & " AND SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        PubDBCn.Execute(SqlStr)

        UpdatePreviousCost = True

        Exit Function
ErrPart:
        UpdatePreviousCost = False
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume			
    End Function

    Private Function IsProcessed() As Boolean

        On Error GoTo IsERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mCustomerCode As String
        Dim mItemCode As String


        SqlStr = "SELECT * " & vbCrLf & " FROM PRD_BOP_COST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TO_CHAR(WEF,'YYYYMM')='" & VB6.Format(TxtDtFrom.Text, "YYYYMM") & "'"

        If optCustomer(1).Checked = True Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustomerCode = MasterNo
            End If
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustomerCode) & "'"
        End If

        If OptItem(1).Checked = True Then
            If MainClass.ValidateWithMasterTable(txtItem.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
            End If

            SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            IsProcessed = True
            Exit Function
        Else
            IsProcessed = False
            Exit Function
        End If


        Exit Function
IsERR:
        MsgBox(Err.Description)
        IsProcessed = False
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click
        SearchItem()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub FrmSuppAmendProcess_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmSuppAmendProcess_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LErr
        MainClass.SetControlsColor(Me)
        TxtDtFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Me.Text = "Process - Supplier Costing Amendment"

        txtItem.Enabled = False
        cmdSearchItem.Enabled = False

        TxtAccount.Enabled = True
        cmdsearch.Enabled = True

        'Me.Height = VB6.TwipsToPixelsY(4200)			
        'Me.Width = VB6.TwipsToPixelsX(6615)			
        Me.Top = 0
        Me.Left = 0
        Exit Sub
LErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub OptCustomer_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCustomer.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optCustomer.GetIndex(eventSender)
            TxtAccount.Enabled = IIf(Index = 0, False, True)
            cmdsearch.Enabled = IIf(Index = 0, False, True)
        End If
    End Sub
    Private Sub OptItem_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptItem.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptItem.GetIndex(eventSender)
            txtItem.Enabled = IIf(Index = 0, False, True)
            cmdSearchItem.Enabled = IIf(Index = 0, False, True)
        End If
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
        Dim SqlStr As String

        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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
                '            TxtDtFrom.focus			
                '            Cancel = True			
            End If
        End If
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub TxtItem_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItem.DoubleClick
        SearchItem()
    End Sub

    Private Sub txtItem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItem.KeyPress
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
        Dim SqlStr As String

        If txtItem.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable(txtItem.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        MainClass.SearchGridMaster(txtItem.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            txtItem.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class
