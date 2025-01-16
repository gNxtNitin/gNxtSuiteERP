Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamRMStm
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20


    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUnit As Short = 3
    Private Const ColInHouseCode As Short = 4
    Private Const ColInHouseDesc As Short = 5
    Private Const ColGrossWt As Short = 6
    Private Const ColPurchase As Short = 7
    Private Const ColOpening As Short = 8
    Private Const ColIssue As Short = 9
    Private Const ColSale As Short = 10
    Private Const ColClosing As Short = 11
    Private Const ColReqQty As Short = 12
    Private Const ColActualQty As Short = 13
    Private Const ColDiff As Short = 14
    Private Const ColDespatch As Short = 15

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim mcntRow As Integer

    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image
    Dim mIsGrouped As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If ChkALL.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub
    Private Sub chkAllItemCode_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItemCode.CheckStateChanged
        Call PrintStatus(False)
        If chkAllItemCode.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemcode.Enabled = False
            cmdSearchItemCode.Enabled = False
            TxtItemName.Enabled = False
        Else
            txtItemcode.Enabled = True
            cmdSearchItemCode.Enabled = True
            TxtItemName.Enabled = True
        End If
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

        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtdateTo.Text, "DD/MM/YYYY")


        mTitle = Me.Text
        mTitle = mTitle & "[" & txtItemcode.Text & " : " & TxtItemName.Text & "]"

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
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mDespatch As Double
        Dim mProduction As Double
        Dim mDataShow As Double
        Dim mInHouseCode As String
        Dim mInHouseDesc As String
        Dim mGrossWt As Double
        Dim mGrossWtGrm As Double
        Dim mUOMFactor As Double

        Dim mPurchase As Double
        Dim mOpening As Double
        Dim mIssue As Double
        Dim mSale As Double
        Dim mClosing As Double
        Dim mReqQty As Double
        Dim mActualQty As Double
        Dim mDiff As Double

        If optView(0).Checked = True Then
            pSqlStr = MakeSQL & " UNION " & MakeSQLAlter
        Else
            pSqlStr = MakeSQLNew
        End If

        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        I = 1
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False

                mItemCode = IIf(IsDbNull(RsTemp.Fields("RM_CODE").Value), "", RsTemp.Fields("RM_CODE").Value)
                mItemDesc = IIf(IsDbNull(RsTemp.Fields("RM_DESC").Value), "", RsTemp.Fields("RM_DESC").Value)
                mItemUOM = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mUOMFactor = IIf(IsDbNull(RsTemp.Fields("UOM_FACTOR").Value), "", RsTemp.Fields("UOM_FACTOR").Value)
                mInHouseCode = IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value)
                mInHouseDesc = IIf(IsDbNull(RsTemp.Fields("FG_DESC").Value), "", RsTemp.Fields("FG_DESC").Value)
                mGrossWtGrm = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value), "0.0000"))
                mGrossWtGrm = mGrossWtGrm + CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("GROSS_WT_SCRAP").Value), 0, RsTemp.Fields("GROSS_WT_SCRAP").Value), "0.0000"))

                If mGrossWtGrm = 0 Then mDataShow = 0 : GoTo NextRecd

                mGrossWt = mGrossWtGrm
                If optView(0).Checked = True Then
                    If mItemUOM = "KGS" Then
                        mGrossWt = mGrossWt / 1000
                    ElseIf mItemUOM = "TON" Or mItemUOM = "MT" Then
                        mGrossWt = mGrossWt / 1000
                        mGrossWt = mGrossWt / 1000
                    End If
                End If
                Call InsertTempTable(mInHouseCode)

                mPurchase = 0
                mOpening = 0
                mIssue = 0
                mSale = 0
                mClosing = 0
                mReqQty = 0
                mActualQty = 0
                mDiff = 0

                mOpening = GetBalanceStockQty(mItemCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), mItemUOM, "", "ST", "", ConWH, -1)
                mOpening = mOpening + GetBalanceStockQty(mItemCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), mItemUOM, "", "QC", "", ConWH, -1)
                '            mOpening = mOpening + GetBalanceStockQty(mItemCode, DateAdd("d", -1, txtDateFrom.Text), mItemUOM, "", "ST", "", ConPH)

                mPurchase = GetPurchaseQty(mItemCode, mItemUOM)
                mClosing = GetBalanceStockQty(mItemCode, (txtdateTo.Text), mItemUOM, "", "ST", "", ConWH, -1)
                mClosing = mClosing + GetBalanceStockQty(mItemCode, (txtdateTo.Text), mItemUOM, "", "QC", "", ConWH, -1)
                mClosing = mClosing + GetBalanceStockQty(mItemCode, (txtdateTo.Text), mItemUOM, "", "CR", "", ConWH, -1)
                '            mClosing = mClosing + GetBalanceStockQty(mItemCode, txtDateTo.Text, mItemUOM, "", "ST", "", ConPH)

                mIssue = GetIssueQty(mItemCode, mItemUOM)
                mIssue = mIssue - GetSRNQty(mItemCode, mItemUOM)
                mSale = GetDespatchQty(mItemCode, True, "")
                mActualQty = GetProductQty(mItemCode, mInHouseCode)
                mDespatch = GetItemDespatch(mInHouseCode, "D")
                '            mProduction = GetItemDespatch(mInHouseCode, "P")
                mReqQty = Int(mIssue / mGrossWt)

                mDiff = mActualQty - mReqQty

                mDataShow = mOpening + mPurchase + mIssue + mSale
                If mDataShow <> 0 Then
                    SprdMain.Row = I
                    SprdMain.Col = ColItemCode
                    SprdMain.Text = mItemCode

                    SprdMain.Col = ColItemDesc
                    SprdMain.Text = mItemDesc

                    SprdMain.Col = ColUnit
                    SprdMain.Text = mItemUOM

                    SprdMain.Col = ColInHouseCode
                    SprdMain.Text = mInHouseCode

                    SprdMain.Col = ColInHouseDesc
                    SprdMain.Text = mInHouseDesc

                    SprdMain.Col = ColGrossWt
                    SprdMain.Text = VB6.Format(mGrossWtGrm, "0.000")

                    SprdMain.Col = ColPurchase
                    SprdMain.Text = VB6.Format(mPurchase, "0.000")

                    SprdMain.Col = ColOpening
                    SprdMain.Text = VB6.Format(mOpening, "0.000")

                    SprdMain.Col = ColIssue
                    SprdMain.Text = VB6.Format(mIssue, "0.000")

                    SprdMain.Col = ColSale
                    SprdMain.Text = VB6.Format(mSale, "0.000")

                    SprdMain.Col = ColClosing
                    SprdMain.Text = VB6.Format(mClosing, "0.000")

                    SprdMain.Col = ColReqQty
                    SprdMain.Text = VB6.Format(mReqQty, "0.000")

                    SprdMain.Col = ColActualQty
                    SprdMain.Text = VB6.Format(mActualQty, "0.000")

                    SprdMain.Col = ColDiff
                    SprdMain.Text = VB6.Format(mDiff, "0.000")

                    SprdMain.Col = ColDespatch
                    SprdMain.Text = VB6.Format(mDespatch, "0.000")
                End If
