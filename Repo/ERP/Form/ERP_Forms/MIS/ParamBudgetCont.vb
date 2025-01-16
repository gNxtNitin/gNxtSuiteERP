Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamBudgetCont
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection	
    Private Const RowHeight As Short = 22

    Private Const ColCustomerCode As Short = 1
    Private Const ColCustomerName As Short = 2
    Private Const ColMainProd As Short = 3
    Private Const ColProductDesc As Short = 4
    Private Const ColProductQty As Short = 5
    Private Const ColProductRate As Short = 6
    Private Const ColProductAmount As Short = 7
    Private Const ColRMQty As Short = 8
    Private Const ColRMAmount As Short = 9
    Private Const ColContributionAmount As Short = 10
    Private Const ColContributionPer As Short = 11
    Dim mRMQty As Double
    Dim mRMAmount As Double

    Dim mActiveRow As Integer
    Dim mcntRow As Integer

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
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdSearchFG_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchFG.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtFGName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtFGName.Text = AcName
            txtFGName_Validating(txtFGName, New System.ComponentModel.CancelEventArgs(False)) ''_Validate False
            txtFGName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        MainClass.ClearGrid(SprdMain, RowHeight)
        FormatSprdMain(-1)

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
        Dim RsBudgetMain As ADODB.Recordset
        Dim SqlStr As String = ""
        'Dim mProdCode As String	
        'Dim mProdName As String	
        Dim mCustCode As String
        'Dim mCustName As String	
        Dim mCheckProdCode As String
        Dim mMonthName As String

        Dim mCustomerCode As String
        Dim mCustomerName As String
        Dim mProductCode As String
        Dim mSaleQty As Double
        Dim mSaleRate As Double
        Dim mSaleAmount As Double


        mMonthName = UCase(MonthName(Month(CDate(lblRunDate.Text))))

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If optBaseOn(0).Checked = True Then
            SqlStr = " SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.ITEM_CODE,INVMST.ITEM_SHORT_DESC, " & vbCrLf & " SUM(ID.QTY) AS SALES_QTY, " & vbCrLf & " AVG(ID.RATE) AS SALES_RATE, " & vbCrLf & " SUM(ID.VALUE) AS SALES_VALUE " & vbCrLf

            SqlStr = SqlStr & vbCrLf & " FROM MIS_SALEBUDGET_DET IH, MIS_SALEBUDGET_TRN ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, INV_GENERAL_MST GMAT " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_NO,LENGTH(IH.AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND IH.AUTO_KEY_NO=ID.AUTO_KEY_NO " & vbCrLf & " AND IH.SERIAL_NO=ID.SERIAL_NO " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.ITEM_CODE=INVMST.ITEM_CODE "
        Else
            SqlStr = " SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC, " & vbCrLf & " SUM(ID.ITEM_QTY) AS SALES_QTY, " & vbCrLf & " AVG(ID.ITEM_RATE) AS SALES_RATE, " & vbCrLf & " SUM(ID.ITEM_AMT) AS SALES_VALUE " & vbCrLf

            SqlStr = SqlStr & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, INV_GENERAL_MST GMAT " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE AND IH.REF_DESP_TYPE IN ('P','G','E') AND CANCELLED='N'" & vbCrLf & " AND ID.ITEM_CODE IN (SELECT DISTINCT PRODUCT_CODE FROM PRD_NEWBOM_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " UNION SELECT DISTINCT REF_ITEM_CODE FROM INV_ITEM_RELATIONSHIP_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"
        End If

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

        If optBaseOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ID.MONTH_NAME = '" & mMonthName & "'"
        ElseIf optBaseOn(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.INVOICE_DATE,'YYYYMM') ='" & VB6.Format(lblRunDate.Text, "YYYYMM") & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.INVOICE_DATE,'YYYYMM') <='" & VB6.Format(lblRunDate.Text, "YYYYMM") & "'"
        End If

        If cboType.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND GMAT.STOCKTYPE='FG' "
        ElseIf cboType.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND GMAT.STOCKTYPE='CS' "
        End If

        If optBaseOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "HAVING (SUM(ID.QTY) IS NOT NULL OR SUM(ID.QTY)>0)"

            SqlStr = SqlStr & vbCrLf & " GROUP BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,IH.ITEM_CODE,INVMST.ITEM_SHORT_DESC " & vbCrLf & " ORDER BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,IH.ITEM_CODE,INVMST.ITEM_SHORT_DESC "

        Else
            SqlStr = SqlStr & vbCrLf & " GROUP BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC " & vbCrLf & " ORDER BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC "
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBudgetMain, ADODB.LockTypeEnum.adLockReadOnly)
        mcntRow = 1

        If RsBudgetMain.EOF = False Then
            Do While Not RsBudgetMain.EOF

                SprdMain.Row = mcntRow
                SprdMain.Col = ColCustomerCode
                SprdMain.Text = Trim(IIf(IsDbNull(RsBudgetMain.Fields("SUPP_CUST_CODE").Value), "", RsBudgetMain.Fields("SUPP_CUST_CODE").Value))

                SprdMain.Col = ColCustomerName
                SprdMain.Text = Trim(IIf(IsDbNull(RsBudgetMain.Fields("SUPP_CUST_NAME").Value), "", RsBudgetMain.Fields("SUPP_CUST_NAME").Value))

                SprdMain.Col = ColMainProd
                mProductCode = Trim(IIf(IsDbNull(RsBudgetMain.Fields("ITEM_CODE").Value), "", RsBudgetMain.Fields("ITEM_CODE").Value))
                SprdMain.Text = mProductCode

                SprdMain.Col = ColProductDesc
                SprdMain.Text = Trim(IIf(IsDbNull(RsBudgetMain.Fields("ITEM_SHORT_DESC").Value), "", RsBudgetMain.Fields("ITEM_SHORT_DESC").Value))

                SprdMain.Col = ColProductQty
                mSaleQty = CDbl(VB6.Format(IIf(IsDbNull(RsBudgetMain.Fields("SALES_QTY").Value), 0, RsBudgetMain.Fields("SALES_QTY").Value), "0.00"))
                SprdMain.Text = CStr(mSaleQty)

                SprdMain.Col = ColProductRate
                mSaleRate = CDbl(VB6.Format(IIf(IsDbNull(RsBudgetMain.Fields("SALES_RATE").Value), 0, RsBudgetMain.Fields("SALES_RATE").Value), "0.00"))
                SprdMain.Text = CStr(mSaleRate)

                SprdMain.Col = ColProductAmount
                mSaleAmount = CDbl(VB6.Format(IIf(IsDbNull(RsBudgetMain.Fields("SALES_VALUE").Value), 0, RsBudgetMain.Fields("SALES_VALUE").Value), "0.00"))
                SprdMain.Text = CStr(mSaleAmount)


                mRMQty = 0
                mRMAmount = 0

                Call ShowDetail(mProductCode, mSaleQty)

                SprdMain.Col = ColRMQty
                SprdMain.Text = CStr(mSaleQty * mRMQty)

                SprdMain.Col = ColRMAmount
                SprdMain.Text = CStr(mSaleQty * mRMAmount)

                SprdMain.Col = ColContributionAmount
                SprdMain.Text = CStr(mSaleAmount - (mSaleQty * mRMAmount))

                SprdMain.Col = ColContributionPer
                If mSaleAmount = 0 Then
                    SprdMain.Text = CStr(0)
                Else
                    SprdMain.Text = CStr((mSaleAmount - (mSaleQty * mRMAmount)) / mSaleAmount)
                End If

                RsBudgetMain.MoveNext()
                If RsBudgetMain.EOF = False Then
                    mcntRow = mcntRow + 1
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                End If
            Loop
        End If

        Call FormatSprdMain(-1)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsBudgetMain = Nothing
        Exit Sub
