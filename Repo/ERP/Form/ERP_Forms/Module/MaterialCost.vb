Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Module MaterialCost
    Public Sub CalcRawMaterialCost(ByRef mProductCode As String, ByRef pStockDeptCode As String, ByRef pStockType As String, ByRef pAsOnDate As String, ByRef pCostType As String, ByRef mAddProcessCost As Boolean)

        On Error GoTo LedgError
        Dim RsMain As ADODB.Recordset = Nothing
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mRMCode As String
        '
        'Dim mNextProductCode As String
        'Dim I As Long
        'Dim mSrn As String
        'Dim RsTemp As ADODB.Recordset=Nothing
        'Dim pSqlStr As String = ""
        Dim mRate As Double
        'Dim mCatCode As String= ""
        'Dim mSubCatCode As String
        Dim pWEF As String
        Dim mRMIssueUOM As String
        Dim mStdQty As Double

        'Dim mCheckProdCode As String
        'Dim mCheckRMCode As String
        Dim mMainItemCode As String
        Dim mProdDeptSeq As Integer
        Dim mDeptSeq As Integer
        Dim mBOMDeptCode As String
        Dim mProcessCost As Double

        mMainItemCode = GetMainItemCode(mProductCode)

        mProdDeptSeq = GetProductSeqNo(mMainItemCode, pStockDeptCode, pAsOnDate)

        If mProdDeptSeq = 0 Then
            mProdDeptSeq = 1
        End If

        If pStockType = "WP" Then
            mProdDeptSeq = mProdDeptSeq - 1
            pStockType = "ST"
        End If

        If pStockType <> "ST" Then
            pStockType = "ST"
        End If


        '       pWEF = Trim(IIf(IsNull(RsMain!WEF), "", RsMain!WEF))

        SqlStr = ""
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.SUBROWNO," & vbCrLf & " ID.RM_CODE, ID.STD_QTY, ID.DEPT_CODE, " & vbCrLf & " ID.GROSS_WT_SCRAP "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID " & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "' " & vbCrLf & " AND IH.WEF= (SELECT MAX(WEF)" & vbCrLf & " FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "'" & vbCrLf & " AND STATUS='O')"

        SqlStr = SqlStr & vbCrLf & " UNION "

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.SERIAL_NO AS SUBROWNO," & vbCrLf & " ID.ITEM_CODE AS RM_CODE, ID.ITEM_QTY AS STD_QTY, 'J/W' AS DEPT_CODE, " & vbCrLf & " ID.SCRAP_QTY AS GROSS_WT_SCRAP "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID " & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "' " & vbCrLf & " AND IH.IS_INHOUSE='N' AND IH.WEF=(SELECT MAX(WEF)" & vbCrLf & " FROM PRD_OUTBOM_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "'" & vbCrLf & " AND STATUS='O' AND IH.IS_INHOUSE='N')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY 1, 2"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            mRate = GetProcessCost(mProductCode)
            mBOMRMAmount = mBOMRMAmount + mRate

            Do While Not RsShow.EOF

                mBOMDeptCode = IIf(IsDbNull(RsShow.Fields("DEPT_CODE").Value), "", RsShow.Fields("DEPT_CODE").Value)
                If mBOMDeptCode = "J/W" Then
                    mDeptSeq = 1
                Else
                    mDeptSeq = GetProductSeqNo(mMainItemCode, mBOMDeptCode, pAsOnDate)
                End If
                mProcessCost = 0

                ''02-07-2011 to be check....
                mStdQty = 1 '' IIf(IsNull(RsShow!STD_QTY), 0, RsShow!STD_QTY) + IIf(IsNull(RsShow!GROSS_WT_SCRAP), 0, RsShow!GROSS_WT_SCRAP)
                If mDeptSeq <= mProdDeptSeq Then
                    Call CalcRawMaterialCostDet(RsShow, mProductCode, mProductCode, pAsOnDate, pCostType, mAddProcessCost, mStdQty)
                End If
                RsShow.MoveNext()
            Loop
        End If


        RsShow = Nothing
        Exit Sub