NextRecd:
                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    If mDataShow <> 0 Then
                        I = I + 1
                        SprdMain.MaxRows = I
                    End If
                End If
            Loop
        End If

        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function GetItemDespatch(ByRef mItemCode As String, ByRef mIsDespatch As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsRM As ADODB.Recordset = Nothing
        Dim mProductCode As String = ""
        Dim mDeptCode As String = ""
        Dim mProductdesc As String = ""
        Dim mStdQty As Double
        Dim mDespatchQty As Double
        Dim mFGScrap As Double
        Dim mTotDespatchQty As Double
        Dim mTotFGScrap As Double
        Dim mCheckRMCode As String
        Dim mString As String = ""
        Dim mLevel As Double

        mTotDespatchQty = 0
        mTotFGScrap = 0

        SqlStr = ""
        '    SqlStr = " SELECT DISTINCT " & vbCrLf _
        ''            & " TRN.FG_CODE, TRN.STD_QTY" & vbCrLf _
        ''            & " FROM TEMP_DESPVSISSUE TRN" & vbCrLf _
        ''            & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
        ''            & " AND CHILD_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        SqlStr = " SELECT RM_CODE ,FG_CODE ,STD_QTY,FG_LEVEL,DEPT_CODE " & vbCrLf & " From TEMP_DESPVSISSUE " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' " & vbCrLf & " START WITH RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " CONNECT BY PRIOR FG_CODE=RM_CODE AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" '' & vbCrLf |
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsRM.EOF = False Then
            Do While RsRM.EOF = False
                mCheckRMCode = Trim(IIf(IsDbNull(RsRM.Fields("RM_CODE").Value), "", RsRM.Fields("RM_CODE").Value))
                If Trim(mCheckRMCode) = Trim(mItemCode) Then
                    mStdQty = 1
                End If
                mProductCode = Trim(IIf(IsDbNull(RsRM.Fields("FG_CODE").Value), "", RsRM.Fields("FG_CODE").Value))
                mLevel = CDbl(Trim(IIf(IsDbNull(RsRM.Fields("FG_LEVEL").Value), 0, RsRM.Fields("FG_LEVEL").Value)))
                mDeptCode = IIf(IsDbNull(RsRM.Fields("DEPT_CODE").Value), "", RsRM.Fields("DEPT_CODE").Value)
                mDespatchQty = 0
                mFGScrap = 0

                If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mProductdesc = MasterNo
                End If
                mStdQty = mStdQty * IIf(IsDbNull(RsRM.Fields("STD_QTY").Value), 0, RsRM.Fields("STD_QTY").Value)

                If InStr(1, mString, mProductCode) = 0 Then
                    If mIsDespatch = "D" Then
                        mDespatchQty = GetDespatchQty(mProductCode, False, "D")
                    ElseIf mIsDespatch = "R" Then
                        mDespatchQty = GetDespatchQty(mProductCode, False, "R")
                    ElseIf mIsDespatch = "S" Then
                        mDespatchQty = GetScrapQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), (txtdateTo.Text))
                    Else
                        If mLevel = 1 Then
                            mDespatchQty = GetProductionQtyOld(mProductCode, mDeptCode)
                        Else
                            mDespatchQty = 0
                        End If
                    End If
                End If
                mTotDespatchQty = mTotDespatchQty + (mDespatchQty * mStdQty)
                mString = mString & "," & mProductCode
                RsRM.MoveNext()

            Loop
        Else
            If mIsDespatch = "D" Then
                mTotDespatchQty = GetDespatchQty(mItemCode, False, "D")
            ElseIf mIsDespatch = "R" Then
                mTotDespatchQty = GetDespatchQty(mItemCode, False, "R")
            ElseIf mIsDespatch = "S" Then
                mTotDespatchQty = GetScrapQty(mItemCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), (txtdateTo.Text))
            Else
                If mLevel = 1 Then
                    mTotDespatchQty = GetProductionQtyOld(mItemCode, mDeptCode)
                Else
                    mDespatchQty = 0
                End If
            End If
        End If
        GetItemDespatch = mTotDespatchQty
        Exit Function
