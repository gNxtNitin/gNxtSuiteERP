Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Module PrintInvoice
    Dim RSSalesDetail As ADODB.Recordset
    Dim RSPaintDetail As ADODB.Recordset
    Dim RSJWDetail As ADODB.Recordset

    Dim SqlStr As String = ""
    Dim PrintLine_Renamed As Integer
    Dim FirstValue As Boolean
    Dim EDPerc As Double
    Dim QtyTotal, RateTotal As Object
    Dim AmtTotal As Double
    Dim AccessAmountPerUnit As Double
    Dim ISNo As Integer
    Dim mRRPPNo As String
    Dim mDespatchMode As String
    Dim AccessAmountTotal As Double
    Dim AccessPerTotal As Double
    Dim AccessTotal As Double
    Dim GrossQtyTotal As Object
    Dim MRPTotal As Double
    Dim GrossAmountTotal As Double
    Dim mMRP As Double
    Dim PageNo As Integer
    Dim pCode As Integer
    Dim ICode As Object
    Dim pName As String
    Dim IDesc As String
    Dim IShortName As String
    Dim IRate As Double
    Dim IUnit As String
    Dim STCode As Integer
    Dim STCodeClub As Integer
    Dim mRemarks As String
    Dim mSTType As String
    Const TabSNo As Short = 0
    Const TabIDesc As Short = 5
    Const TabIPart As Short = 58
    Const TabUnit As Short = 85
    Const TabQty As Short = 90
    Const TabRate As Short = 105
    Const TabAmount As Short = 120
    Const TabLastCol As Short = 135

    Const TabSNo1 As Short = 0
    Const TabIDesc1 As Short = 5
    Const TabIPart1 As Short = 35
    Const TabUnit1 As Short = 50
    Const TabQty1 As Short = 55
    Const TabRate1 As Short = 61
    Const TabAmount1 As Short = 70

    Const TaxAnnexSNo As Short = 0
    Const TabAnnexIDesc As Short = 5
    Const TabAnnexUnit As Short = 58
    Const TabAnnexQty As Short = 63
    Const TabAnnexRate As Short = 75
    Const TabAnnexAmount As Short = 87
    Const TabAnnexMRP As Short = 99
    Const TabAnnexMRPAmount As Short = 111
    Const TabAnnexLastCol As Short = 125

    Const TaxSCSNo As Short = 0
    Const TabSCIDesc As Short = 5
    Const TabSCUnit As Short = 78
    Const TabSCQty As Short = 85
    Const TabSCChallanNo As Short = 100
    Const TabSCBillNo As Short = 115
    Const TabSCChallanDate As Short = 130
    Const TabSCLastCol As Short = 145

    Dim mIsLastPage As Boolean
    Public Function PrintExcise(ByRef pPrintMode As String, ByRef pMKey As String, ByRef mPrintedFormat As String, ByRef mPaintPrint As String, ByRef pInvType As String, ByRef pDespRefType As String, ByRef pAnnexPrint As String, ByRef pExpAnnexPrint As String, ByRef mIsMRP As String, ByRef mJWDetail As String, ByRef pSubsidiaryChallanPrint As String, Optional ByRef mJWRemarks As String = "", Optional ByRef mJWSTRemarks As String = "", Optional ByRef pSC_All As String = "", Optional ByRef pSC_F4No As String = "", Optional ByRef pIsPrintText As String = "", Optional ByRef pPrintTextDesc As String = "", Optional ByRef pItemGroup As String = "", Optional ByRef pAgtPermission As String = "", Optional ByRef mExtraRemarks As String = "") As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        mIsLastPage = False
        SqlStr = ""
        FirstValue = False
        GrossAmountTotal = 0
        GrossQtyTotal = 0
        AccessTotal = 0
        AccessPerTotal = 0
        PageNo = 0
        PrintExcise = True
        ISNo = 0

        FileOpen(1, mLocalPath & "\Invoice.Prn", OpenMode.Output)

        SqlStr = MakeSQL(pMKey, mPrintedFormat, pDespRefType, pItemGroup)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalesDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = MakePaintSQL(pMKey)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSPaintDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If mJWDetail = "Y" Then
            SqlStr = MakeJWDtlSQL(pMKey)
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSJWDetail, ADODB.LockTypeEnum.adLockReadOnly)
        End If

        If RSSalesDetail.EOF = True Then
            MsgInformation("Nothing to print")
            PrintExcise = False
            FileClose(1)
            Exit Function
        End If

        If pAnnexPrint = "Y" Or pExpAnnexPrint = "YA" Then
            Call PrintAnnexHeader(pExpAnnexPrint)
            Call PrintAnnexDetail(mPrintedFormat, RSPaintDetail, mPaintPrint, pExpAnnexPrint)
            If pExpAnnexPrint = "YA" Then
                Call PrintFooterNewFormat(pAgtPermission, "")
            Else
                Call PrintAnnexFooter()
            End If
        ElseIf pSubsidiaryChallanPrint = "Y" Then
            '        Call PrintSubsidiaryChallanHeader	
            Call PrintSubsidiaryChallanDetail(pSC_All, pSC_F4No)
        Else
            If mPrintedFormat = "Y" Then
                If DBConInvPrePrint = "Y" Then
                    Call PrintHeader(mIsMRP)
                    Call PrintDetail(mPrintedFormat, RSPaintDetail, mPaintPrint, pExpAnnexPrint, mIsMRP, RSJWDetail, mJWDetail, pIsPrintText, pPrintTextDesc, pAgtPermission, mExtraRemarks)
                    If RsCompany.Fields("COMPANY_CODE").Value = 6 Then
                        Call PrintFooter(pAgtPermission)
                    Else
                        Call PrintFooterNewFormat(pAgtPermission, mExtraRemarks)
                    End If

                Else
                    '                Call PrintPPHeader("s")	
                    Call PrintHeader_PlainPaper(mIsMRP)
                    Call PrintDetail_PlainPaper(mPrintedFormat, RSPaintDetail, mPaintPrint, pExpAnnexPrint, mIsMRP, RSJWDetail, mJWDetail, pAgtPermission)
                    Call PrintFooter_PlainPaper(pAgtPermission)
                End If
            Else
                If DBConInvPrePrint = "Y" Then
                    Call PrintJWHeader(pInvType)
                    '            Call PrintPPHeader(pInvType)	
                    Call PrintJWDetail(mPrintedFormat, RSPaintDetail, mPaintPrint, pExpAnnexPrint, RSJWDetail, mJWDetail, mJWRemarks, pAgtPermission)
                    Call PrintJWFooter(mJWSTRemarks)
                    '            Call PrintPPFooter	
                Else
                    Call PrintPPHeader(pInvType)
                    Call PrintJWDetail(mPrintedFormat, RSPaintDetail, mPaintPrint, pExpAnnexPrint, RSJWDetail, mJWDetail, mJWRemarks, pAgtPermission)
                    Call PrintPPFooter() 'Call PrintJWFooter(mJWSTRemarks)        ''	
                End If
            End If
        End If

        FileClose(1)

        Dim mFP As Boolean
        Dim pFileName As String
        If pPrintMode = "P" Then
            mFP = Shell(My.Application.Info.DirectoryPath & "\PrintINV.bat", AppWinStyle.NormalFocus) '' '	
            If mFP = False Then GoTo ERR1
            '        Shell App.path & "\PrintINV.bat",vbNormalFocus	
        Else
            pFileName = mLocalPath & "\Invoice.Prn"
            Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
            'App.Path & "\RVIEW.EXE "	
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        PrintExcise = False
        FileClose(1)
        '    Resume	
    End Function

    Public Function PrintMultiExciseInvoice(ByRef pPrintMode As String, ByRef pRsPrint As ADODB.Recordset, ByRef mPrintallInv As String, ByRef mPaintPrint As String, ByRef mJWDetail As String, ByRef pAgtPermission As String) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim pMKey As String
        Dim pInvType As String
        Dim mPrintedFormat As String
        mIsLastPage = False
        SqlStr = ""
        FirstValue = False
        GrossAmountTotal = 0
        GrossQtyTotal = 0
        AccessTotal = 0
        AccessPerTotal = 0
        PageNo = 0
        PrintMultiExciseInvoice = True
        ISNo = 0

        FileOpen(1, mLocalPath & "\Invoice.Prn", OpenMode.Output)

        Do While pRsPrint.EOF = False

            pMKey = IIf(IsDbNull(pRsPrint.Fields("mKey").Value), "-1", pRsPrint.Fields("mKey").Value)
            pInvType = IIf(IsDbNull(pRsPrint.Fields("Name").Value), "-1", pRsPrint.Fields("Name").Value)
            If mPrintallInv = "Y" Then
                mPrintedFormat = "Y"
            Else
                mPrintedFormat = IIf(pRsPrint.Fields("ISSALEJW").Value = "Y", "N", "Y")
            End If

            SqlStr = MakeSQL(pMKey, mPrintedFormat, "", "N")

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalesDetail, ADODB.LockTypeEnum.adLockReadOnly)

            SqlStr = MakePaintSQL(pMKey)
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSPaintDetail, ADODB.LockTypeEnum.adLockReadOnly)

            If mJWDetail = "Y" Then
                SqlStr = MakeJWDtlSQL(pMKey)
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSJWDetail, ADODB.LockTypeEnum.adLockReadOnly)
            End If

            If RSSalesDetail.EOF = True Then
                MsgInformation("Nothing to print")
                PrintMultiExciseInvoice = False
                FileClose(1)
                Exit Function
            End If

            If mPrintedFormat = "Y" Then
                If DBConInvPrePrint = "Y" Then
                    Call PrintHeader("N")
                    Call PrintDetail(mPrintedFormat, RSPaintDetail, mPaintPrint, "N", "N", RSJWDetail, mJWDetail, "", "", pAgtPermission, "")
                    If RsCompany.Fields("COMPANY_CODE").Value = 6 Then
                        Call PrintFooter(pAgtPermission)
                    Else
                        Call PrintFooterNewFormat(pAgtPermission, "")
                    End If
                Else
                    '                Call PrintPPHeader("S")	
                    Call PrintHeader_PlainPaper("N")
                    Call PrintDetail_PlainPaper(mPrintedFormat, RSPaintDetail, "N", "N", "N", RSJWDetail, mJWDetail, pAgtPermission)
                    Call PrintFooter_PlainPaper(pAgtPermission)
                End If
            Else
                If DBConInvPrePrint = "Y" Then
                    Call PrintJWHeader(pInvType)
                    Call PrintJWDetail(mPrintedFormat, RSPaintDetail, mPaintPrint, "N", RSJWDetail, mJWDetail, "", pAgtPermission)
                    Call PrintJWFooter()
                Else
                    Call PrintPPHeader(pInvType)
                    Call PrintJWDetail(mPrintedFormat, RSPaintDetail, mPaintPrint, "N", RSJWDetail, mJWDetail, "", pAgtPermission)
                    Call PrintPPFooter()
                End If
            End If

            pRsPrint.MoveNext()
        Loop
        FileClose(1)

        Dim mFP As Boolean
        Dim pFileName As String
        If pPrintMode = "P" Then
            mFP = Shell(My.Application.Info.DirectoryPath & "\PrintINV.bat", AppWinStyle.NormalFocus) '' '	
            If mFP = False Then GoTo ERR1
            '        Shell App.path & "\PrintINV.bat",vbNormalFocus	
        Else
            pFileName = mLocalPath & "\Invoice.Prn"
            Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
            'App.Path & "\RVIEW.EXE "	
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        PrintMultiExciseInvoice = False
        FileClose(1)
        '    Resume	
    End Function
    Public Function PrintSuppBill(ByRef pPrintMode As String, ByRef pMKey As String, ByRef mPrintedFormat As String, ByRef mPaintPrint As String, ByRef pInvType As String) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        mIsLastPage = False
        SqlStr = ""
        FirstValue = False
        GrossAmountTotal = 0
        GrossQtyTotal = 0
        AccessTotal = 0
        AccessPerTotal = 0
        PageNo = 0
        PrintSuppBill = True
        ISNo = 0
        FileOpen(1, mLocalPath & "\Invoice.Prn", OpenMode.Output)
        SqlStr = MakeSQLSupp(pMKey, mPrintedFormat)

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalesDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = MakePaintSQL(pMKey)

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSPaintDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RSSalesDetail.EOF = True Then
            MsgInformation("Nothing to print")
            PrintSuppBill = False
            FileClose(1)
            Exit Function
        End If

        If mPrintedFormat = "Y" Then
            Call PrintHeader("N")
            Call PrintSuppDetail(mPrintedFormat, RSPaintDetail, mPaintPrint)
            If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
                Call PrintFooterNewFormat("N", "")
            Else
                Call PrintFooter("N")
            End If
        Else
            Call PrintJWHeader(pInvType)
            Call PrintSuppJWDetail(mPrintedFormat, RSPaintDetail, mPaintPrint)
            Call PrintJWFooter()
        End If

        FileClose(1)

        Dim mFP As Boolean
        Dim pFileName As String
        If pPrintMode = "P" Then
            mFP = Shell(My.Application.Info.DirectoryPath & "\PrintINV.bat", AppWinStyle.NormalFocus) '' '	
            If mFP = False Then GoTo ERR1
            '        Shell App.path & "\PrintINV.bat",vbNormalFocus	
        Else
            pFileName = mLocalPath & "\Invoice.Prn"
            Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
            'App.Path & "\RVIEW.EXE "	
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Function
    Public Function PrintPaintDetail(ByRef pPrintMode As String, ByRef pMKey As String) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        SqlStr = ""
        FileOpen(1, mLocalPath & "\Invoice.Prn", OpenMode.Output)
        SqlStr = MakePaintSQL(pMKey)

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalesDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RSSalesDetail.EOF = True Then
            MsgInformation("Nothing to print")
            PrintPaintDetail = False
            FileClose(1)
            Exit Function
        End If
        Call PrintPaintDetailPart(RSSalesDetail)

        FileClose(1)

        Dim mFP As Boolean
        Dim pFileName As String
        If pPrintMode = "P" Then
            mFP = Shell(My.Application.Info.DirectoryPath & "\PrintINV.bat", AppWinStyle.NormalFocus) '' '	
            If mFP = False Then GoTo ERR1
            '        Shell App.path & "\PrintINV.bat",vbNormalFocus	
        Else
            pFileName = mLocalPath & "\Invoice.Prn"
            Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
            'App.Path & "\RVIEW.EXE "	
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Function
    Private Sub PrintDetail(ByRef xPrintedFormat As String, ByRef xRsPaint As ADODB.Recordset, ByRef xPaintPrint As String, ByRef pExpAnnexPrint As String, ByRef pIsMRR As String, ByRef xRSJWDetail As ADODB.Recordset, ByRef xJWDetail As String, ByRef pIsPrintText As String, ByRef pPrintTextDesc As String, ByRef pAgtPermission As String, ByRef mExtraRemarks As String)
        On Error GoTo ERR1
        Dim pItemDesc As String
        Dim pPartNo As String
        Dim pUnit As String
        Dim pQty As String
        Dim pRate As String
        Dim pAmount As String
        Dim mSno As Integer
        Dim p57F4 As String = ""
        Dim p57F4Date As String = ""
        Dim mItemCode As String = ""
        Dim mItemNo As String = ""
        Dim pTotQty As String = ""
        Dim pMRP As String = ""
        Dim pMRPRate As String = ""
        Dim pMRPValue As Double
        Dim pTotMRPValue As Double

        Dim TabMRPRate As Integer
        Dim TabMRPAmount As Integer
        Dim TabMRP As Integer
        Dim mAbatementPer As Double

        Dim mDespRefType As String
        Dim mMRRNo As Double
        Dim mMRRSupplierCode As String = ""
        Dim mDSPCustCode As String
        Dim mBillNo As String = ""
        Dim mBillDate As String

        Dim mProcessCost As Double
        Dim mRMCost As Double
        Dim mTotProcessCost As Double
        Dim mTotRMCost As Double

        Dim mAVGRate As Double

        TabMRPRate = 102
        TabMRPAmount = 114
        TabMRP = 126
        mAbatementPer = IIf(IsDbNull(RSSalesDetail.Fields("ABATEMENT_PER").Value), 0, RSSalesDetail.Fields("ABATEMENT_PER").Value)

        mSno = 1

        If pExpAnnexPrint = "YI" Then
            pTotQty = New String(" ", TabRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("TOTQTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTQTY").Value), "0.000")

            Print(1, TAB(TabSNo), Chr(15) & 1)
            pItemDesc = "As Per Annexure Attached"
            PrintLine(1, TAB(TabIDesc), pItemDesc & Chr(18))
            PrintLine_Renamed = PrintLine_Renamed + 1
            GoTo NextLine
        ElseIf pIsPrintText = "Y" Then

            pTotQty = New String(" ", TabMRPRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("TOTQTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTQTY").Value), "0.000")

            If RSSalesDetail.Fields("TOTQTY").Value > 0 Then
                mAVGRate = CDbl(VB6.Format(RSSalesDetail.Fields("ITEMVALUE").Value / RSSalesDetail.Fields("TOTQTY").Value, "0.000"))
            Else
                mAVGRate = 0
            End If
            pRate = New String(" ", TabMRPAmount - TabMRPRate - Len(VB6.Format(mAVGRate, "0.0000"))) & VB6.Format(Trim(CStr(mAVGRate)), "0.0000")
            pAmount = New String(" ", TabMRP - TabMRPAmount - Len(VB6.Format(RSSalesDetail.Fields("ITEMVALUE").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEMVALUE").Value), "0.00")

            Print(1, TAB(TabSNo), Chr(15) & 1)
            pItemDesc = pPrintTextDesc
            PrintLine(1, TAB(TabIDesc), pItemDesc)
            Print(1, TAB(TabQty), pTotQty)

            Print(1, TAB(TabRate), pRate)
            PrintLine(1, TAB(TabAmount), pAmount & Chr(18))
            '	
            '	
            '        If pIsMRR = "N" Then	
            '            Print #1, Tab(TabRate); pRate;	
            '        Else	
            '            Print #1, Tab(TabMRPRate); pRate;	
            '        End If	
            '	
            '        If xPrintedFormat = "Y" Then	
            '            If pIsMRR = "N" Then	
            '                Print #1, Tab(TabAmount); pAmount & Chr(18)	
            '            Else	
            '                Print #1, Tab(TabMRPAmount); pAmount;	
            '                Print #1, Tab(TabMRP); pMRP & Chr(18)	
            '            End If	
            '            PrintLine = PrintLine + 1	
            '        Else	
            '            Print #1, Tab(TabAmount); pAmount;	
            '            Print #1, Tab(TabLastCol + 2); p57F4	
            '            PrintLine = PrintLine + 1	
            '            Print #1, Tab(TabLastCol + 2); p57F4Date & Chr(18)	
            '            PrintLine = PrintLine + 1	
            '        End If	
            '	
            '	
            PrintLine_Renamed = PrintLine_Renamed + 1
            GoTo NextLine
        End If

        Do While Not RSSalesDetail.EOF

            If pIsMRR = "N" Then
                pTotQty = New String(" ", TabRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("TOTQTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTQTY").Value), "0.000")
            Else
                pTotQty = New String(" ", TabMRPRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("TOTQTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTQTY").Value), "0.000")
            End If

            pItemDesc = IIf(IsDbNull(RSSalesDetail.Fields("ITEM_DESC").Value), "", RSSalesDetail.Fields("ITEM_DESC").Value)

            mItemCode = IIf(IsDbNull(RSSalesDetail.Fields("ITEM_CODE").Value), "", RSSalesDetail.Fields("ITEM_CODE").Value)

            If RSSalesDetail.Fields("REF_DESP_TYPE").Value = "P" Or RSSalesDetail.Fields("REF_DESP_TYPE").Value = "S" Then
                mProcessCost = GetSaleProcessCost(mItemCode, RSSalesDetail.Fields("INVOICE_DATE").Value, IIf(IsDbNull(RSSalesDetail.Fields("OUR_AUTO_KEY_SO").Value), 0, RSSalesDetail.Fields("OUR_AUTO_KEY_SO").Value), RSSalesDetail.Fields("SUPP_CUST_CODE").Value)
            End If

            'UPGRADE_WARNING: Untranslated statement in PrintDetail. Please check source code.	

            If Trim(mItemNo) <> "" Then
                pItemDesc = pItemDesc & " - " & mItemNo
            End If

            pPartNo = IIf(IsDbNull(RSSalesDetail.Fields("CUSTOMER_PART_NO").Value), "", RSSalesDetail.Fields("CUSTOMER_PART_NO").Value)
            pUnit = Trim(IIf(IsDbNull(RSSalesDetail.Fields("ITEM_UOM").Value), "", RSSalesDetail.Fields("ITEM_UOM").Value))

            If pIsMRR = "N" Then
                pQty = New String(" ", TabRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("ITEM_QTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_QTY").Value), "0.000")
                pRate = New String(" ", TabAmount - TabRate - Len(VB6.Format(RSSalesDetail.Fields("ITEM_RATE").Value, "0.0000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_RATE").Value), "0.0000")
                pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("ITEM_AMT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_AMT").Value), "0.00")
            Else
                pMRPValue = Val(RSSalesDetail.Fields("ITEM_MRP").Value) * Val(RSSalesDetail.Fields("ITEM_QTY").Value)
                pMRPValue = pMRPValue * (100 - mAbatementPer) / 100

                pTotMRPValue = pTotMRPValue + pMRPValue
                pQty = New String(" ", TabMRPRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("ITEM_QTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_QTY").Value), "0.000")
                pRate = New String(" ", TabMRPAmount - TabMRPRate - Len(VB6.Format(RSSalesDetail.Fields("ITEM_RATE").Value, "0.0000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_RATE").Value), "0.0000")
                pMRPRate = New String(" ", TabMRPAmount - TabMRPRate - Len(VB6.Format(RSSalesDetail.Fields("ITEM_MRP").Value, "0.0000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_MRP").Value), "0.0000")

                pAmount = New String(" ", TabMRP - TabMRPAmount - Len(VB6.Format(RSSalesDetail.Fields("ITEM_AMT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_AMT").Value), "0.00")
                pMRP = New String(" ", TabLastCol + 3 - TabMRP - Len(VB6.Format(pMRPValue, "0.00"))) & VB6.Format(Trim(CStr(pMRPValue)), "0.00")

            End If

            If mProcessCost > 0 Then
                mTotProcessCost = mTotProcessCost + (mProcessCost * RSSalesDetail.Fields("ITEM_QTY").Value)
                mTotRMCost = mTotRMCost + ((RSSalesDetail.Fields("ITEM_RATE").Value - mProcessCost) * RSSalesDetail.Fields("ITEM_QTY").Value)
            End If

            mDespRefType = Trim(IIf(IsDbNull(RSSalesDetail.Fields("REF_DESP_TYPE").Value), "", RSSalesDetail.Fields("REF_DESP_TYPE").Value))
            If mDespRefType = "Q" Or mDespRefType = "L" Then

                mMRRNo = CDbl(Trim(IIf(IsDbNull(RSSalesDetail.Fields("MRR_REF_NO").Value), -1, RSSalesDetail.Fields("MRR_REF_NO").Value)))
                'UPGRADE_WARNING: Untranslated statement in PrintDetail. Please check source code.	
                '            If MainClass.ValidateWithMasterTable(mMRRNo, "AUTO_KEY_MRR", "BILL_DATE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany!COMPANY_CODE & "") = True Then	
                '                mBillDate = MasterNo	
                '            End If	

                pItemDesc = pItemDesc & " - Agt Bill: " & mBillNo ''& " Dt. " & mBillDate	
            Else
                If xPrintedFormat = "Y" Then
                    p57F4 = Trim(IIf(IsDbNull(RSSalesDetail.Fields("REF_NO").Value), "", RSSalesDetail.Fields("REF_NO").Value))
                    mMRRNo = CDbl(Trim(IIf(IsDbNull(RSSalesDetail.Fields("MRR_REF_NO").Value), -1, RSSalesDetail.Fields("MRR_REF_NO").Value)))
                    mDSPCustCode = Trim(IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_CODE").Value), "", RSSalesDetail.Fields("SUPP_CUST_CODE").Value))
                    'UPGRADE_WARNING: Untranslated statement in PrintDetail. Please check source code.	

                    If Trim(mDSPCustCode) = Trim(mMRRSupplierCode) Then
                        If Trim(p57F4) <> "" And p57F4 <> "0" Then
                            pItemDesc = pItemDesc & " - Agt Bill:" & p57F4
                        End If
                    End If
                Else
                    mMRRNo = CDbl(Trim(IIf(IsDbNull(RSSalesDetail.Fields("MRR_REF_NO").Value), "", RSSalesDetail.Fields("MRR_REF_NO").Value)))
                    mDSPCustCode = Trim(IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_CODE").Value), "", RSSalesDetail.Fields("SUPP_CUST_CODE").Value))
                    'UPGRADE_WARNING: Untranslated statement in PrintDetail. Please check source code.	

                    If Trim(mDSPCustCode) = Trim(mMRRSupplierCode) Then
                        p57F4 = Trim(IIf(IsDbNull(RSSalesDetail.Fields("REF_NO").Value), "", RSSalesDetail.Fields("REF_NO").Value))
                        p57F4Date = VB6.Format(Trim(IIf(IsDbNull(RSSalesDetail.Fields("REF_DATE").Value), "", RSSalesDetail.Fields("REF_DATE").Value)), "DD/MM/YYYY")
                    End If
                End If
            End If

            Print(1, TAB(TabSNo), Chr(15) & mSno)
            pItemDesc = GetMultiLine(pItemDesc, PrintLine_Renamed, TabIPart - TabIDesc, TabIDesc)
            Print(1, TAB(TabIDesc), pItemDesc)
            Print(1, TAB(TabIPart), pPartNo)
            Print(1, TAB(TabUnit), pUnit)
            Print(1, TAB(TabQty), pQty)

            If pIsMRR = "N" Then
                Print(1, TAB(TabRate), pRate)
            Else
                Print(1, TAB(TabMRPRate), pRate)
            End If

            If xPrintedFormat = "Y" Then
                If pIsMRR = "N" Then
                    PrintLine(1, TAB(TabAmount), pAmount & Chr(18))
                Else
                    Print(1, TAB(TabMRPAmount), pAmount)
                    PrintLine(1, TAB(TabMRP), pMRP & Chr(18))
                End If
                PrintLine_Renamed = PrintLine_Renamed + 1
            Else
                Print(1, TAB(TabAmount), pAmount)
                PrintLine(1, TAB(TabLastCol + 2), p57F4)
                PrintLine_Renamed = PrintLine_Renamed + 1
                PrintLine(1, TAB(TabLastCol + 2), p57F4Date & Chr(18))
                PrintLine_Renamed = PrintLine_Renamed + 1
            End If

            If pIsMRR = "Y" Then
                Print(1, TAB(TabSNo), Chr(15))
                PrintLine(1, TAB(TabMRPRate), pMRPRate & "*" & Chr(18))
                PrintLine_Renamed = PrintLine_Renamed + 1
            End If

            RSSalesDetail.MoveNext()

            If Not RSSalesDetail.EOF Then
                If xPrintedFormat = "Y" Then
                    If pIsMRR = "N" Then
                        PrintLine(1, " ")
                        PrintLine_Renamed = PrintLine_Renamed + 1
                    End If
                End If
            End If
            mSno = mSno + 1
        Loop

NextLine:



        Print(1, TAB(0), Chr(15))
        If pIsMRR = "N" Then
            PrintLine(1, TAB(TabQty), New String("-", TabRate - TabQty))
        Else
            Print(1, TAB(TabQty), New String("-", TabMRPRate - TabQty))
            PrintLine(1, TAB(TabMRP), New String("-", TabLastCol + 3 - TabMRP))
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        If pIsMRR = "N" Then
            PrintLine(1, TAB(TabQty), pTotQty & Chr(18))
        Else
            Print(1, TAB(TabQty), pTotQty)
            PrintLine(1, TAB(TabMRP), New String(" ", TabLastCol + 3 - TabMRP - Len(VB6.Format(pTotMRPValue, "0.00"))) & VB6.Format(Trim(CStr(pTotMRPValue)), "0.00") & Chr(18))
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(TabQty); String(TabRate - TabQty, "=") & Chr(18)	
        '    PrintLine = PrintLine + 1	

        If xPaintPrint = "Y" Then
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            Call PrintPaintDetailPart(xRsPaint)
        End If

        If xJWDetail = "Y" Then
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            Call PrintJWDetailPart(xRSJWDetail)
        End If
        If mExtraRemarks <> "" Then
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(TabIDesc), Chr(15) & "Remarks : " & Chr(18))
            PrintLine(1, TAB(TabIDesc), mExtraRemarks) ''& Chr(18)	
            PrintLine_Renamed = PrintLine_Renamed + 4
        End If

        If mTotProcessCost > 0 Then
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            '        Print #1, Tab(TabIDesc); " Own Material Used - Rs. : " & Format(mTotRMCost, "0.00")	
            '        PrintLine = PrintLine + 1	
            '	
            '        Print #1, Tab(0); " Process Cost for TDS Calculation - Rs. : " & Format(mTotProcessCost, "0.00")	
            '        PrintLine = PrintLine + 1	
            '	
            '        Print #1, Tab(0); " TDS on Job Work U/s 194C @2% - Rs. : " & Format(mTotProcessCost * 2 / 100, "0.00")	
            '        PrintLine = PrintLine + 1	

            PrintLine(1, TAB(TabIDesc), Chr(15) & New String("-", 84) & Chr(18))
            PrintLine_Renamed = PrintLine_Renamed + 1

            Print(1, TAB(TabIDesc), Chr(15) & "OWN MATERIAL COST")
            Print(1, TAB(TabIDesc + 30), "PROCESS COST FOR TDS")
            PrintLine(1, TAB(TabIDesc + 60), "TDS ON JOBWORK U/s 194C @2%" & Chr(18))
            PrintLine_Renamed = PrintLine_Renamed + 1

            Print(1, TAB(TabIDesc), Chr(15) & "(RS) : " & VB6.Format(mTotRMCost, "0.00"))
            Print(1, TAB(TabIDesc + 30), "(RS) : " & VB6.Format(mTotProcessCost, "0.00"))
            PrintLine(1, TAB(TabIDesc + 60), "(RS) : " & VB6.Format(mTotProcessCost * 2 / 100, "0.00"))
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(TabIDesc), Chr(15) & New String("-", 84) & Chr(18))
            PrintLine_Renamed = PrintLine_Renamed + 1

        End If

        '    If pAgtPermission = "Y" Then	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "Removed from the premises of job-worker :" & Chr(18)	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "M/s EUROTHERM HEMA RADIATORS INDIA LTD. UNIT-II" & Chr(18)	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "PLOT NO. 5 & 14, SECTOR-6, HSIDC INDUSTRIAL COMPLEX, BAWAL (REWARI) HARYANA" & Chr(18)	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "Central Excise Regn. No.AABCE3677REM002" & Chr(18)	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "as permitted by the of Assistant/Deputy Commissioner,Central Excise,Division under" & Chr(18)	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "F.NO.C.NO.CE/Tech/Div-III/GGN-I/Jobwork/23/14-15/569 Dt.26/03/2015,Rule4(6) of the Cenvat Credit Rules,2004" & Chr(18)	
        '    End If	

        If PrintLine_Renamed < 43 Then
            Do While PrintLine_Renamed <> 43
                If PrintLine_Renamed >= 43 Then Exit Do
                PrintLine(1, TAB(0), " ")
                PrintLine_Renamed = PrintLine_Renamed + 1
            Loop
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub
    Private Function GetSaleProcessCost(ByRef pItemCode As String, ByRef pBillDate As String, ByRef pAutoSONO As Double, ByRef xCustomerCode As String) As Double
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = "SELECT PROCESS_COST AS PROCESS_COST FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND IH.AUTO_KEY_SO=" & Val(CStr(pAutoSONO)) & " AND SO_APPROVED='Y'" & vbCrLf & " AND IH.MKEY = ("


        SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf & " AND SID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND SIH.AUTO_KEY_SO=" & Val(CStr(pAutoSONO)) & " AND SO_APPROVED='Y'" & vbCrLf & " AND SID.AMEND_WEF <='" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSaleProcessCost = Val(IIf(IsDbNull(RsTemp.Fields("PROCESS_COST").Value), 0, RsTemp.Fields("PROCESS_COST").Value))
        Else
            GetSaleProcessCost = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetSaleProcessCost = 0
    End Function
    Private Sub PrintDetail_PlainPaper(ByRef xPrintedFormat As String, ByRef xRsPaint As ADODB.Recordset, ByRef xPaintPrint As String, ByRef pExpAnnexPrint As String, ByRef pIsMRR As String, ByRef xRSJWDetail As ADODB.Recordset, ByRef xJWDetail As String, ByRef pAgtPermission As String)
        On Error GoTo ERR1
        Dim pItemDesc As String
        Dim pPartNo As String
        Dim pUnit As String
        Dim pQty As String
        Dim pRate As String
        Dim pAmount As String
        Dim mSno As Integer
        Dim p57F4 As String
        Dim p57F4Date As String = ""
        Dim mItemCode As String = ""
        Dim mItemNo As String = ""
        Dim pTotQty As String = ""
        Dim pMRP As String = ""
        Dim pMRPRate As String = ""
        Dim pMRPValue As Double
        Dim pTotMRPValue As Double

        Dim TabMRPRate As Integer
        Dim TabMRPAmount As Integer
        Dim TabMRP As Integer
        Dim mAbatementPer As Double


        TabMRPRate = 102
        TabMRPAmount = 114
        TabMRP = 126
        mAbatementPer = IIf(IsDbNull(RSSalesDetail.Fields("ABATEMENT_PER").Value), 0, RSSalesDetail.Fields("ABATEMENT_PER").Value)

        mSno = 1

        If pExpAnnexPrint = "YI" Then
            pTotQty = New String(" ", TabRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("TOTQTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTQTY").Value), "0.000")

            Print(1, TAB(TabSNo), 1)
            pItemDesc = "As Per Annexure Attached"
            PrintLine(1, TAB(TabIDesc), pItemDesc)
            PrintLine_Renamed = PrintLine_Renamed + 1
            GoTo NextLine
        End If

        Do While Not RSSalesDetail.EOF

            If pIsMRR = "N" Then
                pTotQty = New String(" ", TabRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("TOTQTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTQTY").Value), "0.000")
            Else
                pTotQty = New String(" ", TabMRPRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("TOTQTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTQTY").Value), "0.000")
            End If

            pItemDesc = IIf(IsDbNull(RSSalesDetail.Fields("ITEM_DESC").Value), "", RSSalesDetail.Fields("ITEM_DESC").Value)

            mItemCode = IIf(IsDbNull(RSSalesDetail.Fields("ITEM_CODE").Value), "", RSSalesDetail.Fields("ITEM_CODE").Value)
            'UPGRADE_WARNING: Untranslated statement in PrintDetail_PlainPaper. Please check source code.	

            If Trim(mItemNo) <> "" Then
                pItemDesc = pItemDesc & " - " & mItemNo
            End If

            pPartNo = IIf(IsDbNull(RSSalesDetail.Fields("CUSTOMER_PART_NO").Value), "", RSSalesDetail.Fields("CUSTOMER_PART_NO").Value)
            pUnit = Trim(IIf(IsDbNull(RSSalesDetail.Fields("ITEM_UOM").Value), "", RSSalesDetail.Fields("ITEM_UOM").Value))

            If pIsMRR = "N" Then
                pQty = New String(" ", TabRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("ITEM_QTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_QTY").Value), "0.000")
                pRate = New String(" ", TabAmount - TabRate - Len(VB6.Format(RSSalesDetail.Fields("ITEM_RATE").Value, "0.0000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_RATE").Value), "0.0000")
                pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("ITEM_AMT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_AMT").Value), "0.00")
            Else
                pMRPValue = Val(RSSalesDetail.Fields("ITEM_MRP").Value) * Val(RSSalesDetail.Fields("ITEM_QTY").Value)
                pMRPValue = pMRPValue * (100 - mAbatementPer) / 100

                pTotMRPValue = pTotMRPValue + pMRPValue
                pQty = New String(" ", TabMRPRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("ITEM_QTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_QTY").Value), "0.000")
                pRate = New String(" ", TabMRPAmount - TabMRPRate - Len(VB6.Format(RSSalesDetail.Fields("ITEM_RATE").Value, "0.0000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_RATE").Value), "0.0000")
                pMRPRate = New String(" ", TabMRPAmount - TabMRPRate - Len(VB6.Format(RSSalesDetail.Fields("ITEM_MRP").Value, "0.0000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_MRP").Value), "0.0000")

                pAmount = New String(" ", TabMRP - TabMRPAmount - Len(VB6.Format(RSSalesDetail.Fields("ITEM_AMT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_AMT").Value), "0.00")
                pMRP = New String(" ", TabLastCol + 3 - TabMRP - Len(VB6.Format(pMRPValue, "0.00"))) & VB6.Format(Trim(CStr(pMRPValue)), "0.00")

            End If

            If xPrintedFormat = "Y" Then
                p57F4 = Trim(IIf(IsDbNull(RSSalesDetail.Fields("REF_NO").Value), "", RSSalesDetail.Fields("REF_NO").Value))
                If Trim(p57F4) <> "" And p57F4 <> "0" Then
                    pItemDesc = pItemDesc & " - Agt Bill:" & p57F4
                End If
            Else
                p57F4 = Trim(IIf(IsDbNull(RSSalesDetail.Fields("REF_NO").Value), "", RSSalesDetail.Fields("REF_NO").Value))
                p57F4Date = VB6.Format(Trim(IIf(IsDbNull(RSSalesDetail.Fields("REF_DATE").Value), "", RSSalesDetail.Fields("REF_DATE").Value)), "DD/MM/YYYY")
            End If

            Print(1, TAB(TabSNo), mSno)

            pItemDesc = GetMultiLine(pItemDesc, PrintLine_Renamed, TabIPart - TabIDesc, TabIDesc)
            Print(1, TAB(TabIDesc), pItemDesc)
            Print(1, TAB(TabIPart), pPartNo)
            Print(1, TAB(TabUnit), pUnit)
            Print(1, TAB(TabQty), pQty)

            If pIsMRR = "N" Then
                Print(1, TAB(TabRate), pRate)
            Else
                Print(1, TAB(TabMRPRate), pRate)
            End If

            If xPrintedFormat = "Y" Then
                If pIsMRR = "N" Then
                    PrintLine(1, TAB(TabAmount), pAmount)
                Else
                    Print(1, TAB(TabMRPAmount), pAmount)
                    PrintLine(1, TAB(TabMRP), pMRP)
                End If
                PrintLine_Renamed = PrintLine_Renamed + 1
            Else
                Print(1, TAB(TabAmount), pAmount)
                PrintLine(1, TAB(TabLastCol + 2), p57F4)
                PrintLine_Renamed = PrintLine_Renamed + 1
                PrintLine(1, TAB(TabLastCol + 2), p57F4Date)
                PrintLine_Renamed = PrintLine_Renamed + 1
            End If

            If pIsMRR = "Y" Then
                Print(1, TAB(TabSNo), " ")
                PrintLine(1, TAB(TabMRPRate), pMRPRate & "*")
                PrintLine_Renamed = PrintLine_Renamed + 1
            End If

            RSSalesDetail.MoveNext()

            If Not RSSalesDetail.EOF Then
                If xPrintedFormat = "Y" Then
                    '                If pIsMRR = "N" Then	
                    '                    Print #1, " "	
                    '                    PrintLine = PrintLine + 1	
                    '                End If	
                End If
            End If
            mSno = mSno + 1
        Loop

NextLine:

        Print(1, TAB(0), " ")
        If pIsMRR = "N" Then
            PrintLine(1, TAB(TabQty), New String("-", TabRate - TabQty))
        Else
            Print(1, TAB(TabQty), New String("-", TabMRPRate - TabQty))
            PrintLine(1, TAB(TabMRP), New String("-", TabLastCol + 3 - TabMRP))
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        If pIsMRR = "N" Then
            PrintLine(1, TAB(TabQty), pTotQty) ''& Chr(18)	
        Else
            Print(1, TAB(TabQty), pTotQty)
            PrintLine(1, TAB(TabMRP), New String(" ", TabLastCol + 3 - TabMRP - Len(VB6.Format(pTotMRPValue, "0.00"))) & VB6.Format(Trim(CStr(pTotMRPValue)), "0.00")) ''& Chr(18)	
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(TabQty); String(TabRate - TabQty, "=") & Chr(18)	
        '    PrintLine = PrintLine + 1	

        If xPaintPrint = "Y" Then
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            Call PrintPaintDetailPart(xRsPaint)
        End If

        If xJWDetail = "Y" Then
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            Call PrintJWDetailPart(xRSJWDetail)
        End If

        '    If pAgtPermission = "Y" Then	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "Removed from the premises of job-worker as permitted by the of Assistant/Deputy Commissioner,Central Excise,Division" & Chr(18)	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "under F.NO.C.NO.CE/Tech/Div-III/GGN-I/Jobwork/23/14-15/569 Dt.26/03/2015,Rule4(6) of the Cenvat Credit Rules,2004" & Chr(18)	
        '	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "EUROTHERM HEMA RADIATORS INDIA LTD. UNIT-II" & Chr(18)	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "PLOT NO. 5 & 14, SECTOR-6, HSIDC INDUSTRIAL COMPLEX, BAWAL (REWARI) HARYANA" & Chr(18)	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "Central Excise Regn. No.AABCE3677REM002" & Chr(18)	
        '    End If	
        If PrintLine_Renamed < 44 Then ''43	
            Do While PrintLine_Renamed <> 44
                If PrintLine_Renamed >= 44 Then Exit Do
                PrintLine(1, TAB(0), " ")
                PrintLine_Renamed = PrintLine_Renamed + 1
            Loop
        End If

        PrintLine(1, TAB(3), New String("-", TabLastCol))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub


    Private Sub PrintAnnexDetail(ByRef xPrintedFormat As String, ByRef xRsPaint As ADODB.Recordset, ByRef xPaintPrint As String, ByRef xExpAnnexInvoice As String)
        On Error GoTo ERR1
        Dim pItemDesc As String
        Dim pPartNo As String
        Dim pUnit As String
        Dim pQty As String
        Dim pRate As String
        Dim pAmount As String
        Dim xMRPAmount As String
        Dim mSno As Integer
        Dim p57F4 As String
        Dim p57F4Date As String
        Dim mItemCode As String
        Dim mItemNo As String = ""
        Dim pTotQty As String = ""
        Dim pMRPAmount As Double
        Dim pMRPRate As String
        Dim xPageNo As Integer

        mSno = 1
        xPageNo = 1

        Do While Not RSSalesDetail.EOF
            pTotQty = New String(" ", TabAnnexRate - TabAnnexQty - Len(VB6.Format(RSSalesDetail.Fields("TOTQTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTQTY").Value), "0.000")

            pItemDesc = IIf(IsDbNull(RSSalesDetail.Fields("ITEM_DESC").Value), "", RSSalesDetail.Fields("ITEM_DESC").Value)

            mItemCode = IIf(IsDbNull(RSSalesDetail.Fields("ITEM_CODE").Value), "", RSSalesDetail.Fields("ITEM_CODE").Value)
            'UPGRADE_WARNING: Untranslated statement in PrintAnnexDetail. Please check source code.	

            If Trim(mItemNo) <> "" Then
                pItemDesc = pItemDesc & " - " & mItemNo
            End If

            pPartNo = IIf(IsDbNull(RSSalesDetail.Fields("CUSTOMER_PART_NO").Value), "", RSSalesDetail.Fields("CUSTOMER_PART_NO").Value)
            pUnit = Trim(IIf(IsDbNull(RSSalesDetail.Fields("ITEM_UOM").Value), "", RSSalesDetail.Fields("ITEM_UOM").Value))
            pQty = New String(" ", TabAnnexRate - TabAnnexQty - Len(VB6.Format(RSSalesDetail.Fields("ITEM_QTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_QTY").Value), "0.000")
            pRate = New String(" ", TabAnnexAmount - TabAnnexRate - Len(VB6.Format(RSSalesDetail.Fields("ITEM_RATE").Value, "0.0000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_RATE").Value), "0.0000")
            pMRPRate = New String(" ", TabAnnexMRPAmount - TabAnnexMRP - Len(VB6.Format(RSSalesDetail.Fields("ITEM_MRP").Value, "0.0000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_MRP").Value), "0.0000")

            pMRPAmount = IIf(IsDbNull(RSSalesDetail.Fields("ITEM_QTY").Value), 0, RSSalesDetail.Fields("ITEM_QTY").Value) * IIf(IsDbNull(RSSalesDetail.Fields("ITEM_MRP").Value), 0, RSSalesDetail.Fields("ITEM_MRP").Value)

            pAmount = New String(" ", TabAnnexMRP - TabAnnexAmount - Len(VB6.Format(RSSalesDetail.Fields("ITEM_AMT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_AMT").Value), "0.00")
            xMRPAmount = New String(" ", TabAnnexLastCol - TabAnnexMRPAmount - Len(VB6.Format(pMRPAmount, "0.00"))) & VB6.Format(Trim(CStr(pMRPAmount)), "0.00")

            Print(1, TAB(TaxAnnexSNo), Chr(15) & mSno)
            pItemDesc = GetMultiLine(pItemDesc, PrintLine_Renamed, TabAnnexUnit - TabAnnexIDesc, TabAnnexIDesc)
            Print(1, TAB(TabAnnexIDesc), pItemDesc)
            '        Print #1, Tab(TabAnnexIPart); pPartNo;	
            Print(1, TAB(TabAnnexUnit), pUnit)
            Print(1, TAB(TabAnnexQty), pQty)
            Print(1, TAB(TabAnnexRate), pRate)

            If xExpAnnexInvoice = "YA" Then
                PrintLine(1, TAB(TabAnnexMRPAmount), pAmount & Chr(18))
            Else
                Print(1, TAB(TabAnnexAmount), pAmount)
                Print(1, TAB(TabAnnexMRP), pMRPRate)
                PrintLine(1, TAB(TabAnnexMRPAmount), xMRPAmount & Chr(18))
            End If
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(TabAnnexIDesc), Chr(15) & pPartNo & Chr(18))
            PrintLine_Renamed = PrintLine_Renamed + 1

            If PrintLine_Renamed > 66 Then
                PrintLine(1, TAB(TabAnnexQty), Chr(15) & "Page No : " & xPageNo & Chr(18))
                PrintLine(1, TAB(0), "" & Chr(12))
                Call PrintAnnexHeader(xExpAnnexInvoice)
                '            Do While PrintLine <> 70	
                '                If PrintLine >= 70 Then Exit Do	
                '                Print #1, Tab(0); " "	
                '                PrintLine = PrintLine + 1	
                '            Loop	
                PrintLine_Renamed = 1
                xPageNo = xPageNo + 1
            End If
            RSSalesDetail.MoveNext()

            '        If Not RSSalesDetail.EOF Then	
            '            If xPrintedFormat = "Y" Then	
            '                Print #1, " "	
            '                PrintLine = PrintLine + 1	
            '            End If	
            '        End If	
            mSno = mSno + 1
        Loop

        Print(1, TAB(0), Chr(15))
        PrintLine(1, TAB(TabAnnexQty), New String("-", TabAnnexRate - TabAnnexQty))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabAnnexQty), pTotQty & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        If PrintLine_Renamed < 43 Then
            Do While PrintLine_Renamed <> 43
                If PrintLine_Renamed >= 43 Then Exit Do
                PrintLine(1, TAB(0), " ")
                PrintLine_Renamed = PrintLine_Renamed + 1
            Loop
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub

    Private Sub PrintSubsidiaryChallanDetail(ByRef pSC_All As String, ByRef pSC_F4No As String)
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim pItemDesc As String
        Dim pPartNo As String
        Dim pUnit As String
        Dim pQty As String
        Dim pRate As String
        Dim mSno As Integer
        Dim p57F4 As String
        Dim pBillNo As String = ""
        Dim pBillDate As String = ""
        Dim mItemCode As String
        Dim mItemNo As String
        Dim pTotQty As String
        Dim xPageNo As Integer
        Dim mString As String
        Dim xSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSuppCustCode As String
        Dim mDespNo As String

        mSno = 1
        xPageNo = 1

        Do While Not RSSalesDetail.EOF
            If pSC_All = "N" Then
                p57F4 = Trim(IIf(IsDbNull(RSSalesDetail.Fields("REF_NO").Value), "", RSSalesDetail.Fields("REF_NO").Value))
                If UCase(p57F4) <> Trim(UCase(pSC_F4No)) Then
                    GoTo NextRecd
                End If
            End If
            Call PrintSubsidiaryChallanHeader()

            '        pTotQty = String(TabSCChallanNo - TabSCQty - 1 - Len(Format(RSSalesDetail!TOTQTY, "0.000")), " ") & Format(Trim(RSSalesDetail!TOTQTY), "0.000")	

            pTotQty = New String(" ", TabSCChallanNo - TabSCQty - 1 - Len(VB6.Format(RSSalesDetail.Fields("ITEM_QTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_QTY").Value), "0.000")

            pItemDesc = IIf(IsDbNull(RSSalesDetail.Fields("ITEM_DESC").Value), "", RSSalesDetail.Fields("ITEM_DESC").Value)

            mItemCode = IIf(IsDbNull(RSSalesDetail.Fields("ITEM_CODE").Value), "", RSSalesDetail.Fields("ITEM_CODE").Value)
            pPartNo = IIf(IsDbNull(RSSalesDetail.Fields("CUSTOMER_PART_NO").Value), "", RSSalesDetail.Fields("CUSTOMER_PART_NO").Value)

            If Trim(pPartNo) <> "" Then
                pItemDesc = pItemDesc & " - " & pPartNo
            End If

            pUnit = Trim(IIf(IsDbNull(RSSalesDetail.Fields("ITEM_UOM").Value), "", RSSalesDetail.Fields("ITEM_UOM").Value))
            pQty = New String(" ", TabSCChallanNo - TabSCQty - 1 - Len(VB6.Format(RSSalesDetail.Fields("ITEM_QTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_QTY").Value), "0.000")

            p57F4 = Trim(IIf(IsDbNull(RSSalesDetail.Fields("REF_NO").Value), "", RSSalesDetail.Fields("REF_NO").Value))

            mSuppCustCode = Trim(IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_CODE").Value), "", RSSalesDetail.Fields("SUPP_CUST_CODE").Value))
            mDespNo = Trim(IIf(IsDbNull(RSSalesDetail.Fields("AUTO_KEY_DESP").Value), "-1", RSSalesDetail.Fields("AUTO_KEY_DESP").Value))

            'UPGRADE_WARNING: Untranslated statement in PrintSubsidiaryChallanDetail. Please check source code.	

            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                pBillDate = VB6.Format(Trim(IIf(IsDbNull(RsTemp.Fields("PARTY_F4DATE").Value), "", RsTemp.Fields("PARTY_F4DATE").Value)), "DD/MM/YYYY")
            End If

            xSqlStr = "SELECT PARTY_F4NO,PARTY_F4DATE,BILL_NO,BILL_DATE " & vbCrLf & " FROM DSP_PAINT57F4_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'" & vbCrLf & " AND " & vbCrLf & " PARTY_F4NO='" & MainClass.AllowSingleQuote(p57F4) & "'" & vbCrLf & " AND PARTY_F4DATE='" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "' AND ITEM_IO='I'"

            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                pBillNo = Trim(IIf(IsDbNull(RsTemp.Fields("BILL_NO").Value), "", RsTemp.Fields("BILL_NO").Value))
                pBillDate = VB6.Format(Trim(IIf(IsDbNull(RsTemp.Fields("BILL_DATE").Value), "", RsTemp.Fields("BILL_DATE").Value)), "DD/MM/YYYY")
            End If

            Print(1, TAB(TaxSCSNo), Chr(15) & mSno)
            pItemDesc = GetMultiLine(pItemDesc, PrintLine_Renamed, TabSCUnit - TabSCIDesc, TabSCIDesc)
            Print(1, TAB(TabSCIDesc), pItemDesc)
            Print(1, TAB(TabSCUnit), pUnit)
            Print(1, TAB(TabSCQty), pQty)
            Print(1, TAB(TabSCChallanNo), p57F4)
            Print(1, TAB(TabSCBillNo), pBillNo)
            PrintLine(1, TAB(TabSCChallanDate), pBillDate & Chr(18))

            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            '        If PrintLine > 66 Then	
            '            Print #1, Tab(TabAnnexQty); Chr(15) & "Page No : " & xPageNo & Chr(18)	
            '            Print #1, Tab(0); "" & Chr(12)	
            '            Call PrintSubsidiaryChallanHeader(xExpAnnexInvoice)	
            '            PrintLine = 1	
            '            xPageNo = xPageNo + 1	
            '        End If	


            Print(1, TAB(0), Chr(15))
            PrintLine(1, TAB(TabSCQty), New String("-", TabSCChallanNo - TabSCQty))
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(TabSCQty), pTotQty & Chr(18))
            PrintLine_Renamed = PrintLine_Renamed + 1

            If PrintLine_Renamed < 60 Then
                Do While PrintLine_Renamed < 60
                    PrintLine(1, TAB(0), " ")
                    PrintLine_Renamed = PrintLine_Renamed + 1
                Loop
            End If

            mString = New String("-", TabSCLastCol - TaxSCSNo)
            Print(1, TAB(TaxSCSNo), Chr(15) & mString & Chr(18))
            PrintLine_Renamed = PrintLine_Renamed + 1

            mString = "For " & RsCompany.Fields("Company_Name").Value
            mString = New String(" ", (TabSCLastCol - TaxSCSNo - Len(mString) * 2) / 2) & mString
            PrintLine(1, TAB(3), Chr(18) & Chr(15) & Chr(14) & mString & Chr(18) & Chr(15))

            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1


            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1


            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1


            mString = "Authorised Signatory"
            mString = New String(" ", (TabSCLastCol - TaxSCSNo - Len(mString) * 2) / 2) & mString
            PrintLine(1, TAB(3), Chr(18) & Chr(15) & Chr(14) & mString & Chr(18) & Chr(15) & Chr(12))

            '         PrintLine = PrintLine + 1	
            '         mSno = mSno + 1	
NextRecd:
            RSSalesDetail.MoveNext()
            PrintLine_Renamed = 1
            mSno = 1
        Loop
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub
    Private Sub PrintSuppDetail(ByRef xPrintedFormat As String, ByRef xRsPaint As ADODB.Recordset, ByRef xPaintPrint As String)
        On Error GoTo ERR1
        Dim pItemDesc As String
        Dim pAmount As String
        Dim mSno As Integer

        mSno = 1

        Do While Not RSSalesDetail.EOF
            pItemDesc = IIf(IsDbNull(RSSalesDetail.Fields("REMARKS").Value), "", RSSalesDetail.Fields("REMARKS").Value)
            pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("SUPPITEMTOT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("SUPPITEMTOT").Value), "0.00")


            PrintLine_Renamed = PrintLine_Renamed + 1

            Print(1, TAB(TabSNo), Chr(15) & mSno)
            pItemDesc = GetMultiLine(pItemDesc, PrintLine_Renamed, TabIPart - TabIDesc, TabIDesc)
            Print(1, TAB(TabIDesc), pItemDesc)
            If xPrintedFormat = "Y" Then
                PrintLine(1, TAB(TabAmount), pAmount & Chr(18))
                PrintLine_Renamed = PrintLine_Renamed + 1
                PrintLine(1, " ")
            Else
                PrintLine(1, TAB(TabAmount), pAmount & Chr(18))
                PrintLine_Renamed = PrintLine_Renamed + 1
                PrintLine(1, " ")
            End If

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, " ")

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, "") ''" ********  SUPPLEMENTARY INVOICE  ******** "	

            RSSalesDetail.MoveNext()
            mSno = mSno + 1
        Loop

        Do While PrintLine_Renamed <> 43
            If PrintLine_Renamed >= 43 Then Exit Do
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub

    Private Sub PrintJWDetail(ByRef xPrintedFormat As String, ByRef xRsPaint As ADODB.Recordset, ByRef xPaintPrint As String, ByRef pExpAnnexPrint As String, ByRef xRSJWDetail As ADODB.Recordset, ByRef xJWDetail As String, Optional ByRef mJWRemarks As String = "", Optional ByRef pAgtPermission As String = "")
        On Error GoTo ERR1
        Dim pItemDesc As String
        Dim pPartNo As String
        Dim pUnit As String
        Dim pQty As String
        Dim pRate As String
        Dim pAmount As String
        Dim mSno As Integer
        Dim p57F4 As String
        Dim p57F4Date As String = ""
        Dim mItemCode As String
        Dim mItemNo As String = ""
        Dim pTotQty As String = ""

        mSno = 1
        If pExpAnnexPrint = "YI" Then
            pTotQty = New String(" ", TabRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("TOTQTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTQTY").Value), "0.000")
            Print(1, TAB(TabSNo), Chr(15) & 1)
            pItemDesc = "As Per Annexure Attached"
            PrintLine(1, TAB(TabIDesc), pItemDesc & Chr(18))
            PrintLine_Renamed = PrintLine_Renamed + 1
            GoTo NextLine
        End If

        Do While Not RSSalesDetail.EOF

            pTotQty = New String(" ", TabRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("TOTQTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTQTY").Value), "0.000")

            pItemDesc = IIf(IsDbNull(RSSalesDetail.Fields("ITEM_DESC").Value), "", RSSalesDetail.Fields("ITEM_DESC").Value)

            mItemCode = IIf(IsDbNull(RSSalesDetail.Fields("ITEM_CODE").Value), "", RSSalesDetail.Fields("ITEM_CODE").Value)
            'UPGRADE_WARNING: Untranslated statement in PrintJWDetail. Please check source code.	

            If Trim(mItemNo) <> "" Then
                pItemDesc = pItemDesc & " - " & mItemNo
            End If

            pPartNo = IIf(IsDbNull(RSSalesDetail.Fields("CUSTOMER_PART_NO").Value), "", RSSalesDetail.Fields("CUSTOMER_PART_NO").Value)

            pUnit = Trim(IIf(IsDbNull(RSSalesDetail.Fields("ITEM_UOM").Value), "", RSSalesDetail.Fields("ITEM_UOM").Value))
            pQty = New String(" ", TabRate - TabQty - Len(VB6.Format(RSSalesDetail.Fields("ITEM_QTY").Value, "0.000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_QTY").Value), "0.000")
            pRate = New String(" ", TabAmount - TabRate - Len(VB6.Format(RSSalesDetail.Fields("ITEM_RATE").Value, "0.0000"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_RATE").Value), "0.0000")
            pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("ITEM_AMT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEM_AMT").Value), "0.00")

            If xPrintedFormat = "Y" Then
                p57F4 = Trim(IIf(IsDbNull(RSSalesDetail.Fields("REF_NO").Value), "", RSSalesDetail.Fields("REF_NO").Value))
                If Trim(p57F4) <> "" Then
                    pItemDesc = pItemDesc & " - Agt Bill:" & p57F4
                End If
            Else
                p57F4 = Trim(IIf(IsDbNull(RSSalesDetail.Fields("REF_NO").Value), "", RSSalesDetail.Fields("REF_NO").Value))
                p57F4Date = VB6.Format(Trim(IIf(IsDbNull(RSSalesDetail.Fields("REF_DATE").Value), "", RSSalesDetail.Fields("REF_DATE").Value)), "DD/MM/YYYY")
            End If

            PrintLine_Renamed = PrintLine_Renamed + 1

            Print(1, TAB(TabSNo), Chr(15) & mSno)
            pItemDesc = pItemDesc & " / " & pPartNo
            pItemDesc = GetMultiLine(pItemDesc, PrintLine_Renamed, TabUnit - 18 - TabIDesc, TabIDesc)
            Print(1, TAB(TabIDesc), pItemDesc) '' & " - " & pPartNo;	
            '        Print #1, Tab(TabIPart); pPartNo;	
            Print(1, TAB(TabUnit - 18), pUnit)
            Print(1, TAB(TabQty - 17), pQty)
            Print(1, TAB(TabRate - 17), pRate)
            If xPrintedFormat = "Y" Then
                PrintLine(1, TAB(TabAmount - 15), pAmount & Chr(18))
                PrintLine_Renamed = PrintLine_Renamed + 1
                PrintLine(1, " ")
                '            PrintLine = PrintLine + 1	
            Else
                Print(1, TAB(TabAmount - 15), pAmount)
                PrintLine(1, TAB(TabLastCol - 10), p57F4)
                PrintLine_Renamed = PrintLine_Renamed + 1
                PrintLine(1, TAB(TabLastCol - 10), p57F4Date & Chr(18))
                '            PrintLine = PrintLine + 1	
            End If


            RSSalesDetail.MoveNext()
            mSno = mSno + 1
        Loop

NextLine:

        Print(1, TAB(0), Chr(15))
        PrintLine(1, TAB(TabQty - 17), New String("-", TabRate - TabQty))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabQty - 17), pTotQty & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        If xPaintPrint = "Y" Then
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            Call PrintPaintDetailPart(xRsPaint)
        End If

        If xJWDetail = "Y" Then
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            Call PrintJWDetailPart(xRSJWDetail)
        End If

        If mJWRemarks <> "" Then
            PrintLine(1, TAB(0), mJWRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        '    If pAgtPermission = "Y" Then	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "Removed from the premises of job-worker as permitted by the of Assistant/Deputy Commissioner,Central Excise,Division" & Chr(18)	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "under F.NO.C.NO.CE/Tech/Div-III/GGN-I/Jobwork/23/14-15/569 Dt.26/03/2015,Rule4(6) of the Cenvat Credit Rules,2004" & Chr(18)	
        '	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "EUROTHERM HEMA RADIATORS INDIA LTD. UNIT-II" & Chr(18)	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "PLOT NO. 5 & 14, SECTOR-6, HSIDC INDUSTRIAL COMPLEX, BAWAL (REWARI) HARYANA" & Chr(18)	
        '	
        '        PrintLine = PrintLine + 1	
        '        Print #1, Tab(TabIDesc); Chr(15) & "Central Excise Regn. No.AABCE3677REM002" & Chr(18)	
        '    End If	

        Do While PrintLine_Renamed <> 44
            If PrintLine_Renamed >= 44 Then Exit Do
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub
    Private Sub PrintSuppJWDetail(ByRef xPrintedFormat As String, ByRef xRsPaint As ADODB.Recordset, ByRef xPaintPrint As String)
        On Error GoTo ERR1
        Dim pItemDesc As String
        Dim pAmount As String
        Dim mSno As Integer

        mSno = 1

        Do While Not RSSalesDetail.EOF
            pItemDesc = IIf(IsDbNull(RSSalesDetail.Fields("REMARKS").Value), "", RSSalesDetail.Fields("REMARKS").Value)

            pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("SUPPITEMTOT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("SUPPITEMTOT").Value), "0.00")

            PrintLine_Renamed = PrintLine_Renamed + 1

            Print(1, TAB(TabSNo), Chr(15) & mSno)
            pItemDesc = pItemDesc
            pItemDesc = GetMultiLine(pItemDesc, PrintLine_Renamed, TabUnit - 18 - TabIDesc, TabIDesc)
            Print(1, TAB(TabIDesc), pItemDesc) '' & " - " & pPartNo;	

            If xPrintedFormat = "Y" Then
                PrintLine(1, TAB(TabAmount - 15), pAmount & Chr(18))
                PrintLine_Renamed = PrintLine_Renamed + 1
                PrintLine(1, " ")
            Else
                PrintLine(1, TAB(TabAmount - 15), pAmount & Chr(18))
                PrintLine_Renamed = PrintLine_Renamed + 1
                PrintLine(1, " ")
            End If

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, " ")

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, " ********  SUPPLEMENTARY INVOICE  ******** ")

            RSSalesDetail.MoveNext()
            mSno = mSno + 1
        Loop

        Do While PrintLine_Renamed <> 43
            If PrintLine_Renamed >= 43 Then Exit Do
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub
    Private Sub PrintPaintDetailPart(ByRef pRsPaint As ADODB.Recordset)
        On Error GoTo ERR1
        Dim mBillNo As String
        Dim pItemDesc As String

        Dim pPaintItemCode As String
        Dim pPaintItemDesc As String
        Dim pQtyConsmed As String
        Dim p57F4 As String

        '        mBillNo = "Bill No : " & IIf(IsNull(pRsPaint!BILL_No), "", Format(Mid(pRsPaint!BILL_No, 2), "000000"))	
        '        mBillNo = mBillNo & " DT. " & IIf(IsNull(pRsPaint!BILL_DATE), "", pRsPaint!BILL_DATE)	
        '        mBillNo = mBillNo & " Qty: " & IIf(IsNull(pRsPaint!BILL_QTY), "", pRsPaint!BILL_QTY)	
        '        Print #1, Tab(TabIDesc); Chr(15) & mBillNo & Chr(18)	
        '        PrintLine = PrintLine + 1	
        '	
        '        pItemDesc = "ITEM DESC: " & IIf(IsNull(pRsPaint!JOBITEM), "", pRsPaint!JOBITEM)	
        '        Print #1, Tab(TabIDesc); Chr(15) & pItemDesc & Chr(18)	
        '        PrintLine = PrintLine + 1	

        PrintLine(1, TAB(TabIDesc), Chr(15) & New String("-", 84) & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(TabIDesc), Chr(15) & "MTRL CODE")
        Print(1, TAB(TabIDesc + 11), "DESCRIPTION")
        Print(1, TAB(TabIDesc + 61), "MTRL CONSMED")
        PrintLine(1, TAB(TabIDesc + 74), "57F(4) No." & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabIDesc), Chr(15) & New String("-", 84) & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Do While Not pRsPaint.EOF
            pPaintItemCode = IIf(IsDbNull(pRsPaint.Fields.Item("PaintPartNo").Value), "", pRsPaint.Fields.Item("PaintPartNo").Value)
            pPaintItemDesc = IIf(IsDbNull(pRsPaint.Fields.Item("PAINTITEMDESC").Value), "", pRsPaint.Fields.Item("PAINTITEMDESC").Value)
            pQtyConsmed = New String(" ", 12 - Len(VB6.Format(pRsPaint.Fields("ITEM_QTY").Value, "0.0000"))) & VB6.Format(Trim(pRsPaint.Fields("ITEM_QTY").Value), "0.0000")
            p57F4 = IIf(IsDbNull(pRsPaint.Fields.Item("PARTY_F4NO").Value), "", pRsPaint.Fields.Item("PARTY_F4NO").Value)

            PrintLine_Renamed = PrintLine_Renamed + 1

            Print(1, TAB(TabIDesc), Chr(15) & pPaintItemCode)
            '        pItemDesc = GetMultiLine(pItemDesc, PrintLine, TabIPart - TabIDesc, TabIDesc)	
            Print(1, TAB(TabIDesc + 11), Left(pPaintItemDesc, 49))
            Print(1, TAB(TabIDesc + 61), pQtyConsmed)
            PrintLine(1, TAB(TabIDesc + 74), p57F4 & Chr(18))

            pRsPaint.MoveNext()
        Loop

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub

    Private Sub PrintJWDetailPart(ByRef pRsJWDetail As ADODB.Recordset)
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim mBillNo As String
        Dim pItemDesc As String
        Dim pFGItemCode As String
        Dim pJWItemCode As String
        Dim pJWItemDesc As String
        Dim pQtyConsmed As Double
        Dim p57F4 As String

        Dim mSqlStr As String
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim mDCNo As Double
        Dim mMKey As String
        Dim mStdQty As Double
        Dim mScrapQty As Double
        Dim mItemQty As Double
        Dim mTotScrapQty As Double
        Dim mUnitFactor As Double
        Dim mInvoiceDate As String = ""

        PrintLine(1, TAB(TabIDesc), Chr(15) & New String("-", 84) & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(TabIDesc), Chr(15) & "MTRL CODE")
        Print(1, TAB(TabIDesc + 10), "DESCRIPTION")
        Print(1, TAB(TabIDesc + 50), New String(" ", 12 - Len("MTRL CONSMED")) & "MTRL CONSMED") ''"MTRL CONSMED";	
        Print(1, TAB(TabIDesc + 63), New String(" ", 10 - Len("SCRAP")) & "SCRAP")
        PrintLine(1, TAB(TabIDesc + 74), "57F(4) No." & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabIDesc), Chr(15) & New String("-", 84) & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Do While Not pRsJWDetail.EOF
            mDCNo = IIf(IsDbNull(pRsJWDetail.Fields.Item("mKey").Value), "-1", pRsJWDetail.Fields.Item("mKey").Value)
            pFGItemCode = IIf(IsDbNull(pRsJWDetail.Fields.Item("SUB_ITEM_CODE").Value), "", pRsJWDetail.Fields.Item("SUB_ITEM_CODE").Value)
            pJWItemCode = IIf(IsDbNull(pRsJWDetail.Fields.Item("ITEM_CODE").Value), "", pRsJWDetail.Fields.Item("ITEM_CODE").Value)
            pJWItemDesc = IIf(IsDbNull(pRsJWDetail.Fields.Item("PAINTITEMDESC").Value), "", pRsJWDetail.Fields.Item("PAINTITEMDESC").Value)
            '        pQtyConsmed = String(9 - Len(Format(pRsJWDetail!ITEM_QTY, "0.0000")), " ") & Format(Trim(pRsJWDetail!ITEM_QTY), "0.0000")	
            pQtyConsmed = CDbl(VB6.Format(Trim(pRsJWDetail.Fields("ITEM_QTY").Value), "0.0000"))
            p57F4 = IIf(IsDbNull(pRsJWDetail.Fields.Item("PARTY_F4NO").Value), "", pRsJWDetail.Fields.Item("PARTY_F4NO").Value)

            mSqlStr = ""

            mSqlStr = " SELECT IH.INVOICE_DATE " & vbCrLf & " FROM FIN_INVOICE_HDR IH" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND IH.AUTO_KEY_DESP=" & Val(CStr(mDCNo)) & " "

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOM, ADODB.LockTypeEnum.adLockReadOnly)

            If RsBOM.EOF = False Then
                mInvoiceDate = VB6.Format(IIf(IsDbNull(RsBOM.Fields.Item("INVOICE_DATE").Value), "", RsBOM.Fields.Item("INVOICE_DATE").Value), "DD/MM/YYYY")
            End If

            mSqlStr = ""

            mSqlStr = " SELECT ID.STD_QTY, ID.GROSS_WT_SCRAP, INVMST.ISSUE_UOM " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pFGItemCode) & "' " & vbCrLf & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(pJWItemCode) & "' " & vbCrLf & " AND IH.BOM_TYPE='J'"

            mSqlStr = mSqlStr & vbCrLf & " AND IH.WEF = (" & vbCrLf & " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & Trim(pFGItemCode) & "'" & vbCrLf & " AND BOM_TYPE='J' AND WEF<='" & VB6.Format(mInvoiceDate, "DD-MMM-YYYY") & "')"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOM, ADODB.LockTypeEnum.adLockReadOnly)

            If RsBOM.EOF = False Then
                mUnitFactor = 1
                If RsBOM.Fields("ISSUE_UOM").Value = "TON" Or RsBOM.Fields("ISSUE_UOM").Value = "MT" Then
                    mUnitFactor = 1 / 1000
                    mUnitFactor = mUnitFactor / 1000
                ElseIf RsBOM.Fields("ISSUE_UOM").Value = "KGS" Then
                    mUnitFactor = 1 / 1000
                End If

                mStdQty = IIf(IsDbNull(RsBOM.Fields.Item("STD_QTY").Value), 0, RsBOM.Fields.Item("STD_QTY").Value) * mUnitFactor
                mScrapQty = IIf(IsDbNull(RsBOM.Fields.Item("GROSS_WT_SCRAP").Value), 0, RsBOM.Fields.Item("GROSS_WT_SCRAP").Value) * mUnitFactor
            End If

            If mStdQty = 0 Then
                mItemQty = 0
                mTotScrapQty = 0
            Else
                mItemQty = pQtyConsmed / mStdQty
                mTotScrapQty = mItemQty * mScrapQty
            End If



            PrintLine_Renamed = PrintLine_Renamed + 1

            Print(1, TAB(TabIDesc), Chr(15) & pJWItemCode)
            '        pItemDesc = GetMultiLine(pItemDesc, PrintLine, TabIPart - TabIDesc, TabIDesc)	
            Print(1, TAB(TabIDesc + 10), Left(pJWItemDesc, 46))
            Print(1, TAB(TabIDesc + 50), New String(" ", 12 - Len(VB6.Format(pQtyConsmed, "0.00"))) & VB6.Format(Trim(CStr(pQtyConsmed)), "0.00"))
            Print(1, TAB(TabIDesc + 63), New String(" ", 10 - Len(VB6.Format(mTotScrapQty, "0.00"))) & VB6.Format(Trim(CStr(mTotScrapQty)), "0.00"))
            PrintLine(1, TAB(TabIDesc + 74), p57F4 & Chr(18))


            pRsJWDetail.MoveNext()
        Loop

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub

    Private Sub PrintHeader(ByRef pISMRP As String)
        On Error GoTo ERR1
        Dim mRemovalHour As String
        Dim mRemovalMin As String
        Dim mCompanyTinNo As String
        Dim mTabCustDetail As Integer
        Dim mString As String
        Dim mCustomerName As String
        Dim mCompanyCINNo As String

        PageNo = PageNo + 1
        mTabCustDetail = TabAmount - 10
        PrintLine_Renamed = 1
        Do While PrintLine_Renamed <> 3
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop

        If RSSalesDetail.EOF = True Then RSSalesDetail.MoveLast() : mIsLastPage = True

        mCompanyTinNo = IIf(IsDbNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value)
        mCompanyCINNo = IIf(IsDbNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value)

        Print(1, TAB(0), Chr(18) & Chr(15))

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 13 Then
            If mCompanyTinNo <> "" Then
                PrintLine(1, TAB(TabQty - 15), Chr(14) & "TIN NO : " & mCompanyTinNo)
                PrintLine_Renamed = PrintLine_Renamed + 1
                '        Else	
                '            Print #1, Tab(TabQty - 15); Chr(14) & " "	
            End If

        End If

        '    Print #1, Tab(TabQty - 15); Chr(14) & "TAX INVOICE"	
        '    PrintLine = PrintLine + 1	

        ''02/04/2010 ' Bharat Sharma.....	
        If RsCompany.Fields("FYEAR").Value <= 2009 Then
            mString = IIf(IsDbNull(RSSalesDetail.Fields("AUTHSIGN").Value), "", RSSalesDetail.Fields("AUTHSIGN").Value)
        Else
            mString = "Exempt. Under Notif. No 5/2010 CE(NT)" '' "Exempted Under Notification No 5/2010 Ce(NT)"	
        End If
        mString = New String(" ", (TabLastCol - TabQty - Len(mString)) / 2) & mString
        PrintLine(1, TAB(TabQty), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1
        ''02/04/2010 ' Bharat Sharma.....	
        If RsCompany.Fields("FYEAR").Value <= 2009 Then
            mString = IIf(IsDbNull(RSSalesDetail.Fields("AUTHDATE").Value), "", RSSalesDetail.Fields("AUTHDATE").Value)
        Else
            mString = "Dt. 27/2/2010"
        End If
        mString = New String(" ", (TabLastCol - TabQty - Len(mString)) / 2) & mString
        PrintLine(1, TAB(TabQty), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            PrintLine(1, TAB(35), "Office of the Superintendent of Central Excise")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(35), "67/A Sipcot Industrial Complex, Hosur")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Else
            '        Print #1, Tab(0); " "	
            '        PrintLine = PrintLine + 1	
            '	
            '        Print #1, Tab(0); " "	
            '        PrintLine = PrintLine + 1	
        End If

        Print(1, TAB(TabUnit), Chr(18) & Chr(15) & Chr(14) & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("BILLNOSEQ").Value), "", RSSalesDetail.Fields("BILLNOSEQ").Value), "000000") & Chr(18) & Chr(15))
        Print(1, TAB(TabQty + 16), VB6.Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy") & Chr(18) & Chr(15))
        PrintLine_Renamed = PrintLine_Renamed + 1

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            '        Print #1, Tab(35); "Office of the Superintendent of Central Excise"	
            '        PrintLine = PrintLine + 1	
            '	
            '        Print #1, Tab(35); "67/A Sipcot Industrial Complex, Hosur"	
            '        PrintLine = PrintLine + 1	
        Else
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        ''TabQty - 5        ''TabQty + 17	
        '    Print #1, Tab(TabRate); Chr(18) & Chr(15) & Chr(14) & Format(IIf(IsNull(RSSalesDetail.Fields("BILLNOSEQ").Value), "", RSSalesDetail.Fields("BILLNOSEQ").Value), "000000") & Chr(18) & Chr(15);	
        '    Print #1, Tab(TabQty + 17); Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy") & Chr(18) & Chr(15);	
        '    Print #1, Tab(TabRate); Chr(18) & Chr(15) & Chr(14) & Format(IIf(IsNull(RSSalesDetail.Fields("BILLNOSEQ").Value), "", RSSalesDetail.Fields("BILLNOSEQ").Value), "000000") & Chr(18) & Chr(15);	
        '    Print #1, Tab(TabRate + 8); Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy") & Chr(18) & Chr(15);	
        '    PrintLine = PrintLine + 1	


        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
        Else
            Print(1, TAB(40), "CIN : " & mCompanyCINNo)
        End If
        ''11-07-2015 temp..	
        '    Print #1, Tab(TabRate); Format(IIf(IsNull(RSSalesDetail.Fields("BILLNOSEQ").Value), "", RSSalesDetail.Fields("BILLNOSEQ").Value), "000000");	
        '    Print #1, Tab(TabAmount + 12); Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy")	
        PrintLine(1, TAB(TabRate), Chr(18) & Chr(15) & Chr(14) & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("BILLNOSEQ").Value), "", RSSalesDetail.Fields("BILLNOSEQ").Value), "000000") & "    " & VB6.Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy") & Chr(18) & Chr(15))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(TabRate), IIf(IsDbNull(RSSalesDetail.Fields("CUST_PO_NO").Value), "", RSSalesDetail.Fields("CUST_PO_NO").Value))
        PrintLine(1, TAB(TabAmount + 12), VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("CUST_PO_DATE").Value), "", RSSalesDetail.Fields("CUST_PO_DATE").Value), "dd/mm/yyyy"))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(40), IIf(IsDbNull(RSSalesDetail.Fields("ITEMDESC").Value), "", RSSalesDetail.Fields("ITEMDESC").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(TabAmount), VB6.Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy"))
        PrintLine(1, TAB(TabAmount + 15), VB6.Format(RSSalesDetail.Fields("INV_PREP_TIME").Value, "HH:MM"))
        PrintLine_Renamed = PrintLine_Renamed + 1


        Print(1, TAB(TabAmount), VB6.Format(RSSalesDetail.Fields("REMOVAL_DATE").Value, "dd/mm/yyyy"))
        PrintLine(1, TAB(TabAmount + 15), VB6.Format(RSSalesDetail.Fields("REMOVAL_TIME").Value, "HH:MM"))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(65), IIf(IsDbNull(RSSalesDetail.Fields("TARIFFHEADING").Value), "", RSSalesDetail.Fields("TARIFFHEADING").Value))
        mRemovalHour = DigitsInWords(CShort(Left(VB6.Format(RSSalesDetail.Fields("REMOVAL_TIME").Value, "HH:MM"), 2)))
        mRemovalMin = DigitsInWords(CShort(Right(VB6.Format(RSSalesDetail.Fields("REMOVAL_TIME").Value, "HH:MM"), 2)))

        PrintLine(1, TAB(TabQty + 5), "Hrs. " & mRemovalHour & IIf(mRemovalMin = "", "", " and Minutes " & mRemovalMin) & " Only")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        If RSSalesDetail.Fields("WITHIN_STATE").Value = "Y" Then
            PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("LST_No").Value), "", RSSalesDetail.Fields("LST_No").Value))
        Else
            PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("CST_NO").Value), "", RSSalesDetail.Fields("CST_NO").Value))
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        '	
        mCustomerName = IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_NAME").Value), "", RSSalesDetail.Fields("SUPP_CUST_NAME").Value)
        '    mCustomerName = mCustomerName & IIf(IsNull(RSSalesDetail!VENDOR_CODE), "", " - " & RSSalesDetail!VENDOR_CODE)	

        Print(1, TAB(3), mCustomerName)
        PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("CENT_EXC_RGN_NO").Value), "", RSSalesDetail.Fields("CENT_EXC_RGN_NO").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_ADDR").Value), "", RSSalesDetail.Fields("SUPP_CUST_ADDR").Value))
        PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("EXCISE_RANGE").Value), "", RSSalesDetail.Fields("EXCISE_RANGE").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_CITY").Value), "", RSSalesDetail.Fields("SUPP_CUST_CITY").Value)
        mString = mString & ", "
        mString = mString & IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_STATE").Value), "", RSSalesDetail.Fields("SUPP_CUST_STATE").Value)

        '    If RSSalesDetail!WITHIN_COUNTRY = "N" Then	
        mString = mString & ", " & IIf(IsDbNull(RSSalesDetail.Fields("COUNTRY").Value), "", RSSalesDetail.Fields("COUNTRY").Value)
        '    End If	

        '    Print #1, Tab(3); IIf(IsNull(RSSalesDetail.Fields("SUPP_CUST_CITY").Value), "", RSSalesDetail.Fields("SUPP_CUST_CITY").Value) & ", " & IIf(IsNull(RSSalesDetail.Fields("SUPP_CUST_STATE").Value), "", RSSalesDetail.Fields("SUPP_CUST_STATE").Value);	
        Print(1, TAB(3), mString)

        PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("EXCISE_DIV").Value), "", RSSalesDetail.Fields("EXCISE_DIV").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("VENDOR_CODE").Value), "", "Vendor Code : " & RSSalesDetail.Fields("VENDOR_CODE").Value))
        PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("COMMISIONER_RATE").Value), "", RSSalesDetail.Fields("COMMISIONER_RATE").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), "TIN NO. :" & IIf(IsDbNull(RSSalesDetail.Fields("ACCOUNT_CODE").Value), "", RSSalesDetail.Fields("ACCOUNT_CODE").Value))
        PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("ECC_NO").Value), "", RSSalesDetail.Fields("ECC_NO").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_POLICYNO").Value), "", "Policy No. :" & RSSalesDetail.Fields("SUPP_CUST_POLICYNO").Value))
        PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("PAN_NO").Value), "", RSSalesDetail.Fields("PAN_NO").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1




        If pISMRP = "Y" Then
            mString = "MRP Value"
            PrintLine(1, TAB(126), New String(" ", (TabLastCol - 123 - Len(mString)) / 2) & mString)
            PrintLine_Renamed = PrintLine_Renamed + 1

            mString = "after"
            PrintLine(1, TAB(126), New String(" ", (TabLastCol - 123 - Len(mString)) / 2) & mString)
            PrintLine_Renamed = PrintLine_Renamed + 1

            mString = "Abatement"
            PrintLine(1, TAB(126), New String(" ", (TabLastCol - 123 - Len(mString)) / 2) & mString)
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        Do While PrintLine_Renamed < 29
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub

    Private Sub PrintHeader_PlainPaper(ByRef pISMRP As String)
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim mRemovalHour As String
        Dim mRemovalMin As String
        Dim mCompanyTinNo As String
        Dim mTabCustDetail As Integer
        Dim mString As String
        Dim mCustomerName As String
        Dim TabMRPRate As Integer
        Dim TabMRPAmount As Integer
        Dim TabMRP As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mCompanyCINNo As String


        TabMRPRate = 102
        TabMRPAmount = 114
        TabMRP = 126

        PageNo = PageNo + 1
        mTabCustDetail = TabAmount - 10
        PrintLine_Renamed = 1
        '    Do While PrintLine <> 3	
        '        Print #1, Tab(0); " "	
        '        PrintLine = PrintLine + 1	
        '    Loop	

        If RSSalesDetail.EOF = True Then RSSalesDetail.MoveLast() : mIsLastPage = True

        mCompanyTinNo = IIf(IsDbNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value)
        mCompanyCINNo = IIf(IsDbNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value)

        mString = Chr(18) & Chr(15) & "TIN NO : " & IIf(IsDbNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value)
        Print(1, TAB(0), mString)
        mString = "ORIGINAL FOR BUYER/DUPLICATE FOR TRANSPORTER/TRIPLICATE FOR ASSESSEE/EXTRA COPY"
        mString = New String(" ", TabLastCol - 25 - Len(mString)) & mString
        PrintLine(1, TAB(25), mString)

        PrintLine_Renamed = PrintLine_Renamed + 1


        PrintLine(1, TAB(0), Chr(18) & Chr(15) & "CIN : " & mCompanyCINNo)
        PrintLine_Renamed = PrintLine_Renamed + 1


        '    mString = Chr(18) & Chr(15) & "SERVICE REGN NO : " & IIf(IsNull(RsCompany!SERV_REGN_NO), "", RsCompany!SERV_REGN_NO)	
        '    Print #1, Tab(0); mString;	
        '    PrintLine = PrintLine + 1	

        mString = "TAX INVOICE"
        PrintLine(1, TAB(0), Chr(18) & New String(" ", (88 - Len(mString) * 2) / 2) & Chr(14) & mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "(Removal of Excisable goods under Rule 11)"
        PrintLine(1, TAB(0), New String(" ", (85 - Len(mString)) / 2) & mString) ''String((85 - Len(mString) * 2) / 2, " ") & mString	
        PrintLine_Renamed = PrintLine_Renamed + 1


        If RsCompany.Fields("COMPANY_CODE").Value = 25 And RsCompany.Fields("FYEAR").Value = 2012 Then
            SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST WHERE COMPANy_CODE=15"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mString = IIf(IsDbNull(RsCompany.Fields("Company_Name").Value), "", RsCompany.Fields("Company_Name").Value)
            Else
                mString = RsCompany.Fields("COMPANY_NAME").Value
            End If
        Else
            mString = RsCompany.Fields("COMPANY_NAME").Value
        End If
        PrintLine(1, TAB(0), Chr(18) & New String(" ", (88 - Len(mString) * 2) / 2) & Chr(14) & mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = RsCompany.Fields("COMPANY_ADDR").Value
        PrintLine(1, TAB(0), New String(" ", (85 - Len(mString)) / 2) & mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = RsCompany.Fields("COMPANY_CITY").Value & ", " & RsCompany.Fields("COMPANY_STATE").Value
        mString = mString & " Tel : " & RsCompany.Fields("COMPANY_PHONE").Value
        PrintLine(1, TAB(0), New String(" ", (85 - Len(mString)) / 2) & mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    mString = "Tel : " & RsCompany.Fields("COMPANY_PHONE").Value	
        '    Print #1, Tab(0); String((85 - Len(mString)) / 2, " ") & mString	
        '    PrintLine = PrintLine + 1	

        '    Print #1, Tab(3); String(85, "-")	
        '    PrintLine = PrintLine + 1	

        Print(1, TAB(0), Chr(18) & Chr(15))

        PrintLine(1, TAB(3), New String("-", TabLastCol))
        PrintLine_Renamed = PrintLine_Renamed + 1

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            If mCompanyTinNo <> "" Then
                PrintLine(1, TAB(TabQty - 15), Chr(14) & "TIN NO : " & mCompanyTinNo)
                PrintLine_Renamed = PrintLine_Renamed + 1
            End If
        End If

        mString = "Range : " & IIf(IsDbNull(RsCompany.Fields("EXCISE_RANGE").Value), "", RsCompany.Fields("EXCISE_RANGE").Value)
        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|")
        mString = "Pre-Authentication"
        mString = New String(" ", (TabLastCol - TabQty - Len(mString)) / 2) & mString
        PrintLine(1, TAB(TabQty), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "Division : " & IIf(IsDbNull(RsCompany.Fields("EXCISE_DIV").Value), "", RsCompany.Fields("EXCISE_DIV").Value)
        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|")
        mString = "for " & RsCompany.Fields("COMPANY_NAME").Value
        If TabLastCol - TabQty - Len(mString) <= 0 Then
            mString = mString
        Else
            mString = New String(" ", (TabLastCol - TabQty - Len(mString)) / 2) & mString
        End If
        PrintLine(1, TAB(TabQty), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(0); " "	
        mString = "CE Regn No : " & IIf(IsDbNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value)
        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|")
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(0); " "	
        mString = "ECC No : " & IIf(IsDbNull(RsCompany.Fields("ECC_NO").Value), "", RsCompany.Fields("ECC_NO").Value)
        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|")
        If RsCompany.Fields("FYEAR").Value <= 2009 Then
            mString = IIf(IsDbNull(RSSalesDetail.Fields("AUTHSIGN").Value), "", RSSalesDetail.Fields("AUTHSIGN").Value)
        Else
            mString = "Exempt. Under Notif. No 5/2010 CE(NT)" '' "Exempted Under Notification No 5/2010 Ce(NT)"	
        End If
        mString = New String(" ", (TabLastCol - TabQty - Len(mString)) / 2) & mString
        PrintLine(1, TAB(TabQty), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "Personnel Ledger A/c No : " & IIf(IsDbNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value)
        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|")
        If RsCompany.Fields("FYEAR").Value <= 2009 Then
            mString = IIf(IsDbNull(RSSalesDetail.Fields("AUTHDATE").Value), "", RSSalesDetail.Fields("AUTHDATE").Value)
        Else
            mString = "Dt. 27/2/2010"
        End If
        mString = New String(" ", (TabLastCol - TabQty - Len(mString)) / 2) & mString
        PrintLine(1, TAB(TabQty), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "ST/CST No. : " & IIf(IsDbNull(RsCompany.Fields("LST_NO").Value), "", RsCompany.Fields("LST_NO").Value)
        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|")
        mString = IIf(RsCompany.Fields("FYEAR").Value <= 2009, "AUTHORISED SIGNATORY", "")
        mString = New String(" ", (TabLastCol - TabQty - Len(mString)) / 2) & mString
        PrintLine(1, TAB(TabQty), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "Name of Excisable Commodity : " & IIf(IsDbNull(RSSalesDetail.Fields("ITEMDESC").Value), "", RSSalesDetail.Fields("ITEMDESC").Value)
        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|")
        PrintLine(1, TAB(TabUnit + 1), New String("-", TabLastCol - TabUnit + 2))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "PAN : (I. Tax) : " & IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|")
        Print(1, TAB(TabQty), Chr(18) & Chr(15) & Chr(14) & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("BILLNOSEQ").Value), "", RSSalesDetail.Fields("BILLNOSEQ").Value), "000000"))
        Print(1, TAB(TabQty + 10), VB6.Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy") & Chr(18) & Chr(15))
        PrintLine_Renamed = PrintLine_Renamed + 1

        If RsCompany.Fields("COMPANY_CODE").Value = 15 Or RsCompany.Fields("COMPANY_CODE").Value = 25 Then
            mString = "Exemption Notification No : 49/50 Dt. 10/06/2003 as Amended"
        Else
            mString = "Exemption Notification No : "
        End If

        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|Invoice No. :")
        Print(1, TAB(TabRate), VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("BILLNOSEQ").Value), "", RSSalesDetail.Fields("BILLNOSEQ").Value), "000000"))
        Print(1, TAB(TabAmount), "Date : ")
        PrintLine(1, TAB(TabAmount + 12), VB6.Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy"))
        PrintLine_Renamed = PrintLine_Renamed + 1


        '    Print #1, Tab(0); " "	
        '    PrintLine = PrintLine + 1	

        mString = "Tariff Heading No. / Sub Head : " & IIf(IsDbNull(RSSalesDetail.Fields("TARIFFHEADING").Value), "", RSSalesDetail.Fields("TARIFFHEADING").Value)
        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|P.O. No. :")
        Print(1, TAB(TabRate), IIf(IsDbNull(RSSalesDetail.Fields("CUST_PO_NO").Value), "", RSSalesDetail.Fields("CUST_PO_NO").Value))
        Print(1, TAB(TabAmount), "Date : ")
        PrintLine(1, TAB(TabAmount + 12), VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("CUST_PO_DATE").Value), "", RSSalesDetail.Fields("CUST_PO_DATE").Value), "dd/mm/yyyy"))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "Commissionerate : " & IIf(IsDbNull(RsCompany.Fields("COMMISIONER_RATE").Value), "", RsCompany.Fields("COMMISIONER_RATE").Value)
        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|")
        PrintLine(1, TAB(TabUnit + 1), New String("-", TabLastCol + 2 - TabUnit))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "TAX INVOICE - VALID FOR INPUT TAX" '' / CENVAT CREDIT"	
        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|Date & Time of Preparation :")
        Print(1, TAB(TabAmount), VB6.Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy"))
        PrintLine(1, TAB(TabAmount + 15), VB6.Format(RSSalesDetail.Fields("INV_PREP_TIME").Value, "HH:MM"))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(TabUnit), "|Date & Time of Removal of Goods :")
        Print(1, TAB(TabAmount), VB6.Format(RSSalesDetail.Fields("REMOVAL_DATE").Value, "dd/mm/yyyy"))
        PrintLine(1, TAB(TabAmount + 15), VB6.Format(RSSalesDetail.Fields("REMOVAL_TIME").Value, "HH:MM"))
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(65); IIf(IsNull(RSSalesDetail.Fields("TARIFFHEADING").Value), "", RSSalesDetail.Fields("TARIFFHEADING").Value);	
        Print(1, TAB(TabUnit), "|In Words :")
        mRemovalHour = DigitsInWords(CShort(Left(VB6.Format(RSSalesDetail.Fields("REMOVAL_TIME").Value, "HH:MM"), 2)))
        mRemovalMin = DigitsInWords(CShort(Right(VB6.Format(RSSalesDetail.Fields("REMOVAL_TIME").Value, "HH:MM"), 2)))

        PrintLine(1, TAB(TabQty + 6), "Hrs. " & mRemovalHour & IIf(mRemovalMin = "", "", " and Minutes " & mRemovalMin) & " Only")
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    mString = "NOT VALID FOR INPUT TAX / CENVAT CREDIT"	
        '    Print #1, Tab(3); mString;	
        '    Print #1, Tab(TabUnit); "|"	
        '    Print #1, Tab(0); " "	
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), New String("-", TabLastCol))
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(0); " "	
        '    PrintLine = PrintLine + 1	

        Print(1, TAB(3), "Name and Address of the Consignee")
        Print(1, TAB(TabUnit), "|Cust. ST/CST No & Dt :")
        If RSSalesDetail.Fields("WITHIN_STATE").Value = "Y" Then
            PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("LST_No").Value), "", RSSalesDetail.Fields("LST_No").Value))
        Else
            PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("CST_NO").Value), "", RSSalesDetail.Fields("CST_NO").Value))
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        mCustomerName = IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_NAME").Value), "", RSSalesDetail.Fields("SUPP_CUST_NAME").Value)

        Print(1, TAB(3), mCustomerName)
        Print(1, TAB(TabUnit), "|Central Excise Regn No :")
        PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("CENT_EXC_RGN_NO").Value), "", RSSalesDetail.Fields("CENT_EXC_RGN_NO").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_ADDR").Value), "", RSSalesDetail.Fields("SUPP_CUST_ADDR").Value))
        Print(1, TAB(TabUnit), "|Range :")
        PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("EXCISE_RANGE").Value), "", RSSalesDetail.Fields("EXCISE_RANGE").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_CITY").Value), "", RSSalesDetail.Fields("SUPP_CUST_CITY").Value)
        mString = mString & ", "
        mString = mString & IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_STATE").Value), "", RSSalesDetail.Fields("SUPP_CUST_STATE").Value)

        mString = mString & ", " & IIf(IsDbNull(RSSalesDetail.Fields("COUNTRY").Value), "", RSSalesDetail.Fields("COUNTRY").Value)

        Print(1, TAB(3), mString)
        Print(1, TAB(TabUnit), "|Division :")
        PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("EXCISE_DIV").Value), "", RSSalesDetail.Fields("EXCISE_DIV").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("VENDOR_CODE").Value), "", "Vendor Code : " & RSSalesDetail.Fields("VENDOR_CODE").Value))
        Print(1, TAB(TabUnit), "|Commissionerates :")
        PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("COMMISIONER_RATE").Value), "", RSSalesDetail.Fields("COMMISIONER_RATE").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), "TIN NO. :" & IIf(IsDbNull(RSSalesDetail.Fields("ACCOUNT_CODE").Value), "", RSSalesDetail.Fields("ACCOUNT_CODE").Value))
        Print(1, TAB(TabUnit), "|ECC No :")
        PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("ECC_NO").Value), "", RSSalesDetail.Fields("ECC_NO").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_POLICYNO").Value), "", "Policy No. :" & RSSalesDetail.Fields("SUPP_CUST_POLICYNO").Value))
        Print(1, TAB(TabUnit), "|PAN (I.Tax) :")
        PrintLine(1, TAB(mTabCustDetail), IIf(IsDbNull(RSSalesDetail.Fields("PAN_NO").Value), "", RSSalesDetail.Fields("PAN_NO").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), New String("-", TabLastCol))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(TabSNo), "SNo.")
        Print(1, TAB(TabIDesc), "DESCRIPTION")
        Print(1, TAB(TabIPart), "PART NO")
        '    Print #1, Tab(TabUnit - 18); "UNITS";	
        Print(1, TAB(TabUnit), "UNITS")
        If pISMRP = "N" Then
            Print(1, TAB(TabQty), New String(" ", TabRate - TabQty - Len("QTY")) & "QTY")
            Print(1, TAB(TabRate), New String(" ", TabAmount - TabRate - Len("RATE (Rs)")) & "RATE (Rs)")
            Print(1, TAB(TabAmount), New String(" ", TabLastCol - TabAmount - Len("AMOUNT")) & "AMOUNT")
        Else
            Print(1, TAB(TabQty), New String(" ", TabMRPRate - TabQty - Len("QTY")) & "QTY")
            Print(1, TAB(TabMRPRate), New String(" ", TabMRPAmount - TabMRPRate - Len("RATE (Rs)")) & "RATE (Rs)")
            Print(1, TAB(TabMRPAmount), New String(" ", TabMRP - TabMRPAmount - Len("AMOUNT")) & "AMOUNT")
        End If


        If pISMRP = "Y" Then
            mString = "MRP Value"
            PrintLine(1, TAB(126), New String(" ", (TabLastCol - 123 - Len(mString)) / 2) & mString)
            PrintLine_Renamed = PrintLine_Renamed + 1

            mString = "after"
            PrintLine(1, TAB(126), New String(" ", (TabLastCol - 123 - Len(mString)) / 2) & mString)
            PrintLine_Renamed = PrintLine_Renamed + 1

            mString = "Abatement"
            PrintLine(1, TAB(126), New String(" ", (TabLastCol - 123 - Len(mString)) / 2) & mString)
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        PrintLine(1, TAB(3), New String("-", TabLastCol))
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Do While PrintLine <> 29	
        '        Print #1, Tab(0); " "	
        '        PrintLine = PrintLine + 1	
        '    Loop	
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub
    Private Sub PrintAnnexHeader(ByRef pExpAnnexPrint As String)
        On Error GoTo ERR1
        Dim mRemovalHour As String
        Dim mRemovalMin As String
        Dim mCompanyTinNo As String
        Dim mTabCustDetail As Integer
        Dim mString As String



        PageNo = PageNo + 1
        mTabCustDetail = TabAmount - 10
        PrintLine_Renamed = 1
        Do While PrintLine_Renamed <> 3
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop

        If RSSalesDetail.EOF = True Then RSSalesDetail.MoveLast() : mIsLastPage = True


        PrintLine(1, TAB(3), Chr(18) & Chr(15) & Chr(14) & " A  N  N  E  X  U  R  E") ''& Chr(18) & Chr(15)	
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), IIf(IsDbNull(RsCompany.Fields("Company_Name").Value), "", RsCompany.Fields("Company_Name").Value)) ''& Chr(18) & Chr(15)	
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_NAME").Value), "", RSSalesDetail.Fields("SUPP_CUST_NAME").Value) & Chr(18) & Chr(15))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_ADDR").Value), "", RSSalesDetail.Fields("SUPP_CUST_ADDR").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_CITY").Value), "", RSSalesDetail.Fields("SUPP_CUST_CITY").Value)
        mString = mString & ", "
        mString = mString & IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_STATE").Value), "", RSSalesDetail.Fields("SUPP_CUST_STATE").Value)
        mString = mString & ", " & IIf(IsDbNull(RSSalesDetail.Fields("COUNTRY").Value), "", RSSalesDetail.Fields("COUNTRY").Value)
        PrintLine(1, TAB(3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("VENDOR_CODE").Value), "", "Vendor Code : " & RSSalesDetail.Fields("VENDOR_CODE").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), Chr(18) & Chr(15) & Chr(14) & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("BILLNOSEQ").Value), "", " Bill No : " & RSSalesDetail.Fields("BILLNOSEQ").Value), "000000"))
        PrintLine(1, TAB(30), "Date : " & VB6.Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy") & Chr(18) & Chr(15))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("CUST_PO_NO").Value), "", "PO NO : " & RSSalesDetail.Fields("CUST_PO_NO").Value))
        PrintLine(1, TAB(60), VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("CUST_PO_DATE").Value), "", "Date : " & RSSalesDetail.Fields("CUST_PO_DATE").Value), "dd/mm/yyyy"))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Do While PrintLine_Renamed <> 14
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop

        mString = New String("-", TabAnnexLastCol - TaxAnnexSNo)
        Print(1, TAB(TaxAnnexSNo), Chr(15) & mString & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "SNo."
        Print(1, TAB(TaxAnnexSNo), Chr(15) & mString)

        mString = "Description / Part No"
        Print(1, TAB(TabAnnexIDesc), mString)

        mString = "Unit"
        Print(1, TAB(TabAnnexUnit), mString)

        mString = "Qty"
        mString = New String(" ", TabAnnexRate - TabAnnexQty - Len(mString)) & mString
        Print(1, TAB(TabAnnexQty), mString)

        mString = "Rate"
        mString = New String(" ", TabAnnexAmount - TabAnnexRate - Len(mString)) & mString
        Print(1, TAB(TabAnnexRate), mString)

        mString = IIf(pExpAnnexPrint = "YA", "", "Amount")
        mString = New String(" ", TabAnnexMRP - TabAnnexAmount - Len(mString)) & mString
        Print(1, TAB(TabAnnexAmount), mString)

        mString = IIf(pExpAnnexPrint = "YA", "", "MRP")
        mString = New String(" ", TabAnnexMRPAmount - TabAnnexMRP - Len(mString)) & mString
        Print(1, TAB(TabAnnexMRP), mString)

        mString = IIf(pExpAnnexPrint = "YA", "Amount", "MRP Amount")
        mString = New String(" ", TabAnnexLastCol - TabAnnexMRPAmount - Len(mString)) & mString
        PrintLine(1, TAB(TabAnnexMRPAmount), mString & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = New String("-", TabAnnexLastCol - TaxAnnexSNo)
        Print(1, TAB(TaxAnnexSNo), Chr(15) & mString & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume	
    End Sub

    Private Sub PrintSubsidiaryChallanHeader()
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim mRemovalHour As String
        Dim mRemovalMin As String
        Dim mCompanyTinNo As String
        Dim mTabCustDetail As Integer
        Dim mString As String
        Dim mSTRegnNo As String
        Dim mExciseRegnNo As String
        Dim mDivision As String
        Dim mCommissionerate As String
        Dim mRange As String
        Dim mProcessNature As String
        Dim xSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDept As String
        Dim mItemCode As String

        mSTRegnNo = IIf(IsDbNull(RsCompany.Fields("SERV_REGN_NO").Value), "", RsCompany.Fields("SERV_REGN_NO").Value)
        mCompanyTinNo = IIf(IsDbNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value)
        mExciseRegnNo = IIf(IsDbNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value)
        mDivision = IIf(IsDbNull(RsCompany.Fields("EXCISE_DIV").Value), "", RsCompany.Fields("EXCISE_DIV").Value)
        mCommissionerate = IIf(IsDbNull(RsCompany.Fields("COMMISIONER_RATE").Value), "", RsCompany.Fields("COMMISIONER_RATE").Value)
        mRange = IIf(IsDbNull(RsCompany.Fields("EXCISE_RANGE").Value), "", RsCompany.Fields("EXCISE_RANGE").Value)

        PageNo = PageNo + 1
        mTabCustDetail = TabAmount - 10
        PrintLine_Renamed = 1
        Do While PrintLine_Renamed <> 3
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop

        If RSSalesDetail.EOF = True Then RSSalesDetail.MoveLast() : mIsLastPage = True

        mString = "SUBSIDIARY CHALLAN"
        '    mString = String((TabSCLastCol - TaxSCSNo - (Len(mString) * 2)) / 2, " ") & mString	
        '    Print #1, Tab((TabSCLastCol - TaxSCSNo - (Len(mString) * 2)) / 2); Chr(18) & Chr(15) & Chr(14) & mString & Chr(18) & Chr(15)	
        PrintLine(1, TAB(0), New String(" ", (88 - Len(mString) * 2) / 2) & Chr(14) & mString & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(3); Chr(18) & Chr(15) & Chr(14) & "SUBSIDIARY CHALLAN"         ''& Chr(18) & Chr(15)	
        '    PrintLine = PrintLine + 1	

        mString = New String("-", TabSCLastCol - TaxSCSNo)
        Print(1, TAB(TaxSCSNo), Chr(15) & mString & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = IIf(IsDbNull(RsCompany.Fields("Company_Name").Value), "", RsCompany.Fields("Company_Name").Value)
        '    mString = String((TabSCLastCol - TaxSCSNo - (Len(mString) * 2)) / 2, " ") & mString	
        '    Print #1, Tab((TabSCLastCol - TaxSCSNo - (Len(mString) * 2)) / 2); Chr(18) & Chr(15) & Chr(14) & mString & Chr(18) & Chr(15)	
        PrintLine(1, TAB(0), New String(" ", (88 - Len(mString) * 2) / 2) & Chr(14) & mString & Chr(18) & Chr(15))
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    String((88 - Len(mString) * 2) / 2, " ") & Chr(14) & mString	

        mString = IIf(IsDbNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)
        mString = mString & ", " & IIf(IsDbNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        mString = mString & ", " & IIf(IsDbNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
        mString = mString & ", Phone : " & IIf(IsDbNull(RsCompany.Fields("COMPANY_PHONE").Value), "", RsCompany.Fields("COMPANY_PHONE").Value)
        mString = New String(" ", (TabSCLastCol - TaxSCSNo - Len(mString)) / 2) & mString
        PrintLine(1, TAB(3), mString) ''& Chr(18) & Chr(15)	
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = New String("-", TabSCLastCol - TaxSCSNo)
        Print(1, TAB(TaxSCSNo), Chr(15) & mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "Heil Challan/Invoice No : " & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("BILLNOSEQ").Value), "", RSSalesDetail.Fields("BILLNOSEQ").Value), "000000")
        mString = mString & "      Date : " & VB6.Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy")
        PrintLine(1, TAB(3), Chr(18) & Chr(15) & Chr(14) & mString & Chr(18) & Chr(15))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), "Excise Registration No.")
        Print(1, TAB(28), ": " & mExciseRegnNo)
        Print(1, TAB(70), "Division")
        PrintLine(1, TAB(90), ": " & mDivision)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1
        Print(1, TAB(3), "Service Tax Regn No")
        Print(1, TAB(28), ": " & mSTRegnNo)
        Print(1, TAB(70), "Commissionerate")
        PrintLine(1, TAB(90), ": " & mCommissionerate)

        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), "CE. Range")
        Print(1, TAB(28), ": " & mExciseRegnNo)
        Print(1, TAB(70), "TIN No")
        PrintLine(1, TAB(90), ": " & mCompanyTinNo)

        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), "Name of the Party")
        PrintLine(1, TAB(28), ": " & IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_NAME").Value), "", RSSalesDetail.Fields("SUPP_CUST_NAME").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(30), IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_ADDR").Value), "", RSSalesDetail.Fields("SUPP_CUST_ADDR").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_CITY").Value), "", RSSalesDetail.Fields("SUPP_CUST_CITY").Value)
        mString = mString & ", "
        mString = mString & IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_STATE").Value), "", RSSalesDetail.Fields("SUPP_CUST_STATE").Value)
        mString = mString & ", " & IIf(IsDbNull(RSSalesDetail.Fields("COUNTRY").Value), "", RSSalesDetail.Fields("COUNTRY").Value)
        PrintLine(1, TAB(30), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        mItemCode = IIf(IsDbNull(RSSalesDetail.Fields("ITEM_CODE").Value), "-1", RSSalesDetail.Fields("ITEM_CODE").Value)

        'UPGRADE_WARNING: Untranslated statement in PrintSubsidiaryChallanHeader. Please check source code.	
        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mDept = IIf(IsDbNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value)
            If mDept = "PLT" Then
                mProcessNature = "For Plating"
            ElseIf mDept = "PPS" Then
                mProcessNature = "For Painting"
            ElseIf mDept = "PRS" Then
                mProcessNature = "For Cutting"
            Else
                mProcessNature = ""
            End If
        Else
            mProcessNature = ""
        End If

        Print(1, TAB(3), "Nature of Processing")
        PrintLine(1, TAB(28), ": " & mProcessNature)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(3); Chr(18) & Chr(15) & Chr(14) & Format(IIf(IsNull(RSSalesDetail!BILLNOSEQ), "", " Bill No : " & RSSalesDetail!BILLNOSEQ), "000000");	
        '    Print #1, Tab(70); "Date : " & Format(RSSalesDetail!INVOICE_DATE, "dd/mm/yyyy") & Chr(18) & Chr(15)	
        '    PrintLine = PrintLine + 1	

        '    Print #1, Tab(3); IIf(IsNull(RSSalesDetail!CUST_PO_NO), "", "PO NO : " & RSSalesDetail!CUST_PO_NO);	
        '    Print #1, Tab(70); Format(IIf(IsNull(RSSalesDetail!CUST_PO_DATE), "", "Date : " & RSSalesDetail!CUST_PO_DATE), "dd/mm/yyyy")	
        '    PrintLine = PrintLine + 1	

        '    Do While PrintLine <> 21	
        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1
        '    Loop	


        mString = New String("-", TabSCLastCol - TaxSCSNo)
        Print(1, TAB(TaxSCSNo), Chr(15) & mString & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "SNo."
        mString = New String(" ", (TabSCIDesc - TaxSCSNo - Len(mString)) / 2) & mString
        Print(1, TAB(TaxSCSNo), Chr(15) & mString)

        mString = "Description / Part No"
        mString = New String(" ", (TabSCUnit - TabSCIDesc - Len(mString)) / 2) & mString
        Print(1, TAB(TabSCIDesc), mString)

        mString = "Unit"
        mString = New String(" ", (TabSCQty - TabSCUnit - Len(mString)) / 2) & mString
        Print(1, TAB(TabSCUnit), mString)

        mString = "Qty"
        mString = New String(" ", TabSCChallanNo - TabSCQty - Len(mString) - 1) & mString
        Print(1, TAB(TabSCQty), mString)

        mString = "Party F4"
        mString = New String(" ", (TabSCBillNo - TabSCChallanNo - Len(mString)) / 2) & mString
        Print(1, TAB(TabSCChallanNo), mString)

        mString = "Challan/RGP"
        mString = New String(" ", (TabSCChallanDate - TabSCBillNo - Len(mString)) / 2) & mString
        Print(1, TAB(TabSCBillNo), mString)

        mString = "Challan "
        mString = New String(" ", (TabSCLastCol - TabSCChallanDate - Len(mString)) / 2) & mString
        PrintLine(1, TAB(TabSCChallanDate), mString)

        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "Challan No."
        mString = New String(" ", (TabSCBillNo - TabSCChallanNo - Len(mString)) / 2) & mString
        Print(1, TAB(TabSCChallanNo), mString)

        mString = "No"
        mString = New String(" ", (TabSCChallanDate - TabSCBillNo - Len(mString)) / 2) & mString
        Print(1, TAB(TabSCBillNo), mString)

        mString = "Date"
        mString = New String(" ", (TabSCLastCol - TabSCChallanDate - Len(mString)) / 2) & mString
        PrintLine(1, TAB(TabSCChallanDate), mString)



        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = New String("-", TabSCLastCol - TaxSCSNo)
        Print(1, TAB(TaxSCSNo), Chr(15) & mString & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub
    Private Sub PrintPPHeader(ByRef pInvType As String)
        On Error GoTo ERR1
        Dim mRemovalHour As String
        Dim mRemovalMin As String
        Dim mString As String
        Dim mCompanyCINNo As String
        Dim mPolicyNo As String

        PageNo = PageNo + 1
        PrintLine_Renamed = 1
        Do While PrintLine_Renamed <> 3
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop

        If RSSalesDetail.EOF = True Then RSSalesDetail.MoveLast() : mIsLastPage = True

        '    Print #1, Tab(60); "Page : " + Trim(Str(PageNo))	
        '    PrintLine = PrintLine + 1	
        'Print #1, Tab(55); "Order No:";	

        mString = "CIN : " & IIf(IsDbNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value)
        PrintLine(1, TAB(0), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "TIN NO : " & IIf(IsDbNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value)
        PrintLine(1, TAB(0), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "Excise Regn No : " & IIf(IsDbNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value)
        PrintLine(1, TAB(0), Chr(18) & mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "Service Tax Regn No : " & IIf(IsDbNull(RsCompany.Fields("SERV_REGN_NO").Value), "", RsCompany.Fields("SERV_REGN_NO").Value)
        PrintLine(1, TAB(0), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "Business Auxilary Services"
        PrintLine(1, TAB(0), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = RsCompany.Fields("COMPANY_NAME").Value
        PrintLine(1, TAB(0), New String(" ", (88 - Len(mString) * 2) / 2) & Chr(14) & mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = RsCompany.Fields("COMPANY_ADDR").Value
        PrintLine(1, TAB(0), New String(" ", (85 - Len(mString)) / 2) & mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = RsCompany.Fields("COMPANY_CITY").Value & ", " & RsCompany.Fields("COMPANY_STATE").Value
        PrintLine(1, TAB(0), New String(" ", (85 - Len(mString)) / 2) & mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "Tel : " & RsCompany.Fields("COMPANY_PHONE").Value
        PrintLine(1, TAB(0), New String(" ", (85 - Len(mString)) / 2) & mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = UCase(pInvType)
        PrintLine(1, TAB(0), New String(" ", (85 - Len(mString) * 2) / 2) & Chr(14) & mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "(TAX INVOICE)"
        PrintLine(1, TAB(0), New String(" ", (85 - Len(mString)) / 2) & mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), New String("-", 85))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_NAME").Value), "", RSSalesDetail.Fields("SUPP_CUST_NAME").Value)
        mString = GetMultiLine(mString, PrintLine_Renamed, 50 - 4, 3)
        Print(1, TAB(3), mString)
        Print(1, TAB(50), "NO:")
        Print(1, TAB(61), VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("BILLNOSEQ").Value), "", RSSalesDetail.Fields("BILLNOSEQ").Value), "000000"))
        Print(1, TAB(71), "DATE:")
        PrintLine(1, TAB(77), VB6.Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy"))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_ADDR").Value), "", RSSalesDetail.Fields("SUPP_CUST_ADDR").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_CITY").Value), "", RSSalesDetail.Fields("SUPP_CUST_CITY").Value))
        Print(1, TAB(50), "ORDER NO:")
        Print(1, TAB(61), IIf(IsDbNull(RSSalesDetail.Fields("CUST_PO_NO").Value), "", RSSalesDetail.Fields("CUST_PO_NO").Value))
        Print(1, TAB(71), "DATE:")
        PrintLine(1, TAB(77), VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("CUST_PO_DATE").Value), "", RSSalesDetail.Fields("CUST_PO_DATE").Value), "dd/mm/yyyy"))
        '    Print #1, Tab(50); "CHALLAN NO:";	
        '    Print #1, Tab(61); IIf(IsNull(RSSalesDetail.Fields("AUTO_KEY_DESP").Value), "", RSSalesDetail.Fields("AUTO_KEY_DESP").Value);	
        '    Print #1, Tab(71); "DATE:";	
        '    Print #1, Tab(77); Format(IIf(IsNull(RSSalesDetail.Fields("DCDATE").Value), "", RSSalesDetail.Fields("DCDATE").Value), "dd/mm/yyyy")	
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_STATE").Value), "", RSSalesDetail.Fields("SUPP_CUST_STATE").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), "VENDOR CODE:")
        Print(1, TAB(15), IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_CODE").Value), "", RSSalesDetail.Fields("SUPP_CUST_CODE").Value))
        PrintLine(1, TAB(50), "TIN NO. :" & IIf(IsDbNull(RSSalesDetail.Fields("ACCOUNT_CODE").Value), "", RSSalesDetail.Fields("ACCOUNT_CODE").Value))

        mPolicyNo = IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_POLICYNO").Value), "", RSSalesDetail.Fields("SUPP_CUST_POLICYNO").Value)

        If mPolicyNo <> "" Then
            Print(1, TAB(3), "Policy No:")
            PrintLine(1, TAB(15), mPolicyNo)
        End If


        '    Print #1, Tab(50); "CHALLAN NO:";	
        '    Print #1, Tab(61); IIf(IsNull(RSSalesDetail.Fields("CUST_PO_NO").Value), "", RSSalesDetail.Fields("CUST_PO_NO").Value);	
        '    Print #1, Tab(71); "DATE:";	
        '    Print #1, Tab(77); Format(IIf(IsNull(RSSalesDetail.Fields("CUST_PO_DATE").Value), "", RSSalesDetail.Fields("CUST_PO_DATE").Value), "dd/mm/yyyy")	
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), "PARTY ST No.:")
        If RSSalesDetail.Fields("WITHIN_STATE").Value = "Y" Then
            PrintLine(1, TAB(20), Chr(15) & IIf(IsDbNull(RSSalesDetail.Fields("LST_No").Value), "", RSSalesDetail.Fields("LST_No").Value) & Chr(18))
        Else
            PrintLine(1, TAB(20), Chr(15) & IIf(IsDbNull(RSSalesDetail.Fields("CST_NO").Value), "", RSSalesDetail.Fields("CST_NO").Value) & Chr(18))
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), New String("-", 85))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(TabSNo), Chr(15) & "SNo.")
        Print(1, TAB(TabIDesc), "DESCRIPTION")
        Print(1, TAB(TabIPart), "PART NO")
        Print(1, TAB(TabUnit - 18), "UNITS")
        Print(1, TAB(TabQty - 17), New String(" ", TabRate - TabQty - Len("QTY")) & "QTY")
        Print(1, TAB(TabRate - 17), New String(" ", TabAmount - TabRate - Len("RATE (Rs)")) & "RATE (Rs)")
        Print(1, TAB(TabAmount - 15), New String(" ", TabLastCol - TabAmount - Len("AMOUNT")) & "AMOUNT")
        PrintLine(1, TAB(TabLastCol - 10), "57F4 No/Dt" & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1


        PrintLine(1, TAB(3), New String("-", 85))
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Do While PrintLine <> 28	
        '        Print #1, Tab(0); " "	
        '        PrintLine = PrintLine + 1	
        '    Loop	
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub
    Private Sub PrintJWHeader(ByRef pInvType As String)
        On Error GoTo ERR1
        Dim mRemovalHour As String
        Dim mRemovalMin As String
        Dim mString As String
        Dim mCompanyTinNo As String
        Dim mSTRegnNo As String
        Dim mExciseRegnNo As String
        Dim mCustomerName As String
        Dim mCompanyCINNo As String


        PageNo = PageNo + 1
        PrintLine_Renamed = 1

        PrintLine(1, TAB(3), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(3); " "	
        '    PrintLine = PrintLine + 1	

        mSTRegnNo = IIf(IsDbNull(RsCompany.Fields("SERV_REGN_NO").Value), "", RsCompany.Fields("SERV_REGN_NO").Value)
        mCompanyTinNo = IIf(IsDbNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value)
        mExciseRegnNo = IIf(IsDbNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value)
        mCompanyCINNo = IIf(IsDbNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value)

        Print(1, TAB(0), Chr(15))

        If mCompanyCINNo <> "" Then
            Print(1, TAB(TabAmount - 5), "CIN" & New String(" ", 12 - Len("CIN")) & ":" & mCompanyCINNo)
        Else
            PrintLine(1, TAB(TabAmount - 5), " ")
        End If

        If mExciseRegnNo <> "" Then
            PrintLine(1, TAB(TabAmount - 5), "CE Regn No" & New String(" ", 12 - Len("CE Regn No")) & ":" & mExciseRegnNo)
        Else
            PrintLine(1, TAB(TabAmount - 5), " ")
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        If mCompanyTinNo <> "" Then
            PrintLine(1, TAB(TabAmount - 5), "TIN NO" & New String(" ", 12 - Len("TIN NO")) & ":" & mCompanyTinNo)
        Else
            PrintLine(1, TAB(TabAmount - 5), " ")
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        If mSTRegnNo <> "" Then
            PrintLine(1, TAB(TabAmount - 5), "Regn NO" & New String(" ", 12 - Len("Regn NO")) & ":" & mSTRegnNo)
        Else
            PrintLine(1, TAB(TabAmount - 5), " ")
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabAmount - 5), "Business auxilary services " & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1


        Do While PrintLine_Renamed < 6
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop

        If RSSalesDetail.EOF = True Then RSSalesDetail.MoveLast() : mIsLastPage = True

        '    Print #1, Tab(60); "Page : " + Trim(Str(PageNo))	
        '    PrintLine = PrintLine + 1	

        mString = UCase(pInvType)
        PrintLine(1, TAB(65), Chr(18) & Chr(15) & Chr(14) & mString & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(0); " "	
        '    PrintLine = PrintLine + 1	

        mString = VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("BILLNOSEQ").Value), "", RSSalesDetail.Fields("BILLNOSEQ").Value), "000000")
        Print(1, TAB(60), Chr(18) & Chr(15) & Chr(14) & mString)
        PrintLine(1, TAB(72), VB6.Format(RSSalesDetail.Fields("INVOICE_DATE").Value, "dd/mm/yyyy") & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mCustomerName = IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_NAME").Value), "", RSSalesDetail.Fields("SUPP_CUST_NAME").Value)
        '    mCustomerName = mCustomerName & IIf(IsNull(RSSalesDetail!VENDOR_CODE), "", " - " & RSSalesDetail!VENDOR_CODE)	

        mString = mCustomerName
        mString = GetMultiLine(mString, PrintLine_Renamed, 50 - 4, 3)
        PrintLine(1, TAB(3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_ADDR").Value), "", RSSalesDetail.Fields("SUPP_CUST_ADDR").Value))
        Print(1, TAB(60), IIf(IsDbNull(RSSalesDetail.Fields("CUST_PO_NO").Value), "", RSSalesDetail.Fields("CUST_PO_NO").Value))
        PrintLine(1, TAB(74), VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("CUST_PO_DATE").Value), "", RSSalesDetail.Fields("CUST_PO_DATE").Value), "dd/mm/yyyy"))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_CITY").Value), "", RSSalesDetail.Fields("SUPP_CUST_CITY").Value)
        mString = mString & ", "
        mString = mString & IIf(IsDbNull(RSSalesDetail.Fields("SUPP_CUST_STATE").Value), "", RSSalesDetail.Fields("SUPP_CUST_STATE").Value)

        '    If RSSalesDetail!WITHIN_COUNTRY = "N" Then	
        mString = mString & ", " & IIf(IsDbNull(RSSalesDetail.Fields("COUNTRY").Value), "", RSSalesDetail.Fields("COUNTRY").Value)
        '    End If	

        '    Print #1, Tab(3); IIf(IsNull(RSSalesDetail.Fields("SUPP_CUST_CITY").Value), "", RSSalesDetail.Fields("SUPP_CUST_CITY").Value) & ", " & IIf(IsNull(RSSalesDetail.Fields("SUPP_CUST_STATE").Value), "", RSSalesDetail.Fields("SUPP_CUST_STATE").Value);	
        Print(1, TAB(3), mString)

        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(3), IIf(IsDbNull(RSSalesDetail.Fields("VENDOR_CODE").Value), "", "Vendor Code : " & RSSalesDetail.Fields("VENDOR_CODE").Value))
        If RSSalesDetail.Fields("WITHIN_STATE").Value = "Y" Then
            PrintLine(1, TAB(60), IIf(IsDbNull(RSSalesDetail.Fields("LST_No").Value), "", RSSalesDetail.Fields("LST_No").Value))
        Else
            PrintLine(1, TAB(60), IIf(IsDbNull(RSSalesDetail.Fields("CST_NO").Value), "", RSSalesDetail.Fields("CST_NO").Value))
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(3), "")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(60), IIf(IsDbNull(RSSalesDetail.Fields("ACCOUNT_CODE").Value), "", RSSalesDetail.Fields("ACCOUNT_CODE").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Do While PrintLine_Renamed <> 19
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        Loop
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub

    Public Function CreateOutPutFile(ByRef mStringName As String, ByRef pFileName As String) As Boolean
        On Error GoTo ERR1
        Dim mFilename As String

        '    mFilename = App.path & "\" & pFileName	
        mFilename = mLocalPath & "\" & pFileName
        FileOpen(1, mFilename, OpenMode.Output)
        Print(1, TAB(0), mStringName)
        FileClose(1)
        CreateOutPutFile = True
        Exit Function
ERR1:
        CreateOutPutFile = False
        MsgBox(Err.Description)
    End Function

    Private Sub PrintFooterNewFormat(ByRef pAgtPermission As String, ByRef mExtraRemarks As String)
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim mKey As String
        Dim SqlStr As String = ""
        Dim RsBillExp As ADODB.Recordset = Nothing
        Dim mExpPrintSeq As Integer

        Dim mExpName As String
        Dim mExpPer As String
        Dim pExpAmount As String
        Dim pAmount As String

        Dim mAmountInword As String
        Dim xItemValue As Double
        Dim mExpType As String
        Dim mPrintTaxableAmt As Boolean
        Dim mPrintEDAmt As Boolean
        Dim mString As String
        Dim mWithInCountry As String
        Dim mISCT3 As String
        Dim mCurrency As String
        Dim mAbatementPer As Double
        Dim mMRPAmount As Double
        Dim mPermissionNo As String

        mPrintTaxableAmt = False
        mPrintEDAmt = False

        RSSalesDetail.MoveFirst()

        mKey = RSSalesDetail.Fields("MKEY").Value
        mWithInCountry = IIf(IsDbNull(RSSalesDetail.Fields("WITHIN_COUNTRY").Value), "N", RSSalesDetail.Fields("WITHIN_COUNTRY").Value)
        mCurrency = IIf(IsDbNull(RSSalesDetail.Fields("CURRENCYNAME").Value), "RS", RSSalesDetail.Fields("CURRENCYNAME").Value)
        mAbatementPer = IIf(IsDbNull(RSSalesDetail.Fields("ABATEMENT_PER").Value), 0, RSSalesDetail.Fields("ABATEMENT_PER").Value)
        mMRPAmount = IIf(IsDbNull(RSSalesDetail.Fields("TOTMRPVALUE").Value), 0, RSSalesDetail.Fields("TOTMRPVALUE").Value)

        mISCT3 = IIf(IsDbNull(RSSalesDetail.Fields("AGTCT3").Value), "N", RSSalesDetail.Fields("AGTCT3").Value)

        SqlStr = " SELECT EXP.* , INT.*" & vbCrLf & " FROM FIN_INVOICE_EXP EXP, FIN_INTERFACE_MST INT" & vbCrLf & " WHERE " & vbCrLf & " INT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EXP.EXPCODE=INT.CODE" & vbCrLf & " AND EXP.MKEY='" & mKey & "' AND DUTYFORGONE='N'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY INT.PRINTSEQUENCE" & vbCrLf

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBillExp, ADODB.LockTypeEnum.adLockReadOnly)

        '********* Line 1	
        Print(1, TAB(0), Chr(15))
        Print(1, TAB(TabIPart), IIf(mWithInCountry = "Y", "PAYABLE", "NIL"))
        Print(1, TAB(TabUnit - 5), IIf(mWithInCountry = "Y", VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("INVOICE_DATE").Value), "", RSSalesDetail.Fields("INVOICE_DATE").Value), "DD/MM/YYYY"), "-"))

        xItemValue = RSSalesDetail.Fields("ITEMVALUE").Value

        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("ITEMVALUE").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEMVALUE").Value), "0.00")

        If mWithInCountry = "Y" Then
            If Trim(mExtraRemarks) = "" Then
                mString = "ASSESABLE VALUE" & New String(" ", 8) & ":"
            Else
                mString = "NET VALUE" & New String(" ", 8) & ":"
            End If
        Else
            mString = "TOTAL CIF VALUE" & New String(" ", 8) & ":"
        End If

        mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
        Print(1, TAB(TabQty), mString)
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        '********* Line 2	
        PrintLine(1, TAB(TabAmount), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        mExpPrintSeq = 1
        If RsBillExp.EOF = False Then
            Do While Not RsBillExp.EOF

                Call PrintFooterDetail(mExpPrintSeq)

                mExpName = UCase(IIf(IsDbNull(RsBillExp.Fields("Name").Value), "", RsBillExp.Fields("Name").Value))
                mExpName = Left(mExpName, 21)
                mExpPer = VB6.Format(IIf(IsDbNull(RsBillExp.Fields("EXPPERCENT").Value), 0, RsBillExp.Fields("EXPPERCENT").Value), "0.00")
                mExpPer = New String(" ", 5 - Len(mExpPer)) & mExpPer
                pExpAmount = VB6.Format(IIf(IsDbNull(RsBillExp.Fields("Amount").Value), 0, RsBillExp.Fields("Amount").Value), "0.00")
                pExpAmount = New String(" ", TabLastCol - TabAmount - Len(pExpAmount)) & pExpAmount

                mExpName = mExpName & IIf(CDbl(mExpPer) = 0, "", " @%" & mExpPer) & ":"
                mExpName = New String(" ", TabAmount - TabQty - Len(mExpName)) & mExpName

                mExpType = IIf(IsDbNull(RsBillExp.Fields("Identification").Value), "", RsBillExp.Fields("Identification").Value)

                If mExpType = "ED" And mPrintEDAmt = False And mWithInCountry = "N" Then
                    mString = "-"
                    mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
                    PrintLine(1, TAB(TabAmount), mString)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    Call PrintFooterDetail(mExpPrintSeq)

                    mString = "TOTAL FOB VALUE" & New String(" ", 7) & ":"
                    mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
                    xItemValue = xItemValue + IIf(IsDbNull(RSSalesDetail.Fields("TOT_EXPORTEXP").Value), 0, RSSalesDetail.Fields("TOT_EXPORTEXP").Value)
                    pAmount = VB6.Format(Trim(CStr(xItemValue)), "0.00")
                    pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount

                    Print(1, TAB(TabQty), mString)
                    PrintLine(1, TAB(TabAmount), pAmount)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    mPrintEDAmt = True
                    If CDbl(pExpAmount) <> 0 Then
                        Call PrintFooterDetail(mExpPrintSeq)
                    End If
                    mPrintEDAmt = True
                End If

                If mExpType <> "EE" And mPrintEDAmt = False And mWithInCountry = "N" Then
                    mString = "-"
                    mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
                    PrintLine(1, TAB(TabAmount), mString)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    Call PrintFooterDetail(mExpPrintSeq)

                    mString = "TOTAL FOB VALUE" & New String(" ", 7) & ":"
                    mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
                    xItemValue = xItemValue + IIf(IsDbNull(RSSalesDetail.Fields("TOT_EXPORTEXP").Value), 0, RSSalesDetail.Fields("TOT_EXPORTEXP").Value)
                    pAmount = VB6.Format(Trim(CStr(xItemValue)), "0.00")
                    pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount

                    Print(1, TAB(TabQty), mString)
                    PrintLine(1, TAB(TabAmount), pAmount)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    mPrintEDAmt = True
                    If CDbl(pExpAmount) <> 0 Then
                        Call PrintFooterDetail(mExpPrintSeq)
                    End If
                    mPrintEDAmt = True
                End If

                If mExpType = "ST" And mPrintTaxableAmt = False Then
                    mString = "-"
                    mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
                    PrintLine(1, TAB(TabAmount), mString)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    Call PrintFooterDetail(mExpPrintSeq)

                    mString = "TAXABLE AMOUNT" & New String(" ", 8) & ":"
                    mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
                    pAmount = VB6.Format(Trim(RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value), "0.00")
                    pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount

                    Print(1, TAB(TabQty), mString)
                    PrintLine(1, TAB(TabAmount), pAmount)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    mPrintTaxableAmt = True
                    If CDbl(pExpAmount) <> 0 Then
                        Call PrintFooterDetail(mExpPrintSeq)
                    End If

                    '                mString = "-"	
                    '                mString = String(TabLastCol - TabAmount - Len(mString), "-") & mString	
                    '                Print #1, Tab(TabAmount); mString	
                    '                PrintLine = PrintLine + 1	
                    '                mExpPrintSeq = mExpPrintSeq + 1	
                    '                If pExpAmount <> 0 Then	
                    '                    Call PrintFooterDetail(mExpPrintSeq)	
                    '                End If	

                    mPrintTaxableAmt = True
                End If

                If CDbl(pExpAmount) <> 0 Then
                    Print(1, TAB(TabQty), mExpName)
                    PrintLine(1, TAB(TabAmount), pExpAmount)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                End If
                RsBillExp.MoveNext()
            Loop
        End If

        If mPrintEDAmt = False And mWithInCountry = "N" Then
            mString = "-"
            mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
            PrintLine(1, TAB(TabAmount), mString)
            PrintLine_Renamed = PrintLine_Renamed + 1
            Call PrintFooterDetail(mExpPrintSeq)
            '        mExpPrintSeq = mExpPrintSeq + 1	

            mString = "TOTAL FOB VALUE" & New String(" ", 7) & ":"
            mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
            xItemValue = xItemValue + IIf(IsDbNull(RSSalesDetail.Fields("TOT_EXPORTEXP").Value), 0, RSSalesDetail.Fields("TOT_EXPORTEXP").Value)
            pAmount = VB6.Format(Trim(CStr(xItemValue)), "0.00")
            pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount

            Print(1, TAB(TabQty), mString)
            PrintLine(1, TAB(TabAmount), pAmount)
            PrintLine_Renamed = PrintLine_Renamed + 1
            mExpPrintSeq = mExpPrintSeq + 1
            mPrintEDAmt = True
        End If

        Do While mExpPrintSeq <= 10
            Call PrintFooterDetail(mExpPrintSeq)

            PrintLine(1, TAB(TabAmount), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
            mExpPrintSeq = mExpPrintSeq + 1

        Loop

        mString = "-"
        mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
        PrintLine(1, TAB(TabAmount), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        '********* Line 14	
        mAmountInword = MainClass.RupeesConversion(IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), 0, RSSalesDetail.Fields("NETVALUE").Value))
        Print(1, TAB(3), mAmountInword)

        pAmount = VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), "0", RSSalesDetail.Fields("NETVALUE").Value), "0.00")
        pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount
        mString = "BILL AMOUNT" & New String(" ", 7) & ":"
        '    mString = String(TabAmount - TabQty - Len(mString), " ") & mString	
        '    Print #1, Tab(TabQty); mString;	
        Print(1, TAB(TabQty + TabAmount - TabQty - Len(mString)), mString)
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        ''********* Line 15	
        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        mRemarks = IIf(IsDbNull(RSSalesDetail.Fields("REMARKS").Value), "", RSSalesDetail.Fields("REMARKS").Value) ''& IIf(IsNull(RSSalesDetail.Fields("DNCNNO").Value), "", " Our Debit Note No.: " & RSSalesDetail.Fields("DNCNNO").Value)	
        mRemarks = GetMultiLine(mRemarks, PrintLine_Renamed, TabQty - TabIDesc, TabIDesc)

        If Trim(mRemarks) <> "" Then
            PrintLine(1, TAB(TabIDesc), mRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        mRemarks = IIf(IsDbNull(RSSalesDetail.Fields("NARRATION").Value), "", RSSalesDetail.Fields("NARRATION").Value) ''& IIf(IsNull(RSSalesDetail.Fields("DNCNNO").Value), "", " Our Debit Note No.: " & RSSalesDetail.Fields("DNCNNO").Value)	
        If Trim(mRemarks) <> "" Then
            PrintLine(1, TAB(TabIDesc), "ST FORM 16 NO : " & mRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        If mAbatementPer > 0 Then
            mAbatementPer = 100 - mAbatementPer
            mRemarks = "Basic Excise Duty on " & mAbatementPer & "% of MRP Rs. " & VB6.Format(mMRPAmount, "0.00")
            mRemarks = mRemarks & ". '*' :- MRP Rate"
            PrintLine(1, TAB(TabIDesc), mRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        If mISCT3 = "Y" Then
            mRemarks = "Goods Clear Agt. ARE3 No : " & IIf(IsDbNull(RSSalesDetail.Fields("ARE_NO").Value), "", RSSalesDetail.Fields("ARE_NO").Value)
            mRemarks = mRemarks & " & CT3 No : " & IIf(IsDbNull(RSSalesDetail.Fields("CT_NO").Value), "", RSSalesDetail.Fields("CT_NO").Value)
            mRemarks = mRemarks & " Dt. : " & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("CT3_DATE").Value), "", RSSalesDetail.Fields("CT3_DATE").Value), "DD/MM/YYYY")

            PrintLine(1, TAB(TabIDesc), mRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        If mWithInCountry = "N" Then

            mRemarks = "Shipping Bill No : " & IIf(IsDbNull(RSSalesDetail.Fields("SHIPPING_NO").Value), "", RSSalesDetail.Fields("SHIPPING_NO").Value)
            mRemarks = mRemarks & New String(" ", 40 - Len(mRemarks))
            mRemarks = mRemarks & " Dated : " & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("SHIPPING_DATE").Value), "", RSSalesDetail.Fields("SHIPPING_DATE").Value), "DD/MM/YYYY")
            PrintLine(1, TAB(TabIDesc), mRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1

            mRemarks = "ARE1 No : " & IIf(IsDbNull(RSSalesDetail.Fields("ARE1_NO").Value), "", RSSalesDetail.Fields("ARE1_NO").Value)
            mRemarks = mRemarks & New String(" ", 40 - Len(mRemarks))
            mRemarks = mRemarks & " Dated : " & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("ARE1_DATE").Value), "", RSSalesDetail.Fields("ARE1_DATE").Value), "DD/MM/YYYY")
            PrintLine(1, TAB(TabIDesc), mRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1

            mRemarks = "Export Invoice No : " & IIf(IsDbNull(RSSalesDetail.Fields("EXPBILLNO").Value), "", RSSalesDetail.Fields("EXPBILLNO").Value)
            mRemarks = mRemarks & New String(" ", 40 - Len(mRemarks))
            mRemarks = mRemarks & " Dated : " & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("EXPINV_DATE").Value), "", RSSalesDetail.Fields("EXPINV_DATE").Value), "DD/MM/YYYY")
            PrintLine(1, TAB(TabIDesc), mRemarks)

            mRemarks = "Exchange Rate : " & mCurrency & " @" & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("EXCHANGE_RATE").Value), "0", RSSalesDetail.Fields("EXCHANGE_RATE").Value), "0.00")
            mRemarks = mRemarks & New String(" ", 40 - Len(mRemarks))
            mRemarks = mRemarks & " Exchange Value : " & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("TOTEXCHANGEVALUE").Value), "", RSSalesDetail.Fields("TOTEXCHANGEVALUE").Value), "0.00")
            PrintLine(1, TAB(TabIDesc), mRemarks)

            mRemarks = "Advance License No. : " & IIf(IsDbNull(RSSalesDetail.Fields("ADV_LICENSE").Value), "", RSSalesDetail.Fields("ADV_LICENSE").Value)
            PrintLine(1, TAB(TabIDesc), mRemarks)

            mRemarks = "Location : " & IIf(IsDbNull(RSSalesDetail.Fields("DESP_LOCATION").Value), "", RSSalesDetail.Fields("DESP_LOCATION").Value)
            PrintLine(1, TAB(TabIDesc), mRemarks)

            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        If RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
        Else
            mRemarks = "Regd Office : " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
            PrintLine(1, TAB(TabSNo), mRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1

            mRemarks = "              " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value) Or RsCompany.Fields("REGD_ADDR2").Value = "", "", RsCompany.Fields("REGD_ADDR2").Value)
            mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value) Or RsCompany.Fields("REGD_CITY").Value = "", "", RsCompany.Fields("REGD_CITY").Value)
            mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_STATE").Value) Or RsCompany.Fields("REGD_STATE").Value = "", "", " - " & RsCompany.Fields("REGD_STATE").Value)
            mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_PHONE").Value) Or RsCompany.Fields("REGD_PHONE").Value = "", "", " Phone : " & RsCompany.Fields("REGD_PHONE").Value)
            PrintLine(1, TAB(TabSNo), mRemarks)
        End If

        If pAgtPermission = "Y" Then

            If CDate(RSSalesDetail.Fields("INVOICE_DATE").Value) >= CDate("18/03/2016") Then
                mPermissionNo = "F.NO.C.NO.CE/Tech/Div-III/GGN-I/Jobwork/23/14-15/549 Dt.17/03/2016"
            Else
                mPermissionNo = "F.NO.C.NO.CE/Tech/Div-III/GGN-I/Jobwork/23/14-15/569 Dt.26/03/2015"
            End If

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, TAB(TabIDesc), Chr(15) & "Removed from the premises of job-worker : M/s EUROTHERM HEMA RADIATORS INDIA LTD. UNIT-II" & Chr(18))

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, TAB(TabIDesc), Chr(15) & "PLOT NO. 5 & 14, SECTOR-6, HSIDC INDUSTRIAL COMPLEX, BAWAL (REWARI) HARYANA . Central Excise Regn. No. : AABCE3677REM002" & Chr(18))

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, TAB(TabIDesc), Chr(15) & "as permitted by the of Assistant/Deputy Commissioner,Central Excise,Division under" & Chr(18))

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, TAB(TabIDesc), Chr(15) & mPermissionNo & ", Rule4(6) of the Cenvat Credit Rules,2004" & Chr(18))
        End If

        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), "" & Chr(12))
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub
    Private Sub PrintFooter_PlainPaper(ByRef pAgtPermission As String)
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim mKey As String
        Dim SqlStr As String = ""
        Dim RsBillExp As ADODB.Recordset = Nothing
        Dim mExpPrintSeq As Integer

        Dim mExpName As String
        Dim mExpPer As String
        Dim pExpAmount As String
        Dim pAmount As String

        Dim mAmountInword As String
        Dim xItemValue As Double
        Dim mExpType As String
        Dim mPrintTaxableAmt As Boolean
        Dim mPrintEDAmt As Boolean
        Dim mString As String
        Dim mString1 As String
        Dim mString2 As String
        Dim mWithInCountry As String
        Dim mISCT3 As String
        Dim mCurrency As String
        Dim mAbatementPer As Double
        Dim mMRPAmount As Double
        Dim mPermissionNo As String
        mPrintTaxableAmt = False
        mPrintEDAmt = False

        RSSalesDetail.MoveFirst()

        mKey = RSSalesDetail.Fields("MKEY").Value
        mWithInCountry = IIf(IsDbNull(RSSalesDetail.Fields("WITHIN_COUNTRY").Value), "N", RSSalesDetail.Fields("WITHIN_COUNTRY").Value)
        mCurrency = IIf(IsDbNull(RSSalesDetail.Fields("CURRENCYNAME").Value), "RS", RSSalesDetail.Fields("CURRENCYNAME").Value)
        mAbatementPer = IIf(IsDbNull(RSSalesDetail.Fields("ABATEMENT_PER").Value), 0, RSSalesDetail.Fields("ABATEMENT_PER").Value)
        mMRPAmount = IIf(IsDbNull(RSSalesDetail.Fields("TOTMRPVALUE").Value), 0, RSSalesDetail.Fields("TOTMRPVALUE").Value)

        mISCT3 = IIf(IsDbNull(RSSalesDetail.Fields("AGTCT3").Value), "N", RSSalesDetail.Fields("AGTCT3").Value)

        SqlStr = " SELECT EXP.* , INT.*" & vbCrLf & " FROM FIN_INVOICE_EXP EXP, FIN_INTERFACE_MST INT" & vbCrLf & " WHERE " & vbCrLf & " INT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EXP.EXPCODE=INT.CODE" & vbCrLf & " AND EXP.MKEY='" & mKey & "' AND DUTYFORGONE='N'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY INT.PRINTSEQUENCE" & vbCrLf

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBillExp, ADODB.LockTypeEnum.adLockReadOnly)

        '********* Line 1	
        Print(1, TAB(0), Chr(15))
        Print(1, TAB(3), "Duty Payable U/R 8 of CE (No. 2) Rules, 2002" & IIf(mWithInCountry = "Y", "PAYABLE", "NIL"))
        '    Print #1, Tab(TabIPart); IIf(mWithInCountry = "Y", "PAYABLE", "NIL");	
        Print(1, TAB(TabUnit - 20), "Dt. : " & IIf(mWithInCountry = "Y", VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("INVOICE_DATE").Value), "", RSSalesDetail.Fields("INVOICE_DATE").Value), "DD/MM/YYYY"), "-"))

        xItemValue = RSSalesDetail.Fields("ITEMVALUE").Value

        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("ITEMVALUE").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEMVALUE").Value), "0.00")

        If mWithInCountry = "Y" Then
            mString = "ASSESABLE VALUE" & New String(" ", 8) & ":"
        Else
            mString = "TOTAL CIF VALUE" & New String(" ", 8) & ":"
        End If

        mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
        Print(1, TAB(TabQty), mString)
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "-"
        mString = New String("-", TabLastCol) & mString
        PrintLine(1, TAB(3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        '********* Line 2	
        '    Print #1, Tab(TabAmount); " "	
        '    PrintLine = PrintLine + 1	

        mExpPrintSeq = 1
        If RsBillExp.EOF = False Then
            Do While Not RsBillExp.EOF

                Call PrintFooterDetail_PP(mExpPrintSeq)

                mExpName = UCase(IIf(IsDbNull(RsBillExp.Fields("Name").Value), "", RsBillExp.Fields("Name").Value))
                mExpName = Left(mExpName, 21)
                mExpPer = VB6.Format(IIf(IsDbNull(RsBillExp.Fields("EXPPERCENT").Value), 0, RsBillExp.Fields("EXPPERCENT").Value), "0.00")
                mExpPer = New String(" ", 5 - Len(mExpPer)) & mExpPer
                pExpAmount = VB6.Format(IIf(IsDbNull(RsBillExp.Fields("Amount").Value), 0, RsBillExp.Fields("Amount").Value), "0.00")
                pExpAmount = New String(" ", TabLastCol - TabAmount - Len(pExpAmount)) & pExpAmount

                mExpName = mExpName & IIf(CDbl(mExpPer) = 0, "", " @%" & mExpPer) & ":"
                mExpName = New String(" ", TabAmount - TabQty - Len(mExpName)) & mExpName

                mExpType = IIf(IsDbNull(RsBillExp.Fields("Identification").Value), "", RsBillExp.Fields("Identification").Value)

                If mExpType = "ED" And mPrintEDAmt = False And mWithInCountry = "N" Then
                    mString = "-"
                    mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
                    PrintLine(1, TAB(TabAmount), mString)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    Call PrintFooterDetail_PP(mExpPrintSeq)

                    mString = "TOTAL FOB VALUE" & New String(" ", 7) & ":"
                    mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
                    xItemValue = xItemValue + IIf(IsDbNull(RSSalesDetail.Fields("TOT_EXPORTEXP").Value), 0, RSSalesDetail.Fields("TOT_EXPORTEXP").Value)
                    pAmount = VB6.Format(Trim(CStr(xItemValue)), "0.00")
                    pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount

                    Print(1, TAB(TabQty), mString)
                    PrintLine(1, TAB(TabAmount), pAmount)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    mPrintEDAmt = True
                    If CDbl(pExpAmount) <> 0 Then
                        Call PrintFooterDetail_PP(mExpPrintSeq)
                    End If
                    mPrintEDAmt = True
                End If

                If mExpType <> "EE" And mPrintEDAmt = False And mWithInCountry = "N" Then
                    mString = "-"
                    mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
                    PrintLine(1, TAB(TabAmount), mString)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    Call PrintFooterDetail_PP(mExpPrintSeq)

                    mString = "TOTAL FOB VALUE" & New String(" ", 7) & ":"
                    mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
                    xItemValue = xItemValue + IIf(IsDbNull(RSSalesDetail.Fields("TOT_EXPORTEXP").Value), 0, RSSalesDetail.Fields("TOT_EXPORTEXP").Value)
                    pAmount = VB6.Format(Trim(CStr(xItemValue)), "0.00")
                    pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount

                    Print(1, TAB(TabQty), mString)
                    PrintLine(1, TAB(TabAmount), pAmount)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    mPrintEDAmt = True
                    If CDbl(pExpAmount) <> 0 Then
                        Call PrintFooterDetail_PP(mExpPrintSeq)
                    End If
                    mPrintEDAmt = True
                End If

                If mExpType = "ST" And mPrintTaxableAmt = False Then
                    mString = "-"
                    mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
                    PrintLine(1, TAB(TabAmount), mString)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    Call PrintFooterDetail_PP(mExpPrintSeq)

                    mString = "TAXABLE AMOUNT" & New String(" ", 8) & ":"
                    mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
                    pAmount = VB6.Format(Trim(RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value), "0.00")
                    pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount

                    Print(1, TAB(TabQty), mString)
                    PrintLine(1, TAB(TabAmount), pAmount)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    mPrintTaxableAmt = True
                    If CDbl(pExpAmount) <> 0 Then
                        Call PrintFooterDetail_PP(mExpPrintSeq)
                    End If

                    '                mString = "-"	
                    '                mString = String(TabLastCol - TabAmount - Len(mString), "-") & mString	
                    '                Print #1, Tab(TabAmount); mString	
                    '                PrintLine = PrintLine + 1	
                    '                mExpPrintSeq = mExpPrintSeq + 1	
                    '                If pExpAmount <> 0 Then	
                    '                    Call PrintFooterDetail_PP(mExpPrintSeq)	
                    '                End If	

                    mPrintTaxableAmt = True
                End If

                If CDbl(pExpAmount) <> 0 Then
                    Print(1, TAB(TabQty), mExpName)
                    PrintLine(1, TAB(TabAmount), pExpAmount)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                End If
                RsBillExp.MoveNext()
            Loop
        End If

        If mPrintEDAmt = False And mWithInCountry = "N" Then
            mString = "-"
            mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
            PrintLine(1, TAB(TabAmount), mString)
            PrintLine_Renamed = PrintLine_Renamed + 1
            mExpPrintSeq = mExpPrintSeq + 1
            Call PrintFooterDetail_PP(mExpPrintSeq)

            mString = "TOTAL FOB VALUE" & New String(" ", 7) & ":"
            mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
            xItemValue = xItemValue + IIf(IsDbNull(RSSalesDetail.Fields("TOT_EXPORTEXP").Value), 0, RSSalesDetail.Fields("TOT_EXPORTEXP").Value)
            pAmount = VB6.Format(Trim(CStr(xItemValue)), "0.00")
            pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount

            Print(1, TAB(TabQty), mString)
            PrintLine(1, TAB(TabAmount), pAmount)
            PrintLine_Renamed = PrintLine_Renamed + 1
            mExpPrintSeq = mExpPrintSeq + 1
            mPrintEDAmt = True
        End If

        Do While mExpPrintSeq <= 8
            Call PrintFooterDetail_PP(mExpPrintSeq)

            PrintLine(1, TAB(TabAmount), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
            mExpPrintSeq = mExpPrintSeq + 1

        Loop

        mString = "-"
        mString = New String("-", TabLastCol) & mString
        PrintLine(1, TAB(3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        '********* Line 14	
        mAmountInword = MainClass.RupeesConversion(IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), 0, RSSalesDetail.Fields("NETVALUE").Value))
        Print(1, TAB(3), "Rs. : " & mAmountInword)

        pAmount = VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), "0", RSSalesDetail.Fields("NETVALUE").Value), "0.00")
        pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount
        mString = "BILL AMOUNT" & New String(" ", 7) & ":"
        '    mString = String(TabAmount - TabQty - Len(mString), " ") & mString	
        '    Print #1, Tab(TabQty); mString;	
        Print(1, TAB(TabQty + TabAmount - TabQty - Len(mString)), mString)
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        ''********* Line 15	
        '    Print #1, Tab(0); " "	
        '    PrintLine = PrintLine + 1	

        mString = "-"
        mString = New String("-", TabLastCol) & mString
        PrintLine(1, TAB(3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "- Certified that particulars given above are true and correct and the amount indicated represents the price actually charged and there"
        mString1 = "  is no flow of additional consideration directly or indirectly from the buyer."
        PrintLine(1, TAB(3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1
        PrintLine(1, TAB(3), mString1)
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    mString = "-"	
        '    mString = String(TabLastCol, "-") & mString	
        '    Print #1, Tab(3); mString	
        '    PrintLine = PrintLine + 1	

        mString = "- Cretified that the Cenvat duty shown in the Invoice has been paid by us to the Central Govt. in a accordance with the Cenvat Credit Rules, 2002."
        PrintLine(1, TAB(3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "-"
        mString = New String("-", TabLastCol) & mString
        PrintLine(1, TAB(3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mRemarks = IIf(IsDbNull(RSSalesDetail.Fields("REMARKS").Value), "", RSSalesDetail.Fields("REMARKS").Value) ''& IIf(IsNull(RSSalesDetail.Fields("DNCNNO").Value), "", " Our Debit Note No.: " & RSSalesDetail.Fields("DNCNNO").Value)	
        mRemarks = GetMultiLine(mRemarks, PrintLine_Renamed, TabQty - TabIDesc, TabIDesc)


        Print(1, TAB(TabIDesc), mRemarks)
        mString = "For " & RsCompany.Fields("Company_Name").Value
        mString = New String(" ", TabLastCol - TabUnit - Len(mString)) & mString
        PrintLine(1, TAB(TabUnit), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        If mAbatementPer > 0 Then
            mAbatementPer = 100 - mAbatementPer
            mRemarks = "Basic Excise Duty on " & mAbatementPer & "% of MRP Rs. " & VB6.Format(mMRPAmount, "0.00")
            mRemarks = mRemarks & ". '*' :- MRP Rate"
            PrintLine(1, TAB(TabIDesc), mRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        If mISCT3 = "Y" Then
            mRemarks = "Goods Clear Agt. ARE3 No : " & IIf(IsDbNull(RSSalesDetail.Fields("ARE_NO").Value), "", RSSalesDetail.Fields("ARE_NO").Value)
            mRemarks = mRemarks & " & CT3 No : " & IIf(IsDbNull(RSSalesDetail.Fields("CT_NO").Value), "", RSSalesDetail.Fields("CT_NO").Value)
            mRemarks = mRemarks & " Dt. : " & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("CT3_DATE").Value), "", RSSalesDetail.Fields("CT3_DATE").Value), "DD/MM/YYYY")

            PrintLine(1, TAB(TabIDesc), mRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        If mWithInCountry = "N" Then

            mRemarks = "Shipping Bill No : " & IIf(IsDbNull(RSSalesDetail.Fields("SHIPPING_NO").Value), "", RSSalesDetail.Fields("SHIPPING_NO").Value)
            mRemarks = mRemarks & New String(" ", 40 - Len(mRemarks))
            mRemarks = mRemarks & " Dated : " & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("SHIPPING_DATE").Value), "", RSSalesDetail.Fields("SHIPPING_DATE").Value), "DD/MM/YYYY")
            PrintLine(1, TAB(TabIDesc), mRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1

            mRemarks = "ARE1 No : " & IIf(IsDbNull(RSSalesDetail.Fields("ARE1_NO").Value), "", RSSalesDetail.Fields("ARE1_NO").Value)
            mRemarks = mRemarks & New String(" ", 40 - Len(mRemarks))
            mRemarks = mRemarks & " Dated : " & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("ARE1_DATE").Value), "", RSSalesDetail.Fields("ARE1_DATE").Value), "DD/MM/YYYY")
            PrintLine(1, TAB(TabIDesc), mRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1

            mRemarks = "Export Invoice No : " & IIf(IsDbNull(RSSalesDetail.Fields("EXPBILLNO").Value), "", RSSalesDetail.Fields("EXPBILLNO").Value)
            mRemarks = mRemarks & New String(" ", 40 - Len(mRemarks))
            mRemarks = mRemarks & " Dated : " & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("EXPINV_DATE").Value), "", RSSalesDetail.Fields("EXPINV_DATE").Value), "DD/MM/YYYY")
            PrintLine(1, TAB(TabIDesc), mRemarks)

            mRemarks = "Exchange Rate : " & mCurrency & " @" & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("EXCHANGE_RATE").Value), "0", RSSalesDetail.Fields("EXCHANGE_RATE").Value), "0.00")
            mRemarks = mRemarks & New String(" ", 40 - Len(mRemarks))
            mRemarks = mRemarks & " Exchange Value : " & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("TOTEXCHANGEVALUE").Value), "", RSSalesDetail.Fields("TOTEXCHANGEVALUE").Value), "0.00")
            PrintLine(1, TAB(TabIDesc), mRemarks)

            mRemarks = "Advance License No. : " & IIf(IsDbNull(RSSalesDetail.Fields("ADV_LICENSE").Value), "", RSSalesDetail.Fields("ADV_LICENSE").Value)
            PrintLine(1, TAB(TabIDesc), mRemarks)

            mRemarks = "Location : " & IIf(IsDbNull(RSSalesDetail.Fields("DESP_LOCATION").Value), "", RSSalesDetail.Fields("DESP_LOCATION").Value)
            PrintLine(1, TAB(TabIDesc), mRemarks)

            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        PrintLine(1, TAB(3), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(3); " "	
        '    PrintLine = PrintLine + 1	

        '    Print #1, Tab(3); " "	
        '    PrintLine = PrintLine + 1	

        mString = "Authorised Signatory"
        mString = New String(" ", TabLastCol - TabUnit - Len(mString)) & mString
        PrintLine(1, TAB(TabUnit), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1


        mString = "-"
        mString = New String("-", TabLastCol) & mString
        PrintLine(1, TAB(3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    If RsCompany!COMPANY_CODE = 15 Or RsCompany!COMPANY_CODE = 25 Then	
        '        mString = "All disputes subject to Haridwar Jurisdiction"	
        '    ElseIf RsCompany!COMPANY_CODE = 5 Or RsCompany!COMPANY_CODE = 21 Then	
        '        mString = "All disputes subject to Rewari Jurisdiction"	
        '    Else	
        '        mString = "All disputes subject to Gurgaon Jurisdiction"	
        '    End If	

        If pAgtPermission = "Y" Then

            If CDate(RSSalesDetail.Fields("INVOICE_DATE").Value) >= CDate("18/03/2016") Then
                mPermissionNo = "F.NO.C.NO.CE/Tech/Div-III/GGN-I/Jobwork/23/14-15/549 Dt.17/03/2016"
            Else
                mPermissionNo = "F.NO.C.NO.CE/Tech/Div-III/GGN-I/Jobwork/23/14-15/569 Dt.26/03/2015"
            End If

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, TAB(TabIDesc), Chr(15) & "Removed from the premises of job-worker : M/s EUROTHERM HEMA RADIATORS INDIA LTD. UNIT-II" & Chr(18))

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, TAB(TabIDesc), Chr(15) & "PLOT NO. 5 & 14, SECTOR-6, HSIDC INDUSTRIAL COMPLEX, BAWAL (REWARI) HARYANA. Central Excise Regn. No. : AABCE3677REM002" & Chr(18))

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, TAB(TabIDesc), Chr(15) & "as permitted by the of Assistant/Deputy Commissioner,Central Excise,Division under" & Chr(18))

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, TAB(TabIDesc), Chr(15) & mPermissionNo & ", Rule4(6) of the Cenvat Credit Rules,2004" & Chr(18))
        End If

        mRemarks = "Regd Office : " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_STATE").Value), "", " - " & RsCompany.Fields("REGD_STATE").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_PHONE").Value), "", " Phone : " & RsCompany.Fields("REGD_PHONE").Value)
        PrintLine(1, TAB(TabSNo), Chr(15) & mRemarks)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "-"
        mString = New String("-", TabLastCol) & mString
        PrintLine(1, TAB(3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "All disputes subject to " & IIf(IsDbNull(RsCompany.Fields("JURISDICTION").Value), "", RsCompany.Fields("JURISDICTION").Value) & " Jurisdiction." & Chr(18)

        PrintLine(1, TAB(3), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), "" & Chr(12))
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub


    Private Sub PrintAnnexFooter()
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim mKey As String
        Dim SqlStr As String = ""
        Dim RsBillExp As ADODB.Recordset = Nothing
        Dim mExpPrintSeq As Integer

        Dim mExpName As String
        Dim mExpPer As String
        Dim pExpAmount As String
        Dim pAmount As String

        Dim mAmountInword As String
        Dim xItemValue As Double
        Dim mExpType As String
        Dim mPrintTaxableAmt As Boolean
        Dim mPrintEDAmt As Boolean
        Dim mString As String
        Dim mWithInCountry As String
        Dim mISCT3 As String
        Dim mCurrency As String
        Dim mAssessableValue As Double
        Dim mAbatementPer As Double

        mPrintTaxableAmt = False
        mPrintEDAmt = False

        RSSalesDetail.MoveFirst()

        mKey = RSSalesDetail.Fields("mKey").Value
        mWithInCountry = IIf(IsDbNull(RSSalesDetail.Fields("WITHIN_COUNTRY").Value), "N", RSSalesDetail.Fields("WITHIN_COUNTRY").Value)
        mCurrency = IIf(IsDbNull(RSSalesDetail.Fields("CURRENCYNAME").Value), "RS", RSSalesDetail.Fields("CURRENCYNAME").Value)
        mAbatementPer = 100 - IIf(IsDbNull(RSSalesDetail.Fields("ABATEMENT_PER").Value), 0, RSSalesDetail.Fields("ABATEMENT_PER").Value)

        mISCT3 = IIf(IsDbNull(RSSalesDetail.Fields("AGTCT3").Value), "N", RSSalesDetail.Fields("AGTCT3").Value)

        SqlStr = " SELECT EXP.* , INT.*" & vbCrLf & " FROM FIN_INVOICE_EXP EXP, FIN_INTERFACE_MST INT" & vbCrLf & " WHERE " & vbCrLf & " INT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EXP.EXPCODE=INT.CODE" & vbCrLf & " AND EXP.MKEY='" & mKey & "'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY INT.PRINTSEQUENCE"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBillExp, ADODB.LockTypeEnum.adLockReadOnly)

        '********* Line 1	
        Print(1, TAB(0), Chr(15))

        xItemValue = RSSalesDetail.Fields("ITEMVALUE").Value

        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("ITEMVALUE").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEMVALUE").Value), "0.00")

        mString = "ITEM VALUE" & New String(" ", 8) & ":"
        mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
        Print(1, TAB(TabQty), mString)
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "ASSESABLE VALUE" & New String(" ", 8) & ":"

        '********* Line 2	
        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("TOTMRPVALUE").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTMRPVALUE").Value), "0.00")
        mString = "MRP VALUE" & New String(" ", 8) & ":"
        mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
        Print(1, TAB(TabQty), mString)
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        '********* Line 3	
        mAssessableValue = IIf(IsDbNull(RSSalesDetail.Fields("TOTMRPVALUE").Value), 0, RSSalesDetail.Fields("TOTMRPVALUE").Value)
        mAssessableValue = mAssessableValue - (mAssessableValue * 0.01 * IIf(IsDbNull(RSSalesDetail.Fields("ABATEMENT_PER").Value), 0, RSSalesDetail.Fields("ABATEMENT_PER").Value))

        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(mAssessableValue, "0.00"))) & VB6.Format(Trim(CStr(mAssessableValue)), "0.00")
        mString = "ASSESABLE VALUE" & New String(" ", 8) & ":"
        mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
        Print(1, TAB(TabQty), mString)
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mString = "(" & mAbatementPer & "% of MRP)" & New String(" ", 8) & ""
        mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
        PrintLine(1, TAB(TabQty), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mExpPrintSeq = 1
        If RsBillExp.EOF = False Then
            Do While Not RsBillExp.EOF

                mExpName = UCase(IIf(IsDbNull(RsBillExp.Fields("Name").Value), "", RsBillExp.Fields("Name").Value))
                mExpName = Left(mExpName, 21)
                mExpPer = VB6.Format(IIf(IsDbNull(RsBillExp.Fields("EXPPERCENT").Value), 0, RsBillExp.Fields("EXPPERCENT").Value), "0.00")
                mExpPer = New String(" ", 5 - Len(mExpPer)) & mExpPer
                pExpAmount = VB6.Format(IIf(IsDbNull(RsBillExp.Fields("Amount").Value), 0, RsBillExp.Fields("Amount").Value), "0.00")
                pExpAmount = New String(" ", TabLastCol - TabAmount - Len(pExpAmount)) & pExpAmount

                mExpName = mExpName & IIf(CDbl(mExpPer) = 0, "", " @%" & mExpPer) & ":"
                mExpName = New String(" ", TabAmount - TabQty - Len(mExpName)) & mExpName

                mExpType = IIf(IsDbNull(RsBillExp.Fields("Identification").Value), "", RsBillExp.Fields("Identification").Value)

                If mExpType = "ST" And mPrintTaxableAmt = False Then
                    mString = "-"
                    mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
                    PrintLine(1, TAB(TabAmount), mString)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1

                    mString = "TAXABLE AMOUNT" & New String(" ", 8) & ":"
                    mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
                    pAmount = VB6.Format(Trim(RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value), "0.00")
                    pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount

                    Print(1, TAB(TabQty), mString)
                    PrintLine(1, TAB(TabAmount), pAmount)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    mPrintTaxableAmt = True
                End If

                If CDbl(pExpAmount) <> 0 Then
                    Print(1, TAB(TabQty), mExpName)
                    PrintLine(1, TAB(TabAmount), pExpAmount)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                End If
                RsBillExp.MoveNext()
            Loop
        End If

        Do While mExpPrintSeq <= 10
            PrintLine(1, TAB(TabAmount), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
            mExpPrintSeq = mExpPrintSeq + 1

        Loop

        mString = "-"
        mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
        PrintLine(1, TAB(TabAmount), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        '********* Line 14	
        mAmountInword = MainClass.RupeesConversion(IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), 0, RSSalesDetail.Fields("NETVALUE").Value))
        Print(1, TAB(3), mAmountInword)

        pAmount = VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), "0", RSSalesDetail.Fields("NETVALUE").Value), "0.00")
        pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount
        mString = "BILL AMOUNT" & New String(" ", 7) & ":"
        '    mString = String(TabAmount - TabQty - Len(mString), " ") & mString	
        '    Print #1, Tab(TabQty); mString;	
        Print(1, TAB(TabQty + TabAmount - TabQty - Len(mString)), mString)
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        ''********* Line 15	
        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        mRemarks = IIf(IsDbNull(RSSalesDetail.Fields("REMARKS").Value), "", RSSalesDetail.Fields("REMARKS").Value) & IIf(IsDbNull(RSSalesDetail.Fields("DNCNNO").Value), "", " Our Debit Note No.: " & RSSalesDetail.Fields("DNCNNO").Value)
        mRemarks = GetMultiLine(mRemarks, PrintLine_Renamed, TabQty - TabIDesc, TabIDesc)

        If Trim(mRemarks) <> "" Then
            PrintLine(1, TAB(TabIDesc), mRemarks)
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        PrintLine(1, TAB(0), "" & Chr(12))
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub
    Private Sub PrintFooterDetail(ByRef mExpPrintSeq As Integer)
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim pAmount As String
        Dim pDutyAmount As Double
        Dim mAmountInword As String
        Dim pGRNo As String = ""
        Dim mString As String
        Dim mDutyForgone As String

        If mExpPrintSeq = 1 Then
            mDutyForgone = CheckDutyForegone(RSSalesDetail.Fields("mKey").Value) 'IIf(IsNull(RSSalesDetail.Fields("ISDUTY_FORGONE").Value), "N", RSSalesDetail.Fields("ISDUTY_FORGONE").Value)	
            mString = IIf(mDutyForgone = "Y", "Duty forgone : ", "")
            ''06-06-2009	
            '                pAmount = IIf(IsNull(RSSalesDetail.Fields("TOTEDAMOUNT").Value), 0, RSSalesDetail.Fields("TOTEDAMOUNT").Value)	
            '                pDutyAmount = IIf(IsNull(RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value), 0, RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value)	
            '                pDutyAmount = pDutyAmount - IIf(IsNull(RSSalesDetail.Fields("ITEMVALUE").Value), 0, RSSalesDetail.Fields("ITEMVALUE").Value)	
            '                pDutyAmount = pDutyAmount - IIf(IsNull(RSSalesDetail.Fields("TOT_EXPORTEXP").Value), 0, RSSalesDetail.Fields("TOT_EXPORTEXP").Value)	
            pDutyAmount = GetInvoiceDutyAmount(RSSalesDetail.Fields("mKey").Value)
            pAmount = CStr(pDutyAmount)
            'UPGRADE_WARNING: Untranslated statement in PrintFooterDetail. Please check source code.	
        End If

        If mExpPrintSeq = 2 Then
            ''06-06-2009	
            '                mAmountInword = MainClass.RupeesConversion(IIf(IsNull(RSSalesDetail.Fields("TOTEDAMOUNT").Value), 0, RSSalesDetail.Fields("TOTEDAMOUNT").Value))	
            '                pDutyAmount = IIf(IsNull(RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value), 0, RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value)	
            '                pDutyAmount = pDutyAmount - IIf(IsNull(RSSalesDetail.Fields("ITEMVALUE").Value), 0, RSSalesDetail.Fields("ITEMVALUE").Value)	
            '                pDutyAmount = pDutyAmount - IIf(IsNull(RSSalesDetail.Fields("TOT_EXPORTEXP").Value), 0, RSSalesDetail.Fields("TOT_EXPORTEXP").Value)	
            pDutyAmount = GetInvoiceDutyAmount(RSSalesDetail.Fields("mKey").Value)
            mAmountInword = MainClass.RupeesConversion(pDutyAmount)

            Print(1, TAB(11), mAmountInword)
        End If

        If mExpPrintSeq = 5 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("STFORMNAME").Value), "", RSSalesDetail.Fields("STFORMNAME").Value)
            pAmount = New String(" ", TabAmount - TabQty - Len(pAmount)) & pAmount ''TabRate	
            Print(1, TAB(TabIPart), pAmount)
        End If

        If mExpPrintSeq = 7 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("DESPATCHMODE").Value), "", RSSalesDetail.Fields("DESPATCHMODE").Value)
            Print(1, TAB(24), pAmount)

            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("FREIGHTCHARGES").Value), "", RSSalesDetail.Fields("FREIGHTCHARGES").Value)
            Print(1, TAB(TabUnit - 2), pAmount)
        End If

        If mExpPrintSeq = 8 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("VEHICLENO").Value), "", RSSalesDetail.Fields("VEHICLENO").Value)
            Print(1, TAB(24), pAmount)

            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("GRNO").Value), "", RSSalesDetail.Fields("GRNO").Value)
            pGRNo = IIf(IsDbNull(RSSalesDetail.Fields("GRNO").Value), "", RSSalesDetail.Fields("GRNO").Value)
            Print(1, TAB(TabUnit - 5), pAmount)
        End If

        If mExpPrintSeq = 9 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("CARRIERS").Value), "", RSSalesDetail.Fields("CARRIERS").Value)
            Print(1, TAB(24), pAmount)

            If Trim(pGRNo) = "" Then
                pAmount = ""
            Else
                pAmount = IIf(IsDbNull(RSSalesDetail.Fields("GRDATE").Value), "", RSSalesDetail.Fields("GRDATE").Value)
            End If
            Print(1, TAB(TabUnit - 5), pAmount)
        End If

        If mExpPrintSeq = 10 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("DOCSTHROUGH").Value), "", RSSalesDetail.Fields("DOCSTHROUGH").Value)
            Print(1, TAB(TabIDesc), pAmount)

            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("ST_38_NO").Value), "", RSSalesDetail.Fields("ST_38_NO").Value)
            Print(1, TAB(TabUnit - 5), pAmount)
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub
    Private Sub PrintFooterDetail_PP(ByRef mExpPrintSeq As Integer)
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim pAmount As String
        Dim pDutyAmount As Double
        Dim mAmountInword As String
        Dim pGRNo As String = ""
        Dim mString As String
        Dim mDutyForgone As String

        If mExpPrintSeq = 1 Then
            mDutyForgone = CheckDutyForegone(RSSalesDetail.Fields("mKey").Value) 'IIf(IsNull(RSSalesDetail.Fields("ISDUTY_FORGONE").Value), "N", RSSalesDetail.Fields("ISDUTY_FORGONE").Value)	
            mString = "Total amount of Duty Debited : " & IIf(mDutyForgone = "Y", "Duty forgone : ", "")
            ''06-06-2009	
            '                pAmount = IIf(IsNull(RSSalesDetail.Fields("TOTEDAMOUNT").Value), 0, RSSalesDetail.Fields("TOTEDAMOUNT").Value)	
            '                pDutyAmount = IIf(IsNull(RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value), 0, RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value)	
            '                pDutyAmount = pDutyAmount - IIf(IsNull(RSSalesDetail.Fields("ITEMVALUE").Value), 0, RSSalesDetail.Fields("ITEMVALUE").Value)	
            '                pDutyAmount = pDutyAmount - IIf(IsNull(RSSalesDetail.Fields("TOT_EXPORTEXP").Value), 0, RSSalesDetail.Fields("TOT_EXPORTEXP").Value)	
            pDutyAmount = GetInvoiceDutyAmount(RSSalesDetail.Fields("mKey").Value)
            pAmount = CStr(pDutyAmount)
            'UPGRADE_WARNING: Untranslated statement in PrintFooterDetail_PP. Please check source code.	
        End If

        If mExpPrintSeq = 2 Then
            ''06-06-2009	
            '                mAmountInword = MainClass.RupeesConversion(IIf(IsNull(RSSalesDetail.Fields("TOTEDAMOUNT").Value), 0, RSSalesDetail.Fields("TOTEDAMOUNT").Value))	
            '                pDutyAmount = IIf(IsNull(RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value), 0, RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value)	
            '                pDutyAmount = pDutyAmount - IIf(IsNull(RSSalesDetail.Fields("ITEMVALUE").Value), 0, RSSalesDetail.Fields("ITEMVALUE").Value)	
            '                pDutyAmount = pDutyAmount - IIf(IsNull(RSSalesDetail.Fields("TOT_EXPORTEXP").Value), 0, RSSalesDetail.Fields("TOT_EXPORTEXP").Value)	
            pDutyAmount = GetInvoiceDutyAmount(RSSalesDetail.Fields("mKey").Value)
            mAmountInword = MainClass.RupeesConversion(pDutyAmount)

            Print(1, TAB(3), "in words Rs. : " & mAmountInword)
        End If

        If mExpPrintSeq = 4 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("STFORMNAME").Value), "", RSSalesDetail.Fields("STFORMNAME").Value)
            '                pAmount = String(TabAmount - TabQty - Len(pAmount), " ") & pAmount          ''TabRate	
            Print(1, TAB(3), "Against Sales Tax Form No : " & pAmount)
        End If

        If mExpPrintSeq = 5 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("DESPATCHMODE").Value), "", RSSalesDetail.Fields("DESPATCHMODE").Value)
            Print(1, TAB(3), "Mode of Transport : " & pAmount)

            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("FREIGHTCHARGES").Value), "", RSSalesDetail.Fields("FREIGHTCHARGES").Value)
            Print(1, TAB(TabUnit - 20), "Freight : " & pAmount)
        End If

        If mExpPrintSeq = 6 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("VEHICLENO").Value), "", RSSalesDetail.Fields("VEHICLENO").Value)
            Print(1, TAB(3), "Vehicle No : " & pAmount)

            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("GRNO").Value), "", RSSalesDetail.Fields("GRNO").Value)
            pGRNo = IIf(IsDbNull(RSSalesDetail.Fields("GRNO").Value), "", RSSalesDetail.Fields("GRNO").Value)
            Print(1, TAB(TabUnit - 20), "G.R. No : " & pAmount)
        End If

        If mExpPrintSeq = 7 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("CARRIERS").Value), "", RSSalesDetail.Fields("CARRIERS").Value)
            PrintLine(1, TAB(3), "Carriers : " & pAmount & "")

            If Trim(pGRNo) = "" Then
                pAmount = ""
            Else
                pAmount = "G.R. Date : " & IIf(IsDbNull(RSSalesDetail.Fields("GRDATE").Value), "", RSSalesDetail.Fields("GRDATE").Value)
            End If
            Print(1, TAB(TabUnit - 20), pAmount)
        End If

        If mExpPrintSeq = 8 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("DOCSTHROUGH").Value), "", RSSalesDetail.Fields("DOCSTHROUGH").Value)
            Print(1, TAB(3), "Documents Direct to Party / Through : " & pAmount)

            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("ST_38_NO").Value), "", RSSalesDetail.Fields("ST_38_NO").Value)
            Print(1, TAB(TabUnit - 20), "ST-38 No. : " & pAmount)
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub


    Private Sub PrintJWFooterDetail_PP(ByRef mExpPrintSeq As Integer)
        On Error GoTo ERR1
        Dim pAmount As String
        Dim pDutyAmount As Double
        Dim mAmountInword As String
        Dim pGRNo As String
        Dim mString As String
        Dim mDutyForgone As String

        If mExpPrintSeq = 1 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("STFORMNAME").Value), "", RSSalesDetail.Fields("STFORMNAME").Value)
            Print(1, TAB(3), "Against Sales Tax Form No : " & pAmount)
        End If

        If mExpPrintSeq = 2 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("VEHICLENO").Value), "", RSSalesDetail.Fields("VEHICLENO").Value)
            Print(1, TAB(3), "Vehicle No : " & pAmount)
        End If

        If mExpPrintSeq = 3 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("ST_38_NO").Value), "", RSSalesDetail.Fields("ST_38_NO").Value)
            Print(1, TAB(3), "ST-38 No. : " & pAmount)
        End If

        If mExpPrintSeq = 4 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("NATURE").Value), "", RSSalesDetail.Fields("NATURE").Value)
            Print(1, TAB(3), "Nature of Process : " & pAmount)
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub
    Private Function GetInvoiceDutyAmount(ByRef mMKey As String) As Double
        Dim MainClass_Renamed As Object
        On Error GoTo ErrorPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTaxIdentification As String


        mTaxIdentification = "'ED','EDU','SHC','ADE','CED','HCC','AEC','AHC','BCD','BCE'"

        SqlStr = "SELECT  NVL(SUM(EXP.AMOUNT),0) AS EXPAMOUNT " & vbCrLf & " FROM FIN_INVOICE_EXP EXP, FIN_INTERFACE_MST IMST"

        SqlStr = SqlStr & vbCrLf & " WHERE IMST.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EXP.MKEY=  '" & mMKey & "'" & vbCrLf & " AND EXP.EXPCODE=IMST.CODE"

        SqlStr = SqlStr & vbCrLf & " AND IMST.IDENTIFICATION IN (" & mTaxIdentification & ")"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetInvoiceDutyAmount = IIf(IsDbNull(RsTemp.Fields("EXPAMOUNT").Value), 0, RsTemp.Fields("EXPAMOUNT").Value)
        Else
            GetInvoiceDutyAmount = 0
        End If

        Exit Function
