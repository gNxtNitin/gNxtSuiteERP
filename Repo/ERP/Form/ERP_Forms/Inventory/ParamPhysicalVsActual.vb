Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamPhysicalVsActual
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection
    Private Const RowHeight As Short = 22

    Private Structure AlterItemArray
        Dim mAlterCode As String
    End Structure
    Private mAlterItemData() As AlterItemArray

    Private Const ColRMCode As Short = 1
    Private Const ColRMDesc As Short = 2
    Private Const ColUnit As Short = 3
    Private Const ColOPQty As Short = 4
    Private Const ColWIPOPQty As Short = 5
    Private Const ColPurQty As Short = 6
    Private Const ColInhouseQty As Short = 7
    Private Const ColJobworkerQty As Short = 8
    Private Const ColRJQty As Short = 9
    Private Const ColBOMQty As Short = 10
    Private Const ColScrapQty As Short = 11
    Private Const ColWIPScrapQty As Short = 12
    Private Const ColCRCLQty As Short = 13
    Private Const ColNetStdQty As Short = 14
    Private Const ColERPCLQty As Short = 15
    Private Const ColWIPQty As Short = 16
    Private Const ColPhyQty As Short = 17
    Private Const ColWIPPhyQty As Short = 18
    Private Const ColVarQty As Short = 19
    Private Const ColRate As Short = 20
    Private Const ColVarAmount As Short = 21
    Private Const ColPurchasePer As Short = 22
    '
    'Private Const ColINHouseQty = 14
    'Private Const ColRGPINQty = 15
    'Private Const ColRMDespQty = 16
    'Private Const ColRMRGPQty = 17
    'Private Const ColWIPOutQty = 18
    '
    'Private Const ColCROPQty = 19
    'Private Const ColCRRecdQty = 20
    'Private Const ColCRDespQty = 21
    '
    '
    ''Private Const ColCLQty = 23
    '
    '
    '
    '
    '
    'Private Const ColAdjQty = 26
    'Private Const ColWIPAdjQty = 27
    '


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim mActiveRow As Integer
    Dim mcntRow As Integer

    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image
    Dim mIsGrouped As Boolean

    'Dim mFixedCol As Integer
    '
    'Dim mMaxRow As Long
    'Dim mMaxCol As Long
    'Dim mColWidth As Single
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        MainClass.ClearGrid(SprdMain, RowHeight)
        FormatSprdMain(-1)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Call Show1()

        Call FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Call PrintStatus(True)
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function SQLQry() As String

        On Error GoTo LedgError
        Dim RsBudgetMain As ADODB.Recordset
        Dim SqlStr As String = ""
        'Dim mProdCode As String
        'Dim mProdName As String
        Dim mRMCode As String
        'Dim mCustName As String
        Dim mCheckProdCode As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String

        Dim mRMCatCode As String = ""
        Dim mRMCatCodeStr As String = ""
        Dim CntLst As Integer
        Dim mMaterialType As String

        '', WEF

        SqlStr = " SELECT DISTINCT INVMST.ITEM_CODE AS ITEM_CODE, INVMST.ITEM_SHORT_DESC AS RM_NAME, " & vbCrLf _
            & " INVMST.ISSUE_UOM, INVMST.UOM_FACTOR "

        SqlStr = SqlStr & vbCrLf _
            & " FROM INV_ITEM_MST INVMST, INV_GENERAL_MST GMAT" & vbCrLf _
            & " WHERE INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND INVMST.COMPANY_CODE=GMAT.COMPANY_CODE " & vbCrLf _
            & " AND INVMST.CATEGORY_CODE=GMAT.GEN_CODE "

        If cboClass.SelectedIndex <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND INVMST.ITEM_CLASS='" & VB.Left(cboClass.Text, 1) & "'"
        End If

        If chkAllBOP.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtBOPName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtBOPName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mRMCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(mRMCode) & "'"
            End If
        End If

        For CntLst = 0 To lstMaterialType.Items.Count - 1
            If lstMaterialType.GetItemChecked(CntLst) = True Then
                mMaterialType = VB6.GetItemString(lstMaterialType, CntLst)
                If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mRMCatCode = IIf(IsDbNull(MasterNo), "", MasterNo)
                End If
                mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
            End If
        Next

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND INVMST.CATEGORY_CODE IN " & mRMCatCodeStr & ""
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY INVMST.ITEM_CODE"

        SQLQry = SqlStr

        Exit Function
