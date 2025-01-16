Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewDayBook
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColBookType As Short = 2
    Private Const ColBookSubType As Short = 3
    Private Const ColVDate As Short = 4
    Private Const ColChequeNo As Short = 5
    Private Const ColChequeDate As Short = 6
    Private Const ColVNo As Short = 7
    Private Const colAccount As Short = 8
    Private Const ColAmount As Short = 9
    Private Const ColUnit As Short = 10
    Private Const ColBankName As Short = 11
    Private Const ColAccountNo As Short = 12
    Private Const ColIFSCCode As Short = 13
    Private Const ColMKEY As Short = 14
    'Private Const mPageWidth = 132
    'Private Const TabRefNo = 0
    'Private Const TabName = 10
    'Private Const TabDesc = 45
    'Private Const TabCheque = 77
    'Private Const TabDAmount = 87
    'Private Const TabCAmount = 102
    'Private Const TabBalance = 117
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim mClickProcess As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
        cmdExport.Enabled = pPrintEnable
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
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub cmdExport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExport.Click
        On Error GoTo ErrPart
        Dim cntRow As Double
        Dim mVDate As String
        Dim mVNo As String
        Dim mChequeNo As String
        Dim mChequeDate As String
        Dim mAccount As String
        Dim mAmount As String
        Dim mBankName As String
        Dim mAccountNo As String
        Dim mIFSCCode As String
        Dim mHeadingline As Integer
        Dim mSno As Integer
        Dim mNetAmount As Double
        Dim exlobj As Object
        Dim mColHeader As String
        mHeadingline = 1
        mNetAmount = 0
        mSno = 0
        exlobj = CreateObject("excel.application")
        exlobj.Visible = True
        exlobj.Workbooks.Add()
        With exlobj.ActiveSheet
            '        .Cells(mHeadingline, 6).Font.Name = "Verdana"
            .Cells(mHeadingline, 1).Value = "S. No"
            .Cells(mHeadingline, 1).Font.bold = True
            .Cells(mHeadingline, 2).Value = "VDate"
            .Cells(mHeadingline, 2).Font.bold = True
            .Cells(mHeadingline, 3).Value = "VNo"
            .Cells(mHeadingline, 3).Font.bold = True
            .Cells(mHeadingline, 4).Value = "Cheque No"
            .Cells(mHeadingline, 4).Font.bold = True
            .Cells(mHeadingline, 5).Value = "Cheque Date"
            .Cells(mHeadingline, 5).Font.bold = True
            .Cells(mHeadingline, 6).Value = "Account Name"
            .Cells(mHeadingline, 6).Font.bold = True
            .Cells(mHeadingline, 7).Value = "Amount (in Rs.)"
            .Cells(mHeadingline, 7).Font.bold = True
            .Cells(mHeadingline, 8).Value = "Bank Name"
            .Cells(mHeadingline, 8).Font.bold = True
            .Cells(mHeadingline, 9).Value = "Account No"
            .Cells(mHeadingline, 9).Font.bold = True
            .Cells(mHeadingline, 10).Value = "IFSC Code"
            .Cells(mHeadingline, 10).Font.bold = True
            .Cells(mHeadingline, 11).Value = "Our Unit Name"
            .Cells(mHeadingline, 11).Font.bold = True
            mHeadingline = mHeadingline + 1
            For cntRow = 1 To SprdPayment.MaxRows
                SprdPayment.Row = cntRow
                SprdPayment.Col = ColVDate
                mVDate = Trim(SprdPayment.Text)
                SprdPayment.Col = ColVDate
                mVDate = Trim(SprdPayment.Text)
                SprdPayment.Col = ColVDate
                mVDate = Trim(SprdPayment.Text)
                SprdPayment.Col = ColVNo
                mVNo = Trim(SprdPayment.Text)
                SprdPayment.Col = ColChequeNo
                mChequeNo = Trim(SprdPayment.Text)
                SprdPayment.Col = ColChequeDate
                mChequeDate = Trim(SprdPayment.Text)
                SprdPayment.Col = colAccount
                mAccount = Trim(SprdPayment.Text)
                SprdPayment.Col = ColAmount
                mAmount = Trim(SprdPayment.Text)
                SprdPayment.Col = ColBankName
                mBankName = Trim(SprdPayment.Text)
                SprdPayment.Col = ColAccountNo
                mAccountNo = Trim(SprdPayment.Text)
                mAccountNo = Replace(mAccountNo, "'", "")
                '            If InStr(1, mAccountNo, "'") > 0 Then
                '                mAccountNo = Mid(mAccountNo, 2)
                '            Else
                '                mAccountNo = mAccountNo
                '            End If
                SprdPayment.Col = ColIFSCCode
                mIFSCCode = Trim(SprdPayment.Text)
                mSno = mSno + 1
                .Cells(mHeadingline, 1).Value = mSno
                .Cells(mHeadingline, 2).Value = mVDate
                .Cells(mHeadingline, 3).Value = mVNo
                .Cells(mHeadingline, 4).Value = mChequeNo
                .Cells(mHeadingline, 5).Value = mChequeDate
                .Cells(mHeadingline, 6).Value = mAccount
                .Cells(mHeadingline, 7).Value = VB6.Format(mAmount, "0.00")
                .Cells(mHeadingline, 8).Value = mBankName
                .Cells(mHeadingline, 9).Value = "'" & mAccountNo
                .Cells(mHeadingline, 10).Value = mIFSCCode
                .Cells(mHeadingline, 11).Value = IIf(IsDbNull(RsCompany.Fields("COMPANY_SHORTNAME").Value), "", RsCompany.Fields("COMPANY_SHORTNAME").Value)
                mHeadingline = mHeadingline + 1
            Next
            '        With exlobj.ActiveSheet
            mColHeader = "A1" & ":" & "K" & IIf(mHeadingline = 4, 4, mHeadingline - 1)
            .Cells.Range("" & mColHeader & "").Borders(1).LineStyle = 1
            .Cells.Range("" & mColHeader & "").Borders(3).LineStyle = 1
            mColHeader = "A1" & ":" & "K" & IIf(mHeadingline = 4, 4, mHeadingline - 1)
            .Cells.Range("" & mColHeader & "").BorderAround(LineStyle:=1, Weight:=3, ColorIndex:=1)
        End With
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        ''Resume
        '    Close #1
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
        Call ReportForBook(Crystal.DestinationConstants.crptToWindow)
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
        SqlStr = "DELETE FROM TEMP_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = ""
        If InsertBook = False Then GoTo ERR1
        'Select Record for print...
        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        If OptSumDet(0).Checked = True Then
            If lblBookType.Text = ConBankBook Then
                mRPTName = "DayBook.rpt"
            Else
                mRPTName = "DayBookCash.rpt"
            End If
        Else
            mRPTName = "BookSummary.Rpt"
        End If
        mTitle = TxtAccount.Text
        If OptSumDet(1).Checked = True Then
            mTitle = mTitle & " - Daily"
        ElseIf OptSumDet(2).Checked = True Then
            mTitle = mTitle & " - Monthly"
        End If
        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")
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
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        mSqlStr = "SELECT * " & " FROM TEMP_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"
        FetchRecordForReport = mSqlStr
    End Function
    Private Function InsertBook() As Boolean
        Dim mVDateR As String
        Dim mVNoR As String
        Dim mChequeNoR As String
        Dim mAcctNameR As String
        Dim mAmountR As Double
        Dim mVDateP As String
        Dim mVNoP As String
        Dim mChequeNoP As String
        Dim mAcctNameP As String
        Dim mAmountP As Double
        Dim SqlStr As String
        Dim cntMaxRow As Integer
        Dim cntRow As Integer
        Dim mChequeDateR As String
        Dim mChequeDateP As String
        On Error GoTo ERR1
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        cntMaxRow = IIf(SprdReceipt.MaxRows > SprdPayment.MaxRows, SprdReceipt.MaxRows, SprdPayment.MaxRows) - 1
        cntRow = 1
        SqlStr = ""
        For cntRow = 1 To cntMaxRow
            mVDateR = ""
            mVNoR = ""
            mChequeNoR = ""
            mAcctNameR = ""
            mAmountR = 0
            mVDateP = ""
            mVNoP = ""
            mChequeNoP = ""
            mAcctNameP = ""
            mAmountP = 0
            mChequeDateR = ""
            mChequeDateP = ""
            With SprdReceipt
                .Row = cntRow
                If cntRow < .MaxRows Then
                    .Col = ColVNo
                    mVNoR = Trim(.Text)
                    .Col = ColVDate
                    If Trim(.Text) = "" Then
                        mVDateR = ""
                    Else
                        mVDateR = VB6.Format(.Text, "dd/mm/yyyy")
                    End If
                    .Col = colAccount
                    mAcctNameR = Trim(.Text)
                    .Col = ColChequeNo
                    mChequeNoR = Trim(.Text)
                    .Col = ColChequeDate
                    mChequeDateR = Trim(.Text)
                    .Col = ColAmount
                    If IsNumeric(.Text) Then
                        mAmountR = MainClass.FormatRupees(.Text)
                    Else
                        mAmountR = 0
                    End If
                End If
            End With
            With SprdPayment
                .Row = cntRow
                If cntRow < .MaxRows Then
                    .Col = ColVNo
                    mVNoP = Trim(.Text)
                    .Col = ColVDate
                    If Trim(.Text) = "" Then
                        mVDateP = ""
                    Else
                        mVDateP = VB6.Format(.Text, "dd/mm/yyyy")
                    End If
                    .Col = colAccount
                    mAcctNameP = Trim(.Text)
                    .Col = ColChequeNo
                    mChequeNoP = Trim(.Text)
                    .Col = ColChequeDate
                    mChequeDateP = Trim(.Text)
                    .Col = ColAmount
                    If IsNumeric(.Text) Then
                        mAmountP = MainClass.FormatRupees(.Text)
                    Else
                        mAmountP = 0
                    End If
                End If
            End With
            SqlStr = "Insert into TEMP_PrintDummyData NOLOGGING (UserID,SubRow, " & vbCrLf & " Field1, Field2, Field3, Field4, " & vbCrLf & " Field11, Field12, Field13, FIELD14,Field15, FIELD16,Field17, FIELD18) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & Trim(mVDateR) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mChequeNoR)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mAcctNameR)) & "', " & vbCrLf & " '" & Trim(CStr(mAmountR)) & "', " & vbCrLf & " '" & Trim(mVDateP) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mChequeNoP)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mAcctNameP)) & "', " & vbCrLf & " '" & Trim(CStr(mAmountP)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mChequeDateR)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mChequeDateP)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mVNoR)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mVNoP)) & "' " & vbCrLf & " )"
            PubDBCn.Execute(SqlStr)
        Next
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
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        MainClass.AssignCRptFormulas(Report1, "mOpening=""" & lblOpening.Text & """")
        MainClass.AssignCRptFormulas(Report1, "mClosing=""" & lblClosing.Text & """")
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
        Call ReportForBook(Crystal.DestinationConstants.crptToPrinter)
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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdReceipt, RowHeight)
        MainClass.ClearGrid(SprdPayment, RowHeight)

        Call FormatSprd(SprdReceipt, -1)
        Call FormatSprd(SprdPayment, -1)

        If BookInfo = False Then GoTo ErrPart
        SprdReceipt.Focus()
        Call PrintStatus(True)
        MainClass.SetFocusToCell(SprdReceipt, mActiveRow, colAccount)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub DisplayTotals(ByRef fpGrid As AxFPSpreadADO.AxfpSpread)
        On Error GoTo ERR1
        Dim cntCol As Integer
        Dim mDebit As Double
        Dim mCredit As Double
        Dim mBalance As Double
        Dim mDC As String
        With fpGrid
            Call MainClass.AddBlankfpSprdRow(fpGrid, ColVDate)
            .Col = colAccount
            .Row = .MaxRows
            .Text = "TOTAL :"
            .FontBold = True
            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = Color.AliceBlue     '' &H8000000F
            .BlockMode = False
            Call FillRunBalCol(fpGrid)
            Call FormatSprd(fpGrid, .MaxRows)
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Public Sub frmViewDayBook_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblBookType.Text = ConBankBook Then
            Me.Text = "Bank Day Book"
        Else
            Me.Text = "Cash Day Book"
        End If
        TxtAccount.Visible = True
        FraAccount.Text = "Book"
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
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewDayBook_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY") 'Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        OptSumDet(0).Checked = True
        Call frmViewDayBook_Activated(eventSender, eventArgs)
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

    Private Sub frmViewDayBook_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer
        '    mReFormWidth = Me.Width
        '
        '    SprdReceipt.Width = IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth)
        '    Frame4.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
        '    CurrFormWidth = mReFormWidth
        '
        '    MainClass.SetSpreadColor SprdReceipt, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewDayBook_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub SprdPayment_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdPayment.DblClick
        Dim SqlStr As String
        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String
        Dim pIndex As Integer
        Dim xVTYPE As String
        If OptSumDet(0).Checked = True Then
            SprdPayment.Row = SprdPayment.ActiveRow
            SprdPayment.Col = ColVDate
            xVDate = Me.SprdPayment.Text
            SprdPayment.Col = ColMKEY
            xMKey = Me.SprdPayment.Text
            SprdPayment.Col = ColVNo
            xVNo = Me.SprdPayment.Text
            SprdPayment.Col = ColBookType
            xBookType = Me.SprdPayment.Text
            SprdPayment.Col = ColBookSubType
            xBookSubType = Me.SprdPayment.Text

            SprdPayment.Col = ColUnit
            If RsCompany.Fields("COMPANY_SHORTNAME").Value <> Me.SprdPayment.Text Then
                MsgInformation("Cann't Open Other Unit Voucher.")
                Exit Sub
            End If

            If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Or xBookType = "R" Or xBookType = "E" Then
                '            xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
                '            xVNo = Right(xVNo, 5)
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & xMKey & "'" & vbCrLf & " AND BOOKTYPE='" & xBookType & "'" & vbCrLf & " AND BOOKSUBTYPE='" & xBookSubType & "'" & vbCrLf & " AND VDATE='" & VB6.Format(xVDate, "DD-MMM-YYYY") & "'"
                If MainClass.ValidateWithMasterTable(xVNo, "VNO", "VTYPE", "FIN_POSTED_TRN", PubDBCn, MasterNo, , SqlStr) = True Then
                    xVTYPE = MasterNo
                    xVNo = Mid(xVNo, Len(xVTYPE) + 1)
                Else
                    Exit Sub
                End If
            End If
            Call ShowTrn(xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, Me)
        Else
            If OptSumDet(2).Checked = True Then
                pIndex = 2
            Else
                pIndex = 1
                SprdPayment.Row = SprdPayment.ActiveRow
                SprdPayment.Col = ColVDate
                xVDate = Me.SprdPayment.Text
            End If
            Call ViewAccountLedger(pIndex, xVDate, xVDate)
        End If
    End Sub
    Private Sub SprdReceipt_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdReceipt.DataColConfig
        SprdReceipt.Row = -1
        SprdReceipt.Col = eventArgs.col
        SprdReceipt.DAutoCellTypes = True
        SprdReceipt.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdReceipt.TypeEditLen = 1000
    End Sub
    Private Sub SprdReceipt_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdReceipt.DblClick
        Dim SqlStr As String
        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String
        Dim pIndex As Integer
        Dim xVTYPE As String
        If OptSumDet(0).Checked = True Then
            SprdReceipt.Row = SprdReceipt.ActiveRow
            SprdReceipt.Col = ColVDate
            xVDate = Me.SprdReceipt.Text
            SprdReceipt.Col = ColMKEY
            xMKey = Me.SprdReceipt.Text
            SprdReceipt.Col = ColVNo
            xVNo = Me.SprdReceipt.Text
            SprdReceipt.Col = ColBookType
            xBookType = Me.SprdReceipt.Text
            SprdReceipt.Col = ColBookSubType
            xBookSubType = Me.SprdReceipt.Text

            SprdReceipt.Col = ColUnit
            If RsCompany.Fields("COMPANY_SHORTNAME").Value <> Me.SprdReceipt.Text Then
                MsgInformation("Cann't Open Other Unit Voucher.")
                Exit Sub
            End If

            If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Or xBookType = "R" Or xBookType = "E" Then
                '            xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
                '            xVNo = Right(xVNo, 5)
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & xMKey & "'" & vbCrLf & " AND BOOKTYPE='" & xBookType & "'" & vbCrLf & " AND BOOKSUBTYPE='" & xBookSubType & "'" & vbCrLf & " AND VDATE='" & VB6.Format(xVDate, "DD-MMM-YYYY") & "'"
                If MainClass.ValidateWithMasterTable(xVNo, "VNO", "VTYPE", "FIN_POSTED_TRN", PubDBCn, MasterNo, , SqlStr) = True Then
                    xVTYPE = MasterNo
                    xVNo = Mid(xVNo, Len(xVTYPE) + 1)
                Else
                    Exit Sub
                End If
            End If
            Call ShowTrn(xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, Me)
        Else
            If OptSumDet(2).Checked = True Then
                pIndex = 2
            Else
                pIndex = 1
                SprdReceipt.Row = SprdReceipt.ActiveRow
                SprdReceipt.Col = ColVDate
                xVDate = Me.SprdReceipt.Text
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
        SprdReceipt.Row = SprdReceipt.ActiveRow
        SprdReceipt.Col = ColVDate
        If GetMonthStartEndDate((SprdReceipt.Text), mFromDate, mToDate) = True Then
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
    Private Sub SprdReceipt_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdReceipt.KeyDownEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then
            SprdReceipt_DblClick(SprdReceipt, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And STATUS='O' "
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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND "
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
    Private Sub FormatSprd(ByRef pGrid As AxFPSpreadADO.AxfpSpread, ByRef Arow As Integer) ''AxFPSpreadADO.AxfpSpread  ''FPSpreadADO.fpSpread
        With pGrid
            .MaxCols = ColMKEY
            .set_RowHeight(Arow, RowHeight * 1.25)
            .set_ColWidth(0, 0)
            .set_RowHeight(-1, RowHeight)

            .Row = -1
            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)     '.ColWidth(ColLocked) = 15
            .ColHidden = True

            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookType, 15)     '.ColWidth(ColBookType) = 15
            .ColHidden = True
            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookSubType, 2)     '.ColWidth(ColBookSubType) = 2
            .ColHidden = True
            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDate, 8.5)     ' .ColWidth(ColVDate) = 7.5
            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)     ' .ColWidth(ColVNo) = 7
            .ColHidden = IIf(lblBookType.Text = ConBankBook, True, False)
            .Col = colAccount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colAccount, IIf(lblBookType.Text = ConBankBook, 14.5, 21))     ' .ColWidth(colAccount) = IIf(lblBookType.Text = ConBankBook, 14.5, 21)
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
            .set_ColWidth(ColChequeNo, 6)     '.ColWidth(ColChequeNo) = 6
            If OptSumDet(0).Checked = True Then
                .ColHidden = IIf(lblBookType.Text = ConBankBook, False, True)
            Else
                .ColHidden = True
            End If
            .Col = ColChequeDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChequeDate, 8.5)     '.ColWidth(ColChequeDate) = 7.5
            If OptSumDet(0).Checked = True Then
                .ColHidden = IIf(lblBookType.Text = ConBankBook, False, True)
            Else
                .ColHidden = True
            End If
            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = "0"
            .TypeFloatMax = "9999999999"
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColAmount, 12)     ' .ColWidth(ColAmount) = 7.5

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 10)     ' .ColWidth(ColBankName) = 

            .Col = ColBankName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBankName, 15)     ' .ColWidth(ColBankName) = 15
            .ColHidden = IIf(chkBankDetail.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

            .Col = ColAccountNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAccountNo, 15)     '  .ColWidth(ColAccountNo) = 15
            .ColHidden = IIf(chkBankDetail.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            .Col = ColIFSCCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColIFSCCode, 10)     '.ColWidth(ColIFSCCode) = 10
            .ColHidden = IIf(chkBankDetail.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)     ' .ColWidth(ColMKEY) = 8
            .ColHidden = True

            Call FillHeading(pGrid)
            MainClass.SetSpreadColor(pGrid, -1)
            MainClass.ProtectCell(pGrid, 1, .MaxRows, 1, .MaxCols)
            pGrid.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            pGrid.DAutoCellTypes = True
            pGrid.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            pGrid.GridColor = Color.Blue '' &HC00000
        End With
    End Sub
    Private Function BookInfo() As Boolean
        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset
        Dim mOpening As Double
        Dim mDrAmount As Double
        Dim mCrAmount As Double
        Dim SqlStrReceipt As String
        Dim SqlStrPayment As String
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim mBalance As Double
        BookInfo = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If InsertIntoTemp() = False Then GoTo LedgError

        SqlStr1 = MakeSQL
        SqlStrReceipt = MakeSQLCond(False, "CB", VB.Left(ConBankReceipt, 1), "C", VB.Right(ConBankReceipt, 1))
        SqlStrPayment = MakeSQLCond(False, "CB", VB.Left(ConBankPayment, 1), "D", VB.Right(ConBankPayment, 1))
        SqlStr2 = " AND ACCOUNTCODE<>'" & MainClass.AllowSingleQuote(mAccountCode) & "' "

        If OptSumDet(0).Checked = True Then
            SqlStr2 = SqlStr2 & vbCrLf _
                & " GROUP BY TRN.VDATE," & vbCrLf _
                & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE , TRN.VNO,  " & vbCrLf _
                & " ACM.SUPP_CUST_NAME,TRN.MKEY,CHEQUENO,CHQDATE,COMP.COMPANY_SHORTNAME,ACM.CUST_BANK_BANK, ACM.CUST_BANK_ACCT_NO, ACM.BANK_IFSC_CODE  " & vbCrLf _
                & " ORDER BY TRN.VDATE, TRN.VNO"
        ElseIf OptSumDet(1).Checked = True Then
            SqlStr2 = SqlStr2 & vbCrLf & " GROUP BY TRN.VDATE " & vbCrLf & " ORDER BY TRN.VDATE "
        ElseIf OptSumDet(2).Checked = True Then
            SqlStr2 = SqlStr2 & vbCrLf & " GROUP BY TO_CHAR(Vdate,'MON-YYYY'),TO_CHAR(Vdate,'YYYYMM')" & vbCrLf & " ORDER BY TO_CHAR(Vdate,'YYYYMM')"
        End If
        SqlStr = SqlStr1 & vbCrLf & SqlStrReceipt & SqlStr2
        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
        '
        '    SqlStr = SqlStr1 & vbCrLf & SqlStrPayment & SqlStr2
        '    MainClass.AssignDataInSprd SqlStr, AData2, StrConn, "Y"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                If RsTemp.Fields("Amount").Value < 0 Then
                    With SprdReceipt
                        .Row = .MaxRows
                        .Col = ColLocked
                        .Text = IIf(IsDbNull(RsTemp.Fields("Locked").Value), "", RsTemp.Fields("Locked").Value)
                        .Col = ColBookType
                        .Text = IIf(IsDbNull(RsTemp.Fields("BookType").Value), "", RsTemp.Fields("BookType").Value)
                        .Col = ColBookSubType
                        .Text = IIf(IsDbNull(RsTemp.Fields("BOOKSUBTYPE").Value), "", RsTemp.Fields("BOOKSUBTYPE").Value)
                        .Col = ColVDate
                        .Text = IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value)
                        .Col = ColChequeNo
                        .Text = IIf(IsDbNull(RsTemp.Fields("CHEQUENO").Value), "", RsTemp.Fields("CHEQUENO").Value)
                        .Col = ColChequeDate
                        .Text = IIf(IsDbNull(RsTemp.Fields("CHQDATE").Value), "", RsTemp.Fields("CHQDATE").Value)
                        .Col = ColVNo
                        .Text = IIf(IsDbNull(RsTemp.Fields("V_NO").Value), "", RsTemp.Fields("V_NO").Value)
                        .Col = colAccount
                        .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                        .Col = ColAmount
                        .Text = VB6.Format(System.Math.Abs(IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)), "0.00")

                        .Col = ColUnit
                        .Text = IIf(IsDBNull(RsTemp.Fields("COMPANY_SHORTNAME").Value), "", RsTemp.Fields("COMPANY_SHORTNAME").Value)

                        .Col = ColBankName
                        .Text = IIf(IsDbNull(RsTemp.Fields("CUST_BANK_BANK").Value), "", RsTemp.Fields("CUST_BANK_BANK").Value)
                        .Col = ColAccountNo
                        .Text = IIf(IsDbNull(RsTemp.Fields("CUST_BANK_ACCT_NO").Value), "", RsTemp.Fields("CUST_BANK_ACCT_NO").Value)
                        .Col = ColIFSCCode
                        .Text = IIf(IsDbNull(RsTemp.Fields("BANK_IFSC_CODE").Value), "", RsTemp.Fields("BANK_IFSC_CODE").Value)
                        .Col = ColMKEY
                        .Text = IIf(IsDbNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
                        .MaxRows = .MaxRows + 1
                    End With
                End If
                If RsTemp.Fields("Amount").Value > 0 Then
                    With SprdPayment
                        .Row = .MaxRows
                        .Col = ColLocked
                        .Text = IIf(IsDbNull(RsTemp.Fields("Locked").Value), "", RsTemp.Fields("Locked").Value)
                        .Col = ColBookType
                        .Text = IIf(IsDbNull(RsTemp.Fields("BookType").Value), "", RsTemp.Fields("BookType").Value)
                        .Col = ColBookSubType
                        .Text = Trim(IIf(IsDBNull(RsTemp.Fields("BOOKSUBTYPE").Value), "", RsTemp.Fields("BOOKSUBTYPE").Value))
                        .Col = ColVDate
                        .Text = IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value)
                        .Col = ColChequeNo
                        .Text = IIf(IsDbNull(RsTemp.Fields("CHEQUENO").Value), "", RsTemp.Fields("CHEQUENO").Value)
                        .Col = ColChequeDate
                        .Text = IIf(IsDbNull(RsTemp.Fields("CHQDATE").Value), "", RsTemp.Fields("CHQDATE").Value)
                        .Col = ColVNo
                        .Text = IIf(IsDbNull(RsTemp.Fields("V_NO").Value), "", RsTemp.Fields("V_NO").Value)
                        .Col = colAccount
                        .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                        .Col = ColAmount
                        .Text = VB6.Format(System.Math.Abs(IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)), "0.00")

                        .Col = ColUnit
                        .Text = IIf(IsDBNull(RsTemp.Fields("COMPANY_SHORTNAME").Value), "", RsTemp.Fields("COMPANY_SHORTNAME").Value)


                        .Col = ColBankName
                        .Text = IIf(IsDbNull(RsTemp.Fields("CUST_BANK_BANK").Value), "", RsTemp.Fields("CUST_BANK_BANK").Value)
                        .Col = ColAccountNo
                        .Text = IIf(IsDbNull(RsTemp.Fields("CUST_BANK_ACCT_NO").Value), "", RsTemp.Fields("CUST_BANK_ACCT_NO").Value)
                        .Col = ColIFSCCode
                        .Text = IIf(IsDbNull(RsTemp.Fields("BANK_IFSC_CODE").Value), "", RsTemp.Fields("BANK_IFSC_CODE").Value)
                        .Col = ColMKEY
                        .Text = IIf(IsDbNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
                        .MaxRows = .MaxRows + 1
                    End With
                End If
                RsTemp.MoveNext()
            Loop
        End If
        '********************************
        'Get Opening Balance.........
        SqlStr1 = MakeOPSQL
        SqlStr2 = MakeSQLCond(True, "")
        SqlStr = SqlStr1 & vbCrLf & SqlStr2 & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOP, ADODB.LockTypeEnum.adLockReadOnly)
        If RsOP.EOF = False Then
            mOpening = IIf(IsDbNull(RsOP.Fields("OPENING").Value), 0, RsOP.Fields("OPENING").Value)
        End If
        If mOpening <= 0 Then
            lblOpening.Font = VB6.FontChangeBold(lblOpening.Font, True)
            lblOpening.ForeColor = System.Drawing.Color.Red
            lblOpening.Text = VB6.Format(System.Math.Abs(mOpening), "0.00") & "Cr"
        Else
            lblOpening.Font = VB6.FontChangeBold(lblOpening.Font, True)
            lblOpening.ForeColor = System.Drawing.Color.Blue
            lblOpening.Text = VB6.Format(mOpening, "0.00") & "Dr"
        End If
        Call DisplayTotals(SprdReceipt)
        Call DisplayTotals(SprdPayment)
        SprdReceipt.Row = SprdReceipt.MaxRows
        SprdReceipt.Col = ColAmount
        mDrAmount = Val(SprdReceipt.Text)
        SprdPayment.Row = SprdPayment.MaxRows
        SprdPayment.Col = ColAmount
        mCrAmount = Val(SprdPayment.Text)
        mBalance = mOpening + mDrAmount - mCrAmount
        If mBalance <= 0 Then
            lblClosing.Font = VB6.FontChangeBold(lblClosing.Font, True)
            lblClosing.ForeColor = System.Drawing.Color.Red
            lblClosing.Text = VB6.Format(System.Math.Abs(mBalance), "0.00") & "Cr"
        Else
            lblClosing.Font = VB6.FontChangeBold(lblClosing.Font, True)
            lblClosing.ForeColor = System.Drawing.Color.Blue
            lblClosing.Text = VB6.Format(mBalance, "0.00") & "Dr"
        End If
        BookInfo = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
LedgError:
        BookInfo = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub FillRunBalCol(ByRef fpGrid As AxFPSpreadADO.AxfpSpread)
        On Error GoTo ERR1
        Dim ii As Integer
        Dim mAmount As Double
        Dim mTotAmount As Double
        With fpGrid
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColAmount
                If IsNumeric(.Text) Then
                    mAmount = CDbl(.Text)
                Else
                    mAmount = 0
                End If
                mTotAmount = mTotAmount + mAmount
            Next
            For ii = .MaxRows To .MaxRows
                .Row = ii
                .Col = ColAmount
                .Text = MainClass.FormatRupees(System.Math.Abs(mTotAmount))
                .FontBold = True
            Next
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
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
        SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
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
            SqlStr = " SELECT '' AS LOCKED, TRN.BOOKTYPE, TRN.BOOKSUBTYPE , " & vbCrLf _
                & "  TO_CHAR(TRN.VDATE,'DD/MM/YYYY') AS VDATE,CHEQUENO  AS CHEQUENO,CHQDATE,TRN.VNO AS V_NO, " & vbCrLf _
                & " ACM.SUPP_CUST_NAME, "

            SqlStr = SqlStr & vbCrLf _
                & " SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) AS AMOUNT, "

            SqlStr = SqlStr & vbCrLf _
                & " COMP.COMPANY_SHORTNAME,ACM.CUST_BANK_BANK, ACM.CUST_BANK_ACCT_NO, ACM.BANK_IFSC_CODE, TRN.MKEY "
        ElseIf OptSumDet(1).Checked = True Then
            SqlStr = " SELECT '' AS LOCKED, '' AS BOOKTYPE,'' AS BOOKSUBTYPE,TO_CHAR(TRN.VDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS CHEQUENO,'' AS CHQDATE,'' AS V_NO, '' AS SUPP_CUST_NAME, "
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))),'9,99,99,99,999.99') AS AMOUNT, "
            SqlStr = SqlStr & vbCrLf & " '','', '' ,'', '' AS MKEY"
        ElseIf OptSumDet(2).Checked = True Then
            SqlStr = " SELECT '','','', TO_CHAR(Vdate,'MON-YYYY') AS VDATE, " & vbCrLf & " '','', '', '',"
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))),'9,99,99,99,999.99') AS AMOUNT, "
            SqlStr = SqlStr & vbCrLf & " '','', '' ,'', ''"
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
    Private Function MakeSQLCond(ByRef mIsOpening As Boolean, ByRef mBookView As String, Optional ByRef mBookType As String = "", Optional ByRef mDC As String = "", Optional ByRef mBookSubType As String = "") As String
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
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND TRN.Company_Code=ACM.Company_Code " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "

        '            & " TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _

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

        If mBookView = "CB" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND Temp_ViewBook.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
                & " AND TRN.BOOKTYPE =Temp_ViewBook.BOOKTYPE AND TRN.MKEY =Temp_ViewBook.MKEY"
        End If
        If mIsOpening = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND TRN.Vdate<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            SqlStr = SqlStr & vbCrLf _
                & " AND TRN.Vdate BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        '06.01.2004  commit
        '    If mBookType <> "" Then
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND TRN.BOOKTYPE='" & mBookType & "'"
        '    End If
        '    If mBookSubType <> "" Then
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND TRN.BookSubType='" & mBookSubType & "'"
        '    End If
        '    If mDC <> "" Then
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND TRN.DC='" & mDC & "'"
        '    End If
        '    mGroupOption = GetGroupOption
        '    If mIsOpening = True Then
        '        mGroupOption = mGroupOption & vbCrLf & IIf(mGroupOption = "", "", " OR ") & " TRN.BookType = '" & ConOpeningBook & "'"
        '    End If
        '    If mGroupOption <> "" Then
        '        SqlStr = SqlStr & vbCrLf & " And ( " & mGroupOption & " ) "
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
    Private Sub FillHeading(ByRef pGrid As AxFPSpreadADO.AxfpSpread)
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
