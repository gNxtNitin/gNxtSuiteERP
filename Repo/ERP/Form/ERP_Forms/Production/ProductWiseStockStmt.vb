Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProductWiseStockStmt
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection
    Private Const RowHeight As Short = 22

    Private Const ColPicMain As Short = 1
    Private Const ColPicSub As Short = 2
    Private Const ColMainProd As Short = 3
    Private Const ColProductDesc As Short = 4
    Private Const ColSRNo As Short = 5
    Private Const ColRMCode As Short = 6
    Private Const ColRMDesc As Short = 7
    Private Const ColStdQty As Short = 8
    Private Const ColUnit As Short = 9
    Private Const ColOPQty As Short = 10
    Private Const ColRecdQty As Short = 11
    Private Const ColIssueQty As Short = 12
    Private Const ColStockQty As Short = 13
    Private Const ColContPer As Short = 14
    Private Const ColContClosing As Short = 15
    Private Const ColProdPlanning As Short = 16
    Private Const ColInventoryDays As Short = 17
    Private Const ColReqInventory As Short = 18
    Private Const ColInventoryExcess As Short = 19
    Private Const ColQCQty As Short = 20
    Private Const ColDeptStock As Short = 21
    Private Const ColRate As Short = 22
    Private Const ColValue As Short = 23
    Private Const ColQCValue As Short = 24
    Private Const ColMinQty As Short = 25
    Private Const ColMaxQty As Short = 26
    Private Const ColFlag As Short = 27
    Private Const ColLevel As Short = 28
    Private Const ColVendorName As Short = 29
    Private Const ColTotalReq As Short = 30
    Private Const ColCommon As Short = 31

    Dim mActiveRow As Integer
    Dim mcntRow As Integer

    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image
    Dim mIsGrouped As Boolean

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

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
    Private Sub chkFG_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFG.CheckStateChanged
        txtFGName.Enabled = IIf(chkFG.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearchFG.Enabled = IIf(chkFG.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub cmdSearchFG_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchFG.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtFGName.Text), "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtFGName.Text = AcName
            txtFGName_Validating(txtFGName, New System.ComponentModel.CancelEventArgs(False))
            txtFGName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        MainClass.ClearGrid(SprdMain, RowHeight)
        FormatSprdMain(-1)

        '    If chkAll.Value = vbUnchecked Then
        '        If Trim(txtWEF.Text) = "" Then
        '            MsgInformation "Please Enter Date."
        '            txtWEF.SetFocus
        '            Exit Sub
        '        End If
        '    End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Show1()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        SprdMain.Focus()
        Call PrintStatus(True)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim RsMain As ADODB.Recordset = Nothing
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mRMCode As String
        Dim mProductCode As String = ""
        Dim mNextProductCode As String
        Dim I As Integer
        Dim mSrn As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pSqlStr As String = ""
        Dim mLevel As Integer
        Dim mCatCode As String = "" 
        Dim mSubCatCode As String
        Dim pWEF As String

        Dim mCheckProdCode As String
        Dim mCheckRMCode As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = " SELECT IH.PRODUCT_CODE, WEF " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INVMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCheckProdCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(mCheckProdCode) & "'"
            End If
        End If

        '    If chkItem.Value = vbUnchecked Then
        '        If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
        '            mSubCatCode = MasterNo
        '            SqlStr = SqlStr & vbCrLf & "AND INVMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
        '        End If
        '    End If

        SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'" '' AND BOM_TYPE='P' AND IS_EXPORT_ITEM='N'"


        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND IH.WEF = " & vbCrLf _
        ''            & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR" & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mCheckProdCode) & "' " & vbCrLf _
        ''            & " AND WEF='" & VB6.Format((txtDateTo.Text), "DD-MMM-YYYY") & "'"
        '
        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PRODUCT_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockReadOnly)
        mcntRow = 0

        If RsMain.EOF = False Then
            Do While Not RsMain.EOF
                mProductCode = Trim(IIf(IsDbNull(RsMain.Fields("PRODUCT_CODE").Value), "", RsMain.Fields("PRODUCT_CODE").Value))
                pWEF = Trim(IIf(IsDbNull(RsMain.Fields("WEF").Value), "", RsMain.Fields("WEF").Value))

                SqlStr = ""
                SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP, " & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MINIMUM_QTY, MAXIMUM_QTY "

                SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' " & vbCrLf & " AND IH.WEF=TO_DATE('" & VB6.Format(pWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


                SqlStr = SqlStr & vbCrLf & " ORDER BY ID.PRODUCT_CODE, ID.SUBROWNO"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

                I = 0
                mLevel = 1
                If Not RsShow.EOF Then
                    Do While Not RsShow.EOF
                        mcntRow = mcntRow + 1

                        I = I + 1
                        SprdMain.Row = mcntRow


                        mSrn = Str(I)

                        '                    mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))
                        '                    mProductCode = Trim(IIf(IsNull(RsShow!PRODUCT_CODE), "", RsShow!PRODUCT_CODE))

                        Call FillGridCol(RsShow, mSrn, mLevel, mProductCode, mProductCode)

                        SprdMain.MaxRows = SprdMain.MaxRows + 1
                        RsShow.MoveNext()

                    Loop
                End If
                RsMain.MoveNext()
            Loop
        End If

        Call FormatSprdMain(-1)
        GroupBySpread(ColPicMain)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    RsShow.Cancel
        '    RsShow.Close
        RsShow = Nothing
        Exit Sub