LedgError:
        GetItemDespatch = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetItemWIP(ByRef mItemCode As String, ByRef mWIPOPQty As Double, ByRef mWIPInQty As Double, ByRef mWIPCLQty As Double, ByRef mWIPPhyOPQty As Double, ByRef mWIPPhyCLQty As Double) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsRM As ADODB.Recordset = Nothing
        Dim RsSeq As ADODB.Recordset = Nothing
        Dim mProductCode As String = ""
        Dim mDeptCode As String
        Dim mStdQty As Double
        'Dim mWIPOPQty As Double
        'Dim mWIPCLQty As Double
        Dim mItemUOM As String = ""
        Dim mCheckRMCode As String
        Dim mString As String = ""

        SqlStr = ""
        '    SqlStr = " SELECT DISTINCT " & vbCrLf _
        ''            & " TRN.FG_CODE, TRN.STD_QTY" & vbCrLf _
        ''            & " FROM TEMP_DESPVSISSUE TRN" & vbCrLf _
        ''            & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
        ''            & " AND CHILD_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        SqlStr = " SELECT RM_CODE ,FG_CODE ,STD_QTY,FG_LEVEL,DEPT_CODE " & vbCrLf & " From TEMP_DESPVSISSUE " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' " & vbCrLf & " START WITH RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " CONNECT BY PRIOR FG_CODE=RM_CODE AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsRM.EOF = False Then
            Do While RsRM.EOF = False

                mCheckRMCode = Trim(IIf(IsDbNull(RsRM.Fields("RM_CODE").Value), "", RsRM.Fields("RM_CODE").Value))
                If Trim(mCheckRMCode) = Trim(mItemCode) Then
                    mStdQty = 1
                End If
                mProductCode = Trim(IIf(IsDbNull(RsRM.Fields("FG_CODE").Value), "", RsRM.Fields("FG_CODE").Value))
                mStdQty = mStdQty * IIf(IsDbNull(RsRM.Fields("STD_QTY").Value), 0, RsRM.Fields("STD_QTY").Value)
                mDeptCode = Trim(IIf(IsDbNull(RsRM.Fields("DEPT_CODE").Value), "", RsRM.Fields("DEPT_CODE").Value))

                '            If MainClass.ValidateWithMasterTable(mCheckRMCode, "RM_CODE", "DEPT_CODE", "PRD_NEWBOM_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRODUCT_CODE='" & mProductCode & "' AND STATUS='O'") = True Then
                '                mDeptCode = MasterNo
                '            Else
                '                mDeptCode = ""
                '            End If
                If InStr(1, mString, mProductCode) = 0 Then
                    If mDeptCode = "J/W" Then
                        If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mItemUOM = MasterNo
                        End If

                        mWIPOPQty = mWIPOPQty + (GetBalanceStockQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), mItemUOM, "", "QC", "", ConWH, -1) * mStdQty)
                        mWIPCLQty = mWIPCLQty + (GetBalanceStockQty(mProductCode, (txtdateTo.Text), mItemUOM, "", "QC", "", ConWH, -1) * mStdQty)

                        mWIPOPQty = mWIPOPQty + (GetBalanceStockQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), mItemUOM, "", "ST", "", ConWH, -1) * mStdQty)
                        mWIPCLQty = mWIPCLQty + (GetBalanceStockQty(mProductCode, (txtdateTo.Text), mItemUOM, "", "ST", "", ConWH, -1) * mStdQty)

                        mWIPOPQty = mWIPOPQty + (GetBalanceStockQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), mItemUOM, "", "ST", "", ConPH, -1) * mStdQty)
                        mWIPCLQty = mWIPCLQty + (GetBalanceStockQty(mProductCode, (txtdateTo.Text), mItemUOM, "", "ST", "", ConPH, -1) * mStdQty)

                        mWIPOPQty = mWIPOPQty + (GetBalanceStockQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), mItemUOM, "", "FG", "", ConWH, -1) * mStdQty)
                        mWIPCLQty = mWIPCLQty + (GetBalanceStockQty(mProductCode, (txtdateTo.Text), mItemUOM, "", "FG", "", ConWH, -1) * mStdQty)

                        mWIPOPQty = mWIPOPQty + (GetBalanceStockQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), mItemUOM, "", "CR", "", ConWH, -1) * mStdQty)
                        mWIPCLQty = mWIPCLQty + (GetBalanceStockQty(mProductCode, (txtdateTo.Text), mItemUOM, "", "CR", "", ConWH, -1) * mStdQty)
                    Else
                        mWIPOPQty = mWIPOPQty + (GetWIPQty(mProductCode, ConPH, mDeptCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text)))) * mStdQty)
                        mWIPCLQty = mWIPCLQty + (GetWIPQty(mProductCode, ConPH, mDeptCode, (txtdateTo.Text)) * mStdQty)

                        mWIPOPQty = mWIPOPQty + (GetBalanceStockQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), mItemUOM, "", "FG", "", ConWH, -1) * mStdQty)
                        mWIPCLQty = mWIPCLQty + (GetBalanceStockQty(mProductCode, (txtdateTo.Text), mItemUOM, "", "FG", "", ConWH, -1) * mStdQty)

                        mWIPOPQty = mWIPOPQty + (GetBalanceStockQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), mItemUOM, "", "ST", "", ConWH, -1) * mStdQty)
                        mWIPCLQty = mWIPCLQty + (GetBalanceStockQty(mProductCode, (txtdateTo.Text), mItemUOM, "", "ST", "", ConWH, -1) * mStdQty)

                        mWIPOPQty = mWIPOPQty + (GetBalanceStockQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), mItemUOM, "", "QC", "", ConWH, -1) * mStdQty)
                        mWIPCLQty = mWIPCLQty + (GetBalanceStockQty(mProductCode, (txtdateTo.Text), mItemUOM, "", "QC", "", ConWH, -1) * mStdQty)

                        mWIPOPQty = mWIPOPQty + (GetBalanceStockQty(mProductCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateFrom.Text))), mItemUOM, "", "CR", "", ConWH, -1) * mStdQty)
                        mWIPCLQty = mWIPCLQty + (GetBalanceStockQty(mProductCode, (txtdateTo.Text), mItemUOM, "", "CR", "", ConWH, -1) * mStdQty)
                    End If

                    mWIPInQty = mWIPInQty + (GetPurchaseQty(mProductCode, mItemUOM) * mStdQty)

                    mWIPPhyOPQty = mWIPPhyOPQty + (GetPhysicalWIPQty(mProductCode, ConPH, mDeptCode, (txtDateFrom.Text)) * mStdQty)
                    mWIPPhyCLQty = mWIPPhyCLQty + (GetPhysicalWIPQty(mProductCode, ConPH, mDeptCode, (txtdateTo.Text)) * mStdQty)

                    mWIPOPQty = mWIPOPQty '* mStdQty
                    mWIPCLQty = mWIPCLQty '* mStdQty
                    mWIPPhyOPQty = mWIPPhyOPQty '* mStdQty
                    mWIPPhyCLQty = mWIPPhyCLQty '* mStdQty
                End If
                mString = mString & "," & mProductCode
                RsRM.MoveNext()

            Loop
        End If
        GetItemWIP = True
        Exit Function