LedgError:
        ''    Resume	
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamBudgetCont_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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

    Private Sub optRate_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optRate.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optRate.GetIndex(eventSender)
            txtRateAsOn.Enabled = IIf(Index = 0, False, True)
            txtRateAsOn.Visible = IIf(Index = 0, False, True)
        End If
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
    Private Sub ShowDetail(ByRef mProductCode As String, ByRef pSaleQty As Double)

        On Error GoTo LedgError
        Dim RsMain As ADODB.Recordset
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mRMCode As String
        '	
        'Dim mNextProductCode As String	
        'Dim I As Long	
        'Dim mSrn As String	
        'Dim RsTemp As ADODB.Recordset = Nothing	
        'Dim pSqlStr As String	
        Dim mRate As Double
        'Dim mCatCode As String=""	
        'Dim mSubCatCode As String	
        Dim pWEF As String
        Dim mRMIssueUOM As String
        Dim mStdQty As Double

        'Dim mCheckProdCode As String	
        'Dim mCheckRMCode As String	
        Dim mMainItemCode As String
        'Dim mStdQty As Double	

        mRMQty = 0
        mRMAmount = 0
        mMainItemCode = GetMainItemCode(mProductCode)

        SqlStr = " SELECT IH.PRODUCT_CODE, WEF " & vbCrLf _
                & " FROM PRD_NEWBOM_HDR IH" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "'" & vbCrLf _
                & " AND IH.STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY WEF"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockReadOnly)
        '        mcntRow = 0	

        If RsMain.EOF = False Then
            '            Do While Not RsMain.EOF	
            pWEF = Trim(IIf(IsDBNull(RsMain.Fields("WEF").Value), "", RsMain.Fields("WEF").Value))

            SqlStr = ""
            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, ID.STD_QTY * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) As STD_QTY, ID.DEPT_CODE, " & vbCrLf & " ID.GROSS_WT_SCRAP * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) As GROSS_WT_SCRAP"

            SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST " & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.RM_CODE=INVMST.ITEM_CODE"

            SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "' " & vbCrLf _
                & " AND IH.WEF=TO_DATE('" & VB6.Format(pWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.STATUS='O'"


            '                SqlStr = SqlStr & vbCrLf & " AND ID.RM_CODE NOT LIKE 'P%'"	

            SqlStr = SqlStr & vbCrLf & " AND ID.RM_CODE  IN (" & vbCrLf & " SELECT ITEM_CODE FROM INV_ITEM_MST A, INV_GENERAL_MST B" & vbCrLf & " WHERE A.COMPANY_CODE='" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf & " AND A.CATEGORY_CODE=B.GEN_CODE" & vbCrLf & " AND B.PRD_TYPE IN ('R','B','I','D','P','2','3')" & vbCrLf & " AND A.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=ID.RM_CODE )"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.PRODUCT_CODE, ID.SUBROWNO"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                Do While Not RsShow.EOF

                    If optCalcOn(0).Checked = True Then
                        mStdQty = (Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                    Else
                        mStdQty = ((Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)) + Val(IIf(IsDBNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value))))
                    End If
                    Call FillGridCol(RsShow, mProductCode, mProductCode, pSaleQty, mStdQty)

                    mcntRow = mcntRow + 1
                    '                        SprdMain.MaxRows = SprdMain.MaxRows + 1	
                    RsShow.MoveNext()
                Loop
            End If
            '                RsMain.MoveNext	
            '            Loop	
        End If


        RsShow = Nothing
        Exit Sub
