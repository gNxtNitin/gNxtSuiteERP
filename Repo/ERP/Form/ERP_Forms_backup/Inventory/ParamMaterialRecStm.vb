Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMaterialRecStm
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20


    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUnit As Short = 3
    Private Const ColOPQty As Short = 4
    Private Const ColINQty As Short = 5
    Private Const ColOUTQty As Short = 6
    Private Const ColDespatch As Short = 7
    Private Const ColRejQty As Short = 8
    Private Const ColClosingQty As Short = 9
    Private Const ColPhyClosingQty As Short = 10
    Private Const ColVariance As Short = 11
    Private Const ColPhyVariance As Short = 12
    Private Const ColRate As Short = 13
    Private Const ColAmount As Short = 14


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim mcntRow As Integer

    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image
    Dim mIsGrouped As Boolean
    Private Function GetBalanceStockQty_OK(ByRef pItemCode As String, ByRef pDateTo As String, ByRef pPackUnit As String, ByRef pDeptCode As String, ByRef pStock_ID As String, ByRef pIOType As String, ByRef pIsOK As String, Optional ByRef pFromDate As String = "", Optional ByRef pOpening As String = "", Optional ByRef pRefType As String = "", Optional ByRef pStockType As String = "") As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mTableName As String
        SqlStr = ""


        SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"

        mTableName = ConInventoryTable

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"

        If pStockType = "X" Then
            SqlStr = SqlStr & vbCrLf & "AND STOCK_TYPE<>'WP'"
        End If

        If pStockType = "ST" Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE ='" & pStockType & "'"
        Else
            If pIsOK = "N" Then
                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE IN ('RJ','SC')"
            ElseIf pIsOK = "Y" Then
                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('RJ','SC')"
            ElseIf pIsOK = "P" Then
                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('SC')"
            End If
        End If

        If pDeptCode <> "" And pStock_ID = ConPH Then
            SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & pDeptCode & "'"
        Else ''If pDeptCode = "PAD" And pStock_ID = ConWH And pStockType = "FG" Then
            ''02-08-2006
            '        SqlStr = SqlStr & vbCrLf & "AND (DEPT_CODE_FROM='" & pDeptCode & "' OR DEPT_CODE_TO='" & pDeptCode & "')"
        End If

        If pIOType <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM_IO = '" & pIOType & "'"
        End If

        If pRefType <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE IN " & pRefType & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        If pOpening = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<TO_DATE('" & VB6.Format(pFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            If pFromDate <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(pFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalStock, ADODB.LockTypeEnum.adLockReadOnly)

        If RsBalStock.EOF = False Then
            If IsDbNull(RsBalStock.Fields(0).Value) Then
                mBalQty = 0
            Else
                mBalQty = RsBalStock.Fields(0).Value
            End If
        Else
            mBalQty = 0
        End If

        RsBalStock = Nothing

        If mBalQty <> 0 Then
            RsTemp = Nothing

            SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mIssueUOM = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mPurchaseUOM = IIf(IsDbNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                mFactor = IIf(IsDbNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)

                If pPackUnit = mPurchaseUOM Then
                    mBalQty = mBalQty / mFactor
                End If

                RsTemp = Nothing
                '            RsTemp.Close
            End If
        End If

        GetBalanceStockQty_OK = mBalQty

        Exit Function
ErrPart:
        GetBalanceStockQty_OK = 0
    End Function

    'Private Function GetPhysicalBalance(pItemCode As String, pDateTo As String, pPackUnit As String, pDeptCode As String, _
    ''pStock_ID As String, pStockType As String) As Double
    'On Error GoTo ErrPart
    'Dim SqlStr As String = ""
    'Dim RsBalStock As ADODB.Recordset=Nothing
    'Dim mBalQty As Double
    '
    'Dim RsTemp As ADODB.Recordset=Nothing
    'Dim mIssueUOM As String = ""
    'Dim mPurchaseUOM As String = ""
    'Dim mFactor As Double
    'Dim mTableName As String
    '    SqlStr = ""
    '
    '
    '    SqlStr = "SELECT SUM(ID.PHY_QTY*DECODE(ID.ITEM_IO,'I',1,-1)) AS BALQTY"
    '
    '    SqlStr = SqlStr & vbCrLf & " FROM INV_PHY_HDR IH, INV_PHY_DET ID "
    '
    '    SqlStr = SqlStr & vbCrLf _
    ''            & " WHERE IH.AUTO_KEY_PHY=ID.AUTO_KEY_PHY AND IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""
    '
    '    If pStock_ID <> "" Then
    '        SqlStr = SqlStr & vbCrLf & "AND IH.BOOKTYPE='" & pStock_ID & "'"
    '    End If
    '
    '    If pStockType <> "" Then
    '        If pStockType = "X" Then
    '            SqlStr = SqlStr & vbCrLf & "AND ID.STOCK_TYPE<>'WP'"
    '        End If
    '
    '        If pStockType = "ST" Then
    '            SqlStr = SqlStr & vbCrLf & " AND ID.STOCK_TYPE ='" & pStockType & "'"
    '        End If
    '    End If
    '
    '    If pDeptCode <> "" And pStock_ID = ConPH Then
    '        SqlStr = SqlStr & vbCrLf & "AND IH.DEPT_CODE='" & pDeptCode & "'"
    '    Else            ''If pDeptCode = "PAD" And pStock_ID = ConWH And pStockType = "FG" Then
    '        ''02-08-2006
    ''        SqlStr = SqlStr & vbCrLf & "AND (DEPT_CODE_FROM='" & pDeptCode & "' OR DEPT_CODE_TO='" & pDeptCode & "')"
    '    End If
    '
    '
    '    SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
    '
    '    SqlStr = SqlStr & vbCrLf & " AND IH.PHY_DATE=TO_DATE('" & VB6.Format((pDateTo), "DD-MMM-YYYY") & "')"
    '
    '
    '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsBalStock, adLockReadOnly
    '
    '    If RsBalStock.EOF = False Then
    '        If IsNull(RsBalStock.Fields(0).Value) Then
    '            mBalQty = 0
    '        Else
    '            mBalQty = RsBalStock.Fields(0).Value
    '        End If
    '    Else
    '        mBalQty = 0
    '    End If
    '
    '    Set RsBalStock = Nothing
    '
    '    If mBalQty <> 0 Then
    '        Set RsTemp = Nothing
    '
    '        SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR FROM INV_ITEM_MST " & vbCrLf _
    ''                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
    '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
    '
    '        If RsTemp.EOF = False Then
    '            mIssueUOM = IIf(IsNull(RsTemp!ISSUE_UOM), "", RsTemp!ISSUE_UOM)
    '            mPurchaseUOM = IIf(IsNull(RsTemp!PURCHASE_UOM), "", RsTemp!PURCHASE_UOM)
    '            mFactor = IIf(IsNull(RsTemp!UOM_FACTOR) Or RsTemp!UOM_FACTOR = 0, 1, RsTemp!UOM_FACTOR)
    '
    '            If pPackUnit = mPurchaseUOM Then
    '                mBalQty = mBalQty / mFactor
    '            End If
    '
    '            Set RsTemp = Nothing
    ''            RsTemp.Close
    '        End If
    '    End If
    '
    '    GetPhysicalBalance = mBalQty
    '
    'Exit Function
    'ErrPart:
    '    GetPhysicalBalance = 0
    'End Function
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdSearch.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdSearch.Enabled = True
        End If
    End Sub
    Private Sub chkAllItemCode_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItemCode.CheckStateChanged
        Call PrintStatus(False)
        If chkAllItemCode.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemCode.Enabled = False
            cmdSearchItemCode.Enabled = False
            TxtItemName.Enabled = False
        Else
            txtItemCode.Enabled = True
            cmdSearchItemCode.Enabled = True
            TxtItemName.Enabled = True
        End If
    End Sub

    Private Sub chkPhy_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPhy.CheckStateChanged
        txtPhyDate.Enabled = IIf(chkPhy.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        Call PrintStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnIssue(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnIssue(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnIssue(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()

        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")


        mTitle = Me.Text
        mTitle = mTitle & "[" & txtItemCode.Text & " : " & TxtItemName.Text & "]"

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\DespVsIssueReport.rpt"

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub

    Private Sub cmdSearchItemCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItemCode.Click
        SearchItemCode()
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim pSqlStr As String = ""

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)

        Call Show1()
        Call FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Call PrintStatus(True)

        Exit Sub
ErrPart:
        '    PubDBCn.RollbackTrans
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Show1()

        On Error GoTo ErrPart
        Dim pSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim mTotDespatchQty As Double
        Dim mTotBOMQty As Double
        Dim mTotWIPQty As Double
        Dim mTotWIPOPQty As Double
        Dim mTotWIPCLQty As Double
        Dim mTotFGOPQty As Double
        Dim mTotFGCLQty As Double


        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mUOMFactor As Double
        Dim mOPBal As Double
        Dim mPurchase As Double
        Dim mInHouse As Double
        Dim mRMOut As Double
        Dim mRejQty As Double
        Dim mClosingQty As Double
        Dim mConsumption As Double
        Dim mVarianceQty As Double
        Dim xPurchaseCost As Double
        Dim xLandedCost As Double

        Dim mTotDespatch As Double
        Dim mTotFGScrap As Double

        Dim mStartCol As Integer
        Dim mEndCol As Integer

        Dim mPhyClosingQty As Double
        Dim mPhyConsumption As Double
        Dim mTotalIn As Double

        pSqlStr = MakeSQL
        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        I = 1
        With SprdMain
            Do While RsTemp.EOF = False
                mItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                mItemDesc = IIf(IsDbNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)
                mItemUOM = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mPurchaseUOM = IIf(IsDbNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                mUOMFactor = IIf(IsDbNull(RsTemp.Fields("UOM_FACTOR").Value), "", RsTemp.Fields("UOM_FACTOR").Value)
                mPurchase = 0
                mRejQty = 0
                mClosingQty = 0
                mConsumption = 0
                mTotDespatchQty = 0
                mRMOut = 0
                mTotBOMQty = 0
                mInHouse = 0

                If chkPhy.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    mOPBal = GetBalanceStockQty_OK(mItemCode, (txtDateTo.Text), mItemUOM, "", ConWH, "", "Y", txtDateFrom.Text, "Y", , "")
                    mOPBal = mOPBal + GetBalanceStockQty_OK(mItemCode, (txtDateTo.Text), mItemUOM, "", ConPH, "", "Y", txtDateFrom.Text, "Y", , "")
                Else
                    mOPBal = GetPhysicalBalance(mItemCode, (txtPhyDate.Text), mItemUOM, "", "", "", -1) + GetBalanceStockQty_OK(mItemCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), mItemUOM, "", ConWH, "", "P", CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(txtPhyDate.Text))), "", "", "")
                End If

                If chkPurchase.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    mPurchase = GetBalanceStockQty_OK(mItemCode, (txtDateTo.Text), mItemUOM, "", ConWH, "I", "P", txtDateFrom.Text, "", "('" & ConStockRefType_MRR & "')", "")
                    mPurchase = mPurchase + GetBalanceStockQty_OK(mItemCode, (txtDateTo.Text), mItemUOM, "", ConPH, "I", "Y", txtDateFrom.Text, "", "('" & ConStockRefType_PMEMODEPT & "')", "ST")

                    mRMOut = GetBalanceStockQty_OK(mItemCode, (txtDateTo.Text), mItemUOM, "", ConWH, "O", "", txtDateFrom.Text, "", "('" & ConStockRefType_DSP & "','" & ConStockRefType_RGP & "','" & ConStockRefType_NRG & "')", "")
                    mRMOut = System.Math.Abs(mRMOut)
                Else
                    mPurchase = GetPurchaseQty(mItemCode, (txtDateFrom.Text), (txtDateTo.Text), mItemUOM)
                    mRMOut = 0
                End If


                '            mPurchase = mPurchase - Abs(mRMOut)

                mRejQty = GetBalanceStockQty_OK(mItemCode, (txtDateFrom.Text), mItemUOM, "", ConWH, "", "N", txtDateFrom.Text, "Y", , "")
                mRejQty = mRejQty + GetBalanceStockQty_OK(mItemCode, (txtDateTo.Text), mItemUOM, "", ConWH, "", "N", txtDateFrom.Text, "", "", "")

                If chkPhy.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    mClosingQty = GetBalanceStockQty_OK(mItemCode, (txtDateTo.Text), mItemUOM, "STR", ConWH, "", "Y", "") 'GetBalanceStockQty_OK(pItemCode, txtDateTo.Text, mItemUOM, "", "ST", "", ConPH)
                    mClosingQty = mClosingQty + GetBalanceStockQty_OK(mItemCode, (txtDateTo.Text), mItemUOM, "", ConPH, "", "Y")
                Else
                    mClosingQty = mOPBal + GetBalanceStockQty_OK(mItemCode, (txtDateTo.Text), mItemUOM, "", ConWH, "", "P", txtDateFrom.Text, "", "", "")
                End If

                mPhyClosingQty = GetPhysicalBalance(mItemCode, (txtDateTo.Text), mItemUOM, "", "", "", -1)
                '            ''temp...
                '            mOPBal = GetPhysicalBalance(mItemCode, "11-MAY-2008", mItemUOM, "", "", "") + GetBalanceStockQty_OK(mItemCode, txtDateFrom.Text, mItemUOM, "", ConWH, "", "P", "12-MAY-2008", "", "", "")
                '            mClosingQty = mOPBal + GetBalanceStockQty_OK(mItemCode, txtDateTo.Text, mItemUOM, "", ConWH, "", "P", txtDateFrom.Text, "", "", "")

                mConsumption = mOPBal + mPurchase + mInHouse - mRMOut - mClosingQty - mRejQty
                mPhyConsumption = mOPBal + mPurchase + mInHouse - mRMOut - mPhyClosingQty - mRejQty

                If chkPurchase.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    mTotalIn = mOPBal + mPurchase + mRMOut + mInHouse
                Else
                    mTotalIn = mOPBal + mPurchase + mRMOut
                End If

                If mOPBal + mPurchase + mRMOut + mInHouse <> 0 Then ''mClosingQty + mConsumption + mTotDespatchQty
                    Call InsertTempTable(mItemCode)
                    .Row = I
                    mStartCol = I
                    .Col = ColItemCode
                    .Text = mItemCode

                    .Col = ColItemDesc
                    .Text = mItemDesc

                    .Col = ColUnit
                    .Text = mItemUOM

                    .Col = ColOPQty
                    .Text = CStr(mOPBal)

                    .Col = ColINQty
                    .Text = CStr(mPurchase)

                    .Col = ColOUTQty
                    .Text = CStr(mRMOut)

                    '                .Col = ColRejQty
                    '                .Text = mRejQty

                    .Col = ColClosingQty
                    .Text = VB6.Format(mClosingQty, "0.0000")

                    .Col = ColPhyClosingQty
                    .Text = VB6.Format(mPhyClosingQty, "0.0000")

                    If FillFGPart(I, mItemCode, mItemDesc, mItemUOM, mTotDespatch, mTotFGScrap) = False Then GoTo ErrPart

                    .Col = ColDespatch
                    .Text = VB6.Format(mTotDespatch, "0.0000")

                    .Col = ColRejQty
                    .Text = VB6.Format(System.Math.Abs(mRejQty + mTotFGScrap), "0.0000")

                    If GetLatestItemCostFromPO(mItemCode, xPurchaseCost, xLandedCost, (txtDateTo.Text), "ST", "", mItemUOM, mUOMFactor) = False Then GoTo ErrPart
                    If CalcTots(I, xPurchaseCost, mOPBal, mPurchase, mClosingQty, mPhyClosingQty, mRMOut + mTotDespatch, mRejQty + mTotFGScrap) = False Then GoTo ErrPart


                    I = I + 1
                End If
                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    .MaxRows = I
                End If
            Loop
        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function CalcTots(ByRef I As Integer, ByRef mRate As Double, ByRef mOPBal As Double, ByRef mPurchase As Double, ByRef mClosingQty As Double, ByRef mPhyClosingQty As Double, ByRef mTotDespatch As Double, ByRef xTotFGScrap As Double) As Boolean
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mVariation As Double
        Dim mPhyVariation As Double

        mVariation = mTotDespatch - (mOPBal + mPurchase - xTotFGScrap - mClosingQty)
        mPhyVariation = mTotDespatch - (mOPBal + mPurchase - mPhyClosingQty)

        With SprdMain

            .Row = I

            .Col = ColVariance
            .Text = VB6.Format(mVariation, "0.00")

            .Col = ColPhyVariance
            .Text = VB6.Format(mPhyVariation, "0.00")

            .Col = ColRate
            .Text = VB6.Format(mRate, "0.00")

            .Col = ColAmount
            .Text = VB6.Format(mRate * mVariation, "0.00")

        End With
        CalcTots = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CalcTots = False
    End Function
    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
    End Sub

    Private Sub frmParamMaterialRecStm_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Material Reconciliation Statement"

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamMaterialRecStm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)


        chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAll.Visible = False
        cmdSearch.Visible = False
        TxtItemName.Enabled = False
        cmdSearch.Enabled = False

        chkAllItemCode.CheckState = System.Windows.Forms.CheckState.Checked
        txtItemCode.Enabled = False
        cmdSearchItemCode.Enabled = False

        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False

        chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False

        cboShow.Items.Clear()
        cboShow.Items.Add("Defined in BOM")
        cboShow.Items.Add("Not Defined in BOM")
        cboShow.SelectedIndex = 0
        minuspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\minus1.bmp")
        pluspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\plus1.bmp")

        mIsGrouped = True

        Call PrintStatus(True)
        'Call FillPOCombo
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtPhyDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamMaterialRecStm_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamMaterialRecStm_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Close()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        'Dim SqlStr As String = ""
        'Dim mItemCode As String
        'Dim mItemDesc As String
        'Dim mItemUOM As String = ""
        'Dim mConsumption As String
        '
        '
        '    SprdMain.Row = SprdMain.ActiveRow
        '
        '    SprdMain.Col = ColItemCode
        '    mItemCode = Trim(SprdMain.Text)
        '
        '    SprdMain.Col = ColUnit
        '    mItemUOM = Trim(SprdMain.Text)
        '
        '    SprdMain.Col = ColItemDesc
        '    mItemDesc = Trim(SprdMain.Text)
        '
        '    SprdMain.Col = ColConsumption
        '    mConsumption = Trim(SprdMain.Text)
        '
        '    If mConsumption > 0 Then
        '        frmParamDespatchDetail.lblItemCode.text = mItemCode
        '        frmParamDespatchDetail.TxtItemName = mItemCode & " - " & mItemDesc
        '        frmParamDespatchDetail.lblItemUOM.text = mItemUOM
        '
        '        frmParamDespatchDetail.txtFromDate.Text = VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")
        '        frmParamDespatchDetail.txtToDate.Text = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        '        frmParamDespatchDetail.txtConsumption = Val(mConsumption)
        '
        '        frmParamDespatchDetail.Show 1
        '        frmParamDespatchDetail.Form_Activate
        '  End If
    End Sub

    Private Sub txtCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.DoubleClick
        SearchCategory()
    End Sub
    Private Sub txtCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCategory()
    End Sub
    Private Sub txtCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C' AND PRD_TYPE IN ('R','P','C')"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCategory.Text = UCase(Trim(txtCategory.Text))
        Else
            MsgInformation("No Such Category in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C' AND PRD_TYPE IN ('R','P','C')"

        If MainClass.SearchGridMaster((txtCategory.Text), "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtCategory.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
    End Sub
    Private Sub chkAllCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCategory.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCategory.Enabled = False
            cmdsearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdsearchCategory.Enabled = True
        End If
    End Sub

    Private Sub chkAllSubCat_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSubCat.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSubCategory.Enabled = False
            cmdSubCatsearch.Enabled = False
        Else
            txtSubCategory.Enabled = True
            cmdSubCatsearch.Enabled = True
        End If
    End Sub
    Private Sub SearchSubCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtCategory.Text = "" Then
            MsgInformation("Please Select category .")
            txtCategory.Focus()
            Exit Sub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C' AND PRD_TYPE IN ('R','P','C')"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCatCode = MasterNo
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"
        If MainClass.SearchGridMaster((txtSubCategory.Text), "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtSubCategory.Text = AcName
            End If
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtPhyDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPhyDate.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtPhyDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPhyDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtPhyDate) = False Then
            txtPhyDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtPhyDate.Text))) = False Then
            txtPhyDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtSubCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtSubCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.DoubleClick
        SearchSubCategory()
    End Sub


    Private Sub txtSubCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSubCategory()
    End Sub

    Private Sub txtSubCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtSubCategory.Text = "" Then GoTo EventExitSub

        If txtCategory.Text = "" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C' AND PRD_TYPE IN ('R','P','C')"
            If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mCatCode = MasterNo
            End If
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"
        End If

        If MainClass.ValidateWithMasterTable((txtSubCategory.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("No Such Sub Category ")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        SearchItemCode()
    End Sub
    Private Sub txtItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItemCode()
    End Sub
    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtItemCode.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtItemCode.Text = UCase(Trim(txtItemCode.Text))
            TxtItemName.Text = MasterNo
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
    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchItemCode()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtItemCode.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , SqlStr)
        If AcName <> "" Then
            txtItemCode.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub
    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If TxtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtItemName.Text = UCase(Trim(TxtItemName.Text))
            txtItemCode.Text = MasterNo
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
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColAmount
            .set_RowHeight(0, RowHeight * 1.2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            '        .Col = ColPicMain
            '        .CellType = CellTypePicture
            '        .TypePictCenter = True
            '        .TypePictMaintainScale = False
            '        .TypePictStretch = False
            '        .ColHidden = True
            '
            '        .Col = ColPicSub
            '        .CellType = CellTypePicture
            '        .TypePictCenter = True
            '        .TypePictMaintainScale = False
            '        .TypePictStretch = False
            '        .ColHidden = True

            '        .Col = ColProdCode
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(ColProdCode) = 7
            '
            '        .Col = ColProdName
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(ColProdName) = 25
            ''        .ColHidden = True

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 7)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 20)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            '        .Col = ColDeptCode
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(ColDeptCode) = 4

            For cntCol = ColOPQty To ColAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 8)
            Next

            '        .Col = ColItemCode
            '        .ColMerge = MergeAlways
            '        .Col = ColItemDesc
            '        .ColMerge = MergeAlways
            '        .Col = ColUnit
            '        .ColMerge = MergeAlways

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function MakeSQL() As String

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""
        Dim mSubCatCode As String

        SqlStr = " SELECT ITEM_CODE, ITEM_SHORT_DESC, ISSUE_UOM,PURCHASE_UOM,UOM_FACTOR " & vbCrLf & " FROM INV_ITEM_MST INVMST, INV_GENERAL_MST CMST" & vbCrLf & " WHERE INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND INVMST.CATEGORY_CODE=CMST.GEN_CODE" & vbCrLf & " AND CMST.GEN_TYPE='C'" & vbCrLf & " AND CMST.PRD_TYPE IN ('R','P','C')" & vbCrLf & " AND STOCKTYPE<>'FG'"

        If chkAllItemCode.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemCode.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(Trim(txtItemCode.Text)) & "'"
        End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND CMST.GEN_DESC='" & MainClass.AllowSingleQuote(txtCategory.Text) & "'"
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                '            SqlStr = SqlStr & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INVMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND INVMST.ITEM_CODE IN (" & vbCrLf _
        ''            & " SELECT DISTINCT RM_CODE FROM VW_PRD_BOM_TRN" & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ")"

        SqlStr = SqlStr & vbCrLf & " ORDER BY CMST.GEN_DESC, INVMST.ITEM_CODE"

        MakeSQL = SqlStr

        Exit Function
