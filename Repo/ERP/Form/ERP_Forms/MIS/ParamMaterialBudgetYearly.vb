Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMaterialBudgetYearly
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
    Private Const ColAprQty As Short = 13
    Private Const ColAprRate As Short = 14
    Private Const ColAprAmount As Short = 15
    Private Const ColMayQty As Short = 16
    Private Const ColMayRate As Short = 17
    Private Const ColMayAmount As Short = 18
    Private Const ColJunQty As Short = 19
    Private Const ColJunRate As Short = 20
    Private Const ColJunAmount As Short = 21
    Private Const ColJulQty As Short = 22
    Private Const ColJulRate As Short = 23
    Private Const ColJulAmount As Short = 24
    Private Const ColAugQty As Short = 25
    Private Const ColAugRate As Short = 26
    Private Const ColAugAmount As Short = 27
    Private Const ColSepQty As Short = 28
    Private Const ColSepRate As Short = 29
    Private Const ColSepAmount As Short = 30
    Private Const ColOctQty As Short = 31
    Private Const ColOctRate As Short = 32
    Private Const ColOctAmount As Short = 33
    Private Const ColNovQty As Short = 34
    Private Const ColNovRate As Short = 35
    Private Const ColNovAmount As Short = 36
    Private Const ColDecQty As Short = 37
    Private Const ColDecRate As Short = 38
    Private Const ColDecAmount As Short = 39
    Private Const ColJanQty As Short = 40
    Private Const ColJanRate As Short = 41
    Private Const ColJanAmount As Short = 42
    Private Const ColFebQty As Short = 43
    Private Const ColFebRate As Short = 44
    Private Const ColFebAmount As Short = 45
    Private Const ColMarQty As Short = 46
    Private Const ColMarRate As Short = 47
    Private Const ColMarAmount As Short = 48
    Private Const ColTotalQty As Short = 49
    Private Const ColTotalRate As Short = 50
    Private Const ColTotalAmount As Short = 51
    Private Const ColLevel As Short = 52
    Private Const ColFlag As Short = 53


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
        '    SprdMain.SetFocus	
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

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If optBaseOn(0).Checked = True Then
            SqlStr = " SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.ITEM_CODE,INVMST.ITEM_SHORT_DESC,IH.ITEM_UOM, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'APRIL' THEN ID.QTY ELSE 0 END) AS APRIL_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'APRIL' THEN ID.RATE ELSE 0 END) AS APRIL_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'APRIL' THEN ID.VALUE ELSE 0 END) AS APRIL_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'MAY' THEN ID.QTY ELSE 0 END) AS MAY_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'MAY' THEN ID.RATE ELSE 0 END) AS MAY_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'MAY' THEN ID.VALUE ELSE 0 END) AS MAY_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JUNE' THEN ID.QTY ELSE 0 END) AS JUNE_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JUNE' THEN ID.RATE ELSE 0 END) AS JUNE_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JUNE' THEN ID.VALUE ELSE 0 END) AS JUNE_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JULY' THEN ID.QTY ELSE 0 END) AS JULY_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JULY' THEN ID.RATE ELSE 0 END) AS JULY_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JULY' THEN ID.VALUE ELSE 0 END) AS JULY_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'AUGUST' THEN ID.QTY ELSE 0 END) AS AUGUST_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'AUGUST' THEN ID.RATE ELSE 0 END) AS AUGUST_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'AUGUST' THEN ID.VALUE ELSE 0 END) AS AUGUST_VALUE, "

            SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'SEPTEMBER' THEN ID.QTY ELSE 0 END) AS SEPTEMBER_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'SEPTEMBER' THEN ID.RATE ELSE 0 END) AS SEPTEMBER_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'SEPTEMBER' THEN ID.VALUE ELSE 0 END) AS SEPTEMBER_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'OCTOBER' THEN ID.QTY ELSE 0 END) AS OCTOBER_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'OCTOBER' THEN ID.RATE ELSE 0 END) AS OCTOBER_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'OCTOBER' THEN ID.VALUE ELSE 0 END) AS OCTOBER_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'NOVEMBER' THEN ID.QTY ELSE 0 END) AS NOVEMBER_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'NOVEMBER' THEN ID.RATE ELSE 0 END) AS NOVEMBER_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'NOVEMBER' THEN ID.VALUE ELSE 0 END) AS NOVEMBER_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'DECEMBER' THEN ID.QTY ELSE 0 END) AS DECEMBER_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'DECEMBER' THEN ID.RATE ELSE 0 END) AS DECEMBER_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'DECEMBER' THEN ID.VALUE ELSE 0 END) AS DECEMBER_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JANUARY' THEN ID.QTY ELSE 0 END) AS JANUARY_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JANUARY' THEN ID.RATE ELSE 0 END) AS JANUARY_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JANUARY' THEN ID.VALUE ELSE 0 END) AS JANUARY_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'FEBRUARY' THEN ID.QTY ELSE 0 END) AS FEBRUARY_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'FEBRUARY' THEN ID.RATE ELSE 0 END) AS FEBRUARY_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'FEBRUARY' THEN ID.VALUE ELSE 0 END) AS FEBRUARY_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'MARCH' THEN ID.QTY ELSE 0 END) AS MARCH_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'MARCH' THEN ID.RATE ELSE 0 END) AS MARCH_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'MARCH' THEN ID.VALUE ELSE 0 END) AS MARCH_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.QTY IS NULL THEN 0 ELSE ID.QTY END) AS TOTAL_QTY, " & vbCrLf & " AVG(CASE WHEN ID.RATE IS NULL THEN 0 ELSE ID.RATE END) AS TOTAL_RATE, " & vbCrLf & " SUM(CASE WHEN ID.VALUE IS NULL THEN 0 ELSE ID.VALUE END) AS TOTAL_VALUE "
        Else
            SqlStr = " SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '04' THEN ID.ITEM_QTY ELSE 0 END) AS APRIL_QTY, " & vbCrLf & " MAX(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '04' THEN ID.ITEM_RATE ELSE 0 END) AS APRIL_RATE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '04' THEN ID.ITEM_AMT ELSE 0 END) AS APRIL_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '05' THEN ID.ITEM_QTY ELSE 0 END) AS MAY_QTY, " & vbCrLf & " MAX(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '05' THEN ID.ITEM_RATE ELSE 0 END) AS MAY_RATE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '05' THEN ID.ITEM_AMT ELSE 0 END) AS MAY_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '06' THEN ID.ITEM_QTY ELSE 0 END) AS JUNE_QTY, " & vbCrLf & " MAX(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '06' THEN ID.ITEM_RATE ELSE 0 END) AS JUNE_RATE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '06' THEN ID.ITEM_AMT ELSE 0 END) AS JUNE_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '07' THEN ID.ITEM_QTY ELSE 0 END) AS JULY_QTY, " & vbCrLf & " MAX(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '07' THEN ID.ITEM_RATE ELSE 0 END) AS JULY_RATE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '07' THEN ID.ITEM_AMT ELSE 0 END) AS JULY_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '08' THEN ID.ITEM_QTY ELSE 0 END) AS AUGUST_QTY, " & vbCrLf & " MAX(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '08' THEN ID.ITEM_RATE ELSE 0 END) AS AUGUST_RATE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '08' THEN ID.ITEM_AMT ELSE 0 END) AS AUGUST_VALUE, "

            SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '09' THEN ID.ITEM_QTY ELSE 0 END) AS SEPTEMBER_QTY, " & vbCrLf & " MAX(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '09' THEN ID.ITEM_RATE ELSE 0 END) AS SEPTEMBER_RATE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '09' THEN ID.ITEM_AMT ELSE 0 END) AS SEPTEMBER_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '10' THEN ID.ITEM_QTY ELSE 0 END) AS OCTOBER_QTY, " & vbCrLf & " MAX(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '10' THEN ID.ITEM_RATE ELSE 0 END) AS OCTOBER_RATE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '10' THEN ID.ITEM_AMT ELSE 0 END) AS OCTOBER_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '11' THEN ID.ITEM_QTY ELSE 0 END) AS NOVEMBER_QTY, " & vbCrLf & " MAX(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '11' THEN ID.ITEM_RATE ELSE 0 END) AS NOVEMBER_RATE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '11' THEN ID.ITEM_AMT ELSE 0 END) AS NOVEMBER_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '12' THEN ID.ITEM_QTY ELSE 0 END) AS DECEMBER_QTY, " & vbCrLf & " MAX(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '12' THEN ID.ITEM_RATE ELSE 0 END) AS DECEMBER_RATE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '12' THEN ID.ITEM_AMT ELSE 0 END) AS DECEMBER_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '01' THEN ID.ITEM_QTY ELSE 0 END) AS JANUARY_QTY, " & vbCrLf & " MAX(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '01' THEN ID.ITEM_RATE ELSE 0 END) AS JANUARY_RATE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '01' THEN ID.ITEM_AMT ELSE 0 END) AS JANUARY_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '02' THEN ID.ITEM_QTY ELSE 0 END) AS FEBRUARY_QTY, " & vbCrLf & " MAX(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '02' THEN ID.ITEM_RATE ELSE 0 END) AS FEBRUARY_RATE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '02' THEN ID.ITEM_AMT ELSE 0 END) AS FEBRUARY_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '03' THEN ID.ITEM_QTY ELSE 0 END) AS MARCH_QTY, " & vbCrLf & " MAX(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '03' THEN ID.ITEM_RATE ELSE 0 END) AS MARCH_RATE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MM') = '03' THEN ID.ITEM_AMT ELSE 0 END) AS MARCH_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.ITEM_QTY IS NULL THEN 0 ELSE ID.ITEM_QTY END) AS TOTAL_QTY, " & vbCrLf & " CASe WHEN SUM(CASE WHEN ID.ITEM_QTY IS NULL THEN 0 ELSE ID.ITEM_QTY END)=0 THEN 0 ELSE SUM(CASE WHEN ID.ITEM_AMT IS NULL THEN 0 ELSE ID.ITEM_AMT END)/SUM(CASE WHEN ID.ITEM_QTY IS NULL THEN 0 ELSE ID.ITEM_QTY END) END AS TOTAL_RATE, " & vbCrLf & " SUM(CASE WHEN ID.ITEM_AMT IS NULL THEN 0 ELSE ID.ITEM_AMT END) AS TOTAL_VALUE "

        End If

        If optBaseOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " FROM MIS_SALEBUDGET_DET IH, MIS_SALEBUDGET_TRN ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, INV_GENERAL_MST GMAT " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_NO,LENGTH(IH.AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND IH.AUTO_KEY_NO=ID.AUTO_KEY_NO " & vbCrLf & " AND IH.SERIAL_NO=ID.SERIAL_NO "
        Else
            SqlStr = SqlStr & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, INV_GENERAL_MST GMAT " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND CANCELLED='N'"

            If optBaseOn(3).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.REF_DESP_TYPE IN ('S') "
            ElseIf optBaseOn(4).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.REF_DESP_TYPE IN ('P','G','E','S') "
            Else
                SqlStr = SqlStr & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.REF_DESP_TYPE IN ('P','G','E') "
            End If

            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE IN ( " 'AND IH.REF_DESP_TYPE='P'"	

            SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT PRODUCT_CODE FROM PRD_NEWBOM_HDR WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " UNION " & vbCrLf & " SELECT DISTINCT REF_ITEM_CODE FROM INV_ITEM_RELATIONSHIP_DET WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " )"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE "

        If optBaseOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.ITEM_CODE=INVMST.ITEM_CODE "
        Else
            SqlStr = SqlStr & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE "
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
            If cboType.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & " AND GMAT.STOCKTYPE='FG' "
            ElseIf cboType.SelectedIndex = 2 Then
                SqlStr = SqlStr & vbCrLf & " AND GMAT.STOCKTYPE='CS' "
            End If


            SqlStr = SqlStr & vbCrLf & "HAVING (SUM(ID.QTY) IS NOT NULL OR SUM(ID.QTY)>0)"
        Else
            '        SqlStr = SqlStr & vbCrLf & "HAVING (SUM(ID.QTY) IS NOT NULL OR SUM(ID.QTY)>0)"	
        End If

        If optBaseOn(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,IH.ITEM_CODE,INVMST.ITEM_SHORT_DESC,IH.ITEM_UOM " & vbCrLf & " ORDER BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,IH.ITEM_CODE,INVMST.ITEM_SHORT_DESC,IH.ITEM_UOM "

        Else
            SqlStr = SqlStr & vbCrLf & " GROUP BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM " & vbCrLf & " ORDER BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC,ID.ITEM_UOM "
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
        Dim RsMain As ADODB.Recordset
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mRMCode As String
        Dim mProductCode As String
        Dim mNextProductCode As String
        Dim I As Integer
        Dim mSrn As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pSqlStr As String
        Dim mLevel As Integer
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim pWEF As String

        Dim mCheckProdCode As String
        Dim mCheckRMCode As String

        Dim mAprQty As Double
        Dim mAprRate As Double
        Dim mAprAmount As Double
        Dim mMayQty As Double
        Dim mMayRate As Double
        Dim mMayAmount As Double
        Dim mJunQty As Double
        Dim mJunRate As Double
        Dim mJunAmount As Double
        Dim mJulQty As Double
        Dim mJulRate As Double
        Dim mJulAmount As Double
        Dim mAugQty As Double
        Dim mAugRate As Double
        Dim mAugAmount As Double
        Dim mSepQty As Double
        Dim mSepRate As Double
        Dim mSepAmount As Double
        Dim mOctQty As Double
        Dim mOctRate As Double
        Dim mOctAmount As Double
        Dim mNovQty As Double
        Dim mNovRate As Double
        Dim mNovAmount As Double
        Dim mDecQty As Double
        Dim mDecRate As Double
        Dim mDecAmount As Double
        Dim mJanQty As Double
        Dim mJanRate As Double
        Dim mJanAmount As Double
        Dim mFebQty As Double
        Dim mFebRate As Double
        Dim mFebAmount As Double
        Dim mMarQty As Double
        Dim mMarRate As Double
        Dim mMarAmount As Double
        Dim mTotalQty As Double
        Dim mTotalRate As Double
        Dim mTotalAmount As Double
        Dim mCustomerCode As String
        Dim mCustomerName As String
        Dim mMainItemCode As String
        Dim mStdQty As Double
        Dim mOutPutQty As Double
        Dim mSRQty As Double
        Dim mSRAmount As Double
        Dim mSRRate As Double

        Dim mTotSRQty As Double
        Dim mTotSRAmount As Double

        Dim mIssueUOM As String
        Dim mScrapQty As Double

        If mRsBudget.EOF = False Then
            mProductCode = Trim(IIf(IsDbNull(mRsBudget.Fields("ITEM_CODE").Value), "", mRsBudget.Fields("ITEM_CODE").Value))
            mMainItemCode = GetMainItemCode(mProductCode)

            mCustomerCode = Trim(IIf(IsDbNull(mRsBudget.Fields("SUPP_CUST_CODE").Value), "", mRsBudget.Fields("SUPP_CUST_CODE").Value))
            mCustomerName = Trim(IIf(IsDbNull(mRsBudget.Fields("SUPP_CUST_NAME").Value), "", mRsBudget.Fields("SUPP_CUST_NAME").Value))

            mTotSRQty = 0
            mTotSRAmount = 0

            If GetSaleReturn(mCustomerCode, 4, mProductCode, mSRQty, mSRAmount, mSRRate) = False Then GoTo LedgError
            mTotSRQty = mTotSRQty + mSRQty
            mTotSRAmount = mTotSRAmount + mSRAmount
            mAprQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("APRIL_QTY").Value), 0, mRsBudget.Fields("APRIL_QTY").Value), "0.00")) - mSRQty
            '        mAprRate = Format(IIf(IsNull(mRsBudget!APRIL_RATE), 0, mRsBudget!APRIL_RATE), "0.00")	
            mAprAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("APRIL_VALUE").Value), 0, mRsBudget.Fields("APRIL_VALUE").Value), "0.00")) - mSRAmount

            If mAprQty = 0 Then
                mAprRate = 0
            Else
                mAprRate = CDbl(VB6.Format(mAprAmount / mAprQty, "0.00"))
            End If

            If GetSaleReturn(mCustomerCode, 5, mProductCode, mSRQty, mSRAmount, mSRRate) = False Then GoTo LedgError
            mTotSRQty = mTotSRQty + mSRQty
            mTotSRAmount = mTotSRAmount + mSRAmount
            mMayQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("MAY_QTY").Value), 0, mRsBudget.Fields("MAY_QTY").Value), "0.00")) - mSRQty
            '        mMayRate = Format(IIf(IsNull(mRsBudget!MAY_RATE), 0, mRsBudget!MAY_RATE), "0.00")	
            mMayAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("MAY_VALUE").Value), 0, mRsBudget.Fields("MAY_VALUE").Value), "0.00")) - mSRAmount

            If mMayQty = 0 Then
                mMayRate = 0
            Else
                mMayRate = CDbl(VB6.Format(mMayAmount / mMayQty, "0.00"))
            End If

            If GetSaleReturn(mCustomerCode, 6, mProductCode, mSRQty, mSRAmount, mSRRate) = False Then GoTo LedgError
            mTotSRQty = mTotSRQty + mSRQty
            mTotSRAmount = mTotSRAmount + mSRAmount
            mJunQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("JUNE_QTY").Value), 0, mRsBudget.Fields("JUNE_QTY").Value), "0.00")) - mSRQty
            '        mJunRate = Format(IIf(IsNull(mRsBudget!JUNE_RATE), 0, mRsBudget!JUNE_RATE), "0.00")	
            mJunAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("JUNE_VALUE").Value), 0, mRsBudget.Fields("JUNE_VALUE").Value), "0.00")) - mSRAmount

            If mJunQty = 0 Then
                mJunRate = 0
            Else
                mJunRate = CDbl(VB6.Format(mJunAmount / mJunQty, "0.00"))
            End If

            If GetSaleReturn(mCustomerCode, 7, mProductCode, mSRQty, mSRAmount, mSRRate) = False Then GoTo LedgError
            mTotSRQty = mTotSRQty + mSRQty
            mTotSRAmount = mTotSRAmount + mSRAmount
            mJulQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("JULY_QTY").Value), 0, mRsBudget.Fields("JULY_QTY").Value), "0.00")) - mSRQty
            '        mJulRate = Format(IIf(IsNull(mRsBudget!JULY_RATE), 0, mRsBudget!JULY_RATE), "0.00")	
            mJulAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("JULY_VALUE").Value), 0, mRsBudget.Fields("JULY_VALUE").Value), "0.00")) - mSRAmount

            If mJulQty = 0 Then
                mJulRate = 0
            Else
                mJulRate = CDbl(VB6.Format(mJulAmount / mJulQty, "0.00"))
            End If

            If GetSaleReturn(mCustomerCode, 8, mProductCode, mSRQty, mSRAmount, mSRRate) = False Then GoTo LedgError
            mTotSRQty = mTotSRQty + mSRQty
            mTotSRAmount = mTotSRAmount + mSRAmount
            mAugQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("AUGUST_QTY").Value), 0, mRsBudget.Fields("AUGUST_QTY").Value), "0.00")) - mSRQty
            '        mAugRate = Format(IIf(IsNull(mRsBudget!AUGUST_RATE), 0, mRsBudget!AUGUST_RATE), "0.00")	
            mAugAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("AUGUST_VALUE").Value), 0, mRsBudget.Fields("AUGUST_VALUE").Value), "0.00")) - mSRAmount

            If mAugQty = 0 Then
                mAugRate = 0
            Else
                mAugRate = CDbl(VB6.Format(mAugAmount / mAugQty, "0.00"))
            End If

            If GetSaleReturn(mCustomerCode, 9, mProductCode, mSRQty, mSRAmount, mSRRate) = False Then GoTo LedgError
            mTotSRQty = mTotSRQty + mSRQty
            mTotSRAmount = mTotSRAmount + mSRAmount
            mSepQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("SEPTEMBER_QTY").Value), 0, mRsBudget.Fields("SEPTEMBER_QTY").Value), "0.00")) - mSRQty
            '        mSepRate = Format(IIf(IsNull(mRsBudget!SEPTEMBER_RATE), 0, mRsBudget!SEPTEMBER_RATE), "0.00")	
            mSepAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("SEPTEMBER_VALUE").Value), 0, mRsBudget.Fields("SEPTEMBER_VALUE").Value), "0.00")) - mSRAmount

            If mSepQty = 0 Then
                mSepRate = 0
            Else
                mSepRate = CDbl(VB6.Format(mSepAmount / mSepQty, "0.00"))
            End If

            If GetSaleReturn(mCustomerCode, 10, mProductCode, mSRQty, mSRAmount, mSRRate) = False Then GoTo LedgError
            mTotSRQty = mTotSRQty + mSRQty
            mTotSRAmount = mTotSRAmount + mSRAmount
            mOctQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("OCTOBER_QTY").Value), 0, mRsBudget.Fields("OCTOBER_QTY").Value), "0.00")) - mSRQty
            '        mOctRate = Format(IIf(IsNull(mRsBudget!OCTOBER_RATE), 0, mRsBudget!OCTOBER_RATE), "0.00")	
            mOctAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("OCTOBER_VALUE").Value), 0, mRsBudget.Fields("OCTOBER_VALUE").Value), "0.00")) - mSRAmount

            If mOctQty = 0 Then
                mOctRate = 0
            Else
                mOctRate = CDbl(VB6.Format(mOctAmount / mOctQty, "0.00"))
            End If

            If GetSaleReturn(mCustomerCode, 11, mProductCode, mSRQty, mSRAmount, mSRRate) = False Then GoTo LedgError
            mTotSRQty = mTotSRQty + mSRQty
            mTotSRAmount = mTotSRAmount + mSRAmount
            mNovQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("NOVEMBER_QTY").Value), 0, mRsBudget.Fields("NOVEMBER_QTY").Value), "0.00")) - mSRQty
            '        mNovRate = Format(IIf(IsNull(mRsBudget!NOVEMBER_RATE), 0, mRsBudget!NOVEMBER_RATE), "0.00")	
            mNovAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("NOVEMBER_VALUE").Value), 0, mRsBudget.Fields("NOVEMBER_VALUE").Value), "0.00")) - mSRAmount

            If mNovQty = 0 Then
                mNovRate = 0
            Else
                mNovRate = CDbl(VB6.Format(mNovAmount / mNovQty, "0.00"))
            End If

            If GetSaleReturn(mCustomerCode, 12, mProductCode, mSRQty, mSRAmount, mSRRate) = False Then GoTo LedgError
            mTotSRQty = mTotSRQty + mSRQty
            mTotSRAmount = mTotSRAmount + mSRAmount
            mDecQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("DECEMBER_QTY").Value), 0, mRsBudget.Fields("DECEMBER_QTY").Value), "0.00")) - mSRQty
            '        mDecRate = Format(IIf(IsNull(mRsBudget!DECEMBER_RATE), 0, mRsBudget!DECEMBER_RATE), "0.00")	
            mDecAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("DECEMBER_VALUE").Value), 0, mRsBudget.Fields("DECEMBER_VALUE").Value), "0.00")) - mSRAmount

            If mDecQty = 0 Then
                mDecRate = 0
            Else
                mDecRate = CDbl(VB6.Format(mDecAmount / mDecQty, "0.00"))
            End If

            If GetSaleReturn(mCustomerCode, 1, mProductCode, mSRQty, mSRAmount, mSRRate) = False Then GoTo LedgError
            mTotSRQty = mTotSRQty + mSRQty
            mTotSRAmount = mTotSRAmount + mSRAmount
            mJanQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("JANUARY_QTY").Value), 0, mRsBudget.Fields("JANUARY_QTY").Value), "0.00")) - mSRQty
            '        mJanRate = Format(IIf(IsNull(mRsBudget!JANUARY_RATE), 0, mRsBudget!JANUARY_RATE), "0.00")	
            mJanAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("JANUARY_VALUE").Value), 0, mRsBudget.Fields("JANUARY_VALUE").Value), "0.00")) - mSRAmount

            If mJanQty = 0 Then
                mJanRate = 0
            Else
                mJanRate = CDbl(VB6.Format(mJanAmount / mJanQty, "0.00"))
            End If

            If GetSaleReturn(mCustomerCode, 2, mProductCode, mSRQty, mSRAmount, mSRRate) = False Then GoTo LedgError
            mTotSRQty = mTotSRQty + mSRQty
            mTotSRAmount = mTotSRAmount + mSRAmount
            mFebQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("FEBRUARY_QTY").Value), 0, mRsBudget.Fields("FEBRUARY_QTY").Value), "0.00")) - mSRQty
            '        mFebRate = Format(IIf(IsNull(mRsBudget!FEBRUARY_RATE), 0, mRsBudget!FEBRUARY_RATE), "0.00")	
            mFebAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("FEBRUARY_VALUE").Value), 0, mRsBudget.Fields("FEBRUARY_VALUE").Value), "0.00")) - mSRAmount

            If mFebQty = 0 Then
                mFebRate = 0
            Else
                mFebRate = CDbl(VB6.Format(mFebAmount / mFebQty, "0.00"))
            End If

            If GetSaleReturn(mCustomerCode, 3, mProductCode, mSRQty, mSRAmount, mSRRate) = False Then GoTo LedgError
            mTotSRQty = mTotSRQty + mSRQty
            mTotSRAmount = mTotSRAmount + mSRAmount
            mMarQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("MARCH_QTY").Value), 0, mRsBudget.Fields("MARCH_QTY").Value), "0.00")) - mSRQty
            '        mMarRate = Format(IIf(IsNull(mRsBudget!MARCH_RATE), 0, mRsBudget!MARCH_RATE), "0.00")	
            mMarAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("MARCH_VALUE").Value), 0, mRsBudget.Fields("MARCH_VALUE").Value), "0.00")) - mSRAmount

            If mMarQty = 0 Then
                mMarRate = 0
            Else
                mMarRate = CDbl(VB6.Format(mMarAmount / mMarQty, "0.00"))
            End If

            mTotalQty = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("TOTAL_QTY").Value), 0, mRsBudget.Fields("TOTAL_QTY").Value), "0.00")) - mTotSRQty
            '        mTotalRate = Format(IIf(IsNull(mRsBudget!TOTAL_RATE), 0, mRsBudget!TOTAL_RATE), "0.00")	
            mTotalAmount = CDbl(VB6.Format(IIf(IsDbNull(mRsBudget.Fields("TOTAL_VALUE").Value), 0, mRsBudget.Fields("TOTAL_VALUE").Value), "0.00")) - mTotSRAmount

            If mTotalQty = 0 Then
                mTotalRate = 0
            Else
                mTotalRate = CDbl(VB6.Format(mTotalAmount / mTotalQty, "0.00"))
            End If

            SqlStr = " SELECT IH.PRODUCT_CODE, WEF " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "'"
            SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'" '' AND BOM_TYPE='P' AND IS_EXPORT_ITEM='N'"	
            SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PRODUCT_CODE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockReadOnly)
            '        mcntRow = 1	

            If RsMain.EOF = False Then
                Do While Not RsMain.EOF
                    pWEF = Trim(IIf(IsDbNull(RsMain.Fields("WEF").Value), "", RsMain.Fields("WEF").Value))

                    SqlStr = ""
                    SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, OUTPUT_QTY," & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) AS STD_QTY, ID.GROSS_WT_SCRAP * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) AS GROSS_WT_SCRAP, " & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MINIMUM_QTY, MAXIMUM_QTY "

                    SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                    SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "' AND IH.STATUS='O'" & vbCrLf _
                        & " AND IH.WEF=TO_DATE('" & VB6.Format(pWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


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
                                mStdQty = (Val(IIf(IsDbNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                                mScrapQty = 0
                            Else
                                mStdQty = Val(IIf(IsDbNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value))
                                mScrapQty = Val(IIf(IsDbNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value))
                            End If


                            If PubUserID = "A00001" Then
                                mIssueUOM = IIf(IsDbNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                                If mIssueUOM = "KGS" Or mIssueUOM = "TON" Then
                                    mStdQty = mStdQty - (mStdQty * 4 * 0.01)
                                    If RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 1 Then
                                        mScrapQty = mScrapQty - (mScrapQty * 15 * 0.01)
                                    ElseIf RsCompany.Fields("COMPANY_CODE").Value = 28 Then
                                        mScrapQty = mScrapQty - (mScrapQty * 30 * 0.01)
                                    ElseIf RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                                        mScrapQty = mScrapQty - (mScrapQty * 15 * 0.01)
                                    ElseIf RsCompany.Fields("COMPANY_CODE").Value = 25 Or RsCompany.Fields("COMPANY_CODE").Value = 32 Then
                                        mScrapQty = mScrapQty - (mScrapQty * 20 * 0.01)
                                    End If
                                End If
                            End If

                            mStdQty = mStdQty + mScrapQty

                            mOutPutQty = Val(IIf(IsDbNull(RsShow.Fields("OUTPUT_QTY").Value), 1, RsShow.Fields("OUTPUT_QTY").Value))

                            mAprQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mAprQty, mAprQty / mOutPutQty)
                            mMayQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mMayQty, mMayQty / mOutPutQty)
                            mJunQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mJunQty, mJunQty / mOutPutQty)
                            mJulQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mJulQty, mJulQty / mOutPutQty)
                            mAugQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mAugQty, mAugQty / mOutPutQty)
                            mSepQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mSepQty, mSepQty / mOutPutQty)
                            mOctQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mOctQty, mOctQty / mOutPutQty)
                            mNovQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mNovQty, mNovQty / mOutPutQty)
                            mDecQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mDecQty, mDecQty / mOutPutQty)
                            mJanQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mJanQty, mJanQty / mOutPutQty)
                            mFebQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mFebQty, mFebQty / mOutPutQty)
                            mMarQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mMarQty, mMarQty / mOutPutQty)
                            mTotalQty = IIf(mOutPutQty = 0 Or mOutPutQty = 1, mTotalQty, mTotalQty / mOutPutQty)

                            Call FillGridCol(RsShow, mSrn, mLevel, mProductCode, mProductCode, mAprQty, mAprRate, mAprAmount, mMayQty, mMayRate, mMayAmount, mJunQty, mJunRate, mJunAmount, mJulQty, mJulRate, mJulAmount, mAugQty, mAugRate, mAugAmount, mSepQty, mSepRate, mSepAmount, mOctQty, mOctRate, mOctAmount, mNovQty, mNovRate, mNovAmount, mDecQty, mDecRate, mDecAmount, mJanQty, mJanRate, mJanAmount, mFebQty, mFebRate, mFebAmount, mMarQty, mMarRate, mMarAmount, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mStdQty)

                            '                        mcntRow = mcntRow + 1	
                            '                        SprdMain.MaxRows = SprdMain.MaxRows + 1	
                            RsShow.MoveNext()
                        Loop
                    End If
                    RsMain.MoveNext()
                Loop
            End If
        End If

        RsShow = Nothing
        Exit Sub
