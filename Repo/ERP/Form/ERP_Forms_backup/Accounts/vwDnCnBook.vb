Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDnCnViewBook
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
    Private Const ColBillNo As Short = 6
    Private Const colAccount As Short = 7
    Private Const ColNarration As Short = 8
    Private Const ColAccount2 As Short = 9
    Private Const ColAmount As Short = 10
    Private Const ColTaxableAmount As Short = 11
    Private Const ColCGSTAmount As Short = 12
    Private Const ColSGSTAmount As Short = 13
    Private Const ColIGSTAmount As Short = 14
    Private Const ColMKEY As Short = 15
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
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
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
        SqlStr = MakeSQL
        If lblBookType.Text = ConDebitNoteBook Then
            mTitle = "Debit Note Register"
        ElseIf lblBookType.Text = ConCreditNoteBook Then
            mTitle = "Credit Note Register"
        End If
        If OptSumDet(0).Checked = True Then
            mRPTName = "DNCNBook.rpt"
        ElseIf OptSumDet(1).Checked = True Then
            mTitle = mTitle & " - Daily"
            mRPTName = "DNCN_DAILY.Rpt"
        ElseIf OptSumDet(2).Checked = True Then
            mTitle = mTitle & " - Monthly"
            mRPTName = "DNCN_MONTHLY.Rpt"
        End If
        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")
        mSubTitle = mSubTitle & IIf(cboType.Text = "ALL", "", "-" & cboType.Text)
        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)
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
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
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
        'On Error GoTo ErrPart
        'Dim mLineCount As Long
        'Dim pPageNo As Long
        'Dim cntRow As Double
        'Dim mPrintFooter As Boolean
        'Dim pFileName As String
        'Dim mDayBalance As Double
        'Dim mVDate As String
        'Dim xVDate As String
        'Dim mOPBalance As Double
        'Dim mDC As String
        '
        '    mLineCount = 1
        '    pFileName = mLocalPath & "\Report.Prn"
        '    ''Shell "ATTRIB +A -R " & pFileName
        '
        '    Call ShellAndContinue("ATTRIB +A -R " & pFileName)
        '
        '    With SprdLedg
        '        If .MaxRows >= 1 Then
        '
        '            Open pFileName For Output As #1
        '            For cntRow = 1 To .MaxRows - 1
        '                If mLineCount = 1 Then
        '                    pPageNo = pPageNo + 1
        '                    Call PrintHeader(mLineCount)
        '                    mPrintFooter = False
        '                End If
        '
        '                .Row = cntRow
        '                If cntRow = 1 Then GoTo NextRow
        '
        '                .Col = ColVDate
        '                If mVDate <> Trim(.Text) Then
        '                    xVDate = Trim(.Text)
        '                Else
        '                    xVDate = ""
        '                End If
        '                mVDate = Trim(.Text)
        '
        '                .Row = cntRow - 1
        '                .Col = ColBalDC
        '                mDC = Trim(.Text)
        '
        '                .Col = ColBalance
        '                mOPBalance = Val(.Text)
        '
        '                Call PrintDetail(cntRow, mLineCount, xVDate, Format(mOPBalance, "0.00") & " " & mDC)
        '                mLineCount = mLineCount + 1
        '
        '                If mLineCount >= 63 And mPrintFooter = False Then
        '                    Call PrintFooter(pPageNo, mLineCount, mPrintFooter)
        '                ElseIf cntRow = SprdLedg.MaxRows - 1 Then
        '                    Do While mLineCount <= 63
        '                        Print #1, " "
        '                        mLineCount = mLineCount + 1
        '                    Loop
        '
        '                    Print #1, Tab(0); Chr(20) & String(mPageWidth, "-") & Chr(15)
        '                    mLineCount = mLineCount + 1
        '
        '                    .Row = SprdLedg.MaxRows
        '                    .Col = ColDAmount
        '                    Print #1, Tab(TabDAmount); String(TabCAmount - TabDAmount - Len(Trim(.Text)), " ") & Trim(.Text);
        '
        '                    .Col = ColCAmount
        '                    Print #1, Tab(TabCAmount); String(TabBalance - TabCAmount - Len(Trim(.Text)), " ") & Trim(.Text)
        '
        '                    Call PrintFooter(pPageNo, mLineCount, mPrintFooter)
        '                End If
        'NextRow:
        '            Next
        '             Close #1
        '        End If
        '    End With
        '
        '    If pPrintMode = "P" Then
        '        Dim mFP As Boolean
        '        mFP = Shell(App.path & "\PrintReport.bat",vbNormalFocus)
        '        If mFP = False Then GoTo ErrPart
        '        Shell App.path & "\PrintReport.bat",vbNormalFocus
        '    Else
        '        Shell "ATTRIB +R -A " & pFileName
        '        Shell "NOTEPAD.EXE " & pFileName, vbMaximizedFocus
        '        'App.Path & "\RVIEW.EXE "
        '    End If
        '
        '    BookReport = True
        'Exit Function
        'ErrPart:
        '    MsgBox err.Description
        '    BookReport = False
        '    ''Resume
        '    Close #1
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
        PrintLine(1, TAB(0), "BANK BOOK ")
        mLineCount = mLineCount + 1
        mTitle = "Bank Name : " & UCase(TxtAccount.Text)
        PrintLine(1, TAB(0), Chr(27) & Chr(69) & mTitle & Chr(27) & Chr(70))
        mLineCount = mLineCount + 1
        PrintLine(1, TAB(0), Chr(27) & Chr(69) & "For the period : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & "-" & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & Chr(27) & Chr(70))
        mLineCount = mLineCount + 1
        PrintLine(1, TAB(0), New String("-", mPageWidth))
        mLineCount = mLineCount + 1
        Print(1, TAB(TabRefNo), "No.")
        Print(1, TAB(TabName), "On Account Of..")
        Print(1, TAB(TabDesc), "Description")
        Print(1, TAB(TabCheque), "Cheque No")
        Print(1, TAB(TabDAmount), New String(" ", TabCAmount - TabDAmount - Len("Deposit (Rs)")) & "Deposit (Rs)")
        Print(1, TAB(TabCAmount), New String(" ", TabBalance - TabCAmount - Len("Payment (Rs)")) & "Payment (Rs)")
        PrintLine(1, TAB(TabBalance), New String(" ", mPageWidth - TabBalance - Len("Balance (Rs)")) & "Balance (Rs)")
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
    Private Function PrintDetail(ByRef mRow As Double, ByRef mLineCount As Integer, ByRef pDate As String, ByRef pBalance As String) As Boolean
        'On Error GoTo ErrPart
        'Dim mAcctName As String
        'Dim mNarration As String
        'Dim mBillDetail As String
        'Dim mChequeNo As String
        'Dim mRemarks As String
        'Dim mBalDC As String
        'Dim mBalance As String
        'Dim mDAmount As String
        'Dim mCAmount As String
        '
        '    With SprdLedg
        '        .Row = mRow
        '
        '        If pDate <> "" Then
        '            Print #1, Tab(TabRefNo); Format(pDate, "DDDD, dd/mm/yyyy");
        '            Print #1, Tab(TabDAmount); "Opening Balance : ";
        '            Print #1, Tab(TabBalance); String(mPageWidth - TabBalance - Len(Trim(pBalance)), " ") & Trim(pBalance)
        '            mLineCount = mLineCount + 1
        '        End If
        '
        '        .Col = ColVNo
        '        Print #1, Tab(TabRefNo); Trim(.Text);
        '
        '        .Col = ColAccount
        '        mRemarks = GetMultiLine(Trim(.Text), mLineCount, TabDesc - TabName - 1, TabName)
        '        Print #1, Tab(TabName); Trim(mRemarks);
        '
        '        .Col = ColNarration
        '        mRemarks = GetMultiLine(Trim(.Text), mLineCount, TabCheque - TabDesc - 1, TabDesc)
        '        Print #1, Tab(TabDesc); Trim(mRemarks);
        '
        '        .Col = ColChequeNo
        '        mRemarks = GetMultiLine(Trim(.Text), mLineCount, TabDAmount - TabCheque - 1, TabCheque)
        '        Print #1, Tab(TabCheque); Trim(mRemarks);
        '
        '        .Col = ColDAmount
        '        mDAmount = IIf(Val(.Text) = 0, "", Trim(.Text))
        '        Print #1, Tab(TabDAmount); String(TabCAmount - TabDAmount - Len(mDAmount), " ") & mDAmount;
        '
        '        .Col = ColCAmount
        '        mDAmount = IIf(Val(.Text) = 0, "", Trim(.Text))
        '        Print #1, Tab(TabCAmount); String(TabBalance - TabCAmount - Len(mDAmount), " ") & mDAmount;
        '
        '        .Col = ColBalDC
        '        mBalDC = Trim(.Text)
        '
        '        .Col = ColBalance
        '        mBalance = Trim(.Text) & " " & mBalDC
        '        Print #1, Tab(TabBalance); String(mPageWidth - TabBalance - Len(Trim(mBalance)), " ") & Trim(mBalance)
        '        mLineCount = mLineCount + 1
        '
        '    End With
        '    PrintDetail = True
        'Exit Function
        'ErrPart:
        '    MsgBox err.Description
        '    PrintDetail = False
        ''Resume
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdLedg, RowHeight)
        If Show1 = False Then GoTo ErrPart
        SprdLedg.Focus()
        Call PrintStatus(True)
        MainClass.SetFocusToCell(SprdLedg, mActiveRow, 4)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub DisplayTotals()
        On Error GoTo ERR1
        Dim cntCol As Integer
        Dim mAmount As Double
        With SprdLedg
            Call MainClass.AddBlankfpSprdRow(SprdLedg, ColVDate)
            .Col = ColNarration
            .Row = .MaxRows
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)
            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '' &H8000000B             ''&H80FF80
            .BlockMode = False
            Call CalcRowTotal(SprdLedg, ColAmount, 1, ColAmount, .MaxRows - 1, .MaxRows, ColAmount)
            Call CalcRowTotal(SprdLedg, ColTaxableAmount, 1, ColTaxableAmount, .MaxRows - 1, .MaxRows, ColTaxableAmount)
            Call CalcRowTotal(SprdLedg, ColCGSTAmount, 1, ColCGSTAmount, .MaxRows - 1, .MaxRows, ColCGSTAmount)
            Call CalcRowTotal(SprdLedg, ColSGSTAmount, 1, ColSGSTAmount, .MaxRows - 1, .MaxRows, ColSGSTAmount)
            Call CalcRowTotal(SprdLedg, ColIGSTAmount, 1, ColIGSTAmount, .MaxRows - 1, .MaxRows, ColIGSTAmount)
            '        .Row = .MaxRows
            '
            '        .Col = ColAmount
            '        mAmount = Val(.Text)
            '
            '        .Col = ColCAmount
            '        mCredit = Val(.Text)
            '
            '        mBalance = mDebit - mCredit
            '        .Col = ColBalance
            '        .Text = Str(Abs(mBalance))
            '        .FontBold = True
            '
            '        .Col = ColBalDC
            '        .Text = IIf(mBalance >= 0, "DR", "CR")
            '        .FontBold = True
            '        FormatSprdLedg .MaxRows
        End With
        '    Call FillRunBalCol
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Public Sub frmDnCnViewBook_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
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
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmDnCnViewBook_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        cboType.Items.Clear()
        cboType.Items.Add("ALL")
        cboType.Items.Add("PO RATE DIFF.")
        cboType.Items.Add("SHORTAGE")
        cboType.Items.Add("REJECTION")
        cboType.Items.Add("DISCOUNT")
        cboType.Items.Add("OTHERS")
        cboType.Items.Add("AMEND. RATE DIFF")
        cboType.Items.Add("VOLUME DISCOUNT")
        cboType.SelectedIndex = 0
        OptSumDet(0).Checked = True
        Call frmDnCnViewBook_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmDnCnViewBook_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
    Private Sub frmDnCnViewBook_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub OptAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAll.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptAll.GetIndex(eventSender)
            Call PrintStatus(False)
            TxtAccount.Enabled = IIf(Index = 0, False, True)
            cmdsearch.Enabled = IIf(Index = 0, False, True)
        End If
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
        Dim xVTYPE As String = ""
        If OptSumDet(0).Checked = True Then
            SprdLedg.Row = SprdLedg.ActiveRow
            SprdLedg.Col = ColVDate
            xVDate = Me.SprdLedg.Text
            SprdLedg.Col = ColMKEY
            xMKey = Me.SprdLedg.Text
            SprdLedg.Col = ColVNo
            xVNo = Me.SprdLedg.Text
            SprdLedg.Col = ColBookType
            xBookType = Me.SprdLedg.Text
            SprdLedg.Col = ColBookSubType
            xBookSubType = Me.SprdLedg.Text
            '        If CDate(xVDate) < CDate(PubGSTApplicableDate) Then
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
            '        Else
            '            If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Or xBookType = "R" Or xBookType = "E" Then
            '                xVTYPE = Mid(xVNo, 1, Len(xVNo) - 7)
            '                xVNo = Right(xVNo, 5)
            '            End If
            '        End If
            Call ShowTrn(xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, Me)
        Else
            If OptSumDet(2).Checked = True Then
                pIndex = 2
            Else
                pIndex = 1
            End If
            Call ViewAccountLedger(pIndex)
        End If
    End Sub
    Private Sub ViewAccountLedger(ByRef xIndex As Integer)
        Dim ss As New frmDnCnViewBook
        Dim mFromDate As String
        Dim mToDate As String
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    If lblBookType.text = ConLedger Then
        ss.MdiParent = Me.MdiParent
        ss.lblBookType.Text = lblBookType.Text
        ss.TxtAccount.Text = TxtAccount.Text
        ss.lblAcCode.Text = lblAcCode.Text
        SprdLedg.Row = SprdLedg.ActiveRow
        SprdLedg.Col = ColVDate
        If GetMonthStartEndDate((SprdLedg.Text), mFromDate, mToDate) = True Then
            ss.txtDateFrom.Text = VB6.Format(mFromDate, "dd/mm/yyyy")
            ss.txtDateTo.Text = VB6.Format(mToDate, "dd/mm/yyyy")
        Else
            ss.txtDateFrom.Text = VB6.Format(txtDateFrom.Text, "dd/mm/yyyy")
            ss.txtDateTo.Text = VB6.Format(txtDateTo.Text, "dd/mm/yyyy")
        End If
        ss.OptSumDet(xIndex - 1).Checked = True
        ''ss.cboConsolidated.Text = cboConsolidated.Text
        '        ss.cboConsolidated.ListIndex = 3     ''DIVISION...
        ss.Show()
        ss.frmDnCnViewBook_Activated(Nothing, New System.EventArgs())
        ss.cmdShow_Click(Nothing, New System.EventArgs())
        '    End If
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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And SUPP_CUST_TYPE IN ('S','C')"

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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And SUPP_CUST_TYPE IN ('S','C')"
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
            .set_ColWidth(ColVNo, 7)
            If OptSumDet(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 12)
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
            .Col = ColAccount2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAccount2, 20)
            If OptSumDet(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColAmount, 11)
            .Col = ColTaxableAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColTaxableAmount, 11)
            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColCGSTAmount, 11)
            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColSGSTAmount, 11)
            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColIGSTAmount, 11)
            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True
            MainClass.SetSpreadColor(SprdLedg, -1)
            MainClass.ProtectCell(SprdLedg, 1, .MaxRows, 1, .MaxCols)
            SprdLedg.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
            SprdLedg.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdLedg.DAutoCellTypes = True
            SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdLedg.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean
        On Error GoTo LedgError
        Dim SqlStr As String
        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdLedg, StrConn, "Y")
        FormatSprdLedg(-1)
        DisplayTotals()
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If OptSumDet(0).Checked = True Then
            SqlStr = " SELECT '' AS LOCKED, DNCN.BOOKTYPE, DNCN.BOOKSUBTYPE , " & vbCrLf & " TO_CHAR(DNCN.VDATE,'DD/MM/YYYY'),DNCN.VNO AS V_NO, " & vbCrLf & " DNCN.BILLNO || ' Dt. ' || TO_CHAR(DNCN.INVOICE_DATE,'DD/MM/YYYY') As BILLNO, " & vbCrLf & " ACM.SUPP_CUST_NAME, DNCN.NARRATION, ACM2.SUPP_CUST_NAME,DNCN.NETVALUE,DNCN.ITEMVALUE, DNCN.NETCGST_AMOUNT, DNCN.NETSGST_AMOUNT, DNCN.NETIGST_AMOUNT, DNCN.MKEY AS MKEY"
        ElseIf OptSumDet(1).Checked = True Then
            SqlStr = " SELECT '', '','',TO_CHAR(DNCN.VDATE,'DD/MM/YYYY'), " & vbCrLf & " '','', '','','',TO_CHAR(SUM(DNCN.NETVALUE)) AS NETV1,TO_CHAR(SUM(DNCN.ITEMVALUE)) AS TAxV1,TO_CHAR(SUM(DNCN.NETCGST_AMOUNT)) AS CGST, TO_CHAR(SUM(DNCN.NETSGST_AMOUNT)) AS SGST, TO_CHAR(SUM(DNCN.NETIGST_AMOUNT)) AS IGST, ''  AS MKEY"
        ElseIf OptSumDet(2).Checked = True Then
            SqlStr = " SELECT '','','', TO_CHAR(DNCN.VDATE,'MON-YYYY'), " & vbCrLf & " '','', '','','',TO_CHAR(SUM(DNCN.NETVALUE)) AS NETV1,TO_CHAR(SUM(DNCN.ITEMVALUE)) AS TAxV1,TO_CHAR(SUM(DNCN.NETCGST_AMOUNT)) AS CGST, TO_CHAR(SUM(DNCN.NETSGST_AMOUNT)) AS SGST, TO_CHAR(SUM(DNCN.NETIGST_AMOUNT)) AS IGST, ''  AS MKEY"
        End If
        ''*******FROM CLAUSE....
        SqlStr = SqlStr & vbCrLf & " FROM FIN_DNCN_HDR DNCN, FIN_SUPP_CUST_MST ACM, FIN_SUPP_CUST_MST ACM2"
        ''*******WHERE CLAUSE....
        SqlStr = SqlStr & vbCrLf & " WHERE DNCN.COMPANY_CODE=ACM.COMPANY_CODE" & vbCrLf & " AND DNCN.DEBITACCOUNTCODE=ACM.SUPP_CUST_CODE" & vbCrLf & " AND DNCN.COMPANY_CODE=ACM2.COMPANY_CODE" & vbCrLf & " AND DNCN.CREDITACCOUNTCODE=ACM2.SUPP_CUST_CODE" & vbCrLf & " AND DNCN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DNCN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        If OptAll(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If
        SqlStr = SqlStr & vbCrLf & " AND DNCN.Vdate>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND DNCN.Vdate<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        If lblBookType.Text = ConDebitNoteBook Then
            SqlStr = SqlStr & vbCrLf & " AND DNCN.BOOKCODE='" & ConDebitNoteBookCode & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND DNCN.BOOKCODE='" & ConCreditNoteBookCode & "'"
        End If
        SqlStr = SqlStr & vbCrLf & " AND APPROVED='Y' AND CANCELLED='N'"
        If Trim(txtVType.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND VTYPE='" & MainClass.AllowSingleQuote(txtVType.Text) & "'"
        End If
        If cboType.Text <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND DNCN.DNCNTYPE='" & UCase(VB.Left(cboType.Text, 1)) & "'"
        End If
        If OptSumDet(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY VDATE,VNO"
        ElseIf OptSumDet(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY DNCN.VDATE"
            SqlStr = SqlStr & vbCrLf & " ORDER BY VDATE"
        ElseIf OptSumDet(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY TO_CHAR(DNCN.VDATE,'MON-YYYY'),TO_CHAR(DNCN.Vdate,'YYYYMM')"
            SqlStr = SqlStr & vbCrLf & " ORDER BY TO_CHAR(Vdate,'YYYYMM')"
        End If
        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
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
        If Trim(cboType.Text) = "" Then
            MsgInformation("Debit Note/Credit Note Type is Blank.")
            TxtAccount.Focus()
            FieldsVerification = False
            Exit Function
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
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDateTo1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call PrintStatus(False)
    End Sub
    Private Sub txtVType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVType.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtVType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
