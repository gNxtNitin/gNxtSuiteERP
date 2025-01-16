Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Module MISReport
    Public Structure AlterItemArray
        Dim mAlterCode As String
    End Structure
    Public mAlterItemData() As AlterItemArray
    Public Function DespatchSqlQry(ByRef pItemCode As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        ''TRN.RM_CODE,
        DespatchSqlQry = ""
        SqlStr = " SELECT DISTINCT " & vbCrLf & " PRODUCT_CODE, STD_QTY+  GROSS_WT_SCRAP AS STD_QTY, DEPT_CODE, LEVEL" & vbCrLf & " FROM VW_PRD_BOM_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " START WITH  COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE || ' ')=TRIM(RM_CODE) || COMPANY_CODE || ' '"

        DespatchSqlQry = SqlStr

        Exit Function
ErrPart:
        DespatchSqlQry = ""
    End Function
    Public Function GetFGDespatchQty(ByRef mItemCode As String, ByRef mItemUOM As String, ByRef mFromDate As String, ByRef mToDate As String, ByRef pPubDBCn As ADODB.Connection) As Double

        'mOpQty As Double, mCLQty As Double,mPurQty As Double,mINHouseQty As Double,
        On Error GoTo ErrPart
        Dim mMainItemCode As String
        Dim mDespQty As Double
        Dim mSqlStrRel As String
        Dim RsRel As ADODB.Recordset = Nothing
        Dim xProductRelCode As String

        GetFGDespatchQty = GetNetDespatch(mItemCode, mFromDate, mToDate, PubDBCn) ''+ GetNetDespatch(mMainItemCode, mFromDate, mToDate, PubDBCn)

        mSqlStrRel = GetRelationItem(mItemCode)
        If mSqlStrRel <> "" Then
            MainClass.UOpenRecordSet(mSqlStrRel, pPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRel, ADODB.LockTypeEnum.adLockReadOnly)
            If RsRel.EOF = False Then
                Do While RsRel.EOF = False
                    xProductRelCode = Trim(IIf(IsDbNull(RsRel.Fields("REF_ITEM_CODE").Value), "", RsRel.Fields("REF_ITEM_CODE").Value))
                    mDespQty = GetNetDespatch(xProductRelCode, mFromDate, mToDate, PubDBCn) '' Abs(GetStockQty(mParentcode, pItemUOM, "STR", "FG", ConWH, "", "'" & ConStockRefType_DSP & "'"))
                    '                mDespQty = mDespQty * mStdQty
                    GetFGDespatchQty = GetFGDespatchQty + mDespQty
                    RsRel.MoveNext()
                Loop
            End If
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetFGDespatchQty = 0
    End Function
    Public Function GetQueryForAlterItem(ByRef pItemCode As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT DISTINCT TRIM(ALTER_RM_CODE) AS ALTER_RM_CODE" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.STATUS='O'" & vbCrLf & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " UNION "

        SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT TRIM(MAINITEM_CODE) AS ALTER_RM_CODE" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.STATUS='O'" & vbCrLf & " AND ID.ALTER_RM_CODE IN (" & vbCrLf & " SELECT DISTINCT ALTER_RM_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.STATUS='O'" & vbCrLf & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "')"

        SqlStr = SqlStr & vbCrLf & " MINUS SELECT '" & Trim(pItemCode) & "' AS ALTER_RM_CODE FROM DUAL "
        GetQueryForAlterItem = SqlStr

        Exit Function
ErrPart:
        GetQueryForAlterItem = ""
    End Function
    Public Function GetFGDespatchQtyForRM(ByRef pQry As String, ByRef pFromDate As String, ByRef pToDate As String, ByRef pPubDBCn As ADODB.Connection) As Double

        ''GetFGDespatchQtyForRM(pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mParentCode As String
        Dim mChildCode As String
        Dim mStdQty As Double
        Dim mLevel As Integer
        Dim mDespQty As Double
        Dim pItemUOM As String = ""
        Dim mSqlStrRel As String
        Dim RsRel As ADODB.Recordset = Nothing
        Dim xProductRelCode As String

        GetFGDespatchQtyForRM = 0
        MainClass.UOpenRecordSet(pQry, pPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mStdQty = 1
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mLevel = Val(IIf(IsDbNull(RsTemp.Fields("Level").Value), 1, RsTemp.Fields("Level").Value))
                If mLevel = 1 Then
                    mStdQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), "", RsTemp.Fields("STD_QTY").Value), "0.0000"))
                Else
                    mStdQty = mStdQty * CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), "", RsTemp.Fields("STD_QTY").Value), "0.0000"))
                End If

                mParentCode = Trim(IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))

                If MainClass.ValidateWithMasterTable(mParentCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", pPubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    pItemUOM = Trim(MasterNo)
                End If


                mDespQty = GetNetDespatch(mParentCode, pFromDate, pToDate, pPubDBCn)
                mDespQty = mDespQty * mStdQty
                GetFGDespatchQtyForRM = GetFGDespatchQtyForRM + mDespQty

                mSqlStrRel = GetRelationItem(mParentCode)
                If mSqlStrRel <> "" Then
                    MainClass.UOpenRecordSet(mSqlStrRel, pPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRel, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsRel.EOF = False Then
                        Do While RsRel.EOF = False
                            xProductRelCode = Trim(IIf(IsDbNull(RsRel.Fields("REF_ITEM_CODE").Value), "", RsRel.Fields("REF_ITEM_CODE").Value))
                            mDespQty = GetNetDespatch(xProductRelCode, pFromDate, pToDate, pPubDBCn) '' Abs(GetStockQty(mParentcode, pItemUOM, "STR", "FG", ConWH, "", "'" & ConStockRefType_DSP & "'"))
                            mDespQty = mDespQty * mStdQty
                            GetFGDespatchQtyForRM = GetFGDespatchQtyForRM + mDespQty
                            RsRel.MoveNext()
                        Loop
                    End If
                End If

                mDespQty = 0
                RsTemp.MoveNext()
            Loop
        End If


        Exit Function
ErrPart:
        GetFGDespatchQtyForRM = 0
    End Function
    Public Function GetRelationItem(ByRef mProductCode As String) As String
        On Error GoTo ErrPart


        GetRelationItem = " SELECT REF_ITEM_CODE , ITEM_UOM " & vbCrLf & " FROM INV_ITEM_RELATIONSHIP_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"


        Exit Function
ErrPart:
        GetRelationItem = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Public Function GetDespatchDetail(ByRef mItemCode As String, ByRef mItemUOM As String, ByRef mFromDate As String, ByRef mToDate As String, ByRef mDespQty As Double, ByRef pPubDBCn As ADODB.Connection) As Boolean

        'mOpQty As Double, mCLQty As Double,mPurQty As Double,mINHouseQty As Double,
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xSqlStr As String
        Dim mUpperBound As Integer
        Dim mAlterItemCodeStr As String
        Dim I As Integer
        Dim xDespSqlQry As String


        GetDespatchDetail = False


        xSqlStr = GetQueryForAlterItem(mItemCode)
        MainClass.UOpenRecordSet(xSqlStr, pPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        mUpperBound = 0
        mAlterItemCodeStr = ""
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    mUpperBound = mUpperBound + 1
                End If
            Loop
            ReDim mAlterItemData(mUpperBound)
            RsTemp.MoveFirst()
            I = 0
            Do While RsTemp.EOF = False
                mAlterItemData(I).mAlterCode = Trim(IIf(IsDbNull(RsTemp.Fields("ALTER_RM_CODE").Value), "", RsTemp.Fields("ALTER_RM_CODE").Value))
                mAlterItemCodeStr = mAlterItemCodeStr & "/" & Trim(IIf(IsDbNull(RsTemp.Fields("ALTER_RM_CODE").Value), "", RsTemp.Fields("ALTER_RM_CODE").Value))
                RsTemp.MoveNext()
                I = I + 1
            Loop
        Else
            ReDim mAlterItemData(0)
            mAlterItemData(0).mAlterCode = ""
        End If
        xDespSqlQry = DespatchSqlQry(mItemCode)

        For I = 0 To mUpperBound
            If mAlterItemData(I).mAlterCode <> "" Then
                xDespSqlQry = xDespSqlQry & vbCrLf & " UNION " & vbCrLf & DespatchSqlQry(mAlterItemData(I).mAlterCode)
            End If
        Next
        '
        mDespQty = GetFGDespatchQtyForRM(xDespSqlQry, mFromDate, mToDate, pPubDBCn)
        GetDespatchDetail = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetDespatchDetail = False
    End Function
    Public Function GetNetDespatch(ByRef pItemCode As String, ByRef pFromDate As String, ByRef pToDate As String, ByRef pPubDBCn As ADODB.Connection) As Double


        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSaleReturn As Double

        SqlStr = ""

        SqlStr = "SELECT ABS(SUM(ID.PACKED_QTY * DECODE(TRIM(ID.ITEM_UOM),TRIM(INVMST.PURCHASE_UOM),INVMST.UOM_FACTOR,1))) AS INQTY"
        SqlStr = SqlStr & vbCrLf & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE AND IH.DESP_TYPE<>'U'" & vbCrLf & " AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND ID.STOCK_TYPE<>'CR'"


        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE>=TO_DATE('" & VB6.Format(pFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, pPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields(0).Value) Then
                GetNetDespatch = 0
            Else
                GetNetDespatch = RsTemp.Fields(0).Value
            End If
        Else
            GetNetDespatch = 0
        End If


        '    SqlStr = "SELECT SUM(ID.RECEIVED_QTY * DECODE(TRIM(ID.ITEM_UOM),TRIM(INVMST.PURCHASE_UOM),INVMST.UOM_FACTOR,1)) AS INQTY"
        '    SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, INV_ITEM_MST INVMST"
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
        ''            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
        ''            & " AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND ID.STOCK_TYPE<>'CR' AND IH.REF_TYPE='I'"
        '
        '
        '    SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(pFromDate, "DD-MMM-YYYY") & "')"
        '    SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "')"
        '
        '    MainClass.UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        If IsNull(RsTemp.Fields(0).Value) Then
        '            mSaleReturn = 0
        '        Else
        '            mSaleReturn = RsTemp.Fields(0).Value
        '        End If
        '    Else
        '        mSaleReturn = 0
        '    End If

        GetNetDespatch = GetNetDespatch - mSaleReturn
        RsTemp = Nothing

        Exit Function
ErrPart:
        GetNetDespatch = 0
    End Function
End Module
