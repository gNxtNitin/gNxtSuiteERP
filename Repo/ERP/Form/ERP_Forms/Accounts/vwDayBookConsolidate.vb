Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewDayBookConsolidate
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const ColSchema As Short = 1
    Private Const ColLocked As Short = 2
    Private Const ColBookType As Short = 3
    Private Const ColBookSubType As Short = 4
    Private Const ColVDate As Short = 5
    Private Const ColChequeNo As Short = 6
    Private Const ColChequeDate As Short = 7
    Private Const ColVNo As Short = 8
    Private Const colAccount As Short = 9
    Private Const ColAmount As Short = 10
    Private Const ColBankName As Short = 11
    Private Const ColAccountNo As Short = 12
    Private Const ColIFSCCode As Short = 13
    Private Const ColClearDate As Short = 14
    Private Const ColOurUnit As Short = 15
    Private Const ColOurBank As Short = 16
    Private Const ColNature As Short = 17
    Private Const ColRemarks As Short = 18
    Private Const ColCompanyCode As Short = 19
    Private Const ColMKEY As Short = 20
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
        Dim mUnitName As String
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
                SprdPayment.Col = ColOurUnit
                mUnitName = Trim(SprdPayment.Text)
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
                .Cells(mHeadingline, 11).Value = mUnitName
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
        If BookInfo = False Then GoTo ErrPart
        SprdReceipt.Focus()
        Call PrintStatus(True)
        MainClass.SetFocusToCell(SprdReceipt, mActiveRow, colAccount)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub DisplayTotals(ByRef fpGrid As Object)
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
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = &H8000000F
            .BlockMode = False
            Call FillRunBalCol(fpGrid)
            Call FormatSprd(fpGrid, .MaxRows)
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Public Sub frmViewDayBookConsolidate_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
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
    Private Sub frmViewDayBookConsolidate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7470
        CurrFormWidth = 14265
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7470)
        'Me.Width = VB6.TwipsToPixelsX(14265)
        Call FillComboBox()
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY") 'Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        OptSumDet(0).Checked = True
        Call frmViewDayBookConsolidate_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillComboBox()
        On Error GoTo ErrPart
        Dim mCurrentUser As String
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
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
        mCurrentUser = UCase(VB.Left(DBConUID, 6))
        cboUnit.Items.Clear()
        SqlStr = "SELECT USERNAME FROM ALL_USERS " & vbCrLf & " WHERE USERNAME LIKE '" & mCurrentUser & "%' ORDER BY USERNAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        cboUnit.Items.Add("ALL")
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboUnit.Items.Add(RS.Fields("UserName").Value)
                RS.MoveNext()
            Loop
        End If
        cboUnit.Text = DBConUID
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewDayBookConsolidate_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
    Private Sub frmViewDayBookConsolidate_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
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
        SprdReceipt.Row = SprdReceipt.ActiveRow
        SprdReceipt.Col = ColSchema
        If Trim(SprdReceipt.Text) <> DBConUID Then
            Exit Sub
        End If
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
        MainClass.SearchMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr)
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
    Private Sub FormatSprd(ByRef pGrid As Object, ByRef Arow As Integer)
        With pGrid
            .MaxCols = ColMKEY
            .RowHeight(0) = RowHeight * 1.25
            .ColWidth(0) = 0 ' 4.5
            .RowHeight(-1) = RowHeight
            .Row = -1
            .Col = ColSchema
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColSchema) = 15
            .ColHidden = True
            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColLocked) = 15
            .ColHidden = True
            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColBookType) = 15
            .ColHidden = True
            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColBookSubType) = 2
            .ColHidden = True
            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColVDate) = 7.5
            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColVNo) = 7
            .ColHidden = False '' IIf(lblBookType.text = ConBankBook, True, False)
            .Col = colAccount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(colAccount) = IIf(lblBookType.Text = ConBankBook, 14.5, 21)
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
            .ColWidth(ColChequeNo) = 6
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
            .ColWidth(ColChequeDate) = 7.5
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
            .ColWidth(ColAmount) = 7.5
            .Col = ColBankName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColBankName) = 15
            .ColHidden = IIf(chkBankDetail.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            .Col = ColAccountNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColAccountNo) = 15
            .ColHidden = IIf(chkBankDetail.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            .Col = ColIFSCCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColIFSCCode) = 10
            .ColHidden = IIf(chkBankDetail.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            .Col = ColClearDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColClearDate) = 7.5
            .Col = ColOurUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColOurUnit) = 10
            .ColHidden = False
            .Col = ColOurBank
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColOurBank) = 10
            .ColHidden = IIf(chkBankDetail.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            .Col = ColNature
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColNature) = 10
            .ColHidden = IIf(chkBankDetail.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColRemarks) = 10
            .ColHidden = IIf(chkBankDetail.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            .Col = ColCompanyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColCompanyCode) = 8
            .ColHidden = True ''IIf(chkBankDetail.Value = vbChecked, False, True)
            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColWidth(ColMKEY) = 8
            .ColHidden = True
            Call FillHeading(pGrid)
            MainClass.SetSpreadColor(pGrid, -1)
            MainClass.ProtectCell(pGrid, 1, .MaxRows, 1, .MaxCols)
            pGrid.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            pGrid.DAutoCellTypes = True
            pGrid.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            pGrid.GridColor = &HC00000
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
        Dim mUnClearBalance As Double
        Dim mSchemaName As String
        Dim mUnitFrom As Integer
        Dim mUnitTo As Integer
        Dim cntUnit As Integer
        Dim xAccountCode As String
        Dim mTableName As String
        Dim mCompanyCode As Integer
        Dim mSql As String
        Dim RsCC As ADODB.Recordset
        BookInfo = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If InsertIntoTemp = False Then GoTo LedgError
        If cboUnit.SelectedIndex = 0 Then
            mUnitFrom = 1
            mUnitTo = cboUnit.Items.Count - 1
        Else
            mUnitFrom = cboUnit.SelectedIndex
            mUnitTo = cboUnit.SelectedIndex
        End If
        For cntUnit = mUnitFrom To mUnitTo
            mSchemaName = VB6.GetItemString(cboUnit, cntUnit)
            mTableName = mSchemaName & "." & "GEN_COMPANY_MST"
            mSql = "SELECT COMPANY_CODE FROM " & mTableName & " WHERE STATUS='O'"
            '        If mSchemaName = DBConUID Then
            '            mSql = mSql & vbCrLf & " AND COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""
            '        End If
            MainClass.UOpenRecordSet(mSql, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCC, ADODB.LockTypeEnum.adLockReadOnly)
            If RsCC.EOF = False Then
                Do While RsCC.EOF = False
                    mCompanyCode = RsCC.Fields("COMPANY_CODE").Value
                    mTableName = mSchemaName & "." & "FIN_SUPP_CUST_MST"
                    If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", mTableName, PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                        xAccountCode = MasterNo
                    Else
                        xAccountCode = "-1"
                    End If
                    If xAccountCode <> "-1" Then
                        SqlStr1 = MakeSQL(mSchemaName)
                        SqlStrReceipt = MakeSQLCond(mSchemaName, mCompanyCode, False, "CB", VB.Left(ConBankReceipt, 1), "C", VB.Right(ConBankReceipt, 1))
                        SqlStrPayment = MakeSQLCond(mSchemaName, mCompanyCode, False, "CB", VB.Left(ConBankPayment, 1), "D", VB.Right(ConBankPayment, 1))
                        SqlStr2 = " AND ACCOUNTCODE<>'" & MainClass.AllowSingleQuote(xAccountCode) & "' "
                        If OptSumDet(0).Checked = True Then
                            SqlStr2 = SqlStr2 & vbCrLf & " GROUP BY TRN.VDATE," & vbCrLf & " TRN.BOOKTYPE , TRN.BOOKSUBTYPE , TRN.VNO,  " & vbCrLf & " ACM.SUPP_CUST_NAME,TRN.MKEY,CHEQUENO,CHQDATE,ACM.CUST_BANK_BANK, ACM.CUST_BANK_ACCT_NO, ACM.BANK_IFSC_CODE,TRN.CLEARDATE,ACM.ALIAS_NAME,ACM.SUPP_CUST_NATURE,CMST.COMPANY_SHORTNAME,TRN.COMPANY_CODE  " & vbCrLf & " --ORDER BY TRN.COMPANY_CODE,TRN.VDATE, TRN.VNO"
                        ElseIf OptSumDet(1).Checked = True Then
                            SqlStr2 = SqlStr2 & vbCrLf & " GROUP BY TRN.VDATE,CMST.COMPANY_SHORTNAME,TRN.COMPANY_CODE " & vbCrLf & " --ORDER BY TRN.COMPANY_CODE,TRN.VDATE "
                        ElseIf OptSumDet(2).Checked = True Then
                            SqlStr2 = SqlStr2 & vbCrLf & " GROUP BY SUBSTR(Vdate,4,3),TO_CHAR(Vdate,'YYYYMM'),CMST.COMPANY_SHORTNAME,TRN.COMPANY_CODE" & vbCrLf & " --ORDER BY TRN.COMPANY_CODE,TO_CHAR(Vdate,'YYYYMM')"
                        End If
                        If SqlStr = "" Then
                            SqlStr = SqlStr1 & vbCrLf & SqlStrReceipt & SqlStr2
                        Else
                            SqlStr = SqlStr & vbCrLf & " UNION ALL " & vbCrLf & SqlStr1 & vbCrLf & SqlStrReceipt & SqlStr2
                        End If
                    End If
                    RsCC.MoveNext()
                Loop
            End If
        Next
        If SqlStr = "" Then
            BookInfo = True
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY 19, 5, 8"
        End If
        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
        '
        '    SqlStr = SqlStr1 & vbCrLf & SqlStrPayment & SqlStr2
        '    MainClass.AssignDataInSprd SqlStr, AData2, StrConn, "Y"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                If RsTemp.Fields("Amount").Value < 0 Then
                    With SprdReceipt
                        .Row = .MaxRows + 1
                        .Col = ColSchema
                        .Text = IIf(IsDbNull(RsTemp.Fields("SCHEMA_NAME").Value), "", RsTemp.Fields("SCHEMA_NAME").Value)
                        .Col = ColClearDate
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CLEARDATE").Value), "", RsTemp.Fields("CLEARDATE").Value), "DD/MM/YYYY")
                        .Col = ColOurUnit
                        .Text = IIf(IsDbNull(RsTemp.Fields("COMPANY_ALIAS").Value), "", RsTemp.Fields("COMPANY_ALIAS").Value)
                        .Col = ColOurBank
                        .Text = IIf(IsDbNull(RsTemp.Fields("ALIAS_NAME").Value), "", RsTemp.Fields("ALIAS_NAME").Value) ''ALIAS_NAME
                        .Col = ColNature
                        .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NATURE").Value), "", RsTemp.Fields("SUPP_CUST_NATURE").Value) ''ALIAS_NAME
                        .Col = ColCompanyCode
                        .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("COMPANY_CODE").Value), "", RsTemp.Fields("COMPANY_CODE").Value)))
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
                        .Row = .MaxRows + 1
                        .Col = ColSchema
                        .Text = IIf(IsDbNull(RsTemp.Fields("SCHEMA_NAME").Value), "", RsTemp.Fields("SCHEMA_NAME").Value)
                        .Col = ColClearDate
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CLEARDATE").Value), "", RsTemp.Fields("CLEARDATE").Value), "DD/MM/YYYY")
                        .Col = ColOurUnit
                        .Text = IIf(IsDbNull(RsTemp.Fields("COMPANY_ALIAS").Value), "", RsTemp.Fields("COMPANY_ALIAS").Value)
                        .Col = ColOurBank
                        .Text = IIf(IsDbNull(RsTemp.Fields("ALIAS_NAME").Value), "", RsTemp.Fields("ALIAS_NAME").Value) ''ALIAS_NAME
                        .Col = ColNature
                        .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NATURE").Value), "", RsTemp.Fields("SUPP_CUST_NATURE").Value)
                        .Col = ColCompanyCode
                        .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("COMPANY_CODE").Value), "", RsTemp.Fields("COMPANY_CODE").Value)))
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
        mOpening = 0
        For cntUnit = mUnitFrom To mUnitTo
            mSchemaName = VB6.GetItemString(cboUnit, cntUnit)
            mTableName = mSchemaName & "." & "GEN_COMPANY_MST"
            mSql = "SELECT COMPANY_CODE FROM " & mTableName & " WHERE STATUS='O'"
            '        If mSchemaName = DBConUID Then
            '            mSql = mSql & vbCrLf & " AND COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""
            '        End If
            MainClass.UOpenRecordSet(mSql, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCC, ADODB.LockTypeEnum.adLockReadOnly)
            If RsCC.EOF = False Then
                Do While RsCC.EOF = False
                    mCompanyCode = RsCC.Fields("COMPANY_CODE").Value
                    mTableName = mSchemaName & "." & "FIN_SUPP_CUST_MST"
                    If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", mTableName, PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                        xAccountCode = MasterNo
                    Else
                        xAccountCode = "-1"
                    End If
                    SqlStr1 = MakeOPSQL
                    SqlStr2 = MakeSQLCond(mSchemaName, mCompanyCode, True, "")
                    SqlStr = SqlStr1 & vbCrLf & SqlStr2 & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(xAccountCode) & "'"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOP, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsOP.EOF = False Then
                        mOpening = mOpening + IIf(IsDbNull(RsOP.Fields("OPENING").Value), 0, RsOP.Fields("OPENING").Value)
                    End If
                    RsCC.MoveNext()
                Loop
            End If
        Next
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
        '    If mUnClearBalance > 0 Then
        lblUnClearAmount.Font = VB6.FontChangeBold(lblUnClearAmount.Font, True)
        lblUnClearAmount.ForeColor = System.Drawing.Color.Red
        lblUnClearAmount.Text = VB6.Format(System.Math.Abs(mUnClearBalance), "0.00") & IIf(mUnClearBalance <= 0, "Cr", "Dr")
        '    End If
        BookInfo = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