LedgError:
        '    Resume
        MsgInformation(Err.Description)
        SQLQry = ""
    End Function

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mRMCode As String
        Dim mRMName As String
        Dim mItemUOM As String = ""
        Dim mPurQty As Double
        Dim mPurchaseReturnQty As Double
        Dim mDespQty As Double
        Dim mINHouseQty As Double
        Dim mJobWorkerQty As Double
        Dim mVarQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mChildCode As String
        Dim mDate As String
        Dim mOpQty As Double
        Dim mRJQty As Double
        Dim mCLQty As Double
        Dim xSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAlterItemCodeStr As String
        Dim mUpperBound As Integer
        Dim I As Integer
        Dim mCurrRow As Integer
        Dim xDespSqlQry As String
        Dim mItemCodeStr As String
        Dim pTotalWIPOPQty As Double
        Dim pTotalWIPCLQty As Double
        Dim pTotalPhyWIPQty As Double
        Dim pStdBalQty As Double
        Dim pPhyQty As Double
        Dim mBackColor As Integer
        Dim mFactor As Double
        Dim mPurchaseRate As Double
        Dim mLandedCost As Double
        Dim pTotalScrapQty As Double
        Dim pTotalCRCLQty As Double

        SqlStr = SQLQry ''& vbCrLf & " UNION " & vbCrLf & SQLOUTQry & vbCrLf & " ORDER BY 1"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)
        mcntRow = 1

        SprdMain.MaxRows = 1
        With SprdMain
            If RsShow.EOF = False Then
                Do While Not RsShow.EOF
                    .Row = .MaxRows
                    mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value))
                    mRMName = IIf(IsDbNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)
                    mItemUOM = IIf(IsDbNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                    mFactor = IIf(IsDbNull(RsShow.Fields("UOM_FACTOR").Value) Or RsShow.Fields("UOM_FACTOR").Value = 0, 1, RsShow.Fields("UOM_FACTOR").Value)

                    xSqlStr = GetQueryForAlterItem(mRMCode)
                    MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

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
                    '                .Text = Trim(IIf(IsNull(RsShow!ITEM_CODE), "", RsShow!ITEM_CODE)) & mAlterItemCodeStr

                    mOpQty = GetStockQty(mRMCode, mItemUOM, "STR", "QC", ConWH, "OP") + GetStockQty(mRMCode, mItemUOM, "", "QC", ConPH, "OP") ''+ GetStockQty(mRMCode, mItemUOM, "", "RJ", ConWH, "OP")
                    mPurQty = GetNetPurchase(mRMCode, "P")
                    mPurchaseReturnQty = GetNetPurchase(mRMCode, "D")
                    mINHouseQty = GetNetPurchase(mRMCode, "I")
                    mJobWorkerQty = GetNetPurchase(mRMCode, "J")

                    mCLQty = GetStockQty(mRMCode, mItemUOM, "STR", "QC", ConWH, "CL") + GetStockQty(mRMCode, mItemUOM, "", "QC", ConPH, "CL")
                    pPhyQty = GetPhysicalQty(mRMCode, "", ConWH) + GetPhysicalQty(mRMCode, "", ConPH)
                    xDespSqlQry = DespatchSqlQry(mRMCode)

                    For I = 0 To mUpperBound
                        If mAlterItemData(I).mAlterCode <> "" Then
                            mOpQty = mOpQty + GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "STR", "QC", ConWH, "OP") + GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "", "QC", ConPH, "OP") ''+ GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "", "RJ", ConWH, "OP")
                            mPurQty = mPurQty + GetNetPurchase(mAlterItemData(I).mAlterCode, "P")

                            mPurchaseReturnQty = mPurchaseReturnQty + GetNetPurchase(mAlterItemData(I).mAlterCode, "D")
                            mINHouseQty = mINHouseQty + GetNetPurchase(mAlterItemData(I).mAlterCode, "I")
                            mJobWorkerQty = mJobWorkerQty + GetNetPurchase(mAlterItemData(I).mAlterCode, "J")


                            mCLQty = mCLQty + GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "STR", "QC", ConWH, "CL") + GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "", "QC", ConPH, "CL")
                            pPhyQty = pPhyQty + GetPhysicalQty(mAlterItemData(I).mAlterCode, "", ConWH) + GetPhysicalQty(mAlterItemData(I).mAlterCode, "", ConPH)
                            xDespSqlQry = xDespSqlQry & vbCrLf & " UNION " & vbCrLf & DespatchSqlQry(mAlterItemData(I).mAlterCode)
                        End If
                    Next
                    mItemCodeStr = Trim(IIf(IsDbNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)) & mAlterItemCodeStr
                    mCurrRow = .MaxRows
                    ''08-06-2012
                    mDespQty = GetDespatchQty(xDespSqlQry, mItemCodeStr, mRMName, mItemUOM, pTotalWIPOPQty, pTotalWIPCLQty, pTotalPhyWIPQty, pTotalScrapQty, pTotalCRCLQty)
                    mDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDate(0).Text)))

                    .Row = mCurrRow

                    .Col = ColRMCode
                    .Text = Trim(IIf(IsDbNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)) & mAlterItemCodeStr

                    .Col = ColRMDesc
                    .Text = IIf(IsDbNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)


                    .Col = ColUnit
                    .Text = IIf(IsDbNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                    mItemUOM = IIf(IsDbNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)

                    .Col = ColBOMQty
                    .Text = VB6.Format(System.Math.Abs(mDespQty), "0.00")

                    .Col = ColWIPOPQty
                    .Text = VB6.Format(System.Math.Abs(pTotalWIPOPQty), "0.00")

                    .Col = ColOPQty
                    .Text = VB6.Format(mOpQty, "0.00")

                    .Col = ColPurQty
                    .Text = VB6.Format(mPurQty, "0.00")

                    .Col = ColInhouseQty
                    .Text = VB6.Format(mINHouseQty, "0.00")

                    .Col = ColJobworkerQty
                    .Text = VB6.Format(mJobWorkerQty, "0.00")

                    .Col = ColRJQty
                    .Text = VB6.Format(mPurchaseReturnQty, "0.00")

                    .Col = ColScrapQty
                    .Text = VB6.Format(pTotalScrapQty, "0.00")

                    .Col = ColCRCLQty
                    .Text = VB6.Format(pTotalCRCLQty, "0.00")

                    pStdBalQty = mOpQty + pTotalWIPOPQty + mPurQty + mINHouseQty + mJobWorkerQty - mPurchaseReturnQty - mDespQty
                    mVarQty = pPhyQty + pTotalPhyWIPQty - pStdBalQty

                    '                .Col = ColCLQty
                    '                .Text = VB6.Format(pStdBalQty, "0.00")

                    .Col = ColNetStdQty
                    .Text = VB6.Format(pStdBalQty, "0.00")

                    .Col = ColERPCLQty
                    .Text = VB6.Format(mCLQty, "0.00")

                    .Col = ColWIPQty
                    .Text = VB6.Format(pTotalWIPCLQty, "0.00")

                    .Col = ColPhyQty
                    .Text = VB6.Format(pPhyQty, "0.00")

                    .Col = ColWIPPhyQty
                    .Text = VB6.Format(pTotalPhyWIPQty, "0.00")

                    .Col = ColVarQty
                    .Text = VB6.Format(mVarQty, "0.00")

                    '                If GetLatestItemCostFromPO(mRMCode, mPurchaseRate, mLandedCost, VB6.Format(txtDate(1).Text, "DD/MM/YYYY"), "ST", "", mItemUOM, mFactor) = False Then GoTo LedgError


                    mAmount = GetLatestItemCostFromMRR(mRMCode, mItemUOM, System.Math.Abs(IIf(mCLQty = 0, 1, mCLQty)), VB6.Format(txtDate(1).Text, "DD/MM/YYYY"), "L", "ST")

                    mRate = mAmount / System.Math.Abs(IIf(mCLQty = 0, 1, mCLQty))

                    mAmount = mRate * mVarQty

                    .Col = ColRate
                    .Text = VB6.Format(mRate, "0.00")

                    .Col = ColVarAmount
                    .Text = VB6.Format(mAmount, "0.00")

                    .Col = ColPurchasePer
                    If mDespQty <> 0 Then
                        .Text = CStr(System.Math.Round((mPurQty) * 100 / mDespQty, 0))
                    Else
                        .Text = "0"
                    End If


                    '                mBackColor = IIf(mBackColor = &H8000000F, &H80FF80, &H8000000F)
                    ''                mBackColor = &H8000000F
                    '                .Row = .MaxRows
                    '                .Row2 = .MaxRows
                    '                .Col = 1
                    '                .col2 = .MaxCols
                    '                .BlockMode = True
                    '                .BackColor = mBackColor           ''&H80FF80
                    '                .BlockMode = False

                    RsShow.MoveNext()

                    If RsShow.EOF = False Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop
            End If
        End With

        RsShow = Nothing
        Show1 = True
        Exit Function
LedgError:
        '    Resume
        MsgInformation(Err.Description)
        Show1 = False
    End Function

    Private Function GetPhysicalAdjQty(ByRef mItemCode As String, ByRef mDeptCode As String, ByRef pStockID As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDeptSeq As Integer
        Dim mMaxDepSeq As Integer
        Dim pDeptCode As String
        Dim pDeptCodeStr As String = ""
        Dim I As Integer

        If pStockID = ConPH Then
            mDeptSeq = GetProductSeqNo(mItemCode, mDeptCode, txtDate(1).Text)
            mMaxDepSeq = GetMaxProductSeqNo(mItemCode, txtDate(1).Text)

            For I = mDeptSeq To mMaxDepSeq
                pDeptCode = GetProductDept(mItemCode, I, txtDate(1).Text)
                If pDeptCodeStr = "" Then
                    pDeptCodeStr = pDeptCodeStr & "('" & pDeptCode & "'"
                Else
                    pDeptCodeStr = pDeptCodeStr & ", '" & pDeptCode & "'"
                End If
            Next

            pDeptCodeStr = pDeptCodeStr & ")"
        End If

        SqlStr = " SELECT SUM(ADJ_QTY * DECODE(ITEM_IO,'I',1,-1)) AS ADJ_QTY " & vbCrLf & " FROM INV_ADJ_HDR IH, INV_ADJ_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_ADJ=ID.AUTO_KEY_ADJ" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND IH.ADJ_DATE=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND ID.STOCK_TYPE='ST' AND UPD_STOCK='Y'"

        SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & pStockID & "'"

        If pStockID = ConPH Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE IN " & pDeptCodeStr & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPhysicalAdjQty = IIf(IsDbNull(RsTemp.Fields("ADJ_QTY").Value), 0, RsTemp.Fields("ADJ_QTY").Value)
        End If

        Exit Function
ErrPart:
        GetPhysicalAdjQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub frmParamPhysicalVsActual_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11535, 769)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub txtCategory_Change()
        Call PrintStatus(False)
    End Sub
    Private Sub txtBOPName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBOPName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtBOPName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBOPName.DoubleClick
        SearchBOP()
    End Sub
    Private Sub txtBOPName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBOPName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBOPName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBOPName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBOPName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchBOP()
    End Sub
    Private Sub txtBOPName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBOPName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If txtBOPName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable((txtBOPName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtBOPName.Text = UCase(Trim(txtBOPName.Text))
        Else
            MsgInformation("No Such Item in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchBOP()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtBOPName.Text), "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtBOPName.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdsearchBOPName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchBOPName.Click
        SearchBOP()
    End Sub
    Private Sub chkAllBOP_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllBOP.CheckStateChanged
        Call PrintStatus(False)
        If chkAllBOP.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtBOPName.Enabled = False
            cmdsearchBOPName.Enabled = False
        Else
            txtBOPName.Enabled = True
            cmdsearchBOPName.Enabled = True
        End If
    End Sub






    Public Sub frmParamPhysicalVsActual_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Physical Vs Actual Report"
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamPhysicalVsActual_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = VB6.TwipsToPixelsY(24)
        Me.Left = VB6.TwipsToPixelsX(24)
        'Me.Height = VB6.TwipsToPixelsY(7440)
        ''Me.Width = VB6.TwipsToPixelsX(11625)


        '    txtDateFrom.Text = VB6.Format(RsCompany!START_DATE, "DD/MM/YYYY")
        '    txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        txtDate(0).Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
        txtDate(1).Text = VB6.Format(RunDate, "DD/MM/YYYY")


        chkAllBOP.CheckState = System.Windows.Forms.CheckState.Checked

        txtBOPName.Enabled = False
        cmdsearchBOPName.Enabled = False

        Call FillMaterialType()


        minuspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\minus1.bmp")
        pluspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\plus1.bmp")

        mIsGrouped = True

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain(ByRef mRow As Integer)

        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColPurchasePer ''ColTotalAmount
            .set_RowHeight(-1, RowHeight)

            .Row = -1
            .set_ColWidth(0, 4)

            '        .Col = ColPicMain
            '        .CellType = CellTypePicture
            '        .TypePictCenter = True
            '        .TypePictMaintainScale = False
            '        .TypePictStretch = False
            '
            '        .Col = ColPicSub
            '        .CellType = CellTypePicture
            '        .TypePictCenter = True
            '        .TypePictMaintainScale = False
            '        .TypePictStretch = False
            '        .ColHidden = True

            '        .Col = ColCustomerCode
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '        .TypeEditMultiLine = True
            '        .ColHidden = True

            '        .Col = ColCustomerName
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '        .TypeEditMultiLine = True
            '        .ColWidth(ColCustomerName) = 15
            '        .ColHidden = True


            .Col = ColRMCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColRMCode, 6)

            .Col = ColRMDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColRMDesc, 25)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            For cntCol = ColOPQty To ColPurchasePer '' ColTotalAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 9)
            Next

            .ColsFrozen = ColRMDesc

            '        .Col = ColRGPINQty
            '        .ColHidden = True

            Call FillHeading()

            mIsGrouped = False
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle

        End With
    End Sub

    Private Sub FillMaterialType()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        lstMaterialType.Items.Clear()
        SqlStr = "SELECT GEN_DESC FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C' ORDER BY GEN_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            Do While RS.EOF = False
                lstMaterialType.Items.Add(RS.Fields("GEN_DESC").Value)
                lstMaterialType.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstMaterialType.SelectedIndex = 0

        cboClass.Items.Clear()
        cboClass.Items.Add("ALL")
        cboClass.Items.Add("A")
        cboClass.Items.Add("B")
        cboClass.Items.Add("C")
        cboClass.Items.Add("DOL")
        cboClass.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub FillHeading()
        On Error GoTo ErrPart

        With SprdMain
            .MaxCols = ColPurchasePer ''ColTotalAmount

            .ColHeaderRows = 2
            '        .AddCellSpan 1, SpreadHeader, 3, 1
            '        .AddCellSpan 4, SpreadHeader, 3, 1

            .Row = FPSpreadADO.CoordConstants.SpreadHeader

            .Col = ColRMCode
            .Text = "Material Code"

            .Col = ColRMDesc
            .Text = "Material Desc"

            .Col = ColUnit
            .Text = "Unit"

            .Col = ColBOMQty
            .Text = "Despatch As Per BOM Qty"

            .Col = ColOPQty
            .Text = "Book Opening Qty"

            .Col = ColPurQty
            .Text = "Purchase Qty"

            .Col = ColNetStdQty
            .Text = "Standard Closing Qty"

            .Col = ColERPCLQty
            .Text = "Actual Closing Qty"

            .Col = ColWIPOPQty
            .Text = "WIP Opening Qty"

            .Col = ColWIPQty
            .Text = "WIP Closing Qty"

            .Col = ColCRCLQty
            .Text = "CR Closing Qty"

            .Col = ColVarQty
            .Text = "Variation Qty"

            .Col = ColPhyQty
            .Text = "Physical Qty"

            .Col = ColWIPPhyQty
            .Text = "WIP Physical Qty"

            .Col = ColRate
            .Text = "Rate"

            .Col = ColVarAmount
            .Text = "Variation Amount"

            .Col = ColPurchasePer
            .Text = "Purchase / Despatch %"

            .Col = ColScrapQty
            .Text = "Scrap"


            ''Second Header Row

            '        .Row = SpreadHeader + 1
            '
            '        .Col = ColCustomerCode
            '        .Text = ""
            ''
            '        .Col = ColRMCode
            '        .Text = "A"
            '
            '        .Col = ColRMDesc
            '        .Text = "B"
            '
            '        .Col = ColUnit
            '        .Text = "C"
            '
            '        .Col = ColBOMQty
            '        .Text = "I=F x H"
            '
            '        .Col = ColOPQty
            '        .Text = "K"
            '
            '        .Col = ColPurQty
            '        .Text = "L"
            '
            '        .Col = ColINHouseQty
            '        .Text = "M"
            '
            '        .Col = ColRGPINQty
            '        .Text = "N"
            '
            '        .Col = ColRMDespQty
            '        .Text = "O"
            '
            '        .Col = ColRMRGPQty
            '        .Text = "P"
            '
            '        .Col = ColWIPOutQty
            '        .Text = "Q"
            '
            ''        .Col = ColRGPOUTQty
            ''        .Text = "RGP Out Qty"
            '
            ''        .Col = ColCLQty
            ''        .Text = "Standard Closing Qty"
            ''
            '        .Col = ColNetStdQty
            '        .Text = "V = J+K+L+M+N-I-O-P-Q"
            '
            ''        .Col = ColConsQty
            ''        .Text = "Actual Consumption Qty"
            '
            '        .Col = ColERPCLQty
            '        .Text = "W"
            '
            '        .Col = ColWIPOPQty
            '        .Text = "J"
            '
            '        .Col = ColWIPQty
            '        .Text = "X"
            '
            '
            '        .Col = ColCROPQty
            '        .Text = "R"
            '
            '        .Col = ColCRRecdQty
            '        .Text = "S"
            '
            '        .Col = ColCRDespQty
            '        .Text = "T"
            '
            '        .Col = ColCRCLQty
            '        .Text = "U"
            '
            '        .Col = ColAdjQty
            '        .Text = "Y"
            '
            '        .Col = ColWIPAdjQty
            '        .Text = "Z"
            '
            '        .Col = ColScrapQty
            '        .Text = "AA"
            '
            '        .Col = ColPhyQty
            '        .Text = "AB"
            '
            '        .Col = ColWIPPhyQty
            '        .Text = "AC"
            '
            '        .Col = ColVarQty
            '        .Text = "AD = V- AB - AC"
            '
            '        .Col = ColRate
            '        .Text = "AE"
            '
            '        .Col = ColVarAmount
            '        .Text = "AF"
            '
            '        .Col = ColPurchaseAmount
            '        .Text = "AG=L x AE"
            '
            '        .Col = ColPurchasePer
            '        .Text = "AH = L/I"
        End With
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamPhysicalVsActual_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnStock(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnStock(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""


        Report1.Reset()

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1

        '*************** Fetching Record For Report ***************************
        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mTitle = "Productwise Stock Statement"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\PhyVsAct.rpt"

        '    mSubTitle = "As On Date : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")



        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Exit Sub



ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        'Dim ii As Integer

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnStock(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtFGName_Change()
        PrintStatus(False)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Sort on specified column or show/collapse rows

        'Show Summary/Detail info.
        'If clicked on a "+" or "-" grouping

        '    If Col = ColPicMain Then
        '        SprdMain.Col = ColPicMain
        '        SprdMain.Row=eventArgs.Row
        '        If SprdMain.CellType = CellTypePicture Then
        '            'Show or hide the specified rows
        '            ShowHideRows ColPicMain, Row
        '        End If
        '    End If
    End Sub
    Private Sub ShowHideRows(ByRef Col As Integer, ByRef Row As Integer)
        'Collapse or uncollape the specified rows
        Dim I As Short
        Dim collapsetype As Short

        SprdMain.Row = Row
        '    SprdMain.Col = ColFlag

        '    If SprdMain.Text = "0" Then
        '        collapsetype = 0  'collape/hide rows : minus picture
        '        SprdMain.Col = 1
        '        SprdMain.TypePictPicture = pluspict
        '        SprdMain.Col = ColFlag
        '        SprdMain.Text = "1"
        '    Else
        '        collapsetype = 1  'uncollapse / show rows: plus picture
        '        SprdMain.Col = 1
        '        SprdMain.TypePictPicture = minuspict
        '        SprdMain.Col = ColFlag
        '        SprdMain.Text = "0"
        '    End If

        SprdMain.ReDraw = False
        For I = 1 To SprdMain.GetRowItemData(Row)
            SprdMain.Row = SprdMain.Row + 1
            If collapsetype = 0 Then
                SprdMain.RowHidden = True
            Else
                SprdMain.RowHidden = False
            End If
        Next I
        SprdMain.ReDraw = True

    End Sub
    Private Function GetStockQty(ByRef pItemCode As String, ByRef pPackUnit As String, ByRef pDeptCode As String, ByRef pStockType As String, ByRef pStock_ID As String, ByRef xShowType As String, Optional ByRef pRefType As String = "", Optional ByRef pIO As String = "") As Double


        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsDept As ADODB.Recordset
        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mTableName As String
        Dim mDeptCode As String

        mDeptCode = ""

        SqlStr = ""

        If pIO = "I" Then
            SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,0)) AS BALQTY"
        ElseIf pIO = "O" Then
            SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',0,-1)) AS BALQTY"
        Else
            SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"
        End If

        mTableName = ConInventoryTable

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If pDeptCode <> "" And pStock_ID = ConPH Then
            SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & pDeptCode & "'"
        ElseIf pDeptCode = "PAD" And pStock_ID = ConWH And pStockType = "FG" Then
            ''02-08-2006
            'SqlStr = SqlStr & vbCrLf & "AND (DEPT_CODE_FROM='" & pDeptCode & "' OR DEPT_CODE_TO='" & pDeptCode & "')"
        End If

        If pRefType <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN (" & pRefType & ")"
        End If

        'If pStockType = "QC" Then
        '    If xShowType = "OP" Or xShowType = "CL" Then
        '        'SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE <>'CR'"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE IN ('ST','" & pStockType & "')"
        '    End If
        'Else
        '    If pStockType = "" Then
        '        '            SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "')"
        '        'SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE <>'CR'"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & pStockType & "'" '' AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "')"
        '    End If
        'End If

        '    SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format((pDateTo), "DD-MMM-YYYY") & "')"

        If xShowType = "OP" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf xShowType = "CL" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
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

        GetStockQty = mBalQty

        Exit Function