LedgError:
        ''    Resume	
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pProductCode As String, ByRef pParentCode As String, ByRef pSaleQty As Double, ByRef mStdQty As Double)

        On Error GoTo FillGERR
        Dim mRMCode As String
        Dim mItemUOM As String
        Dim mRate As Double
        'Dim mStdQty As Double	
        Dim mDeptCode As String
        Dim mTotClosing As Double
        Dim mTotValue As Double

        mRMCode = IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
        If CheckSubRecord(mRMCode) = True Then
            Call FillSubRecord(mRMCode, "", pProductCode, pSaleQty, mStdQty)
        Else
            If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemUOM = MasterNo
            End If

            '            If optCalcOn(0).Value = True Then	
            '                mStdQty = Val(IIf(IsNull(pRs!STD_QTY), 0, pRs!STD_QTY))	
            '            Else	
            '                mStdQty = Val(IIf(IsNull(pRs!STD_QTY), 0, pRs!STD_QTY)) + Val(IIf(IsNull(pRs!GROSS_WT_SCRAP), 0, pRs!GROSS_WT_SCRAP))	
            '            End If	
            '            mRMQty = mRMQty + Val(IIf(IsNull(pRs!STD_QTY), "", pRs!STD_QTY)) '	

            mDeptCode = IIf(IsDBNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)

            '            If mDeptCode = "J/W" Then	
            ''                If mItemUOM = "TON" Then	
            ''                    mSTDQty = mSTDQty / 1000	
            ''                    mSTDQty = mSTDQty / 1000	
            ''                End If	
            '            Else	
            '                If mItemUOM = "KGS" Then	
            '                    mStdQty = mStdQty / 1000	
            '                ElseIf mItemUOM = "TON" Then	
            '                    mStdQty = mStdQty / 1000	
            '                    mStdQty = mStdQty / 1000	
            '                End If	
            '            End If	
            mRMQty = mRMQty + mStdQty
            '            mRate = GetCurrentItemRate(mRMCode, Format(lblRunDate.text, "DD/MM/YYYY"))	

            mTotClosing = CDbl(VB6.Format(pSaleQty * mStdQty, "0.00"))
            If optRate(0).Checked = True Then
                mTotValue = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mTotClosing, VB6.Format(lblRunDate.Text, "DD/MM/YYYY"), "L")
            Else
                mTotValue = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mTotClosing, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L")
            End If
            If mTotClosing = 0 Then
                mRate = 0
            Else
                mRate = mTotValue / mTotClosing
            End If

            mRMAmount = mRMAmount + (mRate * mStdQty)
            mStdQty = 1
        End If

        '    Call FillSubAlterRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mDeptCode, pParentCode)	

        '    Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode)	

        Exit Sub
