Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLoanLedger
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColLoanType As Short = 3
    Private Const ColLoanAmount As Short = 4
    Private Const ColMonth As Short = 5
    Private Const ColInterestAmount As Short = 6
    Private Const ColPrincipalAmount As Short = 7
    Private Const ColInsttAmount As Short = 8
    Private Const ColPaidAmount As Short = 9
    Private Const ColBalance As Short = 10

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdLoan
            .MaxCols = ColBalance
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 2)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCode, 6)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 25)

            .Col = ColLoanType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColLoanType, 9)

            .Col = ColLoanAmount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColLoanAmount, 9)

            .Col = ColMonth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColMonth, 9)
            .TypeMaxEditLen = 5000

            For cntCol = ColInterestAmount To ColBalance
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 8)
            Next

        End With

        MainClass.ProtectCell(sprdLoan, 1, sprdLoan.MaxRows, 1, sprdLoan.MaxCols)
        MainClass.SetSpreadColor(sprdLoan, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(sprdLoan)

        With sprdLoan
            .MaxCols = ColBalance
            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColLoanType
            .Text = "Loan Type"

            .Col = ColLoanAmount
            .Text = "Loan Amount & Date "

            .Col = ColMonth
            .Text = "Month & Year"

            .Col = ColInterestAmount
            .Text = "Interest Amount"

            .Col = ColPrincipalAmount
            .Text = "Principal Amount"

            .Col = ColInsttAmount
            .Text = "Instt. Amount"

            .Col = ColPaidAmount
            .Text = "Paid Amount"

            .Col = ColBalance

            .Col = ColBalance
            .Text = "Balance Amount"

        End With
    End Sub

    Private Function GetPaidAmount(ByRef mEMPCode As String, ByRef mDeductDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT " & vbCrLf & " FROM PAY_MONTHLY_TRN TRN, PAY_SALARYHEAD_MST HEADMST" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.EMP_CODE='" & MainClass.AllowSingleQuote(mEMPCode) & "'" & vbCrLf & " AND TRN.COMPANY_CODE=HEADMST.COMPANY_CODE" & vbCrLf & " AND TRN.ADD_DEDUCTCODE=HEADMST.CODE" & vbCrLf & " AND HEADMST.TYPE=" & ConAdvance & "" & vbCrLf & " AND TO_CHAR(SAL_MONTH,'MM-YYYY')='" & VB6.Format(mDeductDate, "MM-YYYY") & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPaidAmount = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetPaidAmount = 0
    End Function

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        SetDate(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub

    Private Sub frmLoanLedger_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmLoanLedger_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        lblRunDate.Text = CStr(RunDate)
        OptName.Checked = True
        SetDate(CDate(lblRunDate.Text))
        FillHeading()
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub UpDYear_DownClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub
    Private Sub UpDYear_UpClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsEmpLoan As ADODB.Recordset

        Dim cntRow As Integer
        Dim mDeptCode As String
        Dim mLoanAmount As Double
        Dim mKey As String
        Dim mFirstLine As Boolean
        Dim mInterestAmount As Double
        Dim mPrincipalAmount As Double
        Dim mInsttAmount As Double
        Dim mPaidAmount As Double
        Dim mTotalPaidAmount As Double
        Dim mBalance As Double

        MainClass.ClearGrid(sprdLoan)

        If txtEmpCode.Text = "" Then
            MsgInformation("Please select the Employee Name.")
            txtEmpCode.Focus()
            Exit Sub
        End If

        SqlStr = " SELECT EMP.EMP_CODE, EMP.EMP_NAME, " & vbCrLf & " LOAN.LOANTYPE, LOAN.LOANAMOUNT, LOAN.LOANDATE , " & vbCrLf & " LOAN.INSTALMENTAMT, LOAN.DEDUCT_DATE, LOAN.OPPRINCIPALAMT , " & vbCrLf & " LOAN.INTERESTAMT, LOAN.PRINCIPALAMT, LOAN.DEDUCT_AMOUNT "

        SqlStr = SqlStr & vbCrLf & " FROM PAY_LOAN_MST Loan,PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE " & vbCrLf & " LOAN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND LOAN.EMP_CODE=EMP.EMP_CODE" & vbCrLf & " AND LOAN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " And LOAN.LOANDATE <= TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SUBROWNO=1"


        SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpCode.Text)) & "' "

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME,LOANDATE, DEDUCT_DATE"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE,LOANDATE,LOANDATE, DEDUCT_DATE"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpLoan, ADODB.LockTypeEnum.adLockOptimistic)

        mBalance = 0
        mFirstLine = True
        If RsEmpLoan.EOF = False Then
            With sprdLoan
                Do While Not RsEmpLoan.EOF
                    .Row = .MaxRows
                    .Col = 0
                    .Text = CStr(.Row)
                    '                mKey = IIf(IsNull(RsEmpLoan!mKey), "", RsEmpLoan!mKey)

                    .Col = ColCode
                    If mFirstLine = True Then
                        .Text = IIf(IsDbNull(RsEmpLoan.Fields("EMP_CODE").Value), "", RsEmpLoan.Fields("EMP_CODE").Value)
                    Else
                        .Text = ""
                    End If

                    .Col = ColName
                    If mFirstLine = True Then
                        .Text = RsEmpLoan.Fields("EMP_NAME").Value
                    Else
                        .Text = ""
                    End If

                    .Col = ColLoanType
                    If mFirstLine = True Then
                        .Text = CStr(IIf(IsDbNull(RsEmpLoan.Fields("LOANTYPE").Value), "", RsEmpLoan.Fields("LOANTYPE").Value))
                    Else
                        .Text = ""
                    End If

                    .Col = ColLoanAmount
                    mLoanAmount = IIf(IsDbNull(RsEmpLoan.Fields("LOANAMOUNT").Value), 0, RsEmpLoan.Fields("LOANAMOUNT").Value)
                    '                If mFirstLine = True Then
                    .Text = VB6.Format(IIf(IsDbNull(RsEmpLoan.Fields("LOANAMOUNT").Value), 0, RsEmpLoan.Fields("LOANAMOUNT").Value), "0.00") & vbNewLine & VB6.Format(IIf(IsDbNull(RsEmpLoan.Fields("LOANDATE").Value), "", RsEmpLoan.Fields("LOANDATE").Value), "MMM-YYYY")
                    '                Else
                    '                    .Text = ""
                    '                End If

                    .Col = ColMonth
                    .Text = VB6.Format(IIf(IsDbNull(RsEmpLoan.Fields("DEDUCT_DATE").Value), "", RsEmpLoan.Fields("DEDUCT_DATE").Value), "MMM-YYYY")

                    .Col = ColInterestAmount
                    mInterestAmount = IIf(IsDbNull(RsEmpLoan.Fields("INTERESTAMT").Value), 0, RsEmpLoan.Fields("INTERESTAMT").Value)
                    .Text = VB6.Format(mInterestAmount, "0.00")

                    .Col = ColPrincipalAmount
                    mPrincipalAmount = IIf(IsDbNull(RsEmpLoan.Fields("PRINCIPALAMT").Value), 0, RsEmpLoan.Fields("PRINCIPALAMT").Value)
                    .Text = VB6.Format(mPrincipalAmount, "0.00")

                    .Col = ColInsttAmount
                    mInsttAmount = IIf(IsDbNull(RsEmpLoan.Fields("DEDUCT_AMOUNT").Value), 0, RsEmpLoan.Fields("DEDUCT_AMOUNT").Value)
                    .Text = VB6.Format(mInsttAmount, "0.00")

                    .Col = ColPaidAmount
                    mPaidAmount = GetPaidAmount(RsEmpLoan.Fields("EMP_CODE").Value, RsEmpLoan.Fields("DEDUCT_DATE").Value)
                    .Text = VB6.Format(mPaidAmount, "0.00")

                    .Col = ColBalance
                    mTotalPaidAmount = mTotalPaidAmount + mPaidAmount
                    mBalance = mLoanAmount - mTotalPaidAmount
                    .Text = VB6.Format(mBalance, "0.00")

                    mFirstLine = False
                    RsEmpLoan.MoveNext()
                    If Not RsEmpLoan.EOF Then
                        '                    If mKey <> IIf(IsNull(RsEmpLoan!mKey), "", RsEmpLoan!mKey) Then
                        '                        mBalance = 0
                        '                        mPaidAmount = 0
                        '                        mFirstLine = True
                        '                    End If
                        .MaxRows = .MaxRows + 1
                    End If
                Loop
                MainClass.ProtectCell(sprdLoan, 0, .MaxRows, 0, .MaxCols)
            End With
        End If
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        Daysinmonth = MainClass.LastDay(VB6.Format(lblYear.Text, "mm"), VB6.Format(lblYear.Text, "yyyy"))
        Tempdate = Daysinmonth & "/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)
    End Sub


    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        PubDBCn.Errors.Clear()


        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(sprdLoan, 1, sprdLoan.MaxRows, 0, sprdLoan.MaxCols, PubDBCn) = False Then GoTo ERR1



        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = " AS ON : " & lblYear.Text
        mTitle = "Loan Ledger ( " & txtEmpCode.Text & "- " & txtName.Text & ")"
        Call ShowReport(SqlStr, "LoanLdgr.Rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
End Class