LedgError:
        ''    Resume
        MsgInformation(Err.Description)
    End Sub
    Private Sub GroupBySpread(ByRef Col As Integer)
        'Group the data by the specified column
        Dim I As Short
        Dim currentrow As Integer
        Dim lastid As String
        'Dim prevtext As Object
        Dim lastheaderrow As Integer
        'Dim ret As Boolean
        Dim Currentid As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SprdMain.ReDraw = False
        BoldHeader(Col)

        '    For I = 1 To SprdMain.MaxRows
        '        SprdMain.Row = I
        '        SprdMain.Col = ColLevel
        '        If Trim(SprdMain.Text) = 1 Then
        '            SprdMain.Row = I
        '            SprdMain.Row2 = I
        '            SprdMain.Col = 1
        '            SprdMain.col2 = SprdMain.MaxCols
        '            SprdMain.BlockMode = True
        '            SprdMain.BackColor = &H8000000F         ''&H80FF80
        '            SprdMain.BlockMode = False
        '        End If
        '    Next
        '    Exit Sub

        '    SprdMain.MaxCols = SprdMain.MaxCols + 2
        'Insert 2 columns at beginning
        For I = 1 To 2
            '        SprdMain.InsertCols i, 1

            'Change col width
            SprdMain.set_ColWidth(I, 2)
        Next I

        SprdMain.Col = ColPicMain
        SprdMain.Row = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray

        'Init variables
        lastheaderrow = 0
        currentrow = 1
        lastid = "  "

        While currentrow <= SprdMain.DataRowCnt

            SprdMain.Row = currentrow
            SprdMain.Col = ColMainProd ''ColSRNo
            Currentid = UCase(Trim(SprdMain.Text))
            '        If InStr(1, Currentid, ".") > 0 Then
            '            Currentid = Left(Currentid, InStr(1, Currentid, ".") - 1)
            '        End If
            If Currentid <> lastid Then
                If lastheaderrow <> 0 Then
                    SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow - 1))
                    '                mRecordCount = SprdMain.GetRowItemData(lastheaderrow)
                End If

                lastid = UCase(Trim(SprdMain.Text))
                '            If InStr(1, lastid, ".") > 0 Then
                '                lastid = Left(lastid, InStr(1, lastid, ".") - 1)
                '            End If

                lastheaderrow = currentrow

                'Insert a new header row
                InsertHeaderRow(currentrow, 0)
            Else
                MakePictureCellType((SprdMain.Row), ColPicSub)
                SprdMain.Col = ColPicSub
                SprdMain.TypePictPicture = minuspict
                'SprdMain.SetCellBorder(ColPicSub, SprdMain.Row, ColPicSub, SprdMain.Row, SS_BORDER_TYPE_LEFT, System.Drawing.ColorTranslator.FromOle(0), FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)
            End If
            SprdMain.Row = SprdMain.Row + 1
            currentrow = currentrow + 1

        End While

        'Display last read data
        SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow))
        SprdMain.MaxRows = SprdMain.DataRowCnt
        SprdMain.SetActiveCell(1, 1)

        'Paint Spread
        SprdMain.ReDraw = True

        'Update displays
        System.Windows.Forms.Application.DoEvents()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub MakePictureCellType(ByRef Row As Integer, ByRef Col As Short)
        'Define specified cell as type PICTURE

        Exit Sub
        SprdMain.Col = Col
        SprdMain.Row = Row

        SprdMain.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
        SprdMain.TypePictCenter = True
        SprdMain.TypePictMaintainScale = False
        SprdMain.TypePictStretch = False

    End Sub


    Private Sub InsertHeaderRow(ByRef RowNum As Integer, ByRef pRecordCount As Integer)
        'Insert a header row at the specifed location

        '    SprdMain.InsertRows rownum, 1

        SprdMain.Col = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray
        SprdMain.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue
        SprdMain.Font = VB6.FontChangeBold(SprdMain.Font, True)

        MakePictureCellType(RowNum, ColPicMain)

        SprdMain.Col = ColPicMain
        SprdMain.TypePictPicture = minuspict
        SprdMain.Col = ColPicSub
        SprdMain.Text = ""

        'Add picture state values
        SprdMain.Col = ColFlag
        SprdMain.Text = "0"

        'Add Border

        'SprdMain.SetCellBorder(ColPicMain, RowNum, SprdMain.MaxCols, RowNum, SS_BORDER_TYPE_OUTLINE, System.Drawing.ColorTranslator.FromOle(0), FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

    End Sub

    Private Sub BoldHeader(ByRef Col As Integer)
        'Reset the header bolds and make the sort col bold

        'Change font for visual cue to what column sorting on
        'Reset all header fonts
        With SprdMain
            .Row = 0
            .Col = -1
            .Font = VB6.FontChangeBold(.Font, False)

            'Bold the specified column
            .Row = 0
            .Col = Col
            .Font = VB6.FontChangeBold(.Font, True)
        End With
    End Sub
    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pSRNo As String, ByRef pLevel As Integer, ByRef pProductCode As String, ByRef pParentCode As String)
        On Error GoTo FillGERR
        Dim mDrgRevNo As String
        Dim mRMCode As String
        Dim mItemUOM As String = ""
        Dim mStockQty As Double
        Dim mDeptCode As String
        Dim mFactor As Double
        Dim mPurchaseRate As Double
        Dim mLandedCost As Double
        Dim mRate As Double
        Dim mOpQty As Double
        Dim mRecdQty As Double
        Dim mIssueQty As Double

        Dim mTotalCont As Double
        Dim mItemCont As Double
        Dim mContPer As Double
        Dim mContClosing As Double
        Dim mProdPlanning As Double
        Dim mInventoryDays As Double
        Dim mReqInventory As Double
        Dim mInventoryExcess As Double
        Dim mStdQty As Double
        Dim mQCQty As Double
        Dim mMinLevel As Double
        Dim mMaxLevel As Double

        With SprdMain
            .Col = ColMainProd
            .Text = pProductCode
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            If MainClass.ValidateWithMasterTable(pProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                .Col = ColProductDesc
                .Text = MasterNo
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                If pLevel = 1 Then
                    .Col = ColRMCode
                    .Text = pProductCode
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColRMDesc
                    .Text = MasterNo
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    '                .Col = ColSRNo
                    '                .Text = pSRNo
                    '                .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColLevel
                    .Text = Str(pLevel)

                    .Col = ColUnit
                    .Text = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
                    mItemUOM = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColStockQty
                    mStockQty = GetBalanceStockQty(pProductCode, (txtDateTo.Text), mItemUOM, "", "FG", "", ConWH, -1,,,,, "X")
                    mStockQty = mStockQty + GetBalanceStockQty(pProductCode, (txtDateTo.Text), mItemUOM, "", "CR", "", ConWH, -1,,,,, "X")
                    mStockQty = mStockQty + GetBalanceStockQty(pProductCode, (txtDateTo.Text), mItemUOM, "", "CR", "", ConPH, -1,,,,, "X")

                    .Text = CStr(mStockQty)
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColQCQty
                    mStockQty = GetBalanceStockQty(pProductCode, (txtDateTo.Text), mItemUOM, "", "ST", "", ConWH, -1)
                    mStockQty = mStockQty + GetBalanceStockQty(pProductCode, (txtDateTo.Text), mItemUOM, "", "QC", "", ConWH, -1,,,,, "X")

                    .Text = CStr(mStockQty)
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColDeptStock
                    mStockQty = GetBalanceStockQty(pProductCode, (txtDateTo.Text), mItemUOM, "", "ST", "", ConPH, -1,,,,, "X")
                    '                mStockQty = mStockQty + GetBalanceStockQty(pProductCode, txtDateTo.Text, mItemUOM, "", "ST", "", ConPH, -1)
                    .Text = CStr(mStockQty)
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))


                    .Col = ColFlag
                    .Text = "0"

                    .MaxRows = .MaxRows + 1
                    mcntRow = mcntRow + 1
                    .Row = mcntRow
                    pLevel = pLevel + 1
                    '                pSRNo = pSRNo + 1

                    .Col = ColMainProd
                    .Text = pProductCode
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColProductDesc
                    .Text = MasterNo
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                End If
            End If

            .Col = ColSRNo
            .Text = pSRNo
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            mDeptCode = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)

            .Col = ColRMCode
            .Text = IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
            mRMCode = IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColRMDesc
            .Text = IIf(IsDbNull(pRs.Fields("Item_Short_Desc").Value), "", pRs.Fields("Item_Short_Desc").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColStdQty
            .Text = CStr(Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value)))
            mStdQty = Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value))
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColLevel
            .Text = Str(pLevel)

            .Col = ColUnit
            .Text = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
            mItemUOM = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColOPQty
            If chkClosing.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mOpQty = GetBalanceStockQty(mRMCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDateTo.Text))), mItemUOM, "", "ST", "", ConWH, -1)
            Else
                mOpQty = 0
            End If
            .Text = CStr(mOpQty)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColRecdQty
            If chkClosing.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mRecdQty = GetStockQty_Period(mRMCode, (txtDateTo.Text), (txtDateTo.Text), mItemUOM, "", "ST", "", ConWH, "I")
            Else
                mRecdQty = 0
            End If
            .Text = CStr(mRecdQty)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColIssueQty
            If chkClosing.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mIssueQty = GetStockQty_Period(mRMCode, (txtDateTo.Text), (txtDateTo.Text), mItemUOM, "", "ST", "", ConWH, "O")
            Else
                mIssueQty = 0
            End If
            .Text = CStr(System.Math.Abs(mIssueQty))
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColQCQty
            mStockQty = GetBalanceStockQty(mRMCode, (txtDateTo.Text), mItemUOM, "", "QC", "", ConWH, -1,,,,, "X")
            .Text = CStr(mStockQty)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColDeptStock
            mStockQty = GetBalanceStockQty(mRMCode, (txtDateTo.Text), mItemUOM, "", "ST", "", ConPH, -1,,,,, "X")
            .Text = CStr(mStockQty)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColStockQty
            mStockQty = GetBalanceStockQty(mRMCode, (txtDateTo.Text), mItemUOM, "", "ST", "", ConWH, -1,,,,, "X")
            .Text = CStr(mStockQty)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            If chkClosing.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mTotalCont = GetTotalContribution("", mRMCode)
                mItemCont = GetTotalContribution(pProductCode, mRMCode)

                If mTotalCont = 0 Then
                    mContPer = 0
                Else
                    mContPer = mItemCont * 100 / mTotalCont
                End If

                .Col = ColContPer
                .Text = CStr(mContPer)

                .Col = ColContClosing
                mContClosing = CDbl(VB6.Format(mStockQty * mContPer / 100, "0"))
                .Text = CStr(mContClosing)

                .Col = ColProdPlanning
                mProdPlanning = mItemCont * mStdQty
                .Text = CStr(mProdPlanning)

                .Col = ColInventoryDays
                If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "LEAD_TIME", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mInventoryDays = Val(MasterNo)
                End If

                mInventoryDays = IIf(mInventoryDays = 0, 1, mInventoryDays)
                .Text = CStr(mInventoryDays)

                .Col = ColReqInventory
                mReqInventory = mProdPlanning * mInventoryDays
                .Text = CStr(mReqInventory)

                .Col = ColInventoryExcess
                mInventoryExcess = mContClosing - mReqInventory
                .Text = CStr(mInventoryExcess)

                .Col = ColQCQty
                mQCQty = GetBalanceStockQty(mRMCode, (txtDateTo.Text), mItemUOM, "STR", "QC", "", ConWH, -1)
                .Text = CStr(mQCQty)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColDeptStock
                .Text = CStr(GetBalanceStockQty(mRMCode, (txtDateTo.Text), mItemUOM, "", "ST", "", ConPH, -1))
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "UOM_FACTOR", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mFactor = MasterNo
                Else
                    mFactor = 1
                End If


                If GetLatestItemCostFromPO(mRMCode, mPurchaseRate, mLandedCost, (txtDateTo.Text), "ST", "", mItemUOM, mFactor) = False Then GoTo FillGERR

                mRate = mPurchaseRate ''IIf(mPurchaseRate = 0, mItemCost, mPurchaseRate)

                .Col = ColRate
                .Text = VB6.Format(mRate, "0.000")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColValue
                .Text = VB6.Format(mInventoryExcess * mRate, "0.000") ''mStockQty
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColQCValue
                .Text = VB6.Format(mQCQty * mRate, "0.000") ''mStockQty
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColMinQty
                If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "MINIMUM_QTY", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mMinLevel = Val(MasterNo)
                End If
                .Text = VB6.Format(mMinLevel, "0") ''IIf(IsNull(pRs!MINIMUM_QTY), "", pRs!MINIMUM_QTY)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColMaxQty
                If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "MAXIMUM_QTY", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mMaxLevel = Val(MasterNo)
                End If
                .Text = VB6.Format(mMaxLevel, "0") ''IIf(IsNull(pRs!MAXIMUM_QTY), "", pRs!MAXIMUM_QTY)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColFlag
                .Text = "0"

                .Col = ColVendorName
                .Text = GetVendorName(mRMCode)

                '            .Col = ColTotalReq
                '            .Text = GetTotalRequirement(mRMCode, txtDateTo.Text)

                .Col = ColCommon
                .Text = GetCommonFinishedGood(pProductCode, mRMCode)

            End If

        End With


        Call FillSubAlterRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mDeptCode, pParentCode)

        If chkShowBOM.CheckState = System.Windows.Forms.CheckState.Checked Then
            Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode)
        End If

        Exit Sub
