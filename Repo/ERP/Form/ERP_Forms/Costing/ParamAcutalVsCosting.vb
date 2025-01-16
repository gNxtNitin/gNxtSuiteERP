Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamActualVsCosting
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection	
    Private Const RowHeight As Short = 22

    Private Const ColPicMain As Short = 1
    Private Const ColPicSub As Short = 2
    Private Const ColCustomerCode As Short = 3
    Private Const ColCustomerName As Short = 4
    Private Const ColMainProd As Short = 5
    Private Const ColProductDesc As Short = 6
    Private Const ColProductQty As Short = 7
    Private Const ColSRNo As Short = 8
    Private Const ColRMCode As Short = 9
    Private Const ColRMDesc As Short = 10
    Private Const colStdQty As Short = 11
    Private Const ColUnit As Short = 12
    Private Const ColTotalQty As Short = 13
    Private Const ColTotalRate As Short = 14
    Private Const ColTotalAmount As Short = 15
    Private Const ColTotalMaterialQty As Short = 16
    Private Const ColTotalMaterialRate As Short = 17
    Private Const ColTotalMaterialAmount As Short = 18

    Private Const ColTotalCostingRate As Short = 19
    'Private Const ColTotalCostingAmount = 20	
    'Private Const ColTotalRateDiff = 21	
    'Private Const ColTotalAmountDiff = 22	

    Private Const ColOPCostActual As Short = 20
    Private Const ColOPCostCust As Short = 21
    Private Const ColOthCostActual As Short = 22
    Private Const ColOthCostCust As Short = 23
    Private Const ColProfitActual As Short = 24
    Private Const ColProfitCust As Short = 25
    Private Const ColNetProdCostActual As Short = 26
    Private Const ColNetProdCostCust As Short = 27
    Private Const ColDiffProdCost As Short = 28
    Private Const ColDiffProdAmount As Short = 29


    Private Const ColCategory As Short = 30
    Private Const ColLevel As Short = 31
    Private Const ColFlag As Short = 32


    Dim mClickProcess As Boolean

    Private Const ColCustomerCode2 As Short = 1
    Private Const ColCustomerName2 As Short = 2
    Private Const ColMainProd2 As Short = 3
    Private Const ColProductDesc2 As Short = 4
    Private Const ColUnit2 As Short = 5
    Private Const ColTotalQty2 As Short = 6
    Private Const ColTotalRate2 As Short = 7
    Private Const ColTotalAmount2 As Short = 8
    Private Const ColTotalMaterialRate2 As Short = 9
    Private Const ColTotalMaterialAmount2 As Short = 10
    Private Const ColTotalCostingRate2 As Short = 11
    Private Const ColOPCostActual2 As Short = 12
    Private Const ColOPCostCust2 As Short = 13
    Private Const ColOthCostActual2 As Short = 14
    Private Const ColOthCostCust2 As Short = 15
    Private Const ColProfitActual2 As Short = 16
    Private Const ColProfitCust2 As Short = 17
    Private Const ColNetProdCostActual2 As Short = 18
    Private Const ColNetProdCostCust2 As Short = 19
    Private Const ColDiffProdCost2 As Short = 20
    Private Const ColDiffProdAmount2 As Short = 21


    Dim mActiveRow As Integer
    Dim mcntRow As Integer

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

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
    Private Sub chkFG_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFG.CheckStateChanged
        txtFGName.Enabled = IIf(chkFG.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearchFG.Enabled = IIf(chkFG.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub cmdSearchFG_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchFG.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtFGName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
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

        MainClass.ClearGrid(SprdSummary, RowHeight)
        FormatSprdSummary(-1)

        If Trim(txtDateFrom.Text) = "" Then
            MsgInformation("Please Enter Date.")
            txtDateFrom.Focus()
            Exit Sub
        End If

        If Trim(txtDateTo.Text) = "" Then
            MsgInformation("Please Enter Date.")
            txtDateTo.Focus()
            Exit Sub
        End If

        If optRate(1).Checked = True Then
            If Trim(txtRateAsOn.Text) = "" Then
                MsgInformation("Please Enter Date.")
                txtRateAsOn.Focus()
                Exit Sub
            End If
        End If

        '    If chkAll.Value = vbUnchecked Then	
        '        If Trim(txtWEF.Text) = "" Then	
        '            MsgInformation "Please Enter Date."	
        '            txtWEF.focus	
        '            Exit Sub	
        '        End If	
        '    End If	
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Show1()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    SprdMain.focus	
        Call PrintStatus(True)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim RsBudgetMain As ADODB.Recordset
        Dim SqlStr As String
        'Dim mProdCode As String	
        'Dim mProdName As String	
        Dim mCustCode As String
        'Dim mCustName As String	
        Dim mCheckProdCode As String
        Dim mMonthName As String

        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAll As Boolean

        mMonthName = UCase(MonthName(Month(CDate(txtDateFrom.Text))))

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If optBaseOn(0).Checked = True Then
            SqlStr = " SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.ITEM_CODE,INVMST.ITEM_SHORT_DESC,IH.ITEM_UOM, " & vbCrLf & " SUM(CASE WHEN ID.QTY IS NULL THEN 0 ELSE ID.QTY END) AS TOTAL_QTY, " & vbCrLf & " AVG(CASE WHEN ID.RATE IS NULL THEN 0 ELSE ID.RATE END) AS TOTAL_RATE, " & vbCrLf & " SUM(CASE WHEN ID.VALUE IS NULL THEN 0 ELSE ID.VALUE END) AS TOTAL_VALUE "
        ElseIf optBaseOn(1).Checked = True Then
            SqlStr = " SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM, " & vbCrLf & " SUM(CASE WHEN ID.ITEM_QTY IS NULL THEN 0 ELSE ID.ITEM_QTY END) AS TOTAL_QTY, " & vbCrLf & " CASE WHEN SUM(CASE WHEN ID.ITEM_QTY IS NULL THEN 0 ELSE ID.ITEM_QTY END)=0 THEN 0 ELSE SUM(CASE WHEN ID.ITEM_AMT IS NULL THEN 0 ELSE ID.ITEM_AMT END)/SUM(CASE WHEN ID.ITEM_QTY IS NULL THEN 0 ELSE ID.ITEM_QTY END) END AS TOTAL_RATE, " & vbCrLf & " SUM(CASE WHEN ID.ITEM_AMT IS NULL THEN 0 ELSE ID.ITEM_AMT END) AS TOTAL_VALUE "

        Else
            SqlStr = " SELECT '' AS SUPP_CUST_CODE, '' AS SUPP_CUST_NAME, ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM, " & vbCrLf & " SUM(CASE WHEN ID.OK_QTY IS NULL THEN 0 ELSE ID.OK_QTY END) AS TOTAL_QTY, " & vbCrLf & " 0 AS TOTAL_RATE, " & vbCrLf & " 0 AS TOTAL_VALUE "
        End If

        If optBaseOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " FROM MIS_SALEBUDGET_DET IH, MIS_SALEBUDGET_TRN ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, INV_GENERAL_MST GMAT " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_NO,LENGTH(IH.AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND IH.AUTO_KEY_NO=ID.AUTO_KEY_NO " & vbCrLf & " AND IH.SERIAL_NO=ID.SERIAL_NO "
        ElseIf optBaseOn(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, INV_GENERAL_MST GMAT " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.REF_DESP_TYPE IN ('P','G','E','J') " & vbCrLf & " AND ID.ITEM_CODE IN ( " '''AND IH.REF_DESP_TYPE='P'"	

            SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT PRODUCT_CODE FROM PRD_NEWBOM_HDR WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " UNION " & vbCrLf & " SELECT DISTINCT REF_ITEM_CODE FROM INV_ITEM_RELATIONSHIP_DET WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " )"
        Else
            SqlStr = SqlStr & vbCrLf & " FROM PRD_PMEMO_HDR IH, PRD_PMEMO_DET ID, " & vbCrLf & " INV_ITEM_MST INVMST, INV_GENERAL_MST GMAT " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND IH.AUTO_KEY_PMO=ID.AUTO_KEY_PMO" & vbCrLf & " AND ID.ITEM_CODE IN ( " '''AND IH.REF_DESP_TYPE='P'"	

            SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT PRODUCT_CODE FROM PRD_NEWBOM_HDR WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " UNION " & vbCrLf & " SELECT DISTINCT REF_ITEM_CODE FROM INV_ITEM_RELATIONSHIP_DET WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " )"
        End If

        If optBaseOn(0).Checked = True Or optBaseOn(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE "
        End If

        If optBaseOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.ITEM_CODE=INVMST.ITEM_CODE "
        ElseIf optBaseOn(1).Checked = True Or optBaseOn(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE "
        Else

        End If

        SqlStr = SqlStr & vbCrLf & " AND INVMST.COMPANY_CODE=GMAT.COMPANY_CODE " & vbCrLf & " AND INVMST.CATEGORY_CODE=GMAT.GEN_CODE AND GMAT.GEN_TYPE='C'"

        If optBaseOn(0).Checked = True Or optBaseOn(1).Checked = True Then
            If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCustomerName.Text) <> "" Then
                If MainClass.ValidateWithMasterTable(txtCustomerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCustCode = MasterNo
                    SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "'"
                End If
            End If
        End If

        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCheckProdCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(mCheckProdCode) & "'"
            End If
        End If

        If optBaseOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ID.MONTH_NAME = '" & mMonthName & "'"
        ElseIf optBaseOn(1).Checked = True Then
            If optShowDate(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND IH.DCDATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                SqlStr = SqlStr & vbCrLf & " AND IH.DCDATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.PMO_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            SqlStr = SqlStr & vbCrLf & " AND IH.PMO_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If optBaseOn(0).Checked = True Then
            If cboType.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & " AND GMAT.STOCKTYPE='FG' "
            ElseIf cboType.SelectedIndex = 2 Then
                SqlStr = SqlStr & vbCrLf & " AND GMAT.STOCKTYPE='CS' "
            End If


            SqlStr = SqlStr & vbCrLf & "HAVING (SUM(ID.QTY) IS NOT NULL OR SUM(ID.QTY)>0)"
        ElseIf optBaseOn(1).Checked = True Then
            '        SqlStr = SqlStr & vbCrLf & "HAVING (SUM(ID.QTY) IS NOT NULL OR SUM(ID.QTY)>0)"	


            mShowAll = True
            For CntLst = 1 To lstInvoiceType.Items.Count - 1
                If lstInvoiceType.GetItemChecked(CntLst) = True Then
                    '            lstInvoiceType.ListIndex = CntLst	
                    mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                    If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
                Else
                    mShowAll = False
                End If
            Next

            If mShowAll = False Then
                If mTrnTypeStr <> "" Then
                    mTrnTypeStr = "(" & mTrnTypeStr & ")"
                    SqlStr = SqlStr & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
                End If
            End If
        Else
            If cboType.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & " AND GMAT.STOCKTYPE='FG' "
            ElseIf cboType.SelectedIndex = 2 Then
                SqlStr = SqlStr & vbCrLf & " AND GMAT.STOCKTYPE='CS' "
            End If


            SqlStr = SqlStr & vbCrLf & "HAVING (SUM(ID.OK_QTY) IS NOT NULL OR SUM(ID.OK_QTY)>0)"
        End If

        If optBaseOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,IH.ITEM_CODE,INVMST.ITEM_SHORT_DESC,IH.ITEM_UOM " & vbCrLf & " ORDER BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,IH.ITEM_CODE,INVMST.ITEM_SHORT_DESC,IH.ITEM_UOM "

        ElseIf optBaseOn(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM " & vbCrLf & " ORDER BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM "
        ElseIf optBaseOn(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM " & vbCrLf & " ORDER BY ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM "

        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBudgetMain, ADODB.LockTypeEnum.adLockReadOnly)
        mcntRow = 1

        If RsBudgetMain.EOF = False Then
            Do While Not RsBudgetMain.EOF
                Call ShowDetail(RsBudgetMain, mcntRow)

                '            mcntRow = mcntRow + 1	
                '            SprdMain.MaxRows = SprdMain.MaxRows + 1	
                RsBudgetMain.MoveNext()
            Loop
        End If

        Call FormatSprdMain(-1)
        Call CalcTotal()
        Call FillSummary()
        Call FormatSprdSummary(-1)
        GroupBySpread(ColPicMain)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsBudgetMain = Nothing
        Exit Sub
LedgError:
        ''    Resume	
        MsgInformation(Err.Description)
    End Sub
    Private Sub CalcTotal()
        On Error GoTo LedgError
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mLevel As Integer
        Dim mTotalMaterialQty As Double
        Dim mTotalMaterialRate As Double
        Dim mTotalMaterialAmount As Double
        Dim mSaleQty As Double
        Dim mTotalCostingRate As Double
        Dim mTotalCostingAmount As Double
        Dim mRateDiff As Double
        Dim mAmountDiff As Double


        With SprdMain
            For cntRow = 1 To SprdMain.MaxRows
                .Row = cntRow
                .Col = ColLevel
                mLevel = Val(.Text)

                If mLevel = 1 Then
                    mTotalMaterialQty = 0
                    mTotalMaterialAmount = 0
                    mTotalMaterialRate = 0
                    mTotalCostingAmount = 0
                    mRateDiff = 0
                    mAmountDiff = 0
                    Call GetRowTotal(cntRow, mTotalMaterialQty, 0, mTotalMaterialAmount, mTotalCostingAmount)

                    .Row = cntRow
                    .Col = ColTotalQty
                    mSaleQty = Val(.Text)

                    .Col = ColTotalMaterialQty
                    .Text = VB6.Format(mTotalMaterialQty, "0.00")

                    .Col = ColTotalMaterialRate
                    If mSaleQty = 0 Then
                        mTotalMaterialRate = 0
                    Else
                        mTotalMaterialRate = IIf(mSaleQty = 0, 0, mTotalMaterialAmount / mSaleQty)
                    End If
                    .Text = VB6.Format(mTotalMaterialRate, "0.00")

                    .Col = ColTotalMaterialAmount
                    .Text = VB6.Format(mTotalMaterialAmount, "0.00")

                    .Col = ColTotalCostingRate
                    ''                If mSaleQty = 0 Then	
                    ''                    mTotalCostingRate = 0	
                    ''                Else	
                    ''                    mTotalCostingRate = IIf(mSaleQty = 0, 0, mTotalCostingAmount / mSaleQty)	
                    ''                End If	
                    '                .Text = Format(mTotalCostingRate, "0.00")	
                    mTotalCostingRate = CDbl(VB6.Format(.Text, "0.00"))
                    mTotalCostingAmount = mTotalCostingRate * mSaleQty
                    '	
                    '                .Col = ColTotalCostingAmount	
                    '                .Text = Format(mTotalCostingAmount, "0.00")	
                    '	
                    '                .Col = ColTotalRateDiff	
                    '                .Text = Format(mTotalMaterialRate - mTotalCostingRate, "0.00")	
                    '	
                    '                .Col = ColTotalAmountDiff	
                    '                .Text = Format(mTotalMaterialAmount - mTotalCostingAmount, "0.00")	

                End If
            Next
        End With


        '    Private Const  = 16	
        'Private Const  = 17	
        'Private Const  = 18	
        '	

        Exit Sub
LedgError:
        ''    Resume	
        MsgInformation(Err.Description)
    End Sub


    Private Sub FillSummary()
        On Error GoTo LedgError
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim cntSummaryRow As Integer

        Dim mCustomerCode As String
        Dim mCustomerName As String
        Dim mMainProd As String
        Dim mProductDesc As String
        Dim mUnit As String
        Dim mTotalQty As Double
        Dim mTotalRate As Double
        Dim mTotalAmount As Double
        Dim mTotalMaterialRate As Double
        Dim mTotalMaterialAmount As Double
        Dim mTotalCostingRate As Double
        Dim mOPCostActual As Double
        Dim mOPCostCust As Double
        Dim mOthCostActual As Double
        Dim mOthCostCust As Double
        Dim mProfitActual As Double
        Dim mProfitCust As Double
        Dim mNetProdCostActual As Double
        Dim mNetProdCostCust As Double
        Dim mDiffProdCost As Double
        Dim mDiffProdAmount As Double
        Dim mLevel As Integer


        cntSummaryRow = 1
        With SprdMain
            For cntRow = 1 To SprdMain.MaxRows
                .Row = cntRow
                .Col = ColLevel
                mLevel = Val(.Text)

                If mLevel = 1 Then
                    .Row = cntRow
                    .Col = ColCustomerCode
                    mCustomerCode = Trim(.Text)

                    .Col = ColCustomerName
                    mCustomerName = Trim(.Text)

                    .Col = ColMainProd
                    mMainProd = Trim(.Text)

                    .Col = ColProductDesc
                    mProductDesc = Trim(.Text)

                    .Col = ColUnit
                    mUnit = Trim(.Text)

                    .Col = ColTotalQty
                    mTotalQty = Val(.Text)

                    .Col = ColTotalRate
                    mTotalRate = Val(.Text)

                    .Col = ColTotalAmount
                    mTotalAmount = Val(.Text)

                    .Col = ColTotalMaterialRate
                    mTotalMaterialRate = Val(.Text)

                    .Col = ColTotalMaterialAmount
                    mTotalMaterialAmount = Val(.Text)

                    .Col = ColTotalCostingRate
                    mTotalCostingRate = Val(.Text)

                    .Col = ColOPCostActual
                    mOPCostActual = Val(.Text)

                    .Col = ColOPCostCust
                    mOPCostCust = Val(.Text)

                    .Col = ColOthCostActual
                    mOthCostActual = Val(.Text)

                    .Col = ColOthCostCust
                    mOthCostCust = Val(.Text)

                    .Col = ColProfitActual
                    mProfitActual = Val(.Text)

                    .Col = ColProfitCust
                    mProfitCust = Val(.Text)

                    .Col = ColNetProdCostActual
                    mNetProdCostActual = Val(.Text)

                    .Col = ColNetProdCostCust
                    mNetProdCostCust = Val(.Text)

                    .Col = ColDiffProdCost
                    mDiffProdCost = Val(.Text)

                    .Col = ColDiffProdAmount
                    mDiffProdAmount = Val(.Text)

                    With SprdSummary

                        .Row = cntSummaryRow
                        .Col = ColCustomerCode2
                        .Text = mCustomerCode

                        .Col = ColCustomerName2
                        .Text = mCustomerName

                        .Col = ColMainProd2
                        .Text = mMainProd

                        .Col = ColProductDesc2
                        .Text = mProductDesc

                        .Col = ColUnit2
                        .Text = mUnit

                        .Col = ColTotalQty2
                        .Text = VB6.Format(mTotalQty, "0.00")

                        .Col = ColTotalRate2
                        .Text = VB6.Format(mTotalRate, "0.00")

                        .Col = ColTotalAmount2
                        .Text = VB6.Format(mTotalAmount, "0.00")

                        .Col = ColTotalMaterialRate2
                        .Text = VB6.Format(mTotalMaterialRate, "0.00")

                        .Col = ColTotalMaterialAmount2
                        .Text = VB6.Format(mTotalMaterialAmount, "0.00")

                        .Col = ColTotalCostingRate2
                        .Text = VB6.Format(mTotalCostingRate, "0.00")

                        .Col = ColOPCostActual2
                        .Text = VB6.Format(mOPCostActual, "0.00")

                        .Col = ColOPCostCust2
                        .Text = VB6.Format(mOPCostCust, "0.00")

                        .Col = ColOthCostActual2
                        .Text = VB6.Format(mOthCostActual, "0.00")

                        .Col = ColOthCostCust2
                        .Text = VB6.Format(mOthCostCust, "0.00")

                        .Col = ColProfitActual2
                        .Text = VB6.Format(mProfitActual, "0.00")

                        .Col = ColProfitCust2
                        .Text = VB6.Format(mProfitCust, "0.00")

                        .Col = ColNetProdCostActual2
                        .Text = VB6.Format(mNetProdCostActual, "0.00")

                        .Col = ColNetProdCostCust2
                        .Text = VB6.Format(mNetProdCostCust, "0.00")

                        .Col = ColDiffProdCost2
                        .Text = VB6.Format(mDiffProdCost, "0.00")

                        .Col = ColDiffProdAmount2
                        .Text = VB6.Format(mDiffProdAmount, "0.00")

                        cntSummaryRow = cntSummaryRow + 1
                        .MaxRows = cntSummaryRow
                    End With
                End If
            Next
        End With

        Exit Sub
LedgError:
        ''    Resume	
        MsgInformation(Err.Description)
    End Sub
    Private Function GetRowTotal(ByRef cntRow As Integer, ByRef mTotalMaterialQty As Double, ByRef mTotalMaterialRate As Double, ByRef mTotalMaterialAmount As Double, ByRef mTotalCostingAmount As Double) As Object
        On Error GoTo LedgError
        Dim I As Integer
        Dim mLevel As Integer

        With SprdMain
            For I = cntRow + 1 To SprdMain.MaxRows
                .Row = I
                .Col = ColLevel
                mLevel = Val(.Text)

                If mLevel = 1 Then
                    Exit For
                Else
                    .Row = I
                    .Col = ColTotalMaterialQty
                    mTotalMaterialQty = mTotalMaterialQty + Val(.Text)

                    .Col = ColTotalMaterialAmount
                    mTotalMaterialAmount = mTotalMaterialAmount + Val(.Text)

                    '                .Col = ColTotalCostingAmount	
                    '                mTotalCostingAmount = mTotalCostingAmount + Val(.Text)	
                End If
            Next
        End With

        Exit Function
LedgError:
        ''    Resume	
        MsgInformation(Err.Description)
    End Function
    Private Sub ShowDetail(ByRef mRsBudget As ADODB.Recordset, ByRef mcntRow As Integer)

        On Error GoTo LedgError
        Dim RsMain As ADODB.Recordset
        Dim RsShow As ADODB.Recordset
        Dim SqlStr As String
        Dim mRMCode As String
        Dim mProductCode As String
        Dim mNextProductCode As String
        Dim I As Integer
        Dim mSrn As String
        Dim RsTemp As ADODB.Recordset
        Dim pSqlStr As String
        Dim mLevel As Integer
        Dim mCatCode As String
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
        Dim mStdQty As Double
        Dim xItemUOM As String

        If mRsBudget.EOF = False Then
            mProductCode = Trim(IIf(IsDBNull(mRsBudget.Fields("ITEM_CODE").Value), "", mRsBudget.Fields("ITEM_CODE").Value))
            mMainItemCode = GetMainItemCode(mProductCode)

            mCustomerCode = Trim(IIf(IsDBNull(mRsBudget.Fields("SUPP_CUST_CODE").Value), "", mRsBudget.Fields("SUPP_CUST_CODE").Value))
            mCustomerName = Trim(IIf(IsDBNull(mRsBudget.Fields("SUPP_CUST_NAME").Value), "", mRsBudget.Fields("SUPP_CUST_NAME").Value))


            If optBaseOn(2).Checked = True Then
                xItemUOM = IIf(IsDBNull(mRsBudget.Fields("ITEM_UOM").Value), "", mRsBudget.Fields("ITEM_UOM").Value)
                mTotalQty = CDbl(VB6.Format(IIf(IsDBNull(mRsBudget.Fields("TOTAL_QTY").Value), 0, mRsBudget.Fields("TOTAL_QTY").Value), "0.00"))
                mTotalRate = GetLatestItemCostFromMRR(mProductCode, xItemUOM, 1, (txtDateTo.Text), "S", "FG", "")
                mTotalAmount = CDbl(VB6.Format(mTotalQty * mTotalRate, "0.00"))
            Else
                mTotalQty = CDbl(VB6.Format(IIf(IsDBNull(mRsBudget.Fields("TOTAL_QTY").Value), 0, mRsBudget.Fields("TOTAL_QTY").Value), "0.00"))
                mTotalRate = CDbl(VB6.Format(IIf(IsDBNull(mRsBudget.Fields("TOTAL_RATE").Value), 0, mRsBudget.Fields("TOTAL_RATE").Value), "0.00"))
                mTotalAmount = CDbl(VB6.Format(IIf(IsDBNull(mRsBudget.Fields("TOTAL_VALUE").Value), 0, mRsBudget.Fields("TOTAL_VALUE").Value), "0.00"))
            End If


            SqlStr = " SELECT IH.PRODUCT_CODE, WEF " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "'"
            SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'" '''' AND BOM_TYPE='P' AND IS_EXPORT_ITEM='N'"	
            SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PRODUCT_CODE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockReadOnly)
            '        mcntRow = 1	

            If RsMain.EOF = False Then
                '            Do While Not RsMain.EOF	
                pWEF = Trim(IIf(IsDBNull(RsMain.Fields("WEF").Value), "", RsMain.Fields("WEF").Value))

                SqlStr = ""
                SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) As STD_QTY, ID.GROSS_WT_SCRAP * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) AS GROSS_WT_SCRAP, " & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MINIMUM_QTY, MAXIMUM_QTY "

                SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "' " & vbCrLf & " AND IH.WEF=TO_DATE('" & VB6.Format(pWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


                SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"
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
                        If optCalcOn(0).Checked = True Then
                            mStdQty = (Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                        Else
                            mStdQty = ((Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)) + Val(IIf(IsDBNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value))))
                        End If
                        Call FillGridCol(RsShow, mSrn, mLevel, mProductCode, mProductCode, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mStdQty)

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
        Dim prevtext As Object
        Dim lastheaderrow As Integer
        Dim ret As Boolean
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
            If Currentid = "1" Then '''<> lastid Then	
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


    Private Sub InsertHeaderRow(ByRef rownum As Integer, ByRef pRecordCount As Integer)
        'Insert a header row at the specifed location	

        '    SprdMain.InsertRows rownum, 1	

        SprdMain.Col = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray	
        SprdMain.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue	
        SprdMain.Font = VB6.FontChangeBold(SprdMain.Font, True)

        MakePictureCellType(rownum, ColPicMain)

        SprdMain.Col = ColPicMain
        SprdMain.TypePictPicture = minuspict
        SprdMain.Col = ColPicSub
        SprdMain.Text = ""

        'Add picture state values	
        SprdMain.Col = ColFlag
        SprdMain.Text = "0"

        'Add Border	

        SprdMain.SetCellBorder(ColPicMain, rownum, SprdMain.MaxCols, rownum, SS_BORDER_TYPE_OUTLINE, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

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
    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pSRNo As String, ByRef pLevel As Integer, ByRef pProductCode As String, ByRef pParentCode As String, ByRef mTotalQty As Double, ByRef mTotalRate As Double, ByRef mTotalAmount As Double, ByRef mCustomerCode As String, ByRef mCustomerName As String, ByRef mStqQty As Double)

        On Error GoTo FillGERR
        Dim mDrgRevNo As String
        Dim mRMCode As String
        Dim mItemUOM As String
        Dim mStockQty As Double
        Dim mDeptCode As String
        Dim mRate As Double
        Dim mFactorQty As Double
        Dim mChildRMCode As String
        'Dim mStqQty As Double	
        Dim mTotValue As Double
        Dim mUOM As String
        Dim mTotClosing As Double

        Dim mCostingRate As Double
        Dim mCostingAmount As Double
        Dim mMaterialAmount As Double
        Dim mDateAsOn As String

        With SprdMain
            .Row = .MaxRows
            .Col = ColMainProd
            .Text = pProductCode
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))


            If MainClass.ValidateWithMasterTable(pProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                .Row = .MaxRows
                .Col = ColProductDesc
                .Text = MasterNo
                .FontBold = IIf(pLevel = 1, True, False)


                If pLevel = 1 Then
                    .Col = ColRMCode
                    .Text = pProductCode
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColCategory
                    .Text = GetItemCategory(pProductCode)
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColRMDesc
                    .Text = MasterNo
                    .FontBold = IIf(pLevel = 1, True, False)

                    '                .Col = ColSRNo	
                    '                .Text = pSRNo	
                    '                .FontBold = IIf(pLevel = 1, True, False)	

                    .Col = ColLevel
                    .Text = Str(pLevel)

                    .Col = ColUnit
                    .Text = IIf(IsDBNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
                    mItemUOM = IIf(IsDBNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColCustomerCode
                    .Text = mCustomerCode
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColCustomerName
                    .Text = mCustomerName
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColTotalQty
                    .Text = Format(mTotalQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColTotalRate
                    .Text = Format(mTotalRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColTotalAmount
                    .Text = Format(mTotalAmount, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColTotalCostingRate
                    .Text = GetCustomerCost(mCustomerCode, pProductCode, "MTR")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColOPCostCust
                    .Text = GetCustomerCost(mCustomerCode, pProductCode, "OPR")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColOthCostCust
                    .Text = GetCustomerCost(mCustomerCode, pProductCode, "OTH")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColProfitCust
                    .Text = GetCustomerCost(mCustomerCode, pProductCode, "PRO")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColNetProdCostCust
                    .Text = GetCustomerCost(mCustomerCode, pProductCode, "TOT")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColFlag
                    .Text = "0"

                    .MaxRows = .MaxRows + 1
                    mcntRow = mcntRow + 1
                    .Row = mcntRow
                    pLevel = pLevel + 1
                    '                pSRNo = pSRNo + 1	

                    .Col = ColMainProd
                    .Text = pProductCode
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColProductDesc
                    .Text = MasterNo
                    .FontBold = IIf(pLevel = 1, True, False)

                    '                SprdMain.MaxRows = SprdMain.MaxRows + 1	
                End If
            End If
            mRMCode = Trim(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
            If CheckSubRecord(mRMCode) = True Then
                pLevel = pLevel + 1
                Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mStqQty)

            Else
                .Row = .MaxRows
                .Col = ColSRNo
                .Text = pSRNo
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                mDeptCode = IIf(IsDBNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)
                mItemUOM = IIf(IsDBNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)

                .Col = ColRMCode
                .Text = Trim(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
                mRMCode = Trim(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColRMDesc
                .Text = IIf(IsDBNull(pRs.Fields("ITEM_SHORT_DESC").Value), "", pRs.Fields("ITEM_SHORT_DESC").Value)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColCategory
                .Text = GetItemCategory(mRMCode)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = colStdQty
                '            If optCalcOn(0).Value = True Then	
                '                mStqQty = mStqQty * (Val(IIf(IsNull(pRs!STD_QTY), 0, pRs!STD_QTY)))	
                '            Else	
                '                mStqQty = mStqQty * ((Val(IIf(IsNull(pRs!STD_QTY), 0, pRs!STD_QTY)) + Val(IIf(IsNull(pRs!GROSS_WT_SCRAP), 0, pRs!GROSS_WT_SCRAP))))	
                '            End If	

                mFactorQty = 1
                '            If mDeptCode = "J/W" Then	
                '                If mItemUOM = "TON" Then	
                '                    mFactorQty = 1 / 1000	
                '                End If	
                '            Else	
                '                If mItemUOM = "KGS" Then	
                '                    mFactorQty = 1 / 1000	
                '                ElseIf mItemUOM = "TON" Then	
                '                    mFactorQty = 1 / 1000	
                '                    mFactorQty = mFactorQty / 1000	
                '                End If	
                '            End If	

                .Text = CStr(mStqQty * mFactorQty)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))


                .Col = ColLevel
                .Text = Str(pLevel)

                .Col = ColUnit
                mUOM = IIf(IsDBNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
                .Text = IIf(IsDBNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColCustomerCode
                .Text = mCustomerCode
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColCustomerName
                .Text = mCustomerName
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                '            mRate = GetCurrentItemRate(mRMCode, Format(lblRunDate.text, "DD/MM/YYYY"))	

                .Col = ColTotalMaterialQty
                .Text = VB6.Format(mTotalQty * Val(CStr(mStqQty * mFactorQty)), "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                mTotClosing = CDbl(VB6.Format(mTotalQty * Val(CStr(mStqQty * mFactorQty)), "0.00"))
                mTotClosing = IIf(mTotClosing <= 0, 1, mTotClosing)

                mRate = 0
                mCostingRate = 0
                mCostingAmount = 0
                mMaterialAmount = 0
                If chkRate.CheckState = System.Windows.Forms.CheckState.Checked Then

                    If optRate(0).Checked = True Then
                        mDateAsOn = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
                    Else
                        mDateAsOn = VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY")
                    End If
                    mTotValue = GetLatestItemCostFromMRR(Trim(mRMCode), mUOM, mTotClosing, VB6.Format(mDateAsOn, "DD/MM/YYYY"), "L",  ,  , "Y")

                    If mTotClosing > 0 Then
                        mRate = mTotValue / mTotClosing
                    End If

                    mCostingRate = GetCustomerCostingItemRate(mCustomerCode, pProductCode, mRMCode, mUOM, mDateAsOn)
                End If


                .Row = .MaxRows
                .Col = ColTotalMaterialRate
                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColTotalMaterialAmount
                .Text = VB6.Format(mRate * mTotalQty * Val(CStr(mStqQty * mFactorQty)), "0.00")
                mMaterialAmount = CDbl(VB6.Format(mRate * mTotalQty * Val(CStr(mStqQty * mFactorQty)), "0.00"))
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))


                .Col = ColTotalCostingRate
                .Text = VB6.Format(mCostingRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                '            .Col = ColTotalCostingAmount	
                '            mCostingAmount = Format(mCostingRate * mTotalQty * Val(mStqQty * mFactorQty), "0.00")	
                '            .Text = Format(mCostingAmount, "0.00")	
                '            .FontBold = IIf(pLevel = 1, True, False)	

                '            .Col = ColTotalRateDiff	
                '            .Text = Format(mRate - mCostingRate, "0.00")	
                '            .FontBold = IIf(pLevel = 1, True, False)	
                '	
                '            .Col = ColTotalAmountDiff	
                '            .Text = Format(mMaterialAmount - mCostingAmount, "0.00")	
                '            .FontBold = IIf(pLevel = 1, True, False)	

                .Col = ColFlag
                .Text = "0"

                mStqQty = 1

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
        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset

        pSqlStr = "SELECT DISTINCT PRODUCT_CODE " & vbCrLf _
        & " FROM PRD_NEWBOM_DET ID " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND PRODUCT_CODE<>'" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf _
        & " AND RM_CODE='" & MainClass.AllowSingleQuote(mRMCode) & "'" & vbCrLf _
        & " ORDER BY " & vbCrLf _
        & " PRODUCT_CODE"


        'AND STATUS='O'	

        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                If GetCommonFinishedGood = "" Then
                    GetCommonFinishedGood = Trim(IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                Else
                    GetCommonFinishedGood = GetCommonFinishedGood & ", " & Trim(IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
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


    Private Sub frmParamActualVsCosting_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        SprdSummary.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        SSTab1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1	
        '    MainClass.SetSpreadColor SprdOption, -1	
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub optRate_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optRate.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optRate.GetIndex(eventSender)
            txtRateAsOn.Enabled = IIf(Index = 0, False, True)
            txtRateAsOn.Visible = IIf(Index = 0, False, True)
        End If
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
        Dim SqlStr As String


        If txtCustomerName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

        If MainClass.ValidateWithMasterTable(txtCustomerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster(txtCustomerName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
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






    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef mTotalQty As Double, ByRef mTotalRate As Double, ByRef mTotalAmount As Double, ByRef mCustomerCode As String, ByRef mCustomerName As String, ByRef mStqQty As Double)


        On Error GoTo FillERR
        Dim SqlStr As String
        Dim RsShow As ADODB.Recordset
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String
        Dim j As Integer

        mSrn = pSrn
        pLevel = pLevel + 1
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " " & mStqQty & " * ID.STD_QTY * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) AS STD_QTY, " & mStqQty & " * ID.GROSS_WT_SCRAP * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

        SqlStr = SqlStr & vbCrLf _
        & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
        & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
        & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
        & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
        & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O'" & vbCrLf _
        & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') "       '& vbCrLf _	
        '& " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf _	

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

                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                If optCalcOn(0).Checked = True Then
                    mStqQty = (Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                Else
                    mStqQty = ((Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)) + Val(IIf(IsDBNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value))))
                End If
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mStqQty)
                RsShow.MoveNext()
            Loop
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " " & mStqQty & " * ID.ITEM_QTY * (CASE WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 ELSE 1 END) AS STD_QTY, " & mStqQty & " * ID.SCRAP_QTY * (CASE WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 ELSE 1 END) AS GROSS_WT_SCRAP ," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O' AND IS_INHOUSE='N'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') "

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

                    If optCalcOn(0).Checked = True Then
                        mStqQty = (Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                    Else
                        mStqQty = ((Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)) + Val(IIf(IsDBNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value))))
                    End If
                    mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                    Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mStqQty)
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
        Dim SqlStr As String
        Dim RsShow As ADODB.Recordset
        'Dim mRMCode As String	
        'Dim mSrn As String	
        'Dim xSrn As String	
        'Dim j As Long	
        '	
        CheckSubRecord = False
        SqlStr = " SELECT " & vbCrLf _
        & " IH.PRODUCT_CODE, ID.RM_CODE " & vbCrLf _
        & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID" & vbCrLf _
        & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
        & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O' AND IH.IS_BOP='N'" & vbCrLf _
        & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O' ) "       '& vbCrLf _	
        '& " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf _	

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            '        Do While Not RsShow.EOF	
            '           mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))	
            CheckSubRecord = True
            '        Loop	
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.ITEM_CODE AS RM_CODE " & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'  AND STATUS='O' AND IS_INHOUSE='N' AND IH.IS_BOP='N'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') "

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


    Private Sub FillSubAlterRecord(ByRef pRMMainCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef pDeptCode As String, ByRef pParentCode As String, ByRef mAprQty As Double, ByRef mAprRate As Double, ByRef mAprAmount As Double, ByRef mMayQty As Double, ByRef mMayRate As Double, ByRef mMayAmount As Double, ByRef mJunQty As Double, ByRef mJunRate As Double, ByRef mJunAmount As Double, ByRef mJulQty As Double, ByRef mJulRate As Double, ByRef mJulAmount As Double, ByRef mAugQty As Double, ByRef mAugRate As Double, ByRef mAugAmount As Double, ByRef mSepQty As Double, ByRef mSepRate As Double, ByRef mSepAmount As Double, ByRef mOctQty As Double, ByRef mOctRate As Double, ByRef mOctAmount As Double, ByRef mNovQty As Double, ByRef mNovRate As Double, ByRef mNovAmount As Double, ByRef mDecQty As Double, ByRef mDecRate As Double, ByRef mDecAmount As Double, ByRef mJanQty As Double, ByRef mJanRate As Double, ByRef mJanAmount As Double, ByRef mFebQty As Double, ByRef mFebRate As Double, ByRef mFebAmount As Double, ByRef mMarQty As Double, ByRef mMarRate As Double, ByRef mMarAmount As Double, ByRef mTotalQty As Double, ByRef mTotalRate As Double, ByRef mTotalAmount As Double, ByRef mCustomerCode As String, ByRef mCustomerName As String, ByRef mStqQty As Double)


        On Error GoTo FillERR
        Dim SqlStr As String
        Dim RsShow As ADODB.Recordset
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String

        mSrn = pSrn
        '    pLevel = pLevel + 1	

        If pDeptCode <> "J/W" Then
            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_RM_CODE AS RM_CODE, '(*) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_STD_QTY AS STD_QTY, ID.ALETRSCRAP AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET IDET, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=IDET.MKEY " & vbCrLf _
            & " AND IDET.MKEY=ID.MKEY " & vbCrLf _
            & " AND IDET.RM_CODE=ID.MAINITEM_CODE " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ALTER_RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(pRMMainCode) & "'" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pMainProductCode) & "' AND STATUS='O' " & vbCrLf _
            & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pMainProductCode) & "') "       '& vbCrLf _	
            '& " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf _	

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_ITEM_CODE AS RM_CODE, '(**) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_ITEM_QTY AS STD_QTY, ID.ALTER_SCRAP_QTY AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf _
            & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " and ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ALTER_ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pRMMainCode) & "' " & vbCrLf _
            & " AND ID.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "' AND IS_INHOUSE='N' AND STATUS='O'" & vbCrLf _
            & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "') "

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

                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                If optCalcOn(0).Checked = True Then
                    mStqQty = mStqQty * (Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                Else
                    mStqQty = mStqQty * ((Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)) + Val(IIf(IsDBNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value))))
                End If
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pRMMainCode, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mStqQty)
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
    Public Sub frmParamActualVsCosting_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Customer Costing Vs Actual Register (Sale Wise)"
        FormatSprdMain(-1)
        FormatSprdSummary(-1)
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamActualVsCosting_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection	
        ''PvtDBCn.Open StrConn	
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7440
        CurrFormWidth = 11625

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7440)	
        'Me.Width = VB6.TwipsToPixelsX(11625)	


        '    txtDateFrom.Text = Format(RsCompany!START_DATE, "DD/MM/YYYY")	
        '    txtDateTo.Text = Format(RunDate, "DD/MM/YYYY")	

        cboType.Items.Clear()
        cboType.Items.Add("All")
        cboType.Items.Add("Sale")
        cboType.Items.Add("Jobwork")
        cboType.SelectedIndex = 0

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        chkFG_CheckStateChanged(chkFG, New System.EventArgs())
        chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked

        txtCustomerName.Enabled = False
        cmdsearchCustName.Enabled = False

        Call FillInvoiceType()

        minuspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\minus1.bmp")
        pluspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\plus1.bmp")

        mIsGrouped = True

        SSTab1.SelectedIndex = 0
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
            .ColHidden = False

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

            .Col = ColCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = False

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

            .Col = colStdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(colStdQty, 7)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            For cntCol = ColTotalQty To ColDiffProdAmount
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

    Private Sub FormatSprdSummary(ByRef mRow As Integer)

        Dim cntCol As Integer

        With SprdSummary
            .MaxCols = ColDiffProdAmount2
            .set_RowHeight(-1, RowHeight)

            .Row = -1
            .set_ColWidth(0, 4)

            .Col = ColCustomerCode2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColCustomerName2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerName2, 15)

            .Col = ColMainProd2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColProductDesc2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColUnit2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit2, 4)

            For cntCol = ColTotalQty2 To ColDiffProdAmount2
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 9)
            Next

            MainClass.SetSpreadColor(SprdSummary, -1)
            MainClass.ProtectCell(SprdSummary, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdSummary.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle	

        End With

    End Sub
    Private Sub FillHeading()
        On Error GoTo ErrPart

        With SprdMain
            .MaxCols = ColFlag
            .Row = 0

            '        .Col = ColTotalQty	
            '        .Text = "Total Qty"	
            '	
            '        .Col = ColTotalRate	
            '        .Text = "Total Rate"	
            '	
            '        .Col = ColTotalAmount	
            '        .Text = "Total Amount"	
            '	
            '        .Col = ColTotalMaterialQty	
            '        .Text = "Total Material Qty"	
            '	
            '        .Col = ColTotalMaterialRate	
            '        .Text = "Total Material Rate"	
            '	
            '        .Col = ColTotalMaterialAmount	
            '        .Text = "Total Material Amount"	

            .Col = ColLevel
            .Text = "Level"

            .Col = ColFlag
            .Text = "Flag"

            .Col = ColCategory
            .Text = "Category"

        End With
        Exit Sub
ErrPart:
        '    Resume	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer

        lstInvoiceType.Items.Clear()
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='S' ORDER BY NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstInvoiceType.Items.Add("ALL")
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("Name").Value)
                lstInvoiceType.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub lstInvoiceType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstInvoiceType.ItemCheck
        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstInvoiceType.GetItemChecked(0) = True Then
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstInvoiceType.GetItemChecked(e.Index - 1) = False Then
                    lstInvoiceType.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmParamActualVsCosting_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnStock(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnStock(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String

        Report1.Reset()
        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1



        '*************** Fetching Record For Report ***************************	
        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mTitle = "Productwise Stock Statement"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MatBudget.rpt"

        '    mSubTitle = "As On Date : " & vb6.Format(txtDateTo.Text, "DD/MM/YYYY")	

        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtFGName.Text) <> "" Then
            mSubTitle = mSubTitle & " [Category : " & txtFGName.Text & "]"
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Exit Sub



ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim ii As Integer

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

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            Exit Sub
        End If

        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            Exit Sub
        End If


        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
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


        If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
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
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        '    If MainClass.ValidateWithMasterTable() = True Then	
        '	
        '    End If	

        GetVendorName = ""

        SqlStr = " SELECT DISTINCT SUPP_CUST_NAME " & vbCrLf _
        & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST" & vbCrLf _
        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        & " AND IH.MKEY=ID.MKEY" & vbCrLf _
        & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
        & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
        & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_PO,LENGTH(IH.AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            Do While RsTemp.EOF = False
                GetVendorName = IIf(GetVendorName = "", "", GetVendorName & ", ") & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
ErrPart:
        GetVendorName = ""
    End Function

    Private Function GetCustomerCost(ByRef mCustomerCode As String, ByRef pProductCode As String, ByRef pType As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mField As String

        GetCustomerCost = 0
        If pType = "MTR" Then
            mField = "TOT_RM_COST + TOT_BOP_COST"
        ElseIf pType = "OPR" Then
            mField = "TOT_OPR_COST + TOT_VALUE_ADD"
        ElseIf pType = "OTH" Then
            mField = "OVERHEAD_COST+TOT_PACK_COST+REJ_COST + TRANSPORT_COST + TOT_TOOL_COST"
        ElseIf pType = "PRO" Then
            mField = "PROFIT_COST"
        ElseIf pType = "TOT" Then
            mField = "TOT_RM_COST + TOT_BOP_COST + TOT_OPR_COST + TOT_VALUE_ADD+ OVERHEAD_COST+TOT_PACK_COST+REJ_COST + TRANSPORT_COST + TOT_TOOL_COST +PROFIT_COST"
        End If

        SqlStr = " SELECT  " & mField & " AS TOT_AMOUNT" & vbCrLf _
        & " FROM PRD_CUST_FG_COST_HDR IH" & vbCrLf _
        & " WHERE IH.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf _
        & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'"

        SqlStr = SqlStr & vbCrLf _
        & " AND IH.MKEY = (SELECT MAX(MKEY) FROM PRD_CUST_FG_COST_HDR" & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf _
        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf _
        & " AND WEF <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            GetCustomerCost = IIf(IsDBNull(RsTemp.Fields("TOT_AMOUNT").Value), 0, RsTemp.Fields("TOT_AMOUNT").Value)
        End If

        Exit Function
ErrPart:
        GetCustomerCost = 0
    End Function
    Private Sub txtRateAsOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRateAsOn.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtRateAsOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRateAsOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtRateAsOn) = False Then
            txtRateAsOn.Focus()
            Cancel = True
            Exit Sub
        End If
        eventArgs.Cancel = Cancel
    End Sub
End Class
