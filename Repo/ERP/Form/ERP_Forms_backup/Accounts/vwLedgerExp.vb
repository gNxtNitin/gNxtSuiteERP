Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewLedgerExp
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColBookType As Short = 2
    Private Const ColBookSubType As Short = 3
    Private Const ColVDate As Short = 4
    Private Const ColVNo As Short = 5
    Private Const ColNarration As Short = 6
    Private Const ColDAmount As Short = 7
    Private Const ColCAmount As Short = 8
    Private Const ColBalance As Short = 9
    Private Const ColBalDC As Short = 10
    Private Const ColBillDetail As Short = 11
    Private Const ColChequeNo As Short = 12
    Private Const ColDept As Short = 13
    Private Const ColEmp As Short = 14
    Private Const ColCostC As Short = 15
    Private Const ColMKEY As Short = 16
    Private Const ColSubRowNo As Short = 17
    Private Const ColBranch As Short = 18
    Private Const mPageWidth As Short = 232
    Private Const TabRefDate As Short = 0
    Private Const TabRefNo As Short = 15
    Private Const TabName As Short = 30
    Private Const TabDAmount As Short = 166
    Private Const TabCAmount As Short = 188
    Private Const TabBalance As Short = 210
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub GetFormCaption(ByRef mBookName As String)
        Select Case mBookName
            Case ConCashBook
                Me.Text = "Cash Book"
            Case ConBankBook
                Me.Text = "Bank Book"
            Case ConPDCBook
                Me.Text = "PDC Book"
            Case ConJournalBook
                Me.Text = "Journal Book"
            Case ConContraBook
                Me.Text = "Contra Book"
            Case ConDebitNoteBook
                Me.Text = "Debit Note Book"
            Case ConCreditNoteBook
                Me.Text = "Credit Note Book"
            Case ConLedger
                Me.Text = "Ledger (Expense)"
        End Select
    End Sub
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
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        PrintStatus(False)
    End Sub
    Private Sub cboEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboEmp.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboEmp_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboEmp.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboExpHead_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExpHead.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboExpHead_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExpHead.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub chkAllAccount_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllAccount.CheckStateChanged
        Call PrintStatus(False)
        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAgtAccount.Enabled = False
            cmdAgtsearch.Enabled = False
        Else
            TxtAgtAccount.Enabled = True
            cmdAgtsearch.Enabled = True
        End If
    End Sub
    Private Sub chkGroup_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGroup.CheckStateChanged
        Dim Index As Short = chkGroup.GetIndex(eventSender)
        Call PrintStatus(False)
    End Sub
    Private Sub ChkWithRunBal_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkWithRunBal.CheckStateChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cmdAgtsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAgtsearch.Click
        Call SearchAccounts(TxtAgtAccount)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub cmdOptional_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOptional.Click
        FraOthers.Visible = Not FraOthers.Visible
        FraOthers.BringToFront()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        On Error GoTo ERR1
        Dim All As Boolean
        Dim PrintStatus As Boolean
        '    Dim x As Boolean
        '    x = SprdLedg.ExportToHTML("C:\FILE.HTML", False, "C:\LOGFILE.TXT")
        '    If x = True Then
        '        MsgBox ("Export complete.")
        '    Else
        '        MsgBox ("Export did not succeed.")
        '    End If
        '   Exit Sub
        If TxtAccount.Text = "" Then PrintStatus = False Else PrintStatus = True
        frmPrintLedg.OptSelected.Enabled = PrintStatus
        If OptSumDet(1).Checked = True Or OptSumDet(2).Checked = True Then
            frmPrintLedg.fraPrintOption.Enabled = False
        Else
            frmPrintLedg.fraPrintOption.Enabled = True
        End If
        frmPrintLedg.ShowDialog()
        If G_PrintLedg = False Then
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForLedger(Crystal.DestinationConstants.crptToWindow, PubDBCn)
        frmPrintLedg.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        frmPrintLedg.Close()
    End Sub
    Private Sub ReportForLedger(ByRef Mode As Crystal.DestinationConstants, ByRef pDBCn As ADODB.Connection)
        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String
        pDBCn.Errors.Clear()
        SqlStr = "DELETE FROM Temp_Ledger NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        pDBCn.Execute(SqlStr)
        SqlStr = ""
        If frmPrintLedg.OptSelected.Checked Then
            Call InsertSelectedAcct(pDBCn)
        ElseIf frmPrintLedg.OptAll.Checked Then
            Call InsertAllLederAcct(pDBCn, True, "ALL")
        ElseIf frmPrintLedg.OptGroup.Checked Then
            Call InsertAllLederAcct(pDBCn, False, "ALL")
        ElseIf frmPrintLedg.OptExpenses.Checked Then
            Call InsertAllLederAcct(pDBCn, False, "E")
        ElseIf frmPrintLedg.OptGeneral.Checked Then
            Call InsertAllLederAcct(pDBCn, False, "G")
        ElseIf frmPrintLedg.OptDebtors.Checked Then
            Call InsertAllLederAcct(pDBCn, False, "D")
        ElseIf frmPrintLedg.OptCreditors.Checked Then
            Call InsertAllLederAcct(pDBCn, False, "C")
        End If
        'Select Record for print...
        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        If mDOSPRINTING = True Then
            If Mode = Crystal.DestinationConstants.crptToWindow Then
                Call LedgerReport("V", SqlStr, pDBCn)
            Else
                Call LedgerReport("P", SqlStr, pDBCn)
            End If
        Else
            mTitle = "Account Ledger"
            If OptSumDet(1).Checked = True Then
                mTitle = mTitle & " - Daily"
            ElseIf OptSumDet(2).Checked = True Then
                mTitle = mTitle & " - Monthly"
            End If
            If frmPrintLedg.OptGroup.Checked Then
                mTitle = mTitle & "  (" & frmPrintLedg.txtLedgerGroup.Text & ")"
            End If
            mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")
            If OptSumDet(0).Checked = True Then
                If frmPrintLedg.chkWideFormat.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mReportFileName = "Ledger.Rpt"
                Else
                    mReportFileName = "Ledger_80.Rpt"
                End If
            Else
                mReportFileName = "LedgerSummary.Rpt"
            End If
            Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
        End If
        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        '    Resume
    End Sub
    Private Sub InsertSelectedAcct(ByRef pDBCn As ADODB.Connection)
        Dim mVdate As String
        Dim mVNo As String
        Dim mAcctName As String
        Dim mNarration As String
        Dim mBillDetail As String
        Dim mChequeNo As String
        Dim mDAmt As Double
        Dim mCAmt As Double
        Dim mOpening As Double
        Dim mClosing As String
        Dim mPartyName As String
        Dim mRunningBal As Double
        Dim mRunningBalTot As String
        Dim mCostC As String
        Dim SqlStr As String
        Dim cntRow As Integer
        On Error GoTo ERR1
        pDBCn.Errors.Clear()
        pDBCn.BeginTrans()
        mPartyName = TxtAccount.Text
        '    mOpening = MainClass.FormatRupees(CDbl(lblDOpBal) - CDbl(lblCOpBal))
        '
        '    mClosing = CDbl(lblDClBal) - CDbl(lblCClBal)
        '    mClosing = MainClass.FormatRupees(Abs(mClosing)) & IIf(mClosing >= 0, "Dr", "Cr")
        '
        '    mRunningBal = mOpening
        SqlStr = ""
        With SprdLedg
            '        SqlStr = "Insert into PrintDummyData (UserID,SubRow,Field1,Field8,Field5,Field6,Field9) Values (" & vbCrLf _
            ''                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',1, " & vbCrLf _
            ''                & " '" & MainClass.AllowSingleQuote(Trim(mPartyName)) & "', " & vbCrLf _
            ''                & " 'Opening Balance :', " & vbCrLf _
            ''                & " CASE WHEN " & mOpening & " >= 0 THEN '" & Trim(mOpening) & "' ELSE '0' END," & vbCrLf _
            ''                & " CASE WHEN " & mOpening & " < 0 THEN '" & Abs(Trim(mOpening)) & "' ELSE '0' END,'')"
            '
            '        pDBCn.Execute SqlStr
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColVDate
                mVdate = .Text
                .Col = ColVNo
                mVNo = .Text
                '            .Col = ColAcctName
                '            If frmPrintLedg.chkPrintOption(3).Value = vbChecked Then
                '                mAcctName = Left(.Text, 250)
                '            Else
                '                mAcctName = ""
                '            End If
                .Col = ColNarration
                If frmPrintLedg.chkPrintOption(1).CheckState = System.Windows.Forms.CheckState.Checked Then
                    mNarration = VB.Left(.Text, IIf(mDOSPRINTING = True, 250, 250))
                Else
                    mNarration = ""
                End If
                .Col = ColChequeNo
                If frmPrintLedg.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Checked Then
                    mChequeNo = IIf(Trim(.Text) = "", "", "Chq. No. & Date : ") & .Text
                Else
                    mChequeNo = ""
                End If
                .Col = ColDAmount
                If IsNumeric(.Text) Then
                    mDAmt = CDbl(.Text)
                Else
                    mDAmt = 0
                End If
                .Col = ColCAmount
                If IsNumeric(.Text) Then
                    mCAmt = CDbl(.Text)
                Else
                    mCAmt = 0
                End If
                mRunningBal = mRunningBal + (mDAmt - mCAmt)
                mRunningBalTot = MainClass.FormatRupees(System.Math.Abs(mRunningBal)) & IIf(mRunningBal >= 0, "Dr", "Cr")
                .Col = ColCostC
                mCostC = Trim(.Text)
                .Col = ColBillDetail
                If frmPrintLedg.chkPrintOption(7).CheckState = System.Windows.Forms.CheckState.Checked Then
                    mBillDetail = VB.Left(.Text, IIf(mDOSPRINTING = True, 250, 250))
                Else
                    mBillDetail = ""
                End If
                SqlStr = "Insert into Temp_Ledger ( " & vbCrLf & " UserID,SubRow,PARTYNAME,VDATE, " & vbCrLf & " VNO,NARRATION,DAmount,CAmount, " & vbCrLf & " CHQNO,ACCOUNTNAME,COSTCNAME,BILLDETAIL " & vbCrLf & " ) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mPartyName)) & "', " & vbCrLf & " '" & Trim(mVdate) & "', " & vbCrLf & " '" & Trim(mVNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mNarration)) & "', " & vbCrLf & " " & Val(CStr(mDAmt)) & ", " & vbCrLf & " " & Val(CStr(mCAmt)) & ",'" & mChequeNo & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mAcctName)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mCostC)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mBillDetail)) & "')"
                pDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        pDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume
        pDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub InsertAllLederAcct(ByRef pDBCn As ADODB.Connection, ByRef pAllAccount As Boolean, ByRef pGroupType As String)
        On Error GoTo LedgError
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim SqlStr3 As String
        Dim SqlStr As String
        Dim InsertSqlStr As String
        Dim mGroupOption As String
        Dim RS As ADODB.Recordset
        Dim mCntCount As Integer
        Dim mAccountName As String
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim RsOP As ADODB.Recordset
        pDBCn.Errors.Clear()
        pDBCn.BeginTrans()
        SqlStr1 = ""
        SqlStr2 = ""
        SqlStr3 = ""
        SqlStr = ""
        InsertSqlStr = ""
        lblPrintCount.Visible = True
        lblPrintCount.Text = ""
        If pAllAccount = True Then
            SqlStr = "SELECT SUPP_CUST_CODE,SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE FIN_SUPP_CUST_MST.COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " And FIN_SUPP_CUST_MST.SUPP_CUST_TYPE NOT IN ('1','2') ORDER BY SUPP_CUST_NAME"
        Else
            SqlStr = "SELECT SUPP_CUST_CODE,SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST,FIN_GROUP_MST" & vbCrLf & " WHERE FIN_SUPP_CUST_MST.COMPANY_CODE=FIN_GROUP_MST.COMPANY_CODE" & vbCrLf & " AND FIN_SUPP_CUST_MST.GROUPCODE=FIN_GROUP_MST.GROUP_CODE" & vbCrLf & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " And FIN_SUPP_CUST_MST.SUPP_CUST_TYPE NOT IN ('1','2')"
            If pGroupType <> "ALL" Then
                SqlStr = SqlStr & vbCrLf & " AND GROUP_TYPE='" & pGroupType & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND FIN_GROUP_MST.GROUP_NAME='" & MainClass.AllowSingleQuote(UCase(frmPrintLedg.txtLedgerGroup.Text)) & "'"
            End If
            SqlStr = SqlStr & vbCrLf & " ORDER BY SUPP_CUST_NAME"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While Not RS.EOF
                mAccountCode = IIf(IsDbNull(RS.Fields("SUPP_CUST_CODE").Value), "-1", RS.Fields("SUPP_CUST_CODE").Value)
                mAccountName = IIf(IsDbNull(RS.Fields("SUPP_CUST_NAME").Value), "-1", RS.Fields("SUPP_CUST_NAME").Value)
                mCntCount = mCntCount + 1
                lblPrintCount.Text = mCntCount & " - " & Trim(Replace(mAccountName, "&", "&&"))
                System.Windows.Forms.Application.DoEvents()
                'Get Opening Balance.........
                SqlStr = MakeOPSQL(mAccountCode)
                '            SqlStr2 = MakeSQLCondInsert(True)
                '            SqlStr = SqlStr1 & vbCrLf & SqlStr2 & vbCrLf _
                ''                    & " AND ACCOUNTCODE=" & mAccountCode & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOP, ADODB.LockTypeEnum.adLockReadOnly)
                If RsOP.EOF = False Then
                    mOpening = IIf(IsDbNull(RsOP.Fields("OPENING").Value), 0, RsOP.Fields("OPENING").Value)
                    If mOpening >= 0 Then
                        mOpDr = mOpening
                        mOpCr = 0
                    Else
                        mOpDr = 0
                        mOpCr = System.Math.Abs(mOpening)
                    End If
                End If
                If mOpening <> 0 Then
                    InsertSqlStr = " Insert into Temp_Ledger (UserID,SubRow,PARTYNAME, " & vbCrLf & " NARRATION,DAmount,CAmount) VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',1, " & vbCrLf & " '" & MainClass.AllowSingleQuote(mAccountName) & "','OPENING :' , " & vbCrLf & " " & Val(CStr(mOpDr)) & ", " & vbCrLf & " " & Val(CStr(mOpCr)) & ") "
                    pDBCn.Execute(InsertSqlStr)
                End If
                'Get Detail.........
                SqlStr1 = MakeSQLForInsert(mAccountName)
                SqlStr2 = MakeSQLCondInsert(False)
                SqlStr = SqlStr1 & vbCrLf & SqlStr2
                SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
                ''            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE<>'" & mAccountCode & "' " & vbCrLf _
                '                        & " AND TRN.BOOKTYPE ||TRN.MKEY IN (SELECT TRN.BOOKTYPE || TRN.MKEY " & SqlStr2 & vbCrLf _
                '                        & " AND ACCOUNTCODE='" & mAccountCode & "')"
                ''& " CASE WHEN BOOKCODE='-3' THEN GETACCOUNTNAMEFROMTRN(TRN.MKEY,TRN.COMPANY_CODE,TRN.FYEAR,TRN.BOOKTYPE , TRN.BOOKSUBTYPE,TRN.ACCOUNTCODE)|| ' - ' ELSE '' END || TRN.NARRATION AS NARRATION," & vbCrLf _
                '& " CASE WHEN BOOKCODE='-3' THEN ACM.SUPP_CUST_NAME || ' - ' ELSE '' END || TRN.NARRATION, " & vbCrLf _
                '
                If OptSumDet(0).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " GROUP BY CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.EXPDATE),'YYYYMMDD') ELSE TO_CHAR(EXPDATE,'YYYYMMDD') END," & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.EXPDATE),'DD/MM/YYYY') ELSE TO_CHAR(TRN.EXPDATE,'DD/MM/YYYY') END," & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_DATE(LAST_DAY(TRN.EXPDATE),'DD/MM/YYYY') ELSE TO_DATE(TRN.EXPDATE,'DD/MM/YYYY') END, " & vbCrLf & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE , " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.EXPDATE,'MONYYYY') ELSE TRN.VNO END,  " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN DECODE(BOOKCODE," & ConSalesBookCode & ",'Sales Month of :','Purchase Month of :') || TO_CHAR(TRN.EXPDATE,'MONYYYY') ELSE DECODE(TRN.NARRATION,NULL,'',TRN.NARRATION) END," & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '' ELSE REPLACE(TRN.REMARKS, chr(13)||chr(10),' ') END, " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '-1' ELSE TRN.MKEY END," & vbCrLf & " CHEQUENO || ' ' || TO_CHAR(CHQDATE,'DD/MM/YYYY')" & vbCrLf & " ORDER BY " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_DATE(LAST_DAY(TRN.EXPDATE),'DD/MM/YYYY') ELSE TO_DATE(TRN.EXPDATE,'DD/MM/YYYY') END, " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.EXPDATE,'MONYYYY') ELSE TRN.VNO END"
                ElseIf OptSumDet(1).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.EXPDATE" & vbCrLf & " ORDER BY TRN.EXPDATE"
                ElseIf OptSumDet(2).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " GROUP BY SUBSTR(EXPDATE,4,3),TO_CHAR(EXPDATE,'YYYYMM')" & vbCrLf & " ORDER BY TO_CHAR(EXPDATE,'YYYYMM')"
                End If
                InsertSqlStr = " Insert into Temp_Ledger ( " & vbCrLf & " UserID,SubRow,PARTYNAME,VDATE, " & vbCrLf & " VNO,NARRATION,DAmount,CAmount,CHQNO, " & vbCrLf & " ACCOUNTNAME,COSTCNAME,BILLDETAIL) " & vbCrLf & SqlStr
                pDBCn.Execute(InsertSqlStr)
                RS.MoveNext()
            Loop
        End If
        pDBCn.CommitTrans()
        lblPrintCount.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        '    Resume
        MsgInformation(Err.Description)
        pDBCn.RollbackTrans()
        lblPrintCount.Visible = False
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_Ledger " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY PARTYNAME, SUBROW "
        FetchRecordForReport = mSqlStr
    End Function
    Private Function LedgerReport(ByRef pPrintMode As String, ByRef pSqlStr As String, ByRef pDBCn As ADODB.Connection) As Boolean
        On Error GoTo ErrPart
        Dim mRsTemp As ADODB.Recordset
        Dim mLineCount As Integer
        Dim pPageNo As Integer
        Dim cntRow As Double
        Dim mPrintFooter As Boolean
        Dim pFileName As String
        Dim mFP As Boolean
        Dim pBalance As Double
        Dim pTotDAmount As Double
        Dim pTotCAmount As Double
        Dim mPartyName As String
        MainClass.UOpenRecordSet(pSqlStr, pDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If mRsTemp.EOF = True Then
            MsgInformation("Nothing to print)")
            LedgerReport = True
            Exit Function
        End If
        mLineCount = 1
        pFileName = mLocalPath & "\Report.Prn"
        ''Shell "ATTRIB +A -R " & pFileName
        Call ShellAndContinue("ATTRIB +A -R " & pFileName)
        With mRsTemp
            If .EOF = False Then
                FileOpen(1, pFileName, OpenMode.Output)
                Do While Not .EOF
                    '                If (mPartyName <> !PARTYNAME Or mPartyName = "") Then
                    '                    mLineCount = 1
                    '                End If
                    If mLineCount = 1 Then
                        pPageNo = pPageNo + 1
                        Call PrintHeader(.Fields("PARTYNAME").Value)
                        mLineCount = 12
                        mPrintFooter = False
                    End If
                    pTotDAmount = pTotDAmount + IIf(IsDbNull(.Fields("DAmount").Value), 0, .Fields("DAmount").Value)
                    pTotCAmount = pTotCAmount + IIf(IsDbNull(.Fields("CAmount").Value), 0, .Fields("CAmount").Value)
                    pBalance = pTotDAmount - pTotCAmount
                    Call PrintDetail(cntRow, mLineCount, IIf(IsDbNull(.Fields("VDATE").Value), "", .Fields("VDATE").Value), IIf(IsDbNull(.Fields("VNO").Value), "", .Fields("VNO").Value), IIf(IsDbNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value), IIf(IsDbNull(.Fields("BILLDETAIL").Value), "", .Fields("BILLDETAIL").Value), IIf(IsDbNull(.Fields("CHQNO").Value), "", .Fields("CHQNO").Value), IIf(IsDbNull(.Fields("DAmount").Value), 0, .Fields("DAmount").Value), IIf(IsDbNull(.Fields("CAmount").Value), 0, .Fields("CAmount").Value), pBalance)
                    mPartyName = .Fields("PARTYNAME").Value
                    .MoveNext()
                    '                If !PARTYNAME <> "DAVY AIR SERVICES" Then
                    '                    MsgBox "ok"
                    '                End If
                    If mLineCount >= 63 And mPrintFooter = False Then
                        Call PrintFooter(pPageNo, mLineCount, mPrintFooter)
                        If mPartyName <> .Fields("PARTYNAME").Value Then
                            pBalance = 0
                            pTotDAmount = 0
                            pTotCAmount = 0
                        End If
                    Else
                        If .EOF = True Then
                            Do While mLineCount <= 63
                                PrintLine(1, " ")
                                mLineCount = mLineCount + 1
                            Loop
                            SprdLedg.Row = SprdLedg.MaxRows
                            PrintLine(1, TAB(0), New String("-", mPageWidth))
                            mLineCount = mLineCount + 1
                            Print(1, TAB(TabDAmount), New String(" ", TabCAmount - TabDAmount - 1 - Len(Trim(VB6.Format(pTotDAmount, "0.00")))) & Trim(VB6.Format(pTotDAmount, "0.00")))
                            PrintLine(1, TAB(TabCAmount), New String(" ", TabBalance - TabCAmount - 1 - Len(Trim(VB6.Format(pTotCAmount, "0.00")))) & Trim(VB6.Format(pTotCAmount, "0.00")))
                            Call PrintFooter(pPageNo, mLineCount, mPrintFooter)
                            pBalance = 0
                            pTotDAmount = 0
                            pTotCAmount = 0
                        Else
                            If mPartyName <> .Fields("PARTYNAME").Value Then
                                Do While mLineCount <= 63
                                    PrintLine(1, " ")
                                    mLineCount = mLineCount + 1
                                Loop
                                SprdLedg.Row = SprdLedg.MaxRows
                                PrintLine(1, TAB(0), New String("-", mPageWidth))
                                mLineCount = mLineCount + 1
                                Print(1, TAB(TabDAmount), New String(" ", TabCAmount - TabDAmount - 1 - Len(Trim(VB6.Format(pTotDAmount, "0.00")))) & Trim(VB6.Format(pTotDAmount, "0.00")))
                                PrintLine(1, TAB(TabCAmount), New String(" ", TabBalance - TabCAmount - 1 - Len(Trim(VB6.Format(pTotCAmount, "0.00")))) & Trim(VB6.Format(pTotCAmount, "0.00")))
                                Call PrintFooter(pPageNo, mLineCount, mPrintFooter)
                                pBalance = 0
                                pTotDAmount = 0
                                pTotCAmount = 0
                            End If
                        End If
                    End If
                Loop
                FileClose(1)
            End If
        End With
        If pPrintMode = "P" Then
            mFP = Shell(My.Application.Info.DirectoryPath & "\PrintReport.bat", AppWinStyle.NormalFocus)
            If mFP = False Then GoTo ErrPart
            '        Shell App.path & "\PrintReport.bat",vbNormalFocus
        Else
            Shell("ATTRIB +R -A " & pFileName)
            Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
            'App.Path & "\RVIEW.EXE "
        End If
        LedgerReport = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        LedgerReport = False
        ''Resume
        FileClose(1)
    End Function
    Private Function PrintFooter(ByRef xPageNo As Integer, ByRef mLineCount As Integer, ByRef pPrintFooter As Boolean) As Boolean
        On Error GoTo ErrPart
        Do While mLineCount <= 65
            PrintLine(1, " ")
            mLineCount = mLineCount + 1
        Loop
        PrintLine(1, TAB(0), New String("-", mPageWidth))
        Print(1, TAB(TabCAmount), VB6.Format(RunDate, "DD/MM/YYYY"))
        PrintLine(1, TAB(TabBalance), "Page No. : " & xPageNo)
        PrintLine(1, TAB(0), Chr(10) & Chr(12))
        mLineCount = 1
        PrintFooter = True
        pPrintFooter = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintFooter = False
        pPrintFooter = False
    End Function
    Private Function PrintDetail(ByRef mRow As Double, ByRef mLineCount As Integer, ByRef mVdate As String, ByRef mVNo As String, ByRef mNarration As String, ByRef mBillDetail As String, ByRef mChequeNo As String, ByRef mDAmount As Double, ByRef mCAmount As Double, ByRef mBalance As Double) As Boolean
        On Error GoTo ErrPart
        Dim mRemarks As String
        Dim mBalanceStr As String
        Dim mDAmountStr As String
        Dim mCAmountStr As String
        mNarration = Replace(Trim(mNarration), vbCrLf, " ")
        mBillDetail = Replace(Trim(mBillDetail), vbCrLf, " ")
        Print(1, TAB(TabRefDate), Trim(mVdate))
        Print(1, TAB(TabRefNo), Trim(mVNo))
        If frmPrintLedg.chkPrintOption(1).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRemarks = mRemarks & IIf(Trim(mNarration) = "", "", IIf(mRemarks = "", "", " ") & Trim(mNarration))
        End If
        If frmPrintLedg.chkPrintOption(7).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRemarks = mRemarks & IIf(Trim(mBillDetail) = "", "", IIf(mRemarks = "", "", " ") & Trim(mBillDetail))
        End If
        If frmPrintLedg.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Checked Then
            mRemarks = mRemarks & IIf(Trim(mChequeNo) = "", "", IIf(mRemarks = "", "", " ") & Trim(mChequeNo))
        End If
        mRemarks = Replace(mRemarks, Chr(13), " ")
        mRemarks = GetMultiLine(Trim(mRemarks), mLineCount, TabDAmount - TabName, TabName)
        Print(1, TAB(TabName), Trim(mRemarks))
        mDAmountStr = IIf(mDAmount = 0, "", VB6.Format(mDAmount, "0.00"))
        Print(1, TAB(TabDAmount), New String(" ", TabCAmount - TabDAmount - 1 - Len(mDAmountStr)) & mDAmountStr)
        mCAmountStr = IIf(mCAmount = 0, "", VB6.Format(mCAmount, "0.00"))
        Print(1, TAB(TabCAmount), New String(" ", TabBalance - TabCAmount - 1 - Len(mCAmountStr)) & mCAmountStr)
        mBalanceStr = VB6.Format(System.Math.Abs(mBalance), "0.00") & IIf(mBalance >= 0, "Dr", "Cr")
        PrintLine(1, TAB(TabBalance), New String(" ", mPageWidth - TabBalance - 1 - Len(Trim(mBalanceStr))) & Trim(mBalanceStr))
        mLineCount = mLineCount + 1
        PrintLine(1, " ")
        mLineCount = mLineCount + 1
        PrintDetail = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintDetail = False
        '    Resume
    End Function
    Private Function PrintHeader(ByRef mPartyName As String) As Boolean
        On Error GoTo ErrPart
        Dim mTitle As String
        PrintLine(1, TAB(0), " ")
        If frmPrintLedg.chkWideFormat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            PrintLine(1, TAB(0), Chr(15))
        Else
            PrintLine(1, TAB(0), " ")
        End If
        PrintLine(1, TAB(0), Chr(14) & RsCompany.Fields("COMPANY_NAME").Value)
        PrintLine(1, TAB(0), " ") ''xCompanyAddr
        mTitle = "Ledger / Statement of Account Of : " & UCase(mPartyName)
        PrintLine(1, TAB(0), Chr(27) & Chr(69) & mTitle & Chr(27) & Chr(70))
        PrintLine(1, TAB(0), Chr(27) & Chr(69) & "For the period : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & "-" & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & Chr(27) & Chr(70))
        PrintLine(1, TAB(0), New String("-", mPageWidth))
        Print(1, TAB(TabRefDate), "Date")
        Print(1, TAB(TabRefNo), "No.")
        Print(1, TAB(TabName), "Particulars")
        Print(1, TAB(TabDAmount), New String(" ", TabCAmount - TabDAmount - 1 - Len("Debit (Rs)")) & "Debit (Rs)")
        Print(1, TAB(TabCAmount), New String(" ", TabBalance - TabCAmount - 1 - Len("Credit (Rs)")) & "Credit (Rs)")
        PrintLine(1, TAB(TabBalance), New String(" ", mPageWidth - TabBalance - 1 - Len("Balance (Rs)")) & "Balance (Rs)")
        PrintLine(1, TAB(0), New String("-", mPageWidth))
        PrintHeader = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintHeader = False
        '' Resume
    End Function
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        On Error GoTo ERR1
        Dim All As Boolean
        Dim PrintStatus As Boolean
        If TxtAccount.Text = "" Then PrintStatus = False Else PrintStatus = True
        frmPrintLedg.OptSelected.Enabled = PrintStatus
        If OptSumDet(1).Checked = True Or OptSumDet(2).Checked = True Then
            frmPrintLedg.fraPrintOption.Enabled = False
        Else
            frmPrintLedg.fraPrintOption.Enabled = True
        End If
        frmPrintLedg.ShowDialog()
        If G_PrintLedg = False Then
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForLedger(Crystal.DestinationConstants.crptToPrinter, PubDBCn)
        frmPrintLedg.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        frmPrintLedg.Close()
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Call SearchAccounts(TxtAccount)
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdLedg, RowHeight)
        If LedgInfo = False Then GoTo ErrPart
        SprdLedg.Focus()
        Call PrintStatus(True)
        '    FraOthers.Visible = False
        MainClass.SetFocusToCell(SprdLedg, mActiveRow, 4)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub DisplayTotals(ByRef pOpeningDr As Double, ByRef pOpeningCr As Double)
        On Error GoTo ERR1
        Dim cntCol As Integer
        Dim mDebit As Double
        Dim mCredit As Double
        Dim mBalance As Double
        Dim mDC As String
        With SprdLedg
            .MaxRows = .MaxRows + 1
            .Row = 1
            .Action = SS_ACTION_INSERT_ROW
            .Col = ColNarration
            .Row = 1
            .Text = "OPENING : "
            .Font = VB6.FontChangeBold(.Font, True)
            'FormatSprdLedg -1
            .Col = ColDAmount
            .Text = VB6.Format(pOpeningDr, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColCAmount
            .Text = VB6.Format(pOpeningCr, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColBalance
            .Text = "0.00"
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColBalDC
            .Text = "Dr"
            .Font = VB6.FontChangeBold(.Font, True)
            .Row = 1
            .Row2 = 1
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False
            Call MainClass.AddBlankfpSprdRow(SprdLedg, ColVDate)
            'FormatSprdLedg -1
            '        .MaxRows = .MaxRows + 1
            '        .Row = .MaxRows
            '        .Action = SS_ACTION_INSERT_ROW
            .Col = ColNarration
            .Row = .MaxRows
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)
            'FormatSprdLedg -1
            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '' &H8000000B             ''&H80FF80
            .BlockMode = False
            '        Call CalcRowTotal(SprdLedg, ColDAmount, 1, ColDAmount, .MaxRows - 1, .MaxRows, ColDAmount)
            '        Call CalcRowTotal(SprdLedg, ColCAmount, 1, ColCAmount, .MaxRows - 1, .MaxRows, ColCAmount)
            '
            .Row = .MaxRows
            .Col = ColDAmount
            mDebit = Val(.Text)
            .Col = ColCAmount
            mCredit = Val(.Text)
            mBalance = mDebit - mCredit
            .Col = ColBalance
            .Text = Str(System.Math.Abs(mBalance))
            .Font = VB6.FontChangeBold(.Font, True)
            .Col = ColBalDC
            .Text = IIf(mBalance >= 0, "DR", "CR")
            .Font = VB6.FontChangeBold(.Font, True)
            FormatSprdLedg(-1)
        End With
        Call FillRunBalCol()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Public Sub frmViewLedgerExp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim I As Integer
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call GetFormCaption((lblBookType.Text))
        TxtAccount.Visible = True
        If lblBookType.Text = ConLedger Then
            FraAccount.Text = "Accounts"
            chkGroup(8).CheckState = System.Windows.Forms.CheckState.Unchecked
            chkGroup(8).Enabled = False
        Else
            FraAccount.Text = "PCD Book"
            chkGroup(8).CheckState = System.Windows.Forms.CheckState.Checked
            chkGroup(8).Enabled = False
            For I = 0 To 7
                chkGroup(I).CheckState = System.Windows.Forms.CheckState.Unchecked
                chkGroup(I).Enabled = False
            Next
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewLedgerExp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        Call FillComboBox()
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        OptSumDet(0).Checked = True
        FraOthers.Visible = False
        ' Move the rows or columns with the scroll box
        SprdLedg.ScrollBarTrack = FPSpreadADO.ScrollBarTrackConstants.ScrollBarTrackBoth
        ' Show the scroll tips
        SprdLedg.ShowScrollTips = FPSpreadADO.ShowScrollTipsConstants.ShowScrollTipsBoth
        Call frmViewLedgerExp_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillComboBox()
        On Error GoTo ErrPart
        'MainClass.FillCombo CboCC, "CST_CENTER_MST", "COST_CENTER_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""  ', "BranchCode=" & RsCompany.fields("CBRANCHCODE & ""
        MainClass.FillCombo(CboCC, "FIN_CCENTER_HDR", "CC_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(CboDept, "PAY_DEPT_MST", "DEPT_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(cboEmp, "PAY_EMPLOYEE_MST", "EMP_NAME", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(cboExpHead, "CST_CENTER_MST", "COST_CENTER_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(cboDivision, "INV_DIVISION_MST", "DIV_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        cboDivision.SelectedIndex = 0
        CboDept.SelectedIndex = 0
        cboEmp.SelectedIndex = 0
        CboCC.SelectedIndex = 0
        cboExpHead.SelectedIndex = 0
        txtCondAmount.Text = CStr(0)
        cboCond.Items.Clear()
        cboCond.Items.Add("=")
        cboCond.Items.Add("<=")
        cboCond.Items.Add(">=")
        cboCond.Items.Add("<")
        cboCond.Items.Add(">")
        cboCond.Items.Add("<>")
        cboCond.SelectedIndex = 0
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
    Private Sub frmViewLedgerExp_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdLedg.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth
        MainClass.SetSpreadColor(SprdLedg, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewLedgerExp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        ''MainClass.AssignDataInSprd8("", SprdLedg, "", "N")
        Me.Close()
    End Sub
    Private Sub OptSumDet_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSumDet.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSumDet.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub
    Private Sub SprdLedg_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdLedg.DataColConfig
        SprdLedg.Row = -1
        SprdLedg.Col = eventArgs.col
        SprdLedg.DAutoCellTypes = True
        SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdLedg.TypeEditLen = 1000
    End Sub
    Private Sub SprdLedg_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdLedg.DblClick
        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String
        Dim pIndex As Integer
        Dim xVTYPE As String
        If SprdLedg.ActiveRow < 0 Then Exit Sub
        If OptSumDet(0).Checked = True Then
            SprdLedg.Row = SprdLedg.ActiveRow
            SprdLedg.Col = ColVDate
            xVDate = Me.SprdLedg.Text
            SprdLedg.Col = ColMKEY
            xMKey = Me.SprdLedg.Text
            If xMKey = "-1" Then
                Exit Sub
            End If
            SprdLedg.Col = ColVNo
            xVNo = Me.SprdLedg.Text
            SprdLedg.Col = ColBookType
            xBookType = Me.SprdLedg.Text
            SprdLedg.Col = ColBookSubType
            xBookSubType = Me.SprdLedg.Text
            If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Then
                xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
                xVNo = VB.Right(xVNo, 5)
            ElseIf xBookType = "R" Or xBookType = "E" Then
                If RsCompany.Fields("FYEAR").Value >= 2020 Then
                    xVTYPE = Mid(xVNo, 1, Len(xVNo) - 8)
                    xVNo = VB.Right(xVNo, 8)
                Else
                    xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
                    xVNo = VB.Right(xVNo, 5)
                End If
            End If
            Call ShowTrn(xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, Me)
        Else
            If OptSumDet(2).Checked = True Then
                pIndex = 2
            Else
                pIndex = 1
                SprdLedg.Row = SprdLedg.ActiveRow
                SprdLedg.Col = ColVDate
                xVDate = Me.SprdLedg.Text
            End If
            Call ViewAccountLedger(pIndex, xVDate, xVDate)
        End If
    End Sub
    Private Sub ViewAccountLedger(ByRef xIndex As Integer, ByRef pDateFrom As String, ByRef pDateTo As String)
        Dim ss As New frmViewLedgerExp ''frmViewLedger
        Dim mFromDate As String
        Dim mToDate As String
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblBookType.Text = ConLedger Then
            ss.MdiParent = Me.MdiParent
            ss.lblBookType.Text = "LEDG"
            ss.TxtAccount.Text = TxtAccount.Text
            ss.lblAcCode.Text = lblAcCode.Text
            SprdLedg.Row = SprdLedg.ActiveRow
            SprdLedg.Col = ColVDate
            If GetMonthStartEndDate((SprdLedg.Text), mFromDate, mToDate) = True Then
                ss.txtDateFrom.Text = VB6.Format(mFromDate, "dd/mm/yyyy")
                ss.txtDateTo.Text = VB6.Format(mToDate, "dd/mm/yyyy")
            Else
                ss.txtDateFrom.Text = VB6.Format(pDateFrom, "dd/mm/yyyy")
                ss.txtDateTo.Text = VB6.Format(pDateTo, "dd/mm/yyyy")
            End If
            ss.OptSumDet(xIndex - 1).Checked = True
            ''ss.cboConsolidated.Text = cboConsolidated.Text
            '        ss.cboConsolidated.ListIndex = 3     ''DIVISION...
            ss.Show()
            ss.frmViewLedgerExp_Activated(Nothing, New System.EventArgs())
            ss.cmdShow_Click(Nothing, New System.EventArgs())
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub SprdLedg_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdLedg.KeyDownEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then
            SprdLedg_DblClick(SprdLedg, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Escape Then cmdClose.PerformClick()
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        Call SearchAccounts(TxtAccount)
    End Sub
    Private Sub TxtAgtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAgtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAgtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAgtAccount.DoubleClick
        Call SearchAccounts(TxtAgtAccount)
    End Sub
    Private Sub TxtAgtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAgtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        lblAcCode.Text = ""
        If TxtAgtAccount.Text = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
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
        If MainClass.ValidateWithMasterTable((TxtAgtAccount.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            TxtAgtAccount.Text = UCase(Trim(TxtAgtAccount.Text))
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
    Private Sub SearchAccounts(ByRef mTextBox As System.Windows.Forms.TextBox)
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If UCase(mTextBox.Name) <> UCase("TxtAgtAccount") Then
            Select Case lblBookType.Text
                Case ConLedger
                    SqlStr = SqlStr
                Case ConCashBook
                    SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '1'"
                Case ConBankBook, ConPDCBook
                    SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '2'"
                Case Else
                    SqlStr = " AND 1=2"
            End Select
        End If
        '    MainClass.SearchMaster mTextBox.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr
        If MainClass.SearchGridMaster((mTextBox.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                mTextBox.Text = AcName
            End If
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccounts(TxtAccount)
    End Sub
    Private Sub TxtAgtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAgtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAgtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAgtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAgtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccounts(TxtAgtAccount)
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And "
        Select Case lblBookType.Text
            Case ConLedger
                SqlStr = ""
            Case ConCashBook
                SqlStr = "SUPP_CUST_TYPE = '1'"
            Case ConBankBook, ConPDCBook
                SqlStr = "SUPP_CUST_TYPE = '2'"
            Case Else
                SqlStr = "1=2"
        End Select
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
    Private Sub FormatSprdLedg(ByRef Arow As Integer)
        With SprdLedg
            .MaxCols = ColBranch
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)
            .set_RowHeight(-1, RowHeight)
            .RowsFrozen = 1
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
            .set_ColWidth(ColBookSubType, 15)
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
            .set_ColWidth(ColVNo, 10)
            If OptSumDet(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
            '        .Col = ColAcctName
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = True
            '        .ColWidth(ColAcctName) = 15
            '        If OptSumDet(0).Value = True Then
            '            .ColHidden = False
            '            .ColsFrozen = ColAcctName
            '        Else
            '            .ColHidden = True
            '        End If
            '
            .Col = ColDAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDAmount, 12)
            .Col = ColCAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColCAmount, 12)
            .Col = ColBalance
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBalance, 12)
            .ColHidden = False
            .Col = ColBalDC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBalDC, 5)
            .ColHidden = False
            '        If ChkWithRunBal.Value = vbUnchecked Then
            '            .Col = ColBalance
            '            .ColHidden = True
            '            .Col = ColBalDC
            '            .ColHidden = True
            '        End If
            .Col = ColNarration
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColNarration, 25)
            If OptSumDet(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
            .Col = ColBillDetail
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDetail, 15)
            .ColHidden = True
            If OptSumDet(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
            .Col = ColChequeNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChequeNo, 8)
            If OptSumDet(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDept, 8)
            .ColHidden = True
            .Col = ColEmp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColEmp, 8)
            .ColHidden = True
            .Col = ColCostC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCostC, 8)
            .ColHidden = True
            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True
            .Col = ColSubRowNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSubRowNo, 5)
            .ColHidden = True
            .Col = ColBranch
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBranch, 15)
            .ColHidden = True ''IIf(Left(cboConsolidated.Text, 1) = "D", True, False)
            MainClass.SetSpreadColor(SprdLedg, -1)
            MainClass.ProtectCell(SprdLedg, 1, .MaxRows, 1, .MaxCols)
            SprdLedg.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            SprdLedg.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            'Show the grid lines over the color
            '        SprdLedg.BackColorStyle = BackColorStyleOverVertGridOnly
            SprdLedg.DAutoCellTypes = True
            SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdLedg.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FillRunBalCol()
        On Error GoTo ERR1
        Dim ii As Integer
        Dim mBalance As Double
        Dim mDAmount As Double
        Dim mCAmount As Double
        Dim mTotDAmount As Double
        Dim mTotCAmount As Double
        With SprdLedg
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColDAmount
                If IsNumeric(.Text) Then
                    mDAmount = CDbl(.Text)
                Else
                    mDAmount = 0
                End If
                mTotDAmount = mTotDAmount + mDAmount
                .Col = ColCAmount
                If IsNumeric(.Text) Then
                    mCAmount = CDbl(.Text)
                Else
                    mCAmount = 0
                End If
                mTotCAmount = mTotCAmount + mCAmount
                mBalance = mBalance + mDAmount - mCAmount
                .Col = ColBalance
                .Text = MainClass.FormatRupees(System.Math.Abs(mBalance))
                .Col = ColBalDC
                .Text = IIf(mBalance > 0, "Dr", "Cr")
            Next
            mBalance = 0
            For ii = .MaxRows To .MaxRows
                .set_RowHeight(ii, RowHeight * 1.25)
                .Row = ii
                .Col = ColDAmount
                .Text = MainClass.FormatRupees(System.Math.Abs(mTotDAmount))
                .Font = VB6.FontChangeBold(.Font, True)
                If IsNumeric(.Text) Then
                    mDAmount = CDbl(.Text)
                Else
                    mDAmount = 0
                End If
                .Col = ColCAmount
                .Text = MainClass.FormatRupees(System.Math.Abs(mTotCAmount))
                .Font = VB6.FontChangeBold(.Font, True)
                If IsNumeric(.Text) Then
                    mCAmount = CDbl(.Text)
                Else
                    mCAmount = 0
                End If
                mBalance = mBalance + mDAmount - mCAmount
                .Col = ColBalance
                .Text = MainClass.FormatRupees(System.Math.Abs(mBalance))
                .Col = ColBalDC
                .Text = IIf(mBalance > 0, "Dr", "Cr")
            Next
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Function LedgInfo() As Boolean
        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim mAccountCode2 As String
        LedgInfo = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr1 = MakeSQL
        SqlStr2 = MakeSQLCond(False)
        SqlStr = SqlStr1 & vbCrLf & SqlStr2
        If chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.AMOUNT " & cboCond.Text & Val(txtCondAmount.Text) & "" ''* DECODE(TRN.DC,'D',1,-1)
        End If
        If lblBookType.Text = ConJournalBook Then
            SqlStr = SqlStr & vbCrLf & " AND BookCode='" & ConJournalBookCode & "' "
        ElseIf lblBookType.Text = ConContraBook Then
            SqlStr = SqlStr & vbCrLf & " AND BookCode='" & ConContraBookCode & "' "
        Else
            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
            If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable((TxtAgtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode2 = MasterNo
                Else
                    mAccountCode2 = "-1"
                End If
                ''22-10-2005
                '            SqlStr = SqlStr & vbCrLf _
                ''                    & " AND TRN.BOOKTYPE ||TRN.MKEY IN (SELECT BOOKTYPE || MKEY " & SqlStr2 & vbCrLf _
                ''                    & " AND ACCOUNTCODE='" & mAccountCode2 & "') "
                ''22-10-2005
                SqlStr = SqlStr & vbCrLf & " AND TRN.BOOKTYPE ||TRN.MKEY IN ( " & vbCrLf & " SELECT BOOKTYPE || MKEY " & vbCrLf & " FROM FIN_POSTED_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode2 & "'" & vbCrLf & " AND TRN.EXPDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')" & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')" & vbCrLf & " ) "
            End If
            ''ok
            ''         SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE<>'" & mAccountCode & "' " & vbCrLf _
            '                & " AND TRN.BOOKTYPE ||TRN.MKEY IN (SELECT TRN.BOOKTYPE || TRN.MKEY " & SqlStr2 & vbCrLf _
            '                & " AND ACCOUNTCODE='" & mAccountCode & "')"
        End If
        '    & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.VDATE),'DD/MM/YYYY') ELSE TO_CHAR(TRN.VDATE,'DD/MM/YYYY') END V_DATE, " & vbCrLf _
        ''                & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.VDATE,'MMMYYYY') ELSE TRN.VNO END AS V_NO, " & vbCrLf _
        '
        ''& " ELSE " & vbCrLf _
        '& " CASE WHEN BOOKCODE='-3' THEN GETACCOUNTNAMEFROMTRN(TRN.MKEY,TRN.COMPANY_CODE,TRN.FYEAR,TRN.BOOKTYPE , TRN.BOOKSUBTYPE,TRN.ACCOUNTCODE)  || CHR(13) ELSE '' END || DECODE(TRN.NARRATION,NULL,'', TRN.NARRATION) END ," & vbCrLf _'
        If OptSumDet(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.EXPDATE),'DD/MM/YYYY') ELSE TO_CHAR(TRN.EXPDATE,'DD/MM/YYYY') END," & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_DATE(LAST_DAY(TRN.EXPDATE),'DD/MM/YYYY') ELSE TO_DATE(TRN.EXPDATE,'DD/MM/YYYY') END, " & vbCrLf & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE , " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.EXPDATE,'MONYYYY') ELSE TRN.VNO END,  " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN DECODE(BOOKCODE," & ConSalesBookCode & ",'Sales Month of :','Purchase Month of :') || TO_CHAR(TRN.EXPDATE,'MONYYYY')  ELSE DECODE(TRN.NARRATION,NULL,'', TRN.NARRATION) END ," & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '' ELSE REPLACE(TRN.REMARKS, chr(13)||chr(10),' ') END," & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '-1' ELSE TRN.MKEY END ," & vbCrLf & " CHEQUENO || ' ' || TO_CHAR(CHQDATE,'DD/MM/YYYY'),MONTHWISE_LDGR, BOOKCODE " & vbCrLf & " ORDER BY " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_DATE(LAST_DAY(TRN.EXPDATE),'DD/MM/YYYY') ELSE TO_DATE(TRN.EXPDATE,'DD/MM/YYYY') END, " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.EXPDATE,'MONYYYY') ELSE TRN.VNO END"
            ''ACM.SUPP_CUST_NAME,
            ''& " CASE WHEN BOOKCODE='-3' THEN ACM.SUPP_CUST_NAME || ' - ' ELSE '' END || TRN.NARRATION," & vbCrLf _
            '
            ''& " CASE WHEN BOOKCODE='-3' THEN GETACCOUNTNAMEFROMTRN(TRN.MKEY,TRN.COMPANY_CODE,TRN.FYEAR,TRN.BOOKTYPE , TRN.BOOKSUBTYPE,TRN.ACCOUNTCODE) || ' - ' ELSE '' END || TRN.NARRATION, TRN.REMARKS,TRN.MKEY ,ACM.SUPP_CUST_NAME,CHEQUENO || ' ' || TO_CHAR(CHQDATE,'DD/MM/YYYY')" & vbCrLf _
            '& " TRN.LOCKED, TRN.CHEQUENO, DEPT.DEPT_CODE,EMP.EMP_CODE,COSTC.COST_CENTER_CODE,TRN.MKEY,"
        ElseIf OptSumDet(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.EXPDATE" & vbCrLf & " ORDER BY TRN.EXPDATE"
        ElseIf OptSumDet(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY SUBSTR(TRN.EXPDATE,4,3),TO_CHAR(TRN.EXPDATE,'YYYYMM')" & vbCrLf & " ORDER BY TO_CHAR(TRN.EXPDATE,'YYYYMM')"
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdLedg, StrConn, "Y")
        '********************************
        'Get Opening Balance.........
        SqlStr = MakeOPSQL(mAccountCode)
        '    SqlStr2 = MakeSQLCond(True)
        '    SqlStr = SqlStr1 & vbCrLf & SqlStr2 & vbCrLf _
        ''            & " AND ACCOUNTCODE=" & mAccountCode & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOP, ADODB.LockTypeEnum.adLockReadOnly)
        If RsOP.EOF = False Then
            mOpening = IIf(IsDbNull(RsOP.Fields("OPENING").Value), 0, RsOP.Fields("OPENING").Value)
            If mOpening >= 0 Then
                mOpDr = mOpening
                mOpCr = 0
            Else
                mOpDr = 0
                mOpCr = System.Math.Abs(mOpening)
            End If
        End If
        DisplayTotals(mOpDr, mOpCr)
        LedgInfo = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
LedgError:
        LedgInfo = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String
        ''& " CASE WHEN BOOKCODE='-3' THEN GETACCOUNTNAMEFROMTRN(TRN.MKEY,TRN.COMPANY_CODE,TRN.FYEAR,TRN.BOOKTYPE , TRN.BOOKSUBTYPE,TRN.ACCOUNTCODE)|| ' - ' ELSE '' END || TRN.NARRATION AS NARRATION," & vbCrLf _
        '& " CASE WHEN BOOKCODE='-3' THEN ACM.SUPP_CUST_NAME || ' - ' ELSE '' END || TRN.NARRATION AS NARRATION, " & vbCrLf _
        ''" & mainclass.LastDay(month(trn.VDATE),Year(TRN.VDATE)) & "' || '/' ||
        ''& " CASE WHEN BOOKCODE='-3' THEN GETACCOUNTNAMEFROMTRN(TRN.MKEY,TRN.COMPANY_CODE,TRN.FYEAR,TRN.BOOKTYPE , TRN.BOOKSUBTYPE,TRN.ACCOUNTCODE) || CHR(13) ELSE '' END || DECODE(TRN.NARRATION,NULL,'', TRN.NARRATION) END AS NARRATION, " & vbCrLf _'
        If OptSumDet(0).Checked = True Then
            SqlStr = " SELECT '' AS LOCKED, " & vbCrLf & " TRN.BOOKTYPE , " & vbCrLf & " TRN.BOOKSUBTYPE,  " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN LAST_DAY(TRN.EXPDATE) ELSE TRN.EXPDATE END V_DATE, " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.EXPDATE,'MONYYYY') ELSE TRN.VNO END AS V_NO, " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN DECODE(BOOKCODE," & ConSalesBookCode & ",'Sales Month of :','Purchase Month of :') || TO_CHAR(TRN.EXPDATE,'MONYYYY') ELSE DECODE(TRN.NARRATION,NULL,'', TRN.NARRATION) END AS NARRATION, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END,'9,99,99,99,999.99') AS DEBIT, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END,'9,99,99,99,999.99') AS CREDIT, " & vbCrLf & " '','', " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '' ELSE REPLACE(TRN.REMARKS, chr(13)||chr(10),' ') END, " & vbCrLf & " CHEQUENO || ' ' || TO_CHAR(CHQDATE,'DD/MM/YYYY') AS CHEQUENO, " & vbCrLf & " '' AS DEPT_CODE,'' AS EMP_CODE,'' AS COST_CENTER_CODE, " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '-1' ELSE TRN.MKEY END ," & vbCrLf & " '','' "
            ''& " DEPT.DEPT_CODE,EMP.EMP_CODE,COSTC.COST_CENTER_CODE,TRN.MKEY,'','' "
        ElseIf OptSumDet(1).Checked = True Then
            SqlStr = " SELECT '', '','',TO_CHAR(TRN.EXPDATE,'DD/MM/YYYY'),'','', " & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END,'9,99,99,99,999.99') AS DEBIT, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END,'9,99,99,99,999.99') AS CREDIT, " & vbCrLf & " '','', '','', " & vbCrLf & " '' AS DEPT_CODE,'' AS EMP_CODE,'' AS COST_CENTER_CODE,'','','' "
        ElseIf OptSumDet(2).Checked = True Then
            SqlStr = " SELECT '', '','',TO_CHAR(TRN.EXPDATE,'MON-YYYY'),'','', " & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END,'9,99,99,99,999.99') AS DEBIT, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END,'9,99,99,99,999.99') AS CREDIT, " & vbCrLf & " '','','','', " & vbCrLf & " '' AS DEPT_CODE,'' AS EMP_CODE,'' AS COST_CENTER_CODE,'','','' "
        End If
        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function
    Private Function MakeSQLForInsert(ByRef pAccountName As String) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        ''& " CASE WHEN BOOKCODE='-3' THEN GETACCOUNTNAMEFROMTRN(TRN.MKEY,TRN.COMPANY_CODE,TRN.FYEAR,TRN.BOOKTYPE , TRN.BOOKSUBTYPE,TRN.ACCOUNTCODE)|| ' - ' ELSE '' END || TRN.NARRATION AS NARRATION," & vbCrLf _
        '& " CASE WHEN BOOKCODE='-3' THEN ACM.SUPP_CUST_NAME || ' - ' ELSE '' END || TRN.NARRATION, " & vbCrLf _
        '
        SqlStr = " Select '" & MainClass.AllowSingleQuote(PubUserID) & "', "
        If OptSumDet(0).Checked = True Then
            SqlStr = SqlStr & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN TO_CHAR(LAST_DAY(TRN.EXPDATE),'YYYYMMDD') ELSE TO_CHAR(Vdate,'YYYYMMDD') END," & vbCrLf & " '" & MainClass.AllowSingleQuote(pAccountName) & "'," & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN LAST_DAY(TRN.EXPDATE) ELSE TRN.EXPDATE END, " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN  TO_CHAR(TRN.EXPDATE,'MONYYYY') ELSE TRN.VNO END, " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN DECODE(BOOKCODE," & ConSalesBookCode & ",'Sales Month of :','Purchase Month of :') || TO_CHAR(TRN.EXPDATE,'MONYYYY') ELSE DECODE(TRN.NARRATION,NULL,'',TRN.NARRATION) END AS NARRATION," & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END , " & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END , " & vbCrLf & " CHEQUENO || ' ' || TO_CHAR(CHQDATE,'DD/MM/YYYY') AS CHEQUENO, " & vbCrLf & " '','', " & vbCrLf & " CASE WHEN MONTHWISE_LDGR='Y' AND (BOOKCODE=" & ConPurchaseBookCode & " OR BOOKCODE=" & ConSalesBookCode & ") THEN '' ELSE REPLACE(TRN.REMARKS, chr(13)||chr(10),' ') END"
            ''& " DEPT.DEPT_CODE,EMP.EMP_CODE,COSTC.COST_CENTER_CODE,TRN.MKEY,'','' "
        ElseIf OptSumDet(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(Vdate,'YYYYMMDD'),'', TO_CHAR(TRN.EXPDATE,'DD/MM/YYYY'),'','', " & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END AS DEBIT, " & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END AS CREDIT, " & vbCrLf & " '','', '',''"
        ElseIf OptSumDet(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(Vdate,'YYYYMM'),'',TO_CHAR(TRN.EXPDATE,'MON-YYYY'),'','', " & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) ELSE 0 END AS DEBIT, " & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',-1,1)) ELSE 0 END AS CREDIT, " & vbCrLf & " '','','',''"
        End If
        MakeSQLForInsert = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLForInsert = ""
    End Function
    Private Function MakeOPSQL(ByRef pAccountCode As String) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCostCName As String
        Dim mDeptName As String
        Dim mEmp As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        Dim mDivisionCode As Double
        SqlStr = " SELECT " & vbCrLf & " SUM(DECODE(TRN.DC,'D',1,-1) * TRN.AMOUNT)  AS OPENING "
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN " & vbCrLf & " WHERE  " & vbCrLf & " TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.ACCOUNTCODE='" & pAccountCode & "'" & vbCrLf _
            & " AND TRN.EXPDATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & mDivisionCode & ""
        End If
        mGroupOption = GetGroupOption
        mGroupOption = mGroupOption & vbCrLf & IIf(mGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConOpeningBook & "'"
        If mGroupOption <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ( " & mGroupOption & " ) "
        End If
        '    If CboCC.Text = "ALL" Then
        '        mCostCName = ""
        '    Else
        '        mCostCName = MainClass.AllowSingleQuote(CboCC.Text)
        '    End If
        '
        '    If CboDept.Text = "ALL" Then
        '        mDeptName = ""
        '    Else
        '        mDeptName = MainClass.AllowSingleQuote(CboDept.Text)
        '    End If
        '
        '    If cboEmp.Text = "ALL" Then
        '        mEmp = ""
        '    Else
        '        mEmp = MainClass.AllowSingleQuote(cboEmp.Text)
        '    End If
        MakeOPSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeOPSQL = ""
    End Function
    Private Function MakeSQLCond(ByRef mIsOpening As Boolean) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCostCCode As String
        Dim mDeptCode As String
        Dim mEmpCode As String
        Dim mExpHeadCode As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        Dim mDivisionCode As Double
        If CboCC.Text = "ALL" Then
            mCostCCode = ""
        Else
            If MainClass.ValidateWithMasterTable((CboCC.Text), "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCostCCode = MasterNo
            End If
        End If
        If cboExpHead.Text = "ALL" Then
            mExpHeadCode = ""
        Else
            If MainClass.ValidateWithMasterTable((cboExpHead.Text), "COST_CENTER_DESC", "COST_CENTER_CODE", "CST_CENTER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mExpHeadCode = MasterNo
            End If
        End If
        If CboDept.Text = "ALL" Then
            mDeptCode = ""
        Else
            If MainClass.ValidateWithMasterTable((CboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
            End If
        End If
        If cboEmp.Text = "ALL" Then
            mEmpCode = ""
        Else
            If MainClass.ValidateWithMasterTable((cboEmp.Text), "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmpCode = MasterNo
            End If
        End If
        '    mConsolidated = IIf(cboConsolidated.ListIndex = -1, "D", Left(cboConsolidated.Text, 1))
        SqlStr = " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE  "
        SqlStr = SqlStr & vbCrLf & " TRN.Company_Code=ACM.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "
        If mCostCCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.COSTCCODE='" & mCostCCode & "'"
        End If
        If mDeptCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.DEPTCODE='" & mDeptCode & "'"
        End If
        If mEmpCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.EMPCODE='" & mEmpCode & "'"
        End If
        If mExpHeadCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.EXP_CODE='" & mExpHeadCode & "'"
        End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & mDivisionCode & ""
        End If
        mGroupOption = GetGroupOption
        If mIsOpening = True Then
            mGroupOption = mGroupOption & vbCrLf & IIf(mGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConOpeningBook & "'"
        End If
        If mGroupOption <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ( " & mGroupOption & " ) "
        End If
        If lblBookType.Text = ConLedger Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " "
        End If
        If mIsOpening = True Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.EXPDATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            SqlStr = SqlStr & " AND TRN.EXPDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        MakeSQLCond = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLCond = ""
    End Function
    Private Function MakeSQLCondInsert(ByRef mIsOpening As Boolean) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCostCName As String
        Dim mDeptName As String
        Dim mEmp As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        Dim mDivisionCode As Double
        '& " PAY_EMPLOYEE_MST EMP,PAY_DEPT_MST DEPT,CST_CENTER_MST COSTC " & vbCrLf _
        '
        SqlStr = " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM" & vbCrLf & " WHERE  " & vbCrLf & " TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
        SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code =ACM.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE " '& vbCrLf |            & " AND EMP.Company_Code(+) = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf |            & " AND TRN.EMPCODE=EMP.EMP_CODE(+) " & vbCrLf |            & " AND COSTC.Company_Code(+) = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf |            & " AND TRN.COSTCCODE=COSTC.COST_CENTER_CODE(+) " & vbCrLf |            & " AND DEPT.Company_Code(+) = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf |            & " AND TRN.DEPTCODE=DEPT.DEPT_CODE(+) "
        '    If CboCC.Text <> "ALL" Then SqlStr = SqlStr & vbCrLf & " AND COSTCALIAS='" & MainClass.AllowSingleQuote(CboCC.Text) & "'"
        '    If CboDept.Text <> "ALL" Then SqlStr = SqlStr & vbCrLf & " AND DEPTALIAS='" & MainClass.AllowSingleQuote(CboDept.Text) & "'"
        '    If cboEmp.Text <> "ALL" Then SqlStr = SqlStr & vbCrLf & " AND EMPALIAS='" & MainClass.AllowSingleQuote(cboEmp.Text) & "'"
        mGroupOption = GetGroupOption
        If mIsOpening = True Then
            mGroupOption = mGroupOption & vbCrLf & IIf(mGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConOpeningBook & "'"
        End If
        If mGroupOption <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ( " & mGroupOption & " ) "
        End If
        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND TRN.DIV_CODE=" & mDivisionCode & ""
        End If
        If mIsOpening = True Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.EXPDATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            SqlStr = SqlStr & " AND TRN.EXPDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            '        SqlStr = SqlStr & vbCrLf _
            ''                & " AND TRN.Vdate>='" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "' " & vbCrLf _
            ''                & " AND TRN.Vdate<='" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' "
        End If
        MakeSQLCondInsert = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLCondInsert = ""
    End Function
    Private Function GetGroupOption() As String
        On Error GoTo ErrPart
        Dim mAllCheck As Boolean
        GetGroupOption = ""
        mAllCheck = True
        If chkGroup(0).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConBankBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(1).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConCashBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(2).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConSaleBook & "' OR TRN.BOOKTYPE='" & ConSaleDebitBook & "' OR  TRN.BookType = '" & ConSaleCreditBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(3).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConPurchaseBook & "' OR TRN.BookType = '" & ConGRBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(4).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConDebitNoteBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(5).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConCreditNoteBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(6).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConJournalBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(7).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConContraBook & "'"
        Else
            mAllCheck = False
        End If
        If chkGroup(8).CheckState = System.Windows.Forms.CheckState.Checked Then
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConPDCBook & "'"
        Else
            mAllCheck = False
        End If
        If mAllCheck = True Then
            GetGroupOption = ""
        End If
        Exit Function
ErrPart:
        GetGroupOption = ""
        MsgBox(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        Dim mAccountCode2 As String
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If lblBookType.Text = ConLedger Then
            If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        End If
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            Select Case lblBookType.Text
                Case ConLedger
                    MsgInformation("Please Select Account")
                    Exit Function
                Case ConCashBook, ConBankBook
                    MsgInformation("Please Select Book")
                    Exit Function
                Case ConPurchaseBook
                    mAccountCode = CStr(ConPurchaseBookCode)
                Case ConPurchaseGenBook
                    mAccountCode = CStr(ConPurchaseGenBookCode)
                Case ConSaleBook
                    '                If cboAccount.ListIndex = 0 Then
                    '                    mAccountCode = ConSalesBookCode
                    '                Else
                    '                    mAccountCode = ConExciseSalesBookCode
                    '                End If
                Case ConJournalBook
                    mAccountCode = CStr(ConJournalBookCode)
                Case ConDebitNoteBook
                    mAccountCode = CStr(ConDebitNoteBookCode)
                Case ConCreditNoteBook
                    mAccountCode = CStr(ConCreditNoteBookCode)
                Case ConGRBook
                    mAccountCode = CStr(ConGRBookCode)
            End Select
        End If
        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAgtAccount.Text) = "" Then
                MsgInformation("Please Select Agt. Account Name.")
                TxtAgtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAgtAccount.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Please valid Agt. Account Name.")
                TxtAgtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If lblBookType.Text = ConLedger Then
            If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
                txtDateTo.Focus()
                Cancel = True
                GoTo EventExitSub
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub chkOption_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOption.CheckStateChanged
        FraAmountCond.Visible = IIf(chkOption.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        PrintStatus(False)
    End Sub
    Private Sub cboCond_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub cboCond_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCond.SelectedIndexChanged
        PrintStatus(False)
    End Sub
    Private Sub txtCondAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCondAmount.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtCondAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCondAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