FillGERR:
        '    Resume
        MsgBox(Err.Description)
    End Sub
    Private Function GetTotalRequirement(ByRef pItemCode As String, ByRef pAsOnDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mParentcode As String
        Dim mChildCode As String
        Dim mStdQty As Double
        Dim mLevel As Integer
        Dim mPlaningQty As Double
        Dim pItemUOM As String = ""
        Dim mSqlStrRel As String
        Dim RsRel As ADODB.Recordset = Nothing
        Dim xProductRelCode As String
        Dim mItemLevelStdQty() As Double

        ReDim mItemLevelStdQty(1000)
        GetTotalRequirement = 0

        SqlStr = " SELECT  " & vbCrLf & " LEVEL,TRN.PRODUCT_CODE, TRN.STD_QTY+  GROSS_WT_SCRAP AS STD_QTY, DEPT_CODE,MAIN_ITEM" & vbCrLf & " FROM VW_PRD_BOM_TRN_WO_ALTER TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " START WITH  TRIM(RM_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "' AND MAIN_ITEM='Y'" & vbCrLf & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE) || ' '=TRIM(RM_CODE) || COMPANY_CODE || ' '"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mStdQty = 1
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

                If MainClass.ValidateWithMasterTable(mParentcode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    pItemUOM = Trim(MasterNo)
                End If


                mPlaningQty = GetPlaningQty(mParentcode, pAsOnDate) '' Abs(GetStockQty(mParentcode, pItemUOM, "STR", "FG", ConWH, "", "'" & ConStockRefType_DSP & "'"))

                GetTotalRequirement = GetTotalRequirement + (mPlaningQty * mStdQty)

                mSqlStrRel = GetRelationItem(mParentcode)
                If mSqlStrRel <> "" Then
                    MainClass.UOpenRecordSet(mSqlStrRel, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRel, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsRel.EOF = False Then
                        Do While RsRel.EOF = False
                            xProductRelCode = Trim(IIf(IsDbNull(RsRel.Fields("REF_ITEM_CODE").Value), "", RsRel.Fields("REF_ITEM_CODE").Value))
                            mPlaningQty = GetPlaningQty(xProductRelCode, pAsOnDate) '' Abs(GetStockQty(mParentcode, pItemUOM, "STR", "FG", ConWH, "", "'" & ConStockRefType_DSP & "'"))
                            GetTotalRequirement = GetTotalRequirement + (mPlaningQty * mStdQty)
                            RsRel.MoveNext()
                        Loop
                    End If
                End If

                mPlaningQty = 0
                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
ErrPart:
        GetTotalRequirement = CDbl("")
    End Function

    Private Function GetPlaningQty(ByRef pItemCode As String, ByRef pAsOnDate As String) As Double


        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""

        SqlStr = " SELECT ABS(SUM(DPLAN_QTY)) AS DPLAN_QTY" & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INHOUSE_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(pAsOnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields(0).Value) Then
                GetPlaningQty = 0
            Else
                GetPlaningQty = RsTemp.Fields(0).Value
            End If
        Else
            GetPlaningQty = 0
        End If

        RsTemp = Nothing

        Exit Function
ErrPart:
        GetPlaningQty = 0
    End Function


    Private Function GetTotalContribution(ByRef pFGCode As String, ByRef pRMCode As String) As Double

        On Error GoTo FillGERR
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mProductCode As String = ""
        Dim RsTempMon As ADODB.Recordset = Nothing
        Dim mMainProductCode As String
        Dim mDeptCode As String
        GetTotalContribution = 0
        SqlStr = "SELECT IH.PRODUCT_CODE AS REF_ITEM_CODE, IH.PRODUCT_CODE, (STD_QTY + GROSS_WT_SCRAP) As STD_QTY,0 As MAINCODE, DEPT_CODE" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.STATUS='O'" & vbCrLf & " AND RM_CODE='" & MainClass.AllowSingleQuote(pRMCode) & "'" & vbCrLf
        If Trim(pFGCode) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pFGCode) & "'"
        End If

        SqlStr = SqlStr & " UNION " & vbCrLf & " SELECT IID.REF_ITEM_CODE, IH.PRODUCT_CODE, (STD_QTY + GROSS_WT_SCRAP) As STD_QTY,1 AS MAINCODE, DEPT_CODE" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_RELATIONSHIP_DET IID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.STATUS='O'" & vbCrLf & " AND IH.COMPANY_CODE=IID.COMPANY_CODE" & vbCrLf & " AND IH.PRODUCT_CODE=IID.ITEM_CODE" & vbCrLf & " AND RM_CODE='" & MainClass.AllowSingleQuote(pRMCode) & "'" & vbCrLf
        If Trim(pFGCode) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pFGCode) & "'"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mProductCode = IIf(IsDbNull(RsTemp.Fields("REF_ITEM_CODE").Value), "", RsTemp.Fields("REF_ITEM_CODE").Value)
                mMainProductCode = IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value)
                mDeptCode = IIf(IsDbNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value) 'GetProductDept(Trim(mMainProductCode), 1, txtDateTo.Text)

                SqlStr = "SELECT SUM(IPLAN_QTY) As IPLAN_QTY" & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"

                SqlStr = SqlStr & vbCrLf & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempMon)

                If RsTempMon.EOF = False Then
                    Do While RsTempMon.EOF = False
                        GetTotalContribution = GetTotalContribution + IIf(IsDbNull(RsTempMon.Fields("IPLAN_QTY").Value), 0, RsTempMon.Fields("IPLAN_QTY").Value)
                        RsTempMon.MoveNext()
                    Loop
                End If
                RsTemp.MoveNext()
            Loop
        End If



        Exit Function