ErrPart:
        GetStockQty = 0
    End Function



    Private Function GetNetPurchase(ByRef pItemCode As String, ByRef pType As String) As Double


        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsDept As ADODB.Recordset
        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mTableName As String
        Dim mDeptCode As String

        mDeptCode = ""
        SqlStr = ""
        mTableName = ConInventoryTable
        GetNetPurchase = 0

        SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf _
            & " FROM " & mTableName & " " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND STOCK_ID='WH'" & vbCrLf _
            & " AND STATUS='O'" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If pType = "P" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('MRR') AND REF_FLAG<>'R' AND STOCK_TYPE<>'SC'"
        ElseIf pType = "D" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('DSP','RGP','NRG') AND STOCK_TYPE<>'SC'"
        ElseIf pType = "I" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('PMD') AND STOCK_TYPE<>'SC'"
        ElseIf pType = "J" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('MRR') AND REF_FLAG='R' AND STOCK_TYPE<>'SC'"
        ElseIf pType = "S" Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='SC'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        'SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf _
        '    & " SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',-1,0)) AS BALQTY" & vbCrLf _
        '    & " FROM " & mTableName & " " & vbCrLf _
        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='WH'" & vbCrLf _
        '    & " AND STATUS='O'" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
        '    & " AND REF_TYPE  ='SRN'" & vbCrLf _
        '    & " AND STOCK_TYPE = 'RJ' AND ITEM_IO='I'" & vbCrLf _
        '    & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        '    & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        'SqlStr = SqlStr & vbCrLf _
        '    & " UNION " & vbCrLf & " SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,0)) AS BALQTY" & vbCrLf _
        '    & " FROM " & mTableName & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK_ID='WH'" & vbCrLf _
        '    & " AND STATUS='O'" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
        '    & " AND REF_TYPE  ='PIS'" & vbCrLf & " AND STOCK_TYPE = 'ST' AND ITEM_IO='I'" & vbCrLf _
        '    & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        '    & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalStock, ADODB.LockTypeEnum.adLockReadOnly)


        If RsBalStock.EOF = False Then
            Do While RsBalStock.EOF = False
                mBalQty = 0
                If IsDBNull(RsBalStock.Fields(0).Value) Then
                    mBalQty = 0
                Else
                    mBalQty = RsBalStock.Fields(0).Value
                End If
                GetNetPurchase = GetNetPurchase + mBalQty
                RsBalStock.MoveNext()
            Loop
        Else
            mBalQty = 0
        End If


        RsBalStock = Nothing

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

        '    Set RsTemp = Nothing

        Exit Function
