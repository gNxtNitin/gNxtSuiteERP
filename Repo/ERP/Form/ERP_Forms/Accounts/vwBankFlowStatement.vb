Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewBankFlowStatement
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Dim PvtDBCn As ADODB.Connection
    'Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColDescription As Short = 2
    Dim ColMax As Integer
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        On Error GoTo ERR1
        Dim PrintStatus As Boolean
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
        If MainClass.FillPrintDummyDataFromSprd(SprdReceipt, 1, SprdReceipt.MaxRows, 1, SprdReceipt.MaxCols, PubDBCn) = False Then GoTo ERR1
        'Select Record for print...
        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        mRPTName = "BankFlowStmt.rpt"
        mTitle = "BANK FLOW STATEMENT"
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
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        '    MainClass.AssignCRptFormulas Report1, "mOpening=""" & lblOpening.text & """"
        '    MainClass.AssignCRptFormulas Report1, "mClosing=""" & lblClosing.text & """"
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
        If BookInfo = False Then GoTo ErrPart
        Call FormatSprd(-1, False)
        Call PrintStatus(True)
        MainClass.SetFocusToCell(SprdReceipt, mActiveRow, ColMax)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Public Sub frmViewBankFlowStatement_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Bank Flow Statement"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewBankFlowStatement_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY") 'Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Call FormatSprd(-1, True)
        Call frmViewBankFlowStatement_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmViewBankFlowStatement_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdReceipt.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth
        MainClass.SetSpreadColor(SprdReceipt, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewBankFlowStatement_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub SprdReceipt_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdReceipt.DataColConfig
        SprdReceipt.Row = -1
        SprdReceipt.Col = eventArgs.col
        SprdReceipt.DAutoCellTypes = True
        SprdReceipt.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdReceipt.TypeEditLen = 1000
    End Sub
    Private Sub SprdReceipt_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdReceipt.KeyDownEvent
        '    If KeyCode = vbKeyReturn Then
        '        SprdReceipt_DblClick 1, 1
        '    End If
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
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
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
    Private Sub FormatSprd(ByRef Arow As Integer, ByRef pFillHeading As Boolean)
        Dim I As Integer
        With SprdReceipt
            If pFillHeading = True Then
                Call FillHeading()
            End If
            .MaxCols = ColMax
            .set_RowHeight(0, RowHeight * 2.5)
            .set_ColWidth(0, 0) ' 4.5
            .set_RowHeight(-1, RowHeight)
            .Row = -1
            .RowsFrozen = 1
            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True
            .Col = ColDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDescription, 25)
            .ColHidden = False
            For I = ColDescription + 1 To ColMax
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(I, 12)
            Next
            MainClass.SetSpreadColor(SprdReceipt, -1)
            MainClass.ProtectCell(SprdReceipt, 1, .MaxRows, 1, .MaxCols)
            SprdReceipt.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdReceipt.DAutoCellTypes = True
            SprdReceipt.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdReceipt.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function BookInfo() As Boolean
        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset
        Dim RsShow As ADODB.Recordset
        Dim mOpening As Double
        Dim mAmount As Double
        Dim SqlStrReceipt As String
        Dim SqlStrPayment As String
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim mBalance As Double
        Dim cntCol As Integer
        Dim mBankName As String
        Dim mAccountCode As String
        Dim mBankCode As String
        BookInfo = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Get Opening Balance.........
        With SprdReceipt
            .MaxRows = 3
            .Row = 1
            .Col = ColDescription
            .Text = "OPENING BALANCE"
            .Font = VB6.FontChangeBold(.Font, True)
            .Row = 2
            .Col = ColDescription
            .Text = "RECEIPTS"
            .Font = VB6.FontChangeBold(.Font, True)
            For cntCol = ColDescription + 1 To .MaxCols
                .Row = 0
                .Col = cntCol
                mBankName = Trim(.Text)
                If MainClass.ValidateWithMasterTable(mBankName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mBankCode = MasterNo
                Else
                    mBankCode = "-1"
                End If
                SqlStr1 = MakeOPSQL
                SqlStr2 = MakeSQLCond("O", "")
                SqlStr = SqlStr1 & vbCrLf & SqlStr2 & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mBankCode) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOP, ADODB.LockTypeEnum.adLockReadOnly)
                If RsOP.EOF = False Then
                    mOpening = IIf(IsDbNull(RsOP.Fields("OPENING").Value), 0, RsOP.Fields("OPENING").Value)
                End If
                .Row = 1
                .Col = cntCol
                .Text = VB6.Format(mOpening, "0.00")
            Next
        End With
        ''Receipt.....
        SqlStr = FillGridSQL("R")
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With SprdReceipt
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    .Row = .MaxRows
                    .Col = ColDescription
                    .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    mAccountCode = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                    For cntCol = ColDescription + 1 To .MaxCols
                        .Row = 0
                        .Col = cntCol
                        mBankName = Trim(.Text)
                        If MainClass.ValidateWithMasterTable(mBankName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mBankCode = MasterNo
                        Else
                            mBankCode = "-1"
                        End If
                        If InsertIntoTemp(mBankCode) = False Then GoTo LedgError
                        SqlStr = MakeSQL
                        SqlStr = SqlStr & vbCrLf & MakeSQLCond("", "CB", VB.Left(ConBankReceipt, 1), "C", VB.Right(ConBankReceipt, 1))
                        SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "' "
                        SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE<>'" & MainClass.AllowSingleQuote(mBankCode) & "' "
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)
                        Do While RsShow.EOF = False
                            .Row = .MaxRows
                            .Col = cntCol
                            mAmount = IIf(IsDbNull(RsShow.Fields("Amount").Value), 0, RsShow.Fields("Amount").Value)
                            .Text = VB6.Format(mAmount, "0.00")
                            RsShow.MoveNext()
                        Loop
                    Next
                    RsTemp.MoveNext()
                    .MaxRows = .MaxRows + 1
                Loop
            End If
        End With
        ''Payment.....
        SprdReceipt.Row = SprdReceipt.MaxRows
        SprdReceipt.Col = ColDescription
        SprdReceipt.Text = "PAYMENTS"
        SprdReceipt.Font = VB6.FontChangeBold(SprdReceipt.Font, True)
        SprdReceipt.MaxRows = SprdReceipt.MaxRows + 1
        SqlStr = FillGridSQL("P")
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With SprdReceipt
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    .Row = .MaxRows
                    .Col = ColDescription
                    .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    mAccountCode = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                    For cntCol = ColDescription + 1 To .MaxCols
                        .Row = 0
                        .Col = cntCol
                        mBankName = Trim(.Text)
                        If MainClass.ValidateWithMasterTable(mBankName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mBankCode = MasterNo
                        Else
                            mBankCode = "-1"
                        End If
                        If InsertIntoTemp(mBankCode) = False Then GoTo LedgError
                        SqlStr = MakeSQL
                        SqlStr = SqlStr & vbCrLf & MakeSQLCond("", "CB", VB.Left(ConBankPayment, 1), "D", VB.Right(ConBankPayment, 1))
                        SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "' "
                        SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE<>'" & MainClass.AllowSingleQuote(mBankCode) & "' "
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)
                        Do While RsShow.EOF = False
                            .Row = .MaxRows
                            .Col = cntCol
                            mAmount = IIf(IsDbNull(RsShow.Fields("Amount").Value), 0, RsShow.Fields("Amount").Value)
                            .Text = VB6.Format(mAmount, "0.00")
                            RsShow.MoveNext()
                        Loop
                    Next
                    RsTemp.MoveNext()
                    .MaxRows = .MaxRows + 1
                Loop
            End If
        End With
        'Get Closing Balance.........
        With SprdReceipt
            .Row = .MaxRows
            .Col = ColDescription
            .Text = "CLOSING BALANCE"
            .Font = VB6.FontChangeBold(.Font, True)
            For cntCol = ColDescription + 1 To .MaxCols
                .Row = 0
                .Col = cntCol
                mBankName = Trim(.Text)
                If MainClass.ValidateWithMasterTable(mBankName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mBankCode = MasterNo
                Else
                    mBankCode = "-1"
                End If
                SqlStr1 = MakeOPSQL
                SqlStr2 = MakeSQLCond("C", "")
                SqlStr = SqlStr1 & vbCrLf & SqlStr2 & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mBankCode) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOP, ADODB.LockTypeEnum.adLockReadOnly)
                If RsOP.EOF = False Then
                    mOpening = IIf(IsDbNull(RsOP.Fields("OPENING").Value), 0, RsOP.Fields("OPENING").Value)
                End If
                .Row = .MaxRows
                .Col = cntCol
                .Text = VB6.Format(mOpening, "0.00")
            Next
            .Row = 1
            .Row2 = 1
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F)
            .BlockMode = False
            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F)
            .BlockMode = False
        End With
        BookInfo = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