LedgError:
        ''    Resume	
        MsgInformation(Err.Description)
    End Sub
    Private Function GetSaleReturn(ByRef mCustomerCode As String, ByRef mMonth As Short, ByRef mProductCode As String, ByRef mSRQty As Double, ByRef mSRAmount As Double, ByRef mSRRate As Double) As Boolean

        On Error GoTo LedgError
        Dim RsMain As ADODB.Recordset
        Dim mDateFrom As String
        Dim mDateTo As String
        Dim mYear As Short
        Dim SqlStr As String = ""

        GetSaleReturn = False
        mSRQty = 0
        mSRAmount = 0
        mSRRate = 0

        If optBaseOn(2).Checked = True Or optBaseOn(4).Checked = True Then
        Else
            GetSaleReturn = True
            Exit Function
        End If

        If mMonth = 1 Or mMonth = 2 Or mMonth = 3 Then
            mYear = RsCompany.Fields("FYEAR").Value + 1
        Else
            mYear = RsCompany.Fields("FYEAR").Value
        End If

        mDateFrom = "01/" & VB6.Format(mMonth, "00") & "/" & mYear
        mDateFrom = VB6.Format(mDateFrom, "DD/MM/YYYY")
        mDateTo = MainClass.LastDay(mMonth, mYear) & "/" & VB6.Format(mMonth, "00") & "/" & mYear
        mDateTo = VB6.Format(mDateTo, "DD/MM/YYYY")


        SqlStr = " SELECT SUM(ID.ITEM_QTY) AS ITEM_QTY, SUM(ID.ITEM_AMT) as ITEM_AMT " & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, INV_GATE_HDR GH" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=GH.COMPANY_CODE" & vbCrLf & " AND IH.AUTO_KEY_MRR=GH.AUTO_KEY_MRR" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND GH.REF_TYPE='I'"

        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"
        SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustomerCode) & "'"
        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(mDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMain.EOF = False Then
            mSRQty = CDbl(Trim(IIf(IsDbNull(RsMain.Fields("ITEM_QTY").Value), 0, RsMain.Fields("ITEM_QTY").Value)))
            mSRAmount = CDbl(Trim(IIf(IsDbNull(RsMain.Fields("ITEM_AMT").Value), 0, RsMain.Fields("ITEM_AMT").Value)))
            If mSRQty = 0 Then
                mSRRate = 0
            Else
                mSRRate = CDbl(VB6.Format(mSRAmount / mSRQty, "0.00"))
            End If

        End If
        GetSaleReturn = True
        Exit Function
LedgError:
        ''    Resume	
        MsgInformation(Err.Description)
    End Function
    Private Function GetPurchaseQty(ByRef mMonth As Short, ByRef mProductCode As String) As Double

        On Error GoTo LedgError
        Dim RsMain As ADODB.Recordset
        Dim mDateFrom As String
        Dim mDateTo As String
        Dim mYear As Short
        Dim SqlStr As String = ""


        GetPurchaseQty = 1

        If mMonth = 1 Or mMonth = 2 Or mMonth = 3 Then
            mYear = RsCompany.Fields("FYEAR").Value + 1
        Else
            mYear = RsCompany.Fields("FYEAR").Value
        End If

        mDateFrom = "01/" & VB6.Format(mMonth, "00") & "/" & mYear
        mDateFrom = VB6.Format(mDateFrom, "DD/MM/YYYY")
        mDateTo = MainClass.LastDay(mMonth, mYear) & "/" & VB6.Format(mMonth, "00") & "/" & mYear
        mDateTo = VB6.Format(mDateTo, "DD/MM/YYYY")



        SqlStr = " SELECT SUM(ID.ITEM_QTY) AS ITEM_QTY " & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, INV_GATE_HDR GH" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=GH.COMPANY_CODE" & vbCrLf & " AND IH.AUTO_KEY_MRR=GH.AUTO_KEY_MRR" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND GH.REF_TYPE='P'"

        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"
        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(mDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMain.EOF = False Then
            GetPurchaseQty = CDbl(Trim(IIf(IsDbNull(RsMain.Fields("ITEM_QTY").Value), 0, RsMain.Fields("ITEM_QTY").Value)))
        End If
        GetPurchaseQty = IIf(GetPurchaseQty <= 0, 1, GetPurchaseQty)
        '    GetPurchaseQty = True	
        Exit Function
LedgError:
        ''    Resume	
        MsgInformation(Err.Description)
    End Function
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
        SprdMain.Redraw = False
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
        SprdMain.Redraw = True

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
    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pSRNo As String, ByRef pLevel As Integer, ByRef pProductCode As String, ByRef pParentCode As String, ByRef mAprQty As Double, ByRef mAprRate As Double, ByRef mAprAmount As Double, ByRef mMayQty As Double, ByRef mMayRate As Double, ByRef mMayAmount As Double, ByRef mJunQty As Double, ByRef mJunRate As Double, ByRef mJunAmount As Double, ByRef mJulQty As Double, ByRef mJulRate As Double, ByRef mJulAmount As Double, ByRef mAugQty As Double, ByRef mAugRate As Double, ByRef mAugAmount As Double, ByRef mSepQty As Double, ByRef mSepRate As Double, ByRef mSepAmount As Double, ByRef mOctQty As Double, ByRef mOctRate As Double, ByRef mOctAmount As Double, ByRef mNovQty As Double, ByRef mNovRate As Double, ByRef mNovAmount As Double, ByRef mDecQty As Double, ByRef mDecRate As Double, ByRef mDecAmount As Double, ByRef mJanQty As Double, ByRef mJanRate As Double, ByRef mJanAmount As Double, ByRef mFebQty As Double, ByRef mFebRate As Double, ByRef mFebAmount As Double, ByRef mMarQty As Double, ByRef mMarRate As Double, ByRef mMarAmount As Double, ByRef mTotalQty As Double, ByRef mTotalRate As Double, ByRef mTotalAmount As Double, ByRef mCustomerCode As String, ByRef mCustomerName As String, ByRef mStdQty As Double)

        On Error GoTo FillGERR
        Dim mDrgRevNo As String
        Dim mRMCode As String
        Dim mItemUOM As String
        Dim mStockQty As Double
        Dim mDeptCode As String
        Dim mRate As Double
        Dim mFactorQty As Double
        Dim mChildRMCode As String
        'Dim mStdQty As Double	
        Dim mDate As String
        Dim mRMQty As Double
        Dim mRMTotalAmount As Double
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

                    .Col = ColRMDesc
                    .Text = MasterNo
                    .FontBold = IIf(pLevel = 1, True, False)

                    '                .Col = ColSRNo
                    '                .Text = pSRNo
                    '                .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColLevel
                    .Text = Str(pLevel)

                    .Col = ColUnit
                    If MainClass.ValidateWithMasterTable(pProductCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemUOM = MasterNo
                    End If
                    .Text = mItemUOM
                    '                .Text = IIf(IsNull(pRs!ISSUE_UOM), "", pRs!ISSUE_UOM)
                    '                mItemUOM = IIf(IsNull(pRs!ISSUE_UOM), "", pRs!ISSUE_UOM)
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColCustomerCode
                    .Text = mCustomerCode
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColCustomerName
                    .Text = mCustomerName
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColAprQty
                    .Text = Format(mAprQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    mRate = 1
                    .Col = ColAprRate
                    .Text = Format(mAprRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColAprAmount
                    .Text = Format(mAprAmount, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColMayQty
                    .Text = Format(mMayQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColMayRate
                    .Text = Format(mMayRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColMayAmount
                    .Text = Format(mMayAmount, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColJunQty
                    .Text = Format(mJunQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColJunRate
                    .Text = Format(mJunRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColJunAmount
                    .Text = Format(mJunAmount, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColJulQty
                    .Text = Format(mJulQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColJulRate
                    .Text = Format(mJulRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColJulAmount
                    .Text = Format(mJulAmount, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColAugQty
                    .Text = Format(mAugQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColAugRate
                    .Text = Format(mAugRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColAugAmount
                    .Text = Format(mAugAmount, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColSepQty
                    .Text = Format(mSepQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColSepRate
                    .Text = Format(mSepRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColSepAmount
                    .Text = Format(mSepAmount, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColOctQty
                    .Text = Format(mOctQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColOctRate
                    .Text = Format(mOctRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColOctAmount
                    .Text = Format(mOctAmount, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColNovQty
                    .Text = Format(mNovQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColNovRate
                    .Text = Format(mNovRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColNovAmount
                    .Text = Format(mNovAmount, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColDecQty
                    .Text = Format(mDecQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColDecRate
                    .Text = Format(mDecRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColDecAmount
                    .Text = Format(mDecAmount, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColJanQty
                    .Text = Format(mJanQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColJanRate
                    .Text = Format(mJanRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColJanAmount
                    .Text = Format(mJanAmount, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColFebQty
                    .Text = Format(mFebQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColFebRate
                    .Text = Format(mFebRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColFebAmount
                    .Text = Format(mFebAmount, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColMarQty
                    .Text = Format(mMarQty, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColMarRate
                    .Text = Format(mMarRate, "0.00")
                    .FontBold = IIf(pLevel = 1, True, False)

                    .Col = ColMarAmount
                    .Text = Format(mMarAmount, "0.00")
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
            mRMCode = IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
            If CheckSubRecord(mRMCode) = True Then
                pLevel = pLevel + 1
                Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mAprQty, mAprRate, mAprAmount, mMayQty, mMayRate, mMayAmount, mJunQty, mJunRate, mJunAmount, mJulQty, mJulRate, mJulAmount, mAugQty, mAugRate, mAugAmount, mSepQty, mSepRate, mSepAmount, mOctQty, mOctRate, mOctAmount, mNovQty, mNovRate, mNovAmount, mDecQty, mDecRate, mDecAmount, mJanQty, mJanRate, mJanAmount, mFebQty, mFebRate, mFebAmount, mMarQty, mMarRate, mMarAmount, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mStdQty)

            Else
                .Row = .MaxRows
                .Col = ColSRNo
                .Text = pSRNo
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                mDeptCode = IIf(IsDBNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)
                mItemUOM = IIf(IsDBNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)

                .Col = ColRMCode
                .Text = IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
                mRMCode = IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColRMDesc
                .Text = IIf(IsDBNull(pRs.Fields("ITEM_SHORT_DESC").Value), "", pRs.Fields("ITEM_SHORT_DESC").Value)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = colStdQty
                '            If optCalcOn(0).Value = True Then	
                '                mStdQty = Val(IIf(IsNull(pRs!STD_QTY), 0, pRs!STD_QTY))	
                '            Else	
                '                mStdQty = Val(IIf(IsNull(pRs!STD_QTY), 0, pRs!STD_QTY)) + Val(IIf(IsNull(pRs!GROSS_WT_SCRAP), 0, pRs!GROSS_WT_SCRAP))	
                '            End If	

                .Text = CStr(mStdQty)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
                mFactorQty = 1
                '            If mItemUOM = "KGS" Then	
                '                mFactorQty = 1 / 1000	
                '            ElseIf mItemUOM = "TON" Then	
                '                mFactorQty = 1 / 1000	
                '                mFactorQty = mFactorQty / 1000	
                '            End If	

                .Col = ColLevel
                .Text = Str(pLevel)

                .Col = ColUnit
                .Text = IIf(IsDBNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColCustomerCode
                .Text = mCustomerCode
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColCustomerName
                .Text = mCustomerName
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                '            If optRate(0).Value = True Then	
                '                mRate = GetCurrentItemRate(mRMCode, Format(RunDate, "DD/MM/YYYY"))	
                '            Else	
                '                mRate = GetCurrentItemRate(mRMCode, Format(txtRateAsOn.Text, "DD/MM/YYYY"))	
                '            End If	

                .Col = ColAprQty
                .Text = VB6.Format(mAprQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mAprQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                mRMQty = GetPurchaseQty(4, mRMCode) '' IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColAprRate
                mDate = "30/04/" & VB6.Format(RsCompany.Fields("Start_Date").Value, "YYYY")

                mRate = 0
                If mAprQty <> 0 Then
                    If optRate(0).Checked = True Then
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    Else
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                    If mRate = 0 And mRMQty > 0 Then
                        mDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                End If

                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColAprAmount
                .Text = VB6.Format(mRate * mAprQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMTotalAmount = Val(.Text)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColMayQty
                .Text = VB6.Format(mMayQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mMayQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                mRMQty = GetPurchaseQty(5, mRMCode) ' IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColMayRate
                mDate = "31/05/" & VB6.Format(RsCompany.Fields("Start_Date").Value, "YYYY")
                mRate = 0
                If mMayQty <> 0 Then
                    If optRate(0).Checked = True Then
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    Else
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L") / mRMQty
                    End If

                    If mRate = 0 And mRMQty > 0 Then
                        mDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                End If

                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColMayAmount
                .Text = VB6.Format(mRate * mMayQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMTotalAmount = mRMTotalAmount + Val(.Text)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColJunQty
                .Text = VB6.Format(mJunQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mJunQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                mRMQty = GetPurchaseQty(6, mRMCode) ' IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColJunRate
                mDate = "30/06/" & VB6.Format(RsCompany.Fields("Start_Date").Value, "YYYY")
                mRate = 0
                If mJunQty <> 0 Then
                    If optRate(0).Checked = True Then
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    Else
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                    If mRate = 0 And mRMQty > 0 Then
                        mDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                End If

                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColJunAmount
                .Text = VB6.Format(mRate * mJunQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMTotalAmount = mRMTotalAmount + Val(.Text)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColJulQty
                .Text = VB6.Format(mJulQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mJulQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                mRMQty = GetPurchaseQty(7, mRMCode) ' IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColJulRate
                mDate = "31/07/" & VB6.Format(RsCompany.Fields("Start_Date").Value, "YYYY")
                mRate = 0
                If mJulQty <> 0 Then
                    If optRate(0).Checked = True Then
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    Else
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                    If mRate = 0 And mRMQty > 0 Then
                        mDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                End If
                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColJulAmount
                .Text = VB6.Format(mRate * mJulQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMTotalAmount = mRMTotalAmount + Val(.Text)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColAugQty
                .Text = VB6.Format(mAugQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mAugQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                mRMQty = GetPurchaseQty(8, mRMCode) ' IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColAugRate
                mDate = "31/08/" & VB6.Format(RsCompany.Fields("Start_Date").Value, "YYYY")
                mRate = 0
                If mAugQty <> 0 Then
                    If optRate(0).Checked = True Then
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    Else
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                    If mRate = 0 And mRMQty > 0 Then
                        mDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                End If
                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColAugAmount
                .Text = VB6.Format(mRate * mAugQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMTotalAmount = mRMTotalAmount + Val(.Text)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColSepQty
                .Text = VB6.Format(mSepQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mSepQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                mRMQty = GetPurchaseQty(9, mRMCode) 'IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColSepRate
                mDate = "30/09/" & VB6.Format(RsCompany.Fields("Start_Date").Value, "YYYY")
                mRate = 0
                If mSepQty <> 0 Then
                    If optRate(0).Checked = True Then
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    Else
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                    If mRate = 0 And mRMQty > 0 Then
                        mDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                End If
                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColSepAmount
                .Text = VB6.Format(mRate * mSepQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMTotalAmount = mRMTotalAmount + Val(.Text)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColOctQty
                .Text = VB6.Format(mOctQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mOctQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                mRMQty = GetPurchaseQty(10, mRMCode) ' IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColOctRate
                mDate = "31/10/" & VB6.Format(RsCompany.Fields("Start_Date").Value, "YYYY")
                mRate = 0
                If mOctQty <> 0 Then
                    If optRate(0).Checked = True Then
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    Else
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                    If mRate = 0 And mRMQty > 0 Then
                        mDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                End If
                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColOctAmount
                .Text = VB6.Format(mRate * mOctQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMTotalAmount = mRMTotalAmount + Val(.Text)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColNovQty
                .Text = VB6.Format(mNovQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mNovQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                mRMQty = GetPurchaseQty(11, mRMCode) 'IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColNovRate
                mDate = "30/11/" & VB6.Format(RsCompany.Fields("Start_Date").Value, "YYYY")
                mRate = 0
                If mNovQty <> 0 Then
                    If optRate(0).Checked = True Then
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    Else
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                    If mRate = 0 And mRMQty > 0 Then
                        mDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                End If
                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColNovAmount
                .Text = VB6.Format(mRate * mNovQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMTotalAmount = mRMTotalAmount + Val(.Text)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColDecQty
                .Text = VB6.Format(mDecQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mDecQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                mRMQty = GetPurchaseQty(12, mRMCode) ' IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColDecRate
                mDate = "31/12/" & VB6.Format(RsCompany.Fields("Start_Date").Value, "YYYY")
                mRate = 0
                If mDecQty <> 0 Then
                    If optRate(0).Checked = True Then
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    Else
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                    If mRate = 0 And mRMQty > 0 Then
                        mDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                End If

                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColDecAmount
                .Text = VB6.Format(mRate * mDecQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMTotalAmount = mRMTotalAmount + Val(.Text)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColJanQty
                .Text = VB6.Format(mJanQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mJanQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                mRMQty = GetPurchaseQty(1, mRMCode) 'IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColJanRate
                mDate = "31/01/" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
                mRate = 0
                If mJanQty <> 0 Then
                    If optRate(0).Checked = True Then
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    Else
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                    If mRate = 0 And mRMQty > 0 Then
                        mDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                End If
                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColJanAmount
                .Text = VB6.Format(mRate * mJanQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMTotalAmount = mRMTotalAmount + Val(.Text)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColFebQty
                .Text = VB6.Format(mFebQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mFebQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                mRMQty = GetPurchaseQty(2, mRMCode) 'IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColFebRate
                mDate = "28/02/" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
                mRate = 0
                If mFebQty <> 0 Then
                    If optRate(0).Checked = True Then
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    Else
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                    If mRate = 0 And mRMQty > 0 Then
                        mDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                End If
                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColFebAmount
                .Text = VB6.Format(mRate * mFebQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMTotalAmount = mRMTotalAmount + Val(.Text)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColMarQty
                .Text = VB6.Format(mMarQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mMarQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                mRMQty = GetPurchaseQty(3, mRMCode) 'IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColMarRate
                mDate = "31/03/" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
                mRate = 0
                If mMarQty <> 0 Then
                    If optRate(0).Checked = True Then
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    Else
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(txtRateAsOn.Text, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                    If mRate = 0 And mRMQty > 0 Then
                        mDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
                        mRate = GetLatestItemCostFromMRR(Trim(mRMCode), mItemUOM, mRMQty, VB6.Format(mDate, "DD/MM/YYYY"), "L") / mRMQty
                    End If
                End If
                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColMarAmount
                .Text = VB6.Format(mRate * mMarQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMTotalAmount = mRMTotalAmount + Val(.Text)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColTotalQty
                .Text = VB6.Format(mTotalQty * Val(CStr(mStdQty * mFactorQty)), "0.00")
                mRMQty = CDbl(VB6.Format(mTotalQty * Val(CStr(mStdQty * mFactorQty)), "0.00"))
                '            mRMQty = IIf(mRMQty = 0, 1, mRMQty)	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColTotalRate
                If mRMQty = 0 Then
                    mRate = 0
                Else
                    mRate = mRMTotalAmount / mRMQty
                End If
                .Text = VB6.Format(mRate, "0.00")
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColTotalAmount
                .Text = VB6.Format(mRMTotalAmount, "0.00") ''Format(mRate * mTotalQty * Val(mStdQty * mFactorQty), "0.00")	
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))


                .Col = ColFlag
                .Text = "0"

                mStdQty = 1
                SprdMain.MaxRows = SprdMain.MaxRows + 1
                mcntRow = mcntRow + 1
                mRMTotalAmount = 0
            End If
        End With
        '    Call FillSubAlterRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mDeptCode, pParentCode, _	
        ''                            mAprQty, mAprRate, mAprAmount, _	
        ''                            mMayQty, mMayRate, mMayAmount, _	
        ''                            mJunQty, mJunRate, mJunAmount, _	
        ''                            mJulQty, mJulRate, mJulAmount, _	
        ''                            mAugQty, mAugRate, mAugAmount, _	
        ''                            mSepQty, mSepRate, mSepAmount, _	
        ''                            mOctQty, mOctRate, mOctAmount, _	
        ''                            mNovQty, mNovRate, mNovAmount, _	
        ''                            mDecQty, mDecRate, mDecAmount, _	
        ''                            mJanQty, mJanRate, mJanAmount, _	
        ''                            mFebQty, mFebRate, mFebAmount, _	
        ''                            mMarQty, mMarRate, mMarAmount, _	
        ''                            mTotalQty, mTotalRate, mTotalAmount, _	
        ''                            mCustomerCode, mCustomerName)	

        '    Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode, _	
        ''                            mAprQty, mAprRate, mAprAmount, _	
        ''                            mMayQty, mMayRate, mMayAmount, _	
        ''                            mJunQty, mJunRate, mJunAmount, _	
        ''                            mJulQty, mJulRate, mJulAmount, _	
        ''                            mAugQty, mAugRate, mAugAmount, _	
        ''                            mSepQty, mSepRate, mSepAmount, _	
        ''                            mOctQty, mOctRate, mOctAmount, _	
        ''                            mNovQty, mNovRate, mNovAmount, _	
        ''                            mDecQty, mDecRate, mDecAmount, _	
        ''                            mJanQty, mJanRate, mJanAmount, _	
        ''                            mFebQty, mFebRate, mFebAmount, _	
        ''                            mMarQty, mMarRate, mMarAmount, _	
        ''                            mTotalQty, mTotalRate, mTotalAmount, _	
        ''                            mCustomerCode, mCustomerName)	

        Exit Sub
FillGERR:
        '    Resume	
        MsgBox(Err.Description)
    End Sub
    Private Function GetCommonFinishedGood(ByRef pProductCode As String, ByRef mRMCode As String) As String

        On Error GoTo ErrPart
        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

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






    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef mAprQty As Double, ByRef mAprRate As Double, ByRef mAprAmount As Double, ByRef mMayQty As Double, ByRef mMayRate As Double, ByRef mMayAmount As Double, ByRef mJunQty As Double, ByRef mJunRate As Double, ByRef mJunAmount As Double, ByRef mJulQty As Double, ByRef mJulRate As Double, ByRef mJulAmount As Double, ByRef mAugQty As Double, ByRef mAugRate As Double, ByRef mAugAmount As Double, ByRef mSepQty As Double, ByRef mSepRate As Double, ByRef mSepAmount As Double, ByRef mOctQty As Double, ByRef mOctRate As Double, ByRef mOctAmount As Double, ByRef mNovQty As Double, ByRef mNovRate As Double, ByRef mNovAmount As Double, ByRef mDecQty As Double, ByRef mDecRate As Double, ByRef mDecAmount As Double, ByRef mJanQty As Double, ByRef mJanRate As Double, ByRef mJanAmount As Double, ByRef mFebQty As Double, ByRef mFebRate As Double, ByRef mFebAmount As Double, ByRef mMarQty As Double, ByRef mMarRate As Double, ByRef mMarAmount As Double, ByRef mTotalQty As Double, ByRef mTotalRate As Double, ByRef mTotalAmount As Double, ByRef mCustomerCode As String, ByRef mCustomerName As String, ByRef mStdQty As Double)


        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String
        Dim j As Integer
        Dim mIssueUOM As String
        Dim xStdQty As Double
        Dim xScrapQty As Double

        mSrn = pSrn
        pLevel = pLevel + 1
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
        '& " AND WEF<= '" & vb6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf _

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
                    mStdQty = mStdQty * (Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                Else
                    xStdQty = Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value))
                    xScrapQty = Val(IIf(IsDBNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value))
                    If PubUserID = "A00001" Then
                        mIssueUOM = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                        If mIssueUOM = "KGS" Or mIssueUOM = "TON" Then
                            xStdQty = xStdQty - (xStdQty * 4 * 0.01)

                            If RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 1 Then
                                xScrapQty = xScrapQty - (xScrapQty * 15 * 0.01)
                            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 28 Then
                                xScrapQty = xScrapQty - (xScrapQty * 30 * 0.01)
                            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                                xScrapQty = xScrapQty - (xScrapQty * 15 * 0.01)
                            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 25 Or RsCompany.Fields("COMPANY_CODE").Value = 32 Then
                                xScrapQty = xScrapQty - (xScrapQty * 20 * 0.01)
                            End If
                        End If
                    End If
                    mStdQty = mStdQty * (xStdQty + xScrapQty)
                End If


                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode, mAprQty, mAprRate, mAprAmount, mMayQty, mMayRate, mMayAmount, mJunQty, mJunRate, mJunAmount, mJulQty, mJulRate, mJulAmount, mAugQty, mAugRate, mAugAmount, mSepQty, mSepRate, mSepAmount, mOctQty, mOctRate, mOctAmount, mNovQty, mNovRate, mNovAmount, mDecQty, mDecRate, mDecAmount, mJanQty, mJanRate, mJanAmount, mFebQty, mFebRate, mFebAmount, mMarQty, mMarRate, mMarAmount, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mStdQty)
                RsShow.MoveNext()
            Loop
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ITEM_QTY * (CASE WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 ELSE 1 END) AS STD_QTY, ID.SCRAP_QTY * (CASE WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 ELSE 1 END) AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O' AND IS_INHOUSE='N'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') "

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

                    mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                    If optCalcOn(0).Checked = True Then
                        mStdQty = mStdQty * (Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                    Else
                        xStdQty = Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value))
                        xScrapQty = Val(IIf(IsDBNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value))
                        If PubUserID = "A00001" Then
                            mIssueUOM = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                            If mIssueUOM = "KGS" Or mIssueUOM = "TON" Then
                                xStdQty = xStdQty - (xStdQty * 4 * 0.01)
                                If RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 1 Then
                                    xScrapQty = xScrapQty - (xScrapQty * 15 * 0.01)
                                ElseIf RsCompany.Fields("COMPANY_CODE").Value = 28 Then
                                    xScrapQty = xScrapQty - (xScrapQty * 30 * 0.01)
                                ElseIf RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                                    xScrapQty = xScrapQty - (xScrapQty * 15 * 0.01)
                                ElseIf RsCompany.Fields("COMPANY_CODE").Value = 25 Or RsCompany.Fields("COMPANY_CODE").Value = 32 Then
                                    xScrapQty = xScrapQty - (xScrapQty * 20 * 0.01)
                                End If
                            End If
                        End If
                        mStdQty = mStdQty * (xStdQty + xScrapQty)

                        '                    mStdQty = mStdQty * ((Val(IIf(IsNull(RsShow!STD_QTY), 0, RsShow!STD_QTY)) + Val(IIf(IsNull(RsShow!GROSS_WT_SCRAP), 0, RsShow!GROSS_WT_SCRAP))))	
                    End If



                    Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode, mAprQty, mAprRate, mAprAmount, mMayQty, mMayRate, mMayAmount, mJunQty, mJunRate, mJunAmount, mJulQty, mJulRate, mJulAmount, mAugQty, mAugRate, mAugAmount, mSepQty, mSepRate, mSepAmount, mOctQty, mOctRate, mOctAmount, mNovQty, mNovRate, mNovAmount, mDecQty, mDecRate, mDecAmount, mJanQty, mJanRate, mJanAmount, mFebQty, mFebRate, mFebAmount, mMarQty, mMarRate, mMarAmount, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mStdQty)
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
        SqlStr = " SELECT " & vbCrLf _
            & " IH.PRODUCT_CODE, ID.RM_CODE " & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O' AND IH.IS_BOP='N'" & vbCrLf _
            & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') "       '& vbCrLf _
        '& " AND WEF<= '" & vb6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf _

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            '        Do While Not RsShow.EOF	
            '           mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))	
            CheckSubRecord = True
            '        Loop	
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.ITEM_CODE AS RM_CODE " & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O' AND IS_INHOUSE='N' AND IH.IS_BOP='N'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O')"

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


    Private Sub FillSubAlterRecord(ByRef pRMMainCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef pDeptCode As String, ByRef pParentCode As String, ByRef mAprQty As Double, ByRef mAprRate As Double, ByRef mAprAmount As Double, ByRef mMayQty As Double, ByRef mMayRate As Double, ByRef mMayAmount As Double, ByRef mJunQty As Double, ByRef mJunRate As Double, ByRef mJunAmount As Double, ByRef mJulQty As Double, ByRef mJulRate As Double, ByRef mJulAmount As Double, ByRef mAugQty As Double, ByRef mAugRate As Double, ByRef mAugAmount As Double, ByRef mSepQty As Double, ByRef mSepRate As Double, ByRef mSepAmount As Double, ByRef mOctQty As Double, ByRef mOctRate As Double, ByRef mOctAmount As Double, ByRef mNovQty As Double, ByRef mNovRate As Double, ByRef mNovAmount As Double, ByRef mDecQty As Double, ByRef mDecRate As Double, ByRef mDecAmount As Double, ByRef mJanQty As Double, ByRef mJanRate As Double, ByRef mJanAmount As Double, ByRef mFebQty As Double, ByRef mFebRate As Double, ByRef mFebAmount As Double, ByRef mMarQty As Double, ByRef mMarRate As Double, ByRef mMarAmount As Double, ByRef mTotalQty As Double, ByRef mTotalRate As Double, ByRef mTotalAmount As Double, ByRef mCustomerCode As String, ByRef mCustomerName As String, ByRef mStdQty As Double)


        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String

        mSrn = pSrn
        '    pLevel = pLevel + 1	

        If pDeptCode <> "J/W" Then
            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_RM_CODE AS RM_CODE, '(*) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_STD_QTY * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) AS STD_QTY, ID.ALETRSCRAP * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf _
                & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET IDET, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                & " WHERE IH.MKEY=IDET.MKEY " & vbCrLf _
                & " AND IDET.MKEY=ID.MKEY " & vbCrLf _
                & " AND IDET.RM_CODE=ID.MAINITEM_CODE " & vbCrLf _
                & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                & " AND ID.ALTER_RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(pRMMainCode) & "'" & vbCrLf _
                & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pMainProductCode) & "' AND IH.STATUS='O'" & vbCrLf _
                & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pMainProductCode) & "' AND STATUS='O') "       '& vbCrLf _
            '& " AND WEF<= '" & vb6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf _

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_ITEM_CODE AS RM_CODE, '(**) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_ITEM_QTY * (CASE WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 ELSE 1 END) AS STD_QTY, ID.ALTER_SCRAP_QTY * (CASE WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 ELSE 1 END) AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf _
                & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " and ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                & " AND ID.ALTER_ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pRMMainCode) & "' " & vbCrLf _
                & " AND ID.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "' AND IH.STATUS='O' AND IS_INHOUSE='N'" & vbCrLf _
                & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "' AND STATUS='O') "


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
                    mStdQty = mStdQty * (Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                Else
                    mStdQty = mStdQty * ((Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)) + Val(IIf(IsDBNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value))))
                End If
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pRMMainCode, mAprQty, mAprRate, mAprAmount, mMayQty, mMayRate, mMayAmount, mJunQty, mJunRate, mJunAmount, mJulQty, mJulRate, mJulAmount, mAugQty, mAugRate, mAugAmount, mSepQty, mSepRate, mSepAmount, mOctQty, mOctRate, mOctAmount, mNovQty, mNovRate, mNovAmount, mDecQty, mDecRate, mDecAmount, mJanQty, mJanRate, mJanAmount, mFebQty, mFebRate, mFebAmount, mMarQty, mMarRate, mMarAmount, mTotalQty, mTotalRate, mTotalAmount, mCustomerCode, mCustomerName, mStdQty)
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
    Public Sub frmParamMaterialBudgetYearly_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Material Budget Report"
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamMaterialBudgetYearly_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection	
        ''PvtDBCn.Open StrConn	
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = VB6.TwipsToPixelsY(24)
        Me.Left = VB6.TwipsToPixelsX(24)
        Me.Height = VB6.TwipsToPixelsY(7440)
        Me.Width = VB6.TwipsToPixelsX(11625)


        '    txtDateFrom.Text = Format(RsCompany!START_DATE, "DD/MM/YYYY")	
        '    txtDateTo.Text = Format(RunDate, "DD/MM/YYYY")	

        cboType.Items.Clear()
        cboType.Items.Add("All")
        cboType.Items.Add("Sale")
        cboType.Items.Add("Jobwork")
        cboType.SelectedIndex = 0


        chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        chkFG_CheckStateChanged(chkFG, New System.EventArgs())
        chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked

        txtCustomerName.Enabled = False
        cmdsearchCustName.Enabled = False


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
            .ColHidden = False

            .Col = ColProductDesc
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

            For cntCol = ColAprQty To ColTotalAmount
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

            .ColsFrozen = ColProductQty

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
        Dim cntCol As Integer
        Dim mMonthCount As Integer
        Dim mMonthName As String
        Dim mMonthCol As Integer

        With SprdMain
            .MaxCols = ColFlag
            .Row = 0

            mMonthCount = 4
            mMonthCol = 1
            For cntCol = ColAprQty To ColMarAmount
                mMonthName = MonthName(mMonthCount)

                .Col = cntCol
                .Text = mMonthName & " " & IIf(mMonthCol = 1, "Qty", IIf(mMonthCol = 2, "Rate", "Amount"))

                If mMonthCol = 3 Then
                    mMonthCount = mMonthCount + 1
                    If mMonthCount = 13 Then
                        mMonthCount = 1
                    End If
                    mMonthCol = 0
                End If

                mMonthCol = mMonthCol + 1
            Next

            .Col = ColTotalQty
            .Text = "Total Qty"

            .Col = ColTotalRate
            .Text = "Total Rate"

            .Col = ColTotalAmount
            .Text = "Total Amount"

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
    Private Sub frmParamMaterialBudgetYearly_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

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
