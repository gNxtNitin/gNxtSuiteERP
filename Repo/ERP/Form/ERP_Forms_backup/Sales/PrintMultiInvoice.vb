Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Color
Imports System.Drawing
Imports System.Drawing.Printing
Friend Class frmPrintMultiInvoice
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection

    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        If chkPrintAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboInvType.SelectedIndex = -1 Then
                MsgBox("Please Select a Invoice Type ", MsgBoxStyle.Information)
                cboInvType.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If optPartyName(1).Checked = True Then
            If Trim(txtPartyName.Text) = "" Then
                MsgBox("Customer Name Cann't be blank. ", MsgBoxStyle.Information)
                txtPartyName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid Customer Name. ", MsgBoxStyle.Information)
                txtPartyName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If optPrintRange(1).Checked = True Then
            If Trim(txtInvNoFrom.Text) = "" Then
                MsgBox("Invoice No. Cann't be blank. ", MsgBoxStyle.Information)
                txtInvNoFrom.Focus()
                FieldsVerification = False
                Exit Function
            End If

            If Trim(txtInvNoTo.Text) = "" Then
                MsgBox("Invoice No. Cann't be blank. ", MsgBoxStyle.Information)
                txtInvNoTo.Focus()
                FieldsVerification = False
                Exit Function
            End If

            If Trim(txtInvNoFrom.Text) > Trim(txtInvNoTo.Text) Then
                MsgBox(" 'Invoice No. To ' Cann't be Less Than 'Invoice No. From.' ", MsgBoxStyle.Information)
                txtInvNoTo.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub chkPrintAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPrintAll.CheckStateChanged
        cboInvType.Enabled = IIf(chkPrintAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Me.hide()
    End Sub

    Private Sub cmdPDF_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPDF.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonInv(Crystal.DestinationConstants.crptToWindow, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonInv(Crystal.DestinationConstants.crptToWindow, "N")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonInv(Crystal.DestinationConstants.crptToPrinter, "N")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonInv(ByRef Mode As Crystal.DestinationConstants, pIsPDF As String)
        On Error GoTo ReportErr

        Dim pSqlStr As String
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRptFileName As String
        Dim mPrintOption As String = ""
        Dim mMKey As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pBuyerCode As String
        Dim pCustomerCode As String
        Dim pLocationID As String
        Dim pTotAmount As Double
        Dim pCurrName As String
        Dim pInvoiceType As String


        If cboInvType.Text = "Export Invoice" Then
            frmPrintInvoice.OptInvoice.Enabled = True
            frmPrintInvoice.OptInvoice.Visible = True
            frmPrintInvoice.OptInvoice.Text = "Export Invoice"
            frmPrintInvoice.OptInvoiceAnnex.Enabled = True
            frmPrintInvoice.OptInvoiceAnnex.Visible = True
            frmPrintInvoice.OptInvoiceAnnex.Text = "Packing List"
            frmPrintInvoice.optSubsidiaryChallan.Enabled = False
            frmPrintInvoice.optSubsidiaryChallan.Visible = False
            frmPrintInvoice.FraF4.Enabled = False
            frmPrintInvoice.FraF4.Visible = False
            frmPrintInvoice.ShowDialog()

            If G_PrintLedg = False Then
                frmPrintInvoice.Close()
                frmPrintInvoice.Dispose()
                Exit Sub
            Else
                mPrintOption = IIf(frmPrintInvoice.OptInvoice.Checked = True, "E", "P") 'E-Export Invoice , P-Packing List	
                frmPrintInvoice.Close()
                frmPrintInvoice.Dispose()
            End If
        Else
            mPrintOption = "PL"
        End If

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Report1.Reset()
        If mPrintOption = "E" Then
            mTitle = "COMMERCIAL INVOICE"
            mSubTitle = ""
            mRptFileName = "ExportInv_All.RPT"
            pInvoiceType = "ExpInv"
        ElseIf mPrintOption = "P" Then
            mTitle = "PACKING LIST"
            mSubTitle = ""
            mRptFileName = "ExportPacking_All.RPT"
            pInvoiceType = "ExpPack"
        Else
            mTitle = "Requisation Slip/Dispatch Advice"
            mSubTitle = ""
            mRptFileName = "PackingList_ALLNew.RPT"
            pInvoiceType = ""
        End If

        If mPrintOption = "PL" Then
            SqlStr = MakeSQLPL
            Call ShowReportPL(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        Else
            SqlStr = MakeSQL(mPrintOption, "")

            If pIsPDF = "N" Then
                Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
            Else
                Call ShowReportPDF(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, pInvoiceType)
            End If
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ShowReportPL(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mCompanyCity As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBuyerCode As String
        Dim mFormulaStr As String
        Dim pSqlStr As String
        Dim mCOMPANYTYPE As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        mCompanyCity = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        mCompanyCity = mCompanyCity & "-" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)
        mCompanyCity = mCompanyCity & "(" & IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & ") INDIA"


        MainClass.AssignCRptFormulas(Report1, "CompanyAddress=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyCity=""" & mCompanyCity & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyPhone=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", RsCompany.Fields("COMPANY_PHONE").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyEmail=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", RsCompany.Fields("COMPANY_MAILID").Value) & """")

        mCOMPANYTYPE = IIf(RsCompany.Fields("ISEOU").Value = "Y", "100% E.O.U.", "")
        MainClass.AssignCRptFormulas(Report1, "COMPANYTYPE=""" & mCOMPANYTYPE & """")

        'If MainClass.ValidateWithMasterTable(txtBuyerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mBuyerCode = MasterNo
        '    If mBuyerCode = "" Then
        '        mBuyerCode = txtCustomerCode.Text
        '    End If
        '    pSqlStr = " SELECT CMST.SUPP_CUST_NAME, BMST.SUPP_CUST_ADDR, " & vbCrLf _
        '        & " BMST.SUPP_CUST_CITY, BMST.COUNTRY, BMST.SUPP_CUST_PIN, " & vbCrLf _
        '        & "  CMST.SUPP_CUST_PHONE,  CMST.SUPP_CUST_FAXNO " & vbCrLf _
        '        & " FROM FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BMST" & vbCrLf _
        '        & " WHERE  CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '        & " AND  CMST.COMPANY_CODE=BMST.COMPANY_CODE" & vbCrLf _
        '        & " AND  CMST.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
        '        & " AND  BMST.LOCATION_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'" & vbCrLf _
        '        & " AND  CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mBuyerCode) & "'"

        '    MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        '    If RsTemp.EOF = False Then
        '        mFormulaStr = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
        '        MainClass.AssignCRptFormulas(Report1, "BuyerName=""" & mFormulaStr & """")

        '        mFormulaStr = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
        '        mFormulaStr = Replace(mFormulaStr, vbCrLf, " ")
        '        MainClass.AssignCRptFormulas(Report1, "BuyerAddress=""" & mFormulaStr & """")

        '        mFormulaStr = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
        '        MainClass.AssignCRptFormulas(Report1, "BuyerCity=""" & mFormulaStr & """")

        '        mFormulaStr = IIf(IsDBNull(RsTemp.Fields("COUNTRY").Value), "", RsTemp.Fields("COUNTRY").Value)
        '        MainClass.AssignCRptFormulas(Report1, "BuyerCountry=""" & mFormulaStr & """")

        '        mFormulaStr = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PHONE").Value), "", "Phone No.:" & RsTemp.Fields("SUPP_CUST_PHONE").Value)
        '        mFormulaStr = mFormulaStr & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_FAXNO").Value), "", "Fax No.:" & RsTemp.Fields("SUPP_CUST_FAXNO").Value)
        '        MainClass.AssignCRptFormulas(Report1, "BuyerPhone=""" & mFormulaStr & """")
        '    End If
        'End If


        Report1.ReportFileName = PubReportFolderPath & mRptFileName


        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowReportPDF(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String,
                           ByRef mRptFileName As String, pInvoiceType As String)

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mCompanyCity As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBuyerCode As String
        Dim mFormulaStr As String
        Dim pSqlStr As String
        Dim mMajorCurr As String
        Dim mMinorCurr As String
        Dim mCOMPANYTYPE As String
        Dim mCntRow As Long
        Dim mFormulaName As String
        Dim mFormulaValue As String
        Dim fPath As String

        Dim CrReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument  ' Report Name 
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions

        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions


        'mRptFileName = PubReportFolderPath & mRptFileName

        CrReport.Load(PubReportFolderPath & mRptFileName)

        Call Connect_MainReport_To_Database_11(CrReport)        '', mSqlStr

        'CrReport.RecordSelectionFormula = "{IH.COMPANY_CODE} = " & RsCompany.Fields("COMPANY_CODE").Value & " AND {IH.FYEAR} = " & RsCompany.Fields("FYEAR").Value & " AND {IH.AUTO_KEY_EXPINV} = " & Val(mMKey) & ""

        ClearCRpt11Formulas(CrReport)
        CrReport.ReportOptions.EnableSaveDataWithReport = False
        SetCompanyReport11(CrReport, 1, mTitle, mSubTitle)
        CrReport.Refresh()

        mCompanyCity = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        mCompanyCity = mCompanyCity & "-" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)
        mCompanyCity = mCompanyCity & " (" & IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & ") INDIA"

        AssignCRpt11Formulas(CrReport, "CompanyAddress", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyCity", "'" & mCompanyCity & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPhone", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", RsCompany.Fields("COMPANY_PHONE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyEmail", "'" & "Email : " & IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", RsCompany.Fields("COMPANY_MAILID").Value) & "'")

        mCOMPANYTYPE = IIf(RsCompany.Fields("ISEOU").Value = "Y", "100% E.O.U.", "")
        AssignCRpt11Formulas(CrReport, "COMPANYTYPE", "'" & mCOMPANYTYPE & "'")


        Dim pOutPutFileName As String = ""
        Dim mMKey As String = Format(RunDate, "DDMMYYYY")

        'mMKey = Replace(mMKey, "\", "")
        fPath = mPubBarCodePath & "\" & pInvoiceType & "_" & RsCompany.Fields("COMPANY_CODE").Value & "_" & mMKey & ".pdf"

        If FILEExists(fPath) Then
            DeleteFile(fPath)
        End If

        CrDiskFileDestinationOptions.DiskFileName = fPath
        CrExportOptions = CrReport.ExportOptions

        With CrExportOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
            .DestinationOptions = CrDiskFileDestinationOptions
            .FormatOptions = CrFormatTypeOptions
        End With
        CrReport.Export()
        'CrReport.Dispose()


        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String,
                           ByRef mRptFileName As String)

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mCompanyCity As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBuyerCode As String
        Dim mFormulaStr As String
        Dim pSqlStr As String
        Dim mMajorCurr As String
        Dim mMinorCurr As String
        Dim mCOMPANYTYPE As String
        Dim mCntRow As Long
        Dim mFormulaName As String
        Dim mFormulaValue As String


        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle,,, "Y")

        mCompanyCity = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        mCompanyCity = mCompanyCity & "-" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)
        mCompanyCity = mCompanyCity & " (" & IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & ") INDIA"


        MainClass.AssignCRptFormulas(Report1, "CompanyAddress=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyCity=""" & mCompanyCity & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyPhone=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", RsCompany.Fields("COMPANY_PHONE").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyEmail=""" & "Email : " & IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", RsCompany.Fields("COMPANY_MAILID").Value) & """")

        mCOMPANYTYPE = IIf(RsCompany.Fields("ISEOU").Value = "Y", "100% E.O.U.", "")
        MainClass.AssignCRptFormulas(Report1, "COMPANYTYPE=""" & mCOMPANYTYPE & """")

        Report1.ReportFileName = PubReportFolderPath & mRptFileName


        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False


        Report1.Action = 1
        Report1.Reset()

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function MakeSQL(pType As String, pMKey As String) As Object
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = ""

        If pType = "E" Then
            'If pMKey = "" Then
            '    MakeSQL = "SELECT DISTINCT IH.AUTO_KEY_EXPINV, IH.NETVALUE, IH.NETVALUE_INR, IH.CURR_DESC, IH.BILL_TO_LOC_ID, IH.SUPP_CUST_CODE, IH.BUYER_CODE "
            'Else
            MakeSQL = "SELECT IH.*, ID.*, ED.*, CMST.*, INVMST.*, BMST.*, GMST.*, BUY_CMST.*, BUY_BMST.*, CURRMST.CURR_DESC, CURRMST.MINOR_CURR  "
            'End If

            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_EXPINV_HDR IH, FIN_EXPINV_DET ID, vw_FIN_EXPORT_PARA_EXP ED," & vbCrLf _
                & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BMST, " & vbCrLf _
                & " FIN_SUPP_CUST_MST BUY_CMST, FIN_SUPP_CUST_BUSINESS_MST BUY_BMST, INV_ITEM_MST INVMST, FIN_CURRENCY_MST CURRMST, GEN_COMPANY_MST GMST "

            MakeSQL = MakeSQL & vbCrLf _
                & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
                & " And IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " And IH.AUTO_KEY_EXPINV=ID.AUTO_KEY_EXPINV " & vbCrLf _
                & " And IH.AUTO_KEY_EXPINV=ED.AUTO_KEY_EXPINV " & vbCrLf _
                & " And IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
                & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
                & " And IH.COMPANY_CODE=BMST.COMPANY_CODE " & vbCrLf _
                & " And IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE " & vbCrLf _
                & " And IH.SHIP_TO_LOC_ID=BMST.LOCATION_ID " & vbCrLf _
                & " And ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                & " And ID.ITEM_CODE=INVMST.ITEM_CODE "

            MakeSQL = MakeSQL & vbCrLf _
                & " And IH.COMPANY_CODE=BUY_CMST.COMPANY_CODE " & vbCrLf _
                & " And IH.BUYER_CODE=BUY_CMST.SUPP_CUST_CODE " & vbCrLf _
                & " And IH.COMPANY_CODE=BUY_BMST.COMPANY_CODE " & vbCrLf _
                & " And IH.BUYER_CODE=BUY_BMST.SUPP_CUST_CODE " & vbCrLf _
                & " And IH.BILL_TO_LOC_ID=BUY_BMST.LOCATION_ID "

            MakeSQL = MakeSQL & vbCrLf _
                & " And IH.COMPANY_CODE=CURRMST.COMPANY_CODE " & vbCrLf _
                & " And IH.CURR_DESC=CURRMST.CURR_DESC "

            MakeSQL = MakeSQL & vbCrLf _
                & " And IH.AUTO_KEY_EXPINV=ED.AUTO_KEY_EXPINV "

            'If pMKey <> "" Then
            '    MakeSQL = MakeSQL & vbCrLf & " And IH.AUTO_KEY_EXPINV='" & pMKey & "'"
            '    MakeSQL = MakeSQL & vbCrLf & " ORDER BY ID.SERIAL_NO"
            'Else

            If optPrintRange(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf _
                & " AND IH.EXPINV_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.EXPINV_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf _
                & " AND IH.BILLNO >='" & txtInvNoFrom.Text & "'" & vbCrLf _
                & " AND IH.BILLNO <='" & txtInvNoTo.Text & "'"
            End If
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY IH.AUTO_KEY_EXPINV"
            'End If


        Else
            'If pMKey = "" Then
            '    MakeSQL = "SELECT DISTINCT IH.AUTO_KEY_EXPINV, IH.NETVALUE, IH.NETVALUE_INR, IH.CURR_DESC, IH.BILL_TO_LOC_ID, IH.SUPP_CUST_CODE, IH.BUYER_CODE "
            'Else
            MakeSQL = "SELECT IH.*, ID.*, ED.*, CMST.*, INVMST.*, BMST.*, GMST.*, BUY_CMST.*, BUY_BMST.*, CURRMST.CURR_DESC, CURRMST.MINOR_CURR  "
            'End If

            'MakeSQL = MakeSQL & vbCrLf _
            '    & " FROM FIN_EXPINV_HDR IH, FIN_EXPINV_DET ID, DSP_PACKING_DET PD," & vbCrLf _
            '    & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BMST, INV_ITEM_MST INVMST, GEN_COMPANY_MST GMST "

            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_EXPINV_HDR IH, FIN_EXPINV_DET ID, vw_FIN_EXPORT_PARA_EXP ED, DSP_PACKING_DET PD," & vbCrLf _
                & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BMST, " & vbCrLf _
                & " FIN_SUPP_CUST_MST BUY_CMST, FIN_SUPP_CUST_BUSINESS_MST BUY_BMST, INV_ITEM_MST INVMST, FIN_CURRENCY_MST CURRMST,  GEN_COMPANY_MST GMST "

            MakeSQL = MakeSQL & vbCrLf _
                & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_EXPINV=ID.AUTO_KEY_EXPINV " & vbCrLf _
                & " And IH.AUTO_KEY_EXPINV=ED.AUTO_KEY_EXPINV " & vbCrLf _
                & " AND IH.COMPANY_CODE=PD.COMPANY_CODE " & vbCrLf _
                & " AND IH.AUTO_KEY_PACK=PD.AUTO_KEY_PACK " & vbCrLf _
                & " AND ID.SERIAL_NO=PD.SERIAL_NO " & vbCrLf _
                & " AND ID.ITEM_CODE=PD.ITEM_CODE " & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.COMPANY_CODE=BMST.COMPANY_CODE " & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.SHIP_TO_LOC_ID=BMST.LOCATION_ID " & vbCrLf _
                & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                & " AND ID.ITEM_CODE=INVMST.ITEM_CODE "

            MakeSQL = MakeSQL & vbCrLf _
                & " And IH.COMPANY_CODE=BUY_CMST.COMPANY_CODE " & vbCrLf _
                & " And IH.BUYER_CODE=BUY_CMST.SUPP_CUST_CODE " & vbCrLf _
                & " And IH.COMPANY_CODE=BUY_BMST.COMPANY_CODE " & vbCrLf _
                & " And IH.BUYER_CODE=BUY_BMST.SUPP_CUST_CODE " & vbCrLf _
                & " And IH.BILL_TO_LOC_ID=BUY_BMST.LOCATION_ID "

            MakeSQL = MakeSQL & vbCrLf _
                & " And IH.COMPANY_CODE=CURRMST.COMPANY_CODE " & vbCrLf _
                & " And IH.CURR_DESC=CURRMST.CURR_DESC "

            MakeSQL = MakeSQL & vbCrLf _
                & " And IH.AUTO_KEY_EXPINV=ED.AUTO_KEY_EXPINV "

            'If pMKey <> "" Then
            '    MakeSQL = MakeSQL & vbCrLf & " AND IH.AUTO_KEY_EXPINV='" & pMKey & "'"
            '    MakeSQL = MakeSQL & vbCrLf & " ORDER BY ID.SERIAL_NO"
            'Else

            If optPrintRange(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf _
                    & " AND IH.EXPINV_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND IH.EXPINV_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf _
                    & " AND IH.BILLNO >='" & txtInvNoFrom.Text & "'" & vbCrLf _
                    & " AND IH.BILLNO <='" & txtInvNoTo.Text & "'"
            End If
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY IH.AUTO_KEY_EXPINV"
            'End If


        End If
        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume
    End Function
    Private Function MakeSQLPL() As Object
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = ""


        MakeSQLPL = "SELECT IH.*, ID.*, CMST.*, INVMST.* "

        MakeSQLPL = MakeSQLPL & vbCrLf _
            & " FROM DSP_PACKING_HDR IH, DSP_PACKING_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST,  FIN_SUPP_CUST_BUSINESS_MST BMST,  FIN_SUPP_CUST_MST BUY_CMST, FIN_SUPP_CUST_BUSINESS_MST BUY_BMST, INV_ITEM_MST INVMST "

        MakeSQLPL = MakeSQLPL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_PACK=ID.AUTO_KEY_PACK " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=BMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.SHIP_TO_LOC_ID=BMST.LOCATION_ID " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE "

        MakeSQLPL = MakeSQLPL & vbCrLf _
            & " And IH.COMPANY_CODE=BUY_CMST.COMPANY_CODE " & vbCrLf _
            & " And IH.BUYER_CODE=BUY_CMST.SUPP_CUST_CODE " & vbCrLf _
            & " And IH.COMPANY_CODE=BUY_BMST.COMPANY_CODE " & vbCrLf _
            & " And IH.BUYER_CODE=BUY_BMST.SUPP_CUST_CODE " & vbCrLf _
            & " And IH.BILL_TO_LOC_ID=BUY_BMST.LOCATION_ID "


        If optPrintRange(0).Checked = True Then
            MakeSQLPL = MakeSQLPL & vbCrLf _
                & " AND IH.PACK_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.PACK_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            'MakeSQLPL = MakeSQLPL & vbCrLf _
            '    & " AND IH.AUTO_KEY_PACK IN (SELECT AUTO_KEY_PACK FROM FIN_EXPINV_HDR " & vbCrLf _
            '    & " WHERE EXPINV_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            '    & " And EXPINV_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        Else
            'MakeSQLPL = MakeSQLPL & vbCrLf _
            '    & " AND IH.AUTO_KEY_PACK >='" & txtInvNoFrom.Text & "'" & vbCrLf _
            '    & " AND IH.AUTO_KEY_PACK <='" & txtInvNoTo.Text & "'"

            MakeSQLPL = MakeSQLPL & vbCrLf _
                & " AND IH.AUTO_KEY_PACK IN (SELECT AUTO_KEY_PACK FROM FIN_EXPINV_HDR " & vbCrLf _
                & " WHERE BILLNO >='" & txtInvNoFrom.Text & "'" & vbCrLf _
                & " AND BILLNO <='" & txtInvNoTo.Text & "')"
        End If

        MakeSQLPL = MakeSQLPL & vbCrLf & " ORDER BY IH.AUTO_KEY_PACK,ID.SERIAL_NO"


        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume
    End Function
    Private Sub cmdSearchParty_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchParty.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = SqlStr & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.SearchGridMaster((txtPartyName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtPartyName.Text = AcName
        End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub cmdsearchInvNO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchInvNO.Click
        Dim Index As Short = cmdsearchInvNO.GetIndex(eventSender)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = SqlStr & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        If MainClass.SearchGridMaster(IIf(Index = 0, txtInvNoFrom.Text, txtInvNoTo.Text), "FIN_EXPINV_HDR", "BILLNO", "EXPINV_DATE", , , SqlStr) = True Then
            If Index = 0 Then
                txtInvNoFrom.Text = AcName
            Else
                txtInvNoTo.Text = AcName
            End If
        End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FillCboInvType()

        On Error GoTo ErrPart
        Dim RsSaleType As ADODB.Recordset
        Dim SqlStr As String = ""

        cboInvType.Items.Clear()
        cboInvType.Items.Add("Export Invoice")
        cboInvType.Items.Add("Requisation Slip/Dispatch Advice")            ''"Packing List")

        'SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='S' AND ISSUPPBILL='Y' ORDER BY NAME "

        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleType, ADODB.LockTypeEnum.adLockReadOnly)

        'If RsSaleType.EOF = False Then
        '    Do While Not RsSaleType.EOF
        '        cboInvType.Items.Add(RsSaleType.Fields("NAME").Value)
        '        RsSaleType.MoveNext()
        '    Loop
        'End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmPrintMultiInvoice_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(5010)
        'Me.Width = VB6.TwipsToPixelsX(4875)


        Call FillCboInvType()

        cboInvType.SelectedIndex = 0

        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        optPartyName(1).Checked = True
        cmdSearchParty.Enabled = True
        txtPartyName.Enabled = True
        cboInvType.Enabled = True
        cmdPDF.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub optPartyName_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPartyName.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optPartyName.GetIndex(eventSender)
            txtPartyName.Enabled = IIf(Index = 0, False, True)
            cmdSearchParty.Enabled = IIf(Index = 0, False, True)
        End If
    End Sub

    Private Sub optPrintRange_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPrintRange.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optPrintRange.GetIndex(eventSender)
            FraDateRange.Enabled = IIf(Index = 0, True, False)
            FraVNoRange.Enabled = IIf(Index = 1, True, False)
        End If
    End Sub


    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Cancel = True : txtDateFrom.Focus() : GoTo EventExitSub
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then Cancel = True : txtDateFrom.Focus() : GoTo EventExitSub
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then Cancel = True : txtDateTo.Focus() : GoTo EventExitSub
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then Cancel = True : txtDateTo.Focus() : GoTo EventExitSub
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInvNoFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInvNoFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If txtInvNoFrom.Text = "" Then GoTo EventExitSub

        SqlStr = SqlStr & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        If MainClass.ValidateWithMasterTable((txtInvNoFrom.Text), "BILLNO", "BILLNO", "FIN_EXPINV_HDR", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Invoice No.")
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInvNoTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInvNoTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = SqlStr & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        If MainClass.ValidateWithMasterTable((txtInvNoTo.Text), "BILLNO", "BILLNO", "FIN_EXPINV_HDR", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Invoice No.")
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.DoubleClick
        Call cmdSearchParty_Click(cmdSearchParty, New System.EventArgs())
    End Sub

    Private Sub txtPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchParty_Click(cmdSearchParty, New System.EventArgs())
    End Sub

    Private Sub txtPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtPartyName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Information)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInvNoFrom_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvNoFrom.DoubleClick
        Call cmdsearchInvNO_Click(cmdsearchInvNO.Item(0), New System.EventArgs())
    End Sub


    Private Sub txtInvNoFrom_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInvNoFrom.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInvNoFrom.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInvNoFrom_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInvNoFrom.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdsearchInvNO_Click(cmdsearchInvNO.Item(0), New System.EventArgs())
    End Sub

    Private Sub txtInvNoTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvNoTo.DoubleClick
        Call cmdsearchInvNO_Click(cmdsearchInvNO.Item(1), New System.EventArgs())
    End Sub


    Private Sub txtInvNoTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInvNoTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInvNoTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInvNoTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInvNoTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdsearchInvNO_Click(cmdsearchInvNO.Item(1), New System.EventArgs())
    End Sub
End Class
