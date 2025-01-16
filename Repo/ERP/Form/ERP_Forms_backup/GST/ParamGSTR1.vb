Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamGSTR1
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20


    Private Const ColGSTIN As Short = 1
    Private Const ColBillNo As Short = 2
    Private Const ColBillDate As Short = 3
    Private Const ColItemAmount As Short = 4
    Private Const ColGoodDesc As Short = 5
    Private Const ColHSNCode As Short = 6
    Private Const ColTaxableValue As Short = 7
    Private Const ColIGSTPer As Short = 8
    Private Const ColIGSTAmount As Short = 9
    Private Const ColCGSTPer As Short = 10
    Private Const ColCGSTAmount As Short = 11
    Private Const ColSGSTPer As Short = 12
    Private Const ColSGSTAmount As Short = 13
    Private Const ColPOS As Short = 14
    Private Const ColReverseCharge As Short = 15
    Private Const ColUnderProAssess As Short = 16
    Private Const ColeCommerce As Short = 17
    Private Const ColMKEY As Short = 18

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonST(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonST(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportonST(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mHeading As String

        Report1.Reset()
        mTitle = "FORM GSTR - 1"
        mSubTitle = "(See Rule : )"
        mHeading = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")


        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\GSTR1.RPT"

        '    SqlStr = MakeSQL
        '    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mHeading)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef pHeading As String)

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        MainClass.AssignCRptFormulas(Report1, "QuarterEnded=""" & UCase(pHeading) & """")
        Report1.WindowShowGroupTree = False
        '    Report1.WindowShowPrintBtn = IIf(PubGridLockUser = "Y", False, True) '' IIf(PubSuperUser = "S", True, False)
        '    Report1.WindowShowPrintSetupBtn = IIf(PubGridLockUser = "Y", False, True) ''IIf(PubSuperUser = "S", True, False)
        '    Report1.WindowShowExportBtn = IIf(PubGridLockUser = "Y", False, True)
        Report1.Action = 1
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim SqlStr1 As String
        Dim mCompanyCode As String
        Dim pDateFrom As String
        Dim pDateTo As String
        Dim cntRow As Integer
        Dim mHSNCode As String
        Dim mHSNDesc As String


        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        MainClass.ClearGrid(SprdMain5A, RowHeight)
        MainClass.ClearGrid(SprdMain6, RowHeight)
        MainClass.ClearGrid(SprdMain6A, RowHeight)

        MainClass.ClearGrid(SprdMain7, RowHeight)
        MainClass.ClearGrid(SprdMain7A, RowHeight)

        MainClass.ClearGrid(SprdMain8, RowHeight)
        MainClass.ClearGrid(SprdMain8A, RowHeight)

        MainClass.ClearGrid(SprdMain9, RowHeight)
        MainClass.ClearGrid(SprdMain10, RowHeight)
        MainClass.ClearGrid(SprdMain11, RowHeight)

        pDateFrom = VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")
        pDateTo = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        mCompanyCode = Trim(cboGSTNO.Text)

        If mCompanyCode = "" Then
            MsgInformation("Please Select GST No.")
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '********************************
        ''5
        SqlStr = ""
        If Show_VWGSTR1_B2B(SqlStr, PubDBCnView, mCompanyCode, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '    ''6
        SqlStr = ""
        If Show_VWGSTR1_B2C(SqlStr, PubDBCnView, mCompanyCode, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain5A, StrConn, "Y")
        '
        '    ''7
        SqlStr = ""
        If Show_VWGSTR1_B2CS(SqlStr, PubDBCnView, mCompanyCode, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain6, StrConn, "Y")

        ''8
        SqlStr = ""
        If Show_VWGSTR1_DNCN_REG(SqlStr, PubDBCnView, mCompanyCode, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain6A, StrConn, "Y")

        ''9
        SqlStr = ""
        If Show_VWGSTR1_DNCN_UNREG(SqlStr, PubDBCnView, mCompanyCode, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain7, StrConn, "Y")

        ''10
        SqlStr = ""
        If Show_VWGSTR1_EXPORT(SqlStr, PubDBCnView, mCompanyCode, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain7A, StrConn, "Y")

        ''11
        SqlStr = ""
        If Show_VWGSTR1_ADVANCE(SqlStr, PubDBCnView, mCompanyCode, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain8, StrConn, "Y")

        '    ''12
        '    SqlStr = ""
        '    If Show_VWGSTR1_NILRATE(SqlStr, PubDBCnView, mCompanyCode, pDateFrom, pDateTo) = False Then GoTo ErrPart
        '    MainClass.AssignDataInSprd SqlStr, AData12, StrConn, "Y"

        ''13
        SqlStr = ""
        If Show_VWGSTR1_TAXPAID(SqlStr, PubDBCnView, mCompanyCode, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain8A, StrConn, "Y")

        ''14
        SqlStr = ""
        If Show_VWGSTR1_HSN(SqlStr, PubDBCnView, mCompanyCode, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain9, StrConn, "Y")

        ''15
        SqlStr = ""
        If Show_VWGSTR1_DocIssued(SqlStr, PubDBCnView, mCompanyCode, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain10, StrConn, "Y")



        ''16
        SqlStr = ""
        If Show_VWGSTR1_NILRATE(SqlStr, PubDBCnView, mCompanyCode, pDateFrom, pDateTo) = False Then GoTo ErrPart
        MainClass.AssignDataInSprd8(SqlStr, SprdMain11, StrConn, "Y")

        '********************************

        With SprdMain9
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mHSNCode = Trim(.Text)
                mHSNDesc = ""

                If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mHSNDesc = MasterNo
                End If

                .Col = 2
                .Text = UCase(Trim(mHSNDesc))
            Next
        End With

        Call PrintStatus(True)
        CalcSprdTotal()
        Call FormatSpreadSheet()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default



        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function Show_VWGSTR1_B2B(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr

        ''CMST.SUPP_CUST_NAME


        ''GSTIN/UIN of Recipient  Invoice Number  Invoice date    Invoice Value   Place Of Supply Reverse Charge  Invoice Type    E-Commerce GSTIN    Rate    Taxable Value   Cess Amount

        SqlStr = " SELECT " & vbCrLf _
            & " CMST.GST_RGN_NO AS counter_party_gstin, " & vbCrLf _
            & " IH.BILLNO AS invoice_number," & vbCrLf _
            & " IH.INVOICE_DATE invoice_date, MAX(IH.NETVALUE) AS invoice_value," & vbCrLf _
            & " STATE_CODE ||'-'||SMST.NAME AS place_of_supply," & vbCrLf _
            & " 'N' AS reverse_charge, " & vbCrLf _
            & " 'Regular' AS invoice_type, " & vbCrLf _
            & " '' AS etin, " & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS rate, " & vbCrLf _
            & " SUM(ID.GSTABLE_AMT) AS tax_value,  SUM(CGST_AMOUNT) AS CGST_AMOUNT, SUM(SGST_AMOUNT) AS SGST_AMOUNT, SUM(IGST_AMOUNT) AS IGST_AMOUNT, " & vbCrLf _
            & " 0 As Cess, ACC.SUPP_CUST_NAME AS ACCOUNT_HEAD"


        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_BUSINESS_MST CMST, FIN_SUPP_CUST_BUSINESS_MST CSMST, FIN_SUPP_CUST_MST ACM, GEN_STATE_MST SMST, FIN_SUPP_CUST_MST ACC " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACC.COMPANY_CODE " & vbCrLf _
            & " AND IH.ACCOUNTCODE=ACC.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CSMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.SHIP_TO_LOC_ID=CSMST.LOCATION_ID    " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf _
            & " AND IH.INVOICESEQTYPE IN (1,2) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND ACM.GST_REGD= 'Y' " & vbCrLf _
            & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf _
            & " AND IH.REF_DESP_TYPE<>'U' " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' AND IH.CANCELLED='N' AND (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER)>0"

        SqlStr = SqlStr & vbCrLf & " GROUP BY CMST.GST_RGN_NO, " & vbCrLf _
            & " IH.BILLNO," & vbCrLf _
            & " IH.INVOICE_DATE," & vbCrLf _
            & " STATE_CODE ||'-'||SMST.NAME," & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER),ACC.SUPP_CUST_NAME "


        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & " SELECT CMST.GST_RGN_NO AS counter_party_gstin, " & vbCrLf _
            & " IH.BILLNO AS invoice_number," & vbCrLf _
            & " IH.INVOICE_DATE invoice_date, IH.NETVALUE AS invoice_value," & vbCrLf _
            & " STATE_CODE ||'-'||SMST.NAME AS place_of_supply," & vbCrLf _
            & " 'N' AS reverse_charge, " & vbCrLf _
            & " 'Regular' AS invoice_type, " & vbCrLf _
            & " '' AS etin, " & vbCrLf _
            & " (NET_CGST_PER+NET_SGST_PER+NET_IGST_PER) AS rate, " & vbCrLf & " IH.TOTTAXABLEAMOUNT AS tax_value, " & vbCrLf _
            & " NETCGST_AMOUNT AS CGST_AMOUNT, NETSGST_AMOUNT AS SGST_AMOUNT, NETIGST_AMOUNT AS IGST_AMOUNT, 0 As Cess, ACC.SUPP_CUST_NAME AS ACCOUNT_HEAD"


        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_BUSINESS_MST CMST, FIN_SUPP_CUST_MST ACM,GEN_STATE_MST SMST,FIN_SUPP_CUST_MST ACC " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACC.COMPANY_CODE " & vbCrLf _
            & " AND IH.ACCOUNTCODE=ACC.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf _
            & " AND IH.INVOICESEQTYPE IN (4) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND ACM.GST_REGD='Y' " & vbCrLf _
            & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' AND IH.CANCELLED='N' AND (NET_CGST_PER+NET_SGST_PER+NET_IGST_PER) >0"

        SqlStr = SqlStr & vbCrLf & " ORDER BY 2"
        Show_VWGSTR1_B2B = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_VWGSTR1_B2B = False
        '    Resume
    End Function

    Private Function Show_VWGSTR1_B2C(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr
        'Dim SqlStr As String=""=""
        'Dim RsTemp As ADODB.Recordset = Nothing


        SqlStr = " SELECT " & vbCrLf & " IH.BILLNO AS invoice_number, " & vbCrLf & " IH.INVOICE_DATE invoice_date, " & vbCrLf & " MAX(IH.NETVALUE) AS invoice_value, " & vbCrLf & " STATE_CODE ||'-'||SMST.NAME AS place_of_supply, " & vbCrLf & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS rate, " & vbCrLf & " SUM(ID.GSTABLE_AMT) AS tax_value,  " & vbCrLf & " 0 AS CESS_amount, " & vbCrLf & " '' AS etin " & vbCrLf & " FROM FIN_INVOICE_HDR IH, " & vbCrLf & " FIN_INVOICE_DET ID, GEN_COMPANY_MST GMST, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST CSMST, " & vbCrLf & " GEN_STATE_MST SMST "

        SqlStr = SqlStr & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=CSMST.COMPANY_CODE " & vbCrLf & " AND IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf & " AND IH.INVOICESEQTYPE IN (1,2) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND CMST.GST_REGD='N' " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf & " AND CMST.WITHIN_STATE='N' " & vbCrLf & " AND IH.REF_DESP_TYPE<>'U' " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " AND IH.NETVALUE>250000 "

        SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " IH.BILLNO, " & vbCrLf & " IH.INVOICE_DATE, " & vbCrLf & " STATE_CODE ||'-'||SMST.NAME, " & vbCrLf & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER)"


        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & " SELECT " & vbCrLf & " IH.BILLNO AS invoice_number, " & vbCrLf & " IH.INVOICE_DATE invoice_date, " & vbCrLf & " IH.NETVALUE AS invoice_value, " & vbCrLf & " STATE_CODE ||'-'||SMST.NAME AS place_of_supply, " & vbCrLf & " (NET_CGST_PER+NET_SGST_PER+NET_IGST_PER) AS rate, " & vbCrLf & " IH.TOTTAXABLEAMOUNT AS tax_value, " & vbCrLf & " 0 AS CESS_amount, " & vbCrLf & " '' AS etin " & vbCrLf & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST "


        SqlStr = SqlStr & vbCrLf & " WHERE  " & vbCrLf & " IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf & " AND IH.INVOICESEQTYPE IN (4) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND CMST.GST_REGD='N' " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf & " AND CMST.WITHIN_STATE='N' " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " AND IH.NETVALUE>250000 "

        '    pPubDBCnView.Execute SqlStr

        Show_VWGSTR1_B2C = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_VWGSTR1_B2C = False
        '    Resume
    End Function
    Private Sub FormatSpreadSheet()
        On Error GoTo ErrPart

        FormatSprdMain(-1)
        FormatSprdMain5A(-1)
        FormatSprdMain6(-1)
        FormatSprdMain6A(-1)
        FormatSprdMain7(-1)
        FormatSprdMain7A(-1)

        FormatSprdMain8(-1)
        FormatSprdMain8A(-1)
        FormatSprdMain9(-1)
        FormatSprdMain10(-1)
        FormatSprdMain11(-1)
        '    FormatSprdMain10A -1
        '    FormatSprdMain11 -1
        '    FormatSprdMain11A -1
        '    FormatSprdMain12 -1
        '    FormatSprdMain13 -1
        '    FormatSprdMain13A -1
        '    FormatSprdMain13B -1
        '    FormatSprdMain14 -1

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGSTR1_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "GSTR - 1 (Details of outward Supplies)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function Show_VWGSTR1_B2CS(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr
        'Dim SqlStr As String=""=""
        'Dim RsTemp As ADODB.Recordset = Nothing

        ''Type    Place Of Supply Rate    Taxable Value   Cess Amount E-Commerce GSTIN

        SqlStr = " SELECT " & vbCrLf & " 'E', " & vbCrLf & " STATE_CODE ||'-'||SMST.NAME  AS place_of_supply, " & vbCrLf & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS rate, " & vbCrLf & " SUM(ID.GSTABLE_AMT) AS tax_value,  " & vbCrLf & " 0 AS CESS_amount, " & vbCrLf & " '' AS etin " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST CSMST, GEN_STATE_MST SMST " & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE "


        SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=CSMST.COMPANY_CODE " & vbCrLf & " AND IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf & " AND IH.INVOICESEQTYPE IN (1,2) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND CMST.GST_REGD='N' " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf & " AND IH.REF_DESP_TYPE<>'U' " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " AND IH.NETVALUE<DECODE(CMST.WITHIN_STATE,'N',250000,500000000) "

        SqlStr = SqlStr & vbCrLf & " GROUP BY STATE_CODE ||'-'||SMST.NAME, " & vbCrLf & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER)"

        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & " SELECT " & vbCrLf & " 'E', " & vbCrLf & " STATE_CODE ||'-'||SMST.NAME  AS place_of_supply, " & vbCrLf & " (NET_CGST_PER+NET_SGST_PER+NET_IGST_PER) AS rate, " & vbCrLf & " SUm(IH.TOTTAXABLEAMOUNT) AS tax_value,  " & vbCrLf & " 0 AS CESS_amount, " & vbCrLf & " '' AS etin " & vbCrLf & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST " & vbCrLf & " WHERE  " & vbCrLf & " IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE "

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf & " AND IH.INVOICESEQTYPE IN (4) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND CMST.GST_REGD='N' " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " AND IH.NETVALUE<DECODE(CMST.WITHIN_STATE,'N',250000,500000000) " & vbCrLf & " GROUP BY STATE_CODE ||'-'||SMST.NAME, " & vbCrLf & " (NET_CGST_PER+NET_SGST_PER+NET_IGST_PER)"


        '    pPubDBCnView.Execute SqlStr

        Show_VWGSTR1_B2CS = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_VWGSTR1_B2CS = False
        '    Resume
    End Function
    Private Function Show_VWGSTR1_DNCN_REG(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr

        ''Document Type   Reason For Issuing document Place Of Supply Note/Refund Voucher Value   Rate    Taxable Value   Cess Amount Pre GST

        ''FIN_INVOICE_HDR OIH,   DSP_DESPATCH_DET DD,

        SqlStr = " SELECT " & vbCrLf _
            & " CMST.GST_RGN_NO AS counter_party_gstin, " & vbCrLf _
            & " '' AS invoice_number, " & vbCrLf _
            & " '' AS invoice_date, " & vbCrLf _
            & " IH.BILLNO AS note_number, " & vbCrLf _
            & " IH.INVOICE_DATE note_date, " & vbCrLf _
            & " 'D' AS note_type, " & vbCrLf & " '07-Others' AS reason, " & vbCrLf _
            & " STATE_CODE ||'-'||SMST.NAME  AS place_of_supply, " & vbCrLf & " IH.NETVALUE AS invoice_value, " & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS rate, " & vbCrLf & " SUM(ID.GSTABLE_AMT) AS tax_value,  " & vbCrLf _
            & " SUM(ID.CGST_AMOUNT) AS CGST_AMOUNT, SUM(ID.SGST_AMOUNT) AS SGST_AMOUNT, SUM(ID.IGST_AMOUNT) AS IGST_AMOUNT, 0 AS CESS, " & vbCrLf _
            & " 'Y' AS pre_gst_cdn, ACC.SUPP_CUST_NAME AS ACCOUNT_HEAD"

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, GEN_COMPANY_MST GMST,FIN_SUPP_CUST_MST ACM, FIN_SUPP_CUST_BUSINESS_MST CMST, FIN_SUPP_CUST_BUSINESS_MST CSMST, GEN_STATE_MST SMST,FIN_SUPP_CUST_MST ACC " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACC.COMPANY_CODE " & vbCrLf _
            & " AND IH.ACCOUNTCODE=ACC.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID " & vbCrLf _
            & " AND IH.COMPANY_CODE=CSMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.SHIP_TO_LOC_ID=CSMST.LOCATION_ID " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME "

        '& vbCrLf _
        '    & " AND IH.COMPANY_CODE=DD.COMPANY_CODE " & vbCrLf _
        '    & " AND IH.AUTO_KEY_DESP=DD.AUTO_KEY_DESP " & vbCrLf _
        '    & " AND ID.SUBROWNO=DD.SERIAL_NO " & vbCrLf _
        '    & " AND DD.COMPANY_CODE=OIH.COMPANY_CODE(+) "& vbCrLf _
        '& " AND DD.REF_NO=OIH.AUTO_KEY_INVOICE(+) "

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.INVOICESEQTYPE IN (9) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND ACM.GST_REGD='Y' " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf _
            & " AND IH.REF_DESP_TYPE='U' " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO "

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY CMST.GST_RGN_NO, IH.BILLNO, IH.INVOICE_DATE, STATE_CODE ||'-'||SMST.NAME, IH.NETVALUE,(ID.CGST_PER+ID.SGST_PER+ID.IGST_PER),ACC.SUPP_CUST_NAME"

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf _
            & " CMST.GST_RGN_NO AS counter_party_gstin, " & vbCrLf _
            & " '' AS invoice_number, " & vbCrLf _
            & " '' AS invoice_date, " & vbCrLf _
            & " IH.REJ_CREDITNOTE AS note_number, " & vbCrLf & " IH.VDATE note_date, " & vbCrLf _
            & " 'C' AS note_type, " & vbCrLf & " '01-SALES RETURN' AS reason, " & vbCrLf _
            & " STATE_CODE ||'-'||SMST.NAME  AS place_of_supply, " & vbCrLf & " IH.NETVALUE AS invoice_value, " & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS rate, " & vbCrLf _
            & " SUM(ID.GSTABLE_AMT) AS tax_value,  " & vbCrLf _
            & " SUM(ID.CGST_AMOUNT) AS CGST_AMOUNT, SUM(ID.SGST_AMOUNT) AS SGST_AMOUNT, SUM(ID.IGST_AMOUNT) AS IGST_AMOUNT,0 AS CESS, " & vbCrLf _
            & " 'Y' AS pre_gst_cdn, ACC.SUPP_CUST_NAME AS ACCOUNT_HEAD"



        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, GEN_COMPANY_MST GMST,FIN_SUPP_CUST_MST ACM, FIN_SUPP_CUST_BUSINESS_MST CMST, FIN_SUPP_CUST_BUSINESS_MST CSMST, GEN_STATE_MST SMST,FIN_SUPP_CUST_MST ACC " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACC.COMPANY_CODE " & vbCrLf _
            & " AND IH.ACCOUNTCODE=ACC.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID " & vbCrLf _
            & " AND IH.COMPANY_CODE=CSMST.COMPANY_CODE "

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.SHIP_TO_LOC_ID=CSMST.LOCATION_ID " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf _
            & " AND IH.INVOICE_DATE>='01-Jul-2017' AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND ACM.GST_REGD='Y' " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            & " AND IH.PURCHASESEQTYPE=2 AND IH.CANCELLED='N'" & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO "

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY CMST.GST_RGN_NO, IH.REJ_CREDITNOTE, IH.VDATE, STATE_CODE ||'-'||SMST.NAME, IH.NETVALUE,(ID.CGST_PER+ID.SGST_PER+ID.IGST_PER),ACC.SUPP_CUST_NAME"

        ''AND REJECTION='Y'

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf _
            & " CMST.GST_RGN_NO AS counter_party_gstin, " & vbCrLf _
            & " '' AS invoice_number, " & vbCrLf _
            & " '' AS invoice_date, " & vbCrLf _
            & " NVL(IH.PARTY_DNCN_NO,IH.VNO) AS note_number, " & vbCrLf _
            & " NVL(PARTY_DNCN_DATE,IH.VDATE) note_date, " & vbCrLf _
            & " DECODE(IH.BOOKTYPE,'L','C','D') AS note_type, " & vbCrLf _
            & " '09-OTHERS' AS reason, " & vbCrLf _
            & " STATE_CODE ||'-'||SMST.NAME  AS place_of_supply, " & vbCrLf _
            & " IH.NETVALUE AS invoice_value, " & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS rate, " & vbCrLf _
            & " SUM(ID.AMOUNT) AS tax_value,  " & vbCrLf _
            & " SUM(ID.CGST_AMOUNT) AS CGST_AMOUNT, SUM(ID.SGST_AMOUNT) AS SGST_AMOUNT, SUM(ID.IGST_AMOUNT) AS IGST_AMOUNT, 0 AS CESS, " & vbCrLf _
            & " 'Y' AS pre_gst_cdn, ACC.SUPP_CUST_NAME AS ACCOUNT_HEAD"


        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_SALE_DET ID, GEN_COMPANY_MST GMST,FIN_SUPP_CUST_MST ACM, FIN_SUPP_CUST_BUSINESS_MST CMST, GEN_STATE_MST SMST,FIN_SUPP_CUST_MST ACC " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACC.COMPANY_CODE " & vbCrLf _
            & " AND IH.ACCOUNTCODE=ACC.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND ACM.GST_REGD='Y' " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            & " AND IH.BOOKTYPE IN ('M','L')  " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO " & vbCrLf _
            & " AND IH.ISFINALPOST='Y' AND IH.CANCELLED='N' AND IH.GST_APP='Y' AND IS_ITEMDETAIL='Y'" & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO "

        ''AND IH.BOOKSUBTYPE='E'

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY CMST.GST_RGN_NO, NVL(IH.PARTY_DNCN_NO,IH.VNO), NVL(PARTY_DNCN_DATE,IH.VDATE), STATE_CODE ||'-'||SMST.NAME, IH.NETVALUE,(ID.CGST_PER+ID.SGST_PER+ID.IGST_PER),ACC.SUPP_CUST_NAME,IH.BOOKTYPE"

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf _
            & " CMST.GST_RGN_NO AS counter_party_gstin, " & vbCrLf _
            & " '' AS invoice_number, " & vbCrLf _
            & " '' AS invoice_date, " & vbCrLf _
            & " NVL(IH.PARTY_DNCN_NO,IH.VNO) AS note_number, " & vbCrLf _
            & " NVL(PARTY_DNCN_DATE,IH.VDATE) note_date, " & vbCrLf _
            & " DECODE(IH.BOOKTYPE,'L','C','D') AS note_type, " & vbCrLf _
            & " '09-OTHERS' AS reason, " & vbCrLf _
            & " STATE_CODE ||'-'||SMST.NAME  AS place_of_supply, " & vbCrLf _
            & " IH.NETVALUE AS invoice_value, " & vbCrLf _
            & " (IH.TOTCGST_PER+IH.TOTSGST_PER+IH.TOTCGST_PER) AS rate, " & vbCrLf _
            & " SUM(IH.TOTTAXABLEAMOUNT) AS tax_value,  " & vbCrLf _
            & " SUM(IH.TOTCGST_AMOUNT) AS CGST_AMOUNT, SUM(IH.TOTSGST_AMOUNT) AS SGST_AMOUNT, SUM(IH.TOTIGST_AMOUNT) AS IGST_AMOUNT,0 AS CESS, " & vbCrLf _
            & " 'Y' AS pre_gst_cdn, ACC.SUPP_CUST_NAME AS ACCOUNT_HEAD "


        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_SUPP_SALE_HDR IH,  GEN_COMPANY_MST GMST , FIN_SUPP_CUST_MST ACM,FIN_SUPP_CUST_BUSINESS_MST CMST, GEN_STATE_MST SMST, FIN_SUPP_CUST_MST ACC " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACC.COMPANY_CODE " & vbCrLf _
            & " AND IH.ACCOUNTCODE=ACC.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND ACM.GST_REGD='Y' " & vbCrLf _
            & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            & " AND IH.BOOKTYPE IN ('M','L') " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO " & vbCrLf _
            & " AND IH.ISFINALPOST='Y' AND IH.CANCELLED='N' AND IH.GST_APP='Y' AND IS_ITEMDETAIL='N'" & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO "

        ''AND IH.BOOKSUBTYPE<>'E'
        ''AND GOODS_SERVICE='S'  AND IH.BOOKSUBTYPE='E'

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY CMST.GST_RGN_NO,NVL(IH.PARTY_DNCN_NO,IH.VNO), NVL(PARTY_DNCN_DATE,IH.VDATE), STATE_CODE ||'-'||SMST.NAME, IH.NETVALUE, (IH.TOTCGST_PER+IH.TOTSGST_PER+IH.TOTCGST_PER),ACC.SUPP_CUST_NAME,IH.BOOKTYPE "

        '    pPubDBCnView.Execute SqlStr

        Show_VWGSTR1_DNCN_REG = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_VWGSTR1_DNCN_REG = False
        '    Resume
    End Function
    Private Function Show_VWGSTR1_DNCN_UNREG(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr

        ''    Reason For Issuing document Place Of Supply Note/Refund Voucher Value   Rate    Taxable Value   Cess Amount Pre GST


        SqlStr = " SELECT  " & vbCrLf _
            & " 'B2CL' AS Type_of_invoice,  " & vbCrLf _
            & " IH.BILLNO AS note_number,  " & vbCrLf _
            & " IH.INVOICE_DATE note_date,  " & vbCrLf _
            & " 'D' AS note_type,  " & vbCrLf _
            & " OIH.BILLNO AS invoice_number,  " & vbCrLf _
            & " OIH.INVOICE_DATE AS invoice_date,  " & vbCrLf _
            & " '07-Others' AS reason,  " & vbCrLf _
            & " STATE_CODE ||'-'||SMST.NAME  AS place_of_supply, " & vbCrLf _
            & " IH.NETVALUE AS invoice_value,  " & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS rate, " & vbCrLf _
            & " ID.GSTABLE_AMT AS tax_value,  " & vbCrLf _
            & " 0 AS CESS, " & vbCrLf _
            & " CASE WHEN OIH.INVOICE_DATE<='01-JUL-2017' THEN 'Y' ELSE 'N' END AS pre_gst_cdn "

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, DSP_DESPATCH_DET DD, FIN_INVOICE_HDR OIH, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST,  GEN_STATE_MST SMST  " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY  " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE  " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE  " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE  " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE  " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME  " & vbCrLf _
            & " AND IH.COMPANY_CODE=DD.COMPANY_CODE  " & vbCrLf _
            & " AND IH.AUTO_KEY_DESP=DD.AUTO_KEY_DESP  " & vbCrLf _
            & " AND ID.SUBROWNO=DD.SERIAL_NO  "


        SqlStr = SqlStr & vbCrLf _
            & " AND DD.COMPANY_CODE=OIH.COMPANY_CODE  " & vbCrLf _
            & " AND DD.REF_NO=OIH.AUTO_KEY_INVOICE  " & vbCrLf _
            & " AND IH.INVOICESEQTYPE IN (9)  " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND CMST.GST_REGD='N'  " & vbCrLf _
            & " AND CMST.WITHIN_COUNTRY='Y'  " & vbCrLf _
            & " AND IH.REF_DESP_TYPE='U'  " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "'  " ''& vbCrLf & " AND GMST.COMPANY_GST_RGN_NO<>NVL(CMST.GST_RGN_NO,'')  "

        SqlStr = SqlStr & vbCrLf & " UNION  "

        SqlStr = SqlStr & vbCrLf & " SELECT  " & vbCrLf _
            & " 'B2CL' AS Type_of_invoice,  " & vbCrLf _
            & " IH.BILLNO AS note_number,  " & vbCrLf _
            & " IH.INVOICE_DATE note_date,  " & vbCrLf _
            & " 'C' AS note_type,  " & vbCrLf _
            & " ID.CUST_REF_NO AS invoice_number,  " & vbCrLf _
            & " ID.CUST_REF_DATE AS invoice_date,  " & vbCrLf _
            & " '01-Sales Return' AS reason,  " & vbCrLf _
            & " STATE_CODE ||'-'||SMST.NAME  AS place_of_supply, " & vbCrLf _
            & " IH.NETVALUE AS invoice_value,  " & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS rate, " & vbCrLf _
            & " ID.GSTABLE_AMT AS tax_value,  " & vbCrLf _
            & " 0 AS CESS, " & vbCrLf _
            & " CASE WHEN ID.CUST_REF_DATE<='01-JUL-2017' THEN 'Y' ELSE 'N' END AS pre_gst_cdn "

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST,  GEN_STATE_MST SMST  " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY  " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE  " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE  "


        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE  " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE  " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME  " & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND CMST.GST_REGD='N'  " & vbCrLf _
            & " AND CMST.WITHIN_COUNTRY='Y'  " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "'  " & vbCrLf _
            & " AND IH.PURCHASESEQTYPE=2 AND IH.CANCELLED='N'" ''AND REJECTION='Y'  "


        SqlStr = SqlStr & vbCrLf _
            & " AND IH.ISFINALPOST='Y' AND IH.CANCELLED='N'  "

        '& vbCrLf _
        '    & " AND GMST.COMPANY_GST_RGN_NO<>NVL(CMST.GST_RGN_NO,'')  "

        SqlStr = SqlStr & vbCrLf _
            & " UNION  ALL"

        SqlStr = SqlStr & vbCrLf _
            & " SELECT  " & vbCrLf _
            & " 'B2CL' AS Type_of_invoice,  " & vbCrLf _
            & " IH.VNO AS note_number,  " & vbCrLf _
            & " IH.VDATE note_date,  " & vbCrLf _
            & " DECODE(IH.BOOKTYPE,'L','C','D') AS note_type,  " & vbCrLf _
            & " ID.BILL_NO AS invoice_number,  " & vbCrLf _
            & " ID.INVOICE_DATE AS invoice_date,  " & vbCrLf _
            & " '07-Others' AS reason,  " & vbCrLf _
            & " STATE_CODE ||'-'||SMST.NAME  AS place_of_supply, " & vbCrLf _
            & " IH.NETVALUE AS invoice_value,  " & vbCrLf _
            & " DECODE(IH.GST_APP,'Y',(ID.CGST_PER+ID.SGST_PER+ID.IGST_PER),0) AS rate, " & vbCrLf _
            & " ID.AMOUNT AS tax_value,  " & vbCrLf _
            & " 0 AS CESS, " & vbCrLf _
            & " CASE WHEN ID.INVOICE_DATE<='01-JUL-2017' THEN 'Y' ELSE 'N' END AS pre_gst_cdn "


        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_SALE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST  " & vbCrLf & " WHERE IH.MKEY=ID.MKEY  " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE  " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE  " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE  " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE  " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME  " & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND CMST.GST_REGD='N'  " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "'  " & vbCrLf _
            & " AND IH.BOOKTYPE IN ('L','M') AND  IS_ITEMDETAIL='Y' "

        '& vbCrLf _
        '    & " AND GMST.COMPANY_GST_RGN_NO<>NVL(CMST.GST_RGN_NO,'')  "

        ''AND GOODS_SERVICE='G' IH.BOOKSUBTYPE='E' AND

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.ISFINALPOST='Y' AND IH.CANCELLED='N' AND IS_ITEMDETAIL='Y' "

        SqlStr = SqlStr & vbCrLf _
            & " UNION  ALL"

        SqlStr = SqlStr & vbCrLf _
            & " SELECT  " & vbCrLf _
            & " 'B2CL' AS Type_of_invoice,  " & vbCrLf _
            & " IH.VNO AS note_number,  " & vbCrLf _
            & " IH.VDATE note_date,  " & vbCrLf _
            & " DECODE(IH.BOOKTYPE,'L','C','D') AS note_type,  " & vbCrLf _
            & " IH.O_BILLNO AS invoice_number,  " & vbCrLf _
            & " IH.O_INVOICE_DATE AS invoice_date,  " & vbCrLf _
            & " '07-Others' AS reason,  " & vbCrLf _
            & " STATE_CODE ||'-'||SMST.NAME  AS place_of_supply, " & vbCrLf _
            & " IH.NETVALUE AS invoice_value,  " & vbCrLf _
            & " DECODE(IH.GST_APP,'Y',(IH.TOTCGST_PER+IH.TOTSGST_PER+IH.TOTCGST_PER),0) AS rate, " & vbCrLf _
            & " IH.TOTTAXABLEAMOUNT AS tax_value,  " & vbCrLf _
            & " 0 AS CESS, " & vbCrLf _
            & " CASE WHEN IH.O_INVOICE_DATE<='01-JUL-2017' THEN 'Y' ELSE 'N' END AS pre_gst_cdn "


        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_SUPP_SALE_HDR IH, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST  " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE  " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE  " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE  " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE  " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME  " & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND CMST.GST_REGD='N'  " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "'  " & vbCrLf _
            & " AND IH.BOOKTYPE IN ('L','M')  "

        '& vbCrLf _
        '    & " AND GMST.COMPANY_GST_RGN_NO<>NVL(CMST.GST_RGN_NO,'')  "

        ''GOODS_SERVICE='S' AND IH.BOOKSUBTYPE='E'  AND IS_ITEMDETAIL='N'

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.ISFINALPOST='Y' AND IH.CANCELLED='N'  AND IS_ITEMDETAIL='N'"

        Show_VWGSTR1_DNCN_UNREG = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_VWGSTR1_DNCN_UNREG = False
        '    Resume
    End Function
    Private Function Show_VWGSTR1_EXPORT(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr


        SqlStr = " SELECT  DISTINCT" & vbCrLf _
            & " DECODE(ID.CGST_PER + ID.SGST_PER + ID.IGST_PER,0,'WOPAY','WPAY') AS Export_type,  " & vbCrLf _
            & " IH.BILLNO AS invoice_number,  " & vbCrLf _
            & " IH.INVOICE_DATE invoice_date,  " & vbCrLf _
            & " IH.NETVALUE AS invoice_value,  " & vbCrLf _
            & " IH.PORT_CODE AS PORT_CODE,  " & vbCrLf _
            & " IH.SHIPPING_NO AS SHIPPING_BILL_NUM,  " & vbCrLf _
            & " IH.SHIPPING_DATE AS SHIPPING_BILL_DATE,  " & vbCrLf _
            & " (ID.CGST_PER + ID.SGST_PER + ID.IGST_PER) AS rate,  " & vbCrLf _
            & " ID.GSTABLE_AMT AS tax_value   "

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST CSMST  " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY  " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE  " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE  "

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=CSMST.COMPANY_CODE  " & vbCrLf _
            & " AND IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE  " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE  " & vbCrLf _
            & " AND (IH.INVOICESEQTYPE IN (6) OR IH.IS_LUT='Y')" & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND DECODE(IH.IS_LUT,'Y','N',CMST.WITHIN_COUNTRY)='N'  " & vbCrLf _
            & " AND IH.REF_DESP_TYPE<>'U'  " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "'  "

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.BILLNO"
        Show_VWGSTR1_EXPORT = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_VWGSTR1_EXPORT = False
        '    Resume
    End Function
    Private Function Show_VWGSTR1_ADVANCE(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr


        SqlStr = " SELECT " & vbCrLf & " STATE_CODE ||'-'||SMST.NAME  AS place_of_supply, " & vbCrLf & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS rate, " & vbCrLf & " SUM(IH.NETVALUE) As advance_receipt_value, " & vbCrLf & " 0 AS cess_amount "


        SqlStr = SqlStr & vbCrLf & " FROM FIN_ADVANCE_HDR IH, FIN_ADVANCE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST " & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf & " AND IH.BOOKTYPE='AR' " & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO "

        SqlStr = SqlStr & vbCrLf & " GROUP BY STATE_CODE ||'-'||SMST.NAME, (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER)"

        Show_VWGSTR1_ADVANCE = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_VWGSTR1_ADVANCE = False
        '    Resume
    End Function

    Private Function Show_VWGSTR1_NILRATE(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr


        SqlStr = " "

        SqlStr = " SELECT  " & vbCrLf _
            & " CASE WHEN WITHIN_STATE='N' AND GST_REGD='Y' THEN 'Inter-State supplies to registered persons'" & vbCrLf _
            & " WHEN  WITHIN_STATE='Y' AND GST_REGD='Y' THEN 'Intra-State supplies to registered persons' " & vbCrLf _
            & " WHEN  WITHIN_STATE='N' AND GST_REGD='N' THEN 'Inter-State supplies to unregistered persons' " & vbCrLf _
            & " WHEN  WITHIN_STATE='Y' AND GST_REGD='N' THEN 'Intra-State supplies to unregistered persons' END AS SUPPLY_TYPE, " & vbCrLf _
            & " SUM(IH.NETVALUE) AS NETVALUE, 0 , 0 "

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST  " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE  " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE  "

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE  " & vbCrLf _
            & " AND IH.INVOICESEQTYPE IN (0,4)  " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' AND (NET_CGST_PER+NET_SGST_PER+NET_IGST_PER) =0 "

        SqlStr = SqlStr & vbCrLf & " GROUP BY WITHIN_STATE, GST_REGD"
        Show_VWGSTR1_NILRATE = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_VWGSTR1_NILRATE = False
        '    Resume
    End Function

    Private Function Show_VWGSTR1_TAXPAID(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr

        'Place Of Supply Rate    Gross Advance Adjusted  Cess Amount

        SqlStr = " SELECT " & vbCrLf & " STATE_CODE ||'-'||SMST.NAME  AS place_of_supply, " & vbCrLf & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS rate, " & vbCrLf & " SUM(IH.ADV_ADJUSTED_AMT) As advance_receipt_value, " & vbCrLf & " 0 AS cess_amount "


        SqlStr = SqlStr & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST,  GEN_STATE_MST SMST " & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' AND ADV_ADJUSTED_AMT>0" & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO "

        SqlStr = SqlStr & vbCrLf & " GROUP BY STATE_CODE ||'-'||SMST.NAME, (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER)"

        Show_VWGSTR1_TAXPAID = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_VWGSTR1_TAXPAID = False
        '    Resume
    End Function


    Private Function Show_VWGSTR1_DocIssued(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr


        SqlStr = " "


        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " 'Invoice for outward supply'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf _
            & " TRIM(MIN(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_from, " & vbCrLf _
            & " TRIM(MAX(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_to, " & vbCrLf _
            & " COUNT(BILLNO) AS total_number, " & vbCrLf & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND IH.INVOICESEQTYPE IN (1) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME, BILLNOPREFIX HAVING COUNT(BILLNO)>0 "

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " 'Invoice for outward supply'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf _
            & " TRIM(MIN(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_from, " & vbCrLf & " TRIM(MAX(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_to, " & vbCrLf & " COUNT(BILLNO) AS total_number, " & vbCrLf & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND IH.INVOICESEQTYPE IN (2) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            & " GROUP BY GMST.COMPANY_CODE, BILLNOPREFIX, GMST.COMPANY_SHORTNAME, BILLNOPREFIX HAVING COUNT(BILLNO)>0 "

        SqlStr = SqlStr & vbCrLf & " UNION ALL "

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " 'Invoice for outward supply'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf _
            & " TRIM(MIN(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_from, " & vbCrLf _
            & " TRIM(MAX(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_to, " & vbCrLf _
            & " COUNT(BILLNO) AS total_number, " & vbCrLf & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND IH.INVOICESEQTYPE IN (4) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME, BILLNOPREFIX HAVING COUNT(BILLNO)>0 "

        SqlStr = SqlStr & vbCrLf & " UNION ALL "

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " 'Invoice for outward supply'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf & " TRIM(MIN(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_from, " & vbCrLf & " TRIM(MAX(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_to, " & vbCrLf & " COUNT(BILLNO) AS total_number, " & vbCrLf & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND IH.INVOICESEQTYPE IN (6) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME, BILLNOPREFIX HAVING COUNT(BILLNO)>0"

        SqlStr = SqlStr & vbCrLf & " UNION ALL "

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " 'Invoice for inward supply from unregistered person'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf & " TRIM(MIN(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_from, " & vbCrLf & " TRIM(MAX(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_to, " & vbCrLf & " COUNT(BILLNO) AS total_number, " & vbCrLf & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND IH.INVOICESEQTYPE IN (7) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME, BILLNOPREFIX HAVING COUNT(BILLNO)>0 "

        SqlStr = SqlStr & vbCrLf & " UNION ALL "



        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " 'Invoice for inward supply from unregistered person'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf & " TRIM(MIN(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_from, " & vbCrLf & " TRIM(MAX(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_to, " & vbCrLf & " COUNT(BILLNO) AS total_number, " & vbCrLf & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND IH.INVOICESEQTYPE IN (8) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME, BILLNOPREFIX HAVING COUNT(BILLNO)>0 "

        SqlStr = SqlStr & vbCrLf & " UNION ALL "


        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " 'Debit Note'   || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf & " TRIM(MIN(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_from, " & vbCrLf & " TRIM(MAX(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_to, " & vbCrLf & " COUNT(BILLNO) AS total_number, " & vbCrLf & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND IH.INVOICESEQTYPE IN (9) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME, BILLNOPREFIX HAVING COUNT(BILLNO)>0"

        SqlStr = SqlStr & vbCrLf & " UNION ALL "

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " 'Credit Note'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf & " MIN(REJ_CREDITNOTE) AS bill_from, " & vbCrLf & " MAX(REJ_CREDITNOTE) AS bill_to, " & vbCrLf & " COUNT(REJ_CREDITNOTE) AS total_number, " & vbCrLf & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf & " FROM FIN_PURCHASE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND IH.PURCHASESEQTYPE=2 AND IH.CANCELLED='N' AND ISFINALPOST='Y'" & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME HAVING COUNT(REJ_CREDITNOTE)>0"

        SqlStr = SqlStr & vbCrLf & " UNION ALL "

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 103 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then

        SqlStr = SqlStr & vbCrLf _
                  & " SELECT " & vbCrLf _
                  & " 'Credit Note'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf _
                  & " MIN(PARTY_DNCN_NO) AS bill_from, " & vbCrLf _
                  & " MAX(PARTY_DNCN_NO) AS bill_to, " & vbCrLf _
                  & " COUNT(PARTY_DNCN_NO) AS total_number, " & vbCrLf _
                  & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf _
                  & " FROM FIN_SUPP_SALE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf _
                  & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
                  & " AND IH.BOOKTYPE IN ('L') AND IH.BOOKSUBTYPE='E' AND ISFINALPOST='Y' AND GST_APP='Y'" & vbCrLf _
                  & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                  & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                  & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
                  & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME HAVING COUNT(PARTY_DNCN_NO)>0"

            '' 

            SqlStr = SqlStr & vbCrLf & " UNION ALL "

            SqlStr = SqlStr & vbCrLf _
                & " SELECT " & vbCrLf _
                & " 'Credit Note'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf _
                & " MIN(PARTY_DNCN_NO) AS bill_from, " & vbCrLf _
                & " MAX(PARTY_DNCN_NO) AS bill_to, " & vbCrLf _
                & " COUNT(PARTY_DNCN_NO) AS total_number, " & vbCrLf _
                & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf _
                & " FROM FIN_SUPP_SALE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf _
                & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
                & " AND IH.BOOKTYPE IN ('L') AND IH.BOOKSUBTYPE='M' AND ISFINALPOST='Y' AND GST_APP='Y'" & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
                & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME HAVING COUNT(PARTY_DNCN_NO)>0"

            SqlStr = SqlStr & vbCrLf & " UNION ALL "

            SqlStr = SqlStr & vbCrLf _
                 & " SELECT " & vbCrLf _
                 & " 'Debit Note'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf _
                 & " MIN(PARTY_DNCN_NO) AS bill_from, " & vbCrLf _
                 & " MAX(PARTY_DNCN_NO) AS bill_to, " & vbCrLf _
                 & " COUNT(PARTY_DNCN_NO) AS total_number, " & vbCrLf _
                 & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf _
                 & " FROM FIN_SUPP_SALE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf _
                 & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
                 & " AND IH.BOOKTYPE='M' AND IH.BOOKSUBTYPE='E' AND ISFINALPOST='Y' AND GST_APP='Y'" & vbCrLf _
                 & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                 & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                 & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
                 & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME HAVING COUNT(PARTY_DNCN_NO)>0"

            SqlStr = SqlStr & vbCrLf & " UNION ALL "

            SqlStr = SqlStr & vbCrLf _
                & " SELECT " & vbCrLf _
                & " 'Debit Note'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf _
                & " MIN(PARTY_DNCN_NO) AS bill_from, " & vbCrLf _
                & " MAX(PARTY_DNCN_NO) AS bill_to, " & vbCrLf _
                & " COUNT(PARTY_DNCN_NO) AS total_number, " & vbCrLf _
                & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf _
                & " FROM FIN_SUPP_SALE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf _
                & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
                & " AND IH.BOOKTYPE='M' AND IH.BOOKSUBTYPE='M' AND ISFINALPOST='Y' AND GST_APP='Y'" & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
                & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME HAVING COUNT(PARTY_DNCN_NO)>0"

            'Else

            '    SqlStr = SqlStr & vbCrLf _
            '       & " SELECT " & vbCrLf _
            '       & " 'Credit Note'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf _
            '       & " MIN(VNO) AS bill_from, " & vbCrLf _
            '       & " MAX(VNO) AS bill_to, " & vbCrLf _
            '       & " COUNT(VNO) AS total_number, " & vbCrLf _
            '       & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf _
            '       & " FROM FIN_SUPP_SALE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf _
            '       & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            '       & " AND IH.BOOKTYPE='L' AND IH.BOOKSUBTYPE='E' AND ISFINALPOST='Y'  AND GST_APP='Y'" & vbCrLf _
            '       & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            '       & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            '       & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            '       & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME HAVING COUNT(VNO)>0"

            '    SqlStr = SqlStr & vbCrLf & " UNION ALL "

            '    SqlStr = SqlStr & vbCrLf _
            '       & " SELECT " & vbCrLf _
            '       & " 'Debit Note'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf _
            '       & " MIN(VNO) AS bill_from, " & vbCrLf _
            '       & " MAX(VNO) AS bill_to, " & vbCrLf _
            '       & " COUNT(VNO) AS total_number, " & vbCrLf _
            '       & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf _
            '       & " FROM FIN_SUPP_SALE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf _
            '       & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            '       & " AND IH.BOOKTYPE='M' AND IH.BOOKSUBTYPE='E' AND ISFINALPOST='Y'  AND GST_APP='Y'" & vbCrLf _
            '       & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            '       & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            '       & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            '       & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME HAVING COUNT(VNO)>0"
            'End If


            SqlStr = SqlStr & vbCrLf & " UNION ALL "

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " 'Receipt Voucher'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name , " & vbCrLf & " MIN(VNO) AS bill_from, " & vbCrLf & " MAX(VNO) AS bill_to, " & vbCrLf & " COUNT(VNO) AS total_number, " & vbCrLf & " 0 AS cancel " & vbCrLf & " FROM FIN_ADVANCE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND IH.BOOKTYPE='AR' " & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME HAVING COUNT(VNO)>0 "



        SqlStr = SqlStr & vbCrLf & " UNION ALL "

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " 'Payment Voucher'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name , " & vbCrLf & " MIN(VNO) AS bill_from, " & vbCrLf & " MAX(VNO) AS bill_to, " & vbCrLf & " COUNT(VNO) AS total_number, " & vbCrLf & " 0 AS cancel " & vbCrLf & " FROM FIN_ADVANCE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND IH.BOOKTYPE='AP' " & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME HAVING COUNT(VNO)>0 "

        SqlStr = SqlStr & vbCrLf & " UNION ALL "

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf _
            & " 'Delivery Challan for job work'  || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name , " & vbCrLf _
            & " TO_CHAR(CHALLAN_PREFIX||MIN(GATEPASS_NO)) AS bill_from, " & vbCrLf _
            & " TO_CHAR(CHALLAN_PREFIX||MAX(GATEPASS_NO)) AS bill_to, " & vbCrLf _
            & " COUNT(GATEPASS_NO) AS total_number, " & vbCrLf _
            & " 0 AS cancel " & vbCrLf _
            & " FROM INV_GATEPASS_HDR IH, FIN_SUPP_CUST_BUSINESS_MST BMST, GEN_COMPANY_MST GMST " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=BMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.GATEPASS_TYPE='R' AND GMST.COMPANY_GST_RGN_NO<>NVL(BMST.GST_RGN_NO,'')" & vbCrLf _
            & " AND IH.GATEPASS_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.GATEPASS_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME,CHALLAN_PREFIX HAVING COUNT(GATEPASS_NO)>0"

        SqlStr = SqlStr & vbCrLf & " UNION ALL"


        SqlStr = SqlStr & vbCrLf _
            & " SELECT " & vbCrLf _
            & " 'Bill of Supply'   || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf _
            & " TRIM(MIN(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_from, " & vbCrLf _
            & " TRIM(MAX(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_to, " & vbCrLf _
            & " COUNT(BILLNO) AS total_number, " & vbCrLf _
            & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.INVOICESEQTYPE IN (0) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME, BILLNOPREFIX HAVING COUNT(BILLNO)>0 "

        'SqlStr = SqlStr & vbCrLf & " UNION ALL"


        'SqlStr = SqlStr & vbCrLf _
        '    & " SELECT " & vbCrLf _
        '    & " 'Delivery Challan for supply on approval'   || ' - ' || GMST.COMPANY_SHORTNAME AS doc_name, " & vbCrLf _
        '    & " TRIM(MIN(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_from, " & vbCrLf _
        '    & " TRIM(MAX(CASE WHEN REGEXP_LIKE(BILLNO,'^([0-9]+)$') THEN LPAD(BILLNO,20) ELSE BILLNO END)) AS bill_to, " & vbCrLf _
        '    & " COUNT(BILLNO) AS total_number, " & vbCrLf _
        '    & " SUM(DECODE(CANCELLED,'Y',1,0)) AS cancel " & vbCrLf _
        '    & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST " & vbCrLf _
        '    & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
        '    & " AND IH.INVOICESEQTYPE IN (3) " & vbCrLf _
        '    & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
        '    & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
        '    & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
        '    & " GROUP BY GMST.COMPANY_CODE, GMST.COMPANY_SHORTNAME, BILLNOPREFIX HAVING COUNT(BILLNO)>0 "

        Show_VWGSTR1_DocIssued = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_VWGSTR1_DocIssued = False
        '    Resume
    End Function

    Private Function Show_VWGSTR1_HSN(ByRef SqlStr As String, ByRef pPubDBCnView As ADODB.Connection, ByRef pCompanyCode As String, ByRef pDateFrom As String, ByRef pDateTo As String) As Boolean
        On Error GoTo CreateErr

        '(ID.CHARGEABLEGLASS_AREA * ID.ITEM_QTY)

        SqlStr = " SELECT hsn_sc, " & vbCrLf _
            & " description,  " & vbCrLf _
            & " unit_of_measurement,  " & vbCrLf _
            & " Tax_Rate,  " & vbCrLf _
            & " SUM(quantity) AS quantity, " & vbCrLf _
            & " SUM(SQM_QTY) AS SQM_QTY, " & vbCrLf _
            & " SUM(invoice_value) AS invoice_value,  " & vbCrLf _
            & " SUM(taxable_value) AS taxable_value, " & vbCrLf _
            & " SUM(igst_amount) AS igst_amount,  " & vbCrLf _
            & " SUM(cgst_amount) AS cgst_amount,  " & vbCrLf _
            & " SUM(sgst_amount) AS sgst_amount,  " & vbCrLf _
            & " SUM(cess_amount) AS cess_amount FROM (  "

        SqlStr = SqlStr & vbCrLf _
            & " SELECT ID.HSNCODE AS hsn_sc, '' AS description, '' AS unit_of_measurement," & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS Tax_Rate, " & vbCrLf _
            & " SUM(ID.ITEM_QTY) AS quantity, SUM(DECODE(ID.CHARGEABLEGLASS_AREA,0,1,ID.CHARGEABLEGLASS_AREA) * ID.ITEM_QTY) AS SQM_QTY, SUM(ID.GSTABLE_AMT+ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT) AS invoice_value," & vbCrLf _
            & " SUM(ID.GSTABLE_AMT) AS taxable_value, " & vbCrLf _
            & " SUM(ID.IGST_AMOUNT) AS igst_amount, " & vbCrLf _
            & " SUM(ID.CGST_AMOUNT) AS cgst_amount, " & vbCrLf _
            & " SUM(ID.SGST_AMOUNT) AS sgst_amount, " & vbCrLf _
            & " 0 As cess_amount"


        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_BUSINESS_MST CMST, FIN_SUPP_CUST_BUSINESS_MST CSMST, FIN_SUPP_CUST_MST ACM,GEN_STATE_MST SMST " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CSMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.SHIP_TO_LOC_ID=CSMST.LOCATION_ID    " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf _
            & " AND IH.INVOICESEQTYPE IN (1,2) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND ACM.GST_REGD='Y' " & vbCrLf _
            & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf _
            & " AND IH.REF_DESP_TYPE<>'U' " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' AND IH.CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " GROUP BY ID.HSNCODE, (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) "


        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf _
            & " SELECT SAC_CODE AS hsn_sc,'' AS description, '' AS unit_of_measurement," & vbCrLf _
            & " (NET_CGST_PER+NET_SGST_PER+NET_IGST_PER) AS Tax_Rate," & vbCrLf _
            & " SUM(IH.TOTQTY) AS quantity,  SUM(IH.TOTQTY) AS SQM_QTY, SUM(IH.TOTTAXABLEAMOUNT+IH.NETCGST_AMOUNT+IH.NETSGST_AMOUNT+IH.NETIGST_AMOUNT) AS invoice_value," & vbCrLf _
            & " SUM(IH.TOTTAXABLEAMOUNT) AS taxable_value, " & vbCrLf _
            & " SUM(IH.NETIGST_AMOUNT) AS igst_amount, " & vbCrLf _
            & " SUM(IH.NETCGST_AMOUNT) AS cgst_amount, " & vbCrLf _
            & " SUM(IH.NETSGST_AMOUNT) AS sgst_amount, " & vbCrLf _
            & " 0 As cess_amount"


        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_BUSINESS_MST CMST, FIN_SUPP_CUST_MST ACM, GEN_STATE_MST SMST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID " & vbCrLf _
            & " AND IH.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf _
            & " AND IH.INVOICESEQTYPE IN (4) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND ACM.GST_REGD='Y' " & vbCrLf _
            & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' AND IH.CANCELLED='N'" & vbCrLf _
            & " GROUP BY SAC_CODE, (NET_CGST_PER+NET_SGST_PER+NET_IGST_PER) "


        'SqlStr = SqlStr & vbCrLf & " UNION ALL"

        'SqlStr = SqlStr & vbCrLf _
        '    & " SELECT ID.HSNCODE AS hsn_sc, '' AS description, '' AS unit_of_measurement," & vbCrLf _
        '    & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS Tax_Rate, " & vbCrLf _
        '    & " SUM(ID.ITEM_QTY) AS quantity, SUM(ID.GSTABLE_AMT+ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT) AS invoice_value," & vbCrLf _
        '    & " SUM(ID.GSTABLE_AMT) AS taxable_value, " & vbCrLf _
        '    & " SUM(ID.IGST_AMOUNT) AS igst_amount, " & vbCrLf _
        '    & " SUM(ID.CGST_AMOUNT) AS cgst_amount, " & vbCrLf _
        '    & " SUM(ID.SGST_AMOUNT) AS sgst_amount, " & vbCrLf _
        '    & " 0 As cess_amount"

        'SqlStr = SqlStr & vbCrLf _
        '    & " FROM FIN_INVOICE_HDR IH, " & vbCrLf _
        '    & " FIN_INVOICE_DET ID, GEN_COMPANY_MST GMST, " & vbCrLf _
        '    & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST CSMST, " & vbCrLf _
        '    & " GEN_STATE_MST SMST "

        'SqlStr = SqlStr & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=CSMST.COMPANY_CODE " & vbCrLf & " AND IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf & " AND IH.INVOICESEQTYPE IN (1,2) " & vbCrLf _
        '    & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
        '    & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
        '    & " AND CMST.GST_REGD='N' " & vbCrLf _
        '    & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf _
        '    & " AND CMST.WITHIN_STATE='N' " & vbCrLf _
        '    & " AND IH.REF_DESP_TYPE<>'U' " & vbCrLf _
        '    & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
        '    & " AND IH.NETVALUE>250000 "

        'SqlStr = SqlStr & vbCrLf & " GROUP BY ID.HSNCODE, (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) "


        'SqlStr = SqlStr & vbCrLf & " UNION ALL " & vbCrLf _
        '    & " SELECT SAC_CODE AS hsn_sc,'' AS description, '' AS unit_of_measurement," & vbCrLf _
        '    & " (NET_CGST_PER+NET_SGST_PER+NET_IGST_PER) AS Tax_Rate," & vbCrLf _
        '    & " SUM(IH.TOTQTY) AS quantity, SUM(IH.TOTTAXABLEAMOUNT+IH.NETCGST_AMOUNT+IH.NETSGST_AMOUNT+IH.NETIGST_AMOUNT) AS invoice_value," & vbCrLf _
        '    & " SUM(IH.TOTTAXABLEAMOUNT) AS taxable_value, " & vbCrLf _
        '    & " SUM(IH.NETIGST_AMOUNT) AS igst_amount, " & vbCrLf _
        '    & " SUM(IH.NETCGST_AMOUNT) AS cgst_amount, " & vbCrLf _
        '    & " SUM(IH.NETSGST_AMOUNT) AS sgst_amount, " & vbCrLf _
        '    & " 0 As cess_amount"

        'SqlStr = SqlStr & vbCrLf _
        '    & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST "


        'SqlStr = SqlStr & vbCrLf & " WHERE  " & vbCrLf & " IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf & " AND IH.INVOICESEQTYPE IN (4) " & vbCrLf _
        '    & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
        '    & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
        '    & " AND CMST.GST_REGD='N' " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf _
        '    & " AND CMST.WITHIN_STATE='N' " & vbCrLf _
        '    & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
        '    & " AND IH.NETVALUE>250000 " & vbCrLf _
        '    & " GROUP BY SAC_CODE, (NET_CGST_PER+NET_SGST_PER+NET_IGST_PER) "



        SqlStr = SqlStr & vbCrLf & " UNION ALL " & vbCrLf _
            & " SELECT ID.HSNCODE AS hsn_sc, '' AS description, '' AS unit_of_measurement," & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS Tax_Rate, " & vbCrLf _
            & " SUM(ID.ITEM_QTY) AS quantity,  SUM(DECODE(ID.CHARGEABLEGLASS_AREA,0,1,ID.CHARGEABLEGLASS_AREA) * ID.ITEM_QTY) AS SQM_QTY, SUM(ID.GSTABLE_AMT+ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT) AS invoice_value," & vbCrLf _
            & " SUM(ID.GSTABLE_AMT) AS taxable_value, " & vbCrLf _
            & " SUM(ID.IGST_AMOUNT) AS igst_amount, " & vbCrLf _
            & " SUM(ID.CGST_AMOUNT) AS cgst_amount, " & vbCrLf _
            & " SUM(ID.SGST_AMOUNT) AS sgst_amount, " & vbCrLf _
            & " 0 As cess_amount"

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST CSMST, GEN_STATE_MST SMST " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE "


        SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=CSMST.COMPANY_CODE " & vbCrLf & " AND IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf & " AND IH.INVOICESEQTYPE IN (1,2) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND CMST.GST_REGD='N' " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf & " AND IH.REF_DESP_TYPE<>'U' " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " AND IH.NETVALUE<DECODE(CMST.WITHIN_STATE,'N',250000,500000000) "

        SqlStr = SqlStr & vbCrLf & " GROUP BY ID.HSNCODE, (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) "


        SqlStr = SqlStr & vbCrLf _
            & " UNION ALL " & vbCrLf _
            & " SELECT SAC_CODE AS hsn_sc,'' AS description, '' AS unit_of_measurement," & vbCrLf _
            & " (NET_CGST_PER+NET_SGST_PER+NET_IGST_PER) AS Tax_Rate," & vbCrLf _
            & " SUM(IH.TOTQTY) AS quantity,  SUM(IH.TOTQTY) AS SQM_QTY, SUM(IH.TOTTAXABLEAMOUNT+IH.NETCGST_AMOUNT+IH.NETSGST_AMOUNT+IH.NETIGST_AMOUNT) AS invoice_value," & vbCrLf _
            & " SUM(IH.TOTTAXABLEAMOUNT) AS taxable_value, " & vbCrLf _
            & " SUM(IH.NETIGST_AMOUNT) AS igst_amount, " & vbCrLf _
            & " SUM(IH.NETCGST_AMOUNT) AS cgst_amount, " & vbCrLf _
            & " SUM(IH.NETSGST_AMOUNT) AS sgst_amount, " & vbCrLf _
            & " 0 As cess_amount"

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST " & vbCrLf _
            & " WHERE  " & vbCrLf _
            & " IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE "

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf & " AND IH.INVOICESEQTYPE IN (4) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND CMST.GST_REGD='N' " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            & " AND IH.NETVALUE<DECODE(CMST.WITHIN_STATE,'N',250000,500000000) " & vbCrLf _
            & " GROUP BY SAC_CODE, (NET_CGST_PER+NET_SGST_PER+NET_IGST_PER) "


        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf _
            & " SELECT ID.HSNCODE AS hsn_sc, '' AS description, '' AS unit_of_measurement," & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS Tax_Rate, " & vbCrLf _
            & " SUM(ID.ITEM_QTY) AS quantity, SUM(DECODE(ID.CHARGEABLEGLASS_AREA,0,1,ID.CHARGEABLEGLASS_AREA) * ID.ITEM_QTY) AS SQM_QTY, SUM(ID.GSTABLE_AMT+ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT) AS invoice_value," & vbCrLf _
            & " SUM(ID.GSTABLE_AMT) AS taxable_value, " & vbCrLf _
            & " SUM(ID.IGST_AMOUNT) AS igst_amount, " & vbCrLf _
            & " SUM(ID.CGST_AMOUNT) AS cgst_amount, " & vbCrLf _
            & " SUM(ID.SGST_AMOUNT) AS sgst_amount, " & vbCrLf _
            & " 0 As cess_amount"

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST CSMST, GEN_STATE_MST SMST " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CSMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.SUPP_CUST_STATE=SMST.NAME "


        SqlStr = SqlStr & vbCrLf _
            & " AND IH.INVOICESEQTYPE IN (9) " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND CMST.GST_REGD IN ('N','Y') " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf & " AND IH.REF_DESP_TYPE='U' " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO "

        SqlStr = SqlStr & vbCrLf & " GROUP BY ID.HSNCODE, (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) "

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf _
            & " SELECT ID.HSNCODE AS hsn_sc, '' AS description, '' AS unit_of_measurement," & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS Tax_Rate, " & vbCrLf _
            & " SUM(ID.ITEM_QTY)*-1 AS quantity, SUM(ID.ITEM_QTY)*-1 AS SQM_QTY, SUM(ID.GSTABLE_AMT+ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT)*-1 AS invoice_value," & vbCrLf _
            & " SUM(ID.GSTABLE_AMT)*-1 AS taxable_value, " & vbCrLf _
            & " SUM(ID.IGST_AMOUNT)*-1 AS igst_amount, " & vbCrLf _
            & " SUM(ID.CGST_AMOUNT)*-1 AS cgst_amount, " & vbCrLf _
            & " SUM(ID.SGST_AMOUNT)*-1 AS sgst_amount, " & vbCrLf _
            & " 0 As cess_amount"



        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST CSMST, GEN_STATE_MST SMST " & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=CSMST.COMPANY_CODE "

        SqlStr = SqlStr & vbCrLf & " AND IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf _
            & " AND IH.INVOICE_DATE>='01-Jul-2017' AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND CMST.GST_REGD IN ('N','Y') " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf & " AND IH.PURCHASESEQTYPE=2 AND IH.CANCELLED='N'" & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO "

        SqlStr = SqlStr & vbCrLf & " GROUP BY ID.HSNCODE, (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) "

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        ''  'SUM(ID.CHARGEABLEGLASS_AREA * ID.ITEM_QTY) AS SQM_QTY,
        'SUM(ID.CHARGEABLE_HEIGHT * ID.CHARGEABLE_WIDTH * ID.ITEM_QTY * 0.000001) AS SQM_QTY,

        SqlStr = SqlStr & vbCrLf _
            & " SELECT ID.HSNCODE AS hsn_sc, '' AS description, '' AS unit_of_measurement," & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS Tax_Rate, " & vbCrLf _
            & " SUM(ID.QTY * DECODE(REASON,2,1,0))* DECODE(IH.BOOKTYPE,'L',-1,1) AS quantity, SUM(DECODE(ID.CHARGEABLE_HEIGHT,0,1,ID.CHARGEABLE_HEIGHT) * DECODE(ID.CHARGEABLE_WIDTH,0,1,ID.CHARGEABLE_WIDTH) * ID.QTY * 0.000001) * DECODE(IH.BOOKTYPE,'L',-1,1) AS SQM_QTY, SUM(ID.AMOUNT+ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT)* DECODE(IH.BOOKTYPE,'L',-1,1) AS invoice_value," & vbCrLf _
            & " SUM(ID.AMOUNT)*DECODE(IH.BOOKTYPE,'L',-1,1) AS taxable_value, " & vbCrLf _
            & " SUM(ID.IGST_AMOUNT)*DECODE(IH.BOOKTYPE,'L',-1,1) AS igst_amount, " & vbCrLf _
            & " SUM(ID.CGST_AMOUNT)*DECODE(IH.BOOKTYPE,'L',-1,1) AS cgst_amount, " & vbCrLf _
            & " SUM(ID.SGST_AMOUNT)*DECODE(IH.BOOKTYPE,'L',-1,1) AS sgst_amount, " & vbCrLf _
            & " 0 As cess_amount"


        SqlStr = SqlStr & vbCrLf & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_SALE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST " & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND CMST.GST_REGD IN ('N','Y') " & vbCrLf & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            & " AND IH.BOOKTYPE IN ('L','M') AND IH.BOOKSUBTYPE='E' " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO " & vbCrLf _
            & " AND IH.ISFINALPOST='Y' AND IH.CANCELLED='N' AND IH.GST_APP='Y' AND IS_ITEMDETAIL='Y'" & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO "

        SqlStr = SqlStr & vbCrLf & " GROUP BY ID.HSNCODE, (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER),IH.BOOKTYPE "

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf _
            & " SELECT SAC_CODE AS hsn_sc,'' AS description, '' AS unit_of_measurement," & vbCrLf _
            & " (TOTCGST_PER+TOTSGST_PER+TOTIGST_PER) AS Tax_Rate," & vbCrLf _
            & " SUM(IH.TOTQTY)*DECODE(IH.BOOKTYPE,'L',-1,1) AS quantity, SUM(IH.TOTQTY)*DECODE(IH.BOOKTYPE,'L',-1,1) AS SQM_QTY, SUM(IH.TOTTAXABLEAMOUNT+IH.TOTCGST_AMOUNT+IH.TOTSGST_AMOUNT+IH.TOTIGST_AMOUNT)*DECODE(IH.BOOKTYPE,'L',-1,1) AS invoice_value," & vbCrLf _
            & " SUM(IH.TOTTAXABLEAMOUNT)*DECODE(IH.BOOKTYPE,'L',-1,1) AS taxable_value, " & vbCrLf _
            & " SUM(IH.TOTIGST_AMOUNT)*DECODE(IH.BOOKTYPE,'L',-1,1) AS igst_amount, " & vbCrLf _
            & " SUM(IH.TOTCGST_AMOUNT)*DECODE(IH.BOOKTYPE,'L',-1,1) AS cgst_amount, " & vbCrLf _
            & " SUM(IH.TOTSGST_AMOUNT)*DECODE(IH.BOOKTYPE,'L',-1,1) AS sgst_amount, " & vbCrLf _
            & " 0 As cess_amount"

        SqlStr = SqlStr & vbCrLf & " FROM FIN_SUPP_SALE_HDR IH,  GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, GEN_STATE_MST SMST " & vbCrLf & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND CMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND CMST.SUPP_CUST_STATE=SMST.NAME " & vbCrLf _
            & " AND IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND CMST.GST_REGD IN ('N','Y') " & vbCrLf _
            & " AND CMST.WITHIN_COUNTRY='Y' " & vbCrLf & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "' " & vbCrLf _
            & " AND IH.BOOKTYPE IN ('M','L') " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO " & vbCrLf _
            & " AND IH.ISFINALPOST='Y' AND IH.CANCELLED='N' AND IH.GST_APP='Y' AND IS_ITEMDETAIL='N'" & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO<>CMST.GST_RGN_NO "

        '' AND IH.BOOKSUBTYPE='E'

        SqlStr = SqlStr & vbCrLf & " GROUP BY SAC_CODE, (TOTCGST_PER+TOTSGST_PER+TOTIGST_PER),IH.BOOKTYPE "

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf _
            & " SELECT ID.HSNCODE AS hsn_sc, '' AS description, '' AS unit_of_measurement," & vbCrLf _
            & " (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) AS Tax_Rate, " & vbCrLf _
            & " SUM(ID.ITEM_QTY) AS quantity, SUM(DECODE(ID.CHARGEABLEGLASS_AREA,0,1,ID.CHARGEABLEGLASS_AREA) * ID.ITEM_QTY) AS SQM_QTY, SUM(ID.GSTABLE_AMT+ID.CGST_AMOUNT+ID.SGST_AMOUNT+ID.IGST_AMOUNT) AS invoice_value," & vbCrLf _
            & " SUM(ID.GSTABLE_AMT) AS taxable_value, " & vbCrLf _
            & " SUM(ID.IGST_AMOUNT) AS igst_amount, " & vbCrLf _
            & " SUM(ID.CGST_AMOUNT) AS cgst_amount, " & vbCrLf _
            & " SUM(ID.SGST_AMOUNT) AS sgst_amount, " & vbCrLf _
            & " 0 As cess_amount"


        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, GEN_COMPANY_MST GMST, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST CSMST  " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY  " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE  " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE  "

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=CSMST.COMPANY_CODE  " & vbCrLf _
            & " AND IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE  " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE  " & vbCrLf _
            & " AND IH.INVOICESEQTYPE IN (6)  " & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')  " & vbCrLf _
            & " AND CMST.WITHIN_COUNTRY='N'  " & vbCrLf _
            & " AND IH.REF_DESP_TYPE<>'U'  " & vbCrLf _
            & " AND GMST.COMPANY_GST_RGN_NO='" & pCompanyCode & "'  "

        SqlStr = SqlStr & vbCrLf & " GROUP BY ID.HSNCODE, (ID.CGST_PER+ID.SGST_PER+ID.IGST_PER) "

        SqlStr = SqlStr & vbCrLf _
            & " ) GROUP BY  hsn_sc, " & vbCrLf _
            & " description,  " & vbCrLf _
            & " unit_of_measurement,Tax_Rate"


        Show_VWGSTR1_HSN = True
        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        Show_VWGSTR1_HSN = False
        '    Resume
    End Function


    Private Sub frmParamGSTR1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String
        Dim CntLst As Long
        Dim Rs As ADODB.Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)


        Call PrintStatus(True)

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        SSTab1.SelectedIndex = 0


        SqlStr = "SELECT DISTINCT COMPANY_GST_RGN_NO  FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_GST_RGN_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboGSTNO.SelectedIndex = -1
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboGSTNO.Items.Add(Rs.Fields("COMPANY_GST_RGN_NO").Value)
                Rs.MoveNext()
            Loop
            cboGSTNO.SelectedIndex = 0
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamGSTR1_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Double
        Dim mFrameWidth As Double
        Dim mSSTabWidth As Double
        Dim mSprdMainWidth As Double

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        mFrameWidth = VB6.PixelsToTwipsX(Me.Width) - 2 ''Frame4.Width
        mSSTabWidth = VB6.PixelsToTwipsX(Me.Width) - 220 ''SSTab1.Width
        mSprdMainWidth = VB6.PixelsToTwipsX(Me.Width) - 500 ''SprdMain.Width


        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mFrameWidth, mReFormWidth), 11364.5, 748)
        SSTab1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 220, mSSTabWidth, mReFormWidth))
        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain5A.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain6.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain6A.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain7.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain7A.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain8.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain8A.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain9.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))

        SprdMain10.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        SprdMain11.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth))
        '    SprdMain10A.Width = IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth)
        '    SprdMain11.Width = IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth)
        '    SprdMain11A.Width = IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth)
        '    SprdMain12.Width = IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth)
        '    SprdMain13.Width = IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth)
        '    SprdMain13A.Width = IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth)
        '    SprdMain13B.Width = IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth)
        '    SprdMain14.Width = IIf(mReFormWidth > 500, mSprdMainWidth, mReFormWidth)

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGSTR1_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.hide()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim xVDate As String
        Dim xMkey As String = ""
        Dim xVNo As String
        Dim xBookType As String = ""
        Dim xBookSubType As String


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColBillNo
        xVDate = VB.Right(Me.SprdMain.Text, 10)

        SprdMain.Col = ColMKEY
        xMkey = Me.SprdMain.Text

        SprdMain.Col = ColBillNo
        xVNo = "S" & VB.Left(Me.SprdMain.Text, 6)

        'Call ShowTrn(xMkey, xVDate, "S", xVNo, "S", "")
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = 15
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 8)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 20)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 10)

            .Col = 7
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(7, 12)

            .Col = 8
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(8, 12)

            For cntCol = 9 To 14
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99.99")
                .TypeFloatMax = CDbl("99.99")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .set_ColWidth(cntCol, 10)
            Next

            .Col = 15
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(15, 22)

            '.Col = 11
            '.CellType = SS_CELL_TYPE_FLOAT
            '.TypeFloatDecimalPlaces = 2
            '.TypeFloatMin = CDbl("-99999999999")
            '.TypeFloatMax = CDbl("99999999999")
            '.TypeFloatMoney = False
            '.TypeFloatSeparator = False
            '.TypeFloatDecimalChar = Asc(".")
            '.TypeFloatSepChar = Asc(",")
            '.set_ColWidth(11, 10)


            '.Col = 10
            '.CellType = SS_CELL_TYPE_FLOAT
            '.TypeFloatDecimalPlaces = 2
            '.TypeFloatMin = CDbl("-99999999999")
            '.TypeFloatMax = CDbl("99999999999")
            '.TypeFloatMoney = False
            '.TypeFloatSeparator = False
            '.TypeFloatDecimalChar = Asc(".")
            '.TypeFloatSepChar = Asc(",")
            '.set_ColWidth(10, 10)


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            '        SprdMain.SelectBlockOptions = IIf(PubGridLockUser = "Y", SelectBlockOptionsNone, SelectBlockOptionsAll)
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Sub FormatSprdMain10(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain10
            .MaxCols = 5
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            'Nature  of Document Sr. No. From    Sr. No. To  Total Number    Cancelled

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 45)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 15)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 15)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 15)

            .Col = 5
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(5, 15)


            MainClass.SetSpreadColor(SprdMain10, -1)
            MainClass.ProtectCell(SprdMain10, 1, .MaxRows, 1, .MaxCols)
            SprdMain10.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain10.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            '        SprdMain10.SelectBlockOptions = IIf(PubGridLockUser = "Y", SelectBlockOptionsNone, SelectBlockOptionsAll)
            SprdMain10.DAutoCellTypes = True
            SprdMain10.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain10.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Sub FormatSprdMain11(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain11
            .MaxCols = 4
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            'Nature  of Document Sr. No. From    Sr. No. To  Total Number    Cancelled

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(1, 35)


            .Col = 2
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(2, 15)

            .Col = 3
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(3, 15)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 15)

            MainClass.SetSpreadColor(SprdMain11, -1)
            MainClass.ProtectCell(SprdMain11, 1, .MaxRows, 1, .MaxCols)
            SprdMain11.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain11.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            '        SprdMain11.SelectBlockOptions = IIf(PubGridLockUser = "Y", SelectBlockOptionsNone, SelectBlockOptionsAll)
            SprdMain11.DAutoCellTypes = True
            SprdMain11.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain11.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain5A(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain5A
            .MaxCols = 8
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(1, 8)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            '        .Col = 3
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = False
            '        .ColWidth(3) = 10
            '
            '        .Col = 4
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(4) = 8
            '
            '        .Col = 5
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(5) = 8

            .Col = 3
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(3, 10)

            .Col = 4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(4, 20)


            .Col = 6
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(6, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(5, 10)

            .Col = 7
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(7, 10)

            .Col = 8
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(8, 8)
            .ColHidden = False


            MainClass.SetSpreadColor(SprdMain5A, -1)
            MainClass.ProtectCell(SprdMain5A, 1, .MaxRows, 1, .MaxCols)
            SprdMain5A.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain5A.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            '        SprdMain5A.SelectBlockOptions = IIf(PubGridLockUser = "Y", SelectBlockOptionsNone, SelectBlockOptionsAll)
            SprdMain5A.DAutoCellTypes = True
            SprdMain5A.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain5A.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain6(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain6
            .MaxCols = 6
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(1, 6)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 15)

            .Col = 3
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(3, 10)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 10)


            .Col = 5
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(5, 10)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 8)
            .ColHidden = False


            MainClass.SetSpreadColor(SprdMain6, -1)
            MainClass.ProtectCell(SprdMain6, 1, .MaxRows, 1, .MaxCols)
            SprdMain6.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain6.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            '        SprdMain6.SelectBlockOptions = IIf(PubGridLockUser = "Y", SelectBlockOptionsNone, SelectBlockOptionsAll)
            SprdMain6.DAutoCellTypes = True
            SprdMain6.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain6.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain7(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain7
            .MaxCols = 13
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 6)

            .Col = 4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(4, 8)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 6)


            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 6)


            .Col = 7
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(7, 6)


            .Col = 8
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(8, 6)


            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(9, 10)

            .Col = 10
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(10, 10)

            .Col = 11
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(11, 10)

            .Col = 12
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(12, 10)

            .Col = 13
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(13, 12)
            '        .ColHidden = True

            MainClass.SetSpreadColor(SprdMain7, -1)
            MainClass.ProtectCell(SprdMain7, 1, .MaxRows, 1, .MaxCols)
            SprdMain7.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain7.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            '        SprdMain7.SelectBlockOptions = IIf(PubGridLockUser = "Y", SelectBlockOptionsNone, SelectBlockOptionsAll)
            SprdMain7.DAutoCellTypes = True
            SprdMain7.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain7.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain8(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain8
            .MaxCols = 4
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(2, 10)

            .Col = 3
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(3, 10)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(4, 10)


            MainClass.SetSpreadColor(SprdMain8, -1)
            MainClass.ProtectCell(SprdMain8, 1, .MaxRows, 1, .MaxCols)
            SprdMain8.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain8.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            '        SprdMain8.SelectBlockOptions = IIf(PubGridLockUser = "Y", SelectBlockOptionsNone, SelectBlockOptionsAll)
            SprdMain8.DAutoCellTypes = True
            SprdMain8.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain8.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Sub FormatSprdMain8A(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain8A
            .MaxCols = 4
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(2, 10)

            .Col = 3
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(3, 10)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(4, 10)

            MainClass.SetSpreadColor(SprdMain8A, -1)
            MainClass.ProtectCell(SprdMain8A, 1, .MaxRows, 1, .MaxCols)
            SprdMain8A.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain8A.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            '        SprdMain8A.SelectBlockOptions = IIf(PubGridLockUser = "Y", SelectBlockOptionsNone, SelectBlockOptionsAll)
            SprdMain8A.DAutoCellTypes = True
            SprdMain8A.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain8A.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FormatSprdMain9(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain9
            .MaxCols = 11

            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 15)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 10)

            For cntCol = 4 To 12
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 15)
            Next

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                .Col = 6
                .ColHidden = False
            Else
                .Col = 6
                .ColHidden = True
            End If

            MainClass.SetSpreadColor(SprdMain9, -1)
            MainClass.ProtectCell(SprdMain9, 1, .MaxRows, 1, .MaxCols)
            SprdMain9.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain9.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            '        SprdMain9.SelectBlockOptions = IIf(PubGridLockUser = "Y", SelectBlockOptionsNone, SelectBlockOptionsAll)
            SprdMain9.DAutoCellTypes = True
            SprdMain9.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain9.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)


        End With
    End Sub
    Private Sub FormatSprdMain7A(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain7A
            .MaxCols = 9
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(1, 8)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 10)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 6)

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(4, 10)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 6)

            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 6)

            .Col = 7
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(7, 6)

            .Col = 8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(8, 10)

            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(9, 10)

            MainClass.SetSpreadColor(SprdMain7A, -1)
            MainClass.ProtectCell(SprdMain7A, 1, .MaxRows, 1, .MaxCols)
            SprdMain7A.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain7A.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            '        SprdMain7A.SelectBlockOptions = IIf(PubGridLockUser = "Y", SelectBlockOptionsNone, SelectBlockOptionsAll)
            SprdMain7A.DAutoCellTypes = True
            SprdMain7A.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain7A.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Sub FormatSprdMain6A(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain6A
            .MaxCols = 17
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(1, 10)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(2, 8)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(3, 6)

            .Col = 4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(4, 8)

            .Col = 5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(5, 6)


            .Col = 6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(6, 6)


            .Col = 7
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(7, 6)


            .Col = 8
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(8, 6)


            .Col = 9
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(9, 10)

            .Col = 10
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(10, 10)

            .Col = 11
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(11, 10)


            For cntCol = 12 To 15
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .set_ColWidth(cntCol, 10)
            Next

            .Col = 16
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(16, 12)
            '        .ColHidden = True

            .Col = 17
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(17, 22)

            MainClass.SetSpreadColor(SprdMain6A, -1)
            MainClass.ProtectCell(SprdMain6A, 1, .MaxRows, 1, .MaxCols)
            SprdMain6A.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''  = OperationModeSingle
            SprdMain6A.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            '        SprdMain6A.SelectBlockOptions = IIf(PubGridLockUser = "Y", SelectBlockOptionsNone, SelectBlockOptionsAll)
            SprdMain6A.DAutoCellTypes = True
            SprdMain6A.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain6A.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function SaleQuery(ByRef SqlStr As String, ByRef pReportNo As Integer, ByRef pBookCode As String, ByRef pGSTRegd As String, ByRef pWithInState As String, ByRef pWithInCountry As String, ByRef pSign As String, ByRef pInvoiceLimit As Double, ByRef pRefType As String) As Boolean
        On Error GoTo LedgError


        ''SELECT CLAUSE...
        SqlStr = ""

        If pReportNo = 5 Then
            SqlStr = " SELECT CMST.GST_RGN_NO, TRN.BILLNO, TO_CHAR(TRN.INVOICE_DATE,'DD/MM/YYYY') AS INVOICE_DATE, " & vbCrLf & " TRN.ITEM_AMT, TRN.ITEMDESC, " & vbCrLf & " TRN.HSNCODE, " & vbCrLf & " TRN.ITEM_AMT, " & vbCrLf & " IGST_PER, IGST_AMOUNT, " & vbCrLf & " CGST_PER, CGST_AMOUNT," & vbCrLf & " SGST_PER, SGST_AMOUNT," & vbCrLf & " CASE WHEN SHIPPED_TO_SAMEPARTY='Y' THEN '' ELSE LOCATION END PARTYLOC," & vbCrLf & " '',PROVISIONAL_ASSESSMENT,'', " & vbCrLf & " TRN.MKEY"
        ElseIf pReportNo = 6 Then
            SqlStr = " SELECT STATEMST.STATE_CODE,CMST.SUPP_CUST_NAME, " & vbCrLf & " TRN.BILLNO, TO_CHAR(TRN.INVOICE_DATE,'DD/MM/YYYY') AS INVOICE_DATE, " & vbCrLf & " TRN.ITEM_AMT, TRN.ITEMDESC, " & vbCrLf & " IMST.HSN_CODE, " & vbCrLf & " TRN.ITEM_AMT, " & vbCrLf & " IGST_PER," & vbCrLf & " IGST_AMOUNT," & vbCrLf & " CASE WHEN SHIPPED_TO_SAMEPARTY='Y' THEN '' ELSE LOCATION END PARTYLOC," & vbCrLf & " PROVISIONAL_ASSESSMENT, " & vbCrLf & " TRN.MKEY"
        ElseIf pReportNo = 7 Then
            SqlStr = " SELECT TRN.ITEMDESC, IMST.HSN_CODE, STATEMST.STATE_CODE, " & vbCrLf & " TRN.ITEM_AMT, " & vbCrLf & " IGST_PER, IGST_AMOUNT, " & vbCrLf & " CGST_PER, CGST_AMOUNT," & vbCrLf & " SGST_PER, SGST_AMOUNT," & vbCrLf & " PROVISIONAL_ASSESSMENT, " & vbCrLf & " TRN.MKEY"
        ElseIf pReportNo = 8 Then
            SqlStr = " SELECT CMST.GST_RGN_NO, " & vbCrLf & " CASE WHEN TRN.BOOKCODE=" & ConDebitNoteBookCode & " THEN 'DEBIT' ELSE 'CREDIT' END,  " & vbCrLf & " TRN.VNO,  TRN.VDATE, TRN.BILLNO, TRN.INVOICE_DATE, TRN.ITEM_AMT, " & vbCrLf & " IGST_PER, IGST_AMOUNT, " & vbCrLf & " CGST_PER, CGST_AMOUNT," & vbCrLf & " SGST_PER, SGST_AMOUNT," & vbCrLf & " TRN.MKEY"
        ElseIf pReportNo = 10 Then
            SqlStr = " SELECT DECODE(IGST_AMOUNT+CGST_AMOUNT+SGST_AMOUNT,0,'Without Payment of GST','With Payment of GST'), TRN.BILLNO, TO_CHAR(TRN.INVOICE_DATE,'DD/MM/YYYY') AS INVOICE_DATE, " & vbCrLf & " TRN.ITEM_AMT, TRN.ITEMDESC, " & vbCrLf & " TRN.HSNCODE, " & vbCrLf & " TRN.ITEM_AMT, " & vbCrLf & " (SELECT EXPBILLNO FROM FIN_INVOICE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MKEY=TRN.MKEY), " & vbCrLf & " (SELECT EXPINV_DATE FROM FIN_INVOICE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MKEY=TRN.MKEY), " & vbCrLf & " IGST_PER, IGST_AMOUNT, " & vbCrLf & " CGST_PER, CGST_AMOUNT," & vbCrLf & " SGST_PER, SGST_AMOUNT," & vbCrLf & " PROVISIONAL_ASSESSMENT, " & vbCrLf & " TRN.MKEY"
        ElseIf pReportNo = 11 Then
            SqlStr = " SELECT CMST.GST_RGN_NO, STATEMST.STATE_CODE, TRN.VNO, TRN.VDATE, " & vbCrLf & " DECODE(TRN.BOOKTYPE,'G','GOODS','SERVICES') AS BOOKTYPE," & vbCrLf & " TRN.HSNCODE, TRN.ITEM_AMT, " & vbCrLf & " CMST.SUPP_CUST_NAME, " & vbCrLf & " IGST_PER, IGST_AMOUNT," & vbCrLf & " CGST_PER, CGST_AMOUNT," & vbCrLf & " SGST_PER, SGST_AMOUNT," & vbCrLf & " TRN.MKEY"
        End If
        '

        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM " & vbCrLf & " FIN_GST_POST_TRN TRN, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST, INV_GENERAL_MST GMST, GEN_STATE_MST STATEMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND TRN.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND TRN.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf & " AND TRN.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf & " AND IMST.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf & " AND IMST.CATEGORY_CODE=GMST.GEN_CODE" & vbCrLf & " AND CMST.COMPANY_CODE=STATEMST.COMPANY_CODE" & vbCrLf & " AND CMST.SUPP_CUST_STATE=STATEMST.NAME"

        SqlStr = SqlStr & vbCrLf _
            & " AND TRN.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If pGSTRegd <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND GST_REGD='" & pGSTRegd & "'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND BOOKCODE IN (" & pBookCode & ")"

        If pRefType <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND REF_TYPE IN (" & pRefType & ")"
        End If

        If pReportNo = 7 Then
            SqlStr = SqlStr & vbCrLf & "AND (WITHIN_STATE='Y' OR (SELECT NETVALUE FROM FIN_INVOICE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MKEY=TRN.MKEY) " & pSign & " " & pInvoiceLimit & ")"
        Else
            If pWithInState <> "" Then
                SqlStr = SqlStr & vbCrLf & "AND WITHIN_STATE='" & pWithInState & "'"
            End If

            If pInvoiceLimit > 0 Then
                SqlStr = SqlStr & vbCrLf & "AND (SELECT NETVALUE FROM FIN_INVOICE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MKEY=TRN.MKEY) " & pSign & " " & pInvoiceLimit & ""
            End If
        End If

        If pWithInCountry <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND WITHIN_COUNTRY='" & pWithInCountry & "'"
        End If


        '    If mTrnTypeStr <> "" Then
        '        mTrnTypeStr = "(" & mTrnTypeStr & ")"
        '        SqlStr = SqlStr & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
        '    End If


        ''ORDER CLAUSE...

        '    SqlStr = SqlStr & vbCrLf & "ORDER BY TRN.BILLNO, TRN.INVOICE_DATE"

        SaleQuery = True
        Exit Function
LedgError:
        SqlStr = ""
        SaleQuery = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcSprdTotal()

        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mQty As Double
        Dim mSQMQty As Double
        Dim mItemAmount As Double
        Dim mTaxableAmount As Double
        Dim mIGSTAmount As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mCessAmount As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                '            .Col = 4
                '            mItemAmount = mItemAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 10
                mTaxableAmount = mTaxableAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 11
                mCGSTAmount = mCGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 12
                mSGSTAmount = mSGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 13
                mIGSTAmount = mIGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 14
                mCessAmount = mCessAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColBillNo)
            .Col = ColBillNo
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows
            '
            '        .Col = 4
            '        .Text = Format(mItemAmount, "0.00")

            .Col = 10
            .Text = VB6.Format(mTaxableAmount, "0.00")

            .Col = 11
            .Text = VB6.Format(mCGSTAmount, "0.00")

            .Col = 12
            .Text = VB6.Format(mSGSTAmount, "0.00")

            .Col = 13
            .Text = VB6.Format(mIGSTAmount, "0.00")

            .Col = 14
            .Text = VB6.Format(mCessAmount, "0.00")

        End With

        mTaxableAmount = 0
        mIGSTAmount = 0
        mItemAmount = 0
        mCGSTAmount = 0
        mSGSTAmount = 0

        With SprdMain9
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = 5
                mQty = mQty + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 6
                mSQMQty = mSQMQty + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 7
                mItemAmount = mItemAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 8
                mTaxableAmount = mTaxableAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 9
                mCGSTAmount = mCGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 10
                mSGSTAmount = mSGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 11
                mIGSTAmount = mIGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain9, 1)
            .Col = 1
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows

            .Col = 5
            .Text = VB6.Format(mQty, "0.00")

            .Col = 6
            .Text = VB6.Format(mSQMQty, "0.00")

            .Col = 7
            .Text = VB6.Format(mItemAmount, "0.00")

            .Col = 8
            .Text = VB6.Format(mTaxableAmount, "0.00")

            .Col = 9
            .Text = VB6.Format(mCGSTAmount, "0.00")

            .Col = 10
            .Text = VB6.Format(mSGSTAmount, "0.00")

            .Col = 11
            .Text = VB6.Format(mIGSTAmount, "0.00")


        End With

        mTaxableAmount = 0
        mIGSTAmount = 0
        mItemAmount = 0
        mCGSTAmount = 0
        mSGSTAmount = 0

        With SprdMain6A
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = 9
                mItemAmount = mItemAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 11
                mTaxableAmount = mTaxableAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 12
                mCGSTAmount = mCGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0)) '.Text = VB6.Format(mCGSTAmount, "0.00")

                .Col = 13
                mSGSTAmount = mSGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = 14
                mIGSTAmount = mIGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))
            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain6A, 1)
            .Col = 1
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows


            .Col = 9
            .Text = VB6.Format(mItemAmount, "0.00")

            .Col = 11
            .Text = VB6.Format(mTaxableAmount, "0.00")

            .Col = 12
            .Text = VB6.Format(mCGSTAmount, "0.00")

            .Col = 13
            .Text = VB6.Format(mSGSTAmount, "0.00")

            .Col = 14
            .Text = VB6.Format(mIGSTAmount, "0.00")
        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

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
