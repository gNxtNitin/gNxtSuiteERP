Option Strict Off
Option Explicit On
Imports AxFPSpreadADO
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewBook
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
    Private Const colAccount As Short = 6
    Private Const ColNarration As Short = 7
    Private Const ColDAmount As Short = 8
    Private Const ColCAmount As Short = 9
    Private Const ColBalance As Short = 10
    Private Const ColBalDC As Short = 11
    Private Const ColBillDetail As Short = 12
    Private Const ColChequeNo As Short = 13
    Private Const ColDept As Short = 14
    Private Const ColEmp As Short = 15
    Private Const ColCostC As Short = 16
    Private Const ColAddUser As Short = 17
    Private Const ColAddDate As Short = 18
    Private Const ColModUser As Short = 19
    Private Const ColModDate As Short = 20
    Private Const ColMKEY As Short = 21
    Private Const ColSubRowNo As Short = 22
    Private Const ColBranch As Short = 23



    Private Const mPageWidth As Short = 132
    Private Const TabRefNo As Short = 0
    Private Const TabName As Short = 10
    Private Const TabDesc As Short = 45
    Private Const TabCheque As Short = 77
    Private Const TabDAmount As Short = 87
    Private Const TabCAmount As Short = 102
    Private Const TabBalance As Short = 117
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim mClickProcess As Boolean
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
                Me.Text = "Ledger"
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
    Private Sub cboEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboEmp.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboEmp_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboEmp.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub chkGroup_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGroup.CheckStateChanged
        Dim Index As Short = chkGroup.GetIndex(eventSender)
        Call PrintStatus(False)
    End Sub
    Private Sub ChkWithRunBal_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkWithRunBal.CheckStateChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        On Error GoTo ERR1
        Dim PrintStatus As Boolean
        '    If TxtAccount.Text = "" Then PrintStatus = False Else PrintStatus = True
        If lblBookType.Text = ConBankBook Or lblBookType.Text = ConCashBook Or lblBookType.Text = ConPDCBook Then
            If TxtAccount.Text = "" Then Exit Sub
            PrintStatus = True
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If mDOSPRINTING = True Then
            Call BookReport("V")
        Else
            Call ReportForBook(Crystal.DestinationConstants.crptToWindow)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForBook(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String
        PubDBCn.Errors.Clear()
        frmPrintLedg.Frame1.Enabled = False
        frmPrintLedg.chkPrintOption(0).Enabled = False
        frmPrintLedg.chkPrintOption(4).Enabled = True
        frmPrintLedg.chkPrintOption(5).Enabled = True
        frmPrintLedg.chkPrintOption(6).Enabled = True
        If OptSumDet(1).Checked = True Or OptSumDet(2).Checked = True Then
            frmPrintLedg.fraPrintOption.Enabled = False
        Else
            frmPrintLedg.fraPrintOption.Enabled = True
        End If
        frmPrintLedg.ShowDialog()
        If G_PrintLedg = False Then
            Exit Sub
        End If
        SqlStr = "DELETE FROM TEMP_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = ""
        If InsertBook() = False Then GoTo ERR1
        'Select Record for print...
        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        If OptSumDet(0).Checked = True Then
            If lblBookType.Text = ConBankBook Or lblBookType.Text = ConPDCBook Then
                If frmPrintLedg.chkWideFormat.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mRPTName = "BankBook.rpt"
                Else
                    mRPTName = "BankBook_80.rpt"
                End If
            ElseIf lblBookType.Text = ConCashBook Then
                If frmPrintLedg.chkWideFormat.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mRPTName = "CashBook.rpt"
                Else
                    mRPTName = "CashBook_80.rpt"
                End If
            Else
                If frmPrintLedg.chkWideFormat.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mRPTName = "OtherBook.rpt"
                Else
                    mRPTName = "OtherBook_80.rpt"
                End If
            End If
        Else
            mRPTName = "BookSummary.Rpt"
        End If
        If lblBookType.Text = ConBankBook Then
            mTitle = TxtAccount.Text
        ElseIf lblBookType.Text = ConCashBook Then
            mTitle = "Cash Book"
        ElseIf lblBookType.Text = ConPDCBook Then
            mTitle = "PDC Book"
        ElseIf lblBookType.Text = ConDebitNoteBook Then
            mTitle = "Debit Note Journal"
        ElseIf lblBookType.Text = ConCreditNoteBook Then
            mTitle = "Credit Note Journal"
        ElseIf lblBookType.Text = ConJournalBook Then
            mTitle = "Journal"
        Else
            mTitle = TxtAccount.Text ''Me.text
        End If
        If OptSumDet(1).Checked = True Then
            mTitle = mTitle & " - Daily"
        ElseIf OptSumDet(2).Checked = True Then
            mTitle = mTitle & " - Monthly"
        End If
        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")
        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)
        frmPrintLedg.Close()
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
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        mSqlStr = "SELECT * " & " FROM TEMP_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"
        FetchRecordForReport = mSqlStr
    End Function
    Private Function InsertBook() As Boolean
        Dim mVDate As String
        Dim mVNo As String
        Dim mAcctName As String
        Dim mBillDetail As String
        Dim mNarration As String
        Dim mChequeNo As String
        Dim mDAmt As Double
        Dim mCAmt As Double
        Dim mRunningBal As Double
        Dim mRunningBalStr As String
        Dim SqlStr As String
        Dim cntRow As Integer
        On Error GoTo ERR1
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = ""
        With SprdLedg
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColVDate
                If Trim(.Text) = "" Then
                    mVDate = ""
                Else
                    mVDate = VB6.Format(.Text, "DDDD, dd/mm/yyyy")
                End If
                .Col = ColVNo
                mVNo = .Text
                .Col = colAccount
                If frmPrintLedg.chkPrintOption(3).CheckState = System.Windows.Forms.CheckState.Checked Then
                    mAcctName = Trim(.Text)
                Else
                    mAcctName = ""
                End If
                .Col = ColBillDetail
                If frmPrintLedg.chkPrintOption(7).CheckState = System.Windows.Forms.CheckState.Checked Then
                    mBillDetail = Trim(.Text)
                Else
                    mBillDetail = ""
                End If
                .Col = ColNarration
                If frmPrintLedg.chkPrintOption(1).CheckState = System.Windows.Forms.CheckState.Checked Then
                    mNarration = Trim(.Text)
                Else
                    mNarration = ""
                End If
                .Col = ColChequeNo
                If frmPrintLedg.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Checked Then
                    mChequeNo = Trim(.Text)
                Else
                    mChequeNo = ""
                End If
                .Col = ColDAmount
                If IsNumeric(.Text) Then
                    mDAmt = MainClass.FormatRupees(.Text)
                Else
                    mDAmt = 0
                End If
                .Col = ColCAmount
                If IsNumeric(.Text) Then
                    mCAmt = MainClass.FormatRupees(.Text)
                Else
                    mCAmt = 0
                End If
                mRunningBal = mRunningBal + (mDAmt - mCAmt)
                mRunningBalStr = VB6.Format(System.Math.Abs(mRunningBal), "##,##,##,##,###.00") & IIf(mRunningBal >= 0, " Dr", " Cr")
                SqlStr = "Insert into TEMP_PrintDummyData NOLOGGING (UserID,SubRow,Field1,Field2,Field3,Field4,Field5,Field6,Field7,FIELD8,FIELD9,FIELD10) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & Trim(mVDate) & "', " & vbCrLf & " '" & Trim(mVNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mAcctName)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mBillDetail)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mNarration)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mChequeNo)) & "', " & vbCrLf & " '" & Trim(CStr(mDAmt)) & "', " & vbCrLf & " '" & Trim(CStr(mCAmt)) & "', " & vbCrLf & " '" & Trim(CStr(mRunningBal)) & "','" & MainClass.AllowSingleQuote(Trim(mRunningBalStr)) & "') "
                PubDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        InsertBook = True
        Exit Function