ErrorPart:
        GetInvoiceDutyAmount = 0
    End Function


    Private Function CheckDutyForegone(ByRef mMKey As String) As String
        Dim MainClass_Renamed As Object
        On Error GoTo ErrorPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTaxIdentification As String

        mTaxIdentification = "'ED','EDU','SHC','ADE','CED','HCC','AEC','AHC','BCD','BCE'"

        SqlStr = "SELECT  DUTYFORGONE " & vbCrLf & " FROM FIN_INVOICE_EXP EXP, FIN_INTERFACE_MST IMST" & vbCrLf & " WHERE IMST.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EXP.MKEY=  '" & mMKey & "'" & vbCrLf & " AND EXP.EXPCODE=IMST.CODE"

        SqlStr = SqlStr & vbCrLf & " AND IMST.IDENTIFICATION IN (" & mTaxIdentification & ")"
        SqlStr = SqlStr & vbCrLf & " AND EXP.DUTYFORGONE='Y'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckDutyForegone = "Y"
        Else
            CheckDutyForegone = "N"
        End If

        Exit Function
ErrorPart:
        CheckDutyForegone = "N"
    End Function

    Private Sub PrintJWFooterDetail(ByRef mExpPrintSeq As Integer)
        On Error GoTo ERR1
        Dim pAmount As String
        Dim mAmountInword As String
        Dim pGRNo As String


        If mExpPrintSeq = 2 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("VEHICLENO").Value), 0, RSSalesDetail.Fields("VEHICLENO").Value)
            Print(1, TAB(15), pAmount)
        End If

        If mExpPrintSeq = 4 Then
            pAmount = IIf(IsDbNull(RSSalesDetail.Fields("ST_38_NO").Value), "", RSSalesDetail.Fields("ST_38_NO").Value)
            Print(1, TAB(15), pAmount)
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub
    Private Sub PrintFooter(ByRef pAgtPermission As String)
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim mPerSTR As String
        Dim mTotST As Double
        Dim mIdentification As String
        Dim mExciseAmt As Double
        Dim mMSCAmt As Double
        Dim mAmountInword As String
        Dim pAmount As String
        Dim pPerAmount As String
        Dim mGRNo As String
        Dim mGRDate As String

        Dim xItemValue As Double
        Dim xOtherValue As Double
        Dim mPermissionNo As String

        PrintLine_Renamed = PrintLine_Renamed + 1
        '    If PrintLine >= 50 Then	
        '        Print #1, Tab(0); "" + Chr(12)	
        '        Call PrintHeader	
        '    End If	

        '    Print #1, Tab(TabAmount - 3); MainClass.AdjNum(GrossAmountTotal)	
        '    PrintLine = PrintLine + 1	
        '	
        '    If PrintLine >= 50 Then	
        '        Print #1, Tab(0); "" + Chr(12)	
        '        Call PrintHeader	
        '    End If	
        RSSalesDetail.MoveFirst()
        '    Print #1, Tab(TabAmount); " "	
        '    PrintLine = PrintLine + 1	

        Print(1, TAB(0), Chr(15))
        Print(1, TAB(TabIPart), "PAYABLE") 'IIf(IsNull(RSSalesDetail.Fields("EXCISEDEBITTYPE").Value), "", RSSalesDetail.Fields("EXCISEDEBITTYPE").Value);	
        Print(1, TAB(TabUnit - 5), VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("INVOICE_DATE").Value), "", RSSalesDetail.Fields("INVOICE_DATE").Value), "DD/MM/YYYY"))

        '    pAmount = String(TabRate - TabQty - Len(Format(RSSalesDetail.Fields("TOTQTY").Value, "0.000")), " ") & Format(Trim(RSSalesDetail.Fields("TOTQTY").Value), "0.000")	
        '    Print #1, Tab(TabQty); pAmount;	
        '    PrintLine = PrintLine + 1	


        xItemValue = RSSalesDetail.Fields("ITEMVALUE").Value

        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("ITEMVALUE").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("ITEMVALUE").Value), "0.00")
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabAmount), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        'UPGRADE_WARNING: Untranslated statement in PrintFooter. Please check source code.	

        pAmount = New String(" ", TabAmount - TabRate - Len(VB6.Format(RSSalesDetail.Fields("EDPERCENT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("EDPERCENT").Value), "0.00")
        Print(1, TAB(TabRate), pAmount)

        xItemValue = xItemValue + RSSalesDetail.Fields("TOTEDAMOUNT").Value
        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("TOTEDAMOUNT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTEDAMOUNT").Value), "0.00")
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        xItemValue = xItemValue + RSSalesDetail.Fields("TOTMSCAMOUNT").Value
        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("TOTMSCAMOUNT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTMSCAMOUNT").Value), "0.00")
        mMSCAmt = CDbl(pAmount)
        If Val(CStr(mMSCAmt)) <> 0 Then
            Print(1, TAB(TabUnit), "MATERIAL SUPPLIED BY CLIENT :")
            PrintLine(1, TAB(TabAmount), mMSCAmt)
            PrintLine_Renamed = PrintLine_Renamed + 1
        Else
            Print(1, TAB(0), " ")
        End If

        pPerAmount = New String(" ", TabAmount - TabRate - Len(VB6.Format(RSSalesDetail.Fields("TOTEDUPERCENT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTEDUPERCENT").Value), "0.00")
        xItemValue = xItemValue + RSSalesDetail.Fields("TOTEDUAMOUNT").Value
        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("TOTEDUAMOUNT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTEDUAMOUNT").Value), "0.00")
        If Val(pAmount) <> 0 Then
            If RsCompany.Fields("COMPANY_CODE").Value <> 1 Then
                Print(1, TAB(TabUnit), "EDUCATION CESS TAX :")
            End If
            Print(1, TAB(TabRate), pPerAmount)
            PrintLine(1, TAB(TabAmount), pAmount)
        Else
            PrintLine(1, TAB(0), " ")
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        '03-03-2007	
        pPerAmount = New String(" ", TabAmount - TabRate - Len(VB6.Format(RSSalesDetail.Fields("TOTSHECPERCENT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTSHECPERCENT").Value), "0.00")
        xItemValue = xItemValue + IIf(IsDbNull(RSSalesDetail.Fields("TOTSHECAMOUNT").Value), 0, RSSalesDetail.Fields("TOTSHECAMOUNT").Value)
        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("TOTSHECAMOUNT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTSHECAMOUNT").Value), "0.00")
        If Val(pAmount) <> 0 Then
            If RsCompany.Fields("COMPANY_CODE").Value <> 1 Then
                Print(1, TAB(TabUnit), "SEC HIGHER EDU CESS:")
            End If
            Print(1, TAB(TabRate), pPerAmount)
            PrintLine(1, TAB(TabAmount), pAmount)
        Else
            PrintLine(1, TAB(0), " ")
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1


        mAmountInword = MainClass.RupeesConversion(IIf(IsDbNull(RSSalesDetail.Fields("TOTEDAMOUNT").Value), 0, RSSalesDetail.Fields("TOTEDAMOUNT").Value))

        Print(1, TAB(11), mAmountInword)
        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value), "0.00")
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        If Val(CStr(mMSCAmt)) = 0 Then
            PrintLine(1, TAB(TabAmount), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        pAmount = New String(" ", TabAmount - TabRate - Len(RSSalesDetail.Fields("STFORMNAME").Value)) & IIf(IsDbNull(RSSalesDetail.Fields("STFORMNAME").Value), "", RSSalesDetail.Fields("STFORMNAME").Value)
        Print(1, TAB(TabIPart), pAmount)

        pAmount = New String(" ", TabAmount - TabRate - Len(VB6.Format(RSSalesDetail.Fields("STPERCENT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("STPERCENT").Value), "0.00")
        Print(1, TAB(TabRate), pAmount)

        xItemValue = xItemValue + RSSalesDetail.Fields("TOTSTAMT").Value
        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("TOTSTAMT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTSTAMT").Value), "0.00")
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(TabAmount); " "	
        '    PrintLine = PrintLine + 1	

        xItemValue = xItemValue + RSSalesDetail.Fields("TOTSURCHARGEAMT").Value
        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("TOTSURCHARGEAMT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTSURCHARGEAMT").Value), "0.00")
        If Val(pAmount) <> 0 Then
            Print(1, TAB(TabUnit + 14), "SURCHARGE @5%:")
            PrintLine(1, TAB(TabAmount), pAmount)
        Else
            PrintLine(1, TAB(TabAmount), " ")
        End If

        PrintLine_Renamed = PrintLine_Renamed + 1

        ''    xOtherValue = RSSalesDetail.Fields("NETVALUE").Value - xItemValue	
        ''    pAmount = String(TabLastCol - TabAmount - Len(Format(xOtherValue, "0.00")), " ") & Format(Trim(xOtherValue), "0.00")	
        ''    Print #1, Tab(TabAmount); pAmount	
        ''    PrintLine = PrintLine + 1	

        Print(1, TAB(24), IIf(IsDbNull(RSSalesDetail.Fields("DESPATCHMODE").Value), "", RSSalesDetail.Fields("DESPATCHMODE").Value))

        xItemValue = xItemValue + RSSalesDetail.Fields("TOTFREIGHT").Value
        Print(1, TAB(TabUnit), IIf(IsDbNull(RSSalesDetail.Fields("FREIGHTCHARGES").Value), "", RSSalesDetail.Fields("FREIGHTCHARGES").Value))
        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("TOTFREIGHT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("TOTFREIGHT").Value), "0.00")
        '    Print #1, Tab(TabAmount); pAmount	
        If Val(pAmount) <> 0 Then
            PrintLine(1, TAB(TabAmount), pAmount)
        Else
            PrintLine(1, TAB(TabAmount), " ")
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(24), IIf(IsDbNull(RSSalesDetail.Fields("VEHICLENO").Value), "", RSSalesDetail.Fields("VEHICLENO").Value))
        mGRNo = IIf(IsDbNull(RSSalesDetail.Fields("GRNO").Value), "", RSSalesDetail.Fields("GRNO").Value)
        PrintLine(1, TAB(TabUnit), mGRNo)
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(24), IIf(IsDbNull(RSSalesDetail.Fields("CARRIERS").Value), "", RSSalesDetail.Fields("CARRIERS").Value))
        ''Print #1, Tab(TabUnit); IIf(mGRNo = "", "", IIf(IsNull(RSSalesDetail.Fields("GRDATE").Value), "", RSSalesDetail.Fields("GRDATE").Value));	
        mGRDate = IIf(mGRNo = "", "", IIf(IsDbNull(RSSalesDetail.Fields("GRDATE").Value), "", RSSalesDetail.Fields("GRDATE").Value))

        '    pAmount = String(TabLastCol - TabAmount - Len(Format(RSSalesDetail.Fields("TOTCHARGES").Value, "0.00")), " ") & Format(Trim(RSSalesDetail.Fields("TOTCHARGES").Value), "0.00")	
        '    Print #1, Tab(TabAmount); pAmount	
        xItemValue = xItemValue + RSSalesDetail.Fields("TCSAMOUNT").Value
        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("TCSAMOUNT").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("TCSAMOUNT").Value), "0.00")
        If Val(pAmount) <> 0 Then
            Print(1, TAB(TabUnit), mGRDate & " TAX COLLECTION AT SOURCE:")
            PrintLine(1, TAB(TabAmount), pAmount)
        Else
            Print(1, TAB(TabUnit), mGRDate)
            PrintLine(1, TAB(TabAmount), " ")
        End If

        '    Print #1, Tab(TabAmount); pAmount	
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(TabIDesc), IIf(IsDbNull(RSSalesDetail.Fields("DOCSTHROUGH").Value), "", RSSalesDetail.Fields("DOCSTHROUGH").Value))
        Print(1, TAB(TabUnit), IIf(IsDbNull(RSSalesDetail.Fields("ST_38_NO").Value), "", RSSalesDetail.Fields("ST_38_NO").Value))
        ''18.06.2003	
        xOtherValue = RSSalesDetail.Fields("NETVALUE").Value - xItemValue
        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(xOtherValue, "0.00"))) & VB6.Format(Trim(CStr(xOtherValue)), "0.00")

        If Val(pAmount) <> 0 Then
            PrintLine(1, TAB(TabAmount), pAmount)
        Else
            PrintLine(1, TAB(TabAmount), " ")
        End If
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabAmount), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        mAmountInword = MainClass.RupeesConversion(IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), 0, RSSalesDetail.Fields("NETVALUE").Value))

        Print(1, TAB(6), mAmountInword)
        pAmount = New String(" ", TabLastCol - TabAmount - Len(VB6.Format(RSSalesDetail.Fields("NETVALUE").Value, "0.00"))) & VB6.Format(Trim(RSSalesDetail.Fields("NETVALUE").Value), "0.00")
        PrintLine(1, TAB(TabAmount), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabIDesc), IIf(IsDbNull(RSSalesDetail.Fields("REMARKS").Value), "", RSSalesDetail.Fields("REMARKS").Value) & IIf(IsDbNull(RSSalesDetail.Fields("DNCNNO").Value), "", " Our Debit Note No.: " & RSSalesDetail.Fields("DNCNNO").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        If pAgtPermission = "Y" Then

            If CDate(RSSalesDetail.Fields("INVOICE_DATE").Value) >= CDate("18/03/2016") Then
                mPermissionNo = "F.NO.C.NO.CE/Tech/Div-III/GGN-I/Jobwork/23/14-15/549 Dt.17/03/2016"
            Else
                mPermissionNo = "F.NO.C.NO.CE/Tech/Div-III/GGN-I/Jobwork/23/14-15/569 Dt.26/03/2015"
            End If

            PrintLine(1, TAB(0), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, TAB(TabIDesc), Chr(15) & "Removed from the premises of job-worker : M/s EUROTHERM HEMA RADIATORS INDIA LTD. UNIT-II" & Chr(18))

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, TAB(TabIDesc), Chr(15) & "PLOT NO. 5 & 14, SECTOR-6, HSIDC INDUSTRIAL COMPLEX, BAWAL (REWARI) HARYANA. Central Excise Regn. No. : AABCE3677REM002" & Chr(18))

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, TAB(TabIDesc), Chr(15) & "as permitted by the of Assistant/Deputy Commissioner,Central Excise,Division under" & Chr(18))

            PrintLine_Renamed = PrintLine_Renamed + 1
            PrintLine(1, TAB(TabIDesc), Chr(15) & mPermissionNo & ",Rule4(6) of the Cenvat Credit Rules,2004" & Chr(18))
        End If

        mRemarks = "Regd Office : " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        PrintLine(1, TAB(TabSNo), mRemarks)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mRemarks = "              " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value) Or RsCompany.Fields("REGD_ADDR2").Value = "", "", RsCompany.Fields("REGD_ADDR2").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value) Or RsCompany.Fields("REGD_CITY").Value = "", "", RsCompany.Fields("REGD_CITY").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_STATE").Value) Or RsCompany.Fields("REGD_STATE").Value = "", "", " - " & RsCompany.Fields("REGD_STATE").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_PHONE").Value) Or RsCompany.Fields("REGD_PHONE").Value = "", "", " Phone : " & RsCompany.Fields("REGD_PHONE").Value)
        PrintLine(1, TAB(TabSNo), mRemarks)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), "" & Chr(12))
        '    PrintLine = PrintLine + 1	
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub

    Private Sub PrintPPFooter()
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim mPerSTR As String
        Dim mTotST As Double
        Dim mIdentification As String
        Dim mExciseAmt As Double
        Dim mAmountInword As String
        Dim mLocal As String
        Dim mLocalTaxStr As String
        Dim mLen As Integer
        Dim mSurcharge As Double
        Dim mAmount As Double

        Dim mKey As String
        Dim SqlStr As String = ""
        Dim RsBillExp As ADODB.Recordset = Nothing
        Dim mExpPrintSeq As Integer

        Dim mExpName As String
        Dim mExpPer As String
        Dim pExpAmount As String
        Dim pAmount As String

        Dim xItemValue As Double
        Dim mExpType As String
        Dim mPrintTaxableAmt As Boolean
        Dim mPrintEDAmt As Boolean
        Dim mString As String
        Dim mString1 As String
        Dim mString2 As String
        Dim mWithInCountry As String
        Dim mISCT3 As String
        Dim mCurrency As String
        Dim mAbatementPer As Double
        Dim mMRPAmount As Double

        mPrintTaxableAmt = False
        mPrintEDAmt = False

        PrintLine_Renamed = PrintLine_Renamed + 1
        RSSalesDetail.MoveFirst()

        mKey = RSSalesDetail.Fields("MKEY").Value

        PrintLine(1, TAB(3), New String("-", 85) & Chr(15))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mExpName = "ASSESSABLE VALUE :"
        mExpName = New String(" ", TabAmount - TabQty - Len(mExpName)) & mExpName
        pExpAmount = VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("ITEMVALUE").Value), 0, RSSalesDetail.Fields("ITEMVALUE").Value), "0.00")
        pExpAmount = New String(" ", TabLastCol - TabAmount - Len(pExpAmount)) & pExpAmount

        Print(1, TAB(TabQty), mExpName)
        PrintLine(1, TAB(TabAmount), pExpAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mExpName = ""
        pExpAmount = ""

        SqlStr = " SELECT EXP.* , INT.*" & vbCrLf & " FROM FIN_INVOICE_EXP EXP, FIN_INTERFACE_MST INT" & vbCrLf & " WHERE " & vbCrLf & " INT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EXP.EXPCODE=INT.CODE" & vbCrLf & " AND EXP.MKEY='" & mKey & "' AND DUTYFORGONE='N'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY INT.PRINTSEQUENCE"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBillExp, ADODB.LockTypeEnum.adLockReadOnly)
        Do While RsBillExp.EOF = False
            Call PrintJWFooterDetail_PP(mExpPrintSeq)

            mExpName = UCase(IIf(IsDbNull(RsBillExp.Fields("Name").Value), "", RsBillExp.Fields("Name").Value))
            mExpName = Left(mExpName, 21)
            mExpPer = VB6.Format(IIf(IsDbNull(RsBillExp.Fields("EXPPERCENT").Value), 0, RsBillExp.Fields("EXPPERCENT").Value), "0.00")
            mExpPer = New String(" ", 5 - Len(mExpPer)) & mExpPer
            pExpAmount = VB6.Format(IIf(IsDbNull(RsBillExp.Fields("Amount").Value), 0, RsBillExp.Fields("Amount").Value), "0.00")
            pExpAmount = New String(" ", TabLastCol - TabAmount - Len(pExpAmount)) & pExpAmount

            mExpName = mExpName & IIf(CDbl(mExpPer) = 0, "", " @%" & mExpPer) & ":"
            mExpName = New String(" ", TabAmount - TabQty - Len(mExpName)) & mExpName

            mExpType = IIf(IsDbNull(RsBillExp.Fields("Identification").Value), "", RsBillExp.Fields("Identification").Value)

            If mExpType = "ED" And mPrintEDAmt = False Then
                mString = "-"
                mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
                PrintLine(1, TAB(TabAmount), mString)
                PrintLine_Renamed = PrintLine_Renamed + 1
                mExpPrintSeq = mExpPrintSeq + 1
                Call PrintJWFooterDetail_PP(mExpPrintSeq)

                '            mString = "TOTAL FOB VALUE" & String(7, " ") & ":"	
                '            mString = String(TabAmount - TabQty - Len(mString), " ") & mString	
                '            xItemValue = xItemValue + IIf(IsNull(RSSalesDetail!TOT_EXPORTEXP), 0, RSSalesDetail!TOT_EXPORTEXP)	
                '            pAmount = Format(Trim(xItemValue), "0.00")	
                '            pAmount = String(TabLastCol - TabAmount - Len(pAmount), " ") & pAmount	
                '	
                '            Print #1, Tab(TabQty); mString;	
                '            Print #1, Tab(TabAmount); pAmount	
                '            PrintLine = PrintLine + 1	
                '            mExpPrintSeq = mExpPrintSeq + 1	
                mPrintEDAmt = True
                '            If pExpAmount <> 0 Then	
                '                Call PrintJWFooterDetail_PP(mExpPrintSeq)	
                '            End If	
                '            mPrintEDAmt = True	
            End If

            If mExpType <> "EE" And mPrintEDAmt = False Then
                mString = "-"
                mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
                PrintLine(1, TAB(TabAmount), mString)
                PrintLine_Renamed = PrintLine_Renamed + 1
                mExpPrintSeq = mExpPrintSeq + 1
                Call PrintJWFooterDetail_PP(mExpPrintSeq)

                '            mString = "TOTAL FOB VALUE" & String(7, " ") & ":"	
                '            mString = String(TabAmount - TabQty - Len(mString), " ") & mString	
                '            xItemValue = xItemValue + IIf(IsNull(RSSalesDetail!TOT_EXPORTEXP), 0, RSSalesDetail!TOT_EXPORTEXP)	
                '            pAmount = Format(Trim(xItemValue), "0.00")	
                '            pAmount = String(TabLastCol - TabAmount - Len(pAmount), " ") & pAmount	
                '	
                '            Print #1, Tab(TabQty); mString;	
                '            Print #1, Tab(TabAmount); pAmount	
                '            PrintLine = PrintLine + 1	
                '            mExpPrintSeq = mExpPrintSeq + 1	
                '            mPrintEDAmt = True	
                '            If pExpAmount <> 0 Then	
                '                Call PrintJWFooterDetail_PP(mExpPrintSeq)	
                '            End If	
                mPrintEDAmt = True
            End If

            If mExpType = "ST" And mPrintTaxableAmt = False Then
                mString = "-"
                mString = New String("-", TabLastCol - TabAmount - Len(mString)) & mString
                PrintLine(1, TAB(TabAmount), mString)
                PrintLine_Renamed = PrintLine_Renamed + 1
                mExpPrintSeq = mExpPrintSeq + 1
                Call PrintJWFooterDetail_PP(mExpPrintSeq)

                mString = "TAXABLE AMOUNT" & New String(" ", 8) & ":"
                mString = New String(" ", TabAmount - TabQty - Len(mString)) & mString
                pAmount = VB6.Format(Trim(RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value), "0.00")
                pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount

                Print(1, TAB(TabQty), mString)
                PrintLine(1, TAB(TabAmount), pAmount)
                PrintLine_Renamed = PrintLine_Renamed + 1
                mExpPrintSeq = mExpPrintSeq + 1
                mPrintTaxableAmt = True
                If CDbl(pExpAmount) <> 0 Then
                    Call PrintJWFooterDetail_PP(mExpPrintSeq)
                End If
                mPrintTaxableAmt = True
            End If

            If CDbl(pExpAmount) <> 0 Then
                Print(1, TAB(TabQty), mExpName)
                PrintLine(1, TAB(TabAmount), pExpAmount)
                PrintLine_Renamed = PrintLine_Renamed + 1
                mExpPrintSeq = mExpPrintSeq + 1
            End If
            RsBillExp.MoveNext()
        Loop

        Do While mExpPrintSeq <= 4
            Call PrintJWFooterDetail_PP(mExpPrintSeq)

            PrintLine(1, TAB(TabAmount), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
            mExpPrintSeq = mExpPrintSeq + 1

        Loop

        mString = "-"
        mString = Chr(18) & New String("-", 85) & mString
        PrintLine(1, TAB(3), mString & Chr(15))
        PrintLine_Renamed = PrintLine_Renamed + 1

        '********* Line 14	
        mAmountInword = MainClass.RupeesConversion(IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), 0, RSSalesDetail.Fields("NETVALUE").Value))
        Print(1, TAB(3), "Rs. : " & mAmountInword)

        pAmount = VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), "0", RSSalesDetail.Fields("NETVALUE").Value), "0.00")
        pAmount = New String(" ", TabLastCol - TabAmount - Len(pAmount)) & pAmount
        mString = "BILL AMOUNT" & New String(" ", 7) & ":"
        '    mString = String(TabAmount - TabQty - Len(mString), " ") & mString	
        '    Print #1, Tab(TabQty); mString;	
        Print(1, TAB(TabQty + TabAmount - TabQty - Len(mString)), mString)
        PrintLine(1, TAB(TabAmount), pAmount & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(TabAmount1); " "	
        '    PrintLine = PrintLine + 1	
        '	
        '    mAmountInword = MainClass.RupeesConversion(IIf(IsNull(RSSalesDetail.Fields("NETVALUE").Value), 0, RSSalesDetail.Fields("NETVALUE").Value))	
        '	
        '    Print #1, Tab(3); "RUPEES : " & Chr(15) & mAmountInword & Chr(18);	
        '    PrintLine = PrintLine + 1	
        '    Print #1, Tab(3); Chr(18);	

        PrintLine(1, TAB(3), New String("-", 85))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabIDesc), Chr(15) & IIf(IsDbNull(RSSalesDetail.Fields("REMARKS").Value), "", RSSalesDetail.Fields("REMARKS").Value) & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mLen = 81 - Len(RsCompany.Fields("Company_Name").Value)

        PrintLine(1, TAB(mLen), "for " & RsCompany.Fields("Company_Name").Value)
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabAmount), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(TabAmount); " "	
        '    PrintLine = PrintLine + 1	

        '    Print #1, Tab(3); "PREPARED BY ";	
        '    Print #1, Tab(35); "CHECKED BY ";	
        PrintLine(1, TAB(65), "AUTHORISED SIGNATORY")
        PrintLine_Renamed = PrintLine_Renamed + 1

        mRemarks = "Regd Office : " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value) Or RsCompany.Fields("REGD_ADDR2").Value = "", "", RsCompany.Fields("REGD_ADDR2").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value) Or RsCompany.Fields("REGD_CITY").Value = "", "", RsCompany.Fields("REGD_CITY").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_STATE").Value) Or RsCompany.Fields("REGD_STATE").Value = "", "", " - " & RsCompany.Fields("REGD_STATE").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_PHONE").Value) Or RsCompany.Fields("REGD_PHONE").Value = "", "", " Phone : " & RsCompany.Fields("REGD_PHONE").Value)
        PrintLine(1, TAB(TabSNo), Chr(15) & mRemarks & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " " & Chr(12))
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume	
    End Sub

    Private Sub PrintJWFooter(Optional ByRef mJWSTRemarks As String = "")
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim mKey As String
        Dim SqlStr As String = ""
        Dim RsBillExp As ADODB.Recordset = Nothing
        Dim mExpPrintSeq As Integer

        Dim mExpName As String
        Dim mExpPer As String
        Dim pExpAmount As String
        Dim pAmount As String
        Dim mAmountInword As String
        Dim xItemValue As Double
        Dim mExpType As String
        Dim mPrintTaxableAmt As Boolean
        Dim mString As String
        Dim TabAmount2 As Integer
        Dim mSubTotal As Double

        mPrintTaxableAmt = False

        RSSalesDetail.MoveFirst()
        PrintLine_Renamed = PrintLine_Renamed + 1
        TabAmount2 = TabAmount1 - 7
        mSubTotal = 0
        mKey = RSSalesDetail.Fields("MKEY").Value

        SqlStr = " SELECT EXP.* , INT.*" & vbCrLf & " FROM FIN_INVOICE_EXP EXP, FIN_INTERFACE_MST INT" & vbCrLf & " WHERE " & vbCrLf & " INT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EXP.EXPCODE=INT.CODE" & vbCrLf & " AND EXP.MKEY='" & mKey & "'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY INT.PRINTSEQUENCE"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBillExp, ADODB.LockTypeEnum.adLockReadOnly)

        '********* Line 1	
        pAmount = IIf(IsDbNull(RSSalesDetail.Fields("STFORMNAME").Value), "", RSSalesDetail.Fields("STFORMNAME").Value)
        Print(1, TAB(15), pAmount)

        xItemValue = IIf(IsDbNull(RSSalesDetail.Fields("ITEMVALUE").Value), 0, RSSalesDetail.Fields("ITEMVALUE").Value)
        mSubTotal = xItemValue
        pAmount = Space(10 - Len(Trim(VB6.Format(xItemValue, "0.00")))) & VB6.Format(Trim(CStr(xItemValue)), "0.00")
        PrintLine(1, TAB(TabAmount2), pAmount)
        PrintLine_Renamed = PrintLine_Renamed + 1


        mExpPrintSeq = 1
        If RsBillExp.EOF = False Then
            Do While Not RsBillExp.EOF

                Call PrintJWFooterDetail(mExpPrintSeq)

                mExpName = UCase(IIf(IsDbNull(RsBillExp.Fields("Name").Value), "", RsBillExp.Fields("Name").Value))
                mExpName = Left(mExpName, 21)
                mExpPer = VB6.Format(IIf(IsDbNull(RsBillExp.Fields("EXPPERCENT").Value), 0, RsBillExp.Fields("EXPPERCENT").Value), "0.00")
                mExpPer = New String(" ", 5 - Len(mExpPer)) & mExpPer
                pExpAmount = VB6.Format(IIf(IsDbNull(RsBillExp.Fields("Amount").Value), 0, RsBillExp.Fields("Amount").Value), "0.00")

                pExpAmount = New String(" ", 10 - Len(pExpAmount)) & pExpAmount

                mExpName = mExpName & " @%" & mExpPer & ":"
                mExpName = New String(" ", 30 - Len(mExpName)) & mExpName

                mExpType = IIf(IsDbNull(RsBillExp.Fields("Identification").Value), "", RsBillExp.Fields("Identification").Value)

                If mExpType = "ST" And mPrintTaxableAmt = False Then
                    mString = "-"
                    mString = New String("-", 10 - Len(mString)) & mString
                    PrintLine(1, TAB(TabAmount2), mString)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                    Call PrintJWFooterDetail(mExpPrintSeq)

                    If mJWSTRemarks <> "" Then
                        mString = "SUB TOTAL"
                        mString = mString & New String(" ", 8) & ":"
                        mString = New String(" ", 30 - Len(mString)) & mString
                        pAmount = VB6.Format(mSubTotal, "0.00")
                        pAmount = New String(" ", 10 - Len(pAmount)) & pAmount

                        Print(1, TAB(33), mString)
                        PrintLine(1, TAB(TabAmount2), pAmount)
                        PrintLine_Renamed = PrintLine_Renamed + 1
                        mExpPrintSeq = mExpPrintSeq + 1
                    End If

                    mString = "TAXABLE AMOUNT"
                    mString = mString & New String(" ", 8) & ":"
                    mString = New String(" ", 30 - Len(mString)) & mString
                    pAmount = VB6.Format(Trim(RSSalesDetail.Fields("TOTTAXABLEAMOUNT").Value), "0.00")
                    pAmount = New String(" ", 10 - Len(pAmount)) & pAmount

                    Print(1, TAB(33), mString)
                    PrintLine(1, TAB(TabAmount2), pAmount)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1

                    If mJWSTRemarks <> "" Then
                        mString = mJWSTRemarks
                        mString = New String(" ", 25 - Len(mString)) & mString
                        PrintLine(1, TAB(33), mString)
                        PrintLine_Renamed = PrintLine_Renamed + 1
                        mExpPrintSeq = mExpPrintSeq + 1
                    End If

                    mPrintTaxableAmt = True
                    If CDbl(pExpAmount) <> 0 Then
                        Call PrintJWFooterDetail(mExpPrintSeq)
                    End If

                    mPrintTaxableAmt = True
                Else
                    mSubTotal = mSubTotal + CDbl(VB6.Format(IIf(IsDbNull(RsBillExp.Fields("Amount").Value), 0, RsBillExp.Fields("Amount").Value), "0.00"))
                End If

                If CDbl(pExpAmount) <> 0 Then
                    Print(1, TAB(33), mExpName)
                    PrintLine(1, TAB(TabAmount2), pExpAmount)
                    PrintLine_Renamed = PrintLine_Renamed + 1
                    mExpPrintSeq = mExpPrintSeq + 1
                End If
                RsBillExp.MoveNext()
            Loop
        End If
        Do While mExpPrintSeq <= 6
            Call PrintJWFooterDetail(mExpPrintSeq)

            PrintLine(1, TAB(TabAmount), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
            mExpPrintSeq = mExpPrintSeq + 1
        Loop

        mString = "-"
        mString = New String("-", 10 - Len(mString)) & mString
        PrintLine(1, TAB(TabAmount2), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        pAmount = IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), "", VB6.Format(RSSalesDetail.Fields("NETVALUE").Value, "0.00"))
        pAmount = New String(" ", 10 - Len(pAmount)) & pAmount
        mString = "BILL AMOUNT" & New String(" ", 7) & ":"
        mString = New String(" ", 28 - Len(mString)) & mString
        Print(1, TAB(33), mString)
        PrintLine(1, TAB(TabAmount2 - 2), Chr(18) & Chr(15) & Chr(14) & pAmount & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabAmount2), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        mAmountInword = MainClass.RupeesConversion(IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), 0, RSSalesDetail.Fields("NETVALUE").Value))
        PrintLine(1, TAB(15), Chr(15) & mAmountInword & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabAmount2), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        pAmount = IIf(IsDbNull(RSSalesDetail.Fields("REMARKS").Value), "", RSSalesDetail.Fields("REMARKS").Value)
        PrintLine(1, TAB(TabIDesc), Chr(15) & pAmount & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabAmount), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(TabAmount); " "	
        '    PrintLine = PrintLine + 1	
        '	
        mString = IIf(IsDbNull(RSSalesDetail.Fields("NATURE").Value), "", RSSalesDetail.Fields("NATURE").Value)
        If mString <> "" Then
            mString = "Nature of Process : " & mString
        End If

        PrintLine(1, TAB(TabIDesc), mString)
        PrintLine_Renamed = PrintLine_Renamed + 1

        mRemarks = "Regd Office : " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        PrintLine(1, TAB(TabSNo), Chr(15) & mRemarks & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mRemarks = "              " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value) Or RsCompany.Fields("REGD_ADDR2").Value = "", "", RsCompany.Fields("REGD_ADDR2").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value) Or RsCompany.Fields("REGD_CITY").Value = "", "", RsCompany.Fields("REGD_CITY").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_STATE").Value) Or RsCompany.Fields("REGD_STATE").Value = "", "", " - " & RsCompany.Fields("REGD_STATE").Value)
        mRemarks = mRemarks & IIf(IsDbNull(RsCompany.Fields("REGD_PHONE").Value) Or RsCompany.Fields("REGD_PHONE").Value = "", "", " Phone : " & RsCompany.Fields("REGD_PHONE").Value)
        PrintLine(1, TAB(TabSNo), Chr(15) & mRemarks & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        '    Print #1, Tab(65); ""	
        '    PrintLine = PrintLine + 1	

        PrintLine(1, TAB(0), " " & Chr(12))


        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume	
    End Sub

    Private Sub PrintJWFooterOld()
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim mPerSTR As String
        Dim mTotST As Double
        Dim mIdentification As String
        Dim mExciseAmt As Double
        Dim mAmountInword As String
        Dim mLocal As String = ""
        Dim mLocalTaxStr As String
        Dim mLen As Integer
        Dim mSurcharge As Double
        Dim mAmount As Double
        Dim mString As String
        Dim TabAmount2 As Integer
        Dim mTotAmount As Double

        PrintLine_Renamed = PrintLine_Renamed + 1
        RSSalesDetail.MoveFirst()
        '    Do While PrintLine <> 42	
        '        Print #1, Tab(0); " "	
        '        PrintLine = PrintLine + 1	
        '    Loop	

        TabAmount2 = TabAmount1 - 7
        PrintLine(1, TAB(15), IIf(IsDbNull(RSSalesDetail.Fields("STFORMNAME").Value), "", RSSalesDetail.Fields("STFORMNAME").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mAmount = IIf(IsDbNull(RSSalesDetail.Fields("ITEMVALUE").Value), 0, RSSalesDetail.Fields("ITEMVALUE").Value)
        mTotAmount = mTotAmount + mAmount
        PrintLine(1, TAB(TabAmount2), Space(10 - Len(Trim(VB6.Format(mAmount, "0.00")))) & VB6.Format(Trim(CStr(mAmount)), "0.00"))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(15), IIf(IsDbNull(RSSalesDetail.Fields("VEHICLENO").Value), "", RSSalesDetail.Fields("VEHICLENO").Value))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mAmount = IIf(IsDbNull(RSSalesDetail.Fields("TOTSERVICEAMOUNT").Value), 0, RSSalesDetail.Fields("TOTSERVICEAMOUNT").Value)
        mTotAmount = mTotAmount + mAmount
        Print(1, TAB(40), "Service Tax :" & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("TOTSERVICEPERCENT").Value), 0, RSSalesDetail.Fields("TOTSERVICEPERCENT").Value), "0.00") & "%")
        PrintLine(1, TAB(TabAmount2), Space(10 - Len(Trim(VB6.Format(mAmount, "0.00")))) & VB6.Format(Trim(CStr(mAmount)), "0.00"))
        PrintLine_Renamed = PrintLine_Renamed + 1

        mAmount = IIf(IsDbNull(RSSalesDetail.Fields("TOTEDUAMOUNT").Value), 0, RSSalesDetail.Fields("TOTEDUAMOUNT").Value)
        mTotAmount = mTotAmount + mAmount
        Print(1, TAB(40), "Edu. Cess Tax :" & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("TOTEDUPERCENT").Value), 0, RSSalesDetail.Fields("TOTEDUPERCENT").Value), "0.00") & "%")
        PrintLine(1, TAB(TabAmount2), Space(10 - Len(Trim(VB6.Format(mAmount, "0.00")))) & VB6.Format(Trim(CStr(mAmount)), "0.00"))
        PrintLine_Renamed = PrintLine_Renamed + 1

        Print(1, TAB(15), IIf(IsDbNull(RSSalesDetail.Fields("ST_38_NO").Value), "", RSSalesDetail.Fields("ST_38_NO").Value))

        '***********	
        'UPGRADE_WARNING: Untranslated statement in PrintJWFooterOld. Please check source code.	

        If mLocal = "L" Then
            If RsCompany.Fields("COMPANY_CODE").Value = 3 Then
                mLocalTaxStr = "TNGST TAX @"
            Else
                mLocalTaxStr = "VAT TAX @"
            End If
        Else
            mLocalTaxStr = "C.S.T. @"
        End If

        Print(1, TAB(48), mLocalTaxStr & VB6.Format(IIf(IsDbNull(RSSalesDetail.Fields("STPERCENT").Value), 0, RSSalesDetail.Fields("STPERCENT").Value), "0.00") & "%")

        mAmount = IIf(IsDbNull(RSSalesDetail.Fields("TOTSTAMT").Value), 0, RSSalesDetail.Fields("TOTSTAMT").Value)
        mTotAmount = mTotAmount + mAmount
        'UPGRADE_WARNING: Untranslated statement in PrintJWFooterOld. Please check source code.	
        PrintLine_Renamed = PrintLine_Renamed + 1

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Then
            mSurcharge = IIf(IsDbNull(RSSalesDetail.Fields("TOTSURCHARGEAMT").Value), 0, RSSalesDetail.Fields("TOTSURCHARGEAMT").Value)

            If mSurcharge <> 0 Then
                Print(1, TAB(40), "SURCHARGE @5%:")
                'UPGRADE_WARNING: Untranslated statement in PrintJWFooterOld. Please check source code.	
                PrintLine_Renamed = PrintLine_Renamed + 1

                PrintLine(1, TAB(TabAmount2), " ")
                PrintLine_Renamed = PrintLine_Renamed + 1
            End If
            mTotAmount = mTotAmount + mSurcharge
        Else
            PrintLine(1, TAB(TabAmount2), " ")
            PrintLine_Renamed = PrintLine_Renamed + 1
        End If

        mAmount = IIf(IsDbNull(RSSalesDetail.Fields("TOTFREIGHT").Value), 0, RSSalesDetail.Fields("TOTFREIGHT").Value)
        mTotAmount = mTotAmount + mAmount
        'UPGRADE_WARNING: Untranslated statement in PrintJWFooterOld. Please check source code.	
        PrintLine_Renamed = PrintLine_Renamed + 1

        mAmount = IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), 0, RSSalesDetail.Fields("NETVALUE").Value)
        mAmount = mAmount - mTotAmount

        'UPGRADE_WARNING: Untranslated statement in PrintJWFooterOld. Please check source code.	
        PrintLine_Renamed = PrintLine_Renamed + 1

        mAmount = IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), 0, RSSalesDetail.Fields("NETVALUE").Value)
        mString = Space(10 - Len(Trim(VB6.Format(mAmount, "0.00")))) & VB6.Format(Trim(CStr(mAmount)), "0.00")
        PrintLine(1, TAB(TabAmount2 - 2), Chr(18) & Chr(15) & Chr(14) & mString & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabAmount2), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        mAmountInword = MainClass.RupeesConversion(IIf(IsDbNull(RSSalesDetail.Fields("NETVALUE").Value), 0, RSSalesDetail.Fields("NETVALUE").Value))

        PrintLine(1, TAB(15), Chr(15) & mAmountInword & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabAmount2), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabIDesc), Chr(15) & IIf(IsDbNull(RSSalesDetail.Fields("REMARKS").Value), "", RSSalesDetail.Fields("REMARKS").Value) & Chr(18))
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabAmount), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(TabAmount), " ")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(65), "")
        PrintLine_Renamed = PrintLine_Renamed + 1

        PrintLine(1, TAB(0), " " & Chr(12))
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume	
    End Sub
    Private Function MakeSQL(ByRef pMKey As String, ByRef pPrintedFormat As String, ByRef pDespRefType As String, ByRef pItemGroup As String) As String
        On Error GoTo ERR1

        ''SELECT CLAUSE...	


        MakeSQL = " SELECT IH.MKEY, IH.TRNTYPE, IH.BILLNOPREFIX, " & vbCrLf & " IH.BILLNOSEQ,IH.AUTO_KEY_INVOICE, IH.BILLNOSUFFIX," & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, IH.INV_PREP_DATE," & vbCrLf & " IH.INV_PREP_TIME, IH.AUTO_KEY_DESP, IH.DCDATE," & vbCrLf & " IH.CUST_PO_NO, IH.CUST_PO_DATE, IH.AMEND_NO, IH.OUR_AUTO_KEY_SO," & vbCrLf & " IH.AMEND_DATE, IH.AMEND_WEF_FROM, IH.REMOVAL_DATE," & vbCrLf & " IH.REMOVAL_TIME, IH.SUPP_CUST_CODE, IH.ST_38_NO," & vbCrLf & " IH.AUTHSIGN, IH.AUTHDATE, IH.GRNO," & vbCrLf & " IH.GRDATE, IH.DESPATCHMODE, IH.DOCSTHROUGH," & vbCrLf & " IH.VEHICLENO, IH.CARRIERS, IH.FREIGHTCHARGES," & vbCrLf & " IH.TARIFFHEADING, IH.EXEMPT_NOTIF_NO, IH.EXCISEDEBITTYPE," & vbCrLf & " IH.EXCISEDEBITNO, IH.EXCISEDEBITDATE, IH.BOOKCODE," & vbCrLf & " IH.BOOKTYPE, IH.BOOKSUBTYPE, IH.REMARKS," & vbCrLf & " IH.ITEMDESC, IH.ITEMVALUE, IH.TOTSTAMT," & vbCrLf & " IH.TOTCHARGES, IH.TOTEDAMOUNT, IH.NETVALUE," & vbCrLf & " IH.STFORMNAME, IH.ISREGDNO, IH.FOC, IH.TOTQTY, " & vbCrLf & " IH.PRINTED, IH.CANCELLED, IH.NARRATION, IH.STFORMNO, TOTSERVICEPERCENT, TOTSERVICEAMOUNT, " & vbCrLf & " EDPERCENT,STPERCENT,TOTFREIGHT,TOTTAXABLEAMOUNT, " & vbCrLf & " TOTMSCAMOUNT,TCSAMOUNT,DNCNNo, " & vbCrLf & " TOTSURCHARGEAMT, TOTEDUPERCENT, TOTEDUAMOUNT, NATURE, TOTEXCHANGEVALUE, ADV_LICENSE , DESP_LOCATION, IH.TOTSHECPERCENT, IH.TOTSHECAMOUNT," & vbCrLf
        MakeSQL = MakeSQL & " ID.ITEM_CODE, " & vbCrLf & " ID.CUSTOMER_PART_NO, ID.ITEM_DESC, " & vbCrLf & " ID.ITEM_UOM, ID.ITEM_RATE, ITEM_MRP, IH.ISDUTY_FORGONE, "

        If pDespRefType = "U" Then
            MakeSQL = MakeSQL & " MAX(ID.SUBROWNO) AS SUBROWNO, SUM(ID.ITEM_QTY) AS ITEM_QTY, SUM(ID.ITEM_QTY) * ID.ITEM_RATE AS ITEM_AMT, IH.REF_DESP_TYPE,"
        Else
            If (RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 And pDespRefType = "S") Or pItemGroup = "Y" Then
                MakeSQL = MakeSQL & " MAX(ID.SUBROWNO) AS SUBROWNO, SUM (ID.ITEM_QTY) AS ITEM_QTY, SUM(ID.ITEM_QTY) * ID.ITEM_RATE AS ITEM_AMT, IH.REF_DESP_TYPE,'' AS MRR_REF_NO,"
            Else
                MakeSQL = MakeSQL & " ID.SUBROWNO, ID.ITEM_QTY,ID.ITEM_AMT, IH.REF_DESP_TYPE,DSP.MRR_REF_NO,"
            End If
        End If

        MakeSQL = MakeSQL & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_POLICYNO, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, CMST.COUNTRY, CMST.WITHIN_COUNTRY, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE,CMST.ACCOUNT_CODE,CMST.VENDOR_CODE, TOT_EXPORTEXP,"

        MakeSQL = MakeSQL & vbCrLf & " SHIPPING_NO, SHIPPING_DATE, ARE1_NO, ARE1_DATE, "
        MakeSQL = MakeSQL & vbCrLf & " AUTO_KEY_EXPINV, EXPBILLNO, EXPINV_DATE, EXCHANGE_RATE,CURRENCYNAME,"

        MakeSQL = MakeSQL & vbCrLf & " AGTCT3, CT_NO, ARE_NO, CT3_DATE, TOTMRPVALUE, TAX_ON_MRP, ABATEMENT_PER,"

        '    If pPrintedFormat = "N" Then	
        If pDespRefType = "U" Then
            MakeSQL = MakeSQL & vbCrLf & " '' AS REF_NO, '' AS REF_DATE, MRR_REF_NO"
        Else
            If (RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 And pDespRefType = "S") Or pItemGroup = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " '' AS REF_NO, '' AS REF_DATE"
            Else
                MakeSQL = MakeSQL & vbCrLf & " DSP.REF_NO,DSP.REF_DATE"
            End If
        End If
        '    End If	

        ''FROM CLAUSE...	
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST"

        '    If pPrintedFormat = "N" Then	
        MakeSQL = MakeSQL & vbCrLf & " ,DSP_DESPATCH_DET DSP"
        '    End If	

        ''WHERE CLAUSE...	
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND IH.MKEY='" & pMKey & "'" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        '    If pPrintedFormat = "N" Then	
        MakeSQL = MakeSQL & vbCrLf & " AND IH.AUTO_KEY_DESP=DSP.AUTO_KEY_DESP" & vbCrLf & " AND ID.ITEM_CODE=DSP.ITEM_CODE AND ID.SUBROWNO=DSP.SERIAL_NO"
        '    End If	

        ''GROUP BY CLAUSE...	
        If pDespRefType = "U" Then
            MakeSQL = MakeSQL & vbCrLf & " GROUP BY " & vbCrLf & " IH.MKEY, IH.TRNTYPE, IH.BILLNOPREFIX, " & vbCrLf & " IH.BILLNOSEQ,IH.AUTO_KEY_INVOICE, IH.BILLNOSUFFIX," & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, IH.INV_PREP_DATE," & vbCrLf & " IH.INV_PREP_TIME, IH.AUTO_KEY_DESP, IH.DCDATE," & vbCrLf & " IH.CUST_PO_NO, IH.CUST_PO_DATE, IH.AMEND_NO, IH.OUR_AUTO_KEY_SO, " & vbCrLf & " IH.AMEND_DATE, IH.AMEND_WEF_FROM, IH.REMOVAL_DATE," & vbCrLf & " IH.REMOVAL_TIME, IH.SUPP_CUST_CODE, IH.ST_38_NO," & vbCrLf & " IH.AUTHSIGN, IH.AUTHDATE, IH.GRNO," & vbCrLf & " IH.GRDATE, IH.DESPATCHMODE, IH.DOCSTHROUGH," & vbCrLf & " IH.VEHICLENO, IH.CARRIERS, IH.FREIGHTCHARGES," & vbCrLf & " IH.TARIFFHEADING, IH.EXEMPT_NOTIF_NO, IH.EXCISEDEBITTYPE," & vbCrLf & " IH.EXCISEDEBITNO, IH.EXCISEDEBITDATE, IH.BOOKCODE," & vbCrLf & " IH.BOOKTYPE, IH.BOOKSUBTYPE, IH.REMARKS," & vbCrLf & " IH.ITEMDESC, IH.ITEMVALUE, IH.TOTSTAMT," & vbCrLf & " IH.TOTCHARGES, IH.TOTEDAMOUNT, IH.NETVALUE," & vbCrLf & " IH.STFORMNAME, IH.ISREGDNO, IH.FOC, IH.TOTQTY, " & vbCrLf & " IH.PRINTED, IH.CANCELLED, IH.NARRATION, IH.STFORMNO, TOTSERVICEPERCENT, TOTSERVICEAMOUNT, " & vbCrLf & " EDPERCENT,STPERCENT,TOTFREIGHT,TOTTAXABLEAMOUNT, " & vbCrLf & " TOTMSCAMOUNT,TCSAMOUNT,DNCNNo, " & vbCrLf & " TOTSURCHARGEAMT, TOTEDUPERCENT, TOTEDUAMOUNT, NATURE, TOTEXCHANGEVALUE, ADV_LICENSE , DESP_LOCATION,  IH.TOTSHECPERCENT, IH.TOTSHECAMOUNT," & vbCrLf & " TOTMRPVALUE, TAX_ON_MRP, ABATEMENT_PER,  IH.ISDUTY_FORGONE,"

            MakeSQL = MakeSQL & " ID.ITEM_CODE, " & vbCrLf & " ID.CUSTOMER_PART_NO, ID.ITEM_DESC, " & vbCrLf & " ID.ITEM_UOM, ID.ITEM_RATE, ITEM_MRP, "

            MakeSQL = MakeSQL & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_POLICYNO, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, CMST.COUNTRY, CMST.WITHIN_COUNTRY, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE,CMST.ACCOUNT_CODE,CMST.VENDOR_CODE, TOT_EXPORTEXP,"

            MakeSQL = MakeSQL & vbCrLf & " SHIPPING_NO, SHIPPING_DATE, ARE1_NO, ARE1_DATE, "
            MakeSQL = MakeSQL & vbCrLf & " AUTO_KEY_EXPINV, EXPBILLNO, EXPINV_DATE, EXCHANGE_RATE,CURRENCYNAME,IH.REF_DESP_TYPE,"

            MakeSQL = MakeSQL & vbCrLf & " AGTCT3, CT_NO, ARE_NO, CT3_DATE, TOTMRPVALUE, TAX_ON_MRP, ABATEMENT_PER,MRR_REF_NO"
        End If

        If pDespRefType <> "U" Then
            If (RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 And pDespRefType = "S") Or pItemGroup = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY " & vbCrLf & " IH.MKEY, IH.TRNTYPE, IH.BILLNOPREFIX, " & vbCrLf & " IH.BILLNOSEQ,IH.AUTO_KEY_INVOICE, IH.BILLNOSUFFIX," & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, IH.INV_PREP_DATE," & vbCrLf & " IH.INV_PREP_TIME, IH.AUTO_KEY_DESP, IH.DCDATE," & vbCrLf & " IH.CUST_PO_NO, IH.CUST_PO_DATE, IH.AMEND_NO, IH.OUR_AUTO_KEY_SO, " & vbCrLf & " IH.AMEND_DATE, IH.AMEND_WEF_FROM, IH.REMOVAL_DATE," & vbCrLf & " IH.REMOVAL_TIME, IH.SUPP_CUST_CODE, IH.ST_38_NO," & vbCrLf & " IH.AUTHSIGN, IH.AUTHDATE, IH.GRNO," & vbCrLf & " IH.GRDATE, IH.DESPATCHMODE, IH.DOCSTHROUGH," & vbCrLf & " IH.VEHICLENO, IH.CARRIERS, IH.FREIGHTCHARGES," & vbCrLf & " IH.TARIFFHEADING, IH.EXEMPT_NOTIF_NO, IH.EXCISEDEBITTYPE," & vbCrLf & " IH.EXCISEDEBITNO, IH.EXCISEDEBITDATE, IH.BOOKCODE," & vbCrLf & " IH.BOOKTYPE, IH.BOOKSUBTYPE, IH.REMARKS," & vbCrLf & " IH.ITEMDESC, IH.ITEMVALUE, IH.TOTSTAMT," & vbCrLf & " IH.TOTCHARGES, IH.TOTEDAMOUNT, IH.NETVALUE," & vbCrLf & " IH.STFORMNAME, IH.ISREGDNO, IH.FOC, IH.TOTQTY, " & vbCrLf & " IH.PRINTED, IH.CANCELLED, IH.NARRATION, IH.STFORMNO, TOTSERVICEPERCENT, TOTSERVICEAMOUNT, " & vbCrLf & " EDPERCENT,STPERCENT,TOTFREIGHT,TOTTAXABLEAMOUNT, " & vbCrLf & " TOTMSCAMOUNT,TCSAMOUNT,DNCNNo, " & vbCrLf & " TOTSURCHARGEAMT, TOTEDUPERCENT, TOTEDUAMOUNT, NATURE, TOTEXCHANGEVALUE, ADV_LICENSE , DESP_LOCATION,  IH.TOTSHECPERCENT, IH.TOTSHECAMOUNT," & vbCrLf & " TOTMRPVALUE, TAX_ON_MRP, ABATEMENT_PER,  IH.ISDUTY_FORGONE,"

                MakeSQL = MakeSQL & " ID.ITEM_CODE, " & vbCrLf & " ID.CUSTOMER_PART_NO, ID.ITEM_DESC, " & vbCrLf & " ID.ITEM_UOM, ID.ITEM_RATE, ITEM_MRP, "

                MakeSQL = MakeSQL & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_POLICYNO, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, CMST.COUNTRY, CMST.WITHIN_COUNTRY, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE,CMST.ACCOUNT_CODE,CMST.VENDOR_CODE, TOT_EXPORTEXP,"

                MakeSQL = MakeSQL & vbCrLf & " SHIPPING_NO, SHIPPING_DATE, ARE1_NO, ARE1_DATE, "
                MakeSQL = MakeSQL & vbCrLf & " AUTO_KEY_EXPINV, EXPBILLNO, EXPINV_DATE, EXCHANGE_RATE,CURRENCYNAME,IH.REF_DESP_TYPE,"

                MakeSQL = MakeSQL & vbCrLf & " AGTCT3, CT_NO, ARE_NO, CT3_DATE, TOTMRPVALUE, TAX_ON_MRP, ABATEMENT_PER"
            End If
        End If
        ''ORDER BY CLAUSE...	

        If pDespRefType = "U" Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY ID.ITEM_CODE"
        Else
            If (RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 And pDespRefType = "S") Or pItemGroup = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY ID.ITEM_CODE"
            Else
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY ID.SUBROWNO"
            End If
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLSupp(ByRef pMKey As String, ByRef pPrintedFormat As String) As String
        On Error GoTo ERR1

        ''SELECT CLAUSE...	

        MakeSQLSupp = " SELECT IH.MKEY, IH.TRNTYPE, IH.BILLNOPREFIX, " & vbCrLf & " IH.BILLNOSEQ,IH.AUTO_KEY_INVOICE, IH.BILLNOSUFFIX," & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, IH.INV_PREP_DATE," & vbCrLf & " IH.INV_PREP_TIME, IH.AUTO_KEY_DESP, IH.DCDATE," & vbCrLf & " IH.CUST_PO_NO, IH.CUST_PO_DATE, IH.AMEND_NO," & vbCrLf & " IH.AMEND_DATE, IH.AMEND_WEF_FROM, IH.REMOVAL_DATE," & vbCrLf & " IH.REMOVAL_TIME, IH.SUPP_CUST_CODE, IH.ST_38_NO," & vbCrLf & " IH.AUTHSIGN, IH.AUTHDATE, IH.GRNO," & vbCrLf & " IH.GRDATE, IH.DESPATCHMODE, IH.DOCSTHROUGH," & vbCrLf & " IH.VEHICLENO, IH.CARRIERS, IH.FREIGHTCHARGES," & vbCrLf & " IH.TARIFFHEADING, IH.EXEMPT_NOTIF_NO, IH.EXCISEDEBITTYPE," & vbCrLf & " IH.EXCISEDEBITNO, IH.EXCISEDEBITDATE, IH.BOOKCODE," & vbCrLf & " IH.BOOKTYPE, IH.BOOKSUBTYPE, IH.REMARKS," & vbCrLf & " IH.ITEMDESC, IH.ITEMVALUE, IH.TOTSTAMT," & vbCrLf & " IH.TOTCHARGES, IH.TOTEDAMOUNT, IH.NETVALUE," & vbCrLf & " IH.STFORMNAME, IH.ISREGDNO, IH.FOC, IH.SUPPITEMTOT," & vbCrLf & " IH.PRINTED, IH.CANCELLED, IH.NARRATION, IH.STFORMNO, TOTSERVICEPERCENT, TOTSERVICEAMOUNT,TOTSHECPERCENT,TOTSHECAMOUNT, " & vbCrLf & " EDPERCENT,STPERCENT,TOTFREIGHT,TOTTAXABLEAMOUNT, TOTMSCAMOUNT,TCSAMOUNT,DNCNNo, TOTSURCHARGEAMT, TOTEDUPERCENT, TOTEDUAMOUNT, " & vbCrLf
        MakeSQLSupp = MakeSQLSupp & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_POLICYNO," & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, CMST.COUNTRY, CMST.WITHIN_COUNTRY, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE,CMST.ACCOUNT_CODE,CMST.VENDOR_CODE, TOT_EXPORTEXP,"

        MakeSQLSupp = MakeSQLSupp & vbCrLf & " SHIPPING_NO, SHIPPING_DATE, ARE1_NO, ARE1_DATE, "
        MakeSQLSupp = MakeSQLSupp & vbCrLf & " AUTO_KEY_EXPINV, EXPBILLNO, EXPINV_DATE, EXCHANGE_RATE,CURRENCYNAME,"

        MakeSQLSupp = MakeSQLSupp & vbCrLf & " AGTCT3, CT_NO, ARE_NO, CT3_DATE, TOTMRPVALUE, TAX_ON_MRP, ABATEMENT_PER"

        ''FROM CLAUSE...	
        MakeSQLSupp = MakeSQLSupp & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST"

        ''WHERE CLAUSE...	
        MakeSQLSupp = MakeSQLSupp & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND IH.MKEY='" & pMKey & "'" & vbCrLf & " AND " & vbCrLf & " IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"


        ''ORDER CLAUSE...	

        '    MakeSQLSupp = MakeSQLSupp & vbCrLf & "ORDER BY ID.SUBROWNO"	

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function


    Private Function MakePaintSQL(ByRef pMKey As String) As String
        On Error GoTo ERR1

        ''SELECT CLAUSE...	

        MakePaintSQL = " SELECT ID2.CUSTOMER_PART_NO AS PaintPartNo, ID2.ITEM_SHORT_DESC AS PAINTITEMDESC, " & vbCrLf & " SUM(IH.ITEM_QTY) AS ITEM_QTY, IH.PARTY_F4NO "

        ''FROM CLAUSE...	
        MakePaintSQL = MakePaintSQL & vbCrLf & " FROM DSP_PAINT57F4_TRN IH, INV_ITEM_MST ID1, INV_ITEM_MST ID2"


        ''WHERE CLAUSE...	
        MakePaintSQL = MakePaintSQL & vbCrLf & " WHERE " & vbCrLf & " IH.SUB_ITEM_CODE=ID1.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=ID1.COMPANY_CODE" & vbCrLf & " AND IH.ITEM_CODE=ID2.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=ID2.COMPANY_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY='" & pMKey & "'" & vbCrLf & " AND IH.BOOKTYPE='D' AND IH.ISSCRAP='N'"

        ''GROUP BY CLAUSE...	

        MakePaintSQL = MakePaintSQL & vbCrLf & "GROUP BY ID2.CUSTOMER_PART_NO, ID2.ITEM_SHORT_DESC, IH.PARTY_F4NO "

        ''ORDER CLAUSE...	

        MakePaintSQL = MakePaintSQL & vbCrLf & "ORDER BY ID2.CUSTOMER_PART_NO" ''IH.SUBROWNO"	

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function


    Private Function MakeJWDtlSQL(ByRef pMKey As String) As String
        On Error GoTo ERR1
        Dim mDCNo As Double

        ''SELECT CLAUSE...	


        '    MakeJWDtlSQL = " SELECT IH.MKEY, IH.BILL_NO, IH.BILL_DATE, IH.BILL_QTY, " & vbCrLf _	
        ''            & " IH.ITEM_CODE,IH.ITEM_QTY, " & vbCrLf _	
        ''            & " IH.SUB_ITEM_CODE, IH.PARTY_F4NO, " & vbCrLf _	
        ''            & " ID1.ITEM_SHORT_DESC AS JOBITEM, ID1.CUSTOMER_PART_NO AS JOBPartNo," & vbCrLf _	
        ''            & " ID2.ITEM_SHORT_DESC AS PAINTITEMDESC, ID2.CUSTOMER_PART_NO AS PaintPartNo"	


        'UPGRADE_WARNING: Untranslated statement in MakeJWDtlSQL. Please check source code.	

        MakeJWDtlSQL = " SELECT IH.MKEY,IH.SUB_ITEM_CODE,ID2.ITEM_CODE AS ITEM_CODE, ID2.ITEM_SHORT_DESC AS PAINTITEMDESC, " & vbCrLf & " SUM(IH.ITEM_QTY) AS ITEM_QTY, IH.PARTY_F4NO "

        ''FROM CLAUSE...	
        MakeJWDtlSQL = MakeJWDtlSQL & vbCrLf & " FROM DSP_PAINT57F4_TRN IH, INV_ITEM_MST ID1, INV_ITEM_MST ID2"


        ''WHERE CLAUSE...	
        MakeJWDtlSQL = MakeJWDtlSQL & vbCrLf & " WHERE " & vbCrLf & " IH.SUB_ITEM_CODE=ID1.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=ID1.COMPANY_CODE" & vbCrLf & " AND IH.ITEM_CODE=ID2.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=ID2.COMPANY_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY='" & mDCNo & "'" & vbCrLf & " AND IH.BOOKTYPE='D' AND IH.ISSCRAP='N'"

        ''GROUP BY CLAUSE...	
        MakeJWDtlSQL = MakeJWDtlSQL & vbCrLf & "GROUP BY IH.PARTY_F4NO, ID2.ITEM_CODE, ID2.ITEM_SHORT_DESC,IH.SUB_ITEM_CODE,IH.MKEY "

        ''ORDER CLAUSE...	

        MakeJWDtlSQL = MakeJWDtlSQL & vbCrLf & "ORDER BY IH.PARTY_F4NO, ID2.ITEM_CODE" ''IH.SUBROWNO"	

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Public Sub Print2DBarcode(ByRef Data As String, ByRef InvNo As String, ByRef MSComm2 As Object)
        Dim mStr As String
        'Dim mLen As Integer
        'Dim mSeprater As String

        If Data <> "" Then
            '        Open "lpt1:" For Output As #1	
            '        Print #1, "q600"	
            '        Print #1, "s2"	
            '        Print #1, "O"	
            '        Print #1, "JF"	
            '        Print #1, "WN"	
            '        Print #1, "D10"	
            '        Print #1, "ZB"	
            '        Print #1, "Q304,25"	
            '        Print #1, "N"	
            '        Print #1, "A44,12,0,1,2,2,N," & Chr(34) & InvNo & Chr(34)	
            '        'Print #1, "b44,48,P,600,304,s2,f0,x2,y6,r90,l5,t0,o0," & Chr(34) & Data & Chr(34)	
            '        Print #1, "b54,48,P,600,304,s2,f0,x3,y9,r90,l5,t0,o0," & Chr(34) & Data & Chr(34)	
            '        Print #1, "P1"	
            '        Close #1	

            '        If RsCompany!COMPANY_CODE = 1 Then	
            '            mLen = Len(Data)	
            '            mSeprater = "^"	
            '            mStr = mSeprater & "Q32,3"	
            '            mStr = mStr & vbCrLf & mSeprater & "W61"	
            '            mStr = mStr & vbCrLf & mSeprater & "H10"	
            '            mStr = mStr & vbCrLf & mSeprater & "P1"	
            '            mStr = mStr & vbCrLf & mSeprater & "S4"	
            '            mStr = mStr & vbCrLf & mSeprater & "AT"	
            '            mStr = mStr & vbCrLf & mSeprater & "C1"	
            '            mStr = mStr & vbCrLf & mSeprater & "R0"	
            '            mStr = mStr & vbCrLf & "~Q+0"	
            '            mStr = mStr & vbCrLf & mSeprater & "O0"	
            '            mStr = mStr & vbCrLf & mSeprater & "D0"	
            '            mStr = mStr & vbCrLf & mSeprater & "E12"	
            '            mStr = mStr & vbCrLf & "~R200"	
            '            mStr = mStr & vbCrLf & mSeprater & "L"	
            '            mStr = mStr & vbCrLf & "Dy2-Me-dd"	
            '            mStr = mStr & vbCrLf & "Th:m:s"	
            '            mStr = mStr & vbCrLf & "P83,84,2,5,5,2,3," & mLen	
            '            mStr = mStr & vbCrLf & Data	
            '            mStr = mStr & vbCrLf & "AD,85,35,1,1,0,0," & InvNo	
            '            mStr = mStr & vbCrLf & "E"	
            '            mStr = mStr & vbCrLf	

            '        Else	
            mStr = "q600"
            mStr = mStr & vbCrLf & "s2"
            mStr = mStr & vbCrLf & "O"
            mStr = mStr & vbCrLf & "JF"
            mStr = mStr & vbCrLf & "WN"
            mStr = mStr & vbCrLf & pBARCODEDarkNess '"D7"      ' after "D" is darkness start from 1 - 15	
            mStr = mStr & vbCrLf & "ZB"
            mStr = mStr & vbCrLf & "Q254,25"
            mStr = mStr & vbCrLf & "N"
            mStr = mStr & vbCrLf & "A80,15,0,1,2,2,N," & Chr(34) & InvNo & Chr(34)
            mStr = mStr & vbCrLf & "b75,48,P,600,304,s2,f0,x2,y6,r90,l5,t0,o0," & Chr(34) & Data & Chr(34)
            'mStr = mStr & vbCrLf & "b54,48,P,600,304,s2,f0,x3,y9,r90,l5,t0,o0," & Chr(34) & Data & Chr(34)	
            mStr = mStr & vbCrLf & "P1" & vbCrLf
            '        End If	

            If PrintBySerialPort(mStr, MSComm2) = False Then
                MsgBox("Please Check Printer")
                Exit Sub
            End If

        Else
            MsgBox("Nothing To Print.")
            Exit Sub
        End If
    End Sub
    Public Sub Print2DMRPBarcode(ByRef Data As String, ByRef pPartNo As String, ByRef pPartName As String, ByRef pPktDate As String, ByRef pMRP As String, ByRef pIsMPRPrint As String, ByRef mPQty As Integer, ByRef MSComm2 As Object)
        'Public Sub Print2DSPDBarcode(Data As String, pPartNo As String, pPartName As String, pBinQty As String, pSNo As String, pItemCode As String, pVendorName As String, pMRP As String, MSComm2 As MSComm)	
        On Error GoTo ErrPart
        Dim mStr As String
        Dim mLen As Integer
        Dim mSeprater As String
        Dim xPartName As String
        Dim mFP As Boolean
        Dim mPartName1 As String = ""
        Dim mPartName2 As String = ""
        Dim mPartName3 As String = ""
        Dim pPartNameNew As String = ""

        pPartNameNew = pPartName
        pPartNameNew = GetMultiLine(pPartNameNew, 1, 30, 1)

        If InStr(1, pPartNameNew, vbCrLf) > 1 Then
            mPartName1 = Left(pPartNameNew, InStr(1, pPartNameNew, vbCrLf))
            pPartNameNew = Mid(pPartNameNew, InStr(1, Trim(pPartNameNew), vbCrLf) + 2)
            If InStr(1, pPartNameNew, vbCrLf) > 1 Then
                mPartName2 = Replace(pPartNameNew, vbCrLf, " ")
            Else
                mPartName2 = pPartNameNew
            End If
        Else
            mPartName1 = pPartNameNew
        End If

        ' <xpml><page quantity='0' pitch='50.0 mm'></xpml>^AT	
        '^O0	
        '^D0	
        '^C1	
        '<xpml></page></xpml><xpml><page quantity='2' pitch='50.0 mm'></xpml>^P2	
        '^Q50.0,3.0	
        '^W70	
        '^L	
        'Dy2 -Me - dd	
        'Th: m: s	
        'AC,30,115,1,1,0,0,Part Number : 10210015	
        'AC , 30, 150, 1, 1, 0, 0, Desc:	
        'AC,110,150,1,1,0,0,AIR SPRING BELLOW ASSY.	
        'AC , 110, 185, 1, 1, 0, 0, SA520325_10210015	
        'AC,30,220,1,1,0,0,Qty (in Nos) : 1   PKD - 19/09/2017	
        'AC,30,255,1,1,0,0,MRP (Rs.): 3700.00 (Inclusive of All Taxes)	
        'e	
        '<xpml></page></xpml><xpml><end/></xpml>	
        '	

        mStr = "<xpml><page quantity='0' pitch='50.0 mm'></xpml>^AT"
        mStr = mStr & vbCrLf & "^O0"
        mStr = mStr & vbCrLf & "^D0"
        mStr = mStr & vbCrLf & "^C1"
        mStr = mStr & vbCrLf & "<xpml></page></xpml><xpml><page quantity='4' pitch='50.0 mm'></xpml>^P" & Val(CStr(mPQty))
        mStr = mStr & vbCrLf & "^Q50.0,3.0"
        mStr = mStr & vbCrLf & "^W70"
        mStr = mStr & vbCrLf & "^L"
        mStr = mStr & vbCrLf & "Dy2-me-dd"
        mStr = mStr & vbCrLf & "Th:m:s"
        mStr = mStr & vbCrLf & "AC,30,115,1,1,0,0,Part Number : " & pPartNo
        mStr = mStr & vbCrLf & "AC,30,150,1,1,0,0,Desc : "
        mStr = mStr & vbCrLf & "AC,110,150,1,1,0,0," & mPartName1
        mStr = mStr & vbCrLf & "AC,110,185,1,1,0,0," & mPartName2
        mStr = mStr & vbCrLf & "AC,30,220,1,1,0,0,Qty (in Nos) : 1   PKD - " & pPktDate
        If pIsMPRPrint = "Y" Then
            mStr = mStr & vbCrLf & "AC,30,255,1,1,0,0,MRP (Rs.): " & pMRP & " (Inclusive of All Taxes)"
        End If
        If Data <> "" Then
            mStr = mStr & vbCrLf & "P332,292,1,4,3,2,3,15"
            mStr = mStr & vbCrLf & Data
        End If

        mStr = mStr & vbCrLf & "E"
        mStr = mStr & vbCrLf & "<xpml></page></xpml><xpml><end/></xpml>"
        If CreateOutPutFile(mStr, "Inv.Prn") = False Then GoTo ErrPart
        mFP = Shell(mLocalPath & "\BarCode.bat", AppWinStyle.NormalFocus)



        '        mStr = "^Q50,4"	
        '        mStr = mStr & vbCrLf & "^W70"	
        '        mStr = mStr & vbCrLf & "^H10"	
        '        mStr = mStr & vbCrLf & "^P5"	
        '        mStr = mStr & vbCrLf & "^S4"	
        '        mStr = mStr & vbCrLf & "^AD"	
        '        mStr = mStr & vbCrLf & "^C1"	
        '        mStr = mStr & vbCrLf & "^R0"	
        '        mStr = mStr & vbCrLf & "~Q+0"	
        '        mStr = mStr & vbCrLf & "^O0"	
        '        mStr = mStr & vbCrLf & "^D09"	
        '        mStr = mStr & vbCrLf & "^E25"	
        '        mStr = mStr & vbCrLf & "~R200"	
        '        mStr = mStr & vbCrLf & "^L"	
        '        mStr = mStr & vbCrLf & "Dy2-me-dd"	
        '        mStr = mStr & vbCrLf & "Th:m:s"	
        '        mStr = mStr & vbCrLf & "AC,30,115,1,1,0,0,Part Number : " & pPartNo	
        '        mStr = mStr & vbCrLf & "AC,30,150,1,1,0,0,Desc : "	
        '        mStr = mStr & vbCrLf & "AC,110,150,1,1,0,0," & mPartName1	
        '        mStr = mStr & vbCrLf & "AC,110,185,1,1,0,0," & mPartName2	
        '        mStr = mStr & vbCrLf & "AC,30,220,1,1,0,0,Qty (in Nos) : 1   PKD - " & pPktDate	
        '        If pIsMPRPrint = "Y" Then	
        '            mStr = mStr & vbCrLf & "AC,30,255,1,1,0,0,MRP (Rs.): " & pMRP & " (Inclusive of All Taxes)"	
        '        End If	
        '        mStr = mStr & vbCrLf & "P332,292,1,4,3,2,3,15"	
        '        If Data <> "" Then	
        '            mStr = mStr & vbCrLf & Data	
        '        End If	
        '	
        '        mStr = mStr & vbCrLf & "E"	
        '        mStr = mStr & vbCrLf & ""	
        '        If CreateOutPutFile(mStr, "Inv.Prn") = False Then GoTo ErrPart	
        '        mFP = Shell(mLocalPath & "\BarCode.bat", vbNormalFocus)	


        '    If Data <> "" Then	
        '        mStr = "^Q50,4"	
        '        mStr = mStr & vbCrLf & "^W70"	
        '        mStr = mStr & vbCrLf & "^H10"	
        '        mStr = mStr & vbCrLf & "^P1"	
        '        mStr = mStr & vbCrLf & "^S4"	
        '        mStr = mStr & vbCrLf & "^AD"	
        '        mStr = mStr & vbCrLf & "^C1"	
        '        mStr = mStr & vbCrLf & "^R0"	
        '        mStr = mStr & vbCrLf & "~Q+0"	
        '        mStr = mStr & vbCrLf & "^O0"	
        '        mStr = mStr & vbCrLf & "^D09"	
        '        mStr = mStr & vbCrLf & "^E25"	
        '        mStr = mStr & vbCrLf & "~R200"	
        '        mStr = mStr & vbCrLf & "^L"	
        '        mStr = mStr & vbCrLf & "Dy2-me-dd"	
        '        mStr = mStr & vbCrLf & "Th:m:s"	
        '        mStr = mStr & vbCrLf & "AC,30,115,1,1,0,0,Part Number : " & pPartNo	
        '        mStr = mStr & vbCrLf & "AC,30,150,1,1,0,0,Desc : "	
        '        mStr = mStr & vbCrLf & "AC,110,150,1,1,0,0," & mPartName1	
        '        mStr = mStr & vbCrLf & "AC,110,185,1,1,0,0," & mPartName2	
        '        mStr = mStr & vbCrLf & "AC,30,220,1,1,0,0,Qty (in Nos) : 1   PKD - " & pPktDate	
        '        If pIsMPRPrint = "Y" Then	
        '            mStr = mStr & vbCrLf & "AC,30,255,1,1,0,0,MRP (Rs.): " & pMRP & " (Inclusive of All Taxes)"	
        '        End If	
        '        mStr = mStr & vbCrLf & "P332,292,1,4,3,2,3,15"	
        '        mStr = mStr & vbCrLf & Data	
        '        mStr = mStr & vbCrLf & "E"	
        '        mStr = mStr & vbCrLf & ""	
        '        If CreateOutPutFile(mStr, "Inv.Prn") = False Then GoTo ErrPart	
        '        mFP = Shell(mLocalPath & "\BarCode.bat", vbNormalFocus)	
        '     Else	
        '        mStr = "^Q50,4"	
        '        mStr = mStr & vbCrLf & "^W70"	
        '        mStr = mStr & vbCrLf & "^H10"	
        '        mStr = mStr & vbCrLf & "^P1"	
        '        mStr = mStr & vbCrLf & "^S4"	
        '        mStr = mStr & vbCrLf & "^AD"	
        '        mStr = mStr & vbCrLf & "^C1"	
        '        mStr = mStr & vbCrLf & "^R0"	
        '        mStr = mStr & vbCrLf & "~Q+0"	
        '        mStr = mStr & vbCrLf & "^O0"	
        '        mStr = mStr & vbCrLf & "^D09"	
        '        mStr = mStr & vbCrLf & "^E25"	
        '        mStr = mStr & vbCrLf & "~R200"	
        '        mStr = mStr & vbCrLf & "^L"	
        '        mStr = mStr & vbCrLf & "Dy2-me-dd"	
        '        mStr = mStr & vbCrLf & "Th:m:s"	
        '        mStr = mStr & vbCrLf & "AC,30,115,1,1,0,0,Part Number : " & pPartNo	
        '        mStr = mStr & vbCrLf & "AC,30,150,1,1,0,0,Desc : "	
        '        mStr = mStr & vbCrLf & "AC,110,150,1,1,0,0," & mPartName1	
        '        mStr = mStr & vbCrLf & "AC,110,185,1,1,0,0," & mPartName2	
        '        mStr = mStr & vbCrLf & "AC,30,220,1,1,0,0,Qty (in Nos) : 1   PKD - " & pPktDate	
        '        If pIsMPRPrint = "Y" Then	
        '            mStr = mStr & vbCrLf & "AC,30,255,1,1,0,0,MRP (Rs.): " & pMRP & " (Inclusive of All Taxes)"	
        '        End If	
        '        mStr = mStr & vbCrLf & "E"	
        '        mStr = mStr & vbCrLf & ""	
        '        If CreateOutPutFile(mStr, "Inv.Prn") = False Then GoTo ErrPart	
        '        mFP = Shell(mLocalPath & "\BarCode.bat", vbNormalFocus)	
        '    End If	


        Exit Sub
