Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel
Imports AxFPSpreadADO

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports System.Data
Imports System.IO
Imports System.Configuration




Friend Class frmAdviseEmail
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColBookType As Short = 2
    Private Const ColBookSubType As Short = 3
    Private Const ColVNo As Short = 4
    Private Const ColVDate As Short = 5
    Private Const ColChequeNo As Short = 6
    Private Const ColChequeDate As Short = 7
    Private Const colAccount As Short = 8
    Private Const ColNarration As Short = 9
    Private Const ColAmount As Short = 10
    Private Const ColeMail As Short = 11
    Private Const ColPreviewBtn As Short = 12
    Private Const ColeMailBtn As Short = 13
    Private Const ColMKEY As Short = 14
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub CboCC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboCC.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub CboCC_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboCC.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub CboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboDept.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboDept_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboDept.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboEmp.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboEmp_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboEmp.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        TxtAccount.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdsearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub
    Private Sub chkGroup_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGroup.CheckStateChanged
        Dim Index As Short = chkGroup.GetIndex(eventSender)
        Call PrintStatus(False)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdPayment, RowHeight)
        If BookInfo = False Then GoTo ErrPart
        Call FormatSprd(SprdPayment, -1)
        Call PrintStatus(True)
        MainClass.SetFocusToCell(SprdPayment, mActiveRow, colAccount)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Public Sub frmAdviseEmail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Bank Payment Advice e-Mail"
        TxtAccount.Visible = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmAdviseEmail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Call FillComboBox()
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY") 'Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Call frmAdviseEmail_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillComboBox()
        On Error GoTo ErrPart
        MainClass.FillCombo(CboCC, "FIN_CCENTER_HDR", "CC_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") ', "BranchCode=" & RsCompany.fields("CBRANCHCODE & ""
        MainClass.FillCombo(CboDept, "PAY_DEPT_MST", "DEPT_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(cboEmp, "PAY_EMPLOYEE_MST", "EMP_NAME", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        CboDept.SelectedIndex = 0
        cboEmp.SelectedIndex = 0
        CboCC.SelectedIndex = 0
        '    cboConsolidated.Clear
        '    cboConsolidated.AddItem "COMPANY"
        '    cboConsolidated.AddItem "REGION"
        '    cboConsolidated.AddItem "BRANCH"
        '    cboConsolidated.AddItem "DIVISION"
        '    cboConsolidated.Enabled = True
        '    cboConsolidated.Enabled = True
        '    cboConsolidated.ListIndex = 3
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmAdviseEmail_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdPayment.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth
        MainClass.SetSpreadColor(SprdPayment, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmAdviseEmail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()

        Me.Close()
    End Sub
    Private Sub SprdPayment_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdPayment.ButtonClicked
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mVNo As String
        Dim mBookType As String = ""
        Dim mBookSubType As String = ""
        Dim mBookCode As String = ""
        Dim SqlStr As String
        Dim mRptFileName As String
        Dim cntRow As Integer
        Dim mNarration As String
        Dim mAccountName As String = ""
        Dim mNarrDetail As String
        Dim mChequeNo As String
        Dim mNarrAcct As String
        Dim mDCType As String
        Dim mBankName As String
        Dim mPartyOpBal As String
        Dim pOpBal As Double
        Dim mAccountCode As String = ""
        Dim xAccountName As String = ""
        Dim mVDate As String
        Dim pNetPartyAmount As Double
        Dim mKey As String
        Dim empMailId As String
        Dim mDrCrNo As String
        '    Report1.Reset
        '    MainClass.ClearCRptFormulas Report1
        SqlStr = ""
        mSubTitle = ""
        SprdPayment.Row = eventArgs.row
        SprdPayment.Col = ColVNo
        mVNo = Trim(SprdPayment.Text)

        SprdPayment.Col = ColBookType
        mBookType = Trim(SprdPayment.Text)

        SprdPayment.Col = ColBookSubType
        mBookSubType = Trim(SprdPayment.Text)

        SprdPayment.Col = ColVDate
        mVDate = Trim(SprdPayment.Text)

        SprdPayment.Col = colAccount
        mAccountName = Trim(SprdPayment.Text)

        SprdPayment.Col = ColNarration
        mNarration = "Narration : " & Trim(SprdPayment.Text)
        mNarration = VB.Left(mNarration, 254)

        SprdPayment.Col = ColAmount
        pNetPartyAmount = Val(SprdPayment.Text)

        SprdPayment.Col = ColeMail
        empMailId = Trim(SprdPayment.Text)

        SprdPayment.Col = ColMKEY
        mKey = Trim(SprdPayment.Text)

        If Trim(empMailId) = "" And eventArgs.col = ColeMailBtn Then
            MsgInformation("Invalid e-Mail Id, Cann't be Send Payment Advice.")
            Exit Sub
        End If

        If Trim(mAccountName) <> "" Then
            MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mAccountCode = MasterNo
        End If
        If Trim(mKey) <> "" Then
            MainClass.ValidateWithMasterTable(mKey, "MKEY", "BOOKCODE", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mBookCode = MasterNo
        End If
        If Trim(mBookCode) <> "" Then
            MainClass.ValidateWithMasterTable(mBookCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mBankName = MasterNo
        End If
        If mBookType = "B" Then
            mBankName = "Bank : " & mBankName
            mTitle = "Bank Payment"
        Else
            mBankName = ""
            mTitle = "Journal"
        End If


        mNarrAcct = mAccountName
        mSubTitle = "We have pleasure in enclosing herewith our cheque against "
        mSubTitle = mSubTitle & " your invoice details given below :"

        If IsDate(mVDate) Then
            pOpBal = GetOpeningBal(mAccountCode, mVDate)
        End If
        mPartyOpBal = VB6.Format(System.Math.Abs(pOpBal), "0.00") & IIf(pOpBal >= 0, "Dr", "Cr")

        SqlStr = ""
        SqlStr = SelectQryForAdvise(mVNo, CDate(mVDate), mBookType, mBookSubType, mBookCode, mAccountCode)

        mDrCrNo = GETDRCRNo(mVNo, CDate(mVDate), mBookCode, mAccountCode, CStr(True))

        If eventArgs.col = ColeMailBtn Then
            mRptFileName = "eReceiptAdvise.rpt"
        Else
            mRptFileName = "ReceiptAdvise.rpt"
        End If

        mTitle = mTitle & " Advice"

        '    Call SelectQryForBankAdvise(SqlStr, mVNo, CDate(mVDate), mBookType, mBookCode, mAccountCode)
        '
        '    mRptFileName = "BankAdvise.rpt"
        '    mTitle = mTitle & IIf(chkCancelled.Value = vbChecked, " (CANCELLED )", "")

        If ShowReport(SqlStr, IIf(eventArgs.col = ColeMailBtn, "E", "P"), mTitle, mSubTitle, mRptFileName, mNarration, mBankName, mNarrAcct, pNetPartyAmount, empMailId, mPartyOpBal, mDrCrNo) = False Then GoTo ERR1

        Exit Sub
ERR1:
        If Err.Number <> 0 Then
            MsgInformation(Err.Number & " : " & Err.Description)
        End If
        '    Resume
        frmPrintVoucher.Close()
    End Sub
    Private Function SelectQryForAdvise(ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookSubType As String, ByRef mBookCode As String, ByRef mAccountCode As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSqlStr As String
        If InsertTempTable(mVNo, mVDate, mBookType, mBookCode, mAccountCode) = False Then GoTo ErrPart

        mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', 1, TEMP_FIN_PAYMENT.COMPANY_CODE, TEMP_FIN_PAYMENT.FYEAR, " & vbCrLf _
            & " TEMP_FIN_PAYMENT.BILLNO, " & vbCrLf _
            & " TO_CHAR(TEMP_FIN_PAYMENT.BILLDATE,'DD/MM/YYYY') AS BILLDATE, TEMP_FIN_PAYMENT.BILLAMOUNT, TEMP_FIN_PAYMENT.ADV, " & vbCrLf _
            & " TEMP_FIN_PAYMENT.DNOTE, TEMP_FIN_PAYMENT.CNOTE, TEMP_FIN_PAYMENT.TDS, TEMP_FIN_PAYMENT.PAYMENT, " & vbCrLf _
            & " TEMP_FIN_PAYMENT.BALANCE, TRN.DC, TEMP_FIN_PAYMENT.DCNOTE, ACM.SUPP_CUST_CODE, TEMP_FIN_PAYMENT.ACCOUNTCODE, " & vbCrLf _
            & " ACM.SUPP_CUST_ADDR, ACM.SUPP_CUST_CITY, ACM.SUPP_CUST_STATE, " & vbCrLf _
            & " ACM.SUPP_CUST_PIN,  ACM.SUPP_CUST_PHONE,TRN.CHEQUENO,TO_CHAR(TRN.CHQDATE,'DD/MM/YYYY') AS CHQDATE,TRN.AMOUNT,TEMP_FIN_PAYMENT.DUEDATE," & vbCrLf _
            & " ACM.PAN_NO, TO_CHAR(TRN.VDATE,'DD/MM/YYYY') AS VDATE, TRN.VNO , ACM.SUPP_CUST_NAME, " & vbCrLf _
            & " ACM.CUST_BANK_ACCT_NO, ACM.CUST_BANK_BANK, ACM.BANK_BRANCH_NAME, ACM.BANK_IFSC_CODE" & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN, FIN_SUPP_CUST_MST ACM, TEMP_FIN_PAYMENT" & vbCrLf _
            & " WHERE TEMP_FIN_PAYMENT.UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
            & " AND TRN.COMPANY_CODE=TEMP_FIN_PAYMENT.COMPANY_CODE(+)" & vbCrLf _
            & " AND TRN.FYEAR=TEMP_FIN_PAYMENT.FYEAR(+) " & vbCrLf _
            & " AND TRN.BillNo=TEMP_FIN_PAYMENT.BillNo(+) " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=TEMP_FIN_PAYMENT.ACCOUNTCODE(+) " & vbCrLf _
            & " AND TRN.BillDate=TEMP_FIN_PAYMENT.BillDate(+) " & vbCrLf _
            & " AND TEMP_FIN_PAYMENT.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND TEMP_FIN_PAYMENT.ACCOUNTCODE=ACM.SUPP_CUST_CODE" & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "  " & vbCrLf _
            & " AND TRN.BookType='" & mBookType & "'" & vbCrLf _
            & " AND TRN.BookSubType='" & mBookSubType & "'"

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        If mBookType = "J" Then
            mSqlStr = mSqlStr & vbCrLf & " AND TRN.AccountCode='" & mAccountCode & "'"
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND TRN.BOOKCODE='" & mBookCode & "'" & vbCrLf & " AND TRN.AccountCode<>'" & mBookCode & "'"
        End If

        mSqlStr = mSqlStr & vbCrLf _
            & " AND TRN.VNO='" & mVNo & "'" & vbCrLf _
            & " AND TRN.VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        mSqlStr = mSqlStr & vbCrLf _
            & " ORDER BY TEMP_FIN_PAYMENT.BILLDATE, TEMP_FIN_PAYMENT.BILLNO"

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, FIELD1, FIELD2, " & vbCrLf _
            & " FIELD3, FIELD4, FIELD5, FIELD6, FIELD7, FIELD8, FIELD9, FIELD10, " & vbCrLf _
            & " FIELD11, FIELD12, FIELD13, FIELD14, FIELD15, " & vbCrLf _
            & " FIELD16, FIELD17, FIELD18, " & vbCrLf _
            & " FIELD19,  FIELD20,FIELD21,FIELD22,FIELD23,FIELD24,FIELD25,FIELD26,FIELD27,FIELD28, " & vbCrLf _
            & " FIELD29,FIELD30,FIELD31,FIELD32) " & vbCrLf _
            & mSqlStr

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        'mSqlStr = MainClass.FetchFromTempData(mSqlStr, "SUBROW")
        SelectQryForAdvise = mSqlStr
        Exit Function
ErrPart:
        SelectQryForAdvise = ""
        PubDBCn.RollbackTrans()
    End Function
    Private Function ShowReport(ByRef mSqlStr As String, ByRef mMode As String, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef mNarration As String, ByRef mBankName As String, ByRef mAccountName As String, ByRef pNetPartyAmount As Double, ByRef empMailId As String, ByRef mPartyOpBal As String, ByRef mDrCrNo As String) As Boolean
        On Error GoTo ErrPart
        'Dim crapp As New CRAXDRT.Application
        'Dim objRpt As CRAXDRT.Report
        Dim fPath As String
        Dim RS As New ADODB.Recordset
        Dim mAmountInword As String
        Dim mReceivedBy As String
        Dim mVoucherAmount As Double

        Dim CrReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument  ' Report Name 
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions
        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions

        ''ByRef mMode As Crystal.DestinationConstants


        mVoucherAmount = pNetPartyAmount
        mReceivedBy = " "
        mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(CStr(mVoucherAmount)) = 0, 0, mVoucherAmount)))
        mRptFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName

        If mMode = "E" Then
            CrReport.Load(mRptFileName)
            Call Connect_MainReport_To_Database_11(CrReport)        '', mSqlStr

            ClearCRpt11Formulas(CrReport)
            CrReport.RecordSelectionFormula = "{PRINTDUMMYDATA.USERID} = '" & MainClass.AllowSingleQuote(PubUserID) & "'"


            CrReport.ReportOptions.EnableSaveDataWithReport = False
            SetCompanyReport11(CrReport, 1, mTitle, mSubTitle)
            CrReport.Refresh()


            AssignCRpt11Formulas(CrReport, "Narration", "'" & Replace(mNarration, vbCrLf, "") & "'")
            AssignCRpt11Formulas(CrReport, "BankName", "'" & mBankName & "'")
            AssignCRpt11Formulas(CrReport, "ReceivedBy", "'" & mReceivedBy & "'")
            AssignCRpt11Formulas(CrReport, "AmountInWord", "'" & mAmountInword & "'")
            AssignCRpt11Formulas(CrReport, "AmountPaid", "'" & VB6.Format(mVoucherAmount, "0.00") & "'")
            AssignCRpt11Formulas(CrReport, "CurrBal", "'" & VB6.Format(mPartyOpBal, "0.00") & "'")
            AssignCRpt11Formulas(CrReport, "DrCrNo", "'" & mDrCrNo & "'")


            fPath = mLocalPath & "\ePaymentAdvise" & VB6.Format(GetServerTime(), "hhmm") & ".pdf"

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

            If SendeMail(fPath, mAccountName, empMailId) = False Then GoTo ErrPart
        Else

            Report1.Reset()
            MainClass.ClearCRptFormulas(Report1)
            SetCrpt(Report1, Crystal.DestinationConstants.crptToWindow, 1, mTitle, mSubTitle)

            MainClass.AssignCRptFormulas(Report1, "Narration=""" & mNarration & """")
            MainClass.AssignCRptFormulas(Report1, "BankName=""" & mBankName & """")


            MainClass.AssignCRptFormulas(Report1, "AmountPaid=""" & VB6.Format(mVoucherAmount, "0.00") & """")
            MainClass.AssignCRptFormulas(Report1, "CurrBal=""" & mPartyOpBal & """")

            MainClass.AssignCRptFormulas(Report1, "DrCrNo=""" & mDrCrNo & """")

            MainClass.AssignCRptFormulas(Report1, "ReceivedBy=""" & mReceivedBy & """")
            mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(CStr(mVoucherAmount)) = 0, 0, mVoucherAmount)))
            MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")

            Report1.ReportFileName = mRptFileName
            Report1.SQLQuery = mSqlStr
            Report1.WindowShowGroupTree = False

            Report1.Action = 1
            Report1.Reset()

        End If
        ShowReport = True
        Exit Function
ErrPart:
        '   Resume
        MsgBox(Err.Description)
        ShowReport = False
    End Function
    Private Function SendeMail(ByRef mAttachmentFile As String, ByRef mAccountName As String, ByRef mTo As String) As Boolean
        On Error GoTo ErrPart
        Dim mCC As String
        Dim mFrom As String
        Dim mSubject As String
        Dim mBodyTextHeader As String
        Dim mBodyText As String
        Dim mBodyTextDetail As String
        Dim mBcc As String
        SendeMail = False
        ' *****************************************************************************
        ' This is where all of the Components Properties are set / Methods called
        ' *****************************************************************************

        mFrom = GetEMailID("MAIL_FROM") 'mFrom = GetEMailID("PUR_MAIL_TO")
        'mCC = GetEMailID("PUR_MAIL_TO")
        mCC = GetEMailID("ACCT_MAIL_TO")
        mSubject = "Payment Advice" ''Auto Generated Salary Slip for the month of " & vb6.Format(lblRunDate, "MMMM , YYYY")

        mBodyText = "<html><body><br />" _
            & "<b></b><br />" _
            & "<b></b>To " _
            & mAccountName _
            & ",<br />" _
            & "<b></b><br />" _
            & "<b></b>Dear Sir/Madam,<br />" _
            & "<b></b><br />" _
            & "<b></b>Please find the " _
            & mSubject _
            & ".<br />" _
            & "<br />" _
            & "<br />" _
            & "Your Faithfully<br />" _
            & "for " & RsCompany.Fields("Company_Name").Value _
            & "<br />" _
            & "</body></html>"

        If Trim(mTo) <> "" Then
            If SendMailProcess(mFrom, mTo, mCC, mBcc, mAttachmentFile, mSubject, mBodyText) = False Then GoTo ErrPart
        End If
        SendeMail = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        SendeMail = False
        '    Resume
    End Function
    Private Function InsertTempTable(ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String, ByRef mAccountCode As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSqlStr As String
        Dim mDrCrNo As String
        Dim mDueDate As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_FIN_PAYMENT NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        mDrCrNo = Mid(GETDRCRNo(mVNo, mVDate, mBookCode, mAccountCode, mBookType), 1, 1000)
        If frmPrintVoucher.OptReceiptWithDue.Checked = True Then
            mDueDate = "GETBILLDUEDATE(TRN.COMPANY_CODE, TRN.FYEAR, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE)"
        Else
            mDueDate = "''"
        End If
        mSqlStr = "Select '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf _
            & " TRN.COMPANY_CODE,  TRN.FYEAR, TRN.ACCOUNTCODE, " & vbCrLf _
            & " BillNo,  BillDate, " & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)) ," & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf _
            & " CASE WHEN SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount) >=0 THEn 'DR' ELSE 'CR' END, " & vbCrLf _
            & " '" & mDrCrNo & "'," & mDueDate & "" & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN " & vbCrLf _
            & " Where FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND AccountCode='" & mAccountCode & "'"

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        mSqlStr = mSqlStr & vbCrLf _
            & " GROUP BY BillNo, BillDate,COMPANY_CODE,FYEAR,ACCOUNTCODE " & vbCrLf _
            & " ORDER BY COMPANY_CODE,FYEAR , BillNo, BillDate"

        SqlStr = "INSERT INTO TEMP_FIN_PAYMENT (" & vbCrLf _
            & " USERID, COMPANY_CODE, FYEAR, ACCOUNTCODE," & vbCrLf _
            & " BillNo, BillDate, BILLAMOUNT," & vbCrLf _
            & " ADV, DNOTE, CNOTE, TDS, " & vbCrLf _
            & " PAYMENT,BALANCE, DC,DCNOTE,DUEDATE ) " & vbCrLf & mSqlStr

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        InsertTempTable = True
        Exit Function
ErrPart:
        InsertTempTable = False
        PubDBCn.RollbackTrans()
    End Function
    Private Function GETDRCRNo(ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookCode As String, ByRef lAccountCode As String, ByRef mBookType As String) As String
        On Error GoTo ErrPart
        Dim pCrNo As String
        Dim pDrNo As String
        Dim RS As ADODB.Recordset
        Dim SqlStr As String
        Dim pVType As String
        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mDrVno As String
        '        & " BookType='" & Mid(lblBookType.text, 1, 1) & "' AND " & vbCrLf _
        ''                & " BookSubType='" & Mid(lblBookType.text, 2, 1) & "' AND " & vbCrLf _
        ''& " BOOKCODE='" & mBookCode & "' AND " & vbCrLf _
        '
        pSqlStr = " SELECT BILLNO, BILLDATE FROM FIN_POSTED_TRN" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND  FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND VNO='" & mVNo & "'" & vbCrLf _
            & " AND VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        pSqlStr = pSqlStr & vbCrLf & " AND BookType='" & mBookType & "'"

        If mBookType = "B" Then
            pSqlStr = pSqlStr & vbCrLf & " AND AccountCode<>'" & mBookCode & "'"
        Else
            pSqlStr = pSqlStr & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(lAccountCode) & "'"
        End If

        pSqlStr = pSqlStr & vbCrLf & " ORDER BY BILLDATE,BILLNO"
        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                mBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")

                SqlStr = " SELECT DISTINCT TRN.VNO,VTYPE " & vbCrLf _
                    & " FROM FIN_POSTED_TRN TRN " & vbCrLf _
                    & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND TRN.BookType ='" & ConDebitNoteBook & "'" & vbCrLf _
                    & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(lAccountCode) & "' " & vbCrLf _
                    & " AND TRN.BILLNO='" & Trim(mBillNo) & "'" & vbCrLf _
                    & " AND TRN.BILLDATE =TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

                If RS.EOF = False Then
                    Do While RS.EOF = False
                        mDrVno = IIf(IsDBNull(RS.Fields("VNO").Value), "", Mid(RS.Fields("VNO").Value, 3))
                        If InStr(1, pDrNo, mDrVno) = 0 Then
                            pDrNo = IIf(pDrNo = "", "", pDrNo & ", ") & mDrVno
                        End If
                        RS.MoveNext()
                    Loop
                End If

                SqlStr = " SELECT DISTINCT TRN.VNO,VTYPE " & vbCrLf _
                    & " FROM FIN_POSTED_TRN TRN " & vbCrLf _
                    & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND TRN.BookType ='" & ConCreditNoteBook & "'" & vbCrLf _
                    & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(lAccountCode) & "' " & vbCrLf _
                    & " AND TRN.BILLNO='" & Trim(mBillNo) & "'" & vbCrLf _
                    & " AND TRN.BILLDATE =TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
                If RS.EOF = False Then
                    Do While RS.EOF = False
                        mDrVno = IIf(IsDBNull(RS.Fields("VNO").Value), "", Mid(RS.Fields("VNO").Value, 3))
                        If InStr(1, pCrNo, mDrVno) = 0 Then
                            pCrNo = IIf(pCrNo = "", "", pCrNo & ", ") & mDrVno
                        End If
                        RS.MoveNext()
                    Loop
                End If
                RsTemp.MoveNext()
            Loop
        End If

        GETDRCRNo = If(pDrNo = "", "", "DR" & pDrNo)
        If pCrNo <> "" Then
            GETDRCRNo = GETDRCRNo & ", CR" & pCrNo
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GETDRCRNo = ""
    End Function
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then
        '        txtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And STATUS='O' AND SUPP_CUST_TYPE IN ('S','C')"
        '    Select Case lblBookType.text
        '        Case ConLedger
        '            SqlStr = SqlStr
        '        Case ConCashBook
        '            SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '1'"
        '        Case ConBankBook, ConPDCBook
        '            SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '2'"
        '        Case Else
        '            SqlStr = SqlStr & " AND 1=2"
        '    End Select
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",,, SqlStr)

        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        '    Select Case lblBookType.text
        '        Case ConLedger
        '            SqlStr = ""
        '        Case ConCashBook
        '            SqlStr = "SUPP_CUST_TYPE = '1'"
        '        Case ConBankBook, ConPDCBook
        '            SqlStr = "SUPP_CUST_TYPE = '2'"
        '        Case Else
        '            SqlStr = "1=2"
        '    End Select
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprd(ByRef pGrid As AxFPSpreadADO.AxfpSpread, ByRef Arow As Integer)
        With pGrid
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 0) ' 4.5
            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookType, 15)
            .ColHidden = True

            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookSubType, 2)
            .ColHidden = True

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDate, 9)

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 11)
            .ColHidden = False

            .Col = colAccount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colAccount, 21)
            .ColHidden = False

            .Col = ColNarration
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColNarration, 21)
            .ColHidden = False

            .Col = ColChequeNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChequeNo, 8)

            .Col = ColChequeDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChequeDate, 9)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColAmount, 9)

            .Col = ColeMail
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColeMail, 21)

            .Col = ColPreviewBtn
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False
            .TypeButtonText = "Preview"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColPreviewBtn, 8)

            .Col = ColeMailBtn
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False
            .TypeButtonText = "eMail"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColeMailBtn, 8)

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            Call FillHeading(pGrid)
            MainClass.SetSpreadColor(pGrid, -1)
            MainClass.ProtectCell(pGrid, 1, .MaxRows, 1, ColeMail)
            pGrid.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            pGrid.DAutoCellTypes = True
            pGrid.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            pGrid.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function BookInfo() As Boolean
        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset = Nothing

        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mOpening As Double
        Dim mDrAmount As Double
        Dim mCrAmount As Double
        Dim SqlStrReceipt As String
        Dim SqlStrPayment As String
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim mBalance As Double
        Dim CntRow As Long

        BookInfo = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    If InsertIntoTemp = False Then GoTo LedgError

        SqlStr1 = MakeSQL
        SqlStrPayment = MakeSQLCond(False, "CB")
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr2 = " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "' "
        End If

        SqlStr2 = SqlStr2 & vbCrLf _
            & " GROUP BY TRN.VDATE," & vbCrLf _
            & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE , TRN.VNO,  " & vbCrLf _
            & " ACM.SUPP_CUST_NAME,TRN.MKEY,CHEQUENO,CHQDATE,SUPP_CUST_MAILID ,NARRATION " & vbCrLf _
            & " ORDER BY TRN.VDATE, TRN.VNO"

        SqlStr = SqlStr1 & vbCrLf & SqlStrPayment & SqlStr2


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CntRow = 1
            Do While Not RsTemp.EOF
                With SprdPayment
                    .Row = CntRow
                    .Col = ColLocked
                    .Text = IIf(IsDBNull(RsTemp.Fields("Locked").Value), "", RsTemp.Fields("Locked").Value)
                    .Col = ColBookType
                    .Text = IIf(IsDBNull(RsTemp.Fields("BookType").Value), "", RsTemp.Fields("BookType").Value)
                    .Col = ColBookSubType
                    .Text = IIf(IsDBNull(RsTemp.Fields("BOOKSUBTYPE").Value), "", RsTemp.Fields("BOOKSUBTYPE").Value)
                    .Col = ColVDate
                    .Text = IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value)
                    .Col = ColChequeNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("CHEQUENO").Value), "", RsTemp.Fields("CHEQUENO").Value)
                    .Col = ColChequeDate
                    .Text = IIf(IsDBNull(RsTemp.Fields("CHQDATE").Value), "", RsTemp.Fields("CHQDATE").Value)
                    .Col = ColVNo
                    .Text = IIf(IsDBNull(RsTemp.Fields("V_NO").Value), "", RsTemp.Fields("V_NO").Value)
                    .Col = colAccount
                    .Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    .Col = ColNarration
                    .Text = IIf(IsDBNull(RsTemp.Fields("NARRATION").Value), "", RsTemp.Fields("NARRATION").Value)
                    .Col = ColAmount
                    .Text = VB6.Format(System.Math.Abs(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)), "0.00")
                    .Col = ColeMail
                    .Text = (IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_MAILID").Value), "", RsTemp.Fields("SUPP_CUST_MAILID").Value))
                    .Col = ColMKEY
                    .Text = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
                    .MaxRows = .MaxRows + 1
                    CntRow = CntRow + 1
                End With

                RsTemp.MoveNext()
            Loop
        End If

        BookInfo = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
LedgError:
        BookInfo = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function InsertIntoTemp() As Boolean
        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim mSqlStr As String
        InsertIntoTemp = False
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = "DELETE FROM Temp_ViewBook NOLOGGING " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr1 = "SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "',TRN.BOOKTYPE,TRN.MKEY"
        SqlStr2 = MakeSQLCond(False, "")
        SqlStr = SqlStr1 & vbCrLf & SqlStr2
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If
        mSqlStr = ""
        mSqlStr = "INSERT INTO Temp_ViewBook (" & vbCrLf & " USERID, BOOKTYPE, MKEY) "
        mSqlStr = mSqlStr & vbCrLf & SqlStr
        PubDBCn.Execute(mSqlStr)
        PubDBCn.CommitTrans()
        InsertIntoTemp = True
        Exit Function
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        InsertIntoTemp = False
        PubDBCn.RollbackTrans()
    End Function
    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = " SELECT '' AS LOCKED, TRN.BOOKTYPE, TRN.BOOKSUBTYPE , " & vbCrLf _
            & "  TO_CHAR(TRN.VDATE,'DD/MM/YYYY') AS VDATE,CHEQUENO  AS CHEQUENO,CHQDATE,TRN.VNO AS V_NO, SUPP_CUST_MAILID,NARRATION," & vbCrLf _
            & " ACM.SUPP_CUST_NAME, "

        SqlStr = SqlStr & vbCrLf & " SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) AS AMOUNT, "
        SqlStr = SqlStr & vbCrLf & " '',TRN.MKEY "
        MakeSQL = SqlStr

        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function
    Private Function MakeOPSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = " SELECT " & vbCrLf & " SUM(DECODE(TRN.DC,'D',1,-1) * TRN.AMOUNT)  AS OPENING "
        MakeOPSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeOPSQL = ""
    End Function
    Private Function MakeSQLCond(ByRef mIsOpening As Boolean, ByRef mBookView As String) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCostCName As String
        Dim mDeptName As String
        Dim mEmp As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        If CboCC.Text = "ALL" Then
            mCostCName = ""
        Else
            mCostCName = MainClass.AllowSingleQuote(CboCC.Text)
        End If
        If CboDept.Text = "ALL" Then
            mDeptName = ""
        Else
            mDeptName = MainClass.AllowSingleQuote(CboDept.Text)
        End If
        If cboEmp.Text = "ALL" Then
            mEmp = ""
        Else
            mEmp = MainClass.AllowSingleQuote(cboEmp.Text)
        End If
        SqlStr = " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM"

        SqlStr = SqlStr & vbCrLf _
            & " WHERE  " & vbCrLf _
            & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND TRN.Company_Code=ACM.Company_Code " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "


        ''& " TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _

        SqlStr = SqlStr & vbCrLf _
            & " AND ACM.SUPP_CUST_TYPE IN ('S','C') " & vbCrLf _
            & " AND TRN.BOOKTYPE IN ('" & VB.Left(ConBankPayment, 1) & "', '" & VB.Left(ConJournal, 1) & "') " & vbCrLf _
            & " AND TRN.BOOKSUBTYPE IN ('" & VB.Right(ConBankPayment, 1) & "', '" & VB.Right(ConJournal, 1) & "')"

        '    If mIsOpening = True Then
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND TRN.Vdate<'" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "' "
        '    Else
        SqlStr = SqlStr _
            & " AND TRN.Vdate BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        '    End If
        MakeSQLCond = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLCond = ""
    End Function
    Private Function GetGroupOption() As String
        On Error GoTo ErrPart
        GetGroupOption = ""
        If chkGroup(0).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConBankBook & "'"
        End If
        If chkGroup(1).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConCashBook & "'"
        End If
        If chkGroup(2).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConSaleBook & "'  OR TRN.BOOKTYPE = '" & ConSaleDebitBook & "'"
        End If
        If chkGroup(3).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConPurchaseBook & "' OR TRN.BookType = '" & ConGRBook & "'"
        End If
        If chkGroup(4).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConDebitNoteBook & "'"
        End If
        If chkGroup(5).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConCreditNoteBook & "'"
        End If
        If chkGroup(6).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConJournalBook & "'"
        End If
        If chkGroup(7).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConContraBook & "'"
        End If
        If chkGroup(8).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConPDCBook & "'"
        End If
        Exit Function
ErrPart:
        GetGroupOption = ""
        MsgBox(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Please Select Supplier Name.")
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub FillHeading(ByRef pGrid As Object)
        On Error GoTo ErrPart
        With pGrid
            .Row = 0
            .Col = ColAmount
            .Text = "Amount (Rs.)"
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateTo.Text)) = False Then
        '        txtDateTo.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