LedgError:
        '    Resume
        MakeSQL = ""
        MsgInformation(Err.Description)
    End Function
    Private Function GetDespatchQty(ByRef pProductCode As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mSameItemCode As String

        SqlStr = ""
        GetDespatchQty = 0

        mTable = ConInventoryTable

        mSameItemCode = GetSameItemCode(pProductCode)
        If mSameItemCode = "" Then
            mSameItemCode = "('" & pProductCode & "')"
        Else
            mSameItemCode = "('" & pProductCode & "'," & mSameItemCode & ")"
        End If


        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,-1)) AS ITEM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='" & ConWH & "'" & vbCrLf & " AND ITEM_CODE IN " & mSameItemCode & ""


        SqlStr = SqlStr & vbCrLf & " AND ITEM_IO ='O'"
        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('RJ','CR')"
        SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('" & ConStockRefType_DSP & "','" & ConStockRefType_RGP & "','" & ConStockRefType_NRG & "')"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetDespatchQty = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
LedgError:
        GetDespatchQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetPurchaseQty(ByRef pItemCode As String, ByRef pDateFrom As String, ByRef pDateTo As String, ByRef mItemUOM As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""
        GetPurchaseQty = 0

        SqlStr = " SELECT SUM(APPROVED_QTY+GETREOFFERQTY_NEW (IH.COMPANY_CODE,IH.AUTO_KEY_MRR, IH.MRR_DATE, IH.SUPP_CUST_CODE, ID.ITEM_CODE,ID.REF_AUTO_KEY_NO)) AS ITEM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf & " AND ID.ITEM_CODE = '" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.REF_TYPE IN ('P','F')"
        SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE >= TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.MRR_DATE <= TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPurchaseQty = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If


        Exit Function
LedgError:
        GetPurchaseQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function FillFGPart(ByRef I As Integer, ByRef mItemCode As String, ByRef mItemDesc As String, ByRef mItemUOM As String, ByRef mTotDespatchQty As Double, ByRef mTotFGScrap As Double) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsRM As ADODB.Recordset = Nothing
        Dim mProductCode As String = ""
        Dim mDeptCode As String
        Dim mProductdesc As String = ""
        Dim mStdQty As Double
        Dim mDespatchQty As Double
        Dim mFGScrap As Double

        mTotDespatchQty = 0
        mTotFGScrap = 0

        ''DISTINCT

        ''19-06-2009...
        SqlStr = ""
        SqlStr = " SELECT " & vbCrLf & " TRN.FG_CODE, SUM(TRN.STD_QTY) AS STD_QTY" & vbCrLf & " FROM TEMP_DESPVSISSUE TRN" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND CHILD_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.FG_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsRM.EOF = False Then
            Do While RsRM.EOF = False
                mProductCode = Trim(IIf(IsDbNull(RsRM.Fields("FG_CODE").Value), "", RsRM.Fields("FG_CODE").Value))
                '            mDeptCode = IIf(IsNull(RsRM!DEPT_CODE), "", RsRM!DEPT_CODE)
                mDespatchQty = 0
                mFGScrap = 0

                If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mProductdesc = MasterNo
                End If
                mStdQty = IIf(IsDbNull(RsRM.Fields("STD_QTY").Value), 0, RsRM.Fields("STD_QTY").Value)

                mDespatchQty = GetDespatchQty(mProductCode)
                '            mDespatchQty = mDespatchQty + (-1 * GetBalanceStockQty_OK(mProductCode, txtDateTo.Text, "", "", ConWH, "O", "", txtDateFrom.Text, "", "('" & ConStockRefType_PMEMODEPT & "')", "ST"))
                '            mDespatchQty = mDespatchQty + (-1 * GetBalanceStockQty_OK(mProductCode, txtDateTo.Text, "", "", ConPH, "O", "", txtDateFrom.Text, "", "('" & ConStockRefType_PMEMODEPT & "')", "ST"))
                mTotDespatchQty = mTotDespatchQty + (mDespatchQty * mStdQty)

                mFGScrap = GetFGScrapQty(mProductCode, ConWH)
                mFGScrap = mFGScrap + GetFGScrapQty(mProductCode, ConPH)

                mTotFGScrap = mTotFGScrap + (mFGScrap * mStdQty)

                RsRM.MoveNext()

            Loop
        End If
        FillFGPart = True
        Exit Function
LedgError:
        FillFGPart = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function InsertTempTable(ByRef mItemCode As String) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsRM As ADODB.Recordset = Nothing
        Dim xItemCode As String = ""
        Dim xSTDQty As Double
        Dim mLevel As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " DELETE FROM TEMP_DESPVSISSUE WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " '" & mItemCode & "', ID.RM_CODE, IH.PRODUCT_CODE, (STD_QTY + GROSS_WT_SCRAP)* DECODE(INVMST.ISSUE_UOM,'KGS',.001,DECODE(INVMST.ISSUE_UOM,'TON',.001*.001,1)) ,ID.DEPT_CODE, 1 " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.RM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND STATUS='O'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " '" & mItemCode & "',ID.ITEM_CODE, IH.PRODUCT_CODE, (ITEM_QTY + SCRAP_QTY),'J/W',  1 " & vbCrLf & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND STATUS='O'"

        PubDBCn.Execute(SqlStr)

        mLevel = 1

        For mLevel = 1 To 5
            SqlStr = " SELECT *  FROM TEMP_DESPVSISSUE " & vbCrLf & " WHERE FG_LEVEL=" & mLevel & " AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRM, ADODB.LockTypeEnum.adLockReadOnly)

            If RsRM.EOF = False Then
                Do While Not RsRM.EOF
                    xItemCode = IIf(IsDbNull(RsRM.Fields("FG_CODE").Value), "", RsRM.Fields("FG_CODE").Value)
                    xSTDQty = IIf(IsDbNull(RsRM.Fields("STD_QTY").Value), 0, RsRM.Fields("STD_QTY").Value)

                    SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " '" & mItemCode & "',ID.RM_CODE, IH.PRODUCT_CODE, (STD_QTY + GROSS_WT_SCRAP)* DECODE(INVMST.ISSUE_UOM,'KGS',.001,DECODE(INVMST.ISSUE_UOM,'TON',.001*.001,1)) * " & xSTDQty & ",ID.DEPT_CODE,  " & mLevel + 1 & " " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.RM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "' AND STATUS='O'" ''& vbCrLf |                        & " AND IH.WEF=("

                    '                SqlStr = SqlStr & vbCrLf & " SELECT MAX(WEF) AS WEF" & vbCrLf _
                    ''                        & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf _
                    ''                        & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    ''                        & " AND IH.MKEY=ID.MKEY" & vbCrLf _
                    ''                        & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'" & vbCrLf _
                    ''                        & " AND WEF<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"
                    '
                    PubDBCn.Execute(SqlStr)


                    RsRM.MoveNext()

                Loop
            End If
        Next

        PubDBCn.CommitTrans()
        Exit Function
LedgError:
        '    Resume
        PubDBCn.RollbackTrans()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetDespatchQtyOLD(ByRef mItemCode As String, ByRef pTotDespatchQty As Double, ByRef pTotBOMQty As Double, ByRef pTotWIPOPQty As Double, ByRef pTotWIPProdQty As Double, ByRef pTotWIPCLQty As Double, ByRef pTotFGOPQty As Double, ByRef pTotFGPDIQty As Double, ByRef pTotFGCLQty As Double) As Boolean
        'On Error GoTo LedgError
        'Dim SqlStr As String = ""
        'Dim RsRM As ADODB.Recordset=Nothing
        'Dim RsTemp As ADODB.Recordset=Nothing
        'Dim mTable As String
        'Dim mSameItemCode As String
        'Dim mProductCode As String = ""
        'Dim mStdQty As Double
        'Dim mProductUOM As String = ""
        '
        'Dim mDespatchQty As Double
        'Dim mBOMQty As Double
        'Dim mWIPQty As Double
        'Dim mDeptCode As String
        '
        'Dim mWIPOPQty As Double
        'Dim mWIPCLQty As Double
        'Dim mFGOPQty As Double
        'Dim mFGCLQty As Double
        '
        'Dim mRMCode As String
        'Dim mRMStdQty As Double
        '
        '    SqlStr = ""
        '    pTotDespatchQty = 0
        '    pTotBOMQty = 0
        ''    pTotWIPQty = 0
        '    pTotWIPOPQty = 0
        '    pTotWIPCLQty = 0
        '    pTotFGOPQty = 0
        '    pTotFGCLQty = 0
        '    pTotWIPProdQty = 0
        '    pTotFGPDIQty = 0
        '
        '    GetDespatchQty = False
        '
        '
        '    SqlStr = " SELECT " & vbCrLf _
        ''            & " TRN.PRODUCT_CODE, TRN.RM_CODE, TRN.STD_QTY, DEPT_CODE, GROSS_WT_SCRAP" & vbCrLf _
        ''            & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _
        ''            & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE<>'J/W'"
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE<>'J/W' AND RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
        ''            & " CONNECT BY TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE<>'J/W' AND PRIOR PRODUCT_CODE=RM_CODE"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsRM, adLockReadOnly
        '
        '    If RsRM.EOF = False Then
        '        Do While RsRM.EOF = False
        '            mProductCode = IIf(IsNull(RsRM!PRODUCT_CODE), "", RsRM!PRODUCT_CODE)
        '            mDeptCode = IIf(IsNull(RsRM!DEPT_CODE), "", RsRM!DEPT_CODE)
        '            mStdQty = IIf(IsNull(RsRM!STD_QTY), "", RsRM!STD_QTY) + IIf(IsNull(RsRM!GROSS_WT_SCRAP), "", RsRM!GROSS_WT_SCRAP)
        '            mRMCode = IIf(IsNull(RsRM!RM_CODE), "", RsRM!RM_CODE)
        '            If Trim(mRMCode) = Trim(mItemCode) Then
        '                mRMStdQty = IIf(IsNull(RsRM!STD_QTY), "", RsRM!STD_QTY) + IIf(IsNull(RsRM!GROSS_WT_SCRAP), "", RsRM!GROSS_WT_SCRAP)
        '            End If
        '            mDespatchQty = 0
        '            mBOMQty = 0
        '            mWIPQty = 0
        '
        ''            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        ''                mProductUOM = MasterNo
        ''            End If
        ''            If mProductUOM = "KGS" Then
        ''                mStdQty = mStdQty / 1000
        ''            ElseIf mProductUOM = "TON" Or mProductUOM = "MT" Then
        ''                mStdQty = mStdQty / (1000 * 1000)
        ''            End If
        '            ''temp..
        '            mSameItemCode = GetSameItemCode(mProductCode)
        '            If mSameItemCode = "" Then
        '                mSameItemCode = "('" & mProductCode & "')"
        '            Else
        '                mSameItemCode = "('" & mProductCode & "'," & mSameItemCode & ")"
        '            End If
        '
        ''             If mType = "D" Then
        '            SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,-1)) AS ITEM_QTY "
        ''            Else
        ''                SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) AS ITEM_QTY "
        ''            End If
        '
        '            SqlStr = SqlStr & vbCrLf _
        ''                    & " FROM " & mTable & "" & vbCrLf _
        ''                    & " WHERE " & vbCrLf _
        ''                    & " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''                    & " AND FYEAR=" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
        ''                    & " AND STOCK_ID='" & ConWH & "'" & vbCrLf _
        ''                    & " AND ITEM_CODE IN " & mSameItemCode & ""
        '
        '
        '            SqlStr = SqlStr & vbCrLf & " AND ITEM_IO ='O'"
        '            SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('RJ','CR')"
        '            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('" & ConStockRefType_DSP & "','" & ConStockRefType_RGP & "','" & ConStockRefType_NRG & "')"
        '
        '            SqlStr = SqlStr & vbCrLf _
        ''                    & " AND REF_DATE >= '" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''                    & " AND REF_DATE <= '" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
        '
        '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '            If RsTemp.EOF = False Then
        '                mDespatchQty = IIf(IsNull(RsTemp!ITEM_QTY), 0, RsTemp!ITEM_QTY)
        '            End If
        '
        '            mFGCLQty = GetWIPQty(mProductCode, mSameItemCode, mDeptCode, txtDateTo.Text, "N", ConWH, mTable) * mStdQty * IIf(Trim(mRMCode) = Trim(mItemCode), 1, mRMStdQty)
        '            mFGOPQty = GetWIPQty(mProductCode, mSameItemCode, mDeptCode, txtDateFrom.Text, "Y", ConWH, mTable) * mStdQty * IIf(Trim(mRMCode) = Trim(mItemCode), 1, mRMStdQty)
        '
        '            mWIPCLQty = GetWIPQty(mProductCode, mSameItemCode, mDeptCode, txtDateTo.Text, "N", ConPH, mTable) * mStdQty * IIf(Trim(mRMCode) = Trim(mItemCode), 1, mRMStdQty)
        '            mWIPOPQty = GetWIPQty(mProductCode, mSameItemCode, mDeptCode, txtDateFrom.Text, "Y", ConPH, mTable) * mStdQty * IIf(Trim(mRMCode) = Trim(mItemCode), 1, mRMStdQty)
        '            mBOMQty = mDespatchQty * mStdQty * IIf(Trim(mRMCode) = Trim(mItemCode), 1, mRMStdQty)
        '
        '
        '
        '            pTotDespatchQty = pTotDespatchQty + mDespatchQty
        '            pTotBOMQty = pTotBOMQty + mBOMQty
        ''            pTotWIPQty = pTotWIPQty + mWIPQty
        '            pTotWIPOPQty = pTotWIPOPQty + mWIPOPQty
        '            pTotWIPCLQty = pTotWIPCLQty + mWIPCLQty
        '            pTotFGOPQty = pTotFGOPQty + mFGOPQty
        '            pTotFGCLQty = pTotFGCLQty + mFGCLQty
        '
        '            RsRM.MoveNext
        '        Loop
        '    End If
        '    GetDespatchQty = True
        '    Exit Function
        'LedgError:
        '    GetDespatchQty = False
        '    ErrorMsg err.Description, err.Number, vbCritical
    End Function
    Private Function GetWIPQty(ByRef pProductCode As String, ByRef mIsOpening As String, ByRef mStockID As String, ByRef mDeptCode As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsDeptSeq As ADODB.Recordset = Nothing
        Dim mDeptSeq As Integer

        Dim xCheckDept As String

        Dim mTable As String

        mTable = ConInventoryTable
        GetWIPQty = 0

        SqlStr = "SELECT SERIAL_NO FROM PRD_PRODSEQUENCE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "' ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeptSeq, ADODB.LockTypeEnum.adLockReadOnly)

        If RsDeptSeq.EOF = False Then
            mDeptSeq = IIf(IsDbNull(RsDeptSeq.Fields("SERIAL_NO").Value), 0, RsDeptSeq.Fields("SERIAL_NO").Value)
        Else
            GetWIPQty = 0
            Exit Function
        End If

        SqlStr = "SELECT * FROM PRD_PRODSEQUENCE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf & " AND SERIAL_NO >=" & mDeptSeq & " ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeptSeq, ADODB.LockTypeEnum.adLockReadOnly)

        If RsDeptSeq.EOF = False Then
            Do While RsDeptSeq.EOF = False
                xCheckDept = IIf(IsDbNull(RsDeptSeq.Fields("DEPT_CODE").Value), "", RsDeptSeq.Fields("DEPT_CODE").Value)

                SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) AS ITEM_QTY " & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID = '" & ConPH & "'" & vbCrLf & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(pProductCode) & "'"


                SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

                If mIsOpening = "P" Then

                    If Trim(xCheckDept) = Trim(mDeptCode) Then
                        SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & xCheckDept & "'"
                    Else
                        SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='-1'"
                    End If

                    SqlStr = SqlStr & vbCrLf & " AND REF_TYPE = '" & ConStockRefType_PMEMODEPT & "'"
                    SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('WP')"
                    SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    SqlStr = SqlStr & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                Else
                    SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & xCheckDept & "'"
                    If Trim(xCheckDept) = Trim(mDeptCode) Then
                        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('WP')"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('WR')"
                    End If

                    If mIsOpening = "Y" Then
                        SqlStr = SqlStr & vbCrLf & " AND REF_DATE < TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    End If
                End If

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    GetWIPQty = GetWIPQty + IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                End If

                RsDeptSeq.MoveNext()
            Loop
            If mIsOpening <> "P" Then
                GetWIPQty = GetWIPQty + GetOtherDeptWIP(pProductCode, mIsOpening, mTable)
            End If
        End If
        Exit Function
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetFGQty(ByRef pProductCode As String, ByRef mIsOpening As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mSameItemCode As String
        GetFGQty = 0
        mTable = ConInventoryTable

        mSameItemCode = GetSameItemCode(pProductCode)
        If mSameItemCode = "" Then
            mSameItemCode = "('" & pProductCode & "')"
        Else
            mSameItemCode = "('" & pProductCode & "'," & mSameItemCode & ")"
        End If

        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) AS ITEM_QTY " & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID = '" & ConWH & "'" & vbCrLf & " AND ITEM_CODE IN " & mSameItemCode & ""


        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('CR')"
        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        If mIsOpening = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE < TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetFGQty = GetFGQty + IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetFGScrapQty(ByRef pProductCode As String, ByRef xStockID As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mSameItemCode As String
        GetFGScrapQty = 0

        mTable = ConInventoryTable

        mSameItemCode = GetSameItemCode(pProductCode)
        If mSameItemCode = "" Then
            mSameItemCode = "('" & pProductCode & "')"
        Else
            mSameItemCode = "('" & pProductCode & "'," & mSameItemCode & ")"
        End If

        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) AS ITEM_QTY " & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID = '" & xStockID & "'" & vbCrLf & " AND ITEM_CODE IN " & mSameItemCode & ""

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE IN ('WR','SC','RJ')"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetFGScrapQty = GetFGScrapQty + IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetOtherDeptWIP(ByRef pProductCode As String, ByRef mIsOpening As String, ByRef mTable As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) AS ITEM_QTY " & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID = '" & ConPH & "'" & vbCrLf & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(pProductCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE_FROM NOT IN (" & vbCrLf & " SELECT DEPT_CODE FROM PRD_PRODSEQUENCE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf & " )"

        If mIsOpening = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE < TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetOtherDeptWIP = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If


        Exit Function
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        If chkPhy.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MainClass.ChkIsdateF(txtPhyDate) = False Then Exit Function
            If FYChk(CStr(CDate(txtPhyDate.Text))) = False Then txtPhyDate.Focus()
        End If
        '    If chkAll.Value = vbUnchecked Then
        '        If Trim(TxtItemName.Text) = "" Then
        '            MsgInformation "Invaild Item Name"
        '            TxtItemName.SetFocus
        '            FieldsVerification = False
        '            Exit Function
        '        End If
        '        If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '            MsgInformation "Invaild Item Name"
        '            TxtItemName.SetFocus
        '            FieldsVerification = False
        '            Exit Function
        '        End If
        '    End If

        If chkAllItemCode.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtItemCode.Text) = "" Then
                MsgInformation("Invaild Item Code")
                txtItemCode.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtItemCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Item Code")
                txtItemCode.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