LedgError:
        BookInfo = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = " SELECT  SUM(TRN.AMOUNT*DECODE(TRN.DC,'C',1,-1)) AS AMOUNT "
        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function
    Private Function InsertIntoTemp(ByRef pAccountCode As String) As Boolean
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
        SqlStr2 = MakeSQLCond("", "")
        SqlStr = SqlStr1 & vbCrLf & SqlStr2
        SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(pAccountCode) & "'"
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
    Private Function FillGridSQL(ByRef mBookSubType As String) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        '
        'Dim mDeptName As String
        'Dim mCostCName As String
        'Dim mConsolidated As String
        'Dim mGroupOption As String
        ''********SELECTION..........
        SqlStr = "SELECT DISTINCT ACM.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME "
        ''********TABLEs..........
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN, FIN_SUPP_CUST_MST ACM"
        ''********Joining..........
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "
        SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        SqlStr = SqlStr & vbCrLf & " AND TRN.BOOKTYPE='B' AND TRN.BOOKSUBTYPE='" & mBookSubType & "'"
        SqlStr = SqlStr & vbCrLf & " AND TRN.VDate >= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TRN.VDate <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ''********GROUP BY CLAUSE..........
        '    Sqlstr = Sqlstr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME "
        ''********ORDER BY CLAUSE..........
        SqlStr = SqlStr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME"
        FillGridSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FillGridSQL = ""
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
    Private Function MakeSQLCond(ByRef mIsOpening As String, ByRef mBookView As String, Optional ByRef mBookType As String = "", Optional ByRef mDC As String = "", Optional ByRef mBookSubType As String = "") As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCostCName As String
        Dim mDeptName As String
        Dim mEmp As String
        Dim mConsolidated As String
        Dim mGroupOption As String
        SqlStr = " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM"
        If mBookView = "CB" Then
            SqlStr = SqlStr & ", Temp_ViewBook"
        End If
        SqlStr = SqlStr & vbCrLf & " WHERE  " & vbCrLf & " TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND TRN.Company_Code=ACM.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "
        If mBookView = "CB" Then
            SqlStr = SqlStr & vbCrLf & " AND Temp_ViewBook.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND TRN.BOOKTYPE =Temp_ViewBook.BOOKTYPE AND TRN.MKEY =Temp_ViewBook.MKEY"
        End If
        If mBookSubType = "R" Then
            SqlStr = SqlStr & " AND TRN.BOOKSUBTYPE<>'P'"
        ElseIf mBookSubType = "P" Then
            SqlStr = SqlStr & " AND TRN.BOOKSUBTYPE<>'R'"
        End If
        '    If mBookSubType = "R" Then
        '        SqlStr = SqlStr & " AND TRN.DC='C'"
        '    ElseIf mBookSubType = "P" Then
        '        SqlStr = SqlStr & " AND TRN.DC='D'"
        '    End If
        If mIsOpening = "O" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.Vdate<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf mIsOpening = "C" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.Vdate<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            SqlStr = SqlStr & " AND TRN.Vdate BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        MakeSQLCond = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQLCond = ""
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub FillHeading()
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mName As String
        mSqlStr = "SELECT  SUPP_CUST_NAME " & vbCrLf & " FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE='2' " & vbCrLf & " AND STATUS='O'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        ColMax = ColDescription
        If RsTemp.EOF = False Then
            ColMax = ColDescription + 1
            SprdReceipt.MaxCols = ColMax
            Do While RsTemp.EOF = False
                mName = Trim(IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
                With SprdReceipt
                    .Row = 0
                    .Col = ColMax
                    .Text = mName
                End With
                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    ColMax = ColMax + 1
                    SprdReceipt.MaxCols = ColMax
                End If
            Loop
        End If
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