ErrPart:

    End Sub
    Public Sub Print2DSPDBarcode(ByRef Data As String, ByRef pPartNo As String, ByRef pPartName As String, ByRef pBinQty As String, ByRef pSNo As String, ByRef pItemCode As String, ByRef pVendorName As String, ByRef pMRP As String, ByRef MSComm2 As Object)
        Dim mStr As String
        'Dim mLen As Integer
        'Dim mSeprater As String

        ''Public Sub Print2DBarcode(ItemCode As String, ItemDesc As String, BoxQty As String, Slno As String, MRP As String, Barcode As String, MSComm2 As MSComm)	

        If Data <> "" Then
            mStr = ""
            mStr = mStr & "{D0520,0500,0500|}" & vbCrLf
            mStr = mStr & "{C|}" & vbCrLf
            mStr = mStr & "{PC000;0020,0100,1,2,I,00,B=" & pPartNo & "|}" & vbCrLf
            mStr = mStr & "{PC001;0020,0153,05,1,H,00,B=" & pPartName & "|}" & vbCrLf
            mStr = mStr & "{PC002;0019,0214,1,1,H,00,B=HB Box Qty:|}" & vbCrLf
            mStr = mStr & "{PC003;0019,0293,1,1,H,00,B=Sr. No.       :|}" & vbCrLf
            mStr = mStr & "{PC004;0220,0214,1,1,H,00,W=" & pBinQty & "|}" & vbCrLf
            mStr = mStr & "{PC005;0341,0214,1,1,H,00,B=N|}" & vbCrLf
            mStr = mStr & "{PC006;0220,0293,1,1,H,00,B=" & pSNo & "|}" & vbCrLf
            mStr = mStr & "{PC007;0019,0374,1,1,H,00,B=Unit MRP   :|}" & vbCrLf
            mStr = mStr & "{PC008;0019,0443,1,1,H,00,B=Item Code :|}" & vbCrLf
            mStr = mStr & "{PC009;0220,0374,1,1,H,00,B=Rs. " & pMRP & "|}" & vbCrLf
            mStr = mStr & "{PC010;0220,0443,1,1,H,00,B=" & pItemCode & "|}" & vbCrLf
            mStr = mStr & "{XB00;0380,0179,Q,20,07,04,0,C016016=" & Data & "|}" & vbCrLf
            mStr = mStr & "{XS;I,0001,0002C4201|}" & vbCrLf



            If PrintBySerialPort(mStr, MSComm2) = False Then

                MsgBox("Please Check Printer")

                Exit Sub

            End If



        Else

            MsgBox("Nothing To Print.")

            Exit Sub

        End If

        '    If ItemCode <> "" Then	
        '            mStr = "q600"	
        '            mStr = mStr & vbCrLf & "s2"	
        '            mStr = mStr & vbCrLf & "O"	
        '            mStr = mStr & vbCrLf & "JF"	
        '            mStr = mStr & vbCrLf & "WN"	
        '            mStr = mStr & vbCrLf & pBARCODEDarkNess             '"D7"      ' after "D" is darkness start from 1 - 15	
        '            mStr = mStr & vbCrLf & "ZB"	
        '            mStr = mStr & vbCrLf & "Q254,25"	
        '            mStr = mStr & vbCrLf & "N"	
        '            mStr = mStr & vbCrLf & "A80,15,0,1,2,2,N," & Chr(34) & Trim(pPartNo) & vbCrLf & pPartName & vbCrLf & pBinQty & vbCrLf & pSNo & vbCrLf & pItemCode & vbCrLf & pVendorName & vbCrLf & pMRP & vbCrLf & "MRP Inclusive of all taxes" & Chr(34)  ''vbCrLf & pMRP &	
        '            mStr = mStr & vbCrLf & "A80,15,0,2,2,2,N," & Chr(34) & Trim(pPartNo) & vbCrLf & pPartName & vbCrLf & pBinQty & vbCrLf & pSNo & vbCrLf & pItemCode & vbCrLf & pVendorName & vbCrLf & pMRP & vbCrLf & "MRP Inclusive of all taxes" & Chr(34)  ''vbCrLf & pMRP &	
        '            mStr = mStr & vbCrLf & "A40,15,0,3,2,2,N," & Chr(34) & Trim(pPartNo) & vbCrLf & pPartName & vbCrLf & pBinQty & vbCrLf & pSNo & vbCrLf & pItemCode & vbCrLf & pVendorName & vbCrLf & pMRP & vbCrLf & "MRP Inclusive of all taxes" & Chr(34)  ''vbCrLf & pMRP &	
        '            mStr = mStr & vbCrLf & "b75,48,P,600,304,s2,f0,x2,y6,r90,l5,t0,o0," & Chr(34) & Data & Chr(34)	
        '            mStr = mStr & vbCrLf & "P1" & vbCrLf	
        '	
        '            If PrintBySerialPort(mStr, MSComm2) = False Then	
        '                MsgBox "Please Check Printer"	
        '                Exit Sub	
        '            End If	
        '	
        '    Else	
        '        MsgBox "Nothing To Print."	
        '        Exit Sub	
        '    End If	


    End Sub
    Public Function PrintBySerialPort(ByRef DatatoPrint As String, ByRef MSComm3 As Object) As Boolean
        On Error GoTo ErrPart

        ' Note  Place Microsoft Comm Control to form and run the code	

        Dim F As Integer
        If pPortType = "U" Then
            F = FreeFile '*******Print through Dos	
            '        Open App.path & "\PCLOUT.TXT" For Output As F	
            FileOpen(F, mBarCodePath & "\PCLOUT.TXT", OpenMode.Output)
            PrintLine(F, DatatoPrint)
            FileClose(F)

            Shell(mBarCodePath & "\IDLSP.EXE")

        Else
            If MSComm3.PortOpen Then MSComm3.PortOpen = False ' If port opened close it	
            MSComm3.CommPort = pBARCODEPort ''1 'Com port 1	
            ' 9600 baud, no parity, 8 data, and 1 stop bit.	
            MSComm3.Settings = "9600,N,8,1"

            MSComm3.OutBufferSize = 1000
            'MSComm3.Handshaking = MSCommLib.HandshakeConstants.comXOnXoff
            MSComm3.PortOpen = True
            MSComm3.OutBufferCount = 0
            MSComm3.Output = DatatoPrint 'Sending data to printer	

            If MSComm3.PortOpen Then MSComm3.PortOpen = False
        End If

        PrintBySerialPort = True
        Exit Function
ErrPart:
        PrintBySerialPort = False
    End Function
End Module