LedgError:
        '    Resume
        BookInfo = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub FillRunBalCol(ByRef fpGrid As Object)
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
        Dim mSchemaName As String
        Dim mUnitFrom As Integer
        Dim mUnitTo As Integer
        Dim cntUnit As Integer
        Dim xAccountCode As String
        Dim mTableName As String
        Dim mCompanyCode As Integer
        Dim mSql As String
        Dim RsCC As ADODB.Recordset
        InsertIntoTemp = False
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = "DELETE FROM Temp_ViewBook NOLOGGING " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)
        If cboUnit.SelectedIndex = 0 Then
            mUnitFrom = 1
            mUnitTo = cboUnit.Items.Count - 1
        Else
            mUnitFrom = cboUnit.SelectedIndex
            mUnitTo = cboUnit.SelectedIndex
        End If
        For cntUnit = mUnitFrom To mUnitTo
            mSchemaName = VB6.GetItemString(cboUnit, cntUnit)
            mTableName = mSchemaName & "." & "GEN_COMPANY_MST"
            mSql = "SELECT COMPANY_CODE FROM " & mTableName & " WHERE STATUS='O'"
            '        If mSchemaName = DBConUID Then
            '            mSql = mSql & vbCrLf & " AND COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""
            '        End If
            MainClass.UOpenRecordSet(mSql, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCC, ADODB.LockTypeEnum.adLockReadOnly)
            If RsCC.EOF = False Then
                Do While RsCC.EOF = False
                    mCompanyCode = RsCC.Fields("COMPANY_CODE").Value
                    mTableName = mSchemaName & "." & "FIN_SUPP_CUST_MST"
                    If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", mTableName, PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                        xAccountCode = MasterNo
                    Else
                        xAccountCode = "-1"
                    End If
                    If xAccountCode <> "-1" Then
                        SqlStr1 = "SELECT DISTINCT " & mCompanyCode & ", '" & mSchemaName & "', '" & MainClass.AllowSingleQuote(PubUserID) & "',TRN.BOOKTYPE,TRN.MKEY"
                        SqlStr2 = MakeSQLCond(mSchemaName, mCompanyCode, False, "")
                        SqlStr = SqlStr1 & vbCrLf & SqlStr2
                        SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(xAccountCode) & "'"
                        mSqlStr = ""
                        mSqlStr = "INSERT INTO Temp_ViewBook (" & vbCrLf & " COMPANY_CODE, USER_SCHEMA, USERID, BOOKTYPE, MKEY) "
                        mSqlStr = mSqlStr & vbCrLf & SqlStr
                        PubDBCn.Execute(mSqlStr)
                    End If
                    RsCC.MoveNext()
                Loop
            End If
        Next
        PubDBCn.CommitTrans()
        InsertIntoTemp = True
        Exit Function