ERR1:
        'Resume
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
        InsertBook = False
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        'Dim mOpeningDr As Double
        'Dim mOpeningCr As Double
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        '    SprdLedg.Row = 1
        '    SprdLedg.Col = ColDAmount
        '    mOpeningDr = SprdLedg.Text
        '    SprdLedg.Col = ColCAmount
        '    mOpeningCr = SprdLedg.Text
        '
        '    MainClass.AssignCRptFormulas Report1, "OpeningBalDr=""" & mOpeningDr & """"
        '    MainClass.AssignCRptFormulas Report1, "OpeningBalDr=""" & mOpeningCr & """"
        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        On Error GoTo ERR1
        Dim PrintStatus As Boolean
        '    If TxtAccount.Text = "" Then PrintStatus = False Else PrintStatus = True
        If lblBookType.Text = ConBankBook Or lblBookType.Text = ConCashBook Or lblBookType.Text = ConPDCBook Then
            If TxtAccount.Text = "" Then Exit Sub
        End If
        If G_PrintLedg = False Then
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If mDOSPRINTING = True Then
            Call BookReport("P")
        Else
            Call ReportForBook(Crystal.DestinationConstants.crptToPrinter)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function BookReport(ByRef pPrintMode As String) As Boolean
        On Error GoTo ErrPart
        Dim mLineCount As Integer
        Dim pPageNo As Integer
        Dim cntRow As Double
        Dim mPrintFooter As Boolean
        Dim pFileName As String
        Dim mDayBalance As Double
        Dim mVDate As String
        Dim xVDate As String
        Dim mOPBalance As Double
        Dim mDC As String
        Dim mMKey As String
        Dim xMkey As String
        mLineCount = 1
        pFileName = mLocalPath & "\Report.Prn"
        ''Shell "ATTRIB +A -R " & pFileName
        Call ShellAndContinue("ATTRIB +A -R " & pFileName)
        With SprdLedg
            If .MaxRows >= 1 Then
                FileOpen(1, pFileName, OpenMode.Output)
                For cntRow = 1 To .MaxRows - 1
                    If mLineCount = 1 Then
                        pPageNo = pPageNo + 1
                        Call PrintHeader(mLineCount)
                        mPrintFooter = False
                    End If
                    .Row = cntRow
                    If cntRow = 1 Then GoTo NextRow
                    .Col = ColMKEY
                    If mMKey <> Trim(.Text) Then
                        xMkey = Trim(.Text)
                    Else
                        xMkey = ""
                    End If
                    mMKey = Trim(.Text)
                    .Col = ColVDate
                    If mVDate <> Trim(.Text) Then
                        xVDate = Trim(.Text)
                    Else
                        xVDate = ""
                    End If
                    mVDate = Trim(.Text)
                    .Row = cntRow - 1
                    .Col = ColBalDC
                    mDC = Trim(.Text)
                    .Col = ColBalance
                    mOPBalance = Val(.Text)
                    Call PrintDetail(cntRow, mLineCount, xVDate, VB6.Format(mOPBalance, "0.00") & " " & mDC, xMkey)
                    '                mLineCount = mLineCount + 1
                    If mLineCount >= 60 And mPrintFooter = False Then
                        Call PrintFooter(pPageNo, mLineCount, mPrintFooter)
                    ElseIf cntRow = SprdLedg.MaxRows - 1 Then
                        Do While mLineCount <= 60
                            PrintLine(1, " ")
                            mLineCount = mLineCount + 1
                        Loop
                        PrintLine(1, TAB(0), Chr(20) & New String("-", mPageWidth) & Chr(15))
                        mLineCount = mLineCount + 1
                        .Row = SprdLedg.MaxRows
                        .Col = ColDAmount
                        Print(1, TAB(TabDAmount), New String(" ", TabCAmount - TabDAmount - Len(Trim(.Text))) & Trim(.Text))
                        .Col = ColCAmount
                        PrintLine(1, TAB(TabCAmount), New String(" ", TabBalance - TabCAmount - Len(Trim(.Text))) & Trim(.Text))
                        Call PrintFooter(pPageNo, mLineCount, mPrintFooter)
                    End If