ErrPart:
        GetNetPurchase = 0
    End Function

    Private Function GetRMSaleQty(ByRef pItemCode As String) As Double


        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPurchaseReturn As Double


        SqlStr = ""

        SqlStr = "SELECT ABS(SUM(ID.PACKED_QTY * DECODE(TRIM(ID.ITEM_UOM),TRIM(INVMST.PURCHASE_UOM),INVMST.UOM_FACTOR,1))) AS INQTY"
        SqlStr = SqlStr & vbCrLf & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE AND IH.DESP_TYPE<>'U'" & vbCrLf & " AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"


        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields(0).Value) Then
                mPurchaseReturn = 0
            Else
                mPurchaseReturn = RsTemp.Fields(0).Value
            End If
        Else
            mPurchaseReturn = 0
        End If

        GetRMSaleQty = mPurchaseReturn
        RsTemp = Nothing

        Exit Function
ErrPart:
        GetRMSaleQty = 0
    End Function


    Private Function GetQueryForAlterItem(ByRef pItemCode As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT DISTINCT TRIM(ALTER_RM_CODE) AS ALTER_RM_CODE" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.STATUS='O'" & vbCrLf & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " UNION "

        '    SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT MAINITEM_CODE " & vbCrLf _
        ''            & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf _
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
        ''            & " AND IH.STATUS='O'" & vbCrLf _
        ''            & " AND ID.ALTER_RM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        '
        '    SqlStr = SqlStr & vbCrLf & " UNION "

        SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT TRIM(MAINITEM_CODE) AS ALTER_RM_CODE" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.STATUS='O'" & vbCrLf & " AND ID.ALTER_RM_CODE IN (" & vbCrLf & " SELECT DISTINCT ALTER_RM_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.STATUS='O'" & vbCrLf & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "')"

        SqlStr = SqlStr & vbCrLf & " MINUS SELECT '" & Trim(pItemCode) & "' AS ALTER_RM_CODE FROM DUAL "
        GetQueryForAlterItem = SqlStr

        Exit Function
ErrPart:
        GetQueryForAlterItem = ""
    End Function
    Private Function GetNetDespatch(ByRef pItemCode As String) As Double


        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSaleReturn As Double

        SqlStr = ""

        SqlStr = "SELECT ABS(SUM(ID.PACKED_QTY * DECODE(TRIM(ID.ITEM_UOM),TRIM(INVMST.PURCHASE_UOM),INVMST.UOM_FACTOR,1))) AS INQTY"
        SqlStr = SqlStr & vbCrLf & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUBSTR(IH.AUTO_KEY_DESP,LENGTH(IH.AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE AND IH.DESP_TYPE<>'U'" & vbCrLf & " AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND ID.STOCK_TYPE<>'CR'" ''


        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields(0).Value) Then
                GetNetDespatch = 0
            Else
                GetNetDespatch = RsTemp.Fields(0).Value
            End If
        Else
            GetNetDespatch = 0
        End If


        SqlStr = "SELECT SUM(ID.RECEIVED_QTY * DECODE(TRIM(ID.ITEM_UOM),TRIM(INVMST.PURCHASE_UOM),INVMST.UOM_FACTOR,1)) AS INQTY"
        SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND ID.STOCK_TYPE<>'CR' AND IH.REF_TYPE IN ('2','I')"


        SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields(0).Value) Then
                mSaleReturn = 0
            Else
                mSaleReturn = RsTemp.Fields(0).Value
            End If
        Else
            mSaleReturn = 0
        End If

        GetNetDespatch = GetNetDespatch - mSaleReturn
        RsTemp = Nothing

        Exit Function
ErrPart:
        GetNetDespatch = 0
    End Function
    Private Function GetDespatchQty(ByRef pQry As String, ByRef xItemCode As String, ByRef xItemDesc As String, ByRef xItemUOM As String, ByRef pTotalWIPOPQty As Double, ByRef pTotalWIPCLQty As Double, ByRef pTotalPhyWIPQty As Double, ByRef pTotalScrapQty As Double, ByRef pTotalCRCLQty As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mParentcode As String
        Dim mChildCode As String
        Dim mStdQty As Double
        Dim mLevel As Integer
        Dim mDespQty As Double
        Dim pItemUOM As String = ""
        Dim mSqlStrRel As String
        Dim RsRel As ADODB.Recordset = Nothing
        Dim xProductRelCode As String
        Dim pProductDesc As String


        Dim mDeptCode As String
        Dim mOPWIPQty As Double
        Dim mCLWIPQty As Double
        Dim mWIPPhyQty As Double
        Dim mScrapQty As Double
        Dim mCRCLQty As Double

        GetDespatchQty = 0
        pTotalWIPOPQty = 0
        pTotalWIPCLQty = 0
        pTotalPhyWIPQty = 0
        pTotalScrapQty = 0
        pTotalCRCLQty = 0


        MainClass.UOpenRecordSet(pQry, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mStdQty = 1
        Dim mItemLevelStdQty(1000) As Object
        '    mIsFirstRecord = True
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF

                mLevel = Val(IIf(IsDbNull(RsTemp.Fields("Level").Value), 1, RsTemp.Fields("Level").Value))

                If mLevel = 1 Then
                    mStdQty = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), "", RsTemp.Fields("STD_QTY").Value), "0.0000"))
                Else
                    mStdQty = mItemLevelStdQty(mLevel - 1) * CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), "", RsTemp.Fields("STD_QTY").Value), "0.0000"))
                End If
                mItemLevelStdQty(mLevel) = mStdQty


                mParentcode = Trim(IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                mDeptCode = Trim(IIf(IsDbNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value))

                If MainClass.ValidateWithMasterTable(mParentcode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    pProductDesc = Trim(MasterNo)
                End If

                If MainClass.ValidateWithMasterTable(mParentcode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    pItemUOM = Trim(MasterNo)
                End If


                mDespQty = GetNetDespatch(mParentcode)
                GetDespatchQty = GetDespatchQty + (mDespQty * mStdQty)


                mOPWIPQty = GetWIPProductionQty(mParentcode, pItemUOM, mDeptCode, "OP")
                pTotalWIPOPQty = pTotalWIPOPQty + (mOPWIPQty * mStdQty)

                mCLWIPQty = GetWIPProductionQty(mParentcode, pItemUOM, mDeptCode, "CL")
                pTotalWIPCLQty = pTotalWIPCLQty + (mCLWIPQty * mStdQty)

                mWIPPhyQty = GetPhysicalQty(mParentcode, mDeptCode, ConPH)
                mWIPPhyQty = mWIPPhyQty + GetPhysicalQty(mParentcode, "", ConWH)
                pTotalPhyWIPQty = pTotalPhyWIPQty + (mWIPPhyQty * mStdQty)

                mCRCLQty = GetWIPProductionQty(mParentcode, pItemUOM, mDeptCode, "CL_CR")
                pTotalCRCLQty = pTotalCRCLQty + (mCRCLQty * mStdQty)

                '            mScrapQty = Abs(GetWIPProductionQty(mParentcode, pItemUOM, mDeptCode, "SCP"))
                '            pTotalScrapQty = pTotalScrapQty + (mScrapQty * mStdQty)


                mSqlStrRel = GetRelationItem(mParentcode)
                If mSqlStrRel <> "" Then
                    MainClass.UOpenRecordSet(mSqlStrRel, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRel, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsRel.EOF = False Then
                        Do While RsRel.EOF = False
                            xProductRelCode = Trim(IIf(IsDbNull(RsRel.Fields("REF_ITEM_CODE").Value), "", RsRel.Fields("REF_ITEM_CODE").Value))
                            mDespQty = GetNetDespatch(xProductRelCode)
                            GetDespatchQty = GetDespatchQty + (mDespQty * mStdQty)

                            mOPWIPQty = GetWIPProductionQty(xProductRelCode, pItemUOM, mDeptCode, "OP")
                            pTotalWIPOPQty = pTotalWIPOPQty + (mOPWIPQty * mStdQty)

                            mCLWIPQty = GetWIPProductionQty(xProductRelCode, pItemUOM, mDeptCode, "CL")
                            pTotalWIPCLQty = pTotalWIPCLQty + (mCLWIPQty * mStdQty)

                            mWIPPhyQty = GetPhysicalQty(xProductRelCode, mDeptCode, ConPH)
                            mWIPPhyQty = mWIPPhyQty + GetPhysicalQty(xProductRelCode, "", ConWH)
                            mWIPPhyQty = mWIPPhyQty + GetStockQty(xProductRelCode, pItemUOM, "STR", "FG", ConWH, "CL")


                            mCRCLQty = GetWIPProductionQty(xProductRelCode, pItemUOM, mDeptCode, "CL_CR")
                            pTotalCRCLQty = pTotalCRCLQty + (mCRCLQty * mStdQty)

                            '                        mScrapQty = Abs(GetWIPProductionQty(xProductRelCode, pItemUOM, mDeptCode, "SCP"))
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
        GetDespatchQty = 0
    End Function

    Private Function GetPhysicalQty(ByRef mProductCode As String, ByRef mDeptCode As String, ByRef pStockID As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDeptSeq As Integer
        Dim mMaxDepSeq As Integer
        Dim pDeptCode As String
        Dim pDeptCodeStr As String = ""
        Dim I As Integer

        If pStockID = ConPH Then
            If mDeptCode = "" Then
                pDeptCodeStr = ""
            Else
                mDeptSeq = GetProductSeqNo(mProductCode, mDeptCode, txtDate(1).Text)
                mMaxDepSeq = GetMaxProductSeqNo(mProductCode, txtDate(1).Text)

                For I = mDeptSeq To mMaxDepSeq
                    pDeptCode = GetProductDept(mProductCode, I, txtDate(1).Text)
                    If pDeptCodeStr = "" Then
                        pDeptCodeStr = pDeptCodeStr & "('" & pDeptCode & "'"
                    Else
                        pDeptCodeStr = pDeptCodeStr & ", '" & pDeptCode & "'"
                    End If
                Next
                pDeptCodeStr = pDeptCodeStr & ")"
            End If
        End If

        SqlStr = " SELECT SUM(PHY_QTY) AS PHY_QTY " & vbCrLf & " FROM INV_PHY_HDR IH, INV_PHY_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_PHY=ID.AUTO_KEY_PHY" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND IH.PHY_DATE=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If pStockID = ConPH Then
            '        SqlStr = SqlStr & vbCrLf & " AND ID.STOCK_TYPE='ST'"
        Else
            '        SqlStr = SqlStr & vbCrLf & " AND ID.STOCK_TYPE IN ('FG','ST')"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & pStockID & "'"

        If pStockID = ConPH And pDeptCodeStr <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE IN " & pDeptCodeStr & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPhysicalQty = IIf(IsDbNull(RsTemp.Fields("PHY_QTY").Value), 0, RsTemp.Fields("PHY_QTY").Value)
        End If

        Exit Function
ErrPart:
        GetPhysicalQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetWIPProductionQty(ByRef mProductCode As String, ByRef mItemUOM As String, ByRef mDeptCode As String, ByRef mFieldName As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDeptSeq As Integer
        Dim mMaxDepSeq As Integer
        Dim pDeptCode As String
        Dim I As Integer


        mDeptSeq = GetProductSeqNo(mProductCode, mDeptCode, txtDate(1).Text)
        mMaxDepSeq = GetMaxProductSeqNo(mProductCode, txtDate(1).Text)
        GetWIPProductionQty = 0

        If mFieldName = "ADJ" Then
            For I = mDeptSeq To mMaxDepSeq
                pDeptCode = GetProductDept(mProductCode, I, txtDate(1).Text)
                GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, pDeptCode, "ST", ConPH, mFieldName, "'" & ConStockRefType_ADJ & "'")
                GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, pDeptCode, "WR", ConPH, mFieldName, "'" & ConStockRefType_ADJ & "'")
            Next

            '        If mDeptSeq = mMaxDepSeq Then
            ''TEMP COMMITED...  29-04-2011
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "", ConWH, mFieldName, "'" & ConStockRefType_ADJ & "'")
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "WR", ConWH, mFieldName, "'" & ConStockRefType_ADJ & "'")
            '        Else
            '            ''TEMP COMMITED...  29-04-2011
            '            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "", ConWH, mFieldName, "'" & ConStockRefType_ADJ & "'")
            '        End If
        ElseIf mFieldName = "SCP" Then
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "", ConWH, mFieldName, "'" & ConStockRefType_SCP & "'")
        ElseIf mFieldName = "OP_CR" Then
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "CR", ConWH, "OP")
        ElseIf mFieldName = "CL_CR" Then
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "CR", ConWH, "CL")
        ElseIf mFieldName = "DSP_CR" Then
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "CR", ConWH, "", "'" & ConStockRefType_DSP & "','" & ConStockRefType_NRG & "','" & ConStockRefType_RGP & "'")
        Else
            For I = mDeptSeq To mMaxDepSeq
                pDeptCode = GetProductDept(mProductCode, I, txtDate(1).Text)
                GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, pDeptCode, "ST", ConPH, mFieldName)
                GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, pDeptCode, "WR", ConPH, mFieldName)
            Next

            '        If mDeptSeq = mMaxDepSeq Then
            ''TEMP COMMITED...  29-04-2011
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "", ConWH, mFieldName)
            GetWIPProductionQty = GetWIPProductionQty - GetStockQty(mProductCode, mItemUOM, "", "WR", ConWH, mFieldName)
            '        Else
            '            ''TEMP COMMITED...  29-04-2011
            '            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "", ConWH, mFieldName)
            '        End If
        End If
        ''GetStockQty
        Exit Function