FillGERR:
        '    Resume	
        MsgBox(Err.Description)
    End Sub
    Private Function CheckSubRecord(ByRef pProductCode As String) As Boolean


        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mMainItemCode As String
        'Dim mSrn As String	
        'Dim xSrn As String	
        'Dim j As Long	
        '	
        CheckSubRecord = False
        '    mMainItemCode = GetMainItemCode(mProductCode)	

        SqlStr = " SELECT " & vbCrLf _
            & " IH.PRODUCT_CODE, ID.RM_CODE,ID.DEPT_CODE " & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O' AND IH.IS_BOP='N'" & vbCrLf _
            & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "       '& vbCrLf _
        '& " AND WEF<= '" & vb6.Format(pWEF, "DD-MMM-YYYY") & "')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            '        Do While Not RsShow.EOF	
            '           mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))	
            CheckSubRecord = True
            '        Loop	
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.ITEM_CODE AS RM_CODE,'J/W' AS DEPT_CODE " & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O' AND IS_INHOUSE='N' AND IH.IS_BOP='N'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

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



    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByRef pMainProductCode As String, ByRef pSaleQty As Double, ByRef mStdQty As Double)


        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String
        Dim j As Integer


        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) AS STD_QTY, ID.GROSS_WT_SCRAP * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

        SqlStr = SqlStr & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O'" & vbCrLf _
            & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "       '& vbCrLf _
        '& " AND WEF<= '" & vb6.Format(pWEF, "DD-MMM-YYYY") & "')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                If optCalcOn(0).Checked = True Then
                    mStdQty = mStdQty * (Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                Else
                    mStdQty = mStdQty * ((Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)) + Val(IIf(IsDBNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value))))
                End If
                Call FillGridCol(RsShow, pMainProductCode, pProductCode, pSaleQty, mStdQty)
                RsShow.MoveNext()
            Loop
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ITEM_QTY * (CASE WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 ELSE 1 END) AS STD_QTY, ID.SCRAP_QTY * (CASE WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 ELSE 1 END) AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O' AND IS_INHOUSE='N'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                    If optCalcOn(0).Checked = True Then
                        mStdQty = mStdQty * (Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                    Else
                        mStdQty = mStdQty * ((Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)) + Val(IIf(IsDBNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value))))
                    End If
                    Call FillGridCol(RsShow, pMainProductCode, pProductCode, pSaleQty, mStdQty)
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
        Dim SqlStr As String = ""

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






    Public Sub frmParamBudgetCont_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Budget Contribution Report"
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamBudgetCont_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(7440)
        Me.Width = VB6.TwipsToPixelsX(11625)

        cboType.Items.Clear()
        cboType.Items.Add("All")
        cboType.Items.Add("Sale")
        cboType.Items.Add("Jobwork")
        cboType.SelectedIndex = 0

        '    txtDateFrom.Text = Format(RsCompany!START_DATE, "DD/MM/YYYY")	
        '    txtDateTo.Text = Format(RunDate, "DD/MM/YYYY")	

        lblRunDate.Text = CStr(RunDate)
        SetDate(CDate(lblRunDate.Text))

        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)

        chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        chkFG_CheckStateChanged(chkFG, New System.EventArgs())
        chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked

        txtCustomerName.Enabled = False
        cmdsearchCustName.Enabled = False


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
            .MaxCols = ColContributionPer
            .set_RowHeight(-1, RowHeight)

            .Row = -1
            .set_ColWidth(0, 4)

            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerCode, 6)
            .ColHidden = False

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
            .set_ColWidth(ColMainProd, 6)
            .ColHidden = False

            .Col = ColProductDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColProductDesc, 15)
            .ColHidden = False

            For cntCol = ColProductQty To ColContributionPer
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 8.5)
            Next

            .ColsFrozen = ColProductDesc
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle	

        End With

    End Sub

    Private Sub frmParamBudgetCont_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        Dim SqlStr As String = ""

        Report1.Reset()

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1


        '*************** Fetching Record For Report ***************************	
        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mTitle = "Budget Contribution Report"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\BudgetContribution.rpt"

        mSubTitle = "For the Month : " & VB6.Format(lblRunDate.Text, "MMMM, YYYY")

        '    If chkFG.Value = vbUnchecked And Trim(txtFGName.Text) <> "" Then	
        '        mSubTitle = mSubTitle & " [Product Name : " & txtFGName.Text & "]"	
        '    End If	

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
End Class