FillGERR:
        '    Resume
        MsgBox(Err.Description)
    End Function
    Private Function GetCommonFinishedGood(ByRef pProductCode As String, ByRef mRMCode As String) As String

        On Error GoTo ErrPart
        Dim pSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetCommonFinishedGood = ""
        pSqlStr = "SELECT DISTINCT PRODUCT_CODE " & vbCrLf & " FROM PRD_NEWBOM_DET ID " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE<>'" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf & " AND RM_CODE='" & MainClass.AllowSingleQuote(mRMCode) & "'" & vbCrLf & " ORDER BY " & vbCrLf & " PRODUCT_CODE"

        'AND STATUS='O'

        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                If GetCommonFinishedGood = "" Then
                    GetCommonFinishedGood = Trim(IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                Else
                    GetCommonFinishedGood = GetCommonFinishedGood & ", " & Trim(IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                End If
                RsTemp.MoveNext()
            Loop

            RsTemp = Nothing
            '        RsTemp.Close
        End If
        Exit Function
ErrPart:
        GetCommonFinishedGood = ""
    End Function
    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
    End Sub

    Private Sub frmProductWiseStockStmt_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

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
    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String)

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String
        Dim j As Integer

        mSrn = pSrn
        pLevel = pLevel + 1
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') " '& vbCrLf |            & " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |
        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                mcntRow = mcntRow + 1
                SprdMain.MaxRows = SprdMain.MaxRows + 1
                SprdMain.Row = mcntRow

                j = j + 1
                xSrn = mSrn & "." & j
                pSrn = pSrn & "." & j

                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode)
                RsShow.MoveNext()
            Loop
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ITEM_QTY AS STD_QTY, ID.SCRAP_QTY AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    mcntRow = mcntRow + 1
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    SprdMain.Row = mcntRow

                    j = j + 1
                    xSrn = mSrn & "." & j
                    pSrn = pSrn & "." & j

                    mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                    Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode)
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

    Private Sub FillSubAlterRecord(ByRef pRMMainCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef pDeptCode As String, ByRef pParentCode As String)

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String

        mSrn = pSrn
        '    pLevel = pLevel + 1

        If pDeptCode <> "J/W" Then
            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_RM_CODE AS RM_CODE, '(*) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_STD_QTY AS STD_QTY, ID.ALETRSCRAP AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET IDET, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=IDET.MKEY " & vbCrLf & " AND IDET.MKEY=ID.MKEY " & vbCrLf & " AND IDET.RM_CODE=ID.MAINITEM_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ALTER_RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(pRMMainCode) & "'" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pMainProductCode) & "'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pMainProductCode) & "') " '& vbCrLf |                & " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |
            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_ITEM_CODE AS RM_CODE, '(**) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_ITEM_QTY AS STD_QTY, ID.ALTER_SCRAP_QTY AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " and ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ALTER_ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pRMMainCode) & "' " & vbCrLf & " AND ID.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "') "

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                mcntRow = mcntRow + 1
                SprdMain.MaxRows = SprdMain.MaxRows + 1
                SprdMain.Row = mcntRow

                xSrn = mSrn
                pSrn = pSrn

                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pRMMainCode)
                RsShow.MoveNext()
            Loop
        End If
        RsShow = Nothing
        '        RsShow.Close

        Exit Sub