LedgError:
        ''Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        InsertIntoTemp = False
        PubDBCn.RollbackTrans()
    End Function
    Private Function MakeSQL(ByRef mSchemaName As String) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        If OptSumDet(0).Checked = True Then
            SqlStr = " SELECT '" & mSchemaName & "' AS SCHEMA_NAME, '' AS LOCKED, TRN.BOOKTYPE, TRN.BOOKSUBTYPE , " & vbCrLf & "  TO_CHAR(TRN.VDATE,'DD/MM/YYYY') AS VDATE,CHEQUENO  AS CHEQUENO,CHQDATE,TRN.VNO AS V_NO, " & vbCrLf & " ACM.SUPP_CUST_NAME, "
            SqlStr = SqlStr & vbCrLf & " SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1)) AS AMOUNT, "
            SqlStr = SqlStr & vbCrLf & " '',ACM.CUST_BANK_BANK, ACM.CUST_BANK_ACCT_NO, ACM.BANK_IFSC_CODE, TRN.CLEARDATE,'" & mSchemaName & "',ACM.ALIAS_NAME,ACM.SUPP_CUST_NATURE,'', CMST.COMPANY_SHORTNAME AS COMPANY_ALIAS, TRN.COMPANY_CODE, TRN.MKEY "
        ElseIf OptSumDet(1).Checked = True Then
            SqlStr = " SELECT '" & mSchemaName & "' AS SCHEMA_NAME, '' AS LOCKED, '' AS BOOKTYPE,'' AS BOOKSUBTYPE,TO_CHAR(TRN.VDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS CHEQUENO,'' AS CHQDATE,'' AS V_NO, '' AS SUPP_CUST_NAME, "
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))),'9,99,99,99,999.99') AS AMOUNT, "
            SqlStr = SqlStr & vbCrLf & " '','', '' ,'','','','','','',CMST.COMPANY_SHORTNAME AS COMPANY_ALIAS, TRN.COMPANY_CODE, '' AS MKEY"
        ElseIf OptSumDet(2).Checked = True Then
            SqlStr = " SELECT '" & mSchemaName & "' AS SCHEMA_NAME, '','','', SUBSTR(Vdate,4,3) AS VDATE, " & vbCrLf & " '','', '', '',"
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))),'9,99,99,99,999.99') AS AMOUNT, "
            SqlStr = SqlStr & vbCrLf & " '','', '' ,'','','','','','',CMST.COMPANY_SHORTNAME AS COMPANY_ALIAS, TRN.COMPANY_CODE, ''"
        End If
        '    Private Const ColClearDate = 14
        'Private Const ColOurUnit = 15
        'Private Const ColOurBank = 16
        'Private Const ColNature = 17
        'Private Const ColRemarks = 18
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
    Private Function MakeSQLCond(ByRef mSchemaName As String, ByRef mCompanyCode As Integer, ByRef mIsOpening As Boolean, ByRef mBookView As String, Optional ByRef mBookType As String = "", Optional ByRef mDC As String = "", Optional ByRef mBookSubType As String = "") As String
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
        SqlStr = " FROM " & mSchemaName & ".FIN_POSTED_TRN TRN , " & mSchemaName & ".FIN_SUPP_CUST_MST ACM, " & mSchemaName & ".GEN_COMPANY_MST CMST"
        If mBookView = "CB" Then
            SqlStr = SqlStr & ", Temp_ViewBook"
        End If
        SqlStr = SqlStr & vbCrLf & " WHERE  CMST.Company_Code = " & mCompanyCode & "" & vbCrLf & " AND CMST.Company_Code = TRN.Company_Code " & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND TRN.Company_Code=ACM.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "
        If mBookView = "CB" Then
            SqlStr = SqlStr & vbCrLf & " AND Temp_ViewBook.COMPANY_CODE=TRN.Company_Code " & vbCrLf & " AND Temp_ViewBook.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND TRN.BOOKTYPE =Temp_ViewBook.BOOKTYPE AND TRN.MKEY =Temp_ViewBook.MKEY"
        End If
        If optShow(0).Checked = True Then
            If mIsOpening = True Then
                SqlStr = SqlStr & vbCrLf & " AND TRN.Vdate<'" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "' "
            Else
                SqlStr = SqlStr & " AND TRN.Vdate BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"
            End If
        Else
            If mIsOpening = True Then
                SqlStr = SqlStr & vbCrLf & " AND TRN.Vdate<'" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "' "
            Else
                SqlStr = SqlStr & " AND TRN.CLEARDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"
            End If
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