ErrPart:
        GetWIPProductionQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetProductionQty(ByRef mProductCode As String, ByRef mDeptCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDeptSeq As Integer
        Dim mMaxDepSeq As Integer
        Dim pDeptCode As String
        Dim I As Integer

        mDeptSeq = GetProductSeqNo(mProductCode, mDeptCode, txtDate(1).Text)
        mMaxDepSeq = GetMaxProductSeqNo(mProductCode, txtDate(1).Text)
        GetProductionQty = 0



        '    SqlStr = " SELECT SUM(PROD_QTY) AS PROD_QTY " & vbCrLf _
        ''            & " FROM PRD_PMEMODEPT_HDR IH, PRD_PMEMODEPT_DET ID" & vbCrLf _
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf _
        ''            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _
        ''            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'" & vbCrLf _
        ''            & " AND REF_DATE>='" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''            & " AND REF_DATE<='" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "'"
        '
        '    SqlStr = SqlStr & vbCrLf & " AND (GETFINALOPR(IH.COMPANY_CODE, IH.DEPT_CODE, ID.ITEM_CODE,ID.OPR_CODE)='Y' OR ID.OPR_CODE IS NULL)"

        SqlStr = " SELECT SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY) AS PROD_QTY " & vbCrLf & " FROM " & ConInventoryTable & "" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND STOCK_ID='" & ConPH & "'" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND REF_TYPE='" & ConStockRefType_PMEMODEPT & "'" & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND REF_NO IN ( " & vbCrLf & " SELECT DISTINCT IH.AUTO_KEY_REF FROM PRD_PMEMODEPT_HDR IH, PRD_PMEMODEPT_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'" & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetProductionQty = IIf(IsDbNull(RsTemp.Fields("PROD_QTY").Value), 0, RsTemp.Fields("PROD_QTY").Value)
        End If

        For I = mDeptSeq To mMaxDepSeq
            pDeptCode = GetProductDept(mProductCode, I, txtDate(1).Text)
            SqlStr = " SELECT SUM(DECODE(IS_PRODUCTION,'Y',1,-1) * PROD_QTY) AS PROD_QTY " & vbCrLf & " FROM PRD_BREAKUP_HDR IH, PRD_BREAKUP_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                GetProductionQty = GetProductionQty + IIf(IsDbNull(RsTemp.Fields("PROD_QTY").Value), 0, RsTemp.Fields("PROD_QTY").Value)
            End If
        Next

        Exit Function
ErrPart:
        GetProductionQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function DespatchSqlQry(ByRef pItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        ''TRN.RM_CODE,
        ''    ''TEMP_BOM

        ''DISTINCT

        DespatchSqlQry = ""
        SqlStr = " SELECT  " & vbCrLf & " LEVEL,TRN.PRODUCT_CODE, TRN.STD_QTY+  GROSS_WT_SCRAP AS STD_QTY, DEPT_CODE" & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " START WITH  TRIM(RM_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE) || ' '=TRIM(RM_CODE) || COMPANY_CODE || ' '"

        DespatchSqlQry = SqlStr

        Exit Function
ErrPart:
        DespatchSqlQry = ""
    End Function
    Private Function GetRelationItem(ByRef mProductCode As String) As String
        On Error GoTo ErrPart


        GetRelationItem = " SELECT REF_ITEM_CODE , ITEM_UOM " & vbCrLf & " FROM INV_ITEM_RELATIONSHIP_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"


        Exit Function
ErrPart:
        GetRelationItem = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