LedgError:
        ''    Resume
        MsgInformation(Err.Description)
    End Sub
    Public Function GetProcessCostOld(ByRef mProductCode As String, ByRef pStockDeptCode As String) As Double

        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSqlStr As String
        Dim mItemCode As String

        GetProcessCostOld = 0


        mSqlStr = " SELECT ID.ITEM_CODE " & vbCrLf & " FROM PRD_FG_COST_HDR IH, PRD_CONS_COST_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(pStockDeptCode) & "'" & vbCrLf & " AND IH.STATUS='O'"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.WEF = ( SELECT MAX(WEF) " & vbCrLf & " FROM PRD_FG_COST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND STATUS='O')"


        mSqlStr = mSqlStr & vbCrLf & " ORDER BY IH.WEF"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF
                mItemCode = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                GetProcessCostOld = GetProcessCostOld + GetCurrentItemRate(mItemCode, VB6.Format(RunDate, "DD/MM/YYYY"))
                RsTemp.MoveNext()
            Loop
        End If

        mSqlStr = " SELECT SUM(ID.COST) AS COST " & vbCrLf & " FROM PRD_FG_COST_HDR IH, PRD_MANPOWER_COST_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(pStockDeptCode) & "'" & vbCrLf & " AND IH.STATUS='O'"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.WEF = ( SELECT MAX(WEF) " & vbCrLf & " FROM PRD_FG_COST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND STATUS='O')"

        mSqlStr = mSqlStr & vbCrLf & " ORDER BY IH.WEF"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            '            Do While RsTemp.EOF
            GetProcessCostOld = GetProcessCostOld + IIf(IsDbNull(RsTemp.Fields("COST").Value), 0, RsTemp.Fields("COST").Value)
            '                RsTemp.MoveNext
            '            Loop
        End If

        mSqlStr = " SELECT SUM(ID.COST) AS COST " & vbCrLf & " FROM PRD_FG_COST_HDR IH, PRD_SC_COST_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(pStockDeptCode) & "'" & vbCrLf & " AND IH.STATUS='O'"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.WEF = ( SELECT MAX(WEF) " & vbCrLf & " FROM PRD_FG_COST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND STATUS='O')"

        mSqlStr = mSqlStr & vbCrLf & " ORDER BY IH.WEF"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            '            Do While RsTemp.EOF
            GetProcessCostOld = GetProcessCostOld + IIf(IsDbNull(RsTemp.Fields("COST").Value), 0, RsTemp.Fields("COST").Value)
            '                RsTemp.MoveNext
            '            Loop
        End If



        mSqlStr = " SELECT SUM(ID.JW_COST) AS COST " & vbCrLf & " FROM PRD_FG_COST_HDR IH, PRD_JW_COST_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND IH.STATUS='O'" ''AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(pStockDeptCode) & "'" & vbCrLf |                & " AND IH.STATUS='O'"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.WEF = ( SELECT MAX(WEF) " & vbCrLf & " FROM PRD_FG_COST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND STATUS='O')"

        mSqlStr = mSqlStr & vbCrLf & " ORDER BY IH.WEF"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            '            Do While RsTemp.EOF
            GetProcessCostOld = GetProcessCostOld + IIf(IsDbNull(RsTemp.Fields("COST").Value), 0, RsTemp.Fields("COST").Value)
            '                RsTemp.MoveNext
            '            Loop
        End If
        Exit Function
LedgError:
        ''    Resume
        GetProcessCostOld = 0
        MsgInformation(Err.Description)
    End Function

    Public Sub CalcRawMaterialCostDet(ByRef pRs As ADODB.Recordset, ByRef pProductCode As String, ByRef pParentCode As String, ByRef pAsOnDate As String, ByRef pCostType As String, ByRef mAddProcessCost As Boolean, ByRef mStdQty As Double)

        On Error GoTo FillGERR
        Dim mRMCode As String
        Dim mItemUOM As String = ""
        Dim mRate As Double
        'Dim mStdQty As Double
        Dim mDeptCode As String
        Dim mItemStdQty As Double

        mRMCode = Trim(IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))

        '        If pParentCode = pProductCode Then
        '            mRate = GetProcessCost(pParentCode)
        '            mBOMRMAmount = mBOMRMAmount + mRate
        '        End If

        If CheckBOMSubRecord(mRMCode) = True Then
            If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemUOM = MasterNo
            End If
            mItemStdQty = (Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), 0, pRs.Fields("STD_QTY").Value)) + Val(IIf(IsDbNull(pRs.Fields("GROSS_WT_SCRAP").Value), 0, pRs.Fields("GROSS_WT_SCRAP").Value)))
            mDeptCode = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)
            If mDeptCode = "J/W" Then
                If mItemUOM = "TON" Then
                    mStdQty = mStdQty / 1000
                    '                    mStdQty = mStdQty / 1000
                End If
            Else
                If mItemUOM = "KGS" Then
                    mItemStdQty = mItemStdQty / 1000
                ElseIf mItemUOM = "TON" Then
                    mItemStdQty = mItemStdQty / 1000
                    mItemStdQty = mItemStdQty / 1000
                End If
            End If

            mStdQty = mStdQty * mItemStdQty
            Call CalcRawMaterialSubCostDet(mRMCode, "", pProductCode, pAsOnDate, pCostType, mAddProcessCost, mStdQty)
        Else
            If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemUOM = MasterNo
            End If

            mItemStdQty = (Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), 0, pRs.Fields("STD_QTY").Value)) + Val(IIf(IsDbNull(pRs.Fields("GROSS_WT_SCRAP").Value), 0, pRs.Fields("GROSS_WT_SCRAP").Value)))


            '            mRMQty = mRMQty + Val(IIf(IsNull(pRs!STD_QTY), "", pRs!STD_QTY)) '

            mDeptCode = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)

            If mDeptCode = "J/W" Then
                If mItemUOM = "TON" Then
                    mItemStdQty = mItemStdQty / 1000
                    '                    mStdQty = mStdQty / 1000
                End If
            Else
                If mItemUOM = "KGS" Then
                    mItemStdQty = mItemStdQty / 1000
                ElseIf mItemUOM = "TON" Then
                    mItemStdQty = mItemStdQty / 1000
                    mItemStdQty = mItemStdQty / 1000
                End If
            End If

            mStdQty = mStdQty * mItemStdQty

            mBOMRMQty = mBOMRMQty + mStdQty
            '            mRate = GetCurrentItemRate(mRMCode, VB6.Format(RunDate, "DD/MM/YYYY"))
            mRate = GetLatestItemCostFromMRR(mRMCode, mItemUOM, 1, VB6.Format(pAsOnDate, "DD/MM/YYYY"), pCostType, "ST")
            mBOMRMAmount = mBOMRMAmount + (mRate * mStdQty)
            mStdQty = 1

            '            mRate = IIf(IsNull(pRs!PROCESS_COST), 0, pRs!PROCESS_COST)
            '            mBOMRMAmount = mBOMRMAmount + mRate
        End If

        '    Call FillSubAlterRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mDeptCode, pParentCode)

        '    Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode)

        Exit Sub