LedgError:
        GetItemWIP = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetPhysicalWIPQtyold(ByRef pProductCode As String, ByRef mStockID As String, ByRef mDeptCode As String, ByRef pPhyDate As String) As Double
        'On Error GoTo LedgError
        'Dim SqlStr As String = ""
        'Dim RsTemp As ADODB.Recordset=Nothing
        'Dim RsDeptSeq As ADODB.Recordset=Nothing
        'Dim mDeptSeq As Long
        '
        'Dim xCheckDept As String
        '
        '
        '
        '    SqlStr = "SELECT SERIAL_NO FROM PRD_PRODSEQUENCE_DET" & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf _
        ''            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "' ORDER BY SERIAL_NO"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsDeptSeq, adLockReadOnly
        '
        '    If RsDeptSeq.EOF = False Then
        '        mDeptSeq = IIf(IsNull(RsDeptSeq!SERIAL_NO), 0, RsDeptSeq!SERIAL_NO)
        '    Else
        '        GetPhysicalWIPQty = 0
        '        Exit Function
        '    End If
        '
        '    SqlStr = "SELECT * FROM PRD_PRODSEQUENCE_DET" & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf _
        ''            & " AND SERIAL_NO >=" & mDeptSeq & " ORDER BY SERIAL_NO"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsDeptSeq, adLockReadOnly
        '
        '    If RsDeptSeq.EOF = False Then
        '        Do While RsDeptSeq.EOF = False
        '            xCheckDept = IIf(IsNull(RsDeptSeq!DEPT_CODE), "", RsDeptSeq!DEPT_CODE)
        '
        '            SqlStr = " SELECT SUM(ID.PHY_QTY * DECODE(ID.ITEM_IO,'I',1,-1)) AS ITEM_QTY " & vbCrLf _
        ''                     & " FROM INV_PHY_HDR IH, INV_PHY_DET ID" & vbCrLf _
        ''                     & " WHERE IH.AUTO_KEY_PHY=ID.AUTO_KEY_PHY" & vbCrLf _
        ''                     & " AND IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''                     & " AND IH.BOOKTYPE = '" & ConPH & "'" & vbCrLf _
        ''                     & " AND ID.ITEM_CODE = '" & MainClass.AllowSingleQuote(pProductCode) & "'"
        '
        '            SqlStr = SqlStr & vbCrLf & "AND IH.DEPT_CODE='" & xCheckDept & "'"
        '
        '            If Trim(xCheckDept) = Trim(mDeptCode) Then
        '                SqlStr = SqlStr & vbCrLf & " AND ID.STOCK_TYPE NOT IN ('WP')"
        '            Else
        '                SqlStr = SqlStr & vbCrLf & " AND ID.STOCK_TYPE NOT IN ('WR')"
        '            End If
        '
        '            SqlStr = SqlStr & vbCrLf & " AND IH.PHY_DATE = '" & VB6.Format(pPhyDate, "DD-MMM-YYYY") & "'"
        '
        '
        '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '            If RsTemp.EOF = False Then
        '                GetPhysicalWIPQty = GetPhysicalWIPQty + IIf(IsNull(RsTemp!ITEM_QTY), 0, RsTemp!ITEM_QTY)
        '            End If
        '
        '            RsDeptSeq.MoveNext
        '        Loop
        '    End If
        '    Exit Function
        'LedgError:
        '    ErrorMsg err.Description, err.Number, vbCritical
    End Function

    Private Function GetWIPQty(ByRef pProductCode As String, ByRef mStockID As String, ByRef mDeptCode As String, ByRef pDate As String) As Double

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


                '                If Trim(xCheckDept) = Trim(mDeptCode) Then
                SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & xCheckDept & "'"
                '                Else
                '                    SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='-1'"
                '                End If

                '                SqlStr = SqlStr & vbCrLf & " AND REF_TYPE = '" & ConStockRefType_PMEMODEPT & "'"
                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('WP')"
                SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"
                SqlStr = SqlStr & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                '                SqlStr = SqlStr & vbCrLf & " AND REF_DATE <= '" & VB6.Format(pToDate, "DD-MMM-YYYY") & "'"


                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    GetWIPQty = GetWIPQty + IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                End If

                RsDeptSeq.MoveNext()
            Loop
            '        If mIsOpening <> "P" Then
            '            GetWIPQty = GetWIPQty + GetOtherDeptWIP(pProductCode, mIsOpening, mTable)
            '        End If
        End If
        Exit Function
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetDespatchQty(ByRef pProductCode As String, Optional ByRef mISRMSale As Boolean = False, Optional ByRef pRefType As String = "") As Double

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

        '    If mISRMSale = True Then
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('CR')"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('RJ','CR')"
        '    End If

        If pRefType = "" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('" & ConStockRefType_DSP & "','" & ConStockRefType_RGP & "','" & ConStockRefType_NRG & "')"
        ElseIf pRefType = "D" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('" & ConStockRefType_DSP & "')"
        ElseIf pRefType = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('" & ConStockRefType_RGP & "','" & ConStockRefType_NRG & "')"
        End If

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

    Private Function GetScrapQty(ByRef pProductCode As String, ByRef mDateFrom As String, ByRef mDateTo As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mSameItemCode As String

        SqlStr = ""
        GetScrapQty = 0

        mTable = ConInventoryTable

        mSameItemCode = GetSameItemCode(pProductCode)
        If mSameItemCode = "" Then
            mSameItemCode = "('" & pProductCode & "')"
        Else
            mSameItemCode = "('" & pProductCode & "'," & mSameItemCode & ")"
        End If


        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,-1)) AS ITEM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='" & ConWH & "'" & vbCrLf & " AND ITEM_CODE IN " & mSameItemCode & ""


        SqlStr = SqlStr & vbCrLf & " AND ITEM_IO ='I'"
        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE ='SC'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(mDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetScrapQty = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
LedgError:
        GetScrapQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetProductionQty(ByRef pItemCode As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String


        SqlStr = ""
        GetProductionQty = 0

        mTable = ConInventoryTable

        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,-1)) AS ITEM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='" & ConPH & "'" & vbCrLf & " AND ITEM_CODE = '" & pItemCode & "'"


        SqlStr = SqlStr & vbCrLf & " AND ITEM_IO ='O'"

        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE = 'ST'"

        SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  ='" & ConStockRefType_PMEMODEPT & "'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetProductionQty = System.Math.Abs(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value))
        End If

        Exit Function