FillERR:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Public Sub frmProductWiseStockStmt_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Productwise Stock Statement"
        FormatSprdMain(False)
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmProductWiseStockStmt_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7440
        CurrFormWidth = 11625

        Me.Top = VB6.TwipsToPixelsY(24)
        Me.Left = VB6.TwipsToPixelsX(24)
        'Me.Height = VB6.TwipsToPixelsY(7440)
        ''Me.Width = VB6.TwipsToPixelsX(11625)


        '    txtDateFrom.Text = VB6.Format(RsCompany!START_DATE, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        chkFG_CheckStateChanged(chkFG, New System.EventArgs())
        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked

        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False

        chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False

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
    Private Sub FormatSprdMain(ByRef mFillColHeading As Boolean)

        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColCommon
            .set_RowHeight(-1, RowHeight * 0.75)

            .Row = -1
            .set_ColWidth(0, 4)

            .Col = ColPicMain
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictCenter = True
            .TypePictMaintainScale = False
            .TypePictStretch = False

            .Col = ColPicSub
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictCenter = True
            .TypePictMaintainScale = False
            .TypePictStretch = False
            .ColHidden = True

            .Col = ColMainProd
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColProductDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColSRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColSRNo, 6)

            .Col = ColRMCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColRMCode, 8)

            .Col = ColRMDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColRMDesc, 30)

            .Col = ColStdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStdQty, 8)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            For cntCol = ColOPQty To ColMaxQty
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 12)
                .ColHidden = IIf(chkClosing.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)
            Next

            .Col = ColStockQty
            .ColHidden = False

            .Col = ColQCQty
            .ColHidden = False

            .Col = ColDeptStock
            .ColHidden = False

            .Col = ColLevel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColFlag
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColVendorName
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColVendorName, 20)
            .ColHidden = IIf(chkClosing.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)

            .Col = ColTotalReq
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTotalReq, 9)
            .ColHidden = True

            .Col = ColCommon
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColCommon, 20)

            .ColsFrozen = ColRMDesc

            mIsGrouped = False
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle

        End With

    End Sub
    Private Sub frmProductWiseStockStmt_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ProdWiseStock.rpt"

        mSubTitle = "As On Date : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtFGName.Text) <> "" Then
            mSubTitle = mSubTitle & " [Product Name : " & txtFGName.Text & "]"
        End If

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

    Private Sub txtFGName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFGName.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtFGName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFGName.DoubleClick
        Call cmdSearchFG_Click(cmdSearchFG, New System.EventArgs())
    End Sub

    Private Sub txtFGName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFGName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFGName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFGName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtFGName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchFG_Click(cmdSearchFG, New System.EventArgs())
    End Sub
    Private Sub txtFGName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFGName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtFGName.Text) = "" Then GoTo EventExitSub
        '    If MainClass.ValidateWithMasterTable(txtFGName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = False Then
        '        MsgBox "Invalid Category Code."
        '        Cancel = True
        '    Else
        '        lblCatCode.text = MasterNo
        '    End If

        If MainClass.ValidateWithMasterTable((txtFGName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invalid Item Code.")
            Cancel = True
        Else
            lblCatCode.Text = MasterNo
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Sort on specified column or show/collapse rows

        'Show Summary/Detail info.
        'If clicked on a "+" or "-" grouping

        If eventArgs.col = ColPicMain Then
            SprdMain.Col = ColPicMain
            SprdMain.Row = eventArgs.row
            If SprdMain.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture Then
                'Show or hide the specified rows
                ShowHideRows(ColPicMain, eventArgs.row)
            End If
        End If
    End Sub
    Private Sub ShowHideRows(ByRef Col As Integer, ByRef Row As Integer)
        'Collapse or uncollape the specified rows
        Dim I As Short
        Dim collapsetype As Short

        SprdMain.Row = Row
        SprdMain.Col = ColFlag

        If SprdMain.Text = "0" Then
            collapsetype = 0 'collape/hide rows : minus picture
            SprdMain.Col = 1
            SprdMain.TypePictPicture = pluspict
            SprdMain.Col = ColFlag
            SprdMain.Text = "1"
        Else
            collapsetype = 1 'uncollapse / show rows: plus picture
            SprdMain.Col = 1
            SprdMain.TypePictPicture = minuspict
            SprdMain.Col = ColFlag
            SprdMain.Text = "0"
        End If

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
    Private Function GetVendorName(ByRef pItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        '    If MainClass.ValidateWithMasterTable() = True Then
        '
        '    End If

        GetVendorName = ""
        SqlStr = " SELECT DISTINCT SUPP_CUST_NAME " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            Do While RsTemp.EOF = False
                GetVendorName = IIf(GetVendorName = "", "", GetVendorName & ", ") & IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
ErrPart:
        GetVendorName = ""
    End Function
End Class
