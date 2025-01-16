Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMaterialVsSalesOrder
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection
    Private Const RowHeight As Short = 22

    Private Const ColPicMain As Short = 1
    Private Const ColPicSub As Short = 2

    Private Const ColSalesOrderNo As Short = 3
    Private Const ColSalesOrderDate As Short = 4
    Private Const ColPartyOrderNo As Short = 5
    Private Const ColPartyOrderDate As Short = 6
    Private Const ColCustomerCode As Short = 7
    Private Const ColCustomerName As Short = 8
    Private Const ColMainProd As Short = 9
    Private Const ColProductDesc As Short = 10
    Private Const ColProductQty As Short = 11
    Private Const ColSRNo As Short = 12
    Private Const ColRMCode As Short = 13
    Private Const ColRMDesc As Short = 14
    Private Const ColStdQty As Short = 15
    Private Const ColUnit As Short = 16
    Private Const ColTotalQty As Short = 17
    Private Const ColAvailableQty As Short = 18
    Private Const ColShortQty As Short = 19
    Private Const ColLevel As Short = 20
    Private Const ColFlag As Short = 21

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
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub chkFG_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFG.CheckStateChanged
        txtFGName.Enabled = IIf(chkFG.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearchFG.Enabled = IIf(chkFG.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub chkSOAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSOAll.CheckStateChanged
        Call PrintStatus(False)
        If chkSOAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSalesOrder.Enabled = False
            cmdSaleOrder.Enabled = False
        Else
            txtSalesOrder.Enabled = True
            cmdSaleOrder.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdSaleOrder_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSaleOrder.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mCustCode As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O'"

        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCustomerName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtCustomerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "'"
            End If
        End If
        If MainClass.SearchGridMaster(txtSalesOrder.Text, "DSP_SALEORDER_HDR", "AUTO_KEY_SO", "SO_DATE", "CUST_PO_NO", "CUST_PO_DATE", SqlStr) = True Then
            txtSalesOrder.Text = AcName
            txtSalesOrder_Validating(txtSalesOrder, New System.ComponentModel.CancelEventArgs(False))
            txtSalesOrder.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        '    SprdMain.SetFocus
        Call PrintStatus(True)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim RsBudgetMain As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        'Dim mProdCode As String
        'Dim mProdName As String
        Dim mCustCode As String
        'Dim mCustName As String
        Dim mCheckProdCode As String
        Dim mMonthName As String


        mMonthName = UCase(MonthName(Month(CDate(lblRunDate.Text))))

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        If optShow(0).Checked = True Then
            SqlStr = " SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM, " & vbCrLf & " SUM(CASE WHEN ID.ITEM_QTY IS NULL THEN 0 ELSE ID.ITEM_QTY END) AS TOTAL_QTY, " & vbCrLf & " 0 AS TOTAL_RATE, " & vbCrLf & " 0 AS TOTAL_VALUE, SIH.AUTO_KEY_SO, SIH.SO_DATE, SIH.CUST_PO_NO, SIH.CUST_PO_DATE "
        Else
            SqlStr = " SELECT '' AS SUPP_CUST_CODE, '' AS SUPP_CUST_NAME, ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM, " & vbCrLf & " SUM(CASE WHEN ID.ITEM_QTY IS NULL THEN 0 ELSE ID.ITEM_QTY END) AS TOTAL_QTY, " & vbCrLf & " 0 AS TOTAL_RATE, " & vbCrLf & " 0 AS TOTAL_VALUE, '' AS AUTO_KEY_SO, '' AS SO_DATE, '' AS CUST_PO_NO, '' AS CUST_PO_DATE "
        End If

        SqlStr = SqlStr & vbCrLf & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DELV_SCHLD_DET ID, DSP_SALEORDER_HDR SIH," & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, INV_GENERAL_MST GMAT " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_DELV,LENGTH(IH.AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV "

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=SIH.COMPANY_CODE " & vbCrLf & " AND IH.AUTO_KEY_SO=SIH.AUTO_KEY_SO " & vbCrLf & " AND SIH.SO_STATUS='O'"

        If optBaseOn(0).Checked = True Then
            SqlStr = SqlStr & " AND SIH.ORDER_TYPE='O'"
        Else
            SqlStr = SqlStr & " AND SIH.ORDER_TYPE='C'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE "

        SqlStr = SqlStr & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf & " AND INVMST.COMPANY_CODE=GMAT.COMPANY_CODE " & vbCrLf & " AND INVMST.CATEGORY_CODE=GMAT.GEN_CODE AND GMAT.GEN_TYPE='C'"

        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCustomerName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtCustomerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "'"
            End If
        End If

        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCheckProdCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(mCheckProdCode) & "'"
            End If
        End If

        If chkSOAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtSalesOrder.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.AUTO_KEY_SO='" & MainClass.AllowSingleQuote(txtSalesOrder.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM') ='" & VB6.Format(lblRunDate.Text, "YYYYMM") & "'"

        SqlStr = SqlStr & vbCrLf & " AND GMAT.STOCKTYPE='FG' "


        SqlStr = SqlStr & vbCrLf & "HAVING (SUM(ID.ITEM_QTY) IS NOT NULL AND SUM(ID.ITEM_QTY)>0)"

        If optShow(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM,SIH.AUTO_KEY_SO, SIH.SO_DATE, SIH.CUST_PO_NO, SIH.CUST_PO_DATE " & vbCrLf & " ORDER BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM,SIH.AUTO_KEY_SO, SIH.SO_DATE, SIH.CUST_PO_NO, SIH.CUST_PO_DATE "
        Else
            SqlStr = SqlStr & vbCrLf & " GROUP BY ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM" & vbCrLf & " ORDER BY ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBudgetMain, ADODB.LockTypeEnum.adLockReadOnly)
        mcntRow = 1

        If RsBudgetMain.EOF = False Then
            Do While Not RsBudgetMain.EOF
                '            If Not IsNull(RsBudgetMain!ITEM_QTY) and RsBudgetMain!ITEM_QTY>0 Then
                Call ShowDetail(RsBudgetMain, mcntRow)
                '            End If
                '            mcntRow = mcntRow + 1
                '            SprdMain.MaxRows = SprdMain.MaxRows + 1
                RsBudgetMain.MoveNext()
            Loop
        End If

        Call FormatSprdMain(-1)
        GroupBySpread(ColPicMain)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsBudgetMain = Nothing
        Exit Sub
LedgError:
        ''    Resume
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowDetail(ByRef mRsBudget As ADODB.Recordset, ByRef mcntRow As Integer)

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


        Dim mTotalQty As Double
        Dim mTotalRate As Double
        Dim mTotalAmount As Double
        Dim mCustomerCode As String
        Dim mCustomerName As String
        Dim mMainItemCode As String

        Dim mSONo As String
        Dim mSODate As String
        Dim mCustPONo As String
        Dim mCustPODate As String

        If mRsBudget.EOF = False Then
            mProductCode = Trim(IIf(IsDbNull(mRsBudget.Fields("ITEM_CODE").Value), "", mRsBudget.Fields("ITEM_CODE").Value))
            mMainItemCode = GetMainItemCode(mProductCode)

            mSONo = Trim(IIf(IsDbNull(mRsBudget.Fields("AUTO_KEY_SO").Value), "", mRsBudget.Fields("AUTO_KEY_SO").Value))
            mSODate = Trim(IIf(IsDbNull(mRsBudget.Fields("SO_DATE").Value), "", mRsBudget.Fields("SO_DATE").Value))
            mCustPONo = Trim(IIf(IsDbNull(mRsBudget.Fields("CUST_PO_NO").Value), "", mRsBudget.Fields("CUST_PO_NO").Value))
            mCustPODate = Trim(IIf(IsDbNull(mRsBudget.Fields("CUST_PO_DATE").Value), "", mRsBudget.Fields("CUST_PO_DATE").Value))

            mCustomerCode = Trim(IIf(IsDbNull(mRsBudget.Fields("SUPP_CUST_CODE").Value), "", mRsBudget.Fields("SUPP_CUST_CODE").Value))
            mCustomerName = Trim(IIf(IsDbNull(mRsBudget.Fields("SUPP_CUST_NAME").Value), "", mRsBudget.Fields("SUPP_CUST_NAME").Value))


            mTotalQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("TOTAL_QTY").Value), 0, mRsBudget.Fields("TOTAL_QTY").Value), "0.00"))
            mTotalRate = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("TOTAL_RATE").Value), 0, mRsBudget.Fields("TOTAL_RATE").Value), "0.00"))
            mTotalAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("TOTAL_VALUE").Value), 0, mRsBudget.Fields("TOTAL_VALUE").Value), "0.00"))

            SqlStr = " SELECT IH.PRODUCT_CODE, WEF " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "'"
            SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'" '' AND BOM_TYPE='P' AND IS_EXPORT_ITEM='N'"
            SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PRODUCT_CODE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockReadOnly)
            '        mcntRow = 1

            If RsMain.EOF = False Then
                '            Do While Not RsMain.EOF
                pWEF = Trim(IIf(IsDbNull(RsMain.Fields("WEF").Value), "", RsMain.Fields("WEF").Value))

                SqlStr = ""
                SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP, " & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MINIMUM_QTY, MAXIMUM_QTY "

                SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "' " & vbCrLf & " AND IH.WEF=TO_DATE('" & VB6.Format(pWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


                SqlStr = SqlStr & vbCrLf & " AND ID.RM_CODE NOT LIKE 'P%'"
                SqlStr = SqlStr & vbCrLf & " ORDER BY ID.PRODUCT_CODE, ID.SUBROWNO"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

                I = 0
                mLevel = 1
                If Not RsShow.EOF Then
                    Do While Not RsShow.EOF
                        I = I + 1
                        '                        SprdMain.Row = mcntRow
                        mSrn = Str(I)
                        Call FillGridCol(RsShow, mSrn, mLevel, mProductCode, mProductCode, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mSONo, mSODate, mCustPONo, mCustPODate)

                        '                        mcntRow = mcntRow + 1
                        '                        SprdMain.MaxRows = SprdMain.MaxRows + 1
                        RsShow.MoveNext()
                    Loop
                End If
                '                RsMain.MoveNext
                '            Loop
            End If
        End If

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
            SprdMain.Col = ColLevel 'ColMainProd       ''ColSRNo
            Currentid = UCase(Trim(SprdMain.Text))
            '        If InStr(1, Currentid, ".") > 0 Then
            '            Currentid = Left(Currentid, InStr(1, Currentid, ".") - 1)
            '        End If
            If Currentid = "1" Then '<> lastid Then
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
                SprdMain.SetCellBorder(ColPicSub, SprdMain.Row, ColPicSub, SprdMain.Row, SS_BORDER_TYPE_LEFT, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

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

        SprdMain.SetCellBorder(ColPicMain, RowNum, SprdMain.MaxCols, RowNum, SS_BORDER_TYPE_OUTLINE, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

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
    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pSRNo As String, ByRef pLevel As Integer, ByRef pProductCode As String, ByRef pParentCode As String, ByRef mTotalQty As Double, ByRef mTotalRate As Double, ByRef mTotalAmount As Double, ByRef mCustomerCode As String, ByRef mCustomerName As String, ByRef mSONo As String, ByRef mSODate As String, ByRef mCustPONo As String, ByRef mCustPODate As String)


        On Error GoTo FillGERR
        Dim mDrgRevNo As String
        Dim mRMCode As String
        Dim mItemUOM As String = ""
        Dim mStockQty As Double
        Dim mDeptCode As String
        Dim mRate As Double
        Dim mFactorQty As Double
        Dim mChildRMCode As String
        Dim mStqQty As Double
        Dim mTotValue As Double
        Dim mUOM As String = ""
        Dim mTotClosing As Double
        'Dim mStockQty As Double
        Dim mShortQty As Double
        Dim mMonthDate As String


        mMonthDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(Month(CDate(lblRunDate.Text)), "00") & "/" & Year(CDate(lblRunDate.Text))
        mMonthDate = VB6.Format(mMonthDate, "DD/MM/YYYY")
        With SprdMain
            .Row = .MaxRows
            .Col = ColMainProd
            .Text = pProductCode
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            If MainClass.ValidateWithMasterTable(pProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                .Row = .MaxRows
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

                    .Col = ColSalesOrderNo
                    .Text = mSONo
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColSalesOrderDate
                    .Text = mSODate
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColPartyOrderNo
                    .Text = mCustPONo
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColPartyOrderDate
                    .Text = mCustPODate
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColCustomerCode
                    .Text = mCustomerCode
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColCustomerName
                    .Text = mCustomerName
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColTotalQty
                    .Text = VB6.Format(mTotalQty, "0.00")
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColAvailableQty
                    mStockQty = GetBalanceStockQty(pProductCode, mMonthDate, mItemUOM, "STR", "ST", "", ConWH, -1)
                    If chkQCStockType.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mStockQty = mStockQty + GetBalanceStockQty(pProductCode, mMonthDate, mItemUOM, "STR", "QC", "", ConWH, -1)
                    End If

                    mStockQty = mStockQty + GetBalanceStockQty(pProductCode, mMonthDate, mItemUOM, "STR", "FG", "", ConWH, -1)

                    If chkCRStockType.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mStockQty = mStockQty + GetBalanceStockQty(pProductCode, mMonthDate, mItemUOM, "STR", "CR", "", ConWH, -1)
                    End If

                    .Text = VB6.Format(mStockQty, "0.00")
                    .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                    .Col = ColShortQty
                    mShortQty = IIf(mTotalQty - mStockQty < 0, 0, mTotalQty - mStockQty)
                    .Text = VB6.Format(mShortQty, "0.00")
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

                    '                SprdMain.MaxRows = SprdMain.MaxRows + 1
                End If
            End If
            mRMCode = IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
            If CheckSubRecord(mRMCode) = True Then
                pLevel = pLevel + 1
                Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mSONo, mSODate, mCustPONo, mCustPODate)

            Else
                .Row = .MaxRows
                .Col = ColSRNo
                .Text = pSRNo
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                mDeptCode = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)
                mItemUOM = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)

                .Col = ColRMCode
                .Text = IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
                mRMCode = IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColRMDesc
                .Text = IIf(IsDbNull(pRs.Fields("Item_Short_Desc").Value), "", pRs.Fields("Item_Short_Desc").Value)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColStdQty
                If optCalcOn(0).Checked = True Then
                    mStqQty = Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), 0, pRs.Fields("STD_QTY").Value))
                Else
                    mStqQty = Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), 0, pRs.Fields("STD_QTY").Value)) + Val(IIf(IsDbNull(pRs.Fields("GROSS_WT_SCRAP").Value), 0, pRs.Fields("GROSS_WT_SCRAP").Value))
                End If

                mFactorQty = 1
                If mDeptCode = "J/W" Then
                    If mItemUOM = "TON" Then
                        mFactorQty = 1 / 1000
                    End If
                Else
                    If mItemUOM = "KGS" Then
                        mFactorQty = 1 / 1000
                    ElseIf mItemUOM = "TON" Then
                        mFactorQty = 1 / 1000
                        mFactorQty = mFactorQty / 1000
                    End If
                End If

                .Text = CStr(mStqQty * mFactorQty)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))


                .Col = ColLevel
                .Text = Str(pLevel)

                .Col = ColUnit
                mUOM = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
                .Text = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColSalesOrderNo
                .Text = mSONo
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColSalesOrderDate
                .Text = mSODate
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColPartyOrderNo
                .Text = mCustPONo
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColPartyOrderDate
                .Text = mCustPODate
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColCustomerCode
                .Text = mCustomerCode
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColCustomerName
                .Text = mCustomerName
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                '            mRate = GetCurrentItemRate(mRMCode, VB6.Format(lblRunDate.text, "DD/MM/YYYY"))

                .Col = ColTotalQty
                .Text = VB6.Format(mTotalQty * Val(CStr(mStqQty * mFactorQty)), "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                '            mTotClosing = VB6.Format(mTotalQty * Val(mStqQty * mFactorQty), "0.00")
                '            mTotValue = GetLatestItemCostFromMRR(Trim(mRMCode), mUOM, mTotClosing, VB6.Format(lblRunDate.text, "DD/MM/YYYY"), "L", , , "Y")
                '            If mTotClosing <= 0 Then
                '                mRate = 0
                '            Else
                '                mRate = mTotValue / mTotClosing
                '            End If
                .Row = .MaxRows
                mStockQty = GetBalanceStockQty(mRMCode, mMonthDate, mUOM, "STR", "ST", "", ConWH, -1)
                mStockQty = mStockQty * Val(CStr(mStqQty * mFactorQty))
                .Col = ColAvailableQty
                .Text = VB6.Format(mStockQty, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColShortQty
                mShortQty = IIf(mTotalQty - mStockQty < 0, 0, mTotalQty - mStockQty) * Val(CStr(mStqQty * mFactorQty))
                .Text = VB6.Format(mShortQty, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))


                .Col = ColFlag
                .Text = "0"
                SprdMain.MaxRows = SprdMain.MaxRows + 1
                mcntRow = mcntRow + 1
            End If
        End With

        Exit Sub
FillGERR:
        '    Resume
        MsgBox(Err.Description)
    End Sub
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


    Private Sub frmParamMaterialVsSalesOrder_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtCustomerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtCustomerName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerName.DoubleClick
        SearchCustomer()
    End Sub
    Private Sub txtCustomerName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomerName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCustomerName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomerName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCustomer()
    End Sub
    Private Sub txtCustomerName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomerName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If txtCustomerName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

        If MainClass.ValidateWithMasterTable((txtCustomerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCustomerName.Text = UCase(Trim(txtCustomerName.Text))
        Else
            MsgInformation("No Such Customer in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchCustomer()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtCustomerName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtCustomerName.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdsearchCustName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCustName.Click
        SearchCustomer()
    End Sub
    Private Sub chkAllCustomer_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCustomer.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCustomerName.Enabled = False
            cmdsearchCustName.Enabled = False
        Else
            txtCustomerName.Enabled = True
            cmdsearchCustName.Enabled = True
        End If
    End Sub






    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef mTotalQty As Double, ByRef mTotalRate As Double, ByRef mTotalAmount As Double, ByRef mCustomerCode As String, ByRef mCustomerName As String, ByRef mSONo As String, ByRef mSODate As String, ByRef mCustPONo As String, ByRef mCustPODate As String)


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

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') " '& vbCrLf |            & " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |
        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                '            mcntRow = mcntRow + 1
                '            SprdMain.MaxRows = SprdMain.MaxRows + 1
                SprdMain.Row = mcntRow

                j = j + 1
                xSrn = mSrn & "." & j
                pSrn = pSrn & "." & j

                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mSONo, mSODate, mCustPONo, mCustPODate)
                RsShow.MoveNext()
            Loop
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ITEM_QTY AS STD_QTY, ID.SCRAP_QTY AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    '                mcntRow = mcntRow + 1
                    '                SprdMain.MaxRows = SprdMain.MaxRows + 1
                    SprdMain.Row = mcntRow

                    j = j + 1
                    xSrn = mSrn & "." & j
                    pSrn = pSrn & "." & j

                    mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                    Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mSONo, mSODate, mCustPONo, mCustPODate)
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
    Private Function CheckSubRecord(ByRef pProductCode As String) As Boolean


        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        'Dim mRMCode As String
        'Dim mSrn As String
        'Dim xSrn As String
        'Dim j As Long
        '
        CheckSubRecord = False
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.RM_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') " '& vbCrLf |            & " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |
        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            '        Do While Not RsShow.EOF
            '           mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))
            CheckSubRecord = True
            '        Loop
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.ITEM_CODE AS RM_CODE " & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'  AND STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                '            Do While Not RsShow.EOF
                '                mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))
                CheckSubRecord = True
                RsShow.MoveNext()
                '            Loop
            End If
        End If
        RsShow = Nothing
        '        RsShow.Close

        Exit Function
FillERR:
        CheckSubRecord = False
        MsgBox(Err.Description)
        '    Resume
    End Function


    Private Sub FillSubAlterRecord(ByRef pRMMainCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef pDeptCode As String, ByRef pParentCode As String, ByRef mTotalQty As Double, ByRef mTotalRate As Double, ByRef mTotalAmount As Double, ByRef mCustomerCode As String, ByRef mCustomerName As String, ByRef mSONo As String, ByRef mSODate As String, ByRef mCustPONo As String, ByRef mCustPODate As String)


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
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pRMMainCode, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mSONo, mSODate, mCustPONo, mCustPODate)
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
    Public Sub frmParamMaterialVsSalesOrder_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Sales Order Vs Material Stock Report"
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamMaterialVsSalesOrder_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        '    txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        '    CboType.Clear
        '    CboType.AddItem "All"
        '    CboType.AddItem "Sale"
        '    CboType.AddItem "Jobwork"
        '    CboType.ListIndex = 0

        lblRunDate.Text = CStr(RunDate)
        SetDate(CDate(lblRunDate.Text))

        UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)

        chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        chkFG_CheckStateChanged(chkFG, New System.EventArgs())
        chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked

        txtCustomerName.Enabled = False
        cmdsearchCustName.Enabled = False

        chkSOAll.CheckState = System.Windows.Forms.CheckState.Checked

        txtSalesOrder.Enabled = False
        cmdSaleOrder.Enabled = False

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
            .MaxCols = ColFlag
            .set_RowHeight(-1, RowHeight)

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

            .Col = ColSalesOrderNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColSalesOrderNo, 10)
            .ColHidden = IIf(optShow(0).Checked, False, True)

            .Col = ColSalesOrderDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColSalesOrderDate, 10)
            .ColHidden = True

            .Col = ColPartyOrderNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyOrderNo, 10)
            .ColHidden = IIf(optShow(0).Checked, False, True)

            .Col = ColPartyOrderDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyOrderDate, 10)
            .ColHidden = True

            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerName, 15)
            .ColHidden = IIf(optShow(0).Checked, False, True)

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

            .Col = ColProductQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColProductQty, 9)
            .ColHidden = True

            .Col = ColSRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColSRNo, 6)
            .ColHidden = True

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

            .Col = ColStdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStdQty, 7)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            For cntCol = ColTotalQty To ColShortQty
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 9)
            Next

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

            .ColsFrozen = ColUnit

            Call FillHeading()

            mIsGrouped = False
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle

        End With

    End Sub

    Private Sub FillHeading()
        On Error GoTo ErrPart

        With SprdMain
            .MaxCols = ColFlag
            .Row = 0

            .Col = ColTotalQty
            .Text = "Total Required Qty"

            .Col = ColAvailableQty
            .Text = "Total Available Qty"

            .Col = ColShortQty
            .Text = "Short Qty"

            .Col = ColLevel
            .Text = "Level"

            .Col = ColFlag
            .Text = "Flag"

        End With
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamMaterialVsSalesOrder_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

        mTitle = "Sales Order Vs Material Stock Report"
        '    Report1.ReportFileName = App.path & "\Reports\MatBudget.rpt"

        '    mSubTitle = "As On Date : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtFGName.Text) <> "" Then
            mSubTitle = mSubTitle & " [Category : " & txtFGName.Text & "]"
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
        SqlStr = " SELECT DISTINCT SUPP_CUST_NAME " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

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

    Private Sub txtSalesOrder_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSalesOrder.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If MainClass.ValidateWithMasterTable((txtSalesOrder.Text), "AUTO_KEY_SO", "AUTO_KEY_SO", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invalid Sales Order.")
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub UpDYear_DownClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))

        'RefreshScreen
    End Sub
    Private Sub UpDYear_UpClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))

        'RefreshScreen
    End Sub
    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Daysinmonth = MainClass.LastDay(VB6.Format(xDate, "mm"), VB6.Format(xDate, "yyyy"))
    End Sub
End Class