LedgError:
        GetProductionQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetProductionQtyOld(ByRef pProductCode As String, ByRef pDeptCode As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String
        Dim mSameItemCode As String

        SqlStr = ""
        GetProductionQtyOld = 0

        mTable = ConInventoryTable

        mSameItemCode = GetSameItemCode(pProductCode)
        If mSameItemCode = "" Then
            mSameItemCode = "('" & pProductCode & "')"
        Else
            mSameItemCode = "('" & pProductCode & "'," & mSameItemCode & ")"
        End If


        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,0)) AS ITEM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & "" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ITEM_CODE IN " & mSameItemCode & "" ''& vbCrLf |            & " AND "


        '    SqlStr = SqlStr & vbCrLf & " AND ITEM_IO ='I'"

        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE = 'ST'"

        If pDeptCode = "J/W" Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConWH & "'"
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('" & ConStockRefType_MRR & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConPH & "'"
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('" & ConStockRefType_PMEMODEPT & "')"
            SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE_FROM='" & pDeptCode & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetProductionQtyOld = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
LedgError:
        GetProductionQtyOld = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function GetPurchaseQty(ByRef pItemCode As String, ByRef pPackUnit As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mTableName As String
        Dim mMRRUOM As String

        GetPurchaseQty = 0

        SqlStr = " SELECT SUM(ID.APPROVED_QTY) AS APPROVED_QTY, ITEM_UOM " & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID " & vbCrLf & " WHERE IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_UOM"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalStock, ADODB.LockTypeEnum.adLockReadOnly)

        If RsBalStock.EOF = False Then
            Do While RsBalStock.EOF = False
                mMRRUOM = IIf(IsDbNull(RsBalStock.Fields("ITEM_UOM").Value), "", RsBalStock.Fields("ITEM_UOM").Value)
                If IsDbNull(RsBalStock.Fields(0).Value) Then
                    mBalQty = 0
                Else
                    mBalQty = RsBalStock.Fields(0).Value
                End If


                '            Set RsBalStock = Nothing

                If mBalQty <> 0 Then
                    RsTemp = Nothing

                    SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        mIssueUOM = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                        mPurchaseUOM = IIf(IsDbNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                        mFactor = IIf(IsDbNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)

                        If mMRRUOM <> mIssueUOM Then
                            If pPackUnit = mIssueUOM Then
                                mBalQty = mBalQty * mFactor
                            End If
                        End If
                        RsTemp = Nothing
                        '            RsTemp.Close
                    End If
                End If
                GetPurchaseQty = GetPurchaseQty + mBalQty
                RsBalStock.MoveNext()
            Loop
        Else
            mBalQty = 0
            GetPurchaseQty = 0
        End If


        Exit Function
ErrPart:
        GetPurchaseQty = 0
    End Function

    Private Function GetIssueQty(ByRef pItemCode As String, ByRef pPackUnit As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mTableName As String


        SqlStr = " SELECT SUM(ID.ISSUE_QTY) AS ISSUE_QTY" & vbCrLf & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID " & vbCrLf & " WHERE IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.ISSUE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.ISSUE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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

                If pPackUnit = mIssueUOM Then

                Else
                    mBalQty = mBalQty / mFactor
                End If

                RsTemp = Nothing
                '            RsTemp.Close
            End If
        End If

        GetIssueQty = mBalQty

        Exit Function
ErrPart:
        GetIssueQty = 0
    End Function


    Private Function GetSRNQty(ByRef pItemCode As String, ByRef pPackUnit As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mTableName As String


        SqlStr = " SELECT SUM(ID.RTN_QTY) AS ISSUE_QTY" & vbCrLf & " FROM INV_SRN_HDR IH, INV_SRN_DET ID " & vbCrLf & " WHERE IH.AUTO_KEY_SRN=ID.AUTO_KEY_SRN " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND ID.TO_STOCK_TYPE='ST' AND ID.SRN_STATUS='N'"

        SqlStr = SqlStr & vbCrLf & " AND IH.SRN_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.SRN_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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
        '            If pPackUnit = mIssueUOM Then
        '
        '            Else
        '                mBalQty = mBalQty / mFactor
        '            End If
        '
        '            Set RsTemp = Nothing
        ''            RsTemp.Close
        '        End If
        '    End If

        GetSRNQty = mBalQty

        Exit Function
ErrPart:
        GetSRNQty = 0
    End Function

    Private Function GetProductQty(ByRef pItemCode As String, ByRef pProductCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBalQty As Double

        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mTableName As String
        Dim mDeptCode As String

        SqlStr = " SELECT DEPT_CODE" & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf & " AND RM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mDeptCode = IIf(IsDbNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value)
        Else
            mDeptCode = ""
            GetProductQty = 0
            Exit Function
        End If


        SqlStr = " SELECT SUM(ID.PROD_QTY) AS PROD_QTY" & vbCrLf & " FROM PRD_PMEMODEPT_HDR IH, PRD_PMEMODEPT_DET ID " & vbCrLf & " WHERE IH.AUTO_KEY_REF=ID.AUTO_KEY_REF " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STOCK_TYPE='ST'"

        SqlStr = SqlStr & vbCrLf & " AND (GETFINALOPR(IH.COMPANY_CODE, IH.DEPT_CODE, ID.ITEM_CODE,ID.OPR_CODE)='Y' OR ID.OPR_CODE IS NULL)"
        SqlStr = SqlStr & vbCrLf & " AND IH.PROD_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.PROD_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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
        '            If pPackUnit = mIssueUOM Then
        '
        '            Else
        '                mBalQty = mBalQty / mFactor
        '            End If
        '
        '            Set RsTemp = Nothing
        ''            RsTemp.Close
        '        End If
        '    End If

        GetProductQty = mBalQty

        Exit Function
ErrPart:
        GetProductQty = 0
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

        '    SqlStr = " SELECT PRODUCT_CODE,RM_CODE," & vbCrLf _
        ''            & " (STD_QTY + GROSS_WT_SCRAP) AS STD_QTY " & vbCrLf _
        ''            & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _
        ''            & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
        ''            & " CONNECT BY PRIOR RM_CODE=PRODUCT_CODE"

        SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " '" & mItemCode & "', ID.RM_CODE, IH.PRODUCT_CODE, (STD_QTY + GROSS_WT_SCRAP)* DECODE(INVMST.ISSUE_UOM,'KGS',.001,DECODE(INVMST.ISSUE_UOM,'TON',.001*.001,1)) ,ID.DEPT_CODE, 1 " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.RM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND STATUS='O'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " '" & mItemCode & "',ID.ITEM_CODE, IH.PRODUCT_CODE, (ITEM_QTY + SCRAP_QTY),'J/W',  1 " & vbCrLf & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND STATUS='O'" ''

        PubDBCn.Execute(SqlStr)

        mLevel = 1

        For mLevel = 1 To 5
            SqlStr = " SELECT *  FROM TEMP_DESPVSISSUE " & vbCrLf & " WHERE FG_LEVEL=" & mLevel & " AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRM, ADODB.LockTypeEnum.adLockReadOnly)

            If RsRM.EOF = False Then
                Do While Not RsRM.EOF
                    xItemCode = IIf(IsDbNull(RsRM.Fields("FG_CODE").Value), "", RsRM.Fields("FG_CODE").Value)
                    xSTDQty = 1 'IIf(IsNull(RsRM!STD_QTY), 0, RsRM!STD_QTY)

                    SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " '" & mItemCode & "',ID.RM_CODE, IH.PRODUCT_CODE, (STD_QTY + GROSS_WT_SCRAP)* DECODE(INVMST.ISSUE_UOM,'KGS',.001,DECODE(INVMST.ISSUE_UOM,'TON',.001*.001,1)) * " & xSTDQty & ",ID.DEPT_CODE,  " & mLevel + 1 & " " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.RM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "' AND STATUS='O'" ''& vbCrLf |                        & " AND IH.WEF=("

                    '                SqlStr = SqlStr & vbCrLf & " SELECT MAX(WEF) AS WEF" & vbCrLf _
                    ''                        & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf _
                    ''                        & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    ''                        & " AND IH.MKEY=ID.MKEY" & vbCrLf _
                    ''                        & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'" & vbCrLf _
                    ''                        & " AND WEF<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"
                    '

                    PubDBCn.Execute(SqlStr)

                    SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " '" & mItemCode & "',ID.ITEM_CODE, IH.PRODUCT_CODE, (ITEM_QTY + SCRAP_QTY),'J/W',  " & mLevel + 1 & " " & vbCrLf & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "' AND STATUS='O'" '' AND STATUS='O'

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
    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
    End Sub

    Private Sub frmParamRMStm_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    Me.text = "Actual vs Issue (Material Wise)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamRMStm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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


        ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked
        ChkALL.Visible = False
        cmdsearch.Visible = False
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False

        chkAllItemCode.CheckState = System.Windows.Forms.CheckState.Checked
        txtItemcode.Enabled = False
        cmdSearchItemCode.Enabled = False

        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCategory.Enabled = False
        cmdSearchCategory.Enabled = False

        chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False

        cboShow.Items.Clear()
        cboShow.Items.Add("Only PO Item")
        cboShow.Items.Add("ALL")
        cboShow.SelectedIndex = 0

        cboItemShow.Items.Clear()
        cboItemShow.Items.Add("Active")
        cboItemShow.Items.Add("Inactive")
        cboItemShow.Items.Add("All")
        cboItemShow.SelectedIndex = 0

        optOption(0).Checked = True

        minuspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\minus1.bmp")
        pluspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\plus1.bmp")

        mIsGrouped = True

        Call PrintStatus(True)
        'Call FillPOCombo
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtdateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamRMStm_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamRMStm_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        '    SprdMain.Col = ColCons_Book
        '    mConsumption = Trim(SprdMain.Text)
        '
        '    If mConsumption > 0 Then
        '        frmParamWIPDetail.lblItemCode.text = mItemCode
        '        frmParamWIPDetail.TxtItemName = mItemCode & " - " & mItemDesc
        '        frmParamWIPDetail.lblItemUOM.text = mItemUOM
        '
        '        frmParamWIPDetail.txtFromDate.Text = VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")
        '        frmParamWIPDetail.txtToDate.Text = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        '        frmParamWIPDetail.txtConsumption = Val(mConsumption)
        '
        '        frmParamWIPDetail.Show 1
        '        frmParamWIPDetail.Form_Activate
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C' AND PRD_TYPE IN ('R','P')"

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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C' AND PRD_TYPE IN ('R','P')"

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
            cmdSearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdSearchCategory.Enabled = True
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C' AND PRD_TYPE IN ('R','P')"

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
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C' AND PRD_TYPE IN ('R','P')"
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

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemcode.Text)
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

        If txtItemcode.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemcode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtItemcode.Text = UCase(Trim(txtItemcode.Text))
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
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
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
            txtItemcode.Text = AcName
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
            txtItemcode.Text = MasterNo
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
            .MaxCols = ColDespatch
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

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

            .Col = ColInHouseCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInHouseCode, 7)

            .Col = ColInHouseDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInHouseDesc, 20)

            For cntCol = ColGrossWt To ColDespatch
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

        SqlStr = " SELECT IH.PRODUCT_CODE, INVMST_F.ITEM_SHORT_DESC FG_DESC, " & vbCrLf & " ID.RM_CODE, INVMST_R.ITEM_SHORT_DESC RM_DESC, INVMST_R.ISSUE_UOM, " & vbCrLf & " INVMST_R.PURCHASE_UOM, INVMST_R.UOM_FACTOR, ID.STD_QTY, ID.GROSS_WT_SCRAP " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, " & vbCrLf & " INV_ITEM_MST INVMST_F, INV_ITEM_MST INVMST_R, INV_GENERAL_MST CMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=INVMST_F.COMPANY_CODE" & vbCrLf & " AND IH.PRODUCT_CODE=INVMST_F.ITEM_CODE" & vbCrLf & " AND ID.COMPANY_CODE=INVMST_R.COMPANY_CODE" & vbCrLf & " AND ID.RM_CODE=INVMST_R.ITEM_CODE" & vbCrLf & " AND INVMST_R.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND INVMST_R.CATEGORY_CODE=CMST.GEN_CODE" & vbCrLf & " AND CMST.GEN_TYPE='C'" & vbCrLf & " AND STOCKTYPE<>'FG'"

        If optOption(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND CMST.PRD_TYPE = 'R'"
        Else
            SqlStr = SqlStr & vbCrLf & "AND CMST.PRD_TYPE = 'P'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.BOM_TYPE='P' AND IH.STATUS='O'"

        '    SqlStr = SqlStr & vbCrLf & "AND ID.GROSS_WT_SCRAP>0 "

        If chkAllItemCode.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemcode.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND INVMST_R.ITEM_CODE='" & MainClass.AllowSingleQuote(Trim(txtItemcode.Text)) & "'"
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
                SqlStr = SqlStr & vbCrLf & "AND INVMST_R.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        If cboItemShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND INVMST_R.ITEM_STATUS='A'"
        ElseIf cboItemShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND INVMST_R.ITEM_STATUS='I'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " ORDER BY CMST.GEN_DESC, ID.RM_CODE"

        MakeSQL = SqlStr

        Exit Function
LedgError:
        '    Resume
        MakeSQL = ""
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLNew() As String

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""
        Dim mSubCatCode As String

        '    SqlStr = " SELECT " & vbCrLf _
        ''            & " TRN.PRODUCT_CODE, TRN.RM_CODE, (TRN.STD_QTY + GROSS_WT_SCRAP) AS STD_QTY , DEPT_CODE,LEVEL " & vbCrLf _
        ''            & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _
        ''            & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE<>'J/W'"
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE<>'J/W' AND RM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
        ''            & " CONNECT BY TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE<>'J/W' AND PRIOR PRODUCT_CODE=RM_CODE"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly

        SqlStr = " SELECT TRN.PRODUCT_CODE, INVMST_F.ITEM_SHORT_DESC FG_DESC, " & vbCrLf & " TRN.RM_CODE, INVMST_R.ITEM_SHORT_DESC RM_DESC, INVMST_R.ISSUE_UOM," & vbCrLf & " INVMST_R.PURCHASE_UOM, INVMST_R.UOM_FACTOR, TRN.STD_QTY, TRN.GROSS_WT_SCRAP" & vbCrLf & " FROM VW_PRD_BOM_TRN TRN, " & vbCrLf & " INV_ITEM_MST INVMST_F, INV_ITEM_MST INVMST_R, INV_GENERAL_MST CMST" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=INVMST_F.COMPANY_CODE" & vbCrLf & " AND TRN.PRODUCT_CODE=INVMST_F.ITEM_CODE" & vbCrLf & " AND TRN.COMPANY_CODE=INVMST_R.COMPANY_CODE" & vbCrLf & " AND TRN.RM_CODE=INVMST_R.ITEM_CODE" & vbCrLf & " AND INVMST_R.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND INVMST_R.CATEGORY_CODE=CMST.GEN_CODE" & vbCrLf & " AND CMST.GEN_TYPE='C'" & vbCrLf & " AND STOCKTYPE<>'FG'"

        If optOption(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND CMST.PRD_TYPE = 'R'"
        Else
            SqlStr = SqlStr & vbCrLf & "AND CMST.PRD_TYPE = 'P'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND TRN.STATUS='O'"

        '    SqlStr = SqlStr & vbCrLf & "AND ID.GROSS_WT_SCRAP>0 "

        If chkAllItemCode.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemcode.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND INVMST_R.ITEM_CODE='" & MainClass.AllowSingleQuote(Trim(txtItemcode.Text)) & "'"
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
                SqlStr = SqlStr & vbCrLf & "AND INVMST_R.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        If cboItemShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND INVMST_R.ITEM_STATUS='A'"
        ElseIf cboItemShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND INVMST_R.ITEM_STATUS='I'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " ORDER BY CMST.GEN_DESC, ID.RM_CODE"

        MakeSQLNew = SqlStr

        Exit Function
LedgError:
        '    Resume
        MakeSQLNew = ""
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLAlter() As String

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""
        Dim mSubCatCode As String

        SqlStr = " SELECT IH.PRODUCT_CODE, INVMST_F.ITEM_SHORT_DESC FG_DESC, " & vbCrLf & " IA.ALTER_RM_CODE, INVMST_R.ITEM_SHORT_DESC RM_DESC, INVMST_R.ISSUE_UOM, " & vbCrLf & " INVMST_R.PURCHASE_UOM, INVMST_R.UOM_FACTOR, IA.ALTER_STD_QTY, IA.ALETRSCRAP " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, PRD_BOM_ALTER_DET IA," & vbCrLf & " INV_ITEM_MST INVMST_F, INV_ITEM_MST INVMST_R, INV_GENERAL_MST CMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.MKEY=IA.MKEY" & vbCrLf & " AND ID.RM_CODE=IA.MAINITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=INVMST_F.COMPANY_CODE" & vbCrLf & " AND IH.PRODUCT_CODE=INVMST_F.ITEM_CODE" & vbCrLf & " AND IA.COMPANY_CODE=INVMST_R.COMPANY_CODE" & vbCrLf & " AND IA.ALTER_RM_CODE=INVMST_R.ITEM_CODE" & vbCrLf & " AND INVMST_R.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND INVMST_R.CATEGORY_CODE=CMST.GEN_CODE" & vbCrLf & " AND CMST.GEN_TYPE='C'" & vbCrLf & " AND STOCKTYPE<>'FG'"

        If optOption(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND CMST.PRD_TYPE = 'R'"
        Else
            SqlStr = SqlStr & vbCrLf & "AND CMST.PRD_TYPE = 'P'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.BOM_TYPE='P' AND IH.STATUS='O'"

        '    SqlStr = SqlStr & vbCrLf & "AND ID.GROSS_WT_SCRAP>0 "

        If chkAllItemCode.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemcode.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND INVMST_R.ITEM_CODE='" & MainClass.AllowSingleQuote(Trim(txtItemcode.Text)) & "'"
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
                SqlStr = SqlStr & vbCrLf & "AND INVMST_R.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        If cboItemShow.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND INVMST_R.ITEM_STATUS='A'"
        ElseIf cboItemShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND INVMST_R.ITEM_STATUS='I'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " ORDER BY CMST.GEN_DESC, IA.ALTER_RM_CODE"

        MakeSQLAlter = SqlStr

        Exit Function
LedgError:
        '    Resume
        MakeSQLAlter = ""
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtdateTo.Text))) = False Then txtdateTo.Focus()


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
            If Trim(txtItemcode.Text) = "" Then
                MsgInformation("Invaild Item Code")
                txtItemcode.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtItemcode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Item Code")
                txtItemcode.Focus()
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
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtdateTo) = False Then
            txtdateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtdateTo.Text))) = False Then
            txtdateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