FillGERR:
        '    Resume
        MsgBox(Err.Description)
    End Sub
    Public Sub CalcRawMaterialSubCostDet(ByRef pProductCode As String, ByRef pWEF As String, ByRef pMainProductCode As String, ByRef pAsOnDate As String, ByRef pCostType As String, ByRef mAddProcessCost As Boolean, ByRef mStdQty As Double)


        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String
        Dim j As Integer
        Dim mRate As Double

        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY, PROCESS_COST "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') " '& vbCrLf |            & " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |
        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            mRate = Val(IIf(IsDbNull(RsShow.Fields("PROCESS_COST").Value), 0, RsShow.Fields("PROCESS_COST").Value))
            mBOMRMAmount = mBOMRMAmount + mRate
            Do While Not RsShow.EOF
                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                Call CalcRawMaterialCostDet(RsShow, pMainProductCode, pProductCode, pAsOnDate, pCostType, mAddProcessCost, mStdQty)
                RsShow.MoveNext()
            Loop
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ITEM_QTY AS STD_QTY, ID.SCRAP_QTY AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY,PROCESS_COST "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O' AND IS_INHOUSE='N'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IS_INHOUSE='N' AND STATUS='O') "

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                mRate = Val(IIf(IsDbNull(RsShow.Fields("PROCESS_COST").Value), 0, RsShow.Fields("PROCESS_COST").Value))
                mBOMRMAmount = mBOMRMAmount + mRate
                Do While Not RsShow.EOF
                    mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                    Call CalcRawMaterialCostDet(RsShow, pMainProductCode, pProductCode, pAsOnDate, pCostType, mAddProcessCost, mStdQty)
                    RsShow.MoveNext()
                Loop
            End If
        End If
        RsShow = Nothing
        '        RsShow.Close

        Exit Sub
FillERR:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Public Function GetProcessCost(ByRef pProductCode As String) As Double

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mRate As Double

        GetProcessCost = 0

        SqlStr = " SELECT PROCESS_COST " & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            mRate = Val(IIf(IsDbNull(RsTemp.Fields("PROCESS_COST").Value), 0, RsTemp.Fields("PROCESS_COST").Value))
            GetProcessCost = GetProcessCost + mRate
        Else

            SqlStr = " SELECT PROCESS_COST " & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsTemp.EOF Then
                mRate = Val(IIf(IsDbNull(RsTemp.Fields("PROCESS_COST").Value), 0, RsTemp.Fields("PROCESS_COST").Value))
                GetProcessCost = GetProcessCost + mRate
            End If
        End If
        RsTemp = Nothing

        Exit Function
FillERR:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Public Function CheckBOMSubRecord(ByRef pProductCode As String) As Boolean


        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mMainItemCode As String
        'Dim mSrn As String
        'Dim xSrn As String
        'Dim j As Long
        '
        CheckBOMSubRecord = False
        '    mMainItemCode = GetMainItemCode(mProductCode)

        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.RM_CODE,ID.DEPT_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') " '& vbCrLf |            & " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |
        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            '        Do While Not RsShow.EOF
            '           mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))
            CheckBOMSubRecord = True
            '        Loop
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.ITEM_CODE AS RM_CODE,'J/W' AS DEPT_CODE " & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O' AND IS_INHOUSE='N') "

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                '            Do While Not RsShow.EOF
                '                mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))
                CheckBOMSubRecord = True
                RsShow.MoveNext()
                '            Loop
            End If
        End If
        RsShow = Nothing
        '        RsShow.Close

        Exit Function
FillERR:
        CheckBOMSubRecord = False
        MsgBox(Err.Description)
        '    Resume
    End Function
End Module
