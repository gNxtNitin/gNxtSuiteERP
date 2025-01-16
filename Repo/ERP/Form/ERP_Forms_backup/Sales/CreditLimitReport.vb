Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCreditLimitReport
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim FormLoaded As Boolean
    Dim PrintEnable As Boolean
    Dim PrintCopies As Short
    Dim NewFlagsSetting As Integer
    Dim OldFlagsSetting As Integer
    Private Const ColPartyCode As Short = 1
    Private Const ColPartyName As Short = 2
    Private Const ColSalesPersonName As Short = 3
    Private Const ColCreditLimit As Short = 4
    Private Const ColLedgerBalance As Short = 5
    Private Const ColPDCCheque As Short = 6
    Private Const ColAvailableCreditBal As Short = 7
    Private Const ColTodayApproval As Short = 8

    Private Const ConRowHeight As Short = 15
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub CboShowFor_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboShowFor.SelectedIndexChanged
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        PrintEnable = False
        PrintCommand()
        TxtName.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
        cmdsearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForReminder(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForReminder(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        'Dim RS As ADODB.Recordset
        Dim Sqlstr As String
        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C') AND INTER_UNIT='N'"

        If MainClass.SearchMaster((TxtName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", Sqlstr) = True Then
            TxtName.Text = AcName
        End If
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ERR1
        If Trim(TxtName.Text) = "" And chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("Account Name Cann't be Blank.")
            TxtName.Focus()
            PrintEnable = False
            PrintCommand()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Trim(txtSalesPersonName.Text) = "" And chkAllSales.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("Sales Person Name Cann't be Blank.")
            txtSalesPersonName.Focus()
            PrintEnable = False
            PrintCommand()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdView)
        PrintEnable = True
        PrintCommand()
        Call FormatSprdView(-1)
        '    If OptSumDet(0).Value = True Then
        ViewOuts()
        '    Else
        '        ViewOutsSummary
        '    End If


        DisplayTotals()
        Call FormatSprdView(-1)


        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillHeading()
        On Error GoTo ErrPart

        With SprdView

            .Row = 0
            .Col = ColPartyCode
            .Text = "Party Code"

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColSalesPersonName
            .Text = "Sales Person Name"

            .Col = ColCreditLimit
            .Text = "Credit Limit"

            .Col = ColLedgerBalance
            .Text = "Ledger Balance"

            .Col = ColPDCCheque
            .Text = "PDC Cheque"

            .Col = ColAvailableCreditBal
            .Text = "Available Credit Balance"

            .Col = ColTodayApproval
            .Text = "Today Credit Limit Enhance"

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdView(ByRef Arow As Integer)
        On Error GoTo ErrPart
        Dim I As Long

        With SprdView
            .Row = Arow
            .MaxCols = ColTodayApproval ''ColPaymentTerms
            .set_RowHeight(0, ConRowHeight * 1.7)
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 5
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPartyCode, 6)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPartyName, 40)

            .Col = ColSalesPersonName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColSalesPersonName, 20)

            For I = ColCreditLimit To ColTodayApproval

                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("999999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(I, 11)
            Next

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' = OperationModeSingle
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With

        Call FillHeading()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmCreditLimitReport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ERR1
        Dim Sqlstr As String
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        Call SetMainFormCordinate(Me)
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FormLoaded = False
        Call FillCombo()
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtName.Enabled = False
        cmdsearch.Enabled = False

        chkAllSales.CheckState = System.Windows.Forms.CheckState.Checked
        txtSalesPersonName.Enabled = False
        cmdsearchSales.Enabled = False

        MainClass.SetControlsColor(Me)
        PrintEnable = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillCombo()
        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim RS As ADODB.Recordset
        CboShowFor.Items.Add("ALL")
        CboShowFor.Items.Add("Exceed Credit Limit")
        CboShowFor.Items.Add("Below Credit Limit")
        CboShowFor.SelectedIndex = 0


        txtAsOnDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub frmCreditLimitReport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        '    Frame4.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
        CurrFormWidth = mReFormWidth
        MainClass.SetSpreadColor(SprdView, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmCreditLimitReport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub ViewOuts()
        On Error GoTo ERR1
        Dim Sqlstr As String
        Dim mPartyCode As String


        Sqlstr = " Select DECODE(GROUP_LIMIT,'Y',TO_CHAR(GMST.GROUP_CODE),ACM.SUPP_CUST_CODE),  DECODE(GROUP_LIMIT,'Y',GMST.GROUP_NAME,ACM.SUPP_CUST_NAME), MAX(ACM.RESPONSIBLE_PERSON)," & vbCrLf _
            & " MAX(CREDIT_LIMIT), " & vbCrLf _
            & " SUM(DECODE(DC,'D',1,-1)*Amount * DECODE(BOOKTYPE,'F',0,1)) AS LEDGER_BALANCE, " & vbCrLf _
            & " SUM(DECODE(DC,'D',1,-1)*Amount * DECODE(BOOKTYPE,'F',1,0)) AS PDC_AMOUNT," & vbCrLf _
            & " MAX(CREDIT_LIMIT) - SUM(DECODE(DC,'D',1,-1)*Amount) As AVAILABLE_AMOUNT," & vbCrLf _
            & " NVL(MAX((SELECT MAX(CREDIT_LIMIT) FROM GEN_INVOICE_UNLOCK_TRN WHERE SUPP_CUST_CODE=ACM.SUPP_CUST_CODE AND APP_DATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))),0) As TODAY_APP_AMOUNT"

        Sqlstr = Sqlstr & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN, FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST GMST "

        Sqlstr = Sqlstr & vbCrLf _
            & " WHERE TRN.Company_Code=Acm.Company_Code AND TRN.AccountCode=Acm.SUPP_CUST_Code "

        Sqlstr = Sqlstr & vbCrLf _
            & " AND Acm.Company_Code=GMST.Company_Code AND ACM.GROUPCODE=GMST.GROUP_CODE "

        Sqlstr = Sqlstr & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        'Sqlstr = Sqlstr & vbCrLf _
        '    & " AND TRN.Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

        If OptSumDet(0).Checked = True Then
            Sqlstr = Sqlstr & " AND ACM.SUPP_CUST_TYPE ='C'"
        Else
            Sqlstr = Sqlstr & " AND ACM.SUPP_CUST_TYPE ='S'"
        End If

        Sqlstr = Sqlstr & " AND ACM.INTER_UNIT ='N'"

        'Sqlstr = Sqlstr & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(txtAsOnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(UCase(TxtName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyCode = MasterNo
                Sqlstr = Sqlstr & vbCrLf & " AND ACM.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If
        End If

        If chkAllSales.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            Sqlstr = Sqlstr & vbCrLf & " AND ACM.RESPONSIBLE_PERSON='" & MainClass.AllowSingleQuote(txtSalesPersonName.Text) & "'"
        End If


        Sqlstr = Sqlstr & vbCrLf & " GROUP BY DECODE(GROUP_LIMIT,'Y',TO_CHAR(GMST.GROUP_CODE),ACM.SUPP_CUST_CODE),  DECODE(GROUP_LIMIT,'Y',GMST.GROUP_NAME,ACM.SUPP_CUST_NAME)"

        'CboShowFor.Items.Add("ALL")
        'CboShowFor.Items.Add("Exceed Credit Limit")
        'CboShowFor.Items.Add("Below Credit Limit")
        'CboShowFor.SelectedIndex = 0


        If CboShowFor.Text = "Exceed Credit Limit" Then
            Sqlstr = Sqlstr & vbCrLf & " HAVING MAX(CREDIT_LIMIT) - SUM(DECODE(DC,'D',1,-1)*Amount)<0"
        ElseIf CboShowFor.Text = "Below Credit Limit" Then
            Sqlstr = Sqlstr & vbCrLf & " HAVING MAX(CREDIT_LIMIT) - SUM(DECODE(DC,'D',1,-1)*Amount)>0"
        End If
        Sqlstr = Sqlstr & vbCrLf & " ORDER BY DECODE(GROUP_LIMIT,'Y',GMST.GROUP_NAME,ACM.SUPP_CUST_NAME)"

        MainClass.AssignDataInSprd8(Sqlstr, SprdView, StrConn, "Y")
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub PrintCommand()
        CmdPreview.Enabled = PrintEnable
        cmdPrint.Enabled = PrintEnable
    End Sub
    Private Sub txtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub txtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim RS As ADODB.Recordset
        Dim Sqlstr As String
        If TxtName.Text = "" Then GoTo EventExitSub
        Sqlstr = ""
        Sqlstr = "Select SUPP_CUST_Code, SUPP_CUST_Name" & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST ACM WHERE " & vbCrLf _
            & " SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtName.Text)) & "'"

        If OptSumDet(0).Checked = True Then
            Sqlstr = Sqlstr & vbCrLf & " AND SUPP_CUST_TYPE ='C'"
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND SUPP_CUST_TYPE ='S'"
        End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
        Else
            MsgBox("Invalid Name", MsgBoxStyle.Information)
            Cancel = True
        End If
        RS.Close()
        RS = Nothing
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub DisplayTotals()
        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mTotDebit As Double
        Dim mTotCredit As Double
        Dim mTotBalance As Double
        Dim mDC As String
        'With SprdView
        '    '        Call MainClass.AddBlankfpSprdRow(SprdView, ColBillNo)
        '    .MaxRows = .MaxRows + 1
        '    .Row = .MaxRows
        '    .Col = ColAvailableCreditBal
        '    .Text = "TOTAL :"
        '    .Font = VB6.FontChangeBold(.Font, True)
        '    .Row = .MaxRows
        '    .Row2 = .MaxRows
        '    .Col = 1
        '    .Col2 = .MaxCols
        '    .BlockMode = True
        '    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '&H80FF80
        '    .BlockMode = False
        '    '        Call CalcRowTotal(SprdView, ColDebit, 1, ColDebit, .MaxRows - 1, .MaxRows, ColDebit)
        '    '        Call CalcRowTotal(SprdView, ColCredit, 1, ColCredit, .MaxRows - 1, .MaxRows, ColCredit)
        '    FormatSprdView(-1)
        '    For cntRow = 1 To .MaxRows - 1
        '        .Row = cntRow
        '        .Col = ColDC
        '        mDC = VB.Left(.Text, 1)
        '        .Col = ColDebit
        '        mTotDebit = mTotDebit + (Val(.Text) * IIf(mDC = "D", 1, -1))
        '        .Col = ColCredit
        '        mTotCredit = mTotCredit + Val(.Text)
        '        .Col = ColBalance
        '        mTotBalance = mTotBalance + (Val(.Text) * IIf(mDC = "D", 1, -1))
        '    Next
        '    .Row = .MaxRows
        '    .Col = ColDebit
        '    .Text = VB6.Format(System.Math.Abs(mTotDebit), "0.00")
        '    .Font = VB6.FontChangeBold(.Font, True)
        '    .Col = ColCredit
        '    .Text = VB6.Format(mTotCredit, "0.00")
        '    .Font = VB6.FontChangeBold(.Font, True)
        '    .Col = ColBalance
        '    .Text = VB6.Format(System.Math.Abs(mTotBalance), "0.00")
        '    .Font = VB6.FontChangeBold(.Font, True)
        '    .Col = ColDC
        '    .Text = IIf(mTotBalance >= 0, "DR", "CR")
        '    .Font = VB6.FontChangeBold(.Font, True)
        '    .set_RowHeight(.Row, 1.25 * ConRowHeight)
        '    '        .RowsFrozen = .MaxRows
        'End With
        PrintCommand()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub ReportForReminder(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim All As Boolean
        Dim Sqlstr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String
        Dim mDate As String
        PubDBCn.Errors.Clear()
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If TxtName.Text = "" Then Exit Sub
        End If

        If chkAllSales.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If txtSalesPersonName.Text = "" Then Exit Sub
        End If

        Sqlstr = ""

        If MainClass.FillPrintDummyDataFromSprd(SprdView, 1, SprdView.MaxRows, 1, SprdView.MaxCols, PubDBCn) = False Then GoTo ERR1

        Sqlstr = ""
        'If chkLegelNotice.CheckState = System.Windows.Forms.CheckState.Checked Then
        Sqlstr = MainClass.FetchFromTempData(Sqlstr, "SUBROW")
        'Else
        '    Sqlstr = FetchRecordForReport(Sqlstr)
        'End If

        mTitle = "CREDIT LIMIT REPORT" & mDate ''Format(txtDateTo.Text, "DD MMM, YYYY")
        mSubTitle = ""
        mRPTName = "CreditLimitReport.Rpt"
        Call ShowReport(Sqlstr, mRPTName, Mode, mTitle, mSubTitle)
        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        'Resume
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        mSqlStr = mSqlStr & "SELECT * " _
            & " FROM TEMP_PrintDummyData " & vbCrLf _
            & " WHERE  " & vbCrLf _
            & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf _
            & " ORDER BY Field1,TO_DATE(Field17,'DD/MM/YYYY'),Field7"
        FetchRecordForReport = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        'Dim mInterest As String
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub txtAsOnDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAsOnDate.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub txtAsOnDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAsOnDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtAsOnDate) = False Then
            txtAsOnDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub chkAllSales_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSales.CheckStateChanged
        PrintEnable = False
        PrintCommand()
        txtSalesPersonName.Enabled = IIf(chkAllSales.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
        cmdsearchSales.Enabled = IIf(chkAllSales.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
    End Sub

    Private Sub cmdsearchSales_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSales.Click
        'Dim RS As ADODB.Recordset
        Dim Sqlstr As String
        'Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C') AND INTER_UNIT='N'"

        If MainClass.SearchMaster((txtSalesPersonName.Text), "FIN_SALESPERSON_MST", "NAME", Sqlstr) = True Then
            txtSalesPersonName.Text = AcName
        End If
    End Sub
    Private Sub txtSalesPersonName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSalesPersonName.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub txtSalesPersonName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSalesPersonName.DoubleClick
        cmdsearchSales_Click(cmdsearchSales, New System.EventArgs())
    End Sub
    Private Sub txtSalesPersonName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSalesPersonName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSalesPersonName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSalesPersonName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSalesPersonName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearchSales_Click(cmdsearchSales, New System.EventArgs())
    End Sub
    Private Sub txtSalesPersonName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSalesPersonName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim RS As ADODB.Recordset = Nothing
        Dim Sqlstr As String
        If txtSalesPersonName.Text = "" Then GoTo EventExitSub
        Sqlstr = ""
        Sqlstr = "Select NAME" & vbCrLf _
            & " FROM FIN_SALESPERSON_MST WHERE " & vbCrLf _
            & " NAME='" & MainClass.AllowSingleQuote(UCase(txtSalesPersonName.Text)) & "'"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
        Else
            MsgBox("Invalid Sales Perdson Name", MsgBoxStyle.Information)
            Cancel = True
        End If
        RS.Close()
        RS = Nothing
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