NextRow:
                Next
                FileClose(1)
            End If
        End With
        Dim mFP As Boolean
        If pPrintMode = "P" Then
            mFP = Shell(My.Application.Info.DirectoryPath & "\PrintReport.bat", AppWinStyle.NormalFocus)
            If mFP = False Then GoTo ErrPart
            '        Shell App.path & "\PrintReport.bat",vbNormalFocus
        Else
            Shell("ATTRIB +R -A " & pFileName)
            Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
            'App.Path & "\RVIEW.EXE "
        End If
        BookReport = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        BookReport = False
        ''Resume
        FileClose(1)
    End Function
    Private Function PrintHeader(ByRef mLineCount As Integer) As Boolean
        On Error GoTo ErrPart
        Dim mTitle As String
        PrintLine(1, TAB(0), " ")
        mLineCount = mLineCount + 1
        If chkWideFormat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            PrintLine(1, TAB(0), " " & Chr(15))
        Else
            PrintLine(1, TAB(0), " " & Chr(18))
        End If
        mLineCount = mLineCount + 1
        PrintLine(1, TAB(0), Chr(14) & RsCompany.Fields("COMPANY_NAME").Value)
        mLineCount = mLineCount + 1
        PrintLine(1, TAB(0), " ") ''xCompanyAddr
        mLineCount = mLineCount + 1
        '    Print #1, Tab(0); UCase(Me.text)            '' "BANK BOOK "
        '    mLineCount = mLineCount + 1
        If lblBookType.Text = "C" Then
            mTitle = "Name : " & UCase(TxtAccount.Text)
        ElseIf lblBookType.Text = "B" Then
            mTitle = "Bank Name : " & UCase(TxtAccount.Text)
        Else
            mTitle = UCase(Me.Text)
        End If
        PrintLine(1, TAB(0), Chr(27) & Chr(69) & mTitle & Chr(27) & Chr(70))
        mLineCount = mLineCount + 1
        PrintLine(1, TAB(0), Chr(27) & Chr(69) & "For the period : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & "-" & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & Chr(27) & Chr(70))
        mLineCount = mLineCount + 1
        PrintLine(1, TAB(0), New String("-", mPageWidth))
        mLineCount = mLineCount + 1
        Print(1, TAB(TabRefNo), "No.")
        Print(1, TAB(TabName), "On Account Of..")
        Print(1, TAB(TabDesc), "Description")
        If lblBookType.Text = "B" Or lblBookType.Text = "C" Then
            Print(1, TAB(TabCheque), "Cheque No")
            Print(1, TAB(TabDAmount), New String(" ", TabCAmount - TabDAmount - Len("Deposit (Rs)")) & "Deposit (Rs)")
            Print(1, TAB(TabCAmount), New String(" ", TabBalance - TabCAmount - Len("Payment (Rs)")) & "Payment (Rs)")
            PrintLine(1, TAB(TabBalance), New String(" ", mPageWidth - TabBalance - Len("Balance (Rs)")) & "Balance (Rs)")
        Else
            '        Print #1, Tab(TabCheque); "Cheque No";
            '        Print #1, Tab(TabDAmount); String(TabCAmount - TabDAmount - Len("Deposit (Rs)"), " ") & "Deposit (Rs)";
            Print(1, TAB(TabCAmount), New String(" ", TabBalance - TabCAmount - Len("Debit (Rs)")) & "Debit (Rs)")
            PrintLine(1, TAB(TabBalance), New String(" ", mPageWidth - TabBalance - Len("Credit (Rs)")) & "Credit (Rs)")
        End If
        mLineCount = mLineCount + 1
        PrintLine(1, TAB(0), New String("-", mPageWidth))
        mLineCount = mLineCount + 1
        PrintHeader = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintHeader = False
        'Resume
    End Function
    Private Function PrintFooter(ByRef xPageNo As Integer, ByRef mLineCount As Integer, ByRef pPrintFooter As Boolean) As Boolean
        On Error GoTo ErrPart
        Do While mLineCount <= 65
            PrintLine(1, " ")
            mLineCount = mLineCount + 1
        Loop
        PrintLine(1, TAB(0), Chr(20) & New String("-", mPageWidth))
        Print(1, TAB(TabCAmount), VB6.Format(RunDate, "DD/MM/YYYY"))
        PrintLine(1, TAB(TabBalance), "Page No. : " & xPageNo)
        PrintLine(1, TAB(0), Chr(12))
        mLineCount = 1
        PrintFooter = True
        pPrintFooter = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintFooter = False
        pPrintFooter = False
    End Function
    Private Function PrintDetail(ByRef mRow As Double, ByRef mLineCount As Integer, ByRef pDate As String, ByRef pBalance As String, ByRef pMKey As String) As Boolean
        On Error GoTo ErrPart
        Dim mAcctName As String
        Dim mNarration As String
        Dim mBillDetail As String
        Dim mChequeNo As String
        Dim mRemarks As String
        Dim mBalDC As String
        Dim mBalance As String
        Dim mDAmount As String
        Dim mCAmount As String
        With SprdLedg
            .Row = mRow
            If pDate <> "" Then
                PrintLine(1, TAB(0), " ")
                mLineCount = mLineCount + 1
                If lblBookType.Text = "B" Or lblBookType.Text = "C" Then
                    Print(1, TAB(TabRefNo), VB6.Format(pDate, "DDDD, dd/mm/yyyy"))
                    Print(1, TAB(TabDAmount), "Opening Balance : ")
                    PrintLine(1, TAB(TabBalance), New String(" ", mPageWidth - TabBalance - Len(Trim(pBalance))) & Trim(pBalance))
                    mLineCount = mLineCount + 1
                    PrintLine(1, TAB(0), " ")
                    mLineCount = mLineCount + 1
                Else
                    PrintLine(1, TAB(TabRefNo), VB6.Format(pDate, "DDDD, dd/mm/yyyy"))
                    mLineCount = mLineCount + 1
                End If
            End If
            If pMKey <> "" Then
                If lblBookType.Text = "B" Or lblBookType.Text = "C" Then
                Else
                    PrintLine(1, TAB(0), " ")
                    mLineCount = mLineCount + 1
                End If
            End If
            .Col = ColVNo
            Print(1, TAB(TabRefNo), Trim(.Text))
            .Col = colAccount
            mRemarks = GetMultiLine(Trim(.Text), mLineCount, TabDesc - TabName - 1, TabName)
            Print(1, TAB(TabName), Trim(mRemarks))
            .Col = ColNarration
            mRemarks = Replace(Trim(.Text), vbCrLf, " ")
            If lblBookType.Text = "B" Or lblBookType.Text = "C" Then
                mRemarks = GetMultiLine(mRemarks, mLineCount, TabCheque - TabDesc - 1, TabDesc)
            Else
                mRemarks = GetMultiLine(mRemarks, mLineCount, TabCAmount - TabDesc - 1, TabDesc)
            End If
            Print(1, TAB(TabDesc), Trim(mRemarks))
            If lblBookType.Text = "B" Or lblBookType.Text = "C" Then
                .Col = ColChequeNo
                mRemarks = GetMultiLine(Trim(.Text), mLineCount, TabDAmount - TabCheque - 1, TabCheque)
                Print(1, TAB(TabCheque), Trim(mRemarks))
            End If
            If lblBookType.Text = "B" Or lblBookType.Text = "C" Then
                .Col = ColDAmount
                mDAmount = IIf(Val(.Text) = 0, "", Trim(.Text))
                Print(1, TAB(TabDAmount), New String(" ", TabCAmount - TabDAmount - Len(mDAmount)) & mDAmount)
                .Col = ColCAmount
                mDAmount = IIf(Val(.Text) = 0, "", Trim(.Text))
                Print(1, TAB(TabCAmount), New String(" ", TabBalance - TabCAmount - Len(mDAmount)) & mDAmount)
                .Col = ColBalDC
                mBalDC = Trim(.Text)
                .Col = ColBalance
                mBalance = Trim(.Text) & " " & mBalDC
                PrintLine(1, TAB(TabBalance), New String(" ", mPageWidth - TabBalance - Len(Trim(mBalance))) & Trim(mBalance))
            Else
                .Col = ColDAmount
                mDAmount = IIf(Val(.Text) = 0, "", Trim(.Text))
                Print(1, TAB(TabCAmount), New String(" ", TabBalance - TabCAmount - Len(mDAmount)) & mDAmount)
                .Col = ColCAmount
                mDAmount = IIf(Val(.Text) = 0, "", Trim(.Text))
                PrintLine(1, TAB(TabBalance), New String(" ", mPageWidth - TabBalance - Len(mDAmount)) & mDAmount)
                '            .Col = ColBalDC
                '            mBalDC = Trim(.Text)
                '
                '            .Col = ColBalance
                '            mBalance = Trim(.Text) & " " & mBalDC
                '            Print #1, Tab(TabBalance); String(mPageWidth - TabBalance - Len(Trim(mBalance)), " ") & Trim(mBalance)
            End If
            mLineCount = mLineCount + 1
        End With
        PrintDetail = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintDetail = False
        ''Resume
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdLedg, RowHeight)
        If lblBookType.Text = ConCashBook Or lblBookType.Text = ConBankBook Or lblBookType.Text = ConPDCBook Then ''Add PDC BOOK 17/10/2017
            If BookInfo() = False Then GoTo ErrPart
        Else
            If LedgInfo() = False Then GoTo ErrPart
        End If
        SprdLedg.Focus()
        Call PrintStatus(True)
        MainClass.SetFocusToCell(SprdLedg, mActiveRow, 4)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub lstCompanyName_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstCompanyName.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCompanyName.GetItemChecked(0) = True Then
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCompanyName.GetItemChecked(e.Index - 1) = False Then
                    lstCompanyName.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
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
            .Col2 = .MaxCols
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
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '' &H8000000B             ''&H80FF80
            .BlockMode = False
            '        Call CalcRowTotal(SprdLedg, ColDAmount, 1, ColDAmount, .MaxRows - 1, .MaxRows, ColDAmount)
            '        Call CalcRowTotal(SprdLedg, ColCAmount, 1, ColCAmount, .MaxRows - 1, .MaxRows, ColCAmount)
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
            FormatSprdLedg(.MaxRows)
        End With
        Call FillRunBalCol()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Public Sub frmViewBook_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call GetFormCaption((lblBookType.Text))
        TxtAccount.Visible = True
        If lblBookType.Text = ConLedger Then
            FraAccount.Text = "Accounts"
        Else
            FraAccount.Text = "Book"
        End If
        If lblBookType.Text = ConPDCBook Then
            chkGroup(8).CheckState = System.Windows.Forms.CheckState.Checked
            chkGroup(8).Enabled = True
            chkGroup(0).CheckState = System.Windows.Forms.CheckState.Unchecked
            chkGroup(0).Enabled = False
            chkGroup(1).CheckState = System.Windows.Forms.CheckState.Unchecked
            chkGroup(1).Enabled = False
            chkGroup(2).CheckState = System.Windows.Forms.CheckState.Unchecked
            chkGroup(2).Enabled = False
            chkGroup(3).CheckState = System.Windows.Forms.CheckState.Unchecked
            chkGroup(3).Enabled = False
            chkGroup(4).CheckState = System.Windows.Forms.CheckState.Unchecked
            chkGroup(4).Enabled = False
            chkGroup(5).CheckState = System.Windows.Forms.CheckState.Unchecked
            chkGroup(5).Enabled = False
            chkGroup(6).CheckState = System.Windows.Forms.CheckState.Unchecked
            chkGroup(6).Enabled = False
            chkGroup(7).CheckState = System.Windows.Forms.CheckState.Unchecked
            chkGroup(7).Enabled = False
        Else
            chkGroup(8).CheckState = System.Windows.Forms.CheckState.Unchecked
            chkGroup(8).Enabled = False
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdLedg(-1)
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewBook_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        Call frmViewBook_Activated(eventSender, eventArgs)
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

        Dim CntLst As Long
        Dim mCompanyName As String
        Dim mCompanyAdd As String
        Dim RS As ADODB.Recordset
        Dim SqlStr As String

        mCompanyAdd = IIf(IsDBNull(RsCompany.Fields("COMPANY_SHORTNAME").Value), "", RsCompany.Fields("COMPANY_SHORTNAME").Value)
        mCompanyAdd = mCompanyAdd & ", " & IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME || ', ' ||  COMPANY_ADDR AS COMPANY_NAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_NAME").Value)
                mCompanyName = IIf(IsDBNull(RS.Fields("COMPANY_NAME").Value), "", RS.Fields("COMPANY_NAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(mCompanyName = mCompanyAdd, True, False))      '' RsCompany.Fields("COMPANY_NAME").Value
                'lstCompanyName.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewBook_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
    Private Sub frmViewBook_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
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
        Dim SqlStr As String
        Dim xVDate As String
        Dim xMkey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String
        Dim pIndex As Integer
        Dim xVTYPE As String

        If OptSumDet(0).Checked = True Then

            SprdLedg.Row = SprdLedg.ActiveRow
            SprdLedg.Col = ColVDate
            xVDate = Me.SprdLedg.Text

            SprdLedg.Col = ColMKEY
            xMkey = Me.SprdLedg.Text

            SprdLedg.Col = ColVNo
            xVNo = Me.SprdLedg.Text

            SprdLedg.Col = ColBookType
            xBookType = Me.SprdLedg.Text

            SprdLedg.Col = ColBookSubType
            xBookSubType = Me.SprdLedg.Text

            SprdLedg.Col = ColBranch
            If RsCompany.Fields("COMPANY_SHORTNAME").Value <> Me.SprdLedg.Text Then
                MsgInformation("Cann't Open Other Unit Voucher.")
                Exit Sub
            End If

            If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Or xBookType = "R" Or xBookType = "E" Then
                '            xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
                '            xVNo = Right(xVNo, 5)
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                    & " AND MKEY='" & xMkey & "'" & vbCrLf _
                    & " AND BOOKTYPE='" & xBookType & "'" & vbCrLf _
                    & " AND BOOKSUBTYPE='" & xBookSubType & "'" & vbCrLf _
                    & " AND VDATE=TO_DATE('" & VB6.Format(xVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                If MainClass.ValidateWithMasterTable(xVNo, "VNO", "VTYPE", "FIN_POSTED_TRN", PubDBCn, MasterNo, , SqlStr) = True Then
                    xVTYPE = MasterNo
                    xVNo = Mid(xVNo, Len(xVTYPE) + 1)
                Else
                    Exit Sub
                End If
            End If
            Call ShowTrn(xMkey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, Me)
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
        Dim ss As New frmViewBook
        Dim mFromDate As String
        Dim mToDate As String
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ss.MdiParent = Me.MdiParent
        '    If lblBookType.text = ConLedger Then
        ss.lblBookType.Text = lblBookType.Text
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
        ss.frmViewBook_Activated(Nothing, New System.EventArgs())
        ss.cmdShow_Click(Nothing, New System.EventArgs())
        '    End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub SprdLedg_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdLedg.KeyDownEvent
        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Then
            SprdLedg_DblClick(SprdLedg, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        If eventArgs.keyCode = System.Windows.Forms.Keys.Escape Then cmdClose.PerformClick()
    End Sub
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
    Private Sub SearchAccounts()
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        Select Case lblBookType.Text
            Case ConLedger
                SqlStr = SqlStr
            Case ConCashBook
                SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '1'"
            Case ConBankBook, ConPDCBook
                SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '2'"
            Case Else
                SqlStr = SqlStr & " AND 1=2"
        End Select

        If MainClass.SearchGridMaster((TxtAccount.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                TxtAccount.Text = AcName
            End If
        End If

        'MainClass.SearchMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr)
        'If AcName <> "" Then
        '    TxtAccount.Text = AcName
        'End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchVType()
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'"
        If MainClass.SearchGridMaster((txtVType.Text), "FIN_VOUCHERTYPE_MST", "VTYPE", "VNAME", , , SqlStr) = True Then
            If AcName <> "" Then
                txtVType.Text = AcName
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
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
            .ColHidden = False
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
            .set_ColWidth(ColVNo, 7)
            If OptSumDet(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
            .Col = colAccount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colAccount, 20)
            If OptSumDet(0).Checked = True Then
                .ColHidden = False
                .ColsFrozen = colAccount
            Else
                .ColHidden = True
            End If
            .Col = ColNarration
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColNarration, 18)
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
                If lblBookType.Text = ConBankBook Or lblBookType.Text = ConPDCBook Then
                    .ColHidden = False
                Else
                    .ColHidden = True
                End If
            Else
                .ColHidden = True
            End If
            .Col = ColDAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDAmount, 11)
            .Col = ColCAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColCAmount, 11)
            .Col = ColBalance
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBalance, 11)
            .ColHidden = False
            .Col = ColBalDC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBalDC, 3)
            .ColHidden = False
            '        If ChkWithRunBal.Value = vbUnchecked Then
            '            .Col = ColBalance
            '            .ColHidden = True
            '            .Col = ColBalDC
            '            .ColHidden = True
            '        End If
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

            .Col = ColAddUser
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAddUser, 8)

            .Col = ColAddDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAddDate, 8)

            .Col = ColModUser
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColModUser, 8)

            .Col = ColModDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColModDate, 8)


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
            .ColHidden = False ''True ''IIf(Left(cboConsolidated.Text, 1) = "D", True, False)

            Call FillHeading()
            MainClass.SetSpreadColor(SprdLedg, -1)
            MainClass.ProtectCell(SprdLedg, 1, .MaxRows, 1, .MaxCols)
            SprdLedg.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            SprdLedg.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
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
                .Text = IIf(mBalance >= 0, "Dr", "Cr")
            Next
            mBalance = 0
            For ii = .MaxRows To .MaxRows
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
        LedgInfo = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr1 = MakeSQL()
        SqlStr2 = MakeSQLJoining(False, "")
        SqlStr = SqlStr1 & vbCrLf & SqlStr2
        If chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.AMOUNT " & cboCond.Text & Val(txtCondAmount.Text) & "" ''* DECODE(TRN.DC,'D',1,-1)
        End If
        If lblBookType.Text = ConJournalBook Then
            SqlStr = SqlStr & vbCrLf & " AND BookCode='" & ConJournalBookCode & "' "
        ElseIf lblBookType.Text = ConContraBook Then
            SqlStr = SqlStr & vbCrLf & " AND BookCode='" & ConContraBookCode & "' "
        ElseIf lblBookType.Text = ConDebitNoteBook Then
            SqlStr = SqlStr & vbCrLf & " AND BookCode='" & ConDebitNoteBookCode & "' "
        ElseIf lblBookType.Text = ConCreditNoteBook Then
            SqlStr = SqlStr & vbCrLf & " AND BookCode='" & ConCreditNoteBookCode & "' "
        Else
            '        SqlStr = SqlStr & vbCrLf & "AND Trn.AccountCode = " & mAccountCode & " "
            '        SqlStr = SqlStr & vbCrLf & " AND BookCode='" & mAccountCode & "'"
            SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE<>'" & mAccountCode & "' " & vbCrLf & " AND TRN.BOOKTYPE ||TRN.MKEY IN (SELECT TRN.BOOKTYPE || TRN.MKEY " & SqlStr2 & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "')"
        End If
        SqlStr = SqlStr & vbCrLf & MakeSQLCond(False)

        If OptSumDet(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.VDATE," & vbCrLf _
                & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE , TRN.VNO,  " & vbCrLf _
                & " ACM.SUPP_CUST_NAME,TRN.NARRATION, TRN.REMARKS,TRN.MKEY, COMP.COMPANY_SHORTNAME,CHEQUENO,TRN.ADDUSER, TRN.ADDDATE, TRN.MODUSER, TRN.MODDATE  " & vbCrLf _
                & " ORDER BY TRN.VDATE, TRN.VNO"
            ''|| ' ' || TO_CHAR(CHQDATE,'DD/MM/YYYY')
            ''& " TRN.LOCKED, TRN.CHEQUENO, DEPT.DEPT_CODE,EMP.EMP_CODE,COSTC.COST_CENTER_CODE,TRN.MKEY,"
        ElseIf OptSumDet(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.VDATE " & vbCrLf & " ORDER BY TRN.VDATE "
        ElseIf OptSumDet(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY TO_CHAR(Vdate,'MON-YYYY'),TO_CHAR(Vdate,'YYYYMM')" & vbCrLf & " ORDER BY TO_CHAR(Vdate,'YYYYMM')"
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdLedg, StrConn, "Y")

        '********************************
        'Get Opening Balance.........
        SqlStr1 = MakeOPSQL()
        SqlStr2 = MakeSQLJoining(True, "")
        SqlStr = SqlStr1 & vbCrLf & SqlStr2 & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
        SqlStr = SqlStr & vbCrLf & MakeSQLCond(True)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOP, ADODB.LockTypeEnum.adLockReadOnly)
        If RsOP.EOF = False Then
            mOpening = IIf(IsDBNull(RsOP.Fields("OPENING").Value), 0, RsOP.Fields("OPENING").Value)
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
    Private Function BookInfo() As Boolean
        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        BookInfo = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If InsertIntoTemp() = False Then GoTo LedgError

        SqlStr = ""
        SqlStr1 = MakeSQL()
        SqlStr2 = MakeSQLJoining(False, "CB")
        SqlStr = SqlStr1 & vbCrLf & SqlStr2

        If chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.AMOUNT " & cboCond.Text & Val(txtCondAmount.Text) & "" ''* DECODE(TRN.DC,'D',1,-1)
        End If
        SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE<>'" & mAccountCode & "' "
        SqlStr = SqlStr & vbCrLf & MakeSQLCond(False)
        If OptSumDet(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.VDATE," & vbCrLf _
                & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE, TRN.VNO,  " & vbCrLf _
                & " ACM.SUPP_CUST_NAME,TRN.NARRATION, TRN.REMARKS,TRN.MKEY, COMP.COMPANY_SHORTNAME, CHEQUENO ,TRN.DC,TRN.ADDUSER, TRN.ADDDATE, TRN.MODUSER, TRN.MODDATE " & vbCrLf _
                & " ORDER BY TRN.VDATE, TRN.DC," & vbCrLf & " TRN.BOOKSUBTYPE DESC, TRN.BOOKTYPE, TRN.VNO"
            ''DECODE(TRN.BOOKTYPE,'J','A',TRN.BOOKSUBTYPE)
        ElseIf OptSumDet(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.VDATE " & vbCrLf & " ORDER BY TRN.VDATE "
        ElseIf OptSumDet(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY TO_CHAR(Vdate,'MON-YYYY'),TO_CHAR(Vdate,'YYYYMM')" & vbCrLf & " ORDER BY TO_CHAR(Vdate,'YYYYMM')"
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdLedg, StrConn, "Y")
        '********************************
        'Get Opening Balance.........
        SqlStr1 = MakeOPSQL()
        SqlStr2 = MakeSQLJoining(True, "")
        SqlStr = SqlStr1 & vbCrLf & SqlStr2 & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'"
        SqlStr = SqlStr & vbCrLf & MakeSQLCond(True)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOP, ADODB.LockTypeEnum.adLockReadOnly)
        If RsOP.EOF = False Then
            mOpening = IIf(IsDBNull(RsOP.Fields("OPENING").Value), 0, RsOP.Fields("OPENING").Value)
            If mOpening >= 0 Then
                mOpDr = mOpening
                mOpCr = 0
            Else
                mOpDr = 0
                mOpCr = System.Math.Abs(mOpening)
            End If
        End If
        DisplayTotals(mOpDr, mOpCr)
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
        SqlStr2 = MakeSQLJoining(False, "")
        SqlStr = SqlStr1 & vbCrLf & SqlStr2
        '    SqlStr = SqlStr & vbCrLf & " AND ACM.SUPP_CUST_CODE='" & mAccountCode & "'"
        SqlStr = SqlStr & vbCrLf & " AND TRN.ACCOUNTCODE='" & mAccountCode & "'"
        SqlStr = SqlStr & vbCrLf & MakeSQLCond(False)
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

        If OptSumDet(0).Checked = True Then
            SqlStr = " SELECT '' AS LOCKED, TRN.BOOKTYPE, TRN.BOOKSUBTYPE , " & vbCrLf & "  TO_CHAR(TRN.VDATE,'DD/MM/YYYY'),TRN.VNO AS V_NO, " & vbCrLf & " ACM.SUPP_CUST_NAME, TRN.NARRATION, "
            If lblBookType.Text = ConJournalBook Then
                SqlStr = SqlStr & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',-1,1)) ELSE 0 END,'9,99,99,99,999.99') AS DEBIT, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1)) ELSE 0 END,'9,99,99,99,999.99') AS CREDIT, "
            Else
                SqlStr = SqlStr & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1)) ELSE 0 END,'9,99,99,99,999.99') AS DEBIT, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',-1,1)) ELSE 0 END,'9,99,99,99,999.99') AS CREDIT, "
            End If
            ''|| ' ' || TO_CHAR(CHQDATE,'DD/MM/YYYY')
            SqlStr = SqlStr & vbCrLf & " '','', " & vbCrLf _
                & " TRN.REMARKS, " & vbCrLf _
                & " CHEQUENO  AS CHEQUENO, " & vbCrLf _
                & " '' AS DEPT_CODE,'' AS EMP_CODE,'' AS COST_CENTER_CODE, TRN.ADDUSER, TRN.ADDDATE, TRN.MODUSER, TRN.MODDATE, TRN.MKEY,'',COMP.COMPANY_SHORTNAME "
            ''& " DEPT.DEPT_CODE,EMP.EMP_CODE,COSTC.COST_CENTER_CODE,TRN.MKEY,'','' "
        ElseIf OptSumDet(1).Checked = True Then
            SqlStr = " SELECT '', '','',TO_CHAR(TRN.VDATE,'DD/MM/YYYY'), " & vbCrLf & " '','', " & vbCrLf & " '', "
            If lblBookType.Text = ConJournalBook Then
                SqlStr = SqlStr & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',-1,1)) ELSE 0 END,'9,99,99,99,999.99') AS DEBIT, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1)) ELSE 0 END,'9,99,99,99,999.99') AS CREDIT, "
            Else
                SqlStr = SqlStr & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1)) ELSE 0 END,'9,99,99,99,999.99') AS DEBIT, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',-1,1)) ELSE 0 END,'9,99,99,99,999.99') AS CREDIT, "
            End If
            SqlStr = SqlStr & vbCrLf & " '','', " & vbCrLf & " '',  " & vbCrLf & " '', " & vbCrLf & " '' AS DEPT_CODE,'' AS EMP_CODE,'' AS COST_CENTER_CODE,'','','','','','','' "
        ElseIf OptSumDet(2).Checked = True Then
            SqlStr = " SELECT '','','', TO_CHAR(Vdate,'MON-YYYY'), " & vbCrLf & " '','', " & vbCrLf & " '', "
            If lblBookType.Text = ConJournalBook Then
                SqlStr = SqlStr & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',-1,1)) ELSE 0 END,'9,99,99,99,999.99') AS DEBIT, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1)) ELSE 0 END,'9,99,99,99,999.99') AS CREDIT, "
            Else
                SqlStr = SqlStr & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1))>=0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1)) ELSE 0 END,'9,99,99,99,999.99') AS DEBIT, " & vbCrLf & " TO_CHAR(CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1))<0 THEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',-1,1)) ELSE 0 END,'9,99,99,99,999.99') AS CREDIT, "
            End If
            SqlStr = SqlStr & vbCrLf & " '','', " & vbCrLf & " '', " & vbCrLf & " '', " & vbCrLf & " '' AS DEPT_CODE,'' AS EMP_CODE,'' AS COST_CENTER_CODE,'','','','','','','' "
        End If

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
    Private Function MakeSQLJoining(ByRef mIsOpening As Boolean, ByRef mBookView As String) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCostCName As String
        Dim mDeptName As String
        Dim mEmp As String
        Dim mConsolidated As String
        Dim mGroupOption As String

        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""
        Dim mCompanyCodeStr As String = ""


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
        SqlStr = " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM, GEN_COMPANY_MST COMP"
        If mBookView = "CB" Then
            SqlStr = SqlStr & ", Temp_ViewBook"
        End If
        SqlStr = SqlStr & vbCrLf _
            & " WHERE COMP.Company_Code=TRN.Company_Code" & vbCrLf _
            & " AND TRN.Company_Code=ACM.Company_Code " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "

        ''& " TRN.Company_Code=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(0) = True Then
                    mCompanyCodeStr = ""
                Else
                    If lstCompanyName.GetItemChecked(CntLst) = True Then
                        mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                        If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME || ', ' || COMPANY_ADDR", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                            mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                        End If
                        mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                    End If
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        If lblBookType.Text = ConPDCBook Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR = " & RsCompany.Fields("FYEAR").Value & ""
        End If
        If mBookView = "CB" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND Temp_ViewBook.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
                & " AND TRN.BOOKTYPE =Temp_ViewBook.BOOKTYPE AND TRN.MKEY =Temp_ViewBook.MKEY"
        End If
        '    mGroupOption = GetGroupOption
        '    If mIsOpening = True Then
        '        mGroupOption = mGroupOption & vbCrLf & IIf(mGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConOpeningBook & "'"
        '    End If
        '
        '    If mGroupOption <> "" Then
        '        SqlStr = SqlStr & vbCrLf & " And ( " & mGroupOption & " ) "
        '    End If
        '
        '    If Trim(txtVType.Text) <> "" Then
        '        SqlStr = SqlStr & vbCrLf & " AND VTYPE='" & MainClass.AllowSingleQuote(txtVType.Text) & "'"
        '    End If
        '
        '     If mIsOpening = True Then
        '        SqlStr = SqlStr & vbCrLf & " AND TRN.Vdate<'" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "' "
        '    Else
        '        SqlStr = SqlStr & " vbCrLf &" _
        ''                & " AND TRN.Vdate BETWEEN TO_DATE('" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "') " & vbCrLf _
        ''                & " AND TO_DATE('" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"
        '    End If
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND TRN.FYEAR = " & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
        ''            & " AND TRN.Company_Code=" & RsCompany.Fields("Company_Code").Value & " "
        MakeSQLJoining = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLJoining = ""
    End Function
    Private Function MakeSQLCond(ByRef mIsOpening As Boolean) As String
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mGroupOption As String
        mGroupOption = GetGroupOption()

        If mIsOpening = True Then
            mGroupOption = mGroupOption & IIf(mGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConOpeningBook & "'"
        End If
        If mGroupOption <> "" Then
            SqlStr = SqlStr & " And ( " & mGroupOption & " ) "
        End If
        If Trim(txtVType.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND VTYPE='" & MainClass.AllowSingleQuote(txtVType.Text) & "'"
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.MKEY || TRN.BOOKTYPE NOT IN (SELECT MKEY || BOOKTYPE FROM FIN_POSTED_TRN A1, FIN_SUPP_CUST_MST B1 WHERE A1.COMPANY_CODE=B1.COMPANY_CODE AND A1.ACCOUNTCODE=B1.SUPP_CUST_CODE AND ACCOUNT_HIDE='Y')"
            ''SqlStr = SqlStr & vbCrLf & " AND TRN.MKEY || TRN.BOOKTYPE NOT IN (SELECT MKEY || BOOKTYPE FROM FIN_POSTED_TRN WHERE SUPP_CUST_CODE = '11848')"
        End If

        If mIsOpening = True Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.Vdate<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN.Vdate BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        MakeSQLCond = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLCond = ""
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
            GetGroupOption = GetGroupOption & vbCrLf & IIf(GetGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConSaleBook & "'  OR TRN.BOOKTYPE = '" & ConSaleDebitBook & "'"
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
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus() : Exit Function
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If lblBookType.Text = ConPDCBook Then
        Else
            If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus() : Exit Function
        End If
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            Select Case lblBookType.Text
                Case ConLedger
                    MsgInformation("Please Select Account")
                    Exit Function
                Case ConCashBook, ConBankBook, ConPDCBook
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
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub FillHeading()
        On Error GoTo ErrPart
        With SprdLedg
            .Row = 0
            .Col = ColDAmount
            If lblBookType.Text = ConBankBook Then
                .Text = "Deposit (Rs.)"
            ElseIf lblBookType.Text = ConCashBook Then
                .Text = "Receipts (Rs.)"
            Else
                .Text = "Debit (Rs.)"
            End If
            .Col = ColCAmount
            If lblBookType.Text = ConBankBook Then
                .Text = "Payment (Rs.)"
            ElseIf lblBookType.Text = ConCashBook Then
                .Text = "Payment (Rs.)"
            Else
                .Text = "Credit (Rs.)"
            End If
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
        If lblBookType.Text = ConPDCBook Then
        Else
            If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
                txtDateTo.Focus()
                Cancel = True
                GoTo EventExitSub
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtVType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVType.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtVType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVType.DoubleClick
        Call SearchVType()
    End Sub
    Private Sub txtVType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVType()
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
    Private Sub SprdLedg_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdLedg.KeyPressEvent
        Try
            If e.keyAscii = 18 Then
                Dim mFieldValue As String
                Dim I As Long
                Dim mDelRow As Long

                SprdLedg.DataSource = Nothing


                mDelRow = SprdLedg.ActiveRow

                SprdLedg.Row = mDelRow
                SprdLedg.Col = ColVNo
                mFieldValue = Trim(SprdLedg.Text)

                If mFieldValue <> "" Then
                    SprdLedg.DeleteRows(mDelRow, 1)

                    If SprdLedg.MaxRows > 1 Then SprdLedg.MaxRows = SprdLedg.MaxRows - 1

                    Call FillRunBalCol()

                    FormatSprdLedg(-1)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
